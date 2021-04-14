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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Controls.Video.Codec
{
    public class MagicYUV : Controls.IVideoCodec
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public ObservableCollection<ViewModel.Video.VideoCodec> codec { get; set; } = new ObservableCollection<ViewModel.Video.VideoCodec>()
        {
            new ViewModel.Video.VideoCodec() { Codec = "magicyuv", Parameters = "-vstrict -2 -pred median" }
        };


        // ---------------------------------------------------------------------------
        // Items Source
        // ---------------------------------------------------------------------------

        // -------------------------
        // Encode Speed
        // -------------------------
        public ObservableCollection<ViewModel.Video.VideoEncodeSpeed> encodeSpeed { get; set; } = new ObservableCollection<ViewModel.Video.VideoEncodeSpeed>()
        {
            new ViewModel.Video.VideoEncodeSpeed() { Name = "none", Command = ""},
        };

        // -------------------------
        // Pixel Format
        // -------------------------
        public ObservableCollection<string> pixelFormat { get; set; } = new ObservableCollection<string>()
        {
            "auto",
            "gbrp",
            "gbrap",
            "yuv420p",
            "yuv422p",
            "yuv444p",
            "yuva444p"
        };

        // -------------------------
        // Quality
        // -------------------------
        public ObservableCollection<ViewModel.Video.VideoQuality> quality { get; set; } = new ObservableCollection<ViewModel.Video.VideoQuality>()
        {
            //new ViewModel.Video.VideoQuality() { Name = "Auto",     CRF = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="", NA = "" },
            new ViewModel.Video.VideoQuality() { Name = "Lossless", CRF = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="", Lossless = "" },
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
                // Lossless
                case "Lossless":
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "1 Pass",
                    };

                    VM.VideoView.Video_Pass_SelectedItem = "1 Pass";
                    VM.VideoView.Video_Pass_IsEnabled = false;
                    VM.VideoView.Video_CRF_IsEnabled = false;
                    break;

                // None
                case "None":
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "auto"
                    };

                    VM.VideoView.Video_Pass_SelectedItem = "auto";
                    VM.VideoView.Video_Pass_IsEnabled = false;
                    VM.VideoView.Video_CRF_IsEnabled = false;
                    break;

                // Presets: Ultra, High, Medium, Low, Sub
                default:
                    VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                    {
                        "1 Pass",
                    };

                    VM.VideoView.Video_Pass_SelectedItem = "1 Pass";
                    VM.VideoView.Video_Pass_IsEnabled = false;
                    VM.VideoView.Video_CRF_IsEnabled = false;
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
            new ViewModel.Video.VideoOptimize() { Name = "None", Tune = "none", Profile = "none", Level = "none", Command = "" },
        };

        // -------------------------
        // Tune
        // -------------------------
        public ObservableCollection<string> tune { get; set; } = new ObservableCollection<string>()
        {
            "none",
        };

        // -------------------------
        // Profile
        // -------------------------
        public ObservableCollection<string> profile { get; set; } = new ObservableCollection<string>()
        {
            "none",
        };

        // -------------------------
        // Level
        // -------------------------
        public ObservableCollection<string> level { get; set; } = new ObservableCollection<string>()
        {
            "none",
        };


        // ---------------------------------------------------------------------------
        // Controls Behavior
        // ---------------------------------------------------------------------------

        // -------------------------
        // Selected Item
        // -------------------------
        public List<ViewModel.Video.Selected> controls_Selected { get; set; } = new List<ViewModel.Video.Selected>()
        {
            new ViewModel.Video.Selected()
            {
                // lossless only
                PixelFormat_Lossless = "yuv444p",
                //PixelFormat = ""
            },
        };

        // -------------------------
        // Expanded
        // -------------------------
        public List<ViewModel.Video.Expanded> controls_Expanded { get; set; } = new List<ViewModel.Video.Expanded>()
        {
            new ViewModel.Video.Expanded() {  Optimize = false },
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
                VBR =       false,
                Optimize =  false
            },
            
            // All other controls are set through Format.Controls.MediaTypeControls()
        };

    }
}
