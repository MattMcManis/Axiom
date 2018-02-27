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

using System;
using System.Windows;
using System.Windows.Input;
// Disable XML Comment warnings
#pragma warning disable 1591

namespace Axiom
{
    /// <summary>
    /// Interaction logic for CropWindow.xaml
    /// </summary>
    public partial class CropWindow : Window
    {
        private MainWindow mainwindow;

        // Temp save settings when Crop Window is closed
        public static int? divisibleCropWidth;
        public static int? divisibleCropHeight;
        public static string cropWidth;
        public static string cropHeight;
        public static string cropX;
        public static string cropY;
        public static string crop; // Combined Width, Height, X, Y


        public CropWindow(MainWindow mainwindow)
        {
            InitializeComponent();

            this.mainwindow = mainwindow;

            // Set Min/Max Width/Height to prevent Tablets maximizing
            this.MinWidth = 480;
            this.MinHeight = 270;
            this.MaxWidth = 480;
            this.MaxHeight = 270;

            // --------------------------------------------------
            // Load From Saved Settings
            // --------------------------------------------------
            // -------------------------
            // Crop Width
            // ------------------------- 
            // First time use
            if (string.IsNullOrEmpty(CropWindow.cropWidth))
            {
                textBoxCropWidth.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(CropWindow.cropWidth))
            {
                textBoxCropWidth.Text = CropWindow.cropWidth;
            }

            // -------------------------
            // Crop Height
            // ------------------------- 
            // First time use
            if (string.IsNullOrEmpty(CropWindow.cropHeight))
            {
                textBoxCropHeight.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(CropWindow.cropHeight))
            {
                textBoxCropHeight.Text = CropWindow.cropHeight;
            }

            // -------------------------
            // Crop X
            // ------------------------- 
            // First time use
            if (string.IsNullOrEmpty(CropWindow.cropX))
            {
                textBoxCropX.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(CropWindow.cropX))
            {
                textBoxCropX.Text = CropWindow.cropX;
            }

            // -------------------------
            // Crop Y
            // ------------------------- 
            // First time use
            if (string.IsNullOrEmpty(CropWindow.cropY))
            {
                textBoxCropY.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(CropWindow.cropY))
            {
                textBoxCropY.Text = CropWindow.cropY;
            }
        }


        /// <summary>
        /// Close All
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Close();
        }

        /// <summary>
        /// Crop Width
        /// </summary>
        private void textBoxCropWidth_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers or Backspace
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Crop Height
        /// </summary>
        private void textBoxCropHeight_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers or Backspace
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Crop X
        /// </summary>
        private void textBoxCropX_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers or Backspace
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Crop Y
        /// </summary>
        private void textBoxCropY_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers or Backspace
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }


        /// <summary>
        /// SET Button
        /// </summary>
        public void buttonSet_Click(object sender, RoutedEventArgs e)
        {
            CropWindow.crop = string.Empty;

            // Save Temp TextBox String Holder
            CropWindow.cropWidth = textBoxCropWidth.Text;
            CropWindow.cropHeight = textBoxCropHeight.Text;
            CropWindow.cropX = textBoxCropX.Text;
            CropWindow.cropY = textBoxCropY.Text;

            // Set Empty Crop X Textbox to 0
            //
            if (string.IsNullOrWhiteSpace(textBoxCropX.Text))
            {
                textBoxCropX.Text = "0";
                CropWindow.cropX = textBoxCropX.Text;
            }
            // Set Empty Crop Y Textbox to 0
            //
            if (string.IsNullOrWhiteSpace(textBoxCropY.Text))
            {
                textBoxCropY.Text = "0";
                CropWindow.cropY = textBoxCropY.Text;
            }

            // Make x264 & x265 Width/Height Divisible by 2
            //
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
            {
                try // will error if wrong characters input
                {
                    CropWindow.divisibleCropWidth = Convert.ToInt32(CropWindow.cropWidth);
                    CropWindow.divisibleCropHeight = Convert.ToInt32(CropWindow.cropHeight);

                    // If not divisible by 2, subtract 1 from total
                    if (CropWindow.divisibleCropWidth % 2 != 0)
                    {
                        CropWindow.divisibleCropWidth -= 1;
                    }
                    if (CropWindow.divisibleCropHeight % 2 != 0)
                    {
                        CropWindow.divisibleCropHeight -= 1;
                    }

                    CropWindow.crop = Convert.ToString("crop=" + CropWindow.divisibleCropWidth + ":" + CropWindow.divisibleCropHeight + ":" + CropWindow.cropX + ":" + CropWindow.cropY);

                    // Video Filter Add
                    //Video.VideoFilters.Add(CropWindow.crop);

                }
                catch
                {
                    MessageBox.Show("Error: Must enter numbers only.");
                }
            }

            // Use Normal Width, Height, X, Y
            //
            else
            {
                CropWindow.crop = "crop=" + CropWindow.cropWidth + ":" + CropWindow.cropHeight + ":" + CropWindow.cropX + ":" + CropWindow.cropY;

                // Video Filter Add
                //Video.VideoFilters.Add(CropWindow.crop);
            }

            // Set Button Text to show Crop is Active
            mainwindow.buttonCropClearTextBox.Text = "~";

            this.Close();
        }


        /// <summary>
        /// Clear Button
        /// </summary>
        public void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxCropWidth.Text = string.Empty;
            textBoxCropHeight.Text = string.Empty;
            textBoxCropX.Text = string.Empty;
            textBoxCropY.Text = string.Empty;

            CropWindow.divisibleCropWidth = null;
            CropWindow.divisibleCropHeight = null;

            CropWindow.cropWidth = string.Empty;
            CropWindow.cropHeight = string.Empty;
            CropWindow.cropX = string.Empty;
            CropWindow.cropY = string.Empty;

            CropWindow.crop = string.Empty;
            CropWindow.crop = string.Empty;

            // Set Button Text to show Crop is Active
            mainwindow.buttonCropClearTextBox.Text = string.Empty;
        }
    }
}
