using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Web;

namespace Glasses
{
    /// <summary>
    /// Interaktionslogik für FilterPropsDialog.xaml
    /// </summary>
    public partial class FilterPropsDialog : Window
    {
        public FilterPropsDialog()
        {
            //
            InitializeComponent();
            main = this;
            comboBoxFilter.SelectedIndex = intOld;  //gespeicherten ComboBox Item Index ausgeben
            this.OffsetDisp.Text = "3";





            //IEnumerable<TextBox> TextBoxes = this.rasterGrid.Children.OfType<TextBox>();


            //Grid myGrid = new Grid();
            //myGrid.Width = 250;
            //myGrid.Height = 100;
            //myGrid.HorizontalAlignment = HorizontalAlignment.Left;
            //myGrid.VerticalAlignment = VerticalAlignment.Top;
            //myGrid.ShowGridLines = true;

            //// Define the Columns
            //ColumnDefinition colDef1 = new ColumnDefinition();
            //ColumnDefinition colDef2 = new ColumnDefinition();
            //ColumnDefinition colDef3 = new ColumnDefinition();
            //myGrid.ColumnDefinitions.Add(colDef1);
            //myGrid.ColumnDefinitions.Add(colDef2);
            //myGrid.ColumnDefinitions.Add(colDef3);

            //// Define the Rows
            //RowDefinition rowDef1 = new RowDefinition();
            //RowDefinition rowDef2 = new RowDefinition();
            //RowDefinition rowDef3 = new RowDefinition();
            //myGrid.RowDefinitions.Add(rowDef1);
            //myGrid.RowDefinitions.Add(rowDef2);
            //myGrid.RowDefinitions.Add(rowDef3);
            //groupBox.Content = myGrid;

            for (int i = 0; i < FilterGlass.main.Mask.GetLength(0); i++ )
            {
                    for (int j = 0; j < FilterGlass.main.Mask.GetLength(1); j++ )
                    {
                        double s = FilterGlass.main.Mask[i, j];
                       
                    
                        for(int b = 0; b <= 8; b++)
                        {
                            //this.RegisterName("textBox" + b, Textbox).Text = s.ToString();
                            //tb.Text = s.ToString();
                         }
                      
                        
                        //myGrid.Children.Add(tb);

                            
                        // this.rasterGrid.Children.Add(tb);
                        
                    }

                }

        }


        public string filterName;


        internal static FilterPropsDialog main;

        public class TextBox
        {
            public TextBox()
            {
               
            }
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
                //fontNamesSource = Fonts.SystemFontFamilies.Select(ff => ff.Source).ToList();
                

                return filterSource;
            }
            set
            {
                FilterSource = value;
            }
        }



        //public double Textbox
        //{
        //    get { return Convert.ToDouble(textBox1.Text); }
        //    set { textBox1.Text = value.ToString(); }
        //}


        //public void GenerateMatrix(int N)
        //{
        //    rasterGrid.Children.Clear();
        //    for (int row = N; row >= 0; row--)
        //    {
        //        TextBox txtb = new TextBox();
        //        txtb
        //        rasterGrid.Children.Add(txtb);

        //        for (int column = N; column >= 0; column--)
        //        {
                    
        //        }
        //    }
        //}


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
                Std_KMP_Glasses.main.canvasCanvas.InvalidateVisual();
            }

            else if(comboBoxFilter.SelectedItem.ToString() == "Kanten")
            {
                filterName = "Kanten";
                intOld = comboBoxFilter.SelectedIndex;
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
                //GenerateMatrix(old);

            }
            else
            {
                int New = int.Parse(OffsetDisp.Text);
                New += 1;
                OffsetDisp.Text = New.ToString();
                //GenerateMatrix(New);
            }
        }


    }
}
