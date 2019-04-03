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
        public static void SetControls(ViewModel vm, string codec_SelectedItem)
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
                VP8.Codec_Set(vm);

                // Items Source
                VP8.Controls_ItemsSource(vm);
                // Selected Items
                VP8.Controls_Selected(vm);

                // Expanded
                VP8.Controls_Expanded(vm);
                // Collapsed
                VP8.Controls_Collapsed(vm);

                // Checked
                VP8.Controls_Checked(vm);
                // Unhecked
                VP8.Controls_Unhecked(vm);

                // Enabled
                VP8.Controls_Enable(vm);
                // Disabled
                VP8.Controls_Disable(vm);
            }

            // -------------------------
            // VP9
            // -------------------------
            else if (codec_SelectedItem == "VP9")
            {
                // Codec
                VP9.Codec_Set(vm);

                // Items Source
                VP9.Controls_ItemsSource(vm);
                // Selected Items
                VP9.Controls_Selected(vm);

                // Expanded
                VP9.Controls_Expanded(vm);
                // Collapsed
                VP9.Controls_Collapsed(vm);

                // Checked
                VP9.Controls_Checked(vm);
                // Unhecked
                VP9.Controls_Unhecked(vm);

                // Enabled
                VP9.Controls_Enable(vm);
                // Disabled
                VP9.Controls_Disable(vm);
            }

            // -------------------------
            // x264
            // -------------------------
            else if (codec_SelectedItem == "x264")
            {
                // Codec
                x264.Codec_Set(vm);

                // Items Source
                x264.Controls_ItemsSource(vm);
                // Selected Items
                x264.Controls_Selected(vm);

                // Expanded
                x264.Controls_Expanded(vm);
                // Collapsed
                x264.Controls_Collapsed(vm);

                // Checked
                x264.Controls_Checked(vm);
                // Unhecked
                x264.Controls_Unhecked(vm);

                // Enabled
                x264.Controls_Enable(vm);
                // Disabled
                x264.Controls_Disable(vm);
            }

            // -------------------------
            // x265
            // -------------------------
            else if (codec_SelectedItem == "x265")
            {
                // Codec
                x265.Codec_Set(vm);

                // Items Source
                x265.Controls_ItemsSource(vm);
                // Selected Items
                x265.Controls_Selected(vm);

                // Expanded
                x265.Controls_Expanded(vm);
                // Collapsed
                x265.Controls_Collapsed(vm);

                // Checked
                x265.Controls_Checked(vm);
                // Unhecked
                x265.Controls_Unhecked(vm);

                // Enabled
                x265.Controls_Enable(vm);
                // Disabled
                x265.Controls_Disable(vm);
            }

            // -------------------------
            // AV1
            // -------------------------
            else if (codec_SelectedItem == "AV1")
            {
                // Codec
                AV1.Codec_Set(vm);

                // Items Source
                AV1.Controls_ItemsSource(vm);
                // Selected Items
                AV1.Controls_Selected(vm);

                // Expanded
                AV1.Controls_Expanded(vm);
                // Collapsed
                AV1.Controls_Collapsed(vm);

                // Checked
                AV1.Controls_Checked(vm);
                // Unhecked
                AV1.Controls_Unhecked(vm);

                // Enabled
                AV1.Controls_Enable(vm);
                // Disabled
                AV1.Controls_Disable(vm);
            }

            // -------------------------
            // FFV1
            // -------------------------
            else if (codec_SelectedItem == "FFV1")
            {
                // Codec
                FFV1.Codec_Set(vm);

                // Items Source
                FFV1.Controls_ItemsSource(vm);
                // Selected Items
                FFV1.Controls_Selected(vm);

                // Expanded
                FFV1.Controls_Expanded(vm);
                // Collapsed
                FFV1.Controls_Collapsed(vm);

                // Checked
                FFV1.Controls_Checked(vm);
                // Unhecked
                FFV1.Controls_Unhecked(vm);

                // Enabled
                FFV1.Controls_Enable(vm);
                // Disabled
                FFV1.Controls_Disable(vm);
            }

            // -------------------------
            // HuffYUV
            // -------------------------
            else if (codec_SelectedItem == "HuffYUV")
            {
                // Codec
                HuffYUV.Codec_Set(vm);

                // Items Source
                HuffYUV.Controls_ItemsSource(vm);
                // Selected Items
                HuffYUV.Controls_Selected(vm);

                // Expanded
                HuffYUV.Controls_Expanded(vm);
                // Collapsed
                HuffYUV.Controls_Collapsed(vm);

                // Checked
                HuffYUV.Controls_Checked(vm);
                // Unhecked
                HuffYUV.Controls_Unhecked(vm);

                // Enabled
                HuffYUV.Controls_Enable(vm);
                // Disabled
                HuffYUV.Controls_Disable(vm);
            }

            // -------------------------
            // Theora
            // -------------------------
            else if (codec_SelectedItem == "Theora")
            {
                // Codec
                Theora.Codec_Set(vm);

                // Items Source
                Theora.Controls_ItemsSource(vm);
                // Selected Items
                Theora.Controls_Selected(vm);

                // Expanded
                Theora.Controls_Expanded(vm);
                // Collapsed
                Theora.Controls_Collapsed(vm);

                // Checked
                Theora.Controls_Checked(vm);
                // Unhecked
                Theora.Controls_Unhecked(vm);

                // Enabled
                Theora.Controls_Enable(vm);
                // Disabled
                Theora.Controls_Disable(vm);
            }

            // -------------------------
            // MPEG-2
            // -------------------------
            else if (codec_SelectedItem == "MPEG-2")
            {
                // Codec
                MPEG_2.Codec_Set(vm);

                // Items Source
                MPEG_2.Controls_ItemsSource(vm);
                // Selected Items
                MPEG_2.Controls_Selected(vm);

                // Expanded
                MPEG_2.Controls_Expanded(vm);
                // Collapsed
                MPEG_2.Controls_Collapsed(vm);

                // Checked
                MPEG_2.Controls_Checked(vm);
                // Unhecked
                MPEG_2.Controls_Unhecked(vm);

                // Enabled
                MPEG_2.Controls_Enable(vm);
                // Disabled
                MPEG_2.Controls_Disable(vm);
            }

            // -------------------------
            // MPEG-4
            // -------------------------
            else if (codec_SelectedItem == "MPEG-4")
            {
                // Codec
                MPEG_4.Codec_Set(vm);

                // Items Source
                MPEG_4.Controls_ItemsSource(vm);
                // Selected Items
                MPEG_4.Controls_Selected(vm);

                // Expanded
                MPEG_4.Controls_Expanded(vm);
                // Collapsed
                MPEG_4.Controls_Collapsed(vm);

                // Checked
                MPEG_4.Controls_Checked(vm);
                // Unhecked
                MPEG_4.Controls_Unhecked(vm);

                // Enabled
                MPEG_4.Controls_Enable(vm);
                // Disabled
                MPEG_4.Controls_Disable(vm);
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
                JPEG.Codec_Set(vm);

                // Items Source
                JPEG.Controls_ItemsSource(vm);
                // Selected Items
                JPEG.Controls_Selected(vm);

                // Expanded
                JPEG.Controls_Expanded(vm);
                // Collapsed
                JPEG.Controls_Collapsed(vm);

                // Checked
                JPEG.Controls_Checked(vm);
                // Unhecked
                JPEG.Controls_Unhecked(vm);

                // Enabled
                JPEG.Controls_Enable(vm);
                // Disabled
                JPEG.Controls_Disable(vm);
            }

            // -------------------------
            // PNG
            // -------------------------
            else if (codec_SelectedItem == "PNG")
            {
                // Codec
                PNG.Codec_Set(vm);

                // Items Source
                PNG.Controls_ItemsSource(vm);
                // Selected Items
                PNG.Controls_Selected(vm);

                // Expanded
                PNG.Controls_Expanded(vm);
                // Collapsed
                PNG.Controls_Collapsed(vm);

                // Checked
                PNG.Controls_Checked(vm);
                // Unhecked
                PNG.Controls_Unhecked(vm);

                // Enabled
                PNG.Controls_Enable(vm);
                // Disabled
                PNG.Controls_Disable(vm);
            }

            // -------------------------
            // WebP
            // -------------------------
            else if (codec_SelectedItem == "WebP")
            {
                // Codec
                WebP.Codec_Set(vm);

                // Items Source
                WebP.Controls_ItemsSource(vm);
                // Selected Items
                WebP.Controls_Selected(vm);

                // Expanded
                WebP.Controls_Expanded(vm);
                // Collapsed
                WebP.Controls_Collapsed(vm);

                // Checked
                WebP.Controls_Checked(vm);
                // Unhecked
                WebP.Controls_Unhecked(vm);

                // Enabled
                WebP.Controls_Enable(vm);
                // Disabled
                WebP.Controls_Disable(vm);
            }

            // -------------------------
            // Copy
            // -------------------------
            else if (codec_SelectedItem == "Copy")
            {
                // Codec
                VideoCopy.Codec_Set(vm);

                // Items Source
                VideoCopy.Controls_ItemsSource(vm);
                // Selected Items
                VideoCopy.Controls_Selected(vm);

                // Expanded
                VideoCopy.Controls_Expanded(vm);
                // Collapsed
                VideoCopy.Controls_Collapsed(vm);

                // Checked
                VideoCopy.Controls_Checked(vm);
                // Unhecked
                VideoCopy.Controls_Unhecked(vm);

                // Enabled
                VideoCopy.Controls_Enable(vm);
                // Disabled
                VideoCopy.Controls_Disable(vm);
            }

            // -------------------------
            // None
            // -------------------------
            else if (codec_SelectedItem == "None")
            {
                // Codec
                VideoNone.Codec_Set(vm);

                // Items Source
                VideoNone.Controls_ItemsSource(vm);
                // Selected Items
                VideoNone.Controls_Selected(vm);

                // Expanded
                VideoNone.Controls_Expanded(vm);
                // Collapsed
                VideoNone.Controls_Collapsed(vm);

                // Checked
                VideoNone.Controls_Checked(vm);
                // Unhecked
                VideoNone.Controls_Unhecked(vm);

                // Enabled
                VideoNone.Controls_Enable(vm);
                // Disabled
                VideoNone.Controls_Disable(vm);
            }

            // --------------------------------------------------
            // Default Selected Item
            // Previous Items set in ViewModel _SelectedItem
            // --------------------------------------------------

            // -------------------------
            // Video Encode Speed Selected Item
            // -------------------------
            if (!string.IsNullOrEmpty(vm.Video_EncodeSpeed_SelectedItem) &&
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
                vm.Video_Quality_SelectedItem != "None")
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
            // Problem, do not use, selects Web in mp4 when coming from webm
            //if (!string.IsNullOrEmpty(vm.Video_Optimize_SelectedItem) &&
            //    vm.Video_Optimize_SelectedItem != "None")
            //{
            //    MainWindow.VideoOptimize_PreviousItem = vm.Video_Optimize_SelectedItem;
            //}

            //vm.Video_Optimize_SelectedItem = MainWindow.SelectedItem(vm.Video_Optimize_Items.Select(c => c.Name).ToList(),
            //                                                         MainWindow.VideoOptimize_PreviousItem
            //                                                         );
        }



        /// <summary>
        ///    BitRate Display
        /// </summary>
        public static void VideoBitRateDisplay(ViewModel vm,
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
                    vm.Video_BitRate_Text = string.Empty;
                    vm.Video_MinRate_Text = string.Empty;
                    vm.Video_MaxRate_Text = string.Empty;
                    vm.Video_BufSize_Text = string.Empty;
                }

                // -------------------------
                // CRF
                // -------------------------
                else if (selectedPass == "CRF")
                {
                    // VP8/VP9 CRF is combined with BitRate e.g. -b:v 2000K -crf 16
                    // Other Codecs just use CRF

                    // CRF BitRate
                    vm.Video_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.Video_CRF_BitRate;

                    // CRF
                    vm.Video_CRF_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.CRF;

                    // MinRate
                    vm.Video_MinRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.MinRate;

                    // MaxRate
                    vm.Video_MaxRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.MaxRate;

                    // BufSize
                    vm.Video_BufSize_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.BufSize;
                }

                // -------------------------
                // BitRate
                // -------------------------
                else if (selectedPass == "1 Pass" ||
                         selectedPass == "2 Pass")
                {
                    // CRF
                    vm.Video_CRF_Text = string.Empty;

                    // BitRate CBR
                    if (vm.Video_VBR_IsChecked == false)
                    {
                        vm.Video_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.CBR;
                    }

                    // BitRate VBR
                    else if (vm.Video_VBR_IsChecked == true)
                    {
                        vm.Video_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.VBR;
                    }

                    // MinRate
                    vm.Video_MinRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.MinRate;

                    // MaxRate
                    vm.Video_MaxRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.MaxRate;

                    // BufSize
                    vm.Video_BufSize_Text = items.FirstOrDefault(item => item.Name == selectedQuality) ?.BufSize;
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
                // Enable and Clear BitRate Text Display

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

                // BitRate
                vm.Video_BitRate_IsEnabled = true;
                vm.Video_BitRate_Text = "";

                // VBR
                vm.Video_VBR_IsEnabled = true;

                // MinRate
                vm.Video_MinRate_IsEnabled = true;
                vm.Video_MinRate_Text = "";

                // MaxRate
                vm.Video_MaxRate_IsEnabled = true;
                vm.Video_MaxRate_Text = "";

                // BufSize
                vm.Video_BufSize_IsEnabled = true;
                vm.Video_BufSize_Text = "";

                // Size
                vm.Video_Scale_IsEnabled = true;
            }

            // -------------------------
            // Auto
            // -------------------------
            else if (vm.Video_Quality_SelectedItem == "Auto")
            {
                // Disable and Clear BitRate Text Dispaly

                // Pass
                vm.Video_Pass_IsEnabled = false;

                // CRF
                vm.Video_CRF_IsEnabled = false;
                vm.Video_CRF_Text = "";

                // BitRate
                vm.Video_BitRate_IsEnabled = false;
                vm.Video_BitRate_Text = "";

                // VBR
                vm.Video_VBR_IsEnabled = false;
                vm.Video_VBR_IsChecked = false;

                // MinRate
                vm.Video_MinRate_IsEnabled = false;
                vm.Video_MinRate_Text = "";

                // MaxRate
                vm.Video_MaxRate_IsEnabled = false;
                vm.Video_MaxRate_Text = "";

                // BufSize
                vm.Video_BufSize_IsEnabled = false;
                vm.Video_BufSize_Text = "";

                // Size
                vm.Video_Scale_IsEnabled = true;
            }

            // -------------------------
            // None
            // -------------------------
            else if (vm.Video_Quality_SelectedItem == "None")
            {
                // BitRate Text is Displayed through VideoBitRateDisplay()

                // Pass
                vm.Video_Pass_IsEnabled = false; 

                // CRF
                vm.Video_CRF_IsEnabled = false;

                // BitRate
                vm.Video_BitRate_IsEnabled = false;
                // VBR
                vm.Video_VBR_IsEnabled = false;
                // MinRate
                vm.Video_MinRate_IsEnabled = false;
                // MaxRate
                vm.Video_MaxRate_IsEnabled = false;
                // BufSize
                vm.Video_BufSize_IsEnabled = false;

                // Size
                vm.Video_Scale_IsEnabled = false;
            }

            // -------------------------
            // All Other Qualities
            // -------------------------
            else
            {
                // BitRate Text is Displayed through VideoBitRateDisplay()

                // Pass
                vm.Video_Pass_IsEnabled = true; // always enabled

                // CRF
                vm.Video_CRF_IsEnabled = false;

                // BitRate
                vm.Video_BitRate_IsEnabled = false;

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

                // MinRate
                vm.Video_MinRate_IsEnabled = false;

                // MaxRate
                vm.Video_MaxRate_IsEnabled = false;

                // BufSize
                vm.Video_BufSize_IsEnabled = false;

                // Size
                vm.Video_Scale_IsEnabled = true;
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
                    vm.Video_PixelFormat_SelectedItem = "yuv444p10le";
                }

                // -------------------------
                // HuffYUV
                // -------------------------
                else if (codec == "HuffYUV")
                {
                    vm.Video_PixelFormat_SelectedItem = "yuv444p";
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

                    // Lossless
                    if (quality == "Lossless")
                    {
                        vm.Video_PixelFormat_SelectedItem = "bgra";
                    }
                    // All Other Quality
                    else
                    {
                        vm.Video_PixelFormat_SelectedItem = "yuva420p";
                    }
                }

                // -------------------------
                // Excluded Codecs
                // -------------------------
                else if (codec == "PNG")
                {
                    vm.Video_PixelFormat_IsEnabled = true;

                    // Lossless
                    if (quality == "Lossless")
                    {
                        vm.Video_PixelFormat_SelectedItem = "rgba";
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
        public static void OptimizeControls(ViewModel vm)
        {
            // -------------------------
            // Only for x264 & x265 Video Codecs
            // -------------------------
            if (vm.Video_Codec_SelectedItem == "x264" ||
                vm.Video_Codec_SelectedItem == "x265")
            {
                // -------------------------
                // Enable - Tune, Profile, Level
                // -------------------------
                // Custom
                if (vm.Video_Optimize_SelectedItem == "Custom")
                {
                    // Tune
                    vm.Optimize_Tune_IsEnabled = true;

                    // Profile
                    vm.Optimize_Profile_IsEnabled = true;

                    // Level
                    vm.Video_Optimize_Level_IsEnabled = true;
                }
                // -------------------------
                // Disable - Tune, Profile, Level
                // -------------------------
                // Web, PC HD, HEVC, None, etc.
                else
                {
                    // Tune
                    vm.Optimize_Tune_IsEnabled = false;

                    // Profile
                    vm.Optimize_Profile_IsEnabled = false;

                    // Level
                    vm.Video_Optimize_Level_IsEnabled = false;
                }
            }

            // -------------------------
            // Disable Tune, Profile, Level if Codec not x264/x265
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
            // Video
            // --------------------------------------------------
            // -------------------------
            // VP8
            // -------------------------
            if (vm.Video_Codec_SelectedItem == "VP8")
            {
                VP8.EncodingPass(vm);
            }
            // -------------------------
            // VP9
            // -------------------------
            else if (vm.Video_Codec_SelectedItem == "VP9")
            {
                VP9.EncodingPass(vm);
            }
            // -------------------------
            // x264
            // -------------------------
            else if (vm.Video_Codec_SelectedItem == "x264")
            {
                x264.EncodingPass(vm);
            }
            // -------------------------
            // x265
            // -------------------------
            else if (vm.Video_Codec_SelectedItem == "x265")
            {
                x265.EncodingPass(vm);
            }
            // -------------------------
            // AV1
            // -------------------------
            else if (vm.Video_Codec_SelectedItem == "AV1")
            {
                AV1.EncodingPass(vm);
            }
            // -------------------------
            // FFV1
            // -------------------------
            else if (vm.Video_Codec_SelectedItem == "FFV1")
            {
                FFV1.EncodingPass(vm);
            }
            // -------------------------
            // HuffYUV
            // -------------------------
            else if (vm.Video_Codec_SelectedItem == "HuffYUV")
            {
                HuffYUV.EncodingPass(vm);
            }
            // -------------------------
            // Theora
            // -------------------------
            else if (vm.Video_Codec_SelectedItem == "Theora")
            {
                Theora.EncodingPass(vm);
            }
            // -------------------------
            // MPEG-2
            // -------------------------
            else if (vm.Video_Codec_SelectedItem == "MPEG-2")
            {
                MPEG_2.EncodingPass(vm);
            }
            // -------------------------
            // MPEG-4
            // -------------------------
            else if (vm.Video_Codec_SelectedItem == "MPEG-4")
            {
                MPEG_4.EncodingPass(vm);
            }

            // --------------------------------------------------
            // Image
            // --------------------------------------------------
            // -------------------------
            // JPEG
            // -------------------------
            else if (vm.Video_Codec_SelectedItem == "JPEG")
            {
                JPEG.EncodingPass(vm);
            }
            // -------------------------
            // PNG
            // -------------------------
            else if (vm.Video_Codec_SelectedItem == "PNG")
            {
                PNG.EncodingPass(vm);
            }
            // -------------------------
            // WebP
            // -------------------------
            else if (vm.Video_Codec_SelectedItem == "WebP")
            {
                WebP.EncodingPass(vm);
            }

            // --------------------------------------------------
            // Other
            // --------------------------------------------------
            // -------------------------
            // Copy
            // -------------------------
            else if (vm.Video_Codec_SelectedItem == "Copy")
            {
                VideoCopy.EncodingPass(vm);
            }
            // -------------------------
            // None
            // -------------------------
            else if (vm.Video_Codec_SelectedItem == "None")
            {
                VideoNone.EncodingPass(vm);
            }


            // -------------------------
            // CRF TextBox
            // -------------------------
            if (vm.Video_Quality_SelectedItem == "Custom")
            {
                // Disable
                if (vm.Video_Pass_SelectedItem == "CRF")
                {
                    vm.Video_CRF_IsEnabled = true;
                }
                // Enable
                else if (vm.Video_Pass_SelectedItem == "1 Pass" ||
                         vm.Video_Pass_SelectedItem == "2 Pass" ||
                         vm.Video_Pass_SelectedItem == "auto")
                {
                    vm.Video_CRF_IsEnabled = false;
                }
            }

        }



        /// <summary>
        ///    Auto Copy Conditions Check
        /// <summary>
        public static bool AutoCopyConditionsCheck(ViewModel vm, 
                                                   string inputExt, 
                                                   string outputExt)
        {
            // Input Extension is Same as Output Extension and Video Quality is Auto
            // Note: Aspect Ratio -aspect can be applied to Copy
            if (vm.Video_Quality_SelectedItem == "Auto" &&
                vm.Video_PixelFormat_SelectedItem == "auto" &&
                string.IsNullOrEmpty(CropWindow.crop) &&
                vm.Video_Scale_SelectedItem == "Source" &&
                vm.Video_ScalingAlgorithm_SelectedItem == "auto" &&
                // Do not add Aspect Ratio -aspect, it can be used with Copy
                vm.Video_FPS_SelectedItem == "auto" &&
                vm.Video_Optimize_SelectedItem == "None" &&

                // Filters
                // Fix
                vm.FilterVideo_Deband_SelectedItem == "disabled" &&
                vm.FilterVideo_Deshake_SelectedItem == "disabled" &&
                vm.FilterVideo_Deflicker_SelectedItem == "disabled" &&
                vm.FilterVideo_Dejudder_SelectedItem == "disabled" &&
                vm.FilterVideo_Denoise_SelectedItem == "disabled" &&
                vm.FilterVideo_Deinterlace_SelectedItem == "disabled" &&
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
