/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017 Matt McManis
http://github.com/MattMcManis/Axiom
http://axiomui.github.io
axiom.interface@gmail.com

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
using System.Linq;
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
        public static string vMaxrate;
        public static string vBufsize;
        public static string vOptions; // -pix_fmt, -qcomp
        public static string crf; // Constant Rate Factor
        public static string fps; // Frames Per Second
        public static string image; // JPEG & PNG options
        public static string optTune; // x264 & x265 tuning modes
        public static string optProfile; // x264/x265 Profile
        public static string optLevel; // x264/x265 Level
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

        // Filter
        public static CropWindow cropwindow;
        public static List<string> VideoFilters = new List<string>(); // Filters to String Join
        public static string geq; // png transparent to jpg whtie background filter
        public static string vFilter;

        // Batch
        public static string batchVideoAuto;



        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Process Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Video Codecs (Method)
        /// <summary>
        public static String VideoCodec(MainWindow mainwindow)
        {
            // Video Bitrate None Check
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
                // Theora
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
                {
                    vCodec = "-c:v libtheora";
                }
                // x254
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                {
                    vCodec = "-c:v libx264"; //leave profile:v main here so MKV can choose other ???
                }
                //x265
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                {
                    vCodec = "-c:v libx265"; //does not use profile:v
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
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora") { vBitMode = "-q:v"; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG") { vBitMode = "-q:v"; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "PNG") { vBitMode = string.Empty; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy") { vBitMode = string.Empty; }

            return vBitMode;
        }



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
                        if (string.IsNullOrEmpty(FFprobe.inputVideoBitrate) || FFprobe.inputVideoBitrate == "N/A")
                        {
                            // Codec Detected
                            //
                            if (!string.IsNullOrEmpty(FFprobe.inputVideoCodec))
                            {
                                // 1 Pass / CRF Quality
                                //
                                if ((string)mainwindow.cboPass.SelectedItem == "1 Pass" || (string)mainwindow.cboPass.SelectedItem == "CRF")
                                {
                                    // Default to a High value
                                    //
                                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
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
                                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
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
                                    || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                                {
                                    vBitrate = VideoBitrateCalculator(mainwindow, FFprobe.vEntryType, FFprobe.inputVideoBitrate);
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

                    // VP9
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
                    // x264
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
                    // x265
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
                    // PNG
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                    {
                        // png is lossless
                        crf = string.Empty;
                        vBitMode = string.Empty;
                        vBitrate = string.Empty;
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
                            vMaxrate = "-maxrate 5M";
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
                            crf = "-crf 20 -x265-params crf=20";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "5M";
                        }                       
                        
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
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "10";
                        }
                  
                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // JPEG
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
                    {
                        crf = string.Empty;
                        vBitrate = "2";
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
                            vMaxrate = "-maxrate 2500K";
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
                            crf = "-crf 25 -x265-params crf=25";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "2M" /* for 2 pass */;
                        }
  
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
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "8";
                        }
                                            
                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // JPEG
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
                    {
                        crf = string.Empty;
                        vBitrate = "4";
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
                            vBitrate = "1300K" /* for 2 pass */;
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
                            vMaxrate = "-maxrate 1300K";
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
                            crf = "-crf 30 -x265-params crf=30";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "1300K";
                        }

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
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "6";
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // JPEG
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
                    {
                        crf = string.Empty;
                        vBitrate = "8";
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
                            vMaxrate = "-maxrate 600K";
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
                            crf = "-crf 38 -x265-params crf=38";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "600K";
                        }

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
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "4";
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // JPEG
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
                    {
                        crf = string.Empty;
                        vBitrate = "15";
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
                            vMaxrate = "-maxrate 250K";
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
                            crf = "-crf 45 -x265-params crf=45";
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "250K";
                        }

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
                        }
                        else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                            || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                        {
                            vBitrate = "2";
                        }

                        vOptions = "-pix_fmt yuv420p";
                    }
                    // -------------------------
                    // JPEG
                    // -------------------------
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
                    {
                        crf = string.Empty;
                        vBitrate = "25";
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
                    if (mainwindow.vBitrateCustom.Text == "Bitrate" 
                        || string.IsNullOrWhiteSpace(mainwindow.vBitrateCustom.Text))
                    {
                        crf = string.Empty;
                        vBitMode = string.Empty;
                        vBitrate = string.Empty;
                        vMaxrate = string.Empty;
                        vBufsize = string.Empty;
                        vOptions = string.Empty;
                    }
                    // Textbox Not Empty
                    if (mainwindow.vBitrateCustom.Text != "Bitrate" 
                        && !string.IsNullOrWhiteSpace(mainwindow.vBitrateCustom.Text))
                    {
                        vBitrate = mainwindow.vBitrateCustom.Text.ToString();
                        vOptions = "-pix_fmt yuv420p";
                    }

                    // -------------------------
                    // CRF
                    // -------------------------
                    // if CRF texbox is default or empty
                    if (mainwindow.crfCustom.Text == "CRF" 
                        || string.IsNullOrWhiteSpace(mainwindow.crfCustom.Text))
                    {
                        crf = string.Empty;
                    }
                    // if CRF texbox entered by user and is not blank
                    if (mainwindow.crfCustom.Text != "CRF" 
                        && !string.IsNullOrWhiteSpace(mainwindow.crfCustom.Text))
                    {
                        // x264
                        crf = "-crf " + mainwindow.crfCustom.Text; // crf needs b:v 0
                        vOptions = "-pix_fmt yuv420p";

                        // x265
                        if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                        {
                            crf = "-crf" + mainwindow.crfCustom.Text + " -x265-params crf=" + mainwindow.crfCustom.Text;
                            vOptions = "-pix_fmt yuv420p";
                        }
                    }

                    // -------------------------
                    // VP9 crf -b:v 0
                    // -------------------------
                    // vBitrate is Default or Empty 
                    // & CRF is Custom value
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
                    {
                        if (mainwindow.vBitrateCustom.Text == "Bitrate" 
                            || string.IsNullOrWhiteSpace(mainwindow.vBitrateCustom.Text))
                        {
                            if (mainwindow.crfCustom.Text != "CRF" 
                                && !string.IsNullOrWhiteSpace(mainwindow.crfCustom.Text))
                            {
                                vBitrate = "0";
                                vOptions = "-pix_fmt yuv420p";
                            }
                        }
                    }

                }

                // -------------------------
                // None
                // -------------------------
                else if ((string)mainwindow.cboVideo.SelectedItem == "None")
                {
                    crf = string.Empty;
                    vBitMode = string.Empty;
                    vBitrate = string.Empty;
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
                || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
            {
                width = "-1";
            }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
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
                || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
            {
                height = "-1";
            }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
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
            if ((string)mainwindow.cboSize.SelectedItem == "No")
            {
                // MP4/MKV Width/Height Fix
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" 
                    || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
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

                    // Change the left over Default "width" and "height" text to "auto"
                    if (string.Equals(mainwindow.widthCustom.Text, "width", StringComparison.CurrentCultureIgnoreCase))
                    {
                        mainwindow.widthCustom.Text = "auto";
                    }

                    if (string.Equals(mainwindow.heightCustom.Text, "height", StringComparison.CurrentCultureIgnoreCase))
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
                        // If User enters "auto" or textbox has default "width" or "height"
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
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
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
                                System.Windows.MessageBox.Show("Must enter numbers only.");
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
                                System.Windows.MessageBox.Show("Must enter numbers only.");
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
                                System.Windows.MessageBox.Show("Must enter numbers only.");
                            }
                        }
                        // -------------------------
                        // Width = auto
                        // Height = auto
                        // -------------------------
                        else if (string.Equals(mainwindow.widthCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase)
                            && string.Equals(mainwindow.heightCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                        {
                            // If User enters "auto" or textbox has default "width" or "height"
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
                && string.IsNullOrWhiteSpace(mainwindow.buttonCropClearTextBox.Text))
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
                System.Windows.MessageBox.Show("Crop cannot use Codec Copy. Please select a Video Codec."); 
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
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                {
                    if ((string)mainwindow.cboSpeed.SelectedItem == "Placebo") { speed = "-preset placebo"; }
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
                // VP8
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8")
                {
                    if ((string)mainwindow.cboSpeed.SelectedItem == "Placebo") { speed = "-quality best -cpu-used 0"; }
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
                    if ((string)mainwindow.cboSpeed.SelectedItem == "Placebo") { speed = "-speed -8"; }
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
                    speed = string.Empty;
                }

                // -------------------------
                // JPEG & PNG
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG" 
                    || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                {
                    speed = string.Empty;
                }

                // -------------------------
                // None (No Codec)
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "None"
                    || string.IsNullOrEmpty((string)mainwindow.cboVideoCodec.SelectedItem))
                {
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
                // -------------------------
                // None
                // -------------------------
                // Default to blank
                if ((string)mainwindow.cboOptimize.SelectedItem == "None")
                {
                    optimize = string.Empty;
                }

                // -------------------------
                // VP8, VP9, Theora
                // -------------------------
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8"
                    || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9"
                    || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
                {
                    // Web
                    if ((string)mainwindow.cboOptimize.SelectedItem == "Web")
                    {
                        optimize = "-movflags faststart";
                    }
                }
                // -------------------------
                // x264
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                {
                    // Web
                    if ((string)mainwindow.cboOptimize.SelectedItem == "Web")
                    {
                        optimize = "-profile:v baseline -level 3.0 -movflags +faststart ";
                    }
                    // DVD
                    else if ((string)mainwindow.cboOptimize.SelectedItem == "DVD")
                    {
                        optimize = "-profile:v baseline -level 3.0 -maxrate 9.6M";
                    }
                    // HD Video
                    else if ((string)mainwindow.cboOptimize.SelectedItem == "HD Video")
                    {
                        optimize = "-profile:v main -level 4.0";
                    }
                    // Animation
                    else if ((string)mainwindow.cboOptimize.SelectedItem == "Animation")
                    {
                        optimize = "-profile:v main -level 4.0";
                    }
                    // Blu-ray
                    else if ((string)mainwindow.cboOptimize.SelectedItem == "Blu-ray")
                    {
                        optimize = "-deblock 0:0 -sar 1/1 -x264opts bluray-compat=1:level=4.1:open-gop=1:slices=4:tff=1:colorprim=bt709:colormatrix=bt709:vbv-maxrate=40000:vbv-bufsize=30000:me=umh:ref=4:nal-hrd=vbr:aud=1:b-pyramid=strict";
                    }
                    // Windows Device
                    else if ((string)mainwindow.cboOptimize.SelectedItem == "Windows")
                    {
                        optimize = "-profile:v baseline -level 3.1 -movflags faststart";
                    }
                    // Apple Device
                    else if ((string)mainwindow.cboOptimize.SelectedItem == "Apple")
                    {
                        optimize = "-x264-params ref=4 -profile:v baseline -level 3.1 -movflags +faststart";
                    }
                    // Android Device
                    else if ((string)mainwindow.cboOptimize.SelectedItem == "Android")
                    {
                        optimize = "-profile:v baseline -level 3.0 -movflags faststart";
                    }
                    // PS3
                    else if ((string)mainwindow.cboOptimize.SelectedItem == "PS3")
                    {
                        optimize = "-profile:v main -level 4.0";
                    }
                    // PS4
                    else if ((string)mainwindow.cboOptimize.SelectedItem == "PS4")
                    {
                        optimize = "-profile:v main -level 4.1";
                    }
                    // Xbox 360
                    else if ((string)mainwindow.cboOptimize.SelectedItem == "Xbox 360")
                    {
                        optimize = "-profile:v high -level 4.1 -maxrate 9.8M";
                    }
                    // Xbox One
                    else if ((string)mainwindow.cboOptimize.SelectedItem == "Xbox 360")
                    {
                        optimize = "-profile:v high -level 4.1";
                    }
                }
                // -------------------------
                // x265
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                {
                    // Web
                    if ((string)mainwindow.cboOptimize.SelectedItem == "Web")
                    {
                        optimize = "-movflags +faststart";
                    }
                }

                // -------------------------
                // Advanced (x264 & x265)
                // -------------------------
                if ((string)mainwindow.cboOptimize.SelectedItem == "Advanced")
                {
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" 
                        || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                    {
                        // Tune
                        //
                        if (OptimizeAdvancedWindow.optAdvTune == "none" 
                            || string.IsNullOrEmpty(OptimizeAdvancedWindow.optAdvTune))
                        {
                            optTune = string.Empty;
                        }
                        else
                        {
                            // Tune = Set Tmp Setting from Optimized Advanced Window
                            optTune = "-tune " + OptimizeAdvancedWindow.optAdvTune;
                        }


                        // Profile
                        //
                        if (OptimizeAdvancedWindow.optAdvProfile == "none" 
                            || string.IsNullOrEmpty(OptimizeAdvancedWindow.optAdvProfile))
                        {
                            optProfile = string.Empty;
                        }
                        else
                        {
                            // Tune = Set Tmp Setting from Optimized Advanced Window
                            optProfile = "-profile:v " + OptimizeAdvancedWindow.optAdvProfile;
                        }

                        // Level
                        //
                        if (OptimizeAdvancedWindow.optAdvLevel == "none" 
                            || string.IsNullOrEmpty(OptimizeAdvancedWindow.optAdvLevel))
                        {
                            optLevel = string.Empty;
                        }
                        else
                        {
                            // Tune = Set Tmp Setting from Optimized Advanced Window
                            optLevel = "-level " + OptimizeAdvancedWindow.optAdvLevel;
                        }


                        // Combine Optimize = Tune + Profile + Level
                        //
                        List<string> v2passList = new List<string>() {
                        optProfile,
                        optLevel,
                        optTune
                    };

                        optimize = string.Join(" ", v2passList.Where(s => !string.IsNullOrEmpty(s)));
                    }
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
                    System.Windows.MessageBox.Show("No input file or Framerate not detected.");
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


        /// <summary>
        ///     Subtitle Codec
        /// </summary>
        public static String SubtitleCodec(MainWindow mainwindow)
        {
            // --------------------------------------------------
            // Subtitle Map
            // --------------------------------------------------

            // -------------------------
            // Video
            // -------------------------
            if ((string)mainwindow.cboMediaType.SelectedItem == "Video")
            {
                // None
                //
                if ((string)mainwindow.cboSubtitle.SelectedItem == "none")
                {
                    sCodec = string.Empty;
                }
                // All & Number
                //
                else
                {
                    // Formats
                    if ((string)mainwindow.cboFormat.SelectedItem == "webm")
                    {
                        sCodec = string.Empty;
                    }
                    else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                    {
                        sCodec = "-c:s mov_text";
                    }
                    else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                    {
                        sCodec = "-c:s copy";
                    }
                    else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                    {
                        sCodec = string.Empty;
                    }  
                }
            }
            // -------------------------
            // Image
            // -------------------------
            else if ((string)mainwindow.cboMediaType.SelectedItem == "Image"
                || (string)mainwindow.cboMediaType.SelectedItem == "Sequence")
            {
                sCodec = string.Empty;
            }
            // -------------------------
            // Audio
            // -------------------------
            else if ((string)mainwindow.cboMediaType.SelectedItem == "Audio")
            {
                sCodec = string.Empty;
            }


            // Return Value
            return sCodec;
        }

    }
}