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
        public static void SetControls(ViewModel vm, string container_SelectedItem)
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
                vm.Format_MediaType_Items = Containers.WebM.media;
                vm.Format_MediaType_SelectedItem = "Video";
                vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                vm.Video_Codec_Items = Containers.WebM.video;
                vm.Video_Codec_SelectedItem = "VP8";

                // Audio Codec
                vm.Audio_Codec_Items = Containers.WebM.audio;
                vm.Audio_Codec_SelectedItem = "Vorbis";

                // Subtitle Codec
                vm.Subtitle_Codec_Items = Containers.WebM.subtitle;
                vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // mp4
            // -------------------------
            else if (container_SelectedItem == "mp4")
            {
                // Media Type
                vm.Format_MediaType_Items = Containers.MP4.media;
                vm.Format_MediaType_SelectedItem = "Video";
                vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                vm.Video_Codec_Items = Containers.MP4.video;
                vm.Video_Codec_SelectedItem = "x264";

                // Audio Codec
                vm.Audio_Codec_Items = Containers.MP4.audio;
                vm.Audio_Codec_SelectedItem = "AAC";

                // Subtitle Codec
                vm.Subtitle_Codec_Items = Containers.MP4.subtitle;
                vm.Subtitle_Codec_SelectedItem = "MOV Text";
            }

            // -------------------------
            // mkv
            // -------------------------
            else if (container_SelectedItem == "mkv")
            {
                // Media Type
                vm.Format_MediaType_Items = Containers.MKV.media;
                vm.Format_MediaType_SelectedItem = "Video";
                vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                vm.Video_Codec_Items = Containers.MKV.video;
                vm.Video_Codec_SelectedItem = "x264";

                // Audio Codec
                vm.Audio_Codec_Items = Containers.MKV.audio;
                vm.Audio_Codec_SelectedItem = "AC3";

                // Subtitle Codec
                vm.Subtitle_Codec_Items = Containers.MKV.subtitle;
                vm.Subtitle_Codec_SelectedItem = "SSA";
            }

            // -------------------------
            // m2v
            // -------------------------
            else if (container_SelectedItem == "m2v")
            {
                // Media Type
                vm.Format_MediaType_Items = Containers.M2V.media;
                vm.Format_MediaType_SelectedItem = "Video";
                vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                vm.Video_Codec_Items = Containers.M2V.video;
                vm.Video_Codec_SelectedItem = "MPEG-2";

                // Audio Codec
                vm.Audio_Codec_Items = Containers.M2V.audio;
                vm.Audio_Codec_SelectedItem = "None";

                // Subtitle Codec
                vm.Subtitle_Codec_Items = Containers.M2V.subtitle;
                vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // mpg
            // -------------------------
            else if (container_SelectedItem == "mpg")
            {
                // Media Type
                vm.Format_MediaType_Items = Containers.MPG.media;
                vm.Format_MediaType_SelectedItem = "Video";
                vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                vm.Video_Codec_Items = Containers.MPG.video;
                vm.Video_Codec_SelectedItem = "MPEG-2";

                // Audio Codec
                vm.Audio_Codec_Items = Containers.MPG.audio;
                vm.Audio_Codec_SelectedItem = "MP2";

                // Subtitle Codec
                vm.Subtitle_Codec_Items = Containers.MPG.subtitle;
                vm.Subtitle_Codec_SelectedItem = "SRT";
            }

            // -------------------------
            // avi
            // -------------------------
            else if (container_SelectedItem == "avi")
            {
                // Media Type
                vm.Format_MediaType_Items = Containers.AVI.media;
                vm.Format_MediaType_SelectedItem = "Video";
                vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                vm.Video_Codec_Items = Containers.AVI.video;
                vm.Video_Codec_SelectedItem = "MPEG-4";

                // Audio Codec
                vm.Audio_Codec_Items = Containers.AVI.audio;
                vm.Audio_Codec_SelectedItem = "LAME";

                // Subtitle Codec
                vm.Subtitle_Codec_Items = Containers.AVI.subtitle;
                vm.Subtitle_Codec_SelectedItem = "SRT";
            }

            // -------------------------
            // ogv
            // -------------------------
            else if (container_SelectedItem == "ogv")
            {
                // Media Type
                vm.Format_MediaType_Items = Containers.OGV.media;
                vm.Format_MediaType_SelectedItem = "Video";
                vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                vm.Video_Codec_Items = Containers.OGV.video;
                vm.Video_Codec_SelectedItem = "Theora";

                // Audio Codec
                vm.Audio_Codec_Items = Containers.OGV.audio;
                vm.Audio_Codec_SelectedItem = "Opus";

                // Subtitle Codec
                vm.Subtitle_Codec_Items = Containers.OGV.subtitle;
                vm.Subtitle_Codec_SelectedItem = "None";
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
                vm.Format_MediaType_Items = Containers.LAME.media;
                vm.Format_MediaType_SelectedItem = "Audio";
                vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                vm.Video_Codec_Items = Containers.LAME.video;
                vm.Video_Codec_SelectedItem = "None";

                // Audio Codec
                vm.Audio_Codec_Items = Containers.LAME.audio;
                vm.Audio_Codec_SelectedItem = "LAME";

                // Subtitle Codec
                vm.Subtitle_Codec_Items = Containers.LAME.subtitle;
                vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // m4a
            // -------------------------
            else if (container_SelectedItem == "m4a")
            {
                // Media Type
                vm.Format_MediaType_Items = Containers.M4A.media;
                vm.Format_MediaType_SelectedItem = "Audio";
                vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                vm.Video_Codec_Items = Containers.M4A.video;
                vm.Video_Codec_SelectedItem = "None";

                // Audio Codec
                vm.Audio_Codec_Items = Containers.M4A.audio;
                vm.Audio_Codec_SelectedItem = "AAC";

                // Subtitle Codec
                vm.Subtitle_Codec_Items = Containers.M4A.subtitle;
                vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // ogg
            // -------------------------
            else if (container_SelectedItem == "ogg")
            {
                // Media Type
                vm.Format_MediaType_Items = Containers.OGG.media;
                vm.Format_MediaType_SelectedItem = "Audio";
                vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                vm.Video_Codec_Items = Containers.OGG.video;
                vm.Video_Codec_SelectedItem = "None";

                // Audio Codec
                vm.Audio_Codec_Items = Containers.OGG.audio;
                vm.Audio_Codec_SelectedItem = "Opus";

                // Subtitle Codec
                vm.Subtitle_Codec_Items = Containers.OGG.subtitle;
                vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // flac
            // -------------------------
            else if (container_SelectedItem == "flac")
            {
                // Media Type
                vm.Format_MediaType_Items = Containers.FLAC.media;
                vm.Format_MediaType_SelectedItem = "Audio";
                vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                vm.Video_Codec_Items = Containers.FLAC.video;
                vm.Video_Codec_SelectedItem = "None";

                // Audio Codec
                vm.Audio_Codec_Items = Containers.FLAC.audio;
                vm.Audio_Codec_SelectedItem = "FLAC";

                // Subtitle Codec
                vm.Subtitle_Codec_Items = Containers.FLAC.subtitle;
                vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // wav
            // -------------------------
            else if (container_SelectedItem == "wav")
            {
                // Media Type
                vm.Format_MediaType_Items = Containers.WAV.media;
                vm.Format_MediaType_SelectedItem = "Audio";
                vm.Format_MediaType_IsEnabled = false;

                // Video Codec
                vm.Video_Codec_Items = Containers.WAV.video;
                vm.Video_Codec_SelectedItem = "None";

                // Audio Codec
                vm.Audio_Codec_Items = Containers.WAV.audio;
                vm.Audio_Codec_SelectedItem = "PCM";

                // Subtitle Codec
                vm.Subtitle_Codec_Items = Containers.WAV.subtitle;
                vm.Subtitle_Codec_SelectedItem = "None";
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
                vm.Format_MediaType_Items = Containers.JPG.media;
                vm.Format_MediaType_SelectedItem = "Image";
                vm.Format_MediaType_IsEnabled = true;

                // Video Codec
                vm.Video_Codec_Items = Containers.JPG.video;
                vm.Video_Codec_SelectedItem = "JPEG";

                // Audio Codec
                vm.Audio_Codec_Items = Containers.JPG.audio;
                vm.Audio_Codec_SelectedItem = "None";

                // Subtitle Codec
                vm.Subtitle_Codec_Items = Containers.JPG.subtitle;
                vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // png
            // -------------------------
            else if (container_SelectedItem == "png")
            {
                // Media Type
                vm.Format_MediaType_Items = Containers.JPG.media;
                vm.Format_MediaType_SelectedItem = "Image";
                vm.Format_MediaType_IsEnabled = true;

                // Video Codec
                vm.Video_Codec_Items = Containers.PNG.video;
                vm.Video_Codec_SelectedItem = "PNG";

                // Audio Codec
                vm.Audio_Codec_Items = Containers.PNG.audio;
                vm.Audio_Codec_SelectedItem = "None";

                // Subtitle Codec
                vm.Subtitle_Codec_Items = Containers.PNG.subtitle;
                vm.Subtitle_Codec_SelectedItem = "None";
            }

            // -------------------------
            // webp
            // -------------------------
            else if (container_SelectedItem == "webp")
            {
                // Media Type
                vm.Format_MediaType_Items = Containers.JPG.media;
                vm.Format_MediaType_SelectedItem = "Image";
                vm.Format_MediaType_IsEnabled = true;

                // Video Codec
                vm.Video_Codec_Items = Containers.WebP.video;
                vm.Video_Codec_SelectedItem = "WebP";

                // Audio Codec
                vm.Audio_Codec_Items = Containers.WebP.audio;
                vm.Audio_Codec_SelectedItem = "None";

                // Subtitle Codec
                vm.Subtitle_Codec_Items = Containers.WebP.subtitle;
                vm.Subtitle_Codec_SelectedItem = "None";
            }
        }



        /// <summary>
        ///     Get Output Extension
        /// </summary>
        public static void OutputFormatExt(ViewModel vm)
        {
            // Output Extension is Format ComboBox's Selected Item (eg mp4)
            MainWindow.outputExt = "." + vm.Format_Container_SelectedItem;
        }



        /// <summary>
        ///     MediaType Controls Controls
        /// </summary>
        public static void MediaTypeControls(ViewModel vm)
        {
            // -------------------------
            // Video
            // -------------------------
            // Enable Frame Textbox for Image Screenshot
            if (vm.Format_MediaType_SelectedItem == "Video")
            {
                // -------------------------
                // Format
                // -------------------------
                // Hardware Acceleration
                vm.Format_HWAccel_IsEnabled = true;

                // Cut
                // Change if coming back from JPEG, PNG, WebP
                if (vm.Format_CutStart_IsEnabled == true &&
                    vm.Format_CutEnd_IsEnabled == false)
                {
                    vm.Format_Cut_SelectedItem = "No";
                }

                // YouTube
                vm.Format_YouTube_SelectedItem = "Video + Audio";

                // -------------------------
                // Video
                // -------------------------
                // Codec
                vm.Video_Codec_IsEnabled = true;

                // Size
                vm.Video_Scale_IsEnabled = true;

                // Scaling
                vm.Video_ScalingAlgorithm_IsEnabled = true;
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";

                // Speed
                vm.Video_Speed_IsEnabled = true;

                // Aspect Ratio
                vm.Video_AspectRatio_IsEnabled = true;

                // Crop
                vm.Video_Crop_IsEnabled = true;

                // -------------------------
                // Audio
                // -------------------------
                // Codec
                vm.Audio_Codec_IsEnabled = true;

                // Channel
                vm.Audio_Channel_IsEnabled = true;

                // Volume
                vm.Audio_Volume_IsEnabled = true;

                // Limiter
                vm.Audio_HardLimiter_IsEnabled = true;

                // Audio Stream
                vm.Audio_Stream_IsEnabled = true;
                vm.Audio_Stream_SelectedItem = "all";


                // -------------------------
                // Subtitle
                // -------------------------
                // Codec
                vm.Subtitle_Codec_IsEnabled = true;
            }

            // -------------------------
            // Audio
            // -------------------------
            else if (vm.Format_MediaType_SelectedItem == "Audio")
            {
                // -------------------------
                // Format
                // -------------------------
                // Hardware Acceleration
                vm.Format_HWAccel_IsEnabled = false;
                vm.Format_HWAccel_SelectedItem = "off";

                // Cut
                // Change if coming back from JPEG, PNG, WebP
                if (vm.Format_CutStart_IsEnabled == true &&
                    vm.Format_CutEnd_IsEnabled == false)
                {
                    vm.Format_Cut_SelectedItem = "No";
                }

                // YouTube
                vm.Format_YouTube_SelectedItem = "Audio Only";

                // Frame
                vm.Format_FrameEnd_IsEnabled = false;
                vm.Format_FrameEnd_Text = "";

                // -------------------------
                // Video
                // -------------------------
                // Codec
                vm.Video_Codec_IsEnabled = false;

                // Size
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_Scale_IsEnabled = false;

                // Scaling
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_IsEnabled = false;

                // Speed
                vm.Video_Speed_IsEnabled = false;

                // Aspect Ratio
                vm.Video_AspectRatio_IsEnabled = false;

                // Crop
                vm.Video_Crop_IsEnabled = false;

                // FPS
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_FPS_IsEnabled = false;

                // Encode Speed
                vm.Video_EncodeSpeed_IsEnabled = false;


                // -------------------------
                // Audio
                // -------------------------
                // Codec
                vm.Audio_Codec_IsEnabled = true;

                // Audio Stream
                vm.Audio_Stream_IsEnabled = true;
                vm.Audio_Stream_SelectedItem = "1";

                // Channel
                vm.Audio_Channel_IsEnabled = true;

                // Sample Rate
                // Controled Through Codec Class

                // Bit Depth
                // Controled Through Codec Class

                // Volume
                vm.Audio_Volume_IsEnabled = true;

                // Limiter
                vm.Audio_HardLimiter_IsEnabled = true;


                // -------------------------
                // Subtitle
                // -------------------------
                // Codec
                vm.Subtitle_Codec_IsEnabled = false;
            }

            // -------------------------
            // Image
            // -------------------------
            else if (vm.Format_MediaType_SelectedItem == "Image")
            {
                // -------------------------
                // Format
                // -------------------------
                // Hardware Acceleration
                vm.Format_HWAccel_IsEnabled = true;

                // Cut
                // Enable Cut Start Time for Frame Selection
                vm.Format_Cut_SelectedItem = "Yes";
                vm.Format_CutStart_IsEnabled = true;
                vm.Format_CutStart_Hours_IsEnabled = true;
                vm.Format_CutStart_Minutes_IsEnabled = true;
                vm.Format_CutStart_Seconds_IsEnabled = true;
                vm.Format_CutStart_Milliseconds_IsEnabled = true;

                vm.Format_CutEnd_IsEnabled = false;
                vm.Format_CutEnd_Hours_IsEnabled = false;
                vm.Format_CutEnd_Minutes_IsEnabled = false;
                vm.Format_CutEnd_Seconds_IsEnabled = false;
                vm.Format_CutEnd_Milliseconds_IsEnabled = false;

                vm.Format_CutEnd_Hours_Text = string.Empty;
                vm.Format_CutEnd_Minutes_Text = string.Empty;
                vm.Format_CutEnd_Seconds_Text = string.Empty;
                vm.Format_CutEnd_Milliseconds_Text = string.Empty;

                // Frame
                vm.Format_FrameEnd_IsEnabled = false;
                vm.Format_FrameEnd_Text = "";

                // YouTube
                vm.Format_YouTube_SelectedItem = "Video + Audio";


                // -------------------------
                // Video
                // -------------------------
                // Codec
                vm.Video_Codec_IsEnabled = true;

                //Size
                vm.Video_Scale_IsEnabled = true;

                // Scaling
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_IsEnabled = true;

                // Speed
                vm.Video_Speed_IsEnabled = false;

                // Aspect Ratio
                vm.Video_AspectRatio_IsEnabled = true;

                // Crop
                vm.Video_Crop_IsEnabled = true;

                // Fps
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_FPS_IsEnabled = false;

                // Encode Speed
                vm.Video_EncodeSpeed_IsEnabled = false;

                // -------------------------
                // Audio
                // -------------------------
                // Codec
                vm.Audio_Codec_IsEnabled = false;

                // Audio Stream
                vm.Audio_Stream_SelectedItem = "none";
                vm.Audio_Stream_IsEnabled = false;

                // Channel
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Channel_IsEnabled = false;

                // Quality
                vm.Audio_Quality_SelectedItem = "None";
                vm.Audio_Quality_IsEnabled = false;

                // Compression Level
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_CompressionLevel_IsEnabled = false;

                // Sample Rate
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_SampleRate_IsEnabled = false;

                // Bit Depth
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_BitDepth_IsEnabled = false;

                // Volume
                vm.Audio_Volume_IsEnabled = false;

                // Limiter
                vm.Audio_HardLimiter_IsEnabled = false;
                vm.Audio_HardLimiter_Value = 1;


                // -------------------------
                // Subtitle
                // -------------------------
                // Codec
                vm.Subtitle_Codec_IsEnabled = false;
            }

            // -------------------------
            // Sequence 
            // -------------------------
            else if (vm.Format_MediaType_SelectedItem == "Sequence")
            {
                // -------------------------
                // Format
                // -------------------------
                // Hardware Acceleration
                vm.Format_HWAccel_IsEnabled = true;

                // Cut
                // Change if coming back from Image
                if (vm.Format_CutStart_IsEnabled == true &&
                    vm.Format_CutEnd_IsEnabled == false)
                {
                    vm.Format_Cut_SelectedItem = "No";
                }

                // YouTube
                vm.Format_YouTube_SelectedItem = "Video + Audio";


                // -------------------------
                // Video
                // -------------------------
                // Codec 
                vm.Video_Codec_IsEnabled = true;

                //Size
                vm.Video_Scale_IsEnabled = true;

                // Scaling
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
                vm.Video_ScalingAlgorithm_IsEnabled = true;

                // Speed
                vm.Video_Speed_IsEnabled = true;

                // Aspect Ratio
                vm.Video_AspectRatio_IsEnabled = true;

                // Crop
                vm.Video_Crop_IsEnabled = true;

                // Speed
                vm.Video_EncodeSpeed_IsEnabled = false;

                // FPS
                vm.Video_FPS_IsEnabled = true;


                // -------------------------
                // Audio
                // -------------------------
                // Codec
                vm.Audio_Codec_IsEnabled = false;

                // Audio Stream
                vm.Audio_Stream_SelectedItem = "none";
                vm.Audio_Stream_IsEnabled = false;

                // Channel
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_Channel_IsEnabled = false;

                // Quality
                vm.Audio_Quality_SelectedItem = "None";
                vm.Audio_Quality_IsEnabled = false;

                // Compression Level
                vm.Audio_CompressionLevel_SelectedItem = "auto";
                vm.Audio_CompressionLevel_IsEnabled = false;

                // Sample Rate
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_SampleRate_IsEnabled = false;

                // Bit Depth
                vm.Audio_BitDepth_SelectedItem = "auto";
                vm.Audio_BitDepth_IsEnabled = false;

                // Volume
                vm.Audio_Volume_IsEnabled = false;

                // Limiter
                vm.Audio_HardLimiter_IsEnabled = false;
                vm.Audio_HardLimiter_Value = 1;


                // -------------------------
                // Subtitle
                // -------------------------
                // Codec
                vm.Subtitle_Codec_IsEnabled = false;
            }


            // -------------------------
            // Reset to 0 if Disabled
            // -------------------------
            if (vm.Format_CutStart_IsEnabled == false &&
                vm.Format_CutEnd_IsEnabled == false)
            {
                // Start
                vm.Format_CutStart_Hours_Text = "00";
                vm.Format_CutStart_Minutes_Text = "00";
                vm.Format_CutStart_Seconds_Text = "00";
                vm.Format_CutStart_Milliseconds_Text = "000";
                // End
                vm.Format_CutEnd_Hours_Text = "00";
                vm.Format_CutEnd_Minutes_Text = "00";
                vm.Format_CutEnd_Seconds_Text = "00";
                vm.Format_CutEnd_Milliseconds_Text = "000";
            }
        }


        /// <summary>
        ///     Cut Controls
        /// </summary>
        public static void CutControls(ViewModel vm)
        {
            // Enable Aspect Custom

            // -------------------------
            // No
            // -------------------------
            if (vm.Format_Cut_SelectedItem == "No")
            {
                // Cut
                vm.Format_CutStart_IsEnabled = false;
                vm.Format_CutStart_Hours_IsEnabled = false;
                vm.Format_CutStart_Minutes_IsEnabled = false;
                vm.Format_CutStart_Seconds_IsEnabled = false;
                vm.Format_CutStart_Milliseconds_IsEnabled = false;

                vm.Format_CutEnd_IsEnabled = false;
                vm.Format_CutEnd_Hours_IsEnabled = false;
                vm.Format_CutEnd_Minutes_IsEnabled = false;
                vm.Format_CutEnd_Seconds_IsEnabled = false;
                vm.Format_CutEnd_Milliseconds_IsEnabled = false;

                // Video / Sequence
                if (vm.Format_MediaType_SelectedItem == "Video" ||
                    vm.Format_MediaType_SelectedItem == "Sequence")
                {
                    // Start
                    vm.Format_CutStart_Hours_Text = "00";
                    vm.Format_CutStart_Minutes_Text = "00";
                    vm.Format_CutStart_Seconds_Text = "00";
                    vm.Format_CutStart_Milliseconds_Text = "000";
                    // End
                    vm.Format_CutEnd_Hours_Text = "00";
                    vm.Format_CutEnd_Minutes_Text = "00";
                    vm.Format_CutEnd_Seconds_Text = "00";
                    vm.Format_CutEnd_Milliseconds_Text = "000";
                }
                // Image
                else if (vm.Format_MediaType_SelectedItem == "Image")
                {
                    //vm.Format_CutStart_Text = "00:00:00.000";
                    //vm.Format_CutEnd_Text = string.Empty;

                    // Start
                    vm.Format_CutStart_Hours_Text = "00";
                    vm.Format_CutStart_Minutes_Text = "00";
                    vm.Format_CutStart_Seconds_Text = "00";
                    vm.Format_CutStart_Milliseconds_Text = "000";
                    // End
                    vm.Format_CutEnd_Hours_Text = string.Empty;
                    vm.Format_CutEnd_Minutes_Text = string.Empty;
                    vm.Format_CutEnd_Seconds_Text = string.Empty;
                    vm.Format_CutEnd_Milliseconds_Text = string.Empty;
                }

                // Frames
                vm.Format_FrameStart_IsEnabled = false;
                vm.Format_FrameEnd_IsEnabled = false;

                // Reset Text
                vm.Format_FrameStart_Text = string.Empty;
                vm.Format_FrameEnd_Text = string.Empty;
            }

            // -------------------------
            // Yes
            // -------------------------
            else if (vm.Format_Cut_SelectedItem == "Yes")
            {
                // Frames

                // Only for Video
                if (vm.Format_MediaType_SelectedItem == "Video") 
                {
                    // Time
                    vm.Format_CutStart_IsEnabled = true;
                    vm.Format_CutStart_Hours_IsEnabled = true;
                    vm.Format_CutStart_Minutes_IsEnabled = true;
                    vm.Format_CutStart_Seconds_IsEnabled = true;
                    vm.Format_CutStart_Milliseconds_IsEnabled = true;

                    vm.Format_CutEnd_IsEnabled = true;
                    vm.Format_CutEnd_Hours_IsEnabled = true;
                    vm.Format_CutEnd_Minutes_IsEnabled = true;
                    vm.Format_CutEnd_Seconds_IsEnabled = true;
                    vm.Format_CutEnd_Milliseconds_IsEnabled = true;

                    // Frames
                    vm.Format_FrameStart_IsEnabled = true;
                    vm.Format_FrameEnd_IsEnabled = true;
                }

                // Only for Video
                else if (vm.Format_MediaType_SelectedItem == "Audio")
                {
                    // Time
                    vm.Format_CutStart_IsEnabled = true;
                    vm.Format_CutStart_Hours_IsEnabled = true;
                    vm.Format_CutStart_Minutes_IsEnabled = true;
                    vm.Format_CutStart_Seconds_IsEnabled = true;
                    vm.Format_CutStart_Milliseconds_IsEnabled = true;

                    vm.Format_CutEnd_IsEnabled = true;
                    vm.Format_CutEnd_Hours_IsEnabled = true;
                    vm.Format_CutEnd_Minutes_IsEnabled = true;
                    vm.Format_CutEnd_Seconds_IsEnabled = true;
                    vm.Format_CutEnd_Milliseconds_IsEnabled = true;

                    // Frames
                    vm.Format_FrameStart_IsEnabled = false;
                    vm.Format_FrameEnd_IsEnabled = false;

                    // Text
                    vm.Format_FrameStart_Text = string.Empty;
                    vm.Format_FrameEnd_Text = string.Empty;
                }

                // Only for Video
                else if (vm.Format_MediaType_SelectedItem == "Image")
                {
                    // Time
                    vm.Format_CutStart_IsEnabled = true;
                    vm.Format_CutStart_Hours_IsEnabled = true;
                    vm.Format_CutStart_Minutes_IsEnabled = true;
                    vm.Format_CutStart_Seconds_IsEnabled = true;
                    vm.Format_CutStart_Milliseconds_IsEnabled = true;

                    vm.Format_CutEnd_IsEnabled = false;
                    vm.Format_CutEnd_Hours_IsEnabled = false;
                    vm.Format_CutEnd_Minutes_IsEnabled = false;
                    vm.Format_CutEnd_Seconds_IsEnabled = false;
                    vm.Format_CutEnd_Milliseconds_IsEnabled = false;

                    vm.Format_CutEnd_Hours_Text = string.Empty;
                    vm.Format_CutEnd_Minutes_Text = string.Empty;
                    vm.Format_CutEnd_Seconds_Text = string.Empty;
                    vm.Format_CutEnd_Milliseconds_Text = string.Empty;

                    // Frames
                    vm.Format_FrameStart_IsEnabled = true;
                    vm.Format_FrameEnd_IsEnabled = false;
                    vm.Format_FrameEnd_Text = string.Empty;
                }

                // Only for Video
                else if (vm.Format_MediaType_SelectedItem == "Sequence")
                {
                    // Time
                    vm.Format_CutStart_IsEnabled = true;
                    vm.Format_CutStart_Hours_IsEnabled = true;
                    vm.Format_CutStart_Minutes_IsEnabled = true;
                    vm.Format_CutStart_Seconds_IsEnabled = true;
                    vm.Format_CutStart_Milliseconds_IsEnabled = true;

                    vm.Format_CutEnd_IsEnabled = true;
                    vm.Format_CutEnd_Hours_IsEnabled = true;
                    vm.Format_CutEnd_Minutes_IsEnabled = true;
                    vm.Format_CutEnd_Seconds_IsEnabled = true;
                    vm.Format_CutEnd_Milliseconds_IsEnabled = true;

                    // Frames
                    vm.Format_FrameStart_IsEnabled = true;
                    vm.Format_FrameEnd_IsEnabled = true;
                }
            }
        } 


    }
}
