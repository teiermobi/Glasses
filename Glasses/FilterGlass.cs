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


        public  double[,] Mask
        {
            get { return Mask; }

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

            Mask =  {
                { -3, -1},
                        { 2, -2 }
            };




            Size size = CalcActualSize();
                    Point childPos = this.TranslatePoint(new Point(), Parent as PaintingLib.CanvasBase);
                    int ox = (int)childPos.X, oy = (int)childPos.Y;
                    Color c;


                for (int i = (int)this.Width - 1; i >= 0; i--)
                    for (int j = (int)this.Height - 1; j >= 0; j--)
                    {
                        c = painting.GetPixel((ox + i), (oy + j));
                        for(int m = 0; m <= 3; m++)
                        {
                            for(int s = 0; s <= Mask.Length; s++)
                            {
                                double cR = c.R * Mask[s,s];
                                double cG = c.G * Mask[s,s];
                                double cB = c.B * Mask[s,s];
                                painting.SetPixel(ox + i, oy + j, Color.FromRgb((byte)cR, (byte)cG, (byte)cB));
                            }
                            
                    }
                        

                     
                    }


                painting.Unlock();
          
        }

   



    }
}
