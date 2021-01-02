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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ViewModel;
using Axiom;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Preview
{
    public class FFplay
    {
        // FFplay
        public static string ffplay { get; set; } // ffplay.exe

        /// <summary>
        /// Preview FFplay
        /// </summary>
        public static void Preview()
        {
            // -------------------------
            // Clear Variables before Run
            // -------------------------
            ffplay = string.Empty;
            MainWindow.ClearGlobalVariables();

            // Ignore if Batch
            if (VM.MainView.Batch_IsChecked == false)
            {
                // -------------------------
                // Set FFprobe Path
                // -------------------------
                MainWindow.FFplayPath();

                // -------------------------
                //  Arguments List
                // -------------------------
                List<string> FFplayArgsList = new List<string>()
                {
                    //ffplay,

                    "-i " + "\"" + MainWindow.InputPath("pass 1") + "\"",

                    Generate.Audio.Audio.AudioMux(VM.AudioView.Audio_Codec_SelectedItem,
                                                  VM.AudioView.Audio_Stream_SelectedItem
                                                 ),

                    Generate.Subtitle.Subtitle.SubtitlesMux(VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                            VM.SubtitleView.Subtitle_Stream_SelectedItem
                                                           ),

                    //Video.VideoCodec(),
                    //Video.Speed(),
                    //Video.VideoQuality(),
                    Generate.Video.Video.FPS(VM.VideoView.Video_Codec_SelectedItem,
                                             VM.VideoView.Video_FPS_SelectedItem,
                                             VM.VideoView.Video_FPS_Text
                                            ),

                    Filters.Video.VideoFilter(),

                    Generate.Video.Video.Images(VM.FormatView.Format_MediaType_SelectedItem,
                                                VM.VideoView.Video_Codec_SelectedItem
                                               ),
                    //Video.Optimize(),
                    //Streams.VideoStreamMaps(),

                    //Video.SubtitleCodec(),
                    //"Streams.SubtitleMaps(),

                    //Audio.AudioCodec(),
                    //Audio.AudioQuality(),
                    Generate.Audio.Quality.SampleRate(VM.AudioView.Audio_Codec_SelectedItem,
                                                      VM.AudioView.Audio_SampleRate_Items,
                                                      VM.AudioView.Audio_SampleRate_SelectedItem
                                                     ),

                    Generate.Audio.Quality.BitDepth(VM.AudioView.Audio_Codec_SelectedItem,
                                                    VM.AudioView.Audio_BitDepth_Items,
                                                    VM.AudioView.Audio_BitDepth_SelectedItem
                                                   ),

                    Generate.Audio.Channels.Channel(VM.AudioView.Audio_Codec_SelectedItem,
                                                   VM.AudioView.Audio_Channel_SelectedItem
                                                   ),

                    Filters.Audio.AudioFilter(),
                    //Streams.AudioStreamMaps(),

                    //Format.Cut(),

                    //Streams.FormatMaps(),

                    //Format.ForceFormat(),

                    //MainWindow.ThreadDetect(),

                    //"\"" + MainWindow.OutputPath() + "\""
                };


                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                string ffplayArgs = MainWindow.ReplaceLineBreaksWithSpaces(
                                            string.Join(" ", FFplayArgsList
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
                                              )
                                            );
                //MessageBox.Show(ffplayArgs); //debug


                // Start FFplay
                System.Diagnostics.Process.Start(
                    ffplay,
                    //"/c " //always close cmd
                    //FFmpeg.KeepWindow(mainwindow)
                    ffplayArgs
                );
            }

            // Batch Warning
            else
            {
                MessageBox.Show("Cannot Preview Batch.",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }

    }
}
