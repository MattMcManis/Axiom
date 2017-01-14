using System.ComponentModel;
using System.Windows;
// Disable XML Comment warnings
#pragma warning disable 1591


namespace Axiom
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class LogConsole : Window
    {
        private MainWindow mainwindow;
        //private CropWindow cropwindow;

        public LogConsole()
        {
            //do not remove
        }

        public LogConsole(MainWindow mainwindow)
        {
            InitializeComponent();

            this.mainwindow = mainwindow;
            //this.cropwindow = cropwindow;

            // Set Width/Height to prevent Tablets maximizing
            this.Width = 400;
            this.Height = 500;
            this.MinWidth = 200;
            this.MinHeight = 200;
        }


        /// <summary>
        /// Expand Button
        /// </summary>
        private void buttonExpand_Click(object sender, RoutedEventArgs e)
        {
            // If less than 600px Height
            if (this.Width <= 650)
            {
                this.Width = 650;
                this.Height = 600;
            }          
        }


        /// <summary>
        /// Hide Window Instead of Closing
        /// </summary>
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Hide();
            this.Width = 400;
            this.Height = 500;
            e.Cancel = true;
            base.OnClosing(e);
        }
    }
}