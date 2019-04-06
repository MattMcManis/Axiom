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

/* ----------------------------------
 METHODS

 * Set Controls
 * BitRate Display
 * Quality Controls
 * Pixel Format Controls
 * Optimize Controls
 * Encoding Pass Controls
 * Auto Codec Copy
---------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;
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
        public static bool passUserSelected = false; // Used to determine if User manually selected CRF, 1 Pass or 2 Pass


        /// <summary>
        ///     Set Controls
        /// </summary>
        public static void SetControls(string codec_SelectedItem)
        {
            // --------------------------------------------------
            // Codec
            // --------------------------------------------------

            // --------------------------------------------------
            // Video
            // --------------------------------------------------
            // -------------------------
            // VP8
            // -------------------------
            if (codec_SelectedItem == "VP8")
            {
                // Codec
                VP8.Codec_Set();

                // Items Source
                VP8.Controls_ItemsSource();
                // Selected Items
                VP8.Controls_Selected();

                // Expanded
                VP8.Controls_Expanded();
                // Collapsed
                VP8.Controls_Collapsed();

                // Checked
                VP8.Controls_Checked();
                // Unhecked
                VP8.Controls_Unhecked();

                // Enabled
                VP8.Controls_Enable();
                // Disabled
                VP8.Controls_Disable();
            }

            // -------------------------
            // VP9
            // -------------------------
            else if (codec_SelectedItem == "VP9")
            {
                // Codec
                VP9.Codec_Set();

                // Items Source
                VP9.Controls_ItemsSource();
                // Selected Items
                VP9.Controls_Selected();

                // Expanded
                VP9.Controls_Expanded();
                // Collapsed
                VP9.Controls_Collapsed();

                // Checked
                VP9.Controls_Checked();
                // Unhecked
                VP9.Controls_Unhecked();

                // Enabled
                VP9.Controls_Enable();
                // Disabled
                VP9.Controls_Disable();
            }

            // -------------------------
            // x264
            // -------------------------
            else if (codec_SelectedItem == "x264")
            {
                // Codec
                x264.Codec_Set();

                // Items Source
                x264.Controls_ItemsSource();
                // Selected Items
                x264.Controls_Selected();

                // Expanded
                x264.Controls_Expanded();
                // Collapsed
                x264.Controls_Collapsed();

                // Checked
                x264.Controls_Checked();
                // Unhecked
                x264.Controls_Unhecked();

                // Enabled
                x264.Controls_Enable();
                // Disabled
                x264.Controls_Disable();
            }

            // -------------------------
            // x265
            // -------------------------
            else if (codec_SelectedItem == "x265")
            {
                // Codec
                x265.Codec_Set();

                // Items Source
                x265.Controls_ItemsSource();
                // Selected Items
                x265.Controls_Selected();

                // Expanded
                x265.Controls_Expanded();
                // Collapsed
                x265.Controls_Collapsed();

                // Checked
                x265.Controls_Checked();
                // Unhecked
                x265.Controls_Unhecked();

                // Enabled
                x265.Controls_Enable();
                // Disabled
                x265.Controls_Disable();
            }

            // -------------------------
            // AV1
            // -------------------------
            else if (codec_SelectedItem == "AV1")
            {
                // Codec
                AV1.Codec_Set();

                // Items Source
                AV1.Controls_ItemsSource();
                // Selected Items
                AV1.Controls_Selected();

                // Expanded
                AV1.Controls_Expanded();
                // Collapsed
                AV1.Controls_Collapsed();

                // Checked
                AV1.Controls_Checked();
                // Unhecked
                AV1.Controls_Unhecked();

                // Enabled
                AV1.Controls_Enable();
                // Disabled
                AV1.Controls_Disable();
            }

            // -------------------------
            // FFV1
            // -------------------------
            else if (codec_SelectedItem == "FFV1")
            {
                // Codec
                FFV1.Codec_Set();

                // Items Source
                FFV1.Controls_ItemsSource();
                // Selected Items
                FFV1.Controls_Selected();

                // Expanded
                FFV1.Controls_Expanded();
                // Collapsed
                FFV1.Controls_Collapsed();

                // Checked
                FFV1.Controls_Checked();
                // Unhecked
                FFV1.Controls_Unhecked();

                // Enabled
                FFV1.Controls_Enable();
                // Disabled
                FFV1.Controls_Disable();
            }

            // -------------------------
            // HuffYUV
            // -------------------------
            else if (codec_SelectedItem == "HuffYUV")
            {
                // Codec
                HuffYUV.Codec_Set();

                // Items Source
                HuffYUV.Controls_ItemsSource();
                // Selected Items
                HuffYUV.Controls_Selected();

                // Expanded
                HuffYUV.Controls_Expanded();
                // Collapsed
                HuffYUV.Controls_Collapsed();

                // Checked
                HuffYUV.Controls_Checked();
                // Unhecked
                HuffYUV.Controls_Unhecked();

                // Enabled
                HuffYUV.Controls_Enable();
                // Disabled
                HuffYUV.Controls_Disable();
            }

            // -------------------------
            // Theora
            // -------------------------
            else if (codec_SelectedItem == "Theora")
            {
                // Codec
                Theora.Codec_Set();

                // Items Source
                Theora.Controls_ItemsSource();
                // Selected Items
                Theora.Controls_Selected();

                // Expanded
                Theora.Controls_Expanded();
                // Collapsed
                Theora.Controls_Collapsed();

                // Checked
                Theora.Controls_Checked();
                // Unhecked
                Theora.Controls_Unhecked();

                // Enabled
                Theora.Controls_Enable();
                // Disabled
                Theora.Controls_Disable();
            }

            // -------------------------
            // MPEG-2
            // -------------------------
            else if (codec_SelectedItem == "MPEG-2")
            {
                // Codec
                MPEG_2.Codec_Set();

                // Items Source
                MPEG_2.Controls_ItemsSource();
                // Selected Items
                MPEG_2.Controls_Selected();

                // Expanded
                MPEG_2.Controls_Expanded();
                // Collapsed
                MPEG_2.Controls_Collapsed();

                // Checked
                MPEG_2.Controls_Checked();
                // Unhecked
                MPEG_2.Controls_Unhecked();

                // Enabled
                MPEG_2.Controls_Enable();
                // Disabled
                MPEG_2.Controls_Disable();
            }

            // -------------------------
            // MPEG-4
            // -------------------------
            else if (codec_SelectedItem == "MPEG-4")
            {
                // Codec
                MPEG_4.Codec_Set();

                // Items Source
                MPEG_4.Controls_ItemsSource();
                // Selected Items
                MPEG_4.Controls_Selected();

                // Expanded
                MPEG_4.Controls_Expanded();
                // Collapsed
                MPEG_4.Controls_Collapsed();

                // Checked
                MPEG_4.Controls_Checked();
                // Unhecked
                MPEG_4.Controls_Unhecked();

                // Enabled
                MPEG_4.Controls_Enable();
                // Disabled
                MPEG_4.Controls_Disable();
            }


            // --------------------------------------------------
            // Image
            // --------------------------------------------------
            // -------------------------
            // JPEG
            // -------------------------
            else if (codec_SelectedItem == "JPEG")
            {
                // Codec
                JPEG.Codec_Set();

                // Items Source
                JPEG.Controls_ItemsSource();
                // Selected Items
                JPEG.Controls_Selected();

                // Expanded
                JPEG.Controls_Expanded();
                // Collapsed
                JPEG.Controls_Collapsed();

                // Checked
                JPEG.Controls_Checked();
                // Unhecked
                JPEG.Controls_Unhecked();

                // Enabled
                JPEG.Controls_Enable();
                // Disabled
                JPEG.Controls_Disable();
            }

            // -------------------------
            // PNG
            // -------------------------
            else if (codec_SelectedItem == "PNG")
            {
                // Codec
                PNG.Codec_Set();

                // Items Source
                PNG.Controls_ItemsSource();
                // Selected Items
                PNG.Controls_Selected();

                // Expanded
                PNG.Controls_Expanded();
                // Collapsed
                PNG.Controls_Collapsed();

                // Checked
                PNG.Controls_Checked();
                // Unhecked
                PNG.Controls_Unhecked();

                // Enabled
                PNG.Controls_Enable();
                // Disabled
                PNG.Controls_Disable();
            }

            // -------------------------
            // WebP
            // -------------------------
            else if (codec_SelectedItem == "WebP")
            {
                // Codec
                WebP.Codec_Set();

                // Items Source
                WebP.Controls_ItemsSource();
                // Selected Items
                WebP.Controls_Selected();

                // Expanded
                WebP.Controls_Expanded();
                // Collapsed
                WebP.Controls_Collapsed();

                // Checked
                WebP.Controls_Checked();
                // Unhecked
                WebP.Controls_Unhecked();

                // Enabled
                WebP.Controls_Enable();
                // Disabled
                WebP.Controls_Disable();
            }

            // -------------------------
            // Copy
            // -------------------------
            else if (codec_SelectedItem == "Copy")
            {
                // Codec
                VideoCopy.Codec_Set();

                // Items Source
                VideoCopy.Controls_ItemsSource();
                // Selected Items
                VideoCopy.Controls_Selected();

                // Expanded
                VideoCopy.Controls_Expanded();
                // Collapsed
                VideoCopy.Controls_Collapsed();

                // Checked
                VideoCopy.Controls_Checked();
                // Unhecked
                VideoCopy.Controls_Unhecked();

                // Enabled
                VideoCopy.Controls_Enable();
                // Disabled
                VideoCopy.Controls_Disable();
            }

            // -------------------------
            // None
            // -------------------------
            else if (codec_SelectedItem == "None")
            {
                // Codec
                VideoNone.Codec_Set();

                // Items Source
                VideoNone.Controls_ItemsSource();
                // Selected Items
                VideoNone.Controls_Selected();

                // Expanded
                VideoNone.Controls_Expanded();
                // Collapsed
                VideoNone.Controls_Collapsed();

                // Checked
                VideoNone.Controls_Checked();
                // Unhecked
                VideoNone.Controls_Unhecked();

                // Enabled
                VideoNone.Controls_Enable();
                // Disabled
                VideoNone.Controls_Disable();
            }

            // --------------------------------------------------
            // Default Selected Item
            // Previous Items set in ViewModel _SelectedItem
            // --------------------------------------------------

            // -------------------------
            // Video Encode Speed Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(VideoView.vm.Video_EncodeSpeed_SelectedItem) &&
                VideoView.vm.Video_EncodeSpeed_SelectedItem != "none")
            {
                MainWindow.Video_EncodeSpeed_PreviousItem = VideoView.vm.Video_EncodeSpeed_SelectedItem;
            }

            VideoView.vm.Video_EncodeSpeed_SelectedItem = MainWindow.SelectedItem(VideoView.vm.Video_EncodeSpeed_Items.Select(c => c.Name).ToList(),
                                                                                  MainWindow.Video_EncodeSpeed_PreviousItem
                                                                                  );

            // -------------------------
            // Video Quality Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(VideoView.vm.Video_Quality_SelectedItem) &&
                VideoView.vm.Video_Quality_SelectedItem != "None")
            {
                MainWindow.Video_Quality_PreviousItem = VideoView.vm.Video_Quality_SelectedItem;
            }

            VideoView.vm.Video_Quality_SelectedItem = MainWindow.SelectedItem(VideoView.vm.Video_Quality_Items.Select(c => c.Name).ToList(),
                                                                              MainWindow.Video_Quality_PreviousItem
                                                                              );

            // -------------------------
            // Video Pass Selected Item
            // -------------------------
            //if (!string.IsNullOrEmpty(VideoView.vm.Video_Pass_SelectedItem))
            //{
            //    MainWindow.Video_EncodeSpeed_PreviousItem = VideoView.vm.Video_Pass_SelectedItem;
            //}

            //VideoView.vm.Video_Pass_SelectedItem = MainWindow.SelectedItem(VideoView.vm.Video_Pass_Items,
            //                                               MainWindow.Video_Pass_PreviousItem
            //                                               );

            // -------------------------
            // Video Optimize Selected Item
            // -------------------------
            // Problem, do not use, selects Web in mp4 when coming from webm
            //if (!string.IsNullOrEmpty(VideoView.vm.Video_Optimize_SelectedItem) &&
            //    VideoView.vm.Video_Optimize_SelectedItem != "None")
            //{
            //    MainWindow.VideoOptimize_PreviousItem = VideoView.vm.Video_Optimize_SelectedItem;
            //}

            //VideoView.vm.Video_Optimize_SelectedItem = MainWindow.SelectedItem(VideoView.vm.Video_Optimize_Items.Select(c => c.Name).ToList(),
            //                                                         MainWindow.VideoOptimize_PreviousItem
            //                                                         );
        }



        /// <summary>
        ///    BitRate Display
        /// </summary>
        public static void VideoBitRateDisplay(List<VideoView.VideoQuality> items,
                                               string selectedQuality,
                                               string selectedPass)
        {
            // Condition Check
            if (!string.IsNullOrEmpty(VideoView.vm.Video_Quality_SelectedItem) &&
                VideoView.vm.Video_Quality_SelectedItem != "Auto" &&
                VideoView.vm.Video_Quality_SelectedItem != "Lossless" &&
                VideoView.vm.Video_Quality_SelectedItem != "Custom" &&
                VideoView.vm.Video_Quality_SelectedItem != "None")
            {
                // -------------------------
                // Display in TextBox
                // -------------------------

                // -------------------------
                // auto
                // -------------------------
                if (selectedPass == "auto")
                {
                    VideoView.vm.Video_CRF_Text = string.Empty;
                    VideoView.vm.Video_BitRate_Text = string.Empty;
                    VideoView.vm.Video_MinRate_Text = string.Empty;
                    VideoView.vm.Video_MaxRate_Text = string.Empty;
                    VideoView.vm.Video_BufSize_Text = string.Empty;
                }

                // -------------------------
                // CRF
                // -------------------------
                else if (selectedPass == "CRF")
                {
                    // VP8/VP9 CRF is combined with BitRate e.g. -b:v 2000K -crf 16
                    // Other Codecs just use CRF

                    // CRF BitRate
                    VideoView.vm.Video_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.Video_CRF_BitRate;

                    // CRF
                    VideoView.vm.Video_CRF_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.CRF;

                    // MinRate
                    VideoView.vm.Video_MinRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.MinRate;

                    // MaxRate
                    VideoView.vm.Video_MaxRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.MaxRate;

                    // BufSize
                    VideoView.vm.Video_BufSize_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.BufSize;
                }

                // -------------------------
                // BitRate
                // -------------------------
                else if (selectedPass == "1 Pass" ||
                         selectedPass == "2 Pass")
                {
                    // CRF
                    VideoView.vm.Video_CRF_Text = string.Empty;

                    // BitRate CBR
                    if (VideoView.vm.Video_VBR_IsChecked == false)
                    {
                        VideoView.vm.Video_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.CBR;
                    }

                    // BitRate VBR
                    else if (VideoView.vm.Video_VBR_IsChecked == true)
                    {
                        VideoView.vm.Video_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.VBR;
                    }

                    // MinRate
                    VideoView.vm.Video_MinRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.MinRate;

                    // MaxRate
                    VideoView.vm.Video_MaxRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.MaxRate;

                    // BufSize
                    VideoView.vm.Video_BufSize_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.BufSize;
                }
            }
        }


        /// <summary>
        ///     Quality Controls
        /// <summary>
        public static void QualityControls()
        {
            // -------------------------
            // Enable / Disable
            // -------------------------

            // -------------------------
            // Custom
            // -------------------------
            if (VideoView.vm.Video_Quality_SelectedItem == "Custom")
            {
                // Enable and Clear BitRate Text Display

                // Pass
                VideoView.vm.Video_Pass_IsEnabled = true;

                // CRF
                if (VideoView.vm.Video_Codec_SelectedItem != "JPEG" || // Special Rule
                    VideoView.vm.Video_Codec_SelectedItem != "PNG" ||
                    VideoView.vm.Video_Codec_SelectedItem != "WebP"
                    ) 
                {
                    VideoView.vm.Video_CRF_IsEnabled = true;
                }

                VideoView.vm.Video_CRF_Text = "";

                // BitRate
                VideoView.vm.Video_BitRate_IsEnabled = true;
                VideoView.vm.Video_BitRate_Text = "";

                // VBR
                VideoView.vm.Video_VBR_IsEnabled = true;

                // MinRate
                VideoView.vm.Video_MinRate_IsEnabled = true;
                VideoView.vm.Video_MinRate_Text = "";

                // MaxRate
                VideoView.vm.Video_MaxRate_IsEnabled = true;
                VideoView.vm.Video_MaxRate_Text = "";

                // BufSize
                VideoView.vm.Video_BufSize_IsEnabled = true;
                VideoView.vm.Video_BufSize_Text = "";

                // Size
                VideoView.vm.Video_Scale_IsEnabled = true;
            }

            // -------------------------
            // Auto
            // -------------------------
            else if (VideoView.vm.Video_Quality_SelectedItem == "Auto")
            {
                // Disable and Clear BitRate Text Dispaly

                // Pass
                VideoView.vm.Video_Pass_IsEnabled = false;

                // CRF
                VideoView.vm.Video_CRF_IsEnabled = false;
                VideoView.vm.Video_CRF_Text = "";

                // BitRate
                VideoView.vm.Video_BitRate_IsEnabled = false;
                VideoView.vm.Video_BitRate_Text = "";

                // VBR
                VideoView.vm.Video_VBR_IsEnabled = false;
                VideoView.vm.Video_VBR_IsChecked = false;

                // MinRate
                VideoView.vm.Video_MinRate_IsEnabled = false;
                VideoView.vm.Video_MinRate_Text = "";

                // MaxRate
                VideoView.vm.Video_MaxRate_IsEnabled = false;
                VideoView.vm.Video_MaxRate_Text = "";

                // BufSize
                VideoView.vm.Video_BufSize_IsEnabled = false;
                VideoView.vm.Video_BufSize_Text = "";

                // Size
                VideoView.vm.Video_Scale_IsEnabled = true;
            }

            // -------------------------
            // None
            // -------------------------
            else if (VideoView.vm.Video_Quality_SelectedItem == "None")
            {
                // BitRate Text is Displayed through VideoBitRateDisplay()

                // Pass
                VideoView.vm.Video_Pass_IsEnabled = false; 

                // CRF
                VideoView.vm.Video_CRF_IsEnabled = false;

                // BitRate
                VideoView.vm.Video_BitRate_IsEnabled = false;
                // VBR
                VideoView.vm.Video_VBR_IsEnabled = false;
                // MinRate
                VideoView.vm.Video_MinRate_IsEnabled = false;
                // MaxRate
                VideoView.vm.Video_MaxRate_IsEnabled = false;
                // BufSize
                VideoView.vm.Video_BufSize_IsEnabled = false;

                // Size
                VideoView.vm.Video_Scale_IsEnabled = false;
            }

            // -------------------------
            // All Other Qualities
            // -------------------------
            else
            {
                // BitRate Text is Displayed through VideoBitRateDisplay()

                // Pass
                VideoView.vm.Video_Pass_IsEnabled = true; // always enabled

                // CRF
                VideoView.vm.Video_CRF_IsEnabled = false;

                // BitRate
                VideoView.vm.Video_BitRate_IsEnabled = false;

                // VBR
                if (VideoView.vm.Video_Codec_SelectedItem == "VP8" || // special rules
                    VideoView.vm.Video_Codec_SelectedItem == "x264" ||
                    VideoView.vm.Video_Codec_SelectedItem == "JPEG" ||
                    VideoView.vm.Video_Codec_SelectedItem == "AV1" ||
                    VideoView.vm.Video_Codec_SelectedItem == "FFV1" ||
                    VideoView.vm.Video_Codec_SelectedItem == "HuffYUV" ||
                    VideoView.vm.Video_Codec_SelectedItem == "Copy" ||
                    VideoView.vm.Video_Codec_SelectedItem == "None") 
                {
                    // Disabled
                    VideoView.vm.Video_VBR_IsEnabled = false;
                }
                else
                {
                    // Enabled
                    VideoView.vm.Video_VBR_IsEnabled = true;
                }

                // MinRate
                VideoView.vm.Video_MinRate_IsEnabled = false;

                // MaxRate
                VideoView.vm.Video_MaxRate_IsEnabled = false;

                // BufSize
                VideoView.vm.Video_BufSize_IsEnabled = false;

                // Size
                VideoView.vm.Video_Scale_IsEnabled = true;
            }
        }


        /// <summary>
        ///     Pixel Format Controls
        /// </summary>
        public static void PixelFormatControls(string mediaType, 
                                               string codec, 
                                               string quality
                                               )
        {
            // -------------------------
            // MediaTypeControls
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
                    // Lossless
                    if (quality == "Lossless")
                    {
                        VideoView.vm.Video_PixelFormat_SelectedItem = "yuv444p";
                    }
                    // All Other Quality
                    else
                    {
                        VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                    }
                }

                // -------------------------
                // FFV1
                // -------------------------
                else if (codec == "FFV1")
                {
                    VideoView.vm.Video_PixelFormat_SelectedItem = "yuv444p10le";
                }

                // -------------------------
                // HuffYUV
                // -------------------------
                else if (codec == "HuffYUV")
                {
                    VideoView.vm.Video_PixelFormat_SelectedItem = "yuv444p";
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
                    VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                }

                // -------------------------
                // WebP
                // -------------------------
                else if (codec == "WebP")
                {
                    VideoView.vm.Video_PixelFormat_IsEnabled = true;

                    // Lossless
                    if (quality == "Lossless")
                    {
                        VideoView.vm.Video_PixelFormat_SelectedItem = "bgra";
                    }
                    // All Other Quality
                    else
                    {
                        VideoView.vm.Video_PixelFormat_SelectedItem = "yuva420p";
                    }
                }

                // -------------------------
                // Excluded Codecs
                // -------------------------
                else if (codec == "PNG")
                {
                    VideoView.vm.Video_PixelFormat_IsEnabled = true;

                    // Lossless
                    if (quality == "Lossless")
                    {
                        VideoView.vm.Video_PixelFormat_SelectedItem = "rgba";
                    }
                }

                // -------------------------
                // Excluded Codecs
                // -------------------------
                // JPEG
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
        public static void OptimizeControls()
        {
            // -------------------------
            // Only for x264 & x265 Video Codecs
            // -------------------------
            if (VideoView.vm.Video_Codec_SelectedItem == "x264" ||
                VideoView.vm.Video_Codec_SelectedItem == "x265")
            {
                // -------------------------
                // Enable - Tune, Profile, Level
                // -------------------------
                // Custom
                if (VideoView.vm.Video_Optimize_SelectedItem == "Custom")
                {
                    // Tune
                    VideoView.vm.Video_Optimize_Tune_IsEnabled = true;

                    // Profile
                    VideoView.vm.Video_Optimize_Profile_IsEnabled = true;

                    // Level
                    VideoView.vm.Video_Optimize_Level_IsEnabled = true;
                }
                // -------------------------
                // Disable - Tune, Profile, Level
                // -------------------------
                // Web, PC HD, HEVC, None, etc.
                else
                {
                    // Tune
                    VideoView.vm.Video_Optimize_Tune_IsEnabled = false;

                    // Profile
                    VideoView.vm.Video_Optimize_Profile_IsEnabled = false;

                    // Level
                    VideoView.vm.Video_Optimize_Level_IsEnabled = false;
                }
            }

            // -------------------------
            // Disable Tune, Profile, Level if Codec not x264/x265
            // -------------------------
            else
            {
                // Tune
                VideoView.vm.Video_Optimize_Tune_IsEnabled = false;

                // Profile
                VideoView.vm.Video_Optimize_Profile_IsEnabled = false;

                // Level
                VideoView.vm.Video_Optimize_Level_IsEnabled = false;
            }


            // -------------------------
            // Select Controls
            // -------------------------
            // Tune
            VideoView.vm.Video_Video_Optimize_Tune_SelectedItem = VideoView.vm.Video_Optimize_Items.FirstOrDefault(item => item.Name == VideoView.vm.Video_Optimize_SelectedItem)?.Tune;
            // Profile
            VideoView.vm.Video_Video_Optimize_Profile_SelectedItem = VideoView.vm.Video_Optimize_Items.FirstOrDefault(item => item.Name == VideoView.vm.Video_Optimize_SelectedItem)?.Profile;
            // Level
            VideoView.vm.Video_Optimize_Level_SelectedItem = VideoView.vm.Video_Optimize_Items.FirstOrDefault(item => item.Name == VideoView.vm.Video_Optimize_SelectedItem)?.Level;

        }


        /// <summary>
        ///     Encoding Pass Controls
        /// <summary>
        public static void EncodingPassControls()
        {
            // --------------------------------------------------
            // Video
            // --------------------------------------------------
            // -------------------------
            // VP8
            // -------------------------
            if (VideoView.vm.Video_Codec_SelectedItem == "VP8")
            {
                VP8.EncodingPass();
            }
            // -------------------------
            // VP9
            // -------------------------
            else if (VideoView.vm.Video_Codec_SelectedItem == "VP9")
            {
                VP9.EncodingPass();
            }
            // -------------------------
            // x264
            // -------------------------
            else if (VideoView.vm.Video_Codec_SelectedItem == "x264")
            {
                x264.EncodingPass();
            }
            // -------------------------
            // x265
            // -------------------------
            else if (VideoView.vm.Video_Codec_SelectedItem == "x265")
            {
                x265.EncodingPass();
            }
            // -------------------------
            // AV1
            // -------------------------
            else if (VideoView.vm.Video_Codec_SelectedItem == "AV1")
            {
                AV1.EncodingPass();
            }
            // -------------------------
            // FFV1
            // -------------------------
            else if (VideoView.vm.Video_Codec_SelectedItem == "FFV1")
            {
                FFV1.EncodingPass();
            }
            // -------------------------
            // HuffYUV
            // -------------------------
            else if (VideoView.vm.Video_Codec_SelectedItem == "HuffYUV")
            {
                HuffYUV.EncodingPass();
            }
            // -------------------------
            // Theora
            // -------------------------
            else if (VideoView.vm.Video_Codec_SelectedItem == "Theora")
            {
                Theora.EncodingPass();
            }
            // -------------------------
            // MPEG-2
            // -------------------------
            else if (VideoView.vm.Video_Codec_SelectedItem == "MPEG-2")
            {
                MPEG_2.EncodingPass();
            }
            // -------------------------
            // MPEG-4
            // -------------------------
            else if (VideoView.vm.Video_Codec_SelectedItem == "MPEG-4")
            {
                MPEG_4.EncodingPass();
            }

            // --------------------------------------------------
            // Image
            // --------------------------------------------------
            // -------------------------
            // JPEG
            // -------------------------
            else if (VideoView.vm.Video_Codec_SelectedItem == "JPEG")
            {
                JPEG.EncodingPass();
            }
            // -------------------------
            // PNG
            // -------------------------
            else if (VideoView.vm.Video_Codec_SelectedItem == "PNG")
            {
                PNG.EncodingPass();
            }
            // -------------------------
            // WebP
            // -------------------------
            else if (VideoView.vm.Video_Codec_SelectedItem == "WebP")
            {
                WebP.EncodingPass();
            }

            // --------------------------------------------------
            // Other
            // --------------------------------------------------
            // -------------------------
            // Copy
            // -------------------------
            else if (VideoView.vm.Video_Codec_SelectedItem == "Copy")
            {
                VideoCopy.EncodingPass();
            }
            // -------------------------
            // None
            // -------------------------
            else if (VideoView.vm.Video_Codec_SelectedItem == "None")
            {
                VideoNone.EncodingPass();
            }


            // -------------------------
            // CRF TextBox
            // -------------------------
            if (VideoView.vm.Video_Quality_SelectedItem == "Custom")
            {
                // Disable
                if (VideoView.vm.Video_Pass_SelectedItem == "CRF")
                {
                    VideoView.vm.Video_CRF_IsEnabled = true;
                }
                // Enable
                else if (VideoView.vm.Video_Pass_SelectedItem == "1 Pass" ||
                         VideoView.vm.Video_Pass_SelectedItem == "2 Pass" ||
                         VideoView.vm.Video_Pass_SelectedItem == "auto")
                {
                    VideoView.vm.Video_CRF_IsEnabled = false;
                }
            }

        }



        /// <summary>
        ///    Auto Copy Conditions Check
        /// <summary>
        public static bool AutoCopyConditionsCheck(string inputExt, 
                                                   string outputExt
                                                   )
        {
            // Input Extension is Same as Output Extension and Video Quality is Auto
            // Note: Aspect Ratio -aspect can be applied to Copy
            if (VideoView.vm.Video_Quality_SelectedItem == "Auto" &&
                VideoView.vm.Video_PixelFormat_SelectedItem == "auto" &&
                string.IsNullOrEmpty(CropWindow.crop) &&
                VideoView.vm.Video_Scale_SelectedItem == "Source" &&
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem == "auto" &&
                // Do not add Aspect Ratio -aspect, it can be used with Copy
                VideoView.vm.Video_FPS_SelectedItem == "auto" &&
                VideoView.vm.Video_Optimize_SelectedItem == "None" &&

                // Filters
                // Fix
                FilterVideoView.vm.FilterVideo_Deband_SelectedItem == "disabled" &&
                FilterVideoView.vm.FilterVideo_Deshake_SelectedItem == "disabled" &&
                FilterVideoView.vm.FilterVideo_Deflicker_SelectedItem == "disabled" &&
                FilterVideoView.vm.FilterVideo_Dejudder_SelectedItem == "disabled" &&
                FilterVideoView.vm.FilterVideo_Denoise_SelectedItem == "disabled" &&
                FilterVideoView.vm.FilterVideo_Deinterlace_SelectedItem == "disabled" &&
                // Selective Color
                // Reds
                FilterVideoView.vm.FilterVideo_SelectiveColor_Reds_Cyan_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Reds_Magenta_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Reds_Yellow_Value == 0 &&
                // Yellows
                FilterVideoView.vm.FilterVideo_SelectiveColor_Yellows_Cyan_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Yellows_Magenta_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Yellows_Yellow_Value == 0 &&
                // Greens
                FilterVideoView.vm.FilterVideo_SelectiveColor_Greens_Cyan_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Greens_Magenta_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Greens_Yellow_Value == 0 &&
                // Cyans
                FilterVideoView.vm.FilterVideo_SelectiveColor_Cyans_Cyan_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Cyans_Magenta_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Cyans_Yellow_Value == 0 &&
                // Blues
                FilterVideoView.vm.FilterVideo_SelectiveColor_Blues_Cyan_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Blues_Magenta_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Blues_Yellow_Value == 0 &&
                // Magentas
                FilterVideoView.vm.FilterVideo_SelectiveColor_Magentas_Cyan_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Magentas_Magenta_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Magentas_Yellow_Value == 0 &&
                // Whites
                FilterVideoView.vm.FilterVideo_SelectiveColor_Whites_Cyan_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Whites_Magenta_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Whites_Yellow_Value == 0 &&
                // Neutrals
                FilterVideoView.vm.FilterVideo_SelectiveColor_Neutrals_Cyan_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Neutrals_Magenta_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Neutrals_Yellow_Value == 0 &&
                // Blacks
                FilterVideoView.vm.FilterVideo_SelectiveColor_Blacks_Cyan_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Blacks_Magenta_Value == 0 &&
                FilterVideoView.vm.FilterVideo_SelectiveColor_Blacks_Yellow_Value == 0 &&

                // EQ
                FilterVideoView.vm.FilterVideo_EQ_Brightness_Value == 0 &&
                FilterVideoView.vm.FilterVideo_EQ_Contrast_Value == 0 &&
                FilterVideoView.vm.FilterVideo_EQ_Saturation_Value == 0 &&
                FilterVideoView.vm.FilterVideo_EQ_Gamma_Value == 0 &&

                // File Extension Match
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
                // Set Video Codec Combobox Selected Item to Copy
                // -------------------------
                if (VideoView.vm.Video_Codec_Items.Count > 0)
                {
                    if (VideoView.vm.Video_Codec_Items?.Contains("Copy") == true)
                    {
                        VideoView.vm.Video_Codec_SelectedItem = "Copy";
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
                if (!string.IsNullOrEmpty(VideoView.vm.Video_Quality_SelectedItem))
                {
                    // -------------------------
                    // Copy Selected
                    // -------------------------
                    if (VideoView.vm.Video_Codec_SelectedItem == "Copy")
                    {
                        // -------------------------
                        // Switch back to format's default codec
                        // -------------------------
                        if (VideoView.vm.Video_Codec_SelectedItem != "Auto" ||
                            !string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                            )
                        {
                            // -------------------------
                            // WebM
                            // -------------------------
                            if (FormatView.vm.Format_Container_SelectedItem == "webm")
                            {
                                VideoView.vm.Video_Codec_SelectedItem = "VP8";
                            }
                            // -------------------------
                            // MP4
                            // -------------------------
                            else if (FormatView.vm.Format_Container_SelectedItem == "mp4")
                            {
                                VideoView.vm.Video_Codec_SelectedItem = "x264";
                            }
                            // -------------------------
                            // MKV
                            // -------------------------
                            else if (FormatView.vm.Format_Container_SelectedItem == "mkv")
                            {
                                VideoView.vm.Video_Codec_SelectedItem = "x264";
                            }
                            // -------------------------
                            // MPG
                            // -------------------------
                            else if (FormatView.vm.Format_Container_SelectedItem == "mpg")
                            {
                                VideoView.vm.Video_Codec_SelectedItem = "MPEG-2";
                            }
                            // -------------------------
                            // AVI
                            // -------------------------
                            else if (FormatView.vm.Format_Container_SelectedItem == "avi")
                            {
                                VideoView.vm.Video_Codec_SelectedItem = "MPEG-4";
                            }
                            // -------------------------
                            // OGV
                            // -------------------------
                            else if (FormatView.vm.Format_Container_SelectedItem == "ogv")
                            {
                                VideoView.vm.Video_Codec_SelectedItem = "Theora";
                            }
                            // -------------------------
                            // JPG
                            // -------------------------
                            else if (FormatView.vm.Format_Container_SelectedItem == "jpg")
                            {
                                VideoView.vm.Video_Codec_SelectedItem = "JPEG";
                            }
                            // -------------------------
                            // PNG
                            // -------------------------
                            else if (FormatView.vm.Format_Container_SelectedItem == "png")
                            {
                                VideoView.vm.Video_Codec_SelectedItem = "PNG";
                            }
                            // -------------------------
                            // WebP
                            // -------------------------
                            else if (FormatView.vm.Format_Container_SelectedItem == "webp")
                            {
                                VideoView.vm.Video_Codec_SelectedItem = "WebP";
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
        public static void AutoCopyVideoCodec()
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
                     VideoView.vm.Video_Codec_SelectedItem == "Copy")
            {
                CopyControls();
            } 
        } 

    }
}
