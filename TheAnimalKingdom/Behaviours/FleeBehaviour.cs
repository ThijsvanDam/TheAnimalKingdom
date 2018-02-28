using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours
{
    public class FleeBehaviour : SteeringBehaviour
    {
        public BaseGameEntity Enemy;

        public FleeBehaviour(MovingEntity movingEntity, BaseGameEntity enemy) : base(movingEntity)
        {
            Enemy = enemy;
        }

        public override Vector2D Calculate()
        {
            return new Vector2D(Enemy.VPos.X + MovingEntity.VVelocity.X, Enemy.VPos.Y + MovingEntity.VVelocity.Y);
        }

        public override void DrawBehavior(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}
