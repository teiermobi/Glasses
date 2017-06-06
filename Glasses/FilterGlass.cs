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
        double[,] mask;



        public FilterGlass()
        {
            this.Mask = new double[,] { { -1, 0, -1 }, { 0, 2, 0 }, { -1, 0, -1 } };
        }


        //private double[,] Mask = new double[3, 3];

        //access array through indexer /*https://www.daniweb.com/programming/software-development/threads/301123/accessors-for-an-multidemensional-array */

        public double[,] Mask
        {
            get { return  mask; }
            set { mask = value; }
        }


        public override void ShowPropsDialog(object sender, EventArgs e)
        {
            fi = new FilterPropsDialog();
            fi.ShowDialog();

        }

        public override void Paint(PaintingLib.BitmapEditor painting)
        {

            painting.Lock();
            Size size = CalcActualSize();
            Point childPos = this.TranslatePoint(new Point(), Parent as PaintingLib.CanvasBase);
            int ox = (int)childPos.X, oy = (int)childPos.Y;
            Color c;
            double blue = 0;
            double green = 0;
            double red = 0;


            for (int i = (int)this.Width - 1; i >= 0; i--)
                for (int j = (int)this.Height - 1; j >= 0; j--)
                {
                    c = painting.GetPixel((ox + i), (oy + j));

                    // Matrixgröße abfragen
                    for (int column = 0; column < Mask.GetLength(1); column++)  
                    {
                        for (int row = 0; row < Mask.GetLength(0); row++)        
                        {
                            
                             red = c.R * Mask[row, column];
                             green = c.G * Mask[row, column];
                             blue = c.B * Mask[row, column];
                             //cA = c.A * mask[row, column];
                        }
                       
                    }
                    //if (blue > 255)
                    //{ blue = 255; }
                    //else if (blue < 0)
                    //{ blue = 0; }


                    //if (green > 255)
                    //{ green = 255; }
                    //else if (green < 0)
                    //{ green = 0; }


                    //if (red > 255)
                    //{ red = 255; }
                    //else if (red < 0)
                    //{ red = 0; }
                    painting.SetPixel(ox + i, oy + j, Color.FromRgb((byte)red, (byte)green, (byte)blue));
                }
            
            painting.Unlock();
        }


    }
}