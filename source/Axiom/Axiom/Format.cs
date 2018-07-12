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
        /// Force Format (Method)
        /// </summary>
        // Used for Two-Pass Pass 1
        public static String ForceFormat(MainWindow mainwindow)
        {
            string format = string.Empty;

            // Video
            if ((string)mainwindow.cboFormat.SelectedItem == "webm")
            {
                format = "-f webm";
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
            {
                format = "-f mp4";
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
            {
                format = "-f matroska";
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "m2v")
            {
                format = "-f mpeg2video";
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mpg")
            {
                format = "-f mpeg";
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "avi")
            {
                format = "-f avi";
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
            {
                format = "-f ogv";
            }

            // Image
            else if ((string)mainwindow.cboFormat.SelectedItem == "jpg")
            {
                // do not use
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "png")
            {
                // do not use
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "webp")
            {
                format = "-f webp";
            }

            // Audio
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp3")
            {
                format = "-f mp3";
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "m4a")
            {
                format = "-f m4a";
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogg")
            {
                format = "-f ogg";
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "flac")
            {
                format = "-f flac";
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "wav")
            {
                format = "-f wav";
            }

            return format;
        }


        /// <summary>
        /// Cut (Method)
        /// </summary>
        public static String Cut(MainWindow mainwindow)
        {
            // -------------------------
            // Yes
            // -------------------------
            // VIDEO
            //
            if ((string)mainwindow.cboCut.SelectedItem == "Yes")
            {
                if ((string)mainwindow.cboMediaType.SelectedItem == "Video")
                {
                    // Use Time
                    // If Frame Textboxes Default Use Time
                    if (mainwindow.frameStart.Text == "Frame"
                        || mainwindow.frameEnd.Text == "Range"
                        || string.IsNullOrWhiteSpace(mainwindow.frameStart.Text)
                        || string.IsNullOrWhiteSpace(mainwindow.frameEnd.Text))
                    {
                        trimStart = mainwindow.cutStart.Text;
                        trimEnd = mainwindow.cutEnd.Text;
                    }

                    // Use Frames
                    // If Frame Textboxes have Text, but not Default, use FramesToDecimal Method (Override Time)
                    else if (mainwindow.frameStart.Text != "Frame"
                        && mainwindow.frameEnd.Text != "Range"
                        && !string.IsNullOrWhiteSpace(mainwindow.frameStart.Text)
                        && !string.IsNullOrWhiteSpace(mainwindow.frameEnd.Text))
                    {
                        Video.FramesToDecimal(mainwindow);
                    }

                    // If End Time is Empty, Default to Full Duration
                    // Input Null Check
                    if (!string.IsNullOrWhiteSpace(mainwindow.tbxInput.Text))
                    {
                        if (mainwindow.cutEnd.Text == "00:00:00.000" || string.IsNullOrWhiteSpace(mainwindow.cutEnd.Text))
                        {
                            trimEnd = FFprobe.CutDuration(mainwindow);
                        }
                    }

                    // Combine
                    trim = "-ss " + trimStart + " " + "-to " + trimEnd;
                }

                // AUDIO
                //
                else if ((string)mainwindow.cboMediaType.SelectedItem == "Audio")
                {
                    trimStart = mainwindow.cutStart.Text;
                    trimEnd = mainwindow.cutEnd.Text;

                    // If End Time is Empty, Default to Full Duration
                    // Input Null Check
                    if (!string.IsNullOrWhiteSpace(mainwindow.tbxInput.Text))
                    {
                        if (mainwindow.cutEnd.Text == "00:00:00.000" || string.IsNullOrWhiteSpace(mainwindow.cutEnd.Text))
                        {
                            trimEnd = FFprobe.CutDuration(mainwindow);
                        }
                    }

                    // Combine
                    trim = "-ss " + trimStart + " " + "-to " + trimEnd;
                }

                // JPEG & PNG Screenshot
                //
                else if ((string)mainwindow.cboMediaType.SelectedItem == "Image")
                {
                    // Use Time
                    // If Frame Textbox Default Use Time
                    if (mainwindow.frameStart.Text == "Frame" || string.IsNullOrWhiteSpace(mainwindow.frameStart.Text))
                    {
                        trimStart = mainwindow.cutStart.Text;
                    }

                    // Use Frames
                    // If Frame Textboxes have Text, but not Default, use FramesToDecimal Method (Override Time)
                    else if (mainwindow.frameStart.Text != "Frame"
                        && mainwindow.frameEnd.Text != "Range"
                        && !string.IsNullOrWhiteSpace(mainwindow.frameStart.Text)
                        && string.IsNullOrWhiteSpace(mainwindow.frameEnd.Text))
                    {
                        Video.FramesToDecimal(mainwindow);
                    }

                    trim = "-ss " + trimStart;
                }

                // JPEG & PNG Sequence
                //
                else if ((string)mainwindow.cboMediaType.SelectedItem == "Sequence")
                {
                    // Use Time
                    // If Frame Textboxes Default Use Time
                    if (mainwindow.frameStart.Text == "Frame"
                        || mainwindow.frameEnd.Text == "Range"
                        || string.IsNullOrWhiteSpace(mainwindow.frameStart.Text)
                        || string.IsNullOrWhiteSpace(mainwindow.frameEnd.Text))
                    {
                        trimStart = mainwindow.cutStart.Text;
                        trimEnd = mainwindow.cutEnd.Text;
                    }

                    // Use Frames
                    // If Frame Textboxes have Text, but not Default, use FramesToDecimal Method (Override Time)
                    else if (mainwindow.frameStart.Text != "Frame"
                        && mainwindow.frameEnd.Text != "Range"
                        && !string.IsNullOrWhiteSpace(mainwindow.frameStart.Text)
                        && !string.IsNullOrWhiteSpace(mainwindow.frameEnd.Text))
                    {
                        Video.FramesToDecimal(mainwindow);
                    }

                    trim = "-ss " + trimStart + " " + "-to " + trimEnd;
                }
            }
   
            // -------------------------
            // No
            // -------------------------
            else if ((string)mainwindow.cboCut.SelectedItem == "No")
            {
                trim = string.Empty;
            }

            // Return Value
            return trim;
        }

    }
}