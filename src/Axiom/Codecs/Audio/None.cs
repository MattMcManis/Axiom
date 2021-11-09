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

namespace Controls.Audio.Codec
{
    public class None : Controls.IAudioCodec
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public ObservableCollection<ViewModel.Audio.AudioCodec> codec { get; set; } = new ObservableCollection<ViewModel.Audio.AudioCodec>()
        {
            new ViewModel.Audio.AudioCodec() { Codec = string.Empty, Parameters = string.Empty }
        };

        // ---------------------------------------------------------------------------
        // Items Source
        // ---------------------------------------------------------------------------

        // -------------------------
        // Stream
        // -------------------------
        // Doesn't Change, List Source in ViewModel

        // -------------------------
        // Channel
        // -------------------------
        public ObservableCollection<string> channel { get; set; } = new ObservableCollection<string>()
        {
            "none"
        };

        // -------------------------
        // Quality
        // -------------------------
        public ObservableCollection<ViewModel.Audio.AudioQuality> quality { get; set; } = new ObservableCollection<ViewModel.Audio.AudioQuality>()
        {
            new ViewModel.Audio.AudioQuality() { Name = "None", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", NA = ""}
        };

        // -------------------------
        // Compression Level
        // -------------------------
        public ObservableCollection<string> compressionLevel { get; set; } = new ObservableCollection<string>()
        {
            "none"
        };

        // -------------------------
        // Sample Rate
        // -------------------------
        public ObservableCollection<ViewModel.Audio.AudioSampleRate> sampleRate { get; set; } = new ObservableCollection<ViewModel.Audio.AudioSampleRate>()
        {
            new ViewModel.Audio.AudioSampleRate() { Name = "auto", Frequency = "" }
        };

        // -------------------------
        // Bit Depth
        // -------------------------
        public ObservableCollection<ViewModel.Audio.AudioBitDepth> bitDepth { get; set; } = new ObservableCollection<ViewModel.Audio.AudioBitDepth>()
        {
            new ViewModel.Audio.AudioBitDepth() { Name = "auto", Depth = "" }
        };


        // ---------------------------------------------------------------------------
        // Controls Behavior
        // ---------------------------------------------------------------------------

        // -------------------------
        // Selected Items
        // -------------------------
        public List<ViewModel.Audio.Selected> controls_Selected { get; set; } = new List<ViewModel.Audio.Selected>()
        {
            new ViewModel.Audio.Selected() {  CompressionLevel = "none" },
        };

        // -------------------------
        // Checked
        // -------------------------
        public List<ViewModel.Audio.Checked> controls_Checked { get; set; } = new List<ViewModel.Audio.Checked>()
        {
            new ViewModel.Audio.Checked() {  VBR = false },
        };

        // -------------------------
        // Enabled
        // -------------------------
        public List<ViewModel.Audio.Enabled> controls_Enabled { get; set; } = new List<ViewModel.Audio.Enabled>()
        {
            new ViewModel.Audio.Enabled()
            {
                Codec =            true,
                Stream =           false,
                Channel =          false,
                Quality =          false,
                CompressionLevel = false,
                VBR =              false,
                SampleRate =       false,
                BitDepth =         false,
                Volume =           false,
                HardLimiter =      false
            },
            
            // All other controls are set through Format.Controls.MediaTypeControls()
        };

    }

}
