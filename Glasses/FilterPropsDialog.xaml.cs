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
}
