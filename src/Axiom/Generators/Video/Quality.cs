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

 * BitRate Mode
 * Video Quality
 * Batch Video Quality Auto
 * Video BitRate Calculator
 * Pass 1 Modifier
 * Pass 2 Modifier
 * Optimize
 * Pixel Format
---------------------------------- */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using ViewModel;
using Axiom;
using System.Collections.ObjectModel;
using System.Globalization;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Generate.Video
{
    public class Quality
    {
        // Bit Rate
        public static string vQuality { get; set; } // Video Quality
        public static string vBitMode { get; set; } // -b:v -q:v
        public static string vLossless { get; set; }
        public static string vBitRate { get; set; } // Video Bit Rate
        public static string vBitRateNA { get; set; } // N/A e.g. Define 3000K
        public static string vMinRate { get; set; }
        public static string vMaxRate { get; set; }
        public static string vBufSize { get; set; }
        public static string vOptions { get; set; } // -pix_fmt, -qcomp
        public static string vCRF { get; set; } // Constant Rate Factor
        public static string pix_fmt { get; set; } // Pixel Format

        // Optimize
        public static string optTune { get; set; } // x264/x265 tuning modes
        public static string optProfile { get; set; } // x264/x265 Profile
        public static string optLevel { get; set; } // x264/x265 Level
        public static string optFlags { get; set; } // Additional Optimization Flags
        public static string optimize { get; set; } // Contains opTune + optProfile + optLevel

        // Pass
        public static string v2PassArgs { get; set; } // 2-Pass Arguments
        public static string passSingle { get; set; } // 1-Pass & CRF Args
        public static string pass1Args { get; set; } // Batch 2-Pass (Pass 1)
        public static string pass2Args { get; set; } // Batch 2-Pass (Pass 2)
        public static string pass1 { get; set; } // x265 Modifier
        public static string pass2 { get; set; } // x265 Modifier

        // Batch
        public static string batchVideoAuto { get; set; }

        /// <summary>
        /// BitRate Mode
        /// <summary>
        public static String BitRateMode(ObservableCollection<ViewModel.Video.VideoQuality> quality_Items,
                                         string quality_SelectedItem,
                                         string bitrate_Text,
                                         bool vbr_IsChecked
                                        )
        {
            //MessageBox.Show(vbr_IsChecked.ToString()); //debug

            // Only if BitRate Textbox is not Empty (except for Auto Quality)
            if (quality_SelectedItem == "Auto" ||
                !string.IsNullOrWhiteSpace(bitrate_Text)
                )
            {
                switch (vbr_IsChecked)
                {
                    // -------------------------
                    // CBR
                    // -------------------------
                    case false:
                        // -b:v
                        vBitMode = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CBR_BitMode;
                        //MessageBox.Show(vBitMode); //debug
                        break;

                    // -------------------------
                    // VBR
                    // -------------------------
                    case true:
                        // -q:v
                        vBitMode = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.VBR_BitMode;
                        //MessageBox.Show(vBitMode); //debug
                        break;
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
                                       ObservableCollection<ViewModel.Video.VideoQuality> quality_Items,
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
                                                  Analyze.FFprobe.vEntryType,
                                                  Analyze.FFprobe.inputVideoBitRate);
            }
            // Images
            else if (mediaType_SelectedItem == "Image" ||
                     mediaType_SelectedItem == "Sequence"
                    )
            {
                vBitRate = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.VBR;
            }



            // BitRate N/A
            // Defaults to a sensable value, such as 3000K
            vBitRateNA = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.NA;

            // MinRate
            if (!string.IsNullOrWhiteSpace(minrate_Text))
            {
                vMinRate = "-minrate " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.MinRate;
            }

            // MaxRate
            if (!string.IsNullOrWhiteSpace(maxrate_Text))
            {
                vMaxRate = "-maxrate " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.MaxRate;
            }

            // BufSize
            if (!string.IsNullOrWhiteSpace(bufsize_Text))
            {
                vBufSize = "-bufsize " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.BufSize;
            }


            switch (batch_IsChecked)
            {
                // -------------------------
                // Single
                // -------------------------
                case false:
                    // -------------------------
                    // Input File Has Video
                    // Input Video BitRate NOT Detected
                    // Input Video Codec Detected
                    // -------------------------
                    if (string.IsNullOrWhiteSpace(Analyze.FFprobe.inputVideoBitRate) ||
                        Analyze.FFprobe.inputVideoBitRate == "N/A")
                    {
                        // -------------------------
                        // Codec Detected
                        // -------------------------
                        if (!string.IsNullOrWhiteSpace(Analyze.FFprobe.inputVideoCodec))
                        {
                            // 1 Pass / CRF
                            //
                            if (pass_SelectedItem == "1 Pass" ||
                                pass_SelectedItem == "CRF")
                            {
                                vCRF = string.Empty;

                                if (!string.IsNullOrWhiteSpace(vBitRateNA))
                                {
                                    vBitMode = BitRateMode(quality_Items,
                                                           quality_SelectedItem,
                                                           bitrate_Text,
                                                           vbr_IsChecked);

                                    vBitRate = vBitRateNA; // N/A e.g. Define 3000K
                                }
                            }

                            // 2 Pass
                            //
                            else if (pass_SelectedItem == "2 Pass")
                            {
                                vCRF = string.Empty;

                                //MessageBox.Show(auto_bitrate_na); //debug

                                if (!string.IsNullOrWhiteSpace(vBitRateNA))
                                {
                                    vBitMode = BitRateMode(quality_Items,
                                                           quality_SelectedItem,
                                                           bitrate_Text,
                                                           vbr_IsChecked);
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
                                                   vbr_IsChecked);

                            //MessageBox.Show(vBitRateNA); //debug
                            vBitRate = VideoBitRateCalculator(container_SelectedItem,
                                                              mediaType_SelectedItem,
                                                              codec_SelectedItem,
                                                              Analyze.FFprobe.vEntryType,
                                                              vBitRateNA);

                            //MessageBox.Show(vBitRate); //debug

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
                    else if (!string.IsNullOrWhiteSpace(Analyze.FFprobe.inputVideoBitRate) &&
                             Analyze.FFprobe.inputVideoBitRate != "N/A")
                    {
                        // -------------------------
                        // Codec Detected
                        // -------------------------
                        if (!string.IsNullOrWhiteSpace(Analyze.FFprobe.inputVideoCodec))
                        {
                            //MessageBox.Show("5 " + vBitRate); //debug

                            vCRF = string.Empty;

                            if (!string.IsNullOrWhiteSpace(vBitRate))
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
                    break;

                // -------------------------
                // Batch
                // -------------------------
                case true:
                    // Use the CMD Batch Video Variable
                    vBitMode = "-b:v";

                    switch (VM.ConfigureView.Shell_SelectedItem)
                    {
                        // CMD
                        case "CMD":
                            // Images
                            if (mediaType_SelectedItem == "Image")
                            {
                                // Images can't use FFprobe Auto Quality
                                vBitRate = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.VBR;
                            }
                            // All other media types
                            else
                            {
                                vBitRate = "%V";
                            }
                            break;

                        // PowerShell
                        case "PowerShell":
                            // Images
                            if (mediaType_SelectedItem == "Image")
                            {
                                // Images can't use FFprobe Auto Quality
                                vBitRate = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.VBR;
                            }
                            // All other media types
                            else
                            {
                                vBitRate = "$vBitrate";
                            }
                            break;
                    }
                    break;
            }
        }


        /// <summary>
        /// Video Quality - Lossless
        /// <summary>
        public static void QualityLossless(string codec_SelectedItem,
                                           ObservableCollection<ViewModel.Video.VideoQuality> qualityItems
                                          )
        {
            // -------------------------
            // x265 Params
            // -------------------------
            if (codec_SelectedItem == "x265")
            {
                // e.g. -x265-params "lossless"
                Params.vParamsList.Add("lossless");
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
                                         string codec_SelectedItem,
                                         ObservableCollection<ViewModel.Video.VideoQuality> quality_Items,
                                         string quality_SelectedItem,
                                         string crf_Text,
                                         string bitrate_Text,
                                         string minrate_Text,
                                         string maxrate_Text,
                                         string bufsize_Text
                                        )
        {
            // --------------------------------------------------
            // CRF
            // --------------------------------------------------
            // -------------------------
            // HW Accel Transcode Codecs
            // -------------------------
            if (VM.VideoView.Video_HWAccel_SelectedItem == "On" &&
                VM.VideoView.Video_HWAccel_Transcode_SelectedItem != "off" &&     // Transcode not off
                VM.VideoView.Video_HWAccel_Transcode_SelectedItem != "auto" &&    // Transcode not auto
                VM.VideoView.Video_HWAccel_Transcode_SelectedItem != "AMD AMF" && // Ignore AMD AMF
                (//codec_SelectedItem == "x264" || 
                 //codec_SelectedItem == "x265" || 
                codec_SelectedItem == "H264 NVENC" ||
                codec_SelectedItem == "HEVC NVENC" ||
                codec_SelectedItem == "H264 QSV" ||
                codec_SelectedItem == "HEVC QSV")
              )
            {
                //string crf_hwaccel_val = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CRF_HWAccel;
                //string cbr_val = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CBR;

                if (!string.IsNullOrWhiteSpace(crf_Text))
                {
                    switch (VM.VideoView.Video_HWAccel_Transcode_SelectedItem)
                    {
                        //case "AMD AMF":
                        //    vCRF = "-rc vbr_hq -qmin 0 -cq " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CRF_HWAccel + " " +
                        //           "-b:v " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CBR;
                        //    break;

                        case "NVIDIA NVENC":
                            switch (codec_SelectedItem)
                            {
                                case "H264 NVENC":
                                    // use normal CRF instead of -cq:v
                                    vCRF = "-rc:v vbr_hq -crf " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CRF;
                                    break;

                                case "HEVC NVENC":
                                    vCRF = "-rc:v vbr_hq -qmin 0 -cq:v " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CRF_HWAccel_NVIDIA_NVENC + " " +
                                    "-b:v " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CBR;
                                    break;
                            }
                            break;

                        case "Intel QSV":
                            vCRF = "-global_quality " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CRF_HWAccel_Intel_QSV + " -look_ahead 1";
                            break;
                    }
                }

                // Halt from going any further
                return;
            }

            // -------------------------
            // Normal Codecs
            // -------------------------
            if (!string.IsNullOrWhiteSpace(crf_Text))
            {
                vCRF = "-crf " + crf_Text;
            }


            // --------------------------------------------------
            // Bit Rate
            // --------------------------------------------------
            // Bit Rate Mode
            vBitMode = BitRateMode(quality_Items,
                                   quality_SelectedItem,
                                   bitrate_Text,
                                   vbr_IsChecked
                                  );

            // Bit Rate
            vBitRate = bitrate_Text;


            // MinRate
            if (!string.IsNullOrWhiteSpace(minrate_Text))
            {
                vMinRate = "-minrate " + minrate_Text.ToUpper();
            }

            // MaxRate
            if (!string.IsNullOrWhiteSpace(maxrate_Text))
            {
                vMaxRate = "-maxrate " + maxrate_Text.ToUpper();
            }

            // BufSize
            if (!string.IsNullOrWhiteSpace(bufsize_Text))
            {
                vBufSize = "-bufsize " + bufsize_Text.ToUpper();
            }
        }


        /// <summary>
        /// Video Quality - Preset
        /// <summary>
        public static void QualityPreset(bool vbr_IsChecked,
                                         string codec_SelectedItem,
                                         ObservableCollection<ViewModel.Video.VideoQuality> quality_Items,
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
            if (!string.IsNullOrWhiteSpace(minrate_Text))
            {
                vMinRate = "-minrate " + minrate_Text.ToUpper();
            }

            // MaxRate
            if (!string.IsNullOrWhiteSpace(maxrate_Text))
            {
                vMaxRate = "-maxrate " + maxrate_Text.ToUpper();
            }

            // BufSize
            if (!string.IsNullOrWhiteSpace(bufsize_Text))
            {
                vBufSize = "-bufsize " + bufsize_Text.ToUpper();
            }

            // --------------------------------------------------
            // Encoding Pass
            // --------------------------------------------------
            switch (pass_SelectedItem)
            {
                // -------------------------
                // auto
                // -------------------------
                case "auto":
                    vCRF = string.Empty;
                    vBitRate = string.Empty;
                    vMinRate = string.Empty;
                    vMaxRate = string.Empty;
                    vBufSize = string.Empty;
                    break;

                // -------------------------
                // CRF
                // -------------------------
                case "CRF":
                    // --------------------------------------------------
                    // HW Accel Transcode Codecs
                    // --------------------------------------------------
                    if (VM.VideoView.Video_HWAccel_SelectedItem == "On" &&
                        VM.VideoView.Video_HWAccel_Transcode_SelectedItem != "off" &&     // Transcode not off
                        VM.VideoView.Video_HWAccel_Transcode_SelectedItem != "auto" &&    // Transcode not auto
                        VM.VideoView.Video_HWAccel_Transcode_SelectedItem != "AMD AMF" && // Ignore AMD AMF
                        (//codec_SelectedItem == "x264" ||
                         //codec_SelectedItem == "x265" ||
                         codec_SelectedItem == "H264 NVENC" ||
                         codec_SelectedItem == "HEVC NVENC" ||
                         codec_SelectedItem == "H264 QSV" ||
                         codec_SelectedItem == "HEVC QSV"
                        )
                    )
                    {
                        //vBitRate = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CBR;

                        if (!string.IsNullOrWhiteSpace(crf_Text))
                        {
                            switch (VM.VideoView.Video_HWAccel_Transcode_SelectedItem)
                            {
                                //case "AMD AMF":
                                //    vCRF = "-rc vbr_hq -qmin 0 -cq " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CRF_HWAccel + " " +
                                //           "-b:v " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CBR;
                                //    break;

                                case "NVIDIA NVENC":
                                    //vCRF = "-rc:v vbr_hq -qmin 0 -cq:v " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CRF_HWAccel_NVIDIA_NVENC + " " +
                                    //        "-b:v " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CBR;
                                    switch (codec_SelectedItem)
                                    {
                                        case "H264 NVENC":
                                            // use normal CRF instead of -cq:v
                                            vCRF = "-rc:v vbr_hq -crf " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CRF;
                                            break;

                                        case "HEVC NVENC":
                                            vCRF = "-rc:v vbr_hq -qmin 0 -cq:v " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CRF_HWAccel_NVIDIA_NVENC + " " +
                                            "-b:v " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CBR;
                                            break;
                                    }
                                    break;

                                case "Intel QSV":
                                    vCRF = "-global_quality " + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CRF_HWAccel_Intel_QSV + " -look_ahead 1";
                                    break;
                            }
                        }

                        // Halt from going any further
                        return;
                    }

                    // --------------------------------------------------
                    // Normal Codecs
                    // --------------------------------------------------
                    // -------------------------
                    // x265 Params
                    // -------------------------
                    if (codec_SelectedItem == "x265")
                    {
                        // https://trac.ffmpeg.org/wiki/Encode/H.265
                        //vCRF = string.Empty;
                        if (!string.IsNullOrWhiteSpace(crf_Text))
                        {
                            vCRF = "-crf " + crf_Text;
                        }

                        // x265 Params
                        Params.vParamsList.Add("crf=" + quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CRF);

                        //MessageBox.Show(string.Join("", VideoParams.vParamsList)); //debug
                    }
                    // -------------------------
                    // All Other Codecs
                    // -------------------------
                    else
                    {
                        vBitRate = bitrate_Text;

                        if (!string.IsNullOrWhiteSpace(crf_Text))
                        {
                            vCRF = "-crf " + crf_Text;
                        }
                    }
                    break;

                // -------------------------
                // 1 Pass
                // -------------------------
                case "1 Pass":
                    // BitRate
                    vBitRate = bitrate_Text;
                    break;

                // -------------------------
                // 2 Pass
                // -------------------------
                case "2 Pass":
                    // BitRate
                    vBitRate = bitrate_Text;
                    break;
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
                                          ObservableCollection<ViewModel.Video.VideoQuality> quality_Items,
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
                                  codec_SelectedItem,
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
                IEnumerable<string> vQualityArgs = new List<string>();

                // -------------------------
                // CRF
                // -------------------------
                if (pass_SelectedItem == "CRF")
                {
                    vQualityArgs = new List<string>()
                    {
                        vLossless,
                        vBitMode,
                        //vBitRate,
                        // Format European English comma to US English peroid - 1,234 to 1.234
                        string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:0.0}", vBitRate),
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
                        //vBitRate,
                        // Format European English comma to US English peroid - 1,234 to 1.234
                        string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:0.0}", vBitRate),
                        vMinRate,
                        vMaxRate,
                        vBufSize,
                        vOptions
                    };

                    //MessageBox.Show(string.Join("\n", vQualityArgs)); //debug
                }

                // Join Video Quality Args List
                vQuality = string.Join(" ", vQualityArgs
                                            .Where(s => !string.IsNullOrWhiteSpace(s))
                                            .Where(s => !s.Equals("\n"))
                                        );

                // Log Console Message /////////    
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("BitRate: ")) { Foreground = Log.ConsoleDefault });
                    if (!string.IsNullOrWhiteSpace(vBitRate))
                    {
                        Log.logParagraph.Inlines.Add(new Run(vBitRate) { Foreground = Log.ConsoleDefault });
                    }
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("CRF: ")) { Foreground = Log.ConsoleDefault });
                    if (!string.IsNullOrWhiteSpace(crf_Text))
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
                // -------------------------
                // Video Auto BitRates
                // -------------------------
                if (quality_SelectedItem == "Auto")
                {
                    IEnumerable<string> batchVideoAutoList = new List<string>();

                    switch (VM.ConfigureView.Shell_SelectedItem)
                    {
                        // -------------------------
                        // CMD
                        // -------------------------
                        case "CMD":
                            // Images
                            if (VM.FormatView.Format_MediaType_SelectedItem == "Image")
                            {
                                // Images can't use FFprobe Auto Quality
                                batchVideoAutoList = new List<string>()
                                {
                                    string.Empty
                                };
                            }
                            // All other media types
                            else
                            {
                                batchVideoAutoList = new List<string>()
                                {
                                    // size
                                    "& for /F \"delims=\" %S in ('@" + Analyze.FFprobe.ffprobe + " -v error -select_streams v:0 -show_entries format^=size -of default^=noprint_wrappers^=1:nokey^=1 \"%~f\" 2^>^&1') do (SET size=%S)",
                                    // set %S to %size%
                                    "\r\n\r\n" + "& for /F %S in ('echo %size%') do (echo)",

                                    // duration
                                    "\r\n\r\n" + "& for /F \"delims=\" %D in ('@" + Analyze.FFprobe.ffprobe + " -v error -select_streams v:0 -show_entries format^=duration -of default^=noprint_wrappers^=1:nokey^=1 \"%~f\" 2^>^&1') do (SET duration=%D)",
                                    // remove duration decimals
                                    "\r\n\r\n" + "& for /F \"tokens=1 delims=.\" %R in ('echo %duration%') do (SET duration=%R)",
                                    // set %D to %duration%
                                    "\r\n\r\n" + "& for /F %D in ('echo %duration%') do (echo)",

                                    // vBitRate
                                    "\r\n\r\n" + "& for /F \"delims=\" %V in ('@" + Analyze.FFprobe.ffprobe + " -v error -select_streams v:0 -show_entries " + Analyze.FFprobe.vEntryTypeBatch + " -of default^=noprint_wrappers^=1:nokey^=1 \"%~f\" 2^>^&1') do (SET vBitRate=%V)",
                                    // set %V to %vBitRate%
                                    "\r\n\r\n" + "& for /F %V in ('echo %vBitRate%') do (echo)",
                                    // auto bitrate calcuate
                                    "\r\n\r\n" + "& (if %V EQU N/A (SET /a vBitRate=%S*8/1000/%D*1000) ELSE (echo Video Bit Rate Detected))",
                                    //"\r\n\r\n" + "& (if %V EQU N/A (SET /a vBitRate=(((%S * 8) / 1000) / %D) * 1000) ELSE (echo Video Bit Rate Detected))",
                                    // set %V to %vBitRate%
                                    "\r\n\r\n" + "& for /F %V in ('echo %vBitRate%') do (echo)",
                                };
                            }
                            break;

                        // -------------------------
                        // PowerShell
                        // -------------------------
                        case "PowerShell":
                            // Images
                            if (VM.FormatView.Format_MediaType_SelectedItem == "Image")
                            {
                                // Images can't use FFprobe Auto Quality
                                batchVideoAutoList = new List<string>()
                                {
                                    string.Empty
                                };
                            }
                            // All other media types
                            else
                            {
                                // Uses Batch Arguments in Batch.cs Generate.FFmpeg.Batch.Generate_FFmpegArgs()
                                batchVideoAutoList = new List<string>()
                                {
                                    string.Empty
                                };
                            }
                            break;
                    }

                    // -------------------------
                    // Join List with Spaces, Remove Empty Strings
                    // -------------------------
                    batchVideoAuto = string.Join(" ", batchVideoAutoList
                                                      .Where(s => !string.IsNullOrWhiteSpace(s)));
                }
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
            //MessageBox.Show(inputVideoBitRate.ToString()); //debug

            // -------------------------
            // Halt if Input TextBox is Empty
            // -------------------------
            if (string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
            {
                return inputVideoBitRate;
            }

            // -------------------------
            // Return Auto Bitrate if Web URL
            // -------------------------
            // Without this, returning inputVideoBitRate for N/A causes unknown bug, returning 3K instead of 3000K
            if (MainWindow.IsWebURL(VM.MainView.Input_Text) == true)
            {
                // N/A = Defaults to a sensable value, such as 3000K
                return vBitRateNA; 
            }

            // -------------------------
            // Halt if Input Video Bit Rate does not exist
            // -------------------------
            if (string.IsNullOrWhiteSpace(inputVideoBitRate))
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Input Video Bit Rate does not exist or can't be detected")) { Foreground = Log.ConsoleWarning });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                };
                Log.LogActions.Add(Log.WriteAction);

                return inputVideoBitRate;
            }

            //MessageBox.Show(inputVideoBitRate.ToString()); //debug

            // -------------------------
            // Trim
            // -------------------------
            inputVideoBitRate = inputVideoBitRate.Trim();

            // -------------------------
            // Null Check
            // -------------------------
            //if (!string.IsNullOrWhiteSpace(inputVideoBitRate))
            //{
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
                // Calculating BitRate will crash if input is jpg/png
                try
                {
                    // Input Size
                    double inputSize_Double = 0; // Fallback
                    double.TryParse(Analyze.FFprobe.inputSize.Trim(), out inputSize_Double);

                    // Input Duration
                    double inputDuration_Double = 0; // Fallback
                    double.TryParse(Analyze.FFprobe.inputDuration.Trim(), out inputDuration_Double);

                    // Convert to int to remove decimals
                    if (inputSize_Double == 0 ||
                        inputDuration_Double == 0)
                    {
                        inputVideoBitRate = "0";
                    }
                    else
                    {
                        inputVideoBitRate = Convert.ToInt64((double.Parse(Analyze.FFprobe.inputSize.Trim()) * 8) / 1000 / double.Parse(Analyze.FFprobe.inputDuration.Trim())).ToString();
                    }

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
                double inputVideoBitRate_Double = 6000000; // Fallback
                double.TryParse(inputVideoBitRate.Trim(), out inputVideoBitRate_Double);

                // -------------------------
                // Calculate
                // e.g. FFprobe 10000000 bytes -> 10000K (10M)
                //      -b:v 10000K
                // Round Bit Rate, Remove Decimals
                // -------------------------
                inputVideoBitRate = Convert.ToString(Math.Round(inputVideoBitRate_Double * 0.001));

                // -------------------------
                // WebM Video Auto Quality BitRate Limiter 
                // If input video bitrate is greater than 1.5M, lower the bitrate to 1500K / 1.5M
                // -------------------------
                if (container_SelectedItem == "webm")
                {
                    if (Convert.ToDouble(inputVideoBitRate) >= 1500)
                    {
                        inputVideoBitRate = "1500";
                    }
                }
            }

            // -------------------------
            // Add K to end of BitRate
            // -------------------------
            if (mediaType_SelectedItem != "Image" &&
                mediaType_SelectedItem != "Sequence")
            {
                inputVideoBitRate += "K";
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
                if (Params.vParamsList != null &&
                    Params.vParamsList.Count > 0)
                {
                    Params.vParamsList.Clear();
                    Params.vParamsList.TrimExcess();
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
                    Params.vParamsList.Add("pass=" + passNumber);
                }
            }

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
                    pass1 = string.Empty;
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
                    pass1 = string.Empty;
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
        /// Pixel Format
        /// <summary>
        public static String PixFmt(string codec_SelectedItem,
                                    string pixelFormat_SelectedItem
                                    )
        {
            // Codec Copy
            // Pixel Format auto
            // Pixel Format none
            if (codec_SelectedItem == "Copy" ||
                pixelFormat_SelectedItem == "auto" ||
                pixelFormat_SelectedItem == "none"
                )
            {
                return string.Empty;
            }

            // Option
            pix_fmt = "-pix_fmt " + pixelFormat_SelectedItem;

            //MessageBox.Show(pixelFormat_SelectedItem); //debug

            return pix_fmt;
        }


        /// <summary>
        /// Optimize
        /// <summary>
        public static String Optimize(string codec_SelectedItem,
                                      ObservableCollection<ViewModel.Video.VideoOptimize> optimize_Items,
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
                if (!string.IsNullOrWhiteSpace(tune_SelectedItem) &&
                    tune_SelectedItem != "none")
                {
                    optTune = "-tune " + tune_SelectedItem;
                }

                // -------------------------
                // Profile
                // -------------------------
                if (!string.IsNullOrWhiteSpace(profile_SelectedItem) &&
                    profile_SelectedItem != "none")
                {
                    optProfile = "-profile:v " + profile_SelectedItem;
                }

                // -------------------------
                // Level
                // -------------------------
                if (!string.IsNullOrWhiteSpace(level_SelectedItem) &&
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
                IEnumerable<string> v2passList = new List<string>() {
                    optTune,
                    optProfile,
                    optLevel,
                    optFlags
                };

                optimize = string.Join(" ", v2passList
                                            .Where(s => !string.IsNullOrWhiteSpace(s))
                                      );
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Optimize Tune: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(tune_SelectedItem) { Foreground = Log.ConsoleDefault });

                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Optimize Profile: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(profile_SelectedItem) { Foreground = Log.ConsoleDefault });

                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Optimize Level: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(level_SelectedItem) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            // Return Value
            return optimize;
        }
    }
}
