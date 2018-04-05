using System;

namespace TheAnimalKingdom.Util
{
    public class Matrix
    {
        public double[,] matrix { get; set; }

        public Matrix()
        {
            this.matrix = new double[3, 3]
            {
                {1, 0, 0},
                {0, 1, 0},
                {0, 0, 1}
            };
        }

        public Matrix(
            double a1, double b1,
            double a2, double b2)
            : this(
                a1, b1, 0,
                a2, b2, 0,
                0, 0, 0
            )
        {
        }

        public Matrix(
            double a1, double b1, double c1,
            double a2, double b2, double c2,
            double a3, double b3, double c3)
        {
            this.matrix = new double[3, 3]
            {
                {a1, b1, c1},
                {a2, b2, c2},
                {a3, b3, c3}
            };
        }

        public Vector2D ToVector()
        {
            return new Vector2D(this.matrix[0, 0], this.matrix[1, 0]);
        }

        public static Matrix Scale(float s)
        {
            return new Matrix(
                s, 0, 0,
                0, s, 0,
                0, 0, 1
            );
        }

        public static Matrix Rotate(double rad)
        {
            return new Matrix(
                Math.Cos(rad), -Math.Sin(rad), 0,
                Math.Sin(rad), Math.Cos(rad), 0,
                0, 0, 1
            );
        }

        public static Matrix Translate(Vector2D t)
        {
            Matrix result = new Matrix();

            result.matrix[0, 3] = t.X;
            result.matrix[1, 3] = t.Y;
            result.matrix[2, 3] = t.W;

            return result;
        }

        public static Vector2D operator *(Matrix m1, Vector2D v1)
        {
            return new Vector2D(
                m1.matrix[0, 0] * v1.X + m1.matrix[0, 1] * v1.Y + m1.matrix[0, 2] * v1.W,
                m1.matrix[1, 0] * v1.X + m1.matrix[1, 1] * v1.Y + m1.matrix[1, 2] * v1.W
            );
        }
    }
}