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

/* ----------------------------------
 METHODS

 * Set Controls
 * Cut Controls
---------------------------------- */

using System.Collections.Generic;
using System.Linq;
using System.Windows;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class FormatControls
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ComboBoxes Item Sources
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Set Controls
        /// </summary>
        public static void SetControls(string container_SelectedItem)
        {
            // --------------------------------------------------
            // Containers
            // --------------------------------------------------
            // Select Codecs and Default Selected Items per container

            switch (container_SelectedItem)
            {
                // --------------------------------------------------
                // Video
                // --------------------------------------------------
                // -------------------------
                // webm
                // -------------------------
                case "webm":

                    // Media Type
                    VM.FormatView.Format_MediaType_Items = Containers.WebM.media;
                    VM.FormatView.Format_MediaType_SelectedItem = "Video";
                    VM.FormatView.Format_MediaType_IsEnabled = false;

                    // Video Codec
                    VM.VideoView.Video_Codec_Items = Containers.WebM.video;
                    VM.VideoView.Video_Codec_SelectedItem = "VP8";

                    // Optimize
                    VM.VideoView.Video_Optimize_SelectedItem = "Web";

                    // Audio Codec
                    VM.AudioView.Audio_Codec_Items = Containers.WebM.audio;
                    VM.AudioView.Audio_Codec_SelectedItem = "Vorbis";

                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_Items = Containers.WebM.subtitle;
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                    break;

                // -------------------------
                // mp4
                // -------------------------
                case "mp4":

                    // Media Type
                    VM.FormatView.Format_MediaType_Items = Containers.MP4.media;
                    VM.FormatView.Format_MediaType_SelectedItem = "Video";
                    VM.FormatView.Format_MediaType_IsEnabled = false;

                    // Video Codec
                    VM.VideoView.Video_Codec_Items = Containers.MP4.video;
                    VM.VideoView.Video_Codec_SelectedItem = "x264";

                    // Audio Codec
                    VM.AudioView.Audio_Codec_Items = Containers.MP4.audio;
                    VM.AudioView.Audio_Codec_SelectedItem = "AAC";

                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_Items = Containers.MP4.subtitle;
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "MOV Text";
                    break;

                // -------------------------
                // mkv
                // -------------------------
                case "mkv":

                    // Media Type
                    VM.FormatView.Format_MediaType_Items = Containers.MKV.media;
                    VM.FormatView.Format_MediaType_SelectedItem = "Video";
                    VM.FormatView.Format_MediaType_IsEnabled = false;

                    // Video Codec
                    VM.VideoView.Video_Codec_Items = Containers.MKV.video;
                    VM.VideoView.Video_Codec_SelectedItem = "x264";

                    // Audio Codec
                    VM.AudioView.Audio_Codec_Items = Containers.MKV.audio;
                    VM.AudioView.Audio_Codec_SelectedItem = "AC3";

                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_Items = Containers.MKV.subtitle;
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "SSA";
                    break;

                // -------------------------
                // m2v
                // -------------------------
                case "m2v":

                    // Media Type
                    VM.FormatView.Format_MediaType_Items = Containers.M2V.media;
                    VM.FormatView.Format_MediaType_SelectedItem = "Video";
                    VM.FormatView.Format_MediaType_IsEnabled = false;

                    // Video Codec
                    VM.VideoView.Video_Codec_Items = Containers.M2V.video;
                    VM.VideoView.Video_Codec_SelectedItem = "MPEG-2";

                    // Audio Codec
                    VM.AudioView.Audio_Codec_Items = Containers.M2V.audio;
                    VM.AudioView.Audio_Codec_SelectedItem = "None";

                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_Items = Containers.M2V.subtitle;
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                    break;

                // -------------------------
                // mpg
                // -------------------------
                case "mpg":

                    // Media Type
                    VM.FormatView.Format_MediaType_Items = Containers.MPG.media;
                    VM.FormatView.Format_MediaType_SelectedItem = "Video";
                    VM.FormatView.Format_MediaType_IsEnabled = false;

                    // Video Codec
                    VM.VideoView.Video_Codec_Items = Containers.MPG.video;
                    VM.VideoView.Video_Codec_SelectedItem = "MPEG-2";

                    // Audio Codec
                    VM.AudioView.Audio_Codec_Items = Containers.MPG.audio;
                    VM.AudioView.Audio_Codec_SelectedItem = "MP2";

                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_Items = Containers.MPG.subtitle;
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "SRT";
                    break;

                // -------------------------
                // avi
                // -------------------------
                case "avi":

                    // Media Type
                    VM.FormatView.Format_MediaType_Items = Containers.AVI.media;
                    VM.FormatView.Format_MediaType_SelectedItem = "Video";
                    VM.FormatView.Format_MediaType_IsEnabled = false;

                    // Video Codec
                    VM.VideoView.Video_Codec_Items = Containers.AVI.video;
                    VM.VideoView.Video_Codec_SelectedItem = "MPEG-4";

                    // Audio Codec
                    VM.AudioView.Audio_Codec_Items = Containers.AVI.audio;
                    VM.AudioView.Audio_Codec_SelectedItem = "LAME";

                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_Items = Containers.AVI.subtitle;
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "SRT";
                    break;

                // -------------------------
                // ogv
                // -------------------------
                case "ogv":

                    // Media Type
                    VM.FormatView.Format_MediaType_Items = Containers.OGV.media;
                    VM.FormatView.Format_MediaType_SelectedItem = "Video";
                    VM.FormatView.Format_MediaType_IsEnabled = false;

                    // Video Codec
                    VM.VideoView.Video_Codec_Items = Containers.OGV.video;
                    VM.VideoView.Video_Codec_SelectedItem = "Theora";

                    // Audio Codec
                    VM.AudioView.Audio_Codec_Items = Containers.OGV.audio;
                    VM.AudioView.Audio_Codec_SelectedItem = "Opus";

                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_Items = Containers.OGV.subtitle;
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                    break;


                // --------------------------------------------------
                // Audio
                // --------------------------------------------------
                // -------------------------
                // mp3
                // -------------------------
                case "mp3":

                    // Media Type
                    VM.FormatView.Format_MediaType_Items = Containers.LAME.media;
                    VM.FormatView.Format_MediaType_SelectedItem = "Audio";
                    VM.FormatView.Format_MediaType_IsEnabled = false;

                    // Video Codec
                    VM.VideoView.Video_Codec_Items = Containers.LAME.video;
                    VM.VideoView.Video_Codec_SelectedItem = "None";

                    // Audio Codec
                    VM.AudioView.Audio_Codec_Items = Containers.LAME.audio;
                    VM.AudioView.Audio_Codec_SelectedItem = "LAME";

                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_Items = Containers.LAME.subtitle;
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                    break;

                // -------------------------
                // m4a
                // -------------------------
                case "m4a":

                    // Media Type
                    VM.FormatView.Format_MediaType_Items = Containers.M4A.media;
                    VM.FormatView.Format_MediaType_SelectedItem = "Audio";
                    VM.FormatView.Format_MediaType_IsEnabled = false;

                    // Video Codec
                    VM.VideoView.Video_Codec_Items = Containers.M4A.video;
                    VM.VideoView.Video_Codec_SelectedItem = "None";

                    // Audio Codec
                    VM.AudioView.Audio_Codec_Items = Containers.M4A.audio;
                    VM.AudioView.Audio_Codec_SelectedItem = "AAC";

                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_Items = Containers.M4A.subtitle;
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                    break;

                // -------------------------
                // ogg
                // -------------------------
                case "ogg":

                    // Media Type
                    VM.FormatView.Format_MediaType_Items = Containers.OGG.media;
                    VM.FormatView.Format_MediaType_SelectedItem = "Audio";
                    VM.FormatView.Format_MediaType_IsEnabled = false;

                    // Video Codec
                    VM.VideoView.Video_Codec_Items = Containers.OGG.video;
                    VM.VideoView.Video_Codec_SelectedItem = "None";

                    // Audio Codec
                    VM.AudioView.Audio_Codec_Items = Containers.OGG.audio;
                    VM.AudioView.Audio_Codec_SelectedItem = "Opus";

                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_Items = Containers.OGG.subtitle;
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                    break;

                // -------------------------
                // flac
                // -------------------------
                case "flac":

                    // Media Type
                    VM.FormatView.Format_MediaType_Items = Containers.FLAC.media;
                    VM.FormatView.Format_MediaType_SelectedItem = "Audio";
                    VM.FormatView.Format_MediaType_IsEnabled = false;

                    // Video Codec
                    VM.VideoView.Video_Codec_Items = Containers.FLAC.video;
                    VM.VideoView.Video_Codec_SelectedItem = "None";

                    // Audio Codec
                    VM.AudioView.Audio_Codec_Items = Containers.FLAC.audio;
                    VM.AudioView.Audio_Codec_SelectedItem = "FLAC";

                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_Items = Containers.FLAC.subtitle;
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                    break;

                // -------------------------
                // wav
                // -------------------------
                case "wav":

                    // Media Type
                    VM.FormatView.Format_MediaType_Items = Containers.WAV.media;
                    VM.FormatView.Format_MediaType_SelectedItem = "Audio";
                    VM.FormatView.Format_MediaType_IsEnabled = false;

                    // Video Codec
                    VM.VideoView.Video_Codec_Items = Containers.WAV.video;
                    VM.VideoView.Video_Codec_SelectedItem = "None";

                    // Audio Codec
                    VM.AudioView.Audio_Codec_Items = Containers.WAV.audio;
                    VM.AudioView.Audio_Codec_SelectedItem = "PCM";

                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_Items = Containers.WAV.subtitle;
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                    break;


                // --------------------------------------------------
                // Image
                // --------------------------------------------------
                // -------------------------
                // jpg
                // -------------------------
                case "jpg":

                    // Media Type
                    VM.FormatView.Format_MediaType_Items = Containers.JPG.media;
                    VM.FormatView.Format_MediaType_SelectedItem = "Image";
                    VM.FormatView.Format_MediaType_IsEnabled = true;

                    // Video Codec
                    VM.VideoView.Video_Codec_Items = Containers.JPG.video;
                    VM.VideoView.Video_Codec_SelectedItem = "JPEG";

                    // Audio Codec
                    VM.AudioView.Audio_Codec_Items = Containers.JPG.audio;
                    VM.AudioView.Audio_Codec_SelectedItem = "None";

                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_Items = Containers.JPG.subtitle;
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                    break;

                // -------------------------
                // png
                // -------------------------
                case "png":

                    // Media Type
                    VM.FormatView.Format_MediaType_Items = Containers.JPG.media;
                    VM.FormatView.Format_MediaType_SelectedItem = "Image";
                    VM.FormatView.Format_MediaType_IsEnabled = true;

                    // Video Codec
                    VM.VideoView.Video_Codec_Items = Containers.PNG.video;
                    VM.VideoView.Video_Codec_SelectedItem = "PNG";

                    // Audio Codec
                    VM.AudioView.Audio_Codec_Items = Containers.PNG.audio;
                    VM.AudioView.Audio_Codec_SelectedItem = "None";

                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_Items = Containers.PNG.subtitle;
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                    break;

                // -------------------------
                // webp
                // -------------------------
                case "webp":

                    // Media Type
                    VM.FormatView.Format_MediaType_Items = Containers.JPG.media;
                    VM.FormatView.Format_MediaType_SelectedItem = "Image";
                    VM.FormatView.Format_MediaType_IsEnabled = true;

                    // Video Codec
                    VM.VideoView.Video_Codec_Items = Containers.WebP.video;
                    VM.VideoView.Video_Codec_SelectedItem = "WebP";

                    // Audio Codec
                    VM.AudioView.Audio_Codec_Items = Containers.WebP.audio;
                    VM.AudioView.Audio_Codec_SelectedItem = "None";

                    // Subtitle Codec
                    VM.SubtitleView.Subtitle_Codec_Items = Containers.WebP.subtitle;
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
                    break;
            }
        }



        /// <summary>
        /// Get Output Extension
        /// </summary>
        public static void OutputFormatExt()
        {
            // Output Extension is Format ComboBox's Selected Item (eg mp4)
            MainWindow.outputExt = "." + VM.FormatView.Format_Container_SelectedItem;
        }



        /// <summary>
        /// MediaType Controls Controls
        /// </summary>
        public static void MediaTypeControls()
        {
            switch (VM.FormatView.Format_MediaType_SelectedItem)
            {
                // -------------------------
                // Video
                // -------------------------
                // Enable Frame Textbox for Image Screenshot
                case "Video":
                    // -------------------------
                    // Format
                    // -------------------------
                    // Hardware Acceleration
                    VM.VideoView.Video_HWAccel_IsEnabled = true;

                    // Cut
                    // Change if coming back from JPEG, PNG, WebP
                    if (VM.FormatView.Format_CutStart_IsEnabled == true &&
                        VM.FormatView.Format_CutEnd_IsEnabled == false)
                    {
                        VM.FormatView.Format_Cut_SelectedItem = "No";
                    }

                    // YouTube
                    VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";

                    // -------------------------
                    // Video
                    // -------------------------
                    // Codec
                    VM.VideoView.Video_Codec_IsEnabled = true;

                    // Size
                    VM.VideoView.Video_Scale_IsEnabled = true;

                    // Scaling
                    VM.VideoView.Video_ScalingAlgorithm_IsEnabled = true;
                    VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";

                    // Speed
                    VM.VideoView.Video_Speed_IsEnabled = true;

                    // Screen Format
                    VM.VideoView.Video_ScreenFormat_IsEnabled = true;

                    // Aspect Ratio
                    VM.VideoView.Video_AspectRatio_IsEnabled = true;

                    // Crop
                    VM.VideoView.Video_Crop_IsEnabled = true;

                    // -------------------------
                    // Audio
                    // -------------------------
                    // Codec
                    VM.AudioView.Audio_Codec_IsEnabled = true;

                    // Channel
                    VM.AudioView.Audio_Channel_IsEnabled = true;

                    // Volume
                    VM.AudioView.Audio_Volume_IsEnabled = true;

                    // Limiter
                    VM.AudioView.Audio_HardLimiter_IsEnabled = true;

                    // Audio Stream
                    VM.AudioView.Audio_Stream_IsEnabled = true;
                    //AudioStreamControls(:; // Selected Item
                    //VM.AudioView.Audio_Stream_SelectedItem = "all";


                    // -------------------------
                    // Subtitle
                    // -------------------------
                    // Codec
                    VM.SubtitleView.Subtitle_Codec_IsEnabled = true;
                    break;

                // -------------------------
                // Audio
                // -------------------------
                case "Audio":
                    // -------------------------
                    // Format
                    // -------------------------
                    // Hardware Acceleration
                    VM.VideoView.Video_HWAccel_IsEnabled = false;
                    VM.VideoView.Video_HWAccel_SelectedItem = "off";

                    // Cut
                    // Change if coming back from JPEG, PNG, WebP
                    if (VM.FormatView.Format_CutStart_IsEnabled == true &&
                        VM.FormatView.Format_CutEnd_IsEnabled == false)
                    {
                        VM.FormatView.Format_Cut_SelectedItem = "No";
                    }

                    // YouTube
                    VM.FormatView.Format_YouTube_SelectedItem = "Audio Only";

                    // Frame
                    VM.FormatView.Format_FrameEnd_IsEnabled = false;
                    VM.FormatView.Format_FrameEnd_Text = "";

                    // -------------------------
                    // Video
                    // -------------------------
                    // Codec
                    VM.VideoView.Video_Codec_IsEnabled = false;

                    // Size
                    VM.VideoView.Video_Scale_SelectedItem = "Source";
                    VM.VideoView.Video_Scale_IsEnabled = false;

                    // Scaling
                    VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                    VM.VideoView.Video_ScalingAlgorithm_IsEnabled = false;

                    // Speed
                    VM.VideoView.Video_Speed_IsEnabled = false;

                    // Screen Format
                    VM.VideoView.Video_ScreenFormat_IsEnabled = false;

                    // Aspect Ratio
                    VM.VideoView.Video_AspectRatio_IsEnabled = false;

                    // Crop
                    VM.VideoView.Video_Crop_IsEnabled = false;

                    // FPS
                    VM.VideoView.Video_FPS_SelectedItem = "auto";
                    VM.VideoView.Video_FPS_IsEnabled = false;

                    // Encode Speed
                    VM.VideoView.Video_EncodeSpeed_IsEnabled = false;


                    // -------------------------
                    // Audio
                    // -------------------------
                    // Codec
                    VM.AudioView.Audio_Codec_IsEnabled = true;

                    // Audio Stream
                    VM.AudioView.Audio_Stream_IsEnabled = true;
                    //AudioStreamControls(:; // Selected Item
                    //VM.AudioView.Audio_Stream_SelectedItem = "1";

                    // Channel
                    VM.AudioView.Audio_Channel_IsEnabled = true;

                    // Sample Rate
                    // Controled Through Codec Class

                    // Bit Depth
                    // Controled Through Codec Class

                    // Volume
                    VM.AudioView.Audio_Volume_IsEnabled = true;

                    // Limiter
                    VM.AudioView.Audio_HardLimiter_IsEnabled = true;


                    // -------------------------
                    // Subtitle
                    // -------------------------
                    // Codec
                    VM.SubtitleView.Subtitle_Codec_IsEnabled = false;
                    break;

                // -------------------------
                // Image
                // -------------------------
                case "Image":
                    // -------------------------
                    // Format
                    // -------------------------
                    // Hardware Acceleration
                    VM.VideoView.Video_HWAccel_IsEnabled = true;

                    // Cut
                    // Enable Cut Start Time for Frame Selection
                    VM.FormatView.Format_Cut_SelectedItem = "Yes";
                    VM.FormatView.Format_CutStart_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Hours_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Minutes_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Seconds_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Milliseconds_IsEnabled = true;

                    VM.FormatView.Format_CutEnd_IsEnabled = false;
                    //VM.FormatView.Format_CutEnd_Hours_IsEnabled = false;
                    //VM.FormatView.Format_CutEnd_Minutes_IsEnabled = false;
                    //VM.FormatView.Format_CutEnd_Seconds_IsEnabled = false;
                    //VM.FormatView.Format_CutEnd_Milliseconds_IsEnabled = false;

                    VM.FormatView.Format_CutEnd_Hours_Text = string.Empty;
                    VM.FormatView.Format_CutEnd_Minutes_Text = string.Empty;
                    VM.FormatView.Format_CutEnd_Seconds_Text = string.Empty;
                    VM.FormatView.Format_CutEnd_Milliseconds_Text = string.Empty;

                    // Frame
                    VM.FormatView.Format_FrameEnd_IsEnabled = false;
                    VM.FormatView.Format_FrameEnd_Text = "";

                    // YouTube
                    VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";


                    // -------------------------
                    // Video
                    // -------------------------
                    // Codec
                    VM.VideoView.Video_Codec_IsEnabled = true;

                    //Size
                    VM.VideoView.Video_Scale_IsEnabled = true;

                    // Scaling
                    VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                    VM.VideoView.Video_ScalingAlgorithm_IsEnabled = true;

                    // Speed
                    VM.VideoView.Video_Speed_IsEnabled = false;

                    // Screen Format
                    VM.VideoView.Video_ScreenFormat_IsEnabled = true;

                    // Aspect Ratio
                    VM.VideoView.Video_AspectRatio_IsEnabled = true;

                    // Crop
                    VM.VideoView.Video_Crop_IsEnabled = true;

                    // Fps
                    VM.VideoView.Video_FPS_SelectedItem = "auto";
                    VM.VideoView.Video_FPS_IsEnabled = false;

                    // Encode Speed
                    VM.VideoView.Video_EncodeSpeed_IsEnabled = false;

                    // -------------------------
                    // Audio
                    // -------------------------
                    // Codec
                    VM.AudioView.Audio_Codec_IsEnabled = false;

                    // Audio Stream
                    //VM.AudioView.Audio_Stream_SelectedItem = "none";
                    //AudioStreamControls(:; // Selected Item
                    VM.AudioView.Audio_Stream_IsEnabled = false;

                    // Channel
                    VM.AudioView.Audio_Channel_SelectedItem = "Source";
                    VM.AudioView.Audio_Channel_IsEnabled = false;

                    // Quality
                    VM.AudioView.Audio_Quality_SelectedItem = "None";
                    VM.AudioView.Audio_Quality_IsEnabled = false;

                    // Compression Level
                    VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                    VM.AudioView.Audio_CompressionLevel_IsEnabled = false;

                    // Sample Rate
                    VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                    VM.AudioView.Audio_SampleRate_IsEnabled = false;

                    // Bit Depth
                    VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                    VM.AudioView.Audio_BitDepth_IsEnabled = false;

                    // Volume
                    VM.AudioView.Audio_Volume_IsEnabled = false;

                    // Limiter
                    VM.AudioView.Audio_HardLimiter_IsEnabled = false;
                    VM.AudioView.Audio_HardLimiter_Value = 0.0;


                    // -------------------------
                    // Subtitle
                    // -------------------------
                    // Codec
                    VM.SubtitleView.Subtitle_Codec_IsEnabled = false;
                    break;

                // -------------------------
                // Sequence 
                // -------------------------
                case "Sequence":
                    // -------------------------
                    // Format
                    // -------------------------
                    // Hardware Acceleration
                    VM.VideoView.Video_HWAccel_IsEnabled = true;

                    // Cut
                    // Change if coming back from Image
                    if (VM.FormatView.Format_CutStart_IsEnabled == true &&
                        VM.FormatView.Format_CutEnd_IsEnabled == false)
                    {
                        VM.FormatView.Format_Cut_SelectedItem = "No";
                    }

                    // YouTube
                    VM.FormatView.Format_YouTube_SelectedItem = "Video + Audio";


                    // -------------------------
                    // Video
                    // -------------------------
                    // Codec 
                    VM.VideoView.Video_Codec_IsEnabled = true;

                    //Size
                    VM.VideoView.Video_Scale_IsEnabled = true;

                    // Scaling
                    VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";
                    VM.VideoView.Video_ScalingAlgorithm_IsEnabled = true;

                    // Speed
                    VM.VideoView.Video_Speed_IsEnabled = true;

                    // Screen Format
                    VM.VideoView.Video_ScreenFormat_IsEnabled = true;

                    // Aspect Ratio
                    VM.VideoView.Video_AspectRatio_IsEnabled = true;

                    // Crop
                    VM.VideoView.Video_Crop_IsEnabled = true;

                    // Speed
                    VM.VideoView.Video_EncodeSpeed_IsEnabled = false;

                    // FPS
                    VM.VideoView.Video_FPS_IsEnabled = true;


                    // -------------------------
                    // Audio
                    // -------------------------
                    // Codec
                    VM.AudioView.Audio_Codec_IsEnabled = false;

                    // Audio Stream
                    //VM.AudioView.Audio_Stream_SelectedItem = "none";
                    //AudioStreamControls(:; // Selected Item
                    VM.AudioView.Audio_Stream_IsEnabled = false;

                    // Channel
                    VM.AudioView.Audio_Channel_SelectedItem = "Source";
                    VM.AudioView.Audio_Channel_IsEnabled = false;

                    // Quality
                    VM.AudioView.Audio_Quality_SelectedItem = "None";
                    VM.AudioView.Audio_Quality_IsEnabled = false;

                    // Compression Level
                    VM.AudioView.Audio_CompressionLevel_SelectedItem = "auto";
                    VM.AudioView.Audio_CompressionLevel_IsEnabled = false;

                    // Sample Rate
                    VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                    VM.AudioView.Audio_SampleRate_IsEnabled = false;

                    // Bit Depth
                    VM.AudioView.Audio_BitDepth_SelectedItem = "auto";
                    VM.AudioView.Audio_BitDepth_IsEnabled = false;

                    // Volume
                    VM.AudioView.Audio_Volume_IsEnabled = false;

                    // Limiter
                    VM.AudioView.Audio_HardLimiter_IsEnabled = false;
                    VM.AudioView.Audio_HardLimiter_Value = 0.0;


                    // -------------------------
                    // Subtitle
                    // -------------------------
                    // Codec
                    VM.SubtitleView.Subtitle_Codec_IsEnabled = false;
                    break;
            }
            
            // -------------------------
            // Reset to 0 if Disabled
            // -------------------------
            if (VM.FormatView.Format_CutStart_IsEnabled == false &&
                VM.FormatView.Format_CutEnd_IsEnabled == false)
            {
                // Start
                VM.FormatView.Format_CutStart_Hours_Text = "00";
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                // End
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
            }
        }



        /// <summary>
        /// Audio Stream Controls
        /// </summary>
        public static void AudioStreamControls()
        {
            // --------------------------------------------------
            // Video 
            // Selected Audio Stream
            // --------------------------------------------------
            // -------------------------
            // webm
            // -------------------------
            if (VM.FormatView.Format_Container_SelectedItem == "webm")
            {
                // -------------------------
                // VP8
                // -------------------------
                if (VM.VideoView.Video_Codec_SelectedItem == "VP8")
                {
                    VM.AudioView.Audio_Stream_SelectedItem = "1";
                }

                // -------------------------
                // VP9
                // -------------------------
                else if (VM.VideoView.Video_Codec_SelectedItem == "VP9")
                {
                    VM.AudioView.Audio_Stream_SelectedItem = "all";
                }

                // -------------------------
                // None
                // -------------------------
                if (VM.AudioView.Audio_Codec_SelectedItem == "None")
                {
                    VM.AudioView.Audio_Stream_SelectedItem = "none";
                }
            }

            // -------------------------
            // mp4
            // mkv
            // mpg
            // avi
            // ogv
            // -------------------------
            else if (VM.FormatView.Format_Container_SelectedItem == "mp4" ||
                     VM.FormatView.Format_Container_SelectedItem == "mkv" ||
                     VM.FormatView.Format_Container_SelectedItem == "mpg" ||
                     VM.FormatView.Format_Container_SelectedItem == "avi" ||
                     VM.FormatView.Format_Container_SelectedItem == "ogv"
                     )
            {
                // -------------------------
                // None
                // -------------------------
                if (VM.AudioView.Audio_Codec_SelectedItem == "None")
                {
                    VM.AudioView.Audio_Stream_SelectedItem = "none";
                }

                // -------------------------
                // All Other Codecs
                // -------------------------
                else
                {
                    VM.AudioView.Audio_Stream_SelectedItem = "all";
                }
            }


            // --------------------------------------------------
            // Image / Sequence 
            // Selected Audio Stream
            // --------------------------------------------------
            // -------------------------
            // None
            // -------------------------
            else if (VM.FormatView.Format_Container_SelectedItem == "jpg" ||
                     VM.FormatView.Format_Container_SelectedItem == "png" ||
                     VM.FormatView.Format_Container_SelectedItem == "webp"
                )
            {
                // -------------------------
                // All Codecs
                // -------------------------
                VM.AudioView.Audio_Stream_SelectedItem = "none";
            }


            // --------------------------------------------------
            // Audio 
            // Selected Audio Stream
            // --------------------------------------------------
            // -------------------------
            // None
            // -------------------------
            else if (VM.FormatView.Format_Container_SelectedItem == "mp3" ||
                     VM.FormatView.Format_Container_SelectedItem == "m4a" ||
                     VM.FormatView.Format_Container_SelectedItem == "ogg" ||
                     VM.FormatView.Format_Container_SelectedItem == "flac" ||
                     VM.FormatView.Format_Container_SelectedItem == "wav"
                )
            {
                // -------------------------
                // None
                // -------------------------
                if (VM.AudioView.Audio_Codec_SelectedItem == "None")
                {
                    VM.AudioView.Audio_Stream_SelectedItem = "none";
                }

                // -------------------------
                // All Other Codecs
                // -------------------------
                else
                {
                    VM.AudioView.Audio_Stream_SelectedItem = "1";
                }
            }

        }



        /// <summary>
        /// Cut Controls
        /// </summary>
        public static void CutControls()
        {
            // Enable Aspect Custom

            // -------------------------
            // No
            // -------------------------
            if (VM.FormatView.Format_Cut_SelectedItem == "No")
            {
                // Cut
                VM.FormatView.Format_CutStart_IsEnabled = false;
                //VM.FormatView.Format_CutStart_Hours_IsEnabled = false;
                //VM.FormatView.Format_CutStart_Minutes_IsEnabled = false;
                //VM.FormatView.Format_CutStart_Seconds_IsEnabled = false;
                //VM.FormatView.Format_CutStart_Milliseconds_IsEnabled = false;

                VM.FormatView.Format_CutEnd_IsEnabled = false;
                //VM.FormatView.Format_CutEnd_Hours_IsEnabled = false;
                //VM.FormatView.Format_CutEnd_Minutes_IsEnabled = false;
                //VM.FormatView.Format_CutEnd_Seconds_IsEnabled = false;
                //VM.FormatView.Format_CutEnd_Milliseconds_IsEnabled = false;

                // Video / Sequence
                if (VM.FormatView.Format_MediaType_SelectedItem == "Video" ||
                    VM.FormatView.Format_MediaType_SelectedItem == "Sequence")
                {
                    // Start
                    VM.FormatView.Format_CutStart_Hours_Text = "00";
                    VM.FormatView.Format_CutStart_Minutes_Text = "00";
                    VM.FormatView.Format_CutStart_Seconds_Text = "00";
                    VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                    // End
                    VM.FormatView.Format_CutEnd_Hours_Text = "00";
                    VM.FormatView.Format_CutEnd_Minutes_Text = "00";
                    VM.FormatView.Format_CutEnd_Seconds_Text = "00";
                    VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
                }
                // Image
                else if (VM.FormatView.Format_MediaType_SelectedItem == "Image")
                {
                    //VM.FormatView.Format_CutStart_Text = "00:00:00.000";
                    //VM.FormatView.Format_CutEnd_Text = string.Empty;

                    // Start
                    VM.FormatView.Format_CutStart_Hours_Text = "00";
                    VM.FormatView.Format_CutStart_Minutes_Text = "00";
                    VM.FormatView.Format_CutStart_Seconds_Text = "00";
                    VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
                    // End
                    VM.FormatView.Format_CutEnd_Hours_Text = string.Empty;
                    VM.FormatView.Format_CutEnd_Minutes_Text = string.Empty;
                    VM.FormatView.Format_CutEnd_Seconds_Text = string.Empty;
                    VM.FormatView.Format_CutEnd_Milliseconds_Text = string.Empty;
                }

                // Frames
                VM.FormatView.Format_FrameStart_IsEnabled = false;
                VM.FormatView.Format_FrameEnd_IsEnabled = false;

                // Reset Text
                VM.FormatView.Format_FrameStart_Text = string.Empty;
                VM.FormatView.Format_FrameEnd_Text = string.Empty;
            }

            // -------------------------
            // Yes
            // -------------------------
            else if (VM.FormatView.Format_Cut_SelectedItem == "Yes")
            {
                // Frames

                // Only for Video
                if (VM.FormatView.Format_MediaType_SelectedItem == "Video") 
                {
                    // Time
                    VM.FormatView.Format_CutStart_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Hours_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Minutes_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Seconds_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Milliseconds_IsEnabled = true;

                    VM.FormatView.Format_CutEnd_IsEnabled = true;
                    //VM.FormatView.Format_CutEnd_Hours_IsEnabled = true;
                    //VM.FormatView.Format_CutEnd_Minutes_IsEnabled = true;
                    //VM.FormatView.Format_CutEnd_Seconds_IsEnabled = true;
                    //VM.FormatView.Format_CutEnd_Milliseconds_IsEnabled = true;

                    // Frames
                    VM.FormatView.Format_FrameStart_IsEnabled = true;
                    VM.FormatView.Format_FrameEnd_IsEnabled = true;
                }

                // Only for Video
                else if (VM.FormatView.Format_MediaType_SelectedItem == "Audio")
                {
                    // Time
                    VM.FormatView.Format_CutStart_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Hours_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Minutes_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Seconds_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Milliseconds_IsEnabled = true;

                    VM.FormatView.Format_CutEnd_IsEnabled = true;
                    //VM.FormatView.Format_CutEnd_Hours_IsEnabled = true;
                    //VM.FormatView.Format_CutEnd_Minutes_IsEnabled = true;
                    //VM.FormatView.Format_CutEnd_Seconds_IsEnabled = true;
                    //VM.FormatView.Format_CutEnd_Milliseconds_IsEnabled = true;

                    // Frames
                    VM.FormatView.Format_FrameStart_IsEnabled = false;
                    VM.FormatView.Format_FrameEnd_IsEnabled = false;

                    // Text
                    VM.FormatView.Format_FrameStart_Text = string.Empty;
                    VM.FormatView.Format_FrameEnd_Text = string.Empty;
                }

                // Only for Video
                else if (VM.FormatView.Format_MediaType_SelectedItem == "Image")
                {
                    // Time
                    VM.FormatView.Format_CutStart_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Hours_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Minutes_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Seconds_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Milliseconds_IsEnabled = true;

                    VM.FormatView.Format_CutEnd_IsEnabled = false;
                    //VM.FormatView.Format_CutEnd_Hours_IsEnabled = false;
                    //VM.FormatView.Format_CutEnd_Minutes_IsEnabled = false;
                    //VM.FormatView.Format_CutEnd_Seconds_IsEnabled = false;
                    //VM.FormatView.Format_CutEnd_Milliseconds_IsEnabled = false;

                    VM.FormatView.Format_CutEnd_Hours_Text = string.Empty;
                    VM.FormatView.Format_CutEnd_Minutes_Text = string.Empty;
                    VM.FormatView.Format_CutEnd_Seconds_Text = string.Empty;
                    VM.FormatView.Format_CutEnd_Milliseconds_Text = string.Empty;

                    // Frames
                    VM.FormatView.Format_FrameStart_IsEnabled = true;
                    VM.FormatView.Format_FrameEnd_IsEnabled = false;
                    VM.FormatView.Format_FrameEnd_Text = string.Empty;
                }

                // Only for Video
                else if (VM.FormatView.Format_MediaType_SelectedItem == "Sequence")
                {
                    // Time
                    VM.FormatView.Format_CutStart_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Hours_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Minutes_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Seconds_IsEnabled = true;
                    //VM.FormatView.Format_CutStart_Milliseconds_IsEnabled = true;

                    VM.FormatView.Format_CutEnd_IsEnabled = true;
                    //VM.FormatView.Format_CutEnd_Hours_IsEnabled = true;
                    //VM.FormatView.Format_CutEnd_Minutes_IsEnabled = true;
                    //VM.FormatView.Format_CutEnd_Seconds_IsEnabled = true;
                    //VM.FormatView.Format_CutEnd_Milliseconds_IsEnabled = true;

                    // Frames
                    VM.FormatView.Format_FrameStart_IsEnabled = true;
                    VM.FormatView.Format_FrameEnd_IsEnabled = true;
                }
            }
        } 


    }
}
