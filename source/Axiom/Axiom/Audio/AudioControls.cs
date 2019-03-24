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
 * Bitrate Display
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
        ///     Set Controls
        /// </summary>
        public static void SetControls(ViewModel vm, string codec_SelectedItem)
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
                Vorbis.Codec_Set(vm);

                // Items Source
                Vorbis.Controls_ItemsSource(vm);

                // Selected Items
                Vorbis.Controls_Selected(vm);

                // Checked
                Vorbis.Controls_Checked(vm);

                // Unhecked
                Vorbis.Controls_Unhecked(vm);

                // Enabled
                Vorbis.Controls_Enable(vm);

                // Disabled
                Vorbis.Controls_Disable(vm);
            }

            // -------------------------
            // Opus
            // -------------------------
            else if (codec_SelectedItem == "Opus")
            {
                // Codec
                Opus.Codec_Set(vm);

                // Items Source
                Opus.Controls_ItemsSource(vm);

                // Selected Items
                Opus.Controls_Selected(vm);

                // Checked
                Opus.Controls_Checked(vm);

                // Unhecked
                Opus.Controls_Unhecked(vm);

                // Enabled
                Opus.Controls_Enable(vm);

                // Disabled
                Opus.Controls_Disable(vm);
            }

            // -------------------------
            // AC3
            // -------------------------
            else if (codec_SelectedItem == "AC3")
            {
                // Codec
                AC3.Codec_Set(vm);

                // Items Source
                AC3.Controls_ItemsSource(vm);

                // Selected Items
                AC3.Controls_Selected(vm);

                // Checked
                AC3.Controls_Checked(vm);

                // Unhecked
                AC3.Controls_Unhecked(vm);

                // Enabled
                AC3.Controls_Enable(vm);

                // Disabled
                AC3.Controls_Disable(vm);
            }

            // -------------------------
            // AAC
            // -------------------------
            else if (codec_SelectedItem == "AAC")
            {
                // Codec
                AAC.Codec_Set(vm);

                // Items Source
                AAC.Controls_ItemsSource(vm);

                // Selected Items
                AAC.Controls_Selected(vm);

                // Checked
                AAC.Controls_Checked(vm);

                // Unhecked
                AAC.Controls_Unhecked(vm);

                // Enabled
                AAC.Controls_Enable(vm);

                // Disabled
                AAC.Controls_Disable(vm);
            }

            // -------------------------
            // MP2
            // -------------------------
            else if (codec_SelectedItem == "MP2")
            {
                // Codec
                MP2.Codec_Set(vm);

                // Items Source
                MP2.Controls_ItemsSource(vm);

                // Selected Items
                MP2.Controls_Selected(vm);

                // Checked
                MP2.Controls_Checked(vm);

                // Unhecked
                MP2.Controls_Unhecked(vm);

                // Enabled
                MP2.Controls_Enable(vm);

                // Disabled
                MP2.Controls_Disable(vm);
            }

            // -------------------------
            // LAME
            // -------------------------
            else if (codec_SelectedItem == "LAME")
            {
                // Codec
                LAME.Codec_Set(vm);

                // Items Source
                LAME.Controls_ItemsSource(vm);

                // Selected Items
                LAME.Controls_Selected(vm);

                // Checked
                LAME.Controls_Checked(vm);

                // Unhecked
                LAME.Controls_Unhecked(vm);

                // Enabled
                LAME.Controls_Enable(vm);

                // Disabled
                LAME.Controls_Disable(vm);
            }

            // -------------------------
            // ALAC
            // -------------------------
            else if (codec_SelectedItem == "ALAC")
            {
                // Codec
                ALAC.Codec_Set(vm);

                // Items Source
                ALAC.Controls_ItemsSource(vm);

                // Selected Items
                ALAC.Controls_Selected(vm);

                // Checked
                ALAC.Controls_Checked(vm);

                // Unhecked
                ALAC.Controls_Unhecked(vm);

                // Enabled
                ALAC.Controls_Enable(vm);

                // Disabled
                ALAC.Controls_Disable(vm);
            }

            // -------------------------
            // FLAC
            // -------------------------
            else if (codec_SelectedItem == "FLAC")
            {
                // Codec
                FLAC.Codec_Set(vm);

                // Items Source
                FLAC.Controls_ItemsSource(vm);

                // Selected Items
                FLAC.Controls_Selected(vm);

                // Checked
                FLAC.Controls_Checked(vm);

                // Unhecked
                FLAC.Controls_Unhecked(vm);

                // Enabled
                FLAC.Controls_Enable(vm);

                // Disabled
                FLAC.Controls_Disable(vm);
            }

            // -------------------------
            // PCM
            // -------------------------
            else if (codec_SelectedItem == "PCM")
            {
                // Codec
                PCM.Codec_Set(vm);

                // Items Source
                PCM.Controls_ItemsSource(vm);

                // Selected Items
                PCM.Controls_Selected(vm);

                // Checked
                PCM.Controls_Checked(vm);

                // Unhecked
                PCM.Controls_Unhecked(vm);

                // Enabled
                PCM.Controls_Enable(vm);

                // Disabled
                PCM.Controls_Disable(vm);
            }

            // -------------------------
            // Copy
            // -------------------------
            else if (codec_SelectedItem == "Copy")
            {
                // Codec
                AudioCopy.Codec_Set(vm);

                // Items Source
                AudioCopy.Controls_ItemsSource(vm);

                // Selected Items
                AudioCopy.Controls_Selected(vm);

                // Checked
                AudioCopy.Controls_Checked(vm);

                // Unhecked
                AudioCopy.Controls_Unhecked(vm);

                // Enabled
                AudioCopy.Controls_Enable(vm);

                // Disabled
                AudioCopy.Controls_Disable(vm);
            }

            // -------------------------
            // None
            // -------------------------
            else if (codec_SelectedItem == "None")
            {
                // Codec
                AudioNone.Codec_Set(vm);

                // Items Source
                AudioNone.Controls_ItemsSource(vm);

                // Selected Items
                AudioNone.Controls_Selected(vm);

                // Checked
                AudioNone.Controls_Checked(vm);

                // Unhecked
                AudioNone.Controls_Unhecked(vm);

                // Enabled
                AudioNone.Controls_Enable(vm);

                // Disabled
                AudioNone.Controls_Disable(vm);
            }

            // --------------------------------------------------
            // Default Selected Item
            // --------------------------------------------------
            // -------------------------
            // Audio Quality Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(vm.Audio_Quality_SelectedItem) &&
                vm.Audio_Quality_SelectedItem != "None" &&
                vm.Audio_Quality_SelectedItem != "none")
            {
                MainWindow.Audio_Quality_PreviousItem = vm.Audio_Quality_SelectedItem;
            }

            vm.Audio_Quality_SelectedItem = MainWindow.SelectedItem(vm.Audio_Quality_Items.Select(c => c.Name).ToList(),
                                                                       MainWindow.Audio_Quality_PreviousItem
                                                                       );

            // -------------------------
            // Audio SampleRate Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(vm.Audio_SampleRate_SelectedItem) &&
                vm.Audio_SampleRate_SelectedItem != "None" &&
                vm.Audio_SampleRate_SelectedItem != "none")
            {
                MainWindow.Audio_SampleRate_PreviousItem = vm.Audio_SampleRate_SelectedItem;
            }

            vm.Audio_SampleRate_SelectedItem = MainWindow.SelectedItem(vm.Audio_SampleRate_Items.Select(c => c.Name).ToList(),
                                                                      MainWindow.Audio_SampleRate_PreviousItem
                                                                      );

            // -------------------------
            // Audio BitDepth Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(vm.Audio_BitDepth_SelectedItem) &&
                vm.Audio_BitDepth_SelectedItem != "None" &&
                vm.Audio_BitDepth_SelectedItem != "none")
            {
                MainWindow.Audio_BitDepth_PreviousItem = vm.Audio_BitDepth_SelectedItem;
            }

            vm.Audio_BitDepth_SelectedItem = MainWindow.SelectedItem(vm.Audio_BitDepth_Items.Select(c => c.Name).ToList(),
                                                                    MainWindow.Audio_BitDepth_PreviousItem
                                                                    );


        }


        /// <summary>
        ///    Audio Bitrate Display
        /// </summary>
        public static void AudioBitrateDisplay(ViewModel vm,
                                               List<ViewModel.AudioQuality> items,
                                               string selectedQuality
                                               )
        {
            // Condition Check
            if (!string.IsNullOrEmpty(vm.Audio_Quality_SelectedItem) &&
                vm.Audio_Quality_SelectedItem != "None" &&
                vm.Audio_Quality_SelectedItem != "Auto" &&
                vm.Audio_Quality_SelectedItem != "Lossless" &&
                vm.Audio_Quality_SelectedItem != "Custom" &&
                vm.Audio_Quality_SelectedItem != "Mute")
            {
                // -------------------------
                // Display in TextBox
                // -------------------------
                // Bitrate CBR
                if (vm.Audio_VBR_IsChecked == false)
                {
                    vm.Audio_Bitrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.CBR;
                }

                // Bitrate VBR
                else if (vm.Audio_VBR_IsChecked == true)
                {
                    vm.Audio_Bitrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.VBR;
                }
            }
        }


        /// <summary>
        ///     Quality Controls
        /// <summary>
        public static void QualityControls(ViewModel vm)
        {
            // -------------------------
            // Enable
            // -------------------------
            // Only for Custom
            if (vm.Audio_Quality_SelectedItem == "Custom")
            {
                // Bitrate
                vm.Audio_Bitrate_IsEnabled = true;
                vm.Audio_Bitrate_Text = "";
            }

            // -------------------------
            // Disable
            // -------------------------
            // Only for Custom
            else if (vm.Audio_Quality_SelectedItem == "Auto")
            {
                // Bitrate
                vm.Audio_Bitrate_IsEnabled = false;
                vm.Audio_Bitrate_Text = "";
            }
            // All Other Qualities
            else
            {
                // Bitrate
                vm.Audio_Bitrate_IsEnabled = false;
            }
        }


        /// <summary>
        ///    Auto Copy Conditions Check
        /// <summary>
        public static bool AutoCopyConditionsCheck(ViewModel vm,
                                                   string inputExt,
                                                   string outputExt)
        {
            // Pass Check
            if (vm.Audio_Quality_SelectedItem == "Auto" &&
                vm.Audio_Channel_SelectedItem == "Source" &&
                vm.Audio_SampleRate_SelectedItem == "auto" &&
                vm.Audio_BitDepth_SelectedItem == "auto" &&
                vm.Audio_HardLimiter_Value == 1 &&
                vm.Audio_Volume_Text == "100" &&
                // Filters
                vm.FilterAudio_Lowpass_SelectedItem == "disabled" &&
                vm.FilterAudio_Highpass_SelectedItem == "disabled" &&
                vm.FilterAudio_Headphones_SelectedItem == "disabled" &&
                vm.FilterAudio_Contrast_Value == 0 &&
                vm.FilterAudio_ExtraStereo_Value == 0 &&
                vm.FilterAudio_Tempo_Value == 100 &&
                vm.FilterVideo_EQ_Gamma_Value == 0 &&
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
        ///    Copy Controls
        /// <summary>
        public static void CopyControls(ViewModel vm)
        {
            // -------------------------
            // Conditions Check
            // Enable
            // -------------------------
            if (AutoCopyConditionsCheck(vm, MainWindow.inputExt, MainWindow.outputExt) == true)
            {
                // -------------------------
                // Set Audio Codec Combobox Selected Item to Copy
                // -------------------------
                if (vm.Audio_Codec_Items.Count > 0)
                {
                    if (vm.Audio_Codec_Items?.Contains("Copy") == true)
                    {
                        vm.Audio_Codec_SelectedItem = "Copy";
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
                if (!string.IsNullOrEmpty(vm.Audio_Quality_SelectedItem))
                {
                    // -------------------------
                    // Copy Selected
                    // -------------------------
                    if (vm.Audio_Codec_SelectedItem == "Copy")
                    {
                        // -------------------------
                        // Switch back to format's default codec
                        // -------------------------
                        if (vm.Audio_Codec_SelectedItem != "Auto" ||
                            !string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                            )
                        {
                            // -------------------------
                            // Video Container
                            // -------------------------
                            if (vm.Format_Container_SelectedItem == "webm")
                            {
                                vm.Audio_Codec_SelectedItem = "Vorbis";
                            }
                            else if (vm.Format_Container_SelectedItem == "mp4")
                            {
                                vm.Audio_Codec_SelectedItem = "AAC";
                            }
                            else if (vm.Format_Container_SelectedItem == "mkv")
                            {
                                vm.Audio_Codec_SelectedItem = "AC3";
                            }
                            else if (vm.Format_Container_SelectedItem == "m2v")
                            {
                                vm.Audio_Codec_SelectedItem = "None";
                            }
                            else if (vm.Format_Container_SelectedItem == "mpg")
                            {
                                vm.Audio_Codec_SelectedItem = "MP2";
                            }
                            else if (vm.Format_Container_SelectedItem == "avi")
                            {
                                vm.Audio_Codec_SelectedItem = "LAME";
                            }
                            else if (vm.Format_Container_SelectedItem == "ogv")
                            {
                                vm.Audio_Codec_SelectedItem = "Vorbis";
                            }

                            // -------------------------
                            // Audio Container
                            // -------------------------
                            if (vm.Format_Container_SelectedItem == "m4a")
                            {
                                vm.Audio_Codec_SelectedItem = "AAC";
                            }
                            else if (vm.Format_Container_SelectedItem == "mp3")
                            {
                                vm.Audio_Codec_SelectedItem = "LAME";
                            }
                            else if (vm.Format_Container_SelectedItem == "ogg")
                            {
                                vm.Audio_Codec_SelectedItem = "Opus";
                            }
                            else if (vm.Format_Container_SelectedItem == "flac")
                            {
                                vm.Audio_Codec_SelectedItem = "FLAC";
                            }
                            else if (vm.Format_Container_SelectedItem == "wav")
                            {
                                vm.Audio_Codec_SelectedItem = "PCM";
                            }

                            // -------------------------
                            // Image Container
                            // -------------------------
                            //if (vm.Format_Container_SelectedItem == "jpg")
                            //{
                            //    vm.Audio_Codec_SelectedItem = "None";
                            //}
                            //else if (vm.Format_Container_SelectedItem == "png")
                            //{
                            //    vm.Audio_Codec_SelectedItem = "None";
                            //}
                            //else if (vm.Format_Container_SelectedItem == "webp")
                            //{
                            //    vm.Audio_Codec_SelectedItem = "None";
                            //}
                        }
                    }
                }
            }
        }


        /// <summary>
        ///    Auto Codec Copy
        /// <summary>
        public static void AutoCopyAudioCodec(ViewModel vm)
        {
            // --------------------------------------------------
            // When Input Extension is Not Empty
            // --------------------------------------------------
            if (!string.IsNullOrEmpty(MainWindow.inputExt))
            {
                CopyControls(vm);
            }

            // --------------------------------------------------
            // When Input Extension is Empty
            // --------------------------------------------------
            else if (string.IsNullOrEmpty(MainWindow.inputExt) &&
                vm.Audio_Codec_SelectedItem == "Copy")
            {
                CopyControls(vm);
            }
        }


    }
}
