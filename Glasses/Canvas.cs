using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaintingLib;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;




namespace Glasses
{
    public class Canvas : PaintingLib.CanvasBase
    {
        public EventHandler<EventArgs> NewMouseEvent;
        public Image IMG;

        public Canvas()
        {
            this.MouseLeftButtonDown += new MouseButtonEventHandler(Canvas_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(Canvas_MouseLeftButtonUp);
            this.MouseMove += new MouseEventHandler(Canvas_MouseMove);

        }


        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            IEnumerable<Glass> Glasses = Std_KMP_Glasses.main.canvasCanvas.Children.OfType<Glass>();

            foreach (var glass in Glasses)
            {
                if (e.OriginalSource == glass)
                {
                    glass.IsPressed = 1;
                    glass.FocusBorderWidth = 5;
                    glass.PaintBorder();
                    glass.InvalidateVisual();
                }

            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {

            IEnumerable<Glass> Glasses = Std_KMP_Glasses.main.canvasCanvas.Children.OfType<Glass>();

            foreach (var glass in Glasses)
            {
                if (e.OriginalSource == glass && glass.IsPressed == 1)
                {
                    glass.Margin = new Thickness(e.GetPosition(glass).X + glass.Margin.Left - glass.ActualWidth / 2, e.GetPosition(glass).Y + glass.Margin.Top - glass.ActualHeight / 2, 0, 0);
                }
                //else if(glass.Margin.Left < e.GetPosition(this).X && e.GetPosition(this).X < glass.Margin.Left + glass.ActualWidth)
                //{
                   

                //}
                //else
                //{
                  
                //}

            }
        }


        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IEnumerable<Glass> Glasses = Std_KMP_Glasses.main.canvasCanvas.Children.OfType<Glass>();

            foreach (var glass in Glasses)
            {
                if (e.OriginalSource == glass)
                {
                    glass.IsPressed = 0;
                    if (Std_KMP_Glasses.main.checkBox.IsChecked ?? true)
                    {
                        glass.FocusBorderWidth = 2;
                    }
                    else
                    {
                        glass.FocusBorderWidth = 0;
                    }
                    glass.PaintBorder();
                    glass.InvalidateVisual();
                }


            }

        }

       

        public override void Paint(BitmapEditor painting)
        {

            base.Paint(painting);

            DrawingVisual dv = new DrawingVisual();
            DrawingContext dc = dv.RenderOpen();

            string bild;
            if (Std_KMP_Glasses.main.textboxSrc.Text == "Tulpen.jpg")
            {
                bild = "pack://application:,,,/Images/Tulpen.jpg";
            }

            else
            {
                bild = Std_KMP_Glasses.main.textboxSrc.Text;
            }
            IMG = new Image();
            IMG.Source = new BitmapImage(new Uri(bild));


            dc.DrawImage(IMG.Source, new Rect(0, 0, Std_KMP_Glasses.main.canvasCanvas.MinWidth, Std_KMP_Glasses.main.canvasCanvas.MinHeight));
            dc.Close();
            painting.Render(dv);

        }





    }
}
