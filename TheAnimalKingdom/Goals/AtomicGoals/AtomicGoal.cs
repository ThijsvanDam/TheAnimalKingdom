using System;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Goals.Base;

namespace TheAnimalKingdom.Goals.AtomicGoals
{
    public abstract class AtomicGoal : Goal
    {
        public AtomicGoal(MovingEntity owner, string name) : base(owner: owner, type: GoalType.Atomic, name: name)
        {
        }

        public override void AddSubgoal(Goal goal)
        {
            throw new Exception("Method not allowed for atomic goal");
        }
    }
}