using System;
using System.Windows;
using System.Windows.Input;
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
    /// Interaction logic for CropWindow.xaml
    /// </summary>
    public partial class CropWindow : Window
    {
        private MainWindow mainwindow;

        public static string crop;

        public CropWindow()
        {
            // Don't Remove
        }

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
            if (string.IsNullOrEmpty(MainWindow.cropWidth))
            {
                cropWidth.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(MainWindow.cropWidth))
            {
                cropWidth.Text = MainWindow.cropWidth;
            }

            // -------------------------
            // Crop Height
            // ------------------------- 
            // First time use
            if (string.IsNullOrEmpty(MainWindow.cropHeight))
            {
                cropHeight.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(MainWindow.cropHeight))
            {
                cropHeight.Text = MainWindow.cropHeight;
            }

            // -------------------------
            // Crop X
            // ------------------------- 
            // First time use
            if (string.IsNullOrEmpty(MainWindow.cropX))
            {
                cropX.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(MainWindow.cropX))
            {
                cropX.Text = MainWindow.cropX;
            }

            // -------------------------
            // Crop Y
            // ------------------------- 
            // First time use
            if (string.IsNullOrEmpty(MainWindow.cropY))
            {
                cropY.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(MainWindow.cropY))
            {
                cropY.Text = MainWindow.cropY;
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
        private void cropWidth_KeyDown(object sender, KeyEventArgs e)
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
        private void cropHeight_KeyDown(object sender, KeyEventArgs e)
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
        private void cropX_KeyDown(object sender, KeyEventArgs e)
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
        private void cropY_KeyDown(object sender, KeyEventArgs e)
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
            MainWindow.crop = string.Empty;
            //mainwindow.crop = null;

            // set crop string to be saved after each run
            //MainWindow.cropClear = false;

            // Make x264 & x265 Width/Height Divisible by 2
            //
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
            {
                try // will error if wrong characters input
                {
                    int divisibleWidth = Convert.ToInt32(cropWidth.Text);
                    int divisibleHeight = Convert.ToInt32(cropHeight.Text);

                    // If not divisible by 2, subtract 1 from total
                    if (divisibleWidth % 2 != 0)
                    {
                        divisibleWidth -= 1;
                    }
                    if (divisibleHeight % 2 != 0)
                    {
                        divisibleHeight -= 1;
                    }

                    // Save Temp TextBox String Holder
                    MainWindow.cropWidth = cropWidth.Text;
                    MainWindow.cropHeight = cropWidth.Text;
                    MainWindow.cropX = cropX.Text;
                    MainWindow.cropY = cropY.Text;

                    crop = Convert.ToString("crop=" + divisibleWidth + ":" + divisibleHeight + ":" + cropX.Text + ":" + cropY.Text);

                }
                catch
                {
                    MessageBox.Show("Error: Must enter numbers only.");
                }

            }

            // Use Normal Width/Height
            //
            else
            {
                // Save Temp TextBox String Holder
                MainWindow.cropWidth = cropWidth.Text;
                MainWindow.cropHeight = cropHeight.Text;
                MainWindow.cropX = cropX.Text;
                MainWindow.cropY = cropY.Text;

                crop = "crop=" + cropWidth.Text + ":" + cropHeight.Text + ":" + cropX.Text + ":" + cropY.Text;
            }


            // Set Empty Crop X Textbox to 0
            //
            if (string.IsNullOrWhiteSpace(cropX.Text))
            {
                cropX.Text = "0";
                MainWindow.cropX = cropX.Text;

                crop = "crop=" + cropWidth.Text + ":" + cropHeight.Text + ":" + cropX.Text + ":" + cropY.Text;
            }
            // Set Empty Crop Y Textbox to 0
            //
            if (string.IsNullOrWhiteSpace(cropY.Text))
            {
                cropY.Text = "0";
                MainWindow.cropY = cropY.Text;

                crop = "crop=" + cropWidth.Text + ":" + cropHeight.Text + ":" + cropX.Text + ":" + cropY.Text;
            }

            // Set Button Text to show Crop is Active
            mainwindow.buttonCropClearTextBox.Text = "~";

            // Log Console Message /////////
            //console.rtbLog.Inlines.Add("Crop Set: " + "Width = " + cropWidth.Text + " Height = " + cropHeight.Text + " X = " + cropX.Text + " Y = " + cropY.Text);

            this.Close();
        }


        /// <summary>
        /// Clear Button
        /// </summary>
        public void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            // set crop string to be cleared after each run
            //MainWindow.cropClear = true;

            cropWidth.Text = string.Empty;
            cropHeight.Text = string.Empty;
            cropX.Text = string.Empty;
            cropY.Text = string.Empty;

            MainWindow.cropWidth = string.Empty;
            MainWindow.cropHeight = string.Empty;
            MainWindow.cropX = string.Empty;
            MainWindow.cropY = string.Empty;

            crop = string.Empty;
            MainWindow.crop = string.Empty;

            // Set Button Text to show Crop is Active
            mainwindow.buttonCropClearTextBox.Text = string.Empty;
        }
    }
}
