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
            var me = (Lion) Owner;
                        
            if (Owner.Energy <= 1 && !_sleeping)
            {
                if (_goingToSleep && Vector2D.DistanceSquared(me.HomePosition, Owner.VPos) < 500)
                {
                    _sleeping = true;
                    _goingToSleep = false;
                    RemoveAllSubgoals();
                    AddSubgoal(new GoalSleep(Owner));
                }
                else if(!_goingToSleep)
                {
                    _goingToSleep = true;
                    RemoveAllSubgoals();
                    AddSubgoal(new GoalMoveToPosition(Owner, me.HomePosition));
                } 
            } 
            else if (Owner.Hunger >= 50 && !_sleeping)
            {
                var prey = me.SeekPrey();
                RemoveAllSubgoals();
                AddSubgoal(new GoalCatchGazelle(Owner, prey));
            }
            else if (Owner.Energy >= 99 && _sleeping)
            {
                RemoveAllSubgoals();
                _sleeping = false;
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