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
using System.Collections;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Controls.Video
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

        public static Dictionary<string, dynamic> codecClasses = new Dictionary<string, dynamic>
        {
            { "VP8",        new Codec.VP8() },
            { "VP9",        new Codec.VP9() },
            { "x264",       new Codec.x264() },
            { "H264 AMF",   new Codec.H264_AMF() },
            { "H264 NVENC", new Codec.H264_NVENC() },
            { "H264 QSV",   new Codec.H264_QSV() },
            { "x265",       new Codec.x265() },
            { "HEVC AMF",   new Codec.HEVC_AMF() },
            { "HEVC NVENC", new Codec.HEVC_NVENC() },
            { "HEVC QSV",   new Codec.HEVC_QSV() },
            { "AV1",        new Codec.AV1() },
            { "FFV1",       new Codec.FFV1() },
            { "MagicYUV",   new Codec.MagicYUV() },
            { "HuffYUV",    new Codec.HuffYUV() },
            { "Theora",     new Codec.Theora() },
            { "MPEG-2",     new Codec.MPEG_2() },
            { "MPEG-4",     new Codec.MPEG_4() },
            { "JPEG",       new Image.Codec.JPEG() },
            { "PNG",        new Image.Codec.PNG() },
            { "WebP",       new Image.Codec.WebP() },
            { "Copy",       new Codec.Copy() },
            { "None",       new Codec.None() }
        };

        private static Dictionary<string, IVideoCodec> _codec_class;

        private static void InitializeCodecs()
        {
            _codec_class = codecClasses.ToDictionary(k => k.Key, k => (IVideoCodec)k.Value);

            //_codec_class = new Dictionary<string, IVideoCodec>
            //{
            //    { "VP8",        new Codec.VP8() },
            //    { "VP9",        new Codec.VP9() },
            //    { "x264",       new Codec.x264() },
            //    { "H264 AMF",   new Codec.H264_AMF() },
            //    { "H264 NVENC", new Codec.H264_NVENC() },
            //    { "H264 QSV",   new Codec.H264_QSV() },
            //    { "x265",       new Codec.x265() },
            //    { "HEVC AMF",   new Codec.HEVC_AMF() },
            //    { "HEVC NVENC", new Codec.HEVC_NVENC() },
            //    { "HEVC QSV",   new Codec.HEVC_QSV() },
            //    { "AV1",        new Codec.AV1() },
            //    { "FFV1",       new Codec.FFV1() },
            //    { "MagicYUV",   new Codec.MagicYUV() },
            //    { "HuffYUV",    new Codec.HuffYUV() },
            //    { "Theora",     new Codec.Theora() },
            //    { "MPEG-2",     new Codec.MPEG_2() },
            //    { "MPEG-4",     new Codec.MPEG_4() },
            //    { "JPEG",       new Image.Codec.JPEG() },
            //    { "PNG",        new Image.Codec.PNG() },
            //    { "WebP",       new Image.Codec.WebP() },
            //    { "Copy",       new Codec.Copy() },
            //    { "None",       new Codec.None() }
            //};
        }

        public interface IVideoCodec
        {
            // Codec
            ObservableCollection<ViewModel.Video.VideoCodec> codec { get; set; }

            // Items Source
            ObservableCollection<ViewModel.Video.VideoEncodeSpeed> encodeSpeed { get; set; }
            ObservableCollection<string> pixelFormat { get; set; }
            ObservableCollection<ViewModel.Video.VideoQuality> quality { get; set; }
            ObservableCollection<ViewModel.Video.VideoOptimize> optimize { get; set; }
            ObservableCollection<string> tune { get; set; }
            ObservableCollection<string> profile { get; set; }
            ObservableCollection<string> level { get; set; }
            void EncodingPass();

            // Selected Items
            List<ViewModel.Video.Selected> controls_Selected { get; set; }

            // Expanded
            List<ViewModel.Video.Expanded> controls_Expanded { get; set; }

            // Checked
            List<ViewModel.Video.Checked> controls_Checked { get; set; }

            // Enabled
            List<ViewModel.Video.Enabled> controls_Enabled { get; set; }
        }

        /// <summary>
        /// Controls
        /// </summary>
        public static bool passUserSelected = false; // Used to determine if User manually selected CRF, 1 Pass or 2 Pass

        /// <summary>
        /// Set Controls
        /// </summary>
        public static void CodecControls(string codec_SelectedItem)
        {
            // --------------------------------------------------
            // Codec
            // --------------------------------------------------

            if (!string.IsNullOrWhiteSpace(codec_SelectedItem))
            {
                InitializeCodecs();

                // -------------------------
                // Codec
                // -------------------------
                List<string> codec = new List<string>()
                {
                    // Combine Codec + Parameters
                    "-c:v",
                     _codec_class[codec_SelectedItem].codec.FirstOrDefault()?.Codec,
                     _codec_class[codec_SelectedItem].codec.FirstOrDefault()?.Parameters,
                };

                VM.VideoView.Video_Codec = string.Join(" ", codec.Where(s => !string.IsNullOrEmpty(s)));

                // -------------------------
                // Items Source
                // -------------------------
                // Encode Speed
                VM.VideoView.Video_EncodeSpeed_Items = _codec_class[codec_SelectedItem].encodeSpeed;

                // Pixel Format
                VM.VideoView.Video_PixelFormat_Items = _codec_class[codec_SelectedItem].pixelFormat;

                // Pass
                _codec_class[codec_SelectedItem].EncodingPass();

                // Video Quality
                VM.VideoView.Video_Quality_Items = _codec_class[codec_SelectedItem].quality;

                // Optimize
                VM.VideoView.Video_Optimize_Items = _codec_class[codec_SelectedItem].optimize;
                // Tune
                VM.VideoView.Video_Optimize_Tune_Items = _codec_class[codec_SelectedItem].tune;
                // Profile
                VM.VideoView.Video_Optimize_Profile_Items = _codec_class[codec_SelectedItem].profile;
                // Level
                VM.VideoView.Video_Optimize_Level_Items = _codec_class[codec_SelectedItem].level;

                // -------------------------
                // Selected Items
                // -------------------------
                // Encode Speed
                string encodeSpeed = _codec_class[codec_SelectedItem].controls_Selected
                                                                     .Find(item => item.EncodeSpeed == item.EncodeSpeed)
                                                                     .EncodeSpeed;
                                                                     //.Find(item => item.EncodeSpeed == item.EncodeSpeed)
                                                                     //.EncodeSpeed;
                if (!string.IsNullOrEmpty(encodeSpeed))
                {
                    VM.VideoView.Video_EncodeSpeed_SelectedItem = encodeSpeed;
                }

                // Pixel Format
                string pixelFormat = _codec_class[codec_SelectedItem].controls_Selected
                                                                     .Find(item => item.PixelFormat == item.PixelFormat)
                                                                     .PixelFormat;
                                                                     //.Select(item => item.PixelFormat)
                                                                     //.First();
                if (!string.IsNullOrEmpty(pixelFormat))
                {
                    VM.VideoView.Video_PixelFormat_SelectedItem = pixelFormat;
                }

                // Filters
                // Select Defaults
                if (codec_SelectedItem == "Copy" ||
                    codec_SelectedItem == "None")
                {
                    Filters.Video.VideoFilters_ControlsSelectDefaults();
                }

                //ViewModel.Video.Selected selected = new ViewModel.Video.Selected();
                //VM.VideoView.Video_EncodeSpeed_SelectedItem = selected.EncodeSpeed;
                //VM.VideoView.Video_EncodeSpeed_SelectedItem = _codec_class[codec_SelectedItem].controls_Selected.Find(item => item.EncodeSpeed == item.EncodeSpeed).EncodeSpeed;
                //VM.VideoView.Video_PixelFormat_SelectedItem = _codec_class[codec_SelectedItem].controls_Selected.Find(item => item.PixelFormat == item.PixelFormat).PixelFormat;
                //VM.VideoView.Video_FPS_SelectedItem = _codec_class[codec_SelectedItem].controls_Selected.Find(item => item.FPS == item.FPS).FPS;
                //_codec_class[codec_SelectedItem].Controls_Selected();

                // -------------------------
                // Expanded
                // -------------------------
                // Optimize
                VM.VideoView.Video_Optimize_IsExpanded = _codec_class[codec_SelectedItem].controls_Expanded.Any(item => item.Optimize);

                // -------------------------
                // Checked
                // -------------------------
                // Video VBR
                VM.VideoView.Video_VBR_IsChecked = _codec_class[codec_SelectedItem].controls_Checked.Any(item => item.VBR == item.VBR);

                // -------------------------
                // Enabled
                // -------------------------
                // Video Encode Speed
                VM.VideoView.Video_EncodeSpeed_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.EncodeSpeed);

                // Video Codec
                VM.VideoView.Video_Codec_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.Codec);

                // HW Accel
                VM.VideoView.Video_HWAccel_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.HWAccel);

                // Video Quality
                VM.VideoView.Video_Quality_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.Quality);

                // Video VBR
                VM.VideoView.Video_VBR_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.VBR);

                // Pixel Format
                VM.VideoView.Video_PixelFormat_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.PixelFormat);

                // FPS
                VM.VideoView.Video_FPS_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.FPS);

                // Speed
                VM.VideoView.Video_Speed_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.Speed);

                // Vsync
                VM.VideoView.Video_Vsync_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.Vsync);

                // Optimize ComboBox
                VM.VideoView.Video_Optimize_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.Optimize);

                // Size
                VM.VideoView.Video_Scale_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.Scale);

                // Scaling ComboBox
                VM.VideoView.Video_ScalingAlgorithm_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.Scaling);

                // Crop
                VM.VideoView.Video_Crop_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.Crop);


                // Color Range
                VM.VideoView.Video_Color_Range_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.ColorRange);

                // Color Space
                VM.VideoView.Video_Color_Space_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.ColorSpace);

                // Color Primaries
                VM.VideoView.Video_Color_Primaries_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.ColorPrimaries);

                // Color Transfer Characteristics
                VM.VideoView.Video_Color_TransferCharacteristics_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.ColorTransferChar);

                // Color Matrix
                VM.VideoView.Video_Color_Matrix_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.ColorMatrix);

                // Subtitle Codec
                VM.SubtitleView.Subtitle_Codec_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.SubtitleCodec);

                // Subtitle Stream
                VM.SubtitleView.Subtitle_Stream_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.Any(item => item.SubtitleStream);

                // Filters
                // Disable All
                if (codec_SelectedItem == "Copy" ||
                    codec_SelectedItem == "None")
                {
                    Filters.Video.VideoFilters_DisableAll();
                }
                // Enable All
                else
                {
                    Filters.Video.VideoFilters_EnableAll();
                }

                //Codec.VP8 vp8 = new Codec.VP8();
                //vp8.EncodingPass();
                //MessageBox.Show(vp8.controls_Enabled.Find(item => item.Quality).Quality.ToString()/*.SingleOrDefault().Quality.ToString()*/); // debug
                //MessageBox.Show(_codec_class[codec_SelectedItem].controls_Selected.Find(item => item.PixelFormat == item.PixelFormat).PixelFormat.ToString());
                //MessageBox.Show(selected.EncodeSpeed);
                //MessageBox.Show(_codec_class[codec_SelectedItem].controls_Enabled.Find(item => item.EncodeSpeed == item.EncodeSpeed).EncodeSpeed.ToString()); //deubg
                //Codec.VP8 vp8 = new Codec.VP8();
                //VM.VideoView.Video_Quality_IsEnabled = vp8.controls_Enabled.Any(item => item.Quality);//_codec_class[codec_SelectedItem].controls_Enabled.Quality;
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
                        //VM.VideoView.Video_Codec_SelectedItem == "x265" || // n/a
                        VM.VideoView.Video_Codec_SelectedItem == "JPEG" ||
                        //VM.VideoView.Video_Codec_SelectedItem != "PNG" || // n/a
                        //VM.VideoView.Video_Codec_SelectedItem != "WebP" || // n/a
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
        //private static Dictionary<string, IVideoPixelFormat> _codec_class_PixelFormat;

        //private static void InitializeCodecs_PixelFormat()
        //{
        //    _codec_class = codecClasses.ToDictionary(k => k.Key, k => (IVideoCodec)k.Value);
        //}

        //public interface IVideoPixelFormat
        //{
        //    // Selected Items
        //    List<ViewModel.Video.Selected> controls_Selected { get; set; }
        //}

        /// <summary>
        /// Pixel Format Selected Values
        /// </summary>
        //public static void PixelFormatQuality(string quality_SelectedItem,
        //                                      string lossless,
        //                                      string other
        //    )
        //{
        //    // Lossless
        //    if (quality_SelectedItem == "Lossless")
        //    {
        //        VM.VideoView.Video_PixelFormat_SelectedItem = lossless;
        //    }
        //    // All Other Quality
        //    else
        //    {
        //        VM.VideoView.Video_PixelFormat_SelectedItem = other;
        //    }
        //}
 
        public static void PixelFormatControls(string mediaType_SelectedItem,
                                               string codec_SelectedItem,
                                               string quality_SelectedItem
                                              )
        {
            // -------------------------
            // MediaTypeControls
            // ------------------------- 
            if (mediaType_SelectedItem == "Video" ||
                mediaType_SelectedItem == "Image" ||
                mediaType_SelectedItem == "Sequence")
            {
                if (!string.IsNullOrWhiteSpace(codec_SelectedItem))
                {
                    //InitializeCodecs_PixelFormat();
                    InitializeCodecs();

                    // -------------------------
                    // Lossless
                    // -------------------------
                    if (quality_SelectedItem == "Lossless")
                    {
                        string lossless = _codec_class[codec_SelectedItem].controls_Selected
                                                                          .Find(item => item.PixelFormat_Lossless == item.PixelFormat_Lossless)
                                                                          .PixelFormat_Lossless;
                                                                          //.controls_Selected
                                                                          //.Select(item => item.PixelFormat_Lossless)
                                                                          //.First();
                        if (!string.IsNullOrEmpty(lossless))
                        {
                            VM.VideoView.Video_PixelFormat_SelectedItem = lossless;
                        }
                    }
                    // -------------------------
                    // All Other Quality
                    // -------------------------
                    else
                    {
                        string other = _codec_class[codec_SelectedItem].controls_Selected
                                                                       .Find(item => item.PixelFormat == item.PixelFormat)
                                                                       .PixelFormat;
                                                                       //.First();
                        if (!string.IsNullOrEmpty(other))
                        {
                            VM.VideoView.Video_PixelFormat_SelectedItem = other;
                        }

                        //MessageBox.Show(_codec_class[codec_SelectedItem].ToString()); //debug
                    }
                }

                //switch (codec_SelectedItem)
                //{
                //    // -------------------------
                //    // VP8
                //    // -------------------------
                //    //case "VP8":
                //    //    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                //    //    break;

                //    // -------------------------
                //    // VP9
                //    // -------------------------
                //    case "VP9":
                //        PixelFormatQuality(quality_SelectedItem,
                //                           "yuv444p", // Lossless
                //                           "yuv420p"  // Other
                //                          );
                //        break;

                //    // -------------------------
                //    // x264
                //    // -------------------------
                //    case "x264":
                //        PixelFormatQuality(quality_SelectedItem,
                //                           "yuv444p", // Lossless
                //                           "yuv420p"  // Other
                //                          );
                //        break;

                //    // -------------------------
                //    // x265
                //    // -------------------------
                //    case "x265":
                //        PixelFormatQuality(quality_SelectedItem,
                //                           "yuv444p", // Lossless
                //                           "yuv420p"  // Other
                //                          );
                //        break;

                //    // -------------------------
                //    // H264 AMF
                //    // -------------------------
                //    case "H264 AMF":
                //        PixelFormatQuality(quality_SelectedItem,
                //                           "nv12",   // Lossless
                //                           "yuv420p" // Other
                //                          );
                //        break;

                //    // -------------------------
                //    // HEVC AMF
                //    // -------------------------
                //    case "HEVC AMF":
                //        PixelFormatQuality(quality_SelectedItem,
                //                           "nv12",   // Lossless
                //                           "yuv420p" // Other
                //                          );
                //        break;

                //    // -------------------------
                //    // H264_NVENC
                //    // -------------------------
                //    case "H264 NVENC":
                //        PixelFormatQuality(quality_SelectedItem,
                //                           "yuv444p16le", // Lossless
                //                           "yuv420p"      // Other
                //                          );
                //        break;

                //    // -------------------------
                //    // HEVC NVENC
                //    // -------------------------
                //    case "HEVC NVENC":
                //        PixelFormatQuality(quality_SelectedItem,
                //                           "yuv444p16le", // Lossless
                //                           "p010le"       // Other
                //                          );
                //        break;

                //    // -------------------------
                //    // H264 QSV
                //    // -------------------------
                //    case "H264 QSV":
                //        PixelFormatQuality(quality_SelectedItem,
                //                           "yuv444p16le", // Lossless
                //                           "p010le"       // Other
                //                          );
                //        break;

                //    // -------------------------
                //    // HEVC QSV
                //    // -------------------------
                //    case "HEVC QSV":
                //        PixelFormatQuality(quality_SelectedItem,
                //                           "nv12",   // Lossless
                //                           "yuv420p" // Other
                //                          );
                //        break;

                //    // -------------------------
                //    // AV1
                //    // -------------------------
                //    //case "AV1":
                //    //    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                //    //    break;

                //    // -------------------------
                //    // FFV1
                //    // -------------------------
                //    //case "FFV1":
                //    //    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv444p10le";
                //    //    break;

                //    // -------------------------
                //    // MagicYUV
                //    // -------------------------
                //    //case "MagicYUV":
                //    //    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv444p";
                //    //    break;

                //    // -------------------------
                //    // HuffYUV
                //    // -------------------------
                //    //case "HuffYUV":
                //    //    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv444p";
                //    //    break;

                //    // -------------------------
                //    // MPEG-2
                //    // -------------------------
                //    //case "MPEG-2":
                //    //    // Lossless can't be yuv444p
                //    //    // All Pixel Formats must be yuv420p
                //    //    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                //    //    break;

                //    // -------------------------
                //    // MPEG-4
                //    // -------------------------
                //    //case "MPEG-4":
                //    //    // Lossless can't be yuv444p
                //    //    // All Pixel Formats must be yuv420p
                //    //    VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                //    //    break;

                //    // -------------------------
                //    // JPEG
                //    // -------------------------
                //    //case "JPEG":
                //    //    VM.VideoView.Video_PixelFormat_SelectedItem = "yuvj420p";
                //    //    break;

                //    // -------------------------
                //    // PNG
                //    // -------------------------
                //    //case "PNG":
                //    //    // Lossless
                //    //    if (quality_SelectedItem == "Lossless")
                //    //    {
                //    //        VM.VideoView.Video_PixelFormat_SelectedItem = "rgba";
                //    //    }
                //    //    // All Other Quality
                //    //    else
                //    //    {
                //    //        VM.VideoView.Video_PixelFormat_SelectedItem = "yuva420p";
                //    //    }
                //    //    break;

                //    // -------------------------
                //    // WebP
                //    // -------------------------
                //    case "WebP":
                //        PixelFormatQuality(quality_SelectedItem,
                //                           "rgba",   // Lossless
                //                           "yuv420p" // Other
                //                          );
                //        break;

                //    // -------------------------
                //    // Copy
                //    // -------------------------
                //    //case "Copy":
                //    //    // Excluded
                //    //    break;
                //}
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
                VM.VideoView.Video_Codec_SelectedItem == "x265" ||
                VM.VideoView.Video_Codec_SelectedItem == "H264 AMF" ||
                VM.VideoView.Video_Codec_SelectedItem == "HEVC AMF" ||
                VM.VideoView.Video_Codec_SelectedItem == "H264 NVENC" ||
                VM.VideoView.Video_Codec_SelectedItem == "HEVC NVENC" ||
                VM.VideoView.Video_Codec_SelectedItem == "H264 QSV" ||
                VM.VideoView.Video_Codec_SelectedItem == "HEVC QSV")
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
            // Disable Tune, Profile, Level if Codec not x264/x265/hevc_nvenc
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
            InitializeCodecs();

            _codec_class[VM.VideoView.Video_Codec_SelectedItem].EncodingPass();

            //switch (VM.VideoView.Video_Codec_SelectedItem)
            //{
            //    // --------------------------------------------------
            //    // Video
            //    // --------------------------------------------------

            //    // -------------------------
            //    // VP8
            //    // -------------------------
            //    case "VP8":
            //        Codec.VP8 vp8 = new Codec.VP8();
            //        vp8.EncodingPass();
            //        //Codec.VP8.EncodingPass();
            //        break;
            //    // -------------------------
            //    // VP9
            //    // -------------------------
            //    case "VP9":
            //        Codec.VP9 vp9 = new Codec.VP9();
            //        vp9.EncodingPass();
            //        //Codec.VP9.EncodingPass();
            //        break;
            //    // -------------------------
            //    // x264
            //    // -------------------------
            //    case "x264":
            //        Codec.x264 x264 = new Codec.x264();
            //        x264.EncodingPass();
            //        //Codec.x264.EncodingPass();
            //        break;
            //    // -------------------------
            //    // x265
            //    // -------------------------
            //    case "x265":
            //        Codec.x265 x265 = new Codec.x265();
            //        x265.EncodingPass();
            //        //Codec.x265.EncodingPass();
            //        break;
            //    // -------------------------
            //    // H264 AMF
            //    // -------------------------
            //    case "H264 AMF":
            //        Codec.H264_AMF h264_amf = new Codec.H264_AMF();
            //        h264_amf.EncodingPass();
            //        //Codec.H264_AMF.EncodingPass();
            //        break;
            //    // -------------------------
            //    // HEVC AMF
            //    // -------------------------
            //    case "HEVC AMF":
            //        Codec.HEVC_AMF hevc_amf = new Codec.HEVC_AMF();
            //        hevc_amf.EncodingPass();
            //        //Codec.HEVC_AMF.EncodingPass();
            //        break;
            //    // -------------------------
            //    // H264 NVENC
            //    // -------------------------
            //    case "H264 NVENC":
            //        Codec.H264_NVENC h264_nvenc = new Codec.H264_NVENC();
            //        h264_nvenc.EncodingPass();
            //        //Codec.H264_NVENC.EncodingPass();
            //        break;
            //    // -------------------------
            //    // HEVC NVENC
            //    // -------------------------
            //    case "HEVC NVENC":
            //        Codec.HEVC_NVENC hevc_nvenc = new Codec.HEVC_NVENC();
            //        hevc_nvenc.EncodingPass();
            //        //Codec.HEVC_NVENC.EncodingPass();
            //        break;
            //    // -------------------------
            //    // H264 QSV
            //    // -------------------------
            //    case "H264 QSV":
            //        Codec.H264_QSV h264_qsv = new Codec.H264_QSV();
            //        h264_qsv.EncodingPass();
            //        //Codec.H264_QSV.EncodingPass();
            //        break;
            //    // -------------------------
            //    // HEVC QSV
            //    // -------------------------
            //    case "HEVC QSV":
            //        Codec.HEVC_QSV hevc_qsv = new Codec.HEVC_QSV();
            //        hevc_qsv.EncodingPass();
            //        //Codec.HEVC_QSV.EncodingPass();
            //        break;
            //    // -------------------------
            //    // AV1
            //    // -------------------------
            //    case "AV1":
            //        Codec.AV1 av1 = new Codec.AV1();
            //        av1.EncodingPass();
            //        //Codec.AV1.EncodingPass();
            //        break;
            //    // -------------------------
            //    // FFV1
            //    // -------------------------
            //    case "FFV1":
            //        Codec.FFV1 ffv1 = new Codec.FFV1();
            //        ffv1.EncodingPass();
            //        //Codec.FFV1.EncodingPass();
            //        break;
            //    // -------------------------
            //    // MagicYUV
            //    // -------------------------
            //    case "MagicYUV":
            //        Codec.MagicYUV magicYUV = new Codec.MagicYUV();
            //        magicYUV.EncodingPass();
            //        //Codec.MagicYUV.EncodingPass();
            //        break;
            //    // -------------------------
            //    // HuffYUV
            //    // -------------------------
            //    case "HuffYUV":
            //        Codec.HuffYUV huffYUV = new Codec.HuffYUV();
            //        huffYUV.EncodingPass();
            //        //Codec.HuffYUV.EncodingPass();
            //        break;
            //    // -------------------------
            //    // Theora
            //    // -------------------------
            //    case "Theora":
            //        Codec.Theora theora = new Codec.Theora();
            //        theora.EncodingPass();
            //        //Codec.Theora.EncodingPass();
            //        break;
            //    // -------------------------
            //    // MPEG-2
            //    // -------------------------
            //    case "MPEG-2":
            //        Codec.MPEG_2 mpeg2 = new Codec.MPEG_2();
            //        mpeg2.EncodingPass();
            //        //Codec.MPEG_2.EncodingPass();
            //        break;
            //    // -------------------------
            //    // MPEG-4
            //    // -------------------------
            //    case "MPEG-4":
            //        Codec.MPEG_4 mpeg4 = new Codec.MPEG_4();
            //        mpeg4.EncodingPass();
            //        //Codec.MPEG_4.EncodingPass();
            //        break;

            //    // --------------------------------------------------
            //    // Image
            //    // --------------------------------------------------
            //    // -------------------------
            //    // JPEG
            //    // -------------------------
            //    case "JPEG":
            //        Image.Codec.JPEG jpeg = new Image.Codec.JPEG();
            //        jpeg.EncodingPass();
            //        //Image.Codec.JPEG.EncodingPass();
            //        break;
            //    // -------------------------
            //    // PNG
            //    // -------------------------
            //    case "PNG":
            //        Image.Codec.PNG png = new Image.Codec.PNG();
            //        png.EncodingPass();
            //        //Image.Codec.PNG.EncodingPass();
            //        break;
            //    // -------------------------
            //    // WebP
            //    // -------------------------
            //    case "WebP":
            //        Image.Codec.WebP webp = new Image.Codec.WebP();
            //        webp.EncodingPass();
            //        //Image.Codec.WebP.EncodingPass();
            //        break;

            //    // --------------------------------------------------
            //    // Other
            //    // --------------------------------------------------
            //    // -------------------------
            //    // Copy
            //    // -------------------------
            //    case "Copy":
            //        Codec.Copy copy = new Codec.Copy();
            //        copy.EncodingPass();
            //        //Codec.Copy.EncodingPass();
            //        break;
            //    // -------------------------
            //    // None
            //    // -------------------------
            //    case "None":
            //        Codec.None none = new Codec.None();
            //        none.EncodingPass();
            //        //Codec.None.EncodingPass();
            //        break;
            //}


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
        //public static bool AutoCopyConditionsCheck()
        //{
        //    // Failed
        //    if (VM.VideoView.Video_Quality_SelectedItem != "Auto" ||
        //        VM.VideoView.Video_PixelFormat_SelectedItem != "auto" ||
        //        !string.IsNullOrWhiteSpace(CropWindow.crop) ||
        //        VM.VideoView.Video_Scale_SelectedItem != "Source" ||
        //        VM.VideoView.Video_ScalingAlgorithm_SelectedItem != "auto" ||
        //        // Do not add Aspect Ratio -aspect, it can be used with Copy
        //        VM.VideoView.Video_FPS_SelectedItem != "auto" ||
        //        VM.VideoView.Video_Optimize_SelectedItem != "None" ||

        //        // Color
        //        VM.VideoView.Video_Color_Range_SelectedItem != "auto" ||
        //        VM.VideoView.Video_Color_Space_SelectedItem != "auto" ||
        //        VM.VideoView.Video_Color_Primaries_SelectedItem != "auto" ||
        //        VM.VideoView.Video_Color_TransferCharacteristics_SelectedItem != "auto" ||
        //        VM.VideoView.Video_Color_Matrix_SelectedItem != "auto" ||

        //        // Filters
        //        // Fix
        //        VM.FilterVideoView.FilterVideo_Deband_SelectedItem != "disabled" ||
        //        VM.FilterVideoView.FilterVideo_Deshake_SelectedItem != "disabled" ||
        //        VM.FilterVideoView.FilterVideo_Deflicker_SelectedItem != "disabled" ||
        //        VM.FilterVideoView.FilterVideo_Dejudder_SelectedItem != "disabled" ||
        //        VM.FilterVideoView.FilterVideo_Denoise_SelectedItem != "disabled" ||
        //        VM.FilterVideoView.FilterVideo_Deinterlace_SelectedItem != "disabled" ||
        //        // Selective Color
        //        // Reds
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Cyan_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Magenta_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Yellow_Value != 0 ||
        //        // Yellows
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Cyan_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Magenta_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Yellow_Value != 0 ||
        //        // Greens
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Cyan_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Magenta_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Yellow_Value != 0 ||
        //        // Cyans
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Cyan_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Magenta_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Yellow_Value != 0 ||
        //        // Blues
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Cyan_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Magenta_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Yellow_Value != 0 ||
        //        // Magentas
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Cyan_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Magenta_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Yellow_Value != 0 ||
        //        // Whites
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Cyan_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Magenta_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Yellow_Value != 0 ||
        //        // Neutrals
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Cyan_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Magenta_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Yellow_Value != 0 ||
        //        // Blacks
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Cyan_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Magenta_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Yellow_Value != 0 ||

        //        // EQ
        //        VM.FilterVideoView.FilterVideo_EQ_Brightness_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_EQ_Contrast_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_EQ_Saturation_Value != 0 ||
        //        VM.FilterVideoView.FilterVideo_EQ_Gamma_Value != 0
        //    )
        //    {
        //        //System.Windows.MessageBox.Show("true"); //debug
        //        return false;
        //    }

        //    // Passed
        //    else
        //    {
        //        //System.Windows.MessageBox.Show("false"); //debug
        //        return true;
        //    }
        //}


        /// <summary>
        /// Auto Copy Video Codec
        /// <summary>
        /// <remarks>
        /// Input Extension is same as Output Extension and Video Quality is Auto
        /// </remarks>
        //public static void AutoCopyVideoCodec(string trigger)
        //{
        //    //MessageBox.Show(VM.VideoView.Video_Quality_SelectedItem); //debug

        //    // -------------------------
        //    // Halt if Selected Codec is Null
        //    // -------------------------
        //    if (string.IsNullOrWhiteSpace(VM.VideoView.Video_Codec_SelectedItem))
        //    {
        //        return;
        //    }

        //    // -------------------------
        //    // Halt if trigger is control
        //    // Pass if trigger is input
        //    // -------------------------
        //    if (trigger == "control" &&
        //        VM.AudioView.Audio_Codec_SelectedItem != "Copy" &&
        //        AutoCopyConditionsCheck() == true)
        //    {
        //        return;
        //    }

        //    // -------------------------
        //    // Halt if Web URL
        //    // -------------------------
        //    if (MainWindow.IsWebURL(VM.MainView.Input_Text) == true)
        //    {
        //        return;
        //    }

        //    // -------------------------
        //    // Get Input Extensions
        //    // -------------------------
        //    string inputExt = Path.GetExtension(VM.MainView.Input_Text);

        //    // -------------------------
        //    // Halt if Input Extension is Empty
        //    // -------------------------
        //    if (string.IsNullOrWhiteSpace(inputExt))
        //    {
        //        return;
        //    }

        //    // -------------------------
        //    // Get Output Extensions
        //    // -------------------------
        //    string outputExt = "." + VM.FormatView.Format_Container_SelectedItem;

        //    // -------------------------
        //    // Conditions Check
        //    // Enable
        //    // -------------------------
        //    if (AutoCopyConditionsCheck() == true &&
        //        string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
        //    {
        //        // -------------------------
        //        // Set Video Codec Combobox Selected Item to Copy
        //        // -------------------------
        //        if (VM.VideoView.Video_Codec_Items.Count > 0)
        //        {
        //            if (VM.VideoView.Video_Codec_Items?.Contains("Copy") == true)
        //            {
        //                VM.VideoView.Video_Codec_SelectedItem = "Copy";
        //            }
        //        }
        //    }

        //    // -------------------------
        //    // Reset to Default Codec
        //    // -------------------------
        //    // Disable Copy if:
        //    // Input / Output Extensions don't match
        //    // Batch / Output Extensions don't match
        //    // Size is Not No
        //    // Crop is Not Empty
        //    // FPS is Not Auto
        //    // Optimize is Not None
        //    // -------------------------
        //    else
        //    {
        //        // -------------------------
        //        // Copy Selected
        //        // -------------------------
        //        if (VM.VideoView.Video_Codec_SelectedItem == "Copy")
        //        {
        //            switch (VM.FormatView.Format_Container_SelectedItem)
        //            {
        //                // WebM
        //                case "webm":
        //                    VM.VideoView.Video_Codec_SelectedItem = "VP8";
        //                    break;
        //                // MP4
        //                case "mp4":
        //                    VM.VideoView.Video_Codec_SelectedItem = "x264";
        //                    break;
        //                // MKV
        //                case "mkv":
        //                    VM.VideoView.Video_Codec_SelectedItem = "x264";
        //                    break;
        //                // MPG
        //                case "mpg":
        //                    VM.VideoView.Video_Codec_SelectedItem = "MPEG-2";
        //                    break;
        //                // AVI
        //                case "avi":
        //                    VM.VideoView.Video_Codec_SelectedItem = "MPEG-4";
        //                    break;
        //                // OGV
        //                case "ogv":
        //                    VM.VideoView.Video_Codec_SelectedItem = "Theora";
        //                    break;
        //                // JPG
        //                case "jpg":
        //                    VM.VideoView.Video_Codec_SelectedItem = "JPEG";
        //                    break;
        //                // PNG
        //                case "png":
        //                    VM.VideoView.Video_Codec_SelectedItem = "PNG";
        //                    break;
        //                // WebP
        //                case "webp":
        //                    VM.VideoView.Video_Codec_SelectedItem = "WebP";
        //                    break;
        //            }
        //        }
        //    }
        //}

    }
}
