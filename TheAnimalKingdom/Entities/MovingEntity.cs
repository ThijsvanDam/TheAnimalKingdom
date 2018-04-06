using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        
        public double Energy { get; set; }
        public double Hunger { get; set; }

        public MovingEntity(Vector2D position, World world) : base(position, world)
        {
            SteeringBehaviours = new SteeringBehaviours(this);
            HashTagLifeGoal = null;
            PathPlanner = new PathPlanner(this);

            Energy = 100;
            Hunger = 0;
            
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
            HashTagLifeGoal?.Process();

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
        }

        public override void Render(Graphics g)
        {
            if (World.GodMode)
            {
                double left = VPos.X - Bradius;
                double top = VPos.Y - Bradius;
                double size = Bradius * 2;
                g.DrawString("H: " + (int)Hunger, new Font(new FontFamily("Times New Roman"), 10f), new SolidBrush(Color.White), (float)left + (float)size + 5f, (float)top - 5f);
                g.DrawString("E: " + (int)Energy, new Font(new FontFamily("Times New Roman"), 10f), new SolidBrush(Color.White), (float)left + (float)size + 5f, (float)top - 15f);
                
                SteeringBehaviours.DrawBehaviors(g);
                HashTagLifeGoal.DrawGoal(g);
            }
        }

        public virtual MovingEntity IsScaredOf()
        {
            return null;
        }

        public abstract double DistanceToClosestLion();
    }
}
