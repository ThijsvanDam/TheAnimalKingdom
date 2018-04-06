using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.Base;

namespace TheAnimalKingdom.Goals.AtomicGoals
{
    public class GoalCatchGazelle : AtomicGoal
    {
        private Gazelle _prey;
        
        public GoalCatchGazelle(MovingEntity owner, Gazelle prey) : base(owner, "CatchGazelle")
        {
            _prey = prey;
        }
        
        public override void Activate()
        {
            Status = Status.Active;
            
            Owner.SteeringBehaviours.SeekOn(_prey, 0.5f);
        }

        public override Status Process()
        {
            ActivateIfInactive();
            
            if (Owner.Energy > 0)  Owner.Energy -= 0.08;

            return Status;
        }

        public override void Terminate()
        {
            Owner.SteeringBehaviours.SeekOff();
        }
    }
}