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

 * PNG to JPG
 * Subtitles Burn
 * Deband
 * Deshake
 * Deflicker
 * Dejudder
 * Denoise
 * Deinterlace
 * Selective Color Normalize
 * Selective Color
 * EQ
 * EQ Brightness
 * EQ Contrast
 * EQ Saturation
 * EQ Gamma
 * Video Filter Combine
---------------------------------- */

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
    /// Video Filters (Class)
    /// <summary>
    public class VideoFilters
    {
        // Filter
        public static List<string> vFiltersList = new List<string>(); // Master Filters List
        public static string geq { get; set; } // png transparent to jpg whtie background filter
        public static string vFilter { get; set; }


        /// <summary>
        /// PNG to JPG (Method)
        /// <summary>
        public static void PNGtoJPG_Filter()
        {
            if (VM.VideoView.Video_Codec_SelectedItem == "JPEG")
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
        /// Subtitles Burn Filter
        /// <summary>
        public static void SubtitlesBurn_Filter()
        {
            string burn = string.Empty;

            if (VM.SubtitleView.Subtitle_Codec_SelectedItem == "Burn" &&
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
        /// Deband
        /// <summary>
        public static void Deband_Filter()
        {
            //if ((string)mainwindow.cboFilterVideo_Deband.SelectedItem == "enabled")
            //if (ViewModel.Filters.cboFilterVideo_Deband_SelectedItem == "enabled")
            if (VM.FilterVideoView.FilterVideo_Deband_SelectedItem == "enabled")
            {
                // -------------------------
                // Add Filter to List
                // -------------------------
                vFiltersList.Add("deband");
            }
        }


        /// <summary>
        /// Deshake
        /// <summary>
        public static void Deshake_Filter()
        {
            if (VM.FilterVideoView.FilterVideo_Deshake_SelectedItem == "enabled")
            {
                // -------------------------
                // Add Filter to List
                // -------------------------
                vFiltersList.Add("deshake");
            }
        }


        /// <summary>
        /// Deflicker (Method)
        /// <summary>
        public static void Deflicker_Filter()
        {
            if (VM.FilterVideoView.FilterVideo_Deflicker_SelectedItem == "enabled")
            {
                // -------------------------
                // Add Filter to List
                // -------------------------
                vFiltersList.Add("deflicker");
            }
        }


        /// <summary>
        /// Dejudder
        /// <summary>
        public static void Dejudder_Filter()
        {
            if (VM.FilterVideoView.FilterVideo_Dejudder_SelectedItem == "enabled")
            {
                // -------------------------
                // Add Filter to List
                // -------------------------
                vFiltersList.Add("dejudder");
            }
        }


        /// <summary>
        /// Denoise
        /// <summary>
        public static void Denoise_Filter()
        {
            if (VM.FilterVideoView.FilterVideo_Denoise_SelectedItem != "disabled")
            {
                string denoise = string.Empty;

                switch (VM.FilterVideoView.FilterVideo_Denoise_SelectedItem)
                {
                    // -------------------------
                    // Default
                    // -------------------------
                    case "default":
                        denoise = "removegrain=0";
                        break;

                    // -------------------------
                    // Light
                    // -------------------------
                    case "light":
                        denoise = "removegrain=22";
                        break;

                    // -------------------------
                    // Medium
                    // -------------------------
                    case "medium":
                        denoise = "vaguedenoiser=threshold=3:method=soft:nsteps=5";
                        break;

                    // -------------------------
                    // Heavy
                    // -------------------------
                    case "heavy":
                        denoise = "vaguedenoiser=threshold=6:method=soft:nsteps=5";
                        break;
                }
                //// -------------------------
                //// Default
                //// -------------------------
                //if (VM.FilterVideoView.FilterVideo_Denoise_SelectedItem == "default")
                //{
                //    denoise = "removegrain=0";
                //}
                //// -------------------------
                //// Light
                //// -------------------------
                //else if (VM.FilterVideoView.FilterVideo_Denoise_SelectedItem == "light")
                //{
                //    denoise = "removegrain=22";
                //}
                //// -------------------------
                //// Medium
                //// -------------------------
                //else if (VM.FilterVideoView.FilterVideo_Denoise_SelectedItem == "medium")
                //{
                //    denoise = "vaguedenoiser=threshold=3:method=soft:nsteps=5";
                //}
                //// -------------------------
                //// Heavy
                //// -------------------------
                //else if (VM.FilterVideoView.FilterVideo_Denoise_SelectedItem == "heavy")
                //{
                //    denoise = "vaguedenoiser=threshold=6:method=soft:nsteps=5";
                //}

                // -------------------------
                // Add Filter to List
                // -------------------------
                vFiltersList.Add(denoise);
            }
        }


        /// <summary>
        /// Deinterlace
        /// <summary>
        /// <remarks>
        /// https://ffmpeg.org/ffmpeg-filters.html#yadif-1
        /// </remarks>
        public static void Deinterlace_Filter()
        {
            if (VM.FilterVideoView.FilterVideo_Deinterlace_SelectedItem != "disabled")
            {
                string deinterlace = string.Empty;

                switch (VM.FilterVideoView.FilterVideo_Deinterlace_SelectedItem)
                {
                    // -------------------------
                    // Send Frame
                    // -------------------------
                    case "frame":
                        deinterlace = "yadif=0:-1:0";
                        break;

                    // -------------------------
                    // Send Field
                    // -------------------------
                    case "field":
                        deinterlace = "yadif=1:-1:0";
                        break;

                    // -------------------------
                    // Frame Skip Spatial
                    // -------------------------
                    case "frame nospatial":
                        deinterlace = "yadif=2:-1:0";
                        break;

                    // -------------------------
                    // Field Skip Spatial
                    // -------------------------
                    case "field nospatial":
                        deinterlace = "yadif=3:-1:0";
                        break;
                }
                //// -------------------------
                //// Send Frame
                //// -------------------------
                //if (VM.FilterVideoView.FilterVideo_Deinterlace_SelectedItem == "frame")
                //{
                //    deinterlace = "yadif=0:-1:0";
                //}
                //// -------------------------
                //// Send Field
                //// -------------------------
                //else if (VM.FilterVideoView.FilterVideo_Deinterlace_SelectedItem == "field")
                //{
                //    deinterlace = "yadif=1:-1:0";
                //}
                //// -------------------------
                //// Frame Skip Spatial
                //// -------------------------
                //else if (VM.FilterVideoView.FilterVideo_Deinterlace_SelectedItem == "frame nospatial")
                //{
                //    deinterlace = "yadif=2:-1:0";
                //}
                //// -------------------------
                //// Field Skip Spatial
                //// -------------------------
                //else if (VM.FilterVideoView.FilterVideo_Deinterlace_SelectedItem == "field nospatial")
                //{
                //    deinterlace = "yadif=3:-1:0";
                //}
                ////// -------------------------
                ////// Cuda Frame
                ////// -------------------------
                ////else if (VM.FilterVideoView.FilterVideo_Deinterlace_SelectedItem == "cuda frame")
                ////{
                ////    deinterlace = "yadif_cuda=0:-1:0";
                ////}
                ////// -------------------------
                ////// Cuda Field
                ////// -------------------------
                ////else if (VM.FilterVideoView.FilterVideo_Deinterlace_SelectedItem == "cuda field")
                ////{
                ////    deinterlace = "yadif_cuda=1:-1:0";
                ////}

                // -------------------------
                // Add Filter to List
                // -------------------------
                vFiltersList.Add(deinterlace);
            }
        }



        /// <summary>
        /// Selective Color Class
        /// <summary>
        //public static List<FilterVideoSelectiveColor> SelectiveColorList { get; set; }
        //public partial class FilterVideoSelectiveColor
        //{
        //    public string SelectiveColorName { get; set; }
        //    public Color SelectiveColorPreview { get; set; }
        //    public string SelectiveColorPreviewStr { get { return SelectiveColorPreview.ToString(); } }
        //    public FilterVideoSelectiveColor(string name, Color color)
        //    {
        //        SelectiveColorName = name;
        //        SelectiveColorPreview = color;
        //    }
        //}

        /// <summary>
        /// Selective Color Normalize (Method)
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
        /// Selective Color (Method)
        /// <summary>
        public static void SelectiveColor_Filter()
        {
            string selectiveColor = string.Empty;

            List<double> selectiveColorSliders = new List<double>()
            {
                // Reds
                VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Cyan_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Magenta_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Yellow_Value,
                // Yellows
                VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Cyan_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Magenta_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Yellow_Value,
                // Greens
                VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Cyan_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Magenta_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Yellow_Value,
                // Cyans
                VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Cyan_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Magenta_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Yellow_Value,
                // Blues
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Cyan_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Magenta_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Yellow_Value,
                // Magentas
                VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Cyan_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Magenta_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Yellow_Value,
                // Whites
                VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Cyan_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Magenta_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Yellow_Value,
                // Neutrals
                VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Cyan_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Magenta_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Yellow_Value,
                // Blacks
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Cyan_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Magenta_Value,
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Yellow_Value,
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
                string reds_cyan = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Cyan_Value);
                // Magenta
                string reds_magenta = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Magenta_Value);
                // Yellow
                string reds_yellow = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Yellow_Value);

                // -------------------------
                // Yellows
                // -------------------------
                // Cyan
                string yellows_cyan = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Cyan_Value);
                // Magenta
                string yellows_magenta = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Magenta_Value);
                // Yellow
                string yellows_yellow = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Yellow_Value);

                // -------------------------
                // Greens
                // -------------------------
                // Cyan
                string greens_cyan = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Cyan_Value);
                // Magenta
                string greens_magenta = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Magenta_Value);
                // Yellow
                string greens_yellow = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Yellow_Value);

                // -------------------------
                // Cyans
                // -------------------------
                // Cyan
                string cyans_cyan = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Cyan_Value);
                // Magenta
                string cyans_magenta = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Magenta_Value);
                // Yellow
                string cyans_yellow = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Yellow_Value);

                // -------------------------
                // Blues
                // -------------------------
                // Cyan
                string blues_cyan = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Cyan_Value);
                // Magenta
                string blues_magenta = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Magenta_Value);
                // Yellow
                string blues_yellow = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Yellow_Value);

                // -------------------------
                // Magentas
                // -------------------------
                // Cyan
                string magentas_cyan = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Cyan_Value);
                // Magenta
                string magentas_magenta = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Magenta_Value);
                // Yellow
                string magentas_yellow = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Yellow_Value);

                // -------------------------
                // Whites
                // -------------------------
                // Cyan
                string whites_cyan = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Cyan_Value);
                // Magenta
                string whites_magenta = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Magenta_Value);
                // Yellow
                string whites_yellow = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Yellow_Value);

                // -------------------------
                // Nuetrals
                // -------------------------
                // Cyan
                string neutrals_cyan = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Cyan_Value);
                // Magenta
                string neutrals_magenta = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Magenta_Value);
                // Yellow
                string neutrals_yellow = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Yellow_Value);

                // -------------------------
                // Blacks
                // -------------------------
                // Cyan
                string blacks_cyan = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Cyan_Value);
                // Magenta
                string blacks_magenta = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Magenta_Value);
                // Yellow
                string blacks_yellow = SelectiveColor_Normalize(VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Yellow_Value);

                // -------------------------
                // Combine
                // -------------------------
                List<string> selectiveColorList = new List<string>()
                {
                    "selectivecolor="
                    + "\r\n"
                    + "correction_method=" + VM.FilterVideoView.FilterVideo_SelectiveColor_Correction_Method_SelectedItem
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
        /// Video EQ (Method)
        /// <summary>
        public static void Video_EQ_Filter()
        {
            if (VM.FilterVideoView.FilterVideo_EQ_Brightness_Value != 0 ||
                VM.FilterVideoView.FilterVideo_EQ_Contrast_Value != 0 ||
                VM.FilterVideoView.FilterVideo_EQ_Saturation_Value != 0 ||
                VM.FilterVideoView.FilterVideo_EQ_Gamma_Value != 0)
            {
                // EQ List
                List<string> vEQ_Filter_List = new List<string>()
                {
                    // EQ Brightness
                    VideoFilters.Video_EQ_Brightness_Filter(),
                    // Contrast
                    VideoFilters.Video_EQ_Contrast_Filter(),
                    // Struation
                    VideoFilters.Video_EQ_Saturation_Filter(),
                    // Gamma
                    VideoFilters.Video_EQ_Gamma_Filter(),
                };

                // Join
                string filters = string.Join("\r\n:", vEQ_Filter_List
                                       .Where(s => !string.IsNullOrEmpty(s)));

                // Combine
                vFiltersList.Add("eq=\r\n" + filters);
            }
        }


        /// <summary>
        /// Video EQ - Brightness (Method)
        /// <summary>
        public static String Video_EQ_Brightness_Filter()
        {
            double value = VM.FilterVideoView.FilterVideo_EQ_Brightness_Value;

            string brightness = string.Empty;

            if (value != 0)
            {
                // FFmpeg Range -1 to 1
                // FFmpeg Default 0
                // Slider -100 to 100
                // Slider Default 0
                // Limit to 2 decimal places

                //brightness = "brightness=" + VM.FilterVideoView.FilterVideo_EQ_Brightness.Value.ToString();

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
        /// Video EQ - Contrast (Method)
        /// <summary>
        public static String Video_EQ_Contrast_Filter()
        {
            double value = VM.FilterVideoView.FilterVideo_EQ_Contrast_Value;

            string contrast = string.Empty;

            if (value != 0)
            {
                // FFmpeg Range -2 to 2
                // FFmpeg Default 1
                // Slider -100 to 100
                // Slider Default 0
                // Limit to 2 decimal places

                //contrast = "contrast=" + VM.FilterVideoView.FilterVideo_EQ_Contrast.Value.ToString();

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
        /// Video EQ - Saturation (Method)
        /// <summary>
        public static String Video_EQ_Saturation_Filter()
        {
            double value = VM.FilterVideoView.FilterVideo_EQ_Saturation_Value;

            string saturation = string.Empty;

            if (value != 0)
            {
                // FFmpeg Range 0 to 3
                // FFmpeg Default 1
                // Slider -100 to 100
                // Slider Default 0
                // Limit to 2 decimal places

                //saturation = "saturation=" + VM.FilterVideoView.FilterVideo_EQ_Saturation.Value.ToString();

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
        /// Video EQ - Gamma (Method)
        /// <summary>
        public static String Video_EQ_Gamma_Filter()
        {
            double value = VM.FilterVideoView.FilterVideo_EQ_Gamma_Value;

            string gamma = string.Empty;

            if (value != 0)
            {
                // FFmpeg Range 0.1 to 10
                // FFmpeg Default 1
                // Slider -100 to 100
                // Slider Default 0
                // Limit to 2 decimal places

                //gamma = "gamma=" + VM.FilterVideoView.FilterVideo_EQ_Gamma.Value.ToString();
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
        /// Video Filter Combine (Method)
        /// <summary>
        public static String VideoFilter()
        {
            // Video BitRate None Check
            // Video Codec None Check
            // Codec Copy Check
            // Media Type Check
            if (VM.VideoView.Video_Quality_SelectedItem != "None" &&
                VM.VideoView.Video_Codec_SelectedItem != "None" &&
                VM.VideoView.Video_Codec_SelectedItem != "Copy" &&
                VM.FormatView.Format_MediaType_SelectedItem != "Audio")
            {
                // --------------------------------------------------
                // Add Each Filter to Master Filters List
                // --------------------------------------------------

                // -------------------------
                //  Speed
                // -------------------------
                Video.Speed(//VM.FormatView.Format_MediaType_SelectedItem,
                            VM.VideoView.Video_Codec_SelectedItem,
                            //VM.VideoView.Video_Quality_SelectedItem,
                            VM.VideoView.Video_Speed_SelectedItem,
                            VM.VideoView.Video_Speed_Text
                            );

                // -------------------------
                // Crop/Scale Switcher
                // -------------------------
                //  Scale is Custom width & height
                // -------------------------
                if ((VM.VideoView.Video_Codec_SelectedItem == "x264" ||
                    VM.VideoView.Video_Codec_SelectedItem == "x265" ||
                    VM.VideoView.Video_Codec_SelectedItem == "MPEG-2" ||
                    VM.VideoView.Video_Codec_SelectedItem == "MPEG-4")
                    &&
                    !string.Equals(VM.VideoView.Video_Width_Text, "auto", StringComparison.CurrentCultureIgnoreCase) &&
                    !string.Equals(VM.VideoView.Video_Height_Text, "auto", StringComparison.CurrentCultureIgnoreCase)// && 
                    //VM.VideoView.Video_CropClear_Text == "Clear"
                    )
                {
                    // -------------------------
                    //  Resize - Crop and Divisible Crop are added in the Video Scale() Method
                    // -------------------------
                    Video.Scale(VM.VideoView.Video_Codec_SelectedItem,
                                VM.VideoView.Video_Scale_SelectedItem,
                                VM.VideoView.Video_Width_Text,
                                VM.VideoView.Video_Height_Text,
                                VM.VideoView.Video_ScreenFormat_SelectedItem,
                                //VM.VideoView.Video_AspectRatio_SelectedItem,
                                VM.VideoView.Video_ScalingAlgorithm_SelectedItem,
                                VM.VideoView.Video_CropClear_Text
                                );
                }

                // -------------------------
                //  Scale is a Size Preset
                // -------------------------
                else
                {
                    // -------------------------
                    //  Crop (first)
                    // -------------------------
                    Video.Crop(MainWindow.cropwindow);

                    // -------------------------
                    //  Resize (second)
                    // -------------------------
                    Video.Scale(VM.VideoView.Video_Codec_SelectedItem,
                                VM.VideoView.Video_Scale_SelectedItem,
                                VM.VideoView.Video_Width_Text,
                                VM.VideoView.Video_Height_Text,
                                VM.VideoView.Video_ScreenFormat_SelectedItem,
                                //VM.VideoView.Video_AspectRatio_SelectedItem,
                                VM.VideoView.Video_ScalingAlgorithm_SelectedItem,
                                VM.VideoView.Video_CropClear_Text
                                );
                }

                // -------------------------
                // PNG to JPEG
                // -------------------------
                VideoFilters.PNGtoJPG_Filter();

                // -------------------------
                //    Subtitles Burn
                // -------------------------
                VideoFilters.SubtitlesBurn_Filter(/*mainwindow*/ );

                // -------------------------
                //  Deband
                // -------------------------
                VideoFilters.Deband_Filter();

                // -------------------------
                //  Deshake
                // -------------------------
                VideoFilters.Deshake_Filter();

                // -------------------------
                //  Deflicker
                // -------------------------
                VideoFilters.Deflicker_Filter();

                // -------------------------
                //  Dejudder
                // -------------------------
                VideoFilters.Dejudder_Filter();

                // -------------------------
                //  Denoise
                // -------------------------
                VideoFilters.Denoise_Filter();

                // -------------------------
                //  Denoise
                // -------------------------
                VideoFilters.Deinterlace_Filter();

                // -------------------------
                //  EQ - Brightness, Contrast, Saturation, Gamma
                // -------------------------
                VideoFilters.Video_EQ_Filter();

                // -------------------------
                //  Selective SelectiveColorPreview
                // -------------------------
                VideoFilters.SelectiveColor_Filter();


                // -------------------------
                // Filter Combine
                // -------------------------
                if (VM.VideoView.Video_Codec_SelectedItem != "None") // None Check
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


}
