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
        /// FFmpeg Single File - Generate Args
        /// </summary>
        public static String Generate_SingleArgs()
        {
            if (VM.MainView.Batch_IsChecked == false)
            {
                List<string> ffmpegArgsList = new List<string>()
                {
                    Exe_InvokeOperator_PowerShell() +
                    MainWindow.FFmpegPath(),

                    "\r\n\r\n" +
                    "-y"
                };

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
            FFmpegStart(MainWindow.ReplaceLineBreaksWithSpaces(VM.MainView.ScriptView_Text));
        }


    }
}
