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
using System.Data;
using System.Linq;
using System.Windows.Documents;
using ViewModel;
using Axiom;
using System.Windows;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Generate
{
    public class Streams
    {
        public static string vMap { get; set; } // video streams
        public static string sMap { get; set; } // video subtitles
        public static string aMap { get; set; } // audio streams
        public static string cMap { get; set; } // video chapters
        public static string mMap { get; set; } // file metadata
        public static string mvMap { get; set; } // video metadata
        public static string maMap { get; set; } // audio metadata
        public static string msMap { get; set; } // subtitle metadata
        //public static string mcMap { get; set; } // chapter metadata // uses ChaptersMaps() instead

        /// <summary>
        /// Format Maps
        /// </summary>
        public static String FormatMaps()
        {
            // --------------------------------------------------------------------
            // Metadata Map
            // --------------------------------------------------------------------
            // Go by Format Container
            switch (VM.FormatView.Format_Container_SelectedItem)
            {
                case "webm":
                    mMap = string.Empty; // do not copy metadata
                    mvMap = string.Empty;
                    maMap = string.Empty;
                    msMap = string.Empty;
                    //mcMap = string.Empty;
                    break;

                case "m2v":
                    mMap = string.Empty; // do not copy metadata
                    mvMap = string.Empty;
                    maMap = string.Empty;
                    msMap = string.Empty;
                    //mcMap = string.Empty;
                    break;

                case "mp3":
                    mMap = "-map_metadata 0 -id3v2_version 3";
                    mvMap = string.Empty; // do not copy metadata
                    maMap = string.Empty;
                    msMap = string.Empty;
                    //mcMap = string.Empty;
                    break;

                case "m4a":
                    mMap = "-map_metadata 0";
                    mvMap = string.Empty; // do not copy metadata
                    maMap = string.Empty;
                    msMap = string.Empty;
                    //mcMap = string.Empty;
                    break;

                case "ogg":
                    mMap = "-map_metadata 0";
                    mvMap = string.Empty; // do not copy metadata
                    maMap = string.Empty;
                    msMap = string.Empty;
                    //mcMap = string.Empty;
                    break;

                case "flac":
                    mMap = "-map_metadata 0";
                    mvMap = string.Empty; // do not copy metadata
                    maMap = string.Empty;
                    msMap = string.Empty;
                    //mcMap = string.Empty;
                    break;

                case "wav":
                    mMap = "-map_metadata 0";
                    mvMap = string.Empty; // do not copy metadata
                    maMap = string.Empty;
                    msMap = string.Empty;
                    //mcMap = string.Empty;
                    break;

                case "jpg":
                    mMap = string.Empty; // do not copy metadata
                    mvMap = string.Empty;
                    maMap = string.Empty;
                    msMap = string.Empty;
                    //mcMap = string.Empty;
                    break;

                case "png":
                    mMap = string.Empty; // do not copy metadata
                    mvMap = string.Empty;
                    maMap = string.Empty;
                    msMap = string.Empty;
                    //mcMap = string.Empty;
                    break;

                case "webp":
                    mMap = string.Empty; // do not copy metadata
                    mvMap = string.Empty;
                    maMap = string.Empty;
                    msMap = string.Empty;
                    //mcMap = string.Empty;
                    break;

                // All Other Formats
                // 
                default:
                    // Copy All Metadata
                    if (VM.AudioView.Audio_Stream_SelectedItem != "mux" &&
                        VM.SubtitleView.Subtitle_Stream_SelectedItem != "mux" &&
                        VM.SubtitleView.Subtitle_Stream_SelectedItem != "external"
                        )
                    {
                        mMap = "-map_metadata 0";
                        mvMap = string.Empty;
                        maMap = string.Empty;
                        msMap = string.Empty;
                        //mcMap = string.Empty;

                        break;
                    }

                    // Audio Mux
                    // Copy Video, Subtitle, Chapter Metadata Only
                    if (VM.AudioView.Audio_Stream_SelectedItem == "mux" &&
                        VM.SubtitleView.Subtitle_Stream_SelectedItem != "mux")
                    {
                        mMap = string.Empty;
                        mvMap = "-map_metadata:s:v 0";
                        maMap = string.Empty;
                        msMap = "-map_metadata:s:s 0";
                        //mcMap = "-map_metadata:c 0";
                    }

                    // Subtitle Mux
                    // Copy Video, Audio, Chapter Metadata Only
                    else if (VM.SubtitleView.Subtitle_Stream_SelectedItem == "mux" &&
                             VM.AudioView.Audio_Stream_SelectedItem != "mux")
                    {
                        mMap = string.Empty;
                        mvMap = "-map_metadata:s:v 0";
                        maMap = "-map_metadata:s:a 0";
                        msMap = string.Empty;
                        //mcMap = "-map_metadata:c 0";
                    }

                    // Audio & Subtitle Mux
                    // Copy Video, Chapter Metadata Only
                    else if (VM.SubtitleView.Subtitle_Stream_SelectedItem == "mux" &&
                             VM.AudioView.Audio_Stream_SelectedItem == "mux")
                    {
                        mMap = string.Empty;
                        mvMap = "-map_metadata:s:v 0";
                        maMap = string.Empty;
                        msMap = string.Empty;
                        //mcMap = "-map_metadata:c 0";
                    }

                    break;
            }

            // --------------------------------------------------------------------
            // Combine Maps
            // --------------------------------------------------------------------
            // Make List
            List<string> mapList = new List<string>()
            {
                mMap,
                mvMap,
                maMap,
                msMap,
                //mcMap
            };

            // Join List with Spaces, Remove Empty Strings
            string map = string.Join("\r\n", mapList.Where(s => !string.IsNullOrWhiteSpace(s)));

            return map;
        }

        /// <summary>
        /// Video Stream Maps
        /// </summary>
        public static String VideoStreamMaps()
        {
            // WARNING: If a map is enabled, all other map types must be specified or they will be removed !!!
            // Question Mark ? = ignore warnings

            // --------------------------------------------------------------------
            // Video Map
            // --------------------------------------------------------------------
            switch (VM.FormatView.Format_Container_SelectedItem)
            {
                // -------------------------
                // Video Formats
                // -------------------------
                case "webm":
                    vMap = "-map 0:v:0?"; // only video track 1
                    break;

                case "mp4":
                    vMap = "-map 0:v:0?"; // only video track 1
                    break;

                case "mkv":
                    vMap = "-map 0:v?"; // all video tracks
                    break;

                case "m2v":
                    vMap = "-map 0:v?"; // all video tracks
                    break;

                case "mpg":
                    vMap = "-map 0:v?"; // all video tracks
                    break;

                case "avi":
                    vMap = "-map 0:v:0?"; // only video track 1
                    break;

                case "ogv":
                    vMap = "-map 0:v?"; // all video tracks
                    break;

                // Non-Video Formats
                //
                default:
                    vMap = string.Empty; // do not copy video map
                    break;
            }

            // -------------------------
            // Video Codecs
            // -------------------------
            if (VM.VideoView.Video_Codec_SelectedItem == "None")
            {
                vMap = "-vn"; // only video track 1
            }

            // -------------------------
            // Remove Video Map if Input File is Audio Format
            // -------------------------
            if (Format.AudioFormats.Any(s => s.Equals(MainWindow.inputExt, StringComparison.OrdinalIgnoreCase))
                //|| Format.AudioFormats.Any(s => s.Equals(MainWindow.batchExt, StringComparison.OrdinalIgnoreCase))
                )
            {
                vMap = string.Empty;
            }


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Stream: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run("all") { Foreground = Log.ConsoleDefault }); // always all
            };
            Log.LogActions.Add(Log.WriteAction);

            // --------------------------------------------------------------------
            // Combine Maps
            // --------------------------------------------------------------------
            // Make List
            List<string> mapList = new List<string>() { vMap/*, cMap*/ };
            // Join List with Spaces, Remove Empty Strings
            string map = string.Join(" ", mapList.Where(s => !string.IsNullOrWhiteSpace(s)));


            // Return Value
            return map;
        }


        /// <summary>
        /// Audio Stream Maps
        /// </summary>
        public static String AudioStreamMaps()
        {
            // --------------------------------------------------------------------
            // Audio Map
            // --------------------------------------------------------------------
            // Offset by 1. VLC starts with #1, FFprobe starts with #a:0.
            // WARNING: IF Audio Map Enabled, Video Map must also be enabled or no video !!!!!!!!!!

            switch (VM.AudioView.Audio_Stream_SelectedItem)
            {
                // -------------------------
                // None
                // -------------------------
                case "none":
                    aMap = "-an";
                    break;

                // -------------------------
                // Mux
                // -------------------------
                case "mux":
                    // -------------------------
                    // Map
                    // -------------------------
                    List<string> audioMapsList = new List<string>();

                    if (Audio.Audio.audioFilePathsList != null &&
                        Audio.Audio.audioFilePathsList.Count > 0)
                    {
                        // Give each Audio File its own map
                        for (var i = 0; i < Audio.Audio.audioFilePathsList.Count; i++)
                        {
                            audioMapsList.Add("-map " + (i + 1).ToString() + ":a?");
                        }
                    }

                    // Join multiple maps: 
                    // -map 1:a? 
                    // -map 2:a? 
                    // -map 3:a?
                    aMap = string.Join("\r\n", audioMapsList.Where(s => !string.IsNullOrWhiteSpace(s)));


                    // -------------------------
                    // Default Audio
                    // -------------------------
                    string checkedItem = string.Empty;
                    for (var i = 0; i < VM.AudioView.Audio_ListView_Items.Count; i++)
                    {
                        // If list contains a checked item
                        if (VM.AudioView.Audio_ListView_SelectedItems.Contains(VM.AudioView.Audio_ListView_Items[i]))
                        {
                            // Get Index Position
                            checkedItem = i.ToString();
                        }
                    }

                    // Create Default Audio
                    string disposition = string.Empty;
                    if (!string.IsNullOrWhiteSpace(checkedItem))
                    {
                        disposition = " \r\n-disposition:a:" + checkedItem + " default";
                    }

                    // Combine Map + Default
                    aMap = aMap + disposition;
                    break;

                // -------------------------
                // All
                // -------------------------
                case "all":
                    switch (VM.FormatView.Format_Container_SelectedItem)
                    {
                        // Video/Image Format
                        //
                        case "webm":
                            aMap = "-map 0:a:0?"; // only audio track 1
                            break;

                        case "mp4":
                            aMap = "-map 0:a?"; // all audio tracks 
                            break;

                        case "mkv":
                            aMap = "-map 0:a?"; // all audio tracks 
                            break;

                        case "m2v":
                            aMap = "-an"; // disable audio
                            break;

                        case "mpg":
                            aMap = "-map 0:a?"; // all audio tracks 
                            break;

                        case "avi":
                            aMap = "-map 0:a?"; // all audio tracks 
                            break;

                        case "ogv":
                            aMap = "-map 0:a?"; // all audio tracks 
                            break;

                        case "jpg":
                            aMap = "-an"; // disable audio
                            break;

                        case "png":
                            aMap = "-an"; // disable audio
                            break;

                        case "webp":
                            aMap = "-an"; // disable audio
                            break;

                        default:
                            aMap = string.Empty; // do not copy audio
                            break;
                    }

                    // Audio Media Type
                    //
                    if (VM.FormatView.Format_MediaType_SelectedItem == "Audio")
                    {
                        aMap = "-map 0:a:0?"; // only audio track 1
                    }
                    break;

                // -------------------------
                // Number
                // -------------------------
                default:
                    // Subtract 1, Map starts at 0
                    //int aMapNumber = Int32.Parse(VM.AudioView.Audio_Stream_SelectedItem) - 1;
                    int aMapNumberInt = 1; // Fallback
                    int.TryParse(VM.AudioView.Audio_Stream_SelectedItem, out aMapNumberInt);

                    int aMapNumber = aMapNumberInt - 1;

                    aMap = "-map 0:a:" + aMapNumber + "?";
                    break;
            }


            // -------------------------
            // Remove Audio Map if Input File is Audio Format
            // -------------------------
            if (Format.AudioFormats.Any(s => s.Equals(MainWindow.inputExt, StringComparison.OrdinalIgnoreCase))
                //|| Format.AudioFormats.Any(s => s.Equals(MainWindow.batchExt, StringComparison.OrdinalIgnoreCase))
                )
            {
                aMap = string.Empty;
            }

            // -------------------------
            // Mute
            // -------------------------
            if (VM.AudioView.Audio_Quality_SelectedItem == "Mute")
            {
                aMap = "-an";
            }


            // --------------------------------------------------------------------
            // Combine Maps
            // --------------------------------------------------------------------
            // Make List
            List<string> mapList = new List<string>() { aMap };
            // Join List with Spaces, Remove Empty Strings
            string map = string.Join(" ", mapList.Where(s => !string.IsNullOrWhiteSpace(s)));


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Stream: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(VM.AudioView.Audio_Stream_SelectedItem) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);


            // Return Value
            return map;
        }


        /// <summary>
        /// Subtitle Maps
        /// </summary>
        public static String SubtitleMaps()
        {
            // --------------------------------------------------------------------
            // Subtitle Map
            // --------------------------------------------------------------------

            if (VM.SubtitleView.Subtitle_Codec_SelectedItem != "Burn") // Ignore if Burn
            {
                switch (VM.SubtitleView.Subtitle_Stream_SelectedItem)
                {
                    // -------------------------
                    // None
                    // -------------------------
                    case "none":
                        sMap = "-sn";
                        break;

                    // -------------------------
                    // Mux
                    // -------------------------
                    case "mux":
                        // -------------------------
                        // Map
                        // -------------------------
                        List<string> subtitleMapsList = new List<string>();

                        if (Subtitle.Subtitle.subtitleFilePathsList != null &&
                            Subtitle.Subtitle.subtitleFilePathsList.Count > 0)
                        {
                            // If Audio has also been muxed, start the index after the Audio Count
                            // -map 1:a? 
                            // -map 2:a? 
                            // -map 3:s? <-- start
                            int mapStart = 0;
                            if (VM.AudioView.Audio_Stream_SelectedItem == "mux" &&
                                Audio.Audio.audioFilePathsList != null &&
                                Audio.Audio.audioFilePathsList.Count > 0)
                            {
                                mapStart = Audio.Audio.audioFilePathsList.Count;
                            }

                            //MessageBox.Show(Audio.Audio.audioFilePathsList.Count.ToString()); //debug

                            // Give each Subtitle File its own map
                            for (var i = 0; i < Subtitle.Subtitle.subtitleFilePathsList.Count; i++)
                            {
                                subtitleMapsList.Add("-map " + (i + 1 + mapStart).ToString() + ":s?");
                            }
                        }

                        // Join multiple maps: 
                        // -map 1:s? 
                        // -map 2:s? 
                        // -map 3:s?
                        sMap = string.Join("\r\n", subtitleMapsList.Where(s => !string.IsNullOrWhiteSpace(s)));


                        // -------------------------
                        // Default Subtitle
                        // -------------------------
                        string checkedItem = string.Empty;
                        for (var i = 0; i < VM.SubtitleView.Subtitle_ListView_Items.Count; i++)
                        {
                            // If list contains a checked item
                            if (VM.SubtitleView.Subtitle_ListView_SelectedItems.Contains(VM.SubtitleView.Subtitle_ListView_Items[i]))
                            {
                                // Get Index Position
                                checkedItem = i.ToString();
                            }
                        }

                        // Create Default Subtitle
                        string disposition = string.Empty;
                        if (!string.IsNullOrWhiteSpace(checkedItem))
                        {
                            disposition = " \r\n-disposition:s:" + checkedItem + " default";
                        }

                        // Combine Map + Default
                        sMap = sMap + disposition;
                        break;

                    // -------------------------
                    // All
                    // -------------------------
                    case "all":
                        // -------------------------
                        // Formats
                        // -------------------------
                        switch (VM.FormatView.Format_Container_SelectedItem)
                        {
                            case "webm":
                                sMap = "-sn"; // no subtitles
                                break;

                            case "mp4":
                                sMap = "-map 0:s?"; // all subtitles (:? at the end ignores error if subtitle is not available:
                                break;

                            case "mkv":
                                sMap = "-map 0:s?"; // all subtitles
                                break;

                            case "m2v":
                                sMap = "-sn"; //  no subtitles
                                break;

                            case "mpg":
                                sMap = "-map 0:s?"; // all subtitles
                                break;

                            case "avi":
                                sMap = "-map 0:s?"; // all subtitles
                                break;

                            case "ogv":
                                sMap = "-map 0:s?"; // all subtitles, OGV has problem using Subtitles
                                break;

                            case "jpg":
                                sMap = "-sn"; // disable subtitles
                                break;

                            case "png":
                                sMap = "-sn"; // disable subtitles
                                break;

                            case "webp":
                                sMap = "-sn"; // disable subtitles
                                break;

                            // Non-Video/Image Formats
                            //
                            default:
                                sMap = "-sn"; // disable subtitles
                                break;
                        }
                        break;

                    // -------------------------
                    // Number
                    // -------------------------
                    default:
                        // Subtract 1, Map starts at 0
                        //int sMapNumber = Int32.Parse(VM.SubtitleView.Subtitle_Stream_SelectedItem) - 1;
                        int sMapNumberInt = 1; // Fallback
                        int.TryParse(VM.SubtitleView.Subtitle_Stream_SelectedItem, out sMapNumberInt);

                        int sMapNumber = sMapNumberInt - 1;

                        sMap = "-map 1:s:" + sMapNumber + "?";

                        // Image
                        if (VM.FormatView.Format_Container_SelectedItem == "jpg" ||
                            VM.FormatView.Format_Container_SelectedItem == "png" ||
                            VM.FormatView.Format_Container_SelectedItem == "webp")
                        {
                            sMap = "-sn";
                        }
                        break;
                }

                // -------------------------
                // Remove Subtitle Map if Input File is Audio Format
                // -------------------------
                if (Format.AudioFormats.Any(s => s.Equals(MainWindow.inputExt, StringComparison.OrdinalIgnoreCase))
                    //|| Format.AudioFormats.Any(s => s.Equals(MainWindow.batchExt, StringComparison.OrdinalIgnoreCase))
                    )
                {
                    sMap = string.Empty;
                }
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Subtitle Stream: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(VM.SubtitleView.Subtitle_Stream_SelectedItem) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            // --------------------------------------------------------------------
            // Combine Maps
            // --------------------------------------------------------------------
            // Make List
            List<string> mapList = new List<string>() { sMap };
            // Join List with Spaces, Remove Empty Strings
            string map = string.Join(" ", mapList.Where(s => !string.IsNullOrWhiteSpace(s)));

            // Return Value
            return map;
        }


        /// <summary>
        /// Chapters Maps
        /// </summary>
        public static String ChaptersMaps()
        {
            // --------------------------------------------------------------------
            // Chapters Map
            // --------------------------------------------------------------------
            // -------------------------
            // Formats
            // -------------------------
            switch (VM.FormatView.Format_Container_SelectedItem)
            {
                // Video
                case "webm":
                    cMap = "-map_chapters -1"; // remove chapters
                    break;

                case "mp4":
                    cMap = "-map_chapters 0"; // all chapters
                    break;

                case "mkv":
                    cMap = "-map_chapters 0"; // all chapters
                    break;

                case "m2v":
                    cMap = "-map_chapters -1"; // remove chapters
                    break;

                case "mpg":
                    cMap = "-map_chapters 0"; // all chapters
                    break;

                case "avi":
                    cMap = "-map_chapters 0"; // all chapters
                    break;

                case "ogv":
                    cMap = "-map_chapters 0"; // all chapters
                    break;

                // Audio
                case "mp3":
                    cMap = "-map_chapters 0"; // all chapters
                    break;

                case "m4a":
                    cMap = "-map_chapters 0"; // all chapters
                    break;

                case "ogg":
                    cMap = "-map_chapters 0"; // all chapters
                    break;

                case "flac":
                    cMap = "-map_chapters 0"; // all chapters
                    break;

                case "wav":
                    cMap = "-map_chapters 0"; // all chapters
                    break;

                // All Other Formats
                //
                default:
                    cMap = string.Empty; // do not copy chapters
                    break;
            }

            // -------------------------
            // Remove Chapters Map if Input File is Audio Format
            // -------------------------
            if (Format.AudioFormats.Any(s => s.Equals(MainWindow.inputExt, StringComparison.OrdinalIgnoreCase))
                //|| Format.AudioFormats.Any(s => s.Equals(MainWindow.batchExt, StringComparison.OrdinalIgnoreCase))
                )
            {
                cMap = string.Empty;
            }


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Chapters: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(cMap) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            // --------------------------------------------------------------------
            // Combine Maps
            // --------------------------------------------------------------------
            // Make List
            List<string> mapList = new List<string>() { cMap };
            // Join List with Spaces, Remove Empty Strings
            string map = string.Join(" ", mapList.Where(s => !string.IsNullOrWhiteSpace(s)));

            // Return Value
            return map;
        }

    }
}
