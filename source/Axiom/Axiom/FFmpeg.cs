/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017, 2018 Matt McManis
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
        //public static string cmdWindow; // Keep / Close Batch Argument

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
        ///    Keep FFmpegWindow Switch (Method)
        /// </summary>
        /// <remarks>
        ///     CMD.exe command, /k = keep, /c = close
        ///     Do not .Close(); if using /c, it will throw a Dispose exception
        /// </remarks>
        public static String KeepWindow(MainWindow mainwindow)
        {
            string cmdWindow = string.Empty;

            // Keep
            if (mainwindow.tglWindowKeep.IsChecked == true)
            {
                cmdWindow = "/k ";
            }
            // Close
            else
            {
                cmdWindow = "/c ";
            }

            return cmdWindow;
        }

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
                        "\r\n" + Streams.VideoStreamMaps(mainwindow),

                        "\r\n\r\n" + Video.SubtitleCodec(mainwindow),
                        "\r\n" + Streams.SubtitleMaps(mainwindow),

                        "\r\n\r\n" + Audio.AudioCodec(mainwindow),
                        "\r\n" + Audio.AudioQuality(mainwindow),
                        Audio.SampleRate(mainwindow),
                        Audio.BitDepth(mainwindow),
                        Audio.Channel(mainwindow),
                        "\r\n" + Audio.AudioFilter(mainwindow),
                        "\r\n" + Streams.AudioStreamMaps(mainwindow),

                        "\r\n\r\n" + Format.Cut(mainwindow),

                        "\r\n\r\n" + Streams.FormatMaps(mainwindow),

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

                        "\r\n\r\n" + "-sn -an", // Disable Audio & Subtitles for Pass 1 to speed up encoding

                        "\r\n\r\n" + Format.Cut(mainwindow),
                        "\r\n\r\n" + Format.ForceFormat(mainwindow),
                        "\r\n\r\n" + MainWindow.ThreadDetect(mainwindow),

                        //"\r\n\r\n" + "\"" + MainWindow.OutputPath(mainwindow) + "\""
                        "\r\n\r\n" + "NUL"
                    };

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                Video.pass1Args = string.Join(" ", FFmpegArgsPass1List
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Where(s => !s.Equals("\r\n\r\n"))
                    .Where(s => !s.Equals("\r\n"))
                    );


                // -------------------------
                // Pass 2
                // -------------------------
                List<string> FFmpegArgsPass2List = new List<string>()
                    {
                        // Video Methods have already defined Global Strings in Pass 1
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
                        "\r\n" + Streams.VideoStreamMaps(mainwindow),
                        "\r\n" + Video.Pass2Modifier(mainwindow), // -pass 2, -x265-params pass=2

                        "\r\n\r\n" + Video.SubtitleCodec(mainwindow),
                        "\r\n" + Streams.SubtitleMaps(mainwindow),

                        "\r\n\r\n" + Audio.AudioCodec(mainwindow),
                        "\r\n" + Audio.AudioQuality(mainwindow),
                        Audio.SampleRate(mainwindow),
                        Audio.BitDepth(mainwindow),
                        Audio.Channel(mainwindow),
                        "\r\n" + Audio.AudioFilter(mainwindow),
                        "\r\n" + Streams.AudioStreamMaps(mainwindow),

                        "\r\n\r\n" + Format.trim,

                        "\r\n\r\n" + Streams.FormatMaps(mainwindow),

                        "\r\n\r\n" + MainWindow.threads,

                        "\r\n\r\n" + "\"" + MainWindow.OutputPath(mainwindow) + "\""
                    };

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                Video.pass2Args = string.Join(" ", FFmpegArgsPass2List
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Where(s => !s.Equals("\r\n\r\n"))
                    .Where(s => !s.Equals("\r\n"))
                    );

                // Combine Pass 1 & Pass 2 Args
                //
                Video.v2PassArgs = Video.pass1Args + " " + Video.pass2Args;
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
                ffmpegArgsSort = string.Join(" ", FFmpegArgsList
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Where(s => !s.Equals("\r\n\r\n"))
                    .Where(s => !s.Equals("\r\n"))
                    );

                // Inline 
                ffmpegArgs = string.Join(" ", FFmpegArgsList
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
                Log.logParagraph.Inlines.Add(new Run(ffmpegArgs) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);


            // Return Value
            return ffmpegArgs;
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
                    "cd /d",
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
                ffmpegArgsSort = string.Join(" ", FFmpegBatchArgsList
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Where(s => !s.Equals("\r\n\r\n"))
                    .Where(s => !s.Equals("\r\n"))
                    );

                // Inline 
                ffmpegArgs = string.Join(" ", FFmpegBatchArgsList
                    .Where(s => !string.IsNullOrEmpty(s)))
                    .Replace("\r\n", "") //Remove Linebreaks
                    .Replace(Environment.NewLine, "");
            }
        }


        // --------------------------------------------------------------------------------------------------------


        /// <summary>
        /// FFmpeg Generate Script
        /// </summary>
        public static void FFmpegScript(MainWindow mainwindow, ScriptView scriptview)
        {
            // Detect which screen we're on
            var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
            var thisScreen = allScreens.SingleOrDefault(s => mainwindow.Left >= s.WorkingArea.Left && mainwindow.Left < s.WorkingArea.Right);

            // Start Window
            scriptview = new ScriptView(mainwindow);

            // Position Relative to MainWindow
            // Keep from going off screen
            scriptview.Left = Math.Max((mainwindow.Left + (mainwindow.Width - scriptview.Width) / 2), thisScreen.WorkingArea.Left);
            scriptview.Top = Math.Max(((mainwindow.Top + (mainwindow.Height - scriptview.Height) * 2)), thisScreen.WorkingArea.Top);

            // Open Window
            scriptview.Show();
        }


        /// <summary>
        /// FFmpeg Convert
        /// </summary>
        public static void FFmpegConvert(MainWindow mainwindow)
        {
            // start ffmpeg commands
            System.Diagnostics.Process.Start(
                "cmd.exe",
                //FFmpeg.cmdWindow 
                KeepWindow(mainwindow)
                + " cd " + "\"" + MainWindow.appDir + "\""
                + " & "
                + FFmpeg.ffmpegArgs
            );
        }


    }
}