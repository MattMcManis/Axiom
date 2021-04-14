/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2021 Matt McManis
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

using Axiom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Controls.Video.Codec
{
    public class HEVC_QSV : Controls.IVideoCodec
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public ObservableCollection<ViewModel.Video.VideoCodec> codec { get; set; } = new ObservableCollection<ViewModel.Video.VideoCodec>()
        {
            new ViewModel.Video.VideoCodec() { Codec = "hevc_qsv", Parameters = "" }
        };


        // ---------------------------------------------------------------------------
        // Items Source
        // ---------------------------------------------------------------------------

        // -------------------------
        // Encode Speed
        // -------------------------
        public ObservableCollection<ViewModel.Video.VideoEncodeSpeed> encodeSpeed { get; set; } = new ObservableCollection<ViewModel.Video.VideoEncodeSpeed>()
        {
            new ViewModel.Video.VideoEncodeSpeed() { Name = "none",       Command = ""},
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Very Slow",  Command = "-preset veryslow" },
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Slower",     Command = "-preset slower" },
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Slow",       Command = "-preset slow" },
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Medium",     Command = "-preset medium" },
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Fast",       Command = "-preset fast" },
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Faster",     Command = "-preset faster" },
            new ViewModel.Video.VideoEncodeSpeed() { Name = "Very Fast",  Command = "-preset veryfast" }
        };

        // -------------------------
        // Pixel Format
        // -------------------------
        public ObservableCollection<string> pixelFormat { get; set; } = new ObservableCollection<string>()
        {
            "auto",
            "nv12",
            "p010le",
            "qsv"
        };

        // -------------------------
        // Quality
        // -------------------------
        public ObservableCollection<ViewModel.Video.VideoQuality> quality { get; set; } = new ObservableCollection<ViewModel.Video.VideoQuality>()
        {
            new ViewModel.Video.VideoQuality() { Name = "Auto",      CRF = "",   CRF_HWAccel_Intel_QSV = "",   CRF_HWAccel_NVIDIA_NVENC = "",   CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      MinRate = "", MaxRate = "", BufSize ="", NA = "3000K" },
            new ViewModel.Video.VideoQuality() { Name = "Lossless",  CRF = "",   CRF_HWAccel_Intel_QSV = "",   CRF_HWAccel_NVIDIA_NVENC = "",   CBR_BitMode = "",     CBR = "",      VBR_BitMode = "",     VBR = "",      MinRate = "", MaxRate = "", BufSize ="", Lossless = "-qp 0" },
            new ViewModel.Video.VideoQuality() { Name = "Ultra",     CRF = "16", CRF_HWAccel_Intel_QSV = "18", CRF_HWAccel_NVIDIA_NVENC = "18", CBR_BitMode = "-b:v", CBR = "5000K", VBR_BitMode = "-q:v", VBR = "5000K", MinRate = "", MaxRate = "", BufSize ="" },
            new ViewModel.Video.VideoQuality() { Name = "High",      CRF = "20", CRF_HWAccel_Intel_QSV = "22", CRF_HWAccel_NVIDIA_NVENC = "22", CBR_BitMode = "-b:v", CBR = "2800K", VBR_BitMode = "-q:v", VBR = "2800K", MinRate = "", MaxRate = "", BufSize ="" },
            new ViewModel.Video.VideoQuality() { Name = "Medium",    CRF = "25", CRF_HWAccel_Intel_QSV = "27", CRF_HWAccel_NVIDIA_NVENC = "27", CBR_BitMode = "-b:v", CBR = "1500K", VBR_BitMode = "-q:v", VBR = "1500K", MinRate = "", MaxRate = "", BufSize ="" },
            new ViewModel.Video.VideoQuality() { Name = "Low",       CRF = "35", CRF_HWAccel_Intel_QSV = "37", CRF_HWAccel_NVIDIA_NVENC = "36", CBR_BitMode = "-b:v", CBR = "600K",  VBR_BitMode = "-q:v", VBR = "600K",  MinRate = "", MaxRate = "", BufSize ="" },
            new ViewModel.Video.VideoQuality() { Name = "Sub",       CRF = "45", CRF_HWAccel_Intel_QSV = "47", CRF_HWAccel_NVIDIA_NVENC = "48", CBR_BitMode = "-b:v", CBR = "250K",  VBR_BitMode = "-q:v", VBR = "250K",  MinRate = "", MaxRate = "", BufSize ="" },
            new ViewModel.Video.VideoQuality() { Name = "Custom",    CRF = "",   CRF_HWAccel_Intel_QSV = "",   CRF_HWAccel_NVIDIA_NVENC = "",   CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      MinRate = "", MaxRate = "", BufSize ="" }
        };

        // -------------------------
        // Pass
        // -------------------------
        public void EncodingPass()
        {
            // -------------------------
            // Quality
            // -------------------------
            switch (VM.VideoView.Video_Quality_SelectedItem)
            {
                // Auto
                case "Auto":
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "2 Pass"
                    };

                    VM.VideoView.Video_Pass_SelectedItem = "2 Pass";
                    VM.VideoView.Video_Pass_IsEnabled = false;
                    Controls.passUserSelected = false;
                    VM.VideoView.Video_CRF_IsEnabled = false;
                    break;

                // Lossless
                case "Lossless":
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "1 Pass"
                    };

                    VM.VideoView.Video_Pass_SelectedItem = "1 Pass";
                    VM.VideoView.Video_Pass_IsEnabled = true;
                    VM.VideoView.Video_CRF_IsEnabled = false;
                    break;

                // Custom
                case "Custom":
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "CRF",
                        "1 Pass",
                        "2 Pass"
                    };

                    VM.VideoView.Video_Pass_IsEnabled = true;
                    VM.VideoView.Video_CRF_IsEnabled = true;
                    break;

                // None
                case "None":
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "auto"
                    };

                    VM.VideoView.Video_Pass_IsEnabled = false;
                    VM.VideoView.Video_CRF_IsEnabled = false;
                    break;

                // Presets: Ultra, High, Medium, Low, Sub
                default:
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "CRF",
                        "1 Pass",
                        "2 Pass"
                    };

                    VM.VideoView.Video_Pass_IsEnabled = true;
                    VM.VideoView.Video_CRF_IsEnabled = false;

                    // Default to CRF
                    if (Controls.passUserSelected == false)
                    {
                        VM.VideoView.Video_Pass_SelectedItem = "CRF";
                        Controls.passUserSelected = true;
                    }
                    break;
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
        public ObservableCollection<ViewModel.Video.VideoOptimize> optimize { get; set; } = new ObservableCollection<ViewModel.Video.VideoOptimize>()
        {
            new ViewModel.Video.VideoOptimize() { Name = "None",   Tune = "none", Profile = "none",   Level = "none", Command = "" },
            new ViewModel.Video.VideoOptimize() { Name = "Custom", Tune = "none", Profile = "none",   Level = "none", Command = "" },
            new ViewModel.Video.VideoOptimize() { Name = "Web",    Tune = "none", Profile = "main",   Level = "3.1",  Command = "-movflags +faststart" },
            new ViewModel.Video.VideoOptimize() { Name = "PC HD",  Tune = "none", Profile = "main10", Level = "5.1",  Command = "" },
            new ViewModel.Video.VideoOptimize() { Name = "UHD",    Tune = "none", Profile = "main10", Level = "5.1",  Command = "-HEVC_QSV-params " + MainWindow.WrapWithQuotes("colorprim=bt2020:transfer=bt2020:colormatrix=bt2020:colorspace=bt2020") },
        };

        // -------------------------
        // Tune
        // -------------------------
        public ObservableCollection<string> tune { get; set; } = new ObservableCollection<string>()
        {
            "none",
            "animation",
            "psnr",
            "ssim",
            "grain",
            "fastdecode",
            "zerolatency"
        };

        // -------------------------
        // Profile
        // -------------------------
        public ObservableCollection<string> profile { get; set; } = new ObservableCollection<string>()
        {
            "none",
            "main",
            "main10",
            "mainsp",
            "rext"
        };

        // -------------------------
        // Level
        // -------------------------
        public ObservableCollection<string> level { get; set; } = new ObservableCollection<string>()
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
        // Selected Items
        // -------------------------
        public List<ViewModel.Video.Selected> controls_Selected { get; set; } = new List<ViewModel.Video.Selected>()
        {
            new ViewModel.Video.Selected()
            {
                PixelFormat_Lossless = "auto",
                PixelFormat = "yuv420p"
            },
        };

        // -------------------------
        // Expanded
        // -------------------------
        public List<ViewModel.Video.Expanded> controls_Expanded { get; set; } = new List<ViewModel.Video.Expanded>()
        {
            new ViewModel.Video.Expanded() {  Optimize = true },
        };

        // -------------------------
        // Checked
        // -------------------------
        public List<ViewModel.Video.Checked> controls_Checked { get; set; } = new List<ViewModel.Video.Checked>()
        {
            new ViewModel.Video.Checked() {  VBR = false },
        };

        // -------------------------
        // Enabled
        // -------------------------
        public List<ViewModel.Video.Enabled> controls_Enabled { get; set; } = new List<ViewModel.Video.Enabled>()
        {
            new ViewModel.Video.Enabled()
            {
                Quality =   true,
                VBR =       true,
                Optimize =  true
            },
            
            // All other controls are set through Format.Controls.MediaTypeControls()
        };

    }
}
