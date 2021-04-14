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

 * KeepWindow
 * Generate Single FFmpeg Args
 * FFmpeg Script
 * FFmpeg Start
 * FFmpeg Convert
---------------------------------- */

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using ViewModel;
using Axiom;
using System.Collections;
using System.Threading.Tasks;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Generate
{
    public partial class FFmpeg
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
        /// Output Overwrite
        /// </summary>
        public static String OutputOverwrite()
        {
            switch (VM.ConfigureView.OutputOverwrite_SelectedItem)
            {
                // Always
                case "Always":
                    return "-y";
                // Never
                case "Never":
                    return "-n";
                // Ask
                case "Ask":
                    return string.Empty;
                // Unknown
                default:
                    return string.Empty;
            }
        }


        /// <summary>
        /// FFmpeg Single File - Generate Args
        /// </summary>
        public static String Generate_SingleArgs()
        {
            if (VM.MainView.Batch_IsChecked == false)
            {
                List<string> ffmpegArgsList = new List<string>();

                // Add Arguments
                switch (VM.VideoView.Video_Pass_SelectedItem)
                {
                    // -------------------------
                    // CRF
                    // -------------------------
                    case "CRF":
                        ffmpegArgsList.Add(CRF.Arguments());
                        break;

                    // -------------------------
                    // 1 Pass
                    // -------------------------
                    case "1 Pass":
                        ffmpegArgsList.Add(_1_Pass.Arguments());
                        break;

                    // -------------------------
                    // 2 Pass
                    // -------------------------
                    case "2 Pass":
                        ffmpegArgsList.Add(_2_Pass.Arguments());
                        break;

                    // -------------------------
                    // Empty, none, auto / Audio
                    // -------------------------
                    default:
                        ffmpegArgsList.Add(_1_Pass.Arguments());
                        break;
                }

                // Join List with Spaces
                // Remove: Empty, Null, Standalone LineBreak
                ffmpegArgsSort = string.Join(" ", ffmpegArgsList
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


        /// <summary>
        /// FFmpeg Generate Script
        /// </summary>
        public static async Task<int> FFmpegGenerateScriptAsync()
        {
            int count = 0;
            await Task.Run(() =>
            {
                FFmpegGenerateScript();
            });

            return count;
        }
        public static void FFmpegGenerateScript()
        {
            // -------------------------
            // Write FFmpeg Args
            // -------------------------
            //VM.MainView.ScriptView_Text = ffmpegArgs;
            MainWindow.scriptText = ffmpegArgs;  // Prevents ScriptView Flicker

            // -------------------------
            // Sort Script
            // -------------------------
            // Only if Auto Sort is enabled
            if (VM.MainView.AutoSortScript_IsChecked == true)
            {
                Controls.ScriptView.sort = false;
                MainWindow.Sort();
            }
            // Auto Sort Script Off
            else if (VM.MainView.AutoSortScript_IsChecked == false)
            {
                // Inline
                VM.MainView.ScriptView_Text = ffmpegArgs;
            }
        }

    }
}
