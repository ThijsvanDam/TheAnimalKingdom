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
            Status = Status.Active;
            Owner.SteeringBehaviours.ObstacleAvoidanceOn(5.0f);
            AddSubgoal(new GoalWander(Owner));
        }

        public override Status Process()
        {
            ActivateIfInactive();
            
            var status = ProcessSubgoals();

            if (_subgoals.Count == 0)
            {
                AddSubgoal(new GoalWander(Owner));
            }
            
            return status;
        }
    }
}