/* ----------------------------------------------------------------------
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

 * Lowpass
 * Highpass
 * Contrast
 * Extra Stereo
 * Headphones
 * Tempo
 * Audio Filter Combine
---------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media;
using ViewModel;
using Axiom;
using System.Globalization;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Filters
{
    /// <summary>
    /// Audio Filters (Class)
    /// <summary>
    public class Audio
    {
        // Filter Lists
        public static List<string> aFiltersList = new List<string>(); // Filters to String Join
        public static string aFilter { get; set; }

        public static string aVolume { get; set; }
        public static string aHardLimiter { get; set; }


        /// <summary>
        /// Audio Filters - Enable All 
        /// <summary>
        public static void AudioFilters_EnableAll()
        {
            // Lowpass
            VM.FilterAudioView.FilterAudio_Lowpass_IsEnabled = true;
            // Highpass
            VM.FilterAudioView.FilterAudio_Highpass_IsEnabled = true;

            // Contrast
            VM.FilterAudioView.FilterAudio_Contrast_IsEnabled = true;
            // ExtraStereo
            VM.FilterAudioView.FilterAudio_ExtraStereo_IsEnabled = true;

            // Headphones
            VM.FilterAudioView.FilterAudio_Headphones_IsEnabled = true;

            // Tempo
            VM.FilterAudioView.FilterAudio_Tempo_IsEnabled = true;
        }

        /// <summary>
        /// Audio Filters - Disable All 
        /// <summary>
        public static void AudioFilters_DisableAll()
        {
            // Lowpass
            VM.FilterAudioView.FilterAudio_Lowpass_IsEnabled = false;
            // Highpass
            VM.FilterAudioView.FilterAudio_Highpass_IsEnabled = false;

            // Contrast
            VM.FilterAudioView.FilterAudio_Contrast_IsEnabled = false;
            // ExtraStereo
            VM.FilterAudioView.FilterAudio_ExtraStereo_IsEnabled = false;

            // Headphones
            VM.FilterAudioView.FilterAudio_Headphones_IsEnabled = false;

            // Tempo
            VM.FilterAudioView.FilterAudio_Tempo_IsEnabled = false;
        }

        /// <summary>
        /// Audio Filters - Controls Select Defaults
        /// <summary>
        public static void AudioFilters_ControlsSelectDefaults()
        {
            // Lowpass
            VM.FilterAudioView.FilterAudio_Lowpass_SelectedItem = "disabled";
            // Highpass
            VM.FilterAudioView.FilterAudio_Highpass_SelectedItem = "disabled";

            // Contrast
            VM.FilterAudioView.FilterAudio_Contrast_Value = 0;
            // ExtraStereo
            VM.FilterAudioView.FilterAudio_ExtraStereo_Value = 0;

            // Headphones
            VM.FilterAudioView.FilterAudio_Headphones_SelectedItem = "disabled";

            // Tempo
            VM.FilterAudioView.FilterAudio_Tempo_Value = 100;
        }


        /// <summary>
        /// Filter Audio SelectiveColor Reset All (Method)
        /// <summary>
        public static void FilterAudio_SelectiveColor_ResetAll()
        {

        }


        /// <summary>
        /// Volume
        /// <summary>
        public static void Volume()
        {
            // -------------------------
            // Only if Audio Codec is Not Empty
            // -------------------------
            if (!string.IsNullOrWhiteSpace(VM.AudioView.Audio_Codec_SelectedItem))
            {
                // If TextBox is 100% or Empty
                if (VM.AudioView.Audio_Volume_Text == "100%" ||
                    VM.AudioView.Audio_Volume_Text == "100" ||
                    string.IsNullOrWhiteSpace(VM.AudioView.Audio_Volume_Text))
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

                        aVolume = "volume=" + volumeDecimal.ToString("0.##", CultureInfo.GetCultureInfo("en-US"));

                        // Audio Filter Add
                        aFiltersList.Add(aVolume);
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

                        limit = //Convert.ToString(
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
                                                ).ToString("0.####", CultureInfo.GetCultureInfo("en-US"));
                                            //);
                    }

                    // -4 to -7dB
                    else if (value <= -4 && value >= -7)
                    {
                        double inputMin = -7;
                        double inputMax = -4;
                        double normMin = 0.45;
                        double normMax = 0.65;

                        limit = //Convert.ToString(
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
                                                ).ToString("0.####", CultureInfo.GetCultureInfo("en-US"));
                                            //);
                    }

                    // -8 to -10dB
                    else if (value <= -8 && value >= -10)
                    {
                        double inputMin = -10;
                        double inputMax = -8;
                        double normMin = 0.3;
                        double normMax = 0.4;

                        limit = //Convert.ToString(
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
                                                ).ToString("0.####", CultureInfo.GetCultureInfo("en-US"));
                                            //);
                    }

                    // -11 to -16dB
                    else if (value <= -11 && value >= -16)
                    {
                        double inputMin = -16;
                        double inputMax = -11;
                        double normMin = 0.15;
                        double normMax = 0.275;

                        limit = //Convert.ToString(
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
                                                ).ToString("0.####", CultureInfo.GetCultureInfo("en-US"));
                                            //);
                    }

                    // -17 to -19dB
                    else if (value <= -17 && value >= -19)
                    {
                        double inputMin = -19;
                        double inputMax = -17;
                        double normMin = 0.1;
                        double normMax = 0.135;

                        limit = //Convert.ToString(
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
                                                ).ToString("0.####", CultureInfo.GetCultureInfo("en-US"));
                                        //);
                    }

                    // -20 to -24dB
                    else if (value <= -20)
                    {
                        double inputMin = -24;
                        double inputMax = -20;
                        double normMin = 0.0625;
                        double normMax = 0.0975;

                        limit = //Convert.ToString(
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
                                                ).ToString("0.####", CultureInfo.GetCultureInfo("en-US"));
                                            //);
                    }

                    aHardLimiter = "alimiter=level_in=1:level_out=1:limit=" + limit + ":attack=7:release=100:level=disabled";

                    // Add to Filters List
                    aFiltersList.Add(aHardLimiter);
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


        /// <summary>
        /// Remove Click (Method)
        /// <summary>
        //public static void RemoveClick_Filter(MainWindow mainwindow)
        //{
        //    // FFmpeg Range 1 to 100
        //    // FFmpeg Default 2
        //    // Slider 0 to 100
        //    // Slider Default 0
        //    // Limit to 2 decimal places

        //    double value = VM.FilterAudioView.FilterAudio_RemoveClick.Value;

        //    string adeclick = string.Empty;

        //    if (value != 0)
        //    {
        //        adeclick = "adeclick=t=" + Convert.ToString(value);

        //        // Add to Filters List
        //        aFiltersList.Add(adeclick);
        //    }
        //}


        /// <summary>
        /// Lowpass (Method)
        /// <summary>
        public static void Lowpass_Filter()
        {
            if (VM.FilterAudioView.FilterAudio_Lowpass_SelectedItem == "enabled")
            {
                // -------------------------
                // Add Filter to List
                // -------------------------
                aFiltersList.Add("lowpass");
            }
        }

        /// <summary>
        /// Highpass (Method)
        /// <summary>
        public static void Highpass_Filter()
        {
            if (VM.FilterAudioView.FilterAudio_Highpass_SelectedItem == "enabled")
            {
                // -------------------------
                // Add Filter to List
                // -------------------------
                aFiltersList.Add("highpass");
            }
        }


        /// <summary>
        /// Contrast (Method)
        /// <summary>
        public static void Contrast_Filter()
        {
            // FFmpeg Range 0 to 100
            // FFmpeg Default 33
            // Slider 0 to 100
            // Slider Default 0
            // Limit to 2 decimal places

            double value = VM.FilterAudioView.FilterAudio_Contrast_Value;

            string acontrast = string.Empty;

            if (value != 0)
            {
                acontrast = "acontrast=" + Convert.ToString(value);

                // Add to Filters List
                aFiltersList.Add(acontrast);
            }
        }


        /// <summary>
        /// Extra Stereo (Method)
        /// <summary>
        public static void ExtraStereo_Filter()
        {
            // FFmpeg Range 0 to ??
            // FFmpeg Default 2.5
            // Slider -100 to 100
            // Slider Default 0
            // Limit to 2 decimal places

            double value = VM.FilterAudioView.FilterAudio_ExtraStereo_Value;

            string extrastereo = string.Empty;

            if (value != 0)
            {
                try
                {
                    extrastereo = "extrastereo=" +
                                    Convert.ToString(
                                            Math.Round(
                                                MainWindow.NormalizeValue(
                                                                      value, // input
                                                                       -100, // input min
                                                                        100, // input max
                                                                          0, // normalize min
                                                                         10, // normalize max
                                                                        2.5  // ffmpeg default
                                                            )

                                                        , 3 // max decimal places
                                                    )
                                                );

                    // Add to Filters List
                    aFiltersList.Add(extrastereo);
                }
                catch
                {
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Error: Could not set Extra Stereo.")) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }
        }


        /// <summary>
        /// Headphones (Method)
        /// <summary>
        public static void Headphones_Filter()
        {
            if (VM.FilterAudioView.FilterAudio_Headphones_SelectedItem == "enabled")
            {
                // -------------------------
                // Add Filter to List
                // -------------------------
                aFiltersList.Add("earwax");
            }
        }


        /// <summary>
        /// Tempo (Method)
        /// <summary>
        public static void Tempo_Filter()
        {
            // FFmpeg Range 0.5 to 2
            // FFmpeg Default 1.0
            // Slider 50 to 200
            // Slider Default 100
            // Limit to 2 decimal places

            // Example: Slow down audio to 80% tempo: atempo=0.8
            //          Speed up audio to 200% tempo: atempo=2

            double value = VM.FilterAudioView.FilterAudio_Tempo_Value;

            string tempo = string.Empty;

            if (value != 100)
            {
                tempo = "atempo=" + Convert.ToString(Math.Round(value * 0.01, 2)); // convert to decimal

                // Add to Filters List
                aFiltersList.Add(tempo);
            }
        }


        /// <summary>
        /// Audio Filter Combine (Method)
        /// <summary>
        public static String AudioFilter()
        {
            // Audio BitRate None Check
            // Audio Codec None
            // Codec Copy Check
            // Mute Check
            // Stream None Check
            // Media Type Check
            if (VM.AudioView.Audio_Quality_SelectedItem != "None" &&
                VM.AudioView.Audio_Codec_SelectedItem != "None" &&
                VM.AudioView.Audio_Codec_SelectedItem != "Copy" &&
                VM.AudioView.Audio_Quality_SelectedItem != "Mute" &&
                VM.AudioView.Audio_Stream_SelectedItem != "none" &&
                VM.FormatView.Format_MediaType_SelectedItem != "Image" &&
                VM.FormatView.Format_MediaType_SelectedItem != "Sequence")
            {
                // --------------------------------------------------
                // Filters
                // --------------------------------------------------
                // -------------------------
                // Volume
                // -------------------------
                Volume();

                // -------------------------
                // Hard Limiter
                // -------------------------
                HardLimiter();

                // -------------------------
                // Remove Click
                // -------------------------
                //RemoveClick_Filter(mainwindow);

                // -------------------------
                // Lowpass
                // -------------------------
                Lowpass_Filter();

                // -------------------------
                // Highpass
                // -------------------------
                Highpass_Filter();

                // -------------------------
                // Contrast
                // -------------------------
                Contrast_Filter();

                // -------------------------
                // Extra Stereo
                // -------------------------
                ExtraStereo_Filter();

                // -------------------------
                // Headphones
                // -------------------------
                Headphones_Filter();

                // -------------------------
                // Tempo
                // -------------------------
                Tempo_Filter();


                // -------------------------
                // Filter Combine
                // -------------------------
                if (VM.AudioView.Audio_Codec_SelectedItem != "None") // None Check
                {
                    // -------------------------
                    // Remove Empty Strings
                    // -------------------------
                    aFiltersList.RemoveAll(s => string.IsNullOrEmpty(s));

                    // -------------------------
                    // 1 Filter
                    // -------------------------
                    if (aFiltersList.Count == 1)
                    {
                        // Always wrap in quotes
                        //aFilter = "-af \"" + string.Join(", \r\n\r\n", aFiltersList
                        //                           .Where(s => !string.IsNullOrEmpty(s)))
                        //                           + "\"";
                        aFilter = "-af " + MainWindow.WrapWithQuotes(string.Join("", aFiltersList
                                                                                     .Where(s => !string.IsNullOrEmpty(s))
                                                                                )
                                                                    );
                    }

                    // -------------------------
                    // Multiple Filters
                    // -------------------------
                    else if (aFiltersList.Count > 1)
                    {
                        // Always wrap in quotes
                        // Linebreak beginning and end
                        aFilter = "-af " + MainWindow.WrapWithQuotes("\r\n" +
                                                                     string.Join(", \r\n\r\n", aFiltersList
                                                                                               .Where(s => !string.IsNullOrEmpty(s))
                                                                                ) +
                                                                     "\r\n"
                                                                    );

                        //System.Windows.MessageBox.Show(aFilter); //debug
                    }

                    // -------------------------
                    // Empty
                    // -------------------------
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
            }

            // -------------------------
            // Filter Clear
            // -------------------------
            else
            {
                aFilter = string.Empty;

                if (aFiltersList != null)
                {
                    aFiltersList.Clear();
                    aFiltersList.TrimExcess();
                }
            }


            // Return Value
            return aFilter;
        }


    }
}
