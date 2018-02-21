using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours
{
    public class StraightWalkingBehaviour : SteeringBehaviour
    {
        public StraightWalkingBehaviour(MovingEntity movingEntity) : base(movingEntity)
        {
        }

        public override Vector2D Calculate()
        {
            return new Vector2D(1, 0);
        }
    }
}