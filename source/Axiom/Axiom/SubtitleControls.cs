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

using System;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class SubtitleControls
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
            // MOV_Text
            // -------------------------
            if (selectedCodec == "MOV Text")
            {
                // Codec
                vm.SubtitleCodec_Command = MOV_Text.codec;

                // Stream
                vm.SubtitleStream_SelectedItem = MOV_Text.stream;

                // Checked
                MOV_Text.controlsChecked(vm);

                // Enabled
                MOV_Text.controlsEnable(vm);

                // Disabled
                MOV_Text.controlsDisable(vm);
            }

            // -------------------------
            // SSA
            // -------------------------
            else if (selectedCodec == "SSA")
            {
                // Codec
                vm.SubtitleCodec_Command = SSA.codec;

                // Stream
                vm.SubtitleStream_SelectedItem = SSA.stream;

                // Checked
                SSA.controlsChecked(vm);

                // Enabled
                SSA.controlsEnable(vm);

                // Disabled
                SSA.controlsDisable(vm);
            }

            // -------------------------
            // SRT
            // -------------------------
            else if (selectedCodec == "SRT")
            {
                // Codec
                vm.SubtitleCodec_Command = SRT.codec;

                // Stream
                vm.SubtitleStream_SelectedItem = SRT.stream;

                // Checked
                SRT.controlsChecked(vm);

                // Enabled
                SRT.controlsEnable(vm);

                // Disabled
                SRT.controlsDisable(vm);
            }

            // -------------------------
            // Burn
            // -------------------------
            else if (selectedCodec == "Burn")
            {
                // Codec
                vm.SubtitleCodec_Command = Burn.codec;

                // Stream
                vm.SubtitleStream_SelectedItem = Burn.stream;

                // Checked
                Burn.controlsChecked(vm);

                // Enabled
                Burn.controlsEnable(vm);

                // Disabled
                Burn.controlsDisable(vm);
            }

            // -------------------------
            // Copy
            // -------------------------
            else if (selectedCodec == "Copy")
            {
                // Codec
                vm.SubtitleCodec_Command = SubtitleCopy.codec;

                // Checked
                SubtitleCopy.controlsChecked(vm);

                // Enabled
                SubtitleCopy.controlsEnable(vm);

                // Disabled
                SubtitleCopy.controlsDisable(vm);
            }

            // -------------------------
            // None
            // -------------------------
            else if (selectedCodec == "None")
            {
                // Codec
                vm.SubtitleCodec_Command = SubtitleNone.codec;

                // Checked
                SubtitleNone.controlsChecked(vm);

                // Enabled
                SubtitleNone.controlsEnable(vm);

                // Disabled
                SubtitleNone.controlsDisable(vm);
            }

            // --------------------------------------------------
            // Default Selected Item
            // --------------------------------------------------
            // Video
            //vm.VideoQuality_SelectedItem = SelectedItem(vm.VideoQuality_Items,
            //                                            vm.VideoQuality_SelectedItem
            //                                            );
        }


        /// <summary>
        ///    Auto Copy Conditions Check
        /// <summary>
        public static bool AutoCopyConditionsCheck(ViewModel vm,
                                                   string inputExt,
                                                   string outputExt)
        {
            // Input Extension is Same as Output Extension and Video Quality is Auto
            if (vm.VideoQuality_SelectedItem == "Auto" &&
                vm.Size_SelectedItem == "Source" &&
                string.IsNullOrEmpty(CropWindow.crop) &&
                vm.FPS_SelectedItem == "auto" &&
                vm.Video_Optimize_SelectedItem == "None" &&

                // Extension Match
                //!string.IsNullOrEmpty(inputExt) &&
                //!string.IsNullOrEmpty(outputExt) &&
                string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase)
            )
            {
                return true;
            }

            // Did Not Pass Check
            else
            {
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
                // Set Subtitle Codec Combobox Selected Item to Copy
                // -------------------------
                if (vm.SubtitleCodec_Items.Count > 0)
                {
                    if (vm.SubtitleCodec_Items?.Contains("Copy") == true)
                    {
                        vm.SubtitleCodec_SelectedItem = "Copy";
                    }
                }
            }

            // -------------------------
            // Reset to Default Codec
            // -------------------------
            // Disable Copy if:
            // Input / Output Extensions don't match
            // Batch / Output Extensions don't match
            // Size is Not No
            // Crop is Not Empty
            // FPS is Not Auto
            // Optimize is Not None
            // -------------------------
            else
            {
                // -------------------------
                // Null Check
                // -------------------------
                if (!string.IsNullOrEmpty(vm.SubtitleStream_SelectedItem))
                {
                    // -------------------------
                    // Copy Selected 
                    // -------------------------
                    if (vm.SubtitleCodec_SelectedItem == "Copy")
                    {
                        // -------------------------
                        // Switch back to format's default codec
                        // -------------------------
                        if (!string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                            )
                        {
                            // -------------------------
                            // WebM
                            // -------------------------
                            if (vm.Container_SelectedItem == "webm")
                            {
                                vm.SubtitleCodec_SelectedItem = "None";
                            }
                            // -------------------------
                            // MP4
                            // -------------------------
                            else if (vm.Container_SelectedItem == "mp4")
                            {
                                vm.SubtitleCodec_SelectedItem = "mov_text";
                            }
                            // -------------------------
                            // MKV
                            // -------------------------
                            else if (vm.Container_SelectedItem == "mkv")
                            {
                                vm.SubtitleCodec_SelectedItem = "Copy";
                            }
                            // -------------------------
                            // MPG
                            // -------------------------
                            else if (vm.Container_SelectedItem == "mpg")
                            {
                                vm.SubtitleCodec_SelectedItem = "Copy";
                            }
                            // -------------------------
                            // AVI
                            // -------------------------
                            else if (vm.Container_SelectedItem == "avi")
                            {
                                vm.SubtitleCodec_SelectedItem = "SRT";
                            }
                            // -------------------------
                            // OGV
                            // -------------------------
                            else if (vm.Container_SelectedItem == "ogv")
                            {
                                vm.SubtitleCodec_SelectedItem = "None";
                            }
                            // -------------------------
                            // JPG
                            // -------------------------
                            else if (vm.Container_SelectedItem == "jpg")
                            {
                                vm.SubtitleCodec_SelectedItem = "None";
                            }
                            // -------------------------
                            // PNG
                            // -------------------------
                            else if (vm.Container_SelectedItem == "png")
                            {
                                vm.SubtitleCodec_SelectedItem = "None";
                            }
                            // -------------------------
                            // WebP
                            // -------------------------
                            else if (vm.Container_SelectedItem == "webp")
                            {
                                vm.SubtitleCodec_SelectedItem = "None";
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        ///    Auto Codec Copy
        /// <summary>
        public static void AutoCopySubtitleCodec(ViewModel vm)
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
