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

namespace Controls
{
    namespace Audio
    {
        namespace Codec
        {
            public class None
            {
                // ---------------------------------------------------------------------------
                // Codec
                // ---------------------------------------------------------------------------
                public static void Codec_Set()
                {
                    VM.AudioView.Audio_Codec = string.Empty;
                }



                // ---------------------------------------------------------------------------
                // Items Source
                // ---------------------------------------------------------------------------

                // -------------------------
                // Channel
                // -------------------------
                public static ObservableCollection<string> channel = new ObservableCollection<string>()
                {
                    "none"
                };

                // -------------------------
                // Quality
                // -------------------------
                public static ObservableCollection<ViewModel.Audio.AudioQuality> quality = new ObservableCollection<ViewModel.Audio.AudioQuality>()
                {
                     new ViewModel.Audio.AudioQuality() { Name = "None", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", NA = ""}
                };

                // -------------------------
                // Compression Level
                // -------------------------
                public static ObservableCollection<string> compressionLevel = new ObservableCollection<string>()
                {
                    "none"
                };

                // -------------------------
                // Sample Rate
                // -------------------------
                public static ObservableCollection<ViewModel.Audio.AudioSampleRate> sampleRate = new ObservableCollection<ViewModel.Audio.AudioSampleRate>()
                {
                     new ViewModel.Audio.AudioSampleRate() { Name = "auto", Frequency = "" }
                };

                // -------------------------
                // Bit Depth
                // -------------------------
                public static ObservableCollection<ViewModel.Audio.AudioBitDepth> bitDepth = new ObservableCollection<ViewModel.Audio.AudioBitDepth>()
                {
                     new ViewModel.Audio.AudioBitDepth() { Name = "auto", Depth = "" }
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
                    // Stream
                    //VM.AudioView.Audio_Stream_SelectedItem = "none";

                    // Compression Level
                    VM.AudioView.Audio_CompressionLevel_SelectedItem = "none";


                    // Filters
                    Filters.Audio.AudioFilters_ControlsSelectDefaults();
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
                    // BitRate Mode
                    VM.AudioView.Audio_VBR_IsChecked = false;
                }

                // -------------------------
                // Disabled
                // -------------------------
                public static void Controls_Disable()
                {
                    // Codec
                    //VM.AudioView.Audio_Codec_IsEnabled = false;

                    // Stream
                    VM.AudioView.Audio_Stream_IsEnabled = false;

                    // Channel
                    VM.AudioView.Audio_Channel_IsEnabled = false;

                    // Audio Quality
                    VM.AudioView.Audio_Quality_IsEnabled = false;

                    // VBR Button
                    VM.AudioView.Audio_VBR_IsEnabled = false;

                    // Compression Level
                    VM.AudioView.Audio_CompressionLevel_IsEnabled = false;

                    // SampleRate
                    VM.AudioView.Audio_SampleRate_IsEnabled = false;

                    // Bit Depth
                    VM.AudioView.Audio_BitDepth_IsEnabled = false;

                    // Volume
                    VM.AudioView.Audio_Volume_IsEnabled = false;

                    // Hard Limiter
                    VM.AudioView.Audio_HardLimiter_IsEnabled = false;


                    // Filters
                    Filters.Audio.AudioFilters_DisableAll();
                }

            }
        }
    }
}
