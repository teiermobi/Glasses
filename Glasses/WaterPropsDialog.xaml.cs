//Autoren: Meier, Kleber, Pawlowski
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Glasses
{
    public partial class WaterPropsDialog : Window
    {
        WaterGlass glassRef;
        List<string> waterGlassType;

        public WaterPropsDialog( WaterGlass waterGlass )
        {
            InitializeComponent();

            glassRef = waterGlass; //Ref auf jeweiliges WaterGlass Objekt

            waterGlassType = new List<String>();
            waterGlassType.Add("Welle");
            waterGlassType.Add("Strudel");

            waterGlassComboBox.ItemsSource = waterGlassType;

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

        private void waterGlassComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string chosenType = (sender as ComboBox).SelectedItem as string;

            if (chosenType == "Welle")
            {
                waveDensityTextBox.IsEnabled = true;
                waveDensityScrollBar.IsEnabled = true;
            }
            else if (chosenType == "Strudel")
            {
                waveDensityTextBox.IsEnabled = false;
                waveDensityScrollBar.IsEnabled = false;
            }

            updateModel();
        }

        //Inkrement / Dekrement des jeweiligen Werts im Model und update des Dialogs
        private void scrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (glassRef != null)
            {
                System.Windows.Controls.Primitives.ScrollBar scrollbar = (System.Windows.Controls.Primitives.ScrollBar)(sender);
                if (scrollbar.Name == "distortionLimitScrollBar")
                {
                    //Nur "saubere" Werte durch Round(); der absolute Wert der Scrollbar is irrelevant
                    glassRef.DistortionLimit = Math.Round(glassRef.DistortionLimit + e.OldValue - e.NewValue, 1);
                }
                else if (scrollbar.Name == "distortionDeltaScrollBar")
                {
                    glassRef.DistortionDelta = Math.Round(glassRef.DistortionDelta + e.OldValue - e.NewValue, 1);
                }
                else if (scrollbar.Name == "distortionScrollBar")
                {
                    glassRef.Distortion = Math.Round(glassRef.Distortion + e.OldValue - e.NewValue, 1);
                }
                else if (scrollbar.Name == "waveDensityScrollBar")
                {
                    glassRef.WaveDensity = Math.Round(glassRef.WaveDensity + e.OldValue - e.NewValue, 1);
                }
                updateInterface();
            }
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            updateModel();
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                updateModel();
            }
        }

        //Model wird mit Werten aus dem Dialog befüllt
        private void updateModel()
        {
            string type = waterGlassComboBox.SelectedItem as string;
            if ( type == "Welle")
            {
                glassRef.WaterType = WaterGlassType.Welle;
            }

            else if (type == "Strudel")
            {
                glassRef.WaterType = WaterGlassType.Strudel;
            }

            double newDis = 0.0;
            if (Double.TryParse(distortionTextBox.Text, out newDis) == true)
            {
                glassRef.Distortion = newDis;
            }

            double newDisDelta = 0.0;
            if (Double.TryParse(distortionDeltaTextBox.Text, out newDisDelta) == true)
            {
                glassRef.DistortionDelta = newDisDelta;
            }

            double newDisLim = 0.0;
            if (Double.TryParse(distortionLimitTextBox.Text, out newDisLim) == true)
            {
                glassRef.DistortionLimit = newDisLim;
            }

            double newDens = 0.0;
            if (Double.TryParse(waveDensityTextBox.Text, out newDens) == true)
            {
                glassRef.WaveDensity = newDens;
            }
            updateInterface();
        }

        //Dialog wird mit Model synchronisiert
        private void updateInterface()
        {
            double delta = Math.Round(glassRef.DistortionDelta, 1);

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
                distortionTextBox.Text = Math.Round(glassRef.Distortion, 1).ToString();
            }

            distortionDeltaTextBox.Text = delta.ToString();
            distortionLimitTextBox.Text = Math.Round(glassRef.DistortionLimit, 1).ToString();
            waveDensityTextBox.Text = Math.Round(glassRef.WaveDensity, 1).ToString();
        }
    }
}