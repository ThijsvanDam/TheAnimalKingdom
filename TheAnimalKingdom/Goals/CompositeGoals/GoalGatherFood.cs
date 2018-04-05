using TheAnimalKingdom.Entities;

namespace TheAnimalKingdom.Goals.CompositeGoals
{
    public class GoalGatherFood : CompositeGoal
    {
        public GoalGatherFood(MovingEntity owner) : base(owner)
        {
        }
    }
}