using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAnimalKingdom.Util
{
   
    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2D() : this(0,0)
        {
        }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Length()
        {
            return Math.Sqrt(LengthSquared());
        }

        public double LengthSquared()
        {
            return (X * X) + (Y * Y);
        }

        public static Vector2D operator +(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X + right.X, left.Y + right.Y);
        }

        public void Add(Vector2D v)
        {
            X += v.X;
            Y += v.Y;
        }
        public void Substract(Vector2D v)
        {
            X -= v.X;
            Y -= v.Y;
        }

        public Vector2D Multiply(double value)
        {
            X *= value;
            Y *= value;
            return this;
        }

        public Vector2D Divide(double value)
        {
            X /= value;
            Y /= value;
            return this;
        }

        public Vector2D Normalize()
        {
            return this.Divide(Length());
        }

        public Vector2D Truncate(double max)
        {
            if (Length() > max)
            {
                Vector2D normalized = Normalize();
                return normalized.Multiply(max);
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
        
        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
