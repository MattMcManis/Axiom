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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace ViewModel
{
    public class Audio : INotifyPropertyChanged
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

        // Audio View Model
        //public static AudioView vm = new ViewModel.Audio();


        /// <summary>
        /// Audio View Model
        /// </summary>
        //public ViewModel.Audio()
        public Audio()
        {
            LoadControlsDefaults();
        }


        /// <summary>
        /// Load Controls Defaults
        /// </summary>
        public void LoadControlsDefaults()
        {
            Audio_Codec_IsEnabled = true;
            Audio_Codec_SelectedItem = "Vorbis";

            Audio_Stream_IsEnabled = true;
            Audio_Stream_SelectedItem = "1";

            Audio_Channel_IsEnabled = true;
            Audio_Channel_SelectedItem = "Source";

            Audio_Quality_IsEnabled = true;
            Audio_Quality_SelectedItem = "Auto";

            Audio_BitRate_IsEnabled = true;

            Audio_VBR_IsEnabled = true;

            Audio_CompressionLevel_IsEnabled = true;
            Audio_CompressionLevel_SelectedItem = "auto";

            Audio_SampleRate_IsEnabled = true;
            Audio_SampleRate_SelectedItem = "auto";

            Audio_BitDepth_IsEnabled = true;
            Audio_BitDepth_SelectedItem = "auto";

            Audio_Volume_IsEnabled = true;
            Audio_Volume_Text = "100";

            Audio_HardLimiter_IsEnabled = true;
            Audio_HardLimiter_Value = 0.0;

            Audio_ListView_IsEnabled = false;
            Audio_ListView_Opacity = 0.15;

            //Audio_Metadata_Title_IsEnabled = false;
            Audio_Metadata_Title_Text = string.Empty;

            //Audio_Metadata_Language_IsEnabled = false;
            Audio_Metadata_Language_SelectedItem = "none";
            //Generate.Audio.Metadata.languageList = new List<string>();
            //Generate.Audio.Metadata.languageList.Add(Audio_Metadata_Language_SelectedItem); // set the default at startup

            //Audio_Delay_IsEnabled = false;
            Audio_Delay_Text = string.Empty;
        }


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // --------------------------------------------------
        // Controls Selected
        // --------------------------------------------------
        // Items Source
        public class Selected
        {
            public string Channel { get; set; }
            public string Quality { get; set; }
            public string Stream { get; set; }
            public string CompressionLevel { get; set; }
            public string SampleRate { get; set; }
            public string BitDepth { get; set; }
        }


        // --------------------------------------------------
        // Controls Expanded / Collapsed
        // --------------------------------------------------
        // Items Source
        //public class Expanded
        //{
        //}


        // --------------------------------------------------
        // Controls Checked / Unchecked
        // --------------------------------------------------
        // Items Source
        public class Checked
        {
            public bool? VBR { get; set; }
        }


        // --------------------------------------------------
        // Controls Enabled / Disabled
        // --------------------------------------------------
        // Items Source
        public class Enabled
        {
            public bool? Codec { get; set; }
            public bool? Stream { get; set; }
            public bool? Channel { get; set; }
            public bool? Quality { get; set; }
            public bool? CompressionLevel { get; set; }
            public bool? VBR { get; set; }
            public bool? SampleRate { get; set; }
            public bool? BitDepth { get; set; }
            public bool? Volume { get; set; }
            public bool? HardLimiter { get; set; }

        }

        // --------------------------------------------------
        // Audio Codec
        // --------------------------------------------------
        // Items Source
        private ObservableCollection<string> _Audio_Codec_Items = new ObservableCollection<string>();
        public ObservableCollection<string> Audio_Codec_Items
        {
            get { return _Audio_Codec_Items; }
            set
            {
                _Audio_Codec_Items = value;
                OnPropertyChanged("Audio_Codec_Items");
            }
        }

        // Codec and Parameters
        public class AudioCodec
        {
            public string Codec { get; set; }
            public string Parameters { get; set; }
        }

        // Codec Command
        public string Audio_Codec;

        // Selected Index
        private int _Audio_Codec_SelectedIndex;
        public int Audio_Codec_SelectedIndex
        {
            get { return _Audio_Codec_SelectedIndex; }
            set
            {
                if (_Audio_Codec_SelectedIndex == value)
                {
                    return;
                }

                _Audio_Codec_SelectedIndex = value;
                OnPropertyChanged("Audio_Codec_SelectedIndex");
            }
        }

        // Selected Item
        private string _Audio_Codec_SelectedItem { get; set; }
        public string Audio_Codec_SelectedItem
        {
            get { return _Audio_Codec_SelectedItem; }
            set
            {
                if (_Audio_Codec_SelectedItem == value)
                {
                    return;
                }

                _Audio_Codec_SelectedItem = value;
                OnPropertyChanged("Audio_Codec_SelectedItem");
            }
        }

        // Controls Enable
        private bool? _Audio_Codec_IsEnabled;
        public bool? Audio_Codec_IsEnabled
        {
            get { return _Audio_Codec_IsEnabled; }
            set
            {
                if (_Audio_Codec_IsEnabled == value)
                {
                    return;
                }

                _Audio_Codec_IsEnabled = value;
                OnPropertyChanged("Audio_Codec_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Audio Stream
        // --------------------------------------------------
        // Items Source
        private ObservableCollection<string> _Audio_Stream_Items = new ObservableCollection<string>()
        {
            "all",
            "mux",
            "none",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            //"9",
            //"10",
            //"11",
            //"12",
        };
        public ObservableCollection<string> Audio_Stream_Items
        {
            get { return _Audio_Stream_Items; }
            set
            {
                _Audio_Stream_Items = value;
                OnPropertyChanged("Audio_Stream_Items");
            }
        }

        // Selected Index
        private int _Audio_Stream_SelectedIndex;
        public int Audio_Stream_SelectedIndex
        {
            get { return _Audio_Stream_SelectedIndex; }
            set
            {
                if (_Audio_Stream_SelectedIndex == value)
                {
                    return;
                }

                _Audio_Stream_SelectedIndex = value;
                OnPropertyChanged("Audio_Stream_SelectedIndex");
            }
        }

        // Selected Item
        private string _Audio_Stream_SelectedItem;
        public string Audio_Stream_SelectedItem
        {
            get { return _Audio_Stream_SelectedItem; }
            set
            {
                if (_Audio_Stream_SelectedItem == value)
                {
                    return;
                }

                _Audio_Stream_SelectedItem = value;
                OnPropertyChanged("Audio_Stream_SelectedItem");
            }
        }

        // Controls Enable
        private bool? _Audio_Stream_IsEnabled;
        public bool? Audio_Stream_IsEnabled
        {
            get { return _Audio_Stream_IsEnabled; }
            set
            {
                if (_Audio_Stream_IsEnabled == value)
                {
                    return;
                }

                _Audio_Stream_IsEnabled = value;
                OnPropertyChanged("Audio_Stream_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Audio Channel
        // --------------------------------------------------
        // Items Source
        private ObservableCollection<string> _Audio_Channel_Items = new ObservableCollection<string>();
        public ObservableCollection<string> Audio_Channel_Items
        {
            get { return _Audio_Channel_Items; }
            set
            {
                _Audio_Channel_Items = value;
                OnPropertyChanged("Audio_Channel_Items");
            }
        }

        // Selected Index
        private int _Audio_Channel_SelectedIndex;
        public int Audio_Channel_SelectedIndex
        {
            get { return _Audio_Channel_SelectedIndex; }
            set
            {
                if (_Audio_Channel_SelectedIndex == value)
                {
                    return;
                }

                _Audio_Channel_SelectedIndex = value;
                OnPropertyChanged("Audio_Channel_SelectedIndex");
            }
        }

        // Selected Item
        private string _Audio_Channel_SelectedItem;
        public string Audio_Channel_SelectedItem
        {
            get { return _Audio_Channel_SelectedItem; }
            set
            {
                if (_Audio_Channel_SelectedItem == value)
                {
                    return;
                }

                _Audio_Channel_SelectedItem = value;
                OnPropertyChanged("Audio_Channel_SelectedItem");
            }
        }

        // Controls Enable
        private bool? _Audio_Channel_IsEnabled;
        public bool? Audio_Channel_IsEnabled
        {
            get { return _Audio_Channel_IsEnabled; }
            set
            {
                if (_Audio_Channel_IsEnabled == value)
                {
                    return;
                }

                _Audio_Channel_IsEnabled = value;
                OnPropertyChanged("Audio_Channel_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Audio Quality
        // --------------------------------------------------
        // Items Source
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
        public ObservableCollection<AudioQuality> _Audio_Quality_Items = new ObservableCollection<AudioQuality>();
        public ObservableCollection<AudioQuality> Audio_Quality_Items
        {
            get { return _Audio_Quality_Items; }
            set
            {
                _Audio_Quality_Items = value;
                OnPropertyChanged("Audio_Quality_Items");
            }
        }

        // Selected Index
        private int _Audio_Quality_SelectedIndex;
        public int Audio_Quality_SelectedIndex
        {
            get { return _Audio_Quality_SelectedIndex; }
            set
            {
                if (_Audio_Quality_SelectedIndex == value)
                {
                    return;
                }

                _Audio_Quality_SelectedIndex = value;
                OnPropertyChanged("Audio_Quality_SelectedIndex");
            }
        }

        // Selected Item
        public string _Audio_Quality_SelectedItem;
        public string Audio_Quality_SelectedItem
        {
            get { return _Audio_Quality_SelectedItem; }
            set
            {
                if (_Audio_Quality_SelectedItem == value)
                {
                    return;
                }

                _Audio_Quality_SelectedItem = value;
                OnPropertyChanged("Audio_Quality_SelectedItem");
            }
        }

        // Controls Enable
        private bool? _Audio_Quality_IsEnabled;
        public bool? Audio_Quality_IsEnabled
        {
            get { return _Audio_Quality_IsEnabled; }
            set
            {
                if (_Audio_Quality_IsEnabled == value)
                {
                    return;
                }

                _Audio_Quality_IsEnabled = value;
                OnPropertyChanged("Audio_Quality_IsEnabled");
            }
        }


        // -------------------------
        // Audio BitRate
        // -------------------------
        // Text
        private string _Audio_BitRate_Text;
        public string Audio_BitRate_Text
        {
            get { return _Audio_BitRate_Text; }
            set
            {
                if (_Audio_BitRate_Text == value)
                {
                    return;
                }

                _Audio_BitRate_Text = value;
                OnPropertyChanged("Audio_BitRate_Text");
            }
        }
        // Enabled
        private bool? _Audio_BitRate_IsEnabled;
        public bool? Audio_BitRate_IsEnabled
        {
            get { return _Audio_BitRate_IsEnabled; }
            set
            {
                if (_Audio_BitRate_IsEnabled == value)
                {
                    return;
                }

                _Audio_BitRate_IsEnabled = value;
                OnPropertyChanged("Audio_BitRate_IsEnabled");
            }
        }


        // -------------------------
        // Audio VBR - Toggle
        // -------------------------
        // Checked
        private bool? _Audio_VBR_IsChecked;
        public bool? Audio_VBR_IsChecked
        {
            get { return _Audio_VBR_IsChecked; }
            set
            {
                if (_Audio_VBR_IsChecked != value)
                {
                    _Audio_VBR_IsChecked = value;
                    OnPropertyChanged("Audio_VBR_IsChecked");
                }
            }
        }

        // Enabled
        private bool? _Audio_VBR_IsEnabled;
        public bool? Audio_VBR_IsEnabled
        {
            get { return _Audio_VBR_IsEnabled; }
            set
            {
                if (_Audio_VBR_IsEnabled == value)
                {
                    return;
                }

                _Audio_VBR_IsEnabled = value;
                OnPropertyChanged("Audio_VBR_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Audio Compression Level
        // --------------------------------------------------
        // Items Source
        private ObservableCollection<string> _Audio_CompressionLevel_Items = new ObservableCollection<string>();
        public ObservableCollection<string> Audio_CompressionLevel_Items
        {
            get { return _Audio_CompressionLevel_Items; }
            set
            {
                _Audio_CompressionLevel_Items = value;
                OnPropertyChanged("Audio_CompressionLevel_Items");
            }
        }

        // Selected Index
        private int _Audio_CompressionLevel_SelectedIndex;
        public int Audio_CompressionLevel_SelectedIndex
        {
            get { return _Audio_CompressionLevel_SelectedIndex; }
            set
            {
                if (_Audio_CompressionLevel_SelectedIndex == value)
                {
                    return;
                }

                _Audio_CompressionLevel_SelectedIndex = value;
                OnPropertyChanged("Audio_CompressionLevel_SelectedIndex");
            }
        }

        // Selected Item
        private string _Audio_CompressionLevel_SelectedItem;
        public string Audio_CompressionLevel_SelectedItem
        {
            get { return _Audio_CompressionLevel_SelectedItem; }
            set
            {
                if (_Audio_CompressionLevel_SelectedItem == value)
                {
                    return;
                }

                _Audio_CompressionLevel_SelectedItem = value;
                OnPropertyChanged("Audio_CompressionLevel_SelectedItem");

                //var previousItem = _Audio_CompressionLevel_SelectedItem;
                //_Audio_CompressionLevel_SelectedItem = value;
                //OnPropertyChanged("Audio_CompressionLevel_SelectedItem");

                //if (previousItem != value)
                //{
                //    //AudioControls.AutoCopyAudioCodec(/*this*/);
                //}

                //if (_Audio_CompressionLevel_SelectedItem == value)
                //{
                //    return;
                //}

                //_Audio_CompressionLevel_SelectedItem = value;
                //OnPropertyChanged("Audio_CompressionLevel_SelectedItem");
            }
        }

        // Controls Enable
        private bool? _Audio_CompressionLevel_IsEnabled;
        public bool? Audio_CompressionLevel_IsEnabled
        {
            get { return _Audio_CompressionLevel_IsEnabled; }
            set
            {
                if (_Audio_CompressionLevel_IsEnabled == value)
                {
                    return;
                }

                _Audio_CompressionLevel_IsEnabled = value;
                OnPropertyChanged("Audio_CompressionLevel_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Audio Sample Rate
        // --------------------------------------------------
        // Items Source
        public class AudioSampleRate
        {
            public string Name { get; set; }
            public string Frequency { get; set; }
        }
        public ObservableCollection<AudioSampleRate> _Audio_SampleRate_Items = new ObservableCollection<AudioSampleRate>();
        public ObservableCollection<AudioSampleRate> Audio_SampleRate_Items
        {
            get { return _Audio_SampleRate_Items; }
            set
            {
                _Audio_SampleRate_Items = value;
                OnPropertyChanged("Audio_SampleRate_Items");
            }
        }

        // Selected Index
        private int _Audio_SampleRate_SelectedIndex;
        public int Audio_SampleRate_SelectedIndex
        {
            get { return _Audio_SampleRate_SelectedIndex; }
            set
            {
                if (_Audio_SampleRate_SelectedIndex == value)
                {
                    return;
                }

                _Audio_SampleRate_SelectedIndex = value;
                OnPropertyChanged("Audio_SampleRate_SelectedIndex");
            }
        }

        // Selected Item
        private string _Audio_SampleRate_SelectedItem;
        public string Audio_SampleRate_SelectedItem
        {
            get { return _Audio_SampleRate_SelectedItem; }
            set
            {
                if (_Audio_SampleRate_SelectedItem == value)
                {
                    return;
                }

                _Audio_SampleRate_SelectedItem = value;
                OnPropertyChanged("Audio_SampleRate_SelectedItem");
            }
        }

        // Controls Enable
        private bool? _Audio_SampleRate_IsEnabled;
        public bool? Audio_SampleRate_IsEnabled
        {
            get { return _Audio_SampleRate_IsEnabled; }
            set
            {
                if (_Audio_SampleRate_IsEnabled == value)
                {
                    return;
                }

                _Audio_SampleRate_IsEnabled = value;
                OnPropertyChanged("Audio_SampleRate_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Audio Bit Depth
        // --------------------------------------------------
        // Items Source
        public class AudioBitDepth
        {
            public string Name { get; set; }
            public string Depth { get; set; }
        }
        public ObservableCollection<AudioBitDepth> _Audio_BitDepth_Items = new ObservableCollection<AudioBitDepth>();
        public ObservableCollection<AudioBitDepth> Audio_BitDepth_Items
        {
            get { return _Audio_BitDepth_Items; }
            set
            {
                _Audio_BitDepth_Items = value;
                OnPropertyChanged("Audio_BitDepth_Items");
            }
        }

        // Selected Index
        private int _Audio_BitDepth_SelectedIndex;
        public int Audio_BitDepth_SelectedIndex
        {
            get { return _Audio_BitDepth_SelectedIndex; }
            set
            {
                if (_Audio_BitDepth_SelectedIndex == value)
                {
                    return;
                }

                _Audio_BitDepth_SelectedIndex = value;
                OnPropertyChanged("Audio_BitDepth_SelectedIndex");
            }
        }

        // Selected Item
        private string _Audio_BitDepth_SelectedItem;
        public string Audio_BitDepth_SelectedItem
        {
            get { return _Audio_BitDepth_SelectedItem; }
            set
            {
                if (_Audio_BitDepth_SelectedItem == value)
                {
                    return;
                }

                _Audio_BitDepth_SelectedItem = value;
                OnPropertyChanged("Audio_BitDepth_SelectedItem");
            }
        }

        // Controls Enable
        private bool? _Audio_BitDepth_IsEnabled;
        public bool? Audio_BitDepth_IsEnabled
        {
            get { return _Audio_BitDepth_IsEnabled; }
            set
            {
                if (_Audio_BitDepth_IsEnabled == value)
                {
                    return;
                }

                _Audio_BitDepth_IsEnabled = value;
                OnPropertyChanged("Audio_BitDepth_IsEnabled");
            }
        }


        // -------------------------
        // Volume
        // -------------------------
        // Text
        private string _Audio_Volume_Text;
        public string Audio_Volume_Text
        {
            get { return _Audio_Volume_Text; }
            set
            {
                if (_Audio_Volume_Text == value)
                {
                    return;
                }

                _Audio_Volume_Text = value;
                OnPropertyChanged("Audio_Volume_Text");
            }
        }
        // Enabled
        private bool? _Audio_Volume_IsEnabled;
        public bool? Audio_Volume_IsEnabled
        {
            get { return _Audio_Volume_IsEnabled; }
            set
            {
                if (_Audio_Volume_IsEnabled == value)
                {
                    return;
                }

                _Audio_Volume_IsEnabled = value;
                OnPropertyChanged("Audio_Volume_IsEnabled");
            }
        }


        // -------------------------
        // Hard Limiter
        // -------------------------
        // Value
        private double _Audio_HardLimiter_Value;
        public double Audio_HardLimiter_Value
        {
            get { return _Audio_HardLimiter_Value; }
            set
            {
                if (_Audio_HardLimiter_Value == value)
                {
                    return;
                }

                _Audio_HardLimiter_Value = value;
                OnPropertyChanged("Audio_HardLimiter_Value");
            }
        }

        // Enabled
        private bool? _Audio_HardLimiter_IsEnabled;
        public bool? Audio_HardLimiter_IsEnabled
        {
            get { return _Audio_HardLimiter_IsEnabled; }
            set
            {
                if (_Audio_HardLimiter_IsEnabled == value)
                {
                    return;
                }

                _Audio_HardLimiter_IsEnabled = value;
                OnPropertyChanged("Audio_HardLimiter_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Audio ListView
        // --------------------------------------------------
        // Items Source
        private ObservableCollection<string> _Audio_ListView_Items = new ObservableCollection<string>();
        public ObservableCollection<string> Audio_ListView_Items
        {
            get { return _Audio_ListView_Items; }
            set
            {
                _Audio_ListView_Items = value;
                OnPropertyChanged("Audio_ListView_Items");
            }
        }
        // Selected Items
        private List<string> _Audio_ListView_SelectedItems = new List<string>();
        public List<string> Audio_ListView_SelectedItems
        {
            get { return _Audio_ListView_SelectedItems; }
            set
            {
                _Audio_ListView_SelectedItems = value;
                OnPropertyChanged("Audio_ListView_SelectedItems");
            }
        }
        // Selected Index
        private int _Audio_ListView_SelectedIndex;
        public int Audio_ListView_SelectedIndex
        {
            get { return _Audio_ListView_SelectedIndex; }
            set
            {
                if (_Audio_ListView_SelectedIndex == value)
                {
                    return;
                }

                _Audio_ListView_SelectedIndex = value;
                OnPropertyChanged("Audio_ListView_SelectedIndex");
            }
        }
        private double _Audio_ListView_Opacity;
        public double Audio_ListView_Opacity
        {
            get { return _Audio_ListView_Opacity; }
            set
            {
                if (_Audio_ListView_Opacity == value)
                {
                    return;
                }

                _Audio_ListView_Opacity = value;
                OnPropertyChanged("Audio_ListView_Opacity");
            }
        }
        // Controls Enable
        public bool? _Audio_ListView_IsEnabled;
        public bool? Audio_ListView_IsEnabled
        {
            get { return _Audio_ListView_IsEnabled; }
            set
            {
                if (_Audio_ListView_IsEnabled == value)
                {
                    return;
                }

                _Audio_ListView_IsEnabled = value;
                OnPropertyChanged("Audio_ListView_IsEnabled");
            }
        }


        // -------------------------
        // Title Metadata
        // -------------------------
        // Text
        private string _Audio_Metadata_Title_Text;
        public string Audio_Metadata_Title_Text
        {
            get { return _Audio_Metadata_Title_Text; }
            set
            {
                if (_Audio_Metadata_Title_Text == value)
                {
                    return;
                }

                _Audio_Metadata_Title_Text = value;
                OnPropertyChanged("Audio_Metadata_Title_Text");
            }
        }
        // Enabled
        private bool _Audio_Metadata_Title_IsEnabled;
        public bool Audio_Metadata_Title_IsEnabled
        {
            get { return _Audio_Metadata_Title_IsEnabled; }
            set
            {
                if (_Audio_Metadata_Title_IsEnabled == value)
                {
                    return;
                }

                _Audio_Metadata_Title_IsEnabled = value;
                OnPropertyChanged("Audio_Metadata_Title_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Language Metadata
        // --------------------------------------------------
        public class Audio_Metadata_Language
        {
            public string Name { get; set; }
            public string Param { get; set; }
        }
        private ObservableCollection<Audio_Metadata_Language> _Audio_Metadata_Language_Items = new ObservableCollection<Audio_Metadata_Language>()
        {
            new Audio_Metadata_Language() { Name = "none",       Param = string.Empty  },
            new Audio_Metadata_Language() { Name = "Arabic",     Param = "ar" },
            new Audio_Metadata_Language() { Name = "Bengali",    Param = "bn" },
            new Audio_Metadata_Language() { Name = "Chinese",    Param = "chi" },
            new Audio_Metadata_Language() { Name = "Dutch",      Param = "nl" },
            new Audio_Metadata_Language() { Name = "English",    Param = "en" },
            new Audio_Metadata_Language() { Name = "Finnish",    Param = "fi" },
            new Audio_Metadata_Language() { Name = "French",     Param = "fr" },
            new Audio_Metadata_Language() { Name = "German",     Param = "de" },
            new Audio_Metadata_Language() { Name = "Hindi",      Param = "hi" },
            new Audio_Metadata_Language() { Name = "Italian",    Param = "it" },
            new Audio_Metadata_Language() { Name = "Japanese",   Param = "jp" },
            new Audio_Metadata_Language() { Name = "Korean",     Param = "kor" },
            new Audio_Metadata_Language() { Name = "Portuguese", Param = "pt" },
            new Audio_Metadata_Language() { Name = "Russian",    Param = "ru" },
            new Audio_Metadata_Language() { Name = "Spanish",    Param = "es" },
            new Audio_Metadata_Language() { Name = "Swedish",    Param = "sv" },
            new Audio_Metadata_Language() { Name = "Vietnamese", Param = "vi" },
        };
        public ObservableCollection<Audio_Metadata_Language> Audio_Metadata_Language_Items
        {
            get { return _Audio_Metadata_Language_Items; }
            set
            {
                _Audio_Metadata_Language_Items = value;
                OnPropertyChanged("Audio_Metadata_Language_Items");
            }
        }

        // Selected Index
        private int _Audio_Metadata_Language_SelectedIndex;
        public int Audio_Metadata_Language_SelectedIndex
        {
            get { return _Audio_Metadata_Language_SelectedIndex; }
            set
            {
                if (_Audio_Metadata_Language_SelectedIndex == value)
                {
                    return;
                }

                _Audio_Metadata_Language_SelectedIndex = value;
                OnPropertyChanged("Audio_Metadata_Language_SelectedIndex");
            }
        }

        // Selected Item
        private string _Audio_Metadata_Language_SelectedItem;
        public string Audio_Metadata_Language_SelectedItem
        {
            get { return _Audio_Metadata_Language_SelectedItem; }
            set
            {
                if (_Audio_Metadata_Language_SelectedItem == value)
                {
                    return;
                }

                _Audio_Metadata_Language_SelectedItem = value;
                OnPropertyChanged("Audio_Metadata_Language_SelectedItem");
            }
        }

        // Controls Enable
        private bool? _Audio_Metadata_Language_IsEnabled;
        public bool? Audio_Metadata_Language_IsEnabled
        {
            get { return _Audio_Metadata_Language_IsEnabled; }
            set
            {
                if (_Audio_Metadata_Language_IsEnabled == value)
                {
                    return;
                }

                _Audio_Metadata_Language_IsEnabled = value;
                OnPropertyChanged("Audio_Metadata_Language_IsEnabled");
            }
        }


        // -------------------------
        // Delay
        // -------------------------
        // Text
        private string _Audio_Delay_Text;
        public string Audio_Delay_Text
        {
            get { return _Audio_Delay_Text; }
            set
            {
                if (_Audio_Delay_Text == value)
                {
                    return;
                }

                _Audio_Delay_Text = value;
                OnPropertyChanged("Audio_Delay_Text");
            }
        }
        // Enabled
        private bool _Audio_Delay_IsEnabled;
        public bool Audio_Delay_IsEnabled
        {
            get { return _Audio_Delay_IsEnabled; }
            set
            {
                if (_Audio_Delay_IsEnabled == value)
                {
                    return;
                }

                _Audio_Delay_IsEnabled = value;
                OnPropertyChanged("Audio_Delay_IsEnabled");
            }
        }

    }
}
