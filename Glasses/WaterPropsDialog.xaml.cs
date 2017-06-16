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
using System.Windows.Shapes;

namespace Glasses
{
    /// <summary>
    /// Interaction logic for WaterPropsDialog.xaml
    /// </summary>
    public partial class WaterPropsDialog : Window
    {
        public WaterPropsDialog()
        {
            InitializeComponent();
        }

        
        public List<string> WaterSource
        {
            get
            {
                List<string> waterSource = new List<string>();
                waterSource.Add("Welle");
                waterSource.Add("Strudel");
                return waterSource;
            }
            set
            {
                WaterSource = value;
            }
        }

    }
}
