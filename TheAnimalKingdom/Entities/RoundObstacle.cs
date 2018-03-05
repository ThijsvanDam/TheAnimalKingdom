using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    class RoundObstacle : ObstacleEntity
    {
        public RoundObstacle(Vector2D position, float size, World world) : base(position, size, world)
        {
            Color = Color.DarkBlue;
        }

        public override void Update(float time_elapsed)
        {

        }

        public override void Render(Graphics g)
        {
            double left = VPos.X - Bradius;
            double top = VPos.Y - Bradius;
            double size = Bradius * 2;
            g.FillEllipse(new SolidBrush(Color), (int)left, (int)top, (int)size, (int)size);
        }
    }
}
