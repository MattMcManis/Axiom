using System.ComponentModel;
using System.Windows;
// Disable XML Comment warnings
#pragma warning disable 1591

/* ----------------------------------------------------------------------
    Axiom UI
    Copyright (C) 2017 Matt McManis
    http://github.com/MattMcManis/Axiom
    http://axiomui.github.io
    axiom.interface@gmail.com

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.If not, see <http://www.gnu.org/licenses/>. 
   ---------------------------------------------------------------------- */

namespace Axiom
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class LogConsole : Window
    {
        private MainWindow mainwindow;
        private ConfigureWindow configurewindow;

        public LogConsole()
        {
            //do not remove
        }

        public LogConsole(MainWindow mainwindow)
        {
            InitializeComponent();

            this.mainwindow = mainwindow;

            // Set Width/Height to prevent Tablets maximizing
            this.Width = 400;
            this.Height = 500;
            this.MinWidth = 200;
            this.MinHeight = 200;

            // -------------------------
            // Text Theme Color
            // -------------------------
            ConfigureWindow.ConfigTheme(configurewindow);
        }


        /// <summary>
        /// Close
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Close();
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