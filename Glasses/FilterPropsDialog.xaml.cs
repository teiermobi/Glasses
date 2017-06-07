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

        }
        //class FilterTypeClass
        //{
        //    public void filterTypeClass(int filterTypeId, string filterTypeDescr)
        //    {
        //        this.FilterTypeId = filterTypeId;
        //        this.FilterTypeDescr = filterTypeDescr;
        //    }
        //    public int FilterTypeId;
        //    public string FilterTypeDescr;
        //    public override string ToString()
        //    {
        //        return (FilterTypeDescr);
        //    }
        //}

        //FilterTypeClass FilterTypes;

        //private void FilterTypes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (FilterTypes.SelectedIndex >= 0)
        //    {
        //        FilterTypeId.Text = ((PetTypeClass)FilterTypes.SelectedItem).PetTypeId.ToString();
        //    }
        //    else
        //    {
        //        FilterTypeId.Text = "Keine Auswahl bei ListBox";
        //    }
        //}


        //FilterTypeClass[] PetTypeItems = new FilterTypeClass[]
        //{
        //    new FilterTypeClass(1, "Kontrast"),
        //    new FilterTypeClass(2, "Kanten"),
        //    new FilterTypeClass(3, "Benutzerdef."),

        //};
        //FilterTypes.ItemsSource = FilterTypeItems

        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        //ComboboxItem item = new ComboboxItem();
        //item.Text = "Item text1";
        //item.Value = 12;

        //comboBox1.Items.Add(item);

        //comboBox1.SelectedIndex = 0;

        //MessageBox.Show((comboBox1.SelectedItem as ComboboxItem).Value.ToString());



        public double Textbox
        {
            get { return Convert.ToDouble(textBox.Text); }
            set { textBox.Text = value.ToString(); }
        }

        

        internal static FilterPropsDialog main;

        public void GetMask()
        {
            //IEnumerable<Glass> Glasses = FilterPropsDialog.main.Children.OfType<Glass>();
            //foreach (var glass in Glasses)
            //{
            //}
        }

    }

    internal class PetTypeClass
    {
    }
}
