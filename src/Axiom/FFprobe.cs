using System;
using System.Diagnostics;
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

        public static string ffprobeVideoCodecResult;
        public static string ffprobeVideoBitrateResult;
        public static string ffprobeAudioCodecResult;
        public static string ffprobeAudioBitrateResult;
        public static string ffprobeSizeResult;
        public static string ffprobeDurationResult;
        public static string ffprobeFramerateResult;

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
            if (string.Equals(MainWindow.inputExt, ".wmv", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".mp4", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".ogv", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".mov", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".m4v", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".qt", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".3gp", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".3g2", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".flv", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".swf", StringComparison.CurrentCultureIgnoreCase))
            {
                vEntryType = "stream=bit_rate";
            }
            else if (string.Equals(MainWindow.inputExt, ".avi", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".mpg", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".mpeg", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".mod", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".mkv", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".asf", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".vob", StringComparison.CurrentCultureIgnoreCase))
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
            if (string.Equals(MainWindow.inputExt, ".flac", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, ".wav", StringComparison.CurrentCultureIgnoreCase))
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
            if (string.Equals(MainWindow.batchExt, ".wmv", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".mp4", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".ogv", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".mov", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".m4v", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".qt", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".3gp", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".3g2", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".flv", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".swf", StringComparison.CurrentCultureIgnoreCase))
            {
                FFprobe.vEntryType = "stream=bit_rate";
            }
            else if (string.Equals(MainWindow.batchExt, ".avi", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".mpg", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".mpeg", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".mod", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".mkv", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".asf", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".vob", StringComparison.CurrentCultureIgnoreCase))
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
            if (string.Equals(MainWindow.batchExt, ".flac", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.batchExt, ".wav", StringComparison.CurrentCultureIgnoreCase))
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
                    FFprobeParse.StartInfo.Arguments = " -i" + " " + "\"" + MainWindow.input + "\"" + " -select_streams v:0 -show_entries stream=r_frame_rate -v quiet -of csv=\"p=0\""; // Arguments
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    ffprobeFramerateResult = FFprobeParse.StandardOutput.ReadToEnd(); // Get Ouput Result
                    inputFramerate = ffprobeFramerateResult.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); // remove linebreaks

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("Frame Rate")) { Foreground = Log.ConsoleAction });
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Run(ffprobe + FFprobeParse.StartInfo.Arguments) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                    //FFprobeParse.Close();


                    // -------------------------
                    // Size
                    // -------------------------
                    FFprobeParse.StartInfo.Arguments = " -i" + " " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -select_streams v:0 -show_entries format=size -v quiet -of csv=\"p=0\"";
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    ffprobeSizeResult = FFprobeParse.StandardOutput.ReadToEnd(); // Get Ouput Result
                    inputSize = ffprobeSizeResult.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); // remove linebreaks

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("File Size")) { Foreground = Log.ConsoleAction });
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Run(ffprobe + FFprobeParse.StartInfo.Arguments) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                    //FFprobeParse.Close();


                    // -------------------------
                    // Duration
                    // -------------------------
                    FFprobeParse.StartInfo.Arguments = " -i" + " " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -select_streams v:0 -show_entries format=duration -v quiet -of csv=\"p=0\"";
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    ffprobeDurationResult = FFprobeParse.StandardOutput.ReadToEnd(); // Get Ouput Result
                    inputDuration = ffprobeDurationResult.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); // remove linebreaks


                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("File Duration")) { Foreground = Log.ConsoleAction });
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Run(ffprobe + FFprobeParse.StartInfo.Arguments) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                    //FFprobeParse.Close();


                    // -------------------------
                    // Video Codec
                    // -------------------------
                    FFprobeParse.StartInfo.Arguments = " -i" + " " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -select_streams v:0 -show_entries stream=codec_name -v quiet -of csv=\"p=0\"";
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    ffprobeVideoCodecResult = FFprobeParse.StandardOutput.ReadToEnd(); // Get Ouput Result
                    inputVideoCodec = ffprobeVideoCodecResult.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); // remove linebreaks

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("Video Codec")) { Foreground = Log.ConsoleAction });
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Run(ffprobe + FFprobeParse.StartInfo.Arguments) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                    //FFprobeParse.Close();


                    // -------------------------
                    // Audio Codec
                    // -------------------------
                    FFprobeParse.StartInfo.Arguments = " -i" + " " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -select_streams a:0 -show_entries stream=codec_name -v quiet -of csv=\"p=0\"";
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    ffprobeAudioCodecResult = FFprobeParse.StandardOutput.ReadToEnd(); // Get Ouput Result
                    inputAudioCodec = ffprobeAudioCodecResult.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); // remove linebreaks

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("Audio Codec")) { Foreground = Log.ConsoleAction });
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Run(ffprobe + FFprobeParse.StartInfo.Arguments) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                    //FFprobeParse.Close();


                    // -------------------------
                    // Video Bitrate
                    // -------------------------
                    FFprobeParse.StartInfo.Arguments = " -i" + " " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -select_streams v:0 -show_entries " + vEntryType + " -v quiet -of csv=\"p=0\"";
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    ffprobeVideoBitrateResult = FFprobeParse.StandardOutput.ReadToEnd(); // Get Ouput Result
                    inputVideoBitrate = ffprobeVideoBitrateResult.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); // remove linebreaks                

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("Video Bitrate")) { Foreground = Log.ConsoleAction });
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Run(ffprobe + FFprobeParse.StartInfo.Arguments) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                    //FFprobeParse.Close();


                    // -------------------------
                    // Audio Bitrate
                    // -------------------------
                    FFprobeParse.StartInfo.Arguments = " -i" + " " + "\"" + mainwindow.textBoxBrowse.Text + "\"" + " -select_streams a:0 -show_entries " + aEntryType + " -v quiet -of csv=\"p=0\"";
                    FFprobeParse.Start();
                    FFprobeParse.WaitForExit();
                    ffprobeAudioBitrateResult = FFprobeParse.StandardOutput.ReadToEnd(); // Get Ouput Result
                    inputAudioBitrate = ffprobeAudioBitrateResult.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); // remove linebreaks

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("Audio Bitrate")) { Foreground = Log.ConsoleAction });
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Run(ffprobe + FFprobeParse.StartInfo.Arguments) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);

                    // Close Process
                    //FFprobeParse.Close();
                }


                // Dispose Process
                FFprobeParse.Dispose();
            }


        }


    }
}