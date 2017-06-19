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
        BrightnessPropsDialog bri;
        double Brightness;

        // Konstruktor
        public BrightnessGlass()
        {
         
        }

        // Helligkeitswert Eigenschaft
        public double SGBrightness
        {
            get { return Brightness; }
            set { Brightness = value; }
        }

        // Aufruf des Eigenschaftendialogs
        public override void ShowPropsDialog(object sender, EventArgs e)
        {
            bri = new BrightnessPropsDialog();
            bri.ShowDialog();
        }


        // Überschriebene Paint-Methode
        public override void Paint(PaintingLib.BitmapEditor painting)
        {

               painting.Lock();
               // Check ob es einen "alten" Wert gibt
               if(bri == null)
                {
                    this.SGBrightness = 2;
                    BrightnessPropsDialog.valueOld = 2;

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
                        double cR = c.R * Brightness; // Rot-Kanal mit Heligkeit multiplizieren
                        double cG = c.G * Brightness; // Grün-Kanal mit Heligkeit multiplizieren
                        double cB = c.B * Brightness; // Blau-Kanal mit Heligkeit multiplizieren
                        double cA = 50;

                        // Farbwerte sollen sich für die jeweiligen Kanäle nur zwischen 0 und 255 bewegen
                        if (cR < 0) cR = 1;
                        if (cR > 255) cR = 255;

                        if (cG < 0) cG = 1;
                        if (cG > 255) cG = 255;

                        if (cB < 0) cB = 1;
                        if (cB > 255) cB = 255;

                        // Pixel neu einfärben 
                        painting.SetPixel(ox + i, oy + j, Color.FromArgb((byte)cA, (byte)cR, (byte)cG, (byte)cB));

                }
           

            painting.Unlock();
     
        }

   



    }
}
