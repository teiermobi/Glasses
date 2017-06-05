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
            timmy.Interval = new TimeSpan(0, 0, 0, 0, 40);
            timmy.Tick += Timmy_Tick;

            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) timmy.Start();
        }

     

        internal static double delta = 0.0;
        internal static int newDelta;



        private void Timmy_Tick(object sender, EventArgs e)
        {
            delta++;
            delta = 1 + 5 * Math.Cos(delta);
            newDelta = (int)delta;
            InvalidateVisual();
        }


        public override void Paint(PaintingLib.BitmapEditor painting)
        {
           
       
                painting.Lock();

                Size size = CalcActualSize();
                Point childPos = this.TranslatePoint(new Point(), Parent as PaintingLib.CanvasBase);
                int ox = (int)childPos.X, oy = (int)childPos.Y;
                int sX = 100;
                int sY = 100;

                

                for (int i = sX - 1; i >= 0; i--)
                    for (int j = sY - 1; j >= 0; j--)
                    {  
                            painting.SetPixel(ox + i, oy + j, painting.GetPixel(ox + i + newDelta, oy + j + newDelta));
                    }

               
               painting.Unlock();
            } 
        

    }
}
