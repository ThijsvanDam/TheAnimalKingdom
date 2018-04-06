using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.Base;

namespace TheAnimalKingdom.Goals.AtomicGoals
{
    public class GoalSleep : AtomicGoal
    {
        public GoalSleep(MovingEntity owner) : base(owner, "Sleep")
        {
        }

        public override void Activate()
        {
            Status = Status.Active;
            Owner.SteeringBehaviours.AllOff();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            Owner.Energy += 0.02;

            return Status.Active;
        }

        public override void Terminate()
        {
            Owner.SteeringBehaviours.WanderOn(1.0f);
        }
    }
}