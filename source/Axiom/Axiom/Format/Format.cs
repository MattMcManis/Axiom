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

using System;
using System.Collections.Generic;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class Format
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Variables
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // Cut
        public static string trimStart;
        public static string trimEnd;
        public static string trim; // combine trimStart, trimEnd

        // --------------------------------------------------
        // File Formats
        // --------------------------------------------------

        // -------------------------
        // Video
        // -------------------------
        public static List<string> VideoFormats = new List<string>()
        {
            ".3g2",
            ".3gp",
            ".amv",
            ".asf",
            ".avi",
            ".drc",
            ".flv",
            ".f4v",
            ".f4p",
            ".f4a",
            ".f4b",
            ".gif",
            ".gifv",
            ".m4v",
            ".mkv",
            ".mng",
            ".mov",
            ".mp4",
            ".m4p",
            ".m4v",
            ".mpg",
            ".mp2",
            ".mpeg",
            ".mpe",
            ".mpv",
            ".mpg",
            ".mpeg",
            ".m2v",
            ".mxf",
            ".nsv",
            ".ogv",
            ".ogg",
            ".rm",
            ".rmvb",
            ".roq",
            ".svi",
            ".vob",
            ".qt",
            ".webm",
            ".wmv",
            ".yuv"
        };

        // Video Entry Type Stream
        public static List<string> VideoFormats_EntryType_Stream = new List<string>()
        {
            ".3g2",
            ".3gp",
            ".flv",
            ".m4v",
            ".mov",
            ".mp4",
            ".ogv",
            ".qt",
            ".swf",
            ".webm",
            ".wmv",
        };

        // Video Entry Type Format
        public static List<string> VideoFormats_EntryType_Format = new List<string>()
        {
            ".asf",
            ".avi",
            ".mkv",
            ".mod",
            ".mpeg",
            ".mpg",
            ".vob",
        };

        // -------------------------
        // Audio
        // -------------------------
        public static List<string> AudioFormats = new List<string>()
        {
            ".aa",
            ".aac",
            ".aax",
            ".act",
            ".aiff",
            ".amr",
            ".ape",
            ".au",
            ".awb",
            ".dct",
            ".dss",
            ".dvf",
            ".flac",
            ".gsm",
            ".iklax",
            ".ivs",
            ".m4a",
            ".m4b",
            ".m4p",
            ".mmf",
            ".mp3",
            ".mpc",
            ".msv",
            ".ogg",
            ".oga",
            ".opus",
            ".ra",
            ".rm",
            ".raw",
            ".sln",
            ".tta",
            ".vox",
            ".wav",
            ".wma",
            ".wv",
            ".8svx"
        };


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Process Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        ///     Force Format
        /// </summary>
        // Used for Two-Pass, Pass 1
        public static String ForceFormat(string container_SelectedItem)
        {
            string format = string.Empty;

            // --------------------------------------------------
            // Video
            // --------------------------------------------------
            // -------------------------
            // webm
            // -------------------------
            if (container_SelectedItem == "webm")
            {
                format = "-f webm";
            }

            // -------------------------
            // mp4
            // -------------------------
            else if (container_SelectedItem == "mp4")
            {
                format = "-f mp4";
            }

            // -------------------------
            // mkv
            // -------------------------
            else if (container_SelectedItem == "mkv")
            {
                format = "-f matroska";
            }

            // -------------------------
            // m2v
            // -------------------------
            else if (container_SelectedItem == "m2v")
            {
                format = "-f mpeg2video";
            }

            // -------------------------
            // mpg
            // -------------------------
            else if (container_SelectedItem == "mpg")
            {
                format = "-f mpeg";
            }

            // -------------------------
            // avi
            // -------------------------
            else if (container_SelectedItem == "avi")
            {
                format = "-f avi";
            }

            // -------------------------
            // ogv
            // -------------------------
            else if (container_SelectedItem == "ogv")
            {
                format = "-f ogv";
            }

            // --------------------------------------------------
            // Image
            // --------------------------------------------------
            // -------------------------
            // jpg
            // -------------------------
            else if (container_SelectedItem == "jpg")
            {
                // do not use
            }

            // -------------------------
            // png
            // -------------------------
            else if (container_SelectedItem == "png")
            {
                // do not use
            }

            // -------------------------
            // webp
            // -------------------------
            else if (container_SelectedItem == "webp")
            {
                format = "-f webp";
            }

            // --------------------------------------------------
            // Audio
            // --------------------------------------------------
            // -------------------------
            // mp3
            // -------------------------
            else if (container_SelectedItem == "mp3")
            {
                format = "-f mp3";
            }

            // -------------------------
            // m4a
            // -------------------------
            else if (container_SelectedItem == "m4a")
            {
                format = string.Empty;
            }

            // -------------------------
            // ogg
            // -------------------------
            else if (container_SelectedItem == "ogg")
            {
                format = "-f ogg";
            }

            // -------------------------
            // flac
            // -------------------------
            else if (container_SelectedItem == "flac")
            {
                format = "-f flac";
            }

            // -------------------------
            // wav
            // -------------------------
            else if (container_SelectedItem == "wav")
            {
                format = "-f wav";
            }

            return format;
        }


        /// <summary>
        ///     Cut
        /// </summary>
        /// <remarks>
        public static String Cut(string input_Text,
                                 bool batch_IsChecked,
                                 string mediaType_SelectedItem,
                                 string codec_SelectedItem,
                                 string quality_SelectedItem,
                                 string cut_SelectedItem,
                                 string cutStart_Text,
                                 string cutEnd_Text,
                                 bool frameEnd_IsEnabled,
                                 string frameStart_Text,
                                 string frameEnd_Text
            )
        {
            // -------------------------
            // Yes
            // -------------------------
            // VIDEO
            //
            if (cut_SelectedItem == "Yes")
            {
                if (mediaType_SelectedItem == "Video")
                {
                    // Use Time
                    // If Frame Textboxes Default Use Time
                    if (frameStart_Text == "Frame" || 
                        frameEnd_Text == "Range" || 
                        string.IsNullOrEmpty(frameStart_Text) ||
                        string.IsNullOrEmpty(frameEnd_Text))
                    {
                        trimStart = cutStart_Text;
                        trimEnd = cutEnd_Text;
                    }

                    // Use Frames
                    // If Frame Textboxes have Text, but not Default, use FramesToDecimal Method (Override Time)
                    else if (frameStart_Text != "Frame" &&
                        frameEnd_Text != "Range" &&
                        !string.IsNullOrEmpty(frameStart_Text) &&
                        !string.IsNullOrEmpty(frameEnd_Text))
                    {
                        Video.FramesToDecimal(mediaType_SelectedItem,
                                              codec_SelectedItem,
                                              quality_SelectedItem,
                                              frameEnd_IsEnabled,
                                              frameStart_Text,
                                              frameEnd_Text
                                              );
                    }

                    // If End Time is Empty, Default to Full Duration
                    // Input Null Check
                    if (!string.IsNullOrEmpty(input_Text))
                    {
                        if (cutEnd_Text == "00:00:00.000" || 
                            string.IsNullOrEmpty(cutEnd_Text))
                        {
                            trimEnd = FFprobe.CutDuration(input_Text, 
                                                          batch_IsChecked
                                                          );
                        }
                    }

                    // Combine
                    trim = "-ss " + trimStart + " " + "-to " + trimEnd;
                }

                // AUDIO
                //
                else if (mediaType_SelectedItem == "Audio")
                {
                    trimStart = cutStart_Text;
                    trimEnd = cutEnd_Text;

                    // If End Time is Empty, Default to Full Duration
                    // Input Null Check
                    if (!string.IsNullOrEmpty(input_Text))
                    {
                        if (cutEnd_Text == "00:00:00.000" || 
                            string.IsNullOrEmpty(cutEnd_Text))
                        {
                            trimEnd = FFprobe.CutDuration(input_Text, 
                                                          batch_IsChecked
                                                          );
                        }
                    }

                    // Combine
                    trim = "-ss " + trimStart + " " + "-to " + trimEnd;
                }

                // JPEG & PNG Screenshot
                //
                else if (mediaType_SelectedItem == "Image")
                {
                    // Use Time
                    // If Frame Textbox Default Use Time
                    if (frameStart_Text == "Frame" || 
                        string.IsNullOrEmpty(frameStart_Text))
                    {
                        trimStart = cutStart_Text;
                    }

                    // Use Frames
                    // If Frame Textboxes have Text, but not Default, use FramesToDecimal Method (Override Time)
                    else if (frameStart_Text != "Frame" &&
                        frameEnd_Text != "Range" &&
                        !string.IsNullOrEmpty(frameStart_Text) &&
                        string.IsNullOrEmpty(frameEnd_Text))
                    {
                        Video.FramesToDecimal(mediaType_SelectedItem,
                                              codec_SelectedItem,
                                              quality_SelectedItem,
                                              frameEnd_IsEnabled,
                                              frameStart_Text,
                                              frameEnd_Text
                                              );
                    }

                    trim = "-ss " + trimStart;
                }

                // JPEG & PNG Sequence
                //
                else if (mediaType_SelectedItem == "Sequence")
                {
                    // Use Time
                    // If Frame Textboxes Default Use Time
                    if (frameStart_Text == "Frame" ||
                        frameEnd_Text == "Range" ||
                        string.IsNullOrEmpty(frameStart_Text) ||
                        string.IsNullOrEmpty(frameEnd_Text))
                    {
                        trimStart = cutStart_Text;
                        trimEnd = cutEnd_Text;
                    }

                    // Use Frames
                    // If Frame Textboxes have Text, but not Default, use FramesToDecimal Method (Override Time)
                    else if (frameStart_Text != "Frame" &&
                        frameEnd_Text != "Range" &&
                        !string.IsNullOrEmpty(frameStart_Text) &&
                        !string.IsNullOrEmpty(frameEnd_Text))
                    {
                        Video.FramesToDecimal(mediaType_SelectedItem,
                                              codec_SelectedItem,
                                              quality_SelectedItem,
                                              frameEnd_IsEnabled,
                                              frameStart_Text,
                                              frameEnd_Text
                                              );
                    }

                    trim = "-ss " + trimStart + " " + "-to " + trimEnd;
                }
            }

            // -------------------------
            // No
            // -------------------------
            else if (cut_SelectedItem == "No")
            {
                trim = string.Empty;
            }

            // Return Value
            return trim;
        }


        /// <summary>
        ///     Cut - Fast Seek
        /// </summary>
        ///     When used as an input option (before -i), seeks in this input file to position. 
        ///     When used as an output option (before an output filename), decodes but discards 
        ///     input until the timestamps reach position. This is slower, but more accurate.
        /// </remarks>
        



    }
}