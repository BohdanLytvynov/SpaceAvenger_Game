using Microsoft.Xna.Framework;
using MonoGame.Extensions.Physics.Interfaces;

namespace MonoGame.Extensions.Sprites.Interfaces
{
    public interface IRigidBodySprite
    {
        public Vector2 CenterOfMass { get; }

        public float MOI_CM_DISTANCE { get; }

        public float MOI_CM { get; }

        public IRigidBodyPhysics RigidBodyPhysics { get; }        
    }
}
