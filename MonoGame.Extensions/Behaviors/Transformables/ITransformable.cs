using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extensions.Behaviors.Transformables.Rotate;
using MonoGame.Extensions.Behaviors.Transformables.Scale;
using System.Collections.Generic;

namespace MonoGame.Extensions.Behaviors.Transformables
{
    public interface ITransformable : IRotate, IScale
    {       
        public Vector2 Position { get; set; }
                       
        public Vector2 GeometryCenterOffset { get; set; }

        public Vector2 Origin { get; }

        public Vector2 UpperLeftCorner { get; }

        #region Basis

        public List<Vector2> LocalBasis { get; }

        public List<Vector2> GlobalBasis { get; }

        #endregion        
    }
}
