/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017, 2018 Matt McManis
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


// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class Presets
    {
        public static void Preset(MainWindow mainwindow)
        {
            // -------------------------
            // Custom
            // -------------------------
            // Custom ComboBox Editable
            if ((string)mainwindow.cboPreset.SelectedItem == "Custom")
            {
                mainwindow.cboPreset.IsEditable = true;
            }
            // Maintain Editable Combobox while typing
            if (mainwindow.cboPreset.IsEditable == true)
            {
                mainwindow.cboPreset.IsEditable = true;

                // Clear Custom Text
                mainwindow.cboPreset.Text = string.Empty;
            }

            // -------------------------
            // Default
            // -------------------------
            if ((string)mainwindow.cboPreset.SelectedItem == "Preset"
                || (string)mainwindow.cboPreset.SelectedItem == "Default")
            {
                // -------------------------
                // Default Video
                // -------------------------
                if ((string)mainwindow.cboFormat.SelectedItem == "webm"
                    || (string)mainwindow.cboFormat.SelectedItem == "mp4"
                    || (string)mainwindow.cboFormat.SelectedItem == "mkv"
                    || (string)mainwindow.cboFormat.SelectedItem == "m2v"
                    || (string)mainwindow.cboFormat.SelectedItem == "mpg"
                    || (string)mainwindow.cboFormat.SelectedItem == "avi"
                    || (string)mainwindow.cboFormat.SelectedItem == "ogv"
                    || (string)mainwindow.cboFormat.SelectedItem == "jpg"
                    || (string)mainwindow.cboFormat.SelectedItem == "png"
                    || (string)mainwindow.cboFormat.SelectedItem == "webp")
                {
                    mainwindow.cboPreset.IsEditable = false;

                    // Format
                    mainwindow.cboFormat.SelectedItem = "webm";

                    // Video
                    mainwindow.cboFormat.SelectedItem = mainwindow.cboFormat;
                    mainwindow.cboVideoQuality.SelectedItem = "Auto";
                    mainwindow.vBitrateCustom.Text = "";
                    mainwindow.vMinrateCustom.Text = "";
                    mainwindow.vMaxrateCustom.Text = "";
                    mainwindow.vBufsizeCustom.Text = "";
                    mainwindow.cboPass.SelectedItem = "CRF";
                    mainwindow.cboSize.SelectedItem = "Source";
                    mainwindow.cboScaling.SelectedItem = "default";
                    mainwindow.cboCut.SelectedItem = "No";
                    mainwindow.cutStart.Text = "00:00:00.000";
                    mainwindow.cutEnd.Text = "00:00:00.000";
                    mainwindow.cboSpeed.SelectedItem = "Medium";
                    mainwindow.cboFPS.SelectedItem = "auto";
                    mainwindow.cboFPS.IsEnabled = true;

                    // Subtitle
                    mainwindow.cboSubtitleCodec.SelectedItem = "None";
                    mainwindow.cboSubtitlesStream.SelectedItem = "none";

                    // Audio
                    mainwindow.cboAudioQuality.SelectedItem = "Auto";
                    mainwindow.cboChannel.SelectedItem = "Source";
                    mainwindow.cboSamplerate.SelectedItem = "auto";
                    mainwindow.cboBitDepth.SelectedItem = "auto";
                    mainwindow.cboAudioStream.SelectedItem = "all";
                    mainwindow.volumeUpDown.Text = "100";
                    mainwindow.slAudioLimiter.Value = 1;
                    ////mainwindow.tglAudioLimiter.IsChecked = false;
                    ////mainwindow.audioLimiter.Text = string.Empty;

                    // special rules for webm
                    if ((string)mainwindow.cboFormat.SelectedItem == "webm") 
                    {
                        mainwindow.cboSubtitlesStream.SelectedItem = "none";
                        mainwindow.cboAudioStream.SelectedItem = "1";
                        //mainwindow.cboOptimize.SelectedItem = "Web";
                    }
                    else
                    {
                        mainwindow.cboOptimize.SelectedItem = "None";
                    }

                }

                // -------------------------
                // Default Audio
                // -------------------------
                else if ((string)mainwindow.cboFormat.SelectedItem == "m4a"
                    || (string)mainwindow.cboFormat.SelectedItem == "mp3"
                    || (string)mainwindow.cboFormat.SelectedItem == "ogg"
                    || (string)mainwindow.cboFormat.SelectedItem == "flac"
                    || (string)mainwindow.cboFormat.SelectedItem == "wav")
                {
                    mainwindow.cboPreset.IsEditable = false;

                    // Video
                    mainwindow.cboFormat.SelectedItem = mainwindow.cboFormat;
                    mainwindow.cboVideoQuality.SelectedItem = "None";
                    mainwindow.vBitrateCustom.Text = "";
                    mainwindow.vMinrateCustom.Text = "";
                    mainwindow.vMaxrateCustom.Text = "";
                    mainwindow.vBufsizeCustom.Text = "";
                    mainwindow.cboSize.SelectedItem = "Source";
                    mainwindow.cboScaling.SelectedItem = "default";
                    mainwindow.cboCut.SelectedItem = "No";
                    mainwindow.cutStart.Text = "00:00:00.000";
                    mainwindow.cutEnd.Text = "00:00:00.000";
                    mainwindow.cboSpeed.SelectedItem = "Medium";
                    mainwindow.cboFPS.SelectedItem = "auto";
                    mainwindow.cboFPS.IsEnabled = false;
                    mainwindow.cboOptimize.SelectedItem = "None";

                    // Subtitle
                    mainwindow.cboSubtitleCodec.SelectedItem = "None";
                    mainwindow.cboSubtitlesStream.SelectedItem = "none";

                    // Audio
                    mainwindow.cboAudioQuality.SelectedItem = "Auto";
                    mainwindow.cboChannel.SelectedItem = "Source";
                    mainwindow.cboSamplerate.SelectedItem = "auto";
                    // special rules for PCM codec
                    if ((string)mainwindow.cboAudioCodec.SelectedItem == "PCM")
                    {
                        mainwindow.cboBitDepth.SelectedItem = "24";
                    } 
                    else {
                        mainwindow.cboBitDepth.SelectedItem = "auto";
                    }

                    mainwindow.cboAudioStream.SelectedItem = "1";
                    mainwindow.volumeUpDown.Text = "100";
                    mainwindow.slAudioLimiter.Value = 1;
                    //mainwindow.tglAudioLimiter.IsChecked = false;
                    //mainwindow.audioLimiter.Text = string.Empty;
                }
            }

            // -------------------------
            // DVD
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "DVD")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "mpg";
                mainwindow.cboVideoCodec.SelectedItem = "MPEG-2";
                mainwindow.cboAudioCodec.SelectedItem = "AC3";

                // Video
                mainwindow.cboVideoQuality.SelectedItem = "Custom";
                mainwindow.cboPass.SelectedItem = "2 Pass";
                mainwindow.vBitrateCustom.Text = "3M";
                mainwindow.vMinrateCustom.Text = "";
                mainwindow.vMaxrateCustom.Text = "9.8M";
                mainwindow.vBufsizeCustom.Text = "9.8M";
                mainwindow.cboSize.SelectedItem = "Custom";
                mainwindow.cboScaling.SelectedItem = "default";
                mainwindow.widthCustom.Text = "720";
                mainwindow.heightCustom.Text = "-2";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboFPS.SelectedItem = "ntsc";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.SelectedItem = "None";

                // Subtitle
                mainwindow.cboSubtitleCodec.SelectedItem = "SRT";
                mainwindow.cboSubtitlesStream.SelectedItem = "all";

                // Audio
                mainwindow.cboAudioQuality.SelectedItem = "320";
                mainwindow.tglAudioVBR.IsChecked = false;
                mainwindow.cboChannel.SelectedItem = "Source";
                mainwindow.cboSamplerate.SelectedItem = "44.1k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.slAudioLimiter.Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // HD Video
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "HD Video")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "mp4";
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboAudioCodec.SelectedItem = "AAC";

                // Video
                mainwindow.cboVideoQuality.SelectedItem = "Ultra";
                mainwindow.vBitrateCustom.Text = "";
                mainwindow.vMinrateCustom.Text = "";
                mainwindow.vMaxrateCustom.Text = "";
                mainwindow.vBufsizeCustom.Text = "";
                mainwindow.cboSize.SelectedItem = "Source";
                mainwindow.cboScaling.SelectedItem = "default";
                mainwindow.cboOptimize.SelectedItem = "PC HD";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";

                // Subtitle
                mainwindow.cboSubtitleCodec.SelectedItem = "mov_text";
                mainwindow.cboSubtitlesStream.SelectedItem = "all";

                // Audio
                mainwindow.cboAudioQuality.SelectedItem = "Auto";
                mainwindow.tglAudioVBR.IsChecked = false;
                mainwindow.cboChannel.SelectedItem = "Source";
                mainwindow.cboSamplerate.SelectedItem = "auto";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.slAudioLimiter.Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // SD Video
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "SD Video")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "mp4";
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboAudioCodec.SelectedItem = "AC3";


                // Video
                mainwindow.cboVideoQuality.SelectedItem = "High";
                mainwindow.vBitrateCustom.Text = "";
                mainwindow.vMinrateCustom.Text = "";
                mainwindow.vMaxrateCustom.Text = "";
                mainwindow.vBufsizeCustom.Text = "";
                mainwindow.cboSize.SelectedItem = "Source";
                mainwindow.cboScaling.SelectedItem = "default";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboOptimize.SelectedItem = "PC SD";

                // Subtitle
                mainwindow.cboSubtitleCodec.SelectedItem = "mov_text";
                mainwindow.cboSubtitlesStream.SelectedItem = "all";

                // Audio
                mainwindow.cboAudioQuality.SelectedItem = "256";
                mainwindow.tglAudioVBR.IsChecked = false;
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "44.1k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.slAudioLimiter.Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // Blu-ray
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "Blu-ray")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "mp4";
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboAudioCodec.SelectedItem = "AC3";

                // Video
                mainwindow.cboVideoQuality.SelectedItem = "Ultra";
                mainwindow.vBitrateCustom.Text = "";
                mainwindow.vMinrateCustom.Text = "";
                mainwindow.vMaxrateCustom.Text = "";
                mainwindow.vBufsizeCustom.Text = "";
                mainwindow.cboSize.SelectedItem = "1080p";
                mainwindow.cboScaling.SelectedItem = "default";
                mainwindow.cboFPS.SelectedItem = "23.976";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.SelectedItem = "Blu-ray";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";

                // Subtitle
                mainwindow.cboSubtitleCodec.SelectedItem = "mov_text";
                mainwindow.cboSubtitlesStream.SelectedItem = "all";

                // Audio
                mainwindow.cboAudioQuality.SelectedItem = "640";
                mainwindow.tglAudioVBR.IsChecked = false;
                mainwindow.cboChannel.SelectedItem = "Source";
                mainwindow.cboSamplerate.SelectedItem = "48k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.slAudioLimiter.Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // Windows Phone
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "Win Phone")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "mp4";
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboAudioCodec.SelectedItem = "AAC";

                // Video
                mainwindow.cboVideoQuality.SelectedItem = "High";
                mainwindow.vBitrateCustom.Text = "";
                mainwindow.vMinrateCustom.Text = "";
                mainwindow.vMaxrateCustom.Text = "";
                mainwindow.vBufsizeCustom.Text = "";
                mainwindow.cboSize.SelectedItem = "Source";
                mainwindow.cboScaling.SelectedItem = "default";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                //cboTune.SelectedItem = "none";
                mainwindow.cboFPS.SelectedItem = "23.976";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboSize.SelectedItem = "Source";
                mainwindow.cboOptimize.SelectedItem = "Windows";

                // Subtitle
                mainwindow.cboSubtitleCodec.SelectedItem = "mov_text";
                mainwindow.cboSubtitlesStream.SelectedItem = "all";

                // Audio
                mainwindow.cboAudioQuality.SelectedItem = "400";
                mainwindow.tglAudioVBR.IsChecked = true;
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "44.1k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.slAudioLimiter.Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // iOS
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "iOS")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "mp4";
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboAudioCodec.SelectedItem = "AAC";

                // Video
                mainwindow.cboVideoQuality.SelectedItem = "High";
                mainwindow.vBitrateCustom.Text = "";
                mainwindow.vMinrateCustom.Text = "";
                mainwindow.vMaxrateCustom.Text = "";
                mainwindow.vBufsizeCustom.Text = "";
                mainwindow.cboSize.SelectedItem = "Source";
                mainwindow.cboScaling.SelectedItem = "default";
                mainwindow.cboFPS.SelectedItem = "23.976";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboOptimize.SelectedItem = "Apple";

                // Subtitle
                mainwindow.cboSubtitleCodec.SelectedItem = "mov_text";
                mainwindow.cboSubtitlesStream.SelectedItem = "all";

                // Audio
                mainwindow.cboAudioQuality.SelectedItem = "400";
                mainwindow.tglAudioVBR.IsChecked = true;
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "44.1k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.slAudioLimiter.Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // Android
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "Android")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "mp4";
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboAudioCodec.SelectedItem = "AAC";

                // Video
                mainwindow.cboVideoQuality.SelectedItem = "High";
                mainwindow.vBitrateCustom.Text = "";
                mainwindow.vMinrateCustom.Text = "";
                mainwindow.vMaxrateCustom.Text = "";
                mainwindow.vBufsizeCustom.Text = "";
                mainwindow.cboSize.SelectedItem = "Source";
                mainwindow.cboScaling.SelectedItem = "default";
                mainwindow.cboOptimize.SelectedItem = "Android";
                mainwindow.cboFPS.SelectedItem = "23.976";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";

                // Subtitle
                mainwindow.cboSubtitleCodec.SelectedItem = "mov_text";
                mainwindow.cboSubtitlesStream.SelectedItem = "all";

                // Audio
                mainwindow.cboAudioQuality.SelectedItem = "400";
                mainwindow.tglAudioVBR.IsChecked = true;
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "44.1k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.slAudioLimiter.Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // iTunes
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "iTunes")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "m4a";
                mainwindow.cboAudioCodec.SelectedItem = "AAC";

                // Video
                mainwindow.vBitrateCustom.Text = "";
                mainwindow.vMinrateCustom.Text = "";
                mainwindow.vMaxrateCustom.Text = "";
                mainwindow.vBufsizeCustom.Text = "";
                mainwindow.cboSize.SelectedItem = "Source";
                mainwindow.cboScaling.SelectedItem = "default";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = false;
                mainwindow.cboOptimize.SelectedItem = "None";

                // Subtitle
                mainwindow.cboSubtitleCodec.SelectedItem = "None";
                mainwindow.cboSubtitlesStream.SelectedItem = "none";

                // Audio
                mainwindow.cboAudioQuality.SelectedItem = "320";
                mainwindow.tglAudioVBR.IsChecked = true;
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "auto";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.slAudioLimiter.Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // MP3 HQ
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "MP3 HQ")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "mp3";
                mainwindow.cboAudioCodec.SelectedItem = "LAME";

                // Video
                mainwindow.vBitrateCustom.Text = "";
                mainwindow.vMinrateCustom.Text = "";
                mainwindow.vMaxrateCustom.Text = "";
                mainwindow.vBufsizeCustom.Text = "";
                mainwindow.cboSize.SelectedItem = "Source";
                mainwindow.cboScaling.SelectedItem = "default";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = false;
                mainwindow.cboOptimize.SelectedItem = "None";

                // Subtitle
                mainwindow.cboSubtitleCodec.SelectedItem = "None";
                mainwindow.cboSubtitlesStream.SelectedItem = "none";

                // Audio
                mainwindow.cboAudioQuality.SelectedItem = "320";
                mainwindow.tglAudioVBR.IsChecked = true;
                mainwindow.cboChannel.SelectedItem = "Joint Stereo";
                mainwindow.cboSamplerate.SelectedItem = "auto";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.slAudioLimiter.Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // PS3
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "PS3")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "mp4";
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboAudioCodec.SelectedItem = "AAC";

                // Video
                mainwindow.cboVideoQuality.SelectedItem = "Ultra";
                mainwindow.vBitrateCustom.Text = "";
                mainwindow.vMinrateCustom.Text = "";
                mainwindow.vMaxrateCustom.Text = "";
                mainwindow.vBufsizeCustom.Text = "";
                mainwindow.cboSize.SelectedItem = "Source";
                mainwindow.cboScaling.SelectedItem = "default";
                mainwindow.cboFPS.SelectedItem = "23.976";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboOptimize.SelectedItem = "PS3";

                // Subtitle
                mainwindow.cboSubtitleCodec.SelectedItem = "mov_text";
                mainwindow.cboSubtitlesStream.SelectedItem = "all";

                // Audio
                mainwindow.cboAudioQuality.SelectedItem = "400";
                mainwindow.tglAudioVBR.IsChecked = false;
                mainwindow.cboChannel.SelectedItem = "Source";
                mainwindow.cboSamplerate.SelectedItem = "48k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.slAudioLimiter.Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // PS4
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "PS4")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "mp4";
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboAudioCodec.SelectedItem = "AAC";

                // Video
                mainwindow.cboVideoQuality.SelectedItem = "Ultra";
                mainwindow.vBitrateCustom.Text = "";
                mainwindow.vMinrateCustom.Text = "";
                mainwindow.vMaxrateCustom.Text = "";
                mainwindow.vBufsizeCustom.Text = "";
                mainwindow.cboSize.SelectedItem = "Source";
                mainwindow.cboScaling.SelectedItem = "default";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.SelectedItem = "PS4";

                // Subtitle
                mainwindow.cboSubtitleCodec.SelectedItem = "mov_text";
                mainwindow.cboSubtitlesStream.SelectedItem = "all";

                // Audio
                mainwindow.cboAudioQuality.SelectedItem = "Auto";
                mainwindow.cboChannel.SelectedItem = "Source";
                mainwindow.cboSamplerate.SelectedItem = "auto";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.tglAudioVBR.IsChecked = false;
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.slAudioLimiter.Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // Xbox 360
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "Xbox 360")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "mp4";
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboAudioCodec.SelectedItem = "AAC";

                // Video
                mainwindow.cboVideoQuality.SelectedItem = "High";
                mainwindow.vBitrateCustom.Text = "";
                mainwindow.vMinrateCustom.Text = "";
                mainwindow.vMaxrateCustom.Text = "";
                mainwindow.vBufsizeCustom.Text = "";
                mainwindow.cboSize.SelectedItem = "Source";
                mainwindow.cboScaling.SelectedItem = "default";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboFPS.SelectedItem = "23.976";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.SelectedItem = "Xbox 360";

                // Subtitle
                mainwindow.cboSubtitleCodec.SelectedItem = "mov_text";
                mainwindow.cboSubtitlesStream.SelectedItem = "all";

                // Audio
                mainwindow.cboAudioQuality.SelectedItem = "320";
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "48k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.tglAudioVBR.IsChecked = false;
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.slAudioLimiter.Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // Xbox One
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "Xbox One")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "mp4";
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboAudioCodec.SelectedItem = "AAC";

                // Video
                mainwindow.cboVideoQuality.SelectedItem = "Ultra";
                mainwindow.vBitrateCustom.Text = "";
                mainwindow.vMinrateCustom.Text = "";
                mainwindow.vMaxrateCustom.Text = "";
                mainwindow.vBufsizeCustom.Text = "";
                mainwindow.cboSize.SelectedItem = "Source";
                mainwindow.cboScaling.SelectedItem = "default";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboOptimize.SelectedItem = "Xbox One";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = true;

                // Subtitle
                mainwindow.cboSubtitleCodec.SelectedItem = "mov_text";
                mainwindow.cboSubtitlesStream.SelectedItem = "all";

                // Audio
                mainwindow.cboAudioQuality.SelectedItem = "Auto";
                mainwindow.cboChannel.SelectedItem = "Source";
                mainwindow.cboSamplerate.SelectedItem = "auto";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.tglAudioVBR.IsChecked = false;
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.slAudioLimiter.Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // HTML5
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "HTML5")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "webm";
                mainwindow.cboVideoCodec.SelectedItem = "VP8";
                mainwindow.cboAudioCodec.SelectedItem = "Vorbis";

                // Video
                mainwindow.cboVideoQuality.SelectedItem = "Medium";
                //mainwindow.vBitrateCustom.Text = ""; // use quality preset bitrate
                mainwindow.vMinrateCustom.Text = "";
                mainwindow.vMaxrateCustom.Text = "";
                mainwindow.vBufsizeCustom.Text = "";
                mainwindow.cboSize.SelectedItem = "Source";
                mainwindow.cboScaling.SelectedItem = "defualt";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.SelectedItem = "Web";

                // Subtitle
                mainwindow.cboSubtitleCodec.SelectedItem = "None";
                mainwindow.cboSubtitlesStream.SelectedItem = "none";

                // Audio
                mainwindow.cboAudioQuality.SelectedItem = "192";
                mainwindow.tglAudioVBR.IsChecked = true;
                mainwindow.cboAudioStream.SelectedItem = "1";
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "44.1k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.slAudioLimiter.Value = 1;
                //mainwindow.tglAudioLimiter.IsChecked = false;
                //mainwindow.audioLimiter.Text = string.Empty;
            }

            // -------------------------
            // Debug
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "Debug")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "mkv";
                mainwindow.cboAudioCodec.SelectedItem = "Opus";

                // Video
                mainwindow.cboVideoQuality.SelectedItem = "Custom";
                mainwindow.vBitrateCustom.Text = "1250K";
                mainwindow.vMinrateCustom.Text = "";
                mainwindow.vMaxrateCustom.Text = "";
                mainwindow.vBufsizeCustom.Text = "";
                mainwindow.crfCustom.Text = "26";
                mainwindow.cboFPS.SelectedItem = "29.97";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboSize.SelectedItem = "Custom";
                mainwindow.cboScaling.SelectedItem = "spline";
                mainwindow.widthCustom.Text = "545";
                mainwindow.heightCustom.Text = "307";
                mainwindow.cboCut.SelectedItem = "Yes";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:05.300";
                mainwindow.cboSubtitlesStream.SelectedItem = "all";
                mainwindow.cboOptimize.SelectedItem = "Windows";
                mainwindow.cboSpeed.SelectedItem = "Faster";

                // Subtitle
                mainwindow.cboSubtitleCodec.SelectedItem = "SSA";
                mainwindow.cboSubtitlesStream.SelectedItem = "all";

                // Audio
                mainwindow.cboAudioStream.SelectedItem = "1";
                mainwindow.cboAudioQuality.SelectedItem = "Custom";
                mainwindow.audioCustom.Text = "380";
                mainwindow.tglAudioVBR.IsChecked = true;
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "48k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "120";
                mainwindow.slAudioLimiter.Value = 0.9;
                //mainwindow.tglAudioLimiter.IsChecked = true;
                //mainwindow.audioLimiter.Text = "0.90";
            }
        }
    }
}
