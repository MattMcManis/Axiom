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
        public static void SetPreset()
        {
            // ---------------------------------------------------------------------------
            // Default
            // ---------------------------------------------------------------------------
            if (MainView.vm.Preset_SelectedItem == "Preset")
            {
                // -------------------------
                // Default Video
                // -------------------------
                if (FormatView.vm.Format_Container_SelectedItem == "webm" ||
                    FormatView.vm.Format_Container_SelectedItem == "mp4" ||
                    FormatView.vm.Format_Container_SelectedItem == "mkv" ||
                    FormatView.vm.Format_Container_SelectedItem == "m2v" ||
                    FormatView.vm.Format_Container_SelectedItem == "mpg" ||
                    FormatView.vm.Format_Container_SelectedItem == "avi" ||
                    FormatView.vm.Format_Container_SelectedItem == "ogv" ||
                    FormatView.vm.Format_Container_SelectedItem == "jpg" ||
                    FormatView.vm.Format_Container_SelectedItem == "png" ||
                    FormatView.vm.Format_Container_SelectedItem == "webp")
                {
                    //MainView.vm.Preset.IsEditable = false;

                    // Format
                    //FormatView.vm.Format_Container_SelectedItem = FormatView.vm.Format_Container_Items.FirstOrDefault();
                    FormatView.vm.Format_Container_SelectedItem = "webm";
                    FormatView.vm.Format_Cut_SelectedItem = "No";
                    FormatView.vm.Format_CutStart_Hours_Text = "00";
                    FormatView.vm.Format_CutStart_Minutes_Text = "00";
                    FormatView.vm.Format_CutStart_Seconds_Text = "00";
                    FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                    FormatView.vm.Format_CutEnd_Hours_Text = "00";
                    FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                    FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                    FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                    FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                    FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                    // Video
                    //VideoView.vm.Video_Codec_SelectedItem = "VP8";
                    VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";
                    VideoView.vm.Video_Quality_SelectedItem = "Auto";
                    VideoView.vm.Video_Pass_SelectedItem = "CRF";
                    VideoView.vm.Video_BitRate_Text = "";
                    VideoView.vm.Video_MinRate_Text = "";
                    VideoView.vm.Video_MaxRate_Text = "";
                    VideoView.vm.Video_BufSize_Text = "";
                    VideoView.vm.Video_FPS_SelectedItem = "auto";
                    VideoView.vm.Video_Speed_SelectedItem = "auto";
                    VideoView.vm.Video_Scale_SelectedItem = "Source";
                    VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                    VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                    VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                    VideoView.vm.Video_Crop_X_Text = "";
                    VideoView.vm.Video_Crop_Y_Text = "";
                    VideoView.vm.Video_Crop_Width_Text = "";
                    VideoView.vm.Video_Crop_Height_Text = "";
                    VideoView.vm.Video_CropClear_Text = "Clear";

                    // Subtitle
                    //SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
                    SubtitleView.vm.Subtitle_Stream_SelectedItem = "none";

                    // Audio
                    //AudioView.vm.Audio_Codec_SelectedItem = "Vorbis";
                    AudioView.vm.Audio_Stream_SelectedItem = "all";
                    AudioView.vm.Audio_Channel_SelectedItem = "Source";
                    AudioView.vm.Audio_Quality_SelectedItem = "Auto";
                    AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                    AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                    AudioView.vm.Audio_Volume_Text = "100";
                    AudioView.vm.Audio_HardLimiter_Value = 1;

                    // special rules for webm
                    if (FormatView.vm.Format_Container_SelectedItem == "webm")
                    {
                        SubtitleView.vm.Subtitle_Stream_SelectedItem = "none";
                        AudioView.vm.Audio_Stream_SelectedItem = "1";
                        VideoView.vm.Video_Optimize_SelectedItem = "Web";
                    }
                    else
                    {
                        VideoView.vm.Video_Optimize_SelectedItem = "None";
                        VideoView.vm.Video_Video_Optimize_Tune_SelectedItem = "none";
                        VideoView.vm.Video_Video_Optimize_Profile_SelectedItem = "main";
                        VideoView.vm.Video_Optimize_Level_SelectedItem = "5.2";
                    }

                    // Filters
                    FilterVideoView.vm.LoadFilterVideoDefaults();
                    FilterAudioView.vm.LoadFilterAudioDefaults();

                }

                // -------------------------
                // Default Audio
                // -------------------------
                else if (FormatView.vm.Format_Container_SelectedItem == "m4a" ||
                         FormatView.vm.Format_Container_SelectedItem == "mp3" ||
                         FormatView.vm.Format_Container_SelectedItem == "ogg" ||
                         FormatView.vm.Format_Container_SelectedItem == "flac" ||
                         FormatView.vm.Format_Container_SelectedItem == "wav")
                {
                    //MainView.vm.Preset.IsEditable = false;

                    // Format
                    //FormatView.vm.Format_Container_SelectedItem = FormatView.vm.Format_Container_Items.FirstOrDefault();
                    //FormatView.vm.Format_Container_SelectedIndex = 1;
                    FormatView.vm.Format_Container_SelectedItem = "mp3";
                    FormatView.vm.Format_Cut_SelectedItem = "No";
                    FormatView.vm.Format_CutStart_Hours_Text = "00";
                    FormatView.vm.Format_CutStart_Minutes_Text = "00";
                    FormatView.vm.Format_CutStart_Seconds_Text = "00";
                    FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                    FormatView.vm.Format_CutEnd_Hours_Text = "00";
                    FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                    FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                    FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                    FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                    FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                    // Video
                    VideoView.vm.Video_Codec_SelectedItem = "None";
                    VideoView.vm.Video_EncodeSpeed_SelectedItem = "none";
                    VideoView.vm.Video_Quality_SelectedItem = "None";
                    VideoView.vm.Video_Pass_SelectedItem = "auto";
                    VideoView.vm.Video_BitRate_Text = "";
                    VideoView.vm.Video_MinRate_Text = "";
                    VideoView.vm.Video_MaxRate_Text = "";
                    VideoView.vm.Video_BufSize_Text = "";
                    VideoView.vm.Video_PixelFormat_SelectedItem = "none";
                    VideoView.vm.Video_FPS_SelectedItem = "auto";
                    VideoView.vm.Video_Speed_SelectedItem = "auto";
                    VideoView.vm.Video_Optimize_SelectedItem = "None";
                    VideoView.vm.Video_Scale_SelectedItem = "Source";
                    VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                    VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                    VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                    VideoView.vm.Video_Crop_X_Text = "";
                    VideoView.vm.Video_Crop_Y_Text = "";
                    VideoView.vm.Video_Crop_Width_Text = "";
                    VideoView.vm.Video_Crop_Height_Text = "";
                    VideoView.vm.Video_CropClear_Text = "Clear";

                    // Subtitle
                    SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
                    SubtitleView.vm.Subtitle_Stream_SelectedItem = "none";

                    // Audio
                    AudioView.vm.Audio_Codec_SelectedItem = "LAME";
                    AudioView.vm.Audio_Stream_SelectedItem = "1";
                    AudioView.vm.Audio_Channel_SelectedItem = "Source";
                    AudioView.vm.Audio_Quality_SelectedItem = "Auto";
                    AudioView.vm.Audio_VBR_IsChecked = false;
                    AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                    AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                    // special rules for PCM codec
                    if (AudioView.vm.Audio_Codec_SelectedItem == "PCM")
                    {
                        AudioView.vm.Audio_BitDepth_SelectedItem = "24";
                    }
                    else
                    {
                        AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                    }

                    AudioView.vm.Audio_Volume_Text = "100";
                    AudioView.vm.Audio_HardLimiter_Value = 1;

                    // Filters
                    FilterVideoView.vm.LoadFilterVideoDefaults();
                    FilterAudioView.vm.LoadFilterAudioDefaults();
                }
            }


            // ---------------------------------------------------------------------------
            // Web
            // ---------------------------------------------------------------------------
            // -------------------------
            // HTML5
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "HTML5")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "webm";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "VP8";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";
                VideoView.vm.Video_Quality_SelectedItem = "Medium";
                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                //VideoView.vm.Video_BitRate_Text = ""; // use quality preset bitrate
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "Web";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "Vorbis";
                AudioView.vm.Audio_Stream_SelectedItem = "1";
                AudioView.vm.Audio_Channel_SelectedItem = "Stereo";
                AudioView.vm.Audio_Quality_SelectedItem = "192";
                AudioView.vm.Audio_VBR_IsChecked = true;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "44.1k";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // YouTube
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "YouTube")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x264";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";
                VideoView.vm.Video_Quality_SelectedItem = "Ultra";
                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "Web";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "MOV Text";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "Auto";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }


            // ---------------------------------------------------------------------------
            // PC
            // ---------------------------------------------------------------------------
            // -------------------------
            // Archive Video
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "Archive")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mkv";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "FFV1";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "none";
                VideoView.vm.Video_Quality_SelectedItem = "Lossless";
                VideoView.vm.Video_Pass_SelectedItem = "2 Pass";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv444p10le";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "None";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "Copy";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "FLAC";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "Auto";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // HEVC Ultra
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "HEVC Ultra" ||
                     MainView.vm.Preset_SelectedItem == "HEVC High")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mkv";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x265";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Slow";

                if (MainView.vm.Preset_SelectedItem == "HEVC Ultra")
                {
                    VideoView.vm.Video_Quality_SelectedItem = "Ultra";
                }
                else if (MainView.vm.Preset_SelectedItem == "HEVC High")
                {
                    VideoView.vm.Video_Quality_SelectedItem = "High";
                }

                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                //VideoView.vm.Video_CRF_Text = ""; // Use Quality Preset Value
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p10le";
                VideoView.vm.Video_FPS_SelectedItem = "23.976";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "Custom";
                VideoView.vm.Video_Video_Optimize_Tune_SelectedItem = "none";
                VideoView.vm.Video_Video_Optimize_Profile_SelectedItem = "main10";
                VideoView.vm.Video_Optimize_Level_SelectedItem = "5.2";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";


                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "Copy";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "Auto";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // HD Video
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "HD Ultra" ||
                     MainView.vm.Preset_SelectedItem == "HD High" ||
                     MainView.vm.Preset_SelectedItem == "HD Medium" ||
                     MainView.vm.Preset_SelectedItem == "HD Low"
                     )
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x264";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";

                if (MainView.vm.Preset_SelectedItem == "HD Ultra")
                {
                    VideoView.vm.Video_Quality_SelectedItem = "Ultra";
                }
                else if (MainView.vm.Preset_SelectedItem == "HD High")
                {
                    VideoView.vm.Video_Quality_SelectedItem = "High";
                }
                else if (MainView.vm.Preset_SelectedItem == "HD Medium")
                {
                    VideoView.vm.Video_Quality_SelectedItem = "Medium";
                }
                else if (MainView.vm.Preset_SelectedItem == "HD Low")
                {
                    VideoView.vm.Video_Quality_SelectedItem = "Low";
                }

                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "PC HD";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "MOV Text";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "Auto";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // SD Video
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "SD High" ||
                     MainView.vm.Preset_SelectedItem == "SD Medium" ||
                     MainView.vm.Preset_SelectedItem == "SD Low")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x264";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";

                if (MainView.vm.Preset_SelectedItem == "SD High")
                {
                    VideoView.vm.Video_Quality_SelectedItem = "High";
                }
                else if (MainView.vm.Preset_SelectedItem == "SD Medium")
                {
                    VideoView.vm.Video_Quality_SelectedItem = "Medium";
                }
                else if (MainView.vm.Preset_SelectedItem == "SD Low")
                {
                    VideoView.vm.Video_Quality_SelectedItem = "Low";
                }

                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "PC SD";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "MOV Text";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AC3";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Stereo";
                AudioView.vm.Audio_Quality_SelectedItem = "256";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "44.1k";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }


            // ---------------------------------------------------------------------------
            // Device
            // ---------------------------------------------------------------------------
            // -------------------------
            // Roku
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "Roku")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mkv";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x264";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";
                VideoView.vm.Video_Quality_SelectedItem = "Ultra";
                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "Custom";
                VideoView.vm.Video_Video_Optimize_Tune_SelectedItem = "none";
                VideoView.vm.Video_Video_Optimize_Profile_SelectedItem = "high";
                VideoView.vm.Video_Optimize_Level_SelectedItem = "4.0";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "SRT";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "160";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Amazon Fire
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "Amazon Fire")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x264";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";
                VideoView.vm.Video_Quality_SelectedItem = "Ultra";
                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "Custom";
                VideoView.vm.Video_Video_Optimize_Tune_SelectedItem = "none";
                VideoView.vm.Video_Video_Optimize_Profile_SelectedItem = "high";
                VideoView.vm.Video_Optimize_Level_SelectedItem = "4.2";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "SRT";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "160";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Chromecast
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "Chromecast")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x264";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";
                VideoView.vm.Video_Quality_SelectedItem = "Ultra";
                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "Custom";
                VideoView.vm.Video_Video_Optimize_Tune_SelectedItem = "none";
                VideoView.vm.Video_Video_Optimize_Profile_SelectedItem = "high";
                VideoView.vm.Video_Optimize_Level_SelectedItem = "4.2";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "SRT";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "160";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Apple TV
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "Apple TV")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x264";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";
                VideoView.vm.Video_Quality_SelectedItem = "Ultra";
                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "Custom";
                VideoView.vm.Video_Video_Optimize_Tune_SelectedItem = "none";
                VideoView.vm.Video_Video_Optimize_Profile_SelectedItem = "high";
                VideoView.vm.Video_Optimize_Level_SelectedItem = "4.2";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "SRT";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "160";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Raspberry Pi
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "Raspberry Pi")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x264";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";
                VideoView.vm.Video_Quality_SelectedItem = "High";
                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "Custom";
                VideoView.vm.Video_Video_Optimize_Tune_SelectedItem = "none";
                VideoView.vm.Video_Video_Optimize_Profile_SelectedItem = "main";
                VideoView.vm.Video_Optimize_Level_SelectedItem = "4.2";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "SRT";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "160";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }


            // ---------------------------------------------------------------------------
            // Mobile
            // ---------------------------------------------------------------------------
            // -------------------------
            // Android
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "Android")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x264";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";
                VideoView.vm.Video_Quality_SelectedItem = "High";
                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_Optimize_SelectedItem = "Android";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "MOV Text";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Stereo";
                AudioView.vm.Audio_Quality_SelectedItem = "400";
                AudioView.vm.Audio_VBR_IsChecked = true;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "44.1k";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // iOS
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "iOS")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x264";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";
                VideoView.vm.Video_Quality_SelectedItem = "High";
                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "Apple";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "MOV Text";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Stereo";
                AudioView.vm.Audio_Quality_SelectedItem = "400";
                AudioView.vm.Audio_VBR_IsChecked = true;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "44.1k";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }


            // ---------------------------------------------------------------------------
            // Console
            // ---------------------------------------------------------------------------
            // -------------------------
            // PS3
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "PS3")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x264";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";
                VideoView.vm.Video_Quality_SelectedItem = "Ultra";
                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_FPS_SelectedItem = "23.976";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "PS3";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "MOV Text";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "400";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "48k";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // PS4
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "PS4")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x264";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";
                VideoView.vm.Video_Quality_SelectedItem = "Ultra";
                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "PS4";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "MOV Text";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "Auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Xbox 360
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "Xbox 360")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x264";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";
                VideoView.vm.Video_Quality_SelectedItem = "High";
                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_FPS_SelectedItem = "23.976";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "Xbox 360";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "MOV Text";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Stereo";
                AudioView.vm.Audio_Quality_SelectedItem = "320";
                AudioView.vm.Audio_SampleRate_SelectedItem = "48k";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Xbox One
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "Xbox One")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x264";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Medium";
                VideoView.vm.Video_Quality_SelectedItem = "Ultra";
                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "Xbox One";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "MOV Text";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "Auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }


            // ---------------------------------------------------------------------------
            // Disc
            // ---------------------------------------------------------------------------
            // -------------------------
            // UHD
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "UHD")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x265";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Slow";
                VideoView.vm.Video_Quality_SelectedItem = "Custom";
                VideoView.vm.Video_Pass_SelectedItem = "2 Pass";
                VideoView.vm.Video_BitRate_Text = "50M";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "75M";
                VideoView.vm.Video_BufSize_Text = "75M";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p10le";
                VideoView.vm.Video_FPS_SelectedItem = "60";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "UHD";
                VideoView.vm.Video_Scale_SelectedItem = "4K UHD";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "MOV Text";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "DTS";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "1509";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "48k";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Blu-ray
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "Blu-ray")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "x264";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "Slow";
                VideoView.vm.Video_Quality_SelectedItem = "Ultra";
                VideoView.vm.Video_Pass_SelectedItem = "CRF";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p10le";
                VideoView.vm.Video_FPS_SelectedItem = "23.976";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "Blu-ray";
                VideoView.vm.Video_Scale_SelectedItem = "1080p";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "MOV Text";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AC3";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "640";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "48k";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // DVD
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "DVD")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mpg";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Audio Only";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "MPEG-2";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "none";
                VideoView.vm.Video_Quality_SelectedItem = "Custom";
                VideoView.vm.Video_Pass_SelectedItem = "2 Pass";
                VideoView.vm.Video_BitRate_Text = "3M";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "9.8M";
                VideoView.vm.Video_BufSize_Text = "9.8M";
                VideoView.vm.Video_PixelFormat_SelectedItem = "yuv420p";
                VideoView.vm.Video_FPS_SelectedItem = "ntsc";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "None";
                VideoView.vm.Video_Scale_SelectedItem = "Custom";
                VideoView.vm.Video_Width_Text = "720";
                VideoView.vm.Video_Height_Text = "480";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_AspectRatio_SelectedItem = "16:9";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "SRT";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "all";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "MP2";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "384";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "44.1k";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }


            // ---------------------------------------------------------------------------
            // Music
            // ---------------------------------------------------------------------------
            // -------------------------
            // Lossless
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "Lossless")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "flac";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "None";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "none";
                VideoView.vm.Video_Quality_SelectedItem = "None";
                VideoView.vm.Video_Pass_SelectedItem = "auto";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "none";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "None";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "FLAC";
                AudioView.vm.Audio_Stream_SelectedItem = "1";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "Lossless";
                AudioView.vm.Audio_VBR_IsChecked = true;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // MP3
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "MP3 HQ" ||
                     MainView.vm.Preset_SelectedItem == "MP3 Low")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp3";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Audio Only";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "None";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "none";
                VideoView.vm.Video_Quality_SelectedItem = "None";
                VideoView.vm.Video_Pass_SelectedItem = "auto";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "none";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "None";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "LAME";
                AudioView.vm.Audio_Stream_SelectedItem = "1";
                AudioView.vm.Audio_Channel_SelectedItem = "Joint Stereo";

                // HQ
                if (MainView.vm.Preset_SelectedItem == "MP3 HQ")
                {
                    AudioView.vm.Audio_Quality_SelectedItem = "320";
                }
                // Low
                else if (MainView.vm.Preset_SelectedItem == "MP3 Low")
                {
                    AudioView.vm.Audio_Quality_SelectedItem = "160";
                }

                AudioView.vm.Audio_VBR_IsChecked = true;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // iTunes
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "iTunes")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "m4a";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Audio Only";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "None";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "none";
                VideoView.vm.Video_Quality_SelectedItem = "None";
                VideoView.vm.Video_Pass_SelectedItem = "auto";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "none";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "None";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";
                AudioView.vm.Audio_Stream_SelectedItem = "1";
                AudioView.vm.Audio_Channel_SelectedItem = "Stereo";
                AudioView.vm.Audio_Quality_SelectedItem = "320";
                AudioView.vm.Audio_VBR_IsChecked = true;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Voice
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "Voice")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "ogg";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Audio Only";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "None";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "none";
                VideoView.vm.Video_Quality_SelectedItem = "None";
                VideoView.vm.Video_Pass_SelectedItem = "auto";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "none";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "None";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "Opus";
                AudioView.vm.Audio_Stream_SelectedItem = "1";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "96";
                AudioView.vm.Audio_VBR_IsChecked = true;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "10";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // ---------------------------------------------------------------------------
            // YouTube
            // ---------------------------------------------------------------------------
            // -------------------------
            // Video
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "Video Download")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "mp4";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "Copy";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "none";
                VideoView.vm.Video_Quality_SelectedItem = "Auto";
                VideoView.vm.Video_Pass_SelectedItem = "auto";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "auto";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "None";
                VideoView.vm.Video_Video_Optimize_Tune_SelectedItem = "none";
                VideoView.vm.Video_Video_Optimize_Profile_SelectedItem = "none";
                VideoView.vm.Video_Optimize_Level_SelectedItem = "none";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "Copy";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "Copy";
                AudioView.vm.Audio_Stream_SelectedItem = "all";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "Auto";
                AudioView.vm.Audio_VBR_IsChecked = false;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Music
            // -------------------------
            else if (MainView.vm.Preset_SelectedItem == "Music Download")
            {
                //MainView.vm.Preset.IsEditable = false;

                // Format
                FormatView.vm.Format_Container_SelectedItem = "m4a";
                FormatView.vm.Format_Cut_SelectedItem = "No";
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                FormatView.vm.Format_YouTube_SelectedItem = "Audio Only";
                FormatView.vm.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VideoView.vm.Video_Codec_SelectedItem = "None";
                VideoView.vm.Video_EncodeSpeed_SelectedItem = "none";
                VideoView.vm.Video_Quality_SelectedItem = "None";
                VideoView.vm.Video_Pass_SelectedItem = "auto";
                VideoView.vm.Video_BitRate_Text = "";
                VideoView.vm.Video_MinRate_Text = "";
                VideoView.vm.Video_MaxRate_Text = "";
                VideoView.vm.Video_BufSize_Text = "";
                VideoView.vm.Video_PixelFormat_SelectedItem = "none";
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_Speed_SelectedItem = "auto";
                VideoView.vm.Video_Optimize_SelectedItem = "None";
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_ScreenFormat_SelectedItem = "Widescreen";
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_AspectRatio_SelectedItem = "auto";
                VideoView.vm.Video_Crop_X_Text = "";
                VideoView.vm.Video_Crop_Y_Text = "";
                VideoView.vm.Video_Crop_Width_Text = "";
                VideoView.vm.Video_Crop_Height_Text = "";
                VideoView.vm.Video_CropClear_Text = "Clear";

                // Subtitle
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
                SubtitleView.vm.Subtitle_Stream_SelectedItem = "none";

                // Audio
                AudioView.vm.Audio_Codec_SelectedItem = "Copy";
                AudioView.vm.Audio_Stream_SelectedItem = "1";
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Quality_SelectedItem = "Auto";
                AudioView.vm.Audio_VBR_IsChecked = true;
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_Volume_Text = "100";
                AudioView.vm.Audio_HardLimiter_Value = 1;

                // Filters
                FilterVideoView.vm.LoadFilterVideoDefaults();
                FilterAudioView.vm.LoadFilterAudioDefaults();
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

                    if (MainView.vm.Preset_SelectedItem == filename)
                    {
                        preset = path;
                        break;
                    }
                }

                // Import Preset INI File
                Profiles.ImportPreset(/*main_vm,*/ preset);
            }


        }


    }
}
