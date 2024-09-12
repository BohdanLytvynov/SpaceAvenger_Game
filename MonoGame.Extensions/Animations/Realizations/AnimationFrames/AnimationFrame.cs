using Microsoft.Xna.Framework;
using MonoGame.Extensions.Animations.Interfaces.AnimationFrames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.Animations.Realizations.AnimationFrames
{
    public class AnimationFrame : IAnimationFrame
    {
        private float m_lifespan;

        //Current frame lifespan
        public float Lifespan { get => m_lifespan; }

        public AnimationFrame(float lifespan)
        {
            m_lifespan = lifespan;
        }

        //Here can be the Easing Function 

        public override string ToString()
        {
            return $"Ls: {Lifespan}";
        }
    }
}
