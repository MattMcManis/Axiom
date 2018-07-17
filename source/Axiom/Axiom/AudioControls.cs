/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017, 2018 Matt McManis
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class AudioControls
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     ComboBoxes Item Sources
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // -------------------------
        // Audio
        // -------------------------
        public static List<string> AudioCodec_ItemSource;
        public static List<string> AudioQuality_ItemSource;
        public static List<string> Channel_ItemSource;
        public static List<string> SampleRate_ItemSource;
        public static List<string> BitDepth_ItemSource;


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Control Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        ///     Change Item Source (Method)
        /// </summary>
        private static void ChangeItemSource(
            MainWindow mainwindow,
            ComboBox cbo,           // ComboBox
            List<string> items,     // New Items List
            string selectedItem)    // Selected Item
        {
            // -------------------------
            // Change Item Source
            // -------------------------
            cbo.ItemsSource = items;

            // -------------------------
            // Select Item
            // -------------------------
            // Get Previous Item
            string previousItem = selectedItem;

            // Select
            if (!string.IsNullOrEmpty(previousItem))
            {
                if (items.Contains(previousItem))
                {
                    cbo.SelectedItem = previousItem;
                }
                else
                {
                    cbo.SelectedIndex = 0; // Auto
                }
            }

            return;
        }

        /// <summary>
        /// Audio Codec Controls (Method)
        /// 
        /// Changes Other ComboBox Items and Selections based on Audio Codec
        /// </summary>
        public static void AudioCodecControls(MainWindow mainwindow)
        {
            // --------------------------------------------------
            // Audio Codec Rules
            // --------------------------------------------------
            // MKV Special Inustrctions - If Audio Codec = Copy, select Audio Dropdown to Auto
            if ((string)mainwindow.cboFormat.SelectedItem == "mkv" 
                && (string)mainwindow.cboAudioCodec.SelectedItem == "Copy")
            {
                mainwindow.cboAudioQuality.SelectedItem = "Auto";
            }

            // --------------------------------------------------
            // Opus
            // --------------------------------------------------
            if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
            {
                // --------------------------------------------------
                // Audio Quality
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                // Add 510k to Audio Quality ComboBox
                AudioQuality_ItemSource = new List<string>() { "Auto", "510", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboAudioQuality, // ComboBox
                    AudioQuality_ItemSource, // New Items List
                    (string)mainwindow.cboAudioQuality.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Channel
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Channel_ItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboChannel, // ComboBox
                    Channel_ItemSource, // New Items List
                    (string)mainwindow.cboChannel.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Sample Rate
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                SampleRate_ItemSource = new List<string>() { "auto", "8k", "12k", "16k", "24k", "48k" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboSamplerate, // ComboBox
                    SampleRate_ItemSource, // New Items List
                    (string)mainwindow.cboSamplerate.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Bit Depth
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                BitDepth_ItemSource = new List<string>() { "auto" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboBitDepth, // ComboBox
                    BitDepth_ItemSource, // New Items List
                    (string)mainwindow.cboBitDepth.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                // Audio Quality
                mainwindow.cboAudioQuality.IsEnabled = true;

                // Stream
                mainwindow.cboAudioStream.IsEnabled = true;

                // Channel
                mainwindow.cboChannel.IsEnabled = true;

                // SampleRate
                mainwindow.cboSamplerate.IsEnabled = true;

                // VBR Button
                mainwindow.tglAudioVBR.IsEnabled = true;
                mainwindow.tglAudioVBR.IsChecked = false;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = true;
                mainwindow.volumeUpButton.IsEnabled = true;
                mainwindow.volumeDownButton.IsEnabled = true;

                // -------------------------
                // Disabled
                // -------------------------
                mainwindow.cboBitDepth.IsEnabled = false;
            }


            // --------------------------------------------------
            // Vorbis
            // --------------------------------------------------
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Change ItemSource
                // Add 500k to Audio Quality Combobox
                AudioQuality_ItemSource = new List<string>() { "Auto", "500", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboAudioQuality, // ComboBox
                    AudioQuality_ItemSource, // New Items List
                    (string)mainwindow.cboAudioQuality.SelectedItem); // Selected Item


                // -------------------------
                // Channel
                // -------------------------
                // Change ItemSource
                Channel_ItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboChannel, // ComboBox
                    Channel_ItemSource, // New Items List
                    (string)mainwindow.cboChannel.SelectedItem); // Selected Item


                // -------------------------
                // Sample Rate
                // -------------------------
                // Change ItemSource
                SampleRate_ItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboSamplerate, // ComboBox
                    SampleRate_ItemSource, // New Items List
                    (string)mainwindow.cboSamplerate.SelectedItem); // Selected Item


                // -------------------------
                // Bit Depth
                // -------------------------
                // Change ItemSource
                BitDepth_ItemSource = new List<string>() { "auto" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboBitDepth, // ComboBox
                    BitDepth_ItemSource, // New Items List
                    (string)mainwindow.cboBitDepth.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                // Audio Quality
                mainwindow.cboAudioQuality.IsEnabled = true;

                // Stream
                mainwindow.cboAudioStream.IsEnabled = true;

                // Channel
                mainwindow.cboChannel.IsEnabled = true;

                // SampleRate
                mainwindow.cboSamplerate.IsEnabled = true;

                // VBR Button
                mainwindow.tglAudioVBR.IsEnabled = true;
                mainwindow.tglAudioVBR.IsChecked = true;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = true;
                mainwindow.volumeUpButton.IsEnabled = true;
                mainwindow.volumeDownButton.IsEnabled = true;

                // -------------------------
                // Disabled
                // -------------------------
                mainwindow.cboBitDepth.IsEnabled = false;
            }


            // --------------------------------------------------
            // AAC
            // --------------------------------------------------
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Change ItemSource
                AudioQuality_ItemSource = new List<string>() { "Auto", "400", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboAudioQuality, // ComboBox
                    AudioQuality_ItemSource, // New Items List
                    (string)mainwindow.cboAudioQuality.SelectedItem); // Selected Item


                // -------------------------
                // Channel
                // -------------------------
                // Change ItemSource
                Channel_ItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboChannel, // ComboBox
                    Channel_ItemSource, // New Items List
                    (string)mainwindow.cboChannel.SelectedItem); // Selected Item


                // -------------------------
                // Sample Rate
                // -------------------------
                // Change ItemSource
                SampleRate_ItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k", "64k", "88.2k", "96k" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboSamplerate, // ComboBox
                    SampleRate_ItemSource, // New Items List
                    (string)mainwindow.cboSamplerate.SelectedItem); // Selected Item


                // -------------------------
                // Bit Depth
                // -------------------------
                // Change ItemSource
                BitDepth_ItemSource = new List<string>() { "auto" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboBitDepth, // ComboBox
                    BitDepth_ItemSource, // New Items List
                    (string)mainwindow.cboBitDepth.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                // Audio Quality
                mainwindow.cboAudioQuality.IsEnabled = true;

                // Stream
                mainwindow.cboAudioStream.IsEnabled = true;

                // Channel
                mainwindow.cboChannel.IsEnabled = true;

                // SampleRate
                mainwindow.cboSamplerate.IsEnabled = true;

                // VBR Button
                mainwindow.tglAudioVBR.IsEnabled = true;
                mainwindow.tglAudioVBR.IsChecked = false;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = true;
                mainwindow.volumeUpButton.IsEnabled = true;
                mainwindow.volumeDownButton.IsEnabled = true;

                // -------------------------
                // Disabled
                // -------------------------
                mainwindow.cboBitDepth.IsEnabled = false;

            }


            // --------------------------------------------------
            // ALAC
            // --------------------------------------------------
            // Set Audio to Lossless if Codec is ALAC
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") // May cause LOOP ERROR
            {
                // -------------------------
                // Audio
                // -------------------------
                // Change ItemSource
                AudioQuality_ItemSource = new List<string>() { "Auto", "Lossless", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboAudioQuality, // ComboBox
                    AudioQuality_ItemSource, // New Items List
                    "Lossless"); // Selected Item


                // -------------------------
                // Channel
                // -------------------------
                // Change ItemSource
                Channel_ItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboChannel, // ComboBox
                    Channel_ItemSource, // New Items List
                    (string)mainwindow.cboChannel.SelectedItem); // Selected Item


                // -------------------------
                // Sample Rate
                // -------------------------
                // Change ItemSource
                SampleRate_ItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k", "64k", "88.2k", "96k" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboSamplerate, // ComboBox
                    SampleRate_ItemSource, // New Items List
                    (string)mainwindow.cboSamplerate.SelectedItem); // Selected Item


                // -------------------------
                // Bit Depth
                // -------------------------
                // Change ItemSource
                BitDepth_ItemSource = new List<string>() { "auto", "16", "32" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboBitDepth, // ComboBox
                    BitDepth_ItemSource, // New Items List
                    (string)mainwindow.cboBitDepth.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                // Stream
                mainwindow.cboAudioStream.IsEnabled = true;

                // Channel
                mainwindow.cboChannel.IsEnabled = true;

                // SampleRate
                mainwindow.cboSamplerate.IsEnabled = true;

                // BitDepth
                mainwindow.cboBitDepth.IsEnabled = true;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = true;
                mainwindow.volumeUpButton.IsEnabled = true;
                mainwindow.volumeDownButton.IsEnabled = true;

                // -------------------------
                // Disabled
                // -------------------------
                // Audio Quality
                mainwindow.cboAudioQuality.IsEnabled = true;

                // VBR Button
                mainwindow.tglAudioVBR.IsEnabled = false;
                mainwindow.tglAudioVBR.IsChecked = false;
            }


            // --------------------------------------------------
            // AC3
            // --------------------------------------------------
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Change ItemSource
                // Add 640k & 448k to Audio Quality ComboBox
                AudioQuality_ItemSource = new List<string>() { "Auto", "640", "448", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboAudioQuality, // ComboBox
                    AudioQuality_ItemSource, // New Items List
                    (string)mainwindow.cboAudioQuality.SelectedItem); // Selected Item


                // -------------------------
                // Channel
                // -------------------------
                // Change ItemSource
                Channel_ItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboChannel, // ComboBox
                    Channel_ItemSource, // New Items List
                    (string)mainwindow.cboChannel.SelectedItem); // Selected Item


                // -------------------------
                // Sample Rate
                // -------------------------
                // Change ItemSource
                SampleRate_ItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k", "64k", "88.2k", "96k" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboSamplerate, // ComboBox
                    SampleRate_ItemSource, // New Items List
                    (string)mainwindow.cboSamplerate.SelectedItem); // Selected Item


                // -------------------------
                // Bit Depth
                // -------------------------
                // Change ItemSource
                BitDepth_ItemSource = new List<string>() { "auto" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboBitDepth, // ComboBox
                    BitDepth_ItemSource, // New Items List
                    (string)mainwindow.cboBitDepth.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                // Audio Quality
                mainwindow.cboAudioQuality.IsEnabled = true;

                // Stream
                mainwindow.cboAudioStream.IsEnabled = true;

                // Channel
                mainwindow.cboChannel.IsEnabled = true;

                // SampleRate
                mainwindow.cboSamplerate.IsEnabled = true;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = true;
                mainwindow.volumeUpButton.IsEnabled = true;
                mainwindow.volumeDownButton.IsEnabled = true;

                // -------------------------
                // Disabled
                // -------------------------
                // VBR Button
                mainwindow.tglAudioVBR.IsEnabled = false;
                mainwindow.tglAudioVBR.IsChecked = false;

                // Bit Depth
                mainwindow.cboBitDepth.IsEnabled = false;
            }

            // --------------------------------------------------
            // MP2
            // --------------------------------------------------  
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "MP2")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Change ItemSource
                AudioQuality_ItemSource = new List<string>() { "Auto", "384", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboAudioQuality, // ComboBox
                    AudioQuality_ItemSource, // New Items List
                    (string)mainwindow.cboAudioQuality.SelectedItem); // Selected Item


                // -------------------------
                // Channel
                // -------------------------
                // Change ItemSource
                Channel_ItemSource = new List<string>() { "Source", "Stereo", "Joint Stereo", "Mono", "5.1" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboChannel, // ComboBox
                    Channel_ItemSource, // New Items List
                    "Joint Stereo"); // Selected Item


                // -------------------------
                // Sample Rate
                // -------------------------
                // Change ItemSource
                SampleRate_ItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboSamplerate, // ComboBox
                    SampleRate_ItemSource, // New Items List
                    (string)mainwindow.cboSamplerate.SelectedItem); // Selected Item


                // -------------------------
                // Bit Depth
                // -------------------------
                // Change ItemSource
                BitDepth_ItemSource = new List<string>() { "auto" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboBitDepth, // ComboBox
                    BitDepth_ItemSource, // New Items List
                    (string)mainwindow.cboBitDepth.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                // Audio Quality
                mainwindow.cboAudioQuality.IsEnabled = true;

                // Stream
                mainwindow.cboAudioStream.IsEnabled = true;

                // Channel
                mainwindow.cboChannel.IsEnabled = true;

                // VBR Button
                mainwindow.tglAudioVBR.IsEnabled = true;
                mainwindow.tglAudioVBR.IsChecked = false;

                // SampleRate
                mainwindow.cboSamplerate.IsEnabled = true;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = true;
                mainwindow.volumeUpButton.IsEnabled = true;
                mainwindow.volumeDownButton.IsEnabled = true;

                // -------------------------
                // Disabled
                // -------------------------
                // Bit Depth
                mainwindow.cboBitDepth.IsEnabled = false;
            }

            // --------------------------------------------------
            // LAME
            // --------------------------------------------------  
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Change ItemSource
                AudioQuality_ItemSource = new List<string>() { "Auto", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboAudioQuality, // ComboBox
                    AudioQuality_ItemSource, // New Items List
                    (string)mainwindow.cboAudioQuality.SelectedItem); // Selected Item


                // -------------------------
                // Channel
                // -------------------------
                // Change ItemSource
                Channel_ItemSource = new List<string>() { "Source", "Stereo", "Joint Stereo", "Mono", "5.1" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboChannel, // ComboBox
                    Channel_ItemSource, // New Items List
                    "Joint Stereo"); // Selected Item


                // -------------------------
                // Sample Rate
                // -------------------------
                // Change ItemSource
                SampleRate_ItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboSamplerate, // ComboBox
                    SampleRate_ItemSource, // New Items List
                    (string)mainwindow.cboSamplerate.SelectedItem); // Selected Item


                // -------------------------
                // Bit Depth
                // -------------------------
                // Change ItemSource
                BitDepth_ItemSource = new List<string>() { "auto" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboBitDepth, // ComboBox
                    BitDepth_ItemSource, // New Items List
                    (string)mainwindow.cboBitDepth.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                // Audio Quality
                mainwindow.cboAudioQuality.IsEnabled = true;

                // Stream
                mainwindow.cboAudioStream.IsEnabled = true;

                // Channel
                mainwindow.cboChannel.IsEnabled = true;

                // VBR Button
                mainwindow.tglAudioVBR.IsEnabled = true;
                mainwindow.tglAudioVBR.IsChecked = false;

                // SampleRate
                mainwindow.cboSamplerate.IsEnabled = true;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = true;
                mainwindow.volumeUpButton.IsEnabled = true;
                mainwindow.volumeDownButton.IsEnabled = true;

                // -------------------------
                // Disabled
                // -------------------------
                // Bit Depth
                mainwindow.cboBitDepth.IsEnabled = false;
            }

            // --------------------------------------------------
            // FLAC
            // --------------------------------------------------
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Change ItemSource
                AudioQuality_ItemSource = new List<string>() { "Auto", "Lossless", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboAudioQuality, // ComboBox
                    AudioQuality_ItemSource, // New Items List
                    "Lossless"); // Selected Item

                // Enable Control
                


                // -------------------------
                // Channel
                // -------------------------
                // Change ItemSource
                Channel_ItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboChannel, // ComboBox
                    Channel_ItemSource, // New Items List
                    (string)mainwindow.cboChannel.SelectedItem); // Selected Item

                // -------------------------
                // Sample Rate
                // -------------------------
                // Change ItemSource
                SampleRate_ItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboSamplerate, // ComboBox
                    SampleRate_ItemSource, // New Items List
                    (string)mainwindow.cboSamplerate.SelectedItem); // Selected Item


                // -------------------------
                // Bit Depth
                // -------------------------
                // Change ItemSource
                BitDepth_ItemSource = new List<string>() { "auto", "16", "32" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboBitDepth, // ComboBox
                    BitDepth_ItemSource, // New Items List
                    (string)mainwindow.cboBitDepth.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                // Stream
                mainwindow.cboAudioStream.IsEnabled = true;

                // Channel
                mainwindow.cboChannel.IsEnabled = true;

                // SampleRate
                mainwindow.cboSamplerate.IsEnabled = true;

                // BitDepth
                mainwindow.cboBitDepth.IsEnabled = true;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = true;
                mainwindow.volumeUpButton.IsEnabled = true;
                mainwindow.volumeDownButton.IsEnabled = true;

                // -------------------------
                // Disabled
                // -------------------------
                // Audio Quality
                mainwindow.cboAudioQuality.IsEnabled = true;

                // VBR Button
                mainwindow.tglAudioVBR.IsEnabled = false;
                mainwindow.tglAudioVBR.IsChecked = false;

            }

            // --------------------------------------------------
            // PCM
            // --------------------------------------------------
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Change ItemSource
                AudioQuality_ItemSource = new List<string>() { "Auto", "Lossless", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboAudioQuality, // ComboBox
                    AudioQuality_ItemSource, // New Items List
                    "Lossless"); // Selected Item


                // -------------------------
                // Channel
                // -------------------------
                // Change ItemSource
                Channel_ItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboChannel, // ComboBox
                    Channel_ItemSource, // New Items List
                    (string)mainwindow.cboChannel.SelectedItem); // Selected Item


                // -------------------------
                // Sample Rate
                // -------------------------
                // Change ItemSource
                SampleRate_ItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboSamplerate, // ComboBox
                    SampleRate_ItemSource, // New Items List
                    (string)mainwindow.cboSamplerate.SelectedItem); // Selected Item


                // -------------------------
                // Bit Depth
                // -------------------------
                // Change ItemSource
                BitDepth_ItemSource = new List<string>() { "auto", "8", "16", "24", "32", "64" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboBitDepth, // ComboBox
                    BitDepth_ItemSource, // New Items List
                    (string)mainwindow.cboBitDepth.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                // Stream
                mainwindow.cboAudioStream.IsEnabled = true;

                // Channel
                mainwindow.cboChannel.IsEnabled = true;

                // SampleRate
                mainwindow.cboSamplerate.IsEnabled = true;

                // BitDepth
                mainwindow.cboBitDepth.IsEnabled = true;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = true;
                mainwindow.volumeUpButton.IsEnabled = true;
                mainwindow.volumeDownButton.IsEnabled = true;

                // -------------------------
                // Disabled
                // -------------------------
                // Audio Quality
                mainwindow.cboAudioQuality.IsEnabled = true;

                // VBR Button
                mainwindow.tglAudioVBR.IsEnabled = false;
                mainwindow.tglAudioVBR.IsChecked = false;
            }


            // --------------------------------------------------
            // Copy
            // --------------------------------------------------
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Copy")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Change ItemSource
                AudioQuality_ItemSource = new List<string>() { "Auto", "Lossless", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboAudioQuality, // ComboBox
                    AudioQuality_ItemSource, // New Items List
                    "Auto"); // Selected Item


                // -------------------------
                // Channel
                // -------------------------
                // Change ItemSource
                Channel_ItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboChannel, // ComboBox
                    Channel_ItemSource, // New Items List
                    "Source"); // Selected Item


                // -------------------------
                // Sample Rate
                // -------------------------
                // Change ItemSource
                SampleRate_ItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboSamplerate, // ComboBox
                    SampleRate_ItemSource, // New Items List
                    "auto"); // Selected Item


                // -------------------------
                // Bit Depth
                // -------------------------
                // Change ItemSource
                BitDepth_ItemSource = new List<string>() { "auto", "8", "16", "24", "32", "64" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboBitDepth, // ComboBox
                    BitDepth_ItemSource, // New Items List
                    "auto"); // Selected Item



                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                // Audio Quality
                mainwindow.cboAudioQuality.IsEnabled = true;

                // Stream
                mainwindow.cboAudioStream.IsEnabled = true;

                // Channel
                mainwindow.cboChannel.IsEnabled = true;

                // VBR Button
                mainwindow.tglAudioVBR.IsEnabled = true;
                mainwindow.tglAudioVBR.IsChecked = false;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = true;
                mainwindow.volumeUpButton.IsEnabled = true;
                mainwindow.volumeDownButton.IsEnabled = true;


                // -------------------------
                // Disabled
                // -------------------------
                // SampleRate
                mainwindow.cboSamplerate.IsEnabled = true;

                // Bit Depth
                mainwindow.cboBitDepth.IsEnabled = false;
            }


            // --------------------------------------------------
            // None
            // -------------------------------------------------- 
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "None")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Change ItemSource
                AudioQuality_ItemSource = new List<string>() { "None" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboAudioQuality, // ComboBox
                    AudioQuality_ItemSource, // New Items List
                    "None"); // Selected Item


                // -------------------------
                // Channel
                // -------------------------
                // Change ItemSource
                Channel_ItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboChannel, // ComboBox
                    Channel_ItemSource, // New Items List
                    "Source"); // Selected Item


                // -------------------------
                // Sample Rate
                // -------------------------
                // Change ItemSource
                SampleRate_ItemSource = new List<string>() { "auto" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboSamplerate, // ComboBox
                    SampleRate_ItemSource, // New Items List
                    "auto"); // Selected Item


                // -------------------------
                // Bit Depth
                // -------------------------
                // Change ItemSource
                BitDepth_ItemSource = new List<string>() { "auto" };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboBitDepth, // ComboBox
                    BitDepth_ItemSource, // New Items List
                    "auto"); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                // None

                // -------------------------
                // Disabled
                // -------------------------
                // Audio Quality
                mainwindow.cboAudioQuality.IsEnabled = false;

                // Stream
                mainwindow.cboAudioStream.IsEnabled = false;

                // Channel
                mainwindow.cboChannel.IsEnabled = false;

                // VBR Button
                mainwindow.tglAudioVBR.IsEnabled = false;
                mainwindow.tglAudioVBR.IsChecked = false;

                // SampleRate
                mainwindow.cboSamplerate.IsEnabled = false;

                // Bit Depth
                mainwindow.cboBitDepth.IsEnabled = false;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = false;
                mainwindow.volumeUpButton.IsEnabled = false;
                mainwindow.volumeDownButton.IsEnabled = false;
            }


            // --------------------------------------------------
            // Not Auto
            // --------------------------------------------------
            // Default to the Highest Value Available when switching codecs
            // Only if Audio is Not Auto, None, Custom, Mute
            if ((string)mainwindow.cboAudioQuality.SelectedItem != "Auto"
                && (string)mainwindow.cboAudioQuality.SelectedItem != "None"
                && (string)mainwindow.cboAudioQuality.SelectedItem != "Custom"
                && (string)mainwindow.cboAudioQuality.SelectedItem != "Mute"
                && (string)mainwindow.cboAudioQuality.SelectedItem != "Lossless"
                || string.IsNullOrEmpty((string)mainwindow.cboAudioQuality.SelectedItem)) // If on Auto, leave it while switching codecs
            {
                //System.Windows.MessageBox.Show((string)audio.SelectedValue); // debug

                // Only if Audio Codec is Not Empty
                if (!string.IsNullOrEmpty((string)mainwindow.cboAudioCodec.SelectedItem))
                {
                    // -------------------------
                    // 640
                    // -------------------------
                    if (AudioQuality_ItemSource.Contains("640")
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudioQuality.SelectedItem) >= 320
                            || string.IsNullOrEmpty((string)mainwindow.cboAudioQuality.SelectedItem))
                        {
                            mainwindow.cboAudioQuality.SelectedItem = "640";
                        }
                    }
                    // -------------------------
                    // 510
                    // -------------------------
                    else if (AudioQuality_ItemSource.Contains("510")
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudioQuality.SelectedItem) >= 320
                            || string.IsNullOrEmpty((string)mainwindow.cboAudioQuality.SelectedItem))
                        {
                            mainwindow.cboAudioQuality.SelectedItem = "510";
                        }
                    }
                    // -------------------------
                    // 500
                    // -------------------------
                    else if (AudioQuality_ItemSource.Contains("500")
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudioQuality.SelectedItem) >= 320
                            || string.IsNullOrEmpty((string)mainwindow.cboAudioQuality.SelectedItem))
                        {
                            mainwindow.cboAudioQuality.SelectedItem = "500";
                        }
                    }
                    // -------------------------
                    // 448
                    // -------------------------
                    else if (AudioQuality_ItemSource.Contains("448")
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudioQuality.SelectedItem) >= 320
                            || string.IsNullOrEmpty((string)mainwindow.cboAudioQuality.SelectedItem))
                        {
                            mainwindow.cboAudioQuality.SelectedItem = "448";
                        }
                    }
                    // -------------------------
                    // 400
                    // -------------------------
                    else if (AudioQuality_ItemSource.Contains("400")
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudioQuality.SelectedItem) >= 320
                            || string.IsNullOrEmpty((string)mainwindow.cboAudioQuality.SelectedItem))
                        {
                            mainwindow.cboAudioQuality.SelectedItem = "400";
                        }
                    }
                    // -------------------------
                    // 320
                    // -------------------------
                    else if (AudioQuality_ItemSource.Contains("320")
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudioQuality.SelectedItem) >= 320
                            || string.IsNullOrEmpty((string)mainwindow.cboAudioQuality.SelectedItem))
                        {
                            mainwindow.cboAudioQuality.SelectedItem = "320";
                        }
                    }

                }

                // -------------------------
                // Default to Lossless if ALAC or FLAC
                // -------------------------
                if (mainwindow.cboAudioQuality.Items.Contains("Lossless")
                    && (string)mainwindow.cboAudioCodec.SelectedItem == "ALAC"
                    | (string)mainwindow.cboAudioCodec.SelectedItem == "FLAC"
                    | (string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
                {
                    mainwindow.cboAudioQuality.SelectedItem = "Lossless";
                    mainwindow.cboAudioQuality.IsEnabled = false;
                    mainwindow.cboBitDepth.IsEnabled = true;
                    if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { mainwindow.cboBitDepth.SelectedItem = "24"; } //special rules for PCM codec
                    else { mainwindow.cboBitDepth.SelectedItem = "auto"; }
                    mainwindow.cboSamplerate.SelectedItem = "auto";
                }
            }


            // Call Cut Controls Method from MainWindow
            FormatControls.CutControls(mainwindow); //method

        } // EndAudio Codec Controls


        /// <summary>
        ///     Audio - Auto Codec Copy (Method)
        /// <summary>
        public static void AutoCopyAudioCodec(MainWindow mainwindow) // Method
        {
            if (!string.IsNullOrEmpty(MainWindow.inputExt) || !string.IsNullOrEmpty(MainWindow.batchExt)) // Null Check
            {
                // -------------------------
                // Select Copy - Single
                // -------------------------
                // Input Extension is Same as Output Extension and Audio Quality is Auto
                if ((string)mainwindow.cboAudioQuality.SelectedItem == "Auto"
                    && (string)mainwindow.cboChannel.SelectedItem == "Source"
                    && (string)mainwindow.cboSamplerate.SelectedItem == "auto"
                    //&& mainwindow.tglAudioLimiter.IsChecked == false
                    && mainwindow.slAudioLimiter.Value == 1
                    && mainwindow.volumeUpDown.Text.ToString().Equals("100")

                    // Extension Match
                    && string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    //// -------------------------
                    //// Insert Copy if Does Not Contain
                    //// -------------------------
                    //if (!AudioCodecItemSource.Contains("Copy"))
                    //{
                    //    AudioCodecItemSource.Insert(0, "Copy");
                    //}
                    //// Populate ComboBox from ItemSource
                    //mainwindow.cboAudioCodec.ItemsSource = AudioCodecItemSource;

                    // -------------------------
                    // Set Video Codec Combobox Selected Item to Copy
                    // -------------------------
                    if (mainwindow.cboAudioCodec.Items.Count > 0)
                    {
                        if (mainwindow.cboAudioCodec.Items.Contains("Copy"))
                        {
                            mainwindow.cboAudioCodec.SelectedItem = "Copy";

                            //return;
                        }
                    }
                }

                // -------------------------
                // Select Copy - Batch
                // -------------------------
                else if ((string)mainwindow.cboAudioQuality.SelectedItem == "Auto"
                    && (string)mainwindow.cboSamplerate.SelectedItem == "auto"
                    //&& mainwindow.tglAudioLimiter.IsChecked == false
                    && mainwindow.slAudioLimiter.Value == 1
                    && mainwindow.volumeUpDown.Text == "100"

                    // Batch Extension Match
                    && mainwindow.tglBatch.IsChecked == true
                    && string.Equals(MainWindow.batchExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    //// -------------------------
                    //// Insert Copy if Does Not Contain
                    //// -------------------------
                    //if (!AudioCodecItemSource.Contains("Copy"))
                    //{
                    //    AudioCodecItemSource.Insert(0, "Copy");
                    //}
                    //// Populate ComboBox from ItemSource
                    //mainwindow.cboAudioCodec.ItemsSource = AudioCodecItemSource;

                    // -------------------------
                    // Set Video Codec Combobox Selected Item to Copy
                    // -------------------------
                    if (mainwindow.cboAudioCodec.Items.Count > 0)
                    {
                        if (mainwindow.cboAudioCodec.Items.Contains("Copy"))
                        {
                            mainwindow.cboAudioCodec.SelectedItem = "Copy";

                            //return;
                        }
                    }
                }

                // -------------------------
                // Reset to Default Codec
                // -------------------------
                else
                {
                    // -------------------------
                    // Disable Copy if:
                    // Input / Output Extensions don't match
                    // Audio is Not Auto 
                    // VBR is Checked
                    // Samplerate is Not auto
                    // BitDepth is Not auto
                    // Alimiter is Checked
                    // Volume is Not 100
                    // -------------------------
                    // -------------------------
                    // Null Check
                    // -------------------------
                    if (!string.IsNullOrEmpty((string)mainwindow.cboAudioQuality.SelectedItem))
                    {
                        // -------------------------
                        // Copy Selected
                        // -------------------------
                        if ((string)mainwindow.cboAudioCodec.SelectedItem == "Copy")
                        {
                            // -------------------------
                            // Switch back to format's default codec
                            // -------------------------
                            if (!string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                                || !string.Equals(MainWindow.batchExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                                )
                            {
                                // -------------------------
                                // Video Container
                                // -------------------------
                                if ((string)mainwindow.cboFormat.SelectedItem == "webm")
                                {
                                    mainwindow.cboAudioCodec.SelectedItem = "Vorbis";
                                }
                                else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                                {
                                    mainwindow.cboAudioCodec.SelectedItem = "AAC";
                                }
                                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                                {
                                    mainwindow.cboAudioCodec.SelectedItem = "AC3";
                                }
                                else if ((string)mainwindow.cboFormat.SelectedItem == "m2v")
                                {
                                    mainwindow.cboAudioCodec.SelectedItem = "None";
                                }
                                else if ((string)mainwindow.cboFormat.SelectedItem == "mpg")
                                {
                                    mainwindow.cboAudioCodec.SelectedItem = "AC3";
                                }
                                else if ((string)mainwindow.cboFormat.SelectedItem == "avi")
                                {
                                    mainwindow.cboAudioCodec.SelectedItem = "LAME";
                                }
                                else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                                {
                                    mainwindow.cboAudioCodec.SelectedItem = "Vorbis";
                                }

                                // -------------------------
                                // Audio Container
                                // -------------------------
                                if ((string)mainwindow.cboFormat.SelectedItem == "m4a")
                                {
                                    mainwindow.cboAudioCodec.SelectedItem = "AAC";
                                }
                                else if ((string)mainwindow.cboFormat.SelectedItem == "mp3")
                                {
                                    mainwindow.cboAudioCodec.SelectedItem = "LAME";
                                }
                                else if ((string)mainwindow.cboFormat.SelectedItem == "ogg")
                                {
                                    mainwindow.cboAudioCodec.SelectedItem = "Opus";
                                }
                                else if ((string)mainwindow.cboFormat.SelectedItem == "flac")
                                {
                                    mainwindow.cboAudioCodec.SelectedItem = "FLAC";
                                }
                                else if ((string)mainwindow.cboFormat.SelectedItem == "wav")
                                {
                                    mainwindow.cboAudioCodec.SelectedItem = "PCM";
                                }

                                // -------------------------
                                // Image Container
                                // -------------------------
                                //if ((string)mainwindow.cboFormat.SelectedItem == "jpg")
                                //{
                                //    mainwindow.cboAudioCodec.SelectedItem = "None";
                                //}
                                //else if ((string)mainwindow.cboFormat.SelectedItem == "png")
                                //{
                                //    mainwindow.cboAudioCodec.SelectedItem = "None";
                                //}
                                //else if ((string)mainwindow.cboFormat.SelectedItem == "webp")
                                //{
                                //    mainwindow.cboAudioCodec.SelectedItem = "None";
                                //}
                            }
                        }
                    }
                }
            }
        } // End AutoCopyAudioCodec
    }
}
