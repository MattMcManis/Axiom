/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017, 2018 Matt McManis
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
using System.Windows;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class FFplay
    {
        // FFplay
        public static string ffplay; // ffplay.exe

        /// <summary>
        ///     Preview FFplay
        /// </summary>
        public static void Preview(MainWindow mainwindow)
        {
            // -------------------------
            // Clear Variables before Run
            // -------------------------
            ffplay = string.Empty;
            MainWindow.ClearVariables(mainwindow);

            // Ignore if Batch
            if (mainwindow.tglBatch.IsChecked == false)
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
                    ffplay,

                    "-i " + "\"" + MainWindow.InputPath(mainwindow) + "\"",

                    Video.Subtitles(mainwindow),

                    //Video.VideoCodec(mainwindow),
                    //Video.Speed(mainwindow),
                    //Video.VideoQuality(mainwindow),
                    Video.FPS(mainwindow),
                    VideoFilters.VideoFilter(mainwindow),
                    Video.ScalingAlgorithm(mainwindow),
                    Video.Images(mainwindow),
                    Video.Optimize(mainwindow),
                    //Streams.VideoStreamMaps(mainwindow),

                    //Video.SubtitleCodec(mainwindow),
                    //"Streams.SubtitleMaps(mainwindow),

                    //Audio.AudioCodec(mainwindow),
                    //Audio.AudioQuality(mainwindow),
                    Audio.SampleRate(mainwindow),
                    Audio.BitDepth(mainwindow),
                    Audio.Channel(mainwindow),
                    Audio.AudioFilter(mainwindow),
                    //Streams.AudioStreamMaps(mainwindow),

                    //Format.Cut(mainwindow),

                    //Streams.FormatMaps(mainwindow),

                    //Format.ForceFormat(mainwindow),

                    //MainWindow.ThreadDetect(mainwindow),

                    //"\"" + MainWindow.OutputPath(mainwindow) + "\""
                };


                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                string ffplayArgs = string.Join(" ",
                    FFplayArgsList
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Where(s => !s.Equals(Environment.NewLine))
                    .Where(s => !s.Equals("\r\n\r\n"))
                    .Where(s => !s.Equals("\r\n"))
                    );

                MessageBox.Show(ffplayArgs);


                // Start FFplay
                System.Diagnostics.Process.Start(
                    "cmd.exe",
                    FFmpeg.KeepWindow(mainwindow)
                    + ffplayArgs
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
