using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.AtomicGoals;
using TheAnimalKingdom.Goals.Base;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Goals.CompositeGoals
{
    public class GoalFollowPath : CompositeGoal
    {
        private Stack<NavGraphNode> _path;
        
        public GoalFollowPath(MovingEntity owner, Stack<NavGraphNode> path) : base(owner: owner, name: "FollowPath")
        {
            _path = path;
        }
        
        public override void Activate()
        {
            Status = Status.Active;

            NavGraphNode edge = _path.Pop();
            
            AddSubgoal(new GoalTraverseEdge(owner: Owner, destination: edge.Position, isFinalEdge: _path.Count == 0));
        }

        public override Status Process()
        {
            ActivateIfInactive();

            Status = ProcessSubgoals();

            if (Status == Status.Completed && _path.Count > 0)
            {
                Activate();
            }
            
            return Status;
        }

        public override void DrawGoal(Graphics g, Vector2D position = null)
        {
            if (_path != null)
            {
                Stack<NavGraphNode> copiedRoute = new Stack<NavGraphNode>(_path.Reverse());
        
                while (copiedRoute.Count > 1)
                {
                    var nodeFrom = copiedRoute.Pop();
                    var nodeTo = copiedRoute.Peek();
                    g.DrawLine(new Pen(Color.Yellow), nodeFrom.Position.ToPoint(), nodeTo.Position.ToPoint());
                }
            }
            
            base.DrawGoal(g, position);
        }
    }
}