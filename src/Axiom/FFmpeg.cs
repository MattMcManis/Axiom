using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

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

namespace Axiom
{
    public partial class FFmpeg
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Variables
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        // FFmepg / FFprobe
        public static string ffmpeg; // FFmpeg.exe
        public static string ffmpegArgs; // FFmpeg Arguments
        public static string cmdWindow; // Keep / Close Batch Argument

        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Process Methods
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 2 Pass Clear (Method)
        /// <summary>
        public static void TwoPassClear(MainWindow mainwindow)
        {
            // Disable 2-Pass if Video Selected None (Audio) 
            if ((string)mainwindow.cboVideo.SelectedItem == "None")
            {
                Video.v2passSwitch = 0;
                Video.pass1 = string.Empty;
                Video.pass2 = string.Empty;
            }

            // Disable 2-Pass if User selected Value
            if (FFprobe.inputVideoBitrate == "N/A" 
                || string.IsNullOrEmpty(FFprobe.inputVideoBitrate) 
                && (string)mainwindow.cboVideo.SelectedItem != "Auto" 
                && (string)mainwindow.cboPass.SelectedItem != "2 Pass")
            {
                Video.v2passSwitch = 0;
                Video.pass1 = string.Empty;
                Video.pass2 = string.Empty;
            }

            // Disable 2-Pass if User selected Value (again)
            if ((string)mainwindow.cboVideo.SelectedItem != "Auto" 
                && (string)mainwindow.cboPass.SelectedItem != "2 Pass")
            {
                Video.v2passSwitch = 0;
                Video.pass1 = string.Empty;
                Video.pass2 = string.Empty;
            }

            // If input extension is same as output extension (.mkv = .mkv), uses codec copy, Disable 2 Pass (does same as above)
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy")
            {
                Video.v2passSwitch = 0;
                Video.pass1 = string.Empty;
                Video.pass2 = string.Empty;
            }
        }


        /// <summary>
        /// 2-Pass Switch (Method)
        /// <summary>
        public static void TwoPassSwitch(MainWindow mainwindow)
        {
            // -------------------------
            // Enabled
            // -------------------------
            if (Video.v2passSwitch == 1)
            {
                // -------------------------
                // Single File 2-Pass
                // -------------------------
                //
                if (mainwindow.tglBatch.IsChecked == false)
                {
                    // Enable pass parameters in the FFmpeg Arguments
                    // x265 Pass 2 Params
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                    {
                        Video.pass1 = "-x265-params pass=1";
                        Video.pass2 = "-x265-params pass=2";
                    }
                    // All other codecs
                    else
                    {
                        Video.pass1 = "-pass 1";
                        Video.pass2 = "-pass 2";
                    }
                }

                // -------------------------
                // Batch 2-Pass
                // -------------------------
                //
                else if (mainwindow.tglBatch.IsChecked == true)
                {
                    // Enable pass parameters in the FFmpeg Batch Arguments
                    // x265 Pass 2 Params
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                    {
                        Video.pass1 = "-x265-params pass=1";
                        Video.pass2 = "-x265-params pass=2";
                    }
                    // All other codecs
                    else
                    {
                        Video.pass1 = "-pass 1";
                        Video.pass2 = "-pass 2";
                    }

                    // Batch Input
                    //MainWindow.input = MainWindow.input + "%~f";
                }
            }

            // -------------------------
            // Disabled
            // -------------------------
            else if (Video.v2passSwitch == 0)
            {
                Video.pass1 = string.Empty;
                Video.pass2 = string.Empty;
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {

                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("2-Pass Switch: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Bold(new Run(Video.v2passSwitch.ToString())) { Foreground = Log.ConsoleDefault });

            };
            Log.LogActions.Add(Log.WriteAction);
        }


        /// <summary>
        /// TwoPassArgs
        /// </summary>
        public static String TwoPassArgs(MainWindow mainwindow)
        {
            // -------------------------
            // Make 2nd Pass Arguments List
            // -------------------------
            //
            if (Video.v2passSwitch == 1)
            {
                List<string> v2passList = new List<string>() {
                    "&&",
                    FFmpeg.ffmpeg,
                    "-y",
                    "-i",
                    "\"" + MainWindow.input + "\"",
                    Video.VideoCodec(mainwindow),
                    Video.Speed(mainwindow),
                    Video.VideoQuality(mainwindow),
                    Video.FPS(mainwindow),
                    Video.VideoFilter(mainwindow),
                    Video.Images(mainwindow),
                    Video.Optimize(mainwindow),
                    Video.pass2,
                    Audio.AudioCodec(mainwindow),
                    Audio.AudioQuality(mainwindow),
                    Audio.SampleRate(mainwindow),
                    Audio.BitDepth(mainwindow),
                    Audio.Channel(mainwindow),
                    Audio.AudioFilter(mainwindow),
                    Streams.StreamMaps(mainwindow),
                    Format.Cut(mainwindow),
                    MainWindow.ThreadDetect(mainwindow),
                    "\"" + MainWindow.output + "\""
                };

                // Join List with Spaces, Remove Empty Strings
                Video.v2passArgs = string.Join(" ", v2passList.Where(s => !string.IsNullOrEmpty(s)));
            }


            // Return Value
            return Video.v2passArgs;
        }


        /// <summary>
        /// FFmpeg Single File - Generate Args
        /// </summary>
        public static String FFmpegSingleGenerateArgs(MainWindow mainwindow)
        {
            if (mainwindow.tglBatch.IsChecked == false)
            {
                // Make Arugments List
                List<string> FFmpegArgsList = new List<string>()
                {
                    FFmpeg.ffmpeg,
                    "-y",
                    "-i",
                    "\"" + MainWindow.input + "\"",
                    Video.VideoCodec(mainwindow),
                    Video.Speed(mainwindow),
                    Video.VideoQuality(mainwindow),
                    Video.FPS(mainwindow),
                    Video.VideoFilter(mainwindow),
                    Video.Images(mainwindow),
                    Video.Optimize(mainwindow),
                    Video.pass1,
                    Audio.AudioCodec(mainwindow),
                    Audio.AudioQuality(mainwindow),
                    Audio.SampleRate(mainwindow),
                    Audio.BitDepth(mainwindow),
                    Audio.Channel(mainwindow),
                    Audio.AudioFilter(mainwindow),
                    Streams.StreamMaps(mainwindow),
                    Format.Cut(mainwindow),
                    MainWindow.ThreadDetect(mainwindow),
                    "\"" + MainWindow.output + "\"",
                    FFmpeg.TwoPassArgs(mainwindow)
                };

                // Join List with Spaces, Remove Empty Strings
                ffmpegArgs = string.Join(" ", FFmpegArgsList.Where(s => !string.IsNullOrEmpty(s)));
            }


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("FFmpeg Arguments")) { Foreground = Log.ConsoleTitle });
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Run(FFmpeg.ffmpegArgs) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);


            // Return Value
            return ffmpegArgs;
        }


        /// <summary>
        /// FFmpeg Single - Convert
        /// </summary>
        // Start FFmpeg Process
        public static void FFmpegSingleConvert(MainWindow mainwindow)
        {
            if (mainwindow.tglBatch.IsChecked == false && MainWindow.script == 0) // if script not clicked, start ffmpeg
            {
                System.Diagnostics.Process.Start(
                    "CMD.exe", /* /c or /k -->*/ FFmpeg.cmdWindow + /* needed to start cmd -->*/ "cd " + "\"" + MainWindow.currentDir + "\"" + " && " + /* start ffmpeg commands -->*/ FFmpeg.ffmpegArgs
                    );
            }
        }


        /// <summary>
        /// FFmpeg Batch - Generate Args
        /// </summary>
        public static void FFmpegBatchGenerateArgs(MainWindow mainwindow)
        {
            if (mainwindow.tglBatch.IsChecked == true)
            {
                // Replace ( with ^( to avoid Windows 7 CMD Error //important!
                // This is only used in select areas
                MainWindow.batchInputAuto = mainwindow.textBoxBrowse.Text.Replace(@"(", "^(");
                MainWindow.batchInputAuto = MainWindow.batchInputAuto.Replace(@")", "^)");


                /// <summary>
                /// FFprobe Video Entry Type Containers - Batch (Method)
                /// </summary>
                FFprobe.FFprobeVideoEntryTypeBatch(mainwindow);

                /// <summary>
                /// FFprobe Video Entry Type Containers - Batch (Method)
                /// </summary>
                FFprobe.FFprobeAudioEntryTypeBatch(mainwindow);



                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Batch: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(mainwindow.tglBatch.IsChecked)) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Generating Batch Script...")) { Foreground = Log.ConsoleTitle });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Running Batch Convert...")) { Foreground = Log.ConsoleAction });
                };
                Log.LogActions.Add(Log.WriteAction);


                // -------------------------
                //  Batch 
                //  Media to Audio Only
                // -------------------------
                if ((string)mainwindow.cboMediaType.SelectedItem == "Audio")
                {
                    // Disable Video
                    Video.pass1 = string.Empty;
                }


                // -------------------------
                // Batch Arguments Full
                // -------------------------
                // Make List
                List<string> FFmpegArgsList = new List<string>()
                {
                    "cd",
                    "\"" + MainWindow.inputDir + "\"",
                    "&& for %f in",
                    "(*" + MainWindow.batchExt + ")",
                    "do",
                    "(echo)",
                    //FFprobe.BatchFFprobeAutoBitrate(mainwindow),
                    Video.BatchVideoQualityAuto(mainwindow),
                    Audio.BatchAudioQualityAuto(mainwindow),
                    Audio.BatchAudioBitrateLimiter(mainwindow),
                    FFmpeg.ffmpeg,
                    "-y",
                    "-i",
                    "\"" + MainWindow.input + "\"", //input = %~f added in 2-pass
                    Video.VideoCodec(mainwindow),
                    Video.Speed(mainwindow),
                    Video.VideoQuality(mainwindow),
                    Video.FPS(mainwindow),
                    Video.VideoFilter(mainwindow),
                    Video.Images(mainwindow),
                    Video.Optimize(mainwindow),
                    Video.pass1,
                    Audio.AudioCodec(mainwindow),
                    Audio.AudioQuality(mainwindow),
                    Audio.SampleRate(mainwindow),
                    Audio.BitDepth(mainwindow),
                    Audio.Channel(mainwindow),
                    Audio.AudioFilter(mainwindow),
                    Streams.StreamMaps(mainwindow),
                    Format.Cut(mainwindow),
                    MainWindow.ThreadDetect(mainwindow),
                    "\"" + MainWindow.output + "\"",
                    FFmpeg.TwoPassArgs(mainwindow)
                };

                // Join List with Spaces, Remove Empty Strings
                FFmpeg.ffmpegArgs = string.Join(" ", FFmpegArgsList.Where(s => !string.IsNullOrEmpty(s)));
            }
        }


        /// <summary>
        /// FFmpeg Batch - Convert
        /// </summary>
        public static void FFmpegBatchConvert(MainWindow mainwindow)
        {
            if (mainwindow.tglBatch.IsChecked == true && MainWindow.script == 0) // check if script button enabled
            {
                System.Diagnostics.Process.Start("CMD.exe", FFmpeg.cmdWindow + FFmpeg.ffmpegArgs);
            }
        }


        /// <summary>
        /// FFmpeg Generate Script
        /// </summary>
        public static void FFmpegScript(MainWindow mainwindow)
        {
            if (MainWindow.script == 1)
            {
                //ffmpegArgs = Regex.Replace(ffmpegArgs, @"\s+", " "); /* remove extra white spaces*/

                // Open ScriptView Window
                ScriptView scriptview = new ScriptView();
                scriptview.Left = mainwindow.Left + 90;
                scriptview.Top = mainwindow.Top + 98;
                scriptview.Owner = Window.GetWindow(mainwindow);
                scriptview.Show();
            }
        }


    }
}
