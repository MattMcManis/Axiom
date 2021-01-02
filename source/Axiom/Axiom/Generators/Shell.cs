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
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Sys
{
    public class Shell
    {
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
        /// Shell Title
        /// </summary>
        public static String ShellTitle()
        {
            // Empty
            // Default
            if (string.IsNullOrWhiteSpace(VM.ConfigureView.ShellTitle_SelectedItem) ||
                VM.ConfigureView.ShellTitle_SelectedItem == "Disabled")
            {
                return string.Empty;
            }

            string title = string.Empty;

            // Shell Check
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // CMD
                case "CMD":      
                    switch (VM.ConfigureView.ShellTitle_SelectedItem)
                    {
                        case "Custom":
                            title = string.Empty;
                            break;

                        case "Filename":
                            MainWindow.OutputPath(); // Set Output
                            title = MainWindow.outputFileName_Original + MainWindow.outputExt;
                            //title = MainWindow.OutputFile_Token_Remover(Path.GetFileName(MainWindow.OutputPath()));
                            break;

                        case "Tokens":
                            title = MainWindow.TokenAppender(string.Empty);
                            break;

                        case "Filename+Tokens":
                            title = Path.GetFileName(MainWindow.OutputPath());
                            break;

                        default:
                            title = string.Empty;
                            break;
                    }

                    return "title " + "\"" + title + "\"" + " & " + "\r\n";

                // PowerShell
                case "PowerShell":
                    switch (VM.ConfigureView.ShellTitle_SelectedItem)
                    {
                        case "Custom":
                            title = string.Empty;
                            break;

                        case "Filename":
                            MainWindow.OutputPath(); // Set Output
                            title = MainWindow.outputFileName_Original + MainWindow.outputExt;
                            //title = MainWindow.OutputFile_Token_Remover(Path.GetFileName(MainWindow.OutputPath()));
                            break;

                        case "Tokens":
                            title = MainWindow.TokenAppender(string.Empty);
                            break;

                        case "Filename+Tokens":
                            title = Path.GetFileName(MainWindow.OutputPath());
                            break;

                        default:
                            title = string.Empty;
                            break;
                    }

                    return "$host.ui.RawUI.WindowTitle = " + "\"" + title + "\"" + "; " + "\r\n";

                // Empty
                default:
                    return string.Empty;
            }
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

                // PowerShell
                case "PowerShell":
                    return "($Process = Start-Process ";
                    //return "$Process = Start-Process ";

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

            // Shell Check
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // CMD
                case "CMD":
                    return string.Empty;

                // PowerShell
                case "PowerShell":
                    return " -NoNewWindow";

                // Empty
                default:
                    return string.Empty;
            }
        }


        /// <summary>
        /// Process Priority - PowerShell - ArgumentsList Wrap
        /// </summary>
        public static IEnumerable<string> ProcessPriority_PowerShell_ArgumentsListWrap(IEnumerable<string> ffmpegArgs)
        {
            // Process Priority Default
            if (string.IsNullOrWhiteSpace(VM.ConfigureView.ProcessPriority_SelectedItem) ||
                VM.ConfigureView.ProcessPriority_SelectedItem == "Default")
            {
                return ffmpegArgs;
            }

            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // CMD
                case "CMD":
                    return ffmpegArgs;

                // PowerShell
                case "PowerShell":
                    // Wrap
                    IEnumerable<string> start = new List<string>()
                    {
                        "\r\n\r\n" +
                        "-ArgumentList \""
                    };
                    IEnumerable<string> end = new List<string>()
                    {
                        "\r\n\r\n" +
                        "\""
                    };
                    return start
                            .Concat(ffmpegArgs)
                            .Concat(end)
                            .ToList();

                // Unkown
                default:
                    return ffmpegArgs;
            }
        }


        /// <summary>
        /// Process Priority - PowerShell - Set
        /// </summary>
        public static IEnumerable<string> ProcessPriority_PowerShell_Set(IEnumerable<string> shellArgs)
        {
            // Process Priority Default
            if (string.IsNullOrWhiteSpace(VM.ConfigureView.ProcessPriority_SelectedItem) ||
                VM.ConfigureView.ProcessPriority_SelectedItem == "Default")
            {
                return shellArgs;
            }

            // Shell Check
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // CMD
                case "CMD":
                    // Do not format
                    return shellArgs;

                // PowerShell
                case "PowerShell":
                    // Opening Parentheses
                    //IList<string> shellArgsMod = shellArgs.ToList();
                    //shellArgsMod = shellArgsMod.Select(x => x.Replace("$Process = Start-Process", "($Process = Start-Process")).ToList();

                    // Opening Parentheses
                    // Already Added in ProcessPriority() Method

                    // Closing
                    IEnumerable<string> closing = new List<string>()
                    {
                        "\r\n\r\n" +
                        "-PassThru).PriorityClass = [System.Diagnostics.ProcessPriorityClass]::" + ProcessPriorityLevel() + "; " +
                        "\r\n" +
                        "Wait-Process -Id $Process.id"
                    };

                    return //.Concat(shellArgs)
                           //shellArgsMod.AsEnumerable()
                           shellArgs
                           .Concat(closing)
                           .ToList();

                // Unknown
                default:
                    return shellArgs;
            }
        }


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
        /// PowerShell Call Operator - FFmpeg
        /// </summary>
        /// <remarks>
        /// Adds & symbol before Path to FFmpeg
        /// e.g. & "C:\path\to\ffmpeg\ffmpeg.exe"
        /// </remarks>
        public static String PowerShell_CallOperator_FFmpeg()
        {
            // Shell Check
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // CMD
                case "CMD":
                    return string.Empty;

                // PowerShell
                case "PowerShell":
                    // Do not use Call Operator if already using Process Priority Start-Process
                    if (VM.ConfigureView.ProcessPriority_SelectedItem != "Default")
                    {
                        return string.Empty;
                    }
                    // Use Call Opeator
                    else
                    {
                        return "& ";
                    }

                // Default
                default:
                    return string.Empty;
            }
        }


        /// <summary>
        /// PowerShell Call Operator - FFprobe
        /// </summary>
        /// <remarks>
        /// Adds & symbol before Path to FFprobe
        /// e.g. & "C:\path\to\ffmpeg\ffprobe.exe"
        /// </remarks>
        public static String PowerShell_CallOperator_FFprobe()
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
        /// Shell Logical Operator And
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
    }
}
