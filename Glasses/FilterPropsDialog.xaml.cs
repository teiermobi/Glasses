using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Input;


namespace Glasses
{

    /// <summary>
    /// Interaktionslogik für FilterPropsDialog.xaml
    /// </summary>
    public partial class FilterPropsDialog : Window
    {
        private int filterindex;
        public static int intOld;

        public FilterPropsDialog(FilterGlass filterGlass)   
        {
            main = this;
            InitializeComponent();
            comboBoxFilter.SelectedIndex = intOld;      //gespeicherten ComboBox Item Index ausgeben
            this.OffsetDisp.Text = MaskLength.ToString();
        }

        internal static FilterPropsDialog main;
       


        public List<string> FilterSource
        {
            get
            {
                List<string> filterSource = new List<string>();
                filterSource.Add("Kontrast");
                filterSource.Add("Kanten");
                filterSource.Add("Benutzerdef.");
                filterSource.Add("Relief");
                return filterSource;
            }
            set
            {
                FilterSource = value;
            }
        }

        public int MaskLength { get { return (int)FilterGlass.main.Mask.GetLongLength(0); }}



        public void GenerateDefaultMatrix()         //Matrix für "Kanten" bzw. "Kontrast".
        {
            if (rasterGrid != null)
            {
                rasterGrid.Children.Clear();
                for (int i = 0; i < FilterGlass.main.Mask.GetLength(0); i++)
                {
                    for (int j = 0; j < FilterGlass.main.Mask.GetLength(1); j++)
                    {
                        rasterGrid.Rows = FilterGlass.main.Mask.GetLength(0);
                        rasterGrid.Columns = FilterGlass.main.Mask.GetLength(1);
                        double value = FilterGlass.main.Mask[i, j];
                        NumericTextBox tb = new NumericTextBox();
                        tb.Text = value.ToString();
                        tb.Margin = new Thickness(5, 5, 5, 5);
                        tb.HorizontalContentAlignment = HorizontalAlignment.Center;
                        tb.VerticalContentAlignment = VerticalAlignment.Center;
                        tb.Name = ("box"+i+j);
                        rasterGrid.Children.Add(tb);
                    }
                }
            }
        }

        

        public void GenerateMatrix(int N)        // Matrix erstellen in abhängigkeit von "N" 
        {
            if (rasterGrid != null)
            {
                rasterGrid.Children.Clear();
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        double value;
                        if(i < FilterGlass.main.Mask.GetLength(0) && j < FilterGlass.main.Mask.GetLength(1))
                        {
                             value = FilterGlass.main.Mask[i, j];
                        } else
                        {
                             value = 0;
                        }
                        
                        NumericTextBox ta = new NumericTextBox();
                        ta.Text = value.ToString();
                        ta.Margin = new Thickness(5, 5, 5, 5);
                        ta.HorizontalContentAlignment = HorizontalAlignment.Center;
                        ta.VerticalContentAlignment = VerticalAlignment.Center;
                        ta.Name = ("box" + i + j);
                        rasterGrid.Children.Add(ta);
                    }
                }
                rasterGrid.Columns = N;
                rasterGrid.Rows = N ;
            }
        }




     

        public int Filter_Index
        {
            get { return filterindex; }
            set { filterindex = value; }
        }


       

        private void comboBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (comboBoxFilter.SelectedIndex == 0)
            {
                filterindex = 0;
                intOld = comboBoxFilter.SelectedIndex;
                OffsetDisp.Text = "3";
                Std_KMP_Glasses.main.canvasCanvas.InvalidateVisual();
            }

            else if (comboBoxFilter.SelectedIndex == 1)
            {
                filterindex = 1;
                intOld = comboBoxFilter.SelectedIndex;
                OffsetDisp.Text = "3";
                Std_KMP_Glasses.main.canvasCanvas.InvalidateVisual();
            }
            else if (comboBoxFilter.SelectedIndex == 3)
            {
                filterindex = 3;
                intOld = comboBoxFilter.SelectedIndex;
                OffsetDisp.Text = "3";
                Std_KMP_Glasses.main.canvasCanvas.InvalidateVisual();
            }

        }

        private void OffsetDisp_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }



        private void Offset_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            if (e.NewValue > e.OldValue)
            {
                int Old = int.Parse(OffsetDisp.Text);
                Old -= 1;
                OffsetDisp.Text = Old.ToString();
                comboBoxFilter.SelectedIndex = 2;
                GenerateMatrix(Old);
            }
            else
            {
                int New = int.Parse(OffsetDisp.Text);
                New += 1;
                OffsetDisp.Text = New.ToString();
                comboBoxFilter.SelectedIndex = 2;
                GenerateMatrix(New);
            }

        }


    }
}
