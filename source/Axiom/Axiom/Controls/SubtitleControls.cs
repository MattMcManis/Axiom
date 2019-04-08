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
        public static void SetControls(string selectedCodec)
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
                MOV_Text.Codec_Set();

                // Items Source
                MOV_Text.Controls_ItemsSource();

                // Selected Items
                MOV_Text.Controls_Selected();

                // Checked
                MOV_Text.Controls_Checked();

                // Unhecked
                MOV_Text.Controls_Unhecked();

                // Enabled
                MOV_Text.Controls_Enable();

                // Disabled
                MOV_Text.Controls_Disable();
            }

            // -------------------------
            // SSA
            // -------------------------
            else if (selectedCodec == "SSA")
            {
                // Codec
                SSA.Codec_Set();

                // Items Source
                SSA.Controls_ItemsSource();

                // Selected Items
                SSA.Controls_Selected();

                // Checked
                SSA.Controls_Checked();

                // Unhecked
                SSA.Controls_Unhecked();

                // Enabled
                SSA.Controls_Enable();

                // Disabled
                SSA.Controls_Disable();
            }

            // -------------------------
            // SRT
            // -------------------------
            else if (selectedCodec == "SRT")
            {
                // Codec
                SRT.Codec_Set();

                // Items Source
                SRT.Controls_ItemsSource();

                // Selected Items
                SRT.Controls_Selected();

                // Checked
                SRT.Controls_Checked();

                // Unhecked
                SRT.Controls_Unhecked();

                // Enabled
                SRT.Controls_Enable();

                // Disabled
                SRT.Controls_Disable();
            }

            // -------------------------
            // Burn
            // -------------------------
            else if (selectedCodec == "Burn")
            {
                // Codec
                Burn.Codec_Set();

                // Items Source
                Burn.Controls_ItemsSource();

                // Selected Items
                Burn.Controls_Selected();

                // Checked
                Burn.Controls_Checked();

                // Unhecked
                Burn.Controls_Unhecked();

                // Enabled
                Burn.Controls_Enable();

                // Disabled
                Burn.Controls_Disable();
            }

            // -------------------------
            // Copy
            // -------------------------
            else if (selectedCodec == "Copy")
            {
                // Codec
                SubtitleCopy.Codec_Set();

                // Items Source
                SubtitleCopy.Controls_ItemsSource();

                // Selected Items
                SubtitleCopy.Controls_Selected();

                // Checked
                SubtitleCopy.Controls_Checked();

                // Unhecked
                SubtitleCopy.Controls_Unhecked();

                // Enabled
                SubtitleCopy.Controls_Enable();

                // Disabled
                SubtitleCopy.Controls_Disable();
            }

            // -------------------------
            // None
            // -------------------------
            else if (selectedCodec == "None")
            {
                // Codec
                SubtitleNone.Codec_Set();

                // Items Source
                SubtitleNone.Controls_ItemsSource();

                // Selected Items
                SubtitleNone.Controls_Selected();

                // Checked
                SubtitleNone.Controls_Checked();

                // Unhecked
                SubtitleNone.Controls_Unhecked();

                // Enabled
                SubtitleNone.Controls_Enable();

                // Disabled
                SubtitleNone.Controls_Disable();
            }

            // --------------------------------------------------
            // Default Selected Item
            // --------------------------------------------------
            // Video
            //VM.VideoView.Video_Quality_SelectedItem = SelectedItem(VM.VideoView.Video_Quality_Items,
            //                                            VM.VideoView.Video_Quality_SelectedItem
            //                                            );


            // --------------------------------------------------
            // Selected Items
            // --------------------------------------------------
            // -------------------------
            // None Codec
            // -------------------------
            if (VM.SubtitleView.Subtitle_Codec_SelectedItem == "None")
            {
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "none";
                VM.SubtitleView.Subtitle_Stream_IsEnabled = false;
            }

            // -------------------------
            // Burn Codec
            // -------------------------
            else if (VM.SubtitleView.Subtitle_Codec_SelectedItem == "Burn")
            {
                // Force Select External
                // Can't burn All subtitle streams
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "external";
                VM.SubtitleView.Subtitle_Stream_IsEnabled = true;
            }

            // -------------------------
            // Copy Codec
            // -------------------------
            else if (VM.SubtitleView.Subtitle_Codec_SelectedItem == "Copy")
            {
                VM.SubtitleView.Subtitle_Stream_IsEnabled = true;
            }

            // -------------------------
            // All Other Codecs
            // -------------------------
            else
            {
                VM.SubtitleView.Subtitle_Stream_IsEnabled = true;
            }
        }


        /// <summary>
        ///    Auto Copy Conditions Check
        /// <summary>
        public static bool AutoCopyConditionsCheck(
                                                   string inputExt,
                                                   string outputExt)
        {
            // Input Extension is Same as Output Extension and Video Quality is Auto
            if (VM.VideoView.Video_Quality_SelectedItem == "Auto" &&
                VM.VideoView.Video_Scale_SelectedItem == "Source" &&
                string.IsNullOrEmpty(CropWindow.crop) &&
                VM.VideoView.Video_FPS_SelectedItem == "auto" &&
                VM.VideoView.Video_Optimize_SelectedItem == "None" &&

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
        public static void CopyControls()
        {
            // -------------------------
            // Conditions Check
            // Enable
            // -------------------------
            if (AutoCopyConditionsCheck(/*main_vm,*/ MainWindow.inputExt, MainWindow.outputExt) == true)
            {
                // -------------------------
                // Set Subtitle Codec Combobox Selected Item to Copy
                // -------------------------
                if (VM.SubtitleView.Subtitle_Codec_Items.Count > 0)
                {
                    if (VM.SubtitleView.Subtitle_Codec_Items?.Contains("Copy") == true)
                    {
                        VM.SubtitleView.Subtitle_Codec_SelectedItem = "Copy";
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
                if (!string.IsNullOrEmpty(VM.SubtitleView.Subtitle_Stream_SelectedItem))
                {
                    // -------------------------
                    // Copy Selected 
                    // -------------------------
                    if (VM.SubtitleView.Subtitle_Codec_SelectedItem == "Copy")
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
                            if (VM.FormatView.Format_Container_SelectedItem == "webm")
                            {
                                VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                            }
                            // -------------------------
                            // MP4
                            // -------------------------
                            else if (VM.FormatView.Format_Container_SelectedItem == "mp4")
                            {
                                VM.SubtitleView.Subtitle_Codec_SelectedItem = "MOV Text";
                            }
                            // -------------------------
                            // MKV
                            // -------------------------
                            else if (VM.FormatView.Format_Container_SelectedItem == "mkv")
                            {
                                VM.SubtitleView.Subtitle_Codec_SelectedItem = "Copy";
                            }
                            // -------------------------
                            // MPG
                            // -------------------------
                            else if (VM.FormatView.Format_Container_SelectedItem == "mpg")
                            {
                                VM.SubtitleView.Subtitle_Codec_SelectedItem = "Copy";
                            }
                            // -------------------------
                            // AVI
                            // -------------------------
                            else if (VM.FormatView.Format_Container_SelectedItem == "avi")
                            {
                                VM.SubtitleView.Subtitle_Codec_SelectedItem = "SRT";
                            }
                            // -------------------------
                            // OGV
                            // -------------------------
                            else if (VM.FormatView.Format_Container_SelectedItem == "ogv")
                            {
                                VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                            }
                            // -------------------------
                            // JPG
                            // -------------------------
                            else if (VM.FormatView.Format_Container_SelectedItem == "jpg")
                            {
                                VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                            }
                            // -------------------------
                            // PNG
                            // -------------------------
                            else if (VM.FormatView.Format_Container_SelectedItem == "png")
                            {
                                VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                            }
                            // -------------------------
                            // WebP
                            // -------------------------
                            else if (VM.FormatView.Format_Container_SelectedItem == "webp")
                            {
                                VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        ///    Auto Codec Copy
        /// <summary>
        public static void AutoCopySubtitleCodec()
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
