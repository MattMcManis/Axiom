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

/* ----------------------------------
 METHODS

 * Audio Codec
 * BitRate Mode
 * Audio Quality
 * Audio BitRate Calculator
 * Audio VBR Calculator
 * Channel
 * Sample Rate
 * Bit Depth
 * Batch Audio BitRate Limiter
 * Batch Audio Quality Auto
 * Volume
 * HardLimiter Filter
---------------------------------- */

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
        /// <summary>
        /// Global Variables
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        // Audio
        public static string aCodec { get; set; }
        public static string aChannel { get; set; }
        public static string aBitMode { get; set; } // -b:a, -q:a
        public static string aBitRate { get; set; }
        public static string aBitRateNA { get; set; } // fallback default if not available
        public static string aQuality { get; set; }
        public static string aLossless { get; set; }
        public static string aCompressionLevel { get; set; }
        public static string aSamplerate { get; set; }
        public static string aBitDepth { get; set; }
        public static string aVolume { get; set; }
        public static string aHardLimiter { get; set; }

        // Batch
        //public static string aBitRateLimiter; // limits the bitrate value of webm and ogg
        public static string batchAudioAuto { get; set; }



        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Methods
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Audio Codec
        /// <summary>
        public static String AudioCodec(string codec_SelectedItem,
                                        string codec_Command
                                        )
        {
            // Passed Command
            if (codec_SelectedItem != "None")
            {
                aCodec = codec_Command;
            }
                
            return aCodec;
        }


        /// <summary>
        /// BitRate Mode
        /// <summary>
        public static String BitRateMode(bool vbr_IsChecked,
                                         List<AudioViewModel.AudioQuality> quality_Items,
                                         string quality_SelectedItem,
                                         string bitrate_Text)
        {
            // Only if BitRate Textbox is not Empty (except for Auto Quality)
            if (quality_SelectedItem == "Auto" || 
                !string.IsNullOrEmpty(bitrate_Text))
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
                                       List<AudioViewModel.AudioQuality> quality_Items,
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
                if (!string.IsNullOrEmpty(FFprobe.inputAudioBitRate))
                {
                    //MessageBox.Show(FFprobe.inputAudioBitRate); //debug

                    // Input BitRate was detected
                    if (FFprobe.inputAudioBitRate != "N/A")
                    {
                        // CBR
                        if (vbr_IsChecked == false)
                        {
                            // aBitMode = "-b:a";
                            aBitRate = AudioBitRateCalculator(codec_SelectedItem, 
                                                              FFprobe.aEntryType,

                                                              //FFprobe.inputAudioBitRate

                                                              // Limit FFprobe Input Audio BitRate if higher than Codec's maximum bit rate
                                                              AudioBitRateLimiter(//mediaType_SelectedItem,
                                                                                  //stream_SelectedItem,
                                                                                  codec_SelectedItem,
                                                                                  quality_SelectedItem,
                                                                                  Convert.ToInt32(FFprobe.inputAudioBitRate)
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
                                                          FFprobe.inputAudioBitRate // VBR Birate Limiter is handled in VBR Calculator
                                                          );
                        }
                    }

                    // -------------------------
                    // Input Does Not Have Audio Codec
                    // -------------------------
                    if (!string.IsNullOrEmpty(FFprobe.inputAudioCodec))
                    {
                        // Default to a new bitrate if Input & Output formats Do Not match
                        if (FFprobe.inputAudioBitRate == "N/A" &&
                            !string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase))
                        {
                            // Default to NA value
                            if (!string.IsNullOrEmpty(aBitRateNA))
                            {
                                //aBitRate = aBitRateNA;

                                // CBR
                                if (vbr_IsChecked == false)
                                {
                                    aBitRate = AudioBitRateCalculator(codec_SelectedItem, 
                                                                      FFprobe.aEntryType, 
                                                                      aBitRateNA
                                                                      );
                                }

                                // VBR
                                else if (vbr_IsChecked == true)
                                {
                                    //VBR does not have 'k'
                                    aBitRate = AudioVBRCalculator(vbr_IsChecked, 
                                                                  codec_SelectedItem,
                                                                  quality_Items,
                                                                  quality_SelectedItem,
                                                                  aBitRateNA
                                                                  );
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
                else if (string.IsNullOrEmpty(FFprobe.inputAudioBitRate))
                {
                    aBitMode = string.Empty;
                    aBitRate = string.Empty;
                }

                // -------------------------
                // Input TextBox is Empty - Auto Value
                // -------------------------
                if (string.IsNullOrEmpty(input_Text))
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
                if (string.IsNullOrEmpty(aBitRate))
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
                aBitRate = "%A";

                //MessageBox.Show(aBitRate); //debug
            }
        }


        /// <summary>
        /// Audio Quality - Lossless
        /// <summary>
        public static void QualityLossless(List<AudioViewModel.AudioQuality> quality_Items)
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
                                         List<AudioViewModel.AudioQuality> quality_Items,
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
                                          List<AudioViewModel.AudioQuality> quality_Items,
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

                if (!string.IsNullOrEmpty(aBitMode)) // Null Check
                {
                    // -------------------------
                    // Auto
                    // -------------------------
                    if (quality_SelectedItem == "Auto")
                    {
                        QualityAuto(input_Text,
                                    batch_IsChecked,
                                    mediaType_SelectedItem,
                                    stream_SelectedItem,
                                    codec_SelectedItem,
                                    quality_Items,
                                    quality_SelectedItem,
                                    vbr_IsChecked
                                    );
                    }

                    // -------------------------
                    // Lossless
                    // -------------------------
                    else if (quality_SelectedItem == "Lossless")
                    {
                        QualityLossless(quality_Items);
                    }

                    // -------------------------
                    // Custom
                    // -------------------------
                    else if (quality_SelectedItem == "Custom")
                    {
                        QualityCustom(vbr_IsChecked,
                                      codec_SelectedItem,
                                      quality_Items,
                                      quality_SelectedItem,
                                      bitrate_Text);
                    }

                    // -------------------------
                    // Preset: 640, 400, 320, 128, etc
                    // -------------------------
                    else
                    {
                        // Preset & Custom
                        QualityPreset(bitrate_Text);
                    }

                    // --------------------------------------------------
                    // Add kbps
                    // --------------------------------------------------
                    if (!string.IsNullOrEmpty(aBitRate) && // BitRate Null
                        aBitMode != "-q:a" &&              // Ignore VBR
                        aBitRate != "%A"                   // Ignore Batch Auto Quality CBR
                        )
                    {
                        //aBitRate = aBitRate + "k";
                        aBitRate += "k";
                    }


                    // --------------------------------------------------
                    // Combine Options
                    // --------------------------------------------------
                    List<string> aQualityArgs = new List<string>()
                    {
                        aBitMode,
                        aBitRate
                    };

                    // Quality
                    aQuality = string.Join(" ", aQualityArgs
                                                .Where(s => !string.IsNullOrEmpty(s))
                                                .Where(s => !s.Equals("\n"))
                                          );


                    // --------------------------------------------------
                    // Format European English comma to US English peroid - 1,234 to 1.234
                    // --------------------------------------------------
                    aQuality = string.Format(System.Globalization.CultureInfo.GetCultureInfo("en-US"), "{0:0.0}", aQuality);
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
            // -------------------------
            // Remove k from input if any
            // -------------------------
            //inputAudioBitRate = Regex.Replace(inputAudioBitRate, "k", "", RegexOptions.IgnoreCase);

            try
            {
                // -------------------------
                // If Video is has no Audio, don't set Audio BitRate
                // -------------------------
                if (!string.IsNullOrEmpty(FFprobe.inputAudioBitRate))
                {
                    // -------------------------
                    // Filter out any extra spaces after the first 3 characters IMPORTANT
                    // -------------------------
                    if (inputAudioBitRate.Substring(0, 3) == "N/A")
                    {
                        //MessageBox.Show("1");
                        inputAudioBitRate = "N/A";
                    }
                }

                // -------------------------
                // If Video has Audio, calculate BitRate into decimal
                // -------------------------
                if (inputAudioBitRate != "N/A" && 
                    !string.IsNullOrEmpty(FFprobe.inputAudioBitRate))
                {
                    // -------------------------
                    // Convert inputAudioBitRate to Decimal
                    // -------------------------
                    inputAudioBitRate = Convert.ToString(double.Parse(inputAudioBitRate) * 0.001);

                    // -------------------------
                    // Convert inputAudioBitRate to Double
                    // -------------------------
                    double inputAudioBitRate_Double = double.Parse(inputAudioBitRate);

                    // -------------------------
                    // Apply limits if BitRate goes over
                    // -------------------------
                    // -------------------------
                    // Vorbis
                    // -------------------------
                    if (codec_SelectedItem == "Vorbis" &&
                        inputAudioBitRate_Double > 500)
                    {
                        inputAudioBitRate = Convert.ToString(500); //was 500,000 (before converting to decimal)
                    }
                    // -------------------------
                    // Opus
                    // -------------------------
                    else if (codec_SelectedItem == "Opus" &&
                             inputAudioBitRate_Double > 510)
                    {
                        inputAudioBitRate = Convert.ToString(510); //was 510,000 (before converting to decimal)
                    }
                    // -------------------------
                    // MP2
                    // -------------------------
                    else if (codec_SelectedItem == "MP2" &&
                             inputAudioBitRate_Double > 384)
                    {
                        inputAudioBitRate = Convert.ToString(384); //was 320,000 before converting to decimal)
                    }
                    // -------------------------
                    // LAME
                    // -------------------------
                    else if (codec_SelectedItem == "LAME" &&
                             inputAudioBitRate_Double > 320)
                    {
                        inputAudioBitRate = Convert.ToString(320); //was 320,000 before converting to decimal)
                    }
                    // -------------------------
                    // AC3
                    // -------------------------
                    else if (codec_SelectedItem == "AC3" &&
                             inputAudioBitRate_Double > 640)
                    {
                        inputAudioBitRate = Convert.ToString(640); //was 640,000 (before converting to decimal)
                    }
                    // -------------------------
                    // AAC
                    // -------------------------
                    else if (codec_SelectedItem == "AAC" &&
                             inputAudioBitRate_Double > 400)
                    {
                        inputAudioBitRate = Convert.ToString(400); //was 400,000 (before converting to decimal)
                    }
                    // -------------------------
                    // DTS
                    // -------------------------
                    else if (codec_SelectedItem == "DTS" &&
                             inputAudioBitRate_Double > 1509)
                    {
                        inputAudioBitRate = Convert.ToString(1509); //was 640,000 (before converting to decimal)
                    }

                    // -------------------------
                    // ALAC, FLAC do not need limit
                    // -------------------------

                    // -------------------------
                    // Apply limits if BitRate goes Under
                    // -------------------------
                    // -------------------------
                    // Vorbis
                    // -------------------------
                    // Vorbis has a minimum bitrate limit of 45k, if less than, set to 45k
                    else if (codec_SelectedItem == "Vorbis" &&
                             inputAudioBitRate_Double < 45)
                    {
                        inputAudioBitRate = Convert.ToString(45);
                    }

                    // -------------------------
                    // Opus
                    // -------------------------
                    // Opus has a minimum bitrate limit of 6k, if less than, set to 6k
                    else if (codec_SelectedItem == "Opus" &&
                             inputAudioBitRate_Double < 6)
                    {
                        inputAudioBitRate = Convert.ToString(6);
                    }

                    // -------------------------
                    // Round BitRate, Remove Decimals
                    // -------------------------
                    inputAudioBitRate = Math.Round(inputAudioBitRate_Double).ToString();
                }
            }
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
                                                List<AudioViewModel.AudioQuality> quality_Items,
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
                double aBitRateVBR = (double.Parse(inputBitRate)); // If passed from

                // If inputBitRate is from Auto Quality FFprobe Parse, divide by 1000 to get smaller value
                // e.g. (320000 to 320)
                // If inputBirate is from Custom Quality User Input value, leave the same
                if (quality_SelectedItem == "Auto")
                {
                    aBitRateVBR = aBitRateVBR / 1000;
                }


                // -------------------------
                // Vorbis
                // -------------------------
                if (codec_SelectedItem == "Vorbis")
                {
                    // VBR User entered value algorithm (0 low / 10 high)

                    double inputMin = 0; // kbps
                    double inputMax = 320; // kbps
                    double normMin = 0;    // low
                    double normMax = 10;   // high

                    aBitRateVBR = Math.Round(
                                        MainWindow.NormalizeValue(
                                                        aBitRateVBR, // input
                                                        inputMin,    // input min
                                                        inputMax,    // input max
                                                        normMin,     // normalize min
                                                        normMax,     // normalize max
                                                        (normMin + normMax) / 2 // midpoint average
                                                    )

                                                , 5 // max decimal places
                                        );

                    //// Above 290k set to 10 Quality
                    //if (aBitRateVBR > 290)
                    //{
                    //    aBitRateVBR = aBitRateVBR = Convert.ToDouble(quality_Items.FirstOrDefault(item => item.Name == "Auto")?.VBR);
                    //}
                    //// 32 bracket
                    //else if (aBitRateVBR >= 128)
                    //{
                    //    //aBitRateVBR = aBitRateVBR * 0.03125;
                    //}
                    //// 16 bracket
                    //else if (aBitRateVBR < 128)
                    //{
                    //    //aBitRateVBR = (aBitRateVBR * 0.03125) - 0.5;
                    //}
                    //else if (aBitRateVBR <= 96)
                    //{
                    //    //aBitRateVBR = (aBitRateVBR * 0.013125) - 0.25;
                    //}
                    //// 8 bracket
                    //else if (aBitRateVBR <= 64)
                    //{
                    //    aBitRateVBR = 0;
                    //}

                    //MessageBox.Show(aBitRateVBR.ToString()); //debug
                }

                // -------------------------
                // Opus
                // -------------------------
                else if (codec_SelectedItem == "Opus")
                {
                    // Above 510k set to 256k
                    if (aBitRateVBR > 510)
                    {
                        aBitRateVBR = Convert.ToDouble(quality_Items.FirstOrDefault(item => item.Name == "Auto")?.VBR);
                    }
                    else
                    {
                        // Use the original -b:a bitrate detected (e.g. -b:a 256k)
                    }

                }

                // -------------------------
                // AAC
                // -------------------------
                else if (codec_SelectedItem == "AAC")
                {
                    // Range 
                    // Above 320k 
                    if (aBitRateVBR > 400)
                    {
                        aBitRateVBR = Convert.ToDouble(quality_Items.FirstOrDefault(item => item.Name == "Auto")?.VBR);
                    }
                    else
                    {
                        // VBR User entered value algorithm (0.1 low / 2 high)
                        aBitRateVBR = Math.Min(2, Math.Max(aBitRateVBR * 0.00625, 0.1));
                    }
                }
           
                // -------------------------
                // MP2
                // -------------------------
                else if (codec_SelectedItem == "MP2")
                {
                    // Above 384k set to V0
                    if (aBitRateVBR > 384)
                    {
                        aBitRateVBR = Convert.ToDouble(quality_Items.FirstOrDefault(item => item.Name == "Auto")?.VBR);
                    }
                    else
                    {
                        // VBR User entered value algorithm (10 low / 0 high)
                        aBitRateVBR = (((aBitRateVBR * (-0.01)) / 2.60) + 1) * 10;
                    }
                }

                // -------------------------
                // LAME MP3
                // -------------------------
                else if (codec_SelectedItem == "LAME")
                {
                    // Above 320k set to V0
                    if (aBitRateVBR > 320)
                    {
                        aBitRateVBR = Convert.ToDouble(quality_Items.FirstOrDefault(item => item.Name == "Auto")?.VBR);
                    }
                    else
                    {
                        // VBR User entered value algorithm (10 low / 0 high)
                        aBitRateVBR = (((aBitRateVBR * (-0.01)) / 2.60) + 1) * 10;
                    }
                }


                // -------------------------
                // Convert to String for aQuality Combine
                // -------------------------
                aBitRate = Convert.ToString(aBitRateVBR);

            }

            //MessageBox.Show(aBitRate); //debug

            return aBitRate;
        }



        /// <summary>
        /// Channel
        /// <summary>
        public static String Channel(string codec_SelectedItem,
                                     string channel_SelectedItem
                                    )
        {
            // Check:
            // Audio Codec Not Copy
            if (codec_SelectedItem != "Copy")
            {
                // -------------------------
                // Auto
                // -------------------------
                if (channel_SelectedItem == "Source" ||
                    string.IsNullOrEmpty(channel_SelectedItem))
                {
                    aChannel = string.Empty;
                }
                // -------------------------
                // Mono
                // -------------------------
                else if (channel_SelectedItem == "Mono")
                {
                    aChannel = "-ac 1";
                }
                // -------------------------
                // Stereo
                // -------------------------
                else if (channel_SelectedItem == "Stereo")
                {
                    aChannel = "-ac 2";
                }
                // -------------------------
                // Joint Stereo
                // -------------------------
                else if (channel_SelectedItem == "Joint Stereo")
                {
                    aChannel = "-ac 2 -joint_stereo 1";
                }
                // -------------------------
                // 5.1
                // -------------------------
                else if (channel_SelectedItem == "5.1")
                {
                    aChannel = "-ac 6";
                }

                // -------------------------
                // Prevent Downmix Clipping
                // -------------------------
                if (channel_SelectedItem != "Source")
                {
                    aChannel = "-rematrix_maxval 1.0 " + aChannel;
                }


                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Channel: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(channel_SelectedItem) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            // Return Value
            return aChannel;
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
                    string.IsNullOrEmpty(compressionLevel_SelectedItem)
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
                                        List<AudioViewModel.AudioSampleRate> sampleRate_Items,
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
                                      List<AudioViewModel.AudioBitDepth> bitDepth_Items,
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
                    aBitDepth = bitDepth_Items.FirstOrDefault(item => item.Name == bitDepth_SelectedItem) ?.Depth;
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
                    try
                    {
                        switch (codec_SelectedItem)
                        {
                            // Vorbis
                            case "Vorbis":
                                if (aBitRateLimit > 500000)
                                {
                                    return 500000;
                                }
                                break;
                            // Opus
                            case "Opus":
                                if (aBitRateLimit > 510000)
                                {
                                    return 510000;
                                }
                                break;
                            // AAC
                            case "AAC":
                                if (aBitRateLimit > 400000)
                                {
                                    return 400000;
                                }
                                break;

                            // AC3
                            case "AC3":
                                if (aBitRateLimit > 640000)
                                {
                                    return 640000;
                                }
                                break;
                            // DTS
                            case "DTS":
                                if (aBitRateLimit > 1509075)
                                {
                                    return 1509075;
                                }
                                break;
                            // MP2
                            case "MP2":
                                if (aBitRateLimit > 384000)
                                {
                                    return 384000;
                                }
                                break;
                            // LAME
                            case "LAME":
                                if (aBitRateLimit > 320000)
                                {
                                    return 320000;
                                }
                                break;
                            // FLAC
                            case "FLAC":
                                if (aBitRateLimit > 1411000)
                                {
                                    return 1411000;
                                }
                                break;
                            // PCM
                            case "PCM":
                                if (aBitRateLimit > 1536000)
                                {
                                    return 1536000;
                                }
                                break;
                        }
                    }

                    // Error comparing bit rate
                    catch
                    {
                        // Return a sensible default
                        return 320000;
                    }
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
                    switch (codec_SelectedItem) {
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
                            return "& (IF %A gtr 1509075 (SET aBitRate=640000) ELSE (echo BitRate within DTS Limit of 1509.75k)) & for /F %A in ('echo %aBitRate%') do (echo)";

                        // MP2
                        case "MP2":
                            return "& (IF %A gtr 384000 (SET aBitRate=384000) ELSE (echo BitRate within MP2 Limit of 384k)) & for /F %A in ('echo %aBitRate%') do (echo)";

                        // LAME
                        case "LAME":
                            return "& (IF %A gtr 320000 (SET aBitRate=320000) ELSE (echo BitRate within LAME Limit of 320k)) & for /F %A in ('echo %aBitRate%') do (echo)";

                        //// FLAC
                        //case "FLAC":
                        //    return "& (IF %A gtr 1411000 (SET aBitRate=1411000) ELSE (echo BitRate within LAME Limit of 1411k)) & for /F %A in ('echo %aBitRate%') do (echo)";

                        //// PCM
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
                    List<string> BatchAudioAutoList = new List<string>()
                    {
                        // audio
                        "& for /F \"delims=\" %A in ('@" + FFprobe.ffprobe + " -v error -select_streams a:0 -show_entries " + FFprobe.aEntryType + " -of default^=noprint_wrappers^=1:nokey^=1 \"%~f\" 2^>^&1') do (SET aBitRate=%A)",

                        // set %A to %aBitRate%
                        "\r\n\r\n" + "& for /F %A in ('echo %aBitRate%') do (echo)",

                        // basic limiter
                        "\r\n\r\n" + "& (IF %A EQU N/A (SET aBitRate=320000))",

                        // set %A to %aBitRate%
                        "\r\n\r\n" + "& for /F %A in ('echo %aBitRate%') do (echo)"
                    };

                    // Join List with Spaces, Remove Empty Strings
                    batchAudioAuto = string.Join(" ", BatchAudioAutoList
                                                        .Where(s => !string.IsNullOrEmpty(s)));
                }
            }

            // Return Value
            return batchAudioAuto;
        }



        /// <summary>
        /// Volume
        /// <summary>
        public static void Volume()
        {
            // -------------------------
            // Only if Audio Codec is Not Empty
            // -------------------------
            if (!string.IsNullOrEmpty(VM.AudioView.Audio_Codec_SelectedItem))
            {
                // If TextBox is 100% or Empty
                if (VM.AudioView.Audio_Volume_Text == "100%" ||
                    VM.AudioView.Audio_Volume_Text == "100" ||
                    string.IsNullOrEmpty(VM.AudioView.Audio_Volume_Text))
                {
                    aVolume = string.Empty;
                }
                // If User Custom Entered Value
                // Convert Volume % to Decimal
                else
                {
                    try
                    {
                        // If user enters value, turn on Filter
                        double volumeDecimal = Convert.ToDouble(VM.AudioView.Audio_Volume_Text.Trim()) * 0.01;

                        aVolume = "volume=" + volumeDecimal;

                        // Audio Filter Add
                        AudioFilters.aFiltersList.Add(aVolume);
                    }
                    catch
                    {

                    }
                }
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Volume: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(VM.AudioView.Audio_Volume_Text) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);
        }



        /// <summary>
        /// Hard Limiter Filter (Method)
        /// <summary>
        public static void HardLimiter()
        {
            // FFmpeg Range 0.0625 to 1
            // FFmpeg Default 0
            // Slider -24 to 0
            // Slider Default 0
            // Limit to 4 decimal places

            double value = VM.AudioView.Audio_HardLimiter_Value;

            aHardLimiter = string.Empty;

            if (VM.AudioView.Audio_HardLimiter_IsEnabled == true &&
                value != 0)
            {

                try
                {
                    string limit = string.Empty;

                    // -0.1 to -3dB
                    if (value > -4)
                    {
                        double inputMin = -3;
                        double inputMax = -0.1;
                        double normMin = 0.7;
                        double normMax = 0.99;

                        limit = Convert.ToString(
                                        Math.Round(
                                            MainWindow.NormalizeValue(
                                                            value,    // input
                                                            inputMin, // input min
                                                            inputMax, // input max
                                                            normMin,  // normalize min
                                                            normMax,  // normalize max
                                                            (normMin + normMax) / 2 // midpoint average
                                                        )

                                                    , 4 // max decimal places
                                                )
                                            );
                    }

                    // -4 to -7dB
                    else if (value <= -4 && value >= -7)
                    {
                        double inputMin = -7;
                        double inputMax = -4;
                        double normMin = 0.45;
                        double normMax = 0.65;

                        limit = Convert.ToString(
                                        Math.Round(
                                            MainWindow.NormalizeValue(
                                                            value,    // input
                                                            inputMin, // input min
                                                            inputMax, // input max
                                                            normMin,  // normalize min
                                                            normMax,  // normalize max
                                                            (normMin + normMax) / 2 // midpoint average
                                                        )

                                                    , 4 // max decimal places
                                                )
                                            );
                    }

                    // -8 to -10dB
                    else if (value <= -8 && value >= -10)
                    {
                        double inputMin = -10;
                        double inputMax = -8;
                        double normMin = 0.3;
                        double normMax = 0.4;

                        limit = Convert.ToString(
                                        Math.Round(
                                            MainWindow.NormalizeValue(
                                                            value,    // input
                                                            inputMin, // input min
                                                            inputMax, // input max
                                                            normMin,  // normalize min
                                                            normMax,  // normalize max
                                                            (normMin + normMax) / 2 // midpoint average
                                                        )

                                                    , 4 // max decimal places
                                                )
                                            );
                    }

                    // -11 to -16dB
                    else if (value <= -11 && value >= -16)
                    {
                        double inputMin = -16;
                        double inputMax = -11;
                        double normMin = 0.15;
                        double normMax = 0.275;

                        limit = Convert.ToString(
                                        Math.Round(
                                            MainWindow.NormalizeValue(
                                                            value,    // input
                                                            inputMin, // input min
                                                            inputMax, // input max
                                                            normMin,  // normalize min
                                                            normMax,  // normalize max
                                                            (normMin + normMax) / 2 // midpoint average
                                                        )

                                                    , 4 // max decimal places
                                                )
                                            );
                    }

                    // -17 to -19dB
                    else if (value <= -17 && value >= -19)
                    {
                        double inputMin = -19;
                        double inputMax = -17;
                        double normMin = 0.1;
                        double normMax = 0.135;

                        limit = Convert.ToString(
                                        Math.Round(
                                            MainWindow.NormalizeValue(
                                                            value,    // input
                                                            inputMin, // input min
                                                            inputMax, // input max
                                                            normMin,  // normalize min
                                                            normMax,  // normalize max
                                                            (normMin + normMax) / 2 // midpoint average
                                                        )

                                                    , 8 // max decimal places
                                                )
                                            );
                    }

                    // -20 to -24dB
                    else if (value <= -20)
                    {
                        double inputMin = -24;
                        double inputMax = -20;
                        double normMin = 0.0625;
                        double normMax = 0.0975;

                        limit = Convert.ToString(
                                        Math.Round(
                                            MainWindow.NormalizeValue(
                                                            value,    // input
                                                            inputMin, // input min
                                                            inputMax, // input max
                                                            normMin,  // normalize min
                                                            normMax,  // normalize max
                                                            (normMin + normMax) / 2 // midpoint average
                                                        )

                                                    , 4 // max decimal places
                                                )
                                            );
                    }

                    aHardLimiter = "alimiter=level_in=1:level_out=1:limit=" + limit + ":attack=7:release=100:level=disabled";

                    // Add to Filters List
                    AudioFilters.aFiltersList.Add(aHardLimiter);
                }
                catch
                {
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Error: Could not set Hard Limiter .")) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }
        }


    }
}
