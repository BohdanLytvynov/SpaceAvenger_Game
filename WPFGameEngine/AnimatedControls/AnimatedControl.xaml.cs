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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFGameEngine.Utilities.Images;

namespace WPFGameEngine.AnimatedControls
{
    /// <summary>
    /// Interaction logic for AnimatedControl.xaml
    /// </summary>
    public partial class AnimatedControl : UserControl
    {
        #region Fields
        
        private Storyboard m_storyboard;

        public Image Image { get=> this.img; }

        #endregion

        #region Dependency Properties
              
        #endregion

        static AnimatedControl()
        {
            
        }
       
        public AnimatedControl()
        {
            InitializeComponent();

            m_storyboard = new Storyboard();                
        }

        public void ConfigureAnimation(Action<Storyboard> conf)
        {
            if (m_storyboard == null) throw new Exception("Storyboard hasn't been initialized yet!");

            conf(m_storyboard);
        }

        public void Begin()
        {            
            m_storyboard.Begin();
        }
    }
}
