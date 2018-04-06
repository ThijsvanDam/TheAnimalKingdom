using System;
using System.Drawing;
using TheAnimalKingdom.Properties;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Entities
{
    public class SquaredObstacle : ObstacleEntity
    {
        public Color BaseColor;
        public Image Image;

        public SquaredObstacle(Vector2D position, float size, World world, ItemType type = ItemType.Block) : base(position, size, world)
        {
            if (type == ItemType.Grass)
            {
                BaseColor = Color.Green;
                Image = Resources.grass;
            }

            if (type == ItemType.Water)
            {
                BaseColor = Color.Blue;
                Image = Resources.water;
            }

            if (type == ItemType.Block)
            {
                BaseColor = Color.FromArgb(240, 43, 30, 22);
                Image = null;
            }
            Type = type;
            Color = BaseColor;
        }

        public override void Update(float time_elapsed)
        {

        }

        public override void Render(Graphics g)
        {
            double left = VPos.X - Bradius;
            double top = VPos.Y - Bradius;
            double size = Bradius * 2;
            if (Image == null)
            {
                g.FillRectangle(new SolidBrush(Color), (int) left, (int) top, (int) size, (int) size);
            }
            else
            {
                g.DrawImage(Image, (int)left, (int)top, (int)size, (int)size);
            }
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
