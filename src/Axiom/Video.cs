using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

/* ----------------------------------------------------------------------
Axiom
Copyright (C) 2017 Matt McManis
http://github.com/MattMcManis/Axiom
http://www.x.co/axiomui
axiom.interface@gmail.com

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

namespace Axiom
{
    public partial class Video
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
        public static List<string> VideoCodecItemSource = new List<string>();
        public static List<string> VideoItemSource = new List<string>();
        public static List<string> PassItemSource = new List<string>();
        public static List<string> ResizeItemSource = new List<string>();
        public static List<string> CutItemSource = new List<string>();
        public static List<string> TuneItemSource = new List<string>();
        public static List<string> OptimizeItemSource = new List<string>();
        public static List<string> SpeedItemSource = new List<string>();

        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Variables
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // Video
        public static string vCodec;
        public static string vQual;
        public static string vBitMode;
        public static string vBitrate;
        public static string vMaxrate; // NOT USED RIGHT NOW, set in Optimize options instead
        public static string vOptions;
        public static string crf; // ffmpeg quality
        public static string fps; // frames per second
        public static string tune; // x264 & x265 tuning modes
        public static string options;
        public static string optimizeProfile;
        public static string optimizeLevel;
        public static string optimize;
        public static string speed; // speed combobox modifier

        // Scale
        public static string aspect; // contains scale, width, height
        public static string scale; // -vf
        public static string divisibleCrop; //used on mp4 custom size to keep divisible by 2
        public static string width;
        public static string height;

        // Pass
        public static bool passUserSelected = false; // Used to determine if User willingly selected CRF, 1 Pass or 2 Pass
        public static int? v2passSwitch = 0;
        public static string v2pass; // contains pass2
        public static string pass1; // enabled in ffmpeg main line if v2Pass is enabled
        public static string pass2;
        public static int? v2passBatchSwitch = 0;
        public static string v2passBatch; // enabled if auto and batched checked
        public static string cmdBatch_vQual; // cmd batch Video Auto dynamic value

        // Filter
        public static List<string> VideoFilters = new List<string>(); // Filters to String Join
        public static int? vFilterSwitch = 0;
        public static string geq; // png transparent to jpg whtie background filter
        public static string vFilter;



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
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy") { mainwindow.cboSpeed.IsEnabled = true; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8") { mainwindow.cboSpeed.IsEnabled = true; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9") { mainwindow.cboSpeed.IsEnabled = true; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264") { mainwindow.cboSpeed.IsEnabled = true; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265") { mainwindow.cboSpeed.IsEnabled = true; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG") { mainwindow.cboSpeed.IsEnabled = false; }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "PNG") { mainwindow.cboSpeed.IsEnabled = false; }
            else if (string.IsNullOrEmpty((string)mainwindow.cboVideoCodec.SelectedItem)) { mainwindow.cboSpeed.IsEnabled = false; }
            //Container
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
            if ((string)mainwindow.cboFormat.SelectedItem == "mkv" && (string)mainwindow.cboVideoCodec.SelectedItem == "Copy")
            {
                mainwindow.cboVideo.SelectedItem = "Auto";
            }


            // --------------------------------------------------
            // VP8
            // --------------------------------------------------
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8")
            {
                //string previousItem;

                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>() { "Auto", "Ultra", "High", "Medium", "Low", "Sub", "Custom" };

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
                //MessageBox.Show(previousItem); //debug
                //var message = string.Join(Environment.NewLine, VideoCodecItemSource); //debug
                //MessageBox.Show(message); //debug

                // Change ItemSource
                PassItemSource = new List<string>() { "CRF", "1 Pass", "2 Pass" };

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
                OptimizeItemSource = new List<string>() { "none", "Web" };

                // Populate ComboBox from ItemSource
                mainwindow.cboOptimize.ItemsSource = OptimizeItemSource;

                // Select Item
                mainwindow.cboOptimize.SelectedItem = "Web";

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
                //string previousItem;

                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>() { "Auto", "Lossless", "Ultra", "High", "Medium", "Low", "Sub", "Custom" };

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
                PassItemSource = new List<string>() { "CRF", "1 Pass", "2 Pass" };

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
                OptimizeItemSource = new List<string>() { "none", "Web" };

                // Populate ComboBox from ItemSource
                mainwindow.cboOptimize.ItemsSource = OptimizeItemSource;

                // Select Item
                mainwindow.cboOptimize.SelectedItem = "Web";

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
                //string previousItem;

                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>() { "Auto", "Lossless", "Ultra", "High", "Medium", "Low", "Sub", "Custom" };

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
                PassItemSource = new List<string>() { "CRF", "1 Pass", "2 Pass" };

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
                OptimizeItemSource = new List<string>() { "none", "Advanced", "Web", "DVD", "Blu-ray", "Windows", "Apple", "Android", "PS3", "PS4", "Xbox 360", "Xbox One" };

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
                    OptimizeItemSource = new List<string>() { "none", "Advanced", "Windows", "Apple", "Android", "PS3", "PS4", "Xbox 360", "Xbox One" };

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
                //string previousItem;

                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>() { "Auto", "Lossless", "Ultra", "High", "Medium", "Low", "Sub", "Custom" };

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
                PassItemSource = new List<string>() { "CRF", "1 Pass", "2 Pass" };

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
                OptimizeItemSource = new List<string>() { "none", "Advanced", "Web", "Windows", "Apple", "Android" };

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
                    OptimizeItemSource = new List<string>() { "none", "Windows", "Apple", "Android" };

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
            // Theora
            // --------------------------------------------------
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
            {
                //string previousItem;

                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>() { "Auto", "Lossless", "Ultra", "High", "Medium", "Low", "Sub", "Custom" };

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
                PassItemSource = new List<string>() { "1 Pass", "2 Pass" };

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
                OptimizeItemSource = new List<string>() { "none", "Web" };

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
                //string previousItem;

                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>() { "Auto", "Ultra", "High", "Medium", "Low", "Sub" };

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
                //string previousItem;

                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>() { "Auto", "Lossless", "Ultra", "High", "Medium", "Low", "Sub" };

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
                //string previousItem;

                // -------------------------
                // Video
                // -------------------------
                // Get Previous Item
                previousItem = (string)mainwindow.cboVideo.SelectedItem;

                // Change ItemSource
                VideoItemSource = new List<string>() { "Auto", "Lossless", "Ultra", "High", "Medium", "Low", "Sub", "Custom" };

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
                //string previousItem;

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
        /// Pass Controls (Method)
        /// <summary>
        public static void Pass(MainWindow mainwindow)
        {
            // -------------------------
            // Pass ComboBox Custom
            // -------------------------
            // Disable CRF TextBox if 1 Pass or 2 Pass
            if ((string)mainwindow.cboVideo.SelectedItem == "Custom" && (string)mainwindow.cboPass.SelectedItem == "1 Pass" | (string)mainwindow.cboPass.SelectedItem == "2 Pass")
            {
                mainwindow.crfCustom.IsEnabled = false;
                mainwindow.cboPass.IsEnabled = true;
            }

            // Enable CRF TextBox if CRF
            if ((string)mainwindow.cboVideo.SelectedItem == "Custom" && (string)mainwindow.cboPass.SelectedItem == "CRF")
            {
                mainwindow.crfCustom.IsEnabled = true;
                mainwindow.cboPass.IsEnabled = true;

                // Disable CRF for Theora
                if ((string)mainwindow.cboVideo.SelectedItem == "Custom" && (string)mainwindow.cboVideoCodec.SelectedItem == "Theora") // Special Rule
                {
                    mainwindow.crfCustom.IsEnabled = false;
                    mainwindow.cboPass.IsEnabled = true;
                }
            }


            // -------------------------
            // Pass ComboBox Auto
            // -------------------------
            // Set Pass ComboBox to 2 Pass and Disable 
            if ((string)mainwindow.cboVideo.SelectedItem == "Auto" && (string)mainwindow.cboPass.SelectedItem != "2 Pass")
            {
                mainwindow.cboPass.SelectedItem = "2 Pass";
                // Disable Pass ComboBox if 2 Pass
                mainwindow.cboPass.IsEnabled = false;

                //MessageBox.Show("hello"); //debug
            }
            // If Auto Disable 2 Pass Always
            if ((string)mainwindow.cboVideo.SelectedItem == "Auto" && (string)mainwindow.cboPass.SelectedItem == "2 Pass")
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



        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Process Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Video Codecs (Method)
        /// <summary>
        public static void VideoCodecs(MainWindow mainwindow)
        {
            // #################
            // Video
            // #################
            if (string.IsNullOrEmpty((string)mainwindow.cboVideoCodec.SelectedItem))
            {
                vCodec = string.Empty; Streams.vMap = "-vn";
            }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "None")
            {
                vCodec = string.Empty; Streams.vMap = "-vn";
            }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8")
            {
                vCodec = "-vcodec libvpx";
            }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
            {
                vCodec = "-vcodec libvpx-vp9 -tile-columns 6 -frame-parallel 1 -auto-alt-ref 1 -lag-in-frames 25 -g 128";
            }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
            {
                vCodec = "-vcodec libtheora";
            }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
            {
                vCodec = "-vcodec libx264"; //leave profile:v main here so MKV can choose other ???
            }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
            {
                vCodec = "-vcodec libx265"; //does not use profile:v
            }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
            {
                vCodec = string.Empty;
            }
            else if ((string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
            {
                vCodec = string.Empty;
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {         
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new Bold(new Run("Codec: ")) { Foreground = Log.ConsoleDefault });
                Log.paragraph.Inlines.Add(new Run(Convert.ToString(mainwindow.cboVideoCodec.SelectedItem)) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);
        }


        /// <summary>
        /// Video - Auto Codec Copy (Method)
        /// <summary>
        public static void AutoVideoCodecCopy(MainWindow mainwindow) // Method
        {
            // Set Video Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
            if ((string)mainwindow.cboVideo.SelectedItem == "Auto" && string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase) || mainwindow.tglBatch.IsChecked == true && (string)mainwindow.cboVideo.SelectedItem == "Auto" && string.Equals(MainWindow.batchExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase))
            {
                // Insert Copy if Does Not Contain
                if (!VideoCodecItemSource.Contains("Copy"))
                {
                    VideoCodecItemSource.Insert(0, "Copy");
                }
                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = VideoCodecItemSource;

                mainwindow.cboVideoCodec.SelectedItem = "Copy";

                //debug show list
                //var message = string.Join(Environment.NewLine, VideoCodec);
                //System.Windows.MessageBox.Show(message);
            }


            // Disable Copy if:
            // Input / Output Extensions don't match
            // Batch / Output Extensions don't match
            // Resize is Not No
            // Crop is Not Empty
            // FPS is Not Auto
            // Optimize is Not None
            //
            if (VideoCodecItemSource.Contains("Copy") && !string.IsNullOrEmpty((string)mainwindow.cboVideo.SelectedItem) && !string.Equals(MainWindow.inputExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase) | !string.Equals(MainWindow.batchExt, MainWindow.outputExt, StringComparison.CurrentCultureIgnoreCase))
            {
                // Switch back to format's default codec
                //
                if ((string)mainwindow.cboVideo.SelectedItem != "Auto" || (string)mainwindow.cboSize.SelectedItem != "No" || !string.IsNullOrEmpty(MainWindow.crop) || (string)mainwindow.cboFPS.SelectedItem != "auto" || (string)mainwindow.cboOptimize.SelectedItem != "none")
                {
                    if ((string)mainwindow.cboFormat.SelectedItem == "webm")
                    {
                        mainwindow.cboVideoCodec.SelectedItem = "VP8";
                    }
                    else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                    {
                        mainwindow.cboVideoCodec.SelectedItem = "x264";
                    }
                    else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                    {
                        mainwindow.cboVideoCodec.SelectedItem = "x264"; //ignore mkv, special rules below ??
                    }
                    else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                    {
                        mainwindow.cboVideoCodec.SelectedItem = "Theora";
                    }
                    else if ((string)mainwindow.cboFormat.SelectedItem == "jpg")
                    {
                        mainwindow.cboVideoCodec.SelectedItem = "JPEG";
                    }
                    else if ((string)mainwindow.cboFormat.SelectedItem == "png")
                    {
                        mainwindow.cboVideoCodec.SelectedItem = "PNG";
                    }
                    else if ((string)mainwindow.cboFormat.SelectedItem == "m4a" || (string)mainwindow.cboFormat.SelectedItem == "mp3" || (string)mainwindow.cboFormat.SelectedItem == "ogg" || (string)mainwindow.cboFormat.SelectedItem == "flac" || (string)mainwindow.cboFormat.SelectedItem == "wav")
                    {
                        mainwindow.cboVideoCodec.SelectedItem = string.Empty;
                    }
                }
            }
                
                

            // Special Rules for MKV
            if ((string)mainwindow.cboFormat.SelectedItem == "mkv" && (string)mainwindow.cboVideoCodec.SelectedItem == "Copy" && (string)mainwindow.cboVideo.SelectedItem != "Auto")
            {
                if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                {
                    mainwindow.cboVideoCodec.SelectedItem = "x264";
                }
            }

            //// Always Default to Copy if it exists and Video Dropdown is (Auto) //Causing Problems
            //if (VideoCodecItemSource.Contains("Copy") && (string)mainwindow.cboVideo.SelectedItem == "Auto" && (string)mainwindow.cboFormat.SelectedItem != "mkv" /* ignore if mkv */)
            //{
            //    //videoCodecComboBox.SelectedItem = "Copy";
            //}

        } // End AutoVideoCodecCopy


        /// <summary>
        /// Video Filter (Method)
        /// <summary>
        public static void VideoFilter(MainWindow mainwindow)
        {
            // Initialize the Filter
            // Clear the Filter for each run
            // Anything that pertains to Video must be after the vFilter
            //vFilterSwitch = string.Empty; //do not reset the switch between converts
            vFilter = string.Empty;

            // -------------------------
            // JPEG
            // -------------------------
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
            {
                // Turn on PNG to JPG Filter
                if (string.Equals(MainWindow.inputExt, ".png", StringComparison.CurrentCultureIgnoreCase) || string.Equals(MainWindow.inputExt, "png", StringComparison.CurrentCultureIgnoreCase))
                {
                    vFilterSwitch += 1;

                    geq = "format=yuva444p,geq='if(lte(alpha(X,Y),16),255,p(X,Y))':'if(lte(alpha(X,Y),16),128,p(X,Y))':'if(lte(alpha(X,Y),16),128,p(X,Y))'"; //png transparent to white background
                }
                else
                {
                    geq = string.Empty;
                }
            }
        }


        /// <summary>
        /// Video Bitrate Calculator (Method)
        /// <summary>
        public static void VideoBitrateCalculator(MainWindow mainwindow)
        {
            // set to FFprobe's result
            FFprobe.inputVideoBitrate = FFprobe.ffprobeVideoBitrateResult.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

            // FFprobe values
            if (string.IsNullOrEmpty(FFprobe.inputVideoBitrate))
            {
                // do nothing (dont remove, it will cause substring to overload)

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.paragraph.Inlines.Add(new LineBreak());
                    Log.paragraph.Inlines.Add(new LineBreak());
                    Log.paragraph.Inlines.Add(new Bold(new Run("Input Video Bitrate does not exist or can't be detected")) { Foreground = Log.ConsoleWarning });
                };
                Log.LogActions.Add(Log.WriteAction);
            }
            // Filter out any extra spaces after the first 3 characters IMPORTANT
            else if (FFprobe.inputVideoBitrate.Substring(0, 3) == "N/A")
            {
                FFprobe.inputVideoBitrate = "N/A";
            }

            // If Video has a Bitrate, calculate Bitrate into decimal
            if (FFprobe.inputVideoBitrate != "N/A" && !string.IsNullOrEmpty(FFprobe.inputVideoBitrate)) // NEW
            {
                if (Convert.ToInt32(FFprobe.inputVideoBitrate) >= 1000000000) // e.g. (1000M / 1,000,000K)
                {
                    FFprobe.inputVideoBitrate = Convert.ToString(int.Parse(FFprobe.inputVideoBitrate) * 0.00001) + "k ";
                }
                else if (Convert.ToInt32(FFprobe.inputVideoBitrate) >= 100000000) // e.g. (100M / 100,000K) 
                {
                    FFprobe.inputVideoBitrate = Convert.ToString(int.Parse(FFprobe.inputVideoBitrate) * 0.0001) + "k ";
                }
                else if (Convert.ToInt32(FFprobe.inputVideoBitrate) >= 10000000) // e.g. (10M / 10,000K)
                {
                    FFprobe.inputVideoBitrate = Convert.ToString(int.Parse(FFprobe.inputVideoBitrate) * 0.001) + "k ";
                }
                else if (Convert.ToInt32(FFprobe.inputVideoBitrate) >= 100000) // e.g. (1M /1000K)
                {
                    FFprobe.inputVideoBitrate = Convert.ToString(int.Parse(FFprobe.inputVideoBitrate) * 0.001) + "k ";
                }
                else if (Convert.ToInt32(FFprobe.inputVideoBitrate) >= 10000) // e.g. (100K)
                {
                    FFprobe.inputVideoBitrate = Convert.ToString(int.Parse(FFprobe.inputVideoBitrate) * 0.001) + "k ";
                }
            }

            // WebM Video Bitrate Limiter
            // If input video bitrate is greater than 1.5M, lower the bitrate to 1.5M
            // Error checking the ffprobeVideoBitrateResult when using Batch
            //
            //try
            //{
            //    if (MainWindow.outputExt == ".webm" && Convert.ToInt32(FFprobe.ffprobeVideoBitrateResult) >= 150000)
            //    {
            //        FFprobe.inputVideoBitrate = "1.5M";
            //    }
            //}
            //catch
            //{

            //}


            // If Video Variable = N/A, Calculate Bitate (((Filesize*8)/1000)/Duration)
            // Formats like WebM, MKV and with Missing Metadata can have New Bitrates calculated and applied
            if (FFprobe.inputVideoBitrate == "N/A")
            {
                try // Calculating Bitrate will crash if jpg/png
                {
                    FFprobe.inputVideoBitrate = Convert.ToInt32((double.Parse(FFprobe.inputSize) * 8) / 1000 / double.Parse(FFprobe.inputDuration)).ToString() + "k";
                    // convert to int to remove decimals

                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("Calculating New Bitrate Information...")) { Foreground = Log.ConsoleAction });
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Run("((File Size * 8) / 1000) / File Time Duration") { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }
                catch
                {
                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("Error: Could Not Calculate New Bitrate Information...")) { Foreground = Log.ConsoleError });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }

            }
        }


        /// <summary>
        /// Video Quality (Method)
        /// <summary>
        public static void VideoQuality(MainWindow mainwindow)
        {
            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new Bold(new Run("Quality: ")) { Foreground = Log.ConsoleDefault });
                Log.paragraph.Inlines.Add(new Run(Convert.ToString(mainwindow.cboVideo.SelectedItem)) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            // -------------------------
            // Auto
            // -------------------------
            if ((string)mainwindow.cboVideo.SelectedItem == "Auto")
            {
                // Only Enable 2 Pass if Format is Video
                if ((string)mainwindow.cboFormat.SelectedItem == "webm" || (string)mainwindow.cboFormat.SelectedItem == "mp4" || (string)mainwindow.cboFormat.SelectedItem == "mkv") //exclude ogv for now
                {
                    v2passSwitch = 1;
                    v2passBatchSwitch = 1;
                }
                else
                {
                    v2passSwitch = 0;
                    v2passBatchSwitch = 0;
                    pass1 = string.Empty;
                }

                // Else set the Auto values
                //
                // If Input File has No Video or can't be detected 
                if (FFprobe.inputVideoBitrate == "N/A" && string.IsNullOrEmpty(FFprobe.inputVideoCodec)) // (mp3, m4a, ogg, etc)
                {
                    vBitMode = string.Empty;
                    vQual = string.Empty;
                }

                // If No Bitrate & No Codec
                if (string.IsNullOrEmpty(FFprobe.inputVideoBitrate) && string.IsNullOrEmpty(FFprobe.inputVideoCodec)) // (mp3 converted to a webm)
                {
                    vBitMode = string.Empty;
                    vQual = string.Empty;
                }

                // If Video File has video, but Bitrate CAN'T be detected, but Codec is detected
                if (FFprobe.inputVideoBitrate == "N/A" && !string.IsNullOrEmpty(FFprobe.inputVideoCodec)) // (webm, some mkv's)
                {
                    // Default to a High value
                    // If Theora (Special instructions)
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
                    {
                        crf = "-b:v 0 -crf 16"; //crf value different than x264
                    }
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264")
                    {
                        crf = "-crf 18";
                    }
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                    {
                        crf = "-x265-params crf=23";
                    }
                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
                    {
                        crf = "-q:v 10"; // Theora can't have Auto Value, default to highest -q:v 10
                    }

                    vOptions = "-pix_fmt yuv420p";
                    vBitMode = string.Empty;
                    //combine
                    vQual = crf + " " + vOptions;
                }

                // If Input File has Video and Bitrate was detected
                if (FFprobe.inputVideoBitrate != "N/A" && !string.IsNullOrEmpty(FFprobe.inputVideoBitrate))
                {
                    //MessageBox.Show("Here"); //debug

                    if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8") { vBitrate = "-b:v " + FFprobe.inputVideoBitrate; vOptions = "-pix_fmt yuv420p"; }
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9") { vBitrate = "-b:v " + FFprobe.inputVideoBitrate; vOptions = "-pix_fmt yuv420p"; }
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264") { vBitrate = "-b:v " + FFprobe.inputVideoBitrate; vOptions = "-pix_fmt yuv420p"; }
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265") { vBitrate = "-b:v " + FFprobe.inputVideoBitrate; vOptions = "-pix_fmt yuv420p"; }
                    else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora") { vBitrate = "-q:v 10"; vOptions = "-pix_fmt yuv420p"; } // Theora can't have Auto Value, default to highest -q:v 10

                    //combine
                    vQual = vBitrate + " " + vOptions;
                }

                // -------------------------
                // IMAGE
                // -------------------------
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG")
                {
                    crf = string.Empty;
                    vBitrate = "-qscale:v 2"; //use highest jpeg quality
                    vOptions = string.Empty;

                    vQual = vBitrate;
                }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                {
                    // png is lossless
                    crf = string.Empty; vBitrate = string.Empty; vOptions = string.Empty;

                    vQual = vBitrate;
                }

                //end Auto //////////////////////////
            }
            // -------------------------
            // Lossless
            // -------------------------
            else if ((string)mainwindow.cboVideo.SelectedItem == "Lossless")
            {
                //if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8") { crf = "-b:v 0 -crf 4"; vBitrate = "-b:v 0" /* for 2 pass */; vOptions = "-pix_fmt yuv444p"; } //VP8 cannot be Lossless
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9") { crf = "-lossless 1" /* crf needs b:v 0*/; vBitrate = "-lossless 1" /* for 2 pass */; vOptions = "-pix_fmt yuv444p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264") { crf = "-qp 0"; vBitrate = "-qp 0" /* for 2 pass */; vOptions = "-pix_fmt yuv444p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265") { crf = "-x265-params lossless"; vBitrate = "-x265-params lossless" /* for 2 pass */; vOptions = "-pix_fmt yuv444p"; }
                // Theora can't be Lossless

                // Lossless Switch
                // Encoding Pass Method based on Pass ComboBox Selection
                // Different values than PassesSwitch Method
                //
                // CRF
                if ((string)mainwindow.cboPass.SelectedItem == "CRF")
                {
                    vQual = crf + " " + vOptions; //combine

                    v2passSwitch = 0;
                    v2passBatchSwitch = 0;
                }
                // 1 Pass
                else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass")
                {
                    vQual = crf + " " + vOptions; //combine

                    v2passSwitch = 0;
                    v2passBatchSwitch = 0;
                }
                // 2 Pass
                else if ((string)mainwindow.cboPass.SelectedItem == "2 Pass")
                {
                    vQual = crf + " " + vOptions; //combine

                    v2passSwitch = 0;
                    v2passBatchSwitch = 0;
                }
                // auto
                else if ((string)mainwindow.cboPass.SelectedItem == "auto")
                {
                    vQual = crf + " " + vOptions; //combine

                    v2passSwitch = 0;
                    v2passBatchSwitch = 0;
                }
            }
            // -------------------------
            // Ultra
            // -------------------------
            else if ((string)mainwindow.cboVideo.SelectedItem == "Ultra")
            {
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8") { crf = "-b:v 4M -crf 10" /* crf needs b:v 0*/; vBitrate = "-b:v 5M" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9") { crf = "-b:v 4M -crf 10"/* crf needs b:v 0*/; vBitrate = "-b:v 5M" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264") { crf = "-crf 18"; vBitrate = "-b:v 5M" /* for 2 pass */; vOptions = "-pix_fmt yuv420p -qcomp 0.8"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265") { crf = "-x265-params crf=23"; vBitrate = "-b:v 5M" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora") { crf = "-q:v 10"; vBitrate = "-q:v 10" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; } //OGV uses forced q:v instead of CRF
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG") { crf = string.Empty; vBitrate = "-qscale:v 2"; vOptions = string.Empty; }

                // Encoding Pass Method based on Pass ComboBox Selection
                PassesSwitch(mainwindow);
            }
            // -------------------------
            // High
            // -------------------------
            else if ((string)mainwindow.cboVideo.SelectedItem == "High")
            {
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8") { crf = "-b:v 2M -crf 12" /* crf needs b:v 0*/; vBitrate = "-b:v 2M" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9") { crf = "-b:v 2M -crf 12" /* crf needs b:v 0*/; vBitrate = "-b:v 2M" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264") { crf = "-crf 23"; vBitrate = "-b:v 2M" /* for 2 pass */; vOptions = "-pix_fmt yuv420p -qcomp 0.8"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265") { crf = "-x265-params crf=28"; vBitrate = "-b:v 2M" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora") { crf = "-q:v 8"; vBitrate = "-q:v 8" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; } //OGV uses forced q:v instead of CRF
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG") { crf = string.Empty; vBitrate = "-qscale:v 4"; vOptions = string.Empty; }

                // Encoding Pass Method based on Pass ComboBox Selection
                PassesSwitch(mainwindow);
            }
            // -------------------------
            // Medium
            // -------------------------
            else if ((string)mainwindow.cboVideo.SelectedItem == "Medium")
            {
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8") { crf = "-b:v 1.3M -crf 16" /* crf needs b:v 0*/; vBitrate = "-b:v 1.3M" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9") { crf = "-b:v 1.3M -crf 16" /* crf needs b:v 0*/; vBitrate = "-b:v 1.3M" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264") { crf = "-crf 30"; vBitrate = "-b:v 1.3M" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265") { crf = "-x265-params crf=35"; vBitrate = "-b:v 1.3M" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora") { crf = "-q:v 6"; vBitrate = "-q:v 6" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; } //OGV uses forced q:v instead of CRF
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG") { crf = string.Empty; vBitrate = "-qscale:v 8"; vOptions = string.Empty; }

                // Encoding Pass Method based on Pass ComboBox Selection
                PassesSwitch(mainwindow);
            }
            // -------------------------
            // Low
            // -------------------------
            else if ((string)mainwindow.cboVideo.SelectedItem == "Low")
            {
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8") { crf = "-b:v 600K -crf 20" /* crf needs b:v 0*/; vBitrate = "-b:v 600K" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9") { crf = "-b:v 600K -crf 20" /* crf needs b:v 0*/; vBitrate = "-b:v 600K" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264") { crf = "-crf 37"; vBitrate = "-b:v 600K" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265") { crf = "-x265-params crf=42"; vBitrate = "-b:v 600K" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora") { crf = "-q:v 4"; vBitrate = "-q:v 4" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; } //OGV uses forced q:v instead of CRF
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG") { crf = string.Empty; vBitrate = "-qscale:v 15"; vOptions = string.Empty; }

                // Encoding Pass Method based on Pass ComboBox Selection
                PassesSwitch(mainwindow);
            }
            // -------------------------
            // Sub
            // -------------------------
            else if ((string)mainwindow.cboVideo.SelectedItem == "Sub")
            {
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8") { crf = "-b:v 250K -crf 25" /* crf needs b:v 0*/; vBitrate = "-b:v 250K" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9") { crf = "-b:v 250K -crf 25" /* crf needs b:v 0*/; vBitrate = "-b:v 250K" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264") { crf = "-crf 45"; vBitrate = "-b:v 250K" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265") { crf = "-x265-aQualparams crf=50"; vBitrate = "-b:v 250K" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora") { crf = "-q:v 2"; vBitrate = "-q:v 2" /* for 2 pass */; vOptions = "-pix_fmt yuv420p"; } //OGV uses forced q:v instead of CRF
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG") { crf = string.Empty; vBitrate = "-qscale:v 25"; vOptions = string.Empty; }

                // Encoding Pass Method based on Pass ComboBox Selection
                PassesSwitch(mainwindow);
            }

            // -------------------------
            // Custom
            // -------------------------
            if ((string)mainwindow.cboVideo.SelectedItem == "Custom")
            {
                // VBITRATE
                // if vBitrate Textbox is default or empty
                if (mainwindow.vBitrateCustom.Text == "Bitrate" || string.IsNullOrWhiteSpace(mainwindow.vBitrateCustom.Text)) { vBitMode = string.Empty; vBitrate = string.Empty; }
                // if vBitrate is entered by user and is not blank
                if (mainwindow.vBitrateCustom.Text != "Bitrate" && !string.IsNullOrWhiteSpace(mainwindow.vBitrateCustom.Text)) { vBitMode = "-b:v"; vBitrate = mainwindow.vBitrateCustom.Text; }

                //CRF
                // if CRF texbox is default or empty
                if (mainwindow.crfCustom.Text == "CRF" || string.IsNullOrWhiteSpace(mainwindow.crfCustom.Text)) { crf = string.Empty; }
                // if CRF texbox entered by user and is not blank
                if (mainwindow.crfCustom.Text != "CRF" && !string.IsNullOrWhiteSpace(mainwindow.crfCustom.Text)) { crf = "-crf" + " " + mainwindow.crfCustom.Text /* crf needs b:v 0*/ ; }

                // VP9 crf -b:v 0
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
                {
                    // If vBitrate is default or blank & CRF is custom value
                    if (mainwindow.vBitrateCustom.Text == "Bitrate" | string.IsNullOrWhiteSpace(mainwindow.vBitrateCustom.Text) && mainwindow.crfCustom.Text != "CRF" && !string.IsNullOrWhiteSpace(mainwindow.crfCustom.Text))
                    {
                        vBitMode = "-b:v";
                        vBitrate = "0";
                    }
                }

                // -------------------------
                // Enable 2 Pass (If Bitrate Custom & CRF Empty)
                // -------------------------
                // If vBitrate TextBox is NOT Empty & NOT Default ("Bitrate") & CRF IS Empty or Default ("CRF")
                if ((string)mainwindow.cboPass.SelectedItem == "2 Pass")
                {
                    if (!string.IsNullOrWhiteSpace(mainwindow.vBitrateCustom.Text) && mainwindow.vBitrateCustom.Text != "Bitrate" && string.IsNullOrWhiteSpace(mainwindow.crfCustom.Text) | mainwindow.crfCustom.Text == "CRF")
                    {
                        // Log Console Message /////////
                        Log.WriteAction = () =>
                        {
                            Log.paragraph.Inlines.Add(new LineBreak());
                            Log.paragraph.Inlines.Add(new LineBreak());
                            Log.paragraph.Inlines.Add(new Bold(new Run("2 Pass Toggle: ")) { Foreground = Log.ConsoleDefault });
                            Log.paragraph.Inlines.Add(new Run("On, ") { Foreground = Log.ConsoleDefault });
                            Log.paragraph.Inlines.Add(new Bold(new Run("CRF: ")) { Foreground = Log.ConsoleDefault });
                            Log.paragraph.Inlines.Add(new Run("None, Using Bitrate 2 Pass") { Foreground = Log.ConsoleDefault });
                        };
                        Log.LogActions.Add(Log.WriteAction);

                        // 2 Pass Switch
                        v2passSwitch = 1;
                        v2passBatchSwitch = 1;
                    }
                }

                // Disabled on CRF so Bitrate can run as 1 Pass
                if ((string)mainwindow.cboPass.SelectedItem == "1 Pass")
                {
                    if (!string.IsNullOrWhiteSpace(mainwindow.vBitrateCustom.Text) && mainwindow.vBitrateCustom.Text != "Bitrate" && string.IsNullOrWhiteSpace(mainwindow.crfCustom.Text) | mainwindow.crfCustom.Text == "CRF")
                    {
                        // Log Console Message /////////
                        Log.WriteAction = () =>
                        {
                            Log.paragraph.Inlines.Add(new LineBreak());
                            Log.paragraph.Inlines.Add(new LineBreak());
                            Log.paragraph.Inlines.Add(new Bold(new Run("2 Pass Toggle: ")) { Foreground = Log.ConsoleDefault });
                            Log.paragraph.Inlines.Add(new Run("Off, ") { Foreground = Log.ConsoleDefault });
                            Log.paragraph.Inlines.Add(new Bold(new Run("CRF: ")) { Foreground = Log.ConsoleDefault });
                            Log.paragraph.Inlines.Add(new Run("None, Using Bitrate 1 Pass") { Foreground = Log.ConsoleDefault });
                        };
                        Log.LogActions.Add(Log.WriteAction);

                        // 2 Pass Switch
                        v2passSwitch = 0;
                        v2passBatchSwitch = 0;
                    }
                }

                //CODEC
                // user entered CRF textbox
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8") { vOptions = "-pix_fmt yuv420p"; } //used to have -qmin 0 -qmax 50
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP9") { vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264") { vOptions = "-pix_fmt yuv420p"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x265") { crf = "-x265-params crf=" + mainwindow.crfCustom.Text; vOptions = "-pix_fmt yuv420p"; }
                // Theora cant have Custom yet

                //Combine
                vQual = vBitMode + " " + vBitrate + " " + crf + " " + vMaxrate + " " + vOptions;
            }
            // -------------------------
            // None
            // -------------------------
            else if ((string)mainwindow.cboVideo.SelectedItem == "None")
            {
                vQual = string.Empty;
            }


            // No Video Codec
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "None")
            {
                vCodec = string.Empty;
                Streams.vMap = "-vn";
                vBitMode = string.Empty;
                vBitrate = string.Empty;
                vMaxrate = string.Empty;
                vQual = string.Empty;
            }

            // Video Codec Copy
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "Copy")
            {
                vCodec = "-vcodec copy";
                vBitMode = string.Empty;
                vBitrate = string.Empty;
                vMaxrate = string.Empty;
                vQual = string.Empty;
            }


            // Log Console Message /////////        
            Log.WriteAction = () =>
            {
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new Bold(new Run("Bitrate: ")) { Foreground = Log.ConsoleDefault });
                if (!string.IsNullOrEmpty(vBitrate))
                {
                    Log.paragraph.Inlines.Add(new Run(vBitrate.Replace("-b:v ", "")) { Foreground = Log.ConsoleDefault });
                }
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new Bold(new Run("CRF: ")) { Foreground = Log.ConsoleDefault });
                if (!string.IsNullOrEmpty(crf))
                {
                    Log.paragraph.Inlines.Add(new Run(crf) { Foreground = Log.ConsoleDefault }); //crf combines with bitrate
                }
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new Bold(new Run("Options: ")) { Foreground = Log.ConsoleDefault });
                Log.paragraph.Inlines.Add(new Run(vOptions) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);
        }


        /// <summary>
        /// Resize (Method)
        /// <summary>
        public static void Resize(MainWindow mainwindow)
        {
            // No
            //
            if ((string)mainwindow.cboSize.SelectedItem == "No")
            {
                // Video Filter Switch
                //vFilterSwitch = 0; //do not use

                aspect = string.Empty;

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.paragraph.Inlines.Add(new LineBreak());
                    Log.paragraph.Inlines.Add(new Bold(new Run("Resize: ")) { Foreground = Log.ConsoleDefault });
                    Log.paragraph.Inlines.Add(new Run("No") { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            // FFmpeg MP4 / MKV Width/Height Fix
            //
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" && (string)mainwindow.cboSize.SelectedItem == "No" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265" && (string)mainwindow.cboSize.SelectedItem == "No")
            {
                // Video Filter Switch
                vFilterSwitch += 1;

                scale = "scale=";
                width = "-2";
                height = "-2";
                //combine
                aspect = scale + "\"" + width + ":" + height + "\"";
            }

            // -------------------------
            // 8K
            // -------------------------
            if ((string)mainwindow.cboSize.SelectedItem == "8K")
            {
                // Video Filter Switch
                vFilterSwitch += 1;

                scale = "scale=";
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9" || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora" || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG" || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                { height = "-1"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                { height = "-2"; }
                width = "7680"; // Note: 8K is measured width first

                aspect = scale + width + ":" + height;
            }
            // -------------------------
            // 4K
            // -------------------------
            else if ((string)mainwindow.cboSize.SelectedItem == "4K")
            {
                // Video Filter Switch
                vFilterSwitch += 1;

                scale = "scale=";
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9" || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora" || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG" || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                { height = "-1"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                { height = "-2"; }
                width = "4096"; // Note: 4K is measured width first

                aspect = scale + width + ":" + height;
            }
            // -------------------------
            // 4K UHD
            // -------------------------
            else if ((string)mainwindow.cboSize.SelectedItem == "4K UHD")
            {
                // Video Filter Switch
                vFilterSwitch += 1;

                scale = "scale=";
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9" || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora" || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG" || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                { height = "-1"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                { height = "-2"; }
                width = "3840"; // Note: 4K is measured width first

                aspect = scale + width + ":" + height;
            }
            // -------------------------
            // 2K
            // -------------------------
            else if ((string)mainwindow.cboSize.SelectedItem == "2K")
            {
                // Video Filter Switch
                vFilterSwitch += 1;

                scale = "scale=";
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9" || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora" || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG" || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                { height = "-1"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                { height = "-2"; }
                width = "2048"; // Note: 2K is measured width first

                aspect = scale + width + ":" + height;
            }
            // -------------------------
            // 1440p
            // -------------------------
            else if ((string)mainwindow.cboSize.SelectedItem == "1440p")
            {
                // Video Filter Switch
                vFilterSwitch += 1;

                scale = "scale=";
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9" || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora" || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG" || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                { width = "-1"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                { width = "-2"; }
                height = "1440";

                aspect = scale + width + ":" + height;
            }
            // -------------------------
            // 1200p
            // -------------------------
            else if ((string)mainwindow.cboSize.SelectedItem == "1200p")
            {
                // Video Filter Switch
                vFilterSwitch += 1;

                scale = "scale=";
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9" || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora" || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG" || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                { width = "-1"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                { width = "-2"; }
                height = "1200";

                aspect = scale + width + ":" + height;
            }
            // -------------------------
            // 1080p
            // -------------------------
            else if ((string)mainwindow.cboSize.SelectedItem == "1080p")
            {
                // Video Filter Switch
                vFilterSwitch += 1;

                scale = "scale=";
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9" || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora" || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG" || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                { width = "-1"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                { width = "-2"; }
                height = "1080";

                aspect = scale + width + ":" + height;
            }
            // -------------------------
            // 720p
            // -------------------------
            else if ((string)mainwindow.cboSize.SelectedItem == "720p")
            {
                // Video Filter Switch
                vFilterSwitch += 1;

                //System.Windows.MessageBox.Show(vFilter); //debug
                scale = "scale=";
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9" || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora" || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG" || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                { width = "-1"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                { width = "-2"; }
                height = "720";

                aspect = scale + width + ":" + height;
            }
            // -------------------------
            // 480p
            // -------------------------
            else if ((string)mainwindow.cboSize.SelectedItem == "480p")
            {
                // Video Filter Switch
                vFilterSwitch += 1;

                scale = "scale=";
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9" || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora" || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG" || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                { width = "-1"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                { width = "-2"; }
                height = "480";

                aspect = scale + width + ":" + height;
            }
            // -------------------------
            // 320p
            // -------------------------
            else if ((string)mainwindow.cboSize.SelectedItem == "320p")
            {
                // Video Filter Switch
                vFilterSwitch += 1;

                scale = "scale=";
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9" || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora" || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG" || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                { width = "-1"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                { width = "-2"; }
                height = "320";

                aspect = scale + width + ":" + height;
            }
            // -------------------------
            // 240p
            // -------------------------
            else if ((string)mainwindow.cboSize.SelectedItem == "240p")
            {
                // Video Filter Switch
                vFilterSwitch += 1;

                scale = "scale=";
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9" || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora" || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG" || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                { width = "-1"; }
                else if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                { width = "-2"; }
                height = "240";

                aspect = scale + width + ":" + height;
            }
            // -------------------------
            // Custom Size
            // -------------------------
            else if ((string)mainwindow.cboSize.SelectedItem == "Custom")
            {
                // Change the left over Default "width" and "height" text to "auto"
                if (string.Equals(mainwindow.widthCustom.Text, "width", StringComparison.CurrentCultureIgnoreCase))
                {
                    mainwindow.widthCustom.Foreground = new SolidColorBrush(Colors.White);
                    mainwindow.widthCustom.Text = "auto";
                }

                if (string.Equals(mainwindow.heightCustom.Text, "height", StringComparison.CurrentCultureIgnoreCase))
                {
                    mainwindow.heightCustom.Foreground = new SolidColorBrush(Colors.White);
                    mainwindow.heightCustom.Text = "auto";
                }

                // -------------------------
                // VP8, VP9, Theora
                // -------------------------
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9" || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora" || (string)mainwindow.cboVideoCodec.SelectedItem == "JPEG" || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
                {
                    // Get width height from custom textbox
                    width = mainwindow.widthCustom.Text;
                    height = mainwindow.heightCustom.Text;

                    // If User enters "auto" or textbox has default "width" or "height"
                    if (string.Equals(mainwindow.widthCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                    {
                        width = "-1";
                    }
                    if (string.Equals(mainwindow.heightCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                    {
                        height = "-1";
                    }

                    // Video Filter Switch
                    Video.vFilterSwitch += 1;

                    //set values
                    scale = "scale=";
                    //combine
                    aspect = scale + "\"" + width + ":" + height + "\"";
                }

                // -------------------------
                // x264 & x265
                // -------------------------
                // Fix FFmpeg MP4 but (User entered value)
                // Apply Fix to all scale effects above
                if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
                {

                    width = mainwindow.widthCustom.Text;
                    height = mainwindow.heightCustom.Text;

                    // If width = auto & height = custom value
                    if (string.Equals(mainwindow.widthCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase) && !string.Equals(mainwindow.heightCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                    {
                        // Auto the width (-2), Make user entered height divisible by 2
                        width = "-2";

                        try
                        {
                            // If not divisible by 2, subtract 1 from total
                            int divisibleHeight = Convert.ToInt32(height);

                            if (divisibleHeight % 2 != 0)
                            {
                                divisibleHeight -= 1;
                                height = Convert.ToString(divisibleHeight);
                            }
                        }
                        catch
                        {
                            // Log Console Message /////////
                            Log.WriteAction = () =>
                            {
                                Log.paragraph.Inlines.Add(new LineBreak());
                                Log.paragraph.Inlines.Add(new LineBreak());
                                Log.paragraph.Inlines.Add(new Bold(new Run("Warning: Must enter numbers only.")) { Foreground = Log.ConsoleWarning });
                            };
                            Log.LogActions.Add(Log.WriteAction);

                            System.Windows.MessageBox.Show("Must enter numbers only.");
                            /* lock */
                            MainWindow.ready = 0;
                        }

                        //crop = string.Empty; //divisibleCrop

                        // If crop is null, force Empty
                        if (string.IsNullOrEmpty(MainWindow.crop)) //null check
                        {
                            MainWindow.crop = string.Empty;
                            //crop = null;
                        }
                    }

                    // If width = custom value & height = auto
                    //
                    else if (!string.Equals(mainwindow.widthCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase) && string.Equals(mainwindow.heightCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                    {
                        // Auto the height (-2), Make user entered width divisible by 2
                        height = "-2";

                        try
                        {
                            // If not divisible by 2, subtract 1 from total
                            int divisibleWidth = Convert.ToInt32(width);

                            if (divisibleWidth % 2 != 0)
                            {
                                divisibleWidth -= 1;
                                width = Convert.ToString(divisibleWidth);
                            }
                        }
                        catch
                        {
                            // Log Console Message /////////
                            Log.WriteAction = () =>
                            {
                                Log.paragraph.Inlines.Add(new LineBreak());
                                Log.paragraph.Inlines.Add(new LineBreak());
                                Log.paragraph.Inlines.Add(new Bold(new Run("Warning: Must enter numbers only.")) { Foreground = Log.ConsoleWarning });
                            };
                            Log.LogActions.Add(Log.WriteAction);

                            System.Windows.MessageBox.Show("Must enter numbers only.");
                            /* lock */
                            MainWindow.ready = 0;
                        }

                        //crop = string.Empty; //divisibleCrop

                        // If crop is null, force Empty
                        if (string.IsNullOrEmpty(MainWindow.crop)) //null check
                        {
                            MainWindow.crop = string.Empty;
                            //crop = null;
                        }
                    }

                    // If both width & height are custom value
                    //
                    else if (!string.Equals(mainwindow.widthCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase) && !string.Equals(mainwindow.heightCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                    {
                        try
                        {
                            // If not divisible by 2, subtract 1 from total
                            int divisibleWidth = Convert.ToInt32(width);
                            int divisibleHeight = Convert.ToInt32(height);

                            if (divisibleWidth % 2 != 0)
                            {
                                divisibleWidth -= 1;
                                width = Convert.ToString(divisibleWidth);
                            }
                            if (divisibleHeight % 2 != 0)
                            {
                                divisibleHeight -= 1;
                                height = Convert.ToString(divisibleHeight);
                            }

                            // If crop string is empty use the mp4 divisible crop 
                            if (string.IsNullOrEmpty(MainWindow.crop))
                            {
                                MainWindow.crop = "crop=" + width + ":" + height + ":0:0";
                                //divisibleCrop //Now in vFilter Switch Combine Section
                            }
                        }
                        catch
                        {
                            // Log Console Message /////////
                            Log.WriteAction = () =>
                            {
                                Log.paragraph.Inlines.Add(new LineBreak());
                                Log.paragraph.Inlines.Add(new LineBreak());
                                Log.paragraph.Inlines.Add(new Bold(new Run("Warning: Must enter numbers only.")) { Foreground = Log.ConsoleWarning });
                            };
                            Log.LogActions.Add(Log.WriteAction);

                            System.Windows.MessageBox.Show("Must enter numbers only.");
                            /* lock */
                            MainWindow.ready = 0;
                        }

                    }

                    // Video Filter Switch
                    Video.vFilterSwitch = 2; //always combine (greater than 1)

                    //set values
                    scale = "scale=";

                    //combine
                    aspect = scale + width + ":" + height;

                    //System.Windows.MessageBox.Show(crop); //debug

                } //end x264 & x265


                // REMOVE ASPECT IF Blank
                //
                // Remove "auto" and empty values - no scaling
                if (string.IsNullOrWhiteSpace(mainwindow.widthCustom.Text) && string.IsNullOrWhiteSpace(mainwindow.heightCustom.Text))
                {
                    scale = string.Empty;
                    MainWindow.crop = string.Empty; //divisibleCrop
                    width = string.Empty;
                    height = string.Empty;
                    aspect = string.Empty;
                }
                // if width =  auto & height = auto
                else if (string.Equals(mainwindow.widthCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase) && string.Equals(mainwindow.heightCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                {
                    scale = string.Empty;
                    MainWindow.crop = string.Empty; //divisibleCrop
                    width = string.Empty;
                    height = string.Empty;
                    aspect = string.Empty;
                }
                // if width =  blank & height = auto
                else if (string.IsNullOrWhiteSpace(mainwindow.widthCustom.Text) && string.Equals(mainwindow.heightCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase))
                {
                    scale = string.Empty;
                    MainWindow.crop = string.Empty; //divisibleCrop
                    width = string.Empty;
                    height = string.Empty;
                    aspect = string.Empty;
                }
                // if width =  auto & height = blank
                else if (string.Equals(mainwindow.widthCustom.Text, "auto", StringComparison.CurrentCultureIgnoreCase) && string.IsNullOrWhiteSpace(mainwindow.heightCustom.Text))
                {
                    scale = string.Empty;
                    MainWindow.crop = string.Empty; //divisibleCrop
                    width = string.Empty;
                    height = string.Empty;
                    aspect = string.Empty;
                }

            } //End Resize

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new Bold(new Run("Width: ")) { Foreground = Log.ConsoleDefault });
                Log.paragraph.Inlines.Add(new Run(width) { Foreground = Log.ConsoleDefault });
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new Bold(new Run("Height: ")) { Foreground = Log.ConsoleDefault });
                Log.paragraph.Inlines.Add(new Run(height) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            // Log Console Message /////////
            //console.rtbLog.AppendText("\n\nAspect: " + aspect);
            // Log Console Message /////////
            //try
            //{
            //    console.rtbLog.AppendText("\n\nCrop = " + cropwindow.cropWidth.Text + ":" + cropwindow.cropHeight.Text + ":" + cropwindow.cropX.Text + ":" + cropwindow.cropY.Text);
            //}
            //catch
            //{

            //}
        }


        /// <summary>
        /// Crop (Method)
        /// <summary>
        public static void Crop(MainWindow mainwindow, CropWindow cropwindow) //method
        {
            //CropWindow cropwindow;

            //System.Windows.MessageBox.Show("MainWindow: " + crop); //debug
            //System.Windows.MessageBox.Show("CropWindow: " + cropwindow.crop); //debug
            if (!string.IsNullOrEmpty(CropWindow.crop)) //null check to avoid overwriting mp4 custom crop with null
            {
                MainWindow.crop = CropWindow.crop; //get crop values from CropWindow
            }

            // Clear crop if MediaType is Audio
            if ((string)mainwindow.cboMediaType.SelectedItem == "Audio")
            {
                MainWindow.crop = string.Empty;
            }

            // Enable Video Filter Switch if Not Empty
            if (!string.IsNullOrEmpty(MainWindow.crop))
            {
                // Video Filter Switch
                vFilterSwitch += 1;
            }

            // Crop Codec Copy Check
            // Switch Copy to Codec to avoid error
            if (!string.IsNullOrEmpty(MainWindow.crop) && (string)mainwindow.cboVideoCodec.SelectedItem == "Copy") //null check
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.paragraph.Inlines.Add(new LineBreak());
                    Log.paragraph.Inlines.Add(new LineBreak());
                    Log.paragraph.Inlines.Add(new Bold(new Run("Warning: Crop cannot use Codec Copy. Please select a Video Codec.")) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);

                System.Windows.MessageBox.Show("Crop cannot use Codec Copy. Please select a Video Codec."); /* lock */ MainWindow.ready = 0;
            }
        }


        /// <summary>
        /// FPS (Method)
        /// <summary>
        public static void FPS(MainWindow mainwindow)
        {
            if ((string)mainwindow.cboFPS.SelectedItem == "auto")
            {
                fps = string.Empty;
            }
            else
            {
                fps = "-r " + mainwindow.cboFPS.Text;
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new Bold(new Run("FPS: ")) { Foreground = Log.ConsoleDefault });
                Log.paragraph.Inlines.Add(new Run(mainwindow.cboFPS.Text.ToString()) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);
        }


        /// <summary>
        /// Images (Method)
        /// <summary>
        public static void Images(MainWindow mainwindow)
        {
            if ((string)mainwindow.cboMediaType.SelectedItem == "Image")
            {
                options = "-vframes 1"; //important
            }
            if ((string)mainwindow.cboMediaType.SelectedItem == "Sequence")
            {
                options = string.Empty; //disable -vframes
            }
        }


        /// <summary>
        /// Speed (Method)
        /// <summary>
        public static void Speed(MainWindow mainwindow)
        {
            // -------------------------
            // x264 / x265
            // -------------------------
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
            {
                if ((string)mainwindow.cboSpeed.SelectedItem == "Placebo") { speed = "-preset placebo"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Very Slow") { speed = "-preset veryslow"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Slower") { speed = "-preset slower"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Slow") { speed = "-preset slow"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Medium") { speed = "-preset medium"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Fast") { speed = "-preset fast"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Faster") { speed = "-preset faster"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Very Fast") { speed = "-preset veryfast"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Super Fast") { speed = "-preset superfast"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Ultra Fast") { speed = "-preset ultrafast"; }

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.paragraph.Inlines.Add(new LineBreak());
                    Log.paragraph.Inlines.Add(new Bold(new Run("Encoding Speed: ")) { Foreground = Log.ConsoleDefault });
                    Log.paragraph.Inlines.Add(new Run(mainwindow.cboSpeed.Text.ToString()) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            // -------------------------
            // VP8 / VP9
            // -------------------------
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
            {
                if ((string)mainwindow.cboSpeed.SelectedItem == "Placebo") { speed = "-quality best -cpu-used 0"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Very Slow") { speed = "-quality good -cpu-used 0"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Slower") { speed = "-quality good -cpu-used 0"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Slow") { speed = "-quality good -cpu-used 0"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Medium") { speed = "-quality good -cpu-used 0"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Fast") { speed = "-quality good -cpu-used 1"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Faster") { speed = "-quality good -cpu-used 2"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Very Fast") { speed = "-quality realtime -cpu-used 3"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Super Fast") { speed = "-quality realtime -cpu-used 4"; }
                else if ((string)mainwindow.cboSpeed.SelectedItem == "Ultra Fast") { speed = "-quality realtime -cpu-used 5"; }

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.paragraph.Inlines.Add(new LineBreak());
                    Log.paragraph.Inlines.Add(new Bold(new Run("Encoding Speed: ")) { Foreground = Log.ConsoleDefault });
                    Log.paragraph.Inlines.Add(new Run(mainwindow.cboSpeed.Text.ToString()) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            // -------------------------
            // Theora
            // -------------------------
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
            {
                speed = string.Empty;

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.paragraph.Inlines.Add(new LineBreak());
                    Log.paragraph.Inlines.Add(new Bold(new Run("Encoding Speed: ")) { Foreground = Log.ConsoleDefault });
                    Log.paragraph.Inlines.Add(new Run("N/A") { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }

            // -------------------------
            // JPEG & PNG
            // -------------------------
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "JPEG" || (string)mainwindow.cboVideoCodec.SelectedItem == "PNG")
            {
                speed = string.Empty;

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.paragraph.Inlines.Add(new LineBreak());
                    Log.paragraph.Inlines.Add(new Bold(new Run("Encoding Speed: ")) { Foreground = Log.ConsoleDefault });
                    Log.paragraph.Inlines.Add(new Run("N/A") { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }
            // -------------------------
            // JPEG
            // -------------------------


            // -------------------------
            // None (No Codec)
            // -------------------------
            if (string.IsNullOrEmpty((string)mainwindow.cboVideoCodec.SelectedItem))
            {
                speed = string.Empty;

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.paragraph.Inlines.Add(new LineBreak());
                    Log.paragraph.Inlines.Add(new Bold(new Run("Encoding Speed: ")) { Foreground = Log.ConsoleDefault });
                    Log.paragraph.Inlines.Add(new Run("N/A") { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);
            }
        } // End Speed


        /// <summary>
        /// Optimize (Method)
        /// <summary>
        public static void Optimize(MainWindow mainwindow)
        {
            // -------------------------
            // None
            // -------------------------
            // Default to blank
            if ((string)mainwindow.cboOptimize.SelectedItem == "None")
            {
                optimize = string.Empty;
            }

            // -------------------------
            // VP8, VP9, Theora
            // -------------------------
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9" || (string)mainwindow.cboVideoCodec.SelectedItem == "Theora")
            {
                // Web
                if ((string)mainwindow.cboOptimize.SelectedItem == "Web")
                {
                    optimize = "-movflags faststart";
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
                    optimize = "-profile:v baseline -level 3.0 -movflags +faststart ";
                }
                // DVD
                else if ((string)mainwindow.cboOptimize.SelectedItem == "DVD")
                {
                    optimize = "-profile:v baseline -level 3.0 -maxrate 9.6M";
                }
                // HD Video
                else if ((string)mainwindow.cboOptimize.SelectedItem == "HD Video")
                {
                    optimize = "-profile:v main -level 4.0";
                }
                // Animation
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Animation")
                {
                    optimize = "-profile:v main -level 4.0";
                }
                // Blu-ray
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Blu-ray")
                {
                    optimize = "-deblock 0:0 -sar 1/1 -x264opts bluray-compat=1:level=4.1:open-gop=1:slices=4:tff=1:colorprim=bt709:colormatrix=bt709:vbv-maxrate=40000:vbv-bufsize=30000:me=umh:ref=4:nal-hrd=vbr:aud=1:b-pyramid=strict";
                }
                // Windows Device
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Windows")
                {
                    optimize = "-profile:v baseline -level 3.1 -movflags faststart";
                }
                // Apple Device
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Apple")
                {
                    optimize = "-x264-params ref=4 -profile:v baseline -level 3.1 -movflags +faststart";
                }
                // Android Device
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Android")
                {
                    optimize = "-profile:v baseline -level 3.0 -movflags faststart";
                }
                // PS3
                else if ((string)mainwindow.cboOptimize.SelectedItem == "PS3")
                {
                    optimize = "-profile:v main -level 4.0 -vpre normal";
                }
                // PS4
                else if ((string)mainwindow.cboOptimize.SelectedItem == "PS4")
                {
                    optimize = "-profile:v main -level 4.1";
                }
                // Xbox 360
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Xbox 360")
                {
                    optimize = "-profile:v high -level 4.1 -maxrate 9.8M";
                }
                // Xbox One
                else if ((string)mainwindow.cboOptimize.SelectedItem == "Xbox 360")
                {
                    optimize = "-profile:v high -level 4.1";
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
                    optimize = "-movflags +faststart";
                }
            }

            // -------------------------
            // Advanced (x264 & x265)
            // -------------------------
            if ((string)mainwindow.cboOptimize.SelectedItem == "Advanced" && (string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
            {
                // Tune
                //
                if (MainWindow.OptAdvTune == "none" || string.IsNullOrEmpty(MainWindow.OptAdvTune))
                {
                    tune = string.Empty;
                }
                else
                {
                    // Tune = Set Tmp Setting from Optimized Advanced Window
                    tune = "-tune " + MainWindow.OptAdvTune;
                }


                // Profile
                //
                if (MainWindow.OptAdvProfile == "none" || string.IsNullOrEmpty(MainWindow.OptAdvProfile))
                {
                    optimizeProfile = string.Empty;
                }
                else
                {
                    // Tune = Set Tmp Setting from Optimized Advanced Window
                    optimizeProfile = "-profile:v " + MainWindow.OptAdvProfile;
                }

                // Level
                //
                if (MainWindow.OptAdvLevel == "none" || string.IsNullOrEmpty(MainWindow.OptAdvLevel))
                {
                    optimizeLevel = string.Empty;
                }
                else
                {
                    // Tune = Set Tmp Setting from Optimized Advanced Window
                    optimizeLevel = "-level " + MainWindow.OptAdvLevel;
                }

                // Combine Optimize = Profile + Level
                //
                List<string> v2passList = new List<string>() { optimizeProfile, optimizeLevel };
                optimize = string.Join(" ", v2passList.Where(s => !string.IsNullOrEmpty(s)));

            }

        } // End Optimize


        /// <summary>
        // Frame Rate To Decimal (Method)
        /// <summary>
        // When using Video Frame Range instead of Time
        public static void FramesToDecimal(MainWindow mainwindow) //method
        {
            // FFprobeInputFrameRate() Method is called in Convert Button
            // FFprobeInputFrameRate();

            //MessageBox.Show(inputFramerate); //debug

            // Separate FFprobe Result (e.g. 30000/1001)
            string[] f = FFprobe.inputFramerate.Split('/');


            try
            {
                double detectedFramerate = Convert.ToDouble(f[0]) / Convert.ToDouble(f[1]); // divide FFprobe values
                detectedFramerate = Math.Truncate(detectedFramerate * 1000) / 1000; // limit to 3 decimal places

                // Trim Start Frame
                //
                if (mainwindow.frameStart.Text != "Frame" && !string.IsNullOrWhiteSpace(mainwindow.frameStart.Text) && !string.IsNullOrWhiteSpace(mainwindow.frameStart.Text)) // Default/Null Check
                {
                    Format.trimStart = Convert.ToString(Convert.ToDouble(mainwindow.frameStart.Text) / detectedFramerate); // Divide Frame Start Number by Video's Framerate
                }

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.paragraph.Inlines.Add(new LineBreak());
                    Log.paragraph.Inlines.Add(new Bold(new Run("Start Frame: ")) { Foreground = Log.ConsoleDefault });
                    Log.paragraph.Inlines.Add(new Run(mainwindow.frameStart.Text + " / " + detectedFramerate + " = " + Format.trimStart) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);

                // Trim End Frame
                //
                if (mainwindow.frameEnd.Text != "Range" && !string.IsNullOrWhiteSpace(mainwindow.frameEnd.Text) && !string.IsNullOrWhiteSpace(mainwindow.frameEnd.Text)) // Default/Null Check
                {
                    Format.trimEnd = Convert.ToString(Convert.ToDouble(mainwindow.frameEnd.Text) / detectedFramerate); ; // Divide Frame End Number by Video's Framerate
                }

                // Log Console Message /////////
                if (mainwindow.frameEnd.IsEnabled == true)
                {
                    Log.WriteAction = () =>
                    {
                        Log.paragraph.Inlines.Add(new LineBreak());
                        Log.paragraph.Inlines.Add(new Bold(new Run("End Frame: ")) { Foreground = Log.ConsoleDefault });
                        Log.paragraph.Inlines.Add(new Run(mainwindow.frameEnd.Text + " / " + detectedFramerate + " = " + Format.trimEnd) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }

            }
            catch
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.paragraph.Inlines.Add(new LineBreak());
                    Log.paragraph.Inlines.Add(new LineBreak());
                    Log.paragraph.Inlines.Add(new Bold(new Run("Warning: No input file or Framerate not detected.")) { Foreground = Log.ConsoleWarning });
                };
                Log.LogActions.Add(Log.WriteAction);

                System.Windows.MessageBox.Show("No input file or Framerate not detected.");
                /* lock */
                MainWindow.ready = 0;
            }

        } // End Frame Rate To Decimal


        /// <summary>
        /// Video Filter Combine (Method)
        /// <summary>
        public static void VideoFilterCombine(MainWindow mainwindow)
        {
            // -------------------------
            // vFilter Switch   (On, Combine, Off, Empty)
            // -------------------------
            // If -vf already on, ready to combine multiple filters

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.paragraph.Inlines.Add(new LineBreak());
                Log.paragraph.Inlines.Add(new Bold(new Run("Filter: ")) { Foreground = Log.ConsoleDefault });
                Log.paragraph.Inlines.Add(new Run(vFilterSwitch.ToString()) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            // Use Single Filter
            if (vFilterSwitch == 1)
            {
                vFilter = "-vf " + aspect + MainWindow.crop + geq; //either aspect, crop, or geq will be enabled
            }
            // Combine Multiple Filters
            else if (vFilterSwitch > 1)
            {
                // Add Filters to List
                if (!string.IsNullOrEmpty(aspect)) { VideoFilters.Add(aspect); }
                if (!string.IsNullOrEmpty(MainWindow.crop)) { VideoFilters.Add(MainWindow.crop); }
                if (!string.IsNullOrEmpty(geq)) { VideoFilters.Add(geq); }

                // Join List with Comma, Remove Empty Strings
                vFilter = "-vf \"" + string.Join(", ", VideoFilters.Where(s => !string.IsNullOrEmpty(s))) + "\"";

            }
            else if (vFilterSwitch == 0)
            {
                vFilter = string.Empty;
            }
            // No Filters
            else if (vFilterSwitch == null)
            {
                vFilterSwitch = 0;
                vFilter = string.Empty;
            }

            // Remove vFilter if Video Codec is Empty
            if (string.IsNullOrEmpty((string)mainwindow.cboVideoCodec.SelectedItem))
            {
                vFilterSwitch = 0;
                vFilter = string.Empty;
            }
        }


        /// <summary>
        ///     Passes Switch
        ///     
        ///     Encoding Pass Method based on Pass ComboBox Selection
        /// </summary>
        public static void PassesSwitch(MainWindow mainwindow)
        {
            // CRF (Use -crf)
            if ((string)mainwindow.cboPass.SelectedItem == "CRF")
            {
                vBitrate = string.Empty; //clear -b:v 2 pass
                vQual = crf + " " + vOptions; //combine

                v2passSwitch = 0;
                v2passBatchSwitch = 0;
            }
            // 1 Pass Toggle On (Use Bitrate -b:v)
            else if ((string)mainwindow.cboPass.SelectedItem == "1 Pass")
            {
                crf = string.Empty; //clear crf
                vQual = vBitrate + " " + vOptions; //combine

                v2passSwitch = 0;
                v2passBatchSwitch = 0;
            }
            // 2 Pass Toggle On (Use Bitrate -b:v)
            else if ((string)mainwindow.cboPass.SelectedItem == "2 Pass")
            {
                crf = string.Empty; //clear crf
                vQual = vBitrate + " " + vOptions; //combine

                v2passSwitch = 1;
                v2passBatchSwitch = 1;
            }
            // auto
            else if ((string)mainwindow.cboPass.SelectedItem == "auto")
            {
                vQual = vBitrate + " " + vOptions; //combine

                v2passSwitch = 0;
                v2passBatchSwitch = 0;
            }
        }
    }
}