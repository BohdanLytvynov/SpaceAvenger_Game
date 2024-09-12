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
using ViewModelBaseLibDotNetCore.VM;
using WPF.UI.Attributes.PageManager;
using Domain.Utilities;
using WPF.UI.ViewModels.GameWindowVM;
using System.ComponentModel;

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
                        
            services.AddSingleton<GameWindow_ViewModel>();
                                                    
            return services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = Services.GetRequiredService<MainWindow>();

            var gameViewModel = Services.GetRequiredService<GameWindow_ViewModel>();

            gameViewModel.EnableDebugging();//Activate or deactivate debugging

            mainWindow.MonoGameControl.DataContext = gameViewModel;

            var mainWindowViewModel = Services.GetRequiredService<MainWindowViewModel>();

            mainWindow.Closing += (object? sender, CancelEventArgs e) =>
            {
                mainWindowViewModel.Dispose();
            };

            mainWindow.KeyDown += gameViewModel.KeyDownHandler;

            mainWindow.DataContext = mainWindowViewModel;
            mainWindowViewModel.Dispatcher = mainWindow.Dispatcher;

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

                if (viewModelInfo is null && page.GetCustomAttribute<IgnoreVVMMapping>() is null)
                    throw new Exception($"Can't find corresponding ViewModel to the View {pageName}! Please check you page's and viewmodel's namings.");

                if(viewModelInfo is not null)
                    vm = Services.GetRequiredService(viewModelInfo.AsType()) as ViewModelBase;

                view = Activator.CreateInstance(page.AsType()) as Page;

                if (viewModelInfo is not null)
                {
                    view!.DataContext = vm;
                    vm!.Dispatcher = view.Dispatcher;
                }
                
                pm.AddPage(
                page.Name, view);
            }
                                                            
            mainWindow.Show();

            pm.SwitchPage(nameof(ChooseProfile_Page), FrameType.MainFrame);

            pm.SwitchPage(nameof(UserProfileInfo_Page), FrameType.InfoFrame);                        
        }

        
    }
}
