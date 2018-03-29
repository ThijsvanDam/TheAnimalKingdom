using System.Collections.Generic;
using System.Drawing;
using TheAnimalKingdom.Behaviours.BaseBehaviours;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours.NormalBehaviours
{
    public class FollowPathBehaviour : SteeringBehaviour
    {
        private List<NavGraphNode> _route;
        private bool _finished;
        private Vector2D _nextLocation;
        private int _routeIndex;
        
        private const double SeekingDistance = 20.0;
        
        public FollowPathBehaviour(MovingEntity movingEntity, List<NavGraphNode> route) : base(movingEntity)
        {
            _route = route;
            _routeIndex = 0;
            _finished = false;
            _nextLocation = _route[_routeIndex].Position;
        }

        public override Vector2D Calculate()
        {
            if (Vector2D.DistanceSquared(_nextLocation, MovingEntity.VPos) < SeekingDistance)
            {
                if (_routeIndex < _route.Count)
                {
                    _routeIndex++;
                    _nextLocation = _route[_routeIndex].Position;
                }
                else
                {
                    _finished = true;
                }
            }
            
            if (!_finished)
            {
                return Seek();
            }
            else
            {
                return Arrive();
            }
        }
        
        private Vector2D Seek()
        {
            Vector2D currentVelocity = MovingEntity.VVelocity;
            Vector2D desiredVelocity = _nextLocation.Clone().Substract(MovingEntity.VPos).Normalize();
            desiredVelocity.Multiply(MovingEntity.DMaxForce);
            var steering = desiredVelocity.Substract(currentVelocity);
            return steering;
        }

        private Vector2D Arrive()
        {
            double deceleration = MovingEntity.DDeceleration;

            Vector2D toTarget = _nextLocation.Clone().Substract(MovingEntity.VPos);
            Vector2D desiredVelocity;

            double distance = toTarget.Length();

            if (distance > 0.05)
            {
                const double decelerationTweaker = 1;
                double speed = distance / (deceleration * decelerationTweaker);

                speed = speed <= MovingEntity.DMaxSpeed ? speed : MovingEntity.DMaxSpeed;

                desiredVelocity = toTarget.Multiply(speed / distance);
                Vector2D currentVelocity = MovingEntity.VVelocity;
                desiredVelocity.Substract(currentVelocity);
            }
            else
            {
                desiredVelocity = new Vector2D(0, 0);
            }
            return desiredVelocity;
        }

        public override void DrawBehavior(Graphics g)
        {
            throw new System.NotImplementedException();
        }
    }
}