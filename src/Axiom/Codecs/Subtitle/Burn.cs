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

namespace Controls.Subtitles.Codec
{
    public class Burn : Controls.ISubtitleCodec
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public ObservableCollection<ViewModel.Subtitle.SubtitleCodec> codec { get; set; } = new ObservableCollection<ViewModel.Subtitle.SubtitleCodec>()
        {
            new ViewModel.Subtitle.SubtitleCodec() { Codec = string.Empty, Parameters = string.Empty }
        };

        // -------------------------
        // Stream
        // -------------------------
        public ObservableCollection<string> stream { get; set; } = new ObservableCollection<string>()
        {
            "none",
            "external"
        };


        // ---------------------------------------------------------------------------
        // Controls Behavior
        // ---------------------------------------------------------------------------

        // -------------------------
        // Selected Items
        // -------------------------
        public List<ViewModel.Subtitle.Selected> controls_Selected { get; set; } = new List<ViewModel.Subtitle.Selected>()
        {
            new ViewModel.Subtitle.Selected() {  Stream = "external" },
        };

        // -------------------------
        // Enabled
        // -------------------------
        public List<ViewModel.Subtitle.Enabled> controls_Enabled { get; set; } = new List<ViewModel.Subtitle.Enabled>()
        {
            new ViewModel.Subtitle.Enabled() {  Stream = true },
            // Subtitle List View controlled in cboSubtitle_Stream_SelectionChanged
            
            // All other controls are set through Format.Controls.MediaTypeControls()
        };

    }
}
