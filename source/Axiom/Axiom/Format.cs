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

using System;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class Format
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


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Process Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
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