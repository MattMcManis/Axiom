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
        /// Variables
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        public static string sCodec; // Subtitle Codec
        public static string subtitles;

        public static string subsDir; // Subtitles Directory
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
        public static String SubtitleCodec(string codec)
        {
            //string sCodec = string.Empty;

            // Passed Command
            sCodec = codec;

            return sCodec;
        }

        /// <summary>
        /// Subtitles External
        /// <summary>
        public static String SubtitlesExternal(ViewModel vm)
        {
            //string subtitles = string.Empty;

            // -------------------------
            // External
            // -------------------------
            if (vm.SubtitleStream_SelectedItem == "external"
                && vm.SubtitleCodec_SelectedItem != "Burn" // Ignore if Burn
                && subtitleFilePathsList != null
                && subtitleFilePathsList.Count > 0)
            {
                subtitles = "-i " + string.Join(" \r\n\r\n-i ", subtitleFilePathsList
                                          .Where(s => !string.IsNullOrEmpty(s))
                                          );
            }

            return subtitles;
        }



    }
}
