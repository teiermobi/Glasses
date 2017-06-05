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
    /// Interaktionslogik für HelligkeitPropsDialog.xaml
    /// </summary>
    public partial class HelligkeitPropsDialog : Window
    {
       
        public HelligkeitPropsDialog()
        {
            InitializeComponent();
            main = this;
            this.Brightness = (int)valueOld;
        }

        internal static Glasses.HelligkeitPropsDialog main;

        public int Brightness
        {
            get { return (int)BrightnessSlider.Value; }
            set { BrightnessSlider.Value = value; }
        }


        public static double valueOld;


        private void BrightnessChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BrightnessSlider.Value = e.NewValue;
            Std_KMP_Glasses.main.canvasCanvas.InvalidateVisual();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            valueOld = BrightnessSlider.Value;
        }
    }
}
