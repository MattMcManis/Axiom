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
using System.IO;
using System.Linq;
using System.Windows;
using ViewModel;
using Axiom;
using System.Collections.ObjectModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Controls
{
    namespace Video
    {
        public class Controls
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
                        Codec.VP8.Codec_Set();

                        // Items Source
                        Codec.VP8.Controls_ItemsSource();
                        // Selected Items
                        Codec.VP8.Controls_Selected();

                        // Expanded
                        Codec.VP8.Controls_Expanded();
                        // Collapsed
                        Codec.VP8.Controls_Collapsed();

                        // Checked
                        Codec.VP8.Controls_Checked();
                        // Unhecked
                        Codec.VP8.Controls_Unhecked();

                        // Enabled
                        Codec.VP8.Controls_Enable();
                        // Disabled
                        Codec.VP8.Controls_Disable();
                        break;

                    // -------------------------
                    // VP9
                    // -------------------------
                    case "VP9":
                        // Codec
                        Codec.VP9.Codec_Set();

                        // Items Source
                        Codec.VP9.Controls_ItemsSource();
                        // Selected Items
                        Codec.VP9.Controls_Selected();

                        // Expanded
                        Codec.VP9.Controls_Expanded();
                        // Collapsed
                        Codec.VP9.Controls_Collapsed();

                        // Checked
                        Codec.VP9.Controls_Checked();
                        // Unhecked
                        Codec.VP9.Controls_Unhecked();

                        // Enabled
                        Codec.VP9.Controls_Enable();
                        // Disabled
                        Codec.VP9.Controls_Disable();
                        break;

                    // -------------------------
                    // x264
                    // -------------------------
                    case "x264":
                        // Codec
                        Codec.x264.Codec_Set();

                        // Items Source
                        Codec.x264.Controls_ItemsSource();
                        // Selected Items
                        Codec.x264.Controls_Selected();

                        // Expanded
                        Codec.x264.Controls_Expanded();
                        // Collapsed
                        Codec.x264.Controls_Collapsed();

                        // Checked
                        Codec.x264.Controls_Checked();
                        // Unhecked
                        Codec.x264.Controls_Unhecked();

                        // Enabled
                        Codec.x264.Controls_Enable();
                        // Disabled
                        Codec.x264.Controls_Disable();
                        break;

                    // -------------------------
                    // x265
                    // -------------------------
                    case "x265":
                        // Codec
                        Codec.x265.Codec_Set();

                        // Items Source
                        Codec.x265.Controls_ItemsSource();
                        // Selected Items
                        Codec.x265.Controls_Selected();

                        // Expanded
                        Codec.x265.Controls_Expanded();
                        // Collapsed
                        Codec.x265.Controls_Collapsed();

                        // Checked
                        Codec.x265.Controls_Checked();
                        // Unhecked
                        Codec.x265.Controls_Unhecked();

                        // Enabled
                        Codec.x265.Controls_Enable();
                        // Disabled
                        Codec.x265.Controls_Disable();
                        break;

                    // -------------------------
                    // AV1
                    // -------------------------
                    case "AV1":
                        // Codec
                        Codec.AV1.Codec_Set();

                        // Items Source
                        Codec.AV1.Controls_ItemsSource();
                        // Selected Items
                        Codec.AV1.Controls_Selected();

                        // Expanded
                        Codec.AV1.Controls_Expanded();
                        // Collapsed
                        Codec.AV1.Controls_Collapsed();

                        // Checked
                        Codec.AV1.Controls_Checked();
                        // Unhecked
                        Codec.AV1.Controls_Unhecked();

                        // Enabled
                        Codec.AV1.Controls_Enable();
                        // Disabled
                        Codec.AV1.Controls_Disable();
                        break;

                    // -------------------------
                    // FFV1
                    // -------------------------
                    case "FFV1":
                        // Codec
                        Codec.FFV1.Codec_Set();

                        // Items Source
                        Codec.FFV1.Controls_ItemsSource();
                        // Selected Items
                        Codec.FFV1.Controls_Selected();

                        // Expanded
                        Codec.FFV1.Controls_Expanded();
                        // Collapsed
                        Codec.FFV1.Controls_Collapsed();

                        // Checked
                        Codec.FFV1.Controls_Checked();
                        // Unhecked
                        Codec.FFV1.Controls_Unhecked();

                        // Enabled
                        Codec.FFV1.Controls_Enable();
                        // Disabled
                        Codec.FFV1.Controls_Disable();
                        break;

                    // -------------------------
                    // MagicYUV
                    // -------------------------
                    case "MagicYUV":
                        // Codec
                        Codec.MagicYUV.Codec_Set();

                        // Items Source
                        Codec.MagicYUV.Controls_ItemsSource();
                        // Selected Items
                        Codec.MagicYUV.Controls_Selected();

                        // Expanded
                        Codec.MagicYUV.Controls_Expanded();
                        // Collapsed
                        Codec.MagicYUV.Controls_Collapsed();

                        // Checked
                        Codec.MagicYUV.Controls_Checked();
                        // Unhecked
                        Codec.MagicYUV.Controls_Unhecked();

                        // Enabled
                        Codec.MagicYUV.Controls_Enable();
                        // Disabled
                        Codec.MagicYUV.Controls_Disable();
                        break;

                    // -------------------------
                    // HuffYUV
                    // -------------------------
                    case "HuffYUV":
                        // Codec
                        Codec.HuffYUV.Codec_Set();

                        // Items Source
                        Codec.HuffYUV.Controls_ItemsSource();
                        // Selected Items
                        Codec.HuffYUV.Controls_Selected();

                        // Expanded
                        Codec.HuffYUV.Controls_Expanded();
                        // Collapsed
                        Codec.HuffYUV.Controls_Collapsed();

                        // Checked
                        Codec.HuffYUV.Controls_Checked();
                        // Unhecked
                        Codec.HuffYUV.Controls_Unhecked();

                        // Enabled
                        Codec.HuffYUV.Controls_Enable();
                        // Disabled
                        Codec.HuffYUV.Controls_Disable();
                        break;

                    // -------------------------
                    // Theora
                    // -------------------------
                    case "Theora":
                        // Codec
                        Codec.Theora.Codec_Set();

                        // Items Source
                        Codec.Theora.Controls_ItemsSource();
                        // Selected Items
                        Codec.Theora.Controls_Selected();

                        // Expanded
                        Codec.Theora.Controls_Expanded();
                        // Collapsed
                        Codec.Theora.Controls_Collapsed();

                        // Checked
                        Codec.Theora.Controls_Checked();
                        // Unhecked
                        Codec.Theora.Controls_Unhecked();

                        // Enabled
                        Codec.Theora.Controls_Enable();
                        // Disabled
                        Codec.Theora.Controls_Disable();
                        break;

                    // -------------------------
                    // MPEG-2
                    // -------------------------
                    case "MPEG-2":
                        // Codec
                        Codec.MPEG_2.Codec_Set();

                        // Items Source
                        Codec.MPEG_2.Controls_ItemsSource();
                        // Selected Items
                        Codec.MPEG_2.Controls_Selected();

                        // Expanded
                        Codec.MPEG_2.Controls_Expanded();
                        // Collapsed
                        Codec.MPEG_2.Controls_Collapsed();

                        // Checked
                        Codec.MPEG_2.Controls_Checked();
                        // Unhecked
                        Codec.MPEG_2.Controls_Unhecked();

                        // Enabled
                        Codec.MPEG_2.Controls_Enable();
                        // Disabled
                        Codec.MPEG_2.Controls_Disable();
                        break;

                    // -------------------------
                    // MPEG-4
                    // -------------------------
                    case "MPEG-4":
                        // Codec
                        Codec.MPEG_4.Codec_Set();

                        // Items Source
                        Codec.MPEG_4.Controls_ItemsSource();
                        // Selected Items
                        Codec.MPEG_4.Controls_Selected();

                        // Expanded
                        Codec.MPEG_4.Controls_Expanded();
                        // Collapsed
                        Codec.MPEG_4.Controls_Collapsed();

                        // Checked
                        Codec.MPEG_4.Controls_Checked();
                        // Unhecked
                        Codec.MPEG_4.Controls_Unhecked();

                        // Enabled
                        Codec.MPEG_4.Controls_Enable();
                        // Disabled
                        Codec.MPEG_4.Controls_Disable();
                        break;


                    // --------------------------------------------------
                    // Image
                    // --------------------------------------------------
                    // -------------------------
                    // JPEG
                    // -------------------------
                    case "JPEG":
                        // Codec
                        Image.Codec.JPEG.Codec_Set();

                        // Items Source
                        Image.Codec.JPEG.Controls_ItemsSource();
                        // Selected Items
                        Image.Codec.JPEG.Controls_Selected();

                        // Expanded
                        Image.Codec.JPEG.Controls_Expanded();
                        // Collapsed
                        Image.Codec.JPEG.Controls_Collapsed();

                        // Checked
                        Image.Codec.JPEG.Controls_Checked();
                        // Unhecked
                        Image.Codec.JPEG.Controls_Unhecked();

                        // Enabled
                        Image.Codec.JPEG.Controls_Enable();
                        // Disabled
                        Image.Codec.JPEG.Controls_Disable();
                        break;

                    // -------------------------
                    // PNG
                    // -------------------------
                    case "PNG":
                        // Codec
                        Image.Codec.PNG.Codec_Set();

                        // Items Source
                        Image.Codec.PNG.Controls_ItemsSource();
                        // Selected Items
                        Image.Codec.PNG.Controls_Selected();

                        // Expanded
                        Image.Codec.PNG.Controls_Expanded();
                        // Collapsed
                        Image.Codec.PNG.Controls_Collapsed();

                        // Checked
                        Image.Codec.PNG.Controls_Checked();
                        // Unhecked
                        Image.Codec.PNG.Controls_Unhecked();

                        // Enabled
                        Image.Codec.PNG.Controls_Enable();
                        // Disabled
                        Image.Codec.PNG.Controls_Disable();
                        break;

                    // -------------------------
                    // WebP
                    // -------------------------
                    case "WebP":
                        // Codec
                        Image.Codec.WebP.Codec_Set();

                        // Items Source
                        Image.Codec.WebP.Controls_ItemsSource();
                        // Selected Items
                        Image.Codec.WebP.Controls_Selected();

                        // Expanded
                        Image.Codec.WebP.Controls_Expanded();
                        // Collapsed
                        Image.Codec.WebP.Controls_Collapsed();

                        // Checked
                        Image.Codec.WebP.Controls_Checked();
                        // Unhecked
                        Image.Codec.WebP.Controls_Unhecked();

                        // Enabled
                        Image.Codec.WebP.Controls_Enable();
                        // Disabled
                        Image.Codec.WebP.Controls_Disable();
                        break;

                    // -------------------------
                    // Copy
                    // -------------------------
                    case "Copy":
                        // Codec
                        Codec.Copy.Codec_Set();

                        // Items Source
                        Codec.Copy.Controls_ItemsSource();
                        // Selected Items
                        Codec.Copy.Controls_Selected();

                        // Expanded
                        Codec.Copy.Controls_Expanded();
                        // Collapsed
                        Codec.Copy.Controls_Collapsed();

                        // Checked
                        Codec.Copy.Controls_Checked();
                        // Unhecked
                        Codec.Copy.Controls_Unhecked();

                        // Enabled
                        Codec.Copy.Controls_Enable();
                        // Disabled
                        Codec.Copy.Controls_Disable();
                        break;

                    // -------------------------
                    // None
                    // -------------------------
                    case "None":
                        // Codec
                        Codec.None.Codec_Set();

                        // Items Source
                        Codec.None.Controls_ItemsSource();
                        // Selected Items
                        Codec.None.Controls_Selected();

                        // Expanded
                        Codec.None.Controls_Expanded();
                        // Collapsed
                        Codec.None.Controls_Collapsed();

                        // Checked
                        Codec.None.Controls_Checked();
                        // Unhecked
                        Codec.None.Controls_Unhecked();

                        // Enabled
                        Codec.None.Controls_Enable();
                        // Disabled
                        Codec.None.Controls_Disable();
                        break;
                }

                // --------------------------------------------------
                // Default Selected Item
                // --------------------------------------------------

                // -------------------------
                // Video Encode Speed Selected Item
                // -------------------------
                // Save the Previous Codec's Item
                if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_EncodeSpeed_SelectedItem) &&
                    !string.Equals(VM.VideoView.Video_EncodeSpeed_SelectedItem, "auto", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(VM.VideoView.Video_EncodeSpeed_SelectedItem, "none", StringComparison.OrdinalIgnoreCase))
                    //VM.VideoView.Video_EncodeSpeed_SelectedItem.ToLower() != "auto" && // Auto / auto
                    //VM.VideoView.Video_EncodeSpeed_SelectedItem.ToLower() != "none") // None / none
                {
                    MainWindow.Video_EncodeSpeed_PreviousItem = VM.VideoView.Video_EncodeSpeed_SelectedItem;
                }

                // Select the Prevoius Codec's Item if available
                // If missing Select Default to First Item
                // Ignore Codec Copy
                //if (VM.VideoView.Video_Codec_SelectedItem != "Copy")
                //{
                VM.VideoView.Video_EncodeSpeed_SelectedItem = MainWindow.SelectedItem(VM.VideoView.Video_EncodeSpeed_Items.Select(c => c.Name).ToList(),
                                                                                      MainWindow.Video_EncodeSpeed_PreviousItem
                                                                                      );
                //}

                // -------------------------
                // Video Quality Selected Item
                // -------------------------
                //// Save the Previous Codec's Item
                //if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_Quality_SelectedItem) &&
                //    VM.VideoView.Video_Quality_SelectedItem.ToLower() != "auto" && // Auto / auto
                //    VM.VideoView.Video_Quality_SelectedItem.ToLower() != "none") // None / none
                //{
                //    MainWindow.Video_Quality_PreviousItem = VM.VideoView.Video_Quality_SelectedItem;
                //}

                //// Select the Prevoius Codec's Item if available
                //// If missing Select Default to First Item
                //// Ignore Codec Copy
                //if (VM.VideoView.Video_Codec_SelectedItem != "Copy")
                //{
                //    VM.VideoView.Video_Quality_SelectedItem = MainWindow.SelectedItem(VM.VideoView.Video_Quality_Items.Select(c => c.Name).ToList(),
                //                                                                  MainWindow.Video_Quality_PreviousItem
                //                                                                  );
                //}

                // -------------------------
                // Video Pass Selected Item
                // -------------------------

                // -------------------------
                // Video Optimize Selected Item
                // -------------------------
                // Problem, do not use, selects Web in mp4 when coming from webm

                // -------------------------
                // Video Quality Selected Item Null Check
                // -------------------------
                // For errors causing ComboBox not to select an item
                if (string.IsNullOrWhiteSpace(VM.VideoView.Video_Quality_SelectedItem))
                {
                    // Default to First Item
                    VM.VideoView.Video_Quality_Items.FirstOrDefault();
                }
            }



            /// <summary>
            /// BitRate Display
            /// </summary>
            public static void VideoBitRateDisplay(ObservableCollection<ViewModel.Video.VideoQuality> items,
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
                        VM.VideoView.Video_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.Video_CRF_BitRate;

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

                        switch (VM.VideoView.Video_VBR_IsChecked)
                        {
                            // Bit Rate CBR
                            case false:
                                VM.VideoView.Video_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.CBR;
                                break;

                            // Bit Rate VBR
                            case true:
                                VM.VideoView.Video_BitRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.VBR;
                                break;
                        }

                        // MinRate
                        VM.VideoView.Video_MinRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.MinRate;

                        // MaxRate
                        VM.VideoView.Video_MaxRate_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.MaxRate;

                        // BufSize
                        VM.VideoView.Video_BufSize_Text = items.FirstOrDefault(item => item.Name == selectedQuality)?.BufSize;
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

                switch (VM.VideoView.Video_Quality_SelectedItem)
                {
                    // -------------------------
                    // Custom
                    // -------------------------
                    case "Custom":
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
                        break;

                    // -------------------------
                    // Auto
                    // -------------------------
                    case "Auto":
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
                        break;

                    // -------------------------
                    // None
                    // -------------------------
                    case "None":
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
                        break;

                    // -------------------------
                    // All Other Qualities
                    // -------------------------
                    default:
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
                            VM.VideoView.Video_Codec_SelectedItem == "MagicYUV" ||
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
                        break;
                }
            }


            /// <summary>
            /// Pixel Format Controls
            /// </summary>
            //public static void PixelFormatControls(string mediaType_SelectedItem,
            //                                       string codec_SelectedItem,
            //                                       string quality_SelectedItem
            //                                       )
            //{
            //    // -------------------------
            //    // MediaTypeControls
            //    // ------------------------- 
            //    if (mediaType_SelectedItem == "Video" ||
            //        mediaType_SelectedItem == "Image" ||
            //        mediaType_SelectedItem == "Sequence")
            //    {
            //        switch (codec_SelectedItem)
            //        {
            //            // -------------------------
            //            // VP8
            //            // -------------------------
            //            case "VP8":
            //                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
            //                break;

            //            // -------------------------
            //            // VP9
            //            // -------------------------
            //            case "VP9":
            //                // Lossless
            //                if (quality_SelectedItem == "Lossless")
            //                {
            //                    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv444p";
            //                }
            //                // All Other Quality
            //                else
            //                {
            //                    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
            //                }
            //                break;

            //            // -------------------------
            //            // x264
            //            // -------------------------
            //            case "x264":
            //                // Lossless
            //                if (quality_SelectedItem == "Lossless")
            //                {
            //                    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv444p";
            //                }
            //                // All Other Quality
            //                else
            //                {
            //                    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
            //                }
            //                break;

            //            // -------------------------
            //            // x265
            //            // -------------------------
            //            case "x265":
            //                // Lossless
            //                if (quality_SelectedItem == "Lossless")
            //                {
            //                    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv444p";
            //                }
            //                // All Other Quality
            //                else
            //                {
            //                    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
            //                }
            //                break;

            //            // -------------------------
            //            // AV1
            //            // -------------------------
            //            case "AV1":
            //                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
            //                break;

            //            // -------------------------
            //            // FFV1
            //            // -------------------------
            //            case "FFV1":
            //                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv444p10le";
            //                break;

            //            // -------------------------
            //            // MagicYUV
            //            // -------------------------
            //            case "MagicYUV":
            //                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv444p";
            //                break;

            //            // -------------------------
            //            // HuffYUV
            //            // -------------------------
            //            case "HuffYUV":
            //                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv444p";
            //                break;

            //            // -------------------------
            //            // MPEG-2
            //            // -------------------------
            //            case "MPEG-2":
            //                // Lossless can't be yuv444p
            //                // All Pixel Formats must be yuv420p
            //                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
            //                break;

            //            // -------------------------
            //            // MPEG-4
            //            // -------------------------
            //            case "MPEG-4":
            //                // Lossless can't be yuv444p
            //                // All Pixel Formats must be yuv420p
            //                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
            //                break;

            //            // -------------------------
            //            // JPEG
            //            // -------------------------
            //            case "JPEG":
            //                VM.VideoView.Video_PixelFormat_SelectedItem = "yuvj420p";
            //                break;

            //            // -------------------------
            //            // PNG
            //            // -------------------------
            //            case "PNG":
            //                // Lossless
            //                if (quality_SelectedItem == "Lossless")
            //                {
            //                    VM.VideoView.Video_PixelFormat_SelectedItem = "rgba";
            //                }
            //                // All Other Quality
            //                else
            //                {
            //                    VM.VideoView.Video_PixelFormat_SelectedItem = "yuva420p";
            //                }
            //                break;

            //            // -------------------------
            //            // WebP
            //            // -------------------------
            //            case "WebP":
            //                // Lossless
            //                if (quality_SelectedItem == "Lossless")
            //                {
            //                    VM.VideoView.Video_PixelFormat_SelectedItem = "rgba";
            //                }
            //                // All Other Quality
            //                else
            //                {
            //                    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
            //                }
            //                break;

            //            // -------------------------
            //            // Copy
            //            // -------------------------
            //            case "Copy":
            //                // Excluded
            //                break;
            //        }
            //    }

            //    // -------------------------
            //    // Excluded Media Types
            //    // -------------------------
            //    // Audio
            //}



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
                VM.VideoView.Video_Video_Optimize_Tune_SelectedItem = VM.VideoView.Video_Optimize_Items
                                                                      .FirstOrDefault(item => item.Name == VM.VideoView.Video_Optimize_SelectedItem)?.Tune;
                // Profile
                VM.VideoView.Video_Video_Optimize_Profile_SelectedItem = VM.VideoView.Video_Optimize_Items
                                                                         .FirstOrDefault(item => item.Name == VM.VideoView.Video_Optimize_SelectedItem)?.Profile;
                // Level
                VM.VideoView.Video_Optimize_Level_SelectedItem = VM.VideoView.Video_Optimize_Items
                                                                 .FirstOrDefault(item => item.Name == VM.VideoView.Video_Optimize_SelectedItem)?.Level;

            }


            /// <summary>
            /// Encoding Pass Controls
            /// <summary>
            public static void EncodingPassControls()
            {
                switch (VM.VideoView.Video_Codec_SelectedItem)
                {
                    // --------------------------------------------------
                    // Video
                    // --------------------------------------------------
                    // -------------------------
                    // VP8
                    // -------------------------
                    case "VP8":
                        Codec.VP8.EncodingPass();
                        break;
                    // -------------------------
                    // VP9
                    // -------------------------
                    case "VP9":
                        Codec.VP9.EncodingPass();
                        break;
                    // -------------------------
                    // x264
                    // -------------------------
                    case "x264":
                        Codec.x264.EncodingPass();
                        break;
                    // -------------------------
                    // x265
                    // -------------------------
                    case "x265":
                        Codec.x265.EncodingPass();
                        break;
                    // -------------------------
                    // AV1
                    // -------------------------
                    case "AV1":
                        Codec.AV1.EncodingPass();
                        break;
                    // -------------------------
                    // FFV1
                    // -------------------------
                    case "FFV1":
                        Codec.FFV1.EncodingPass();
                        break;
                    // -------------------------
                    // MagicYUV
                    // -------------------------
                    case "MagicYUV":
                        Codec.MagicYUV.EncodingPass();
                        break;
                    // -------------------------
                    // HuffYUV
                    // -------------------------
                    case "HuffYUV":
                        Codec.HuffYUV.EncodingPass();
                        break;
                    // -------------------------
                    // Theora
                    // -------------------------
                    case "Theora":
                        Codec.Theora.EncodingPass();
                        break;
                    // -------------------------
                    // MPEG-2
                    // -------------------------
                    case "MPEG-2":
                        Codec.MPEG_2.EncodingPass();
                        break;
                    // -------------------------
                    // MPEG-4
                    // -------------------------
                    case "MPEG-4":
                        Codec.MPEG_4.EncodingPass();
                        break;

                    // --------------------------------------------------
                    // Image
                    // --------------------------------------------------
                    // -------------------------
                    // JPEG
                    // -------------------------
                    case "JPEG":
                        Image.Codec.JPEG.EncodingPass();
                        break;
                    // -------------------------
                    // PNG
                    // -------------------------
                    case "PNG":
                        Image.Codec.PNG.EncodingPass();
                        break;
                    // -------------------------
                    // WebP
                    // -------------------------
                    case "WebP":
                        Image.Codec.WebP.EncodingPass();
                        break;

                    // --------------------------------------------------
                    // Other
                    // --------------------------------------------------
                    // -------------------------
                    // Copy
                    // -------------------------
                    case "Copy":
                        Codec.Copy.EncodingPass();
                        break;
                    // -------------------------
                    // None
                    // -------------------------
                    case "None":
                        Codec.None.EncodingPass();
                        break;
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
                             VM.VideoView.Video_Pass_SelectedItem == "none" ||
                             VM.VideoView.Video_Pass_SelectedItem == "auto")
                    {
                        VM.VideoView.Video_CRF_IsEnabled = false;
                    }
                }

            }


            /// <summary>
            /// Auto Copy Conditions Check
            /// <summary>
            public static bool AutoCopyConditionsCheck()
            {
                // Failed
                if (VM.VideoView.Video_Quality_SelectedItem != "Auto" ||
                    VM.VideoView.Video_PixelFormat_SelectedItem != "auto" ||
                    !string.IsNullOrWhiteSpace(CropWindow.crop) ||
                    VM.VideoView.Video_Scale_SelectedItem != "Source" ||
                    VM.VideoView.Video_ScalingAlgorithm_SelectedItem != "auto" ||
                    // Do not add Aspect Ratio -aspect, it can be used with Copy
                    VM.VideoView.Video_FPS_SelectedItem != "auto" ||
                    VM.VideoView.Video_Optimize_SelectedItem != "None" ||

                    // Color
                    VM.VideoView.Video_Color_Range_SelectedItem != "auto" ||
                    VM.VideoView.Video_Color_Space_SelectedItem != "auto" ||
                    VM.VideoView.Video_Color_Primaries_SelectedItem != "auto" ||
                    VM.VideoView.Video_Color_TransferCharacteristics_SelectedItem != "auto" ||
                    VM.VideoView.Video_Color_Matrix_SelectedItem != "auto" ||

                    // Filters
                    // Fix
                    VM.FilterVideoView.FilterVideo_Deband_SelectedItem != "disabled" ||
                    VM.FilterVideoView.FilterVideo_Deshake_SelectedItem != "disabled" ||
                    VM.FilterVideoView.FilterVideo_Deflicker_SelectedItem != "disabled" ||
                    VM.FilterVideoView.FilterVideo_Dejudder_SelectedItem != "disabled" ||
                    VM.FilterVideoView.FilterVideo_Denoise_SelectedItem != "disabled" ||
                    VM.FilterVideoView.FilterVideo_Deinterlace_SelectedItem != "disabled" ||
                    // Selective Color
                    // Reds
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Cyan_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Magenta_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Yellow_Value != 0 ||
                    // Yellows
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Cyan_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Magenta_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Yellow_Value != 0 ||
                    // Greens
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Cyan_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Magenta_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Yellow_Value != 0 ||
                    // Cyans
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Cyan_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Magenta_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Yellow_Value != 0 ||
                    // Blues
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Cyan_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Magenta_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Yellow_Value != 0 ||
                    // Magentas
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Cyan_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Magenta_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Yellow_Value != 0 ||
                    // Whites
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Cyan_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Magenta_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Yellow_Value != 0 ||
                    // Neutrals
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Cyan_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Magenta_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Yellow_Value != 0 ||
                    // Blacks
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Cyan_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Magenta_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Yellow_Value != 0 ||

                    // EQ
                    VM.FilterVideoView.FilterVideo_EQ_Brightness_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_EQ_Contrast_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_EQ_Saturation_Value != 0 ||
                    VM.FilterVideoView.FilterVideo_EQ_Gamma_Value != 0
                )
                {
                    //System.Windows.MessageBox.Show("true"); //debug
                    return false;
                }

                // Passed
                else
                {
                    //System.Windows.MessageBox.Show("false"); //debug
                    return true;
                }
            }


            /// <summary>
            /// Auto Copy Video Codec
            /// <summary>
            /// <remarks>
            /// Input Extension is same as Output Extension and Video Quality is Auto
            /// </remarks>
            public static void AutoCopyVideoCodec(string trigger)
            {
                //MessageBox.Show(VM.VideoView.Video_Quality_SelectedItem); //debug

                // -------------------------
                // Halt if Selected Codec is Null
                // -------------------------
                if (string.IsNullOrWhiteSpace(VM.VideoView.Video_Codec_SelectedItem))
                {
                    return;
                }

                // -------------------------
                // Halt if trigger is control
                // Pass if trigger is input
                // -------------------------
                if (trigger == "control" &&
                    VM.AudioView.Audio_Codec_SelectedItem != "Copy" &&
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
                    // Set Video Codec Combobox Selected Item to Copy
                    // -------------------------
                    if (VM.VideoView.Video_Codec_Items.Count > 0)
                    {
                        if (VM.VideoView.Video_Codec_Items?.Contains("Copy") == true)
                        {
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
                    // Copy Selected
                    // -------------------------
                    if (VM.VideoView.Video_Codec_SelectedItem == "Copy")
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
}
