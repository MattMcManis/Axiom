/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2019 Matt McManis
http://github.com/MattMcManis/Axiom
http://axiomui.github.io
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

using System;
using System.Windows;
using System.Windows.Input;
// Disable XML Comment warnings
#pragma warning disable 1591

namespace Axiom
{
    /// <summary>
    /// Interaction logic for xaml
    /// </summary>
    public partial class CropWindow : Window
    {
        private MainWindow mainwindow;

        //private ViewModel vm;

        // Temp save settings when Crop Window is closed
        public static int? divisibleCropWidth;
        public static int? divisibleCropHeight;
        public static string cropWidth;
        public static string cropHeight;
        public static string cropX;
        public static string cropY;
        public static string crop; // Combined Width, Height, X, Y


        public CropWindow(MainWindow mainwindow, ViewModel vm)
        {
            InitializeComponent();

            this.mainwindow = mainwindow;

            //vm = mainwindow.DataContext as ViewModel;

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
            if (string.IsNullOrEmpty(cropWidth))
            {
                textBoxCropWidth.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(cropWidth))
            {
                textBoxCropWidth.Text = cropWidth;
            }

            // -------------------------
            // Crop Height
            // ------------------------- 
            // First time use
            if (string.IsNullOrEmpty(cropHeight))
            {
                textBoxCropHeight.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(cropHeight))
            {
                textBoxCropHeight.Text = cropHeight;
            }

            // -------------------------
            // Crop X
            // ------------------------- 
            // First time use
            if (string.IsNullOrEmpty(cropX))
            {
                textBoxCropX.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(cropX))
            {
                textBoxCropX.Text = cropX;
            }

            // -------------------------
            // Crop Y
            // ------------------------- 
            // First time use
            if (string.IsNullOrEmpty(cropY))
            {
                textBoxCropY.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(cropY))
            {
                textBoxCropY.Text = cropY;
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
            ViewModel vm = mainwindow.DataContext as ViewModel;

            crop = string.Empty;

            // Save Temp TextBox String Holder
            cropWidth = textBoxCropWidth.Text;
            cropHeight = textBoxCropHeight.Text;
            cropX = textBoxCropX.Text;
            cropY = textBoxCropY.Text;

            // Set Empty Crop X Textbox to 0
            //
            if (string.IsNullOrWhiteSpace(textBoxCropX.Text))
            {
                textBoxCropX.Text = "0";
                cropX = textBoxCropX.Text;
            }
            // Set Empty Crop Y Textbox to 0
            //
            if (string.IsNullOrWhiteSpace(textBoxCropY.Text))
            {
                textBoxCropY.Text = "0";
                cropY = textBoxCropY.Text;
            }

            // Make x264 & x265 Width/Height Divisible by 2
            //
            if (vm.VideoCodec_SelectedItem == "x264" || 
                vm.VideoCodec_SelectedItem == "x265")
            {
                try // will error if wrong characters input
                {
                    divisibleCropWidth = Convert.ToInt32(cropWidth);
                    divisibleCropHeight = Convert.ToInt32(cropHeight);

                    // If not divisible by 2, subtract 1 from total
                    if (divisibleCropWidth % 2 != 0)
                    {
                        divisibleCropWidth -= 1;
                    }
                    if (divisibleCropHeight % 2 != 0)
                    {
                        divisibleCropHeight -= 1;
                    }

                    crop = Convert.ToString("crop=" + divisibleCropWidth + ":" + divisibleCropHeight + ":" + cropX + ":" + cropY);

                    // Video Filter Add
                    //Video.VideoFilters.Add(crop);

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
                crop = "crop=" + cropWidth + ":" + cropHeight + ":" + cropX + ":" + cropY;

                // Video Filter Add
                //Video.VideoFilters.Add(crop);
            }

            // Set Button Text to show Crop is Active
            vm.CropClear_Text = "Clear*";

            this.Close();
        }


        /// <summary>
        /// Clear Button
        /// </summary>
        public void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            ViewModel vm = mainwindow.DataContext as ViewModel;

            textBoxCropWidth.Text = string.Empty;
            textBoxCropHeight.Text = string.Empty;
            textBoxCropX.Text = string.Empty;
            textBoxCropY.Text = string.Empty;

            CropClear(vm);
        }

        /// <summary>
        /// Crop Clear (Method)
        /// </summary>
        public static void CropClear(ViewModel vm)
        {
            divisibleCropWidth = null;
            divisibleCropHeight = null;

            cropWidth = string.Empty;
            cropHeight = string.Empty;
            cropX = string.Empty;
            cropY = string.Empty;

            crop = string.Empty;

            // Set Button Text to show Crop is Active
            vm.CropClear_Text = "Clear";
        }
    }
}
