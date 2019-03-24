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
using System.Windows.Forms;
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
        ///     Cut Start
        /// </summary>
        /// <remarks>
        public static String CutStart(string input_Text,
                                      bool batch_IsChecked,
                                      string mediaType_SelectedItem,
                                      string codec_SelectedItem,
                                      string quality_SelectedItem,
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
                if (string.IsNullOrEmpty(frameStart_Text))
                {
                    // Start
                    trimStart = "-ss " + cutStart_Text_Hours.PadLeft(2, '0') + ":" +
                                         cutStart_Text_Minutes.PadLeft(2, '0') + ":" +
                                         cutStart_Text_Seconds.PadLeft(2, '0') + "." +
                                         cutStart_Text_Milliseconds.PadLeft(3, '0');
                }

                // -------------------------
                // Frames
                // -------------------------
                // If Frame Textboxes have Text, but not Default, 
                // use FramesToDecimal Method (Override Time)
                else if (!string.IsNullOrEmpty(frameStart_Text))
                {
                    trimStart = "-ss " + Video.FramesToDecimal(mediaType_SelectedItem,
                                                               codec_SelectedItem,
                                                               quality_SelectedItem,
                                                               frameStart_Text
                                                               );
                }
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
        ///     Cut End
        /// </summary>
        /// <remarks>
        public static String CutEnd(string input_Text,
                                    bool batch_IsChecked,
                                    string mediaType_SelectedItem,
                                    string codec_SelectedItem,
                                    string quality_SelectedItem,
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
                    if (string.IsNullOrEmpty(frameEnd_Text))
                    {
                        // End
                        trimEnd = "-to " + cutEnd_Text_Hours.PadLeft(2, '0') + ":" +
                                           cutEnd_Text_Minutes.PadLeft(2, '0') + ":" +
                                           cutEnd_Text_Seconds.PadLeft(2, '0') + "." +
                                           cutEnd_Text_Milliseconds.PadLeft(3, '0');
                    }

                    // -------------------------
                    // Frames
                    // -------------------------
                    // If Frame Textboxes have Text, but not Default, 
                    // use FramesToDecimal Method (Override Time)
                    else if (!string.IsNullOrEmpty(frameEnd_Text))
                    {
                        trimEnd = "-to " + Video.FramesToDecimal(mediaType_SelectedItem,
                                                                 codec_SelectedItem,
                                                                 quality_SelectedItem,
                                                                 frameEnd_Text
                                                                 );
                    }
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


        /// <summary>
        ///     Cut
        /// </summary>
        /// <remarks>
        //public static String CutEnd(string input_Text,
        //                            bool batch_IsChecked,
        //                            string mediaType_SelectedItem,
        //                            string codec_SelectedItem,
        //                            string quality_SelectedItem,
        //                            string cut_SelectedItem,
        //                            string cutStart_Text_Hours,
        //                            string cutStart_Text_Minutes,
        //                            string cutStart_Text_Seconds,
        //                            string cutStart_Text_Milliseconds,
        //                            string cutEnd_Text_Hours,
        //                            string cutEnd_Text_Minutes,
        //                            string cutEnd_Text_Seconds,
        //                            string cutEnd_Text_Milliseconds,
        //                            bool frameEnd_IsEnabled,
        //                            string frameStart_Text,
        //                            string frameEnd_Text
        //    )
        //{
        //    // -------------------------
        //    // Yes
        //    // -------------------------
        //    if (cut_SelectedItem == "Yes")
        //    {
        //        // Start
        //        trimStart = cutStart_Text_Hours.PadLeft(2, '0') + ":" +
        //                    cutStart_Text_Minutes.PadLeft(2, '0') + ":" +
        //                    cutStart_Text_Seconds.PadLeft(2, '0') + "." +
        //                    cutStart_Text_Milliseconds.PadLeft(3, '0');

        //        // End
        //        trimEnd = cutEnd_Text_Hours.PadLeft(2, '0') + ":" +
        //                  cutEnd_Text_Minutes.PadLeft(2, '0') + ":" +
        //                  cutEnd_Text_Seconds.PadLeft(2, '0') + "." +
        //                  cutEnd_Text_Milliseconds.PadLeft(3, '0');

        //        // --------------------------------------------------
        //        // Video
        //        // --------------------------------------------------
        //        if (mediaType_SelectedItem == "Video")
        //        {
        //            // -------------------------
        //            // Time
        //            // -------------------------
        //            // If Frame Textboxes Default Use Time
        //            if (string.IsNullOrEmpty(frameStart_Text) ||
        //                string.IsNullOrEmpty(frameEnd_Text))
        //            {
        //                // If End Time is Empty, Default to Full Duration
        //                // Input Null Check
        //                if (!string.IsNullOrEmpty(input_Text))
        //                {
        //                    if (trimEnd == "00:00:00.000" ||
        //                        trimEnd == "::." ||
        //                        string.IsNullOrEmpty(trimEnd))
        //                    {
        //                        // Ignore if Both Start and End are 0
        //                        //if (trimStart != "00:00:00.000" &&
        //                        //    trimEnd != "00:00:00.000")
        //                        //{
        //                        trimEnd = FFprobe.CutDuration(input_Text,
        //                                                  batch_IsChecked
        //                                                  );
        //                        //}
        //                    }
        //                }
        //            }

        //            // -------------------------
        //            // Frames
        //            // -------------------------
        //            // If Frame Textboxes have Text, but not Default, 
        //            // use FramesToDecimal Method (Override Time)
        //            else if (!string.IsNullOrEmpty(frameStart_Text) &&
        //                     !string.IsNullOrEmpty(frameEnd_Text))
        //            {
        //                Video.FramesToDecimal(mediaType_SelectedItem,
        //                                      codec_SelectedItem,
        //                                      quality_SelectedItem,
        //                                      frameEnd_IsEnabled,
        //                                      frameStart_Text,
        //                                      frameEnd_Text
        //                                      );
        //            }


        //            // Combine
        //            trim = "-ss " + trimStart + " -to " + trimEnd;
        //        }

        //        // --------------------------------------------------
        //        // Audio
        //        // --------------------------------------------------
        //        else if (mediaType_SelectedItem == "Audio")
        //        {
        //            // If End Time is Empty, Default to Full Time Duration
        //            if (!string.IsNullOrEmpty(input_Text)) // Input Null Check
        //            {
        //                if (trimEnd == "00:00:00.000" ||
        //                    trimEnd == "::." ||
        //                    string.IsNullOrEmpty(trimEnd))
        //                {
        //                    // Ignore if Both Start and End are 0
        //                    //if (trimStart != "00:00:00.000" &&
        //                    //    trimEnd != "00:00:00.000")
        //                    //{
        //                    trimEnd = FFprobe.CutDuration(input_Text,
        //                                                  batch_IsChecked
        //                                                  );
        //                    //}
        //                }
        //            }

        //            // Combine
        //            trim = "-ss " + trimStart + " -to " + trimEnd;
        //        }

        //        // -------------------------
        //        // Image
        //        // -------------------------
        //        else if (mediaType_SelectedItem == "Image")
        //        {
        //            // -------------------------
        //            // Time
        //            // -------------------------
        //            // If Frame Textboxes Default Use Time
        //            if (string.IsNullOrEmpty(frameStart_Text) ||
        //                string.IsNullOrEmpty(frameEnd_Text))
        //            {
        //                trimEnd = string.Empty;
        //            }

        //            // -------------------------
        //            // Frames
        //            // -------------------------
        //            // If Frame Textboxes have Text, but not Default, use FramesToDecimal Method (Override Time)
        //            else if (!string.IsNullOrEmpty(frameStart_Text) &&
        //                     !string.IsNullOrEmpty(frameEnd_Text))
        //            {
        //                Video.FramesToDecimal(mediaType_SelectedItem,
        //                                      codec_SelectedItem,
        //                                      quality_SelectedItem,
        //                                      frameEnd_IsEnabled,
        //                                      frameStart_Text,
        //                                      frameEnd_Text
        //                                      );
        //            }

        //            trim = "-ss " + trimStart;
        //        }

        //        // --------------------------------------------------
        //        // Image Sequence
        //        // --------------------------------------------------
        //        else if (mediaType_SelectedItem == "Sequence")
        //        {

        //            // -------------------------
        //            // Time
        //            // -------------------------
        //            // If Frame Textboxes Default Use Time
        //            if (string.IsNullOrEmpty(frameStart_Text) ||
        //                string.IsNullOrEmpty(frameEnd_Text))
        //            {
        //                // If End Time is Empty, Default to Full Duration
        //                // Input Null Check
        //                if (!string.IsNullOrEmpty(input_Text))
        //                {
        //                    if (trimEnd == "00:00:00.000" ||
        //                        trimEnd == "::." ||
        //                        string.IsNullOrEmpty(trimEnd))
        //                    {
        //                        // Ignore if Both Start and End are 0
        //                        //if (trimStart != "00:00:00.000" &&
        //                        //    trimEnd != "00:00:00.000")
        //                        //{
        //                        trimEnd = FFprobe.CutDuration(input_Text,
        //                                                  batch_IsChecked
        //                                                  );
        //                        //}
        //                    }
        //                }
        //            }

        //            // -------------------------
        //            // Frames
        //            // -------------------------
        //            // If Frame Textboxes have Text, but not Default, use FramesToDecimal Method (Override Time)
        //            else if (!string.IsNullOrEmpty(frameStart_Text) &&
        //                     !string.IsNullOrEmpty(frameEnd_Text))
        //            {
        //                Video.FramesToDecimal(mediaType_SelectedItem,
        //                                      codec_SelectedItem,
        //                                      quality_SelectedItem,
        //                                      frameEnd_IsEnabled,
        //                                      frameStart_Text,
        //                                      frameEnd_Text
        //                                      );
        //            }

        //            trim = "-ss " + trimStart + " -to " + trimEnd;
        //        }
        //    }

        //    // -------------------------
        //    // No
        //    // -------------------------
        //    else if (cut_SelectedItem == "No")
        //    {
        //        trim = string.Empty;
        //    }

        //    // Return Value
        //    return trim;
        //}

    }
}