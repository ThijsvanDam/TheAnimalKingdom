using System;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.Base;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Goals.AtomicGoals
{
    public class GoalTraverseEdge : AtomicGoal
    {
        private Vector2D _destination;
        private bool _isFinalEdge;
        
        
        public GoalTraverseEdge(MovingEntity owner, Vector2D destination, bool isFinalEdge) : base(owner)
        {
            _destination = destination;
            _isFinalEdge = isFinalEdge;
        }

        public override void Activate()
        {
            if (_isFinalEdge)
            {
                Owner.SteeringBehaviours.ArriveOn(_destination, 1.0);
            }
            else
            {
                Owner.SteeringBehaviours.SeekOn(_destination, 1.0);
            }
            Status = Status.Active;
        }

        public override Status Process()
        {
            ActivateIfInactive();

            if (Owner.IsAtPosition(_destination)) Status = Status.Completed;

            return Status;
        }

        public override void Terminate()
        {
            Owner.SteeringBehaviours.SeekOff();
            Owner.SteeringBehaviours.ArriveOff();
        }
    }
}