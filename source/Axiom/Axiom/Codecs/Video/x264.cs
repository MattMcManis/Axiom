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
    public class x264
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public static List<ViewModel.VideoCodec> codec = new List<ViewModel.VideoCodec>()
        {
             new ViewModel.VideoCodec()
             {
                 Codec = "libx264",
                 Parameters = ""
             }
        };

        public static void Codec_Set(ViewModel vm)
        {
            // Combine Codec + Parameters
            List<string> codec = new List<string>()
            {
                "-c:v",
                x264.codec.FirstOrDefault()?.Codec,
                x264.codec.FirstOrDefault()?.Parameters
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
             new ViewModel.VideoEncodeSpeed() { Name = "none",       Command = ""},
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
            "gray",
            "gray10le",
            "nv12",
            "nv16",
            "nv20le",
            "nv21",
            "yuv420p",
            "yuv420p10le",
            "yuv422p",
            "yuv422p10le",
            "yuv444p",
            "yuv444p10le",
            "yuvj420p",
            "yuvj422p",
            "yuvj444p",
        };

        // -------------------------
        // Quality
        // -------------------------
        public static List<ViewModel.VideoQuality> quality = new List<ViewModel.VideoQuality>()
        {
             new ViewModel.VideoQuality() { Name = "Auto",      CRF = "",   CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      MinRate = "", MaxRate = "", BufSize ="", NA = "3000K" },
             new ViewModel.VideoQuality() { Name = "Lossless",  CRF = "",   CBR_BitMode = "",     CBR = "",      VBR_BitMode = "",     VBR = "",      MinRate = "", MaxRate = "", BufSize ="", Lossless = "-qp 0" },
             new ViewModel.VideoQuality() { Name = "Ultra",     CRF = "16", CBR_BitMode = "-b:v", CBR = "5000K", VBR_BitMode = "-q:v", VBR = "5000K", MinRate = "", MaxRate = "", BufSize ="" },
             new ViewModel.VideoQuality() { Name = "High",      CRF = "20", CBR_BitMode = "-b:v", CBR = "2500K", VBR_BitMode = "-q:v", VBR = "2500K", MinRate = "", MaxRate = "", BufSize ="" },
             new ViewModel.VideoQuality() { Name = "Medium",    CRF = "25", CBR_BitMode = "-b:v", CBR = "1300K", VBR_BitMode = "-q:v", VBR = "1300K", MinRate = "", MaxRate = "", BufSize ="" },
             new ViewModel.VideoQuality() { Name = "Low",       CRF = "35", CBR_BitMode = "-b:v", CBR = "600K",  VBR_BitMode = "-q:v", VBR = "600K",  MinRate = "", MaxRate = "", BufSize ="" },
             new ViewModel.VideoQuality() { Name = "Sub",       CRF = "45", CBR_BitMode = "-b:v", CBR = "250K",  VBR_BitMode = "-q:v", VBR = "250K",  MinRate = "", MaxRate = "", BufSize ="" },
             new ViewModel.VideoQuality() { Name = "Custom",    CRF = "",   CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v",     VBR = "",  MinRate = "", MaxRate = "", BufSize ="" }
        };

        // -------------------------
        // Pass
        // -------------------------
        public static void EncodingPass(ViewModel vm)
        {
            // -------------------------
            // Quality
            // -------------------------
            // Auto
            if (vm.Video_Quality_SelectedItem == "Auto")
            {
                vm.Video_Pass_Items = new List<string>()
                {
                    "2 Pass"
                };

                vm.Video_Pass_SelectedItem = "2 Pass";
                vm.Video_Pass_IsEnabled = false;
                VideoControls.passUserSelected = false;

                vm.Video_CRF_IsEnabled = false;
            }
            // Lossless
            else if (vm.Video_Quality_SelectedItem == "Lossless")
            {
                vm.Video_Pass_Items = new List<string>()
                {
                    "1 Pass",
                    "2 Pass"
                };

                vm.Video_Pass_SelectedItem = "2 Pass";
                vm.Video_Pass_IsEnabled = true;
                vm.Video_CRF_IsEnabled = false;
            }
            // Custom
            else if (vm.Video_Quality_SelectedItem == "Custom")
            {
                vm.Video_Pass_Items = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                vm.Video_Pass_IsEnabled = true;
                vm.Video_CRF_IsEnabled = true;
            }
            // None
            else if (vm.Video_Quality_SelectedItem == "None")
            {
                vm.Video_Pass_Items = new List<string>()
                {
                    "auto"
                };

                vm.Video_Pass_IsEnabled = false;
                vm.Video_CRF_IsEnabled = false;
            }
            // Presets: Ultra, High, Medium, Low, Sub
            else
            {
                vm.Video_Pass_Items = new List<string>()
                {
                    "CRF",
                    "1 Pass",
                    "2 Pass"
                };

                vm.Video_Pass_IsEnabled = true;
                vm.Video_CRF_IsEnabled = false;

                // Default to CRF
                if (VideoControls.passUserSelected == false)
                {
                    vm.Video_Pass_SelectedItem = "CRF";
                    VideoControls.passUserSelected = true;
                }
            }

            // Clear TextBoxes
            if (vm.Video_Quality_SelectedItem == "Auto" ||
                vm.Video_Quality_SelectedItem == "Lossless" ||
                vm.Video_Quality_SelectedItem == "Custom" ||
                vm.Video_Quality_SelectedItem == "None"
                )
            {
                vm.Video_CRF_Text = string.Empty;
                vm.Video_BitRate_Text = string.Empty;
                vm.Video_MinRate_Text = string.Empty;
                vm.Video_MaxRate_Text = string.Empty;
                vm.Video_BufSize_Text = string.Empty;
            }

        }


        // -------------------------
        // Optimize
        // -------------------------
        public static List<ViewModel.VideoOptimize> optimize = new List<ViewModel.VideoOptimize>()
        {
            new ViewModel.VideoOptimize() { Name = "None",      Tune = "none",      Profile = "none",     Level = "none", Command = "" },
            new ViewModel.VideoOptimize() { Name = "Custom",    Tune = "none",      Profile = "none",     Level = "none", Command = "" },
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
            vm.Video_PixelFormat_SelectedItem = "yuv420p";

            // Framerate
            vm.Video_FPS_SelectedItem = "auto";
        }


        // -------------------------
        // Expanded
        // -------------------------
        public static void Controls_Expanded(ViewModel vm)
        {
            vm.Video_Optimize_IsExpanded = true;
        }

        // -------------------------
        // Collapsed
        // -------------------------
        public static void Controls_Collapsed(ViewModel vm)
        {
            // None
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
        public static void Controls_Disable(ViewModel vm)
        {
            // None
        }
    }
}
