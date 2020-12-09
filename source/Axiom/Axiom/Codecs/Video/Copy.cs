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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Controls.Video.Codec
{
    public class Copy : Controls.IVideoCodec
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public ObservableCollection<ViewModel.Video.VideoCodec> codec { get; set; } = new ObservableCollection<ViewModel.Video.VideoCodec>()
        {
            new ViewModel.Video.VideoCodec() { Codec = "copy", Parameters = "" }
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
            "auto"
        };

        // -------------------------
        // Quality
        // -------------------------
        public ObservableCollection<ViewModel.Video.VideoQuality> quality { get; set; } = new ObservableCollection<ViewModel.Video.VideoQuality>()
        {
            new ViewModel.Video.VideoQuality() { Name = "Auto",   CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="", NA = "" },
            new ViewModel.Video.VideoQuality() { Name = "Ultra",  CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
            new ViewModel.Video.VideoQuality() { Name = "High",   CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
            new ViewModel.Video.VideoQuality() { Name = "Medium", CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
            new ViewModel.Video.VideoQuality() { Name = "Low",    CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
            new ViewModel.Video.VideoQuality() { Name = "Sub",    CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
            new ViewModel.Video.VideoQuality() { Name = "Custom", CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" }
        };

        // -------------------------
        // Pass
        // -------------------------
        public void EncodingPass()
        {
            // Items Source
            VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
            {
                "auto"
            };

            VM.VideoView.Video_Pass_SelectedItem = "auto";
            VM.VideoView.Video_Pass_IsEnabled = false;
            Controls.passUserSelected = false;

            VM.VideoView.Video_CRF_IsEnabled = false;
            VM.VideoView.Video_CRF_Text = string.Empty;
            VM.VideoView.Video_BitRate_Text = string.Empty;
            VM.VideoView.Video_MinRate_Text = string.Empty;
            VM.VideoView.Video_MaxRate_Text = string.Empty;
            VM.VideoView.Video_BufSize_Text = string.Empty;
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
            "none"
        };

        // -------------------------
        // Profile
        // -------------------------
        public ObservableCollection<string> profile { get; set; } = new ObservableCollection<string>()
        {
            "none"
        };

        // -------------------------
        // Level
        // -------------------------
        public ObservableCollection<string> level { get; set; } = new ObservableCollection<string>()
        {
            "none"
        };


        // ---------------------------------------------------------------------------
        // Controls Behavior
        // ---------------------------------------------------------------------------

        // -------------------------
        // Selected Items
        // -------------------------
        public List<ViewModel.Video.Selected> controls_Selected { get; set; } = new List<ViewModel.Video.Selected>()
        {
            new ViewModel.Video.Selected() {  EncodeSpeed =         "none" },
            new ViewModel.Video.Selected() {  HWAccel =             "Off" },
            new ViewModel.Video.Selected() {  PixelFormat =         "auto" },
            new ViewModel.Video.Selected() {  FPS =                 "auto" },
            new ViewModel.Video.Selected() {  Speed =               "auto" },
            new ViewModel.Video.Selected() {  Vsync =               "off" },
            new ViewModel.Video.Selected() {  Scale =               "Source" },
            new ViewModel.Video.Selected() {  ColorRange =          "auto" },
            new ViewModel.Video.Selected() {  ColorSpace =          "auto" },
            new ViewModel.Video.Selected() {  ColorPrimaries =      "auto" },
            new ViewModel.Video.Selected() {  ColorTransferChar =   "auto" },
            new ViewModel.Video.Selected() {  ColorMatrix =         "auto" },
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
        // -------------------------
        public List<ViewModel.Video.Enabled> controls_Enabled { get; set; } = new List<ViewModel.Video.Enabled>()
        {
            new ViewModel.Video.Enabled() {  Codec =             true }, // Override Format Controls Bypass
            new ViewModel.Video.Enabled() {  EncodeSpeed =       false },
            new ViewModel.Video.Enabled() {  HWAccel =           false },
            new ViewModel.Video.Enabled() {  Quality =           false },
            new ViewModel.Video.Enabled() {  VBR =               false },
            new ViewModel.Video.Enabled() {  PixelFormat =       false },
            new ViewModel.Video.Enabled() {  FPS =               false },
            new ViewModel.Video.Enabled() {  Speed =             false },
            new ViewModel.Video.Enabled() {  Vsync =             false },
            new ViewModel.Video.Enabled() {  Optimize =          false },
            new ViewModel.Video.Enabled() {  Scale =             false },
            new ViewModel.Video.Enabled() {  Scaling =           false },
            new ViewModel.Video.Enabled() {  ScreenFormat =      false },
            //new ViewModel.Video.Enabled() {  AspectRatio =       true }, // Works with Copy
            new ViewModel.Video.Enabled() {  Crop =              false },
            new ViewModel.Video.Enabled() {  ColorRange =        false },
            new ViewModel.Video.Enabled() {  ColorSpace =        false },
            new ViewModel.Video.Enabled() {  ColorPrimaries =    false },
            new ViewModel.Video.Enabled() {  ColorTransferChar = false },
            new ViewModel.Video.Enabled() {  ColorMatrix =       false },
        };

    }
}
