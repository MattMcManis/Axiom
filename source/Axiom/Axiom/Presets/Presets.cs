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
            if (VM.MainView.Preset_SelectedItem == "Preset")
            {
                // -------------------------
                // Default Video
                // -------------------------
                if (VM.FormatView.Format_Container_SelectedItem == "webm" ||
                    VM.FormatView.Format_Container_SelectedItem == "mp4" ||
                    VM.FormatView.Format_Container_SelectedItem == "mkv" ||
                    VM.FormatView.Format_Container_SelectedItem == "m2v" ||
                    VM.FormatView.Format_Container_SelectedItem == "mpg" ||
                    VM.FormatView.Format_Container_SelectedItem == "avi" ||
                    VM.FormatView.Format_Container_SelectedItem == "ogv" ||
                    VM.FormatView.Format_Container_SelectedItem == "jpg" ||
                    VM.FormatView.Format_Container_SelectedItem == "png" ||
                    VM.FormatView.Format_Container_SelectedItem == "webp")
                {
                    // Format
                    VM.FormatView.Format_Container_SelectedItem = "webm";
                    VM.FormatView.Format_Cut_SelectedItem = "No";
                    VM.FormatView.Format_CutStart_Hours_Text = "00";
                    VM.FormatView.Format_CutStart_Minutes_Text = "00";
                    VM.FormatView.Format_CutStart_Seconds_Text = "00";
                    VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                    VM.FormatView.Format_CutEnd_Hours_Text = "00";
                    VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                    VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                    VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                    VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                    VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                    // Video
                    //VM.VideoView.Video_Codec_SelectedItem = "VP8";
                    VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";
                    VM.VideoView.Video_Quality_SelectedItem = "Auto";
                    VM.VideoView.Video_Pass_SelectedItem = "CRF";
                    VM.VideoView.Video_BitRate_Text = "";
                    VM.VideoView.Video_MinRate_Text = "";
                    VM.VideoView.Video_MaxRate_Text = "";
                    VM.VideoView.Video_BufSize_Text = "";
                    VM.VideoView.Video_FPS_Text = "";
                    VM.VideoView.Video_FPS_IsEditable = false;
                    VM.VideoView.Video_FPS_SelectedItem = "auto";
                    VM.VideoView.Video_Speed_Text = "";
                    VM.VideoView.Video_Speed_IsEditable = false;
                    VM.VideoView.Video_Speed_SelectedItem = "auto";
                    VM.VideoView.Video_Scale_SelectedItem = "Source";
                    VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                    VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                    VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                    VM.VideoView.Video_Crop_X_Text = "";
                    VM.VideoView.Video_Crop_Y_Text = "";
                    VM.VideoView.Video_Crop_Width_Text = "";
                    VM.VideoView.Video_Crop_Height_Text = "";
                    VM.VideoView.Video_CropClear_Text = "Clear";

                    // Subtitle
                    //VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                    VM.SubtitleView.Subtitle_Stream_SelectedItem = "none";

                    // Audio
                    //VM.AudioView.Audio_Codec_SelectedItem = "Vorbis";
                    VM.AudioView.Audio_Stream_SelectedItem = "all";
                    VM.AudioView.Audio_Channel_SelectedItem = "Source";
                    VM.AudioView.Audio_Quality_SelectedItem = "Auto";
                    VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                    VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                    VM.AudioView.Audio_Volume_Text = "100";
                    VM.AudioView.Audio_HardLimiter_Value = 0.0;

                    // special rules for webm
                    if (VM.FormatView.Format_Container_SelectedItem == "webm")
                    {
                        VM.SubtitleView.Subtitle_Stream_SelectedItem = "none";
                        VM.AudioView.Audio_Stream_SelectedItem = "1";
                        VM.VideoView.Video_Optimize_SelectedItem = "Web";
                    }
                    else
                    {
                        VM.VideoView.Video_Optimize_SelectedItem = "None";
                        VM.VideoView.Video_Video_Optimize_Tune_SelectedItem = "none";
                        VM.VideoView.Video_Video_Optimize_Profile_SelectedItem = "main";
                        VM.VideoView.Video_Optimize_Level_SelectedItem = "5.2";
                    }

                    // Filters
                    VM.FilterVideoView.LoadFilterVideoDefaults();
                    VM.FilterAudioView.LoadFilterAudioDefaults();

                }

                // -------------------------
                // Default Audio
                // -------------------------
                else if (VM.FormatView.Format_Container_SelectedItem == "m4a" ||
                         VM.FormatView.Format_Container_SelectedItem == "mp3" ||
                         VM.FormatView.Format_Container_SelectedItem == "ogg" ||
                         VM.FormatView.Format_Container_SelectedItem == "flac" ||
                         VM.FormatView.Format_Container_SelectedItem == "wav")
                {
                    //VM.MainView.Preset.IsEditable = false;

                    // Format
                    //VM.FormatView.Format_Container_SelectedItem = VM.FormatView.Format_Container_Items.FirstOrDefault();
                    //VM.FormatView.Format_Container_SelectedIndex = 1;
                    VM.FormatView.Format_Container_SelectedItem = "mp3";
                    VM.FormatView.Format_Cut_SelectedItem = "No";
                    VM.FormatView.Format_CutStart_Hours_Text = "00";
                    VM.FormatView.Format_CutStart_Minutes_Text = "00";
                    VM.FormatView.Format_CutStart_Seconds_Text = "00";
                    VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                    VM.FormatView.Format_CutEnd_Hours_Text = "00";
                    VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                    VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                    VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                    VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                    VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                    // Video
                    VM.VideoView.Video_Codec_SelectedItem = "None";
                    VM.VideoView.Video_EncodeSpeed_SelectedItem = "none";
                    VM.VideoView.Video_Quality_SelectedItem = "None";
                    VM.VideoView.Video_Pass_SelectedItem = "auto";
                    VM.VideoView.Video_BitRate_Text = "";
                    VM.VideoView.Video_MinRate_Text = "";
                    VM.VideoView.Video_MaxRate_Text = "";
                    VM.VideoView.Video_BufSize_Text = "";
                    VM.VideoView.Video_PixelFormat_SelectedItem = "none";
                    VM.VideoView.Video_FPS_Text = "";
                    VM.VideoView.Video_FPS_IsEditable = false;
                    VM.VideoView.Video_FPS_SelectedItem = "auto";
                    VM.VideoView.Video_Speed_Text = "";
                    VM.VideoView.Video_Speed_IsEditable = false;
                    VM.VideoView.Video_Speed_SelectedItem = "auto";
                    VM.VideoView.Video_Optimize_SelectedItem = "None";
                    VM.VideoView.Video_Scale_SelectedItem = "Source";
                    VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                    VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                    VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                    VM.VideoView.Video_Crop_X_Text = "";
                    VM.VideoView.Video_Crop_Y_Text = "";
                    VM.VideoView.Video_Crop_Width_Text = "";
                    VM.VideoView.Video_Crop_Height_Text = "";
                    VM.VideoView.Video_CropClear_Text = "Clear";

                    // Subtitle
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                    VM.SubtitleView.Subtitle_Stream_SelectedItem = "none";

                    // Audio
                    VM.AudioView.Audio_Codec_SelectedItem = "LAME";
                    VM.AudioView.Audio_Stream_SelectedItem = "1";
                    VM.AudioView.Audio_Channel_SelectedItem = "Source";
                    VM.AudioView.Audio_Quality_SelectedItem = "Auto";
                    VM.AudioView.Audio_VBR_IsChecked = false;
                    VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                    VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                    // special rules for PCM codec
                    if (VM.AudioView.Audio_Codec_SelectedItem == "PCM")
                    {
                        VM.AudioView.Audio_BitDepth_SelectedItem = "24";
                    }
                    else
                    {
                        VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                    }

                    VM.AudioView.Audio_Volume_Text = "100";
                    VM.AudioView.Audio_HardLimiter_Value = 0.0;

                    // Filters
                    VM.FilterVideoView.LoadFilterVideoDefaults();
                    VM.FilterAudioView.LoadFilterAudioDefaults();
                }
            }


            // ---------------------------------------------------------------------------
            // Web
            // ---------------------------------------------------------------------------
            // -------------------------
            // HTML5
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "HTML5")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "webm";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "VP8";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";
                VM.VideoView.Video_Quality_SelectedItem = "Medium";
                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                //VM.VideoView.Video_BitRate_Text = ""; // use quality preset bitrate
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "Web";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "none";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "Vorbis";
                VM.AudioView.Audio_Stream_SelectedItem = "1";
                VM.AudioView.Audio_Channel_SelectedItem = "Stereo";
                VM.AudioView.Audio_Quality_SelectedItem = "192";
                VM.AudioView.Audio_VBR_IsChecked = true;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "44.1k";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // YouTube
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "YouTube")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x264";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";
                VM.VideoView.Video_Quality_SelectedItem = "Ultra";
                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "Web";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "MOV Text";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "Auto";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }


            // ---------------------------------------------------------------------------
            // PC
            // ---------------------------------------------------------------------------
            // -------------------------
            // Archive Video
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "Archive")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mkv";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "FFV1";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "none";
                VM.VideoView.Video_Quality_SelectedItem = "Lossless";
                VM.VideoView.Video_Pass_SelectedItem = "2 Pass";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv444p10le";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "None";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "Copy";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "FLAC";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "Auto";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // HEVC Ultra
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "HEVC Ultra" ||
                     VM.MainView.Preset_SelectedItem == "HEVC High")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mkv";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x265";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Slow";

                if (VM.MainView.Preset_SelectedItem == "HEVC Ultra")
                {
                    VM.VideoView.Video_Quality_SelectedItem = "Ultra";
                }
                else if (VM.MainView.Preset_SelectedItem == "HEVC High")
                {
                    VM.VideoView.Video_Quality_SelectedItem = "High";
                }

                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                //VM.VideoView.Video_CRF_Text = ""; // Use Quality Preset Value
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p10le";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "23.976";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "Custom";
                VM.VideoView.Video_Video_Optimize_Tune_SelectedItem = "none";
                VM.VideoView.Video_Video_Optimize_Profile_SelectedItem = "main10";
                VM.VideoView.Video_Optimize_Level_SelectedItem = "5.2";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";


                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "Copy";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "Auto";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // HD Video
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "HD Ultra" ||
                     VM.MainView.Preset_SelectedItem == "HD High" ||
                     VM.MainView.Preset_SelectedItem == "HD Medium" ||
                     VM.MainView.Preset_SelectedItem == "HD Low"
                     )
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x264";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";

                if (VM.MainView.Preset_SelectedItem == "HD Ultra")
                {
                    VM.VideoView.Video_Quality_SelectedItem = "Ultra";
                }
                else if (VM.MainView.Preset_SelectedItem == "HD High")
                {
                    VM.VideoView.Video_Quality_SelectedItem = "High";
                }
                else if (VM.MainView.Preset_SelectedItem == "HD Medium")
                {
                    VM.VideoView.Video_Quality_SelectedItem = "Medium";
                }
                else if (VM.MainView.Preset_SelectedItem == "HD Low")
                {
                    VM.VideoView.Video_Quality_SelectedItem = "Low";
                }

                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "PC HD";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "MOV Text";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "Auto";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // SD Video
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "SD High" ||
                     VM.MainView.Preset_SelectedItem == "SD Medium" ||
                     VM.MainView.Preset_SelectedItem == "SD Low")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x264";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";

                if (VM.MainView.Preset_SelectedItem == "SD High")
                {
                    VM.VideoView.Video_Quality_SelectedItem = "High";
                }
                else if (VM.MainView.Preset_SelectedItem == "SD Medium")
                {
                    VM.VideoView.Video_Quality_SelectedItem = "Medium";
                }
                else if (VM.MainView.Preset_SelectedItem == "SD Low")
                {
                    VM.VideoView.Video_Quality_SelectedItem = "Low";
                }

                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "PC SD";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "MOV Text";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AC3";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Stereo";
                VM.AudioView.Audio_Quality_SelectedItem = "256";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "44.1k";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }


            // ---------------------------------------------------------------------------
            // Device
            // ---------------------------------------------------------------------------
            // -------------------------
            // Roku
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "Roku")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mkv";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x264";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";
                VM.VideoView.Video_Quality_SelectedItem = "Ultra";
                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "Custom";
                VM.VideoView.Video_Video_Optimize_Tune_SelectedItem = "none";
                VM.VideoView.Video_Video_Optimize_Profile_SelectedItem = "high";
                VM.VideoView.Video_Optimize_Level_SelectedItem = "4.0";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "SRT";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "160";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Amazon Fire
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "Amazon Fire")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x264";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";
                VM.VideoView.Video_Quality_SelectedItem = "Ultra";
                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "Custom";
                VM.VideoView.Video_Video_Optimize_Tune_SelectedItem = "none";
                VM.VideoView.Video_Video_Optimize_Profile_SelectedItem = "high";
                VM.VideoView.Video_Optimize_Level_SelectedItem = "4.2";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "SRT";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "160";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Chromecast
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "Chromecast")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x264";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";
                VM.VideoView.Video_Quality_SelectedItem = "Ultra";
                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "Custom";
                VM.VideoView.Video_Video_Optimize_Tune_SelectedItem = "none";
                VM.VideoView.Video_Video_Optimize_Profile_SelectedItem = "high";
                VM.VideoView.Video_Optimize_Level_SelectedItem = "4.2";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "SRT";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "160";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Apple TV
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "Apple TV")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x264";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";
                VM.VideoView.Video_Quality_SelectedItem = "Ultra";
                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "Custom";
                VM.VideoView.Video_Video_Optimize_Tune_SelectedItem = "none";
                VM.VideoView.Video_Video_Optimize_Profile_SelectedItem = "high";
                VM.VideoView.Video_Optimize_Level_SelectedItem = "4.2";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "SRT";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "160";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Raspberry Pi
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "Raspberry Pi")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x264";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";
                VM.VideoView.Video_Quality_SelectedItem = "High";
                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "Custom";
                VM.VideoView.Video_Video_Optimize_Tune_SelectedItem = "none";
                VM.VideoView.Video_Video_Optimize_Profile_SelectedItem = "main";
                VM.VideoView.Video_Optimize_Level_SelectedItem = "4.2";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "SRT";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "160";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }


            // ---------------------------------------------------------------------------
            // Mobile
            // ---------------------------------------------------------------------------
            // -------------------------
            // Android
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "Android")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x264";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";
                VM.VideoView.Video_Quality_SelectedItem = "High";
                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_Optimize_SelectedItem = "Android";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "MOV Text";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Stereo";
                VM.AudioView.Audio_Quality_SelectedItem = "400";
                VM.AudioView.Audio_VBR_IsChecked = true;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "44.1k";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // iOS
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "iOS")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x264";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";
                VM.VideoView.Video_Quality_SelectedItem = "High";
                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "Apple";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "MOV Text";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Stereo";
                VM.AudioView.Audio_Quality_SelectedItem = "400";
                VM.AudioView.Audio_VBR_IsChecked = true;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "44.1k";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }


            // ---------------------------------------------------------------------------
            // Console
            // ---------------------------------------------------------------------------
            // -------------------------
            // PS3
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "PS3")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x264";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";
                VM.VideoView.Video_Quality_SelectedItem = "Ultra";
                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "23.976";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "PS3";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "MOV Text";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "400";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "48k";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // PS4
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "PS4")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x264";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";
                VM.VideoView.Video_Quality_SelectedItem = "Ultra";
                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "PS4";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "MOV Text";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "Auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Xbox 360
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "Xbox 360")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x264";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";
                VM.VideoView.Video_Quality_SelectedItem = "High";
                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "23.976";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "Xbox 360";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "MOV Text";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Stereo";
                VM.AudioView.Audio_Quality_SelectedItem = "320";
                VM.AudioView.Audio_SampleRate_SelectedItem = "48k";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Xbox One
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "Xbox One")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x264";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Medium";
                VM.VideoView.Video_Quality_SelectedItem = "Ultra";
                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "Xbox One";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "MOV Text";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "Auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }


            // ---------------------------------------------------------------------------
            // Disc
            // ---------------------------------------------------------------------------
            // -------------------------
            // UHD
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "UHD")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x265";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Slow";
                VM.VideoView.Video_Quality_SelectedItem = "Custom";
                VM.VideoView.Video_Pass_SelectedItem = "2 Pass";
                VM.VideoView.Video_BitRate_Text = "50M";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "75M";
                VM.VideoView.Video_BufSize_Text = "75M";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p10le";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "60";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "UHD";
                VM.VideoView.Video_Scale_SelectedItem = "4K UHD";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "MOV Text";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "DTS";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "1509";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "48k";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Blu-ray
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "Blu-ray")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "x264";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "Slow";
                VM.VideoView.Video_Quality_SelectedItem = "Ultra";
                VM.VideoView.Video_Pass_SelectedItem = "CRF";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p10le";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "23.976";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "Blu-ray";
                VM.VideoView.Video_Scale_SelectedItem = "1080p";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "MOV Text";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AC3";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "640";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "48k";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // DVD
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "DVD")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mpg";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Audio Only";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "MPEG-2";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "none";
                VM.VideoView.Video_Quality_SelectedItem = "Custom";
                VM.VideoView.Video_Pass_SelectedItem = "2 Pass";
                VM.VideoView.Video_BitRate_Text = "3M";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "9.8M";
                VM.VideoView.Video_BufSize_Text = "9.8M";
                VM.VideoView.Video_PixelFormat_SelectedItem = "yuv420p";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "ntsc";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "None";
                VM.VideoView.Video_Scale_SelectedItem = "Custom";
                VM.VideoView.Video_Width_Text = "720";
                VM.VideoView.Video_Height_Text = "480";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_AspectRatio_SelectedItem = "16:9";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "SRT";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "all";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "MP2";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "384";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "44.1k";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }


            // ---------------------------------------------------------------------------
            // Music
            // ---------------------------------------------------------------------------
            // -------------------------
            // Lossless
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "Lossless")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "flac";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "None";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "none";
                VM.VideoView.Video_Quality_SelectedItem = "None";
                VM.VideoView.Video_Pass_SelectedItem = "auto";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "none";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "None";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "none";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "FLAC";
                VM.AudioView.Audio_Stream_SelectedItem = "1";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "Lossless";
                VM.AudioView.Audio_VBR_IsChecked = true;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // MP3
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "MP3 HQ" ||
                     VM.MainView.Preset_SelectedItem == "MP3 Low")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp3";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Audio Only";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "None";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "none";
                VM.VideoView.Video_Quality_SelectedItem = "None";
                VM.VideoView.Video_Pass_SelectedItem = "auto";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "none";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "None";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "none";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "LAME";
                VM.AudioView.Audio_Stream_SelectedItem = "1";
                VM.AudioView.Audio_Channel_SelectedItem = "Joint Stereo";

                // HQ
                if (VM.MainView.Preset_SelectedItem == "MP3 HQ")
                {
                    VM.AudioView.Audio_Quality_SelectedItem = "320";
                }
                // Low
                else if (VM.MainView.Preset_SelectedItem == "MP3 Low")
                {
                    VM.AudioView.Audio_Quality_SelectedItem = "160";
                }

                VM.AudioView.Audio_VBR_IsChecked = true;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // iTunes
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "iTunes")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "m4a";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Audio Only";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "None";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "none";
                VM.VideoView.Video_Quality_SelectedItem = "None";
                VM.VideoView.Video_Pass_SelectedItem = "auto";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "none";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "None";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "none";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "AAC";
                VM.AudioView.Audio_Stream_SelectedItem = "1";
                VM.AudioView.Audio_Channel_SelectedItem = "Stereo";
                VM.AudioView.Audio_Quality_SelectedItem = "320";
                VM.AudioView.Audio_VBR_IsChecked = true;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Voice
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "Voice")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "ogg";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Audio Only";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "None";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "none";
                VM.VideoView.Video_Quality_SelectedItem = "None";
                VM.VideoView.Video_Pass_SelectedItem = "auto";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "none";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "None";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "none";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "Opus";
                VM.AudioView.Audio_Stream_SelectedItem = "1";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "96";
                VM.AudioView.Audio_VBR_IsChecked = true;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "10";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // ---------------------------------------------------------------------------
            // YouTube
            // ---------------------------------------------------------------------------
            // -------------------------
            // Video
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "Video Download")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "mp4";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "Copy";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "none";
                VM.VideoView.Video_Quality_SelectedItem = "Auto";
                VM.VideoView.Video_Pass_SelectedItem = "auto";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "auto";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "None";
                VM.VideoView.Video_Video_Optimize_Tune_SelectedItem = "none";
                VM.VideoView.Video_Video_Optimize_Profile_SelectedItem = "none";
                VM.VideoView.Video_Optimize_Level_SelectedItem = "none";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "Copy";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "none";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "Copy";
                VM.AudioView.Audio_Stream_SelectedItem = "all";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "Auto";
                VM.AudioView.Audio_VBR_IsChecked = false;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
            }

            // -------------------------
            // Music
            // -------------------------
            else if (VM.MainView.Preset_SelectedItem == "Music Download")
            {
                //VM.MainView.Preset.IsEditable = false;

                // Format
                VM.FormatView.Format_Container_SelectedItem = "m4a";
                VM.FormatView.Format_Cut_SelectedItem = "No";
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                VM.FormatView.Format_YouTube_SelectedItem = "Audio Only";
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";

                // Video
                VM.VideoView.Video_Codec_SelectedItem = "None";
                VM.VideoView.Video_EncodeSpeed_SelectedItem = "none";
                VM.VideoView.Video_Quality_SelectedItem = "None";
                VM.VideoView.Video_Pass_SelectedItem = "auto";
                VM.VideoView.Video_BitRate_Text = "";
                VM.VideoView.Video_MinRate_Text = "";
                VM.VideoView.Video_MaxRate_Text = "";
                VM.VideoView.Video_BufSize_Text = "";
                VM.VideoView.Video_PixelFormat_SelectedItem = "none";
                VM.VideoView.Video_FPS_Text = "";
                VM.VideoView.Video_FPS_IsEditable = false;
                VM.VideoView.Video_FPS_SelectedItem = "auto";
                VM.VideoView.Video_Speed_Text = "";
                VM.VideoView.Video_Speed_IsEditable = false;
                VM.VideoView.Video_Speed_SelectedItem = "auto";
                VM.VideoView.Video_Optimize_SelectedItem = "None";
                VM.VideoView.Video_Scale_SelectedItem = "Source";
                VM.VideoView.Video_ScreenFormat_SelectedItem = "Widescreen";
                VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                VM.VideoView.Video_AspectRatio_SelectedItem = "auto";
                VM.VideoView.Video_Crop_X_Text = "";
                VM.VideoView.Video_Crop_Y_Text = "";
                VM.VideoView.Video_Crop_Width_Text = "";
                VM.VideoView.Video_Crop_Height_Text = "";
                VM.VideoView.Video_CropClear_Text = "Clear";

                // Subtitle
                VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                VM.SubtitleView.Subtitle_Stream_SelectedItem = "none";

                // Audio
                VM.AudioView.Audio_Codec_SelectedItem = "Copy";
                VM.AudioView.Audio_Stream_SelectedItem = "1";
                VM.AudioView.Audio_Channel_SelectedItem = "Source";
                VM.AudioView.Audio_Quality_SelectedItem = "Auto";
                VM.AudioView.Audio_VBR_IsChecked = true;
                VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                VM.AudioView.Audio_Volume_Text = "100";
                VM.AudioView.Audio_HardLimiter_Value = 0.0;

                // Filters
                VM.FilterVideoView.LoadFilterVideoDefaults();
                VM.FilterAudioView.LoadFilterAudioDefaults();
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

                    if (VM.MainView.Preset_SelectedItem == filename)
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
