using System.Drawing;
using TheAnimalKingdom.Behaviours.BaseBehaviours;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours.NormalBehaviours
{
    public class FollowPathBehaviour : SteeringBehaviour
    {
        public FollowPathBehaviour(MovingEntity movingEntity) : base(movingEntity)
        {
        }

        public override Vector2D Calculate()
        {
            throw new System.NotImplementedException();
        }

        public override void DrawBehavior(Graphics g)
        {
            throw new System.NotImplementedException();
        }
    }
}