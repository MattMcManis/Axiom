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

 * Hardware Acceleration
 * Video Codec
 * Speed
 * BitRate Mode
 * Video Quality
 * Batch Video Quality Auto
 * Video BitRate Calculator
 * Pass 1 Modifier
 * Pass 2 Modifier
 * Frame Rate To Decimal
 * Optimize
 * Pixel Format
 * FPS
 * Size Width Auto
 * Size
 * Scaling Algorithm
 * Crop
 * Images
---------------------------------- */


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    class Video
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Global Variables
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // Video
        public static string vEncodeSpeed { get; set; }
        public static string vCodec { get; set; } // Video Codec
        public static string vQuality { get; set; } // Video Quality
        public static string vBitMode { get; set; }
        public static string vLossless { get; set; }
        public static string vBitRate { get; set; } // Video BitRate
        public static string vBitRateNA { get; set; } // N/A e.g. Define 3000K
        public static string vMinRate { get; set; }
        public static string vMaxRate { get; set; }
        public static string vBufSize { get; set; }
        public static string vOptions { get; set; } // -pix_fmt, -qcomp
        public static string vCRF { get; set; } // Constant Rate Factor
        public static string colorPrimaries { get; set; }
        public static string colorTransferCharacteristics { get; set; }
        public static string colorSpace { get; set; }
        public static string colorRange { get; set; }
        public static string colorMatrix { get; set; }
        public static string pix_fmt { get; set; }
        public static string vAspectRatio { get; set; }
        public static string vScalingAlgorithm { get; set; }
        public static string fps { get; set; } // Frames Per Second
        public static string image { get; set; } // JPEG & PNG options
        public static string optTune { get; set; } // x264/x265 tuning modes
        public static string optProfile { get; set; } // x264/x265 Profile
        public static string optLevel { get; set; } // x264/x265 Level
        public static string optFlags { get; set; } // Additional Optimization Flags
        public static string optimize { get; set; } // Contains opTune + optProfile + optLevel

        //// x264 Params
        //public static List<string> x264paramsList = new List<string>(); // multiple parameters
        //public static string x264params { get; set; } // combined inline list
        //// x265 Params
        //public static List<string> x265paramsList = new List<string>(); // multiple parameters
        //public static string x265params { get; set; } // combined inline list

        // Scale
        public static string width { get; set; }
        public static string height { get; set; }
        public static string scale { get; set; } // contains size, width, height

        // Pass
        public static string v2PassArgs { get; set; } // 2-Pass Arguments
        public static string passSingle { get; set; } // 1-Pass & CRF Args
        public static string pass1Args { get; set; } // Batch 2-Pass (Pass 1)
        public static string pass2Args { get; set; } // Batch 2-Pass (Pass 2)
        public static string pass1 { get; set; } // x265 Modifier
        public static string pass2 { get; set; } // x265 Modifier

        // Batch
        public static string batchVideoAuto { get; set; }

        // Rendering
        //public static string hwAcceleration { get; set; }
        public static string hwAccelDecode { get; set; }
        public static string hwAccelTranscode { get; set; }



        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Methods
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------

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

            return hwAccelTranscode;
        }


        /// <summary>
        /// Hardware Acceleration Codec Override
        /// <summary>
        /// <remarks>
        /// https://trac.ffmpeg.org/wiki/HWAccelIntro
        /// https://trac.ffmpeg.org/wiki/Hardware/QuickSync
        /// </remarks>
        public static String HWAccelerationCodecOverride(string hwaccel_transcode_SelectedItem,
                                                         string codec_SelectedItem
                                                         )
        {
            switch (hwaccel_transcode_SelectedItem)
            {
                // -------------------------
                // AMD AMF
                // -------------------------
                case "AMD AMF":
                    // x264
                    if (codec_SelectedItem == "x264")
                    {
                        vCodec = "-c:v h264_amf";
                    }
                    // x265
                    else if (codec_SelectedItem == "x265")
                    {
                        vCodec = "-c:v hevc_amf";
                    }
                    break;

                // -------------------------
                // NVIDIA NVENC
                // -------------------------
                case "NVIDIA NVENC":
                    // Only x264/ x265
                    // e.g. ffmpeg -i input -c:v h264_nvenc -profile high444p -pix_fmt yuv444p -preset default output.mp4

                    // Override Codecs
                    if (codec_SelectedItem == "x264")
                    {
                        vCodec = "-c:v h264_nvenc";
                    }
                    else if (codec_SelectedItem == "x265")
                    {
                        vCodec = "-c:v hevc_nvenc";
                    }
                    break;

                // -------------------------
                // Intel QSV
                // -------------------------
                case "Intel QSV":
                    // x264
                    if (codec_SelectedItem == "x264")
                    {
                        vCodec = "-c:v h264_qsv";
                    }
                    // x265
                    else if (codec_SelectedItem == "x265")
                    {
                        vCodec = "-c:v hevc_qsv";
                    }
                    break;

            }

            return vCodec;
        }


        /// <summary>
        /// Video Codec
        /// <summary>
        public static String VideoCodec(string hwAccel_transcode_SelectedItem,
                                        string codec_SelectedItem, 
                                        string codec_Command
                                        )
        {
            // Passed Command
            if (codec_SelectedItem != "None")
            {
                vCodec = codec_Command;

                // HW Acceleration vCodec Override
                if (hwAccel_transcode_SelectedItem == "AMD AMF" || // h264_amf / hevc_amf
                    hwAccel_transcode_SelectedItem == "NVIDIA NVENC" || // h264_nvenc / hevc_nvenc
                    hwAccel_transcode_SelectedItem == "Intel QSV" // h264_qsv / hevc_qsv
                    )
                {
                    vCodec = HWAccelerationCodecOverride(hwAccel_transcode_SelectedItem,
                                                         codec_SelectedItem
                                                         );

                    //MessageBox.Show(vCodec); //debug
                }
            }              

            return vCodec;
        }


        /// <summary>
        /// Encode Speed
        /// <summary>
        public static String VideoEncodeSpeed(List<VideoViewModel.VideoEncodeSpeed> encodeSpeedItems,
                                              string encodeSpeed_SelectedItem,
                                              string codec_SelectedItem,
                                              string pass
                                              )
        {
            // Check:
            // Video Codec Not Copy
            if (codec_SelectedItem != "Copy")
            {
                // -------------------------
                // Auto / VP8 - Special Rules
                // -------------------------
                if (codec_SelectedItem == "VP8" ||
                    codec_SelectedItem == "Auto")
                {
                    if (pass == "CRF" ||
                        pass == "1 Pass")
                    {
                        vEncodeSpeed = encodeSpeedItems.FirstOrDefault(item => item.Name == encodeSpeed_SelectedItem)?.Command;
                    }
                    else if (pass == "2 Pass")
                    {
                        vEncodeSpeed = encodeSpeedItems.FirstOrDefault(item => item.Name == encodeSpeed_SelectedItem)?.Command_2Pass;
                    }
                }

                // -------------------------
                // All Other Codecs
                // -------------------------
                else
                {
                    vEncodeSpeed = encodeSpeedItems.FirstOrDefault(item => item.Name == encodeSpeed_SelectedItem) ?.Command;
                }


                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Video Encode Speed: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(encodeSpeed_SelectedItem) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }


            // Return Value
            return vEncodeSpeed;
        }



        /// <summary>
        /// BitRate Mode
        /// <summary>
        public static String BitRateMode(List<VideoViewModel.VideoQuality> quality_Items,
                                         string quality_SelectedItem,
                                         string bitrate_Text,
                                         bool vbr_IsChecked
                                         )
        {
            //MessageBox.Show(vbr_IsChecked.ToString()); //debug

            // Only if BitRate Textbox is not Empty (except for Auto Quality)
            if (quality_SelectedItem == "Auto" || 
                (!string.IsNullOrEmpty(bitrate_Text) && !string.IsNullOrWhiteSpace(bitrate_Text))
                )
            {
                // -------------------------
                // CBR
                // -------------------------
                if (vbr_IsChecked == false)
                {
                    // -b:v
                    vBitMode = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CBR_BitMode;

                    //MessageBox.Show(vBitMode); //debug
                }

                // -------------------------
                // VBR
                // -------------------------
                else if (vbr_IsChecked == true)
                {
                    // -q:v
                    vBitMode = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.VBR_BitMode;

                    //MessageBox.Show(vBitMode); //debug
                }
            }

            return vBitMode;
        }


        /// <summary>
        /// Video Quality - Auto
        /// <summary>
        public static void QualityAuto(bool batch_IsChecked,
                                       bool vbr_IsChecked,
                                       string container_SelectedItem,
                                       string mediaType_SelectedItem,
                                       string codec_SelectedItem,
                                       List<VideoViewModel.VideoQuality> quality_Items,
                                       string quality_SelectedItem,
                                       string pass_SelectedItem,
                                       string crf_Text,
                                       string bitrate_Text,
                                       string minrate_Text,
                                       string maxrate_Text,
                                       string bufsize_Text,
                                       string input_Text
                                       )
        {
            // BitRate
            // Video
            if (mediaType_SelectedItem == "Video")
            {
                vBitRate = VideoBitRateCalculator(container_SelectedItem,
                                                  mediaType_SelectedItem,
                                                  codec_SelectedItem,
                                                  FFprobe.vEntryType,
                                                  FFprobe.inputVideoBitRate);
            }
            // Images
            else if (mediaType_SelectedItem == "Image" ||
                     mediaType_SelectedItem == "Sequence"
                    )
            {
                vBitRate = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.VBR;
            }



            // BitRate NA
            vBitRateNA = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.NA;

            // MinRate
            if (!string.IsNullOrEmpty(minrate_Text) &&
                !string.IsNullOrWhiteSpace(minrate_Text))
            {
                vMinRate = "-minrate " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.MinRate;
            }

            // MaxRate
            if (!string.IsNullOrEmpty(maxrate_Text) &&
                !string.IsNullOrWhiteSpace(maxrate_Text))
            {
                vMaxRate = "-maxrate " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.MaxRate;
            }

            // BufSize
            if (!string.IsNullOrEmpty(bufsize_Text) &&
                !string.IsNullOrWhiteSpace(bufsize_Text))
            {
                vBufSize = "-bufsize " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.BufSize;
            }


            // -------------------------
            // Single
            // -------------------------
            if (batch_IsChecked == false)
            {
                // -------------------------
                // Input File Has Video
                // Input Video BitRate NOT Detected
                // Input Video Codec Detected
                // -------------------------
                if (string.IsNullOrEmpty(FFprobe.inputVideoBitRate) ||
                    string.IsNullOrWhiteSpace(FFprobe.inputVideoBitRate) ||
                    FFprobe.inputVideoBitRate == "N/A")
                {
                    // -------------------------
                    // Codec Detected
                    // -------------------------
                    if (!string.IsNullOrEmpty(FFprobe.inputVideoCodec))
                    {
                        // 1 Pass / CRF
                        //
                        if (pass_SelectedItem == "1 Pass" ||
                            pass_SelectedItem == "CRF")
                        {
                            vCRF = string.Empty;

                            if (!string.IsNullOrEmpty(vBitRateNA) &&
                                !string.IsNullOrWhiteSpace(vBitRateNA))
                            {
                                vBitMode = BitRateMode(quality_Items,
                                                       quality_SelectedItem,
                                                       bitrate_Text,
                                                       vbr_IsChecked
                                                       );

                                vBitRate = vBitRateNA; // N/A e.g. Define 3000K
                            }
                        }

                        // 2 Pass
                        //
                        else if (pass_SelectedItem == "2 Pass")
                        {
                            vCRF = string.Empty;

                            //MessageBox.Show(auto_bitrate_na); //debug

                            if (!string.IsNullOrEmpty(vBitRateNA) &&
                                !string.IsNullOrWhiteSpace(vBitRateNA))
                            {
                                vBitMode = BitRateMode(quality_Items,
                                                        quality_SelectedItem,
                                                        bitrate_Text,
                                                        vbr_IsChecked
                                                        );
                                vBitRate = vBitRateNA; // N/A e.g. Define 3000K
                            }
                        }
                    }
                    // -------------------------
                    // Codec Not Detected
                    // -------------------------
                    else
                    {
                        vCRF = string.Empty;

                        // Default to NA BitRate
                        vBitMode = BitRateMode(quality_Items,
                                               quality_SelectedItem,
                                               bitrate_Text,
                                               vbr_IsChecked
                                               );

                        vBitRate = VideoBitRateCalculator(container_SelectedItem,
                                                          mediaType_SelectedItem,
                                                          codec_SelectedItem,
                                                          FFprobe.vEntryType,
                                                          vBitRateNA);

                        vMinRate = string.Empty;
                        vMaxRate = string.Empty;
                        vBufSize = string.Empty;

                        // Pixel Format
                        vOptions = string.Empty;
                    }
                }

                // -------------------------
                // Input File Has Video
                // Input Video BitRate IS Detected
                // Input Video Codec Detected
                // -------------------------
                else if (!string.IsNullOrEmpty(FFprobe.inputVideoBitRate) &&
                         !string.IsNullOrWhiteSpace(FFprobe.inputVideoBitRate) &&
                         FFprobe.inputVideoBitRate != "N/A")
                {
                    // -------------------------
                    // Codec Detected
                    // -------------------------
                    if (!string.IsNullOrEmpty(FFprobe.inputVideoCodec) &&
                        !string.IsNullOrWhiteSpace(FFprobe.inputVideoCodec))
                    {
                        //MessageBox.Show("5 " + vBitRate); //debug

                        vCRF = string.Empty;

                        if (!string.IsNullOrEmpty(vBitRate) &&
                            !string.IsNullOrWhiteSpace(vBitRate))
                        {
                            vBitMode = BitRateMode(quality_Items,
                                                   quality_SelectedItem,
                                                   bitrate_Text,
                                                   vbr_IsChecked
                                                   );
                            //MessageBox.Show(vBitMode); //debug
                        }


                    }
                    // -------------------------
                    // Codec Not Detected
                    // -------------------------
                    else
                    {
                        vCRF = string.Empty;

                        vBitMode = string.Empty;
                        vBitRate = string.Empty;
                        vMinRate = string.Empty;
                        vMaxRate = string.Empty;
                        vBufSize = string.Empty;

                        // Pixel Format
                        vOptions = string.Empty;
                    }
                }
            }

            // -------------------------
            // Batch
            // -------------------------
            else if (batch_IsChecked == true)
            {
                // Use the CMD Batch Video Variable
                vBitMode = "-b:v";
                //vBitRate = "%V";

                // CMD
                if (VM.ConfigureView.Shell_SelectedItem == "CMD")
                {
                    vBitRate = "%V";
                }

                // PowerShell
                else if (VM.ConfigureView.Shell_SelectedItem == "PowerShell")
                {
                    vBitRate = "$vBitrate";
                }
            }
        }


        /// <summary>
        /// Video Quality - Lossless
        /// <summary>
        public static void QualityLossless(string codec_SelectedItem,
                                           List<VideoViewModel.VideoQuality> qualityItems
                                           )
        {
            // -------------------------
            // x265 Params
            // -------------------------
            if (codec_SelectedItem == "x265")
            {
                // e.g. -x265-params "lossless"
                //x265paramsList.Add("lossless");
                VideoParams.vParamsList.Add("lossless");
            }
            // -------------------------
            // All Other Codecs
            // -------------------------
            else
            {
                vLossless = qualityItems.FirstOrDefault(item => item.Name == "Lossless")?.Lossless;
            }
        }


        /// <summary>
        /// Video Quality - Custom
        /// <summary>
        public static void QualityCustom(bool vbr_IsChecked,
                                         List<VideoViewModel.VideoQuality> quality_Items,
                                         string quality_SelectedItem,
                                         string crf_Text,
                                         string bitrate_Text,
                                         string minrate_Text,
                                         string maxrate_Text,
                                         string bufsize_Text
                                         )
        {
            // CRF
            if (!string.IsNullOrEmpty(crf_Text) &&
                !string.IsNullOrWhiteSpace(crf_Text))
            {
                vCRF = "-crf " + crf_Text;
            }

            // BitRate Mode
            vBitMode = BitRateMode(quality_Items,
                                   quality_SelectedItem,
                                   bitrate_Text,
                                   vbr_IsChecked
                                   );

            // BitRate
            vBitRate = bitrate_Text;


            // MinRate
            if (!string.IsNullOrEmpty(minrate_Text) &&
                !string.IsNullOrWhiteSpace(minrate_Text))
            {
                vMinRate = "-minrate " + minrate_Text;
            }

            // MaxRate
            if (!string.IsNullOrEmpty(maxrate_Text) &&
                !string.IsNullOrWhiteSpace(maxrate_Text))
            {
                vMaxRate = "-maxrate " + maxrate_Text;
            }

            // BufSize
            if (!string.IsNullOrEmpty(bufsize_Text) &&
                !string.IsNullOrWhiteSpace(bufsize_Text))
            {
                vBufSize = "-bufsize " + bufsize_Text;
            }
        }


        /// <summary>
        /// Video Quality - Preset
        /// <summary>
        public static void QualityPreset(bool vbr_IsChecked,
                                         string codec_SelectedItem,
                                         List<VideoViewModel.VideoQuality> quality_Items,
                                         string quality_SelectedItem,
                                         string pass_SelectedItem,
                                         string crf_Text,
                                         string bitrate_Text,
                                         string minrate_Text,
                                         string maxrate_Text,
                                         string bufsize_Text
                                         )
        {
            // BitRate Mode
            vBitMode = BitRateMode(quality_Items,
                                   quality_SelectedItem,
                                   bitrate_Text,
                                   vbr_IsChecked
                                   );

            // MinRate
            if (!string.IsNullOrEmpty(minrate_Text) &&
                !string.IsNullOrWhiteSpace(minrate_Text))
            {
                vMinRate = "-minrate " + minrate_Text;
            }

            // MaxRate
            if (!string.IsNullOrEmpty(maxrate_Text) &&
                !string.IsNullOrWhiteSpace(maxrate_Text))
            {
                vMaxRate = "-maxrate " + maxrate_Text;
            }

            // BufSize
            if (!string.IsNullOrEmpty(bufsize_Text) &&
                !string.IsNullOrWhiteSpace(bufsize_Text))
            {
                vBufSize = "-bufsize " + bufsize_Text;
            }

            // --------------------------------------------------
            // Encoding Pass
            // --------------------------------------------------
            // -------------------------
            // auto
            // -------------------------
            if (pass_SelectedItem == "auto")
            {
                vCRF = string.Empty;
                vBitRate = string.Empty;
                vMinRate = string.Empty;
                vMaxRate = string.Empty;
                vBufSize = string.Empty;
            }

            // -------------------------
            // CRF
            // -------------------------
            else if (pass_SelectedItem == "CRF")
            {
                // -------------------------
                // x265 Params
                // -------------------------
                if (codec_SelectedItem == "x265")
                {
                    // x265 Params
                    VideoParams.vParamsList.Add("crf=" + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CRF);
                    vCRF = string.Empty;
                }
                // -------------------------
                // All Other Codecs
                // -------------------------
                else
                {
                    //crf = items.FirstOrDefault(item => item.Name == selectedQuality) ?.CRF;
                    vBitRate = bitrate_Text;

                    if (!string.IsNullOrEmpty(crf_Text) &&
                        !string.IsNullOrWhiteSpace(crf_Text))
                    {
                        vCRF = "-crf " + crf_Text;
                    }
                }
            }

            // -------------------------
            // 1 & 2 Pass
            // -------------------------
            else if (pass_SelectedItem == "1 Pass" ||
                     pass_SelectedItem == "2 Pass")
            {
                // -------------------------
                // BitRate
                // -------------------------
                vBitRate = bitrate_Text;
            }
        }



        /// <summary>
        /// Video Quality
        /// <summary>
        public static String VideoQuality(bool batch_IsChecked,
                                          bool vbr_IsChecked,
                                          string container_SelectedItem,
                                          string mediaType_SelectedItem,
                                          string codec_SelectedItem,
                                          List<VideoViewModel.VideoQuality> quality_Items,
                                          string quality_SelectedItem,
                                          string pass_SelectedItem,
                                          string crf_Text,
                                          string bitrate_Text,
                                          string minrate_Text,
                                          string maxrate_Text,
                                          string bufsize_Text,
                                          string input_Text
                                          )
        {
            //MessageBox.Show(vbr_IsChecked.ToString()); //debug

            // Check:
            // Video Codec Not Copy
            if (codec_SelectedItem != "Copy")
            {
                // -------------------------
                // Auto
                // -------------------------
                if (quality_SelectedItem == "Auto" &&
                    codec_SelectedItem != "FFV1" &&   // Special Rule
                    codec_SelectedItem != "HuffYUV"   // FFV1, HuffYUV cannot use Auto BitRate, Lossless Only, Auto is used for Codec Copy
                    )  
                {
                    QualityAuto(batch_IsChecked,
                                vbr_IsChecked,
                                container_SelectedItem,
                                mediaType_SelectedItem,
                                codec_SelectedItem,
                                quality_Items,
                                quality_SelectedItem,
                                pass_SelectedItem,
                                crf_Text,
                                bitrate_Text,
                                minrate_Text,
                                maxrate_Text,
                                bufsize_Text,
                                input_Text
                                );
                }

                // -------------------------
                // Lossless
                // -------------------------
                else if (quality_SelectedItem == "Lossless")
                {
                    QualityLossless(codec_SelectedItem,
                                    quality_Items
                                    );
                }

                // -------------------------
                // Custom
                // -------------------------
                else if (quality_SelectedItem == "Custom")
                {
                    QualityCustom(vbr_IsChecked,
                                  quality_Items,
                                  quality_SelectedItem,
                                  crf_Text,
                                  bitrate_Text,
                                  minrate_Text,
                                  maxrate_Text,
                                  bufsize_Text
                                  );
                }

                // -------------------------
                // Preset: Ultra, High, Medium, Low, Sub
                // -------------------------
                else
                {
                    QualityPreset(vbr_IsChecked,
                                  codec_SelectedItem,
                                  quality_Items,
                                  quality_SelectedItem,
                                  pass_SelectedItem,
                                  crf_Text,
                                  bitrate_Text,
                                  minrate_Text,
                                  maxrate_Text,
                                  bufsize_Text
                                  );
                }

                //MessageBox.Show(vBufSize); //debug

                // --------------------------------------------------
                // Combine
                // --------------------------------------------------
                List<string> vQualityArgs = new List<string>();

                // -------------------------
                // x265 Params
                // -------------------------
                //if (codec_SelectedItem == "x265" &&
                //    x265paramsList.Count > 0)
                //{
                //    x265params = "-x265-params " + "\"" + string.Join(":", x265paramsList
                //                                                           .Where(s => !string.IsNullOrEmpty(s)))
                //                                 + "\"";
                //}
                //else
                //{
                //    x265params = string.Empty;
                //}

                // -------------------------
                // CRF
                // -------------------------
                if (pass_SelectedItem == "CRF")
                {
                    vQualityArgs = new List<string>()
                    {
                        vLossless,
                        vBitMode,
                        vBitRate,
                        vMinRate,
                        vMaxRate,
                        vBufSize,
                        vCRF,
                        vOptions
                    };
                }

                // -------------------------
                // 1 Pass, 2 Pass, auto
                // -------------------------
                else if (pass_SelectedItem == "1 Pass" ||
                         pass_SelectedItem == "2 Pass" ||
                         pass_SelectedItem == "auto")
                {
                    // Quality Arguments List
                    vQualityArgs = new List<string>()
                    {
                        vLossless,
                        vBitMode,
                        vBitRate,
                        vMinRate,
                        vMaxRate,
                        vBufSize,
                        vOptions
                    };

                    //MessageBox.Show(string.Join("\n", vQualityArgs)); //debug
                }

                // Format European English comma to US English peroid - 1,234 to 1.234
                vQuality = string.Format(System.Globalization.CultureInfo.GetCultureInfo("en-US"), "{0:0.0}", vQuality);

                // Join Video Quality Args List
                vQuality = string.Join(" ", vQualityArgs
                                            .Where(s => !string.IsNullOrEmpty(s))
                                            .Where(s => !string.IsNullOrWhiteSpace(s))
                                            .Where(s => !s.Equals("\n"))
                                      );

                // Log Console Message /////////    
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("BitRate: ")) { Foreground = Log.ConsoleDefault });
                    if (!string.IsNullOrEmpty(vBitRate))
                    {
                        Log.logParagraph.Inlines.Add(new Run(vBitRate) { Foreground = Log.ConsoleDefault });
                    }
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("CRF: ")) { Foreground = Log.ConsoleDefault });
                    if (!string.IsNullOrEmpty(crf_Text))
                    {
                        Log.logParagraph.Inlines.Add(new Run(crf_Text) { Foreground = Log.ConsoleDefault }); //crf combines with bitrate
                }
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Options: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(vOptions) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            return vQuality;
        }



        /// <summary>
        /// Batch Video Quality Auto (Method)
        /// <summary>
        public static String BatchVideoQualityAuto(bool batch_IsChecked,
                                                   string codec_SelectedItem,
                                                   string quality_SelectedItem
                                                   )
        {
            // -------------------------
            // Batch Auto
            // -------------------------

            // Check:
            // Video Codec Not Copy
            if (codec_SelectedItem != "Copy")
            {
                // Batch Check
                //if (batch_IsChecked == true)
                //{
                // -------------------------
                // Video Auto BitRates
                // -------------------------
                if (quality_SelectedItem == "Auto")
                {
                    List<string> batchVideoAutoList = new List<string>();

                    // -------------------------
                    // CMD
                    // -------------------------
                    if (VM.ConfigureView.Shell_SelectedItem == "CMD")
                    {
                        batchVideoAutoList = new List<string>()
                        {
                            // size
                            "& for /F \"delims=\" %S in ('@" + FFprobe.ffprobe + " -v error -select_streams v:0 -show_entries format^=size -of default^=noprint_wrappers^=1:nokey^=1 \"%~f\" 2^>^&1') do (SET size=%S)",
                            // set %S to %size%
                            "\r\n\r\n" + "& for /F %S in ('echo %size%') do (echo)",

                            // duration
                            "\r\n\r\n" + "& for /F \"delims=\" %D in ('@" + FFprobe.ffprobe + " -v error -select_streams v:0 -show_entries format^=duration -of default^=noprint_wrappers^=1:nokey^=1 \"%~f\" 2^>^&1') do (SET duration=%D)",
                            // remove duration decimals
                            "\r\n\r\n" + "& for /F \"tokens=1 delims=.\" %R in ('echo %duration%') do (SET duration=%R)",
                            // set %D to %duration%
                            "\r\n\r\n" + "& for /F %D in ('echo %duration%') do (echo)",

                            // vBitRate
                            "\r\n\r\n" + "& for /F \"delims=\" %V in ('@" + FFprobe.ffprobe + " -v error -select_streams v:0 -show_entries " + FFprobe.vEntryTypeBatch + " -of default^=noprint_wrappers^=1:nokey^=1 \"%~f\" 2^>^&1') do (SET vBitRate=%V)",
                            // set %V to %vBitRate%
                            "\r\n\r\n" + "& for /F %V in ('echo %vBitRate%') do (echo)",
                            // auto bitrate calcuate
                            "\r\n\r\n" + "& (if %V EQU N/A (SET /a vBitRate=%S*8/1000/%D*1000) ELSE (echo Video Bit Rate Detected))",
                            // set %V to %vBitRate%
                            "\r\n\r\n" + "& for /F %V in ('echo %vBitRate%') do (echo)",
                        };
                    }

                    // -------------------------
                    // PowerShell
                    // -------------------------
                    else if (VM.ConfigureView.Shell_SelectedItem == "PowerShell")
                    {
                        batchVideoAutoList = new List<string>()
                        {
                            string.Empty
                        };
                    }

                    // -------------------------
                    // Join List with Spaces, Remove Empty Strings
                    // -------------------------
                    batchVideoAuto = string.Join(" ", batchVideoAutoList
                                                        .Where(s => !string.IsNullOrEmpty(s))
                                                        .Where(s => !string.IsNullOrWhiteSpace(s)));

                    }
                //}
            }

            // Return Value
            return batchVideoAuto;
        }



        /// <summary>
        /// Video BitRate Calculator (Method)
        /// <summary>
        public static String VideoBitRateCalculator(string container_SelectedItem,
                                                    string mediaType_SelectedItem,
                                                    string codec_SelectedItem,
                                                    string vEntryType, 
                                                    string inputVideoBitRate)
        {
            inputVideoBitRate = inputVideoBitRate.Trim();

            // -------------------------
            // Null Check
            // -------------------------
            if (!string.IsNullOrEmpty(inputVideoBitRate) &&
                !string.IsNullOrWhiteSpace(inputVideoBitRate))
            {
                // -------------------------
                // Remove K & M from input if any
                // -------------------------
                inputVideoBitRate = Regex.Replace(inputVideoBitRate, "k", "", RegexOptions.IgnoreCase);
                inputVideoBitRate = Regex.Replace(inputVideoBitRate, "m", "", RegexOptions.IgnoreCase);

                // -------------------------
                // Capture only "N/A" from FFprobe
                // -------------------------
                if (inputVideoBitRate.Length >= 3) // Out of Rang check
                {
                    if (inputVideoBitRate.Substring(0, 3) == "N/A")
                    {
                        inputVideoBitRate = "N/A";
                    }
                }

                // -------------------------
                // If Video Variable = N/A, Calculate Bitate (((Filesize*8)/1000)/Duration)
                // Formats like WebM, MKV and with Missing Metadata can have New BitRates calculated and applied
                // -------------------------
                if (inputVideoBitRate == "N/A")
                {
                    // Calculating BitRate will crash if jpg/png
                    try
                    {
                        // Convert to int to remove decimals
                        inputVideoBitRate = Convert.ToInt64((double.Parse(FFprobe.inputSize) * 8) / 1000 / double.Parse(FFprobe.inputDuration)).ToString();


                        // Log Console Message /////////
                        Log.WriteAction = () =>
                        {
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new Bold(new Run("Calculating New BitRate Information...")) { Foreground = Log.ConsoleAction });
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new Run("((File Size * 8) / 1000) / File Time Duration") { Foreground = Log.ConsoleDefault });
                        };
                        Log.LogActions.Add(Log.WriteAction);
                    }
                    catch
                    {
                        // Log Console Message /////////
                        Log.WriteAction = () =>
                        {
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new Bold(new Run("Error: Could Not Calculate New BitRate Information...")) { Foreground = Log.ConsoleError });
                        };
                        Log.LogActions.Add(Log.WriteAction);
                    }
                }

                // -------------------------
                // If Video has a BitRate (and is not N/A), calculate BitRate into decimal
                // -------------------------
                else if (inputVideoBitRate != "N/A")
                {
                    // Convert Input Video Bit Rate to Double
                    //double inputVideoBitRate_Double = Convert.ToDouble(inputVideoBitRate);

                    // Convert Input Video Bit Rate to Double
                    int inputVideoBitRate_Double = 8000000; // Fallback
                    int.TryParse(inputVideoBitRate, out inputVideoBitRate_Double);

                    // Calculate
                    // e.g. FFprobe 10000000 bytes -> 10000K (10M)
                    //      -b:v 10000K
                    inputVideoBitRate = Convert.ToString(inputVideoBitRate_Double * 0.001);

                    //// 1GB
                    //if (inputVideoBitRate_Double >= 1000000000)
                    //{
                    //    MessageBox.Show("2");
                    //    inputVideoBitRate = Convert.ToString(inputVideoBitRate_Double * 0.001); // 1,000M / 1,000,000K
                    //}
                    //// 100M
                    //else if (inputVideoBitRate_Double >= 100000000)
                    //{
                    //    MessageBox.Show("3");
                    //    inputVideoBitRate = Convert.ToString(inputVideoBitRate_Double * 0.001); // 100,000K
                    //}
                    //// 10M
                    //else if (inputVideoBitRate_Double >= 10000000)
                    //{
                    //    //MessageBox.Show("4" + inputVideoBitRate);
                    //    inputVideoBitRate = Convert.ToString(inputVideoBitRate_Double * 0.001); // 10,000K
                    //}
                    //// 1M / 1000000 bytes
                    //else if (inputVideoBitRate_Double >= 1000000)
                    //{
                    //    MessageBox.Show("5" + inputVideoBitRate);
                    //    inputVideoBitRate = Convert.ToString(inputVideoBitRate_Double * 0.001); // 10,000K
                    //}
                    //// 0.1M 100k / 100000 bytes
                    //else if (inputVideoBitRate_Double >= 100000)
                    //{
                    //    //MessageBox.Show("6 " + inputVideoBitRate);
                    //    inputVideoBitRate = Convert.ToString(inputVideoBitRate_Double * 0.001); // 100k
                    //}
                    //// 0.01M / 10k / 10000 bytes
                    //else if (inputVideoBitRate_Double >= 10000)
                    //{
                    //    MessageBox.Show("7");
                    //    inputVideoBitRate = Convert.ToString(inputVideoBitRate_Double * 0.001);
                    //}
                    //// 0.1M / 1k / 1000 bytes
                    //else if (inputVideoBitRate_Double >= 1000)
                    //{
                    //    MessageBox.Show("8");
                    //    inputVideoBitRate = Convert.ToString(inputVideoBitRate_Double * 0.001);
                    //}
                    //// 0.01M
                    //else if (inputVideoBitRate_Double >= 100)
                    //{
                    //    MessageBox.Show("9");
                    //    inputVideoBitRate = Convert.ToString(inputVideoBitRate_Double * 0.001); // 1k / 1,000b
                    //}
                    //// 0
                    //else
                    //{
                    //    MessageBox.Show("9");
                    //    inputVideoBitRate = Convert.ToString(inputVideoBitRate_Double * 0);
                    //}
                }


                // -------------------------
                // WebM Video BitRate Limiter
                // If input video bitrate is greater than 1.5M, lower the bitrate to 1.5M
                // -------------------------
                if (inputVideoBitRate != "N/A" &&
                    !string.IsNullOrEmpty(inputVideoBitRate) &&
                    !string.IsNullOrWhiteSpace(inputVideoBitRate) &&
                    container_SelectedItem == "webm" &&
                    codec_SelectedItem != "Copy"
                    )
                {
                    if (Convert.ToDouble(inputVideoBitRate) >= 1500)
                    {
                        inputVideoBitRate = "1500";
                    }
                }


                // -------------------------
                // Round BitRate, Remove Decimals
                // -------------------------
                try
                {
                    inputVideoBitRate = Math.Round(double.Parse(inputVideoBitRate)).ToString();
                }
                catch
                {
                    inputVideoBitRate = "2000";
                }

                // -------------------------
                // Add K to end of BitRate
                // -------------------------
                if (mediaType_SelectedItem != "Image" &&
                    mediaType_SelectedItem != "Sequence")
                {
                    inputVideoBitRate = inputVideoBitRate + "K";
                }
            }

            // -------------------------
            // Input Video BitRate does not exist
            // -------------------------
            else
            {
                if (string.IsNullOrEmpty(inputVideoBitRate) ||
                    string.IsNullOrWhiteSpace(inputVideoBitRate))
                {
                    // do nothing (dont remove, it will cause substring to overload)

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Input Video Bit Rate does not exist or can't be detected")) { Foreground = Log.ConsoleWarning });
                        Log.logParagraph.Inlines.Add(new LineBreak());
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }


            return inputVideoBitRate;
        }


        /// <summary>
        /// Pass (Method)
        /// <summary>
        public static String PassParams(string codec_SelectedItem,
                                        string pass_SelectedItem,
                                        string passNumber)
        {
            string pass = string.Empty;

            // -------------------------
            // Clear and Re-Generate All Params for Pass 2
            // -------------------------
            if (passNumber == "2")
            {
                if (VideoParams.vParamsList != null &&
                    VideoParams.vParamsList.Count > 0)
                {
                    VideoParams.vParamsList.Clear();
                    VideoParams.vParamsList.TrimExcess();
                }
            }

            // -------------------------
            // Enabled
            // -------------------------
            if (pass_SelectedItem == "2 Pass")
            {
                // Enable pass parameters in the FFmpeg Arguments
                // x265 Pass 2 Params
                if (codec_SelectedItem == "x265")
                {
                    //pass = string.Empty;
                    VideoParams.vParamsList.Add("pass=" + passNumber);
                }
                // All other codecs
                //else
                //{
                //    pass = string.Empty;
                //}
                //MessageBox.Show(string.Join("", VideoParams.vParamsList)); //debug
            }

            // -------------------------
            // Disabled
            // -------------------------
            //else if (pass_SelectedItem == "1 Pass" ||
            //         pass_SelectedItem == "CRF" ||
            //         pass_SelectedItem == "auto") // JPG, PNG, WebP
            //{
            //    pass = string.Empty;
            //}


            // Return Value
            return pass;
        }


        /// <summary>
        /// Pass 1 Modifier (Method)
        /// <summary>
        // x265 Pass 1
        public static String Pass1Modifier(string codec_SelectedItem,
                                           string pass_SelectedItem
                                           )
        {
            // -------------------------
            // Enabled
            // -------------------------
            if (pass_SelectedItem == "2 Pass")
            {
                // Enable pass parameters in the FFmpeg Arguments
                // x265 Pass 2 Params
                if (codec_SelectedItem == "x265")
                {
                    //pass1 = "-x265-params pass=1";
                    pass1 = string.Empty;
                    //VideoParams.vParamsList.Add("pass=1");
                }
                // All other codecs
                else
                {
                    pass1 = "-pass 1";
                }
            }

            // -------------------------
            // Disabled
            // -------------------------
            else if (pass_SelectedItem == "1 Pass" ||
                     pass_SelectedItem == "CRF" ||
                     pass_SelectedItem == "auto") // JPG, PNG, WebP
            {
                pass1 = string.Empty;
            }


            // Return Value
            return pass1;
        }


        /// <summary>
        /// Pass 2 Modifier (Method)
        /// <summary>
        // x265 Pass 2
        public static String Pass2Modifier(string codec_SelectedItem,
                                           string pass_SelectedItem
                                           )
        {
            // -------------------------
            // Enabled
            // -------------------------
            if (pass_SelectedItem == "2 Pass")
            {
                // Enable pass parameters in the FFmpeg Arguments
                // x265 Pass 2 Params
                if (codec_SelectedItem == "x265")
                {
                    //pass2 = "-x265-params pass=2";
                    pass1 = string.Empty;
                    //VideoParams.vParamsList.Add("pass=2");
                }
                // All other codecs
                else
                {
                    pass2 = "-pass 2";
                }
            }

            // -------------------------
            // Disabled
            // -------------------------
            else if (pass_SelectedItem == "1 Pass" ||
                     pass_SelectedItem == "CRF" ||
                     pass_SelectedItem == "auto") //jpg/png
            {
                pass2 = string.Empty;
            }


            // Return Value
            return pass2;
        }


        /// <summary>
        /// Frame Rate To Decimal
        /// <summary>
        /// <remarks>
        /// When using Video Frame Range instead of Time
        /// </remarks>
        public static String FramesToDecimal(string frame)
        {
            // Separate FFprobe Result (e.g. 30000/1001)
            string[] f = FFprobe.inputFrameRate.Split('/');

            try
            {
                double detectedFramerate = Convert.ToDouble(f[0]) / Convert.ToDouble(f[1]); // divide FFprobe values
                detectedFramerate = Math.Truncate(detectedFramerate * 1000) / 1000; // limit to 3 decimal places

                // Trim Start Frame
                //
                if (!string.IsNullOrEmpty(frame) &&
                    !string.IsNullOrWhiteSpace(frame)) // Default/Null Check
                {
                    // Divide Frame Start Number by Video's Framerate
                    frame = Convert.ToString(Convert.ToDouble(frame) / detectedFramerate); 
                }

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Frame: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(frame + " / " + detectedFramerate + " = " + Format.trimStart) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }
            catch
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Warning: No input file or Framerate not detected.")) { Foreground = Log.ConsoleWarning });
                };
                Log.LogActions.Add(Log.WriteAction);

                /* lock */
                //MainWindow.ready = false;
                // Warning
                MessageBox.Show("No input file or Framerate not detected.",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }


            return frame;
        }


        /// <summary>
        /// Color Primaries
        /// <summary>
        /// <remarks>
        /// https://ffmpeg.org/ffmpeg-filters.html#colorspace
        /// </remarks>
        public static String Color_Primaries(string primaries_SelectedItem)
        {
            //string colorPrimaries = string.Empty;

            if (primaries_SelectedItem != "auto")
            {
                switch (primaries_SelectedItem)
                {
                    case "BT.709":
                        colorPrimaries = "bt709";
                        break;

                    case "BT.470M":
                        colorPrimaries = "bt470m";
                        break;

                    case "BT.470BG":
                        colorPrimaries = "bt470bg";
                        break;

                    case "BT.601-6 525":
                        colorPrimaries = "smpte170m";
                        break;

                    case "BT.601-6 625":
                        colorPrimaries = "bt470bg";
                        break;

                    case "SMPTE-170M":
                        colorPrimaries = "smpte170m";
                        break;

                    case "SMPTE-240M":
                        colorPrimaries = "smpte240m";
                        break;

                    case "film":
                        colorPrimaries = "film";
                        break;

                    case "SMPTE-431":
                        colorPrimaries = "smpte431";
                        break;

                    case "SMPTE-432":
                        colorPrimaries = "smpte432";
                        break;

                    case "BT.2020":
                        colorPrimaries = "bt2020";
                        break;

                    case "JEDEC P22 phosphors":
                        colorPrimaries = "jedec-p22";
                        break;
                }

                colorPrimaries = "-color_primaries " + colorPrimaries;

                return colorPrimaries;
            }

            // Auto
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Color Transfer Characteristics
        /// <summary>
        /// <remarks>
        /// https://ffmpeg.org/ffmpeg-filters.html#colorspace
        /// </remarks>
        public static String Color_TransferCharacteristics(string transferChar_SelectedItem)
        {
            //string colorTransferCharacteristics = string.Empty;

            if (transferChar_SelectedItem != "auto")
            {
                switch (transferChar_SelectedItem)
                {
                    case "BT.709":
                        colorTransferCharacteristics = "bt709";
                        break;

                    case "BT.470M":
                        colorTransferCharacteristics = "bt470m";
                        break;

                    case "BT.470BG":
                        colorTransferCharacteristics = "bt470bg";
                        break;

                    case "Gamma 2.2":
                        colorTransferCharacteristics = "gamma22";
                        break;

                    case "Gamma 2.8":
                        colorTransferCharacteristics = "gamma28";
                        break;

                    case "BT.601-6 525":
                        colorTransferCharacteristics = "smpte170m";
                        break;

                    case "BT.601-6 625":
                        colorTransferCharacteristics = "smpte170m";
                        break;

                    case "SMPTE-170M":
                        colorTransferCharacteristics = "smpte170m";
                        break;

                    case "SMPTE-240M":
                        colorTransferCharacteristics = "smpte240m";
                        break;

                    case "SRGB":
                        colorTransferCharacteristics = "srgb";
                        break;

                    case "iec61966-2-1":
                        colorTransferCharacteristics = "iec61966-2-1";
                        break;

                    case "iec61966-2-4":
                        colorTransferCharacteristics = "iec61966-2-4";
                        break;

                    case "xvycc":
                        colorTransferCharacteristics = "xvycc";
                        break;

                    case "BT.2020 10-bit":
                        colorTransferCharacteristics = "bt2020-10";
                        break;

                    case "BT.2020 12-bit":
                        colorTransferCharacteristics = "bt2020-12";
                        break;
                }

                colorTransferCharacteristics = "-color_trc " + colorTransferCharacteristics;

                return colorTransferCharacteristics;
            }

            // Auto
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Color Space
        /// <summary>
        /// <remarks>
        /// https://ffmpeg.org/ffmpeg-filters.html#colorspace
        /// </remarks>
        public static String Color_Space(string colorspace_SelectedItem)
        {
            //string colorSpace = string.Empty;

            if (colorspace_SelectedItem != "auto")
            {
                switch (colorspace_SelectedItem)
                {
                    case "BT.709":
                        colorSpace = "bt709";
                        break;

                    case "FCC":
                        colorSpace = "fcc";
                        break;

                    case "BT.470BG":
                        colorSpace = "bt470bg";
                        break;

                    case "BT.601-6 525":
                        colorSpace = "smpte170m";
                        break;

                    case "BT.601-6 625":
                        colorSpace = "bt470bg";
                        break;

                    case "SMPTE-170M":
                        colorSpace = "smpte170m";
                        break;

                    case "SMPTE-240M":
                        colorSpace = "smpte240m";
                        break;

                    case "YCgCo":
                        colorSpace = "ycgco";
                        break;

                    case "BT.2020 NCL":
                        colorSpace = "bt2020ncl";
                        break;
                }

                colorSpace = "-colorspace " + colorSpace;

                return colorSpace;
            }

            // Auto
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Color Range
        /// <summary>
        /// <remarks>
        /// https://ffmpeg.org/ffmpeg-filters.html#colorspace
        /// </remarks>
        public static String Color_Range(string colorRange_SelectedItem)
        {
            //string colorRange = string.Empty;

            if (colorRange_SelectedItem != "auto")
            {
                switch (colorRange_SelectedItem)
                {
                    case "TV":
                        colorRange = "tv";
                        break;

                    case "PC":
                        colorRange = "pc";
                        break;

                    case "MPEG":
                        colorRange = "mpeg";
                        break;

                    case "JPEG":
                        colorRange = "jpeg";
                        break;
                }

                colorRange = "-color_range " + colorRange;

                return colorRange;
            }

            // Auto
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Color Matrix
        /// <summary>
        /// <remarks>
        /// https://ffmpeg.org/ffmpeg-filters.html#colorspace
        /// </remarks>
        //public static String Color_Matrix(string colormatrix_SelectedItem)
        //{
        //    //string colorMatrix = string.Empty;

        //    if (colormatrix_SelectedItem != "auto")
        //    {
        //        switch (colormatrix_SelectedItem)
        //        {
        //            case "BT.709":
        //                colorMatrix = "bt709";
        //                break;

        //            case "FCC":
        //                colorMatrix = "fcc";
        //                break;

        //            case "BT.601":
        //                colorMatrix = "bt601";
        //                break;

        //            case "BT.470":
        //                colorMatrix = "bt470";
        //                break;

        //            case "BT.470BG":
        //                colorMatrix = "bt470bg";
        //                break;

        //            case "SMPTE-170M":
        //                colorMatrix = "smpte170m";
        //                break;

        //            case "SMPTE-240M":
        //                colorMatrix = "smpte240m";
        //                break;

        //            case "BT.2020":
        //                colorMatrix = "bt2020";
        //                break;
        //        }

        //        colorMatrix = "-colormatrix " + colorMatrix;

        //        return colorMatrix;
        //    }

        //    // Auto
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}


        /// <summary>
        /// Pixel Format
        /// <summary>
        public static String PixFmt(string codec_SelectedItem,
                                    string pixelFormat_SelectedItem
                                    )
        {
            // Check:
            // Video Codec Not Copy
            if (codec_SelectedItem != "Copy")
            {
                // If Auto, Use Empty
                // If Not Auto, use Selected Item
                if (pixelFormat_SelectedItem != "auto" &&
                    pixelFormat_SelectedItem != "none")
                {
                    pix_fmt = "-pix_fmt " + pixelFormat_SelectedItem;
                }
            }

            return pix_fmt;
        }



        /// <summary>
        /// Optimize
        /// <summary>
        public static String Optimize(string codec_SelectedItem,
                                      List<VideoViewModel.VideoOptimize> optimize_Items,
                                      string optimize_SelectedItem,
                                      string tune_SelectedItem,
                                      string profile_SelectedItem,
                                      string level_SelectedItem
                                      )
        {
            // Check:
            // Video Codec Not Copy
            if (codec_SelectedItem != "Copy")
            {
                //MessageBox.Show(tune_SelectedItem); //debug
                //MessageBox.Show(profile_SelectedItem); //debug
                //MessageBox.Show(level_SelectedItem); //debug

                // -------------------------
                // Tune
                // -------------------------
                if (!string.IsNullOrEmpty(tune_SelectedItem) &&
                    !string.IsNullOrWhiteSpace(tune_SelectedItem) &&
                    tune_SelectedItem != "none")
                {
                    optTune = "-tune " + tune_SelectedItem;
                }

                // -------------------------
                // Profile
                // -------------------------
                if (!string.IsNullOrEmpty(profile_SelectedItem) &&
                    !string.IsNullOrWhiteSpace(profile_SelectedItem) &&
                    profile_SelectedItem != "none")
                {
                    optProfile = "-profile:v " + profile_SelectedItem;
                }

                // -------------------------
                // Level
                // -------------------------
                if (!string.IsNullOrEmpty(level_SelectedItem) &&
                    !string.IsNullOrWhiteSpace(level_SelectedItem) &&
                    level_SelectedItem != "none")
                {
                    optLevel = "-level " + level_SelectedItem;
                }

                // -------------------------
                // Additional Options
                // -------------------------
                optFlags = optimize_Items.FirstOrDefault(item => item.Name == optimize_SelectedItem)?.Command;

                // -------------------------
                // Combine Optimize = Tune + Profile + Level
                // -------------------------
                List<string> v2passList = new List<string>() {
                    optTune,
                    optProfile,
                    optLevel,
                    optFlags
                };

                optimize = string.Join(" ", v2passList
                                            .Where(s => !string.IsNullOrEmpty(s))
                                            .Where(s => !string.IsNullOrWhiteSpace(s))
                                            );
            }

            // Return Value
            return optimize;
        }



        /// <summary>
        /// FPS
        /// <summary>
        public static String FPS(string codec_SelectedItem,
                                 string fps_SelectedItem,
                                 string fps_Text
                                 )
        {
            // Check:
            // Video Codec Not Copy
            // FPS Not Empty
            if (codec_SelectedItem != "Copy" &&
                !string.IsNullOrEmpty(fps_Text) &&
                !string.IsNullOrWhiteSpace(fps_Text)
                )
            {
                //fps = string.Empty;

                switch (fps_SelectedItem)
                {
                    case "auto":
                        fps = string.Empty;
                        break;
                    case "film":
                        fps = "-r film";
                        break;
                    case "pal":
                        fps = "-r pal";
                        break;
                    case "ntsc":
                        fps = "-r ntsc";
                        break;
                    case "23.976":
                        fps = "-r 24000/1001";
                        break;
                    case "24":
                        fps = "-r 24";
                        break;
                    case "25":
                        fps = "-r 25";
                        break;
                    case "29.97":
                        fps = "-r 30000/1001";
                        break;
                    case "30":
                        fps = "-r 30";
                        break;
                    case "48":
                        fps = "-r 48";
                        break;
                    case "50":
                        fps = "-r 50";
                        break;
                    case "59.94":
                        fps = "-r 60000/1001";
                        break;
                    case "60":
                        fps = "-r 60";
                        break;

                    default:
                        try
                        {
                            fps = "-r " + fps_Text;
                            break;
                        }
                        catch
                        {
                            /* lock */
                            //MainWindow.ready = false;
                            // Warning
                            MessageBox.Show("Invalid Custom FPS.",
                            "Notice",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        }
                        break;
                }

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("FPS: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(fps_Text) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            return fps;
        }



        /// <summary>
        /// Video Speed (Method)
        /// <summary>
        public static void Speed(//string mediaType_SelectedItem,
                                 string codec_SelectedItem,
                                 //string quality_SelectedItem,
                                 string speed_SelectedItem,
                                 string speed_Text
                                 )
        {
            // Media Type Audio
            // Video Codec None
            // Video Codec Copy
            // Video BitRate None
            // Speed Auto/Null
            if (codec_SelectedItem != "Copy" &&
                speed_SelectedItem != "auto" &&
                !string.IsNullOrEmpty(speed_Text) &&
                !string.IsNullOrWhiteSpace(speed_Text)
                )
            {
                // Slow Down 50% -vf "setpts=2.0*PTS"
                // Speed Up 200% -vf "setpts=0.5*PTS"

                // Convert to setpts:
                // 50%: (100 / (50 * 0.01)) * 0.01) = 200
                // 2.0: 200 * 0.01 = 2.0
                try
                {
                    double val = Convert.ToDouble(speed_Text.Replace("%", "").Trim());
                    val = (100 / (val * 0.01)) * 0.01;

                    string speed = "setpts=" + val.ToString("#.#####") + "*PTS";

                    VideoFilters.vFiltersList.Add(speed);
                }
                catch
                {

                }
            }
        }



        /// <summary>
        /// Size Width Auto
        /// <summary>
        public static String SizeWidthAuto(string codec_SelectedItem)
        {
            if (codec_SelectedItem == "VP8" ||
                codec_SelectedItem == "VP9" ||
                codec_SelectedItem == "AV1" ||
                codec_SelectedItem == "FFV1" ||
                codec_SelectedItem == "HuffYUV" ||
                codec_SelectedItem == "Theora" ||
                codec_SelectedItem == "JPEG" ||
                codec_SelectedItem == "PNG" ||
                codec_SelectedItem == "WebP")
            {
                return "-1";
            }
            else if (codec_SelectedItem == "x264" ||
                     codec_SelectedItem == "x265" ||
                     codec_SelectedItem == "MPEG-2" ||
                     codec_SelectedItem == "MPEG-4")
            {
                return "-2";
            }

            return "-1";
        }


        /// <summary>
        /// Size Height Auto
        /// <summary>
        public static String SizeHeightAuto(string codec_SelectedItem)
        {
            if (codec_SelectedItem == "VP8" ||
                codec_SelectedItem == "VP9" ||
                codec_SelectedItem == "AV1" ||
                codec_SelectedItem == "FFV1" ||
                codec_SelectedItem == "HuffYUV" ||
                codec_SelectedItem == "Theora" ||
                codec_SelectedItem == "JPEG" ||
                codec_SelectedItem == "PNG" ||
                codec_SelectedItem == "WebP")
            {
                return "-1";
            }
            else if (codec_SelectedItem == "x264" ||
                     codec_SelectedItem == "x265" ||
                     codec_SelectedItem == "MPEG-2" ||
                     codec_SelectedItem == "MPEG-4")
            {
                return "-2";
            }

            return "-1";
        }


        /// <summary>
        /// Size
        /// <summary>
        // Size is a Filter
        public static void Scale(string codec_SelectedItem,
                                 string size_SelectedItem,
                                 string width_Text,
                                 string height_Text,
                                 string screenFormat_SelectedItem,
                                 //string aspectRatio_SelectedItem,
                                 string scalingAlgorithm_SelectedItem,
                                 string cropClear_Text
                                 )
        {
            // -------------------------
            // No
            // -------------------------
            if (size_SelectedItem == "Source")
            {
                // MP4/MKV Width/Height Fix
                if (codec_SelectedItem == "x264" ||
                    codec_SelectedItem == "x265" ||
                    codec_SelectedItem == "MPEG-2" ||
                    codec_SelectedItem == "MPEG-4")
                {
                    width = "trunc(iw/2)*2";
                    height = "trunc(ih/2)*2";

                    // -------------------------
                    // Combine & Add Scaling Algorithm
                    // -------------------------
                    scale = "scale=" + width + ":" + height + ScalingAlgorithm(scalingAlgorithm_SelectedItem);

                    // Video Filter Add
                    VideoFilters.vFiltersList.Add(scale);
                }
            }

            // -------------------------
            // Yes
            // -------------------------
            else
            {
                // -------------------------
                // Preset Scale
                // -------------------------
                if (size_SelectedItem != "Custom")
                {
                    // Widescreen
                    //if (MainWindow.IsAspectRatioWidescreen(aspectRatio_SelectedItem) == true)
                    if (screenFormat_SelectedItem == "auto" ||
                        screenFormat_SelectedItem == "Widescreen" ||
                        screenFormat_SelectedItem == "Ultrawide"
                    )
                    {
                        width = width_Text;
                        height = SizeHeightAuto(codec_SelectedItem);
                    }

                    // Full Screen
                    else if (screenFormat_SelectedItem == "Full Screen")
                    {
                        width = SizeWidthAuto(codec_SelectedItem);
                        height = height_Text;
                    }

                    // -------------------------
                    // Combine & Add Scaling Algorithm
                    // -------------------------
                    scale = "scale=" + width + ":" + height + ScalingAlgorithm(scalingAlgorithm_SelectedItem);

                    // Video Filter Add
                    VideoFilters.vFiltersList.Add(scale);
                }

                // -------------------------
                // Custom Size
                // -------------------------
                else if (size_SelectedItem == "Custom")
                {
                    //MainWindow mainwindow = (MainWindow)System.Windows.Application.Current.MainWindow;
                    //MainView vm = mainwindow.DataContext as MainView;

                    // Get width height from custom textbox
                    width = width_Text;
                    height = height_Text;

                    // Change the left over Default empty text to "auto"
                    if (string.Equals(width_Text, "", StringComparison.CurrentCultureIgnoreCase))
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                    }

                    if (string.Equals(height_Text, "", StringComparison.CurrentCultureIgnoreCase))
                    {
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // -------------------------
                    // VP8, VP9, Theora
                    // -------------------------
                    if (codec_SelectedItem == "VP8" ||
                        codec_SelectedItem == "VP9" ||
                        codec_SelectedItem == "Theora" ||
                        codec_SelectedItem == "JPEG" ||
                        codec_SelectedItem == "PNG" ||
                        codec_SelectedItem == "WebP")
                    {
                        // If User enters "auto" or textbox is empty
                        if (string.Equals(width_Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                        {
                            width = "-1";
                        }
                        if (string.Equals(height_Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                        {
                            height = "-1";
                        }

                        // -------------------------
                        // Combine & Add Scaling Algorithm
                        // -------------------------
                        scale = "scale=" + width + ":" + height + ScalingAlgorithm(scalingAlgorithm_SelectedItem);

                        // Video Filter Add
                        VideoFilters.vFiltersList.Add(scale);
                    }


                    // -------------------------
                    // x264 & x265
                    // -------------------------
                    // Fix FFmpeg MP4 but (User entered value)
                    // Apply Fix to all scale effects above
                    //
                    else if (codec_SelectedItem == "x264" ||
                             codec_SelectedItem == "x265" ||
                             codec_SelectedItem == "MPEG-2" ||
                             codec_SelectedItem == "MPEG-4")
                    {
                        // -------------------------
                        // Width = Custom value
                        // Height = Custom value
                        // -------------------------
                        if (!string.Equals(width_Text, "auto", StringComparison.CurrentCultureIgnoreCase) &&
                            !string.Equals(height_Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                        {
                            // -------------------------
                            // Aspect Must be Cropped to be divisible by 2
                            // e.g. -vf "scale=777:777, crop=776:776:0:0"
                            // -------------------------
                            try
                            {
                                // Convert width and height from string to int
                                int width_int;
                                int.TryParse(width, out width_int);
                                int height_int;
                                int.TryParse(height, out height_int);

                                // Set divible crop placeholders
                                int divisibleWidthCrop = width_int;
                                int divisibleHeightCrop = height_int;

                                // Set Crop Position to default top left
                                string cropX = "0";
                                string cropY = "0";

                                // Width
                                if (width_int % 2 != 0)
                                {
                                    divisibleWidthCrop = width_int - 1;
                                }
                                // Height
                                if (height_int % 2 != 0)
                                {
                                    divisibleHeightCrop = height_int - 1;
                                }


                                // -------------------------
                                // Only if Crop is already Empty
                                // -------------------------
                                // User Defined Crop should always override Divisible Crop
                                // CropClear Button symbol * is used as an Identifier, Divisible Crop does not leave "Clear*"
                                //
                                if (cropClear_Text == "Clear") // Crop Set Check
                                {
                                    // -------------------------
                                    // Combine & Add Scaling Algorithm
                                    // -------------------------
                                    scale = "scale=" + width + ":" + height + ScalingAlgorithm(scalingAlgorithm_SelectedItem);
                                    VideoFilters.vFiltersList.Add(scale);

                                    // Divisible Crop - crop off the extra pixels
                                    if (width_int % 2 != 0 || height_int % 2 != 0)
                                    {
                                        CropWindow.crop = Convert.ToString("crop=" + divisibleWidthCrop + ":" + divisibleHeightCrop + ":" + cropX + ":" + cropY);
                                        VideoFilters.vFiltersList.Add(CropWindow.crop);
                                    }
                                }

                                // -------------------------
                                // If Crop has manually been set
                                // -------------------------
                                else if (cropClear_Text == "Clear*")
                                {
                                    // -------------------------
                                    // Combine & Add Scaling Algorithm
                                    // -------------------------
                                    // Manual Crop - Crop what you want out of the video
                                    VideoFilters.vFiltersList.Add(CropWindow.crop);

                                    // Scale - Resize video
                                    scale = "scale=" + width + ":" + height + ScalingAlgorithm(scalingAlgorithm_SelectedItem);
                                    VideoFilters.vFiltersList.Add(scale);

                                    // Divisible Crop - crop off the extra pixels
                                    if (width_int % 2 != 0 || height_int % 2 != 0)
                                    {
                                        string divisibleCrop = Convert.ToString("crop=" + divisibleWidthCrop + ":" + divisibleHeightCrop + ":" + cropX + ":" + cropY);
                                        VideoFilters.vFiltersList.Add(divisibleCrop);
                                    }
                                }
                            }
                            catch
                            {
                                // Log Console Message /////////
                                Log.WriteAction = () =>
                                {
                                    Log.logParagraph.Inlines.Add(new LineBreak());
                                    Log.logParagraph.Inlines.Add(new LineBreak());
                                    Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Must enter numbers only.")) { Foreground = Log.ConsoleWarning });
                                };
                                Log.LogActions.Add(Log.WriteAction);

                                /* lock */
                                //MainWindow.ready = false;
                                // Warning
                                MessageBox.Show("Must enter numbers only.",
                                                "Notice",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Exclamation);
                            }

                        }

                        // -------------------------
                        // Width = auto
                        // Height = Custom value
                        // -------------------------
                        else if (string.Equals(width_Text, "auto", StringComparison.CurrentCultureIgnoreCase) &&
                                !string.Equals(height_Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                        {
                            //Width
                            width = "-2";

                            // Height
                            // Make user entered height divisible by 2
                            try
                            {
                                // Convert Height TextBox Value to Int
                                int divisibleHeight = Convert.ToInt32(height);

                                // int convert check
                                if (int.TryParse(height, out divisibleHeight))
                                {
                                    // If not divisible by 2, subtract 1 from total
                                    if (divisibleHeight % 2 != 0)
                                    {
                                        divisibleHeight -= 1;
                                        height = Convert.ToString(divisibleHeight);
                                    }
                                }
                            }
                            catch
                            {
                                // Log Console Message /////////
                                Log.WriteAction = () =>
                                {
                                    Log.logParagraph.Inlines.Add(new LineBreak());
                                    Log.logParagraph.Inlines.Add(new LineBreak());
                                    Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Must enter numbers only.")) { Foreground = Log.ConsoleWarning });
                                };
                                Log.LogActions.Add(Log.WriteAction);

                                /* lock */
                                //MainWindow.ready = false;
                                // Warning
                                MessageBox.Show("Must enter numbers only.",
                                                "Notice",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Exclamation);
                            }
                        }

                        // -------------------------
                        // Width = Custom value
                        // Height = auto
                        // -------------------------
                        else if (!string.Equals(width_Text, "auto", StringComparison.CurrentCultureIgnoreCase) &&
                                 string.Equals(height_Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                        {
                            // Height
                            height = "-2";

                            // Width
                            // Make user entered Width divisible by 2
                            try
                            {
                                // Convert Height TextBox Value to Int
                                int divisibleWidth = Convert.ToInt32(width);

                                // int convert check
                                if (int.TryParse(width, out divisibleWidth))
                                {
                                    // If not divisible by 2, subtract 1 from total
                                    if (divisibleWidth % 2 != 0)
                                    {
                                        divisibleWidth -= 1;
                                        width = Convert.ToString(divisibleWidth);
                                    }
                                }
                            }
                            catch
                            {
                                // Log Console Message /////////
                                Log.WriteAction = () =>
                                {
                                    Log.logParagraph.Inlines.Add(new LineBreak());
                                    Log.logParagraph.Inlines.Add(new LineBreak());
                                    Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Must enter numbers only.")) { Foreground = Log.ConsoleWarning });
                                };
                                Log.LogActions.Add(Log.WriteAction);

                                /* lock */
                                //MainWindow.ready = false;
                                // Warning
                                MessageBox.Show("Must enter numbers only.",
                                                "Notice",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Exclamation);
                            }
                        }

                        // -------------------------
                        // Width = auto
                        // Height = auto
                        // -------------------------
                        else if (string.Equals(width_Text, "auto", StringComparison.CurrentCultureIgnoreCase) &&
                                 string.Equals(height_Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                        {
                            // If User enters "auto" or textbox is empty
                            if (string.Equals(width_Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                            {
                                width = "trunc(iw/2)*2";

                            }
                            if (string.Equals(height_Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                            {
                                height = "trunc(ih/2)*2";
                            }
                        }

                    } //end codec check

                } //end custom


                //// -------------------------
                //// Combine & Add Scaling Algorithm
                //// -------------------------
                //scale = "scale=" + width + ":" + height + ScalingAlgorithm(scalingAlgorithm_SelectedItem);

                //// Video Filter Add
                //VideoFilters.vFiltersList.Add(scale);

            } //end Yes


            // -------------------------
            // Filter Clear
            // -------------------------
            // Copy
            if (codec_SelectedItem == "Copy")
            {
                scale = string.Empty;

                // Video Filter Add
                if (VideoFilters.vFiltersList != null)
                {
                    VideoFilters.vFiltersList.Clear();
                    VideoFilters.vFiltersList.TrimExcess();
                }
            }


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Resize: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(size_SelectedItem.ToString()) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Width: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(width) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Height: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(height) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

        } //End Size



        /// <summary>
        /// Aspect Ratio
        /// <summary>
        public static String AspectRatio(string aspectRatio_SelectedItem)
        {
            // Note: Can be used with Video Codec Copy

            // None & Default
            //
            if (aspectRatio_SelectedItem == "auto")
            {
                vAspectRatio = string.Empty;
            }

            // 2.4:1
            else if (aspectRatio_SelectedItem == "2.4:1")
            {
                vAspectRatio = "-aspect " + "240:100";
            }

            // 4:3, 16:9, etc.
            //
            else
            {
                vAspectRatio = "-aspect " + aspectRatio_SelectedItem;
            }

            return vAspectRatio;
        }



        /// <summary>
        /// Scaling Algorithm
        /// <summary>
        public static String ScalingAlgorithm(string scalingAlgorithm_SelectedItem)
        {
            // -------------------------
            // None & Default
            // -------------------------
            if (scalingAlgorithm_SelectedItem == "auto")
            {
                vScalingAlgorithm = string.Empty;
            }

            // -------------------------
            // Scaler
            // -------------------------
            else
            {
                vScalingAlgorithm = ":flags=" + scalingAlgorithm_SelectedItem;
            }

            return vScalingAlgorithm;
        }



        /// <summary>
        /// Crop (Method)
        /// <summary>
        public static void Crop(CropWindow cropwindow)
        {
            // -------------------------
            // Clear
            // -------------------------
            // Clear leftover Divisible Crop if not x264/x265
            // CropClearButton is used as an Identifier, Divisible Crop does not leave "Clear*"
            if (VM.VideoView.Video_Codec_SelectedItem != "x264" &&
                VM.VideoView.Video_Codec_SelectedItem != "x265" &&
                VM.VideoView.Video_Codec_SelectedItem != "MPEG-2" &&
                VM.VideoView.Video_Codec_SelectedItem != "MPEG-4" &&
                VM.VideoView.Video_CropClear_Text == "Clear"
                )
            {
                CropWindow.crop = string.Empty;
            }

            // Clear Crop if MediaTypeControls is Audio
            if (VM.FormatView.Format_MediaType_SelectedItem == "Audio")
            {
                CropWindow.crop = string.Empty;
            }

            // -------------------------
            // Add Crop to Video Filters if Not Null
            // -------------------------
            // If Crop is set by User in the CropWindow
            if (!string.IsNullOrEmpty(CropWindow.crop) &&
                !string.IsNullOrWhiteSpace(CropWindow.crop))
            {
                // Video Filters Add
                VideoFilters.vFiltersList.Add(CropWindow.crop);
            }
        }


        /// <summary>
        /// Images
        /// <summary>
        public static String Images(string mediaType_SelectedItem,
                                    string codec_SelectedItem
                                    )
        {
            // Check:
            // Video Codec Not Copy
            if (codec_SelectedItem != "Copy")
            {
                switch (mediaType_SelectedItem)
                {
                    // -------------------------
                    // Image
                    // -------------------------
                    case "Image":
                        image = "-vframes 1"; //important
                        break;

                    // -------------------------
                    // Sequence
                    // -------------------------
                    case "Sequence":
                        image = string.Empty; //disable -vframes
                        break;

                    // -------------------------
                    // All Other Media Types
                    // -------------------------
                    default:
                        image = string.Empty;
                        break;
                }

                //// -------------------------
                //// Image
                //// -------------------------
                //if (mediaType_SelectedItem == "Image")
                //{
                //    image = "-vframes 1"; //important
                //}

                //// -------------------------
                //// Sequence
                //// -------------------------
                //else if (mediaType_SelectedItem == "Sequence")
                //{
                //    image = string.Empty; //disable -vframes
                //}

                //// -------------------------
                //// All Other Media Types
                //// -------------------------
                //else
                //{
                //    image = string.Empty;
                //}
            }

            return image;
        }



    }
}
