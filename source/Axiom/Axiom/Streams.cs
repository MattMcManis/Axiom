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
using System.Data;
using System.Linq;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class Streams
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Variables
        /// <summary>
        // --------------------------------------------------------------------------------------------------------
        public static string vMap; // video streams
        public static string cMap; // video chapters
        public static string sMap; // video subtitles
        public static string aMap; // audio streams
        public static string mMap; // file metadata
        //public static string map; // combines all maps


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Process Methods
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Video Stream Maps (Method)
        /// </summary>
        public static String VideoStreamMaps(MainWindow mainwindow)
        {
            // WARNING: If a map is enabled, all other map types must be specified or they will be removed !!!
            // Question Mark ? = ignore warnings

            // --------------------------------------------------------------------
            // Video Map
            // --------------------------------------------------------------------
            // -------------------------
            // Video Formats
            // -------------------------
            if ((string)mainwindow.cboFormat.SelectedItem == "webm")
            {
                vMap = "-map 0:v:0?"; // only video track 1
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
            {
                vMap = "-map 0:v:0?"; // only video track 1
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
            {
                vMap = "-map 0:v?"; // all video tracks
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "avi")
            {
                vMap = "-map 0:v:0?"; // only video track 1
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
            {
                vMap = "-map 0:v?"; // all video tracks
            }
            // -------------------------
            // Non-Video Formats
            // -------------------------
            else
            {
                vMap = string.Empty; // do not copy video map
            }

            // -------------------------
            // Video Codecs
            // -------------------------
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "None")
            {
                vMap = "-vn"; // only video track 1
            }

            // -------------------------
            // Remove Video Map if Input File is Audio Format
            // -------------------------
            if (Format.AudioFormats.Any(s => s.Equals(MainWindow.inputExt, StringComparison.OrdinalIgnoreCase))
                || Format.AudioFormats.Any(s => s.Equals(MainWindow.batchExt, StringComparison.OrdinalIgnoreCase)))
            {
                vMap = string.Empty;
            }


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Video Stream: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run("all") { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);



            // --------------------------------------------------------------------
            // Chapters Map
            // --------------------------------------------------------------------
            // -------------------------
            // Video Formats
            // -------------------------
            if ((string)mainwindow.cboFormat.SelectedItem == "webm")
            {
                cMap = "-map_chapters -1"; // remove chapters
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
            {
                cMap = "-map_chapters 0"; // all chapters
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
            {
                cMap = "-map_chapters 0"; // all chapters
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "avi")
            {
                cMap = "-map_chapters 0"; // all chapters
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
            {
                cMap = "-map_chapters 0"; // all chapters
            }
            // -------------------------
            // All Other Formats
            // -------------------------
            else
            {
                cMap = string.Empty; // do not copy chapters
            }

            // -------------------------
            // Remove Chapters Map if Input File is Audio Format
            // -------------------------
            if (Format.AudioFormats.Any(s => s.Equals(MainWindow.inputExt, StringComparison.OrdinalIgnoreCase))
                || Format.AudioFormats.Any(s => s.Equals(MainWindow.batchExt, StringComparison.OrdinalIgnoreCase)))
            {
                cMap = string.Empty;
            }


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Chapters: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(Streams.cMap) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);


            // --------------------------------------------------------------------
            // Combine Maps
            // --------------------------------------------------------------------
            // Make List
            List<string> mapList = new List<string>() { vMap, cMap };
            // Join List with Spaces, Remove Empty Strings
            string map = string.Join(" ", mapList.Where(s => !string.IsNullOrEmpty(s)));


            // Return Value
            return map;
        }


        /// <summary>
        /// Subtitle Maps (Method)
        /// </summary>
        public static String SubtitleMaps(MainWindow mainwindow)
        {
            // --------------------------------------------------------------------
            // Subtitle Map
            // --------------------------------------------------------------------
            // -------------------------
            // None
            // -------------------------
            if ((string)mainwindow.cboSubtitle.SelectedItem == "none")
            {
                sMap = "-sn";
            }
            // -------------------------
            // External
            // -------------------------
            else if ((string)mainwindow.cboSubtitle.SelectedItem == "external")
            {
                // -------------------------
                // Map
                // -------------------------
                List<string> subtitleMapsList = new List<string>();

                if (Video.subtitleFilePathsList.Count > 0)
                {
                    // Give each Subtitle File it's own map
                    for (var i = 0; i < Video.subtitleFilePathsList.Count; i++)
                    {
                        subtitleMapsList.Add("-map " + (i + 1).ToString() + ":s?");
                    }
                }

                // Join multiple maps: -map 1:s? -map 2:s? -map 3:s?
                sMap = string.Join(" ", subtitleMapsList.Where(s => !string.IsNullOrEmpty(s)));


                // -------------------------
                // Default Subtitle
                // -------------------------
                string checkedItem = string.Empty;
                for (var i = 0; i < mainwindow.listViewSubtitles.Items.Count; i++)
                {
                    // If list contains a checked item
                    if (mainwindow.listViewSubtitles.SelectedItems.Contains(mainwindow.listViewSubtitles.Items[i]))
                    {
                        // Get Index Position
                        checkedItem = i.ToString();
                    }
                }

                // Create Default Subtitle
                string disposition = string.Empty;
                if (!string.IsNullOrEmpty(checkedItem))
                {
                    disposition = " \r\n-disposition:s:" + checkedItem + " default";
                }

                // Combine Map + Default
                sMap = sMap + disposition;
            }
            //
            // -------------------------
            // All
            // -------------------------
            else if ((string)mainwindow.cboSubtitle.SelectedItem == "all")
            {
                // Formats
                //
                if ((string)mainwindow.cboFormat.SelectedItem == "webm")
                {
                    sMap = "-sn"; // no subtitles for webm
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                {
                    sMap = "-map 1:s?"; // all subtitles (:? at the end ignores error if subtitle is not available)
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                {
                    sMap = "-map 1:s?"; // all subtitles
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "avi")
                {
                    sMap = "-map 1:s?"; // all subtitles
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                {
                    sMap = "-map 1:s?"; // all subtitles, OGV has problem using Subtitles
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "jpg")
                {
                    sMap = "-sn"; // disable subtitles
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "png")
                {
                    sMap = "-sn"; // disable subtitles
                }
                // Non-Video/Image Formats
                //
                else
                {
                    sMap = "-sn"; // disable subtitles
                }
            }
            // -------------------------
            // Number
            // -------------------------
            else
            {
                // Subtract 1, Map starts at 0
                int sMapNumber = Int32.Parse(mainwindow.cboSubtitle.SelectedItem.ToString()) - 1;

                sMap = "-map 1:s:" + sMapNumber + "?";

                // Image
                if ((string)mainwindow.cboFormat.SelectedItem == "jpg"
                    || (string)mainwindow.cboFormat.SelectedItem == "png")
                {
                    sMap = "-sn";
                }
            }

            // -------------------------
            // Remove Subtitle Map if Input File is Audio Format
            // -------------------------
            if (Format.AudioFormats.Any(s => s.Equals(MainWindow.inputExt, StringComparison.OrdinalIgnoreCase))
                || Format.AudioFormats.Any(s => s.Equals(MainWindow.batchExt, StringComparison.OrdinalIgnoreCase)))
            {
                sMap = string.Empty;
            }


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Subtitle Stream: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(mainwindow.cboSubtitle.SelectedItem.ToString()) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);


            // --------------------------------------------------------------------
            // Combine Maps
            // --------------------------------------------------------------------
            // Make List
            List<string> mapList = new List<string>() { sMap };
            // Join List with Spaces, Remove Empty Strings
            string map = string.Join(" ", mapList.Where(s => !string.IsNullOrEmpty(s)));


            // Return Value
            return map;
        }




        /// <summary>
        /// Audio Stream Maps (Method)
        /// </summary>
        public static String AudioStreamMaps(MainWindow mainwindow)
        {
            // --------------------------------------------------------------------
            // Audio Map
            // --------------------------------------------------------------------
            // Offset by 1. VLC starts with #1, FFprobe starts with #a:0.
            // WARNING: IF Audio Map Enabled, Video Map must also be enabled or no video !!!!!!!!!!

            // -------------------------
            // None
            // -------------------------
            if ((string)mainwindow.cboAudioStream.SelectedItem == "none")
            {
                aMap = "-an";
            }
            // -------------------------
            // All
            // -------------------------
            else if ((string)mainwindow.cboAudioStream.SelectedItem == "all")
            {
                // Video/Image Format
                //
                if ((string)mainwindow.cboFormat.SelectedItem == "webm")
                {
                    aMap = "-map 0:a:0?"; // only audio track 1
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                {
                    aMap = "-map 0:a?"; // all audio tracks 
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                {
                    aMap = "-map 0:a?"; // all audio tracks 
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "avi")
                {
                    aMap = "-map 0:a?"; // all audio tracks 
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                {
                    aMap = "-map 0:a?"; // all audio tracks 
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "jpg")
                {
                    aMap = "-an"; // disable audio
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "png")
                {
                    aMap = "-an"; // disable audio
                }

                // Audio Media Type
                //
                else if ((string)mainwindow.cboMediaType.SelectedItem == "Audio")
                {
                    aMap = "-map 0:a:0?"; // only audio track 1
                }
            }
            // -------------------------
            // Number
            // -------------------------
            else
            {
                // Subtract 1, Map starts at 0
                int aMapNumber = Int32.Parse(mainwindow.cboAudioStream.SelectedItem.ToString()) - 1;

                aMap = "-map 0:a:" + aMapNumber + "?";
            }

            // -------------------------
            // Remove Audio Map if Input File is Audio Format
            // -------------------------
            if (Format.AudioFormats.Any(s => s.Equals(MainWindow.inputExt, StringComparison.OrdinalIgnoreCase))
                || Format.AudioFormats.Any(s => s.Equals(MainWindow.batchExt, StringComparison.OrdinalIgnoreCase)))
            {
                aMap = string.Empty;
            }

            // -------------------------
            // Mute
            // -------------------------
            if ((string)mainwindow.cboAudio.SelectedItem == "Mute")
            {
                aMap = "-an";
            }


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Audio Stream: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(mainwindow.cboAudioStream.SelectedItem.ToString()) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);


            // --------------------------------------------------------------------
            // Combine Maps
            // --------------------------------------------------------------------
            // Make List
            List<string> mapList = new List<string>() { aMap };
            // Join List with Spaces, Remove Empty Strings
            string map = string.Join(" ", mapList.Where(s => !string.IsNullOrEmpty(s)));


            // Return Value
            return map;
        }



        /// <summary>
        /// Format Maps (Method)
        /// </summary>
        public static String FormatMaps(MainWindow mainwindow)
        {
            // --------------------------------------------------------------------
            // Metadata Map
            // --------------------------------------------------------------------
            // Go by Format Container

            // -------------------------
            // MP3
            // -------------------------
            if ((string)mainwindow.cboFormat.SelectedItem == "mp3")
            {
                mMap = "-map_metadata 0 -id3v2_version 3";
            }
            // -------------------------
            // JPG
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "jpg")
            {
                mMap = string.Empty; // do not copy metadata
            }
            // -------------------------
            // PNG
            // -------------------------
            else if ((string)mainwindow.cboFormat.SelectedItem == "png")
            {
                mMap = string.Empty; // do not copy metadata
            }
            // -------------------------
            // All Other Formats
            // -------------------------
            else
            {
                mMap = "-map_metadata 0"; // copy all metadata
            }


            // --------------------------------------------------------------------
            // Combine Maps
            // --------------------------------------------------------------------
            // Make List
            List<string> mapList = new List<string>() { mMap };
            // Join List with Spaces, Remove Empty Strings
            string map = string.Join(" ", mapList.Where(s => !string.IsNullOrEmpty(s)));


            // Return Value
            return map;
        }

    }
}