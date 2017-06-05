using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Controls;

namespace Glasses
{
    public class BrightnessGlass : Glass
    {
        HelligkeitPropsDialog bri;
        double Brightness;

        public BrightnessGlass()
        {
         
        }

        public double SGBrightness
        {
            get { return Brightness; }
            set { Brightness = value; }
        }

        public override void ShowPropsDialog(object sender, EventArgs e)
        {
            bri = new HelligkeitPropsDialog();
            bri.ShowDialog();
        }

        public override void Paint(PaintingLib.BitmapEditor painting)
        {
            
           
      
                painting.Lock();

               if(bri == null)
                {
                    this.SGBrightness = 2;
                    HelligkeitPropsDialog.valueOld = 2;

                } else
                {
                    this.SGBrightness = bri.Brightness;
                }

                

                Size size = CalcActualSize();
                Point childPos = this.TranslatePoint(new Point(), Parent as Canvas);
                int ox = (int)childPos.X, oy = (int)childPos.Y;
                Color c;


                for (int i = (int)this.Width - 1; i >= 0; i--)
                    for (int j = (int)this.Height - 1; j >= 0; j--)
                    {
                        c = painting.GetPixel((ox + i), (oy + j));
                        double cR = c.R * Brightness;
                        double cG = c.G * Brightness;
                        double cB = c.B * Brightness;
                        double cA = 50;


                        if (cR < 0) cR = 1;
                        if (cR > 255) cR = 255;

                        if (cG < 0) cG = 1;
                        if (cG > 255) cG = 255;

                        if (cB < 0) cB = 1;
                        if (cB > 255) cB = 255;

                        painting.SetPixel(ox + i, oy + j, Color.FromArgb((byte)cA, (byte)cR, (byte)cG, (byte)cB));

                    }


                painting.Unlock();
     
        }

   



    }
}
