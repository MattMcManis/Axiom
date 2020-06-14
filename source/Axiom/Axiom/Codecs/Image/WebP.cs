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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Controls.Image.Codec
{
    public class WebP
    {
        // ---------------------------------------------------------------------------
        // Arguments
        // ---------------------------------------------------------------------------

        // -------------------------
        // Codec
        // -------------------------
        public static ObservableCollection<ViewModel.Video.VideoCodec> codec = new ObservableCollection<ViewModel.Video.VideoCodec>()
        {
                new ViewModel.Video.VideoCodec()
                {
                    Codec = "libwebp",
                    Parameters = ""
                }
        };

        public static void Codec_Set()
        {
            // Combine Codec + Parameters
            List<string> codec = new List<string>()
            {
                "-c:v",
                WebP.codec.FirstOrDefault()?.Codec,
                WebP.codec.FirstOrDefault()?.Parameters
            };

            VM.VideoView.Video_Codec = string.Join(" ", codec.Where(s => !string.IsNullOrEmpty(s)));
        }



        // ---------------------------------------------------------------------------
        // Items Source
        // ---------------------------------------------------------------------------

        // -------------------------
        // Encode Speed
        // -------------------------
        public static ObservableCollection<ViewModel.Video.VideoEncodeSpeed> encodeSpeed = new ObservableCollection<ViewModel.Video.VideoEncodeSpeed>()
        {
                new ViewModel.Video.VideoEncodeSpeed() { Name = "none", Command = ""},
        };

        // -------------------------
        // Pixel Format
        // -------------------------
        public static ObservableCollection<string> pixelFormat = new ObservableCollection<string>()
        {
            "auto",
            "bgra",
            "yuv420p",
            "yuva420p"
        };

        // -------------------------
        // Quality
        // -------------------------
        public static ObservableCollection<ViewModel.Video.VideoQuality> quality = new ObservableCollection<ViewModel.Video.VideoQuality>()
        {
                new ViewModel.Video.VideoQuality() { Name = "Auto",     CRF = "", CBR_BitMode = "-q:v", CBR = "85",  VBR_BitMode = "-q:v", VBR = "85",  MinRate = "", MaxRate = "", BufSize ="", NA = "85" },
                new ViewModel.Video.VideoQuality() { Name = "Lossless", CRF = "", CBR_BitMode = "",     CBR = "",    VBR_BitMode = "",     VBR = "",    MinRate = "", MaxRate = "", BufSize ="", Lossless = "-lossless 1" },
                new ViewModel.Video.VideoQuality() { Name = "Ultra",    CRF = "", CBR_BitMode = "-q:v", CBR = "100", VBR_BitMode = "-q:v", VBR = "100", MinRate = "", MaxRate = "", BufSize ="" },
                new ViewModel.Video.VideoQuality() { Name = "High",     CRF = "", CBR_BitMode = "-q:v", CBR = "85",  VBR_BitMode = "-q:v", VBR = "85",  MinRate = "", MaxRate = "", BufSize ="" },
                new ViewModel.Video.VideoQuality() { Name = "Medium",   CRF = "", CBR_BitMode = "-q:v", CBR = "60",  VBR_BitMode = "-q:v", VBR = "60",  MinRate = "", MaxRate = "", BufSize ="" },
                new ViewModel.Video.VideoQuality() { Name = "Low",      CRF = "", CBR_BitMode = "-q:v", CBR = "45",  VBR_BitMode = "-q:v", VBR = "45",  MinRate = "", MaxRate = "", BufSize ="" },
                new ViewModel.Video.VideoQuality() { Name = "Sub",      CRF = "", CBR_BitMode = "-q:v", CBR = "25",  VBR_BitMode = "-q:v", VBR = "25",  MinRate = "", MaxRate = "", BufSize ="" },
                new ViewModel.Video.VideoQuality() { Name = "Custom",   CRF = "", CBR_BitMode = "-q:v", CBR = "",    VBR_BitMode = "-q:v", VBR = "",    MinRate = "", MaxRate = "", BufSize ="" }
        };

        // -------------------------
        // Pass
        // -------------------------
        public static void EncodingPass()
        {
            // -------------------------
            // Quality
            // -------------------------
            switch (VM.VideoView.Video_Quality_SelectedItem)
            {
                // Auto
                case "Auto":
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "1 Pass"
                    };

                    VM.VideoView.Video_Pass_SelectedItem = "1 Pass";
                    VM.VideoView.Video_Pass_IsEnabled = false;
                    Video.Controls.passUserSelected = false;

                    VM.VideoView.Video_CRF_IsEnabled = false;
                    break;

                // Lossless
                case "Lossless":
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "1 Pass"
                    };

                    VM.VideoView.Video_Pass_SelectedItem = "1 Pass";
                    VM.VideoView.Video_Pass_IsEnabled = false;
                    VM.VideoView.Video_CRF_IsEnabled = false;
                    break;

                // Custom
                case "Custom":
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "1 Pass"
                    };

                    VM.VideoView.Video_Pass_SelectedItem = "1 Pass";
                    VM.VideoView.Video_Pass_IsEnabled = false;
                    VM.VideoView.Video_CRF_IsEnabled = false;
                    break;

                // Presets: Ultra, High, Medium, Low, Sub
                default:
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "1 Pass"
                    };

                    VM.VideoView.Video_Pass_SelectedItem = "1 Pass";
                    VM.VideoView.Video_Pass_IsEnabled = false;
                    VM.VideoView.Video_CRF_IsEnabled = false;
                    break;
            }

            // Clear TextBoxes
            if (VM.VideoView.Video_Quality_SelectedItem == "Auto" ||
                VM.VideoView.Video_Quality_SelectedItem == "Lossless" ||
                VM.VideoView.Video_Quality_SelectedItem == "Custom" ||
                VM.VideoView.Video_Quality_SelectedItem == "None"
                )
            {
                VM.VideoView.Video_CRF_Text = string.Empty;
                VM.VideoView.Video_BitRate_Text = string.Empty;
                VM.VideoView.Video_MinRate_Text = string.Empty;
                VM.VideoView.Video_MaxRate_Text = string.Empty;
                VM.VideoView.Video_BufSize_Text = string.Empty;
            }

            //// -------------------------
            //// Quality
            //// -------------------------
            //// Auto
            //if (VM.VideoView.Video_Quality_SelectedItem == "Auto")
            //{
            //    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
            //    {
            //        "1 Pass"
            //    };

            //    VM.VideoView.Video_Pass_SelectedItem = "1 Pass";
            //    VM.VideoView.Video_Pass_IsEnabled = false;
            //    Video.Controls.passUserSelected = false;

            //    VM.VideoView.Video_CRF_IsEnabled = false;
            //}
            //// Lossless
            //else if (VM.VideoView.Video_Quality_SelectedItem == "Lossless")
            //{
            //    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
            //    {
            //        "1 Pass"
            //    };

            //    VM.VideoView.Video_Pass_IsEnabled = false;
            //    VM.VideoView.Video_CRF_IsEnabled = false;
            //}
            //// Custom
            //else if (VM.VideoView.Video_Quality_SelectedItem == "Custom")
            //{
            //    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
            //    {
            //        "1 Pass"
            //    };

            //    VM.VideoView.Video_Pass_IsEnabled = false;
            //    VM.VideoView.Video_CRF_IsEnabled = true;
            //}
            //// None
            //else if (VM.VideoView.Video_Quality_SelectedItem == "None")
            //{
            //    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
            //    {
            //        "auto"
            //    };

            //    VM.VideoView.Video_Pass_IsEnabled = false;
            //    VM.VideoView.Video_CRF_IsEnabled = false;
            //}
            //// Presets: Ultra, High, Medium, Low, Sub
            //else
            //{
            //    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
            //    {
            //        "1 Pass"
            //    };

            //    VM.VideoView.Video_Pass_IsEnabled = false;
            //    VM.VideoView.Video_CRF_IsEnabled = false;

            //    // Default to 1 Pass
            //    if (Video.Controls.passUserSelected == false)
            //    {
            //        VM.VideoView.Video_Pass_SelectedItem = "1 Pass";
            //        Video.Controls.passUserSelected = true;
            //    }
            //}

            //// Clear TextBoxes
            //if (VM.VideoView.Video_Quality_SelectedItem == "Auto" ||
            //    VM.VideoView.Video_Quality_SelectedItem == "Lossless" ||
            //    VM.VideoView.Video_Quality_SelectedItem == "Custom" ||
            //    VM.VideoView.Video_Quality_SelectedItem == "None"
            //    )
            //{
            //    VM.VideoView.Video_CRF_Text = string.Empty;
            //    VM.VideoView.Video_BitRate_Text = string.Empty;
            //    VM.VideoView.Video_MinRate_Text = string.Empty;
            //    VM.VideoView.Video_MaxRate_Text = string.Empty;
            //    VM.VideoView.Video_BufSize_Text = string.Empty;
            //}

        }

        // -------------------------
        // Optimize
        // -------------------------
        public static ObservableCollection<ViewModel.Video.VideoOptimize> optimize = new ObservableCollection<ViewModel.Video.VideoOptimize>()
        {
            new ViewModel.Video.VideoOptimize() { Name = "None", Tune = "none", Profile = "none", Level = "none", Command = "" },
        };

        // -------------------------
        // Tune
        // -------------------------
        public static ObservableCollection<string> tune = new ObservableCollection<string>()
        {
            "none"
        };

        // -------------------------
        // Profile
        // -------------------------
        public static ObservableCollection<string> profile = new ObservableCollection<string>()
        {
            "none"
        };

        // -------------------------
        // Level
        // -------------------------
        public static ObservableCollection<string> level = new ObservableCollection<string>()
        {
            "none"
        };



        // ---------------------------------------------------------------------------
        // Controls Behavior
        // ---------------------------------------------------------------------------

        // -------------------------
        // Items Source
        // -------------------------
        public static void Controls_ItemsSource()
        {
            // Encode Speed
            VM.VideoView.Video_EncodeSpeed_Items = encodeSpeed;

            // Pixel Format
            VM.VideoView.Video_PixelFormat_Items = pixelFormat;

            // Pass
            //VM.VideoView.Video_Pass_Items = pass;
            EncodingPass();

            // Video Quality
            VM.VideoView.Video_Quality_Items = quality;

            // Optimize
            VM.VideoView.Video_Optimize_Items = optimize;
            // Tune
            VM.VideoView.Video_Optimize_Tune_Items = tune;
            // Profile
            VM.VideoView.Video_Optimize_Profile_Items = profile;
            // Level
            VM.VideoView.Video_Optimize_Level_Items = level;
        }

        // -------------------------
        // Selected Items
        // -------------------------
        public static void Controls_Selected()
        {
            // Pixel Format
            VM.VideoView.Video_PixelFormat_SelectedItem = "yuva420p";

            // Framerate
            VM.VideoView.Video_FPS_SelectedItem = "auto";

            // Channel
            VM.AudioView.Audio_Channel_SelectedItem = "none";
        }


        // -------------------------
        // Expanded
        // -------------------------
        public static void Controls_Expanded()
        {
            // None
        }

        // -------------------------
        // Collapsed
        // -------------------------
        public static void Controls_Collapsed()
        {
            VM.VideoView.Video_Optimize_IsExpanded = false;
        }


        // -------------------------
        // Checked
        // -------------------------
        public static void Controls_Checked()
        {
            // BitRate Mode
            VM.VideoView.Video_VBR_IsChecked = true;
        }

        // -------------------------
        // Unchecked
        // -------------------------
        public static void Controls_Unhecked()
        {
            // None
        }


        // -------------------------
        // Enabled
        // -------------------------
        public static void Controls_Enable()
        {
            // Video Codec
            VM.VideoView.Video_Codec_IsEnabled = true;

            // Video Quality
            VM.VideoView.Video_Quality_IsEnabled = true;

            // Pixel Format
            VM.VideoView.Video_PixelFormat_IsEnabled = true;

            // FPS ComboBox
            VM.VideoView.Video_FPS_IsEnabled = true;

            // Scaling ComboBox
            VM.VideoView.Video_ScalingAlgorithm_IsEnabled = true;

            // Crop
            VM.VideoView.Video_Crop_IsEnabled = true;

            // Subtitle Codec
            VM.SubtitleView.Subtitle_Codec_IsEnabled = true;

            // Subtitle Stream
            VM.SubtitleView.Subtitle_Stream_IsEnabled = true;


            // Filters
            Filters.Video.VideoFilters_EnableAll();
        }

        // -------------------------
        // Disabled
        // -------------------------
        public static void Controls_Disable()
        {
            // Video Encode Speed
            VM.VideoView.Video_EncodeSpeed_IsEnabled = false;

            // Video VBR
            VM.VideoView.Video_VBR_IsEnabled = false;

            // Optimize ComboBox
            VM.VideoView.Video_Optimize_IsEnabled = false;
        }
    }
}
