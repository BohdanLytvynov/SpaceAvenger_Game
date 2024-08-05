using System.Windows.Controls;
using WPF.UI.Attributes.PageManager;
using WPF.UI.MonoGameControls;

namespace WPF.UI.Views.Pages
{
    /// <summary>
    /// Interaction logic for Game_Page.xaml
    /// </summary>    
    [ReflexionDetectionIgnore]
    public partial class Game_Page : Page
    {
        public MonoGameContentControl GameControlProp { get;}

        public Game_Page()
        {
            InitializeComponent();

            this.GameControlProp = this.GameControl;
        }
    }
}
