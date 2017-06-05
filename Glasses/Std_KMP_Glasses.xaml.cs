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
using Microsoft.Win32;
using System.IO;






namespace Glasses
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class Std_KMP_Glasses : Window
    {
        public Std_KMP_Glasses()
        {
            InitializeComponent();
            main = this;
 
        }
        internal static Std_KMP_Glasses main;
       

        private void WindoW_SizeChanged(object sender, SizeChangedEventArgs e)
        {
 
            canvasCanvas.MinWidth = main.Width;
            canvasCanvas.MinHeight = main.Height -150;
            if (WindowState == WindowState.Maximized)
            {
              // ToDo
            }


        }

   


        private void btnImageLoad_Click(object sender, RoutedEventArgs e)
        {
            canvasCanvas.InvalidateVisual();
       
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            //For any other formats
            of.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            of.InitialDirectory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString() + "Images";
            if (of.ShowDialog() == true)
            {
                textboxSrc.Text = of.FileName;
            }
           
        }


        Glass he, fi, wa;
        private int index = 1;

        private void btnHelligkeit_Click(object sender, RoutedEventArgs e)
        {

            he = new BrightnessGlass();
            he.Name = "Helligkeit" + index++;
            canvasCanvas.Children.Add(he);
            he.showContextmenu();
            he.InvalidateVisual();

        }

        private void btnHelligkeit_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvasCanvas.Children.Remove(he);
        }




        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            fi = new FilterGlass();
            fi.Name = "Filter" + index++;
            canvasCanvas.Children.Add(fi);
            fi.showContextmenu();
            fi.InvalidateVisual();
        }
        private void btnFilter_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvasCanvas.Children.Remove(fi);
        }


        private void btnWasser_Click(object sender, RoutedEventArgs e)
        {
            wa = new WaterGlass();
            wa.Name = "Wasser" + index++;
            canvasCanvas.Children.Add(wa);
            wa.showContextmenu();
            wa.InvalidateVisual();
            WaterGlass.timmy.Start();
        }

        private void btnWasser_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvasCanvas.Children.Remove(wa);
            WaterGlass.timmy.Stop();
            WaterGlass.delta = 2;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IEnumerable<Glass> Glasses = canvasCanvas.Children.OfType<Glass>();

            foreach (var glass in Glasses)
            {
                //canvasCanvas.Children.Remove(glass);
            }
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IEnumerable<Glass> Glasses = canvasCanvas.Children.OfType<Glass>();

            foreach (var glass in Glasses)
            {
                glass.FocusBorderColor = Color.FromRgb(0, 0, 0);
                glass.FocusBorderWidth = 2;
                glass.PaintBorder();
                glass.InvalidateVisual();
            }

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            IEnumerable<Glass> Glasses = canvasCanvas.Children.OfType<Glass>();

            foreach (var glass in Glasses)
            {
                glass.FocusBorderWidth = 0;
                glass.PaintBorder();
                glass.InvalidateVisual();
            }

        }




       

        private void FilterProps_Click(object sender, RoutedEventArgs e)
        {
            FilterPropsDialog fil = new FilterPropsDialog();
            fil.ShowDialog();
        }


    }

   

    

}
