using System;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.AtomicGoals;
using TheAnimalKingdom.Goals.Base;

namespace TheAnimalKingdom.Goals.CompositeGoals
{
    public class GoalThinkGazelle: CompositeGoal
    {
        private bool _sleeping;
        private bool _fleeing;
        private bool _seekingFood;
        
        public GoalThinkGazelle(MovingEntity owner) : base(owner: owner, name: "Think")
        {
            _sleeping = false;
            _fleeing = false;
            _seekingFood = false;
        }
        
        public override void Activate()
        {
            Status = Status.Active;
            Owner.SteeringBehaviours.ObstacleAvoidanceOn(1.0f);
            AddSubgoal(new GoalWander(Owner));
        }

        public override Status Process()
        {
            var enemy = Owner.IsScaredOf();
            
            ActivateIfInactive();
                        
            if (Owner.Hunger >= 3 && !_fleeing && !_seekingFood)
            {
                _seekingFood = true;
                RemoveAllSubgoals();
                AddSubgoal(new GoalGatherFood(Owner));
            }

            if (Owner.Hunger <= 0 && _seekingFood)
            {
                _seekingFood = false;
                RemoveAllSubgoals();
            }
            
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