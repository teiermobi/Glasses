using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Glasses
{
    class ZoomGlass : Glass
    {
       // ZoomPropsDialog zm;
        double Zoom;

        // Konstruktor
        public ZoomGlass()
        {

        }

        // Zoomwert Eigenschaft
        public double SGZomm
        {
            get { return Zoom; }
            set { Zoom = value; }
        }

        // Aufruf des Eigenschaftendialogs
        public override void ShowPropsDialog(object sender, EventArgs e)
        {
            //bri = new ZoomPropsDialog();
            //bri.ShowDialog();
        }


        // Überschriebene Paint-Methode
        public override void Paint(PaintingLib.BitmapEditor painting)
        {

            painting.Lock();
            // Check ob es einen "alten" Wert gibt
            //if (zm == null)
            //{
            //    this.SGZomm = 2;
            //    ZoomPropsDialog.valueOld = 2;

            //}
            //else
            //{
            //    this.SGZomm = zm.Zoom;
            //}



            Size size = CalcActualSize();
            Point childPos = this.TranslatePoint(new Point(), Parent as Canvas);
            int ox = (int)childPos.X, oy = (int)childPos.Y;
            Color c;


            for (int i = (int)this.Width - 1; i >= 0; i--)
                for (int j = (int)this.Height - 1; j >= 0; j--)
                {
                    c = painting.GetPixel((ox + i), (oy + j));
                    //double cR = c.R * Zomm; // Rot-Kanal mit Heligkeit multiplizieren
                    //double cG = c.G * Zomm; // Grün-Kanal mit Heligkeit multiplizieren
                    //double cB = c.B * Zomm; // Blau-Kanal mit Heligkeit multiplizieren
                    //double cA = 50;

                    double cR = c.R;
                    double cG = c.G;
                    double cB = c.B;

                    // Farbwerte sollen sich für die jeweiligen Kanäle nur zwischen 0 und 255 bewegen
                    if (cR < 0) cR = 1;
                    if (cR > 255) cR = 255;

                    if (cG < 0) cG = 1;
                    if (cG > 255) cG = 255;

                    if (cB < 0) cB = 1;
                    if (cB > 255) cB = 255;

                    // Pixel neu einfärben 
                    painting.SetPixel(ox + i, oy + j, Color.FromRgb((byte)cR, (byte)cG, (byte)cB));

                }


            painting.Unlock();

        }
    }
}
