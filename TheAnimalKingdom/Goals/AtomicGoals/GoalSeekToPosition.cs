using System;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.Base;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Goals.AtomicGoals
{
    public class GoalSeekToPosition : AtomicGoal
    {
        private Vector2D _destination;
        
        public GoalSeekToPosition(MovingEntity owner, Vector2D position) : base(owner)
        {
            _destination = position;
        }

        public override void Activate()
        {
            Owner.SteeringBehaviours.SeekOn(_destination, 0.7f);
            Status = Status.Active;
        }

        public override Status Process()
        {
            ActivateIfInactive();
                        
            return Status;
        }

        public override void Terminate()
        {
            Owner.SteeringBehaviours.SeekOff();
        }
    }
}