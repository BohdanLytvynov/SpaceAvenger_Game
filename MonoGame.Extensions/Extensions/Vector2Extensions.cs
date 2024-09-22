using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 MultiplyByNumber(this Vector2 vector, float number)
        {
            return new Vector2(vector.X * number, vector.Y * number);
        }

        public static float GetAngleBetweenInRadians(this Vector2 v1, Vector2 v2)
        {
            var dot = Vector2.Dot(v1, v2);

            return MathF.Acos(dot / (v1.Length() * v2.Length()));
        }

        public static Vector2 MultiplyBySizeF(this Vector2 v, SizeF s)
        {
            return new Vector2(v.X * s.Width, v.Y * s.Height);
        }
    }
}
