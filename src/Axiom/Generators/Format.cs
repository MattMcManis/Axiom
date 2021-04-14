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

/* ----------------------------------
 METHODS

 * Force Format
 * Cut Controls
 * Cut Start
 * Cut End
---------------------------------- */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Generate
{
    public class Format
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Global Variables
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        // Cut
        public static string trimStart { get; set; }
        public static string trimEnd { get; set; }
        //public static string trim; // combine trimStart, trimEnd

        // --------------------------------------------------
        // File Formats
        // --------------------------------------------------

        // -------------------------
        // Video
        // -------------------------
        // Sort by Descending Order
        public static IEnumerable<string> VideoExtensions = new List<string>()
        {
            ".yuv",
            ".x265",
            ".x264",
            ".wmv",
            ".webm",
            ".vob",
            ".svi",
            ".roq",
            ".rmvb",
            ".rm",
            ".qt",
            ".ogv",
            ".ogg",
            ".nsv",
            ".mxf",
            ".mpv",
            ".mpg",
            ".mpg",
            ".mpeg",
            ".mpe",
            ".mp4",
            ".mp2",
            ".mov",
            ".mng",
            ".mkv",
            ".m4v",
            ".m4p",
            ".m2v",
            ".gifv",
            ".gif",
            ".flv",
            ".f4v",
            ".f4p",
            ".f4b",
            ".f4a",
            ".drc",
            ".avi",
            ".asf",
            ".amv",
            ".3gp",
            ".3g2",
        };

        // Video Entry Type Stream
        public static IEnumerable<string> VideoExtensions_EntryType_Stream = new List<string>()
        {
            ".wmv",
            ".webm",
            ".swf",
            ".qt",
            ".ogv",
            ".mp4",
            ".mov",
            ".m4v",
            ".flv",
            ".3gp",
            ".3g2",
        };

        // Video Entry Type Format
        public static IEnumerable<string> VideoExtensions_EntryType_Format = new List<string>()
        {
            ".vob",
            ".mpg",
            ".mpeg",
            ".mod",
            ".mkv",
            ".avi",
            ".asf",
        };

        // -------------------------
        // Audio
        // -------------------------
        // Sort by Descending Order
        public static IEnumerable<string> AudioExtensions = new List<string>()
        {
            ".wv",
            ".wma",
            ".wav",
            ".vox",
            ".tta",
            ".sln",
            ".rm",
            ".raw",
            ".ra",
            ".opus",
            ".ogg",
            ".oga",
            ".msv",
            ".mpc",
            ".mp3",
            ".mmf",
            ".m4p",
            ".m4b",
            ".m4a",
            ".ivs",
            ".iklax",
            ".gsm",
            ".flac",
            ".dvf",
            ".dss",
            ".dct",
            ".awb",
            ".au",
            ".ape",
            ".amr",
            ".aiff",
            ".act",
            ".aax",
            ".aac",
            ".aa",
            ".8svx"
        };

        // -------------------------
        // Video Formats
        // -------------------------
        // Sort by Descending Order
        public static IEnumerable<string> VideoFormats = new List<string>()
        {
            "yuv",
            "x265",
            "x264",
            "wmv",
            "webm",
            "vob",
            "ts",
            "swf",
            "svi",
            "roq",
            "rmvb",
            "rm",
            "qt",
            "ogv",
            "ogg",
            "nsv",
            "mxf",
            "mpv",
            "mpg",
            "mpeg",
            "mpe",
            "mp4",
            "mp2",
            "mov",
            "mod",
            "mng",
            "mkv",
            "m4v",
            "m4p",
            "m2v",
            "gifv",
            "gif",
            "flv",
            "f4v",
            "f4p",
            "f4b",
            "f4a",
            "drc",
            "avi",
            "asf",
            "amv",
            "3gp",
            "3g2",
        };

        // -------------------------
        // Audio Formats
        // -------------------------
        // Sort by Descending Order
        public static IEnumerable<string> AudioFormats = new List<string>()
        {
            "wv",
            "wma",
            "wav",
            "vox",
            "tta",
            "sln",
            "rm",
            "raw",
            "ra",
            "opus",
            "ogg",
            "oga",
            "msv",
            "mpc",
            "mp3",
            "mmf",
            "m4p",
            "m4b",
            "m4a",
            "ivs",
            "iklax",
            "gsm",
            "flac",
            "dvf",
            "dss",
            "dct",
            "awb",
            "au",
            "ape",
            "amr",
            "aiff",
            "act",
            "aax",
            "aac",
            "aa",
            "8svx",

            // Image
            "bmp",
            "cr2",
            "crw",
            "eps",
            "exif",
            "gif",
            "heif",
            "jfif",
            "jpeg",
            "jpeg2000",
            "jpg",
            "pbm",
            "pdf",
            "pgm",
            "png",
            "pnm",
            "ppm",
            "psd",
            "svg",
            "tiff",
            "webp",
        };

        // -------------------------
        // Image Formats
        // -------------------------
        // Sort by Descending Order
        public static IEnumerable<string> ImageFormats = new List<string>()
        {
            "webp",
            "tiff",
            "svg",
            "psd",
            "ppm",
            "pnm",
            "png",
            "pgm",
            "pdf",
            "pbm",
            "jpg",
            "jpeg2000",
            "jpeg",
            "jfif",
            "heif",
            "gif",
            "exif",
            "eps",
            "crw",
            "cr2",
            "bmp",
        };


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Methods
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Force Format
        /// </summary>
        // Used for Two-Pass, Pass 1
        public static String ForceFormat(string container_SelectedItem)
        {
            string format = string.Empty;

            // --------------------------------------------------
            // Video
            // --------------------------------------------------
            switch (container_SelectedItem)
            {
                // -------------------------
                // webm
                // -------------------------
                case "webm":
                    format = "-f webm";
                    break;

                // -------------------------
                // mp4
                // -------------------------
                case "mp4":
                    format = "-f mp4";
                    break;

                // -------------------------
                // mkv
                // -------------------------
                case "mkv":
                    format = "-f matroska";
                    break;

                // -------------------------
                // m2v
                // -------------------------
                case "m2v":
                    format = "-f mpeg2video";
                    break;

                // -------------------------
                // mpg
                // -------------------------
                case "mpg":
                    format = "-f mpeg";
                    break;

                // -------------------------
                // avi
                // -------------------------
                case "avi":
                    format = "-f avi";
                    break;

                // -------------------------
                // ogv
                // -------------------------
                case "ogv":
                    format = "-f ogv";
                    break;

                // --------------------------------------------------
                // Image
                // --------------------------------------------------
                // -------------------------
                // jpg
                // -------------------------
                case "jpg":
                    // do not use
                    break;

                // -------------------------
                // png
                // -------------------------
                case "png":
                    // do not use
                    break;

                // -------------------------
                // webp
                // -------------------------
                case "webp":
                    format = "-f webp";
                    break;

                // --------------------------------------------------
                // Audio
                // --------------------------------------------------
                // -------------------------
                // mp3
                // -------------------------
                case "mp3":
                    format = "-f mp3";
                    break;

                // -------------------------
                // m4a
                // -------------------------
                case "m4a":
                    format = string.Empty;
                    break;

                // -------------------------
                // ogg
                // -------------------------
                case "ogg":
                    format = "-f ogg";
                    break;

                // -------------------------
                // flac
                // -------------------------
                case "flac":
                    format = "-f flac";
                    break;

                // -------------------------
                // wav
                // -------------------------
                case "wav":
                    format = "-f wav";
                    break;
            }

            return format;
        }


        /// <summary>
        /// Cut Start
        /// </summary>
        public static String CutStart(string input_Text,
                                      bool batch_IsChecked,
                                      string cut_SelectedItem,
                                      string cutStart_Text_Hours,
                                      string cutStart_Text_Minutes,
                                      string cutStart_Text_Seconds,
                                      string cutStart_Text_Milliseconds,
                                      string frameStart_Text
            )
        {
            // -------------------------
            // Yes
            // -------------------------
            if (cut_SelectedItem == "Yes")
            {
                // -------------------------
                // Time
                // -------------------------
                // If Frame Textboxes Default Use Time
                if (string.IsNullOrWhiteSpace(frameStart_Text))
                {
                    // Start
                    trimStart = cutStart_Text_Hours.PadLeft(2, '0') + ":" +
                                cutStart_Text_Minutes.PadLeft(2, '0') + ":" +
                                cutStart_Text_Seconds.PadLeft(2, '0') + "." +
                                cutStart_Text_Milliseconds.PadLeft(3, '0');
                }

                // -------------------------
                // Frames
                // -------------------------
                // If Frame Textboxes have Text, but not Default, 
                // use FramesToDecimal Method (Override Time)
                else if (!string.IsNullOrWhiteSpace(frameStart_Text))
                {
                    trimStart = Video.Video.FramesToDecimal(frameStart_Text);
                }


                trimStart = "-ss " + trimStart;
            }

            // -------------------------
            // No
            // -------------------------
            else if (cut_SelectedItem == "No")
            {
                trimStart = string.Empty;
            }

            // Return Value
            return trimStart;
        }


        /// <summary>
        /// Cut End
        /// </summary>
        public static String CutEnd(string input_Text,
                                    bool batch_IsChecked,
                                    string mediaType_SelectedItem,
                                    string cut_SelectedItem,
                                    string cutEnd_Text_Hours,
                                    string cutEnd_Text_Minutes,
                                    string cutEnd_Text_Seconds,
                                    string cutEnd_Text_Milliseconds,
                                    string frameEnd_Text
            )
        {
            // -------------------------
            // Yes
            // -------------------------
            if (cut_SelectedItem == "Yes")
            {
                // Video, Image Sequence, Audio
                // Image only has Start, no End
                if (mediaType_SelectedItem != "Image")
                {
                    // -------------------------
                    // Time
                    // -------------------------
                    // If Frame Textboxes Default Use Time
                    if (string.IsNullOrWhiteSpace(frameEnd_Text))
                    {
                        // End
                        trimEnd = cutEnd_Text_Hours.PadLeft(2, '0') + ":" +
                                  cutEnd_Text_Minutes.PadLeft(2, '0') + ":" +
                                  cutEnd_Text_Seconds.PadLeft(2, '0') + "." +
                                  cutEnd_Text_Milliseconds.PadLeft(3, '0');

                        // If End Time is Empty, Default to Full Duration
                        // Input Null Check
                        if (!string.IsNullOrWhiteSpace(input_Text))
                        {
                            if (trimEnd == "00:00:00.000" ||
                                string.IsNullOrWhiteSpace(trimEnd))
                            {
                                trimEnd = Analyze.FFprobe.CutDuration(input_Text, batch_IsChecked);
                            }
                        }

                    }

                    // -------------------------
                    // Frames
                    // -------------------------
                    // If Frame Textboxes have Text, but not Default, 
                    // use FramesToDecimal Method (Override Time)
                    else if (!string.IsNullOrWhiteSpace(frameEnd_Text))
                    {
                        trimEnd = Video.Video.FramesToDecimal(frameEnd_Text);
                    }


                    trimEnd = "-to " + trimEnd;
                }
            }

            // -------------------------
            // No
            // -------------------------
            else if (cut_SelectedItem == "No")
            {
                trimEnd = string.Empty;
            }

            // Return Value
            return trimEnd;
        }


    }
}
