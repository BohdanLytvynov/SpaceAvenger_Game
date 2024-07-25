using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SpaceAvenger.Managers.PageManager;
using ViewModelBaseLibDotNetCore.Commands;
using ViewModelBaseLibDotNetCore.VM;

namespace SpaceAvenger.ViewModels.PagesVM
{
    public class MainPageViewModel : ViewModelBase
    {       
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Commands

        public ICommand OnNewGameButtonPressed { get; }

        #endregion

        #region Ctor
        public MainPageViewModel()
        {
            

            #region Init Commands

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
            PageManager.SwitchPage("levels");
        }

        #endregion

        #endregion

        #endregion
    }
}
