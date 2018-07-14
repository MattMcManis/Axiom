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
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class Audio
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

        // Batch
        public static string aBitrateLimiter; // limits the bitrate value of webm and ogg
        public static string batchAudioAuto;



        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Process Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        ///     Audio Codecs (Method)
        /// <summary>
        public static String AudioCodec(MainWindow mainwindow)
        {
            // Audio Bitrate None Check
            // Audio Codec None Check
            // Mute Check
            // Stream None Check
            // Media Type Check
            if ((string)mainwindow.cboAudioQuality.SelectedItem != "None"
                && (string)mainwindow.cboAudioCodec.SelectedItem != "None"
                && (string)mainwindow.cboAudioQuality.SelectedItem != "Mute"
                && (string)mainwindow.cboAudioStream.SelectedItem != "none"
                && (string)mainwindow.cboMediaType.SelectedItem != "Image"
                && (string)mainwindow.cboMediaType.SelectedItem != "Sequence")
            {
                // -------------------------
                // Audio
                // -------------------------
                // None
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "None")
                {
                    aCodec = string.Empty;
                }
                // Vorbis
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
                {
                    aCodec = "-c:a libvorbis";
                }
                // Opus
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                {
                    aCodec = "-c:a libopus";
                }
                // AAC
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
                {
                    aCodec = "-c:a aac";
                }
                // ALAC
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC")
                {
                    aCodec = "-c:a alac";
                }
                // AC3
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3")
                {
                    aCodec = "-c:a ac3";
                }
                // LAME
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
                {
                    aCodec = "-c:a libmp3lame";
                }
                // FLAC
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC")
                {
                    aCodec = "-c:a flac";
                }
                // PCM
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
                {
                    // Codec and BitDepth are the same
                    if ((string)mainwindow.cboBitDepth.SelectedItem == "auto")
                    {
                        aCodec = "-c:a pcm_s24le";
                    }
                    if ((string)mainwindow.cboBitDepth.SelectedItem == "8")
                    {
                        aCodec = "-c:a pcm_u8";
                    }
                    if ((string)mainwindow.cboBitDepth.SelectedItem == "16")
                    {
                        aCodec = "-c:a pcm_s16le";
                    }
                    if ((string)mainwindow.cboBitDepth.SelectedItem == "24")
                    {
                        aCodec = "-c:a pcm_s24le";
                    }
                    if ((string)mainwindow.cboBitDepth.SelectedItem == "32")
                    {
                        aCodec = "-c:a pcm_f32le";
                    }
                    if ((string)mainwindow.cboBitDepth.SelectedItem == "64")
                    {
                        aCodec = "-c:a pcm_f64le";
                    }
                }
                // Copy
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Copy")
                {
                    aCodec = "-c:a copy";
                }
                // Unknown
                else
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
            }


            // Return Value
            return aCodec;
        }



        /// <summary>
        ///     Audio Bitrate Mode (Method)
        /// <summary>
        public static String AudioBitrateMode(MainWindow mainwindow)
        {
            // -------------------------
            // VBR
            // -------------------------
            if (mainwindow.tglAudioVBR.IsChecked == true)
            {
                // Codec
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis") { aBitMode = "-q:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus") { aBitMode = "-vbr on -compression_level 10 -b:a"; } //special rule for opus -b:a -vbr on
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC") { aBitMode = "-q:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME") { aBitMode = "-q:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Copy") { aBitMode = string.Empty; }

                // Format
                if ((string)mainwindow.cboFormat.SelectedItem == "ogv") { aBitMode = "-q:a"; }

                // Bitrate
                if ((string)mainwindow.cboAudioQuality.SelectedItem == "Lossless") { aBitMode = string.Empty; }
                else if ((string)mainwindow.cboAudioQuality.SelectedItem == "Mute") { aBitMode = string.Empty; }

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate Method: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run("VBR") { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            // -------------------------
            // CBR
            // -------------------------
            if (mainwindow.tglAudioVBR.IsChecked == false 
                || mainwindow.tglAudioVBR.IsEnabled == false)
            {
                // Codec
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis") { aBitMode = "-b:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus") { aBitMode = "-b:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC") { aBitMode = "-b:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME") { aBitMode = "-b:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC") { aBitMode = string.Empty; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3") { aBitMode = "-b:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC") { aBitMode = "-b:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM") { aBitMode = "-b:a"; }
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Copy") { aBitMode = string.Empty; }

                // Format
                if ((string)mainwindow.cboFormat.SelectedItem == "ogv") { aBitMode = "-q:a"; } // OGV, Force VBR or it will not work

                // Bitrate
                if ((string)mainwindow.cboAudioQuality.SelectedItem == "Lossless") { aBitMode = string.Empty; }
                else if ((string)mainwindow.cboAudioQuality.SelectedItem == "Mute") { aBitMode = string.Empty; }

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate Method: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run("CBR") { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            return aBitMode;
        }


        /// <summary>
        ///     Audio Bitrate Calculator (Method)
        /// <summary>
        public static String AudioBitrateCalculator(MainWindow mainwindow, string aEntryType, string inputAudioBitrate)
        {
            try
            {
                // -------------------------
                // If Video is Mute, don't set Audio Bitrate
                // -------------------------
                if (string.IsNullOrEmpty(FFprobe.inputAudioBitrate))
                {
                    // do nothing (dont remove, it will cause substring to overload)
                }

                // -------------------------
                // Filter out any extra spaces after the first 3 characters IMPORTANT
                // -------------------------
                else if (inputAudioBitrate.Substring(0, 3) == "N/A")
                {
                    inputAudioBitrate = "N/A";
                }

                // -------------------------
                // If Video has Audio, calculate Bitrate into decimal
                // -------------------------
                if (inputAudioBitrate != "N/A" && !string.IsNullOrEmpty(FFprobe.inputAudioBitrate))
                {
                    // -------------------------
                    // Convert to Decimal
                    // -------------------------
                    inputAudioBitrate = Convert.ToString(double.Parse(inputAudioBitrate) * 0.001);

                    // -------------------------
                    // Apply limits if Bitrate goes over
                    // -------------------------
                    // -------------------------
                    // Vorbis
                    // -------------------------
                    if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis"
                        && double.Parse(inputAudioBitrate) > 500)
                    {
                        inputAudioBitrate = Convert.ToString(500); //was 500,000 (before converting to decimal)
                    }
                    // -------------------------
                    // Opus
                    // -------------------------
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus"
                        && double.Parse(inputAudioBitrate) > 510)
                    {
                        inputAudioBitrate = Convert.ToString(510); //was 510,000 (before converting to decimal)
                    }
                    // -------------------------
                    // LAME
                    // -------------------------
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME"
                        && double.Parse(inputAudioBitrate) > 320)
                    {
                        inputAudioBitrate = Convert.ToString(320); //was 320,000 before converting to decimal)
                    }
                    // -------------------------
                    // AAC
                    // -------------------------
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC"
                        && double.Parse(inputAudioBitrate) > 400)
                    {
                        inputAudioBitrate = Convert.ToString(400); //was 400,000 (before converting to decimal)
                    }
                    // -------------------------
                    // AC3
                    // -------------------------
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3"
                        && double.Parse(inputAudioBitrate) > 640)
                    {
                        inputAudioBitrate = Convert.ToString(640); //was 640,000 (before converting to decimal)
                    }

                    // -------------------------
                    // ALAC, FLAC do not need limit
                    // -------------------------

                    // -------------------------
                    // Apply limits if Bitrate goes Under
                    // -------------------------
                    // -------------------------
                    // Vorbis
                    // -------------------------
                    // Vorbis has a minimum bitrate limit of 45k, if less than, set to 45k
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis"
                        && double.Parse(inputAudioBitrate) < 45)
                    {
                        inputAudioBitrate = Convert.ToString(45);
                    }

                    // -------------------------
                    // Opus
                    // -------------------------
                    // Opus has a minimum bitrate limit of 6k, if less than, set to 6k
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus"
                        && double.Parse(inputAudioBitrate) < 6)
                    {
                        inputAudioBitrate = Convert.ToString(6);
                    }

                    // -------------------------
                    // Round Bitrate, Remove Decimals
                    // -------------------------
                    inputAudioBitrate = Math.Round(double.Parse(inputAudioBitrate)).ToString();
                }
            }
            catch
            {
                MessageBox.Show("Error calculating Audio Bitrate.");
            }

            return inputAudioBitrate;
        }


        /// <summary>
        ///     Audio VBR Calculator (Method)
        /// <summary>
        public static String AudioVBRCalculator(MainWindow mainwindow, string inputBitrate)
        {
            // -------------------------
            // VBR 
            // User entered value
            // -------------------------
            if (mainwindow.tglAudioVBR.IsChecked == true)
            {
                // Used to Calculate VBR Double
                //
                double aBitrateVBR = double.Parse(inputBitrate); // passed parameter


                // -------------------------
                // AAC (M4A, MP4, MKV) User Custom VBR
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
                // Vorbis (WebM, OGG) User Custom VBR
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
                // LAME (MP3) User Custom VBR
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
        ///     BatchAudioBitrateLimiter (Method)
        /// <summary>
        public static String BatchAudioBitrateLimiter(MainWindow mainwindow)
        {
            // Audio Bitrate None Check
            // Audio Codec None
            // Codec Copy Check
            // Mute Check
            // Stream None Check
            // Media Type Check
            if ((string)mainwindow.cboAudioQuality.SelectedItem != "None"
                && (string)mainwindow.cboAudioCodec.SelectedItem != "None"
                && (string)mainwindow.cboAudioCodec.SelectedItem != "Copy"
                && (string)mainwindow.cboAudioQuality.SelectedItem != "Mute"
                && (string)mainwindow.cboAudioStream.SelectedItem != "none"
                && (string)mainwindow.cboMediaType.SelectedItem != "Image"
                && (string)mainwindow.cboMediaType.SelectedItem != "Sequence")
            {
                // -------------------------
                // Batch Limit Bitrates
                // -------------------------
                // Only if Audio ComboBox Auto
                if ((string)mainwindow.cboAudioQuality.SelectedItem == "Auto")
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
            }

            // Return Value
            return aBitrateLimiter;
        }


        /// <summary>
        /// BatchAudioQualityAuto (Method)
        /// <summary>
        public static String BatchAudioQualityAuto(MainWindow mainwindow)
        {
            // Audio Bitrate None Check
            // Audio Codec None
            // Codec Copy Check
            // Mute Check
            // Stream None Check
            // Media Type Check
            if ((string)mainwindow.cboAudioQuality.SelectedItem != "None"
                && (string)mainwindow.cboAudioCodec.SelectedItem != "None"
                && (string)mainwindow.cboAudioCodec.SelectedItem != "Copy"
                && (string)mainwindow.cboAudioQuality.SelectedItem != "Mute"
                && (string)mainwindow.cboAudioStream.SelectedItem != "none"
                && (string)mainwindow.cboMediaType.SelectedItem != "Image"
                && (string)mainwindow.cboMediaType.SelectedItem != "Sequence")
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
                    if ((string)mainwindow.cboAudioQuality.SelectedItem == "Auto")
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
                }
            }

            // Return Value
            return batchAudioAuto;
        }



        /// <summary>
        ///     Audio Quality Presets (Method)
        /// <summary>
        public static void AudioQualityPresets(
                                                MainWindow mainwindow,
                                                string audio_Quality,
                                                bool vbr_IsChecked,

                                                // CBR
                                                string lossless,   // Lossless
                                                string cbr_640,    // 640k
                                                string cbr_510,    // 510k
                                                string cbr_500,    // 500k
                                                string cbr_448,    // 448k
                                                string cbr_400,    // 400k
                                                string cbr_320,    // 320k
                                                string cbr_256,    // 256k
                                                string cbr_224,    // 224k
                                                string cbr_192,    // 192k
                                                string cbr_160,    // 160k
                                                string cbr_128,    // 128k
                                                string cbr_96,     // 96k

                                                // VBR
                                                string vbr_640,    // 640k
                                                string vbr_510,    // 510k
                                                string vbr_500,    // 500k
                                                string vbr_448,    // 448k
                                                string vbr_400,    // 400k
                                                string vbr_320,    // 320k
                                                string vbr_256,    // 256k
                                                string vbr_224,    // 224k
                                                string vbr_192,    // 192k
                                                string vbr_160,    // 160k
                                                string vbr_128,    // 128k
                                                string vbr_96      // 96k
                                                )
        {
            // -------------------------
            // Auto
            // -------------------------
            if ((string)mainwindow.cboAudioQuality.SelectedItem == "Auto")
            {
                // -------------------------
                // Single
                // -------------------------
                if (mainwindow.tglBatch.IsChecked == false)
                {
                    // -------------------------
                    // Input Has Audio
                    // -------------------------
                    if (!string.IsNullOrEmpty(FFprobe.inputAudioBitrate))
                    {
                        // Input Bitrate was detected
                        if (FFprobe.inputAudioBitrate != "N/A")
                        {
                            // CBR
                            if (vbr_IsChecked == false)
                            {
                                // aBitMode = "-b:a";
                                aBitrate = AudioBitrateCalculator(mainwindow, FFprobe.aEntryType, FFprobe.inputAudioBitrate);

                                // add k to value
                                aBitrate = aBitrate + "k";
                            }

                            // VBR
                            else if (vbr_IsChecked == true)
                            {
                                //vbr does not have k

                                aBitrate = AudioVBRCalculator(mainwindow, FFprobe.inputAudioBitrate);

                                // Opus uses -b:a (value)k -vbr on
                                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                                {
                                    aBitrate = aBitrate + "k";
                                }
                            }
                        }

                        // -------------------------
                        // Input Does Not Have Audio Codec
                        // -------------------------
                        if (!string.IsNullOrEmpty(FFprobe.inputAudioCodec))
                        {
                            // Default to a new bitrate if Input & Output formats Do Not match
                            if (FFprobe.inputAudioBitrate == "N/A"
                                && !string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase))
                            {
                                // aBitMode = "-b:a";
                                aBitrate = "320k";
                            }
                        }

                    }

                    // -------------------------
                    // Input No Audio
                    // -------------------------
                    else if (string.IsNullOrEmpty(FFprobe.inputAudioBitrate))
                    {
                        aBitMode = string.Empty;
                        aBitrate = string.Empty;
                    }


                    // -------------------------
                    // Bitrate Returned Empty
                    // -------------------------
                    if (string.IsNullOrEmpty(aBitrate))
                    {
                        // Disable Bit Mode
                        aBitMode = string.Empty;
                    }
                }

                // -------------------------
                // Batch
                // -------------------------
                else if (mainwindow.tglBatch.IsChecked == true)
                {
                    // Use the CMD Batch Audio Variable
                    aBitrate = "%A";
                }
            }


            // -------------------------
            // CBR
            // -------------------------
            if (vbr_IsChecked == false)
            {
                if (audio_Quality == "Lossless")
                {
                    aBitrate = lossless;
                }
                else if (audio_Quality == "640")
                {
                    aBitrate = cbr_640;
                }
                else if (audio_Quality == "510")
                {
                    aBitrate = cbr_510;
                }
                else if (audio_Quality == "500")
                {
                    aBitrate = cbr_510;
                }
                else if (audio_Quality == "448")
                {
                    aBitrate = cbr_448;
                }
                else if (audio_Quality == "400")
                {
                    aBitrate = cbr_400;
                }
                else if (audio_Quality == "320")
                {
                    aBitrate = cbr_320;
                }
                else if (audio_Quality == "256")
                {
                    aBitrate = cbr_256;
                }
                else if (audio_Quality == "224")
                {
                    aBitrate = cbr_224;
                }
                else if (audio_Quality == "192")
                {
                    aBitrate = cbr_192;
                }
                else if (audio_Quality == "160")
                {
                    aBitrate = cbr_160;
                }
                else if (audio_Quality == "128")
                {
                    aBitrate = cbr_128;
                }
                else if (audio_Quality == "96")
                {
                    aBitrate = cbr_96;
                }
                // Custom
                else if (audio_Quality == "Custom")
                {
                    // Empty Check - Prevents Crash
                    if (!string.IsNullOrWhiteSpace(mainwindow.audioCustom.Text.Trim()))
                    {
                        aBitrate = mainwindow.audioCustom.Text + "k";
                    }
                }
                // Mute
                else if (audio_Quality == "Mute")
                {
                    aBitrate = string.Empty;
                }
            }

            // -------------------------
            // VBR
            // -------------------------
            else if (vbr_IsChecked == true)
            {
                if (audio_Quality == "640")
                {
                    aBitrate = vbr_640;
                }
                else if (audio_Quality == "510")
                {
                    aBitrate = vbr_510;
                }
                else if (audio_Quality == "500")
                {
                    aBitrate = vbr_510;
                }
                else if (audio_Quality == "448")
                {
                    aBitrate = vbr_448;
                }
                else if (audio_Quality == "400")
                {
                    aBitrate = vbr_400;
                }
                else if (audio_Quality == "320")
                {
                    aBitrate = vbr_320;
                }
                else if (audio_Quality == "256")
                {
                    aBitrate = vbr_256;
                }
                else if (audio_Quality == "224")
                {
                    aBitrate = vbr_224;
                }
                else if (audio_Quality == "192")
                {
                    aBitrate = vbr_192;
                }
                else if (audio_Quality == "160")
                {
                    aBitrate = vbr_160;
                }
                else if (audio_Quality == "128")
                {
                    aBitrate = vbr_128;
                }
                else if (audio_Quality == "96")
                {
                    aBitrate = vbr_96;
                }
                // Custom
                else if (audio_Quality == "Custom")
                {
                    // Empty Check - Prevents Crash
                    if (!string.IsNullOrWhiteSpace(mainwindow.audioCustom.Text.Trim()))
                    {
                        aBitrate = AudioVBRCalculator(mainwindow, mainwindow.audioCustom.Text.Trim());

                        // Opus uses -b:a (value)k -vbr on
                        if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                        {
                            aBitrate = aBitrate + "k";
                        }
                    }
                }
                // Mute
                else if (audio_Quality == "Mute")
                {
                    aBitrate = string.Empty;
                }
            }
        }



        /// <summary>
        ///     Audio Quality (Method)
        /// <summary>
        public static String AudioQuality(MainWindow mainwindow)
        {
            // -------------------------
            // Vorbis low 0-10 high
            // Opus low 0-10 high
            // AAC low 0.1-2 high
            // LAME low 9-0 high
            // -------------------------

            // Audio Bitrate None Check
            // Audio Codec None
            // Codec Copy Check
            // Mute Check
            // Stream None Check
            // Media Type Check
            if ((string)mainwindow.cboAudioQuality.SelectedItem != "None"
                && (string)mainwindow.cboAudioCodec.SelectedItem != "None"
                && (string)mainwindow.cboAudioCodec.SelectedItem != "Copy"
                && (string)mainwindow.cboAudioQuality.SelectedItem != "Mute"
                && (string)mainwindow.cboAudioStream.SelectedItem != "none"
                && (string)mainwindow.cboMediaType.SelectedItem != "Image"
                && (string)mainwindow.cboMediaType.SelectedItem != "Sequence")
            {
                // -------------------------
                // Audio Bitrate Mode
                // -------------------------
                aBitMode = AudioBitrateMode(mainwindow);

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Quality: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(mainwindow.cboAudioQuality.SelectedItem.ToString())) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);


                // -------------------------
                // Vorbis
                // -------------------------
                if ((string)mainwindow.cboAudioCodec.SelectedItem == "Vorbis")
                {
                    AudioQualityPresets(
                                mainwindow,
                                // Quality Selected
                                (string)mainwindow.cboAudioQuality.SelectedItem,

                                // VBR Toggle
                                (bool)mainwindow.tglAudioVBR.IsChecked,

                                // Lossless
                                "",

                                // CBR
                                "",         // 640k
                                "",         // 510k
                                "500k",     // 500k
                                "",         // 448k
                                "",         // 400k
                                "320k",     // 320k
                                "256k",     // 256k
                                "224k",     // 224k
                                "192k",     // 192k
                                "160k",     // 160k
                                "128k",     // 128k
                                "96k",      // 96k

                                // VBR
                                "",         // 640k
                                "",         // 510k
                                "10",       // 500k
                                "",         // 448k
                                "",         // 400k
                                "9",        // 320k
                                "8",        // 256k
                                "7",        // 224k
                                "6",        // 192k
                                "5",        // 160k
                                "4",        // 128k
                                "2"         // 96k
                            );
                }

                // -------------------------
                // Opus
                // -------------------------
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Opus")
                {
                    AudioQualityPresets(
                                mainwindow,
                                // Quality Selected
                                (string)mainwindow.cboAudioQuality.SelectedItem,

                                // VBR Toggle
                                (bool)mainwindow.tglAudioVBR.IsChecked,

                                // Lossless
                                "",   

                                // CBR
                                "",         // 640k
                                "510k",     // 510k
                                "",         // 500k
                                "",         // 448k
                                "",         // 400k
                                "320k",     // 320k
                                "256k",     // 256k
                                "224k",     // 224k
                                "192k",     // 192k
                                "160k",     // 160k
                                "128k",     // 128k
                                "96k",      // 96k

                                // VBR
                                "",         // 640k
                                "",         // 510k
                                "256k",     // 500k
                                "",         // 448k
                                "",         // 400k
                                "256k",     // 320k
                                "256k",     // 256k
                                "224k",     // 224k
                                "192k",     // 192k
                                "160k",     // 160k
                                "128k",     // 128k
                                "96k"       // 96k
                            );
                }

                // -------------------------
                // AAC
                // -------------------------
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AAC")
                {
                    AudioQualityPresets(
                                mainwindow,
                                // Quality Selected
                                (string)mainwindow.cboAudioQuality.SelectedItem,

                                // VBR Toggle
                                (bool)mainwindow.tglAudioVBR.IsChecked,

                                // Lossless
                                "",   

                                // CBR
                                "",         // 640k
                                "",         // 510k
                                "",         // 500k
                                "",         // 448k
                                "400k",     // 400k
                                "320k",     // 320k
                                "256k",     // 256k
                                "224k",     // 224k
                                "192k",     // 192k
                                "160k",     // 160k
                                "128k",     // 128k
                                "96k",      // 96k

                                // VBR
                                "",         // 640k
                                "",         // 510k
                                "",         // 500k
                                "",         // 448k
                                "2",        // 400k
                                "2",        // 320k
                                "1.6",      // 256k
                                "1.4",      // 224k
                                "1.2",      // 192k
                                "1.1",      // 160k
                                "0.8",      // 128k
                                "0.6"       // 96k
                            );
                }

                // -------------------------
                // AC3
                // -------------------------
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "AC3")
                {
                    AudioQualityPresets(
                                mainwindow,
                                // Quality Selected
                                (string)mainwindow.cboAudioQuality.SelectedItem,

                                // VBR Toggle
                                (bool)mainwindow.tglAudioVBR.IsChecked,

                                // Lossless
                                "",   

                                // CBR
                                "640k",     // 640k
                                "",         // 510k
                                "",         // 500k
                                "448k",     // 448k
                                "",         // 400k
                                "320k",     // 320k
                                "256k",     // 256k
                                "224k",     // 224k
                                "192k",     // 192k
                                "160k",     // 160k
                                "128k",     // 128k
                                "96k",      // 96k

                                // VBR
                                "",         // 640k
                                "",         // 510k
                                "",         // 500k
                                "",         // 448k
                                "",         // 400k
                                "",         // 320k
                                "",         // 256k
                                "",         // 224k
                                "",         // 192k
                                "",         // 160k
                                "",         // 128k
                                ""          // 96k
                            );
                }

                // -------------------------
                // LAME
                // -------------------------
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "LAME")
                {
                    AudioQualityPresets(
                                mainwindow,
                                // Quality Selected
                                (string)mainwindow.cboAudioQuality.SelectedItem,

                                // VBR Toggle
                                (bool)mainwindow.tglAudioVBR.IsChecked,

                                // Lossless
                                "",   

                                // CBR
                                "",         // 640k
                                "",         // 510k
                                "",         // 500k
                                "",         // 448k
                                "",         // 400k
                                "320k",     // 320k
                                "256k",     // 256k
                                "224k",     // 224k
                                "192k",     // 192k
                                "160k",     // 160k
                                "128k",     // 128k
                                "96k",      // 96k

                                // VBR
                                "",         // 640k
                                "",         // 510k
                                "",         // 500k
                                "",         // 448k
                                "",         // 400k
                                "0",        // 320k
                                "0",        // 256k
                                "1",        // 224k
                                "2",        // 192k
                                "3",        // 160k
                                "5",        // 128k
                                "7"         // 96k
                            );
                }

                // -------------------------
                // FLAC
                // -------------------------
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC")
                {
                    AudioQualityPresets(
                                mainwindow,
                                // Quality Selected
                                (string)mainwindow.cboAudioQuality.SelectedItem,

                                // VBR Toggle
                                (bool)mainwindow.tglAudioVBR.IsChecked,

                                // Lossless
                                "",   

                                // CBR
                                "",         // 640k
                                "",         // 510k
                                "",         // 500k
                                "",         // 448k
                                "",         // 400k
                                "320k",     // 320k
                                "256k",     // 256k
                                "224k",     // 224k
                                "192k",     // 192k
                                "160k",     // 160k
                                "128k",     // 128k
                                "96k",      // 96k

                                // VBR
                                "",         // 640k
                                "",         // 510k
                                "",         // 500k
                                "",         // 448k
                                "",         // 400k
                                "",         // 320k
                                "",         // 256k
                                "",         // 224k
                                "",         // 192k
                                "",         // 160k
                                "",         // 128k
                                ""          // 96k
                            );
                }

                // -------------------------
                // ALAC
                // -------------------------
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC")
                {
                    AudioQualityPresets(
                                mainwindow,
                                // Quality Selected
                                (string)mainwindow.cboAudioQuality.SelectedItem,

                                // VBR Toggle
                                (bool)mainwindow.tglAudioVBR.IsChecked,

                                // Lossless
                                "",   

                                // CBR
                                "",         // 640k
                                "",         // 510k
                                "",         // 500k
                                "",         // 448k
                                "",         // 400k
                                "320k",     // 320k
                                "256k",     // 256k
                                "224k",     // 224k
                                "192k",     // 192k
                                "160k",     // 160k
                                "128k",     // 128k
                                "96k",      // 96k

                                // VBR
                                "",         // 640k
                                "",         // 510k
                                "",         // 500k
                                "",         // 448k
                                "",         // 400k
                                "",         // 320k
                                "",         // 256k
                                "",         // 224k
                                "",         // 192k
                                "",         // 160k
                                "",         // 128k
                                ""          // 96k
                            );
                }

                // -------------------------
                // PCM
                // -------------------------
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
                {
                    AudioQualityPresets(
                                mainwindow,
                                // Quality Selected
                                (string)mainwindow.cboAudioQuality.SelectedItem,

                                // VBR Toggle
                                (bool)mainwindow.tglAudioVBR.IsChecked,

                                // Lossless
                                "",   

                                // CBR
                                "",         // 640k
                                "",         // 510k
                                "",         // 500k
                                "",         // 448k
                                "",         // 400k
                                "320k",     // 320k
                                "256k",     // 256k
                                "224k",     // 224k
                                "192k",     // 192k
                                "160k",     // 160k
                                "128k",     // 128k
                                "96k",      // 96k

                                // VBR
                                "",         // 640k
                                "",         // 510k
                                "",         // 500k
                                "",         // 448k
                                "",         // 400k
                                "",         // 320k
                                "",         // 256k
                                "",         // 224k
                                "",         // 192k
                                "",         // 160k
                                "",         // 128k
                                ""          // 96k
                            );
                }

                // -------------------------
                // Copy
                // -------------------------
                else if ((string)mainwindow.cboAudioCodec.SelectedItem == "Copy")
                {
                    AudioQualityPresets(
                                mainwindow,
                                // Quality Selected
                                (string)mainwindow.cboAudioQuality.SelectedItem,

                                // VBR Toggle
                                (bool)mainwindow.tglAudioVBR.IsChecked,

                                // Lossless
                                "",   

                                // CBR
                                "",         // 640k
                                "",         // 510k
                                "",         // 500k
                                "",         // 448k
                                "",         // 400k
                                "",         // 320k
                                "",         // 256k
                                "",         // 224k
                                "",         // 192k
                                "",         // 160k
                                "",         // 128k
                                "",         // 96k

                                // VBR
                                "",         // 640k
                                "",         // 510k
                                "",         // 500k
                                "",         // 448k
                                "",         // 400k
                                "",         // 320k
                                "",         // 256k
                                "",         // 224k
                                "",         // 192k
                                "",         // 160k
                                "",         // 128k
                                ""          // 96k
                            );
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
                                            .Where(s => !s.Equals(Environment.NewLine))
                                            .Where(s => !s.Equals("\n"))
                                 );


                // Log Console Message /////////
                // CBR
                if (mainwindow.tglAudioVBR.IsChecked == false)
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
                else if (mainwindow.tglAudioVBR.IsChecked == true)
                {
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run("CBR " + mainwindow.cboAudioQuality.SelectedItem.ToString() + " to VBR " + aBitrate) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }

            } // end null check


            // Return Value
            return aQuality;

        } // end AudioQuality


        /// <summary>
        /// Channel (Method)
        /// <summary>
        public static String Channel(MainWindow mainwindow)
        {
            // Audio Bitrate None Check
            // Audio Codec None Check
            // Codec Copy Check
            // Mute Check
            // Stream None Check
            // Media Type Check
            if ((string)mainwindow.cboAudioQuality.SelectedItem != "None"
                && (string)mainwindow.cboAudioCodec.SelectedItem != "None"
                && (string)mainwindow.cboAudioCodec.SelectedItem != "Copy"
                && (string)mainwindow.cboAudioQuality.SelectedItem != "Mute"
                && (string)mainwindow.cboAudioStream.SelectedItem != "none"
                && (string)mainwindow.cboMediaType.SelectedItem != "Image"
                && (string)mainwindow.cboMediaType.SelectedItem != "Sequence")
            {
                // Auto
                if ((string)mainwindow.cboChannel.SelectedItem == "Auto")
                {
                    aChannel = string.Empty;
                }
                // Mono
                else if ((string)mainwindow.cboChannel.SelectedItem == "Mono")
                {
                    aChannel = "-ac 1";
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
                // 5.1
                else if ((string)mainwindow.cboChannel.SelectedItem == "5.1")
                {
                    aChannel = "-ac 6";
                }


                // Prevent Downmix Clipping
                //
                if ((string)mainwindow.cboChannel.SelectedItem != "Source")
                {
                    aChannel = "-rematrix_maxval 1.0 " + aChannel;
                }


                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Channel: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(mainwindow.cboChannel.Text.ToString()) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            // Return Value
            return aChannel;
        }


        /// <summary>
        /// Sample Rate (Method)
        /// <summary>
        public static String SampleRate(MainWindow mainwindow)
        {
            // Audio Bitrate None Check
            // Audio Codec None
            // Codec Copy Check
            // Mute Check
            // Stream None Check
            // Media Type Check
            if ((string)mainwindow.cboAudioQuality.SelectedItem != "None"
                && (string)mainwindow.cboAudioCodec.SelectedItem != "None"
                && (string)mainwindow.cboAudioCodec.SelectedItem != "Copy"
                && (string)mainwindow.cboAudioQuality.SelectedItem != "Mute"
                && (string)mainwindow.cboAudioStream.SelectedItem != "none"
                && (string)mainwindow.cboMediaType.SelectedItem != "Image"
                && (string)mainwindow.cboMediaType.SelectedItem != "Sequence")
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

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Sample Rate: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(mainwindow.cboSamplerate.Text.ToString()) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }


            // Return Value
            return aSamplerate;
        }


        /// <summary>
        /// Bit Depth (Method)
        /// <summary>
        public static String BitDepth(MainWindow mainwindow)
        {
            // Audio Bitrate None Check
            // Audio Codec None
            // Codec Copy Check
            // Mute Check
            // Stream None Check
            // Media Type Check
            if ((string)mainwindow.cboAudioQuality.SelectedItem != "None"
                && (string)mainwindow.cboAudioCodec.SelectedItem != "None"
                && (string)mainwindow.cboAudioCodec.SelectedItem != "Copy"
                && (string)mainwindow.cboAudioQuality.SelectedItem != "Mute"
                && (string)mainwindow.cboAudioStream.SelectedItem != "none"
                && (string)mainwindow.cboMediaType.SelectedItem != "Image"
                && (string)mainwindow.cboMediaType.SelectedItem != "Sequence")
            {
                // PCM has Bitdepth defined by Codec instead of sample_fmt, can use 8, 16, 24, 32, 64-bit
                // FLAC can only use 16 and 32-bit
                // ALAC can only use 16 and 32-bit

                // -------------------------
                // auto
                // -------------------------
                if ((string)mainwindow.cboBitDepth.SelectedItem == "auto")
                {
                    if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
                    {
                        aBitDepth = string.Empty;
                        aCodec = "-c:a pcm_s24le";
                    }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC")
                    {
                        aBitDepth = string.Empty;
                    }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC")
                    {
                        aBitDepth = string.Empty;
                    }
                    // all other codecs
                    else
                    {
                        aBitDepth = string.Empty;
                    } 
                }
                // -------------------------
                // 8
                // -------------------------
                else if ((string)mainwindow.cboBitDepth.SelectedItem == "8")
                {
                    if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
                    {
                        aBitDepth = string.Empty;
                        aCodec = "-c:a pcm_u8";
                    }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC")
                    {
                        aBitDepth = string.Empty;
                    }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC")
                    {
                        aBitDepth = string.Empty;
                    }
                    // all other codecs
                    else
                    {
                        aBitDepth = string.Empty;
                    } 
                }
                // -------------------------
                // 16
                // -------------------------
                else if ((string)mainwindow.cboBitDepth.SelectedItem == "16")
                {
                    if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
                    {
                        aBitDepth = string.Empty;
                        aCodec = "-c:a pcm_s16le";
                    }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC")
                    {
                        aBitDepth = "-sample_fmt s16";
                    }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC")
                    {
                        aBitDepth = "-sample_fmt s16p";
                    }
                    // all other codecs
                    else
                    {
                        aBitDepth = string.Empty;
                    } 
                }
                // -------------------------
                // 24
                // -------------------------
                else if ((string)mainwindow.cboBitDepth.SelectedItem == "24")
                {
                    if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
                    {
                        aBitDepth = string.Empty;
                        aCodec = "-c:a pcm_s24le";
                    }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC")
                    {
                        aBitDepth = string.Empty; }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC")
                    {
                        aBitDepth = string.Empty;
                    }
                    // all other codecs
                    else
                    {
                        aBitDepth = string.Empty;
                    } 
                }
                // -------------------------
                // 32
                // -------------------------
                else if ((string)mainwindow.cboBitDepth.SelectedItem == "32")
                {
                    if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
                    {
                        aBitDepth = string.Empty;
                        aCodec = "-c:a pcm_f32le";
                    }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC")
                    {
                        aBitDepth = "-sample_fmt s32";
                    }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC")
                    {
                        aBitDepth = "-sample_fmt s32p";
                    }
                    // all other codecs
                    else
                    {
                        aBitDepth = string.Empty;
                    } 
                }
                // -------------------------
                // 64
                // -------------------------
                else if ((string)mainwindow.cboBitDepth.SelectedItem == "64")
                {
                    if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
                    {
                        aBitDepth = string.Empty;
                        aCodec = "-c:a pcm_f64le";
                    }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "FLAC")
                    {
                        aBitDepth = string.Empty;
                    }
                    else if ((string)mainwindow.cboAudioCodec.SelectedItem == "ALAC")
                    {
                        aBitDepth = string.Empty;
                    }
                    // all other codecs
                    else
                    {
                        aBitDepth = string.Empty;
                    } 
                }

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Bit Depth: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(mainwindow.cboBitDepth.SelectedItem.ToString()) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }


            // Return Value
            return aBitDepth;
        }


        /// <summary>
        ///     Volume (Method)
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
                    AudioFilters.aFiltersList.Add(aVolume);
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
        ///     HardLimiter Filter (Method)
        /// <summary>
        public static void HardLimiter(MainWindow mainwindow)
        {
            double value = mainwindow.slAudioLimiter.Value;

            // If enabled and not default
            if (mainwindow.slAudioLimiter.IsEnabled == true 
                && value != 1)
            {
                aLimiter = "alimiter=level_in=1:level_out=1:limit=" + Convert.ToString(Math.Round(value, 2)) + ":attack=7:release=100:level=disabled";

                // Add to Audio Filters
                AudioFilters.aFiltersList.Add(aLimiter);
            }



            //// -------------------------
            //// On
            //// -------------------------
            //if (mainwindow.tglAudioLimiter.IsChecked == true) 
            //{
            //    // Log Console Message /////////
            //    Log.WriteAction = () =>
            //    {
            //        Log.logParagraph.Inlines.Add(new LineBreak());
            //        Log.logParagraph.Inlines.Add(new Bold(new Run("Hard Limiter Toggle: ")) { Foreground = Log.ConsoleDefault });
            //        Log.logParagraph.Inlines.Add(new Run("On") { Foreground = Log.ConsoleDefault });
            //    };
            //    Log.LogActions.Add(Log.WriteAction);

            //    aLimiter = "alimiter=level_in=1:level_out=1:limit=" + mainwindow.audioLimiter.Text + ":attack=7:release=100:level=disabled";

            //    // Audio Filter Add
            //    AudioFilters.Add(aLimiter);
            //}

            //// -------------------------
            //// Off
            //// -------------------------
            //else if (mainwindow.tglAudioLimiter.IsChecked == false)
            //{
            //    // Log Console Message /////////
            //    Log.WriteAction = () =>
            //    {
            //        Log.logParagraph.Inlines.Add(new LineBreak());
            //        Log.logParagraph.Inlines.Add(new Bold(new Run("alimiter Toggle: ")) { Foreground = Log.ConsoleDefault });
            //        Log.logParagraph.Inlines.Add(new Run("Off") { Foreground = Log.ConsoleDefault });
            //    };
            //    Log.LogActions.Add(Log.WriteAction);

            //    aLimiter = string.Empty;
            //}
        }
        

    }
}