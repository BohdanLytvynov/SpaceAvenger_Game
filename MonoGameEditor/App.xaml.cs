using MonoGameEditor.ViewModels.Windows;
using System.Windows;

namespace MonoGameEditor;

public partial class App : Application
{
    MainWindow m_MainWindow;
    MainWindowViewModel m_ViewModel;

    public App()
    {
        m_MainWindow = new MainWindow();
        m_ViewModel = new MainWindowViewModel();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        m_ViewModel.Dispose();  

        base.OnExit(e);
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        m_MainWindow.DataContext = m_ViewModel;

        m_MainWindow.ContentControl.DataContext = m_ViewModel;

        m_ViewModel.Dispatcher = m_MainWindow.Dispatcher;

        m_MainWindow.Show();

        base.OnStartup(e);
    }
}
