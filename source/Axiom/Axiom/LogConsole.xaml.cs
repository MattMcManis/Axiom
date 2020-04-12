/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2020 Matt McManis
https://github.com/MattMcManis/Axiom
https://axiomui.github.io
mattmcmanis@outlook.com

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

using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
// Disable XML Comment warnings
#pragma warning disable 1591

namespace Axiom
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class LogConsole : Window
    {
        public LogConsole()
        {
            InitializeComponent();

            // Set Width/Height to prevent Tablets maximizing
            //this.Width = 400;
            //this.Height = 500;
            this.MinWidth = 200;
            this.MinHeight = 200;
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


        /// <summary>
        /// Expand Button
        /// </summary>
        private void btnExpand_Click(object sender, RoutedEventArgs e)
        {
            // If less than 600px Height
            if (this.Height <= 650)
            {
                this.Width = 650;
                this.Height = 600;

                double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
                double windowWidth = this.Width;
                double windowHeight = this.Height;
                this.Left = (screenWidth / 2) - (windowWidth / 2);
                this.Top = (screenHeight / 2) - (windowHeight / 2);
            }
        }

    }
}
