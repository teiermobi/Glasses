﻿using System;
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
        public string ISource;

        public Canvas()
        {
            this.MouseLeftButtonDown += new MouseButtonEventHandler(Canvas_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(Canvas_MouseLeftButtonUp);
            this.MouseMove += new MouseEventHandler(Canvas_MouseMove);
            this.ImageSource = "pack://application:,,,/Images/Tulpen.jpg";
        }



        private Point MouseDownLocation;
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            IEnumerable<Glass> Glasses = Std_KMP_Glasses.main.canvasCanvas.Children.OfType<Glass>();

            foreach (var glass in Glasses)
            {
                
                if (e.OriginalSource == glass)
                {
                    MouseDownLocation = new Point(e.GetPosition(glass).X, e.GetPosition(glass).Y);
                    glass.FocusBorderColor = Color.FromRgb(255, 30, 50);
                    glass.FocusBorderWidth = 5;
                    glass.PaintBorder();
                    glass.InvalidateVisual();
                }

            }
        }


            // Wenn Maus bewegt wird (innerhalb dann nur wenn zusätzlich auch die Maustaste gedrückt ist)

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {

            IEnumerable<Glass> Glasses = Std_KMP_Glasses.main.canvasCanvas.Children.OfType<Glass>();
            foreach (var glass in Glasses)
            {

                // Glass verschieben

                if (e.OriginalSource == glass && e.LeftButton == MouseButtonState.Pressed)
                {
                    glass.Margin = new Thickness(e.GetPosition(glass).X + glass.Margin.Left - MouseDownLocation.X, e.GetPosition(glass).Y + glass.Margin.Top - MouseDownLocation.Y, 0, 0);
                }

                //// Glass skalieren

                //else if (glass.Margin.Left + glass.ActualWidth - 5 <= e.GetPosition(this).X && glass.Margin.Left + glass.ActualWidth  >= e.GetPosition(this).X || glass.Margin.Top + glass.ActualHeight - 5 <= e.GetPosition(this).Y && glass.Margin.Top + glass.ActualHeight  >= e.GetPosition(this).Y)
                //{
                //    this.Cursor = Cursors.SizeNWSE;
                //    if (e.LeftButton == MouseButtonState.Pressed && this.Cursor == Cursors.SizeNWSE)
                //    {
                //        glass.PaintBorder();

                //        if (e.GetPosition(this).Y - glass.Margin.Top > 0 && e.GetPosition(this).X - glass.Margin.Left > 0)
                //        {
                //            glass.Height = e.GetPosition(this).Y - glass.Margin.Top;
                //            glass.Width = e.GetPosition(this).X - glass.Margin.Left;
                //        }
                //        else
                //        {

                //        }
                //        glass.InvalidateVisual();
                //    }
                //    else
                //    {


                //    }

                //}
                else
                {
                     this.Cursor = Cursors.Arrow;
                }

            }
        }


        // Linke Maustaste losgelassen

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IEnumerable<Glass> Glasses = Std_KMP_Glasses.main.canvasCanvas.Children.OfType<Glass>();

            foreach (var glass in Glasses)
            {
                
                    if (Std_KMP_Glasses.main.checkBox.IsChecked ?? true)
                    {
                        glass.FocusBorderColor = Color.FromRgb(0, 0, 0);
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

        public string ImageSource
        {
            get { return ISource; }
            set { ISource = value; }
        }

       
        // Überrschriebene Paint-Methode um das Bild zu zeichnen

        public override void Paint(BitmapEditor painting)
        {

            base.Paint(painting);

            DrawingVisual dv = new DrawingVisual();
            DrawingContext dc = dv.RenderOpen();

            string bild;
            if (Std_KMP_Glasses.main.textboxSrc.Text == "Tulpen.jpg")
            {
                bild = this.ImageSource; // "pack://application:,,,/Images/Tulpen.jpg";
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
