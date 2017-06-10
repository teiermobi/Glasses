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

namespace Glasses
{
    /// <summary>
    /// Interaktionslogik für FilterPropsDialog.xaml
    /// </summary>
    public partial class FilterPropsDialog : Window
    {
        public FilterPropsDialog()
        {
            InitializeComponent();
            main = this;
            comboBoxFilter.SelectedIndex = 0;
        }

        internal static FilterPropsDialog main;

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
                
                filterSource.Add("Kanten");
                filterSource.Add("Kontrast");
                filterSource.Add("Benutzerdef.");
                //fontNamesSource = Fonts.SystemFontFamilies.Select(ff => ff.Source).ToList();
                

                return filterSource;
            }
            set
            {
                FilterSource = value;
            }
        }



        public double Textbox
        {
            get { return Convert.ToDouble(textBox.Text); }
            set { textBox.Text = value.ToString(); }
        }



       

        public void GetMask()
        {
            //IEnumerable<Glass> Glasses = FilterPropsDialog.main.Children.OfType<Glass>();
            //foreach (var glass in Glasses)
            //{
            //}
        }

        private void comboBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (comboBoxFilter.SelectedItem.ToString() == "Kanten")
            //{
            //    //FilterGlass kante new FilterGlass();
            //    //Console.Write("Hier");
            //}

            //else if(comboBoxFilter.SelectedItem.ToString() == "Kontrast")
            //{

            //}

        }
    }
}
