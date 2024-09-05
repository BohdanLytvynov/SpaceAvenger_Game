using MonoGame.Extensions.Sprites.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.Physics.Interfaces
{
    public interface IRigidBodyObject : ISprite
    {
        public float Mass { get; set; }
    }
}
