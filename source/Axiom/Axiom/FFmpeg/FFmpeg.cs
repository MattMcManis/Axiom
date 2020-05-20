/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2020 Matt McManis
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
 * YouTubeDownloadGenerateArgs
 * FFmpegScript
 * FFmpegStart
 * FFmpegConvert
---------------------------------- */

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    public class FFmpeg
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Global Variables
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        public static string ffmpeg { get; set; } // ffmpeg.exe location
        public static string ffmpegArgs { get; set; } // FFmpeg Arguments
        public static string ffmpegArgsSort { get; set; } // FFmpeg Arguments Sorted


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Process Methods
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Keep FFmpegWindow Switch (Method)
        /// </summary>
        /// <remarks>
        /// CMD.exe command, /k = keep, /c = close
        /// Do not .Close(); if using /c, it will throw a Dispose exception
        /// </remarks>
        public static String KeepWindow()
        {
            string window = string.Empty;

            // Shell Check
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // CMD
                case "CMD":
                    // Keep
                    if (VM.MainView.CMDWindowKeep_IsChecked == true)
                        window = "/k ";
                    // Close
                    else
                        window = "/c ";
                    break;

                // PowerShell
                case "PowerShell":
                    // Keep
                    if (VM.MainView.CMDWindowKeep_IsChecked == true)
                        window = "-NoExit ";
                    // Close
                    else
                        window = string.Empty;
                    break;
            }

            return window;
        }

        /// <summary>
        /// FFmpeg Invoke Operator for PowerShell
        /// </summary>
        public static String Exe_InvokeOperator_PowerShell()
        {
            // Shell Check
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // CMD
                case "CMD":
                    return string.Empty;

                // PowerShell
                case "PowerShell":
                    return "& ";

                // Default
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Logical Operator - And && - Shell Formatter
        /// </summary>
        public static String LogicalOperator_And_ShellFormatter()
        {
            // Shell Check
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // CMD
                case "CMD":
                    return "&&";

                // PowerShell
                case "PowerShell":
                    return ";";

                // Default
                default:
                    return "&&";
            }
        }


        /// <summary>
        /// 1-Pass Arguments
        /// </summary>
        // 1-Pass, CRF, & Auto
        public static String OnePass_CRF_Args()
        {
            // -------------------------
            //  Single Pass (1 Pass & CRF)
            // -------------------------
            if (VM.VideoView.Video_Pass_SelectedItem == "1 Pass" ||
                VM.VideoView.Video_Pass_SelectedItem == "CRF" ||
                VM.VideoView.Video_Pass_SelectedItem == "auto" ||
                VM.FormatView.Format_Container_SelectedItem == "ogv" //ogv (special rule)
                )
            {
                // -------------------------
                // HW Accel Decode
                // -------------------------
                List<string> hwAccelDecodeList = new List<string>()
                {
                    "\r\n\r\n" +
                    Video.HWAccelerationDecode(VM.FormatView.Format_MediaType_SelectedItem,
                                               VM.VideoView.Video_Codec_SelectedItem,
                                               VM.VideoView.Video_HWAccel_Decode_SelectedItem
                                               ),
                };

                // -------------------------
                // Input
                // -------------------------
                List<string> inputList = new List<string>()
                {
                    "\r\n\r\n" +
                    "-i " + "\"" + MainWindow.InputPath("pass 1") + "\"",
                    //"-i " + Path_ShellFormatter(MainWindow.InputPath("pass 1")),

                    "\r\n\r\n" +
                    Subtitle.SubtitlesExternal(VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                               VM.SubtitleView.Subtitle_Stream_SelectedItem
                                               ),
                };

                // -------------------------
                // HW Accel Transcode
                // -------------------------
                List<string> hwAccelTranscodeList = new List<string>()
                {
                    "\r\n\r\n" +
                    Video.HWAccelerationTranscode(VM.FormatView.Format_MediaType_SelectedItem,
                                                  VM.VideoView.Video_Codec_SelectedItem,
                                                  VM.VideoView.Video_HWAccel_Transcode_SelectedItem
                                                  ),
                };

                // -------------------------
                // Format
                // -------------------------
                List<string> formatList = new List<string>()
                {
                    "\r\n\r\n" +
                    Format.CutStart(VM.MainView.Input_Text,
                                    VM.MainView.Batch_IsChecked,
                                    VM.FormatView.Format_Cut_SelectedItem,
                                    VM.FormatView.Format_CutStart_Hours_Text,
                                    VM.FormatView.Format_CutStart_Minutes_Text,
                                    VM.FormatView.Format_CutStart_Seconds_Text,
                                    VM.FormatView.Format_CutStart_Milliseconds_Text,
                                    VM.FormatView.Format_FrameStart_Text
                                    ),
                    Format.CutEnd(VM.MainView.Input_Text,
                                  VM.MainView.Batch_IsChecked,
                                  VM.FormatView.Format_MediaType_SelectedItem,
                                  VM.FormatView.Format_Cut_SelectedItem,
                                  VM.FormatView.Format_CutEnd_Hours_Text,
                                  VM.FormatView.Format_CutEnd_Minutes_Text,
                                  VM.FormatView.Format_CutEnd_Seconds_Text,
                                  VM.FormatView.Format_CutEnd_Milliseconds_Text,
                                  VM.FormatView.Format_FrameEnd_Text
                                  ),
                };

                // -------------------------
                // Video
                // -------------------------
                List<string> videoList = new List<string>();

                if (VM.FormatView.Format_MediaType_SelectedItem != "Audio" &&
                    VM.VideoView.Video_Codec_SelectedItem != "None" &&
                    VM.VideoView.Video_Quality_SelectedItem != "None"
                    )
                {
                    videoList = new List<string>()
                    {
                        "\r\n\r\n" +
                        Video.VideoCodec(VM.VideoView.Video_HWAccel_Transcode_SelectedItem,
                                         VM.VideoView.Video_Codec_SelectedItem,
                                         VM.VideoView.Video_Codec
                                         ),

                        "\r\n" +
                        // No PassParams() for 1 Pass / CRF
                        VideoParams.Video_Params(VM.VideoView.Video_Quality_SelectedItem,
                                                 VM.VideoView.Video_Codec_SelectedItem,
                                                 VM.FormatView.Format_MediaType_SelectedItem
                                                 ),

                        "\r\n" +
                        Video.VideoEncodeSpeed(VM.VideoView.Video_EncodeSpeed_Items,
                                               VM.VideoView.Video_EncodeSpeed_SelectedItem,
                                               VM.VideoView.Video_Codec_SelectedItem,
                                               VM.VideoView.Video_Pass_SelectedItem
                                               ),

                        Video.VideoQuality(VM.MainView.Batch_IsChecked,
                                           VM.VideoView.Video_VBR_IsChecked,
                                           VM.FormatView.Format_Container_SelectedItem,
                                           VM.FormatView.Format_MediaType_SelectedItem,
                                           VM.VideoView.Video_Codec_SelectedItem,
                                           VM.VideoView.Video_Quality_Items,
                                           VM.VideoView.Video_Quality_SelectedItem,
                                           VM.VideoView.Video_Pass_SelectedItem,
                                           VM.VideoView.Video_CRF_Text,
                                           VM.VideoView.Video_BitRate_Text,
                                           VM.VideoView.Video_MinRate_Text,
                                           VM.VideoView.Video_MaxRate_Text,
                                           VM.VideoView.Video_BufSize_Text,
                                           VM.MainView.Input_Text
                                           ),

                        "\r\n" +
                        Video.PixFmt(VM.VideoView.Video_Codec_SelectedItem,
                                     VM.VideoView.Video_PixelFormat_SelectedItem
                                     ),

                        "\r\n" +
                        Video.Color_Primaries(VM.VideoView.Video_Color_Primaries_SelectedItem),
                        "\r\n" +
                        Video.Color_TransferCharacteristics(VM.VideoView.Video_Color_TransferCharacteristics_SelectedItem),
                        "\r\n" +
                        Video.Color_Space(VM.VideoView.Video_Color_Space_SelectedItem),
                        "\r\n" +
                        Video.Color_Range(VM.VideoView.Video_Color_Range_SelectedItem),

                        "\r\n" +
                        Video.FPS(VM.VideoView.Video_Codec_SelectedItem,
                                  VM.VideoView.Video_FPS_SelectedItem,
                                  VM.VideoView.Video_FPS_Text
                                  ),
                        "\r\n" +
                        VideoFilters.VideoFilter(),
                        "\r\n" +
                        Video.AspectRatio(VM.VideoView.Video_AspectRatio_SelectedItem),
                        "\r\n" +
                        Video.Images(VM.FormatView.Format_MediaType_SelectedItem,
                                     VM.VideoView.Video_Codec_SelectedItem
                                     ),
                        "\r\n" +
                        Video.Optimize(VM.VideoView.Video_Codec_SelectedItem,
                                       VM.VideoView.Video_Optimize_Items,
                                       VM.VideoView.Video_Optimize_SelectedItem,
                                       VM.VideoView.Video_Video_Optimize_Tune_SelectedItem,
                                       VM.VideoView.Video_Video_Optimize_Profile_SelectedItem,
                                       VM.VideoView.Video_Optimize_Level_SelectedItem
                                       ),
                        "\r\n" +
                        Streams.VideoStreamMaps(),
                    };
                }
                // Disable Video
                else
                {
                    videoList = new List<string>()
                    {
                        "\r\n\r\n" +
                        "-vn",
                    };
                }

                // -------------------------
                // Subtitle
                // -------------------------
                List<string> subtitleList = new List<string>();

                if (VM.FormatView.Format_MediaType_SelectedItem != "Audio" &&
                    VM.VideoView.Video_Codec_SelectedItem != "None" &&
                    VM.VideoView.Video_Quality_SelectedItem != "None"
                    )
                {
                    subtitleList = new List<string>()
                    {
                        "\r\n\r\n" +
                        Subtitle.SubtitleCodec(VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                               VM.SubtitleView.Subtitle_Codec
                                               ),
                        "\r\n" +
                        Streams.SubtitleMaps(),
                    };
                }
                // Disable Subtitles
                else
                {
                    subtitleList = new List<string>()
                    {
                        
                        "\r\n" +
                        "-sn",
                    };
                }

                // -------------------------
                // Audio
                // -------------------------
                List<string> audioList = new List<string>();

                if (VM.FormatView.Format_MediaType_SelectedItem != "Image" &&
                    VM.FormatView.Format_MediaType_SelectedItem != "Sequence" &&
                    VM.AudioView.Audio_Codec_SelectedItem != "None" &&
                    VM.AudioView.Audio_Stream_SelectedItem != "none" &&
                    VM.AudioView.Audio_Quality_SelectedItem != "None" &&
                    VM.AudioView.Audio_Quality_SelectedItem != "Mute"
                    )
                {
                    audioList = new List<string>()
                    {
                        "\r\n\r\n" +
                        Audio.AudioCodec(VM.AudioView.Audio_Codec_SelectedItem,
                                         VM.AudioView.Audio_Codec
                                         ),
                        "\r\n" +
                        Audio.AudioQuality(VM.MainView.Input_Text,
                                           VM.MainView.Batch_IsChecked,
                                           VM.FormatView.Format_MediaType_SelectedItem,
                                           VM.AudioView.Audio_Stream_SelectedItem,
                                           VM.AudioView.Audio_Codec_SelectedItem,
                                           VM.AudioView.Audio_Quality_Items,
                                           VM.AudioView.Audio_Quality_SelectedItem,
                                           VM.AudioView.Audio_BitRate_Text,
                                           VM.AudioView.Audio_VBR_IsChecked
                                           ),
                        Audio.CompressionLevel(VM.AudioView.Audio_Codec_SelectedItem,
                                               VM.AudioView.Audio_CompressionLevel_SelectedItem
                                               ),
                        Audio.SampleRate(VM.AudioView.Audio_Codec_SelectedItem,
                                         VM.AudioView.Audio_SampleRate_Items,
                                         VM.AudioView.Audio_SampleRate_SelectedItem
                                         ),
                        Audio.BitDepth(VM.AudioView.Audio_Codec_SelectedItem,
                                       VM.AudioView.Audio_BitDepth_Items,
                                       VM.AudioView.Audio_BitDepth_SelectedItem
                                       ),
                        Audio.Channel(VM.AudioView.Audio_Codec_SelectedItem,
                                      VM.AudioView.Audio_Channel_SelectedItem
                                      ),
                        "\r\n" +
                        AudioFilters.AudioFilter(),
                        "\r\n" +
                        Streams.AudioStreamMaps(),
                    };
                }
                // Disable Audio
                else
                {
                    audioList = new List<string>()
                    {
                        "\r\n" +
                        "-an",
                    };
                }

                // -------------------------
                // Output
                // -------------------------
                List<string> outputList = new List<string>()
                {
                    "\r\n\r\n" +
                    Streams.FormatMaps(),

                    "\r\n\r\n" +
                    Format.ForceFormat(VM.FormatView.Format_Container_SelectedItem),

                    "\r\n\r\n" +
                    MainWindow.ThreadDetect(),

                    "\r\n\r\n" +
                    "\"" + MainWindow.OutputPath() + "\""
                };
                

                // Combine Lists
                List<string> FFmpegArgsSinglePassList = hwAccelDecodeList
                                                        .Concat(inputList)
                                                        .Concat(hwAccelTranscodeList)
                                                        .Concat(formatList)
                                                        .Concat(videoList)
                                                        .Concat(subtitleList)
                                                        .Concat(audioList)
                                                        .Concat(outputList)
                                                        .ToList();

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                Video.passSingle = string.Join(" ", FFmpegArgsSinglePassList
                                                    .Where(s => !string.IsNullOrEmpty(s))
                                                    .Where(s => !string.IsNullOrWhiteSpace(s))
                                                    .Where(s => !s.Equals(Environment.NewLine))
                                                    .Where(s => !s.Equals("\r\n\r\n"))
                                                    .Where(s => !s.Equals("\r\n"))
                                              );
            }


            // Return Value
            return Video.passSingle;
        }


        /// <summary>
        /// 2-Pass Arguments
        /// </summary>      
        public static String TwoPass_Args()
        {
            // -------------------------
            //  2-Pass / Auto Quality
            // -------------------------
            // Enabled 
            //
            if (VM.VideoView.Video_Pass_SelectedItem == "2 Pass" &&
                VM.FormatView.Format_MediaType_SelectedItem == "Video" && // video only
                VM.VideoView.Video_Codec_SelectedItem != "Copy" &&  // exclude copy
                VM.FormatView.Format_Container_SelectedItem != "ogv" // exclude ogv (special rule)
                )
            {
                // --------------------------------------------------
                // Pass 1
                // --------------------------------------------------
                // -------------------------
                // HW Accel Decode
                // -------------------------
                List<string> hwAccelDecodeList_Pass1 = new List<string>()
                {
                    "\r\n\r\n" +
                    Video.HWAccelerationDecode(VM.FormatView.Format_MediaType_SelectedItem,
                                               VM.VideoView.Video_Codec_SelectedItem,
                                               VM.VideoView.Video_HWAccel_Decode_SelectedItem
                                               ),
                };
                // -------------------------
                // Input
                // -------------------------
                List<string> inputList_Pass1 = new List<string>()
                {
                    "\r\n\r\n" +
                    "-i "+ "\"" +
                    MainWindow.InputPath("pass 1") + "\"",
                    //Path_ShellFormatter(MainWindow.InputPath("pass 1"))
                };

                // -------------------------
                // HW Accel Transcode
                // -------------------------
                List<string> hwAccelTranscodeList_Pass1 = new List<string>()
                {
                    "\r\n\r\n" +
                    Video.HWAccelerationTranscode(VM.FormatView.Format_MediaType_SelectedItem,
                                                  VM.VideoView.Video_Codec_SelectedItem,
                                                  VM.VideoView.Video_HWAccel_Transcode_SelectedItem
                                                  ),
                };

                // -------------------------
                // Format
                // -------------------------
                List<string> formatList_Pass1 = new List<string>()
                {
                    "\r\n\r\n" +
                    Format.CutStart(VM.MainView.Input_Text,
                                    VM.MainView.Batch_IsChecked,
                                    VM.FormatView.Format_Cut_SelectedItem,
                                    VM.FormatView.Format_CutStart_Hours_Text,
                                    VM.FormatView.Format_CutStart_Minutes_Text,
                                    VM.FormatView.Format_CutStart_Seconds_Text,
                                    VM.FormatView.Format_CutStart_Milliseconds_Text,
                                    VM.FormatView.Format_FrameStart_Text
                                    ),
                    Format.CutEnd(VM.MainView.Input_Text,
                                  VM.MainView.Batch_IsChecked,
                                  VM.FormatView.Format_MediaType_SelectedItem,
                                  VM.FormatView.Format_Cut_SelectedItem,
                                  VM.FormatView.Format_CutEnd_Hours_Text,
                                  VM.FormatView.Format_CutEnd_Minutes_Text,
                                  VM.FormatView.Format_CutEnd_Seconds_Text,
                                  VM.FormatView.Format_CutEnd_Milliseconds_Text,
                                  VM.FormatView.Format_FrameEnd_Text
                                  ),
                };

                // -------------------------
                // Video
                // -------------------------
                List<string> videoList_Pass1 = new List<string>();

                if (VM.FormatView.Format_MediaType_SelectedItem != "Audio" &&
                    VM.VideoView.Video_Codec_SelectedItem != "None" &&
                    VM.VideoView.Video_Quality_SelectedItem != "None"
                    )
                {
                    videoList_Pass1 = new List<string>()
                    {
                        "\r\n\r\n" +
                        Video.VideoCodec(VM.VideoView.Video_HWAccel_Transcode_SelectedItem,
                                         VM.VideoView.Video_Codec_SelectedItem,
                                         VM.VideoView.Video_Codec
                                         ),

                        Video.PassParams(VM.VideoView.Video_Codec_SelectedItem, //-x265-params pass=1
                                         VM.VideoView.Video_Pass_SelectedItem,
                                         "1"
                                         ),
                        "\r\n" +
                        VideoParams.Video_Params(VM.VideoView.Video_Quality_SelectedItem,
                                                 VM.VideoView.Video_Codec_SelectedItem,
                                                 VM.FormatView.Format_MediaType_SelectedItem
                                                 ),

                        "\r\n" +
                        Video.VideoEncodeSpeed(VM.VideoView.Video_EncodeSpeed_Items,
                                               VM.VideoView.Video_EncodeSpeed_SelectedItem,
                                               VM.VideoView.Video_Codec_SelectedItem,
                                               VM.VideoView.Video_Pass_SelectedItem
                                               ),

                        Video.VideoQuality(VM.MainView.Batch_IsChecked,
                                           VM.VideoView.Video_VBR_IsChecked,
                                           VM.FormatView.Format_Container_SelectedItem,
                                           VM.FormatView.Format_MediaType_SelectedItem,
                                           VM.VideoView.Video_Codec_SelectedItem,
                                           VM.VideoView.Video_Quality_Items,
                                           VM.VideoView.Video_Quality_SelectedItem,
                                           VM.VideoView.Video_Pass_SelectedItem,
                                           VM.VideoView.Video_CRF_Text,
                                           VM.VideoView.Video_BitRate_Text,
                                           VM.VideoView.Video_MinRate_Text,
                                           VM.VideoView.Video_MaxRate_Text,
                                           VM.VideoView.Video_BufSize_Text,
                                           VM.MainView.Input_Text
                                           ),

                        "\r\n" +
                        Video.PixFmt(VM.VideoView.Video_Codec_SelectedItem,
                                     VM.VideoView.Video_PixelFormat_SelectedItem
                                     ),

                        "\r\n" +
                        Video.Color_Primaries(VM.VideoView.Video_Color_Primaries_SelectedItem),
                        "\r\n" +
                        Video.Color_TransferCharacteristics(VM.VideoView.Video_Color_TransferCharacteristics_SelectedItem),
                        "\r\n" +
                        Video.Color_Space(VM.VideoView.Video_Color_Space_SelectedItem),
                        "\r\n" +
                        Video.Color_Range(VM.VideoView.Video_Color_Range_SelectedItem),

                        "\r\n" +
                        Video.FPS(VM.VideoView.Video_Codec_SelectedItem,
                                  VM.VideoView.Video_FPS_SelectedItem,
                                  VM.VideoView.Video_FPS_Text
                                  ),
                        "\r\n" +
                        VideoFilters.VideoFilter(),
                        "\r\n" +
                        Video.AspectRatio(VM.VideoView.Video_AspectRatio_SelectedItem),
                        "\r\n" +
                        Video.Images(VM.FormatView.Format_MediaType_SelectedItem,
                                     VM.VideoView.Video_Codec_SelectedItem
                                     ),
                        "\r\n" +
                        Video.Optimize(VM.VideoView.Video_Codec_SelectedItem,
                                       VM.VideoView.Video_Optimize_Items,
                                       VM.VideoView.Video_Optimize_SelectedItem,
                                       VM.VideoView.Video_Video_Optimize_Tune_SelectedItem,
                                       VM.VideoView.Video_Video_Optimize_Profile_SelectedItem,
                                       VM.VideoView.Video_Optimize_Level_SelectedItem
                                       ),

                        // -pass 1, -x265-params pass=2
                        "\r\n" +
                        Video.Pass1Modifier(VM.VideoView.Video_Codec_SelectedItem,
                                            VM.VideoView.Video_Pass_SelectedItem
                                            ),
                    };
                }
                // Disable Video
                else
                {
                    videoList_Pass1 = new List<string>()
                    {
                        "\r\n\r\n" +
                        "-vn",
                    };
                }

                // -------------------------
                // Subtitle
                // -------------------------
                List<string> subtitleList_Pass1 = new List<string>();

                subtitleList_Pass1 = new List<string>()
                {
                    // Disable Subtitles for Pass 1 to speed up encoding
                    "\r\n" +
                    "-sn",
                };

                // -------------------------
                // Audio
                // -------------------------
                List<string> audioList_Pass1 = new List<string>();

                audioList_Pass1 = new List<string>()
                {
                    // Disable Audio for Pass 1 to speed up encoding
                    "\r\n" +
                    "-an",
                };

                // -------------------------
                // Output
                // -------------------------
                List<string> outputList_Pass1 = new List<string>()
                {
                    // Disable FormatMaps()

                    "\r\n\r\n" +
                    Format.ForceFormat(VM.FormatView.Format_Container_SelectedItem),

                    "\r\n\r\n" +
                    MainWindow.ThreadDetect(),

                    // Output Path Null
                    "\r\n\r\n" +
                    "NUL"
                };
                

                // Combine Lists
                List<string> FFmpegArgsPass1List = hwAccelDecodeList_Pass1
                                                   .Concat(inputList_Pass1)
                                                   .Concat(hwAccelTranscodeList_Pass1)
                                                   .Concat(formatList_Pass1)
                                                   .Concat(videoList_Pass1)
                                                   .Concat(subtitleList_Pass1)
                                                   .Concat(audioList_Pass1)
                                                   .Concat(outputList_Pass1)
                                                   .ToList();

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                Video.pass1Args = string.Join(" ", FFmpegArgsPass1List
                                                   .Where(s => !string.IsNullOrEmpty(s))
                                                   .Where(s => !string.IsNullOrWhiteSpace(s))
                                                   .Where(s => !s.Equals(Environment.NewLine))
                                                   .Where(s => !s.Equals("\r\n\r\n"))
                                                   .Where(s => !s.Equals("\r\n"))
                                             );


                // --------------------------------------------------
                // Pass 2
                // --------------------------------------------------
                // -------------------------
                // Pass 2 Initialize
                // -------------------------
                List<string> initializeList_Pass2 = new List<string>()
                {
                    "\r\n\r\n" +
                    //"&&",
                    LogicalOperator_And_ShellFormatter(),

                    "\r\n\r\n" +
                    Exe_InvokeOperator_PowerShell() +
                    MainWindow.FFmpegPath(),

                    "\r\n\r\n" +
                    "-y",
                };

                // -------------------------
                // HW Accel Decode
                // -------------------------
                List<string> hwAccelDecodeList_Pass2 = new List<string>()
                {
                    "\r\n\r\n" +
                    Video.HWAccelerationDecode(VM.FormatView.Format_MediaType_SelectedItem,
                                               VM.VideoView.Video_Codec_SelectedItem,
                                               VM.VideoView.Video_HWAccel_Decode_SelectedItem
                                               ),
                };

                // -------------------------
                // Input
                // -------------------------
                List<string> inputList_Pass2 = new List<string>()
                {
                    "\r\n\r\n" +
                    "-i " +
                    "\"" + MainWindow.InputPath("pass 2") + "\"",
                    //Path_ShellFormatter(MainWindow.InputPath("pass 2")),

                    "\r\n\r\n" +
                    Subtitle.SubtitlesExternal(VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                               VM.SubtitleView.Subtitle_Stream_SelectedItem
                                               ),
                };

                // -------------------------
                // HW Accel Transcode
                // -------------------------
                List<string> hwAccelTranscodeList_Pass2 = new List<string>()
                {
                    "\r\n\r\n" +
                    Video.HWAccelerationTranscode(VM.FormatView.Format_MediaType_SelectedItem,
                                                  VM.VideoView.Video_Codec_SelectedItem,
                                                  VM.VideoView.Video_HWAccel_Transcode_SelectedItem
                                                  ),
                };

                // -------------------------
                // Format
                // -------------------------
                List<string> formatList_Pass2 = new List<string>()
                {
                    "\r\n\r\n" +
                    Format.trimStart,
                    Format.trimEnd,
                };

                // -------------------------
                // Video
                // -------------------------
                List<string> videoList_Pass2 = new List<string>();

                if (VM.FormatView.Format_MediaType_SelectedItem != "Audio" &&
                    VM.VideoView.Video_Codec_SelectedItem != "None" &&
                    VM.VideoView.Video_Quality_SelectedItem != "None"
                    )
                {
                    videoList_Pass2 = new List<string>()
                    {
                        "\r\n\r\n" +
                        Video.vCodec,
                        Video.PassParams(VM.VideoView.Video_Codec_SelectedItem, //-x265-params pass=2
                                         VM.VideoView.Video_Pass_SelectedItem,
                                         "2"
                                         ),
                        "\r\n" +
                        VideoParams.Video_Params(VM.VideoView.Video_Quality_SelectedItem,    // Note: Use Method, not String, to re-generate all Pass 2 Params
                                                 VM.VideoView.Video_Codec_SelectedItem,      //       for 2 Pass -x265-params pass=2
                                                 VM.FormatView.Format_MediaType_SelectedItem
                                                 ),
                        "\r\n" +
                        Video.vEncodeSpeed,
                        Video.vQuality,
                        "\r\n" +
                        Video.pix_fmt,
                        "\r\n" +
                        Video.colorPrimaries,
                        "\r\n" +
                        Video.colorTransferCharacteristics,
                        "\r\n" +
                        Video.colorSpace,
                        "\r\n" +
                        Video.colorRange,
                        "\r\n" +
                        Video.colorPrimaries,
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
                        Streams.VideoStreamMaps(),
                        "\r\n" +
                        Video.Pass2Modifier(VM.VideoView.Video_Codec_SelectedItem, // -pass 2, -x265-params pass=2
                                            VM.VideoView.Video_Pass_SelectedItem
                                            ),
                    };
                }
                // Disable Video
                else
                {
                    videoList_Pass1 = new List<string>()
                    {
                        "\r\n\r\n" +
                        "-vn",
                    };
                }

                // -------------------------
                // Subtitle
                // -------------------------
                List<string> subtitleList_Pass2 = new List<string>();

                if (VM.FormatView.Format_MediaType_SelectedItem != "Audio" &&
                    VM.VideoView.Video_Codec_SelectedItem != "None" &&
                    VM.VideoView.Video_Quality_SelectedItem != "None"
                    )
                {
                    subtitleList_Pass2 = new List<string>()
                    {
                        "\r\n\r\n" +
                        Subtitle.SubtitleCodec(VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                               VM.SubtitleView.Subtitle_Codec
                                               ),
                        "\r\n" +
                        Streams.SubtitleMaps(),
                    };
                }
                // Disable Subtitle
                else
                {
                    subtitleList_Pass1 = new List<string>()
                    {
                        "\r\n\r\n" +
                        "-sn",
                    };
                }

                // -------------------------
                // Audio
                // -------------------------
                List<string> audioList_Pass2 = new List<string>();

                if (VM.FormatView.Format_MediaType_SelectedItem != "Image" &&
                    VM.FormatView.Format_MediaType_SelectedItem != "Sequence" &&
                    VM.AudioView.Audio_Codec_SelectedItem != "None" &&
                    VM.AudioView.Audio_Stream_SelectedItem != "none" &&
                    VM.AudioView.Audio_Quality_SelectedItem != "None" &&
                    VM.AudioView.Audio_Quality_SelectedItem != "Mute"
                    )
                {
                    audioList_Pass2 = new List<string>()
                    {
                        "\r\n\r\n" +
                        Audio.AudioCodec(VM.AudioView.Audio_Codec_SelectedItem,
                                         VM.AudioView.Audio_Codec
                                         ),
                        "\r\n" +
                        Audio.AudioQuality(VM.MainView.Input_Text,
                                           VM.MainView.Batch_IsChecked,
                                           VM.FormatView.Format_MediaType_SelectedItem,
                                           VM.AudioView.Audio_Stream_SelectedItem,
                                           VM.AudioView.Audio_Codec_SelectedItem,
                                           VM.AudioView.Audio_Quality_Items,
                                           VM.AudioView.Audio_Quality_SelectedItem,
                                           VM.AudioView.Audio_BitRate_Text,
                                           VM.AudioView.Audio_VBR_IsChecked
                                           ),
                        Audio.CompressionLevel(VM.AudioView.Audio_Codec_SelectedItem,
                                               VM.AudioView.Audio_CompressionLevel_SelectedItem
                                               ),
                        Audio.SampleRate(VM.AudioView.Audio_Codec_SelectedItem,
                                         VM.AudioView.Audio_SampleRate_Items,
                                         VM.AudioView.Audio_SampleRate_SelectedItem
                                         ),
                        Audio.BitDepth(VM.AudioView.Audio_Codec_SelectedItem,
                                       VM.AudioView.Audio_BitDepth_Items,
                                       VM.AudioView.Audio_BitDepth_SelectedItem
                                       ),
                        Audio.Channel(VM.AudioView.Audio_Codec_SelectedItem,
                                      VM.AudioView.Audio_Channel_SelectedItem
                                      ),
                        "\r\n" +
                        AudioFilters.AudioFilter(),
                        "\r\n" +
                        Streams.AudioStreamMaps(),
                    };
                }
                // Disable Audio
                else
                {
                    audioList_Pass1 = new List<string>()
                    {
                        "\r\n\r\n" +
                        "-an",
                    };
                }

                // -------------------------
                // Output
                // -------------------------
                List<string> outputList_Pass2 = new List<string>()
                {
                    "\r\n\r\n" +
                    Streams.FormatMaps(),

                    "\r\n\r\n" +
                    Format.ForceFormat(VM.FormatView.Format_Container_SelectedItem),

                    "\r\n\r\n" +
                    Configure.threads,

                    "\r\n\r\n" +
                    "\"" + MainWindow.OutputPath() + "\""
                    //Path_ShellFormatter(MainWindow.OutputPath())
                };


                // Combine Lists
                List<string> FFmpegArgsPass2List = initializeList_Pass2
                                                   .Concat(hwAccelDecodeList_Pass2)
                                                   .Concat(inputList_Pass2)
                                                   .Concat(hwAccelTranscodeList_Pass2)
                                                   .Concat(formatList_Pass2)
                                                   .Concat(videoList_Pass2)
                                                   .Concat(subtitleList_Pass2)
                                                   .Concat(audioList_Pass2)
                                                   .Concat(outputList_Pass2)
                                                   .ToList();


                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                Video.pass2Args = string.Join(" ", FFmpegArgsPass2List
                                                   .Where(s => !string.IsNullOrEmpty(s))
                                                   .Where(s => !string.IsNullOrWhiteSpace(s))
                                                   .Where(s => !s.Equals(Environment.NewLine))
                                                   .Where(s => !s.Equals("\r\n\r\n"))
                                                   .Where(s => !s.Equals("\r\n"))
                                             );


                // Combine Pass 1 & Pass 2 Args
                Video.v2PassArgs = Video.pass1Args + " " + Video.pass2Args;
            }


            // Return Value
            return Video.v2PassArgs;
        }


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------


        /// <summary>
        /// FFmpeg Single File - Generate Args
        /// </summary>
        public static String FFmpegSingleGenerateArgs()
        {
            if (VM.MainView.Batch_IsChecked == false)
            {
                // Make Arugments List
                List<string> FFmpegArgsList = new List<string>()
                {
                    Exe_InvokeOperator_PowerShell() + 
                    MainWindow.FFmpegPath(),

                    "\r\n\r\n" +
                    "-y",

                    OnePass_CRF_Args(), // disabled if 2-Pass
                    TwoPass_Args() // disabled if 1-Pass/CRF
                };

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                ffmpegArgsSort = string.Join(" ", FFmpegArgsList
                                                  .Where(s => !string.IsNullOrEmpty(s))
                                                  .Where(s => !string.IsNullOrWhiteSpace(s))
                                                  .Where(s => !s.Equals(Environment.NewLine))
                                                  .Where(s => !s.Equals("\r\n\r\n"))
                                                  .Where(s => !s.Equals("\r\n"))
                                            );

                // Inline 
                ffmpegArgs = MainWindow.RemoveLineBreaks(ffmpegArgsSort);
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
        /// FFmpeg Batch - Generate Args
        /// </summary>
        public static void FFmpegBatchGenerateArgs()
        {
            if (VM.MainView.Batch_IsChecked == true)
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Batch: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(VM.MainView.Batch_IsChecked)) { Foreground = Log.ConsoleDefault });
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


                List<string> ffmpegBatchArgsList = new List<string>();

                // -------------------------
                // CMD
                // -------------------------
                if (VM.ConfigureView.Shell_SelectedItem == "CMD")
                {
                    // -------------------------
                    // Batch Arguments Full
                    // -------------------------
                    ffmpegBatchArgsList = new List<string>()
                    {
                        "cd /d",
                        "\"" + MainWindow.BatchInputDirectory() + "\"",
                        //MainWindow.ShellStringFormatter(MainWindow.BatchInputDirectory()),

                        "\r\n\r\n" + "&& for %f in",
                        "(*" + MainWindow.inputExt + ")",
                        "do (echo)",

                        // Video
                        "\r\n\r\n" +
                        Video.BatchVideoQualityAuto(VM.MainView.Batch_IsChecked,
                                                    VM.VideoView.Video_Codec_SelectedItem,
                                                    VM.VideoView.Video_Quality_SelectedItem
                                                    ),

                        // Audio
                        "\r\n\r\n" +
                        Audio.BatchAudioQualityAuto(VM.MainView.Batch_IsChecked,
                                                    VM.AudioView.Audio_Codec_SelectedItem,
                                                    VM.AudioView.Audio_Quality_SelectedItem
                                                    ),
                        "\r\n\r\n" +
                        Audio.BatchAudioBitRateLimiter(VM.AudioView.Audio_Codec_SelectedItem,
                                                       VM.AudioView.Audio_Quality_SelectedItem
                                                       ),

                        "\r\n\r\n" + 
                        "&&",
                        "\r\n\r\n" + 
                        Exe_InvokeOperator_PowerShell() + 
                        MainWindow.FFmpegPath(),

                        "\r\n\r\n" +
                        "-y",
                   
                        // %~f added in InputPath()

                        OnePass_CRF_Args(), // disabled if 2-Pass       
                        TwoPass_Args() // disabled if 1-Pass/CRF
                    };
                }

                // -------------------------
                // PowerShell
                // -------------------------
                if (VM.ConfigureView.Shell_SelectedItem == "PowerShell")
                {
                    // Video Auto Quality Detect Bitrate
                    string vBitRateBatch = string.Empty;
                    if (VM.VideoView.Video_Quality_SelectedItem == "Auto")
                    {
                        vBitRateBatch = "\r\n" + "$vBitrate = " + Exe_InvokeOperator_PowerShell() + FFprobe.ffprobe + " -v error -select_streams v:0 -show_entries " + FFprobe.vEntryTypeBatch + " -of default=noprint_wrappers=1:nokey=1 \"$fullName\"" + ";";
                    }

                    // Audio Auto Quality Detect Bitrate
                    string aBitRateBatch = string.Empty;
                    string aBitRateBatch_NullCheck = string.Empty;
                    string aBitRateBatch_Limited = string.Empty;
                    if (VM.AudioView.Audio_Quality_SelectedItem == "Auto")
                    {
                        aBitRateBatch = "\r\n" + "$aBitrate = " + Exe_InvokeOperator_PowerShell() + FFprobe.ffprobe + " -v error -select_streams a:0 -show_entries " + FFprobe.aEntryType + " -of default=noprint_wrappers=1:nokey=1 \"$fullName\"" + ";";

                        // Bitrate Null Check
                        aBitRateBatch_NullCheck = "if (!$aBitrate) { $aBitrate = 0};";

                        // Bitrate Limiter
                        switch (VM.AudioView.Audio_Codec_SelectedItem)
                        {
                            // Vorbis
                            case "Vorbis":
                                aBitRateBatch_Limited = "if ($aBitrate -gt 500000) { $aBitrate = 500000 };";
                                break;

                            // Opus
                            case "Opus":
                                aBitRateBatch_Limited = "if ($aBitrate -gt 510000) { $aBitrate = 510000 };";
                                break;

                            // AAC
                            case "AAC":
                                aBitRateBatch_Limited = "if ($aBitrate -gt 400000) { $aBitrate = 400000 };"; 
                                break;

                            // AC3
                            case "AC3":
                                aBitRateBatch_Limited = "if ($aBitrate -gt 640000) { $aBitrate = 640000 };"; 
                                break;

                            // DTS
                            case "DTS":
                                aBitRateBatch_Limited = "if ($aBitrate -gt 1509075) { $aBitrate = 1509075 };";
                                break;

                            // MP2
                            case "MP2":
                                aBitRateBatch_Limited = "if ($aBitrate -gt 384000) { $aBitrate = 384000 };";
                                break;

                            // LAME
                            case "LAME":
                                aBitRateBatch_Limited = "if ($aBitrate -gt 320000) { $aBitrate = 320000 };";
                                break;

                            //// FLAC
                            // Do not use, empty is FFmpeg default
                            //case "FLAC":
                            //    aBitRateBatch_Limited = "if ($aBitrate -gt 1411000) { $aBitrate = 1411000 };";
                            //    break;

                            //// PCM
                            // Do not use, empty is FFmpeg default
                            //case "PCM":
                            //    aBitRateBatch_Limited = "if ($aBitrate -gt 1536000) { $aBitrate = 1536000 };";
                            //    break;
                        }
                    }

                    // -------------------------
                    // Batch Arguments Full
                    // -------------------------
                    ffmpegBatchArgsList = new List<string>()
                    {
                        "$files = Get-ChildItem " + "\"" + MainWindow.BatchInputDirectory().TrimEnd('\\') + "\"" + " -Filter *" + MainWindow.inputExt, // trim the dir's end backslash
                        "\r\n\r\n" + ";",

                        "\r\n\r\n" + "foreach ($f in $files) {" +

                        "\r\n\r\n" + "$fullName = $f.FullName" + ";", // capture full path to variable
                        "\r\n" + "$name = $f.Name" + ";", // capture name to variable
                        vBitRateBatch, // capture video bitrate to variable
                        aBitRateBatch, // capture audio bitrate to variable
                        aBitRateBatch_NullCheck, // check if bitrate is null, change to 0
                        aBitRateBatch_Limited, // limit audio bitrate

                        "\r\n\r\n" + 
                        Exe_InvokeOperator_PowerShell() + 
                        MainWindow.FFmpegPath(),
                        "\r\n\r\n" +
                        "-y",
                   
                        // $name added in InputPath()

                        OnePass_CRF_Args(), // disabled if 2-Pass       
                        TwoPass_Args(), // disabled if 1-Pass/CRF

                        "\r\n\r\n" + "}"
                    };
                }

                

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                ffmpegArgsSort = string.Join(" ", ffmpegBatchArgsList
                                                  .Where(s => !string.IsNullOrEmpty(s))
                                                  .Where(s => !string.IsNullOrWhiteSpace(s))
                                                  .Where(s => !s.Equals(Environment.NewLine))
                                                  .Where(s => !s.Equals("\r\n\r\n"))
                                                  .Where(s => !s.Equals("\r\n"))
                                        );

                // Inline 
                ffmpegArgs = MainWindow.RemoveLineBreaks(ffmpegArgsSort);
            }
        }


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------


        /// <summary>
        /// YouTube Download - Generate Args
        /// </summary>
        public static void YouTubeDownloadGenerateArgs()
        {
            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("YouTube Download: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(Convert.ToString(VM.MainView.Batch_IsChecked)) { Foreground = Log.ConsoleDefault });
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
            // Generate the youtube-dl Output Path and break it into sections for args below
            // -------------------------
            string outputPath = MainWindow.OutputPath();
            string outputDir = Path.GetDirectoryName(VM.MainView.Output_Text).TrimEnd('\\') + @"\"; // eg. C:\Output\Path\
            string outputFileName = Path.GetFileNameWithoutExtension(outputPath);
            string output = Path.Combine(outputDir, outputFileName + MainWindow.outputExt);

            // -------------------------
            // Generate Download Script
            // -------------------------
            List<string> youtubedlArgs = new List<string>();

            // -------------------------
            // CMD
            // -------------------------
            if (VM.ConfigureView.Shell_SelectedItem == "CMD")
            {
                // -------------------------
                // YouTube Download Arguments Full
                // -------------------------
                // youtube-dl Args List
                youtubedlArgs = new List<string>()
                {
                    "cd /d",
                    "\"" + outputDir + "\"",

                    "\r\n\r\n" + "&&",

                    "\r\n\r\n" + "for /f \"delims=\" %f in ('",

                    // Get Title
                    "\r\n\r\n" + "@" + "\"" + MainWindow.youtubedl + "\"",
                    "\r\n"  + "--get-filename -o \"%(title)s\" " + "\"" + MainWindow.YouTubeDownloadURL(VM.MainView.Input_Text) + "\"",
                    "\r\n" + "')",

                    // Download Video
                    "\r\n\r\n" + "do (",
                    "\r\n\r\n" + "@" + "\"" + MainWindow.youtubedl + "\"",

                    "\r\n\r\n" + "-f " + MainWindow.YouTubeDownloadQuality(VM.MainView.Input_Text,
                                                                           VM.FormatView.Format_YouTube_SelectedItem,
                                                                           VM.FormatView.Format_YouTube_Quality_SelectedItem
                                                                           ),
                    "\r\n\r\n" + "\"" + VM.MainView.Input_Text + "\"",
                    "\r\n" + "-o " + "\"" + outputDir + outputFileName + "." + MainWindow.YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
                                                                                                                VM.VideoView.Video_Codec_SelectedItem,
                                                                                                                VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                                                                                VM.AudioView.Audio_Codec_SelectedItem
                                                                                                                ) + "\"",

                    // FFmpeg Location
                    "\r\n\r\n" + 
                    MainWindow.YouTubeDL_FFmpegPath(),

                    // Merge Output Format
                    "\r\n\r\n" + "--merge-output-format " + MainWindow.YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
                                                                                             VM.VideoView.Video_Codec_SelectedItem,
                                                                                             VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                                                             VM.AudioView.Audio_Codec_SelectedItem
                                                                                             )
                };
            }

            // -------------------------
            // PowerShell
            // -------------------------
            else if (VM.ConfigureView.Shell_SelectedItem == "PowerShell")
            {
                // Format youtube-dl Path
                string youtubedl_formatted = string.Empty;
                // Check if youtube-dl is not default, if so it is a user-defined path, wrap in quotes
                // If default, it will not be wrapped in quotes
                if (MainWindow.youtubedl != "youtube-dl")
                {
                    youtubedl_formatted = MainWindow.WrapQuotes(MainWindow.youtubedl);
                }
                else
                {
                    youtubedl_formatted = MainWindow.youtubedl;
                }

                // youtube-dl Args List
                youtubedlArgs = new List<string>()
                {
                    // Get Title
                    "$name =" + " & " + youtubedl_formatted + " " + "--get-filename -o \"%(title)s\" " + "\"" + MainWindow.YouTubeDownloadURL(VM.MainView.Input_Text) + "\"",

                    "\r\n\r\n" + ";",

                    // Download Video
                    "\r\n\r\n" + "& " + youtubedl_formatted,

                    "\r\n\r\n" + "-f " + MainWindow.YouTubeDownloadQuality(VM.MainView.Input_Text,
                                                                            VM.FormatView.Format_YouTube_SelectedItem,
                                                                            VM.FormatView.Format_YouTube_Quality_SelectedItem
                                                                            ),
                    "\r\n\r\n" + "\"" + VM.MainView.Input_Text + "\"",
                    "\r\n" +"-o " + "\"" + outputDir + outputFileName + "." + MainWindow.YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
                                                                                                               VM.VideoView.Video_Codec_SelectedItem,
                                                                                                               VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                                                                               VM.AudioView.Audio_Codec_SelectedItem
                                                                                                               ) + "\"",

                    // FFmpeg Location
                    "\r\n\r\n" +
                    MainWindow.YouTubeDL_FFmpegPath(),

                    // Merge Output Format
                    "\r\n\r\n" + "--merge-output-format " + MainWindow.YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
                                                                                             VM.VideoView.Video_Codec_SelectedItem,
                                                                                             VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                                                             VM.AudioView.Audio_Codec_SelectedItem
                                                                                             )
                };
            }


            // FFmpeg Args
            //
            List<string> ffmpegArgsList = new List<string>()
            {
                //"\r\n\r\n" + "&&",
                "\r\n\r\n" + 
                LogicalOperator_And_ShellFormatter(),
                "\r\n\r\n" + 
                Exe_InvokeOperator_PowerShell() + 
                MainWindow.FFmpegPath(),

                "\r\n\r\n" +
                "-y",

                OnePass_CRF_Args(), //disabled if 2-Pass       
                TwoPass_Args(), //disabled if 1-Pass/CRF

                //"\r\n\r\n" + "&&",
                "\r\n\r\n" + LogicalOperator_And_ShellFormatter(),

                // Delete Downloaded File
                "\r\n\r\n" + "del " + "\"" + MainWindow.downloadDir + "%f" + "." + MainWindow.YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
                                                                                                                    VM.VideoView.Video_Codec_SelectedItem,
                                                                                                                    VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                                                                                    VM.AudioView.Audio_Codec_SelectedItem
                                                                                                                    ) + "\"",
            };


            // -------------------------
            // Download-Only
            // -------------------------
            if (MainWindow.IsWebDownloadOnly(VM.VideoView.Video_Codec_SelectedItem,
                                             VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                             VM.AudioView.Audio_Codec_SelectedItem) == true
                                             )
            {
                // Add "do" Closing Tag
                // CMD
                if (VM.ConfigureView.Shell_SelectedItem == "CMD")
                {
                    youtubedlArgs.Add("\r\n)");
                }

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                ffmpegArgsSort = string.Join(" ", youtubedlArgs
                                                 .Where(s => !string.IsNullOrEmpty(s))
                                                 .Where(s => !string.IsNullOrWhiteSpace(s))
                                                 .Where(s => !s.Equals(Environment.NewLine))
                                                 .Where(s => !s.Equals("\r\n\r\n"))
                                                 .Where(s => !s.Equals("\r\n"))
                                    );

                // Inline 
                ffmpegArgs = MainWindow.RemoveLineBreaks(ffmpegArgsSort);
            }

            // -------------------------
            // Download & Convert
            // -------------------------
            else
            {
                // Join YouTube Args & FFmpeg Args
                youtubedlArgs.AddRange(ffmpegArgsList);
                // Add "do" Closing Tag
                youtubedlArgs.Add("\r\n)");

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                ffmpegArgsSort = string.Join(" ", youtubedlArgs
                                                  .Where(s => !string.IsNullOrEmpty(s))
                                                  .Where(s => !string.IsNullOrWhiteSpace(s))
                                                  .Where(s => !s.Equals(Environment.NewLine))
                                                  .Where(s => !s.Equals("\r\n\r\n"))
                                                  .Where(s => !s.Equals("\r\n"))
                                 );

                // Inline 
                ffmpegArgs = MainWindow.RemoveLineBreaks(ffmpegArgsSort);
            }
        }


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Arguments Shell Formatter
        /// </summary>
        //public static String ArgsShellFormatter(string args)
        //{
        //    // Format FFmpeg Arguments for PowerShell
        //    if (VM.ConfigureView.Shell_SelectedItem == "PowerShell")
        //    {
        //        return args//.Replace("\\", "\\\\") // Format Backslashes \ → \\
        //                   //.Replace("\"", "\\\"") // Format Quotes " → \"
        //                   //.Replace("&&", ";") // Format && Logical Operator && → ;
        //                   //.Replace("&", ";") // Format & Logical Operator & → ;
        //                   .Replace("cd /d", "cd"); // Format & Logical Operator & → ;
        //    }

        //    // Default CMD
        //    return args;
        //}

        /// <summary>
        /// FFmpeg Generate Script
        /// </summary>
        public static void FFmpegGenerateScript()
        {
            // -------------------------
            // Write FFmpeg Args
            // -------------------------
            // Sorted (Default)
            // Inline (User Selected)
            VM.MainView.ScriptView_Text = ffmpegArgs;
        }


        /// <summary>
        /// FFmpeg Start
        /// </summary>
        public static void FFmpegStart(string args)
        {
            // -------------------------
            // Start FFmpeg Process
            // -------------------------
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // -------------------------
                // CMD
                // -------------------------
                case "CMD":
                    System.Diagnostics.Process.Start("cmd.exe",
                                                    KeepWindow() +
                                                    "cd " + MainWindow.WrapQuotes(MainWindow.outputDir) +
                                                    " & " +
                                                    args
                                                    );
                    break;

                // -------------------------
                // PowerShell
                // -------------------------
                case "PowerShell":
                    System.Diagnostics.Process.Start("powershell.exe",
                                                    KeepWindow() +
                                                    "-command \"Set-Location " + MainWindow.WrapQuotes(MainWindow.outputDir).Replace("\\", "\\\\") // Format Backslashes for PowerShell \ → \\
                                                                                                                            .Replace("\"", "\\\"") + // Format Quotes " → \"
                                                    "; " +
                                                    args.Replace("\"", "\\\"") // Format Quotes " → \"
                                                    );
                    break;
            }
        }


        /// <summary>
        /// FFmpeg Convert
        /// </summary>
        public static void FFmpegConvert()
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
            // FFmpegArgs → ScriptView
            FFmpegGenerateScript();

            // -------------------------
            // Start FFmpeg
            // -------------------------
            // ScriptView → CMD/PowerShell
            FFmpegStart(MainWindow.ReplaceLineBreaksWithSpaces(
                            VM.MainView.ScriptView_Text)
                        );
        }


    }
}
