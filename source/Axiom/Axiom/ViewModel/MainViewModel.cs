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

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class MainViewModel : INotifyPropertyChanged
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

        // Main View Model
        //public static MainView vm = new MainView();


        /// <summary>
        /// Main View Model
        /// </summary>
        public MainViewModel()
        {
            LoadControlsDefaults();
        }


        /// <summary>
        /// Load Controls Defaults
        /// </summary>
        public void LoadControlsDefaults()
        {
            // -------------------------
            // Main
            // -------------------------
            Preset_IsEnabled = true;
            Preset_SelectedItem = "Preset";
            Input_Location_IsEnabled = false;
            Output_Location_IsEnabled = false;
            BatchExtension_IsEnabled = false;
            CMDWindowKeep_IsChecked = true;
            AutoSortScript_IsChecked = true;
        } 

    
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Main
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // --------------------------------------------------
        // Preset - ComboBox
        // --------------------------------------------------
        // Items Source
        public class Preset
        {
            public string Name { get; set; }
            public bool Category { get; set; }
            public string Type { get; set; }
        }

        public ObservableCollection<Preset> _Preset_Items = new ObservableCollection<Preset>()
        {
            // Default
            new Preset() { Name = "Default",       Category = true  },
            new Preset() { Name = "Preset",        Category = false },

            // Custom
            new Preset() { Name = "Custom",        Category = true  },

            // Web
            new Preset() { Name = "Web",           Category = true  },
            new Preset() { Name = "HTML5",         Category = false },
            new Preset() { Name = "YouTube",       Category = false },

            // UHD
            new Preset() { Name = "UHD",           Category = true  },
            new Preset() { Name = "Archive",       Category = false },
            new Preset() { Name = "HEVC Ultra",    Category = false },
            new Preset() { Name = "HEVC High",     Category = false },

            // HD
            new Preset() { Name = "HD",            Category = true  },
            new Preset() { Name = "HD Ultra",      Category = false },
            new Preset() { Name = "HD High",       Category = false },
            new Preset() { Name = "HD Medium",     Category = false },

            // SD
            new Preset() { Name = "SD",            Category = true  },
            new Preset() { Name = "SD High",       Category = false },
            new Preset() { Name = "SD Medium",     Category = false },
            new Preset() { Name = "SD Low",        Category = false },

            // Mobile
            new Preset() { Name = "Mobile",        Category = true  },
            new Preset() { Name = "Android",       Category = false },
            new Preset() { Name = "iOS",           Category = false },

            // Device
            new Preset() { Name = "Device",        Category = true  },
            new Preset() { Name = "Roku",          Category = false },
            new Preset() { Name = "Amazon Fire",   Category = false },
            new Preset() { Name = "Chromecast",    Category = false },
            new Preset() { Name = "Apple TV",      Category = false },
            new Preset() { Name = "Raspberry Pi",  Category = false },

            // Console
            new Preset() { Name = "Console",       Category = true  },
            new Preset() { Name = "PS3",           Category = false },
            new Preset() { Name = "PS4",           Category = false },
            new Preset() { Name = "Xbox 360",      Category = false },
            new Preset() { Name = "Xbox One",      Category = false },

            // Disc
            new Preset() { Name = "Disc",          Category = true  },
            new Preset() { Name = "UHD",           Category = false },
            new Preset() { Name = "Blu-ray",       Category = false },
            new Preset() { Name = "DVD",           Category = false },          

            // Music
            new Preset() { Name = "Music",         Category = true  },
            new Preset() { Name = "Lossless",      Category = false },
            new Preset() { Name = "MP3 HQ",        Category = false },
            new Preset() { Name = "MP3 Low",       Category = false },
            new Preset() { Name = "iTunes",        Category = false },
            new Preset() { Name = "Voice",         Category = false },

            // YouTube
            new Preset() { Name = "YouTube-DL",       Category = true  },
            new Preset() { Name = "Video Download",   Category = false },
            new Preset() { Name = "Music Download",   Category = false },
        };

        public ObservableCollection<Preset> Preset_Items
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


        // -------------------------
        // Window Top
        // -------------------------
        // Value
        private double _Window_Position_Top = 0;
        public double Window_Position_Top
        {
            get { return _Window_Position_Top; }
            set
            {
                if (_Window_Position_Top == value)
                {
                    return;
                }

                _Window_Position_Top = value;
                OnPropertyChanged("Window_Position_Top");
            }
        }

        // -------------------------
        // Window Left
        // -------------------------
        private double _Window_Position_Left = 0;
        public double Window_Position_Left
        {
            get { return _Window_Position_Left; }
            set
            {
                if (_Window_Position_Left == value)
                {
                    return;
                }

                _Window_Position_Left = value;
                OnPropertyChanged("Window_Position_Left");
            }
        }

        // -------------------------
        // Window Width
        // -------------------------
        // Value
        private double _Window_Width = 0;
        public double Window_Width
        {
            get { return _Window_Width; }
            set
            {
                if (_Window_Width == value)
                {
                    return;
                }

                _Window_Width = value;
                OnPropertyChanged("Window_Width");
            }
        }

        // -------------------------
        // Window Height
        // -------------------------
        // Value
        private double _Window_Height = 0;
        public double Window_Height
        {
            get { return _Window_Height; }
            set
            {
                if (_Window_Height == value)
                {
                    return;
                }

                _Window_Height = value;
                OnPropertyChanged("Window_Height");
            }
        }

        // -------------------------
        // Window Maximized
        // -------------------------
        // Value
        private bool _Window_IsMaximized = true;
        public bool Window_IsMaximized
        {
            get { return _Window_IsMaximized; }
            set
            {
                if (_Window_IsMaximized == value)
                {
                    return;
                }

                _Window_IsMaximized = value;
                OnPropertyChanged("Window_IsMaximized");
            }
        }


        // -------------------------
        // Window State
        // -------------------------
        // Value
        private WindowState _Window_State = WindowState.Normal;
        public WindowState Window_State
        {
            get { return _Window_State; }
            set
            {
                if (_Window_State == value)
                {
                    return;
                }

                _Window_State = value;
                OnPropertyChanged("Window_State");
            }
        }


        // -------------------------
        // Window Title
        // -------------------------
        // Text
        private string _TitleVersion;
        public string TitleVersion
        {
            get { return _TitleVersion; }
            set
            {
                if (value != _TitleVersion)
                {
                    _TitleVersion = value;
                    OnPropertyChanged("TitleVersion");
                }
            }
        }

    

        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Tools
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

        

        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Input
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
        // Input Location - Button
        // --------------------------------------------------
        // Controls Enable
        private bool _Input_Location_IsEnabled { get; set; }
        public bool Input_Location_IsEnabled
        {
            get { return _Input_Location_IsEnabled; }
            set
            {
                if (_Input_Location_IsEnabled == value)
                {
                    return;
                }

                _Input_Location_IsEnabled = value;
                OnPropertyChanged("Input_Location_IsEnabled");
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



        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Output
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

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
        // Output Location - Button
        // --------------------------------------------------
        // Controls Enable
        private bool _Output_Location_IsEnabled { get; set; }
        public bool Output_Location_IsEnabled
        {
            get { return _Output_Location_IsEnabled; }
            set
            {
                if (_Output_Location_IsEnabled == value)
                {
                    return;
                }

                _Output_Location_IsEnabled = value;
                OnPropertyChanged("Output_Location_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Script View
        // --------------------------------------------------
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


        // --------------------------------------------------
        // Convert Button - TextBlock
        // --------------------------------------------------
        // Text
        private string _Convert_Text = "Convert";
        public string Convert_Text
        {
            get { return _Convert_Text; }
            set
            {
                if (_Convert_Text == value)
                {
                    return;
                }

                _Convert_Text = value;
                OnPropertyChanged("Convert_Text");
            }
        }


    }
}
