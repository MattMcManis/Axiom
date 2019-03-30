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

 * KeepWindow
 * OnePassArgs
 * TwoPassArgs
 * FFmpegSingleGenerateArgs
 * FFmpegBatchGenerateArgs
 * FFmpegScript
 * FFmpegStart
 * FFmpegConvert
---------------------------------- */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
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
        ///     Global Variables
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        // FFmepg / FFprobe
        public static string ffmpeg; // ffmpeg.exe location
        public static string ffmpegArgs; // FFmpeg Arguments
        public static string ffmpegArgsSort; // FFmpeg Arguments Sorted


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Process Methods
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        ///     Keep FFmpegWindow Switch (Method)
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
            if (vm.Video_Pass_SelectedItem == "1 Pass" ||
                vm.Video_Pass_SelectedItem == "CRF" ||
                vm.Video_Pass_SelectedItem == "auto" ||
                vm.Format_Container_SelectedItem == "ogv" //ogv (special rule)
                )
            {
                // -------------------------
                //  Arguments List
                // -------------------------
                List<string> FFmpegArgsSinglePassList = new List<string>()
                {
                    "\r\n\r\n" +
                    "-i "+ "\"" + MainWindow.InputPath(vm, "pass 1") + "\"",

                    "\r\n\r\n" +
                    Format.CutStart(vm.Input_Text,
                                    vm.Batch_IsChecked,
                                    vm.Format_MediaType_SelectedItem,
                                    vm.Video_Codec_SelectedItem,
                                    vm.Video_Quality_SelectedItem,
                                    vm.Format_Cut_SelectedItem,
                                    vm.Format_CutStart_Hours_Text,
                                    vm.Format_CutStart_Minutes_Text,
                                    vm.Format_CutStart_Seconds_Text,
                                    vm.Format_CutStart_Milliseconds_Text,
                                    vm.Format_FrameStart_Text
                                    ),
                    Format.CutEnd(vm.Input_Text,
                                  vm.Batch_IsChecked,
                                  vm.Format_MediaType_SelectedItem,
                                  vm.Video_Codec_SelectedItem,
                                  vm.Video_Quality_SelectedItem,
                                  vm.Format_Cut_SelectedItem,
                                  vm.Format_CutEnd_Hours_Text,
                                  vm.Format_CutEnd_Minutes_Text,
                                  vm.Format_CutEnd_Seconds_Text,
                                  vm.Format_CutEnd_Milliseconds_Text,
                                  vm.Format_FrameEnd_Text
                                  ),

                    "\r\n\r\n" +
                    Subtitle.SubtitlesExternal(vm.Subtitle_Codec_SelectedItem,
                                               vm.Subtitle_Stream_SelectedItem
                                               ),

                    "\r\n\r\n" +
                    Video.VideoCodec(vm.Format_HWAccel_SelectedItem,
                                     vm.Video_Codec_SelectedItem,
                                     vm.Video_Codec
                                     ),
                    "\r\n" +
                    Video.VideoEncodeSpeed(vm.Video_EncodeSpeed_Items,
                                           vm.Video_EncodeSpeed_SelectedItem,
                                           vm.Format_MediaType_SelectedItem,
                                           vm.Video_Codec_SelectedItem,
                                           vm.Video_Quality_SelectedItem,
                                           vm.Video_Pass_SelectedItem
                                           ),

                    Video.VideoQuality(vm.Batch_IsChecked,
                                       vm.Video_VBR_IsChecked,
                                       vm.Format_Container_SelectedItem,
                                       vm.Format_MediaType_SelectedItem,
                                       vm.Video_Codec_SelectedItem,
                                       vm.Video_Quality_Items,
                                       vm.Video_Quality_SelectedItem,
                                       vm.Video_Pass_SelectedItem,
                                       vm.Video_CRF_Text,
                                       vm.Video_BitRate_Text,
                                       vm.Video_MinRate_Text,
                                       vm.Video_MaxRate_Text,
                                       vm.Video_BufSize_Text,
                                       vm.Input_Text
                                       ),
                    "\r\n" +
                    Video.PixFmt(vm.Format_MediaType_SelectedItem,
                                 vm.Video_Codec_SelectedItem,
                                 vm.Video_Quality_SelectedItem,
                                 vm.Video_PixelFormat_SelectedItem
                                 ),
                    "\r\n" +
                    Video.FPS(vm.Format_MediaType_SelectedItem,
                              vm.Video_Codec_SelectedItem,
                              vm.Video_Quality_SelectedItem,
                              vm.Video_FPS_SelectedItem,
                              vm.Video_FPS_Text
                              ),
                    "\r\n" +
                    VideoFilters.VideoFilter(vm),
                    "\r\n" +
                    Video.AspectRatio(vm.Format_MediaType_SelectedItem,
                                      vm.Video_Codec_SelectedItem,
                                      vm.Video_Quality_SelectedItem,
                                      vm.Video_AspectRatio_SelectedItem
                                      ),
                    "\r\n" +
                    Video.Images(vm.Format_MediaType_SelectedItem,
                                 vm.Video_Codec_SelectedItem,
                                 vm.Video_Quality_SelectedItem
                                 ),
                    "\r\n" +
                    Video.Optimize(vm.Format_MediaType_SelectedItem,
                                   vm.Video_Codec_SelectedItem,
                                   vm.Video_Quality_SelectedItem,
                                   vm.Video_Optimize_Items,
                                   vm.Video_Optimize_SelectedItem,
                                   vm.Video_Optimize_Tune_SelectedItem,
                                   vm.Video_Optimize_Profile_SelectedItem,
                                   vm.Video_Optimize_Level_SelectedItem
                                   ),
                    "\r\n" +
                    Streams.VideoStreamMaps(vm),

                    "\r\n\r\n" +
                    Subtitle.SubtitleCodec(vm.Subtitle_Codec_SelectedItem,
                                           vm.Subtitle_Codec
                                           ),
                    "\r\n" +
                    Streams.SubtitleMaps(vm),

                    "\r\n\r\n" +
                    Audio.AudioCodec(vm.Audio_Codec_SelectedItem,
                                     vm.Audio_Codec,
                                     vm.Audio_BitDepth_SelectedItem,
                                     vm.Input_Text
                                     ),
                    "\r\n" +
                    Audio.AudioQuality(vm.Input_Text,
                                       vm.Batch_IsChecked,
                                       vm.Format_MediaType_SelectedItem,
                                       vm.Audio_Stream_SelectedItem,
                                       vm.Audio_Codec_SelectedItem,
                                       vm.Audio_Quality_Items,
                                       vm.Audio_Quality_SelectedItem,
                                       vm.Audio_BitRate_Text,
                                       vm.Audio_VBR_IsChecked
                                       ),
                    Audio.CompressionLevel(vm.Format_MediaType_SelectedItem,
                                           vm.Audio_Codec_SelectedItem,
                                           vm.Audio_Stream_SelectedItem,
                                           vm.Audio_Quality_SelectedItem,
                                           vm.Audio_CompressionLevel_SelectedItem
                                           ),
                    Audio.SampleRate(vm.Format_MediaType_SelectedItem,
                                     vm.Audio_Codec_SelectedItem,
                                     vm.Audio_Stream_SelectedItem,
                                     vm.Audio_Quality_SelectedItem,
                                     vm.Audio_Channel_SelectedItem,
                                     vm.Audio_SampleRate_Items,
                                     vm.Audio_SampleRate_SelectedItem
                                     ),
                    Audio.BitDepth(vm.Format_MediaType_SelectedItem,
                                   vm.Audio_Codec_SelectedItem,
                                   vm.Audio_Stream_SelectedItem,
                                   vm.Audio_Quality_SelectedItem,
                                   vm.Audio_BitDepth_Items,
                                   vm.Audio_BitDepth_SelectedItem
                                   ),
                    Audio.Channel(vm.Format_MediaType_SelectedItem,
                                  vm.Audio_Codec_SelectedItem,
                                  vm.Audio_Stream_SelectedItem,
                                  vm.Audio_Quality_SelectedItem,
                                  vm.Audio_Channel_SelectedItem
                                  ),
                    "\r\n" +
                    AudioFilters.AudioFilter(vm),
                    "\r\n" +
                    Streams.AudioStreamMaps(vm),

                    "\r\n\r\n" +
                    Streams.FormatMaps(vm),

                    "\r\n\r\n" +
                    Format.ForceFormat(vm.Format_Container_SelectedItem),

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
            if (vm.Video_Pass_SelectedItem == "2 Pass" &&
                vm.Format_MediaType_SelectedItem == "Video" &&  // video only
                vm.Video_Codec_SelectedItem != "Copy" &&  // exclude copy
                vm.Format_Container_SelectedItem != "ogv" // exclude ogv (special rule)
                )
            {
                // -------------------------
                // Pass 1
                // -------------------------
                List<string> FFmpegArgsPass1List = new List<string>()
                {
                    "\r\n\r\n" +
                    "-i "+ "\"" +
                    MainWindow.InputPath(vm, "pass 1") + "\"",

                    "\r\n\r\n" +
                    Format.CutStart(vm.Input_Text,
                                    vm.Batch_IsChecked,
                                    vm.Format_MediaType_SelectedItem,
                                    vm.Video_Codec_SelectedItem,
                                    vm.Video_Quality_SelectedItem,
                                    vm.Format_Cut_SelectedItem,
                                    vm.Format_CutStart_Hours_Text,
                                    vm.Format_CutStart_Minutes_Text,
                                    vm.Format_CutStart_Seconds_Text,
                                    vm.Format_CutStart_Milliseconds_Text,
                                    vm.Format_FrameStart_Text
                                    ),
                    Format.CutEnd(vm.Input_Text,
                                  vm.Batch_IsChecked,
                                  vm.Format_MediaType_SelectedItem,
                                  vm.Video_Codec_SelectedItem,
                                  vm.Video_Quality_SelectedItem,
                                  vm.Format_Cut_SelectedItem,
                                  vm.Format_CutEnd_Hours_Text,
                                  vm.Format_CutEnd_Minutes_Text,
                                  vm.Format_CutEnd_Seconds_Text,
                                  vm.Format_CutEnd_Milliseconds_Text,
                                  vm.Format_FrameEnd_Text
                                  ),

                    "\r\n\r\n" +
                    Video.VideoCodec(vm.Format_HWAccel_SelectedItem,
                                     vm.Video_Codec_SelectedItem,
                                     vm.Video_Codec
                                     ),
                    "\r\n" +
                    Video.VideoEncodeSpeed(vm.Video_EncodeSpeed_Items,
                                           vm.Video_EncodeSpeed_SelectedItem,
                                           vm.Format_MediaType_SelectedItem,
                                           vm.Video_Codec_SelectedItem,
                                           vm.Video_Quality_SelectedItem,
                                           vm.Video_Pass_SelectedItem
                                           ),

                    Video.VideoQuality(vm.Batch_IsChecked,
                                       vm.Video_VBR_IsChecked,
                                       vm.Format_Container_SelectedItem,
                                       vm.Format_MediaType_SelectedItem,
                                       vm.Video_Codec_SelectedItem,
                                       vm.Video_Quality_Items,
                                       vm.Video_Quality_SelectedItem,
                                       vm.Video_Pass_SelectedItem,
                                       vm.Video_CRF_Text,
                                       vm.Video_BitRate_Text,
                                       vm.Video_MinRate_Text,
                                       vm.Video_MaxRate_Text,
                                       vm.Video_BufSize_Text,
                                       vm.Input_Text
                                       ),
                    "\r\n" +
                    Video.PixFmt(vm.Format_MediaType_SelectedItem,
                                 vm.Video_Codec_SelectedItem,
                                 vm.Video_Quality_SelectedItem,
                                 vm.Video_PixelFormat_SelectedItem
                                 ),
                    "\r\n" +
                    Video.FPS(vm.Format_MediaType_SelectedItem,
                              vm.Video_Codec_SelectedItem,
                              vm.Video_Quality_SelectedItem,
                              vm.Video_FPS_SelectedItem,
                              vm.Video_FPS_Text
                              ),
                    "\r\n" +
                    VideoFilters.VideoFilter(vm),
                    "\r\n" +
                    Video.AspectRatio(vm.Format_MediaType_SelectedItem,
                                      vm.Video_Codec_SelectedItem,
                                      vm.Video_Quality_SelectedItem,
                                      vm.Video_AspectRatio_SelectedItem
                                      ),
                    "\r\n" +
                    Video.Images(vm.Format_MediaType_SelectedItem,
                                 vm.Video_Codec_SelectedItem,
                                 vm.Video_Quality_SelectedItem
                                 ),
                    "\r\n" +
                    Video.Optimize(vm.Format_MediaType_SelectedItem,
                                   vm.Video_Codec_SelectedItem,
                                   vm.Video_Quality_SelectedItem,
                                   vm.Video_Optimize_Items,
                                   vm.Video_Optimize_SelectedItem,
                                   vm.Video_Optimize_Tune_SelectedItem,
                                   vm.Video_Optimize_Profile_SelectedItem,
                                   vm.Video_Optimize_Level_SelectedItem
                                   ),

                    // -pass 1, -x265-params pass=2
                    "\r\n" +
                    Video.Pass1Modifier(vm.Video_Codec_SelectedItem, 
                                        vm.Video_Pass_SelectedItem
                                        ),  

                    // Disable Audio & Subtitles for Pass 1 to speed up encoding
                    "\r\n\r\n" +
                    "-sn -an", 

                    "\r\n\r\n" +
                    Format.ForceFormat(vm.Format_Container_SelectedItem),
                    "\r\n\r\n" +
                    MainWindow.ThreadDetect(vm),

                    // Output Path Null
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
                    MainWindow.FFmpegPath(vm),
                    "-y",

                    "\r\n\r\n" +
                    Video.HWAcceleration(vm.Format_MediaType_SelectedItem,
                                         vm.Video_Codec_SelectedItem,
                                         vm.Format_HWAccel_SelectedItem
                                         ),

                    "\r\n\r\n" +
                    Format.trimStart,

                    "\r\n\r\n" +
                    "-i " + "\"" + MainWindow.InputPath(vm, "pass 2") + "\"",
                    //"-i " + "\"" + MainWindow.input + "\"",

                    "\r\n\r\n" +
                    Format.trimEnd,

                    "\r\n\r\n" +
                    Subtitle.SubtitlesExternal(vm.Subtitle_Codec_SelectedItem,
                                               vm.Subtitle_Stream_SelectedItem
                                               ),

                    "\r\n\r\n" +
                    Video.vCodec,
                    "\r\n" +
                    Video.vEncodeSpeed,
                    Video.vQuality,
                    "\r\n" +
                    Video.pix_fmt,
                    "\r\n" +
                    Video.fps,
                    "\r\n" +
                    VideoFilters.vFilter,
                    "\r\n" +
                    Video.vAspectRatio,
                    "\r\n" +
                    Video.image,
                    "\r\n" +
                    Video.optimize,
                    "\r\n" +
                    Streams.VideoStreamMaps(vm),
                    "\r\n" +
                    Video.Pass2Modifier(vm.Video_Codec_SelectedItem, // -pass 2, -x265-params pass=2
                                        vm.Video_Pass_SelectedItem
                                        ), 

                    "\r\n\r\n" +
                    Subtitle.SubtitleCodec(vm.Subtitle_Codec_SelectedItem,
                                           vm.Subtitle_Codec
                                           ),
                    "\r\n" +
                    Streams.SubtitleMaps(vm),

                    "\r\n\r\n" +
                    Audio.AudioCodec(vm.Audio_Codec_SelectedItem,
                                     vm.Audio_Codec,
                                     vm.Audio_BitDepth_SelectedItem,
                                     vm.Input_Text
                                     ),
                    "\r\n" +
                    Audio.AudioQuality(vm.Input_Text,
                                       vm.Batch_IsChecked,
                                       vm.Format_MediaType_SelectedItem,
                                       vm.Audio_Stream_SelectedItem,
                                       vm.Audio_Codec_SelectedItem,
                                       vm.Audio_Quality_Items,
                                       vm.Audio_Quality_SelectedItem,
                                       vm.Audio_BitRate_Text,
                                       vm.Audio_VBR_IsChecked
                                       ),
                    Audio.CompressionLevel(vm.Format_MediaType_SelectedItem,
                                           vm.Audio_Codec_SelectedItem,
                                           vm.Audio_Stream_SelectedItem,
                                           vm.Audio_Quality_SelectedItem,
                                           vm.Audio_CompressionLevel_SelectedItem
                                           ),
                    Audio.SampleRate(vm.Format_MediaType_SelectedItem,
                                     vm.Audio_Codec_SelectedItem,
                                     vm.Audio_Stream_SelectedItem,
                                     vm.Audio_Quality_SelectedItem,
                                     vm.Audio_Channel_SelectedItem,
                                     vm.Audio_SampleRate_Items,
                                     vm.Audio_SampleRate_SelectedItem
                                     ),
                    Audio.BitDepth(vm.Format_MediaType_SelectedItem,
                                   vm.Audio_Codec_SelectedItem,
                                   vm.Audio_Stream_SelectedItem,
                                   vm.Audio_Quality_SelectedItem,
                                   vm.Audio_BitDepth_Items,
                                   vm.Audio_BitDepth_SelectedItem
                                   ),
                    Audio.Channel(vm.Format_MediaType_SelectedItem,
                                  vm.Audio_Codec_SelectedItem,
                                  vm.Audio_Stream_SelectedItem,
                                  vm.Audio_Quality_SelectedItem,
                                  vm.Audio_Channel_SelectedItem
                                  ),
                    "\r\n" +
                    AudioFilters.AudioFilter(vm),
                    "\r\n" +
                    Streams.AudioStreamMaps(vm),

                    "\r\n\r\n" +
                    Streams.FormatMaps(vm),

                    "\r\n\r\n" +
                    Format.ForceFormat(vm.Format_Container_SelectedItem),

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
                    MainWindow.FFmpegPath(vm),
                    "-y",
                    "\r\n\r\n" + Video.HWAcceleration(vm.Format_MediaType_SelectedItem,
                                                      vm.Video_Codec_SelectedItem,
                                                      vm.Format_HWAccel_SelectedItem
                                                      ),
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
                ffmpegArgs = MainWindow.RemoveLineBreaks(string.Join(" ", FFmpegArgsList
                                                               .Where(s => !string.IsNullOrEmpty(s))
                                                               .Where(s => !s.Equals(Environment.NewLine))
                                                               .Where(s => !s.Equals("\r\n\r\n"))
                                                               .Where(s => !s.Equals("\r\n"))
                                                                    )
                                                        );
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
                    //Log.logParagraph.Inlines.Add(new LineBreak());
                    //Log.logParagraph.Inlines.Add(new LineBreak());
                    //Log.logParagraph.Inlines.Add(new Bold(new Run("Running Batch Convert...")) { Foreground = Log.ConsoleAction });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Run(ffmpegArgs) { Foreground = Log.ConsoleDefault });
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

                    // Video
                    "\r\n\r\n" + Video.BatchVideoQualityAuto(vm.Batch_IsChecked,
                                                             vm.Format_MediaType_SelectedItem,
                                                             vm.Video_Codec_SelectedItem,
                                                             vm.Video_Quality_SelectedItem 
                                                             ),

                    // Audio
                    "\r\n\r\n" + Audio.BatchAudioQualityAuto(vm.Batch_IsChecked,
                                                             vm.Format_MediaType_SelectedItem,
                                                             vm.Audio_Codec_SelectedItem,
                                                             vm.Audio_Stream_SelectedItem,
                                                             vm.Audio_Quality_SelectedItem
                                                             ),
                    "\r\n\r\n" + Audio.BatchAudioBitRateLimiter(vm.Format_MediaType_SelectedItem,
                                                                vm.Audio_Codec_SelectedItem,
                                                                vm.Audio_Stream_SelectedItem,
                                                                vm.Audio_Quality_SelectedItem
                                                                ),

                    "\r\n\r\n" + "&&",
                    "\r\n\r\n" + MainWindow.FFmpegPath(vm),
                    "\r\n\r\n" + Video.HWAcceleration(vm.Format_MediaType_SelectedItem,
                                                      vm.Video_Codec_SelectedItem,
                                                      vm.Format_HWAccel_SelectedItem
                                                      ),
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
                ffmpegArgs = MainWindow.RemoveLineBreaks(string.Join(" ", FFmpegBatchArgsList
                                                               .Where(s => !string.IsNullOrEmpty(s))
                                                               .Where(s => !s.Equals(Environment.NewLine))
                                                               .Where(s => !s.Equals("\r\n\r\n"))
                                                               .Where(s => !s.Equals("\r\n"))
                                                        )
                                        );
            }
        }


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------


        /// <summary>
        ///     YouTube Download - Generate Args
        /// </summary>
        public static void YouTubeDownloadGenerateArgs(ViewModel vm)
        {
            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("YouTube Download: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(Convert.ToString(vm.Batch_IsChecked)) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Generating Script...")) { Foreground = Log.ConsoleTitle });
                //Log.logParagraph.Inlines.Add(new LineBreak());
                //Log.logParagraph.Inlines.Add(new LineBreak());
                //Log.logParagraph.Inlines.Add(new Bold(new Run("Running Convert...")) { Foreground = Log.ConsoleAction });
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Run(ffmpegArgs) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);


            // -------------------------
            // YouTube Download Arguments Full
            // -------------------------
            // Download
            //
            List<string> youtubedlArgs = new List<string>()
            {
                "cd /d",
                "\"" + MainWindow.downloadDir + "\"",

                "\r\n\r\n" + "&&",

                "\r\n\r\n" + "for /f \"delims=\" %f in ('",

                // Get Title
                "\r\n\r\n" + "@" + "\"" + MainWindow.youtubedl + "\"",
                "\r\n"  + " --get-filename -o \"%(title)s\" " + "\"" + MainWindow.YouTubeDownloadURL(vm.Input_Text) + "\"",
                "\r\n" + "')",

                // Download Video
                "\r\n\r\n" + "do (",
                "\r\n\r\n" + "@" + "\"" + MainWindow.youtubedl + "\"",

                "\r\n\r\n" + " -f " + MainWindow.YouTubeDownloadQuality(vm.Input_Text, 
                                                                        vm.Format_YouTube_SelectedItem, 
                                                                        vm.Format_YouTube_Quality_SelectedItem
                                                                        ),
                "\r\n\r\n" + "\"" + vm.Input_Text + "\"",
                "\r\n" +" -o " + "\"" + MainWindow.downloadDir + "%f" + "." + MainWindow.YouTubeDownloadFormat(vm.Format_YouTube_SelectedItem,
                                                                                                               vm.Video_Codec_SelectedItem,
                                                                                                               vm.Subtitle_Codec_SelectedItem,
                                                                                                               vm.Audio_Codec_SelectedItem
                                                                                                               ) + "\"",

                // FFmpeg Location
                "\r\n\r\n" + MainWindow.YouTubeDL_FFmpegPath(vm),

                // Merge Output Format
                "\r\n\r\n" + "--merge-output-format " + MainWindow.YouTubeDownloadFormat(vm.Format_YouTube_SelectedItem,
                                                                                         vm.Video_Codec_SelectedItem,
                                                                                         vm.Subtitle_Codec_SelectedItem,
                                                                                         vm.Audio_Codec_SelectedItem
                                                                                         )
            };

            // FFmpeg Args
            //
            List<string> FFmpegArgs = new List<string>()
            {
                "\r\n\r\n" + "&&",
                "\r\n\r\n" + MainWindow.FFmpegPath(vm),
                "\r\n\r\n" + Video.HWAcceleration(vm.Format_MediaType_SelectedItem,
                                                  vm.Video_Codec_SelectedItem,
                                                  vm.Format_HWAccel_SelectedItem
                                                  ),
                "-y",

                OnePassArgs(vm), //disabled if 2-Pass       
                TwoPassArgs(vm), //disabled if 1-Pass

                "\r\n\r\n" + "&&",

                // Delete Downloaded File
                "\r\n\r\n" + "del " + "\"" + MainWindow.downloadDir + "%f" + "." + MainWindow.YouTubeDownloadFormat(vm.Format_YouTube_SelectedItem, 
                                                                                                                    vm.Video_Codec_SelectedItem,
                                                                                                                    vm.Subtitle_Codec_SelectedItem,
                                                                                                                    vm.Audio_Codec_SelectedItem
                                                                                                                    ) + "\"",
            };


            // -------------------------
            // Download Only
            // -------------------------
            if (MainWindow.IsWebDownloadOnly(vm.Video_Codec_SelectedItem,
                                                 vm.Subtitle_Codec_SelectedItem,
                                                 vm.Audio_Codec_SelectedItem) == true
                                                 )
            {
                // Add "do" Closing Tag
                youtubedlArgs.Add("\r\n)");

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                ffmpegArgsSort = string.Join(" ", youtubedlArgs
                                                .Where(s => !string.IsNullOrEmpty(s))
                                                .Where(s => !s.Equals(Environment.NewLine))
                                                .Where(s => !s.Equals("\r\n\r\n"))
                                                .Where(s => !s.Equals("\r\n"))
                                        );

                // Inline 
                ffmpegArgs = MainWindow.RemoveLineBreaks(string.Join(" ", youtubedlArgs
                                                               .Where(s => !string.IsNullOrEmpty(s))
                                                               .Where(s => !s.Equals(Environment.NewLine))
                                                               .Where(s => !s.Equals("\r\n\r\n"))
                                                               .Where(s => !s.Equals("\r\n"))
                                                        )
                                        );
            }

            // -------------------------
            // Download & Convert
            // -------------------------
            else
            {
                // Join YouTube Args & FFmpeg Args
                youtubedlArgs.AddRange(FFmpegArgs);
                // Add "do" Closing Tag
                youtubedlArgs.Add("\r\n)");

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                ffmpegArgsSort = string.Join(" ", youtubedlArgs
                                                .Where(s => !string.IsNullOrEmpty(s))
                                                .Where(s => !s.Equals(Environment.NewLine))
                                                .Where(s => !s.Equals("\r\n\r\n"))
                                                .Where(s => !s.Equals("\r\n"))
                                        );

                // Inline 
                ffmpegArgs = MainWindow.RemoveLineBreaks(string.Join(" ", youtubedlArgs
                                                                .Where(s => !string.IsNullOrEmpty(s))
                                                                .Where(s => !s.Equals(Environment.NewLine))
                                                                .Where(s => !s.Equals("\r\n\r\n"))
                                                                .Where(s => !s.Equals("\r\n"))
                                                        )
                                        );
            }
        }


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------


        /// <summary>
        ///     FFmpeg Generate Script
        /// </summary>
        public static void FFmpegScript(ViewModel vm)
        {
            // Write FFmpeg Args
            vm.ScriptView_Text = ffmpegArgs;
        }


        /// <summary>
        ///     FFmpeg Start
        /// </summary>
        public static void FFmpegStart(ViewModel vm)
        {
            // Start FFmpeg Process
            System.Diagnostics.Process.Start("cmd.exe",
                                             KeepWindow(vm)
                                             + " cd " + "\"" + MainWindow.outputDir + "\""
                                             + " & "
                                             + ffmpegArgs
                                             );
        }


        /// <summary>
        ///     FFmpeg Convert
        /// </summary>
        public static void FFmpegConvert(ViewModel vm)
        {
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Running Convert...")) { Foreground = Log.ConsoleAction });
            };
            Log.LogActions.Add(Log.WriteAction);

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
