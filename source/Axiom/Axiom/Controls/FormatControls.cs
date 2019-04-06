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
        ///     ComboBoxes Item Sources
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        ///     Set Controls
        /// </summary>
        public static void SetControls(string container_SelectedItem)
        {
            // --------------------------------------------------
            // Containers
            // --------------------------------------------------
            // Select Codecs and Default Selected Items per container

            // --------------------------------------------------
            // Video
            // --------------------------------------------------
            // -------------------------
            // webm
            // -------------------------
            if (container_SelectedItem == "webm")
            {
                // Media Type
                FormatView.vm.Format_MediaType_Items = Containers.WebM.media;
                FormatView.vm.Format_MediaType_SelectedItem = "Video";
                FormatView.vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                VideoView.vm.Video_Codec_Items = Containers.WebM.video;
                VideoView.vm.Video_Codec_SelectedItem = "VP8";

                // Optimize
                VideoView.vm.Video_Optimize_SelectedItem = "Web";

                // Audio Codec
                AudioView.vm.Audio_Codec_Items = Containers.WebM.audio;
                AudioView.vm.Audio_Codec_SelectedItem = "Vorbis";

                // Subtitle Codec
                SubtitleView.vm.Subtitle_Codec_Items = Containers.WebM.subtitle;
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // mp4
            // -------------------------
            else if (container_SelectedItem == "mp4")
            {
                // Media Type
                FormatView.vm.Format_MediaType_Items = Containers.MP4.media;
                FormatView.vm.Format_MediaType_SelectedItem = "Video";
                FormatView.vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                VideoView.vm.Video_Codec_Items = Containers.MP4.video;
                VideoView.vm.Video_Codec_SelectedItem = "x264";

                // Audio Codec
                AudioView.vm.Audio_Codec_Items = Containers.MP4.audio;
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";

                // Subtitle Codec
                SubtitleView.vm.Subtitle_Codec_Items = Containers.MP4.subtitle;
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "MOV Text";
            }

            // -------------------------
            // mkv
            // -------------------------
            else if (container_SelectedItem == "mkv")
            {
                // Media Type
                FormatView.vm.Format_MediaType_Items = Containers.MKV.media;
                FormatView.vm.Format_MediaType_SelectedItem = "Video";
                FormatView.vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                VideoView.vm.Video_Codec_Items = Containers.MKV.video;
                VideoView.vm.Video_Codec_SelectedItem = "x264";

                // Audio Codec
                AudioView.vm.Audio_Codec_Items = Containers.MKV.audio;
                AudioView.vm.Audio_Codec_SelectedItem = "AC3";

                // Subtitle Codec
                SubtitleView.vm.Subtitle_Codec_Items = Containers.MKV.subtitle;
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "SSA";
            }

            // -------------------------
            // m2v
            // -------------------------
            else if (container_SelectedItem == "m2v")
            {
                // Media Type
                FormatView.vm.Format_MediaType_Items = Containers.M2V.media;
                FormatView.vm.Format_MediaType_SelectedItem = "Video";
                FormatView.vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                VideoView.vm.Video_Codec_Items = Containers.M2V.video;
                VideoView.vm.Video_Codec_SelectedItem = "MPEG-2";

                // Audio Codec
                AudioView.vm.Audio_Codec_Items = Containers.M2V.audio;
                AudioView.vm.Audio_Codec_SelectedItem = "None";

                // Subtitle Codec
                SubtitleView.vm.Subtitle_Codec_Items = Containers.M2V.subtitle;
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // mpg
            // -------------------------
            else if (container_SelectedItem == "mpg")
            {
                // Media Type
                FormatView.vm.Format_MediaType_Items = Containers.MPG.media;
                FormatView.vm.Format_MediaType_SelectedItem = "Video";
                FormatView.vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                VideoView.vm.Video_Codec_Items = Containers.MPG.video;
                VideoView.vm.Video_Codec_SelectedItem = "MPEG-2";

                // Audio Codec
                AudioView.vm.Audio_Codec_Items = Containers.MPG.audio;
                AudioView.vm.Audio_Codec_SelectedItem = "MP2";

                // Subtitle Codec
                SubtitleView.vm.Subtitle_Codec_Items = Containers.MPG.subtitle;
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "SRT";
            }

            // -------------------------
            // avi
            // -------------------------
            else if (container_SelectedItem == "avi")
            {
                // Media Type
                FormatView.vm.Format_MediaType_Items = Containers.AVI.media;
                FormatView.vm.Format_MediaType_SelectedItem = "Video";
                FormatView.vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                VideoView.vm.Video_Codec_Items = Containers.AVI.video;
                VideoView.vm.Video_Codec_SelectedItem = "MPEG-4";

                // Audio Codec
                AudioView.vm.Audio_Codec_Items = Containers.AVI.audio;
                AudioView.vm.Audio_Codec_SelectedItem = "LAME";

                // Subtitle Codec
                SubtitleView.vm.Subtitle_Codec_Items = Containers.AVI.subtitle;
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "SRT";
            }

            // -------------------------
            // ogv
            // -------------------------
            else if (container_SelectedItem == "ogv")
            {
                // Media Type
                FormatView.vm.Format_MediaType_Items = Containers.OGV.media;
                FormatView.vm.Format_MediaType_SelectedItem = "Video";
                FormatView.vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                VideoView.vm.Video_Codec_Items = Containers.OGV.video;
                VideoView.vm.Video_Codec_SelectedItem = "Theora";

                // Audio Codec
                AudioView.vm.Audio_Codec_Items = Containers.OGV.audio;
                AudioView.vm.Audio_Codec_SelectedItem = "Opus";

                // Subtitle Codec
                SubtitleView.vm.Subtitle_Codec_Items = Containers.OGV.subtitle;
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
            }


            // --------------------------------------------------
            // Audio
            // --------------------------------------------------
            // -------------------------
            // mp3
            // -------------------------
            else if (container_SelectedItem == "mp3")
            {
                // Media Type
                FormatView.vm.Format_MediaType_Items = Containers.LAME.media;
                FormatView.vm.Format_MediaType_SelectedItem = "Audio";
                FormatView.vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                VideoView.vm.Video_Codec_Items = Containers.LAME.video;
                VideoView.vm.Video_Codec_SelectedItem = "None";

                // Audio Codec
                AudioView.vm.Audio_Codec_Items = Containers.LAME.audio;
                AudioView.vm.Audio_Codec_SelectedItem = "LAME";

                // Subtitle Codec
                SubtitleView.vm.Subtitle_Codec_Items = Containers.LAME.subtitle;
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // m4a
            // -------------------------
            else if (container_SelectedItem == "m4a")
            {
                // Media Type
                FormatView.vm.Format_MediaType_Items = Containers.M4A.media;
                FormatView.vm.Format_MediaType_SelectedItem = "Audio";
                FormatView.vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                VideoView.vm.Video_Codec_Items = Containers.M4A.video;
                VideoView.vm.Video_Codec_SelectedItem = "None";

                // Audio Codec
                AudioView.vm.Audio_Codec_Items = Containers.M4A.audio;
                AudioView.vm.Audio_Codec_SelectedItem = "AAC";

                // Subtitle Codec
                SubtitleView.vm.Subtitle_Codec_Items = Containers.M4A.subtitle;
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // ogg
            // -------------------------
            else if (container_SelectedItem == "ogg")
            {
                // Media Type
                FormatView.vm.Format_MediaType_Items = Containers.OGG.media;
                FormatView.vm.Format_MediaType_SelectedItem = "Audio";
                FormatView.vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                VideoView.vm.Video_Codec_Items = Containers.OGG.video;
                VideoView.vm.Video_Codec_SelectedItem = "None";

                // Audio Codec
                AudioView.vm.Audio_Codec_Items = Containers.OGG.audio;
                AudioView.vm.Audio_Codec_SelectedItem = "Opus";

                // Subtitle Codec
                SubtitleView.vm.Subtitle_Codec_Items = Containers.OGG.subtitle;
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // flac
            // -------------------------
            else if (container_SelectedItem == "flac")
            {
                // Media Type
                FormatView.vm.Format_MediaType_Items = Containers.FLAC.media;
                FormatView.vm.Format_MediaType_SelectedItem = "Audio";
                FormatView.vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                VideoView.vm.Video_Codec_Items = Containers.FLAC.video;
                VideoView.vm.Video_Codec_SelectedItem = "None";

                // Audio Codec
                AudioView.vm.Audio_Codec_Items = Containers.FLAC.audio;
                AudioView.vm.Audio_Codec_SelectedItem = "FLAC";

                // Subtitle Codec
                SubtitleView.vm.Subtitle_Codec_Items = Containers.FLAC.subtitle;
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // wav
            // -------------------------
            else if (container_SelectedItem == "wav")
            {
                // Media Type
                FormatView.vm.Format_MediaType_Items = Containers.WAV.media;
                FormatView.vm.Format_MediaType_SelectedItem = "Audio";
                FormatView.vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                VideoView.vm.Video_Codec_Items = Containers.WAV.video;
                VideoView.vm.Video_Codec_SelectedItem = "None";

                // Audio Codec
                AudioView.vm.Audio_Codec_Items = Containers.WAV.audio;
                AudioView.vm.Audio_Codec_SelectedItem = "PCM";

                // Subtitle Codec
                SubtitleView.vm.Subtitle_Codec_Items = Containers.WAV.subtitle;
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
            }


            // --------------------------------------------------
            // Image
            // --------------------------------------------------
            // -------------------------
            // jpg
            // -------------------------
            else if (container_SelectedItem == "jpg")
            {
                // Media Type
                FormatView.vm.Format_MediaType_Items = Containers.JPG.media;
                FormatView.vm.Format_MediaType_SelectedItem = "Image";
                FormatView.vm.Format_MediaType_IsEnabled = true;

                // Video Codec
                VideoView.vm.Video_Codec_Items = Containers.JPG.video;
                VideoView.vm.Video_Codec_SelectedItem = "JPEG";

                // Audio Codec
                AudioView.vm.Audio_Codec_Items = Containers.JPG.audio;
                AudioView.vm.Audio_Codec_SelectedItem = "None";

                // Subtitle Codec
                SubtitleView.vm.Subtitle_Codec_Items = Containers.JPG.subtitle;
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // png
            // -------------------------
            else if (container_SelectedItem == "png")
            {
                // Media Type
                FormatView.vm.Format_MediaType_Items = Containers.JPG.media;
                FormatView.vm.Format_MediaType_SelectedItem = "Image";
                FormatView.vm.Format_MediaType_IsEnabled = true;

                // Video Codec
                VideoView.vm.Video_Codec_Items = Containers.PNG.video;
                VideoView.vm.Video_Codec_SelectedItem = "PNG";

                // Audio Codec
                AudioView.vm.Audio_Codec_Items = Containers.PNG.audio;
                AudioView.vm.Audio_Codec_SelectedItem = "None";

                // Subtitle Codec
                SubtitleView.vm.Subtitle_Codec_Items = Containers.PNG.subtitle;
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // webp
            // -------------------------
            else if (container_SelectedItem == "webp")
            {
                // Media Type
                FormatView.vm.Format_MediaType_Items = Containers.JPG.media;
                FormatView.vm.Format_MediaType_SelectedItem = "Image";
                FormatView.vm.Format_MediaType_IsEnabled = true;

                // Video Codec
                VideoView.vm.Video_Codec_Items = Containers.WebP.video;
                VideoView.vm.Video_Codec_SelectedItem = "WebP";

                // Audio Codec
                AudioView.vm.Audio_Codec_Items = Containers.WebP.audio;
                AudioView.vm.Audio_Codec_SelectedItem = "None";

                // Subtitle Codec
                SubtitleView.vm.Subtitle_Codec_Items = Containers.WebP.subtitle;
                SubtitleView.vm.Subtitle_Codec_SelectedItem = "None";
            }
        }



        /// <summary>
        ///     Get Output Extension
        /// </summary>
        public static void OutputFormatExt()
        {
            // Output Extension is Format ComboBox's Selected Item (eg mp4)
            MainWindow.outputExt = "." + FormatView.vm.Format_Container_SelectedItem;
        }



        /// <summary>
        ///     MediaType Controls Controls
        /// </summary>
        public static void MediaTypeControls()
        {
            // -------------------------
            // Video
            // -------------------------
            // Enable Frame Textbox for Image Screenshot
            if (FormatView.vm.Format_MediaType_SelectedItem == "Video")
            {
                // -------------------------
                // Format
                // -------------------------
                // Hardware Acceleration
                VideoView.vm.Video_HWAccel_IsEnabled = true;

                // Cut
                // Change if coming back from JPEG, PNG, WebP
                if (FormatView.vm.Format_CutStart_IsEnabled == true &&
                    FormatView.vm.Format_CutEnd_IsEnabled == false)
                {
                    FormatView.vm.Format_Cut_SelectedItem = "No";
                }

                // YouTube
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";

                // -------------------------
                // Video
                // -------------------------
                // Codec
                VideoView.vm.Video_Codec_IsEnabled = true;

                // Size
                VideoView.vm.Video_Scale_IsEnabled = true;

                // Scaling
                VideoView.vm.Video_ScalingAlgorithm_IsEnabled = true;
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";

                // Speed
                VideoView.vm.Video_Speed_IsEnabled = true;

                // Screen Format
                VideoView.vm.Video_ScreenFormat_IsEnabled = true;

                // Aspect Ratio
                VideoView.vm.Video_AspectRatio_IsEnabled = true;

                // Crop
                VideoView.vm.Video_Crop_IsEnabled = true;

                // -------------------------
                // Audio
                // -------------------------
                // Codec
                AudioView.vm.Audio_Codec_IsEnabled = true;

                // Channel
                AudioView.vm.Audio_Channel_IsEnabled = true;

                // Volume
                AudioView.vm.Audio_Volume_IsEnabled = true;

                // Limiter
                AudioView.vm.Audio_HardLimiter_IsEnabled = true;

                // Audio Stream
                AudioView.vm.Audio_Stream_IsEnabled = true;
                //AudioStreamControls(); // Selected Item
                //AudioView.vm.Audio_Stream_SelectedItem = "all";


                // -------------------------
                // Subtitle
                // -------------------------
                // Codec
                SubtitleView.vm.Subtitle_Codec_IsEnabled = true;
            }

            // -------------------------
            // Audio
            // -------------------------
            else if (FormatView.vm.Format_MediaType_SelectedItem == "Audio")
            {
                // -------------------------
                // Format
                // -------------------------
                // Hardware Acceleration
                VideoView.vm.Video_HWAccel_IsEnabled = false;
                VideoView.vm.Video_HWAccel_SelectedItem = "off";

                // Cut
                // Change if coming back from JPEG, PNG, WebP
                if (FormatView.vm.Format_CutStart_IsEnabled == true &&
                    FormatView.vm.Format_CutEnd_IsEnabled == false)
                {
                    FormatView.vm.Format_Cut_SelectedItem = "No";
                }

                // YouTube
                FormatView.vm.Format_YouTube_SelectedItem = "Audio Only";

                // Frame
                FormatView.vm.Format_FrameEnd_IsEnabled = false;
                FormatView.vm.Format_FrameEnd_Text = "";

                // -------------------------
                // Video
                // -------------------------
                // Codec
                VideoView.vm.Video_Codec_IsEnabled = false;

                // Size
                VideoView.vm.Video_Scale_SelectedItem = "Source";
                VideoView.vm.Video_Scale_IsEnabled = false;

                // Scaling
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_ScalingAlgorithm_IsEnabled = false;

                // Speed
                VideoView.vm.Video_Speed_IsEnabled = false;

                // Screen Format
                VideoView.vm.Video_ScreenFormat_IsEnabled = false;

                // Aspect Ratio
                VideoView.vm.Video_AspectRatio_IsEnabled = false;

                // Crop
                VideoView.vm.Video_Crop_IsEnabled = false;

                // FPS
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_FPS_IsEnabled = false;

                // Encode Speed
                VideoView.vm.Video_EncodeSpeed_IsEnabled = false;


                // -------------------------
                // Audio
                // -------------------------
                // Codec
                AudioView.vm.Audio_Codec_IsEnabled = true;

                // Audio Stream
                AudioView.vm.Audio_Stream_IsEnabled = true;
                //AudioStreamControls(); // Selected Item
                //AudioView.vm.Audio_Stream_SelectedItem = "1";

                // Channel
                AudioView.vm.Audio_Channel_IsEnabled = true;

                // Sample Rate
                // Controled Through Codec Class

                // Bit Depth
                // Controled Through Codec Class

                // Volume
                AudioView.vm.Audio_Volume_IsEnabled = true;

                // Limiter
                AudioView.vm.Audio_HardLimiter_IsEnabled = true;


                // -------------------------
                // Subtitle
                // -------------------------
                // Codec
                SubtitleView.vm.Subtitle_Codec_IsEnabled = false;
            }

            // -------------------------
            // Image
            // -------------------------
            else if (FormatView.vm.Format_MediaType_SelectedItem == "Image")
            {
                // -------------------------
                // Format
                // -------------------------
                // Hardware Acceleration
                VideoView.vm.Video_HWAccel_IsEnabled = true;

                // Cut
                // Enable Cut Start Time for Frame Selection
                FormatView.vm.Format_Cut_SelectedItem = "Yes";
                FormatView.vm.Format_CutStart_IsEnabled = true;
                //FormatView.vm.Format_CutStart_Hours_IsEnabled = true;
                //FormatView.vm.Format_CutStart_Minutes_IsEnabled = true;
                //FormatView.vm.Format_CutStart_Seconds_IsEnabled = true;
                //FormatView.vm.Format_CutStart_Milliseconds_IsEnabled = true;

                FormatView.vm.Format_CutEnd_IsEnabled = false;
                //FormatView.vm.Format_CutEnd_Hours_IsEnabled = false;
                //FormatView.vm.Format_CutEnd_Minutes_IsEnabled = false;
                //FormatView.vm.Format_CutEnd_Seconds_IsEnabled = false;
                //FormatView.vm.Format_CutEnd_Milliseconds_IsEnabled = false;

                FormatView.vm.Format_CutEnd_Hours_Text = string.Empty;
                FormatView.vm.Format_CutEnd_Minutes_Text = string.Empty;
                FormatView.vm.Format_CutEnd_Seconds_Text = string.Empty;
                FormatView.vm.Format_CutEnd_Milliseconds_Text = string.Empty;

                // Frame
                FormatView.vm.Format_FrameEnd_IsEnabled = false;
                FormatView.vm.Format_FrameEnd_Text = "";

                // YouTube
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";


                // -------------------------
                // Video
                // -------------------------
                // Codec
                VideoView.vm.Video_Codec_IsEnabled = true;

                //Size
                VideoView.vm.Video_Scale_IsEnabled = true;

                // Scaling
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_ScalingAlgorithm_IsEnabled = true;

                // Speed
                VideoView.vm.Video_Speed_IsEnabled = false;

                // Screen Format
                VideoView.vm.Video_ScreenFormat_IsEnabled = true;

                // Aspect Ratio
                VideoView.vm.Video_AspectRatio_IsEnabled = true;

                // Crop
                VideoView.vm.Video_Crop_IsEnabled = true;

                // Fps
                VideoView.vm.Video_FPS_SelectedItem = "auto";
                VideoView.vm.Video_FPS_IsEnabled = false;

                // Encode Speed
                VideoView.vm.Video_EncodeSpeed_IsEnabled = false;

                // -------------------------
                // Audio
                // -------------------------
                // Codec
                AudioView.vm.Audio_Codec_IsEnabled = false;

                // Audio Stream
                //AudioView.vm.Audio_Stream_SelectedItem = "none";
                //AudioStreamControls(); // Selected Item
                AudioView.vm.Audio_Stream_IsEnabled = false;

                // Channel
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Channel_IsEnabled = false;

                // Quality
                AudioView.vm.Audio_Quality_SelectedItem = "None";
                AudioView.vm.Audio_Quality_IsEnabled = false;

                // Compression Level
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_CompressionLevel_IsEnabled = false;

                // Sample Rate
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_IsEnabled = false;

                // Bit Depth
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_IsEnabled = false;

                // Volume
                AudioView.vm.Audio_Volume_IsEnabled = false;

                // Limiter
                AudioView.vm.Audio_HardLimiter_IsEnabled = false;
                AudioView.vm.Audio_HardLimiter_Value = 1;


                // -------------------------
                // Subtitle
                // -------------------------
                // Codec
                SubtitleView.vm.Subtitle_Codec_IsEnabled = false;
            }

            // -------------------------
            // Sequence 
            // -------------------------
            else if (FormatView.vm.Format_MediaType_SelectedItem == "Sequence")
            {
                // -------------------------
                // Format
                // -------------------------
                // Hardware Acceleration
                VideoView.vm.Video_HWAccel_IsEnabled = true;

                // Cut
                // Change if coming back from Image
                if (FormatView.vm.Format_CutStart_IsEnabled == true &&
                    FormatView.vm.Format_CutEnd_IsEnabled == false)
                {
                    FormatView.vm.Format_Cut_SelectedItem = "No";
                }

                // YouTube
                FormatView.vm.Format_YouTube_SelectedItem = "Video + Audio";


                // -------------------------
                // Video
                // -------------------------
                // Codec 
                VideoView.vm.Video_Codec_IsEnabled = true;

                //Size
                VideoView.vm.Video_Scale_IsEnabled = true;

                // Scaling
                VideoView.vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                VideoView.vm.Video_ScalingAlgorithm_IsEnabled = true;

                // Speed
                VideoView.vm.Video_Speed_IsEnabled = true;

                // Screen Format
                VideoView.vm.Video_ScreenFormat_IsEnabled = true;

                // Aspect Ratio
                VideoView.vm.Video_AspectRatio_IsEnabled = true;

                // Crop
                VideoView.vm.Video_Crop_IsEnabled = true;

                // Speed
                VideoView.vm.Video_EncodeSpeed_IsEnabled = false;

                // FPS
                VideoView.vm.Video_FPS_IsEnabled = true;


                // -------------------------
                // Audio
                // -------------------------
                // Codec
                AudioView.vm.Audio_Codec_IsEnabled = false;

                // Audio Stream
                //AudioView.vm.Audio_Stream_SelectedItem = "none";
                //AudioStreamControls(); // Selected Item
                AudioView.vm.Audio_Stream_IsEnabled = false;

                // Channel
                AudioView.vm.Audio_Channel_SelectedItem = "Source";
                AudioView.vm.Audio_Channel_IsEnabled = false;

                // Quality
                AudioView.vm.Audio_Quality_SelectedItem = "None";
                AudioView.vm.Audio_Quality_IsEnabled = false;

                // Compression Level
                AudioView.vm.Audio_CompressionLevel_SelectedItem = "auto";
                AudioView.vm.Audio_CompressionLevel_IsEnabled = false;

                // Sample Rate
                AudioView.vm.Audio_SampleRate_SelectedItem = "auto";
                AudioView.vm.Audio_SampleRate_IsEnabled = false;

                // Bit Depth
                AudioView.vm.Audio_BitDepth_SelectedItem = "auto";
                AudioView.vm.Audio_BitDepth_IsEnabled = false;

                // Volume
                AudioView.vm.Audio_Volume_IsEnabled = false;

                // Limiter
                AudioView.vm.Audio_HardLimiter_IsEnabled = false;
                AudioView.vm.Audio_HardLimiter_Value = 1;


                // -------------------------
                // Subtitle
                // -------------------------
                // Codec
                SubtitleView.vm.Subtitle_Codec_IsEnabled = false;
            }


            // -------------------------
            // Reset to 0 if Disabled
            // -------------------------
            if (FormatView.vm.Format_CutStart_IsEnabled == false &&
                FormatView.vm.Format_CutEnd_IsEnabled == false)
            {
                // Start
                FormatView.vm.Format_CutStart_Hours_Text = "00";
                FormatView.vm.Format_CutStart_Minutes_Text = "00";
                FormatView.vm.Format_CutStart_Seconds_Text = "00";
                FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                // End
                FormatView.vm.Format_CutEnd_Hours_Text = "00";
                FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
            }
        }



        /// <summary>
        ///     Audio Stream Controls
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
            if (FormatView.vm.Format_Container_SelectedItem == "webm")
            {
                // -------------------------
                // VP8
                // -------------------------
                if (VideoView.vm.Video_Codec_SelectedItem == "VP8")
                {
                    AudioView.vm.Audio_Stream_SelectedItem = "1";
                }

                // -------------------------
                // VP9
                // -------------------------
                else if (VideoView.vm.Video_Codec_SelectedItem == "VP9")
                {
                    AudioView.vm.Audio_Stream_SelectedItem = "all";
                }

                // -------------------------
                // None
                // -------------------------
                if (AudioView.vm.Audio_Codec_SelectedItem == "None")
                {
                    AudioView.vm.Audio_Stream_SelectedItem = "none";
                }
            }

            // -------------------------
            // mp4
            // mkv
            // mpg
            // avi
            // ogv
            // -------------------------
            else if (FormatView.vm.Format_Container_SelectedItem == "mp4" ||
                     FormatView.vm.Format_Container_SelectedItem == "mkv" ||
                     FormatView.vm.Format_Container_SelectedItem == "mpg" ||
                     FormatView.vm.Format_Container_SelectedItem == "avi" ||
                     FormatView.vm.Format_Container_SelectedItem == "ogv"
                     )
            {
                // -------------------------
                // None
                // -------------------------
                if (AudioView.vm.Audio_Codec_SelectedItem == "None")
                {
                    AudioView.vm.Audio_Stream_SelectedItem = "none";
                }

                // -------------------------
                // All Other Codecs
                // -------------------------
                else
                {
                    AudioView.vm.Audio_Stream_SelectedItem = "all";
                }
            }


            // --------------------------------------------------
            // Image / Sequence 
            // Selected Audio Stream
            // --------------------------------------------------
            // -------------------------
            // None
            // -------------------------
            else if (FormatView.vm.Format_Container_SelectedItem == "jpg" ||
                     FormatView.vm.Format_Container_SelectedItem == "png" ||
                     FormatView.vm.Format_Container_SelectedItem == "webp"
                )
            {
                // -------------------------
                // All Codecs
                // -------------------------
                AudioView.vm.Audio_Stream_SelectedItem = "none";
            }


            // --------------------------------------------------
            // Audio 
            // Selected Audio Stream
            // --------------------------------------------------
            // -------------------------
            // None
            // -------------------------
            else if (FormatView.vm.Format_Container_SelectedItem == "mp3" ||
                     FormatView.vm.Format_Container_SelectedItem == "m4a" ||
                     FormatView.vm.Format_Container_SelectedItem == "ogg" ||
                     FormatView.vm.Format_Container_SelectedItem == "flac" ||
                     FormatView.vm.Format_Container_SelectedItem == "wav"
                )
            {
                // -------------------------
                // None
                // -------------------------
                if (AudioView.vm.Audio_Codec_SelectedItem == "None")
                {
                    AudioView.vm.Audio_Stream_SelectedItem = "none";
                }

                // -------------------------
                // All Other Codecs
                // -------------------------
                else
                {
                    AudioView.vm.Audio_Stream_SelectedItem = "1";
                }
            }

        }



        /// <summary>
        ///     Cut Controls
        /// </summary>
        public static void CutControls()
        {
            // Enable Aspect Custom

            // -------------------------
            // No
            // -------------------------
            if (FormatView.vm.Format_Cut_SelectedItem == "No")
            {
                // Cut
                FormatView.vm.Format_CutStart_IsEnabled = false;
                //FormatView.vm.Format_CutStart_Hours_IsEnabled = false;
                //FormatView.vm.Format_CutStart_Minutes_IsEnabled = false;
                //FormatView.vm.Format_CutStart_Seconds_IsEnabled = false;
                //FormatView.vm.Format_CutStart_Milliseconds_IsEnabled = false;

                FormatView.vm.Format_CutEnd_IsEnabled = false;
                //FormatView.vm.Format_CutEnd_Hours_IsEnabled = false;
                //FormatView.vm.Format_CutEnd_Minutes_IsEnabled = false;
                //FormatView.vm.Format_CutEnd_Seconds_IsEnabled = false;
                //FormatView.vm.Format_CutEnd_Milliseconds_IsEnabled = false;

                // Video / Sequence
                if (FormatView.vm.Format_MediaType_SelectedItem == "Video" ||
                    FormatView.vm.Format_MediaType_SelectedItem == "Sequence")
                {
                    // Start
                    FormatView.vm.Format_CutStart_Hours_Text = "00";
                    FormatView.vm.Format_CutStart_Minutes_Text = "00";
                    FormatView.vm.Format_CutStart_Seconds_Text = "00";
                    FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                    // End
                    FormatView.vm.Format_CutEnd_Hours_Text = "00";
                    FormatView.vm.Format_CutEnd_Minutes_Text = "00";
                    FormatView.vm.Format_CutEnd_Seconds_Text = "00";
                    FormatView.vm.Format_CutEnd_Milliseconds_Text = "000";
                }
                // Image
                else if (FormatView.vm.Format_MediaType_SelectedItem == "Image")
                {
                    //FormatView.vm.Format_CutStart_Text = "00:00:00.000";
                    //FormatView.vm.Format_CutEnd_Text = string.Empty;

                    // Start
                    FormatView.vm.Format_CutStart_Hours_Text = "00";
                    FormatView.vm.Format_CutStart_Minutes_Text = "00";
                    FormatView.vm.Format_CutStart_Seconds_Text = "00";
                    FormatView.vm.Format_CutStart_Milliseconds_Text = "000";
                    // End
                    FormatView.vm.Format_CutEnd_Hours_Text = string.Empty;
                    FormatView.vm.Format_CutEnd_Minutes_Text = string.Empty;
                    FormatView.vm.Format_CutEnd_Seconds_Text = string.Empty;
                    FormatView.vm.Format_CutEnd_Milliseconds_Text = string.Empty;
                }

                // Frames
                FormatView.vm.Format_FrameStart_IsEnabled = false;
                FormatView.vm.Format_FrameEnd_IsEnabled = false;

                // Reset Text
                FormatView.vm.Format_FrameStart_Text = string.Empty;
                FormatView.vm.Format_FrameEnd_Text = string.Empty;
            }

            // -------------------------
            // Yes
            // -------------------------
            else if (FormatView.vm.Format_Cut_SelectedItem == "Yes")
            {
                // Frames

                // Only for Video
                if (FormatView.vm.Format_MediaType_SelectedItem == "Video") 
                {
                    // Time
                    FormatView.vm.Format_CutStart_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Hours_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Minutes_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Seconds_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Milliseconds_IsEnabled = true;

                    FormatView.vm.Format_CutEnd_IsEnabled = true;
                    //FormatView.vm.Format_CutEnd_Hours_IsEnabled = true;
                    //FormatView.vm.Format_CutEnd_Minutes_IsEnabled = true;
                    //FormatView.vm.Format_CutEnd_Seconds_IsEnabled = true;
                    //FormatView.vm.Format_CutEnd_Milliseconds_IsEnabled = true;

                    // Frames
                    FormatView.vm.Format_FrameStart_IsEnabled = true;
                    FormatView.vm.Format_FrameEnd_IsEnabled = true;
                }

                // Only for Video
                else if (FormatView.vm.Format_MediaType_SelectedItem == "Audio")
                {
                    // Time
                    FormatView.vm.Format_CutStart_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Hours_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Minutes_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Seconds_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Milliseconds_IsEnabled = true;

                    FormatView.vm.Format_CutEnd_IsEnabled = true;
                    //FormatView.vm.Format_CutEnd_Hours_IsEnabled = true;
                    //FormatView.vm.Format_CutEnd_Minutes_IsEnabled = true;
                    //FormatView.vm.Format_CutEnd_Seconds_IsEnabled = true;
                    //FormatView.vm.Format_CutEnd_Milliseconds_IsEnabled = true;

                    // Frames
                    FormatView.vm.Format_FrameStart_IsEnabled = false;
                    FormatView.vm.Format_FrameEnd_IsEnabled = false;

                    // Text
                    FormatView.vm.Format_FrameStart_Text = string.Empty;
                    FormatView.vm.Format_FrameEnd_Text = string.Empty;
                }

                // Only for Video
                else if (FormatView.vm.Format_MediaType_SelectedItem == "Image")
                {
                    // Time
                    FormatView.vm.Format_CutStart_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Hours_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Minutes_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Seconds_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Milliseconds_IsEnabled = true;

                    FormatView.vm.Format_CutEnd_IsEnabled = false;
                    //FormatView.vm.Format_CutEnd_Hours_IsEnabled = false;
                    //FormatView.vm.Format_CutEnd_Minutes_IsEnabled = false;
                    //FormatView.vm.Format_CutEnd_Seconds_IsEnabled = false;
                    //FormatView.vm.Format_CutEnd_Milliseconds_IsEnabled = false;

                    FormatView.vm.Format_CutEnd_Hours_Text = string.Empty;
                    FormatView.vm.Format_CutEnd_Minutes_Text = string.Empty;
                    FormatView.vm.Format_CutEnd_Seconds_Text = string.Empty;
                    FormatView.vm.Format_CutEnd_Milliseconds_Text = string.Empty;

                    // Frames
                    FormatView.vm.Format_FrameStart_IsEnabled = true;
                    FormatView.vm.Format_FrameEnd_IsEnabled = false;
                    FormatView.vm.Format_FrameEnd_Text = string.Empty;
                }

                // Only for Video
                else if (FormatView.vm.Format_MediaType_SelectedItem == "Sequence")
                {
                    // Time
                    FormatView.vm.Format_CutStart_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Hours_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Minutes_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Seconds_IsEnabled = true;
                    //FormatView.vm.Format_CutStart_Milliseconds_IsEnabled = true;

                    FormatView.vm.Format_CutEnd_IsEnabled = true;
                    //FormatView.vm.Format_CutEnd_Hours_IsEnabled = true;
                    //FormatView.vm.Format_CutEnd_Minutes_IsEnabled = true;
                    //FormatView.vm.Format_CutEnd_Seconds_IsEnabled = true;
                    //FormatView.vm.Format_CutEnd_Milliseconds_IsEnabled = true;

                    // Frames
                    FormatView.vm.Format_FrameStart_IsEnabled = true;
                    FormatView.vm.Format_FrameEnd_IsEnabled = true;
                }
            }
        } 


    }
}
