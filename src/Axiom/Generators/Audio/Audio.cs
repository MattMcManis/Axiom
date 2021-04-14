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

 * 
---------------------------------- */

using Axiom;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Generate.Audio
{
    public class Audio
    {
        public static string audioMux { get; set; }
        //public static List<string> audioMuxList;

        public static string audioDir { get; set; } // Subtitles Directory
        public static List<string> audioFilePathsList = new List<string>(); // Files Added   
        public static List<string> audioFileNamesList = new List<string>(); // File Names without Path

        /// <summary>
        /// Audio External
        /// <summary>
        public static/* List<string> */String AudioMux(string codec_SelectedItem,
                                            string stream_SelectedItem
                                           )
        {
            // -------------------------
            // External
            // -------------------------
            if (codec_SelectedItem != "None" &&
                stream_SelectedItem == "mux" &&
                audioFilePathsList != null &&
                audioFilePathsList.Count > 0)
            {
                audioMux = "-i " + string.Join(" \r\n\r\n-i ", audioFilePathsList
                                         .Where(s => !string.IsNullOrWhiteSpace(s))
                                        );

                //for (var i = 0; i < audioFilePathsList.Count; i++)
                //{
                //    audioMuxList.Add("-i " + MainWindow.WrapWithQuotes(audioFilePathsList[i]));
                //}
            }

            return audioMux;
            //return audioMuxList;
        }
    }
}
