﻿using System;
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

        public SquaredObstacle(Vector2D position, float size, World world) : base(position, size, world)
        {
            BaseColor =  Color.FromArgb(240, 43, 30, 22);
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
