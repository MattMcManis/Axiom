/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2021 Matt McManis
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
        // Used to determine if User manually selected CRF, 1 Pass or 2 Pass
        public static bool passUserSelected = false;

        /// <summary>
        /// Set Controls
        /// </summary>
        public static void CodecControls(string codec_SelectedItem)
        {
            if (!string.IsNullOrWhiteSpace(codec_SelectedItem))
            {
                InitializeCodecs();

                // --------------------------------------------------
                // Codec
                // --------------------------------------------------
                List<string> codec = new List<string>()
                {
                    // Combine Codec + Parameters
                    "-c:v",
                     _codec_class[codec_SelectedItem].codec.FirstOrDefault()?.Codec,
                     _codec_class[codec_SelectedItem].codec.FirstOrDefault()?.Parameters,
                };

                VM.VideoView.Video_Codec = string.Join(" ", codec.Where(s => !string.IsNullOrEmpty(s)));


                // --------------------------------------------------
                // Save Previous Item
                // --------------------------------------------------
                // Save before changing Items Source
                // -------------------------
                // Encode Speed
                // -------------------------
                if (MainWindow.SavePreviousItemCond(VM.VideoView.Video_EncodeSpeed_SelectedItem) == true)
                {
                    //MessageBox.Show("1 Save Previous Item " + VM.VideoView.Video_EncodeSpeed_SelectedItem); //debug
                    MainWindow.Video_EncodeSpeed_PreviousItem = VM.VideoView.Video_EncodeSpeed_SelectedItem;
                }

                // -------------------------
                // Quality
                // -------------------------
                if (MainWindow.SavePreviousItemCond(VM.VideoView.Video_Quality_SelectedItem) == true)
                {
                    MainWindow.Video_Quality_PreviousItem = VM.VideoView.Video_Quality_SelectedItem;
                }

                // -------------------------
                // Optimize
                // -------------------------
                if (MainWindow.SavePreviousItemCond(VM.VideoView.Video_Optimize_SelectedItem) == true)
                {
                    MainWindow.Video_Optimize_PreviousItem = VM.VideoView.Video_Optimize_SelectedItem;
                }

                // -------------------------
                // Optimize Tune
                // -------------------------
                if (MainWindow.SavePreviousItemCond(VM.VideoView.Video_Optimize_Tune_SelectedItem) == true)
                {
                    MainWindow.Video_Optimize_Tune_PreviousItem = VM.VideoView.Video_Optimize_Tune_SelectedItem;
                }

                // -------------------------
                // Optimize Profile
                // -------------------------
                if (MainWindow.SavePreviousItemCond(VM.VideoView.Video_Optimize_Profile_SelectedItem) == true)
                {
                    MainWindow.Video_Optimize_Profile_PreviousItem = VM.VideoView.Video_Optimize_Profile_SelectedItem;
                }

                // -------------------------
                // Optimize Level
                // -------------------------
                if (MainWindow.SavePreviousItemCond(VM.VideoView.Video_Optimize_Level_SelectedItem) == true)
                {
                    MainWindow.Video_Optimize_Level_PreviousItem = VM.VideoView.Video_Optimize_Level_SelectedItem;
                }


                // --------------------------------------------------
                // Items Source
                // --------------------------------------------------
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


                // --------------------------------------------------
                // Enabled
                // --------------------------------------------------
                // Video Encode Speed
                //VM.VideoView.Video_EncodeSpeed_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().EncodeSpeed;
                bool? encodeSpeed_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().EncodeSpeed;
                if (!encodeSpeed_IsEnabled.HasValue)
                    VM.VideoView.Video_EncodeSpeed_IsEnabled = true;
                else
                    VM.VideoView.Video_EncodeSpeed_IsEnabled = encodeSpeed_IsEnabled;

                // Video Codec
                //VM.VideoView.Video_Codec_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Codec;
                bool? codec_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Codec;
                if (!codec_IsEnabled.HasValue)
                    VM.VideoView.Video_Codec_IsEnabled = true;
                else
                    VM.VideoView.Video_Codec_IsEnabled = codec_IsEnabled;

                // HW Accel
                //VM.VideoView.Video_HWAccel_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().HWAccel;
                bool? hwAccel_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().HWAccel;
                if (!hwAccel_IsEnabled.HasValue)
                    VM.VideoView.Video_HWAccel_IsEnabled = true;
                else
                    VM.VideoView.Video_HWAccel_IsEnabled = hwAccel_IsEnabled;

                // Video Quality
                //VM.VideoView.Video_Quality_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Quality;
                bool? quality_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Quality;
                if (!codec_IsEnabled.HasValue)
                    VM.VideoView.Video_Quality_IsEnabled = true;
                else
                    VM.VideoView.Video_Quality_IsEnabled = codec_IsEnabled;

                // Video VBR
                //VM.VideoView.Video_VBR_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().VBR;
                bool? vbr_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().VBR;
                if (!vbr_IsEnabled.HasValue)
                    VM.VideoView.Video_VBR_IsEnabled = true;
                else
                    VM.VideoView.Video_VBR_IsEnabled = vbr_IsEnabled;

                // Pixel Format
                //VM.VideoView.Video_PixelFormat_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().PixelFormat;
                bool? pixelFormat_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().PixelFormat;
                if (!pixelFormat_IsEnabled.HasValue)
                    VM.VideoView.Video_PixelFormat_IsEnabled = true;
                else
                    VM.VideoView.Video_PixelFormat_IsEnabled = pixelFormat_IsEnabled;

                // FPS
                //VM.VideoView.Video_FPS_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().FPS;
                bool? fps_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().FPS;
                if (!fps_IsEnabled.HasValue)
                    VM.VideoView.Video_FPS_IsEnabled = true;
                else
                    VM.VideoView.Video_FPS_IsEnabled = fps_IsEnabled;

                // Speed
                //VM.VideoView.Video_Speed_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Speed;
                bool? speed_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Speed;
                if (!speed_IsEnabled.HasValue)
                    VM.VideoView.Video_Speed_IsEnabled = true;
                else
                    VM.VideoView.Video_Speed_IsEnabled = speed_IsEnabled;

                // Vsync
                //VM.VideoView.Video_Vsync_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Vsync;
                bool? vsync_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Vsync;
                if (!vsync_IsEnabled.HasValue)
                    VM.VideoView.Video_Vsync_IsEnabled = true;
                else
                    VM.VideoView.Video_Vsync_IsEnabled = vsync_IsEnabled;

                // Optimize ComboBox
                //VM.VideoView.Video_Optimize_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Optimize;
                bool? optimize_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Optimize;
                if (!optimize_IsEnabled.HasValue)
                    VM.VideoView.Video_Optimize_IsEnabled = true;
                else
                    VM.VideoView.Video_Optimize_IsEnabled = optimize_IsEnabled;

                // Size
                //VM.VideoView.Video_Scale_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Scale;
                bool? size_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Scale;
                if (!size_IsEnabled.HasValue)
                    VM.VideoView.Video_Scale_IsEnabled = true;
                else
                    VM.VideoView.Video_Scale_IsEnabled = size_IsEnabled;

                // Scaling ComboBox
                //VM.VideoView.Video_ScalingAlgorithm_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Scaling;
                bool? scalingAlgorithm_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Scaling;
                if (!scalingAlgorithm_IsEnabled.HasValue)
                    VM.VideoView.Video_ScalingAlgorithm_IsEnabled = true;
                else
                    VM.VideoView.Video_ScalingAlgorithm_IsEnabled = scalingAlgorithm_IsEnabled;

                // Crop
                //VM.VideoView.Video_Crop_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Crop;
                bool? crop_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().Crop;
                if (!crop_IsEnabled.HasValue)
                    VM.VideoView.Video_Crop_IsEnabled = true;
                else
                    VM.VideoView.Video_Crop_IsEnabled = crop_IsEnabled;

                // Color Range
                //VM.VideoView.Video_Color_Range_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().ColorRange;
                bool? colorRange_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().ColorRange;
                if (!colorRange_IsEnabled.HasValue)
                    VM.VideoView.Video_Color_Range_IsEnabled = true;
                else
                    VM.VideoView.Video_Color_Range_IsEnabled = colorRange_IsEnabled;

                // Color Space
                //VM.VideoView.Video_Color_Space_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().ColorSpace;
                bool? colorSpace_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().ColorSpace;
                if (!colorSpace_IsEnabled.HasValue)
                    VM.VideoView.Video_Color_Space_IsEnabled = true;
                else
                    VM.VideoView.Video_Color_Space_IsEnabled = colorSpace_IsEnabled;

                // Color Primaries
                //VM.VideoView.Video_Color_Primaries_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().ColorPrimaries;
                bool? colorPrimaries_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().ColorPrimaries;
                if (!colorPrimaries_IsEnabled.HasValue)
                    VM.VideoView.Video_Color_Primaries_IsEnabled = true;
                else
                    VM.VideoView.Video_Color_Primaries_IsEnabled = colorPrimaries_IsEnabled;

                // Color Transfer Characteristics
                //VM.VideoView.Video_Color_TransferCharacteristics_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().ColorTransferChar;
                bool? colorTransferCharacteristics_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().ColorTransferChar;
                if (!colorTransferCharacteristics_IsEnabled.HasValue)
                    VM.VideoView.Video_Color_TransferCharacteristics_IsEnabled = true;
                else
                    VM.VideoView.Video_Color_TransferCharacteristics_IsEnabled = colorTransferCharacteristics_IsEnabled;

                // Color Matrix
                //VM.VideoView.Video_Color_Matrix_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().ColorMatrix;
                bool? colorMatrix_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().ColorMatrix;
                if (!colorMatrix_IsEnabled.HasValue)
                    VM.VideoView.Video_Color_Matrix_IsEnabled = true;
                else
                    VM.VideoView.Video_Color_Matrix_IsEnabled = colorMatrix_IsEnabled;

                // Subtitle Codec
                //VM.SubtitleView.Subtitle_Codec_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().SubtitleCodec;
                bool? subtitleCodec_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().SubtitleCodec;
                if (!subtitleCodec_IsEnabled.HasValue)
                    VM.SubtitleView.Subtitle_Codec_IsEnabled = true;
                else
                    VM.SubtitleView.Subtitle_Codec_IsEnabled = subtitleCodec_IsEnabled;

                // Subtitle Stream
                //VM.SubtitleView.Subtitle_Stream_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().SubtitleStream;
                bool? subtitleStream_IsEnabled = _codec_class[codec_SelectedItem].controls_Enabled.FirstOrDefault().SubtitleStream;
                if (!subtitleStream_IsEnabled.HasValue)
                    VM.SubtitleView.Subtitle_Stream_IsEnabled = true;
                else
                    VM.SubtitleView.Subtitle_Stream_IsEnabled = subtitleStream_IsEnabled;

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


                // --------------------------------------------------
                // Selected Items
                // --------------------------------------------------
                // -------------------------
                // Encode Speed
                // -------------------------
                string encodeSpeed = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().EncodeSpeed;

                // Has Value
                if (!string.IsNullOrEmpty(encodeSpeed))
                {
                    VM.VideoView.Video_EncodeSpeed_SelectedItem = encodeSpeed;
                }
                // Previous Item
                else
                {
                    //MessageBox.Show("2 Previous Item: " + MainWindow.Video_EncodeSpeed_PreviousItem); //debug // empty on change

                    // Select the Prevoius Codec's Item
                    VM.VideoView.Video_EncodeSpeed_SelectedItem = MainWindow.SelectedItem(
                                                                      VM.VideoView.Video_EncodeSpeed_Items.Select(x => x.Name).ToList(),
                                                                      MainWindow.Video_EncodeSpeed_PreviousItem
                                                                  );
                    //MessageBox.Show("3 Selected Item: " + VM.VideoView.Video_EncodeSpeed_SelectedItem); //debug
                    // Clear Previous Item
                    MainWindow.Video_EncodeSpeed_PreviousItem = string.Empty;
                }

                // -------------------------
                // HW Accel
                // -------------------------
                string hwAccel = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().HWAccel;

                if (!string.IsNullOrEmpty(hwAccel))
                {
                    VM.VideoView.Video_HWAccel_SelectedItem = hwAccel;
                }

                // -------------------------
                // Quality
                // -------------------------
                string quality = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().Quality;

                // Has Value
                if (!string.IsNullOrEmpty(quality))
                {
                    VM.VideoView.Video_Quality_SelectedItem = quality;
                }
                // Previous Item
                else
                {
                    // Select the Prevoius Codec's Item
                    VM.VideoView.Video_Quality_SelectedItem = MainWindow.SelectedItem(
                                                                      VM.VideoView.Video_Quality_Items.Select(x => x.Name).ToList(),
                                                                      MainWindow.Video_Quality_PreviousItem
                                                                  );
                    // Clear Previous Item
                    MainWindow.Video_Quality_PreviousItem = string.Empty;
                }

                // For errors causing ComboBox not to select an item
                if (string.IsNullOrWhiteSpace(VM.VideoView.Video_Quality_SelectedItem))
                {
                    // Default to First Item
                    VM.VideoView.Video_Quality_Items.FirstOrDefault();
                }

                // -------------------------
                // Pixel Format
                // -------------------------
                switch (VM.VideoView.Video_Quality_SelectedItem)
                {
                    // Lossless
                    case "Lossless":
                        string pixelFormatLossless = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().PixelFormat_Lossless;

                        if (!string.IsNullOrEmpty(pixelFormatLossless))
                        {
                            VM.VideoView.Video_PixelFormat_SelectedItem = pixelFormatLossless;
                        }
                        break;

                    // Lossy
                    default:
                        string pixelFormat = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().PixelFormat;

                        if (!string.IsNullOrEmpty(pixelFormat))
                        {
                            VM.VideoView.Video_PixelFormat_SelectedItem = pixelFormat;
                        }
                        break;
                }

                // -------------------------
                // Optimize
                // -------------------------
                string optimize = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().Optimize;

                // Has Value
                if (!string.IsNullOrEmpty(optimize))
                {
                    VM.VideoView.Video_Optimize_SelectedItem = optimize;
                }
                // Previous Item
                else
                {
                    // Select the Prevoius Codec's Item
                    VM.VideoView.Video_Optimize_SelectedItem = MainWindow.SelectedItem(
                                                                      VM.VideoView.Video_Optimize_Items.Select(x => x.Name).ToList(),
                                                                      MainWindow.Video_Optimize_PreviousItem
                                                                  );
                    // Clear Previous Item
                    MainWindow.Video_Optimize_PreviousItem = string.Empty;
                }

                // -------------------------
                // Optimize Tune
                // -------------------------
                string tune = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().Optimize_Tune;

                // Has Value
                if (!string.IsNullOrEmpty(tune))
                {
                    VM.VideoView.Video_Optimize_Tune_SelectedItem = tune;
                }
                // Previous Item
                else
                {
                    // Select the Prevoius Codec's Item
                    VM.VideoView.Video_Optimize_Tune_SelectedItem = MainWindow.SelectedItem(
                                                                      VM.VideoView.Video_Optimize_Tune_Items.ToList(),
                                                                      MainWindow.Video_Optimize_Tune_PreviousItem
                                                                  );
                    //MessageBox.Show("1 Selected Item: " + VM.VideoView.Video_Optimize_Tune_SelectedItem); //debug // empty on change
                    // Clear Previous Item
                    MainWindow.Video_Optimize_Tune_PreviousItem = string.Empty;
                }

                // -------------------------
                // Optimize Profile
                // -------------------------
                string profile = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().Optimize_Profile;

                // Has Value
                if (!string.IsNullOrEmpty(profile))
                {
                    VM.VideoView.Video_Optimize_Profile_SelectedItem = profile;
                }
                // Previous Item
                else
                {
                    // Select the Prevoius Codec's Item
                    VM.VideoView.Video_Optimize_Profile_SelectedItem = MainWindow.SelectedItem(
                                                                      VM.VideoView.Video_Optimize_Profile_Items.ToList(),
                                                                      MainWindow.Video_Optimize_Profile_PreviousItem
                                                                  );
                    // Clear Previous Item
                    MainWindow.Video_Optimize_Profile_PreviousItem = string.Empty;
                }

                // -------------------------
                // Optimize Level
                // -------------------------
                string level = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().Optimize_Level;

                // Has Value
                if (!string.IsNullOrEmpty(level))
                {
                    VM.VideoView.Video_Optimize_Level_SelectedItem = level;
                }
                // Previous Item
                else
                {
                    // Select the Prevoius Codec's Item
                    VM.VideoView.Video_Optimize_Level_SelectedItem = MainWindow.SelectedItem(
                                                                      VM.VideoView.Video_Optimize_Level_Items.ToList(),
                                                                      MainWindow.Video_Optimize_Level_PreviousItem
                                                                  );
                    // Clear Previous Item
                    MainWindow.Video_Optimize_Level_PreviousItem = string.Empty;
                }

                // -------------------------
                // FPS
                // -------------------------
                string fps = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().FPS;

                if (!string.IsNullOrEmpty(fps))
                {
                    VM.VideoView.Video_FPS_SelectedItem = fps;
                }

                // -------------------------
                // Speed
                // -------------------------
                string speed = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().Speed;

                if (!string.IsNullOrEmpty(speed))
                {
                    VM.VideoView.Video_Speed_SelectedItem = speed;
                }

                // -------------------------
                // Vsync
                // -------------------------
                string vsync = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().Vsync;

                if (!string.IsNullOrEmpty(vsync))
                {
                    VM.VideoView.Video_Vsync_SelectedItem = vsync;
                }

                // -------------------------
                // Scale
                // -------------------------
                string scale = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().Scale;

                if (!string.IsNullOrEmpty(scale))
                {
                    VM.VideoView.Video_Scale_SelectedItem = scale;
                }

                // -------------------------
                // Color Range
                // -------------------------
                string colorRange = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().ColorRange;

                if (!string.IsNullOrEmpty(colorRange))
                {
                    VM.VideoView.Video_Color_Range_SelectedItem = colorRange;
                }

                // -------------------------
                // Color Space
                // -------------------------
                string colorSpace = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().ColorSpace;

                if (!string.IsNullOrEmpty(colorSpace))
                {
                    VM.VideoView.Video_Color_Space_SelectedItem = colorSpace;
                }

                // -------------------------
                // Color Primaries
                // -------------------------
                string colorPrimaries = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().ColorPrimaries;

                if (!string.IsNullOrEmpty(colorPrimaries))
                {
                    VM.VideoView.Video_Color_Primaries_SelectedItem = colorPrimaries;
                }

                // -------------------------
                // Color Transfer Characteristics
                // -------------------------
                string colorTransferChar = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().ColorTransferChar;

                if (!string.IsNullOrEmpty(colorTransferChar))
                {
                    VM.VideoView.Video_Color_TransferCharacteristics_SelectedItem = colorTransferChar;
                }

                // -------------------------
                // Color Matrix
                // -------------------------
                string colorMatrix = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().ColorMatrix;

                if (!string.IsNullOrEmpty(colorMatrix))
                {
                    VM.VideoView.Video_Color_Matrix_SelectedItem = colorMatrix;
                }

                // -------------------------
                // Filters
                // -------------------------
                // Select Defaults
                if (codec_SelectedItem == "Copy" ||
                    codec_SelectedItem == "None")
                {
                    Filters.Video.VideoFilters_ControlsSelectDefaults();
                }


                // --------------------------------------------------
                // Checked
                // --------------------------------------------------
                // Video VBR
                //VM.VideoView.Video_VBR_IsChecked = _codec_class[codec_SelectedItem].controls_Checked.FirstOrDefault().VBR;
                if (VM.VideoView.Video_VBR_IsEnabled == true)
                {
                    bool? vbr_IsChecked = _codec_class[codec_SelectedItem].controls_Checked.FirstOrDefault().VBR;
                    if (!vbr_IsChecked.HasValue)
                        VM.VideoView.Video_VBR_IsChecked = true;
                    else
                        VM.VideoView.Video_VBR_IsChecked = vbr_IsChecked;
                }
                else
                {
                    VM.VideoView.Video_VBR_IsChecked = false;
                }


                // --------------------------------------------------
                // Expanded
                // --------------------------------------------------
                // Optimize
                //VM.VideoView.Video_Optimize_IsExpanded = _codec_class[codec_SelectedItem].controls_Expanded.FirstOrDefault().Optimize;
                if (VM.VideoView.Video_Optimize_IsEnabled == true)
                {
                    bool? optimize_IsExpanded = _codec_class[codec_SelectedItem].controls_Expanded.FirstOrDefault().Optimize;
                    if (!optimize_IsExpanded.HasValue)
                        VM.VideoView.Video_Optimize_IsExpanded = true;
                    else
                        VM.VideoView.Video_Optimize_IsExpanded = optimize_IsExpanded;
                }
                else
                {
                    VM.VideoView.Video_Optimize_IsExpanded = false;
                }
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
                // Lossless
                // -------------------------
                case "Lossless":
                    // Encode Speed
                    if (//VM.VideoView.Video_Codec_SelectedItem == "H264 AMF" ||// n/a
                        VM.VideoView.Video_Codec_SelectedItem == "H264 NVENC" ||
                        //VM.VideoView.Video_Codec_SelectedItem == "H264 QSV" || // n/a
                        //VM.VideoView.Video_Codec_SelectedItem == "HEVC AMF" ||
                        VM.VideoView.Video_Codec_SelectedItem == "HEVC NVENC"
                        //VM.VideoView.Video_Codec_SelectedItem == "HEVC QSV"// n/a
                       )
                    {
                        VM.VideoView.Video_EncodeSpeed_SelectedItem = "Lossless";
                    }

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
                    VM.VideoView.Video_Scale_IsEnabled = true;
                    break;

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
                        VM.VideoView.Video_Codec_SelectedItem == "H264_AMF" ||
                        VM.VideoView.Video_Codec_SelectedItem == "H264_NVENC" ||
                        VM.VideoView.Video_Codec_SelectedItem == "H264_QSV" ||
                        //VM.VideoView.Video_Codec_SelectedItem == "x265" || // n/a
                        //VM.VideoView.Video_Codec_SelectedItem == "HEVC AMF" || // n/a
                        //VM.VideoView.Video_Codec_SelectedItem == "HEVC NVENC" || // n/a
                        //VM.VideoView.Video_Codec_SelectedItem == "HEVC QSV"
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
                        string lossless = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().PixelFormat_Lossless;

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
                        string lossy = _codec_class[codec_SelectedItem].controls_Selected.FirstOrDefault().PixelFormat;

                        if (!string.IsNullOrEmpty(lossy))
                        {
                            VM.VideoView.Video_PixelFormat_SelectedItem = lossy;
                        }

                        //MessageBox.Show(_codec_class[codec_SelectedItem].ToString()); //debug
                    }
                }
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
            if (VM.VideoView.Video_Optimize_SelectedItem != "Custom") // Ignore Custom
            {
                // Tune
                VM.VideoView.Video_Optimize_Tune_SelectedItem = VM.VideoView.Video_Optimize_Items
                                                                .FirstOrDefault(item => item.Name == VM.VideoView.Video_Optimize_SelectedItem)?.Tune;

                // Profile
                VM.VideoView.Video_Optimize_Profile_SelectedItem = VM.VideoView.Video_Optimize_Items
                                                                   .FirstOrDefault(item => item.Name == VM.VideoView.Video_Optimize_SelectedItem)?.Profile;
                // Level
                VM.VideoView.Video_Optimize_Level_SelectedItem = VM.VideoView.Video_Optimize_Items
                                                                 .FirstOrDefault(item => item.Name == VM.VideoView.Video_Optimize_SelectedItem)?.Level;
            }

            // -------------------------
            // Special Rule WebM
            // -------------------------
            // If coming from Format webm, default to None.
            // Do not clear Format_Container_PreviousItem here, 
            // it is cleared at the end of the chain in Controls.Format.AudioStreamControls().
            if (MainWindow.Format_Container_PreviousItem == "webm")
            {
                VM.VideoView.Video_Optimize_SelectedItem = "None";
            }
        }


        /// <summary>
        /// Encoding Pass Controls
        /// <summary>
        public static void EncodingPassControls()
        {
            InitializeCodecs();

            _codec_class[VM.VideoView.Video_Codec_SelectedItem].EncodingPass();

            // CRF TextBox
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
