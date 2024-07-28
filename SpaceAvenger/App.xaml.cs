using Microsoft.Extensions.DependencyInjection;
using SpaceAvenger.Enums.FrameTypes;
using SpaceAvenger.Services.Interfaces;
using SpaceAvenger.Services.Realizations;
using SpaceAvenger.ViewModels.MainWindowVM;
using SpaceAvenger.Extensions.Services;
using System;
using System.Windows;
using SpaceAvenger.Views.Pages;

namespace SpaceAvenger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IServiceProvider? _Services;

        public static IServiceProvider Services => _Services ??= InitializeServices().BuildServiceProvider();

        private static IServiceCollection InitializeServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IPageManagerService<FrameType>, PageManagerService<FrameType>>();

            services.ConfigurePageManagerService();

            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton(
                s =>
                { 
                    var vm = s.GetService<MainWindowViewModel>();

                    MainWindow main = new MainWindow();

                    main.DataContext = vm;
                    vm.Dispatcher = main.Dispatcher;
                    return main;
                }
                );

            return services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Services.GetRequiredService<MainWindow>().Show();

            var PageManager = Services.GetService<IPageManagerService<FrameType>>();

            if (PageManager is null)
                throw new Exception($"Fail to get {nameof(PageManagerService<FrameType>)} on Startup!");

            PageManager.SwitchPage(nameof(ChooseProfile_Page), FrameType.MainFrame);

            PageManager.SwitchPage(nameof(UserProfileInfo_Page), FrameType.InfoFrame);
        }

        
    }
}
