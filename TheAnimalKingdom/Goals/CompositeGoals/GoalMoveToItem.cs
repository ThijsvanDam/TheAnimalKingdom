﻿using System;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.AtomicGoals;
using TheAnimalKingdom.Goals.Base;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Goals.CompositeGoals
{
    public class GoalMoveToItem: CompositeGoal
    {
        private ItemType _item;
        private Vector2D _destination;
        private bool _routeFound;
        
        public GoalMoveToItem(MovingEntity owner, ItemType item) : base(owner, "MoveToItem")
        {
            _item = item;
            _routeFound = false;
        }
        
        public override void Activate()
        {
            Status = Status.Active;
            
            RemoveAllSubgoals();
            
            Owner.PathPlanner.RequestPathToItem(_item);
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

        public override void Terminate()
        {
            Owner.PathPlanner.AbortRequest();
            base.Terminate();
        }
    }
}