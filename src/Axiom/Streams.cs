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
        public static string aMap; // audio streams
        public static string cMap; // video chapters
        public static string sMap; // subtitle files
        public static string mMap; // file metadata
        public static string map; // controls all maps


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
                aMap = "-map 0:a:0?"; // only audio track 1
                sMap = "-sn"; // no subtitles for webm
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
            {
                vMap = "-map 0:v:0?"; // only video track 1
                aMap = "-map 0:a:0?"; // only audio track 1
                sMap = "-map 0:s? -c:s copy"; // all subtitles (:? at the end ignores error if subtitle is not available)
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "mkv") // if codec copy, copy all
            {
                vMap = "-map 0:v?"; // all video tracks
                aMap = "-map 0:a?"; // all audio tracks 
                sMap = "-map 0:s? -c:s copy"; // all subtitles
            }
            else if ((string)mainwindow.cboFormat.SelectedItem == "ogv") // if codec copy, copy all
            {
                vMap = "-map 0:v?"; // all video tracks
                aMap = "-map 0:a?"; // all audio tracks 
                sMap = "-map 0:s? -c:s copy"; // all subtitles, OGV has problem using Subtitles
            }


            // -------------------------
            // Video Map
            // -------------------------
            // Always all (empty)


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                //console.rtbLog.Document = new FlowDocument(paragraph);
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Video Stream: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run("all") { Foreground = Log.ConsoleDefault });
                //this.DataContext = this;
            };
            Log.LogActions.Add(Log.WriteAction);


            // -------------------------
            // Audio Map
            // -------------------------
            // Overrides
            // Offset by 1. VLC starts with #1, FFprobe starts with #a:0.
            // WARNING: IF Audio Map Enabled, Video Map must also be enabled or no video !!!!!!!!!!
            if ((string)mainwindow.cboAudioStream.SelectedItem == "none")
            {
                aMap = "-an";
            }
            else if ((string)mainwindow.cboAudioStream.SelectedItem == "all")
            {

            }
            else if ((string)mainwindow.cboAudioStream.SelectedItem == "1")
            {
                aMap = "-map 0:a:0?";
            }
            else if ((string)mainwindow.cboAudioStream.SelectedItem == "2")
            {
                aMap = "-map 0:a:1?";
            }
            else if ((string)mainwindow.cboAudioStream.SelectedItem == "3")
            {
                aMap = "-map 0:a:2?";
            }
            else if ((string)mainwindow.cboAudioStream.SelectedItem == "4")
            {
                aMap = "-map 0:a:3?";
            }
            else if ((string)mainwindow.cboAudioStream.SelectedItem == "5")
            {
                aMap = "-map 0:a:4?";
            }
            else if ((string)mainwindow.cboAudioStream.SelectedItem == "6")
            {
                aMap = "-map 0:a:5?";
            }
            else if ((string)mainwindow.cboAudioStream.SelectedItem == "7")
            {
                aMap = "-map 0:a:6?";
            }
            else if ((string)mainwindow.cboAudioStream.SelectedItem == "8")
            {
                aMap = "-map 0:a:7?";
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
            if ((string)mainwindow.cboSubtitle.SelectedItem == "all")
            {

            }
            else if ((string)mainwindow.cboSubtitle.SelectedItem == "none")
            {
                sMap = "-sn"; //might interfere with webm's -sn
            }
            else if ((string)mainwindow.cboSubtitle.SelectedItem == "1")
            {
                // mp4 uses mov_text / mkv uses ass
                if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                {
                    sMap = "-map 0:s:0? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                {
                    sMap = "-map 0:s:0? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                {
                    sMap = "-map 0:s:0? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "jpg" || (string)mainwindow.cboFormat.SelectedItem == "png")
                {
                    sMap = "-sn";
                }
            }
            else if ((string)mainwindow.cboSubtitle.SelectedItem == "2")
            {
                // mp4 uses mov_text / mkv uses ass
                if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                {
                    sMap = "-map 0:s:1? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                {
                    sMap = "-map 0:s:1? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                {
                    sMap = "-map 0:s:1? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "jpg" || (string)mainwindow.cboFormat.SelectedItem == "png")
                {
                    sMap = "-sn";
                }
            }
            else if ((string)mainwindow.cboSubtitle.SelectedItem == "3")
            {
                // mp4 uses mov_text / mkv uses ass
                if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                {
                    sMap = "-map 0:s:2? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                {
                    sMap = "-map 0:s:2? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                {
                    sMap = "-map 0:s:2? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "jpg" || (string)mainwindow.cboFormat.SelectedItem == "png")
                {
                    sMap = "-sn";
                }
            }
            else if ((string)mainwindow.cboSubtitle.SelectedItem == "4")
            {
                // mp4 uses mov_text / mkv uses ass
                if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                {
                    sMap = "-map 0:s:3? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                {
                    sMap = "-map 0:s:3? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                {
                    sMap = "-map 0:s:3? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "jpg" || (string)mainwindow.cboFormat.SelectedItem == "png")
                {
                    sMap = "-sn";
                }
            }
            else if ((string)mainwindow.cboSubtitle.SelectedItem == "5")
            {
                // mp4 uses mov_text / mkv uses ass
                if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                {
                    sMap = "-map 0:s:4? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                {
                    sMap = "-map 0:s:4? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                {
                    sMap = "-map 0:s:4? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "jpg" || (string)mainwindow.cboFormat.SelectedItem == "png")
                {
                    sMap = "-sn";
                }
            }
            else if ((string)mainwindow.cboSubtitle.SelectedItem == "6")
            {
                // mp4 uses mov_text / mkv uses ass
                if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                {
                    sMap = "-map 0:s:5? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                {
                    sMap = "-map 0:s:5? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                {
                    sMap = "-map 0:s:5? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "jpg" || (string)mainwindow.cboFormat.SelectedItem == "png")
                {
                    sMap = "-sn";
                }
            }
            else if ((string)mainwindow.cboSubtitle.SelectedItem == "7")
            {
                // mp4 uses mov_text / mkv uses ass
                if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                {
                    sMap = "-map 0:s:6? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                {
                    sMap = "-map 0:s:6? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                {
                    sMap = "-map 0:s:6? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "jpg" || (string)mainwindow.cboFormat.SelectedItem == "png")
                {
                    sMap = "-sn";
                }
            }
            else if ((string)mainwindow.cboSubtitle.SelectedItem == "8")
            {
                // mp4 uses mov_text / mkv uses ass
                if ((string)mainwindow.cboFormat.SelectedItem == "mp4")
                {
                    sMap = "-map 0:s:7? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "mkv")
                {
                    sMap = "-map 0:s:7? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "ogv")
                {
                    sMap = "-map 0:s:7? -c:s copy";
                }
                else if ((string)mainwindow.cboFormat.SelectedItem == "jpg" || (string)mainwindow.cboFormat.SelectedItem == "png")
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
            // Chapters Map
            // -------------------------
            // Overrides
            // Go by Container instead of Codec
            //if ((string)mainwindow.cboFormat.SelectedItem == "mp4" || (string)mainwindow.cboFormat.SelectedItem == "mkv" || (string)mainwindow.cboFormat.SelectedItem == "ogv")
            //{
            //    //cMap = "-map_chapters outfile:infile";
            //}
            //else
            //{
            //    //cMap = "-map_chapters -1";
            //}


            // -------------------------
            // Metadata Map
            // -------------------------
            // Go by Format Container
            if ((string)mainwindow.cboFormat.SelectedItem == "mp3")
            {
                mMap = "-map_metadata 0 -id3v2_version 3";
            }
            else
            {
                mMap = "-map_metadata 0";
            }

            // MediaType
            if ((string)mainwindow.cboMediaType.SelectedItem == "Image" || (string)mainwindow.cboMediaType.SelectedItem == "Sequence")
            {
                mMap = string.Empty;
            }


            // -------------------------
            // MediaType Overrides (Input & Output)
            // -------------------------
            // If output is Image Format, disable Video & Audio Map
            // Must be after inputVideoCodec
            if ((string)mainwindow.cboMediaType.SelectedItem == "Image" || (string)mainwindow.cboMediaType.SelectedItem == "Sequence")
            {
                vMap = string.Empty;
                aMap = "-an";
                sMap = "-sn";
            }

            // If input is Mute, disable the Audio Map (This can only happen in Auto Mode, as it enables FFmpeg detection)
            // This caused problems when generating Script. When Output TextBox was blank aMap was empty.
            //if (string.IsNullOrEmpty(FFprobe.inputAudioCodec))
            //{
            //    aMap = string.Empty;
            //}

            // If output is Audio Format, disable Video & Subtitle Map
            if ((string)mainwindow.cboMediaType.SelectedItem == "Audio")
            {
                vMap = string.Empty;
                sMap = string.Empty;
            }

            // -------------------------
            // Mute Override
            // -------------------------
            if ((string)mainwindow.cboAudio.SelectedItem == "Mute")
            {
                aMap = "-an";
            }


            // -------------------------
            // Combine Maps
            // -------------------------
            // Make List
            List<string> mapList = new List<string>() { vMap, cMap, aMap, sMap, mMap };
            // Join List with Spaces, Remove Empty Strings
            map = string.Join(" ", mapList.Where(s => !string.IsNullOrEmpty(s)));


            // Return Value
            return map;

        }
    }
}