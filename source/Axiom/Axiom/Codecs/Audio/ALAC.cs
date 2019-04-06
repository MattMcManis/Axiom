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
    public class ALAC
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public static List<AudioView.AudioCodec> codec = new List<AudioView.AudioCodec>()
        {
             new AudioView.AudioCodec()
             {
                 Codec = "alac",
                 Parameters = ""
             }
        };

        public static void Codec_Set()
        {
            // Combine Codec + Parameters
            List<string> codec = new List<string>()
            {
                "-c:a",
                ALAC.codec.FirstOrDefault()?.Codec,
                ALAC.codec.FirstOrDefault()?.Parameters
            };

            AudioView.vm.Audio_Codec = string.Join(" ", codec.Where(s => !string.IsNullOrEmpty(s)));
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
        public static List<AudioView.AudioQuality> quality = new List<AudioView.AudioQuality>()
        {
             new AudioView.AudioQuality() { Name = "Auto",     CBR_BitMode = "",     CBR = "",    VBR_BitMode = "", VBR = "", NA = "" },
             new AudioView.AudioQuality() { Name = "Lossless", CBR_BitMode = "",     CBR = "",    VBR_BitMode = "", VBR = ""   },
             new AudioView.AudioQuality() { Name = "320",      CBR_BitMode = "-b:a", CBR = "320", VBR_BitMode = "", VBR = ""   },
             new AudioView.AudioQuality() { Name = "256",      CBR_BitMode = "-b:a", CBR = "256", VBR_BitMode = "", VBR = ""   },
             new AudioView.AudioQuality() { Name = "224",      CBR_BitMode = "-b:a", CBR = "224", VBR_BitMode = "", VBR = ""   },
             new AudioView.AudioQuality() { Name = "192",      CBR_BitMode = "-b:a", CBR = "192", VBR_BitMode = "", VBR = ""   },
             new AudioView.AudioQuality() { Name = "160",      CBR_BitMode = "-b:a", CBR = "160", VBR_BitMode = "", VBR = ""   },
             new AudioView.AudioQuality() { Name = "128",      CBR_BitMode = "-b:a", CBR = "128", VBR_BitMode = "", VBR = ""   },
             new AudioView.AudioQuality() { Name = "96",       CBR_BitMode = "-b:a", CBR = "96",  VBR_BitMode = "", VBR = ""   },
             new AudioView.AudioQuality() { Name = "Custom",   CBR_BitMode = "-b:a", CBR = "",    VBR_BitMode = "", VBR = ""   },
             new AudioView.AudioQuality() { Name = "Mute",     CBR_BitMode = "",     CBR = "",    VBR_BitMode = "", VBR = ""   }
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
        public static List<AudioView.AudioSampleRate> sampleRate = new List<AudioView.AudioSampleRate>()
        {
             new AudioView.AudioSampleRate() { Name = "auto",     Frequency = "" },
             new AudioView.AudioSampleRate() { Name = "8k",       Frequency = "8000" },
             new AudioView.AudioSampleRate() { Name = "11.025k",  Frequency = "11025" },
             new AudioView.AudioSampleRate() { Name = "12k",      Frequency = "12000" },
             new AudioView.AudioSampleRate() { Name = "16k",      Frequency = "16000" },
             new AudioView.AudioSampleRate() { Name = "22.05k",   Frequency = "22050" },
             new AudioView.AudioSampleRate() { Name = "24k",      Frequency = "24000" },
             new AudioView.AudioSampleRate() { Name = "32k",      Frequency = "32000" },
             new AudioView.AudioSampleRate() { Name = "44.1k",    Frequency = "44100" },
             new AudioView.AudioSampleRate() { Name = "48k",      Frequency = "48000" },
             new AudioView.AudioSampleRate() { Name = "64k",      Frequency = "64000" },
             new AudioView.AudioSampleRate() { Name = "88.2k",    Frequency = "88200" },
             new AudioView.AudioSampleRate() { Name = "96k",      Frequency = "96000" }
        };

        // -------------------------
        // Bit Depth
        // -------------------------
        public static List<AudioView.AudioBitDepth> bitDepth = new List<AudioView.AudioBitDepth>()
        {
             new AudioView.AudioBitDepth() { Name = "auto", Depth = "" },
             new AudioView.AudioBitDepth() { Name = "16",   Depth = "-sample_fmt s16p" },
             new AudioView.AudioBitDepth() { Name = "32",   Depth = "-sample_fmt s32p" }
        };



        // ---------------------------------------------------------------------------
        // Controls Behavior
        // ---------------------------------------------------------------------------

        // -------------------------
        // Items Source
        // -------------------------
        public static void Controls_ItemsSource()
        {
            // Channel
            AudioView.vm.Audio_Channel_Items = channel;

            // Quality
            AudioView.vm.Audio_Quality_Items = quality;

            // Compression Level
            AudioView.vm.Audio_CompressionLevel_Items = compressionLevel;

            // Samplerate
            AudioView.vm.Audio_SampleRate_Items = sampleRate;

            // Bit Depth
            AudioView.vm.Audio_BitDepth_Items = bitDepth;
        }

        // -------------------------
        // Selected Items
        // -------------------------
        public static void Controls_Selected()
        {
            //AudioView.vm.Audio_Stream_SelectedItem = "all";

            // Compression Level
            AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
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
            // BitRate Mode
            AudioView.vm.Audio_VBR_IsChecked = false;
        }

        // -------------------------
        // Enabled
        // -------------------------
        public static void Controls_Enable()
        {
            // Audio Codec
            AudioView.vm.Audio_Codec_IsEnabled = true;

            // Stream
            AudioView.vm.Audio_Stream_IsEnabled = true;

            // Channel
            AudioView.vm.Audio_Channel_IsEnabled = true;

            // Audio Quality
            AudioView.vm.Audio_Quality_IsEnabled = true;

            // SampleRate
            AudioView.vm.Audio_SampleRate_IsEnabled = true;

            // Bit Depth
            AudioView.vm.Audio_BitDepth_IsEnabled = true;

            // Volume
            AudioView.vm.Audio_Volume_IsEnabled = true;

            // Hard Limiter
            AudioView.vm.Audio_HardLimiter_IsEnabled = true;
        }

        // -------------------------
        // Disabled
        // -------------------------
        public static void Controls_Disable()
        {
            // VBR Button
            AudioView.vm.Audio_VBR_IsEnabled = false;

            // Compression Level
            AudioView.vm.Audio_CompressionLevel_IsEnabled = false;
        }
    }
}
