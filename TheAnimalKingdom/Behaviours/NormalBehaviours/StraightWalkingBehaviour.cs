using System.Drawing;
using TheAnimalKingdom.Behaviours.BaseBehaviours;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours.NormalBehaviours
{
    public class StraightWalkingBehaviour : SteeringBehaviour
    {
        public StraightWalkingBehaviour(MovingEntity movingEntity) : base(movingEntity)
        {
        }

        public override Vector2D Calculate()
        {
            return new Vector2D(1 + World.Intensity, 0);
        }

        public override void DrawBehavior(Graphics g)
        {
            throw new System.NotImplementedException();
        }
    }
}