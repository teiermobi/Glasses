//Autoren: Meier, Kleber, Pawlowski
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Glasses
{
    // NummericDoubleTextBox Klasse für WasserGlas
    public class NumericDoubleTextBox : TextBox
    {
        public NumericDoubleTextBox()
        {
            CommandManager.AddPreviewExecutedHandler(this, CommandManager_PreviewExecuted);
        }

        // Überschriebene OnPreviewTextInput
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
