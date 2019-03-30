/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2019 Matt McManis
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axiom
{
    public class VP9
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public static List<ViewModel.VideoCodec> codec = new List<ViewModel.VideoCodec>()
        {
             new ViewModel.VideoCodec()
             {
                 Codec = "libvpx-vp9",
                 Parameters = "-tile-columns 6 -frame-parallel 1 -auto-alt-ref 1 -lag-in-frames 25"
             }
        };

        public static void Codec_Set(ViewModel vm)
        {
            // Combine Codec + Parameters
            List<string> codec = new List<string>()
            {
                "-c:v",
                VP9.codec.FirstOrDefault()?.Codec,
                VP9.codec.FirstOrDefault()?.Parameters
            };

            vm.Video_Codec = string.Join(" ", codec.Where(s => !string.IsNullOrEmpty(s)));
        }



        // ---------------------------------------------------------------------------
        // Items Source
        // ---------------------------------------------------------------------------
        // -------------------------
        // Encode Speed
        // -------------------------
        public static List<ViewModel.VideoEncodeSpeed> encodeSpeed = new List<ViewModel.VideoEncodeSpeed>()
        {
             new ViewModel.VideoEncodeSpeed() { Name = "None",       Command = ""},
             new ViewModel.VideoEncodeSpeed() { Name = "Placebo",    Command = "-speed -8" },
             new ViewModel.VideoEncodeSpeed() { Name = "Very Slow",  Command = "-speed -4" },
             new ViewModel.VideoEncodeSpeed() { Name = "Slower",     Command = "-speed -2" },
             new ViewModel.VideoEncodeSpeed() { Name = "Slow",       Command = "-speed 0" },
             new ViewModel.VideoEncodeSpeed() { Name = "Medium",     Command = "-speed 1" },
             new ViewModel.VideoEncodeSpeed() { Name = "Fast",       Command = "-speed 2" },
             new ViewModel.VideoEncodeSpeed() { Name = "Faster",     Command = "-speed 3" },
             new ViewModel.VideoEncodeSpeed() { Name = "Very Fast",  Command = "-speed 4" },
             new ViewModel.VideoEncodeSpeed() { Name = "Super Fast", Command = "-speed 5" },
             new ViewModel.VideoEncodeSpeed() { Name = "Ultra Fast", Command = "-speed 6" }
        };

        // -------------------------
        // Pixel Format
        // -------------------------
        public static List<string> pixelFormat = new List<string>()
        {
            "auto",
            "yuv420p",
            "yuva420p",
            "yuv422p",
            "yuv440p",
            "yuv444p",
            "yuv420p10le",
            "yuv422p10le",
            "yuv440p10le",
            "yuv444p10le",
            "yuv420p12le",
            "yuv422p12le",
            "yuv440p12le",
            "yuv444p12le",
            "gbrp",
            "gbrp10le",
            "gbrp12le"
        };

        // -------------------------
        // Quality
        // -------------------------
        public static List<ViewModel.VideoQuality> quality = new List<ViewModel.VideoQuality>()
        {
             new ViewModel.VideoQuality() { Name = "Auto",     CRF = "",   Video_CRF_BitRate = "",      CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      MinRate = "", MaxRate = "", BufSize ="", NA = "2000K" },
             new ViewModel.VideoQuality() { Name = "Lossless", CRF = "",   Video_CRF_BitRate = "",      CBR_BitMode = "",     CBR = "",      VBR_BitMode = "-q:v", VBR = "",      MinRate = "", MaxRate = "", BufSize ="", Lossless = "-lossless 1" },
             new ViewModel.VideoQuality() { Name = "Ultra",    CRF = "10", Video_CRF_BitRate = "4000K", CBR_BitMode = "-b:v", CBR = "4000K", VBR_BitMode = "-q:v", VBR = "4000K", MinRate = "", MaxRate = "", BufSize ="" },
             new ViewModel.VideoQuality() { Name = "High",     CRF = "12", Video_CRF_BitRate = "2000K", CBR_BitMode = "-b:v", CBR = "2000K", VBR_BitMode = "-q:v", VBR = "2000K", MinRate = "", MaxRate = "", BufSize ="" },
             new ViewModel.VideoQuality() { Name = "Medium",   CRF = "16", Video_CRF_BitRate = "1300K", CBR_BitMode = "-b:v", CBR = "1300K", VBR_BitMode = "-q:v", VBR = "1300K", MinRate = "", MaxRate = "", BufSize ="" },
             new ViewModel.VideoQuality() { Name = "Low",      CRF = "20", Video_CRF_BitRate = "600K",  CBR_BitMode = "-b:v", CBR = "600K",  VBR_BitMode = "-q:v", VBR = "600K",  MinRate = "", MaxRate = "", BufSize ="" },
             new ViewModel.VideoQuality() { Name = "Sub",      CRF = "25", Video_CRF_BitRate = "250K",  CBR_BitMode = "-b:v", CBR = "250K",  VBR_BitMode = "-q:v", VBR = "250K",  MinRate = "", MaxRate = "", BufSize ="" },
             new ViewModel.VideoQuality() { Name = "Custom",   CRF = "",   Video_CRF_BitRate = "",      CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      MinRate = "", MaxRate = "", BufSize ="" }
        };

        // -------------------------
        // Pass
        // -------------------------
        public static void EncodingPass(ViewModel vm)
        {
            // -------------------------
            // Quality
            // -------------------------
            // Auto
            if (vm.Video_Quality_SelectedItem == "Auto")
            {
                vm.Video_Pass_Items = new List<string>()
                {
                    "2 Pass"
                };

                vm.Video_Pass_SelectedItem = "2 Pass";
                vm.Video_Pass_IsEnabled = false;
                VideoControls.passUserSelected = false;

                vm.Video_CRF_IsEnabled = false;
            }
            // Lossless
            else if (vm.Video_Quality_SelectedItem == "Lossless")
            {
                vm.Video_Pass_Items = new List<string>()
                {
                    "1 Pass",
                    "2 Pass"
                };

                vm.Video_Pass_SelectedItem = "2 Pass";
                vm.Video_Pass_IsEnabled = true;
                vm.Video_CRF_IsEnabled = false;
            }
            // Custom
            else if (vm.Video_Quality_SelectedItem == "Custom")
            {
                vm.Video_Pass_Items = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                vm.Video_Pass_IsEnabled = true;
                vm.Video_CRF_IsEnabled = true;
            }
            // None
            else if (vm.Video_Quality_SelectedItem == "None")
            {
                vm.Video_Pass_Items = new List<string>()
                {
                    "auto"
                };

                vm.Video_Pass_IsEnabled = false;
                vm.Video_CRF_IsEnabled = false;
            }
            // Presets: Ultra, High, Medium, Low, Sub
            else
            {
                vm.Video_Pass_Items = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                vm.Video_Pass_IsEnabled = true;
                vm.Video_CRF_IsEnabled = false;

                // Default to CRF
                if (VideoControls.passUserSelected == false)
                {
                    vm.Video_Pass_SelectedItem = "CRF";
                    VideoControls.passUserSelected = true;
                }
            }

            // Clear TextBoxes
            if (vm.Video_Quality_SelectedItem == "Auto" ||
                vm.Video_Quality_SelectedItem == "Lossless" ||
                vm.Video_Quality_SelectedItem == "Custom" ||
                vm.Video_Quality_SelectedItem == "None"
                )
            {
                vm.Video_CRF_Text = string.Empty;
                vm.Video_BitRate_Text = string.Empty;
                vm.Video_MinRate_Text = string.Empty;
                vm.Video_MaxRate_Text = string.Empty;
                vm.Video_BufSize_Text = string.Empty;
            }

        }

        // -------------------------
        // Optimize
        // -------------------------
        public static List<ViewModel.VideoOptimize> optimize = new List<ViewModel.VideoOptimize>()
        {
            new ViewModel.VideoOptimize() { Name = "None", Tune = "none", Profile = "none", Level = "none", Command = "" },
            new ViewModel.VideoOptimize() { Name = "Web",  Tune = "none", Profile = "none", Level = "none", Command = "-movflags faststart" }
        };

        // -------------------------
        // Tune
        // -------------------------
        public static List<string> tune = new List<string>()
        {
            "none"
        };

        // -------------------------
        // Profile
        // -------------------------
        public static List<string> profile = new List<string>()
        {
            "none"
        };

        // -------------------------
        // Level
        // -------------------------
        public static List<string> level = new List<string>()
        {
            "none"
        };



        // ---------------------------------------------------------------------------
        // Controls Behavior
        // ---------------------------------------------------------------------------

        // -------------------------
        // Items Source
        // -------------------------
        public static void Controls_ItemsSource(ViewModel vm)
        {
            // Encode Speed
            vm.Video_EncodeSpeed_Items = encodeSpeed;

            // Pixel Format
            vm.Video_PixelFormat_Items = pixelFormat;

            // Pass
            //vm.Video_Pass_Items = pass;
            EncodingPass(vm);

            // Video Quality
            vm.Video_Quality_Items = quality;

            // Optimize
            vm.Video_Optimize_Items = optimize;
            // Tune
            vm.Optimize_Tune_Items = tune;
            // Profile
            vm.Optimize_Profile_Items = profile;
            // Level
            vm.Video_Optimize_Level_Items = level;
        }

        // -------------------------
        // Selected Items
        // -------------------------
        public static void Controls_Selected(ViewModel vm)
        {

            // Pixel Format
            vm.Video_PixelFormat_SelectedItem = "yuv420p";

            // Framerate
            vm.Video_FPS_SelectedItem = "auto";
        }

        // -------------------------
        // Checked
        // -------------------------
        public static void Controls_Checked(ViewModel vm)
        {
            // None
        }

        // -------------------------
        // Unchecked
        // -------------------------
        public static void Controls_Unhecked(ViewModel vm)
        {
            // BitRate Mode
            vm.Video_VBR_IsChecked = false;
        }

        // -------------------------
        // Enabled
        // -------------------------
        public static void Controls_Enable(ViewModel vm)
        {
            // Video Encode Speed
            vm.Video_EncodeSpeed_IsEnabled = true;

            // Video Codec
            vm.Video_Codec_IsEnabled = true;

            // Video Quality
            vm.Video_Quality_IsEnabled = true;

            // Video VBR
            vm.Video_VBR_IsEnabled = true;

            // Pixel Format
            vm.Video_PixelFormat_IsEnabled = true;

            // FPS ComboBox
            vm.Video_FPS_IsEnabled = true;

            // Optimize ComboBox
            vm.Video_Optimize_IsEnabled = true;

            // Scaling ComboBox
            vm.Video_ScalingAlgorithm_IsEnabled = true;

            // Crop
            vm.Video_Crop_IsEnabled = true;
        }

        // -------------------------
        // Disabled
        // -------------------------
        public static void Controls_Disable(ViewModel vm)
        {
            // Subtitle Codec
            vm.Subtitle_Codec_IsEnabled = false;

            // Subtitle Stream
            vm.Subtitle_Stream_IsEnabled = false;
        }
    }
}
