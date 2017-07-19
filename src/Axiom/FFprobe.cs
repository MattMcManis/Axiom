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

        // Results to Modify
        public static string inputFileProperties;
        public static string inputVideoCodec;
        public static string inputVideoBitrate;
        public static string inputAudioCodec;
        public static string inputAudioBitrate;
        public static string inputSize; //used to calculate video bitrate
        public static string inputDuration; //used to calculate video bitrate
        public static string inputFramerate; //used for Frame Range

        // Single Auto
        public static string vEntryType; // ffprobe format or stream
        public static string aEntryType; // ffprobe format or stream

        // Batch Auto
        public static string vEntryTypeBatch;
        public static string batchFFprobeAuto;



        /// <summary>
        ///    FFprobe Detect Metadata (Method)
        /// </summary> 
        public static void Metadata(MainWindow mainwindow)
        {
            // --------------------------------------------------------------------
            // Section: FFprobe
            // --------------------------------------------------------------------

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("FFprobe")) { Foreground = Log.ConsoleTitle });
            };
            Log.LogActions.Add(Log.WriteAction);


            // Only Run FFprobe if Input File is Not Null
            // Strange FFprobe Class problem - methods halting after FFprobeInputFileInfo() 
            // unless Null Check is put here instead of inside the Class.
            if (!string.IsNullOrWhiteSpace(mainwindow.textBoxBrowse.Text)
                && !string.IsNullOrEmpty(MainWindow.inputDir)
                && !string.IsNullOrEmpty(FFprobe.ffprobe))
            {
                // -------------------------
                //    FFprobe Video Entry Type Containers
                // -------------------------
                FFprobe.FFprobeVideoEntryType(mainwindow);


                // -------------------------
                //    FFprobe Audio Entry Type Containers
                // -------------------------
                FFprobe.FFprobeAudioEntryType(mainwindow);


                // -------------------------
                //    FFprobe File Info
                // -------------------------
                FFprobe.FFprobeInputFileInfo(mainwindow);


                // -------------------------
                //    Video Bitrate Calculator
                // -------------------------
                Video.VideoBitrateCalculator(mainwindow);


                // -------------------------
                //    Audio Bitrate Calculator
                // -------------------------
                Audio.AudioBitrateCalculator(mainwindow);
            }


            // --------------------------------------------------------------------
            // Section: Input
            // --------------------------------------------------------------------

            // Log Console Message /////////
            // Only Check FFprobe Input if Input File is Not Null
            if (!string.IsNullOrWhiteSpace(mainwindow.textBoxBrowse.Text) 
                && !string.IsNullOrEmpty(MainWindow.inputDir) 
                && !string.IsNullOrEmpty(FFprobe.ffprobe))
            {
                Log.WriteAction = () =>
                {
                    // Log Console Message /////////
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Input File Details")) { Foreground = Log.ConsoleTitle });

                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Directory: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(MainWindow.inputDir) { Foreground = Log.ConsoleDefault });

                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("General")) { Foreground = Log.ConsoleAction });
                    Log.logParagraph.Inlines.Add(new LineBreak());

                    Log.logParagraph.Inlines.Add(new Bold(new Run("Container: ")) { Foreground = Log.ConsoleDefault });
                    // single file
                    if (!string.IsNullOrEmpty(MainWindow.inputExt))
                    {
                        Log.logParagraph.Inlines.Add(new Run(MainWindow.inputExt) { Foreground = Log.ConsoleDefault });
                    }
                    // batch
                    if (!string.IsNullOrEmpty(MainWindow.batchExt) && MainWindow.batchExt != "extension")
                    {
                        Log.logParagraph.Inlines.Add(new Run(MainWindow.batchExt) { Foreground = Log.ConsoleDefault });
                    }

                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Size: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(FFprobe.inputSize) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Duration: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(FFprobe.inputDuration) { Foreground = Log.ConsoleDefault });

                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Video")) { Foreground = Log.ConsoleAction });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Codec: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(FFprobe.inputVideoCodec) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(FFprobe.inputVideoBitrate) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("FPS: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(FFprobe.inputFramerate) { Foreground = Log.ConsoleDefault });

                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Audio")) { Foreground = Log.ConsoleAction });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Codec: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(FFprobe.inputAudioCodec) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(FFprobe.inputAudioBitrate)) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }
            else
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Input File Not Found")) { Foreground = Log.ConsoleWarning });
                };
                Log.LogActions.Add(Log.WriteAction);
            }


            // --------------------------------------------------------------------
            // Section: Output
            // --------------------------------------------------------------------
            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Output File Settings")) { Foreground = Log.ConsoleTitle });
            };
            Log.LogActions.Add(Log.WriteAction);

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Directory: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(MainWindow.outputDir) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            // --------------------------------------------------
            // Category: General
            // --------------------------------------------------
            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("General")) { Foreground = Log.ConsoleAction });
            };
            Log.LogActions.Add(Log.WriteAction);


            // -------------------------
            //    Format
            // -------------------------
            // OutputFormat() is not called because it is instead used in Controls
            // Use a Message for Log Console

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Format: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(MainWindow.outputExt) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);
        }


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
            // -------------------------
            // VIDEO
            // -------------------------
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
                FFprobe.vEntryTypeBatch = "stream^=bit_rate";
            }
            else if (string.Equals(MainWindow.batchExt, ".avi", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(MainWindow.batchExt, ".mpg", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.batchExt, ".mpeg", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(MainWindow.batchExt, ".mod", StringComparison.CurrentCultureIgnoreCase) 
                || string.Equals(MainWindow.batchExt, ".mkv", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(MainWindow.batchExt, ".asf", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(MainWindow.batchExt, ".vob", StringComparison.CurrentCultureIgnoreCase))
            {
                FFprobe.vEntryTypeBatch = "format^=bit_rate";
            }
            // UNLISTED Filetypes & Audio to Video (this may cause conflict)
            else
            {
                FFprobe.vEntryTypeBatch = "stream^=bit_rate";
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
                    // Get Ouput Result
                    inputFileProperties = FFprobeParse.StandardOutput.ReadToEnd(); 
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
                FFprobeParse.StartInfo.FileName = FFprobe.ffprobe;

                if (!string.IsNullOrEmpty(ffprobe)) //FFprobe.exe Null Check
                {
                    // -------------------------
                    // Frame Rate
                    // -------------------------
                    argsFramerate = " -i" + " " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -select_streams v:0 -show_entries stream=r_frame_rate -v quiet -of csv=\"p=0\"";
                    FFprobeParse.StartInfo.Arguments = argsFramerate;
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    // Get Ouput Result
                    inputFramerate = FFprobeParse.StandardOutput.ReadToEnd();
                    // Remove linebreaks
                    inputFramerate = inputFramerate.Replace(Environment.NewLine, ""); 
                    // Remove any white space from end of string
                    inputFramerate = inputFramerate.Trim();
                    inputFramerate = inputFramerate.TrimEnd();

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
                    // Get Ouput Result
                    inputSize = FFprobeParse.StandardOutput.ReadToEnd();
                    // Remove linebreaks
                    inputSize = inputSize.Replace(Environment.NewLine, ""); 
                    // Remove any white space from end of string
                    inputSize = inputSize.Trim();
                    inputSize = inputSize.TrimEnd();

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
                    // Get Ouput Result
                    inputDuration = FFprobeParse.StandardOutput.ReadToEnd();
                    // Remove linebreaks
                    inputDuration = inputDuration.Replace(Environment.NewLine, ""); 
                    // Remove any white space from end of string
                    inputDuration = inputDuration.Trim();
                    inputDuration = inputDuration.TrimEnd();

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
                    // Get Ouput Result
                    inputVideoCodec = FFprobeParse.StandardOutput.ReadToEnd();
                    // Remove linebreaks
                    inputVideoCodec = inputVideoCodec.Replace(Environment.NewLine, ""); 
                    // Remove any white space from end of string
                    inputVideoCodec = inputVideoCodec.Trim();
                    inputVideoCodec = inputVideoCodec.TrimEnd();

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
                    // Get Ouput Result
                    inputAudioCodec = FFprobeParse.StandardOutput.ReadToEnd();
                    // Remove linebreaks
                    inputAudioCodec = inputAudioCodec.Replace(Environment.NewLine, ""); 
                    // Remove any white space from end of string
                    inputAudioCodec = inputAudioCodec.Trim();
                    inputAudioCodec = inputAudioCodec.TrimEnd();

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
                    argsVideoBitrate = " -i " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -select_streams v:0 -show_entries " + vEntryType + " -v quiet -of csv=\"p=0\"";
                    FFprobeParse.StartInfo.Arguments = argsVideoBitrate;
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    // Get Ouput Result
                    inputVideoBitrate = FFprobeParse.StandardOutput.ReadToEnd();
                    // Remove linebreaks  
                    inputVideoBitrate = inputVideoBitrate.Replace(Environment.NewLine, "");
                    // Remove any white space from end of string
                    inputVideoBitrate = inputVideoBitrate.Trim();
                    inputVideoBitrate = inputVideoBitrate.TrimEnd();


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
                    // Get Ouput Result
                    inputAudioBitrate = FFprobeParse.StandardOutput.ReadToEnd();
                    // Remove linebreaks
                    inputAudioBitrate = inputAudioBitrate.Replace(Environment.NewLine, ""); 
                    // Remove any white space from end of string
                    inputAudioBitrate = inputAudioBitrate.Trim();
                    inputAudioBitrate = inputAudioBitrate.TrimEnd();

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