/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017, 2018 Matt McManis
http://github.com/MattMcManis/Axiom
http://axiomui.github.io
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

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class Video
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Variables
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // Video
        public static string vCodec; // Video Codec
        public static string vQuality; // Video Quality
        public static string vBitMode;
        public static string vBitrate; // Video Bitrate
        public static string vMinrate;
        public static string vMaxrate;
        public static string vBufsize;
        public static string vOptions; // -pix_fmt, -qcomp
        public static string crf; // Constant Rate Factor
        public static string fps; // Frames Per Second
        public static string image; // JPEG & PNG options
        public static string optTune; // x264 & x265 tuning modes
        public static string optProfile; // x264/x265 Profile
        public static string optLevel; // x264/x265 Level
        public static string optFlags; // Additional Optimization Flags
        public static string optimize; // Contains opTune + optProfile + optLevel
        public static string speed; // Speed combobox modifier
        public static string sCodec; // Subtitle Codec

        // Scale
        public static string width;
        public static string height;
        public static string aspect; // contains scale, width, height

        // Pass
        public static string v2PassArgs; // 2-Pass Arguments
        public static string passSingle; // 1-Pass & CRF Args
        public static string pass1Args; // Batch 2-Pass (Pass 1)
        public static string pass2Args; // Batch 2-Pass (Pass 2)
        public static string pass1; // x265 Modifier
        public static string pass2; // x265 Modifier

        // Subtitles
        public static string subsDir; // Subtitles Directory
        public static List<string> subtitleFilePathsList = new List<string>(); // Files Added   
        public static List<string> subtitleFileNamesList = new List<string>(); // File Names without Path

        // Filter
        public static CropWindow cropwindow;
        public static List<string> VideoFilters = new List<string>(); // Filters to String Join
        public static string geq; // png transparent to jpg whtie background filter
        public static string vFilter;

        // Batch
        public static string batchVideoAuto;

        // Rendering
        public static string hwaccel;


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Process Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Hardware Acceleration (Method)
        /// <summary>
        /// https://trac.ffmpeg.org/wiki/HWAccelIntro
        public static String HWAcceleration(MainWindow mainwindow)
        {
            // Hardware Acceleration Codec in VideoCodec() Method

            //if (mainwindow.tglHWAccel.IsChecked == true)
            //{
            //    hwaccel = "-hwaccel";
            //}
            //else
            //{
            //    hwaccel = string.Empty;
            //}

            // Only x264/x265
            //
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
            {
                // Off
                if ((string)mainwindow.cboHWAccel.SelectedItem == "off")
                {
                    hwaccel = string.Empty;
                }
                // DXVA2
                else if ((string)mainwindow.cboHWAccel.SelectedItem == "dxva2")
                {
                    // ffmpeg -hwaccel dxva2 -threads 1 -i INPUT -f null
                    hwaccel = "-hwaccel dxva2";
                }
                // CUVID
                else if ((string)mainwindow.cboHWAccel.SelectedItem == "cuvid")
                {
                    // ffmpeg -c:v h264_cuvid -i input output.mkv
                    hwaccel = string.Empty;
                }
                // NVENC
                else if ((string)mainwindow.cboHWAccel.SelectedItem == "nvenc")
                {
                    // ffmpeg -i input -c:v h264_nvenc -profile high444p -pixel_format yuv444p -preset default output.mp4
                    hwaccel = string.Empty;
                }
                // CUVID + NVENC
                else if ((string)mainwindow.cboHWAccel.SelectedItem == "cuvid+nvenc")
                {
                    // ffmpeg -hwaccel cuvid -c:v h264_cuvid -i input -c:v h264_nvenc -preset slow output.mkv
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                    {
                        hwaccel = "-hwaccel cuvid -c:v h264_cuvid";
                    }
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                    {
                        hwaccel = "-hwaccel cuvid -c:v hevc_cuvid";
                    }
                }
            }

            return hwaccel;
        }

        /// <summary>
        /// Video Codecs (Method)
        /// <summary>
        public static String VideoCodec(MainWindow mainwindow)
        {
            // Video None Check
            // Video Codec None Check
            // Media Type Check
            if ((string)mainwindow.cboVideo.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "None"
                && (string)mainwindow.cboMediaType.SelectedItem != "Audio")
            {
                // -------------------------
                // Video
                // -------------------------
                // None
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "None")
                {
                    vCodec = string.Empty;
                }
                // VP8
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8")
                {
                    vCodec = "-c:v libvpx";
                }
                // VP9
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
                {
                    vCodec = "-c:v libvpx-vp9 -tile-columns 6 -frame-parallel 1 -auto-alt-ref 1 -lag-in-frames 25";
                }
                // AV1
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "AV1")
                {
                    vCodec = "-c:v libaom-av1 -strict experimental";
                }
                // Theora
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
                {
                    vCodec = "-c:v libtheora";
                }
                // x264
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                {
                    vCodec = "-c:v libx264"; //leave profile:v main here so MKV can choose other ???
                }
                // x265
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                {
                    vCodec = "-c:v libx265"; //does not use profile:v
                }
                // mpeg4
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
                {
                    vCodec = "-c:v mpeg4 -vtag xvid";
                }
                // JPEG
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
                {
                    vCodec = string.Empty;
                }
                // PNG
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                {
                    vCodec = string.Empty;
                }
                // Copy
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy")
                {
                    vCodec = "-c:v copy";
                }
                // Unknown
                else
                {
                    vCodec = string.Empty;
                }


                // Hardware Acceleration Codec
                // HW options in HWAcceleration() Method
                //
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264"
                    || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                {
                    if ((string)mainwindow.cboHWAccel.SelectedItem == "dxva2")
                    {
                        // default
                    }
                    // CUVID
                    else if ((string)mainwindow.cboHWAccel.SelectedItem == "cuvid")
                    {
                        // x264
                        if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                        {
                            vCodec = "-c:v h264_cuvid";
                        }
                        // x265
                        else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                        {
                            vCodec = "-c:v hevc_cuvid";
                        }
                    }
                    else if ((string)mainwindow.cboHWAccel.SelectedItem == "nvenc")
                    {
                        // x264
                        if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                        {
                            vCodec = "-c:v h264_nvenc";
                        }
                        // x265
                        else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                        {
                            vCodec = "-c:v hevc_nvenc";
                        }
                    }
                    else if ((string)mainwindow.cboHWAccel.SelectedItem == "cuvid+nvenc")
                    {
                        // x264
                        if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                        {
                            vCodec = "-c:v h264_nvenc";
                        }
                        // x265
                        else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                        {
                            vCodec = "-c:v hevc_nvenc";
                        }
                    }
                }


                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Video")) { Foreground = Log.ConsoleAction });

                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Codec: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(mainwindow.cboVideoCodec.SelectedItem)) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            // Return Value
            return vCodec;
        }


        /// <summary>
        /// Subtitle Codecs (Method)
        /// <summary>
        public static String SubtitleCodec(MainWindow mainwindow)
        {
            // Video None Check
            // Subtitle none -sn Check
            // Subtitle Codec None Check
            // Media Type Check
            if ((string)mainwindow.cboVideo.SelectedItem != "None"
                && (string)mainwindow.cboSubtitle.SelectedItem != "none"
                && (string)mainwindow.cboSubtitleCodec.SelectedItem != "None"
                && (string)mainwindow.cboMediaType.SelectedItem != "Audio")
            {
                //MessageBox.Show("here"); //debug

                // -------------------------
                // Subtitle
                // -------------------------
                // None
                if ((string)mainwindow.cboSubtitleCodec.SelectedItem == "None")
                {
                    sCodec = string.Empty;
                }
                // mov_text
                else if ((string)mainwindow.cboSubtitleCodec.SelectedItem == "mov_text")
                {
                    sCodec = "-c:s mov_text";
                }
                // ASS
                else if ((string)mainwindow.cboSubtitleCodec.SelectedItem == "ASS")
                {
                    sCodec = "-c:s ass";
                }
                // SSA
                else if ((string)mainwindow.cboSubtitleCodec.SelectedItem == "SSA")
                {
                    sCodec = "-c:s ssa";
                }
                // SRT
                else if ((string)mainwindow.cboSubtitleCodec.SelectedItem == "SRT")
                {
                    sCodec = "-c:s srt";
                }
                // Copy
                else if ((string)mainwindow.cboSubtitleCodec.SelectedItem == "Copy")
                {
                    sCodec = "-c:s copy";
                }


                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Codec: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(mainwindow.cboSubtitleCodec.SelectedItem)) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            // Return Value
            return sCodec;
        }


        /// <summary>
        /// Video Bitrate Mode (Method)
        /// <summary>
        // For Bitrate Only, Not CRF
        public static String VideoBitrateMode(MainWindow mainwindow)
        {
            string vBitMode = string.Empty;

            if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8") { vBitMode = "-b:v"; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9") { vBitMode = "-b:v"; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264") { vBitMode = "-b:v"; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265") { vBitMode = "-b:v"; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "AV1") { vBitMode = "-b:v"; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4") { vBitMode = "-b:v"; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora") { vBitMode = "-q:v"; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG") { vBitMode = "-q:v"; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "PNG") { vBitMode = string.Empty; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy") { vBitMode = string.Empty; }


            // VBR Toggle Override
            if (mainwindow.tglVideoVBR.IsChecked == true)
            {
                //if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8") { vBitMode = "-q:v"; }
                //else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9") { vBitMode = "-q:v"; }
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4") { vBitMode = "-q:v"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora") { vBitMode = "-q:v"; }
            }

            return vBitMode;
        }



        /// <summary>
        /// Video CBR to VBR
        /// <summary>
        //public static String VideoCBRtoVBR(MainWindow mainwindow, string value)
        //{
        //    // MPEG-4
        //    if (mainwindow.tglVideoVBR.IsChecked == true)
        //    {
        //        if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
        //        {

        //        }
        //    }

        //    return value;
        //}



        /// <summary>
        /// Video Bitrate Calculator (Method)
        /// <summary>
        public static String VideoBitrateCalculator(MainWindow mainwindow, string vEntryType, string inputVideoBitrate)
        {
            // FFprobe values
            //
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
            // Filter out any extra spaces after the first 3 characters IMPORTANT
            //
            else if (inputVideoBitrate.Substring(0, 3) == "N/A")
            {
                inputVideoBitrate = "N/A";
            }

            // If Video has a Bitrate, calculate Bitrate into decimal
            //
            if (inputVideoBitrate != "N/A" && !string.IsNullOrEmpty(inputVideoBitrate))
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


            // If Video Variable = N/A, Calculate Bitate (((Filesize*8)/1000)/Duration)
            // Formats like WebM, MKV and with Missing Metadata can have New Bitrates calculated and applied
            //
            if (inputVideoBitrate == "N/A")
            {
                try // Calculating Bitrate will crash if jpg/png
                {
                    inputVideoBitrate = Convert.ToInt32((double.Parse(FFprobe.inputSize) * 8) / 1000 / double.Parse(FFprobe.inputDuration)).ToString();
                    // convert to int to remove decimals

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


            // WebM Video Bitrate Limiter
            // If input video bitrate is greater than 1.5M, lower the bitrate to 1.5M
            //
            if (MainWindow.outputExt == ".webm" && Convert.ToDouble(inputVideoBitrate) >= 1500)
            {
                inputVideoBitrate = "1500";
            }

            // Round Bitrate, Remove Decimals
            //
            inputVideoBitrate = Math.Round(double.Parse(inputVideoBitrate)).ToString();

            // Add K to end of Bitrate
            //
            inputVideoBitrate = inputVideoBitrate + "K";


            return inputVideoBitrate;
        }


        /// <summary>
        /// BatchVideoQualityAuto (Method)
        /// <summary>
        public static String BatchVideoQualityAuto(MainWindow mainwindow)
        {
            // -------------------------
            // Batch Auto
            // -------------------------
            // Video Bitrate None Check
            // Video Codec None Check
            // Codec Copy Check
            // Media Type Check
            if ((string)mainwindow.cboVideo.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "Copy"
                && (string)mainwindow.cboMediaType.SelectedItem != "Audio")
            {
                // Batch Check
                if (mainwindow.tglBatch.IsChecked == true)
                {
                    // -------------------------
                    // Video Auto Bitrates
                    // -------------------------
                    if ((string)mainwindow.cboVideo.SelectedItem == "Auto")
                    {
                        // Make List
                        List<string> BatchVideoAutoList = new List<string>()
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

                        // vBitrate
                        "\r\n\r\n" + "& for /F \"delims=\" %V in ('@" + FFprobe.ffprobe + " -v error -select_streams v:0 -show_entries " + FFprobe.vEntryTypeBatch + " -of default^=noprint_wrappers^=1:nokey^=1 \"%~f\" 2^>^&1') do (SET vBitrate=%V)",
                        // set %V to %vBitrate%
                        "\r\n\r\n" + "& for /F %V in ('echo %vBitrate%') do (echo)",
                        // auto bitrate calcuate
                        "\r\n\r\n" + "& (if %V EQU N/A (SET /a vBitrate=%S*8/1000/%D*1000) ELSE (echo Video Bitrate Detected))",
                        // set %V to %vBitrate%
                        "\r\n\r\n" + "& for /F %V in ('echo %vBitrate%') do (echo)",
                    };

                        // Join List with Spaces, Remove Empty Strings
                        Video.batchVideoAuto = string.Join(" ", BatchVideoAutoList.Where(s => !string.IsNullOrEmpty(s)));

                    }
                }
            }

            // Return Value
            return batchVideoAuto;
        }


        /// <summary>
        /// Video Quality (Method)
        /// <summary>
        public static String VideoQuality(MainWindow mainwindow)
        {
            // Video Bitrate None Check
            // Video Codec None Check
            // Codec Copy Check
            // Media Type Check
            if ((string)mainwindow.cboVideo.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "Copy"
                && (string)mainwindow.cboMediaType.SelectedItem != "Audio")
            {
                // Video Bitrate Mode
                //
                vBitMode = VideoBitrateMode(mainwindow);


                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Quality: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(mainwindow.cboVideo.SelectedItem)) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);

                // -------------------------
                // Auto
                // -------------------------
                if ((string)mainwindow.cboVideo.SelectedItem == "Auto")
                {
                    // -------------------------
                    // Single
                    // -------------------------
                    if (mainwindow.tglBatch.IsChecked == false)
                    {
                        // -------------------------
                        // Input File Has Video
                        // Input Video Bitrate Not Detected
                        // Input Video Codec Detected
                        // -------------------------
                        if (string.IsNullOrEmpty(FFprobe.inputVideoBitrate) 
                            || FFprobe.inputVideoBitrate == "N/A")
                        {
                            // Codec Detected
                            //
                            if (!string.IsNullOrEmpty(FFprobe.inputVideoCodec))
                            {
                                // 1 Pass / CRF Quality
                                //
                                if ((string)mainwindow.cboPass.SelectedItem == "1 Pass" 
                                    || (string)mainwindow.cboPass.SelectedItem == "CRF")
                                {
                                    // Default to a High value
                                    //
                                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" 
                                        || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
                                    {
                                        crf = "-b:v 0 -crf 16"; //crf value different than x264
                                    }
                                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                                    {
                                        crf = "-crf 18";
                                    }
                                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                                    {
                                        crf = "-x265-params crf=23";
                                    }
                                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "AV1")
                                    {
                                        crf = "-crf 18";
                                    }
                                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
                                    {
                                        crf = "-b:v 5";
                                    }
                                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
                                    {
                                        crf = "-q:v 10"; // Theora can't have Auto Value, default to highest -q:v 10
                                    }

                                    // Remove vBitrate
                                    vBitrate = string.Empty;
                                }

                                // 2 Pass Quality
                                // (Can't use CRF)
                                //
                                else if ((string)mainwindow.cboPass.SelectedItem == "2 Pass")
                                {
                                    // Default to a High value
                                    //
                                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" 
                                        || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
                                    {
                                        vBitrate = "3M"; //crf value different than x264
                                    }
                                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                                    {
                                        vBitrate = "3M";
                                    }
                                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                                    {
                                        vBitrate = "3M";
                                    }
                                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "AV1")
                                    {
                                        vBitrate = "3M";
                                    }
                                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
                                    {
                                        vBitrate = "3M";
                                    }
                                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
                                    {
                                        vBitrate = "10"; // Theora can't have Auto Value, default to highest -q:v 10
                                    }

                                    // Remove CRF
                                    crf = string.Empty;
                                }


                                vOptions = "-pix_fmt yuv420p";
                            }

                            // Codec Not Detected
                            //
                            else
                            {
                                crf = string.Empty;
                                vBitMode = string.Empty;
                                vBitrate = string.Empty;
                                vMinrate = string.Empty;
                                vMaxrate = string.Empty;
                                vBufsize = string.Empty;
                                vOptions = string.Empty;
                            }
                        }

                        // -------------------------
                        // Input File Has Video
                        // Input Video Bitrate Detected
                        // Input Video Codec Detected
                        // -------------------------
                        else if (!string.IsNullOrEmpty(FFprobe.inputVideoBitrate) && FFprobe.inputVideoBitrate != "N/A")
                        {
                            // Codec Detected
                            //
                            if (!string.IsNullOrEmpty(FFprobe.inputVideoCodec))
                            {
                                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8"
                                    || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9"
                                    || (string)mainwindow.cboVideoCodec.SelectedItem == "x264"
                                    || (string)mainwindow.cboVideoCodec.SelectedItem == "x265"
                                    || (string)mainwindow.cboVideoCodec.SelectedItem == "AV1")
                                {
                                    vBitrate = VideoBitrateCalculator(mainwindow, FFprobe.vEntryType, FFprobe.inputVideoBitrate);
                                }
                                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
                                {
                                    // CBR
                                    if (mainwindow.tglVideoVBR.IsChecked == false)
                                    {
                                        vBitrate = VideoBitrateCalculator(mainwindow, FFprobe.vEntryType, FFprobe.inputVideoBitrate);
                                    }
                                    // VBR
                                    else if (mainwindow.tglVideoVBR.IsChecked == true)
                                    {
                                        vBitrate = "5";
                                    }       
                                }
                                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
                                {
                                    vBitrate = "10"; // Theora can't have Auto Value, default to highest -q:v 10
                                }

                                vOptions = "-pix_fmt yuv420p";
                            }

                            // Codec Not Detected
                            //
                            else
                            {
                                crf = string.Empty;
                                vBitMode = string.Empty;
                                vBitrate = string.Empty;
                                vMinrate = string.Empty;
                                vMaxrate = string.Empty;
                                vBufsize = string.Empty;
                                vOptions = string.Empty;
                            }
                        }
                    }

                    // -------------------------
                    // Batch
                    // -------------------------
                    else if (mainwindow.tglBatch.IsChecked == true)
                    {
                        // Use the CMD Batch Video Variable
                        vBitMode = "-b:v";
                        vBitrate = "%V";
                    }

                    // -------------------------
                    // IMAGE
                    // -------------------------
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
                    {
                        crf = string.Empty;
                        vBitrate = "2"; //use highest jpeg quality
                        vMinrate = string.Empty;
                        vMaxrate = string.Empty;
                        vBufsize = string.Empty;
                        vOptions = string.Empty;
                    }
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                    {
                        // png is lossless
                        crf = string.Empty;
                        vBitMode = string.Empty;
                        vBitrate = string.Empty;
                        vMinrate = string.Empty;
                        vMaxrate = string.Empty;
                        vBufsize = string.Empty;
                        vOptions = string.Empty;
                    }
                }

                // -------------------------
                // Lossless
                // -------------------------
                else if ((string)mainwindow.cboVideo.SelectedItem == "Lossless")
                {
                    // VP8 cannot be Lossless

                    // Theora cannot be Lossless

                    // -------------------------
                    // VP9
                    // -------------------------
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-lossless 1";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "-lossless 1";
                        }
   
                        vOptions = "-pix_fmt yuv444p";
                    }
                    // -------------------------
                    // x264
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-qp 0";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "-qp 0"; /* for 2 pass */
                        }
           
                        vOptions = "-pix_fmt yuv444p";
                    }
                    // -------------------------
                    // x265
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-qp 0 -x265-params lossless";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "-qp 0 -x265-params lossless";
                        }
                              
                        vOptions = "-pix_fmt yuv444p";
                    }
                    // -------------------------
                    // AV1
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "AV1")
                    {
                        // Disabled
                    }
                    // -------------------------
                    // mpeg4
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
                    {
                        // CBR
                        if (mainwindow.tglVideoVBR.IsChecked == false)
                        {
                            vBitMode = "-q:v"; // force vbr
                            vBitrate = "2";
                        }
                        // VBR
                        else if (mainwindow.tglVideoVBR.IsChecked == true)
                        {
                            vBitrate = "2";
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // PNG
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                    {
                        // png is lossless
                        crf = string.Empty;
                        vBitMode = string.Empty;
                        vBitrate = string.Empty;
                        vMinrate = string.Empty;
                        vMaxrate = string.Empty;
                        vBufsize = string.Empty;
                        vOptions = string.Empty;
                    }
                }

                // -------------------------
                // Ultra
                // -------------------------
                else if ((string)mainwindow.cboVideo.SelectedItem == "Ultra")
                {
                    // -------------------------
                    // VP8
                    // -------------------------
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitrate = "4M";
                            crf = "-crf 10" /* crf needs b:v 0*/;
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass" 
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "4M";
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // VP9
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitrate = "4M";
                            crf = "-crf 10"/* crf needs b:v 0*/;
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "4M";
                            crf = string.Empty;
                        }
                   
                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // x264
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-crf 16";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "5M";
                            vMinrate = string.Empty;
                            vMaxrate = string.Empty;
                            crf = string.Empty;
                        }
                                                                       
                        vOptions = "-pix_fmt yuv420p -qcomp 0.8";
                    }
                    // -------------------------
                    // x265
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-crf 18 -x265-params crf=18";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "5M";
                            crf = string.Empty;
                        }                       
                        
                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // AV1
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "AV1")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-crf 16";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "5M";
                            vMinrate = string.Empty;
                            vMaxrate = string.Empty;
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // mpeg4
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
                    {
                        // CBR
                        if (mainwindow.tglVideoVBR.IsChecked == false)
                        {
                            vBitrate = "5M";
                        }
                        // VBR
                        else if (mainwindow.tglVideoVBR.IsChecked == true)
                        {
                            vBitrate = "4";
                        }
                       
                        vMinrate = string.Empty;
                        vMaxrate = string.Empty;
                        crf = string.Empty;

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // Theora
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            // OGV uses forced q:v instead of CRF
                            vBitrate = "10";
                            crf = string.Empty;
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "10";
                            crf = string.Empty;
                        }
                  
                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // JPEG
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
                    {
                        vBitrate = "2";
                        crf = string.Empty;
                        vOptions = string.Empty;
                    }
                }

                // -------------------------
                // High
                // -------------------------
                else if ((string)mainwindow.cboVideo.SelectedItem == "High")
                {
                    // -------------------------
                    // VP8
                    // -------------------------
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitrate = "2M";
                            crf = "-crf 12";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "2M";
                            crf = string.Empty;
                        }
                              
                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // VP9
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitrate = "2M";
                            crf = "-crf 12";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "2M";
                            crf = string.Empty;
                        }
                        
                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // x264
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-crf 20";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "2500K";
                            vMinrate = string.Empty;
                            vMaxrate = string.Empty;
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p -qcomp 0.8";
                    }
                    // -------------------------
                    // x265
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-crf 21 -x265-params crf=21";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "2M";
                            crf = string.Empty;
                        }
  
                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // AV1
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "AV1")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-crf 20";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "2500K";
                            vMinrate = string.Empty;
                            vMaxrate = string.Empty;
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // mpeg4
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
                    {
                        // CBR
                        if (mainwindow.tglVideoVBR.IsChecked == false)
                        {
                            vBitrate = "2M";
                        }
                        // VBR
                        else if (mainwindow.tglVideoVBR.IsChecked == true)
                        {
                            vBitrate = "6";
                        }

                        vMinrate = string.Empty;
                        vMaxrate = string.Empty;
                        crf = string.Empty;

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // Theora
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            // OGV uses forced q:v instead of CRF
                            vBitrate = "8";
                            crf = string.Empty;
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "8";
                            crf = string.Empty;
                        }
                                            
                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // JPEG
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
                    {
                        vBitrate = "4";
                        crf = string.Empty;
                        vOptions = string.Empty;
                    }
                }

                // -------------------------
                // Medium
                // -------------------------
                else if ((string)mainwindow.cboVideo.SelectedItem == "Medium")
                {
                    // -------------------------
                    // VP8
                    // -------------------------
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitrate = "1300K";
                            crf = "-crf 16";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "1300K";
                            crf = string.Empty;
                        }
                   
                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // VP9
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitrate = "1300K";
                            crf = "-crf 16";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "1300K";
                            crf = string.Empty;
                        }
   
                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // x264
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-crf 28";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "1300K";
                            vMinrate = string.Empty;
                            vMaxrate = string.Empty;
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // x265
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-crf 26 -x265-params crf=26";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "1300K";
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // AV1
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "AV1")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-crf 28";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "1300K";
                            vMinrate = string.Empty;
                            vMaxrate = string.Empty;
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // mpeg4
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
                    {
                        // CBR
                        if (mainwindow.tglVideoVBR.IsChecked == false)
                        {
                            vBitrate = "1300K";
                        }
                        // VBR
                        else if (mainwindow.tglVideoVBR.IsChecked == true)
                        {
                            vBitrate = "8";
                        }

                        vMinrate = string.Empty;
                        vMaxrate = string.Empty;
                        crf = string.Empty;

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // Theora
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            // OGV uses forced q:v instead of CRF
                            vBitrate = "6";
                            crf = string.Empty;
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "6";
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // JPEG
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
                    {
                        vBitrate = "8";
                        crf = string.Empty;
                        vOptions = string.Empty;
                    }
                }

                // -------------------------
                // Low
                // -------------------------
                else if ((string)mainwindow.cboVideo.SelectedItem == "Low")
                {
                    // -------------------------
                    // VP8
                    // -------------------------
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitrate = "600K";
                            crf = "-crf 20";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "600K";
                            crf = string.Empty;
                        }
                                
                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // VP9
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitrate = "600K";
                            crf = "-crf 20";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "600K";
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // x264
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-crf 37";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "600K";
                            vMinrate = string.Empty;
                            vMaxrate = string.Empty;
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // x265
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-crf 35 -x265-params crf=35";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "600K";
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // AV1
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "AV1")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-crf 37";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "600K";
                            vMinrate = string.Empty;
                            vMaxrate = string.Empty;
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // mpeg4
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
                    {
                        // CBR
                        if (mainwindow.tglVideoVBR.IsChecked == false)
                        {
                            vBitrate = "600K";
                        }
                        // VBR
                        else if (mainwindow.tglVideoVBR.IsChecked == true)
                        {
                            vBitrate = "10";
                        }

                        vMinrate = string.Empty;
                        vMaxrate = string.Empty;
                        crf = string.Empty;

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // Theora
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            // OGV uses forced q:v instead of CRF
                            vBitrate = "4";
                            crf = string.Empty;
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "4";
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // JPEG
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
                    {
                        vBitrate = "15";
                        crf = string.Empty;
                        vOptions = string.Empty;
                    }
                }

                // -------------------------
                // Sub
                // -------------------------
                else if ((string)mainwindow.cboVideo.SelectedItem == "Sub")
                {
                    // -------------------------
                    // VP8
                    // -------------------------
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitrate = "250K";
                            crf = "-crf 25";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "250K";
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // VP9
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitrate = "250K";
                            crf = "-crf 25";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "250K";
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // x264
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-crf 45";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "250K";
                            vMinrate = string.Empty;
                            vMaxrate = string.Empty;
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // x265
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-crf 42 -x265-params crf=42";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "250K";
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // AV1
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "AV1")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            vBitMode = string.Empty;
                            vBitrate = string.Empty;
                            crf = "-crf 45";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "250K";
                            vMinrate = string.Empty;
                            vMaxrate = string.Empty;
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // mpeg4
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
                    {
                        // CBR
                        if (mainwindow.tglVideoVBR.IsChecked == false)
                        {
                            vBitrate = "250K";
                        }
                        // VBR
                        else if (mainwindow.tglVideoVBR.IsChecked == true)
                        {
                            vBitrate = "12";
                        }

                        vMinrate = string.Empty;
                        vMaxrate = string.Empty;
                        crf = string.Empty;

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // Theora
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
                    {
                        if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                        {
                            // OGV uses forced q:v instead of CRF
                            vBitrate = "2";
                            crf = string.Empty;
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "2";
                            crf = string.Empty;
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // JPEG
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
                    {
                        vBitrate = "25";
                        crf = string.Empty;
                        vOptions = string.Empty;
                    }
                }

                // -------------------------
                // Custom
                // -------------------------
                else if ((string)mainwindow.cboVideo.SelectedItem == "Custom")
                {
                    // -------------------------
                    // Bitrate
                    // -------------------------
                    // Textbox Default or Empty
                    if (string.IsNullOrWhiteSpace(mainwindow.vBitrateCustom.Text))
                    {
                        vBitMode = string.Empty;
                        vBitrate = string.Empty;
                    }
                    // Textbox Not Empty
                    else if (!string.IsNullOrWhiteSpace(mainwindow.vBitrateCustom.Text))
                    {
                        vBitrate = mainwindow.vBitrateCustom.Text.ToString();
                    }

                    // -------------------------
                    // Minrate
                    // -------------------------
                    // Textbox Default or Empty
                    if (string.IsNullOrWhiteSpace(mainwindow.vMinrateCustom.Text))
                    {
                        vMinrate = string.Empty;
                    }
                    // Textbox Not Empty
                    else if (!string.IsNullOrWhiteSpace(mainwindow.vMinrateCustom.Text))
                    {
                        vMinrate = "-minrate " + mainwindow.vMinrateCustom.Text.ToString();
                    }

                    // -------------------------
                    // Maxrate
                    // -------------------------
                    // Textbox Default or Empty
                    if (string.IsNullOrWhiteSpace(mainwindow.vMaxrateCustom.Text))
                    {
                        vMaxrate = string.Empty;
                    }
                    // Textbox Not Empty
                    else if (!string.IsNullOrWhiteSpace(mainwindow.vMaxrateCustom.Text))
                    {
                        vMaxrate = "-maxrate " + mainwindow.vMaxrateCustom.Text.ToString();
                    }

                    // -------------------------
                    // Bufsize
                    // -------------------------
                    // Textbox Default or Empty
                    if (string.IsNullOrWhiteSpace(mainwindow.vBufsizeCustom.Text))
                    {
                        vBufsize = string.Empty;
                    }
                    // Textbox Not Empty
                    else if (!string.IsNullOrWhiteSpace(mainwindow.vBufsizeCustom.Text))
                    {
                        vBufsize = "-bufsize " + mainwindow.vBufsizeCustom.Text.ToString();
                    }

                    // -------------------------
                    // CRF
                    // -------------------------
                    // Textbox Default or Empty
                    if (string.IsNullOrWhiteSpace(mainwindow.crfCustom.Text))
                    {
                        crf = string.Empty;
                    }
                    // Textbox Not Empty
                    else if (!string.IsNullOrWhiteSpace(mainwindow.crfCustom.Text))
                    {
                        // x264
                        crf = "-crf " + mainwindow.crfCustom.Text; // crf needs b:v 0

                        // x265
                        if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                        {
                            crf = "-crf " + mainwindow.crfCustom.Text + " -x265-params crf=" + mainwindow.crfCustom.Text;
                        }
                    }

                    // -------------------------
                    // VP9 crf -b:v 0
                    // -------------------------
                    // vBitrate is Default or Empty 
                    // & CRF is Custom value
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
                    {
                        if (string.IsNullOrWhiteSpace(mainwindow.vBitrateCustom.Text))
                        {
                            if (!string.IsNullOrWhiteSpace(mainwindow.crfCustom.Text))
                            {
                                vBitrate = "0";
                            }
                        }
                    }

                    // -------------------------
                    // Pix Format
                    // -------------------------
                    vOptions = "-pix_fmt yuv420p";

                }

                // -------------------------
                // None
                // -------------------------
                else if ((string)mainwindow.cboVideo.SelectedItem == "None")
                {
                    crf = string.Empty;
                    vBitMode = string.Empty;
                    vBitrate = string.Empty;
                    vMinrate = string.Empty;
                    vMaxrate = string.Empty;
                    vBufsize = string.Empty;
                    vOptions = string.Empty;
                }

                // -------------------------
                // Combine
                // -------------------------
                List<string> vQualityArgs = new List<string>();

                // CRF
                if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                {
                    vQualityArgs = new List<string>()
                    {
                        vBitMode,
                        vBitrate,
                        vMinrate,
                        vMaxrate,
                        vBufsize,
                        crf,
                        vOptions
                    };
                }

                // 1 Pass, 2 Pass, auto
                else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass" 
                    || (string)mainwindow.cboPass.SelectedItem == "2 Pass"
                    || (string)mainwindow.cboPass.SelectedItem == "auto")
                {
                    vQualityArgs = new List<string>()
                    {
                        vBitMode,
                        vBitrate,
                        vMinrate,
                        vMaxrate,
                        vBufsize,
                        vOptions
                    };
                }


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
                    if (!string.IsNullOrEmpty(crf))
                    {
                        Log.logParagraph.Inlines.Add(new Run(crf) { Foreground = Log.ConsoleDefault }); //crf combines with bitrate
                    }
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Options: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(vOptions) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);

            } // end Null Check

            // Return Value
            return vQuality;

        } // end VideoQuality


        /// <summary>
        /// Pass 1 Modifier (Method)
        /// <summary>
        // x265 Pass 1
        public static String Pass1Modifier(MainWindow mainwindow)
        {
            // -------------------------
            // Enabled
            // -------------------------
            if ((string)mainwindow.cboPass.SelectedItem == "2 Pass")
            {
                // Enable pass parameters in the FFmpeg Arguments
                // x265 Pass 2 Params
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                {
                    Video.pass1 = "-x265-params pass=1";
                }
                // All other codecs
                else
                {
                    Video.pass1 = "-pass 1";
                }
            }

            // -------------------------
            // Disabled
            // -------------------------
            else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass" 
                || (string)mainwindow.cboPass.SelectedItem == "CRF"
                || (string)mainwindow.cboPass.SelectedItem == "auto") //jpg/png)
            {
                Video.pass1 = string.Empty;
            }


            // Return Value
            return Video.pass1;
        }


        /// <summary>
        /// Pass 2 Modifier (Method)
        /// <summary>
        // x265 Pass 2
        public static String Pass2Modifier(MainWindow mainwindow)
        {
            // -------------------------
            // Enabled
            // -------------------------
            if ((string)mainwindow.cboPass.SelectedItem == "2 Pass")
            {
                // Enable pass parameters in the FFmpeg Arguments
                // x265 Pass 2 Params
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                {
                    Video.pass2 = "-x265-params pass=2";
                }
                // All other codecs
                else
                {
                    Video.pass2 = "-pass 2";
                }
            }

            // -------------------------
            // Disabled
            // -------------------------
            else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass" 
                || (string)mainwindow.cboPass.SelectedItem == "CRF"
                || (string)mainwindow.cboPass.SelectedItem == "auto") //jpg/png
            {
                Video.pass2 = string.Empty;
            }


            // Return Value
            return Video.pass2;
        }


        /// <summary>
        /// Size Width Auto
        /// <summary>
        public static void SizeWidthAuto(MainWindow mainwindow)
        {
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "AV1"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
            {
                width = "-1";
            }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "x265"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
            {
                width = "-2";
            }
        }

        /// <summary>
        /// Size Height Auto
        /// <summary>
        public static void SizeHeightAuto(MainWindow mainwindow)
        {
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "AV1"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
            {
                height = "-1";
            }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "x265"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
            {
                height = "-2";
            }
        }

        /// <summary>
        /// Size (Method)
        /// <summary>
        // Size is a Filter
        public static void Size(MainWindow mainwindow)
        {
            // -------------------------
            // No
            // -------------------------
            if ((string)mainwindow.cboSize.SelectedItem == "Source")
            {
                // MP4/MKV Width/Height Fix
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" 
                    || (string)mainwindow.cboVideoCodec.SelectedItem == "x265"
                    || (string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
                {
                    width = "trunc(iw/2)*2";
                    height = "trunc(ih/2)*2";

                    // -------------------------
                    // Combine & Add Aspect Filter
                    // -------------------------
                    //combine
                    aspect = "scale=" + width + ":" + height;

                    // Video Filter Add
                    VideoFilters.Add(aspect);
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
                if ((string)mainwindow.cboSize.SelectedItem == "8K")
                {
                    // Width
                    width = "7680"; // Note: 8K is measured width first

                    // Height
                    SizeHeightAuto(mainwindow);
                }
                // -------------------------
                // 4K
                // -------------------------
                else if ((string)mainwindow.cboSize.SelectedItem == "4K")
                {
                    // Width
                    width = "4096"; // Note: 4K is measured width first

                    // Height
                    SizeHeightAuto(mainwindow);
                }
                // -------------------------
                // 4K UHD
                // -------------------------
                else if ((string)mainwindow.cboSize.SelectedItem == "4K UHD")
                {
                    // Width
                    width = "3840"; // Note: 4K is measured width first

                    // Height
                    SizeHeightAuto(mainwindow);

                }
                // -------------------------
                // 2K
                // -------------------------
                else if ((string)mainwindow.cboSize.SelectedItem == "2K")
                {
                    // Width
                    width = "2048"; // Note: 2K is measured width first

                    // Height
                    SizeHeightAuto(mainwindow);
                }
                // -------------------------
                // 1440p
                // -------------------------
                else if ((string)mainwindow.cboSize.SelectedItem == "1440p")
                {
                    // Width
                    SizeWidthAuto(mainwindow);

                    // Height
                    height = "1440";
                }
                // -------------------------
                // 1200p
                // -------------------------
                else if ((string)mainwindow.cboSize.SelectedItem == "1200p")
                {
                    // Width
                    SizeWidthAuto(mainwindow);

                    // Height
                    height = "1200";
                }
                // -------------------------
                // 1080p
                // -------------------------
                else if ((string)mainwindow.cboSize.SelectedItem == "1080p")
                {
                    // Width
                    SizeWidthAuto(mainwindow);

                    // Height
                    height = "1080";
                }
                // -------------------------
                // 720p
                // -------------------------
                else if ((string)mainwindow.cboSize.SelectedItem == "720p")
                {
                    // Width
                    SizeWidthAuto(mainwindow);

                    // Height
                    height = "720";
                }
                // -------------------------
                // 480p
                // -------------------------
                else if ((string)mainwindow.cboSize.SelectedItem == "480p")
                {
                    // Width
                    SizeWidthAuto(mainwindow);

                    // Height
                    height = "480";

                }
                // -------------------------
                // 320p
                // -------------------------
                else if ((string)mainwindow.cboSize.SelectedItem == "320p")
                {
                    // Width
                    SizeWidthAuto(mainwindow);

                    // Height
                    height = "320";
                }
                // -------------------------
                // 240p
                // -------------------------
                else if ((string)mainwindow.cboSize.SelectedItem == "240p")
                {
                    // Width
                    SizeWidthAuto(mainwindow);

                    // Height
                    height = "240";
                }
                // -------------------------
                // Custom Size
                // -------------------------
                else if ((string)mainwindow.cboSize.SelectedItem == "Custom")
                {
                    // Get width height from custom textbox
                    width = mainwindow.widthCustom.Text;
                    height = mainwindow.heightCustom.Text;

                    // Change the left over Default empty text to "auto"
                    if (string.Equals(mainwindow.widthCustom.Text, "", StringComparison.CurrentCultureIgnoreCase))
                    {
                        mainwindow.widthCustom.Text = "auto";
                    }

                    if (string.Equals(mainwindow.heightCustom.Text, "", StringComparison.CurrentCultureIgnoreCase))
                    {
                        mainwindow.heightCustom.Text = "auto";
                    }

                    // -------------------------
                    // VP8, VP9, Theora
                    // -------------------------
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8"
                        || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9"
                        || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora"
                        || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG"
                        || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                    {
                        // If User enters "auto" or textbox is empty
                        if (string.Equals(mainwindow.widthCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                        {
                            width = "-1";
                        }
                        if (string.Equals(mainwindow.heightCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                        {
                            height = "-1";
                        }
                    } //end vp8/vp9/theora


                    // -------------------------
                    // x264 & x265
                    // -------------------------
                    // Fix FFmpeg MP4 but (User entered value)
                    // Apply Fix to all scale effects above
                    //
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" 
                        || (string)mainwindow.cboVideoCodec.SelectedItem == "x265"
                        || (string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
                    {
                        // -------------------------
                        // Width = Custom value
                        // Height = Custom value
                        // -------------------------
                        if (!string.Equals(mainwindow.widthCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase)
                            && !string.Equals(mainwindow.heightCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                        {
                            // Aspect Must be Cropped to be divisible by 2
                            // e.g. -vf "scale=777:777, crop=776:776:0:0"
                            //
                            try
                            {
                                // Only if Crop is already Empty
                                // User Defined Crop should always override Divisible Crop
                                // CropClearButton ~ is used as an Identifier, Divisible Crop does not leave "~"
                                //
                                if (mainwindow.buttonCropClearTextBox.Text == "") // Crop Set Check
                                {
                                    // Temporary Strings
                                    // So not to Override User Defined Crop
                                    int divisibleCropWidth = Convert.ToInt32(width);
                                    int divisibleCropHeight = Convert.ToInt32(height);
                                    string cropX = "0";
                                    string cropY = "0";

                                    // int convert check
                                    if (Int32.TryParse(width, out divisibleCropWidth)
                                        && Int32.TryParse(height, out divisibleCropHeight))
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
                                MainWindow.ready = false;
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
                        else if (string.Equals(mainwindow.widthCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase)
                            && !string.Equals(mainwindow.heightCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
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
                                MainWindow.ready = false;
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
                        else if (!string.Equals(mainwindow.widthCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase)
                            && string.Equals(mainwindow.heightCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
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
                                MainWindow.ready = false;
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
                        else if (string.Equals(mainwindow.widthCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase)
                            && string.Equals(mainwindow.heightCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                        {
                            // If User enters "auto" or textbox is empty
                            if (string.Equals(mainwindow.widthCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                            {
                                width = "trunc(iw/2)*2";

                            }
                            if (string.Equals(mainwindow.heightCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                            {
                                height = "trunc(ih/2)*2";
                            }
                        }

                    } //end x264 & x265

                } //end custom


                // -------------------------
                // Combine & Add Aspect Filter
                // -------------------------
                //combine
                aspect = "scale=" + width + ":" + height;

                // Video Filter Add
                VideoFilters.Add(aspect);

            } //end Yes


            // -------------------------
            // Filter Clear
            // -------------------------
            // Copy
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy")
            {
                aspect = string.Empty;

                // Video Filter Add
                if (VideoFilters != null)
                {
                    VideoFilters.Clear();
                    VideoFilters.TrimExcess();
                }
            }


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Resize: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(mainwindow.cboSize.SelectedItem.ToString()) { Foreground = Log.ConsoleDefault });
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
        /// Scaling Algorithm
        /// <summary>
        public static String ScalingAlgorithm(MainWindow mainwindow)
        {
            string algorithm = string.Empty;

            // None & Default
            //
            if ((string)mainwindow.cboScaling.SelectedItem == "None"
                || (string)mainwindow.cboScaling.SelectedItem == "Default")
            {
                algorithm = string.Empty;
            }

            // Scaler
            //
            else
            {
                algorithm = "-sws_flags " + mainwindow.cboScaling.SelectedItem.ToString();
            }

            return algorithm;
        }
        //public static String ScalingAlgorithm(MainWindow mainwindow)
        //{
        //    string algorithm = string.Empty;

        //    // None & Default
        //    //
        //    if ((string)mainwindow.cboScaling.SelectedItem == "None"
        //        || (string)mainwindow.cboScaling.SelectedItem == "Default")
        //    {
        //        algorithm = string.Empty;
        //    }

        //    // Scaler
        //    //
        //    else
        //    {
        //        algorithm = "-sws_flags " + mainwindow.cboScaling.SelectedItem.ToString();
        //    }

        //    return algorithm;
        //}


        /// <summary>
        /// Crop (Method)
        /// <summary>
        public static void Crop(MainWindow mainwindow, CropWindow cropwindow)
        {
            // -------------------------
            // Clear
            // -------------------------
            // Clear leftover Divisible Crop if not x264/x265
            // CropClearButton is used as an Identifier, Divisible Crop does not leave "~"
            if ((string)mainwindow.cboVideoCodec.SelectedItem != "x264"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "x265"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "mpeg4"
                && mainwindow.buttonCropClearTextBox.Text == "Clear"
                //&& string.IsNullOrWhiteSpace(mainwindow.buttonCropClearTextBox.Text)
                )
            {
                CropWindow.crop = string.Empty;
            }

            // Clear Crop if MediaType is Audio
            if ((string)mainwindow.cboMediaType.SelectedItem == "Audio")
            {
                CropWindow.crop = string.Empty;
            }

            // -------------------------
            // Halt
            // -------------------------
            // Crop Codec Copy Check
            // Switch Copy to Codec to avoid error
            if (!string.IsNullOrEmpty(CropWindow.crop) && (string)mainwindow.cboVideoCodec.SelectedItem == "Copy") //null check
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Warning: Crop cannot use Codec Copy. Please select a Video Codec.")) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);

                /* lock */
                MainWindow.ready = false;
                // Warning
                MessageBox.Show("Crop cannot use Codec Copy. Please select a Video Codec.",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
            }

            // -------------------------
            // Add Crop to Video Filters if Not Null
            // -------------------------
            // If Crop is set by User in the CropWindow
            if (!string.IsNullOrEmpty(CropWindow.crop))
            {
                // Video Filters Add
                VideoFilters.Add(CropWindow.crop);
            }
        }


        /// <summary>
        /// FPS (Method)
        /// <summary>
        public static String FPS(MainWindow mainwindow)
        {
            // Video Bitrate None Check
            // Video Codec None Check
            // Codec Copy Check
            // Media Type Check
            if ((string)mainwindow.cboVideo.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "Copy"
                && (string)mainwindow.cboMediaType.SelectedItem != "Audio")
            {
                if ((string)mainwindow.cboFPS.SelectedItem == "auto")
                {
                    fps = string.Empty;
                }
                else
                {
                    fps = "-r " + mainwindow.cboFPS.Text;
                }

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("FPS: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(mainwindow.cboFPS.Text.ToString()) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            return fps;
        }


        /// <summary>
        /// Images (Method)
        /// <summary>
        public static String Images(MainWindow mainwindow)
        {
            // Video Bitrate None Check
            // Video Codec None Check
            // Codec Copy Check
            // Media Type Check
            if ((string)mainwindow.cboVideo.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "Copy"
                && (string)mainwindow.cboMediaType.SelectedItem != "Audio")
            {
                if ((string)mainwindow.cboMediaType.SelectedItem == "Image")
                {
                    image = "-vframes 1"; //important
                }
                else if ((string)mainwindow.cboMediaType.SelectedItem == "Sequence")
                {
                    image = string.Empty; //disable -vframes
                }
                else
                {
                    image = string.Empty;
                }
            }

            return image;
        }


        /// <summary>
        /// Subtitles (Method)
        /// <summary>
        public static String Subtitles(MainWindow mainwindow)
        {
            string subtitles = string.Empty;

            // -------------------------
            // External
            // -------------------------
            if ((string)mainwindow.cboSubtitle.SelectedItem == "external"
                && subtitleFilePathsList != null 
                && subtitleFilePathsList.Count > 0)
            {
                subtitles = "-i " + string.Join(" \r\n\r\n-i ", subtitleFilePathsList
                                          .Where(s => !string.IsNullOrEmpty(s))
                                          );
            }

            return subtitles;
        }


        /// <summary>
        /// Subtitles Style Filter (Method)
        /// <summary>
        public static void SubtitlesStyleFilter(MainWindow mainwindow)
        {
            string style = string.Empty;

            // -------------------------
            // External
            // -------------------------
            if ((string)mainwindow.cboSubtitle.SelectedItem == "external"
                && subtitleFileNamesList.Count > 0)
            {
                //// Join File Names List
                //string files = string.Join(",", subtitleFileNamesList.Where(s => !string.IsNullOrEmpty(s)));

                //// Create Subtitles Filter
                //string subtitles = "subtitles=" + files + ":force_style='FontName=Arial,FontSize=22'" + style;

                //// Add to Filters List
                //VideoFilters.Add(subtitles);
            }
        }


        /// <summary>
        /// Speed (Method)
        /// <summary>
        public static String Speed(MainWindow mainwindow)
        {
            // Video Bitrate None Check
            // Video Codec None Check
            // Codec Copy Check
            // Media Type Check
            if ((string)mainwindow.cboVideo.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "Copy"
                && (string)mainwindow.cboMediaType.SelectedItem != "Audio")
            {
                // -------------------------
                // x264 / x265
                // -------------------------
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" 
                    || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                {
                    if ((string)mainwindow.cboSpeed.SelectedItem == "None") { speed = string.Empty; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Placebo") { speed = "-preset placebo"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Very Slow") { speed = "-preset veryslow"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Slower") { speed = "-preset slower"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Slow") { speed = "-preset slow"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Medium") { speed = "-preset medium"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Fast") { speed = "-preset fast"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Faster") { speed = "-preset faster"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Very Fast") { speed = "-preset veryfast"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Super Fast") { speed = "-preset superfast"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Ultra Fast") { speed = "-preset ultrafast"; }
                }

                // -------------------------
                // AV1
                // -------------------------  
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "AV1")
                {
                    if ((string)mainwindow.cboSpeed.SelectedItem == "None") { speed = string.Empty; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Placebo") { speed = "-cpu-used 0"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Very Slow") { speed = "-cpu-used 0"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Slower") { speed = "-cpu-used 0"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Slow") { speed = "-cpu-used 0"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Medium") { speed = "-cpu-used 1"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Fast") { speed = "-cpu-used 1"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Faster") { speed = "-cpu-used 2"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Very Fast") { speed = "-cpu-used 3"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Super Fast") { speed = "-cpu-used 4"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Ultra Fast") { speed = "-cpu-used 5"; }
                }

                // -------------------------
                // MPEG-4
                // -------------------------             
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
                {
                    // Does not use speed presets
                    speed = string.Empty;
                }

                // -------------------------
                // VP8
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8")
                {
                    if ((string)mainwindow.cboSpeed.SelectedItem == "None") { speed = string.Empty; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Placebo") { speed = "-quality best -cpu-used 0"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Very Slow") { speed = "-quality good -cpu-used 0"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Slower") { speed = "-quality good -cpu-used 0"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Slow") { speed = "-quality good -cpu-used 0"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Medium") { speed = "-quality good -cpu-used 0"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Fast") { speed = "-quality good -cpu-used 1"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Faster") { speed = "-quality good -cpu-used 2"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Very Fast") { speed = "-quality realtime -cpu-used 3"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Super Fast") { speed = "-quality realtime -cpu-used 4"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Ultra Fast") { speed = "-quality realtime -cpu-used 5"; }
                }

                // -------------------------
                // VP9
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
                {
                    if ((string)mainwindow.cboSpeed.SelectedItem == "None") { speed = string.Empty; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Placebo") { speed = "-speed -8"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Very Slow") { speed = "-speed -4"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Slower") { speed = "-speed -2"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Slow") { speed = "-speed 0"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Medium") { speed = "-speed 1"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Fast") { speed = "-speed 2"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Faster") { speed = "-speed 3"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Very Fast") { speed = "-speed 4"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Super Fast") { speed = "-speed 5"; }
                    else if ((string)mainwindow.cboSpeed.SelectedItem == "Ultra Fast") { speed = "-speed 6"; }
                }

                // -------------------------
                // Theora
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
                {
                    // Does not use speed presets
                    speed = string.Empty;
                }

                // -------------------------
                // JPEG & PNG
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG" 
                    || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                {
                    // Does not use speed presets
                    speed = string.Empty;
                }

                // -------------------------
                // None (No Codec)
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "None"
                    || string.IsNullOrEmpty((string)mainwindow.cboVideoCodec.SelectedItem))
                {
                    // Does not use speed presets
                    speed = string.Empty;
                }

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Encoding Speed: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(mainwindow.cboSpeed.Text.ToString()) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }


            // Return Value
            return speed;

        } // End Speed


        /// <summary>
        /// Optimize (Method)
        /// <summary>
        public static String Optimize(MainWindow mainwindow)
        {
            // Video Bitrate None Check
            // Video Codec None Check
            // Codec Copy Check
            // Media Type Check
            if ((string)mainwindow.cboVideo.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "Copy"
                && (string)mainwindow.cboMediaType.SelectedItem != "Audio")
            {
                // Tune, Profile, Level set in MainWindow cboOptimize_SelectionChanged
                //

                // -------------------------
                // x264
                // -------------------------
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                {
                    // -------------------------
                    // Tune
                    // -------------------------
                    if ((string)mainwindow.cboOptTune.SelectedItem == "none")
                    {
                        optTune = string.Empty;
                    }
                    else
                    {
                        optTune = "-tune " + mainwindow.cboOptTune.SelectedItem.ToString();
                    }

                    // -------------------------
                    // Profile
                    // -------------------------
                    if ((string)mainwindow.cboOptProfile.SelectedItem == "none")
                    {
                        optProfile = string.Empty;
                    }
                    else
                    {
                        optProfile = "-profile:v " + mainwindow.cboOptProfile.SelectedItem.ToString();
                    }

                    // -------------------------
                    // Level
                    // -------------------------
                    if ((string)mainwindow.cboOptLevel.SelectedItem == "none")
                    {
                        optLevel = string.Empty;
                    }
                    else
                    {
                        optLevel = "-level " + mainwindow.cboOptLevel.SelectedItem.ToString();
                    }

                    // -------------------------
                    // Combine Optimize = Tune + Profile + Level
                    // -------------------------
                    List<string> v2passList = new List<string>() {
                        optProfile,
                        optLevel,
                        optTune,
                        optFlags
                    };

                    optimize = string.Join(" ", v2passList.Where(s => !string.IsNullOrEmpty(s)));
                }

                // -------------------------
                // x265
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                {
                    // -------------------------
                    // Tune
                    // -------------------------
                    if ((string)mainwindow.cboOptTune.SelectedItem == "none")
                    {
                        optTune = string.Empty;
                    }
                    else
                    {
                        optTune = "-tune " + mainwindow.cboOptTune.SelectedItem.ToString();
                    }
                }

                // -------------------------
                // All Other Codecs
                // -------------------------
                else
                {
                    optimize = optFlags;
                }

            }

            // Return Value
            return optimize;

        } // End Optimize



        /// <summary>
        /// Frame Rate To Decimal (Method)
        /// <summary>
        // When using Video Frame Range instead of Time
        public static void FramesToDecimal(MainWindow mainwindow) //method
        {
            // Video Bitrate None Check
            // Video Codec None Check
            // Codec Copy Check
            // Media Type Check
            if ((string)mainwindow.cboVideo.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "Copy"
                && (string)mainwindow.cboMediaType.SelectedItem != "Audio")
            {
                // Separate FFprobe Result (e.g. 30000/1001)
                string[] f = FFprobe.inputFrameRate.Split('/');

                try
                {
                    double detectedFramerate = Convert.ToDouble(f[0]) / Convert.ToDouble(f[1]); // divide FFprobe values
                    detectedFramerate = Math.Truncate(detectedFramerate * 1000) / 1000; // limit to 3 decimal places

                    // Trim Start Frame
                    //
                    if (mainwindow.frameStart.Text != "Frame"
                        && !string.IsNullOrWhiteSpace(mainwindow.frameStart.Text)
                        && !string.IsNullOrWhiteSpace(mainwindow.frameStart.Text)) // Default/Null Check
                    {
                        Format.trimStart = Convert.ToString(Convert.ToDouble(mainwindow.frameStart.Text) / detectedFramerate); // Divide Frame Start Number by Video's Framerate
                    }

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Start Frame: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run(mainwindow.frameStart.Text + " / " + detectedFramerate + " = " + Format.trimStart) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);

                    // Trim End Frame
                    //
                    if (mainwindow.frameEnd.Text != "Range"
                        && !string.IsNullOrWhiteSpace(mainwindow.frameEnd.Text)
                        && !string.IsNullOrWhiteSpace(mainwindow.frameEnd.Text)) // Default/Null Check
                    {
                        Format.trimEnd = Convert.ToString(Convert.ToDouble(mainwindow.frameEnd.Text) / detectedFramerate); // Divide Frame End Number by Video's Framerate
                    }

                    // Log Console Message /////////
                    if (mainwindow.frameEnd.IsEnabled == true)
                    {
                        Log.WriteAction = () =>
                        {
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new Bold(new Run("End Frame: ")) { Foreground = Log.ConsoleDefault });
                            Log.logParagraph.Inlines.Add(new Run(mainwindow.frameEnd.Text + " / " + detectedFramerate + " = " + Format.trimEnd) { Foreground = Log.ConsoleDefault });
                        };
                        Log.LogActions.Add(Log.WriteAction);
                    }

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
                    MainWindow.ready = false;
                    // Warning
                    MessageBox.Show("No input file or Framerate not detected.",
                                        "Notice",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                }
            }

        }


        /// <summary>
        /// Video Filter Combine (Method)
        /// <summary>
        public static String VideoFilter(MainWindow mainwindow)
        {
            // Video Bitrate None Check
            // Video Codec None Check
            // Codec Copy Check
            // Media Type Check
            if ((string)mainwindow.cboVideo.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "Copy"
                && (string)mainwindow.cboMediaType.SelectedItem != "Audio")
            {
                // --------------------------------------------------
                // Filters
                // --------------------------------------------------
                /// <summary>
                ///    Resize
                /// </summary> 
                Video.Size(mainwindow);

                /// <summary>
                ///    Crop
                /// </summary> 
                Video.Crop(mainwindow, cropwindow);

                /// <summary>
                ///    Subtitles Style
                /// </summary> 
                Video.SubtitlesStyleFilter(mainwindow);


                // -------------------------
                // PNG to JPEG
                // -------------------------
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
                {
                    // Turn on PNG to JPG Filter
                    if (string.Equals(MainWindow.inputExt, ".png", StringComparison.CurrentCultureIgnoreCase)
                        || string.Equals(MainWindow.inputExt, "png", StringComparison.CurrentCultureIgnoreCase))
                    {
                        //png transparent to white background
                        geq = "format=yuva444p,geq='if(lte(alpha(X,Y),16),255,p(X,Y))':'if(lte(alpha(X,Y),16),128,p(X,Y))':'if(lte(alpha(X,Y),16),128,p(X,Y))'";

                        // Video Filter Add
                        VideoFilters.Add(geq);
                    }
                    else
                    {
                        geq = string.Empty;
                    }
                }

                // -------------------------
                // Filter Combine
                // -------------------------
                if ((string)mainwindow.cboVideoCodec.SelectedItem != "None") // None Check
                {
                    // 1 Filter
                    //
                    if (VideoFilters.Count() == 1)
                    {
                        vFilter = "-vf " + string.Join(", ", VideoFilters.Where(s => !string.IsNullOrEmpty(s)));
                    }

                    // Multiple Filters
                    //
                    else if (VideoFilters.Count() > 1)
                    {
                        vFilter = "-vf \"" + string.Join(", ", VideoFilters.Where(s => !string.IsNullOrEmpty(s))) + "\"";
                    }

                    // Empty
                    //
                    else
                    {
                        vFilter = string.Empty;
                    }
                }
                // Video Codec None
                else
                {
                    vFilter = string.Empty;

                }
            }

            // Return Value
            return vFilter;
        }

    }
}