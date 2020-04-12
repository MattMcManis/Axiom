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
    public class Profiles
    {
        /// <summary>
        /// Global Variables
        /// </summary>
        //public static string presetsDir = MainWindow.appDataLocalDir + @"Axiom UI\presets\"; // Custom User ini presets


        /// <summary>
        /// Scan PC Custom Presets
        /// </summary>
        public static List<string> customPresetPathsList = new List<string>();
        
        public static void LoadCustomPresets()
        {
            // Check if Custom Presets Path is valid
            if (MainWindow.IsValidPath(VM.ConfigureView.CustomPresetsPath_Text) == false)
            {
                return;
            }

            // -------------------------
            // User Custom Preset Full Path
            // -------------------------
            if (Directory.Exists(VM.ConfigureView.CustomPresetsPath_Text))
            {
                // Get Custom .ini Preset Paths
                customPresetPathsList = Directory.GetFiles(VM.ConfigureView.CustomPresetsPath_Text, "*.ini")
                                                             .Select(Path.GetFullPath)
                                                             .OrderByDescending(x => x)
                                                             .ToList();

                // Preset Names Only List
                List<string> presetNamesList = VM.MainView.Preset_Items.Select(item => item.Name).ToList();

                try
                {
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
                            VM.MainView.Preset_Items.Insert(3, new MainViewModel.Preset() { Name = presetName, Category = false, Type = "Custom" });
                        }
                    }
                }
                catch
                {

                }


                // -------------------------
                // Cleanup Missing/Deleted Presets
                // -------------------------
                // Refresh the List
                presetNamesList = VM.MainView.Preset_Items.Select(item => item.Name).ToList();

                // .ini file is missing in \Axiom UI\presets directory
                try
                {
                    for (int i = VM.MainView.Preset_Items.Count - 1; i >= 0; --i)
                    {
                        // If .ini File List does not contain Preset Name
                        if (!customPresetPathsList.Contains(/*Profiles.presetsDir*/ VM.ConfigureView.CustomPresetsPath_Text + presetNamesList[i] + ".ini"))
                        //if (!File.Exists(Profiles.presetsDir + presetNamesList[i] + ".ini"))
                        {
                            // Remove from Presets List if Type is Custom
                            if (VM.MainView.Preset_Items.FirstOrDefault(item => item.Name == presetNamesList[i])?.Type == "Custom")
                            {
                                VM.MainView.Preset_Items.RemoveAt(i);
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
                //VM.MainView.CustomPresetPath_Text = presetsDir;
            }
        }



        /// <summary>
        /// Import Preset
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
                VM.MainView.Batch_IsChecked = mainwindow_Batch_IsChecked;

                // Batch Extension
                VM.MainView.BatchExtension_Text = inif.Read("Main Window", "BatchExtension_Text");


                // --------------------------------------------------
                // Format
                // --------------------------------------------------
                // Container
                string container = inif.Read("Format", "Container_SelectedItem");
                List<string> containerNames = VM.FormatView.Format_Container_Items.Select(item => item.Name).ToList();

                if (containerNames.Contains(container))
                    VM.FormatView.Format_Container_SelectedItem = container;
                else
                    listFailedImports.Add("Format: Container");

                // MediaType
                string mediaType = inif.Read("Format", "MediaType_SelectedItem");
                if (VM.FormatView.Format_MediaType_Items.Contains(mediaType))
                    VM.FormatView.Format_MediaType_SelectedItem = mediaType;
                else
                    listFailedImports.Add("Format: Media Type");

                // Cut
                string cut = inif.Read("Format", "Cut_SelectedItem");
                if (VM.FormatView.Format_Cut_Items.Contains(cut))
                    VM.FormatView.Format_Cut_SelectedItem = cut;
                else
                    listFailedImports.Add("Format: Cut");

                // Cut Start
                VM.FormatView.Format_CutStart_Hours_Text = inif.Read("Format", "CutStart_Hours_Text");
                VM.FormatView.Format_CutStart_Minutes_Text = inif.Read("Format", "CutStart_Minutes_Text");
                VM.FormatView.Format_CutStart_Seconds_Text = inif.Read("Format", "CutStart_Seconds_Text");
                VM.FormatView.Format_CutStart_Milliseconds_Text = inif.Read("Format", "CutStart_Milliseconds_Text");
                // Cut End
                VM.FormatView.Format_CutEnd_Hours_Text = inif.Read("Format", "CutEnd_Hours_Text");
                VM.FormatView.Format_CutEnd_Minutes_Text = inif.Read("Format", "CutEnd_Minutes_Text");
                VM.FormatView.Format_CutEnd_Seconds_Text = inif.Read("Format", "CutEnd_Seconds_Text");
                VM.FormatView.Format_CutEnd_Milliseconds_Text = inif.Read("Format", "CutEnd_Milliseconds_Text");

                // Cut Frames
                VM.FormatView.Format_FrameStart_Text = inif.Read("Format", "FrameStart_Text");
                VM.FormatView.Format_FrameEnd_Text = inif.Read("Format", "FrameEnd_Text");

                // YouTube Download
                VM.FormatView.Format_YouTube_SelectedItem = inif.Read("Format", "YouTube_SelectedItem");
                VM.FormatView.Format_YouTube_Quality_SelectedItem = inif.Read("Format", "YouTube_Quality_SelectedItem");


                // --------------------------------------------------
                // Video
                // --------------------------------------------------
                // Codec
                string videoCodec = inif.Read("Video", "Codec_SelectedItem");

                if (VM.VideoView.Video_Codec_Items.Contains(videoCodec))
                    VM.VideoView.Video_Codec_SelectedItem = videoCodec;
                else
                    listFailedImports.Add("Video: Codec");

                // Encode Speed
                string videoEncodeSpeed = inif.Read("Video", "EncodeSpeed_SelectedItem");
                List<string> videoEncodeSpeedNames = VM.VideoView.Video_EncodeSpeed_Items.Select(item => item.Name).ToList();

                if (videoEncodeSpeedNames.Contains(videoEncodeSpeed))
                    VM.VideoView.Video_EncodeSpeed_SelectedItem = videoEncodeSpeed;
                else
                    listFailedImports.Add("Video: EncodeSpeed");

                // HWAccel
                string hwAccel = inif.Read("Video", "HWAccel_SelectedItem");
                if (VM.VideoView.Video_HWAccel_Items.Contains(hwAccel))
                    VM.VideoView.Video_HWAccel_SelectedItem = hwAccel;
                else
                    listFailedImports.Add("Video: HWAccel");

                // Quality
                string videoQuality = inif.Read("Video", "Quality_SelectedItem");
                List<string> videoQualityNames = VM.VideoView.Video_Quality_Items.Select(item => item.Name).ToList();

                if (videoQualityNames.Contains(videoQuality))
                    VM.VideoView.Video_Quality_SelectedItem = videoQuality;
                else
                    listFailedImports.Add("Video: Quality");

                // Pass
                string videoPass = inif.Read("Video", "Pass_SelectedItem");

                if (VM.VideoView.Video_Pass_Items.Contains(videoPass))
                    VM.VideoView.Video_Pass_SelectedItem = videoPass;
                else
                    listFailedImports.Add("Video: Pass");

                // CRF
                VM.VideoView.Video_CRF_Text = inif.Read("Video", "CRF_Text");

                // Bit Rate
                VM.VideoView.Video_BitRate_Text = inif.Read("Video", "BitRate_Text");

                // Min Rate
                VM.VideoView.Video_MinRate_Text = inif.Read("Video", "MinRate_Text");

                // Max Rate
                VM.VideoView.Video_MaxRate_Text = inif.Read("Video", "MaxRate_Text");

                // Max Rate
                VM.VideoView.Video_BufSize_Text = inif.Read("Video", "BufSize_Text");

                // Video VBR
                bool video_VBR_IsChecked;
                bool.TryParse(inif.Read("Video", "VBR_IsChecked").ToLower(), out video_VBR_IsChecked);
                VM.VideoView.Video_VBR_IsChecked = video_VBR_IsChecked;

                // Pixel Format
                string videoPixelFormat = inif.Read("Video", "PixelFormat_SelectedItem");

                if (VM.VideoView.Video_PixelFormat_Items.Contains(videoPixelFormat))
                    VM.VideoView.Video_PixelFormat_SelectedItem = videoPixelFormat;
                else
                    listFailedImports.Add("Video: PixelFormat");

                // FPS
                bool videoFPS_IsEditable;
                bool.TryParse(inif.Read("Video", "FPS_IsEditable").ToLower(), out videoFPS_IsEditable);

                if (videoFPS_IsEditable == false) // Selected
                {
                    string videoFPS = inif.Read("Video", "FPS_SelectedItem");

                    if (VM.VideoView.Video_FPS_Items.Contains(videoFPS))
                        VM.VideoView.Video_FPS_SelectedItem = videoFPS;
                    else
                        listFailedImports.Add("Video: FPS");
                }
                else if (videoFPS_IsEditable == true) // Custom
                {
                    VM.VideoView.Video_FPS_IsEditable = true;
                    VM.VideoView.Video_FPS_Text = inif.Read("Video", "FPS_Text");
                }

                // Speed
                bool videoSpeed_IsEditable;
                bool.TryParse(inif.Read("Video", "Speed_IsEditable").ToLower(), out videoSpeed_IsEditable);

                if (videoSpeed_IsEditable == false) // Selected
                {
                    string videoSpeed = inif.Read("Video", "Speed_SelectedItem");

                    if (VM.VideoView.Video_Speed_Items.Contains(videoSpeed))
                        VM.VideoView.Video_Speed_SelectedItem = videoSpeed;
                    else
                        listFailedImports.Add("Video: Speed");
                }
                else if (videoSpeed_IsEditable == true) // Custom
                {
                    VM.VideoView.Video_Speed_IsEditable = true;
                    VM.VideoView.Video_Speed_Text = inif.Read("Video", "Speed_Text");
                }

                // Optimize
                string videoOptimize = inif.Read("Video", "Optimize_SelectedItem");
                List<string> videoOptimizeNames = VM.VideoView.Video_Optimize_Items.Select(item => item.Name).ToList();

                if (videoOptimizeNames.Contains(videoOptimize))
                    VM.VideoView.Video_Optimize_SelectedItem = videoOptimize;
                else
                    listFailedImports.Add("Video: Optimize");

                // Optimize Tune
                string videoOptimize_Tune = inif.Read("Video", "Video_Optimize_Tune_SelectedItem");

                if (VM.VideoView.Video_Optimize_Tune_Items.Contains(videoOptimize_Tune))
                    VM.VideoView.Video_Video_Optimize_Tune_SelectedItem = videoOptimize_Tune;
                else
                    listFailedImports.Add("Video: Optimize Tune");

                // Optimize Profile
                string videoOptimize_Profile = inif.Read("Video", "Video_Optimize_Profile_SelectedItem");

                if (VM.VideoView.Video_Optimize_Profile_Items.Contains(videoOptimize_Profile))
                    VM.VideoView.Video_Video_Optimize_Profile_SelectedItem = videoOptimize_Profile;
                else
                    listFailedImports.Add("Video: Optimize Profile");

                // Optimize Level
                string videoOptimize_Level = inif.Read("Video", "Optimize_Level_SelectedItem");

                if (VM.VideoView.Video_Optimize_Level_Items.Contains(videoOptimize_Level))
                    VM.VideoView.Video_Optimize_Level_SelectedItem = videoOptimize_Level;
                else
                    listFailedImports.Add("Video: Optimize Level");

                // Scale
                string videoScale = inif.Read("Video", "Scale_SelectedItem");

                if (VM.VideoView.Video_Scale_Items.Contains(videoScale))
                    VM.VideoView.Video_Scale_SelectedItem = videoScale;
                else
                    listFailedImports.Add("Video: Scale");

                // Width
                VM.VideoView.Video_Width_Text = inif.Read("Video", "Width_Text");
                // Height
                VM.VideoView.Video_Height_Text = inif.Read("Video", "Height_Text");

                // Screen Format
                string videoScreenFormat = inif.Read("Video", "ScreenFormat_SelectedItem");

                if (VM.VideoView.Video_ScreenFormat_Items.Contains(videoScreenFormat))
                    VM.VideoView.Video_ScreenFormat_SelectedItem = videoScreenFormat;
                else
                    listFailedImports.Add("Video: Screen Format");

                // Aspect Ratio
                string videoAspectRatio = inif.Read("Video", "AspectRatio_SelectedItem");

                if (VM.VideoView.Video_AspectRatio_Items.Contains(videoAspectRatio))
                    VM.VideoView.Video_AspectRatio_SelectedItem = videoAspectRatio;
                else
                    listFailedImports.Add("Video: Aspect Ratio");

                // Scaling Algorithm
                string videoScalingAlgorithm = inif.Read("Video", "ScalingAlgorithm_SelectedItem");

                if (VM.VideoView.Video_ScalingAlgorithm_Items.Contains(videoScalingAlgorithm))
                    VM.VideoView.Video_ScalingAlgorithm_SelectedItem = videoScalingAlgorithm;
                else
                    listFailedImports.Add("Video: Scaling Algorithm");

                // Crop X
                VM.VideoView.Video_Crop_X_Text = inif.Read("Video", "Crop_X_Text");
                // Crop Y
                VM.VideoView.Video_Crop_Y_Text = inif.Read("Video", "Crop_Y_Text");
                // Crop Width
                VM.VideoView.Video_Crop_Width_Text = inif.Read("Video", "Crop_Width_Text");
                // Crop Height
                VM.VideoView.Video_Crop_Height_Text = inif.Read("Video", "Crop_Height_Text");
                // Crop Clear
                VM.VideoView.Video_CropClear_Text = inif.Read("Video", "CropClear_Text");

                // --------------------------------------------------
                // Audio
                // --------------------------------------------------
                // Codec
                string audioCodec = inif.Read("Audio", "Codec_SelectedItem");

                if (VM.AudioView.Audio_Codec_Items.Contains(audioCodec))
                    VM.AudioView.Audio_Codec_SelectedItem = audioCodec;
                else
                    listFailedImports.Add("Audio: Codec");

                // Stream
                string audioStream = inif.Read("Audio", "Stream_SelectedItem");

                if (VM.AudioView.Audio_Stream_Items.Contains(audioStream))
                    VM.AudioView.Audio_Stream_SelectedItem = audioStream;
                else
                    listFailedImports.Add("Audio: Stream");

                // Channel
                string audioChannel = inif.Read("Audio", "Channel_SelectedItem");

                if (VM.AudioView.Audio_Channel_Items.Contains(audioChannel))
                    VM.AudioView.Audio_Channel_SelectedItem = audioChannel;
                else
                    listFailedImports.Add("Audio: Channel");

                // Quality
                string audioQuality = inif.Read("Audio", "Quality_SelectedItem");
                List<string> audioQualityNames = VM.AudioView.Audio_Quality_Items.Select(item => item.Name).ToList();

                if (audioQualityNames.Contains(audioQuality))
                    VM.AudioView.Audio_Quality_SelectedItem = audioQuality;
                else
                    listFailedImports.Add("Audio: Quality");

                // Audio VBR
                bool audio_VBR_IsChecked;
                bool.TryParse(inif.Read("Audio", "VBR_IsChecked").ToLower(), out audio_VBR_IsChecked);
                VM.AudioView.Audio_VBR_IsChecked = audio_VBR_IsChecked;

                // Bit Rate
                VM.AudioView.Audio_BitRate_Text = inif.Read("Audio", "BitRate_Text");

                // Compression Level
                string audioCompressionLevel = inif.Read("Audio", "CompressionLevel_SelectedItem");

                if (VM.AudioView.Audio_CompressionLevel_Items.Contains(audioCompressionLevel))
                    VM.AudioView.Audio_CompressionLevel_SelectedItem = audioCompressionLevel;
                else
                    listFailedImports.Add("Audio: Compression Level");

                // Sample Rate
                string audioSampleRate = inif.Read("Audio", "SampleRate_SelectedItem");
                List<string> audioSampleRateNames = VM.AudioView.Audio_SampleRate_Items.Select(item => item.Name).ToList();

                if (audioSampleRateNames.Contains(audioSampleRate))
                    VM.AudioView.Audio_SampleRate_SelectedItem = audioSampleRate;
                else
                    listFailedImports.Add("Audio: Sample Rate");

                // Bit Depth
                string audioBitDepth = inif.Read("Audio", "BitDepth_SelectedItem");
                List<string> audioBitDepthNames = VM.AudioView.Audio_BitDepth_Items.Select(item => item.Name).ToList();

                if (audioBitDepthNames.Contains(audioBitDepth))
                    VM.AudioView.Audio_BitDepth_SelectedItem = audioBitDepth;
                else
                    listFailedImports.Add("Audio: Bit Depth");

                // Volume
                VM.AudioView.Audio_Volume_Text = inif.Read("Audio", "Volume_Text");

                // Hard Limiter
                string audio_HardLimiterStr = inif.Read("Audio", "HardLimiter_Value"); // old value before using dB

                double audio_HardLimiter_Value;
                double.TryParse(inif.Read("Audio", "HardLimiter_Value"), out audio_HardLimiter_Value);

                if (audio_HardLimiterStr == "1.00") // Fixes the old default value
                {
                    // Change the new default value to use dB
                    audio_HardLimiter_Value = 0.0;
                }
                else
                {
                    VM.AudioView.Audio_HardLimiter_Value = audio_HardLimiter_Value;
                }

               
                // --------------------------------------------------
                // Subtitle
                // --------------------------------------------------
                // Codec
                string subtitleCodec = inif.Read("Subtitle", "Codec_SelectedItem");

                if (VM.SubtitleView.Subtitle_Codec_Items.Contains(subtitleCodec))
                    VM.SubtitleView.Subtitle_Codec_SelectedItem = subtitleCodec;
                else
                    listFailedImports.Add("Subtitle: Codec");

                // Stream
                string subtitleStream = inif.Read("Subtitle", "Stream_SelectedItem");

                if (VM.SubtitleView.Subtitle_Stream_Items.Contains(subtitleStream))
                    VM.SubtitleView.Subtitle_Stream_SelectedItem = subtitleStream;
                else
                    listFailedImports.Add("Subtitle: Stream");


                // --------------------------------------------------
                // Filter Video
                // --------------------------------------------------
                // Deband
                string filterVideoDeband = inif.Read("Filter Video", "Deband_SelectedItem");

                if (VM.FilterVideoView.FilterVideo_Deband_Items.Contains(filterVideoDeband))
                    VM.FilterVideoView.FilterVideo_Deband_SelectedItem = filterVideoDeband;
                else
                    listFailedImports.Add("Filter Video: Deband");

                // Deshake
                string filterVideoDeshake = inif.Read("Filter Video", "Deshake_SelectedItem");

                if (VM.FilterVideoView.FilterVideo_Deshake_Items.Contains(filterVideoDeshake))
                    VM.FilterVideoView.FilterVideo_Deshake_SelectedItem = filterVideoDeshake;
                else
                    listFailedImports.Add("Filter Video: Deshake");

                // Deflicker
                string filterVideoDeflicker = inif.Read("Filter Video", "Deflicker_SelectedItem");

                if (VM.FilterVideoView.FilterVideo_Deflicker_Items.Contains(filterVideoDeflicker))
                    VM.FilterVideoView.FilterVideo_Deflicker_SelectedItem = filterVideoDeflicker;
                else
                    listFailedImports.Add("Filter Video: Deflicker");

                // Dejudder
                string filterVideoDejudder = inif.Read("Filter Video", "Dejudder_SelectedItem");

                if (VM.FilterVideoView.FilterVideo_Dejudder_Items.Contains(filterVideoDejudder))
                    VM.FilterVideoView.FilterVideo_Dejudder_SelectedItem = filterVideoDejudder;
                else
                    listFailedImports.Add("Filter Video: Dejudder");

                // Denoise
                string filterVideoDenoise = inif.Read("Filter Video", "Denoise_SelectedItem");

                if (VM.FilterVideoView.FilterVideo_Denoise_Items.Contains(filterVideoDenoise))
                    VM.FilterVideoView.FilterVideo_Denoise_SelectedItem = filterVideoDenoise;
                else
                    listFailedImports.Add("Filter Video: Denoise");

                // Deinterlace
                string filterVideoDeinterlace = inif.Read("Filter Video", "Deinterlace_SelectedItem");

                if (VM.FilterVideoView.FilterVideo_Deinterlace_Items.Contains(filterVideoDeinterlace))
                    VM.FilterVideoView.FilterVideo_Deinterlace_SelectedItem = filterVideoDeinterlace;
                else
                    listFailedImports.Add("Filter Video: Deinterlace");

                // EQ 
                // Brightness
                double video_EQ_Brightness_Value;
                double.TryParse(inif.Read("Filter Video", "EQ_Brightness_Value"), out video_EQ_Brightness_Value);
                VM.FilterVideoView.FilterVideo_EQ_Brightness_Value = video_EQ_Brightness_Value;

                // Contrast
                double video_EQ_Contrast_Value;
                double.TryParse(inif.Read("Filter Video", "EQ_Contrast_Value"), out video_EQ_Contrast_Value);
                VM.FilterVideoView.FilterVideo_EQ_Contrast_Value = video_EQ_Contrast_Value;

                // Saturation
                double video_EQ_Saturation_Value;
                double.TryParse(inif.Read("Filter Video", "EQ_Saturation_Value"), out video_EQ_Saturation_Value);
                VM.FilterVideoView.FilterVideo_EQ_Saturation_Value = video_EQ_Saturation_Value;

                // Gamma
                double video_EQ_Gamma_Value;
                double.TryParse(inif.Read("Filter Video", "EQ_Gamma_Value"), out video_EQ_Gamma_Value);
                VM.FilterVideoView.FilterVideo_EQ_Gamma_Value = video_EQ_Gamma_Value;

                // Selective Color
                int filterVideo_SelectiveColor_Index;
                int.TryParse(inif.Read("Filter Video", "SelectiveColor_SelectedIndex"), out filterVideo_SelectiveColor_Index);

                if (filterVideo_SelectiveColor_Index <= VM.FilterVideoView.FilterVideo_SelectiveColor_Items.Count) // Check if Index is in range
                {
                    VM.FilterVideoView.FilterVideo_SelectiveColor_SelectedIndex = filterVideo_SelectiveColor_Index;
                }
                else
                {
                    listFailedImports.Add("Filter Video: Selective Color");
                }

                // SelectiveColor Correction_Method
                string filterVideoSelectiveColor_Correction_Method = inif.Read("Filter Video", "SelectiveColor_Correction_Method_SelectedItem");

                if (VM.FilterVideoView.FilterVideo_SelectiveColor_Correction_Method_Items.Contains(filterVideoSelectiveColor_Correction_Method))
                    VM.FilterVideoView.FilterVideo_SelectiveColor_Correction_Method_SelectedItem = filterVideoSelectiveColor_Correction_Method;
                else
                    listFailedImports.Add("Filter Video: Selective Color Correction Method");

                // Reds
                // Reds Cyan
                double filterVideo_SelectiveColor_Reds_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Reds_Cyan_Value"), out filterVideo_SelectiveColor_Reds_Cyan_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Cyan_Value = filterVideo_SelectiveColor_Reds_Cyan_Value;
                // Reds Magenta
                double filterVideo_SelectiveColor_Reds_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Reds_Magenta_Value"), out filterVideo_SelectiveColor_Reds_Magenta_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Magenta_Value = filterVideo_SelectiveColor_Reds_Magenta_Value;
                // Reds Yellow
                double filterVideo_SelectiveColor_Reds_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Reds_Yellow_Value"), out filterVideo_SelectiveColor_Reds_Yellow_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Yellow_Value = filterVideo_SelectiveColor_Reds_Yellow_Value;

                // Yellows
                // Yellows Cyan
                double filterVideo_SelectiveColor_Yellows_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Yellows_Cyan_Value"), out filterVideo_SelectiveColor_Yellows_Cyan_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Cyan_Value = filterVideo_SelectiveColor_Yellows_Cyan_Value;
                // Yellows Magenta
                double filterVideo_SelectiveColor_Yellows_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Yellows_Magenta_Value"), out filterVideo_SelectiveColor_Yellows_Magenta_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Magenta_Value = filterVideo_SelectiveColor_Yellows_Magenta_Value;
                // Yellows Yellow
                double filterVideo_SelectiveColor_Yellows_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Yellows_Yellow_Value"), out filterVideo_SelectiveColor_Yellows_Yellow_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Yellow_Value = filterVideo_SelectiveColor_Yellows_Yellow_Value;

                // Greens
                // Greens Cyan
                double filterVideo_SelectiveColor_Greens_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Greens_Cyan_Value"), out filterVideo_SelectiveColor_Greens_Cyan_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Cyan_Value = filterVideo_SelectiveColor_Greens_Cyan_Value;
                // Greens Magenta
                double filterVideo_SelectiveColor_Greens_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Greens_Magenta_Value"), out filterVideo_SelectiveColor_Greens_Magenta_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Magenta_Value = filterVideo_SelectiveColor_Greens_Magenta_Value;
                // Greens Yellow
                double filterVideo_SelectiveColor_Greens_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Greens_Yellow_Value"), out filterVideo_SelectiveColor_Greens_Yellow_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Yellow_Value = filterVideo_SelectiveColor_Greens_Yellow_Value;

                // Cyans
                // Cyans Cyan
                double filterVideo_SelectiveColor_Cyans_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Cyans_Cyan_Value"), out filterVideo_SelectiveColor_Cyans_Cyan_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Cyan_Value = filterVideo_SelectiveColor_Cyans_Cyan_Value;
                // Cyans Magenta
                double filterVideo_SelectiveColor_Cyans_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Cyans_Magenta_Value"), out filterVideo_SelectiveColor_Cyans_Magenta_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Magenta_Value = filterVideo_SelectiveColor_Cyans_Magenta_Value;
                // Cyans Yellow
                double filterVideo_SelectiveColor_Cyans_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Cyans_Yellow_Value"), out filterVideo_SelectiveColor_Cyans_Yellow_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Yellow_Value = filterVideo_SelectiveColor_Cyans_Yellow_Value;

                // Blues
                // Blues Cyan
                double filterVideo_SelectiveColor_Blues_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Blues_Cyan_Value"), out filterVideo_SelectiveColor_Blues_Cyan_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Cyan_Value = filterVideo_SelectiveColor_Blues_Cyan_Value;
                // Blues Magenta
                double filterVideo_SelectiveColor_Blues_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Blues_Magenta_Value"), out filterVideo_SelectiveColor_Blues_Magenta_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Magenta_Value = filterVideo_SelectiveColor_Blues_Magenta_Value;
                // Blues Yellow
                double filterVideo_SelectiveColor_Blues_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Blues_Yellow_Value"), out filterVideo_SelectiveColor_Blues_Yellow_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Yellow_Value = filterVideo_SelectiveColor_Blues_Yellow_Value;

                // Magentas
                // Magentas Cyan
                double filterVideo_SelectiveColor_Magentas_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Magentas_Cyan_Value"), out filterVideo_SelectiveColor_Magentas_Cyan_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Cyan_Value = filterVideo_SelectiveColor_Magentas_Cyan_Value;
                // Magentas Magenta
                double filterVideo_SelectiveColor_Magentas_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Magentas_Magenta_Value"), out filterVideo_SelectiveColor_Magentas_Magenta_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Magenta_Value = filterVideo_SelectiveColor_Magentas_Magenta_Value;
                // Magentas Yellow
                double filterVideo_SelectiveColor_Magentas_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Magentas_Yellow_Value"), out filterVideo_SelectiveColor_Magentas_Yellow_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Yellow_Value = filterVideo_SelectiveColor_Magentas_Yellow_Value;

                // Whites
                // Whites Cyan
                double filterVideo_SelectiveColor_Whites_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Whites_Cyan_Value"), out filterVideo_SelectiveColor_Whites_Cyan_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Cyan_Value = filterVideo_SelectiveColor_Whites_Cyan_Value;
                // Whites Magenta
                double filterVideo_SelectiveColor_Whites_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Whites_Magenta_Value"), out filterVideo_SelectiveColor_Whites_Magenta_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Magenta_Value = filterVideo_SelectiveColor_Whites_Magenta_Value;
                // Whites Yellow
                double filterVideo_SelectiveColor_Whites_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Whites_Yellow_Value"), out filterVideo_SelectiveColor_Whites_Yellow_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Yellow_Value = filterVideo_SelectiveColor_Whites_Yellow_Value;

                // Neutrals
                // Neutrals Cyan
                double filterVideo_SelectiveColor_Neutrals_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Neutrals_Cyan_Value"), out filterVideo_SelectiveColor_Neutrals_Cyan_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Cyan_Value = filterVideo_SelectiveColor_Neutrals_Cyan_Value;
                // Neutrals Magenta
                double filterVideo_SelectiveColor_Neutrals_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Neutrals_Magenta_Value"), out filterVideo_SelectiveColor_Neutrals_Magenta_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Magenta_Value = filterVideo_SelectiveColor_Neutrals_Magenta_Value;
                // Neutrals Yellow
                double filterVideo_SelectiveColor_Neutrals_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Neutrals_Yellow_Value"), out filterVideo_SelectiveColor_Neutrals_Yellow_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Yellow_Value = filterVideo_SelectiveColor_Neutrals_Yellow_Value;

                // Blacks
                // Blacks Cyan
                double filterVideo_SelectiveColor_Blacks_Cyan_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Blacks_Cyan_Value"), out filterVideo_SelectiveColor_Blacks_Cyan_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Cyan_Value = filterVideo_SelectiveColor_Blacks_Cyan_Value;
                // Blacks Magenta
                double filterVideo_SelectiveColor_Blacks_Magenta_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Blacks_Magenta_Value"), out filterVideo_SelectiveColor_Blacks_Magenta_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Magenta_Value = filterVideo_SelectiveColor_Blacks_Magenta_Value;
                // Blacks Yellow
                double filterVideo_SelectiveColor_Blacks_Yellow_Value;
                double.TryParse(inif.Read("Filter Video", "SelectiveColor_Blacks_Yellow_Value"), out filterVideo_SelectiveColor_Blacks_Yellow_Value);
                VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Yellow_Value = filterVideo_SelectiveColor_Blacks_Yellow_Value;


                // --------------------------------------------------
                // Filter Audio
                // --------------------------------------------------
                // Lowpass
                string filterAudioLowpass = inif.Read("Filter Audio", "Lowpass_SelectedItem");

                if (VM.FilterAudioView.FilterAudio_Lowpass_Items.Contains(filterAudioLowpass))
                    VM.FilterAudioView.FilterAudio_Lowpass_SelectedItem = filterAudioLowpass;
                else
                    listFailedImports.Add("Filter Audio: Lowpass");

                // Highpass
                string filterAudioHighpass = inif.Read("Filter Audio", "Highpass_SelectedItem");

                if (VM.FilterAudioView.FilterAudio_Highpass_Items.Contains(filterAudioHighpass))
                    VM.FilterAudioView.FilterAudio_Highpass_SelectedItem = filterAudioHighpass;
                else
                    listFailedImports.Add("Filter Audio: Highpass");

                // Contrast
                double filterAudio_Contrast_Value;
                double.TryParse(inif.Read("Filter Audio", "Contrast_Value"), out filterAudio_Contrast_Value);
                VM.FilterAudioView.FilterAudio_Contrast_Value = filterAudio_Contrast_Value;

                // Extra Stereo
                double filterAudio_ExtraStereo_Value;
                double.TryParse(inif.Read("Filter Audio", "ExtraStereo_Value"), out filterAudio_ExtraStereo_Value);
                VM.FilterAudioView.FilterAudio_ExtraStereo_Value = filterAudio_ExtraStereo_Value;

                // Headphones
                string filterAudioHeadphones = inif.Read("Filter Audio", "Headphones_SelectedItem");

                if (VM.FilterAudioView.FilterAudio_Headphones_Items.Contains(filterAudioHeadphones))
                    VM.FilterAudioView.FilterAudio_Headphones_SelectedItem = filterAudioHeadphones;
                else
                    listFailedImports.Add("Filter Audio: Headphones");

                // Tempo
                double filterAudio_Tempo_Value;
                double.TryParse(inif.Read("Filter Audio", "Tempo_Value"), out filterAudio_Tempo_Value);
                VM.FilterAudioView.FilterAudio_Tempo_Value = filterAudio_Tempo_Value;


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
        /// Export Prest
        /// </summary>
        public static void ExportPreset(string profile)
        {
            // Check if Custom Presets Path is valid
            if (MainWindow.IsValidPath(VM.ConfigureView.CustomPresetsPath_Text) == false)
            {
                return;
            }

            // -------------------------
            // Check if Preset Directory exists
            // -------------------------
            if (Directory.Exists(VM.ConfigureView.CustomPresetsPath_Text))
            {
                // Start INI File Write
                Configure.INIFile inif = new Configure.INIFile(profile);

                // --------------------------------------------------
                // Main Window
                // --------------------------------------------------
                // Batch
                inif.Write("Main Window", "Batch_IsChecked", VM.MainView.Batch_IsChecked.ToString().ToLower());
                inif.Write("Main Window", "BatchExtension_Text", VM.MainView.BatchExtension_Text);


                // --------------------------------------------------
                // Format
                // --------------------------------------------------
                // Container
                inif.Write("Format", "Container_SelectedItem", VM.FormatView.Format_Container_SelectedItem);

                // MediaType
                inif.Write("Format", "MediaType_SelectedItem", VM.FormatView.Format_MediaType_SelectedItem);

                // Cut Time
                inif.Write("Format", "Cut_SelectedItem", VM.FormatView.Format_Cut_SelectedItem);
                // Start
                inif.Write("Format", "CutStart_Hours_Text", VM.FormatView.Format_CutStart_Hours_Text);
                inif.Write("Format", "CutStart_Minutes_Text", VM.FormatView.Format_CutStart_Minutes_Text);
                inif.Write("Format", "CutStart_Seconds_Text", VM.FormatView.Format_CutStart_Seconds_Text);
                inif.Write("Format", "CutStart_Milliseconds_Text", VM.FormatView.Format_CutStart_Milliseconds_Text);
                // End
                inif.Write("Format", "CutEnd_Hours_Text", VM.FormatView.Format_CutEnd_Hours_Text);
                inif.Write("Format", "CutEnd_Minutes_Text", VM.FormatView.Format_CutEnd_Minutes_Text);
                inif.Write("Format", "CutEnd_Seconds_Text", VM.FormatView.Format_CutEnd_Seconds_Text);
                inif.Write("Format", "CutEnd_Milliseconds_Text", VM.FormatView.Format_CutEnd_Milliseconds_Text);

                // Cut Frames
                inif.Write("Format", "FrameStart_Text", VM.FormatView.Format_FrameStart_Text);
                inif.Write("Format", "FrameEnd_Text", VM.FormatView.Format_FrameEnd_Text);

                // YouTube Download
                inif.Write("Format", "YouTube_SelectedItem", VM.FormatView.Format_YouTube_SelectedItem);
                inif.Write("Format", "YouTube_Quality_SelectedItem", VM.FormatView.Format_YouTube_Quality_SelectedItem);

                // --------------------------------------------------
                // Video
                // --------------------------------------------------
                // Codec
                inif.Write("Video", "Codec_SelectedItem", VM.VideoView.Video_Codec_SelectedItem);

                // Quality
                inif.Write("Video", "EncodeSpeed_SelectedItem", VM.VideoView.Video_EncodeSpeed_SelectedItem);
                inif.Write("Video", "HWAccel_SelectedItem", VM.VideoView.Video_HWAccel_SelectedItem);
                inif.Write("Video", "Quality_SelectedItem", VM.VideoView.Video_Quality_SelectedItem);
                inif.Write("Video", "Pass_SelectedItem", VM.VideoView.Video_Pass_SelectedItem);
                inif.Write("Video", "CRF_Text", VM.VideoView.Video_CRF_Text);
                inif.Write("Video", "BitRate_Text", VM.VideoView.Video_BitRate_Text);
                inif.Write("Video", "MinRate_Text", VM.VideoView.Video_MinRate_Text);
                inif.Write("Video", "MaxRate_Text", VM.VideoView.Video_MaxRate_Text);
                inif.Write("Video", "BufSize_Text", VM.VideoView.Video_BufSize_Text);
                inif.Write("Video", "VBR_IsChecked", VM.VideoView.Video_VBR_IsChecked.ToString().ToLower());
                inif.Write("Video", "PixelFormat_SelectedItem", VM.VideoView.Video_PixelFormat_SelectedItem);
                
                if (VM.VideoView.Video_FPS_IsEditable == false) // Selected
                {
                    inif.Write("Video", "FPS_IsEditable", VM.VideoView.Video_FPS_IsEditable.ToString().ToLower());
                    inif.Write("Video", "FPS_SelectedItem", VM.VideoView.Video_FPS_SelectedItem);
                }
                else if (VM.VideoView.Video_FPS_IsEditable == true) // Custom
                {
                    inif.Write("Video", "FPS_IsEditable", VM.VideoView.Video_FPS_IsEditable.ToString().ToLower());
                    inif.Write("Video", "FPS_Text", VM.VideoView.Video_FPS_Text);
                    
                }

                if (VM.VideoView.Video_Speed_IsEditable == false) // Selected
                {
                    inif.Write("Video", "Speed_IsEditable", VM.VideoView.Video_Speed_IsEditable.ToString().ToLower());
                    inif.Write("Video", "Speed_SelectedItem", VM.VideoView.Video_Speed_SelectedItem);          
                }
                else if (VM.VideoView.Video_Speed_IsEditable == true) // Custom
                {
                    inif.Write("Video", "Speed_IsEditable", VM.VideoView.Video_Speed_IsEditable.ToString().ToLower());
                    inif.Write("Video", "Speed_Text", VM.VideoView.Video_Speed_Text);
                }

                // Optimize
                inif.Write("Video", "Optimize_SelectedItem", VM.VideoView.Video_Optimize_SelectedItem);
                inif.Write("Video", "Video_Optimize_Tune_SelectedItem", VM.VideoView.Video_Video_Optimize_Tune_SelectedItem);
                inif.Write("Video", "Video_Optimize_Profile_SelectedItem", VM.VideoView.Video_Video_Optimize_Profile_SelectedItem);
                inif.Write("Video", "Optimize_Level_SelectedItem", VM.VideoView.Video_Optimize_Level_SelectedItem);

                // Size
                inif.Write("Video", "Scale_SelectedItem", VM.VideoView.Video_Scale_SelectedItem);
                inif.Write("Video", "Width_Text", VM.VideoView.Video_Width_Text);
                inif.Write("Video", "Height_Text", VM.VideoView.Video_Height_Text);
                inif.Write("Video", "ScreenFormat_SelectedItem", VM.VideoView.Video_ScreenFormat_SelectedItem);
                inif.Write("Video", "AspectRatio_SelectedItem", VM.VideoView.Video_AspectRatio_SelectedItem);
                inif.Write("Video", "ScalingAlgorithm_SelectedItem", VM.VideoView.Video_ScalingAlgorithm_SelectedItem);

                // Crop
                inif.Write("Video", "Crop_X_Text", VM.VideoView.Video_Crop_X_Text);
                inif.Write("Video", "Crop_Y_Text", VM.VideoView.Video_Crop_Y_Text);
                inif.Write("Video", "Crop_Width_Text", VM.VideoView.Video_Crop_Width_Text);
                inif.Write("Video", "Crop_Height_Text", VM.VideoView.Video_Crop_Height_Text);
                inif.Write("Video", "CropClear_Text", VM.VideoView.Video_CropClear_Text);


                // --------------------------------------------------
                // Audio
                // --------------------------------------------------
                // Codec
                inif.Write("Audio", "Codec_SelectedItem", VM.AudioView.Audio_Codec_SelectedItem);

                // Stream
                inif.Write("Audio", "Stream_SelectedItem", VM.AudioView.Audio_Stream_SelectedItem);

                // Channel
                inif.Write("Audio", "Channel_SelectedItem", VM.AudioView.Audio_Channel_SelectedItem);

                // Quality
                inif.Write("Audio", "Quality_SelectedItem", VM.AudioView.Audio_Quality_SelectedItem);
                inif.Write("Audio", "VBR_IsChecked", VM.AudioView.Audio_VBR_IsChecked.ToString().ToLower());
                inif.Write("Audio", "BitRate_Text", VM.AudioView.Audio_BitRate_Text);
                inif.Write("Audio", "CompressionLevel_SelectedItem", VM.AudioView.Audio_CompressionLevel_SelectedItem);
                inif.Write("Audio", "SampleRate_SelectedItem", VM.AudioView.Audio_SampleRate_SelectedItem);
                inif.Write("Audio", "BitDepth_SelectedItem", VM.AudioView.Audio_BitDepth_SelectedItem);

                // Filter
                inif.Write("Audio", "Volume_Text", VM.AudioView.Audio_Volume_Text);
                inif.Write("Audio", "HardLimiter_Value", VM.AudioView.Audio_HardLimiter_Value.ToString());


                // --------------------------------------------------
                // Subtitle
                // --------------------------------------------------
                // Codec
                inif.Write("Subtitle", "Codec_SelectedItem", VM.SubtitleView.Subtitle_Codec_SelectedItem);

                // Stream
                inif.Write("Subtitle", "Stream_SelectedItem", VM.SubtitleView.Subtitle_Stream_SelectedItem);


                // --------------------------------------------------
                // Filter Video
                // --------------------------------------------------
                // Fix
                inif.Write("Filter Video", "Deband_SelectedItem", VM.FilterVideoView.FilterVideo_Deband_SelectedItem);
                inif.Write("Filter Video", "Deshake_SelectedItem", VM.FilterVideoView.FilterVideo_Deshake_SelectedItem);
                inif.Write("Filter Video", "Deflicker_SelectedItem", VM.FilterVideoView.FilterVideo_Deflicker_SelectedItem);
                inif.Write("Filter Video", "Dejudder_SelectedItem", VM.FilterVideoView.FilterVideo_Dejudder_SelectedItem);
                inif.Write("Filter Video", "Denoise_SelectedItem", VM.FilterVideoView.FilterVideo_Denoise_SelectedItem);
                inif.Write("Filter Video", "Deinterlace_SelectedItem", VM.FilterVideoView.FilterVideo_Deinterlace_SelectedItem);

                // EQ
                inif.Write("Filter Video", "EQ_Brightness_Value", VM.FilterVideoView.FilterVideo_EQ_Brightness_Value.ToString());
                inif.Write("Filter Video", "EQ_Contrast_Value", VM.FilterVideoView.FilterVideo_EQ_Contrast_Value.ToString());
                inif.Write("Filter Video", "EQ_Saturation_Value", VM.FilterVideoView.FilterVideo_EQ_Saturation_Value.ToString());
                inif.Write("Filter Video", "EQ_Gamma_Value", VM.FilterVideoView.FilterVideo_EQ_Gamma_Value.ToString());

                // Selective Color
                inif.Write("Filter Video", "SelectiveColor_SelectedIndex", (VM.FilterVideoView.FilterVideo_SelectiveColor_SelectedIndex.ToString()));

                // Selective Color Method
                inif.Write("Filter Video", "SelectiveColor_Correction_Method_SelectedItem", VM.FilterVideoView.FilterVideo_SelectiveColor_Correction_Method_SelectedItem);

                // Reds
                inif.Write("Filter Video", "SelectiveColor_Reds_Cyan_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Reds_Magenta_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Reds_Yellow_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Yellow_Value.ToString());

                // Yellows
                inif.Write("Filter Video", "SelectiveColor_Yellows_Cyan_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Yellows_Magenta_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Yellows_Yellow_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Yellow_Value.ToString());

                // Greens
                inif.Write("Filter Video", "SelectiveColor_Greens_Cyan_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Greens_Magenta_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Greens_Yellow_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Yellow_Value.ToString());

                // Cyans
                inif.Write("Filter Video", "SelectiveColor_Cyans_Cyan_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Cyans_Magenta_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Cyans_Yellow_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Yellow_Value.ToString());

                // Blues
                inif.Write("Filter Video", "SelectiveColor_Blues_Cyan_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Blues_Magenta_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Blues_Yellow_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Yellow_Value.ToString());

                // Magentas
                inif.Write("Filter Video", "SelectiveColor_Magentas_Cyan_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Magentas_Magenta_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Magentas_Yellow_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Yellow_Value.ToString());

                // Whites
                inif.Write("Filter Video", "SelectiveColor_Whites_Cyan_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Whites_Magenta_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Whites_Yellow_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Yellow_Value.ToString());

                // Neutrals
                inif.Write("Filter Video", "SelectiveColor_Neutrals_Cyan_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Neutrals_Magenta_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Neutrals_Yellow_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Yellow_Value.ToString());

                // Blacks
                inif.Write("Filter Video", "SelectiveColor_Blacks_Cyan_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Cyan_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Blacks_Magenta_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Magenta_Value.ToString());
                inif.Write("Filter Video", "SelectiveColor_Blacks_Yellow_Value", VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Yellow_Value.ToString());

                // --------------------------------------------------
                // Filter Audio
                // --------------------------------------------------
                // Lowpass
                inif.Write("Filter Audio", "Lowpass_SelectedItem", VM.FilterAudioView.FilterAudio_Lowpass_SelectedItem);

                // Highpass
                inif.Write("Filter Audio", "Highpass_SelectedItem", VM.FilterAudioView.FilterAudio_Highpass_SelectedItem);

                // Contrast
                inif.Write("Filter Audio", "Contrast_Value", VM.FilterAudioView.FilterAudio_Contrast_Value.ToString());

                // Extra Stereo
                inif.Write("Filter Audio", "ExtraStereo_Value", VM.FilterAudioView.FilterAudio_ExtraStereo_Value.ToString());

                // Headphones
                inif.Write("Filter Audio", "Headphones_SelectedItem", VM.FilterAudioView.FilterAudio_Headphones_SelectedItem);

                // Tempo
                inif.Write("Filter Audio", "Tempo_Value", VM.FilterAudioView.FilterAudio_Tempo_Value.ToString());
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
                            Directory.CreateDirectory(VM.ConfigureView.CustomPresetsPath_Text);
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
        /// Failed Import Window Open
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
