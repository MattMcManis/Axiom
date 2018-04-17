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
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class VideoControls
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ComboBoxes Item Sources
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        private static string previousItem; // Previous ComboBox Item

        // -------------------------
        // Video
        // -------------------------
        public static List<string> VideoCodecItemSource;
        public static List<string> SubtitleCodecItemSource;
        public static List<string> VideoItemSource;
        public static List<string> PassItemSource;
        public static List<string> ResizeItemSource;
        public static List<string> CutItemSource;
        public static List<string> TuneItemSource;
        public static List<string> OptimizeItemSource;
        public static List<string> SpeedItemSource;

        public static bool passUserSelected = false; // Used to determine if User willingly selected CRF, 1 Pass or 2 Pass


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Control Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Video Codec Controls (Method)
        /// 
        /// Changes Other ComboBox Items and Selections based on Video Codec
        /// </summary>
        public static void VideoCodecControls(MainWindow mainwindow)
        {
            // On ComboBox Selection Change, Through Method

            // -------------------------
            // Encoding Speed
            // -------------------------
            // Codec
            //
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy") { mainwindow.cboSpeed.IsEnabled = true; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8") { mainwindow.cboSpeed.IsEnabled = true; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9") { mainwindow.cboSpeed.IsEnabled = true; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264") { mainwindow.cboSpeed.IsEnabled = true; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265") { mainwindow.cboSpeed.IsEnabled = true; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4") { mainwindow.cboSpeed.IsEnabled = true; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "PNG") { mainwindow.cboSpeed.IsEnabled = false; }
            else if (string.IsNullOrEmpty((string)mainwindow.cboVideoCodec.SelectedItem)) { mainwindow.cboSpeed.IsEnabled = false; }

            // Container
            // Audio
            //
            if ((string)mainwindow.cboFormat.SelectedItem == "ogv") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp3") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboFormat.SelectedItem == "m4a") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogg") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboFormat.SelectedItem == "flac") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboFormat.SelectedItem == "wav") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboFormat.SelectedItem == "jpg") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboFormat.SelectedItem == "png") { mainwindow.cboSpeed.IsEnabled = false; }


            // -------------------------
            // MKV Special Inustrctions - If Video Codec = Copy, select Video Dropdown to Auto
            // -------------------------
            if ((string)mainwindow.cboFormat.SelectedItem == "mkv" 
                && (string)mainwindow.cboVideoCodec.SelectedItem == "Copy")
            {
                mainwindow.cboVideo.SelectedItem = "Auto";
            }


            // --------------------------------------------------
            // VP8
            // --------------------------------------------------
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8")
            {
                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>()
                {
                    "Auto",
                    "Ultra",
                    "High",
                    "Medium",
                    "Low",
                    "Sub",
                    "Custom"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideo.ItemsSource = VideoItemSource;

                // Select Item
                if (VideoItemSource.Contains(previousItem))
                {
                    //System.Windows.MessageBox.Show("Previous Item");
                    mainwindow.cboVideo.SelectedItem = previousItem;
                }
                else
                {
                    //System.Windows.MessageBox.Show("0");
                    mainwindow.cboVideo.SelectedIndex = 0; // Auto
                }

                // Enable Control
                mainwindow.cboVideo.IsEnabled = true;

                // Enable CRF
                if ((string)mainwindow.cboVideo.SelectedItem == "Custom")
                {
                    mainwindow.crfCustom.IsEnabled = true;
                }


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboPass.SelectedItem;

                // Change ItemSource
                PassItemSource = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboPass.ItemsSource = PassItemSource;

                // Select Item
                if (PassItemSource.Contains(previousItem))
                {
                    mainwindow.cboPass.SelectedItem = previousItem;
                }
                else
                {
                    //MessageBox.Show("not available"); //debug
                    mainwindow.cboPass.SelectedIndex = 0; // Auto
                }


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboOptimize.SelectedItem;

                // Change ItemSource
                OptimizeItemSource = new List<string>()
                {
                    "none",
                    "Web"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboOptimize.ItemsSource = OptimizeItemSource;

                // Select Item
                mainwindow.cboOptimize.SelectedItem = "none";

                // Enable Control
                mainwindow.cboOptimize.IsEnabled = true;


                // --------------------------------------------------
                // Speed
                // --------------------------------------------------
                // Controlled through Video Quality ComboBox
            }

            // --------------------------------------------------
            // VP9
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
            {
                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>()
                {
                    "Auto",
                    "Lossless",
                    "Ultra",
                    "High",
                    "Medium",
                    "Low",
                    "Sub",
                    "Custom"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideo.ItemsSource = VideoItemSource;

                // Select Item
                if (VideoItemSource.Contains(previousItem))
                {
                    mainwindow.cboVideo.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboVideo.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboVideo.IsEnabled = true;

                // Enable CRF
                if ((string)mainwindow.cboVideo.SelectedItem == "Custom")
                {
                    mainwindow.crfCustom.IsEnabled = true;
                }

                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboPass.SelectedItem;

                // Change ItemSource
                PassItemSource = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboPass.ItemsSource = PassItemSource;

                // Select Item
                if (PassItemSource.Contains(previousItem))
                {
                    mainwindow.cboPass.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboPass.SelectedIndex = 0; // Auto
                }


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboOptimize.SelectedItem;

                // Change ItemSource
                OptimizeItemSource = new List<string>()
                {
                    "none",
                    "Web"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboOptimize.ItemsSource = OptimizeItemSource;

                // Select Item
                mainwindow.cboOptimize.SelectedItem = "none";

                // Enable Control
                mainwindow.cboOptimize.IsEnabled = true;


                // --------------------------------------------------
                // Speed
                // --------------------------------------------------
                // Controlled through Video Quality ComboBox
            }


            // --------------------------------------------------
            // x264
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
            {
                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>()
                {
                    "Auto",
                    "Lossless",
                    "Ultra",
                    "High",
                    "Medium",
                    "Low",
                    "Sub",
                    "Custom"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideo.ItemsSource = VideoItemSource;

                // Select Item
                if (VideoItemSource.Contains(previousItem))
                {
                    mainwindow.cboVideo.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboVideo.SelectedIndex = 0; // Auto
                }

                // Enable Control
                mainwindow.cboVideo.IsEnabled = true;

                // Enable CRF
                if ((string)mainwindow.cboVideo.SelectedItem == "Custom")
                {
                    mainwindow.crfCustom.IsEnabled = true;
                }


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboPass.SelectedItem;
                //MessageBox.Show(previousItem); //debug
                //var message = string.Join(Environment.NewLine, VideoCodecItemSource); //debug
                //MessageBox.Show(message); //debug

                // Change ItemSource
                PassItemSource = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboPass.ItemsSource = PassItemSource;

                // Select Item
                if (PassItemSource.Contains(previousItem))
                {
                    mainwindow.cboPass.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboPass.SelectedIndex = 0; // Auto
                }


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboOptimize.SelectedItem;

                // Change ItemSource
                OptimizeItemSource = new List<string>()
                {
                    "none",
                    "Advanced",
                    "Web",
                    "DVD",
                    "Blu-ray",
                    "Windows",
                    "Apple",
                    "Android",
                    "PS3",
                    "PS4",
                    "Xbox 360",
                    "Xbox One"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboOptimize.ItemsSource = OptimizeItemSource;

                // Select Item
                if (OptimizeItemSource.Contains(previousItem))
                {
                    mainwindow.cboOptimize.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboOptimize.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboOptimize.IsEnabled = true;


                //MKV NEEDS ITS OWN RULES ////////////////////////////
                if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                {
                    // Get Previous Item
                    previousItem = (string)mainwindow.cboOptimize.SelectedItem;

                    // Change ItemSource
                    OptimizeItemSource = new List<string>()
                    {
                        "none",
                        "Advanced",
                        "Windows",
                        "Apple",
                        "Android",
                        "PS3",
                        "PS4",
                        "Xbox 360",
                        "Xbox One"
                    };

                    // Populate ComboBox from ItemSource
                    mainwindow.cboOptimize.ItemsSource = OptimizeItemSource;

                    // Select Item
                    if (OptimizeItemSource.Contains(previousItem))
                    {
                        mainwindow.cboOptimize.SelectedItem = previousItem;
                    }
                    else
                    {
                        mainwindow.cboOptimize.SelectedIndex = 0; // auto
                    }

                    // Enable Control
                    mainwindow.cboOptimize.IsEnabled = true;
                }

                // --------------------------------------------------
                // Speed
                // --------------------------------------------------
                // Controlled through Video Quality ComboBox
            }

            // --------------------------------------------------
            // x265
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
            {
                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>()
                {
                    "Auto",
                    "Lossless",
                    "Ultra",
                    "High",
                    "Medium",
                    "Low",
                    "Sub",
                    "Custom"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideo.ItemsSource = VideoItemSource;

                // Select Item
                if (VideoItemSource.Contains(previousItem))
                {
                    mainwindow.cboVideo.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboVideo.SelectedIndex = 0; // Auto
                }

                // Enable Control
                mainwindow.cboVideo.IsEnabled = true;

                // Enable CRF
                if ((string)mainwindow.cboVideo.SelectedItem == "Custom")
                {
                    mainwindow.crfCustom.IsEnabled = true;
                }


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboPass.SelectedItem;

                // Change ItemSource
                PassItemSource = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboPass.ItemsSource = PassItemSource;

                // Select Item
                if (PassItemSource.Contains(previousItem))
                {
                    mainwindow.cboPass.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboPass.SelectedIndex = 0; // Auto
                }


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboOptimize.SelectedItem;

                // Change ItemSource
                OptimizeItemSource = new List<string>()
                {
                    "none",
                    "Web"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboOptimize.ItemsSource = OptimizeItemSource;

                // Select Item
                if (OptimizeItemSource.Contains(previousItem))
                {
                    mainwindow.cboOptimize.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboOptimize.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboOptimize.IsEnabled = true;


                // MKV NEEDS ITS OWN RULES ////////////////////////////
                if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                {
                    // Get Previous Item
                    previousItem = (string)mainwindow.cboOptimize.SelectedItem;

                    // Change ItemSource
                    OptimizeItemSource = new List<string>() { "none" };

                    // Populate ComboBox from ItemSource
                    mainwindow.cboOptimize.ItemsSource = OptimizeItemSource;

                    // Select Item
                    if (OptimizeItemSource.Contains(previousItem))
                    {
                        mainwindow.cboOptimize.SelectedItem = previousItem;
                    }
                    else
                    {
                        mainwindow.cboOptimize.SelectedIndex = 0; // auto
                    }

                    // Enable Control
                    mainwindow.cboOptimize.IsEnabled = true;
                }

                // --------------------------------------------------
                // Speed
                // --------------------------------------------------
                // Controlled through Video Quality ComboBox
            }

            // --------------------------------------------------
            // MPEG-4
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
            {
                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>()
                {
                    "Auto",
                    "Lossless",
                    "Ultra",
                    "High",
                    "Medium",
                    "Low",
                    "Sub",
                    "Custom"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideo.ItemsSource = VideoItemSource;

                // Select Item
                if (VideoItemSource.Contains(previousItem))
                {
                    mainwindow.cboVideo.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboVideo.SelectedIndex = 0; // Auto
                }

                // Enable Control
                mainwindow.cboVideo.IsEnabled = true;

                // Enable CRF
                if ((string)mainwindow.cboVideo.SelectedItem == "Custom")
                {
                    mainwindow.crfCustom.IsEnabled = true;
                }


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboPass.SelectedItem;

                // Change ItemSource
                PassItemSource = new List<string>()
                {
                    "1 Pass",
                    "2 Pass"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboPass.ItemsSource = PassItemSource;

                // Select Item
                if (PassItemSource.Contains(previousItem))
                {
                    mainwindow.cboPass.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboPass.SelectedIndex = 0; // Auto
                }


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboOptimize.SelectedItem;

                // Change ItemSource
                OptimizeItemSource = new List<string>()
                {
                    "none"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboOptimize.ItemsSource = OptimizeItemSource;

                // Select Item
                if (OptimizeItemSource.Contains(previousItem))
                {
                    mainwindow.cboOptimize.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboOptimize.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboOptimize.IsEnabled = true;


                // --------------------------------------------------
                // Speed
                // --------------------------------------------------
                // Controlled through Video Quality ComboBox
            }

            // --------------------------------------------------
            // Theora
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
            {
                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>()
                {
                    "Auto",
                    "Lossless",
                    "Ultra",
                    "High",
                    "Medium",
                    "Low",
                    "Sub",
                    "Custom"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideo.ItemsSource = VideoItemSource;

                // Select Item
                if (VideoItemSource.Contains(previousItem))
                {
                    mainwindow.cboVideo.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboVideo.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboVideo.IsEnabled = true;

                // Disable CRF (Only for Theora)
                if ((string)mainwindow.cboVideo.SelectedItem == "Custom")
                {
                    mainwindow.crfCustom.IsEnabled = false;
                }


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboPass.SelectedItem;

                // Change ItemSource
                PassItemSource = new List<string>()
                {
                    "1 Pass",
                    "2 Pass"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboPass.ItemsSource = PassItemSource;

                // Select Item
                if (PassItemSource.Contains(previousItem))
                {
                    mainwindow.cboPass.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboPass.SelectedIndex = 0; // Auto
                }

                // Disable Control
                //mainwindow.cboPass.IsEnabled = true;


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboOptimize.SelectedItem;

                // Change ItemSource
                OptimizeItemSource = new List<string>()
                {
                    "none",
                    "Web"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboOptimize.ItemsSource = OptimizeItemSource;

                // Select Item
                if (OptimizeItemSource.Contains(previousItem))
                {
                    mainwindow.cboOptimize.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboOptimize.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboOptimize.IsEnabled = true;


                // --------------------------------------------------
                // Speed (Only for Theora)
                // --------------------------------------------------
                // Controlled through Video Quality ComboBox
                mainwindow.cboSpeed.IsEnabled = false;
            }

            // --------------------------------------------------
            // JPEG
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
            {
                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>()
                {
                    "Auto",
                    "Ultra",
                    "High",
                    "Medium",
                    "Low",
                    "Sub"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideo.ItemsSource = VideoItemSource;

                // Select Item
                if (VideoItemSource.Contains(previousItem))
                {
                    mainwindow.cboVideo.SelectedItem = previousItem;
                }
                else
                {
                    mainwindow.cboVideo.SelectedIndex = 0; // auto
                }

                // Enable Control
                mainwindow.cboVideo.IsEnabled = true;


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboPass.SelectedItem;

                // Change ItemSource
                PassItemSource = new List<string>() { "auto" };

                // Populate ComboBox from ItemSource
                mainwindow.cboPass.ItemsSource = PassItemSource;

                // Select Item
                mainwindow.cboPass.SelectedItem = "auto";

                // Disable Control
                mainwindow.cboPass.IsEnabled = false;


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboOptimize.SelectedItem;

                // Change ItemSource
                OptimizeItemSource = new List<string>() { "none" };

                // Populate ComboBox from ItemSource
                mainwindow.cboOptimize.ItemsSource = OptimizeItemSource;

                // Select Item
                mainwindow.cboOptimize.SelectedItem = "none";

                // Enable Control
                mainwindow.cboOptimize.IsEnabled = false;


                // --------------------------------------------------
                // Speed
                // --------------------------------------------------
                // Controlled through Video Quality ComboBox
            }

            // --------------------------------------------------
            // PNG
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
            {
                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>()
                {
                    "Auto",
                    "Lossless",
                    "Ultra",
                    "High",
                    "Medium",
                    "Low",
                    "Sub"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideo.ItemsSource = VideoItemSource;

                // Select Item
                mainwindow.cboVideo.SelectedItem = "Lossless";

                // Enable Control
                mainwindow.cboVideo.IsEnabled = false;


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboPass.SelectedItem;

                // Change ItemSource
                PassItemSource = new List<string>() { "auto" };

                // Populate ComboBox from ItemSource
                mainwindow.cboPass.ItemsSource = PassItemSource;

                // Select Item
                mainwindow.cboPass.SelectedItem = "auto";

                // Disable Control
                mainwindow.cboPass.IsEnabled = false;


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboOptimize.SelectedItem;

                // Change ItemSource
                OptimizeItemSource = new List<string>() { "none" };

                // Populate ComboBox from ItemSource
                mainwindow.cboOptimize.ItemsSource = OptimizeItemSource;

                // Select Item
                mainwindow.cboOptimize.SelectedItem = "none";

                // Enable Control
                mainwindow.cboOptimize.IsEnabled = false;


                // --------------------------------------------------
                // Speed
                // --------------------------------------------------
                // Controlled through Video Quality ComboBox
            }


            // --------------------------------------------------
            // Copy
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy")
            {
                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>()
                {
                    "Auto",
                    "Lossless",
                    "Ultra",
                    "High",
                    "Medium",
                    "Low",
                    "Sub",
                    "Custom"
                };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideo.ItemsSource = VideoItemSource;

                // Select Item
                mainwindow.cboVideo.SelectedItem = "Auto";

                // Enable Control
                mainwindow.cboVideo.IsEnabled = true;


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboPass.SelectedItem;

                // Change ItemSource
                PassItemSource = new List<string>() { "auto" };

                // Populate ComboBox from ItemSource
                mainwindow.cboPass.ItemsSource = PassItemSource;

                // Select Item
                mainwindow.cboPass.SelectedItem = "auto";

                // Disable Control
                mainwindow.cboPass.IsEnabled = false;


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboOptimize.SelectedItem;

                // Change ItemSource
                OptimizeItemSource = new List<string>() { "none" };

                // Populate ComboBox from ItemSource
                mainwindow.cboOptimize.ItemsSource = OptimizeItemSource;

                // Select Item
                mainwindow.cboOptimize.SelectedItem = "none";

                // Enable Control
                mainwindow.cboOptimize.IsEnabled = false;


                // --------------------------------------------------
                // Speed
                // --------------------------------------------------
                // Controlled through Video Quality ComboBox
            }

            // --------------------------------------------------
            // None
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "None")
            {
                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideo.ItemsSource = VideoItemSource;

                // Select Item
                mainwindow.cboVideo.SelectedItem = "None";

                // Enable Control
                mainwindow.cboVideo.IsEnabled = false;


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboPass.SelectedItem;

                // Change ItemSource
                PassItemSource = new List<string>() { "auto" };

                // Populate ComboBox from ItemSource
                mainwindow.cboPass.ItemsSource = PassItemSource;

                // Select Item
                mainwindow.cboPass.SelectedItem = "auto";

                // Disable Control
                mainwindow.cboPass.IsEnabled = false;


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboOptimize.SelectedItem;

                // Change ItemSource
                OptimizeItemSource = new List<string>() { "none" };

                // Populate ComboBox from ItemSource
                mainwindow.cboOptimize.ItemsSource = OptimizeItemSource;

                // Select Item
                mainwindow.cboOptimize.SelectedItem = "none";

                // Enable Control
                mainwindow.cboOptimize.IsEnabled = false;


                // --------------------------------------------------
                // Speed
                // --------------------------------------------------
                // Controlled through Video Quality ComboBox
                mainwindow.cboSpeed.IsEnabled = false;
            }


            // If Video ComboBox Null, Select Default Auto
            else if (string.IsNullOrEmpty((string)mainwindow.cboVideo.SelectedItem))
            {
                mainwindow.cboVideo.SelectedIndex = 0;
            }

        } // End Video Codec Controls 



        /// <summary>
        /// Video - Auto Codec Copy (Method)
        /// <summary>
        public static void AutoCopyVideoCodec(MainWindow mainwindow) // Method
        {
            if (!string.IsNullOrEmpty(MainWindow.inputExt) 
                || !string.IsNullOrEmpty(MainWindow.batchExt)) // Null Check
            {
                // -------------------------
                // Add Copy to Video Codec ComboBox
                // -------------------------
                // Input Extension is Same as Output Extension and Video Quality is Auto
                if ((string)mainwindow.cboVideo.SelectedItem == "Auto"
                    && string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                    || mainwindow.tglBatch.IsChecked == true
                    && (string)mainwindow.cboVideo.SelectedItem == "Auto"
                    && string.Equals(MainWindow.batchExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase))
                {
                    // Insert Copy if Does Not Contain
                    if (!VideoControls.VideoCodecItemSource.Contains("Copy"))
                    {
                        VideoControls.VideoCodecItemSource.Insert(0, "Copy");
                    }
                    // Populate ComboBox from ItemSource
                    mainwindow.cboVideoCodec.ItemsSource = VideoControls.VideoCodecItemSource;


                    // -------------------------
                    // Set Video Codec Combobox Selected Item to Copy
                    // -------------------------
                    if ((string)mainwindow.cboVideo.SelectedItem == "Auto"
                        && (string)mainwindow.cboSize.SelectedItem == "Source"
                        && string.IsNullOrEmpty(CropWindow.crop)
                        && (string)mainwindow.cboFPS.SelectedItem == "auto"
                        && (string)mainwindow.cboOptimize.SelectedItem == "none")
                    {
                        // Video
                        mainwindow.cboVideoCodec.SelectedItem = "Copy";

                        // Subtitle
                        mainwindow.cboSubtitleCodec.SelectedItem = "Copy";
                    }
                }


                // -------------------------
                // Disable Copy if:
                // Input / Output Extensions don't match
                // Batch / Output Extensions don't match
                // Size is Not No
                // Crop is Not Empty
                // FPS is Not Auto
                // Optimize is Not None
                // -------------------------
                if (VideoControls.VideoCodecItemSource.Contains("Copy"))
                {
                    // Copy Selected
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy")
                    {
                        // Null Check
                        //
                        if (!string.IsNullOrEmpty((string)mainwindow.cboVideo.SelectedItem))
                        {
                            // Extension Match
                            //
                            if (!string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                                || !string.Equals(MainWindow.batchExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase))
                            {
                                // -------------------------
                                // WebM
                                // -------------------------
                                if ((string)mainwindow.cboFormat.SelectedItem == "webm")
                                {
                                    // Not Auto
                                    if ((string)mainwindow.cboVideo.SelectedItem != "Auto")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "VP8";
                                    }

                                    // Size
                                    if ((string)mainwindow.cboVideo.SelectedItem == "Auto"
                                        && (string)mainwindow.cboSize.SelectedItem != "No")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "VP8";
                                    }

                                    // Crop, FPS, Optimize
                                    if (!string.IsNullOrEmpty(CropWindow.crop)
                                        || (string)mainwindow.cboFPS.SelectedItem != "auto"
                                        || (string)mainwindow.cboOptimize.SelectedItem != "none")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "VP8";
                                    }
                                }
                                // -------------------------
                                // MP4
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                                {
                                    // Not Auto
                                    if ((string)mainwindow.cboVideo.SelectedItem != "Auto")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "x264";
                                    }

                                    // Size
                                    if ((string)mainwindow.cboVideo.SelectedItem == "Auto"
                                        && (string)mainwindow.cboSize.SelectedItem != "No")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "x264";
                                    }

                                    // Crop, FPS, Optimize
                                    if (!string.IsNullOrEmpty(CropWindow.crop)
                                        || (string)mainwindow.cboFPS.SelectedItem != "auto"
                                        || (string)mainwindow.cboOptimize.SelectedItem != "none")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "x264";
                                    }
                                }
                                // -------------------------
                                // MKV
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                                {
                                    // Not Auto
                                    if ((string)mainwindow.cboVideo.SelectedItem != "Auto")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "x264";
                                    }

                                    // Size
                                    if ((string)mainwindow.cboVideo.SelectedItem == "Auto"
                                        && (string)mainwindow.cboSize.SelectedItem != "No")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "x264";
                                    }

                                    // Crop, FPS, Optimize
                                    if (!string.IsNullOrEmpty(CropWindow.crop)
                                        || (string)mainwindow.cboFPS.SelectedItem != "auto"
                                        || (string)mainwindow.cboOptimize.SelectedItem != "none")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "x264";
                                    }
                                }
                                // -------------------------
                                // AVI
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "avi")
                                {
                                    // Not Auto
                                    if ((string)mainwindow.cboVideo.SelectedItem != "Auto")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "mpeg4";
                                    }

                                    // Size
                                    if ((string)mainwindow.cboVideo.SelectedItem == "Auto"
                                        && (string)mainwindow.cboSize.SelectedItem != "No")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "mpeg4";
                                    }

                                    // Crop, FPS, Optimize
                                    if (!string.IsNullOrEmpty(CropWindow.crop)
                                        || (string)mainwindow.cboFPS.SelectedItem != "auto"
                                        || (string)mainwindow.cboOptimize.SelectedItem != "none")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "mpeg4";
                                    }
                                }
                                // -------------------------
                                // OGV
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                                {
                                    // Not Auto
                                    if ((string)mainwindow.cboVideo.SelectedItem != "Auto")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "Theora";
                                    }

                                    // Size
                                    if ((string)mainwindow.cboVideo.SelectedItem == "Auto"
                                        && (string)mainwindow.cboSize.SelectedItem != "No")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "Theora";
                                    }

                                    // Crop, FPS, Optimize
                                    if (!string.IsNullOrEmpty(CropWindow.crop)
                                        || (string)mainwindow.cboFPS.SelectedItem != "auto"
                                        || (string)mainwindow.cboOptimize.SelectedItem != "none")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "Theora";
                                    }
                                }
                                // -------------------------
                                // JPG
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "jpg")
                                {
                                    // Not Auto
                                    if ((string)mainwindow.cboVideo.SelectedItem != "Auto")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "JPEG";
                                    }

                                    // Size
                                    if ((string)mainwindow.cboVideo.SelectedItem == "Auto"
                                        && (string)mainwindow.cboSize.SelectedItem != "No")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "JPEG";
                                    }

                                    // Crop, FPS, Optimize
                                    if (!string.IsNullOrEmpty(CropWindow.crop)
                                        || (string)mainwindow.cboFPS.SelectedItem != "auto"
                                        || (string)mainwindow.cboOptimize.SelectedItem != "none")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "JPEG";
                                    }
                                }
                                // -------------------------
                                // PNG
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "png")
                                {
                                    // Not Auto
                                    if ((string)mainwindow.cboVideo.SelectedItem != "Auto")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "PNG";
                                    }

                                    // Size
                                    if ((string)mainwindow.cboVideo.SelectedItem == "Auto"
                                        && (string)mainwindow.cboSize.SelectedItem != "No")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "PNG";
                                    }

                                    // Crop, FPS, Optimize
                                    if (!string.IsNullOrEmpty(CropWindow.crop)
                                        || (string)mainwindow.cboFPS.SelectedItem != "auto"
                                        || (string)mainwindow.cboOptimize.SelectedItem != "none")
                                    {
                                        mainwindow.cboVideoCodec.SelectedItem = "PNG";
                                    }
                                }
                            }
                        }
                    }
                }
            } // End Disable Copy
        } // End AutoCopyVideoCodec


        /// <summary>
        /// Pass Controls (Method)
        /// <summary>
        public static void EncodingPass(MainWindow mainwindow)
        {
            // -------------------------
            // Encoding Pass ComboBox Custom
            // -------------------------
            // Disable CRF TextBox if 1 Pass or 2-Pass
            if ((string)mainwindow.cboVideo.SelectedItem == "Custom"
                && (string)mainwindow.cboPass.SelectedItem == "1 Pass"
                | (string)mainwindow.cboPass.SelectedItem == "2 Pass")
            {
                mainwindow.crfCustom.IsEnabled = false;
                mainwindow.cboPass.IsEnabled = true;

                // Set CRF back to Default value
                mainwindow.crfCustom.Text = "CRF";
            }

            // Enable CRF TextBox if CRF
            if ((string)mainwindow.cboVideo.SelectedItem == "Custom" 
                && (string)mainwindow.cboPass.SelectedItem == "CRF")
            {
                mainwindow.crfCustom.IsEnabled = true;
                mainwindow.cboPass.IsEnabled = true;

                // Disable CRF for Theora
                if ((string)mainwindow.cboVideo.SelectedItem == "Custom" 
                    && (string)mainwindow.cboVideoCodec.SelectedItem == "Theora") // Special Rule
                {
                    mainwindow.crfCustom.IsEnabled = false;
                    mainwindow.cboPass.IsEnabled = true;
                }
            }


            // -------------------------
            // Encoding Pass ComboBox Auto
            // -------------------------
            // Set Pass ComboBox to 2-Pass and Disable 
            if ((string)mainwindow.cboVideo.SelectedItem == "Auto" 
                && (string)mainwindow.cboPass.SelectedItem != "2 Pass")
            {
                mainwindow.cboPass.SelectedItem = "2 Pass";
                // Disable Pass ComboBox if 2-Pass
                mainwindow.cboPass.IsEnabled = false;
            }
            // If Auto Disable 2-Pass Always
            if ((string)mainwindow.cboVideo.SelectedItem == "Auto" 
                && (string)mainwindow.cboPass.SelectedItem == "2 Pass")
            {
                mainwindow.cboPass.IsEnabled = false;

                // Reset the User willing selected bool
                passUserSelected = false;
            }

            // Enable if Not Auto
            if ((string)mainwindow.cboVideo.SelectedItem != "Auto")
            {
                mainwindow.cboPass.IsEnabled = true;
            }
        }


        /// <summary>
        ///     Subtitle Codec Controls
        /// </summary>
        public static void SubtitleCodecControls(MainWindow mainwindow)
        {
            // -------------------------
            // Video
            // -------------------------
            if ((string)mainwindow.cboMediaType.SelectedItem == "Video")
            {
                // None
                //
                if ((string)mainwindow.cboSubtitle.SelectedItem == "none")
                {
                    mainwindow.cboSubtitleCodec.SelectedItem = "None";
                    mainwindow.cboSubtitleCodec.IsEnabled = false;
                }

                // All, External, & Stream Number
                //
                else
                {
                    // Formats

                    // WebM
                    if ((string)mainwindow.cboFormat.SelectedItem == "webm")
                    {
                        mainwindow.cboSubtitleCodec.SelectedItem = "None";
                        mainwindow.cboSubtitleCodec.IsEnabled = false;
                    }

                    // MP4
                    else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                    {
                        mainwindow.cboSubtitleCodec.SelectedItem = "mov_text";
                        mainwindow.cboSubtitleCodec.IsEnabled = true;
                    }

                    // MKV
                    else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                    {
                        mainwindow.cboSubtitleCodec.SelectedItem = "Copy";
                        mainwindow.cboSubtitleCodec.IsEnabled = true;
                    }

                    // AVI
                    else if ((string)mainwindow.cboFormat.SelectedItem == "avi")
                    {
                        mainwindow.cboSubtitleCodec.SelectedItem = "SRT";
                        mainwindow.cboSubtitleCodec.IsEnabled = true;
                    }

                    // OGV
                    else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                    {
                        mainwindow.cboSubtitleCodec.SelectedItem = "None";
                        mainwindow.cboSubtitleCodec.IsEnabled = false;
                    }
                }
            }
            // -------------------------
            // Image
            // -------------------------
            else if ((string)mainwindow.cboMediaType.SelectedItem == "Image"
                || (string)mainwindow.cboMediaType.SelectedItem == "Sequence")
            {
                mainwindow.cboSubtitleCodec.SelectedItem = "None";
                mainwindow.cboSubtitleCodec.IsEnabled = false;
            }
            // -------------------------
            // Audio
            // -------------------------
            else if ((string)mainwindow.cboMediaType.SelectedItem == "Audio")
            {
                mainwindow.cboSubtitleCodec.SelectedItem = "None";
                mainwindow.cboSubtitleCodec.IsEnabled = false;
            }
        }
    }
}
