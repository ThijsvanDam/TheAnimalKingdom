using System;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.Base;

namespace TheAnimalKingdom.Goals.CompositeGoals
{
    public class GoalGatherFood : CompositeGoal
    {        
        public GoalGatherFood(MovingEntity owner) : base(owner, "GatherFood")
        {
        }

        public override void Activate()
        {
            Status = Status.Active;
            
            AddSubgoal(new GoalMoveToItem(Owner, ItemType.Grass));
        }

        public override Status Process()
        {
            ActivateIfInactive();

            return ProcessSubgoals();
        }
    }
}