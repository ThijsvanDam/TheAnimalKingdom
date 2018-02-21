﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TheAnimalKingdom.Entities
{
    public abstract class MovingEntity : BaseGameEntity
    {
        protected Vector2 VVelocity;
        //a normalized vector pointing in the direction the enity is heading.
        protected Vector2 VHeading;
        //a vector perpendicular to the heading vector
        protected Vector2 VSide;

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