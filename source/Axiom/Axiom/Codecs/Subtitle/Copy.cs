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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axiom
{
    public class SubtitleCopy
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public static List<SubtitleViewModel.SubtitleCodec> codec = new List<SubtitleViewModel.SubtitleCodec>()
        {
             new SubtitleViewModel.SubtitleCodec()
             {
                 Codec = "copy",
                 Parameters = ""
             }
        };

        public static void Codec_Set()
        {
            // Combine Codec + Parameters
            List<string> codec = new List<string>()
            {
                "-c:s",
                SubtitleCopy.codec.FirstOrDefault()?.Codec,
                SubtitleCopy.codec.FirstOrDefault()?.Parameters
            };

            VM.SubtitleView.Subtitle_Codec = string.Join(" ", codec.Where(s => !string.IsNullOrEmpty(s)));
        }

        // -------------------------
        // Stream
        // -------------------------
        public static List<string> stream = new List<string>()
        {
            "none",
            "all",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
        };



        // ---------------------------------------------------------------------------
        // Controls Behavior
        // ---------------------------------------------------------------------------

        // -------------------------
        // Items Source
        // -------------------------
        public static void Controls_ItemsSource()
        {
            VM.SubtitleView.Subtitle_Stream_Items = stream;
        }

        // -------------------------
        // Selected Items
        // -------------------------
        public static void Controls_Selected()
        {
            // Stream
            VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";
        }

        // -------------------------
        // Checked
        // -------------------------
        public static void Controls_Checked()
        {
            // None
        }

        // -------------------------
        // Unchecked
        // -------------------------
        public static void Controls_Unhecked()
        {
            // None
        }

        // -------------------------
        // Enabled
        // -------------------------
        public static void Controls_Enable()
        {
            // Subtitle Codec
            VM.SubtitleView.Subtitle_Codec_IsEnabled = true;

            // Subtitle Stream
            VM.SubtitleView.Subtitle_Stream_IsEnabled = true;

            // Subtitle List View
            // Controlled in cboSubtitle_Stream_SelectionChanged
        }

        // -------------------------
        // Disabled
        // -------------------------
        public static void Controls_Disable()
        {
            // None
        }


    }
}
