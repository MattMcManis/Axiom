using System;
using System.Collections.Generic;
using System.ComponentModel;
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
// Disable XML Comment warnings
#pragma warning disable 1591

namespace Axiom
{
    /// <summary>
    /// Interaction logic for FileQueue.xaml
    /// </summary>
    public partial class FileQueue : Window
    {
        private MainWindow mainwindow;

        public FileQueue()
        {
        }

        public FileQueue(MainWindow mainwindow)
        {
            InitializeComponent();

            this.mainwindow = mainwindow;
        }

        /// <summary>
        ///  Hide Window Instead of Closing
        /// </summary>
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Hide();
            this.Width = 400;
            this.Height = 500;
            e.Cancel = true;
            base.OnClosing(e);
        }

        private void listViewFileQueue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
