/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017, 2018 Matt McManis
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class ViewModel
    {
        /// <summary>
        /// Change Item Source (Method)
        /// </summary>
        public static void ChangeItemSource(
            MainWindow mainwindow,
            ComboBox cbo,                               // ComboBox
            List<string> items,                         // New Items List
            ObservableCollection<string> collection,    // View Model Observable Collection
            string selectedItem)                        // Selected Item
        {
            // -------------------------
            // Get Previous Item
            // -------------------------
            string previousItem = selectedItem;

            // -------------------------
            // Change Item Source
            // -------------------------
            // Clear List
            if (collection != null && collection.Count > 0) // null check
            {
                collection.Clear();
            }

            // Add Items
            if (items != null) // null check
            {
                if (items.Count > 0) // null check
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        collection.Add(items[i]);
                    }
                }
            }

            //items.ForEach(collection.Add);

            // -------------------------
            // Select Item
            // -------------------------
            if (!string.IsNullOrEmpty(previousItem))
            {
                if (collection.Contains(previousItem))
                {
                    cbo.SelectedItem = previousItem;

                    return;
                }
                else
                {
                    cbo.SelectedIndex = 0; // Auto

                    return;
                }
            }
        }


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Format
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // -------------------------
        // Container
        // -------------------------
        // Items
        public static ObservableCollection<string> _cboContainer_Items = new ObservableCollection<string>()
        {
            "webm",
            "mp4",
            "mkv",
            "avi",
            "ogv",
            "mp3",
            "m4a",
            "ogg",
            "flac",
            "wav",
            "jpg",
            "png"
        };

        public static ObservableCollection<string> cboContainer_Items
        {
            get { return _cboContainer_Items; }
            set { _cboContainer_Items = value; }
        }

        // Selected Item
        public static string cboContainer_SelectedItem { get; set; }


        // -------------------------
        // Container
        // -------------------------
        // Items
        public static ObservableCollection<string> _cboMediaType_Items = new ObservableCollection<string>()
        {
            "Video",
            "Audio",
            "Image",
            "Sequence"
        };

        public static ObservableCollection<string> cboMediaType_Items
        {
            get { return _cboMediaType_Items; }
            set { _cboMediaType_Items = value; }
        }

        // Selected Item
        public static string cboMediaType_SelectedItem { get; set; }


        // -------------------------
        // Video Codec
        // -------------------------
        // Items
        public static ObservableCollection<string> _cboVideoCodec_Items = new ObservableCollection<string>();

        public static ObservableCollection<string> cboVideoCodec_Items
        {
            get { return _cboVideoCodec_Items; }
            set { _cboVideoCodec_Items = value; }
        }

        // Selected Item
        public static string cboVideoCodec_SelectedItem { get; set; }


        // -------------------------
        // Audio Codec
        // -------------------------
        // Items
        public static ObservableCollection<string> _cboAudioCodec_Items = new ObservableCollection<string>();

        public static ObservableCollection<string> cboAudioCodec_Items
        {
            get { return _cboAudioCodec_Items; }
            set { _cboAudioCodec_Items = value; }
        }

        // Selected Item
        public static string cboAudioCodec_SelectedItem { get; set; }


        // -------------------------
        // Subtitle Codec
        // -------------------------
        // Items
        public static ObservableCollection<string> _cboSubtitleCodec_Items = new ObservableCollection<string>();

        public static ObservableCollection<string> cboSubtitleCodec_Items
        {
            get { return _cboSubtitleCodec_Items; }
            set { _cboSubtitleCodec_Items = value; }
        }

        // Selected Item
        public static string cboSubtitleCodec_SelectedItem { get; set; }



        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Video
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // -------------------------
        // Quality
        // -------------------------
        // Items - Changed in VideoControls Class
        public static ObservableCollection<string> _cboVideoQuality_Items = new ObservableCollection<string>(); 

        public static ObservableCollection<string> cboVideoQuality_Items
        {
            get { return _cboVideoQuality_Items; }
            set { _cboVideoQuality_Items = value; }
        }

        // Selected Item
        public static string cboVideoQuality_SelectedItem { get; set; }


        // -------------------------
        // Pass
        // -------------------------
        // Items - Changed in VideoControls Class
        public static ObservableCollection<string> _cboVideoPass_Items = new ObservableCollection<string>();

        public static ObservableCollection<string> cboVideoPass_Items
        {
            get { return _cboVideoPass_Items; }
            set { _cboVideoPass_Items = value; }
        }

        // Selected Item
        public static string cboVideoPass_SelectedItem { get; set; }


        // -------------------------
        // Optimize
        // -------------------------
        // Items - Changed in VideoControls Class
        public static ObservableCollection<string> _cboOptimize_Items = new ObservableCollection<string>();

        public static ObservableCollection<string> cboOptimize_Items
        {
            get { return _cboOptimize_Items; }
            set { _cboOptimize_Items = value; }
        }

        // Selected Item
        public static string cboOptimize_SelectedItem { get; set; }


        // -------------------------
        // Scaling
        // -------------------------
        // Items
        public static ObservableCollection<string> _cboScaling_Items = new ObservableCollection<string>()
        {
            "None",
            "Default",
            "neighbor",
            "area",
            "fast_bilinear",
            "bilinear",
            "bicubic",
            "experimental",
            "bicublin",
            "gauss",
            "sinc",
            "lanczos",
            "spline",
        };

        public static ObservableCollection<string> cboScaling_Items
        {
            get { return _cboScaling_Items; }
            set { _cboScaling_Items = value; }
        }

        // Selected Item
        public static string cboScaling_SelectedItem { get; set; }



        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Audio
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // -------------------------
        // Pass
        // -------------------------
        // -------------------------
        // Quality
        // -------------------------
        // Items - Changed in AudioControls Class
        public static ObservableCollection<string> _cboAudioQuality_Items = new ObservableCollection<string>();

        public static ObservableCollection<string> cboAudioQuality_Items
        {
            get { return _cboAudioQuality_Items; }
            set { _cboAudioQuality_Items = value; }
        }

        // Selected Item
        public static string cboAudioQuality_SelectedItem { get; set; }


        // -------------------------
        // Channel
        // -------------------------
        // Items - Changed in AudioControls Class
        public static ObservableCollection<string> _cboAudioChannel_Items = new ObservableCollection<string>();

        public static ObservableCollection<string> cboAudioChannel_Items
        {
            get { return _cboAudioChannel_Items; }
            set { _cboAudioChannel_Items = value; }
        }

        // Selected Item
        public static string cboAudioChannel_SelectedItem { get; set; }


        // -------------------------
        // Sample Rate
        // -------------------------
        // Items - Changed in AudioControls Class
        public static ObservableCollection<string> _cboAudioSampleRate_Items = new ObservableCollection<string>();

        public static ObservableCollection<string> cboAudioSampleRate_Items
        {
            get { return _cboAudioSampleRate_Items; }
            set { _cboAudioSampleRate_Items = value; }
        }

        // Selected Item
        public static string cboAudioSampleRate_SelectedItem { get; set; }


        // -------------------------
        // Bit Depth
        // -------------------------
        // Items - Changed in AudioControls Class
        public static ObservableCollection<string> _cboAudioBitDepth_Items = new ObservableCollection<string>();

        public static ObservableCollection<string> cboAudioBitDepth_Items
        {
            get { return _cboAudioBitDepth_Items; }
            set { _cboAudioBitDepth_Items = value; }
        }

        // Selected Item
        public static string cboAudioBitDepth_SelectedItem { get; set; }


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Settings
        /// </summary>
        // --------------------------------------------------------------------------------------------------------



    }
}
