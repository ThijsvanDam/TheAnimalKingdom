using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAnimalKingdom.Util
{
   
    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector2D() : this(0,0)
        {
        }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
            Z = 1;
        }

        public double Length()
        {
            return Math.Sqrt(LengthSquared());
        }

        public double LengthSquared()
        {
            return (X * X) + (Y * Y);
        }

        public Vector2D Add(Vector2D v)
        {
            X += v.X;
            Y += v.Y;

            return this;
        }
        public Vector2D Substract(Vector2D v)
        {
            X -= v.X;
            Y -= v.Y;

            return this;
        }

        public Vector2D Multiply(double value)
        {
            X *= value;
            Y *= value;
            return this;
        }
        public double DotMultiplication(Vector2D v2)
        {
            return X*v2.X + Y*v2.Y;
        }

        public Vector2D Divide(double value)
        {
            X /= value;
            Y /= value;
            return this;
        }

        public Vector2D Normalize()
        {
            Vector2D rtrn;
            if (Length() > 0)
            {
                rtrn = Divide(Length());
            }
            else
            {
                X = 1;
                Y = 1;
                rtrn = this;
            }
            return rtrn;
        }

        public Vector2D Truncate(double max)
        {
            if (Length() > max)
            {
                Normalize();
                Multiply(max);
            }
            return this;
        }

        public Vector2D Perpendicular()
        {
            return new Vector2D(-Y, X);
        }
        
        public Vector2D Clone()
        {
            return new Vector2D(this.X, this.Y);
        }

        public static double DistanceSquared(Vector2D v1, Vector2D v2)
        {
            // d(v1, v2) = ||v1 - v2|| = Sqrt((v1.X - v2.X)^2 + (v1.Y - v2.Y)^2)
            return Math.Pow(v1.X - v2.X, 2.0) + Math.Pow(v1.Y - v2.Y, 2.0);
        }

        public static double Direction(Vector2D from, Vector2D to)
        {
            if (to.X > from.X)
            {
                return Math.Atan((from.Y - to.Y) / (from.X - to.X));
            }
            return Math.Atan((from.Y - to.Y) / (from.X - to.X)) + Math.PI;
        }
        
        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public Point ToPoint()
        {
            return new Point((int)this.X, (int)this.Y);
        }
        public PointF ToPointF()
        {
            return new PointF((float)this.X, (float)this.Y);
        }
    }
}
