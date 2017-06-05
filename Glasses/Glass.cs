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
   

    public class Glass :  PaintingLib.PainterBase
    {
        EventHandler Click;
        Color BorderColor = Color.FromRgb(0,0,0);
        double BorderWidth = 5;
        int ispressed = 0;

        public Glass()
        {
            this.Width = 100;
            this.Height = 100;
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
            this.MinWidth = 100;
            this.Margin = new Thickness(120, 80, 0, 0);
            
            

        }

        public int IsPressed
        {
            get { return ispressed; }
            set { ispressed = value; }
        }

        public int FocusBorderWidth
        {
            get { return (int)BorderWidth; }
            set { BorderWidth = value; }
        }


        public void PaintBorder()
        {
            this.BorderBrush = new SolidColorBrush(BorderColor);
            this.BorderThickness = new Thickness(BorderWidth);
            this.InvalidateVisual();
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
            menuItem2.Click += new RoutedEventHandler(HelligkeitProps_Remove);
            menuItem3.Header = "In den Hintergrund";
            menuItem4.Header = "In den Vordergrund";
            this.ContextMenu.Items.Add(menuItem4);
            this.ContextMenu.Items.Add(menuItem3);
            this.ContextMenu.Items.Add(menuItem2);
            this.ContextMenu.Items.Add(menuItem1);
        }



        public virtual void ShowPropsDialog(object sender, EventArgs e)
        {
            //HelligkeitPropsDialog bri = new HelligkeitPropsDialog();
            //bri.ShowDialog();
        }

        private void HelligkeitProps_Remove(object sender, EventArgs e)
        {
            Std_KMP_Glasses.main.canvasCanvas.Children.Remove(this);
        }

      

     



    }

}
