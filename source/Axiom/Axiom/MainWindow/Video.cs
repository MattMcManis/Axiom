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
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Encode Speed Presets - ComboBox
        /// </summary>
        private void cboEncodeSpeed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Output Path Update Display
            // -------------------------
            OutputPath_UpdateDisplay();
        }

        /// <summary>
        /// HW Accel Transcode - ComboBox
        /// </summary>
        private void cboHWAccelTranscode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Output Path Update Display
            // -------------------------
            OutputPath_UpdateDisplay();
        }

        /// <summary>
        /// Video Codec - ComboBox
        /// </summary>
        private void cboVideo_Codec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string video_Codec_SelectedItem = (sender as ComboBox).SelectedItem as string;

            // -------------------------
            // Halt if Selected Codec is Null
            // -------------------------
            //if (string.IsNullOrWhiteSpace(video_Codec_SelectedItem))
            //{
            //    return;
            //}

            // -------------------------
            // Get Input/Output Extensions
            // -------------------------
            //string inputExt = Path.GetExtension(VM.MainView.Input_Text).ToLower();
            //string outputExt = "." + VM.FormatView.Format_Container_SelectedItem.ToLower();

            // -------------------------
            // Save Selected Quality
            // When you change the Quality ComboBox from Auto to 320, it triggers the Codec ComboBox to change from Copy to x264,
            // in turn changing the Quality ComboBox back to Auto on the Codec switch
            // -------------------------
            //string userSelected_VideoQuality = string.Empty;
            //if (string.IsNullOrWhiteSpace(inputExt) ||
            //    string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
            //{
            //    if (video_Codec_SelectedItem != "Copy" &&
            //        VM.VideoView.Video_Quality_SelectedItem != "Auto")
            //    {
            //        userSelected_VideoQuality = VM.VideoView.Video_Quality_SelectedItem;
            //    }
            //}

            // -------------------------
            // Set Copy Quality to Auto
            // -------------------------
            //if (string.IsNullOrWhiteSpace(inputExt) ||
            //    string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
            //{
            //    if (video_Codec_SelectedItem == "Copy")
            //    {
            //        VM.VideoView.Video_Quality_SelectedItem = "Auto";
            //    }
            //}

            // -------------------------
            // Set Controls
            // -------------------------
            Controls.Video.Controls.SetControls(video_Codec_SelectedItem);

            // -------------------------
            // Re-Select the Quality Preset
            // -------------------------
            // Copy Codec -> VP8, x264, etc Codec
            //if (string.IsNullOrWhiteSpace(inputExt) ||
            //    string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
            //{
            //    if (video_Codec_SelectedItem != "Copy")
            //    {
            //        if (!string.IsNullOrWhiteSpace(userSelected_VideoQuality))
            //        {
            //            VM.VideoView.Video_Quality_SelectedItem = userSelected_VideoQuality;
            //        }
            //    }
            //    // Set to Top of Item List: Auto or None
            //    else
            //    {
            //        VM.VideoView.Video_Quality_SelectedIndex = 0;
            //    }
            //}

            // -------------------------
            // Audio Stream Controls
            // -------------------------
            Controls.Format.Controls.AudioStreamControls();

            // -------------------------
            // Video Encoding Pass
            // -------------------------
            Controls.Video.Controls.EncodingPassControls();

            // -------------------------
            // Pixel Format
            // -------------------------
            Controls.Video.Controls.PixelFormatControls(VM.FormatView.Format_MediaType_SelectedItem,
                                                             VM.VideoView.Video_Codec_SelectedItem,
                                                             VM.VideoView.Video_Quality_SelectedItem);

            // -------------------------
            // Optimize Controls
            // -------------------------
            Controls.Video.Controls.OptimizeControls();

            // -------------------------
            // Output Path Update Display
            // -------------------------
            OutputPath_UpdateDisplay();

            // -------------------------
            // Convert Button Text Change
            // -------------------------
            ConvertButtonText();
        }


        /// <summary>
        /// HW Accel
        /// </summary>
        private void cboHWAccel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (VM.VideoView.Video_HWAccel_SelectedItem)
            {
                case "Auto":
                    VM.VideoView.Video_HWAccel_Decode_IsEnabled = false;
                    VM.VideoView.Video_HWAccel_Decode_SelectedItem = "auto";
                    VM.VideoView.Video_HWAccel_Transcode_IsEnabled = false;
                    VM.VideoView.Video_HWAccel_Transcode_SelectedItem = "auto";
                    break;

                case "On":
                    VM.VideoView.Video_HWAccel_Decode_IsEnabled = true;
                    VM.VideoView.Video_HWAccel_Decode_SelectedItem = "auto";
                    VM.VideoView.Video_HWAccel_Transcode_IsEnabled = true;
                    VM.VideoView.Video_HWAccel_Transcode_SelectedItem = "auto";
                    break;

                case "Off":
                    VM.VideoView.Video_HWAccel_Decode_IsEnabled = false;
                    VM.VideoView.Video_HWAccel_Decode_SelectedItem = "off";
                    VM.VideoView.Video_HWAccel_Transcode_IsEnabled = false;
                    VM.VideoView.Video_HWAccel_Transcode_SelectedItem = "off";
                    break;

                default:
                    VM.VideoView.Video_HWAccel_Decode_IsEnabled = true;
                    VM.VideoView.Video_HWAccel_Decode_SelectedItem = "off";
                    VM.VideoView.Video_HWAccel_Transcode_IsEnabled = true;
                    VM.VideoView.Video_HWAccel_Transcode_SelectedItem = "off";
                    break;
            }
        }


        /// <summary>
        /// Pass - ComboBox
        /// </summary>
        private void cboVideo_Pass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Set Controls
            // -------------------------
            //VideoControls.SetControls(VM.VideoView.Video_Quality_SelectedItem);

            // -------------------------
            // Pass Controls
            // -------------------------
            Controls.Video.Controls.EncodingPassControls();

            // -------------------------
            // Display Bit Rate in TextBox
            // -------------------------
            Controls.Video.Controls.VideoBitRateDisplay(VM.VideoView.Video_Quality_Items,
                                                             VM.VideoView.Video_Quality_SelectedItem,
                                                             VM.VideoView.Video_Pass_SelectedItem);
            // -------------------------
            // Output Path Update Display
            // -------------------------
            OutputPath_UpdateDisplay();
        }
        private void cboVideo_Pass_DropDownClosed(object sender, EventArgs e)
        {
            // User willingly selected a Pass
            Controls.Video.Controls.passUserSelected = true;
        }


        /// <summary>
        /// Video Quality - ComboBox
        /// </summary>
        private void cboVideo_Quality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Halt if: Selected Codec is Null
            //          Selected Quality is Null
            // -------------------------
            //if (string.IsNullOrWhiteSpace(VM.VideoView.Video_Codec_SelectedItem) ||
            //    string.IsNullOrWhiteSpace(VM.VideoView.Video_Quality_SelectedItem))
            //{
            //    return;
            //}

            // -------------------------
            // Quality Controls
            // -------------------------
            Controls.Video.Controls.QualityControls();

            // -------------------------
            // Display Bit Rate in TextBox
            // -------------------------
            Controls.Video.Controls.VideoBitRateDisplay(VM.VideoView.Video_Quality_Items,
                                                       VM.VideoView.Video_Quality_SelectedItem,
                                                       VM.VideoView.Video_Pass_SelectedItem);

            // -------------------------
            // Video Encoding Pass
            // -------------------------
            Controls.Video.Controls.EncodingPassControls();

            // Custom
            if (VM.VideoView.Video_Quality_SelectedItem == "Custom")
            {
                // Default to CRF
                if (VM.VideoView.Video_Pass_Items?.Contains("CRF") == true)
                {
                    VM.VideoView.Video_Pass_SelectedItem = "CRF";
                }
                // Select first available (1 Pass, 2 Pass, auto)
                else
                {
                    VM.VideoView.Video_Pass_SelectedItem = VM.VideoView.Video_Pass_Items.FirstOrDefault();
                }
            }

            // -------------------------
            // Pixel Format
            // -------------------------
            Controls.Video.Controls.PixelFormatControls(VM.FormatView.Format_MediaType_SelectedItem,
                                                       VM.VideoView.Video_Codec_SelectedItem,
                                                       VM.VideoView.Video_Quality_SelectedItem);

            // -------------------------
            // Output Path Update Display
            // -------------------------
            OutputPath_UpdateDisplay();

            // -------------------------
            // Set Audio Codec Combobox to "Copy" if 
            // Input File Extension is Same as Output File Extension 
            // and Quality is Auto
            // -------------------------
            //VideoControls.AutoCopyVideoCodec("control");
        }


        /// <summary>
        /// Video CRF Custom Number Textbox
        /// </summary>
        // TextBox TextChanged
        private void tbxVideo_CRF_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Update Slider with entered value
            if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_CRF_Text))
            {
                VM.VideoView.Video_CRF_Value = Convert.ToDouble(VM.VideoView.Video_CRF_Text);
            }

            // TextBox Empty
            //else if (string.IsNullOrWhiteSpace(VM.VideoView.Video_CRF_Text))
            //{
            //  VM.VideoView.Video_CRF_Value = 0;
            //  VM.VideoView.Video_CRF_Text = string.Empty;
            //}

            // -------------------------
            // Output Path Update Display
            // -------------------------
            OutputPath_UpdateDisplay();

        }
        // TextBox Key Down
        private void tbxVideo_CRF_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers and Backspace
            Allow_Only_Number_Keys(e);
        }
        // Slider Value Change
        private void slVideo_CRF_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Update TextBox with value
            VM.VideoView.Video_CRF_Text = VM.VideoView.Video_CRF_Value.ToString();
        }
        // Slider Double Click
        private void slVideo_CRF_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.VideoView.Video_CRF_Value = 23;
        }

        /// <summary>
        /// Video VBR Toggle - Checked
        /// </summary>
        private void tglVideo_VBR_Checked(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Quality Controls
            // -------------------------
            Controls.Video.Controls.QualityControls();

            // -------------------------
            // MPEG-4 VBR can only use 1 Pass
            // -------------------------
            if (VM.VideoView.Video_Codec_SelectedItem == "MPEG-2" ||
                VM.VideoView.Video_Codec_SelectedItem == "MPEG-4")
            {
                // Change ItemsSource
                VM.VideoView.Video_Pass_Items = new List<string>()
                {
                    "1 Pass",
                };

                // Populate ComboBox from ItemsSource
                //VM.VideoView.Video_Pass_Items = VideoControls.Video_Pass_ItemsSource;

                // Select Item
                VM.VideoView.Video_Pass_SelectedItem = "1 Pass";
            }


            // -------------------------
            // Display Bit Rate in TextBox
            // -------------------------
            Controls.Video.Controls.VideoBitRateDisplay(VM.VideoView.Video_Quality_Items,
                                                       VM.VideoView.Video_Quality_SelectedItem,
                                                       VM.VideoView.Video_Pass_SelectedItem);
        }

        /// <summary>
        /// Video VBR Toggle - Unchecked
        /// </summary>
        private void tglVideo_VBR_Unchecked(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Quality Controls
            // -------------------------
            Controls.Video.Controls.QualityControls();

            // -------------------------
            // MPEG-2 / MPEG-4 CBR Reset
            // -------------------------
            if (VM.VideoView.Video_Codec_SelectedItem == "MPEG-2" ||
                VM.VideoView.Video_Codec_SelectedItem == "MPEG-4")
            {
                // Change ItemsSource
                VM.VideoView.Video_Pass_Items = new List<string>()
                {
                    "2 Pass",
                    "1 Pass",
                };

                // Populate ComboBox from ItemsSource
                //cboVideo_Pass.ItemsSource = VideoControls.Video_Pass_ItemsSource;

                // Select Item
                VM.VideoView.Video_Pass_SelectedItem = "2 Pass";
            }

            // -------------------------
            // Display Bit Rate in TextBox
            // -------------------------
            Controls.Video.Controls.VideoBitRateDisplay(VM.VideoView.Video_Quality_Items,
                                                       VM.VideoView.Video_Quality_SelectedItem,
                                                       VM.VideoView.Video_Pass_SelectedItem);
        }


        /// <summary>
        /// Pixel Format
        /// </summary>
        private void cboVideo_PixelFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Output Path Update Display
            // -------------------------
            OutputPath_UpdateDisplay();
        }


        /// <summary>
        /// FPS ComboBox
        /// </summary>
        private void cboVideo_FPS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Custom ComboBox Editable
            // -------------------------
            if (VM.VideoView.Video_FPS_SelectedItem == "Custom" ||
                string.IsNullOrWhiteSpace(VM.VideoView.Video_FPS_SelectedItem))
            {
                VM.VideoView.Video_FPS_IsEditable = true;
            }

            // -------------------------
            // Other Items Disable Editable
            // -------------------------
            if (VM.VideoView.Video_FPS_SelectedItem != "Custom" &&
                !string.IsNullOrWhiteSpace(VM.VideoView.Video_FPS_SelectedItem))
            {
                VM.VideoView.Video_FPS_IsEditable = false;
            }

            // -------------------------
            // Maintain Editable Combobox while typing
            // -------------------------
            if (VM.VideoView.Video_FPS_IsEditable == true)
            {
                VM.VideoView.Video_FPS_IsEditable = true;

                // Clear Custom Text
                VM.VideoView.Video_FPS_SelectedIndex = -1;
            }

            // -------------------------
            // Output Path Update Display
            // -------------------------
            OutputPath_UpdateDisplay();
        }

        // Speed Custom KeyDown
        private void cboVideo_FrameRate_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers and Backspace
            // Deny Symbols (Shift + Number)
            // Allow Forward Slash
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back ||
                Keyboard.IsKeyDown(Key.LeftShift) && (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                Keyboard.IsKeyDown(Key.RightShift) && (e.Key >= Key.D0 && e.Key <= Key.D9)
                )
            {
                e.Handled = true;
            }

            //if (e.Key != Key.OemQuestion)
            //{
            //    e.Handled = true;
            //}
        }


        /// <summary>
        /// Speed ComboBox
        /// </summary>
        private void cboVideo_Speed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Custom ComboBox Editable
            // -------------------------
            if (VM.VideoView.Video_Speed_SelectedItem == "Custom" ||
                string.IsNullOrWhiteSpace(VM.VideoView.Video_Speed_SelectedItem))
            {
                VM.VideoView.Video_Speed_IsEditable = true;
            }

            // -------------------------
            // Other Items Disable Editable
            // -------------------------
            if (VM.VideoView.Video_Speed_SelectedItem != "Custom" &&
                !string.IsNullOrWhiteSpace(VM.VideoView.Video_Speed_SelectedItem))
            {
                VM.VideoView.Video_Speed_IsEditable = false;
            }

            // -------------------------
            // Maintain Editable Combobox while typing
            // -------------------------
            if (VM.VideoView.Video_Speed_IsEditable == true)
            {
                VM.VideoView.Video_Speed_IsEditable = true;

                // Clear Custom Text
                VM.VideoView.Video_Speed_SelectedIndex = -1;
            }
        }

        // Speed Custom KeyDown
        private void cboVideo_Speed_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers and Backspace
            Allow_Only_Number_Keys(e);
        }


        /// <summary>
        /// Presets
        /// </summary>
        private void cboPreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Profiles.Presets.SetPreset();
        }

        /// <summary>
        /// Delete Preset - Button
        /// </summary>
        private void btnDeletePreset_Click(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Set Preset Dir, Name, Ext
            // -------------------------
            string presetsDir = Path.GetDirectoryName(/*@Profiles.presetsDir)*/VM.ConfigureView.CustomPresetsPath_Text).TrimEnd('\\') + @"\";
            string presetFileName = Path.GetFileNameWithoutExtension(VM.MainView.Preset_SelectedItem);
            string presetExt = Path.GetExtension(".ini");
            string preset = presetsDir + presetFileName + presetExt;

            // -------------------------
            // Get Selected Preset Type
            // -------------------------
            string type = VM.MainView.Preset_Items.FirstOrDefault(item => item.Name == VM.MainView.Preset_SelectedItem)?.Type;

            // -------------------------
            // Delete
            // -------------------------
            if (type == "Custom")
            {
                // Yes/No Dialog Confirmation
                //
                MessageBoxResult resultExport = MessageBox.Show("Delete " + presetFileName + "?",
                                                                "Delete Confirm",
                                                                MessageBoxButton.YesNo,
                                                                MessageBoxImage.Information);
                switch (resultExport)
                {
                    // Yes
                    case MessageBoxResult.Yes:

                        // Delete
                        if (File.Exists(preset))
                        {
                            try
                            {
                                File.Delete(preset);
                            }
                            catch
                            {
                                MessageBox.Show("Could not delete Preset. May be missing or requires Administrator Privileges.",
                                                "Error",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Error);
                            }

                            // Set the Index
                            var selectedIndex = VM.MainView.Preset_SelectedIndex;

                            // Select Default Item
                            VM.MainView.Preset_SelectedItem = "Preset";

                            // Delete from Items Source
                            // (needs to be after SelectedItem change to prevent error reloading)
                            try
                            {
                                VM.MainView.Preset_Items.RemoveAt(selectedIndex);
                            }
                            catch
                            {

                            }

                            // Load Custom Presets
                            // Refresh Presets ComboBox
                            Profiles.Profiles.LoadCustomPresets();
                        }
                        else
                        {
                            MessageBox.Show("The Preset does not exist.",
                                            "Notice",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Warning);
                        }

                        break;

                    // No
                    case MessageBoxResult.No:
                        break;
                }
            }

            // -------------------------
            // Not Custom
            // -------------------------
            else
            {
                MessageBox.Show("This is not a Custom Preset.",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }


        /// <summary>
        /// Save Preset - Button
        /// </summary>
        private void btnSavePreset_Click(object sender, RoutedEventArgs e)
        {
            // Check if Profiles Directory exists
            // Check if Custom Presets Path is valid
            if (MainWindow.IsValidPath(VM.ConfigureView.CustomPresetsPath_Text) == false)
            {
                return;
            }

            // If not, create it
            if (!Directory.Exists(VM.ConfigureView.CustomPresetsPath_Text))
            {
                // Yes/No Dialog Confirmation
                //
                MessageBoxResult resultExport = MessageBox.Show("Presets folder does not exist. Automatically create it?",
                                                                "Directory Not Found",
                                                                MessageBoxButton.YesNo,
                                                                MessageBoxImage.Information);
                switch (resultExport)
                {
                    // Create
                    case MessageBoxResult.Yes:
                        try
                        {
                            Directory.CreateDirectory(VM.ConfigureView.CustomPresetsPath_Text);
                        }
                        catch
                        {
                            MessageBox.Show("Could not create Profiles folder. May require Administrator privileges.",
                                            "Error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);
                        }
                        break;
                    // Use Default
                    case MessageBoxResult.No:
                        break;
                }
            }

            // Open 'Save File'
            Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();

            // Defaults
            saveFile.InitialDirectory = VM.ConfigureView.CustomPresetsPath_Text;
            saveFile.RestoreDirectory = true;
            saveFile.Filter = "Initialization Files (*.ini)|*.ini";
            saveFile.DefaultExt = ".ini";
            saveFile.FileName = "Custom Preset.ini";

            // Show save file dialog box
            Nullable<bool> result = saveFile.ShowDialog();

            // Set Preset Dir, Name, Ext
            string presetsDir = Path.GetDirectoryName(saveFile.FileName).TrimEnd('\\') + @"\";
            string presetFileName = Path.GetFileNameWithoutExtension(saveFile.FileName);
            string presetExt = Path.GetExtension(saveFile.FileName);
            string preset = presetsDir + presetFileName + presetExt;
            //string presets = Path.Combine(presetsDir, presetsFileName);

            // Process dialog box
            if (result == true)
            {
                //// Set Preset Dir, Name, Ext
                //string presetsDir = Path.GetDirectoryName(saveFile.FileName).TrimEnd('\\') + @"\";
                //string presetFileName = Path.GetFileNameWithoutExtension(saveFile.FileName);
                //string presetExt = Path.GetExtension(saveFile.FileName);
                //string preset = presetsDir + presetFileName + presetExt;
                ////string presets = Path.Combine(presetsDir, presetsFileName);

                // -------------------------
                // Overwriting doesn't work properly with INI Writer
                // Delete File instead before saving new
                // -------------------------
                if (File.Exists(preset))
                {
                    try
                    {
                        File.Delete(preset);
                    }
                    catch
                    {

                    }
                }

                // -------------------------
                // Save Custom Preset ini file
                // -------------------------
                Profiles.Profiles.ExportPreset(preset);

                // -------------------------
                // Load Custom Presets
                // Refresh Presets ComboBox
                // -------------------------
                Profiles.Profiles.LoadCustomPresets();

                // -------------------------
                // Select the Preset
                // -------------------------
                VM.MainView.Preset_SelectedItem = presetFileName;
            }
            else
            {
                // -------------------------
                // Load Custom Presets
                // Refresh Presets ComboBox
                // -------------------------
                Profiles.Profiles.LoadCustomPresets();

                if (string.IsNullOrWhiteSpace(VM.MainView.Preset_SelectedItem))
                {
                    VM.MainView.Preset_SelectedItem = "Preset";
                }
            }

        }


        /// <summary>
        /// Video Optimize Combobox
        /// </summary>
        private void cboVideo_Optimize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Optimize Controls
            // -------------------------
            Controls.Video.Controls.OptimizeControls();
        }

        /// <summary>
        /// Video Optimize Expander
        /// </summary>
        //// Expanded
        //private void expVideo_Optimize_Expander_Expanded(object sender, RoutedEventArgs e)
        //{

        //}
        //// Collapsed
        //private void expVideo_Optimize_Expander_Collapsed(object sender, RoutedEventArgs e)
        //{

        //}
        //// Mouse Down
        //private void expVideo_Optimize_Expander_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{

        //}
        //// Mouse Up
        //private void expVideo_Optimize_Expander_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        //{

        //}


        /// <summary>
        /// Video Color Range Combobox
        /// </summary>
        private void cboColorRange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //VideoControls.AutoCopyVideoCodec("control");
        }



        private void cboVideo_Scale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // --------------------------------------------------
            // Enable/Disable Size
            // --------------------------------------------------
            // -------------------------
            // Custom
            // -------------------------
            if (VM.VideoView.Video_Scale_SelectedItem == "Custom")
            {
                VM.VideoView.Video_Width_IsEnabled = true;
                VM.VideoView.Video_Height_IsEnabled = true;

                VM.VideoView.Video_ScalingAlgorithm_IsEnabled = true;
            }

            // -------------------------
            // Source
            // -------------------------
            else if (VM.VideoView.Video_Scale_SelectedItem == "Source")
            {
                VM.VideoView.Video_Width_IsEnabled = false;
                VM.VideoView.Video_Height_IsEnabled = false;

                VM.VideoView.Video_ScalingAlgorithm_IsEnabled = false;
            }

            // -------------------------
            // All Other Sizes
            // -------------------------
            else
            {
                VM.VideoView.Video_Width_IsEnabled = false;
                VM.VideoView.Video_Height_IsEnabled = false;

                VM.VideoView.Video_ScalingAlgorithm_IsEnabled = true;
            }


            // -------------------------
            // Update Width/Height TextBox Display
            // -------------------------
            VideoScaleDisplay();
        }



        /// <summary>
        /// Video Scale Display
        /// </summary>
        /// <remarks>
        /// If Input Video is Widescreen (16:9, 16:10, etc) or auto, scale by Width -vf "scale=1920:-2" 
        /// If Input Video is Full Screen (4:3, 5:4, etc), scale by Height -vf "scale=-2:1080" 
        /// </remarks>
        public static void VideoScaleDisplay()
        {
            switch (VM.VideoView.Video_Scale_SelectedItem)
            {
                // --------------------------------------------------
                // Change TextBox Resolution Numbers
                // --------------------------------------------------
                // -------------------------
                // Source
                // -------------------------
                case "Source":
                    VM.VideoView.Video_Width_Text = "auto";
                    VM.VideoView.Video_Height_Text = "auto";

                    VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                    break;

                // -------------------------
                // 8K
                // -------------------------
                case "8K":
                    // Widescreen
                    if (VM.VideoView.Video_ScreenFormat_SelectedItem == "auto" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Ultrawide"
                        )
                    {
                        VM.VideoView.Video_Width_Text = "8192";
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // Full Screen
                    else if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Full Screen")
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                        VM.VideoView.Video_Height_Text = "4320";
                    }
                    break;

                // -------------------------
                // 8K UHD
                // -------------------------
                case "8K UHD":
                    // Widescreen
                    if (VM.VideoView.Video_ScreenFormat_SelectedItem == "auto" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Ultrawide"
                        )
                    {
                        VM.VideoView.Video_Width_Text = "7680";
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // Full Screen
                    else if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Full Screen")
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                        VM.VideoView.Video_Height_Text = "4320";
                    }
                    break;

                // -------------------------
                // 4K
                // -------------------------
                case "4K":
                    // Widescreen
                    if (VM.VideoView.Video_ScreenFormat_SelectedItem == "auto" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Ultrawide"
                        )
                    {
                        VM.VideoView.Video_Width_Text = "4096";
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // Full Screen
                    else if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Full Screen")
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                        VM.VideoView.Video_Height_Text = "2160";
                    }
                    break;

                // -------------------------
                // 4K UHD
                // -------------------------
                case "4K UHD":
                    // Widescreen
                    if (VM.VideoView.Video_ScreenFormat_SelectedItem == "auto" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Ultrawide"
                        )
                    {
                        VM.VideoView.Video_Width_Text = "3840";
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // Full Screen
                    else if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Full Screen")
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                        VM.VideoView.Video_Height_Text = "2160";
                    }
                    break;

                // -------------------------
                // 2K
                // -------------------------
                case "2K":
                    // Widescreen
                    if (VM.VideoView.Video_ScreenFormat_SelectedItem == "auto" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Ultrawide"
                        )
                    {
                        VM.VideoView.Video_Width_Text = "2048";
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // Full Screen
                    else if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Full Screen")
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                        VM.VideoView.Video_Height_Text = "1556";
                    }
                    break;

                // -------------------------
                // 1600p
                // -------------------------
                case "1600p":
                    // Widescreen
                    if (VM.VideoView.Video_ScreenFormat_SelectedItem == "auto" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Ultrawide"
                        )
                    {

                        VM.VideoView.Video_Width_Text = "2560";
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // Full Screen
                    else if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Full Screen")
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                        VM.VideoView.Video_Height_Text = "1600";
                    }
                    break;

                // -------------------------
                // 1440p
                // -------------------------
                case "1440p":
                    // Widescreen
                    if (VM.VideoView.Video_ScreenFormat_SelectedItem == "auto" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Ultrawide"
                        )
                    {

                        VM.VideoView.Video_Width_Text = "2560";
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // Full Screen
                    else if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Full Screen")
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                        VM.VideoView.Video_Height_Text = "1440";
                    }
                    break;

                // -------------------------
                // 1200p
                // -------------------------
                case "1200p":
                    // Widescreen
                    if (VM.VideoView.Video_ScreenFormat_SelectedItem == "auto" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Ultrawide"
                        )
                    {
                        VM.VideoView.Video_Width_Text = "1920";
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // Full Screen
                    else if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Full Screen")
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                        VM.VideoView.Video_Height_Text = "1200";
                    }
                    break;

                // -------------------------
                // 1080p
                // -------------------------
                case "1080p":
                    // Widescreen
                    if (VM.VideoView.Video_ScreenFormat_SelectedItem == "auto" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Ultrawide"
                        )
                    {
                        VM.VideoView.Video_Width_Text = "1920";
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // Full Screen
                    else if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Full Screen")
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                        VM.VideoView.Video_Height_Text = "1080";
                    }
                    break;

                // -------------------------
                // 900p
                // -------------------------
                case "900p":
                    // Widescreen
                    if (VM.VideoView.Video_ScreenFormat_SelectedItem == "auto" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Ultrawide"
                        )
                    {
                        VM.VideoView.Video_Width_Text = "1600";
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // Full Screen
                    else if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Full Screen")
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                        VM.VideoView.Video_Height_Text = "900";
                    }
                    break;

                // -------------------------
                // 720p
                // -------------------------
                case "720p":
                    // Widescreen
                    if (VM.VideoView.Video_ScreenFormat_SelectedItem == "auto" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Ultrawide"
                        )
                    {
                        VM.VideoView.Video_Width_Text = "1280";
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // Full Screen
                    else if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Full Screen")
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                        VM.VideoView.Video_Height_Text = "720";
                    }
                    break;

                // -------------------------
                // 576p
                // -------------------------
                case "576p":
                    // Widescreen
                    if (VM.VideoView.Video_ScreenFormat_SelectedItem == "auto" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Ultrawide"
                        )
                    {
                        VM.VideoView.Video_Width_Text = "1024";
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // Full Screen
                    else if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Full Screen")
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                        VM.VideoView.Video_Height_Text = "576";
                    }
                    break;

                // -------------------------
                // 480p
                // -------------------------
                case "480p":
                    // Widescreen
                    if (VM.VideoView.Video_ScreenFormat_SelectedItem == "auto" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Ultrawide"
                        )
                    {
                        VM.VideoView.Video_Width_Text = "720";
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // Full Screen
                    else if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Full Screen")
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                        VM.VideoView.Video_Height_Text = "480";
                    }
                    break;

                // -------------------------
                // 320p
                // -------------------------
                case "320p":
                    // Widescreen
                    if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Ultrawide"
                        )
                    {
                        VM.VideoView.Video_Width_Text = "480";
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // Full Screen
                    else if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Full Screen")
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                        VM.VideoView.Video_Height_Text = "320";
                    }
                    break;

                // -------------------------
                // 240p
                // -------------------------
                case "240p":
                    // Widescreen
                    if (VM.VideoView.Video_ScreenFormat_SelectedItem == "auto" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                        VM.VideoView.Video_ScreenFormat_SelectedItem == "Ultrawide"
                        )
                    {
                        VM.VideoView.Video_Width_Text = "320";
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // Full Screen
                    else if (VM.VideoView.Video_ScreenFormat_SelectedItem == "Full Screen")
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                        VM.VideoView.Video_Height_Text = "240";
                    }
                    break;

                // -------------------------
                // Custom
                // -------------------------
                case "Custom":
                    VM.VideoView.Video_Width_Text = "auto";
                    VM.VideoView.Video_Height_Text = "auto";
                    break;
            }
        }


        /// <summary>
        /// Is Aspect Ratio Widescreen
        /// </summary>
        //public static bool IsAspectRatioWidescreen(string aspectRatio_SelectedItem)
        //{
        //    // Widescreen (16:9, 16:10, etc) or auto, scale by Width 
        //    if (aspectRatio_SelectedItem == "auto" ||
        //        aspectRatio_SelectedItem == "14:10" ||
        //        aspectRatio_SelectedItem == "16:9" ||
        //        aspectRatio_SelectedItem == "16:10" ||
        //        aspectRatio_SelectedItem == "19:10" ||
        //        aspectRatio_SelectedItem == "21:9" ||
        //        aspectRatio_SelectedItem == "32:9" ||
        //        aspectRatio_SelectedItem == "240:100"
        //        )
        //    {
        //        return true;
        //    }

        //    // Full Screen (4:3, 5:4, etc), scale by Height
        //    else
        //    {
        //        return false;
        //    }
        //}


        /// <summary>
        /// Width Textbox Change
        /// </summary>
        // Got Focus
        private void tbxVideo_Width_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxVideo_Width.Focus() == true &&
                VM.VideoView.Video_Width_Text == "auto")
            {
                VM.VideoView.Video_Width_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxVideo_Width_LostFocus(object sender, RoutedEventArgs e)
        {
            VM.VideoView.Video_Width_Text = tbxVideo_Width.Text;

            // Change textbox back to "auto" if left empty
            if (string.IsNullOrWhiteSpace(VM.VideoView.Video_Width_Text))
            {
                VM.VideoView.Video_Width_Text = "auto";
            }
        }

        /// <summary>
        /// Height Textbox Change
        /// </summary>
        // Got Focus
        private void tbxVideo_Height_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxVideo_Height.Focus() == true &&
                VM.VideoView.Video_Height_Text == "auto")
            {
                VM.VideoView.Video_Height_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxVideo_Height_LostFocus(object sender, RoutedEventArgs e)
        {
            VM.VideoView.Video_Height_Text = tbxVideo_Height.Text;

            // Change textbox back to "height" if left empty
            if (string.IsNullOrWhiteSpace(VM.VideoView.Video_Height_Text))
            {
                VM.VideoView.Video_Height_Text = "auto";
            }
        }


        /// <summary>
        /// Video Screen Format
        /// </summary>
        private void cboVideo_ScreenFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VM.VideoView.Video_Scale_SelectedItem != "Custom")
            {
                VideoScaleDisplay();
            }
        }


        /// <summary>
        /// Video Aspect Ratio
        /// </summary>
        private void cboVideo_AspectRatio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        /// <summary>
        /// Video Scaling Algorithm
        /// </summary>
        private void cboVideo_ScalingAlgorithm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        /// <summary>
        /// Crop Window - Button
        /// </summary>
        private void btnVideo_Crop_Click(object sender, RoutedEventArgs e)
        {
            // Start Window
            cropwindow = new CropWindow(this);

            // Detect which screen we're on
            var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
            var thisScreen = allScreens.SingleOrDefault(s => Left >= s.WorkingArea.Left && Left < s.WorkingArea.Right);

            // Position Relative to MainWindow
            // Keep from going off screen
            cropwindow.Left = Math.Max((Left + (Width - cropwindow.Width) / 2), thisScreen.WorkingArea.Left);
            cropwindow.Top = Math.Max(Top - cropwindow.Height - 12, thisScreen.WorkingArea.Top);

            // Keep Window on Top
            cropwindow.Owner = Window.GetWindow(this);

            // Open Window
            cropwindow.ShowDialog();
        }


        /// <summary>
        /// Crop Clear Button
        /// </summary>
        private void btnVideo_CropClear_Click(object sender, RoutedEventArgs e)
        {
            // Clear Crop Values
            CropWindow.CropClear();
        }

    }
}
