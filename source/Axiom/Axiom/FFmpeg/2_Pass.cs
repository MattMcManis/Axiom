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

 * Arguments
---------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axiom
{
    public class _2_Pass
    {
        /// <summary>
        /// 2 Pass Arguments
        /// </summary>      
        public static String Arguments()
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
                    FFmpeg.LogicalOperator_And_ShellFormatter(),

                    "\r\n\r\n" +
                    FFmpeg.Exe_InvokeOperator_PowerShell() +
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
    }

}
