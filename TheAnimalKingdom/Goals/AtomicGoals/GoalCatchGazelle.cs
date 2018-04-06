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
            
            Owner.SteeringBehaviours.SeekOn(_prey, 2.0f);
        }

        public override Status Process()
        {
            ActivateIfInactive();
            Owner.Energy -= 0.05;

            return Status;
        }

        public override void Terminate()
        {
            Owner.SteeringBehaviours.SeekOff();
        }
    }
}