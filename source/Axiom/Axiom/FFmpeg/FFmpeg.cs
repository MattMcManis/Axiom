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
        /// Process Priority
        /// </summary>
        public static String ProcessPriority()
        {
            // Empty
            // Default
            if (string.IsNullOrWhiteSpace(VM.ConfigureView.ProcessPriority_SelectedItem) ||
                VM.ConfigureView.ProcessPriority_SelectedItem == "Default")
            {
                return string.Empty;
            }

            // Shell Check
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // CMD
                case "CMD":
                    return "start \"\" /b /wait " + "/" + ProcessPriorityLevel() + " ";
                    //return "start " + "/" + ProcessPriorityLevel() + " /wait /b ";

                // PowerShell
                case "PowerShell":
                    return "$Process = Start-Process ";

                // Empty
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Process Priority PowerShell Flags
        /// </summary>
        public static String ProcessPriority_PowerShell_Flags()
        {
            // Empty
            // Default
            if (string.IsNullOrWhiteSpace(VM.ConfigureView.ProcessPriority_SelectedItem) ||
                VM.ConfigureView.ProcessPriority_SelectedItem == "Default")
            {
                return string.Empty;
            }

            //// Wait
            //string wait = string.Empty;
            //switch (VM.VideoView.Video_Pass_SelectedItem)
            //{
            //    // CRF
            //    case "CRF":
            //        wait = " -Wait";
            //        break;

            //    // 1 Pass
            //    case "1 Pass":
            //        wait = " -Wait";
            //        break;

            //    // 2 Pass
            //    case "2 Pass":
            //        wait = string.Empty;
            //        break;

            //    // auto, none, Unknown
            //    default:
            //        return string.Empty;
            //}

            // Shell Check
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // CMD
                case "CMD":
                    return string.Empty;

                // PowerShell
                case "PowerShell":
                    return " -NoNewWindow"/* + wait*/;
                    //return " -NoNewWindow -Wait";

                // Empty
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Process Priority PowerShell Single Quote Start
        /// </summary>
        public static String ProcessPriority_PowerShell_Args_Start()
        {
            // Empty
            // Default
            if (string.IsNullOrWhiteSpace(VM.ConfigureView.ProcessPriority_SelectedItem) ||
                VM.ConfigureView.ProcessPriority_SelectedItem == "Default")
            {
                return string.Empty;
            }

            // Shell Check
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // PowerShell
                case "PowerShell":
                    switch (VM.MainView.Batch_IsChecked)
                    {
                        // Single
                        case false:
                            return "-ArgumentList '";

                        // Batch
                        case true:
                            return "-ArgumentList \"";

                        // Unknown
                        default:
                            return "-ArgumentList '";
                    }
                    //return "-ArgumentList '";

                // Empty
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Process Priority PowerShell Single Quote End
        /// </summary>
        public static String ProcessPriority_PowerShell_Args_End()
        {
            // Empty
            // Default
            if (string.IsNullOrWhiteSpace(VM.ConfigureView.ProcessPriority_SelectedItem) ||
                VM.ConfigureView.ProcessPriority_SelectedItem == "Default")
            {
                return string.Empty;
            }

            // Shell Check
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // PowerShell
                case "PowerShell":
                    switch (VM.MainView.Batch_IsChecked)
                    {
                        // Single
                        case false:
                            return "'";

                        // Batch
                        case true:
                            return "\"";

                        // Unknown
                        default:
                            return "'";
                    }
                    //return "'";

                // Empty
                default:
                    return string.Empty;
            }
        }


        /// <summary>
        /// Process Priority PowerShell Set Start
        /// </summary>
        public static String ProcessPriority_PowerShell_Set(string args)
        {

            // Empty
            // Default
            if (string.IsNullOrWhiteSpace(VM.ConfigureView.ProcessPriority_SelectedItem) ||
                VM.ConfigureView.ProcessPriority_SelectedItem == "Default")
            {
                return args;
            }

            // Format Arguments
            // Shell Check
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // CMD
                case "CMD":
                    // Do not format
                    return args;

                // PowerShell
                case "PowerShell":
                    return "(" + args + " -PassThru).PriorityClass = [System.Diagnostics.ProcessPriorityClass]::" + ProcessPriorityLevel() +"; Wait-Process -Id $Process.id";

                // Unknown
                default:
                    return args;
            }
            
        }


        /// <summary>
        /// Process Priority PowerShell Set Start
        /// </summary>
        //public static String ProcessPriority_PowerShell_Set_Start()
        //{
        //    // Empty
        //    // Default
        //    if (string.IsNullOrWhiteSpace(VM.ConfigureView.ProcessPriority_SelectedItem) ||
        //        VM.ConfigureView.ProcessPriority_SelectedItem == "Default")
        //    {
        //        return string.Empty;
        //    }

        //    // Shell Check
        //    switch (VM.ConfigureView.Shell_SelectedItem)
        //    {
        //        //// CMD
        //        //case "CMD":
        //        //    return string.Empty;

        //        // PowerShell
        //        case "PowerShell":
        //            return "(";

        //        // Empty
        //        default:
        //            return string.Empty;
        //    }
        //}

        /// <summary>
        /// Process Priority PowerShell Set
        /// </summary>
        //public static String ProcessPriority_PowerShell_Set_End()
        //{
        //    // Empty
        //    // Default
        //    if (string.IsNullOrWhiteSpace(VM.ConfigureView.ProcessPriority_SelectedItem) ||
        //        VM.ConfigureView.ProcessPriority_SelectedItem == "Default")
        //    {
        //        return string.Empty;
        //    }

        //    // Shell Check
        //    switch (VM.ConfigureView.Shell_SelectedItem)
        //    {
        //        //// CMD
        //        //case "CMD":
        //        //    return string.Empty;

        //        // PowerShell
        //        case "PowerShell":
        //            return "-PassThru).PriorityClass = [System.Diagnostics.ProcessPriorityClass]::" + ProcessPriorityLevel();
        //            //return "| ForEach-Object { Set-ProcessPriority -ProcessId $_.id -Priority " + ProcessPriorityLevel() + " }";

        //        // Empty
        //        default:
        //            return string.Empty;
        //    }
        //}

        /// <summary>
        /// Process Priority
        /// </summary>
        public static String ProcessPriorityLevel()
        {
            // Shell Check
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // CMD
                case "CMD":
                    switch (VM.ConfigureView.ProcessPriority_SelectedItem)
                    {
                        // Default
                        case "Default":
                            return string.Empty;

                        // Low
                        case "Low":
                            return "low";

                        // Below Normal
                        case "Below Normal":
                            return "belownormal";

                        // Normal
                        case "Normal":
                            return "normal";

                        // Above Normal
                        case "Above Normal":
                            return "abovenormal";

                        // High
                        case "High":
                            return "high";

                        // Unknown
                        default:
                            return "normal";
                    }

                // PowerShell
                case "PowerShell":
                    switch (VM.ConfigureView.ProcessPriority_SelectedItem)
                    {
                        // Default
                        case "Default":
                            return string.Empty;

                        // Low
                        case "Low":
                            return "Low";

                        // Below Normal
                        case "Below Normal":
                            return "BelowNormal";

                        // Normal
                        case "Normal":
                            return "Normal";

                        // Above Normal
                        case "Above Normal":
                            return "AboveNormal";

                        // High
                        case "High":
                            return "High";

                        // Unknown
                        default:
                            return "Normal";
                    }

                // Empty
                default:
                    return string.Empty;
            }
        }


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
        /// PowerShell Call Operator
        /// </summary>
        /// <remarks>
        /// Adds & symbol before Path to FFmpeg
        /// e.g. & "C:\path\to\ffmpeg\ffmpeg.exe"
        /// </remarks>
        public static String PowerShell_CallOperator()
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
        /// Shell LogicalOperator And
        /// </summary>
        /// <remarks>
        /// Selects && for CMD or ; for PowerShell
        /// </remarks>
        public static String Shell_LogicalOperator_And()
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
                                                     "cd " + "\"" + MainWindow.outputDir + "\"" +
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
                                                     "-command \"Set-Location " + "\"" + MainWindow.outputDir.Replace("\\", "\\\\") // Format Backslashes for PowerShell \ → \\
                                                                                                             .Replace("\"", "\\\"") + // Format Quotes " → \"
                                                                                  "\"" +
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
