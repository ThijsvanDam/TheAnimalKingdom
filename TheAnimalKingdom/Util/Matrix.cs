using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TheAnimalKingdom.Util
{
    public class Matrix
    {
        public double[,] Elements;
        public int Columns => Elements.GetLength(0);
        public int Rows => Elements.GetLength(1);

        public Matrix()
        {
            this.Elements = Identity().Elements;
        }

        public Matrix(double[,] elements)
        {
            if (elements.GetLength(0) != elements.GetLength(1))
            {
                Console.WriteLine(@"Let op! Je maakt een niet-vierkante matrix aan!");
            }

            Elements = elements;
        }

        private static double ToRad(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        public static Matrix Translate(Vector2D t)
        {
            return new Matrix(new double[,]
            {
                {1, 0, t.X},
                {0, 1, t.Y},
                {0, 0, 1  }
            });
        }
        public static Matrix Scale(double s)
        {
            return new Matrix(new double[,]
            {
                {s, 0, 0},
                {0, s, 0},
                {0, 0, 1}
            });
        }

        public static Matrix Rotate(double degrees)
        {
            double rad = ToRad(degrees);

            return new Matrix(new double[,]
            {
                { Math.Cos(rad), -Math.Sin(rad), 0},
                { Math.Sin(rad), Math.Cos(rad) , 0},
                {0             ,0              , 1},
            });
        }

        public static Matrix RotateRad(double rad)
        {
            return new Matrix(new double[,]
            {
                { Math.Cos(rad), -Math.Sin(rad), 0},
                { Math.Sin(rad), Math.Cos(rad) , 0},
                {0             ,0              , 1},
            });
        }
        public Matrix(Vector2D vector)
        {
            // Weet ik niet zeker. Op welke matrix-spots komen de vectorwaarden?

            double[,] temp = new double[4, 4];

            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(0); j++)
                {
                    double setValue;
                    if (i == 0 && j == 0)
                    {
                        setValue = vector.X;
                    }
                    else if (i == 1 && j == 1)
                    {
                        setValue = vector.Y;
                    }
                    else
                    {
                        setValue = 0;
                    }

                    temp[i, j] = setValue;
                }
            }

            Elements = temp;
        }

        public Vector2D ToVector()
        {
            // Weet ik niet zeker, op welke matrixspots staan de vectorelementen?
            return new Vector2D(Elements[0, 0], Elements[1, 1], 1);
        }

        public static Matrix Identity()
        {
            return Identity(4);
        }

        public static Matrix Identity(int s)
        {
            double[,] values = new double[s, s];
            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < s; j++)
                {
                    double setValue;
                    if (i == j)
                    {
                        setValue = 1;
                    }
                    else
                    {
                        setValue = 0;
                    }

                    values[i, j] = setValue;
                }
            }

            return new Matrix(values);
        }

        public static Matrix operator +(Matrix left, Matrix right)
        {
            double[,] combined = new double[left.Columns, left.Rows];
            if (CheckEqualSize(left, right))
            {
                for (int i = 0; i < left.Rows; i++)
                {
                    for (int j = 0; j < right.Columns; j++)
                    {
                        combined[j, i] = left.Elements[j, i] + right.Elements[j, i];
                    }
                }
            }

            return new Matrix(combined);
        }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            double[,] combined = new double[left.Columns, left.Rows];
            if (CheckEqualSize(left, right))
            {
                for (int i = 0; i < left.Rows; i++)
                {
                    for (int j = 0; j < right.Columns; j++)
                    {
                        combined[j, i] = left.Elements[j, i] - right.Elements[j, i];
                    }
                }
            }

            return new Matrix(combined);
        }

        public static bool CheckEqualSize(Matrix left, Matrix right)
        {
            return left.Columns == right.Columns && left.Rows == right.Rows
                ? true
                : throw new IndexOutOfRangeException(
                    message: "Lekker bezig, pannekoek! Die matrices zijn niet even groot.");
        }


        public static Matrix operator *(Matrix left, Matrix right)
        {
            // This is only implemented for same-size matrixes. The problem with different size matrixes is that making it one for-loop is much harder.
            if (CheckEqualSize(left, right))
            {
                double[,] result = new double[left.Rows, right.Columns];
                for (int i = 1; i < left.Rows + 1; i++)
                {
                    for (int j = 1; j < left.Columns + 1; j++)
                    {
                        double spot = 0;
                        for (int t = 0; t < left.Rows; t++)
                        {
                            spot += (left.Elements[i - 1, t] * right.Elements[t, j - 1]);
                        }

                        result[i - 1, j - 1] = spot;
                    }
                }

                return new Matrix(result);
            }
            else
            {
                Console.WriteLine(@"The matrix multiplication is only implemented for same-size matrixes.");
            }

            return null;
        }

        public static Matrix operator *(Matrix a, float b)
        {
            Matrix m = new Matrix(new Vector2D(0, 0, 1));
            for (int i = 0; i < m.Rows; i++)
            {
                for (int j = 0; j < m.Columns; j++)
                {
                    m.Elements[i, j] = a.Elements[i, j] * b;
                }
            }

            return m;
        }

        public static Matrix operator *(float a, Matrix b)
        {
            return b * a;
        }

        public static Vector2D operator *(Matrix a, Vector2D b)
        {
            double x, y, w;

            x = (a.Elements[0, 0] * b.X) + (a.Elements[0, 1] * b.Y) + (a.Elements[0, 2] * b.W);
            y = (a.Elements[1, 0] * b.X) + (a.Elements[1, 1] * b.Y) + (a.Elements[1, 2] * b.W);
            w = (a.Elements[2, 0] * b.X) + (a.Elements[2, 1] * b.Y) + (a.Elements[2, 2] * b.W);

            return new Vector2D(x, y, w);
        }

        public override string ToString()
        {
            string str = "";
            str += "Matrix: (";
            for (int i = 0; i < Rows; i++)
            {
                str += "\n";
                for (int j = 0; j < Columns; j++)
                {
                    str += "\t" + Elements[i, j];
                }
            }

            str += "\n)";
            return str;
        }
    }
}