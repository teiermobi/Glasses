﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
//using System.Timers;
using System.Windows.Input;

namespace Glasses
{

    public class WaterGlass : Glass
    {
        WaterPropsDialog wa;

        // Aufruf des Eigenschaftendialog

        public override void ShowPropsDialog(object sender, EventArgs e)
        {
            wa = new WaterPropsDialog();
            wa.ShowDialog();
        }

        internal static DispatcherTimer timmy = new DispatcherTimer();

        double s, dd, d, dl;
        public enum WaterGlassType { Strudel, Welle };
        WaterGlassType type = WaterGlassType.Strudel; // Strudel Standardmäßig ausgewählt
        int iorg, jorg;
        Color c;


        // Initialisierung und setzen der Standardwerte 
        public WaterGlass()
        {
            timmy.Interval = new TimeSpan(0, 0, 0, 0, 40); //25 mal pro Sekunde
            timmy.Tick += Timmy_Tick;
            this.DistortionLimit = 80.0;
            this.Distortion = 0.0;
            this.DistortionDelta = 2.0;
            this.WaveDensity = 0.1;

            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) timmy.Start();
        }


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
            set { dl = value; }
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
                else
                {
                    s = 0;
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


        // Timer der den Distortion Wert zwischen den Grenzen hält
        private void Timmy_Tick(object sender, EventArgs e)
        {
            if (Distortion == DistortionLimit || Distortion == -DistortionLimit)
            {
                dd = -dd;
            }
            if (Distortion <= DistortionLimit && Distortion >= -DistortionLimit)
            {
                Distortion += dd;
            }

            InvalidateVisual();
        }

        // Paint Methode zum Zeichnen des Glases
        public override void Paint(PaintingLib.BitmapEditor painting)
        {

            painting.Lock();


            Size size = CalcActualSize();
            Point childPos = this.TranslatePoint(new Point(), Parent as PaintingLib.CanvasBase);
            int ox = (int)childPos.X, oy = (int)childPos.Y;



            int sX = (int)this.Width;
            int sY = (int)this.Height;

            int m = (int)this.Width;
            int n = (int)this.Height;


            if (WaterType == WaterGlassType.Welle)
            {
                for (int i = sX - 1; i >= 0; i--)
                {
                    for (int j = sY - 1; j >= 0; j--)
                    {

                        iorg = (int)((ox + i) + this.Distortion * Math.Cos((i + ox) * this.WaveDensity));
                        jorg = (int)((oy + j) + this.Distortion * Math.Sin((j + oy) * this.WaveDensity));

                        c = painting.GetPixel(iorg, jorg);

                        painting.SetPixel(ox + i, oy + j, c);

                    }
                }
            }

            else if (WaterType == WaterGlassType.Strudel)
            {
                for (int j = 0; j < sY; j++)
                {
                    for (int i = 0; i < sX; i++)
                    {
                        double ir = ((2.0 * i) / m) - 1;
                        double jr = ((2.0 * j) / n) - 1;
                        double l = Math.Sqrt(Math.Pow(ir, 2.0) + Math.Pow(jr, 2.0));
                        //double winkel = Math.Atan2(oy + j, ox + i);
                        double winkel = Math.Atan2( j-(n/2.0), i-(m/2.0));

                        if (l < 1.0)
                        {
                            iorg = (ox + i) + (int)(0.5 * m * (1 + l * Math.Cos((winkel + 0.1 * (l - 1) * Distortion))));
                            jorg = (oy + j) + (int)(0.5 * n * (1 + l * Math.Sin((winkel + 0.1 * (l - 1) * Distortion))));
                        }
                        else
                        {
                            iorg = (int)(0.5 * m * (1 + l * Math.Cos(winkel)));
                            jorg = (int)(0.5 * n * (1 + l * Math.Sin(winkel)));
                        }

                        c = painting.GetPixel(iorg, jorg);

                        painting.SetPixel(ox + i, oy + j, c);
                    }
                }
            }




            painting.Unlock();
        }
    }
}
