﻿/* ----------------------------------------------------------------------
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

 * BitRate Mode
 * Audio Quality
 * Audio BitRate Calculator
 * Audio VBR Calculator
 * Sample Rate
 * Bit Depth
 * Batch Audio BitRate Limiter
 * Batch Audio Quality Auto
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
using System.Collections.ObjectModel;
using System.Globalization;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Generate
{
    namespace Audio
    {
        public class Quality
        {
            public static string aBitMode { get; set; } // -b:a, -q:a
            public static string aBitRate { get; set; }
            public static string aBitRateNA { get; set; } // fallback default if not available
            public static string aQuality { get; set; }
            public static string aLossless { get; set; }
            public static string aCompressionLevel { get; set; }
            public static string aSamplerate { get; set; }
            public static string aBitDepth { get; set; }
            public static string batchAudioAuto { get; set; }

            /// <summary>
            /// BitRate Mode
            /// <summary>
            public static String BitRateMode(bool vbr_IsChecked,
                                             ObservableCollection<ViewModel.Audio.AudioQuality> quality_Items,
                                             string quality_SelectedItem,
                                             string bitrate_Text)
            {
                // Only if BitRate Textbox is not Empty (except for Auto Quality)
                if (quality_SelectedItem == "Auto" ||
                    !string.IsNullOrWhiteSpace(bitrate_Text)
                    )
                {
                    // CBR
                    if (vbr_IsChecked == false)
                    {
                        //aBitmode = "-b:a";
                        aBitMode = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CBR_BitMode;
                    }

                    // VBR
                    else if (vbr_IsChecked == true)
                    {
                        //aBitmode = "-q:a";
                        aBitMode = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.VBR_BitMode;
                    }
                }

                return aBitMode;
            }



            /// <summary>
            /// Audio Quality - Auto
            /// <summary>
            public static void QualityAuto(string input_Text,
                                           bool batch_IsChecked,
                                           string mediaType_SelectedItem,
                                           string stream_SelectedItem,
                                           string codec_SelectedItem,
                                           ObservableCollection<ViewModel.Audio.AudioQuality> quality_Items,
                                           string quality_SelectedItem,
                                           bool vbr_IsChecked
                                           )
            {
                // No Detectable BitRate Default
                aBitRateNA = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.NA;

                // --------------------------------------------------
                // Single
                // --------------------------------------------------
                if (batch_IsChecked == false)
                {
                    // -------------------------
                    // Input Has Audio
                    // -------------------------
                    if (!string.IsNullOrWhiteSpace(Analyze.FFprobe.inputAudioBitRate))
                    {
                        //MessageBox.Show(FFprobe.inputAudioBitRate); //debug

                        // Input BitRate was detected
                        if (Analyze.FFprobe.inputAudioBitRate != "N/A")
                        {
                            // CBR
                            if (vbr_IsChecked == false)
                            {
                                // Parse FFprobe Audio Bit Rate
                                int inputAudioBitRate_Int = 320000; // Fallback
                                int.TryParse(Analyze.FFprobe.inputAudioBitRate, out inputAudioBitRate_Int);

                                // aBitMode = "-b:a";
                                aBitRate = AudioBitRateCalculator(codec_SelectedItem,
                                                                  Analyze.FFprobe.aEntryType,
                                                                  // Limit FFprobe Input Audio BitRate if higher than Codec's maximum bit rate
                                                                  AudioBitRateLimiter(codec_SelectedItem,
                                                                                      quality_SelectedItem,
                                                                                      inputAudioBitRate_Int
                                                                                      )
                                                                                      .ToString()
                                                                  );
                            }

                            // VBR
                            else if (vbr_IsChecked == true)
                            {
                                //VBR does not have 'k'
                                // aBitMode = "-q:a";
                                aBitRate = AudioVBRCalculator(vbr_IsChecked,
                                                              codec_SelectedItem,
                                                              quality_Items,
                                                              quality_SelectedItem,
                                                              Analyze.FFprobe.inputAudioBitRate // VBR Birate Limiter is handled in VBR Calculator
                                                              );
                            }
                        }

                        // -------------------------
                        // Input Does Not Have Audio Codec
                        // -------------------------
                        string inputExt = Path.GetExtension(VM.MainView.Input_Text); //.ToLower()
                        string outputExt = "." + VM.FormatView.Format_Container_SelectedItem; //.ToLower()

                        if (!string.IsNullOrWhiteSpace(Analyze.FFprobe.inputAudioCodec))
                        {
                            // Default to a new bitrate if Input & Output formats Do Not match
                            if (Analyze.FFprobe.inputAudioBitRate == "N/A" &&
                                //inputExt != outputExt
                                !string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase)
                                )
                            {
                                // Default to NA value
                                if (!string.IsNullOrWhiteSpace(aBitRateNA))
                                {
                                    //aBitRate = aBitRateNA;

                                    switch (vbr_IsChecked)
                                    {
                                        // -------------------------
                                        // CBR
                                        // -------------------------
                                        case false:
                                            aBitRate = AudioBitRateCalculator(codec_SelectedItem,
                                                                              Analyze.FFprobe.aEntryType,
                                                                              aBitRateNA
                                                                              );
                                            break;

                                        // -------------------------
                                        // VBR
                                        // -------------------------
                                        case true:
                                            //VBR does not have 'k'
                                            aBitRate = AudioVBRCalculator(vbr_IsChecked,
                                                                          codec_SelectedItem,
                                                                          quality_Items,
                                                                          quality_SelectedItem,
                                                                          aBitRateNA
                                                                          );
                                            break;
                                    }
                                }
                                // Default to 320k if NA value is empty
                                else
                                {
                                    aBitRate = "320";
                                }
                            }
                        }
                    }

                    // -------------------------
                    // Input No Audio
                    // -------------------------
                    else if (string.IsNullOrWhiteSpace(Analyze.FFprobe.inputAudioBitRate))
                    {
                        aBitMode = string.Empty;
                        aBitRate = string.Empty;
                    }

                    // -------------------------
                    // Input TextBox is Empty - Auto Value
                    // -------------------------
                    if (string.IsNullOrWhiteSpace(input_Text))
                    {
                        aBitMode = "-b:a";
                        //aBitRate = "320";
                        aBitRate = quality_Items.FirstOrDefault(item => item.Name == "Auto")?.NA;
                    }

                    // -------------------------
                    // BitRate is 0 (Happens with NA)
                    // -------------------------
                    if (aBitRate == "0" &&
                        vbr_IsChecked == false) // Ignore VBR, Some Codecs use VBR 0
                    {
                        aBitMode = string.Empty;
                        aBitRate = string.Empty;
                    }

                    // -------------------------
                    // YouTube Download
                    // -------------------------
                    // Can't detect bitrate from URL
                    if (MainWindow.IsWebURL(input_Text) == true)
                    {
                        aBitMode = "-b:a";
                        aBitRate = "320";
                    }

                    // -------------------------
                    // BitRate Returned Empty, Disable BitMode
                    // -------------------------
                    if (string.IsNullOrWhiteSpace(aBitRate))
                    {
                        aBitMode = string.Empty;
                    }
                }

                // --------------------------------------------------
                // Batch
                // --------------------------------------------------
                else if (batch_IsChecked == true)
                {
                    // Use the CMD Batch Audio Variable
                    //aBitRate = "%A";

                    // Shell Check
                    switch (VM.ConfigureView.Shell_SelectedItem)
                    {
                        // CMD
                        case "CMD":
                            // BitMode is VBR
                            if (aBitMode == "-q:a")
                            {
                                // Use the codec database value
                                aBitRate = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.VBR;
                            }
                            else
                            {
                                // Use CMD variable
                                aBitRate = "%A";
                            }
                            break;

                        // PowerShell
                        case "PowerShell":
                            // BitMode is VBR
                            if (aBitMode == "-q:a")
                            {
                                // Use the codec database value
                                aBitRate = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.VBR;
                            }
                            else
                            {
                                // Use PowerShell variable
                                aBitRate = "$aBitrate";
                            }
                            break;
                    }

                    //MessageBox.Show(aBitRate); //debug
                }
            }


            /// <summary>
            /// Audio Quality - Lossless
            /// <summary>
            public static void QualityLossless(ObservableCollection<ViewModel.Audio.AudioQuality> quality_Items)
            {
                aLossless = quality_Items.FirstOrDefault(item => item.Name == "Lossless")?.Lossless;
            }


            /// <summary>
            /// Audio Quality - Preset
            /// <summary>
            public static void QualityPreset(string bitrate_Text)
            {
                // -------------------------
                // BitRate
                // -------------------------
                aBitRate = bitrate_Text;
            }


            /// <summary>
            /// Audio Quality - Custom
            /// <summary>
            public static void QualityCustom(bool vbr_IsChecked,
                                             string codec_SelectedItem,
                                             ObservableCollection<ViewModel.Audio.AudioQuality> quality_Items,
                                             string quality_SelectedItem,
                                             string bitrate_Text
                                             )
            {
                // --------------------------------------------------
                // BitRate
                // --------------------------------------------------
                // -------------------------
                // CBR
                // -------------------------
                if (vbr_IsChecked == false)
                {
                    // .e.g. 320k
                    aBitRate = bitrate_Text;
                }

                // -------------------------
                // VBR
                // -------------------------
                else if (vbr_IsChecked == true)
                {
                    // e.g. 320k converted to -q:a 2
                    aBitRate = AudioVBRCalculator(vbr_IsChecked,
                                                  codec_SelectedItem,
                                                  quality_Items,
                                                  quality_SelectedItem,
                                                  bitrate_Text
                                                  );
                }
            }


            /// <summary>
            /// Audio Quality
            /// <summary>
            public static String AudioQuality(string input_Text,
                                              bool batch_IsChecked,
                                              string mediaType_SelectedItem,
                                              string stream_SelectedItem,
                                              string codec_SelectedItem,
                                              ObservableCollection<ViewModel.Audio.AudioQuality> quality_Items,
                                              string quality_SelectedItem,
                                              string bitrate_Text,
                                              bool vbr_IsChecked
                                              )
            {
                // Check:
                // Audio Codec Not Copy
                if (codec_SelectedItem != "Copy")
                {
                    // BitRate Mode
                    aBitMode = BitRateMode(vbr_IsChecked,
                                           quality_Items,
                                           quality_SelectedItem,
                                           bitrate_Text
                                           );

                    if (!string.IsNullOrWhiteSpace(aBitMode)) // Null Check
                    {
                        switch (quality_SelectedItem)
                        {
                            // -------------------------
                            // Auto
                            // -------------------------
                            case "Auto":
                                QualityAuto(input_Text,
                                            batch_IsChecked,
                                            mediaType_SelectedItem,
                                            stream_SelectedItem,
                                            codec_SelectedItem,
                                            quality_Items,
                                            quality_SelectedItem,
                                            vbr_IsChecked
                                            );
                                break;

                            // -------------------------
                            // Lossless
                            // -------------------------
                            case "Lossless":
                                QualityLossless(quality_Items);
                                break;

                            // -------------------------
                            // Custom
                            // -------------------------
                            case "Custom":
                                QualityCustom(vbr_IsChecked,
                                              codec_SelectedItem,
                                              quality_Items,
                                              quality_SelectedItem,
                                              bitrate_Text);
                                break;

                            // -------------------------
                            // Preset: 640, 400, 320, 128, etc
                            // -------------------------
                            default:
                                // Preset & Custom
                                QualityPreset(bitrate_Text);
                                break;
                        }

                        // --------------------------------------------------
                        // Add kbps
                        // --------------------------------------------------
                        if (!string.IsNullOrWhiteSpace(aBitRate) && // Bit Rate Empty Check
                            aBitMode != "-q:a" &&              // Ignore VBR
                            aBitRate != "%A" &&                // Ignore Batch Auto Quality CBR
                            aBitRate != "$aBitrate"            // Ignore Batch Auto Quality CBR
                            )
                        {
                            //aBitRate = aBitRate + "k";
                            aBitRate += "k";
                        }


                        // --------------------------------------------------
                        // Combine Options
                        // --------------------------------------------------
                        IEnumerable<string> aQualityArgs = new List<string>()
                        {
                            aBitMode,
                            aBitRate
                        };

                        // Quality
                        aQuality = string.Join(" ", aQualityArgs
                                                    .Where(s => !string.IsNullOrWhiteSpace(s))
                                                    .Where(s => !s.Equals("\n"))
                                              );


                        // --------------------------------------------------
                        // Format European English comma to US English peroid - 1,234 to 1.234
                        // --------------------------------------------------
                        aQuality = string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:0.0}", aQuality);
                    }

                }

                return aQuality;
            }



            /// <summary>
            /// Audio BitRate Calculator
            /// <summary>
            public static String AudioBitRateCalculator(string codec_SelectedItem,
                                                        string aEntryType,
                                                        string inputAudioBitRate
                                                        )
            {
                try
                {
                    // -------------------------
                    // If Video is has no Audio, don't set Audio BitRate
                    // -------------------------
                    if (!string.IsNullOrWhiteSpace(Analyze.FFprobe.inputAudioBitRate))
                    {
                        // -------------------------
                        // Filter out any extra spaces after the first 3 characters IMPORTANT
                        // -------------------------
                        if (inputAudioBitRate.Substring(0, 3) == "N/A")
                        {
                            //MessageBox.Show("1"); //debug
                            inputAudioBitRate = "N/A";
                        }
                    }

                    // -------------------------
                    // If Video has Audio, calculate BitRate into decimal
                    // -------------------------
                    if (inputAudioBitRate != "N/A" &&
                        !string.IsNullOrWhiteSpace(Analyze.FFprobe.inputAudioBitRate))
                    {
                        // -------------------------
                        // Convert inputAudioBitRate to Double
                        // -------------------------
                        double inputAudioBitRate_Double = 0; // Fallback
                        double.TryParse(inputAudioBitRate, out inputAudioBitRate_Double);

                        // -------------------------
                        // Convert FFprobe inputAudioBitRate byte Value to kb
                        // e.g. 320000 -> 320
                        // -------------------------
                        inputAudioBitRate_Double = inputAudioBitRate_Double * 0.001;

                        // -------------------------
                        // Apply Bit Rate Min/Max Limits
                        // -------------------------
                        switch (codec_SelectedItem)
                        {
                            // -------------------------
                            // Vorbis
                            // -------------------------
                            case "Vorbis":
                                // Min
                                if (inputAudioBitRate_Double < 45)
                                {
                                    inputAudioBitRate = "45";
                                }
                                // Max
                                else if (inputAudioBitRate_Double > 500)
                                {
                                    inputAudioBitRate = "500";
                                }
                                break;

                            // -------------------------
                            // Opus
                            // -------------------------
                            case "Opus":
                                // Min
                                if (inputAudioBitRate_Double < 6)
                                {
                                    inputAudioBitRate = "6";
                                }
                                // Max
                                else if (inputAudioBitRate_Double > 510)
                                {
                                    inputAudioBitRate = "510"; 
                                }
                                break;

                            // -------------------------
                            // MP2
                            // -------------------------
                            case "MP2":
                                if (inputAudioBitRate_Double > 384)
                                {
                                    inputAudioBitRate = "384"; 
                                }
                                break;

                            // -------------------------
                            // LAME
                            // -------------------------
                            case "LAME":
                                if (inputAudioBitRate_Double > 320)
                                {
                                    inputAudioBitRate = "320"; 
                                }
                                break;

                            // -------------------------
                            // AC3
                            // -------------------------
                            case "AC3":
                                if (inputAudioBitRate_Double > 640)
                                {
                                    inputAudioBitRate = "640"; 
                                }
                                break;

                            // -------------------------
                            // AAC
                            // -------------------------
                            case "AAC":
                                if (inputAudioBitRate_Double > 400)
                                {
                                    inputAudioBitRate = "400"; 
                                }
                                break;

                            // -------------------------
                            // DTS
                            // -------------------------
                            case "DTS":
                                if (inputAudioBitRate_Double > 1509)
                                {
                                    inputAudioBitRate = "1509";
                                }
                                break;

                            // -------------------------
                            // ALAC, FLAC, PCM
                            // -------------------------
                            // Does not need limiter
                        }

                        // -------------------------
                        // Remove Decimals
                        // -------------------------
                        inputAudioBitRate = string.Format("{0:0.#}", inputAudioBitRate_Double);

                        //// -------------------------
                        //// Round Bitrate
                        //// -------------------------
                        //inputAudioBitRate = Math.Round(inputAudioBitRate_Double).ToString();
                    }
                }

                // -------------------------
                // Error
                // -------------------------
                catch
                {
                    MessageBox.Show("Problem calculating Audio BitRate.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }

                return inputAudioBitRate;
            }


            /// <summary>
            /// Audio VBR Calculator
            /// <summary>
            public static String AudioVBRCalculator(bool vbr_IsChecked,
                                                    string codec_SelectedItem,
                                                    ObservableCollection<ViewModel.Audio.AudioQuality> quality_Items,
                                                    string quality_SelectedItem,
                                                    string inputBitRate
                                                    )
            {
                //MessageBox.Show(inputBitRate); //debug
                //MessageBox.Show(codec_SelectedItem); //debug

                // -------------------------
                // VBR 
                // User entered value
                // -------------------------
                if (vbr_IsChecked == true)
                {
                    // Used to Calculate VBR Double
                    //
                    //double aBitRateVBR = double.Parse(inputBitRate);
                    double aBitRate_Double = 320000; // Fallback
                    double.TryParse(inputBitRate, out aBitRate_Double);

                    double aBitRateVBR = 0;

                    // If inputBitRate is from Auto Quality FFprobe Parse, divide by 1000 to get smaller value
                    // e.g. (320000 to 320)
                    // If inputBirate is from Custom Quality User Input value, leave the same
                    if (quality_SelectedItem == "Auto")
                    {
                        //aBitRateVBR = aBitRateVBR / 1000;
                        aBitRate_Double = aBitRate_Double / 1000;
                    }

                    switch (codec_SelectedItem)
                    {
                        // -------------------------
                        // Vorbis
                        // -------------------------
                        case "Vorbis":
                            // VBR User entered value algorithm (0 low / 10 high)

                            double inputMin = 0;   // kbps
                            double inputMax = 320; // kbps
                            double normMin = 0;    // low
                            double normMax = 10;   // high

                            aBitRateVBR = Math.Round(
                                                MainWindow.NormalizeValue(
                                                                aBitRate_Double, // input
                                                                inputMin,    // input min
                                                                inputMax,    // input max
                                                                normMin,     // normalize min
                                                                normMax,     // normalize max
                                                                (normMin + normMax) / 2 // midpoint average
                                                            )

                                                        , 5 // max decimal places
                                                );
                            break;

                        // -------------------------
                        // Opus
                        // -------------------------
                        case "Opus":
                            // Above 510k set to 256k
                            if (aBitRate_Double > 510)
                            {
                                // e.g. -vbr on -b:a 510k -compression_level 10 
                                aBitRateVBR = Convert.ToDouble(quality_Items.FirstOrDefault(item => item.Name == "Auto")?.VBR);
                            }
                            else
                            {
                                // Use the original -b:a bitrate detected by FFprobe (e.g. -b:a 123k)
                                // Round to remove decimals
                                // e.g. -vbr on -b:a 123k -compression_level 10 
                                aBitRateVBR = Math.Round(aBitRate_Double);
                            }
                            break;

                        // -------------------------
                        // AAC
                        // -------------------------
                        case "AAC":
                            // Range 
                            // Above 320k 
                            if (aBitRate_Double > 400)
                            {
                                aBitRateVBR = Convert.ToDouble(quality_Items.FirstOrDefault(item => item.Name == "Auto")?.VBR);
                            }
                            else
                            {
                                // VBR User entered value algorithm (0.1 low / 2 high)
                                aBitRateVBR = Math.Min(2, Math.Max(aBitRate_Double * 0.00625, 0.1));
                            }
                            break;

                        // -------------------------
                        // MP2
                        // -------------------------
                        case "MP2":
                            // Above 384k set to V0
                            if (aBitRate_Double > 384)
                            {
                                aBitRateVBR = Convert.ToDouble(quality_Items.FirstOrDefault(item => item.Name == "Auto")?.VBR);
                            }
                            else
                            {
                                // VBR User entered value algorithm (10 low / 0 high)
                                aBitRateVBR = (((aBitRate_Double * (-0.01)) / 2.60) + 1) * 10;
                            }
                            break;

                        // -------------------------
                        // LAME MP3
                        // -------------------------
                        case "LAME":
                            // Above 320k set to V0
                            if (aBitRate_Double > 320)
                            {
                                aBitRateVBR = Convert.ToDouble(quality_Items.FirstOrDefault(item => item.Name == "Auto")?.VBR);
                            }
                            else
                            {
                                // VBR User entered value algorithm (10 low / 0 high)
                                aBitRateVBR = (((aBitRate_Double * (-0.01)) / 2.60) + 1) * 10;
                            }
                            break;
                    }

                    // -------------------------
                    // Convert to String for aQuality Combine
                    // -------------------------
                    aBitRate = Convert.ToString(aBitRateVBR);

                    // -------------------------
                    // Limit to 5 Decimal Places
                    // -------------------------
                    //aBitRate = string.Format("{0:0.#####}", Convert.ToDouble(aBitRate));
                    aBitRate = Convert.ToDouble(aBitRate).ToString("0.#####", CultureInfo.GetCultureInfo("en-US"));
                }

                //MessageBox.Show(aBitRate); //debug

                return aBitRate;
            }

            /// <summary>
            /// Compression Level
            /// <summary>
            public static String CompressionLevel(string codec_SelectedItem,
                                                  string compressionLevel_SelectedItem
                                                 )
            {
                // Check:
                // Audio Codec Not Copy
                if (codec_SelectedItem != "Copy")
                {
                    // -------------------------
                    // none
                    // default
                    // -------------------------
                    if (compressionLevel_SelectedItem == "none" ||
                        compressionLevel_SelectedItem == "auto" ||
                        string.IsNullOrWhiteSpace(compressionLevel_SelectedItem)
                        )
                    {
                        aCompressionLevel = string.Empty;
                    }
                    // -------------------------
                    // value
                    // -------------------------
                    else
                    {
                        // e.g. -compression_level 5
                        aCompressionLevel = "-compression_level " + compressionLevel_SelectedItem;
                    }


                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Compression Level: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run(compressionLevel_SelectedItem) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }

                // Return Value
                return aCompressionLevel;
            }



            /// <summary>
            /// Sample Rate
            /// <summary>
            public static String SampleRate(string codec_SelectedItem,
                                            ObservableCollection<ViewModel.Audio.AudioSampleRate> sampleRate_Items,
                                            string sampleRate_SelectedItem
                                            )
            {
                // Check:
                // Audio Codec Not Copy
                if (codec_SelectedItem != "Copy")
                {
                    // Auto
                    if (sampleRate_SelectedItem == "auto")
                    {
                        aSamplerate = string.Empty;
                    }
                    // All other Sample Rates
                    else
                    {
                        aSamplerate = "-ar " + sampleRate_Items.FirstOrDefault(item => item.Name == sampleRate_SelectedItem)?.Frequency;
                    }

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Sample Rate: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run(sampleRate_SelectedItem) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }

                // Return Value
                return aSamplerate;
            }


            /// <summary>
            /// Bit Depth
            /// <summary>
            public static String BitDepth(string codec_SelectedItem,
                                          ObservableCollection<ViewModel.Audio.AudioBitDepth> bitDepth_Items,
                                          string bitDepth_SelectedItem
                                         )
            {
                // Check:
                // Audio Codec Not Copy
                if (codec_SelectedItem != "Copy")
                {
                    // PCM has Bitdepth defined by Codec instead of sample_fmt, can use 8, 16, 24, 32, 64-bit
                    // FLAC can only use 16 and 32-bit
                    // ALAC can only use 16 and 32-bit

                    // -------------------------
                    // auto
                    // -------------------------
                    if (bitDepth_SelectedItem == "auto")
                    {
                        aBitDepth = string.Empty;
                    }
                    // -------------------------
                    // All Other Bit Depths
                    // -------------------------
                    else
                    {
                        aBitDepth = bitDepth_Items.FirstOrDefault(item => item.Name == bitDepth_SelectedItem)?.Depth;
                    }

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Bit Depth: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run(bitDepth_SelectedItem) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }


                // Return Value
                return aBitDepth;
            }



            /// <summary>
            /// Audio BitRate Limiter (Method)
            /// <summary>
            public static int AudioBitRateLimiter(string codec_SelectedItem,
                                                  string quality_SelectedItem,
                                                  int aBitRateLimit
                                                 )
            {
                // Check:
                // Audio Codec Not Copy
                if (codec_SelectedItem != "Copy")
                {
                    // -------------------------
                    // Batch Limit BitRates
                    // -------------------------
                    // Only if Audio Quality Auto
                    if (quality_SelectedItem == "Auto")
                    {
                        //try
                        //{
                        switch (codec_SelectedItem)
                        {
                            // Vorbis
                            case "Vorbis":
                                if (aBitRateLimit > 500000)
                                    return 500000;
                                break;

                            // Opus
                            case "Opus":
                                if (aBitRateLimit > 510000)
                                    return 510000;
                                break;

                            // AAC
                            case "AAC":
                                if (aBitRateLimit > 400000)
                                    return 400000;
                                break;

                            // AC3
                            case "AC3":
                                if (aBitRateLimit > 640000)
                                    return 640000;
                                break;

                            // DTS
                            case "DTS":
                                // max
                                if (aBitRateLimit > 1509075)
                                    return 1509075;
                                // min 320k
                                else if (aBitRateLimit < 320000)
                                    return 320000;
                                break;

                            // MP2
                            case "MP2":
                                if (aBitRateLimit > 384000)
                                    return 384000;
                                break;

                            // LAME
                            case "LAME":
                                if (aBitRateLimit > 320000)
                                    return 320000;
                                break;

                            // FLAC
                            case "FLAC":
                                if (aBitRateLimit > 1411000)
                                    return 1411000;
                                break;

                            // PCM
                            case "PCM":
                                if (aBitRateLimit > 1536000)
                                    return 1536000;
                                break;

                            // Unknown
                            default:
                                return 320000;
                        }
                        //}

                        // Error comparing bit rate
                        //catch
                        //{
                        //    // Return a sensible default
                        //    return 320000;
                        //}
                    }
                }

                // Return Value
                return aBitRateLimit;
            }


            /// <summary>
            /// Batch Audio BitRate Limiter (Method)
            /// <summary>
            public static String BatchAudioBitRateLimiter(string codec_SelectedItem,
                                                          string quality_SelectedItem
                                                          )
            {
                // Check:
                // Audio Codec Not Copy
                if (codec_SelectedItem != "Copy")
                {
                    // -------------------------
                    // Batch Limit BitRates
                    // -------------------------
                    // Only if Audio Quality Auto
                    if (quality_SelectedItem == "Auto")
                    {
                        switch (codec_SelectedItem)
                        {
                            // Vorbis
                            case "Vorbis":
                                return "& (IF %A gtr 500000 (SET aBitRate=500000) ELSE (echo BitRate within Vorbis Limit of 500k)) & for /F %A in ('echo %aBitRate%') do (echo)";

                            // Opus
                            case "Opus":
                                return "& (IF %A gtr 510000 (SET aBitRate=510000) ELSE (echo BitRate within Opus Limit of 510k)) & for /F %A in ('echo %aBitRate%') do (echo)";

                            // AAC
                            case "AAC":
                                return "& (IF %A gtr 400000 (SET aBitRate=400000) ELSE (echo BitRate within AAC Limit of 400k)) & for /F %A in ('echo %aBitRate%') do (echo)";

                            // AC3
                            case "AC3":
                                return "& (IF %A gtr 640000 (SET aBitRate=640000) ELSE (echo BitRate within AC3 Limit of 640k)) & for /F %A in ('echo %aBitRate%') do (echo)";

                            // DTS
                            case "DTS":
                                return "& (IF %A lss 320000 (SET aBitRate=320000) ELSE (echo BitRate within DTS Limit of 320k)) & for /F %A in ('echo %aBitRate%') do (echo)" +
                                       " & (IF %A gtr 1509075 (SET aBitRate=640000) ELSE (echo BitRate within DTS Limit of 1509.75k)) & for /F %A in ('echo %aBitRate%') do (echo)";

                            // MP2
                            case "MP2":
                                return "& (IF %A gtr 384000 (SET aBitRate=384000) ELSE (echo BitRate within MP2 Limit of 384k)) & for /F %A in ('echo %aBitRate%') do (echo)";

                            // LAME
                            case "LAME":
                                return "& (IF %A gtr 320000 (SET aBitRate=320000) ELSE (echo BitRate within LAME Limit of 320k)) & for /F %A in ('echo %aBitRate%') do (echo)";

                            //// FLAC
                            // Do not use, empty is FFmpeg default
                            //case "FLAC":
                            //    return "& (IF %A gtr 1411000 (SET aBitRate=1411000) ELSE (echo BitRate within LAME Limit of 1411k)) & for /F %A in ('echo %aBitRate%') do (echo)";

                            //// PCM
                            // Do not use, empty is FFmpeg default
                            //case "PCM":
                            //    return "& (IF %A gtr 1536000 (SET aBitRate=1536000) ELSE (echo BitRate within LAME Limit of 1536k)) & for /F %A in ('echo %aBitRate%') do (echo)";

                        }
                    }
                }

                // Return Value
                return string.Empty;
            }



            /// <summary>
            /// Batch Audio Quality Auto
            /// <summary>
            public static String BatchAudioQualityAuto(bool batch_IsChecked,
                                                       string codec_SelectedItem,
                                                       string quality_SelectedItem
                                                       )
            {
                // Check:
                // Audio Codec Not Copy
                if (codec_SelectedItem != "Copy")
                {
                    // -------------------------
                    // Batch Audio Auto BitRates
                    // -------------------------

                    // Batch CMD Detect
                    //
                    if (quality_SelectedItem == "Auto" &&
                        codec_SelectedItem != "FLAC" && // Don't set Auto FLAC bitrate, FFmpeg will choose default
                        codec_SelectedItem != "PCM"// Don't set Auto PCM bitrate, FFmpeg will choose default
                        )
                    {
                        // Make List
                        IEnumerable<string> BatchAudioAutoList = new List<string>()
                        {
                            // audio
                            "& for /F \"delims=\" %A in ('@" + Analyze.FFprobe.ffprobe + " -v error -select_streams a:0 -show_entries " + Analyze.FFprobe.aEntryType + " -of default^=noprint_wrappers^=1:nokey^=1 \"%~f\" 2^>^&1') do (SET aBitRate=%A)",

                            // set %A to %aBitRate%
                            "\r\n\r\n" + "& for /F %A in ('echo %aBitRate%') do (echo)",

                            // basic limiter
                            "\r\n\r\n" + "& (IF %A EQU N/A (SET aBitRate=320000))",

                            // null check
                            "\r\n\r\n" + "& (IF %A EQU \"\" (SET aBitRate=320000))",

                            // set %A to %aBitRate%
                            "\r\n\r\n" + "& for /F %A in ('echo %aBitRate%') do (echo)"
                        };

                        // Join List with Spaces, Remove Empty Strings
                        batchAudioAuto = string.Join(" ", BatchAudioAutoList
                                                            .Where(s => !string.IsNullOrWhiteSpace(s))
                                                            );
                    }
                }

                // Return Value
                return batchAudioAuto;
            }
        }
    }
}
