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
    public class Vorbis
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public static List<ViewModel.AudioCodec> codec = new List<ViewModel.AudioCodec>()
        {
             new ViewModel.AudioCodec()
             {
                 Codec = "libvorbis", // Determined by SampleRate
                 Parameters = ""
             }
        };

        public static void Codec_Set(ViewModel vm)
        {
            // Combine Codec + Parameters
            List<string> codec = new List<string>()
            {
                "-c:a",
                Vorbis.codec.FirstOrDefault()?.Codec,
                Vorbis.codec.FirstOrDefault()?.Parameters
            };

            vm.Audio_Codec = string.Join(" ", codec.Where(s => !string.IsNullOrEmpty(s)));
        }



        // ---------------------------------------------------------------------------
        // Items Source
        // ---------------------------------------------------------------------------

        // -------------------------
        // Channel
        // -------------------------
        public static List<string> channel = new List<string>()
        {
            "Source",
            "Mono",
            "Stereo",
            "5.1"
        };

        // -------------------------
        // Quality
        // -------------------------
        public static List<ViewModel.AudioQuality> quality = new List<ViewModel.AudioQuality>()
        {
             new ViewModel.AudioQuality() { Name = "Auto",   CBR_BitMode = "-b:a", CBR = "",    VBR_BitMode = "-q:a", VBR = "10",   NA = "192" },
             new ViewModel.AudioQuality() { Name = "500",    CBR_BitMode = "-b:a", CBR = "500", VBR_BitMode = "-q:a", VBR = "10" },
             new ViewModel.AudioQuality() { Name = "320",    CBR_BitMode = "-b:a", CBR = "320", VBR_BitMode = "-q:a", VBR = "9"  },
             new ViewModel.AudioQuality() { Name = "256",    CBR_BitMode = "-b:a", CBR = "256", VBR_BitMode = "-q:a", VBR = "8"  },
             new ViewModel.AudioQuality() { Name = "224",    CBR_BitMode = "-b:a", CBR = "224", VBR_BitMode = "-q:a", VBR = "7"  },
             new ViewModel.AudioQuality() { Name = "192",    CBR_BitMode = "-b:a", CBR = "192", VBR_BitMode = "-q:a", VBR = "6"  },
             new ViewModel.AudioQuality() { Name = "160",    CBR_BitMode = "-b:a", CBR = "160", VBR_BitMode = "-q:a", VBR = "5"  },
             new ViewModel.AudioQuality() { Name = "128",    CBR_BitMode = "-b:a", CBR = "128", VBR_BitMode = "-q:a", VBR = "4"  },
             new ViewModel.AudioQuality() { Name = "96",     CBR_BitMode = "-b:a", CBR = "96",  VBR_BitMode = "-q:a", VBR = "2"  },
             new ViewModel.AudioQuality() { Name = "Custom", CBR_BitMode = "-b:a", CBR = "",    VBR_BitMode = "-q:a", VBR = ""   },
             new ViewModel.AudioQuality() { Name = "Mute",   CBR_BitMode = "",     CBR = "",    VBR_BitMode = "",     VBR = ""   }
        };

        // -------------------------
        // Compression Level
        // -------------------------
        public static List<string> compressionLevel = new List<string>()
        {
            "auto"
        };

        // -------------------------
        // Sample Rate
        // -------------------------
        public static List<ViewModel.AudioSampleRate> sampleRate = new List<ViewModel.AudioSampleRate>()
        {
             new ViewModel.AudioSampleRate() { Name = "auto",     Frequency = "" },
             new ViewModel.AudioSampleRate() { Name = "8k",       Frequency = "8000" },
             new ViewModel.AudioSampleRate() { Name = "11.025k",  Frequency = "11025" },
             new ViewModel.AudioSampleRate() { Name = "12k",      Frequency = "12000" },
             new ViewModel.AudioSampleRate() { Name = "16k",      Frequency = "16000" },
             new ViewModel.AudioSampleRate() { Name = "22.05k",   Frequency = "22050" },
             new ViewModel.AudioSampleRate() { Name = "24k",      Frequency = "24000" },
             new ViewModel.AudioSampleRate() { Name = "32k",      Frequency = "32000" },
             new ViewModel.AudioSampleRate() { Name = "44.1k",    Frequency = "44100" },
             new ViewModel.AudioSampleRate() { Name = "48k",      Frequency = "48000" }
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
            vm.Audio_CompressionLevel_SelectedItem = "auto";
        }

        // -------------------------
        // Checked
        // -------------------------
        public static void Controls_Checked(ViewModel vm)
        {
            // BitRate Mode
            vm.Audio_VBR_IsChecked = true;
        }

        // -------------------------
        // Unchecked
        // -------------------------
        public static void Controls_Unhecked(ViewModel vm)
        {
            // None
        }

        // -------------------------
        // Enabled
        // -------------------------
        public static void Controls_Enable(ViewModel vm)
        {
            // Codec
            vm.Audio_Codec_IsEnabled = true;

            // Stream
            vm.Audio_Stream_IsEnabled = true;

            // Channel
            vm.Audio_Channel_IsEnabled = true;

            // Audio Quality
            vm.Audio_Quality_IsEnabled = true;

            // Audio VBR
            vm.Audio_VBR_IsEnabled = true;

            // SampleRate
            vm.Audio_SampleRate_IsEnabled = true;

            // Volume
            vm.Audio_Volume_IsEnabled = true;

            // Hard Limiter
            vm.Audio_HardLimiter_IsEnabled = true;
        }

        // -------------------------
        // Disabled
        // -------------------------
        public static void Controls_Disable(ViewModel vm)
        {
            // Compression Level
            vm.Audio_CompressionLevel_IsEnabled = false;

            // Bit Depth
            vm.Audio_BitDepth_IsEnabled = false;
        }

    }
}
