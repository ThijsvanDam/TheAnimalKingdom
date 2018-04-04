using System;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.Base;

namespace TheAnimalKingdom.Goals.AtomicGoals
{
    public class GoalWander : AtomicGoal
    {
        public GoalWander(MovingEntity owner) : base(owner)
        {
        }

        public override void Activate()
        {
            Status = Status.Active;
            Owner.SteeringBehaviours.WanderOn(1.0);
        }

        public override Status Process()
        {
            ActivateIfInactive();
            return Status;
        }

        public override void Terminate()
        {
            Owner.SteeringBehaviours.WanderOff();
        }
    }
}