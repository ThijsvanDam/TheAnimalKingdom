using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.AtomicGoals;

namespace TheAnimalKingdom.Goals.CompositeGoals
{
    public class GoalThink: CompositeGoal
    {
        public GoalThink(MovingEntity owner) : base(owner: owner)
        {
            AddSubgoal(new GoalWander(Owner));
        }
        
// ToDo: Implement this
//        public override void Activate()
//        {
//            throw new System.NotImplementedException();
//        }
//
//        public override Status Process()
//        {
//            throw new System.NotImplementedException();
//        }
//
//        public override void Terminate()
//        {
//            throw new System.NotImplementedException();
//        }
    }
}