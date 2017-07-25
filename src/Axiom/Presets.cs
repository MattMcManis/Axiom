// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class Presets
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
            if ((string)mainwindow.cboPreset.SelectedItem == "Preset")
            {
                // -------------------------
                // Default Video
                // -------------------------
                if ((string)mainwindow.cboFormat.SelectedItem == "webm"
                    || (string)mainwindow.cboFormat.SelectedItem == "mp4"
                    || (string)mainwindow.cboFormat.SelectedItem == "mkv"
                    || (string)mainwindow.cboFormat.SelectedItem == "ogv"
                    || (string)mainwindow.cboFormat.SelectedItem == "jpg"
                    || (string)mainwindow.cboFormat.SelectedItem == "png")
                {
                    mainwindow.cboPreset.IsEditable = false;

                    // Video
                    mainwindow.cboFormat.SelectedItem = mainwindow.cboFormat;
                    mainwindow.cboVideo.SelectedItem = "Auto";
                    mainwindow.cboSize.SelectedItem = "No";
                    mainwindow.cboCut.SelectedItem = "No";
                    mainwindow.cutStart.Text = "00:00:00.000";
                    mainwindow.cutEnd.Text = "00:00:00.000";
                    mainwindow.cboSpeed.SelectedItem = "Medium";
                    mainwindow.cboFPS.SelectedItem = "auto";
                    mainwindow.cboFPS.IsEnabled = true;
                    mainwindow.cboSubtitle.SelectedItem = "all";
                    // Audio
                    mainwindow.cboAudio.SelectedItem = "Auto";
                    mainwindow.cboChannel.SelectedItem = "Source";
                    mainwindow.cboSamplerate.SelectedItem = "auto";
                    mainwindow.cboBitDepth.SelectedItem = "auto";
                    mainwindow.cboAudioStream.SelectedItem = "all";
                    mainwindow.volumeUpDown.Text = "100";
                    mainwindow.tglAudioLimiter.IsChecked = false;
                    mainwindow.audioLimiter.Text = string.Empty;

                    // special rules for webm
                    if ((string)mainwindow.cboFormat.SelectedItem == "webm") 
                    {
                        mainwindow.cboSubtitle.SelectedItem = "none";
                        mainwindow.cboAudioStream.SelectedItem = "1";
                        //mainwindow.cboOptimize.SelectedItem = "Web";
                    }
                    else
                    {
                        mainwindow.cboOptimize.SelectedItem = "none";
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
                    mainwindow.cboVideo.SelectedItem = "None";
                    mainwindow.cboSize.SelectedItem = "No";
                    mainwindow.cboCut.SelectedItem = "No";
                    mainwindow.cutStart.Text = "00:00:00.000";
                    mainwindow.cutEnd.Text = "00:00:00.000";
                    mainwindow.cboSpeed.SelectedItem = "Medium";
                    mainwindow.cboFPS.SelectedItem = "auto";
                    mainwindow.cboFPS.IsEnabled = false;
                    mainwindow.cboSubtitle.SelectedItem = "none";
                    mainwindow.cboOptimize.SelectedItem = "none";

                    // Audio
                    mainwindow.cboAudio.SelectedItem = "Auto";
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
                    mainwindow.tglAudioLimiter.IsChecked = false;
                    mainwindow.audioLimiter.Text = string.Empty;
                }
            }

            // -------------------------
            // DVD
            // -------------------------
            else if ((string)mainwindow.cboPreset.SelectedItem == "DVD")
            {
                mainwindow.cboPreset.IsEditable = false;

                // Format
                mainwindow.cboFormat.SelectedItem = "mp4";
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboAudioCodec.SelectedItem = "AC3";

                // Video
                mainwindow.cboVideo.SelectedItem = "High";
                mainwindow.cboSize.SelectedItem = "Custom";
                mainwindow.widthCustom.Text = "720";
                mainwindow.heightCustom.Text = "480";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboFPS.SelectedItem = "23.976";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboOptimize.SelectedItem = "DVD";

                // Audio
                mainwindow.cboAudio.SelectedItem = "320";
                mainwindow.tglVBR.IsChecked = false;
                mainwindow.cboChannel.SelectedItem = "Source";
                mainwindow.cboSamplerate.SelectedItem = "44.1k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.tglAudioLimiter.IsChecked = false;
                mainwindow.audioLimiter.Text = string.Empty;
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
                mainwindow.cboVideo.SelectedItem = "Ultra";
                mainwindow.cboSize.SelectedItem = "No";
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboOptimize.SelectedItem = "none";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";

                // Audio
                mainwindow.cboAudio.SelectedItem = "400";
                mainwindow.tglVBR.IsChecked = false;
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "48k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.tglAudioLimiter.IsChecked = false;
                mainwindow.audioLimiter.Text = string.Empty;
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
                mainwindow.cboVideo.SelectedItem = "High";
                mainwindow.cboSize.SelectedItem = "No";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboOptimize.SelectedItem = "none";

                // Audio
                mainwindow.cboAudio.SelectedItem = "256";
                mainwindow.tglVBR.IsChecked = false;
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "44.1k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.tglAudioLimiter.IsChecked = false;
                mainwindow.audioLimiter.Text = string.Empty;
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
                mainwindow.cboSize.SelectedItem = "1080p";
                mainwindow.cboVideo.SelectedItem = "Ultra";
                mainwindow.cboFPS.SelectedItem = "23.976";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboOptimize.SelectedItem = "Blu-ray";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";

                // Audio
                mainwindow.cboAudio.SelectedItem = "640";
                mainwindow.tglVBR.IsChecked = false;
                mainwindow.cboChannel.SelectedItem = "Source";
                mainwindow.cboSamplerate.SelectedItem = "48k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.tglAudioLimiter.IsChecked = false;
                mainwindow.audioLimiter.Text = string.Empty;
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
                mainwindow.cboVideo.SelectedItem = "High";

                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                //cboTune.SelectedItem = "none";
                mainwindow.cboFPS.SelectedItem = "23.976";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboSize.SelectedItem = "No";
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboOptimize.SelectedItem = "Windows";

                // Audio
                mainwindow.cboAudio.SelectedItem = "400";
                mainwindow.tglVBR.IsChecked = true;
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "44.1k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.tglAudioLimiter.IsChecked = false;
                mainwindow.audioLimiter.Text = string.Empty;
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
                mainwindow.cboVideo.SelectedItem = "High";
                mainwindow.cboSize.SelectedItem = "No";
                mainwindow.cboFPS.SelectedItem = "23.976";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboOptimize.SelectedItem = "Apple";

                // Audio
                mainwindow.cboAudio.SelectedItem = "400";
                mainwindow.tglVBR.IsChecked = true;
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "44.1k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.tglAudioLimiter.IsChecked = false;
                mainwindow.audioLimiter.Text = string.Empty;
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
                mainwindow.cboVideo.SelectedItem = "High";
                mainwindow.cboSize.SelectedItem = "No";
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboOptimize.SelectedItem = "Android";
                mainwindow.cboFPS.SelectedItem = "23.976";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";

                // Audio
                mainwindow.cboAudio.SelectedItem = "400";
                mainwindow.tglVBR.IsChecked = true;
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "44.1k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.tglAudioLimiter.IsChecked = false;
                mainwindow.audioLimiter.Text = string.Empty;
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
                mainwindow.cboVideo.SelectedItem = "Ultra";
                mainwindow.cboSize.SelectedItem = "No";
                mainwindow.cboFPS.SelectedItem = "23.976";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboOptimize.SelectedItem = "PS3";

                // Audio
                mainwindow.cboAudio.SelectedItem = "400";
                mainwindow.tglVBR.IsChecked = false;
                mainwindow.cboChannel.SelectedItem = "Source";
                mainwindow.cboSamplerate.SelectedItem = "48k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.tglAudioLimiter.IsChecked = false;
                mainwindow.audioLimiter.Text = string.Empty;
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
                mainwindow.cboVideo.SelectedItem = "Ultra";
                mainwindow.cboSize.SelectedItem = "No";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboOptimize.SelectedItem = "PS4";

                // Audio
                mainwindow.cboAudio.SelectedItem = "400";
                mainwindow.cboChannel.SelectedItem = "Source";
                mainwindow.cboSamplerate.SelectedItem = "auto";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.tglVBR.IsChecked = false;
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.tglAudioLimiter.IsChecked = false;
                mainwindow.audioLimiter.Text = string.Empty;
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
                mainwindow.cboVideo.SelectedItem = "High";
                mainwindow.cboSize.SelectedItem = "No";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboFPS.SelectedItem = "23.976";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboOptimize.SelectedItem = "Xbox 360";

                // Audio
                mainwindow.cboAudio.SelectedItem = "320";
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "48k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.tglVBR.IsChecked = false;
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.tglAudioLimiter.IsChecked = false;
                mainwindow.audioLimiter.Text = string.Empty;
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
                mainwindow.cboVideo.SelectedItem = "Ultra";
                mainwindow.cboSize.SelectedItem = "No";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboOptimize.SelectedItem = "Xbox One";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = true;

                // Audio
                mainwindow.cboAudio.SelectedItem = "400";
                mainwindow.cboChannel.SelectedItem = "Source";
                mainwindow.cboSamplerate.SelectedItem = "auto";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.tglVBR.IsChecked = false;
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.tglAudioLimiter.IsChecked = false;
                mainwindow.audioLimiter.Text = string.Empty;
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
                mainwindow.cboVideo.SelectedItem = "Medium";
                mainwindow.cboSize.SelectedItem = "No";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboOptimize.SelectedItem = "Web";

                // Audio
                mainwindow.cboAudio.SelectedItem = "192";
                mainwindow.tglVBR.IsChecked = true;
                mainwindow.cboAudioStream.SelectedItem = "1";
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "44.1k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.tglAudioLimiter.IsChecked = false;
                mainwindow.audioLimiter.Text = string.Empty;
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
                mainwindow.cboSize.SelectedItem = "No";
                mainwindow.cboCut.SelectedItem = "No";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = false;
                mainwindow.cboOptimize.SelectedItem = "optimize";
                mainwindow.cboSubtitle.SelectedItem = "none";

                // Audio
                mainwindow.cboAudio.SelectedItem = "320";
                mainwindow.tglVBR.IsChecked = true;
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "44.1k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "100";
                mainwindow.tglAudioLimiter.IsChecked = false;
                mainwindow.audioLimiter.Text = string.Empty;
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
                mainwindow.cboVideo.SelectedItem = "Custom";
                mainwindow.vBitrateCustom.Text = "1250K";
                mainwindow.crfCustom.Text = "26";
                mainwindow.cboFPS.SelectedItem = "24";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboSize.SelectedItem = "Custom";
                mainwindow.widthCustom.Text = "545";
                mainwindow.heightCustom.Text = "307";
                mainwindow.cboCut.SelectedItem = "Yes";
                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:05.300";
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboOptimize.SelectedItem = "Windows";
                mainwindow.cboSpeed.SelectedItem = "Faster";

                // Audio
                mainwindow.cboAudioStream.SelectedItem = "1";
                mainwindow.cboAudio.SelectedItem = "Custom";
                mainwindow.audioCustom.Text = "380";
                mainwindow.tglVBR.IsChecked = true;
                mainwindow.cboChannel.SelectedItem = "Stereo";
                mainwindow.cboSamplerate.SelectedItem = "48k";
                mainwindow.cboBitDepth.SelectedItem = "auto";
                mainwindow.volumeUpDown.Text = "120";
                mainwindow.tglAudioLimiter.IsChecked = true;
                mainwindow.audioLimiter.Text = "0.90";
            }
        }
    }
}
