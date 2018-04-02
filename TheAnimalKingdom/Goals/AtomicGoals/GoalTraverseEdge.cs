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
            throw new System.NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            if (Owner.VPos == _destination) Status = Status.Completed;

            return Status;
        }

        public override void Terminate()
        {
            Owner.SteeringBehaviours.SeekOff();
            Owner.SteeringBehaviours.ArriveOff();
        }
    }
}