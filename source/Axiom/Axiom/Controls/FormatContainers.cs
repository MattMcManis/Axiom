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

using System.Collections.Generic;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class Containers
    {
        // --------------------------------------------------
        // Containers
        // --------------------------------------------------

        // -------------------------
        // webm
        // -------------------------
        public class WebM
        {
            // Media Type
            public static List<string> media = new List<string>()
            {
                "Video"
            };

            // Codecs
            public static List<string> video = new List<string>()
            {
                "VP8",
                "VP9",
                "Copy"
            };

            public static List<string> audio = new List<string>()
            {
                "Vorbis",
                "Opus",
                "Copy",
                "None"
            };

            public static List<string> subtitle = new List<string>()
            {
                "None"
            };
        }



        // -------------------------
        // mp4
        // -------------------------
        public class MP4
        {
            // Media Type
            public static List<string> media = new List<string>()
            {
                "Video"
            };

            // Codecs
            public static List<string> video = new List<string>()
            {
                "x264",
                "x265",
                "Copy"
            };

            public static List<string> audio = new List<string>()
            {
                "AC3",
                "AAC",
                "DTS",
                "Copy",
                "None"
            };

            public static List<string> subtitle = new List<string>()
            {
                "None",
                "MOV Text",
                "Burn",
                "Copy"
            };
        }



        // -------------------------
        // mkv
        // -------------------------
        public class MKV
        {
            // Media Type
            public static List<string> media = new List<string>()
            {
                "Video"
            };

            // Codecs
            public static List<string> video = new List<string>()
            {
                "x264",
                "x265",
                "VP8",
                "VP9",
                "AV1",
                "FFV1",
                "HuffYUV",
                "Theora",
                "Copy"
            };

            public static List<string> audio = new List<string>()
            {
                "AC3",
                "AAC",
                "DTS",
                "Vorbis",
                "Opus",
                "LAME",
                "FLAC",
                "PCM",
                "Copy",
                "None"
            };

            public static List<string> subtitle = new List<string>()
            {
                "None",
                "MOV Text",
                "SSA",
                "SRT",
                "Burn",
                "Copy"
            };
        }



        // -------------------------
        // m2v
        // -------------------------
        public class M2V
        {
            // Media Type
            public static List<string> media = new List<string>()
            {
                "Video"
            };

            // Codecs
            public static List<string> video = new List<string>()
            {
                "MPEG-2",
                "Copy"
            };

            public static List<string> audio = new List<string>()
            {
                "None"
            };

            public static List<string> subtitle = new List<string>()
            {
                "None"
            };
        }



        // -------------------------
        // mpg
        // -------------------------
        public class MPG
        {
            // Media Type
            public static List<string> media = new List<string>()
            {
                "Video"
            };

            // Codecs
            public static List<string> video = new List<string>()
            {
                "MPEG-2",
                "MPEG-4",
                "Copy"
            };

            public static List<string> audio = new List<string>()
            {
                "MP2",
                "AC3",
                "AAC",
                "DTS",
                "LAME",
                "PCM",
                "Copy",
                "None"
            };

            public static List<string> subtitle = new List<string>()
            {
                "None",
                "SRT",
                "Burn",
                "Copy"
            };
        }



        // -------------------------
        // avi
        // -------------------------
        public class AVI
        {
            // Media Type
            public static List<string> media = new List<string>()
            {
                "Video"
            };

            // Codecs
            public static List<string> video = new List<string>()
            {
                "MPEG-2",
                "MPEG-4",
                "Copy"
            };

            public static List<string> audio = new List<string>()
            {
                "MP2",
                "AC3",
                "AAC",
                "DTS",
                "LAME",
                "PCM",
                "Copy",
                "None"
            };

            public static List<string> subtitle = new List<string>()
            {
                "None",
                "SRT",
                "Burn",
                "Copy"
            };
        }



        // -------------------------
        // ogv
        // -------------------------
        public class OGV
        {
            // Media Type
            public static List<string> media = new List<string>()
            {
                "Video"

            };
            // Codecs
            public static List<string> video = new List<string>()
            {
                "Theora",
                "Copy"
            };

            public static List<string> audio = new List<string>()
            {
                "Vorbis",
                "Copy",
                "None"
            };

            public static List<string> subtitle = new List<string>()
            {
                "None",
                "Copy"
            };
        }



        // -------------------------
        // mp3
        // -------------------------
        public class LAME
        {
            // Media Type
            public static List<string> media = new List<string>()
            {
                "Audio"
            };

            // Codecs
            public static List<string> video = new List<string>()
            {
                "None"
            };

            public static List<string> audio = new List<string>()
            {
                "LAME",
                "Copy"
            };

            public static List<string> subtitle = new List<string>()
            {
                "None"
            };
        }



        // -------------------------
        // m4a
        // -------------------------
        public class M4A
        {
            // Media Type
            public static List<string> media = new List<string>()
            {
                "Audio"
            };

            // Codecs
            public static List<string> video = new List<string>()
            {
                "None"
            };

            public static List<string> audio = new List<string>()
            {
                "AAC",
                "ALAC",
                "Copy"
            };

            public static List<string> subtitle = new List<string>()
            {
                "None"
            };
        }



        // -------------------------
        // ogg
        // -------------------------
        public class OGG
        {
            // Media Type
            public static List<string> media = new List<string>()
            {
                "Audio"
            };

            // Codecs
            public static List<string> video = new List<string>()
            {
                "None"
            };

            public static List<string> audio = new List<string>()
            {
                "Opus",
                "Vorbis",
                "Copy"
            };

            public static List<string> subtitle = new List<string>()
            {
                "None"
            };
        }



        // -------------------------
        // flac
        // -------------------------
        public class FLAC
        {
            // Media Type
            public static List<string> media = new List<string>()
            {
                "Audio"
            };

            // Codecs
            public static List<string> video = new List<string>()
            {
                "None"
            };

            public static List<string> audio = new List<string>()
            {
                "FLAC",
                "Copy"
            };

            public static List<string> subtitle = new List<string>()
            {
                "None"
            };
        }



        // -------------------------
        // wav
        // -------------------------
        public class WAV
        {
            // Media Type
            public static List<string> media = new List<string>()
            {
                "Audio"
            };

            // Codecs
            public static List<string> video = new List<string>()
            {
                "None"
            };

            public static List<string> audio = new List<string>()
            {
                "PCM",
                "Copy"
            };

            public static List<string> subtitle = new List<string>()
            {
                "None"
            };
        }



        // -------------------------
        // jpg
        // -------------------------
        public class JPG
        {
            // Media Type
            public static List<string> media = new List<string>()
            {
                "Image",
                "Sequence"
            };

            // Codecs
            public static List<string> video = new List<string>()
            {
                "JPEG"
            };

            public static List<string> audio = new List<string>()
            {
                "None"
            };

            public static List<string> subtitle = new List<string>()
            {
                "None",
                "Burn"
            };
        }



        // -------------------------
        // png
        // -------------------------
        public class PNG
        {
            // Media Type
            public static List<string> media = new List<string>()
            {
                "Image",
                "Sequence"
            };

            // Codecs
            public static List<string> video = new List<string>()
            {
                "PNG"
            };

            public static List<string> audio = new List<string>()
            {
                "None"
            };

            public static List<string> subtitle = new List<string>()
            {
                "None",
                "Burn"
            };
        }



        // -------------------------
        // webp
        // -------------------------
        public class WebP
        {
            // Media Type
            public static List<string> media = new List<string>()
            {
                "Image",
                "Sequence"
            };

            // Codecs
            public static List<string> video = new List<string>()
            {
                "WebP"
            };

            public static List<string> audio = new List<string>()
            {
                "None"
            };

            public static List<string> subtitle = new List<string>()
            {
                "None",
                "Burn"
            };
        }



    }
}
