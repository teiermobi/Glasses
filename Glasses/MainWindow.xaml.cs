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
using System.Drawing;
using System.Windows.Media;


namespace Glasses
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnImageLoad_Click(object sender, RoutedEventArgs e)
        {
            imageMain.Source = new BitmapImage(new Uri(textboxSrc.Text));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            //For any other formats
            of.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (of.ShowDialog() == true)
            {
                textboxSrc.Text = of.FileName;

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }

    class Glass
    {
        private int BorderWidth;
        private Color BorderColor;

        public void PaintBorder(Color BorderColor, int BorderWidth)
        {
            this.BorderColor = BorderColor;
            this.BorderWidth = BorderWidth;
        }
    }


    class Canvas
    {

    }

}
