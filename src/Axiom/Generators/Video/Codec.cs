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

  * Video Codec
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
    public class Codec
    {
        public static string vCodec { get; set; } // Video Codec

        /// <summary>
        /// Video Codec
        /// <summary>
        public static String VideoCodec(string hwAccel_transcode_SelectedItem,
                                        string codec_SelectedItem,
                                        string codec_Command
                                        )
        {
            // Passed Command
            if (codec_SelectedItem != "None")
            {
                vCodec = codec_Command;

                //// HW Acceleration vCodec Override
                //if (hwAccel_transcode_SelectedItem == "AMD AMF" || // h264_amf / hevc_amf
                //    hwAccel_transcode_SelectedItem == "NVIDIA NVENC" || // h264_nvenc / hevc_nvenc
                //    hwAccel_transcode_SelectedItem == "Intel QSV" // h264_qsv / hevc_qsv
                //    )
                //{
                //    vCodec = Encoding.HWAccelerationCodecOverride(hwAccel_transcode_SelectedItem,
                //                                                  codec_SelectedItem
                //                                                 );

                //    //MessageBox.Show(vCodec); //debug
                //}
            }

            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Codec: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(codec_Command) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

            //MessageBox.Show(vCodec); //debug

            return vCodec;
        }
    }
}
