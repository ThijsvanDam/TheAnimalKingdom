using System.Collections.Generic;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.Base;

namespace TheAnimalKingdom.Goals.CompositeGoals
{
    public abstract class CompositeGoal : Goal
    {
        private Stack<Goal> _subgoals;

        public CompositeGoal(MovingEntity owner) : base(owner: owner, type: GoalType.Composite)
        {
            _subgoals = new Stack<Goal>();;
        }
        
        public override void Activate()
        {
            _subgoals.Peek().Activate();
        }

        public override Status Process()
        {
            return ProcessSubgoals();
        }

        public override void Terminate()
        {
            RemoveAllSubgoals();
        }

        public override void AddSubgoal(Goal goal)
        {
            _subgoals.Push(goal);
        }

        public Status ProcessSubgoals()
        {           
            while (_subgoals.Count > 0 && (_subgoals.Peek().IsCompleted || _subgoals.Peek().HasFailed))
            {
                _subgoals.Peek().Terminate();
                _subgoals.Pop();
            }

            if (_subgoals.Count > 0)
            {
                Status status = _subgoals.Peek().Process();

                if (status == Status.Completed && _subgoals.Count > 1)
                {
                    return Status.Active;
                }

                return status;
            }
            
            return Status.Completed;
        }

        public void RemoveAllSubgoals()
        {
            while (_subgoals.Count > 0)
            {
                _subgoals.Peek().Terminate();
                _subgoals.Pop();
            }
            _subgoals.Clear();
        }
    }
}