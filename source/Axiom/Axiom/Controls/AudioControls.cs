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
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Controls
{
    namespace Audio
    {
        public class Controls
        {
            /// <summary>
            /// Set Controls
            /// </summary>
            public static void SetControls(string codec_SelectedItem)
            {
                // --------------------------------------------------
                // Codec
                // --------------------------------------------------

                switch (codec_SelectedItem)
                {
                    // -------------------------
                    // Vorbis
                    // -------------------------
                    case "Vorbis":
                        // Codec
                        Codec.Vorbis.Codec_Set();

                        // Items Source
                        Codec.Vorbis.Controls_ItemsSource();
                        // Selected Items
                        Codec.Vorbis.Controls_Selected();

                        // Checked
                        Codec.Vorbis.Controls_Checked();
                        // Unhecked
                        Codec.Vorbis.Controls_Unhecked();

                        // Enabled
                        Codec.Vorbis.Controls_Enable();
                        // Disabled
                        Codec.Vorbis.Controls_Disable();
                        break;

                    // -------------------------
                    // Opus
                    // -------------------------
                    case "Opus":
                        // Codec
                        Codec.Opus.Codec_Set();

                        // Items Source
                        Codec.Opus.Controls_ItemsSource();
                        // Selected Items
                        Codec.Opus.Controls_Selected();

                        // Checked
                        Codec.Opus.Controls_Checked();
                        // Unhecked
                        Codec.Opus.Controls_Unhecked();

                        // Enabled
                        Codec.Opus.Controls_Enable();
                        // Disabled
                        Codec.Opus.Controls_Disable();
                        break;

                    // -------------------------
                    // AC3
                    // -------------------------
                    case "AC3":
                        // Codec
                        Codec.AC3.Codec_Set();

                        // Items Source
                        Codec.AC3.Controls_ItemsSource();
                        // Selected Items
                        Codec.AC3.Controls_Selected();

                        // Checked
                        Codec.AC3.Controls_Checked();
                        // Unhecked
                        Codec.AC3.Controls_Unhecked();

                        // Enabled
                        Codec.AC3.Controls_Enable();
                        // Disabled
                        Codec.AC3.Controls_Disable();
                        break;

                    // -------------------------
                    // AAC
                    // -------------------------
                    case "AAC":
                        // Codec
                        Codec.AAC.Codec_Set();

                        // Items Source
                        Codec.AAC.Controls_ItemsSource();
                        // Selected Items
                        Codec.AAC.Controls_Selected();

                        // Checked
                        Codec.AAC.Controls_Checked();
                        // Unhecked
                        Codec.AAC.Controls_Unhecked();

                        // Enabled
                        Codec.AAC.Controls_Enable();
                        // Disabled
                        Codec.AAC.Controls_Disable();
                        break;

                    // -------------------------
                    // DTS
                    // -------------------------
                    case "DTS":
                        // Codec
                        Codec.DTS.Codec_Set();

                        // Items Source
                        Codec.DTS.Controls_ItemsSource();
                        // Selected Items
                        Codec.DTS.Controls_Selected();

                        // Checked
                        Codec.DTS.Controls_Checked();
                        // Unhecked
                        Codec.DTS.Controls_Unhecked();

                        // Enabled
                        Codec.DTS.Controls_Enable();
                        // Disabled
                        Codec.DTS.Controls_Disable();
                        break;

                    // -------------------------
                    // MP2
                    // -------------------------
                    case "MP2":
                        // Codec
                        Codec.MP2.Codec_Set();

                        // Items Source
                        Codec.MP2.Controls_ItemsSource();
                        // Selected Items
                        Codec.MP2.Controls_Selected();

                        // Checked
                        Codec.MP2.Controls_Checked();
                        // Unhecked
                        Codec.MP2.Controls_Unhecked();

                        // Enabled
                        Codec.MP2.Controls_Enable();
                        // Disabled
                        Codec.MP2.Controls_Disable();
                        break;

                    // -------------------------
                    // LAME
                    // -------------------------
                    case "LAME":
                        // Codec
                        Codec.LAME.Codec_Set();

                        // Items Source
                        Codec.LAME.Controls_ItemsSource();
                        // Selected Items
                        Codec.LAME.Controls_Selected();

                        // Checked
                        Codec.LAME.Controls_Checked();
                        // Unhecked
                        Codec.LAME.Controls_Unhecked();

                        // Enabled
                        Codec.LAME.Controls_Enable();
                        // Disabled
                        Codec.LAME.Controls_Disable();
                        break;

                    // -------------------------
                    // ALAC
                    // -------------------------
                    case "ALAC":
                        // Codec
                        Codec.ALAC.Codec_Set();

                        // Items Source
                        Codec.ALAC.Controls_ItemsSource();
                        // Selected Items
                        Codec.ALAC.Controls_Selected();

                        // Checked
                        Codec.ALAC.Controls_Checked();
                        // Unhecked
                        Codec.ALAC.Controls_Unhecked();

                        // Enabled
                        Codec.ALAC.Controls_Enable();
                        // Disabled
                        Codec.ALAC.Controls_Disable();
                        break;

                    // -------------------------
                    // FLAC
                    // -------------------------
                    case "FLAC":
                        // Codec
                        Codec.FLAC.Codec_Set();

                        // Items Source
                        Codec.FLAC.Controls_ItemsSource();
                        // Selected Items
                        Codec.FLAC.Controls_Selected();

                        // Checked
                        Codec.FLAC.Controls_Checked();
                        // Unhecked
                        Codec.FLAC.Controls_Unhecked();

                        // Enabled
                        Codec.FLAC.Controls_Enable();
                        // Disabled
                        Codec.FLAC.Controls_Disable();
                        break;

                    // -------------------------
                    // PCM
                    // -------------------------
                    case "PCM":
                        // Codec
                        Codec.PCM.Codec_Set();

                        // Items Source
                        Codec.PCM.Controls_ItemsSource();
                        // Selected Items
                        Codec.PCM.Controls_Selected();

                        // Checked
                        Codec.PCM.Controls_Checked();
                        // Unhecked
                        Codec.PCM.Controls_Unhecked();

                        // Enabled
                        Codec.PCM.Controls_Enable();
                        // Disabled
                        Codec.PCM.Controls_Disable();
                        break;

                    // -------------------------
                    // Copy
                    // -------------------------
                    case "Copy":
                        // Codec
                        Codec.Copy.Codec_Set();

                        // Items Source
                        Codec.Copy.Controls_ItemsSource();
                        // Selected Items
                        Codec.Copy.Controls_Selected();

                        // Checked
                        Codec.Copy.Controls_Checked();
                        // Unhecked
                        Codec.Copy.Controls_Unhecked();

                        // Enabled
                        Codec.Copy.Controls_Enable();
                        // Disabled
                        Codec.Copy.Controls_Disable();
                        break;

                    // -------------------------
                    // None
                    // -------------------------
                    case "None":
                        // Codec
                        Codec.None.Codec_Set();

                        // Items Source
                        Codec.None.Controls_ItemsSource();
                        // Selected Items
                        Codec.None.Controls_Selected();

                        // Checked
                        Codec.None.Controls_Checked();
                        // Unhecked
                        Codec.None.Controls_Unhecked();

                        // Enabled
                        Codec.None.Controls_Enable();
                        // Disabled
                        Codec.None.Controls_Disable();
                        break;

                }

                // --------------------------------------------------
                // Default Selected Item
                // --------------------------------------------------

                //// -------------------------
                //// Audio Quality Selected Item
                //// -------------------------
                //// Save the Previous Codec's Item
                //if (!string.IsNullOrWhiteSpace(VM.AudioView.Audio_Quality_SelectedItem) &&
                //    VM.AudioView.Audio_Quality_SelectedItem.ToLower() != "auto" && // Auto / auto
                //    VM.AudioView.Audio_Quality_SelectedItem.ToLower() != "none") // None / none
                //{
                //    MainWindow.Audio_Quality_PreviousItem = VM.AudioView.Audio_Quality_SelectedItem;
                //}

                //// Select the Prevoius Codec's Item if available
                //// If missing Select Default to First Item
                //// Ignore Codec Copy
                //if (VM.AudioView.Audio_Codec_SelectedItem != "Copy")
                //{
                //    VM.AudioView.Audio_Quality_SelectedItem = MainWindow.SelectedItem(VM.AudioView.Audio_Quality_Items.Select(c => c.Name).ToList(),
                //                                                                      MainWindow.Audio_Quality_PreviousItem
                //                                                                     );
                //}

                //// -------------------------
                //// Audio SampleRate Selected Item
                //// -------------------------
                //// Save the Previous Codec's Item
                //if (!string.IsNullOrWhiteSpace(VM.AudioView.Audio_SampleRate_SelectedItem) &&
                //    VM.AudioView.Audio_SampleRate_SelectedItem.ToLower() != "auto" && // Auto / auto
                //    VM.AudioView.Audio_SampleRate_SelectedItem.ToLower() != "none") // None / none
                //{
                //    MainWindow.Audio_SampleRate_PreviousItem = VM.AudioView.Audio_SampleRate_SelectedItem;
                //}

                //// Select the Prevoius Codec's Item if available
                //// If missing Select Default to First Item
                //// Ignore Codec Copy
                ////if (VM.AudioView.Audio_Codec_SelectedItem != "Copy")
                ////{
                //    VM.AudioView.Audio_SampleRate_SelectedItem = MainWindow.SelectedItem(VM.AudioView.Audio_SampleRate_Items.Select(c => c.Name).ToList(),
                //                                                                     MainWindow.Audio_SampleRate_PreviousItem
                //                                                                     );
                ////}

                //// -------------------------
                //// Audio BitDepth Selected Item
                //// -------------------------
                //// Save the Previous Codec's Item
                //if (!string.IsNullOrWhiteSpace(VM.AudioView.Audio_BitDepth_SelectedItem) &&
                //    VM.AudioView.Audio_BitDepth_SelectedItem.ToLower() != "auto" && // Auto / auto
                //    VM.AudioView.Audio_BitDepth_SelectedItem.ToLower() != "none") // None / none
                //{
                //    MainWindow.Audio_BitDepth_PreviousItem = VM.AudioView.Audio_BitDepth_SelectedItem;
                //}

                //// Select the Prevoius Codec's Item if available
                //// If missing Select Default to First Item
                //// Ignore Codec Copy
                ////if (VM.AudioView.Audio_Codec_SelectedItem != "Copy")
                ////{
                //    VM.AudioView.Audio_BitDepth_SelectedItem = MainWindow.SelectedItem(VM.AudioView.Audio_BitDepth_Items.Select(c => c.Name).ToList(),
                //                                                                   MainWindow.Audio_BitDepth_PreviousItem
                //                                                                  );
                ////}
            }


            /// <summary>
            /// Audio BitRate Display
            /// </summary>
            public static void AudioBitRateDisplay(List<ViewModel.Audio.AudioQuality> items,
                                                   string selectedQuality
                                                   )
            {
                // Condition Check
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


                // -------------------------
                // Compression Level
                // -------------------------
                if (VM.AudioView.Audio_Codec_SelectedItem == "Opus")
                {
                    switch (VM.AudioView.Audio_VBR_IsChecked)
                    {
                        // VBR
                        // Enable
                        case true:
                            VM.AudioView.Audio_CompressionLevel_IsEnabled = true;
                            VM.AudioView.Audio_CompressionLevel_SelectedItem = "10";
                            break;

                        // CBR
                        // Disable
                        case false:
                            VM.AudioView.Audio_CompressionLevel_IsEnabled = false;
                            VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                            break;
                    }
                }
            }


            /// <summary>
            /// Auto Copy Conditions Check
            /// <summary>
            public static bool AutoCopyConditionsCheck()
            {
                // Failed
                if (VM.AudioView.Audio_Quality_SelectedItem != "Auto" ||
                    VM.AudioView.Audio_Channel_SelectedItem != "Source" ||
                    //VM.AudioView.Audio_SampleRate_SelectedItem != "auto" //||
                    VM.AudioView.Audio_BitDepth_SelectedItem != "auto" ||
                    VM.AudioView.Audio_HardLimiter_Value != 0.0 ||
                    VM.AudioView.Audio_Volume_Text != "100" ||
                    // Filters
                    VM.FilterAudioView.FilterAudio_Lowpass_SelectedItem != "disabled" ||
                    VM.FilterAudioView.FilterAudio_Highpass_SelectedItem != "disabled" ||
                    VM.FilterAudioView.FilterAudio_Headphones_SelectedItem != "disabled" ||
                    VM.FilterAudioView.FilterAudio_Contrast_Value != 0 ||
                    VM.FilterAudioView.FilterAudio_ExtraStereo_Value != 0 ||
                    VM.FilterAudioView.FilterAudio_Tempo_Value != 100
                    )
                {
                    //MessageBox.Show("false");
                    return false;
                }

                // Passed
                else
                {
                    //MessageBox.Show("true");
                    return true;
                }
            }


            /// <summary>
            /// Auto Copy Audio Codec
            /// <summary>
            /// <remarks>
            /// Input Extension is same as Output Extension and Audio Quality is Auto
            /// </remarks>
            public static void AutoCopyAudioCodec(string trigger)
            {
                //string audio_Quality_SelectedItem = VM.AudioView.Audio_Quality_Items.FirstOrDefault(item => item.Name == VM.AudioView.Audio_Quality_SelectedItem)?.Name;

                //MessageBox.Show(VM.AudioView.Audio_Quality_SelectedItem);

                // -------------------------
                // Halt if Selected Codec is Null
                // -------------------------
                if (string.IsNullOrWhiteSpace(VM.AudioView.Audio_Codec_SelectedItem))
                {
                    return;
                }

                // -------------------------
                // Halt if trigger is control
                // Pass if trigger is input
                // -------------------------
                if (trigger == "control" &&
                    VM.AudioView.Audio_Codec_SelectedItem != "Copy" &&
                    AutoCopyConditionsCheck() == true)
                {
                    return;
                }

                // -------------------------
                // Halt if Web URL
                // -------------------------
                if (MainWindow.IsWebURL(VM.MainView.Input_Text) == true)
                {
                    return;
                }

                // -------------------------
                // Get Input Extensions
                // -------------------------
                string inputExt = Path.GetExtension(VM.MainView.Input_Text);

                // -------------------------
                // Halt if Input Extension is Empty
                // -------------------------
                if (string.IsNullOrWhiteSpace(inputExt))
                {
                    return;
                }

                // -------------------------
                // Get Output Extensions
                // -------------------------
                string outputExt = "." + VM.FormatView.Format_Container_SelectedItem;

                // -------------------------
                // Conditions Check
                // Enable
                // -------------------------
                if (AutoCopyConditionsCheck() == true &&
                    string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
                {
                    // Set Audio Codec Combobox Selected Item to Copy
                    if (VM.AudioView.Audio_Codec_Items.Count > 0)
                    {
                        if (VM.AudioView.Audio_Codec_Items?.Contains("Copy") == true)
                        {
                            VM.AudioView.Audio_Codec_SelectedItem = "Copy";
                        }
                    }
                }

                // -------------------------
                // Reset to Default Codec
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
                else
                {
                    // -------------------------
                    // Copy Selected
                    // -------------------------
                    if (VM.AudioView.Audio_Codec_SelectedItem == "Copy")
                    {
                        switch (VM.FormatView.Format_Container_SelectedItem)
                        {
                            // -------------------------
                            // Video Container
                            // -------------------------
                            // WebM
                            case "webm":
                                VM.AudioView.Audio_Codec_SelectedItem = "Vorbis";
                                break;
                            // MP4
                            case "mp4":
                                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                                break;
                            // MKV
                            case "mkv":
                                VM.AudioView.Audio_Codec_SelectedItem = "AC3";
                                break;
                            // M2V
                            case "m2v":
                                VM.AudioView.Audio_Codec_SelectedItem = "None";
                                break;
                            // MPG
                            case "mpg":
                                VM.AudioView.Audio_Codec_SelectedItem = "MP2";
                                break;
                            // AVI
                            case "avi":
                                VM.AudioView.Audio_Codec_SelectedItem = "LAME";
                                break;
                            // OGV
                            case "ogv":
                                VM.AudioView.Audio_Codec_SelectedItem = "Vorbis";
                                break;

                            // -------------------------
                            // Audio Container
                            // -------------------------
                            // M4A
                            case "m4a":
                                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                                break;
                            // MP3
                            case "mp3":
                                VM.AudioView.Audio_Codec_SelectedItem = "LAME";
                                break;
                            // OGG
                            case "ogg":
                                VM.AudioView.Audio_Codec_SelectedItem = "Opus";
                                break;
                            // FLAC
                            case "flac":
                                VM.AudioView.Audio_Codec_SelectedItem = "FLAC";
                                break;
                            // WAV
                            case "wav":
                                VM.AudioView.Audio_Codec_SelectedItem = "PCM";
                                break;

                                // -------------------------
                                // Image Container
                                // -------------------------
                                //case "jpg":
                                //    VM.AudioView.Audio_Codec_SelectedItem = "None";
                                //    break;

                                //case "png":
                                //    VM.AudioView.Audio_Codec_SelectedItem = "None";
                                //    break;

                                //case "webp":
                                //    VM.AudioView.Audio_Codec_SelectedItem = "None";
                                //    break;
                        }
                    }
                }
            }

        }
    }
}
