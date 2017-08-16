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
    public partial class Audio
    {
        // --------------------------------------------------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------------------------------------------------
        // Audio
        public static string aCodec;
        public static string aQuality;
        public static string aBitMode;
        public static string aBitrate;
        public static string aChannel;
        public static string aSamplerate;
        public static string aBitDepth;
        public static string aVolume;
        public static string aLimiter;

        // Filter Lists
        public static List<string> AudioFilters = new List<string>(); // Filters to String Join
        public static string aFilter;

        // Batch
        public static string aBitrateLimiter; // limits the bitrate value of webm and ogg
        public static string batchAudioAuto;



        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Process Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Audio Codecs (Method)
        /// <summary>
        public static String AudioCodec(MainWindow mainwindow)
        {
            // -------------------------
            // Audio
            // -------------------------
            // None
            if ((string)mainwindow.cboAudioCodec.SelectedItem == "None")
            {
                aCodec = string.Empty;
                Streams.aMap = "-an";
            }
            // Vorbis
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
            {
                aCodec = "-acodec libvorbis";
            }
            // Opus
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
            {
                aCodec = "-acodec libopus";
            }
            // AAC
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
            {
                aCodec = "-acodec aac";
            }
            // ALAC
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC")
            {
                aCodec = "-acodec alac";
            }
            // AC3
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3")
            {
                aCodec = "-acodec ac3";
            }
            // LAME
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
            {
                aCodec = "-acodec libmp3lame";
            }
            // FLAC
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC")
            {
                aCodec = "-acodec flac";
            }
            // PCM
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
            {
                aCodec = string.Empty; // Codec not needed for PCM or Controlled by "PCM Match Bit Depth Audio" Section
            }
            // Copy
            else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Copy")
            {
                aCodec = "-acodec copy";
            }
            // Unknown
            else
            {
                aCodec = string.Empty;
            }


            // Mute
            if ((string)mainwindow.cboAudio.SelectedItem == "Mute" || (string)mainwindow.cboAudioStream.SelectedItem == "none")
            {
                aCodec = string.Empty;
            }


            // --------------------------------------------------
            // Category: Audio (Log)
            // --------------------------------------------------
            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Audio")) { Foreground = Log.ConsoleAction });
            };
            Log.LogActions.Add(Log.WriteAction);

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Codec: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(mainwindow.cboAudioCodec.SelectedItem.ToString()) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);


            // Return Value
            return aCodec;
        }



        /// <summary>
        /// Audio Bitrate Mode (Method)
        /// <summary>
        public static void AudioBitrateMode(MainWindow mainwindow)
        {
            // VBR
            if (mainwindow.tglVBR.IsChecked == true)
            {
                // Codec
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis") { aBitMode = "-q:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus") { aBitMode = "-vbr on -compression_level 10 -b:a"; } //special rule for opus -b:a -vbr on
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC") { aBitMode = "-q:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME") { aBitMode = "-q:a"; }

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate Method: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run("VBR") { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            // CBR
            if (mainwindow.tglVBR.IsChecked == false || mainwindow.tglVBR.IsEnabled == false)
            {
                // Codec
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis") { aBitMode = "-b:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus") { aBitMode = "-b:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC") { aBitMode = "-b:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME") { aBitMode = "-b:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitMode = string.Empty; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3") { aBitMode = "-b:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitMode = string.Empty; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitMode = string.Empty; }
                // Format
                if ((string)mainwindow.cboFormat.SelectedItem == "ogv") { aBitMode = "-q:a"; } // OGV, Force VBR or it will not work

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate Method: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run("CBR") { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }
        }


        /// <summary>
        /// Audio Bitrate Calculator (Method)
        /// <summary>
        public static void AudioBitrateCalculator(MainWindow mainwindow)
        {
            try
            {
                // If Video is Mute, don't set Audio Bitrate
                if (string.IsNullOrEmpty(FFprobe.inputAudioBitrate))
                {
                    // do nothing (dont remove, it will cause substring to overload)
                }
                // Filter out any extra spaces after the first 3 characters IMPORTANT
                else if (FFprobe.inputAudioBitrate.Substring(0, 3) == "N/A")
                {
                    FFprobe.inputAudioBitrate = "N/A";
                }

                // If Video has Audio, calculate Bitrate into decimal
                if (FFprobe.inputAudioBitrate != "N/A" && !string.IsNullOrEmpty(FFprobe.inputAudioBitrate))
                {
                    // Convert to Decimal
                    FFprobe.inputAudioBitrate = Convert.ToString(double.Parse(FFprobe.inputAudioBitrate) * 0.001);

                    // Apply limits if Bitrate goes over
                    if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis"
                        && double.Parse(FFprobe.inputAudioBitrate) > 500)
                    {
                        FFprobe.inputAudioBitrate = Convert.ToString(500); //was 500,000 (before converting to decimal)
                    }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus"
                        && double.Parse(FFprobe.inputAudioBitrate) > 510)
                    {
                        FFprobe.inputAudioBitrate = Convert.ToString(510); //was 510,000 (before converting to decimal)
                    }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME"
                        && double.Parse(FFprobe.inputAudioBitrate) > 320)
                    {
                        FFprobe.inputAudioBitrate = Convert.ToString(320); //was 320,000 before converting to decimal)
                    }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC"
                        && double.Parse(FFprobe.inputAudioBitrate) > 400)
                    {
                        FFprobe.inputAudioBitrate = Convert.ToString(400); //was 400,000 (before converting to decimal)
                    }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3"
                        && double.Parse(FFprobe.inputAudioBitrate) > 640)
                    {
                        FFprobe.inputAudioBitrate = Convert.ToString(640); //was 640,000 (before converting to decimal)
                    }
                    // ALAC, FLAC do not need limit

                    // Apply limits if Bitrate goes Under
                    // Vorbis has a minimum bitrate limit of 45k, if less than, set to 45k
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis"
                        && double.Parse(FFprobe.inputAudioBitrate) < 45)
                    {
                        FFprobe.inputAudioBitrate = Convert.ToString(45);
                    }

                    // Opus has a minimum bitrate limit of 6k, if less than, set to 6k
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus"
                        && double.Parse(FFprobe.inputAudioBitrate) < 6)
                    {
                        FFprobe.inputAudioBitrate = Convert.ToString(6);
                    }

                    // add k to value
                    //FFprobe.inputAudioBitrate = Convert.ToString(double.Parse(FFprobe.inputAudioBitrate)) + "k";
                    FFprobe.inputAudioBitrate = Convert.ToString(FFprobe.inputAudioBitrate);
                }
            }
            catch
            {
                MessageBox.Show("Error calculating Audio Bitrate.");
            }
        }


        /// <summary>
        /// Audio VBR Calculator (Method)
        /// <summary>
        public static String AudioVBRCalculator(MainWindow mainwindow, string inputBitrate)
        {
            // -------------------------
            // VBR 
            // User entered value
            // -------------------------
            if (mainwindow.tglVBR.IsChecked == true && (string)mainwindow.cboAudioCodec.SelectedItem != "Opus") // exclude opus
            {
                // Used to Calculate VBR Double
                //
                double aBitrateVBR = double.Parse(inputBitrate); // passed parameter


                // -------------------------
                // AAC (M4A, MP4, MKV) USER CUSTOM VBR
                // -------------------------
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
                {
                    // Calculate VBR
                    aBitrateVBR = aBitrateVBR * 0.00625;


                    // AAC VBR Above 320k Error (look into this)
                    if (aBitrateVBR > 400)
                    {
                        // Log Console Message /////////
                        Log.WriteAction = () =>
                        {
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new Bold(new Run("Warning: AAC VBR cannot be above 400k."))
                            { Foreground = Log.ConsoleWarning });
                        };
                        Log.LogActions.Add(Log.WriteAction);

                        /* lock */
                        MainWindow.ready = false;
                        // Error
                        MessageBox.Show("Error: AAC VBR cannot be above 400k.");
                    }
                }


                // -------------------------
                // VORBIS (WEBM, OGG) USER CUSTOM VBR
                // -------------------------
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
                {
                    // Above 290k set to 10 Quality
                    if (aBitrateVBR > 290)
                    {
                        aBitrateVBR = 10;
                    }
                    // 32 bracket
                    else if (aBitrateVBR >= 128) //above 113kbps, use standard equation
                    {
                        aBitrateVBR = aBitrateVBR * 0.03125;
                    }
                    // 16 bracket
                    else if (aBitrateVBR <= 127) //112kbps needs work, half decimal off
                    {
                        aBitrateVBR = (aBitrateVBR * 0.03125) - 0.5;
                    }
                    else if (aBitrateVBR <= 96)
                    {
                        aBitrateVBR = (aBitrateVBR * 0.013125) - 0.25;
                    }
                    // 8 bracket
                    else if (aBitrateVBR <= 64)
                    {
                        aBitrateVBR = 0;
                    }
                }

                // -------------------------
                // LAME (MP3) USER CUSTOM VBR
                // -------------------------
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
                {
                    // Above 245k set to V0
                    if (aBitrateVBR > 260)
                    {
                        aBitrateVBR = 0;
                    }
                    else
                    {
                        // VBR User entered value algorithm (0 high / 10 low)
                        aBitrateVBR = (((aBitrateVBR * (-0.01)) / 2.60) + 1) * 10;
                    }
                }


                // -------------------------
                // Convert to String for aQuality Combine
                // -------------------------
                aBitrate = Convert.ToString(aBitrateVBR);

            } //end VBR

            return aBitrate;
        }


        /// <summary>
        /// BatchAudioBitrateLimiter (Method)
        /// <summary>
        public static String BatchAudioBitrateLimiter(MainWindow mainwindow)
        {
            // -------------------------
            // Batch Limit Bitrates
            // -------------------------
            // Only if Audio ComboBox Auto
            if ((string)mainwindow.cboAudio.SelectedItem == "Auto")
            {
                // Limit Vorbis bitrate to 500k through cmd.exe
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
                {
                    aBitrateLimiter = "& (IF %A gtr 500000 (SET aBitrate=500000) ELSE (echo Bitrate within Vorbis Limit of 500k)) & for /F %A in ('echo %aBitrate%') do (echo)";
                }
                // Limit Opus bitrate to 510k through cmd.exe
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                {
                    aBitrateLimiter = "& (IF %A gtr 510000 (SET aBitrate=510000) ELSE (echo Bitrate within Opus Limit of 510k)) & for /F %A in ('echo %aBitrate%') do (echo)";
                }
                // Limit AAC bitrate to 400k through cmd.exe
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
                {
                    aBitrateLimiter = "& (IF %A gtr 400000 (SET aBitrate=400000) ELSE (echo Bitrate within AAC Limit of 400k)) & for /F %A in ('echo %aBitrate%') do (echo)";
                }
                // Limit AC3 bitrate to 640k through cmd.exe
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3")
                {
                    aBitrateLimiter = "& (IF %A gtr 640000 (SET aBitrate=640000) ELSE (echo Bitrate within AC3 Limit of 640k)) & for /F %A in ('echo %aBitrate%') do (echo)";
                }
                // Limit LAME bitrate to 320k through cmd.exe
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
                {
                    aBitrateLimiter = "& (IF %A gtr 320000 (SET aBitrate=320000) ELSE (echo Bitrate within LAME Limit of 320k)) & for /F %A in ('echo %aBitrate%') do (echo)";
                }
            }

            // Return Value
            return aBitrateLimiter;
        }


        /// <summary>
        /// BatchAudioQualityAuto (Method)
        /// <summary>
        public static String BatchAudioQualityAuto(MainWindow mainwindow)
        {
            // -------------------------
            // Batch Auto
            // -------------------------
            if (mainwindow.tglBatch.IsChecked == true)
            {
                // -------------------------
                // Batch Audio Auto Bitrates
                // -------------------------

                // Batch CMD Detect
                //
                if ((string)mainwindow.cboAudio.SelectedItem == "Auto")
                {
                    // Make List
                    List<string> BatchAudioAutoList = new List<string>()
                    {
                        // audio
                        "& for /F \"delims=\" %A in ('@" + FFprobe.ffprobe + " -v error -select_streams a:0 -show_entries " + FFprobe.aEntryType + " -of default^=noprint_wrappers^=1:nokey^=1 \"%~f\" 2^>^&1') do (SET aBitrate=%A)",

                        // set %A to %aBitrate%
                        "\r\n\r\n" + "& for /F %A in ('echo %aBitrate%') do (echo)",

                        // basic limiter
                        "\r\n\r\n" + "& (IF %A EQU N/A (SET aBitrate=320000))",

                        // set %A to %aBitrate%
                        "\r\n\r\n" + "& for /F %A in ('echo %aBitrate%') do (echo)"
                    };

                    // Join List with Spaces, Remove Empty Strings
                    Audio.batchAudioAuto = string.Join(" ", BatchAudioAutoList.Where(s => !string.IsNullOrEmpty(s)));

                }
                // Batch Audio Copy
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Copy")
                {
                    batchAudioAuto = string.Empty;
                }
            }

            // Return Value
            return batchAudioAuto;
        }


        /// <summary>
        /// Audio Quality (Method)
        /// <summary>
        public static String AudioQuality(MainWindow mainwindow)
        {
            // Audio None Check
            if ((string)mainwindow.cboAudio.SelectedItem != "None")
            {
                // Audio Bitrate Mode (Method)
                //
                AudioBitrateMode(mainwindow);


                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Quality: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(mainwindow.cboAudio.SelectedItem.ToString())) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);

                // -------------------------
                // Auto
                // -------------------------
                if ((string)mainwindow.cboAudio.SelectedItem == "Auto")
                {
                    // -------------------------
                    // Single
                    // -------------------------
                    if (mainwindow.tglBatch.IsChecked == false)
                    {
                        // Input Has Audio
                        //
                        if (!string.IsNullOrEmpty(FFprobe.inputAudioBitrate))
                        {
                            // Input Bitrate was detected
                            if (FFprobe.inputAudioBitrate != "N/A")
                            {
                                //CBR
                                if (mainwindow.tglVBR.IsChecked == false)
                                {
                                    aBitMode = "-b:a";
                                    aBitrate = FFprobe.inputAudioBitrate;

                                    // add k to value
                                    aBitrate = aBitrate + "k";
                                }

                                // VBR
                                else if (mainwindow.tglVBR.IsChecked == true)
                                {
                                    //aBitMode = "-q:a";
                                    aBitrate = AudioVBRCalculator(mainwindow, FFprobe.inputAudioBitrate);

                                    //vbr does not have k


                                    // Opus special rules
                                    if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                                    {
                                        // special rule for opus -b:a -vbr on
                                        // opus vbr cannot be above 256k
                                        aBitrate = "256k"; 
                                    }
                                }
                            }

                            // Input Does Not Have Audio Codec
                            //
                            if (!string.IsNullOrEmpty(FFprobe.inputAudioCodec))
                            {
                                // Default to a new bitrate if Input & Output formats Do Not match
                                if (FFprobe.inputAudioBitrate == "N/A"
                                    && !string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    aBitMode = "-b:a";
                                    aBitrate = "320k";
                                }
                            }

                        }

                        // Input No Audio
                        //
                        else if (string.IsNullOrEmpty(FFprobe.inputAudioBitrate))
                        {
                            aBitMode = string.Empty;
                            aBitrate = string.Empty;
                        }
                    }

                    // -------------------------
                    // Batch
                    // -------------------------
                    else if (mainwindow.tglBatch.IsChecked == true)
                    {
                        // Use the CMD Batch Audio Variable
                        if ((string)mainwindow.cboAudioCodec.SelectedItem != "Copy")
                        {
                            aBitMode = "-b:a";
                            aBitrate = "%A";
                        }
                    }
                }

                // -------------------------
                // Lossless
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "Lossless")
                {
                    // ALAC
                    if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC")
                    {
                        aBitMode = string.Empty;
                        aBitrate = string.Empty;
                    }
                    // FLAC
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC")
                    {
                        aBitMode = string.Empty;
                        aBitrate = string.Empty;
                    }
                    // PCM
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
                    {
                        aBitMode = string.Empty;
                        aBitrate = string.Empty;
                    }
                }

                // -------------------------
                // 640
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "640")
                {
                    // VBR
                    if (mainwindow.tglVBR.IsChecked == true)
                    {
                        // AC3 value > low 0-10 high
                        if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3")
                        {
                            aBitrate = string.Empty; //AC3 can't be VBR
                        }
                    }
                    // CBR default
                    else
                    {
                        aBitrate = "640k";
                    }
                }

                // -------------------------
                // 510
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "510")
                {
                    // VBR
                    if (mainwindow.tglVBR.IsChecked == true)
                    {
                        // Opus value > low 0-10 high
                        if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                        {
                            //special rule for opus -b:a -vbr on
                            // opus vbr cannot be above 256k
                            aBitrate = "256k"; 
                        }
                    }
                    // CBR default                                                                                                               
                    else
                    {
                        aBitrate = "510k";
                    }
                }

                // -------------------------
                // 500
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "500")
                {
                    // VBR
                    if (mainwindow.tglVBR.IsChecked == true)
                    {
                        // Vorbis value > low 0-10 high
                        // VBR default
                        if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
                        {
                            aBitrate = "10";
                        }
                    }
                    else
                    {
                        aBitrate = "500k";
                    }
                }

                // -------------------------
                // 448
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "448")
                {
                    // VBR
                    if (mainwindow.tglVBR.IsChecked == true)
                    {
                        // AC3 value > low 0.1-2 high
                        if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3")
                        {
                            aBitrate = string.Empty; /* AC3 can't be VBR */
                        }
                    }
                    // CBR default
                    else
                    {
                        aBitrate = "448k";
                    }
                }

                // -------------------------
                // 400
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "400")
                {
                    // VBR
                    if (mainwindow.tglVBR.IsChecked == true)
                    {
                        // AAC value > low 0.1-2 high
                        if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
                        {
                            aBitrate = "2";
                        }
                    }
                    // CBR default
                    else
                    {
                        aBitrate = "400k";
                    }
                }

                // -------------------------
                // 320
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "320")
                {
                    // VBR
                    if (mainwindow.tglVBR.IsChecked == true)
                    {
                        // Vorbis value > low 0-10 high
                        if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
                        {
                            aBitrate = "9";
                        }
                        // Opus value > low 0-10 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                        {
                            aBitrate = "320k"; //special rule for opus -b:a -vbr on
                        }
                        // AAC value > low 0.1-2 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
                        {
                            aBitrate = "2";
                        }
                        // LAME value > low 9-0 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
                        {
                            aBitrate = "0";
                        }
                    }
                    
                    // CBR default
                    else
                    {
                        aBitrate = "320k";
                    }
                }

                // -------------------------
                // 256
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "256")
                {
                    // VBR
                    if (mainwindow.tglVBR.IsChecked == true)
                    {
                        //webm value low 0-10 high
                        if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
                        {
                            aBitrate = "8";
                        }
                        // Opus value > low 0-10 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                        {
                            aBitrate = "256k"; //special rule for opus -b:a -vbr on
                        }
                        // AAC value > low 0.1-2 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
                        {
                            aBitrate = "1.6";
                        }
                        // LAME value > low 9-0 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
                        {
                            aBitrate = "0";
                        }
                    }
                    // CBR default
                    else
                    {
                        aBitrate = "256k";
                    }
                }

                // -------------------------
                // 224
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "224")
                {
                    // VBR
                    if (mainwindow.tglVBR.IsChecked == true)
                    {
                        // Vorbis value low 0-10 high
                        if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
                        {
                            aBitrate = "7";
                        }
                        // Opus value > low 0-10 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                        {
                            aBitrate = "224k"; //special rule for opus -b:a -vbr on
                        }
                        // AAC value > low 0.1-2 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
                        {
                            aBitrate = "1.4";
                        }
                        // LAME value > low 9-0 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
                        {
                            aBitrate = "1";
                        }
                    }
                    // CBR default
                    else
                    {
                        aBitrate = "224k";
                    }
                }

                // -------------------------
                // 192
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "192")
                {
                    // VBR
                    if (mainwindow.tglVBR.IsChecked == true)
                    {
                        // Vorbis value low 0-10 high
                        if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
                        {
                            aBitrate = "6";
                        }
                        // Opus value > low 0-10 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                        {
                            aBitrate = "192k"; //special rule for opus -b:a -vbr on
                        }
                        // AAC value > low 0.1-2 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
                        {
                            aBitrate = "1.2";
                        }
                        // LAME value > low 9-0 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
                        {
                            aBitrate = "2";
                        }
                    }
                    // CBR default
                    else
                    {
                        aBitrate = "192k";
                    }
                }

                // -------------------------
                // 160
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "160")
                {
                    // VBR
                    if (mainwindow.tglVBR.IsChecked == true)
                    {
                        //webm value low 0-10 high
                        if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
                        {
                            aBitrate = "5";
                        }
                        //opus value > low 0-10 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                        {
                            aBitrate = "160k"; //special rule for opus -b:a -vbr on
                        } 
                        //aac value > low 0.1-2 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
                        {
                            aBitrate = "1.1";
                        }
                        //lame value > low 9-0 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
                        {
                            aBitrate = "3";
                        }
                    }
                    //CBR default
                    else
                    {
                        aBitrate = "160k";
                    }
                }

                // -------------------------
                // 128
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "128")
                {   
                    // VBR
                    if (mainwindow.tglVBR.IsChecked == true)
                    {
                        //webm value low 0-10 high
                        if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
                        {
                            aBitrate = "4";
                        }
                        //opus value > low 0-10 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                        {
                            aBitrate = "128k"; //special rule for opus -b:a -vbr on
                        }
                        //aac value > low 0.1-2 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
                        {
                            aBitrate = "0.8";
                        }
                        //lame value > low 9-0 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
                        {
                            aBitrate = "5";
                        }
                    }
                    //CBR default
                    else
                    {
                        aBitrate = "128k";
                    }
                }

                // -------------------------
                // 96
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "96")
                {
                    // VBR
                    if (mainwindow.tglVBR.IsChecked == true)
                    {
                        //webm value low 0-10 high
                        if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
                        {
                            aBitrate = "2";
                        }
                        //opus value > low 0-10 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                        {
                            aBitrate = "96k"; //special rule for opus -b:a -vbr on
                        }
                        //aac value > low 0.1-2 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
                        {
                            aBitrate = "0.6";
                        }
                        //lame value > low 9-0 high
                        else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
                        {
                            aBitrate = "7";
                        }
                    }
                    //CBR default
                    else
                    {
                        aBitrate = "96k";
                    }
                }

                // -------------------------
                // Custom
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "Custom")
                {
                    // Empty Check
                    // Prevents Crash
                    if (!string.IsNullOrWhiteSpace(mainwindow.audioCustom.Text))
                    {
                        // -------------------------
                        // CBR 
                        // User entered value
                        // -------------------------
                        if (mainwindow.tglVBR.IsChecked == false)
                        {
                            aBitrate = mainwindow.audioCustom.Text + "k";
                        }


                        // -------------------------
                        // VBR 
                        // User entered value
                        // -------------------------
                        aBitrate = AudioVBRCalculator(mainwindow, mainwindow.audioCustom.Text);
                    }

                    // Custom Empty
                    else
                    {
                        aBitMode = string.Empty;
                        aBitrate = string.Empty;
                    }
                }

                // -------------------------
                // Mute
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "Mute")
                {
                    aBitMode = string.Empty;
                    aBitrate = string.Empty;
                }

                // -------------------------
                // None
                // -------------------------
                else if ((string)mainwindow.cboAudio.SelectedItem == "None")
                {
                    aBitMode = string.Empty;
                    aBitrate = string.Empty;
                }


                // -------------------------
                // Copy
                // -------------------------
                // Audio Codec Copy
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Copy")
                {
                    aBitMode = string.Empty;
                    aBitrate = string.Empty;
                }

                // -------------------------
                // Combine
                // -------------------------
                List<string> aQualityArgs = new List<string>()
                {
                    aBitMode,
                    aBitrate
                };

                aQuality = string.Join(" ", aQualityArgs
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Where(s => !s.Equals("\n"))
                    );


                // Log Console Message /////////
                // CBR
                if (mainwindow.tglVBR.IsChecked == false)
                {
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run(aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
                // VBR
                else if (mainwindow.tglVBR.IsChecked == true)
                {
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + mainwindow.cboAudio.SelectedItem.ToString() + " to VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }

            } // end Null Check


            // Return Value
            return aQuality;

        } // end AudioQuality


        /// <summary>
        /// Channel (Method)
        /// <summary>
        public static String Channel(MainWindow mainwindow)
        {
            // Auto
            if ((string)mainwindow.cboChannel.SelectedItem == "Auto")
            {
                aChannel = string.Empty;
            }
            // Stereo
            else if ((string)mainwindow.cboChannel.SelectedItem == "Stereo")
            {
                aChannel = "-ac 2";
            }
            // Joint Stereo
            else if ((string)mainwindow.cboChannel.SelectedItem == "Joint Stereo")
            {
                aChannel = "-ac 2 -joint_stereo 1";
            }
            // Mono
            else if ((string)mainwindow.cboChannel.SelectedItem == "Mono")
            {
                aChannel = "-ac 1";
            }

            // Mute
            if ((string)mainwindow.cboAudio.SelectedItem == "Mute" || (string)mainwindow.cboAudioStream.SelectedItem == "none")
            {
                aChannel = string.Empty;
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Channel: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(mainwindow.cboChannel.Text.ToString()) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            // Return Value
            return aChannel;
        }


        /// <summary>
        /// Sample Rate (Method)
        /// <summary>
        public static String SampleRate(MainWindow mainwindow)
        {
            if ((string)mainwindow.cboSamplerate.SelectedItem == "auto")
            {
                aSamplerate = string.Empty;
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "8k")
            {
                aSamplerate = "-ar 8000";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "11.025k")
            {
                aSamplerate = "-ar 11025";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "12k")
            {
                aSamplerate = "-ar 12000";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "16k")
            {
                aSamplerate = "-ar 16000";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "22.05k")
            {
                aSamplerate = "-ar 22050";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "24k")
            {
                aSamplerate = "-ar 24000";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "32k")
            {
                aSamplerate = "-ar 32000";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "44.1k")
            {
                aSamplerate = "-ar 44100";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "48k")
            {
                aSamplerate = "-ar 48000";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "64k")
            {
                aSamplerate = "-ar 64000";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "88.2k")
            {
                aSamplerate = "-ar 88200";
            }
            else if ((string)mainwindow.cboSamplerate.SelectedItem == "96k")
            {
                aSamplerate = "-ar 96000";
            }

            // Mute
            if ((string)mainwindow.cboAudio.SelectedItem == "Mute" || (string)mainwindow.cboAudioStream.SelectedItem == "none")
            {
                aSamplerate = string.Empty;
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Sample Rate: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(mainwindow.cboSamplerate.Text.ToString()) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);


            // Return Value
            return aSamplerate;
        }


        /// <summary>
        /// Bit Depth (Method)
        /// <summary>
        public static String BitDepth(MainWindow mainwindow)
        {
            // PCM has Bitdepth defined by Codec instead of sample_fmt, can use 8, 16, 24, 32, 64-bit
            // FLAC can only use 16 and 32-bit
            // ALAC can only use 16 and 32-bit

            if ((string)mainwindow.cboBitDepth.SelectedItem == "auto")
            {
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitDepth = string.Empty; aCodec = "-acodec pcm_s24le"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitDepth = string.Empty; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitDepth = string.Empty; }
                else { aBitDepth = string.Empty; } // all other codecs
            }
            else if ((string)mainwindow.cboBitDepth.SelectedItem == "8")
            {
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitDepth = string.Empty; aCodec = "-acodec pcm_u8"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitDepth = string.Empty; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitDepth = string.Empty; }
                else { aBitDepth = string.Empty; } // all other codecs
            }
            else if ((string)mainwindow.cboBitDepth.SelectedItem == "16")
            {
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitDepth = string.Empty; aCodec = "-acodec pcm_s16le"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitDepth = "-sample_fmt s16"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitDepth = "-sample_fmt s16p"; }
                else { aBitDepth = string.Empty; } // all other codecs
            }
            else if ((string)mainwindow.cboBitDepth.SelectedItem == "24")
            {
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitDepth = string.Empty; aCodec = "-acodec pcm_s24le"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitDepth = string.Empty; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitDepth = string.Empty; }
                else { aBitDepth = string.Empty; } // all other codecs
            }
            else if ((string)mainwindow.cboBitDepth.SelectedItem == "32")
            {
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitDepth = string.Empty; aCodec = "-acodec pcm_f32le"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitDepth = "-sample_fmt s32"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitDepth = "-sample_fmt s32p"; }
                else { aBitDepth = string.Empty; } // all other codecs
            }
            else if ((string)mainwindow.cboBitDepth.SelectedItem == "64")
            {
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitDepth = string.Empty; aCodec = "-acodec pcm_f64le"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitDepth = string.Empty; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitDepth = string.Empty; }
                else { aBitDepth = string.Empty; } // all other codecs
            }


            // Mute
            if ((string)mainwindow.cboAudio.SelectedItem == "Mute" || (string)mainwindow.cboAudioStream.SelectedItem == "none")
            {
                aBitDepth = string.Empty;
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Bit Depth: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(mainwindow.cboBitDepth.SelectedItem.ToString()) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);


            // Return Value
            return aBitDepth;
        }


        /// <summary>
        /// Volume (Method)
        /// <summary>
        public static void Volume(MainWindow mainwindow)
        {
            // -------------------------
            // Only if Audio Codec is Not Empty
            // -------------------------
            if (!string.IsNullOrEmpty((string)mainwindow.cboAudioCodec.SelectedItem))
            {
                // If TextBox is 100% or Blank
                if (mainwindow.volumeUpDown.Text == "100%" 
                    || mainwindow.volumeUpDown.Text == "100" 
                    || string.IsNullOrWhiteSpace(mainwindow.volumeUpDown.Text))
                {
                    aVolume = string.Empty;
                }
                // If User Custom Entered Value
                // Convert Volume % to Decimal
                else
                {
                    // If user enters value, turn on Filter
                    string volumePercent = mainwindow.volumeUpDown.Text;
                    double volumeDecimal = double.Parse(volumePercent.TrimEnd(new[] { '%' })) / 100;
                    aVolume = "volume=" + volumeDecimal;

                    // Audio Filter Add
                    AudioFilters.Add(aVolume);
                }
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Volume: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(mainwindow.volumeUpDown.Text.ToString()) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);
        }





        /// <summary>
        /// HardLimiter Filter (Method)
        /// <summary>
        public static void HardLimiter(MainWindow mainwindow)
        {
            // -------------------------
            // Only if Audio Codec is Not Empty
            // -------------------------
            if (mainwindow.tglAudioLimiter.IsChecked == true && !string.IsNullOrEmpty((string)mainwindow.cboAudioCodec.SelectedItem)) 
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Hard Limiter Toggle: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run("On") { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);

                aLimiter = "alimiter=level_in=1:level_out=1:limit=" + mainwindow.audioLimiter.Text + ":attack=7:release=100:level=disabled";

                // Audio Filter Add
                AudioFilters.Add(aLimiter);
            }

            // -------------------------
            // Off
            // -------------------------
            if (mainwindow.tglAudioLimiter.IsChecked == false)
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("alimiter Toggle: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run("Off") { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);

                aLimiter = string.Empty;
            }
        }


        /// <summary>
        /// Audio Filter Combine (Method)
        /// <summary>
        public static String AudioFilter(MainWindow mainwindow)
        {
            // --------------------------------------------------
            // Filters
            // --------------------------------------------------
            /// <summary>
            ///    Volume
            /// </summary> 
            Audio.Volume(mainwindow);

            /// <summary>
            ///    ALimiter
            /// </summary> 
            Audio.HardLimiter(mainwindow);

            // -------------------------
            // Filter Combine
            // -------------------------
            if ((string)mainwindow.cboAudioCodec.SelectedItem != "None") // None Check
            {
                // 1 Filter
                //
                if (AudioFilters.Count() == 1)
                {
                    aFilter = "-af " + string.Join(", ", AudioFilters.Where(s => !string.IsNullOrEmpty(s)));
                }

                // Multiple Filters
                //
                else if (AudioFilters.Count() > 1)
                {
                    aFilter = "-af \"" + string.Join(", ", AudioFilters.Where(s => !string.IsNullOrEmpty(s))) + "\"";
                }

                // Empty
                //
                else
                {
                    aFilter = string.Empty;
                }
            }
            // Audio Codec None
            else
            {
                aFilter = string.Empty;

            }

            // -------------------------
            // Filter Clear
            // -------------------------
            // Copy
            // Mute
            // Stream none
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy"
                || (string)mainwindow.cboAudio.SelectedItem == "Mute" 
                || (string)mainwindow.cboAudioStream.SelectedItem == "none")
            {
                aFilter = string.Empty;

                if (AudioFilters != null)
                {
                    AudioFilters.Clear();
                    AudioFilters.TrimExcess();
                }
            }


            // Return Value
            return aFilter;
        }

    }
}