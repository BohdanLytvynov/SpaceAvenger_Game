using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.Extensions
{
    public static class MatrixExtensions
    {
        public static Vector2 Multiply(this float[,] matrix1, Vector2 vector)
        {
            if (matrix1.GetLength(1) != 2)
                throw new ArgumentException("Multiplication is imposible!");

            Vector2 result = new Vector2();

            result.X = matrix1[0,0] * vector.X + matrix1[0,1] * vector.Y;

            result.Y = matrix1[1,0] * vector.X + matrix1[1,1] * vector.Y;

            return result;
        }
    }
}
