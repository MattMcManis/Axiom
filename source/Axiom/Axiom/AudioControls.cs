/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2019 Matt McManis
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
        public static void SetControls(ViewModel vm, string selectedCodec)
        {
            // --------------------------------------------------
            // Codec
            // --------------------------------------------------

            // -------------------------
            // Vorbis
            // -------------------------
            if (selectedCodec == "Vorbis")
            {
                // Audio Codec
                vm.AudioCodec_Command = Vorbis.codec;

                // Item Source
                Vorbis.controlsItemSource(vm);

                // Selected Items
                Vorbis.controlsSelected(vm);

                // Checked
                Vorbis.controlsChecked(vm);

                // Unhecked
                Vorbis.controlsUnhecked(vm);

                // Enabled
                Vorbis.controlsEnable(vm);

                // Disabled
                Vorbis.controlsDisable(vm);
            }

            // -------------------------
            // Opus
            // -------------------------
            else if (selectedCodec == "Opus")
            {
                // Audio Codec
                vm.AudioCodec_Command = Opus.codec;

                // Item Source
                Opus.controlsItemSource(vm);

                // Selected Items
                Opus.controlsSelected(vm);

                // Checked
                Opus.controlsChecked(vm);

                // Unhecked
                Opus.controlsUnhecked(vm);

                // Enabled
                Opus.controlsEnable(vm);

                // Disabled
                Opus.controlsDisable(vm);
            }

            // -------------------------
            // AC3
            // -------------------------
            else if (selectedCodec == "AC3")
            {
                // Audio Codec
                vm.AudioCodec_Command = AC3.codec;

                // Item Source
                AC3.controlsItemSource(vm);

                // Selected Items
                AC3.controlsSelected(vm);

                // Checked
                AC3.controlsChecked(vm);

                // Unhecked
                AC3.controlsUnhecked(vm);

                // Enabled
                AC3.controlsEnable(vm);

                // Disabled
                AC3.controlsDisable(vm);
            }

            // -------------------------
            // AAC
            // -------------------------
            else if (selectedCodec == "AAC")
            {
                // Audio Codec
                vm.AudioCodec_Command = AAC.codec;

                // Item Source
                AAC.controlsItemSource(vm);

                // Selected Items
                AAC.controlsSelected(vm);

                // Checked
                AAC.controlsChecked(vm);

                // Unhecked
                AAC.controlsUnhecked(vm);

                // Enabled
                AAC.controlsEnable(vm);

                // Disabled
                AAC.controlsDisable(vm);
            }

            // -------------------------
            // ALAC
            // -------------------------
            else if (selectedCodec == "ALAC")
            {
                // Audio Codec
                vm.AudioCodec_Command = ALAC.codec;

                // Item Source
                ALAC.controlsItemSource(vm);

                // Selected Items
                ALAC.controlsSelected(vm);

                // Checked
                ALAC.controlsChecked(vm);

                // Unhecked
                ALAC.controlsUnhecked(vm);

                // Enabled
                ALAC.controlsEnable(vm);

                // Disabled
                ALAC.controlsDisable(vm);
            }

            // -------------------------
            // FLAC
            // -------------------------
            else if (selectedCodec == "FLAC")
            {
                // Audio Codec
                vm.AudioCodec_Command = FLAC.codec;

                // Item Source
                FLAC.controlsItemSource(vm);

                // Selected Items
                FLAC.controlsSelected(vm);

                // Checked
                FLAC.controlsChecked(vm);

                // Unhecked
                FLAC.controlsUnhecked(vm);

                // Enabled
                FLAC.controlsEnable(vm);

                // Disabled
                FLAC.controlsDisable(vm);
            }

            // -------------------------
            // PCM
            // -------------------------
            else if (selectedCodec == "PCM")
            {
                // Audio Codec
                vm.AudioCodec_Command = PCM.codec;

                // Item Source
                PCM.controlsItemSource(vm);

                // Selected Items
                PCM.controlsSelected(vm);

                // Checked
                PCM.controlsChecked(vm);

                // Unhecked
                PCM.controlsUnhecked(vm);

                // Enabled
                PCM.controlsEnable(vm);

                // Disabled
                PCM.controlsDisable(vm);
            }

            // -------------------------
            // LAME
            // -------------------------
            else if (selectedCodec == "LAME")
            {
                // Audio Codec
                vm.AudioCodec_Command = LAME.codec;

                // Item Source
                LAME.controlsItemSource(vm);

                // Selected Items
                LAME.controlsSelected(vm);

                // Checked
                LAME.controlsChecked(vm);

                // Unhecked
                LAME.controlsUnhecked(vm);

                // Enabled
                LAME.controlsEnable(vm);

                // Disabled
                LAME.controlsDisable(vm);
            }

            // -------------------------
            // Copy
            // -------------------------
            else if (selectedCodec == "Copy")
            {
                // Audio Codec
                vm.AudioCodec_Command = AudioCopy.codec;

                // Item Source
                AudioCopy.controlsItemSource(vm);

                // Selected Items
                AudioCopy.controlsSelected(vm);

                // Checked
                AudioCopy.controlsChecked(vm);

                // Unhecked
                AudioCopy.controlsUnhecked(vm);

                // Enabled
                AudioCopy.controlsEnable(vm);

                // Disabled
                AudioCopy.controlsDisable(vm);
            }

            // -------------------------
            // None
            // -------------------------
            else if (selectedCodec == "None")
            {
                // Audio Codec
                vm.AudioCodec_Command = AudioNone.codec;

                // Item Source
                AudioNone.controlsItemSource(vm);

                // Selected Items
                AudioNone.controlsSelected(vm);

                // Checked
                AudioNone.controlsChecked(vm);

                // Unhecked
                AudioNone.controlsUnhecked(vm);

                // Enabled
                AudioNone.controlsEnable(vm);

                // Disabled
                AudioNone.controlsDisable(vm);
            }

            // --------------------------------------------------
            // Default Selected Item
            // --------------------------------------------------
            // -------------------------
            // Audio Quality Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(vm.AudioQuality_SelectedItem) &&
                vm.AudioQuality_SelectedItem != "None" &&
                vm.AudioQuality_SelectedItem != "none")
            {
                MainWindow.AudioQuality_PreviousItem = vm.AudioQuality_SelectedItem;
            }

            vm.AudioQuality_SelectedItem = MainWindow.SelectedItem(vm.AudioQuality_Items.Select(c => c.Name).ToList(),
                                                                       MainWindow.AudioQuality_PreviousItem
                                                                       );

            // -------------------------
            // Audio SampleRate Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(vm.AudioSampleRate_SelectedItem) &&
                vm.AudioSampleRate_SelectedItem != "None" &&
                vm.AudioSampleRate_SelectedItem != "none")
            {
                MainWindow.AudioSampleRate_PreviousItem = vm.AudioSampleRate_SelectedItem;
            }

            vm.AudioSampleRate_SelectedItem = MainWindow.SelectedItem(vm.AudioSampleRate_Items.Select(c => c.Name).ToList(),
                                                                      MainWindow.AudioSampleRate_PreviousItem
                                                                      );

            // -------------------------
            // Audio BitDepth Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(vm.AudioBitDepth_SelectedItem) &&
                vm.AudioBitDepth_SelectedItem != "None" &&
                vm.AudioBitDepth_SelectedItem != "none")
            {
                MainWindow.AudioBitDepth_PreviousItem = vm.AudioBitDepth_SelectedItem;
            }

            vm.AudioBitDepth_SelectedItem = MainWindow.SelectedItem(vm.AudioBitDepth_Items.Select(c => c.Name).ToList(),
                                                                    MainWindow.AudioBitDepth_PreviousItem
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
            if (!string.IsNullOrEmpty(vm.AudioQuality_SelectedItem) &&
                vm.AudioQuality_SelectedItem != "None" &&
                vm.AudioQuality_SelectedItem != "Auto" &&
                vm.AudioQuality_SelectedItem != "Lossless" &&
                vm.AudioQuality_SelectedItem != "Custom" &&
                vm.AudioQuality_SelectedItem != "Mute")
            {
                // -------------------------
                // Display in TextBox
                // -------------------------
                //MessageBox.Show(vm.AudioQuality_SelectedItem);
                //MessageBox.Show(vm.AudioVBR_IsChecked.ToString());
                //vm.AudioBitrate_Text = "test";
                // Bitrate CBR
                if (vm.AudioVBR_IsChecked == false)
                {
                    vm.AudioBitrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.CBR;
                }

                // Bitrate VBR
                else if (vm.AudioVBR_IsChecked == true)
                {
                    vm.AudioBitrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.VBR;
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
            if (vm.AudioQuality_SelectedItem == "Custom")
            {
                // Bitrate
                vm.AudioBitrate_IsEnabled = true;
                vm.AudioBitrate_Text = "";
            }

            // -------------------------
            // Disable
            // -------------------------
            // Only for Custom
            else if (vm.AudioQuality_SelectedItem == "Auto")
            {
                // Bitrate
                vm.AudioBitrate_IsEnabled = false;
                vm.AudioBitrate_Text = "";
            }
            // All Other Qualities
            else
            {
                // Bitrate
                vm.AudioBitrate_IsEnabled = false;
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
            if (vm.AudioQuality_SelectedItem == "Auto" &&
                vm.AudioChannel_SelectedItem == "Source" &&
                vm.AudioSampleRate_SelectedItem == "auto" &&
                vm.AudioBitDepth_SelectedItem == "auto" &&
                vm.AudioHardLimiter_Value == 1 &&
                vm.Volume_Text == "100" &&
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
                if (vm.AudioCodec_Items.Count > 0)
                {
                    if (vm.AudioCodec_Items?.Contains("Copy") == true)
                    {
                        vm.AudioCodec_SelectedItem = "Copy";
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
                if (!string.IsNullOrEmpty(vm.AudioQuality_SelectedItem))
                {
                    // -------------------------
                    // Copy Selected
                    // -------------------------
                    if (vm.AudioCodec_SelectedItem == "Copy")
                    {
                        // -------------------------
                        // Switch back to format's default codec
                        // -------------------------
                        if (vm.AudioCodec_SelectedItem != "Auto" ||
                            !string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                            )
                        {
                            // -------------------------
                            // Video Container
                            // -------------------------
                            if (vm.Container_SelectedItem == "webm")
                            {
                                vm.AudioCodec_SelectedItem = "Vorbis";
                            }
                            else if (vm.Container_SelectedItem == "mp4")
                            {
                                vm.AudioCodec_SelectedItem = "AAC";
                            }
                            else if (vm.Container_SelectedItem == "mkv")
                            {
                                vm.AudioCodec_SelectedItem = "AC3";
                            }
                            else if (vm.Container_SelectedItem == "m2v")
                            {
                                vm.AudioCodec_SelectedItem = "None";
                            }
                            else if (vm.Container_SelectedItem == "mpg")
                            {
                                vm.AudioCodec_SelectedItem = "AC3";
                            }
                            else if (vm.Container_SelectedItem == "avi")
                            {
                                vm.AudioCodec_SelectedItem = "LAME";
                            }
                            else if (vm.Container_SelectedItem == "ogv")
                            {
                                vm.AudioCodec_SelectedItem = "Vorbis";
                            }

                            // -------------------------
                            // Audio Container
                            // -------------------------
                            if (vm.Container_SelectedItem == "m4a")
                            {
                                vm.AudioCodec_SelectedItem = "AAC";
                            }
                            else if (vm.Container_SelectedItem == "mp3")
                            {
                                vm.AudioCodec_SelectedItem = "LAME";
                            }
                            else if (vm.Container_SelectedItem == "ogg")
                            {
                                vm.AudioCodec_SelectedItem = "Opus";
                            }
                            else if (vm.Container_SelectedItem == "flac")
                            {
                                vm.AudioCodec_SelectedItem = "FLAC";
                            }
                            else if (vm.Container_SelectedItem == "wav")
                            {
                                vm.AudioCodec_SelectedItem = "PCM";
                            }

                            // -------------------------
                            // Image Container
                            // -------------------------
                            //if (vm.Container_SelectedItem == "jpg")
                            //{
                            //    vm.AudioCodec_SelectedItem = "None";
                            //}
                            //else if (vm.Container_SelectedItem == "png")
                            //{
                            //    vm.AudioCodec_SelectedItem = "None";
                            //}
                            //else if (vm.Container_SelectedItem == "webp")
                            //{
                            //    vm.AudioCodec_SelectedItem = "None";
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
                vm.AudioCodec_SelectedItem == "Copy")
            {
                CopyControls(vm);
            }
        }




    }
}
