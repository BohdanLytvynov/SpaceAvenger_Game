using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SpaceAvenger.Attributes.PageManager;
using SpaceAvenger.Enums.FrameTypes;
using SpaceAvenger.Services.Interfaces.MessageBus;
using SpaceAvenger.Services.Interfaces.PageManager;
using SpaceAvenger.Services.Realizations;
using SpaceAvenger.Views.Pages;
using ViewModelBaseLibDotNetCore.Commands;
using ViewModelBaseLibDotNetCore.VM;

namespace SpaceAvenger.ViewModels.PagesVM
{
    [ViewModelType(ViewModelUsage.Page)]
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

        public ICommand OnSurvivalModeButtonPressed { get; }

        #endregion

        #region Ctor

        public Main_ViewModel() 
        {
            #region Init Commands

            OnNewGameButtonPressed = new Command(
                OnNewGameButtonPressedExecute,
                CanOnNewGameButtonPressedExecute
                );

            OnSurvivalModeButtonPressed = new Command(
                OnSurvivalModeButtonPressedExecute,
                CanOnSurvivalModeButtonPressedExecute
                );

            #endregion
        }

        public Main_ViewModel(IPageManagerService<FrameType> pageManagerService) : this()
        {            
            #region Init Fields
            
            m_PageManager = pageManagerService;
            
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

        #region OnSurvivalModeButtonPressed

        private bool CanOnSurvivalModeButtonPressedExecute(object p) => true;

        private void OnSurvivalModeButtonPressedExecute(object p)
        {
            m_PageManager.SwitchPage(nameof(Game_Page), FrameType.MainFrame);
        }

        #endregion

        #endregion

        #endregion
    }
}
