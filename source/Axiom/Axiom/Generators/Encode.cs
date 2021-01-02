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

namespace Encode
{
    public partial class FFmpeg
    {
        /// <summary>
        /// FFmpeg Start
        /// </summary>
        public static async Task<int> FFmpegStartAsync(string args)
        {
            int count = 0;
            await Task.Run(() =>
            {
                FFmpegStart(args);
            });

            return count;
        }
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
                    System.Diagnostics.Process.Start(
                        "cmd.exe",
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
                    System.Diagnostics.Process.Start(
                        "powershell.exe",
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
        //public static async Task<int> FFmpegConvertAsync()
        //{
        //    int count = 0;
        //    await Task.Run(() =>
        //    {
        //        FFmpegConvert();
        //    });

        //    return count;
        //}
        //public async static void FFmpegConvert()
        //{
        //    Log.WriteAction = () =>
        //    {
        //        Log.logParagraph.Inlines.Add(new LineBreak());
        //        Log.logParagraph.Inlines.Add(new LineBreak());
        //        Log.logParagraph.Inlines.Add(new Bold(new Run("Converting...")) { Foreground = Log.ConsoleAction });
        //    };
        //    Log.LogActions.Add(Log.WriteAction);

        //    // -------------------------
        //    // Generate Controls Script
        //    // -------------------------
        //    // Inline
        //    // FFmpegArgs → ScriptView
        //    //Generate.FFmpeg.FFmpegGenerateScript();
        //    Task<int> script = Generate.FFmpeg.FFmpegGenerateScriptAsync();
        //    int count1 = await script;

        //    // -------------------------
        //    // Start FFmpeg
        //    // -------------------------
        //    // ScriptView → CMD/PowerShell
        //    //FFmpegStart(MainWindow.ReplaceLineBreaksWithSpaces(VM.MainView.ScriptView_Text));
        //    //Task.Run(() => FFmpegStart(MainWindow.ReplaceLineBreaksWithSpaces(VM.MainView.ScriptView_Text)));
        //    Task<int> start = FFmpegStartAsync(MainWindow.ReplaceLineBreaksWithSpaces(VM.MainView.ScriptView_Text));
        //    int count2 = await start;
        //}

    }
}
