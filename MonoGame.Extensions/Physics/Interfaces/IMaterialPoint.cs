using Microsoft.Xna.Framework;

namespace MonoGame.Extensions.Physics.Interfaces
{
    public interface IMaterialPoint : IPhysicsBehavior
    {
        public Vector2 LinearVelocity { get; }

        public float LinearAcceleration { get; }

        void Move(ref Vector2 Position, Vector2 dir, GameTime gameTime);
    }
}
