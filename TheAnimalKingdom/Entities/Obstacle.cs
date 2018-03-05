using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public class Obstacle : BaseGameEntity
    {
        public Obstacle(Vector2D position, float size, World world) : base(position, world)
        {
            Color = Color.DarkRed;
            Scale = (size * 25)/ 2;

            world.Obstacles.Add(this);
        }

        public override void Update(float time_elapsed)
        {

        }

        public override void Render(Graphics g)
        {
            double left = VPos.X - Scale;
            double top = VPos.Y - Scale;
            double size = Scale * 2;
            g.FillRectangle(new SolidBrush(Color), (int) left, (int) top, (int) size, (int) size);
        }
    }
}
