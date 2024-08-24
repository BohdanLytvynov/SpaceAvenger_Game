using Microsoft.Xna.Framework;

namespace MonoGame.Extensions.Behaviors.Transformables
{
    public interface ITransformable
    {
        public Vector2 Position { get; set; }

        public float Rotation { get; set; }

        public Vector2 Scale { get; set; }
    }
}
