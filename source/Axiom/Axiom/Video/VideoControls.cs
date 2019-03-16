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
        // Codec Class -> ComboBox Item Source -> Video Controls Class -> Pass to ViewModel
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
                // Video Codec
                vm.VideoCodec_Command = VP8.codec;

                // Item Source
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
                // Video Codec
                vm.VideoCodec_Command = VP9.codec;

                // Item Source
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
                // Video Codec
                vm.VideoCodec_Command = x264.codec;

                // Item Source
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
                // Video Codec
                vm.VideoCodec_Command = x265.codec;

                // Item Source
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
                // Video Codec
                vm.VideoCodec_Command = MPEG_2.codec;

                // Item Source
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
                // Video Codec
                vm.VideoCodec_Command = MPEG_4.codec;

                // Item Source
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
                // Video Codec
                vm.VideoCodec_Command = AV1.codec;

                // Item Source
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
                // Video Codec
                vm.VideoCodec_Command = FFV1.codec;

                // Item Source
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
            // Theora
            // -------------------------
            else if (codec_SelectedItem == "Theora")
            {
                // Video Codec
                vm.VideoCodec_Command = Theora.codec;

                // Item Source
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
                // Video Codec
                vm.VideoCodec_Command = JPEG.codec;

                // Item Source
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
                // Video Codec
                vm.VideoCodec_Command = PNG.codec;

                // Item Source
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
                // Video Codec
                vm.VideoCodec_Command = WebP.codec;

                // Item Source
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
                // Video Codec
                vm.VideoCodec_Command = VideoCopy.codec;

                // Item Source
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
                // Video Codec
                vm.VideoCodec_Command = VideoNone.codec;

                // Item Source
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
            if (!string.IsNullOrEmpty(vm.VideoEncodeSpeed_SelectedItem) &&
                vm.VideoEncodeSpeed_SelectedItem != "None" &&
                vm.VideoEncodeSpeed_SelectedItem != "none")
            {
                MainWindow.VideoEncodeSpeed_PreviousItem = vm.VideoEncodeSpeed_SelectedItem;
            }

            vm.VideoEncodeSpeed_SelectedItem = MainWindow.SelectedItem(vm.VideoEncodeSpeed_Items.Select(c => c.Name).ToList(),
                                                                       MainWindow.VideoEncodeSpeed_PreviousItem
                                                                       );

            // -------------------------
            // Video Quality Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(vm.VideoQuality_SelectedItem) &&
                vm.VideoQuality_SelectedItem != "None" &&
                vm.VideoQuality_SelectedItem != "none")
            {
                MainWindow.VideoQuality_PreviousItem = vm.VideoQuality_SelectedItem;
            }

            vm.VideoQuality_SelectedItem = MainWindow.SelectedItem(vm.VideoQuality_Items.Select(c => c.Name).ToList(),
                                                                   MainWindow.VideoQuality_PreviousItem
                                                                   );

            // -------------------------
            // Video Pass Selected Item
            // -------------------------
            //if (!string.IsNullOrEmpty(vm.Pass_SelectedItem))
            //{
            //    MainWindow.VideoEncodeSpeed_PreviousItem = vm.Pass_SelectedItem;
            //}

            //vm.Pass_SelectedItem = MainWindow.SelectedItem(vm.Pass_Items,
            //                                               MainWindow.Pass_PreviousItem
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
            if (!string.IsNullOrEmpty(vm.VideoQuality_SelectedItem) &&
                vm.VideoQuality_SelectedItem != "Auto" &&
                vm.VideoQuality_SelectedItem != "Lossless" &&
                vm.VideoQuality_SelectedItem != "Custom" &&
                vm.VideoQuality_SelectedItem != "None")
            {
                // -------------------------
                // Display in TextBox
                // -------------------------

                // -------------------------
                // auto
                // -------------------------
                if (selectedPass == "auto")
                {
                    vm.CRF_Text = string.Empty;
                    vm.VideoBitrate_Text = string.Empty;
                    vm.VideoMinrate_Text = string.Empty;
                    vm.VideoMaxrate_Text = string.Empty;
                    vm.VideoBufsize_Text = string.Empty;
                }

                // -------------------------
                // CRF
                // -------------------------
                else if (selectedPass == "CRF")
                {
                    // VP8/VP9 CRF is combined with Bitrate e.g. -b:v 2000K -crf 16
                    // Other Codecs just use CRF

                    // CRF Bitrate
                    vm.VideoBitrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.CRF_Bitrate;

                    // CRF
                    vm.CRF_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.CRF;

                    // Minrate
                    vm.VideoMinrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.Minrate;

                    // Maxrate
                    vm.VideoMaxrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.Maxrate;

                    // Bufsize
                    vm.VideoBufsize_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.Bufsize;
                }

                // -------------------------
                // Bitrate
                // -------------------------
                else if (selectedPass == "1 Pass" ||
                         selectedPass == "2 Pass")
                {
                    // CRF
                    vm.CRF_Text = string.Empty;

                    // Bitrate CBR
                    if (vm.VideoVBR_IsChecked == false)
                    {
                        vm.VideoBitrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.CBR;
                    }

                    // Bitrate VBR
                    else if (vm.VideoVBR_IsChecked == true)
                    {
                        vm.VideoBitrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.VBR;
                    }

                    // Minrate
                    vm.VideoMinrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.Minrate;

                    // Maxrate
                    vm.VideoMaxrate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.Maxrate;

                    // Bufsize
                    vm.VideoBufsize_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.Bufsize;
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
            if (vm.VideoQuality_SelectedItem == "Custom")
            {
                // Enable and Clear Bitrate Text Display

                // Pass
                vm.Pass_IsEnabled = true;

                // CRF
                if (vm.VideoCodec_SelectedItem != "JPEG" || // Special Rule
                    vm.VideoCodec_SelectedItem != "PNG" ||
                    vm.VideoCodec_SelectedItem != "WebP"
                    ) 
                {
                    vm.CRF_IsEnabled = true;
                }
                vm.CRF_Text = "";

                // Bitrate
                vm.VideoBitrate_IsEnabled = true;
                vm.VideoBitrate_Text = "";

                // VBR
                vm.VideoVBR_IsEnabled = true;

                // Minrate
                vm.VideoMinrate_IsEnabled = true;
                vm.VideoMinrate_Text = "";

                // Maxrate
                vm.VideoMaxrate_IsEnabled = true;
                vm.VideoMaxrate_Text = "";

                // Bufsize
                vm.VideoBufsize_IsEnabled = true;
                vm.VideoBufsize_Text = "";

                // Size
                vm.Size_IsEnabled = true;
            }

            // -------------------------
            // Auto
            // -------------------------
            else if (vm.VideoQuality_SelectedItem == "Auto")
            {
                // Disable and Clear Bitrate Text Dispaly

                // Pass
                vm.Pass_IsEnabled = false;

                // CRF
                vm.CRF_IsEnabled = false;
                vm.CRF_Text = "";

                // Bitrate
                vm.VideoBitrate_IsEnabled = false;
                vm.VideoBitrate_Text = "";

                // VBR
                vm.VideoVBR_IsEnabled = false;

                // Minrate
                vm.VideoMinrate_IsEnabled = false;
                vm.VideoMinrate_Text = "";

                // Maxrate
                vm.VideoMaxrate_IsEnabled = false;
                vm.VideoMaxrate_Text = "";

                // Bufsize
                vm.VideoBufsize_IsEnabled = false;
                vm.VideoBufsize_Text = "";

                // Size
                vm.Size_IsEnabled = true;
            }

            // -------------------------
            // None
            // -------------------------
            else if (vm.VideoQuality_SelectedItem == "None")
            {
                // Bitrate Text is Displayed through VideoBitrateDisplay()

                // Pass
                vm.Pass_IsEnabled = false; 

                // CRF
                vm.CRF_IsEnabled = false;

                // Bitrate
                vm.VideoBitrate_IsEnabled = false;
                // VBR
                vm.VideoVBR_IsEnabled = false;
                // Minrate
                vm.VideoMinrate_IsEnabled = false;
                // Maxrate
                vm.VideoMaxrate_IsEnabled = false;
                // Bufsize
                vm.VideoBufsize_IsEnabled = false;

                // Size
                vm.Size_IsEnabled = false;
            }

            // -------------------------
            // All Other Qualities
            // -------------------------
            else
            {
                // Bitrate Text is Displayed through VideoBitrateDisplay()

                // Pass
                vm.Pass_IsEnabled = true; // always enabled

                // CRF
                vm.CRF_IsEnabled = false;

                // Bitrate
                vm.VideoBitrate_IsEnabled = false;

                // VBR
                if (vm.VideoCodec_SelectedItem == "VP8" || // special rules
                    vm.VideoCodec_SelectedItem == "x264" ||
                    vm.VideoCodec_SelectedItem == "JPEG" ||
                    vm.VideoCodec_SelectedItem == "AV1" ||
                    vm.VideoCodec_SelectedItem == "FFV1" ||
                    vm.VideoCodec_SelectedItem == "Copy" ||
                    vm.VideoCodec_SelectedItem == "None") 
                {
                    // Disabled
                    vm.VideoVBR_IsEnabled = false;
                }
                else
                {
                    // Enabled
                    vm.VideoVBR_IsEnabled = true;
                }

                // Minrate
                vm.VideoMinrate_IsEnabled = false;

                // Maxrate
                vm.VideoMaxrate_IsEnabled = false;

                // Bufsize
                vm.VideoBufsize_IsEnabled = false;

                // Size
                vm.Size_IsEnabled = true;

                // -------------------------
                // Pass - Default to CRF
                // -------------------------
                // Keep in Video SelectionChanged
                // If Video Not Auto and User Willingly Selected Pass is false

                // Check if CRF Exists in ComboBox
                if (vm.Pass_Items?.Contains("CRF") == true)
                {
                    if (passUserSelected == false)
                    {
                        vm.Pass_SelectedItem = "CRF";
                    }
                }
                // If does not contain, select first available (CRF or 1 Pass)
                else
                {
                    vm.Pass_SelectedItem = vm.Pass_Items.FirstOrDefault();
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
                // FFV1
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
                    //    vm.PixelFormat_SelectedItem = "yuv420p";
                    //}
                    // Lossless
                    if (quality == "Lossless")
                    {
                        vm.PixelFormat_SelectedItem = "yuv444p";
                    }
                    // All Other Quality
                    else
                    {
                        vm.PixelFormat_SelectedItem = "yuv420p";
                    }
                }

                // -------------------------
                // FFV1
                // -------------------------
                else if (codec == "FFV1")
                {
                    vm.PixelFormat_SelectedItem = "yuv422p10le";
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
                    vm.PixelFormat_SelectedItem = "yuv420p";
                }

                // -------------------------
                // WebP
                // -------------------------
                else if (codec == "WebP")
                {
                    vm.PixelFormat_IsEnabled = true;

                    // Auto
                    //if (quality == "yuv420p")
                    //{
                    //    //vm.PixelFormat_SelectedItem = "auto";
                    //}
                    // Lossless
                    if (quality == "Lossless")
                    {
                        vm.PixelFormat_SelectedItem = "bgra";
                    }
                    // All Other Quality
                    else
                    {
                        vm.PixelFormat_SelectedItem = "yuv420p";
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
            if (vm.VideoCodec_SelectedItem == "x264" ||
                vm.VideoCodec_SelectedItem == "x265")
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
                    vm.Optimize_Level_IsEnabled = false;
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
                    vm.Optimize_Level_IsEnabled = true;
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
                vm.Optimize_Level_IsEnabled = false;
            }


            // -------------------------
            // Select Controls
            // -------------------------
            // Tune
            vm.Optimize_Tune_SelectedItem = vm.Video_Optimize_Items.FirstOrDefault(item => item.Name == vm.Video_Optimize_SelectedItem)?.Tune;
            // Profile
            vm.Optimize_Profile_SelectedItem = vm.Video_Optimize_Items.FirstOrDefault(item => item.Name == vm.Video_Optimize_SelectedItem)?.Profile;
            // Level
            vm.Optimize_Level_SelectedItem = vm.Video_Optimize_Items.FirstOrDefault(item => item.Name == vm.Video_Optimize_SelectedItem)?.Level;

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
            if (vm.VideoQuality_SelectedItem == "Auto")
            {
                // -------------------------
                // Check if 2-Pass Exists in ComboBox
                // -------------------------
                if (vm.Pass_Items?.Contains("2 Pass") == true)
                {
                    // -------------------------
                    // Disable 2-Pass Always
                    // -------------------------
                    if (vm.Pass_SelectedItem == "2 Pass")
                    {
                        vm.Pass_IsEnabled = false;

                        // Reset the User willing selected bool
                        passUserSelected = false;
                    }
                    // -------------------------
                    // Select and Disable 2-Pass
                    // -------------------------
                    else
                    {
                        vm.Pass_SelectedItem = "2 Pass";
                        // Disable Pass ComboBox if 2-Pass
                        vm.Pass_IsEnabled = false;
                    }
                }

                // -------------------------
                // If does not contain 2 Pass, select first available (CRF, 1 Pass, or auto)
                // -------------------------
                else
                {
                    vm.Pass_SelectedItem = vm.Pass_Items.FirstOrDefault();
                }
            }

            // -------------------------
            // Lossless
            // -------------------------
            else if (vm.VideoQuality_SelectedItem == "Lossless")
            {
                // Select 1 Pass
                if (vm.Pass_Items?.Contains("1 Pass") == true)
                {
                    if (vm.VideoCodec_SelectedItem != "FFV1") // FFV1 (Special Lossless Rule)
                    {
                        vm.Pass_SelectedItem = "1 Pass";
                    }
                }
                // Default to First Item Available
                else
                {
                    vm.Pass_SelectedItem = vm.Pass_Items.FirstOrDefault();
                }

                // Enable/Disable Encoding Pass
                // FFV1 (Special Lossless Rule)
                if (vm.VideoCodec_SelectedItem == "FFV1")
                {
                    vm.Pass_IsEnabled = true;
                }
                // All Other Codecs
                else
                {
                    vm.Pass_IsEnabled = false;
                }
                
                // Disable CRF
                vm.CRF_IsEnabled = false;

                // Set CRF & Bitrate back to Default value
                vm.CRF_Text = string.Empty;
                vm.VideoBitrate_Text = string.Empty;
                vm.VideoMinrate_Text = string.Empty;
                vm.VideoMaxrate_Text = string.Empty;
                vm.VideoBufsize_Text = string.Empty;
            }

            // -------------------------
            // Custom
            // -------------------------
            else if (vm.VideoQuality_SelectedItem == "Custom")
            {
                // -------------------------
                // Disable CRF TextBox if 1 Pass or 2 Pass
                // -------------------------
                if (vm.Pass_SelectedItem == "1 Pass" ||
                    vm.Pass_SelectedItem == "2 Pass")
                {
                    vm.CRF_IsEnabled = false;
                    vm.Pass_IsEnabled = true;

                    // Set CRF back to Default value
                    vm.CRF_Text = string.Empty;
                }

                // -------------------------
                // Enable CRF TextBox if CRF
                // -------------------------
                else if (vm.Pass_SelectedItem == "CRF")
                {
                    vm.Pass_IsEnabled = true;
                    vm.CRF_IsEnabled = true;

                    // Theora - Special Rule
                    if (vm.VideoCodec_SelectedItem == "Theora")
                    {
                        vm.Pass_IsEnabled = true;
                        vm.CRF_IsEnabled = false;
                    }
                }
            }

            // -------------------------
            // None
            // -------------------------
            else if (vm.VideoQuality_SelectedItem == "None")
            {
                // Disable Pass
                vm.Pass_SelectedItem = vm.Pass_Items.FirstOrDefault();
                vm.Pass_IsEnabled = false;
            }

            // -------------------------
            // All Other Quality
            // -------------------------
            else
            {
                // Enable Pass
                vm.Pass_IsEnabled = true;
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
            if (vm.VideoQuality_SelectedItem == "Auto" &&
                vm.Size_SelectedItem == "Source" &&
                string.IsNullOrEmpty(CropWindow.crop) &&
                vm.FPS_SelectedItem == "auto" &&
                vm.PixelFormat_SelectedItem == "auto" &&
                vm.Video_Optimize_SelectedItem == "None" &&
                vm.ScalingAlgorithm_SelectedItem == "default" &&

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
                if (vm.VideoCodec_Items.Count > 0)
                {
                    if (vm.VideoCodec_Items?.Contains("Copy") == true)
                    {
                        vm.VideoCodec_SelectedItem = "Copy";
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
                if (!string.IsNullOrEmpty(vm.VideoQuality_SelectedItem))
                {
                    // -------------------------
                    // Copy Selected
                    // -------------------------
                    if (vm.VideoCodec_SelectedItem == "Copy")
                    {
                        // -------------------------
                        // Switch back to format's default codec
                        // -------------------------
                        if (vm.VideoCodec_SelectedItem != "Auto" ||
                            !string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                            )
                        {
                            // -------------------------
                            // WebM
                            // -------------------------
                            if (vm.Container_SelectedItem == "webm")
                            {
                                vm.VideoCodec_SelectedItem = "VP8";
                            }
                            // -------------------------
                            // MP4
                            // -------------------------
                            else if (vm.Container_SelectedItem == "mp4")
                            {
                                vm.VideoCodec_SelectedItem = "x264";
                            }
                            // -------------------------
                            // MKV
                            // -------------------------
                            else if (vm.Container_SelectedItem == "mkv")
                            {
                                vm.VideoCodec_SelectedItem = "x264";
                            }
                            // -------------------------
                            // MPG
                            // -------------------------
                            else if (vm.Container_SelectedItem == "mpg")
                            {
                                vm.VideoCodec_SelectedItem = "MPEG-2";
                            }
                            // -------------------------
                            // AVI
                            // -------------------------
                            else if (vm.Container_SelectedItem == "avi")
                            {
                                vm.VideoCodec_SelectedItem = "MPEG-4";
                            }
                            // -------------------------
                            // OGV
                            // -------------------------
                            else if (vm.Container_SelectedItem == "ogv")
                            {
                                vm.VideoCodec_SelectedItem = "Theora";
                            }
                            // -------------------------
                            // JPG
                            // -------------------------
                            else if (vm.Container_SelectedItem == "jpg")
                            {
                                vm.VideoCodec_SelectedItem = "JPEG";
                            }
                            // -------------------------
                            // PNG
                            // -------------------------
                            else if (vm.Container_SelectedItem == "png")
                            {
                                vm.VideoCodec_SelectedItem = "PNG";
                            }
                            // -------------------------
                            // WebP
                            // -------------------------
                            else if (vm.Container_SelectedItem == "webp")
                            {
                                vm.VideoCodec_SelectedItem = "WebP";
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
                vm.VideoCodec_SelectedItem == "Copy")
            {
                CopyControls(vm);
            } 
        } 



    }
}
