using System.Collections.Generic;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.AtomicGoals;
using TheAnimalKingdom.Goals.Base;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Goals.CompositeGoals
{
    public class GoalFollowPath : CompositeGoal
    {
        private Stack<NavGraphNode> _path;
        
        public GoalFollowPath(MovingEntity owner, Stack<NavGraphNode> path) : base(owner: owner)
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
    }
}