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
        
        public GoalMoveToPosition(MovingEntity owner, Vector2D destination) : base(owner)
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
            Console.WriteLine("Process MoveToPosition");

            ActivateIfInactive();

            if (Owner.FindPathResult == PathResult.Found && !_routeFound)
            {
                Owner.Route = Owner.PathPlanner.Route;
                _routeFound = true;
                RemoveAllSubgoals();
                AddSubgoal(new GoalFollowPath(Owner, Owner.Route));
            }

            if (Owner.FindPathResult == PathResult.NotFound)
            {
                return Status.Failed;
            }
            
            var status = ProcessSubgoals();

            return status;
        }

        public override void Terminate()
        {
            Console.WriteLine("Terminate MoveToPosition");
            base.Terminate();
        }
    }
}