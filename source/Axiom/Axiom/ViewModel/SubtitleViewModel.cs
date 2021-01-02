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
    public class Subtitle : INotifyPropertyChanged
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

        // Subtitle View Model
        //public static SubtitleView vm = new SubtitleView();


        /// <summary>
        /// Subtitle View Model
        /// </summary>
        public Subtitle()
        {
            LoadControlsDefaults();
        }


        /// <summary>
        /// Load Controls Defaults
        /// </summary>
        public void LoadControlsDefaults()
        {
            Subtitle_Codec_IsEnabled = true;
            Subtitle_Codec_SelectedItem = "None";

            Subtitle_Stream_IsEnabled = true;
            Subtitle_Stream_SelectedItem = "none";

            Subtitle_ListView_IsEnabled = false;
            Subtitle_ListView_Opacity = 0.15;

            Subtitle_Metadata_Title_IsEnabled = false;
            Subtitle_Metadata_Title_Text = string.Empty;

            Subtitle_Metadata_Language_IsEnabled = false;
            Subtitle_Metadata_Language_SelectedItem = "none";

            Subtitle_Delay_IsEnabled = false;
            Subtitle_Delay_Text = string.Empty;
        }


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Subtitle
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // --------------------------------------------------
        // Controls Selected
        // --------------------------------------------------
        // Items Source
        public class Selected
        {
            public string Codec { get; set; }
            public string Stream { get; set; }
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
        //public class Checked
        //{
        //}


        // --------------------------------------------------
        // Controls Enabled / Disabled
        // --------------------------------------------------
        // Items Source
        public class Enabled
        {
            public bool? Codec { get; set; }
            public bool? Stream { get; set; }
        }

        // --------------------------------------------------
        // Subtitle Codec
        // --------------------------------------------------
        // Items Source
        public ObservableCollection<string> _Subtitle_Codec_Items = new ObservableCollection<string>();
        public ObservableCollection<string> Subtitle_Codec_Items
        {
            get { return _Subtitle_Codec_Items; }
            set
            {
                _Subtitle_Codec_Items = value;
                OnPropertyChanged("Subtitle_Codec_Items");
            }
        }

        // Codec and Parameters
        public class SubtitleCodec
        {
            public string Codec { get; set; }
            public string Parameters { get; set; }
        }

        // Codec Command
        public string Subtitle_Codec;

        // Selected Index
        public int _Subtitle_Codec_SelectedIndex;
        public int Subtitle_Codec_SelectedIndex
        {
            get { return _Subtitle_Codec_SelectedIndex; }
            set
            {
                if (_Subtitle_Codec_SelectedIndex == value)
                {
                    return;
                }

                _Subtitle_Codec_SelectedIndex = value;
                OnPropertyChanged("Subtitle_Codec_SelectedIndex");
            }
        }

        // Selected Item
        public string _Subtitle_Codec_SelectedItem;
        public string Subtitle_Codec_SelectedItem
        {
            get { return _Subtitle_Codec_SelectedItem; }
            set
            {
                if (_Subtitle_Codec_SelectedItem == value)
                {
                    return;
                }

                _Subtitle_Codec_SelectedItem = value;
                OnPropertyChanged("Subtitle_Codec_SelectedItem");
            }
        }

        // Controls Enable
        public bool? _Subtitle_Codec_IsEnabled;
        public bool? Subtitle_Codec_IsEnabled
        {
            get { return _Subtitle_Codec_IsEnabled; }
            set
            {
                if (_Subtitle_Codec_IsEnabled == value)
                {
                    return;
                }

                _Subtitle_Codec_IsEnabled = value;
                OnPropertyChanged("Subtitle_Codec_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Subtitle Stream
        // --------------------------------------------------
        // Items Source
        public ObservableCollection<string> _Subtitle_Stream_Items = new ObservableCollection<string>();
        public ObservableCollection<string> Subtitle_Stream_Items
        {
            get { return _Subtitle_Stream_Items; }
            set
            {
                _Subtitle_Stream_Items = value;
                OnPropertyChanged("Subtitle_Stream_Items");
            }
        }

        // Selected Index
        public int _Subtitle_Stream_SelectedIndex;
        public int Subtitle_Stream_SelectedIndex
        {
            get { return _Subtitle_Stream_SelectedIndex; }
            set
            {
                if (_Subtitle_Stream_SelectedIndex == value)
                {
                    return;
                }

                _Subtitle_Stream_SelectedIndex = value;
                OnPropertyChanged("Subtitle_Stream_SelectedIndex");
            }
        }

        // Selected Item
        public string _Subtitle_Stream_SelectedItem;
        public string Subtitle_Stream_SelectedItem
        {
            get { return _Subtitle_Stream_SelectedItem; }
            set
            {
                if (_Subtitle_Stream_SelectedItem == value)
                {
                    return;
                }

                _Subtitle_Stream_SelectedItem = value;
                OnPropertyChanged("Subtitle_Stream_SelectedItem");
            }
        }

        // Controls Enable
        public bool? _Subtitle_Stream_IsEnabled;
        public bool? Subtitle_Stream_IsEnabled
        {
            get { return _Subtitle_Stream_IsEnabled; }
            set
            {
                if (_Subtitle_Stream_IsEnabled == value)
                {
                    return;
                }

                _Subtitle_Stream_IsEnabled = value;
                OnPropertyChanged("Subtitle_Stream_IsEnabled");
            }
        }



        // --------------------------------------------------
        // Subtitle ListView
        // --------------------------------------------------
        // Items Source
        private ObservableCollection<string> _Subtitle_ListView_Items = new ObservableCollection<string>();
        public ObservableCollection<string> Subtitle_ListView_Items
        {
            get { return _Subtitle_ListView_Items; }
            set
            {
                _Subtitle_ListView_Items = value;
                OnPropertyChanged("Subtitle_ListView_Items");
            }
        }
        // Selected Items
        private List<string> _Subtitle_ListView_SelectedItems = new List<string>();
        public List<string> Subtitle_ListView_SelectedItems
        {
            get { return _Subtitle_ListView_SelectedItems; }
            set
            {
                _Subtitle_ListView_SelectedItems = value;
                OnPropertyChanged("Subtitle_ListView_SelectedItems");
            }
        }
        // Selected Index
        private int _Subtitle_ListView_SelectedIndex;
        public int Subtitle_ListView_SelectedIndex
        {
            get { return _Subtitle_ListView_SelectedIndex; }
            set
            {
                if (_Subtitle_ListView_SelectedIndex == value)
                {
                    return;
                }

                _Subtitle_ListView_SelectedIndex = value;
                OnPropertyChanged("Subtitle_ListView_SelectedIndex");
            }
        }
        private double _Subtitle_ListView_Opacity;
        public double Subtitle_ListView_Opacity
        {
            get { return _Subtitle_ListView_Opacity; }
            set
            {
                if (_Subtitle_ListView_Opacity == value)
                {
                    return;
                }

                _Subtitle_ListView_Opacity = value;
                OnPropertyChanged("Subtitle_ListView_Opacity");
            }
        }
        // Controls Enable
        public bool? _Subtitle_ListView_IsEnabled;
        public bool? Subtitle_ListView_IsEnabled
        {
            get { return _Subtitle_ListView_IsEnabled; }
            set
            {
                if (_Subtitle_ListView_IsEnabled == value)
                {
                    return;
                }

                _Subtitle_ListView_IsEnabled = value;
                OnPropertyChanged("Subtitle_ListView_IsEnabled");
            }
        }

        // -------------------------
        // Title Metadata
        // -------------------------
        // Text
        private string _Subtitle_Metadata_Title_Text;
        public string Subtitle_Metadata_Title_Text
        {
            get { return _Subtitle_Metadata_Title_Text; }
            set
            {
                if (_Subtitle_Metadata_Title_Text == value)
                {
                    return;
                }

                _Subtitle_Metadata_Title_Text = value;
                OnPropertyChanged("Subtitle_Metadata_Title_Text");
            }
        }
        // Enabled
        private bool _Subtitle_Metadata_Title_IsEnabled;
        public bool Subtitle_Metadata_Title_IsEnabled
        {
            get { return _Subtitle_Metadata_Title_IsEnabled; }
            set
            {
                if (_Subtitle_Metadata_Title_IsEnabled == value)
                {
                    return;
                }

                _Subtitle_Metadata_Title_IsEnabled = value;
                OnPropertyChanged("Subtitle_Metadata_Title_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Language Metadata
        // --------------------------------------------------
        public class Subtitle_Metadata_Language
        {
            public string Name { get; set; }
            public string Param { get; set; }
        }
        private ObservableCollection<Subtitle_Metadata_Language> _Subtitle_Metadata_Language_Items = new ObservableCollection<Subtitle_Metadata_Language>()
        {
            new Subtitle_Metadata_Language() { Name = "none",       Param = string.Empty  },
            new Subtitle_Metadata_Language() { Name = "Arabic",     Param = "ar" },
            new Subtitle_Metadata_Language() { Name = "Bengali",    Param = "bn" },
            new Subtitle_Metadata_Language() { Name = "Chinese",    Param = "chi" },
            new Subtitle_Metadata_Language() { Name = "Dutch",      Param = "nl" },
            new Subtitle_Metadata_Language() { Name = "English",    Param = "en" },
            new Subtitle_Metadata_Language() { Name = "Finnish",    Param = "fi" },
            new Subtitle_Metadata_Language() { Name = "French",     Param = "fr" },
            new Subtitle_Metadata_Language() { Name = "German",     Param = "de" },
            new Subtitle_Metadata_Language() { Name = "Hindi",      Param = "hi" },
            new Subtitle_Metadata_Language() { Name = "Italian",    Param = "it" },
            new Subtitle_Metadata_Language() { Name = "Japanese",   Param = "jp" },
            new Subtitle_Metadata_Language() { Name = "Korean",     Param = "kor" },
            new Subtitle_Metadata_Language() { Name = "Portuguese", Param = "pt" },
            new Subtitle_Metadata_Language() { Name = "Russian",    Param = "ru" },
            new Subtitle_Metadata_Language() { Name = "Spanish",    Param = "es" },
            new Subtitle_Metadata_Language() { Name = "Swedish",    Param = "sv" },
            new Subtitle_Metadata_Language() { Name = "Vietnamese", Param = "vi" },
        };
        public ObservableCollection<Subtitle_Metadata_Language> Subtitle_Metadata_Language_Items
        {
            get { return _Subtitle_Metadata_Language_Items; }
            set
            {
                _Subtitle_Metadata_Language_Items = value;
                OnPropertyChanged("Subtitle_Metadata_Language_Items");
            }
        }

        // Selected Index
        private int _Subtitle_Metadata_Language_SelectedIndex;
        public int Subtitle_Metadata_Language_SelectedIndex
        {
            get { return _Subtitle_Metadata_Language_SelectedIndex; }
            set
            {
                if (_Subtitle_Metadata_Language_SelectedIndex == value)
                {
                    return;
                }

                _Subtitle_Metadata_Language_SelectedIndex = value;
                OnPropertyChanged("Subtitle_Metadata_Language_SelectedIndex");
            }
        }

        // Selected Item
        private string _Subtitle_Metadata_Language_SelectedItem;
        public string Subtitle_Metadata_Language_SelectedItem
        {
            get { return _Subtitle_Metadata_Language_SelectedItem; }
            set
            {
                if (_Subtitle_Metadata_Language_SelectedItem == value)
                {
                    return;
                }

                _Subtitle_Metadata_Language_SelectedItem = value;
                OnPropertyChanged("Subtitle_Metadata_Language_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Subtitle_Metadata_Language_IsEnabled;
        public bool Subtitle_Metadata_Language_IsEnabled
        {
            get { return _Subtitle_Metadata_Language_IsEnabled; }
            set
            {
                if (_Subtitle_Metadata_Language_IsEnabled == value)
                {
                    return;
                }

                _Subtitle_Metadata_Language_IsEnabled = value;
                OnPropertyChanged("Subtitle_Metadata_Language_IsEnabled");
            }
        }


        // -------------------------
        // Delay
        // -------------------------
        // Text
        private string _Subtitle_Delay_Text;
        public string Subtitle_Delay_Text
        {
            get { return _Subtitle_Delay_Text; }
            set
            {
                if (_Subtitle_Delay_Text == value)
                {
                    return;
                }

                _Subtitle_Delay_Text = value;
                OnPropertyChanged("Subtitle_Delay_Text");
            }
        }
        // Enabled
        private bool _Subtitle_Delay_IsEnabled;
        public bool Subtitle_Delay_IsEnabled
        {
            get { return _Subtitle_Delay_IsEnabled; }
            set
            {
                if (_Subtitle_Delay_IsEnabled == value)
                {
                    return;
                }

                _Subtitle_Delay_IsEnabled = value;
                OnPropertyChanged("Subtitle_Delay_IsEnabled");
            }
        }

    }
}
