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
using WPF.UI.Attributes.PageManager;

namespace WPF.UI.Views.Pages
{
    /// <summary>
    /// Interaction logic for Empty_Page.xaml
    /// </summary>
    [IgnoreVVMMapping]
    public partial class Empty_Page : Page
    {
        public Empty_Page()
        {
            InitializeComponent();
        }
    }
}
