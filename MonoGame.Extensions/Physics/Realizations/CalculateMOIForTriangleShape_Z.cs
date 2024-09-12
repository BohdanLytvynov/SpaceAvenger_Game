using MonoGame.Extensions.Physics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.Physics.Realizations
{
    public class CalculateMOIForTriangleShape_Z : IMOICalculator
    {
        public float Mass { get; }

        public float Base { get; }

        public float Length { get; }

        public CalculateMOIForTriangleShape_Z(float mass, float Base, float length)
        {
            Mass = mass;
            this.Base = Base;
            Length = length;
        }
       
        public float Calculate()
        {
            return (Mass * Base * MathF.Pow(Length, 2))/72f;
        }
    }
}
