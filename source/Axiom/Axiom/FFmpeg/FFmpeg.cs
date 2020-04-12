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
        // FFmepg / FFprobe
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
            string cmdWindow = string.Empty;

            // Keep
            if (VM.MainView.CMDWindowKeep_IsChecked == true)
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
        /// 1-Pass Arguments
        /// </summary>
        // 1-Pass, CRF, & Auto
        public static String OnePassArgs()
        {
            // -------------------------
            //  Single Pass
            // -------------------------
            if (VM.VideoView.Video_Pass_SelectedItem == "1 Pass" ||
                VM.VideoView.Video_Pass_SelectedItem == "CRF" ||
                VM.VideoView.Video_Pass_SelectedItem == "auto" ||
                VM.FormatView.Format_Container_SelectedItem == "ogv" //ogv (special rule)
                )
            {
                // -------------------------
                // Input
                // -------------------------
                List<string> inputList = new List<string>()
                {
                    "\r\n\r\n" +
                    "-i "+ "\"" + MainWindow.InputPath(/*main_vm,*/ "pass 1") + "\"",

                    "\r\n\r\n" +
                    Subtitle.SubtitlesExternal(VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                               VM.SubtitleView.Subtitle_Stream_SelectedItem
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
                        Video.VideoCodec(VM.VideoView.Video_HWAccel_SelectedItem,
                                         VM.VideoView.Video_Codec_SelectedItem,
                                         VM.VideoView.Video_Codec
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
                                         VM.AudioView.Audio_Codec//,
                                         //VM.AudioView.Audio_BitDepth_SelectedItem,
                                         //VM.MainView.Input_Text
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
                List<string> FFmpegArgsSinglePassList = inputList
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
        public static String TwoPassArgs()
        {
            // -------------------------
            //  2-Pass Auto Quality
            // -------------------------
            // Enabled 
            //
            if (VM.VideoView.Video_Pass_SelectedItem == "2 Pass" &&
                VM.FormatView.Format_MediaType_SelectedItem == "Video" &&  // video only
                VM.VideoView.Video_Codec_SelectedItem != "Copy" &&  // exclude copy
                VM.FormatView.Format_Container_SelectedItem != "ogv" // exclude ogv (special rule)
                )
            {
                // --------------------------------------------------
                // Pass 1
                // --------------------------------------------------
                // -------------------------
                // Input
                // -------------------------
                List<string> inputList_Pass1 = new List<string>()
                {
                    "\r\n\r\n" +
                    "-i "+ "\"" +
                    MainWindow.InputPath(/*main_vm,*/ "pass 1") + "\"",
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
                        Video.VideoCodec(VM.VideoView.Video_HWAccel_SelectedItem,
                                         VM.VideoView.Video_Codec_SelectedItem,
                                         VM.VideoView.Video_Codec
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
                List<string> FFmpegArgsPass1List = inputList_Pass1
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
                                                   .Where(s => !s.Equals(Environment.NewLine))
                                                   .Where(s => !s.Equals("\r\n\r\n"))
                                                   .Where(s => !s.Equals("\r\n"))
                                             );


                // --------------------------------------------------
                // Pass 2
                // --------------------------------------------------
                // -------------------------
                // Input
                // -------------------------
                List<string> inputList_Pass2 = new List<string>()
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
                    Video.HWAcceleration(VM.FormatView.Format_MediaType_SelectedItem,
                                         VM.VideoView.Video_Codec_SelectedItem,
                                         VM.VideoView.Video_HWAccel_SelectedItem
                                         ),

                    "\r\n\r\n" +
                    "-i " + "\"" + MainWindow.InputPath(/*main_vm,*/ "pass 2") + "\"",

                    "\r\n\r\n" +
                    Subtitle.SubtitlesExternal(VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                               VM.SubtitleView.Subtitle_Stream_SelectedItem
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
                };


                // Combine Lists
                List<string> FFmpegArgsPass2List = inputList_Pass2
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
                    MainWindow.FFmpegPath(),
                    "-y",

                    "\r\n\r\n" + Video.HWAcceleration(VM.FormatView.Format_MediaType_SelectedItem,
                                                      VM.VideoView.Video_Codec_SelectedItem,
                                                      VM.VideoView.Video_HWAccel_SelectedItem
                                                      ),
                    OnePassArgs(), // disabled if 2-Pass
                    TwoPassArgs() // disabled if 1-Pass
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
        /// FFmpeg Batch - Generate Args
        /// </summary>
        public static void FFmpegBatchGenerateArgs()
        {
            if (VM.MainView.Batch_IsChecked == true)
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


                // -------------------------
                // Batch Arguments Full
                // -------------------------
                // Make List
                //
                List<string> FFmpegBatchArgsList = new List<string>()
                {
                    "cd /d",
                    "\"" + MainWindow.BatchInputDirectory() + "\"",

                    "\r\n\r\n" + "&& for %f in",
                    "(*" + MainWindow.inputExt + ")",
                    "do (echo)",

                    // Video
                    "\r\n\r\n" + Video.BatchVideoQualityAuto(VM.MainView.Batch_IsChecked,
                                                             VM.VideoView.Video_Codec_SelectedItem,
                                                             VM.VideoView.Video_Quality_SelectedItem 
                                                             ),

                    // Audio
                    "\r\n\r\n" + Audio.BatchAudioQualityAuto(VM.MainView.Batch_IsChecked,
                                                             VM.AudioView.Audio_Codec_SelectedItem,
                                                             VM.AudioView.Audio_Quality_SelectedItem
                                                             ),
                    "\r\n\r\n" + Audio.BatchAudioBitRateLimiter(VM.AudioView.Audio_Codec_SelectedItem,
                                                                VM.AudioView.Audio_Quality_SelectedItem
                                                                ),

                    "\r\n\r\n" + "&&",
                    "\r\n\r\n" + MainWindow.FFmpegPath(),
                    "-y",

                    "\r\n\r\n" + Video.HWAcceleration(VM.FormatView.Format_MediaType_SelectedItem,
                                                      VM.VideoView.Video_Codec_SelectedItem,
                                                      VM.VideoView.Video_HWAccel_SelectedItem
                                                      ),
                   
                    // %~f added in InputPath()

                    OnePassArgs(), // disabled if 2-Pass       
                    TwoPassArgs() // disabled if 1-Pass
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
            // YouTube Download Arguments Full
            // -------------------------
            // Download
            //
            List<string> youtubedlArgs = new List<string>()
            {
                "cd /d",
                "\"" + /*MainWindow.downloadDir*/ outputDir + "\"",

                "\r\n\r\n" + "&&",

                "\r\n\r\n" + "for /f \"delims=\" %f in ('",

                // Get Title
                "\r\n\r\n" + "@" + "\"" + MainWindow.youtubedl + "\"",
                "\r\n"  + " --get-filename -o \"%(title)s\" " + "\"" + MainWindow.YouTubeDownloadURL(VM.MainView.Input_Text) + "\"",
                "\r\n" + "')",

                // Download Video
                "\r\n\r\n" + "do (",
                "\r\n\r\n" + "@" + "\"" + MainWindow.youtubedl + "\"",

                "\r\n\r\n" + " -f " + MainWindow.YouTubeDownloadQuality(VM.MainView.Input_Text, 
                                                                        VM.FormatView.Format_YouTube_SelectedItem, 
                                                                        VM.FormatView.Format_YouTube_Quality_SelectedItem
                                                                        ),
                "\r\n\r\n" + "\"" + VM.MainView.Input_Text + "\"",
                "\r\n" +" -o " + "\"" + /*MainWindow.downloadDir*/outputDir + /*"%f"*/outputFileName + "." + MainWindow.YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
                                                                                                               VM.VideoView.Video_Codec_SelectedItem,
                                                                                                               VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                                                                               VM.AudioView.Audio_Codec_SelectedItem
                                                                                                               ) + "\"",

                // FFmpeg Location
                "\r\n\r\n" + MainWindow.YouTubeDL_FFmpegPath(),

                // Merge Output Format
                "\r\n\r\n" + "--merge-output-format " + MainWindow.YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
                                                                                         VM.VideoView.Video_Codec_SelectedItem,
                                                                                         VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                                                         VM.AudioView.Audio_Codec_SelectedItem
                                                                                         )
            };

            // FFmpeg Args
            //
            List<string> FFmpegArgs = new List<string>()
            {
                "\r\n\r\n" + "&&",
                "\r\n\r\n" + MainWindow.FFmpegPath(),
                "-y",

                "\r\n\r\n" + Video.HWAcceleration(VM.FormatView.Format_MediaType_SelectedItem,
                                                  VM.VideoView.Video_Codec_SelectedItem,
                                                  VM.VideoView.Video_HWAccel_SelectedItem
                                                  ),
                
                OnePassArgs(), //disabled if 2-Pass       
                TwoPassArgs(), //disabled if 1-Pass

                "\r\n\r\n" + "&&",

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
        /// FFmpeg Generate Script
        /// </summary>
        public static void FFmpegScript()
        {
            // Write FFmpeg Args
            VM.MainView.ScriptView_Text = ffmpegArgs;
        }


        /// <summary>
        /// FFmpeg Start
        /// </summary>
        public static void FFmpegStart()
        {
            // Start FFmpeg Process
            System.Diagnostics.Process.Start("cmd.exe",
                                             KeepWindow()
                                             + " cd " + "\"" + MainWindow.outputDir + "\""
                                             + " & "
                                             + ffmpegArgs
                                             );
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
            FFmpegScript();

            // -------------------------
            // Start FFmpeg
            // -------------------------
            FFmpegStart();
        }


    }
}
