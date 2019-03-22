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

using System.Linq;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class Presets
    {
        public static void Preset(ViewModel vm)
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

            // -------------------------
            // Default
            // -------------------------
            if (vm.Preset_SelectedItem == "Preset" ||
                vm.Preset_SelectedItem == "Default")
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
                    vm.Format_Container_SelectedItem = "webm";
                    //vm.Format_Container_SelectedIndex = 1;
                    //vm.Format_Container_SelectedItem = vm.Format_Container_Items.FirstOrDefault();

                    // Video
                    vm.Video_Quality_SelectedItem = "Auto";
                    vm.Video_Pass_SelectedItem = "CRF";
                    vm.Video_Bitrate_Text = "";
                    vm.Video_Minrate_Text = "";
                    vm.Video_Maxrate_Text = "";
                    vm.Video_Bufsize_Text = "";
                    vm.Video_Scale_SelectedItem = "Source";
                    vm.Video_AspectRatio_SelectedItem = "auto";
                    vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                    vm.Format_Cut_SelectedItem = "No";
                    vm.Format_CutStart_Text = "00:00:00.000";
                    vm.Format_CutEnd_Text = "00:00:00.000";
                    vm.Video_EncodeSpeed_SelectedItem = "Medium";
                    vm.Video_FPS_SelectedItem = "auto";
                    vm.Video_FPS_IsEnabled = true;

                    // Subtitle
                    vm.Subtitle_Codec_SelectedItem = "None";
                    vm.Subtitle_Stream_SelectedItem = "none";

                    // Audio
                    vm.Audio_Stream_SelectedItem = "all";
                    vm.Audio_Quality_SelectedItem = "Auto";
                    vm.Audio_Channel_SelectedItem = "Source";
                    vm.Audio_SampleRate_SelectedItem = "auto";
                    vm.Audio_BitDepth_SelectedItem = "auto";
                    vm.Audio_Volume_Text = "100";
                    vm.Audio_HardLimiter_Value = 1;

                    // special rules for webm
                    if (vm.Format_Container_SelectedItem == "webm")
                    {
                        vm.Subtitle_Stream_SelectedItem = "none";
                        vm.Audio_Stream_SelectedItem = "1";
                        //vm.Video_Optimize_SelectedItem = "Web";
                    }
                    else
                    {
                        vm.Video_Optimize_SelectedItem = "None";
                        vm.Video_Optimize_Tune_SelectedItem = "none";
                        vm.Video_Optimize_Profile_SelectedItem = "main";
                        vm.Video_Optimize_Level_SelectedItem = "5.2";
                    }

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
                    vm.Format_Container_SelectedItem = "mp3";
                    vm.Video_Codec_SelectedItem = "None";
                    vm.Audio_Codec_SelectedItem = "LAME";
                    vm.Format_Container_SelectedIndex = 1;
                    vm.Format_Cut_SelectedItem = "No";
                    vm.Format_CutStart_Text = "00:00:00.000";
                    vm.Format_CutEnd_Text = "00:00:00.000";

                    // Video
                    vm.Video_Quality_SelectedItem = "None";
                    vm.Video_Pass_SelectedItem = "auto";
                    vm.Video_Bitrate_Text = "";
                    vm.Video_Minrate_Text = "";
                    vm.Video_Maxrate_Text = "";
                    vm.Video_Bufsize_Text = "";
                    vm.Video_Scale_SelectedItem = "Source";
                    vm.Video_AspectRatio_SelectedItem = "auto";
                    vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                    vm.Video_EncodeSpeed_SelectedItem = "None";
                    vm.Video_FPS_SelectedItem = "auto";
                    vm.Video_FPS_IsEnabled = false;
                    vm.Video_Optimize_SelectedItem = "None";

                    // Subtitle
                    vm.Subtitle_Codec_SelectedItem = "None";
                    vm.Subtitle_Stream_SelectedItem = "none";

                    // Audio
                    vm.Audio_Stream_SelectedItem = "1";
                    vm.Audio_Quality_SelectedItem = "Auto";
                    vm.Audio_Channel_SelectedItem = "Source";
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
                }
            }

            // -------------------------
            // DVD
            // -------------------------
            else if (vm.Preset_SelectedItem == "DVD")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mpg";
                vm.Video_Codec_SelectedItem = "MPEG-2";
                vm.Audio_Codec_SelectedItem = "MP2";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Text = "00:00:00.000";
                vm.Format_CutEnd_Text = "00:00:00.000";

                // Video
                vm.Video_Quality_SelectedItem = "Custom";
                vm.Video_Pass_SelectedItem = "2 Pass";
                vm.Video_Bitrate_Text = "3M";
                vm.Video_Minrate_Text = "";
                vm.Video_Maxrate_Text = "9.8M";
                vm.Video_Bufsize_Text = "9.8M";
                vm.Video_Scale_SelectedItem = "Custom";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_Width_Text = "720";
                vm.Video_Height_Text = "auto";
                vm.Video_EncodeSpeed_SelectedItem = "None";
                vm.Video_FPS_SelectedItem = "ntsc";
                vm.Video_FPS_IsEnabled = true;
                vm.Video_Optimize_SelectedItem = "None";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "SRT";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Quality_SelectedItem = "320";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_SampleRate_SelectedItem = "44.1k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;
            }

            // -------------------------
            // HEVC
            // -------------------------
            else if (vm.Preset_SelectedItem == "HEVC")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mkv";
                vm.Video_Codec_SelectedItem = "x265";
                vm.Subtitle_Codec_SelectedItem = "Copy";
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Text = "00:00:00.000";
                vm.Format_CutEnd_Text = "00:00:00.000";

                // Video
                vm.Video_Quality_SelectedItem = "Ultra";
                vm.Video_Pass_SelectedItem = "CRF";
                //vm.Video_CRF_Text = "18";
                vm.Video_Bitrate_Text = "";
                vm.Video_Minrate_Text = "";
                vm.Video_Maxrate_Text = "";
                vm.Video_Bufsize_Text = "";
                vm.Video_PixelFormat_SelectedItem = "yuv420p10le";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "Custom";
                vm.Video_Optimize_Tune_SelectedItem = "none";
                vm.Video_Optimize_Profile_SelectedItem = "main10";
                vm.Video_Optimize_Level_SelectedItem = "5.2";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_FPS_IsEnabled = true;
                vm.Video_EncodeSpeed_SelectedItem = "Slow";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "mov_text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Quality_SelectedItem = "Auto";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;
            }

            // -------------------------
            // HD Video
            // -------------------------
            else if (vm.Preset_SelectedItem == "HD Video")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Video_Codec_SelectedItem = "x264";
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Text = "00:00:00.000";
                vm.Format_CutEnd_Text = "00:00:00.000";

                // Video
                vm.Video_Quality_SelectedItem = "Ultra";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_Bitrate_Text = "";
                vm.Video_Minrate_Text = "";
                vm.Video_Maxrate_Text = "";
                vm.Video_Bufsize_Text = "";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "PC HD";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_FPS_IsEnabled = true;
                vm.Video_EncodeSpeed_SelectedItem = "Medium";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "mov_text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Quality_SelectedItem = "Auto";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;
            }

            // -------------------------
            // SD Video
            // -------------------------
            else if (vm.Preset_SelectedItem == "SD Video")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Video_Codec_SelectedItem = "x264";
                vm.Audio_Codec_SelectedItem = "AC3";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Text = "00:00:00.000";
                vm.Format_CutEnd_Text = "00:00:00.000";

                // Video
                vm.Video_Quality_SelectedItem = "High";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_Bitrate_Text = "";
                vm.Video_Minrate_Text = "";
                vm.Video_Maxrate_Text = "";
                vm.Video_Bufsize_Text = "";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_FPS_IsEnabled = true;
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Optimize_SelectedItem = "PC SD";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "mov_text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Quality_SelectedItem = "256";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_Channel_SelectedItem = "Stereo";
                vm.Audio_SampleRate_SelectedItem = "44.1k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;
            }

            // -------------------------
            // Blu-ray
            // -------------------------
            else if (vm.Preset_SelectedItem == "Blu-ray")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Video_Codec_SelectedItem = "x264";
                vm.Audio_Codec_SelectedItem = "AC3";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Text = "00:00:00.000";
                vm.Format_CutEnd_Text = "00:00:00.000";

                // Video
                vm.Video_Quality_SelectedItem = "Ultra";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_Bitrate_Text = "";
                vm.Video_Minrate_Text = "";
                vm.Video_Maxrate_Text = "";
                vm.Video_Bufsize_Text = "";
                vm.Video_Scale_SelectedItem = "1080p";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_FPS_SelectedItem = "23.976";
                vm.Video_FPS_IsEnabled = true;
                vm.Video_Optimize_SelectedItem = "Blu-ray";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "mov_text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Quality_SelectedItem = "640";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_SampleRate_SelectedItem = "48k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;
            }

            // -------------------------
            // iOS
            // -------------------------
            else if (vm.Preset_SelectedItem == "iOS")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Video_Codec_SelectedItem = "x264";
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Text = "00:00:00.000";
                vm.Format_CutEnd_Text = "00:00:00.000";

                // Video
                vm.Video_Quality_SelectedItem = "High";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_Bitrate_Text = "";
                vm.Video_Minrate_Text = "";
                vm.Video_Maxrate_Text = "";
                vm.Video_Bufsize_Text = "";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_FPS_SelectedItem = "23.976";
                vm.Video_FPS_IsEnabled = true;
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Optimize_SelectedItem = "Apple";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "mov_text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Quality_SelectedItem = "400";
                vm.Audio_VBR_IsChecked = true;
                vm.Audio_Channel_SelectedItem = "Stereo";
                vm.Audio_SampleRate_SelectedItem = "44.1k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;
            }

            // -------------------------
            // Android
            // -------------------------
            else if (vm.Preset_SelectedItem == "Android")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Video_Codec_SelectedItem = "x264";
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Text = "00:00:00.000";
                vm.Format_CutEnd_Text = "00:00:00.000";

                // Video
                vm.Video_Quality_SelectedItem = "High";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_Bitrate_Text = "";
                vm.Video_Minrate_Text = "";
                vm.Video_Maxrate_Text = "";
                vm.Video_Bufsize_Text = "";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "Android";
                vm.Video_FPS_SelectedItem = "23.976";
                vm.Video_FPS_IsEnabled = true;
                vm.Video_EncodeSpeed_SelectedItem = "Medium";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "mov_text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Quality_SelectedItem = "400";
                vm.Audio_VBR_IsChecked = true;
                vm.Audio_Channel_SelectedItem = "Stereo";
                vm.Audio_SampleRate_SelectedItem = "44.1k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;
            }

            // -------------------------
            // iTunes
            // -------------------------
            else if (vm.Preset_SelectedItem == "iTunes")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "m4a";
                vm.Video_Codec_SelectedItem = "None";
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Text = "00:00:00.000";
                vm.Format_CutEnd_Text = "00:00:00.000";

                // Video
                vm.Video_Quality_SelectedItem = "None";
                vm.Video_Pass_SelectedItem = "auto";
                vm.Video_Bitrate_Text = "";
                vm.Video_Minrate_Text = "";
                vm.Video_Maxrate_Text = "";
                vm.Video_Bufsize_Text = "";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_EncodeSpeed_SelectedItem = "None";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_FPS_IsEnabled = false;
                vm.Video_Optimize_SelectedItem = "None";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "None";
                vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                vm.Audio_Stream_SelectedItem = "1";
                vm.Audio_Quality_SelectedItem = "320";
                vm.Audio_VBR_IsChecked = true;
                vm.Audio_Channel_SelectedItem = "Stereo";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;
            }

            // -------------------------
            // MP3 HQ
            // -------------------------
            else if (vm.Preset_SelectedItem == "MP3 HQ")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp3";
                vm.Video_Codec_SelectedItem = "None";
                vm.Audio_Codec_SelectedItem = "LAME";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Text = "00:00:00.000";
                vm.Format_CutEnd_Text = "00:00:00.000";

                // Video
                vm.Video_Quality_SelectedItem = "None";
                vm.Video_Pass_SelectedItem = "auto";
                vm.Video_Bitrate_Text = "";
                vm.Video_Minrate_Text = "";
                vm.Video_Maxrate_Text = "";
                vm.Video_Bufsize_Text = "";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_EncodeSpeed_SelectedItem = "None";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_FPS_IsEnabled = false;
                vm.Video_Optimize_SelectedItem = "None";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "None";
                vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                vm.Audio_Stream_SelectedItem = "1";
                vm.Audio_Quality_SelectedItem = "320";
                vm.Audio_VBR_IsChecked = true;
                vm.Audio_Channel_SelectedItem = "Joint Stereo";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;
            }

            // -------------------------
            // PS3
            // -------------------------
            else if (vm.Preset_SelectedItem == "PS3")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Video_Codec_SelectedItem = "x264";
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Text = "00:00:00.000";
                vm.Format_CutEnd_Text = "00:00:00.000";

                // Video
                vm.Video_Quality_SelectedItem = "Ultra";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_Bitrate_Text = "";
                vm.Video_Minrate_Text = "";
                vm.Video_Maxrate_Text = "";
                vm.Video_Bufsize_Text = "";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_FPS_SelectedItem = "23.976";
                vm.Video_FPS_IsEnabled = true;
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Optimize_SelectedItem = "PS3";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "mov_text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Quality_SelectedItem = "400";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_SampleRate_SelectedItem = "48k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;
            }

            // -------------------------
            // PS4
            // -------------------------
            else if (vm.Preset_SelectedItem == "PS4")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Video_Codec_SelectedItem = "x264";
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Text = "00:00:00.000";
                vm.Format_CutEnd_Text = "00:00:00.000";

                // Video
                vm.Video_Quality_SelectedItem = "Ultra";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_Bitrate_Text = "";
                vm.Video_Minrate_Text = "";
                vm.Video_Maxrate_Text = "";
                vm.Video_Bufsize_Text = "";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_FPS_IsEnabled = true;
                vm.Video_Optimize_SelectedItem = "PS4";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "mov_text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Quality_SelectedItem = "Auto";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;
            }

            // -------------------------
            // Xbox 360
            // -------------------------
            else if (vm.Preset_SelectedItem == "Xbox 360")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Video_Codec_SelectedItem = "x264";
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Text = "00:00:00.000";
                vm.Format_CutEnd_Text = "00:00:00.000";

                // Video
                vm.Video_Quality_SelectedItem = "High";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_Bitrate_Text = "";
                vm.Video_Minrate_Text = "";
                vm.Video_Maxrate_Text = "";
                vm.Video_Bufsize_Text = "";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_FPS_SelectedItem = "23.976";
                vm.Video_FPS_IsEnabled = true;
                vm.Video_Optimize_SelectedItem = "Xbox 360";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "mov_text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Quality_SelectedItem = "320";
                vm.Audio_Channel_SelectedItem = "Stereo";
                vm.Audio_SampleRate_SelectedItem = "48k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;
            }

            // -------------------------
            // Xbox One
            // -------------------------
            else if (vm.Preset_SelectedItem == "Xbox One")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mp4";
                vm.Video_Codec_SelectedItem = "x264";
                vm.Audio_Codec_SelectedItem = "AAC";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Text = "00:00:00.000";
                vm.Format_CutEnd_Text = "00:00:00.000";

                // Video
                vm.Video_Quality_SelectedItem = "Ultra";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_Bitrate_Text = "";
                vm.Video_Minrate_Text = "";
                vm.Video_Maxrate_Text = "";
                vm.Video_Bufsize_Text = "";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_Optimize_SelectedItem = "Xbox One";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_FPS_IsEnabled = true;

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "mov_text";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Stream_SelectedItem = "all";
                vm.Audio_Quality_SelectedItem = "Auto";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_VBR_IsChecked = false;
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;

            }

            // -------------------------
            // HTML5
            // -------------------------
            else if (vm.Preset_SelectedItem == "HTML5")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "webm";
                vm.Video_Codec_SelectedItem = "VP8";
                vm.Audio_Codec_SelectedItem = "Vorbis";
                vm.Format_Cut_SelectedItem = "No";
                vm.Format_CutStart_Text = "00:00:00.000";
                vm.Format_CutEnd_Text = "00:00:00.000";

                // Video
                vm.Video_Quality_SelectedItem = "Medium";
                vm.Video_Pass_SelectedItem = "CRF";
                //vm.Video_Bitrate_Text = ""; // use quality preset bitrate
                vm.Video_Minrate_Text = "";
                vm.Video_Maxrate_Text = "";
                vm.Video_Bufsize_Text = "";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_AspectRatio_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_SelectedItem = "defualt";
                vm.Video_EncodeSpeed_SelectedItem = "Medium";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_FPS_IsEnabled = true;
                vm.Video_Optimize_SelectedItem = "Web";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "None";
                vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                vm.Audio_Stream_SelectedItem = "1";
                vm.Audio_Quality_SelectedItem = "192";
                vm.Audio_VBR_IsChecked = true;
                vm.Audio_Channel_SelectedItem = "Stereo";
                vm.Audio_SampleRate_SelectedItem = "44.1k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;
            }

            // -------------------------
            // Debug
            // -------------------------
            else if (vm.Preset_SelectedItem == "Debug")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Format_Container_SelectedItem = "mkv";
                vm.Video_Codec_SelectedItem = "x264";
                vm.Audio_Codec_SelectedItem = "Opus";
                vm.Format_Cut_SelectedItem = "Yes";
                vm.Format_CutStart_Text = "00:00:00.000";
                vm.Format_CutEnd_Text = "00:00:05.300";

                // Video
                vm.Video_Quality_SelectedItem = "Custom";
                vm.Video_Pass_SelectedItem = "CRF";
                vm.Video_Bitrate_Text = "1250K";
                vm.Video_Minrate_Text = "";
                vm.Video_Maxrate_Text = "";
                vm.Video_Bufsize_Text = "";
                vm.Video_CRF_Text = "26";
                vm.Video_FPS_SelectedItem = "29.97";
                vm.Video_FPS_IsEnabled = true;
                vm.Video_Scale_SelectedItem = "Custom";
                vm.Video_AspectRatio_SelectedItem = "16:9";
                vm.Video_ScalingAlgorithm_SelectedItem = "spline";
                vm.Video_Width_Text = "545";
                vm.Video_Height_Text = "307";
                vm.Video_Optimize_SelectedItem = "Windows";
                vm.Video_EncodeSpeed_SelectedItem = "Faster";

                // Subtitle
                vm.Subtitle_Codec_SelectedItem = "SSA";
                vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                vm.Audio_Stream_SelectedItem = "1";
                vm.Audio_Quality_SelectedItem = "Custom";
                vm.Audio_Bitrate_Text = "380";
                vm.Audio_VBR_IsChecked = true;
                vm.Audio_Channel_SelectedItem = "Stereo";
                vm.Audio_SampleRate_SelectedItem = "48k";
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_Volume_Text = "120";
                vm.Audio_HardLimiter_Value = 0.9;
            }
        }
    }
}
