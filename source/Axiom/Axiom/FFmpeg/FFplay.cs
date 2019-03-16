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

using System.Collections.Generic;
using System.Windows;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class FFplay
    {
        // FFplay
        public static string ffplay; // ffplay.exe

        /// <summary>
        ///     Preview FFplay
        /// </summary>
        public static void Preview(MainWindow mainwindow, ViewModel vm)
        {
            // -------------------------
            // Clear Variables before Run
            // -------------------------
            ffplay = string.Empty;
            MainWindow.ClearVariables(vm);

            // Ignore if Batch
            if (vm.Batch_IsChecked == false)
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

                    "-i " + "\"" + MainWindow.InputPath(vm) + "\"",

                    Subtitle.SubtitlesExternal(vm),

                    //Video.VideoCodec(),
                    //Video.Speed(mainwindow),
                    //Video.VideoQuality(mainwindow),
                    Video.FPS(vm.MediaType_SelectedItem,
                              vm.VideoCodec_SelectedItem,
                              vm.VideoQuality_SelectedItem,
                              vm.FPS_SelectedItem,
                              vm.FPS_Text
                              ),
                    VideoFilters.VideoFilter(vm),
                    //Video.ScalingAlgorithm(vm),
                    Video.Images(vm),
                    //Video.Optimize(mainwindow),
                    //Streams.VideoStreamMaps(mainwindow),

                    //Video.SubtitleCodec(mainwindow),
                    //"Streams.SubtitleMaps(mainwindow),

                    //Audio.AudioCodec(mainwindow),
                    //Audio.AudioQuality(mainwindow),
                    Audio.SampleRate(vm.MediaType_SelectedItem,
                                     vm.AudioCodec_SelectedItem,
                                     vm.AudioStream_SelectedItem,
                                     vm.AudioQuality_SelectedItem,
                                     vm.AudioChannel_SelectedItem,
                                     vm.AudioSampleRate_Items,
                                     vm.AudioSampleRate_SelectedItem
                                     ),
                    Audio.BitDepth(vm.MediaType_SelectedItem,
                                   vm.AudioCodec_SelectedItem,
                                   vm.AudioStream_SelectedItem,
                                   vm.AudioQuality_SelectedItem,
                                   vm.AudioBitDepth_Items,
                                   vm.AudioBitDepth_SelectedItem
                                   ),
                    Audio.Channel(vm.MediaType_SelectedItem,
                                  vm.AudioCodec_SelectedItem,
                                  vm.AudioStream_SelectedItem,
                                  vm.AudioQuality_SelectedItem,
                                  vm.AudioChannel_SelectedItem
                                  ),
                    AudioFilters.AudioFilter(vm),
                    //Streams.AudioStreamMaps(mainwindow),

                    //Format.Cut(mainwindow),

                    //Streams.FormatMaps(mainwindow),

                    //Format.ForceFormat(mainwindow),

                    //MainWindow.ThreadDetect(mainwindow),

                    //"\"" + MainWindow.OutputPath(mainwindow) + "\""
                };


                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                string ffplayArgs = MainWindow.ReplaceLineBreaksWithSpaces(
                                        string.Join(" ", FFplayArgsList)
                                    );

                //string ffplayArgs = string.Join(" ", FFplayArgsList
                //                          .Where(s => !string.IsNullOrEmpty(s)))
                //                          .Replace("\r\n", " ") //Remove Linebreaks
                //                          .Replace(Environment.NewLine, " ");


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
