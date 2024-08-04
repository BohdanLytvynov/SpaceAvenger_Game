using SpaceAvenger.Attributes.PageManager;
using SpaceAvenger.Enums.FrameTypes;
using SpaceAvenger.Services.Interfaces.PageManager;
using SpaceAvenger.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;
using WPFGameEngine.Utilities.Directories;
using System.Windows;
using WPFGameEngine.Utilities.Images;
using System.Windows.Controls;
using WPFGameEngine.AnimatedControls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using WPFGameEngine.Extensions;
using WPFGameEngine.Realizations.Loader;
using WPFGameEngine.Extensions.Animations;

namespace SpaceAvenger.ViewModels.PagesVM
{
    [ViewModelName("Game_ViewModel")]
    [ViewModelType(ViewModelUsage.Page)]
    internal class GamePage_ViewModel : SubscriptableViewModel, IDisposable
    {
        #region event

        public event Func<Task> OnPageBuild;

        #endregion

        #region Fields

        private string m_pathToImages;

        private string m_pathToBackGrounds;

        private int m_backCount = 3;

        IPageManagerService<FrameType> m_PageManager;

        List<BitmapImage>? m_GameBacks;

        ImageSource m_GameBack;

        private Canvas m_Canvas;

        #endregion

        #region Properties

        public Canvas Canvas { get=> m_Canvas; set=> Set(ref m_Canvas, value); }

        public ImageSource Background { get=> m_GameBack; set=> Set(ref m_GameBack, value); }

        private RecursiveBitmapImageLoader m_RecursiveBitmapImageLoader;
        #endregion

        #region Ctor
        public GamePage_ViewModel()
        {
            m_pathToImages = Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + 
                "Images";

            m_pathToBackGrounds = Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar +
                "Images/Backgrounds/GamePage";
            
            m_RecursiveBitmapImageLoader = new RecursiveBitmapImageLoader(m_pathToImages);

            #region InitFields

            m_Canvas = new Canvas();

            #endregion

            SetBackGround();

            SetEnvironment();            
        }
        
        public GamePage_ViewModel(IPageManagerService<FrameType> pageManager) : this() 
        {
            m_PageManager = pageManager;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods

        #region Set BackGround

        private void SetBackGround()
        {            
            Random r = new Random();

            m_GameBacks = App.Current.TryGetResourceOrLoad("GamePage",
                m_RecursiveBitmapImageLoader, "Back");

            var index = r.Next(0, m_backCount);

            m_GameBack = m_GameBacks[index];
        }

        private void SetEnvironment()
        { 
            AnimatedControl pulseStar = new AnimatedControl();
            pulseStar.Width = 128;
            pulseStar.Height = 128;                                    

            Canvas.Children.Add(pulseStar);

            pulseStar.ConfigureAnimation(async sb =>
            {
                ObjectAnimationUsingKeyFrames m_anim = new();

                m_anim.AutoReverse = true;

                sb.RepeatBehavior = new RepeatBehavior(1.4);
                                
                sb.Duration = new Duration(TimeSpan.FromSeconds(0.7));

                var images = App.Current.TryGetResourceOrLoad("PulsatingStar", 
                    m_RecursiveBitmapImageLoader, "Anim");

                m_anim.AddKeyFrames(images);
                                
                sb.Children.Add(m_anim);

                Storyboard.SetTarget(m_anim, pulseStar.Image);
                Storyboard.SetTargetProperty(m_anim, new PropertyPath(Image.SourceProperty));

                sb.AccelerationRatio = 0.33;
                sb.DecelerationRatio = 0.55;
            });

            pulseStar.Begin();
        }

        #endregion

        

        #endregion
    }

}
