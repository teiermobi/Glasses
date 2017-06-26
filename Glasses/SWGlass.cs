//Autoren: Meier, Kleber, Pawlowski
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Glasses
{
    class SWGlass : Glass
    {

        // Konstruktor Schwarz-Weiß Glas (mittig positioniert und größer als alle anderen Gläser)
        public SWGlass()
        {
            this.Width = 90;
            this.Height = 90;
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
            this.Margin = new Thickness(Std_KMP_Glasses.main.canvasCanvas.ActualWidth / 2 - 45, Std_KMP_Glasses.main.canvasCanvas.ActualHeight / 2 - 45, 0, 0);
            this.Cursor = Cursors.Hand;
            this.IsPressed = 0;
        }

        // Überschriebene Methode um "Eigenschaften" zu entfernen
        public override void showContextmenu()
        {
            this.ContextMenu = new System.Windows.Controls.ContextMenu();
            MenuItem  menuItem2, menuItem3, menuItem4;
            menuItem2 = new MenuItem();
            menuItem3 = new MenuItem();
            menuItem4 = new MenuItem();
            menuItem2.Header = "Löschen";
            menuItem2.Click += new RoutedEventHandler(Props_Remove);
            menuItem3.Header = "In den Hintergrund";
            menuItem3.Click += new RoutedEventHandler(Change_ZindexMinus);
            menuItem4.Header = "In den Vordergrund";
            menuItem4.Click += new RoutedEventHandler(Change_ZindexPlus);
            this.ContextMenu.Items.Add(menuItem4);
            this.ContextMenu.Items.Add(menuItem3);
            this.ContextMenu.Items.Add(menuItem2);
        }



        // Überschriebene Paint-Methode
        public override void Paint(PaintingLib.BitmapEditor painting)
        {

            painting.Lock();
            Size size = CalcActualSize();
            Point childPos = this.TranslatePoint(new Point(), Parent as Canvas);
            int ox = (int)childPos.X, oy = (int)childPos.Y;
            Color c;
            int m = (int)this.Width;
            int n = (int)this.Height;
            int rad = m < n ? (int)(m / 2.0) : (int)(n / 2.0);

            for (int i = (int)this.Width - 1; i >= 0; i--)
                for (int j = (int)this.Height - 1; j >= 0; j--)
                {

                    c = painting.GetPixel((ox + i), (oy + j));

                    // Berechnung der Kanäle mit verschiedenen Intensitäten (natürliches Farbergebnis)
                    int grayScale = (int)((c.R * 0.3) + (c.G * 0.59) + (c.B * 0.11));


                    // Pixel neu einfärben 
                    painting.SetPixel(ox + i, oy + j, Color.FromRgb((byte)grayScale, (byte)grayScale, (byte)grayScale));

                }


            painting.Unlock();

        }
    }
}
