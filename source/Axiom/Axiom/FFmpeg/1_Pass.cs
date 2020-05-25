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

                        //"\r\n" +
                        // No PassParams() for 1 Pass / CRF
                        //VideoParams.Video_Params(VM.VideoView.Video_Quality_SelectedItem,
                        //                         VM.VideoView.Video_Codec_SelectedItem,
                        //                         VM.FormatView.Format_MediaType_SelectedItem
                        //                         ),

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
                                           VM.VideoView.Video_BitRate_Text.ToUpper(),
                                           VM.VideoView.Video_MinRate_Text.ToUpper(),
                                           VM.VideoView.Video_MaxRate_Text.ToUpper(),
                                           VM.VideoView.Video_BufSize_Text.ToUpper(),
                                           VM.MainView.Input_Text
                                           ),

                        "\r\n" +
                        VideoParams.Video_Params(VM.VideoView.Video_Quality_SelectedItem,
                                                 VM.VideoView.Video_Codec_SelectedItem,
                                                 VM.FormatView.Format_MediaType_SelectedItem
                                                 ),

                        "\r\n" +
                        Video.PixFmt(VM.VideoView.Video_Codec_SelectedItem,
                                     VM.VideoView.Video_PixelFormat_SelectedItem
                                     ),

                        "\r\n" +
                        Video.Color_Range(VM.VideoView.Video_Color_Range_SelectedItem),
                        "\r\n" +
                        Video.Color_Space(VM.VideoView.Video_Color_Space_SelectedItem),
                         "\r\n" +
                        Video.Color_Primaries(VM.VideoView.Video_Color_Primaries_SelectedItem),
                        "\r\n" +
                        Video.Color_TransferCharacteristics(VM.VideoView.Video_Color_TransferCharacteristics_SelectedItem), 

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
                                                    .Where(s => !string.IsNullOrWhiteSpace(s))
                                                    .Where(s => !s.Equals(Environment.NewLine))
                                                    .Where(s => !s.Equals("\r\n\r\n"))
                                                    .Where(s => !s.Equals("\r\n"))
                                              );
            }


            // Return Value
            return Video.passSingle;
        }
    }
}
