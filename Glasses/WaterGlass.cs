using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
//using System.Timers;
using System.Windows.Input;

namespace Glasses
{
    
    public class WaterGlass : Glass
    {


        internal static DispatcherTimer timmy = new DispatcherTimer();

        double s, dd, d, dl;
        public enum WaterGlassType { Strudel, Welle };
        WaterGlassType type = WaterGlassType.Welle;
        int iorg, jorg;
        Color c;

        public WaterGlass()
        {
            timmy.Interval = new TimeSpan(0, 0, 0, 0, 40); //25 mal pro Sekunde
            timmy.Tick += Timmy_Tick;
            this.DistortionLimit = 80.0;
            this.Distortion = 0.0;
            this.DistortionDelta = 1.0;
            this.WaveDensity = 0.1;

            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) timmy.Start();
        }



        public WaterGlassType WaterType
        {
            get { return type; }
            set { type = value; }
        }

        public double DistortionLimit
        {
            get { return dl; }
            set { dl = value; }
        }

        public double Distortion
        {
            get { return (double)s; }
            set
            {
                if( value <= DistortionLimit && value >= -DistortionLimit) //DistortionLimit ist immer positiv
                {
                    s = value;
                }
                else
                {
                    s = 0;
                }
                
            }
        }

        public double DistortionDelta
        {
            get { return (double)dd; }
            set { dd = value; }
        }

        public double WaveDensity
        {
            get { return (double)d; }
            set { d = value; }
        }



        private void Timmy_Tick(object sender, EventArgs e)
        {
            if (Distortion == DistortionLimit || Distortion == -DistortionLimit)
            {
                dd = -dd;
            }
            if (Distortion <= DistortionLimit && Distortion >= -DistortionLimit)
            {
                Distortion += dd;
            }

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





            if (WaterType == WaterGlassType.Welle)
            {
                for (int i = sX - 1; i >= 0; i--)
                {
                    for (int j = sY - 1; j >= 0; j--)
                    {

                        iorg = (int)((ox + i) + this.Distortion * Math.Cos((i + ox) * this.WaveDensity));
                        jorg = (int)((oy + j) + this.Distortion * Math.Sin((j + oy) * this.WaveDensity));

                        c = painting.GetPixel( iorg, jorg );

                        painting.SetPixel(ox + i, oy + j, c);

                    }
                }
            }

            


            painting.Unlock();
        } 
        

    }
}
