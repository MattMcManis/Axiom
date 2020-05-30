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
using System.Windows.Documents;
using ViewModel;
using Axiom;

namespace Generate
{
    public partial class FFmpeg
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
                    // FFmpeg Initialize
                    // -------------------------
                    List<string> initializeList_Pass1 = new List<string>()
                    {
                        ProcessPriority() +
                        //PowerShell_CallOperator_FFmpeg() +
                        MainWindow.FFmpegPath() +
                        ProcessPriority_PowerShell_Flags(),

                        "\r\n\r\n" +
                        ProcessPriority_PowerShell_Args_Start(),

                        "\r\n\r\n" +
                        //"-y"
                        OutputOverwrite(),
                    };

                    // -------------------------
                    // HW Accel Decode
                    // -------------------------
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("HW Accel")) { Foreground = Log.ConsoleAction });
                    };
                    Log.LogActions.Add(Log.WriteAction);

                    List<string> hwAccelDecodeList_Pass1 = new List<string>()
                    {
                        "\r\n\r\n" +
                        Video.Encoding.HWAccelerationDecode(VM.FormatView.Format_MediaType_SelectedItem,
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
                        //"-i " + "\"" + MainWindow.InputPath("pass 1") + "\"",
                        "-i " + MainWindow.WrapWithQuotes(MainWindow.InputPath("pass 1"))
                    };

                    // -------------------------
                    // HW Accel Transcode
                    // -------------------------
                    List<string> hwAccelTranscodeList_Pass1 = new List<string>()
                    {
                        "\r\n\r\n" +
                        Video.Encoding.HWAccelerationTranscode(VM.FormatView.Format_MediaType_SelectedItem,
                                                               VM.VideoView.Video_Codec_SelectedItem,
                                                               VM.VideoView.Video_HWAccel_Transcode_SelectedItem
                                                              ),
                    };

                    // -------------------------
                    // Format
                    // -------------------------
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Format")) { Foreground = Log.ConsoleAction });
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Run("Extension: " + MainWindow.outputExt) { Foreground = Log.ConsoleDefault }); // output Extension
                    };
                    Log.LogActions.Add(Log.WriteAction);

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
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Video")) { Foreground = Log.ConsoleAction });
                    };
                    Log.LogActions.Add(Log.WriteAction);

                    List<string> videoList_Pass1 = new List<string>();

                    if (VM.FormatView.Format_MediaType_SelectedItem != "Audio" &&
                        VM.VideoView.Video_Codec_SelectedItem != "None" &&
                        VM.VideoView.Video_Quality_SelectedItem != "None"
                        )
                    {
                        videoList_Pass1 = new List<string>()
                        {
                            "\r\n\r\n" +
                            Video.Codec.VideoCodec(VM.VideoView.Video_HWAccel_Transcode_SelectedItem,
                                             VM.VideoView.Video_Codec_SelectedItem,
                                             VM.VideoView.Video_Codec
                                             ),

                            Video.Quality.PassParams(VM.VideoView.Video_Codec_SelectedItem, //-x265-params pass=1
                                             VM.VideoView.Video_Pass_SelectedItem,
                                             "1"
                                             ),
                            "\r\n" +
                            Video.Params.Video_Params(VM.VideoView.Video_Quality_SelectedItem,
                                                     VM.VideoView.Video_Codec_SelectedItem,
                                                     VM.FormatView.Format_MediaType_SelectedItem
                                                     ),

                            "\r\n" +
                            Video.Encoding.VideoEncodeSpeed(VM.VideoView.Video_EncodeSpeed_Items,
                                                   VM.VideoView.Video_EncodeSpeed_SelectedItem,
                                                   VM.VideoView.Video_Codec_SelectedItem,
                                                   VM.VideoView.Video_Pass_SelectedItem
                                                   ),

                            Video.Quality.VideoQuality(VM.MainView.Batch_IsChecked,
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
                            Video.Quality.PixFmt(VM.VideoView.Video_Codec_SelectedItem,
                                         VM.VideoView.Video_PixelFormat_SelectedItem
                                         ),

                            "\r\n" +
                            Video.Color.Color_Range(VM.VideoView.Video_Color_Range_SelectedItem),
                            "\r\n" +
                            Video.Color.Color_Space(VM.VideoView.Video_Color_Space_SelectedItem),
                             "\r\n" +
                            Video.Color.Color_Primaries(VM.VideoView.Video_Color_Primaries_SelectedItem),
                            "\r\n" +
                            Video.Color.Color_TransferCharacteristics(VM.VideoView.Video_Color_TransferCharacteristics_SelectedItem),

                            "\r\n" +
                            Video.Video.FPS(VM.VideoView.Video_Codec_SelectedItem,
                                      VM.VideoView.Video_FPS_SelectedItem,
                                      VM.VideoView.Video_FPS_Text
                                      ),
                            "\r\n" +
                            Filters.Video.VideoFilter(),
                            "\r\n" +
                            Video.Size.AspectRatio(VM.VideoView.Video_AspectRatio_SelectedItem),
                            "\r\n" +
                            Video.Video.Images(VM.FormatView.Format_MediaType_SelectedItem,
                                         VM.VideoView.Video_Codec_SelectedItem
                                         ),
                            "\r\n" +
                            Video.Quality.Optimize(VM.VideoView.Video_Codec_SelectedItem,
                                                   VM.VideoView.Video_Optimize_Items,
                                                   VM.VideoView.Video_Optimize_SelectedItem,
                                                   VM.VideoView.Video_Video_Optimize_Tune_SelectedItem,
                                                   VM.VideoView.Video_Video_Optimize_Profile_SelectedItem,
                                                   VM.VideoView.Video_Optimize_Level_SelectedItem
                                                   ),

                            // -pass 1, -x265-params pass=2
                            "\r\n" +
                            Video.Quality.Pass1Modifier(VM.VideoView.Video_Codec_SelectedItem,
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
                    // Log Console Message in Pass 2

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
                    // Log Console Message in Pass 2

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

                    // -------------------------
                    // Combine Lists
                    // -------------------------
                    List<string> FFmpegArgsPass1List = initializeList_Pass1
                                                       .Concat(hwAccelDecodeList_Pass1)
                                                       .Concat(inputList_Pass1)
                                                       .Concat(hwAccelTranscodeList_Pass1)
                                                       .Concat(formatList_Pass1)
                                                       .Concat(videoList_Pass1)
                                                       .Concat(subtitleList_Pass1)
                                                       .Concat(audioList_Pass1)
                                                       .Concat(outputList_Pass1)
                                                       .ToList();

                    // Process Priority PowerShell Arguments End
                    FFmpegArgsPass1List.Add("\r\n\r\n" + ProcessPriority_PowerShell_Args_End());

                    // Process Priority PowerShell Set
                    //FFmpegArgsPass1List.Add("\r\n\r\n" + ProcessPriority_PowerShell_Set_End());

                    // Join List with Spaces
                    // Remove: Empty, Null, Standalone LineBreak
                    Video.Quality.pass1Args = string.Join(" ", FFmpegArgsPass1List
                                                               .Where(s => !string.IsNullOrWhiteSpace(s))
                                                               .Where(s => !s.Equals(Environment.NewLine))
                                                               .Where(s => !s.Equals("\r\n\r\n"))
                                                               .Where(s => !s.Equals("\r\n"))
                                                         );


                    // Process Priority Format
                    Video.Quality.pass1Args = ProcessPriority_PowerShell_Set(Video.Quality.pass1Args);


                    // --------------------------------------------------
                    // Pass 2
                    // --------------------------------------------------
                    // -------------------------
                    // Pass 2 Initialize
                    // -------------------------
                    List <string> initializeList_Pass2 = new List<string>()
                    {
                        ProcessPriority() +
                        //PowerShell_CallOperator_FFmpeg() +
                        MainWindow.FFmpegPath() +
                        ProcessPriority_PowerShell_Flags(),

                        "\r\n\r\n" +
                        ProcessPriority_PowerShell_Args_Start(),

                        "\r\n\r\n" +
                        OutputOverwrite()
                    };

                    // -------------------------
                    // HW Accel Decode
                    // -------------------------
                    List<string> hwAccelDecodeList_Pass2 = new List<string>()
                    {
                        "\r\n\r\n" +
                        Video.Encoding.hwAccelDecode
                    };

                    // -------------------------
                    // Input
                    // -------------------------
                    List<string> inputList_Pass2 = new List<string>()
                    {
                        "\r\n\r\n" +
                        //"-i " + "\"" + MainWindow.InputPath("pass 2") + "\"",
                        "-i " + MainWindow.WrapWithQuotes(MainWindow.InputPath("pass 2")),

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
                        Video.Encoding.hwAccelTranscode
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
                            Video.Codec.vCodec,
                            Video.Quality.PassParams(VM.VideoView.Video_Codec_SelectedItem, //-x265-params pass=2
                                                     VM.VideoView.Video_Pass_SelectedItem,
                                                     "2"
                                                     ),
                            "\r\n" +
                            Video.Params.Video_Params(VM.VideoView.Video_Quality_SelectedItem,    // Note: Use Method, not String, to re-generate all Pass 2 Params
                                                     VM.VideoView.Video_Codec_SelectedItem,      //       for 2 Pass -x265-params pass=2
                                                     VM.FormatView.Format_MediaType_SelectedItem
                                                     ),
                            "\r\n" +
                            Video.Encoding.vEncodeSpeed,
                            Video.Quality.vQuality,
                            "\r\n" +
                            Video.Quality.pix_fmt,
                            "\r\n" +
                            Video.Color.colorRange,
                            "\r\n" +
                            Video.Color.colorSpace,
                            "\r\n" +
                            Video.Color.colorPrimaries,
                            "\r\n" +
                            Video.Color.colorTransferCharacteristics,
                            "\r\n" +
                            Video.Video.fps,
                            "\r\n" +
                            Filters.Video.vFilter,
                            "\r\n" +
                            Video.Size.vAspectRatio,
                            "\r\n" +
                            Video.Video.image,
                            "\r\n" +
                            Video.Quality.optimize,
                            "\r\n" +
                            Streams.VideoStreamMaps(),
                            "\r\n" +
                            Video.Quality.Pass2Modifier(VM.VideoView.Video_Codec_SelectedItem, // -pass 2, -x265-params pass=2
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
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Subtitle")) { Foreground = Log.ConsoleAction });
                    };
                    Log.LogActions.Add(Log.WriteAction);

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
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Audio")) { Foreground = Log.ConsoleAction });
                    };
                    Log.LogActions.Add(Log.WriteAction);

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
                            Generate.Audio.Codec.AudioCodec(VM.AudioView.Audio_Codec_SelectedItem,
                                                            VM.AudioView.Audio_Codec
                                                            ),
                            "\r\n" +
                            Audio.Quality.AudioQuality(VM.MainView.Input_Text,
                                               VM.MainView.Batch_IsChecked,
                                               VM.FormatView.Format_MediaType_SelectedItem,
                                               VM.AudioView.Audio_Stream_SelectedItem,
                                               VM.AudioView.Audio_Codec_SelectedItem,
                                               VM.AudioView.Audio_Quality_Items,
                                               VM.AudioView.Audio_Quality_SelectedItem,
                                               VM.AudioView.Audio_BitRate_Text,
                                               VM.AudioView.Audio_VBR_IsChecked
                                               ),
                            Audio.Quality.CompressionLevel(VM.AudioView.Audio_Codec_SelectedItem,
                                                   VM.AudioView.Audio_CompressionLevel_SelectedItem
                                                   ),
                            Audio.Quality.SampleRate(VM.AudioView.Audio_Codec_SelectedItem,
                                                     VM.AudioView.Audio_SampleRate_Items,
                                                     VM.AudioView.Audio_SampleRate_SelectedItem
                                                     ),
                            Audio.Quality.BitDepth(VM.AudioView.Audio_Codec_SelectedItem,
                                                   VM.AudioView.Audio_BitDepth_Items,
                                                   VM.AudioView.Audio_BitDepth_SelectedItem
                                                   ),
                            Audio.Channels.Channel(VM.AudioView.Audio_Codec_SelectedItem,
                                                   VM.AudioView.Audio_Channel_SelectedItem
                                                   ),
                            "\r\n" +
                            Filters.Audio.AudioFilter(),
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
                        Controls.Configure.threads,

                        "\r\n\r\n" +
                        //"\"" + MainWindow.OutputPath() + "\""
                        MainWindow.WrapWithQuotes(MainWindow.OutputPath())
                    };


                    // -------------------------
                    // Combine Lists
                    // -------------------------
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

                    // Process Priority PowerShell Arguments End
                    FFmpegArgsPass2List.Add("\r\n\r\n" + ProcessPriority_PowerShell_Args_End());

                    // Process Priority PowerShell Set
                    //FFmpegArgsPass2List.Add("\r\n\r\n" + ProcessPriority_PowerShell_Set_End());

                    // Join List with Spaces
                    // Remove: Empty, Null, Standalone LineBreak
                    Video.Quality.pass2Args = string.Join(" ", FFmpegArgsPass2List
                                                               .Where(s => !string.IsNullOrWhiteSpace(s))
                                                               .Where(s => !s.Equals(Environment.NewLine))
                                                               .Where(s => !s.Equals("\r\n\r\n"))
                                                               .Where(s => !s.Equals("\r\n"))
                                                         );

                    // Process Priority Format
                    Video.Quality.pass2Args = ProcessPriority_PowerShell_Set(Video.Quality.pass2Args);


                    // Combine Pass 1 & Pass 2 Args
                    Video.Quality.v2PassArgs = Video.Quality.pass1Args +
                                               //" " +
                                               "\r\n\r\n" +
                                               Shell_LogicalOperator_And() +
                                               "\r\n\r\n" +
                                               Video.Quality.pass2Args;
                }


                // Return Value
                return Video.Quality.v2PassArgs;
            }
        }
    }
}
