﻿using System;
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
        public double W { get; set; }

        public Vector2D() : this(0,0)
        {
        }

        public Vector2D(double x, double y, double w = 1)
        {
            X = x;
            Y = y;
            W = w;
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
        public Vector2D DotMultiplication(Vector2D value)
        {
            X *= value.X;
            Y *= value.Y;
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
        

        public static Vector2D operator +(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X + right.X, left.Y + right.Y, 1);
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

        public Vector2D Scale(float xFactor, float yFactor)
        {
            Matrix scaleMatrix = Matrix.Scale(xFactor);
            return scaleMatrix * new Vector2D(X, Y, 1);
        }

        public Vector2D Rotate(double degrees)
        {
            Matrix rotationMatrix = Matrix.Rotate(degrees);
            return rotationMatrix * new Vector2D(X, Y, 1);
        }

        public Vector2D Translate(Vector2D v)
        {
            Matrix translationMatrix = Matrix.Translate(v);
            return translationMatrix * new Vector2D(X, Y, 1);
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
    }
}
