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
                if (vm.Container_SelectedItem == "webm"
                    || vm.Container_SelectedItem == "mp4"
                    || vm.Container_SelectedItem == "mkv"
                    || vm.Container_SelectedItem == "m2v"
                    || vm.Container_SelectedItem == "mpg"
                    || vm.Container_SelectedItem == "avi"
                    || vm.Container_SelectedItem == "ogv"
                    || vm.Container_SelectedItem == "jpg"
                    || vm.Container_SelectedItem == "png"
                    || vm.Container_SelectedItem == "webp")
                {
                    //vm.Preset.IsEditable = false;

                    // Format
                    vm.Container_SelectedItem = vm.Container_Items.FirstOrDefault();

                    // Video
                    vm.VideoQuality_SelectedItem = "Auto";
                    vm.Pass_SelectedItem = "CRF";
                    vm.VideoBitrate_Text = "";
                    vm.VideoMinrate_Text = "";
                    vm.VideoMaxrate_Text = "";
                    vm.VideoBufsize_Text = "";
                    vm.Size_SelectedItem = "Source";
                    vm.AspectRatio_SelectedItem = "auto";
                    vm.ScalingAlgorithm_SelectedItem = "default";
                    vm.Cut_SelectedItem = "No";
                    vm.CutStart_Text = "00:00:00.000";
                    vm.CutEnd_Text = "00:00:00.000";
                    vm.VideoEncodeSpeed_SelectedItem = "Medium";
                    vm.FPS_SelectedItem = "auto";
                    vm.FPS_IsEnabled = true;

                    // Subtitle
                    vm.SubtitleCodec_SelectedItem = "None";
                    vm.SubtitleStream_SelectedItem = "none";

                    // Audio
                    vm.AudioStream_SelectedItem = "all";
                    vm.AudioQuality_SelectedItem = "Auto";
                    vm.AudioChannel_SelectedItem = "Source";
                    vm.AudioSampleRate_SelectedItem = "auto";
                    vm.AudioBitDepth_SelectedItem = "auto";
                    vm.Volume_Text = "100";
                    vm.AudioHardLimiter_Value = 1;
                    ////mainwindow.tglAudioLimiter.IsChecked = false;
                    ////mainwindow.audioLimiter.Text = string.Empty;

                    // special rules for webm
                    if (vm.Container_SelectedItem == "webm")
                    {
                        vm.SubtitleStream_SelectedItem = "none";
                        vm.AudioStream_SelectedItem = "1";
                        //vm.Video_Optimize_SelectedItem = "Web";
                    }
                    else
                    {
                        vm.Video_Optimize_SelectedItem = "None";
                    }

                }

                // -------------------------
                // Default Audio
                // -------------------------
                else if (vm.Container_SelectedItem == "m4a"
                    || vm.Container_SelectedItem == "mp3"
                    || vm.Container_SelectedItem == "ogg"
                    || vm.Container_SelectedItem == "flac"
                    || vm.Container_SelectedItem == "wav")
                {
                    //vm.Preset.IsEditable = false;

                    // Video
                    vm.Container_SelectedItem = vm.Container_Items.FirstOrDefault();
                    vm.VideoQuality_SelectedItem = "None";
                    vm.Pass_SelectedItem = "auto";
                    vm.VideoBitrate_Text = "";
                    vm.VideoMinrate_Text = "";
                    vm.VideoMaxrate_Text = "";
                    vm.VideoBufsize_Text = "";
                    vm.Size_SelectedItem = "Source";
                    vm.AspectRatio_SelectedItem = "auto";
                    vm.ScalingAlgorithm_SelectedItem = "default";
                    vm.Cut_SelectedItem = "No";
                    vm.CutStart_Text = "00:00:00.000";
                    vm.CutEnd_Text = "00:00:00.000";
                    vm.VideoEncodeSpeed_SelectedItem = "None";
                    vm.FPS_SelectedItem = "auto";
                    vm.FPS_IsEnabled = false;
                    vm.Video_Optimize_SelectedItem = "None";

                    // Subtitle
                    vm.SubtitleCodec_SelectedItem = "None";
                    vm.SubtitleStream_SelectedItem = "none";

                    // Audio
                    vm.AudioStream_SelectedItem = "1";
                    vm.AudioQuality_SelectedItem = "Auto";
                    vm.AudioChannel_SelectedItem = "Source";
                    vm.AudioSampleRate_SelectedItem = "auto";
                    // special rules for PCM codec
                    if ((string)vm.AudioCodec_SelectedItem == "PCM")
                    {
                        vm.AudioBitDepth_SelectedItem = "24";
                    }
                    else
                    {
                        vm.AudioBitDepth_SelectedItem = "auto";
                    }

                    vm.Volume_Text = "100";
                    vm.AudioHardLimiter_Value = 1;
                    //mainwindow.tglAudioLimiter.IsChecked = false;
                    //mainwindow.audioLimiter.Text = string.Empty;
                }
            }

            // -------------------------
            // DVD
            // -------------------------
            else if (vm.Preset_SelectedItem == "DVD")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Container_SelectedItem = "mpg";
                vm.VideoCodec_SelectedItem = "MPEG-2";
                vm.AudioCodec_SelectedItem = "MP2";

                // Video
                vm.VideoQuality_SelectedItem = "Custom";
                vm.Pass_SelectedItem = "2 Pass";
                vm.VideoBitrate_Text = "3M";
                vm.VideoMinrate_Text = "";
                vm.VideoMaxrate_Text = "9.8M";
                vm.VideoBufsize_Text = "9.8M";
                vm.Size_SelectedItem = "Custom";
                vm.AspectRatio_SelectedItem = "auto";
                vm.ScalingAlgorithm_SelectedItem = "default";
                vm.Width_Text = "720";
                vm.Height_Text = "auto";
                vm.Cut_SelectedItem = "No";
                vm.CutStart_Text = "00:00:00.000";
                vm.CutEnd_Text = "00:00:00.000";
                vm.VideoEncodeSpeed_SelectedItem = "None";
                vm.FPS_SelectedItem = "ntsc";
                vm.FPS_IsEnabled = true;
                vm.Video_Optimize_SelectedItem = "None";

                // Subtitle
                vm.SubtitleCodec_SelectedItem = "SRT";
                vm.SubtitleStream_SelectedItem = "all";

                // Audio
                vm.AudioStream_SelectedItem = "all";
                vm.AudioQuality_SelectedItem = "320";
                vm.AudioVBR_IsChecked = false;
                vm.AudioChannel_SelectedItem = "Source";
                vm.AudioSampleRate_SelectedItem = "44.1k";
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.Volume_Text = "100";
                vm.AudioHardLimiter_Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // HD Video
            // -------------------------
            else if (vm.Preset_SelectedItem == "HD Video")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Container_SelectedItem = "mp4";
                vm.VideoCodec_SelectedItem = "x264";
                vm.AudioCodec_SelectedItem = "AAC";

                // Video
                vm.VideoQuality_SelectedItem = "Ultra";
                vm.Pass_SelectedItem = "CRF";
                vm.VideoBitrate_Text = "";
                vm.VideoMinrate_Text = "";
                vm.VideoMaxrate_Text = "";
                vm.VideoBufsize_Text = "";
                vm.Size_SelectedItem = "Source";
                vm.AspectRatio_SelectedItem = "auto";
                vm.ScalingAlgorithm_SelectedItem = "default";
                vm.Video_Optimize_SelectedItem = "PC HD";
                vm.FPS_SelectedItem = "auto";
                vm.FPS_IsEnabled = true;
                vm.Cut_SelectedItem = "No";
                vm.CutStart_Text = "00:00:00.000";
                vm.CutEnd_Text = "00:00:00.000";
                vm.VideoEncodeSpeed_SelectedItem = "Medium";

                // Subtitle
                vm.SubtitleCodec_SelectedItem = "mov_text";
                vm.SubtitleStream_SelectedItem = "all";

                // Audio
                vm.AudioStream_SelectedItem = "all";
                vm.AudioQuality_SelectedItem = "Auto";
                vm.AudioVBR_IsChecked = false;
                vm.AudioChannel_SelectedItem = "Source";
                vm.AudioSampleRate_SelectedItem = "auto";
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.Volume_Text = "100";
                vm.AudioHardLimiter_Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // SD Video
            // -------------------------
            else if (vm.Preset_SelectedItem == "SD Video")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Container_SelectedItem = "mp4";
                vm.VideoCodec_SelectedItem = "x264";
                vm.AudioCodec_SelectedItem = "AC3";


                // Video
                vm.VideoQuality_SelectedItem = "High";
                vm.Pass_SelectedItem = "CRF";
                vm.VideoBitrate_Text = "";
                vm.VideoMinrate_Text = "";
                vm.VideoMaxrate_Text = "";
                vm.VideoBufsize_Text = "";
                vm.Size_SelectedItem = "Source";
                vm.AspectRatio_SelectedItem = "auto";
                vm.ScalingAlgorithm_SelectedItem = "default";
                vm.FPS_SelectedItem = "auto";
                vm.FPS_IsEnabled = true;
                vm.Cut_SelectedItem = "No";
                vm.CutStart_Text = "00:00:00.000";
                vm.CutEnd_Text = "00:00:00.000";
                vm.VideoEncodeSpeed_SelectedItem = "Medium";
                vm.Video_Optimize_SelectedItem = "PC SD";

                // Subtitle
                vm.SubtitleCodec_SelectedItem = "mov_text";
                vm.SubtitleStream_SelectedItem = "all";

                // Audio
                vm.AudioStream_SelectedItem = "all";
                vm.AudioQuality_SelectedItem = "256";
                vm.AudioVBR_IsChecked = false;
                vm.AudioChannel_SelectedItem = "Stereo";
                vm.AudioSampleRate_SelectedItem = "44.1k";
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.Volume_Text = "100";
                vm.AudioHardLimiter_Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // Blu-ray
            // -------------------------
            else if (vm.Preset_SelectedItem == "Blu-ray")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Container_SelectedItem = "mp4";
                vm.VideoCodec_SelectedItem = "x264";
                vm.AudioCodec_SelectedItem = "AC3";

                // Video
                vm.VideoQuality_SelectedItem = "Ultra";
                vm.Pass_SelectedItem = "CRF";
                vm.VideoBitrate_Text = "";
                vm.VideoMinrate_Text = "";
                vm.VideoMaxrate_Text = "";
                vm.VideoBufsize_Text = "";
                vm.Size_SelectedItem = "1080p";
                vm.AspectRatio_SelectedItem = "auto";
                vm.ScalingAlgorithm_SelectedItem = "default";
                vm.FPS_SelectedItem = "23.976";
                vm.FPS_IsEnabled = true;
                vm.Video_Optimize_SelectedItem = "Blu-ray";
                vm.Cut_SelectedItem = "No";
                vm.CutStart_Text = "00:00:00.000";
                vm.CutEnd_Text = "00:00:00.000";
                vm.VideoEncodeSpeed_SelectedItem = "Medium";

                // Subtitle
                vm.SubtitleCodec_SelectedItem = "mov_text";
                vm.SubtitleStream_SelectedItem = "all";

                // Audio
                vm.AudioStream_SelectedItem = "all";
                vm.AudioQuality_SelectedItem = "640";
                vm.AudioVBR_IsChecked = false;
                vm.AudioChannel_SelectedItem = "Source";
                vm.AudioSampleRate_SelectedItem = "48k";
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.Volume_Text = "100";
                vm.AudioHardLimiter_Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // iOS
            // -------------------------
            else if (vm.Preset_SelectedItem == "iOS")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Container_SelectedItem = "mp4";
                vm.VideoCodec_SelectedItem = "x264";
                vm.AudioCodec_SelectedItem = "AAC";

                // Video
                vm.VideoQuality_SelectedItem = "High";
                vm.Pass_SelectedItem = "CRF";
                vm.VideoBitrate_Text = "";
                vm.VideoMinrate_Text = "";
                vm.VideoMaxrate_Text = "";
                vm.VideoBufsize_Text = "";
                vm.Size_SelectedItem = "Source";
                vm.AspectRatio_SelectedItem = "auto";
                vm.ScalingAlgorithm_SelectedItem = "default";
                vm.FPS_SelectedItem = "23.976";
                vm.FPS_IsEnabled = true;
                vm.Cut_SelectedItem = "No";
                vm.CutStart_Text = "00:00:00.000";
                vm.CutEnd_Text = "00:00:00.000";
                vm.VideoEncodeSpeed_SelectedItem = "Medium";
                vm.Video_Optimize_SelectedItem = "Apple";

                // Subtitle
                vm.SubtitleCodec_SelectedItem = "mov_text";
                vm.SubtitleStream_SelectedItem = "all";

                // Audio
                vm.AudioStream_SelectedItem = "all";
                vm.AudioQuality_SelectedItem = "400";
                vm.AudioVBR_IsChecked = true;
                vm.AudioChannel_SelectedItem = "Stereo";
                vm.AudioSampleRate_SelectedItem = "44.1k";
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.Volume_Text = "100";
                vm.AudioHardLimiter_Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // Android
            // -------------------------
            else if (vm.Preset_SelectedItem == "Android")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Container_SelectedItem = "mp4";
                vm.VideoCodec_SelectedItem = "x264";
                vm.AudioCodec_SelectedItem = "AAC";

                // Video
                vm.VideoQuality_SelectedItem = "High";
                vm.Pass_SelectedItem = "CRF";
                vm.VideoBitrate_Text = "";
                vm.VideoMinrate_Text = "";
                vm.VideoMaxrate_Text = "";
                vm.VideoBufsize_Text = "";
                vm.Size_SelectedItem = "Source";
                vm.AspectRatio_SelectedItem = "auto";
                vm.ScalingAlgorithm_SelectedItem = "default";
                vm.Video_Optimize_SelectedItem = "Android";
                vm.FPS_SelectedItem = "23.976";
                vm.FPS_IsEnabled = true;
                vm.Cut_SelectedItem = "No";
                vm.CutStart_Text = "00:00:00.000";
                vm.CutEnd_Text = "00:00:00.000";
                vm.VideoEncodeSpeed_SelectedItem = "Medium";

                // Subtitle
                vm.SubtitleCodec_SelectedItem = "mov_text";
                vm.SubtitleStream_SelectedItem = "all";

                // Audio
                vm.AudioStream_SelectedItem = "all";
                vm.AudioQuality_SelectedItem = "400";
                vm.AudioVBR_IsChecked = true;
                vm.AudioChannel_SelectedItem = "Stereo";
                vm.AudioSampleRate_SelectedItem = "44.1k";
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.Volume_Text = "100";
                vm.AudioHardLimiter_Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // iTunes
            // -------------------------
            else if (vm.Preset_SelectedItem == "iTunes")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Container_SelectedItem = "m4a";
                vm.AudioCodec_SelectedItem = "AAC";

                // Video
                vm.VideoQuality_SelectedItem = "None";
                vm.Pass_SelectedItem = "auto";
                vm.VideoBitrate_Text = "";
                vm.VideoMinrate_Text = "";
                vm.VideoMaxrate_Text = "";
                vm.VideoBufsize_Text = "";
                vm.Size_SelectedItem = "Source";
                vm.AspectRatio_SelectedItem = "auto";
                vm.ScalingAlgorithm_SelectedItem = "default";
                vm.Cut_SelectedItem = "No";
                vm.CutStart_Text = "00:00:00.000";
                vm.CutEnd_Text = "00:00:00.000";
                vm.VideoEncodeSpeed_SelectedItem = "None";
                vm.FPS_SelectedItem = "auto";
                vm.FPS_IsEnabled = false;
                vm.Video_Optimize_SelectedItem = "None";

                // Subtitle
                vm.SubtitleCodec_SelectedItem = "None";
                vm.SubtitleStream_SelectedItem = "none";

                // Audio
                vm.AudioStream_SelectedItem = "1";
                vm.AudioQuality_SelectedItem = "320";
                vm.AudioVBR_IsChecked = true;
                vm.AudioChannel_SelectedItem = "Stereo";
                vm.AudioSampleRate_SelectedItem = "auto";
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.Volume_Text = "100";
                vm.AudioHardLimiter_Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // MP3 HQ
            // -------------------------
            else if (vm.Preset_SelectedItem == "MP3 HQ")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Container_SelectedItem = "mp3";
                vm.AudioCodec_SelectedItem = "LAME";

                // Video
                vm.VideoQuality_SelectedItem = "None";
                vm.Pass_SelectedItem = "auto";
                vm.VideoBitrate_Text = "";
                vm.VideoMinrate_Text = "";
                vm.VideoMaxrate_Text = "";
                vm.VideoBufsize_Text = "";
                vm.Size_SelectedItem = "Source";
                vm.AspectRatio_SelectedItem = "auto";
                vm.ScalingAlgorithm_SelectedItem = "default";
                vm.Cut_SelectedItem = "No";
                vm.CutStart_Text = "00:00:00.000";
                vm.CutEnd_Text = "00:00:00.000";
                vm.VideoEncodeSpeed_SelectedItem = "None";
                vm.FPS_SelectedItem = "auto";
                vm.FPS_IsEnabled = false;
                vm.Video_Optimize_SelectedItem = "None";

                // Subtitle
                vm.SubtitleCodec_SelectedItem = "None";
                vm.SubtitleStream_SelectedItem = "none";

                // Audio
                vm.AudioStream_SelectedItem = "1";
                vm.AudioQuality_SelectedItem = "320";
                vm.AudioVBR_IsChecked = true;
                vm.AudioChannel_SelectedItem = "Joint Stereo";
                vm.AudioSampleRate_SelectedItem = "auto";
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.Volume_Text = "100";
                vm.AudioHardLimiter_Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // PS3
            // -------------------------
            else if (vm.Preset_SelectedItem == "PS3")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Container_SelectedItem = "mp4";
                vm.VideoCodec_SelectedItem = "x264";
                vm.AudioCodec_SelectedItem = "AAC";

                // Video
                vm.VideoQuality_SelectedItem = "Ultra";
                vm.Pass_SelectedItem = "CRF";
                vm.VideoBitrate_Text = "";
                vm.VideoMinrate_Text = "";
                vm.VideoMaxrate_Text = "";
                vm.VideoBufsize_Text = "";
                vm.Size_SelectedItem = "Source";
                vm.AspectRatio_SelectedItem = "auto";
                vm.ScalingAlgorithm_SelectedItem = "default";
                vm.FPS_SelectedItem = "23.976";
                vm.FPS_IsEnabled = true;
                vm.Cut_SelectedItem = "No";
                vm.CutStart_Text = "00:00:00.000";
                vm.CutEnd_Text = "00:00:00.000";
                vm.VideoEncodeSpeed_SelectedItem = "Medium";
                vm.Video_Optimize_SelectedItem = "PS3";

                // Subtitle
                vm.SubtitleCodec_SelectedItem = "mov_text";
                vm.SubtitleStream_SelectedItem = "all";

                // Audio
                vm.AudioStream_SelectedItem = "all";
                vm.AudioQuality_SelectedItem = "400";
                vm.AudioVBR_IsChecked = false;
                vm.AudioChannel_SelectedItem = "Source";
                vm.AudioSampleRate_SelectedItem = "48k";
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.Volume_Text = "100";
                vm.AudioHardLimiter_Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // PS4
            // -------------------------
            else if (vm.Preset_SelectedItem == "PS4")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Container_SelectedItem = "mp4";
                vm.VideoCodec_SelectedItem = "x264";
                vm.AudioCodec_SelectedItem = "AAC";

                // Video
                vm.VideoQuality_SelectedItem = "Ultra";
                vm.Pass_SelectedItem = "CRF";
                vm.VideoBitrate_Text = "";
                vm.VideoMinrate_Text = "";
                vm.VideoMaxrate_Text = "";
                vm.VideoBufsize_Text = "";
                vm.Size_SelectedItem = "Source";
                vm.AspectRatio_SelectedItem = "auto";
                vm.ScalingAlgorithm_SelectedItem = "default";
                vm.Cut_SelectedItem = "No";
                vm.CutStart_Text = "00:00:00.000";
                vm.CutEnd_Text = "00:00:00.000";
                vm.VideoEncodeSpeed_SelectedItem = "Medium";
                vm.FPS_SelectedItem = "auto";
                vm.FPS_IsEnabled = true;
                vm.Video_Optimize_SelectedItem = "PS4";

                // Subtitle
                vm.SubtitleCodec_SelectedItem = "mov_text";
                vm.SubtitleStream_SelectedItem = "all";

                // Audio
                vm.AudioStream_SelectedItem = "all";
                vm.AudioQuality_SelectedItem = "Auto";
                vm.AudioChannel_SelectedItem = "Source";
                vm.AudioSampleRate_SelectedItem = "auto";
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.AudioVBR_IsChecked = false;
                vm.Volume_Text = "100";
                vm.AudioHardLimiter_Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // Xbox 360
            // -------------------------
            else if (vm.Preset_SelectedItem == "Xbox 360")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Container_SelectedItem = "mp4";
                vm.VideoCodec_SelectedItem = "x264";
                vm.AudioCodec_SelectedItem = "AAC";

                // Video
                vm.VideoQuality_SelectedItem = "High";
                vm.Pass_SelectedItem = "CRF";
                vm.VideoBitrate_Text = "";
                vm.VideoMinrate_Text = "";
                vm.VideoMaxrate_Text = "";
                vm.VideoBufsize_Text = "";
                vm.Size_SelectedItem = "Source";
                vm.AspectRatio_SelectedItem = "auto";
                vm.ScalingAlgorithm_SelectedItem = "default";
                vm.Cut_SelectedItem = "No";
                vm.CutStart_Text = "00:00:00.000";
                vm.CutEnd_Text = "00:00:00.000";
                vm.VideoEncodeSpeed_SelectedItem = "Medium";
                vm.FPS_SelectedItem = "23.976";
                vm.FPS_IsEnabled = true;
                vm.Video_Optimize_SelectedItem = "Xbox 360";

                // Subtitle
                vm.SubtitleCodec_SelectedItem = "mov_text";
                vm.SubtitleStream_SelectedItem = "all";

                // Audio
                vm.AudioStream_SelectedItem = "all";
                vm.AudioQuality_SelectedItem = "320";
                vm.AudioChannel_SelectedItem = "Stereo";
                vm.AudioSampleRate_SelectedItem = "48k";
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.AudioVBR_IsChecked = false;
                vm.Volume_Text = "100";
                vm.AudioHardLimiter_Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // Xbox One
            // -------------------------
            else if (vm.Preset_SelectedItem == "Xbox One")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Container_SelectedItem = "mp4";
                vm.VideoCodec_SelectedItem = "x264";
                vm.AudioCodec_SelectedItem = "AAC";

                // Video
                vm.VideoQuality_SelectedItem = "Ultra";
                vm.Pass_SelectedItem = "CRF";
                vm.VideoBitrate_Text = "";
                vm.VideoMinrate_Text = "";
                vm.VideoMaxrate_Text = "";
                vm.VideoBufsize_Text = "";
                vm.Size_SelectedItem = "Source";
                vm.AspectRatio_SelectedItem = "auto";
                vm.ScalingAlgorithm_SelectedItem = "default";
                vm.Cut_SelectedItem = "No";
                vm.CutStart_Text = "00:00:00.000";
                vm.CutEnd_Text = "00:00:00.000";
                vm.VideoEncodeSpeed_SelectedItem = "Medium";
                vm.Video_Optimize_SelectedItem = "Xbox One";
                vm.FPS_SelectedItem = "auto";
                vm.FPS_IsEnabled = true;

                // Subtitle
                vm.SubtitleCodec_SelectedItem = "mov_text";
                vm.SubtitleStream_SelectedItem = "all";

                // Audio
                vm.AudioStream_SelectedItem = "all";
                vm.AudioQuality_SelectedItem = "Auto";
                vm.AudioChannel_SelectedItem = "Source";
                vm.AudioSampleRate_SelectedItem = "auto";
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.AudioVBR_IsChecked = false;
                vm.Volume_Text = "100";
                vm.AudioHardLimiter_Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // HTML5
            // -------------------------
            else if (vm.Preset_SelectedItem == "HTML5")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Container_SelectedItem = "webm";
                vm.VideoCodec_SelectedItem = "VP8";
                vm.AudioCodec_SelectedItem = "Vorbis";

                // Video
                vm.VideoQuality_SelectedItem = "Medium";
                vm.Pass_SelectedItem = "CRF";
                //vm.VideoBitrate_Text = ""; // use quality preset bitrate
                vm.VideoMinrate_Text = "";
                vm.VideoMaxrate_Text = "";
                vm.VideoBufsize_Text = "";
                vm.Size_SelectedItem = "Source";
                vm.AspectRatio_SelectedItem = "auto";
                vm.ScalingAlgorithm_SelectedItem = "defualt";
                vm.Cut_SelectedItem = "No";
                vm.CutStart_Text = "00:00:00.000";
                vm.CutEnd_Text = "00:00:00.000";
                vm.VideoEncodeSpeed_SelectedItem = "Medium";
                vm.FPS_SelectedItem = "auto";
                vm.FPS_IsEnabled = true;
                vm.Video_Optimize_SelectedItem = "Web";

                // Subtitle
                vm.SubtitleCodec_SelectedItem = "None";
                vm.SubtitleStream_SelectedItem = "none";

                // Audio
                vm.AudioStream_SelectedItem = "1";
                vm.AudioQuality_SelectedItem = "192";
                vm.AudioVBR_IsChecked = true;
                vm.AudioChannel_SelectedItem = "Stereo";
                vm.AudioSampleRate_SelectedItem = "44.1k";
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.Volume_Text = "100";
                vm.AudioHardLimiter_Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // Debug
            // -------------------------
            else if (vm.Preset_SelectedItem == "Debug")
            {
                //vm.Preset.IsEditable = false;

                // Format
                vm.Container_SelectedItem = "mkv";
                vm.AudioCodec_SelectedItem = "Opus";

                // Video
                vm.VideoQuality_SelectedItem = "Custom";
                vm.Pass_SelectedItem = "CRF";
                vm.VideoBitrate_Text = "1250K";
                vm.VideoMinrate_Text = "";
                vm.VideoMaxrate_Text = "";
                vm.VideoBufsize_Text = "";
                vm.CRF_Text = "26";
                vm.FPS_SelectedItem = "29.97";
                vm.FPS_IsEnabled = true;
                vm.VideoCodec_SelectedItem = "x264";
                vm.Size_SelectedItem = "Custom";
                vm.AspectRatio_SelectedItem = "16:9";
                vm.ScalingAlgorithm_SelectedItem = "spline";
                vm.Width_Text = "545";
                vm.Height_Text = "307";
                vm.Cut_SelectedItem = "Yes";
                vm.CutStart_Text = "00:00:00.000";
                vm.CutEnd_Text = "00:00:05.300";
                vm.Video_Optimize_SelectedItem = "Windows";
                vm.VideoEncodeSpeed_SelectedItem = "Faster";

                // Subtitle
                vm.SubtitleCodec_SelectedItem = "SSA";
                vm.SubtitleStream_SelectedItem = "all";

                // Audio
                vm.AudioStream_SelectedItem = "1";
                vm.AudioQuality_SelectedItem = "Custom";
                vm.AudioBitrate_Text = "380";
                vm.AudioVBR_IsChecked = true;
                vm.AudioChannel_SelectedItem = "Stereo";
                vm.AudioSampleRate_SelectedItem = "48k";
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.Volume_Text = "120";
                vm.AudioHardLimiter_Value = 0.9;
                //mainwindow.tglAudioLimiter.IsChecked = true;
                //mainwindow.audioLimiter.Text = "0.90";
            }
        }
    }
}
