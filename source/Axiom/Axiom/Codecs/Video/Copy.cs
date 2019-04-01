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
    class VideoCopy
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public static List<ViewModel.VideoCodec> codec = new List<ViewModel.VideoCodec>()
        {
             new ViewModel.VideoCodec()
             {
                 Codec = "copy",
                 Parameters = ""
             }
        };

        public static void Codec_Set(ViewModel vm)
        {
            // Combine Codec + Parameters
            List<string> codec = new List<string>()
            {
                "-c:v",
                VideoCopy.codec.FirstOrDefault()?.Codec,
                VideoCopy.codec.FirstOrDefault()?.Parameters
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
             new ViewModel.VideoEncodeSpeed() { Name = "none", Command = ""},
        };

        // -------------------------
        // Pixel Format
        // -------------------------
        public static List<string> pixelFormat = new List<string>()
        {
            "auto"
        };

        // -------------------------
        // Quality
        // -------------------------
        public static List<ViewModel.VideoQuality> quality = new List<ViewModel.VideoQuality>()
        {
             new ViewModel.VideoQuality() { Name = "Auto",   CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="", NA = "" },
             new ViewModel.VideoQuality() { Name = "Ultra",  CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
             new ViewModel.VideoQuality() { Name = "High",   CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
             new ViewModel.VideoQuality() { Name = "Medium", CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
             new ViewModel.VideoQuality() { Name = "Low",    CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
             new ViewModel.VideoQuality() { Name = "Sub",    CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
             new ViewModel.VideoQuality() { Name = "Custom", CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" }
        };

        // -------------------------
        // Pass
        // -------------------------
        public static void EncodingPass(ViewModel vm)
        {
            // Items Source
            vm.Video_Pass_Items = new List<string>()
            {
                "auto"
            };

            vm.Video_Pass_SelectedItem = "auto";
            vm.Video_Pass_IsEnabled = false;
            VideoControls.passUserSelected = false;

            vm.Video_CRF_IsEnabled = false;
            vm.Video_CRF_Text = string.Empty;
            vm.Video_BitRate_Text = string.Empty;
            vm.Video_MinRate_Text = string.Empty;
            vm.Video_MaxRate_Text = string.Empty;
            vm.Video_BufSize_Text = string.Empty;
        }

        // -------------------------
        // Optimize
        // -------------------------
        public static List<ViewModel.VideoOptimize> optimize = new List<ViewModel.VideoOptimize>()
        {
            new ViewModel.VideoOptimize() { Name = "None", Tune = "none", Profile = "none", Level = "none", Command = "" },
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
            vm.Video_PixelFormat_SelectedItem = "auto";

            // Framerate
            vm.Video_FPS_SelectedItem = "auto";
        }


        // -------------------------
        // Expanded
        // -------------------------
        public static void Controls_Expanded(ViewModel vm)
        {
            // None
        }

        // -------------------------
        // Collapsed
        // -------------------------
        public static void Controls_Collapsed(ViewModel vm)
        {
            vm.Video_Optimize_IsExpanded = false;
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
            // Video Codec
            vm.Video_Codec_IsEnabled = true;

            // Video Quality
            vm.Video_Quality_IsEnabled = true;

            // Video VBR
            vm.Audio_VBR_IsEnabled = true;

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

            // Subtitle Codec
            vm.Subtitle_Codec_IsEnabled = true;

            // Subtitle Stream
            vm.Subtitle_Stream_IsEnabled = true;
        }

        // -------------------------
        // Disabled
        // -------------------------
        public static void Controls_Disable(ViewModel vm)
        {
            // Video Encode Speed
            vm.Video_EncodeSpeed_IsEnabled = false;
        }
    }
}
