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

        //public static void VideoContainerDefaults(ViewModel vm)
        //{
        //    // -------------------------
        //    // WebM
        //    // -------------------------
        //    if (vm.Container_SelectedItem == "webm")
        //    {
        //        vm.VideoCodec_SelectedItem = "VP8";
        //    }
        //    // -------------------------
        //    // MP4
        //    // -------------------------
        //    else if (vm.Container_SelectedItem == "mp4")
        //    {
        //        vm.VideoCodec_SelectedItem = "x264";
        //    }
        //    // -------------------------
        //    // MKV
        //    // -------------------------
        //    else if (vm.Container_SelectedItem == "mkv")
        //    {
        //        vm.VideoCodec_SelectedItem = "x264";
        //    }
        //    // -------------------------
        //    // MPG
        //    // -------------------------
        //    else if (vm.Container_SelectedItem == "mpg")
        //    {
        //        vm.VideoCodec_SelectedItem = "MPEG-2";
        //    }
        //    // -------------------------
        //    // AVI
        //    // -------------------------
        //    else if (vm.Container_SelectedItem == "avi")
        //    {
        //        vm.VideoCodec_SelectedItem = "MPEG-4";
        //    }
        //    // -------------------------
        //    // OGV
        //    // -------------------------
        //    else if (vm.Container_SelectedItem == "ogv")
        //    {
        //        vm.VideoCodec_SelectedItem = "Theora";
        //    }
        //    // -------------------------
        //    // JPG
        //    // -------------------------
        //    else if (vm.Container_SelectedItem == "jpg")
        //    {
        //        vm.VideoCodec_SelectedItem = "JPEG";
        //    }
        //    // -------------------------
        //    // PNG
        //    // -------------------------
        //    else if (vm.Container_SelectedItem == "png")
        //    {
        //        vm.VideoCodec_SelectedItem = "PNG";
        //    }
        //    // -------------------------
        //    // WebP
        //    // -------------------------
        //    else if (vm.Container_SelectedItem == "webp")
        //    {
        //        vm.VideoCodec_SelectedItem = "WebP";
        //    }
        //}



        /// <summary>
        ///     Set Controls
        /// </summary>
        public static void SetControls(ViewModel vm, string selectedCodec)
        {
            // --------------------------------------------------
            // Codec
            // --------------------------------------------------

            // -------------------------
            // VP8
            // -------------------------
            if (selectedCodec == "VP8")
            {
                // Codec
                vm.VideoCodec_Command = VP8.codec;

                // Encode Speed
                vm.VideoEncodeSpeed_Items = VP8.encodeSpeed;
                //if (vm.VideoEncodeSpeed_SelectedItem == "None")
                //{
                //    vm.VideoEncodeSpeed_SelectedItem = "Medium";
                //}

                // Pass
                vm.Pass_Items = VP8.pass;

                // Quality Items
                vm.VideoQuality_Items = VP8.quality;

                // Pixel Format
                vm.PixelFormat_SelectedItem = VP8.pixfmt;

                // Framerate
                vm.FPS_SelectedItem = VP8.fps;

                // Optimize
                vm.Video_Optimize_Items = VP8.optimize;
                // Tune
                vm.Optimize_Tune_Items = VP8.tune;
                // Profile
                vm.Optimize_Profile_Items = VP8.profile;
                // Level
                vm.Optimize_Level_Items = VP8.level;

                // Checked
                VP8.controlsChecked(vm);

                // Enabled
                VP8.controlsEnable(vm);

                // Disabled
                VP8.controlsDisable(vm);
            }

            // -------------------------
            // VP9
            // -------------------------
            else if (selectedCodec == "VP9")
            {
                // Codec
                vm.VideoCodec_Command = VP9.codec;

                // Encode Speed
                vm.VideoEncodeSpeed_Items = VP9.encodeSpeed;

                // Pass
                vm.Pass_Items = VP9.pass;

                // Quality Items
                vm.VideoQuality_Items = VP9.quality;

                // Pixel Format
                vm.PixelFormat_SelectedItem = VP9.pixfmt;

                // Framerate
                vm.FPS_SelectedItem = VP9.fps;

                // Optimize
                vm.Video_Optimize_Items = VP9.optimize;
                // Tune
                vm.Optimize_Tune_Items = VP9.tune;
                // Profile
                vm.Optimize_Profile_Items = VP9.profile;
                // Level
                vm.Optimize_Level_Items = VP9.level;

                // Checked
                VP9.controlsChecked(vm);

                // Enabled
                VP9.controlsEnable(vm);

                // Disabled
                VP9.controlsDisable(vm);
            }

            // -------------------------
            // x264
            // -------------------------
            else if (selectedCodec == "x264")
            {
                // Codec
                vm.VideoCodec_Command = x264.codec;

                // Encode Speed
                vm.VideoEncodeSpeed_Items = x264.encodeSpeed;

                // Pass
                vm.Pass_Items = x264.pass;

                // Quality Items
                vm.VideoQuality_Items = x264.quality;

                // Pixel Format
                vm.PixelFormat_SelectedItem = x264.pixfmt;

                // Framerate
                vm.FPS_SelectedItem = x264.fps;

                // Optimize
                vm.Video_Optimize_Items = x264.optimize;
                // Tune
                vm.Optimize_Tune_Items = x264.tune;
                // Profile
                vm.Optimize_Profile_Items = x264.profile;
                // Level
                vm.Optimize_Level_Items = x264.level;

                // Checked
                x264.controlsChecked(vm);

                // Enabled
                x264.controlsEnable(vm);

                // Disabled
                x264.controlsDisable(vm);
            }

            // -------------------------
            // x265
            // -------------------------
            else if (selectedCodec == "x265")
            {
                // Codec
                vm.VideoCodec_Command = x265.codec;

                // Encode Speed
                vm.VideoEncodeSpeed_Items = x265.encodeSpeed;

                // Pass
                vm.Pass_Items = x265.pass;

                // Quality Items
                vm.VideoQuality_Items = x265.quality;

                // Pixel Format
                vm.PixelFormat_SelectedItem = x265.pixfmt;

                // Framerate
                vm.FPS_SelectedItem = x265.fps;

                // Optimize
                vm.Video_Optimize_Items = x265.optimize;
                // Tune
                vm.Optimize_Tune_Items = x265.tune;
                // Profile
                vm.Optimize_Profile_Items = x265.profile;
                // Level
                vm.Optimize_Level_Items = x265.level;

                // Checked
                x265.controlsChecked(vm);

                // Enabled
                x265.controlsEnable(vm);

                // Disabled
                x265.controlsDisable(vm);
            }

            // -------------------------
            // MPEG-2
            // -------------------------
            else if (selectedCodec == "MPEG-2")
            {
                // Codec
                vm.VideoCodec_Command = MPEG_2.codec;

                // Encode Speed
                vm.VideoEncodeSpeed_Items = MPEG_2.encodeSpeed;

                // Pass
                vm.Pass_Items = MPEG_2.pass;

                // Quality Items
                vm.VideoQuality_Items = MPEG_2.quality;

                // Pixel Format
                vm.PixelFormat_SelectedItem = MPEG_2.pixfmt;

                // Framerate
                vm.FPS_SelectedItem = MPEG_2.fps;

                // Optimize
                vm.Video_Optimize_Items = MPEG_2.optimize;
                // Tune
                vm.Optimize_Tune_Items = MPEG_2.tune;
                // Profile
                vm.Optimize_Profile_Items = MPEG_2.profile;
                // Level
                vm.Optimize_Level_Items = MPEG_2.level;

                // Checked
                MPEG_2.controlsChecked(vm);

                // Enabled
                MPEG_2.controlsEnable(vm);

                // Disabled
                MPEG_2.controlsDisable(vm);
            }

            // -------------------------
            // MPEG-4
            // -------------------------
            else if (selectedCodec == "MPEG-4")
            {
                // Codec
                vm.VideoCodec_Command = MPEG_4.codec;

                // Encode Speed
                vm.VideoEncodeSpeed_Items = MPEG_4.encodeSpeed;

                // Pass
                vm.Pass_Items = MPEG_4.pass;

                // Quality Items
                vm.VideoQuality_Items = MPEG_4.quality;

                // Pixel Format
                vm.PixelFormat_SelectedItem = MPEG_4.pixfmt;

                // Framerate
                vm.FPS_SelectedItem = MPEG_4.fps;

                // Optimize
                vm.Video_Optimize_Items = MPEG_4.optimize;
                // Tune
                vm.Optimize_Tune_Items = MPEG_4.tune;
                // Profile
                vm.Optimize_Profile_Items = MPEG_4.profile;
                // Level
                vm.Optimize_Level_Items = MPEG_4.level;

                // Checked
                MPEG_4.controlsChecked(vm);

                // Enabled
                MPEG_4.controlsEnable(vm);

                // Disabled
                MPEG_4.controlsDisable(vm);
            }

            // -------------------------
            // AV1
            // -------------------------
            else if (selectedCodec == "AV1")
            {
                // Codec
                vm.VideoCodec_Command = AV1.codec;

                // Encode Speed
                vm.VideoEncodeSpeed_Items = AV1.encodeSpeed;

                // Pass
                vm.Pass_Items = AV1.pass;

                // Quality Items
                vm.VideoQuality_Items = AV1.quality;

                // Pixel Format
                vm.PixelFormat_SelectedItem = AV1.pixfmt;

                // Framerate
                vm.FPS_SelectedItem = AV1.fps;

                // Optimize
                vm.Video_Optimize_Items = AV1.optimize;
                // Tune
                vm.Optimize_Tune_Items = AV1.tune;
                // Profile
                vm.Optimize_Profile_Items = AV1.profile;
                // Level
                vm.Optimize_Level_Items = AV1.level;

                // Checked
                AV1.controlsChecked(vm);

                // Enabled
                AV1.controlsEnable(vm);

                // Disabled
                AV1.controlsDisable(vm);
            }

            // -------------------------
            // Theora
            // -------------------------
            else if (selectedCodec == "Theora")
            {
                // Codec
                vm.VideoCodec_Command = Theora.codec;

                // Encode Speed
                vm.VideoEncodeSpeed_Items = Theora.encodeSpeed;

                // Pass
                vm.Pass_Items = Theora.pass;
                //vm.Pass_SelectedItem = "1 Pass";

                // Quality Items
                vm.VideoQuality_Items = Theora.quality;

                // Pixel Format
                vm.PixelFormat_SelectedItem = Theora.pixfmt;

                // Framerate
                vm.FPS_SelectedItem = Theora.fps;

                // Optimize
                vm.Video_Optimize_Items = Theora.optimize;
                // Tune
                vm.Optimize_Tune_Items = Theora.tune;
                // Profile
                vm.Optimize_Profile_Items = Theora.profile;
                // Level
                vm.Optimize_Level_Items = Theora.level;

                // Checked
                Theora.controlsChecked(vm);

                // Enabled
                Theora.controlsEnable(vm);

                // Disabled
                Theora.controlsDisable(vm);
            }

            // -------------------------
            // JPEG
            // -------------------------
            else if (selectedCodec == "JPEG")
            {
                // Codec
                vm.VideoCodec_Command = JPEG.codec;

                // Encode Speed
                vm.VideoEncodeSpeed_Items = JPEG.encodeSpeed;

                // Pass
                vm.Pass_Items = JPEG.pass;

                // Quality Items
                vm.VideoQuality_Items = JPEG.quality;

                // Pixel Format
                vm.PixelFormat_SelectedItem = JPEG.pixfmt;

                // Framerate
                vm.FPS_SelectedItem = JPEG.fps;

                // Optimize
                vm.Video_Optimize_Items = JPEG.optimize;
                // Tune
                vm.Optimize_Tune_Items = JPEG.tune;
                // Profile
                vm.Optimize_Profile_Items = JPEG.profile;
                // Level
                vm.Optimize_Level_Items = JPEG.level;

                // Checked
                JPEG.controlsChecked(vm);

                // Enabled
                JPEG.controlsEnable(vm);

                // Disabled
                JPEG.controlsDisable(vm);
            }

            // -------------------------
            // PNG
            // -------------------------
            else if (selectedCodec == "PNG")
            {
                // Codec
                vm.VideoCodec_Command = PNG.codec;

                // Encode Speed
                vm.VideoEncodeSpeed_Items = PNG.encodeSpeed;

                // Pass
                vm.Pass_Items = PNG.pass;

                // Quality Items
                vm.VideoQuality_Items = PNG.quality;

                // Pixel Format
                vm.PixelFormat_SelectedItem = PNG.pixfmt;

                // Framerate
                vm.FPS_SelectedItem = PNG.fps;

                // Optimize
                vm.Video_Optimize_Items = PNG.optimize;
                // Tune
                vm.Optimize_Tune_Items = PNG.tune;
                // Profile
                vm.Optimize_Profile_Items = PNG.profile;
                // Level
                vm.Optimize_Level_Items = PNG.level;

                // Checked
                PNG.controlsChecked(vm);

                // Enabled
                PNG.controlsEnable(vm);

                // Disabled
                PNG.controlsDisable(vm);
            }

            // -------------------------
            // WebP
            // -------------------------
            else if (selectedCodec == "WebP")
            {
                // Codec
                vm.VideoCodec_Command = WebP.codec;

                // Encode Speed
                vm.VideoEncodeSpeed_Items = WebP.encodeSpeed;

                // Pass
                vm.Pass_Items = WebP.pass;

                // Quality Items
                vm.VideoQuality_Items = WebP.quality;

                // Pixel Format
                vm.PixelFormat_SelectedItem = WebP.pixfmt;

                // Framerate
                vm.FPS_SelectedItem = WebP.fps;

                // Optimize
                vm.Video_Optimize_Items = WebP.optimize;
                // Tune
                vm.Optimize_Tune_Items = WebP.tune;
                // Profile
                vm.Optimize_Profile_Items = WebP.profile;
                // Level
                vm.Optimize_Level_Items = WebP.level;

                // Checked
                WebP.controlsChecked(vm);

                // Enabled
                WebP.controlsEnable(vm);

                // Disabled
                WebP.controlsDisable(vm);
            }

            // -------------------------
            // Copy
            // -------------------------
            else if (selectedCodec == "Copy")
            {
                // Codec
                vm.VideoCodec_Command = VideoCopy.codec;

                // Encode Speed
                vm.VideoEncodeSpeed_Items = VideoCopy.encodeSpeed;

                // Pass
                vm.Pass_Items = VideoCopy.pass;

                // Quality Items
                vm.VideoQuality_Items = VideoCopy.quality;

                // Pixel Format
                vm.PixelFormat_SelectedItem = VideoCopy.pixfmt;

                // Framerate
                vm.FPS_SelectedItem = VideoCopy.fps;

                // Optimize
                vm.Video_Optimize_Items = VideoCopy.optimize;
                // Tune
                vm.Optimize_Tune_Items = VideoCopy.tune;
                // Profile
                vm.Optimize_Profile_Items = VideoCopy.profile;
                // Level
                vm.Optimize_Level_Items = VideoCopy.level;

                // Checked
                VideoCopy.controlsChecked(vm);

                // Enabled
                VideoCopy.controlsEnable(vm);

                // Disabled
                VideoCopy.controlsDisable(vm);
            }

            // -------------------------
            // None
            // -------------------------
            else if (selectedCodec == "None")
            {
                // Codec
                vm.VideoCodec_Command = VideoNone.codec;

                // Encode Speed
                vm.VideoEncodeSpeed_Items = VideoNone.encodeSpeed;

                // Pass
                vm.Pass_Items = VideoNone.pass;

                // Quality Items
                vm.VideoQuality_Items = VideoNone.quality;

                // Pixel Format
                vm.PixelFormat_SelectedItem = VideoNone.pixfmt;

                // Framerate
                vm.FPS_SelectedItem = VideoNone.fps;

                // Optimize
                vm.Video_Optimize_Items = VideoNone.optimize;
                // Tune
                vm.Optimize_Tune_Items = VideoNone.tune;
                // Profile
                vm.Optimize_Profile_Items = VideoNone.profile;
                // Level
                vm.Optimize_Level_Items = VideoNone.level;

                // Checked
                VideoNone.controlsChecked(vm);

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
            // Empty
            // -------------------------
            //else if (string.IsNullOrEmpty(vm.VideoQuality_SelectedItem))
            //{
            //    // Bitrate Text is Displayed through VideoBitrateDisplay()

            //    // Pass
            //    vm.Pass_IsEnabled = false;

            //    // CRF
            //    vm.CRF_IsEnabled = false;

            //    // Bitrate
            //    vm.VideoBitrate_IsEnabled = false;
            //    // VBR
            //    vm.VideoVBR_IsEnabled = false;
            //    // Minrate
            //    vm.VideoMinrate_IsEnabled = false;
            //    // Maxrate
            //    vm.VideoMaxrate_IsEnabled = false;
            //    // Bufsize
            //    vm.VideoBufsize_IsEnabled = false;

            //    // Size
            //    vm.Size_IsEnabled = false;
            //}

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
                if (vm.VideoCodec_SelectedItem == "AV1" || // special rules
                    //vm.VideoCodec_SelectedItem == "VP9" ||
                    vm.VideoCodec_SelectedItem == "Copy" ||
                    vm.VideoCodec_SelectedItem == "None") 
                {
                    vm.VideoVBR_IsEnabled = false;
                }
                else
                {
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
                // -------------------------
                if (codec == "VP9" ||
                    codec == "x264" ||
                    codec == "x265" ||
                    codec == "AV1")
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
                if (vm.Pass_Items?.Contains("1 Pass") == true)
                {
                    vm.Pass_SelectedItem = "1 Pass";
                }
                else
                {
                    vm.Pass_SelectedItem = vm.Pass_Items.FirstOrDefault();
                }

                vm.Pass_IsEnabled = false;
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

                //vm.CRF_IsEnabled = true;
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
                vm.Scaling_SelectedItem == "default" &&

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
