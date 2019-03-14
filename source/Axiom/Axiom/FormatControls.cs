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

using System.Collections.Generic;
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

        // Load in Intialize Component
        //
        // MediaType
        //public static List<string> MediaTypeItemSource = new List<string>()
        //{
        //    "Video",
        //    "Audio",
        //    "Image",
        //    "Sequence"
        //};


        /// <summary>
        ///     Set Controls
        /// </summary>
        public static void SetControls(ViewModel vm, string container)
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
            if (container == "webm")
            {
                // Media Type
                vm.MediaType_Items = Containers.WebM.media;
                vm.MediaType_SelectedItem = "Video";
                vm.MediaType_IsEnabled = false;

                // Video Codec
                vm.VideoCodec_Items = Containers.WebM.video;
                vm.VideoCodec_SelectedItem = "VP8";

                // Audio Codec
                vm.AudioCodec_Items = Containers.WebM.audio;
                vm.AudioCodec_SelectedItem = "Vorbis";

                // Subtitle Codec
                vm.SubtitleCodec_Items = Containers.WebM.subtitle;
                vm.SubtitleCodec_SelectedItem = "None";
            }

            // -------------------------
            // mp4
            // -------------------------
            else if (container == "mp4")
            {
                // Media Type
                vm.MediaType_Items = Containers.MP4.media;
                vm.MediaType_SelectedItem = "Video";
                vm.MediaType_IsEnabled = false;

                // Video Codec
                vm.VideoCodec_Items = Containers.MP4.video;
                vm.VideoCodec_SelectedItem = "x264";

                // Audio Codec
                vm.AudioCodec_Items = Containers.MP4.audio;
                vm.AudioCodec_SelectedItem = "AAC";

                // Subtitle Codec
                vm.SubtitleCodec_Items = Containers.MP4.subtitle;
                vm.SubtitleCodec_SelectedItem = "MOV Text";
            }

            // -------------------------
            // mkv
            // -------------------------
            else if (container == "mkv")
            {
                // Media Type
                vm.MediaType_Items = Containers.MKV.media;
                vm.MediaType_SelectedItem = "Video";
                vm.MediaType_IsEnabled = false;

                // Video Codec
                vm.VideoCodec_Items = Containers.MKV.video;
                vm.VideoCodec_SelectedItem = "x264";

                // Audio Codec
                vm.AudioCodec_Items = Containers.MKV.audio;
                vm.AudioCodec_SelectedItem = "AC3";

                // Subtitle Codec
                vm.SubtitleCodec_Items = Containers.MKV.subtitle;
                vm.SubtitleCodec_SelectedItem = "SSA";
            }

            // -------------------------
            // m2v
            // -------------------------
            else if (container == "m2v")
            {
                // Media Type
                vm.MediaType_Items = Containers.M2V.media;
                vm.MediaType_SelectedItem = "Video";
                vm.MediaType_IsEnabled = false;

                // Video Codec
                vm.VideoCodec_Items = Containers.M2V.video;
                vm.VideoCodec_SelectedItem = "MPEG-2";

                // Audio Codec
                vm.AudioCodec_Items = Containers.M2V.audio;
                vm.AudioCodec_SelectedItem = "None";

                // Subtitle Codec
                vm.SubtitleCodec_Items = Containers.M2V.subtitle;
                vm.SubtitleCodec_SelectedItem = "None";
            }

            // -------------------------
            // mpg
            // -------------------------
            else if (container == "mpg")
            {
                // Media Type
                vm.MediaType_Items = Containers.MPG.media;
                vm.MediaType_SelectedItem = "Video";
                vm.MediaType_IsEnabled = false;

                // Video Codec
                vm.VideoCodec_Items = Containers.MPG.video;
                vm.VideoCodec_SelectedItem = "MPEG-2";

                // Audio Codec
                vm.AudioCodec_Items = Containers.MPG.audio;
                vm.AudioCodec_SelectedItem = "AC3";

                // Subtitle Codec
                vm.SubtitleCodec_Items = Containers.MPG.subtitle;
                vm.SubtitleCodec_SelectedItem = "SRT";
            }

            // -------------------------
            // avi
            // -------------------------
            else if (container == "avi")
            {
                // Media Type
                vm.MediaType_Items = Containers.AVI.media;
                vm.MediaType_SelectedItem = "Video";
                vm.MediaType_IsEnabled = false;

                // Video Codec
                vm.VideoCodec_Items = Containers.AVI.video;
                vm.VideoCodec_SelectedItem = "MPEG-4";

                // Audio Codec
                vm.AudioCodec_Items = Containers.AVI.audio;
                vm.AudioCodec_SelectedItem = "AC3";

                // Subtitle Codec
                vm.SubtitleCodec_Items = Containers.AVI.subtitle;
                vm.SubtitleCodec_SelectedItem = "SRT";
            }

            // -------------------------
            // ogv
            // -------------------------
            else if (container == "ogv")
            {
                // Media Type
                vm.MediaType_Items = Containers.OGV.media;
                vm.MediaType_SelectedItem = "Video";
                vm.MediaType_IsEnabled = false;

                // Video Codec
                vm.VideoCodec_Items = Containers.OGV.video;
                vm.VideoCodec_SelectedItem = "Theora";

                // Audio Codec
                vm.AudioCodec_Items = Containers.OGV.audio;
                vm.AudioCodec_SelectedItem = "Opus";

                // Subtitle Codec
                vm.SubtitleCodec_Items = Containers.OGV.subtitle;
                vm.SubtitleCodec_SelectedItem = "None";
            }


            // --------------------------------------------------
            // Audio
            // --------------------------------------------------
            // -------------------------
            // mp3
            // -------------------------
            else if (container == "mp3")
            {
                // Media Type
                vm.MediaType_Items = Containers.LAME.media;
                vm.MediaType_SelectedItem = "Audio";
                vm.MediaType_IsEnabled = false;

                // Video Codec
                vm.VideoCodec_Items = Containers.LAME.video;
                vm.VideoCodec_SelectedItem = "None";

                // Audio Codec
                vm.AudioCodec_Items = Containers.LAME.audio;
                vm.AudioCodec_SelectedItem = "LAME";

                // Subtitle Codec
                vm.SubtitleCodec_Items = Containers.LAME.subtitle;
                vm.SubtitleCodec_SelectedItem = "None";
            }

            // -------------------------
            // m4a
            // -------------------------
            else if (container == "m4a")
            {
                // Media Type
                vm.MediaType_Items = Containers.M4A.media;
                vm.MediaType_SelectedItem = "Audio";
                vm.MediaType_IsEnabled = false;

                // Video Codec
                vm.VideoCodec_Items = Containers.M4A.video;
                vm.VideoCodec_SelectedItem = "None";

                // Audio Codec
                vm.AudioCodec_Items = Containers.M4A.audio;
                vm.AudioCodec_SelectedItem = "AAC";

                // Subtitle Codec
                vm.SubtitleCodec_Items = Containers.M4A.subtitle;
                vm.SubtitleCodec_SelectedItem = "None";
            }

            // -------------------------
            // ogg
            // -------------------------
            else if (container == "ogg")
            {
                // Media Type
                vm.MediaType_Items = Containers.OGG.media;
                vm.MediaType_SelectedItem = "Audio";
                vm.MediaType_IsEnabled = false;

                // Video Codec
                vm.VideoCodec_Items = Containers.OGG.video;
                vm.VideoCodec_SelectedItem = "None";

                // Audio Codec
                vm.AudioCodec_Items = Containers.OGG.audio;
                vm.AudioCodec_SelectedItem = "Opus";

                // Subtitle Codec
                vm.SubtitleCodec_Items = Containers.OGG.subtitle;
                vm.SubtitleCodec_SelectedItem = "None";
            }

            // -------------------------
            // flac
            // -------------------------
            else if (container == "flac")
            {
                // Media Type
                vm.MediaType_Items = Containers.FLAC.media;
                vm.MediaType_SelectedItem = "Audio";
                vm.MediaType_IsEnabled = false;

                // Video Codec
                vm.VideoCodec_Items = Containers.FLAC.video;
                vm.VideoCodec_SelectedItem = "None";

                // Audio Codec
                vm.AudioCodec_Items = Containers.FLAC.audio;
                vm.AudioCodec_SelectedItem = "FLAC";

                // Subtitle Codec
                vm.SubtitleCodec_Items = Containers.FLAC.subtitle;
                vm.SubtitleCodec_SelectedItem = "None";
            }

            // -------------------------
            // wav
            // -------------------------
            else if (container == "wav")
            {
                // Media Type
                vm.MediaType_Items = Containers.WAV.media;
                vm.MediaType_SelectedItem = "Audio";
                vm.MediaType_IsEnabled = false;

                // Video Codec
                vm.VideoCodec_Items = Containers.WAV.video;
                vm.VideoCodec_SelectedItem = "None";

                // Audio Codec
                vm.AudioCodec_Items = Containers.WAV.audio;
                vm.AudioCodec_SelectedItem = "PCM";

                // Subtitle Codec
                vm.SubtitleCodec_Items = Containers.WAV.subtitle;
                vm.SubtitleCodec_SelectedItem = "None";
            }


            // --------------------------------------------------
            // Image
            // --------------------------------------------------
            // -------------------------
            // jpg
            // -------------------------
            else if (container == "jpg")
            {
                // Media Type
                vm.MediaType_Items = Containers.JPG.media;
                vm.MediaType_SelectedItem = "Image";
                vm.MediaType_IsEnabled = true;

                // Video Codec
                vm.VideoCodec_Items = Containers.JPG.video;
                vm.VideoCodec_SelectedItem = "JPEG";

                // Audio Codec
                vm.AudioCodec_Items = Containers.JPG.audio;
                vm.AudioCodec_SelectedItem = "None";

                // Subtitle Codec
                vm.SubtitleCodec_Items = Containers.JPG.subtitle;
                vm.SubtitleCodec_SelectedItem = "None";
            }

            // -------------------------
            // png
            // -------------------------
            else if (container == "png")
            {
                // Media Type
                vm.MediaType_Items = Containers.JPG.media;
                vm.MediaType_SelectedItem = "Image";
                vm.MediaType_IsEnabled = true;

                // Video Codec
                vm.VideoCodec_Items = Containers.PNG.video;
                vm.VideoCodec_SelectedItem = "PNG";

                // Audio Codec
                vm.AudioCodec_Items = Containers.PNG.audio;
                vm.AudioCodec_SelectedItem = "None";

                // Subtitle Codec
                vm.SubtitleCodec_Items = Containers.PNG.subtitle;
                vm.SubtitleCodec_SelectedItem = "None";
            }

            // -------------------------
            // webp
            // -------------------------
            else if (container == "webp")
            {
                // Media Type
                vm.MediaType_Items = Containers.JPG.media;
                vm.MediaType_SelectedItem = "Image";
                vm.MediaType_IsEnabled = true;

                // Video Codec
                vm.VideoCodec_Items = Containers.WebP.video;
                vm.VideoCodec_SelectedItem = "WebP";

                // Audio Codec
                vm.AudioCodec_Items = Containers.WebP.audio;
                vm.AudioCodec_SelectedItem = "None";

                // Subtitle Codec
                vm.SubtitleCodec_Items = Containers.WebP.subtitle;
                vm.SubtitleCodec_SelectedItem = "None";
            }
        }



        /// <summary>
        ///     Get Output Extension
        /// </summary>
        public static void OutputFormatExt(ViewModel vm)
        {
            // Output Extension is Format ComboBox's Selected Item (eg mp4)
            MainWindow.outputExt = "." + vm.Container_SelectedItem;
        }



        /// <summary>
        ///     MediaType Controls
        /// </summary>
        public static void MediaType(ViewModel vm)
        {
            // -------------------------
            // Video MediaType
            // -------------------------
            // Enable Frame Textbox for Image Screenshot
            if (vm.MediaType_SelectedItem == "Video")
            {
                // -------------------------
                // Video
                // -------------------------
                // Codec
                vm.VideoCodec_IsEnabled = true;

                // Size
                vm.Size_IsEnabled = true;

                // Scaling
                vm.Scaling_IsEnabled = true;
                vm.Scaling_SelectedItem = "default";

                // Cut
                // Cut Change - If coming back from JPEG or PNG
                if (vm.CutStart_IsEnabled == true && 
                    vm.CutEnd_IsEnabled == false)
                {
                    vm.Cut_SelectedItem = "No";
                }

                // Crop
                vm.Crop_IsEnabled = true;

                // Encode Speed
                vm.VideoEncodeSpeed_IsEnabled = true;

                // -------------------------
                // Audio
                // -------------------------
                // Codec
                vm.AudioCodec_IsEnabled = true;

                // Channel
                vm.AudioChannel_IsEnabled = true;

                // Volume
                vm.Volume_IsEnabled = true;

                // Limiter
                vm.AudioHardLimiter_IsEnabled = true;

                // Audio Stream
                vm.AudioStream_IsEnabled = true;
                vm.AudioStream_SelectedItem = "all";


                // -------------------------
                // Subtitle
                // -------------------------
                // Codec
                vm.SubtitleCodec_IsEnabled = true;
            }

            // -------------------------
            // Audio MediaType
            // -------------------------
            else if (vm.MediaType_SelectedItem == "Audio")
            {
                // -------------------------
                // Video
                // -------------------------
                // Codec
                vm.VideoCodec_IsEnabled = false;

                // Size
                vm.Size_SelectedItem = "Source";
                vm.Size_IsEnabled = false;

                // Scaling
                vm.Scaling_SelectedItem = "default";
                vm.Scaling_IsEnabled = false;

                // Cut
                // Cut Change - If coming back from JPEG or PNG
                if (vm.CutStart_IsEnabled == true && vm.CutEnd_IsEnabled == false)
                {
                    vm.Cut_SelectedItem = "No";
                }

                // Frame
                vm.FrameEnd_IsEnabled = false;
                vm.FrameEnd_Text = "";

                // Crop
                vm.Crop_IsEnabled = false;

                // FPS
                vm.FPS_SelectedItem = "auto";
                vm.FPS_IsEnabled = false;

                // Encode Speed
                vm.VideoEncodeSpeed_IsEnabled = false;


                // -------------------------
                // Audio
                // -------------------------
                // Codec
                vm.AudioCodec_IsEnabled = true;

                // Channel
                vm.AudioChannel_IsEnabled = true;

                // Audio Stream
                vm.AudioStream_IsEnabled = true;
                vm.AudioStream_SelectedItem = "1";

                // Sample Rate
                // Controled Through Codec Class

                // Bit Depth
                // Controled Through Codec Class

                // Volume
                vm.Volume_IsEnabled = true;

                // Limiter
                vm.AudioHardLimiter_IsEnabled = true;


                // -------------------------
                // Subtitle
                // -------------------------
                // Codec
                vm.SubtitleCodec_IsEnabled = false;
            }

            // -------------------------
            // Image MediaType
            // -------------------------
            else if (vm.MediaType_SelectedItem == "Image")
            {
                // -------------------------
                // Video
                // -------------------------
                // Codec
                vm.VideoCodec_IsEnabled = true;

                //Size
                vm.Size_IsEnabled = true;

                // Scaling
                vm.Scaling_SelectedItem = "default";
                vm.Scaling_IsEnabled = true;

                // Cut
                // Enable Cut Start Time for Frame Selection
                vm.Cut_SelectedItem = "Yes";
                vm.CutStart_IsEnabled = true;
                vm.CutEnd_Text = "00:00:00.000";
                vm.CutEnd_IsEnabled = false;

                // Frame
                vm.FrameEnd_IsEnabled = false;
                vm.FrameEnd_Text = "";

                // Crop
                vm.Crop_IsEnabled = true;

                // Fps
                vm.FPS_SelectedItem = "auto";
                vm.FPS_IsEnabled = false;

                // Encode Speed
                vm.VideoEncodeSpeed_IsEnabled = false;

                // -------------------------
                // Audio
                // -------------------------
                // Codec
                vm.AudioCodec_IsEnabled = false;

                // Quality
                vm.AudioQuality_SelectedItem = "None";
                vm.AudioQuality_IsEnabled = false;

                // Channel
                vm.AudioChannel_SelectedItem = "Source";
                vm.AudioChannel_IsEnabled = false;

                // Audio Stream
                vm.AudioStream_SelectedItem = "none";
                vm.AudioStream_IsEnabled = false;

                // Sample Rate
                vm.AudioSampleRate_SelectedItem = "auto";
                vm.AudioSampleRate_IsEnabled = false;

                // Bit Depth
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.AudioBitDepth_IsEnabled = false;

                // Volume
                vm.Volume_IsEnabled = false;

                // Limiter
                vm.AudioHardLimiter_IsEnabled = false;
                vm.AudioHardLimiter_Value = 1;


                // -------------------------
                // Subtitle
                // -------------------------
                // Codec
                vm.SubtitleCodec_IsEnabled = false;
            }

            // -------------------------
            // Sequence MediaType
            // -------------------------
            else if (vm.MediaType_SelectedItem == "Sequence")
            {
                // -------------------------
                // Video
                // -------------------------
                // Codec 
                vm.VideoCodec_IsEnabled = true;

                //Size
                vm.Size_IsEnabled = true;

                // Scaling
                vm.Scaling_SelectedItem = "default";
                vm.Scaling_IsEnabled = true;

                // Cut
                // Enable Cut for Time Selection
                vm.Cut_SelectedItem = "No";

                // Crop
                vm.Crop_IsEnabled = true;

                // Speed
                vm.VideoEncodeSpeed_IsEnabled = false;

                // FPS
                vm.FPS_IsEnabled = true;


                // -------------------------
                // Audio
                // -------------------------
                // Codec
                vm.AudioCodec_IsEnabled = false;

                // Quality
                vm.AudioQuality_SelectedItem = "Auto";
                vm.AudioQuality_IsEnabled = false;

                // Channel
                vm.AudioChannel_SelectedItem = "Source";
                vm.AudioChannel_IsEnabled = false;

                // Audio Stream
                vm.AudioStream_SelectedItem = "none";
                vm.AudioStream_IsEnabled = false;

                // Sample Rate
                vm.AudioSampleRate_SelectedItem = "auto";
                vm.AudioSampleRate_IsEnabled = false;

                // Bit Depth
                vm.AudioBitDepth_SelectedItem = "auto";
                vm.AudioBitDepth_IsEnabled = false;

                // Volume
                vm.Volume_IsEnabled = false;

                // Limiter
                vm.AudioHardLimiter_IsEnabled = false;
                vm.AudioHardLimiter_Value = 1;


                // -------------------------
                // Subtitle
                // -------------------------
                // Codec
                vm.SubtitleCodec_IsEnabled = false;
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
            if (vm.Cut_SelectedItem == "No")
            {
                // Time
                vm.CutStart_IsEnabled = false;
                vm.CutEnd_IsEnabled = false;

                vm.CutStart_Text = "00:00:00.000";
                vm.CutEnd_Text = "00:00:00.000";

                // Frames
                vm.FrameStart_IsEnabled = false;
                vm.FrameEnd_IsEnabled = false;

                // Reset Text
                vm.FrameStart_Text = "Frame";
                vm.FrameEnd_Text = "Range";
            }

            // -------------------------
            // Yes
            // -------------------------
            else if (vm.Cut_SelectedItem == "Yes")
            {
                // Frames

                // Only for Video
                if (vm.MediaType_SelectedItem == "Video") 
                {
                    // Time
                    vm.CutStart_IsEnabled = true;
                    vm.CutEnd_IsEnabled = true;

                    // Frames
                    vm.FrameStart_IsEnabled = true;
                    vm.FrameEnd_IsEnabled = true;
                }

                // Only for Video
                else if (vm.MediaType_SelectedItem == "Audio")
                {
                    // Time
                    vm.CutStart_IsEnabled = true;
                    vm.CutEnd_IsEnabled = true;

                    // Frames
                    vm.FrameStart_IsEnabled = false;
                    vm.FrameEnd_IsEnabled = false;

                    // Text
                    vm.FrameStart_Text = "Frame";
                    vm.FrameEnd_Text = "Range";
                }

                // Only for Video
                else if (vm.MediaType_SelectedItem == "Image")
                {
                    // Time
                    vm.CutStart_IsEnabled = true;
                    vm.CutEnd_IsEnabled = false;
                    vm.CutEnd_Text = "00:00:00.000"; //important

                    // Frames
                    vm.FrameStart_IsEnabled = true;
                    vm.FrameEnd_IsEnabled = false;
                    vm.FrameEnd_Text = string.Empty; //important
                }

                // Only for Video
                else if (vm.MediaType_SelectedItem == "Sequence")
                {
                    // Time
                    vm.CutStart_IsEnabled = true;
                    vm.CutEnd_IsEnabled = true;

                    // Frames
                    vm.FrameStart_IsEnabled = true;
                    vm.FrameEnd_IsEnabled = true;
                }
            }
        } 


    }
}
