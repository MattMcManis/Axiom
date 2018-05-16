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
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class AudioControls
    {
        // --------------------------------------------------------------------------------------------------------
        // ComboBoxes Item Sources
        // --------------------------------------------------------------------------------------------------------
        private static string previousItem; // Previous ComboBox Item

        // -------------------------
        // Audio
        // -------------------------
        public static List<string> AudioCodecItemSource;
        public static List<string> AudioItemSource;
        public static List<string> ChannelItemSource;
        public static List<string> SampleRateItemSource;
        public static List<string> BitDepthItemSource;


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Control Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Audio Codec Controls (Method)
        /// 
        /// Changes Other ComboBox Items and Selections based on Audio Codec
        /// </summary>
        public static void AudioCodecControls(MainWindow mainwindow)
        {
            // Audio Codec Rules
            //
            // MKV Special Inustrctions - If Audio Codec = Copy, select Audio Dropdown to Auto
            if ((string)mainwindow.cboFormat.SelectedItem == "mkv" && (string)mainwindow.cboAudioCodec.SelectedItem == "Copy")
            {
                mainwindow.cboAudio.SelectedItem = "Auto";
            }

            // --------------------------------------------------
            // OPUS
            // --------------------------------------------------
            if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboAudio.SelectedItem;

                // Change ItemSource
                // Add 510k to Audio Quality ComboBox
                AudioItemSource = new List<string>() { "Auto", "510", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudio.ItemsSource = AudioItemSource;
                //OnPropertyChanged("AudioItemSource");

                // Select Item
                if (AudioItemSource.Contains(previousItem))
                {
                    mainwindow.cboAudio.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboAudio.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboAudio.IsEnabled = true;


                // -------------------------
                // Channel
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboChannel.SelectedItem;

                // Change ItemSource
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                // Populate ComboBox from ItemSource
                mainwindow.cboChannel.ItemsSource = ChannelItemSource;

                // Select Item
                if (ChannelItemSource.Contains(previousItem))
                {
                    mainwindow.cboChannel.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboChannel.SelectedIndex = 0; // Source
                }


                // -------------------------
                // Sample Rate
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboSamplerate.SelectedItem;

                // Change ItemSource
                SampleRateItemSource = new List<string>() { "auto", "8k", "12k", "16k", "24k", "48k" };

                // Populate ComboBox from ItemSource
                mainwindow.cboSamplerate.ItemsSource = SampleRateItemSource;

                // Select Item
                if (SampleRateItemSource.Contains(previousItem))
                {
                    mainwindow.cboSamplerate.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboSamplerate.SelectedIndex = 0;
                }

                // Enable Control
                mainwindow.cboSamplerate.IsEnabled = true;


                // -------------------------
                // Bit Depth
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboBitDepth.SelectedItem;

                // Change ItemSource
                BitDepthItemSource = new List<string>() { "auto", "8", "16", "24", "32", "64" };

                // Populate ComboBox from ItemSource
                mainwindow.cboBitDepth.ItemsSource = BitDepthItemSource;

                // Select Item
                if (BitDepthItemSource.Contains(previousItem))
                {
                    mainwindow.cboBitDepth.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboBitDepth.SelectedIndex = 0; // auto
                }

                // Disable Control
                mainwindow.cboBitDepth.IsEnabled = false;


                // -------------------------
                // VBR Button
                // -------------------------
                // Enable Control
                mainwindow.tglVBR.IsEnabled = true;
                mainwindow.tglVBR.IsChecked = false;
            }


            // --------------------------------------------------
            // VORBIS
            // --------------------------------------------------
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboAudio.SelectedItem;

                // Change ItemSource
                // Add 500k to Audio Quality Combobox
                AudioItemSource = new List<string>() { "Auto", "500", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudio.ItemsSource = AudioItemSource;

                // Select Item
                if (AudioItemSource.Contains(previousItem))
                {
                    mainwindow.cboAudio.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboAudio.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboAudio.IsEnabled = true;


                // -------------------------
                // Channel
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboChannel.SelectedItem;

                // Change ItemSource
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                // Populate ComboBox from ItemSource
                mainwindow.cboChannel.ItemsSource = ChannelItemSource;

                // Select Item
                if (ChannelItemSource.Contains(previousItem))
                {
                    mainwindow.cboChannel.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboChannel.SelectedIndex = 0; // Source
                }


                // -------------------------
                // Sample Rate
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboSamplerate.SelectedItem;

                // Change ItemSource
                SampleRateItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k" };

                // Populate ComboBox from ItemSource
                mainwindow.cboSamplerate.ItemsSource = SampleRateItemSource;

                // Select Item
                if (SampleRateItemSource.Contains(previousItem))
                {
                    mainwindow.cboSamplerate.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboSamplerate.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboSamplerate.IsEnabled = true;


                // -------------------------
                // Bit Depth
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboBitDepth.SelectedItem;

                // Change ItemSource
                BitDepthItemSource = new List<string>() { "auto", "8", "16", "24", "32", "64" };

                // Populate ComboBox from ItemSource
                mainwindow.cboBitDepth.ItemsSource = BitDepthItemSource;

                // Select Item
                if (BitDepthItemSource.Contains(previousItem))
                {
                    mainwindow.cboBitDepth.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboBitDepth.SelectedIndex = 0; // auto
                }

                // Disable Control
                mainwindow.cboBitDepth.IsEnabled = false;


                // -------------------------
                // VBR Button
                // -------------------------
                mainwindow.tglVBR.IsEnabled = true;
                mainwindow.tglVBR.IsChecked = true;

            }


            // --------------------------------------------------
            // AAC
            // --------------------------------------------------
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboAudio.SelectedItem;

                // Change ItemSource
                AudioItemSource = new List<string>() { "Auto", "400", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudio.ItemsSource = AudioItemSource;

                // Select Item
                if (AudioItemSource.Contains(previousItem))
                {
                    mainwindow.cboAudio.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboAudio.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboAudio.IsEnabled = true;


                // -------------------------
                // Channel
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboChannel.SelectedItem;

                // Change ItemSource
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                // Populate ComboBox from ItemSource
                mainwindow.cboChannel.ItemsSource = ChannelItemSource;

                // Select Item
                if (ChannelItemSource.Contains(previousItem))
                {
                    mainwindow.cboChannel.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboChannel.SelectedIndex = 0; // Source
                }


                // -------------------------
                // Sample Rate
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboSamplerate.SelectedItem;

                // Change ItemSource
                SampleRateItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k", "64k", "88.2k", "96k" };

                // Populate ComboBox from ItemSource
                mainwindow.cboSamplerate.ItemsSource = SampleRateItemSource;

                // Select Item
                if (SampleRateItemSource.Contains(previousItem))
                {
                    mainwindow.cboSamplerate.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboSamplerate.SelectedIndex = 0;
                }

                //Enable Control
                mainwindow.cboSamplerate.IsEnabled = true;


                // -------------------------
                // Bit Depth
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboBitDepth.SelectedItem;

                // Change ItemSource
                BitDepthItemSource = new List<string>() { "auto", "8", "16", "24", "32", "64" };

                // Populate ComboBox from ItemSource
                mainwindow.cboBitDepth.ItemsSource = BitDepthItemSource;

                // Select Item
                if (BitDepthItemSource.Contains(previousItem))
                {
                    mainwindow.cboBitDepth.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboBitDepth.SelectedIndex = 0; // auto
                }

                // Disable Control
                mainwindow.cboBitDepth.IsEnabled = false;


                // -------------------------
                // VBR Button
                // -------------------------
                mainwindow.tglVBR.IsEnabled = true;
                mainwindow.tglVBR.IsChecked = false;

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
                // Get Previous Item
                previousItem = (string)mainwindow.cboAudio.SelectedItem;

                // Change ItemSource
                AudioItemSource = new List<string>() { "Auto", "Lossless", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudio.ItemsSource = AudioItemSource;

                // Select Item
                mainwindow.cboAudio.SelectedItem = "Lossless";

                // Enable Control
                mainwindow.cboAudio.IsEnabled = false;


                // -------------------------
                // Channel
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboChannel.SelectedItem;

                // Change ItemSource
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                // Populate ComboBox from ItemSource
                mainwindow.cboChannel.ItemsSource = ChannelItemSource;

                // Select Item
                if (ChannelItemSource.Contains(previousItem))
                {
                    mainwindow.cboChannel.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboChannel.SelectedIndex = 0; // Source
                }


                // -------------------------
                // Sample Rate
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboSamplerate.SelectedItem;

                // Change ItemSource
                SampleRateItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k", "64k", "88.2k", "96k" };

                // Populate ComboBox from ItemSource
                mainwindow.cboSamplerate.ItemsSource = SampleRateItemSource;

                // Select Item
                if (SampleRateItemSource.Contains(previousItem))
                {
                    mainwindow.cboSamplerate.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboSamplerate.SelectedIndex = 0; // auto
                }

                //Enable Control
                mainwindow.cboSamplerate.IsEnabled = true;


                // -------------------------
                // Bit Depth
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboBitDepth.SelectedItem;

                // Change ItemSource
                BitDepthItemSource = new List<string>() { "auto", "16", "32" };

                // Populate ComboBox from ItemSource
                mainwindow.cboBitDepth.ItemsSource = BitDepthItemSource;

                // Select Item
                if (BitDepthItemSource.Contains(previousItem))
                {
                    mainwindow.cboBitDepth.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboBitDepth.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboBitDepth.IsEnabled = true;


                // -------------------------
                // VBR Button
                // -------------------------
                mainwindow.tglVBR.IsEnabled = false;
                mainwindow.tglVBR.IsChecked = false;

            }


            // --------------------------------------------------
            // AC3
            // --------------------------------------------------
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboAudio.SelectedItem;

                // Change ItemSource
                // Add 640k & 448k to Audio Quality ComboBox
                AudioItemSource = new List<string>() { "Auto", "640", "448", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudio.ItemsSource = AudioItemSource;

                // Select Item
                if (AudioItemSource.Contains(previousItem))
                {
                    mainwindow.cboAudio.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboAudio.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboAudio.IsEnabled = true;


                // -------------------------
                // Channel
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboChannel.SelectedItem;

                // Change ItemSource
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                // Populate ComboBox from ItemSource
                mainwindow.cboChannel.ItemsSource = ChannelItemSource;

                // Select Item
                if (ChannelItemSource.Contains(previousItem))
                {
                    mainwindow.cboChannel.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboChannel.SelectedIndex = 0; // Source
                }


                // -------------------------
                // Sample Rate
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboSamplerate.SelectedItem;

                // Change ItemSource
                SampleRateItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k", "64k", "88.2k", "96k" };

                // Populate ComboBox from ItemSource
                mainwindow.cboSamplerate.ItemsSource = SampleRateItemSource;

                // Select Item
                if (SampleRateItemSource.Contains(previousItem))
                {
                    mainwindow.cboSamplerate.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboSamplerate.SelectedIndex = 0; // auto
                }

                //Enable Control
                mainwindow.cboSamplerate.IsEnabled = true;


                // -------------------------
                // Bit Depth
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboBitDepth.SelectedItem;

                // Change ItemSource
                BitDepthItemSource = new List<string>() { "auto", "8", "16", "24", "32", "64" };

                // Populate ComboBox from ItemSource
                mainwindow.cboBitDepth.ItemsSource = BitDepthItemSource;

                // Select Item
                if (BitDepthItemSource.Contains(previousItem))
                {
                    mainwindow.cboBitDepth.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboBitDepth.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboBitDepth.IsEnabled = false;


                // -------------------------
                // VBR Button
                // -------------------------
                mainwindow.tglVBR.IsEnabled = false;
                mainwindow.tglVBR.IsChecked = false;

            }


            // --------------------------------------------------
            // MP3 LAME
            // --------------------------------------------------  
            // Grey out Audio Codec if MP3/LAME
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboAudio.SelectedItem;

                // Change ItemSource
                AudioItemSource = new List<string>() { "Auto", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudio.ItemsSource = AudioItemSource;

                // Select Item
                if (AudioItemSource.Contains(previousItem))
                {
                    mainwindow.cboAudio.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboAudio.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboAudio.IsEnabled = true;


                // -------------------------
                // Channel
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboChannel.SelectedItem;

                // Change ItemSource
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Joint Stereo", "Mono", "5.1" };

                // Populate ComboBox from ItemSource
                mainwindow.cboChannel.ItemsSource = ChannelItemSource;

                // Select Item
                mainwindow.cboChannel.SelectedItem = "Joint Stereo";


                // -------------------------
                // Sample Rate
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboSamplerate.SelectedItem;

                // Change ItemSource
                SampleRateItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k" };

                // Populate ComboBox from ItemSource
                mainwindow.cboSamplerate.ItemsSource = SampleRateItemSource;

                // Select Item
                if (SampleRateItemSource.Contains(previousItem))
                {
                    mainwindow.cboSamplerate.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboSamplerate.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboSamplerate.IsEnabled = true;


                // -------------------------
                // Bit Depth
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboBitDepth.SelectedItem;

                // Change ItemSource
                BitDepthItemSource = new List<string>() { "auto", "8", "16", "24", "32", "64" };

                // Populate ComboBox from ItemSource
                mainwindow.cboBitDepth.ItemsSource = BitDepthItemSource;

                // Select Item
                if (BitDepthItemSource.Contains(previousItem))
                {
                    mainwindow.cboBitDepth.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboBitDepth.SelectedIndex = 0; // auto
                }

                // Disable Control
                mainwindow.cboBitDepth.IsEnabled = false;


                // -------------------------
                // VBR Button
                // -------------------------
                mainwindow.tglVBR.IsEnabled = true;
                mainwindow.tglVBR.IsChecked = false;
            }

            // --------------------------------------------------
            // FLAC
            // --------------------------------------------------
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboAudio.SelectedItem;

                // Change ItemSource
                AudioItemSource = new List<string>() { "Auto", "Lossless", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudio.ItemsSource = AudioItemSource;

                // Select Item
                mainwindow.cboAudio.SelectedItem = "Lossless";

                // Enable Control
                mainwindow.cboAudio.IsEnabled = false;


                // -------------------------
                // Channel
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboChannel.SelectedItem;

                // Change ItemSource
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                // Populate ComboBox from ItemSource
                mainwindow.cboChannel.ItemsSource = ChannelItemSource;

                // Select Item
                if (ChannelItemSource.Contains(previousItem))
                {
                    mainwindow.cboChannel.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboChannel.SelectedIndex = 0; // Source
                }

                // -------------------------
                // Sample Rate
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboSamplerate.SelectedItem;

                // Change ItemSource
                SampleRateItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k" };

                // Populate ComboBox from ItemSource
                mainwindow.cboSamplerate.ItemsSource = SampleRateItemSource;

                // Select Item
                if (SampleRateItemSource.Contains(previousItem))
                {
                    mainwindow.cboSamplerate.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboSamplerate.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboSamplerate.IsEnabled = true;


                // -------------------------
                // Bit Depth
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboBitDepth.SelectedItem;

                // Change ItemSource
                BitDepthItemSource = new List<string>() { "auto", "16", "32" };

                // Populate ComboBox from ItemSource
                mainwindow.cboBitDepth.ItemsSource = BitDepthItemSource;

                // Select Item
                if (BitDepthItemSource.Contains(previousItem))
                {
                    mainwindow.cboBitDepth.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboBitDepth.SelectedIndex = 0; // auto
                }

                // Disable Control
                mainwindow.cboBitDepth.IsEnabled = true;


                // -------------------------
                // VBR Button
                // -------------------------
                mainwindow.tglVBR.IsEnabled = false;
                mainwindow.tglVBR.IsChecked = false;

            }

            // --------------------------------------------------
            // PCM
            // --------------------------------------------------
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboAudio.SelectedItem;

                // Change ItemSource
                AudioItemSource = new List<string>() { "Auto", "Lossless", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudio.ItemsSource = AudioItemSource;

                // Select Item
                mainwindow.cboAudio.SelectedItem = "Lossless";

                // Enable Control
                mainwindow.cboAudio.IsEnabled = false;

                // -------------------------
                // Channel
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboChannel.SelectedItem;

                // Change ItemSource
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                // Populate ComboBox from ItemSource
                mainwindow.cboChannel.ItemsSource = ChannelItemSource;

                // Select Item
                if (ChannelItemSource.Contains(previousItem))
                {
                    mainwindow.cboChannel.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboChannel.SelectedIndex = 0; // Source
                }


                // -------------------------
                // Sample Rate
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboSamplerate.SelectedItem;

                // Change ItemSource
                SampleRateItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k" };

                // Populate ComboBox from ItemSource
                mainwindow.cboSamplerate.ItemsSource = SampleRateItemSource;

                // Select Item
                if (SampleRateItemSource.Contains(previousItem))
                {
                    mainwindow.cboSamplerate.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboSamplerate.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboSamplerate.IsEnabled = true;


                // -------------------------
                // Bit Depth
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboBitDepth.SelectedItem;

                // Change ItemSource
                BitDepthItemSource = new List<string>() { "auto", "8", "16", "24", "32", "64" };

                // Populate ComboBox from ItemSource
                mainwindow.cboBitDepth.ItemsSource = BitDepthItemSource;

                // Select Item
                if (BitDepthItemSource.Contains(previousItem))
                {
                    mainwindow.cboBitDepth.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboBitDepth.SelectedIndex = 0; // auto
                }

                // Disable Control
                mainwindow.cboBitDepth.IsEnabled = true;


                // -------------------------
                // VBR Button
                // -------------------------
                mainwindow.tglVBR.IsEnabled = false;
                mainwindow.tglVBR.IsChecked = false;
            }

            // --------------------------------------------------
            // Copy
            // --------------------------------------------------
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Copy")
            {
                // -------------------------
                // Audio
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboAudio.SelectedItem;

                // Change ItemSource
                AudioItemSource = new List<string>() { "Auto", "Lossless", "320", "256", "224", "192", "160", "128", "96", "Custom", "Mute" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudio.ItemsSource = AudioItemSource;

                // Select Item
                mainwindow.cboAudio.SelectedItem = "Auto";

                //if (Audio.Contains(previousItem))
                //{
                //    audio.SelectedItem = previousItem;
                //}
                //else
                //{
                //    audio.SelectedIndex = 0; // auto
                //}

                // Enable Control
                mainwindow.cboAudio.IsEnabled = true;

                // -------------------------
                // Channel
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboChannel.SelectedItem;

                // Change ItemSource
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                // Populate ComboBox from ItemSource
                mainwindow.cboChannel.ItemsSource = ChannelItemSource;

                // Select Item
                mainwindow.cboChannel.SelectedItem = "Source";


                // -------------------------
                // Sample Rate
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboSamplerate.SelectedItem;

                // Change ItemSource
                SampleRateItemSource = new List<string>() { "auto", "8k", "11.025k", "12k", "16k", "22.05k", "24k", "32k", "44.1k", "48k" };

                // Populate ComboBox from ItemSource
                mainwindow.cboSamplerate.ItemsSource = SampleRateItemSource;

                // Select Item
                mainwindow.cboSamplerate.SelectedItem = "auto";

                // Enable Control
                mainwindow.cboSamplerate.IsEnabled = true;


                // -------------------------
                // Bit Depth
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboBitDepth.SelectedItem;

                // Change ItemSource
                BitDepthItemSource = new List<string>() { "auto", "8", "16", "24", "32", "64" };

                // Populate ComboBox from ItemSource
                mainwindow.cboBitDepth.ItemsSource = BitDepthItemSource;

                // Select Item
                mainwindow.cboBitDepth.SelectedItem = "auto";

                // Disable Control
                mainwindow.cboBitDepth.IsEnabled = false;


                // -------------------------
                // VBR Button
                // -------------------------
                mainwindow.tglVBR.IsEnabled = true;
                mainwindow.tglVBR.IsChecked = false;
            }


            // --------------------------------------------------
            // None
            // -------------------------------------------------- 
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "None")
            {
                //debug show list
                //Loop Error
                //var message = string.Join(Environment.NewLine, VideoCodec);
                //System.Windows.MessageBox.Show(message);

                // -------------------------
                // Audio
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboAudio.SelectedItem;

                // Change ItemSource
                AudioItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudio.ItemsSource = AudioItemSource;

                // Select Item
                mainwindow.cboAudio.SelectedItem = "None";

                // Enable Control
                mainwindow.cboAudio.IsEnabled = false;


                // -------------------------
                // Channel
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboChannel.SelectedItem;

                // Change ItemSource
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono", "5.1" };

                // Populate ComboBox from ItemSource
                mainwindow.cboChannel.ItemsSource = ChannelItemSource;

                // Select Item
                if (ChannelItemSource.Contains(previousItem))
                {
                    mainwindow.cboChannel.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboChannel.SelectedIndex = 0; // Source
                }


                // -------------------------
                // Sample Rate
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboSamplerate.SelectedItem;

                // Change ItemSource
                SampleRateItemSource = new List<string>() { "auto" };

                // Populate ComboBox from ItemSource
                mainwindow.cboSamplerate.ItemsSource = SampleRateItemSource;

                // Select Item
                mainwindow.cboSamplerate.SelectedItem = "auto";

                // Enable Control
                mainwindow.cboSamplerate.IsEnabled = false;


                // -------------------------
                // Bit Depth
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboBitDepth.SelectedItem;

                // Change ItemSource

                BitDepthItemSource = new List<string>() { "auto" };

                // Populate ComboBox from ItemSource
                mainwindow.cboBitDepth.ItemsSource = BitDepthItemSource;

                // Select Item
                mainwindow.cboBitDepth.SelectedItem = "auto";

                // Disable Control
                mainwindow.cboBitDepth.IsEnabled = false;


                // -------------------------
                // VBR Button
                // -------------------------
                mainwindow.tglVBR.IsEnabled = false;
                mainwindow.tglVBR.IsChecked = false;

            }


            // --------------------------------------------------
            // Not Auto
            // --------------------------------------------------
            // Default to the Highest Value Available when switching codecs
            // Only if Audio is Not Auto, None, Custom, Mute
            if ((string)mainwindow.cboAudio.SelectedItem != "Auto"
                && (string)mainwindow.cboAudio.SelectedItem != "None"
                && (string)mainwindow.cboAudio.SelectedItem != "Custom"
                && (string)mainwindow.cboAudio.SelectedItem != "Mute"
                && (string)mainwindow.cboAudio.SelectedItem != "Lossless"
                || string.IsNullOrEmpty((string)mainwindow.cboAudio.SelectedItem)) // If on Auto, leave it while switching codecs
            {
                //System.Windows.MessageBox.Show((string)audio.SelectedValue); // debug

                // Only if Audio Codec is Not Empty
                if (!string.IsNullOrEmpty((string)mainwindow.cboAudioCodec.SelectedItem))
                {

                    if (AudioItemSource.Contains("640")
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudio.SelectedItem) >= 320
                            || string.IsNullOrEmpty((string)mainwindow.cboAudio.SelectedItem))
                        {
                            mainwindow.cboAudio.SelectedItem = "640";
                        }
                    }
                    else if (AudioItemSource.Contains("510")
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudio.SelectedItem) >= 320
                            || string.IsNullOrEmpty((string)mainwindow.cboAudio.SelectedItem))
                        {
                            mainwindow.cboAudio.SelectedItem = "510";
                        }
                    }
                    else if (AudioItemSource.Contains("500")
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudio.SelectedItem) >= 320
                            || string.IsNullOrEmpty((string)mainwindow.cboAudio.SelectedItem))
                        {
                            mainwindow.cboAudio.SelectedItem = "500";
                        }
                    }
                    else if (AudioItemSource.Contains("448")
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudio.SelectedItem) >= 320
                            || string.IsNullOrEmpty((string)mainwindow.cboAudio.SelectedItem))
                        {
                            mainwindow.cboAudio.SelectedItem = "448";
                        }
                    }
                    else if (AudioItemSource.Contains("400")
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudio.SelectedItem) >= 320
                            || string.IsNullOrEmpty((string)mainwindow.cboAudio.SelectedItem))
                        {
                            mainwindow.cboAudio.SelectedItem = "400";
                        }
                    }
                    else if (AudioItemSource.Contains("320")
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC"
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudio.SelectedItem) >= 320
                            || string.IsNullOrEmpty((string)mainwindow.cboAudio.SelectedItem))
                        {
                            mainwindow.cboAudio.SelectedItem = "320";
                        }
                    }

                }
                // Default to Lossless if ALAC or FLAC
                if (mainwindow.cboAudio.Items.Contains("Lossless")
                    && (string)mainwindow.cboAudioCodec.SelectedItem == "ALAC"
                    | (string)mainwindow.cboAudioCodec.SelectedItem == "FLAC"
                    | (string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
                {
                    mainwindow.cboAudio.SelectedItem = "Lossless";
                    mainwindow.cboAudio.IsEnabled = false;
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
                if ((string)mainwindow.cboAudio.SelectedItem == "Auto"
                    && (string)mainwindow.cboChannel.SelectedItem == "Source"
                    && (string)mainwindow.cboSamplerate.SelectedItem == "auto"
                    && mainwindow.tglAudioLimiter.IsChecked == false
                    && mainwindow.volumeUpDown.Text.ToString().Equals("100")

                    // Extension Match
                    && string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // -------------------------
                    // Insert Copy if Does Not Contain
                    // -------------------------
                    if (!AudioCodecItemSource.Contains("Copy"))
                    {
                        AudioCodecItemSource.Insert(0, "Copy");
                    }
                    // Populate ComboBox from ItemSource
                    mainwindow.cboAudioCodec.ItemsSource = AudioCodecItemSource;

                    // -------------------------
                    // Set Video Codec Combobox Selected Item to Copy
                    // -------------------------
                    if (AudioCodecItemSource.Count > 0)
                    {
                        if (AudioCodecItemSource.Contains("Copy"))
                        {
                            mainwindow.cboAudioCodec.SelectedItem = "Copy";

                            //return;
                        }
                    }
                }

                // -------------------------
                // Select Copy - Batch
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "Auto"
                    && (string)mainwindow.cboSamplerate.SelectedItem == "auto"
                    && mainwindow.tglAudioLimiter.IsChecked == false
                    && mainwindow.volumeUpDown.Text.ToString().Equals("100")

                    // Batch Extension Match
                    && mainwindow.tglBatch.IsChecked == true
                    && string.Equals(MainWindow.batchExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // -------------------------
                    // Insert Copy if Does Not Contain
                    // -------------------------
                    if (!AudioCodecItemSource.Contains("Copy"))
                    {
                        AudioCodecItemSource.Insert(0, "Copy");
                    }
                    // Populate ComboBox from ItemSource
                    mainwindow.cboAudioCodec.ItemsSource = AudioCodecItemSource;

                    // -------------------------
                    // Set Video Codec Combobox Selected Item to Copy
                    // -------------------------
                    if (AudioCodecItemSource.Count > 0)
                    {
                        if (AudioCodecItemSource.Contains("Copy"))
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
                    if (!string.IsNullOrEmpty((string)mainwindow.cboAudio.SelectedItem))
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
                            }
                        }
                    }
                }
            }
        } // End AutoCopyAudioCodec
    }
}
