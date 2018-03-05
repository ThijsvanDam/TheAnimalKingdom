using System.Collections.Generic;
using System.Drawing;
using TheAnimalKingdom.Behaviours.BaseBehaviours;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours.AdvancedBehaviours
{
    public class ObstacleAvoidance : SteeringBehaviour
    {
        public ObstacleAvoidance(MovingEntity movingEntity) : base(movingEntity)
        {
        }

        public override Vector2D Calculate()
        {
            List<Obstacle> obstacles = MovingEntity.World.Obstacles;





            return new Vector2D(0, 0);
        }

        public override void DrawBehavior(Graphics g)
        {
//            throw new System.NotImplementedException();
        }
    }
}