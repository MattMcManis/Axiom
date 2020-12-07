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
using ViewModel;
using Axiom;
using System.Collections.Generic;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Controls
{
    namespace Subtitles
    {
        public class Controls
        {
            private static Dictionary<string, ISubtitleCodec> _codec_s;

            private static void InitializeCodecs()
            {
                _codec_s = new Dictionary<string, ISubtitleCodec> {
                    { "MOV Text",   new Codec.MOV_Text() },
                    { "SSA",        new Codec.SSA() },
                    { "SRT",        new Codec.SRT() },
                    { "Burn",       new Codec.Burn() },
                    { "Copy",       new Codec.Copy() },
                    { "None",       new Codec.None() }
                };
            }

            public interface ISubtitleCodec
            {
                // Codec
                void Codec_Set();

                // Items Source
                void Controls_ItemsSource();
                // Selected Items
                void Controls_Selected();

                // Checked
                void Controls_Checked();
                // Unhecked
                void Controls_Unhecked();

                // Enabled
                void Controls_Enable();
                // Disabled
                void Controls_Disable();
            }

            /// <summary>
            /// Set Controls
            /// </summary>
            public static void SetControls(string codec_SelectedItem)
            {
                // --------------------------------------------------
                // Codec
                // --------------------------------------------------

                if (!string.IsNullOrWhiteSpace(codec_SelectedItem))
                {
                    InitializeCodecs();

                    // Codec
                    _codec_s[codec_SelectedItem].Codec_Set();

                    // Items Source
                    _codec_s[codec_SelectedItem].Controls_ItemsSource();
                    // Selected Items
                    _codec_s[codec_SelectedItem].Controls_Selected();

                    // Checked
                    _codec_s[codec_SelectedItem].Controls_Checked();
                    // Unhecked
                    _codec_s[codec_SelectedItem].Controls_Unhecked();

                    // Enabled
                    _codec_s[codec_SelectedItem].Controls_Enable();
                    // Disabled
                    _codec_s[codec_SelectedItem].Controls_Disable();
                }

                //switch (selectedCodec)
                //{
                //    // -------------------------
                //    // MOV_Text
                //    // -------------------------
                //    case "MOV Text":
                //        // Codec
                //        Codec.MOV_Text.Codec_Set();

                //        // Items Source
                //        Codec.MOV_Text.Controls_ItemsSource();
                //        // Selected Items
                //        Codec.MOV_Text.Controls_Selected();

                //        // Checked
                //        Codec.MOV_Text.Controls_Checked();
                //        // Unhecked
                //        Codec.MOV_Text.Controls_Unhecked();

                //        // Enabled
                //        Codec.MOV_Text.Controls_Enable();
                //        // Disabled
                //        Codec.MOV_Text.Controls_Disable();
                //        break;

                //    // -------------------------
                //    // SSA
                //    // -------------------------
                //    case "SSA":
                //        // Codec
                //        Codec.SSA.Codec_Set();

                //        // Items Source
                //        Codec.SSA.Controls_ItemsSource();
                //        // Selected Items
                //        Codec.SSA.Controls_Selected();

                //        // Checked
                //        Codec.SSA.Controls_Checked();
                //        // Unhecked
                //        Codec.SSA.Controls_Unhecked();

                //        // Enabled
                //        Codec.SSA.Controls_Enable();
                //        // Disabled
                //        Codec.SSA.Controls_Disable();
                //        break;

                //    // -------------------------
                //    // SRT
                //    // -------------------------
                //    case "SRT":
                //        // Codec
                //        Codec.SRT.Codec_Set();

                //        // Items Source
                //        Codec.SRT.Controls_ItemsSource();
                //        // Selected Items
                //        Codec.SRT.Controls_Selected();

                //        // Checked
                //        Codec.SRT.Controls_Checked();
                //        // Unhecked
                //        Codec.SRT.Controls_Unhecked();

                //        // Enabled
                //        Codec.SRT.Controls_Enable();
                //        // Disabled
                //        Codec.SRT.Controls_Disable();
                //        break;

                //    // -------------------------
                //    // Burn
                //    // -------------------------
                //    case "Burn":
                //        // Codec
                //        Codec.Burn.Codec_Set();

                //        // Items Source
                //        Codec.Burn.Controls_ItemsSource();
                //        // Selected Items
                //        Codec.Burn.Controls_Selected();

                //        // Checked
                //        Codec.Burn.Controls_Checked();
                //        // Unhecked
                //        Codec.Burn.Controls_Unhecked();

                //        // Enabled
                //        Codec.Burn.Controls_Enable();
                //        // Disabled
                //        Codec.Burn.Controls_Disable();
                //        break;

                //    // -------------------------
                //    // Copy
                //    // -------------------------
                //    case "Copy":
                //        // Codec
                //        Codec.Copy.Codec_Set();

                //        // Items Source
                //        Codec.Copy.Controls_ItemsSource();
                //        // Selected Items
                //        Codec.Copy.Controls_Selected();

                //        // Checked
                //        Codec.Copy.Controls_Checked();
                //        // Unhecked
                //        Codec.Copy.Controls_Unhecked();

                //        // Enabled
                //        Codec.Copy.Controls_Enable();
                //        // Disabled
                //        Codec.Copy.Controls_Disable();
                //        break;

                //    // -------------------------
                //    // None
                //    // -------------------------
                //    case "None":
                //        // Codec
                //        Codec.None.Codec_Set();

                //        // Items Source
                //        Codec.None.Controls_ItemsSource();
                //        // Selected Items
                //        Codec.None.Controls_Selected();

                //        // Checked
                //        Codec.None.Controls_Checked();
                //        // Unhecked
                //        Codec.None.Controls_Unhecked();

                //        // Enabled
                //        Codec.None.Controls_Enable();
                //        // Disabled
                //        Codec.None.Controls_Disable();
                //        break;
                //}

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
                switch (VM.SubtitleView.Subtitle_Codec_SelectedItem)
                {
                    case "None":
                        VM.SubtitleView.Subtitle_Stream_SelectedItem = "none";
                        VM.SubtitleView.Subtitle_Stream_IsEnabled = false;
                        break;

                    case "Burn":
                        // Force Select External
                        // Can't burn All subtitle streams
                        VM.SubtitleView.Subtitle_Stream_SelectedItem = "external";
                        VM.SubtitleView.Subtitle_Stream_IsEnabled = true;
                        break;

                    case "Copy":
                        VM.SubtitleView.Subtitle_Stream_IsEnabled = true;
                        break;

                    // All Other Codecs
                    default:
                        VM.SubtitleView.Subtitle_Stream_IsEnabled = true;
                        break;
                }

                //// -------------------------
                //// None Codec
                //// -------------------------
                //if (VM.SubtitleView.Subtitle_Codec_SelectedItem == "None")
                //{
                //    VM.SubtitleView.Subtitle_Stream_SelectedItem = "none";
                //    VM.SubtitleView.Subtitle_Stream_IsEnabled = false;
                //}

                //// -------------------------
                //// Burn Codec
                //// -------------------------
                //else if (VM.SubtitleView.Subtitle_Codec_SelectedItem == "Burn")
                //{
                //    // Force Select External
                //    // Can't burn All subtitle streams
                //    VM.SubtitleView.Subtitle_Stream_SelectedItem = "external";
                //    VM.SubtitleView.Subtitle_Stream_IsEnabled = true;
                //}

                //// -------------------------
                //// Copy Codec
                //// -------------------------
                //else if (VM.SubtitleView.Subtitle_Codec_SelectedItem == "Copy")
                //{
                //    VM.SubtitleView.Subtitle_Stream_IsEnabled = true;
                //}

                //// -------------------------
                //// All Other Codecs
                //// -------------------------
                //else
                //{
                //    VM.SubtitleView.Subtitle_Stream_IsEnabled = true;
                //}
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
}
