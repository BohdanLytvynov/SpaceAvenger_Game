using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;

namespace MonoGame.Extensions.Behaviors.Transformables.RelativeTransform
{
    public class RelativeTransform : Transform, IRelativeTransform, ITransformable
    {
        public Vector2 ParentPosition 
        {
            get; set;
        }

        public float[,] BuildTransformMatrix()
        {
            float[,] matrix = new float[2,2];

            matrix[0, 0] = LocalBasis[0].Dot(GlobalBasis[0]);
            matrix[0, 1] = LocalBasis[0].Dot(GlobalBasis[1]);
            matrix[1, 0] = LocalBasis[1].Dot(GlobalBasis[0]);
            matrix[1, 1] = LocalBasis[1].Dot(GlobalBasis[1]);

           return matrix;
        }
    }
}
