using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.Behaviors.Transformables.RelativeTransform
{
    public interface IRelativeTransform
    {
        Vector2 ParentPosition { get; set; }

        float[,] BuildTransformMatrix();
    }
}
