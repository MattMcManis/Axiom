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
using System.Threading.Tasks;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class ConfigureViewModel : INotifyPropertyChanged
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

        // Configure View Model
        //public static ConfigureView vm = new ConfigureView();


        /// <summary>
        /// Configure View Model
        /// </summary>
        public ConfigureViewModel()
        {
            LoadControlsDefaults();
        }


        /// <summary>
        /// Load Controls Defaults
        /// </summary>
        public void LoadControlsDefaults()
        {
            LoadConfigDefaults();
        }


        /// <summary>
        /// Load Controls Defaults
        /// </summary>
        public void LoadConfigDefaults()
        {
            ConfigPath_SelectedItem = "AppData Local";
            CustomPresetsPath_Text = MainWindow.appDataLocalDir + @"Axiom UI\presets\";
            FFmpegPath_Text = "<auto>";
            FFprobePath_Text = "<auto>";
            FFplayPath_Text = "<auto>";
            youtubedlPath_Text = "<auto>";
            LogPath_Text = Log.logDir;
            LogCheckBox_IsChecked = false;
            LogPath_IsEnabled = false;
            Theme_SelectedItem = "Axiom";
            Threads_SelectedItem = "optimal";
            UpdateAutoCheck_IsChecked = true;
        }

        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Configure
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // -------------------------
        // Config Path
        // -------------------------
        // Items Source
        private List<string> _ConfigPath_Items = new List<string>()
        {
           "AppData Local",
           "AppData Roaming",
           //"Documents",
           "App Root"
        };
        public List<string> ConfigPath_Items
        {
            get { return _ConfigPath_Items; }
            set
            {
                _ConfigPath_Items = value;
                OnPropertyChanged("ConfigPath_Items");
            }
        }

        // Selected Index
        private int _ConfigPath_SelectedIndex { get; set; }
        public int ConfigPath_SelectedIndex
        {
            get { return _ConfigPath_SelectedIndex; }
            set
            {
                if (_ConfigPath_SelectedIndex == value)
                {
                    return;
                }

                _ConfigPath_SelectedIndex = value;
                OnPropertyChanged("ConfigPath_SelectedIndex");
            }
        }

        // Selected Item
        private string _ConfigPath_SelectedItem { get; set; }
        public string ConfigPath_SelectedItem
        {
            get { return _ConfigPath_SelectedItem; }
            set
            {
                if (_ConfigPath_SelectedItem == value)
                {
                    return;
                }

                _ConfigPath_SelectedItem = value;
                OnPropertyChanged("ConfigPath_SelectedItem");
            }
        }

        // Controls Enable
        private bool _ConfigPath_IsEnabled = true;
        public bool ConfigPath_IsEnabled
        {
            get { return _ConfigPath_IsEnabled; }
            set
            {
                if (_ConfigPath_IsEnabled == value)
                {
                    return;
                }

                _ConfigPath_IsEnabled = value;
                OnPropertyChanged("ConfigPath_IsEnabled");
            }
        }
        //// Text
        //private string _ConfigPath_Text;
        //public string ConfigPath_Text
        //{
        //    get { return _ConfigPath_Text; }
        //    set
        //    {
        //        if (_ConfigPath_Text == value)
        //        {
        //            return;
        //        }

        //        _ConfigPath_Text = value;
        //        OnPropertyChanged("ConfigPath_Text");
        //    }
        //}
        //// Enabled
        //private bool _ConfigPath_IsEnabled = false;
        //public bool ConfigPath_IsEnabled
        //{
        //    get { return _ConfigPath_IsEnabled; }
        //    set
        //    {
        //        if (_ConfigPath_IsEnabled == value)
        //        {
        //            return;
        //        }

        //        _ConfigPath_IsEnabled = value;
        //        OnPropertyChanged("ConfigPath_IsEnabled");
        //    }
        //}

        // -------------------------
        // Custom Presets Path
        // -------------------------
        // Text
        private string _CustomPresetsPath_Text;
        public string CustomPresetsPath_Text
        {
            get { return _CustomPresetsPath_Text; }
            set
            {
                if (_CustomPresetsPath_Text == value)
                {
                    return;
                }

                _CustomPresetsPath_Text = value;
                OnPropertyChanged("CustomPresetsPath_Text");
            }
        }
        // Enabled
        private bool _CustomPresetsPath_IsEnabled = true;
        public bool CustomPresetsPath_IsEnabled
        {
            get { return _CustomPresetsPath_IsEnabled; }
            set
            {
                if (_CustomPresetsPath_IsEnabled == value)
                {
                    return;
                }

                _CustomPresetsPath_IsEnabled = value;
                OnPropertyChanged("CustomPresetsPath_IsEnabled");
            }
        }

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
        // Log Console
        // -------------------------
        // Text
        private string _LogConsole_Text;
        public string LogConsole_Text
        {
            get { return _LogConsole_Text; }
            set
            {
                if (_LogConsole_Text == value)
                {
                    return;
                }

                _LogConsole_Text = value;
                OnPropertyChanged("LogConsole_Text");
            }
        }


        // -------------------------
        // youtubedl Path
        // -------------------------
        // Text
        private string _youtubedlPath_Text;
        public string youtubedlPath_Text
        {
            get { return _youtubedlPath_Text; }
            set
            {
                if (_youtubedlPath_Text == value)
                {
                    return;
                }

                _youtubedlPath_Text = value;
                OnPropertyChanged("youtubedlPath_Text");
            }
        }
        // Enabled
        private bool _youtubedlPath_IsEnabled = true;
        public bool youtubedlPath_IsEnabled
        {
            get { return _youtubedlPath_IsEnabled; }
            set
            {
                if (_youtubedlPath_IsEnabled == value)
                {
                    return;
                }

                _youtubedlPath_IsEnabled = value;
                OnPropertyChanged("youtubedlPath_IsEnabled");
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
        // Items Source
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
        // Items Source
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

        // Text
        private string _UpdateAutoCheck_Text = "On";
        public string UpdateAutoCheck_Text
        {
            get { return _UpdateAutoCheck_Text; }
            set
            {
                if (_UpdateAutoCheck_Text == value)
                {
                    return;
                }

                _UpdateAutoCheck_Text = value;
                OnPropertyChanged("UpdateAutoCheck_Text");
            }
        }

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
        /// App Threads
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        public Task youtubedlInputWorker = null;
        //public Thread youtubedlInputWorker = null;
        //public BackgroundWorker youtubedlInputWorker = null;
        //public bool threadFinished = false;

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

        // --------------------------------------------------
        // Debug
        // --------------------------------------------------
        // FlowDocument
        private FlowDocument _Debug_FlowDocument;
        public FlowDocument Debug_FlowDocument
        {
            get { return _Debug_FlowDocument; }
            set
            {
                if (_Debug_FlowDocument == value)
                {
                    return;
                }

                _Debug_FlowDocument = value;
                OnPropertyChanged("Debug_FlowDocument");
            }
        }

        // Paragraph
        private Paragraph _Debug_Paragraph;
        public Paragraph Debug_Paragraph
        {
            get { return _Debug_Paragraph; }
            set
            {
                if (_Debug_Paragraph == value)
                {
                    return;
                }

                _Debug_Paragraph = value;
                OnPropertyChanged("Debug_Paragraph");
            }
        }

        // Text
        private string _Debug_Text;
        public string Debug_Text
        {
            get { return _Debug_Text; }
            set
            {
                if (value != _Debug_Text)
                {
                    _Debug_Text = value;
                    OnPropertyChanged("Debug_Text");
                }
            }
        }


    }
}
