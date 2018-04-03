using System;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.Base;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Goals.AtomicGoals
{
    public class GoalSeekToPosition : AtomicGoal
    {
        public GoalSeekToPosition(MovingEntity owner, Vector2D position) : base(owner)
        {
        }

        public override void Activate()
        {
            Console.WriteLine("Activate SeekToPosition");
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
            Console.WriteLine("Terminate SeekToPosition");
            return;
        }
    }
}