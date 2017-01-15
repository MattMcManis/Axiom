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

/* ----------------------------------------------------------------------
Axiom
Copyright (C) 2017 Matt McManis
http://github.com/MattMcManis/Axiom
http://www.x.co/axiomui
axiom.interface@gmail.com

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

namespace Axiom
{
    public partial class FFmpeg
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Variables
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        // FFmepg / FFprobe
        public static string ffmpeg; // FFmpeg.exe
        public static string ffmpegArgs; // FFmpeg Arguments
        public static string cmdWindow; // Keep / Close Batch Argument

        // Batch
        public static string aBitrateLimiter; // limits the bitrate value of webm and ogg


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Process Methods
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// FFmpeg Single File Process
        /// </summary>
        public static void FFmpegSingle(MainWindow mainwindow)
        {
            if (mainwindow.tglBatch.IsChecked == false)
            {
                // Make Arugments List
                List<string> FFmpegArgsList = new List<string>()
                {
                    ffmpeg,
                    "-i",
                    "\"" + MainWindow.input + "\"",
                    "-y",
                    Video.vCodec,
                    Video.vQual,
                    Video.tune,
                    Video.speed,
                    Video.vFilter,
                    Video.fps,
                    Audio.aCodec,
                    Audio.aQual,
                    Audio.aSamplerate,
                    Audio.aBitDepth,
                    Audio.aChannel,
                    Audio.aFilter,
                    Streams.map,
                    Format.trim,
                    Video.options,
                    Video.optimize,
                    MainWindow.threads,
                    Video.pass1,
                    "\"" + MainWindow.output + "\"",
                    Video.v2pass
                };

                // Join List with Spaces, Remove Empty Strings
                ffmpegArgs = string.Join(" ", FFmpegArgsList.Where(s => !string.IsNullOrEmpty(s)));


                if (MainWindow.script == 0) // if script not clicked, start ffmpeg
                {
                    System.Diagnostics.Process.Start("CMD.exe", /* /c or /k -->*/ cmdWindow + /* needed to start cmd -->*/ "cd " + "\"" + MainWindow.currentDir + "\"" + " & " + /* start ffmpeg commands -->*/ ffmpegArgs);

                    // Another Way // Passes through FFmpeg.exe instead of CMD.exe
                    // May not work in Windows 7
                    //using (Process pFFmpeg = new Process())
                    //{
                    //    pFFmpeg.StartInfo.UseShellExecute = false;
                    //    pFFmpeg.StartInfo.CreateNoWindow = false;
                    //    pFFmpeg.StartInfo.RedirectStandardOutput = true;
                    //    //pFFmpeg.StartInfo.RedirectStandardError = true; //??
                    //    pFFmpeg.StartInfo.FileName = "ffmpeg.exe";
                    //    pFFmpeg.StartInfo.Arguments = ffmpegArgs;

                    //    if (ffmpeg != string.Empty && ffmpeg != null) //FFmpeg.exe Null Check
                    //    {
                    //        try
                    //        {
                    //            pFFmpeg.Start();
                    //            pFFmpeg.WaitForExit();
                    //            //pFFmpeg.Close();
                    //        }
                    //        catch
                    //        {

                    //        }
                    //    }
                    //}
                }
            }


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new Bold(new Run("FFmpeg Arguments")) { Foreground = Log.ConsoleTitle });
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new Run(ffmpegArgs) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);
        }


        /// <summary>
        /// FFmpeg Batch Convert
        /// </summary>
        public static void FFmpegBatch(MainWindow mainwindow)
        {
            if (mainwindow.tglBatch.IsChecked == true)
            {
                // Replace ( with ^( to avoid Windows 7 CMD Error //important!
                // This is only used in select areas
                string browseBatchInput;
                browseBatchInput = mainwindow.textBoxBrowse.Text.Replace(@"(", "^(");
                browseBatchInput = browseBatchInput.Replace(@")", "^)");


                /// <summary>
                /// FFprobe Video Entry Type Containers - Batch (Method)
                /// </summary>
                FFprobe.FFprobeVideoEntryTypeBatch(mainwindow);

                /// <summary>
                /// FFprobe Video Entry Type Containers - Batch (Method)
                /// </summary>
                FFprobe.FFprobeAudioEntryTypeBatch(mainwindow);


                // -------------------------
                // Limit Bitrates
                // -------------------------
                // Limit Vorbis bitrate to 500k through cmd.exe
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
                {
                    aBitrateLimiter = "(if %A gtr 500000 (set aBitrate=500000) else (echo Bitrate within Vorbis Limit of 500k)) & for /F %A in ('echo %aBitrate%') do (echo %A) &";
                }
                // Limit Opus bitrate to 510k through cmd.exe
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                {
                    aBitrateLimiter = "(if %A gtr 510000 (set aBitrate=510000) else (echo Bitrate within Opus Limit of 510k)) & for /F %A in ('echo %aBitrate%') do (echo %A) &";
                }
                // Limit AAC bitrate to 400k through cmd.exe
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
                {
                    aBitrateLimiter = "(if %A gtr 400000 (set aBitrate=400000) else (echo Bitrate within AAC Limit of 400k)) & for /F %A in ('echo %aBitrate%') do (echo %A) &";
                }
                // Limit AC3 bitrate to 640k through cmd.exe
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3")
                {
                    aBitrateLimiter = "(if %A gtr 640000 (set aBitrate=640000) else (echo Bitrate within AC3 Limit of 640k)) & for /F %A in ('echo %aBitrate%') do (echo %A) &";
                }
                // Limit LAME bitrate to 320k through cmd.exe
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
                {
                    aBitrateLimiter = "(if %A gtr 320000 (set aBitrate=320000) else (echo Bitrate within LAME Limit of 320k)) & for /F %A in ('echo %aBitrate%') do (echo %A) &";
                }

                // -------------------------
                // Batch (Auto)
                // -------------------------
                // Log Console Message /////////
                if (mainwindow.tglBatch.IsChecked == true)
                {
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("Batch: ")) { Foreground = Log.ConsoleDefault });
                        Log.paragraph.Inlines.Add(new Run(Convert.ToString(mainwindow.tglBatch.IsChecked)) { Foreground = Log.ConsoleDefault });
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("Generating Batch Script...")) { Foreground = Log.ConsoleTitle });
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("Running Batch Convert...")) { Foreground = Log.ConsoleAction });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }


                // -------------------------
                //  Audio Convert  (Media to Audio - Auto) Batch
                // -------------------------
                if (mainwindow.tglBatch.IsChecked == true && (string)mainwindow.cboMediaType.SelectedItem == "Audio" && (string)mainwindow.cboAudio.SelectedItem == "Auto")
                {
                    // Make List
                    List<string> FFmpegArgsList = new List<string>()
                    {
                        "cd",
                        "\"" + MainWindow.input + "\"",
                        "& for %f in", "(*" + MainWindow.batchExt + ")",
                        "do", FFprobe.ffprobe, "-i",
                        "\"" + browseBatchInput + "%~f" + "\"",
                        "-select_streams a:0 -show_entries " + FFprobe.aEntryType + " -v quiet -of csv=\"p=0\" & for /f \"tokens=*\" %A in (" + "\"" + FFprobe.ffprobe + " -i " + "\"" + browseBatchInput + "%~f" + "\"" + " -select_streams a:0 -show_entries " + FFprobe.aEntryType + " -v quiet -of csv=p=0\") do (SET z=%A) & (%A > tmp_aBitrate) & SET /p aBitrate= < tmp_aBitrate & del tmp_aBitrate & for /F %A in (\'echo %aBitrate%\') do echo %A &",
                        aBitrateLimiter,
                        ffmpeg,
                        "-i",
                        "\"" + MainWindow.input + "%~f" + "\"",
                        "-y",
                        Video.vCodec,
                        Audio.aCodec,
                        "-b:a %A",
                        Audio.aSamplerate,
                        Audio.aBitDepth,
                        Audio.aChannel,
                        Audio.aFilter,
                        Streams.map,
                        Format.trim,
                        Video.options,
                        Video.optimize,
                        MainWindow.threads,
                        Video.pass1,
                        "\"" + MainWindow.output + "\""
                    };
                    
                    // Join List with Spaces, Remove Empty Strings
                    ffmpegArgs = string.Join(" ", FFmpegArgsList.Where(s => !string.IsNullOrEmpty(s)));

                    if (MainWindow.script == 0) // check if script button enabled
                    {
                        System.Diagnostics.Process.Start("CMD.exe", cmdWindow + ffmpegArgs);
                    }
                }

                // -------------------------
                // Video Convert  (Media to Video - Auto) Batch 
                // -------------------------
                else if (mainwindow.tglBatch.IsChecked == true && (string)mainwindow.cboMediaType.SelectedItem == "Video" && (string)mainwindow.cboVideo.SelectedItem == "Auto" | (string)mainwindow.cboAudio.SelectedItem == "Auto")
                {
                    // Make List
                    List<string> FFmpegArgsList = new List<string>()
                    {
                        "cd",
                        "\"" + MainWindow.input + "\"",
                        "& for %f in",
                        "(*" + MainWindow.batchExt + ")",
                        "do",
                        FFprobe.ffprobe,
                        "-i", "\"" + browseBatchInput + "%~f" + "\"",
                        "-select_streams v:0 -show_entries " + FFprobe.vEntryType + " -v quiet -of csv=\"p=0\" & for /f \"tokens=*\" %S in (\"" + FFprobe.ffprobe + " -i " + "\"" + browseBatchInput + "%~f" + "\"" + " -select_streams v:0 -show_entries format=size -v quiet -of csv=p=0\") do (echo ) & (%S > tmp_size) & SET /p size= < tmp_size & del tmp_size & for /F %S in ('echo %size%') do (echo %S) & for /f \"tokens=*\" %D in (\"" + FFprobe.ffprobe + " -i " + "\"" + browseBatchInput + "%~f" + "\"" + " -select_streams v:0 -show_entries format=duration -v quiet -of csv=p=0\") do (echo ) & (%D > tmp_duration) & SET /p duration= < tmp_duration & del tmp_duration & for /f \"tokens=1 delims=.\" %R in ('echo %duration%') do set duration=%R & for /F %D in ('echo %duration%') do (echo %D) & for /f \"tokens=*\" %V in (" + "\"" + FFprobe.ffprobe + " -i " + "\"" + browseBatchInput + "%~f" + "\"" + " -select_streams v:0 -show_entries " + FFprobe.vEntryType + " -v quiet -of csv=p=0\") do (echo ) & (%V > tmp_vBitrate) & SET /p vBitrate= < tmp_vBitrate & del tmp_vBitrate & for /F %V in ('echo %vBitrate%') do (echo %V) & (if %V EQU N/A (set /a vBitrate=%S*8/1000/%D*1000) else (echo Video Bitrate Detected)) & for /F %V in ('echo %vBitrate%') do (echo %V) & " + FFprobe.ffprobe + " -i " + "\"" + browseBatchInput + "%~f" + "\"",
                        "-select_streams a:0 -show_entries " + FFprobe.aEntryType + " -v quiet -of csv=\"p=0\" & for /f \"tokens=*\" %A in (" + "\"" + FFprobe.ffprobe + " -i " + "\"" + browseBatchInput + "%~f" + "\"" + " -select_streams a:0 -show_entries " + FFprobe.aEntryType + " -v quiet -of csv=p=0\") do (echo ) & (%A > tmp_aBitrate) & SET /p aBitrate= < tmp_aBitrate & del tmp_aBitrate & for /F %A in ('echo %aBitrate%') do echo %A & (if %A EQU N/A (set aBitrate=320000)) & for /F %A in ('echo %aBitrate%') do echo %A &",
                        aBitrateLimiter,
                        ffmpeg,
                        "-i ",
                        "\"" + MainWindow.input + "%~f" + "\"",
                        "-y",
                        Video.vCodec,
                        Video.cmdBatch_vQual,
                        Video.tune,
                        Video.speed,
                        Video.vFilter,
                        Video.fps,
                        Audio.aCodec,
                        Audio.cmdBatch_aQual,
                        Audio.aSamplerate,
                        Audio.aBitDepth,
                        Audio.aChannel,
                        Audio.aFilter,
                        Streams.map,
                        Format.trim,
                        Video.options,
                        Video.optimize,
                        MainWindow.threads,
                        Video.pass1,
                        "\"" + MainWindow.output + "\"" + Video.v2passBatch
                    };

                    // Join List with Spaces, Remove Empty Strings
                    ffmpegArgs = string.Join(" ", FFmpegArgsList.Where(s => !string.IsNullOrEmpty(s)));

                    if (MainWindow.script == 0) // check if script button enabled
                    {
                        System.Diagnostics.Process.Start("CMD.exe", cmdWindow + ffmpegArgs);
                    }
                }

                // -------------------------
                // Batch Media  (User selected options)
                // -------------------------
                else if (mainwindow.tglBatch.IsChecked == true && (string)mainwindow.cboVideo.SelectedItem != "Auto" && (string)mainwindow.cboAudio.SelectedItem != "Auto")
                {
                    // Make List
                    List<string> FFmpegArgsList = new List<string>()
                    {
                        "cd",
                        "\"" + MainWindow.input + "\"",
                        "& for %f in",
                        "(*" + MainWindow.batchExt + ")",
                        "do",
                        ffmpeg,
                        "-i",
                        "\"" + MainWindow.input + "%~f" + "\"",
                        "-y",
                        Video.vCodec,
                        Video.vQual,
                        Video.tune,
                        Video.speed,
                        Video.vFilter,
                        Video.fps,
                        Audio.aCodec,
                        Audio.aQual,
                        Audio.aSamplerate,
                        Audio.aBitDepth,
                        Audio.aChannel,
                        Audio.aFilter,
                        Streams.map,
                        Format.trim,
                        Video.options,
                        Video.optimize,
                        MainWindow.threads,
                        Video.pass1,
                        "\"" + MainWindow.output + "\"",
                        Video.v2pass
                    };

                    // Join List with Spaces, Remove Empty Strings
                    ffmpegArgs = string.Join(" ", FFmpegArgsList.Where(s => !string.IsNullOrEmpty(s)));

                    if (MainWindow.script == 0) // check if script button enabled
                    {
                        System.Diagnostics.Process.Start("CMD.exe", cmdWindow + ffmpegArgs);
                    }
                }
            }
        }


        /// <summary>
        /// FFmpeg Generate Script
        /// </summary>
        public static void FFmpegScript(MainWindow mainwindow)
        {
            if (MainWindow.script == 1)
            {
                //ffmpegArgs = Regex.Replace(ffmpegArgs, @"\s+", " "); /* remove extra white spaces*/

                // Open ScriptView Window
                ScriptView scriptview = new ScriptView(ffmpegArgs);
                scriptview.Left = mainwindow.Left + 90;
                scriptview.Top = mainwindow.Top + 98;
                scriptview.Owner = Window.GetWindow(mainwindow);
                scriptview.Show();
            }
        }


    }
}
