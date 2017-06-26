//Autoren: Meier, Kleber, Pawlowski
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Glasses
{
    public class Glass : PaintingLib.PainterBase
    {
     
        Color BorderColor = Color.FromRgb(0,0,0);
        double BorderWidth = 5;
        int ispressed = 0;
        int isscaling = 0;
 
        // Standard Settings eines Glasses
        public Glass()
        {
            this.Width = 60;
            this.Height = 60;
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
            this.Margin = new Thickness(0, 0, 0, 0);
            this.Cursor = Cursors.Hand;
            this.IsPressed = 0;
        }

        // Eigenschaft um Rahmenfarbe zu bestimmen
        public Color FocusBorderColor
        {
            get { return (Color)BorderColor; }
            set { BorderColor = value; }
        }

        // Eigenschaft um Rahmenbreite zu bestimmen
        public int FocusBorderWidth
        {
            get { return (int)BorderWidth; }
            set { BorderWidth = value; }
        }

        // Ist ein Glass gedrückt ?
        public int IsPressed
        {
            get { return ispressed; }
            set { ispressed = value; }
        }

        // Wird ein Glas skaliert
        public int IsScaling
        {
            get { return isscaling; }
            set { isscaling = value; }
        }

      

        // Funktion um Rahmen zu zeichnen
        public void PaintBorder()
        {
            this.BorderBrush = new SolidColorBrush(BorderColor);
            this.BorderThickness = new Thickness(BorderWidth);
            this.InvalidateVisual();
        }

        public void Removing()
        {
            Std_KMP_Glasses.main.canvasCanvas.Children.Remove(this);
        }

        // Funktion mit Initialisierung des Kontextmenüs für jedes Glas
        public virtual void showContextmenu()
        {
            this.ContextMenu = new System.Windows.Controls.ContextMenu();
            MenuItem menuItem1, menuItem2, menuItem3, menuItem4;
            menuItem1 = new MenuItem();
            menuItem2 = new MenuItem();
            menuItem3 = new MenuItem();
            menuItem4 = new MenuItem();
            menuItem1.Header = "Eigenschaften";
            menuItem1.Click += new RoutedEventHandler(ShowPropsDialog);
            menuItem2.Header = "Löschen";
            menuItem2.Click += new RoutedEventHandler(Props_Remove);
            menuItem3.Header = "In den Hintergrund";
            menuItem3.Click += new RoutedEventHandler(Change_ZindexMinus);
            menuItem4.Header = "In den Vordergrund";
            menuItem4.Click += new RoutedEventHandler(Change_ZindexPlus);
            this.ContextMenu.Items.Add(menuItem4);
            this.ContextMenu.Items.Add(menuItem3);
            this.ContextMenu.Items.Add(menuItem2);
            this.ContextMenu.Items.Add(menuItem1);
        }

        // Funktion um Z-Index eines Glases zu ändern (In den Hintergrund)
        public void Change_ZindexMinus(object sender, EventArgs e)
        {
            int ind = Canvas.GetZIndex(this);
            Canvas.SetZIndex(this, ind - 1 );
        }


        // Funktion um Z-Index eines Glases zu ändern (In den Vordergrund)
        public void Change_ZindexPlus(object sender, EventArgs e)
        {
            int ind = Canvas.GetZIndex(this);
            Canvas.SetZIndex(this, ind + 1);
        }

        // Virtuelle Methode, welche später in den jeweiligen Klassen der Gläser überschrieben wird
        public virtual void ShowPropsDialog(object sender, EventArgs e)
        {
            
        }

        // Funktion um ein Glass aus Canvas zu entfernen
        public void Props_Remove(object sender, EventArgs e)
        {
            Std_KMP_Glasses.main.canvasCanvas.Children.Remove(this);
        }


    }

}
