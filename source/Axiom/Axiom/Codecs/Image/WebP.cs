/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2019 Matt McManis
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axiom
{
    public class WebP
    {
        // ---------------------------------------------------------------------------
        // Arguments
        // ---------------------------------------------------------------------------

        // -------------------------
        // Codec
        // -------------------------
        public static string codec = "-c:v libwebp";



        // ---------------------------------------------------------------------------
        // Item Source
        // ---------------------------------------------------------------------------

        // -------------------------
        // Encode Speed
        // -------------------------
        public static List<ViewModel.VideoEncodeSpeed> encodeSpeed = new List<ViewModel.VideoEncodeSpeed>()
        {
             new ViewModel.VideoEncodeSpeed() { Name = "None", Command = ""},
        };

        // -------------------------
        // Pixel Format
        // -------------------------
        public static List<string> pixelFormat = new List<string>()
        {
            "bgra",
            "yuv420p",
            "yuva420p"
        };

        // -------------------------
        // Quality
        // -------------------------
        public static List<ViewModel.VideoQuality> quality = new List<ViewModel.VideoQuality>()
        {
             new ViewModel.VideoQuality() { Name = "Auto",     CRF = "", CBR_BitMode = "-q:v", CBR = "85",  VBR_BitMode = "-q:v", VBR = "85",  Minrate = "", Maxrate = "", Bufsize ="", NA = "85" },
             new ViewModel.VideoQuality() { Name = "Lossless", CRF = "", CBR_BitMode = "",     CBR = "",    VBR_BitMode = "",     VBR = "",    Minrate = "", Maxrate = "", Bufsize ="", Lossless = "-lossless 1" },
             new ViewModel.VideoQuality() { Name = "Ultra",    CRF = "", CBR_BitMode = "-q:v", CBR = "100", VBR_BitMode = "-q:v", VBR = "100", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "High",     CRF = "", CBR_BitMode = "-q:v", CBR = "85",  VBR_BitMode = "-q:v", VBR = "85",  Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Medium",   CRF = "", CBR_BitMode = "-q:v", CBR = "60",  VBR_BitMode = "-q:v", VBR = "60",  Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Low",      CRF = "", CBR_BitMode = "-q:v", CBR = "45",  VBR_BitMode = "-q:v", VBR = "45",  Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Sub",      CRF = "", CBR_BitMode = "-q:v", CBR = "25",  VBR_BitMode = "-q:v", VBR = "25",  Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Custom",   CRF = "", CBR_BitMode = "-q:v", CBR = "",    VBR_BitMode = "-q:v", VBR = "",    Minrate = "", Maxrate = "", Bufsize ="" }
        };

        // -------------------------
        // Pass
        // -------------------------
        public static List<string> pass = new List<string>()
        {
            "1 Pass"
        };

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
        // Item Source
        // -------------------------
        public static void controlsItemSource(ViewModel vm)
        {
            // Video Codec
            vm.Video_Codec_Command = codec;

            // Encode Speed
            vm.Video_EncodeSpeed_Items = encodeSpeed;

            // Pixel Format
            vm.Video_PixelFormat_Items = pixelFormat;

            // Pass
            vm.Video_Pass_Items = pass;

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
        public static void controlsSelected(ViewModel vm)
        {

            // Pixel Format
            vm.Video_PixelFormat_SelectedItem = "bgra";

            // Framerate
            vm.Video_FPS_SelectedItem = "auto";
        }

        // -------------------------
        // Checked
        // -------------------------
        public static void controlsChecked(ViewModel vm)
        {
            // Bitrate Mode
            vm.Video_VBR_IsChecked = true;
        }

        // -------------------------
        // Unchecked
        // -------------------------
        public static void controlsUnhecked(ViewModel vm)
        {
            // None
        }

        // -------------------------
        // Enabled
        // -------------------------
        public static void controlsEnable(ViewModel vm)
        {
            // Video Codec
            vm.Video_Codec_IsEnabled = true;

            // Video Quality
            vm.Video_Quality_IsEnabled = true;

            // Pixel Format
            vm.Video_PixelFormat_IsEnabled = true;

            // FPS ComboBox
            vm.Video_FPS_IsEnabled = true;

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
        public static void controlsDisable(ViewModel vm)
        {
            // Video Encode Speed
            vm.Video_EncodeSpeed_IsEnabled = false;

            // Video VBR
            vm.Video_VBR_IsEnabled = false;

            // Optimize ComboBox
            vm.Video_Optimize_IsEnabled = false;
        }
    }
}
