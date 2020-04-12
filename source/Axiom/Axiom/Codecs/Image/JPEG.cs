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
    public class JPEG
    {
        // ---------------------------------------------------------------------------
        // Arguments
        // ---------------------------------------------------------------------------

        // -------------------------
        // Codec
        // -------------------------
        public static List<VideoViewModel.VideoCodec> codec = new List<VideoViewModel.VideoCodec>()
        {
             new VideoViewModel.VideoCodec()
             {
                 Codec = "mjpeg",
                 Parameters = ""
             }
        };

        public static void Codec_Set()
        {
            // Combine Codec + Parameters
            List<string> codec = new List<string>()
            {
                "-c:v",
                JPEG.codec.FirstOrDefault()?.Codec,
                JPEG.codec.FirstOrDefault()?.Parameters
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
             new VideoViewModel.VideoEncodeSpeed() { Name = "none", Command = ""},
        };

        // -------------------------
        // Pixel Format
        // -------------------------
        public static List<string> pixelFormat = new List<string>()
        {
            "auto",
            "yuvj420p",
            "yuvj422p",
            "yuvj444p"
        };

        // -------------------------
        // Quality
        // -------------------------
        public static List<VideoViewModel.VideoQuality> quality = new List<VideoViewModel.VideoQuality>()
        {
             new VideoViewModel.VideoQuality() { Name = "Auto",   CRF = "", CBR_BitMode = "-q:v", CBR = "2",  VBR_BitMode = "-q:v", VBR = "2",  MinRate = "", MaxRate = "", BufSize ="", NA = "2" },
             new VideoViewModel.VideoQuality() { Name = "Ultra",  CRF = "", CBR_BitMode = "-q:v", CBR = "2",  VBR_BitMode = "-q:v", VBR = "2",  MinRate = "", MaxRate = "", BufSize ="" },
             new VideoViewModel.VideoQuality() { Name = "High",   CRF = "", CBR_BitMode = "-q:v", CBR = "4",  VBR_BitMode = "-q:v", VBR = "4",  MinRate = "", MaxRate = "", BufSize ="" },
             new VideoViewModel.VideoQuality() { Name = "Medium", CRF = "", CBR_BitMode = "-q:v", CBR = "8",  VBR_BitMode = "-q:v", VBR = "8",  MinRate = "", MaxRate = "", BufSize ="" },
             new VideoViewModel.VideoQuality() { Name = "Low",    CRF = "", CBR_BitMode = "-q:v", CBR = "15", VBR_BitMode = "-q:v", VBR = "15", MinRate = "", MaxRate = "", BufSize ="" },
             new VideoViewModel.VideoQuality() { Name = "Sub",    CRF = "", CBR_BitMode = "-q:v", CBR = "25", VBR_BitMode = "-q:v", VBR = "25", MinRate = "", MaxRate = "", BufSize ="" },
             new VideoViewModel.VideoQuality() { Name = "Custom", CRF = "", CBR_BitMode = "-q:v", CBR = "",   VBR_BitMode = "-q:v", VBR = "",   MinRate = "", MaxRate = "", BufSize ="" }
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
                    "1 Pass"
                };

                VM.VideoView.Video_Pass_SelectedItem = "1 Pass";
                VM.VideoView.Video_Pass_IsEnabled = false;
                VideoControls.passUserSelected = false;

                VM.VideoView.Video_CRF_IsEnabled = false;
            }
            // Custom
            else if (VM.VideoView.Video_Quality_SelectedItem == "Custom")
            {
                VM.VideoView.Video_Pass_Items = new List<string>()
                {
                    "1 Pass"
                };

                VM.VideoView.Video_Pass_IsEnabled = false;
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
                    "1 Pass"
                };

                VM.VideoView.Video_Pass_IsEnabled = false;
                VM.VideoView.Video_CRF_IsEnabled = false;

                // Default to 1 Pass
                if (VideoControls.passUserSelected == false)
                {
                    VM.VideoView.Video_Pass_SelectedItem = "1 Pass";
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
            new VideoViewModel.VideoOptimize() { Name = "None", Tune = "none", Profile = "none", Level = "none", Command = "" },
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
            VM.VideoView.Video_PixelFormat_SelectedItem = "yuvj444p";

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
