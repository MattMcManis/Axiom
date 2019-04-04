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

using System.IO;
using System.Linq;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class Presets
    {
        public static void SetPreset(ViewModel vm)
        {
            // -------------------------
            // Custom
            // -------------------------
            //// Custom ComboBox Editable
            //if (vm.Preset_SelectedItem == "Custom")
            //{
            //    //vm.Preset.IsEditable = true;
            //}
            //// Maintain Editable Combobox while typing
            //if (vm.Preset.IsEditable == true)
            //{
            //    //vm.Preset.IsEditable = true;

            //    // Clear Custom Text
            //    //vm.Preset.Text = string.Empty;
            //}

            //System.Windows.MessageBox.Show(vm.Preset_SelectedItem); //debug

            // ---------------------------------------------------------------------------
            // Default
            // ---------------------------------------------------------------------------
            if (vm.Preset_SelectedItem == "Preset")
            {
                // -------------------------
                // Default Video
                // -------------------------
                if (vm.Format_Container_SelectedItem == "webm" ||
                    vm.Format_Container_SelectedItem == "mp4" ||
                    vm.Format_Container_SelectedItem == "mkv" ||
                    vm.Format_Container_SelectedItem == "m2v" ||
                    vm.Format_Container_SelectedItem == "mpg" ||
                    vm.Format_Container_SelectedItem == "avi" ||
                    vm.Format_Container_SelectedItem == "ogv" ||
                    vm.Format_Container_SelectedItem == "jpg" ||
                    vm.Format_Container_SelectedItem == "png" ||
                    vm.Format_Container_SelectedItem == "webp")
                {
                    //vm.Preset.IsEditable = false;

                    // Format
                    //vm.Format_Container_SelectedItem = vm.Format_Container_Items.FirstOrDefault();
                    vm.Format_Container_SelectedItem = "webm";
                    vm.Format_Cut_SelectedItem = "No";
                    vm.Format_CutStart_Hours_Text = "00";
                    vm.Format_CutStart_Minutes_Text = "00";
                    vm.Format_CutStart_Seconds_Text = "00";
                    vm.Format_CutStart_Milliseconds_Text = "000";
                    vm.Format_CutEnd_Hours_Text = "00";
                    vm.Format_CutEnd_Minutes_Text = "00";
                    vm.Format_CutEnd_Seconds_Text = "00";
                    vm.Format_CutEnd_Milliseconds_Text = "000";
                    vm.Format_YouTube_SelectedItem = "Video + Audio";
                    vm.Format_YouTube_Quality_SelectedItem = "best";

                    // Video
                    //vm.Video_Codec_SelectedItem = "VP8";
                    vm.Video_EncodeSpeed_SelectedItem = "Medium";
                    vm.Video_Quality_SelectedItem = "Auto";
                    vm.Video_Pass_SelectedItem = "CRF";
                    vm.Video_BitRate_Text = "";
                    vm.Video_MinRate_Text = "";
                    vm.Video_MaxRate_Text = "";
                    vm.Video_BufSize_Text = "";
                    vm.Video_FPS_SelectedItem = "auto";
                    vm.Video_Speed_SelectedItem = "auto";
                    vm.Video_Scale_SelectedItem = "Source";
                    vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                    vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                    vm.Video_AspectRatio_SelectedItem = "auto";
                    vm.Video_Crop_X_Text = "";
                    vm.Video_Crop_Y_Text = "";
                    vm.Video_Crop_Width_Text = "";
                    vm.Video_Crop_Height_Text = "";
                    vm.Video_CropClear_Text = "Clear";

                    // Subtitle
                    //vm.Subtitle_Codec_SelectedItem = "None";
                    vm.Subtitle_Stream_SelectedItem = "none";

                    // Audio
                    //vm.Audio_Codec_SelectedItem = "Vorbis";
                    vm.Audio_Stream_SelectedItem = "all";
                    vm.Audio_Channel_SelectedItem = "Source";
                    vm.Audio_Quality_SelectedItem = "Auto";
                    vm.Audio_SampleRate_SelectedItem = "auto";
                    vm.Audio_BitDepth_SelectedItem = "auto";
                    vm.Audio_Volume_Text = "100";
                    vm.Audio_HardLimiter_Value = 1;

                    // special rules for webm
                    if (vm.Format_Container_SelectedItem == "webm")
                    {
                        vm.Subtitle_Stream_SelectedItem = "none";
                        vm.Audio_Stream_SelectedItem = "1";
                        vm.Video_Optimize_SelectedItem = "Web";
                    }
                    else
                    {
                        vm.Video_Optimize_SelectedItem = "None";
                        vm.Video_Optimize_Tune_SelectedItem = "none";
                        vm.Video_Optimize_Profile_SelectedItem = "main";
                        vm.Video_Optimize_Level_SelectedItem = "5.2";
                    }

                    // Filters
                    vm.LoadFiltersDefault();

                }

                // -------------------------
                // Default Audio
                // -------------------------
                else if (vm.Format_Container_SelectedItem == "m4a" ||
                         vm.Format_Container_SelectedItem == "mp3" ||
                         vm.Format_Container_SelectedItem == "ogg" ||
                         vm.Format_Container_SelectedItem == "flac" ||
                         vm.Format_Container_SelectedItem == "wav")
                {
                    //vm.Preset.IsEditable = false;

                    // Format
                    //vm.Format_Container_SelectedItem = vm.Format_Container_Items.FirstOrDefault();
                    //vm.Format_Container_SelectedIndex = 1;
                    vm.Format_Container_SelectedItem = "mp3";
                    vm.Format_Cut_SelectedItem = "No";
                    vm.Format_CutStart_Hours_Text = "00";
                    vm.Format_CutStart_Minutes_Text = "00";
                    vm.Format_CutStart_Seconds_Text = "00";
                    vm.Format_CutStart_Milliseconds_Text = "000";
                    vm.Format_CutEnd_Hours_Text = "00";
                    vm.Format_CutEnd_Minutes_Text = "00";
                    vm.Format_CutEnd_Seconds_Text = "00";
                    vm.Format_CutEnd_Milliseconds_Text = "000";
                    vm.Format_YouTube_SelectedItem = "Video + Audio";
                    vm.Format_YouTube_Quality_SelectedItem = "best";

                    // Video
                    vm.Video_Codec_SelectedItem = "None";
                    vm.Video_EncodeSpeed_SelectedItem = "none";
                    vm.Video_Quality_SelectedItem = "None";
                    vm.Video_Pass_SelectedItem = "auto";
                    vm.Video_BitRate_Text = "";
                    vm.Video_MinRate_Text = "";
                    vm.Video_MaxRate_Text = "";
                    vm.Video_BufSize_Text = "";
                    vm.Video_PixelFormat_SelectedItem = "none";
                    vm.Video_FPS_SelectedItem = "auto";
                    vm.Video_Speed_SelectedItem = "auto";
                    vm.Video_Optimize_SelectedItem = "None";
                    vm.Video_Scale_SelectedItem = "Source";
                    vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                    vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                    vm.Video_AspectRatio_SelectedItem = "auto";
                    vm.Video_AspectRatio_SelectedItem = "auto";
                    vm.Video_Crop_X_Text = "";
                    vm.Video_Crop_Y_Text = "";
                    vm.Video_Crop_Width_Text = "";
                    vm.Video_Crop_Height_Text = "";
                    vm.Video_CropClear_Text = "Clear";

                    // Subtitle
                    vm.Subtitle_Codec_SelectedItem = "None";
                    vm.Subtitle_Stream_SelectedItem = "none";

                    // Audio
                    vm.Audio_Codec_SelectedItem = "LAME";
                    vm.Audio_Stream_SelectedItem = "1";
                    vm.Audio_Channel_SelectedItem = "Source";
                    vm.Audio_Quality_SelectedItem = "Auto";
                    vm.Audio_CompressionLevel_SelectedItem = "auto";
                    vm.Audio_SampleRate_SelectedItem = "auto";
                    // special rules for PCM codec
                    if (vm.Audio_Codec_SelectedItem == "PCM")
                    {
                        vm.Audio_BitDepth_SelectedItem = "24";
                    }
                    else
                    {
                        vm.Audio_BitDepth_SelectedItem = "auto";
                    }

                    vm.Audio_Volume_Text = "100";
                    vm.Audio_HardLimiter_Value = 1;

                    // Filters
                    vm.LoadFiltersDefault();
                }
            }


            // ---------------------------------------------------------------------------
            // Web
            // ---------------------------------------------------------------------------
            // -------------------------
            // HTML5
            // -------------------------
            else if (vm.Preset_SelectedItem == "HTML5")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "webm";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "VP8";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Quality_SelectedItem = "Medium";
                vm.Video_Pass_SelectedItem = "CRF";
                //vm.Video_BitRate_Text = ""; // use quality preset bitrate
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "Web";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "None";
                vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                vm.Audio_Codec_SelectedItem = "Vorbis";
                vm.Audio_Stream_SelectedItem = "1";
                vm.Audio_Channel_SelectedItem = "Stereo";
                vm.Audio_Quality_SelectedItem = "192";
                vm.Audio_VBR_IsChecked = true;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "44.1k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // YouTube
            // -------------------------
            else if (vm.Preset_SelectedItem == "YouTube")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x264";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Quality_SelectedItem = "Ultra";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "Web";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "MOV Text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "Auto";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }


            // ---------------------------------------------------------------------------
            // PC
            // ---------------------------------------------------------------------------
            // -------------------------
            // Archive Video
            // -------------------------
            else if (vm.Preset_SelectedItem == "Archive")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mkv";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "FFV1";
                vm.Video_EncodeSpeed_SelectedItem = "none";
                vm.Video_Quality_SelectedItem = "Lossless";
                vm.Video_Pass_SelectedItem = "2 Pass";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv444p10le";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "None";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "Copy";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "FLAC";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "Auto";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // HEVC Ultra
            // -------------------------
            else if (vm.Preset_SelectedItem == "HEVC Ultra" ||
                     vm.Preset_SelectedItem == "HEVC High")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mkv";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x265";
                vm.Video_EncodeSpeed_SelectedItem = "Slow";

                if (vm.Preset_SelectedItem == "HEVC Ultra")
                {
                    vm.Video_Quality_SelectedItem = "Ultra";
                }
                else if (vm.Preset_SelectedItem == "HEVC High")
                {
                    vm.Video_Quality_SelectedItem = "High";
                }

                vm.Video_Pass_SelectedItem = "CRF";
                //vm.Video_CRF_Text = ""; // Use Quality Preset Value
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p10le";
                vm.Video_FPS_SelectedItem = "59.94";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "Custom";
                vm.Video_Optimize_Tune_SelectedItem = "none";
                vm.Video_Optimize_Profile_SelectedItem = "main10";
                vm.Video_Optimize_Level_SelectedItem = "5.2";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";


                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "Copy";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "Auto";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // HD Video
            // -------------------------
            else if (vm.Preset_SelectedItem == "HD Ultra" ||
                     vm.Preset_SelectedItem == "HD High" ||
                     vm.Preset_SelectedItem == "HD Medium" ||
                     vm.Preset_SelectedItem == "HD Low"
                     )
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x264";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";

                if (vm.Preset_SelectedItem == "HD Ultra")
                {
                    vm.Video_Quality_SelectedItem = "Ultra";
                }
                else if (vm.Preset_SelectedItem == "HD High")
                {
                    vm.Video_Quality_SelectedItem = "High";
                }
                else if (vm.Preset_SelectedItem == "HD Medium")
                {
                    vm.Video_Quality_SelectedItem = "Medium";
                }
                else if (vm.Preset_SelectedItem == "HD Low")
                {
                    vm.Video_Quality_SelectedItem = "Low";
                }

                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "PC HD";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "MOV Text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "Auto";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // SD Video
            // -------------------------
            else if (vm.Preset_SelectedItem == "SD High" ||
                     vm.Preset_SelectedItem == "SD Medium" ||
                     vm.Preset_SelectedItem == "SD Low")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x264";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";

                if (vm.Preset_SelectedItem == "SD High")
                {
                    vm.Video_Quality_SelectedItem = "High";
                }
                else if (vm.Preset_SelectedItem == "SD Medium")
                {
                    vm.Video_Quality_SelectedItem = "Medium";
                }
                else if (vm.Preset_SelectedItem == "SD Low")
                {
                    vm.Video_Quality_SelectedItem = "Low";
                }

                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "PC SD";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "MOV Text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AC3";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Stereo";
                vm.Audio_Quality_SelectedItem = "256";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "44.1k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }


            // ---------------------------------------------------------------------------
            // Device
            // ---------------------------------------------------------------------------
            // -------------------------
            // Roku
            // -------------------------
            else if (vm.Preset_SelectedItem == "Roku")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mkv";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x264";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Quality_SelectedItem = "Ultra";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "Custom";
                vm.Video_Optimize_Tune_SelectedItem = "none";
                vm.Video_Optimize_Profile_SelectedItem = "high";
                vm.Video_Optimize_Level_SelectedItem = "4.0";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "SRT";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "160";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // Amazon Fire
            // -------------------------
            else if (vm.Preset_SelectedItem == "Amazon Fire")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x264";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Quality_SelectedItem = "Ultra";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "Custom";
                vm.Video_Optimize_Tune_SelectedItem = "none";
                vm.Video_Optimize_Profile_SelectedItem = "high";
                vm.Video_Optimize_Level_SelectedItem = "4.2";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "SRT";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "160";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // Chromecast
            // -------------------------
            else if (vm.Preset_SelectedItem == "Chromecast")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x264";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Quality_SelectedItem = "Ultra";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "Custom";
                vm.Video_Optimize_Tune_SelectedItem = "none";
                vm.Video_Optimize_Profile_SelectedItem = "high";
                vm.Video_Optimize_Level_SelectedItem = "4.2";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "SRT";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "160";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // Apple TV
            // -------------------------
            else if (vm.Preset_SelectedItem == "Apple TV")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x264";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Quality_SelectedItem = "Ultra";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "Custom";
                vm.Video_Optimize_Tune_SelectedItem = "none";
                vm.Video_Optimize_Profile_SelectedItem = "high";
                vm.Video_Optimize_Level_SelectedItem = "4.2";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "SRT";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "160";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // Raspberry Pi
            // -------------------------
            else if (vm.Preset_SelectedItem == "Raspberry Pi")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x264";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Quality_SelectedItem = "High";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "Custom";
                vm.Video_Optimize_Tune_SelectedItem = "none";
                vm.Video_Optimize_Profile_SelectedItem = "main";
                vm.Video_Optimize_Level_SelectedItem = "4.2";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "SRT";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "160";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }


            // ---------------------------------------------------------------------------
            // Mobile
            // ---------------------------------------------------------------------------
            // -------------------------
            // Android
            // -------------------------
            else if (vm.Preset_SelectedItem == "Android")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x264";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Quality_SelectedItem = "High";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_Optimize_SelectedItem = "Android";
                vm.Video_FPS_SelectedItem = "23.976";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "MOV Text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Stereo";
                vm.Audio_Quality_SelectedItem = "400";
                vm.Audio_VBR_IsChecked = true;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "44.1k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // iOS
            // -------------------------
            else if (vm.Preset_SelectedItem == "iOS")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x264";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Quality_SelectedItem = "High";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_FPS_SelectedItem = "23.976";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "Apple";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "MOV Text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Stereo";
                vm.Audio_Quality_SelectedItem = "400";
                vm.Audio_VBR_IsChecked = true;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "44.1k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }


            // ---------------------------------------------------------------------------
            // Console
            // ---------------------------------------------------------------------------
            // -------------------------
            // PS3
            // -------------------------
            else if (vm.Preset_SelectedItem == "PS3")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x264";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Quality_SelectedItem = "Ultra";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_FPS_SelectedItem = "23.976";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "PS3";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "MOV Text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "400";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "48k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // PS4
            // -------------------------
            else if (vm.Preset_SelectedItem == "PS4")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x264";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Quality_SelectedItem = "Ultra";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "PS4";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "MOV Text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "Auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // Xbox 360
            // -------------------------
            else if (vm.Preset_SelectedItem == "Xbox 360")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x264";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Quality_SelectedItem = "High";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_FPS_SelectedItem = "23.976";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "Xbox 360";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "MOV Text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Stereo";
                vm.Audio_Quality_SelectedItem = "320";
                vm.Audio_SampleRate_SelectedItem = "48k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // Xbox One
            // -------------------------
            else if (vm.Preset_SelectedItem == "Xbox One")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x264";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Quality_SelectedItem = "Ultra";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "Xbox One";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "MOV Text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "Auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }


            // ---------------------------------------------------------------------------
            // Disc
            // ---------------------------------------------------------------------------
            // -------------------------
            // UHD
            // -------------------------
            else if (vm.Preset_SelectedItem == "UHD")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x265";
                vm.Video_EncodeSpeed_SelectedItem = "Slow";
                vm.Video_Quality_SelectedItem = "Custom";
                vm.Video_Pass_SelectedItem = "2 Pass";
                vm.Video_BitRate_Text = "50M";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "75M";
                vm.Video_BufSize_Text = "75M";
                vm.Video_PixelFormat_SelectedItem = "yuv420p10le";
                vm.Video_FPS_SelectedItem = "60";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "UHD";
                vm.Video_Scale_SelectedItem = "4K UHD";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "MOV Text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "DTS";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "1509";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "48k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // Blu-ray
            // -------------------------
            else if (vm.Preset_SelectedItem == "Blu-ray")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "x264";
                vm.Video_EncodeSpeed_SelectedItem = "Slow";
                vm.Video_Quality_SelectedItem = "Ultra";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p10le";
                vm.Video_FPS_SelectedItem = "23.976";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "Blu-ray";
                vm.Video_Scale_SelectedItem = "1080p";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "MOV Text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "AC3";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "640";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "48k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // DVD
            // -------------------------
            else if (vm.Preset_SelectedItem == "DVD")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mpg";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Audio Only";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "MPEG-2";
                vm.Video_EncodeSpeed_SelectedItem = "none";
                vm.Video_Quality_SelectedItem = "Custom";
                vm.Video_Pass_SelectedItem = "2 Pass";
                vm.Video_BitRate_Text = "3M";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "9.8M";
                vm.Video_BufSize_Text = "9.8M";
                vm.Video_PixelFormat_SelectedItem = "yuv420p";
                vm.Video_FPS_SelectedItem = "ntsc";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "None";
                vm.Video_Scale_SelectedItem = "Custom";
                vm.Video_Width_Text = "720";
                vm.Video_Height_Text = "480";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_AspectRatio_SelectedItem = "16:9";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "SRT";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Codec_SelectedItem = "MP2";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "384";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "44.1k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }


            // ---------------------------------------------------------------------------
            // Music
            // ---------------------------------------------------------------------------
            // -------------------------
            // Lossless
            // -------------------------
            else if (vm.Preset_SelectedItem == "Lossless")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "flac";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "None";
                vm.Video_EncodeSpeed_SelectedItem = "none";
                vm.Video_Quality_SelectedItem = "None";
                vm.Video_Pass_SelectedItem = "auto";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "none";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "None";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "None";
                vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                vm.Audio_Codec_SelectedItem = "FLAC";
                vm.Audio_Stream_SelectedItem = "1";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "Lossless";
                vm.Audio_VBR_IsChecked = true;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // MP3
            // -------------------------
            else if (vm.Preset_SelectedItem == "MP3 HQ" ||
                     vm.Preset_SelectedItem == "MP3 Low")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp3";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Audio Only";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "None";
                vm.Video_EncodeSpeed_SelectedItem = "none";
                vm.Video_Quality_SelectedItem = "None";
                vm.Video_Pass_SelectedItem = "auto";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "none";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "None";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "None";
                vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                vm.Audio_Codec_SelectedItem = "LAME";
                vm.Audio_Stream_SelectedItem = "1";
                vm.Audio_Channel_SelectedItem = "Joint Stereo";

                // HQ
                if (vm.Preset_SelectedItem == "MP3 HQ")
                {
                    vm.Audio_Quality_SelectedItem = "320";
                }
                // Low
                else if (vm.Preset_SelectedItem == "MP3 Low")
                {
                    vm.Audio_Quality_SelectedItem = "160";
                }

                vm.Audio_VBR_IsChecked = true;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // iTunes
            // -------------------------
            else if (vm.Preset_SelectedItem == "iTunes")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "m4a";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Audio Only";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "None";
                vm.Video_EncodeSpeed_SelectedItem = "none";
                vm.Video_Quality_SelectedItem = "None";
                vm.Video_Pass_SelectedItem = "auto";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "none";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "None";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "None";
                vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Audio_Stream_SelectedItem = "1";
                vm.Audio_Channel_SelectedItem = "Stereo";
                vm.Audio_Quality_SelectedItem = "320";
                vm.Audio_VBR_IsChecked = true;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // Voice
            // -------------------------
            else if (vm.Preset_SelectedItem == "Voice")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "ogg";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Audio Only";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "None";
                vm.Video_EncodeSpeed_SelectedItem = "none";
                vm.Video_Quality_SelectedItem = "None";
                vm.Video_Pass_SelectedItem = "auto";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "none";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "None";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "None";
                vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                vm.Audio_Codec_SelectedItem = "Opus";
                vm.Audio_Stream_SelectedItem = "1";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "96";
                vm.Audio_VBR_IsChecked = true;
                vm.Audio_CompressionLevel_SelectedItem = "10";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // ---------------------------------------------------------------------------
            // YouTube
            // ---------------------------------------------------------------------------
            // -------------------------
            // Video
            // -------------------------
            else if (vm.Preset_SelectedItem == "Video Download")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Video + Audio";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "Copy";
                vm.Video_EncodeSpeed_SelectedItem = "none";
                vm.Video_Quality_SelectedItem = "Auto";
                vm.Video_Pass_SelectedItem = "auto";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "auto";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "None";
                vm.Video_Optimize_Tune_SelectedItem = "none";
                vm.Video_Optimize_Profile_SelectedItem = "none";
                vm.Video_Optimize_Level_SelectedItem = "none";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "Copy";
                vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                vm.Audio_Codec_SelectedItem = "Copy";
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "Auto";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // Music
            // -------------------------
            else if (vm.Preset_SelectedItem == "Music Download")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "m4a";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
                vm.Format_YouTube_SelectedItem = "Audio Only";
                vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                vm.Video_Codec_SelectedItem = "None";
                vm.Video_EncodeSpeed_SelectedItem = "none";
                vm.Video_Quality_SelectedItem = "None";
                vm.Video_Pass_SelectedItem = "auto";
                vm.Video_BitRate_Text = "";
                vm.Video_MinRate_Text = "";
                vm.Video_MaxRate_Text = "";
                vm.Video_BufSize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "none";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Speed_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "None";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_Crop_X_Text = "";
                vm.Video_Crop_Y_Text = "";
                vm.Video_Crop_Width_Text = "";
                vm.Video_Crop_Height_Text = "";
                vm.Video_CropClear_Text = "Clear";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "None";
                vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                vm.Audio_Codec_SelectedItem = "Copy";
                vm.Audio_Stream_SelectedItem = "1";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Quality_SelectedItem = "Auto";
                vm.Audio_VBR_IsChecked = true;
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

                // Filters
                vm.LoadFiltersDefault();
            }

            // -------------------------
            // Custom Preset
            // -------------------------
            else
            {
                // Get Preset INI Path
                string preset = string.Empty;

                foreach (string path in Profiles.customPresetPathsList)
                {
                    string filename = Path.GetFileNameWithoutExtension(path);

                    if (vm.Preset_SelectedItem == filename)
                    {
                        preset = path;
                        break;
                    }
                }

                // Import Preset INI File
                Profiles.ImportPreset(vm, preset);
            }


        }


    }
}
