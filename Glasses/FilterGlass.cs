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


        private double[,] Mask = new double[3, 3];

        //access array through indexer /*https://www.daniweb.com/programming/software-development/threads/301123/accessors-for-an-multidemensional-array */
        public double this[int x, int y]
        {
            get { return Mask[x, y]; }
            set { Mask[x, y] = value; }
        }


        public override void ShowPropsDialog(object sender, EventArgs e)
        {
            fi = new FilterPropsDialog();
            fi.ShowDialog();

        }

        public override void Paint(PaintingLib.BitmapEditor painting)
        {

            painting.Lock();

            // Mask =  { { -3, -1}, {2, -2 } };

            int[,] array2D2 = { { 1, 2, 3 }, { 4, 5, 6 } }; //Hier noch den Variablen Wert einbauen anstatt "3"


            Size size = CalcActualSize();
            Point childPos = this.TranslatePoint(new Point(), Parent as PaintingLib.CanvasBase);
            int ox = (int)childPos.X, oy = (int)childPos.Y;
            Color c;

            //Jeweilige Pixel des Fensters
            for (int i = (int)this.Width - 1; i >= 0; i--)
                for (int j = (int)this.Height - 1; j >= 0; j--)
                {
                    c = painting.GetPixel((ox + i), (oy + j));
                    for (int m = 0; m <= 3; m++)
                    {
                        //Matrixgröße abfragen
                        for (int column = 0; column < 2; column++) //Hier noch den Variablen Wert einbauen anstatt "3"
                        {
                            for (int row = 0; row < 2; row++) //Hier noch den Variablen Wert einbauen anstatt "3"
                            {
                                double cR = c.R * array2D2[row, column];
                                double cG = c.G * array2D2[row, column];
                                double cB = c.B * array2D2[row, column];
                                painting.SetPixel(ox + i, oy + j, Color.FromRgb((byte)cR, (byte)cG, (byte)cB));
                            }
                        }
                    }
                    painting.Unlock();
                }
        }


    }
}