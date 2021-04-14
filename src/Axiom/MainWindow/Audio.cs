/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2021 Matt McManis
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using ViewModel;
using System.IO;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Audio Codec - ComboBox
        /// </summary>
        private void cboAudio_Codec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string audio_Codec_SelectedItem = (sender as ComboBox).SelectedItem as string;
            //MessageBox.Show(audio_Codec_SelectedItem); //debug

            // -------------------------
            // Halt if Selected Codec is Null
            // -------------------------
            //if (string.IsNullOrWhiteSpace(audio_Codec_SelectedItem))
            //{
            //    return;
            //}

            // -------------------------
            // Get Input/Output Extensions
            // -------------------------
            //string inputExt = Path.GetExtension(VM.MainView.Input_Text);
            //string outputExt = "." + VM.FormatView.Format_Container_SelectedItem;

            // -------------------------
            // Save Selected Quality
            // When you change the Quality ComboBox from Auto to 320, it triggers the Codec ComboBox to change from Copy to x264,
            // in turn changing the Quality ComboBox back to Auto on the Codec switch
            // -------------------------
            //string userSelected_AudioQuality = string.Empty;
            //if (string.IsNullOrWhiteSpace(inputExt) || 
            //    string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
            //{
            //    if (audio_Codec_SelectedItem != "Copy" && 
            //        VM.AudioView.Audio_Quality_SelectedItem != "Auto")
            //    {
            //        userSelected_AudioQuality = VM.AudioView.Audio_Quality_SelectedItem;
            //    }
            //}

            // -------------------------
            // Set Copy Quality to Auto
            // -------------------------
            //if (string.IsNullOrWhiteSpace(inputExt) ||
            //    string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
            //{
            //    if (audio_Codec_SelectedItem == "Copy")
            //    {
            //        VM.AudioView.Audio_Quality_SelectedItem = "Auto";
            //    }
            //}

            // -------------------------
            // Codec Controls
            // -------------------------
            Controls.Audio.Controls.CodecControls(audio_Codec_SelectedItem);

            // -------------------------
            // Media Type Controls
            // Overrides Codec Controls
            // -------------------------
            // Must be after Codec Controls
            Controls.Format.Controls.MediaTypeControls();

            // -------------------------
            // Re-Select the Quality Preset
            // -------------------------
            // Copy Codec -> VP8, x264, etc Codec
            //if (string.IsNullOrWhiteSpace(inputExt) ||
            //    string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
            //{
            //    if (audio_Codec_SelectedItem != "Copy")
            //    {
            //        if (!string.IsNullOrWhiteSpace(userSelected_AudioQuality))
            //        {
            //            VM.AudioView.Audio_Quality_SelectedItem = userSelected_AudioQuality;
            //        }
            //    }
            //    // Set to Top of Item List: Auto or None
            //    else
            //    {
            //        VM.AudioView.Audio_Quality_SelectedIndex = 0;
            //    }
            //}

            // -------------------------
            // Audio Stream Controls
            // -------------------------
            Controls.Format.Controls.AudioStreamControls();

            // -------------------------
            // Convert Button Text Change
            // -------------------------
            ConvertButtonText();

            // -------------------------
            // Output Path Update Display
            // -------------------------
            //OutputPath_UpdateDisplay();
        }

        /// <summary>
        /// Audio Stream - ComboBox
        /// </summary>
        private void cboAudio_Stream_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Mux
            // -------------------------
            if (VM.AudioView.Audio_Stream_SelectedItem == "mux")
            {
                // Enable Audio Mux ListView and Buttons
                VM.AudioView.Audio_ListView_IsEnabled = true;

                VM.AudioView.Audio_ListView_Opacity = 1;
            }
            else
            {
                // Disable Audio Mux ListView and Buttons
                VM.AudioView.Audio_ListView_IsEnabled = false;

                VM.AudioView.Audio_ListView_Opacity = 0.15;
            }
        }


        /// <summary>
        /// Audio Channel - ComboBox
        /// </summary>
        private void cboAudio_Channel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Output Path Update Display
            // -------------------------
            //OutputPath_UpdateDisplay();
        }


        /// <summary>
        /// Audio Quality - ComboBox
        /// </summary>
        private void cboAudio_Quality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //(sender as ComboBox).SelectedItem as string;
            //string audio_Quality_SelectedItem = (sender as ViewModel.Audio.AudioQuality).Name as string;
            //MessageBox.Show(audio_Quality_SelectedItem);

            // -------------------------
            // Halt if: Selected Codec is Null
            //          Selected Quality is Null
            // -------------------------
            //if (string.IsNullOrWhiteSpace(VM.AudioView.Audio_Quality_SelectedItem))
            //{
            //    return;
            //}

            // -------------------------
            // Set Controls
            // -------------------------
            //Controls.Audio.Controls.SetControls(VM.AudioView.Audio_Codec_SelectedItem);

            // -------------------------
            // Quality Controls
            // -------------------------
            Controls.Audio.Controls.QualityControls();

            // -------------------------
            // Display Bit Rate in TextBox
            // -------------------------
            Controls.Audio.Controls.AudioBitRateDisplay(VM.AudioView.Audio_Quality_Items,
                                                        VM.AudioView.Audio_Quality_SelectedItem
                                                        );

            // -------------------------
            // Output Path Update Display
            // -------------------------
            //OutputPath_UpdateDisplay();

            // -------------------------
            // Set Video and AudioCodec Combobox to "Copy" if 
            // Input File Extension is Same as Output File Extension 
            // and Quality is Auto
            // -------------------------
            //Controls.Audio.Controls.AutoCopyAudioCodec("control");
        }

        /// <summary>
        /// Audio VBR - TextBox
        /// </summary>
        private void tbxAudio_BitRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            // -------------------------
            // Output Path Update Display
            // -------------------------
            //OutputPath_UpdateDisplay();
        }

        /// <summary>
        /// Audio VBR - Toggle
        /// </summary>
        // Checked
        private void tglAudio_VBR_Checked(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Quality Controls
            // -------------------------
            //Controls.Audio.Controls.QualityControls();

            // -------------------------
            // Display Bit Rate in TextBox
            // -------------------------
            Controls.Audio.Controls.AudioBitRateDisplay(VM.AudioView.Audio_Quality_Items,
                                                        VM.AudioView.Audio_Quality_SelectedItem
                                                       );

            //// -------------------------
            //// Compression Level
            //// -------------------------
            //if (VM.AudioView.Audio_Codec_SelectedItem == "Opus")
            //{
            //    VM.AudioView.Audio_CompressionLevel_IsEnabled = true;
            //}

            // -------------------------
            // Output Path Update Display
            // -------------------------
            //OutputPath_UpdateDisplay();
        }
        // Unchecked
        private void tglAudio_VBR_Unchecked(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Quality Controls
            // -------------------------
            //Controls.Audio.Controls.QualityControls();

            // -------------------------
            // Display Bit Rate in TextBox
            // -------------------------
            Controls.Audio.Controls.AudioBitRateDisplay(VM.AudioView.Audio_Quality_Items,
                                                        VM.AudioView.Audio_Quality_SelectedItem
                                                       );

            // -------------------------
            // Compression Level
            // -------------------------
            if (VM.AudioView.Audio_Codec_SelectedItem == "Opus")
            {
                VM.AudioView.Audio_CompressionLevel_IsEnabled = false;
            }
        }


        /// <summary>
        /// Audio Custom BitRate kbps - Textbox
        /// </summary>
        private void tbxAudio_BitRate_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers and Backspace
            Allow_Only_Number_Keys(e);
        }
        // Got Focus
        private void tbxAudio_BitRate_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear Textbox on first use
            if (VM.AudioView.Audio_BitRate_Text == string.Empty)
            {
                TextBox tbac = (TextBox)sender;
                tbac.Text = string.Empty;
                tbac.GotFocus += tbxAudio_BitRate_GotFocus; //used to be -=
            }
        }
        // Lost Focus
        private void tbxAudio_BitRate_LostFocus(object sender, RoutedEventArgs e)
        {
            // Change Textbox back to kbps
            TextBox tbac = sender as TextBox;
            if (tbac.Text.Trim().Equals(string.Empty))
            {
                tbac.Text = string.Empty;
                tbac.GotFocus -= tbxAudio_BitRate_GotFocus; //used to be +=
            }
        }


        /// <summary>
        /// Samplerate ComboBox
        /// </summary>
        private void cboAudio_SampleRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(VM.AudioView.Audio_SampleRate_SelectedItem))
            //{
            //    Audio_SampleRate_PreviousItem = VM.AudioView.Audio_SampleRate_SelectedItem;
            //}

            //MessageBox.Show("Previous: " + Audio_SampleRate_PreviousItem); //debug
            //MessageBox.Show("Current: " + VM.AudioView.Audio_SampleRate_SelectedItem); //debug

            //if (Audio_SampleRate_PreviousItem != VM.AudioView.Audio_SampleRate_SelectedItem)
            //{
            //    // Switch to Copy if inputExt & outputExt match
            //    //Controls.Audio.Controls.AutoCopyAudioCodec("control");
            //}

            //MessageBox.Show("Current Changed: " + VM.AudioView.Audio_SampleRate_SelectedItem); //debug

            //Controls.Audio.Controls.AutoCopyAudioCodec("control");

            // -------------------------
            // Output Path Update Display
            // -------------------------
            //OutputPath_UpdateDisplay();
        }


        /// <summary>
        /// Bit Depth ComboBox
        /// </summary>
        private void cboAudio_BitDepth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (VM.AudioView.Audio_Codec_SelectedItem == "PCM")
            //{
            //    //Controls.Audio.Codec.PCM pcm = new Controls.Audio.Codec.PCM();
            //    //pcm.Codec_Set();

            //    Controls.Audio.Codec.PCM.Codec_Set();
            //}

            // -------------------------
            // Output Path Update Display
            // -------------------------
            //OutputPath_UpdateDisplay();
        }


        /// <summary>
        /// Volume TextBox Changed
        /// </summary>
        private void tbxAudio_Volume_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        /// <summary>
        /// Volume TextBox KeyDown
        /// </summary>
        private void tbxAudio_Volume_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers and Backspace
            Allow_Only_Number_Keys(e);
        }

        /// <summary>
        /// Volume Buttons
        /// </summary>
        // -------------------------
        // Up
        // -------------------------
        // Volume Up Button Click
        private void btnAudio_VolumeUp_Click(object sender, RoutedEventArgs e)
        {
            int value;
            int.TryParse(VM.AudioView.Audio_Volume_Text, out value);

            value += 1;
            VM.AudioView.Audio_Volume_Text = value.ToString();
        }
        // Up Button Each Timer Tick
        private void dispatcherTimerUp_Tick(object sender, EventArgs e)
        {
            int value;
            int.TryParse(VM.AudioView.Audio_Volume_Text, out value);

            value += 1;
            VM.AudioView.Audio_Volume_Text = value.ToString();
        }
        // Hold Up Button
        private void btnAudio_VolumeUp_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Timer      
            dispatcherTimerUp.Interval = new TimeSpan(0, 0, 0, 0, 100); //100ms
            dispatcherTimerUp.Start();
        }
        // Up Button Released
        private void btnAudio_VolumeUp_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Disable Timer
            dispatcherTimerUp.Stop();
        }
        // -------------------------
        // Down
        // -------------------------
        // Volume Down Button Click
        private void btnAudio_VolumeDown_Click(object sender, RoutedEventArgs e)
        {
            int value;
            int.TryParse(VM.AudioView.Audio_Volume_Text, out value);

            value -= 1;
            VM.AudioView.Audio_Volume_Text = value.ToString();
        }
        // Down Button Each Timer Tick
        private void dispatcherTimerDown_Tick(object sender, EventArgs e)
        {
            int value;
            int.TryParse(VM.AudioView.Audio_Volume_Text, out value);

            value -= 1;
            VM.AudioView.Audio_Volume_Text = value.ToString();
        }
        // Hold Down Button
        private void btnAudio_VolumeDown_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Timer      
            dispatcherTimerDown.Interval = new TimeSpan(0, 0, 0, 0, 100); //100ms
            dispatcherTimerDown.Start();
        }
        // Down Button Released
        private void btnAudio_VolumeDown_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Disable Timer
            dispatcherTimerDown.Stop();
        }


        /// <summary>
        /// Audio Hard Limiter - Slider
        /// </summary>
        private void slAudio_HardLimiter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.AudioView.Audio_HardLimiter_Value = 0.0;

            //Controls.Audio.Controls.AutoCopyAudioCodec("control");
        }

        private void slAudio_HardLimiter_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            //Controls.Audio.Controls.AutoCopyAudioCodec("control");
        }

        private void tbxAudio_HardLimiter_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            //Controls.Audio.Controls.AutoCopyAudioCodec("control");
        }


        /// <summary>
        /// Audio Mux - ListView
        /// </summary>
        private void lstvAudio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // ListView
            // -------------------------
            // Clear before adding new selected items
            if (VM.AudioView.Audio_ListView_SelectedItems != null &&
                VM.AudioView.Audio_ListView_SelectedItems.Count > 0)
            {
                VM.AudioView.Audio_ListView_SelectedItems.Clear();
                VM.AudioView.Audio_ListView_SelectedItems.TrimExcess();
            }

            // Create Selected Items List for ViewModel
            VM.AudioView.Audio_ListView_SelectedItems = lstvAudio.SelectedItems
                                                                 .Cast<string>()
                                                                 .ToList();

            // -------------------------
            // Set Metadata
            // -------------------------
            int selectedIndex = VM.AudioView.Audio_ListView_SelectedIndex;

            // Title
            if (Generate.Audio.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
            {
                tbxAudio_Metadata_Title.Text = Generate.Audio.Metadata.titleList[selectedIndex];
            }
            else
            {
                tbxAudio_Metadata_Title.Text = string.Empty;
            }

            // Language
            if (Generate.Audio.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
            {
                VM.AudioView.Audio_Metadata_Language_SelectedItem = Generate.Audio.Metadata.languageList[selectedIndex];

                // default
                if (string.IsNullOrWhiteSpace(VM.AudioView.Audio_Metadata_Language_SelectedItem))
                {
                    VM.AudioView.Audio_Metadata_Language_SelectedItem = "none";
                }
            }
            else
            {
                VM.AudioView.Audio_Metadata_Language_SelectedItem = "none";
            }

            // Delay
            if (Generate.Audio.Metadata.delayList.ElementAtOrDefault(selectedIndex) != null)
            {
                VM.AudioView.Audio_Delay_Text = Generate.Audio.Metadata.delayList[selectedIndex];
            }
            else
            {
                VM.AudioView.Audio_Delay_Text = string.Empty;
            }
        }

        /// <summary>
        /// Audio Add - Button
        /// </summary>
        private void btnAudio_Add_Click(object sender, RoutedEventArgs e)
        {
            // Open Select File Window
            //Microsoft.Win32.OpenFileDialog selectFiles = new Microsoft.Win32.OpenFileDialog();
            Microsoft.Win32.OpenFileDialog selectFiles = new Microsoft.Win32.OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                //RestoreDirectory = true,
                //ReadOnlyChecked = true,
                //ShowReadOnly = true
            };

            // Defaults
            selectFiles.Multiselect = true;
            selectFiles.Filter = "All files (*.*)|*.*";

            // Process Dialog Box
            //Nullable<bool> result = selectFiles.ShowDialog();
            //if (result == true)
            if (selectFiles.ShowDialog() == true)
            {
                // Reset
                //AudiosClear();

                // Add Selected Files to List
                for (var i = 0; i < selectFiles.FileNames.Length; i++)
                {
                    // Wrap in quotes for ffmpeg -i
                    Generate.Audio.Audio.audioFilePathsList.Add(WrapWithQuotes(selectFiles.FileNames[i]));
                    //MessageBox.Show(Video.audioFiles[i]); //debug

                    Generate.Audio.Audio.audioFileNamesList.Add(Path.GetFileName(selectFiles.FileNames[i]));

                    // ListView Display File Names + Ext
                    VM.AudioView.Audio_ListView_Items.Add(Path.GetFileName(selectFiles.FileNames[i]));

                    // Metadata Placeholders
                    // Title
                    Generate.Audio.Metadata.titleList.Add(string.Empty);

                    // Language
                    Generate.Audio.Metadata.languageList.Add(string.Empty);

                    // Delay
                    Generate.Audio.Metadata.delayList.Add(string.Empty);
                }
            }
        }

        /// <summary>
        /// Audio Remove
        /// </summary>
        private void btnAudio_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (VM.AudioView.Audio_ListView_SelectedItems.Count > 0)
            {
                var selectedIndex = VM.AudioView.Audio_ListView_SelectedIndex;

                // -------------------------
                // List View
                // -------------------------
                // ListView Items
                var itemlsvFileNames = VM.AudioView.Audio_ListView_Items[selectedIndex];
                VM.AudioView.Audio_ListView_Items.RemoveAt(selectedIndex);

                // List File Paths
                string itemFilePaths = Generate.Audio.Audio.audioFilePathsList[selectedIndex];
                Generate.Audio.Audio.audioFilePathsList.RemoveAt(selectedIndex);

                // List File Names
                string itemFileNames = Generate.Audio.Audio.audioFileNamesList[selectedIndex];
                Generate.Audio.Audio.audioFileNamesList.RemoveAt(selectedIndex);

                // -------------------------
                // Metadata
                // -------------------------
                // Title
                if (Generate.Audio.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
                {
                    Generate.Audio.Metadata.titleList.RemoveAt(selectedIndex);
                }

                // Language
                if (Generate.Audio.Metadata.languageList.ElementAtOrDefault(selectedIndex) != null)
                {
                    Generate.Audio.Metadata.languageList.RemoveAt(selectedIndex);
                }

                // Delay
                if (Generate.Audio.Metadata.delayList.ElementAtOrDefault(selectedIndex) != null)
                {
                    Generate.Audio.Metadata.delayList.RemoveAt(selectedIndex);
                }
            }
        }

        /// <summary>
        /// Audio Sort Up
        /// </summary>
        private void btnAudio_SortUp_Click(object sender, RoutedEventArgs e)
        {
            if (VM.AudioView.Audio_ListView_SelectedItems.Count > 0)
            {
                var selectedIndex = VM.AudioView.Audio_ListView_SelectedIndex;

                if (selectedIndex > 0)
                {
                    // -------------------------
                    // List View
                    // -------------------------
                    // ListView Items
                    var itemlsvFileNames = VM.AudioView.Audio_ListView_Items[selectedIndex];
                    VM.AudioView.Audio_ListView_Items.RemoveAt(selectedIndex);
                    VM.AudioView.Audio_ListView_Items.Insert(selectedIndex - 1, itemlsvFileNames);

                    // List File Paths
                    string itemFilePaths = Generate.Audio.Audio.audioFilePathsList[selectedIndex];
                    Generate.Audio.Audio.audioFilePathsList.RemoveAt(selectedIndex);
                    Generate.Audio.Audio.audioFilePathsList.Insert(selectedIndex - 1, itemFilePaths);

                    // List File Names
                    string itemFileNames = Generate.Audio.Audio.audioFileNamesList[selectedIndex];
                    Generate.Audio.Audio.audioFileNamesList.RemoveAt(selectedIndex);
                    Generate.Audio.Audio.audioFileNamesList.Insert(selectedIndex - 1, itemFileNames);

                    // -------------------------
                    // Metadata
                    // -------------------------
                    // Title
                    if (Generate.Audio.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
                    {
                        var titleItem = Generate.Audio.Metadata.titleList[selectedIndex];
                        Generate.Audio.Metadata.titleList.RemoveAt(selectedIndex);
                        Generate.Audio.Metadata.titleList.Insert(selectedIndex - 1, titleItem);
                    }

                    // Language
                    if (Generate.Audio.Metadata.languageList.ElementAtOrDefault(selectedIndex) != null)
                    {
                        var titleItem = Generate.Audio.Metadata.languageList[selectedIndex];
                        Generate.Audio.Metadata.languageList.RemoveAt(selectedIndex);
                        Generate.Audio.Metadata.languageList.Insert(selectedIndex - 1, titleItem);
                    }

                    // Delay
                    if (Generate.Audio.Metadata.delayList.ElementAtOrDefault(selectedIndex) != null)
                    {
                        var titleItem = Generate.Audio.Metadata.delayList[selectedIndex];
                        Generate.Audio.Metadata.delayList.RemoveAt(selectedIndex);
                        Generate.Audio.Metadata.delayList.Insert(selectedIndex - 1, titleItem);
                    }

                    // -------------------------
                    // Highlight Selected Index
                    // -------------------------
                    VM.AudioView.Audio_ListView_SelectedIndex = selectedIndex - 1;
                }
            }
        }

        /// <summary>
        /// Audio Sort Down
        /// </summary>
        private void btnAudio_SortDown_Click(object sender, RoutedEventArgs e)
        {
            if (VM.AudioView.Audio_ListView_SelectedItems.Count > 0)
            {
                var selectedIndex = VM.AudioView.Audio_ListView_SelectedIndex;

                if (selectedIndex + 1 < VM.AudioView.Audio_ListView_Items.Count)
                {
                    // -------------------------
                    // ListView
                    // -------------------------
                    // ListView Items
                    var itemlsvFileNames = VM.AudioView.Audio_ListView_Items[selectedIndex];
                    VM.AudioView.Audio_ListView_Items.RemoveAt(selectedIndex);
                    VM.AudioView.Audio_ListView_Items.Insert(selectedIndex + 1, itemlsvFileNames);

                    // List FilePaths
                    string itemFilePaths = Generate.Audio.Audio.audioFilePathsList[selectedIndex];
                    Generate.Audio.Audio.audioFilePathsList.RemoveAt(selectedIndex);
                    Generate.Audio.Audio.audioFilePathsList.Insert(selectedIndex + 1, itemFilePaths);

                    // List File Names
                    string itemFileNames = Generate.Audio.Audio.audioFileNamesList[selectedIndex];
                    Generate.Audio.Audio.audioFileNamesList.RemoveAt(selectedIndex);
                    Generate.Audio.Audio.audioFileNamesList.Insert(selectedIndex + 1, itemFileNames);

                    // -------------------------
                    // Metadata
                    // -------------------------
                    // Title
                    if (Generate.Audio.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
                    {
                        var titleItem = Generate.Audio.Metadata.titleList[selectedIndex];
                        Generate.Audio.Metadata.titleList.RemoveAt(selectedIndex);
                        Generate.Audio.Metadata.titleList.Insert(selectedIndex + 1, titleItem);
                    }

                    // Language
                    if (Generate.Audio.Metadata.languageList.ElementAtOrDefault(selectedIndex) != null)
                    {
                        var titleItem = Generate.Audio.Metadata.languageList[selectedIndex];
                        Generate.Audio.Metadata.languageList.RemoveAt(selectedIndex);
                        Generate.Audio.Metadata.languageList.Insert(selectedIndex + 1, titleItem);
                    }

                    // Delay
                    if (Generate.Audio.Metadata.delayList.ElementAtOrDefault(selectedIndex) != null)
                    {
                        var titleItem = Generate.Audio.Metadata.delayList[selectedIndex];
                        Generate.Audio.Metadata.delayList.RemoveAt(selectedIndex);
                        Generate.Audio.Metadata.delayList.Insert(selectedIndex + 1, titleItem);
                    }

                    // -------------------------
                    // Highlight Selected Index
                    // -------------------------
                    VM.AudioView.Audio_ListView_SelectedIndex = selectedIndex + 1;
                }
            }
        }

        /// <summary>
        /// Audio Clear All
        /// </summary>
        private void btnAudio_Clear_Click(object sender, RoutedEventArgs e)
        {
            AudioClear();
        }
        /// <summary>
        /// Audio Clear - Method
        /// </summary>
        public void AudioClear()
        {
            // -------------------------
            // List View
            // -------------------------
            // Clear List View
            if (VM.AudioView.Audio_ListView_Items != null &&
                VM.AudioView.Audio_ListView_Items.Count > 0)
            {
                VM.AudioView.Audio_ListView_Items.Clear();
            }

            // Clear Paths List
            if (Generate.Audio.Audio.audioFilePathsList != null &&
                Generate.Audio.Audio.audioFilePathsList.Count > 0)
            {
                Generate.Audio.Audio.audioFilePathsList.Clear();
                Generate.Audio.Audio.audioFilePathsList.TrimExcess();
            }

            // Clear Names List
            if (Generate.Audio.Audio.audioFileNamesList != null &&
                Generate.Audio.Audio.audioFileNamesList.Count > 0)
            {
                Generate.Audio.Audio.audioFileNamesList.Clear();
                Generate.Audio.Audio.audioFileNamesList.TrimExcess();
            }

            // -------------------------
            // Clear Metadata
            // -------------------------
            // Title
            if (Generate.Audio.Metadata.titleList != null &&
                Generate.Audio.Metadata.titleList.Count > 0)
            {
                Generate.Audio.Metadata.titleList.Clear();
                Generate.Audio.Metadata.titleList.TrimExcess();
            }

            // Language
            if (Generate.Audio.Metadata.languageList != null &&
                Generate.Audio.Metadata.languageList.Count > 0)
            {
                Generate.Audio.Metadata.languageList.Clear();
                Generate.Audio.Metadata.languageList.TrimExcess();
            }

            // Delay
            if (Generate.Audio.Metadata.delayList != null &&
                Generate.Audio.Metadata.delayList.Count > 0)
            {
                Generate.Audio.Metadata.delayList.Clear();
                Generate.Audio.Metadata.delayList.TrimExcess();
            }
        }

        /// <summary>
        /// Title Metadata - TextBox
        /// </summary>
        private void tbxAudio_Metadata_Title_KeyUp(object sender, KeyEventArgs e)
        {
            SaveMetadata_Audio_Title();
        }
        private void tbxAudio_Metadata_Title_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveMetadata_Audio_Title();
        }
        public void SaveMetadata_Audio_Title()
        {
            // -------------------------
            // Halts
            // -------------------------
            if (VM.AudioView.Audio_Stream_SelectedItem != "mux")
            {
                return;
            }

            // -------------------------
            // Title
            // -------------------------
            if (Generate.Audio.Metadata.titleList != null &&
                Generate.Audio.Metadata.titleList.Count > 0)
            {
                // Set selected index
                int selectedIndex = VM.AudioView.Audio_ListView_SelectedIndex;

                // Remove previous from the list at selected track index
                if (Generate.Audio.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
                {
                    try
                    {
                        Generate.Audio.Metadata.titleList.RemoveAt(selectedIndex);
                    }
                    catch
                    {

                    }
                }

                // Add to list
                //MessageBox.Show(string.Join("\r\n", Generate.Audio.Metadata.titleList));
                try
                {
                    Generate.Audio.Metadata.titleList.Insert(selectedIndex, tbxAudio_Metadata_Title.Text);
                }
                catch
                {

                }

                //Generate.Audio.Metadata.titleList.Add(tbxAudio_Metadata_Title.Text/*VM.AudioView.Audio_Metadata_Title_Text*/);
                //MessageBox.Show(tbxAudio_Metadata_Title.Text); // binding not working
                //MessageBox.Show(string.Join("\r\n", Generate.Audio.Metadata.titleList));
                //MessageBox.Show(VM.AudioView.Audio_ListView_SelectedIndex.ToString());
            }
        }

        /// <summary>
        /// Audio Language Metadata - ComboBox
        /// </summary>
        private void cboAudio_Metadata_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Halts
            // -------------------------
            if (VM.AudioView.Audio_Stream_SelectedItem != "mux")
            {
                return;
            }

            // -------------------------
            // Language
            // -------------------------
            if (Generate.Audio.Metadata.languageList != null &&
                Generate.Audio.Metadata.languageList.Count > 0)
            {
                // Set selected index
                int selectedIndex = VM.AudioView.Audio_ListView_SelectedIndex;

                // Remove previous from the list at selected track index
                if (Generate.Audio.Metadata.languageList.ElementAtOrDefault(selectedIndex) != null)
                {
                    try
                    {
                        Generate.Audio.Metadata.languageList.RemoveAt(selectedIndex);
                    }
                    catch
                    {

                    }
                }

                // Add to list
                try
                {
                    Generate.Audio.Metadata.languageList.Insert(selectedIndex, VM.AudioView.Audio_Metadata_Language_SelectedItem);
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// Audio Delay - TextBox
        /// </summary>
        private void tbxAudio_Delay_KeyUp(object sender, KeyEventArgs e)
        {
            SaveMetadata_Audio_Delay();
        }
        private void tbxAudio_Delay_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveMetadata_Audio_Delay();
        }
        public void SaveMetadata_Audio_Delay()
        {
            // -------------------------
            // Halts
            // -------------------------
            if (VM.AudioView.Audio_Stream_SelectedItem != "mux")
            {
                return;
            }

            // -------------------------
            // Delay
            // -------------------------
            if (Generate.Audio.Metadata.delayList != null &&
                Generate.Audio.Metadata.titleList.Count > 0)
            {
                // Set selected index
                int selectedIndex = VM.AudioView.Audio_ListView_SelectedIndex;

                // Remove previous from the list at selected track index
                if (Generate.Audio.Metadata.delayList.ElementAtOrDefault(selectedIndex) != null)
                {
                    try
                    {
                        Generate.Audio.Metadata.delayList.RemoveAt(selectedIndex);
                    }
                    catch
                    {

                    }
                }

                // Add to list
                //MessageBox.Show(string.Join("\r\n", Generate.Audio.Metadata.titleList));
                try
                {
                    Generate.Audio.Metadata.delayList.Insert(selectedIndex, VM.AudioView.Audio_Delay_Text);
                }
                catch
                {

                }
            }
        }

    }
}
