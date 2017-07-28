using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017 Matt McManis
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
        public static string map; // combines all maps


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Process Methods
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Stream Maps (Method)
        /// </summary>
        public static String StreamMaps(MainWindow mainwindow)
        {
            // WARNING: If a map is enabled, all other map types must be specified or they will be removed !!!!!!!!!
            // Question Mark ? = ignore warnings

            // -------------------------
            // Format
            // -------------------------
            // These Defaults will be overriden by below Map selections
            if ((string)mainwindow.cboFormat.SelectedItem == "webm")
            {
                vMap = "-map 0:v:0?"; // only video track 1
                cMap = "-map_chapters -1"; // remove chapters
                sMap = "-sn"; // no subtitles for webm
                aMap = "-map 0:a:0?"; // only audio track 1
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
            {
                vMap = "-map 0:v:0?"; // only video track 1
                cMap = "-map_chapters 0"; // all chapters
                sMap = "-map 0:s?"; // all subtitles (:? at the end ignores error if subtitle is not available)
                //aMap = "-map 0:a:0?"; // only audio track 1
                aMap = "-map 0:a?"; // all audio tracks 
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
            {
                vMap = "-map 0:v?"; // all video tracks
                cMap = "-map_chapters 0"; // all chapters
                sMap = "-map 0:s?"; // all subtitles
                aMap = "-map 0:a?"; // all audio tracks 
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
            {
                vMap = "-map 0:v?"; // all video tracks
                cMap = "-map_chapters 0"; // all chapters
                sMap = "-map 0:s?"; // all subtitles, OGV has problem using Subtitles
                aMap = "-map 0:a?"; // all audio tracks 
            }


            // -------------------------
            // Video Map
            // -------------------------
            // Always all (empty)


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Video Stream: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run("all") { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);


            // -------------------------
            // Audio Map
            // -------------------------
            // Overrides
            // Offset by 1. VLC starts with #1, FFprobe starts with #a:0.
            // WARNING: IF Audio Map Enabled, Video Map must also be enabled or no video !!!!!!!!!!

            // None
            if ((string)mainwindow.cboAudioStream.SelectedItem == "none")
            {
                aMap = "-an";
            }
            // All
            else if ((string)mainwindow.cboAudioStream.SelectedItem == "all")
            {

            }
            // Number
            else
            {
                // Subtract 1, Map starts at 0
                int aMapNumber = Int32.Parse(mainwindow.cboAudioStream.SelectedItem.ToString()) - 1;

                aMap = "-map 0:a:" + aMapNumber + "?";
            }
            // Mute
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


            // -------------------------
            // Subtitle Map
            // -------------------------
            // Overrides

            // None
            //
            if ((string)mainwindow.cboSubtitle.SelectedItem == "none")
            {
                sMap = "-sn";
            }
            // All
            //
            else if ((string)mainwindow.cboSubtitle.SelectedItem == "all")
            {
                
            }
            // Number
            //
            else
            {
                // Subtract 1, Map starts at 0
                int sMapNumber = Int32.Parse(mainwindow.cboSubtitle.SelectedItem.ToString()) - 1;

                sMap = "-map 0:s:" + sMapNumber + "?";

                // Image
                if ((string)mainwindow.cboFormat.SelectedItem == "jpg"
                    || (string)mainwindow.cboFormat.SelectedItem == "png")
                {
                    sMap = "-sn";
                }
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Subtitle Stream: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(mainwindow.cboSubtitle.SelectedItem.ToString()) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);


            // -------------------------
            // Format Overrides
            // -------------------------
            // Go by Format Container

            // MP3
            //
            if ((string)mainwindow.cboFormat.SelectedItem == "mp3")
            {
                mMap = "-map_metadata 0 -id3v2_version 3";
            }
            else
            {
                mMap = "-map_metadata 0";
            }


            // -------------------------
            // MediaType Overrides (Input & Output)
            // -------------------------
            // If output is Image Format, disable Video & Audio Map
            // Must be after inputVideoCodec
            if ((string)mainwindow.cboMediaType.SelectedItem == "Image" 
                || (string)mainwindow.cboMediaType.SelectedItem == "Sequence")
            {
                vMap = string.Empty;
                cMap = string.Empty;
                aMap = "-an";
                sMap = "-sn";
                mMap = string.Empty;
            }

            // If output is Audio Format, disable Video & Subtitle Map
            if ((string)mainwindow.cboMediaType.SelectedItem == "Audio")
            {
                vMap = string.Empty;
                cMap = string.Empty;
                sMap = string.Empty;
            }


            // -------------------------
            // Two-Pass Overrides
            // -------------------------
            if ((string)mainwindow.cboMediaType.SelectedItem == "Video" 
                && (string)mainwindow.cboPass.SelectedItem == "2 Pass"
                && Video.v2PassSwitch == 0)
            {
                // Remove Chapters, Subtitles, Audio, Metadata on Pass 1
                cMap = string.Empty;
                sMap = "-sn";
                aMap = "-an";
                mMap = string.Empty;

                // Turn On Two-Pass Switch
                // Pass 2 will now avoid this Override and use Audio Args
                Video.v2PassSwitch = 1;
            }


            // -------------------------
            // Combine Maps
            // -------------------------
            // Make List
            List<string> mapList = new List<string>() { vMap, cMap, sMap, aMap, mMap };
            // Join List with Spaces, Remove Empty Strings
            map = string.Join(" ", mapList.Where(s => !string.IsNullOrEmpty(s)));


            // Return Value
            return map;

        }
    }
}