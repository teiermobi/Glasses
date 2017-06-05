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
    public class FilterGlass : Glass
    {
        FilterPropsDialog fi;

        public FilterGlass()
        {

        }


        public static double[,] Mask
        {
            get
            {
                return new double[,]
                                     { { 1, 2, 1, },
                                       { 2, 4, 2, },
                                       { 1, 2, 1, }, };
            }

            set { Mask = value; }
        }


        public override void ShowPropsDialog(object sender, EventArgs e)
        {
            fi = new FilterPropsDialog();
            fi.ShowDialog();
        }

        public override void Paint(PaintingLib.BitmapEditor painting)
        {
            
       
                
                painting.Lock();
                double[] Mask1 = new double[3];
                Mask1[0] = 1;
                Mask1[1] = 0;
                Mask1[2] = 2;

                Size size = CalcActualSize();
                Point childPos = this.TranslatePoint(new Point(), Parent as PaintingLib.CanvasBase);
                int ox = (int)childPos.X, oy = (int)childPos.Y;
                Color c;


                for (int i = 100 - 1; i >= 0; i--)
                    for (int j = 100 - 1; j >= 0; j--)
                    {
                        c = painting.GetPixel((ox + i), (oy + j));
                        for(int m = 0; m <= 3; m++)
                        {
                            double cR = c.R * -1;
                            double cG = c.G * 2;
                            double cB = c.B * 1;
                            painting.SetPixel(ox + i, oy + j, Color.FromRgb((byte)cR, (byte)cG, (byte)cB));
                    }
                        

                        //if (cR < 0) cR = 1;
                        //if (cR > 255) cR = 255;

                        //if (cG < 0) cG = 1;
                        //if (cG > 255) cG = 255;

                        //if (cB < 0) cB = 1;
                        //if (cB > 255) cB = 255;

                        

                    }


                painting.Unlock();
          
        }

   



    }
}
