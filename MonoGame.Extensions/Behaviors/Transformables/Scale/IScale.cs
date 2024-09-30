using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.Behaviors.Transformables.Scale
{
    public interface IScale
    {
        Vector2 Scale { get; set; }        

        SizeF TextureSize
        {
            get;
            set;
        }

        SizeF ActualSize
        {
            get;
        }
    }
}
