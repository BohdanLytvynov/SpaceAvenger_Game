using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System.Collections.Generic;

namespace MonoGame.Extensions.Behaviors.Transformables
{
    public interface ITransformable
    {
        public Vector2 Position { get; set; }

        public float Rotation { get; set; }

        public Vector2 Scale { get; set; }

        public SizeF TextureSize { get; set; }

        public SizeF ActualSize { get; }

        public Vector2 GeometryCenterOffset { get; set; }

        public Vector2 Origin { get; }

        public Vector2 UpperLeftCorner { get; }

        #region Basis

        public List<Vector2> LocalBasis { get; }

        public List<Vector2> GlobalBasis { get; }

        #endregion

        void Move();

        void Rotate(); 


    }
}
