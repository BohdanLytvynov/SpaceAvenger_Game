using Microsoft.Xna.Framework;
using MonoGame.Extensions.Behaviors.Transformables;

namespace MonoGame.Extensions.Sprites.Interfaces
{
    public interface ISprite
    {
        public ITransformable Transform { get; set; }                  
    }
}
