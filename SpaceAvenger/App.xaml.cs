using Microsoft.Extensions.DependencyInjection;
using SpaceAvenger.Enums.FrameTypes;
using SpaceAvenger.Services.Interfaces;
using SpaceAvenger.Services.Realizations;
using SpaceAvenger.ViewModels.MainWindowVM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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

            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<MainWindow>(
                s =>
                { 
                    var vm = s.GetService<MainWindowViewModel>();

                    MainWindow main = new MainWindow();

                    main.DataContext = vm;

                    return main;
                }
                );

            return services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Services.GetRequiredService<MainWindow>().Show();
        }
    }
}
