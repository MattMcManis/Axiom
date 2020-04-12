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
    public class PCM
    {
        // ---------------------------------------------------------------------------
        // Codec
        // ---------------------------------------------------------------------------
        public static List<AudioViewModel.AudioCodec> codec = new List<AudioViewModel.AudioCodec>()
        {
             new AudioViewModel.AudioCodec()
             {
                 Codec = "pcm_s24le", // Determined by SampleRate
                 Parameters = ""
             }
        };

        public static void Codec_Set()
        {
            //string codec = string.Empty;

            // auto
            if (VM.AudioView.Audio_BitDepth_SelectedItem == "auto")
            {
                PCM.codec.FirstOrDefault().Codec = "pcm_s24le";
            }
            // 8
            else if (VM.AudioView.Audio_BitDepth_SelectedItem == "8")
            {
                PCM.codec.FirstOrDefault().Codec = "pcm_u8";
            }
            // 16
            else if (VM.AudioView.Audio_BitDepth_SelectedItem == "16")
            {
                PCM.codec.FirstOrDefault().Codec = "pcm_s16le";
            }
            // 24
            else if (VM.AudioView.Audio_BitDepth_SelectedItem == "24")
            {
                PCM.codec.FirstOrDefault().Codec = "pcm_s24le";
            }
            // 32
            else if (VM.AudioView.Audio_BitDepth_SelectedItem == "32")
            {
                PCM.codec.FirstOrDefault().Codec = "pcm_f32le";
            }
            // 64
            else if (VM.AudioView.Audio_BitDepth_SelectedItem == "64")
            {
                PCM.codec.FirstOrDefault().Codec = "pcm_f64le";
            }

            // Combine Codec + Parameters
            List<string> codec_parameters = new List<string>()
            {
                "-c:a",
                PCM.codec.FirstOrDefault()?.Codec,
                PCM.codec.FirstOrDefault()?.Parameters
            };

            VM.AudioView.Audio_Codec = string.Join(" ", codec_parameters.Where(s => !string.IsNullOrEmpty(s)));
        }



        // ---------------------------------------------------------------------------
        // Items Source
        // ---------------------------------------------------------------------------

        // -------------------------
        // Stream
        // -------------------------
        public static string stream = "all";

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
        public static List<AudioViewModel.AudioQuality> quality = new List<AudioViewModel.AudioQuality>()
        {
             new AudioViewModel.AudioQuality() { Name = "Auto",     CBR_BitMode = "-b:a", CBR = "",    VBR_BitMode = "", VBR = "", NA = "" },
             new AudioViewModel.AudioQuality() { Name = "Lossless", CBR_BitMode = "",     CBR = "",    VBR_BitMode = "", VBR = ""   },
             new AudioViewModel.AudioQuality() { Name = "320",      CBR_BitMode = "-b:a", CBR = "320", VBR_BitMode = "", VBR = ""   },
             new AudioViewModel.AudioQuality() { Name = "256",      CBR_BitMode = "-b:a", CBR = "256", VBR_BitMode = "", VBR = ""   },
             new AudioViewModel.AudioQuality() { Name = "224",      CBR_BitMode = "-b:a", CBR = "224", VBR_BitMode = "", VBR = ""   },
             new AudioViewModel.AudioQuality() { Name = "192",      CBR_BitMode = "-b:a", CBR = "192", VBR_BitMode = "", VBR = ""   },
             new AudioViewModel.AudioQuality() { Name = "160",      CBR_BitMode = "-b:a", CBR = "160", VBR_BitMode = "", VBR = ""   },
             new AudioViewModel.AudioQuality() { Name = "128",      CBR_BitMode = "-b:a", CBR = "128", VBR_BitMode = "", VBR = ""   },
             new AudioViewModel.AudioQuality() { Name = "96",       CBR_BitMode = "-b:a", CBR = "96",  VBR_BitMode = "", VBR = ""   },
             new AudioViewModel.AudioQuality() { Name = "Custom",   CBR_BitMode = "-b:a", CBR = "",    VBR_BitMode = "", VBR = ""   },
             new AudioViewModel.AudioQuality() { Name = "Mute",     CBR_BitMode = "",     CBR = "",    VBR_BitMode = "", VBR = ""   }
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
        public static List<AudioViewModel.AudioSampleRate> sampleRate = new List<AudioViewModel.AudioSampleRate>()
        {
             new AudioViewModel.AudioSampleRate() { Name = "auto",     Frequency = "" },
             new AudioViewModel.AudioSampleRate() { Name = "8k",       Frequency = "8000" },
             new AudioViewModel.AudioSampleRate() { Name = "11.025k",  Frequency = "11025" },
             new AudioViewModel.AudioSampleRate() { Name = "12k",      Frequency = "12000" },
             new AudioViewModel.AudioSampleRate() { Name = "16k",      Frequency = "16000" },
             new AudioViewModel.AudioSampleRate() { Name = "22.05k",   Frequency = "22050" },
             new AudioViewModel.AudioSampleRate() { Name = "24k",      Frequency = "24000" },
             new AudioViewModel.AudioSampleRate() { Name = "32k",      Frequency = "32000" },
             new AudioViewModel.AudioSampleRate() { Name = "44.1k",    Frequency = "44100" },
             new AudioViewModel.AudioSampleRate() { Name = "48k",      Frequency = "48000" },
             new AudioViewModel.AudioSampleRate() { Name = "64k",      Frequency = "64000" },
             new AudioViewModel.AudioSampleRate() { Name = "88.2k",    Frequency = "88200" },
             new AudioViewModel.AudioSampleRate() { Name = "96k",      Frequency = "96000" }
        };

        // -------------------------
        // Bit Depth
        // -------------------------
        public static List<AudioViewModel.AudioBitDepth> bitDepth = new List<AudioViewModel.AudioBitDepth>()
        {
             new AudioViewModel.AudioBitDepth() { Name = "auto", Depth = "" },
             new AudioViewModel.AudioBitDepth() { Name = "8",    Depth = "" },
             new AudioViewModel.AudioBitDepth() { Name = "16",   Depth = "" },
             new AudioViewModel.AudioBitDepth() { Name = "24",   Depth = "" },
             new AudioViewModel.AudioBitDepth() { Name = "32",   Depth = "" },
             new AudioViewModel.AudioBitDepth() { Name = "64",   Depth = "" }
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
            VM.AudioView.Audio_Channel_Items = channel;

            // Quality
            VM.AudioView.Audio_Quality_Items = quality;

            // Compression Level
            VM.AudioView.Audio_CompressionLevel_Items = compressionLevel;

            // Samplerate
            VM.AudioView.Audio_SampleRate_Items = sampleRate;

            // Bit Depth
            VM.AudioView.Audio_BitDepth_Items = bitDepth;
        }

        // -------------------------
        // Selected Items
        // -------------------------
        public static void Controls_Selected()
        {
            //VM.AudioView.Audio_Stream_SelectedItem = "all";

            // Compression Level
            VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
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
            VM.AudioView.Audio_VBR_IsChecked = false;
        }

        // -------------------------
        // Enabled
        // -------------------------
        public static void Controls_Enable()
        {
            // Audio Codec
            VM.AudioView.Audio_Codec_IsEnabled = true;

            // Stream
            VM.AudioView.Audio_Stream_IsEnabled = true;

            // Channel
            VM.AudioView.Audio_Channel_IsEnabled = true;

            // Audio Quality
            VM.AudioView.Audio_Quality_IsEnabled = true;

            // SampleRate
            VM.AudioView.Audio_SampleRate_IsEnabled = true;

            // Bit Depth
            VM.AudioView.Audio_BitDepth_IsEnabled = true;

            // Volume
            VM.AudioView.Audio_Volume_IsEnabled = true;

            // Hard Limiter
            VM.AudioView.Audio_HardLimiter_IsEnabled = true;
        }

        // -------------------------
        // Disabled
        // -------------------------
        public static void Controls_Disable()
        {
            // VBR Button
            VM.AudioView.Audio_VBR_IsEnabled = false;

            // Compression Level
            VM.AudioView.Audio_CompressionLevel_IsEnabled = false;
        }
    }
}
