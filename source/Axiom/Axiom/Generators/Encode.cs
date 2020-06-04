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
using ViewModel;
using Axiom;
using System.Collections;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Encode
{
    public partial class FFmpeg
    {
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
                                                     Sys.Shell.KeepWindow() +
                                                     // Do not use WrapWithQuotes() Method on outputDir
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
                                                     Sys.Shell.KeepWindow() +
                                                     // Do not use WrapWithQuotes() Method on outputDir
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
                Log.logParagraph.Inlines.Add(new Bold(new Run("Converting...")) { Foreground = Log.ConsoleAction });
            };
            Log.LogActions.Add(Log.WriteAction);

            // -------------------------
            // Generate Controls Script
            // -------------------------
            // Inline
            // FFmpegArgs → ScriptView
            Generate.FFmpeg.FFmpegGenerateScript();

            // -------------------------
            // Start FFmpeg
            // -------------------------
            // ScriptView → CMD/PowerShell
            FFmpegStart(MainWindow.ReplaceLineBreaksWithSpaces(VM.MainView.ScriptView_Text));
        }
    }
}
