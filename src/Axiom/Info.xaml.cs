using System.Windows;
// Disable XML Comment warnings
#pragma warning disable 1591

namespace Axiom
{
    /// <summary>
    /// Interaction logic for Info.xaml
    /// </summary>
    public partial class Info : Window
    {

        public Info()
        {
            InitializeComponent();

            // Set Min/Max Width/Height to prevent Tablets maximizing
            this.MinWidth = 445;
            this.MinHeight = 335;
            this.MaxWidth = 445;
            this.MaxHeight = 335;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
