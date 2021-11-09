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
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Documents;
using Axiom;
using System.Collections.ObjectModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace ViewModel
{
    public class Configure : INotifyPropertyChanged
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
        public Configure()
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
            LogPath_Text = Log.logAppDataLocalDir;
            LogCheckBox_IsChecked = false;
            LogPath_IsEnabled = false;
            Shell_SelectedItem = "CMD";
            ShellTitle_SelectedItem = "Disabled";
            ProcessPriority_SelectedItem = "Default";
            Threads_SelectedItem = "Optimal";
            OutputNaming_ListView_SelectedIndex = -1;
            InputFileNameTokens_SelectedItem = "Keep";
            InputFileNameTokensCustom_Text = string.Empty;
            OutputFileNameSpacing_SelectedItem = "Original";
            OutputOverwrite_SelectedItem = "Always";
            Theme_SelectedItem = "Axiom";
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
        private ObservableCollection<string> _ConfigPath_Items = new ObservableCollection<string>()
        {
           "AppData Local",
           "AppData Roaming",
           //"Documents",
           "App Root"
        };
        public ObservableCollection<string> ConfigPath_Items
        {
            get { return _ConfigPath_Items; }
            set
            {
                _ConfigPath_Items = value;
                OnPropertyChanged("ConfigPath_Items");
            }
        }

        // Selected Index
        private int _ConfigPath_SelectedIndex;
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
        private string _ConfigPath_SelectedItem;
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
        // Shell
        // --------------------------------------------------
        // Items Source
        private ObservableCollection<string> _Shell_Items = new ObservableCollection<string>()
        {
           "CMD",
           "PowerShell"
        };
        public ObservableCollection<string> Shell_Items
        {
            get { return _Shell_Items; }
            set
            {
                _Shell_Items = value;
                OnPropertyChanged("Shell_Items");
            }
        }

        // Selected Index
        private int _Shell_SelectedIndex;
        public int Shell_SelectedIndex
        {
            get { return _Shell_SelectedIndex; }
            set
            {
                if (_Shell_SelectedIndex == value)
                {
                    return;
                }

                _Shell_SelectedIndex = value;
                OnPropertyChanged("Shell_SelectedIndex");
            }
        }

        // Selected Item
        private string _Shell_SelectedItem;
        public string Shell_SelectedItem
        {
            get { return _Shell_SelectedItem; }
            set
            {
                if (_Shell_SelectedItem == value)
                {
                    return;
                }

                _Shell_SelectedItem = value;
                OnPropertyChanged("Shell_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Shell_IsEnabled = true;
        public bool Shell_IsEnabled
        {
            get { return _Shell_IsEnabled; }
            set
            {
                if (_Shell_IsEnabled == value)
                {
                    return;
                }

                _Shell_IsEnabled = value;
                OnPropertyChanged("Shell_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Shell Title
        // --------------------------------------------------
        // Items Source
        private ObservableCollection<string> _ShellTitle_Items = new ObservableCollection<string>()
        {
           "Disabled",
           "Custom",
           "Filename",
           "Tokens",
           "Filename+Tokens"
        };
        public ObservableCollection<string> ShellTitle_Items
        {
            get { return _ShellTitle_Items; }
            set
            {
                _ShellTitle_Items = value;
                OnPropertyChanged("ShellTitle_Items");
            }
        }

        // Selected Index
        private int _ShellTitle_SelectedIndex;
        public int ShellTitle_SelectedIndex
        {
            get { return _ShellTitle_SelectedIndex; }
            set
            {
                if (_ShellTitle_SelectedIndex == value)
                {
                    return;
                }

                _ShellTitle_SelectedIndex = value;
                OnPropertyChanged("ShellTitle_SelectedIndex");
            }
        }

        // Selected Item
        private string _ShellTitle_SelectedItem;
        public string ShellTitle_SelectedItem
        {
            get { return _ShellTitle_SelectedItem; }
            set
            {
                if (_ShellTitle_SelectedItem == value)
                {
                    return;
                }

                _ShellTitle_SelectedItem = value;
                OnPropertyChanged("ShellTitle_SelectedItem");
            }
        }

        // Controls Enable
        private bool _ShellTitle_IsEnabled = true;
        public bool ShellTitle_IsEnabled
        {
            get { return _ShellTitle_IsEnabled; }
            set
            {
                if (_ShellTitle_IsEnabled == value)
                {
                    return;
                }

                _ShellTitle_IsEnabled = value;
                OnPropertyChanged("ShellTitle_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Process Priority
        // --------------------------------------------------
        // Items Source
        private ObservableCollection<string> _ProcessPriority_Items = new ObservableCollection<string>()
        {
           "Default",
           "Low",
           "Below Normal",
           "Normal",
           "Above Normal",
           "High"
        };
        public ObservableCollection<string> ProcessPriority_Items
        {
            get { return _ProcessPriority_Items; }
            set
            {
                _ProcessPriority_Items = value;
                OnPropertyChanged("ProcessPriority_Items");
            }
        }

        // Selected Index
        private int _ProcessPriority_SelectedIndex;
        public int ProcessPriority_SelectedIndex
        {
            get { return _ProcessPriority_SelectedIndex; }
            set
            {
                if (_ProcessPriority_SelectedIndex == value)
                {
                    return;
                }

                _ProcessPriority_SelectedIndex = value;
                OnPropertyChanged("ProcessPriority_SelectedIndex");
            }
        }

        // Selected Item
        private string _ProcessPriority_SelectedItem;
        public string ProcessPriority_SelectedItem
        {
            get { return _ProcessPriority_SelectedItem; }
            set
            {
                if (_ProcessPriority_SelectedItem == value)
                {
                    return;
                }

                _ProcessPriority_SelectedItem = value;
                OnPropertyChanged("ProcessPriority_SelectedItem");
            }
        }

        // Controls Enable
        private bool _ProcessPriority_IsEnabled = true;
        public bool ProcessPriority_IsEnabled
        {
            get { return _ProcessPriority_IsEnabled; }
            set
            {
                if (_ProcessPriority_IsEnabled == value)
                {
                    return;
                }

                _ProcessPriority_IsEnabled = value;
                OnPropertyChanged("ProcessPriority_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Threads
        // --------------------------------------------------
        // Items Source
        private ObservableCollection<string> _Threads_Items = new ObservableCollection<string>()
        {
           "Default",
           "Optimal",
           "All",
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
        public ObservableCollection<string> Threads_Items
        {
            get { return _Threads_Items; }
            set
            {
                _Threads_Items = value;
                OnPropertyChanged("Threads_Items");
            }
        }

        // Selected Index
        private int _Threads_SelectedIndex;
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
        private string _Threads_SelectedItem;
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
        // Output Naming ListView
        // --------------------------------------------------
        public static ObservableCollection<string> OutputNaming_LoadDefaults()
        {
            ObservableCollection<string> items = new ObservableCollection<string>(MainWindow.outputNaming_Defaults);

            return items;
        }

        // Items Source
        private ObservableCollection<string> _OutputNaming_ListView_Items = OutputNaming_LoadDefaults();
        public ObservableCollection<string> OutputNaming_ListView_Items
        {
            get { return _OutputNaming_ListView_Items; }
            set
            {
                _OutputNaming_ListView_Items = value;
                OnPropertyChanged("OutputNaming_ListView_Items");
            }
        }
        // Selected Items
        private List<string> _OutputNaming_ListView_SelectedItems = new List<string>();
        public List<string> OutputNaming_ListView_SelectedItems
        {
            get { return _OutputNaming_ListView_SelectedItems; }
            set
            {
                _OutputNaming_ListView_SelectedItems = value;
                OnPropertyChanged("OutputNaming_ListView_SelectedItems");
            }
        }
        // Selected Index
        private int _OutputNaming_ListView_SelectedIndex;
        public int OutputNaming_ListView_SelectedIndex
        {
            get { return _OutputNaming_ListView_SelectedIndex; }
            set
            {
                if (_OutputNaming_ListView_SelectedIndex == value)
                {
                    return;
                }

                _OutputNaming_ListView_SelectedIndex = value;
                OnPropertyChanged("OutputNaming_ListView_SelectedIndex");
            }
        }
        private double _OutputNaming_ListView_Opacity { get; set; }
        public double OutputNaming_ListView_Opacity
        {
            get { return _OutputNaming_ListView_Opacity; }
            set
            {
                if (_OutputNaming_ListView_Opacity == value)
                {
                    return;
                }

                _OutputNaming_ListView_Opacity = value;
                OnPropertyChanged("OutputNaming_ListView_Opacity");
            }
        }
        // Controls Enable
        public bool _OutputNaming_ListView_IsEnabled = true;
        public bool OutputNaming_ListView_IsEnabled
        {
            get { return _OutputNaming_ListView_IsEnabled; }
            set
            {
                if (_OutputNaming_ListView_IsEnabled == value)
                {
                    return;
                }

                _OutputNaming_ListView_IsEnabled = value;
                OnPropertyChanged("OutputNaming_ListView_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Input Filename Tokens
        // --------------------------------------------------
        // Items Source
        private ObservableCollection<string> _InputFileNameTokens_Items = new ObservableCollection<string>()
        {
           "Keep",
           "Remove"
        };
        public ObservableCollection<string> InputFileNameTokens_Items
        {
            get { return _InputFileNameTokens_Items; }
            set
            {
                _InputFileNameTokens_Items = value;
                OnPropertyChanged("InputFileNameTokens_Items");
            }
        }

        // Selected Index
        private int _InputFileNameTokens_SelectedIndex;
        public int InputFileNameTokens_SelectedIndex
        {
            get { return _InputFileNameTokens_SelectedIndex; }
            set
            {
                if (_InputFileNameTokens_SelectedIndex == value)
                {
                    return;
                }

                _InputFileNameTokens_SelectedIndex = value;
                OnPropertyChanged("InputFileNameTokens_SelectedIndex");
            }
        }

        // Selected Item
        private string _InputFileNameTokens_SelectedItem;
        public string InputFileNameTokens_SelectedItem
        {
            get { return _InputFileNameTokens_SelectedItem; }
            set
            {
                if (_InputFileNameTokens_SelectedItem == value)
                {
                    return;
                }

                _InputFileNameTokens_SelectedItem = value;
                OnPropertyChanged("InputFileNameTokens_SelectedItem");
            }
        }
        // Enabled
        private bool _InputFileNameTokens_IsEnabled = true;
        public bool InputFileNameTokens_IsEnabled
        {
            get { return _InputFileNameTokens_IsEnabled; }
            set
            {
                if (_InputFileNameTokens_IsEnabled == value)
                {
                    return;
                }

                _InputFileNameTokens_IsEnabled = value;
                OnPropertyChanged("InputFileNameTokens_IsEnabled");
            }
        }

        // -------------------------
        // Input FileName Tokens Custom
        // -------------------------
        // Text
        private string _InputFileNameTokensCustom_Text;
        public string InputFileNameTokensCustom_Text
        {
            get { return _InputFileNameTokensCustom_Text; }
            set
            {
                if (_InputFileNameTokensCustom_Text == value)
                {
                    return;
                }

                _InputFileNameTokensCustom_Text = value;
                OnPropertyChanged("InputFileNameTokensCustom_Text");
            }
        }
        // Enabled
        private bool _InputFileNameTokensCustom_IsEnabled = true;
        public bool InputFileNameTokensCustom_IsEnabled
        {
            get { return _InputFileNameTokensCustom_IsEnabled; }
            set
            {
                if (_InputFileNameTokensCustom_IsEnabled == value)
                {
                    return;
                }

                _InputFileNameTokensCustom_IsEnabled = value;
                OnPropertyChanged("InputFileNameTokensCustom_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Output Filename Spacing
        // --------------------------------------------------
        // Items Source
        private ObservableCollection<string> _OutputFileNameSpacing_Items = new ObservableCollection<string>()
        {
           "Original",
           "Spaces",
           "Periods",
           "Dashes",
           "Underscores"
        };
        public ObservableCollection<string> OutputFileNameSpacing_Items
        {
            get { return _OutputFileNameSpacing_Items; }
            set
            {
                _OutputFileNameSpacing_Items = value;
                OnPropertyChanged("OutputFileNameSpacing_Items");
            }
        }

        // Selected Index
        private int _OutputFileNameSpacing_SelectedIndex;
        public int OutputFileNameSpacing_SelectedIndex
        {
            get { return _OutputFileNameSpacing_SelectedIndex; }
            set
            {
                if (_OutputFileNameSpacing_SelectedIndex == value)
                {
                    return;
                }

                _OutputFileNameSpacing_SelectedIndex = value;
                OnPropertyChanged("OutputFileNameSpacing_SelectedIndex");
            }
        }

        // Selected Item
        private string _OutputFileNameSpacing_SelectedItem;
        public string OutputFileNameSpacing_SelectedItem
        {
            get { return _OutputFileNameSpacing_SelectedItem; }
            set
            {
                if (_OutputFileNameSpacing_SelectedItem == value)
                {
                    return;
                }

                _OutputFileNameSpacing_SelectedItem = value;
                OnPropertyChanged("OutputFileNameSpacing_SelectedItem");
            }
        }

        // Controls Enable
        private bool _OutputFileNameSpacing_IsEnabled = true;
        public bool OutputFileNameSpacing_IsEnabled
        {
            get { return _OutputFileNameSpacing_IsEnabled; }
            set
            {
                if (_OutputFileNameSpacing_IsEnabled == value)
                {
                    return;
                }

                _OutputFileNameSpacing_IsEnabled = value;
                OnPropertyChanged("OutputFileNameSpacing_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Output File Overwrite
        // --------------------------------------------------
        // Items Source
        private ObservableCollection<string> _OutputOverwrite_Items = new ObservableCollection<string>()
        {
           "Ask",
           "Always",
           "Never"
        };
        public ObservableCollection<string> OutputOverwrite_Items
        {
            get { return _OutputOverwrite_Items; }
            set
            {
                _OutputOverwrite_Items = value;
                OnPropertyChanged("OutputOverwrite_Items");
            }
        }

        // Selected Index
        private int _OutputOverwrite_SelectedIndex;
        public int OutputOverwrite_SelectedIndex
        {
            get { return _OutputOverwrite_SelectedIndex; }
            set
            {
                if (_OutputOverwrite_SelectedIndex == value)
                {
                    return;
                }

                _OutputOverwrite_SelectedIndex = value;
                OnPropertyChanged("OutputOverwrite_SelectedIndex");
            }
        }

        // Selected Item
        private string _OutputOverwrite_SelectedItem;
        public string OutputOverwrite_SelectedItem
        {
            get { return _OutputOverwrite_SelectedItem; }
            set
            {
                if (_OutputOverwrite_SelectedItem == value)
                {
                    return;
                }

                _OutputOverwrite_SelectedItem = value;
                OnPropertyChanged("OutputOverwrite_SelectedItem");
            }
        }

        // Controls Enable
        private bool _OutputOverwrite_IsEnabled = true;
        public bool OutputOverwrite_IsEnabled
        {
            get { return _OutputOverwrite_IsEnabled; }
            set
            {
                if (_OutputOverwrite_IsEnabled == value)
                {
                    return;
                }

                _OutputOverwrite_IsEnabled = value;
                OnPropertyChanged("OutputOverwrite_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Theme
        // --------------------------------------------------
        // Items Source
        private ObservableCollection<string> _Theme_Items = new ObservableCollection<string>()
        {
            "Axiom",
            "FFmpeg",
            "Cyberpunk",
            "Onyx",
            "Circuit",
            "System"
        };
        public ObservableCollection<string> Theme_Items
        {
            get { return _Theme_Items; }
            set
            {
                _Theme_Items = value;
                OnPropertyChanged("Theme_Items");
            }
        }

        // Selected Index
        private int _Theme_SelectedIndex;
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
        private string _Theme_SelectedItem;
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
