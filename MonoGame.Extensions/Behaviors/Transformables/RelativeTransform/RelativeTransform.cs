using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extensions.Extensions;
using System;
using System.Collections.Generic;

namespace MonoGame.Extensions.Behaviors.Transformables.RelativeTransform
{
    public class RelativeTransform : Transform, IRelativeTransform, ITransformable
    {
        #region Fields

        private ITransformable m_parentTransform;

        #endregion
        //Relative to Parent Origin Position
        public Vector2 RelativePosition { get; set; }
        
        public ITransformable ParentTransform { get => m_parentTransform; 
            set => m_parentTransform = value; }

        public float RelativeRotation { get; set; }

        public RelativeTransform(Vector2 relaivePosition, 
            float rotation_Rad, 
            Vector2 scale,
            Vector2 geomCenterOffset, 
            List<Vector2> globalBasis,
            ITransformable? parentTransform = default) : base(
                Vector2.Zero,
                0f, 
                scale, 
                geomCenterOffset
                , globalBasis) 
        {
            RelativePosition = relaivePosition;

            RelativeRotation = rotation_Rad;

            if (parentTransform is not null)
            {
                m_parentTransform = parentTransform;
            }            
        }
        
        public void UpdateParentData(ITransformable parentTransform)
        {
            m_parentTransform = parentTransform;
        }

        public void CalculateAbsoluteTransform()
        {                        
            Vector2 relPos_Scaled = new Vector2()
            {
                X = RelativePosition.X * ParentTransform.Scale.X,
                Y = RelativePosition.Y * ParentTransform.Scale.Y
            };

            var after_transform = relPos_Scaled.Rotate(ParentTransform.Rotation);
            
            Position = after_transform + m_parentTransform.Position;

            var apply_rot = RelativeRotation + ParentTransform.Rotation;
           
            Rotation = apply_rot;

            //Recalculate Local Basis
            for (int i = 0; i < ParentTransform.LocalBasis.Count; i++)
            {
                this.LocalBasis[i] = ParentTransform.LocalBasis[i].Rotate(this.RelativeRotation);
            }
        }        
    }
}
