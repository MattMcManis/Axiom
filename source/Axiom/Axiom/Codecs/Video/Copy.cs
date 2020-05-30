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
using ViewModel;

namespace Controls
{
    namespace Video
    {
        namespace Codec
        {
            class Copy
            {
                // ---------------------------------------------------------------------------
                // Codec
                // ---------------------------------------------------------------------------
                public static List<ViewModel.Video.VideoCodec> codec = new List<ViewModel.Video.VideoCodec>()
                {
                     new ViewModel.Video.VideoCodec()
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
                        "-c:v",
                        Copy.codec.FirstOrDefault()?.Codec,
                        Copy.codec.FirstOrDefault()?.Parameters
                    };

                    VM.VideoView.Video_Codec = string.Join(" ", codec.Where(s => !string.IsNullOrEmpty(s)));
                }



                // ---------------------------------------------------------------------------
                // Items Source
                // ---------------------------------------------------------------------------

                // -------------------------
                // Encode Speed
                // -------------------------
                public static List<ViewModel.Video.VideoEncodeSpeed> encodeSpeed = new List<ViewModel.Video.VideoEncodeSpeed>()
                {
                     new ViewModel.Video.VideoEncodeSpeed() { Name = "none", Command = ""},
                };

                // -------------------------
                // Pixel Format
                // -------------------------
                public static List<string> pixelFormat = new List<string>()
                {
                    "auto"
                };

                // -------------------------
                // Quality
                // -------------------------
                public static List<ViewModel.Video.VideoQuality> quality = new List<ViewModel.Video.VideoQuality>()
                {
                     new ViewModel.Video.VideoQuality() { Name = "Auto",   CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="", NA = "" },
                     new ViewModel.Video.VideoQuality() { Name = "Ultra",  CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
                     new ViewModel.Video.VideoQuality() { Name = "High",   CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
                     new ViewModel.Video.VideoQuality() { Name = "Medium", CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
                     new ViewModel.Video.VideoQuality() { Name = "Low",    CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
                     new ViewModel.Video.VideoQuality() { Name = "Sub",    CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" },
                     new ViewModel.Video.VideoQuality() { Name = "Custom", CRF = "",  Video_CRF_BitRate = "", CBR_BitMode = "", CBR = "", VBR_BitMode = "", VBR = "", MinRate = "", MaxRate = "", BufSize ="" }
                };

                // -------------------------
                // Pass
                // -------------------------
                public static void EncodingPass()
                {
                    // Items Source
                    VM.VideoView.Video_Pass_Items = new List<string>()
                    {
                        "auto"
                    };

                    VM.VideoView.Video_Pass_SelectedItem = "auto";
                    VM.VideoView.Video_Pass_IsEnabled = false;
                    Controls.passUserSelected = false;

                    VM.VideoView.Video_CRF_IsEnabled = false;
                    VM.VideoView.Video_CRF_Text = string.Empty;
                    VM.VideoView.Video_BitRate_Text = string.Empty;
                    VM.VideoView.Video_MinRate_Text = string.Empty;
                    VM.VideoView.Video_MaxRate_Text = string.Empty;
                    VM.VideoView.Video_BufSize_Text = string.Empty;
                }

                // -------------------------
                // Optimize
                // -------------------------
                public static List<ViewModel.Video.VideoOptimize> optimize = new List<ViewModel.Video.VideoOptimize>()
                {
                    new ViewModel.Video.VideoOptimize() { Name = "None", Tune = "none", Profile = "none", Level = "none", Command = "" },
                };

                // -------------------------
                // Tune
                // -------------------------
                public static List<string> tune = new List<string>()
                {
                    "none"
                };

                // -------------------------
                // Profile
                // -------------------------
                public static List<string> profile = new List<string>()
                {
                    "none"
                };

                // -------------------------
                // Level
                // -------------------------
                public static List<string> level = new List<string>()
                {
                    "none"
                };


                // ---------------------------------------------------------------------------
                // Controls Behavior
                // ---------------------------------------------------------------------------

                // -------------------------
                // Items Source
                // -------------------------
                public static void Controls_ItemsSource()
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
                public static void Controls_Selected()
                {
                    // HW Accel
                    VM.VideoView.Video_EncodeSpeed_SelectedItem = "none";

                    // HW Accel
                    VM.VideoView.Video_HWAccel_SelectedItem = "Off";

                    // Pixel Format
                    VM.VideoView.Video_PixelFormat_SelectedItem = "auto";

                    // Framerate
                    VM.VideoView.Video_FPS_SelectedItem = "auto";

                    // Speed
                    VM.VideoView.Video_Speed_SelectedItem = "auto";

                    // Size
                    VM.VideoView.Video_Scale_SelectedItem = "Source";


                    // Color Range
                    VM.VideoView.Video_Color_Range_SelectedItem = "auto";

                    // Color Space
                    VM.VideoView.Video_Color_Space_SelectedItem = "auto";

                    // Color Primaries
                    VM.VideoView.Video_Color_Primaries_SelectedItem = "auto";

                    // Color Transfer Characteristics
                    VM.VideoView.Video_Color_TransferCharacteristics_SelectedItem = "auto";

                    // Color Matrix
                    VM.VideoView.Video_Color_Matrix_SelectedItem = "auto";


                    // Filters
                    Filters.Video.VideoFilters_ControlsSelectDefaults();
                }


                // -------------------------
                // Expanded
                // -------------------------
                public static void Controls_Expanded()
                {
                    // None
                }

                // -------------------------
                // Collapsed
                // -------------------------
                public static void Controls_Collapsed()
                {
                    VM.VideoView.Video_Optimize_IsExpanded = false;
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
                    VM.VideoView.Video_VBR_IsChecked = false;
                }


                // -------------------------
                // Enabled
                // -------------------------
                public static void Controls_Enable()
                {
                    // Video Codec
                    VM.VideoView.Video_Codec_IsEnabled = true;

                    //// Video Quality
                    //VM.VideoView.Video_Quality_IsEnabled = true;

                    //// Video VBR
                    //VM.VideoView.Video_VBR_IsEnabled = true;

                    //// Pixel Format
                    //VM.VideoView.Video_PixelFormat_IsEnabled = true;

                    //// FPS ComboBox
                    //VM.VideoView.Video_FPS_IsEnabled = true;

                    //// Optimize ComboBox
                    //VM.VideoView.Video_Optimize_IsEnabled = true;

                    //// Scaling ComboBox
                    //VM.VideoView.Video_ScalingAlgorithm_IsEnabled = true;

                    //// Crop
                    //VM.VideoView.Video_Crop_IsEnabled = true;

                    // Subtitle Codec
                    //VM.SubtitleView.Subtitle_Codec_IsEnabled = true;

                    // Subtitle Stream
                    //VM.SubtitleView.Subtitle_Stream_IsEnabled = true;
                }

                // -------------------------
                // Disabled
                // -------------------------
                public static void Controls_Disable()
                {
                    // Video Encode Speed
                    VM.VideoView.Video_EncodeSpeed_IsEnabled = false;

                    // HW Accel
                    VM.VideoView.Video_HWAccel_IsEnabled = false;

                    // Video Quality
                    VM.VideoView.Video_Quality_IsEnabled = false;

                    // Video VBR
                    VM.VideoView.Video_VBR_IsEnabled = false;

                    // Pixel Format
                    VM.VideoView.Video_PixelFormat_IsEnabled = false;

                    // FPS ComboBox
                    VM.VideoView.Video_FPS_IsEnabled = false;

                    // Speed
                    VM.VideoView.Video_Speed_IsEnabled = false;

                    // Optimize ComboBox
                    VM.VideoView.Video_Optimize_IsEnabled = false;

                    // Size
                    VM.VideoView.Video_Scale_IsEnabled = false;

                    // Scaling ComboBox
                    VM.VideoView.Video_ScalingAlgorithm_IsEnabled = false;

                    // Crop
                    VM.VideoView.Video_Crop_IsEnabled = false;


                    // Color Range
                    VM.VideoView.Video_Color_Range_IsEnabled = false;

                    // Color Space
                    VM.VideoView.Video_Color_Space_IsEnabled = false;

                    // Color Primaries
                    VM.VideoView.Video_Color_Primaries_IsEnabled = false;

                    // Color Transfer Characteristics
                    VM.VideoView.Video_Color_TransferCharacteristics_IsEnabled = false;

                    // Color Matrix
                    VM.VideoView.Video_Color_Matrix_IsEnabled = false;


                    // Filters
                    Filters.Video.VideoFilters_DisableAll();
                }

            }
        }
    }
}
