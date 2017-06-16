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
        private string filterName;

        public FilterPropsDialog()
        {
            InitializeComponent();

            comboBoxFilter.SelectedIndex = intOld;  //gespeicherten ComboBox Item Index ausgeben

            this.OffsetDisp.Text = MaskLength.ToString();
            GenerateMatrix(MaskLength);
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

        public int MaskLength { get { return (int)FilterGlass.main.Mask.GetLongLength(0); } }

        public void GenerateMatrix(int N)
        {
            if (rasterGrid != null)
            {
                rasterGrid.Children.Clear();
                for (int i = 0; i < FilterGlass.main.Mask.GetLength(0); i++)
                {
                    for (int j = 0; j < FilterGlass.main.Mask.GetLength(1); j++)
                    {
                        double value = FilterGlass.main.Mask[i, j];
                        TextBox tb = new TextBox();
                        tb.Text = value.ToString();
                        tb.Margin = new Thickness(5, 5, 5, 5);
                        tb.HorizontalContentAlignment = HorizontalAlignment.Center;
                        tb.VerticalContentAlignment = VerticalAlignment.Center;
                        // tb.Name = i+"_"+j;
                        rasterGrid.Children.Add(tb);
                    }
                }
            }
        }


        public void GetMask()
        {
            //IEnumerable<Glass> Glasses = FilterPropsDialog.main.Children.OfType<Glass>();
            //foreach (var glass in Glasses)
            //{
            //}
        }

        public string Filter_Name
        {
            get { return filterName; }
            set { filterName = value; }
        }


        public static int intOld;

        private void comboBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxFilter.SelectedItem.ToString() == "Kontrast")
            {
                filterName = "Kontrast";
                intOld = comboBoxFilter.SelectedIndex;
                GenerateMatrix(MaskLength);
                Std_KMP_Glasses.main.canvasCanvas.InvalidateVisual();
                InvalidateVisual();
            }

            else if (comboBoxFilter.SelectedItem.ToString() == "Kanten")
            {
                filterName = "Kanten";
                intOld = comboBoxFilter.SelectedIndex;
                GenerateMatrix(MaskLength);
                Std_KMP_Glasses.main.canvasCanvas.InvalidateVisual();
                InvalidateVisual();
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
                GenerateMatrix(old);
            }
            else
            {
                int New = int.Parse(OffsetDisp.Text);
                New += 1;
                OffsetDisp.Text = New.ToString();
                GenerateMatrix(New);
            }
        }


    }
}
