using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFGameEngine.AnimatedControls
{
    /// <summary>
    /// Interaction logic for AnimatedControl.xaml
    /// </summary>
    public partial class AnimatedControl : UserControl
    {
        #region Fields        
        ObjectAnimationUsingKeyFrames m_imgAnim;
        #endregion

        #region Dependency Properties


        public ObjectAnimationUsingKeyFrames ImgAnimation
        {
            get { return (ObjectAnimationUsingKeyFrames)GetValue(ImgAnimationProperty); }
            set { SetValue(ImgAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImgAnimation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImgAnimationProperty;

        private static void OnImgAnimatonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AnimatedControl).m_imgAnim = e.NewValue as ObjectAnimationUsingKeyFrames;
        }

        #endregion

        static AnimatedControl()
        {
            ImgAnimationProperty =
            DependencyProperty.Register("ImageAnimation", typeof(ObjectAnimationUsingKeyFrames),
                typeof(AnimatedControl), new PropertyMetadata(new ObjectAnimationUsingKeyFrames(),
                    OnImgAnimatonChanged));
        }

        public AnimatedControl()
        {
            InitializeComponent();           

            m_imgAnim = new();            
        }

        public void ConfigureImageAnimation(Action<ObjectAnimationUsingKeyFrames> conf)
        {
            if (m_imgAnim == null) throw new Exception("Animation instance hasn't been initialized!");

            conf?.Invoke(m_imgAnim);
        }        
    }
}
