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
    public class HuffYUV
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public static List<VideoView.VideoCodec> codec = new List<VideoView.VideoCodec>()
        {
             new VideoView.VideoCodec()
             {
                 Codec = "huffyuv",
                 Parameters_1Pass = "-context 1 -vstrict -2 -pred 2",
                 Parameters_2Pass = "-context 2 -vstrict -2 -pred 2",
             }
        };

        public static void Codec_Set()
        {
            string parameters = string.Empty;

            // 1 Pass
            if (VideoView.vm.Video_Pass_SelectedItem == "1 Pass")
            {
                parameters = HuffYUV.codec.FirstOrDefault()?.Parameters_1Pass;
            }
            // 2 Pass
            else if (VideoView.vm.Video_Pass_SelectedItem == "2 Pass")
            {
                parameters = HuffYUV.codec.FirstOrDefault()?.Parameters_2Pass;
            }

            // Combine Codec + Parameters
            List<string> codec = new List<string>()
            {
                "-c:v",
                HuffYUV.codec.FirstOrDefault()?.Codec,
                parameters
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
             new VideoView.VideoEncodeSpeed() { Name = "none", Command = ""},
        };

        // -------------------------
        // Pixel Format
        // -------------------------
        public static List<string> pixelFormat = new List<string>()
        {
            "auto",
            "bgra",
            "gbrap",
            "gbrp",
            "gbrp10le",
            "gbrp12le",
            "gbrp14le",
            "gbrp9le",
            "gray",
            "gray16le",
            "rgb24",
            "ya8",
            "yuv410p",
            "yuv411p",
            "yuv420p",
            "yuv420p10le",
            "yuv420p12le",
            "yuv420p14le",
            "yuv420p16le",
            "yuv420p9le",
            "yuv422p",
            "yuv422p10le",
            "yuv422p12le",
            "yuv422p14le",
            "yuv422p16le",
            "yuv422p9le",
            "yuv440p",
            "yuv444p",
            "yuv444p10le",
            "yuv444p12le",
            "yuv444p14le",
            "yuv444p16le",
            "yuv444p9le",
            "yuva420p",
            "yuva420p10le",
            "yuva420p16le",
            "yuva420p9le",
            "yuva422p",
            "yuva422p10le",
            "yuva422p16le",
            "yuva422p9le",
            "yuva444p",
            "yuva444p10le",
            "yuva444p16le",
            "yuva444p9le",
        };

        // -------------------------
        // Quality
        // -------------------------
        public static List<VideoView.VideoQuality> quality = new List<VideoView.VideoQuality>()
        {
             new VideoView.VideoQuality() { Name = "Auto",     CRF = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="", NA = "" },
             new VideoView.VideoQuality() { Name = "Lossless", CRF = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="", Lossless = "" },
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
            // Lossless
            else if (VideoView.vm.Video_Quality_SelectedItem == "Lossless")
            {
                VideoView.vm.Video_Pass_Items = new List<string>()
                {
                    "1 Pass",
                    "2 Pass"
                };

                VideoView.vm.Video_Pass_IsEnabled = true;
                VideoView.vm.Video_CRF_IsEnabled = false;
            }
            // Custom
            else if (VideoView.vm.Video_Quality_SelectedItem == "Custom")
            {
                VideoView.vm.Video_Pass_Items = new List<string>()
                {
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
                    "1 Pass",
                    "2 Pass"
                };

                VideoView.vm.Video_Pass_IsEnabled = true;
                VideoView.vm.Video_CRF_IsEnabled = false;

                // Default to CRF
                if (VideoControls.passUserSelected == false)
                {
                    VideoView.vm.Video_Pass_SelectedItem = "2 Pass";
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

            // Set New Codec Parameters on Pass Change
            // 1 Pass -context 1
            // 2 Pass -context 2
            Codec_Set();
        }

        // -------------------------
        // Optimize
        // -------------------------
        public static List<VideoView.VideoOptimize> optimize = new List<VideoView.VideoOptimize>()
        {
            new VideoView.VideoOptimize() { Name = "None", Tune = "none", Profile = "none", Level = "none", Command = "" },
        };

        // -------------------------
        // Tune
        // -------------------------
        public static List<string> tune = new List<string>()
        {
            "none",
        };

        // -------------------------
        // Profile
        // -------------------------
        public static List<string> profile = new List<string>()
        {
            "none",
        };

        // -------------------------
        // Level
        // -------------------------
        public static List<string> level = new List<string>()
        {
            "none",
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
        // Selected Item
        // -------------------------
        public static void Controls_Selected()
        {

            // Pixel Format
            VideoView.vm.Video_PixelFormat_SelectedItem = "yuv444p";

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
            // Video Codec
            VideoView.vm.Video_Codec_IsEnabled = true;

            // Video Quality
            VideoView.vm.Video_Quality_IsEnabled = true;

            // Pixel Format
            VideoView.vm.Video_PixelFormat_IsEnabled = true;

            // Framerate
            VideoView.vm.Video_FPS_IsEnabled = true;

            // Scaling
            VideoView.vm.Video_ScalingAlgorithm_IsEnabled = true;

            // Crop
            VideoView.vm.Video_Crop_IsEnabled = true;

            // Subtitle Codec
            SubtitleView.vm.Subtitle_Codec_IsEnabled = true;

            // Subtitle Stream
            SubtitleView.vm.Subtitle_Stream_IsEnabled = true;
        }

        // -------------------------
        // Disabled
        // -------------------------
        public static void Controls_Disable()
        {
            // Video Encode Speed
            VideoView.vm.Video_EncodeSpeed_IsEnabled = false;

            // Video VBR
            VideoView.vm.Video_VBR_IsEnabled = false;

            // Optimize
            VideoView.vm.Video_Optimize_IsEnabled = false;
        }


    }
}
