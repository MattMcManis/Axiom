using System;
using System.Diagnostics;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591

/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017 Matt McManis
http://github.com/MattMcManis/Axiom
http://axiomui.github.io
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
    public partial class FFprobe
    {
        // --------------------------------------------------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------------------------------------------------

        // FFprobe
        public static string ffprobe; // FFprobe.exe

        // Arguments for StartInfo
        public static string argsProperties;
        public static string argsVideoCodec; 
        public static string argsAudioCodec; 
        public static string argsVideoBitrate;
        public static string argsAudioBitrate;
        public static string argsSize;
        public static string argsDuration;
        public static string argsFramerate;

        // FFprobe Results
        //public static string resultVideoCodec;
        //public static string resultVideoBitrate;
        //public static string resultAudioCodec;
        //public static string resultAudioBitrate;
        //public static string resultSize;
        //public static string resultDuration;
        //public static string resultFramerate;

        // Results to Modify
        public static string inputFileProperties;
        public static string inputVideoCodec;
        public static string inputVideoBitrate;
        public static string inputAudioCodec;
        public static string inputAudioBitrate;
        public static string inputSize; //used to calculate video bitrate
        public static string inputDuration; //used to calculate video bitrate
        public static string inputFramerate; //used for Frame Range

        // Batch
        public static string vEntryType; // ffprobe format or stream
        public static string aEntryType; // ffprobe format or stream



        /// <summary>
        /// FFprobe Video Entry Type Containers (Method)
        /// </summary>
        public static void FFprobeVideoEntryType(MainWindow mainwindow)
        {
            // Choose FFprobe Entry Type based on Input file extension
            // Note: Format sometimes encompasses the entire file, Video + Audio bitrate. 
            if (string.Equals(MainWindow.inputExt, ".wmv", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.inputExt, ".mp4", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(MainWindow.inputExt, ".ogv", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.inputExt, ".mov", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.inputExt, ".m4v", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.inputExt, ".qt", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.inputExt, ".3gp", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.inputExt, ".3g2", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.inputExt, ".flv", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.inputExt, ".swf", StringComparison.CurrentCultureIgnoreCase))
            {
                vEntryType = "stream=bit_rate";
            }
            else if (string.Equals(MainWindow.inputExt, ".avi", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.inputExt, ".mpg", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.inputExt, ".mpeg", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.inputExt, ".mod", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.inputExt, ".mkv", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.inputExt, ".asf", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.inputExt, ".vob", StringComparison.CurrentCultureIgnoreCase))
            {
                vEntryType = "format=bit_rate";
            }
            else // UNLISTED Filetypes & Audio to Video (this may cause conflict)
            {
                vEntryType = "stream=bit_rate";
            }
        }


        /// <summary>
        /// FFprobe Audio Entry Type Containers (Method)
        /// </summary>
        public static void FFprobeAudioEntryType(MainWindow mainwindow)
        {
            // These file types need special instructions for FFprobe to detect
            if (string.Equals(MainWindow.inputExt, ".flac", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.inputExt, ".wav", StringComparison.CurrentCultureIgnoreCase))
            {
                aEntryType = "format=bit_rate";
            }
            else
            {
                // All other audio types use stream=bit_rate? -maybe
                aEntryType = "stream=bit_rate";
            }
        }


        /// <summary>
        /// FFprobe Video Entry Type Containers - Batch (Method)
        /// </summary>
        public static void FFprobeVideoEntryTypeBatch(MainWindow mainwindow)
        {
            // ##### VIDEO #####
            if (string.Equals(MainWindow.batchExt, ".wmv", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.batchExt, ".mp4", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.batchExt, ".ogv", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(MainWindow.batchExt, ".mov", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.batchExt, ".m4v", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.batchExt, ".qt", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.batchExt, ".3gp", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(MainWindow.batchExt, ".3g2", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(MainWindow.batchExt, ".flv", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(MainWindow.batchExt, ".swf", StringComparison.CurrentCultureIgnoreCase))
            {
                FFprobe.vEntryType = "stream=bit_rate";
            }
            else if (string.Equals(MainWindow.batchExt, ".avi", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(MainWindow.batchExt, ".mpg", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.batchExt, ".mpeg", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(MainWindow.batchExt, ".mod", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.batchExt, ".mkv", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(MainWindow.batchExt, ".asf", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(MainWindow.batchExt, ".vob", StringComparison.CurrentCultureIgnoreCase))
            {
                FFprobe.vEntryType = "format=bit_rate";
            }
            else // UNLISTED Filetypes & Audio to Video (this may cause conflict)
            {
                FFprobe.vEntryType = "stream=bit_rate";
            }
        }


        /// <summary>
        /// FFprobe Audio Entry Type Containers - Batch (Method)
        /// </summary>
        public static void FFprobeAudioEntryTypeBatch(MainWindow mainwindow)
        {
            // Choose FFprobe Entry Type based on Input file extension
            if (string.Equals(MainWindow.batchExt, ".flac", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.batchExt, ".wav", StringComparison.CurrentCultureIgnoreCase))
            {
                FFprobe.aEntryType = "format=bit_rate";
            }
            else
            {
                // All other audio types use stream=bit_rate? -maybe
                FFprobe.aEntryType = "stream=bit_rate";
            }
        }

        /// <summary>
        /// FFprobe Input File Properties Parse (Method)
        /// </summary>
        public static void FFprobeInputFileProperties(MainWindow mainwindow)
        {
            // Start FFprobe Process
            using (Process FFprobeParse = new Process())
            {
                FFprobeParse.StartInfo.UseShellExecute = false;
                FFprobeParse.StartInfo.CreateNoWindow = true;
                FFprobeParse.StartInfo.RedirectStandardOutput = true;
                FFprobeParse.StartInfo.FileName = ffprobe;

                if (!string.IsNullOrEmpty(ffprobe)) //FFprobe.exe Null Check
                {
                    // -------------------------
                    // Get All Streams Properties
                    // -------------------------
                    argsProperties = " -i" + " " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -v quiet -print_format ini -show_format -show_streams";
                    FFprobeParse.StartInfo.Arguments = argsProperties;
                    FFprobeParse.Start();
                    //FFprobeParse.WaitForExit(); //hangs ffprobe
                    inputFileProperties = FFprobeParse.StandardOutput.ReadToEnd(); // Get Ouput Result
                }

            }
        }


        /// <summary>
        /// FFprobe Input File Info Parse (Method)
        /// </summary>
        public static void FFprobeInputFileInfo(MainWindow mainwindow)
        {
            // Start FFprobe Process
            using (Process FFprobeParse = new Process())
            {
                FFprobeParse.StartInfo.UseShellExecute = false;
                FFprobeParse.StartInfo.CreateNoWindow = true;
                FFprobeParse.StartInfo.RedirectStandardOutput = true;
                FFprobeParse.StartInfo.FileName = ffprobe;

                if (!string.IsNullOrEmpty(ffprobe)) //FFprobe.exe Null Check
                {
                    // -------------------------
                    // Frame Rate
                    // -------------------------
                    argsFramerate = " -i" + " " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -select_streams v:0 -show_entries stream=r_frame_rate -v quiet -of csv=\"p=0\"";
                    FFprobeParse.StartInfo.Arguments = argsFramerate;
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    inputFramerate = FFprobeParse.StandardOutput.ReadToEnd(); // Get Ouput Result
                    inputFramerate = inputFramerate.Replace(Environment.NewLine, ""); // remove linebreaks
                    //inputFramerate = resultFramerate.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); // remove linebreaks

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Frame Rate")) { Foreground = Log.ConsoleAction });
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Run(ffprobe + argsFramerate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                    //FFprobeParse.Close();


                    // -------------------------
                    // Size
                    // -------------------------
                    argsSize = " -i" + " " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -select_streams v:0 -show_entries format=size -v quiet -of csv=\"p=0\"";
                    FFprobeParse.StartInfo.Arguments = argsSize;
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    inputSize = FFprobeParse.StandardOutput.ReadToEnd(); // Get Ouput Result
                    inputSize = inputSize.Replace(Environment.NewLine, ""); // remove linebreaks
                    //inputSize = resultSize.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); // remove linebreaks

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("File Size")) { Foreground = Log.ConsoleAction });
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Run(ffprobe + argsSize) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                    //FFprobeParse.Close();


                    // -------------------------
                    // Duration
                    // -------------------------
                    argsDuration = " -i" + " " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -select_streams v:0 -show_entries format=duration -v quiet -of csv=\"p=0\"";
                    FFprobeParse.StartInfo.Arguments = argsDuration;
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    inputDuration = FFprobeParse.StandardOutput.ReadToEnd(); // Get Ouput Result
                    inputDuration = inputDuration.Replace(Environment.NewLine, ""); // remove linebreaks
                    //inputDuration = resultDuration.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); // remove linebreaks

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("File Duration")) { Foreground = Log.ConsoleAction });
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Run(ffprobe + argsDuration) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                    //FFprobeParse.Close();


                    // -------------------------
                    // Video Codec
                    // -------------------------
                    argsVideoCodec = " -i" + " " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -select_streams v:0 -show_entries stream=codec_name -v quiet -of csv=\"p=0\"";
                    FFprobeParse.StartInfo.Arguments = argsVideoCodec;
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    inputVideoCodec = FFprobeParse.StandardOutput.ReadToEnd(); // Get Ouput Result
                    inputVideoCodec = inputVideoCodec.Replace(Environment.NewLine, ""); // remove linebreaks
                    //inputVideoCodec = resultVideoCodec.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); // remove linebreaks

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Video Codec")) { Foreground = Log.ConsoleAction });
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Run(ffprobe + argsVideoCodec) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                    //FFprobeParse.Close();


                    // -------------------------
                    // Audio Codec
                    // -------------------------
                    argsAudioCodec = " -i" + " " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -select_streams a:0 -show_entries stream=codec_name -v quiet -of csv=\"p=0\"";
                    FFprobeParse.StartInfo.Arguments = argsAudioCodec;
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    inputAudioCodec = FFprobeParse.StandardOutput.ReadToEnd(); // Get Ouput Result
                    inputAudioCodec = inputAudioCodec.Replace(Environment.NewLine, ""); // remove linebreaks
                    //inputAudioCodec = resultAudioCodec.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); // remove linebreaks

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Audio Codec")) { Foreground = Log.ConsoleAction });
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Run(ffprobe + argsAudioCodec) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                    //FFprobeParse.Close();


                    // -------------------------
                    // Video Bitrate
                    // -------------------------
                    argsVideoBitrate = " -i" + " " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -select_streams v:0 -show_entries " + vEntryType + " -v quiet -of csv=\"p=0\"";
                    FFprobeParse.StartInfo.Arguments = argsVideoBitrate;
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    inputVideoBitrate = FFprobeParse.StandardOutput.ReadToEnd(); // Get Ouput Result
                    inputVideoBitrate = inputVideoBitrate.Replace(Environment.NewLine, ""); // remove linebreaks
                    //resultVideoBitrate = resultVideoBitrate.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); // remove linebreaks  
                    //inputVideoBitrate = resultVideoBitrate;       

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Video Bitrate")) { Foreground = Log.ConsoleAction });
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Run(ffprobe + argsVideoBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                    //FFprobeParse.Close();


                    // -------------------------
                    // Audio Bitrate
                    // -------------------------
                    argsAudioBitrate = " -i" + " " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -select_streams a:0 -show_entries " + aEntryType + " -v quiet -of csv=\"p=0\"";
                    FFprobeParse.StartInfo.Arguments = argsAudioBitrate;
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    inputAudioBitrate = FFprobeParse.StandardOutput.ReadToEnd(); // Get Ouput Result
                    inputAudioBitrate = inputAudioBitrate.Replace(Environment.NewLine, ""); // remove linebreaks
                    //inputAudioBitrate = resultAudioBitrate.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); // remove linebreaks

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Audio Bitrate")) { Foreground = Log.ConsoleAction });
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Run(ffprobe + argsAudioBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);

                    // Close Process
                    FFprobeParse.Close();
                }


                // Dispose Process
                //FFprobeParse.Dispose();
            }


        }


    }
}