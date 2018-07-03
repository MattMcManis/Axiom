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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public partial class VideoFilters
    {
        // Filter
        public static List<string> vFiltersList = new List<string>(); // Filters to String Join
        public static string geq; // png transparent to jpg whtie background filter
        public static string vFilter;


        /// <summary>
        ///     PNG to JPG (Method)
        /// <summary>
        public static void PNGtoJPG_Filter(MainWindow mainwindow)
        {
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
            {
                // Turn on PNG to JPG Filter
                if (string.Equals(MainWindow.inputExt, ".png", StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(MainWindow.batchExt, "png", StringComparison.CurrentCultureIgnoreCase))
                {
                    //png transparent to white background
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
        public static void SubtitlesBurn_Filter(MainWindow mainwindow)
        {
            string burn = string.Empty;

            if ((string)mainwindow.cboSubtitleCodec.SelectedItem == "Burn"
                && Video.subtitleFileNamesList.Count > 0)
            {
                // Join File Names List
                //string files = string.Join(",", subtitleFileNamesList.Where(s => !string.IsNullOrEmpty(s)));

                //// Create Subtitles Filter
                //string subtitles = "subtitles=" + files + ":force_style='FontName=Arial,FontSize=22'" + style;

                // -------------------------
                // Get First Subtitle File
                // -------------------------
                string file = Video.subtitleFilePathsList.First().Replace("\"", "'");

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
        public static void Deband_Filter(MainWindow mainwindow)
        {
            if ((string)mainwindow.cboFilterVideo_Deband.SelectedItem == "enabled")
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
        public static void Deshake_Filter(MainWindow mainwindow)
        {
            if ((string)mainwindow.cboFilterVideo_Deshake.SelectedItem == "enabled")
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
        public static void Deflicker_Filter(MainWindow mainwindow)
        {
            if ((string)mainwindow.cboFilterVideo_Deflicker.SelectedItem == "enabled")
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
        public static void Dejudder_Filter(MainWindow mainwindow)
        {
            if ((string)mainwindow.cboFilterVideo_Dejudder.SelectedItem == "enabled")
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
        public static void Denoise_Filter(MainWindow mainwindow)
        {
            if ((string)mainwindow.cboFilterVideo_Denoise.SelectedItem != "disabled")
            {
                string denoise = string.Empty;

                // -------------------------
                // Default
                // -------------------------
                if ((string)mainwindow.cboFilterVideo_Denoise.SelectedItem == "default")
                {
                    denoise = "removegrain=0";
                }
                // -------------------------
                // Light
                // -------------------------
                else if ((string)mainwindow.cboFilterVideo_Denoise.SelectedItem == "light")
                {
                    denoise = "removegrain=22";
                }
                // -------------------------
                // Medium
                // -------------------------
                else if ((string)mainwindow.cboFilterVideo_Denoise.SelectedItem == "medium")
                {
                    denoise = "vaguedenoiser=threshold=3:method=soft:nsteps=5";
                }
                // -------------------------
                // Heavy
                // -------------------------
                else if ((string)mainwindow.cboFilterVideo_Denoise.SelectedItem == "heavy")
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
        ///     Selective SelectiveColorPreview Calculator (Method)
        /// <summary>
        public static String SelectiveColor_Calculator(double sliderValue)
        {
            // Convert Slider Value -100 to 100 integer to -1 to 1 decimal
            string decimalValue = Convert.ToString(Math.Round(sliderValue * 0.01, 2));

            return decimalValue;
        }


        /// <summary>
        ///     Selective SelectiveColorPreview (Method)
        /// <summary>
        public static void SelectiveColor_Filter(MainWindow mainwindow)
        {
            string selectiveColor = string.Empty;

            List<double> selectiveColorSliders = new List<double>()
            {
                // Reds
                mainwindow.slFiltersVideo_SelectiveColor_Reds_Cyan.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Reds_Magenta.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Reds_Yellow.Value,
                // Yellows
                mainwindow.slFiltersVideo_SelectiveColor_Yellows_Cyan.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Yellows_Magenta.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Yellows_Yellow.Value,
                // Greens
                mainwindow.slFiltersVideo_SelectiveColor_Greens_Cyan.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Greens_Magenta.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Greens_Yellow.Value,
                // Cyans
                mainwindow.slFiltersVideo_SelectiveColor_Cyans_Cyan.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Cyans_Magenta.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Cyans_Yellow.Value,
                // Blues
                mainwindow.slFiltersVideo_SelectiveColor_Blues_Cyan.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Blues_Magenta.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Blues_Yellow.Value,
                // Magentas
                mainwindow.slFiltersVideo_SelectiveColor_Magentas_Cyan.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Magentas_Magenta.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Magentas_Yellow.Value,
                // Whites
                mainwindow.slFiltersVideo_SelectiveColor_Whites_Cyan.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Whites_Magenta.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Whites_Yellow.Value,
                // Neutrals
                mainwindow.slFiltersVideo_SelectiveColor_Neutrals_Cyan.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Neutrals_Magenta.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Neutrals_Yellow.Value,
                // Blacks
                mainwindow.slFiltersVideo_SelectiveColor_Blacks_Cyan.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Blacks_Magenta.Value,
                mainwindow.slFiltersVideo_SelectiveColor_Blacks_Yellow.Value,
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
                string reds_cyan = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Reds_Cyan.Value);
                //if (string.IsNullOrEmpty(reds_cyan)) { reds_cyan = "0"; };
                // Magenta
                string reds_magenta = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Reds_Magenta.Value);
                // Yellow
                string reds_yellow = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Reds_Yellow.Value);

                // -------------------------
                // Yellows
                // -------------------------
                // Cyan
                string yellows_cyan = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Yellows_Cyan.Value);
                // Magenta
                string yellows_magenta = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Yellows_Magenta.Value);
                // Yellow
                string yellows_yellow = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Yellows_Yellow.Value);

                // -------------------------
                // Greens
                // -------------------------
                // Cyan
                string greens_cyan = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Greens_Cyan.Value);
                // Magenta
                string greens_magenta = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Greens_Magenta.Value);
                // Yellow
                string greens_yellow = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Greens_Yellow.Value);

                // -------------------------
                // Cyans
                // -------------------------
                // Cyan
                string cyans_cyan = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Cyans_Cyan.Value);
                // Magenta
                string cyans_magenta = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Cyans_Magenta.Value);
                // Yellow
                string cyans_yellow = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Cyans_Yellow.Value);

                // -------------------------
                // Blues
                // -------------------------
                // Cyan
                string blues_cyan = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Blues_Cyan.Value);
                // Magenta
                string blues_magenta = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Blues_Magenta.Value);
                // Yellow
                string blues_yellow = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Blues_Yellow.Value);

                // -------------------------
                // Magentas
                // -------------------------
                // Cyan
                string magentas_cyan = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Magentas_Cyan.Value);
                // Magenta
                string magentas_magenta = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Magentas_Magenta.Value);
                // Yellow
                string magentas_yellow = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Magentas_Yellow.Value);

                // -------------------------
                // Whites
                // -------------------------
                // Cyan
                string whites_cyan = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Whites_Cyan.Value);
                // Magenta
                string whites_magenta = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Whites_Magenta.Value);
                // Yellow
                string whites_yellow = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Whites_Yellow.Value);

                // -------------------------
                // Nuetrals
                // -------------------------
                // Cyan
                string neutrals_cyan = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Neutrals_Cyan.Value);
                // Magenta
                string neutrals_magenta = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Neutrals_Magenta.Value);
                // Yellow
                string neutrals_yellow = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Neutrals_Yellow.Value);

                // -------------------------
                // Blacks
                // -------------------------
                // Cyan
                string blacks_cyan = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Blacks_Cyan.Value);
                // Magenta
                string blacks_magenta = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Blacks_Magenta.Value);
                // Yellow
                string blacks_yellow = SelectiveColor_Calculator(mainwindow.slFiltersVideo_SelectiveColor_Blacks_Yellow.Value);

                // -------------------------
                // Combine
                // -------------------------
                List<string> selectiveColorList = new List<string>()
                {
                    "selectivecolor=" + "\r\n"
                    + "correction_method=" + mainwindow.cboFilterVideo_SelectiveColor_Correction_Method.SelectedItem.ToString().ToLower() + "\r\n",

                    "reds=" + reds_cyan + " " + reds_magenta + " " + reds_yellow + "\r\n",
                    "yellows=" + yellows_cyan + " " + yellows_magenta + " " + yellows_yellow + "\r\n",
                    "greens=" + greens_cyan + " " + greens_magenta + " " + greens_yellow + "\r\n",
                    "cyans=" + cyans_cyan + " " + cyans_magenta + " " + cyans_yellow + "\r\n",
                    "blues=" + blues_cyan + " " + blues_magenta + " " + blues_yellow + "\r\n",
                    "magentas=" + magentas_cyan + " " + magentas_magenta + " " + magentas_yellow + "\r\n",
                    "whites=" + whites_cyan + " " + whites_magenta + " " + whites_yellow + "\r\n",
                    "neutrals=" + neutrals_cyan + " " + neutrals_magenta + " " + neutrals_yellow + "\r\n",
                    "blacks=" + blacks_cyan + " " + blacks_magenta + " " + blacks_yellow + "\r\n",
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
        ///     Video Filter Combine (Method)
        /// <summary>
        public static String VideoFilter(MainWindow mainwindow)
        {
            // Video Bitrate None Check
            // Video Codec None Check
            // Codec Copy Check
            // Media Type Check
            if ((string)mainwindow.cboVideoQuality.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "None"
                && (string)mainwindow.cboVideoCodec.SelectedItem != "Copy"
                && (string)mainwindow.cboMediaType.SelectedItem != "Audio")
            {
                // --------------------------------------------------
                // Filters
                // --------------------------------------------------
                // -------------------------
                //  Resize
                // -------------------------
                Video.Size(mainwindow);

                // -------------------------
                //  Crop
                // -------------------------
                Video.Crop(mainwindow, Video.cropwindow);

                // -------------------------
                //    Subtitles Burn
                // -------------------------
                VideoFilters.SubtitlesBurn_Filter(mainwindow);

                // -------------------------
                //  Deband
                // -------------------------
                VideoFilters.Deband_Filter(mainwindow);

                // -------------------------
                //  Deshake
                // -------------------------
                VideoFilters.Deshake_Filter(mainwindow);

                // -------------------------
                //  Deflicker
                // -------------------------
                VideoFilters.Deflicker_Filter(mainwindow);

                // -------------------------
                //  Dejudder
                // -------------------------
                VideoFilters.Dejudder_Filter(mainwindow);

                // -------------------------
                //  Denoise
                // -------------------------
                VideoFilters.Denoise_Filter(mainwindow);

                // -------------------------
                //  Selective SelectiveColorPreview
                // -------------------------
                VideoFilters.SelectiveColor_Filter(mainwindow);

                // -------------------------
                // PNG to JPEG
                // -------------------------
                VideoFilters.PNGtoJPG_Filter(mainwindow);


                // -------------------------
                // Filter Combine
                // -------------------------
                if ((string)mainwindow.cboVideoCodec.SelectedItem != "None") // None Check
                {
                    //System.Windows.MessageBox.Show(string.Join(",\r\n\r\n", vFiltersList.Where(s => !string.IsNullOrEmpty(s)))); //debug
                    //System.Windows.MessageBox.Show(Convert.ToString(vFiltersList.Count())); //debug

                    // 1 Filter
                    //
                    if (vFiltersList.Count() == 1)
                    {
                        vFilter = "-vf " + string.Join(",\r\n\r\n", vFiltersList.Where(s => !string.IsNullOrEmpty(s)));
                    }

                    // Multiple Filters
                    // Wrap in Quotes
                    //
                    else if (vFiltersList.Count() > 1)
                    {
                        vFilter = "-vf \"" + string.Join(",\r\n\r\n", vFiltersList.Where(s => !string.IsNullOrEmpty(s))) + "\"";
                    }

                    // Empty
                    //
                    else
                    {
                        vFilter = string.Empty;
                    }
                }
                // Video Codec None
                else
                {
                    vFilter = string.Empty;

                }
            }

            // Return Value
            return vFilter;
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


    }
}
