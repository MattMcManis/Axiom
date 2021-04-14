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

 * Speed
 * Frame Rate To Decimal
 * FPS
 * Images
---------------------------------- */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using Axiom;
using System.Globalization;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Generate.Video
{
    public class Video
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Global Variables
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        public static string fps { get; set; } // Frames Per Second
        public static string vsync { get; set; }
        public static string image { get; set; } // JPEG & PNG options


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Methods
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Frame Rate To Decimal
        /// <summary>
        /// <remarks>
        /// When using Video Frame Range instead of Time
        /// </remarks>
        public static String FramesToDecimal(string frame)
        {
            // Separate FFprobe Result (e.g. 30000/1001)
            string[] f = Analyze.FFprobe.inputFrameRate.Split('/');

            try
            {
                double detectedFramerate = Convert.ToDouble(f[0]) / Convert.ToDouble(f[1]); // divide FFprobe values
                detectedFramerate = Math.Truncate(detectedFramerate * 1000) / 1000; // limit to 3 decimal places

                // Trim Start Frame
                //
                if (!string.IsNullOrWhiteSpace(frame)) // Default/Null Check
                {
                    // Divide Frame Start Number by Video's Framerate
                    frame = Convert.ToString(Convert.ToDouble(frame) / detectedFramerate);
                }

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Frame: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(frame + " / " + detectedFramerate + " = " + Format.trimStart) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }
            catch
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Warning: No input file or Framerate not detected.")) { Foreground = Log.ConsoleWarning });
                };
                Log.LogActions.Add(Log.WriteAction);

                // Warning
                MessageBox.Show("No input file or Framerate not detected.",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }

            return frame;
        }


        /// <summary>
        /// FPS
        /// <summary>
        public static String FPS(string codec_SelectedItem,
                                 string fps_SelectedItem,
                                 string fps_Text
                                )
        {
            // Check:
            // Video Codec Not Copy
            // FPS Not Empty
            if (codec_SelectedItem != "Copy" &&
                !string.IsNullOrWhiteSpace(fps_Text)
                )
            {
                fps = "-r ";

                switch (fps_SelectedItem)
                {
                    case "auto":
                        fps = string.Empty;
                        break;
                    case "film":
                        fps += "film";
                        break;
                    case "pal":
                        fps += "pal";
                        break;
                    case "ntsc":
                        fps += "ntsc";
                        break;
                    case "23.976":
                        fps += "24000/1001";
                        break;
                    case "24":
                        fps += "24";
                        break;
                    case "25":
                        fps += "25";
                        break;
                    case "29.97":
                        fps += "30000/1001";
                        break;
                    case "30":
                        fps += "30";
                        break;
                    case "48":
                        fps += "48";
                        break;
                    case "50":
                        fps += "50";
                        break;
                    case "59.94":
                        fps += "60000/1001";
                        break;
                    case "60":
                        fps += "60";
                        break;

                    default:
                        try
                        {
                            fps += fps_Text;
                            break;
                        }
                        catch
                        {
                            // Warning
                            MessageBox.Show("Invalid Custom FPS.",
                                            "Notice",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Warning);
                        }
                        break;
                }

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("FPS: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(fps_Text) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            return fps;
        }


        /// <summary>
        /// Video Speed (Method)
        /// <summary>
        public static void Speed(string codec_SelectedItem,
                                    string speed_SelectedItem,
                                    string speed_Text
                                    )
        {
            // Media Type Audio
            // Video Codec None
            // Video Codec Copy
            // Video BitRate None
            // Speed Auto/Null
            if (codec_SelectedItem == "Copy" ||
                speed_SelectedItem == "auto" ||
                string.IsNullOrWhiteSpace(speed_Text))
            {
                // Halt
                return;
            }

            // Slow Down 50% -vf "setpts=2.0*PTS"
            // Speed Up 200% -vf "setpts=0.5*PTS"

            // Convert to setpts:
            // 50%: (100 / (50 * 0.01)) * 0.01) = 200
            // 2.0: 200 * 0.01 = 2.0
            double val = 1; // Fallback
            double.TryParse(speed_Text.Replace("%", "").Trim(), out val);

            val = (100 / (val * 0.01)) * 0.01;

            string speed = "setpts=" + val.ToString("0.#####", CultureInfo.GetCultureInfo("en-US")) + "*PTS";

            Filters.Video.vFiltersList.Add(speed);
        }


        /// <summary>
        /// Vsync (Method)
        /// </summary>
        public static String Vsync(string codec_SelectedItem,
                                    string vsync_SelectedItem
            )
        {
            // Check:
            // Video Codec Not Copy
            // FPS Not Empty
            if (codec_SelectedItem != "Copy" &&
                vsync_SelectedItem != "off")
            {
                vsync = "-vsync ";

                switch (vsync_SelectedItem)
                {
                    //case "off":
                    //    vsync = string.Empty;
                    //    break;

                    case "auto":
                        vsync += "auto";
                        break;

                    case "passthrough":
                        vsync += "passthrough";
                        break;

                    case "cfr":
                        vsync += "cfr";
                        break;

                    case "vfr":
                        vsync += "vfr";
                        break;

                    case "drop":
                        vsync += "drop";
                        break;

                    default:
                        vsync = string.Empty;
                        break;
                }
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Vsync: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(vsync) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            return vsync;
        }


        /// <summary>
        /// Images
        /// <summary>
        public static String Images(string mediaType_SelectedItem,
                                    string codec_SelectedItem
                                    )
        {
            // Copy
            if (codec_SelectedItem == "Copy")
            {
                return string.Empty;
            }

            // Option
            switch (mediaType_SelectedItem)
            {
                // -------------------------
                // Image
                // -------------------------
                case "Image":
                    image = "-vframes 1"; //important
                    break;

                // -------------------------
                // Sequence
                // -------------------------
                case "Sequence":
                    image = string.Empty; //disable -vframes
                    break;

                // -------------------------
                // All Other Media Types
                // -------------------------
                default:
                    image = string.Empty;
                    break;
            }

            return image;
        }


    }
}
