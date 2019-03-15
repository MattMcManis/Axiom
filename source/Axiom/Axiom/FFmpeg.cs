/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2019 Matt McManis
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
using System.Linq;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class FFmpeg
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Variables
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        // FFmepg / FFprobe
        public static string ffmpeg; // ffmpeg.exe
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
        public static String KeepWindow(ViewModel vm)
        {
            string cmdWindow = string.Empty;

            // Keep
            if (vm.CMDWindowKeep_IsChecked == true)
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
        ///     1-Pass Arguments
        /// </summary>
        // 1-Pass, CRF, & Auto
        public static String OnePassArgs(ViewModel vm)
        {
            // -------------------------
            //  Single Pass
            // -------------------------
            if (vm.Pass_SelectedItem == "1 Pass" ||
                vm.Pass_SelectedItem == "CRF" ||
                vm.Pass_SelectedItem == "auto" ||
                vm.Container_SelectedItem == "ogv" //ogv (special rule)
                )
            {
                // -------------------------
                //  Arguments List
                // -------------------------
                List<string> FFmpegArgsSinglePassList = new List<string>()
                {
                    "\r\n\r\n" +
                    "-i "+ "\"" + MainWindow.InputPath(vm) + "\"",

                    "\r\n\r\n" +
                    Subtitle.SubtitlesExternal(vm),

                    "\r\n\r\n" +
                    Video.VideoCodec(vm, vm.VideoCodec_Command),
                    "\r\n" +
                    Video.Speed(vm,
                                vm.VideoEncodeSpeed_Items,
                                vm.VideoEncodeSpeed_SelectedItem,
                                vm.Pass_SelectedItem
                                ),
                    Video.VideoQuality(vm,
                                       vm.VideoQuality_Items,
                                       vm.VideoQuality_SelectedItem,
                                       vm.Pass_SelectedItem
                                        ),
                    "\r\n" +
                    Video.PixFmt(vm,
                                 vm.PixelFormat_SelectedItem
                                 ),
                    "\r\n" +
                    Video.FPS(vm,
                              vm.FPS_SelectedItem
                              ),
                    "\r\n" +
                    VideoFilters.VideoFilter(vm),
                    //"\r\n" +
                    //Video.ScalingAlgorithm(vm),
                    "\r\n" +
                    Video.Images(vm),
                    "\r\n" +
                    Video.Optimize(vm,
                                   vm.Video_Optimize_Items,
                                   vm.Video_Optimize_SelectedItem
                                   ),
                    "\r\n" +
                    Streams.VideoStreamMaps(vm),

                    "\r\n\r\n" +
                    Subtitle.SubtitleCodec(vm.SubtitleCodec_Command),
                    "\r\n" +
                    Streams.SubtitleMaps(vm),

                    "\r\n\r\n" +
                    Audio.AudioCodec(vm, vm.AudioCodec_Command),
                    "\r\n" +
                    Audio.AudioQuality(//vm
                                       vm.Batch_IsChecked,
                                       vm.AudioVBR_IsChecked,
                                       vm.AudioCodec_SelectedItem,
                                       vm.AudioQuality_Items,
                                       vm.AudioQuality_SelectedItem,
                                       vm.AudioBitrate_Text
                                       ),
                    Audio.SampleRate(vm),
                    Audio.BitDepth(vm,
                                   vm.AudioBitDepth_Items,
                                   vm.AudioBitDepth_SelectedItem
                                  ),
                    Audio.Channel(vm,
                                  vm.AudioChannel_SelectedItem
                                 ),
                    "\r\n" +
                    AudioFilters.AudioFilter(vm),
                    "\r\n" +
                    Streams.AudioStreamMaps(vm),

                    "\r\n\r\n" +
                    Format.Cut(vm),

                    "\r\n\r\n" +
                    Streams.FormatMaps(vm),

                    "\r\n\r\n" +
                    Format.ForceFormat(vm),

                    "\r\n\r\n" +
                    MainWindow.ThreadDetect(vm),

                    "\r\n\r\n" +
                    "\"" + MainWindow.OutputPath(vm) + "\""
                };

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                Video.passSingle = string.Join(" ", FFmpegArgsSinglePassList
                                                    .Where(s => !string.IsNullOrEmpty(s))
                                                    .Where(s => !s.Equals(Environment.NewLine))
                                                    .Where(s => !s.Equals("\r\n\r\n"))
                                                    .Where(s => !s.Equals("\r\n"))
                                              );
            }


            // Return Value
            return Video.passSingle;
        }


        /// <summary>
        ///     2-Pass Arguments
        /// </summary>      
        public static String TwoPassArgs(ViewModel vm)
        {
            // -------------------------
            //  2-Pass Auto Quality
            // -------------------------
            // Enabled 
            //
            if (vm.Pass_SelectedItem == "2 Pass"
                && vm.MediaType_SelectedItem == "Video" // video only
                && vm.VideoCodec_SelectedItem != "Copy" // exclude copy
                && vm.Container_SelectedItem != "ogv" // exclude ogv (special rule)
                )
            {
                // -------------------------
                // Pass 1
                // -------------------------
                List<string> FFmpegArgsPass1List = new List<string>()
                {
                    "\r\n\r\n" +
                    "-i "+ "\"" +
                    MainWindow.InputPath(vm) + "\"",

                    //"\r\n\r\n" + 
                    //Video.Subtitles(vm),

                    "\r\n\r\n" +
                    Video.VideoCodec(vm, vm.VideoCodec_Command),
                    "\r\n" +
                    Video.Speed(vm,
                                vm.VideoEncodeSpeed_Items,
                                vm.VideoEncodeSpeed_SelectedItem,
                                vm.Pass_SelectedItem
                                ),
                    Video.VideoQuality(vm,
                                       vm.VideoQuality_Items,
                                       vm.VideoQuality_SelectedItem,
                                       vm.Pass_SelectedItem
                                        ),
                    "\r\n" +
                    Video.PixFmt(vm,
                                 vm.PixelFormat_SelectedItem
                                 ),
                    "\r\n" +
                    Video.FPS(vm,
                              vm.FPS_SelectedItem
                             ),
                    "\r\n" +
                    VideoFilters.VideoFilter(vm),
                    //"\r\n" +
                    //Video.ScalingAlgorithm(vm),
                    "\r\n" +
                    Video.Images(vm),
                    "\r\n" +
                    Video.Optimize(vm,
                                   vm.Video_Optimize_Items,
                                   vm.Video_Optimize_SelectedItem
                                   ),
                    "\r\n" +
                    Video.Pass1Modifier(vm), // -pass 1, -x265-params pass=2

                    "\r\n\r\n" +
                    "-sn -an", // Disable Audio & Subtitles for Pass 1 to speed up encoding

                    "\r\n\r\n" +
                    Format.Cut(vm),
                    "\r\n\r\n" +
                    Format.ForceFormat(vm),
                    "\r\n\r\n" +
                    MainWindow.ThreadDetect(vm),

                    //"\r\n\r\n" + "\"" + MainWindow.OutputPath(vm) + "\""
                    "\r\n\r\n" +
                    "NUL"
                };

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                Video.pass1Args = string.Join(" ", FFmpegArgsPass1List
                                                   .Where(s => !string.IsNullOrEmpty(s))
                                                   .Where(s => !s.Equals(Environment.NewLine))
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
                    "\r\n\r\n" +
                    "&&",

                    "\r\n\r\n" +
                    MainWindow.FFmpegPath(),
                    "-y",

                    "\r\n\r\n" +
                    Video.HWAcceleration(vm),

                    "\r\n\r\n" +
                    "-i " + "\"" + MainWindow.InputPath(vm) + "\"",

                    "\r\n\r\n" +
                    Subtitle.SubtitlesExternal(vm),

                    "\r\n\r\n" +
                    Video.vCodec,
                    //Video.VideoCodec(vm, vm.VideoCodec_Command),
                    "\r\n" +
                    Video.vEncodeSpeed,
                    //Video.Speed(vm,
                    //            vm.VideoEncodeSpeed_Items,
                    //            vm.VideoEncodeSpeed_SelectedItem,
                    //            vm.Pass_SelectedItem
                    //            ),
                    Video.vQuality,
                    "\r\n" +
                    Video.pix_fmt,
                    //Video.PixFmt(vm,
                    //             vm.PixelFormat_SelectedItem
                    //             ),
                    "\r\n" +
                    Video.fps,
                    "\r\n" +
                    VideoFilters.vFilter,
                    //"\r\n" +
                    //Video.ScalingAlgorithm(vm),
                    "\r\n" +
                    Video.image,
                    "\r\n" +
                    Video.optimize,
                    "\r\n" +
                    Streams.VideoStreamMaps(vm),
                    "\r\n" +
                    Video.Pass2Modifier(vm), // -pass 2, -x265-params pass=2

                    "\r\n\r\n" +
                    Subtitle.SubtitleCodec(vm.SubtitleCodec_Command),
                    "\r\n" +
                    Streams.SubtitleMaps(vm),

                    "\r\n\r\n" +
                    Audio.AudioCodec(vm, vm.AudioCodec_Command),
                    "\r\n" +
                    Audio.AudioQuality(//vm
                                       vm.Batch_IsChecked,
                                       vm.AudioVBR_IsChecked,
                                       vm.AudioCodec_SelectedItem,
                                       vm.AudioQuality_Items,
                                       vm.AudioQuality_SelectedItem,
                                       vm.AudioBitrate_Text
                                       ),
                    Audio.SampleRate(vm),
                    Audio.BitDepth(vm,
                                   vm.AudioBitDepth_Items,
                                   vm.AudioBitDepth_SelectedItem
                                   ),
                    Audio.Channel(vm,
                                  vm.AudioChannel_SelectedItem
                                  ),
                    "\r\n" +
                    AudioFilters.AudioFilter(vm),
                    "\r\n" +
                    Streams.AudioStreamMaps(vm),

                    "\r\n\r\n" +
                    Format.trim,

                    "\r\n\r\n" +
                    Streams.FormatMaps(vm),

                    "\r\n\r\n" +
                    Format.ForceFormat(vm),

                    "\r\n\r\n" +
                    Configure.threads,

                    "\r\n\r\n" +
                    "\"" + MainWindow.OutputPath(vm) + "\""
                };

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                Video.pass2Args = string.Join(" ", FFmpegArgsPass2List
                                                   .Where(s => !string.IsNullOrEmpty(s))
                                                   .Where(s => !s.Equals(Environment.NewLine))
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
        ///     FFmpeg Single File - Generate Args
        /// </summary>
        public static String FFmpegSingleGenerateArgs(ViewModel vm)
        {
            if (vm.Batch_IsChecked == false)
            {
                // Make Arugments List
                List<string> FFmpegArgsList = new List<string>()
                {
                    //MainWindow.YouTubeDownload(MainWindow.InputPath(vm)),
                    MainWindow.FFmpegPath(),
                    "-y",
                    "\r\n\r\n" + Video.HWAcceleration(vm),
                    OnePassArgs(vm), //disabled if 2-Pass
                    TwoPassArgs(vm) //disabled if 1-Pass
                };

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                ffmpegArgsSort = string.Join(" ", FFmpegArgsList
                                                  .Where(s => !string.IsNullOrEmpty(s))
                                                  .Where(s => !s.Equals(Environment.NewLine))
                                                  .Where(s => !s.Equals("\r\n\r\n"))
                                                  .Where(s => !s.Equals("\r\n"))
                                            );

                // Inline 
                ffmpegArgs = MainWindow.RemoveLineBreaks(
                                            string.Join(" ", FFmpegArgsList
                                                             .Where(s => !string.IsNullOrEmpty(s))
                                                             .Where(s => !s.Equals(Environment.NewLine))
                                                             .Where(s => !s.Equals("\r\n\r\n"))
                                                             .Where(s => !s.Equals("\r\n"))
                                                        )
                                        );

                //.Replace("\r\n", "") //Remove Linebreaks
                //.Replace(Environment.NewLine, "")
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
        ///     FFmpeg Batch - Generate Args
        /// </summary>
        public static void FFmpegBatchGenerateArgs(ViewModel vm)
        {
            if (vm.Batch_IsChecked == true)
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
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(vm.Batch_IsChecked)) { Foreground = Log.ConsoleDefault });
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
                    "\"" + MainWindow.BatchInputDirectory(vm) + "\"",

                    "\r\n\r\n" + "&& for %f in",
                    "(*" + MainWindow.inputExt + ")",
                    "do (echo)",

                    "\r\n\r\n" + Video.BatchVideoQualityAuto(vm),

                    "\r\n\r\n" + Audio.BatchAudioQualityAuto(vm),
                    "\r\n\r\n" + Audio.BatchAudioBitrateLimiter(vm),

                    "\r\n\r\n" + "&&",
                    "\r\n\r\n" + MainWindow.FFmpegPath(),
                    "\r\n\r\n" + Video.HWAcceleration(vm),
                    "-y",
                    //%~f added in InputPath()

                    OnePassArgs(vm), //disabled if 2-Pass       
                    TwoPassArgs(vm) //disabled if 1-Pass
                };

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                ffmpegArgsSort = string.Join(" ", FFmpegBatchArgsList
                                                  .Where(s => !string.IsNullOrEmpty(s))
                                                  .Where(s => !s.Equals(Environment.NewLine))
                                                  .Where(s => !s.Equals("\r\n\r\n"))
                                                  .Where(s => !s.Equals("\r\n"))
                                            );

                // Inline 
                ffmpegArgs = MainWindow.RemoveLineBreaks(
                                            string.Join(" ", FFmpegBatchArgsList
                                                            .Where(s => !string.IsNullOrEmpty(s))
                                                            .Where(s => !s.Equals(Environment.NewLine))
                                                            .Where(s => !s.Equals("\r\n\r\n"))
                                                            .Where(s => !s.Equals("\r\n"))
                                                        )
                                        );
            }
        }


        // --------------------------------------------------------------------------------------------------------


        /// <summary>
        ///     FFmpeg Generate Script
        /// </summary>
        public static void FFmpegScript(ViewModel vm)
        {
            // Clear Old Text
            //ScriptView.ClearScriptView(vm);
            //ScriptView.scriptParagraph.Inlines.Clear();

            // Write FFmpeg Args
            //mainwindow.rtbScriptView.Document = new FlowDocument(ScriptView.scriptParagraph);
            //mainwindow.rtbScriptView.BeginChange();
            //ScriptView.scriptParagraph.Inlines.Add(new Run(ffmpegArgs));
            //mainwindow.rtbScriptView.EndChange();
            //vm.ScriptView_Text = string.Join<char>(" ", ffmpegArgs);
            vm.ScriptView_Text = ffmpegArgs;
        }


        /// <summary>
        /// FFmpeg Start
        /// </summary>
        public static void FFmpegStart(ViewModel vm)
        {
            System.Diagnostics.Process.Start(
                "cmd.exe",
                KeepWindow(vm)
                + " cd " + "\"" + MainWindow.outputDir + "\""
                + " & "
                + ffmpegArgs
            );
        }

        /// <summary>
        /// FFmpeg Convert
        /// </summary>
        public static void FFmpegConvert(ViewModel vm)
        {
            //// -------------------------
            //// Use User Custom Script Args
            //// -------------------------
            //// Check if Set Controls Differ from Script TextBox. If so, Script has been edited and is custom..
            //if (!string.IsNullOrWhiteSpace(ScriptView.GetScriptRichTextBoxContents(vm)) // Script is not Empty
            //    && MainWindow.ReplaceLineBreaksWithSpaces(ScriptView.GetScriptRichTextBoxContents(vm))

            //    != ffmpegArgs // Set Controls Args
            //    )
            //{
            //    // CMD Arguments are from Script TextBox
            //    // Stays Sorted
            //    ffmpegArgs = MainWindow.ReplaceLineBreaksWithSpaces(ScriptView.GetScriptRichTextBoxContents(vm));
            //}

            //// -------------------------
            //// Generate Controls Script
            //// -------------------------
            //else
            //{
            //    // Inline
            //    FFmpegScript(vm);
            //}


            // -------------------------
            // Generate Controls Script
            // -------------------------
            // Inline
            FFmpegScript(vm);

            // -------------------------
            // Start FFmpeg
            // -------------------------
            FFmpegStart(vm);
        }

    }
}