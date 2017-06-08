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
    //test
    public class WaterGlass : Glass
    {


        internal static DispatcherTimer timmy = new DispatcherTimer();

        internal static double delta = 0.0;
        double s, dd, d, dl;
        enum GlassWaserType { Strudel, Welle };
        int typ = 0;
        int iorg, jorg;
        Color c;

        public WaterGlass()
        {
            timmy.Interval = new TimeSpan(0, 0, 0, 0, 24);
            timmy.Tick += Timmy_Tick;
            this.Distortion = 31;
            this.DistortionDelta = 0;
            this.DistortionLimit = 80;
            this.WaveDensity = 0.3;

            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) timmy.Start();
        }



        public int WaterTyp
        {
            get { return  typ; }
            set { typ = value; }
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
                if(this.DistortionLimit > value && value > -Math.Abs(this.DistortionLimit))
                {
                    s = value;
                } else
                {
                    s = 80;
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
                
                



            for (int i = sX - 1; i >= 0; i--)
            {
                    for (int j = sY - 1; j >= 0; j--)
                    {
                            
                    iorg = (ox + i) + (int)this.Distortion *  (int)Math.Cos((int)this.Distortion  + (i + ox)  * this.WaveDensity);
                    jorg = (oy + j) + (int)this.Distortion *  (int)Math.Sin((int)this.Distortion  + (j + oy)  * this.WaveDensity);

                    Point childPosA = this.TranslatePoint(new Point(iorg,jorg), Parent as PaintingLib.CanvasBase);
                    int oa = (int)childPosA.X, ob = (int)childPosA.Y;

                    c = painting.GetPixel(oa + i, ob + j);

                    //painting.SetPixel(ox + iorg, oy + jorg, Color.FromRgb((byte)c.R, (byte)c.G, (byte)c.G));
                    //painting.SetPixel(ox + i, oy + j, painting.GetPixel(iorg + i + (int)delta, jorg + j + (int)delta));
                    painting.SetPixel(ox + i, oy + j, Color.FromRgb((byte)c.R, (byte)c.G, (byte)c.B));

                    }
            }


            


            painting.Unlock();
        } 
        

    }
}
