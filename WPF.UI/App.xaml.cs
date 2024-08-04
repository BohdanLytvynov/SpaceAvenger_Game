using Microsoft.Extensions.DependencyInjection;
using WPF.UI.Enums.FrameTypes;
using WPF.UI.ViewModels.MainWindowVM;
using WPF.UI.Extensions.Services;
using System;
using System.Windows;
using WPF.UI.Views.Pages;
using WPF.UI.Services.Interfaces.PageManager;
using WPF.UI.Services.Realizations.PageManager;
using WPF.UI.Services.Interfaces.MessageBus;
using WPF.UI.Services.Realizations.MessageBus;
using System.Reflection;
using System.Linq;
using System.Windows.Controls;
using WPF.UI.ViewModels.PagesVM;
using ViewModelBaseLibDotNetCore.VM;
using WPF.UI.Attributes.PageManager;
using System.Collections.Generic;
using Domain.Utilities;

namespace WPF.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string SEPARATOR = "_";

        private static IServiceProvider? _Services;

        public static IServiceProvider Services => _Services ??= InitializeServices().BuildServiceProvider();
        
        private static IServiceCollection InitializeServices()
        {
            var services = new ServiceCollection();
            // Add ViewModels (Windows)
            services.AddSingleton<MainWindowViewModel>();
            // Add ViewModels (Pages)

            services.AddSingleton<MainWindow>();

            services.AddPageViewModelsAsSingleton();

            services.AddSingleton<IPageManagerService<FrameType>, PageManagerService<FrameType>>();               
            
            services.AddSingleton<IMessageBus, MessageBusService>();

            services.AddSingleton<Game_Page>();

            services.AddSingleton<GamePage_ViewModel>();
                                                    
            return services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = Services.GetRequiredService<MainWindow>();

            App.Current.MainWindow = mainWindow;

            var pm = Services.GetRequiredService<IPageManagerService<FrameType>>();
            
            if (pm is null)
                throw new Exception($"Fail to get {nameof(PageManagerService<FrameType>)} on Startup!");
            
            // Init PageManager

            var assembly = Assembly.GetExecutingAssembly();

            var pages = ReflexionUtility.GetObjectsTypeInfo(assembly,
                (TypeInfo t) => t is not null &&
             (t.BaseType?.Name.Equals(nameof(Page)) ?? false)
             && t.Name.Contains("Page") && t.GetCustomAttribute<ReflexionDetectionIgnore>() is null);

            var pageViewModels = ReflexionUtility.GetObjectsTypeInfo(assembly,
                (TypeInfo t) => t is not null &&
            (t.GetCustomAttribute<ViewModelType>()?.Usage.Equals(ViewModelUsage.Page) ?? false)
            && t.Name.Contains("ViewModel")
            && t.GetCustomAttribute<ReflexionDetectionIgnore>() is null); 
                                   
            TypeInfo? viewModelInfo = null;

            Page? view = null;

            ViewModelBase? vm = null;

            foreach (var page in pages)
            {
                string pageName = page.Name.Split(SEPARATOR)[0];

                viewModelInfo = pageViewModels.FirstOrDefault(
                    vm => vm.GetCustomAttribute<ViewModelName>() is null? 
                    vm.Name.Split(SEPARATOR)[0].Equals(pageName)
                    : vm.GetCustomAttribute<ViewModelName>()?.Name?.Split(SEPARATOR)[0].Equals(pageName) ?? false);

                if (viewModelInfo is null)
                    throw new Exception($"Can't find corresponding ViewModel to the View {pageName}! Please check you page's and viewmodel's namings.");

                vm = Services.GetRequiredService(viewModelInfo.AsType()) as ViewModelBase;
                view = Activator.CreateInstance(page.AsType()) as Page;

                view!.DataContext = vm;
                vm!.Dispatcher = view.Dispatcher;

                pm.AddPage(
                page.Name, view);
            }

            

            var mainWindowViewModel = Services.GetRequiredService<MainWindowViewModel>();

            mainWindow.DataContext = mainWindowViewModel;
            mainWindowViewModel.Dispatcher = mainWindow.Dispatcher;

            mainWindow.Show();

            pm.SwitchPage(nameof(Game_Page), FrameType.MainFrame);

            pm.SwitchPage(nameof(UserProfileInfo_Page), FrameType.InfoFrame);
            
            base.OnStartup(e);
        }

        
    }
}
