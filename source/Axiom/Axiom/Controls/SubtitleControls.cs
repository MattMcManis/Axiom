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

using System;
using System.IO;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class SubtitleControls
    {
        /// <summary>
        /// Set Controls
        /// </summary>
        public static void SetControls(string selectedCodec)
        {
            // --------------------------------------------------
            // Codec
            // --------------------------------------------------

            switch (selectedCodec)
            {
                // -------------------------
                // MOV_Text
                // -------------------------
                case "MOV Text":
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
                    break;

                // -------------------------
                // SSA
                // -------------------------
                case "SSA":
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
                    break;

                // -------------------------
                // SRT
                // -------------------------
                case "SRT":
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
                    break;

                // -------------------------
                // Burn
                // -------------------------
                case "Burn":
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
                    break;

                // -------------------------
                // Copy
                // -------------------------
                case "Copy":
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
                    break;

                // -------------------------
                // None
                // -------------------------
                case "None":
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
                    break;
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
        /// Auto Copy Conditions Check
        /// <summary>
        public static bool AutoCopyConditionsCheck()
        {
            // Failed
            if (VM.VideoView.Video_Quality_SelectedItem != "Auto" ||
                VM.VideoView.Video_Scale_SelectedItem != "Source" ||
                !string.IsNullOrWhiteSpace(CropWindow.crop) ||
                VM.VideoView.Video_FPS_SelectedItem != "auto" ||
                VM.VideoView.Video_Optimize_SelectedItem != "None"
            )
            {
                return false;
            }

            // Passed
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Copy Controls
        /// <summary>
        public static void AutoCopySubtitleCodec(string trigger)
        {
            // -------------------------
            // Halt if Selected Codec is Null
            // -------------------------
            if (string.IsNullOrWhiteSpace(VM.SubtitleView.Subtitle_Codec_SelectedItem))
            {
                return;
            }

            // -------------------------
            // Halt if trigger is control
            // Pass if trigger is input
            // -------------------------
            if (trigger == "control" &&
                VM.SubtitleView.Subtitle_Codec_SelectedItem != "Copy" &&
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
            string inputExt = Path.GetExtension(VM.MainView.Input_Text).ToLower();

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
            string outputExt = "." + VM.FormatView.Format_Container_SelectedItem.ToLower();

            // -------------------------
            // Conditions Check
            // Enable
            // -------------------------
            if (AutoCopyConditionsCheck() == true &&
                inputExt == outputExt)
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
                // Copy Selected 
                // -------------------------
                if (VM.SubtitleView.Subtitle_Codec_SelectedItem == "Copy")
                {
                    switch (VM.FormatView.Format_Container_SelectedItem)
                    {
                        // -------------------------
                        // WebM
                        // -------------------------
                        case "webm":
                            VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                            break;
                        // -------------------------
                        // MP4
                        // -------------------------
                        case "mp4":
                            VM.SubtitleView.Subtitle_Codec_SelectedItem = "MOV Text";
                            break;
                        // -------------------------
                        // MKV
                        // -------------------------
                        case "mkv":
                            VM.SubtitleView.Subtitle_Codec_SelectedItem = "Copy";
                            break;
                        // -------------------------
                        // MPG
                        // -------------------------
                        case "mpg":
                            VM.SubtitleView.Subtitle_Codec_SelectedItem = "Copy";
                            break;
                        // -------------------------
                        // AVI
                        // -------------------------
                        case "avi":
                            VM.SubtitleView.Subtitle_Codec_SelectedItem = "SRT";
                            break;
                        // -------------------------
                        // OGV
                        // -------------------------
                        case "ogv":
                            VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                            break;
                        // -------------------------
                        // JPG
                        // -------------------------
                        case "jpg":
                            VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                            break;
                        // -------------------------
                        // PNG
                        // -------------------------
                        case "png":
                            VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                            break;
                        // -------------------------
                        // WebP
                        // -------------------------
                        case "webp":
                            VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                            break;
                    }
                }
            }
        }

    }
}
