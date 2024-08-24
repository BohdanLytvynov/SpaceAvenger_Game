using System.Windows;
using WPF.UI.MonoGameControls;

namespace WPF.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MonoGameContentControl MonoGameContentControl { get; }

        public MainWindow()
        {            
            InitializeComponent();

            MonoGameContentControl = this.MonoGameControl;            
        }       
    }
}
