using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace MonoGame.Extensions.Extensions
{
    public static class SizeFExtensions
    {
        public static SizeF Multiply(this SizeF s1, SizeF s2)
        {
            return new SizeF(s1.Width * s2.Width, s1.Height * s2.Height);
        }        
    }
}
