using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        public static string ffmpegArgsSort; // FFmpeg Arguments Sorted
        public static string cmdWindow; // Keep / Close Batch Argument

        // Sorted Argument Colors
        //public static System.Windows.Media.Brush Blue = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0000CC"));
        //public static System.Windows.Media.Brush Purple = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9900CC"));
        //public static System.Windows.Media.Brush Red = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0000"));


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Process Methods
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// OnePassArgs
        /// </summary>
        // 1-Pass, CRF, & Auto
        public static String OnePassArgs(MainWindow mainwindow)
        {
            // -------------------------
            //  Single Pass
            // -------------------------
            if ((string)mainwindow.cboPass.SelectedItem == "1 Pass" 
                || (string)mainwindow.cboPass.SelectedItem == "CRF" 
                || (string)mainwindow.cboPass.SelectedItem == "auto"
                || (string)mainwindow.cboFormat.SelectedItem == "ogv" //ogv (special rule)
                )
            {
                // -------------------------
                //  Arguments List
                // -------------------------
                List<string> FFmpegArgsSinglePassList = new List<string>()
                    {
                        "\r\n\r\n" + "\"" + MainWindow.InputPath(mainwindow) + "\"",

                        "\r\n\r\n" + Video.VideoCodec(mainwindow),
                        "\r\n" + Video.Speed(mainwindow),
                        Video.VideoQuality(mainwindow),
                        "\r\n" + Video.FPS(mainwindow),
                        "\r\n" + Video.VideoFilter(mainwindow),
                        "\r\n" + Video.Images(mainwindow),
                        "\r\n" + Video.Optimize(mainwindow),

                        "\r\n\r\n" + Video.SubtitleCodec(mainwindow),

                        "\r\n\r\n" + Audio.AudioCodec(mainwindow),
                        "\r\n" + Audio.AudioQuality(mainwindow),
                        Audio.SampleRate(mainwindow),
                        Audio.BitDepth(mainwindow),
                        Audio.Channel(mainwindow),
                        "\r\n" + Audio.AudioFilter(mainwindow),

                        "\r\n\r\n" + Streams.StreamMaps(mainwindow),
                        "\r\n\r\n" + Format.Cut(mainwindow),
                        "\r\n\r\n" + MainWindow.ThreadDetect(mainwindow),

                        "\r\n\r\n" + "\"" + MainWindow.OutputPath(mainwindow) + "\""
                    };

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                Video.passSingle = string.Join(" ",
                    FFmpegArgsSinglePassList
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Where(s => !s.Equals("\r\n\r\n"))
                    .Where(s => !s.Equals("\r\n"))
                    );
            }


            // Return Value
            return Video.passSingle;
        }


        /// <summary>
        /// Batch2PassArgs
        /// </summary>
        
        public static String TwoPassArgs(MainWindow mainwindow)
        {
            // -------------------------
            //  2-Pass Auto Quality
            // -------------------------
            // Enabled 
            //
            if ((string)mainwindow.cboPass.SelectedItem == "2 Pass" 
                && (string)mainwindow.cboMediaType.SelectedItem == "Video" //video only
                && (string)mainwindow.cboVideoCodec.SelectedItem != "Copy" //exclude copy
                && (string)mainwindow.cboFormat.SelectedItem != "ogv" //exclude ogv (special rule)
                )
            {
                // -------------------------
                // Pass 1
                // -------------------------
                List<string> FFmpegArgsPass1List = new List<string>()
                    {
                        "\r\n\r\n" + "\"" + MainWindow.InputPath(mainwindow) + "\"",

                        "\r\n\r\n" + Video.VideoCodec(mainwindow),
                        "\r\n" + Video.Speed(mainwindow),
                        Video.VideoQuality(mainwindow),
                        "\r\n" + Video.FPS(mainwindow),
                        "\r\n" + Video.VideoFilter(mainwindow),
                        "\r\n" + Video.Images(mainwindow),
                        "\r\n" + Video.Optimize(mainwindow),
                        "\r\n" + Video.Pass1Modifier(mainwindow), // -pass 1, -x265-params pass=2

                        // Disable Subtitles for Pass 1 to speed up encoding
                        // -sn put in Stream Maps
                        // Disable Audio for Pass 1 to speed up encoding
                        // -an put in Stream Maps

                        "\r\n\r\n" + Streams.StreamMaps(mainwindow),
                        "\r\n\r\n" + Format.Cut(mainwindow),
                        "\r\n\r\n" + MainWindow.ThreadDetect(mainwindow),

                        "\r\n\r\n" + "\"" + MainWindow.OutputPath(mainwindow) + "\""
                    };

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                //
                Video.pass1Args = string.Join(" ", FFmpegArgsPass1List
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Where(s => !s.Equals("\r\n\r\n"))
                    .Where(s => !s.Equals("\r\n"))
                    );

                // Turn on Two-Pass Switch
                // Lets Pass 2 know Pass 1 has been run
                //
                Video.v2PassSwitch = 1;


                // -------------------------
                // Pass 2
                // -------------------------
                List<string> FFmpegArgsPass2List = new List<string>()
                    {
                        // Video Strings have already been defined in Pass 1
                        // Use Strings instead of Methods
                        //
                        "\r\n\r\n" + "&&",
                        "\r\n\r\n" + MainWindow.FFmpegPath(mainwindow),
                        "-y",
                        "-i",

                        "\r\n\r\n" + "\"" + MainWindow.InputPath(mainwindow) + "\"",

                        "\r\n\r\n" + Video.vCodec,
                        "\r\n" + Video.speed,
                        Video.vQuality,
                        "\r\n" + Video.fps,
                        "\r\n" + Video.vFilter,
                        "\r\n" + Video.image,
                        "\r\n" + Video.optimize,
                        "\r\n" + Video.Pass2Modifier(mainwindow), // -pass 2, -x265-params pass=2

                        "\r\n\r\n" + Video.SubtitleCodec(mainwindow),

                        "\r\n\r\n" + Audio.AudioCodec(mainwindow),
                        "\r\n" + Audio.AudioQuality(mainwindow),
                        Audio.SampleRate(mainwindow),
                        Audio.BitDepth(mainwindow),
                        Audio.Channel(mainwindow),
                        "\r\n" + Audio.AudioFilter(mainwindow),

                        "\r\n\r\n" + Streams.StreamMaps(mainwindow), // use method, contains 2-Pass Switch
                        "\r\n\r\n" + Format.trim,
                        "\r\n\r\n" + MainWindow.threads,

                        "\r\n\r\n" + "\"" + MainWindow.OutputPath(mainwindow) + "\""
                    };

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                //
                Video.pass2Args = string.Join(" ", FFmpegArgsPass2List
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Where(s => !s.Equals("\r\n\r\n"))
                    .Where(s => !s.Equals("\r\n"))
                    );

                // Combine Pass 1 & Pass 2 Args
                //
                Video.v2PassArgs = Video.pass1Args + " " + Video.pass2Args;


                // Turn Off Two-Pass Switch
                // All Passes have been run
                //
                Video.v2PassSwitch = 0;
            }


            // Return Value
            return Video.v2PassArgs;
        }


        // --------------------------------------------------------------------------------------------------------


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
                    MainWindow.FFmpegPath(mainwindow),
                    "-y",
                    "-i",
                    FFmpeg.OnePassArgs(mainwindow), //disabled if 2-Pass
                    FFmpeg.TwoPassArgs(mainwindow) //disabled if 1-Pass
                };

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                FFmpeg.ffmpegArgsSort = string.Join(" ", FFmpegArgsList
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Where(s => !s.Equals("\r\n\r\n"))
                    .Where(s => !s.Equals("\r\n"))
                    );

                // Inline 
                FFmpeg.ffmpegArgs = string.Join(" ", FFmpegArgsList
                    .Where(s => !string.IsNullOrEmpty(s)))
                    .Replace("\r\n", "") //Remove Linebreaks
                    .Replace(Environment.NewLine, "");
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
        /// FFmpeg Convert
        /// </summary>
        // Start FFmpeg Process
        public static void FFmpegSingleConvert(MainWindow mainwindow)
        {
            // if script not clicked, start ffmpeg
            if (mainwindow.tglBatch.IsChecked == false && MainWindow.script == 0)
            {
                System.Diagnostics.Process.Start(
                    "CMD.exe", 
                    FFmpeg.cmdWindow // /c or /k
                    + "cd " + "\"" + MainWindow.currentDir + "\"" // needed to start cmd
                    + " && " 
                    + FFmpeg.ffmpegArgs // start ffmpeg commands
                    );
            }
        }


        // --------------------------------------------------------------------------------------------------------


        /// <summary>
        /// FFmpeg Batch - Generate Args
        /// </summary>
        public static void FFmpegBatchGenerateArgs(MainWindow mainwindow)
        {
            if (mainwindow.tglBatch.IsChecked == true)
            {
                // Replace ( with ^( to avoid Windows 7 CMD Error //important!
                // This is only used in select areas
                //MainWindow.batchInputAuto = mainwindow.textBoxBrowse.Text.Replace(@"(", "^(");
                //MainWindow.batchInputAuto = MainWindow.batchInputAuto.Replace(@")", "^)");


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
                // Batch Arguments Full
                // -------------------------
                // Make List
                //
                List<string> FFmpegBatchArgsList = new List<string>()
                {
                    "cd",
                    "\"" + MainWindow.BatchInputDirectory(mainwindow) + "\"",

                    "\r\n\r\n" + "&& for %f in",
                    "(*" + MainWindow.batchExt + ")",
                    "do (echo)",

                    "\r\n\r\n" + Video.BatchVideoQualityAuto(mainwindow),

                    "\r\n\r\n" + Audio.BatchAudioQualityAuto(mainwindow),
                    "\r\n\r\n" + Audio.BatchAudioBitrateLimiter(mainwindow),

                    "\r\n\r\n" + "&&",
                    "\r\n\r\n" + MainWindow.FFmpegPath(mainwindow),
                    "-y",
                    "-i",
                    //%~f added in InputPath()

                    FFmpeg.OnePassArgs(mainwindow), //disabled if 2-Pass       
                    FFmpeg.TwoPassArgs(mainwindow) //disabled if 1-Pass
                };

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                FFmpeg.ffmpegArgsSort = string.Join(" ", FFmpegBatchArgsList
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Where(s => !s.Equals("\r\n\r\n"))
                    .Where(s => !s.Equals("\r\n"))
                    );

                // Inline 
                FFmpeg.ffmpegArgs = string.Join(" ", FFmpegBatchArgsList
                    .Where(s => !string.IsNullOrEmpty(s)))
                    .Replace("\r\n", "") //Remove Linebreaks
                    .Replace(Environment.NewLine, "");
            }
        }


        /// <summary>
        /// FFmpeg Batch Convert
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
        public static void FFmpegScript(MainWindow mainwindow, ScriptView scriptview)
        {
            if (MainWindow.script == 1)
            {
                // Open ScriptView Window
                scriptview = new ScriptView(mainwindow);
                scriptview.Left = mainwindow.Left + 90;
                scriptview.Top = mainwindow.Top + 98;
                //scriptview.Owner = Window.GetWindow(mainwindow);
                scriptview.Show();
            }
        }


    }
}