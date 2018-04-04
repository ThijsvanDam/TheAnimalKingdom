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


        public Matrix(Vector2D v1)
        {
            this.matrix = new double[3, 3] {{v1.X, 0, 0}, {v1.Y, 0, 0}, {v1.Z, 0, 0}};
        }

        public Vector2D ToVector()
        {
            return new Vector2D(this.matrix[0, 0], this.matrix[1, 0]);
        }

        public static Matrix Identity()
        {
            return new Matrix();
        }

        public static Matrix Scale(float s)
        {
            return new Matrix(
                s, 0, 0,
                0, s, 0,
                0, 0, 1
            );
        }

        public static Matrix RotateX(float degrees)
        {
            double rad = ToRad(degrees);

            return new Matrix(
                1, 0, 0,
                0, (float) Math.Cos(rad), (float) -Math.Sin(rad),
                0, (float) Math.Sin(rad), (float) Math.Cos(rad)
            );
        }

        public static Matrix RotateY(float degrees)
        {
            double rad = ToRad(degrees);

            return new Matrix(
                (float) Math.Cos(rad), 0, (float) Math.Sin(rad),
                0, 1, 0,
                (float) -Math.Sin(rad), 0, (float) Math.Cos(rad)
            );
        }

        public static Matrix RotateZ(float degrees)
        {
            double rad = ToRad(degrees);

            return new Matrix(
                (float) Math.Cos(rad), (float) -Math.Sin(rad), 0,
                (float) Math.Sin(rad), (float) Math.Cos(rad), 0,
                0, 0, 1
            );
        }

        public static Matrix Translate(Vector2D t)
        {
            Matrix result = new Matrix();

            result.matrix[0, 3] = t.X;
            result.matrix[1, 3] = t.Y
            result.matrix[2, 3] = t.Z;

            return result;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            Matrix result = new Matrix();

            int row, col;

            for (row = 0; row < 4; row++)
            {
                for (col = 0; col < 4; col++)
                {
                    result.matrix[row, col] = m1.matrix[row, col] + m2.matrix[row, col];
                }
            }

            return result;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            Matrix result = new Matrix();

            int row, col;

            for (row = 0; row < 4; row++)
            {
                for (col = 0; col < 4; col++)
                {
                    result.matrix[row, col] = m1.matrix[row, col] - m2.matrix[row, col];
                }
            }

            return result;
        }

        public static Matrix operator *(Matrix m1, float f)
        {
            Matrix result = new Matrix();

            int row, col;

            for (row = 0; row < 4; row++)
            {
                for (col = 0; col < 4; col++)
                {
                    result.matrix[row, col] = m1.matrix[row, col] * f;
                }
            }

            return result;
        }

        public static Matrix operator *(float f, Matrix m1)
        {
            return m1 * f;
        }

        public static Vector2D operator *(Matrix m1, Vector2D v1)
        {
            return new Vector2D(
                m1.matrix[0, 0] * v1.X + m1.matrix[0, 1] * v1.Y + m1.matrix[0, 2] * v1.Z,
                m1.matrix[1, 0] * v1.X + m1.matrix[1, 1] * v1.Y + m1.matrix[1, 2] * v1.Z
            );
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            Matrix m = new Matrix();
            for (int row = 0; row < 4; row++)
            {
                for (int col_m2 = 0; col_m2 < 4; col_m2++)
                {
                    double x = 0;

                    for (int index = 0; index < 4; index++)
                    {
                        x += m1.matrix[row, index] * m2.matrix[index, col_m2];
                    }

                    m.matrix[row, col_m2] = x;
                }
            }

            return m;
        }

        private static double ToRad(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}