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
using System.Diagnostics;
using System.Linq;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591

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
        public static string argsFrameRate;
        public static string argsFileProperties;

        // Results to Modify
        public static string inputFileProperties;
        public static string inputVideoCodec;
        public static string inputVideoBitrate;
        public static string inputAudioCodec;
        public static string inputAudioBitrate;
        public static string inputSize; //used to calculate video bitrate
        public static string inputDuration; //used to calculate video bitrate
        public static string inputFrameRate; //used for Frame Range

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
            // Strange FFprobe Class problem - methods halting after InputFileInfo() 
            // unless Null Check is put here instead of inside the Class.
            if (!string.IsNullOrWhiteSpace(mainwindow.tbxInput.Text)
                //&& !string.IsNullOrEmpty(MainWindow.inputDir)
                && !string.IsNullOrEmpty(FFprobe.ffprobe))
            {
                // -------------------------
                //    FFprobe Video Entry Type Containers
                // -------------------------
                FFprobe.VideoEntryType(mainwindow);

                // -------------------------
                //    FFprobe Audio Entry Type Containers
                // -------------------------
                FFprobe.AudioEntryType(mainwindow);

                // -------------------------
                //    FFprobe File Info
                // -------------------------
                argsFrameRate = " -i " + "\"" + mainwindow.tbxInput.Text + "\"" + " -select_streams v:0 -show_entries stream=r_frame_rate -v quiet -of csv=\"p=0\"";
                argsSize = " -i " + "\"" + mainwindow.tbxInput.Text + "\"" + " -select_streams v:0 -show_entries format=size -v quiet -of csv=\"p=0\"";
                argsDuration = " -i " + "\"" + mainwindow.tbxInput.Text + "\"" + " -select_streams v:0 -show_entries format=duration -v quiet -of csv=\"p=0\"";
                argsVideoCodec = " -i " + "\"" + mainwindow.tbxInput.Text + "\"" + " -select_streams v:0 -show_entries stream=codec_name -v quiet -of csv=\"p=0\"";
                argsVideoBitrate = " -i " + "\"" + mainwindow.tbxInput.Text + "\"" + " -select_streams v:0 -show_entries " + vEntryType + " -v quiet -of csv=\"p=0\"";
                argsAudioCodec = " -i " + "\"" + mainwindow.tbxInput.Text + "\"" + " -select_streams a:0 -show_entries stream=codec_name -v quiet -of csv=\"p=0\"";
                argsAudioBitrate = " -i" + " " + "\"" + mainwindow.tbxInput.Text + "\"" + " -select_streams a:0 -show_entries " + aEntryType + " -v quiet -of csv=\"p=0\"";

                inputFrameRate = MainWindow.RemoveLineBreaks(InputFileInfo(mainwindow, argsFrameRate));
                inputSize = MainWindow.RemoveLineBreaks(InputFileInfo(mainwindow, argsSize));
                inputDuration = MainWindow.RemoveLineBreaks(InputFileInfo(mainwindow, argsDuration));
                inputVideoCodec = MainWindow.RemoveLineBreaks(InputFileInfo(mainwindow, argsVideoCodec));
                inputVideoBitrate = MainWindow.RemoveLineBreaks(InputFileInfo(mainwindow, argsVideoBitrate));
                inputAudioCodec = MainWindow.RemoveLineBreaks(InputFileInfo(mainwindow, argsAudioCodec));
                inputAudioBitrate = MainWindow.RemoveLineBreaks(InputFileInfo(mainwindow, argsAudioBitrate));

                // Log won't write the input data unless we pass it to a new string
                string logInputFrameRate = inputFrameRate;
                string logInputSize = inputSize;
                string logInputDuration = inputDuration;
                string logInputVideoCodec = inputVideoCodec;
                string logInputVideoBitrate = inputVideoBitrate;
                string logInputAudioCodec = inputAudioCodec;
                string logInputAudioBitrate = inputAudioBitrate;

                // --------------------------------------------------------------------
                // Section: Input
                // --------------------------------------------------------------------
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
                    // Single File
                    if (!string.IsNullOrEmpty(MainWindow.inputExt))
                    {
                        Log.logParagraph.Inlines.Add(new Run(MainWindow.inputExt) { Foreground = Log.ConsoleDefault });
                    }
                    // Batch
                    if (!string.IsNullOrEmpty(MainWindow.batchExt) && MainWindow.batchExt != "extension")
                    {
                        Log.logParagraph.Inlines.Add(new Run(MainWindow.batchExt) { Foreground = Log.ConsoleDefault });
                    }

                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Size: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(logInputSize) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Duration: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(logInputDuration) { Foreground = Log.ConsoleDefault });

                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Video")) { Foreground = Log.ConsoleAction });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Codec: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(logInputVideoCodec) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(logInputVideoBitrate) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("FPS: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(logInputFrameRate) { Foreground = Log.ConsoleDefault });

                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Audio")) { Foreground = Log.ConsoleAction });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Codec: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(logInputAudioCodec) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(logInputAudioBitrate)) { Foreground = Log.ConsoleDefault });
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
        // Used for Auto Quality to pass Bit-rate Entry Type to FFprobe
        public static void VideoEntryType(MainWindow mainwindow)
        {
            // -------------------------
            // Single
            // -------------------------
            if (mainwindow.tglBatch.IsChecked == false)
            {
                if (Format.VideoFormats_EntryType_Stream.Any(s => s.Equals(MainWindow.inputExt, StringComparison.OrdinalIgnoreCase)))
                {
                    vEntryType = "stream=bit_rate";
                }
                else if (Format.VideoFormats_EntryType_Format.Any(s => s.Equals(MainWindow.inputExt, StringComparison.OrdinalIgnoreCase)))
                {
                    vEntryType = "format=bit_rate";
                }
                else // UNLISTED Filetypes & Audio to Video
                {
                    vEntryTypeBatch = "stream=bit_rate";
                }
            }

            // -------------------------
            // Batch
            // -------------------------
            else if (mainwindow.tglBatch.IsChecked == true)
            {
                if (Format.VideoFormats_EntryType_Stream.Any(s => s.Equals(MainWindow.batchExt, StringComparison.OrdinalIgnoreCase)))
                {
                    vEntryType = "stream^=bit_rate";
                }
                else if (Format.VideoFormats_EntryType_Format.Any(s => s.Equals(MainWindow.batchExt, StringComparison.OrdinalIgnoreCase)))
                {
                    vEntryType = "format^=bit_rate";
                }
                else // UNLISTED Filetypes & Audio to Video
                {
                    vEntryTypeBatch = "stream^=bit_rate";
                }
            }          
        }


        /// <summary>
        /// FFprobe Audio Entry Type Containers (Method)
        /// </summary>
        // Used for Auto Quality to pass Bit-rate Entry Type to FFprobe
        public static void AudioEntryType(MainWindow mainwindow)
        {
            // -------------------------
            // Single
            // -------------------------
            if (mainwindow.tglBatch.IsChecked == false)
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

            // -------------------------
            // Batch
            // -------------------------
            else if (mainwindow.tglBatch.IsChecked == true)
            {
                // Choose FFprobe Entry Type based on Input file extension
                if (string.Equals(MainWindow.batchExt, ".flac", StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(MainWindow.batchExt, ".wav", StringComparison.CurrentCultureIgnoreCase))
                {
                    aEntryType = "format^=bit_rate";
                }
                else
                {
                    // All other audio types use stream=bit_rate? -maybe
                    aEntryType = "stream^=bit_rate";
                }
            }
        }


        /// <summary>
        /// FFprobe Cut Duration
        /// </summary>
        public static String CutDuration(MainWindow mainwindow)
        {
            string argsDuration = " -i " + "\"" + mainwindow.tbxInput.Text + "\"" + " -select_streams v:0 -show_entries format=duration -v quiet -of csv=\"p=0\"";
            inputDuration = InputFileInfo(mainwindow, argsDuration);

            // Format Time for FFmpeg 00:00:00.000
            //
            if (!string.IsNullOrEmpty(inputDuration)) //null check
            {
                TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(inputDuration));

                inputDuration = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                                t.Hours,
                                t.Minutes,
                                t.Seconds,
                                t.Milliseconds);
            }

            return inputDuration;
        }

        

        /// <summary>
        /// FFprobe Input File Info Parse (Method)
        /// </summary>
        public static String InputFileInfo(MainWindow mainwindow, string arguments)
        {
            string inputMetaData = string.Empty;

            // Ignore if Batch
            // Input Empty Check
            // FFprobe.exe Null Check
            if (mainwindow.tglBatch.IsChecked == false
                && !string.IsNullOrWhiteSpace(mainwindow.tbxInput.Text)
                && !string.IsNullOrEmpty(FFprobe.ffprobe))
            {
                // Start FFprobe Process
                using (Process FFprobeParse = new Process())
                {
                    FFprobeParse.StartInfo.UseShellExecute = false;
                    FFprobeParse.StartInfo.CreateNoWindow = true;
                    FFprobeParse.StartInfo.RedirectStandardOutput = true;
                    FFprobeParse.StartInfo.FileName = FFprobe.ffprobe;

                    // -------------------------
                    // Frame Rate
                    // -------------------------
                    FFprobeParse.StartInfo.Arguments = arguments;
                    FFprobeParse.Start();
                    //FFprobeParse.WaitForExit();
                    // Get Ouput Result
                    inputMetaData = FFprobeParse.StandardOutput.ReadToEnd();

                    if (!string.IsNullOrEmpty(inputMetaData))
                    {
                        inputMetaData = inputMetaData.Trim();
                        inputMetaData = inputMetaData.TrimEnd();
                    }
                }
            }

            return inputMetaData;
        }

    }
}