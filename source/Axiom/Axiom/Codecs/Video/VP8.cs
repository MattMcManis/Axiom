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
    public class VP8
    {
        // -------------------------
        // Codec
        // -------------------------
        // command
        public static string codec = "-c:v libvpx";

        // -------------------------
        // Pixel Format
        // -------------------------
        // selected item
        public static string pixfmt = "yuv420p";

        // -------------------------
        // Framerate
        // -------------------------
        // selected item
        public static string fps = "auto";

        // -------------------------
        // Encode Speed
        // -------------------------
        public static List<ViewModel.VideoEncodeSpeed> encodeSpeed = new List<ViewModel.VideoEncodeSpeed>()
        {
             new ViewModel.VideoEncodeSpeed() { Name = "None",       Command = ""},
             new ViewModel.VideoEncodeSpeed() { Name = "Placebo",    Command = "-quality best -cpu-used 0",  Command_2Pass = "-quality best -cpu-used 0" },
             new ViewModel.VideoEncodeSpeed() { Name = "Very Slow",  Command = "-quality good -cpu-used 0",  Command_2Pass = "-quality best -cpu-used 0" },
             new ViewModel.VideoEncodeSpeed() { Name = "Slower",     Command = "-quality good -cpu-used 0",  Command_2Pass = "-quality best -cpu-used 0" },
             new ViewModel.VideoEncodeSpeed() { Name = "Slow",       Command = "-quality good -cpu-used 0",  Command_2Pass = "-quality best -cpu-used 0" },
             new ViewModel.VideoEncodeSpeed() { Name = "Medium",     Command = "-quality good -cpu-used 0",  Command_2Pass = "-quality best -cpu-used 0" },
             new ViewModel.VideoEncodeSpeed() { Name = "Fast",       Command = "-quality good -cpu-used 1",  Command_2Pass = "-quality best -cpu-used 0" },
             new ViewModel.VideoEncodeSpeed() { Name = "Faster",     Command = "-quality good -cpu-used 2",  Command_2Pass = "-quality best -cpu-used 0" },
             new ViewModel.VideoEncodeSpeed() { Name = "Very Fast",  Command = "-quality good -cpu-used 3",  Command_2Pass = "-quality realtime -cpu-used 3" },
             new ViewModel.VideoEncodeSpeed() { Name = "Super Fast", Command = "-quality good -cpu-used 4",  Command_2Pass = "-quality realtime -cpu-used 4" },
             new ViewModel.VideoEncodeSpeed() { Name = "Ultra Fast", Command = "-quality good -cpu-used 5",  Command_2Pass = "-quality realtime -cpu-used 5" },
        };

        // -------------------------
        // Quality
        // -------------------------
        public static List<ViewModel.VideoQuality> quality = new List<ViewModel.VideoQuality>()
        {
             new ViewModel.VideoQuality() { Name = "Auto",   CRF = "",   CRF_Bitrate = "",      CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "", VBR = "", Minrate = "", Maxrate = "", Bufsize ="", NA = "1500K" },
             new ViewModel.VideoQuality() { Name = "Ultra",  CRF = "10", CRF_Bitrate = "4000K", CBR_BitMode = "-b:v", CBR = "4000K", VBR_BitMode = "", VBR = "", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "High",   CRF = "12", CRF_Bitrate = "2000K", CBR_BitMode = "-b:v", CBR = "2000K", VBR_BitMode = "", VBR = "", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Medium", CRF = "16", CRF_Bitrate = "1300K", CBR_BitMode = "-b:v", CBR = "1300K", VBR_BitMode = "", VBR = "", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Low",    CRF = "20", CRF_Bitrate = "600K",  CBR_BitMode = "-b:v", CBR = "600K",  VBR_BitMode = "", VBR = "", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Sub",    CRF = "25", CRF_Bitrate = "250K",  CBR_BitMode = "-b:v", CBR = "250K",  VBR_BitMode = "", VBR = "", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Custom", CRF = "",   CRF_Bitrate = "",      CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "", VBR = "", Minrate = "", Maxrate = "", Bufsize ="" }
        };

        // -------------------------
        // Pass
        // -------------------------
        public static List<string> pass = new List<string>()
        {
            "CRF",
            "1 Pass",
            "2 Pass"
        };

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


        // -------------------------
        // Checked
        // -------------------------
        public static void controlsChecked(ViewModel vm)
        {
            // Bitrate Mode
            vm.VideoVBR_IsChecked = false;
        }

        // -------------------------
        // Enabled
        // -------------------------
        public static void controlsEnable(ViewModel vm)
        {
            // Video Encode Speed
            vm.VideoEncodeSpeed_IsEnabled = true;

            // Video Codec
            vm.VideoCodec_IsEnabled = true;

            // Video Quality
            vm.VideoQuality_IsEnabled = true;

            // Video VBR
            vm.VideoVBR_IsEnabled = true;

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
        }

        // -------------------------
        // Disabled
        // -------------------------
        public static void controlsDisable(ViewModel vm)
        {
            // Subtitle Codec
            vm.SubtitleCodec_IsEnabled = false;

            // Subtitle Stream
            vm.SubtitleStream_IsEnabled = false;
        }

    }
}
