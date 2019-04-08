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
        public static List<VideoView.VideoCodec> codec = new List<VideoView.VideoCodec>()
        {
             new VideoView.VideoCodec()
             {
                 Codec = "libvpx-vp9",
                 Parameters = "-tile-columns 6 -frame-parallel 1 -auto-alt-ref 1 -lag-in-frames 25"
             }
        };

        public static void Codec_Set()
        {
            // Combine Codec + Parameters
            List<string> codec = new List<string>()
            {
                "-c:v",
                VP9.codec.FirstOrDefault()?.Codec,
                VP9.codec.FirstOrDefault()?.Parameters
            };

            VM.VideoView.Video_Codec = string.Join(" ", codec.Where(s => !string.IsNullOrEmpty(s)));
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
             new VideoView.VideoEncodeSpeed() { Name = "Placebo",    Command = "-speed -8" },
             new VideoView.VideoEncodeSpeed() { Name = "Very Slow",  Command = "-speed -4" },
             new VideoView.VideoEncodeSpeed() { Name = "Slower",     Command = "-speed -2" },
             new VideoView.VideoEncodeSpeed() { Name = "Slow",       Command = "-speed 0" },
             new VideoView.VideoEncodeSpeed() { Name = "Medium",     Command = "-speed 1" },
             new VideoView.VideoEncodeSpeed() { Name = "Fast",       Command = "-speed 2" },
             new VideoView.VideoEncodeSpeed() { Name = "Faster",     Command = "-speed 3" },
             new VideoView.VideoEncodeSpeed() { Name = "Very Fast",  Command = "-speed 4" },
             new VideoView.VideoEncodeSpeed() { Name = "Super Fast", Command = "-speed 5" },
             new VideoView.VideoEncodeSpeed() { Name = "Ultra Fast", Command = "-speed 6" }
        };

        // -------------------------
        // Pixel Format
        // -------------------------
        public static List<string> pixelFormat = new List<string>()
        {
            "auto",
            "gbrp",
            "gbrp10le",
            "gbrp12le",
            "yuv420p",
            "yuv420p10le",
            "yuv420p12le",
            "yuv422p",
            "yuv422p10le",
            "yuv422p12le",
            "yuv440p",
            "yuv440p10le",
            "yuv440p12le",
            "yuv444p",
            "yuv444p10le",
            "yuv444p12le",
            "yuva420p",
        };

        // -------------------------
        // Quality
        // -------------------------
        public static List<VideoView.VideoQuality> quality = new List<VideoView.VideoQuality>()
        {
             new VideoView.VideoQuality() { Name = "Auto",     CRF = "",   Video_CRF_BitRate = "",      CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      MinRate = "", MaxRate = "", BufSize ="", NA = "2000K" },
             new VideoView.VideoQuality() { Name = "Lossless", CRF = "",   Video_CRF_BitRate = "",      CBR_BitMode = "",     CBR = "",      VBR_BitMode = "-q:v", VBR = "",      MinRate = "", MaxRate = "", BufSize ="", Lossless = "-lossless 1" },
             new VideoView.VideoQuality() { Name = "Ultra",    CRF = "10", Video_CRF_BitRate = "4000K", CBR_BitMode = "-b:v", CBR = "4000K", VBR_BitMode = "-q:v", VBR = "4000K", MinRate = "", MaxRate = "", BufSize ="" },
             new VideoView.VideoQuality() { Name = "High",     CRF = "12", Video_CRF_BitRate = "2000K", CBR_BitMode = "-b:v", CBR = "2000K", VBR_BitMode = "-q:v", VBR = "2000K", MinRate = "", MaxRate = "", BufSize ="" },
             new VideoView.VideoQuality() { Name = "Medium",   CRF = "16", Video_CRF_BitRate = "1300K", CBR_BitMode = "-b:v", CBR = "1300K", VBR_BitMode = "-q:v", VBR = "1300K", MinRate = "", MaxRate = "", BufSize ="" },
             new VideoView.VideoQuality() { Name = "Low",      CRF = "20", Video_CRF_BitRate = "600K",  CBR_BitMode = "-b:v", CBR = "600K",  VBR_BitMode = "-q:v", VBR = "600K",  MinRate = "", MaxRate = "", BufSize ="" },
             new VideoView.VideoQuality() { Name = "Sub",      CRF = "25", Video_CRF_BitRate = "250K",  CBR_BitMode = "-b:v", CBR = "250K",  VBR_BitMode = "-q:v", VBR = "250K",  MinRate = "", MaxRate = "", BufSize ="" },
             new VideoView.VideoQuality() { Name = "Custom",   CRF = "",   Video_CRF_BitRate = "",      CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      MinRate = "", MaxRate = "", BufSize ="" }
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
            if (VM.VideoView.Video_Quality_SelectedItem == "Auto")
            {
                VM.VideoView.Video_Pass_Items = new List<string>()
                {
                    "2 Pass"
                };

                VM.VideoView.Video_Pass_SelectedItem = "2 Pass";
                VM.VideoView.Video_Pass_IsEnabled = false;
                VideoControls.passUserSelected = false;

                VM.VideoView.Video_CRF_IsEnabled = false;
            }
            // Lossless
            else if (VM.VideoView.Video_Quality_SelectedItem == "Lossless")
            {
                VM.VideoView.Video_Pass_Items = new List<string>()
                {
                    "1 Pass",
                    "2 Pass"
                };

                VM.VideoView.Video_Pass_SelectedItem = "2 Pass";
                VM.VideoView.Video_Pass_IsEnabled = true;
                VM.VideoView.Video_CRF_IsEnabled = false;
            }
            // Custom
            else if (VM.VideoView.Video_Quality_SelectedItem == "Custom")
            {
                VM.VideoView.Video_Pass_Items = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                VM.VideoView.Video_Pass_IsEnabled = true;
                VM.VideoView.Video_CRF_IsEnabled = true;
            }
            // None
            else if (VM.VideoView.Video_Quality_SelectedItem == "None")
            {
                VM.VideoView.Video_Pass_Items = new List<string>()
                {
                    "auto"
                };

                VM.VideoView.Video_Pass_IsEnabled = false;
                VM.VideoView.Video_CRF_IsEnabled = false;
            }
            // Presets: Ultra, High, Medium, Low, Sub
            else
            {
                VM.VideoView.Video_Pass_Items = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                VM.VideoView.Video_Pass_IsEnabled = true;
                VM.VideoView.Video_CRF_IsEnabled = false;

                // Default to CRF
                if (VideoControls.passUserSelected == false)
                {
                    VM.VideoView.Video_Pass_SelectedItem = "CRF";
                    VideoControls.passUserSelected = true;
                }
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
            VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";

            // Framerate
            VM.VideoView.Video_FPS_SelectedItem = "auto";
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
            // None
        }

        // -------------------------
        // Unchecked
        // -------------------------
        public static void Controls_Unhecked()
        {
            // BitRate Mode
            VM.VideoView.Video_VBR_IsChecked = false;
        }


        // -------------------------
        // Enabled
        // -------------------------
        public static void Controls_Enable()
        {
            // Video Encode Speed
            VM.VideoView.Video_EncodeSpeed_IsEnabled = true;

            // Video Codec
            VM.VideoView.Video_Codec_IsEnabled = true;

            // Video Quality
            VM.VideoView.Video_Quality_IsEnabled = true;

            // Video VBR
            VM.VideoView.Video_VBR_IsEnabled = true;

            // Pixel Format
            VM.VideoView.Video_PixelFormat_IsEnabled = true;

            // FPS ComboBox
            VM.VideoView.Video_FPS_IsEnabled = true;

            // Optimize ComboBox
            VM.VideoView.Video_Optimize_IsEnabled = true;

            // Scaling ComboBox
            VM.VideoView.Video_ScalingAlgorithm_IsEnabled = true;

            // Crop
            VM.VideoView.Video_Crop_IsEnabled = true;
        }

        // -------------------------
        // Disabled
        // -------------------------
        public static void Controls_Disable()
        {
            // Subtitle Codec
            VM.SubtitleView.Subtitle_Codec_IsEnabled = false;

            // Subtitle Stream
            VM.SubtitleView.Subtitle_Stream_IsEnabled = false;
        }
    }
}
