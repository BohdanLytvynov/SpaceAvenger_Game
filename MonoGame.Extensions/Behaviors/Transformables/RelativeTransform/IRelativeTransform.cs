using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace MonoGame.Extensions.Behaviors.Transformables.RelativeTransform
{
    public interface IRelativeTransform
    {
        Vector2 RelativePosition { get; set; }

        Vector2 ParentPosition { get; }

        SizeF ParentTextureSize { get; }

        SizeF ParentActualSize { get; }

        float[,] BuildTransformMatrix();

        void UpdateParentData(ITransformable parentTransform);
    }
}
