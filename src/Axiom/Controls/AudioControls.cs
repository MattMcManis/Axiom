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

/* ----------------------------------
 METHODS

 * Set Controls
 * BitRate Display
 * Auto Codec Copy
---------------------------------- */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using ViewModel;
using Axiom;
using System.Collections.ObjectModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Controls.Audio
{
    public class Controls
    {
        public static Dictionary<string, dynamic> codecClasses = new Dictionary<string, dynamic>
        {
            { "Vorbis", new Codec.Vorbis() },
            { "Opus",   new Codec.Opus() },
            { "AC3",    new Codec.AC3() },
            { "AAC",    new Codec.AAC() },
            { "DTS",    new Codec.DTS() },
            { "MP2",    new Codec.MP2() },
            { "LAME",   new Codec.LAME() },
            { "ALAC",   new Codec.ALAC() },
            { "FLAC",   new Codec.FLAC() },
            { "PCM",    new Codec.PCM() },
            { "Copy",   new Codec.Copy() },
            { "None",   new Codec.None() }
        };

        private static Dictionary<string, IAudioCodec> _codec_class;

        private static void InitializeCodecs()
        {
            _codec_class = codecClasses.ToDictionary(k => k.Key, k => (IAudioCodec)k.Value);

            //_codec_class = new Dictionary<string, IAudioCodec> {
            //    { "Vorbis", new Codec.Vorbis() },
            //    { "Opus",   new Codec.Opus() },
            //    { "AC3",    new Codec.AC3() },
            //    { "AAC",    new Codec.AAC() },
            //    { "DTS",    new Codec.DTS() },
            //    { "MP2",    new Codec.MP2() },
            //    { "LAME",   new Codec.LAME() },
            //    { "ALAC",   new Codec.ALAC() },
            //    { "FLAC",   new Codec.FLAC() },
            //    { "PCM",    new Codec.PCM() },
            //    { "Copy",   new Codec.Copy() },
            //    { "None",   new Codec.None() }
            //};
        }

        public interface IAudioCodec
        {
            // Codec
            ObservableCollection<ViewModel.Audio.AudioCodec> codec { get; set; }

            // Items Source
            ObservableCollection<string> channel { get; set; }
            ObservableCollection<ViewModel.Audio.AudioQuality> quality { get; set; }
            ObservableCollection<string> compressionLevel { get; set; }
            ObservableCollection<ViewModel.Audio.AudioSampleRate> sampleRate { get; set; }
            ObservableCollection<ViewModel.Audio.AudioBitDepth> bitDepth { get; set; }

            // Selected Items
            List<ViewModel.Audio.Selected> controls_Selected { get; set; }

            // Checked
            List<ViewModel.Audio.Checked> controls_Checked { get; set; }

            // Enabled
            List<ViewModel.Audio.Enabled> controls_Enabled { get; set; }
        }


        /// <summary>
        /// Codec Controls
        /// </summary>
        public static void CodecControls(string codec_SelectedItem)
        {
            // --------------------------------------------------
            // Codec
            // --------------------------------------------------

            if (!string.IsNullOrWhiteSpace(codec_SelectedItem))
            {
                InitializeCodecs();

                // -------------------------
                // Codec
                // -------------------------
                List<string> codec = new List<string>()
                {
                    // Combine Codec + Parameters
                    "-c:a",
                    _codec_class[codec_SelectedItem].codec.FirstOrDefault()?.Codec,
                    _codec_class[codec_SelectedItem].codec.FirstOrDefault()?.Parameters,
                };

                VM.AudioView.Audio_Codec = string.Join(" ", codec.Where(s => !string.IsNullOrEmpty(s)));

                // --------------------------------------------------
                // Save Previous Item
                // --------------------------------------------------
                // Save before changing Items Source
                // -------------------------
                // Stream
                // -------------------------
                if (MainWindow.SavePreviousItemCond(VM.AudioView.Audio_Stream_SelectedItem) == true)
                {
                    MainWindow.Audio_Stream_PreviousItem = VM.AudioView.Audio_Stream_SelectedItem;
                }

                // -------------------------
                // Channel
                // -------------------------
                if (MainWindow.SavePreviousItemCond(VM.AudioView.Audio_Channel_SelectedItem) == true)
                {
                    MainWindow.Audio_Channel_PreviousItem = VM.AudioView.Audio_Channel_SelectedItem;
                }

                // -------------------------
                // Quality
                // -------------------------
                if (MainWindow.SavePreviousItemCond(VM.AudioView.Audio_Quality_SelectedItem) == true)
                {
                    MainWindow.Audio_Quality_PreviousItem = VM.AudioView.Audio_Quality_SelectedItem;
                }

                // -------------------------
                // Compression Level
                // -------------------------
                if (MainWindow.SavePreviousItemCond(VM.AudioView.Audio_CompressionLevel_SelectedItem) == true)
                {
                    MainWindow.Audio_CompressionLevel_PreviousItem = VM.AudioView.Audio_CompressionLevel_SelectedItem;
                }

                // -------------------------
                // Sample Rate
                // -------------------------
                if (MainWindow.SavePreviousItemCond(VM.AudioView.Audio_SampleRate_SelectedItem) == true)
                {
                    MainWindow.Audio_SampleRate_PreviousItem = VM.AudioView.Audio_SampleRate_SelectedItem;
                }

                // -------------------------
                // Bit Depth
                // -------------------------
                if (MainWindow.SavePreviousItemCond(VM.AudioView.Audio_BitDepth_SelectedItem) == true)
                {
                    MainWindow.Audio_BitDepth_PreviousItem = VM.AudioView.Audio_BitDepth_SelectedItem;
                }

                // --------------------------------------------------
                // Save Previous Checked
                // --------------------------------------------------
                MainWindow.Audio_VBR_PreviousChecked = VM.AudioView.Audio_VBR_IsChecked;

                // --------------------------------------------------
                // Items Source
                // --------------------------------------------------
                // -------------------------
                // Channel
                // -------------------------
                VM.AudioView.Audio_Channel_Items = _codec_class[codec_SelectedItem].channel;

                // -------------------------
                // Quality
                // -------------------------
                VM.AudioView.Audio_Quality_Items = _codec_class[codec_SelectedItem].quality;

                // -------------------------
                // Compression Level
                // -------------------------
                VM.AudioView.Audio_CompressionLevel_Items = _codec_class[codec_SelectedItem].compressionLevel;

                // -------------------------
                // Sample Rate
                // -------------------------
                VM.AudioView.Audio_SampleRate_Items = _codec_class[codec_SelectedItem].sampleRate;

                // -------------------------
                // Bit Depth
                // -------------------------
                VM.AudioView.Audio_BitDepth_Items = _codec_class[codec_SelectedItem].bitDepth;


                // --------------------------------------------------
                // Enabled (Nullable)
                // --------------------------------------------------
                // Codec
                //VM.AudioView.Audio_Codec_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.Codec;
                bool? codec_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.Codec;
                if (!codec_IsEnabled.HasValue)
                    VM.AudioView.Audio_Codec_IsEnabled = true;
                else
                    VM.AudioView.Audio_Codec_IsEnabled = codec_IsEnabled;

                // Stream
                //VM.AudioView.Audio_Stream_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.Stream;
                bool? stream_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.Stream;
                if (!stream_IsEnabled.HasValue)
                    VM.AudioView.Audio_Stream_IsEnabled = true;
                else
                    VM.AudioView.Audio_Stream_IsEnabled = stream_IsEnabled;

                // Channel
                //VM.AudioView.Audio_Channel_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.Channel;
                bool? channel_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.Channel;
                if (!channel_IsEnabled.HasValue)
                    VM.AudioView.Audio_Channel_IsEnabled = true;
                else
                    VM.AudioView.Audio_Channel_IsEnabled = channel_IsEnabled;

                // Quality
                //VM.AudioView.Audio_Quality_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.Quality;
                bool? quality_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.Quality;
                if (!quality_IsEnabled.HasValue)
                    VM.AudioView.Audio_Quality_IsEnabled = true;
                else
                    VM.AudioView.Audio_Quality_IsEnabled = quality_IsEnabled;

                // Compression Level
                //VM.AudioView.Audio_CompressionLevel_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.CompressionLevel;
                bool? compressionLevel_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.CompressionLevel;
                if (!compressionLevel_IsEnabled.HasValue)
                    VM.AudioView.Audio_CompressionLevel_IsEnabled = true;
                else
                    VM.AudioView.Audio_CompressionLevel_IsEnabled = compressionLevel_IsEnabled;

                // VBR
                //VM.AudioView.Audio_VBR_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.VBR;
                bool? vbr_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.VBR;
                if (!vbr_IsEnabled.HasValue)
                    VM.AudioView.Audio_VBR_IsEnabled = true;
                else
                    VM.AudioView.Audio_VBR_IsEnabled = vbr_IsEnabled;

                // Sample Rate
                //VM.AudioView.Audio_SampleRate_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.SampleRate;
                bool? sampleRate_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.SampleRate;
                if (!sampleRate_IsEnabled.HasValue)
                    VM.AudioView.Audio_SampleRate_IsEnabled = true;
                else
                    VM.AudioView.Audio_SampleRate_IsEnabled = sampleRate_IsEnabled;

                // Bit Depth
                //VM.AudioView.Audio_BitDepth_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.BitDepth;
                bool? bitDepth_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.BitDepth;
                if (!bitDepth_IsEnabled.HasValue)
                    VM.AudioView.Audio_BitDepth_IsEnabled = true;
                else
                    VM.AudioView.Audio_BitDepth_IsEnabled = bitDepth_IsEnabled;

                // Volume
                //VM.AudioView.Audio_Volume_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.Volume;
                bool? volume_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.Volume;
                if (!volume_IsEnabled.HasValue)
                    VM.AudioView.Audio_Volume_IsEnabled = true;
                else
                    VM.AudioView.Audio_Volume_IsEnabled = volume_IsEnabled;

                // Hard Limiter
                //VM.AudioView.Audio_HardLimiter_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.HardLimiter;
                bool? hardLimiter_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.HardLimiter;
                if (!hardLimiter_IsEnabled.HasValue)
                    VM.AudioView.Audio_HardLimiter_IsEnabled = true;
                else
                    VM.AudioView.Audio_HardLimiter_IsEnabled = hardLimiter_IsEnabled;

                // Filters
                // Disable All
                if (codec_SelectedItem == "Copy" ||
                    codec_SelectedItem == "None")
                {
                    Filters.Audio.AudioFilters_DisableAll();
                }
                // Enable All
                else
                {
                    Filters.Audio.AudioFilters_EnableAll();
                }


                // --------------------------------------------------
                // Selected Items
                // --------------------------------------------------
                // -------------------------
                // Stream
                // -------------------------
                string stream = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().Stream;

                // Has Value
                if (!string.IsNullOrEmpty(stream))
                {
                    VM.AudioView.Audio_Stream_SelectedItem = stream;
                }
                // Previous Item
                else
                {
                    // Select the Prevoius Codec's Item
                    VM.AudioView.Audio_Stream_SelectedItem = MainWindow.SelectedItem(
                                                                      VM.AudioView.Audio_Stream_Items.ToList(),
                                                                      MainWindow.Audio_Stream_PreviousItem
                                                                  );
                    // Clear Previous Item
                    MainWindow.Audio_Stream_PreviousItem = string.Empty;
                }

                // -------------------------
                // Channel
                // -------------------------
                string channel = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().Channel;

                // Has Value
                if (!string.IsNullOrEmpty(channel))
                {
                    VM.AudioView.Audio_Channel_SelectedItem = channel;
                }
                // Previous Item
                else
                {
                    // Select the Prevoius Codec's Item
                    VM.AudioView.Audio_Channel_SelectedItem = MainWindow.SelectedItem(
                                                                      VM.AudioView.Audio_Channel_Items.ToList(),
                                                                      MainWindow.Audio_Channel_PreviousItem
                                                                  );
                    // Clear Previous Item
                    MainWindow.Audio_Channel_PreviousItem = string.Empty;
                }

                // -------------------------
                // Quality
                // -------------------------
                string quality = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().Quality;

                // Has Value
                if (!string.IsNullOrEmpty(quality))
                {
                    VM.AudioView.Audio_Quality_SelectedItem = quality;
                }
                // Previous Item
                else
                {
                    // Select the Prevoius Codec's Item
                    VM.AudioView.Audio_Quality_SelectedItem = MainWindow.SelectedItem(
                                                                      VM.AudioView.Audio_Quality_Items.Select(x => x.Name).ToList(),
                                                                      MainWindow.Audio_Quality_PreviousItem
                                                                  );
                    // Clear Previous Item
                    MainWindow.Audio_Quality_PreviousItem = string.Empty;
                }

                // For errors causing ComboBox not to select an item
                if (string.IsNullOrWhiteSpace(VM.AudioView.Audio_Quality_SelectedItem))
                {
                    // Default to First Item
                    VM.AudioView.Audio_Quality_Items.FirstOrDefault();
                }

                // -------------------------
                // Compression Level
                // -------------------------
                string compressionLevel = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().CompressionLevel;

                // Has Value
                if (!string.IsNullOrEmpty(compressionLevel))
                {
                    VM.AudioView.Audio_CompressionLevel_SelectedItem = compressionLevel;
                }
                // Previous Item
                else
                {
                    // Select the Prevoius Codec's Item
                    VM.AudioView.Audio_CompressionLevel_SelectedItem = MainWindow.SelectedItem(
                                                                      VM.AudioView.Audio_CompressionLevel_Items.ToList(),
                                                                      MainWindow.Audio_CompressionLevel_PreviousItem
                                                                  );
                    // Clear Previous Item
                    MainWindow.Audio_CompressionLevel_PreviousItem = string.Empty;
                }

                // -------------------------
                // Sample Rate
                // -------------------------
                string sampleRate = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().SampleRate;

                // Has Value
                if (!string.IsNullOrEmpty(sampleRate))
                {
                    VM.AudioView.Audio_SampleRate_SelectedItem = sampleRate;
                }
                // Previous Item
                else
                {
                    // Select the Prevoius Codec's Item
                    VM.AudioView.Audio_SampleRate_SelectedItem = MainWindow.SelectedItem(
                                                                      VM.AudioView.Audio_SampleRate_Items.Select(x => x.Name).ToList(),
                                                                      MainWindow.Audio_SampleRate_PreviousItem
                                                                  );
                    // Clear Previous Item
                    MainWindow.Audio_SampleRate_PreviousItem = string.Empty;
                }

                // For errors causing ComboBox not to select an item
                if (string.IsNullOrWhiteSpace(VM.AudioView.Audio_SampleRate_SelectedItem))
                {
                    // Default to First Item
                    VM.AudioView.Audio_SampleRate_Items.FirstOrDefault();
                }

                // -------------------------
                // Bit Depth
                // -------------------------
                string bitDepth = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().BitDepth;

                // Has Value
                if (!string.IsNullOrEmpty(bitDepth))
                {
                    VM.AudioView.Audio_BitDepth_SelectedItem = bitDepth;
                }
                // Previous Item
                else
                {
                    // Select the Prevoius Codec's Item
                    VM.AudioView.Audio_BitDepth_SelectedItem = MainWindow.SelectedItem(
                                                                      VM.AudioView.Audio_BitDepth_Items.Select(x => x.Name).ToList(),
                                                                      MainWindow.Audio_BitDepth_PreviousItem
                                                                  );
                    // Clear Previous Item
                    MainWindow.Audio_BitDepth_PreviousItem = string.Empty;
                }

                // For errors causing ComboBox not to select an item
                if (string.IsNullOrWhiteSpace(VM.AudioView.Audio_BitDepth_SelectedItem))
                {
                    // Default to First Item
                    VM.AudioView.Audio_BitDepth_Items.FirstOrDefault();
                }

                // -------------------------
                // Filters
                // -------------------------
                // Select Defaults
                if (codec_SelectedItem == "Copy" ||
                    codec_SelectedItem == "None")
                {
                    Filters.Audio.AudioFilters_ControlsSelectDefaults();
                }


                // --------------------------------------------------
                // Checked (Nullable)
                // --------------------------------------------------
                // VBR
                //VM.AudioView.Audio_VBR_IsChecked = _codec_class[codec_SelectedItem].controls_Checked.FirstOrDefault().VBR;
                if (VM.AudioView.Audio_VBR_IsEnabled == true)
                {
                    bool? vbr_IsChecked = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault()?.Stream;
                    if (!vbr_IsChecked.HasValue)
                        VM.AudioView.Audio_VBR_IsChecked = true;
                    else
                        VM.AudioView.Audio_VBR_IsChecked = vbr_IsChecked;
                }
                else
                {
                    VM.AudioView.Audio_VBR_IsChecked = false;
                }
            }
        }


        /// <summary>
        /// Audio BitRate Display
        /// </summary>
        public static void AudioBitRateDisplay(ObservableCollection<ViewModel.Audio.AudioQuality> items,
                                               string selectedQuality
                                              )
        {
            if (!string.IsNullOrEmpty(VM.AudioView.Audio_Quality_SelectedItem) &&
                VM.AudioView.Audio_Quality_SelectedItem != "None" &&
                VM.AudioView.Audio_Quality_SelectedItem != "Auto" &&
                VM.AudioView.Audio_Quality_SelectedItem != "Lossless" &&
                VM.AudioView.Audio_Quality_SelectedItem != "Custom" &&
                VM.AudioView.Audio_Quality_SelectedItem != "Mute")
            {
                // -------------------------
                // Display in TextBox
                // -------------------------
                switch (VM.AudioView.Audio_VBR_IsChecked)
                {
                    // Bit Rate CBR
                    case false:
                        VM.AudioView.Audio_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.CBR;
                        break;

                    // Bit Rate VBR
                    case true:
                        VM.AudioView.Audio_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.VBR;
                        break;
                }
            }

            // Disable
            else if (string.IsNullOrEmpty(VM.AudioView.Audio_Quality_SelectedItem) ||
                VM.AudioView.Audio_Quality_SelectedItem == "None" ||
                VM.AudioView.Audio_Quality_SelectedItem == "Auto" ||
                VM.AudioView.Audio_Quality_SelectedItem == "Lossless" ||
                //VM.AudioView.Audio_Quality_SelectedItem == "Custom" ||
                VM.AudioView.Audio_Quality_SelectedItem == "Mute")
            {
                VM.AudioView.Audio_BitRate_Text = string.Empty;
            }
        }


        /// <summary>
        /// Quality Controls
        /// <summary>
        public static void QualityControls()
        {
            switch (VM.AudioView.Audio_Quality_SelectedItem)
            {
                // -------------------------
                // Enable
                // -------------------------
                // Only for Custom
                case "Custom":
                    // BitRate
                    VM.AudioView.Audio_BitRate_IsEnabled = true;
                    VM.AudioView.Audio_BitRate_Text = "";
                    break;

                // -------------------------
                // Disable
                // -------------------------
                // Only for Custom
                case "Auto":
                    // BitRate
                    VM.AudioView.Audio_BitRate_IsEnabled = false;
                    VM.AudioView.Audio_BitRate_Text = "";
                    break;

                // All Other Qualities
                default:
                    // BitRate
                    VM.AudioView.Audio_BitRate_IsEnabled = false;
                    break;
            }

            //// Only for Custom
            //if (VM.AudioView.Audio_Quality_SelectedItem == "Custom")
            //{
            //    // BitRate
            //    VM.AudioView.Audio_BitRate_IsEnabled = true;
            //    VM.AudioView.Audio_BitRate_Text = "";
            //}

            //// -------------------------
            //// Disable
            //// -------------------------
            //// Only for Custom
            //else if (VM.AudioView.Audio_Quality_SelectedItem == "Auto")
            //{
            //    // BitRate
            //    VM.AudioView.Audio_BitRate_IsEnabled = false;
            //    VM.AudioView.Audio_BitRate_Text = "";
            //}
            //// All Other Qualities
            //else
            //{
            //    // BitRate
            //    VM.AudioView.Audio_BitRate_IsEnabled = false;
            //}


            //// -------------------------
            //// Compression Level
            //// -------------------------
            //if (VM.AudioView.Audio_Codec_SelectedItem == "Opus")
            //{
            //    switch (VM.AudioView.Audio_VBR_IsChecked)
            //    {
            //        // VBR
            //        // Enable
            //        case true:
            //            VM.AudioView.Audio_CompressionLevel_IsEnabled = true;
            //            //VM.AudioView.Audio_CompressionLevel_SelectedItem = "10";
            //            break;

            //        // CBR
            //        // Disable
            //        case false:
            //            VM.AudioView.Audio_CompressionLevel_IsEnabled = false;
            //            VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
            //            break;
            //    }
            //}
        }


        /// <summary>
        /// Auto Copy Conditions Check
        /// <summary>
        //public static bool AutoCopyConditionsCheck()
        //{
        //    // Failed
        //    if (VM.AudioView.Audio_Quality_SelectedItem != "Auto" ||
        //        VM.AudioView.Audio_Channel_SelectedItem != "Source" ||
        //        //VM.AudioView.Audio_SampleRate_SelectedItem != "auto" //||
        //        VM.AudioView.Audio_BitDepth_SelectedItem != "auto" ||
        //        VM.AudioView.Audio_HardLimiter_Value != 0.0 ||
        //        VM.AudioView.Audio_Volume_Text != "100" ||
        //        // Filters
        //        VM.FilterAudioView.FilterAudio_Lowpass_SelectedItem != "disabled" ||
        //        VM.FilterAudioView.FilterAudio_Highpass_SelectedItem != "disabled" ||
        //        VM.FilterAudioView.FilterAudio_Headphones_SelectedItem != "disabled" ||
        //        VM.FilterAudioView.FilterAudio_Contrast_Value != 0 ||
        //        VM.FilterAudioView.FilterAudio_ExtraStereo_Value != 0 ||
        //        VM.FilterAudioView.FilterAudio_Tempo_Value != 100
        //        )
        //    {
        //        //MessageBox.Show("false");
        //        return false;
        //    }

        //    // Passed
        //    else
        //    {
        //        //MessageBox.Show("true");
        //        return true;
        //    }
        //}


        /// <summary>
        /// Auto Copy Audio Codec
        /// <summary>
        /// <remarks>
        /// Input Extension is same as Output Extension and Audio Quality is Auto
        /// </remarks>
        //public static void AutoCopyAudioCodec(string trigger)
        //{
        //    //string audio_Quality_SelectedItem = VM.AudioView.Audio_Quality_Items.FirstOrDefault(item => item.Name == VM.AudioView.Audio_Quality_SelectedItem)?.Name;

        //    //MessageBox.Show(VM.AudioView.Audio_Quality_SelectedItem);

        //    // -------------------------
        //    // Halt if Selected Codec is Null
        //    // -------------------------
        //    if (string.IsNullOrWhiteSpace(VM.AudioView.Audio_Codec_SelectedItem))
        //    {
        //        return;
        //    }

        //    // -------------------------
        //    // Halt if trigger is control
        //    // Pass if trigger is input
        //    // -------------------------
        //    if (trigger == "control" &&
        //        VM.AudioView.Audio_Codec_SelectedItem != "Copy" &&
        //        AutoCopyConditionsCheck() == true)
        //    {
        //        return;
        //    }

        //    // -------------------------
        //    // Halt if Web URL
        //    // -------------------------
        //    if (MainWindow.IsWebURL(VM.MainView.Input_Text) == true)
        //    {
        //        return;
        //    }

        //    // -------------------------
        //    // Get Input Extensions
        //    // -------------------------
        //    string inputExt = Path.GetExtension(VM.MainView.Input_Text);

        //    // -------------------------
        //    // Halt if Input Extension is Empty
        //    // -------------------------
        //    if (string.IsNullOrWhiteSpace(inputExt))
        //    {
        //        return;
        //    }

        //    // -------------------------
        //    // Get Output Extensions
        //    // -------------------------
        //    string outputExt = "." + VM.FormatView.Format_Container_SelectedItem;

        //    // -------------------------
        //    // Conditions Check
        //    // Enable
        //    // -------------------------
        //    if (AutoCopyConditionsCheck() == true &&
        //        string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
        //    {
        //        // Set Audio Codec Combobox Selected Item to Copy
        //        if (VM.AudioView.Audio_Codec_Items.Count > 0)
        //        {
        //            if (VM.AudioView.Audio_Codec_Items?.Contains("Copy") == true)
        //            {
        //                VM.AudioView.Audio_Codec_SelectedItem = "Copy";
        //            }
        //        }
        //    }

        //    // -------------------------
        //    // Reset to Default Codec
        //    // -------------------------
        //    // Disable Copy if:
        //    // Input / Output Extensions don't match
        //    // Audio is Not Auto 
        //    // VBR is Checked
        //    // Samplerate is Not auto
        //    // BitDepth is Not auto
        //    // Alimiter is Checked
        //    // Volume is Not 100
        //    // -------------------------
        //    else
        //    {
        //        // -------------------------
        //        // Copy Selected
        //        // -------------------------
        //        if (VM.AudioView.Audio_Codec_SelectedItem == "Copy")
        //        {
        //            switch (VM.FormatView.Format_Container_SelectedItem)
        //            {
        //                // -------------------------
        //                // Video Container
        //                // -------------------------
        //                // WebM
        //                case "webm":
        //                    VM.AudioView.Audio_Codec_SelectedItem = "Vorbis";
        //                    break;
        //                // MP4
        //                case "mp4":
        //                    VM.AudioView.Audio_Codec_SelectedItem = "AAC";
        //                    break;
        //                // MKV
        //                case "mkv":
        //                    VM.AudioView.Audio_Codec_SelectedItem = "AC3";
        //                    break;
        //                // M2V
        //                case "m2v":
        //                    VM.AudioView.Audio_Codec_SelectedItem = "None";
        //                    break;
        //                // MPG
        //                case "mpg":
        //                    VM.AudioView.Audio_Codec_SelectedItem = "MP2";
        //                    break;
        //                // AVI
        //                case "avi":
        //                    VM.AudioView.Audio_Codec_SelectedItem = "LAME";
        //                    break;
        //                // OGV
        //                case "ogv":
        //                    VM.AudioView.Audio_Codec_SelectedItem = "Vorbis";
        //                    break;

        //                // -------------------------
        //                // Audio Container
        //                // -------------------------
        //                // M4A
        //                case "m4a":
        //                    VM.AudioView.Audio_Codec_SelectedItem = "AAC";
        //                    break;
        //                // MP3
        //                case "mp3":
        //                    VM.AudioView.Audio_Codec_SelectedItem = "LAME";
        //                    break;
        //                // OGG
        //                case "ogg":
        //                    VM.AudioView.Audio_Codec_SelectedItem = "Opus";
        //                    break;
        //                // FLAC
        //                case "flac":
        //                    VM.AudioView.Audio_Codec_SelectedItem = "FLAC";
        //                    break;
        //                // WAV
        //                case "wav":
        //                    VM.AudioView.Audio_Codec_SelectedItem = "PCM";
        //                    break;

        //                // -------------------------
        //                // Image Container
        //                // -------------------------
        //                //case "jpg":
        //                //    VM.AudioView.Audio_Codec_SelectedItem = "None";
        //                //    break;

        //                //case "png":
        //                //    VM.AudioView.Audio_Codec_SelectedItem = "None";
        //                //    break;

        //                //case "webp":
        //                //    VM.AudioView.Audio_Codec_SelectedItem = "None";
        //                //    break;
        //            }
        //        }
        //    }
        //}

    }
}
