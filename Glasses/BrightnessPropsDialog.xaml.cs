//Autoren: Meier, Kleber, Pawlowski
using System;
using System.Windows;

namespace Glasses
{
    /// <summary>
    /// Interaktionslogik für BrightnessPropsDialog.xaml
    /// </summary>
    public partial class BrightnessPropsDialog : Window
    {
       
        public BrightnessPropsDialog()
        {
            InitializeComponent();
            main = this;
            this.Brightness = (int)valueOld;
        }

        internal static Glasses.BrightnessPropsDialog main;

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
