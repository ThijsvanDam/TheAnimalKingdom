using System.Drawing;
using System.Windows.Forms.VisualStyles;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

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
        protected readonly string Name;

        public Goal(MovingEntity owner, GoalType type, string name)
        {
            Status = Status.Inactive;
            Owner = owner;
            Type = type;
            Name = name;
        }
        
        public abstract void Activate();
        public abstract Status Process();
        public abstract void Terminate();
        public abstract void AddSubgoal(Goal goal);

        protected void ActivateIfInactive()
        {
            if(Status == Status.Inactive) Activate();
        }

        public virtual void DrawGoal(Graphics g, Vector2D position = null)
        {
            if (position == null) position = Owner.VPos;
            
            g.DrawString(Name, new Font(new FontFamily("Times New Roman"), 10f), new SolidBrush(Color.DarkBlue), (float)position.X, (float)position.Y + 5f);
        }
    }
}