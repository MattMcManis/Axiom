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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class ViewModel : INotifyPropertyChanged
    {
        //private MainWindow mainwindow = (MainWindow)System.Windows.Application.Current.MainWindow;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void OnPropertyChanged(string prop)
        {
            //PropertyChangedEventHandler handler = PropertyChanged;
            //handler(this, new PropertyChangedEventArgs(name));

            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(prop));
            }
        }

        public ViewModel()
        {
            // -------------------------
            // ComboBox Defaults
            // -------------------------
            // Main
            Preset_IsEnabled = true;
            Preset_SelectedItem = Preset_Items.FirstOrDefault();

            // Format
            Container_SelectedItem = Container_Items.FirstOrDefault();
            MediaType_SelectedItem = "Video";
            //Container_SelectedIndex = 0;
            //Container_SelectedItem = "webm";
            //VideoEncodeSpeed_SelectedItem = "Medium";
            HWAccel_SelectedItem = "off";
            Cut_SelectedItem = "No";

            // Video
            VideoCodec_SelectedItem = "VP8";
            VideoQuality_SelectedItem = "Auto";
            Pass_SelectedItem = "2 Pass";
            PixelFormat_SelectedItem = "auto";
            FPS_SelectedItem = "auto";
            Video_Optimize_SelectedItem = "None";
            Optimize_Tune_SelectedItem = "none";
            Optimize_Profile_SelectedItem = "none";
            Optimize_Level_SelectedItem = "none";

            Size_SelectedItem = "Source";
            Scaling_SelectedItem = "default";

            // Subtitle
            SubtitleCodec_SelectedItem = "None";
            SubtitleStream_SelectedItem = "none";

            // Audio
            AudioCodec_SelectedItem = "Vorbis";
            AudioStream_SelectedItem = "1";
            AudioChannel_SelectedItem = "Source";
            AudioQuality_SelectedItem = "Auto";
            AudioSampleRate_SelectedItem = "auto";
            AudioBitDepth_SelectedItem = "auto";

            // Configure
            Theme_SelectedItem = "Axiom";
            Threads_SelectedItem = "optimal";
        }

        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Tools
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------
        // CMD Window Keep - Toggle
        // --------------------------------------------------
        private bool _CMDWindowKeep_IsChecked;
        public bool CMDWindowKeep_IsChecked
        {
            get { return _CMDWindowKeep_IsChecked; }
            set
            {
                if (_CMDWindowKeep_IsChecked != value)
                {
                    _CMDWindowKeep_IsChecked = value;
                    OnPropertyChanged("CMDWindowKeep_IsChecked");
                }
            }
        }
        //private static bool _CMDWindowKeep_IsChecked;
        //public bool CMDWindowKeep_IsChecked
        //{
        //    get { return _CMDWindowKeep_IsChecked; }
        //    set
        //    {
        //        if (_CMDWindowKeep_IsChecked == value) return;

        //        _CMDWindowKeep_IsChecked = value;
        //    }
        //}

        // --------------------------------------------------
        // Auto Sort Script - Toggle
        // --------------------------------------------------
        private bool _AutoSortScript_IsChecked;
        public bool AutoSortScript_IsChecked
        {
            get { return _AutoSortScript_IsChecked; }
            set
            {
                if (_AutoSortScript_IsChecked != value)
                {
                    _AutoSortScript_IsChecked = value;
                    OnPropertyChanged("AutoSortScript_IsChecked");
                }
            }
        }
        //private static bool _AutoSortScript_IsChecked;
        //public bool AutoSortScript_IsChecked
        //{
        //    get { return _AutoSortScript_IsChecked; }
        //    set
        //    {
        //        if (_AutoSortScript_IsChecked == value) return;

        //        _AutoSortScript_IsChecked = value;
        //    }
        //}


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Input
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------
        // Input - TextBox
        // --------------------------------------------------
        // Text
        private string _Input_Text = string.Empty;
        public string Input_Text
        {
            get { return _Input_Text; }
            set
            {
                if (_Input_Text == value)
                {
                    return;
                }

                _Input_Text = value;
                OnPropertyChanged("Input_Text");
            }
        }

        // --------------------------------------------------
        // Output - TextBox
        // --------------------------------------------------
        // Text
        private string _Output_Text = string.Empty;
        public string Output_Text
        {
            get { return _Output_Text; }
            set
            {
                if (_Output_Text == value)
                {
                    return;
                }

                _Output_Text = value;
                OnPropertyChanged("Output_Text");
            }
        }

        // --------------------------------------------------
        // Batch - Toggle
        // --------------------------------------------------
        // Checked
        private bool _Batch_IsChecked;
        public bool Batch_IsChecked
        {
            get { return _Batch_IsChecked; }
            set
            {
                if (_Batch_IsChecked != value)
                {
                    _Batch_IsChecked = value;
                    OnPropertyChanged("Batch_IsChecked");
                }
            }
        }
        //private static bool _Batch_IsChecked = false;
        //public bool Batch_IsChecked
        //{
        //    get { return _Batch_IsChecked; }
        //    set
        //    {
        //        if (_Batch_IsChecked == value) return;

        //        _Batch_IsChecked = value;
        //    }
        //}

        // --------------------------------------------------
        // Batch Extension - TextBox
        // --------------------------------------------------
        // Text
        private string _BatchExtension_Text = "extension";
        public string BatchExtension_Text
        {
            get { return _BatchExtension_Text; }
            set
            {
                if (_BatchExtension_Text == value)
                {
                    return;
                }

                _BatchExtension_Text = value;
                OnPropertyChanged("BatchExtension_Text");
            }
        }
        // Enabled
        private bool _BatchExtension_IsEnabled { get; set; }
        public bool BatchExtension_IsEnabled
        {
            get { return _BatchExtension_IsEnabled; }
            set
            {
                if (_BatchExtension_IsEnabled == value)
                {
                    return;
                }

                _BatchExtension_IsEnabled = value;
                OnPropertyChanged("BatchExtension_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Script View
        // --------------------------------------------------
        // FlowDocument
        private FlowDocument _ScriptView_FlowDocument;
        public FlowDocument ScriptView_FlowDocument
        {
            get { return _ScriptView_FlowDocument; }
            set
            {
                if (_ScriptView_FlowDocument == value)
                {
                    return;
                }

                _ScriptView_FlowDocument = value;
                OnPropertyChanged("ScriptView_FlowDocument");
            }
        }

        // Paragraph
        private Paragraph _ScriptView_Paragraph;
        public Paragraph ScriptView_Paragraph
        {
            get { return _ScriptView_Paragraph; }
            set
            {
                if (_ScriptView_Paragraph == value)
                {
                    return;
                }

                _ScriptView_Paragraph = value;
                OnPropertyChanged("ScriptView_Paragraph");
            }
        }

        // Text
        private string _ScriptView_Text;
        public string ScriptView_Text
        {
            get { return _ScriptView_Text; }
            set
            {
                if (value != _ScriptView_Text)
                {
                    _ScriptView_Text = value;
                    OnPropertyChanged("ScriptView_Text");
                }
            }
        }
        //private string _ScriptView_Text;
        //public string ScriptView_Text
        //{
        //    get { return _ScriptView_Text; }
        //    set
        //    {
        //        if (_ScriptView_Text == value)
        //        {
        //            return;
        //        }

        //        _ScriptView_Text = value;
        //        OnPropertyChanged("ScriptView_Text");
        //    }
        //}

        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Settings
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------
        // Preset
        // --------------------------------------------------
        // Item Source
        private List<string> _Preset_Items = new List<string>()
        {
            "Preset",
            "Default",
            "DVD",
            "Blu-ray",
            "HD Video",
            "SD Video",
            "HTML5",
            "Android",
            "iOS",
            "iTunes",
            "MP3 HQ",
            "PS3",
            "PS4",
            "Xbox 360",
            "Xbox One",
            "Debug"
        };
        public List<string> Preset_Items
        {
            get { return _Preset_Items; }
            set
            {
                _Preset_Items = value;
                OnPropertyChanged("Preset_Items");
            }
        }

        // Selected Index
        private int _Preset_SelectedIndex { get; set; }
        public int Preset_SelectedIndex
        {
            get { return _Preset_SelectedIndex; }
            set
            {
                if (_Preset_SelectedIndex == value)
                {
                    return;
                }

                _Preset_SelectedIndex = value;
                OnPropertyChanged("Preset_SelectedIndex");
            }
        }

        // Selected Item
        private string _Preset_SelectedItem { get; set; }
        public string Preset_SelectedItem
        {
            get { return _Preset_SelectedItem; }
            set
            {
                if (_Preset_SelectedItem == value)
                {
                    return;
                }

                _Preset_SelectedItem = value;
                OnPropertyChanged("Preset_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Preset_IsEnabled { get; set; }
        public bool Preset_IsEnabled
        {
            get { return _Preset_IsEnabled; }
            set
            {
                if (_Preset_IsEnabled == value)
                {
                    return;
                }

                _Preset_IsEnabled = value;
                OnPropertyChanged("Preset_IsEnabled");
            }
        }

        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Format
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------
        // Containers
        // --------------------------------------------------
        // Item Source
        private List<string> _Container_Items = new List<string>()
        {
            "webm",
            "mp4",
            "mkv",
            "m2v",
            "mpg",
            "avi",
            "ogv",
            "mp3",
            "m4a",
            "ogg",
            "flac",
            "wav",
            "jpg",
            "png",
            "webp"
        };
        public List<string> Container_Items
        {
            get { return _Container_Items; }
            set
            {
                _Container_Items = value;
                OnPropertyChanged("Container_Items");
            }
        }

        // Selected Index
        //private int _Container_SelectedIndex { get; set; }
        //public int Container_SelectedIndex
        //{
        //    get
        //    {
        //        if (_Container_SelectedIndex == 0)
        //        {
        //            Container_Items.FirstOrDefault();
        //        }

        //        return _Container_SelectedIndex;
        //    }
        //    set
        //    {
        //        _Container_SelectedIndex = value;
        //        OnPropertyChanged("Container_SelectedIndex");
        //    }
        //}
        //public int Container_SelectedIndex
        //{
        //    get { return _Container_SelectedIndex; }
        //    set
        //    {
        //        _Container_SelectedIndex = value;
        //        OnPropertyChanged("Container_SelectedIndex");
        //    }
        //}
        private int _Container_SelectedIndex { get; set; }
        public int Container_SelectedIndex
        {
            get { return _Container_SelectedIndex; }
            set
            {
                //if (_Container_SelectedIndex != value)
                //{
                //    _Container_SelectedIndex = value;
                //    OnPropertyChanged("Container_SelectedIndex");
                //}
                if (_Container_SelectedIndex == value)
                {
                    return;
                }

                _Container_SelectedIndex = value;
                OnPropertyChanged("Container_SelectedIndex");
            }
        }

        // Selected Item
        //public string Container_SelectedItem { get; set; }
        private string _Container_SelectedItem { get; set; }
        public string Container_SelectedItem
        {
            get { return _Container_SelectedItem; }
            set
            {
                if (_Container_SelectedItem == value)
                {
                    return;
                }

                _Container_SelectedItem = value;
                OnPropertyChanged("Container_SelectedItem");
            }
        }


        // --------------------------------------------------
        // Media Type
        // --------------------------------------------------
        // Item Source
        private List<string> _MediaType_Items = new List<string>()
        {
            "Video",
            "Audio",
            "Image",
            "Sequence"
        };
        public List<string> MediaType_Items
        {
            get { return _MediaType_Items; }
            set
            {
                _MediaType_Items = value;
                OnPropertyChanged("MediaType_Items");
            }
        }

        // Selected Index
        private int _MediaType_SelectedIndex { get; set; }
        public int MediaType_SelectedIndex
        {
            get { return _MediaType_SelectedIndex; }
            set
            {
                if (_MediaType_SelectedIndex == value)
                {
                    return;
                }

                _MediaType_SelectedIndex = value;
                OnPropertyChanged("MediaType_SelectedIndex");
            }
        }

        // Selected Item
        private string _MediaType_SelectedItem { get; set; }
        public string MediaType_SelectedItem
        {
            get { return _MediaType_SelectedItem; }
            set
            {
                if (_MediaType_SelectedItem == value)
                {
                    return;
                }

                _MediaType_SelectedItem = value;
                OnPropertyChanged("MediaType_SelectedItem");
            }
        }

        // Controls Enable
        private bool _MediaType_IsEnabled = true;
        public bool MediaType_IsEnabled
        {
            get { return _MediaType_IsEnabled; }
            set
            {
                if (_MediaType_IsEnabled == value)
                {
                    return;
                }

                _MediaType_IsEnabled = value;
                OnPropertyChanged("MediaType_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Encode Speed
        // --------------------------------------------------
        // Item Source
        public class VideoEncodeSpeed
        {
            public string Name { get; set; }
            public string Command { get; set; }
            public string Command_2Pass { get; set; }
        }
        public List<VideoEncodeSpeed> _VideoEncodeSpeed_Items = new List<VideoEncodeSpeed>();
        public List<VideoEncodeSpeed> VideoEncodeSpeed_Items
        {
            get { return _VideoEncodeSpeed_Items; }
            set
            {
                _VideoEncodeSpeed_Items = value;
                OnPropertyChanged("VideoEncodeSpeed_Items");
            }
        }
        //private List<string> _EncodeSpeed_Items = new List<string>()
        //{
        //    "Placebo",
        //    "Very Slow",
        //    "Slower",
        //    "Slow",
        //    "Medium",
        //    "Fast",
        //    "Faster",
        //    "Very Fast",
        //    "Super Fast",
        //    "Ultra Fast"
        //};
        //public List<string> EncodeSpeed_Items
        //{
        //    get { return _EncodeSpeed_Items; }
        //    set
        //    {
        //        _EncodeSpeed_Items = value;
        //        OnPropertyChanged("EncodeSpeed_Items");
        //    }
        //}

        // Selected Index
        private int _VideoEncodeSpeed_SelectedIndex { get; set; }
        public int VideoEncodeSpeed_SelectedIndex
        {
            get { return _VideoEncodeSpeed_SelectedIndex; }
            set
            {
                if (_VideoEncodeSpeed_SelectedIndex == value)
                {
                    return;
                }

                _VideoEncodeSpeed_SelectedIndex = value;
                OnPropertyChanged("VideoEncodeSpeed_SelectedIndex");
            }
        }

        // Selected Item
        private string _VideoEncodeSpeed_SelectedItem { get; set; }
        public string VideoEncodeSpeed_SelectedItem
        {
            get { return _VideoEncodeSpeed_SelectedItem; }
            set
            {
                var previousItem = _VideoEncodeSpeed_SelectedItem;

                if (!string.IsNullOrEmpty(VideoEncodeSpeed_SelectedItem) &&
                    VideoEncodeSpeed_SelectedItem != "None")
                {
                    MainWindow.VideoEncodeSpeed_PreviousItem = previousItem;
                }


                if (_VideoEncodeSpeed_SelectedItem == value)
                {
                    return;
                }

                _VideoEncodeSpeed_SelectedItem = value;
                OnPropertyChanged("VideoEncodeSpeed_SelectedItem");
            }
        }

        // Controls Enable
        private bool _VideoEncodeSpeed_IsEnabled = true;
        public bool VideoEncodeSpeed_IsEnabled
        {
            get { return _VideoEncodeSpeed_IsEnabled; }
            set
            {
                if (_VideoEncodeSpeed_IsEnabled == value)
                {
                    return;
                }

                _VideoEncodeSpeed_IsEnabled = value;
                OnPropertyChanged("VideoEncodeSpeed_IsEnabled");
            }
        }


        // --------------------------------------------------
        // HW Accel
        // --------------------------------------------------
        // Item Source
        private List<string> _HWAccel_Items = new List<string>()
        {
            "off",
            "dxva2",
            "cuvid",
            "nvenc",
            "cuvid+nvenc"
        };
        public List<string> HWAccel_Items
        {
            get { return _HWAccel_Items; }
            set
            {
                _HWAccel_Items = value;
                OnPropertyChanged("HWAccel_Items");
            }
        }

        // Selected Index
        private int _HWAccel_SelectedIndex { get; set; }
        public int HWAccel_SelectedIndex
        {
            get { return _HWAccel_SelectedIndex; }
            set
            {
                if (_HWAccel_SelectedIndex == value)
                {
                    return;
                }

                _HWAccel_SelectedIndex = value;
                OnPropertyChanged("HWAccel_SelectedIndex");
            }
        }

        // Selected Item
        private string _HWAccel_SelectedItem { get; set; }
        public string HWAccel_SelectedItem
        {
            get { return _HWAccel_SelectedItem; }
            set
            {
                if (_HWAccel_SelectedItem == value)
                {
                    return;
                }

                _HWAccel_SelectedItem = value;
                OnPropertyChanged("HWAccel_SelectedItem");
            }
        }

        // Controls Enable
        private bool _HWAccel_IsEnabled = true;
        public bool HWAccel_IsEnabled
        {
            get { return _HWAccel_IsEnabled; }
            set
            {
                if (_HWAccel_IsEnabled == value)
                {
                    return;
                }

                _HWAccel_IsEnabled = value;
                OnPropertyChanged("HWAccel_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Cut
        // --------------------------------------------------
        // Item Source
        private List<string> _Cut_Items = new List<string>()
        {
            "No",
            "Yes"
        };
        public List<string> Cut_Items
        {
            get { return _Cut_Items; }
            set
            {
                _Cut_Items = value;
                OnPropertyChanged("Cut_Items");
            }
        }

        // Selected Index
        private int _Cut_SelectedIndex { get; set; }
        public int Cut_SelectedIndex
        {
            get { return _Cut_SelectedIndex; }
            set
            {
                if (_Cut_SelectedIndex == value)
                {
                    return;
                }

                _Cut_SelectedIndex = value;
                OnPropertyChanged("Cut_SelectedIndex");
            }
        }

        // Selected Item
        private string _Cut_SelectedItem { get; set; }
        public string Cut_SelectedItem
        {
            get { return _Cut_SelectedItem; }
            set
            {
                if (_Cut_SelectedItem == value)
                {
                    return;
                }

                _Cut_SelectedItem = value;
                OnPropertyChanged("Cut_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Cut_IsEnabled = true;
        public bool Cut_IsEnabled
        {
            get { return _Cut_IsEnabled; }
            set
            {
                if (_Cut_IsEnabled == value)
                {
                    return;
                }

                _Cut_IsEnabled = value;
                OnPropertyChanged("Cut_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Cut Start
        // --------------------------------------------------
        // Text
        private string _CutStart_Text = "00:00:00.000";
        public string CutStart_Text
        {
            get { return _CutStart_Text; }
            set
            {
                if (_CutStart_Text == value)
                {
                    return;
                }

                _CutStart_Text = value;
                OnPropertyChanged("CutStart_Text");
            }
        }
        // Controls Enable
        private bool _CutStart_IsEnabled = true;
        public bool CutStart_IsEnabled
        {
            get { return _CutStart_IsEnabled; }
            set
            {
                if (_CutStart_IsEnabled == value)
                {
                    return;
                }

                _CutStart_IsEnabled = value;
                OnPropertyChanged("CutStart_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Cut End
        // --------------------------------------------------
        // Text
        private string _CutEnd_Text = "00:00:00.000";
        public string CutEnd_Text
        {
            get { return _CutEnd_Text; }
            set
            {
                if (_CutEnd_Text == value)
                {
                    return;
                }

                _CutEnd_Text = value;
                OnPropertyChanged("CutEnd_Text");
            }
        }
        // Controls Enable
        private bool _CutEnd_IsEnabled = true;
        public bool CutEnd_IsEnabled
        {
            get { return _CutEnd_IsEnabled; }
            set
            {
                if (_CutEnd_IsEnabled == value)
                {
                    return;
                }

                _CutEnd_IsEnabled = value;
                OnPropertyChanged("CutEnd_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Frame Start
        // --------------------------------------------------
        // Text
        private string _FrameStart_Text = "Frame";
        public string FrameStart_Text
        {
            get { return _FrameStart_Text; }
            set
            {
                if (_FrameStart_Text == value)
                {
                    return;
                }

                _FrameStart_Text = value;
                OnPropertyChanged("FrameStart_Text");
            }
        }
        // Controls Enable
        private bool _FrameStart_IsEnabled = true;
        public bool FrameStart_IsEnabled
        {
            get { return _FrameStart_IsEnabled; }
            set
            {
                if (_FrameStart_IsEnabled == value)
                {
                    return;
                }

                _FrameStart_IsEnabled = value;
                OnPropertyChanged("FrameStart_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Frame End
        // --------------------------------------------------
        // Text
        private string _FrameEnd_Text = "Range";
        public string FrameEnd_Text
        {
            get { return _FrameEnd_Text; }
            set
            {
                if (_FrameEnd_Text == value)
                {
                    return;
                }

                _FrameEnd_Text = value;
                OnPropertyChanged("FrameEnd_Text");
            }
        }
        // Controls Enable
        private bool _FrameEnd_IsEnabled = true;
        public bool FrameEnd_IsEnabled
        {
            get { return _FrameEnd_IsEnabled; }
            set
            {
                if (_FrameEnd_IsEnabled == value)
                {
                    return;
                }

                _FrameEnd_IsEnabled = value;
                OnPropertyChanged("FrameEnd_IsEnabled");
            }
        }



        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Video
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // --------------------------------------------------
        // Video Codec
        // --------------------------------------------------
        // Item Source
        private List<string> _VideoCodec_Items = new List<string>();
        public List<string> VideoCodec_Items
        {
            get { return _VideoCodec_Items; }
            set
            {
                _VideoCodec_Items = value;
                OnPropertyChanged("VideoCodec_Items");
            }
        }

        // Command
        public string VideoCodec_Command;

        // Selected Index
        private int _VideoCodec_SelectedIndex { get; set; }
        public int VideoCodec_SelectedIndex
        {
            get { return _VideoCodec_SelectedIndex; }
            set
            {
                if (_VideoCodec_SelectedIndex == value)
                {
                    return;
                }

                _VideoCodec_SelectedIndex = value;
                OnPropertyChanged("VideoCodec_SelectedIndex");
            }
        }

        // Selected Item
        private string _VideoCodec_SelectedItem { get; set; }
        public string VideoCodec_SelectedItem
        {
            get { return _VideoCodec_SelectedItem; }
            set
            {
                if (_VideoCodec_SelectedItem == value)
                {
                    return;
                }

                _VideoCodec_SelectedItem = value;
                OnPropertyChanged("VideoCodec_SelectedItem");
            }
        }

        // Controls Enable
        private bool _VideoCodec_IsEnabled = true;
        public bool VideoCodec_IsEnabled
        {
            get { return _VideoCodec_IsEnabled; }
            set
            {
                if (_VideoCodec_IsEnabled == value)
                {
                    return;
                }

                _VideoCodec_IsEnabled = value;
                OnPropertyChanged("VideoCodec_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Pass
        // --------------------------------------------------
        // Item Source
        private List<string> _Pass_Items = new List<string>()
        {
            "CRF",
            "1 Pass",
            "2 Pass"
        };
        public List<string> Pass_Items
        {
            get { return _Pass_Items; }
            set
            {
                _Pass_Items = value;
                OnPropertyChanged("Pass_Items");
            }
        }

        // Selected Index
        private int _Pass_SelectedIndex { get; set; }
        public int Pass_SelectedIndex
        {
            get { return _Pass_SelectedIndex; }
            set
            {
                if (_Pass_SelectedIndex == value)
                {
                    return;
                }

                _Pass_SelectedIndex = value;
                OnPropertyChanged("Pass_SelectedIndex");
            }
        }

        // Selected Item
        private string _Pass_SelectedItem { get; set; }
        public string Pass_SelectedItem
        {
            get { return _Pass_SelectedItem; }
            set
            {
                if (_Pass_SelectedItem == value)
                {
                    return;
                }

                _Pass_SelectedItem = value;
                OnPropertyChanged("Pass_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Pass_IsEnabled = true;
        public bool Pass_IsEnabled
        {
            get { return _Pass_IsEnabled; }
            set
            {
                if (_Pass_IsEnabled == value)
                {
                    return;
                }

                _Pass_IsEnabled = value;
                OnPropertyChanged("Pass_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Video Quality
        // --------------------------------------------------
        public class VideoQuality
        {
            public string Name { get; set; }
            public string NA { get; set; }
            public string Lossless { get; set; }
            public string CRF { get; set; }
            public string CRF_Bitrate { get; set; }
            public string CBR_BitMode { get; set; }
            public string CBR { get; set; }
            public string VBR_BitMode { get; set; }
            public string VBR { get; set; }
            public string Minrate { get; set; }
            public string Maxrate { get; set; }
            public string Bufsize { get; set; }
            //public string Custom { get; set; }
        }
        private List<VideoQuality> _VideoQuality_Items = new List<VideoQuality>();
        public List<VideoQuality> VideoQuality_Items
        {
            get { return _VideoQuality_Items; }
            set
            {
                _VideoQuality_Items = value;
                OnPropertyChanged("VideoQuality_Items");
            }
        }

        // Command
        //public string VideoBitrateMode_Command;

        // Selected Index
        private int _VideoQuality_SelectedIndex { get; set; }
        public int VideoQuality_SelectedIndex
        {
            get { return _VideoQuality_SelectedIndex; }
            set
            {
                if (_VideoQuality_SelectedIndex == value)
                {
                    return;
                }

                _VideoQuality_SelectedIndex = value;
                OnPropertyChanged("VideoQuality_SelectedIndex");
            }
        }

        // Selected Item
        private string _VideoQuality_SelectedItem { get; set; }
        public string VideoQuality_SelectedItem
        {
            get { return _VideoQuality_SelectedItem; }
            set
            {
                var previousItem = _VideoQuality_SelectedItem;
                _VideoQuality_SelectedItem = value;
                OnPropertyChanged("VideoQuality_SelectedItem");


                //if (!string.IsNullOrEmpty(VideoQuality_SelectedItem) &&
                //    VideoQuality_SelectedItem != "None")
                //{
                //    MainWindow.VideoQuality_PreviousItem = previousItem;
                //}

                if (previousItem != value)
                {
                    VideoControls.AutoCopyVideoCodec(/*/*System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault(), */this);
                    SubtitleControls.AutoCopySubtitleCodec(this);
                }

                //if (_VideoQuality_SelectedItem == value)
                //{
                //    return;
                //}

                //_VideoQuality_SelectedItem = value;
                //OnPropertyChanged("VideoQuality_SelectedItem");
            }
        }

        // Controls Enable
        private bool _VideoQuality_IsEnabled = true;
        public bool VideoQuality_IsEnabled
        {
            get { return _VideoQuality_IsEnabled; }
            set
            {
                if (_VideoQuality_IsEnabled == value)
                {
                    return;
                }

                _VideoQuality_IsEnabled = value;
                OnPropertyChanged("VideoQuality_IsEnabled");
            }
        }


        // -------------------------
        // Video VBR - Toggle
        // -------------------------
        // Checked
        private bool _VideoVBR_IsChecked;
        public bool VideoVBR_IsChecked
        {
            get { return _VideoVBR_IsChecked; }
            set
            {
                if (_VideoVBR_IsChecked != value)
                {
                    _VideoVBR_IsChecked = value;
                    OnPropertyChanged("VideoVBR_IsChecked");
                }
            }
        }
        //private bool _VideoVBR_IsChecked;
        //public bool VideoVBR_IsChecked
        //{
        //    get { return _VideoVBR_IsChecked; }
        //    set
        //    {
        //        if (_VideoVBR_IsChecked == value) return;

        //        _VideoVBR_IsChecked = value;
        //    }
        //}
        // Enabled
        private bool _VideoVBR_IsEnabled = true;
        public bool VideoVBR_IsEnabled
        {
            get { return _VideoVBR_IsEnabled; }
            set
            {
                if (_VideoVBR_IsEnabled == value)
                {
                    return;
                }

                _VideoVBR_IsEnabled = value;
                OnPropertyChanged("VideoVBR_IsEnabled");
            }
        }


        // -------------------------
        // CRF
        // -------------------------
        // Text
        private string _CRF_Text;
        public string CRF_Text
        {
            get { return _CRF_Text; }
            set
            {
                if (_CRF_Text == value)
                {
                    return;
                }

                _CRF_Text = value;
                OnPropertyChanged("CRF_Text");
            }
        }
        // Enabled
        private bool _CRF_IsEnabled = true;
        public bool CRF_IsEnabled
        {
            get { return _CRF_IsEnabled; }
            set
            {
                if (_CRF_IsEnabled == value)
                {
                    return;
                }

                _CRF_IsEnabled = value;
                OnPropertyChanged("CRF_IsEnabled");
            }
        }


        // -------------------------
        // Video Bitrate
        // -------------------------
        // Text
        private string _VideoBitrate_Text;
        public string VideoBitrate_Text
        {
            get { return _VideoBitrate_Text; }
            set
            {
                if (_VideoBitrate_Text == value)
                {
                    return;
                }

                _VideoBitrate_Text = value;
                OnPropertyChanged("VideoBitrate_Text");
            }
        }
        // Enabled
        private bool _VideoBitrate_IsEnabled = true;
        public bool VideoBitrate_IsEnabled
        {
            get { return _VideoBitrate_IsEnabled; }
            set
            {
                if (_VideoBitrate_IsEnabled == value)
                {
                    return;
                }

                _VideoBitrate_IsEnabled = value;
                OnPropertyChanged("VideoBitrate_IsEnabled");
            }
        }


        // -------------------------
        // Video Minrate
        // -------------------------
        // Text
        private string _VideoMinrate_Text;
        public string VideoMinrate_Text
        {
            get { return _VideoMinrate_Text; }
            set
            {
                if (_VideoMinrate_Text == value)
                {
                    return;
                }

                _VideoMinrate_Text = value;
                OnPropertyChanged("VideoMinrate_Text");
            }
        }
        // Enabled
        private bool _VideoMinrate_IsEnabled = true;
        public bool VideoMinrate_IsEnabled
        {
            get { return _VideoMinrate_IsEnabled; }
            set
            {
                if (_VideoMinrate_IsEnabled == value)
                {
                    return;
                }

                _VideoMinrate_IsEnabled = value;
                OnPropertyChanged("VideoMinrate_IsEnabled");
            }
        }

        // -------------------------
        // Video Maxrate
        // -------------------------
        // Text
        private string _VideoMaxrate_Text;
        public string VideoMaxrate_Text
        {
            get { return _VideoMaxrate_Text; }
            set
            {
                if (_VideoMaxrate_Text == value)
                {
                    return;
                }

                _VideoMaxrate_Text = value;
                OnPropertyChanged("VideoMaxrate_Text");
            }
        }
        // Enabled
        private bool _VideoMaxrate_IsEnabled = true;
        public bool VideoMaxrate_IsEnabled
        {
            get { return _VideoMaxrate_IsEnabled; }
            set
            {
                if (_VideoMaxrate_IsEnabled == value)
                {
                    return;
                }

                _VideoMaxrate_IsEnabled = value;
                OnPropertyChanged("VideoMaxrate_IsEnabled");
            }
        }


        // -------------------------
        // Video Bufsize
        // -------------------------
        // Text
        private string _VideoBufsize_Text;
        public string VideoBufsize_Text
        {
            get { return _VideoBufsize_Text; }
            set
            {
                if (_VideoBufsize_Text == value)
                {
                    return;
                }

                _VideoBufsize_Text = value;
                OnPropertyChanged("VideoBufsize_Text");
            }
        }
        // Enabled
        private bool _VideoBufsize_IsEnabled = true;
        public bool VideoBufsize_IsEnabled
        {
            get { return _VideoBufsize_IsEnabled; }
            set
            {
                if (_VideoBufsize_IsEnabled == value)
                {
                    return;
                }

                _VideoBufsize_IsEnabled = value;
                OnPropertyChanged("VideoBufsize_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Pixel Format
        // --------------------------------------------------
        // Item Source
        private List<string> _PixelFormat_Items = new List<string>()
        {
            "auto",
            "gray",
            "bgra",
            "rgb24",
            "yuv420p",
            "yuv422p",
            "yuv440p",
            "yuv444p",
            "yuv420p10be",
            "yuv422p10be",
            "yuv444p10be",
            "yuv420p10le",
            "yuv422p10le",
            "yuv444p10le",
            "yuva420p",
            "yuva422p",
            "yuva444p",
            "yuva420p10be",
            "yuva422p10be",
            "yuva440p10be",
            "yuva444p10be",
            "yuva420p10le",
            "yuva422p10le",
            "yuva440p10le",
            "yuva444p10le",
            "yuv420p12be",
            "yuv422p12be",
            "yuv444p12be",
            "yuv420p12le",
            "yuv422p12le",
            "yuv444p12le",
            "yuvj420p",
            "yuvj422p",
            "yuvj440p",
            "yuvj444p"
        };
        public List<string> PixelFormat_Items
        {
            get { return _PixelFormat_Items; }
            set
            {
                _PixelFormat_Items = value;
                OnPropertyChanged("PixelFormat_Items");
            }
        }

        // Selected Index
        private int _PixelFormat_SelectedIndex { get; set; }
        public int PixelFormat_SelectedIndex
        {
            get { return _PixelFormat_SelectedIndex; }
            set
            {
                if (_PixelFormat_SelectedIndex == value)
                {
                    return;
                }

                _PixelFormat_SelectedIndex = value;
                OnPropertyChanged("PixelFormat_SelectedIndex");
            }
        }

        // Selected Item
        private string _PixelFormat_SelectedItem { get; set; }
        public string PixelFormat_SelectedItem
        {
            get { return _PixelFormat_SelectedItem; }
            set
            {
                var previousItem = _PixelFormat_SelectedItem;
                _PixelFormat_SelectedItem = value;
                OnPropertyChanged("PixelFormat_SelectedItem");

                if (previousItem != value)
                {
                    //VideoControls.AutoCopyVideoCodec(/*System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault(), */this); // Crash Problem
                    SubtitleControls.AutoCopySubtitleCodec(this);
                }
                //if (_PixelFormat_SelectedItem == value)
                //{
                //    return;
                //}

                //_PixelFormat_SelectedItem = value;
                //OnPropertyChanged("PixelFormat_SelectedItem");
            }
        }

        // Controls Enable
        private bool _PixelFormat_IsEnabled = true;
        public bool PixelFormat_IsEnabled
        {
            get { return _PixelFormat_IsEnabled; }
            set
            {
                if (_PixelFormat_IsEnabled == value)
                {
                    return;
                }

                _PixelFormat_IsEnabled = value;
                OnPropertyChanged("PixelFormat_IsEnabled");
            }
        }


        // --------------------------------------------------
        // FPS
        // --------------------------------------------------
        // Item Source
        private List<string> _FPS_Items = new List<string>()
        {
            "auto",
            "ntsc",
            "pal",
            "film",
            "23.976",
            "24",
            "25",
            "29.97",
            "30",
            "48",
            "50",
            "59.94",
            "60",
            "Custom"
        };
        public List<string> FPS_Items
        {
            get { return _FPS_Items; }
            set
            {
                _FPS_Items = value;
                OnPropertyChanged("FPS_Items");
            }
        }

        // Selected Index
        private int _FPS_SelectedIndex { get; set; }
        public int FPS_SelectedIndex
        {
            get { return _FPS_SelectedIndex; }
            set
            {
                if (_FPS_SelectedIndex == value)
                {
                    return;
                }

                _FPS_SelectedIndex = value;
                OnPropertyChanged("FPS_SelectedIndex");
            }
        }

        // Selected Item
        private string _FPS_SelectedItem { get; set; }
        public string FPS_SelectedItem
        {
            get { return _FPS_SelectedItem; }
            set
            {
                var previousItem = _FPS_SelectedItem;
                _FPS_SelectedItem = value;
                OnPropertyChanged("FPS_SelectedItem");

                if (previousItem != value)
                {
                    VideoControls.AutoCopyVideoCodec(/*System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault(), */this);
                    SubtitleControls.AutoCopySubtitleCodec(this);
                }

                //if (_FPS_SelectedItem == value)
                //{
                //    return;
                //}

                //_FPS_SelectedItem = value;
                //OnPropertyChanged("FPS_SelectedItem");
            }
        }

        // Text
        private string _FPS_Text;
        public string FPS_Text
        {
            get { return _FPS_Text; }
            set
            {
                if (_FPS_Text == value)
                {
                    return;
                }

                _FPS_Text = value;
                OnPropertyChanged("FPS_Text");
            }
        }

        // Controls Enable
        private bool _FPS_IsEnabled = true;
        public bool FPS_IsEnabled
        {
            get { return _FPS_IsEnabled; }
            set
            {
                if (_FPS_IsEnabled == value)
                {
                    return;
                }

                _FPS_IsEnabled = value;
                OnPropertyChanged("FPS_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Optimize
        // --------------------------------------------------
        public class VideoOptimize
        {
            public string Name { get; set; }
            public string Command { get; set; }
            public string Tune { get; set; }
            public string Profile { get; set; }
            public string Level { get; set; }
        }
        private List<VideoOptimize> _Video_Optimize_Items = new List<VideoOptimize>();
        // Item Source
        //private List<string> _Optimize_Items = new List<string>();
        public List<VideoOptimize> Video_Optimize_Items
        {
            get { return _Video_Optimize_Items; }
            set
            {
                _Video_Optimize_Items = value;
                OnPropertyChanged("Video_Optimize_Items");
            }
        }

        // Selected Index
        private int _Video_Optimize_SelectedIndex { get; set; }
        public int Video_Optimize_SelectedIndex
        {
            get { return _Video_Optimize_SelectedIndex; }
            set
            {
                if (_Video_Optimize_SelectedIndex == value)
                {
                    return;
                }

                _Video_Optimize_SelectedIndex = value;
                OnPropertyChanged("Video_Optimize_SelectedIndex");
            }
        }

        // Selected Item
        private string _Video_Optimize_SelectedItem { get; set; }
        public string Video_Optimize_SelectedItem
        {
            get { return _Video_Optimize_SelectedItem; }
            set
            {
                var previousItem = _Video_Optimize_SelectedItem;
                _Video_Optimize_SelectedItem = value;
                OnPropertyChanged("Video_Optimize_SelectedItem");

                if (previousItem != value)
                {
                    VideoControls.AutoCopyVideoCodec(/*System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault(), */this);
                    SubtitleControls.AutoCopySubtitleCodec(this);
                }

                //if (_Video_Optimize_SelectedItem == value)
                //{
                //    return;
                //}

                //_Video_Optimize_SelectedItem = value;
                //OnPropertyChanged("Video_Optimize_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Video_Optimize_IsEnabled = true;
        public bool Video_Optimize_IsEnabled
        {
            get { return _Video_Optimize_IsEnabled; }
            set
            {
                if (_Video_Optimize_IsEnabled == value)
                {
                    return;
                }

                _Video_Optimize_IsEnabled = value;
                OnPropertyChanged("Video_Optimize_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Optimize Tune
        // --------------------------------------------------
        // Item Source
        private List<string> _Optimize_Tune_Items = new List<string>();
        public List<string> Optimize_Tune_Items
        {
            get { return _Optimize_Tune_Items; }
            set
            {
                _Optimize_Tune_Items = value;
                OnPropertyChanged("Optimize_Tune_Items");
            }
        }

        // Selected Index
        private int _Optimize_Tune_SelectedIndex { get; set; }
        public int Optimize_Tune_SelectedIndex
        {
            get { return _Optimize_Tune_SelectedIndex; }
            set
            {
                if (_Optimize_Tune_SelectedIndex == value)
                {
                    return;
                }

                _Optimize_Tune_SelectedIndex = value;
                OnPropertyChanged("Optimize_Tune_SelectedIndex");
            }
        }

        // Selected Item
        public string _Optimize_Tune_SelectedItem { get; set; }
        public string Optimize_Tune_SelectedItem
        {
            get { return _Optimize_Tune_SelectedItem; }
            set
            {
                if (_Optimize_Tune_SelectedItem == value)
                {
                    return;
                }

                _Optimize_Tune_SelectedItem = value;
                OnPropertyChanged("Optimize_Tune_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Optimize_Tune_IsEnabled = true;
        public bool Optimize_Tune_IsEnabled
        {
            get { return _Optimize_Tune_IsEnabled; }
            set
            {
                if (_Optimize_Tune_IsEnabled == value)
                {
                    return;
                }

                _Optimize_Tune_IsEnabled = value;
                OnPropertyChanged("Optimize_Tune_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Optimize Profile
        // --------------------------------------------------
        // Item Source
        private List<string> _Optimize_Profile_Items = new List<string>();
        public List<string> Optimize_Profile_Items
        {
            get { return _Optimize_Profile_Items; }
            set
            {
                _Optimize_Profile_Items = value;
                OnPropertyChanged("Optimize_Profile_Items");
            }
        }

        // Selected Index
        private int _Optimize_Profile_SelectedIndex { get; set; }
        public int Optimize_Profile_SelectedIndex
        {
            get { return _Optimize_Profile_SelectedIndex; }
            set
            {
                if (_Optimize_Profile_SelectedIndex == value)
                {
                    return;
                }

                _Optimize_Profile_SelectedIndex = value;
                OnPropertyChanged("Optimize_Profile_SelectedIndex");
            }
        }

        // Selected Item
        public string _Optimize_Profile_SelectedItem { get; set; }
        public string Optimize_Profile_SelectedItem
        {
            get { return _Optimize_Profile_SelectedItem; }
            set
            {
                if (_Optimize_Profile_SelectedItem == value)
                {
                    return;
                }

                _Optimize_Profile_SelectedItem = value;
                OnPropertyChanged("Optimize_Profile_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Optimize_Profile_IsEnabled = true;
        public bool Optimize_Profile_IsEnabled
        {
            get { return _Optimize_Profile_IsEnabled; }
            set
            {
                if (_Optimize_Profile_IsEnabled == value)
                {
                    return;
                }

                _Optimize_Profile_IsEnabled = value;
                OnPropertyChanged("Optimize_Profile_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Optimize Level
        // --------------------------------------------------
        // Item Source
        private List<string> _Optimize_Level_Items = new List<string>();
        public List<string> Optimize_Level_Items
        {
            get { return _Optimize_Level_Items; }
            set
            {
                _Optimize_Level_Items = value;
                OnPropertyChanged("Optimize_Level_Items");
            }
        }

        // Selected Index
        private int _Optimize_Level_SelectedIndex { get; set; }
        public int Optimize_Level_SelectedIndex
        {
            get { return _Optimize_Level_SelectedIndex; }
            set
            {
                if (_Optimize_Level_SelectedIndex == value)
                {
                    return;
                }

                _Optimize_Level_SelectedIndex = value;
                OnPropertyChanged("Optimize_Level_SelectedIndex");
            }
        }

        // Selected Item
        public string _Optimize_Level_SelectedItem { get; set; }
        public string Optimize_Level_SelectedItem
        {
            get { return _Optimize_Level_SelectedItem; }
            set
            {
                if (_Optimize_Level_SelectedItem == value)
                {
                    return;
                }

                _Optimize_Level_SelectedItem = value;
                OnPropertyChanged("Optimize_Level_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Optimize_Level_IsEnabled = true;
        public bool Optimize_Level_IsEnabled
        {
            get { return _Optimize_Level_IsEnabled; }
            set
            {
                if (_Optimize_Level_IsEnabled == value)
                {
                    return;
                }

                _Optimize_Level_IsEnabled = value;
                OnPropertyChanged("Optimize_Level_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Size
        // --------------------------------------------------
        // Item Source
        private List<string> _Size_Items = new List<string>()
        {
            "Source",
            "8K",
            "4K",
            "4K UHD",
            "2K",
            "1440p",
            "1200p",
            "1080p",
            "720p",
            "480p",
            "320p",
            "240p",
            "Custom"
        };
        public List<string> Size_Items
        {
            get { return _Size_Items; }
            set
            {
                _Size_Items = value;
                OnPropertyChanged("Size_Items");
            }
        }

        // Selected Index
        private int _Size_SelectedIndex { get; set; }
        public int Size_SelectedIndex
        {
            get { return _Size_SelectedIndex; }
            set
            {
                if (_Size_SelectedIndex == value)
                {
                    return;
                }

                _Size_SelectedIndex = value;
                OnPropertyChanged("Size_SelectedIndex");
            }
        }

        // Selected Item
        private string _Size_SelectedItem { get; set; }
        public string Size_SelectedItem
        {
            get { return _Size_SelectedItem; }
            set
            {
                var previousItem = _Size_SelectedItem;
                _Size_SelectedItem = value;
                OnPropertyChanged("Size_SelectedItem");

                if (previousItem != value)
                {
                    VideoControls.AutoCopyVideoCodec(/*System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault(), */this);
                    SubtitleControls.AutoCopySubtitleCodec(this);
                }

                //if (_Size_SelectedItem == value)
                //{
                //    return;
                //}

                //_Size_SelectedItem = value;
                //OnPropertyChanged("Size_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Size_IsEnabled = true;
        public bool Size_IsEnabled
        {
            get { return _Size_IsEnabled; }
            set
            {
                if (_Size_IsEnabled == value)
                {
                    return;
                }

                _Size_IsEnabled = value;
                OnPropertyChanged("Size_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Width
        // --------------------------------------------------
        // Text
        private string _Width_Text;
        public string Width_Text
        {
            get { return _Width_Text; }
            set
            {
                if (_Width_Text == value)
                {
                    return;
                }

                _Width_Text = value;
                OnPropertyChanged("Width_Text");
            }
        }
        // Enabled
        private bool _Width_IsEnabled = true;
        public bool Width_IsEnabled
        {
            get { return _Width_IsEnabled; }
            set
            {
                if (_Width_IsEnabled == value)
                {
                    return;
                }

                _Width_IsEnabled = value;
                OnPropertyChanged("Width_IsEnabled");
            }
        }
        // --------------------------------------------------
        // Height
        // --------------------------------------------------
        // Text
        private string _Height_Text;
        public string Height_Text
        {
            get { return _Height_Text; }
            set
            {
                if (_Height_Text == value)
                {
                    return;
                }

                _Height_Text = value;
                OnPropertyChanged("Height_Text");
            }
        }
        // Enabled
        private bool _Height_IsEnabled = true;
        public bool Height_IsEnabled
        {
            get { return _Height_IsEnabled; }
            set
            {
                if (_Height_IsEnabled == value)
                {
                    return;
                }

                _Height_IsEnabled = value;
                OnPropertyChanged("Height_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Scaling
        // --------------------------------------------------
        // Item Source
        private List<string> _Scaling_Items = new List<string>()
        {
            "default",
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
            "spline"
        };
        public List<string> Scaling_Items
        {
            get { return _Scaling_Items; }
            set
            {
                _Scaling_Items = value;
                OnPropertyChanged("Scaling_Items");
            }
        }

        // Selected Index
        private int _Scaling_SelectedIndex { get; set; }
        public int Scaling_SelectedIndex
        {
            get { return _Scaling_SelectedIndex; }
            set
            {
                if (_Scaling_SelectedIndex == value)
                {
                    return;
                }

                _Scaling_SelectedIndex = value;
                OnPropertyChanged("Scaling_SelectedIndex");
            }
        }

        // Selected Item
        private string _Scaling_SelectedItem { get; set; }
        public string Scaling_SelectedItem
        {
            get { return _Scaling_SelectedItem; }
            set
            {
                var previousItem = _Scaling_SelectedItem;
                _Scaling_SelectedItem = value;
                OnPropertyChanged("Scaling_SelectedItem");

                if (previousItem != value)
                {
                    VideoControls.AutoCopyVideoCodec(/*System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault(), */this);
                    SubtitleControls.AutoCopySubtitleCodec(this);
                }

                //if (_Scaling_SelectedItem == value)
                //{
                //    return;
                //}

                //_Scaling_SelectedItem = value;
                //OnPropertyChanged("Scaling_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Scaling_IsEnabled = true;
        public bool Scaling_IsEnabled
        {
            get { return _Scaling_IsEnabled; }
            set
            {
                if (_Scaling_IsEnabled == value)
                {
                    return;
                }

                _Scaling_IsEnabled = value;
                OnPropertyChanged("Scaling_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Crop
        // --------------------------------------------------
        // Controls Enable
        private bool _Crop_IsEnabled = true;
        public bool Crop_IsEnabled
        {
            get { return _Crop_IsEnabled; }
            set
            {
                if (_Crop_IsEnabled == value)
                {
                    return;
                }

                _Crop_IsEnabled = value;
                OnPropertyChanged("Crop_IsEnabled");
            }
        }

        // -------------------------
        // Crop Width
        // -------------------------
        // Text
        private string _CropWidth_Text;
        public string CropWidth_Text
        {
            get { return _CropWidth_Text; }
            set
            {
                if (_CropWidth_Text == value)
                {
                    return;
                }

                _CropWidth_Text = value;
                OnPropertyChanged("CropWidth_Text");
            }
        }

        // -------------------------
        // Crop Height
        // -------------------------
        // Text
        private string _CropHeight_Text;
        public string CropHeight_Text
        {
            get { return _CropHeight_Text; }
            set
            {
                if (_CropHeight_Text == value)
                {
                    return;
                }

                _CropHeight_Text = value;
                OnPropertyChanged("CropHeight_Text");
            }
        }

        // -------------------------
        // Crop X
        // -------------------------
        // Text
        private string _CropX_Text;
        public string CropX_Text
        {
            get { return _CropX_Text; }
            set
            {
                if (_CropX_Text == value)
                {
                    return;
                }

                _CropX_Text = value;
                OnPropertyChanged("CropX_Text");
            }
        }

        // -------------------------
        // Crop X
        // -------------------------
        private string _CropY_Text;
        public string CropY_Text
        {
            get { return _CropY_Text; }
            set
            {
                if (_CropY_Text == value)
                {
                    return;
                }

                _CropY_Text = value;
                OnPropertyChanged("CropY_Text");
            }
        }



        // --------------------------------------------------
        // Crop Clear Button
        // --------------------------------------------------
        // Text
        private string _CropClear_Text = "Clear";
        public string CropClear_Text
        {
            get { return _CropClear_Text; }
            set
            {
                if (_CropClear_Text == value)
                {
                    return;
                }

                _CropClear_Text = value;
                OnPropertyChanged("CropClear_Text");
            }
        }




        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Audio
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // --------------------------------------------------
        // Audio Codec
        // --------------------------------------------------
        // Item Source
        private List<string> _AudioCodec_Items = new List<string>();
        public List<string> AudioCodec_Items
        {
            get { return _AudioCodec_Items; }
            set
            {
                _AudioCodec_Items = value;
                OnPropertyChanged("AudioCodec_Items");
            }
        }

        // Command
        public string AudioCodec_Command;

        // Selected Index
        private int _AudioCodec_SelectedIndex { get; set; }
        public int AudioCodec_SelectedIndex
        {
            get { return _AudioCodec_SelectedIndex; }
            set
            {
                if (_AudioCodec_SelectedIndex == value)
                {
                    return;
                }

                _AudioCodec_SelectedIndex = value;
                OnPropertyChanged("AudioCodec_SelectedIndex");
            }
        }

        // Selected Item
        private string _AudioCodec_SelectedItem { get; set; }
        public string AudioCodec_SelectedItem
        {
            get { return _AudioCodec_SelectedItem; }
            set
            {
                if (_AudioCodec_SelectedItem == value)
                {
                    return;
                }

                _AudioCodec_SelectedItem = value;
                OnPropertyChanged("AudioCodec_SelectedItem");
            }
        }

        // Controls Enable
        private bool _AudioCodec_IsEnabled = true;
        public bool AudioCodec_IsEnabled
        {
            get { return _AudioCodec_IsEnabled; }
            set
            {
                if (_AudioCodec_IsEnabled == value)
                {
                    return;
                }

                _AudioCodec_IsEnabled = value;
                OnPropertyChanged("AudioCodec_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Audio Stream
        // --------------------------------------------------
        // Item Source
        private List<string> _AudioStream_Items = new List<string>()
        {
            "none",
            "all",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
        };
        public List<string> AudioStream_Items
        {
            get { return _AudioStream_Items; }
            set
            {
                _AudioStream_Items = value;
                OnPropertyChanged("AudioStream_Items");
            }
        }

        // Selected Index
        private int _AudioStream_SelectedIndex { get; set; }
        public int AudioStream_SelectedIndex
        {
            get { return _AudioStream_SelectedIndex; }
            set
            {
                if (_AudioStream_SelectedIndex == value)
                {
                    return;
                }

                _AudioStream_SelectedIndex = value;
                OnPropertyChanged("AudioStream_SelectedIndex");
            }
        }

        // Selected Item
        private string _AudioStream_SelectedItem { get; set; }
        public string AudioStream_SelectedItem
        {
            get { return _AudioStream_SelectedItem; }
            set
            {
                if (_AudioStream_SelectedItem == value)
                {
                    return;
                }

                _AudioStream_SelectedItem = value;
                OnPropertyChanged("AudioStream_SelectedItem");
            }
        }

        // Controls Enable
        private bool _AudioStream_IsEnabled = true;
        public bool AudioStream_IsEnabled
        {
            get { return _AudioStream_IsEnabled; }
            set
            {
                if (_AudioStream_IsEnabled == value)
                {
                    return;
                }

                _AudioStream_IsEnabled = value;
                OnPropertyChanged("AudioStream_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Audio Channel
        // --------------------------------------------------
        // Item Source
        private List<string> _AudioChannel_Items = new List<string>();
        public List<string> AudioChannel_Items
        {
            get { return _AudioChannel_Items; }
            set
            {
                _AudioChannel_Items = value;
                OnPropertyChanged("AudioChannel_Items");
            }
        }

        // Selected Index
        private int _AudioChannel_SelectedIndex { get; set; }
        public int AudioChannel_SelectedIndex
        {
            get { return _AudioChannel_SelectedIndex; }
            set
            {
                if (_AudioChannel_SelectedIndex == value)
                {
                    return;
                }

                _AudioChannel_SelectedIndex = value;
                OnPropertyChanged("AudioChannel_SelectedIndex");
            }
        }

        // Selected Item
        private string _AudioChannel_SelectedItem { get; set; }
        public string AudioChannel_SelectedItem
        {
            get { return _AudioChannel_SelectedItem; }
            set
            {
                var previousItem = _AudioChannel_SelectedItem;
                _AudioChannel_SelectedItem = value;
                OnPropertyChanged("AudioChannel_SelectedItem");

                if (previousItem != value)
                {
                    AudioControls.AutoCopyAudioCodec(/*System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault(), */this);
                }

                //if (_AudioChannel_SelectedItem == value)
                //{
                //    return;
                //}

                //_AudioChannel_SelectedItem = value;
                //OnPropertyChanged("AudioChannel_SelectedItem");
            }
        }

        // Controls Enable
        private bool _AudioChannel_IsEnabled = true;
        public bool AudioChannel_IsEnabled
        {
            get { return _AudioChannel_IsEnabled; }
            set
            {
                if (_AudioChannel_IsEnabled == value)
                {
                    return;
                }

                _AudioChannel_IsEnabled = value;
                OnPropertyChanged("AudioChannel_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Audio Quality
        // --------------------------------------------------
        // Item Source
        public class AudioQuality
        {
            public string Name { get; set; }
            public string NA { get; set; }
            //public string Auto { get; set; }
            public string Lossless { get; set; }
            public string CBR_BitMode { get; set; }
            public string CBR { get; set; }
            public string VBR_BitMode { get; set; }
            public string VBR { get; set; }
            //public string Custom { get; set; }
        }
        public List<AudioQuality> _AudioQuality_Items = new List<AudioQuality>();
        public List<AudioQuality> AudioQuality_Items
        {
            get { return _AudioQuality_Items; }
            set
            {
                _AudioQuality_Items = value;
                OnPropertyChanged("AudioQuality_Items");
            }
        }
        //private List<string> _AudioQuality_Items = new List<string>();
        //public List<string> AudioQuality_Items
        //{
        //    get { return _AudioQuality_Items; }
        //    set
        //    {
        //        _AudioQuality_Items = value;
        //        OnPropertyChanged("AudioQuality_Items");
        //    }
        //}

        // Selected Index
        private int _AudioQuality_SelectedIndex { get; set; }
        public int AudioQuality_SelectedIndex
        {
            get { return _AudioQuality_SelectedIndex; }
            set
            {
                if (_AudioQuality_SelectedIndex == value)
                {
                    return;
                }

                _AudioQuality_SelectedIndex = value;
                OnPropertyChanged("AudioQuality_SelectedIndex");
            }
        }

        // Selected Item
        public string _AudioQuality_SelectedItem { get; set; }
        public string AudioQuality_SelectedItem
        {
            get { return _AudioQuality_SelectedItem; }
            set
            {
                var previousItem = _AudioQuality_SelectedItem;
                _AudioQuality_SelectedItem = value;
                OnPropertyChanged("AudioQuality_SelectedItem");

                if (previousItem != value)
                {
                    AudioControls.AutoCopyAudioCodec(/*System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault(), */this);
                }

                //if (_AudioQuality_SelectedItem == value)
                //{
                //    return;
                //}

                //_AudioQuality_SelectedItem = value;
                //OnPropertyChanged("AudioQuality_SelectedItem");
            }
        }

        // Controls Enable
        private bool _AudioQuality_IsEnabled = true;
        public bool AudioQuality_IsEnabled
        {
            get { return _AudioQuality_IsEnabled; }
            set
            {
                if (_AudioQuality_IsEnabled == value)
                {
                    return;
                }

                _AudioQuality_IsEnabled = value;
                OnPropertyChanged("AudioQuality_IsEnabled");
            }
        }


        // -------------------------
        // Audio Bitrate
        // -------------------------
        // Text
        private string _AudioBitrate_Text;
        public string AudioBitrate_Text
        {
            get { return _AudioBitrate_Text; }
            set
            {
                if (_AudioBitrate_Text == value)
                {
                    return;
                }

                _AudioBitrate_Text = value;
                OnPropertyChanged("AudioBitrate_Text");
            }
        }
        // Enabled
        private bool _AudioBitrate_IsEnabled = true;
        public bool AudioBitrate_IsEnabled
        {
            get { return _AudioBitrate_IsEnabled; }
            set
            {
                if (_AudioBitrate_IsEnabled == value)
                {
                    return;
                }

                _AudioBitrate_IsEnabled = value;
                OnPropertyChanged("AudioBitrate_IsEnabled");
            }
        }


        // -------------------------
        // Audio VBR - Toggle
        // -------------------------
        // Checked
        private bool _AudioVBR_IsChecked;
        public bool AudioVBR_IsChecked
        {
            get { return _AudioVBR_IsChecked; }
            set
            {
                if (_AudioVBR_IsChecked != value)
                {
                    _AudioVBR_IsChecked = value;
                    OnPropertyChanged("AudioVBR_IsChecked");
                }
            }
        }
        //private bool _AudioVBR_IsChecked;
        //public bool AudioVBR_IsChecked
        //{
        //    get { return _AudioVBR_IsChecked; }
        //    set
        //    {
        //        if (_AudioVBR_IsChecked == value) return;

        //        _AudioVBR_IsChecked = value;
        //    }
        //}
        // Enabled
        private bool _AudioVBR_IsEnabled = true;
        public bool AudioVBR_IsEnabled
        {
            get { return _AudioVBR_IsEnabled; }
            set
            {
                if (_AudioVBR_IsEnabled == value)
                {
                    return;
                }

                _AudioVBR_IsEnabled = value;
                OnPropertyChanged("AudioVBR_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Audio Sample Rate
        // --------------------------------------------------
        // Item Source
        public class AudioSampleRate
        {
            public string Name { get; set; }
            public string Frequency { get; set; }
        }
        public List<AudioSampleRate> _AudioSampleRate_Items = new List<AudioSampleRate>();
        public List<AudioSampleRate> AudioSampleRate_Items
        {
            get { return _AudioSampleRate_Items; }
            set
            {
                _AudioSampleRate_Items = value;
                OnPropertyChanged("AudioSampleRate_Items");
            }
        }
        //private List<string> _AudioSampleRate_Items = new List<string>();
        //public List<string> AudioSampleRate_Items
        //{
        //    get { return _AudioSampleRate_Items; }
        //    set
        //    {
        //        _AudioSampleRate_Items = value;
        //        OnPropertyChanged("AudioSampleRate_Items");
        //    }
        //}

        // Selected Index
        private int _AudioSampleRate_SelectedIndex { get; set; }
        public int AudioSampleRate_SelectedIndex
        {
            get { return _AudioSampleRate_SelectedIndex; }
            set
            {
                if (_AudioSampleRate_SelectedIndex == value)
                {
                    return;
                }

                _AudioSampleRate_SelectedIndex = value;
                OnPropertyChanged("AudioSampleRate_SelectedIndex");
            }
        }

        // Selected Item
        private string _AudioSampleRate_SelectedItem { get; set; }
        public string AudioSampleRate_SelectedItem
        {
            get { return _AudioSampleRate_SelectedItem; }
            set
            {
                var previousItem = _AudioSampleRate_SelectedItem;
                _AudioSampleRate_SelectedItem = value;
                OnPropertyChanged("AudioSampleRate_SelectedItem");

                if (previousItem != value)
                {
                    AudioControls.AutoCopyAudioCodec(/*System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault(), */this);
                }

                //if (_AudioSampleRate_SelectedItem == value)
                //{
                //    return;
                //}

                //_AudioSampleRate_SelectedItem = value;
                //OnPropertyChanged("AudioSampleRate_SelectedItem");
            }
        }

        // Controls Enable
        private bool _AudioSampleRate_IsEnabled = true;
        public bool AudioSampleRate_IsEnabled
        {
            get { return _AudioSampleRate_IsEnabled; }
            set
            {
                if (_AudioSampleRate_IsEnabled == value)
                {
                    return;
                }

                _AudioSampleRate_IsEnabled = value;
                OnPropertyChanged("AudioSampleRate_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Audio Bit Depth
        // --------------------------------------------------
        // Item Source
        public class AudioBitDepth
        {
            public string Name { get; set; }
            public string Depth { get; set; }
        }
        public List<AudioBitDepth> _AudioBitDepth_Items = new List<AudioBitDepth>();
        public List<AudioBitDepth> AudioBitDepth_Items
        {
            get { return _AudioBitDepth_Items; }
            set
            {
                _AudioBitDepth_Items = value;
                OnPropertyChanged("AudioBitDepth_Items");
            }
        }

        //private List<string> _AudioBitDepth_Items = new List<string>();
        //public List<string> AudioBitDepth_Items
        //{
        //    get { return _AudioBitDepth_Items; }
        //    set
        //    {
        //        _AudioBitDepth_Items = value;
        //        OnPropertyChanged("AudioBitDepth_Items");
        //    }
        //}

        // Selected Index
        private int _AudioBitDepth_SelectedIndex { get; set; }
        public int AudioBitDepth_SelectedIndex
        {
            get { return _AudioBitDepth_SelectedIndex; }
            set
            {
                if (_AudioBitDepth_SelectedIndex == value)
                {
                    return;
                }

                _AudioBitDepth_SelectedIndex = value;
                OnPropertyChanged("AudioBitDepth_SelectedIndex");
            }
        }

        // Selected Item
        private string _AudioBitDepth_SelectedItem { get; set; }
        public string AudioBitDepth_SelectedItem
        {
            get { return _AudioBitDepth_SelectedItem; }
            set
            {
                var previousItem = _AudioBitDepth_SelectedItem;
                _AudioBitDepth_SelectedItem = value;
                OnPropertyChanged("AudioBitDepth_SelectedItem");

                if (previousItem != value)
                {
                    AudioControls.AutoCopyAudioCodec(/*System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault(), */this);
                }

                //if (_AudioBitDepth_SelectedItem == value)
                //{
                //    return;
                //}

                //_AudioBitDepth_SelectedItem = value;
                //OnPropertyChanged("AudioBitDepth_SelectedItem");
            }
        }

        // Controls Enable
        private bool _AudioBitDepth_IsEnabled = true;
        public bool AudioBitDepth_IsEnabled
        {
            get { return _AudioBitDepth_IsEnabled; }
            set
            {
                if (_AudioBitDepth_IsEnabled == value)
                {
                    return;
                }

                _AudioBitDepth_IsEnabled = value;
                OnPropertyChanged("AudioBitDepth_IsEnabled");
            }
        }


        // -------------------------
        // Volume
        // -------------------------
        // Text
        private string _Volume_Text = "100";
        public string Volume_Text
        {
            get { return _Volume_Text; }
            set
            {
                if (_Volume_Text == value)
                {
                    return;
                }

                _Volume_Text = value;
                OnPropertyChanged("Volume_Text");
            }
        }
        // Enabled
        private bool _Volume_IsEnabled = true;
        public bool Volume_IsEnabled
        {
            get { return _Volume_IsEnabled; }
            set
            {
                if (_Volume_IsEnabled == value)
                {
                    return;
                }

                _Volume_IsEnabled = value;
                OnPropertyChanged("Volume_IsEnabled");
            }
        }


        // -------------------------
        // Hard Limiter
        // -------------------------
        // Value
        private double _AudioHardLimiter_Value = 1;
        public double AudioHardLimiter_Value
        {
            get { return _AudioHardLimiter_Value; }
            set
            {
                if (_AudioHardLimiter_Value == value)
                {
                    return;
                }

                _AudioHardLimiter_Value = value;
                OnPropertyChanged("AudioHardLimiter_Value");
            }
        }

        // Enabled
        private bool _AudioHardLimiter_IsEnabled = true;
        public bool AudioHardLimiter_IsEnabled
        {
            get { return _AudioHardLimiter_IsEnabled; }
            set
            {
                if (_AudioHardLimiter_IsEnabled == value)
                {
                    return;
                }

                _AudioHardLimiter_IsEnabled = value;
                OnPropertyChanged("AudioHardLimiter_IsEnabled");
            }
        }


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Configure
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // -------------------------
        // FFmpeg Path
        // -------------------------
        // Text
        private string _FFmpegPath_Text;
        public string FFmpegPath_Text
        {
            get { return _FFmpegPath_Text; }
            set
            {
                if (_FFmpegPath_Text == value)
                {
                    return;
                }

                _FFmpegPath_Text = value;
                OnPropertyChanged("FFmpegPath_Text");
            }
        }
        // Enabled
        private bool _FFmpegPath_IsEnabled = true;
        public bool FFmpegPath_IsEnabled
        {
            get { return _FFmpegPath_IsEnabled; }
            set
            {
                if (_FFmpegPath_IsEnabled == value)
                {
                    return;
                }

                _FFmpegPath_IsEnabled = value;
                OnPropertyChanged("FFmpegPath_IsEnabled");
            }
        }

        // -------------------------
        // FFprobe Path
        // -------------------------
        // Text
        private string _FFprobePath_Text;
        public string FFprobePath_Text
        {
            get { return _FFprobePath_Text; }
            set
            {
                if (_FFprobePath_Text == value)
                {
                    return;
                }

                _FFprobePath_Text = value;
                OnPropertyChanged("FFprobePath_Text");
            }
        }
        // Enabled
        private bool _FFprobePath_IsEnabled = true;
        public bool FFprobePath_IsEnabled
        {
            get { return _FFprobePath_IsEnabled; }
            set
            {
                if (_FFprobePath_IsEnabled == value)
                {
                    return;
                }

                _FFprobePath_IsEnabled = value;
                OnPropertyChanged("FFprobePath_IsEnabled");
            }
        }

        // -------------------------
        // FFplay Path
        // -------------------------
        // Text
        private string _FFplayPath_Text;
        public string FFplayPath_Text
        {
            get { return _FFplayPath_Text; }
            set
            {
                if (_FFplayPath_Text == value)
                {
                    return;
                }

                _FFplayPath_Text = value;
                OnPropertyChanged("FFplayPath_Text");
            }
        }
        // Enabled
        private bool _FFplayPath_IsEnabled = true;
        public bool FFplayPath_IsEnabled
        {
            get { return _FFplayPath_IsEnabled; }
            set
            {
                if (_FFplayPath_IsEnabled == value)
                {
                    return;
                }

                _FFplayPath_IsEnabled = value;
                OnPropertyChanged("FFplayPath_IsEnabled");
            }
        }

        // -------------------------
        // Log Path
        // -------------------------
        // Text
        private string _LogPath_Text;
        public string LogPath_Text
        {
            get { return _LogPath_Text; }
            set
            {
                if (_LogPath_Text == value)
                {
                    return;
                }

                _LogPath_Text = value;
                OnPropertyChanged("LogPath_Text");
            }
        }
        // Enabled
        private bool _LogPath_IsEnabled = true;
        public bool LogPath_IsEnabled
        {
            get { return _LogPath_IsEnabled; }
            set
            {
                if (_LogPath_IsEnabled == value)
                {
                    return;
                }

                _LogPath_IsEnabled = value;
                OnPropertyChanged("LogPath_IsEnabled");
            }
        }

        // -------------------------
        // Log - CheckBox
        // -------------------------
        // Checked
        private bool _LogCheckBox_IsChecked;
        public bool LogCheckBox_IsChecked
        {
            get { return _LogCheckBox_IsChecked; }
            set
            {
                if (_LogCheckBox_IsChecked != value)
                {
                    _LogCheckBox_IsChecked = value;
                    OnPropertyChanged("LogCheckBox_IsChecked");
                }
            }
        }
        //private bool _LogCheckBox_IsChecked;
        //public bool LogCheckBox_IsChecked
        //{
        //    get { return _LogCheckBox_IsChecked; }
        //    set
        //    {
        //        if (_LogCheckBox_IsChecked == value) return;

        //        _LogCheckBox_IsChecked = value;
        //    }
        //}
        // Enabled
        private bool _LogCheckBox_IsEnabled = true;
        public bool LogCheckBox_IsEnabled
        {
            get { return _LogCheckBox_IsEnabled; }
            set
            {
                if (_LogCheckBox_IsEnabled == value)
                {
                    return;
                }

                _LogCheckBox_IsEnabled = value;
                OnPropertyChanged("LogCheckBox_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Threads
        // --------------------------------------------------
        // Item Source
        private List<string> _Threads_Items = new List<string>()
        {
           "default",
           "optimal",
           "all",
           "1",
           "2",
           "3",
           "4",
           "5",
           "6",
           "7",
           "8",
           "9",
           "10",
           "11",
           "12",
           "13",
           "14",
           "15",
           "16"
        };
        public List<string> Threads_Items
        {
            get { return _Threads_Items; }
            set
            {
                _Threads_Items = value;
                OnPropertyChanged("Threads_Items");
            }
        }

        // Selected Index
        private int _Threads_SelectedIndex { get; set; }
        public int Threads_SelectedIndex
        {
            get { return _Threads_SelectedIndex; }
            set
            {
                if (_Threads_SelectedIndex == value)
                {
                    return;
                }

                _Threads_SelectedIndex = value;
                OnPropertyChanged("Threads_SelectedIndex");
            }
        }

        // Selected Item
        private string _Threads_SelectedItem { get; set; }
        public string Threads_SelectedItem
        {
            get { return _Threads_SelectedItem; }
            set
            {
                if (_Threads_SelectedItem == value)
                {
                    return;
                }

                _Threads_SelectedItem = value;
                OnPropertyChanged("Threads_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Threads_IsEnabled = true;
        public bool Threads_IsEnabled
        {
            get { return _Threads_IsEnabled; }
            set
            {
                if (_Threads_IsEnabled == value)
                {
                    return;
                }

                _Threads_IsEnabled = value;
                OnPropertyChanged("Threads_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Theme
        // --------------------------------------------------
        // Item Source
        private List<string> _Theme_Items = new List<string>()
        {
            "Axiom",
            "FFmpeg",
            "Cyberpunk",
            "Onyx",
            "Circuit",
            "System"
        };
        public List<string> Theme_Items
        {
            get { return _Theme_Items; }
            set
            {
                _Theme_Items = value;
                OnPropertyChanged("Theme_Items");
            }
        }

        // Selected Index
        private int _Theme_SelectedIndex { get; set; }
        public int Theme_SelectedIndex
        {
            get { return _Theme_SelectedIndex; }
            set
            {
                if (_Theme_SelectedIndex == value)
                {
                    return;
                }

                _Theme_SelectedIndex = value;
                OnPropertyChanged("Theme_SelectedIndex");
            }
        }

        // Selected Item
        private string _Theme_SelectedItem { get; set; }
        public string Theme_SelectedItem
        {
            get { return _Theme_SelectedItem; }
            set
            {
                if (_Theme_SelectedItem == value)
                {
                    return;
                }

                _Theme_SelectedItem = value;
                OnPropertyChanged("Theme_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Theme_IsEnabled = true;
        public bool Theme_IsEnabled
        {
            get { return _Theme_IsEnabled; }
            set
            {
                if (_Theme_IsEnabled == value)
                {
                    return;
                }

                _Theme_IsEnabled = value;
                OnPropertyChanged("Theme_IsEnabled");
            }
        }


        // -------------------------
        // Update Auto Check - Toggle
        // -------------------------
        // Checked
        private bool _UpdateAutoCheck_IsChecked;
        public bool UpdateAutoCheck_IsChecked
        {
            get { return _UpdateAutoCheck_IsChecked; }
            set
            {
                if (_UpdateAutoCheck_IsChecked != value)
                {
                    _UpdateAutoCheck_IsChecked = value;
                    OnPropertyChanged("UpdateAutoCheck_IsChecked");
                }
            }
        }
        //private bool _UpdateAutoCheck_IsChecked;
        //public bool UpdateAutoCheck_IsChecked
        //{
        //    get { return _UpdateAutoCheck_IsChecked; }
        //    set
        //    {
        //        if (_UpdateAutoCheck_IsChecked == value) return;

        //        _UpdateAutoCheck_IsChecked = value;
        //    }
        //}
        // Enabled
        private bool _UpdateAutoCheck_IsEnabled = true;
        public bool UpdateAutoCheck_IsEnabled
        {
            get { return _UpdateAutoCheck_IsEnabled; }
            set
            {
                if (_UpdateAutoCheck_IsEnabled == value)
                {
                    return;
                }

                _UpdateAutoCheck_IsEnabled = value;
                OnPropertyChanged("UpdateAutoCheck_IsEnabled");
            }
        }




        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Subtitle
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // --------------------------------------------------
        // Subtitle Codec
        // --------------------------------------------------
        // Item Source
        public List<string> _SubtitleCodec_Items = new List<string>();
        public List<string> SubtitleCodec_Items
        {
            get { return _SubtitleCodec_Items; }
            set
            {
                _SubtitleCodec_Items = value;
                OnPropertyChanged("SubtitleCodec_Items");
            }
        }

        // Command
        public string SubtitleCodec_Command;

        // Selected Index
        public int _SubtitleCodec_SelectedIndex { get; set; }
        public int SubtitleCodec_SelectedIndex
        {
            get { return _SubtitleCodec_SelectedIndex; }
            set
            {
                if (_SubtitleCodec_SelectedIndex == value)
                {
                    return;
                }

                _SubtitleCodec_SelectedIndex = value;
                OnPropertyChanged("SubtitleCodec_SelectedIndex");
            }
        }

        // Selected Item
        public string _SubtitleCodec_SelectedItem { get; set; }
        public string SubtitleCodec_SelectedItem
        {
            get { return _SubtitleCodec_SelectedItem; }
            set
            {
                if (_SubtitleCodec_SelectedItem == value)
                {
                    return;
                }

                _SubtitleCodec_SelectedItem = value;
                OnPropertyChanged("SubtitleCodec_SelectedItem");
            }
        }

        // Controls Enable
        public bool _SubtitleCodec_IsEnabled = true;
        public bool SubtitleCodec_IsEnabled
        {
            get { return _SubtitleCodec_IsEnabled; }
            set
            {
                if (_SubtitleCodec_IsEnabled == value)
                {
                    return;
                }

                _SubtitleCodec_IsEnabled = value;
                OnPropertyChanged("SubtitleCodec_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Subtitle Stream
        // --------------------------------------------------
        // Item Source
        public List<string> _SubtitleStream_Items = new List<string>()
        {
            "none",
            "external",
            "all",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
        };
        public List<string> SubtitleStream_Items
        {
            get { return _SubtitleStream_Items; }
            set
            {
                _SubtitleStream_Items = value;
                OnPropertyChanged("SubtitleStream_Items");
            }
        }

        // Selected Index
        public int _SubtitleStream_SelectedIndex { get; set; }
        public int SubtitleStream_SelectedIndex
        {
            get { return _SubtitleStream_SelectedIndex; }
            set
            {
                if (_SubtitleStream_SelectedIndex == value)
                {
                    return;
                }

                _SubtitleStream_SelectedIndex = value;
                OnPropertyChanged("SubtitleStream_SelectedIndex");
            }
        }

        // Selected Item
        public string _SubtitleStream_SelectedItem { get; set; }
        public string SubtitleStream_SelectedItem
        {
            get { return _SubtitleStream_SelectedItem; }
            set
            {
                if (_SubtitleStream_SelectedItem == value)
                {
                    return;
                }

                _SubtitleStream_SelectedItem = value;
                OnPropertyChanged("SubtitleStream_SelectedItem");
            }
        }

        // Controls Enable
        public bool _SubtitleStream_IsEnabled = true;
        public bool SubtitleStream_IsEnabled
        {
            get { return _SubtitleStream_IsEnabled; }
            set
            {
                if (_SubtitleStream_IsEnabled == value)
                {
                    return;
                }

                _SubtitleStream_IsEnabled = value;
                OnPropertyChanged("SubtitleStream_IsEnabled");
            }
        }



        // --------------------------------------------------
        // Subtitle ListView
        // --------------------------------------------------
        // Item Source
        private ObservableCollection<string> _SubtitleListView_Items = new ObservableCollection<string>();
        public ObservableCollection<string> SubtitleListView_Items
        {
            get { return _SubtitleListView_Items; }
            set
            {
                _SubtitleListView_Items = value;
                OnPropertyChanged("SubtitleListView_Items");
            }
        }
        // Selected Items
        private List<string> _SubtitleListView_SelectedItems = new List<string>();
        public List<string> SubtitleListView_SelectedItems
        {
            get { return _SubtitleListView_SelectedItems; }
            set
            {
                _SubtitleListView_SelectedItems = value;
                OnPropertyChanged("SubtitleListView_SelectedItems");
            }
        }
        // Selected Idnex
        private int _SubtitleListView_SelectedIndex { get; set; }
        public int SubtitleListView_SelectedIndex
        {
            get { return _SubtitleListView_SelectedIndex; }
            set
            {
                if (_SubtitleListView_SelectedIndex == value)
                {
                    return;
                }

                _SubtitleListView_SelectedIndex = value;
                OnPropertyChanged("SubtitleListView_SelectedIndex");
            }
        }
        // Controls Enable
        public bool _SubtitleListView_IsEnabled = true;
        public bool SubtitleListView_IsEnabled
        {
            get { return _SubtitleListView_IsEnabled; }
            set
            {
                if (_SubtitleListView_IsEnabled == value)
                {
                    return;
                }

                _SubtitleListView_IsEnabled = value;
                OnPropertyChanged("SubtitleListView_IsEnabled");
            }
        }


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Filters
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // --------------------------------------------------------------------------------------------------------
        // Video
        // --------------------------------------------------------------------------------------------------------

        // -------------------------
        // Deband
        // -------------------------
        // -------------------------
        // Deband
        // -------------------------
        // Items
        public static List<string> _FilterVideo_Deband_Items = new List<string>()
        {
            "disabled",
            "enabled"
        };
        public static List<string> FilterVideo_Deband_Items
        {
            get { return _FilterVideo_Deband_Items; }
            set { _FilterVideo_Deband_Items = value; }
        }

        // Selected Item
        private string _FilterVideo_Deband_SelectedItem { get; set; }
        public string FilterVideo_Deband_SelectedItem
        {
            get { return _FilterVideo_Deband_SelectedItem; }
            set
            {
                if (_FilterVideo_Deband_SelectedItem == value)
                {
                    return;
                }

                _FilterVideo_Deband_SelectedItem = value;
                OnPropertyChanged("FilterVideo_Deband_SelectedItem");
            }
        }

        // -------------------------
        // Deshake
        // -------------------------
        // Items
        public static List<string> _FilterVideo_Deshake_Items = new List<string>()
        {
            "disabled",
            "enabled"
        };
        public static List<string> FilterVideo_Deshake_Items
        {
            get { return _FilterVideo_Deshake_Items; }
            set { _FilterVideo_Deshake_Items = value; }
        }

        // Selected Item
        private string _FilterVideo_Deshake_SelectedItem { get; set; }
        public string FilterVideo_Deshake_SelectedItem
        {
            get { return _FilterVideo_Deshake_SelectedItem; }
            set
            {
                if (_FilterVideo_Deshake_SelectedItem == value)
                {
                    return;
                }

                _FilterVideo_Deshake_SelectedItem = value;
                OnPropertyChanged("FilterVideo_Deshake_SelectedItem");
            }
        }


        // -------------------------
        // Deflicker
        // -------------------------
        // Items
        public static List<string> _FilterVideo_Deflicker_Items = new List<string>()
        {
            "disabled",
            "enabled"
        };
        public static List<string> FilterVideo_Deflicker_Items
        {
            get { return _FilterVideo_Deflicker_Items; }
            set { _FilterVideo_Deflicker_Items = value; }
        }
        // Selected Item
        private string _FilterVideo_Deflicker_SelectedItem { get; set; }
        public string FilterVideo_Deflicker_SelectedItem
        {
            get { return _FilterVideo_Deflicker_SelectedItem; }
            set
            {
                if (_FilterVideo_Deflicker_SelectedItem == value)
                {
                    return;
                }

                _FilterVideo_Deflicker_SelectedItem = value;
                OnPropertyChanged("FilterVideo_Deflicker_SelectedItem");
            }
        }


        // -------------------------
        // Dejudder
        // -------------------------
        // Items
        public static List<string> _FilterVideo_Dejudder_Items = new List<string>()
        {
            "disabled",
            "enabled"
        };
        public static List<string> FilterVideo_Dejudder_Items
        {
            get { return _FilterVideo_Dejudder_Items; }
            set { _FilterVideo_Dejudder_Items = value; }
        }
        // Selected Item
        private string _FilterVideo_Dejudder_SelectedItem { get; set; }
        public string FilterVideo_Dejudder_SelectedItem
        {
            get { return _FilterVideo_Dejudder_SelectedItem; }
            set
            {
                if (_FilterVideo_Dejudder_SelectedItem == value)
                {
                    return;
                }

                _FilterVideo_Dejudder_SelectedItem = value;
                OnPropertyChanged("FilterVideo_Dejudder_SelectedItem");
            }
        }

        // -------------------------
        // Denoise
        // -------------------------
        // Items
        public static List<string> _FilterVideo_Denoise_Items = new List<string>()
        {
            "disabled",
            "default",
            "light",
            "medium",
            "heavy",
        };
        public static List<string> FilterVideo_Denoise_Items
        {
            get { return _FilterVideo_Denoise_Items; }
            set { _FilterVideo_Denoise_Items = value; }
        }
        // Selected Item
        private string _FilterVideo_Denoise_SelectedItem { get; set; }
        public string FilterVideo_Denoise_SelectedItem
        {
            get { return _FilterVideo_Denoise_SelectedItem; }
            set
            {
                if (_FilterVideo_Denoise_SelectedItem == value)
                {
                    return;
                }

                _FilterVideo_Denoise_SelectedItem = value;
                OnPropertyChanged("FilterVideo_Denoise_SelectedItem");
            }
        }


        // -------------------------
        // EQ Brightness
        // -------------------------
        // Value
        private double _FilterVideo_EQ_Brightness_Value = 0;
        public double FilterVideo_EQ_Brightness_Value
        {
            get { return _FilterVideo_EQ_Brightness_Value; }
            set
            {
                if (_FilterVideo_EQ_Brightness_Value == value)
                {
                    return;
                }

                _FilterVideo_EQ_Brightness_Value = value;
                OnPropertyChanged("FilterVideo_EQ_Brightness_Value");
            }
        }

        // -------------------------
        // EQ Contrast
        // -------------------------
        // Value
        private double _FilterVideo_EQ_Contrast_Value = 0;
        public double FilterVideo_EQ_Contrast_Value
        {
            get { return _FilterVideo_EQ_Contrast_Value; }
            set
            {
                if (_FilterVideo_EQ_Contrast_Value == value)
                {
                    return;
                }

                _FilterVideo_EQ_Contrast_Value = value;
                OnPropertyChanged("FilterVideo_EQ_Contrast_Value");
            }
        }

        // -------------------------
        // EQ Saturation
        // -------------------------
        // Value
        private double _FilterVideo_EQ_Saturation_Value = 0;
        public double FilterVideo_EQ_Saturation_Value
        {
            get { return _FilterVideo_EQ_Saturation_Value; }
            set
            {
                if (_FilterVideo_EQ_Saturation_Value == value)
                {
                    return;
                }

                _FilterVideo_EQ_Saturation_Value = value;
                OnPropertyChanged("FilterVideo_EQ_Saturation_Value");
            }
        }

        // -------------------------
        // EQ Gamma
        // -------------------------
        // Value
        private double _FilterVideo_EQ_Gamma_Value = 0;
        public double FilterVideo_EQ_Gamma_Value
        {
            get { return _FilterVideo_EQ_Gamma_Value; }
            set
            {
                if (_FilterVideo_EQ_Gamma_Value == value)
                {
                    return;
                }

                _FilterVideo_EQ_Gamma_Value = value;
                OnPropertyChanged("FilterVideo_EQ_Gamma_Value");
            }
        }


        // -------------------------
        // Selective Color
        // -------------------------
        // -------------------------
        // Correction Method
        // -------------------------
        // Items
        public static List<string> _FilterVideo_SelectiveColor_Correction_Method_Items = new List<string>()
        {
            "relative",
            "absolute"
        };
        public static List<string> FilterVideo_SelectiveColor_Correction_Method_Items
        {
            get { return _FilterVideo_SelectiveColor_Correction_Method_Items; }
            set { _FilterVideo_SelectiveColor_Correction_Method_Items = value; }
        }

        // Selected Item
        private string _FilterVideo_SelectiveColor_Correction_Method_SelectedItem { get; set; }
        public string FilterVideo_SelectiveColor_Correction_Method_SelectedItem
        {
            get { return _FilterVideo_SelectiveColor_Correction_Method_SelectedItem; }
            set
            {
                if (_FilterVideo_SelectiveColor_Correction_Method_SelectedItem == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Correction_Method_SelectedItem = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Correction_Method_SelectedItem");
            }
        }


        // --------------------------------------------------
        // Reds
        // --------------------------------------------------
        // -------------------------
        // Reds Cyan
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Reds_Cyan_Value = 0;
        public double FilterVideo_SelectiveColor_Reds_Cyan_Value
        {
            get { return _FilterVideo_SelectiveColor_Reds_Cyan_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Reds_Cyan_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Reds_Cyan_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Reds_Cyan_Value");
            }
        }

        // -------------------------
        // Reds Magenta
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Reds_Magenta_Value = 0;
        public double FilterVideo_SelectiveColor_Reds_Magenta_Value
        {
            get { return _FilterVideo_SelectiveColor_Reds_Magenta_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Reds_Magenta_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Reds_Magenta_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Reds_Magenta_Value");
            }
        }

        // -------------------------
        // Reds Yellow
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Reds_Yellow_Value = 0;
        public double FilterVideo_SelectiveColor_Reds_Yellow_Value
        {
            get { return _FilterVideo_SelectiveColor_Reds_Yellow_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Reds_Yellow_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Reds_Yellow_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Reds_Yellow_Value");
            }
        }


        // --------------------------------------------------
        // Yellows
        // --------------------------------------------------
        // -------------------------
        // Yellows Cyan
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Yellows_Cyan_Value = 0;
        public double FilterVideo_SelectiveColor_Yellows_Cyan_Value
        {
            get { return _FilterVideo_SelectiveColor_Yellows_Cyan_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Yellows_Cyan_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Yellows_Cyan_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Yellows_Cyan_Value");
            }
        }

        // -------------------------
        // Yellows Magenta
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Yellows_Magenta_Value = 0;
        public double FilterVideo_SelectiveColor_Yellows_Magenta_Value
        {
            get { return _FilterVideo_SelectiveColor_Yellows_Magenta_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Yellows_Magenta_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Yellows_Magenta_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Yellows_Magenta_Value");
            }
        }

        // -------------------------
        // Yellows Yellow
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Yellows_Yellow_Value = 0;
        public double FilterVideo_SelectiveColor_Yellows_Yellow_Value
        {
            get { return _FilterVideo_SelectiveColor_Yellows_Yellow_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Yellows_Yellow_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Yellows_Yellow_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Yellows_Yellow_Value");
            }
        }


        // --------------------------------------------------
        // Greens
        // --------------------------------------------------
        // -------------------------
        // Greens Cyan
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Greens_Cyan_Value = 0;
        public double FilterVideo_SelectiveColor_Greens_Cyan_Value
        {
            get { return _FilterVideo_SelectiveColor_Greens_Cyan_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Greens_Cyan_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Greens_Cyan_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Greens_Cyan_Value");
            }
        }

        // -------------------------
        // Greens Magenta
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Greens_Magenta_Value = 0;
        public double FilterVideo_SelectiveColor_Greens_Magenta_Value
        {
            get { return _FilterVideo_SelectiveColor_Greens_Magenta_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Greens_Magenta_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Greens_Magenta_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Greens_Magenta_Value");
            }
        }

        // -------------------------
        // Greens Yellow
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Greens_Yellow_Value = 0;
        public double FilterVideo_SelectiveColor_Greens_Yellow_Value
        {
            get { return _FilterVideo_SelectiveColor_Greens_Yellow_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Greens_Yellow_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Greens_Yellow_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Greens_Yellow_Value");
            }
        }

        // --------------------------------------------------
        // Cyans
        // --------------------------------------------------
        // -------------------------
        // Cyans Cyan
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Cyans_Cyan_Value = 0;
        public double FilterVideo_SelectiveColor_Cyans_Cyan_Value
        {
            get { return _FilterVideo_SelectiveColor_Cyans_Cyan_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Cyans_Cyan_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Cyans_Cyan_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Cyans_Cyan_Value");
            }
        }

        // -------------------------
        // Cyans Magenta
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Cyans_Magenta_Value = 0;
        public double FilterVideo_SelectiveColor_Cyans_Magenta_Value
        {
            get { return _FilterVideo_SelectiveColor_Cyans_Magenta_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Cyans_Magenta_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Cyans_Magenta_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Cyans_Magenta_Value");
            }
        }

        // -------------------------
        // Cyans Yellow
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Cyans_Yellow_Value = 0;
        public double FilterVideo_SelectiveColor_Cyans_Yellow_Value
        {
            get { return _FilterVideo_SelectiveColor_Cyans_Yellow_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Cyans_Yellow_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Cyans_Yellow_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Cyans_Yellow_Value");
            }
        }

        // --------------------------------------------------
        // Blues
        // --------------------------------------------------
        // -------------------------
        // Blues Cyan
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Blues_Cyan_Value = 0;
        public double FilterVideo_SelectiveColor_Blues_Cyan_Value
        {
            get { return _FilterVideo_SelectiveColor_Blues_Cyan_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Blues_Cyan_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Blues_Cyan_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Blues_Cyan_Value");
            }
        }

        // -------------------------
        // Blues Magenta
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Blues_Magenta_Value = 0;
        public double FilterVideo_SelectiveColor_Blues_Magenta_Value
        {
            get { return _FilterVideo_SelectiveColor_Blues_Magenta_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Blues_Magenta_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Blues_Magenta_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Blues_Magenta_Value");
            }
        }

        // -------------------------
        // Blues Yellow
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Blues_Yellow_Value = 0;
        public double FilterVideo_SelectiveColor_Blues_Yellow_Value
        {
            get { return _FilterVideo_SelectiveColor_Blues_Yellow_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Blues_Yellow_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Blues_Yellow_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Blues_Yellow_Value");
            }
        }

        // --------------------------------------------------
        // Magentas
        // --------------------------------------------------
        // -------------------------
        // Magentas Cyan
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Magentas_Cyan_Value = 0;
        public double FilterVideo_SelectiveColor_Magentas_Cyan_Value
        {
            get { return _FilterVideo_SelectiveColor_Magentas_Cyan_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Magentas_Cyan_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Magentas_Cyan_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Magentas_Cyan_Value");
            }
        }

        // -------------------------
        // Magentas Magenta
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Magentas_Magenta_Value = 0;
        public double FilterVideo_SelectiveColor_Magentas_Magenta_Value
        {
            get { return _FilterVideo_SelectiveColor_Magentas_Magenta_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Magentas_Magenta_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Magentas_Magenta_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Magentas_Magenta_Value");
            }
        }

        // -------------------------
        // Magentas Yellow
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Magentas_Yellow_Value = 0;
        public double FilterVideo_SelectiveColor_Magentas_Yellow_Value
        {
            get { return _FilterVideo_SelectiveColor_Magentas_Yellow_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Magentas_Yellow_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Magentas_Yellow_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Magentas_Yellow_Value");
            }
        }

        // --------------------------------------------------
        // Whites
        // --------------------------------------------------
        // -------------------------
        // Whites Cyan
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Whites_Cyan_Value = 0;
        public double FilterVideo_SelectiveColor_Whites_Cyan_Value
        {
            get { return _FilterVideo_SelectiveColor_Whites_Cyan_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Whites_Cyan_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Whites_Cyan_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Whites_Cyan_Value");
            }
        }

        // -------------------------
        // Whites Magenta
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Whites_Magenta_Value = 0;
        public double FilterVideo_SelectiveColor_Whites_Magenta_Value
        {
            get { return _FilterVideo_SelectiveColor_Whites_Magenta_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Whites_Magenta_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Whites_Magenta_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Whites_Magenta_Value");
            }
        }

        // -------------------------
        // Whites Yellow
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Whites_Yellow_Value = 0;
        public double FilterVideo_SelectiveColor_Whites_Yellow_Value
        {
            get { return _FilterVideo_SelectiveColor_Whites_Yellow_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Whites_Yellow_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Whites_Yellow_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Whites_Yellow_Value");
            }
        }

        // --------------------------------------------------
        // Neutrals
        // --------------------------------------------------
        // -------------------------
        // Neutrals Cyan
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Neutrals_Cyan_Value = 0;
        public double FilterVideo_SelectiveColor_Neutrals_Cyan_Value
        {
            get { return _FilterVideo_SelectiveColor_Neutrals_Cyan_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Neutrals_Cyan_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Neutrals_Cyan_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Neutrals_Cyan_Value");
            }
        }

        // -------------------------
        // Neutrals Magenta
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Neutrals_Magenta_Value = 0;
        public double FilterVideo_SelectiveColor_Neutrals_Magenta_Value
        {
            get { return _FilterVideo_SelectiveColor_Neutrals_Magenta_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Neutrals_Magenta_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Neutrals_Magenta_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Neutrals_Magenta_Value");
            }
        }

        // -------------------------
        // Neutrals Yellow
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Neutrals_Yellow_Value = 0;
        public double FilterVideo_SelectiveColor_Neutrals_Yellow_Value
        {
            get { return _FilterVideo_SelectiveColor_Neutrals_Yellow_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Neutrals_Yellow_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Neutrals_Yellow_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Neutrals_Yellow_Value");
            }
        }

        // --------------------------------------------------
        // Blacks
        // --------------------------------------------------
        // -------------------------
        // Blacks Cyan
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Blacks_Cyan_Value = 0;
        public double FilterVideo_SelectiveColor_Blacks_Cyan_Value
        {
            get { return _FilterVideo_SelectiveColor_Blacks_Cyan_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Blacks_Cyan_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Blacks_Cyan_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Blacks_Cyan_Value");
            }
        }

        // -------------------------
        // Blacks Magenta
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Blacks_Magenta_Value = 0;
        public double FilterVideo_SelectiveColor_Blacks_Magenta_Value
        {
            get { return _FilterVideo_SelectiveColor_Blacks_Magenta_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Blacks_Magenta_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Blacks_Magenta_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Blacks_Magenta_Value");
            }
        }

        // -------------------------
        // Blacks Yellow
        // -------------------------
        // Value
        private double _FilterVideo_SelectiveColor_Blacks_Yellow_Value = 0;
        public double FilterVideo_SelectiveColor_Blacks_Yellow_Value
        {
            get { return _FilterVideo_SelectiveColor_Blacks_Yellow_Value; }
            set
            {
                if (_FilterVideo_SelectiveColor_Blacks_Yellow_Value == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Blacks_Yellow_Value = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Blacks_Yellow_Value");
            }
        }



        // --------------------------------------------------------------------------------------------------------
        // Audio
        // --------------------------------------------------------------------------------------------------------
        // -------------------------
        // Lowpass
        // -------------------------
        // Items
        public static List<string> _FilterAudio_Lowpass_Items = new List<string>()
        {
            "disabled",
            "enabled"
        };
        public static List<string> FilterAudio_Lowpass_Items
        {
            get { return _FilterAudio_Lowpass_Items; }
            set { _FilterAudio_Lowpass_Items = value; }
        }

        // Selected Item
        private string _FilterAudio_Lowpass_SelectedItem { get; set; }
        public string FilterAudio_Lowpass_SelectedItem
        {
            get { return _FilterAudio_Lowpass_SelectedItem; }
            set
            {
                if (_FilterAudio_Lowpass_SelectedItem == value)
                {
                    return;
                }

                _FilterAudio_Lowpass_SelectedItem = value;
                OnPropertyChanged("FilterAudio_Lowpass_SelectedItem");
            }
        }

        // -------------------------
        // Highpass
        // -------------------------
        // Items
        public static List<string> _FilterAudio_Highpass_Items = new List<string>()
        {
            "disabled",
            "enabled"
        };
        public static List<string> FilterAudio_Highpass_Items
        {
            get { return _FilterAudio_Highpass_Items; }
            set { _FilterAudio_Highpass_Items = value; }
        }
        // Selected Item
        private string _FilterAudio_Highpass_SelectedItem { get; set; }
        public string FilterAudio_Highpass_SelectedItem
        {
            get { return _FilterAudio_Highpass_SelectedItem; }
            set
            {
                if (_FilterAudio_Highpass_SelectedItem == value)
                {
                    return;
                }

                _FilterAudio_Highpass_SelectedItem = value;
                OnPropertyChanged("FilterAudio_Highpass_SelectedItem");
            }
        }

        // -------------------------
        // Headphones (Earwax)
        // -------------------------
        // Items
        public static List<string> _FilterAudio_Headphones_Items = new List<string>()
        {
            "disabled",
            "enabled"
        };
        public static List<string> FilterAudio_Headphones_Items
        {
            get { return _FilterAudio_Headphones_Items; }
            set { _FilterAudio_Headphones_Items = value; }
        }

        // Selected Item
        private string _FilterAudio_Headphones_SelectedItem { get; set; }
        public string FilterAudio_Headphones_SelectedItem
        {
            get { return _FilterAudio_Headphones_SelectedItem; }
            set
            {
                if (_FilterAudio_Headphones_SelectedItem == value)
                {
                    return;
                }

                _FilterAudio_Headphones_SelectedItem = value;
                OnPropertyChanged("FilterAudio_Headphones_SelectedItem");
            }
        }

        // -------------------------
        // Contrast
        // -------------------------
        // Value
        private double _FilterAudio_Contrast_Value = 0;
        public double FilterAudio_Contrast_Value
        {
            get { return _FilterAudio_Contrast_Value; }
            set
            {
                if (_FilterAudio_Contrast_Value == value)
                {
                    return;
                }

                _FilterAudio_Contrast_Value = value;
                OnPropertyChanged("FilterAudio_Contrast_Value");
            }
        }

        // -------------------------
        // Extra Stereo
        // -------------------------
        // Value
        private double _FilterAudio_ExtraStereo_Value = 0;
        public double FilterAudio_ExtraStereo_Value
        {
            get { return _FilterAudio_ExtraStereo_Value; }
            set
            {
                if (_FilterAudio_ExtraStereo_Value == value)
                {
                    return;
                }

                _FilterAudio_ExtraStereo_Value = value;
                OnPropertyChanged("FilterAudio_ExtraStereo_Value");
            }
        }

        // -------------------------
        // Tempo
        // -------------------------
        // Value
        private double _FilterAudio_Tempo_Value = 100;
        public double FilterAudio_Tempo_Value
        {
            get { return _FilterAudio_Tempo_Value; }
            set
            {
                if (_FilterAudio_Tempo_Value == value)
                {
                    return;
                }

                _FilterAudio_Tempo_Value = value;
                OnPropertyChanged("FilterAudio_Tempo_Value");
            }
        }


    }
}
