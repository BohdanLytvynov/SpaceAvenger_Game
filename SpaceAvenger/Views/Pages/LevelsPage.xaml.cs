using SpaceAvenger.Managers.CommunicationManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpaceAvenger.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для LevelsPage.xaml
    /// </summary>
    public partial class LevelsPage : Page
    {
        public LevelsPage()
        {
            CommunicationManager<Page>.Add(this.GetType().Name, this);
            InitializeComponent();
        }
    }
}
