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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
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
            // Set Controls
            // -------------------------
            Controls.Audio.Controls.SetControls(audio_Codec_SelectedItem);

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
            Controls.Audio.Controls.SetControls(VM.AudioView.Audio_Codec_SelectedItem);

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
            if (VM.AudioView.Audio_Codec_SelectedItem == "PCM")
            {
                Controls.Audio.Codec.PCM.Codec_Set();
            }

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
    }
}
