using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace WPFGameEngine.Extensions.Animations
{
    public static class ImageAnimationExtension 
    {
        public static void AddKeyFrames(this ObjectAnimationUsingKeyFrames anim,
            IEnumerable<BitmapImage> bitmaps)
        {
            var time = 1f / bitmaps.Count();

            double curr = 0;

            foreach (var item in bitmaps)
            {
                anim.KeyFrames.Add(
                    new DiscreteObjectKeyFrame()
                    {
                        KeyTime = KeyTime.FromPercent(curr),
                        Value = item
                    }
                    );

                curr += time;
            }
        }
    }
}
