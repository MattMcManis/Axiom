/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2021 Matt McManis
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using ViewModel;
using System.Collections.ObjectModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Container - ComboBox
        /// </summary>
        private void cboFormat_Container_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Save Previouis Item
            MainWindow.Format_Container_PreviousItem = VM.FormatView.Format_Container_SelectedItem;
            //MessageBox.Show(MainWindow.Format_Container_PreviousItem);
        }
        private void cboFormat_Container_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Format Controls
            // -------------------------
            Controls.Format.Controls.FormatControls(VM.FormatView.Format_Container_SelectedItem);

            // -------------------------
            // Set HW Accel Codec
            // -------------------------
            SetHWAccelVideoCodecs();
            SelectHWAccelVideoCodec();
            ChangeHWAccelTranscode();

            // -------------------------
            // Get Output Extension
            // -------------------------
            Controls.Format.Controls.OutputFormatExt();

            // -------------------------
            // Video Encoding Pass
            // -------------------------
            Controls.Video.Controls.EncodingPassControls();

            // -------------------------
            // Pixel Format
            // -------------------------
            //VideoControls.PixelFormatControls(VM.FormatView.Format_MediaType_SelectedItem,
            //                                  VM.VideoView.Video_Codec_SelectedItem,
            //                                  VM.VideoView.Video_Quality_SelectedItem);

            // -------------------------
            // Optimize Controls
            // -------------------------
            Controls.Video.Controls.OptimizeControls();


            //// -------------------------
            //// File Renamer
            //// -------------------------
            //// Add (1) if File Names are the same
            //if (VM.MainView.Batch_IsChecked == false) // Ignore batch
            //{
            //    if (!string.IsNullOrWhiteSpace(inputDir) &&
            //        string.Equals(inputFileName, outputFileName, StringComparison.OrdinalIgnoreCase))
            //    {
            //        outputFileName = FileRenamer(inputFileName);
            //        outputFileName_Tokens = FileRenamer(TokenAppender(inputFileName));
            //    }
            //}

            // --------------------------------------------------
            // Default Auto if Input Extension matches Output Extsion
            // This will trigger Auto Codec Copy
            // --------------------------------------------------
            //ExtensionMatchLoadAutoValues();
            //MessageBox.Show(inputExt + " " + outputExt); //debug

            // -------------------------
            // Update Ouput Textbox with current Format extension
            // -------------------------
            //OutputPath_UpdateDisplay(); 
            if (!string.IsNullOrWhiteSpace(VM.MainView.Output_Text) &&
                VM.MainView.Batch_IsChecked == false &&
                IsValidPath(VM.MainView.Output_Text) == true)
            {
                string outputDir = Path.GetDirectoryName(VM.MainView.Output_Text);
                string outputFileName = Path.GetFileNameWithoutExtension(VM.MainView.Output_Text);
                VM.MainView.Output_Text = Path.Combine(outputDir, outputFileName + outputExt);
            }

            // -------------------------
            // Force MediaTypeControls ComboBox to fire SelectionChanged Event
            // to update Format changes such as Audio_Stream_SelectedItem
            // -------------------------
            cboFormat_MediaType_SelectionChanged(cboFormat_MediaType, null);

            // -------------------------
            // Set Video and AudioCodec Combobox to "Copy" if 
            // Input File Extension is Same as Output File Extension 
            // and Quality is Auto
            // -------------------------
            //VideoControls.AutoCopyVideoCodec("control");
            //SubtitleControls.AutoCopySubtitleCodec("control");
            //AudioControls.AutoCopyAudioCodec("control");
        }



        /// <summary>
        /// Media Type - Combobox
        /// </summary>
        private void cboFormat_MediaType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Controls.Format.Controls.MediaTypeControls(); // Enabled / Disabled

            Controls.Format.Controls.MediaTypeControls_SelectedItems(); // Selected Items
        }


        /// <summary>
        /// Cut Combobox
        /// </summary>
        private void cboFormat_Cut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Controls.Format.Controls.CutControls();
        }

        /// <summary>
        /// Cut Start - Textbox
        /// </summary>
        // -------------------------
        // Cut Start - Hours - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutStartHours_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutStartHours.Focus() == true &&
                VM.FormatView.Format_CutStart_Hours_Text == "00")
            {
                VM.FormatView.Format_CutStart_Hours_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutStartHours_LostFocus(object sender, RoutedEventArgs e)
        {
            VM.FormatView.Format_CutStart_Hours_Text = tbxCutStartHours.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutStart_Hours_Text))
            {
                VM.FormatView.Format_CutStart_Hours_Text = "00";
            }
        }
        // Key Down
        private void tbxCutStartHours_KeyDown(object sender, KeyEventArgs e)
        {
            Allow_Only_Number_Keys(e);
        }

        // -------------------------
        // Cut Start - Minutes - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutStartMinutes_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutStartMinutes.Focus() == true &&
                VM.FormatView.Format_CutStart_Minutes_Text == "00")
            {
                VM.FormatView.Format_CutStart_Minutes_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutStartMinutes_LostFocus(object sender, RoutedEventArgs e)
        {
            VM.FormatView.Format_CutStart_Minutes_Text = tbxCutStartMinutes.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutStart_Minutes_Text))
            {
                VM.FormatView.Format_CutStart_Minutes_Text = "00";
            }
        }
        // Key Down
        private void tbxCutStartMinutes_KeyDown(object sender, KeyEventArgs e)
        {
            Allow_Only_Number_Keys(e);
        }

        // -------------------------
        // Cut Start - Seconds - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutStartSeconds_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutStartSeconds.Focus() == true &&
                VM.FormatView.Format_CutStart_Seconds_Text == "00")
            {
                VM.FormatView.Format_CutStart_Seconds_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutStartSeconds_LostFocus(object sender, RoutedEventArgs e)
        {
            VM.FormatView.Format_CutStart_Seconds_Text = tbxCutStartSeconds.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutStart_Seconds_Text))
            {
                VM.FormatView.Format_CutStart_Seconds_Text = "00";
            }
        }
        // Key Down
        private void tbxCutStartSeconds_KeyDown(object sender, KeyEventArgs e)
        {
            Allow_Only_Number_Keys(e);
        }

        // -------------------------
        // Cut Start - Milliseconds - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutStartMilliseconds_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutStartMilliseconds.Focus() == true &&
                VM.FormatView.Format_CutStart_Milliseconds_Text == "000")
            {
                VM.FormatView.Format_CutStart_Milliseconds_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutStartMilliseconds_LostFocus(object sender, RoutedEventArgs e)
        {
            VM.FormatView.Format_CutStart_Milliseconds_Text = tbxCutStartMilliseconds.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutStart_Milliseconds_Text))
            {
                VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
            }
        }
        // Key Down
        private void tbxCutStartMilliseconds_KeyDown(object sender, KeyEventArgs e)
        {
            Allow_Only_Number_Keys(e);
        }

        /// <summary>
        /// Cut End - Textbox
        /// </summary>
        // -------------------------
        // Cut End - Hours - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutEndHours_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutEndHours.Focus() == true &&
                VM.FormatView.Format_CutEnd_Hours_Text == "00")
            {
                VM.FormatView.Format_CutEnd_Hours_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutEndHours_LostFocus(object sender, RoutedEventArgs e)
        {
            VM.FormatView.Format_CutEnd_Hours_Text = tbxCutEndHours.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutEnd_Hours_Text))
            {
                VM.FormatView.Format_CutEnd_Hours_Text = "00";
            }
        }
        // Key Down
        private void tbxCutEndHours_KeyDown(object sender, KeyEventArgs e)
        {
            Allow_Only_Number_Keys(e);
        }

        // -------------------------
        // Cut End - Minutes - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutEndMinutes_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutEndMinutes.Focus() == true &&
                VM.FormatView.Format_CutEnd_Minutes_Text == "00")
            {
                VM.FormatView.Format_CutEnd_Minutes_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutEndMinutes_LostFocus(object sender, RoutedEventArgs e)
        {
            VM.FormatView.Format_CutEnd_Minutes_Text = tbxCutEndMinutes.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutEnd_Minutes_Text))
            {
                VM.FormatView.Format_CutEnd_Minutes_Text = "00";
            }
        }
        // Key Down
        private void tbxCutEndMinutes_KeyDown(object sender, KeyEventArgs e)
        {
            Allow_Only_Number_Keys(e);
        }

        // -------------------------
        // Cut End - Seconds - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutEndSeconds_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutEndSeconds.Focus() == true &&
                VM.FormatView.Format_CutEnd_Seconds_Text == "00")
            {
                VM.FormatView.Format_CutEnd_Seconds_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutEndSeconds_LostFocus(object sender, RoutedEventArgs e)
        {
            VM.FormatView.Format_CutEnd_Seconds_Text = tbxCutEndSeconds.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutEnd_Seconds_Text))
            {
                VM.FormatView.Format_CutEnd_Seconds_Text = "00";
            }
        }
        // Key Down
        private void tbxCutEndSeconds_KeyDown(object sender, KeyEventArgs e)
        {
            Allow_Only_Number_Keys(e);
        }

        // -------------------------
        // Cut End - Milliseconds - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutEndMilliseconds_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutEndMilliseconds.Focus() == true &&
                VM.FormatView.Format_CutEnd_Milliseconds_Text == "000")
            {
                VM.FormatView.Format_CutEnd_Milliseconds_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutEndMilliseconds_LostFocus(object sender, RoutedEventArgs e)
        {
            VM.FormatView.Format_CutEnd_Milliseconds_Text = tbxCutEndMilliseconds.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutEnd_Milliseconds_Text))
            {
                VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
            }
        }
        // Key Down
        private void tbxCutEndMilliseconds_KeyDown(object sender, KeyEventArgs e)
        {
            Allow_Only_Number_Keys(e);
        }


        // -------------------------
        // Frame Start Textbox Change
        // -------------------------
        // Got Focus
        private void tbxFrameStart_GotFocus(object sender, RoutedEventArgs e)
        {
        }
        // Lost Focus
        private void tbxFrameStart_LostFocus(object sender, RoutedEventArgs e)
        {
        }
        // Key Down
        private void tbxFrameStart_KeyDown(object sender, KeyEventArgs e)
        {
            Allow_Only_Number_Keys(e);
        }

        // -------------------------
        // Frame End Textbox Change
        // -------------------------
        // Got Focus
        private void tbxFrameEnd_GotFocus(object sender, RoutedEventArgs e)
        {
        }
        // Lost Focus
        private void tbxFrameEnd_LostFocus(object sender, RoutedEventArgs e)
        {
        }
        // Key Down
        private void tbxFrameEnd_KeyDown(object sender, KeyEventArgs e)
        {
            Allow_Only_Number_Keys(e);
        }


        /// <summary>
        /// YouTube Download Check (Method)
        /// </summary>
        /// <remarks>
        /// Check if youtube-dl.exe is on Computer 
        /// </remarks>
        public static bool YouTubeDownloadCheck()
        {
            if (File.Exists(youtubedl))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// YouTube Download - URL (Method)
        /// </summary>
        public static String YouTubeDownloadURL(string url)
        {
            // Strip URL Parameters
            int index = url.IndexOf("&");
            if (index > 0)
                url = url.Substring(0, index);

            return url;
        }


        /// <summary>
        /// YouTube Download - Format (Method)
        /// </summary>
        /// <remarks>
        /// For YouTube downloads - use mp4, as most users can play this format on their PC
        /// For Other Websites - use mkv for merging format, in case the video+audio codecs can't be merged to mp4
        /// </remarks>
        public static String YouTubeDownloadFormat(string youtubedl_SelectedItem,
                                                   string videoCodec_SelectedItem,
                                                   string subtitleCodec_SelectedItem,
                                                   string audioCodec_SelectedItem
                                                   )
        {
            // Video + Audio
            if (youtubedl_SelectedItem == "Video + Audio")
            {
                // Use mp4 for Download-Only Mode
                if (IsWebDownloadOnly(videoCodec_SelectedItem,
                                      subtitleCodec_SelectedItem,
                                      audioCodec_SelectedItem) == true
                                      )
                {
                    return "mp4";
                }

                // Use mkv for converting
                else
                {
                    return "mkv";
                }
            }

            // Video Only
            else if (youtubedl_SelectedItem == "Video Only")
            {
                // Use mp4 for Download-Only Mode
                if (IsWebDownloadOnly(videoCodec_SelectedItem,
                                      subtitleCodec_SelectedItem,
                                      audioCodec_SelectedItem) == true
                                      )
                {
                    return "mp4";
                }

                // Use mkv for converting
                else
                {
                    return "mkv";
                }
            }

            // Audio Only
            else if (youtubedl_SelectedItem == "Audio Only")
            {
                // Can only use m4a, not mp3

                return "m4a";
            }

            return string.Empty;
        }


        /// <summary>
        /// YouTube Download - Quality (Method)
        /// </summary>
        public static String YouTubeDownloadQuality(string input_Text,
                                                    string youtubedl_SelectedItem,
                                                    string youtubedl_Quality_SelectedItem
                                                    )
        {
            // -------------------------
            // Video + Audio
            // -------------------------
            if (youtubedl_SelectedItem == "Video + Audio")
            {
                switch (youtubedl_Quality_SelectedItem)
                {
                    // Best
                    case "best":
                        return "bestvideo[ext=mp4]+bestaudio[ext=m4a]/bestvideo+bestaudio/best";

                    // Best 4K
                    case "best 4K":
                        return "bestvideo[height=2160][ext=mp4]+bestaudio[ext=m4a]/bestvideo[height=2160]+bestaudio/bestvideo[ext=mp4]+bestaudio[ext=m4a]/bestvideo+bestaudio/best";

                    // Best 1080p
                    case "best 1080p":
                        return "bestvideo[height=1080][ext=mp4]+bestaudio[ext=m4a]/bestvideo[height=1080]+bestaudio/bestvideo[ext=mp4]+bestaudio[ext=m4a]/bestvideo+bestaudio/best";

                    // Best 720p
                    case "best 720p":
                        return "bestvideo[height=720][ext=mp4]+bestaudio[ext=m4a]/bestvideo[height=720]+bestaudio/bestvideo[ext=mp4]+bestaudio[ext=m4a]/bestvideo+bestaudio/best";

                    // Best 480p
                    case "best 480p":
                        return "bestvideo[height=480][ext=mp4]+bestaudio[ext=m4a]/bestvideo[height=480]+bestaudio/bestvideo[ext=mp4]+bestaudio[ext=m4a]/bestvideo+bestaudio/best";

                    // Worst
                    case "worst":
                        return "worstvideo[ext=mp4]+worstaudio[ext=m4a]/worstvideo+worstaudio/worst";
                }
            }

            // -------------------------
            // Video Only
            // -------------------------
            else if (youtubedl_SelectedItem == "Video Only")
            {
                switch (youtubedl_Quality_SelectedItem)
                {
                    // Best
                    case "best":
                        return "bestvideo[ext=mp4]/bestvideo";

                    // Best 4K
                    case "best 4K":
                        return "bestvideo[height=2160][ext=mp4]/bestvideo[ext=mp4]/bestvideo";

                    // Best 1080p
                    case "best 1080p":
                        return "bestvideo[height=1080][ext=mp4]/bestvideo[ext=mp4]/bestvideo";

                    // Best 720p
                    case "best 720p":
                        return "bestvideo[height=720p][ext=mp4]/bestvideo[ext=mp4]/bestvideo";

                    // Best 480p
                    case "best 480p":
                        return "bestvideo[height=480p][ext=mp4]/bestvideo[ext=mp4]/bestvideo";

                    // Worst
                    case "worst":
                        return "worstvideo[ext=mp4]/worstvideo";

                }
            }

            // -------------------------
            // Audio Only
            // -------------------------
            else if (youtubedl_SelectedItem == "Audio Only")
            {
                // Best
                if (youtubedl_Quality_SelectedItem == "best")
                {
                    return "bestaudio[ext=m4a]/bestaudio";
                }
                // Worst
                else if (youtubedl_Quality_SelectedItem == "worst")
                {
                    return "worstaudio[ext=m4a]/worstaudio";
                }
            }


            return string.Empty;
        }


        /// <summary>
        /// YouTube Method - Selection Changed
        /// </summary>
        private void cboYouTube_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Video + Audio
            // Video Only
            if (VM.FormatView.Format_YouTube_SelectedItem == "Video + Audio" ||
                VM.FormatView.Format_YouTube_SelectedItem == "Video Only")
            {
                // Change Items Source
                VM.FormatView.Format_YouTube_Quality_Items = new ObservableCollection<string>()
                {
                    "best",
                    "best 4K",
                    "best 1080p",
                    "best 720p",
                    "best 480p",
                    "worst"
                };

                // Select Default
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";
            }

            // Audio Only
            else if (VM.FormatView.Format_YouTube_SelectedItem == "Audio Only")
            {
                // Change Items Source
                VM.FormatView.Format_YouTube_Quality_Items = new ObservableCollection<string>()
                {
                    "best",
                    "worst"
                };

                // Select Default
                VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";
            }
        }



        /// <summary>
        /// YouTube Download - Merge Output Format
        /// </summary>
        //public static String YouTubeDownload_MergeOutputFormat()
        //{

        //    if (IsWebDownloadOnly() == true)
        //    {
        //        return string.Empty;
        //    }
        //    else
        //    {
        //        return "--merge-output-format " + YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
        //                                                                VM.VideoView.Video_Codec_SelectedItem,
        //                                                                VM.SubtitleView.Subtitle_Codec_SelectedItem,
        //                                                                VM.AudioView.Audio_Codec_SelectedItem
        //                                                                );
        //    }
        //}


    }
}
