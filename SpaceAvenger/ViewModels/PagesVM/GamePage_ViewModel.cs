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

namespace SpaceAvenger.ViewModels.PagesVM
{
    [ViewModelName("Game_ViewModel")]
    [ViewModelType(ViewModelUsage.Page)]
    internal class GamePage_ViewModel : SubscriptableViewModel, IDisposable
    {
        #region Fields

        const int BACKGROUND_COUNT = 3;

        IPageManagerService<FrameType> m_PageManager;

        List<ImageSource> m_GameBacks;

        ImageSource m_GameBack;

        #endregion

        #region Properties
        public ImageSource Background { get=> m_GameBack; set=> Set(ref m_GameBack, value); }
        #endregion

        #region Ctor
        public GamePage_ViewModel()
        {
            Random r = new Random();

            #region InitFields

            m_GameBacks = GetBackImages(BACKGROUND_COUNT);

            var index = r.Next(0, BACKGROUND_COUNT);

            m_GameBack = m_GameBacks[index];

            #endregion
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

        private List<ImageSource> GetBackImages(int count)
        {
            List<ImageSource> imgs = new List<ImageSource>();
            for (int i = 0; i < count; i++)
            {
                imgs.Add(App.Current.Resources[$"Back{i + 1}_GamePage"] as ImageSource);
            }
            
            return imgs;
        }

        #endregion
    }

}
