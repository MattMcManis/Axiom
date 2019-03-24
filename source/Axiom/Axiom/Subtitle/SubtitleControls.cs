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
                MOV_Text.Codec_Set(vm);

                // Items Source
                MOV_Text.Controls_ItemsSource(vm);

                // Selected Items
                MOV_Text.Controls_Selected(vm);

                // Checked
                MOV_Text.Controls_Checked(vm);

                // Unhecked
                MOV_Text.Controls_Unhecked(vm);

                // Enabled
                MOV_Text.Controls_Enable(vm);

                // Disabled
                MOV_Text.Controls_Disable(vm);
            }

            // -------------------------
            // SSA
            // -------------------------
            else if (selectedCodec == "SSA")
            {
                // Codec
                SSA.Codec_Set(vm);

                // Items Source
                SSA.Controls_ItemsSource(vm);

                // Selected Items
                SSA.Controls_Selected(vm);

                // Checked
                SSA.Controls_Checked(vm);

                // Unhecked
                SSA.Controls_Unhecked(vm);

                // Enabled
                SSA.Controls_Enable(vm);

                // Disabled
                SSA.Controls_Disable(vm);
            }

            // -------------------------
            // SRT
            // -------------------------
            else if (selectedCodec == "SRT")
            {
                // Codec
                SRT.Codec_Set(vm);

                // Items Source
                SRT.Controls_ItemsSource(vm);

                // Selected Items
                SRT.Controls_Selected(vm);

                // Checked
                SRT.Controls_Checked(vm);

                // Unhecked
                SRT.Controls_Unhecked(vm);

                // Enabled
                SRT.Controls_Enable(vm);

                // Disabled
                SRT.Controls_Disable(vm);
            }

            // -------------------------
            // Burn
            // -------------------------
            else if (selectedCodec == "Burn")
            {
                // Codec
                Burn.Codec_Set(vm);

                // Items Source
                Burn.Controls_ItemsSource(vm);

                // Selected Items
                Burn.Controls_Selected(vm);

                // Checked
                Burn.Controls_Checked(vm);

                // Unhecked
                Burn.Controls_Unhecked(vm);

                // Enabled
                Burn.Controls_Enable(vm);

                // Disabled
                Burn.Controls_Disable(vm);
            }

            // -------------------------
            // Copy
            // -------------------------
            else if (selectedCodec == "Copy")
            {
                // Codec
                SubtitleCopy.Codec_Set(vm);

                // Items Source
                SubtitleCopy.Controls_ItemsSource(vm);

                // Selected Items
                SubtitleCopy.Controls_Selected(vm);

                // Checked
                SubtitleCopy.Controls_Checked(vm);

                // Unhecked
                SubtitleCopy.Controls_Unhecked(vm);

                // Enabled
                SubtitleCopy.Controls_Enable(vm);

                // Disabled
                SubtitleCopy.Controls_Disable(vm);
            }

            // -------------------------
            // None
            // -------------------------
            else if (selectedCodec == "None")
            {
                // Codec
                SubtitleNone.Codec_Set(vm);

                // Items Source
                SubtitleNone.Controls_ItemsSource(vm);

                // Selected Items
                SubtitleNone.Controls_Selected(vm);

                // Checked
                SubtitleNone.Controls_Checked(vm);

                // Unhecked
                SubtitleNone.Controls_Unhecked(vm);

                // Enabled
                SubtitleNone.Controls_Enable(vm);

                // Disabled
                SubtitleNone.Controls_Disable(vm);
            }

            // --------------------------------------------------
            // Default Selected Item
            // --------------------------------------------------
            // Video
            //vm.Video_Quality_SelectedItem = SelectedItem(vm.Video_Quality_Items,
            //                                            vm.Video_Quality_SelectedItem
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
            if (vm.Video_Quality_SelectedItem == "Auto" &&
                vm.Video_Scale_SelectedItem == "Source" &&
                string.IsNullOrEmpty(CropWindow.crop) &&
                vm.Video_FPS_SelectedItem == "auto" &&
                vm.Video_Optimize_SelectedItem == "None" &&

                // Extension Match
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
                if (vm.Subtitle_Codec_Items.Count > 0)
                {
                    if (vm.Subtitle_Codec_Items?.Contains("Copy") == true)
                    {
                        vm.Subtitle_Codec_SelectedItem = "Copy";
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
                if (!string.IsNullOrEmpty(vm.Subtitle_Stream_SelectedItem))
                {
                    // -------------------------
                    // Copy Selected 
                    // -------------------------
                    if (vm.Subtitle_Codec_SelectedItem == "Copy")
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
                            if (vm.Format_Container_SelectedItem == "webm")
                            {
                                vm.Subtitle_Codec_SelectedItem = "None";
                            }
                            // -------------------------
                            // MP4
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "mp4")
                            {
                                vm.Subtitle_Codec_SelectedItem = "MOV Text";
                            }
                            // -------------------------
                            // MKV
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "mkv")
                            {
                                vm.Subtitle_Codec_SelectedItem = "Copy";
                            }
                            // -------------------------
                            // MPG
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "mpg")
                            {
                                vm.Subtitle_Codec_SelectedItem = "Copy";
                            }
                            // -------------------------
                            // AVI
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "avi")
                            {
                                vm.Subtitle_Codec_SelectedItem = "SRT";
                            }
                            // -------------------------
                            // OGV
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "ogv")
                            {
                                vm.Subtitle_Codec_SelectedItem = "None";
                            }
                            // -------------------------
                            // JPG
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "jpg")
                            {
                                vm.Subtitle_Codec_SelectedItem = "None";
                            }
                            // -------------------------
                            // PNG
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "png")
                            {
                                vm.Subtitle_Codec_SelectedItem = "None";
                            }
                            // -------------------------
                            // WebP
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "webp")
                            {
                                vm.Subtitle_Codec_SelectedItem = "None";
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
                vm.Audio_Codec_SelectedItem == "Copy")
            {
                CopyControls(vm);
            }
        } 



    }
}
