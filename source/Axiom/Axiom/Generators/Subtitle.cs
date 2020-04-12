/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2020 Matt McManis
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
using System.Linq;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class Subtitle
    {
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Global Variables
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------

        public static string sCodec { get; set; } // Subtitle Codec
        public static string subtitles { get; set; }

        public static string subsDir { get; set; } // Subtitles Directory
        public static List<string> subtitleFilePathsList = new List<string>(); // Files Added   
        public static List<string> subtitleFileNamesList = new List<string>(); // File Names without Path



        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Methods
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Subtitle Codec
        /// <summary>
        public static String SubtitleCodec(string codec_SelectedItem,
                                           string codec_Command
                                           )
        {
            // Passed Command
            if (codec_SelectedItem != "None")
            {
                // e.g. -c:s srt
                sCodec = codec_Command;
            }

            return sCodec;
        }


        /// <summary>
        /// Subtitles External
        /// <summary>
        public static String SubtitlesExternal(string codec_SelectedItem,
                                               string stream_SelectedItem
                                               )
        {
            // -------------------------
            // External
            // -------------------------
            if (codec_SelectedItem != "Burn" &&  // Ignore if Burn
                stream_SelectedItem == "external" &&
                subtitleFilePathsList != null &&
                subtitleFilePathsList.Count > 0)
            {
                subtitles = "-i " + string.Join(" \r\n\r\n-i ", subtitleFilePathsList
                                          .Where(s => !string.IsNullOrEmpty(s))
                                          );
            }

            return subtitles;
        }


    }
}
