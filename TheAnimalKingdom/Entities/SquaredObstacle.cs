using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public class SquaredObstacle : ObstacleEntity
    {
        public static Color BaseColor;
        public readonly ItemType Type;

        public SquaredObstacle(Vector2D position, float size, World world, ItemType type = ItemType.None) : base(position, size, world)
        {
            if (type == ItemType.Grass) BaseColor = Color.Green;
            if (type == ItemType.Water) BaseColor = Color.Blue;
            if (type == ItemType.None) BaseColor = Color.FromArgb(240, 43, 30, 22);
            Color = BaseColor;
            Type = type;
        }

        public override void Update(float time_elapsed)
        {

        }

        public override void Render(Graphics g)
        {
            double left = VPos.X - Bradius;
            double top = VPos.Y - Bradius;
            double size = Bradius * 2;
            g.FillRectangle(new SolidBrush(Color), (int) left, (int) top, (int) size, (int) size);
        }

        public override void Tag()
        {
            Color = Color.Black;
        }

        public override void RemoveTag()
        {
            Color = BaseColor;
        }
    }
}
