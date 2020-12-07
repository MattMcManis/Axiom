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

using Axiom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Controls
{
    namespace Video
    {
        namespace Codec
        {
            public class HEVC_NVENC : Controls.IVideoCodec
            {
                // ---------------------------------------------------------------------------
                // Codec
                // ---------------------------------------------------------------------------
                public static ObservableCollection<ViewModel.Video.VideoCodec> codec = new ObservableCollection<ViewModel.Video.VideoCodec>()
                {
                     new ViewModel.Video.VideoCodec()
                     {
                         Codec = "hevc_nvenc",
                         Parameters = ""
                     }
                };

                public /*static*/ void Codec_Set()
                {
                    // Combine Codec + Parameters
                    List<string> codec = new List<string>()
                    {
                        "-c:v",
                        HEVC_NVENC.codec.FirstOrDefault()?.Codec,
                        HEVC_NVENC.codec.FirstOrDefault()?.Parameters
                    };

                    VM.VideoView.Video_Codec = string.Join(" ", codec.Where(s => !string.IsNullOrEmpty(s)));
                }



                // ---------------------------------------------------------------------------
                // Items Source
                // ---------------------------------------------------------------------------

                // -------------------------
                // Encode Speed
                // -------------------------
                public /*static*/ ObservableCollection<ViewModel.Video.VideoEncodeSpeed> encodeSpeed = new ObservableCollection<ViewModel.Video.VideoEncodeSpeed>()
                {
                     new ViewModel.Video.VideoEncodeSpeed() { Name = "none",            Command = ""},
                     new ViewModel.Video.VideoEncodeSpeed() { Name = "Default",         Command = "-preset default" },
                     new ViewModel.Video.VideoEncodeSpeed() { Name = "Lossless",        Command = "-preset lossless" },
                     new ViewModel.Video.VideoEncodeSpeed() { Name = "Lossless HP",     Command = "-preset losslesshp" },
                     new ViewModel.Video.VideoEncodeSpeed() { Name = "Slow",            Command = "-preset slow" },
                     new ViewModel.Video.VideoEncodeSpeed() { Name = "Medium",          Command = "-preset medium" },
                     new ViewModel.Video.VideoEncodeSpeed() { Name = "Fast",            Command = "-preset fast" },
                     new ViewModel.Video.VideoEncodeSpeed() { Name = "HP",              Command = "-preset hp" },
                     new ViewModel.Video.VideoEncodeSpeed() { Name = "HQ",              Command = "-preset hq" },
                     new ViewModel.Video.VideoEncodeSpeed() { Name = "BD",              Command = "-preset hq" },
                     new ViewModel.Video.VideoEncodeSpeed() { Name = "Low Latency",     Command = "-preset ll" },
                     new ViewModel.Video.VideoEncodeSpeed() { Name = "Low Latency HP",  Command = "-preset llhp" },
                     new ViewModel.Video.VideoEncodeSpeed() { Name = "Low Latency HQ",  Command = "-preset llhq" },
                };

                // -------------------------
                // Pixel Format
                // -------------------------
                public static ObservableCollection<string> pixelFormat = new ObservableCollection<string>()
                {
                    "auto",
                    "yuv420p",
                    "nv12",
                    "p010le",
                    "yuv444p",
                    "yuv444p16le",
                    "bgr0",
                    "rgb0",
                    "cuda",
                    "d3d11"
                };

                // -------------------------
                // Quality
                // -------------------------
                public /*static*/ ObservableCollection<ViewModel.Video.VideoQuality> quality = new ObservableCollection<ViewModel.Video.VideoQuality>()
                {
                     new ViewModel.Video.VideoQuality() { Name = "Auto",      CRF = "",   CRF_HWAccel_Intel_QSV = "",   CRF_HWAccel_NVIDIA_NVENC = "",   CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      MinRate = "", MaxRate = "", BufSize ="", NA = "3000K" },
                     new ViewModel.Video.VideoQuality() { Name = "Lossless",  CRF = "",   CRF_HWAccel_Intel_QSV = "",   CRF_HWAccel_NVIDIA_NVENC = "",   CBR_BitMode = "",     CBR = "",      VBR_BitMode = "",     VBR = "",      MinRate = "", MaxRate = "", BufSize ="", Lossless = "" },
                     new ViewModel.Video.VideoQuality() { Name = "Ultra",     CRF = "16", CRF_HWAccel_Intel_QSV = "18", CRF_HWAccel_NVIDIA_NVENC = "18", CBR_BitMode = "-b:v", CBR = "5000K", VBR_BitMode = "-q:v", VBR = "5000K", MinRate = "", MaxRate = "", BufSize ="" },
                     new ViewModel.Video.VideoQuality() { Name = "High",      CRF = "20", CRF_HWAccel_Intel_QSV = "22", CRF_HWAccel_NVIDIA_NVENC = "22", CBR_BitMode = "-b:v", CBR = "2800K", VBR_BitMode = "-q:v", VBR = "2800K", MinRate = "", MaxRate = "", BufSize ="" },
                     new ViewModel.Video.VideoQuality() { Name = "Medium",    CRF = "25", CRF_HWAccel_Intel_QSV = "27", CRF_HWAccel_NVIDIA_NVENC = "27", CBR_BitMode = "-b:v", CBR = "1500K", VBR_BitMode = "-q:v", VBR = "1500K", MinRate = "", MaxRate = "", BufSize ="" },
                     new ViewModel.Video.VideoQuality() { Name = "Low",       CRF = "35", CRF_HWAccel_Intel_QSV = "37", CRF_HWAccel_NVIDIA_NVENC = "36", CBR_BitMode = "-b:v", CBR = "600K",  VBR_BitMode = "-q:v", VBR = "600K",  MinRate = "", MaxRate = "", BufSize ="" },
                     new ViewModel.Video.VideoQuality() { Name = "Sub",       CRF = "45", CRF_HWAccel_Intel_QSV = "47", CRF_HWAccel_NVIDIA_NVENC = "48", CBR_BitMode = "-b:v", CBR = "250K",  VBR_BitMode = "-q:v", VBR = "250K",  MinRate = "", MaxRate = "", BufSize ="" },
                     new ViewModel.Video.VideoQuality() { Name = "Custom",    CRF = "",   CRF_HWAccel_Intel_QSV = "",   CRF_HWAccel_NVIDIA_NVENC = "",   CBR_BitMode = "-b:v", CBR = "",      VBR_BitMode = "-q:v", VBR = "",      MinRate = "", MaxRate = "", BufSize ="" }
                };

                // -------------------------
                // Pass
                // -------------------------
                public static void EncodingPass()
                {
                    // -------------------------
                    // Quality
                    // -------------------------
                    switch (VM.VideoView.Video_Quality_SelectedItem)
                    {
                        // Auto
                        case "Auto":
                            VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                            {
                                "2 Pass"
                            };

                            VM.VideoView.Video_Pass_SelectedItem = "2 Pass";
                            VM.VideoView.Video_Pass_IsEnabled = false;
                            Controls.passUserSelected = false;
                            VM.VideoView.Video_CRF_IsEnabled = false;
                            break;

                        // Lossless
                        case "Lossless":
                            VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                            {
                                "1 Pass"
                            };

                            VM.VideoView.Video_Pass_SelectedItem = "1 Pass";
                            VM.VideoView.Video_Pass_IsEnabled = true;
                            VM.VideoView.Video_CRF_IsEnabled = false;
                            break;

                        // Custom
                        case "Custom":
                            VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                            {
                                "CRF",
                                "1 Pass",
                                "2 Pass"
                            };

                            VM.VideoView.Video_Pass_IsEnabled = true;
                            VM.VideoView.Video_CRF_IsEnabled = true;
                            break;

                        // None
                        case "None":
                            VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                            {
                                "auto"
                            };

                            VM.VideoView.Video_Pass_IsEnabled = false;
                            VM.VideoView.Video_CRF_IsEnabled = false;
                            break;

                        // Presets: Ultra, High, Medium, Low, Sub
                        default:
                            VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                            {
                                "CRF",
                                "1 Pass",
                                "2 Pass"
                            };

                            VM.VideoView.Video_Pass_IsEnabled = true;
                            VM.VideoView.Video_CRF_IsEnabled = false;

                            // Default to CRF
                            if (Controls.passUserSelected == false)
                            {
                                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                                Controls.passUserSelected = true;
                            }
                            break;
                    }

                    // Clear TextBoxes
                    if (VM.VideoView.Video_Quality_SelectedItem == "Auto" ||
                        VM.VideoView.Video_Quality_SelectedItem == "Lossless" ||
                        VM.VideoView.Video_Quality_SelectedItem == "Custom" ||
                        VM.VideoView.Video_Quality_SelectedItem == "None"
                        )
                    {
                        VM.VideoView.Video_CRF_Text = string.Empty;
                        VM.VideoView.Video_BitRate_Text = string.Empty;
                        VM.VideoView.Video_MinRate_Text = string.Empty;
                        VM.VideoView.Video_MaxRate_Text = string.Empty;
                        VM.VideoView.Video_BufSize_Text = string.Empty;
                    }

                }

                // -------------------------
                // Optimize
                // -------------------------
                public /*static*/ ObservableCollection<ViewModel.Video.VideoOptimize> optimize = new ObservableCollection<ViewModel.Video.VideoOptimize>()
                {
                    new ViewModel.Video.VideoOptimize() { Name = "None",   Tune = "none", Profile = "none",   Level = "none", Command = "" },
                    new ViewModel.Video.VideoOptimize() { Name = "Custom", Tune = "none", Profile = "none",   Level = "none", Command = "" },
                    new ViewModel.Video.VideoOptimize() { Name = "Web",    Tune = "none", Profile = "main",   Level = "3.1",  Command = "-movflags +faststart" },
                    new ViewModel.Video.VideoOptimize() { Name = "PC HD",  Tune = "none", Profile = "main10", Level = "5.1",  Command = "" },
                    new ViewModel.Video.VideoOptimize() { Name = "UHD",    Tune = "none", Profile = "main10", Level = "5.1",  Command = "" },
                };

                // -------------------------
                // Tune
                // -------------------------
                public /*static*/ ObservableCollection<string> tune = new ObservableCollection<string>()
                {
                    "none",
                    "animation",
                    "psnr",
                    "ssim",
                    "grain",
                    "fastdecode",
                    "zerolatency"
                };

                // -------------------------
                // Profile
                // -------------------------
                public /*static*/ ObservableCollection<string> profile = new ObservableCollection<string>()
                {
                    "none",
                    "main",
                    "main10",
                    "rext"
                };

                // -------------------------
                // Level
                // -------------------------
                public /*static*/ ObservableCollection<string> level = new ObservableCollection<string>()
                {
                    "none",
                    "1",
                    "2",
                    "2.1",
                    "3",
                    "3.1",
                    "4",
                    "4.1",
                    "5",
                    "5.1",
                    "5.2",
                    "6",
                    "6.1",
                    "6.2",
                };


                // ---------------------------------------------------------------------------
                // Controls Behavior
                // ---------------------------------------------------------------------------

                // -------------------------
                // Items Source
                // -------------------------
                public /*static*/ void Controls_ItemsSource()
                {
                    // Encode Speed
                    VM.VideoView.Video_EncodeSpeed_Items = encodeSpeed;

                    // Pixel Format
                    VM.VideoView.Video_PixelFormat_Items = pixelFormat;

                    // Pass
                    //VM.VideoView.Video_Pass_Items = pass;
                    EncodingPass();

                    // Video Quality
                    VM.VideoView.Video_Quality_Items = quality;

                    // Optimize
                    VM.VideoView.Video_Optimize_Items = optimize;
                    // Tune
                    VM.VideoView.Video_Optimize_Tune_Items = tune;
                    // Profile
                    VM.VideoView.Video_Optimize_Profile_Items = profile;
                    // Level
                    VM.VideoView.Video_Optimize_Level_Items = level;
                }

                // -------------------------
                // Selected Items
                // -------------------------
                public /*static*/ void Controls_Selected()
                {
                    // Pixel Format
                    VM.VideoView.Video_PixelFormat_SelectedItem = "p010le";

                    // Framerate
                    VM.VideoView.Video_FPS_SelectedItem = "auto";
                }


                // -------------------------
                // Expanded
                // -------------------------
                public /*static*/ void Controls_Expanded()
                {
                    VM.VideoView.Video_Optimize_IsExpanded = true;
                }

                // -------------------------
                // Collapsed
                // -------------------------
                public /*static*/ void Controls_Collapsed()
                {
                    // None
                }


                // -------------------------
                // Checked
                // -------------------------
                public /*static*/ void Controls_Checked()
                {
                    // None
                }

                // -------------------------
                // Unchecked
                // -------------------------
                public /*static*/ void Controls_Unhecked()
                {
                    // BitRate Mode
                    VM.VideoView.Video_VBR_IsChecked = false;
                }


                // -------------------------
                // Enabled
                // -------------------------
                public /*static*/ void Controls_Enable()
                {
                    // Video Encode Speed
                    VM.VideoView.Video_EncodeSpeed_IsEnabled = true;

                    // Video Codec
                    VM.VideoView.Video_Codec_IsEnabled = true;

                    // Video Quality
                    VM.VideoView.Video_Quality_IsEnabled = true;

                    // HW Accel
                    VM.VideoView.Video_HWAccel_IsEnabled = true;

                    // Video VBR
                    VM.VideoView.Video_VBR_IsEnabled = true;

                    // Pixel Format
                    VM.VideoView.Video_PixelFormat_IsEnabled = true;

                    // FPS ComboBox
                    VM.VideoView.Video_FPS_IsEnabled = true;

                    // Speed
                    VM.VideoView.Video_Speed_IsEnabled = true;

                    // Vsync
                    VM.VideoView.Video_Vsync_IsEnabled = true;

                    // Optimize ComboBox
                    VM.VideoView.Video_Optimize_IsEnabled = true;

                    // Size
                    VM.VideoView.Video_Scale_IsEnabled = true;

                    // Scaling ComboBox
                    VM.VideoView.Video_ScalingAlgorithm_IsEnabled = true;

                    // Crop
                    VM.VideoView.Video_Crop_IsEnabled = true;


                    // Color Range
                    VM.VideoView.Video_Color_Range_IsEnabled = true;

                    // Color Space
                    VM.VideoView.Video_Color_Space_IsEnabled = true;

                    // Color Primaries
                    VM.VideoView.Video_Color_Primaries_IsEnabled = true;

                    // Color Transfer Characteristics
                    VM.VideoView.Video_Color_TransferCharacteristics_IsEnabled = true;

                    // Color Matrix
                    VM.VideoView.Video_Color_Matrix_IsEnabled = true;


                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_IsEnabled = true;

                    // Subtitle Stream
                    VM.SubtitleView.Subtitle_Stream_IsEnabled = true;


                    // Filters
                    Filters.Video.VideoFilters_EnableAll();
                }

                // -------------------------
                // Disabled
                // -------------------------
                public /*static*/ void Controls_Disable()
                {
                    // None
                }
            }
        }
    }
}
