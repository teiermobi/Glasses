using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Input;

namespace Glasses
{

    public class WaterGlass : Glass
    {


        internal static DispatcherTimer timmy = new DispatcherTimer();

        public WaterGlass()
        {
            timmy.Interval = new TimeSpan(0, 0, 0, 0, 24);
            timmy.Tick += Timmy_Tick;
            this.Distortion = 4;

            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) timmy.Start();
        }

     

        internal static double delta = 0.0;
        public double distortion;

        public double Distortion
        {
            get { return (double)distortion; }
            set { distortion = value; }
        }



        private void Timmy_Tick(object sender, EventArgs e)
        {
            delta++;
            
            InvalidateVisual();
        }


        public override void Paint(PaintingLib.BitmapEditor painting)
        {
           
       
                painting.Lock();
                
                
                Size size = CalcActualSize();
                Point childPos = this.TranslatePoint(new Point(), Parent as PaintingLib.CanvasBase);
                int ox = (int)childPos.X, oy = (int)childPos.Y;
                int sX = (int)this.Width;
                int sY = (int)this.Height;
                
                Color c;



                for (int i = sX - 1; i >= 0; i--)
                        for (int j = sY - 1; j >= 0; j--)
                        {
                            int iorg = ox + i + ((int)this.Distortion * (int)Math.Cos(ox + i * 200));
                            int jorg = oy + j + ((int)this.Distortion * (int)Math.Sin(oy + j * 200));
                            c = painting.GetPixel((ox + i), (oy + j));
                            double cR = c.R;
                            double cG = c.G;
                            double cB = c.B;
                            //if (delta <= this.Width)
                            painting.SetPixel(ox  + i , oy  + j, painting.GetPixel(iorg , jorg));  //+(int)jorg + (int)iorg

                }

               
                   painting.Unlock();
                } 
        

    }
}
