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
using System.Windows.Media;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace ViewModel
{
    public class FilterVideo : INotifyPropertyChanged
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

        // Filter Video View Model
        //public static FilterVideoView vm = new FilterVideoView();


        /// <summary>
        /// Filter View Model
        /// </summary>
        public FilterVideo()
        {
            LoadControlsDefaults();
        }


        /// <summary>
        /// Load Controls Defaults
        /// </summary>
        public void LoadControlsDefaults()
        {
            LoadFilterVideoDefaults();
        }


        /// <summary>
        /// Load Filter Video Defaults
        /// </summary>
        public void LoadFilterVideoDefaults()
        {
            // Display
            FilterVideo_DropFrames_IsEnabled = true;
            FilterVideo_DropFrames_SelectedItem = "disabled";

            // Fix
            FilterVideo_Deinterlace_IsEnabled = true;
            FilterVideo_Deinterlace_SelectedItem = "disabled";
            FilterVideo_Deblock_IsEnabled = true;
            FilterVideo_Deblock_SelectedItem = "disabled";
            FilterVideo_Deflicker_IsEnabled = true;
            FilterVideo_Deflicker_SelectedItem = "disabled";
            FilterVideo_Denoise_IsEnabled = true;
            FilterVideo_Denoise_SelectedItem = "disabled";
            FilterVideo_Deband_IsEnabled = true;
            FilterVideo_Deband_SelectedItem = "disabled";
            FilterVideo_Deshake_IsEnabled = true;
            FilterVideo_Deshake_SelectedItem = "disabled";
            FilterVideo_Dejudder_IsEnabled = true;
            FilterVideo_Dejudder_SelectedItem = "disabled";

            // Transpose
            FilterVideo_Flip_IsEnabled = true;
            FilterVideo_Flip_SelectedItem = "disabled";
            FilterVideo_Rotate_IsEnabled = true;
            FilterVideo_Rotate_SelectedItem = "disabled";

            // EQ
            FilterVideo_EQ_Brightness_IsEnabled = true;
            FilterVideo_EQ_Brightness_Value = 0;
            FilterVideo_EQ_Contrast_IsEnabled = true;
            FilterVideo_EQ_Contrast_Value = 0;
            FilterVideo_EQ_Saturation_IsEnabled = true;
            FilterVideo_EQ_Saturation_Value = 0;
            FilterVideo_EQ_Gamma_IsEnabled = true;
            FilterVideo_EQ_Gamma_Value = 0;

            // Selective Color
            FilterVideo_SelectiveColor_IsEnabled = true;
            FilterVideo_SelectiveColor_SelectedIndex = 0;
            FilterVideo_SelectiveColor_Correction_Method_IsEnabled = true;
            FilterVideo_SelectiveColor_Correction_Method_SelectedItem = "relative";

            // Reds
            FilterVideo_SelectiveColor_Reds_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Reds_Cyan_IsEnabled = true;
            FilterVideo_SelectiveColor_Reds_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Reds_Magenta_IsEnabled = true;
            FilterVideo_SelectiveColor_Reds_Yellow_Value = 0;
            FilterVideo_SelectiveColor_Reds_Yellow_IsEnabled = true;
            // Yellows
            FilterVideo_SelectiveColor_Yellows_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Yellows_Cyan_IsEnabled = true;
            FilterVideo_SelectiveColor_Yellows_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Yellows_Magenta_IsEnabled = true;
            FilterVideo_SelectiveColor_Yellows_Yellow_Value = 0;
            FilterVideo_SelectiveColor_Yellows_Yellow_IsEnabled = true;
            // Greens
            FilterVideo_SelectiveColor_Greens_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Greens_Cyan_IsEnabled = true;
            FilterVideo_SelectiveColor_Greens_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Greens_Magenta_IsEnabled = true;
            FilterVideo_SelectiveColor_Greens_Yellow_Value = 0;
            FilterVideo_SelectiveColor_Greens_Yellow_IsEnabled = true;
            // Cyans
            FilterVideo_SelectiveColor_Cyans_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Cyans_Cyan_IsEnabled = true;
            FilterVideo_SelectiveColor_Cyans_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Cyans_Magenta_IsEnabled = true;
            FilterVideo_SelectiveColor_Cyans_Yellow_Value = 0;
            FilterVideo_SelectiveColor_Cyans_Yellow_IsEnabled = true;
            // Blues
            FilterVideo_SelectiveColor_Blues_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Blues_Cyan_IsEnabled = true;
            FilterVideo_SelectiveColor_Blues_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Blues_Magenta_IsEnabled = true;
            FilterVideo_SelectiveColor_Blues_Yellow_Value = 0;
            FilterVideo_SelectiveColor_Blues_Yellow_IsEnabled = true;
            // Magentas
            FilterVideo_SelectiveColor_Magentas_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Magentas_Cyan_IsEnabled = true;
            FilterVideo_SelectiveColor_Magentas_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Magentas_Magenta_IsEnabled = true;
            FilterVideo_SelectiveColor_Magentas_Yellow_Value = 0;
            FilterVideo_SelectiveColor_Magentas_Yellow_IsEnabled = true;
            // Whites
            FilterVideo_SelectiveColor_Whites_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Whites_Cyan_IsEnabled = true;
            FilterVideo_SelectiveColor_Whites_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Whites_Magenta_IsEnabled = true;
            FilterVideo_SelectiveColor_Whites_Yellow_Value = 0;
            FilterVideo_SelectiveColor_Whites_Yellow_IsEnabled = true;
            // Neutrals
            FilterVideo_SelectiveColor_Neutrals_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Neutrals_Cyan_IsEnabled = true;
            FilterVideo_SelectiveColor_Neutrals_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Neutrals_Magenta_IsEnabled = true;
            FilterVideo_SelectiveColor_Neutrals_Yellow_Value = 0;
            FilterVideo_SelectiveColor_Neutrals_Yellow_IsEnabled = true;
            // Blacks 
            FilterVideo_SelectiveColor_Blacks_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Blacks_Cyan_IsEnabled = true;
            FilterVideo_SelectiveColor_Blacks_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Blacks_Magenta_IsEnabled = true;
            FilterVideo_SelectiveColor_Blacks_Yellow_Value = 0;
            FilterVideo_SelectiveColor_Blacks_Yellow_IsEnabled = true;

        }


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Filter Video
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // -------------------------
        // Drop Frames
        // -------------------------
        // Items
        public ObservableCollection<string> _FilterVideo_DropFrames_Items = new ObservableCollection<string>()
        {
            "disabled",
            "enabled",
            //"max",
            //"hi",
            //"lo",
            //"frac"
        };
        public ObservableCollection<string> FilterVideo_DropFrames_Items
        {
            get { return _FilterVideo_DropFrames_Items; }
            set { _FilterVideo_DropFrames_Items = value; }
        }

        // Selected Item
        private string _FilterVideo_DropFrames_SelectedItem;
        public string FilterVideo_DropFrames_SelectedItem
        {
            get { return _FilterVideo_DropFrames_SelectedItem; }
            set
            {
                if (_FilterVideo_DropFrames_SelectedItem == value)
                {
                    return;
                }

                _FilterVideo_DropFrames_SelectedItem = value;
                OnPropertyChanged("FilterVideo_DropFrames_SelectedItem");
            }
        }
        // Controls Enable
        private bool _FilterVideo_DropFrames_IsEnabled = true;
        public bool FilterVideo_DropFrames_IsEnabled
        {
            get { return _FilterVideo_DropFrames_IsEnabled; }
            set
            {
                if (_FilterVideo_DropFrames_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_DropFrames_IsEnabled = value;
                OnPropertyChanged("FilterVideo_DropFrames_IsEnabled");
            }
        }


        // -------------------------
        // Deband
        // -------------------------
        // Items
        public ObservableCollection<string> _FilterVideo_Deband_Items = new ObservableCollection<string>()
        {
            "disabled",
            "enabled"
        };
        public ObservableCollection<string> FilterVideo_Deband_Items
        {
            get { return _FilterVideo_Deband_Items; }
            set { _FilterVideo_Deband_Items = value; }
        }

        // Selected Item
        private string _FilterVideo_Deband_SelectedItem;
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
        // Controls Enable
        private bool _FilterVideo_Deband_IsEnabled = true;
        public bool FilterVideo_Deband_IsEnabled
        {
            get { return _FilterVideo_Deband_IsEnabled; }
            set
            {
                if (_FilterVideo_Deband_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_Deband_IsEnabled = value;
                OnPropertyChanged("FilterVideo_Deband_IsEnabled");
            }
        }

        // -------------------------
        // Deshake
        // -------------------------
        // Items
        public ObservableCollection<string> _FilterVideo_Deshake_Items = new ObservableCollection<string>()
        {
            "disabled",
            "enabled"
        };
        public ObservableCollection<string> FilterVideo_Deshake_Items
        {
            get { return _FilterVideo_Deshake_Items; }
            set { _FilterVideo_Deshake_Items = value; }
        }

        // Selected Item
        private string _FilterVideo_Deshake_SelectedItem;
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
        // Controls Enable
        private bool _FilterVideo_Deshake_IsEnabled = true;
        public bool FilterVideo_Deshake_IsEnabled
        {
            get { return _FilterVideo_Deshake_IsEnabled; }
            set
            {
                if (_FilterVideo_Deshake_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_Deshake_IsEnabled = value;
                OnPropertyChanged("FilterVideo_Deshake_IsEnabled");
            }
        }

        // -------------------------
        // Deflicker
        // -------------------------
        // Items
        public ObservableCollection<string> _FilterVideo_Deflicker_Items = new ObservableCollection<string>()
        {
            "disabled",
            "enabled"
        };
        public ObservableCollection<string> FilterVideo_Deflicker_Items
        {
            get { return _FilterVideo_Deflicker_Items; }
            set { _FilterVideo_Deflicker_Items = value; }
        }
        // Selected Item
        private string _FilterVideo_Deflicker_SelectedItem;
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
        // Controls Enable
        private bool _FilterVideo_Deflicker_IsEnabled = true;
        public bool FilterVideo_Deflicker_IsEnabled
        {
            get { return _FilterVideo_Deflicker_IsEnabled; }
            set
            {
                if (_FilterVideo_Deflicker_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_Deflicker_IsEnabled = value;
                OnPropertyChanged("FilterVideo_Deflicker_IsEnabled");
            }
        }

        // -------------------------
        // Dejudder
        // -------------------------
        // Items
        public ObservableCollection<string> _FilterVideo_Dejudder_Items = new ObservableCollection<string>()
        {
            "disabled",
            "enabled"
        };
        public ObservableCollection<string> FilterVideo_Dejudder_Items
        {
            get { return _FilterVideo_Dejudder_Items; }
            set { _FilterVideo_Dejudder_Items = value; }
        }
        // Selected Item
        private string _FilterVideo_Dejudder_SelectedItem;
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
        // Controls Enable
        private bool _FilterVideo_Dejudder_IsEnabled = true;
        public bool FilterVideo_Dejudder_IsEnabled
        {
            get { return _FilterVideo_Dejudder_IsEnabled; }
            set
            {
                if (_FilterVideo_Dejudder_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_Dejudder_IsEnabled = value;
                OnPropertyChanged("FilterVideo_Dejudder_IsEnabled");
            }
        }

        // -------------------------
        // Denoise
        // -------------------------
        // Items
        public ObservableCollection<string> _FilterVideo_Denoise_Items = new ObservableCollection<string>()
        {
            "disabled",
            "default",

            // nlmeans
            "nlmeans light",
            "nlmeans medium",
            "nlmeans strong",

            // hqdn3d
            "hqdn3d light",
            "hqdn3d medium",
            "hqdn3d strong",

            // vaguedenoiser
            "vaguedenoiser light",
            "vaguedenoiser medium",
            "vaguedenoiser strong",

            // removegrain
            "removegrain light",
            "removegrain medium",
            "removegrain strong",

            //"light",
            //"medium",
            //"heavy",
        };
        public ObservableCollection<string> FilterVideo_Denoise_Items
        {
            get { return _FilterVideo_Denoise_Items; }
            set { _FilterVideo_Denoise_Items = value; }
        }
        // Selected Item
        private string _FilterVideo_Denoise_SelectedItem;
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
        // Controls Enable
        private bool _FilterVideo_Denoise_IsEnabled = true;
        public bool FilterVideo_Denoise_IsEnabled
        {
            get { return _FilterVideo_Denoise_IsEnabled; }
            set
            {
                if (_FilterVideo_Denoise_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_Denoise_IsEnabled = value;
                OnPropertyChanged("FilterVideo_Denoise_IsEnabled");
            }
        }

        // -------------------------
        // Deinterlace
        // -------------------------
        // Items
        public ObservableCollection<string> _FilterVideo_Deinterlace_Items = new ObservableCollection<string>()
        {
            "disabled",
            "default",
            "yes",
            "frame",
            "field",
            "frame nospatial",
            "field nospatial",
            //"cuda frame",
            //"cuda field"
        };
        public ObservableCollection<string> FilterVideo_Deinterlace_Items
        {
            get { return _FilterVideo_Deinterlace_Items; }
            set { _FilterVideo_Deinterlace_Items = value; }
        }
        // Selected Item
        private string _FilterVideo_Deinterlace_SelectedItem;
        public string FilterVideo_Deinterlace_SelectedItem
        {
            get { return _FilterVideo_Deinterlace_SelectedItem; }
            set
            {
                if (_FilterVideo_Deinterlace_SelectedItem == value)
                {
                    return;
                }

                _FilterVideo_Deinterlace_SelectedItem = value;
                OnPropertyChanged("FilterVideo_Deinterlace_SelectedItem");
            }
        }
        // Controls Enable
        private bool _FilterVideo_Deinterlace_IsEnabled = true;
        public bool FilterVideo_Deinterlace_IsEnabled
        {
            get { return _FilterVideo_Deinterlace_IsEnabled; }
            set
            {
                if (_FilterVideo_Deinterlace_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_Deinterlace_IsEnabled = value;
                OnPropertyChanged("FilterVideo_Deinterlace_IsEnabled");
            }
        }


        // -------------------------
        // Deblock
        // -------------------------
        // Items
        public ObservableCollection<string> _FilterVideo_Deblock_Items = new ObservableCollection<string>()
        {
            "disabled",
            "default",
            "weak",
            "strong",
            "strong+",
        };
        public ObservableCollection<string> FilterVideo_Deblock_Items
        {
            get { return _FilterVideo_Deblock_Items; }
            set { _FilterVideo_Deblock_Items = value; }
        }
        // Selected Item
        private string _FilterVideo_Deblock_SelectedItem;
        public string FilterVideo_Deblock_SelectedItem
        {
            get { return _FilterVideo_Deblock_SelectedItem; }
            set
            {
                if (_FilterVideo_Deblock_SelectedItem == value)
                {
                    return;
                }

                _FilterVideo_Deblock_SelectedItem = value;
                OnPropertyChanged("FilterVideo_Deblock_SelectedItem");
            }
        }
        // Controls Enable
        private bool _FilterVideo_Deblock_IsEnabled = true;
        public bool FilterVideo_Deblock_IsEnabled
        {
            get { return _FilterVideo_Deblock_IsEnabled; }
            set
            {
                if (_FilterVideo_Deblock_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_Deblock_IsEnabled = value;
                OnPropertyChanged("FilterVideo_Deblock_IsEnabled");
            }
        }


        // -------------------------
        // Flip
        // -------------------------
        // Items
        public ObservableCollection<string> _FilterVideo_Flip_Items = new ObservableCollection<string>()
        {
            "disabled",
            "horizontal",
            "vertical",
            "both"
        };
        public ObservableCollection<string> FilterVideo_Flip_Items
        {
            get { return _FilterVideo_Flip_Items; }
            set { _FilterVideo_Flip_Items = value; }
        }

        // Selected Item
        private string _FilterVideo_Flip_SelectedItem;
        public string FilterVideo_Flip_SelectedItem
        {
            get { return _FilterVideo_Flip_SelectedItem; }
            set
            {
                if (_FilterVideo_Flip_SelectedItem == value)
                {
                    return;
                }

                _FilterVideo_Flip_SelectedItem = value;
                OnPropertyChanged("FilterVideo_Flip_SelectedItem");
            }
        }

        // Controls Enable
        private bool _FilterVideo_Flip_IsEnabled = true;
        public bool FilterVideo_Flip_IsEnabled
        {
            get { return _FilterVideo_Flip_IsEnabled; }
            set
            {
                if (_FilterVideo_Flip_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_Flip_IsEnabled = value;
                OnPropertyChanged("FilterVideo_Flip_IsEnabled");
            }
        }


        // -------------------------
        // Rotate
        // -------------------------
        // Items
        public ObservableCollection<string> _FilterVideo_Rotate_Items = new ObservableCollection<string>()
        {
            "disabled",
            "90° CW",   //transpose=1
            "180° CW",  //transpose=1, transpose=1
            "270° CW",  //transpose=1, transpose=1, transpose=1
            "90° CCW",  //transpose=2
            "180° CCW", //transpose=2, transpose=2
            "270° CCW"  //transpose=2, transpose=2, transpose=2
        };
        public ObservableCollection<string> FilterVideo_Rotate_Items
        {
            get { return _FilterVideo_Rotate_Items; }
            set { _FilterVideo_Rotate_Items = value; }
        }

        // Selected Item
        private string _FilterVideo_Rotate_SelectedItem;
        public string FilterVideo_Rotate_SelectedItem
        {
            get { return _FilterVideo_Rotate_SelectedItem; }
            set
            {
                if (_FilterVideo_Rotate_SelectedItem == value)
                {
                    return;
                }

                _FilterVideo_Rotate_SelectedItem = value;
                OnPropertyChanged("FilterVideo_Rotate_SelectedItem");
            }
        }

        // Controls Enable
        private bool _FilterVideo_Rotate_IsEnabled = true;
        public bool FilterVideo_Rotate_IsEnabled
        {
            get { return _FilterVideo_Rotate_IsEnabled; }
            set
            {
                if (_FilterVideo_Rotate_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_Rotate_IsEnabled = value;
                OnPropertyChanged("FilterVideo_Rotate_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_EQ_Brightness_IsEnabled = true;
        public bool FilterVideo_EQ_Brightness_IsEnabled
        {
            get { return _FilterVideo_EQ_Brightness_IsEnabled; }
            set
            {
                if (_FilterVideo_EQ_Brightness_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_EQ_Brightness_IsEnabled = value;
                OnPropertyChanged("FilterVideo_EQ_Brightness_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_EQ_Contrast_IsEnabled = true;
        public bool FilterVideo_EQ_Contrast_IsEnabled
        {
            get { return _FilterVideo_EQ_Contrast_IsEnabled; }
            set
            {
                if (_FilterVideo_EQ_Contrast_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_EQ_Contrast_IsEnabled = value;
                OnPropertyChanged("FilterVideo_EQ_Contrast_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_EQ_Saturation_IsEnabled = true;
        public bool FilterVideo_EQ_Saturation_IsEnabled
        {
            get { return _FilterVideo_EQ_Saturation_IsEnabled; }
            set
            {
                if (_FilterVideo_EQ_Saturation_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_EQ_Saturation_IsEnabled = value;
                OnPropertyChanged("FilterVideo_EQ_Saturation_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_EQ_Gamma_IsEnabled = true;
        public bool FilterVideo_EQ_Gamma_IsEnabled
        {
            get { return _FilterVideo_EQ_Gamma_IsEnabled; }
            set
            {
                if (_FilterVideo_EQ_Gamma_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_EQ_Gamma_IsEnabled = value;
                OnPropertyChanged("FilterVideo_EQ_Gamma_IsEnabled");
            }
        }

        // -------------------------
        // Selective Color
        // -------------------------
        public static List<SelectiveColor> SelectiveColorList { get; set; }
        public partial class SelectiveColor
        {
            public string Name { get; set; }
            public string Color { get; set; }
        }

        public static List<SelectiveColor> _FilterVideo_SelectiveColor_Items = new List<SelectiveColor>()
        {
            new SelectiveColor() { Name = "Reds",     Color = "Red"},
            new SelectiveColor() { Name = "Yellows",  Color = "Yellow"},
            new SelectiveColor() { Name = "Greens",   Color = "Green"},
            new SelectiveColor() { Name = "Cyans",    Color = "Cyan"},
            new SelectiveColor() { Name = "Blues",    Color = "Blue"},
            new SelectiveColor() { Name = "Magentas", Color = "Magenta"},
            new SelectiveColor() { Name = "Whites",   Color = "White"},
            new SelectiveColor() { Name = "Neutrals", Color = "Gray"},
            new SelectiveColor() { Name = "Blacks",   Color = "Black"},
        };
        public List<SelectiveColor> FilterVideo_SelectiveColor_Items
        {
            get { return _FilterVideo_SelectiveColor_Items; }
            set
            {
                _FilterVideo_SelectiveColor_Items = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Items");
            }
        }

        // Selected Index
        private int _FilterVideo_SelectiveColor_SelectedIndex;
        public int FilterVideo_SelectiveColor_SelectedIndex
        {
            get { return _FilterVideo_SelectiveColor_SelectedIndex; }
            set
            {
                if (_FilterVideo_SelectiveColor_SelectedIndex == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_SelectedIndex = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_SelectedIndex");
            }
        }

        // Selected Item
        private string _FilterVideo_SelectiveColor_SelectedItem;
        public string FilterVideo_SelectiveColor_SelectedItem
        {
            get { return _FilterVideo_SelectiveColor_SelectedItem; }
            set
            {
                if (_FilterVideo_SelectiveColor_SelectedItem == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_SelectedItem = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_SelectedItem");
            }
        }

        // Controls Enable
        private bool _FilterVideo_SelectiveColor_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_IsEnabled");
            }
        }

        // -------------------------
        // Correction Method
        // -------------------------
        // Items
        public ObservableCollection<string> _FilterVideo_SelectiveColor_Correction_Method_Items = new ObservableCollection<string>()
        {
            "relative",
            "absolute"
        };
        public ObservableCollection<string> FilterVideo_SelectiveColor_Correction_Method_Items
        {
            get { return _FilterVideo_SelectiveColor_Correction_Method_Items; }
            set { _FilterVideo_SelectiveColor_Correction_Method_Items = value; }
        }

        // Selected Item
        private string _FilterVideo_SelectiveColor_Correction_Method_SelectedItem;
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

        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Correction_Method_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Correction_Method_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Correction_Method_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Correction_Method_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Correction_Method_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Correction_Method_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Reds_Cyan_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Reds_Cyan_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Reds_Cyan_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Reds_Cyan_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Reds_Cyan_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Reds_Cyan_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Reds_Magenta_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Reds_Magenta_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Reds_Magenta_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Reds_Magenta_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Reds_Magenta_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Reds_Magenta_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Reds_Yellow_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Reds_Yellow_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Reds_Yellow_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Reds_Yellow_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Reds_Yellow_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Reds_Yellow_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Yellows_Cyan_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Yellows_Cyan_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Yellows_Cyan_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Yellows_Cyan_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Yellows_Cyan_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Yellows_Cyan_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Yellows_Magenta_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Yellows_Magenta_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Yellows_Magenta_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Yellows_Magenta_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Yellows_Magenta_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Yellows_Magenta_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Yellows_Yellow_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Yellows_Yellow_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Yellows_Yellow_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Yellows_Yellow_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Yellows_Yellow_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Yellows_Yellow_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Greens_Cyan_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Greens_Cyan_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Greens_Cyan_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Greens_Cyan_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Greens_Cyan_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Greens_Cyan_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Greens_Magenta_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Greens_Magenta_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Greens_Magenta_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Greens_Magenta_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Greens_Magenta_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Greens_Magenta_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Greens_Yellow_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Greens_Yellow_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Greens_Yellow_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Greens_Yellow_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Greens_Yellow_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Greens_Yellow_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Cyans_Cyan_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Cyans_Cyan_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Cyans_Cyan_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Cyans_Cyan_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Cyans_Cyan_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Cyans_Cyan_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Cyans_Magenta_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Cyans_Magenta_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Cyans_Magenta_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Cyans_Magenta_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Cyans_Magenta_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Cyans_Magenta_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Cyans_Yellow_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Cyans_Yellow_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Cyans_Yellow_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Cyans_Yellow_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Cyans_Yellow_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Cyans_Yellow_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Blues_Cyan_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Blues_Cyan_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Blues_Cyan_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Blues_Cyan_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Blues_Cyan_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Blues_Cyan_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Blues_Magenta_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Blues_Magenta_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Blues_Magenta_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Blues_Magenta_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Blues_Magenta_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Blues_Magenta_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Blues_Yellow_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Blues_Yellow_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Blues_Yellow_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Blues_Yellow_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Blues_Yellow_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Blues_Yellow_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Magentas_Cyan_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Magentas_Cyan_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Magentas_Cyan_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Magentas_Cyan_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Magentas_Cyan_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Magentas_Cyan_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Magentas_Magenta_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Magentas_Magenta_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Magentas_Magenta_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Magentas_Magenta_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Magentas_Magenta_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Magentas_Magenta_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Magentas_Yellow_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Magentas_Yellow_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Magentas_Yellow_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Magentas_Yellow_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Magentas_Yellow_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Magentas_Yellow_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Whites_Cyan_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Whites_Cyan_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Whites_Cyan_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Whites_Cyan_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Whites_Cyan_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Cyan_Yellow_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Whites_Magenta_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Whites_Magenta_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Whites_Magenta_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Whites_Magenta_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Whites_Magenta_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Whites_Magenta_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Whites_Yellow_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Whites_Yellow_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Whites_Yellow_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Whites_Yellow_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Whites_Yellow_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Whites_Yellow_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Neutrals_Cyan_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Neutrals_Cyan_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Neutrals_Cyan_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Neutrals_Cyan_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Neutrals_Cyan_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Cyan_Yellow_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Neutrals_Magenta_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Neutrals_Magenta_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Neutrals_Magenta_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Neutrals_Magenta_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Neutrals_Magenta_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Neutrals_Magenta_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Neutrals_Yellow_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Neutrals_Yellow_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Neutrals_Yellow_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Neutrals_Yellow_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Neutrals_Yellow_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Neutrals_Yellow_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Blacks_Cyan_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Blacks_Cyan_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Blacks_Cyan_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Blacks_Cyan_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Blacks_Cyan_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Cyan_Yellow_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Blacks_Magenta_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Blacks_Magenta_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Blacks_Magenta_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Blacks_Magenta_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Blacks_Magenta_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Blacks_Magenta_IsEnabled");
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
        // Controls Enable
        private bool _FilterVideo_SelectiveColor_Blacks_Yellow_IsEnabled = true;
        public bool FilterVideo_SelectiveColor_Blacks_Yellow_IsEnabled
        {
            get { return _FilterVideo_SelectiveColor_Blacks_Yellow_IsEnabled; }
            set
            {
                if (_FilterVideo_SelectiveColor_Blacks_Yellow_IsEnabled == value)
                {
                    return;
                }

                _FilterVideo_SelectiveColor_Blacks_Yellow_IsEnabled = value;
                OnPropertyChanged("FilterVideo_SelectiveColor_Blacks_Yellow_IsEnabled");
            }
        }


    }
}
