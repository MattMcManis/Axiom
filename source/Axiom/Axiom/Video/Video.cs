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

 * Hardware Acceleration
 * Video Codec
 * Speed
 * Bitrate Mode
 * Video Quality
 * Batch Video Quality Auto
 * Video Bitrate Calculator
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
        ///     Global Variables
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // Video
        public static string vEncodeSpeed;
        public static string vCodec; // Video Codec
        public static string vQuality; // Video Quality
        public static string vBitMode;
        public static string vLossless;
        public static string vBitrate; // Video Bitrate
        public static string vBitrateNA; // N/A e.g. Define 3000K
        public static string vMinrate;
        public static string vMaxrate;
        public static string vBufsize;
        public static string vOptions; // -pix_fmt, -qcomp
        public static string vCRF; // Constant Rate Factor
        public static string pix_fmt;
        public static string vAspectRatio;
        public static string vScalingAlgorithm;
        public static string fps; // Frames Per Second
        public static string image; // JPEG & PNG options
        public static string optTune; // x264/x265 tuning modes
        public static string optProfile; // x264/x265 Profile
        public static string optLevel; // x264/x265 Level
        public static string optFlags; // Additional Optimization Flags
        public static string optimize; // Contains opTune + optProfile + optLevel

        // x265 Params
        public static List<string> x265paramsList = new List<string>(); // multiple parameters
        public static string x265params; // combined inline list

        // Scale
        public static string width;
        public static string height;
        public static string scale; // contains scale, width, height

        // Pass
        public static string v2PassArgs; // 2-Pass Arguments
        public static string passSingle; // 1-Pass & CRF Args
        public static string pass1Args; // Batch 2-Pass (Pass 1)
        public static string pass2Args; // Batch 2-Pass (Pass 2)
        public static string pass1; // x265 Modifier
        public static string pass2; // x265 Modifier

        // Batch
        public static string batchVideoAuto;

        // Rendering
        public static string hwacceleration;



        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Methods
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------

        /// <summary>
        ///     Hardware Acceleration
        /// <summary>
        /// https://trac.ffmpeg.org/wiki/HWAccelIntro
        public static String HWAcceleration(string mediaType_SelectedItem,
                                            string codec_SelectedItem,
                                            string hwaccel_SelectedItem
                                            )
        {
            // Check:
            // Media Type Video, Image, Sequence
            if (mediaType_SelectedItem == "Video" ||
                mediaType_SelectedItem == "Image" ||
                mediaType_SelectedItem == "Sequence")
            {
                // --------------------------------------------------
                // All Codecs
                // --------------------------------------------------
                // -------------------------
                // Off
                // -------------------------
                if (hwaccel_SelectedItem == "off")
                {
                    hwacceleration = string.Empty;
                }
                // -------------------------
                // DXVA2
                // -------------------------
                else if (hwaccel_SelectedItem == "dxva2")
                {
                    // ffmpeg -hwaccel dxva2 -threads 1 -i INPUT -f null
                    hwacceleration = "-hwaccel dxva2";
                }

                // --------------------------------------------------
                // Only x264/x265
                // --------------------------------------------------
                if (codec_SelectedItem == "x264" ||
                    codec_SelectedItem == "x265")
                {
                    // -------------------------
                    // CUVID
                    // -------------------------
                    if (hwaccel_SelectedItem == "cuvid")
                    {
                        // ffmpeg -c:v h264_cuvid -i input output.mkv

                        // Override Codecs
                        if (codec_SelectedItem == "x264")
                        {
                            vCodec = "-c:v h264_cuvid";
                        }
                        else if (codec_SelectedItem == "x264")
                        {
                            vCodec = "-c:v h265_cuvid";
                        }

                        hwacceleration = string.Empty;
                    }
                    // -------------------------
                    // NVENC
                    // -------------------------
                    else if (hwaccel_SelectedItem == "nvenc")
                    {
                        // ffmpeg -i input -c:v h264_nvenc -profile high444p -pix_fmt yuv444p -preset default output.mp4

                        // Override Codecs
                        if (codec_SelectedItem == "x264")
                        {
                            vCodec = "-c:v h264_nvenc";
                        }
                        else if (codec_SelectedItem == "x264")
                        {
                            vCodec = "-c:v h265_nvenc";
                        }

                        hwacceleration = string.Empty;
                    }
                    // -------------------------
                    // CUVID + NVENC
                    // -------------------------
                    else if (hwaccel_SelectedItem == "cuvid+nvenc")
                    {
                        // ffmpeg -hwaccel cuvid -c:v h264_cuvid -i input -c:v h264_nvenc -preset slow output.mkv
                        if (codec_SelectedItem == "x264")
                        {
                            hwacceleration = "-hwaccel cuvid -c:v h264_cuvid";
                        }
                        else if (codec_SelectedItem == "x265")
                        {
                            hwacceleration = "-hwaccel cuvid -c:v hevc_cuvid";
                        }
                    }
                }
            }

            return hwacceleration;
        }


        /// <summary>
        ///     Hardware Acceleration Codec Override
        /// <summary>
        /// https://trac.ffmpeg.org/wiki/HWAccelIntro
        public static String HWAccelerationCodecOverride(string hwaccel_SelectedItem, 
                                                         string codec_SelectedItem
                                                         )
        {
            // -------------------------
            // Cuvid
            // -------------------------
            if (hwaccel_SelectedItem == "cuvid")
            {
                // Only x264/ x265
                // e.g. ffmpeg -c:v h264_cuvid -i input output.mkv

                // Override Codecs
                if (codec_SelectedItem == "x264")
                {
                    vCodec = "-c:v h264_cuvid";
                }
                else if (codec_SelectedItem == "x264")
                {
                    vCodec = "-c:v h265_cuvid";
                }
            }
            // -------------------------
            // NVENC
            // -------------------------
            else if (hwaccel_SelectedItem == "nvenc")
            {
                // Only x264/ x265
                // e.g. ffmpeg -i input -c:v h264_nvenc -profile high444p -pix_fmt yuv444p -preset default output.mp4

                // Override Codecs
                if (codec_SelectedItem == "x264")
                {
                    vCodec = "-c:v h264_nvenc";
                }
                else if (codec_SelectedItem == "x264")
                {
                    vCodec = "-c:v h265_nvenc";
                }
            }

            return vCodec;
        }


        /// <summary>
        ///     Video Codec
        /// <summary>
        public static String VideoCodec(string hwAccel_SelectedItem, 
                                        string codec_SelectedItem, 
                                        string codec_Command
                                        )
        {
            // Passed Command
            if (codec_SelectedItem != "None")
            {
                vCodec = codec_Command;

                // HW Acceleration Override
                if (hwAccel_SelectedItem == "cuvid" ||
                    hwAccel_SelectedItem == "nvenc"
                    )
                {
                    vCodec = HWAccelerationCodecOverride(hwAccel_SelectedItem,
                                                         codec_SelectedItem
                                                         );

                    //MessageBox.Show(vCodec); //debug
                }
            }              

            return vCodec;
        }


        /// <summary>
        ///    Encode Speed
        /// <summary>
        public static String VideoEncodeSpeed(List<ViewModel.VideoEncodeSpeed> encodeSpeedItems,
                                              string encodeSpeed_SelectedItem,
                                              string mediaType_SelectedItem,
                                              string codec_SelectedItem,
                                              string quality_SelectedItem,
                                              string pass
                                              )
        {
            // Check:
            // Media Type Audio
            // Video Codec None
            // Video Codec Copy
            // Video Quality None
            if (mediaType_SelectedItem != "Audio" &&
                codec_SelectedItem != "None" &&
                codec_SelectedItem != "Copy" &&
                quality_SelectedItem != "None"
                )
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
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Encoding EncodeSpeed: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(encodeSpeed_SelectedItem) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }


            // Return Value
            return vEncodeSpeed;
        }



        /// <summary>
        ///     Bitrate Mode
        /// <summary>
        public static String BitrateMode(List<ViewModel.VideoQuality> quality_Items,
                                         string quality_SelectedItem,
                                         string bitrate_Text,
                                         bool vbr_IsChecked)
        {
            //MessageBox.Show(vbr_IsChecked.ToString()); //debug

            // Only if Bitrate Textbox is not Empty (except for Auto Quality)
            if (quality_SelectedItem == "Auto" || 
                !string.IsNullOrEmpty(bitrate_Text))
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
        ///     Video Quality - Auto
        /// <summary>
        public static void QualityAuto(bool batch_IsChecked,
                                       bool vbr_IsChecked,
                                       string container_SelectedItem,
                                       string mediaType_SelectedItem,
                                       string codec_SelectedItem,
                                       List<ViewModel.VideoQuality> quality_Items,
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
            // Bitrate
            // Video
            if (mediaType_SelectedItem == "Video")
            {
                vBitrate = VideoBitrateCalculator(container_SelectedItem,
                                                    mediaType_SelectedItem,
                                                    codec_SelectedItem,
                                                    FFprobe.vEntryType,
                                                    FFprobe.inputVideoBitrate);
            }
            // Images
            else if (mediaType_SelectedItem == "Image" ||
                        mediaType_SelectedItem == "Sequence"
                    )
            {
                vBitrate = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.VBR;
            }



            // Bitrate NA
            vBitrateNA = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.NA;

            // Minrate
            if (!string.IsNullOrEmpty(minrate_Text))
            {
                vMinrate = "-minrate " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.Minrate;
            }

            // Maxrate
            if (!string.IsNullOrEmpty(maxrate_Text))
            {
                vMaxrate = "-maxrate " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.Maxrate;
            }

            // Bufsize
            if (!string.IsNullOrEmpty(bufsize_Text))
            {
                vBufsize = "-bufsize " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.Bufsize;
            }


            // -------------------------
            // Single
            // -------------------------
            if (batch_IsChecked == false)
            {
                // -------------------------
                // Input File Has Video
                // Input Video Bitrate NOT Detected
                // Input Video Codec Detected
                // -------------------------
                if (string.IsNullOrEmpty(FFprobe.inputVideoBitrate) ||
                    FFprobe.inputVideoBitrate == "N/A")
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

                            if (!string.IsNullOrEmpty(vBitrateNA))
                            {
                                vBitMode = BitrateMode(quality_Items,
                                                        quality_SelectedItem,
                                                        bitrate_Text,
                                                        vbr_IsChecked
                                                        );

                                vBitrate = vBitrateNA; // N/A e.g. Define 3000K
                            }
                        }

                        // 2 Pass
                        //
                        else if (pass_SelectedItem == "2 Pass")
                        {
                            vCRF = string.Empty;

                            //MessageBox.Show(auto_bitrate_na); //debug

                            if (!string.IsNullOrEmpty(vBitrateNA))
                            {
                                vBitMode = BitrateMode(quality_Items,
                                                        quality_SelectedItem,
                                                        bitrate_Text,
                                                        vbr_IsChecked
                                                        );
                                vBitrate = vBitrateNA; // N/A e.g. Define 3000K
                            }
                        }
                    }
                    // -------------------------
                    // Codec Not Detected
                    // -------------------------
                    else
                    {
                        vCRF = string.Empty;

                        // Default to NA Bitrate
                        vBitMode = BitrateMode(quality_Items,
                                                quality_SelectedItem,
                                                bitrate_Text,
                                                vbr_IsChecked
                                                );

                        vBitrate = VideoBitrateCalculator(container_SelectedItem,
                                                            mediaType_SelectedItem,
                                                            codec_SelectedItem,
                                                            FFprobe.vEntryType,
                                                            vBitrateNA);

                        vMinrate = string.Empty;
                        vMaxrate = string.Empty;
                        vBufsize = string.Empty;

                        // Pixel Format
                        vOptions = string.Empty;
                    }
                }

                // -------------------------
                // Input File Has Video
                // Input Video Bitrate IS Detected
                // Input Video Codec Detected
                // -------------------------
                else if (!string.IsNullOrEmpty(FFprobe.inputVideoBitrate) &&
                            FFprobe.inputVideoBitrate != "N/A")
                {
                    // -------------------------
                    // Codec Detected
                    // -------------------------
                    if (!string.IsNullOrEmpty(FFprobe.inputVideoCodec))
                    {
                        //MessageBox.Show("5 " + vBitrate);

                        vCRF = string.Empty;

                        if (!string.IsNullOrEmpty(vBitrate))
                        {
                            vBitMode = BitrateMode(quality_Items,
                                                    quality_SelectedItem,
                                                    bitrate_Text,
                                                    vbr_IsChecked
                                                    );
                            //MessageBox.Show(vBitMode);
                        }


                    }
                    // -------------------------
                    // Codec Not Detected
                    // -------------------------
                    else
                    {
                        vCRF = string.Empty;

                        vBitMode = string.Empty;
                        vBitrate = string.Empty;
                        vMinrate = string.Empty;
                        vMaxrate = string.Empty;
                        vBufsize = string.Empty;

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
                vBitrate = "%V";
            }
        }


        /// <summary>
        ///     Video Quality - Lossless
        /// <summary>
        public static void QualityLossless(string codec_SelectedItem,
                                           List<ViewModel.VideoQuality> qualityItems
                                           )
        {
            // -------------------------
            // x265 Params
            // -------------------------
            if (codec_SelectedItem == "x265")
            {
                // e.g. -x265-params "lossless"
                x265paramsList.Add("lossless");
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
        ///     Video Quality - Custom
        /// <summary>
        public static void QualityCustom(bool vbr_IsChecked,
                                         List<ViewModel.VideoQuality> quality_Items,
                                         string quality_SelectedItem,
                                         string crf_Text,
                                         string bitrate_Text,
                                         string minrate_Text,
                                         string maxrate_Text,
                                         string bufsize_Text
                                         )
        {
            // CRF
            if (!string.IsNullOrEmpty(crf_Text))
            {
                vCRF = "-crf " + crf_Text;
            }

            // Bitrate Mode
            vBitMode = BitrateMode(quality_Items,
                                   quality_SelectedItem,
                                   bitrate_Text,
                                   vbr_IsChecked
                                   );

            // Bitrate
            vBitrate = bitrate_Text;


            // Minrate
            if (!string.IsNullOrEmpty(minrate_Text))
            {
                vMinrate = "-minrate " + minrate_Text;
            }

            // Maxrate
            if (!string.IsNullOrEmpty(maxrate_Text))
            {
                vMaxrate = "-maxrate " + maxrate_Text;
            }

            // Bufsize
            if (!string.IsNullOrEmpty(bufsize_Text))
            {
                vBufsize = "-bufsize " + bufsize_Text;
            }
        }


        /// <summary>
        ///     Video Quality - Preset
        /// <summary>
        public static void QualityPreset(bool vbr_IsChecked,
                                         string codec_SelectedItem,
                                         List<ViewModel.VideoQuality> quality_Items,
                                         string quality_SelectedItem,
                                         string pass_SelectedItem,
                                         string crf_Text,
                                         string bitrate_Text,
                                         string minrate_Text,
                                         string maxrate_Text,
                                         string bufsize_Text
                                         )
        {
            // Bitrate Mode
            vBitMode = BitrateMode(quality_Items,
                                   quality_SelectedItem,
                                   bitrate_Text,
                                   vbr_IsChecked
                                   );

            // Minrate
            if (!string.IsNullOrEmpty(minrate_Text))
            {
                vMinrate = "-minrate " + minrate_Text;
            }

            // Maxrate
            if (!string.IsNullOrEmpty(maxrate_Text))
            {
                vMaxrate = "-maxrate " + maxrate_Text;
            }

            // Bufsize
            if (!string.IsNullOrEmpty(bufsize_Text))
            {
                vBufsize = "-bufsize " + bufsize_Text;
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
                vBitrate = string.Empty;
                vMinrate = string.Empty;
                vMaxrate = string.Empty;
                vBufsize = string.Empty;
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
                    x265paramsList.Add("crf=" + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CRF);
                    vCRF = string.Empty;
                }
                // -------------------------
                // All Other Codecs
                // -------------------------
                else
                {
                    //crf = items.FirstOrDefault(item => item.Name == selectedQuality) ?.CRF;
                    vBitrate = bitrate_Text;

                    if (!string.IsNullOrEmpty(crf_Text))
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
                // Bitrate
                // -------------------------
                vBitrate = bitrate_Text;
            }
        }



        /// <summary>
        ///     Video Quality
        /// <summary>
        public static String VideoQuality(bool batch_IsChecked,
                                          bool vbr_IsChecked,
                                          string container_SelectedItem,
                                          string mediaType_SelectedItem,
                                          string codec_SelectedItem,
                                          List<ViewModel.VideoQuality> quality_Items,
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
            // Media Type Audio
            // Video Quality None
            // Video Codec None
            // Video Codec Copy
            if (mediaType_SelectedItem != "Audio" &&
                codec_SelectedItem != "None" &&
                codec_SelectedItem != "Copy" &&
                quality_SelectedItem != "None"
                )
            {
                // -------------------------
                // Auto
                // -------------------------
                if (quality_SelectedItem == "Auto" &&
                    codec_SelectedItem != "FFV1" &&   // Special Rule
                    codec_SelectedItem != "HuffYUV"   // FFV1, HuffYUV cannot use Auto Bitrate, Lossless Only, Auto is used for Codec Copy
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

                //MessageBox.Show(vBufsize); //debug

                // --------------------------------------------------
                // Combine
                // --------------------------------------------------
                List<string> vQualityArgs = new List<string>();

                // -------------------------
                // x265 Params
                // -------------------------
                if (codec_SelectedItem == "x265" &&
                    x265paramsList.Count > 0)
                {
                    x265params = "-x265-params " + "\"" + string.Join(":", x265paramsList
                                                                           .Where(s => !string.IsNullOrEmpty(s)))
                                                 + "\"";
                }
                else
                {
                    x265params = string.Empty;
                }

                // -------------------------
                // CRF
                // -------------------------
                if (pass_SelectedItem == "CRF")
                {
                    vQualityArgs = new List<string>()
                    {
                        vLossless,
                        vBitMode,
                        vBitrate,
                        vMinrate,
                        vMaxrate,
                        vBufsize,
                        vCRF,
                        x265params,
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
                        vBitrate,
                        vMinrate,
                        vMaxrate,
                        vBufsize,
                        x265params,
                        vOptions
                    };

                    //MessageBox.Show(string.Join("\n", vQualityArgs)); //debug
                }

                // Join Video Quality Args List
                vQuality = string.Join(" ", vQualityArgs
                                            .Where(s => !string.IsNullOrEmpty(s))
                                            .Where(s => !s.Equals("\n"))
                                      );

                // Log Console Message /////////        
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                    if (!string.IsNullOrEmpty(vBitrate))
                    {
                        Log.logParagraph.Inlines.Add(new Run(vBitrate) { Foreground = Log.ConsoleDefault });
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
        ///     Batch Video Quality Auto (Method)
        /// <summary>
        public static String BatchVideoQualityAuto(bool batch_IsChecked,
                                                   string mediaType_SelectedItem,
                                                   string codec_SelectedItem,
                                                   string quality_SelectedItem
                                                   )
        {
            // -------------------------
            // Batch Auto
            // -------------------------
            // Check:
            // Media Type Audio
            // Video Codec None
            // Video Codec Copy
            // Video Quality None
            if (mediaType_SelectedItem != "Audio" &&
                codec_SelectedItem != "None" &&
                codec_SelectedItem != "Copy" &&
                quality_SelectedItem != "None"
                )
            {
                // Batch Check
                //if (batch_IsChecked == true)
                //{
                    // -------------------------
                    // Video Auto Bitrates
                    // -------------------------
                    if (quality_SelectedItem == "Auto")
                    {
                        // Make List
                        List<string> BatchVideoAutoList = new List<string>()
                        {
                            // scale
                            "& for /F \"delims=\" %S in ('@" + FFprobe.ffprobe + " -v error -select_streams v:0 -show_entries format^=scale -of default^=noprint_wrappers^=1:nokey^=1 \"%~f\" 2^>^&1') do (SET scale=%S)",
                            // set %S to %scale%
                            "\r\n\r\n" + "& for /F %S in ('echo %scale%') do (echo)",

                            // duration
                            "\r\n\r\n" + "& for /F \"delims=\" %D in ('@" + FFprobe.ffprobe + " -v error -select_streams v:0 -show_entries format^=duration -of default^=noprint_wrappers^=1:nokey^=1 \"%~f\" 2^>^&1') do (SET duration=%D)",
                            // remove duration decimals
                            "\r\n\r\n" + "& for /F \"tokens=1 delims=.\" %R in ('echo %duration%') do (SET duration=%R)",
                            // set %D to %duration%
                            "\r\n\r\n" + "& for /F %D in ('echo %duration%') do (echo)",

                            // vBitrate
                            "\r\n\r\n" + "& for /F \"delims=\" %V in ('@" + FFprobe.ffprobe + " -v error -select_streams v:0 -show_entries " + FFprobe.vEntryTypeBatch + " -of default^=noprint_wrappers^=1:nokey^=1 \"%~f\" 2^>^&1') do (SET vBitrate=%V)",
                            // set %V to %vBitrate%
                            "\r\n\r\n" + "& for /F %V in ('echo %vBitrate%') do (echo)",
                            // auto bitrate calcuate
                            "\r\n\r\n" + "& (if %V EQU N/A (SET /a vBitrate=%S*8/1000/%D*1000) ELSE (echo Video Bitrate Detected))",
                            // set %V to %vBitrate%
                            "\r\n\r\n" + "& for /F %V in ('echo %vBitrate%') do (echo)",
                        };

                        // -------------------------
                        // Join List with Spaces, Remove Empty Strings
                        // -------------------------
                        batchVideoAuto = string.Join(" ", BatchVideoAutoList
                                                          .Where(s => !string.IsNullOrEmpty(s)));

                    }
                //}
            }

            // Return Value
            return batchVideoAuto;
        }



        /// <summary>
        ///     Video Bitrate Calculator (Method)
        /// <summary>
        public static String VideoBitrateCalculator(string container_SelectedItem,
                                                    string mediaType_SelectedItem,
                                                    string codec_SelectedItem,
                                                    string vEntryType, 
                                                    string inputVideoBitrate)
        {
            // -------------------------
            // Null Check
            // -------------------------
            if (!string.IsNullOrEmpty(inputVideoBitrate))
            {
                // -------------------------
                // Remove K & M from input if any
                // -------------------------
                inputVideoBitrate = Regex.Replace(inputVideoBitrate, "k", "", RegexOptions.IgnoreCase);
                inputVideoBitrate = Regex.Replace(inputVideoBitrate, "m", "", RegexOptions.IgnoreCase);

                // -------------------------
                // Capture only "N/A" from FFprobe
                // -------------------------
                if (inputVideoBitrate.Length >= 3) // Out of Rang check
                {
                    if (inputVideoBitrate.Substring(0, 3) == "N/A")
                    {
                        inputVideoBitrate = "N/A";
                    }
                }

                // -------------------------
                // If Video has a Bitrate, calculate Bitrate into decimal
                // -------------------------
                if (inputVideoBitrate != "N/A")
                {
                    // e.g. (1000M / 1,000,000K)
                    if (Convert.ToInt32(inputVideoBitrate) >= 1000000000)
                    {
                        inputVideoBitrate = Convert.ToString(int.Parse(inputVideoBitrate) * 0.00001);
                    }
                    // e.g. (100M / 100,000K) 
                    else if (Convert.ToInt32(inputVideoBitrate) >= 100000000)
                    {
                        inputVideoBitrate = Convert.ToString(int.Parse(inputVideoBitrate) * 0.0001);
                    }
                    // e.g. (10M / 10,000K)
                    else if (Convert.ToInt32(inputVideoBitrate) >= 10000000)
                    {
                        inputVideoBitrate = Convert.ToString(int.Parse(inputVideoBitrate) * 0.001);
                    }
                    // e.g. (1M /1000K)
                    else if (Convert.ToInt32(inputVideoBitrate) >= 100000)
                    {
                        inputVideoBitrate = Convert.ToString(int.Parse(inputVideoBitrate) * 0.001);
                    }
                    // e.g. (100K)
                    else if (Convert.ToInt32(inputVideoBitrate) >= 10000)
                    {
                        inputVideoBitrate = Convert.ToString(int.Parse(inputVideoBitrate) * 0.001);
                    }
                }

                // -------------------------
                // If Video Variable = N/A, Calculate Bitate (((Filesize*8)/1000)/Duration)
                // Formats like WebM, MKV and with Missing Metadata can have New Bitrates calculated and applied
                // -------------------------
                if (inputVideoBitrate == "N/A")
                {
                    // Calculating Bitrate will crash if jpg/png
                    try
                    {
                        // Convert to int to remove decimals
                        inputVideoBitrate = Convert.ToInt32((double.Parse(FFprobe.inputSize) * 8) / 1000 / double.Parse(FFprobe.inputDuration)).ToString();


                        // Log Console Message /////////
                        Log.WriteAction = () =>
                        {
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new Bold(new Run("Calculating New Bitrate Information...")) { Foreground = Log.ConsoleAction });
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
                            Log.logParagraph.Inlines.Add(new Bold(new Run("Error: Could Not Calculate New Bitrate Information...")) { Foreground = Log.ConsoleError });
                        };
                        Log.LogActions.Add(Log.WriteAction);
                    }
                }


                // -------------------------
                // WebM Video Bitrate Limiter
                // If input video bitrate is greater than 1.5M, lower the bitrate to 1.5M
                // -------------------------
                if (inputVideoBitrate != "N/A" &&
                    !string.IsNullOrEmpty(inputVideoBitrate) &&
                    container_SelectedItem == "webm" &&
                    codec_SelectedItem != "Copy"
                    )
                {
                    if (Convert.ToDouble(inputVideoBitrate) >= 1500)
                    {
                        inputVideoBitrate = "1500";
                    }
                }


                // -------------------------
                // Round Bitrate, Remove Decimals
                // -------------------------
                try
                {
                    inputVideoBitrate = Math.Round(double.Parse(inputVideoBitrate)).ToString();
                }
                catch
                {
                    inputVideoBitrate = "2000";
                }

                // -------------------------
                // Add K to end of Bitrate
                // -------------------------
                if (mediaType_SelectedItem != "Image" &&
                    mediaType_SelectedItem != "Sequence")
                {
                    inputVideoBitrate = inputVideoBitrate + "K";
                }
            }

            // -------------------------
            // Input Video Bitrate does not exist
            // -------------------------
            else
            {
                if (string.IsNullOrEmpty(inputVideoBitrate))
                {
                    // do nothing (dont remove, it will cause substring to overload)

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Input Video Bitrate does not exist or can't be detected")) { Foreground = Log.ConsoleWarning });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }


            return inputVideoBitrate;
        }



        /// <summary>
        ///     Pass 1 Modifier (Method)
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
                    pass1 = "-x265-params pass=1";
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
        ///     Pass 2 Modifier (Method)
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
                    pass2 = "-x265-params pass=2";
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
        ///     Frame Rate To Decimal
        /// <summary>
        /// <remarks>
        ///     When using Video Frame Range instead of Time
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
                if (!string.IsNullOrEmpty(frame)) // Default/Null Check
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
        ///     Pixel Foramt
        /// <summary>
        public static String PixFmt(string mediaType_SelectedItem,
                                    string codec_SelectedItem,
                                    string quality_SelectedItem,
                                    string pixelFormat_SelectedItem
                                    )
        {
            // Check:
            // Media Type Audio
            // Video Codec None
            // Video Codec Copy
            // Video Quality None
            if (mediaType_SelectedItem != "Audio" &&
                codec_SelectedItem != "None" &&
                codec_SelectedItem != "Copy" &&
                quality_SelectedItem != "None"
                )
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
        ///     Optimize
        /// <summary>
        public static String Optimize(string mediaType_SelectedItem,
                                      string codec_SelectedItem,
                                      string quality_SelectedItem,
                                      List<ViewModel.VideoOptimize> optimize_Items,
                                      string optimize_SelectedItem,
                                      string tune_SelectedItem,
                                      string profile_SelectedItem,
                                      string level_SelectedItem
                                      )
        {
            // Check:
            // Media Type Audio
            // Video Codec None
            // Video Codec Copy
            // Video Quality None
            if (mediaType_SelectedItem != "Audio" &&
                codec_SelectedItem != "None" &&
                codec_SelectedItem != "Copy" &&
                quality_SelectedItem != "None" 
                )
            {
                //MessageBox.Show(tune_SelectedItem); //debug
                //MessageBox.Show(profile_SelectedItem); //debug
                //MessageBox.Show(level_SelectedItem); //debug

                // -------------------------
                // Tune
                // -------------------------
                if (!string.IsNullOrEmpty(tune_SelectedItem) && 
                    tune_SelectedItem != "none")
                {
                    optTune = "-tune " + tune_SelectedItem;
                }

                // -------------------------
                // Profile
                // -------------------------
                if (!string.IsNullOrEmpty(profile_SelectedItem) && 
                    profile_SelectedItem != "none")
                {
                    optProfile = "-profile:v " + profile_SelectedItem;
                }

                // -------------------------
                // Level
                // -------------------------
                if (!string.IsNullOrEmpty(level_SelectedItem) && 
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

                optimize = string.Join(" ", v2passList.Where(s => !string.IsNullOrEmpty(s)));
            }

            // Return Value
            return optimize;
        }



        /// <summary>
        ///     FPS
        /// <summary>
        public static String FPS(string mediaType_SelectedItem,
                                 string codec_SelectedItem,
                                 string quality_SelectedItem,
                                 string fps_SelectedItem,
                                 string fps_Text
                                 )
        {
            // Check:
            // Media Type Audio
            // Video Codec None
            // Video Codec Copy
            // Video Quality None
            if (mediaType_SelectedItem != "Audio" &&
                codec_SelectedItem != "None" &&
                codec_SelectedItem != "Copy" &&
                quality_SelectedItem != "None" &&
                !string.IsNullOrEmpty(fps_Text)
                )
            {
                //fps = string.Empty;

                if (fps_SelectedItem == "auto")
                {
                    fps = string.Empty;
                }
                else if (fps_SelectedItem == "film")
                {
                    fps = "-r film";
                }
                else if (fps_SelectedItem == "pal")
                {
                    fps = "-r pal";
                }
                else if (fps_SelectedItem == "ntsc")
                {
                    fps = "-r ntsc";
                }
                else if (fps_SelectedItem == "23.976")
                {
                    fps = "-r 24000/1001";
                }
                else if (fps_SelectedItem == "24")
                {
                    fps = "24";
                }
                else if (fps_SelectedItem == "25")
                {
                    fps = "-r 25";
                }
                else if (fps_SelectedItem == "ntsc" ||
                         fps_SelectedItem == "29.97")
                {
                    fps = "-r 30000/1001";
                }
                else if (fps_SelectedItem == "30")
                {
                    fps = "-r 30";
                }
                else if (fps_SelectedItem == "48")
                {
                    fps = "-r 48";
                }
                else if (fps_SelectedItem == "50")
                {
                    fps = "-r 50";
                }
                else if (fps_SelectedItem == "59.94")
                {
                    fps = "-r 60000/1001";
                }
                else if (fps_SelectedItem == "60")
                {
                    fps = "-r 60";
                }
                else
                {
                    try
                    {
                        fps = "-r " + fps_Text;
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
        ///     Video Speed (Method)
        /// <summary>
        public static void Speed(string mediaType_SelectedItem,
                                 string codec_SelectedItem,
                                 string quality_SelectedItem,
                                 string speed_SelectedItem,
                                 string speed_Text
                                 )
        {
            // Media Type Audio
            // Video Codec None
            // Video Codec Copy
            // Video Bitrate None
            // Speed Auto/Null
            if (mediaType_SelectedItem != "Audio" &&
                codec_SelectedItem != "None" &&
                codec_SelectedItem != "Copy" &&
                quality_SelectedItem != "None" &&
                speed_SelectedItem != "auto" &&
                !string.IsNullOrEmpty(speed_Text)
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
        ///     Size Width Auto
        /// <summary>
        public static void SizeWidthAuto(string codec_SelectedItem)
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
                width = "-1";
            }
            else if (codec_SelectedItem == "x264" ||
                     codec_SelectedItem == "x265" ||
                     codec_SelectedItem == "MPEG-2" ||
                     codec_SelectedItem == "MPEG-4")
            {
                width = "-2";
            }
        }


        /// <summary>
        ///     Size Height Auto
        /// <summary>
        public static void SizeHeightAuto(string codec_SelectedItem)
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
                height = "-1";
            }
            else if (codec_SelectedItem == "x264" ||
                     codec_SelectedItem == "x265" ||
                     codec_SelectedItem == "MPEG-2" ||
                     codec_SelectedItem == "MPEG-4")
            {
                height = "-2";
            }
        }


        /// <summary>
        ///     Size
        /// <summary>
        // Size is a Filter
        public static void Scale(string codec_SelectedItem,
                                 string size_SelectedItem,
                                 string width_Text,
                                 string height_Text,
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
                    // Combine & Add Aspect Filter
                    // -------------------------
                    //combine
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
                // 8K
                // -------------------------
                if (size_SelectedItem == "8K")
                {
                    // Width
                    width = "7680"; // Note: 8K is measured width first

                    // Height
                    SizeHeightAuto(codec_SelectedItem);
                }
                // -------------------------
                // 4K
                // -------------------------
                else if (size_SelectedItem == "4K")
                {
                    // Width
                    width = "4096"; // Note: 4K is measured width first

                    // Height
                    SizeHeightAuto(codec_SelectedItem);
                }
                // -------------------------
                // 4K UHD
                // -------------------------
                else if (size_SelectedItem == "4K UHD")
                {
                    // Width
                    width = "3840"; // Note: 4K is measured width first

                    // Height
                    SizeHeightAuto(codec_SelectedItem);

                }
                // -------------------------
                // 2K
                // -------------------------
                else if (size_SelectedItem == "2K")
                {
                    // Width
                    width = "2048"; // Note: 2K is measured width first

                    // Height
                    SizeHeightAuto(codec_SelectedItem);
                }
                // -------------------------
                // 1440p
                // -------------------------
                else if (size_SelectedItem == "1440p")
                {
                    // Width
                    SizeWidthAuto(codec_SelectedItem);

                    // Height
                    height = "1440";
                }
                // -------------------------
                // 1200p
                // -------------------------
                else if (size_SelectedItem == "1200p")
                {
                    // Width
                    SizeWidthAuto(codec_SelectedItem);

                    // Height
                    height = "1200";
                }
                // -------------------------
                // 1080p
                // -------------------------
                else if (size_SelectedItem == "1080p")
                {
                    // Width
                    SizeWidthAuto(codec_SelectedItem);

                    // Height
                    height = "1080";
                }
                // -------------------------
                // 720p
                // -------------------------
                else if (size_SelectedItem == "720p")
                {
                    // Width
                    SizeWidthAuto(codec_SelectedItem);

                    // Height
                    height = "720";
                }
                // -------------------------
                // 480p
                // -------------------------
                else if (size_SelectedItem == "480p")
                {
                    // Width
                    SizeWidthAuto(codec_SelectedItem);

                    // Height
                    height = "480";

                }
                // -------------------------
                // 320p
                // -------------------------
                else if (size_SelectedItem == "320p")
                {
                    // Width
                    SizeWidthAuto(codec_SelectedItem);

                    // Height
                    height = "320";
                }
                // -------------------------
                // 240p
                // -------------------------
                else if (size_SelectedItem == "240p")
                {
                    // Width
                    SizeWidthAuto(codec_SelectedItem);

                    // Height
                    height = "240";
                }
                // -------------------------
                // Custom Size
                // -------------------------
                else if (size_SelectedItem == "Custom")
                {
                    MainWindow mainwindow = (MainWindow)System.Windows.Application.Current.MainWindow;
                    ViewModel vm = mainwindow.DataContext as ViewModel;

                    // Get width height from custom textbox
                    width = width_Text;
                    height = height_Text;

                    // Change the left over Default empty text to "auto"
                    if (string.Equals(width_Text, "", StringComparison.CurrentCultureIgnoreCase))
                    {
                        vm.Video_Width_Text = "auto";
                    }

                    if (string.Equals(height_Text, "", StringComparison.CurrentCultureIgnoreCase))
                    {
                        vm.Video_Height_Text = "auto";
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
                    }


                    // -------------------------
                    // x264 & x265
                    // -------------------------
                    // Fix FFmpeg MP4 but (User entered value)
                    // Apply Fix to all scale effects above
                    //
                    if (codec_SelectedItem == "x264" ||
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
                            // Aspect Must be Cropped to be divisible by 2
                            // e.g. -vf "scale=777:777, crop=776:776:0:0"
                            //
                            try
                            {
                                // Only if Crop is already Empty
                                // User Defined Crop should always override Divisible Crop
                                // CropClearButton ~ is used as an Identifier, Divisible Crop does not leave "Clear*"
                                //
                                if (cropClear_Text == "") // Crop Set Check
                                {
                                    // Temporary Strings
                                    // So not to Override User Defined Crop
                                    int divisibleCropWidth = Convert.ToInt32(width);
                                    int divisibleCropHeight = Convert.ToInt32(height);
                                    string cropX = "0";
                                    string cropY = "0";

                                    // int convert check
                                    if (Int32.TryParse(width, out divisibleCropWidth) &&
                                        Int32.TryParse(height, out divisibleCropHeight))
                                    {
                                        // If not divisible by 2, subtract 1 from total

                                        // Width
                                        if (divisibleCropWidth % 2 != 0)
                                        {
                                            divisibleCropWidth -= 1;
                                        }
                                        // Height
                                        if (divisibleCropHeight % 2 != 0)
                                        {
                                            divisibleCropHeight -= 1;
                                        }

                                        CropWindow.crop = Convert.ToString("crop=" + divisibleCropWidth + ":" + divisibleCropHeight + ":" + cropX + ":" + cropY);
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
                                    Log.logParagraph.Inlines.Add(new Bold(new Run("Warning: Must enter numbers only.")) { Foreground = Log.ConsoleWarning });
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
                                if (Int32.TryParse(height, out divisibleHeight))
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
                                    Log.logParagraph.Inlines.Add(new Bold(new Run("Warning: Must enter numbers only.")) { Foreground = Log.ConsoleWarning });
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
                                if (Int32.TryParse(width, out divisibleWidth))
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
                                    Log.logParagraph.Inlines.Add(new Bold(new Run("Warning: Must enter numbers only.")) { Foreground = Log.ConsoleWarning });
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


                // -------------------------
                // Combine & Add Aspect Filter
                // -------------------------
                scale = "scale=" + width + ":" + height + ScalingAlgorithm(scalingAlgorithm_SelectedItem);

                // Video Filter Add
                VideoFilters.vFiltersList.Add(scale);

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
        ///     Aspect Ratio
        /// <summary>
        public static String AspectRatio(string mediaType_SelectedItem,
                                         string codec_SelectedItem,
                                         string quality_SelectedItem,
                                         string aspectRatio_SelectedItem
                                         )
        {
            // Check:
            // Media Type Audio
            // Video Codec None
            // Video Quality None
            if (mediaType_SelectedItem != "Audio" &&
                codec_SelectedItem != "None" &&
                quality_SelectedItem != "None"
                )
            {
                // None & Default
                //
                if (aspectRatio_SelectedItem == "auto")
                {
                    vAspectRatio = string.Empty;
                }

                // Aspect, eg. 4:3, 16:9
                //
                else
                {
                    vAspectRatio = "-aspect " + aspectRatio_SelectedItem;
                }
            }

            return vAspectRatio;
        }



        /// <summary>
        ///     Scaling Algorithm
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
        ///     Crop (Method)
        /// <summary>
        public static void Crop(CropWindow cropwindow, ViewModel vm)
        {
            // -------------------------
            // Clear
            // -------------------------
            // Clear leftover Divisible Crop if not x264/x265
            // CropClearButton is used as an Identifier, Divisible Crop does not leave "Clear*"
            if (vm.Video_Codec_SelectedItem != "x264" &&
                vm.Video_Codec_SelectedItem != "x265" &&
                vm.Video_Codec_SelectedItem != "MPEG-2" &&
                vm.Video_Codec_SelectedItem != "MPEG-4" &&
                vm.Video_CropClear_Text == "Clear"
                )
            {
                CropWindow.crop = string.Empty;
            }

            // Clear Crop if MediaTypeControls is Audio
            if (vm.Format_MediaType_SelectedItem == "Audio")
            {
                CropWindow.crop = string.Empty;
            }

            // -------------------------
            // Add Crop to Video Filters if Not Null
            // -------------------------
            // If Crop is set by User in the CropWindow
            if (!string.IsNullOrEmpty(CropWindow.crop))
            {
                // Video Filters Add
                VideoFilters.vFiltersList.Add(CropWindow.crop);
            }
        }


        /// <summary>
        ///     Images
        /// <summary>
        public static String Images(string mediaType_SelectedItem,
                                    string codec_SelectedItem,
                                    string quality_SelectedItem)
        {
            // Check:
            // Media Type Audio
            // Video Codec None
            // Video Codec Copy
            // Video Quality None
            if (mediaType_SelectedItem != "Audio" &&
                codec_SelectedItem != "None" &&
                codec_SelectedItem != "Copy" &&
                quality_SelectedItem != "None" 
                )
            {
                // -------------------------
                // Image
                // -------------------------
                if (mediaType_SelectedItem == "Image")
                {
                    image = "-vframes 1"; //important
                }

                // -------------------------
                // Sequence
                // -------------------------
                else if (mediaType_SelectedItem == "Sequence")
                {
                    image = string.Empty; //disable -vframes
                }

                // -------------------------
                // All Other Media Types
                // -------------------------
                else
                {
                    image = string.Empty;
                }
            }

            return image;
        }



    }
}
