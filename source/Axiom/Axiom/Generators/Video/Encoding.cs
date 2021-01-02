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

  * HWAccelerationDecode
  * HWAccelerationTranscode
  * HWAccelerationCodecOverride
  * VideoEncodeSpeed
---------------------------------- */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using Axiom;
using System.Collections.ObjectModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Generate.Video
{
    public class Encoding
    {
        public static string hwAccelDecode { get; set; }
        public static string hwAccelTranscode { get; set; }
        public static string vEncodeSpeed { get; set; }

        /// <summary>
        /// Hardware Acceleration Decode
        /// <summary>
        /// <remarks>
        /// https://trac.ffmpeg.org/wiki/HWAccelIntro
        /// https://trac.ffmpeg.org/wiki/Hardware/QuickSync
        /// </remarks>
        public static String HWAccelerationDecode(string mediaType_SelectedItem,
                                                    string codec_SelectedItem,
                                                    string hwaccel_decode_SelectedItem
                                                    )
        {
            // Check:
            // HW Accel Not Off
            // Media Type Not Audio
            if (hwaccel_decode_SelectedItem != "off" &&
                mediaType_SelectedItem != "Audio")
            {
                // --------------------------------------------------
                // All Codecs
                // --------------------------------------------------
                switch (hwaccel_decode_SelectedItem)
                {
                    // -------------------------
                    // Auto
                    // -------------------------
                    case "auto":
                        hwAccelDecode = "-hwaccel auto";
                        break;

                    // -------------------------
                    // CUDA
                    // -------------------------
                    case "CUDA":
                        hwAccelDecode = "-hwaccel cuda";
                        //// Decode Only
                        //if (VM.VideoView.Video_HWAccel_Transcode_SelectedItem == "off" ||
                        //    VM.VideoView.Video_HWAccel_Transcode_SelectedItem == "auto")
                        //{
                        //    hwAccelDecode = "-hwaccel cuda";
                        //}
                        //// Decode + Transcode
                        //else
                        //{
                        //    hwAccelDecode = "-hwaccel cuda -hwaccel_output_format cuda";
                        //}
                        break;

                    // -------------------------
                    // CUVID
                    // -------------------------
                    case "CUVID":
                        hwAccelDecode = "-hwaccel cuvid";
                        break;

                    // -------------------------
                    // D3D11VA
                    // -------------------------
                    case "D3D11VA":
                        hwAccelDecode = "-hwaccel d3d11va";
                        break;

                    // -------------------------
                    // DXVA2
                    // -------------------------
                    case "DXVA2":
                        hwAccelDecode = "-hwaccel dxva2";
                        break;

                    // -------------------------
                    // Intel QSV
                    // -------------------------
                    case "Intel QSV":
                        hwAccelDecode = "-hwaccel qsv -hwaccel_output_format qsv";
                        break;

                    // -------------------------
                    // Other
                    // -------------------------
                    default:
                        hwAccelDecode = string.Empty;
                        break;
                }
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Decode: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(hwaccel_decode_SelectedItem) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            return hwAccelDecode;
        }


        /// <summary>
        /// Hardware Acceleration Transcode
        /// <summary>
        /// <remarks>
        /// https://trac.ffmpeg.org/wiki/HWAccelIntro
        /// https://trac.ffmpeg.org/wiki/Hardware/QuickSync
        /// </remarks>
        public static String HWAccelerationTranscode(string mediaType_SelectedItem,
                                                        string codec_SelectedItem,
                                                        string hwaccel_transcode_SelectedItem
                                                    )
        {
            // Check:
            // HW Accel Not Off
            // Media Type Not Audio
            if (hwaccel_transcode_SelectedItem != "off" &&
                mediaType_SelectedItem != "Audio")
            {
                // --------------------------------------------------
                // All Codecs
                // --------------------------------------------------
                switch (hwaccel_transcode_SelectedItem)
                {
                    // -------------------------
                    // Auto
                    // -------------------------
                    case "auto":
                        hwAccelTranscode = string.Empty;
                        break;

                    // -------------------------
                    // AMD AMF
                    // -------------------------
                    case "CUDA":
                        hwAccelTranscode = string.Empty;
                        break;

                    // -------------------------
                    // NVIDIA NVENC
                    // -------------------------
                    case "NVIDIA NVENC":
                        hwAccelTranscode = string.Empty;
                        break;

                    // -------------------------
                    // Intel QSV
                    // -------------------------
                    case "Intel QSV":
                        //hwAccelTranscode = "-init_hw_device qsv=hw -filter_hw_device hw"; //software
                        hwAccelTranscode = string.Empty;
                        break;

                    // -------------------------
                    // Other
                    // -------------------------
                    default:
                        hwAccelTranscode = string.Empty;
                        break;
                }
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Transcode: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(hwaccel_transcode_SelectedItem) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            return hwAccelTranscode;
        }


        /// <summary>
        /// Hardware Acceleration Codec Override
        /// <summary>
        /// <remarks>
        /// https://trac.ffmpeg.org/wiki/HWAccelIntro
        /// https://trac.ffmpeg.org/wiki/Hardware/QuickSync
        /// </remarks>
        //public static String HWAccelerationCodecOverride(string hwaccel_transcode_SelectedItem,
        //                                                 string codec_SelectedItem
        //                                                 )
        //{
        //    switch (hwaccel_transcode_SelectedItem)
        //    {
        //        // -------------------------
        //        // AMD AMF
        //        // -------------------------
        //        case "AMD AMF":
        //            // x264
        //            if (codec_SelectedItem == "x264")
        //            {
        //                Codec.vCodec = "-c:v h264_amf";
        //            }
        //            // x265
        //            else if (codec_SelectedItem == "x265" ||
        //                     codec_SelectedItem == "HEVC_NVENC")
        //            {
        //                Codec.vCodec = "-c:v hevc_amf";
        //            }
        //            break;

        //        // -------------------------
        //        // NVIDIA NVENC
        //        // -------------------------
        //        case "NVIDIA NVENC":
        //            // Only x264/ x265
        //            // e.g. ffmpeg -i input -c:v h264_nvenc -profile high444p -pix_fmt yuv444p -preset default output.mp4

        //            // Override Codecs
        //            if (codec_SelectedItem == "x264")
        //            {
        //                Codec.vCodec = "-c:v h264_nvenc";
        //            }
        //            else if (codec_SelectedItem == "x265" ||
        //                     codec_SelectedItem == "HEVC_NVENC")
        //            {
        //                Codec.vCodec = "-c:v hevc_nvenc";
        //            }
        //            break;

        //        // -------------------------
        //        // Intel QSV
        //        // -------------------------
        //        case "Intel QSV":
        //            // x264
        //            if (codec_SelectedItem == "x264")
        //            {
        //                Codec.vCodec = "-c:v h264_qsv";
        //            }
        //            // x265
        //            else if (codec_SelectedItem == "x265" ||
        //                     codec_SelectedItem == "HEVC_NVENC")
        //            {
        //                Codec.vCodec = "-c:v hevc_qsv";
        //            }
        //            break;
        //    }

        //    // Log Console Message /////////
        //    Log.WriteAction = () =>
        //    {
        //        Log.logParagraph.Inlines.Add(new LineBreak());
        //        Log.logParagraph.Inlines.Add(new Bold(new Run("Codec Override: ")) { Foreground = Log.ConsoleDefault });
        //        Log.logParagraph.Inlines.Add(new Run(Codec.vCodec) { Foreground = Log.ConsoleDefault });
        //    };
        //    Log.LogActions.Add(Log.WriteAction);

        //    return Codec.vCodec;
        //}


        /// <summary>
        /// Encode Speed
        /// <summary>
        public static String VideoEncodeSpeed(ObservableCollection<ViewModel.Video.VideoEncodeSpeed> encodeSpeedItems,
                                                string encodeSpeed_SelectedItem,
                                                string codec_SelectedItem,
                                                string pass
                                                )
        {
            // -------------------------
            // Empty
            // -------------------------
            if (string.IsNullOrWhiteSpace(codec_SelectedItem))
            {
                return vEncodeSpeed;
            }

            switch (codec_SelectedItem)
            {
                // -------------------------
                // Auto
                // -------------------------
                case "Auto":
                    if (pass == "CRF" ||
                        pass == "1 Pass")
                    {
                        vEncodeSpeed = encodeSpeedItems.FirstOrDefault(item => item.Name == encodeSpeed_SelectedItem)?.Command;
                    }
                    else if (pass == "2 Pass")
                    {
                        vEncodeSpeed = encodeSpeedItems.FirstOrDefault(item => item.Name == encodeSpeed_SelectedItem)?.Command_2Pass;
                    }
                    break;

                // -------------------------
                // VP8
                // -------------------------
                case "VP8":
                    if (pass == "CRF" ||
                        pass == "1 Pass")
                    {
                        vEncodeSpeed = encodeSpeedItems.FirstOrDefault(item => item.Name == encodeSpeed_SelectedItem)?.Command;
                    }
                    else if (pass == "2 Pass")
                    {
                        vEncodeSpeed = encodeSpeedItems.FirstOrDefault(item => item.Name == encodeSpeed_SelectedItem)?.Command_2Pass;
                    }
                    break;

                // -------------------------
                // Copy
                // -------------------------
                case "Copy":
                    // Skip
                    break;

                // -------------------------
                // All Other Codecs
                // -------------------------
                default:
                    vEncodeSpeed = encodeSpeedItems.FirstOrDefault(item => item.Name == encodeSpeed_SelectedItem)?.Command;
                    break;
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Encode Speed: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(encodeSpeed_SelectedItem) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            return vEncodeSpeed;
        }
    }
}
