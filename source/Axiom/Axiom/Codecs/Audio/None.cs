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
    public class AudioNone
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public static void Codec_Set(ViewModel vm)
        {
            vm.Audio_Codec = string.Empty;
        }



        // ---------------------------------------------------------------------------
        // Items Source
        // ---------------------------------------------------------------------------

        // -------------------------
        // Channel
        // -------------------------
        public static List<string> channel = new List<string>()
        {
            "none"
        };

        // -------------------------
        // Quality
        // -------------------------
        public static List<ViewModel.AudioQuality> quality = new List<ViewModel.AudioQuality>()
        {
             new ViewModel.AudioQuality() { Name = "None", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", NA = ""}
        };

        // -------------------------
        // Compression Level
        // -------------------------
        public static List<string> compressionLevel = new List<string>()
        {
            "none"
        };

        // -------------------------
        // Sample Rate
        // -------------------------
        public static List<ViewModel.AudioSampleRate> sampleRate = new List<ViewModel.AudioSampleRate>()
        {
             new ViewModel.AudioSampleRate() { Name = "auto", Frequency = "" }
        };

        // -------------------------
        // Bit Depth
        // -------------------------
        public static List<ViewModel.AudioBitDepth> bitDepth = new List<ViewModel.AudioBitDepth>()
        {
             new ViewModel.AudioBitDepth() { Name = "auto", Depth = "" }
        };



        // ---------------------------------------------------------------------------
        // Controls Behavior
        // ---------------------------------------------------------------------------

        // -------------------------
        // Items Source
        // -------------------------
        public static void Controls_ItemsSource(ViewModel vm)
        {
            // Channel
            vm.Audio_Channel_Items = channel;

            // Quality
            vm.Audio_Quality_Items = quality;

            // Compression Level
            vm.Audio_CompressionLevel_Items = compressionLevel;

            // Samplerate
            vm.Audio_SampleRate_Items = sampleRate;

            // Bit Depth
            vm.Audio_BitDepth_Items = bitDepth;
        }

        // -------------------------
        // Selected Items
        // -------------------------
        public static void Controls_Selected(ViewModel vm)
        {
            //vm.Audio_Stream_SelectedItem = "all";

            // Compression Level
            vm.Audio_CompressionLevel_SelectedItem = "none";
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
            vm.Audio_VBR_IsChecked = false;
        }

        // -------------------------
        // Enabled
        // -------------------------
        public static void Controls_Enable(ViewModel vm)
        {
            // BitRate Mode
            vm.Audio_VBR_IsChecked = false;
        }

        // -------------------------
        // Disabled
        // -------------------------
        public static void Controls_Disable(ViewModel vm)
        {
            // Codec
            vm.Audio_Codec_IsEnabled = false;

            // Stream
            vm.Audio_Stream_IsEnabled = false;

            // Channel
            vm.Audio_Channel_IsEnabled = false;

            // Audio Quality
            vm.Audio_Quality_IsEnabled = false;

            // VBR Button
            vm.Audio_VBR_IsEnabled = false;

            // Compression Level
            vm.Audio_CompressionLevel_IsEnabled = false;

            // SampleRate
            vm.Audio_SampleRate_IsEnabled = false;

            // Volume
            vm.Audio_Volume_IsEnabled = false;

            // Bit Depth
            vm.Audio_BitDepth_IsEnabled = false;
        }

    }
}
