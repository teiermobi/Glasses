﻿//Autoren: Meier, Kleber, Pawlowski
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Glasses
{
    public enum WaterGlassType { Strudel, Welle };

    public class WaterGlass : Glass
    {
        WaterPropsDialog waterPropsDia;
        double s; // Distortion
        double dd; // DistortionDelta
        double d; // WaveDensity
        double dl; // DistortionLimit
        WaterGlassType type = WaterGlassType.Welle;
        int i_orig, j_orig; //Ort der zukünftigen Farbe
        Color c; //zukünftige Farbe
        int dir = 1; //Richtung des DistortionDelta
        internal static DispatcherTimer timmy = new DispatcherTimer();

        // Eigenschaft Watertyp um die Wasserarten zu unterscheiden (siehe Angabe)
        public WaterGlassType WaterType
        {
            get { return type; }
            set { type = value; }
        }

        // Eigenschaft DistortionLimit (siehe Angabe)
        public double DistortionLimit
        {
            get { return dl; }
            set
            {
                if (value > 0)
                {
                    dl = value;
                    if (Distortion > dl)
                    {
                        Distortion = dl;
                    }
                    else if (Distortion < -dl)
                    {
                        Distortion = -dl;
                    }
                }
            }
        }

        // Eigenschaft Distortion (Wert darf nur zwischen positivem und negativem DistortionLimit liegen) (siehe ANgabe)
        public double Distortion
        {
            get { return (double)s; }
            set
            {
                if (value <= DistortionLimit && value >= -DistortionLimit) //DistortionLimit ist immer positiv
                {
                    s = value;
                }
                else if (value > DistortionLimit)
                {
                    s = DistortionLimit; //Sollte niemals passieren, per def.
                }
                else if (value < -DistortionLimit)
                {
                    s = -DistortionLimit;
                }

            }
        }

        // Eigenschaft DistortionDelta (siehe Angabe)
        public double DistortionDelta
        {
            get { return (double)dd; }
            set { dd = value; }
        }

        // Eigenschaft WaveDensity (siehe Angabe)
        public double WaveDensity
        {
            get { return (double)d; }
            set { d = value; }
        }

        // Initialisierung und setzen der Standardwerte 
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

        // Aufruf des Eigenschaftendialog
        public override void ShowPropsDialog(object sender, EventArgs e)
        {
            waterPropsDia = new WaterPropsDialog(this);
            waterPropsDia.ShowDialog();
        }

        // Paint Methode zum Zeichnen des Glases
        public override void Paint(PaintingLib.BitmapEditor painting)
        {
            //if (waterPropsDia != null)
            //{
                //type = waterPropsDia.type;
            //}

            Point childPos = this.TranslatePoint(new Point(), Parent as PaintingLib.CanvasBase);
            int ox = (int)childPos.X, oy = (int)childPos.Y;

            int m = (int)this.Width;
            int n = (int)this.Height;

            painting.Lock();

            //unbenutze Vorlage sichern
            System.Windows.Media.Imaging.WriteableBitmap originalBitmap = painting.Bitmap;
            PaintingLib.BitmapEditor originalEditor = new PaintingLib.BitmapEditor(originalBitmap);


            if (WaterType == WaterGlassType.Welle)
            {
                int global_x = 0;
                int global_y = 0;

                for ( int i = 0; i < m; i++ )
                {
                    for ( int j = 0; j < n; j++ )
                    {
                        //Globale Positionen
                        global_x = ox + i;
                        global_y = oy + j;
                        
                        //Herkunft
                        i_orig = (int)((global_x) + this.Distortion * Math.Cos((global_x) * this.WaveDensity));
                        j_orig = (int)((global_y) + this.Distortion * Math.Sin((global_y) * this.WaveDensity));

                        c = originalEditor.GetPixel(i_orig, j_orig); 

                        painting.SetPixel(global_x, global_y, c);
                    }
                }
            }

            else if (WaterType == WaterGlassType.Strudel)
            {
                //Hälfte der kleineren Dimension wird als Radius definiert 
                int rad = m < n ? (int)(m / 2.0) : (int)(n / 2.0);

                //Internes Koordinatensystem 
                for ( int j = rad; j > -rad; j--)
                {
                    for ( int i = -rad; i < rad; i++)
                    {
                        if ( i*i + j*j <= rad*rad ) //einfachere Prüfung für "Pixel im Kreis?"
                        {
                            double l = Math.Sqrt(i * i + j * j) / rad; //Abstand vom Mittelpunkt (einfach darstellbar durch internes Koord-syst.)
                            double angle_radians = Math.Atan2(j, i); //Winkel zum Mittelpunkt
                            double angle_degrees = ((angle_radians * 180.0) / Math.PI) + Distortion * 10.0 * (1 - l);
                            angle_radians = (angle_degrees * Math.PI) / 180.0;

                            //Ursprünge der neuen Farben
                            i_orig = (int)(l * rad * Math.Cos(angle_radians));
                            j_orig = (int)(l * rad * Math.Sin(angle_radians));

                            //Abholen der Farbe im globalen Koordianatensystem
                            c = originalEditor.GetPixel(ox + (int)(m / 2.0) + i_orig, oy + (int)(n/2.0) - j_orig);
                            painting.SetPixel(ox + (int)(m / 2.0) + i, oy + (int)(n / 2.0) - j, c);
                        }
                    }
                }
            }
            painting.Unlock();
        }

        // Timer der den Distortion Wert zwischen den Grenzen hält
        private void Timmy_Tick(object sender, EventArgs e)
        {
            if (Distortion + dd * dir > DistortionLimit || Distortion + dd * dir < -DistortionLimit)
            {
                dir = -dir;
            }

            Distortion += dd * dir;

            InvalidateVisual();
        }
    }
}
