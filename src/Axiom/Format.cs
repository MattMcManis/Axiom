using System.Collections.Generic;
using System.Windows.Media;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

/* ----------------------------------------------------------------------
    Axiom
    Copyright (C) 2017 Matt McManis
    http://github.com/MattMcManis/Axiom
    http://www.x.co/axiomui
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

namespace Axiom
{
    public partial class Format
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ComboBoxes Item Sources
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // Previous ComboBox Item
        //private string previousItem; 

        // Load in Intialize Component
        //
        // MediaType
        public static List<string> MediaTypeItemSource = new List<string>() { "Video", "Audio", "Image", "Sequence" };

        // Format
        public static List<string> FormatItemSource = new List<string>() { "webm", "mp4", "mkv", "ogv", "mp3", "m4a", "ogg", "flac", "wav", "jpg", "png" };


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Variables
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // Cut
        public static string trimStart;
        public static string trimEnd;
        public static string trim; // contains trimStart, trimEnd



        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Control Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Format Controls (Method)
        /// </summary>
        // On Format Combobox Change
        public static void fileFormat(MainWindow mainwindow)
        {
            // Reset MediaType ComboBox back to Default if not jpg/png and does not contain video/audio (must be above other format options)
            if ((string)mainwindow.cboFormat.SelectedItem != "jpg" && (string)mainwindow.cboFormat.SelectedItem != "png" && !mainwindow.cboMediaType.Items.Contains("Video") && !mainwindow.cboMediaType.Items.Contains("Audio"))
            {
                MediaTypeItemSource = new List<string>() { "Video", "Audio", "Image", "Sequence" };

                mainwindow.cboMediaType.ItemsSource = MediaTypeItemSource;
            }

            // -------------------------
            // Format Container Rules
            // -------------------------
            if ((string)mainwindow.cboFormat.SelectedItem == "webm")
            {
                MainWindow.outputExt = ".webm";
                mainwindow.cboMediaType.SelectedItem = "Video";
                mainwindow.cboMediaType.IsEnabled = false;
                Video.options = string.Empty;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboAudioStream.SelectedItem = "1";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.IsEnabled = true;
                mainwindow.cboOptimize.SelectedItem = "Web";
                //webm has no video tuning available
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
            {
                MainWindow.outputExt = ".mp4";
                mainwindow.cboMediaType.SelectedItem = "Video";
                mainwindow.cboMediaType.IsEnabled = false;
                Video.options = string.Empty;
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboSubtitle.IsEnabled = true;
                mainwindow.cboAudioStream.SelectedItem = "all";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.IsEnabled = true;
                //video tuning is under videoCodec method
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
            {
                MainWindow.outputExt = ".mkv";
                mainwindow.cboMediaType.SelectedItem = "Video";
                mainwindow.cboMediaType.IsEnabled = false;
                Video.options = string.Empty;
                mainwindow.cboSubtitle.SelectedItem = "all";
                mainwindow.cboSubtitle.IsEnabled = true;
                mainwindow.cboAudioStream.SelectedItem = "all";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.IsEnabled = true;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
            {
                MainWindow.outputExt = ".ogv";
                mainwindow.cboMediaType.SelectedItem = "Video";
                mainwindow.cboMediaType.IsEnabled = false;
                Video.options = string.Empty;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboAudioStream.SelectedItem = "all";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.IsEnabled = true;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "gif")
            {
                MainWindow.outputExt = ".gif";
                mainwindow.cboMediaType.SelectedItem = "Image";
                mainwindow.cboMediaType.IsEnabled = true;
                Video.vCodec = string.Empty;
                Audio.aCodec = string.Empty;
                Video.options = string.Empty;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = true;
                mainwindow.cboOptimize.IsEnabled = false;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp3")
            {
                MainWindow.outputExt = ".mp3";
                mainwindow.cboMediaType.SelectedItem = "Audio";
                mainwindow.cboMediaType.IsEnabled = false;
                Video.options = string.Empty;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboAudioStream.SelectedItem = "1";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = false;
                mainwindow.cboOptimize.IsEnabled = false;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "m4a")
            {
                MainWindow.outputExt = ".m4a";
                mainwindow.cboMediaType.SelectedItem = "Audio";
                mainwindow.cboMediaType.IsEnabled = false;
                Video.options = string.Empty;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboAudioStream.SelectedItem = "1";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = false;
                mainwindow.cboOptimize.IsEnabled = false;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogg")
            {
                MainWindow.outputExt = ".ogg";
                mainwindow.cboMediaType.SelectedItem = "Audio";
                mainwindow.cboMediaType.IsEnabled = false;
                Video.options = string.Empty;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboAudioStream.SelectedItem = "1";
                mainwindow.cboFPS.SelectedItem = "auto";
                mainwindow.cboFPS.IsEnabled = false;
                mainwindow.cboOptimize.IsEnabled = false;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "flac")
            {
                MainWindow.outputExt = ".flac";
                mainwindow.cboMediaType.SelectedItem = "Audio";
                mainwindow.cboMediaType.IsEnabled = false;
                Video.options = string.Empty;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboAudioStream.SelectedItem = "1";
                mainwindow.cboOptimize.IsEnabled = false;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "wav")
            {
                MainWindow.outputExt = ".wav";
                mainwindow.cboMediaType.SelectedItem = "Audio";
                mainwindow.cboMediaType.IsEnabled = false;
                Video.options = string.Empty;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboOptimize.IsEnabled = false;
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "jpg")
            {
                MainWindow.outputExt = ".jpg";
                // Media Type Combobox ///////
                // Remove all other options but Image and Sequence
                MediaTypeItemSource = new List<string>() { "Image", "Sequence" };
                mainwindow.cboMediaType.ItemsSource = MediaTypeItemSource;

                mainwindow.cboMediaType.SelectedItem = "Image";

                mainwindow.cboMediaType.IsEnabled = true;
                Video.vCodec = string.Empty;
                Audio.aCodec = string.Empty;
                Streams.mMap = string.Empty;
                Video.options = string.Empty;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboOptimize.IsEnabled = false;

                // more options enable/disable in MediaType Section
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "png")
            {
                MainWindow.outputExt = ".png";
                // Media Type Combobox ///////
                // Remove all other options but Image and Sequence
                MediaTypeItemSource = new List<string>() { "Image", "Sequence" };
                mainwindow.cboMediaType.ItemsSource = MediaTypeItemSource;

                mainwindow.cboMediaType.SelectedItem = "Image";

                mainwindow.cboMediaType.IsEnabled = true;
                Video.vCodec = string.Empty;
                Audio.aCodec = string.Empty;
                Streams.mMap = string.Empty;
                Video.options = string.Empty;
                mainwindow.cboSubtitle.SelectedItem = "none";
                mainwindow.cboSubtitle.IsEnabled = false;
                mainwindow.cboOptimize.IsEnabled = false;

                // more options enable/disable in MediaType Section
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
                Video.VideoCodecItemSource = new List<string>() { "VP8", "VP9" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = Video.VideoCodecItemSource;

                // AUDIO ///////    
                Audio.AudioCodecItemSource = new List<string>() { "Vorbis", "Opus" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = Audio.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "VP8";
                mainwindow.cboAudioCodec.SelectedItem = "Vorbis";
            }

            // -------------------------
            // MP4 
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
            {
                // VIDEO ///////
                Video.VideoCodecItemSource = new List<string>() { "x264", "x265" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = Video.VideoCodecItemSource;

                // AUDIO ///////    
                Audio.AudioCodecItemSource = new List<string>() { "AAC", "AC3" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = Audio.AudioCodecItemSource;

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
                Video.VideoCodecItemSource = new List<string>() { "x264", "x265", "VP8", "VP9", "Theora", "Copy" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = Video.VideoCodecItemSource;


                // AUDIO ///////    
                Audio.AudioCodecItemSource = new List<string>() { "AAC", "AC3", "Vorbis", "Opus", "LAME", "PCM", "Copy" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = Audio.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "x264";
                mainwindow.cboAudioCodec.SelectedItem = "AC3";
            }

            // -------------------------
            // OGV 
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
            {
                // VIDEO ///////
                Video.VideoCodecItemSource = new List<string>() { "Theora" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = Video.VideoCodecItemSource;


                // AUDIO ///////    
                Audio.AudioCodecItemSource = new List<string>() { "Vorbis" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = Audio.AudioCodecItemSource;

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
                Video.VideoCodecItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = Video.VideoCodecItemSource;


                // AUDIO ///////    
                Audio.AudioCodecItemSource = new List<string>() { "AAC", "ALAC" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = Audio.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "None"; // was empty
                mainwindow.cboAudioCodec.SelectedItem = "AAC";
            }

            // -------------------------
            // MP3
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp3")
            {
                // VIDEO ///////
                Video.VideoCodecItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = Video.VideoCodecItemSource;


                // AUDIO ///////    
                Audio.AudioCodecItemSource = new List<string>() { "LAME" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = Audio.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "None"; // was empty
                mainwindow.cboAudioCodec.SelectedItem = "LAME";
            }

            // -------------------------
            // OGG 
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogg")
            {
                // VIDEO ///////
                Video.VideoCodecItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = Video.VideoCodecItemSource;


                // AUDIO ///////    
                Audio.AudioCodecItemSource = new List<string>() { "Opus", "Vorbis" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = Audio.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "None"; // was empty
                mainwindow.cboAudioCodec.SelectedItem = "Opus";
            }

            // -------------------------
            // FLAC 
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "flac")
            {
                // VIDEO ///////
                Video.VideoCodecItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = Video.VideoCodecItemSource;


                // AUDIO ///////    
                Audio.AudioCodecItemSource = new List<string>() { "Flac" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = Audio.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "None"; // was empty
                mainwindow.cboAudioCodec.SelectedItem = "Flac";
            }

            // -------------------------
            // WAV 
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "wav")
            {
                // VIDEO ///////
                Video.VideoCodecItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = Video.VideoCodecItemSource;


                // AUDIO ///////    
                Audio.AudioCodecItemSource = new List<string>() { "PCM" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = Audio.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "None"; // was empty
                mainwindow.cboAudioCodec.SelectedItem = "PCM";
                mainwindow.tglVBR.IsEnabled = false;
            }

            // -------------------------
            // JPG
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "jpg")
            {
                // VIDEO ///////
                Video.VideoCodecItemSource = new List<string>() { "JPEG" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = Video.VideoCodecItemSource;


                // AUDIO ///////    
                Audio.AudioCodecItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = Audio.AudioCodecItemSource;

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
                Video.VideoCodecItemSource = new List<string>() { "PNG" };

                // Populate ComboBox from ItemSource
                mainwindow.cboVideoCodec.ItemsSource = Video.VideoCodecItemSource;


                // AUDIO ///////    
                Audio.AudioCodecItemSource = new List<string>() { "None" };

                // Populate ComboBox from ItemSource
                mainwindow.cboAudioCodec.ItemsSource = Audio.AudioCodecItemSource;

                // Set the List Defaults
                mainwindow.cboVideoCodec.SelectedItem = "PNG";
                mainwindow.cboAudioCodec.SelectedItem = "None";  //important
            }


            // -------------------------
            // Format Container Additional Rules
            // -------------------------
            // If Video File is Auto, make Audio also Auto (to avoid batch interfere)
            //else if ((string)mainwindow.cboFormat.SelectedItem == "webm" | (string)mainwindow.cboFormat.SelectedItem == "mp4" | (string)mainwindow.cboFormat.SelectedItem == "mkv" | (string)mainwindow.cboFormat.SelectedItem == "ogv" | (string)mainwindow.cboFormat.SelectedItem == "gif")
            //{
            //    cboAudio.SelectedItem = "Auto";
            //}

            // Disable VBR checkbox if Audio is Auto (ALWAYS) - This might not work, might be overridden by below
            if ((string)mainwindow.cboAudio.SelectedItem == "Auto" && (string)mainwindow.cboFormat.SelectedItem == "mp4" || (string)mainwindow.cboFormat.SelectedItem == "mkv" || (string)mainwindow.cboFormat.SelectedItem == "gif" || (string)mainwindow.cboFormat.SelectedItem == "mp3" || (string)mainwindow.cboFormat.SelectedItem == "m4a" || (string)mainwindow.cboFormat.SelectedItem == "flac" || (string)mainwindow.cboFormat.SelectedItem == "wav")
            {
                mainwindow.tglVBR.IsEnabled = false;
                mainwindow.tglVBR.IsChecked = false;
            }
            // Check VBR for WebM (VBR-Only codec) (ALWAYS)
            else if ((string)mainwindow.cboAudio.SelectedItem == "Auto" && (string)mainwindow.cboFormat.SelectedItem == "webm")
            {
                mainwindow.tglVBR.IsEnabled = false;
                mainwindow.tglVBR.IsChecked = true;
            }
            // Check VBR for OGV (VBR-Only codec) (ALWAYS)
            else if ((string)mainwindow.cboAudio.SelectedItem == "Auto" && (string)mainwindow.cboFormat.SelectedItem == "ogv")
            {
                mainwindow.tglVBR.IsEnabled = false;
                mainwindow.tglVBR.IsChecked = true; //doesnt work
            }

            // Change All MainWindow Items
            Video.VideoCodecControls(mainwindow);
            Audio.AudioCodecControls(mainwindow);

            // Set Video & Audio Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
            Video.AutoVideoCodecCopy(mainwindow);
            Audio.AutoAudioCodecCopy(mainwindow);

        } // end fileFormat() method





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
                mainwindow.buttonCrop.Foreground = new SolidColorBrush(Colors.White);

                //Speed
                mainwindow.cboSpeed.IsEnabled = true;

                // -------------------------
                // Audio
                // -------------------------
                // Channel
                mainwindow.cboChannel.IsEnabled = true;

                // Volume
                mainwindow.volumeUpDown.IsEnabled = true;
                mainwindow.volumeUpDown.Foreground = new SolidColorBrush(Colors.White);
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

                //mainwindow.cutStart.IsEnabled = true; 
                //mainwindow.cutEnd.Text = "00:00:00.000";
                //mainwindow.cutEnd.IsEnabled = true; 

                // Frame
                mainwindow.frameEnd.IsEnabled = false;
                mainwindow.frameEnd.Text = string.Empty;

                // Crop
                //crop = string.Empty; //??
                mainwindow.buttonCrop.IsEnabled = false;
                mainwindow.buttonCrop.Foreground = MainWindow.TextBoxDiabledForeground;

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
                mainwindow.volumeUpDown.Foreground = new SolidColorBrush(Colors.White);
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
                mainwindow.buttonCrop.IsEnabled = true;
                mainwindow.buttonCrop.Foreground = new SolidColorBrush(Colors.White);

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
                mainwindow.volumeUpDown.Foreground = MainWindow.TextBoxDiabledForeground;
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
                mainwindow.buttonCrop.Foreground = new SolidColorBrush(Colors.White);

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
                mainwindow.volumeUpDown.Foreground = MainWindow.TextBoxDiabledForeground;
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

                // TextBox Color
                mainwindow.frameStart.Foreground = MainWindow.TextBoxDiabledForeground;
                mainwindow.frameEnd.Foreground = MainWindow.TextBoxDiabledForeground;

                // Reset Text
                mainwindow.frameStart.Text = "Frame";
                mainwindow.frameEnd.Text = "Range";

                trim = string.Empty;
            }

            // Yes
            //
            else if ((string)mainwindow.cboCut.SelectedItem == "Yes")
            {
                // TextBox Color
                mainwindow.frameStart.Foreground = MainWindow.TextBoxDarkBlue;
                mainwindow.frameEnd.Foreground = MainWindow.TextBoxDarkBlue;

                // Frames
                if ((string)mainwindow.cboMediaType.SelectedItem == "Video") // only for video
                {
                    // Time
                    mainwindow.cutStart.IsEnabled = true;
                    mainwindow.cutEnd.IsEnabled = true;

                    // Frames
                    mainwindow.frameStart.IsEnabled = true;
                    mainwindow.frameEnd.IsEnabled = true;

                    // TextBox Color
                    if (mainwindow.frameStart.Text != "Frame")
                    {
                        mainwindow.frameStart.Foreground = new SolidColorBrush(Colors.White);
                    }
                    if (mainwindow.frameEnd.Text != "Range")
                    {
                        mainwindow.frameEnd.Foreground = new SolidColorBrush(Colors.White);
                    }
                }
                else if ((string)mainwindow.cboMediaType.SelectedItem == "Audio") // only for video
                {
                    // Time
                    mainwindow.cutStart.IsEnabled = true;
                    mainwindow.cutEnd.IsEnabled = true;

                    // Frames
                    mainwindow.frameStart.IsEnabled = false;
                    mainwindow.frameEnd.IsEnabled = false;

                    // TextBox Color
                    mainwindow.frameStart.Foreground = MainWindow.TextBoxDiabledForeground;
                    mainwindow.frameEnd.Foreground = MainWindow.TextBoxDiabledForeground;

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

                    // TextBox Color
                    if (mainwindow.frameStart.Text != "Frame")
                    {
                        mainwindow.frameStart.Foreground = new SolidColorBrush(Colors.White);
                    }
                }
                else if ((string)mainwindow.cboMediaType.SelectedItem == "Sequence") // only for video
                {
                    // Time
                    mainwindow.cutStart.IsEnabled = true;
                    mainwindow.cutEnd.IsEnabled = true;

                    // Frames
                    mainwindow.frameStart.IsEnabled = true;
                    mainwindow.frameEnd.IsEnabled = true;

                    // TextBox Color
                    if (mainwindow.frameStart.Text != "Frame")
                    {
                        mainwindow.frameStart.Foreground = new SolidColorBrush(Colors.White);
                    }
                    if (mainwindow.frameEnd.Text != "Range")
                    {
                        mainwindow.frameEnd.Foreground = new SolidColorBrush(Colors.White);
                    }
                }
            }
        } // End Cut Controls




        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Process Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Cut (Method)
        /// </summary>
        public static void Cut(MainWindow mainwindow)
        {
            // VIDEO
            //
            if ((string)mainwindow.cboCut.SelectedItem == "Yes" && (string)mainwindow.cboMediaType.SelectedItem == "Video")
            {
                // Use Time
                // If Frame Textboxes Default Use Time
                if (mainwindow.frameStart.Text == "Frame" && mainwindow.frameEnd.Text == "Range" || string.IsNullOrWhiteSpace(mainwindow.frameStart.Text) && string.IsNullOrWhiteSpace(mainwindow.frameEnd.Text))
                {
                    trimStart = mainwindow.cutStart.Text;
                    trimEnd = mainwindow.cutEnd.Text;
                }
                // Use Frames
                // If Frame Textboxes have Text, but not Default, use FramesToDecimal Method (Override Time)
                else if (mainwindow.frameStart.Text != "Frame" && mainwindow.frameEnd.Text != "Range" && !string.IsNullOrWhiteSpace(mainwindow.frameStart.Text) && !string.IsNullOrWhiteSpace(mainwindow.frameEnd.Text))
                {
                    Video.FramesToDecimal(mainwindow);
                }

                trim = "-ss " + trimStart + " " + "-to " + trimEnd;
            }

            // AUDIO
            //
            else if ((string)mainwindow.cboCut.SelectedItem == "Yes" && (string)mainwindow.cboMediaType.SelectedItem == "Audio")
            {
                trimStart = mainwindow.cutStart.Text;
                trimEnd = mainwindow.cutEnd.Text;

                trim = "-ss " + trimStart + " " + "-to " + trimEnd;
            }

            // JPEG & PNG Screenshot
            //
            else if ((string)mainwindow.cboCut.SelectedItem == "Yes" && (string)mainwindow.cboMediaType.SelectedItem == "Image")
            {
                // Use Time
                // If Frame Textbox Default Use Time
                if (mainwindow.frameStart.Text == "Frame" || string.IsNullOrWhiteSpace(mainwindow.frameStart.Text))
                {
                    trimStart = mainwindow.cutStart.Text;
                }
                // Use Frames
                // If Frame Textboxes have Text, but not Default, use FramesToDecimal Method (Override Time)
                else if (mainwindow.frameStart.Text != "Frame" && mainwindow.frameEnd.Text != "Range" && !string.IsNullOrWhiteSpace(mainwindow.frameStart.Text) && string.IsNullOrWhiteSpace(mainwindow.frameEnd.Text))
                {
                    Video.FramesToDecimal(mainwindow);
                }

                trim = "-ss " + trimStart;
            }

            // JPEG & PNG Sequence
            //
            else if ((string)mainwindow.cboCut.SelectedItem == "Yes" && (string)mainwindow.cboMediaType.SelectedItem == "Sequence")
            {
                // Use Time
                // If Frame Textboxes Default Use Time
                if (mainwindow.frameStart.Text == "Frame" && mainwindow.frameEnd.Text == "Range" || string.IsNullOrWhiteSpace(mainwindow.frameStart.Text) && string.IsNullOrWhiteSpace(mainwindow.frameEnd.Text))
                {
                    trimStart = mainwindow.cutStart.Text;
                    trimEnd = mainwindow.cutEnd.Text;
                }
                // Use Frames
                // If Frame Textboxes have Text, but not Default, use FramesToDecimal Method (Override Time)
                else if (mainwindow.frameStart.Text != "Frame" && mainwindow.frameEnd.Text != "Range" && !string.IsNullOrWhiteSpace(mainwindow.frameStart.Text) && !string.IsNullOrWhiteSpace(mainwindow.frameEnd.Text))
                {
                    Video.FramesToDecimal(mainwindow);
                }

                trim = "-ss " + trimStart + " " + "-to " + trimEnd;
            }
        }

    }
}