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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    class Profiles
    {
        /// <summary>
        ///    Global Variables
        /// </summary>
        public static string presetsDir = MainWindow.appDataLocalDir + @"Axiom UI\presets\"; // Custom User ini presets


        /// <summary>
        ///    Scan PC Custom Presets
        /// </summary>
        public static List<string> customPresetPathsList = new List<string>();
        
        public static void LoadCustomPresets()
        {
            // -------------------------
            // User Custom Preset Full Path
            // -------------------------
            if (Directory.Exists(ConfigureView.vm.CustomPresetsPath_Text))
            {
                // Get Custom .ini Preset Paths
                customPresetPathsList = Directory.GetFiles(ConfigureView.vm.CustomPresetsPath_Text, "*.ini")
                                                             .Select(Path.GetFullPath)
                                                             .OrderByDescending(x => x)
                                                             .ToList();

                // Preset Names Only List
                List<string> presetNamesList = MainView.vm.Preset_Items.Select(item => item.Name).ToList();

                // Add .ini Preset Names to Presets List
                for (var i = 0; i < customPresetPathsList.Count; i++)
                {
                    // Get name from .ini
                    string presetName = Path.GetFileNameWithoutExtension(customPresetPathsList[i]);

                    // Prevent adding duplicate
                    // Ignore Desktop.ini
                    // Ignore ntuser.ini
                    if (!presetNamesList.Contains(presetName) &&
                        !string.Equals(presetName, "desktop", StringComparison.CurrentCultureIgnoreCase) &&
                        !string.Equals(presetName, "ntuser", StringComparison.CurrentCultureIgnoreCase)
                        )
                    {
                        MainView.vm.Preset_Items.Insert(3, new MainView.Preset() { Name = presetName, Category = false, Type = "Custom" });
                    }

                }


                // -------------------------
                // Cleanup Missing/Deleted Presets
                // -------------------------
                // Refresh the List
                presetNamesList = MainView.vm.Preset_Items.Select(item => item.Name).ToList();

                // .ini file is missing in \Axiom UI\presets directory
                try
                {
                    for (int i = MainView.vm.Preset_Items.Count - 1; i >= 0; --i)
                    {
                        // If .ini File List does not contain Preset Name
                        if (!customPresetPathsList.Contains(Profiles.presetsDir + presetNamesList[i] + ".ini"))
                        //if (!File.Exists(Profiles.presetsDir + presetNamesList[i] + ".ini"))
                        {
                            // Remove from Presets List if Type is Custom
                            if (MainView.vm.Preset_Items.FirstOrDefault(item => item.Name == presetNamesList[i])?.Type == "Custom")
                            {
                                MainView.vm.Preset_Items.RemoveAt(i);
                            }
                        }
                    }
                }
                catch
                {

                }
            }

            // -------------------------
            // Preset path does not exist
            // -------------------------
            else
            {
                // Load Default
                //MainView.vm.CustomPresetPath_Text = presetsDir;
            }
        }



        /// <summary>
        ///    Import Preset
        /// </summary>
        public static void ImportPreset(string profile)
        {
            // If control failed to imported, add to list
            List<string> listFailedImports = new List<string>();

            // Start INI File Read
            Configure.INIFile inif = null;

            // -------------------------
            // Check if Preset ini file exists
            // -------------------------
            if (File.Exists(profile))
            {
                inif = new Configure.INIFile(profile);

                // --------------------------------------------------
                // Main Window
                // --------------------------------------------------
                // Batch
                bool mainwindow_Batch_IsChecked;
                bool.TryParse(inif.Read("Main Window", "Batch_IsChecked").ToLower(), out mainwindow_Batch_IsChecked);
                MainView.vm.Batch_IsChecked = mainwindow_Batch_IsChecked;

                // Batch Extension
                MainView.vm.BatchExtension_Text = inif.Read("Main Window", "BatchExtension_Text");


                // --------------------------------------------------
                // Format
                // --------------------------------------------------
                // Container
                string container = inif.Read("Format", "Container_SelectedItem");
                List<string> containerNames = FormatView.vm.Format_Container_Items.Select(item => item.Name).ToList();

                if (containerNames.Contains(container))
                    FormatView.vm.Format_Container_SelectedItem = container;
                else
                    listFailedImports.Add("Format: Container");

                // MediaType
                string mediaType = inif.Read("Format", "MediaType_SelectedItem");
                if (FormatView.vm.Format_MediaType_Items.Contains(mediaType))
                    FormatView.vm.Format_MediaType_SelectedItem = mediaType;
                else
                    listFailedImports.Add("Format: Media Type");

                // Cut
                string cut = inif.Read("Format", "Cut_SelectedItem");
                if (FormatView.vm.Format_Cut_Items.Contains(cut))
                    FormatView.vm.Format_Cut_SelectedItem = cut;
                else
                    listFailedImports.Add("Format: Cut");

                // Cut Start
                FormatView.vm.Format_CutStart_Hours_Text = inif.Read("Format", "CutStart_Hours_Text");
                FormatView.vm.Format_CutStart_Minutes_Text = inif.Read("Format", "CutStart_Minutes_Text");
                FormatView.vm.Format_CutStart_Seconds_Text = inif.Read("Format", "CutStart_Seconds_Text");
                FormatView.vm.Format_CutStart_Milliseconds_Text = inif.Read("Format", "CutStart_Milliseconds_Text");
                // Cut End
                FormatView.vm.Format_CutEnd_Hours_Text = inif.Read("Format", "CutEnd_Hours_Text");
                FormatView.vm.Format_CutEnd_Minutes_Text = inif.Read("Format", "CutEnd_Minutes_Text");
                FormatView.vm.Format_CutEnd_Seconds_Text = inif.Read("Format", "CutEnd_Seconds_Text");
                FormatView.vm.Format_CutEnd_Milliseconds_Text = inif.Read("Format", "CutEnd_Milliseconds_Text");

                // Cut Frames
                FormatView.vm.Format_FrameStart_Text = inif.Read("Format", "FrameStart_Text");
                FormatView.vm.Format_FrameEnd_Text = inif.Read("Format", "FrameEnd_Text");

                // YouTube Download
                FormatView.vm.Format_YouTube_SelectedItem = inif.Read("Format", "YouTube_SelectedItem");
                FormatView.vm.Format_YouTube_Quality_SelectedItem = inif.Read("Format", "YouTube_Quality_SelectedItem");


                // --------------------------------------------------
                // Video
                // --------------------------------------------------
                // Codec
                string videoCodec = inif.Read("Video", "Codec_SelectedItem");

                if (VideoView.vm.Video_Codec_Items.Contains(videoCodec))
                    VideoView.vm.Video_Codec_SelectedItem = videoCodec;
                else
                    listFailedImports.Add("Video: Codec");

                // Encode Speed
                string videoEncodeSpeed = inif.Read("Video", "EncodeSpeed_SelectedItem");
                List<string> videoEncodeSpeedNames = VideoView.vm.Video_EncodeSpeed_Items.Select(item => item.Name).ToList();

                if (videoEncodeSpeedNames.Contains(videoEncodeSpeed))
                    VideoView.vm.Video_EncodeSpeed_SelectedItem = videoEncodeSpeed;
                else
                    listFailedImports.Add("Video: EncodeSpeed");

                // HWAccel
                string hwAccel = inif.Read("Video", "HWAccel_SelectedItem");
                if (VideoView.vm.Video_HWAccel_Items.Contains(hwAccel))
                    VideoView.vm.Video_HWAccel_SelectedItem = hwAccel;
                else
                    listFailedImports.Add("Video: HWAccel");

                // Quality
                string videoQuality = inif.Read("Video", "Quality_SelectedItem");
                List<string> videoQualityNames = VideoView.vm.Video_Quality_Items.Select(item => item.Name).ToList();

                if (videoQualityNames.Contains(videoQuality))
                    VideoView.vm.Video_Quality_SelectedItem = videoQuality;
                else
                    listFailedImports.Add("Video: Quality");

                // Pass
                string videoPass = inif.Read("Video", "Pass_SelectedItem");

                if (VideoView.vm.Video_Pass_Items.Contains(videoPass))
                    VideoView.vm.Video_Pass_SelectedItem = videoPass;
                else
                    listFailedImports.Add("Video: Pass");

                // CRF
                VideoView.vm.Video_CRF_Text = inif.Read("Video", "CRF_Text");

                // Bit Rate
                VideoView.vm.Video_BitRate_Text = inif.Read("Video", "BitRate_Text");

                // Min Rate
                VideoView.vm.Video_MinRate_Text = inif.Read("Video", "MinRate_Text");

                // Max Rate
                VideoView.vm.Video_MaxRate_Text = inif.Read("Video", "MaxRate_Text");

                // Max Rate
                VideoView.vm.Video_BufSize_Text = inif.Read("Video", "BufSize_Text");

                // Video VBR
                bool video_VBR_IsChecked;
                bool.TryParse(inif.Read("Video", "VBR_IsChecked").ToLower(), out video_VBR_IsChecked);
                VideoView.vm.Video_VBR_IsChecked = video_VBR_IsChecked;

                // Pixel Format
                string videoPixelFormat = inif.Read("Video", "PixelFormat_SelectedItem");

                if (VideoView.vm.Video_PixelFormat_Items.Contains(videoPixelFormat))
                    VideoView.vm.Video_PixelFormat_SelectedItem = videoPixelFormat;
                else
                    listFailedImports.Add("Video: PixelFormat");

                // FPS
                string videoFPS = inif.Read("Video", "FPS_SelectedItem");

                if (VideoView.vm.Video_FPS_Items.Contains(videoFPS))
                    VideoView.vm.Video_FPS_SelectedItem = videoFPS;
                else
                    listFailedImports.Add("Video: FPS");

                // Speed
                string videoSpeed = inif.Read("Video", "Speed_SelectedItem");

                if (VideoView.vm.Video_Speed_Items.Contains(videoSpeed))
                    VideoView.vm.Video_Speed_SelectedItem = videoSpeed;
                else
                    listFailedImports.Add("Video: Speed");

                // Optimize
                string videoOptimize = inif.Read("Video", "Optimize_SelectedItem");
                List<string> videoOptimizeNames = VideoView.vm.Video_Optimize_Items.Select(item => item.Name).ToList();

                if (videoOptimizeNames.Contains(videoOptimize))
                    VideoView.vm.Video_Optimize_SelectedItem = videoOptimize;
                else
                    listFailedImports.Add("Video: Optimize");

                // Optimize Tune
                string videoOptimize_Tune = inif.Read("Video", "Video_Optimize_Tune_SelectedItem");

                if (VideoView.vm.Video_Optimize_Tune_Items.Contains(videoOptimize_Tune))
                    VideoView.vm.Video_Video_Optimize_Tune_SelectedItem = videoOptimize_Tune;
                else
                    listFailedImports.Add("Video: Optimize Tune");

                // Optimize Profile
                string videoOptimize_Profile = inif.Read("Video", "Video_Optimize_Profile_SelectedItem");

                if (VideoView.vm.Video_Optimize_Profile_Items.Contains(videoOptimize_Profile))
                    VideoView.vm.Video_Video_Optimize_Profile_SelectedItem = videoOptimize_Profile;
                else
                    listFailedImports.Add("Video: Optimize Profile");

                // Optimize Level
                string videoOptimize_Level = inif.Read("Video", "Optimize_Level_SelectedItem");

                if (VideoView.vm.Video_Optimize_Level_Items.Contains(videoOptimize_Level))
                    VideoView.vm.Video_Optimize_Level_SelectedItem = videoOptimize_Level;
                else
                    listFailedImports.Add("Video: Optimize Level");

                // Scale
                string videoScale = inif.Read("Video", "Scale_SelectedItem");

                if (VideoView.vm.Video_Scale_Items.Contains(videoScale))
                    VideoView.vm.Video_Scale_SelectedItem = videoScale;
                else
                    listFailedImports.Add("Video: Scale");

                // Width
                VideoView.vm.Video_Width_Text = inif.Read("Video", "Width_Text");
                // Height
                VideoView.vm.Video_Height_Text = inif.Read("Video", "Height_Text");

                // Screen Format
                string videoScreenFormat = inif.Read("Video", "ScreenFormat_SelectedItem");

                if (VideoView.vm.Video_ScreenFormat_Items.Contains(videoScreenFormat))
                    VideoView.vm.Video_ScreenFormat_SelectedItem = videoScreenFormat;
                else
                    listFailedImports.Add("Video: Screen Format");

                // Aspect Ratio
                string videoAspectRatio = inif.Read("Video", "AspectRatio_SelectedItem");

                if (VideoView.vm.Video_AspectRatio_Items.Contains(videoAspectRatio))
                    VideoView.vm.Video_AspectRatio_SelectedItem = videoAspectRatio;
                else
                    listFailedImports.Add("Video: Aspect Ratio");

                // Scaling Algorithm
                string videoScalingAlgorithm = inif.Read("Video", "ScalingAlgorithm_SelectedItem");

                if (VideoView.vm.Video_ScalingAlgorithm_Items.Contains(videoScalingAlgorithm))
                    VideoView.vm.Video_ScalingAlgorithm_SelectedItem = videoScalingAlgorithm;
                else
                    listFailedImports.Add("Video: Scaling Algorithm");

                // Crop X
                VideoView.vm.Video_Crop_X_Text = inif.Read("Video", "Crop_X_Text");
                // Crop Y
                VideoView.vm.Video_Crop_Y_Text = inif.Read("Video", "Crop_Y_Text");
                // Crop Width
                VideoView.vm.Video_Crop_Width_Text = inif.Read("Video", "Crop_Width_Text");
                // Crop Height
                VideoView.vm.Video_Crop_Height_Text = inif.Read("Video", "Crop_Height_Text");
                // Crop Clear
                VideoView.vm.Video_CropClear_Text = inif.Read("Video", "CropClear_Text");

                // --------------------------------------------------
                // Audio
                // --------------------------------------------------
                // Codec
                string audioCodec = inif.Read("Audio", "Codec_SelectedItem");

                if (AudioView.vm.Audio_Codec_Items.Contains(audioCodec))
                    AudioView.vm.Audio_Codec_SelectedItem = audioCodec;
                else
                    listFailedImports.Add("Audio: Codec");

                // Stream
                string audioStream = inif.Read("Audio", "Stream_SelectedItem");

                if (AudioView.vm.Audio_Stream_Items.Contains(audioStream))
                    AudioView.vm.Audio_Stream_SelectedItem = audioStream;
                else
                    listFailedImports.Add("Audio: Stream");

                // Channel
                string audioChannel = inif.Read("Audio", "Channel_SelectedItem");

                if (AudioView.vm.Audio_Channel_Items.Contains(audioChannel))
                    AudioView.vm.Audio_Channel_SelectedItem = audioChannel;
                else
                    listFailedImports.Add("Audio: Channel");

                // Quality
                string audioQuality = inif.Read("Audio", "Quality_SelectedItem");
                List<string> audioQualityNames = AudioView.vm.Audio_Quality_Items.Select(item => item.Name).ToList();

                if (audioQualityNames.Contains(audioQuality))
                    AudioView.vm.Audio_Quality_SelectedItem = audioQuality;
                else
                    listFailedImports.Add("Audio: Quality");

                // Audio VBR
                bool audio_VBR_IsChecked;
                bool.TryParse(inif.Read("Audio", "VBR_IsChecked").ToLower(), out audio_VBR_IsChecked);
                AudioView.vm.Audio_VBR_IsChecked = audio_VBR_IsChecked;

                // Bit Rate
                AudioView.vm.Audio_BitRate_Text = inif.Read("Audio", "BitRate_Text");

                // Compression Level
                string audioCompressionLevel = inif.Read("Audio", "CompressionLevel_SelectedItem");

                if (AudioView.vm.Audio_CompressionLevel_Items.Contains(audioCompressionLevel))
                    AudioView.vm.Audio_CompressionLevel_SelectedItem = audioCompressionLevel;
                else
                    listFailedImports.Add("Audio: Compression Level");

                // Sample Rate
                string audioSampleRate = inif.Read("Audio", "SampleRate_SelectedItem");
                List<string> audioSampleRateNames = AudioView.vm.Audio_SampleRate_Items.Select(item => item.Name).ToList();

                if (audioSampleRateNames.Contains(audioSampleRate))
                    AudioView.vm.Audio_SampleRate_SelectedItem = audioSampleRate;
                else
                    listFailedImports.Add("Audio: Sample Rate");

                // Bit Depth
                string audioBitDepth = inif.Read("Audio", "BitDepth_SelectedItem");
                List<string> audioBitDepthNames = AudioView.vm.Audio_BitDepth_Items.Select(item => item.Name).ToList();

                if (audioBitDepthNames.Contains(audioBitDepth))
                    AudioView.vm.Audio_BitDepth_SelectedItem = audioBitDepth;
                else
                    listFailedImports.Add("Audio: Bit Depth");

                // Volume
                AudioView.vm.Audio_Volume_Text = inif.Read("Audio", "Volume_Text");

                // Hard Limiter
                double audio_HardLimiter_Value;
                double.TryParse(inif.Read("Audio", "HardLimiter_Value"), out audio_HardLimiter_Value);
                AudioView.vm.Audio_HardLimiter_Value = audio_HardLimiter_Value;


                // --------------------------------------------------
                // Subtitle
                // --------------------------------------------------
                // Codec
                string subtitleCodec = inif.Read("Subtitle", "Codec_SelectedItem");

                if (SubtitleView.vm.Subtitle_Codec_Items.Contains(subtitleCodec))
                    SubtitleView.vm.Subtitle_Codec_SelectedItem = subtitleCodec;
                else
                    listFailedImports.Add("Subtitle: Codec");

                // Stream
                string subtitleStream = inif.Read("Subtitle", "Stream_SelectedItem");

                if (SubtitleView.vm.Subtitle_Stream_Items.Contains(subtitleStream))
                    SubtitleView.vm.Subtitle_Stream_SelectedItem = subtitleStream;
                else
                    listFailedImports.Add("Subtitle: Stream");


                // --------------------------------------------------
                // Filter Video
                // --------------------------------------------------
                // Deband
                string filterVideoDeband = inif.Read("Filter Video", "Deband_SelectedItem");

                if (FilterVideoView.vm.FilterVideo_Deband_Items.Contains(filterVideoDeband))
                    FilterVideoView.vm.FilterVideo_Deband_SelectedItem = filterVideoDeband;
                else
                    listFailedImports.Add("Filter Video: Deband");

                // Deshake
                string filterVideoDeshake = inif.Read("Filter Video", "Deshake_SelectedItem");

                if (FilterVideoView.vm.FilterVideo_Deshake_Items.Contains(filterVideoDeshake))
                    FilterVideoView.vm.FilterVideo_Deshake_SelectedItem = filterVideoDeshake;
                else
                    listFailedImports.Add("Filter Video: Deshake");

                // Deflicker
                string filterVideoDeflicker = inif.Read("Filter Video", "Deflicker_SelectedItem");

                if (FilterVideoView.vm.FilterVideo_Deflicker_Items.Contains(filterVideoDeflicker))
                    FilterVideoView.vm.FilterVideo_Deflicker_SelectedItem = filterVideoDeflicker;
                else
                    listFailedImports.Add("Filter Video: Deflicker");

                // Dejudder
                string filterVideoDejudder = inif.Read("Filter Video", "Dejudder_SelectedItem");

                if (FilterVideoView.vm.FilterVideo_Dejudder_Items.Contains(filterVideoDejudder))
                    FilterVideoView.vm.FilterVideo_Dejudder_SelectedItem = filterVideoDejudder;
                else
                    listFailedImports.Add("Filter Video: Dejudder");

                // Denoise
                string filterVideoDenoise = inif.Read("Filter Video", "Denoise_SelectedItem");

                if (FilterVideoView.vm.FilterVideo_Denoise_Items.Contains(filterVideoDenoise))
                    FilterVideoView.vm.FilterVideo_Denoise_SelectedItem = filterVideoDenoise;
                else
                    listFailedImports.Add("Filter Video: Denoise");

                // Deinterlace
                string filterVideoDeinterlace = inif.Read("Filter Video", "Deinterlace_SelectedItem");

                if (FilterVideoView.vm.FilterVideo_Deinterlace_Items.Contains(filterVideoDeinterlace))
                    FilterVideoView.vm.FilterVideo_Deinterlace_SelectedItem = filterVideoDeinterlace;
                else
                    listFailedImports.Add("Filter Video: Deinterlace");

                // EQ 
                // Brightness
                double video_EQ_Brightness_Value;
                double.TryParse(inif.Read("Filter Video", "EQ_Brightness_Value"), out video_EQ_Brightness_Value);
                FilterVideoView.vm.FilterVideo_EQ_Brightness_Value = video_EQ_Brightness_Value;

                // Contrast
                double video_EQ_Contrast_Value;
                double.TryParse(inif.Read("Filter Video", "EQ_Contrast_Value"), out video_EQ_Contrast_Value);
                FilterVideoView.vm.FilterVideo_EQ_Contrast_Value = video_EQ_Contrast_Value;

                // Saturation
                double video_EQ_Saturation_Value;
                double.TryParse(inif.Read("Filter Video", "EQ_Saturation_Value"), out video_EQ_Saturation_Value);
                FilterVideoView.vm.FilterVideo_EQ_Saturation_Value = video_EQ_Saturation_Value;

                // Gamma
                double video_EQ_Gamma_Value;
                double.TryParse(inif.Read("Filter Video", "EQ_Gamma_Value"), out video_EQ_Gamma_Value);
                FilterVideoView.vm.FilterVideo_EQ_Gamma_Value = video_EQ_Gamma_Value;

                // Selective Color
                int filterVideo_SelectiveColor_Index;
                int.TryParse(inif.Read("Filter Video", "SelectiveColor_SelectedIndex"), out filterVideo_SelectiveColor_Index);

                if (filterVideo_SelectiveColor_Index <= MainWindow.cboSelectiveColor_Items.Count) // Check if Index is in range
                {
                    FilterVideoView.vm.FilterVideo_SelectiveColor_SelectedIndex = filterVideo_SelectiveColor_Index;
                }
                else
                {
                    listFailedImports.Add("Filter Video: Selective Color");
                }

                // SelectiveColor Correction_Method
                string filterVideoSelectiveColor_Correction_Method = inif.Read("Filter Video", "SelectiveColor_Correction_Method_SelectedItem");

                if (FilterVideoView.vm.FilterVideo_SelectiveColor_Correction_Method_Items.Contains(filterVideoSelectiveColor_Correction_Method))
                    FilterVideoView.vm.FilterVideo_SelectiveColor_Correction_Method_SelectedItem = filterVideoSelectiveColor_Correction_Method;
                else
                    listFailedImports.Add("Filter Video: Selective Color Correction Method");

                // Reds
                // Reds Cyan
                double filterVideo_SelectiveColor_Reds_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Reds_Cyan_Value"), out filterVideo_SelectiveColor_Reds_Cyan_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Reds_Cyan_Value = filterVideo_SelectiveColor_Reds_Cyan_Value;
                // Reds Magenta
                double filterVideo_SelectiveColor_Reds_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Reds_Magenta_Value"), out filterVideo_SelectiveColor_Reds_Magenta_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Reds_Magenta_Value = filterVideo_SelectiveColor_Reds_Magenta_Value;
                // Reds Yellow
                double filterVideo_SelectiveColor_Reds_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Reds_Yellow_Value"), out filterVideo_SelectiveColor_Reds_Yellow_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Reds_Yellow_Value = filterVideo_SelectiveColor_Reds_Yellow_Value;

                // Yellows
                // Yellows Cyan
                double filterVideo_SelectiveColor_Yellows_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Yellows_Cyan_Value"), out filterVideo_SelectiveColor_Yellows_Cyan_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Yellows_Cyan_Value = filterVideo_SelectiveColor_Yellows_Cyan_Value;
                // Yellows Magenta
                double filterVideo_SelectiveColor_Yellows_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Yellows_Magenta_Value"), out filterVideo_SelectiveColor_Yellows_Magenta_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Yellows_Magenta_Value = filterVideo_SelectiveColor_Yellows_Magenta_Value;
                // Yellows Yellow
                double filterVideo_SelectiveColor_Yellows_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Yellows_Yellow_Value"), out filterVideo_SelectiveColor_Yellows_Yellow_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Yellows_Yellow_Value = filterVideo_SelectiveColor_Yellows_Yellow_Value;

                // Greens
                // Greens Cyan
                double filterVideo_SelectiveColor_Greens_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Greens_Cyan_Value"), out filterVideo_SelectiveColor_Greens_Cyan_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Greens_Cyan_Value = filterVideo_SelectiveColor_Greens_Cyan_Value;
                // Greens Magenta
                double filterVideo_SelectiveColor_Greens_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Greens_Magenta_Value"), out filterVideo_SelectiveColor_Greens_Magenta_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Greens_Magenta_Value = filterVideo_SelectiveColor_Greens_Magenta_Value;
                // Greens Yellow
                double filterVideo_SelectiveColor_Greens_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Greens_Yellow_Value"), out filterVideo_SelectiveColor_Greens_Yellow_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Greens_Yellow_Value = filterVideo_SelectiveColor_Greens_Yellow_Value;

                // Cyans
                // Cyans Cyan
                double filterVideo_SelectiveColor_Cyans_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Cyans_Cyan_Value"), out filterVideo_SelectiveColor_Cyans_Cyan_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Cyans_Cyan_Value = filterVideo_SelectiveColor_Cyans_Cyan_Value;
                // Cyans Magenta
                double filterVideo_SelectiveColor_Cyans_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Cyans_Magenta_Value"), out filterVideo_SelectiveColor_Cyans_Magenta_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Cyans_Magenta_Value = filterVideo_SelectiveColor_Cyans_Magenta_Value;
                // Cyans Yellow
                double filterVideo_SelectiveColor_Cyans_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Cyans_Yellow_Value"), out filterVideo_SelectiveColor_Cyans_Yellow_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Cyans_Yellow_Value = filterVideo_SelectiveColor_Cyans_Yellow_Value;

                // Blues
                // Blues Cyan
                double filterVideo_SelectiveColor_Blues_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Blues_Cyan_Value"), out filterVideo_SelectiveColor_Blues_Cyan_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Blues_Cyan_Value = filterVideo_SelectiveColor_Blues_Cyan_Value;
                // Blues Magenta
                double filterVideo_SelectiveColor_Blues_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Blues_Magenta_Value"), out filterVideo_SelectiveColor_Blues_Magenta_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Blues_Magenta_Value = filterVideo_SelectiveColor_Blues_Magenta_Value;
                // Blues Yellow
                double filterVideo_SelectiveColor_Blues_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Blues_Yellow_Value"), out filterVideo_SelectiveColor_Blues_Yellow_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Blues_Yellow_Value = filterVideo_SelectiveColor_Blues_Yellow_Value;

                // Magentas
                // Magentas Cyan
                double filterVideo_SelectiveColor_Magentas_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Magentas_Cyan_Value"), out filterVideo_SelectiveColor_Magentas_Cyan_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Magentas_Cyan_Value = filterVideo_SelectiveColor_Magentas_Cyan_Value;
                // Magentas Magenta
                double filterVideo_SelectiveColor_Magentas_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Magentas_Magenta_Value"), out filterVideo_SelectiveColor_Magentas_Magenta_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Magentas_Magenta_Value = filterVideo_SelectiveColor_Magentas_Magenta_Value;
                // Magentas Yellow
                double filterVideo_SelectiveColor_Magentas_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Magentas_Yellow_Value"), out filterVideo_SelectiveColor_Magentas_Yellow_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Magentas_Yellow_Value = filterVideo_SelectiveColor_Magentas_Yellow_Value;

                // Whites
                // Whites Cyan
                double filterVideo_SelectiveColor_Whites_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Whites_Cyan_Value"), out filterVideo_SelectiveColor_Whites_Cyan_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Whites_Cyan_Value = filterVideo_SelectiveColor_Whites_Cyan_Value;
                // Whites Magenta
                double filterVideo_SelectiveColor_Whites_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Whites_Magenta_Value"), out filterVideo_SelectiveColor_Whites_Magenta_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Whites_Magenta_Value = filterVideo_SelectiveColor_Whites_Magenta_Value;
                // Whites Yellow
                double filterVideo_SelectiveColor_Whites_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Whites_Yellow_Value"), out filterVideo_SelectiveColor_Whites_Yellow_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Whites_Yellow_Value = filterVideo_SelectiveColor_Whites_Yellow_Value;

                // Neutrals
                // Neutrals Cyan
                double filterVideo_SelectiveColor_Neutrals_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Neutrals_Cyan_Value"), out filterVideo_SelectiveColor_Neutrals_Cyan_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Neutrals_Cyan_Value = filterVideo_SelectiveColor_Neutrals_Cyan_Value;
                // Neutrals Magenta
                double filterVideo_SelectiveColor_Neutrals_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Neutrals_Magenta_Value"), out filterVideo_SelectiveColor_Neutrals_Magenta_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Neutrals_Magenta_Value = filterVideo_SelectiveColor_Neutrals_Magenta_Value;
                // Neutrals Yellow
                double filterVideo_SelectiveColor_Neutrals_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Neutrals_Yellow_Value"), out filterVideo_SelectiveColor_Neutrals_Yellow_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Neutrals_Yellow_Value = filterVideo_SelectiveColor_Neutrals_Yellow_Value;

                // Blacks
                // Blacks Cyan
                double filterVideo_SelectiveColor_Blacks_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Blacks_Cyan_Value"), out filterVideo_SelectiveColor_Blacks_Cyan_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Blacks_Cyan_Value = filterVideo_SelectiveColor_Blacks_Cyan_Value;
                // Blacks Magenta
                double filterVideo_SelectiveColor_Blacks_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Blacks_Magenta_Value"), out filterVideo_SelectiveColor_Blacks_Magenta_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Blacks_Magenta_Value = filterVideo_SelectiveColor_Blacks_Magenta_Value;
                // Blacks Yellow
                double filterVideo_SelectiveColor_Blacks_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Blacks_Yellow_Value"), out filterVideo_SelectiveColor_Blacks_Yellow_Value);
                FilterVideoView.vm.FilterVideo_SelectiveColor_Blacks_Yellow_Value = filterVideo_SelectiveColor_Blacks_Yellow_Value;


                // --------------------------------------------------
                // Filter Audio
                // --------------------------------------------------
                // Lowpass
                string filterAudioLowpass = inif.Read("Filter Audio", "Lowpass_SelectedItem");

                if (FilterAudioView.vm.FilterAudio_Lowpass_Items.Contains(filterAudioLowpass))
                    FilterAudioView.vm.FilterAudio_Lowpass_SelectedItem = filterAudioLowpass;
                else
                    listFailedImports.Add("Filter Audio: Lowpass");

                // Highpass
                string filterAudioHighpass = inif.Read("Filter Audio", "Highpass_SelectedItem");

                if (FilterAudioView.vm.FilterAudio_Highpass_Items.Contains(filterAudioHighpass))
                    FilterAudioView.vm.FilterAudio_Highpass_SelectedItem = filterAudioHighpass;
                else
                    listFailedImports.Add("Filter Audio: Highpass");

                // Contrast
                double filterAudio_Contrast_Value;
                double.TryParse(inif.Read("Filter Audio", "Contrast_Value"), out filterAudio_Contrast_Value);
                FilterAudioView.vm.FilterAudio_Contrast_Value = filterAudio_Contrast_Value;

                // Extra Stereo
                double filterAudio_ExtraStereo_Value;
                double.TryParse(inif.Read("Filter Audio", "ExtraStereo_Value"), out filterAudio_ExtraStereo_Value);
                FilterAudioView.vm.FilterAudio_ExtraStereo_Value = filterAudio_ExtraStereo_Value;

                // Headphones
                string filterAudioHeadphones = inif.Read("Filter Audio", "Headphones_SelectedItem");

                if (FilterAudioView.vm.FilterAudio_Headphones_Items.Contains(filterAudioHeadphones))
                    FilterAudioView.vm.FilterAudio_Headphones_SelectedItem = filterAudioHeadphones;
                else
                    listFailedImports.Add("Filter Audio: Headphones");

                // Tempo
                double filterAudio_Tempo_Value;
                double.TryParse(inif.Read("Filter Audio", "Tempo_Value"), out filterAudio_Tempo_Value);
                FilterAudioView.vm.FilterAudio_Tempo_Value = filterAudio_Tempo_Value;


                // --------------------------------------------------
                // Failed Imports
                // --------------------------------------------------
                if (listFailedImports.Count > 0 && listFailedImports != null)
                {
                    // Open Window
                    MainWindow mainwindow = (MainWindow)Application.Current.MainWindow;
                    FailedImportWindow(mainwindow, listFailedImports);
                }
            }

            // -------------------------
            // Preset ini file does not exist
            // -------------------------
            else
            {
                MessageBox.Show("Preset does not exist.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                return;
            }
        }



        /// <summary>
        ///    Export Prest
        /// </summary>
        public static void ExportPreset(string profile)
        {
            // -------------------------
            // Check if Preset Directory exists
            // -------------------------
            if (Directory.Exists(ConfigureView.vm.CustomPresetsPath_Text))
            {
                // Start INI File Write
                Configure.INIFile inif = new Configure.INIFile(profile);

                // --------------------------------------------------
                // Main Window
                // --------------------------------------------------
                // Batch
                inif.Write("Main Window", "Batch_IsChecked", MainView.vm.Batch_IsChecked.ToString().ToLower());
                inif.Write("Main Window", "BatchExtension_Text", MainView.vm.BatchExtension_Text);


                // --------------------------------------------------
                // Format
                // --------------------------------------------------
                // Container
                inif.Write("Format", "Container_SelectedItem", FormatView.vm.Format_Container_SelectedItem);

                // MediaType
                inif.Write("Format", "MediaType_SelectedItem", FormatView.vm.Format_MediaType_SelectedItem);

                // Cut Time
                inif.Write("Format", "Cut_SelectedItem", FormatView.vm.Format_Cut_SelectedItem);
                // Start
                inif.Write("Format", "CutStart_Hours_Text", FormatView.vm.Format_CutStart_Hours_Text);
                inif.Write("Format", "CutStart_Minutes_Text", FormatView.vm.Format_CutStart_Minutes_Text);
                inif.Write("Format", "CutStart_Seconds_Text", FormatView.vm.Format_CutStart_Seconds_Text);
                inif.Write("Format", "CutStart_Milliseconds_Text", FormatView.vm.Format_CutStart_Milliseconds_Text);
                // End
                inif.Write("Format", "CutEnd_Hours_Text", FormatView.vm.Format_CutEnd_Hours_Text);
                inif.Write("Format", "CutEnd_Minutes_Text", FormatView.vm.Format_CutEnd_Minutes_Text);
                inif.Write("Format", "CutEnd_Seconds_Text", FormatView.vm.Format_CutEnd_Seconds_Text);
                inif.Write("Format", "CutEnd_Milliseconds_Text", FormatView.vm.Format_CutEnd_Milliseconds_Text);

                // Cut Frames
                inif.Write("Format", "FrameStart_Text", FormatView.vm.Format_FrameStart_Text);
                inif.Write("Format", "FrameEnd_Text", FormatView.vm.Format_FrameEnd_Text);

                // YouTube Download
                inif.Write("Format", "YouTube_SelectedItem", FormatView.vm.Format_YouTube_SelectedItem);
                inif.Write("Format", "YouTube_Quality_SelectedItem", FormatView.vm.Format_YouTube_Quality_SelectedItem);

                // --------------------------------------------------
                // Video
                // --------------------------------------------------
                // Codec
                inif.Write("Video", "Codec_SelectedItem", VideoView.vm.Video_Codec_SelectedItem);

                // Quality
                inif.Write("Video", "EncodeSpeed_SelectedItem", VideoView.vm.Video_EncodeSpeed_SelectedItem);
                inif.Write("Video", "HWAccel_SelectedItem", VideoView.vm.Video_HWAccel_SelectedItem);
                inif.Write("Video", "Quality_SelectedItem", VideoView.vm.Video_Quality_SelectedItem);
                inif.Write("Video", "Pass_SelectedItem", VideoView.vm.Video_Pass_SelectedItem);
                inif.Write("Video", "CRF_Text", VideoView.vm.Video_CRF_Text);
                inif.Write("Video", "BitRate_Text", VideoView.vm.Video_BitRate_Text);
                inif.Write("Video", "MinRate_Text", VideoView.vm.Video_MinRate_Text);
                inif.Write("Video", "MaxRate_Text", VideoView.vm.Video_MaxRate_Text);
                inif.Write("Video", "BufSize_Text", VideoView.vm.Video_BufSize_Text);
                inif.Write("Video", "VBR_IsChecked", VideoView.vm.Video_VBR_IsChecked.ToString().ToLower());
                inif.Write("Video", "PixelFormat_SelectedItem", VideoView.vm.Video_PixelFormat_SelectedItem);
                inif.Write("Video", "FPS_SelectedItem", VideoView.vm.Video_FPS_SelectedItem);
                inif.Write("Video", "Speed_SelectedItem", VideoView.vm.Video_Speed_SelectedItem);

                // Optimize
                inif.Write("Video", "Optimize_SelectedItem", VideoView.vm.Video_Optimize_SelectedItem);
                inif.Write("Video", "Video_Optimize_Tune_SelectedItem", VideoView.vm.Video_Video_Optimize_Tune_SelectedItem);
                inif.Write("Video", "Video_Optimize_Profile_SelectedItem", VideoView.vm.Video_Video_Optimize_Profile_SelectedItem);
                inif.Write("Video", "Optimize_Level_SelectedItem", VideoView.vm.Video_Optimize_Level_SelectedItem);

                // Size
                inif.Write("Video", "Scale_SelectedItem", VideoView.vm.Video_Scale_SelectedItem);
                inif.Write("Video", "Width_Text", VideoView.vm.Video_Width_Text);
                inif.Write("Video", "Height_Text", VideoView.vm.Video_Height_Text);
                inif.Write("Video", "ScreenFormat_SelectedItem", VideoView.vm.Video_ScreenFormat_SelectedItem);
                inif.Write("Video", "AspectRatio_SelectedItem", VideoView.vm.Video_AspectRatio_SelectedItem);
                inif.Write("Video", "ScalingAlgorithm_SelectedItem", VideoView.vm.Video_ScalingAlgorithm_SelectedItem);

                // Crop
                inif.Write("Video", "Crop_X_Text", VideoView.vm.Video_Crop_X_Text);
                inif.Write("Video", "Crop_Y_Text", VideoView.vm.Video_Crop_Y_Text);
                inif.Write("Video", "Crop_Width_Text", VideoView.vm.Video_Crop_Width_Text);
                inif.Write("Video", "Crop_Height_Text", VideoView.vm.Video_Crop_Height_Text);
                inif.Write("Video", "CropClear_Text", VideoView.vm.Video_CropClear_Text);


                // --------------------------------------------------
                // Audio
                // --------------------------------------------------
                // Codec
                inif.Write("Audio", "Codec_SelectedItem", AudioView.vm.Audio_Codec_SelectedItem);

                // Stream
                inif.Write("Audio", "Stream_SelectedItem", AudioView.vm.Audio_Stream_SelectedItem);

                // Channel
                inif.Write("Audio", "Channel_SelectedItem", AudioView.vm.Audio_Channel_SelectedItem);

                // Quality
                inif.Write("Audio", "Quality_SelectedItem", AudioView.vm.Audio_Quality_SelectedItem);
                inif.Write("Audio", "VBR_IsChecked", AudioView.vm.Audio_VBR_IsChecked.ToString().ToLower());
                inif.Write("Audio", "BitRate_Text", AudioView.vm.Audio_BitRate_Text);
                inif.Write("Audio", "CompressionLevel_SelectedItem", AudioView.vm.Audio_CompressionLevel_SelectedItem);
                inif.Write("Audio", "SampleRate_SelectedItem", AudioView.vm.Audio_SampleRate_SelectedItem);
                inif.Write("Audio", "BitDepth_SelectedItem", AudioView.vm.Audio_BitDepth_SelectedItem);

                // Filter
                inif.Write("Audio", "Volume_Text", AudioView.vm.Audio_Volume_Text);
                inif.Write("Audio", "HardLimiter_Value", AudioView.vm.Audio_HardLimiter_Value.ToString());


                // --------------------------------------------------
                // Subtitle
                // --------------------------------------------------
                // Codec
                inif.Write("Subtitle", "Codec_SelectedItem", SubtitleView.vm.Subtitle_Codec_SelectedItem);

                // Stream
                inif.Write("Subtitle", "Stream_SelectedItem", SubtitleView.vm.Subtitle_Stream_SelectedItem);


                // --------------------------------------------------
                // Filter Video
                // --------------------------------------------------
                // Fix
                inif.Write("Filter Video", "Deband_SelectedItem", FilterVideoView.vm.FilterVideo_Deband_SelectedItem);
                inif.Write("Filter Video", "Deshake_SelectedItem", FilterVideoView.vm.FilterVideo_Deshake_SelectedItem);
                inif.Write("Filter Video", "Deflicker_SelectedItem", FilterVideoView.vm.FilterVideo_Deflicker_SelectedItem);
                inif.Write("Filter Video", "Dejudder_SelectedItem", FilterVideoView.vm.FilterVideo_Dejudder_SelectedItem);
                inif.Write("Filter Video", "Denoise_SelectedItem", FilterVideoView.vm.FilterVideo_Denoise_SelectedItem);
                inif.Write("Filter Video", "Deinterlace_SelectedItem", FilterVideoView.vm.FilterVideo_Deinterlace_SelectedItem);

                // EQ
                inif.Write("Filter Video", "EQ_Brightness_Value", FilterVideoView.vm.FilterVideo_EQ_Brightness_Value.ToString());
                inif.Write("Filter Video", "EQ_Contrast_Value", FilterVideoView.vm.FilterVideo_EQ_Contrast_Value.ToString());
                inif.Write("Filter Video", "EQ_Saturation_Value", FilterVideoView.vm.FilterVideo_EQ_Saturation_Value.ToString());
                inif.Write("Filter Video", "EQ_Gamma_Value", FilterVideoView.vm.FilterVideo_EQ_Gamma_Value.ToString());

                // Selective Color
                inif.Write("Filter Video", "SelectiveColor_SelectedIndex", (FilterVideoView.vm.FilterVideo_SelectiveColor_SelectedIndex.ToString()));

                // Selective Color Method
                inif.Write("Filter Video", "SelectiveColor_Correction_Method_SelectedItem", FilterVideoView.vm.FilterVideo_SelectiveColor_Correction_Method_SelectedItem);

                // Reds
                inif.Write("Filter Video", "SelectiveColor_Reds_Cyan_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Reds_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Reds_Magenta_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Reds_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Reds_Yellow_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Reds_Yellow_Value.ToString());

                // Yellows
                inif.Write("Filter Video", "SelectiveColor_Yellows_Cyan_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Yellows_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Yellows_Magenta_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Yellows_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Yellows_Yellow_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Yellows_Yellow_Value.ToString());

                // Greens
                inif.Write("Filter Video", "SelectiveColor_Greens_Cyan_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Greens_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Greens_Magenta_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Greens_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Greens_Yellow_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Greens_Yellow_Value.ToString());

                // Cyans
                inif.Write("Filter Video", "SelectiveColor_Cyans_Cyan_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Cyans_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Cyans_Magenta_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Cyans_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Cyans_Yellow_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Cyans_Yellow_Value.ToString());

                // Blues
                inif.Write("Filter Video", "SelectiveColor_Blues_Cyan_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Blues_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Blues_Magenta_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Blues_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Blues_Yellow_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Blues_Yellow_Value.ToString());

                // Magentas
                inif.Write("Filter Video", "SelectiveColor_Magentas_Cyan_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Magentas_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Magentas_Magenta_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Magentas_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Magentas_Yellow_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Magentas_Yellow_Value.ToString());

                // Whites
                inif.Write("Filter Video", "SelectiveColor_Whites_Cyan_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Whites_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Whites_Magenta_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Whites_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Whites_Yellow_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Whites_Yellow_Value.ToString());

                // Neutrals
                inif.Write("Filter Video", "SelectiveColor_Neutrals_Cyan_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Neutrals_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Neutrals_Magenta_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Neutrals_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Neutrals_Yellow_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Neutrals_Yellow_Value.ToString());

                // Blacks
                inif.Write("Filter Video", "SelectiveColor_Blacks_Cyan_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Blacks_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Blacks_Magenta_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Blacks_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Blacks_Yellow_Value", FilterVideoView.vm.FilterVideo_SelectiveColor_Blacks_Yellow_Value.ToString());

                // --------------------------------------------------
                // Filter Audio
                // --------------------------------------------------
                // Lowpass
                inif.Write("Filter Audio", "Lowpass_SelectedItem", FilterAudioView.vm.FilterAudio_Lowpass_SelectedItem);

                // Highpass
                inif.Write("Filter Audio", "Highpass_SelectedItem", FilterAudioView.vm.FilterAudio_Highpass_SelectedItem);

                // Contrast
                inif.Write("Filter Audio", "Contrast_Value", FilterAudioView.vm.FilterAudio_Contrast_Value.ToString());

                // Extra Stereo
                inif.Write("Filter Audio", "ExtraStereo_Value", FilterAudioView.vm.FilterAudio_ExtraStereo_Value.ToString());

                // Headphones
                inif.Write("Filter Audio", "Headphones_SelectedItem", FilterAudioView.vm.FilterAudio_Headphones_SelectedItem);

                // Tempo
                inif.Write("Filter Audio", "Tempo_Value", FilterAudioView.vm.FilterAudio_Tempo_Value.ToString());
            }

            // -------------------------
            // Create Presets Folder if it does not exist
            // -------------------------
            else
            {
                // Yes/No Dialog Confirmation
                //
                MessageBoxResult resultExport = MessageBox.Show("Presets folder does not yet exist. Automatically create it?",
                                                                "Directory Not Found",
                                                                MessageBoxButton.YesNo,
                                                                MessageBoxImage.Information);
                switch (resultExport)
                {
                    // Create
                    case MessageBoxResult.Yes:
                        try
                        {
                            Directory.CreateDirectory(ConfigureView.vm.CustomPresetsPath_Text);
                        }
                        catch
                        {
                            MessageBox.Show("Could not create Presets folder. May require Administrator privileges.",
                                            "Error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);
                        }
                        break;
                    // Use Default
                    case MessageBoxResult.No:
                        break;
                }
            }

        }



        /// <summary>
        ///    Failed Import Window Open
        /// </summary>
        private static Boolean IsFailedImportWindowOpened = false;
        public static string failedImportMessage;
        public static void FailedImportWindow(MainWindow mainwindow, List<string> listFailedImports)
        {
            failedImportMessage = string.Join(Environment.NewLine, listFailedImports);

            // Detect which screen we're on
            var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
            var thisScreen = allScreens.SingleOrDefault(s => mainwindow.Left >= s.WorkingArea.Left && mainwindow.Left < s.WorkingArea.Right);

            // Start Window
            FailedImportWindow failedimportwindow = new FailedImportWindow();

            // Only allow 1 Window instance
            if (IsFailedImportWindowOpened) return;
            failedimportwindow.ContentRendered += delegate { IsFailedImportWindowOpened = true; };
            failedimportwindow.Closed += delegate { IsFailedImportWindowOpened = false; };

            // Position Relative to MainWindow
            failedimportwindow.Left = Math.Max((mainwindow.Left + (mainwindow.Width - failedimportwindow.Width) / 2), thisScreen.WorkingArea.Left);
            failedimportwindow.Top = Math.Max((mainwindow.Top + (mainwindow.Height - failedimportwindow.Height) / 2), thisScreen.WorkingArea.Top);

            // Open Window
            failedimportwindow.Show();
        }


    }
}
