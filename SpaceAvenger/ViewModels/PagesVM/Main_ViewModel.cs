using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SpaceAvenger.Enums.FrameTypes;
using SpaceAvenger.Services.Interfaces.MessageBus;
using SpaceAvenger.Services.Interfaces.PageManager;
using SpaceAvenger.Services.Realizations;
using ViewModelBaseLibDotNetCore.Commands;
using ViewModelBaseLibDotNetCore.VM;

namespace SpaceAvenger.ViewModels.PagesVM
{
    internal class Main_ViewModel : ViewModelBase
    {
        #region Fields
        private IPageManagerService<FrameType> m_PageManager;

        private IMessageBus m_messageBus;
        #endregion

        #region Properties

        #endregion

        #region Commands

        public ICommand OnNewGameButtonPressed { get; }

        #endregion

        #region Ctor

        public Main_ViewModel() : this(default)
        {
            
        }

        public Main_ViewModel(IPageManagerService<FrameType> pageManagerService) 
        {            
            #region Init Commands
            
            m_PageManager = pageManagerService;

            OnNewGameButtonPressed = new Command(
                OnNewGameButtonPressedExecute,
                CanOnNewGameButtonPressedExecute
                );

            #endregion
        }
        #endregion

        #region Methods

        #region Command Methods

        #region OnNewGameButtonPressed

        public bool CanOnNewGameButtonPressedExecute(object p)
        {
            return true;
        }

        public void OnNewGameButtonPressedExecute(object p)
        {
            m_PageManager.SwitchPage("levels", FrameType.MainFrame);
        }

        #endregion

        #endregion

        #endregion
    }
}
