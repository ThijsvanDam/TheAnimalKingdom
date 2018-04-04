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
            Console.WriteLine("Activate SeekToPosition");
            Owner.SteeringBehaviours.SeekOn(_destination ,1.0);
            Status = Status.Active;
            return;
        }

        public override Status Process()
        {
            ActivateIfInactive();
            Console.WriteLine("Process SeekToPosition");
            return Status.Completed;
        }

        public override void Terminate()
        {
            Owner.SteeringBehaviours.SeekOff();
            Console.WriteLine("Terminate SeekToPosition");
            return;
        }
    }
}