using System;
using System.CodeDom;
using System.Drawing;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours
{
    public class SteeringBehaviours
    {
        private ArriveBehaviour _arrive;
        private FleeBehaviour _flee;
        private SeekBehaviour _seek;
        private StraightWalkingBehaviour _straightWalking;
        private WanderBehaviour _wander;

        private MovingEntity _movingEntity;

        private double _dArrive;
        private double _dFlee;
        private double _dSeek;
        private double _dStraightWalking;
        private double _dWander;

        public SteeringBehaviours(MovingEntity movingEntity)
        {
            _movingEntity = movingEntity;
        }


        private bool _instanceExists(SteeringBehaviour behaviour)
        {
            return behaviour != null;
        }


        public Vector2D Calculate()
        {
            Vector2D sum = new Vector2D(0, 0);

            if (_instanceExists(_arrive))
            {
                Vector2D v = _arrive.Calculate().Multiply(_dArrive);
                Console.WriteLine("Arrive: " + v);
                sum.Add(v);
            }
            if (_instanceExists(_flee))
            {
                Vector2D v = _flee.Calculate().Multiply(_dFlee);
                Console.WriteLine("Flee: " + v);
                sum.Add(v);
            }
            if (_instanceExists(_seek))
            {
                Vector2D v = _seek.Calculate().Multiply(_dSeek);
                Console.WriteLine("Seek: " + v);
                sum.Add(v);
            }
            if (_instanceExists(_straightWalking))
            {
                Vector2D v = _straightWalking.Calculate().Multiply(_dStraightWalking);
                Console.WriteLine("Straight walking: " + v);
                sum.Add(v);
            }
            if (_instanceExists(_wander))
            {
                Vector2D v = _wander.Calculate().Multiply(_dWander);
                Console.WriteLine("Wander: " + v);
                sum.Add(v);
            }

            Vector2D a = sum.Truncate(_movingEntity.DMaxForce);
//            Console.WriteLine(" DIRECT_FORCE_GAIN (TRUNCATED): " + a + " MAXFORCE: " + _movingEntity.DMaxForce);
            return a;
        }

        public void WanderOn(double intensity)
        {
            _wander = new WanderBehaviour(_movingEntity);
            _dWander = intensity;
        }

        public void WanderOff()
        {
            _wander = null;
            _dWander = 0;
        }

        public void StraightWalkingOn(double intensity)
        {
            _straightWalking = new StraightWalkingBehaviour(_movingEntity);
            _dStraightWalking = intensity;
        }

        public void StraightWalkingOff()
        {
            _straightWalking = null;
            _dStraightWalking = 0;
        }


        public void SeekOn(BaseGameEntity goal, double intensity)
        {
            _seek = new SeekBehaviour(_movingEntity, goal);
            _dSeek = intensity;
        }

        public void SeekOff()
        {
            _seek = null;
            _dSeek = 0;
        }

        public void FleeOn(BaseGameEntity enemy, double intensity)
        {
            _flee = new FleeBehaviour(_movingEntity, enemy);
            _dFlee = intensity;
        }

        public void FleeOff()
        {
            _flee = null;
            _dFlee = 0;
        }

        public void ArriveOn(BaseGameEntity goal, double intensity)
        {
            _arrive = new ArriveBehaviour(_movingEntity, goal);
            _dArrive = intensity;
        }

        public void ArriveOff()
        {
            _arrive = null;
            _dArrive = 0;
        }

        public void DrawBehaviors(Graphics g)
        {
            if (_instanceExists(_arrive))
            {
                _arrive.DrawBehavior(g);
            }
            if (_instanceExists(_flee))
            {
                _flee.DrawBehavior(g);
            }
            if (_instanceExists(_seek))
            {
                _seek.DrawBehavior(g);
            }
            if (_instanceExists(_straightWalking))
            {
                _straightWalking.DrawBehavior(g);
            }
            if (_instanceExists(_wander))
            {
                _wander.DrawBehavior(g);
            }
        }
    }
}