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
using System.IO;
using ViewModel;
using Axiom;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Controls.Subtitles
{
    public class Controls
    {
        public static Dictionary<string, dynamic> codecClasses = new Dictionary<string, dynamic>
        {
            { "MOV Text",   new Codec.MOV_Text() },
            { "SSA",        new Codec.SSA() },
            { "SRT",        new Codec.SRT() },
            { "Burn",       new Codec.Burn() },
            { "Copy",       new Codec.Copy() },
            { "None",       new Codec.None() }
        };

        private static Dictionary<string, ISubtitleCodec> _codec_class;

        private static void InitializeCodecs()
        {
            _codec_class = codecClasses.ToDictionary(k => k.Key, k => (ISubtitleCodec)k.Value);

            //_codec_class = new Dictionary<string, ISubtitleCodec> {
            //    { "MOV Text",   new Codec.MOV_Text() },
            //    { "SSA",        new Codec.SSA() },
            //    { "SRT",        new Codec.SRT() },
            //    { "Burn",       new Codec.Burn() },
            //    { "Copy",       new Codec.Copy() },
            //    { "None",       new Codec.None() }
            //};
        }

        public interface ISubtitleCodec
        {
            // Codec
            ObservableCollection<ViewModel.Subtitle.SubtitleCodec> codec { get; set; }

            // Items Source
            ObservableCollection<string> stream { get; set; }

            // Selected Items
            List<ViewModel.Subtitle.Selected> controls_Selected { get; set; }
        }

        /// <summary>
        /// Codec Controls
        /// </summary>
        public static void CodecControls(string codec_SelectedItem)
        {
            // --------------------------------------------------
            // Codec
            // --------------------------------------------------

            if (!string.IsNullOrWhiteSpace(codec_SelectedItem))
            {
                InitializeCodecs();

                // -------------------------
                // Codec
                // -------------------------
                List<string> codec = new List<string>()
                {
                    //"-c:s",
                    _codec_class[codec_SelectedItem].codec.FirstOrDefault()?.Codec,
                    _codec_class[codec_SelectedItem].codec.FirstOrDefault()?.Parameters
                };

                // Initiate all codecs with -c:s, except Burn
                // Burn uses Filter -vf
                if (codec_SelectedItem != "Burn") 
                {
                    codec.Insert(0, "-c:s");
                }

                VM.SubtitleView.Subtitle_Codec = string.Join(" ", codec.Where(s => !string.IsNullOrEmpty(s)));

                // -------------------------
                // Items Source
                // -------------------------
                VM.SubtitleView.Subtitle_Stream_Items = _codec_class[codec_SelectedItem].stream;


                // -------------------------
                // Enabled
                // -------------------------
                // Subtitle Codec
                VM.SubtitleView.Subtitle_Codec_IsEnabled = true;

                // Subtitle Stream
                VM.SubtitleView.Subtitle_Stream_IsEnabled = true;


                // -------------------------
                // Selected Items
                // -------------------------
                string stream = _codec_class[codec_SelectedItem].controls_Selected
                                                                .Find(item => item.Stream == item.Stream)
                                                                .Stream;
                if (!string.IsNullOrEmpty(stream))
                {
                    VM.SubtitleView.Subtitle_Stream_SelectedItem = stream;
                }
            }

            // --------------------------------------------------
            // Selected Items
            // --------------------------------------------------
            switch (VM.SubtitleView.Subtitle_Codec_SelectedItem)
            {
                case "None":
                    VM.SubtitleView.Subtitle_Stream_SelectedItem = "none";
                    VM.SubtitleView.Subtitle_Stream_IsEnabled = false;
                    break;

                case "Burn":
                    // Force Select External
                    // Can't burn All subtitle streams
                    VM.SubtitleView.Subtitle_Stream_SelectedItem = "external";
                    VM.SubtitleView.Subtitle_Stream_IsEnabled = true;
                    break;

                case "Copy":
                    VM.SubtitleView.Subtitle_Stream_IsEnabled = true;
                    break;

                // All Other Codecs
                default:
                    VM.SubtitleView.Subtitle_Stream_IsEnabled = true;
                    break;
            }
        }


        /// <summary>
        /// Auto Copy Conditions Check
        /// <summary>
        //public static bool AutoCopyConditionsCheck()
        //{
        //    // Failed
        //    if (VM.VideoView.Video_Quality_SelectedItem != "Auto" ||
        //        VM.VideoView.Video_Scale_SelectedItem != "Source" ||
        //        !string.IsNullOrWhiteSpace(CropWindow.crop) ||
        //        VM.VideoView.Video_FPS_SelectedItem != "auto" ||
        //        VM.VideoView.Video_Optimize_SelectedItem != "None"
        //    )
        //    {
        //        return false;
        //    }

        //    // Passed
        //    else
        //    {
        //        return true;
        //    }
        //}


        /// <summary>
        /// Copy Controls
        /// <summary>
        //public static void AutoCopySubtitleCodec(string trigger)
        //{
        //    // -------------------------
        //    // Halt if Selected Codec is Null
        //    // -------------------------
        //    if (string.IsNullOrWhiteSpace(VM.SubtitleView.Subtitle_Codec_SelectedItem))
        //    {
        //        return;
        //    }

        //    // -------------------------
        //    // Halt if trigger is control
        //    // Pass if trigger is input
        //    // -------------------------
        //    if (trigger == "control" &&
        //        VM.SubtitleView.Subtitle_Codec_SelectedItem != "Copy" &&
        //        AutoCopyConditionsCheck() == true)
        //    {
        //        return;
        //    }

        //    // -------------------------
        //    // Halt if Web URL
        //    // -------------------------
        //    if (MainWindow.IsWebURL(VM.MainView.Input_Text) == true)
        //    {
        //        return;
        //    }

        //    // -------------------------
        //    // Get Input Extensions
        //    // -------------------------
        //    string inputExt = Path.GetExtension(VM.MainView.Input_Text);

        //    // -------------------------
        //    // Halt if Input Extension is Empty
        //    // -------------------------
        //    if (string.IsNullOrWhiteSpace(inputExt))
        //    {
        //        return;
        //    }

        //    // -------------------------
        //    // Get Output Extensions
        //    // -------------------------
        //    string outputExt = "." + VM.FormatView.Format_Container_SelectedItem;

        //    // -------------------------
        //    // Conditions Check
        //    // Enable
        //    // -------------------------
        //    if (AutoCopyConditionsCheck() == true &&
        //        string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
        //    {
        //        // -------------------------
        //        // Set Subtitle Codec Combobox Selected Item to Copy
        //        // -------------------------
        //        if (VM.SubtitleView.Subtitle_Codec_Items.Count > 0)
        //        {
        //            if (VM.SubtitleView.Subtitle_Codec_Items?.Contains("Copy") == true)
        //            {
        //                VM.SubtitleView.Subtitle_Codec_SelectedItem = "Copy";
        //            }
        //        }
        //    }

        //    // -------------------------
        //    // Reset to Default Codec
        //    // -------------------------
        //    // Disable Copy if:
        //    // Input / Output Extensions don't match
        //    // Batch / Output Extensions don't match
        //    // Size is Not No
        //    // Crop is Not Empty
        //    // FPS is Not Auto
        //    // Optimize is Not None
        //    // -------------------------
        //    else
        //    {
        //        // -------------------------
        //        // Copy Selected 
        //        // -------------------------
        //        if (VM.SubtitleView.Subtitle_Codec_SelectedItem == "Copy")
        //        {
        //            switch (VM.FormatView.Format_Container_SelectedItem)
        //            {
        //                // -------------------------
        //                // WebM
        //                // -------------------------
        //                case "webm":
        //                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
        //                    break;
        //                // -------------------------
        //                // MP4
        //                // -------------------------
        //                case "mp4":
        //                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "MOV Text";
        //                    break;
        //                // -------------------------
        //                // MKV
        //                // -------------------------
        //                case "mkv":
        //                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "Copy";
        //                    break;
        //                // -------------------------
        //                // MPG
        //                // -------------------------
        //                case "mpg":
        //                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "Copy";
        //                    break;
        //                // -------------------------
        //                // AVI
        //                // -------------------------
        //                case "avi":
        //                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "SRT";
        //                    break;
        //                // -------------------------
        //                // OGV
        //                // -------------------------
        //                case "ogv":
        //                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
        //                    break;
        //                // -------------------------
        //                // JPG
        //                // -------------------------
        //                case "jpg":
        //                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
        //                    break;
        //                // -------------------------
        //                // PNG
        //                // -------------------------
        //                case "png":
        //                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
        //                    break;
        //                // -------------------------
        //                // WebP
        //                // -------------------------
        //                case "webp":
        //                    VM.SubtitleView.Subtitle_Codec_SelectedItem = "None";
        //                    break;
        //            }
        //        }
        //    }
        //}

    }
}
