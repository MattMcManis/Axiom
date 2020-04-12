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
using System.ComponentModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class FormatViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void OnPropertyChanged(string prop)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(prop));
            }
        }

        // Format View Model
        //public static FormatView vm = new FormatView();


        /// <summary>
        /// Format View Model
        /// </summary>
        public FormatViewModel()
        {
            LoadControlsDefaults();
        }

        /// <summary>
        /// Load Controls Defaults
        /// </summary>
        public void LoadControlsDefaults()
        {
            Format_Container_SelectedItem = "webm";
            Format_MediaType_SelectedItem = "Video";
            Format_Cut_SelectedItem = "No";
            Format_YouTube_SelectedItem = "Video + Audio";
            Format_YouTube_Quality_SelectedItem = "best";
        }


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Format
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------
        // Containers
        // --------------------------------------------------
        // Items Source
        public class FormatContainer
        {
            public string Name { get; set; }
            public bool Category { get; set; }
        }

        public List<FormatContainer> _Format_Container_Items = new List<FormatContainer>()
        {
            new FormatContainer() { Name = "Video", Category = true  },
            new FormatContainer() { Name = "webm",  Category = false },
            new FormatContainer() { Name = "mp4",   Category = false },
            new FormatContainer() { Name = "mkv",   Category = false },
            new FormatContainer() { Name = "mpg",   Category = false },
            new FormatContainer() { Name = "avi",   Category = false },
            new FormatContainer() { Name = "ogv",   Category = false },
            //new FormatContainer() { Name = "m2v",   Category = false },

            new FormatContainer() { Name = "Audio", Category = true  },
            new FormatContainer() { Name = "mp3",   Category = false },
            new FormatContainer() { Name = "m4a",   Category = false },
            new FormatContainer() { Name = "ogg",   Category = false },
            new FormatContainer() { Name = "flac",  Category = false },
            new FormatContainer() { Name = "wav",   Category = false },

            new FormatContainer() { Name = "Image", Category = true  },
            new FormatContainer() { Name = "jpg",   Category = false },
            new FormatContainer() { Name = "png",   Category = false },
            new FormatContainer() { Name = "webp",  Category = false },
        };

        public List<FormatContainer> Format_Container_Items
        {
            get { return _Format_Container_Items; }
            set
            {
                _Format_Container_Items = value;
                OnPropertyChanged("Format_Container_Items");
            }
        }

        private int _Format_Container_SelectedIndex { get; set; }
        public int Format_Container_SelectedIndex
        {
            get { return _Format_Container_SelectedIndex; }
            set
            {
                //if (_Format_Container_SelectedIndex != value)
                //{
                //    _Format_Container_SelectedIndex = value;
                //    OnPropertyChanged("Format_Container_SelectedIndex");
                //}
                if (_Format_Container_SelectedIndex == value)
                {
                    return;
                }

                _Format_Container_SelectedIndex = value;
                OnPropertyChanged("Format_Container_SelectedIndex");
            }
        }

        // Selected Item
        //public string Format_Container_SelectedItem { get; set; }
        private string _Format_Container_SelectedItem { get; set; }
        public string Format_Container_SelectedItem
        {
            get { return _Format_Container_SelectedItem; }
            set
            {
                //var previousItem = _Format_Container_SelectedItem;
                //System.Windows.MessageBox.Show(previousItem); //debug

                if (_Format_Container_SelectedItem == value)
                {
                    return;
                }

                _Format_Container_SelectedItem = value;
                OnPropertyChanged("Format_Container_SelectedItem");

                //System.Windows.MessageBox.Show(previousItem); //debug
                //if (previousItem != value && 
                //    !string.IsNullOrEmpty(previousItem))
                //{
                //    //System.Windows.MessageBox.Show("!"); //debug
                //    VideoControls.AutoCopyVideoCodec();
                //    SubtitleControls.AutoCopySubtitleCodec();
                //    AudioControls.AutoCopyAudioCodec();
                //}
            }
        }


        // --------------------------------------------------
        // Media Type
        // --------------------------------------------------
        // Items Source
        private List<string> _Format_MediaType_Items = new List<string>()
        {
            "Video",
            "Audio",
            "Image",
            "Sequence"
        };
        public List<string> Format_MediaType_Items
        {
            get { return _Format_MediaType_Items; }
            set
            {
                _Format_MediaType_Items = value;
                OnPropertyChanged("Format_MediaType_Items");
            }
        }

        // Selected Index
        private int _Format_MediaType_SelectedIndex { get; set; }
        public int Format_MediaType_SelectedIndex
        {
            get { return _Format_MediaType_SelectedIndex; }
            set
            {
                if (_Format_MediaType_SelectedIndex == value)
                {
                    return;
                }

                _Format_MediaType_SelectedIndex = value;
                OnPropertyChanged("Format_MediaType_SelectedIndex");
            }
        }

        // Selected Item
        private string _Format_MediaType_SelectedItem { get; set; }
        public string Format_MediaType_SelectedItem
        {
            get { return _Format_MediaType_SelectedItem; }
            set
            {
                if (_Format_MediaType_SelectedItem == value)
                {
                    return;
                }

                _Format_MediaType_SelectedItem = value;
                OnPropertyChanged("Format_MediaType_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Format_MediaType_IsEnabled = true;
        public bool Format_MediaType_IsEnabled
        {
            get { return _Format_MediaType_IsEnabled; }
            set
            {
                if (_Format_MediaType_IsEnabled == value)
                {
                    return;
                }

                _Format_MediaType_IsEnabled = value;
                OnPropertyChanged("Format_MediaType_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Cut
        // --------------------------------------------------
        // Items Source
        private List<string> _Format_Cut_Items = new List<string>()
        {
            "No",
            "Yes"
        };
        public List<string> Format_Cut_Items
        {
            get { return _Format_Cut_Items; }
            set
            {
                _Format_Cut_Items = value;
                OnPropertyChanged("Format_Cut_Items");
            }
        }

        // Selected Index
        private int _Format_Cut_SelectedIndex { get; set; }
        public int Format_Cut_SelectedIndex
        {
            get { return _Format_Cut_SelectedIndex; }
            set
            {
                if (_Format_Cut_SelectedIndex == value)
                {
                    return;
                }

                _Format_Cut_SelectedIndex = value;
                OnPropertyChanged("Format_Cut_SelectedIndex");
            }
        }

        // Selected Item
        private string _Format_Cut_SelectedItem { get; set; }
        public string Format_Cut_SelectedItem
        {
            get { return _Format_Cut_SelectedItem; }
            set
            {
                if (_Format_Cut_SelectedItem == value)
                {
                    return;
                }

                _Format_Cut_SelectedItem = value;
                OnPropertyChanged("Format_Cut_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Format_Cut_IsEnabled = true;
        public bool Format_Cut_IsEnabled
        {
            get { return _Format_Cut_IsEnabled; }
            set
            {
                if (_Format_Cut_IsEnabled == value)
                {
                    return;
                }

                _Format_Cut_IsEnabled = value;
                OnPropertyChanged("Format_Cut_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Cut Start
        // --------------------------------------------------
        // Text
        //private string _Format_CutStart_Text = "00:00:00.000";
        //public string Format_CutStart_Text
        //{
        //    get { return _Format_CutStart_Text; }
        //    set
        //    {
        //        if (_Format_CutStart_Text == value)
        //        {
        //            return;
        //        }

        //        _Format_CutStart_Text = value;
        //        OnPropertyChanged("Format_CutStart_Text");
        //    }
        //}
        // Controls Enable
        private bool _Format_CutStart_IsEnabled = true;
        public bool Format_CutStart_IsEnabled
        {
            get { return _Format_CutStart_IsEnabled; }
            set
            {
                if (_Format_CutStart_IsEnabled == value)
                {
                    return;
                }

                _Format_CutStart_IsEnabled = value;
                OnPropertyChanged("Format_CutStart_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Cut Start - Hours
        // --------------------------------------------------
        // Text
        private string _Format_CutStart_Hours_Text;
        public string Format_CutStart_Hours_Text
        {
            get { return _Format_CutStart_Hours_Text; }
            set
            {
                if (_Format_CutStart_Hours_Text == value)
                {
                    return;
                }

                _Format_CutStart_Hours_Text = value;
                OnPropertyChanged("Format_CutStart_Hours_Text");
            }
        }
        // Controls Enable
        //private bool _Format_CutStart_Hours_IsEnabled = true;
        //public bool Format_CutStart_Hours_IsEnabled
        //{
        //    get { return _Format_CutStart_Hours_IsEnabled; }
        //    set
        //    {
        //        if (_Format_CutStart_Hours_IsEnabled == value)
        //        {
        //            return;
        //        }

        //        _Format_CutStart_Hours_IsEnabled = value;
        //        OnPropertyChanged("Format_CutStart_Hours_IsEnabled");
        //    }
        //}

        // --------------------------------------------------
        // Cut Start - Minutes
        // --------------------------------------------------
        // Text
        private string _Format_CutStart_Minutes_Text;
        public string Format_CutStart_Minutes_Text
        {
            get { return _Format_CutStart_Minutes_Text; }
            set
            {
                if (_Format_CutStart_Minutes_Text == value)
                {
                    return;
                }

                _Format_CutStart_Minutes_Text = value;
                OnPropertyChanged("Format_CutStart_Minutes_Text");
            }
        }
        // Controls Enable
        //private bool _Format_CutStart_Minutes_IsEnabled = true;
        //public bool Format_CutStart_Minutes_IsEnabled
        //{
        //    get { return _Format_CutStart_Minutes_IsEnabled; }
        //    set
        //    {
        //        if (_Format_CutStart_Minutes_IsEnabled == value)
        //        {
        //            return;
        //        }

        //        _Format_CutStart_Minutes_IsEnabled = value;
        //        OnPropertyChanged("Format_CutStart_Minutes_IsEnabled");
        //    }
        //}

        // --------------------------------------------------
        // Cut Start - Seconds
        // --------------------------------------------------
        // Text
        private string _Format_CutStart_Seconds_Text;
        public string Format_CutStart_Seconds_Text
        {
            get { return _Format_CutStart_Seconds_Text; }
            set
            {
                if (_Format_CutStart_Seconds_Text == value)
                {
                    return;
                }

                _Format_CutStart_Seconds_Text = value;
                OnPropertyChanged("Format_CutStart_Seconds_Text");
            }
        }
        // Controls Enable
        //private bool _Format_CutStart_Seconds_IsEnabled = true;
        //public bool Format_CutStart_Seconds_IsEnabled
        //{
        //    get { return _Format_CutStart_Seconds_IsEnabled; }
        //    set
        //    {
        //        if (_Format_CutStart_Seconds_IsEnabled == value)
        //        {
        //            return;
        //        }

        //        _Format_CutStart_Seconds_IsEnabled = value;
        //        OnPropertyChanged("Format_CutStart_Seconds_IsEnabled");
        //    }
        //}

        // --------------------------------------------------
        // Cut Start - Milliseconds
        // --------------------------------------------------
        // Text
        private string _Format_CutStart_Milliseconds_Text;
        public string Format_CutStart_Milliseconds_Text
        {
            get { return _Format_CutStart_Milliseconds_Text; }
            set
            {
                if (_Format_CutStart_Milliseconds_Text == value)
                {
                    return;
                }

                _Format_CutStart_Milliseconds_Text = value;
                OnPropertyChanged("Format_CutStart_Milliseconds_Text");
            }
        }
        // Controls Enable
        //private bool _Format_CutStart_Milliseconds_IsEnabled = true;
        //public bool Format_CutStart_Milliseconds_IsEnabled
        //{
        //    get { return _Format_CutStart_Milliseconds_IsEnabled; }
        //    set
        //    {
        //        if (_Format_CutStart_Milliseconds_IsEnabled == value)
        //        {
        //            return;
        //        }

        //        _Format_CutStart_Milliseconds_IsEnabled = value;
        //        OnPropertyChanged("Format_CutStart_Milliseconds_IsEnabled");
        //    }
        //}



        // --------------------------------------------------
        // Cut End
        // --------------------------------------------------
        // Text
        //private string _Format_CutEnd_Text = "00:00:00.000";
        //public string Format_CutEnd_Text
        //{
        //    get { return _Format_CutEnd_Text; }
        //    set
        //    {
        //        if (_Format_CutEnd_Text == value)
        //        {
        //            return;
        //        }

        //        _Format_CutEnd_Text = value;
        //        OnPropertyChanged("Format_CutEnd_Text");
        //    }
        //}
        // Controls Enable
        private bool _Format_CutEnd_IsEnabled = true;
        public bool Format_CutEnd_IsEnabled
        {
            get { return _Format_CutEnd_IsEnabled; }
            set
            {
                if (_Format_CutEnd_IsEnabled == value)
                {
                    return;
                }

                _Format_CutEnd_IsEnabled = value;
                OnPropertyChanged("Format_CutEnd_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Cut End - Hours
        // --------------------------------------------------
        // Text
        private string _Format_CutEnd_Hours_Text;
        public string Format_CutEnd_Hours_Text
        {
            get { return _Format_CutEnd_Hours_Text; }
            set
            {
                if (_Format_CutEnd_Hours_Text == value)
                {
                    return;
                }

                _Format_CutEnd_Hours_Text = value;
                OnPropertyChanged("Format_CutEnd_Hours_Text");
            }
        }
        // Controls Enable
        //private bool _Format_CutEnd_Hours_IsEnabled = true;
        //public bool Format_CutEnd_Hours_IsEnabled
        //{
        //    get { return _Format_CutEnd_Hours_IsEnabled; }
        //    set
        //    {
        //        if (_Format_CutEnd_Hours_IsEnabled == value)
        //        {
        //            return;
        //        }

        //        _Format_CutEnd_Hours_IsEnabled = value;
        //        OnPropertyChanged("Format_CutEnd_Hours_IsEnabled");
        //    }
        //}

        // --------------------------------------------------
        // Cut End - Minutes
        // --------------------------------------------------
        // Text
        private string _Format_CutEnd_Minutes_Text;
        public string Format_CutEnd_Minutes_Text
        {
            get { return _Format_CutEnd_Minutes_Text; }
            set
            {
                if (_Format_CutEnd_Minutes_Text == value)
                {
                    return;
                }

                _Format_CutEnd_Minutes_Text = value;
                OnPropertyChanged("Format_CutEnd_Minutes_Text");
            }
        }
        // Controls Enable
        //private bool _Format_CutEnd_Minutes_IsEnabled = true;
        //public bool Format_CutEnd_Minutes_IsEnabled
        //{
        //    get { return _Format_CutEnd_Minutes_IsEnabled; }
        //    set
        //    {
        //        if (_Format_CutEnd_Minutes_IsEnabled == value)
        //        {
        //            return;
        //        }

        //        _Format_CutEnd_Minutes_IsEnabled = value;
        //        OnPropertyChanged("Format_CutEnd_Minutes_IsEnabled");
        //    }
        //}

        // --------------------------------------------------
        // Cut End - Seconds
        // --------------------------------------------------
        // Text
        private string _Format_CutEnd_Seconds_Text;
        public string Format_CutEnd_Seconds_Text
        {
            get { return _Format_CutEnd_Seconds_Text; }
            set
            {
                if (_Format_CutEnd_Seconds_Text == value)
                {
                    return;
                }

                _Format_CutEnd_Seconds_Text = value;
                OnPropertyChanged("Format_CutEnd_Seconds_Text");
            }
        }
        // Controls Enable
        //private bool _Format_CutEnd_Seconds_IsEnabled = true;
        //public bool Format_CutEnd_Seconds_IsEnabled
        //{
        //    get { return _Format_CutEnd_Seconds_IsEnabled; }
        //    set
        //    {
        //        if (_Format_CutEnd_Seconds_IsEnabled == value)
        //        {
        //            return;
        //        }

        //        _Format_CutEnd_Seconds_IsEnabled = value;
        //        OnPropertyChanged("Format_CutEnd_Seconds_IsEnabled");
        //    }
        //}

        // --------------------------------------------------
        // Cut End - Milliseconds
        // --------------------------------------------------
        // Text
        private string _Format_CutEnd_Milliseconds_Text;
        public string Format_CutEnd_Milliseconds_Text
        {
            get { return _Format_CutEnd_Milliseconds_Text; }
            set
            {
                if (_Format_CutEnd_Milliseconds_Text == value)
                {
                    return;
                }

                _Format_CutEnd_Milliseconds_Text = value;
                OnPropertyChanged("Format_CutEnd_Milliseconds_Text");
            }
        }
        // Controls Enable
        //private bool _Format_CutEnd_Milliseconds_IsEnabled = true;
        //public bool Format_CutEnd_Milliseconds_IsEnabled
        //{
        //    get { return _Format_CutEnd_Milliseconds_IsEnabled; }
        //    set
        //    {
        //        if (_Format_CutEnd_Milliseconds_IsEnabled == value)
        //        {
        //            return;
        //        }

        //        _Format_CutEnd_Milliseconds_IsEnabled = value;
        //        OnPropertyChanged("Format_CutEnd_Milliseconds_IsEnabled");
        //    }
        //}



        // --------------------------------------------------
        // Frame Start
        // --------------------------------------------------
        // Text
        private string _Format_FrameStart_Text;
        public string Format_FrameStart_Text
        {
            get { return _Format_FrameStart_Text; }
            set
            {
                if (_Format_FrameStart_Text == value)
                {
                    return;
                }

                _Format_FrameStart_Text = value;
                OnPropertyChanged("Format_FrameStart_Text");
            }
        }
        // Controls Enable
        private bool _Format_FrameStart_IsEnabled = true;
        public bool Format_FrameStart_IsEnabled
        {
            get { return _Format_FrameStart_IsEnabled; }
            set
            {
                if (_Format_FrameStart_IsEnabled == value)
                {
                    return;
                }

                _Format_FrameStart_IsEnabled = value;
                OnPropertyChanged("Format_FrameStart_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Frame End
        // --------------------------------------------------
        // Text
        private string _Format_FrameEnd_Text;
        public string Format_FrameEnd_Text
        {
            get { return _Format_FrameEnd_Text; }
            set
            {
                if (_Format_FrameEnd_Text == value)
                {
                    return;
                }

                _Format_FrameEnd_Text = value;
                OnPropertyChanged("Format_FrameEnd_Text");
            }
        }
        // Controls Enable
        private bool _Format_FrameEnd_IsEnabled = true;
        public bool Format_FrameEnd_IsEnabled
        {
            get { return _Format_FrameEnd_IsEnabled; }
            set
            {
                if (_Format_FrameEnd_IsEnabled == value)
                {
                    return;
                }

                _Format_FrameEnd_IsEnabled = value;
                OnPropertyChanged("Format_FrameEnd_IsEnabled");
            }
        }


        // --------------------------------------------------
        // YouTube
        // --------------------------------------------------
        // Items Source
        private List<string> _Format_YouTube_Items = new List<string>()
        {
            "Video + Audio",
            "Video Only",
            "Audio Only"
        };
        public List<string> Format_YouTube_Items
        {
            get { return _Format_YouTube_Items; }
            set
            {
                _Format_YouTube_Items = value;
                OnPropertyChanged("Format_YouTube_Items");
            }
        }

        // Selected Index
        private int _Format_YouTube_SelectedIndex { get; set; }
        public int Format_YouTube_SelectedIndex
        {
            get { return _Format_YouTube_SelectedIndex; }
            set
            {
                if (_Format_YouTube_SelectedIndex == value)
                {
                    return;
                }

                _Format_YouTube_SelectedIndex = value;
                OnPropertyChanged("Format_YouTube_SelectedIndex");
            }
        }

        // Selected Item
        private string _Format_YouTube_SelectedItem { get; set; }
        public string Format_YouTube_SelectedItem
        {
            get { return _Format_YouTube_SelectedItem; }
            set
            {
                if (_Format_YouTube_SelectedItem == value)
                {
                    return;
                }

                _Format_YouTube_SelectedItem = value;
                OnPropertyChanged("Format_YouTube_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Format_YouTube_IsEnabled = true;
        public bool Format_YouTube_IsEnabled
        {
            get { return _Format_YouTube_IsEnabled; }
            set
            {
                if (_Format_YouTube_IsEnabled == value)
                {
                    return;
                }

                _Format_YouTube_IsEnabled = value;
                OnPropertyChanged("Format_YouTube_IsEnabled");
            }
        }


        // --------------------------------------------------
        // YouTube_Quality
        // --------------------------------------------------
        // Items Source
        private List<string> _Format_YouTube_Quality_Items = new List<string>()
        {
            "best",
            "best 4K",
            "best 1080p",
            "best 720p",
            "best 480p",
            "worst"
        };
        public List<string> Format_YouTube_Quality_Items
        {
            get { return _Format_YouTube_Quality_Items; }
            set
            {
                _Format_YouTube_Quality_Items = value;
                OnPropertyChanged("Format_YouTube_Quality_Items");
            }
        }

        // Selected Index
        private int _Format_YouTube_Quality_SelectedIndex { get; set; }
        public int Format_YouTube_Quality_SelectedIndex
        {
            get { return _Format_YouTube_Quality_SelectedIndex; }
            set
            {
                if (_Format_YouTube_Quality_SelectedIndex == value)
                {
                    return;
                }

                _Format_YouTube_Quality_SelectedIndex = value;
                OnPropertyChanged("Format_YouTube_Quality_SelectedIndex");
            }
        }

        // Selected Item
        private string _Format_YouTube_Quality_SelectedItem { get; set; }
        public string Format_YouTube_Quality_SelectedItem
        {
            get { return _Format_YouTube_Quality_SelectedItem; }
            set
            {
                if (_Format_YouTube_Quality_SelectedItem == value)
                {
                    return;
                }

                _Format_YouTube_Quality_SelectedItem = value;
                OnPropertyChanged("Format_YouTube_Quality_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Format_YouTube_Quality_IsEnabled = true;
        public bool Format_YouTube_Quality_IsEnabled
        {
            get { return _Format_YouTube_Quality_IsEnabled; }
            set
            {
                if (_Format_YouTube_Quality_IsEnabled == value)
                {
                    return;
                }

                _Format_YouTube_Quality_IsEnabled = value;
                OnPropertyChanged("Format_YouTube_Quality_IsEnabled");
            }
        }



    }
}
