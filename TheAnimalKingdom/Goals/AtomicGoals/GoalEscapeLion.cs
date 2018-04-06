using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.Base;

namespace TheAnimalKingdom.Goals.AtomicGoals
{
    public class GoalEscapeLion : AtomicGoal
    {
        private MovingEntity _enemy;
        
        public GoalEscapeLion(MovingEntity owner, MovingEntity enemy) : base(owner, "EscapeLion")
        {
            _enemy = enemy;
        }

        public override void Activate()
        {
            Owner.DMaxSpeed += 7f;
            Owner.SteeringBehaviours.FleeOn(_enemy, 0.5f);
        }

        public override Status Process()
        {
            ActivateIfInactive();

            return Status.Active;
        }

        public override void Terminate()
        {
            Owner.SteeringBehaviours.FleeOff();
        }
    }
}