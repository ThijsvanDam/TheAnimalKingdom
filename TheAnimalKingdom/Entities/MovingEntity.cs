using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Behaviours;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public abstract class MovingEntity : BaseGameEntity
    {
        public SteeringBehaviour Steering { get; set; }

        public MovingEntity(Vector2D position, World world) : base(position, world)
        {

        }

        protected Vector2D VVelocity;
        //a normalized vector pointing in the direction the enity is heading.
        protected Vector2D VHeading;
        //a vector perpendicular to the heading vector
        protected Vector2D VSide;

        protected double DMass;

        protected double DMaxSpeed;

        protected double DMaxForce;

        protected double DMaxTurnRate;

        private World _world;


        public override void Update(float time_elapsed)
        {
            Vector2D steeringForce = Steering.Calculate();
            Vector2D acceleration = steeringForce.Divide(DMass);

            VVelocity.Add(acceleration.Multiply(time_elapsed));

            VVelocity.Truncate(DMaxSpeed);

            VPos.Add(VVelocity.Multiply(time_elapsed));

            if (VVelocity.LengthSquared() > 0.00000001)
            {
                VHeading = VVelocity.Normalize();
                VSide = VHeading.Perpendicular();
            }
        }

    }
}
