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
    public class FilterAudioViewModel : INotifyPropertyChanged
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

        // Filter Audio View Model
        //public static FilterAudioView vm = new FilterAudioView();


        /// <summary>
        /// Filter View Model
        /// </summary>
        public FilterAudioViewModel()
        {
            LoadControlsDefaults();
        }


        /// <summary>
        /// Load Controls Defaults
        /// </summary>
        public void LoadControlsDefaults()
        {
            LoadFilterAudioDefaults();
        }


        /// <summary>
        /// Load Filter Video Defaults
        /// </summary>
        public void LoadFilterAudioDefaults()
        {
            FilterAudio_Lowpass_SelectedItem = "disabled";
            FilterAudio_Highpass_SelectedItem = "disabled";
            FilterAudio_Headphones_SelectedItem = "disabled";
        }


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Filter Audio
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // -------------------------
        // Lowpass
        // -------------------------
        // Items
        public List<string> _FilterAudio_Lowpass_Items = new List<string>()
        {
            "disabled",
            "enabled"
        };
        public List<string> FilterAudio_Lowpass_Items
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
        public List<string> _FilterAudio_Highpass_Items = new List<string>()
        {
            "disabled",
            "enabled"
        };
        public List<string> FilterAudio_Highpass_Items
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
        public List<string> _FilterAudio_Headphones_Items = new List<string>()
        {
            "disabled",
            "enabled"
        };
        public List<string> FilterAudio_Headphones_Items
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
