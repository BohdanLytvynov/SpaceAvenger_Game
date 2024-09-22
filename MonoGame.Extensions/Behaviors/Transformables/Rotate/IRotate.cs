using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.Behaviors.Transformables.Rotate
{
    public interface IRotate
    {
        public float Rotation { get; set; }

        void Rotate(float radians);        
    }
}
