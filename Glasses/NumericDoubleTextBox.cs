using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

namespace Glasses
{
    public class NumericDoubleTextBox : TextBox
    {
        public NumericDoubleTextBox()
        {
            CommandManager.AddPreviewExecutedHandler(this, CommandManager_PreviewExecuted);
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (!(e.Text.Length == 1 && ((e.Text.ElementAt(0) >= '0' && e.Text.ElementAt(0) <= '9') || e.Text.ElementAt(0) == ',' || e.Text.ElementAt(0) == '-')))
            {
                e.Handled = true;
            }
            base.OnPreviewTextInput(e);
        }

        private void CommandManager_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste) { e.Handled = true; };
        }
    }
}
