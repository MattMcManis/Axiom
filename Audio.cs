using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

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
    public partial class Audio
    {
        // --------------------------------------------------------------------------------------------------------
        // ComboBoxes Item Sources
        // --------------------------------------------------------------------------------------------------------

        private static string previousItem; // Previous ComboBox Item

        // -------------------------
        // Audio
        // -------------------------
        public static List<string> AudioCodecItemSource = new List<string>();
        public static List<string> AudioItemSource = new List<string>();
        public static List<string> ChannelItemSource = new List<string>();
        public static List<string> SampleRateItemSource = new List<string>();
        public static List<string> BitDepthItemSource = new List<string>();


        // --------------------------------------------------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------------------------------------------------
        // Audio
        //public static int autoCopyAudioCodecSwitch = 0; // If 1, do not run AutoCopy Method again
        public static string aCodec;
        public static string aQuality;
        public static string aBitMode;
        public static string aBitrate;
        public static string aChannel;
        public static string aSamplerate;
        public static string aBitDepth;
        public static string volume;
        public static string aLimiter;

        // Filter Lists
        public static List<string> AudioFilters = new List<string>(); // Filters to String Join

        public static int? aFilterSwitch = 0;
        public static string aFilter;

        // Batch
        //public static string cmdBatch_aQuality; // cmd batch audio dynamic value
        public static string aBitrateLimiter; // limits the bitrate value of webm and ogg
        public static string batchAudioAuto;



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
                //string previousItem;

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
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono" };

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
                //string previousItem;

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
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono" };

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
                //string previousItem;

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
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono" };

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
                //string previousItem;

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
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono" };

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
                //string previousItem;

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
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono" };

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
                //string previousItem;

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
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Joint Stereo", "Mono" };

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
                //string previousItem;

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
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono" };

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
                //string previousItem;

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
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono" };

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
                //string previousItem;

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
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono" };

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
                //string previousItem;

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
                ChannelItemSource = new List<string>() { "Source", "Stereo", "Mono" };

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
                //var audioValue = audio.SelectedValue;
                //System.Windows.MessageBox.Show(Convert.ToString(audioValue));

                // Only if Audio Codec is Not Empty
                if (!string.IsNullOrEmpty((string)mainwindow.cboAudioCodec.SelectedItem))
                {

                    if (AudioItemSource.Contains("640") 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC" 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC" 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudio.SelectedItem) >= 320 || string.IsNullOrEmpty((string)mainwindow.cboAudio.SelectedItem))
                        {
                            mainwindow.cboAudio.SelectedItem = "640";
                        }
                    }
                    else if (AudioItemSource.Contains("510") 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC" 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC" 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudio.SelectedItem) >= 320 || string.IsNullOrEmpty((string)mainwindow.cboAudio.SelectedItem))
                        {
                            mainwindow.cboAudio.SelectedItem = "510";
                        }
                    }
                    else if (AudioItemSource.Contains("500") 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC" 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC" 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudio.SelectedItem) >= 320 || string.IsNullOrEmpty((string)mainwindow.cboAudio.SelectedItem))
                        {
                            mainwindow.cboAudio.SelectedItem = "500";
                        }
                    }
                    else if (AudioItemSource.Contains("448") 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC" 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC" 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudio.SelectedItem) >= 320 || string.IsNullOrEmpty((string)mainwindow.cboAudio.SelectedItem))
                        {
                            mainwindow.cboAudio.SelectedItem = "448";
                        }
                    }
                    else if (AudioItemSource.Contains("400") 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC" 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC" 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudio.SelectedItem) >= 320 || string.IsNullOrEmpty((string)mainwindow.cboAudio.SelectedItem))
                        {
                            mainwindow.cboAudio.SelectedItem = "400";
                        }
                    }
                    else if (AudioItemSource.Contains("320") 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "ALAC" 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "FLAC" 
                        && (string)mainwindow.cboAudioCodec.SelectedItem != "PCM")
                    {
                        if (Convert.ToInt32((string)mainwindow.cboAudio.SelectedItem) >= 320 || string.IsNullOrEmpty((string)mainwindow.cboAudio.SelectedItem))
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
            Format.CutControls(mainwindow); //method

        } // EndAudio Codec Controls





        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Process Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Audio Codecs (Method)
        /// <summary>
        public static String AudioCodec(MainWindow mainwindow)
        {
            // #################
            // Audio
            // #################
            if (string.IsNullOrEmpty((string)mainwindow.cboAudioCodec.SelectedItem))
            {
                aCodec = string.Empty;
            }
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "None")
            {
                aCodec = string.Empty;
                Streams.aMap = "-an";
            }
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
            {
                aCodec = "-acodec libvorbis";
            }
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
            {
                aCodec = "-acodec libopus";
            }
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
            {
                aCodec = "-acodec aac";
            }
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC")
            {
                aCodec = "-acodec alac";
            }
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3")
            {
                aCodec = "-acodec ac3";
            }
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
            {
                aCodec = "-acodec libmp3lame";
            }
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC")
            {
                aCodec = "-acodec flac";
            }
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
            {
                aCodec = string.Empty; // Codec not needed for PCM or Controlled by "PCM Match Bit Depth Audio" Section
            }


            // --------------------------------------------------
            // Category: Audio (Log)
            // --------------------------------------------------
            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Audio")) { Foreground = Log.ConsoleAction });
            };
            Log.LogActions.Add(Log.WriteAction);

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Codec: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(Convert.ToString(mainwindow.cboAudioCodec.SelectedItem)) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);


            // Return Value
            return aCodec;
        }


        /// <summary>
        /// Audio - Auto Codec Copy (Method)
        /// <summary>
        public static void AutoCopyAudioCodec(MainWindow mainwindow) // Method
        {
            //if (autoCopyAudioCodecSwitch == 0) // Switch Check
            //{
                if (!string.IsNullOrEmpty(MainWindow.inputExt)) // Null Check
                {
                    // Set Audio Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Audio Quality is Auto
                    if ((string)mainwindow.cboAudio.SelectedItem == "Auto"
                        && string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                        || mainwindow.tglBatch.IsChecked == true
                        && (string)mainwindow.cboAudio.SelectedItem == "Auto"
                        && string.Equals(MainWindow.batchExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase))
                    {

                        // Insert Copy if Does Not Contain
                        if (!AudioCodecItemSource.Contains("Copy"))
                        {
                            AudioCodecItemSource.Insert(0, "Copy");
                        }
                        // Populate ComboBox from ItemSource
                        mainwindow.cboAudioCodec.ItemsSource = AudioCodecItemSource;

                        mainwindow.cboAudioCodec.SelectedItem = "Copy";

                        // Turn on Switch
                        // Does not let AutoCopy Method run again
                        //autoCopyAudioCodecSwitch = 1;

                        // Debug
                        //System.Windows.MessageBox.Show("Input: " + MainWindow.inputExt);
                        //System.Windows.MessageBox.Show("Output: " + MainWindow.outputExt);
                    }

                    // Disable Copy if:
                    // Input / Output Extensions don't match
                    // Audio is Not Auto 
                    // VBR is Checked
                    // Samplerate is Not auto
                    // BitDepth is Not auto
                    // Alimiter is Checked
                    // Volume is Not 100
                    //
                    if (AudioCodecItemSource.Contains("Copy")
                        && !string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                        | !string.Equals(MainWindow.batchExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // Switch back to format's default codec
                        //
                        if ((string)mainwindow.cboAudio.SelectedItem != "Auto"
                            || mainwindow.tglVBR.IsChecked != false
                            || (string)mainwindow.cboSamplerate.SelectedItem != "auto"
                            || mainwindow.tglAudioLimiter.IsChecked != false
                            || !mainwindow.volumeUpDown.Text.ToString().Equals("100"))
                        {
                            // VIDEO
                            //
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
                                //cboAudioCodec.SelectedItem = "AC3"; //ignore mkv, special rules below ??
                            }
                            else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                            {
                                mainwindow.cboAudioCodec.SelectedItem = "Vorbis";
                            }
                            // AUDIO
                            //
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
                            // IMAGE
                            //
                            if ((string)mainwindow.cboFormat.SelectedItem == "jpg")
                            {
                                mainwindow.cboAudioCodec.SelectedItem = string.Empty;
                            }
                            else if ((string)mainwindow.cboFormat.SelectedItem == "png")
                            {
                                mainwindow.cboAudioCodec.SelectedItem = string.Empty;
                            }
                        }
                    }


                    // Special Rules for MKV
                    if ((string)mainwindow.cboFormat.SelectedItem == "mkv"
                        && (string)mainwindow.cboAudioCodec.SelectedItem == "Copy"
                        && (string)mainwindow.cboAudio.SelectedItem != "Auto")
                    {
                        if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                        {
                            mainwindow.cboAudioCodec.SelectedItem = "AC3";
                        }
                    }
                }
            //}

        } // End AutoCopyAudioCodec



        /// <summary>
        /// Audio Bitrate Mode (Method)
        /// <summary>
        public static void AudioBitrateMode(MainWindow mainwindow)
        {
            if (mainwindow.tglVBR.IsChecked == true)
            {
                // VBR
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis") { aBitMode = "-q:a"; }
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus") { aBitMode = "-vbr on -compression_level 10 -b:a"; } //special rule for opus -b:a -vbr on
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC") { aBitMode = "-q:a"; }
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME") { aBitMode = "-q:a"; }

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate Method: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run("VBR") { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            if (mainwindow.tglVBR.IsChecked == false || mainwindow.tglVBR.IsEnabled == false)
            {
                // CBR
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis") { aBitMode = "-b:a"; }
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus") { aBitMode = "-b:a"; }
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC") { aBitMode = "-b:a"; }
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME") { aBitMode = "-b:a"; }
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitMode = string.Empty; }
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3") { aBitMode = "-b:a"; }
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitMode = string.Empty; }
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitMode = string.Empty; }

                // OGV IS FORCED VBR or it will not work
                if ((string)mainwindow.cboFormat.SelectedItem == "ogv") { aBitMode = "-q:a"; }


                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate Method: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run("CBR") { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            //MessageBox.Show(aBitMode); //debug
        }


        /// <summary>
        /// Audio Bitrate Calculator (Method)
        /// <summary>
        public static void AudioBitrateCalculator(MainWindow mainwindow)
        {
            // set to FFprobe's result
            //FFprobe.inputAudioBitrate = FFprobe.resultAudioBitrate.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

            // If Video is Mute, don't set Audio Bitrate
            if (string.IsNullOrEmpty(FFprobe.inputAudioBitrate))
            {
                // do nothing (dont remove, it will cause substring to overload)
            }
            // Filter out any extra spaces after the first 3 characters IMPORTANT
            else if (FFprobe.inputAudioBitrate.Substring(0, 3) == "N/A")
            {
                FFprobe.inputAudioBitrate = "N/A";
            }

            // If Video has Audio, calculate Bitrate into decimal
            if (FFprobe.inputAudioBitrate != "N/A" && !string.IsNullOrEmpty(FFprobe.inputAudioBitrate))
            {
                // Convert to Decimal
                FFprobe.inputAudioBitrate = Convert.ToString(double.Parse(FFprobe.inputAudioBitrate) * 0.001); // changed from (int.Prase to double.Parse)

                // Apply limits if Bitrate goes over
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis" && Convert.ToDouble(FFprobe.inputAudioBitrate) > 500)
                {
                    FFprobe.inputAudioBitrate = Convert.ToString(500); //was 500,000 (before converting to decimal)
                }
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus" && Convert.ToDouble(FFprobe.inputAudioBitrate) > 510)
                {
                    FFprobe.inputAudioBitrate = Convert.ToString(510); //was 510,000 (before converting to decimal)
                }
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME" && Convert.ToDouble(FFprobe.inputAudioBitrate) > 320)
                {
                    FFprobe.inputAudioBitrate = Convert.ToString(320); //was 320,000 before converting to decimal)
                }
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC" && Convert.ToDouble(FFprobe.inputAudioBitrate) > 400)
                {
                    FFprobe.inputAudioBitrate = Convert.ToString(400); //was 400,000 (before converting to decimal)
                }
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3" && Convert.ToDouble(FFprobe.inputAudioBitrate) > 640)
                {
                    FFprobe.inputAudioBitrate = Convert.ToString(640); //was 640,000 (before converting to decimal)
                }
                // ALAC, FLAC do not need limit

                // Apply limits if Bitrate goes Under
                // Vorbis has a minimum bitrate limit of 45k, if less than, set to 45k
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis" && Convert.ToDouble(FFprobe.inputAudioBitrate) < 45)
                {
                    FFprobe.inputAudioBitrate = Convert.ToString(45);
                }

                // Opus has a minimum bitrate limit of 6k, if less than, set to 6k
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus" && Convert.ToDouble(FFprobe.inputAudioBitrate) < 6)
                {
                    FFprobe.inputAudioBitrate = Convert.ToString(6);
                }

                // add k to value
                FFprobe.inputAudioBitrate = Convert.ToString(double.Parse(FFprobe.inputAudioBitrate)) + "k";
            }
        }


        /// <summary>
        /// BatchAudioBitrateLimiter (Method)
        /// <summary>
        public static String BatchAudioBitrateLimiter(MainWindow mainwindow)
        {
            // -------------------------
            // Batch Limit Bitrates
            // -------------------------
            // Only if Audio ComboBox Auto
            if ((string)mainwindow.cboAudio.SelectedItem == "Auto")
            {
                // Limit Vorbis bitrate to 500k through cmd.exe
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
                {
                    aBitrateLimiter = "& (IF %A gtr 500000 (SET aBitrate=500000) ELSE (echo Bitrate within Vorbis Limit of 500k)) & for /F %A in ('echo %aBitrate%') do (echo %A)";
                }
                // Limit Opus bitrate to 510k through cmd.exe
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                {
                    aBitrateLimiter = "& (IF %A gtr 510000 (SET aBitrate=510000) ELSE (echo Bitrate within Opus Limit of 510k)) & for /F %A in ('echo %aBitrate%') do (echo %A)";
                }
                // Limit AAC bitrate to 400k through cmd.exe
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
                {
                    aBitrateLimiter = "& (IF %A gtr 400000 (SET aBitrate=400000) ELSE (echo Bitrate within AAC Limit of 400k)) & for /F %A in ('echo %aBitrate%') do (echo %A)";
                }
                // Limit AC3 bitrate to 640k through cmd.exe
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3")
                {
                    aBitrateLimiter = "& (IF %A gtr 640000 (SET aBitrate=640000) ELSE (echo Bitrate within AC3 Limit of 640k)) & for /F %A in ('echo %aBitrate%') do (echo %A)";
                }
                // Limit LAME bitrate to 320k through cmd.exe
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
                {
                    aBitrateLimiter = "& (IF %A gtr 320000 (SET aBitrate=320000) ELSE (echo Bitrate within LAME Limit of 320k)) & for /F %A in ('echo %aBitrate%') do (echo %A)";
                }
            }

            // Return Value
            return aBitrateLimiter;
        }


        /// <summary>
        /// BatchAudioQualityAuto (Method)
        /// <summary>
        public static String BatchAudioQualityAuto(MainWindow mainwindow)
        {
            // -------------------------
            // Batch Auto
            // -------------------------
            if (mainwindow.tglBatch.IsChecked == true)
            {
                // -------------------------
                // Batch Audio Auto Bitrates
                // -------------------------

                // Batch CMD Detect
                //
                if ((string)mainwindow.cboAudio.SelectedItem == "Auto")
                {
                    // Make List
                    List<string> BatchAudioAutoList = new List<string>()
                    {
                        // audio
                        "& for /F \"delims=\" %A in ('@" + FFprobe.ffprobe + " -v error -select_streams a:0 -show_entries " + FFprobe.aEntryType + " -of default^=noprint_wrappers^=1:nokey^=1 \"%~f\" 2^>^&1') do (echo)",
                        "& SET aBitrate=%A",

                        // expand var
                        "& for /F %A in ('echo %aBitrate%') do (echo %A)",

                        // basic limiter
                        "& (IF %A EQU N/A (SET aBitrate=320000))",

                        // expand var
                        "& for /F %A in ('echo %aBitrate%') do (echo %A)"
                    };

                    // Join List with Spaces, Remove Empty Strings
                    Audio.batchAudioAuto = string.Join(" ", BatchAudioAutoList.Where(s => !string.IsNullOrEmpty(s)));

                }
                // Batch Audio Copy
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Copy")
                {
                    batchAudioAuto = string.Empty;
                }

                //// Not Auto
                //if ((string)mainwindow.cboAudio.SelectedItem != "Auto")
                //{
                //    batchAudioAuto = string.Empty;
                //}
            }

            // Return Value
            return batchAudioAuto;
        }


        /// <summary>
        /// Audio Quality (Method)
        /// <summary>
        public static String AudioQuality(MainWindow mainwindow)
        {
            // Audio Bitrate Mode (Method)
            AudioBitrateMode(mainwindow);


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Quality: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(Convert.ToString(mainwindow.cboAudio.SelectedItem)) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            // ####################
            // Auto
            // ####################
            if ((string)mainwindow.cboAudio.SelectedItem == "Auto")
            {
                // If Input File has No audio
                if (string.IsNullOrEmpty(FFprobe.inputAudioBitrate) && mainwindow.tglBatch.IsChecked == false)
                {
                    aCodec = string.Empty; //used to be -an, but that is for Mute enabled only
                    aBitMode = string.Empty;
                    aQuality = string.Empty;

                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("") { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }

                // If Input File audio exists and bitrate was detected
                if (FFprobe.inputAudioBitrate != "N/A" && !string.IsNullOrEmpty(FFprobe.inputAudioBitrate))
                {
                    aBitMode = "-b:a";
                    //combine
                    aQuality = aBitMode + " " + FFprobe.inputAudioBitrate;

                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run(FFprobe.inputAudioBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }

                // The following fixes the problem of FFprobe not being able to detect existing Audio Bitrate in an MKV
                // Set new Bitrate Default if Input File Audio exists but FFprobe cant detect Bitrate
                if (!string.IsNullOrEmpty(FFprobe.inputAudioCodec)) // this prevents substring overload
                {
                    // Default to a new bitrate if Input & Output formats DONT match
                    if (FFprobe.inputAudioBitrate == "N/A" && !string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // output codec is specified by the format selected
                        aQuality = "-b:a 320k"; // set a high default

                        Log.WriteAction = () =>
                        {
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                            Log.logParagraph.Inlines.Add(new Run("320k") { Foreground = Log.ConsoleDefault });
                        };
                        Log.LogActions.Add(Log.WriteAction);
                    }
                }

                // NEW RULES
                // Audio Codec Copy cannot have -b:a
                if (aCodec == "-acodec copy")
                {
                    aBitMode = string.Empty;
                    aQuality = string.Empty;
                }
                // If input extension is same as output, use codec copy and remove -b:a and quality
                // This does the same as codec copy above, but just to make sure
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy")
                {
                    aBitMode = string.Empty;
                    aQuality = string.Empty;
                }
            }
            // ####################
            // Lossless
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "Lossless")
            {
                // ALAC
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitMode = string.Empty; aBitrate = string.Empty; }
                // FLAC
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitMode = string.Empty; aBitrate = string.Empty; }
                // PCM
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitMode = string.Empty; aBitrate = string.Empty; }
                //combine
                aQuality = aBitMode + " " + aBitrate;

                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(aBitrate) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);

            }
            // ####################
            // 640
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "640")
            {
                //ac3 value > low 0-10 high
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3" && mainwindow.tglVBR.IsChecked == true) { /* AC3 can't be VBR */ }
                //CBR default
                else { aBitrate = "640k"; }
                //combine
                aQuality = aBitMode + " " + aBitrate;

                // Log Console Message /////////
                if (mainwindow.tglVBR.IsChecked == true)
                {
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + mainwindow.cboAudio.SelectedItem + " to VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }
            // ####################
            // 510
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "510")
            {
                //opus value > low 0-10 high
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "510k"; } //special rule for opus -b:a -vbr on
                //CBR default
                else { aBitrate = "510k"; }
                //combine
                aQuality = aBitMode + " " + aBitrate;

                // Log Console Message /////////
                if (mainwindow.tglVBR.IsChecked == true)
                {
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + mainwindow.cboAudio.SelectedItem + " to VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }
            // ####################
            // 500
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "500")
            {
                //vorbis value > low 0-10 high
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "10"; }
                //CBR default
                else { aBitrate = "500k"; }
                //combine
                aQuality = aBitMode + " " + aBitrate;

                // Log Console Message /////////
                if (mainwindow.tglVBR.IsChecked == true)
                {
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + mainwindow.cboAudio.SelectedItem + " to VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }
            // ####################
            // 448
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "448")
            {
                //ac3 value > low 0.1-2 high
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3" && mainwindow.tglVBR.IsChecked == true) { /* AC3 can't be VBR */ }
                //CBR default
                else { aBitrate = "448k"; }
                //combine
                aQuality = aBitMode + " " + aBitrate;

                // Log Console Message /////////
                if (mainwindow.tglVBR.IsChecked == true)
                {
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + mainwindow.cboAudio.SelectedItem + " to VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }
            // ####################
            // 400
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "400")
            {
                //aac value > low 0.1-2 high
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "2"; }
                //CBR default
                else { aBitrate = "400k"; }
                //combine
                aQuality = aBitMode + " " + aBitrate;

                // Log Console Message /////////
                if (mainwindow.tglVBR.IsChecked == true)
                {
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + mainwindow.cboAudio.SelectedItem + " to VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }
            // ####################
            // 320
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "320")
            {
                //vorbis value > low 0-10 high
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "9"; }
                //opus value > low 0-10 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "320k"; } //special rule for opus -b:a -vbr on
                //aac value > low 0.1-2 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "2"; }
                //lame value > low 9-0 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "0"; }
                //CBR default
                else { aBitrate = "320k"; }
                //combine
                aQuality = aBitMode + " " + aBitrate;

                // Log Console Message /////////
                if (mainwindow.tglVBR.IsChecked == true)
                {
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + mainwindow.cboAudio.SelectedItem + " to VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }
            // ####################
            // 256
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "256")
            {
                //webm value low 0-10 high
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "8"; }
                //opus value > low 0-10 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "256k"; } //special rule for opus -b:a -vbr on
                //aac value > low 0.1-2 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "1.6"; }
                //lame value > low 9-0 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "0"; }
                //CBR default
                else { aBitrate = "256k"; }
                //combine
                aQuality = aBitMode + " " + aBitrate;

                // Log Console Message /////////
                if (mainwindow.tglVBR.IsChecked == true)
                {
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + mainwindow.cboAudio.SelectedItem + " to VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }
            // ####################
            // 224
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "224")
            {
                //webm value low 0-10 high
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "7"; }
                //opus value > low 0-10 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "224k"; } //special rule for opus -b:a -vbr on
                //aac value > low 0.1-2 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "1.4"; }
                //lame value > low 9-0 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "1"; }
                //CBR default
                else { aBitrate = "224k"; }
                //combine
                aQuality = aBitMode + " " + aBitrate;

                // Log Console Message /////////
                if (mainwindow.tglVBR.IsChecked == true)
                {
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + mainwindow.cboAudio.SelectedItem + " to VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }
            // ####################
            // 192
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "192")
            {
                //webm value low 0-10 high
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "6"; }
                //opus value > low 0-10 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "192k"; } //special rule for opus -b:a -vbr on
                //aac value > low 0.1-2 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "1.2"; }
                //lame value > low 9-0 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "2"; }
                //CBR default
                else { aBitrate = "192k"; }
                //combine
                aQuality = aBitMode + " " + aBitrate;

                // Log Console Message /////////
                if (mainwindow.tglVBR.IsChecked == true)
                {
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + mainwindow.cboAudio.SelectedItem + " to VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }
            // ####################
            // 160
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "160")
            {
                //webm value low 0-10 high
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "5"; }
                //opus value > low 0-10 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "160k"; } //special rule for opus -b:a -vbr on
                //aac value > low 0.1-2 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "1.1"; }
                //lame value > low 9-0 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "3"; }
                //CBR default
                else { aBitrate = "160k"; }
                //combine
                aQuality = aBitMode + " " + aBitrate;

                // Log Console Message /////////
                if (mainwindow.tglVBR.IsChecked == true)
                {
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + mainwindow.cboAudio.SelectedItem + " to VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }
            // ####################
            // 128
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "128")
            {
                //webm value low 0-10 high
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "4"; }
                //opus value > low 0-10 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "128k"; } //special rule for opus -b:a -vbr on
                //aac value > low 0.1-2 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "0.8"; }
                //lame value > low 9-0 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "5"; }
                //CBR default
                else { aBitrate = "128k"; }
                //combine
                aQuality = aBitMode + " " + aBitrate;

                // Log Console Message /////////
                if (mainwindow.tglVBR.IsChecked == true)
                {
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + mainwindow.cboAudio.SelectedItem + " to VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }
            // ####################
            // 96
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "96")
            {
                //webm value low 0-10 high
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "2"; }
                //opus value > low 0-10 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "96k"; }
                //aac value > low 0.1-2 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "0.6"; }
                //lame value > low 9-0 high
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME" && mainwindow.tglVBR.IsChecked == true) { aBitrate = "7"; }
                //CBR default
                else { aBitrate = "96k"; }
                //combine
                aQuality = aBitMode + " " + aBitrate;

                // Log Console Message /////////
                if (mainwindow.tglVBR.IsChecked == true)
                {
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + mainwindow.cboAudio.SelectedItem + " to VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }

            }
            // ####################
            // Mute
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "Mute")
            {
                aBitMode = string.Empty; aBitrate = string.Empty; Streams.aMap = "-an";
                //combine
                aQuality = aBitMode + " " + aBitrate;
            }
            // ####################
            // None
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "None")
            {
                aBitMode = string.Empty; aBitrate = string.Empty;
                //combine
                aQuality = aBitMode + " " + aBitrate;
            }
            // ####################
            // Custom
            // ####################
            else if ((string)mainwindow.cboAudio.SelectedItem == "Custom" && !string.IsNullOrWhiteSpace(mainwindow.audioCustom.Text)) //dont allow if blank, crashes
            {
                // Convert user entered text into double
                double audioCustomNum;

                audioCustomNum = Convert.ToDouble(mainwindow.audioCustom.Text);

                /// <summary>
                /// VBR User entered value Algorithm
                /// <summary>

                // -------------------------
                // AAC (M4A, MP4, MKV) USER CUSTOM VBR
                // -------------------------
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC" && mainwindow.tglVBR.IsChecked == true)
                {
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("AAC: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + aBitrate + " to ") { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);

                    // VBR User entered value algorithm
                    aBitrate = String.Concat(audioCustomNum * 0.00625);

                    // AAC VBR Above 320k Error (look into this)
                    if (audioCustomNum > 400)
                    {
                        // Log Console Message /////////
                        Log.WriteAction = () =>
                        {
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new Bold(new Run("Warning: AAC VBR cannot be above 400k.")) { Foreground = Log.ConsoleWarning });
                        };
                        Log.LogActions.Add(Log.WriteAction);


                        System.Windows.MessageBox.Show("Error: AAC VBR cannot be above 400k."); /* lock */ MainWindow.ready = 0;
                    }

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new Run("VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
                else
                {
                    aBitrate = mainwindow.audioCustom.Text + "k";
                }

                // -------------------------
                // VORBIS (WEBM, OGG) USER CUSTOM VBR
                // -------------------------
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis" && mainwindow.tglVBR.IsChecked == true)
                {
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Vorbis: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + aBitrate + " to ") { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);

                    // Above 290k set to 10 Quality
                    if (audioCustomNum > 290)
                    {
                        aBitrate = "10";
                    }
                    // 32 bracket
                    if (audioCustomNum >= 128) //above 113kbps, use standard equation
                    {
                        // VBR User entered value algorithm
                        aBitrate = String.Concat(audioCustomNum * 0.03125);
                    }
                    // 16 bracket
                    if (audioCustomNum <= 127) //112kbps needs work, half decimal off
                    {
                        // VBR User entered value algorithm
                        aBitrate = String.Concat((audioCustomNum * 0.03125) - 0.5);
                    }
                    if (audioCustomNum <= 96)
                    {
                        // VBR User entered value algorithm
                        aBitrate = String.Concat((audioCustomNum * 0.013125) - 0.25);
                    }
                    // 8 bracket
                    if (audioCustomNum <= 64)
                    {
                        // VBR User entered value algorithm
                        aBitrate = String.Concat(0);
                    }

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new Run("VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }

                // -------------------------
                // LAME (MP3) USER CUSTOM VBR
                // -------------------------
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME" && mainwindow.tglVBR.IsChecked == true)
                {
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("LAME: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + aBitrate + " to ") { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);

                    // Above 245k set to V0
                    if (audioCustomNum > 260)
                    {
                        aBitrate = "0";
                    }
                    else
                    {
                        // VBR User entered value algorithm (0 high / 10 low)
                        aBitrate = String.Concat((((audioCustomNum * (-0.01)) / 2.60) + 1) * 10);
                    }

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new Run("VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }

                //combine
                aQuality = aBitMode + " " + aBitrate;

            }//end custom

            //if blank, remove aQuality (prevents the last variable from being used again)
            if (string.IsNullOrWhiteSpace(mainwindow.audioCustom.Text))
            {
                aQuality = string.Empty;
            }

            // Audio Codec Copy - (Must be at this location)
            if ((string)mainwindow.cboAudioCodec.SelectedItem == "Copy")
            {
                aCodec = "-acodec copy";
                aBitMode = string.Empty;
                aBitrate = string.Empty;
                aQuality = string.Empty;
            }


            // If Audio = Auto, use the CMD Batch Audio Variable
            if (mainwindow.tglBatch.IsChecked == true
                && (string)mainwindow.cboAudio.SelectedItem == "Auto"
                && (string)mainwindow.cboAudioCodec.SelectedItem != "Copy")
            {
                Audio.aQuality = "-b:a %A";
                // Skipped if Codec Copy
            }


            // Remove any white space from end of string
            aQuality = aQuality.Trim();
            aQuality = aQuality.TrimEnd();

            // Return Value
            return aQuality;
        }


        /// <summary>
        /// Channel (Method)
        /// <summary>
        public static String Channel(MainWindow mainwindow)
        {
            // Auto
            if ((string)mainwindow.cboChannel.SelectedItem == "Auto")
            {
                aChannel = string.Empty;
            }
            // Stereo
            else if ((string)mainwindow.cboChannel.SelectedItem == "Stereo")
            {
                aChannel = "-ac 2";
            }
            // Joint Stereo
            else if ((string)mainwindow.cboChannel.SelectedItem == "Joint Stereo")
            {
                aChannel = "-ac 2 -joint_stereo 1";
            }
            // Mono
            else if ((string)mainwindow.cboChannel.SelectedItem == "Mono")
            {
                aChannel = "-ac 1";
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Channel: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(mainwindow.cboChannel.Text.ToString()) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            // Return Value
            return aChannel;
        }


        /// <summary>
        /// Sample Rate (Method)
        /// <summary>
        public static String SampleRate(MainWindow mainwindow)
        {
            if ((string)mainwindow.cboSamplerate.SelectedItem == "auto")
            {
                aSamplerate = string.Empty;
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "8k")
            {
                aSamplerate = "-ar 8000";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "11.025k")
            {
                aSamplerate = "-ar 11025";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "12k")
            {
                aSamplerate = "-ar 12000";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "16k")
            {
                aSamplerate = "-ar 16000";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "22.05k")
            {
                aSamplerate = "-ar 22050";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "24k")
            {
                aSamplerate = "-ar 24000";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "32k")
            {
                aSamplerate = "-ar 32000";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "44.1k")
            {
                aSamplerate = "-ar 44100";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "48k")
            {
                aSamplerate = "-ar 48000";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "64k")
            {
                aSamplerate = "-ar 64000";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "88.2k")
            {
                aSamplerate = "-ar 88200";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "96k")
            {
                aSamplerate = "-ar 96000";
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Sample Rate: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(mainwindow.cboSamplerate.Text.ToString()) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);


            // Return Value
            return aSamplerate;
        }


        /// <summary>
        /// Bit Depth (Method)
        /// <summary>
        public static String BitDepth(MainWindow mainwindow)
        {
            // PCM has Bitdepth defined by Codec instead of sample_fmt, can use 8, 16, 24, 32, 64-bit
            // FLAC can only use 16 and 32-bit
            // ALAC can only use 16 and 32-bit

            if ((string)mainwindow.cboBitDepth.SelectedItem == "auto")
            {
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitDepth = string.Empty; aCodec = "-acodec pcm_s24le"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitDepth = string.Empty; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitDepth = string.Empty; }
                else { aBitDepth = string.Empty; } // all other codecs
            }
            else if ((string)mainwindow.cboBitDepth.SelectedItem == "8")
            {
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitDepth = string.Empty; aCodec = "-acodec pcm_u8"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitDepth = string.Empty; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitDepth = string.Empty; }
                else { aBitDepth = string.Empty; } // all other codecs
            }
            else if ((string)mainwindow.cboBitDepth.SelectedItem == "16")
            {
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitDepth = string.Empty; aCodec = "-acodec pcm_s16le"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitDepth = "-sample_fmt s16"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitDepth = "-sample_fmt s16p"; }
                else { aBitDepth = string.Empty; } // all other codecs
            }
            else if ((string)mainwindow.cboBitDepth.SelectedItem == "24")
            {
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitDepth = string.Empty; aCodec = "-acodec pcm_s24le"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitDepth = string.Empty; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitDepth = string.Empty; }
                else { aBitDepth = string.Empty; } // all other codecs
            }
            else if ((string)mainwindow.cboBitDepth.SelectedItem == "32")
            {
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitDepth = string.Empty; aCodec = "-acodec pcm_f32le"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitDepth = "-sample_fmt s32"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitDepth = "-sample_fmt s32p"; }
                else { aBitDepth = string.Empty; } // all other codecs
            }
            else if ((string)mainwindow.cboBitDepth.SelectedItem == "64")
            {
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitDepth = string.Empty; aCodec = "-acodec pcm_f64le"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitDepth = string.Empty; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitDepth = string.Empty; }
                else { aBitDepth = string.Empty; } // all other codecs
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Bit Depth: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(mainwindow.cboBitDepth.SelectedItem.ToString()) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);


            // Return Value
            return aBitDepth;
        }


        /// <summary>
        /// Volume (Method)
        /// <summary>
        public static void Volume(MainWindow mainwindow)
        {
            // Only if Audio Codec is Not Empty
            if (!string.IsNullOrEmpty((string)mainwindow.cboAudioCodec.SelectedItem))
            {
                // If TextBox is 100% or Blank
                if (mainwindow.volumeUpDown.Text == "100%" 
                    || mainwindow.volumeUpDown.Text == "100" 
                    || string.IsNullOrWhiteSpace(mainwindow.volumeUpDown.Text))
                {
                    // aFilter Switch
                    //aFilterSwitch = 0;

                    volume = string.Empty;
                }
                // If User Custom Entered Value
                // Convert Volume % to Decimal
                else
                {
                    // aFilter Switch
                    aFilterSwitch += 1;

                    // If user enters value, turn on Filter
                    string volumePercent = mainwindow.volumeUpDown.Text;
                    double volumeDecimal = double.Parse(volumePercent.TrimEnd(new[] { '%' })) / 100;
                    //volume = "\"volume=" + volumeDecimal + "\""; //old
                    volume = "volume=" + volumeDecimal;
                }
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Volume: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(mainwindow.volumeUpDown.Text.ToString()) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);
        }





        /// <summary>
        /// ALimiter Filter (Method)
        /// <summary>
        public static void ALimiter(MainWindow mainwindow)
        {
            if (mainwindow.tglAudioLimiter.IsChecked == true && !string.IsNullOrEmpty((string)mainwindow.cboAudioCodec.SelectedItem)) // Only if Audio Codec is Not Empty
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("alimiter Toggle: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run("On") { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);

                aLimiter = "alimiter=level_in=1:level_out=1:limit=" + mainwindow.audioLimiter.Text + ":attack=7:release=100:level=disabled";

                // aFilter Switch
                aFilterSwitch += 1;

            }
            if (mainwindow.tglAudioLimiter.IsChecked == false)
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("alimiter Toggle: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run("Off") { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);

                aLimiter = string.Empty; //off
            }

            // If TextBox Empty
            if (mainwindow.tglAudioLimiter.IsChecked == true && string.IsNullOrWhiteSpace(mainwindow.audioLimiter.Text))
            {
                // aFilter Switch
                if (aFilterSwitch == 1)
                {
                    aFilterSwitch = 0; //off
                }
                if (aFilterSwitch > 1)
                {
                    aFilterSwitch = 1; //on
                }

            }
        }


        /// <summary>
        /// Audio Filter Combine (Method)
        /// <summary>
        public static String AudioFilter(MainWindow mainwindow)
        {
            // Initialize the Filter for all
            // Clear Filter for next run
            // Anything that pertains to Audio must be after the aFilter
            //aFilterSwitch = string.Empty; //do not reset the switch between converts
            aFilter = string.Empty; //important


            // Filters
            /// <summary>
            ///    Volume
            /// </summary> 
            Audio.Volume(mainwindow);

            /// <summary>
            ///    ALimiter
            /// </summary> 
            Audio.ALimiter(mainwindow);



            // aFilter Switch   (On, Combine, Off, Empty)
            // If -af alMainWindow.ready on, MainWindow.ready to combine multiple filters

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Filter: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(aFilterSwitch.ToString()) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            // Use Single Filter
            if (aFilterSwitch == 1)
            {
                aFilter = "-af " + volume + aLimiter; //either volume or aLimiter will be enabled
            }
            // Combine Multiple Filters
            else if (aFilterSwitch > 1)
            {
                // Add Filters to List
                if (!string.IsNullOrEmpty(volume)) { AudioFilters.Add(volume); }
                if (!string.IsNullOrEmpty(aLimiter)) { AudioFilters.Add(aLimiter); }

                // Join List with Comma, Remove Empty Strings
                aFilter = "-af \"" + string.Join(", ", AudioFilters.Where(s => !string.IsNullOrEmpty(s))) + "\"";
            }
            else if (aFilterSwitch == 0)
            {
                aFilter = string.Empty;
            }
            // No Filters
            else if (aFilterSwitch == null)
            {
                aFilterSwitch = 0;
                aFilter = string.Empty;
            }

            // Remove aFilter if Video Codec is Empty
            if (string.IsNullOrEmpty((string)mainwindow.cboAudioCodec.SelectedItem))
            {
                aFilterSwitch = 0;
                aFilter = string.Empty;
            }


            // Return Value
            return aFilter;
        }

    }
}