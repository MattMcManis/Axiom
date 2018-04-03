/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017, 2018 Matt McManis
http://github.com/MattMcManis/Axiom
http://axiomui.github.io
axiom.interface@gmail.com

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
    public partial class FormatControls
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ComboBoxes Item Sources
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // Load in Intialize Component
        //
        // MediaType
        public static List<string> MediaTypeItemSource = new List<string>() { "Video", "Audio", "Image", "Sequence" };

        // Format
        public static List<string> FormatItemSource = new List<string>() { "webm", "mp4", "mkv", "avi", "ogv", "mp3", "m4a", "ogg", "flac", "wav", "jpg", "png" };

        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Control Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Get Output Extension (Method)
        /// </summary>
        public static void OutputFormatExt(MainWindow mainwindow)
        {
            // Output Extension is Format ComboBox Selected Item
            MainWindow.outputExt = "." + mainwindow.cboFormat.SelectedItem.ToString();
        }


        /// <summary>
        /// File Output Format Defaults (Method)
        /// </summary>
        // Output Control Selections
        public static void OuputFormatDefaults(MainWindow mainwindow)
        {
            // Get Output Extension (Method)
            //Format.GetOutputExt(mainwindow);

            // -------------------------
            // Output Format Container Rules
            // -------------------------
            if ((string)mainwindow.cboFormat.SelectedItem == "webm")
            {
                mainwindow.cboMediaType.SelectedItem = "Video";
                mainwindow.cboMediaType.IsEnabled = false;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboAudioStream.SelectedItem = "1";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.IsEnabled = true;
                //mainwindow.cboOptimize.SelectedItem = "Web";
                //webm has no video tuning available
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
            {
                mainwindow.cboMediaType.SelectedItem = "Video";
                mainwindow.cboMediaType.IsEnabled = false;
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboSubtitle.IsEnabled = true;
                mainwindow.cboAudioStream.SelectedItem = "all";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.IsEnabled = true;
                //video tuning is under videoCodec method
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
            {
                mainwindow.cboMediaType.SelectedItem = "Video";
                mainwindow.cboMediaType.IsEnabled = false;
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboSubtitle.IsEnabled = true;
                mainwindow.cboAudioStream.SelectedItem = "all";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.IsEnabled = true;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "avi")
            {
                mainwindow.cboMediaType.SelectedItem = "Video";
                mainwindow.cboMediaType.IsEnabled = false;
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboSubtitle.IsEnabled = true;
                mainwindow.cboAudioStream.SelectedItem = "all";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.IsEnabled = false;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
            {
                mainwindow.cboMediaType.SelectedItem = "Video";
                mainwindow.cboMediaType.IsEnabled = false;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboAudioStream.SelectedItem = "all";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.IsEnabled = true;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "gif")
            {
                mainwindow.cboMediaType.SelectedItem = "Image";
                mainwindow.cboMediaType.IsEnabled = true;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.IsEnabled = false;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp3")
            {
                mainwindow.cboMediaType.SelectedItem = "Audio";
                mainwindow.cboMediaType.IsEnabled = false;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboAudioStream.SelectedItem = "1";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = false;
                mainwindow.cboOptimize.IsEnabled = false;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "m4a")
            {
                mainwindow.cboMediaType.SelectedItem = "Audio";
                mainwindow.cboMediaType.IsEnabled = false;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboAudioStream.SelectedItem = "1";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = false;
                mainwindow.cboOptimize.IsEnabled = false;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogg")
            {
                mainwindow.cboMediaType.SelectedItem = "Audio";
                mainwindow.cboMediaType.IsEnabled = false;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboAudioStream.SelectedItem = "1";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = false;
                mainwindow.cboOptimize.IsEnabled = false;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "flac")
            {
                mainwindow.cboMediaType.SelectedItem = "Audio";
                mainwindow.cboMediaType.IsEnabled = false;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboAudioStream.SelectedItem = "1";
                mainwindow.cboOptimize.IsEnabled = false;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "wav")
            {
                mainwindow.cboMediaType.SelectedItem = "Audio";
                mainwindow.cboMediaType.IsEnabled = false;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboOptimize.IsEnabled = false;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "jpg")
            {
                // Media Type Combobox ///////
                // Remove all other options but Image and Sequence
                MediaTypeItemSource = new List<string>() { "Image", "Sequence" };
                mainwindow.cboMediaType.ItemsSource = MediaTypeItemSource;

                mainwindow.cboMediaType.SelectedItem = "Image";

                mainwindow.cboMediaType.IsEnabled = true;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboOptimize.IsEnabled = false;

                // more options enable/disable in MediaType Section
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "png")
            {
                // Media Type Combobox ///////
                // Remove all other options but Image and Sequence
                MediaTypeItemSource = new List<string>() { "Image", "Sequence" };
                mainwindow.cboMediaType.ItemsSource = MediaTypeItemSource;

                mainwindow.cboMediaType.SelectedItem = "Image";

                mainwindow.cboMediaType.IsEnabled = true;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboOptimize.IsEnabled = false;

                // more options enable/disable in MediaType Section
            }
        }



        /// <summary>
        /// Output Format (Method)
        /// </summary>
        // On Format Combobox Change
        // Output ComboBox Options
        public static void OutputFormat(MainWindow mainwindow)
        {
            // Reset MediaType ComboBox back to Default if not jpg/png and does not contain video/audio (must be above other format options)
            if ((string)mainwindow.cboFormat.SelectedItem != "jpg"
                && (string)mainwindow.cboFormat.SelectedItem != "png"
                && !mainwindow.cboMediaType.Items.Contains("Video")
                && !mainwindow.cboMediaType.Items.Contains("Audio"))
            {
                MediaTypeItemSource = new List<string>() { "Video", "Audio", "Image", "Sequence" };

                mainwindow.cboMediaType.ItemsSource = MediaTypeItemSource;
            }

            // --------------------------------------------------------------------------------------------------------
            // Codecs Per Container
            // --------------------------------------------------------------------------------------------------------
            // Change Video Codec Items

            // -------------------------
            // WEBM 
            // -------------------------
            if ((string)mainwindow.cboFormat.SelectedItem == "webm")
            {
                // VIDEO ///////
                VideoControls.VideoCodecItemSource = new List<string>() { "VP8", "VP9" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = VideoControls.VideoCodecItemSource;

                // AUDIO ///////    
                AudioControls.AudioCodecItemSource = new List<string>() { "Vorbis", "Opus" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = AudioControls.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "VP8";
                mainwindow.cboAudioCodec.SelectedItem = "Vorbis";
                //mainwindow.cboOptimize.SelectedItem = "Web";
            }

            // -------------------------
            // MP4 
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
            {
                // VIDEO ///////
                VideoControls.VideoCodecItemSource = new List<string>() { "x264", "x265" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = VideoControls.VideoCodecItemSource;

                // AUDIO ///////    
                AudioControls.AudioCodecItemSource = new List<string>() { "AAC", "AC3" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = AudioControls.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboAudioCodec.SelectedItem = "AAC";
            }

            // -------------------------
            // MKV 
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
            {
                // VIDEO ///////
                VideoControls.VideoCodecItemSource = new List<string>() { "x264", "x265", "VP8", "VP9", "Theora", "Copy" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = VideoControls.VideoCodecItemSource;


                // AUDIO ///////    
                AudioControls.AudioCodecItemSource = new List<string>() { "AAC", "AC3", "Vorbis", "Opus", "LAME", "FLAC", "PCM", "Copy" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = AudioControls.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboAudioCodec.SelectedItem = "AC3";
            }

            // -------------------------
            // AVI 
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "avi")
            {
                // VIDEO ///////
                VideoControls.VideoCodecItemSource = new List<string>() { "mpeg4" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = VideoControls.VideoCodecItemSource;


                // AUDIO ///////    
                AudioControls.AudioCodecItemSource = new List<string>() { "AAC", "AC3", "LAME", "PCM" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = AudioControls.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "mpeg4";
                mainwindow.cboAudioCodec.SelectedItem = "LAME";
            }

            // -------------------------
            // OGV 
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
            {
                // VIDEO ///////
                VideoControls.VideoCodecItemSource = new List<string>() { "Theora" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = VideoControls.VideoCodecItemSource;


                // AUDIO ///////    
                AudioControls.AudioCodecItemSource = new List<string>() { "Vorbis" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = AudioControls.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "Theora";
                mainwindow.cboAudioCodec.SelectedItem = "Vorbis";
            }

            // -------------------------
            // M4A 
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "m4a")
            {
                // VIDEO ///////
                VideoControls.VideoCodecItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = VideoControls.VideoCodecItemSource;


                // AUDIO ///////    
                AudioControls.AudioCodecItemSource = new List<string>() { "AAC", "ALAC" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = AudioControls.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "None";
                mainwindow.cboAudioCodec.SelectedItem = "AAC";
            }

            // -------------------------
            // MP3
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp3")
            {
                // VIDEO ///////
                VideoControls.VideoCodecItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = VideoControls.VideoCodecItemSource;


                // AUDIO ///////    
                AudioControls.AudioCodecItemSource = new List<string>() { "LAME" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = AudioControls.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "None";
                mainwindow.cboAudioCodec.SelectedItem = "LAME";
            }

            // -------------------------
            // OGG 
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogg")
            {
                // VIDEO ///////
                VideoControls.VideoCodecItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = VideoControls.VideoCodecItemSource;


                // AUDIO ///////    
                AudioControls.AudioCodecItemSource = new List<string>() { "Opus", "Vorbis" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = AudioControls.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "None";
                mainwindow.cboAudioCodec.SelectedItem = "Opus";
            }

            // -------------------------
            // FLAC 
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "flac")
            {
                // VIDEO ///////
                VideoControls.VideoCodecItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = VideoControls.VideoCodecItemSource;


                // AUDIO ///////    
                AudioControls.AudioCodecItemSource = new List<string>() { "FLAC" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = AudioControls.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "None";
                mainwindow.cboAudioCodec.SelectedItem = "FLAC";
            }

            // -------------------------
            // WAV 
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "wav")
            {
                // VIDEO ///////
                VideoControls.VideoCodecItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = VideoControls.VideoCodecItemSource;


                // AUDIO ///////    
                AudioControls.AudioCodecItemSource = new List<string>() { "PCM" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = AudioControls.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "None";
                mainwindow.cboAudioCodec.SelectedItem = "PCM";
                mainwindow.tglVBR.IsEnabled = false;
            }

            // -------------------------
            // JPG
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "jpg")
            {
                // VIDEO ///////
                VideoControls.VideoCodecItemSource = new List<string>() { "JPEG" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = VideoControls.VideoCodecItemSource;


                // AUDIO ///////    
                AudioControls.AudioCodecItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = AudioControls.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "JPEG";
                mainwindow.cboAudioCodec.SelectedItem = "None";  //important
            }

            // -------------------------
            // PNG
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "png")
            {
                // VIDEO ///////
                VideoControls.VideoCodecItemSource = new List<string>() { "PNG" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = VideoControls.VideoCodecItemSource;


                // AUDIO ///////    
                AudioControls.AudioCodecItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = AudioControls.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "PNG";
                mainwindow.cboAudioCodec.SelectedItem = "None";  //important
            }


            // -------------------------
            // Format Container Additional Rules
            // -------------------------
            // Disable VBR checkbox if Audio is Auto (ALWAYS) - This might not work, might be overridden by below
            if ((string)mainwindow.cboAudio.SelectedItem == "Auto"
                && (string)mainwindow.cboFormat.SelectedItem == "mp4"
                || (string)mainwindow.cboFormat.SelectedItem == "mkv"
                || (string)mainwindow.cboFormat.SelectedItem == "gif"
                || (string)mainwindow.cboFormat.SelectedItem == "mp3"
                || (string)mainwindow.cboFormat.SelectedItem == "m4a"
                || (string)mainwindow.cboFormat.SelectedItem == "flac"
                || (string)mainwindow.cboFormat.SelectedItem == "wav")
            {
                mainwindow.tglVBR.IsEnabled = false;
                mainwindow.tglVBR.IsChecked = false;
            }
            // Check VBR for WebM (VBR-Only codec) (ALWAYS)
            else if ((string)mainwindow.cboAudio.SelectedItem == "Auto" 
                && (string)mainwindow.cboFormat.SelectedItem == "webm")
            {
                mainwindow.tglVBR.IsEnabled = false;
                mainwindow.tglVBR.IsChecked = true;
            }
            // Check VBR for OGV (VBR-Only codec) (ALWAYS)
            else if ((string)mainwindow.cboAudio.SelectedItem == "Auto" 
                && (string)mainwindow.cboFormat.SelectedItem == "ogv")
            {
                mainwindow.tglVBR.IsEnabled = false;
                mainwindow.tglVBR.IsChecked = true; //doesnt work
            }


            // Set Video & Audio Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
            VideoControls.AutoCopyVideoCodec(mainwindow);
            AudioControls.AutoCopyAudioCodec(mainwindow);
        }





        /// <summary>
        /// MediaType Controls (Method)
        /// </summary>
        public static void MediaType(MainWindow mainwindow)
        {
            // Note: Try to Only Use *Disabled*. Enable will unnecessarily override.

            // -------------------------
            // Video MediaType
            // -------------------------
            // Enable Frame Textbox for Image Screenshot
            if ((string)mainwindow.cboMediaType.SelectedItem == "Video")
            {
                // -------------------------
                // Codec
                // -------------------------
                mainwindow.cboVideoCodec.IsEnabled = true;
                mainwindow.cboAudioCodec.IsEnabled = true;

                // -------------------------
                // Video
                // -------------------------
                //Size
                //size.SelectedItem = "No";
                mainwindow.cboSize.IsEnabled = true;

                // Cut
                // Cut Change - If coming back from JPEG or PNG
                if (mainwindow.cutStart.IsEnabled == true && mainwindow.cutEnd.IsEnabled == false)
                {
                    mainwindow.cboCut.SelectedItem = "No";
                }

                // Crop
                mainwindow.buttonCrop.IsEnabled = true;

                //Speed
                mainwindow.cboSpeed.IsEnabled = true;

                // -------------------------
                // Audio
                // -------------------------
                // Channel
                mainwindow.cboChannel.IsEnabled = true;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = true;
                mainwindow.volumeUpButton.IsEnabled = true;
                mainwindow.volumeDownButton.IsEnabled = true;

                // Limiter
                mainwindow.tglAudioLimiter.IsEnabled = true;

                // Audio Stream
                mainwindow.cboAudioStream.IsEnabled = true;
                mainwindow.cboAudioStream.SelectedItem = "all";
            }

            // -------------------------
            // Audio MediaType
            // -------------------------
            else if ((string)mainwindow.cboMediaType.SelectedItem == "Audio")
            {
                // -------------------------
                // Codec
                // -------------------------
                mainwindow.cboVideoCodec.IsEnabled = false;
                mainwindow.cboAudioCodec.IsEnabled = true;

                // -------------------------
                // Video
                // -------------------------
                //Size
                mainwindow.cboSize.SelectedItem = "No";
                mainwindow.cboSize.IsEnabled = false;

                // Cut
                // Cut Change - If coming back from JPEG or PNG
                if (mainwindow.cutStart.IsEnabled == true && mainwindow.cutEnd.IsEnabled == false)
                {
                    mainwindow.cboCut.SelectedItem = "No";
                }

                // Frame
                mainwindow.frameEnd.IsEnabled = false;
                mainwindow.frameEnd.Text = string.Empty;

                // Crop
                //crop = string.Empty; //??
                mainwindow.buttonCrop.IsEnabled = false;

                // Fps
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = false;

                //Speed
                mainwindow.cboSpeed.IsEnabled = false;

                // -------------------------
                // Audio
                // -------------------------
                // Channel
                mainwindow.cboChannel.IsEnabled = true; //always

                // Audio Stream
                mainwindow.cboAudioStream.IsEnabled = true;
                mainwindow.cboAudioStream.SelectedItem = "1";

                // Sample Rate
                //samplerateSelect.SelectedItem = true;

                // Bit Depth
                //bitdepthSelect.SelectedItem = true;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = true;
                mainwindow.volumeUpButton.IsEnabled = true;
                mainwindow.volumeDownButton.IsEnabled = true;

                // Limiter
                mainwindow.tglAudioLimiter.IsEnabled = true;

            }

            // -------------------------
            // Image MediaType
            // -------------------------
            else if ((string)mainwindow.cboMediaType.SelectedItem == "Image")
            {
                // -------------------------
                // Codec
                // -------------------------
                mainwindow.cboVideoCodec.IsEnabled = true;
                mainwindow.cboAudioCodec.IsEnabled = false;

                // -------------------------
                // Video
                // -------------------------
                //Size
                mainwindow.cboSize.IsEnabled = true;

                // Cut
                // Enable Cut Start Time for Frame Selection
                mainwindow.cboCut.SelectedItem = "Yes";
                mainwindow.cutStart.IsEnabled = true; //important
                mainwindow.cutEnd.Text = "00:00:00.000"; //important
                mainwindow.cutEnd.IsEnabled = false; //important

                // Frame
                mainwindow.frameEnd.IsEnabled = false; //important
                mainwindow.frameEnd.Text = string.Empty; //important

                // Crop

                // Fps
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = false;

                //Speed
                mainwindow.cboSpeed.IsEnabled = false;

                // -------------------------
                // Audio
                // -------------------------
                // Channel
                mainwindow.cboChannel.IsEnabled = false;

                // Audio Stream
                mainwindow.cboAudioStream.IsEnabled = false;
                mainwindow.cboAudioStream.SelectedItem = "none";

                // Sample Rate
                mainwindow.cboSamplerate.SelectedItem = false;

                // Bit Depth
                mainwindow.cboBitDepth.SelectedItem = false;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = false;
                mainwindow.volumeUpButton.IsEnabled = false;
                mainwindow.volumeDownButton.IsEnabled = false;

                // Limiter
                mainwindow.tglAudioLimiter.IsChecked = false;
                mainwindow.tglAudioLimiter.IsEnabled = false;
                mainwindow.audioLimiter.Text = string.Empty;

            }

            // -------------------------
            // Sequence MediaType
            // -------------------------
            else if ((string)mainwindow.cboMediaType.SelectedItem == "Sequence")
            {
                // -------------------------
                // Codec
                // -------------------------
                mainwindow.cboVideoCodec.IsEnabled = true;
                mainwindow.cboAudioCodec.IsEnabled = false;

                // -------------------------
                // Video
                // -------------------------
                //Size
                mainwindow.cboSize.IsEnabled = true;

                // Cut
                // Enable Cut for Time Selection
                mainwindow.cboCut.SelectedItem = "No";

                // Frame

                // Crop
                mainwindow.buttonCrop.IsEnabled = true;

                //Speed
                mainwindow.cboSpeed.IsEnabled = false;

                // Fps
                //fpsSelect.SelectedItem = "24";
                mainwindow.cboFPS.IsEnabled = true;

                // -------------------------
                // Audio
                // -------------------------
                // Channel
                mainwindow.cboChannel.IsEnabled = false;

                // Audio Stream
                mainwindow.cboAudioStream.IsEnabled = false;
                mainwindow.cboAudioStream.SelectedItem = "none";

                // Sample Rate
                mainwindow.cboSamplerate.SelectedItem = false;

                // Bit Depth
                mainwindow.cboBitDepth.SelectedItem = false;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = false;
                mainwindow.volumeUpButton.IsEnabled = false;
                mainwindow.volumeDownButton.IsEnabled = false;

                // Limiter
                mainwindow.tglAudioLimiter.IsChecked = false;
                mainwindow.tglAudioLimiter.IsEnabled = false;
                mainwindow.audioLimiter.Text = string.Empty;
            }
        }


        /// <summary>
        /// Cut Controls (Method)
        /// </summary>
        //On Selection Change
        public static void CutControls(MainWindow mainwindow)
        {
            //Enable Aspect Custom
            // No
            //
            if ((string)mainwindow.cboCut.SelectedItem == "No")
            {
                // Time
                mainwindow.cutStart.IsEnabled = false;
                mainwindow.cutEnd.IsEnabled = false;

                mainwindow.cutStart.Text = "00:00:00.000";
                mainwindow.cutEnd.Text = "00:00:00.000";

                // Frames
                mainwindow.frameStart.IsEnabled = false;
                mainwindow.frameEnd.IsEnabled = false;

                // Reset Text
                mainwindow.frameStart.Text = "Frame";
                mainwindow.frameEnd.Text = "Range";

                //trim = string.Empty;
            }

            // Yes
            //
            else if ((string)mainwindow.cboCut.SelectedItem == "Yes")
            {
                // Frames
                if ((string)mainwindow.cboMediaType.SelectedItem == "Video") // only for video
                {
                    // Time
                    mainwindow.cutStart.IsEnabled = true;
                    mainwindow.cutEnd.IsEnabled = true;

                    // Frames
                    mainwindow.frameStart.IsEnabled = true;
                    mainwindow.frameEnd.IsEnabled = true;
                }
                else if ((string)mainwindow.cboMediaType.SelectedItem == "Audio") // only for video
                {
                    // Time
                    mainwindow.cutStart.IsEnabled = true;
                    mainwindow.cutEnd.IsEnabled = true;

                    // Frames
                    mainwindow.frameStart.IsEnabled = false;
                    mainwindow.frameEnd.IsEnabled = false;

                    // Text
                    mainwindow.frameStart.Text = "Frame";
                    mainwindow.frameEnd.Text = "Range";
                }
                else if ((string)mainwindow.cboMediaType.SelectedItem == "Image") // only for video
                {
                    // Time
                    mainwindow.cutStart.IsEnabled = true;
                    mainwindow.cutEnd.IsEnabled = false;
                    mainwindow.cutEnd.Text = "00:00:00.000"; //important

                    // Frames
                    mainwindow.frameStart.IsEnabled = true;
                    mainwindow.frameEnd.IsEnabled = false;
                    mainwindow.frameEnd.Text = string.Empty; //important
                }
                else if ((string)mainwindow.cboMediaType.SelectedItem == "Sequence") // only for video
                {
                    // Time
                    mainwindow.cutStart.IsEnabled = true;
                    mainwindow.cutEnd.IsEnabled = true;

                    // Frames
                    mainwindow.frameStart.IsEnabled = true;
                    mainwindow.frameEnd.IsEnabled = true;
                }
            }
        } // End Cut Controls
    }
}
