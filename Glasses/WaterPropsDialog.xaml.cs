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
        WaterGlass glassRef;

        public WaterPropsDialog( WaterGlass waterGlass )
        {
            InitializeComponent();

            glassRef = waterGlass;
            waterGlassType = new List<String>();
            waterGlassType.Add("Welle");
            waterGlassType.Add("Strudel");
            waterGlassComboBox.ItemsSource = waterGlassType;

            //distortionLimitScrollBar.Value = waterGlass.DistortionLimit;

            if ( waterGlass.WaterType == WaterGlassType.Welle)
            {
                waterGlassComboBox.SelectedIndex = 0;
            }
            else if (waterGlass.WaterType == WaterGlassType.Strudel )
            {
                waterGlassComboBox.SelectedIndex = 1;
            }

            updateInterface();

        }

        private void updateInterface()
        {
            distortionLimitTextBox.Text = Math.Round(glassRef.DistortionLimit, 1).ToString();

            double delta = Math.Round(glassRef.DistortionDelta, 1);
            distortionDeltaTextBox.Text = delta.ToString();

            if (delta != 0.0)
            {
                distortionTextBox.Text = "0,0";
                distortionTextBox.IsEnabled = false;
                distortionScrollBar.IsEnabled = false;

            }
            else
            {
                distortionTextBox.IsEnabled = true;
                distortionScrollBar.IsEnabled = true;
                distortionTextBox.Text = glassRef.Distortion.ToString();
            }
            waveDensityTextBox.Text = glassRef.WaveDensity.ToString();
        }

        private List<string> waterGlassType;
        public WaterGlassType type;

        private void waterGlassComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string chosenType = (sender as ComboBox).SelectedItem as string;

            if (chosenType == "Welle")
            {
                waveDensityTextBox.IsEnabled = true;
                waveDensityScrollBar.IsEnabled = true;
                type = WaterGlassType.Welle;
            }
            else if (chosenType == "Strudel")
            {
                waveDensityTextBox.IsEnabled = false;
                waveDensityScrollBar.IsEnabled = false;
                type = WaterGlassType.Strudel;
            }
        }

        private void distortionLimitScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (glassRef != null)
            {
                glassRef.DistortionLimit = Math.Round( glassRef.DistortionLimit + e.OldValue - e.NewValue, 1);
                updateInterface();
            }
        }

        private void distortionLimitTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            updateDistortionLimit();
        }

        private void distortionLimitTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.Key == Key.Enter )
            {
                updateDistortionLimit();
            }
        }

        private void updateDistortionLimit()
        {
            double newDisLim = 0.0;
            if (Double.TryParse(distortionLimitTextBox.Text, out newDisLim) == true)
            {
                glassRef.DistortionLimit = newDisLim;
            }
            updateInterface();
        }

        private void distortionDeltaTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            updateDistortionDelta();
        }

        private void distortionDeltaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                updateDistortionDelta();
            }
        }

        private void updateDistortionDelta()
        {
            double newDisDelta = 0.0;
            if (Double.TryParse(distortionDeltaTextBox.Text, out newDisDelta) == true)
            {
                glassRef.DistortionDelta = newDisDelta;
            }
            updateInterface();
        }

        private void deltascrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (glassRef != null)
            {
                glassRef.DistortionDelta = Math.Round(glassRef.DistortionDelta + e.OldValue - e.NewValue, 1);
                updateInterface();
            }
        }
    }
}
