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

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591

namespace Axiom
{
    public class FFprobe
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Global Variables
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // FFprobe
        public static string ffprobe { get; set; } // ffprobe.exe

        // Arguments for StartInfo
        public static string argsProperties { get; set; }
        public static string argsVideoCodec { get; set; }
        public static string argsAudioCodec { get; set; }
        public static string argsVideoBitRate { get; set; }
        public static string argsAudioBitRate { get; set; }
        public static string argsSize { get; set; }
        public static string argsDuration { get; set; }
        public static string argsFrameRate { get; set; }
        public static string argsFileProperties { get; set; }

        // Results to Modify
        public static string inputFileProperties { get; set; }
        public static string inputVideoCodec { get; set; }
        public static string inputVideoBitRate { get; set; }
        public static string inputAudioCodec { get; set; }
        public static string inputAudioBitRate { get; set; }
        public static string inputSize { get; set; } //used to calculate video bitrate
        public static string inputDuration { get; set; } //used to calculate video bitrate
        public static string inputFrameRate { get; set; } //used for Frame Range

        // Single Auto
        public static string vEntryType { get; set; } // ffprobe format or stream
        public static string aEntryType { get; set; } // ffprobe format or stream

        // Batch Auto
        public static string vEntryTypeBatch { get; set; }
        public static string batchFFprobeAuto { get; set; }


        /// <summary>
        /// FFprobe Detect Metadata (Method)
        /// </summary> 
        public static void Metadata()
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
            if (!string.IsNullOrEmpty(VM.MainView.Input_Text) && 
                !string.IsNullOrEmpty(ffprobe))
            {
                // -------------------------
                //    FFprobe Video Entry Type Containers
                // -------------------------
                VideoEntryType();

                // -------------------------
                //    FFprobe Audio Entry Type Containers
                // -------------------------
                AudioEntryType();

                // -------------------------
                //    FFprobe File Info
                // -------------------------
                argsFrameRate = " -i " + "\"" + VM.MainView.Input_Text + "\"" + " -select_streams v:0 -show_entries stream=r_frame_rate -v quiet -of csv=\"p=0\"";
                argsSize = " -i " + "\"" + VM.MainView.Input_Text + "\"" + " -select_streams v:0 -show_entries format=scale -v quiet -of csv=\"p=0\"";
                argsDuration = " -i " + "\"" + VM.MainView.Input_Text + "\"" + " -select_streams v:0 -show_entries format=duration -v quiet -of csv=\"p=0\"";
                argsVideoCodec = " -i " + "\"" + VM.MainView.Input_Text + "\"" + " -select_streams v:0 -show_entries stream=codec_name -v quiet -of csv=\"p=0\"";
                argsVideoBitRate = " -i " + "\"" + VM.MainView.Input_Text + "\"" + " -select_streams v:0 -show_entries " + vEntryType + " -v quiet -of csv=\"p=0\"";
                argsAudioCodec = " -i " + "\"" + VM.MainView.Input_Text + "\"" + " -select_streams a:0 -show_entries stream=codec_name -v quiet -of csv=\"p=0\"";
                argsAudioBitRate = " -i" + " " + "\"" + VM.MainView.Input_Text + "\"" + " -select_streams a:0 -show_entries " + aEntryType + " -v quiet -of csv=\"p=0\"";

                inputFrameRate = MainWindow.RemoveLineBreaks(InputFileInfo(VM.MainView.Input_Text, VM.MainView.Batch_IsChecked, argsFrameRate));
                inputSize = MainWindow.RemoveLineBreaks(InputFileInfo(VM.MainView.Input_Text, VM.MainView.Batch_IsChecked, argsSize));
                inputDuration = MainWindow.RemoveLineBreaks(InputFileInfo(VM.MainView.Input_Text, VM.MainView.Batch_IsChecked, argsDuration));
                inputVideoCodec = MainWindow.RemoveLineBreaks(InputFileInfo(VM.MainView.Input_Text, VM.MainView.Batch_IsChecked, argsVideoCodec));
                inputVideoBitRate = MainWindow.RemoveLineBreaks(InputFileInfo(VM.MainView.Input_Text, VM.MainView.Batch_IsChecked, argsVideoBitRate));
                inputAudioCodec = MainWindow.RemoveLineBreaks(InputFileInfo(VM.MainView.Input_Text, VM.MainView.Batch_IsChecked, argsAudioCodec));
                inputAudioBitRate = MainWindow.RemoveLineBreaks(InputFileInfo(VM.MainView.Input_Text, VM.MainView.Batch_IsChecked, argsAudioBitRate));

                // Log won't write the input data unless we pass it to a new string
                string logInputFrameRate = inputFrameRate;
                string logInputSize = inputSize;
                string logInputDuration = inputDuration;
                string logInputVideoCodec = inputVideoCodec;
                string logInputVideoBitRate = inputVideoBitRate;
                string logInputAudioCodec = inputAudioCodec;
                string logInputAudioBitRate = inputAudioBitRate;

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
                    //if (!string.IsNullOrEmpty(MainWindow.inputExt) &&
                    //    MainWindow.inputExt != "extension")
                    //{
                    //    Log.logParagraph.Inlines.Add(new Run(MainWindow.inputExt) { Foreground = Log.ConsoleDefault });
                    //}

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
                    Log.logParagraph.Inlines.Add(new Bold(new Run("BitRate: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(logInputVideoBitRate) { Foreground = Log.ConsoleDefault });
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
                    Log.logParagraph.Inlines.Add(new Bold(new Run("BitRate: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(logInputAudioBitRate)) { Foreground = Log.ConsoleDefault });
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
        // Used for Auto Quality to pass Bit Rate Entry Type to FFprobe
        public static void VideoEntryType()
        {
            // -------------------------
            // Single
            // -------------------------
            if (VM.MainView.Batch_IsChecked == false)
            {
                if (Format.VideoFormats_EntryType_Stream.Any(s => s.Equals(MainWindow.inputExt, StringComparison.OrdinalIgnoreCase)))
                {
                    vEntryType = "stream=bit_rate";
                }
                else if (Format.VideoFormats_EntryType_Format.Any(s => s.Equals(MainWindow.inputExt, StringComparison.OrdinalIgnoreCase)))
                {
                    vEntryType = "format=bit_rate";
                }
                // UNLISTED Filetypes & Audio to Video
                else
                {
                    vEntryTypeBatch = "stream=bit_rate";
                }
            }

            // -------------------------
            // Batch
            // -------------------------
            else if (VM.MainView.Batch_IsChecked == true)
            {
                if (Format.VideoFormats_EntryType_Stream.Any(s => s.Equals(MainWindow.inputExt, StringComparison.OrdinalIgnoreCase)))
                {
                    vEntryTypeBatch = "stream^=bit_rate";
                }
                else if (Format.VideoFormats_EntryType_Format.Any(s => s.Equals(MainWindow.inputExt, StringComparison.OrdinalIgnoreCase)))
                {
                    vEntryTypeBatch = "format^=bit_rate";
                }
                // UNLISTED Filetypes & Audio to Video
                else
                {
                    vEntryTypeBatch = "stream^=bit_rate";
                }
            }
        }


        /// <summary>
        /// FFprobe Audio Entry Type Containers (Method)
        /// </summary>
        // Used for Auto Quality to pass Bit Rate Entry Type to FFprobe
        public static void AudioEntryType()
        {
            // -------------------------
            // Single
            // -------------------------
            if (VM.MainView.Batch_IsChecked == false)
            {
                // These file types need special instructions for FFprobe to detect
                if (string.Equals(MainWindow.inputExt, ".flac", StringComparison.CurrentCultureIgnoreCase) ||
                    string.Equals(MainWindow.inputExt, ".wav", StringComparison.CurrentCultureIgnoreCase))
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
            else if (VM.MainView.Batch_IsChecked == true)
            {
                // Choose FFprobe Entry Type based on Input file extension
                if (string.Equals(MainWindow.inputExt, ".flac", StringComparison.CurrentCultureIgnoreCase) ||
                    string.Equals(MainWindow.inputExt, ".wav", StringComparison.CurrentCultureIgnoreCase))
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
        public static String CutDuration(string input_Text,
                                         bool batch_IsChecked
                                         )
        {
            string argsDuration = " -i " + "\"" + input_Text + "\"" + " -select_streams v:0 -show_entries format=duration -v quiet -of csv=\"p=0\"";
            inputDuration = InputFileInfo(input_Text, batch_IsChecked, argsDuration);

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
        public static String InputFileInfo(string input_Text, 
                                           bool batch_IsChecked, 
                                           string arguments
                                           )
        {
            string inputMetaData = string.Empty;

            // Ignore if Batch
            // Input Empty Check
            // FFprobe.exe Null Check
            if (batch_IsChecked == false && 
                !string.IsNullOrEmpty(input_Text) && 
                !string.IsNullOrEmpty(ffprobe))
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
                    else
                    {
                        inputMetaData = string.Empty;
                    }

                    FFprobeParse.Dispose();
                }
            }

            return inputMetaData;
        }

    }
}
