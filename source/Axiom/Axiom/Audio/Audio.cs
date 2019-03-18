/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2019 Matt McManis
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

/* ----------------------------------
 METHODS

 * Audio Codec
 * Bitrate Mode
 * Audio Quality
 * Audio Bitrate Calculator
 * Audio VBR Calculator
 * Channel
 * Sample Rate
 * Bit Depth
 * Batch Audio Bitrate Limiter
 * Batch Audio Quality Auto
 * Volume
 * HardLimiter Filter
---------------------------------- */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
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
        public static string aBitMode; // -b:a, -q:a
        public static string aBitrate;
        public static string aBitrateNA; // fallback default if not available
        public static string aQuality;
        public static string aLossless;
        public static string aChannel;
        public static string aSamplerate;
        public static string aBitDepth;
        public static string aVolume;
        public static string aHardLimiter;

        // Batch
        public static string aBitrateLimiter; // limits the bitrate value of webm and ogg
        public static string batchAudioAuto;


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Process Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        ///     Audio Codec
        /// <summary>
        public static String AudioCodec(string codec_SelectedItem,
                                        string codec_Command,
                                        string bitDepth_SelectedItem
                                        )
        {
            // Passed Command
            if (codec_SelectedItem != "None")
            {
                aCodec = "-c:a " + codec_Command;

                if (codec_SelectedItem == "PCM") // Special
                {
                    aCodec = PCM_BitDepth(codec_SelectedItem,
                                          bitDepth_SelectedItem
                                          );
                }
            }
                
            return aCodec;
        }


        /// <summary>
        ///     Bitrate Mode
        /// <summary>
        public static String BitrateMode(bool vbr_IsChecked,
                                         List<ViewModel.AudioQuality> quality_Items,
                                         string quality_SelectedItem,
                                         string bitrate_Text)
        {
            // Only if Bitrate Textbox is not Empty (except for Auto Quality)
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
        ///     Audio Quality - Auto
        /// <summary>
        public static void QualityAuto(string input_Text,
                                       bool batch_IsChecked,
                                       bool vbr_IsChecked,
                                       string codec_SelectedItem,
                                       List<ViewModel.AudioQuality> quality_Items,
                                       string quality_SelectedItem
                                       )
        {
            // No Detectable Bitrate Default
            aBitrateNA = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.NA;

            // --------------------------------------------------
            // Single
            // --------------------------------------------------
            if (batch_IsChecked == false)
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
                            aBitrate = AudioBitrateCalculator(codec_SelectedItem, FFprobe.aEntryType, FFprobe.inputAudioBitrate);
                        }

                        // VBR
                        else if (vbr_IsChecked == true)
                        {
                            //VBR does not have 'k'

                            aBitrate = AudioVBRCalculator(vbr_IsChecked, codec_SelectedItem, FFprobe.inputAudioBitrate);
                        }
                    }

                    // -------------------------
                    // Input Does Not Have Audio Codec
                    // -------------------------
                    if (!string.IsNullOrEmpty(FFprobe.inputAudioCodec))
                    {
                        // Default to a new bitrate if Input & Output formats Do Not match
                        if (FFprobe.inputAudioBitrate == "N/A" &&
                            !string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase))
                        {
                            // Default to NA value
                            if (!string.IsNullOrEmpty(aBitrateNA))
                            {
                                aBitrate = aBitrateNA;
                            }
                            // Default to 320k if NA value is empty
                            else
                            {
                                aBitrate = "320";
                            }
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
                // Input TextBox is Empty - Auto Value
                // -------------------------
                if (string.IsNullOrEmpty(input_Text))
                {
                    //// CBR
                    //if (vbr_IsChecked == false)
                    //{
                    //    aBitMode = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.CBR_BitMode;
                    //}

                    //// VBR
                    //else if (vbr_IsChecked == true)
                    //{
                    //    aBitMode = quality_Items.FirstOrDefault(item => item.Name == quality_SelectedItem)?.VBR_BitMode;
                    //}

                    aBitMode = "-b:a";
                    aBitrate = "320";
                }

                // -------------------------
                // Bitrate Returned Empty, Disable BitMode
                // -------------------------
                if (string.IsNullOrEmpty(aBitrate))
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
                aBitrate = "%A";

                //MessageBox.Show(aBitrate); //debug
            }
        }


        /// <summary>
        ///     Audio Quality - Lossless
        /// <summary>
        public static void QualityLossless(List<ViewModel.AudioQuality> quality_Items)
        {
            aLossless = quality_Items.FirstOrDefault(item => item.Name == "Lossless")?.Lossless;
        }


        /// <summary>
        ///     Audio Quality - Preset
        /// <summary>
        public static void QualityPreset(string bitrate_Text)
        {
            // -------------------------
            // Bitrate
            // -------------------------
            aBitrate = bitrate_Text;
        }


        /// <summary>
        ///     Audio Quality - Custom
        /// <summary>
        public static void QualityCustom(bool vbr_IsChecked,
                                         string codec_SelectedItem,
                                         string bitrate_Text
                                         )
        {
            // --------------------------------------------------
            // Bitrate
            // --------------------------------------------------
            // -------------------------
            // CBR
            // -------------------------
            if (vbr_IsChecked == false)
            {
                // .e.g. 320k
                aBitrate = bitrate_Text;
            }

            // -------------------------
            // VBR
            // -------------------------
            else if (vbr_IsChecked == true)
            {
                // e.g. 320k converted to -q:a 2
                aBitrate = AudioVBRCalculator(vbr_IsChecked, 
                                              codec_SelectedItem, 
                                              bitrate_Text
                                              );
            }
        }


        /// <summary>
        ///     Audio Quality
        /// <summary>
        public static String AudioQuality(string input_Text,
                                          bool batch_IsChecked,
                                          bool vbr_IsChecked,
                                          string codec_SelectedItem,
                                          List<ViewModel.AudioQuality> quality_Items,
                                          string quality_SelectedItem,
                                          string bitrate_Text
                                          )
        {
            // Audio Quality None Check
            // Audio Codec None Check
            // Audio Codec Copy Check
            if (quality_SelectedItem != "None" &&
                codec_SelectedItem != "None" &&
                codec_SelectedItem != "Copy")
            {
                // Bitrate Mode
                aBitMode = BitrateMode(vbr_IsChecked,
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
                                    vbr_IsChecked,
                                    codec_SelectedItem,
                                    quality_Items,
                                    quality_SelectedItem
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
                    if (!string.IsNullOrEmpty(aBitrate) && // Bitrate Null
                        aBitMode != "-q:a"                 // Ignore VBR
                        )
                    {
                        //aBitrate = aBitrate + "k";
                        aBitrate += "k";
                    }


                    // --------------------------------------------------
                    // Combine Options
                    // --------------------------------------------------
                    List<string> aQualityArgs = new List<string>()
                    {
                        aBitMode,
                        aBitrate
                    };

                    // Quality
                    aQuality = string.Join(" ", aQualityArgs
                                                .Where(s => !string.IsNullOrEmpty(s))
                                                .Where(s => !s.Equals("\n"))
                                          );
                }

            }

            return aQuality;
        }



        /// <summary>
        ///     Audio Bitrate Calculator
        /// <summary>
        public static String AudioBitrateCalculator(string codec_SelectedItem, 
                                                    string aEntryType, 
                                                    string inputAudioBitrate
                                                    )
        {
            // -------------------------
            // Remove k from input if any
            // -------------------------
            //inputAudioBitrate = Regex.Replace(inputAudioBitrate, "k", "", RegexOptions.IgnoreCase);

            try
            {
                // -------------------------
                // If Video is has no Audio, don't set Audio Bitrate
                // -------------------------
                if (!string.IsNullOrEmpty(FFprobe.inputAudioBitrate))
                {
                    // -------------------------
                    // Filter out any extra spaces after the first 3 characters IMPORTANT
                    // -------------------------
                    if (inputAudioBitrate.Substring(0, 3) == "N/A")
                    {
                        MessageBox.Show("1");
                        inputAudioBitrate = "N/A";
                    }
                }

                // -------------------------
                // If Video has Audio, calculate Bitrate into decimal
                // -------------------------
                if (inputAudioBitrate != "N/A" && 
                    !string.IsNullOrEmpty(FFprobe.inputAudioBitrate))
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
                    if (codec_SelectedItem == "Vorbis" &&
                        double.Parse(inputAudioBitrate) > 500)
                    {
                        inputAudioBitrate = Convert.ToString(500); //was 500,000 (before converting to decimal)
                    }
                    // -------------------------
                    // Opus
                    // -------------------------
                    else if (codec_SelectedItem == "Opus" &&
                             double.Parse(inputAudioBitrate) > 510)
                    {
                        inputAudioBitrate = Convert.ToString(510); //was 510,000 (before converting to decimal)
                    }
                    // -------------------------
                    // MP2
                    // -------------------------
                    else if (codec_SelectedItem == "MP2" &&
                             double.Parse(inputAudioBitrate) > 384)
                    {
                        inputAudioBitrate = Convert.ToString(384); //was 320,000 before converting to decimal)
                    }
                    // -------------------------
                    // LAME
                    // -------------------------
                    else if (codec_SelectedItem == "LAME" &&
                             double.Parse(inputAudioBitrate) > 320)
                    {
                        inputAudioBitrate = Convert.ToString(320); //was 320,000 before converting to decimal)
                    }
                    // -------------------------
                    // AAC
                    // -------------------------
                    else if (codec_SelectedItem == "AAC" &&
                             double.Parse(inputAudioBitrate) > 400)
                    {
                        inputAudioBitrate = Convert.ToString(400); //was 400,000 (before converting to decimal)
                    }
                    // -------------------------
                    // AC3
                    // -------------------------
                    else if (codec_SelectedItem == "AC3" &&
                            double.Parse(inputAudioBitrate) > 640)
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
                    else if (codec_SelectedItem == "Vorbis" &&
                             double.Parse(inputAudioBitrate) < 45)
                    {
                        inputAudioBitrate = Convert.ToString(45);
                    }

                    // -------------------------
                    // Opus
                    // -------------------------
                    // Opus has a minimum bitrate limit of 6k, if less than, set to 6k
                    else if (codec_SelectedItem == "Opus" &&
                             double.Parse(inputAudioBitrate) < 6)
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
        ///     Audio VBR Calculator
        /// <summary>
        public static String AudioVBRCalculator(bool vbr_IsChecked, 
                                                string codec_SelectedItem, 
                                                string inputBitrate
                                                )
        {
            // -------------------------
            // VBR 
            // User entered value
            // -------------------------
            if (vbr_IsChecked == true)
            {
                // Used to Calculate VBR Double
                //
                double aBitrateVBR = double.Parse(inputBitrate); // passed parameter


                // -------------------------
                // AAC
                // -------------------------
                if (codec_SelectedItem == "AAC")
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
                // Vorbis
                // -------------------------
                else if (codec_SelectedItem == "Vorbis")
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
                // Opus
                // -------------------------
                else if (codec_SelectedItem == "Opus")
                {
                    // e.g. 128000 to 128k
                    aBitrateVBR = aBitrateVBR * 0.001;
                }

                // -------------------------
                // MP2
                // -------------------------
                // LAME MP3
                // -------------------------
                else if (codec_SelectedItem == "MP2" ||
                         codec_SelectedItem == "LAME")
                {
                    // Above 260k set to V0
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

            } 

            return aBitrate;
        }



        /// <summary>
        ///     Channel
        /// <summary>
        public static String Channel(string mediaType_SelectedItem,
                                     string codec_SelectedItem,
                                     string stream_SelectedItem,
                                     string quality_SelectedItem,
                                     string channel_SelectedItem)
        {
            // Audio Bitrate None Check
            // Audio Codec None Check
            // Audio Codec Copy Check
            // Mute Check
            // Stream None Check
            // Media Type Check
            if (mediaType_SelectedItem != "Image" &&
                mediaType_SelectedItem != "Sequence" &&
                codec_SelectedItem != "None" &&
                codec_SelectedItem != "Copy" &&
                quality_SelectedItem != "None" &&
                quality_SelectedItem != "Mute" &&
                stream_SelectedItem != "none" 
                )
            {
                // -------------------------
                // Auto
                // -------------------------
                if (channel_SelectedItem == "Auto")
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
        ///     Sample Rate
        /// <summary>
        public static String SampleRate(string mediaType_SelectedItem,
                                        string codec_SelectedItem,
                                        string stream_SelectedItem,
                                        string quality_SelectedItem,
                                        string channel_SelectedItem,
                                        List<ViewModel.AudioSampleRate> sampleRate_Items,
                                        string sampleRate_SelectedItem
                                        )
        {
            // Audio Bitrate None Check
            // Audio Codec None
            // Audio Codec Copy Check
            // Mute Check
            // Stream None Check
            // Media Type Check
            if (mediaType_SelectedItem != "Image" &&
                mediaType_SelectedItem != "Sequence" &&
                codec_SelectedItem != "None" &&
                codec_SelectedItem != "Copy" &&
                quality_SelectedItem != "None" &&
                quality_SelectedItem != "Mute" &&
                stream_SelectedItem != "none"
                )
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
        ///     PCM Bit Depth
        /// <summary>
        /// <remarks>
        ///     Changes the PCM Codec depending on BitDepth Selected
        /// </remarks>
        public static String PCM_BitDepth(string codec_SelectedItem,
                                          string bitDepth_SelectedItem
                                          )
        {
            if (codec_SelectedItem == "PCM")
            {
                // -------------------------
                // auto
                // -------------------------
                if (bitDepth_SelectedItem == "auto")
                {
                    aCodec = "-c:a pcm_s24le";
                }
                // -------------------------
                // 8
                // -------------------------
                else if (bitDepth_SelectedItem == "8")
                {
                    aCodec = "-c:a pcm_u8";
                }
                // -------------------------
                // 16
                // -------------------------
                else if (bitDepth_SelectedItem == "16")
                {
                    aCodec = "-c:a pcm_s16le";
                }
                // -------------------------
                // 24
                // -------------------------
                else if (bitDepth_SelectedItem == "24")
                {
                    aCodec = "-c:a pcm_s24le";
                }
                // -------------------------
                // 32
                // -------------------------
                else if (bitDepth_SelectedItem == "32")
                {
                    aCodec = "-c:a pcm_f32le";
                }
                // -------------------------
                // 64
                // -------------------------
                else if (bitDepth_SelectedItem == "64")
                {
                    aCodec = "-c:a pcm_f64le";
                }
            }

            return aCodec;
        }


        /// <summary>
        ///     Bit Depth
        /// <summary>
        public static String BitDepth(string mediaType_SelectedItem,
                                      string codec_SelectedItem,
                                      string stream_SelectedItem,
                                      string quality_SelectedItem,
                                      List<ViewModel.AudioBitDepth> bitDepth_Items,
                                      string bitDepth_SelectedItem)
        {
            // Audio Bitrate None Check
            // Audio Codec None
            // Audio Codec Copy Check
            // Mute Check
            // Stream None Check
            // Media Type Check
            if (mediaType_SelectedItem != "Image" &&
                mediaType_SelectedItem != "Sequence" &&
                codec_SelectedItem != "None" &&
                codec_SelectedItem != "Copy" &&
                quality_SelectedItem != "None" &&
                quality_SelectedItem != "Mute" &&
                stream_SelectedItem != "none"
                )
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
        ///     Batch Audio Bitrate Limiter (Method)
        /// <summary>
        public static String BatchAudioBitrateLimiter(string mediaType_SelectedItem,
                                                      string codec_SelectedItem,
                                                      string stream_SelectedItem,
                                                      string quality_SelectedItem
                                                      )
        {
            // Audio Bitrate None Check
            // Audio Codec None
            // Audio Codec Copy Check
            // Mute Check
            // Stream None Check
            // Media Type Check
            if (mediaType_SelectedItem != "Image" &&
                mediaType_SelectedItem != "Sequence" &&
                codec_SelectedItem != "None" &&
                codec_SelectedItem != "Copy" &&
                quality_SelectedItem != "None" &&
                quality_SelectedItem != "Mute" &&
                stream_SelectedItem != "none"
                )
            {
                // -------------------------
                // Batch Limit Bitrates
                // -------------------------
                // Only if Audio ComboBox Auto
                if (quality_SelectedItem == "Auto")
                {
                    // Limit Vorbis bitrate to 500k through cmd.exe
                    if (codec_SelectedItem == "Vorbis")
                    {
                        aBitrateLimiter = "& (IF %A gtr 500000 (SET aBitrate=500000) ELSE (echo Bitrate within Vorbis Limit of 500k)) & for /F %A in ('echo %aBitrate%') do (echo)";
                    }
                    // Limit Opus bitrate to 510k through cmd.exe
                    else if (codec_SelectedItem == "Opus")
                    {
                        aBitrateLimiter = "& (IF %A gtr 510000 (SET aBitrate=510000) ELSE (echo Bitrate within Opus Limit of 510k)) & for /F %A in ('echo %aBitrate%') do (echo)";
                    }
                    // Limit AAC bitrate to 400k through cmd.exe
                    else if (codec_SelectedItem == "AAC")
                    {
                        aBitrateLimiter = "& (IF %A gtr 400000 (SET aBitrate=400000) ELSE (echo Bitrate within AAC Limit of 400k)) & for /F %A in ('echo %aBitrate%') do (echo)";
                    }
                    // Limit AC3 bitrate to 640k through cmd.exe
                    else if (codec_SelectedItem == "AC3")
                    {
                        aBitrateLimiter = "& (IF %A gtr 640000 (SET aBitrate=640000) ELSE (echo Bitrate within AC3 Limit of 640k)) & for /F %A in ('echo %aBitrate%') do (echo)";
                    }
                    // Limit MP2 bitrate to 384k through cmd.exe
                    else if (codec_SelectedItem == "MP2")
                    {
                        aBitrateLimiter = "& (IF %A gtr 384000 (SET aBitrate=384000) ELSE (echo Bitrate within MP2 Limit of 384k)) & for /F %A in ('echo %aBitrate%') do (echo)";
                    }
                    // Limit LAME bitrate to 320k through cmd.exe
                    else if (codec_SelectedItem == "LAME")
                    {
                        aBitrateLimiter = "& (IF %A gtr 320000 (SET aBitrate=320000) ELSE (echo Bitrate within LAME Limit of 320k)) & for /F %A in ('echo %aBitrate%') do (echo)";
                    }
                }
            }

            // Return Value
            return aBitrateLimiter;
        }



        /// <summary>
        ///     Batch Audio Quality Auto
        /// <summary>
        public static String BatchAudioQualityAuto(bool batch_IsChecked,
                                                   string mediaType_SelectedItem,
                                                   string codec_SelectedItem,
                                                   string stream_SelectedItem,
                                                   string quality_SelectedItem)
        {
            // Audio Bitrate None Check
            // Audio Codec None
            // Audio Codec Copy Check
            // Mute Check
            // Stream None Check
            // Media Type Check
            if (quality_SelectedItem != "None" &&
                quality_SelectedItem != "Mute" &&
                codec_SelectedItem != "None" &&
                codec_SelectedItem != "Copy" &&
                stream_SelectedItem != "none" &&
                mediaType_SelectedItem != "Image" &&
                mediaType_SelectedItem != "Sequence")
            {
                // -------------------------
                // Batch Auto
                // -------------------------
                if (batch_IsChecked == true)
                {
                    // -------------------------
                    // Batch Audio Auto Bitrates
                    // -------------------------

                    // Batch CMD Detect
                    //
                    if (quality_SelectedItem == "Auto")
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
                        batchAudioAuto = string.Join(" ", BatchAudioAutoList
                                                          .Where(s => !string.IsNullOrEmpty(s)));
                    }
                }
            }

            // Return Value
            return batchAudioAuto;
        }



        /// <summary>
        ///     Volume
        /// <summary>
        public static void Volume(ViewModel vm)
        {
            // -------------------------
            // Only if Audio Codec is Not Empty
            // -------------------------
            if (!string.IsNullOrEmpty(vm.Audio_Codec_SelectedItem))
            {
                // If TextBox is 100% or Empty
                if (vm.Audio_Volume_Text == "100%" ||
                    vm.Audio_Volume_Text == "100" ||
                    string.IsNullOrEmpty(vm.Audio_Volume_Text))
                {
                    aVolume = string.Empty;
                }
                // If User Custom Entered Value
                // Convert Volume % to Decimal
                else
                {
                    // If user enters value, turn on Filter
                    string volumePercent = vm.Audio_Volume_Text;
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
                Log.logParagraph.Inlines.Add(new Run(vm.Audio_Volume_Text) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);
        }



        /// <summary>
        ///     Hard Limiter Filter (Method)
        /// <summary>
        public static void HardLimiter(ViewModel vm)
        {
            double value = vm.Audio_HardLimiter_Value;

            // If enabled and not default
            if (vm.Audio_HardLimiter_IsEnabled == true && 
                value != 1)
            {
                aHardLimiter = "alimiter=level_in=1:level_out=1:limit=" + Convert.ToString(Math.Round(value, 2)) + ":attack=7:release=100:level=disabled";

                // Add to Audio Filters
                AudioFilters.aFiltersList.Add(aHardLimiter);
            }
        }


    }
}
