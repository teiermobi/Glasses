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



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

           // Vielleicht hierdurch das canvasCanvas laden lassen !?
           //canvasCanvas. ..
        }


        // Wenn das Fenster vergrößert wird, auch das Canvas mit Bild anpassen
        private void WindoW_SizeChanged(object sender, SizeChangedEventArgs e)
        {
 
            canvasCanvas.MinWidth = canvasGrid.ActualWidth;
            canvasCanvas.MinHeight = canvasGrid.ActualHeight;
            if (WindowState == WindowState.Maximized)
            {
              // ToDo
            }


        }



        // Button um manuel das Bild zu laden
        private void btnImageLoad_Click(object sender, RoutedEventArgs e)
        {
            canvasCanvas.InvalidateVisual();
       
        }

        // Filedialog zur Auswahl eines neuen Bildes laden
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            //For any other formats
            of.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            of.InitialDirectory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString() + "Images";
            if (of.ShowDialog() == true)
            {
                textboxSrc.Text = of.FileName;
                canvasCanvas.ImageSource = of.FileName;

                canvasCanvas.InvalidateVisual();
            }
           
        }


        Glass he, fi, wa;
        private int index = 1;


        // Hellgiekitsglas erstellen (vorher Check ob die Checkbox gechecked ist)
        private void btnHelligkeit_Click(object sender, RoutedEventArgs e)
        {

            he = new BrightnessGlass();
            he.Name = "Helligkeit" + index++;
            if (Std_KMP_Glasses.main.checkBox.IsChecked ?? true)
            {
                he.FocusBorderColor = Color.FromRgb(0, 0, 0);
                he.FocusBorderWidth = 2;
                he.PaintBorder();
            }
            else
            {}
            canvasCanvas.Children.Add(he);
            he.showContextmenu();
            he.InvalidateVisual();

        }

        // Filterglas erstellen (vorher Check ob die Checkbox gechecked ist)
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            fi = new FilterGlass();
            fi.Name = "Filter" + index++;
            if (Std_KMP_Glasses.main.checkBox.IsChecked ?? true)
            {
                fi.FocusBorderColor = Color.FromRgb(0, 0, 0);
                fi.FocusBorderWidth = 2;
                fi.PaintBorder();
            }
            else
            { }
            
            canvasCanvas.Children.Add(fi);
            fi.showContextmenu();
            fi.InvalidateVisual();
        }

        // Waserglas erstellen (vorher Check ob die Checkbox gechecked ist)
        private void btnWasser_Click(object sender, RoutedEventArgs e)
        {
            wa = new WaterGlass();
            wa.Name = "Wasser" + index++;
            if (Std_KMP_Glasses.main.checkBox.IsChecked ?? true)
            {
                wa.FocusBorderColor = Color.FromRgb(0, 0, 0);
                wa.FocusBorderWidth = 2;
                wa.PaintBorder();
            }
            else
            {}
            
            canvasCanvas.Children.Add(wa);
            wa.showContextmenu();
            wa.InvalidateVisual();
            WaterGlass.timmy.Start();
        }


        

        // Demo 1 Gläser (Filter + Helligkeit mit blauen Rändern)

        private void Button_Click_Demo1(object sender, RoutedEventArgs e)
        {
            
            canvasCanvas.Children.Clear();

            fi = new FilterGlass();
            fi.Name = "Filter" + index++;
            fi.FocusBorderColor = Color.FromRgb(0, 0, 220);
            fi.FocusBorderWidth = 2;
            fi.PaintBorder();
            fi.Margin = new Thickness(80, 20, 0, 0);
            fi.Width = 200;
            fi.Height = 120;
            canvasCanvas.Children.Add(fi);
            fi.showContextmenu();
            fi.InvalidateVisual();

            he = new BrightnessGlass();
            he.Name = "Helligkeit" + index++;
            he.FocusBorderColor = Color.FromRgb(0, 0, 220);
            he.FocusBorderWidth = 2;
            he.PaintBorder();
            he.Margin = new Thickness(120, 120, 0, 0);
            he.Width = 70;
            he.Height = 70;
            canvasCanvas.Children.Add(he);
            he.showContextmenu();
            he.InvalidateVisual();

        }


        // Demo 2 Gläser (Wasser + Helligkeit mit grünen Rändern)
        private void Button_Click_Demo2(object sender, RoutedEventArgs e)
        {
            canvasCanvas.Children.Clear();

            wa = new WaterGlass();
            wa.Name = "Wasser" + index++;
            wa.FocusBorderColor = Color.FromRgb(0, 220, 0);
            wa.FocusBorderWidth = 2;
            wa.PaintBorder();
            wa.Margin = new Thickness(80, 20, 0, 0);
            wa.Width = 200;
            wa.Height = 180;
            canvasCanvas.Children.Add(wa);
            wa.showContextmenu();
            wa.InvalidateVisual();

            he = new BrightnessGlass();
            he.Name = "Helligkeit" + index++;
            he.FocusBorderColor = Color.FromRgb(0, 220, 0);
            he.FocusBorderWidth = 2;
            he.PaintBorder();
            he.Margin = new Thickness(190, 120, 0, 0);
            he.Width = 90;
            he.Height = 70;
            canvasCanvas.Children.Add(he);
            he.showContextmenu();
            he.InvalidateVisual();
        }


        // Demo 3 Gläser (Filter + Wasser mit grün-blauem Rändern)
        private void Button_Click_Demo3(object sender, RoutedEventArgs e)
        {
            canvasCanvas.Children.Clear();

            wa = new WaterGlass();
            wa.Name = "Wasser" + index++;
            wa.FocusBorderColor = Color.FromRgb(0, 220, 220);
            wa.FocusBorderWidth = 2;
            wa.PaintBorder();
            wa.Margin = new Thickness(80, 20, 0, 0);
            wa.Width = 200;
            wa.Height = 180;
            canvasCanvas.Children.Add(wa);
            wa.showContextmenu();
            wa.InvalidateVisual();


            fi = new FilterGlass();
            fi.Name = "Filter" + index++;
            fi.FocusBorderColor = Color.FromRgb(0, 220, 220);
            fi.FocusBorderWidth = 2;
            fi.PaintBorder();
            fi.Margin = new Thickness(300, 20, 0, 0);
            fi.Width = 200;
            fi.Height = 120;
            canvasCanvas.Children.Add(fi);
            fi.showContextmenu();
            fi.InvalidateVisual();
        }


        // Checkbox um Ränder der Gläser zu aktivieren
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


        // Checkbox um Ränder der Gläser zu deaktivieren
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
