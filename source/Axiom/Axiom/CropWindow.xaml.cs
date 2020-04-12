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

using System;
using System.Collections.Generic;
using System.Linq;
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
        private MainWindow mainwindow = (MainWindow)System.Windows.Application.Current.MainWindow;

        // Temp save settings when Crop Window is closed
        public static int? divisibleCropWidth { get; set; }
        public static int? divisibleCropHeight { get; set; }
        public static string cropWidth { get; set; }
        public static string cropHeight { get; set; }
        public static string cropX { get; set; }
        public static string cropY { get; set; }
        public static string crop { get; set; } // Combined Width, Height, X, Y


        public CropWindow(MainWindow mainwindow)
        {
            InitializeComponent();

            this.mainwindow = mainwindow;

            //DataContext = new VM();

            // Set Min/Max Width/Height to prevent Tablets maximizing
            this.MinWidth = 480;
            this.MinHeight = 270;
            this.MaxWidth = 480;
            this.MaxHeight = 270;

            // --------------------------------------------------
            // Load From Saved Settings
            // --------------------------------------------------
            //// -------------------------
            //// Crop Width
            //// ------------------------- 
            //// First time use
            //if (string.IsNullOrEmpty(cropWidth))
            //{
            //    VM.VideoView.Video_Crop_Width_Text = string.Empty;
            //}
            //// Load Temp Saved String
            //else if (!string.IsNullOrEmpty(cropWidth))
            //{
            //    VM.VideoView.Video_Crop_Width_Text = cropWidth;
            //}

            //// -------------------------
            //// Crop Height
            //// ------------------------- 
            //// First time use
            //if (string.IsNullOrEmpty(cropHeight))
            //{
            //    VM.VideoView.Video_Crop_Height_Text = string.Empty;
            //}
            //// Load Temp Saved String
            //else if (!string.IsNullOrEmpty(cropHeight))
            //{
            //    VM.VideoView.Video_Crop_Height_Text = cropHeight;
            //}

            //// -------------------------
            //// Crop X
            //// ------------------------- 
            //// First time use
            //if (string.IsNullOrEmpty(cropX))
            //{
            //    VM.VideoView.Video_Crop_X_Text = string.Empty;
            //}
            //// Load Temp Saved String
            //else if (!string.IsNullOrEmpty(cropX))
            //{
            //    VM.VideoView.Video_Crop_X_Text = cropX;
            //}

            //// -------------------------
            //// Crop Y
            //// ------------------------- 
            //// First time use
            //if (string.IsNullOrEmpty(cropY))
            //{
            //    VM.VideoView.Video_Crop_Y_Text = string.Empty;
            //}
            //// Load Temp Saved String
            //else if (!string.IsNullOrEmpty(cropY))
            //{
            //    VM.VideoView.Video_Crop_Y_Text = cropY;
            //}
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
        private void tbxVideo_Crop_Width_KeyDown(object sender, KeyEventArgs e)
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
        private void tbxVideo_Crop_Height_KeyDown(object sender, KeyEventArgs e)
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
        private void tbxVideo_Crop_X_KeyDown(object sender, KeyEventArgs e)
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
        private void tbxVideo_Crop_Y_KeyDown(object sender, KeyEventArgs e)
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
        public void btnSet_Click(object sender, RoutedEventArgs e)
        {
            //MainView vm = mainwindow.DataContext as MainView;

            crop = string.Empty;

            // -------------------------
            // Save Temp TextBox String Holder
            // -------------------------
            cropWidth = VM.VideoView.Video_Crop_Width_Text;
            cropHeight = VM.VideoView.Video_Crop_Height_Text;
            cropX = VM.VideoView.Video_Crop_X_Text;
            cropY = VM.VideoView.Video_Crop_Y_Text;


            // Set Empty Crop Width Textbox to 0
            //
            if (string.IsNullOrEmpty(cropWidth))
            {
                VM.VideoView.Video_Crop_Width_Text = "0";
                cropWidth = VM.VideoView.Video_Crop_Width_Text;
            }
            // Set Empty Crop Height Textbox to 0
            //
            if (string.IsNullOrEmpty(cropHeight))
            {
                VM.VideoView.Video_Crop_Height_Text = "0";
                cropHeight = VM.VideoView.Video_Crop_Height_Text;
            }
            // Set Empty Crop X Textbox to 0
            //
            if (string.IsNullOrEmpty(cropX))
            {
                VM.VideoView.Video_Crop_X_Text = "0";
                cropX = VM.VideoView.Video_Crop_X_Text;
            }
            // Set Empty Crop Y Textbox to 0
            //
            if (string.IsNullOrEmpty(cropY))
            {
                VM.VideoView.Video_Crop_Y_Text = "0";
                cropY = VM.VideoView.Video_Crop_Y_Text;
            }

            // -------------------------
            // Crop Values Null Check
            // -------------------------
            List<int> cropValues = new List<int>()
            {
                Convert.ToInt32(cropWidth),
                Convert.ToInt32(cropHeight),
                Convert.ToInt32(cropX),
                Convert.ToInt32(cropY)
            };

            if (cropValues.Sum() != 0)
            {
                // Make x264 & x265 Width/Height Divisible by 2
                //
                if (VM.VideoView.Video_Codec_SelectedItem == "x264" ||
                    VM.VideoView.Video_Codec_SelectedItem == "x265")
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
                }

                // Set Button Text to show Crop is Active
                VM.VideoView.Video_CropClear_Text = "Clear*";
            }

            // -------------------------
            // Reset TextBoxes to Empty for next Window open
            // -------------------------
            else
            {
                CropClear();
            }

            this.Close();
        }


        /// <summary>
        /// Clear Button
        /// </summary>
        public void btnClear_Click(object sender, RoutedEventArgs e)
        {
            CropClear();
        }

        /// <summary>
        /// Crop Clear (Method)
        /// </summary>
        public static void CropClear()
        {
            VideoFilters.vFilter = string.Empty;

            if (VideoFilters.vFiltersList != null && 
                VideoFilters.vFiltersList.Count > 0)
            {
                VideoFilters.vFiltersList.Clear();
                VideoFilters.vFiltersList.TrimExcess();
            }

            VM.VideoView.Video_Crop_Width_Text = string.Empty;
            VM.VideoView.Video_Crop_Height_Text = string.Empty;
            VM.VideoView.Video_Crop_X_Text = string.Empty;
            VM.VideoView.Video_Crop_Y_Text = string.Empty;

            divisibleCropWidth = null;
            divisibleCropHeight = null;

            cropWidth = string.Empty;
            cropHeight = string.Empty;
            cropX = string.Empty;
            cropY = string.Empty;

            crop = string.Empty;

            // Set Button Text to show Crop is Active
            VM.VideoView.Video_CropClear_Text = "Clear";
        }
    }
}
