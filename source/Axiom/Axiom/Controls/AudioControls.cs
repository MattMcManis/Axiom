/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2019 Matt McManis
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
using System.Linq;
using System.Windows;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class AudioControls
    {
        /// <summary>
        /// Set Controls
        /// </summary>
        public static void SetControls(string codec_SelectedItem)
        {
            // --------------------------------------------------
            // Codec
            // --------------------------------------------------

            // -------------------------
            // Vorbis
            // -------------------------
            if (codec_SelectedItem == "Vorbis")
            {
                // Codec
                Vorbis.Codec_Set();

                // Items Source
                Vorbis.Controls_ItemsSource();

                // Selected Items
                Vorbis.Controls_Selected();

                // Checked
                Vorbis.Controls_Checked();

                // Unhecked
                Vorbis.Controls_Unhecked();

                // Enabled
                Vorbis.Controls_Enable();

                // Disabled
                Vorbis.Controls_Disable();
            }

            // -------------------------
            // Opus
            // -------------------------
            else if (codec_SelectedItem == "Opus")
            {
                // Codec
                Opus.Codec_Set();

                // Items Source
                Opus.Controls_ItemsSource();

                // Selected Items
                Opus.Controls_Selected();

                // Checked
                Opus.Controls_Checked();

                // Unhecked
                Opus.Controls_Unhecked();

                // Enabled
                Opus.Controls_Enable();

                // Disabled
                Opus.Controls_Disable();
            }

            // -------------------------
            // AC3
            // -------------------------
            else if (codec_SelectedItem == "AC3")
            {
                // Codec
                AC3.Codec_Set();

                // Items Source
                AC3.Controls_ItemsSource();

                // Selected Items
                AC3.Controls_Selected();

                // Checked
                AC3.Controls_Checked();

                // Unhecked
                AC3.Controls_Unhecked();

                // Enabled
                AC3.Controls_Enable();

                // Disabled
                AC3.Controls_Disable();
            }

            // -------------------------
            // AAC
            // -------------------------
            else if (codec_SelectedItem == "AAC")
            {
                // Codec
                AAC.Codec_Set();

                // Items Source
                AAC.Controls_ItemsSource();

                // Selected Items
                AAC.Controls_Selected();

                // Checked
                AAC.Controls_Checked();

                // Unhecked
                AAC.Controls_Unhecked();

                // Enabled
                AAC.Controls_Enable();

                // Disabled
                AAC.Controls_Disable();
            }

            // -------------------------
            // DTS
            // -------------------------
            else if (codec_SelectedItem == "DTS")
            {
                // Codec
                DTS.Codec_Set();

                // Items Source
                DTS.Controls_ItemsSource();

                // Selected Items
                DTS.Controls_Selected();

                // Checked
                DTS.Controls_Checked();

                // Unhecked
                DTS.Controls_Unhecked();

                // Enabled
                DTS.Controls_Enable();

                // Disabled
                DTS.Controls_Disable();
            }

            // -------------------------
            // MP2
            // -------------------------
            else if (codec_SelectedItem == "MP2")
            {
                // Codec
                MP2.Codec_Set();

                // Items Source
                MP2.Controls_ItemsSource();

                // Selected Items
                MP2.Controls_Selected();

                // Checked
                MP2.Controls_Checked();

                // Unhecked
                MP2.Controls_Unhecked();

                // Enabled
                MP2.Controls_Enable();

                // Disabled
                MP2.Controls_Disable();
            }

            // -------------------------
            // LAME
            // -------------------------
            else if (codec_SelectedItem == "LAME")
            {
                // Codec
                LAME.Codec_Set();

                // Items Source
                LAME.Controls_ItemsSource();

                // Selected Items
                LAME.Controls_Selected();

                // Checked
                LAME.Controls_Checked();

                // Unhecked
                LAME.Controls_Unhecked();

                // Enabled
                LAME.Controls_Enable();

                // Disabled
                LAME.Controls_Disable();
            }

            // -------------------------
            // ALAC
            // -------------------------
            else if (codec_SelectedItem == "ALAC")
            {
                // Codec
                ALAC.Codec_Set();

                // Items Source
                ALAC.Controls_ItemsSource();

                // Selected Items
                ALAC.Controls_Selected();

                // Checked
                ALAC.Controls_Checked();

                // Unhecked
                ALAC.Controls_Unhecked();

                // Enabled
                ALAC.Controls_Enable();

                // Disabled
                ALAC.Controls_Disable();
            }

            // -------------------------
            // FLAC
            // -------------------------
            else if (codec_SelectedItem == "FLAC")
            {
                // Codec
                FLAC.Codec_Set();

                // Items Source
                FLAC.Controls_ItemsSource();

                // Selected Items
                FLAC.Controls_Selected();

                // Checked
                FLAC.Controls_Checked();

                // Unhecked
                FLAC.Controls_Unhecked();

                // Enabled
                FLAC.Controls_Enable();

                // Disabled
                FLAC.Controls_Disable();
            }

            // -------------------------
            // PCM
            // -------------------------
            else if (codec_SelectedItem == "PCM")
            {
                // Codec
                PCM.Codec_Set();

                // Items Source
                PCM.Controls_ItemsSource();

                // Selected Items
                PCM.Controls_Selected();

                // Checked
                PCM.Controls_Checked();

                // Unhecked
                PCM.Controls_Unhecked();

                // Enabled
                PCM.Controls_Enable();

                // Disabled
                PCM.Controls_Disable();
            }

            // -------------------------
            // Copy
            // -------------------------
            else if (codec_SelectedItem == "Copy")
            {
                // Codec
                AudioCopy.Codec_Set();

                // Items Source
                AudioCopy.Controls_ItemsSource();

                // Selected Items
                AudioCopy.Controls_Selected();

                // Checked
                AudioCopy.Controls_Checked();

                // Unhecked
                AudioCopy.Controls_Unhecked();

                // Enabled
                AudioCopy.Controls_Enable();

                // Disabled
                AudioCopy.Controls_Disable();
            }

            // -------------------------
            // None
            // -------------------------
            else if (codec_SelectedItem == "None")
            {
                // Codec
                AudioNone.Codec_Set();

                // Items Source
                AudioNone.Controls_ItemsSource();

                // Selected Items
                AudioNone.Controls_Selected();

                // Checked
                AudioNone.Controls_Checked();

                // Unhecked
                AudioNone.Controls_Unhecked();

                // Enabled
                AudioNone.Controls_Enable();

                // Disabled
                AudioNone.Controls_Disable();
            }

            // --------------------------------------------------
            // Default Selected Item
            // --------------------------------------------------
            // -------------------------
            // Audio Quality Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(VM.AudioView.Audio_Quality_SelectedItem) &&
                VM.AudioView.Audio_Quality_SelectedItem != "None" &&
                VM.AudioView.Audio_Quality_SelectedItem != "none")
            {
                MainWindow.Audio_Quality_PreviousItem = VM.AudioView.Audio_Quality_SelectedItem;
            }

            VM.AudioView.Audio_Quality_SelectedItem = MainWindow.SelectedItem(VM.AudioView.Audio_Quality_Items.Select(c => c.Name).ToList(),
                                                                       MainWindow.Audio_Quality_PreviousItem
                                                                       );

            // -------------------------
            // Audio SampleRate Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(VM.AudioView.Audio_SampleRate_SelectedItem) &&
                VM.AudioView.Audio_SampleRate_SelectedItem != "None" &&
                VM.AudioView.Audio_SampleRate_SelectedItem != "none")
            {
                MainWindow.Audio_SampleRate_PreviousItem = VM.AudioView.Audio_SampleRate_SelectedItem;
            }

            VM.AudioView.Audio_SampleRate_SelectedItem = MainWindow.SelectedItem(VM.AudioView.Audio_SampleRate_Items.Select(c => c.Name).ToList(),
                                                                      MainWindow.Audio_SampleRate_PreviousItem
                                                                      );

            // -------------------------
            // Audio BitDepth Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(VM.AudioView.Audio_BitDepth_SelectedItem) &&
                VM.AudioView.Audio_BitDepth_SelectedItem != "None" &&
                VM.AudioView.Audio_BitDepth_SelectedItem != "none")
            {
                MainWindow.Audio_BitDepth_PreviousItem = VM.AudioView.Audio_BitDepth_SelectedItem;
            }

            VM.AudioView.Audio_BitDepth_SelectedItem = MainWindow.SelectedItem(VM.AudioView.Audio_BitDepth_Items.Select(c => c.Name).ToList(),
                                                                    MainWindow.Audio_BitDepth_PreviousItem
                                                                    );


        }


        /// <summary>
        /// Audio BitRate Display
        /// </summary>
        public static void AudioBitRateDisplay(
                                               List<AudioViewModel.AudioQuality> items,
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
                // BitRate CBR
                if (VM.AudioView.Audio_VBR_IsChecked == false)
                {
                    VM.AudioView.Audio_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.CBR;
                }

                // BitRate VBR
                else if (VM.AudioView.Audio_VBR_IsChecked == true)
                {
                    VM.AudioView.Audio_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.VBR;
                }
            }
        }


        /// <summary>
        /// Quality Controls
        /// <summary>
        public static void QualityControls()
        {
            // -------------------------
            // Enable
            // -------------------------
            // Only for Custom
            if (VM.AudioView.Audio_Quality_SelectedItem == "Custom")
            {
                // BitRate
                VM.AudioView.Audio_BitRate_IsEnabled = true;
                VM.AudioView.Audio_BitRate_Text = "";
            }

            // -------------------------
            // Disable
            // -------------------------
            // Only for Custom
            else if (VM.AudioView.Audio_Quality_SelectedItem == "Auto")
            {
                // BitRate
                VM.AudioView.Audio_BitRate_IsEnabled = false;
                VM.AudioView.Audio_BitRate_Text = "";
            }
            // All Other Qualities
            else
            {
                // BitRate
                VM.AudioView.Audio_BitRate_IsEnabled = false;
            }


            // -------------------------
            // Compression Level
            // -------------------------
            if (VM.AudioView.Audio_Codec_SelectedItem == "Opus")
            {
                // VBR
                // Enable
                if (VM.AudioView.Audio_VBR_IsChecked == true)
                {
                    VM.AudioView.Audio_CompressionLevel_IsEnabled = true;
                    VM.AudioView.Audio_CompressionLevel_SelectedItem = "10";
                }
                // CBR
                // Disable
                else
                {
                    VM.AudioView.Audio_CompressionLevel_IsEnabled = false;
                    VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                }
            }
        }


        /// <summary>
        /// Auto Copy Conditions Check
        /// <summary>
        public static bool AutoCopyConditionsCheck(
                                                   string inputExt,
                                                   string outputExt)
        {
            // Pass Check
            if (VM.AudioView.Audio_Quality_SelectedItem == "Auto" &&
                VM.AudioView.Audio_Channel_SelectedItem == "Source" &&
                VM.AudioView.Audio_SampleRate_SelectedItem == "auto" &&
                VM.AudioView.Audio_BitDepth_SelectedItem == "auto" &&
                VM.AudioView.Audio_HardLimiter_Value == 0.0 &&
                VM.AudioView.Audio_Volume_Text == "100" &&
                // Filters
                VM.FilterAudioView.FilterAudio_Lowpass_SelectedItem == "disabled" &&
                VM.FilterAudioView.FilterAudio_Highpass_SelectedItem == "disabled" &&
                VM.FilterAudioView.FilterAudio_Headphones_SelectedItem == "disabled" &&
                VM.FilterAudioView.FilterAudio_Contrast_Value == 0 &&
                VM.FilterAudioView.FilterAudio_ExtraStereo_Value == 0 &&
                VM.FilterAudioView.FilterAudio_Tempo_Value == 100 &&
                VM.FilterVideoView.FilterVideo_EQ_Gamma_Value == 0 &&
                // File Extension Match
                string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase)
                )
            {
                return true;
            }

            // Did Not Pass Check
            else
            {
                //MessageBox.Show("did not pass"); //debug
                return false;
            }
        }


        /// <summary>
        /// Copy Controls
        /// <summary>
        public static void CopyControls()
        {
            // -------------------------
            // Conditions Check
            // Enable
            // -------------------------
            if (AutoCopyConditionsCheck(/*main_vm,*/ MainWindow.inputExt, MainWindow.outputExt) == true)
            {
                // -------------------------
                // Set Audio Codec Combobox Selected Item to Copy
                // -------------------------
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
                // Null Check
                // -------------------------
                if (!string.IsNullOrEmpty(VM.AudioView.Audio_Quality_SelectedItem))
                {
                    // -------------------------
                    // Copy Selected
                    // -------------------------
                    if (VM.AudioView.Audio_Codec_SelectedItem == "Copy")
                    {
                        // -------------------------
                        // Switch back to format's default codec
                        // -------------------------
                        if (VM.AudioView.Audio_Codec_SelectedItem != "Auto" ||
                            !string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                            )
                        {
                            // -------------------------
                            // Video Container
                            // -------------------------
                            if (VM.FormatView.Format_Container_SelectedItem == "webm")
                            {
                                VM.AudioView.Audio_Codec_SelectedItem = "Vorbis";
                            }
                            else if (VM.FormatView.Format_Container_SelectedItem == "mp4")
                            {
                                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                            }
                            else if (VM.FormatView.Format_Container_SelectedItem == "mkv")
                            {
                                VM.AudioView.Audio_Codec_SelectedItem = "AC3";
                            }
                            else if (VM.FormatView.Format_Container_SelectedItem == "m2v")
                            {
                                VM.AudioView.Audio_Codec_SelectedItem = "None";
                            }
                            else if (VM.FormatView.Format_Container_SelectedItem == "mpg")
                            {
                                VM.AudioView.Audio_Codec_SelectedItem = "MP2";
                            }
                            else if (VM.FormatView.Format_Container_SelectedItem == "avi")
                            {
                                VM.AudioView.Audio_Codec_SelectedItem = "LAME";
                            }
                            else if (VM.FormatView.Format_Container_SelectedItem == "ogv")
                            {
                                VM.AudioView.Audio_Codec_SelectedItem = "Vorbis";
                            }

                            // -------------------------
                            // Audio Container
                            // -------------------------
                            if (VM.FormatView.Format_Container_SelectedItem == "m4a")
                            {
                                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                            }
                            else if (VM.FormatView.Format_Container_SelectedItem == "mp3")
                            {
                                VM.AudioView.Audio_Codec_SelectedItem = "LAME";
                            }
                            else if (VM.FormatView.Format_Container_SelectedItem == "ogg")
                            {
                                VM.AudioView.Audio_Codec_SelectedItem = "Opus";
                            }
                            else if (VM.FormatView.Format_Container_SelectedItem == "flac")
                            {
                                VM.AudioView.Audio_Codec_SelectedItem = "FLAC";
                            }
                            else if (VM.FormatView.Format_Container_SelectedItem == "wav")
                            {
                                VM.AudioView.Audio_Codec_SelectedItem = "PCM";
                            }

                            // -------------------------
                            // Image Container
                            // -------------------------
                            //if (VM.FormatView.Format_Container_SelectedItem == "jpg")
                            //{
                            //    VM.AudioView.Audio_Codec_SelectedItem = "None";
                            //}
                            //else if (VM.FormatView.Format_Container_SelectedItem == "png")
                            //{
                            //    VM.AudioView.Audio_Codec_SelectedItem = "None";
                            //}
                            //else if (VM.FormatView.Format_Container_SelectedItem == "webp")
                            //{
                            //    VM.AudioView.Audio_Codec_SelectedItem = "None";
                            //}
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Auto Codec Copy
        /// <summary>
        public static void AutoCopyAudioCodec()
        {
            // --------------------------------------------------
            // When Input Extension is Not Empty
            // --------------------------------------------------
            if (!string.IsNullOrEmpty(MainWindow.inputExt))
            {
                CopyControls();
            }

            // --------------------------------------------------
            // When Input Extension is Empty
            // --------------------------------------------------
            else if (string.IsNullOrEmpty(MainWindow.inputExt) &&
                VM.AudioView.Audio_Codec_SelectedItem == "Copy")
            {
                CopyControls();
            }
        }


    }
}
