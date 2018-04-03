﻿using System;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.Base;

namespace TheAnimalKingdom.Goals.AtomicGoals
{
    public class GoalWander : AtomicGoal
    {
        public GoalWander(MovingEntity owner) : base(owner)
        {
        }

        public override void Activate()
        {
            Console.WriteLine("Activate Wander");
            Status = Status.Active;
            Owner.SteeringBehaviours.WanderOn(1.0);
        }

        public override Status Process()
        {
            Console.WriteLine("Processing Wander");
            ActivateIfInactive();
            return Status;
        }

        public override void Terminate()
        {
            Console.WriteLine("Stopping Wander");
            Owner.SteeringBehaviours.WanderOff();
        }
    }
}