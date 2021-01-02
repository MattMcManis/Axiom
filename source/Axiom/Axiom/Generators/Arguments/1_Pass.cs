/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2021 Matt McManis
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
        public class _1_Pass
        {
            /// <summary>
            /// 1 Pass & CRF Arguments
            /// </summary>
            public static String Arguments()
            {
                // -------------------------
                //  Single Pass (1 Pass & CRF)
                // -------------------------
                if (VM.VideoView.Video_Pass_SelectedItem == "1 Pass" ||
                    VM.VideoView.Video_Pass_SelectedItem == "CRF" ||
                    VM.VideoView.Video_Pass_SelectedItem == "none" ||
                    VM.VideoView.Video_Pass_SelectedItem == "auto" //||
                    //VM.FormatView.Format_Container_SelectedItem == "ogv" //ogv (special rule)
                    )
                {
                    // -------------------------
                    // FFmpeg Initialize
                    // -------------------------
                    IEnumerable<string> ffmpegInitializeList = new List<string>()
                    {
                        Sys.Shell.ShellTitle() +
                        Sys.Shell.ProcessPriority() +
                        MainWindow.FFmpegPath() +
                        Sys.Shell.ProcessPriority_PowerShell_Flags(),
                    };

                    // -------------------------
                    // Options
                    // -------------------------
                    IEnumerable<string> optionsList = new List<string>()
                    {
                        "\r\n\r\n" +
                        OutputOverwrite(), //-y, -n
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

                    IEnumerable<string> hwAccelDecodeList = new List<string>()
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
                    IEnumerable<string> inputList = new List<string>()
                    {
                        "\r\n\r\n" +
                        "-i " + MainWindow.WrapWithQuotes(MainWindow.InputPath("pass 1")),

                        "\r\n\r\n" +
                        Audio.Audio.AudioMux(VM.AudioView.Audio_Codec_SelectedItem,
                                             VM.AudioView.Audio_Stream_SelectedItem
                                             ),

                        "\r\n\r\n" +
                        Subtitle.Subtitle.SubtitlesMux(VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                       VM.SubtitleView.Subtitle_Stream_SelectedItem
                                                      ),
                    };


                    // -------------------------
                    // HW Accel Transcode
                    // -------------------------
                    IEnumerable<string> hwAccelTranscodeList = new List<string>()
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

                    IEnumerable<string> formatList = new List<string>()
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

                    //System.Windows.MessageBox.Show(string.Join("\n",VM.VideoView.Video_PixelFormat_Items)); //debug

                    IEnumerable<string> videoList = new List<string>();

                    if (VM.FormatView.Format_MediaType_SelectedItem != "Audio" &&
                        VM.VideoView.Video_Codec_SelectedItem != "None" &&
                        VM.VideoView.Video_Quality_SelectedItem != "None"
                        )
                    {
                        videoList = new List<string>()
                        {
                            "\r\n\r\n" +
                            Video.Codec.VideoCodec(VM.VideoView.Video_HWAccel_Transcode_SelectedItem,
                                                   VM.VideoView.Video_Codec_SelectedItem,
                                                   VM.VideoView.Video_Codec
                                                   ),

                            // No PassParams() for 1 Pass / CRF

                            "\r\n" +
                            Video.Encoding.VideoEncodeSpeed(VM.VideoView.Video_EncodeSpeed_Items,
                                                            VM.VideoView.Video_EncodeSpeed_SelectedItem,
                                                            VM.VideoView.Video_Codec_SelectedItem,
                                                            VM.VideoView.Video_Pass_SelectedItem
                                                            ),

                            Video.Quality.VideoQuality(VM.MainView.Batch_IsChecked,
                                                       (bool)VM.VideoView.Video_VBR_IsChecked,
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
                            Video.Params.QualityParams(VM.VideoView.Video_Quality_SelectedItem,
                                                       VM.VideoView.Video_Codec_SelectedItem,
                                                       VM.FormatView.Format_MediaType_SelectedItem
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
                            Video.Quality.Optimize(VM.VideoView.Video_Codec_SelectedItem,
                                                   VM.VideoView.Video_Optimize_Items,
                                                   VM.VideoView.Video_Optimize_SelectedItem,
                                                   VM.VideoView.Video_Optimize_Tune_SelectedItem,
                                                   VM.VideoView.Video_Optimize_Profile_SelectedItem,
                                                   VM.VideoView.Video_Optimize_Level_SelectedItem
                                                   ),

                            "\r\n" +
                            Video.Video.FPS(VM.VideoView.Video_Codec_SelectedItem,
                                            VM.VideoView.Video_FPS_SelectedItem,
                                            VM.VideoView.Video_FPS_Text
                                           ),

                            "\r\n" +
                            Filters.Video.VideoFilter(),

                            "\r\n" +
                            Video.Video.Vsync(VM.VideoView.Video_Codec_SelectedItem, // vsync after filters
                                              VM.VideoView.Video_Vsync_SelectedItem
                                             ),

                            "\r\n" +
                            Video.Size.AspectRatio(VM.VideoView.Video_AspectRatio_SelectedItem),

                            "\r\n" +
                            Video.Video.Images(VM.FormatView.Format_MediaType_SelectedItem,
                                               VM.VideoView.Video_Codec_SelectedItem
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

                    IEnumerable<string> audioList = new List<string>();

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
                            Audio.Codec.AudioCodec(VM.AudioView.Audio_Codec_SelectedItem,
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
                                                       (bool)VM.AudioView.Audio_VBR_IsChecked
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

                            "\r\n\r\n" +
                            Audio.Metadata.Audio(),
                        };
                    }
                    // Disable Audio
                    else
                    {
                        audioList = new List<string>()
                        {
                            "\r\n\r\n" +
                            "-an",
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

                    IEnumerable<string> subtitleList = new List<string>();

                    if (VM.FormatView.Format_MediaType_SelectedItem != "Audio" &&
                        VM.VideoView.Video_Codec_SelectedItem != "None" &&
                        VM.VideoView.Video_Quality_SelectedItem != "None"
                        )
                    {
                        subtitleList = new List<string>()
                        {
                            "\r\n\r\n" +
                            Subtitle.Subtitle.SubtitleCodec(VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                   VM.SubtitleView.Subtitle_Codec
                                                   ),
                            "\r\n" +
                            Streams.SubtitleMaps(),

                            "\r\n\r\n" +
                            Subtitle.Metadata.Subtitles(),
                        };
                    }
                    // Disable Subtitles
                    else
                    {
                        subtitleList = new List<string>()
                        {
                            "\r\n\r\n" +
                            "-sn",
                        };
                    }

                    // -------------------------
                    // Chapters
                    // -------------------------
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Chapters")) { Foreground = Log.ConsoleAction });
                    };
                    Log.LogActions.Add(Log.WriteAction);

                    IEnumerable<string> chaptersList = new List<string>();

                    if (VM.FormatView.Format_MediaType_SelectedItem != "Image" &&
                        VM.FormatView.Format_MediaType_SelectedItem != "Sequence"
                        )
                    {
                        chaptersList = new List<string>()
                        {
                            "\r\n\r\n" +
                            Streams.ChaptersMaps(),
                        };
                    }
                    // Disable Chapters
                    //else
                    //{
                    //    chaptersList = new List<string>()
                    //    {
                    //        "\r\n\r\n" +
                    //        "-cn",
                    //    };
                    //}

                    // -------------------------
                    // Output
                    // -------------------------
                    IEnumerable<string> outputList = new List<string>()
                    {
                        "\r\n\r\n" +
                        Streams.FormatMaps(),

                        "\r\n\r\n" +
                        Format.ForceFormat(VM.FormatView.Format_Container_SelectedItem),

                        "\r\n\r\n" +
                        MainWindow.ThreadDetect(),

                        "\r\n\r\n" +
                        MainWindow.WrapWithQuotes(MainWindow.OutputPath()),
                    };


                    // --------------------------------------------------
                    // Combine Lists
                    // --------------------------------------------------
                    // -------------------------
                    // FFmpeg Arguments
                    // -------------------------
                    IEnumerable<string> FFmpegArgs_SinglePass_List = optionsList
                                                                     .Concat(hwAccelDecodeList)
                                                                     .Concat(inputList)
                                                                     .Concat(hwAccelTranscodeList)
                                                                     .Concat(formatList)
                                                                     .Concat(videoList)
                                                                     .Concat(audioList)
                                                                     .Concat(subtitleList)
                                                                     .Concat(chaptersList)
                                                                     .Concat(outputList)
                                                                     .ToList();

                    // -------------------------
                    // Shell Arguments
                    // -------------------------
                    IEnumerable<string> ShellArgs_List = // Process Priority
                                                         Sys.Shell.ProcessPriority_PowerShell_Set( 
                                                            // FFmpeg Init
                                                            ffmpegInitializeList
                                                            // FFmpeg PowerShell -ArgsList
                                                            .Concat(Sys.Shell.ProcessPriority_PowerShell_ArgumentsListWrap(
                                                                        // FFmpeg Arguments
                                                                        FFmpegArgs_SinglePass_List
                                                                    )
                                                                )
                                                             )
                                                        .ToList();

                    // Join List with Spaces
                    // Remove: Empty, Null, Line Breaks
                    Video.Quality.passSingle = string.Join(" ", ShellArgs_List
                                                                .Where(s => !string.IsNullOrWhiteSpace(s))
                                                                .Where(s => !s.Equals(Environment.NewLine))
                                                                .Where(s => !s.Equals("\r\n\r\n"))
                                                                .Where(s => !s.Equals("\r\n"))
                                                                .Where(s => !s.Equals("\n"))
                                                                .Where(s => !s.Equals("\u2028"))
                                                                .Where(s => !s.Equals("\u000A"))
                                                                .Where(s => !s.Equals("\u000B"))
                                                                .Where(s => !s.Equals("\u000C"))
                                                                .Where(s => !s.Equals("\u000D"))
                                                                .Where(s => !s.Equals("\u0085"))
                                                                .Where(s => !s.Equals("\u2028"))
                                                                .Where(s => !s.Equals("\u2029"))
                                                          );
                }


                // Return Value
                return Video.Quality.passSingle;
            }
        }
    }
}
