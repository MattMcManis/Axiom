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

/* ----------------------------------
 METHODS

 * Color Primaries
 * Color Transfer Characteristics
 * Color Space
 * Color Range
 * Color Matrix
 * Video Color
 * VideoParams

---------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using ViewModel;
using Axiom;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Generate
{
    namespace Video
    {
        public class Params
        {
            public static List<string> vParamsList = new List<string>(); // multiple parameters
            public static string vParams { get; set; } // combined inline list

            /// <summary>
            /// Color Primaries
            /// <summary>
            /// <remarks>
            /// https://ffmpeg.org/ffmpeg-filters.html#colorspace
            /// </remarks>
            //public static String Color_Primaries_Filter()
            //{
            //    string colorPrimaries = string.Empty;

            //    if (VM.VideoView.Video_Color_Primaries_SelectedItem != "auto")
            //    {
            //        switch (VM.VideoView.Video_Color_Primaries_SelectedItem)
            //        {
            //            case "BT.709":
            //                colorPrimaries = "bt709";
            //                break;

            //            case "BT.470M":
            //                colorPrimaries = "bt470m";
            //                break;

            //            case "BT.470BG":
            //                colorPrimaries = "bt470bg";
            //                break;

            //            case "BT.601-6 525":
            //                colorPrimaries = "smpte170m";
            //                break;

            //            case "BT.601-6 625":
            //                colorPrimaries = "bt470bg";
            //                break;

            //            case "SMPTE-170M":
            //                colorPrimaries = "smpte170m";
            //                break;

            //            case "SMPTE-240M":
            //                colorPrimaries = "smpte240m";
            //                break;

            //            case "film":
            //                colorPrimaries = "film";
            //                break;

            //            case "SMPTE-431":
            //                colorPrimaries = "smpte431";
            //                break;

            //            case "SMPTE-432":
            //                colorPrimaries = "smpte432";
            //                break;

            //            case "BT.2020":
            //                colorPrimaries = "bt2020";
            //                break;

            //            case "JEDEC P22 phosphors":
            //                colorPrimaries = "jedec-p22";
            //                break;
            //        }

            //        return "colorprim=" + colorPrimaries;
            //    }

            //    // Auto
            //    else
            //    {
            //        return string.Empty;
            //    }
            //}

            /// <summary>
            /// Color Transfer Characteristics
            /// <summary>
            /// <remarks>
            /// https://ffmpeg.org/ffmpeg-filters.html#colorspace
            /// </remarks>
            //public static String Color_TransferCharacteristics_Filter()
            //{
            //    string colorTransferCharacteristics = string.Empty;

            //    if (VM.VideoView.Video_Color_TransferCharacteristics_SelectedItem != "auto")
            //    {
            //        switch (VM.VideoView.Video_Color_TransferCharacteristics_SelectedItem)
            //        {
            //            case "BT.709":
            //                colorTransferCharacteristics = "bt709";
            //                break;

            //            case "BT.470M":
            //                colorTransferCharacteristics = "bt470m";
            //                break;

            //            case "BT.470BG":
            //                colorTransferCharacteristics = "bt470bg";
            //                break;

            //            case "Gamma 2.2":
            //                colorTransferCharacteristics = "gamma22";
            //                break;

            //            case "Gamma 2.8":
            //                colorTransferCharacteristics = "gamma28";
            //                break;

            //            case "BT.601-6 525":
            //                colorTransferCharacteristics = "smpte170m";
            //                break;

            //            case "BT.601-6 625":
            //                colorTransferCharacteristics = "smpte170m";
            //                break;

            //            case "SMPTE-170M":
            //                colorTransferCharacteristics = "smpte170m";
            //                break;

            //            case "SMPTE-240M":
            //                colorTransferCharacteristics = "smpte240m";
            //                break;

            //            case "SRGB":
            //                colorTransferCharacteristics = "srgb";
            //                break;

            //            case "iec61966-2-1":
            //                colorTransferCharacteristics = "iec61966-2-1";
            //                break;

            //            case "iec61966-2-4":
            //                colorTransferCharacteristics = "iec61966-2-4";
            //                break;

            //            case "xvycc":
            //                colorTransferCharacteristics = "xvycc";
            //                break;

            //            case "BT.2020 10-bit":
            //                colorTransferCharacteristics = "bt2020-10";
            //                break;

            //            case "BT.2020 12-bit":
            //                colorTransferCharacteristics = "bt2020-12";
            //                break;
            //        }

            //        return "transfer=" + colorTransferCharacteristics;
            //    }

            //    // Auto
            //    else
            //    {
            //        return string.Empty;
            //    }
            //}

            ///// <summary>
            ///// Color Space
            ///// <summary>
            ///// <remarks>
            ///// https://ffmpeg.org/ffmpeg-filters.html#colorspace
            ///// </remarks>
            //public static String Color_Space_Filter()
            //{
            //    string colorSpace = string.Empty;

            //    if (VM.VideoView.Video_Color_Space_SelectedItem != "auto")
            //    {
            //        switch (VM.VideoView.Video_Color_Space_SelectedItem)
            //        {
            //            case "BT.709":
            //                colorSpace = "bt709";
            //                break;

            //            case "FCC":
            //                colorSpace = "fcc";
            //                break;

            //            case "BT.470BG":
            //                colorSpace = "bt470bg";
            //                break;

            //            case "BT.601-6 525":
            //                colorSpace = "smpte170m";
            //                break;

            //            case "BT.601-6 625":
            //                colorSpace = "bt470bg";
            //                break;

            //            case "SMPTE-170M":
            //                colorSpace = "smpte170m";
            //                break;

            //            case "SMPTE-240M":
            //                colorSpace = "smpte240m";
            //                break;

            //            case "YCgCo":
            //                colorSpace = "ycgco";
            //                break;

            //            case "BT.2020 NCL":
            //                colorSpace = "bt2020ncl";
            //                break;
            //        }

            //        return "colorspace=" + colorSpace;
            //    }

            //    // Auto
            //    else
            //    {
            //        return string.Empty;
            //    }
            //}

            /// <summary>
            /// Color Range
            /// <summary>
            /// <remarks>
            /// https://ffmpeg.org/ffmpeg-filters.html#colorspace
            /// </remarks>
            //public static String Color_Range_Filter()
            //{
            //    string colorRange = string.Empty;

            //    if (VM.VideoView.Video_Color_Range_SelectedItem != "auto")
            //    {
            //        switch (VM.VideoView.Video_Color_Range_SelectedItem)
            //        {
            //            case "TV":
            //                colorRange = "tv";
            //                break;

            //            case "PC":
            //                colorRange = "pc";
            //                break;

            //            case "MPEG":
            //                colorRange = "mpeg";
            //                break;

            //            case "JPEG":
            //                colorRange = "jpeg";
            //                break;
            //        }

            //        return "colorrange=" + colorRange;
            //    }

            //    // Auto
            //    else
            //    {
            //        return string.Empty;
            //    }
            //}

            /// <summary>
            /// Color Matrix
            /// <summary>
            /// <remarks>
            /// https://ffmpeg.org/ffmpeg-filters.html#colorspace
            /// </remarks>
            public static String Color_Matrix_Filter(string colorMatrix_SelectedItem)
            {
                // Auto
                if (colorMatrix_SelectedItem == "auto")
                {
                    return string.Empty;
                }

                // Options
                string colorMatrix = "colormatrix=";

                switch (colorMatrix_SelectedItem)
                {
                    case "BT.709":
                        colorMatrix += "bt709";
                        break;

                    case "FCC":
                        colorMatrix += "fcc";
                        break;

                    //case "BT.601":
                    //    colorMatrix += "bt601";
                    //    break;

                    //case "BT.470":
                    //    colorMatrix += "bt470";
                    //    break;

                    case "BT.470BG":
                        colorMatrix += "bt470bg";
                        break;

                    case "SMPTE-170M":
                        colorMatrix += "smpte170m";
                        break;

                    case "SMPTE-240M":
                        colorMatrix += "smpte240m";
                        break;

                        //case "BT.2020":
                        //    colorMatrix += "bt2020";
                        //    break;
                }

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Color Matrix: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(colorMatrix) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);

                return colorMatrix;
            }

            /// <summary>
            /// Video Color (Method)
            /// <summary>
            public static void Video_Color()
            {
                // Color List
                List<string> vColor_Params_List = new List<string>()
                {
                    //VideoParams.Color_Primaries_Filter(),
                    //VideoParams.Color_TransferCharacteristics_Filter(),
                    //VideoParams.Color_Space_Filter(),
                    //VideoParams.Color_Range_Filter(),
                    Color_Matrix_Filter(VM.VideoView.Video_Color_Matrix_SelectedItem)
                };

                // Join
                string filters = string.Join("\r\n:", vColor_Params_List
                                       .Where(s => !string.IsNullOrEmpty(s)));

                // Video Filter Add
                vParamsList.Add(filters);
            }


            /// <summary>
            /// Video Params Combine (Method)
            /// <summary>
            public static String Video_Params(string video_Quality_SelectedItem,
                                              string video_Codec_SelectedItem,
                                              string format_MediaType_SelectedItem
                )
            {
                // Video BitRate None Check
                // Video Codec None Check
                // Codec Copy Check
                // Media Type Check
                if (video_Quality_SelectedItem != "None" &&
                    video_Codec_SelectedItem != "None" &&
                    video_Codec_SelectedItem != "Copy" &&
                    format_MediaType_SelectedItem != "Audio")
                {
                    //MessageBox.Show(string.Join("", VideoParams.vParamsList)); //debug
                    // --------------------------------------------------
                    // Add Each Filter to Master Filters List
                    // --------------------------------------------------

                    // -------------------------
                    // Color
                    // -------------------------
                    Video_Color();

                    // -------------------------
                    // Empty Halt
                    // -------------------------
                    if (vParamsList == null || // Null Check
                        vParamsList.Count == 0) // None Check
                    {
                        return string.Empty;
                    }

                    // -------------------------
                    // Remove Empty Strings
                    // -------------------------
                    vParamsList.RemoveAll(s => string.IsNullOrEmpty(s));

                    // -------------------------
                    // Codec
                    // -------------------------
                    string codec = string.Empty;
                    switch (VM.VideoView.Video_Codec_SelectedItem)
                    {
                        // x264
                        case "x264":
                            codec = "-x264-params" + " ";
                            break;
                        // x265
                        case "x265":
                            codec = "-x265-params" + " ";
                            break;
                        // All Other Codecs
                        default: return string.Empty;
                    }

                    // -------------------------
                    // 1 Param
                    // -------------------------
                    if (vParamsList.Count == 1)
                    {
                        // Always wrap in quotes
                        vParams = codec + "\"" + string.Join("", vParamsList
                                                       .Where(s => !string.IsNullOrEmpty(s)))
                                        + "\"";
                    }

                    // -------------------------
                    // Multiple Params
                    // -------------------------
                    else if (vParamsList.Count > 1)
                    {
                        // Always wrap in quotes
                        // Linebreak beginning and end
                        vParams = codec + "\"\r\n" + string.Join("\r\n:", vParamsList
                                                           .Where(s => !string.IsNullOrEmpty(s)))
                                        + "\r\n\"";
                    }

                    // -------------------------
                    // Empty
                    // -------------------------
                    else
                    {
                        vParams = string.Empty;
                    }
                }

                //MessageBox.Show(vParams); //debug

                // Return Value
                return vParams;
            }


        }
    }
}
