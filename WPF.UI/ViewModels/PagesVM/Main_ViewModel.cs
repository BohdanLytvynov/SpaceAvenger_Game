using System.Windows.Input;
using WPF.UI.Attributes.PageManager;
using WPF.UI.Enums.FrameTypes;
using WPF.UI.Services.Interfaces.MessageBus;
using WPF.UI.Services.Interfaces.PageManager;
using ViewModelBaseLibDotNetCore.Commands;
using ViewModelBaseLibDotNetCore.VM;
using WPF.UI.Views.Pages;
using WPF.UI.Services.Realizations.Message;
using WPF.UI.Services.Interfaces.Message;

namespace WPF.UI.ViewModels.PagesVM
{
    [ViewModelType(ViewModelUsage.Page)]
    internal class Main_ViewModel : ViewModelBase
    {
        #region Fields
        private IPageManagerService<FrameType>? m_PageManager;

        private IMessageBus? m_messageBus;
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

        public Main_ViewModel(IPageManagerService<FrameType> pageManagerService, IMessageBus messBus) : this()
        {            
            #region Init Fields
            
            m_PageManager = pageManagerService;

            m_messageBus = messBus;
            
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
            m_PageManager!.SwitchPage("levels", FrameType.MainFrame);
        }

        #endregion

        #region OnSurvivalModeButtonPressed

        private bool CanOnSurvivalModeButtonPressedExecute(object p) => true;

        private void OnSurvivalModeButtonPressedExecute(object p)
        {            
            m_messageBus!.Send<IGameMessage>(new StartGame());

            m_PageManager!.SwitchPage(nameof(Game_Page), FrameType.MainFrame);
        }

        #endregion

        #endregion

        #endregion
    }
}
