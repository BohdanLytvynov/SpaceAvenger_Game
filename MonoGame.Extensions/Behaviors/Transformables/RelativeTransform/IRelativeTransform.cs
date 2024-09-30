using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace MonoGame.Extensions.Behaviors.Transformables.RelativeTransform
{
    public interface IRelativeTransform
    {
        Vector2 RelativePosition { get; set; }

        float RelativeRotation { get; set; }    

        ITransformable ParentTransform { get; set; }
       
        void UpdateParentData(ITransformable parentTransform);

        void CalculateAbsoluteTransform();        
    }
}
