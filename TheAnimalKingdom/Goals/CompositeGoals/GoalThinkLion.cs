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
        private bool _goingToSleep;
        
        public GoalThinkLion(MovingEntity owner) : base(owner: owner, name: "Think")
        {
            _sleeping = false;
            _fleeing = false;
            _goingToSleep = false;
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
                var lair = new Vector2D(50f, 50f);
                if (_goingToSleep && Vector2D.DistanceSquared(lair, Owner.VPos) < 500)
                {
                    _sleeping = true;
                    RemoveAllSubgoals();
                    AddSubgoal(new GoalSleep(Owner));
                }
                else if(!_goingToSleep)
                {
                    _goingToSleep = true;
                    RemoveAllSubgoals();
                    var lion = (Lion) Owner;
                    AddSubgoal(new GoalMoveToPosition(Owner, lion.HomePosition));
                }
                
            } 
            else if (Owner.Energy >= 99 && _sleeping)
            {
                _sleeping = false;
            }
            else if (Owner.Hunger >= 50 && !_sleeping)
            {
                var me = (Lion) Owner;
                var prey = me.SeekPrey();
                RemoveAllSubgoals();
                AddSubgoal(new GoalCatchGazelle(Owner, prey));
            }

            if (_subgoals.Count == 0)
            {
                RemoveAllSubgoals();
                AddSubgoal(new GoalWander(Owner));
            }
            
            var status = ProcessSubgoals();
            
            return status;
        }
    }
}