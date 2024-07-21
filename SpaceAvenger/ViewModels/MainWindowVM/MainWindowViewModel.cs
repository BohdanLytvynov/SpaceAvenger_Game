using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBaseLibDotNetCore.VM;
using ViewModelBaseLibDotNetCore.Commands;
using SpaceAvenger.Views.Pages;
using System.Windows.Input;
using System.Windows.Controls;

namespace SpaceAvenger.ViewModels.MainWindowVM
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Pages

        MainPage m_mainPage;

        LevelsPage m_levelsPage;

        ChooseProfileWpf m_ChoosePofile;
        
        #endregion

        #region Fields

        object m_mainframe;

        string m_title;

        #endregion

        #region Properties

        public object MainFrame 
        {
            get=>m_mainframe;
            set=> Set<object>(ref m_mainframe, value);
        }

        public string Tittle 
        {
            get=> m_title;
            set=> Set(ref m_title, value);
        }

        #endregion
        
        #region Ctor
        public MainWindowViewModel()
        {            
            #region Init Fields

            m_title = "Space Avenger V 1.0";

            #endregion

            #region Init Pages

            m_mainPage = new MainPage();

            m_levelsPage = new LevelsPage();

            m_ChoosePofile = new ChooseProfileWpf();

            PageManager.AddPage("main",m_mainPage);

            PageManager.AddPage("levels", m_levelsPage);

            PageManager.AddPage("ChooseProfile", m_ChoosePofile);

            #endregion

            PageManager.OnSwitchScreenMethodInvoked += PageManager_OnSwitchScreenMethodInvoked;

            m_mainframe = m_ChoosePofile;
        }

        private void PageManager_OnSwitchScreenMethodInvoked(Page obj)
        {
            MainFrame = obj;
        }
        #endregion

        #region Methods

        #endregion
    }
}
