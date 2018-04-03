﻿using System;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.Base;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Goals.AtomicGoals
{
    public class GoalTraverseEdge : AtomicGoal
    {
        private Vector2D _destination;
        private bool _isFinalEdge;
        
        
        public GoalTraverseEdge(MovingEntity owner, Vector2D destination, bool isFinalEdge) : base(owner)
        {
            _destination = destination;
            _isFinalEdge = isFinalEdge;
        }

        public override void Activate()
        {
            Console.WriteLine("Activate TraverseEdge");
            Status = Status.Active;
        }

        public override Status Process()
        {
            ActivateIfInactive();
            Console.WriteLine("Process TraverseEdge");

            if (Owner.VPos == _destination) Status = Status.Completed;

            return Status;
        }

        public override void Terminate()
        {
            Console.WriteLine("Terminate TraverseEdge");
            Owner.SteeringBehaviours.SeekOff();
            Owner.SteeringBehaviours.ArriveOff();
        }
    }
}