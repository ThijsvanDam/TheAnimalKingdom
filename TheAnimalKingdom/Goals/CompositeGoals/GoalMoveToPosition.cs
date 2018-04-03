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
            Console.WriteLine("Activate MoveToPosition");
            Status = Status.Active;
            
            RemoveAllSubgoals();
            
            Owner.PathPlanner.RequestPathToTarget(_destination);

            if (Owner.FindPathResult == PathResult.NotFound)
            {
                AddSubgoal(new GoalSeekToPosition(Owner, _destination));
            }
        }
        
        public override Status Process()
        {
            ActivateIfInactive();
            Console.WriteLine("Process MoveToPosition");

            if (Owner.FindPathResult == PathResult.Found && !_routeFound)
            {
                RemoveAllSubgoals();
                AddSubgoal(new GoalFollowPath(Owner, Owner.PathPlanner.GetRoute()));
                _routeFound = true;
            }

            if (Owner.FindPathResult == PathResult.NotFound)
            {
                RemoveAllSubgoals();
                return Status.Failed;
            }
            
            var status = ProcessSubgoals();

            return status;
        }
    }
}