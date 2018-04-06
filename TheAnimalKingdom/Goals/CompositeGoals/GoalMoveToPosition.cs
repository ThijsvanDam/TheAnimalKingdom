using System;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.AtomicGoals;
using TheAnimalKingdom.Goals.Base;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Goals.CompositeGoals
{
    public class GoalMoveToPosition : CompositeGoal
    {
        private Vector2D _destination;
        private bool _routeFound;
        
        public GoalMoveToPosition(MovingEntity owner, Vector2D destination) : base(owner, "MoveToPosition")
        {
            _destination = destination;
            _routeFound = false;
        }
        
        public override void Activate()
        {
            Status = Status.Active;
            
            RemoveAllSubgoals();
            
            Owner.PathPlanner.RequestPathToTarget(_destination);

            AddSubgoal(new GoalSeekToPosition(Owner, _destination));
        }
        
        public override Status Process()
        {
            ActivateIfInactive();

            if (Owner.FindPathResult == PathResult.Found && !_routeFound)
            {
                _routeFound = true;
                Owner.Route = Owner.PathPlanner.Route;
                AddSubgoal(new GoalFollowPath(Owner, Owner.Route));
            }

            if (Owner.FindPathResult == PathResult.NotFound)
            {
                return Status.Failed;
            }
            
            var status = ProcessSubgoals();

            return status;
        }
    }
}