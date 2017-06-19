using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Glasses
{

    /// <summary>
    /// Interaktionslogik für FilterPropsDialog.xaml
    /// </summary>
    public partial class FilterPropsDialog : Window
    {
        private int filterindex;

        public FilterPropsDialog(FilterGlass filterGlass)    // Damit man auf die Werte von FilterGlass zugreifen kann "filterGlass."
        {
           
            InitializeComponent();
            comboBoxFilter.SelectedIndex = intOld;  //gespeicherten ComboBox Item Index ausgeben
            this.OffsetDisp.Text = MaskLength.ToString();
            GenerateMatrix(5);
        }

        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }


        public List<string> FilterSource
        {
            get
            {
                List<string> filterSource = new List<string>();
                filterSource.Add("Kontrast");
                filterSource.Add("Kanten");
                filterSource.Add("Benutzerdef.");
                return filterSource;
            }
            set
            {
                FilterSource = value;
            }
        }

        public int MaskLength { get { return (int)FilterGlass.main.Mask.GetLongLength(0); }}

        public double MaskValue { get { return (int)FilterGlass.main.Mask.GetValue(0); } }


        public void GenerateMatrix(int N)
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
                        TextBox tb = new TextBox();
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

        public void GenerateDefaultMatrix(int N)
        {
            if (rasterGrid != null)
            {
                rasterGrid.Children.Clear();
                
                for (int i = 0; i <= N; i++)
                {
                    for (int j = 0; j <= N; j++)
                    {
                        double value = 0;
                        TextBox tb = new TextBox();
                        tb.Text = value.ToString();
                        tb.Margin = new Thickness(5, 5, 5, 5);
                        tb.HorizontalContentAlignment = HorizontalAlignment.Center;
                        tb.VerticalContentAlignment = VerticalAlignment.Center;
                        tb.Name = ("box" + i + j);
                        rasterGrid.Children.Add(tb);
                    }
                }
                //rasterGrid.Columns = N;
                //rasterGrid.Rows = N ;
            }
        }




        public void GetMask()
        {
            //IEnumerable<Glass> Glasses = FilterPropsDialog.main.Children.OfType<Glass>();
            //foreach (var glass in Glasses)
            //{
            //}
        }

        public int Filter_Index
        {
            get { return filterindex; }
            set { filterindex = value; }
        }


        public static int intOld;

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
           
        }

        private void OffsetDisp_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }



        private void Offset_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            if (e.NewValue > e.OldValue)
            {
                int old = int.Parse(OffsetDisp.Text);
                old -= 1;
                OffsetDisp.Text = old.ToString();
                comboBoxFilter.SelectedIndex = 2;
                GenerateDefaultMatrix(old);
            }
            else
            {
                int New = int.Parse(OffsetDisp.Text);
                New += 1;
                OffsetDisp.Text = New.ToString();
                comboBoxFilter.SelectedIndex = 2;
                GenerateDefaultMatrix(New);
            }

        }


    }
}
