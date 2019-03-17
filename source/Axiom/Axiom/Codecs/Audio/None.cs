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
    public class AudioNone
    {
        // ---------------------------------------------------------------------------
        // Arguments
        // ---------------------------------------------------------------------------

        // -------------------------
        // Codec
        // -------------------------
        public static string codec = "";



        // ---------------------------------------------------------------------------
        // Item Source
        // ---------------------------------------------------------------------------

        // -------------------------
        // Quality
        // -------------------------
        public static List<ViewModel.AudioQuality> quality = new List<ViewModel.AudioQuality>()
        {
             new ViewModel.AudioQuality() { Name = "None", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", NA = ""}
        };

        // -------------------------
        // Channel
        // -------------------------
        public static List<string> channel = new List<string>()
        {
            "Source"
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
        // Item Source
        // -------------------------
        public static void controlsItemSource(ViewModel vm)
        {
            // Quality
            vm.Audio_Quality_Items = quality;

            // Channel
            vm.Audio_Channel_Items = channel;

            // Samplerate
            vm.Audio_SampleRate_Items = sampleRate;

            // Bit Depth
            vm.Audio_BitDepth_Items = bitDepth;
        }

        // -------------------------
        // Selected Items
        // -------------------------
        public static void controlsSelected(ViewModel vm)
        {
            //vm.Audio_Stream_SelectedItem = "all";
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
            vm.Audio_VBR_IsChecked = false;
        }

        // -------------------------
        // Enabled
        // -------------------------
        public static void controlsEnable(ViewModel vm)
        {
            // Bitrate Mode
            vm.Audio_VBR_IsChecked = false;
        }

        // -------------------------
        // Disabled
        // -------------------------
        public static void controlsDisable(ViewModel vm)
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

            // SampleRate
            vm.Audio_SampleRate_IsEnabled = false;

            // Volume
            vm.Audio_Volume_IsEnabled = false;

            // Bit Depth
            vm.Audio_BitDepth_IsEnabled = false;
        }

    }
}
