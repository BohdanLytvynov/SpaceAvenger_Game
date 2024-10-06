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
        /// <summary>
        /// Main Ctor
        /// </summary>
        /// <param name="relaivePosition">Position relative to the parent's object origin</param>
        /// <param name="rotation_Rad">Rotation relative to the parents object rotation</param>
        /// <param name="scale">Scale</param>
        /// <param name="geomCenterOffset">Origin of the object</param>
        /// <param name="globalBasis">Global Basis</param>
        /// <param name="parentTransform">Transform of the Parent</param>
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
        /// <summary>
        /// Updates parent transform of the relative transform, must be called during each update
        /// </summary>
        /// <param name="parentTransform">Parent Transform</param>
        public void UpdateParentData(ITransformable parentTransform)
        {
            m_parentTransform = parentTransform;
        }

        /// <summary>
        /// Calculates the propriate transform and rotation
        /// </summary>
        public void CalculateAbsoluteTransform()
        {           
            //Rescale relPosition Vector
            Vector2 relPos_Scaled = new Vector2()
            {
                X = RelativePosition.X * ParentTransform.Scale.X,
                Y = RelativePosition.Y * ParentTransform.Scale.Y
            };

            //Transform relPosition Vector according to ParentsRotation
            var after_transform = relPos_Scaled.Rotate(ParentTransform.Rotation);
            
            //Calculate new absolute Position
            Position = after_transform + m_parentTransform.Position;

            //Calculate propriate rotation with  respect to parent's one
            Rotation = RelativeRotation + ParentTransform.Rotation;
                       
            //Recalculate Local Basis
            for (int i = 0; i < ParentTransform.LocalBasis.Count; i++)
            {
                this.LocalBasis[i] = ParentTransform.LocalBasis[i].Rotate(this.RelativeRotation);
            }
        }        
    }
}
