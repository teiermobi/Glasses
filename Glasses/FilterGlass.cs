//Autoren: Meier, Kleber, Pawlowski
using System;
using System.Windows;
using System.Windows.Media;

namespace Glasses
{
    public class FilterGlass : Glass
    {
        FilterPropsDialog fi;
        public double[,] mask;


        // Initialisierung, Setzen der StandardMaske
        public FilterGlass()
        {
            main = this;
            this.Mask = new double[,] { { -1, 0, -1 }, { 0, 5, 0 }, { -1, 0, -1 } };
            
        }

        internal static FilterGlass main;

        // Masken Eigenschaft
        public double[,] Mask
        {
            get { return  mask; }
            set { mask = value; }
        }

        public void ResizeArray(ref double[,] original, int cols)
        {
            // Neues 2 Dimensionales Array
            // Größe unserer Wahl
            double[,] newArray = new double[cols, cols];
            //Alten Arrayinhalt in neues Array kopieren
            Array.Copy(original, newArray, original.Length);
            original = newArray;
        }

        // Eigenschaftendialog aufrufen
        public override void ShowPropsDialog(object sender, EventArgs e)
        {
            fi = new FilterPropsDialog( this );
            
            fi.ShowDialog();
           
        }


        // Paint Methode zum Zeichnen des Glases
        public override void Paint(PaintingLib.BitmapEditor painting)
        {
            painting.Lock();

            if (fi == null)
            {
                FilterPropsDialog.intOld = 0;
            }
            else
            {
                // Maske setzen je nachdem welcher Eintrag des Dropdwons ausgewählt wurde
                if (fi.Filter_Index == 0)
                {
                    this.Mask = new double[,] { { -1, 0, -1  }, { 0, 5, 0 }, { -1, 0, -1} };
                    fi.GenerateDefaultMatrix();
                   
                }
                else if (fi.Filter_Index == 1)
                {
                    this.Mask = new double[,] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
                    fi.GenerateDefaultMatrix();
                }
                else if (fi.Filter_Index == 3)
                {
                    this.Mask = new double[,] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
                    fi.GenerateDefaultMatrix();
                }
            }
            

            Size size = CalcActualSize();
            Point childPos = this.TranslatePoint(new Point(), Parent as PaintingLib.CanvasBase);
            int ox = (int)childPos.X, oy = (int)childPos.Y;

            Color[,] result = new Color[(int)this.Width, (int)this.Height];
            Color imageColor;

       
            for (int i = (int)this.Width - 1; i >= 0; i--)
            {
                for (int j = (int)this.Height - 1; j >= 0; j--)
                {
                    double red = 0.0;
                    double green = 0.0;
                    double blue = 0.0;

                    for (int filterX = 0; filterX < Mask.GetLength(1); filterX++)
                    {
                        for (int filterY = 0; filterY < Mask.GetLength(0); filterY++)
                        {
                            int imageX = (i - Mask.GetLength(1) / 2 + filterX + (int)this.Width) % (int)this.Width;
                            int imageY = (j - Mask.GetLength(0) / 2 + filterY + (int)this.Height) % (int)this.Height;

                            imageColor = painting.GetPixel(ox + imageX, oy + imageY);

                            // Jeweiligen Farbkanal mit Maske multiplizieren
                            red += imageColor.R * Mask[filterX, filterY];
                            green += imageColor.G * Mask[filterX, filterY];
                            blue += imageColor.B * Mask[filterX, filterY];
                           
                        }                      
                    }
                    int r = Math.Min(Math.Max((int)(red), 0), 255);
                    int g = Math.Min(Math.Max((int)(green), 0), 255);
                    int b = Math.Min(Math.Max((int)(blue), 0), 255);
                    result[i, j] = Color.FromRgb((byte)r, (byte)g, (byte)b);
                 
                }
               
            }
            for (int k = 0; k < (int)this.Width; ++k)
            {
                for (int l = 0; l < (int)this.Height; ++l)
                {
                  painting.SetPixel(ox + k, oy + l, result[k, l]);
                }
            }
            painting.Unlock();
        }
    }
}