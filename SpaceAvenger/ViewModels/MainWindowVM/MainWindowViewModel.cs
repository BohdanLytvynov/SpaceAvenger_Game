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
using SpaceAvenger.Managers.PageManager;
using Models.DAL.Entities.User;
using System.Reflection;

namespace SpaceAvenger.ViewModels.MainWindowVM
{
    public class MainWindowViewModel : ViewModelBase
    {        
        #region Fields

        object m_mainframe;

        string m_title;

        private Guid m_userId;

        private string m_userName;

        #endregion

        #region Properties

        public object MainFrame 
        {
            get=>m_mainframe;
            set=> Set(ref m_mainframe, value);
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

            LoadPages();
            
            #endregion

            PageManager.OnSwitchScreenMethodInvoked += PageManager_OnSwitchScreenMethodInvoked;

            m_mainframe = PageManager.GetPage("ChooseProfilePage")!;
        }

        private void PageManager_OnSwitchScreenMethodInvoked(Page obj)
        {
            MainFrame = obj;
        }
        #endregion

        #region Methods
        private void LoadPages()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var types = assembly.DefinedTypes;

            var pages = types.Where(t => t is not null &&
             t.BaseType!.Name.Equals(nameof(Page))
             && t.Name.Contains("Page", StringComparison.OrdinalIgnoreCase));

            foreach (var page in pages)
            {
                PageManager.AddPage(
                    page.Name,
                    Activator.CreateInstance(page.AsType()) as Page);
            }
        }
        #endregion
    }
}
