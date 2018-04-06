using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.AtomicGoals;
using TheAnimalKingdom.Goals.Base;

namespace TheAnimalKingdom.Goals.CompositeGoals
{
    public class GoalThink: CompositeGoal
    {
        private bool _sleeping;
        private bool _fleeing;
        
        public GoalThink(MovingEntity owner) : base(owner: owner, name: "Think")
        {
            _sleeping = false;
            _fleeing = false;
        }
        
        public override void Activate()
        {
            Status = Status.Active;
            Owner.SteeringBehaviours.ObstacleAvoidanceOn(1.0f);
            AddSubgoal(new GoalWander(Owner));
        }

        public override Status Process()
        {
            ActivateIfInactive();
            
            if (Owner.Energy == 0 && !_sleeping)
            {
                _sleeping = true;
                RemoveAllSubgoals();
                AddSubgoal(new GoalSleep(Owner));
            } 
            else if (Owner.Energy == 10 && _sleeping)
            {
                _sleeping = false;
                RemoveAllSubgoals();
            }
            else if (Owner.Hunger == 10)
            {
                RemoveAllSubgoals();
                AddSubgoal(new GoalGatherFood(Owner));
            }

            var enemy = Owner.IsScaredOf();
            
            if (enemy != null && !_fleeing)
            {
                _fleeing = true;
                RemoveAllSubgoals();
                AddSubgoal(new GoalEscapeLion(Owner, enemy));
            } 
            else if (enemy == null && _fleeing)
            {
                RemoveAllSubgoals();
                _fleeing = false;
            }

            if (_subgoals.Count == 0)
            {
                AddSubgoal(new GoalWander(Owner));
            }
            
            var status = ProcessSubgoals();
            
            return status;
        }
    }
}