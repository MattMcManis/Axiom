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
    public class x264
    {
        // -------------------------
        // Codec
        // -------------------------
        public static string codec = "-c:v libx264";

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
        // Quality
        // -------------------------
        public static List<ViewModel.VideoQuality> quality = new List<ViewModel.VideoQuality>()
        {
             new ViewModel.VideoQuality() { Name = "Auto",      CRF = "",   CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      Minrate = "", Maxrate = "", Bufsize ="", NA = "3000K" },
             new ViewModel.VideoQuality() { Name = "Lossless",  CRF = "",   CBR_BitMode = "",     CBR = "",      VBR_BitMode = "",     VBR = "",      Minrate = "", Maxrate = "", Bufsize ="", Lossless = "-qp 0" },
             new ViewModel.VideoQuality() { Name = "Ultra",     CRF = "16", CBR_BitMode = "-b:v", CBR = "5000K", VBR_BitMode = "-q:v", VBR = "5000K", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "High",      CRF = "20", CBR_BitMode = "-b:v", CBR = "2500K", VBR_BitMode = "-q:v", VBR = "2500K", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Medium",    CRF = "25", CBR_BitMode = "-b:v", CBR = "1300K", VBR_BitMode = "-q:v", VBR = "1300K", Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Low",       CRF = "35", CBR_BitMode = "-b:v", CBR = "600K",  VBR_BitMode = "-q:v", VBR = "600K",  Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Sub",       CRF = "45", CBR_BitMode = "-b:v", CBR = "250K",  VBR_BitMode = "-q:v", VBR = "250K",  Minrate = "", Maxrate = "", Bufsize ="" },
             new ViewModel.VideoQuality() { Name = "Custom",    CRF = "",   CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "",     VBR = "",      Minrate = "", Maxrate = "", Bufsize ="" }
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
            new ViewModel.VideoOptimize() { Name = "None",      Tune = "none",      Profile = "none",     Level = "none", Command = "" },
            new ViewModel.VideoOptimize() { Name = "Custom",    Tune = "",          Profile = "",         Level = "",     Command = "" },
            new ViewModel.VideoOptimize() { Name = "Web",       Tune = "none",      Profile = "baseline", Level = "3.0",  Command = "-movflags +faststart" },
            new ViewModel.VideoOptimize() { Name = "Animation", Tune = "animation", Profile = "main",     Level = "4.1",  Command = "" },
            new ViewModel.VideoOptimize() { Name = "PC HD",     Tune = "none",      Profile = "high",     Level = "4.2",  Command = "" },
            new ViewModel.VideoOptimize() { Name = "PC SD",     Tune = "none",      Profile = "baseline", Level = "3.1",  Command = "" },
            new ViewModel.VideoOptimize() { Name = "Blu-ray",   Tune = "none",      Profile = "main",     Level = "4.1",  Command = "-deblock 0:0 -sar 1/1 -x264-params \"bluray-compat=1:level=4.1:open-gop=1:slices=4:tff=1:colorprim=bt709:colormatrix=bt709:vbv-maxrate=40000:vbv-bufsize=30000:me=umh:ref=4:nal-hrd=vbr:aud=1:b-pyramid=strict\"" },
            new ViewModel.VideoOptimize() { Name = "Windows",   Tune = "none",      Profile = "baseline", Level = "3.1",  Command = "" },
            new ViewModel.VideoOptimize() { Name = "Apple",     Tune = "none",      Profile = "baseline", Level = "3.1",  Command = "" },
            new ViewModel.VideoOptimize() { Name = "Android",   Tune = "none",      Profile = "baseline", Level = "3.0",  Command = "" },
            new ViewModel.VideoOptimize() { Name = "PS3",       Tune = "none",      Profile = "main",     Level = "4.0",  Command = "" },
            new ViewModel.VideoOptimize() { Name = "PS4",       Tune = "none",      Profile = "high",     Level = "4.1",  Command = "" },
            new ViewModel.VideoOptimize() { Name = "Xbox 360",  Tune = "none",      Profile = "main",     Level = "4.0",  Command = "" },
            new ViewModel.VideoOptimize() { Name = "Xbox One",  Tune = "none",      Profile = "high",     Level = "4.1",  Command = "" }
        };

        // -------------------------
        // Tune
        // -------------------------
        public static List<string> tune = new List<string>()
        {
            "none",
            "film",
            "animation",
            "grain",
            "stillimage",
            "fastdecode",
            "zerolatency"
        };

        // -------------------------
        // Profile
        // -------------------------
        public static List<string> profile = new List<string>()
        {
            "none",
            "baseline",
            "main",
            "high"
        };

        // -------------------------
        // Level
        // -------------------------
        public static List<string> level = new List<string>()
        {
            "none",
            "1.0",
            "1.1",
            "1.2",
            "1.3",
            "2.0",
            "2.2",
            "2.2",
            "3.0",
            "3.1",
            "3.2",
            "4.0",
            "4.1",
            "4.2",
            "5.0",
            "5.1",
            "5.2"
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
            // None
        }
    }
}
