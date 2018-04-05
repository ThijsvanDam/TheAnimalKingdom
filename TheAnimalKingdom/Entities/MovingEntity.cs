using System.Collections.Generic;
using TheAnimalKingdom.Behaviours;
using TheAnimalKingdom.Goals.Base;
using TheAnimalKingdom.Goals.CompositeGoals;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public abstract class MovingEntity : BaseGameEntity
    {
        public SteeringBehaviours SteeringBehaviours{ get; set; }
        public PathResult FindPathResult { get; set; }
        public PathPlanner PathPlanner { get; set; }
        public CompositeGoal HashTagLifeGoal { get; set; }
        public Stack<NavGraphNode> Route { get; set; }

        public MovingEntity(Vector2D position, World world) : base(position, world)
        {
            SteeringBehaviours = new SteeringBehaviours(this);
            HashTagLifeGoal = new GoalThink(this);
            PathPlanner = new PathPlanner(this);
            
            SteeringBehaviours.ObstacleAvoidanceOn(1.0);
        }

        public Vector2D VVelocity;
        //a normalized vector pointing in the direction the enity is heading.
        protected Vector2D VHeading;
        //a vector perpendicular to the heading vector
        protected Vector2D VSide;

        protected double DMass;
        public double DMaxSpeed;
        public double DMaxForce;
        public double DDeceleration;
        protected double DMaxTurnRate;

        public bool IsAtPosition(Vector2D position)
        {
            var squaredDistance = Vector2D.DistanceSquared(VPos, position);

            return (squaredDistance < 100);
        }

        public override void Update(float time_elapsed)
        {
            HashTagLifeGoal.Process();

            Vector2D steeringForce = SteeringBehaviours.Calculate();
            Vector2D acceleration = steeringForce.Divide(DMass);

            VVelocity.Add(acceleration.Multiply(time_elapsed));

            VVelocity.Truncate(DMaxSpeed);

            VPos.Add(VVelocity.Multiply(time_elapsed));

            if (VVelocity.LengthSquared() > 0.00000001)
            {
                VHeading = VVelocity.Clone().Normalize();
                VSide = VHeading.Perpendicular();
            }
            
            //CheckOutOfScreen();
        }

        private void CheckOutOfScreen()
        {
            if (VPos.X < 0)
            {
                VPos.X = 600;
            }
            if (VPos.X > 600)
            {
                VPos.X = 0;
            }
            if (VPos.Y < 0)
            {
                VPos.Y = 500;
            }
            if (VPos.Y > 500)
            {
                VPos.Y = 0;
            }
        }
    }
}
