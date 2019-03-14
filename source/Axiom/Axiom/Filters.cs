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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    /// <summary>
    ///     Video Filters (Class)
    /// <summary>
    public class VideoFilters
    {
        // Filter
        public static List<string> vFiltersList = new List<string>(); // Master Filters List
        public static string geq; // png transparent to jpg whtie background filter
        public static string vFilter;


        /// <summary>
        ///     PNG to JPG (Method)
        /// <summary>
        public static void PNGtoJPG_Filter(ViewModel vm)
        {
            if (vm.VideoCodec_SelectedItem == "JPEG")
            {
                // Turn on PNG to JPG Filter
                if (string.Equals(MainWindow.inputExt, ".png", StringComparison.CurrentCultureIgnoreCase)
                    //|| string.Equals(MainWindow.batchExt, "png", StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // png transparent to white background
                    geq = "format=yuva444p,geq='if(lte(alpha(X,Y),16),255,p(X,Y))':'if(lte(alpha(X,Y),16),128,p(X,Y))':'if(lte(alpha(X,Y),16),128,p(X,Y))'";

                    // Video Filter Add
                    vFiltersList.Add(geq);
                }
                else
                {
                    geq = string.Empty;
                }
            }
        }


        /// <summary>
        /// Subtitles Burn Filter (Method)
        /// <summary>
        public static void SubtitlesBurn_Filter(ViewModel vm)
        {
            string burn = string.Empty;

            if (vm.SubtitleCodec_SelectedItem == "Burn" &&
                Subtitle.subtitleFileNamesList.Count > 0)
            {
                // Join File Names List
                //string files = string.Join(",", subtitleFileNamesList.Where(s => !string.IsNullOrEmpty(s)));

                //// Create Subtitles Filter
                //string subtitles = "subtitles=" + files + ":force_style='FontName=Arial,FontSize=22'" + style;

                // -------------------------
                // Get First Subtitle File
                // -------------------------
                string file = Subtitle.subtitleFilePathsList.First().Replace("\"", "'");

                // -------------------------
                // Create Subtitles Filter
                // -------------------------
                burn = "subtitles=" + file;

                // -------------------------
                // Add Filter to List
                // -------------------------
                vFiltersList.Add(burn);
            }
        }


        /// <summary>
        ///     Deband (Method)
        /// <summary>
        public static void Deband_Filter(ViewModel vm)
        {
            //if ((string)mainwindow.cboFilterVideo_Deband.SelectedItem == "enabled")
            //if (ViewModel.Filters.cboFilterVideo_Deband_SelectedItem == "enabled")
            if (vm.FilterVideo_Deband_SelectedItem == "enabled")
            {
                // -------------------------
                // Add Filter to List
                // -------------------------
                vFiltersList.Add("deband");
            }
        }


        /// <summary>
        ///     Deshake (Method)
        /// <summary>
        public static void Deshake_Filter(ViewModel vm)
        {
            if (vm.FilterVideo_Deshake_SelectedItem == "enabled")
            {
                // -------------------------
                // Add Filter to List
                // -------------------------
                vFiltersList.Add("deshake");
            }
        }


        /// <summary>
        ///     Deflicker (Method)
        /// <summary>
        public static void Deflicker_Filter(ViewModel vm)
        {
            if (vm.FilterVideo_Deflicker_SelectedItem == "enabled")
            {
                // -------------------------
                // Add Filter to List
                // -------------------------
                vFiltersList.Add("deflicker");
            }
        }


        /// <summary>
        ///     Dejudder (Method)
        /// <summary>
        public static void Dejudder_Filter(ViewModel vm)
        {
            if (vm.FilterVideo_Dejudder_SelectedItem == "enabled")
            {
                // -------------------------
                // Add Filter to List
                // -------------------------
                vFiltersList.Add("dejudder");
            }
        }


        /// <summary>
        ///     Denoise (Method)
        /// <summary>
        public static void Denoise_Filter(ViewModel vm)
        {
            if (vm.FilterVideo_Denoise_SelectedItem != "disabled")
            {
                string denoise = string.Empty;

                // -------------------------
                // Default
                // -------------------------
                if (vm.FilterVideo_Denoise_SelectedItem == "default")
                {
                    denoise = "removegrain=0";
                }
                // -------------------------
                // Light
                // -------------------------
                else if (vm.FilterVideo_Denoise_SelectedItem == "light")
                {
                    denoise = "removegrain=22";
                }
                // -------------------------
                // Medium
                // -------------------------
                else if (vm.FilterVideo_Denoise_SelectedItem == "medium")
                {
                    denoise = "vaguedenoiser=threshold=3:method=soft:nsteps=5";
                }
                // -------------------------
                // Heavy
                // -------------------------
                else if (vm.FilterVideo_Denoise_SelectedItem == "heavy")
                {
                    denoise = "vaguedenoiser=threshold=6:method=soft:nsteps=5";
                }

                // -------------------------
                // Add Filter to List
                // -------------------------
                vFiltersList.Add(denoise);
            }
        }



        /// <summary>
        ///     Selective Color Class
        /// <summary>
        public static List<FilterVideoSelectiveColor> SelectiveColorList { get; set; }
        public partial class FilterVideoSelectiveColor
        {
            public string SelectiveColorName { get; set; }
            public Color SelectiveColorPreview { get; set; }
            public string SelectiveColorPreviewStr { get { return SelectiveColorPreview.ToString(); } }
            public FilterVideoSelectiveColor(string name, Color color)
            {
                SelectiveColorName = name;
                SelectiveColorPreview = color;
            }
        }

        /// <summary>
        ///     Selective Color Normalize (Method)
        /// <summary>
        public static String SelectiveColor_Normalize(double value)
        {
            // FFmpeg Range -1 to 1
            // Slider -100 to 100
            // Limit to 2 decimal places

            string decimalValue = Convert.ToString(
                                        Math.Round(
                                            MainWindow.NormalizeValue(
                                                           value, // input
                                                            -100, // input min
                                                             100, // input max
                                                              -1, // normalize min
                                                               1, // normalize max
                                                               0  // ffmpeg default
                                                        )
                                                    , 2
                                                )
                                            );

            return decimalValue;

        }

        /// <summary>
        ///     Selective SelectiveColorPreview (Method)
        /// <summary>
        public static void SelectiveColor_Filter(ViewModel vm)
        {
            string selectiveColor = string.Empty;

            List<double> selectiveColorSliders = new List<double>()
            {
                // Reds
                vm.FilterVideo_SelectiveColor_Reds_Cyan_Value,
                vm.FilterVideo_SelectiveColor_Reds_Magenta_Value,
                vm.FilterVideo_SelectiveColor_Reds_Yellow_Value,
                // Yellows
                vm.FilterVideo_SelectiveColor_Yellows_Cyan_Value,
                vm.FilterVideo_SelectiveColor_Yellows_Magenta_Value,
                vm.FilterVideo_SelectiveColor_Yellows_Yellow_Value,
                // Greens
                vm.FilterVideo_SelectiveColor_Greens_Cyan_Value,
                vm.FilterVideo_SelectiveColor_Greens_Magenta_Value,
                vm.FilterVideo_SelectiveColor_Greens_Yellow_Value,
                // Cyans
                vm.FilterVideo_SelectiveColor_Cyans_Cyan_Value,
                vm.FilterVideo_SelectiveColor_Cyans_Magenta_Value,
                vm.FilterVideo_SelectiveColor_Cyans_Yellow_Value,
                // Blues
                vm.FilterVideo_SelectiveColor_Blues_Cyan_Value,
                vm.FilterVideo_SelectiveColor_Blues_Magenta_Value,
                vm.FilterVideo_SelectiveColor_Blues_Yellow_Value,
                // Magentas
                vm.FilterVideo_SelectiveColor_Magentas_Cyan_Value,
                vm.FilterVideo_SelectiveColor_Magentas_Magenta_Value,
                vm.FilterVideo_SelectiveColor_Magentas_Yellow_Value,
                // Whites
                vm.FilterVideo_SelectiveColor_Whites_Cyan_Value,
                vm.FilterVideo_SelectiveColor_Whites_Magenta_Value,
                vm.FilterVideo_SelectiveColor_Whites_Yellow_Value,
                // Neutrals
                vm.FilterVideo_SelectiveColor_Neutrals_Cyan_Value,
                vm.FilterVideo_SelectiveColor_Neutrals_Magenta_Value,
                vm.FilterVideo_SelectiveColor_Neutrals_Yellow_Value,
                // Blacks
                vm.FilterVideo_SelectiveColor_Blacks_Cyan_Value,
                vm.FilterVideo_SelectiveColor_Blacks_Magenta_Value,
                vm.FilterVideo_SelectiveColor_Blacks_Yellow_Value,
            };

            // -------------------------
            // Enable if at least one Slider is active
            // Combines values of all sliders
            // -------------------------
            if (selectiveColorSliders.Sum() != 0)
            {
                // -------------------------
                // Reds
                // -------------------------
                // Cyan
                string reds_cyan = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Reds_Cyan_Value);
                // Magenta
                string reds_magenta = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Reds_Magenta_Value);
                // Yellow
                string reds_yellow = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Reds_Yellow_Value);

                // -------------------------
                // Yellows
                // -------------------------
                // Cyan
                string yellows_cyan = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Yellows_Cyan_Value);
                // Magenta
                string yellows_magenta = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Yellows_Magenta_Value);
                // Yellow
                string yellows_yellow = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Yellows_Yellow_Value);

                // -------------------------
                // Greens
                // -------------------------
                // Cyan
                string greens_cyan = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Greens_Cyan_Value);
                // Magenta
                string greens_magenta = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Greens_Magenta_Value);
                // Yellow
                string greens_yellow = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Greens_Yellow_Value);

                // -------------------------
                // Cyans
                // -------------------------
                // Cyan
                string cyans_cyan = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Cyans_Cyan_Value);
                // Magenta
                string cyans_magenta = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Cyans_Magenta_Value);
                // Yellow
                string cyans_yellow = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Cyans_Yellow_Value);

                // -------------------------
                // Blues
                // -------------------------
                // Cyan
                string blues_cyan = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Blues_Cyan_Value);
                // Magenta
                string blues_magenta = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Blues_Magenta_Value);
                // Yellow
                string blues_yellow = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Blues_Yellow_Value);

                // -------------------------
                // Magentas
                // -------------------------
                // Cyan
                string magentas_cyan = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Magentas_Cyan_Value);
                // Magenta
                string magentas_magenta = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Magentas_Magenta_Value);
                // Yellow
                string magentas_yellow = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Magentas_Yellow_Value);

                // -------------------------
                // Whites
                // -------------------------
                // Cyan
                string whites_cyan = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Whites_Cyan_Value);
                // Magenta
                string whites_magenta = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Whites_Magenta_Value);
                // Yellow
                string whites_yellow = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Whites_Yellow_Value);

                // -------------------------
                // Nuetrals
                // -------------------------
                // Cyan
                string neutrals_cyan = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Neutrals_Cyan_Value);
                // Magenta
                string neutrals_magenta = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Neutrals_Magenta_Value);
                // Yellow
                string neutrals_yellow = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Neutrals_Yellow_Value);

                // -------------------------
                // Blacks
                // -------------------------
                // Cyan
                string blacks_cyan = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Blacks_Cyan_Value);
                // Magenta
                string blacks_magenta = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Blacks_Magenta_Value);
                // Yellow
                string blacks_yellow = SelectiveColor_Normalize(vm.FilterVideo_SelectiveColor_Blacks_Yellow_Value);

                // -------------------------
                // Combine
                // -------------------------
                List<string> selectiveColorList = new List<string>()
                {
                    "selectivecolor="
                    + "\r\n"
                    + "correction_method=" + vm.FilterVideo_SelectiveColor_Correction_Method_SelectedItem
                    + "\r\n",

                    "reds="     + reds_cyan     + " " + reds_magenta     + " " + reds_yellow     + "\r\n",
                    "yellows="  + yellows_cyan  + " " + yellows_magenta  + " " + yellows_yellow  + "\r\n",
                    "greens="   + greens_cyan   + " " + greens_magenta   + " " + greens_yellow   + "\r\n",
                    "cyans="    + cyans_cyan    + " " + cyans_magenta    + " " + cyans_yellow    + "\r\n",
                    "blues="    + blues_cyan    + " " + blues_magenta    + " " + blues_yellow    + "\r\n",
                    "magentas=" + magentas_cyan + " " + magentas_magenta + " " + magentas_yellow + "\r\n",
                    "whites="   + whites_cyan   + " " + whites_magenta   + " " + whites_yellow   + "\r\n",
                    "neutrals=" + neutrals_cyan + " " + neutrals_magenta + " " + neutrals_yellow + "\r\n",
                    "blacks="   + blacks_cyan   + " " + blacks_magenta   + " " + blacks_yellow   + "\r\n",
                };

                selectiveColor = string.Join(":", selectiveColorList
                                       .Where(s => !string.IsNullOrEmpty(s))
                                       );

                // -------------------------
                // Add Filter to List
                // -------------------------
                vFiltersList.Add(selectiveColor);
            }
        }


        /// <summary>
        ///     Video EQ (Method)
        /// <summary>
        public static void Video_EQ_Filter(ViewModel vm)
        {
            if (vm.FilterVideo_EQ_Brightness_Value != 0 ||
                vm.FilterVideo_EQ_Contrast_Value != 0 ||
                vm.FilterVideo_EQ_Saturation_Value != 0 ||
                vm.FilterVideo_EQ_Gamma_Value != 0)
            {
                // EQ List
                List<string> vEQ_Filter_List = new List<string>()
                {
                    // EQ Brightness
                    VideoFilters.Video_EQ_Brightness_Filter(vm),
                    // Contrast
                    VideoFilters.Video_EQ_Contrast_Filter(vm),
                    // Struation
                    VideoFilters.Video_EQ_Saturation_Filter(vm),
                    // Gamma
                    VideoFilters.Video_EQ_Gamma_Filter(vm),
                };

                // Join
                string filters = string.Join("\r\n:", vEQ_Filter_List
                                       .Where(s => !string.IsNullOrEmpty(s)));

                // Combine
                vFiltersList.Add("eq=\r\n" + filters);
            }
        }


        /// <summary>
        ///     Video EQ - Brightness (Method)
        /// <summary>
        public static String Video_EQ_Brightness_Filter(ViewModel vm)
        {
            double value = vm.FilterVideo_EQ_Brightness_Value;

            string brightness = string.Empty;

            if (value != 0)
            {
                // FFmpeg Range -1 to 1
                // FFmpeg Default 0
                // Slider -100 to 100
                // Slider Default 0
                // Limit to 2 decimal places

                //brightness = "brightness=" + vm.FilterVideo_EQ_Brightness.Value.ToString();

                try
                {
                    brightness = "brightness=" +
                                        Convert.ToString(
                                                Math.Round(
                                                        MainWindow.NormalizeValue(
                                                                       value, // input
                                                                        -100, // input min
                                                                         100, // input max
                                                                          -1, // normalize min
                                                                           1, // normalize max
                                                                           0  // ffmpeg default
                                                            )
                                                        , 2
                                                    )
                                                );
                }
                catch
                {
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Error: Could not set Brightness.")) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }

            return brightness;
        }

        /// <summary>
        ///     Video EQ - Contrast (Method)
        /// <summary>
        public static String Video_EQ_Contrast_Filter(ViewModel vm)
        {
            double value = vm.FilterVideo_EQ_Contrast_Value;

            string contrast = string.Empty;

            if (value != 0)
            {
                // FFmpeg Range -2 to 2
                // FFmpeg Default 1
                // Slider -100 to 100
                // Slider Default 0
                // Limit to 2 decimal places

                //contrast = "contrast=" + vm.FilterVideo_EQ_Contrast.Value.ToString();

                try
                {
                    contrast = "contrast=" +
                                Convert.ToString(
                                        Math.Round(
                                            MainWindow.NormalizeValue(
                                                                    value, // input
                                                                     -100, // input min
                                                                      100, // input max
                                                                       -2, // normalize min
                                                                        2, // normalize max
                                                                        1  // ffdefault
                                                        )

                                                    , 2 // max decimal places
                                                )
                                            );
                }
                catch
                {
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Error: Could not set Contrast.")) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }

            }

            return contrast;
        }

        /// <summary>
        ///     Video EQ - Saturation (Method)
        /// <summary>
        public static String Video_EQ_Saturation_Filter(ViewModel vm)
        {
            double value = vm.FilterVideo_EQ_Saturation_Value;

            string saturation = string.Empty;

            if (value != 0)
            {
                // FFmpeg Range 0 to 3
                // FFmpeg Default 1
                // Slider -100 to 100
                // Slider Default 0
                // Limit to 2 decimal places

                //saturation = "saturation=" + vm.FilterVideo_EQ_Saturation.Value.ToString();

                try
                {
                    saturation = "saturation=" +
                                    Convert.ToString(
                                            Math.Round(
                                                MainWindow.NormalizeValue(
                                                                       value, // input
                                                                        -100, // input min
                                                                         100, // input max
                                                                           0, // normalize min
                                                                           3, // normalize max
                                                                           1  // ffmpeg default
                                                            )

                                                        , 2 // max decimal places
                                                    )
                                                );
                }
                catch
                {
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Error: Could not set Saturation.")) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }

            }

            return saturation;
        }

        /// <summary>
        ///     Video EQ - Gamma (Method)
        /// <summary>
        public static String Video_EQ_Gamma_Filter(ViewModel vm)
        {
            double value = vm.FilterVideo_EQ_Gamma_Value;

            string gamma = string.Empty;

            if (value != 0)
            {
                // FFmpeg Range 0.1 to 10
                // FFmpeg Default 1
                // Slider -100 to 100
                // Slider Default 0
                // Limit to 2 decimal places

                //gamma = "gamma=" + vm.FilterVideo_EQ_Gamma.Value.ToString();
                try
                {
                    gamma = "gamma=" +
                                    Convert.ToString(
                                            Math.Round(
                                                MainWindow.NormalizeValue(
                                                                      value, // input
                                                                       -100, // input min
                                                                        100, // input max
                                                                        0.1, // normalize min
                                                                         10, // normalize max
                                                                          1  // ffmpeg default
                                                            )

                                                        , 2 // max decimal places
                                                    )
                                                );
                }
                catch
                {
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Error: Could not set Gamma.")) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
            }

            return gamma;
        }



        /// <summary>
        ///     Video Filter Combine (Method)
        /// <summary>
        public static String VideoFilter(ViewModel vm)
        {
            // Video Bitrate None Check
            // Video Codec None Check
            // Codec Copy Check
            // Media Type Check
            if (vm.VideoQuality_SelectedItem != "None" &&
                vm.VideoCodec_SelectedItem != "None" &&
                vm.VideoCodec_SelectedItem != "Copy" &&
                vm.MediaType_SelectedItem != "Audio")
            {
                // --------------------------------------------------
                // Add Each Filter to Master Filters List
                // --------------------------------------------------

                // -------------------------
                //  Crop
                // -------------------------
                Video.Crop(MainWindow.cropwindow, vm);

                // -------------------------
                //  Resize
                // -------------------------
                Video.Size(vm);

                // -------------------------
                // PNG to JPEG
                // -------------------------
                VideoFilters.PNGtoJPG_Filter( vm);

                // -------------------------
                //    Subtitles Burn
                // -------------------------
                VideoFilters.SubtitlesBurn_Filter(/*mainwindow*/ vm);

                // -------------------------
                //  Deband
                // -------------------------
                VideoFilters.Deband_Filter(vm);

                // -------------------------
                //  Deshake
                // -------------------------
                VideoFilters.Deshake_Filter(vm);

                // -------------------------
                //  Deflicker
                // -------------------------
                VideoFilters.Deflicker_Filter(vm);

                // -------------------------
                //  Dejudder
                // -------------------------
                VideoFilters.Dejudder_Filter(vm);

                // -------------------------
                //  Denoise
                // -------------------------
                VideoFilters.Denoise_Filter(vm);

                // -------------------------
                //  EQ - Brightness, Contrast, Saturation, Gamma
                // -------------------------
                VideoFilters.Video_EQ_Filter(vm);

                // -------------------------
                //  Selective SelectiveColorPreview
                // -------------------------
                VideoFilters.SelectiveColor_Filter(vm);


                // -------------------------
                // Filter Combine
                // -------------------------
                if (vm.VideoCodec_SelectedItem != "None") // None Check
                {
                    //System.Windows.MessageBox.Show(string.Join(",\r\n\r\n", vFiltersList.Where(s => !string.IsNullOrEmpty(s)))); //debug
                    //System.Windows.MessageBox.Show(Convert.ToString(vFiltersList.Count())); //debug

                    // -------------------------
                    // 1 Filter
                    // -------------------------
                    if (vFiltersList.Count == 1)
                    {
                        // Always wrap in quotes
                        vFilter = "-vf \"" + string.Join(", \r\n\r\n", vFiltersList
                                                   .Where(s => !string.IsNullOrEmpty(s)))
                                                   + "\"";
                    }

                    // -------------------------
                    // Multiple Filters
                    // -------------------------
                    else if (vFiltersList.Count > 1)
                    {
                        // Always wrap in quotes
                        // Linebreak beginning and end
                        vFilter = "-vf \"\r\n" + string.Join(", \r\n\r\n", vFiltersList
                                                       .Where(s => !string.IsNullOrEmpty(s)))
                                                       + "\r\n\"";
                    }

                    // -------------------------
                    // Empty
                    // -------------------------
                    else
                    {
                        vFilter = string.Empty;
                    }
                }

                // -------------------------
                // Video Codec None
                // -------------------------
                else
                {
                    vFilter = string.Empty;

                }
            }

            // Return Value
            return vFilter;
        }

    }





    /// <summary>
    ///     Audio Filters (Class)
    /// <summary>
    public class AudioFilters
    {
        // Filter Lists
        public static List<string> aFiltersList = new List<string>(); // Filters to String Join
        public static string aFilter;


        /// <summary>
        ///     Remove Click (Method)
        /// <summary>
        //public static void RemoveClick_Filter(MainWindow mainwindow)
        //{
        //    // FFmpeg Range 1 to 100
        //    // FFmpeg Default 2
        //    // Slider 0 to 100
        //    // Slider Default 0
        //    // Limit to 2 decimal places

        //    double value = vm.FilterAudio_RemoveClick.Value;

        //    string adeclick = string.Empty;

        //    if (value != 0)
        //    {
        //        adeclick = "adeclick=t=" + Convert.ToString(value);

        //        // Add to Filters List
        //        aFiltersList.Add(adeclick);
        //    }
        //}


        /// <summary>
        ///     Lowpass (Method)
        /// <summary>
        public static void Lowpass_Filter(ViewModel vm)
        {
            if (vm.FilterAudio_Lowpass_SelectedItem == "enabled")
            {
                // -------------------------
                // Add Filter to List
                // -------------------------
                aFiltersList.Add("lowpass");
            }
        }

        /// <summary>
        ///     Highpass (Method)
        /// <summary>
        public static void Highpass_Filter(ViewModel vm)
        {
            if (vm.FilterAudio_Highpass_SelectedItem == "enabled")
            {
                // -------------------------
                // Add Filter to List
                // -------------------------
                aFiltersList.Add("highpass");
            }
        }


        /// <summary>
        ///     Contrast (Method)
        /// <summary>
        public static void Contrast_Filter(ViewModel vm)
        {
            // FFmpeg Range 0 to 100
            // FFmpeg Default 33
            // Slider 0 to 100
            // Slider Default 0
            // Limit to 2 decimal places

            double value = vm.FilterAudio_Contrast_Value;

            string acontrast = string.Empty;

            if (value != 0)
            {
                acontrast = "acontrast=" + Convert.ToString(value);

                // Add to Filters List
                aFiltersList.Add(acontrast);
            }
        }


        /// <summary>
        ///     Extra Stereo (Method)
        /// <summary>
        public static void ExtraStereo_Filter(ViewModel vm)
        {
            // FFmpeg Range 0 to ??
            // FFmpeg Default 2.5
            // Slider -100 to 100
            // Slider Default 0
            // Limit to 2 decimal places

            double value = vm.FilterAudio_ExtraStereo_Value;

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
        ///     Headphones (Method)
        /// <summary>
        public static void Headphones_Filter(ViewModel vm)
        {
            if (vm.FilterAudio_Headphones_SelectedItem == "enabled")
            {
                // -------------------------
                // Add Filter to List
                // -------------------------
                aFiltersList.Add("earwax");
            }
        }


        /// <summary>
        ///     Tempo (Method)
        /// <summary>
        public static void Tempo_Filter(ViewModel vm)
        {
            // FFmpeg Range 0.5 to 2
            // FFmpeg Default 1.0
            // Slider 50 to 200
            // Slider Default 100
            // Limit to 2 decimal places

            // Example: Slow down audio to 80% tempo: atempo=0.8
            //          Speed up audio to 200% tempo: atempo=2

            double value = vm.FilterAudio_Tempo_Value;

            string tempo = string.Empty;

            if (value != 100)
            {
                tempo = "atempo=" + Convert.ToString(Math.Round(value * 0.01, 2)); // convert to decimal

                // Add to Filters List
                aFiltersList.Add(tempo);
            }
        }


        /// <summary>
        ///     Audio Filter Combine (Method)
        /// <summary>
        public static String AudioFilter(ViewModel vm)
        {
            // Audio Bitrate None Check
            // Audio Codec None
            // Codec Copy Check
            // Mute Check
            // Stream None Check
            // Media Type Check
            if (vm.AudioQuality_SelectedItem != "None"
                && vm.AudioCodec_SelectedItem != "None"
                && vm.AudioCodec_SelectedItem != "Copy"
                && vm.AudioQuality_SelectedItem != "Mute"
                && vm.AudioStream_SelectedItem != "none"
                && vm.MediaType_SelectedItem != "Image"
                && vm.MediaType_SelectedItem != "Sequence")
            {
                // --------------------------------------------------
                // Filters
                // --------------------------------------------------
                // -------------------------
                // Volume
                // -------------------------
                Audio.Volume(vm);

                // -------------------------
                // Hard Limiter
                // -------------------------
                Audio.HardLimiter(vm);

                // -------------------------
                // Remove Click
                // -------------------------
                //RemoveClick_Filter(mainwindow);

                // -------------------------
                // Lowpass
                // -------------------------
                Lowpass_Filter(vm);

                // -------------------------
                // Highpass
                // -------------------------
                Highpass_Filter(vm);

                // -------------------------
                // Contrast
                // -------------------------
                Contrast_Filter(vm);

                // -------------------------
                // Extra Stereo
                // -------------------------
                ExtraStereo_Filter(vm);

                // -------------------------
                // Headphones
                // -------------------------
                Headphones_Filter(vm);

                // -------------------------
                // Tempo
                // -------------------------
                Tempo_Filter(vm);


                // -------------------------
                // Filter Combine
                // -------------------------
                if (vm.AudioCodec_SelectedItem != "None") // None Check
                {
                    // -------------------------
                    // 1 Filter
                    // -------------------------
                    if (aFiltersList.Count == 1)
                    {
                        // Always wrap in quotes
                        aFilter = "-af \"" + string.Join(", \r\n\r\n", aFiltersList
                                                   .Where(s => !string.IsNullOrEmpty(s)))
                                                   + "\"";
                    }

                    // -------------------------
                    // Multiple Filters
                    // -------------------------
                    else if (aFiltersList.Count > 1)
                    {
                        // Always wrap in quotes
                        // Linebreak beginning and end
                        aFilter = "-af \"\r\n" + string.Join(", \r\n\r\n", aFiltersList
                                                       .Where(s => !string.IsNullOrEmpty(s)))
                                                       + "\r\n\"";

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
