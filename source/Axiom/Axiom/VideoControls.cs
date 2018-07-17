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
using System.Collections.ObjectModel;
using System.Windows.Controls;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class VideoControls
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     ComboBoxes Item Sources
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // -------------------------
        // Video
        // -------------------------
        public static List<string> VideoCodec_ItemSource;
        public static List<string> SubtitleCodec_ItemSource;
        public static List<string> VideoQuality_ItemSource;
        public static List<string> Pass_ItemSource;
        public static List<string> ResizeItemSource;
        public static List<string> CutItemSource;
        public static List<string> Optimize_ItemSource;
        public static List<string> SpeedItemSource;

        public static List<string> Optimize_Tune_ItemSource;
        public static List<string> Optimize_Profile_ItemSource;
        public static List<string> Optimize_Level_ItemSource;

        public static bool passUserSelected = false; // Used to determine if User willingly selected CRF, 1 Pass or 2 Pass


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Control Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        ///     Change Item Source (Method)
        /// </summary>
        private static void ChangeItemSource(
            MainWindow mainwindow,
            ComboBox cbo,           // ComboBox
            List<string> items,     // New Items List
            string selectedItem)    // Selected Item
        {
            // -------------------------
            // Change Item Source
            // -------------------------
            cbo.ItemsSource = items;

            // -------------------------
            // Select Item
            // -------------------------
            // Get Previous Item
            string previousItem = selectedItem;

            // Select
            if (!string.IsNullOrEmpty(previousItem))
            {
                if (items.Contains(previousItem))
                {
                    cbo.SelectedItem = previousItem;
                }
                else
                {
                    cbo.SelectedIndex = 0; // Auto
                }
            }

            return;
        }



        /// <summary>
        ///     Video Codec Controls (Method)
        /// </summary>
        /// <remarks>
        ///     Changes Other ComboBox Items and Selections based on Video Codec
        /// </remarks>
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
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "MPEG-2") { mainwindow.cboSpeed.IsEnabled = true; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "MPEG-4") { mainwindow.cboSpeed.IsEnabled = true; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "PNG") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "WebP") { mainwindow.cboSpeed.IsEnabled = false; }
            else if (string.IsNullOrEmpty((string)mainwindow.cboVideoCodec.SelectedItem)) { mainwindow.cboSpeed.IsEnabled = false; }

            // Container
            //
            // Video
            if ((string)mainwindow.cboFormat.SelectedItem == "ogv") { mainwindow.cboSpeed.IsEnabled = false; }
            // Image
            else if ((string)mainwindow.cboFormat.SelectedItem == "jpg") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboFormat.SelectedItem == "png") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboFormat.SelectedItem == "webp") { mainwindow.cboSpeed.IsEnabled = false; }
            // Audio
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp3") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboFormat.SelectedItem == "m4a") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogg") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboFormat.SelectedItem == "flac") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboFormat.SelectedItem == "wav") { mainwindow.cboSpeed.IsEnabled = false; }


            // -------------------------
            // MKV Special Inustrctions - If Video Codec = Copy, select Video Quality Dropdown to Auto
            // -------------------------
            if ((string)mainwindow.cboFormat.SelectedItem == "mkv" 
                && (string)mainwindow.cboVideoCodec.SelectedItem == "Copy")
            {
                mainwindow.cboVideoQuality.SelectedItem = "Auto";
            }


            // --------------------------------------------------
            // VP8
            // --------------------------------------------------
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8")
            {
                // --------------------------------------------------
                // Video
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                VideoQuality_ItemSource = new List<string>()
                {
                    "Auto",
                    "Ultra",
                    "High",
                    "Medium",
                    "Low",
                    "Sub",
                    "Custom"
                };

                ChangeItemSource(
                    mainwindow, 
                    mainwindow.cboVideoQuality, // ComboBox
                    VideoQuality_ItemSource, // New Items List
                    (string)mainwindow.cboVideoQuality.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Pass_ItemSource = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboPass, // ComboBox
                    Pass_ItemSource, // New Items List
                    (string)mainwindow.cboPass.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Optimize_ItemSource = new List<string>()
                {
                    "None",
                    "Web"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboOptimize, // ComboBox
                    Optimize_ItemSource, // New Items List
                    "None"); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                List<Control> Controls_Enabled = new List<Control>()
                {
                    // Video Quality ComboBox
                    mainwindow.cboVideoQuality,
                    // FPS ComboBox
                    mainwindow.cboFPS,
                    // Optimize ComboBox
                    mainwindow.cboOptimize,
                    // Scaling ComboBox
                    mainwindow.cboScaling
                };

                // Add CRF Custom Textbox
                if ((string)mainwindow.cboVideoQuality.SelectedItem == "Custom")
                {
                    Controls_Enabled.Add(mainwindow.crfCustom);
                }

                // Enable
                for (int i = 0; i < Controls_Enabled.Count; i++)
                {
                    Controls_Enabled[i].IsEnabled = true;
                }

                // -------------------------
                // Disabled
                // -------------------------
                // None
            }

            // --------------------------------------------------
            // VP9
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
            {
                // --------------------------------------------------
                // Video
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                List<string> VideoQuality_ItemSource = new List<string>()
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

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboVideoQuality, // ComboBox
                    VideoQuality_ItemSource, // New Items List
                    (string)mainwindow.cboVideoQuality.SelectedItem); // Selected Item

                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Pass_ItemSource = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboPass, // ComboBox
                    Pass_ItemSource, // New Items List
                    (string)mainwindow.cboPass.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Optimize_ItemSource = new List<string>()
                {
                    "None",
                    "Web"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboOptimize, // ComboBox
                    Optimize_ItemSource, // New Items List
                    "None"); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                List<Control> Controls_Enabled = new List<Control>()
                {
                    // Video Quality ComboBox
                    mainwindow.cboVideoQuality,
                    // FPS ComboBox
                    mainwindow.cboFPS,
                    // Optimize ComboBox
                    mainwindow.cboOptimize,
                    // Scaling ComboBox
                    mainwindow.cboScaling
                };

                // Add CRF Custom Textbox
                if ((string)mainwindow.cboVideoQuality.SelectedItem == "Custom")
                {
                    Controls_Enabled.Add(mainwindow.crfCustom);
                }

                // Enable
                for (int i = 0; i < Controls_Enabled.Count; i++)
                {
                    Controls_Enabled[i].IsEnabled = true;
                }

                // -------------------------
                // Disabled
                // -------------------------
                // None
            }


            // --------------------------------------------------
            // x264
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
            {
                // --------------------------------------------------
                // Video
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                List<string> VideoQuality_ItemSource = new List<string>()
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

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboVideoQuality, // ComboBox
                    VideoQuality_ItemSource, // New Items List
                    (string)mainwindow.cboVideoQuality.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Pass_ItemSource = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboPass, // ComboBox
                    Pass_ItemSource, // New Items List
                    (string)mainwindow.cboPass.SelectedItem); // Selected Item


                // -------------------------
                // MP4 Container
                // -------------------------
                if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                {
                    // -------------------------
                    // Change ItemSource
                    // -------------------------
                    Optimize_ItemSource = new List<string>()
                    {
                        "None",
                        "Custom",
                        "Web",
                        "PC HD",
                        "PC SD",
                        //"DVD",
                        "Blu-ray",
                        "Windows",
                        "Animation",
                        "Apple",
                        "Android",
                        "PS3",
                        "PS4",
                        "Xbox 360",
                        "Xbox One"
                    };

                    ChangeItemSource(
                        mainwindow,
                        mainwindow.cboOptimize, // ComboBox
                        Optimize_ItemSource, // New Items List
                        (string)mainwindow.cboOptimize.SelectedItem); // Selected Item
                }
                // -------------------------
                // MKV Container
                // -------------------------
                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                {
                    // -------------------------
                    // Change ItemSource
                    // -------------------------
                    Optimize_ItemSource = new List<string>()
                    {
                        "None",
                        "Custom",
                        "PC HD",
                        "PC SD",
                        "Windows",
                        "Animation",
                        "Apple",
                        "Android",
                        "PS3",
                        "PS4",
                        "Xbox 360",
                        "Xbox One"
                    };

                    ChangeItemSource(
                        mainwindow,
                        mainwindow.cboOptimize, // ComboBox
                        Optimize_ItemSource, // New Items List
                        (string)mainwindow.cboOptimize.SelectedItem); // Selected Item
                }


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                List<Control> Controls_Enabled = new List<Control>()
                {
                    // Video Quality ComboBox
                    mainwindow.cboVideoQuality,
                    // FPS ComboBox
                    mainwindow.cboFPS,
                    // Optimize ComboBox
                    mainwindow.cboOptimize,
                    // Scaling ComboBox
                    mainwindow.cboScaling
                };

                // Add CRF Custom Textbox
                if ((string)mainwindow.cboVideoQuality.SelectedItem == "Custom")
                {
                    Controls_Enabled.Add(mainwindow.crfCustom);
                }

                // Enable
                for (int i = 0; i < Controls_Enabled.Count; i++)
                {
                    Controls_Enabled[i].IsEnabled = true;
                }

                // -------------------------
                // Disabled
                // -------------------------
                // None
            }

            // --------------------------------------------------
            // x265
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
            {
                // --------------------------------------------------
                // Video
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                List<string> VideoQuality_ItemSource = new List<string>()
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

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboVideoQuality, // ComboBox
                    VideoQuality_ItemSource, // New Items List
                    (string)mainwindow.cboVideoQuality.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Pass_ItemSource = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboPass, // ComboBox
                    Pass_ItemSource, // New Items List
                    (string)mainwindow.cboPass.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Optimize_ItemSource = new List<string>()
                {
                    "None",
                    "Custom",
                    "Web"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboOptimize, // ComboBox
                    Optimize_ItemSource, // New Items List
                    (string)mainwindow.cboOptimize.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                List<Control> Controls_Enabled = new List<Control>()
                {
                    // Video Quality ComboBox
                    mainwindow.cboVideoQuality,
                    // FPS ComboBox
                    mainwindow.cboFPS,
                    // Optimize ComboBox
                    mainwindow.cboOptimize,
                    // Scaling ComboBox
                    mainwindow.cboScaling
                };

                // Add CRF Custom Textbox
                if ((string)mainwindow.cboVideoQuality.SelectedItem == "Custom")
                {
                    Controls_Enabled.Add(mainwindow.crfCustom);
                }

                // Enable
                for (int i = 0; i < Controls_Enabled.Count; i++)
                {
                    Controls_Enabled[i].IsEnabled = true;
                }

                // -------------------------
                // Disabled
                // -------------------------
                // None
            }

            // --------------------------------------------------
            // MPEG-2
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "MPEG-2")
            {
                // -------------------------
                // Video
                // -------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                List<string> VideoQuality_ItemSource = new List<string>()
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

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboVideoQuality, // ComboBox
                    VideoQuality_ItemSource, // New Items List
                    (string)mainwindow.cboVideoQuality.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Pass_ItemSource = new List<string>()
                {
                    "1 Pass",
                    "2 Pass"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboPass, // ComboBox
                    Pass_ItemSource, // New Items List
                    (string)mainwindow.cboPass.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Optimize_ItemSource = new List<string>()
                {
                    "None"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboOptimize, // ComboBox
                    Optimize_ItemSource, // New Items List
                    (string)mainwindow.cboOptimize.SelectedItem); // Selected Item

                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                List<Control> Controls_Enabled = new List<Control>()
                {
                    // Video Quality ComboBox
                    mainwindow.cboVideoQuality,
                    // FPS ComboBox
                    mainwindow.cboFPS,
                    // Optimize ComboBox
                    mainwindow.cboOptimize,
                    // Scaling ComboBox
                    mainwindow.cboScaling
                };

                // Add CRF Custom Textbox
                if ((string)mainwindow.cboVideoQuality.SelectedItem == "Custom")
                {
                    Controls_Enabled.Add(mainwindow.crfCustom);
                }

                // Enable
                for (int i = 0; i < Controls_Enabled.Count; i++)
                {
                    Controls_Enabled[i].IsEnabled = true;
                }

                // -------------------------
                // Disabled
                // -------------------------
                // None
            }

            // --------------------------------------------------
            // MPEG-4
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "MPEG-4")
            {
                // -------------------------
                // Video
                // -------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                List<string> VideoQuality_ItemSource = new List<string>()
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

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboVideoQuality, // ComboBox
                    VideoQuality_ItemSource, // New Items List
                    (string)mainwindow.cboVideoQuality.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Pass_ItemSource = new List<string>()
                {
                    "1 Pass",
                    "2 Pass"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboPass, // ComboBox
                    Pass_ItemSource, // New Items List
                    (string)mainwindow.cboPass.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Optimize_ItemSource = new List<string>()
                {
                    "None"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboOptimize, // ComboBox
                    Optimize_ItemSource, // New Items List
                    (string)mainwindow.cboOptimize.SelectedItem); // Selected Item

                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                List<Control> Controls_Enabled = new List<Control>()
                {
                    // Video Quality ComboBox
                    mainwindow.cboVideoQuality,
                    // FPS ComboBox
                    mainwindow.cboFPS,
                    // Optimize ComboBox
                    mainwindow.cboOptimize,
                    // Scaling ComboBox
                    mainwindow.cboScaling
                };

                // Add CRF Custom Textbox
                if ((string)mainwindow.cboVideoQuality.SelectedItem == "Custom")
                {
                    Controls_Enabled.Add(mainwindow.crfCustom);
                }

                // Enable
                for (int i = 0; i < Controls_Enabled.Count; i++)
                {
                    Controls_Enabled[i].IsEnabled = true;
                }

                // -------------------------
                // Disabled
                // -------------------------
                // None
            }

            // --------------------------------------------------
            // AV1
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "AV1")
            {
                // -------------------------
                // Video
                // -------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                List<string> VideoQuality_ItemSource = new List<string>()
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

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboVideoQuality, // ComboBox
                    VideoQuality_ItemSource, // New Items List
                    (string)mainwindow.cboVideoQuality.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Pass_ItemSource = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboPass, // ComboBox
                    Pass_ItemSource, // New Items List
                    (string)mainwindow.cboPass.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Optimize_ItemSource = new List<string>()
                {
                    "None",
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboOptimize, // ComboBox
                    Optimize_ItemSource, // New Items List
                    (string)mainwindow.cboOptimize.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                List<Control> Controls_Enabled = new List<Control>()
                {
                    // Video Quality ComboBox
                    mainwindow.cboVideoQuality,
                    // FPS ComboBox
                    mainwindow.cboFPS,
                    // Optimize ComboBox
                    mainwindow.cboOptimize,
                    // Scaling ComboBox
                    mainwindow.cboScaling
                };

                // Add CRF Custom Textbox
                if ((string)mainwindow.cboVideoQuality.SelectedItem == "Custom")
                {
                    Controls_Enabled.Add(mainwindow.crfCustom);
                }

                // Enable
                for (int i = 0; i < Controls_Enabled.Count; i++)
                {
                    Controls_Enabled[i].IsEnabled = true;
                }

                // -------------------------
                // Disabled
                // -------------------------
                // None
            }

            // --------------------------------------------------
            // Theora
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
            {
                // -------------------------
                // Video
                // -------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                List<string> VideoQuality_ItemSource = new List<string>()
                {
                    "Auto",
                    //"Lossless", //disabled
                    "Ultra",
                    "High",
                    "Medium",
                    "Low",
                    "Sub",
                    "Custom"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboVideoQuality, // ComboBox
                    VideoQuality_ItemSource, // New Items List
                    (string)mainwindow.cboVideoQuality.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Pass_ItemSource = new List<string>()
                {
                    "1 Pass",
                    //"2 Pass" //disabled
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboPass, // ComboBox
                    Pass_ItemSource, // New Items List
                    "1 Pass"); // Selected Item


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Optimize_ItemSource = new List<string>()
                {
                    "None",
                    "Web"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboOptimize, // ComboBox
                    Optimize_ItemSource, // New Items List
                    (string)mainwindow.cboOptimize.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                List<Control> Controls_Enabled = new List<Control>()
                {
                    // Video Quality ComboBox
                    mainwindow.cboVideoQuality,
                    // FPS ComboBox
                    mainwindow.cboFPS,
                    // Optimize ComboBox
                    mainwindow.cboOptimize,
                    // Scaling ComboBox
                    mainwindow.cboScaling
                };

                // Enable
                for (int i = 0; i < Controls_Enabled.Count; i++)
                {
                    Controls_Enabled[i].IsEnabled = true;
                }

                // -------------------------
                // Disabled
                // -------------------------
                List<Control> Controls_Disabled = new List<Control>()
                {
                    // Speed
                    mainwindow.cboSpeed,
                };

                // Add CRF Custom Textbox
                if ((string)mainwindow.cboVideoQuality.SelectedItem == "Custom")
                {
                    Controls_Disabled.Add(mainwindow.crfCustom);
                }

                // Disable
                for (int i = 0; i < Controls_Disabled.Count; i++)
                {
                    Controls_Disabled[i].IsEnabled = false;
                }
            }

            // --------------------------------------------------
            // JPEG
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
            {
                // -------------------------
                // Video
                // -------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                List<string> VideoQuality_ItemSource = new List<string>()
                {
                    "Auto",
                    "Ultra",
                    "High",
                    "Medium",
                    "Low",
                    "Sub",
                    "Custom"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboVideoQuality, // ComboBox
                    VideoQuality_ItemSource, // New Items List
                    (string)mainwindow.cboVideoQuality.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Pass_ItemSource = new List<string>()
                {
                    "1 Pass"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboPass, // ComboBox
                    Pass_ItemSource, // New Items List
                    "1 Pass"); // Selected Item


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Optimize_ItemSource = new List<string>()
                {
                    "None"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboOptimize, // ComboBox
                    Optimize_ItemSource, // New Items List
                    "None"); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                List<Control> Controls_Enabled = new List<Control>()
                {
                    // Video Quality ComboBox
                    mainwindow.cboVideoQuality,
                    // Scaling ComboBox
                    mainwindow.cboScaling
                };

                // Enable
                for (int i = 0; i < Controls_Enabled.Count; i++)
                {
                    Controls_Enabled[i].IsEnabled = true;
                }

                // -------------------------
                // Disabled
                // -------------------------
                List<Control> Controls_Disabled = new List<Control>()
                {
                    // Speed ComboBox
                    mainwindow.cboSpeed,
                    // CRF Custom Textbox
                    mainwindow.crfCustom,
                    // Pass ComboBox
                    mainwindow.cboPass,
                    // FPS ComboBox
                    mainwindow.cboFPS,
                    // Optimize ComboBox
                    mainwindow.cboOptimize,
                };

                // Disable
                for (int i = 0; i < Controls_Disabled.Count; i++)
                {
                    Controls_Disabled[i].IsEnabled = false;
                }
            }

            // --------------------------------------------------
            // PNG
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
            {
                // -------------------------
                // Video
                // -------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                List<string> VideoQuality_ItemSource = new List<string>()
                {
                    "Auto",
                    "Lossless",
                    "Ultra",
                    "High",
                    "Medium",
                    "Low",
                    "Sub"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboVideoQuality, // ComboBox
                    VideoQuality_ItemSource, // New Items List
                    "Lossless"); // Selected Item


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Pass_ItemSource = new List<string>()
                {
                    "1 Pass"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboPass, // ComboBox
                    Pass_ItemSource, // New Items List
                    "1 Pass"); // Selected Item


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Optimize_ItemSource = new List<string>()
                {
                    "None"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboOptimize, // ComboBox
                    Optimize_ItemSource, // New Items List
                    "None"); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                List<Control> Controls_Enabled = new List<Control>()
                {
                    // Scaling ComboBox
                    mainwindow.cboScaling
                };

                // Enable
                for (int i = 0; i < Controls_Enabled.Count; i++)
                {
                    Controls_Enabled[i].IsEnabled = true;
                }

                // -------------------------
                // Disabled
                // -------------------------
                List<Control> Controls_Disabled = new List<Control>()
                {
                    // Speed ComboBox
                    mainwindow.cboSpeed,
                    // Video Quality ComboBox
                    mainwindow.cboVideoQuality,
                    // CRF Custom Textbox
                    mainwindow.crfCustom,
                    // Pass ComboBox
                    mainwindow.cboPass,
                    // FPS ComboBox
                    mainwindow.cboFPS,
                    // Optimize ComboBox
                    mainwindow.cboOptimize,
                };

                // Disable
                for (int i = 0; i < Controls_Disabled.Count; i++)
                {
                    Controls_Disabled[i].IsEnabled = false;
                }
            }

            // --------------------------------------------------
            // WebP
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "WebP")
            {
                // -------------------------
                // Video
                // -------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                List<string> VideoQuality_ItemSource = new List<string>()
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

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboVideoQuality, // ComboBox
                    VideoQuality_ItemSource, // New Items List
                    (string)mainwindow.cboVideoQuality.SelectedItem); // Selected Item


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Pass_ItemSource = new List<string>()
                {
                    "1 Pass"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboPass, // ComboBox
                    Pass_ItemSource, // New Items List
                    "1 Pass"); // Selected Item


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Optimize_ItemSource = new List<string>()
                {
                    "None"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboOptimize, // ComboBox
                    Optimize_ItemSource, // New Items List
                    "None"); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                List<Control> Controls_Enabled = new List<Control>()
                {
                    // Video Quality ComboBox
                    mainwindow.cboVideoQuality,
                    // Scaling ComboBox
                    mainwindow.cboScaling
                };

                // Enable
                for (int i = 0; i < Controls_Enabled.Count; i++)
                {
                    Controls_Enabled[i].IsEnabled = true;
                }

                // -------------------------
                // Disabled
                // -------------------------
                List<Control> Controls_Disabled = new List<Control>()
                {
                    // Speed ComboBox
                    mainwindow.cboSpeed,
                    // CRF Custom Textbox
                    mainwindow.crfCustom,
                    // Pass ComboBox
                    mainwindow.cboPass,
                    // FPS ComboBox
                    mainwindow.cboFPS,
                    // Optimize ComboBox
                    mainwindow.cboOptimize,
                };

                // Disable
                for (int i = 0; i < Controls_Disabled.Count; i++)
                {
                    Controls_Disabled[i].IsEnabled = false;
                }
            }


            // --------------------------------------------------
            // Copy
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy")
            {
                // -------------------------
                // Video
                // -------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                List<string> VideoQuality_ItemSource = new List<string>()
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

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboVideoQuality, // ComboBox
                    VideoQuality_ItemSource, // New Items List
                    "Auto"); // Selected Item


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Pass_ItemSource = new List<string>()
                {
                    "auto"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboPass, // ComboBox
                    Pass_ItemSource, // New Items List
                    "auto"); // Selected Item


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Optimize_ItemSource = new List<string>()
                {
                    "None"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboOptimize, // ComboBox
                    Optimize_ItemSource, // New Items List
                    "None"); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                List<Control> Controls_Enabled = new List<Control>()
                {
                    // Video Quality ComboBox
                    mainwindow.cboVideoQuality,
                };

                // Enable
                for (int i = 0; i < Controls_Enabled.Count; i++)
                {
                    Controls_Enabled[i].IsEnabled = true;
                }

                // -------------------------
                // Disabled
                // -------------------------
                List<Control> Controls_Disabled = new List<Control>()
                {
                    // Speed ComboBox
                    mainwindow.cboSpeed,
                    // CRF Custom Textbox
                    mainwindow.crfCustom,
                    // Pass ComboBox
                    mainwindow.cboPass,
                    // FPS ComboBox
                    mainwindow.cboFPS,
                    // Optimize ComboBox
                    mainwindow.cboOptimize,
                    // Scaling ComboBox
                    mainwindow.cboScaling
                };

                // Disable
                for (int i = 0; i < Controls_Disabled.Count; i++)
                {
                    Controls_Disabled[i].IsEnabled = false;
                }
            }

            // --------------------------------------------------
            // None
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "None")
            {
                // -------------------------
                // Video
                // -------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                List<string> VideoQuality_ItemSource = new List<string>()
                {
                    "None"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboVideoQuality, // ComboBox
                    VideoQuality_ItemSource, // New Items List
                    "None"); // Selected Item


                // --------------------------------------------------
                // Pass
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Pass_ItemSource = new List<string>()
                {
                    "auto"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboPass, // ComboBox
                    Pass_ItemSource, // New Items List
                    "auto"); // Selected Item


                // --------------------------------------------------
                // Optimize
                // --------------------------------------------------
                // -------------------------
                // Change ItemSource
                // -------------------------
                Optimize_ItemSource = new List<string>()
                {
                    "None"
                };

                ChangeItemSource(
                    mainwindow,
                    mainwindow.cboOptimize, // ComboBox
                    Optimize_ItemSource, // New Items List
                    "None"); // Selected Item


                // --------------------------------------------------
                // Controls
                // --------------------------------------------------
                // -------------------------
                // Enabled
                // -------------------------
                // None

                // -------------------------
                // Disabled
                // -------------------------
                List<Control> Controls_Disabled = new List<Control>()
                {
                    // Video Quality ComboBox
                    mainwindow.cboVideoQuality,
                    // Speed ComboBox
                    mainwindow.cboSpeed,
                    // CRF Custom Textbox
                    mainwindow.crfCustom,
                    // Pass ComboBox
                    mainwindow.cboPass,
                    // FPS ComboBox
                    mainwindow.cboFPS,
                    // Optimize ComboBox
                    mainwindow.cboOptimize,
                    // Scaling ComboBox
                    mainwindow.cboScaling
                };

                // Disable
                for (int i = 0; i < Controls_Disabled.Count; i++)
                {
                    Controls_Disabled[i].IsEnabled = false;
                }
            }


            // If Video ComboBox Null, Select Default Auto
            else if (string.IsNullOrEmpty((string)mainwindow.cboVideoQuality.SelectedItem))
            {
                mainwindow.cboVideoQuality.SelectedIndex = 0;
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
                if ((string)mainwindow.cboVideoQuality.SelectedItem == "Auto"
                    && (string)mainwindow.cboSize.SelectedItem == "Source"
                    && string.IsNullOrEmpty(CropWindow.crop)
                    && (string)mainwindow.cboFPS.SelectedItem == "auto"
                    && (string)mainwindow.cboPixelFormat.SelectedItem == "auto"
                    && (string)mainwindow.cboOptimize.SelectedItem == "None"
                    && (string)mainwindow.cboScaling.SelectedItem == "default"

                    // Filters
                    // Fix
                    && (string)mainwindow.cboFilterVideo_Deband.SelectedItem == "disabled"
                    && (string)mainwindow.cboFilterVideo_Deshake.SelectedItem == "disabled"
                    && (string)mainwindow.cboFilterVideo_Deflicker.SelectedItem == "disabled"
                    && (string)mainwindow.cboFilterVideo_Dejudder.SelectedItem == "disabled"
                    && (string)mainwindow.cboFilterVideo_Denoise.SelectedItem == "disabled"
                    // Selective Color
                    // Reds
                    && mainwindow.slFiltersVideo_SelectiveColor_Reds_Cyan.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Reds_Magenta.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Reds_Yellow.Value == 0
                    // Yellows
                    && mainwindow.slFiltersVideo_SelectiveColor_Yellows_Cyan.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Yellows_Magenta.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Yellows_Yellow.Value == 0
                    // Greens
                    && mainwindow.slFiltersVideo_SelectiveColor_Greens_Cyan.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Greens_Magenta.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Greens_Yellow.Value == 0
                    // Cyans
                    && mainwindow.slFiltersVideo_SelectiveColor_Cyans_Cyan.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Cyans_Magenta.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Cyans_Yellow.Value == 0
                    // Blues
                    && mainwindow.slFiltersVideo_SelectiveColor_Blues_Cyan.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Blues_Magenta.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Blues_Yellow.Value == 0
                    // Magentas
                    && mainwindow.slFiltersVideo_SelectiveColor_Magentas_Cyan.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Magentas_Magenta.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Magentas_Yellow.Value == 0
                    // Whites
                    && mainwindow.slFiltersVideo_SelectiveColor_Whites_Cyan.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Whites_Magenta.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Whites_Yellow.Value == 0
                    // Neutrals
                    && mainwindow.slFiltersVideo_SelectiveColor_Neutrals_Cyan.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Neutrals_Magenta.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Neutrals_Yellow.Value == 0
                    // Blacks
                    && mainwindow.slFiltersVideo_SelectiveColor_Blacks_Cyan.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Blacks_Magenta.Value == 0
                    && mainwindow.slFiltersVideo_SelectiveColor_Blacks_Yellow.Value == 0

                    // EQ
                    && mainwindow.slFiltersVideo_EQ_Brightness.Value == 0
                    && mainwindow.slFiltersVideo_EQ_Contrast.Value == 0
                    && mainwindow.slFiltersVideo_EQ_Saturation.Value == 0
                    && mainwindow.slFiltersVideo_EQ_Gamma.Value == 0

                    // Extension Match
                    && string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    //// -------------------------
                    //// Insert Copy if Does Not Contain
                    //// -------------------------
                    //if (!VideoCodecItemSource.Contains("Copy"))
                    //{
                    //    VideoCodecItemSource.Insert(0, "Copy");
                    //}
                    //// Populate ComboBox from ItemSource
                    //mainwindow.cboVideoCodec.ItemsSource = VideoCodecItemSource;

                    //System.Windows.MessageBox.Show(string.Join("\n", vm.cboVideoCodec_Items)); //debug

                    // -------------------------
                    // Set Video Codec Combobox Selected Item to Copy
                    // -------------------------
                    //if (vm.cboVideoCodec_Items.Count > 0)
                    if (VideoCodec_ItemSource.Count > 0)
                    {
                        if (VideoCodec_ItemSource.Contains("Copy"))
                        {
                            mainwindow.cboVideoCodec.SelectedItem = "Copy";

                            //System.Windows.MessageBox.Show("Copy"); //debug

                            //return;
                        }
                    }
                }

                // -------------------------
                // Select Copy - Batch
                // -------------------------
                else if ((string)mainwindow.cboVideoQuality.SelectedItem == "Auto" &&
                         (string)mainwindow.cboSize.SelectedItem == "Source" &&
                         string.IsNullOrEmpty(CropWindow.crop) &&
                         (string)mainwindow.cboFPS.SelectedItem == "auto" &&
                         (string)mainwindow.cboOptimize.SelectedItem == "None" &&

                         // Batch Extension Match
                         mainwindow.tglBatch.IsChecked == true &&
                         string.Equals(MainWindow.batchExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    //// -------------------------
                    //// Insert Copy if Does Not Contain
                    //// -------------------------
                    //if (!VideoCodecItemSource.Contains("Copy"))
                    //{
                    //    VideoCodecItemSource.Insert(0, "Copy");
                    //}
                    //// Populate ComboBox from ItemSource
                    //mainwindow.cboVideoCodec.ItemsSource = VideoCodecItemSource;

                    // -------------------------
                    // Set Video Codec Combobox Selected Item to Copy
                    // -------------------------
                    if (VideoCodec_ItemSource.Count > 0)
                    {
                        if (VideoCodec_ItemSource.Contains("Copy"))
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
                    if (!string.IsNullOrEmpty((string)mainwindow.cboVideoQuality.SelectedItem))
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
                                // MPG
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "mpg")
                                {
                                    mainwindow.cboVideoCodec.SelectedItem = "MPEG-2";
                                }
                                // -------------------------
                                // AVI
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "avi")
                                {
                                    mainwindow.cboVideoCodec.SelectedItem = "MPEG-4";
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
                                // -------------------------
                                // WebP
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "webp")
                                {
                                    mainwindow.cboVideoCodec.SelectedItem = "WebP";
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
                if ((string)mainwindow.cboVideoQuality.SelectedItem == "Auto"
                    && (string)mainwindow.cboSize.SelectedItem == "Source"
                    && string.IsNullOrEmpty(CropWindow.crop)
                    && (string)mainwindow.cboFPS.SelectedItem == "auto"
                    && (string)mainwindow.cboOptimize.SelectedItem == "None"

                    // Extension Match
                    && string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    //// -------------------------
                    //// Insert Copy if Does Not Contain
                    //// -------------------------
                    //if (!SubtitleCodecItemSource.Contains("Copy"))
                    //{
                    //    SubtitleCodecItemSource.Insert(0, "Copy");
                    //}
                    //// Populate ComboBox from ItemSource
                    //mainwindow.cboSubtitleCodec.ItemsSource = SubtitleCodecItemSource;

                    // -------------------------
                    // Set Subtitle Codec Combobox Selected Item to Copy
                    // -------------------------
                    if (SubtitleCodec_ItemSource.Count > 0)
                    {
                        if (SubtitleCodec_ItemSource.Contains("Copy"))
                        {
                            mainwindow.cboSubtitleCodec.SelectedItem = "Copy";

                            //return;
                        }
                    }
                }

                // -------------------------
                // Select Copy - Batch
                // -------------------------
                else if ((string)mainwindow.cboVideoQuality.SelectedItem == "Auto"
                    && (string)mainwindow.cboSize.SelectedItem == "Source"
                    && string.IsNullOrEmpty(CropWindow.crop)
                    && (string)mainwindow.cboFPS.SelectedItem == "auto"
                    && (string)mainwindow.cboOptimize.SelectedItem == "None"

                    // Batch Extension Match
                    && mainwindow.tglBatch.IsChecked == true
                    && string.Equals(MainWindow.batchExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    //// -------------------------
                    //// Insert Copy if Does Not Contain
                    //// -------------------------
                    //if (!SubtitleCodecItemSource.Contains("Copy"))
                    //{
                    //    SubtitleCodecItemSource.Insert(0, "Copy");
                    //}
                    //// Populate ComboBox from ItemSource
                    //mainwindow.cboSubtitleCodec.ItemsSource = SubtitleCodecItemSource;

                    // -------------------------
                    // Set Subtitle Codec Combobox Selected Item to Copy
                    // -------------------------
                    if (SubtitleCodec_ItemSource.Count > 0)
                    {
                        if (SubtitleCodec_ItemSource.Contains("Copy"))
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
                    if (!string.IsNullOrEmpty((string)mainwindow.cboSubtitlesStream.SelectedItem))
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
                                // MPG
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "mpg")
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
                                // -------------------------
                                // WebP
                                // -------------------------
                                else if ((string)mainwindow.cboFormat.SelectedItem == "webp")
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
            // MPEG-2 / MPEG-4 Lossless (Special Rules)
            // -------------------------
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "MPEG-2"
                || (string)mainwindow.cboVideoCodec.SelectedItem == "MPEG-4")
            {
                // -------------------------
                // Lossless is VBR -q:v 2
                // VBR can only be 1 Pass
                // -------------------------
                if ((string)mainwindow.cboVideoQuality.SelectedItem == "Lossless")
                {
                    // Change ItemSource
                    Pass_ItemSource = new List<string>()
                    {
                        "1 Pass",
                    };

                    //Change Item Source
                    if (Pass_ItemSource.Count > 0)
                    {
                        Pass_ItemSource.Clear();
                    }

                    //Pass_ItemSource.ForEach(ViewModel._cboVideoPass_Items.Add);

                    for (int i = 0; i < Pass_ItemSource.Count; i++)
                    {
                        Pass_ItemSource.Add(Pass_ItemSource[i]);
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
                    Pass_ItemSource = new List<string>()
                    {
                        "1 Pass",
                        "2 Pass",
                    };

                    // Populate ComboBox from ItemSource
                    mainwindow.cboPass.ItemsSource = Pass_ItemSource;
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
            if ((string)mainwindow.cboVideoQuality.SelectedItem == "Custom")
            {
                // -------------------------
                // Disable CRF TextBox if 1 Pass or 2 Pass
                // -------------------------
                if ((string)mainwindow.cboPass.SelectedItem == "1 Pass" ||
                    (string)mainwindow.cboPass.SelectedItem == "2 Pass")
                {
                    mainwindow.crfCustom.IsEnabled = false;
                    mainwindow.cboPass.IsEnabled = true;

                    // Set CRF back to Default value
                    //mainwindow.crfCustom.Text = "CRF";
                    mainwindow.crfCustom.Text = string.Empty;
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
            if ((string)mainwindow.cboVideoQuality.SelectedItem == "Auto")
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
            else if ((string)mainwindow.cboVideoQuality.SelectedItem != "Auto")
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
            // MediaType
            // -------------------------
            if ((string)mainwindow.cboMediaType.SelectedItem == "Video"
                || (string)mainwindow.cboMediaType.SelectedItem == "Image"
                || (string)mainwindow.cboMediaType.SelectedItem == "Sequence")
            {
                // -------------------------
                // VP8
                // -------------------------
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8")
                {
                    mainwindow.cboPixelFormat.IsEnabled = true;
                    mainwindow.cboPixelFormat.SelectedItem = "yuv420p";
                    // VP8 Lossless Disabled
                }

                // -------------------------
                // VP9
                // x264
                // x265
                // AV1
                // Theora
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9"
                    || (string)mainwindow.cboVideoCodec.SelectedItem == "x264"
                    || (string)mainwindow.cboVideoCodec.SelectedItem == "x265"
                    || (string)mainwindow.cboVideoCodec.SelectedItem == "AV1"
                    || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
                {
                    mainwindow.cboPixelFormat.IsEnabled = true;

                    // Lossless
                    if ((string)mainwindow.cboVideoQuality.SelectedItem == "Lossless")
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
                // MPEG-2
                // MPEG-4
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "MPEG-2"
                    || (string)mainwindow.cboVideoCodec.SelectedItem == "MPEG-4")
                {
                    mainwindow.cboPixelFormat.IsEnabled = true;

                    // Lossless can't be yuv444p
                    // All Pixel Formats yuv420p
                    mainwindow.cboPixelFormat.SelectedItem = "yuv420p"; 
                }

                // -------------------------
                // JPG
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
                {
                    mainwindow.cboPixelFormat.IsEnabled = true;
                    mainwindow.cboPixelFormat.SelectedItem = "yuvj444p";
                }

                // -------------------------
                // PNG
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                {
                    mainwindow.cboPixelFormat.IsEnabled = true;
                    mainwindow.cboPixelFormat.SelectedItem = "rgb24";
                }

                // -------------------------
                // WebP
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "WebP")
                {
                    mainwindow.cboPixelFormat.IsEnabled = true;

                    // Lossless
                    if ((string)mainwindow.cboVideoQuality.SelectedItem == "Lossless")
                    {
                        mainwindow.cboPixelFormat.SelectedItem = "bgra";
                    }
                    // All Other Quality
                    else
                    {
                        mainwindow.cboPixelFormat.SelectedItem = "yuv420p";
                    }
                }

                // -------------------------
                // Copy
                // -------------------------
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy")
                {
                    mainwindow.cboPixelFormat.IsEnabled = false;
                    mainwindow.cboPixelFormat.SelectedItem = "auto";
                }

                // -------------------------
                // All Other Codecs
                // -------------------------
                else
                {
                    mainwindow.cboPixelFormat.IsEnabled = false;
                    mainwindow.cboPixelFormat.SelectedItem = "auto";
                }
            }

            // -------------------------
            // MediaType Audio
            // -------------------------
            else if ((string)mainwindow.cboMediaType.SelectedItem == "Audio")
            {
                mainwindow.cboPixelFormat.SelectedItem = "auto";
                mainwindow.cboPixelFormat.IsEnabled = false;
            }

            // -------------------------
            // Auto
            // -------------------------
            if ((string)mainwindow.cboVideoQuality.SelectedItem == "Auto")
            {
                mainwindow.cboPixelFormat.SelectedItem = "auto";
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
                Optimize_Tune_ItemSource = new List<string>()
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
                Optimize_Profile_ItemSource = new List<string>()
                {
                    "none",
                    "baseline",
                    "main",
                    "high"
                };

                // -------------------------
                // Level
                // -------------------------
                Optimize_Level_ItemSource = new List<string>()
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
                Optimize_Tune_ItemSource = new List<string>()
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
                Optimize_Profile_ItemSource = new List<string>()
                {
                    "none",
                    "main",
                    //"main-intra",
                    "mainstillpicture",
                    "main444-8",
                    //"main444-intra",
                    "main444-stillpicture",
                    "main10",
                    //"main10-intra",
                    "main422-10",
                    //"main422-10-intra",
                    "main444-10",
                    //"main444-10-intra",
                    "main12",
                    "main422-12",
                    //"main422-12-intra",
                    "main444-12",
                    //"main444-12-intra",
                };

                // -------------------------
                // Level
                // -------------------------
                Optimize_Level_ItemSource = new List<string>()
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
                Optimize_Tune_ItemSource = new List<string>()
                {
                    "none"
                };

                // -------------------------
                // Profile
                // -------------------------
                Optimize_Profile_ItemSource = new List<string>()
                {
                    "none"
                };

                // -------------------------
                // Level
                // -------------------------
                Optimize_Level_ItemSource = new List<string>()
                {
                    "none"
                };
            }

            // -------------------------
            // Populate Optimize Tune, Profile, Level ComboBox from ItemSource
            // -------------------------
            mainwindow.cboOptTune.ItemsSource = Optimize_Tune_ItemSource;
            mainwindow.cboOptProfile.ItemsSource = Optimize_Profile_ItemSource;
            mainwindow.cboOptLevel.ItemsSource = Optimize_Level_ItemSource;

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
                if ((string)mainwindow.cboOptimize.SelectedItem == "None")
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
                //else if ((string)mainwindow.cboOptimize.SelectedItem == "DVD")
                //{
                //    mainwindow.cboOptTune.SelectedItem = "none";
                //    mainwindow.cboOptProfile.SelectedItem = "baseline";
                //    mainwindow.cboOptLevel.SelectedItem = "3.0";
                //    Video.optFlags = "-maxrate 9.6M";
                //}
                // HD Video
                else if ((string)mainwindow.cboOptimize.SelectedItem == "HD Video")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "main";
                    mainwindow.cboOptLevel.SelectedItem = "4.2";
                    Video.optFlags = string.Empty;
                }
                // PC HD
                else if ((string)mainwindow.cboOptimize.SelectedItem == "PC HD")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "main";
                    mainwindow.cboOptLevel.SelectedItem = "4.2";
                    Video.optFlags = string.Empty;
                }
                // PC SD
                else if ((string)mainwindow.cboOptimize.SelectedItem == "PC SD")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "baseline";
                    mainwindow.cboOptLevel.SelectedItem = "3.1";
                }
                // Animation
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Animation")
                {
                    mainwindow.cboOptTune.SelectedItem = "animation";
                    mainwindow.cboOptProfile.SelectedItem = "main";
                    mainwindow.cboOptLevel.SelectedItem = "4.2";
                    Video.optFlags = string.Empty;
                }
                // Blu-ray
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Blu-ray")
                {
                    mainwindow.cboOptTune.SelectedItem = "none";
                    mainwindow.cboOptProfile.SelectedItem = "main";
                    mainwindow.cboOptLevel.SelectedItem = "4.1";
                    Video.optFlags = "-deblock 0:0 -sar 1/1 -x264-params \"bluray-compat=1:level=4.1:open-gop=1:slices=4:tff=1:colorprim=bt709:colormatrix=bt709:vbv-maxrate=40000:vbv-bufsize=30000:me=umh:ref=4:nal-hrd=vbr:aud=1:b-pyramid=strict\"";
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
                    //mainwindow.vMaxrateCustom.Text = "9.8M";
                    //Video.optFlags = "-maxrate 9.8M";
                    Video.optFlags = string.Empty;
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
            if ((string)mainwindow.cboOptimize.SelectedItem == "None")
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
                // -------------------------
                // None Stream
                // -------------------------
                if ((string)mainwindow.cboSubtitlesStream.SelectedItem == "none")
                {
                    mainwindow.cboSubtitleCodec.SelectedItem = "None";
                    //mainwindow.cboSubtitleCodec.IsEnabled = false;
                }

                // -------------------------
                // All, External, & Stream Number
                // -------------------------
                else
                {
                    // Formats

                    if ((string)mainwindow.cboSubtitleCodec.SelectedItem != "Burn") // Ignore if Burn
                    {
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

                        // MPG
                        else if ((string)mainwindow.cboFormat.SelectedItem == "mpg")
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
