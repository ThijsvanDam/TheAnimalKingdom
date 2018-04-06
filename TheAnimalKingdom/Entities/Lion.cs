using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TheAnimalKingdom.Goals.CompositeGoals;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public class Lion : Animal
    {
        public Vector2D HomePosition { get; }
        
        public Lion(Vector2D position, World world) : base(position, world)
        {
            HomePosition = position.Clone();
            Color = Color.Orange;
            Bradius = 5;
            VVelocity = new Vector2D(0, 0);
            DMass = 15;
            DMaxSpeed = 5;
            DDeceleration = 3;
            DMaxForce = 10;
            
            HashTagLifeGoal = new GoalThinkLion(this);
        }
        
        public override void Update(float time_elapsed)
        {
            if (IsCloseToFood())
            {
                Hunger -= 0.5 * time_elapsed;
                if (Hunger < 0) Hunger = 0;

            }
            else
            {
                Hunger += 0.2 * time_elapsed;
                if (Hunger > 150) Hunger = 150;
            }
            
            base.Update(time_elapsed);
        }
        
        public bool IsCloseToFood()
        {
            foreach (var entity in World.Entities.Where(x => x.GetType() == typeof(Gazelle)))
            {
                if (Vector2D.DistanceSquared(VPos, entity.VPos) <= 2000)
                    return true;
            }

            return false;
        }

        public override void Render(Graphics g)
        {
            double left = VPos.X - Bradius;
            double top = VPos.Y - Bradius;
            double size = Bradius * 3;
            g.FillEllipse(new SolidBrush(Color), (int)left, (int)top, (int)size, (int)size);
            g.DrawString("L", new Font(new FontFamily("Times New Roman"), 10f), new SolidBrush(Color.Black), (float)left, (float)top);

            base.Render(g);
        }

        public override double DistanceToClosestEnemy()
        {
            double nearestDistance = double.MaxValue;
            foreach (var entity in World.Entities.Where(x => x.GetType() == typeof(Gazelle)))
            {

                var dist = Vector2D.DistanceSquared(VPos, entity.VPos);
                if (dist < nearestDistance)
                    nearestDistance = dist;
            }
            return nearestDistance;
        }

        public Gazelle SeekPrey()
        {
            double distanceClosestGazelle = double.MaxValue;
            Gazelle closestGazelle = null;
            
            foreach (var entity in World.Entities.Where(x => x.GetType() == typeof(Gazelle)))
            {
                var dist = Vector2D.DistanceSquared(VPos, entity.VPos);
                if (dist <= distanceClosestGazelle)
                {
                    distanceClosestGazelle = dist;
                    closestGazelle = (Gazelle) entity;
                }
            }
            return closestGazelle;
        }
    }
}