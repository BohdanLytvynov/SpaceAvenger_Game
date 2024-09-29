using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace MonoGame.Extensions.Behaviors.Transformables.RelativeTransform
{
    public class RelativeTransform : Transform, IRelativeTransform, ITransformable
    {
        public Vector2 ParentPosition 
        {
            get; set;
        }

        public SizeF ParentTextureSize { get; set; }

        public SizeF ParentActualSize { get ; set; }

        public Vector2 RelativePosition { get; set; }

        public RelativeTransform(Vector2 relaivePosition, 
            float rotation, 
            Vector2 scale,
            Vector2 geomCenterOffset, 
            List<Vector2> globalBasis,
            ITransformable? parentTransform = default) : base(
                Vector2.Zero, 
                rotation, 
                scale, 
                geomCenterOffset
                , globalBasis) 
        {
            RelativePosition = relaivePosition;

            if (parentTransform is not null)
            {
                ParentActualSize = parentTransform.ActualSize;
                ParentPosition = parentTransform.Position;
                ParentTextureSize = parentTransform.TextureSize;
            }            
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

        public void UpdateParentData(ITransformable parentTransform)
        {
            ParentActualSize = parentTransform.ActualSize;
            ParentPosition = parentTransform.Position;
            ParentTextureSize = parentTransform.TextureSize;
        }
    }
}
