using System.Drawing;
using System.Linq;
using TheAnimalKingdom.Goals.CompositeGoals;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public class Gazelle : Animal
    {   
        public Gazelle(Vector2D position, World world) : base(position, world, false)
        {
            Color = Color.Coral;
            Bradius = 5;
            VVelocity = new Vector2D(0, 0);
            DMass = 10;
            DMaxSpeed = 7;
            DDeceleration = 3;
            DMaxForce = 10;
            
            HashTagLifeGoal = new GoalThink(this);
        }

        public override void Render(Graphics g)
        {
            double left = VPos.X - Bradius;
            double top = VPos.Y - Bradius;
            double size = Bradius * 2;
            g.FillEllipse(new SolidBrush(Color), (int)left, (int)top, (int)size, (int)size);

            base.Render(g);
        }

        public new MovingEntity IsScaredOf()
        {
            foreach (var entity in World.Entities.Where(x => x.GetType() == typeof(MovingEntity)))
            {
                var possibleEnemy = (MovingEntity) entity;
                    if (!possibleEnemy.IsPredator) continue;
                
                    if (Vector2D.DistanceSquared(VPos, entity.VPos) <= 400)
                        return possibleEnemy;
            }

            return null;
        }
    }
}
