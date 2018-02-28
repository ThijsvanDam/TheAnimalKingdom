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
        
        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
