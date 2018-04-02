using TheAnimalKingdom.Entities;

namespace TheAnimalKingdom.Goals.Base
{
    public abstract class Goal
    {
        public bool IsActive => Status == Status.Active;
        public bool IsInactive => Status == Status.Inactive;
        public bool IsCompleted => Status == Status.Completed;
        public bool HasFailed => Status == Status.Failed;

        protected Status Status;
        protected MovingEntity Owner;
        protected readonly GoalType Type;

        public Goal(MovingEntity owner, GoalType type)
        {
            Status = Status.Inactive;
            Owner = owner;
            Type = type;
        }
        
        public abstract void Activate();
        public abstract Status Process();
        public abstract void Terminate();
        public abstract void AddSubgoal(Goal goal);

        protected void ActivateIfInactive()
        {
            if(Status == Status.Inactive) Activate();
        }
    }
}