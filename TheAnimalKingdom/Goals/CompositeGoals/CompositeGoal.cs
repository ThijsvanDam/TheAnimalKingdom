using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.Base;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Goals.CompositeGoals
{
    public abstract class CompositeGoal : Goal
    {
        protected Stack<Goal> _subgoals;

        public CompositeGoal(MovingEntity owner, string name) : base(owner: owner, type: GoalType.Composite, name: name)
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
                Status statusOfSubgoals = _subgoals.Peek().Process();

                if (statusOfSubgoals == Status.Completed && _subgoals.Count > 1)
                {
                    return Status.Active;
                }

                return statusOfSubgoals;
            }
            else
            {
                return Status.Completed;

            }
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

        public override void DrawGoal(Graphics g, Vector2D position = null)
        {
            if (position == null) position = Owner.VPos;

            base.DrawGoal(g, position);

            double depth = 10;

            foreach (var goal in _subgoals)
            {
                goal.DrawGoal(g, new Vector2D(position.X + 5, position.Y + depth));
                depth += 5;
            }
        }
    }
}