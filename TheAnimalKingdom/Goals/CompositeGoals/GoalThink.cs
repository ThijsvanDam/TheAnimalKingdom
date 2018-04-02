using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.AtomicGoals;
using TheAnimalKingdom.Goals.Base;

namespace TheAnimalKingdom.Goals.CompositeGoals
{
    public class GoalThink: CompositeGoal
    {
        public GoalThink(MovingEntity owner) : base(owner: owner)
        {
        }
        
        public override void Activate()
        {
            throw new System.NotImplementedException();
        }

        public override Status Process()
        {
            throw new System.NotImplementedException();
        }

        public override void Terminate()
        {
            throw new System.NotImplementedException();
        }
    }
}