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
    public class VP8
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public static List<VideoView.VideoCodec> codec = new List<VideoView.VideoCodec>()
        {
             new VideoView.VideoCodec()
             {
                 Codec = "libvpx",
                 Parameters = ""
             }
        };

        public static void Codec_Set()
        {
            // Combine Codec + Parameters
            List<string> codec = new List<string>()
            {
                "-c:v",
                VP8.codec.FirstOrDefault()?.Codec,
                VP8.codec.FirstOrDefault()?.Parameters,
            };

            VideoView.vm.Video_Codec = string.Join(" ", codec.Where(s => !string.IsNullOrEmpty(s)));
        }



        // ---------------------------------------------------------------------------
        // Items Source
        // ---------------------------------------------------------------------------

        // -------------------------
        // Encode Speed
        // -------------------------
        public static List<VideoView.VideoEncodeSpeed> encodeSpeed = new List<VideoView.VideoEncodeSpeed>()
        {
             new VideoView.VideoEncodeSpeed() { Name = "none",       Command = ""},
             new VideoView.VideoEncodeSpeed() { Name = "Placebo",    Command = "-quality best -cpu-used 0",  Command_2Pass = "-quality best -cpu-used 0" },
             new VideoView.VideoEncodeSpeed() { Name = "Very Slow",  Command = "-quality good -cpu-used 0",  Command_2Pass = "-quality best -cpu-used 0" },
             new VideoView.VideoEncodeSpeed() { Name = "Slower",     Command = "-quality good -cpu-used 0",  Command_2Pass = "-quality best -cpu-used 0" },
             new VideoView.VideoEncodeSpeed() { Name = "Slow",       Command = "-quality good -cpu-used 0",  Command_2Pass = "-quality best -cpu-used 0" },
             new VideoView.VideoEncodeSpeed() { Name = "Medium",     Command = "-quality good -cpu-used 0",  Command_2Pass = "-quality best -cpu-used 0" },
             new VideoView.VideoEncodeSpeed() { Name = "Fast",       Command = "-quality good -cpu-used 1",  Command_2Pass = "-quality best -cpu-used 1" },
             new VideoView.VideoEncodeSpeed() { Name = "Faster",     Command = "-quality good -cpu-used 2",  Command_2Pass = "-quality best -cpu-used 2" },
             new VideoView.VideoEncodeSpeed() { Name = "Very Fast",  Command = "-quality good -cpu-used 3",  Command_2Pass = "-quality best -cpu-used 3" },
             new VideoView.VideoEncodeSpeed() { Name = "Super Fast", Command = "-quality good -cpu-used 4",  Command_2Pass = "-quality best -cpu-used 4" },
             new VideoView.VideoEncodeSpeed() { Name = "Ultra Fast", Command = "-quality good -cpu-used 5",  Command_2Pass = "-quality best -cpu-used 5" },
        };

        // -------------------------
        // Pixel Format
        // -------------------------
        public static List<string> pixelFormat = new List<string>()
        {
            "auto",
            "yuv420p",
            "yuva420p"
        };

        // -------------------------
        // Quality
        // -------------------------
        public static List<VideoView.VideoQuality> quality = new List<VideoView.VideoQuality>()
        {
             new VideoView.VideoQuality() { Name = "Auto",   CRF = "",   Video_CRF_BitRate = "",      CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      MinRate = "", MaxRate = "", BufSize ="", NA = "1500K" },
             new VideoView.VideoQuality() { Name = "Ultra",  CRF = "10", Video_CRF_BitRate = "4000K", CBR_BitMode = "-b:v", CBR = "4000K", VBR_BitMode = "-q:v", VBR = "4000K", MinRate = "", MaxRate = "", BufSize ="" },
             new VideoView.VideoQuality() { Name = "High",   CRF = "12", Video_CRF_BitRate = "2000K", CBR_BitMode = "-b:v", CBR = "2000K", VBR_BitMode = "-q:v", VBR = "2000K", MinRate = "", MaxRate = "", BufSize ="" },
             new VideoView.VideoQuality() { Name = "Medium", CRF = "16", Video_CRF_BitRate = "1300K", CBR_BitMode = "-b:v", CBR = "1300K", VBR_BitMode = "-q:v", VBR = "1300K", MinRate = "", MaxRate = "", BufSize ="" },
             new VideoView.VideoQuality() { Name = "Low",    CRF = "20", Video_CRF_BitRate = "600K",  CBR_BitMode = "-b:v", CBR = "600K",  VBR_BitMode = "-q:v", VBR = "600K",  MinRate = "", MaxRate = "", BufSize ="" },
             new VideoView.VideoQuality() { Name = "Sub",    CRF = "25", Video_CRF_BitRate = "250K",  CBR_BitMode = "-b:v", CBR = "250K",  VBR_BitMode = "-q:v", VBR = "250K",  MinRate = "", MaxRate = "", BufSize ="" },
             new VideoView.VideoQuality() { Name = "Custom", CRF = "",   Video_CRF_BitRate = "",      CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      MinRate = "", MaxRate = "", BufSize ="" }
        };

        // -------------------------
        // Pass
        // -------------------------
        public static void EncodingPass()
        {
            // -------------------------
            // Quality
            // -------------------------
            // Auto
            if (VideoView.vm.Video_Quality_SelectedItem == "Auto")
            {
                VideoView.vm.Video_Pass_Items = new List<string>()
                {
                    "2 Pass"
                };

                VideoView.vm.Video_Pass_SelectedItem = "2 Pass";
                VideoView.vm.Video_Pass_IsEnabled = false;
                VideoControls.passUserSelected = false;

                VideoView.vm.Video_CRF_IsEnabled = false;
            }
            // Custom
            else if (VideoView.vm.Video_Quality_SelectedItem == "Custom")
            {
                VideoView.vm.Video_Pass_Items = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                VideoView.vm.Video_Pass_IsEnabled = true;
                VideoView.vm.Video_CRF_IsEnabled = true;
            }
            // None
            else if (VideoView.vm.Video_Quality_SelectedItem == "None")
            {
                VideoView.vm.Video_Pass_Items = new List<string>()
                {
                    "auto"
                };

                VideoView.vm.Video_Pass_IsEnabled = false;
                VideoView.vm.Video_CRF_IsEnabled = false;
            }
            // Presets: Ultra, High, Medium, Low, Sub
            else
            {
                VideoView.vm.Video_Pass_Items = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                VideoView.vm.Video_Pass_IsEnabled = true;
                VideoView.vm.Video_CRF_IsEnabled = false;

                // Default to CRF
                if (VideoControls.passUserSelected == false)
                {
                    VideoView.vm.Video_Pass_SelectedItem = "CRF";
                    VideoControls.passUserSelected = true;
                }
            }

            // Clear TextBoxes
            if (VideoView.vm.Video_Quality_SelectedItem == "Auto" ||
                VideoView.vm.Video_Quality_SelectedItem == "Lossless" ||
                VideoView.vm.Video_Quality_SelectedItem == "Custom" ||
                VideoView.vm.Video_Quality_SelectedItem == "None"
                )
            {
                VideoView.vm.Video_CRF_Text = string.Empty;
                VideoView.vm.Video_BitRate_Text = string.Empty;
                VideoView.vm.Video_MinRate_Text = string.Empty;
                VideoView.vm.Video_MaxRate_Text = string.Empty;
                VideoView.vm.Video_BufSize_Text = string.Empty;
            }

        }

        // -------------------------
        // Optimize
        // -------------------------
        public static List<VideoView.VideoOptimize> optimize = new List<VideoView.VideoOptimize>()
        {
            new VideoView.VideoOptimize() { Name = "None", Tune = "none", Profile = "none", Level = "none", Command = "" },
            new VideoView.VideoOptimize() { Name = "Web",  Tune = "none", Profile = "none", Level = "none", Command = "-movflags faststart" }
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
        public static void Controls_ItemsSource()
        {
            // Encode Speed
            VideoView.vm.Video_EncodeSpeed_Items = encodeSpeed;

            // Pixel Format
            VideoView.vm.Video_PixelFormat_Items = pixelFormat;

            // Pass
            //VideoView.vm.Video_Pass_Items = pass;
            EncodingPass();

            // Video Quality
            VideoView.vm.Video_Quality_Items = quality;

            // Optimize
            VideoView.vm.Video_Optimize_Items = optimize;
            // Tune
            VideoView.vm.Video_Optimize_Tune_Items = tune;
            // Profile
            VideoView.vm.Video_Optimize_Profile_Items = profile;
            // Level
            VideoView.vm.Video_Optimize_Level_Items = level;
        }

        // -------------------------
        // Selected Items
        // -------------------------
        public static void Controls_Selected()
        {

            // Pixel Format
            VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";

            // Framerate
            VideoView.vm.Video_FPS_SelectedItem = "auto";
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
            VideoView.vm.Video_Optimize_IsExpanded = false;
        }


        // -------------------------
        // Checked
        // -------------------------
        public static void Controls_Checked()
        {
            // None
        }

        // -------------------------
        // Unchecked
        // -------------------------
        public static void Controls_Unhecked()
        {
            // BitRate Mode
            VideoView.vm.Video_VBR_IsChecked = false;
        }


        // -------------------------
        // Enabled
        // -------------------------
        public static void Controls_Enable()
        {
            // Video Encode Speed
            VideoView.vm.Video_EncodeSpeed_IsEnabled = true;

            // Video Codec
            VideoView.vm.Video_Codec_IsEnabled = true;

            // Video Quality
            VideoView.vm.Video_Quality_IsEnabled = true;

            // Video VBR
            VideoView.vm.Video_VBR_IsEnabled = true;

            // Pixel Format
            VideoView.vm.Video_PixelFormat_IsEnabled = true;

            // FPS ComboBox
            VideoView.vm.Video_FPS_IsEnabled = true;

            // Optimize ComboBox
            VideoView.vm.Video_Optimize_IsEnabled = true;

            // Scaling ComboBox
            VideoView.vm.Video_ScalingAlgorithm_IsEnabled = true;

            // Crop
            VideoView.vm.Video_Crop_IsEnabled = true;
        }

        // -------------------------
        // Disabled
        // -------------------------
        public static void Controls_Disable()
        {
            // Subtitle Codec
            SubtitleView.vm.Subtitle_Codec_IsEnabled = false;

            // Subtitle Stream
            SubtitleView.vm.Subtitle_Stream_IsEnabled = false;
        }

    }
}
