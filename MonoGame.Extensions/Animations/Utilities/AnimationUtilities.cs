using MonoGame.Extensions.Animations.Interfaces.AnimationFrames;
using MonoGame.Extensions.Animations.Realizations.AnimationFrames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Extensions.Animations.Utilities
{
    public static class AnimationUtilities
    {
        public static List<IAnimationFrame> BuildAnimationFrames(uint count, float lifeSpan,
            Action<IAnimationFrame, int> configureFrame = null)
        {
            List<IAnimationFrame> frames = new List<IAnimationFrame>();

            for (int i = 0; i < count; i++)
            {
                frames.Add(new AnimationFrame(lifeSpan));

                configureFrame?.Invoke(frames[i], i);
            }

            return frames;
        }       
    }
}
