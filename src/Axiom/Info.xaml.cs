using System.Windows;
// Disable XML Comment warnings
#pragma warning disable 1591

/* ----------------------------------------------------------------------
Axiom
Copyright (C) 2017 Matt McManis
http://github.com/MattMcManis/Axiom
http://www.x.co/axiomui
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
