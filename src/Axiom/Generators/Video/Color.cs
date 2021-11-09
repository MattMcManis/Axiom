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

 * Color Primaries
 * Color Transfer Characteristics
 * Colorspace
 * Color Range
 * Color Matrix

---------------------------------- */


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using Axiom;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Generate.Video
{
    public class Color
    {
        public static string colorPrimaries { get; set; }
        public static string colorTransferCharacteristics { get; set; }
        public static string colorSpace { get; set; }
        public static string colorRange { get; set; }
        public static string colorMatrix { get; set; }

        /// <summary>
        /// Color Primaries
        /// <summary>
        /// <remarks>
        /// https://ffmpeg.org/ffmpeg-filters.html#colorspace
        /// </remarks>
        public static String Color_Primaries(string primaries_SelectedItem)
        {
            // Auto
            if (primaries_SelectedItem == "auto")
            {
                return string.Empty;
            }

            // Options
            colorPrimaries = "-color_primaries ";

            switch (primaries_SelectedItem)
            {
                case "BT.709":
                    colorPrimaries += "bt709";
                    break;

                case "BT.470M":
                    colorPrimaries += "bt470m";
                    break;

                case "BT.470BG":
                    colorPrimaries += "bt470bg";
                    break;

                case "BT.601-6 525":
                    colorPrimaries += "smpte170m";
                    break;

                case "BT.601-6 625":
                    colorPrimaries += "bt470bg";
                    break;

                case "SMPTE-170M":
                    colorPrimaries += "smpte170m";
                    break;

                case "SMPTE-240M":
                    colorPrimaries += "smpte240m";
                    break;

                case "film":
                    colorPrimaries += "film";
                    break;

                case "SMPTE-431":
                    colorPrimaries += "smpte431";
                    break;

                case "SMPTE-432":
                    colorPrimaries += "smpte432";
                    break;

                case "BT.2020":
                    colorPrimaries += "bt2020";
                    break;

                case "JEDEC P22 phosphors":
                    colorPrimaries += "jedec-p22";
                    break;
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Color Primaries: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(colorPrimaries) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            return colorPrimaries;
        }

        /// <summary>
        /// Color Transfer Characteristics
        /// <summary>
        /// <remarks>
        /// https://ffmpeg.org/ffmpeg-filters.html#colorspace
        /// </remarks>
        public static String Color_TransferCharacteristics(string transferChar_SelectedItem)
        {
            // Auto
            if (transferChar_SelectedItem == "auto")
            {
                return string.Empty;
            }

            // Options
            colorTransferCharacteristics = "-color_trc ";

            switch (transferChar_SelectedItem)
            {
                case "BT.709":
                    colorTransferCharacteristics += "bt709";
                    break;

                case "BT.470M":
                    colorTransferCharacteristics += "bt470m";
                    break;

                case "BT.470BG":
                    colorTransferCharacteristics += "bt470bg";
                    break;

                case "Gamma 2.2":
                    colorTransferCharacteristics += "gamma22";
                    break;

                case "Gamma 2.8":
                    colorTransferCharacteristics += "gamma28";
                    break;

                case "BT.601-6 525":
                    colorTransferCharacteristics += "smpte170m";
                    break;

                case "BT.601-6 625":
                    colorTransferCharacteristics += "smpte170m";
                    break;

                case "SMPTE-170M":
                    colorTransferCharacteristics += "smpte170m";
                    break;

                case "SMPTE-240M":
                    colorTransferCharacteristics += "smpte240m";
                    break;

                case "SRGB":
                    colorTransferCharacteristics += "srgb";
                    break;

                case "iec61966-2-1":
                    colorTransferCharacteristics += "iec61966-2-1";
                    break;

                case "iec61966-2-4":
                    colorTransferCharacteristics += "iec61966-2-4";
                    break;

                case "xvycc":
                    colorTransferCharacteristics += "xvycc";
                    break;

                case "BT.2020 10-bit":
                    colorTransferCharacteristics += "bt2020-10";
                    break;

                case "BT.2020 12-bit":
                    colorTransferCharacteristics += "bt2020-12";
                    break;
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Color Transfer Characteristics: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(colorTransferCharacteristics) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            return colorTransferCharacteristics;
        }

        /// <summary>
        /// Color Space
        /// <summary>
        /// <remarks>
        /// https://ffmpeg.org/ffmpeg-filters.html#colorspace
        /// </remarks>
        public static String Color_Space(string colorspace_SelectedItem)
        {
            // Auto
            if (colorspace_SelectedItem == "auto")
            {
                return string.Empty;
            }

            // Options
            colorSpace = "-colorspace ";

            switch (colorspace_SelectedItem)
            {
                case "BT.709":
                    colorSpace += "bt709";
                    break;

                case "FCC":
                    colorSpace += "fcc";
                    break;

                case "BT.470BG":
                    colorSpace += "bt470bg";
                    break;

                case "BT.601-6 525":
                    colorSpace += "smpte170m";
                    break;

                case "BT.601-6 625":
                    colorSpace += "bt470bg";
                    break;

                case "SMPTE-170M":
                    colorSpace += "smpte170m";
                    break;

                case "SMPTE-240M":
                    colorSpace += "smpte240m";
                    break;

                case "YCgCo":
                    colorSpace += "ycgco";
                    break;

                case "BT.2020 NCL":
                    colorSpace += "bt2020ncl";
                    break;
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Colorspace: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(colorSpace) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            return colorSpace;
        }

        /// <summary>
        /// Color Range
        /// <summary>
        /// <remarks>
        /// https://ffmpeg.org/ffmpeg-filters.html#colorspace
        /// </remarks>
        public static String Color_Range(string colorRange_SelectedItem)
        {
            // Auto
            if (colorRange_SelectedItem == "auto")
            {
                return string.Empty;
            }

            // Options
            colorRange = "-color_range ";

            switch (colorRange_SelectedItem)
            {
                case "TV":
                    colorRange += "tv";
                    break;

                case "PC":
                    colorRange += "pc";
                    break;

                case "MPEG":
                    colorRange += "mpeg";
                    break;

                case "JPEG":
                    colorRange += "jpeg";
                    break;
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Color Range: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(colorRange) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            return colorRange;
        }

        /// <summary>
        /// Color Matrix
        /// <summary>
        /// <remarks>
        /// https://ffmpeg.org/ffmpeg-filters.html#colorspace
        /// </remarks>
        // Color Matrix is an x264/x265 Param
    }
}
