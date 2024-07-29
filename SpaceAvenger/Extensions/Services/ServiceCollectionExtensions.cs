using Microsoft.Extensions.DependencyInjection;
using SpaceAvenger.Attributes.PageManager;
using SpaceAvenger.Enums.FrameTypes;
using SpaceAvenger.Services.Interfaces.PageManager;
using SpaceAvenger.Services.Realizations.PageManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ViewModelBaseLibDotNetCore.VM;

namespace SpaceAvenger.Extensions.Services
{
    internal static class ServiceCollectionExtensions
    {
        private const string SEPARATOR = "_";

        public static void ConfigurePageManagerService(this IServiceCollection services)
        {            
            var pageManager = services.BuildServiceProvider().
                GetService<IPageManagerService<FrameType>>();

            if (pageManager is null)
                throw new ArgumentNullException($"Fail to get {nameof(PageManagerService<FrameType>)}!");

            var assembly = Assembly.GetExecutingAssembly();

            var types = assembly.DefinedTypes;

            if (types.Count() <= 0)
                throw new Exception("Assembly is empty!");

            var pages = types.Where(t => t is not null &&
             (t.BaseType?.Name.Equals(nameof(Page)) ?? false)
             && t.Name.Contains("Page"));

            if (pages.Count() <= 0)
                throw new Exception("Fail to find some PageViews!");

            var viewModels = types.Where(t => t is not null &&
            (t.BaseType?.Name.Equals(nameof(ViewModelBase)) ?? false )
            && t.Name.Contains("ViewModel") 
            && t.GetCustomAttribute<PageManagerDetectionIgnore>() is null);

            if (viewModels.Count() <= 0)
                throw new Exception("Fail to find some ViewModels!");

            TypeInfo? viewModelInfo = null;

            Page? pageView = null;

            ViewModelBase? viewModel = null;

            foreach (var page in pages)// O(n)
            {                
                // Get the name of the page
                string pageName = page.Name.Split(SEPARATOR)[0];

                viewModelInfo = viewModels.FirstOrDefault(vm => vm.Name.Split(SEPARATOR)[0].Equals(pageName));

                if (viewModelInfo is null)
                    throw new Exception($"Can't find corresponding ViewModel to the View {pageName}! Please check you page's and viewmodel's namings.");

                pageView = Activator.CreateInstance(page.AsType()) as Page;

                // Analize ctors
                var paramsCtor = viewModelInfo.GetConstructors().Where(c => c.GetParameters().Length > 0).Count() > 0;
                if (paramsCtor)
                {
                    viewModel = Activator.CreateInstance(viewModelInfo.AsType(), pageManager) as ViewModelBase;
                }
                else
                {
                    viewModel = Activator.CreateInstance(viewModelInfo.AsType()) as ViewModelBase;
                }
                

                pageView!.DataContext = viewModel;
                viewModel!.Dispatcher = pageView.Dispatcher;

                pageManager.AddPage(
                    page.Name, pageView);

                services.AddSingleton(viewModel);
            }
        }

        
    }
}
