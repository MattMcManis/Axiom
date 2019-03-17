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
    public class x265
    {
        // ---------------------------------------------------------------------------
        // Arguments
        // ---------------------------------------------------------------------------

        // -------------------------
        // Codec
        // -------------------------
        public static string codec = "-c:v libx265";


        // ---------------------------------------------------------------------------
        // Item Source
        // ---------------------------------------------------------------------------

        // -------------------------
        // Encode Speed
        // -------------------------
        public static List<ViewModel.VideoEncodeSpeed> encodeSpeed = new List<ViewModel.VideoEncodeSpeed>()
        {
             new ViewModel.VideoEncodeSpeed() { Name = "None",       Command = ""},
             new ViewModel.VideoEncodeSpeed() { Name = "Placebo",    Command = "-preset placebo" },
             new ViewModel.VideoEncodeSpeed() { Name = "Very Slow",  Command = "-preset veryslow" },
             new ViewModel.VideoEncodeSpeed() { Name = "Slower",     Command = "-preset slower" },
             new ViewModel.VideoEncodeSpeed() { Name = "Slow",       Command = "-preset slow" },
             new ViewModel.VideoEncodeSpeed() { Name = "Medium",     Command = "-preset medium" },
             new ViewModel.VideoEncodeSpeed() { Name = "Fast",       Command = "-preset fast" },
             new ViewModel.VideoEncodeSpeed() { Name = "Faster",     Command = "-preset faster" },
             new ViewModel.VideoEncodeSpeed() { Name = "Very Fast",  Command = "-preset veryfast" },
             new ViewModel.VideoEncodeSpeed() { Name = "Super Fast", Command = "-preset superfast" },
             new ViewModel.VideoEncodeSpeed() { Name = "Ultra Fast", Command = "-preset ultrafast" }
        };

        // -------------------------
        // Pixel Format
        // -------------------------
        public static List<string> pixelFormat = new List<string>()
        {
            "auto",
            "yuv420p",
            "yuv422p",
            "yuv444p",
            "gbrp",
            "yuv420p10le",
            "yuv422p10le",
            "yuv444p10le",
            "gbrp10le",
            "gray",
            "gray10le"
        };

        // -------------------------
        // Quality
        // -------------------------
        public static List<ViewModel.VideoQuality> quality = new List<ViewModel.VideoQuality>()
        {
             new ViewModel.VideoQuality() { Name = "Auto",      CRF = "",   CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      Minrate = "", Maxrate = "", Bufsize ="", NA = "" },
             new ViewModel.VideoQuality() { Name = "Lossless",  CRF = "",   CBR_BitMode = "",     CBR = "",      VBR_BitMode = "",     VBR = "",      Minrate = "", Maxrate = "", Bufsize ="", Lossless = "" },
             new ViewModel.VideoQuality() { Name = "Ultra",     CRF = "16", CBR_BitMode = "-b:v", CBR = "5000K", VBR_BitMode = "-q:v", VBR = "5000K", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "High",      CRF = "20", CBR_BitMode = "-b:v", CBR = "2500K", VBR_BitMode = "-q:v", VBR = "2500K", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Medium",    CRF = "25", CBR_BitMode = "-b:v", CBR = "1300K", VBR_BitMode = "-q:v", VBR = "1300K", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Low",       CRF = "35", CBR_BitMode = "-b:v", CBR = "600K",  VBR_BitMode = "-q:v", VBR = "600K",  Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Sub",       CRF = "45", CBR_BitMode = "-b:v", CBR = "250K",  VBR_BitMode = "-q:v", VBR = "250K",  Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Custom",    CRF = "",   CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      Minrate = "", Maxrate = "", Bufsize ="" }
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
            new ViewModel.VideoOptimize() { Name = "None",   Tune = "none", Profile = "none",  Level = "none", Command = "" },
            new ViewModel.VideoOptimize() { Name = "Custom", Tune = "",     Profile = "",      Level = "",     Command = "" },
            new ViewModel.VideoOptimize() { Name = "Web",    Tune = "none", Profile = "main",  Level = "3.1",  Command = "-movflags +faststart" },
            new ViewModel.VideoOptimize() { Name = "PC HD",  Tune = "none", Profile = "main",  Level = "5.2",  Command = "" },
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
            //"main-intra",
            "mainstillpicture",
            "main444-8",
            //"main444-intra",
            "main444-stillpicture",
            "main10",
            //"main10-intra",
            "main422-10",
            //"main422-10-intra",
            "main444-10",
            //"main444-10-intra",
            "main12",
            "main422-12",
            //"main422-12-intra",
            "main444-12",
            //"main444-12-intra",
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
        // Item Source
        // -------------------------
        public static void controlsItemSource(ViewModel vm)
        {
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
            vm.Video_PixelFormat_SelectedItem = "yuv420p";

            // Framerate
            vm.Video_FPS_SelectedItem = "auto";
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
            vm.Video_VBR_IsChecked = false;
        }

        // -------------------------
        // Enabled
        // -------------------------
        public static void controlsEnable(ViewModel vm)
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
            // None
        }
    }
}
