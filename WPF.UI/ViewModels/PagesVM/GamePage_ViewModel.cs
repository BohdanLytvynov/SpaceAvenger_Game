using WPF.UI.Attributes.PageManager;
using WPF.UI.Enums.FrameTypes;
using WPF.UI.Services.Interfaces.PageManager;
using WPF.UI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace WPF.UI.ViewModels.PagesVM
{
    [ViewModelName("Game_ViewModel")]
    [ViewModelType(ViewModelUsage.Page)]
    internal class GamePage_ViewModel : SubscriptableViewModel, IDisposable
    {        
        #region Fields
        
        private int m_backCount = 3;

        IPageManagerService<FrameType>? m_PageManager;
                        
        #endregion

        #region Properties
        
        #endregion

        #region Ctor
        public GamePage_ViewModel()
        {                                    
            #region InitFields
            
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
                
        #endregion
    }

}
