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
        /// Controls
        /// </summary>
        public static bool passUserSelected = false; // Used to determine if User manually selected CRF, 1 Pass or 2 Pass


        /// <summary>
        /// Set Controls
        /// </summary>
        public static void SetControls(string codec_SelectedItem)
        {
            // --------------------------------------------------
            // Codec
            // --------------------------------------------------

            // --------------------------------------------------
            // Video
            // --------------------------------------------------
            switch (codec_SelectedItem)
            {
                // -------------------------
                // VP8
                // -------------------------
                case "VP8":
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
                    break;

                // -------------------------
                // VP9
                // -------------------------
                case "VP9":
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
                    break;

                // -------------------------
                // x264
                // -------------------------
                case "x264":
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
                    break;

                // -------------------------
                // x265
                // -------------------------
                case "x265":
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
                    break;

                // -------------------------
                // AV1
                // -------------------------
                case "AV1":
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
                    break;

                // -------------------------
                // FFV1
                // -------------------------
                case "FFV1":
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
                    break;

                // -------------------------
                // HuffYUV
                // -------------------------
                case "HuffYUV":
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
                    break;

                // -------------------------
                // Theora
                // -------------------------
                case "Theora":
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
                    break;

                // -------------------------
                // MPEG-2
                // -------------------------
                case "MPEG-2":
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
                    break;

                // -------------------------
                // MPEG-4
                // -------------------------
                case "MPEG-4":
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
                    break;


                // --------------------------------------------------
                // Image
                // --------------------------------------------------
                // -------------------------
                // JPEG
                // -------------------------
                case "JPEG":
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
                    break;

                // -------------------------
                // PNG
                // -------------------------
                case "PNG":
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
                    break;

                // -------------------------
                // WebP
                // -------------------------
                case "WebP":
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
                    break;

                // -------------------------
                // Copy
                // -------------------------
                case "Copy":
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
                    break;

                // -------------------------
                // None
                // -------------------------
                case "None":
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
                    break;
            }

            // --------------------------------------------------
            // Default Selected Item
            // Previous Items set in ViewModel _SelectedItem
            // --------------------------------------------------

            // -------------------------
            // Video Encode Speed Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(VM.VideoView.Video_EncodeSpeed_SelectedItem) &&
                VM.VideoView.Video_EncodeSpeed_SelectedItem != "none")
            {
                MainWindow.Video_EncodeSpeed_PreviousItem = VM.VideoView.Video_EncodeSpeed_SelectedItem;
            }

            VM.VideoView.Video_EncodeSpeed_SelectedItem = MainWindow.SelectedItem(VM.VideoView.Video_EncodeSpeed_Items.Select(c => c.Name).ToList(),
                                                                                  MainWindow.Video_EncodeSpeed_PreviousItem
                                                                                  );

            // -------------------------
            // Video Quality Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(VM.VideoView.Video_Quality_SelectedItem) &&
                VM.VideoView.Video_Quality_SelectedItem != "None")
            {
                MainWindow.Video_Quality_PreviousItem = VM.VideoView.Video_Quality_SelectedItem;
            }

            VM.VideoView.Video_Quality_SelectedItem = MainWindow.SelectedItem(VM.VideoView.Video_Quality_Items.Select(c => c.Name).ToList(),
                                                                              MainWindow.Video_Quality_PreviousItem
                                                                              );

            // -------------------------
            // Video Pass Selected Item
            // -------------------------
            //if (!string.IsNullOrEmpty(VM.VideoView.Video_Pass_SelectedItem))
            //{
            //    MainWindow.Video_EncodeSpeed_PreviousItem = VM.VideoView.Video_Pass_SelectedItem;
            //}

            //VM.VideoView.Video_Pass_SelectedItem = MainWindow.SelectedItem(VM.VideoView.Video_Pass_Items,
            //                                               MainWindow.Video_Pass_PreviousItem
            //                                               );

            // -------------------------
            // Video Optimize Selected Item
            // -------------------------
            // Problem, do not use, selects Web in mp4 when coming from webm
            //if (!string.IsNullOrEmpty(VM.VideoView.Video_Optimize_SelectedItem) &&
            //    VM.VideoView.Video_Optimize_SelectedItem != "None")
            //{
            //    MainWindow.VideoOptimize_PreviousItem = VM.VideoView.Video_Optimize_SelectedItem;
            //}

            //VM.VideoView.Video_Optimize_SelectedItem = MainWindow.SelectedItem(VM.VideoView.Video_Optimize_Items.Select(c => c.Name).ToList(),
            //                                                         MainWindow.VideoOptimize_PreviousItem
            //                                                         );
        }



        /// <summary>
        /// BitRate Display
        /// </summary>
        public static void VideoBitRateDisplay(List<VideoViewModel.VideoQuality> items,
                                               string selectedQuality,
                                               string selectedPass)
        {
            // Condition Check
            if (!string.IsNullOrEmpty(VM.VideoView.Video_Quality_SelectedItem) &&
                VM.VideoView.Video_Quality_SelectedItem != "Auto" &&
                VM.VideoView.Video_Quality_SelectedItem != "Lossless" &&
                VM.VideoView.Video_Quality_SelectedItem != "Custom" &&
                VM.VideoView.Video_Quality_SelectedItem != "None")
            {
                // -------------------------
                // Display in TextBox
                // -------------------------

                // -------------------------
                // auto
                // -------------------------
                if (selectedPass == "auto")
                {
                    VM.VideoView.Video_CRF_Text = string.Empty;
                    VM.VideoView.Video_BitRate_Text = string.Empty;
                    VM.VideoView.Video_MinRate_Text = string.Empty;
                    VM.VideoView.Video_MaxRate_Text = string.Empty;
                    VM.VideoView.Video_BufSize_Text = string.Empty;
                }

                // -------------------------
                // CRF
                // -------------------------
                else if (selectedPass == "CRF")
                {
                    // VP8/VP9 CRF is combined with BitRate e.g. -b:v 2000K -crf 16
                    // Other Codecs just use CRF

                    // CRF BitRate
                    VM.VideoView.Video_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.Video_CRF_BitRate;

                    // CRF
                    VM.VideoView.Video_CRF_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.CRF;

                    // MinRate
                    VM.VideoView.Video_MinRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.MinRate;

                    // MaxRate
                    VM.VideoView.Video_MaxRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.MaxRate;

                    // BufSize
                    VM.VideoView.Video_BufSize_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.BufSize;
                }

                // -------------------------
                // BitRate
                // -------------------------
                else if (selectedPass == "1 Pass" ||
                         selectedPass == "2 Pass")
                {
                    // CRF
                    VM.VideoView.Video_CRF_Text = string.Empty;

                    // BitRate CBR
                    if (VM.VideoView.Video_VBR_IsChecked == false)
                    {
                        VM.VideoView.Video_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.CBR;
                    }

                    // BitRate VBR
                    else if (VM.VideoView.Video_VBR_IsChecked == true)
                    {
                        VM.VideoView.Video_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.VBR;
                    }

                    // MinRate
                    VM.VideoView.Video_MinRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.MinRate;

                    // MaxRate
                    VM.VideoView.Video_MaxRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.MaxRate;

                    // BufSize
                    VM.VideoView.Video_BufSize_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.BufSize;
                }
            }
        }


        /// <summary>
        /// Quality Controls
        /// <summary>
        public static void QualityControls()
        {
            // -------------------------
            // Enable / Disable
            // -------------------------

            // -------------------------
            // Custom
            // -------------------------
            if (VM.VideoView.Video_Quality_SelectedItem == "Custom")
            {
                // Enable and Clear BitRate Text Display

                // Pass
                VM.VideoView.Video_Pass_IsEnabled = true;

                // CRF
                if (VM.VideoView.Video_Codec_SelectedItem != "JPEG" || // Special Rule
                    VM.VideoView.Video_Codec_SelectedItem != "PNG" ||
                    VM.VideoView.Video_Codec_SelectedItem != "WebP"
                    ) 
                {
                    VM.VideoView.Video_CRF_IsEnabled = true;
                }

                VM.VideoView.Video_CRF_Text = "";

                // BitRate
                VM.VideoView.Video_BitRate_IsEnabled = true;
                VM.VideoView.Video_BitRate_Text = "";

                // VBR
                VM.VideoView.Video_VBR_IsEnabled = true;

                // MinRate
                VM.VideoView.Video_MinRate_IsEnabled = true;
                VM.VideoView.Video_MinRate_Text = "";

                // MaxRate
                VM.VideoView.Video_MaxRate_IsEnabled = true;
                VM.VideoView.Video_MaxRate_Text = "";

                // BufSize
                VM.VideoView.Video_BufSize_IsEnabled = true;
                VM.VideoView.Video_BufSize_Text = "";

                // Size
                VM.VideoView.Video_Scale_IsEnabled = true;
            }

            // -------------------------
            // Auto
            // -------------------------
            else if (VM.VideoView.Video_Quality_SelectedItem == "Auto")
            {
                // Disable and Clear BitRate Text Dispaly

                // Pass
                VM.VideoView.Video_Pass_IsEnabled = false;

                // CRF
                VM.VideoView.Video_CRF_IsEnabled = false;
                VM.VideoView.Video_CRF_Text = "";

                // BitRate
                VM.VideoView.Video_BitRate_IsEnabled = false;
                VM.VideoView.Video_BitRate_Text = "";

                // VBR
                VM.VideoView.Video_VBR_IsEnabled = false;
                VM.VideoView.Video_VBR_IsChecked = false;

                // MinRate
                VM.VideoView.Video_MinRate_IsEnabled = false;
                VM.VideoView.Video_MinRate_Text = "";

                // MaxRate
                VM.VideoView.Video_MaxRate_IsEnabled = false;
                VM.VideoView.Video_MaxRate_Text = "";

                // BufSize
                VM.VideoView.Video_BufSize_IsEnabled = false;
                VM.VideoView.Video_BufSize_Text = "";

                // Size
                VM.VideoView.Video_Scale_IsEnabled = true;
            }

            // -------------------------
            // None
            // -------------------------
            else if (VM.VideoView.Video_Quality_SelectedItem == "None")
            {
                // BitRate Text is Displayed through VideoBitRateDisplay()

                // Pass
                VM.VideoView.Video_Pass_IsEnabled = false; 

                // CRF
                VM.VideoView.Video_CRF_IsEnabled = false;

                // BitRate
                VM.VideoView.Video_BitRate_IsEnabled = false;
                // VBR
                VM.VideoView.Video_VBR_IsEnabled = false;
                // MinRate
                VM.VideoView.Video_MinRate_IsEnabled = false;
                // MaxRate
                VM.VideoView.Video_MaxRate_IsEnabled = false;
                // BufSize
                VM.VideoView.Video_BufSize_IsEnabled = false;

                // Size
                VM.VideoView.Video_Scale_IsEnabled = false;
            }

            // -------------------------
            // All Other Qualities
            // -------------------------
            else
            {
                // BitRate Text is Displayed through VideoBitRateDisplay()

                // Pass
                VM.VideoView.Video_Pass_IsEnabled = true; // always enabled

                // CRF
                VM.VideoView.Video_CRF_IsEnabled = false;

                // BitRate
                VM.VideoView.Video_BitRate_IsEnabled = false;

                // VBR
                if (VM.VideoView.Video_Codec_SelectedItem == "VP8" || // special rules
                    VM.VideoView.Video_Codec_SelectedItem == "x264" ||
                    VM.VideoView.Video_Codec_SelectedItem == "JPEG" ||
                    VM.VideoView.Video_Codec_SelectedItem == "AV1" ||
                    VM.VideoView.Video_Codec_SelectedItem == "FFV1" ||
                    VM.VideoView.Video_Codec_SelectedItem == "HuffYUV" ||
                    VM.VideoView.Video_Codec_SelectedItem == "Copy" ||
                    VM.VideoView.Video_Codec_SelectedItem == "None") 
                {
                    // Disabled
                    VM.VideoView.Video_VBR_IsEnabled = false;
                }
                else
                {
                    // Enabled
                    VM.VideoView.Video_VBR_IsEnabled = true;
                }

                // MinRate
                VM.VideoView.Video_MinRate_IsEnabled = false;

                // MaxRate
                VM.VideoView.Video_MaxRate_IsEnabled = false;

                // BufSize
                VM.VideoView.Video_BufSize_IsEnabled = false;

                // Size
                VM.VideoView.Video_Scale_IsEnabled = true;
            }
        }


        /// <summary>
        /// Pixel Format Controls
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
                        VM.VideoView.Video_PixelFormat_SelectedItem = "yuv444p";
                    }
                    // All Other Quality
                    else
                    {
                        VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                    }
                }

                // -------------------------
                // FFV1
                // -------------------------
                else if (codec == "FFV1")
                {
                    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv444p10le";
                }

                // -------------------------
                // HuffYUV
                // -------------------------
                else if (codec == "HuffYUV")
                {
                    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv444p";
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
                    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                }

                // -------------------------
                // WebP
                // -------------------------
                else if (codec == "WebP")
                {
                    VM.VideoView.Video_PixelFormat_IsEnabled = true;

                    // Lossless
                    if (quality == "Lossless")
                    {
                        VM.VideoView.Video_PixelFormat_SelectedItem = "bgra";
                    }
                    // All Other Quality
                    else
                    {
                        VM.VideoView.Video_PixelFormat_SelectedItem = "yuva420p";
                    }
                }

                // -------------------------
                // Excluded Codecs
                // -------------------------
                else if (codec == "PNG")
                {
                    VM.VideoView.Video_PixelFormat_IsEnabled = true;

                    // Lossless
                    if (quality == "Lossless")
                    {
                        VM.VideoView.Video_PixelFormat_SelectedItem = "rgba";
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
        /// Optimize Controls
        /// <summary>
        public static void OptimizeControls()
        {
            // -------------------------
            // Only for x264 & x265 Video Codecs
            // -------------------------
            if (VM.VideoView.Video_Codec_SelectedItem == "x264" ||
                VM.VideoView.Video_Codec_SelectedItem == "x265")
            {
                // -------------------------
                // Enable - Tune, Profile, Level
                // -------------------------
                // Custom
                if (VM.VideoView.Video_Optimize_SelectedItem == "Custom")
                {
                    // Tune
                    VM.VideoView.Video_Optimize_Tune_IsEnabled = true;

                    // Profile
                    VM.VideoView.Video_Optimize_Profile_IsEnabled = true;

                    // Level
                    VM.VideoView.Video_Optimize_Level_IsEnabled = true;
                }
                // -------------------------
                // Disable - Tune, Profile, Level
                // -------------------------
                // Web, PC HD, HEVC, None, etc.
                else
                {
                    // Tune
                    VM.VideoView.Video_Optimize_Tune_IsEnabled = false;

                    // Profile
                    VM.VideoView.Video_Optimize_Profile_IsEnabled = false;

                    // Level
                    VM.VideoView.Video_Optimize_Level_IsEnabled = false;
                }
            }

            // -------------------------
            // Disable Tune, Profile, Level if Codec not x264/x265
            // -------------------------
            else
            {
                // Tune
                VM.VideoView.Video_Optimize_Tune_IsEnabled = false;

                // Profile
                VM.VideoView.Video_Optimize_Profile_IsEnabled = false;

                // Level
                VM.VideoView.Video_Optimize_Level_IsEnabled = false;
            }


            // -------------------------
            // Select Controls
            // -------------------------
            // Tune
            VM.VideoView.Video_Video_Optimize_Tune_SelectedItem = VM.VideoView.Video_Optimize_Items.FirstOrDefault(item => item.Name == VM.VideoView.Video_Optimize_SelectedItem)?.Tune;
            // Profile
            VM.VideoView.Video_Video_Optimize_Profile_SelectedItem = VM.VideoView.Video_Optimize_Items.FirstOrDefault(item => item.Name == VM.VideoView.Video_Optimize_SelectedItem)?.Profile;
            // Level
            VM.VideoView.Video_Optimize_Level_SelectedItem = VM.VideoView.Video_Optimize_Items.FirstOrDefault(item => item.Name == VM.VideoView.Video_Optimize_SelectedItem)?.Level;

        }


        /// <summary>
        /// Encoding Pass Controls
        /// <summary>
        public static void EncodingPassControls()
        {
            // --------------------------------------------------
            // Video
            // --------------------------------------------------
            // -------------------------
            // VP8
            // -------------------------
            if (VM.VideoView.Video_Codec_SelectedItem == "VP8")
            {
                VP8.EncodingPass();
            }
            // -------------------------
            // VP9
            // -------------------------
            else if (VM.VideoView.Video_Codec_SelectedItem == "VP9")
            {
                VP9.EncodingPass();
            }
            // -------------------------
            // x264
            // -------------------------
            else if (VM.VideoView.Video_Codec_SelectedItem == "x264")
            {
                x264.EncodingPass();
            }
            // -------------------------
            // x265
            // -------------------------
            else if (VM.VideoView.Video_Codec_SelectedItem == "x265")
            {
                x265.EncodingPass();
            }
            // -------------------------
            // AV1
            // -------------------------
            else if (VM.VideoView.Video_Codec_SelectedItem == "AV1")
            {
                AV1.EncodingPass();
            }
            // -------------------------
            // FFV1
            // -------------------------
            else if (VM.VideoView.Video_Codec_SelectedItem == "FFV1")
            {
                FFV1.EncodingPass();
            }
            // -------------------------
            // HuffYUV
            // -------------------------
            else if (VM.VideoView.Video_Codec_SelectedItem == "HuffYUV")
            {
                HuffYUV.EncodingPass();
            }
            // -------------------------
            // Theora
            // -------------------------
            else if (VM.VideoView.Video_Codec_SelectedItem == "Theora")
            {
                Theora.EncodingPass();
            }
            // -------------------------
            // MPEG-2
            // -------------------------
            else if (VM.VideoView.Video_Codec_SelectedItem == "MPEG-2")
            {
                MPEG_2.EncodingPass();
            }
            // -------------------------
            // MPEG-4
            // -------------------------
            else if (VM.VideoView.Video_Codec_SelectedItem == "MPEG-4")
            {
                MPEG_4.EncodingPass();
            }

            // --------------------------------------------------
            // Image
            // --------------------------------------------------
            // -------------------------
            // JPEG
            // -------------------------
            else if (VM.VideoView.Video_Codec_SelectedItem == "JPEG")
            {
                JPEG.EncodingPass();
            }
            // -------------------------
            // PNG
            // -------------------------
            else if (VM.VideoView.Video_Codec_SelectedItem == "PNG")
            {
                PNG.EncodingPass();
            }
            // -------------------------
            // WebP
            // -------------------------
            else if (VM.VideoView.Video_Codec_SelectedItem == "WebP")
            {
                WebP.EncodingPass();
            }

            // --------------------------------------------------
            // Other
            // --------------------------------------------------
            // -------------------------
            // Copy
            // -------------------------
            else if (VM.VideoView.Video_Codec_SelectedItem == "Copy")
            {
                VideoCopy.EncodingPass();
            }
            // -------------------------
            // None
            // -------------------------
            else if (VM.VideoView.Video_Codec_SelectedItem == "None")
            {
                VideoNone.EncodingPass();
            }


            // -------------------------
            // CRF TextBox
            // -------------------------
            if (VM.VideoView.Video_Quality_SelectedItem == "Custom")
            {
                // Disable
                if (VM.VideoView.Video_Pass_SelectedItem == "CRF")
                {
                    VM.VideoView.Video_CRF_IsEnabled = true;
                }
                // Enable
                else if (VM.VideoView.Video_Pass_SelectedItem == "1 Pass" ||
                         VM.VideoView.Video_Pass_SelectedItem == "2 Pass" ||
                         VM.VideoView.Video_Pass_SelectedItem == "auto")
                {
                    VM.VideoView.Video_CRF_IsEnabled = false;
                }
            }

        }



        /// <summary>
        /// Auto Copy Conditions Check
        /// <summary>
        public static bool AutoCopyConditionsCheck(string inputExt, 
                                                   string outputExt
                                                   )
        {
            // Problem: Getting Previous Selected Values

            //System.Windows.MessageBox.Show(VM.VideoView.Video_Quality_SelectedItem); //debug

            // Input Extension is Same as Output Extension and Video Quality is Auto
            // Note: Aspect Ratio -aspect can be applied to Copy
            if (VM.VideoView.Video_Quality_SelectedItem == "Auto" &&
                VM.VideoView.Video_PixelFormat_SelectedItem == "auto" &&
                string.IsNullOrEmpty(CropWindow.crop) &&
                VM.VideoView.Video_Scale_SelectedItem == "Source" &&
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem == "auto" &&
                // Do not add Aspect Ratio -aspect, it can be used with Copy
                VM.VideoView.Video_FPS_SelectedItem == "auto" &&
                VM.VideoView.Video_Optimize_SelectedItem == "None" &&

                // Filters
                // Fix
                VM.FilterVideoView.FilterVideo_Deband_SelectedItem == "disabled" &&
                VM.FilterVideoView.FilterVideo_Deshake_SelectedItem == "disabled" &&
                VM.FilterVideoView.FilterVideo_Deflicker_SelectedItem == "disabled" &&
                VM.FilterVideoView.FilterVideo_Dejudder_SelectedItem == "disabled" &&
                VM.FilterVideoView.FilterVideo_Denoise_SelectedItem == "disabled" &&
                VM.FilterVideoView.FilterVideo_Deinterlace_SelectedItem == "disabled" &&
                // Selective Color
                // Reds
                VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Cyan_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Magenta_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Yellow_Value == 0 &&
                // Yellows
                VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Cyan_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Magenta_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Yellow_Value == 0 &&
                // Greens
                VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Cyan_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Magenta_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Yellow_Value == 0 &&
                // Cyans
                VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Cyan_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Magenta_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Yellow_Value == 0 &&
                // Blues
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Cyan_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Magenta_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Yellow_Value == 0 &&
                // Magentas
                VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Cyan_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Magenta_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Yellow_Value == 0 &&
                // Whites
                VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Cyan_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Magenta_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Yellow_Value == 0 &&
                // Neutrals
                VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Cyan_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Magenta_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Yellow_Value == 0 &&
                // Blacks
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Cyan_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Magenta_Value == 0 &&
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Yellow_Value == 0 &&

                // EQ
                VM.FilterVideoView.FilterVideo_EQ_Brightness_Value == 0 &&
                VM.FilterVideoView.FilterVideo_EQ_Contrast_Value == 0 &&
                VM.FilterVideoView.FilterVideo_EQ_Saturation_Value == 0 &&
                VM.FilterVideoView.FilterVideo_EQ_Gamma_Value == 0 &&

                // File Extension Match
                string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase)
            )
            {
                //System.Windows.MessageBox.Show("pass check"); //debug
                return true;
            }

            // Did Not Pass Check
            else
            {
                return false;
            }
        }



        /// <summary>
        /// Copy Controls
        /// <summary>
        private static void CopyControls()
        {
            // -------------------------
            // Conditions Check
            // Enable
            // -------------------------
            if (AutoCopyConditionsCheck(MainWindow.inputExt.ToLower(), MainWindow.outputExt) == true)
            //if (AutoCopyConditionsCheck(MainWindow.inputExt.ToLower(), "." + VM.FormatView.Format_Container_SelectedItem.ToLower()) == true)
            {
                // -------------------------
                // Set Video Codec Combobox Selected Item to Copy
                // -------------------------
                if (VM.VideoView.Video_Codec_Items.Count > 0)
                {
                    //System.Windows.MessageBox.Show("copy1"); //debug
                    if (VM.VideoView.Video_Codec_Items?.Contains("Copy") == true)
                    {
                        //System.Windows.MessageBox.Show("copy2"); //debug
                        VM.VideoView.Video_Codec_SelectedItem = "Copy";
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
                if (!string.IsNullOrEmpty(VM.VideoView.Video_Quality_SelectedItem))
                {
                    // -------------------------
                    // Copy Selected
                    // -------------------------
                    if (VM.VideoView.Video_Codec_SelectedItem == "Copy")
                    {
                        // -------------------------
                        // Switch back to format's default codec
                        // -------------------------
                        if (VM.VideoView.Video_Codec_SelectedItem != "Auto" ||
                            !string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                            )
                        {
                            switch (VM.FormatView.Format_Container_SelectedItem)
                            {
                                // WebM
                                case "webm":
                                    VM.VideoView.Video_Codec_SelectedItem = "VP8";
                                    break;
                                // MP4
                                case "mp4":
                                    VM.VideoView.Video_Codec_SelectedItem = "x264";
                                    break;
                                // MKV
                                case "mkv":
                                    VM.VideoView.Video_Codec_SelectedItem = "x264";
                                    break;
                                // MPG
                                case "mpg":
                                    VM.VideoView.Video_Codec_SelectedItem = "MPEG-2";
                                    break;
                                // AVI
                                case "avi":
                                    VM.VideoView.Video_Codec_SelectedItem = "MPEG-4";
                                    break;
                                // OGV
                                case "ogv":
                                    VM.VideoView.Video_Codec_SelectedItem = "Theora";
                                    break;
                                // JPG
                                case "jpg":
                                    VM.VideoView.Video_Codec_SelectedItem = "JPEG";
                                    break;
                                // PNG
                                case "png":
                                    VM.VideoView.Video_Codec_SelectedItem = "PNG";
                                    break;
                                // WebP
                                case "webp":
                                    VM.VideoView.Video_Codec_SelectedItem = "WebP";
                                    break;
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Auto Codec Copy
        /// <summary>
        /// <remarks>
        /// Input Extension is Same as Output Extension and Video Quality is Auto
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
                     VM.VideoView.Video_Codec_SelectedItem == "Copy")
            {
                CopyControls();
            } 
        } 

    }
}
