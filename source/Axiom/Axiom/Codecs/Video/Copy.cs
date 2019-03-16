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
    class VideoCopy
    {
        // ---------------------------------------------------------------------------
        // Arguments
        // ---------------------------------------------------------------------------

        // -------------------------
        // Codec
        // -------------------------
        // command
        public static string codec = "-c:v copy";



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
            "auto"
        };

        // -------------------------
        // Quality
        // -------------------------
        public static List<ViewModel.VideoQuality> quality = new List<ViewModel.VideoQuality>()
        {
             new ViewModel.VideoQuality() { Name = "Auto",   CRF = "",  CRF_Bitrate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", Minrate = "", Maxrate = "", Bufsize ="", NA = "" },
             new ViewModel.VideoQuality() { Name = "Ultra",  CRF = "",  CRF_Bitrate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "High",   CRF = "",  CRF_Bitrate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Medium", CRF = "",  CRF_Bitrate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Low",    CRF = "",  CRF_Bitrate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Sub",    CRF = "",  CRF_Bitrate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Custom", CRF = "",  CRF_Bitrate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", Minrate = "", Maxrate = "", Bufsize ="" }
        };

        // -------------------------
        // Pass
        // -------------------------
        public static List<string> pass = new List<string>()
        {
            "auto"
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
            // Encode Speed
            vm.VideoEncodeSpeed_Items = encodeSpeed;

            // Pixel Format
            vm.PixelFormat_Items = pixelFormat;

            // Pass
            vm.Pass_Items = pass;

            // Video Quality
            vm.VideoQuality_Items = quality;

            // Optimize
            vm.Video_Optimize_Items = optimize;
            // Tune
            vm.Optimize_Tune_Items = tune;
            // Profile
            vm.Optimize_Profile_Items = profile;
            // Level
            vm.Optimize_Level_Items = level;
        }

        // -------------------------
        // Selected Items
        // -------------------------
        public static void controlsSelected(ViewModel vm)
        {
            // Pixel Format
            vm.PixelFormat_SelectedItem = "auto";

            // Framerate
            vm.FPS_SelectedItem = "auto";
        }

        // -------------------------
        // Checked
        // -------------------------
        public static void controlsChecked(ViewModel vm)
        {
            // None
        }

        // -------------------------
        // Unchecked
        // -------------------------
        public static void controlsUnhecked(ViewModel vm)
        {
            // Bitrate Mode
            vm.VideoVBR_IsChecked = false;
        }

        // -------------------------
        // Enabled
        // -------------------------
        public static void controlsEnable(ViewModel vm)
        {
            // Video Codec
            vm.VideoCodec_IsEnabled = true;

            // Video Quality
            vm.VideoQuality_IsEnabled = true;

            // Video VBR
            vm.AudioVBR_IsEnabled = true;

            // Pixel Format
            vm.PixelFormat_IsEnabled = true;

            // FPS ComboBox
            vm.FPS_IsEnabled = true;

            // Optimize ComboBox
            vm.Video_Optimize_IsEnabled = true;

            // Scaling ComboBox
            vm.Scaling_IsEnabled = true;

            // Crop
            vm.Crop_IsEnabled = true;

            // Subtitle Codec
            vm.SubtitleCodec_IsEnabled = true;

            // Subtitle Stream
            vm.SubtitleStream_IsEnabled = true;
        }

        // -------------------------
        // Disabled
        // -------------------------
        public static void controlsDisable(ViewModel vm)
        {
            // Video Encode Speed
            vm.VideoEncodeSpeed_IsEnabled = false;
        }
    }
}
