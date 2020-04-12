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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axiom
{
    public class x265
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public static List<VideoViewModel.VideoCodec> codec = new List<VideoViewModel.VideoCodec>()
        {
             new VideoViewModel.VideoCodec()
             {
                 Codec = "libx265",
                 Parameters = ""
             }
        };

        public static void Codec_Set()
        {
            // Combine Codec + Parameters
            List<string> codec = new List<string>()
            {
                "-c:v",
                x265.codec.FirstOrDefault()?.Codec,
                x265.codec.FirstOrDefault()?.Parameters
            };

            VM.VideoView.Video_Codec = string.Join(" ", codec.Where(s => !string.IsNullOrEmpty(s)));
        }



        // ---------------------------------------------------------------------------
        // Items Source
        // ---------------------------------------------------------------------------

        // -------------------------
        // Encode Speed
        // -------------------------
        public static List<VideoViewModel.VideoEncodeSpeed> encodeSpeed = new List<VideoViewModel.VideoEncodeSpeed>()
        {
             new VideoViewModel.VideoEncodeSpeed() { Name = "none",       Command = ""},
             new VideoViewModel.VideoEncodeSpeed() { Name = "Placebo",    Command = "-preset placebo" },
             new VideoViewModel.VideoEncodeSpeed() { Name = "Very Slow",  Command = "-preset veryslow" },
             new VideoViewModel.VideoEncodeSpeed() { Name = "Slower",     Command = "-preset slower" },
             new VideoViewModel.VideoEncodeSpeed() { Name = "Slow",       Command = "-preset slow" },
             new VideoViewModel.VideoEncodeSpeed() { Name = "Medium",     Command = "-preset medium" },
             new VideoViewModel.VideoEncodeSpeed() { Name = "Fast",       Command = "-preset fast" },
             new VideoViewModel.VideoEncodeSpeed() { Name = "Faster",     Command = "-preset faster" },
             new VideoViewModel.VideoEncodeSpeed() { Name = "Very Fast",  Command = "-preset veryfast" },
             new VideoViewModel.VideoEncodeSpeed() { Name = "Super Fast", Command = "-preset superfast" },
             new VideoViewModel.VideoEncodeSpeed() { Name = "Ultra Fast", Command = "-preset ultrafast" }
        };

        // -------------------------
        // Pixel Format
        // -------------------------
        public static List<string> pixelFormat = new List<string>()
        {
            "auto",
            "gbrp",
            "gbrp10le",
            "gray",
            "gray10le",
            "yuv420p",
            "yuv420p10le",
            "yuv422p",
            "yuv422p10le",
            "yuv444p",
            "yuv444p10le",
        };

        // -------------------------
        // Quality
        // -------------------------
        public static List<VideoViewModel.VideoQuality> quality = new List<VideoViewModel.VideoQuality>()
        {
             new VideoViewModel.VideoQuality() { Name = "Auto",      CRF = "",   CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      MinRate = "", MaxRate = "", BufSize ="", NA = "3000K" },
             new VideoViewModel.VideoQuality() { Name = "Lossless",  CRF = "",   CBR_BitMode = "",     CBR = "",      VBR_BitMode = "",     VBR = "",      MinRate = "", MaxRate = "", BufSize ="", Lossless = "" },
             new VideoViewModel.VideoQuality() { Name = "Ultra",     CRF = "16", CBR_BitMode = "-b:v", CBR = "5000K", VBR_BitMode = "-q:v", VBR = "5000K", MinRate = "", MaxRate = "", BufSize ="" },
             new VideoViewModel.VideoQuality() { Name = "High",      CRF = "20", CBR_BitMode = "-b:v", CBR = "2500K", VBR_BitMode = "-q:v", VBR = "2500K", MinRate = "", MaxRate = "", BufSize ="" },
             new VideoViewModel.VideoQuality() { Name = "Medium",    CRF = "25", CBR_BitMode = "-b:v", CBR = "1300K", VBR_BitMode = "-q:v", VBR = "1300K", MinRate = "", MaxRate = "", BufSize ="" },
             new VideoViewModel.VideoQuality() { Name = "Low",       CRF = "35", CBR_BitMode = "-b:v", CBR = "600K",  VBR_BitMode = "-q:v", VBR = "600K",  MinRate = "", MaxRate = "", BufSize ="" },
             new VideoViewModel.VideoQuality() { Name = "Sub",       CRF = "45", CBR_BitMode = "-b:v", CBR = "250K",  VBR_BitMode = "-q:v", VBR = "250K",  MinRate = "", MaxRate = "", BufSize ="" },
             new VideoViewModel.VideoQuality() { Name = "Custom",    CRF = "",   CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      MinRate = "", MaxRate = "", BufSize ="" }
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
        public static List<VideoViewModel.VideoOptimize> optimize = new List<VideoViewModel.VideoOptimize>()
        {
            new VideoViewModel.VideoOptimize() { Name = "None",   Tune = "none", Profile = "none",   Level = "none", Command = "" },
            new VideoViewModel.VideoOptimize() { Name = "Custom", Tune = "none", Profile = "none",   Level = "none", Command = "" },
            new VideoViewModel.VideoOptimize() { Name = "Web",    Tune = "none", Profile = "main",   Level = "3.1",  Command = "-movflags +faststart" },
            new VideoViewModel.VideoOptimize() { Name = "PC HD",  Tune = "none", Profile = "main10", Level = "5.1",  Command = "" },
            new VideoViewModel.VideoOptimize() { Name = "UHD",    Tune = "none", Profile = "main10", Level = "5.1",  Command = "-sar 1:1 -x265-params \"colorprim=bt2020:transfer=bt2020:colormatrix=bt2020:colorspace=bt2020\"" },
        };

        // -------------------------
        // Tune
        // -------------------------
        public static List<string> tune = new List<string>()
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
        public static List<string> profile = new List<string>()
        {
            "none",
            "main",
            "mainstillpicture",
            "main444-8",
            "main444-stillpicture",
            "main10",
            "main422-10",
            "main444-10",
            "main12",
            "main422-12",
            "main444-12",
        };

        // -------------------------
        // Level
        // -------------------------
        public static List<string> level = new List<string>()
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
            "8.5"
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
            VM.VideoView.Video_Optimize_IsExpanded = true;
        }

        // -------------------------
        // Collapsed
        // -------------------------
        public static void Controls_Collapsed()
        {
            // None
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

            // Subtitle Codec
            VM.SubtitleView.Subtitle_Codec_IsEnabled = true;

            // Subtitle Stream
            VM.SubtitleView.Subtitle_Stream_IsEnabled = true;
        }

        // -------------------------
        // Disabled
        // -------------------------
        public static void Controls_Disable()
        {
            // None
        }
    }
}
