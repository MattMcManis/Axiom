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
using System.Windows.Media;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class FilterVideoViewModel : INotifyPropertyChanged
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
        public FilterVideoViewModel()
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
            // Fix
            FilterVideo_Deband_SelectedItem = "disabled";
            FilterVideo_Deshake_SelectedItem = "disabled";
            FilterVideo_Deflicker_SelectedItem = "disabled";
            FilterVideo_Dejudder_SelectedItem = "disabled";
            FilterVideo_Denoise_SelectedItem = "disabled";
            FilterVideo_Deinterlace_SelectedItem = "disabled";

            // EQ
            FilterVideo_EQ_Brightness_Value = 0;
            FilterVideo_EQ_Contrast_Value = 0;
            FilterVideo_EQ_Saturation_Value = 0;
            FilterVideo_EQ_Gamma_Value = 0;

            // Selective Color
            FilterVideo_SelectiveColor_SelectedIndex = 0;
            FilterVideo_SelectiveColor_Correction_Method_SelectedItem = "relative";

            // Reds
            FilterVideo_SelectiveColor_Reds_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Reds_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Reds_Yellow_Value = 0;
            // Yellows
            FilterVideo_SelectiveColor_Yellows_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Yellows_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Yellows_Yellow_Value = 0;
            // Greens
            FilterVideo_SelectiveColor_Greens_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Greens_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Greens_Yellow_Value = 0;
            // Cyans
            FilterVideo_SelectiveColor_Cyans_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Cyans_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Cyans_Yellow_Value = 0;
            // Blues
            FilterVideo_SelectiveColor_Blues_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Blues_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Blues_Yellow_Value = 0;
            // Magentas
            FilterVideo_SelectiveColor_Magentas_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Magentas_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Magentas_Yellow_Value = 0;
            // Whites
            FilterVideo_SelectiveColor_Whites_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Whites_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Whites_Yellow_Value = 0;
            // Neutrals
            FilterVideo_SelectiveColor_Neutrals_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Neutrals_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Neutrals_Yellow_Value = 0;
            // Blacks
            FilterVideo_SelectiveColor_Blacks_Cyan_Value = 0;
            FilterVideo_SelectiveColor_Blacks_Magenta_Value = 0;
            FilterVideo_SelectiveColor_Blacks_Yellow_Value = 0;
        }


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Filter Video
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // -------------------------
        // Deband
        // -------------------------
        // Items
        public List<string> _FilterVideo_Deband_Items = new List<string>()
        {
            "disabled",
            "enabled"
        };
        public List<string> FilterVideo_Deband_Items
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
        public List<string> _FilterVideo_Deshake_Items = new List<string>()
        {
            "disabled",
            "enabled"
        };
        public List<string> FilterVideo_Deshake_Items
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
        public List<string> _FilterVideo_Deflicker_Items = new List<string>()
        {
            "disabled",
            "enabled"
        };
        public List<string> FilterVideo_Deflicker_Items
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
        public List<string> _FilterVideo_Dejudder_Items = new List<string>()
        {
            "disabled",
            "enabled"
        };
        public List<string> FilterVideo_Dejudder_Items
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
        public List<string> _FilterVideo_Denoise_Items = new List<string>()
        {
            "disabled",
            "default",
            "light",
            "medium",
            "heavy",
        };
        public List<string> FilterVideo_Denoise_Items
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
        // Deinterlace
        // -------------------------
        // Items
        public List<string> _FilterVideo_Deinterlace_Items = new List<string>()
        {
            "disabled",
            "frame",
            "field",
            "frame nospatial",
            "field nospatial",
            //"cuda frame",
            //"cuda field"
        };
        public List<string> FilterVideo_Deinterlace_Items
        {
            get { return _FilterVideo_Deinterlace_Items; }
            set { _FilterVideo_Deinterlace_Items = value; }
        }
        // Selected Item
        private string _FilterVideo_Deinterlace_SelectedItem { get; set; }
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
        private int _FilterVideo_SelectiveColor_SelectedIndex { get; set; }
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
        private string _FilterVideo_SelectiveColor_SelectedItem { get; set; }
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


        // -------------------------
        // Correction Method
        // -------------------------
        // Items
        public List<string> _FilterVideo_SelectiveColor_Correction_Method_Items = new List<string>()
        {
            "relative",
            "absolute"
        };
        public List<string> FilterVideo_SelectiveColor_Correction_Method_Items
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





    }
}
