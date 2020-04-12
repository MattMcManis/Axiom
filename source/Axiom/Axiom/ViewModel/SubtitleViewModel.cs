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
using System.Collections.ObjectModel;
using System.ComponentModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class SubtitleViewModel : INotifyPropertyChanged
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
        public SubtitleViewModel()
        {
            LoadControlsDefaults();
        }


        /// <summary>
        /// Load Controls Defaults
        /// </summary>
        public void LoadControlsDefaults()
        {
            Subtitle_Codec_SelectedItem = "None";
            Subtitle_Stream_SelectedItem = "none";
        }


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Subtitle
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // --------------------------------------------------
        // Subtitle Codec
        // --------------------------------------------------
        // Items Source
        public List<string> _Subtitle_Codec_Items = new List<string>();
        public List<string> Subtitle_Codec_Items
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
        public int _Subtitle_Codec_SelectedIndex { get; set; }
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
        public string _Subtitle_Codec_SelectedItem { get; set; }
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
        public bool _Subtitle_Codec_IsEnabled = true;
        public bool Subtitle_Codec_IsEnabled
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
        public List<string> _Subtitle_Stream_Items = new List<string>();
        public List<string> Subtitle_Stream_Items
        {
            get { return _Subtitle_Stream_Items; }
            set
            {
                _Subtitle_Stream_Items = value;
                OnPropertyChanged("Subtitle_Stream_Items");
            }
        }

        // Selected Index
        public int _Subtitle_Stream_SelectedIndex { get; set; }
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
        public string _Subtitle_Stream_SelectedItem { get; set; }
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
        public bool _Subtitle_Stream_IsEnabled = true;
        public bool Subtitle_Stream_IsEnabled
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
        private int _Subtitle_ListView_SelectedIndex { get; set; }
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
        private double _Subtitle_ListView_Opacity { get; set; }
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
        public bool _Subtitle_ListView_IsEnabled = true;
        public bool Subtitle_ListView_IsEnabled
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



    }
}
