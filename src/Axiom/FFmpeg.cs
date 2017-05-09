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
            // Disable 2 Pass if Video Selected None (Audio) 
            if ((string)mainwindow.cboVideo.SelectedItem == "None")
            {
                Video.v2passSwitch = 0;
                Video.v2passBatchSwitch = 0;
                Video.pass1 = string.Empty;
            }

            // Disable 2 Pass if User selected Value
            if (FFprobe.inputVideoBitrate == "N/A" 
                || string.IsNullOrEmpty(FFprobe.inputVideoBitrate) 
                && (string)mainwindow.cboVideo.SelectedItem != "Auto" 
                && (string)mainwindow.cboPass.SelectedItem != "2 Pass")
            {
                Video.v2passSwitch = 0;
                Video.v2passBatchSwitch = 0;
                Video.pass1 = string.Empty;
            }

            // Disable 2 Pass if User selected Value (again)
            if ((string)mainwindow.cboVideo.SelectedItem != "Auto" 
                && (string)mainwindow.cboPass.SelectedItem != "2 Pass")
            {
                Video.v2passSwitch = 0;
                Video.v2passBatchSwitch = 0;
                Video.pass1 = string.Empty;
            }

            // If input extension is same as output extension (.mkv = .mkv), uses codec copy, Disable 2 Pass (does same as above)
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy")
            {
                Video.v2passSwitch = 0;
                Video.v2passBatchSwitch = 0;
                Video.pass1 = string.Empty;
            }
        }


        /// <summary>
        /// 2 Pass Switch (Method)
        /// <summary>
        public static void TwoPassSwitch(MainWindow mainwindow)
        {
            // If 2 Pass is Enabled
            // Single File
            //
            if (Video.v2passSwitch == 1)
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

                // Make Arguments List
                List<string> v2passList = new List<string>() {
                    "&&",
                    FFmpeg.ffmpeg,
                    "-y",
                    "-i",
                    "\"" + MainWindow.input + "\"",
                    Video.vCodec,
                    Video.speed,
                    Video.vQuality,
                    Video.tune,
                    Video.fps,
                    Video.vFilter,
                    Video.options,
                    Video.optimize,
                    Video.pass2,
                    Audio.aCodec,
                    Audio.aQuality,
                    Audio.aSamplerate,
                    Audio.aBitDepth,
                    Audio.aChannel,
                    Audio.aFilter,
                    Streams.map,
                    Format.trim,
                    MainWindow.threads,
                    "\"" + MainWindow.output + "\""
                };

                // Join List with Spaces, Remove Empty Strings
                Video.v2pass = string.Join(" ", v2passList.Where(s => !string.IsNullOrEmpty(s)));

                // Remove double spaces
                //Video.v2pass = Regex.Replace(Video.v2pass, @"\s+", " ");
            }


            // If 2 Pass is Enabled
            // Batch
            //
            if (Video.v2passBatchSwitch == 1)
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

                // If Video = Auto, use the CMD Batch Video Variable
                if (mainwindow.tglBatch.IsChecked == true 
                    && (string)mainwindow.cboVideo.SelectedItem == "Auto" 
                    && (string)mainwindow.cboVideoCodec.SelectedItem != "Copy")
                {
                    Video.vQuality = "-b:v %V";
                    // Skipped if Codec Copy
                }
                // If Audio = Auto, use the CMD Batch Audio Variable
                if (mainwindow.tglBatch.IsChecked == true 
                    && (string)mainwindow.cboAudio.SelectedItem == "Auto" 
                    && (string)mainwindow.cboAudioCodec.SelectedItem != "Copy")
                {
                    Audio.aQuality = "-b:a %A";
                    // Skipped if Codec Copy
                }

                // Make Arguments List
                List<string> v2passBatchList = new List<string>() {
                    "&&",
                    FFmpeg.ffmpeg,
                    "-y",
                    "-i",
                    "\"" + MainWindow.input + "%~f" + "\"",
                    Video.vCodec,
                    Video.speed,
                    Video.vQuality,
                    Video.tune,
                    Video.fps,
                    Video.vFilter,
                    Video.options,
                    Video.optimize,
                    Video.pass2,
                    Audio.aCodec,
                    Audio.aQuality,
                    Audio.aSamplerate,
                    Audio.aBitDepth,
                    Audio.aChannel,
                    Audio.aFilter,
                    Streams.map,
                    Format.trim,
                    MainWindow.threads,
                    "\"" + MainWindow.output + "\""
                };

                // Join List with Spaces, Remove Empty Strings
                Video.v2passBatch = string.Join(" ", v2passBatchList.Where(s => !string.IsNullOrEmpty(s)));

                // Remove double spaces
                //Video.v2passBatch = Regex.Replace(Video.v2passBatch, @"\s+", " ");
            }
        }


        /// <summary>
        /// FFmpeg Single File Process
        /// </summary>
        public static void FFmpegSingleGenerateArgs(MainWindow mainwindow)
        {
            if (mainwindow.tglBatch.IsChecked == false)
            {
                // Make Arugments List
                List<string> FFmpegArgsList = new List<string>()
                {
                    ffmpeg,
                    "-y",
                    "-i",
                    "\"" + MainWindow.input + "\"",
                    Video.vCodec,
                    Video.speed,
                    Video.vQuality,
                    Video.tune,
                    Video.fps,
                    Video.vFilter,
                    Video.options,
                    Video.optimize,
                    Video.pass1,
                    Audio.aCodec,
                    Audio.aQuality,
                    Audio.aSamplerate,
                    Audio.aBitDepth,
                    Audio.aChannel,
                    Audio.aFilter,
                    Streams.map,
                    Format.trim,
                    MainWindow.threads,
                    "\"" + MainWindow.output + "\"",
                    Video.v2pass
                };

                // Join List with Spaces, Remove Empty Strings
                ffmpegArgs = string.Join(" ", FFmpegArgsList.Where(s => !string.IsNullOrEmpty(s)));

                // Remove double spaces
                //ffmpegArgs = Regex.Replace(ffmpegArgs, @"\s+", " ");
            }


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new Bold(new Run("FFmpeg Arguments")) { Foreground = Log.ConsoleTitle });
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new Run(ffmpegArgs) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);
        }


        /// <summary>
        /// FFmpeg Single Convert
        /// </summary>
        // Start FFmpeg Process
        public static void FFmpegSingleConvert(MainWindow mainwindow)
        {
            if (mainwindow.tglBatch.IsChecked == false && MainWindow.script == 0) // if script not clicked, start ffmpeg
            {
                System.Diagnostics.Process.Start("CMD.exe", /* /c or /k -->*/ cmdWindow + /* needed to start cmd -->*/ "cd " + "\"" + MainWindow.currentDir + "\"" + " && " + /* start ffmpeg commands -->*/ ffmpegArgs);
            }
        }


        /// <summary>
        /// FFmpeg Batch Generate Args
        /// </summary>
        public static void FFmpegBatchGenerateArgs(MainWindow mainwindow)
        {
            if (mainwindow.tglBatch.IsChecked == true)
            {
                // Replace ( with ^( to avoid Windows 7 CMD Error //important!
                // This is only used in select areas
                MainWindow.autoBatchInput = mainwindow.textBoxBrowse.Text.Replace(@"(", "^(");
                MainWindow.autoBatchInput = MainWindow.autoBatchInput.Replace(@")", "^)");


                /// <summary>
                /// FFprobe Video Entry Type Containers - Batch (Method)
                /// </summary>
                FFprobe.FFprobeVideoEntryTypeBatch(mainwindow);

                /// <summary>
                /// FFprobe Video Entry Type Containers - Batch (Method)
                /// </summary>
                FFprobe.FFprobeAudioEntryTypeBatch(mainwindow);


                // -------------------------
                // FFprobe Auto Bitrate Detect
                // -------------------------
                if ((string)mainwindow.cboVideo.SelectedItem == "Auto" 
                    || (string)mainwindow.cboAudio.SelectedItem == "Auto")
                {
                    MainWindow.batchFFprobeAuto = FFprobe.ffprobe + " -i " + "\"" + MainWindow.autoBatchInput + "%~f" + "\"";
                }
                // Batch FFprobe Auto Copy
                // Video [Quality Preset] / Audio [Auto][Copy] - Disable FFprobe
                if ((string)mainwindow.cboVideo.SelectedItem != "Auto" 
                    && (string)mainwindow.cboAudio.SelectedItem == "Auto" 
                    && (string)mainwindow.cboAudioCodec.SelectedItem == "Copy")
                {
                    MainWindow.batchFFprobeAuto = string.Empty;
                }
                // Video [Auto][Copy] / Audio [Quality Preset] - Disable FFprobe
                if ((string)mainwindow.cboAudio.SelectedItem != "Auto" 
                    && (string)mainwindow.cboVideo.SelectedItem == "Auto" 
                    && (string)mainwindow.cboVideoCodec.SelectedItem == "Copy")
                {
                    MainWindow.batchFFprobeAuto = string.Empty;
                }




                


                // -------------------------
                // Batch (Auto)
                // -------------------------
                // Log Console Message /////////
                if (mainwindow.tglBatch.IsChecked == true)
                {
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("Batch: ")) { Foreground = Log.ConsoleDefault });
                        Log.paragraph.Inlines.Add(new Run(Convert.ToString(mainwindow.tglBatch.IsChecked)) { Foreground = Log.ConsoleDefault });
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("Generating Batch Script...")) { Foreground = Log.ConsoleTitle });
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("Running Batch Convert...")) { Foreground = Log.ConsoleAction });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }


                // -------------------------
                //  Batch Audio Convert - Media to Audio - Audio (Auto)
                // -------------------------
                if (mainwindow.tglBatch.IsChecked == true 
                    && (string)mainwindow.cboMediaType.SelectedItem == "Audio" 
                    && (string)mainwindow.cboAudio.SelectedItem == "Auto")
                {
                    // Make List
                    List<string> FFmpegArgsList = new List<string>()
                    {
                        "cd",
                        "\"" + MainWindow.input + "\"",
                        "&& for %f in",
                        "(*" + MainWindow.batchExt + ")",
                        "do",
                        MainWindow.batchFFprobeAuto,
                        Audio.batchAudioAuto,
                        Audio.aBitrateLimiter,
                        ffmpeg,
                        "-y",
                        "-i",
                        "\"" + MainWindow.input + "%~f" + "\"",
                        Video.vCodec, //-an
                        Audio.aCodec,
                        Audio.aQuality,
                        Audio.aSamplerate,
                        Audio.aBitDepth,
                        Audio.aChannel,
                        Audio.aFilter,
                        Streams.map,
                        Format.trim,
                        MainWindow.threads,
                        "\"" + MainWindow.output + "\""
                    };
                    
                    // Join List with Spaces, Remove Empty Strings
                    ffmpegArgs = string.Join(" ", FFmpegArgsList.Where(s => !string.IsNullOrEmpty(s)));

                    // Remove double spaces
                    //ffmpegArgs = Regex.Replace(ffmpegArgs, @"\s+", " ");

                }


                // -------------------------
                // Batch Video Convert - Media to Video - Video OR Audio (Auto)
                // -------------------------
                else if (mainwindow.tglBatch.IsChecked == true 
                    && (string)mainwindow.cboMediaType.SelectedItem == "Video" 
                    && (string)mainwindow.cboVideo.SelectedItem == "Auto" 
                    || (string)mainwindow.cboAudio.SelectedItem == "Auto")
                {
                    // Make List
                    List<string> FFmpegArgsList = new List<string>()
                    {
                        "cd",
                        "\"" + MainWindow.input + "\"",
                        "&& for %f in",
                        "(*" + MainWindow.batchExt + ")",
                        "do",
                        MainWindow.batchFFprobeAuto,
                        Video.batchVideoAuto,
                        Audio.batchAudioAuto,
                        Audio.aBitrateLimiter,
                        ffmpeg,
                        "-y",
                        "-i ",
                        "\"" + MainWindow.input + "%~f" + "\"",
                        Video.vCodec,
                        Video.speed,
                        Video.vQuality,
                        Video.tune,
                        Video.fps,
                        Video.vFilter,
                        Video.options,
                        Video.optimize,
                        Video.pass1,
                        Audio.aCodec,
                        Audio.aQuality,
                        Audio.aSamplerate,
                        Audio.aBitDepth,
                        Audio.aChannel,
                        Audio.aFilter,
                        Streams.map,
                        Format.trim,
                        MainWindow.threads,
                        "\"" + MainWindow.output + "\"",
                        Video.v2passBatch
                    };

                    // Join List with Spaces, Remove Empty Strings
                    ffmpegArgs = string.Join(" ", FFmpegArgsList.Where(s => !string.IsNullOrEmpty(s)));

                    // Remove double spaces
                    //ffmpegArgs = Regex.Replace(ffmpegArgs, @"\s+", " ");
                }


                // -------------------------
                // Batch Media (User selected options) Not Auto
                // -------------------------
                else if (mainwindow.tglBatch.IsChecked == true 
                    && (string)mainwindow.cboVideo.SelectedItem != "Auto" 
                    && (string)mainwindow.cboAudio.SelectedItem != "Auto")
                {
                    // Make List
                    List<string> FFmpegArgsList = new List<string>()
                    {
                        "cd",
                        "\"" + MainWindow.input + "\"",
                        "&& for %f in",
                        "(*" + MainWindow.batchExt + ")",
                        "do",
                        ffmpeg,
                        "-y",
                        "-i",
                        "\"" + MainWindow.input + "%~f" + "\"",
                        Video.vCodec,
                        Video.speed,
                        Video.vQuality,
                        Video.tune,
                        Video.fps,
                        Video.vFilter,
                        Video.options,
                        Video.optimize,
                        Video.pass1,
                        Audio.aCodec,
                        Audio.aQuality,
                        Audio.aSamplerate,
                        Audio.aBitDepth,
                        Audio.aChannel,
                        Audio.aFilter,
                        Streams.map,
                        Format.trim,
                        MainWindow.threads,
                        "\"" + MainWindow.output + "\"",
                        Video.v2passBatch
                    };

                    // Join List with Spaces, Remove Empty Strings
                    ffmpegArgs = string.Join(" ", FFmpegArgsList.Where(s => !string.IsNullOrEmpty(s)));

                    // Remove double spaces
                    //ffmpegArgs = Regex.Replace(ffmpegArgs, @"\s+", " ");
                }
            }
        }


        /// <summary>
        /// FFmpeg Batch Convert
        /// </summary>
        public static void FFmpegBatchConvert(MainWindow mainwindow)
        {
            if (mainwindow.tglBatch.IsChecked == true && MainWindow.script == 0) // check if script button enabled
            {
                System.Diagnostics.Process.Start("CMD.exe", cmdWindow + ffmpegArgs);
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
                ScriptView scriptview = new ScriptView(ffmpegArgs);
                scriptview.Left = mainwindow.Left + 90;
                scriptview.Top = mainwindow.Top + 98;
                scriptview.Owner = Window.GetWindow(mainwindow);
                scriptview.Show();
            }
        }


    }
}
