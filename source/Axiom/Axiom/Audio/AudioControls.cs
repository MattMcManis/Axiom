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
                // Audio Codec
                vm.Audio_Codec_Command = Vorbis.codec;

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
            else if (codec_SelectedItem == "Opus")
            {
                // Audio Codec
                vm.Audio_Codec_Command = Opus.codec;

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
            else if (codec_SelectedItem == "AC3")
            {
                // Audio Codec
                vm.Audio_Codec_Command = AC3.codec;

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
            else if (codec_SelectedItem == "AAC")
            {
                // Audio Codec
                vm.Audio_Codec_Command = AAC.codec;

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
            else if (codec_SelectedItem == "ALAC")
            {
                // Audio Codec
                vm.Audio_Codec_Command = ALAC.codec;

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
            else if (codec_SelectedItem == "FLAC")
            {
                // Audio Codec
                vm.Audio_Codec_Command = FLAC.codec;

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
            else if (codec_SelectedItem == "PCM")
            {
                // Audio Codec
                vm.Audio_Codec_Command = PCM.codec;

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
            // MP2
            // -------------------------
            else if (codec_SelectedItem == "MP2")
            {
                // Audio Codec
                vm.Audio_Codec_Command = MP2.codec;

                // Item Source
                MP2.controlsItemSource(vm);

                // Selected Items
                MP2.controlsSelected(vm);

                // Checked
                MP2.controlsChecked(vm);

                // Unhecked
                MP2.controlsUnhecked(vm);

                // Enabled
                MP2.controlsEnable(vm);

                // Disabled
                MP2.controlsDisable(vm);
            }

            // -------------------------
            // LAME
            // -------------------------
            else if (codec_SelectedItem == "LAME")
            {
                // Audio Codec
                vm.Audio_Codec_Command = LAME.codec;

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
            else if (codec_SelectedItem == "Copy")
            {
                // Audio Codec
                vm.Audio_Codec_Command = AudioCopy.codec;

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
            else if (codec_SelectedItem == "None")
            {
                // Audio Codec
                vm.Audio_Codec_Command = AudioNone.codec;

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
                //MessageBox.Show(vm.Audio_Quality_SelectedItem);
                //MessageBox.Show(vm.Audio_VBR_IsChecked.ToString());
                //vm.Audio_Bitrate_Text = "test";
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
