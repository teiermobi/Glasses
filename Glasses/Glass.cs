using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Controls;

namespace Glasses
{
   

    public class Glass : PaintingLib.PainterBase
    {
      //  EventHandler Click;
        Color BorderColor = Color.FromRgb(0,0,0);
        double BorderWidth = 5;
        int ispressed = 0;
        int isscaling = 0;
 

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


        public Color FocusBorderColor
        {
            get { return (Color)BorderColor; }
            set { BorderColor = value; }
        }

        public int FocusBorderWidth
        {
            get { return (int)BorderWidth; }
            set { BorderWidth = value; }
        }


        public int IsPressed
        {
            get { return ispressed; }
            set { ispressed = value; }
        }

        public int IsScaling
        {
            get { return isscaling; }
            set { isscaling = value; }
        }

      


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

        public void showContextmenu()
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


        private void Change_ZindexMinus(object sender, EventArgs e)
        {
            int ind = Canvas.GetZIndex(this);
            Canvas.SetZIndex(this, ind - 1 );
        }

        private void Change_ZindexPlus(object sender, EventArgs e)
        {
            int ind = Canvas.GetZIndex(this);
            Canvas.SetZIndex(this, ind + 1);
        }


        public virtual void ShowPropsDialog(object sender, EventArgs e)
        {
            
        }

        private void Props_Remove(object sender, EventArgs e)
        {
            Std_KMP_Glasses.main.canvasCanvas.Children.Remove(this);
        }


    }

}
