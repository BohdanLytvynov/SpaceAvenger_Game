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

namespace WPFGameEngine.Controls
{
    /// <summary>
    /// Interaction logic for GameObject.xaml
    /// </summary>
    public partial class GameObject : UserControl
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region DependencyProperty

        public Canvas Canvas
        {
            get { return (Canvas)GetValue(CanvasProperty); }
            set { SetValue(CanvasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Canvas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanvasProperty;

        #endregion

        #region Ctor

        public GameObject()
        {
            InitializeComponent();
        }

        static GameObject()
        {
            CanvasProperty =
            DependencyProperty.Register("Canvas", typeof(Canvas), 
            typeof(GameObject), 
            new PropertyMetadata(new Canvas(), OnCanvasChanged));
        }

        private static void OnCanvasChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as GameObject).main = e.NewValue as Canvas;
        } 

        #endregion

        #region Methods



        #endregion


    }
}
