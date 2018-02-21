using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public abstract class MovingEntity : BaseGameEntity
    {

        public MovingEntity(int id) : base(id)
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

        private SteeringBehaviours _steering;

        public void Update(double time_elapsed)
        {
            
        }
    }
}
