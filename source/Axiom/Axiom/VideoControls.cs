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

        public static List<string> OptimizeTuneItemSource;
        public static List<string> OptimizeProfileItemSource;
        public static List<string> OptimizeLevelItemSource;

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
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "AV1") { mainwindow.cboSpeed.IsEnabled = true; }
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

                // Enable FPS
                mainwindow.cboFPS.IsEnabled = true;


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
                // Size
                // --------------------------------------------------
                // Scaling
                mainwindow.cboScaling.IsEnabled = true;


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

                // Enable FPS
                mainwindow.cboFPS.IsEnabled = true;

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
                // Size
                // --------------------------------------------------
                // Scaling
                mainwindow.cboScaling.IsEnabled = true;


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

                // Enable FPS
                mainwindow.cboFPS.IsEnabled = true;


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


                // -------------------------
                // MP4 Container
                // -------------------------
                if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                {
                    // Change ItemSource
                    OptimizeItemSource = new List<string>()
                    {
                        "none",
                        "Custom",
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
                }
                // -------------------------
                // MKV Container
                // -------------------------
                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                {
                    // Change ItemSource
                    OptimizeItemSource = new List<string>()
                    {
                        "none",
                        "Custom",
                        "Windows",
                        "Apple",
                        "Android",
                        "PS3",
                        "PS4",
                        "Xbox 360",
                        "Xbox One"
                    };
                }

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
                // Size
                // --------------------------------------------------
                // Scaling
                mainwindow.cboScaling.IsEnabled = true;


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

                // Enable FPS
                mainwindow.cboFPS.IsEnabled = true;


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
                    "Custom",
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
                // Size
                // --------------------------------------------------
                // Scaling
                mainwindow.cboScaling.IsEnabled = true;


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

                // Enable FPS
                mainwindow.cboFPS.IsEnabled = true;


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
                // Size
                // --------------------------------------------------
                // Scaling
                mainwindow.cboScaling.IsEnabled = true;


                // --------------------------------------------------
                // Speed
                // --------------------------------------------------
                // Controlled through Video Quality ComboBox
            }

            // --------------------------------------------------
            // AV1
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "AV1")
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
                    //"Lossless", // disabled
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

                // Enable FPS
                mainwindow.cboFPS.IsEnabled = true;


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
                // Size
                // --------------------------------------------------
                // Scaling
                mainwindow.cboScaling.IsEnabled = true;


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

                // Enable FPS
                mainwindow.cboFPS.IsEnabled = true;


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
                // Size
                // --------------------------------------------------
                // Scaling
                mainwindow.cboScaling.IsEnabled = true;


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

                // Disable CRF
                mainwindow.crfCustom.IsEnabled = false;

                // Enable FPS
                mainwindow.cboFPS.IsEnabled = false;


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
                // Size
                // --------------------------------------------------
                // Scaling
                mainwindow.cboScaling.IsEnabled = true;


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

                // Disable CRF
                mainwindow.crfCustom.IsEnabled = false;

                // Enable FPS
                mainwindow.cboFPS.IsEnabled = false;


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
                // Size
                // --------------------------------------------------
                // Scaling
                mainwindow.cboScaling.IsEnabled = true;


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

                // Disable CRF
                mainwindow.crfCustom.IsEnabled = false;

                // Enable FPS
                mainwindow.cboFPS.IsEnabled = false;


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
                // Size
                // --------------------------------------------------
                // Scaling
                mainwindow.cboScaling.IsEnabled = false;


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

                // Enable Video Quality
                mainwindow.cboVideo.IsEnabled = false;

                // Disable CRF
                mainwindow.crfCustom.IsEnabled = false;

                // Enable FPS
                mainwindow.cboFPS.IsEnabled = false;

                //// Pixel Format
                //mainwindow.cboPixelFormat.IsEnabled = false;
                //mainwindow.cboPixelFormat.SelectedItem = "auto";


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
        ///     Video - Auto Codec Copy (Method)
        /// <summary>
        public static void AutoCopyVideoCodec(MainWindow mainwindow)
        {
            if (!string.IsNullOrEmpty(MainWindow.inputExt) || !string.IsNullOrEmpty(MainWindow.batchExt)) // Null Check
            {
                // -------------------------
                // Select Copy - Single
                // -------------------------
                // Input Extension is Same as Output Extension and Video Quality is Auto
                if ((string)mainwindow.cboVideo.SelectedItem == "Auto"
                    && (string)mainwindow.cboSize.SelectedItem == "Source"
                    && string.IsNullOrEmpty(CropWindow.crop)
                    && (string)mainwindow.cboFPS.SelectedItem == "auto"
                    && (string)mainwindow.cboOptimize.SelectedItem == "none"

                    // Extension Match
                    && string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // -------------------------
                    // Insert Copy if Does Not Contain
                    // -------------------------
                    if (!VideoCodecItemSource.Contains("Copy"))
                    {
                        VideoCodecItemSource.Insert(0, "Copy");
                    }
                    // Populate ComboBox from ItemSource
                    mainwindow.cboVideoCodec.ItemsSource = VideoCodecItemSource;

                    // -------------------------
                    // Set Video Codec Combobox Selected Item to Copy
                    // -------------------------
                    if (VideoCodecItemSource.Count > 0)
                    {
                        if (VideoCodecItemSource.Contains("Copy"))
                        {
                            mainwindow.cboVideoCodec.SelectedItem = "Copy";

                            //return;
                        }
                    }
                }

                // -------------------------
                // Select Copy - Batch
                // -------------------------
                else if ((string)mainwindow.cboVideo.SelectedItem == "Auto"
                    && (string)mainwindow.cboSize.SelectedItem == "Source"
                    && string.IsNullOrEmpty(CropWindow.crop)
                    && (string)mainwindow.cboFPS.SelectedItem == "auto"
                    && (string)mainwindow.cboOptimize.SelectedItem == "none"

                    // Batch Extension Match
                    && mainwindow.tglBatch.IsChecked == true
                    && string.Equals(MainWindow.batchExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // -------------------------
                    // Insert Copy if Does Not Contain
                    // -------------------------
                    if (!VideoCodecItemSource.Contains("Copy"))
                    {
                        VideoCodecItemSource.Insert(0, "Copy");
                    }
                    // Populate ComboBox from ItemSource
                    mainwindow.cboVideoCodec.ItemsSource = VideoCodecItemSource;

                    // -------------------------
                    // Set Video Codec Combobox Selected Item to Copy
                    // -------------------------
                    if (VideoCodecItemSource.Count > 0)
                    {
                        if (VideoCodecItemSource.Contains("Copy"))
                        {
                            mainwindow.cboVideoCodec.SelectedItem = "Copy";

                            //return;
                        }
                    }
                }

                // -------------------------
                // Reset to Default Codec
                // -------------------------
                else
                {
                    // -------------------------
                    // Disable Copy if:
                    // Input / Output Extensions don't match
                    // Batch / Output Extensions don't match
                    // Size is Not No
                    // Crop is Not Empty
                    // FPS is Not Auto
                    // Optimize is Not None
                    // -------------------------
                    // -------------------------
                    // Null Check
                    // -------------------------
                    if (!string.IsNullOrEmpty((string)mainwindow.cboVideo.SelectedItem))
                    {
                        // -------------------------
                        // Copy Selected
                        // -------------------------
                        if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy")
                        {
                            // -------------------------
                            // Switch back to format's default codec
                            // -------------------------
                            if (!string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                                || !string.Equals(MainWindow.batchExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                                )
                            {
                                // -------------------------
                                // WebM
                                // -------------------------
                                if ((string)mainwindow.cboFormat.SelectedItem == "webm")
                                {
                                    mainwindow.cboVideoCodec.SelectedItem = "VP8";
                                }
                                // -------------------------
                                // MP4
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                                {
                                    mainwindow.cboVideoCodec.SelectedItem = "x264";
                                }
                                // -------------------------
                                // MKV
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                                {
                                    mainwindow.cboVideoCodec.SelectedItem = "x264";
                                }
                                // -------------------------
                                // AVI
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "avi")
                                {
                                    mainwindow.cboVideoCodec.SelectedItem = "mpeg4";
                                }
                                // -------------------------
                                // OGV
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                                {
                                    mainwindow.cboVideoCodec.SelectedItem = "Theora";
                                }
                                // -------------------------
                                // JPG
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "jpg")
                                {
                                    mainwindow.cboVideoCodec.SelectedItem = "JPEG";
                                }
                                // -------------------------
                                // PNG
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "png")
                                {
                                    mainwindow.cboVideoCodec.SelectedItem = "PNG";
                                }
                            }
                        }
                    }

                } // End Disable Copy

            } // End Input / Batch Null Check

        } // End AutoCopyVideoCodec


        /// <summary>
        ///     Subtitle - Auto Codec Copy (Method)
        /// <summary>
        public static void AutoCopySubtitleCodec(MainWindow mainwindow)
        {
            if (!string.IsNullOrEmpty(MainWindow.inputExt) || !string.IsNullOrEmpty(MainWindow.batchExt)) // Null Check
            {
                // -------------------------
                // Select Copy - Single
                // -------------------------
                // Input Extension is Same as Output Extension and Subtitle Quality is Auto
                if ((string)mainwindow.cboVideo.SelectedItem == "Auto"
                    && (string)mainwindow.cboSize.SelectedItem == "Source"
                    && string.IsNullOrEmpty(CropWindow.crop)
                    && (string)mainwindow.cboFPS.SelectedItem == "auto"
                    && (string)mainwindow.cboOptimize.SelectedItem == "none"

                    // Extension Match
                    && string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // -------------------------
                    // Insert Copy if Does Not Contain
                    // -------------------------
                    if (!SubtitleCodecItemSource.Contains("Copy"))
                    {
                        SubtitleCodecItemSource.Insert(0, "Copy");
                    }
                    // Populate ComboBox from ItemSource
                    mainwindow.cboSubtitleCodec.ItemsSource = SubtitleCodecItemSource;

                    // -------------------------
                    // Set Video Codec Combobox Selected Item to Copy
                    // -------------------------
                    if (SubtitleCodecItemSource.Count > 0)
                    {
                        if (SubtitleCodecItemSource.Contains("Copy"))
                        {
                            mainwindow.cboSubtitleCodec.SelectedItem = "Copy";

                            //return;
                        }
                    }
                }

                // -------------------------
                // Select Copy - Batch
                // -------------------------
                else if ((string)mainwindow.cboVideo.SelectedItem == "Auto"
                    && (string)mainwindow.cboSize.SelectedItem == "Source"
                    && string.IsNullOrEmpty(CropWindow.crop)
                    && (string)mainwindow.cboFPS.SelectedItem == "auto"
                    && (string)mainwindow.cboOptimize.SelectedItem == "none"

                    // Batch Extension Match
                    && mainwindow.tglBatch.IsChecked == true
                    && string.Equals(MainWindow.batchExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    // -------------------------
                    // Insert Copy if Does Not Contain
                    // -------------------------
                    if (!SubtitleCodecItemSource.Contains("Copy"))
                    {
                        SubtitleCodecItemSource.Insert(0, "Copy");
                    }
                    // Populate ComboBox from ItemSource
                    mainwindow.cboSubtitleCodec.ItemsSource = SubtitleCodecItemSource;

                    // -------------------------
                    // Set Video Codec Combobox Selected Item to Copy
                    // -------------------------
                    if (SubtitleCodecItemSource.Count > 0)
                    {
                        if (SubtitleCodecItemSource.Contains("Copy"))
                        {
                            mainwindow.cboSubtitleCodec.SelectedItem = "Copy";

                            //return;
                        }
                    }
                }

                // -------------------------
                // Reset to Default Codec
                // -------------------------
                else
                {
                    // -------------------------
                    // Disable Copy if:
                    // Input / Output Extensions don't match
                    // Batch / Output Extensions don't match
                    // Size is Not No
                    // Crop is Not Empty
                    // FPS is Not Auto
                    // Optimize is Not None
                    // -------------------------
                    // -------------------------
                    // Null Check
                    // -------------------------
                    if (!string.IsNullOrEmpty((string)mainwindow.cboSubtitle.SelectedItem))
                    {
                        // -------------------------
                        // Copy Selected 
                        // -------------------------
                        if ((string)mainwindow.cboSubtitleCodec.SelectedItem == "Copy")
                        {
                            // -------------------------
                            // Switch back to format's default codec
                            // -------------------------
                            if (!string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                                || !string.Equals(MainWindow.batchExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase))
                            {
                                // -------------------------
                                // WebM
                                // -------------------------
                                if ((string)mainwindow.cboFormat.SelectedItem == "webm")
                                {
                                    mainwindow.cboSubtitleCodec.SelectedItem = "None";
                                }
                                // -------------------------
                                // MP4
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                                {
                                    mainwindow.cboSubtitleCodec.SelectedItem = "mov_text";
                                }
                                // -------------------------
                                // MKV
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                                {
                                    mainwindow.cboSubtitleCodec.SelectedItem = "Copy";
                                }
                                // -------------------------
                                // AVI
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "avi")
                                {
                                    mainwindow.cboSubtitleCodec.SelectedItem = "SRT";
                                }
                                // -------------------------
                                // OGV
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                                {
                                    mainwindow.cboSubtitleCodec.SelectedItem = "None";
                                }
                                // -------------------------
                                // JPG
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "jpg")
                                {
                                    mainwindow.cboSubtitleCodec.SelectedItem = "None";
                                }
                                // -------------------------
                                // PNG
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "png")
                                {
                                    mainwindow.cboSubtitleCodec.SelectedItem = "None";
                                }
                            }
                        }
                    }
                }

            } // End Input / Batch Null Check

        } // End AutoCopySubtitleCodec


        /// <summary>
        ///     Video Quality Controls (Method)
        /// </summary>
        public static void VideoQualityControls(MainWindow mainwindow)
        {
            // -------------------------
            // MPEG-4 Lossless (Special Rules)
            // -------------------------
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "mpeg4")
            {
                // -------------------------
                // Lossless is VBR -q:v 2
                // VBR can only be 1 Pass
                // -------------------------
                if ((string)mainwindow.cboVideo.SelectedItem == "Lossless")
                {
                    // Change ItemSource
                    PassItemSource = new List<string>()
                    {
                        "1 Pass",
                    };

                    //Change Item Source
                    if (PassItemSource.Count > 0)
                    {
                        PassItemSource.Clear();
                    }

                    //Pass_ItemSource.ForEach(ViewModel._cboVideoPass_Items.Add);

                    for (int i = 0; i < PassItemSource.Count; i++)
                    {
                        PassItemSource.Add(PassItemSource[i]);
                    }

                    // Select Item
                    mainwindow.cboPass.SelectedItem = "1 Pass";
                }

                // -------------------------
                // High, Medium, Low, Sub is CBR/VBR
                // CBR can be 1 Pass or 2 Pass
                // -------------------------
                else
                {
                    // Change ItemSource
                    PassItemSource = new List<string>()
                    {
                        "1 Pass",
                        "2 Pass",
                    };

                    // Populate ComboBox from ItemSource
                    mainwindow.cboPass.ItemsSource = PassItemSource;
                }
            }
        }


        /// <summary>
        /// Pass Controls (Method)
        /// <summary>
        public static void EncodingPass(MainWindow mainwindow)
        {
            // -------------------------
            // Encoding Pass ComboBox Custom
            // -------------------------
            if ((string)mainwindow.cboVideo.SelectedItem == "Custom")
            {
                // -------------------------
                // Disable CRF TextBox if 1 Pass or 2-Pass
                // -------------------------
                if ((string)mainwindow.cboPass.SelectedItem == "1 Pass"
                    || (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                {
                    mainwindow.crfCustom.IsEnabled = false;
                    mainwindow.cboPass.IsEnabled = true;

                    // Set CRF back to Default value
                    mainwindow.crfCustom.Text = "CRF";
                }

                // -------------------------
                // Enable CRF TextBox if CRF
                // -------------------------
                if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                {
                    mainwindow.crfCustom.IsEnabled = true;
                    mainwindow.cboPass.IsEnabled = true;

                    // Theora - Special Rule
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
                    {
                        mainwindow.crfCustom.IsEnabled = false;
                        mainwindow.cboPass.IsEnabled = true;
                    }
                }
            }


            // -------------------------
            // Encoding Pass ComboBox - Auto
            // -------------------------
            if ((string)mainwindow.cboVideo.SelectedItem == "Auto")
            {
                // -------------------------
                // // If Auto Disable 2-Pass Always
                // -------------------------
                if ((string)mainwindow.cboPass.SelectedItem == "2 Pass")
                {
                    mainwindow.cboPass.IsEnabled = false;

                    // Reset the User willing selected bool
                    passUserSelected = false;
                }

                // -------------------------
                // Set Pass ComboBox to 2-Pass and Disable 
                // -------------------------
                else if ((string)mainwindow.cboPass.SelectedItem != "2 Pass")
                {
                    mainwindow.cboPass.SelectedItem = "2 Pass";
                    // Disable Pass ComboBox if 2-Pass
                    mainwindow.cboPass.IsEnabled = false;
                }
            }

            // -------------------------
            // Encoding Pass ComboBox - Not Auto
            // -------------------------
            else if ((string)mainwindow.cboVideo.SelectedItem != "Auto")
            {
                mainwindow.cboPass.IsEnabled = true;
            }
        }


        /// <summary>
        ///     Pixel Format
        /// </summary>
        public static void PixelFormat(MainWindow mainwindow)
        {
            // -------------------------
            // MediaType Enable
            // -------------------------
            if ((string)mainwindow.cboMediaType.SelectedItem == "Video"
                || (string)mainwindow.cboMediaType.SelectedItem == "Image"
                || (string)mainwindow.cboMediaType.SelectedItem == "Sequence")
            {
                // -------------------------
                // Codec Enable
                // -------------------------
                if ((string)mainwindow.cboVideoCodec.SelectedItem != "Copy")
                {
                    mainwindow.cboPixelFormat.IsEnabled = true;

                    // Lossless
                    if ((string)mainwindow.cboVideo.SelectedItem == "Lossless")
                    {
                        mainwindow.cboPixelFormat.SelectedItem = "yuv444p";
                    }
                    // All Other Quality
                    else
                    {
                        mainwindow.cboPixelFormat.SelectedItem = "yuv420p";
                    }
                }

                // -------------------------
                // Codec Disable
                // -------------------------
                else
                {
                    mainwindow.cboPixelFormat.IsEnabled = false;
                    mainwindow.cboPixelFormat.SelectedItem = "auto";
                }
            }

            // MediaType Disable
            else if ((string)mainwindow.cboMediaType.SelectedItem == "Audio")
            {
                mainwindow.cboPixelFormat.IsEnabled = false;
            }
        }


        /// <summary>
        ///    Optimize Controls
        /// </summary>
        public static void OptimizeControls(MainWindow mainwindow)
        {
            // --------------------------------------------------
            // Optimize ComboBox Items
            // --------------------------------------------------

            // -------------------------
            // x264
            // -------------------------
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
            {
                // Change ItemSource

                // -------------------------
                // Tune
                // -------------------------

                OptimizeTuneItemSource = new List<string>()
                {
                    "none",
                    "film",
                    "animation",
                    "grain",
                    "stillimage",
                    "fastdecode",
                    "zerolatency"
                };

                // -------------------------
                // Profile
                // -------------------------
                OptimizeProfileItemSource = new List<string>()
                {
                    "none",
                    "baseline",
                    "main",
                    "high"
                };

                // -------------------------
                // Level
                // -------------------------
                OptimizeLevelItemSource = new List<string>()
                {
                    "none",
                    "1.0",
                    "1.1",
                    "1.2",
                    "1.3",
                    "2.0",
                    "2.2",
                    "2.2",
                    "3.0",
                    "3.1",
                    "3.2",
                    "4.0",
                    "4.1",
                    "4.2",
                    "5.0",
                    "5.1",
                    "5.2"
                };
            }

            // -------------------------
            // x265
            // -------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
            {
                // Change ItemSource

                // -------------------------
                // Tune
                // -------------------------
                OptimizeTuneItemSource = new List<string>()
                {
                    "none",
                    "psnr",
                    "ssim",
                    "grain",
                    "fastdecode",
                    "zerolatency"
                };

                // -------------------------
                // Profile
                // -------------------------
                OptimizeProfileItemSource = new List<string>()
                {
                    "none",
                    "main",
                    "main-intra",
                    "mainstillpicture",
                    "main444-8",
                    "main444-intra",
                    "main444-stillpicture",
                    "main10",
                    "main10-intra",
                    "main422-10",
                    "main422-10-intra",
                    "main444-10",
                    "main444-10-intra",
                    "main12",
                    "main422-12",
                    "main422-12-intra",
                    "main444-12",
                    "main444-12-intra",
                };

                // -------------------------
                // Level
                // -------------------------
                OptimizeLevelItemSource = new List<string>()
                {
                    "none",
                    "1",
                    "2",
                    "2.1",
                    "3",
                    "3.1",
                    "4",
                    "4.1",
                    "5",
                    "5.1",
                    "5.2",
                    "6",
                    "6.1",
                    "6.2",
                    "8.5",
                };
            }

            // -------------------------
            // All Other Codecs
            // -------------------------
            else
            {
                // Change ItemSource

                // -------------------------
                // Tune
                // -------------------------
                OptimizeTuneItemSource = new List<string>()
                {
                    "none"
                };

                // -------------------------
                // Profile
                // -------------------------
                OptimizeProfileItemSource = new List<string>()
                {
                    "none"
                };

                // -------------------------
                // Level
                // -------------------------
                OptimizeLevelItemSource = new List<string>()
                {
                    "none"
                };
            }

            // -------------------------
            // Populate Optimize Tune, Profile, Level ComboBox from ItemSource
            // -------------------------
            mainwindow.cboOptTune.ItemsSource = OptimizeTuneItemSource;
            mainwindow.cboOptProfile.ItemsSource = OptimizeProfileItemSource;
            mainwindow.cboOptLevel.ItemsSource = OptimizeLevelItemSource;

            mainwindow.cboOptTune.SelectedIndex = 0;
            mainwindow.cboOptProfile.SelectedIndex = 0;
            mainwindow.cboOptLevel.SelectedIndex = 0;


            // --------------------------------------------------
            // Enable / Disable - Optimize Tune, Profile, Level
            // --------------------------------------------------
            // -------------------------
            // x264 / x265
            // -------------------------
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
            {
                if ((string)mainwindow.cboOptimize.SelectedItem == "none")
                {
                    // Disabled
                    mainwindow.cboOptTune.IsEnabled = false;
                    mainwindow.cboOptProfile.IsEnabled = false;
                    mainwindow.cboOptLevel.IsEnabled = false;
                    Video.optFlags = string.Empty;
                }
                else
                {
                    // Enable 
                    mainwindow.cboOptTune.IsEnabled = true;
                    mainwindow.cboOptProfile.IsEnabled = true;
                    mainwindow.cboOptLevel.IsEnabled = true;
                }
            }

            // -------------------------
            // All other Codecs
            // -------------------------
            else
            {
                // Disable All
                // Tune, Profile, Level not available for other codecs
                mainwindow.cboOptTune.IsEnabled = false;
                mainwindow.cboOptProfile.IsEnabled = false;
                mainwindow.cboOptLevel.IsEnabled = false;

                mainwindow.cboOptTune.SelectedItem = "none";
                mainwindow.cboOptProfile.SelectedItem = "none";
                mainwindow.cboOptLevel.SelectedItem = "none";
                Video.optFlags = string.Empty;
            }



            // --------------------------------------------------
            // Presets
            // --------------------------------------------------
            // -------------------------
            // VP8, VP9, Theora
            // -------------------------
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora"
            )
            {
                // Web
                if ((string)mainwindow.cboOptimize.SelectedItem == "Web")
                {
                    Video.optFlags = "-movflags faststart";
                }
            }

            // -------------------------
            // x264
            // -------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
            {
                // Web
                if ((string)mainwindow.cboOptimize.SelectedItem == "Web")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "baseline";
                    mainwindow.cboOptLevel.SelectedItem = "3.0";
                    Video.optFlags = "-movflags +faststart";
                }
                // DVD
                else if ((string)mainwindow.cboOptimize.SelectedItem == "DVD")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "baseline";
                    mainwindow.cboOptLevel.SelectedItem = "3.0";
                    Video.optFlags = "-maxrate 9.6M";
                }
                // HD Video
                else if ((string)mainwindow.cboOptimize.SelectedItem == "HD Video")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "main";
                    mainwindow.cboOptLevel.SelectedItem = "4.0";
                    Video.optFlags = string.Empty;
                }
                // Animation
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Animation")
                {
                    mainwindow.cboOptTune.SelectedItem = "animation";
                    mainwindow.cboOptProfile.SelectedItem = "main";
                    mainwindow.cboOptLevel.SelectedItem = "4.0";
                    Video.optFlags = string.Empty;
                }
                // Blu-ray
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Blu-ray")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "main";
                    mainwindow.cboOptLevel.SelectedItem = "4.1";
                    Video.optFlags = "-deblock 0:0 -sar 1/1 -x264opts bluray-compat=1:level=4.1:open-gop=1:slices=4:tff=1:colorprim=bt709:colormatrix=bt709:vbv-maxrate=40000:vbv-bufsize=30000:me=umh:ref=4:nal-hrd=vbr:aud=1:b-pyramid=strict";
                }
                // Windows Device
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Windows")
                {

                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "baseline";
                    mainwindow.cboOptLevel.SelectedItem = "3.1";
                    Video.optFlags = "-movflags faststart";
                }
                // Apple Device
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Apple")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "baseline";
                    mainwindow.cboOptLevel.SelectedItem = "3.1";
                    Video.optFlags = "-x264-params ref=4";
                }
                // Android Device
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Android")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "baseline";
                    mainwindow.cboOptLevel.SelectedItem = "3.0";
                    Video.optFlags = "-movflags faststart";
                }
                // PS3
                else if ((string)mainwindow.cboOptimize.SelectedItem == "PS3")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "main";
                    mainwindow.cboOptLevel.SelectedItem = "4.0";
                    Video.optFlags = string.Empty;
                }
                // PS4
                else if ((string)mainwindow.cboOptimize.SelectedItem == "PS4")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "main";
                    mainwindow.cboOptLevel.SelectedItem = "4.1";
                    Video.optFlags = string.Empty;
                }
                // Xbox 360
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Xbox 360")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "high";
                    mainwindow.cboOptLevel.SelectedItem = "4.1";
                    Video.optFlags = "-maxrate 9.8M";
                }
                // Xbox One
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Xbox One")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "high";
                    mainwindow.cboOptLevel.SelectedItem = "4.1";
                    Video.optFlags = string.Empty;
                }
                // Custom
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Custom")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "none";
                    mainwindow.cboOptLevel.SelectedItem = "none";
                    Video.optFlags = string.Empty;
                }
            }

            // -------------------------
            // x265
            // -------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
            {
                // Web
                if ((string)mainwindow.cboOptimize.SelectedItem == "Web")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "none";
                    mainwindow.cboOptLevel.SelectedItem = "none";
                    Video.optFlags = "-movflags faststart";
                }
            }



            // -------------------------
            // None
            // -------------------------
            if ((string)mainwindow.cboOptimize.SelectedItem == "none")
            {
                mainwindow.cboOptTune.SelectedItem = "none";
                mainwindow.cboOptProfile.SelectedItem = "none";
                mainwindow.cboOptLevel.SelectedItem = "none";
                Video.optFlags = string.Empty;
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
