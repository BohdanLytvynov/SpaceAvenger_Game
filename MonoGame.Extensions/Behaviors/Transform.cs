using Microsoft.Xna.Framework;
using MonoGame.Extensions.Behaviors.Transformables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.Behaviors
{
    public class Transform : ITransformable
    {
        #region Fields

        private Vector2 m_position;

        private float m_rotation;

        private Vector2 m_scale;

        #endregion

        #region Properties

        public Vector2 Position
        {
            get => m_position;
            set => m_position = value;
        }
        public float Rotation
        {
            get => m_rotation;
            set => m_rotation = value;
        }
        public Vector2 Scale
        {
            get => m_scale;
            set => m_scale = value;
        }

        #endregion

        #region ctor

        public Transform(Vector2 position, float rotation, Vector2 scale)
        {
            m_scale = scale;
            m_position = position;
            m_rotation = rotation;
        }

        public Transform() : this(Vector2.Zero, 0f, new Vector2(1,1))
        {
            
        }

        #endregion

    }
}
