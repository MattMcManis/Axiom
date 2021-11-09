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

  * Channel
---------------------------------- */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using ViewModel;
using Axiom;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Generate
{
    namespace Audio
    {
        public class Channels
        {
            public static string aChannel { get; set; }

            /// <summary>
            /// Channel
            /// <summary>
            public static String Channel(string codec_SelectedItem,
                                         string channel_SelectedItem
                                        )
            {
                // Check:
                // Audio Codec Not Copy
                if (codec_SelectedItem != "Copy")
                {
                    switch (channel_SelectedItem)
                    {
                        // -------------------------
                        // Empty
                        // -------------------------
                        case null:
                            aChannel = string.Empty;
                            break;

                        case "":
                            aChannel = string.Empty;
                            break;

                        // -------------------------
                        // Auto
                        // -------------------------
                        case "Source":
                            aChannel = string.Empty;
                            break;

                        // -------------------------
                        // Mono
                        // -------------------------
                        case "Mono":
                            aChannel = "-ac 1";
                            break;

                        // -------------------------
                        // Stereo
                        // -------------------------
                        case "Stereo":
                            aChannel = "-ac 2";
                            break;

                        // -------------------------
                        // Joint Stereo
                        // -------------------------
                        case "Joint Stereo":
                            aChannel = "-ac 2 -joint_stereo 1";
                            break;

                        // -------------------------
                        // 5.1
                        // -------------------------
                        case "5.1":
                            aChannel = "-ac 6";
                            break;

                        // -------------------------
                        // Default
                        // -------------------------
                        default:
                            aChannel = string.Empty;
                            break;
                    }

                    // -------------------------
                    // Prevent Downmix Clipping
                    // -------------------------
                    if (channel_SelectedItem != "Source")
                    {
                        aChannel = "-rematrix_maxval 1.0 " + aChannel;
                    }


                    // Log Console Message /////////
                    Log.WriteAction = () =>
                    {
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Channel: ")) { Foreground = Log.ConsoleDefault });
                        Log.logParagraph.Inlines.Add(new Run(channel_SelectedItem) { Foreground = Log.ConsoleDefault });
                    };
                    Log.LogActions.Add(Log.WriteAction);
                }

                // Return Value
                return aChannel;
            }
        }
    }
}
