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
 * Quality Controls
 * Pixel Format Controls
 * Optimize Controls
 * Encoding Pass Controls
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
    public class VideoControls
    {
        // ----------------------------------------------------------------------------------------------------
        //
        // Order of Operations
        //
        // Container -> Codec -> Quality / Pixel Format / Optimize
        // 
        // Codec Class -> ComboBox Items Source -> Video Controls Class -> Pass to ViewModel
        //
        // ----------------------------------------------------------------------------------------------------


        /// <summary>
        ///     Controls
        /// </summary>
        public static bool passUserSelected = false; // Used to determine if User willingly selected CRF, 1 Pass or 2 Pass


        /// <summary>
        ///     Set Controls
        /// </summary>
        public static void SetControls(ViewModel vm, string codec_SelectedItem)
        {
            // --------------------------------------------------
            // Codec
            // --------------------------------------------------

            // -------------------------
            // VP8
            // -------------------------
            if (codec_SelectedItem == "VP8")
            {
                // Codec
                vm.Video_Codec_Command = VP8.codec;
                // Codec Parameters
                vm.Video_Codec_Parameters = VP8.codecParameters;

                // Items Source
                VP8.controlsItemSource(vm);

                // Selected Items
                VP8.controlsSelected(vm);

                // Checked
                VP8.controlsChecked(vm);

                // Unhecked
                VP8.controlsUnhecked(vm);

                // Enabled
                VP8.controlsEnable(vm);

                // Disabled
                VP8.controlsDisable(vm);
            }

            // -------------------------
            // VP9
            // -------------------------
            else if (codec_SelectedItem == "VP9")
            {
                // Codec
                vm.Video_Codec_Command = VP9.codec;
                // Codec Parameters
                vm.Video_Codec_Parameters = VP9.codecParameters;

                // Items Source
                VP9.controlsItemSource(vm);

                // Selected Items
                VP9.controlsSelected(vm);

                // Checked
                VP9.controlsChecked(vm);

                // Unhecked
                VP9.controlsUnhecked(vm);

                // Enabled
                VP9.controlsEnable(vm);

                // Disabled
                VP9.controlsDisable(vm);
            }

            // -------------------------
            // x264
            // -------------------------
            else if (codec_SelectedItem == "x264")
            {
                // Codec
                vm.Video_Codec_Command = x264.codec;
                // Codec Parameters
                vm.Video_Codec_Parameters = x264.codecParameters;

                // Items Source
                x264.controlsItemSource(vm);

                // Selected Items
                x264.controlsSelected(vm);

                // Checked
                x264.controlsChecked(vm);

                // Unhecked
                x264.controlsUnhecked(vm);

                // Enabled
                x264.controlsEnable(vm);

                // Disabled
                x264.controlsDisable(vm);
            }

            // -------------------------
            // x265
            // -------------------------
            else if (codec_SelectedItem == "x265")
            {
                // Codec
                vm.Video_Codec_Command = x265.codec;
                // Codec Parameters
                vm.Video_Codec_Parameters = x265.codecParameters;

                // Items Source
                x265.controlsItemSource(vm);

                // Selected Items
                x265.controlsSelected(vm);

                // Checked
                x265.controlsChecked(vm);

                // Unhecked
                x265.controlsUnhecked(vm);

                // Enabled
                x265.controlsEnable(vm);

                // Disabled
                x265.controlsDisable(vm);
            }

            // -------------------------
            // MPEG-2
            // -------------------------
            else if (codec_SelectedItem == "MPEG-2")
            {
                // Codec
                vm.Video_Codec_Command = MPEG_2.codec;
                // Codec Parameters
                vm.Video_Codec_Parameters = MPEG_2.codecParameters;

                // Items Source
                MPEG_2.controlsItemSource(vm);

                // Selected Items
                MPEG_2.controlsSelected(vm);

                // Checked
                MPEG_2.controlsChecked(vm);

                // Unhecked
                MPEG_2.controlsUnhecked(vm);

                // Enabled
                MPEG_2.controlsEnable(vm);

                // Disabled
                MPEG_2.controlsDisable(vm);
            }

            // -------------------------
            // MPEG-4
            // -------------------------
            else if (codec_SelectedItem == "MPEG-4")
            {
                // Codec
                vm.Video_Codec_Command = MPEG_4.codec;
                // Codec Parameters
                vm.Video_Codec_Parameters = MPEG_4.codecParameters;

                // Items Source
                MPEG_4.controlsItemSource(vm);

                // Selected Items
                MPEG_4.controlsSelected(vm);

                // Checked
                MPEG_4.controlsChecked(vm);

                // Unhecked
                MPEG_4.controlsUnhecked(vm);

                // Enabled
                MPEG_4.controlsEnable(vm);

                // Disabled
                MPEG_4.controlsDisable(vm);
            }

            // -------------------------
            // AV1
            // -------------------------
            else if (codec_SelectedItem == "AV1")
            {
                // Codec
                vm.Video_Codec_Command = AV1.codec;
                // Codec Parameters
                vm.Video_Codec_Parameters = AV1.codecParameters;

                // Items Source
                AV1.controlsItemSource(vm);

                // Selected Items
                AV1.controlsSelected(vm);

                // Checked
                AV1.controlsChecked(vm);

                // Unhecked
                AV1.controlsUnhecked(vm);

                // Enabled
                AV1.controlsEnable(vm);

                // Disabled
                AV1.controlsDisable(vm);
            }

            // -------------------------
            // FFV1
            // -------------------------
            else if (codec_SelectedItem == "FFV1")
            {
                // Codec
                vm.Video_Codec_Command = FFV1.codec;
                // Codec Parameters
                vm.Video_Codec_Parameters = FFV1.codecParameters;

                // Items Source
                FFV1.controlsItemSource(vm);

                // Selected Items
                FFV1.controlsSelected(vm);

                // Checked
                FFV1.controlsChecked(vm);

                // Unhecked
                FFV1.controlsUnhecked(vm);

                // Enabled
                FFV1.controlsEnable(vm);

                // Disabled
                FFV1.controlsDisable(vm);
            }

            // -------------------------
            // HuffYUV
            // -------------------------
            else if (codec_SelectedItem == "HuffYUV")
            {
                // Codec
                vm.Video_Codec_Command = HuffYUV.codec;
                // Codec Parameters
                vm.Video_Codec_Parameters = HuffYUV.codecParameters;

                // Items Source
                HuffYUV.controlsItemSource(vm);

                // Selected Items
                HuffYUV.controlsSelected(vm);

                // Checked
                HuffYUV.controlsChecked(vm);

                // Unhecked
                HuffYUV.controlsUnhecked(vm);

                // Enabled
                HuffYUV.controlsEnable(vm);

                // Disabled
                HuffYUV.controlsDisable(vm);
            }

            // -------------------------
            // Theora
            // -------------------------
            else if (codec_SelectedItem == "Theora")
            {
                // Codec
                vm.Video_Codec_Command = Theora.codec;
                // Codec Parameters
                vm.Video_Codec_Parameters = Theora.codecParameters;

                // Items Source
                Theora.controlsItemSource(vm);

                // Selected Items
                Theora.controlsSelected(vm);

                // Checked
                Theora.controlsChecked(vm);

                // Unhecked
                Theora.controlsUnhecked(vm);

                // Enabled
                Theora.controlsEnable(vm);

                // Disabled
                Theora.controlsDisable(vm);
            }

            // -------------------------
            // JPEG
            // -------------------------
            else if (codec_SelectedItem == "JPEG")
            {
                // Codec
                vm.Video_Codec_Command = JPEG.codec;
                // Codec Parameters
                vm.Video_Codec_Parameters = JPEG.codecParameters;

                // Items Source
                JPEG.controlsItemSource(vm);

                // Selected Items
                JPEG.controlsSelected(vm);

                // Checked
                JPEG.controlsChecked(vm);

                // Unhecked
                JPEG.controlsUnhecked(vm);

                // Enabled
                JPEG.controlsEnable(vm);

                // Disabled
                JPEG.controlsDisable(vm);
            }

            // -------------------------
            // PNG
            // -------------------------
            else if (codec_SelectedItem == "PNG")
            {
                // Codec
                vm.Video_Codec_Command = PNG.codec;
                // Codec Parameters
                vm.Video_Codec_Parameters = PNG.codecParameters;

                // Items Source
                PNG.controlsItemSource(vm);

                // Selected Items
                PNG.controlsSelected(vm);

                // Checked
                PNG.controlsChecked(vm);

                // Unhecked
                PNG.controlsUnhecked(vm);

                // Enabled
                PNG.controlsEnable(vm);

                // Disabled
                PNG.controlsDisable(vm);
            }

            // -------------------------
            // WebP
            // -------------------------
            else if (codec_SelectedItem == "WebP")
            {
                // Codec
                vm.Video_Codec_Command = WebP.codec;
                // Codec Parameters
                vm.Video_Codec_Parameters = WebP.codecParameters;

                // Items Source
                WebP.controlsItemSource(vm);

                // Selected Items
                WebP.controlsSelected(vm);

                // Checked
                WebP.controlsChecked(vm);

                // Unhecked
                WebP.controlsUnhecked(vm);

                // Enabled
                WebP.controlsEnable(vm);

                // Disabled
                WebP.controlsDisable(vm);
            }

            // -------------------------
            // Copy
            // -------------------------
            else if (codec_SelectedItem == "Copy")
            {
                // Codec
                vm.Video_Codec_Command = VideoCopy.codec;
                // Codec Parameters
                vm.Video_Codec_Parameters = VideoCopy.codecParameters;

                // Items Source
                VideoCopy.controlsItemSource(vm);

                // Selected Items
                VideoCopy.controlsSelected(vm);

                // Checked
                VideoCopy.controlsChecked(vm);

                // Unhecked
                VideoCopy.controlsUnhecked(vm);

                // Enabled
                VideoCopy.controlsEnable(vm);

                // Disabled
                VideoCopy.controlsDisable(vm);
            }

            // -------------------------
            // None
            // -------------------------
            else if (codec_SelectedItem == "None")
            {
                // Codec
                vm.Video_Codec_Command = VideoNone.codec;
                // Codec Parameters
                vm.Video_Codec_Parameters = VideoNone.codecParameters;

                // Items Source
                VideoNone.controlsItemSource(vm);

                // Selected Items
                VideoNone.controlsSelected(vm);

                // Checked
                VideoNone.controlsChecked(vm);

                // Unhecked
                VideoNone.controlsUnhecked(vm);

                // Enabled
                VideoNone.controlsEnable(vm);

                // Disabled
                VideoNone.controlsDisable(vm);
            }

            // --------------------------------------------------
            // Default Selected Item
            // --------------------------------------------------
            // Previous Items set in ViewModel _SelectedItem

            // -------------------------
            // Video Encode Speed Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(vm.Video_EncodeSpeed_SelectedItem) &&
                vm.Video_EncodeSpeed_SelectedItem != "None" &&
                vm.Video_EncodeSpeed_SelectedItem != "none")
            {
                MainWindow.Video_EncodeSpeed_PreviousItem = vm.Video_EncodeSpeed_SelectedItem;
            }

            vm.Video_EncodeSpeed_SelectedItem = MainWindow.SelectedItem(vm.Video_EncodeSpeed_Items.Select(c => c.Name).ToList(),
                                                                       MainWindow.Video_EncodeSpeed_PreviousItem
                                                                       );

            // -------------------------
            // Video Quality Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(vm.Video_Quality_SelectedItem) &&
                vm.Video_Quality_SelectedItem != "None" &&
                vm.Video_Quality_SelectedItem != "none")
            {
                MainWindow.Video_Quality_PreviousItem = vm.Video_Quality_SelectedItem;
            }

            vm.Video_Quality_SelectedItem = MainWindow.SelectedItem(vm.Video_Quality_Items.Select(c => c.Name).ToList(),
                                                                   MainWindow.Video_Quality_PreviousItem
                                                                   );

            // -------------------------
            // Video Pass Selected Item
            // -------------------------
            //if (!string.IsNullOrEmpty(vm.Video_Pass_SelectedItem))
            //{
            //    MainWindow.Video_EncodeSpeed_PreviousItem = vm.Video_Pass_SelectedItem;
            //}

            //vm.Video_Pass_SelectedItem = MainWindow.SelectedItem(vm.Video_Pass_Items,
            //                                               MainWindow.Video_Pass_PreviousItem
            //                                               );

            // -------------------------
            // Video Optimize Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(vm.Video_Optimize_SelectedItem) &&
                vm.Video_Optimize_SelectedItem != "None" &&
                vm.Video_Optimize_SelectedItem != "none")
            {
                MainWindow.VideoOptimize_PreviousItem = vm.Video_Optimize_SelectedItem;
            }

            vm.Video_Optimize_SelectedItem = MainWindow.SelectedItem(vm.Video_Optimize_Items.Select(c => c.Name).ToList(),
                                                                     MainWindow.VideoOptimize_PreviousItem
                                                                     );
        }



        /// <summary>
        ///    Bitrate Display
        /// </summary>
        public static void VideoBitrateDisplay(ViewModel vm,
                                               List<ViewModel.VideoQuality> items,
                                               string selectedQuality,
                                               string selectedPass)
        {
            // Condition Check
            if (!string.IsNullOrEmpty(vm.Video_Quality_SelectedItem) &&
                vm.Video_Quality_SelectedItem != "Auto" &&
                vm.Video_Quality_SelectedItem != "Lossless" &&
                vm.Video_Quality_SelectedItem != "Custom" &&
                vm.Video_Quality_SelectedItem != "None")
            {
                // -------------------------
                // Display in TextBox
                // -------------------------

                // -------------------------
                // auto
                // -------------------------
                if (selectedPass == "auto")
                {
                    vm.Video_CRF_Text = string.Empty;
                    vm.Video_Bitrate_Text = string.Empty;
                    vm.Video_Minrate_Text = string.Empty;
                    vm.Video_Maxrate_Text = string.Empty;
                    vm.Video_Bufsize_Text = string.Empty;
                }

                // -------------------------
                // CRF
                // -------------------------
                else if (selectedPass == "CRF")
                {
                    // VP8/VP9 CRF is combined with Bitrate e.g. -b:v 2000K -crf 16
                    // Other Codecs just use CRF

                    // CRF Bitrate
                    vm.Video_Bitrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.Video_CRF_Bitrate;

                    // CRF
                    vm.Video_CRF_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.CRF;

                    // Minrate
                    vm.Video_Minrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.Minrate;

                    // Maxrate
                    vm.Video_Maxrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.Maxrate;

                    // Bufsize
                    vm.Video_Bufsize_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.Bufsize;
                }

                // -------------------------
                // Bitrate
                // -------------------------
                else if (selectedPass == "1 Pass" ||
                         selectedPass == "2 Pass")
                {
                    // CRF
                    vm.Video_CRF_Text = string.Empty;

                    // Bitrate CBR
                    if (vm.Video_VBR_IsChecked == false)
                    {
                        vm.Video_Bitrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.CBR;
                    }

                    // Bitrate VBR
                    else if (vm.Video_VBR_IsChecked == true)
                    {
                        vm.Video_Bitrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.VBR;
                    }

                    // Minrate
                    vm.Video_Minrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.Minrate;

                    // Maxrate
                    vm.Video_Maxrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.Maxrate;

                    // Bufsize
                    vm.Video_Bufsize_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.Bufsize;
                }
            }
        }


        /// <summary>
        ///     Quality Controls
        /// <summary>
        public static void QualityControls(ViewModel vm)
        {
            // -------------------------
            // Enable / Disable
            // -------------------------

            // -------------------------
            // Custom
            // -------------------------
            if (vm.Video_Quality_SelectedItem == "Custom")
            {
                // Enable and Clear Bitrate Text Display

                // Pass
                vm.Video_Pass_IsEnabled = true;

                // CRF
                if (vm.Video_Codec_SelectedItem != "JPEG" || // Special Rule
                    vm.Video_Codec_SelectedItem != "PNG" ||
                    vm.Video_Codec_SelectedItem != "WebP"
                    ) 
                {
                    vm.Video_CRF_IsEnabled = true;
                }
                vm.Video_CRF_Text = "";

                // Bitrate
                vm.Video_Bitrate_IsEnabled = true;
                vm.Video_Bitrate_Text = "";

                // VBR
                vm.Video_VBR_IsEnabled = true;

                // Minrate
                vm.Video_Minrate_IsEnabled = true;
                vm.Video_Minrate_Text = "";

                // Maxrate
                vm.Video_Maxrate_IsEnabled = true;
                vm.Video_Maxrate_Text = "";

                // Bufsize
                vm.Video_Bufsize_IsEnabled = true;
                vm.Video_Bufsize_Text = "";

                // Size
                vm.Video_Scale_IsEnabled = true;
            }

            // -------------------------
            // Auto
            // -------------------------
            else if (vm.Video_Quality_SelectedItem == "Auto")
            {
                // Disable and Clear Bitrate Text Dispaly

                // Pass
                vm.Video_Pass_IsEnabled = false;

                // CRF
                vm.Video_CRF_IsEnabled = false;
                vm.Video_CRF_Text = "";

                // Bitrate
                vm.Video_Bitrate_IsEnabled = false;
                vm.Video_Bitrate_Text = "";

                // VBR
                vm.Video_VBR_IsEnabled = false;
                vm.Video_VBR_IsChecked = false;

                // Minrate
                vm.Video_Minrate_IsEnabled = false;
                vm.Video_Minrate_Text = "";

                // Maxrate
                vm.Video_Maxrate_IsEnabled = false;
                vm.Video_Maxrate_Text = "";

                // Bufsize
                vm.Video_Bufsize_IsEnabled = false;
                vm.Video_Bufsize_Text = "";

                // Size
                vm.Video_Scale_IsEnabled = true;
            }

            // -------------------------
            // None
            // -------------------------
            else if (vm.Video_Quality_SelectedItem == "None")
            {
                // Bitrate Text is Displayed through VideoBitrateDisplay()

                // Pass
                vm.Video_Pass_IsEnabled = false; 

                // CRF
                vm.Video_CRF_IsEnabled = false;

                // Bitrate
                vm.Video_Bitrate_IsEnabled = false;
                // VBR
                vm.Video_VBR_IsEnabled = false;
                // Minrate
                vm.Video_Minrate_IsEnabled = false;
                // Maxrate
                vm.Video_Maxrate_IsEnabled = false;
                // Bufsize
                vm.Video_Bufsize_IsEnabled = false;

                // Size
                vm.Video_Scale_IsEnabled = false;
            }

            // -------------------------
            // All Other Qualities
            // -------------------------
            else
            {
                // Bitrate Text is Displayed through VideoBitrateDisplay()

                // Pass
                vm.Video_Pass_IsEnabled = true; // always enabled

                // CRF
                vm.Video_CRF_IsEnabled = false;

                // Bitrate
                vm.Video_Bitrate_IsEnabled = false;

                // VBR
                if (vm.Video_Codec_SelectedItem == "VP8" || // special rules
                    vm.Video_Codec_SelectedItem == "x264" ||
                    vm.Video_Codec_SelectedItem == "JPEG" ||
                    vm.Video_Codec_SelectedItem == "AV1" ||
                    vm.Video_Codec_SelectedItem == "FFV1" ||
                    vm.Video_Codec_SelectedItem == "HuffYUV" ||
                    vm.Video_Codec_SelectedItem == "Copy" ||
                    vm.Video_Codec_SelectedItem == "None") 
                {
                    // Disabled
                    vm.Video_VBR_IsEnabled = false;
                }
                else
                {
                    // Enabled
                    vm.Video_VBR_IsEnabled = true;
                }

                // Minrate
                vm.Video_Minrate_IsEnabled = false;

                // Maxrate
                vm.Video_Maxrate_IsEnabled = false;

                // Bufsize
                vm.Video_Bufsize_IsEnabled = false;

                // Size
                vm.Video_Scale_IsEnabled = true;

                // -------------------------
                // Pass - Default to CRF
                // -------------------------
                // Keep in Video SelectionChanged
                // If Video Not Auto and User Willingly Selected Pass is false

                // Check if CRF Exists in ComboBox
                if (vm.Video_Pass_Items?.Contains("CRF") == true)
                {
                    if (passUserSelected == false)
                    {
                        vm.Video_Pass_SelectedItem = "CRF";
                    }
                }
                // If does not contain, select first available (CRF or 1 Pass)
                else
                {
                    vm.Video_Pass_SelectedItem = vm.Video_Pass_Items.FirstOrDefault();
                }

            }
        }


        /// <summary>
        ///     Pixel Format Controls
        /// </summary>
        public static void PixelFormatControls(ViewModel vm, 
                                               string mediaType, 
                                               string codec, 
                                               string quality)
        {
            // -------------------------
            // MediaType
            // ------------------------- 
            if (mediaType == "Video" ||
                mediaType == "Image" ||
                mediaType == "Sequence")
            {
                // -------------------------
                // VP9
                // x264
                // x265
                // AV1
                // -------------------------
                if (codec == "VP9" ||
                    codec == "x264" ||
                    codec == "x265" ||
                    codec == "AV1"
                    )
                {
                    // Auto
                    //if (quality == "Auto")
                    //{
                    //    vm.Video_PixelFormat_SelectedItem = "yuv420p";
                    //}
                    // Lossless
                    if (quality == "Lossless")
                    {
                        vm.Video_PixelFormat_SelectedItem = "yuv444p";
                    }
                    // All Other Quality
                    else
                    {
                        vm.Video_PixelFormat_SelectedItem = "yuv420p";
                    }
                }

                // -------------------------
                // FFV1
                // -------------------------
                else if (codec == "FFV1")
                {
                    vm.Video_PixelFormat_SelectedItem = "yuv422p";
                }

                // -------------------------
                // HuffYUV
                // -------------------------
                else if (codec == "HuffYUV")
                {
                    vm.Video_PixelFormat_SelectedItem = "yuv422p10le";
                }

                // -------------------------
                // MPEG-2
                // MPEG-4
                // -------------------------
                else if (codec == "MPEG-2" ||
                         codec == "MPEG-4")
                {
                    // Lossless can't be yuv444p
                    // All Pixel Formats must be yuv420p
                    vm.Video_PixelFormat_SelectedItem = "yuv420p";
                }

                // -------------------------
                // WebP
                // -------------------------
                else if (codec == "WebP")
                {
                    vm.Video_PixelFormat_IsEnabled = true;

                    // Auto
                    //if (quality == "yuv420p")
                    //{
                    //    //vm.Video_PixelFormat_SelectedItem = "auto";
                    //}
                    // Lossless
                    if (quality == "Lossless")
                    {
                        vm.Video_PixelFormat_SelectedItem = "bgra";
                    }
                    // All Other Quality
                    else
                    {
                        vm.Video_PixelFormat_SelectedItem = "yuv420p";
                    }
                }

                // -------------------------
                // Excluded Codecs
                // -------------------------
                // JPEG
                // PNG
                // Copy
            }

            // -------------------------
            // Excluded Media Types
            // -------------------------
            // Audio
        }



        /// <summary>
        ///     Optimize Controls
        /// <summary>
        public static void OptimizeControls(ViewModel vm)
        {
            // -------------------------
            // Only for x264 & x265 Video Codecs
            // -------------------------
            if (vm.Video_Codec_SelectedItem == "x264" ||
                vm.Video_Codec_SelectedItem == "x265")
            {
                // -------------------------
                // Disable
                // -------------------------
                if (vm.Video_Optimize_SelectedItem == "None")
                {
                    // Tune
                    vm.Optimize_Tune_IsEnabled = false;

                    // Profile
                    vm.Optimize_Profile_IsEnabled = false;

                    // Level
                    vm.Video_Optimize_Level_IsEnabled = false;
                }

                // -------------------------
                // Enable
                // -------------------------
                // All Other Qualities
                else
                {
                    // Tune
                    vm.Optimize_Tune_IsEnabled = true;

                    // Profile
                    vm.Optimize_Profile_IsEnabled = true;

                    // Level
                    vm.Video_Optimize_Level_IsEnabled = true;
                }
            }

            // -------------------------
            // Disable Tune, Profile, Level if not x264 & x265
            // -------------------------
            else
            {
                // Tune
                vm.Optimize_Tune_IsEnabled = false;

                // Profile
                vm.Optimize_Profile_IsEnabled = false;

                // Level
                vm.Video_Optimize_Level_IsEnabled = false;
            }


            // -------------------------
            // Select Controls
            // -------------------------
            // Tune
            vm.Video_Optimize_Tune_SelectedItem = vm.Video_Optimize_Items.FirstOrDefault(item => item.Name == vm.Video_Optimize_SelectedItem)?.Tune;
            // Profile
            vm.Video_Optimize_Profile_SelectedItem = vm.Video_Optimize_Items.FirstOrDefault(item => item.Name == vm.Video_Optimize_SelectedItem)?.Profile;
            // Level
            vm.Video_Optimize_Level_SelectedItem = vm.Video_Optimize_Items.FirstOrDefault(item => item.Name == vm.Video_Optimize_SelectedItem)?.Level;

        }


        /// <summary>
        ///     Encoding Pass Controls
        /// <summary>
        public static void EncodingPassControls(ViewModel vm)
        {
            // --------------------------------------------------
            // Encoding Pass ComboBox
            // --------------------------------------------------

            // -------------------------
            // Auto
            // -------------------------
            if (vm.Video_Quality_SelectedItem == "Auto")
            {
                // Change Items Source
                vm.Video_Pass_Items = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                // -------------------------
                // Check if 2-Pass Exists in ComboBox
                // -------------------------
                if (vm.Video_Pass_Items?.Contains("2 Pass") == true)
                {
                    // -------------------------
                    // Disable 2-Pass Always
                    // -------------------------
                    if (vm.Video_Pass_SelectedItem == "2 Pass")
                    {
                        vm.Video_Pass_IsEnabled = false;

                        // Reset the User willing selected bool
                        passUserSelected = false;

                        // Set Codec Parameters
                        // 2 Pass Parameters -context 2
                        //if (vm.Video_Codec_SelectedItem == "HuffYUV")
                        //{
                        //    HuffYUV.codecParameters = "-context 2 -vstrict -2 -pred 2";
                        //}
                    }
                    // -------------------------
                    // Select and Disable 2-Pass
                    // -------------------------
                    else
                    {
                        vm.Video_Pass_SelectedItem = "2 Pass";
                        // Disable Pass ComboBox if 2-Pass
                        vm.Video_Pass_IsEnabled = false;

                        // Set Codec Parameters
                        // 2 Pass Parameters -context 2
                        //if (vm.Video_Codec_SelectedItem == "HuffYUV")
                        //{
                        //    HuffYUV.codecParameters = "-context 2 -vstrict -2 -pred 2";
                        //}
                    }
                }

                // -------------------------
                // If does not contain 2 Pass, select first available (CRF, 1 Pass, or auto)
                // -------------------------
                else
                {
                    vm.Video_Pass_SelectedItem = vm.Video_Pass_Items.FirstOrDefault();

                    // -------------------------
                    // Set Codec Parameters
                    // -------------------------
                    // 1 Pass Parameters -context 1
                    //if (vm.Video_Codec_SelectedItem == "HuffYUV")
                    //{
                    //    HuffYUV.codecParameters = "-context 1 -vstrict -2 -pred 2";
                    //}
                }
            }

            // -------------------------
            // Lossless
            // -------------------------
            else if (vm.Video_Quality_SelectedItem == "Lossless")
            {
                // Change Items Source
                vm.Video_Pass_Items = new List<string>()
                {
                    "1 Pass",
                    "2 Pass"
                };

                // Select Pass
                if (string.IsNullOrEmpty(vm.Video_Pass_SelectedItem))
                {
                    // Select 1 Pass
                    if (vm.Video_Pass_Items?.Contains("1 Pass") == true)
                    {
                        vm.Video_Pass_SelectedItem = "1 Pass";
                    }
                    // Default to First Item Available
                    else
                    {
                        vm.Video_Pass_SelectedItem = vm.Video_Pass_Items.FirstOrDefault();
                    }
                }

                //if (vm.Video_Pass_Items?.Contains("1 Pass") == true)
                //{
                //    vm.Video_Pass_SelectedItem = "1 Pass";

                //    //if (vm.Video_Codec_SelectedItem != "FFV1" || // Special Lossless Rule
                //    //    vm.Video_Codec_SelectedItem != "HuffYUV") 
                //    //{
                //    //    vm.Video_Pass_SelectedItem = "1 Pass";
                //    //}
                //}
                //// Default to First Item Available
                //else
                //{
                //    vm.Video_Pass_SelectedItem = vm.Video_Pass_Items.FirstOrDefault();
                //}

                // Enable/Disable Encoding Pass
                // Special Lossless Rule
                //if (vm.Video_Codec_SelectedItem == "FFV1" ||
                //    vm.Video_Codec_SelectedItem == "HuffYUV")
                //{
                //    vm.Video_Pass_IsEnabled = true;
                //}
                //// All Other Codecs
                //else
                //{
                //    vm.Video_Pass_IsEnabled = false;
                //}

                // Disable CRF
                vm.Video_CRF_IsEnabled = false;

                // Set CRF & Bitrate back to Default value
                vm.Video_CRF_Text = string.Empty;
                vm.Video_Bitrate_Text = string.Empty;
                vm.Video_Minrate_Text = string.Empty;
                vm.Video_Maxrate_Text = string.Empty;
                vm.Video_Bufsize_Text = string.Empty;
            }

            // -------------------------
            // Custom
            // -------------------------
            else if (vm.Video_Quality_SelectedItem == "Custom")
            {
                // Change Items Source
                vm.Video_Pass_Items = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                // -------------------------
                // Disable CRF TextBox if 1 Pass or 2 Pass
                // -------------------------
                if (vm.Video_Pass_SelectedItem == "1 Pass" ||
                    vm.Video_Pass_SelectedItem == "2 Pass")
                {
                    vm.Video_CRF_IsEnabled = false;
                    vm.Video_Pass_IsEnabled = true;

                    // Set CRF back to Default value
                    vm.Video_CRF_Text = string.Empty;
                }

                // -------------------------
                // Enable CRF TextBox if CRF
                // -------------------------
                else if (vm.Video_Pass_SelectedItem == "CRF")
                {
                    vm.Video_Pass_IsEnabled = true;
                    vm.Video_CRF_IsEnabled = true;

                    // Theora - Special Rule
                    if (vm.Video_Codec_SelectedItem == "Theora")
                    {
                        vm.Video_Pass_IsEnabled = true;
                        vm.Video_CRF_IsEnabled = false;
                    }
                }
            }

            // -------------------------
            // None
            // -------------------------
            else if (vm.Video_Quality_SelectedItem == "None")
            {
                // Change Items Source
                vm.Video_Pass_Items = new List<string>()
                {
                    "auto"
                };

                // Disable Pass
                vm.Video_Pass_SelectedItem = vm.Video_Pass_Items.FirstOrDefault();
                vm.Video_Pass_IsEnabled = false;
            }

            // -------------------------
            // All Other Quality
            // -------------------------
            else
            {
                vm.Video_Pass_Items = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                // Enable Pass
                vm.Video_Pass_IsEnabled = true;
            }
        }



        /// <summary>
        ///    Auto Copy Conditions Check
        /// <summary>
        public static bool AutoCopyConditionsCheck(
                                                   ViewModel vm, 
                                                   string inputExt, 
                                                   string outputExt)
        {
            //try // try/catch to prevent crash until mainwindow has been converted to viewmodel
            //{
            // Input Extension is Same as Output Extension and Video Quality is Auto
            if (vm.Video_Quality_SelectedItem == "Auto" &&
                vm.Video_Scale_SelectedItem == "Source" &&
                string.IsNullOrEmpty(CropWindow.crop) &&
                vm.Video_FPS_SelectedItem == "auto" &&
                vm.Video_PixelFormat_SelectedItem == "auto" &&
                vm.Video_Optimize_SelectedItem == "None" &&
                vm.Video_ScalingAlgorithm_SelectedItem == "default" &&

                // Filters
                // Fix
                vm.FilterVideo_Deband_SelectedItem == "disabled" &&
                vm.FilterVideo_Deshake_SelectedItem == "disabled" &&
                vm.FilterVideo_Deflicker_SelectedItem == "disabled" &&
                vm.FilterVideo_Dejudder_SelectedItem == "disabled" &&
                vm.FilterVideo_Denoise_SelectedItem == "disabled" &&
                // Selective Color
                // Reds
                vm.FilterVideo_SelectiveColor_Reds_Cyan_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Reds_Magenta_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Reds_Yellow_Value == 0 &&
                // Yellows
                vm.FilterVideo_SelectiveColor_Yellows_Cyan_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Yellows_Magenta_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Yellows_Yellow_Value == 0 &&
                // Greens
                vm.FilterVideo_SelectiveColor_Greens_Cyan_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Greens_Magenta_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Greens_Yellow_Value == 0 &&
                // Cyans
                vm.FilterVideo_SelectiveColor_Cyans_Cyan_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Cyans_Magenta_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Cyans_Yellow_Value == 0 &&
                // Blues
                vm.FilterVideo_SelectiveColor_Blues_Cyan_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Blues_Magenta_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Blues_Yellow_Value == 0 &&
                // Magentas
                vm.FilterVideo_SelectiveColor_Magentas_Cyan_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Magentas_Magenta_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Magentas_Yellow_Value == 0 &&
                // Whites
                vm.FilterVideo_SelectiveColor_Whites_Cyan_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Whites_Magenta_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Whites_Yellow_Value == 0 &&
                // Neutrals
                vm.FilterVideo_SelectiveColor_Neutrals_Cyan_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Neutrals_Magenta_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Neutrals_Yellow_Value == 0 &&
                // Blacks
                vm.FilterVideo_SelectiveColor_Blacks_Cyan_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Blacks_Magenta_Value == 0 &&
                vm.FilterVideo_SelectiveColor_Blacks_Yellow_Value == 0 &&

                // EQ
                vm.FilterVideo_EQ_Brightness_Value == 0 &&
                vm.FilterVideo_EQ_Contrast_Value == 0 &&
                vm.FilterVideo_EQ_Saturation_Value == 0 &&
                vm.FilterVideo_EQ_Gamma_Value == 0 &&

                // File Extension Match
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
            //}
            //catch
            //{
            //    return false;
            //}

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
                // Set Video Codec Combobox Selected Item to Copy
                // -------------------------
                if (vm.Video_Codec_Items.Count > 0)
                {
                    if (vm.Video_Codec_Items?.Contains("Copy") == true)
                    {
                        vm.Video_Codec_SelectedItem = "Copy";
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
                if (!string.IsNullOrEmpty(vm.Video_Quality_SelectedItem))
                {
                    // -------------------------
                    // Copy Selected
                    // -------------------------
                    if (vm.Video_Codec_SelectedItem == "Copy")
                    {
                        // -------------------------
                        // Switch back to format's default codec
                        // -------------------------
                        if (vm.Video_Codec_SelectedItem != "Auto" ||
                            !string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                            )
                        {
                            // -------------------------
                            // WebM
                            // -------------------------
                            if (vm.Format_Container_SelectedItem == "webm")
                            {
                                vm.Video_Codec_SelectedItem = "VP8";
                            }
                            // -------------------------
                            // MP4
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "mp4")
                            {
                                vm.Video_Codec_SelectedItem = "x264";
                            }
                            // -------------------------
                            // MKV
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "mkv")
                            {
                                vm.Video_Codec_SelectedItem = "x264";
                            }
                            // -------------------------
                            // MPG
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "mpg")
                            {
                                vm.Video_Codec_SelectedItem = "MPEG-2";
                            }
                            // -------------------------
                            // AVI
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "avi")
                            {
                                vm.Video_Codec_SelectedItem = "MPEG-4";
                            }
                            // -------------------------
                            // OGV
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "ogv")
                            {
                                vm.Video_Codec_SelectedItem = "Theora";
                            }
                            // -------------------------
                            // JPG
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "jpg")
                            {
                                vm.Video_Codec_SelectedItem = "JPEG";
                            }
                            // -------------------------
                            // PNG
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "png")
                            {
                                vm.Video_Codec_SelectedItem = "PNG";
                            }
                            // -------------------------
                            // WebP
                            // -------------------------
                            else if (vm.Format_Container_SelectedItem == "webp")
                            {
                                vm.Video_Codec_SelectedItem = "WebP";
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        ///    Auto Codec Copy
        /// <summary>
        /// <remarks>
        ///     Input Extension is Same as Output Extension and Video Quality is Auto
        /// </remarks>
        public static void AutoCopyVideoCodec(ViewModel vm)
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
                vm.Video_Codec_SelectedItem == "Copy")
            {
                CopyControls(vm);
            } 
        } 



    }
}
