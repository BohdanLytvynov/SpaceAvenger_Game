using Microsoft.Xna.Framework;
using MonoGame.Extensions.Extensions;
using MonoGame.Extensions.Physics.Interfaces;

namespace MonoGame.Extensions.Physics.Realizations
{
    public class MaterialPoint : IMaterialPoint
    {
        #region Fields

        private float m_LinearAcceleration;

        private Vector2 m_LinearVelocity;

        #endregion

        #region Properties

        public Vector2 LinearVelocity => m_LinearVelocity;

        public float LinearAcceleration => m_LinearAcceleration;

        #endregion

        #region Functions

        public virtual void Move(ref Vector2 Position, Vector2 dir, GameTime gameTime)
        {
            dir *= m_LinearAcceleration;

            m_LinearVelocity += dir.MultiplyByNumber((float)gameTime.ElapsedGameTime.TotalSeconds);

            Position += m_LinearVelocity.MultiplyByNumber((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        #endregion
    }
}
