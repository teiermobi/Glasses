using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;


namespace Glasses
{
    public class NumericTextBox : TextBox
    {
        public NumericTextBox()
        {
            CommandManager.AddPreviewExecutedHandler(this, CommandManager_PreviewExecuted);
          //  this.onChange += new RoutedEventHandler(GetTextfromTextboxes);
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (e.Text.Length != 1 || !char.IsDigit(e.Text[0])) e.Handled = true;
            base.OnPreviewTextInput(e);
        }

        private void CommandManager_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste) e.Handled = true;
        }

        //public void GetMaskfromTextbox()
        //{
        //    IEnumerable<TextBox> TextBoxes = FilterPropsDialog.main.rasterGrid.Children.OfType<TextBox>();
           
                    
        //    double value;
        //    foreach (var textbox in TextBoxes)
        //    {
        //        for (int x = 0; x < 5; x++)
        //        {
        //            for (int y = 0; y < 5; y++)
        //            {
        //                FilterGlass.main.Mask = new double[x, y];
        //                value = Convert.ToDouble(textbox.Text);
        //                FilterGlass.main.Mask[x, y] = value;
        //            }
        //        }

        //    }
  

        //}

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
           
            base.OnTextChanged(e);
 

            int val = 0;
            if (!int.TryParse(Text, out val)) val = 0;

            Value = val;
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(NumericTextBox), new PropertyMetadata(0));
    }
}
