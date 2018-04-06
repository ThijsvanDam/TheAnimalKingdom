using System;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.AtomicGoals;
using TheAnimalKingdom.Goals.Base;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Goals.CompositeGoals
{
    public class GoalThinkLion: CompositeGoal
    {
        private bool _sleeping;
        private bool _fleeing;
        
        public GoalThinkLion(MovingEntity owner) : base(owner: owner, name: "Think")
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
                        
            if (Owner.Energy <= 1 && !_sleeping)
            {
                _sleeping = true;
                RemoveAllSubgoals();
                AddSubgoal(new GoalSleep(Owner));
            } 
            else if (Owner.Energy >= 99 && _sleeping)
            {
                _sleeping = false;
                RemoveAllSubgoals();
            }
            else if (Owner.Hunger >= 50 && !_sleeping)
            {
                RemoveAllSubgoals();
                var me = (Lion) Owner;
                var prey = me.SeekPrey();
                AddSubgoal(new GoalCatchGazelle(Owner, prey));
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