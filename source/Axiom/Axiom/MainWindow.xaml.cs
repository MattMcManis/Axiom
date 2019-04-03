/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2019 Matt McManis
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

using Axiom.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // View Model
        public ViewModel vm = new ViewModel();

        // Axiom Current Version
        public static Version currentVersion;
        // Axiom GitHub Latest Version
        public static Version latestVersion;
        // Alpha, Beta, Stable
        public static string currentBuildPhase = "alpha";
        public static string latestBuildPhase;
        public static string[] splitVersionBuildPhase;

        //public string TitleVersion
        //{
        //    get { return (string)GetValue(TitleProperty); }
        //    set { SetValue(TitleProperty, value); }
        //}

        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Global Variables
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // System
        public static string appDir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + @"\"; // Axiom.exe directory
        public static string tempDir = Path.GetTempPath(); // Windows AppData Temp Directory
        public static string userProfile = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%").TrimEnd('\\') + @"\"; // C:\Users\Example\
        public static string downloadDir = userProfile + @"Downloads\"; // C:\Users\Example\Downloads\

        // Programs
        public static string youtubedl; // youtube-dl.exe

        // Input
        public static string inputDir; // Input File Directory
        public static string inputFileName; // (eg. myvideo.mp4 = myvideo)
        public static string inputExt; // (eg. .mp4)
        public static string input; // Single: Input Path + Filename No Ext + Input Ext (Browse Text Box) /// Batch: Input Path (Browse Text Box)

        // Output
        public static string outputDir; // Output Path
        public static string outputFileName; // Output Directory + Filename (No Extension)
        public static string outputExt; // (eg. .webm)
        public static string output; // Single: outputDir + outputFileName + outputExt /// Batch: outputDir + %~nf
        public static string outputNewFileName; // File Rename if File already exists

        // Batch
        //public static string batchExt; // Batch user entered extension (eg. mp4 or .mp4)
        public static string batchInputAuto;

        /// <summary>
        ///     Volume Up Down
        /// </summary>
        /// <remarks>
        ///     Used for Volume Up Down buttons. Integer += 1 for each tick of the timer.
        ///     Timer Tick in MainWindow Initialize
        /// </remarks>
        public DispatcherTimer dispatcherTimerUp = new DispatcherTimer(DispatcherPriority.Render);
        public DispatcherTimer dispatcherTimerDown = new DispatcherTimer(DispatcherPriority.Render);


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Other Windows
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        ///     Log Console
        /// </summary>
        public LogConsole logconsole = new LogConsole(/*((MainWindow)Application.Current.MainWindow)*/);

        /// <summary>
        ///     Debug Console
        /// </summary>
        public static DebugConsole debugconsole;

        /// <summary>
        ///     File Properties Console
        /// </summary>
        public FilePropertiesWindow filepropwindow;

        /// <summary>
        ///     Crop Window
        /// </summary>
        public static CropWindow cropwindow;

        /// <summary>
        ///     Optimize Advanced Window
        /// </summary>
        public static InfoWindow infowindow;

        /// <summary>
        ///     Update Window
        /// </summary>
        public static UpdateWindow updatewindow;



        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Main Window Initialize
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        public MainWindow()
        {
            InitializeComponent();

            // -----------------------------------------------------------------
            /// <summary>
            ///     Window & Components
            /// </summary>
            // -----------------------------------------------------------------
            // Set Min/Max Width/Height to prevent Tablets maximizing
            MinWidth = 824;
            MinHeight = 464;

            // -------------------------
            // Set Current Version to Assembly Version
            // -------------------------
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string assemblyVersion = fvi.FileVersion;
            currentVersion = new Version(assemblyVersion);

            // -----------------------------------------------------------------
            /// <summary>
            ///     Control Binding
            /// </summary>
            // -----------------------------------------------------------------
            DataContext = vm;

            // -------------------------
            // Start the Log Console (Hidden)
            // -------------------------
            StartLogConsole();

            // -------------------------
            // Title + Version
            // -------------------------
            vm.TitleVersion = "Axiom ~ FFmpeg UI (" + Convert.ToString(currentVersion) + "-" + currentBuildPhase + ")";

            //// -------------------------
            //// Load Theme
            //// -------------------------
            //// --------------------------
            //// First time use
            //// --------------------------
            //try
            //{
            //    if (string.IsNullOrEmpty(Settings.Default.Theme.ToString()))
            //    {
            //        Configure.theme = "Axiom";

            //        // Set ComboBox if Configure Window is Open
            //        vm.Theme_SelectedItem = "Axiom";

            //        // Save Theme for next launch
            //        Settings.Default.Theme = Configure.theme;
            //        Settings.Default.Save();

            //        // Change Theme Resource
            //        App.Current.Resources.MergedDictionaries.Clear();
            //        App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            //        {
            //            Source = new Uri("Themes/" + "Theme" + Configure.theme + ".xaml", UriKind.RelativeOrAbsolute)
            //        });
            //    }
            //    // --------------------------
            //    // Load Saved Settings Override
            //    // --------------------------
            //    else if (!string.IsNullOrEmpty(Settings.Default.Theme.ToString())) // null check
            //    {
            //        Configure.theme = Settings.Default.Theme.ToString();

            //        // Set ComboBox
            //        vm.Theme_SelectedItem = Configure.theme;

            //        // Change Theme Resource
            //        App.Current.Resources.MergedDictionaries.Clear();
            //        App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            //        {
            //            Source = new Uri("Themes/" + "Theme" + Configure.theme + ".xaml", UriKind.RelativeOrAbsolute)
            //        });
            //    }


            //    // -------------------------
            //    // Log Text Theme SelectiveColorPreview
            //    // -------------------------
            //    if (Configure.theme == "Axiom")
            //    {
            //        Log.ConsoleDefault = Brushes.White; // Default
            //        Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2")); // Titles
            //        Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
            //        Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
            //        Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8")); // Actions
            //    }
            //    else if (Configure.theme == "FFmpeg")
            //    {
            //        Log.ConsoleDefault = Brushes.White; // Default
            //        Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cb85c")); // Titles
            //        Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
            //        Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
            //        Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cb85c")); // Actions
            //    }
            //    else if (Configure.theme == "Cyberpunk")
            //    {
            //        Log.ConsoleDefault = Brushes.White; // Default
            //        Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9f3ed2")); // Titles
            //        Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
            //        Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
            //        Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9380fd")); // Actions
            //    }
            //    else if (Configure.theme == "Onyx")
            //    {
            //        Log.ConsoleDefault = Brushes.White; // Default
            //        Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999")); // Titles
            //        Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
            //        Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
            //        Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#777777")); // Actions
            //    }
            //    else if (Configure.theme == "Circuit")
            //    {
            //        Log.ConsoleDefault = Brushes.White; // Default
            //        Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ad8a4a")); // Titles
            //        Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
            //        Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
            //        Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2ebf93")); // Actions
            //    }
            //    else if (Configure.theme == "Prelude")
            //    {
            //        Log.ConsoleDefault = Brushes.White; // Default
            //        Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999")); // Titles
            //        Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
            //        Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
            //        Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#777777")); // Actions
            //    }
            //    else if (Configure.theme == "System")
            //    {
            //        Log.ConsoleDefault = Brushes.White; // Default
            //        Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2")); // Titles
            //        Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
            //        Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
            //        Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8")); // Actions
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("Problem loading Theme.",
            //                    "Error",
            //                    MessageBoxButton.OK,
            //                    MessageBoxImage.Error);
            //}


            // Log Console Message /////////
            //logconsole.rtbLog.Document = new FlowDocument(Log.logParagraph); //start
            //logconsole.rtbLog.BeginChange(); //begin change

            Log.logParagraph.Inlines.Add(new Bold(new Run(vm.TitleVersion)) { Foreground = Log.ConsoleTitle });

            //Log.LogConsoleMessageAdd(TitleVersion,      // Message
            //                         "bold",            // Emphasis
            //                         Log.ConsoleAction, // Color
            //                         0);                // Linebreaks

            /// <summary>
            ///     System Info
            /// </summary>
            // Shows OS and Hardware information in Log Console
            SystemInfoDisplay();
            //Task<int> task = SystemInfoDisplay();


            // -----------------------------------------------------------------
            /// <summary>
            ///     Load Saved Settings
            /// </summary>
            // -----------------------------------------------------------------  
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Loading Saved Settings...")) { Foreground = Log.ConsoleAction });

            //// -------------------------
            //// Prevent Loading Corrupt App.Config
            //// -------------------------
            //try
            //{
            //    ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            //}
            //catch (ConfigurationErrorsException ex)
            //{
            //    string filename = ex.Filename;

            //    if (File.Exists(filename) == true)
            //    {
            //        File.Delete(filename);
            //        Settings.Default.Upgrade();
            //    }
            //    else
            //    {

            //    }
            //}


            //// -------------------------
            //// Window Position
            //// -------------------------
            if (this.Top == 0 &&
                this.Left == 0
                )
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            //if (Convert.ToDouble(Settings.Default.Left) == 0
            //    && Convert.ToDouble(Settings.Default.Top) == 0)
            //{
            //    WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //}
            //// Load Saved
            //else
            //{
            //    Top = Settings.Default.Top;
            //    Left = Settings.Default.Left;
            //    Height = Settings.Default.Height;
            //    Width = Settings.Default.Width;

            //    if (Settings.Default.Maximized)
            //    {
            //        WindowState = WindowState.Maximized;
            //    }
            //}


            //// -------------------------
            //// Load FFmpeg.exe Path
            //// -------------------------
            //Configure.LoadFFmpegPath(vm);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("FFmpeg: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(vm.FFmpegPath_Text) { Foreground = Log.ConsoleDefault });

            //// -------------------------
            //// Load FFprobe.exe Path
            //// -------------------------
            //Configure.LoadFFprobePath(vm);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("FFprobe: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(vm.FFprobePath_Text) { Foreground = Log.ConsoleDefault });

            //// -------------------------
            //// Load FFplay.exe Path
            //// -------------------------
            //Configure.LoadFFplayPath(vm);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("FFplay: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(vm.FFplayPath_Text) { Foreground = Log.ConsoleDefault });

            //// -------------------------
            //// Load youtube-dl.exe Path
            //// -------------------------
            //Configure.LoadyoutubedlPath(vm);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("youtube-dl: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(vm.youtubedlPath_Text) { Foreground = Log.ConsoleDefault });

            //// -------------------------
            //// Load Log Enabled
            //// -------------------------
            //Configure.LoadLogCheckbox(vm);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Log Enabled: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(Convert.ToString(vm.LogCheckBox_IsChecked.ToString())) { Foreground = Log.ConsoleDefault });

            //// -------------------------
            //// Load Log Path
            //// -------------------------
            //Configure.LoadLogPath(vm);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Log Path: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(vm.LogPath_Text) { Foreground = Log.ConsoleDefault });

            //// -------------------------
            //// Load Threads
            //// -------------------------
            //Configure.LoadThreads(vm);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Using CPU Threads: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(vm.Threads_SelectedItem) { Foreground = Log.ConsoleDefault });


            //end change !important
            //logconsole.rtbLog.EndChange();


            //// -------------------------
            //// Load CMD Keep Window Toggle
            //// -------------------------
            //// Log Checkbox     
            //// Safeguard Against Corrupt Saved Settings
            //try
            //{
            //    // --------------------------
            //    // First time use
            //    // --------------------------
            //    vm.CMDWindowKeep_IsChecked = Convert.ToBoolean(Settings.Default.CMDWindowKeep);
            //}
            //catch
            //{

            //}

            //// -------------------------
            //// Load Auto Sort Script Toggle
            //// -------------------------
            //// Log Checkbox     
            //// Safeguard Against Corrupt Saved Settings
            //try
            //{
            //    // --------------------------
            //    // First time use
            //    // --------------------------
            //    vm.AutoSortScript_IsChecked = Convert.ToBoolean(Settings.Default.AutoSortScript);
            //}
            //catch
            //{

            //}


            //// -------------------------
            //// Load Updates Auto Check
            //// -------------------------
            //// Log Checkbox     
            //// Safeguard Against Corrupt Saved Settings
            //try
            //{
            //    // --------------------------
            //    // First time use
            //    // --------------------------
            //    vm.UpdateAutoCheck_IsChecked = Convert.ToBoolean(Settings.Default.UpdateAutoCheck);
            //}
            //catch
            //{

            //}

            // -------------------------
            // Volume Up/Down Button Timer Tick
            // Dispatcher Tick
            // In Intializer to prevent Tick from doubling up every MouseDown
            // -------------------------
            dispatcherTimerUp.Tick += new EventHandler(dispatcherTimerUp_Tick);
            dispatcherTimerDown.Tick += new EventHandler(dispatcherTimerDown_Tick);

            // --------------------------
            // ScriptView Copy/Paste
            // --------------------------
            DataObject.AddCopyingHandler(tbxScriptView, new DataObjectCopyingEventHandler(OnScriptCopy));
            DataObject.AddPastingHandler(tbxScriptView, new DataObjectPastingEventHandler(OnScriptPaste));


            // -------------------------
            // Import Axiom Config axiom.conf
            // -------------------------
            if (File.Exists(Configure.configFile))
            {
                Configure.ImportConfig(this, vm);
            }
            // Defaults
            else
            {
                vm.LoadDefaults();
            }

            //// -------------------------
            //// Load Custom Presets
            //// -------------------------
            //Profiles.LoadCustomPresets(vm);



            // -------------------------
            // Log Text Theme SelectiveColorPreview
            // -------------------------
            if (vm.Theme_SelectedItem == "Axiom")
            {
                Log.ConsoleDefault = Brushes.White; // Default
                Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2")); // Titles
                Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8")); // Actions
            }
            else if (vm.Theme_SelectedItem == "FFmpeg")
            {
                Log.ConsoleDefault = Brushes.White; // Default
                Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cb85c")); // Titles
                Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cb85c")); // Actions
            }
            else if (vm.Theme_SelectedItem == "Cyberpunk")
            {
                Log.ConsoleDefault = Brushes.White; // Default
                Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9f3ed2")); // Titles
                Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9380fd")); // Actions
            }
            else if (vm.Theme_SelectedItem == "Onyx")
            {
                Log.ConsoleDefault = Brushes.White; // Default
                Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999")); // Titles
                Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#777777")); // Actions
            }
            else if (vm.Theme_SelectedItem == "Circuit")
            {
                Log.ConsoleDefault = Brushes.White; // Default
                Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ad8a4a")); // Titles
                Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2ebf93")); // Actions
            }
            else if (vm.Theme_SelectedItem == "Prelude")
            {
                Log.ConsoleDefault = Brushes.White; // Default
                Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999")); // Titles
                Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#777777")); // Actions
            }
            else if (vm.Theme_SelectedItem == "System")
            {
                Log.ConsoleDefault = Brushes.White; // Default
                Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2")); // Titles
                Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8")); // Actions
            }
        }



        /// <summary>
        ///    Window Loaded
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = this;

            // -------------------------
            // Control Defaults
            // -------------------------
            lstv_Subtitles.SelectionMode = SelectionMode.Single;

            // -------------------------
            // Load ComboBox Items
            // -------------------------
            // Filter Selective SelectiveColorPreview
            cboFilterVideo_SelectiveColor.ItemsSource = cboSelectiveColor_Items;

            // -------------------------
            // Check for Available Updates
            // -------------------------
            //Task.Factory.StartNew(() =>
            //{
            Task<int> task = UpdateAvailableCheck(vm);
            //UpdateAvailableCheck(vm);
            //int count = await task; // do not await
            //});

            // -------------------------
            // Load Custom Presets
            // -------------------------
            Profiles.LoadCustomPresets(vm);
        }


        /// <summary>
        ///     Close / Exit (Method)
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            // Force Exit All Executables
            base.OnClosed(e);
            System.Windows.Forms.Application.ExitThread();
            Application.Current.Shutdown();
        }

        // Save Window Position
        void Window_Closing(object sender, CancelEventArgs e)
        {
            //if (WindowState == WindowState.Maximized)
            //{
            //    // Use the RestoreBounds as the current values will be 0, 0 and the scale of the screen
            //    Settings.Default.Top = RestoreBounds.Top;
            //    Settings.Default.Left = RestoreBounds.Left;
            //    Settings.Default.Height = RestoreBounds.Height;
            //    Settings.Default.Width = RestoreBounds.Width;
            //    Settings.Default.Maximized = true;
            //}
            //else
            //{
            //    Settings.Default.Top = Top;
            //    Settings.Default.Left = Left;
            //    Settings.Default.Height = Height;
            //    Settings.Default.Width = Width;
            //    Settings.Default.Maximized = false;
            //}

            //Settings.Default.Save();

            // -------------------------
            // Export axiom.conf
            // -------------------------
            try
            {
                // Overwrite only if changes made
                if (File.Exists(Configure.configFile))
                {
                    Configure.INIFile conf = new Configure.INIFile(Configure.configFile);

                    //double? top = Convert.ToDouble(inif.Read("Main Window", "Window_Position_Top"));
                    //double? left = Convert.ToDouble(inif.Read("Main Window", "Window_Position_Left"));
                    //double? width = Convert.ToDouble(inif.Read("Main Window", "Window_Width"));
                    //double? height = Convert.ToDouble(inif.Read("Main Window", "Window_Height"));

                    // Window
                    double top;
                    double.TryParse(conf.Read("Main Window", "Window_Position_Top"), out top);
                    double left;
                    double.TryParse(conf.Read("Main Window", "Window_Position_Left"), out left);
                    double width;
                    double.TryParse(conf.Read("Main Window", "Window_Width"), out width);
                    double height;
                    double.TryParse(conf.Read("Main Window", "Window_Height"), out height);
                    //bool windowState;
                    //bool.TryParse(conf.Read("Main Window", "WindowState_Maximized").ToLower(), out windowState);

                    // CMD Window Keep
                    bool settings_CMDWindowKeep_IsChecked;
                    bool.TryParse(conf.Read("Main Window", "CMDWindowKeep_IsChecked").ToLower(), out settings_CMDWindowKeep_IsChecked);

                    // Auto Sort Script
                    bool settings_AutoSortScript_IsChecked;
                    bool.TryParse(conf.Read("Main Window", "AutoSortScript_IsChecked").ToLower(), out settings_AutoSortScript_IsChecked);

                    // Log CheckBox
                    bool settings_LogCheckBox_IsChecked;
                    bool.TryParse(conf.Read("Settings", "LogCheckBox_IsChecked").ToLower(), out settings_LogCheckBox_IsChecked);

                    // Update Auto Check
                    bool settings_UpdateAutoCheck_IsChecked;
                    bool.TryParse(conf.Read("Settings", "UpdateAutoCheck_IsChecked").ToLower(), out settings_UpdateAutoCheck_IsChecked);

                    if (// Main Window
                        this.Top != top ||
                        this.Left != left ||
                        this.Width != width ||
                        this.Height != height ||

                        //this.WindowState != windowState ||

                        //vm.CMDWindowKeep_IsChecked != Convert.ToBoolean(inif.Read("Main Window", "CMDWindowKeep_IsChecked").ToLower()) ||
                        //vm.AutoSortScript_IsChecked != Convert.ToBoolean(inif.Read("Main Window", "AutoSortScript_IsChecked").ToLower()) ||
                        vm.CMDWindowKeep_IsChecked != settings_CMDWindowKeep_IsChecked ||
                        vm.AutoSortScript_IsChecked != settings_AutoSortScript_IsChecked ||

                        // Settings
                        vm.FFmpegPath_Text != conf.Read("Settings", "FFmpegPath_Text") ||
                        vm.FFprobePath_Text != conf.Read("Settings", "FFprobePath_Text") ||
                        vm.FFplayPath_Text != conf.Read("Settings", "FFplayPath_Text") ||
                        vm.CustomPresetsPath_Text != conf.Read("Settings", "CustomPresetsPath_Text") ||
                        vm.LogPath_Text != conf.Read("Settings", "LogPath_Text") ||
                        //vm.LogCheckBox_IsChecked != Convert.ToBoolean(inif.Read("Settings", "LogCheckBox_IsChecked").ToLower()) ||
                        vm.LogCheckBox_IsChecked != settings_LogCheckBox_IsChecked ||
                        vm.Threads_SelectedItem != conf.Read("Settings", "Threads_SelectedItem") ||
                        vm.Theme_SelectedItem != conf.Read("Settings", "Theme_SelectedItem") ||
                        //vm.UpdateAutoCheck_IsChecked != Convert.ToBoolean(inif.Read("Settings", "UpdateAutoCheck_IsChecked").ToLower())
                        vm.UpdateAutoCheck_IsChecked != settings_UpdateAutoCheck_IsChecked
                        )
                    {
                        //MessageBox.Show("1"); //debug
                        Configure.ExportConfig(this, vm);
                    }
                }

                // Export Defaults & Currently Selected
                else if (!File.Exists(Configure.configFile))
                {
                    Configure.ExportConfig(this, vm);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            

            // Exit
            e.Cancel = true;
            System.Windows.Forms.Application.ExitThread();
            Environment.Exit(0);
        }


        /// <summary>
        ///     Clear Global Variables (Method)
        /// </summary>
        public static void ClearGlobalVariables(ViewModel vm)
        {
            // FFmpeg
            //FFmpeg.cmdWindow = string.Empty;

            // FFprobe
            FFprobe.argsVideoCodec = string.Empty;
            FFprobe.argsAudioCodec = string.Empty;
            FFprobe.argsVideoBitRate = string.Empty;
            FFprobe.argsAudioBitRate = string.Empty;
            FFprobe.argsSize = string.Empty;
            FFprobe.argsDuration = string.Empty;
            FFprobe.argsFrameRate = string.Empty;

            FFprobe.inputVideoCodec = string.Empty;
            FFprobe.inputVideoBitRate = string.Empty;
            FFprobe.inputAudioCodec = string.Empty;
            FFprobe.inputAudioBitRate = string.Empty;
            FFprobe.inputSize = string.Empty;
            FFprobe.inputDuration = string.Empty;
            FFprobe.inputFrameRate = string.Empty;

            FFprobe.vEntryType = string.Empty;
            FFprobe.aEntryType = string.Empty;

            // Video
            Video.passSingle = string.Empty;
            Video.vEncodeSpeed = string.Empty;
            Video.hwacceleration = string.Empty;
            Video.vCodec = string.Empty;
            Video.vBitMode = string.Empty;
            Video.vQuality = string.Empty;
            Video.vBitRateNA = string.Empty;
            Video.vLossless = string.Empty;
            Video.vBitRate = string.Empty;
            Video.vMinRate = string.Empty;
            Video.vMaxRate = string.Empty;
            Video.vBufSize = string.Empty;
            Video.vOptions = string.Empty;
            Video.vCRF = string.Empty;
            Video.pix_fmt = string.Empty;
            Video.vAspectRatio = string.Empty;
            Video.vScalingAlgorithm = string.Empty;
            Video.fps = string.Empty;
            Video.optTune = string.Empty;
            Video.optProfile = string.Empty;
            Video.optLevel = string.Empty;
            Video.optFlags = string.Empty;
            Video.width = string.Empty;
            Video.height = string.Empty;
            //Video.scale = string.Empty;

            if (Video.x265paramsList != null &&
                Video.x265paramsList.Count > 0)
            {
                Video.x265paramsList.Clear();
                Video.x265paramsList.TrimExcess();
            }

            Video.x265params = string.Empty;

            // Clear Crop if ClearCrop Button Identifier is Empty
            if (vm.Video_CropClear_Text == "Clear")
            {
                CropWindow.crop = string.Empty;
                CropWindow.divisibleCropWidth = null; //int
                CropWindow.divisibleCropHeight = null; //int
            }

            //Format.trim = string.Empty;
            Format.trimStart = string.Empty;
            Format.trimEnd = string.Empty;

            VideoFilters.vFilter = string.Empty;
            VideoFilters.geq = string.Empty;

            if (VideoFilters.vFiltersList != null && 
                VideoFilters.vFiltersList.Count > 0)
            {
                VideoFilters.vFiltersList.Clear();
                VideoFilters.vFiltersList.TrimExcess();
            }

            Video.v2PassArgs = string.Empty;
            Video.pass1Args = string.Empty; // Batch 2-Pass
            Video.pass2Args = string.Empty; // Batch 2-Pass
            Video.pass1 = string.Empty;
            Video.pass2 = string.Empty;
            Video.image = string.Empty;
            Video.optimize = string.Empty;
           
            // Subtitle
            Subtitle.sCodec = string.Empty;
            Subtitle.subtitles = string.Empty;

            // Audio
            Audio.aCodec = string.Empty;
            Audio.aChannel = string.Empty;
            Audio.aBitMode = string.Empty;
            Audio.aBitRate = string.Empty;
            Audio.aBitRateNA = string.Empty;
            Audio.aQuality = string.Empty;
            Audio.aCompressionLevel = string.Empty;
            Audio.aSamplerate = string.Empty;
            Audio.aBitDepth = string.Empty;
            AudioFilters.aFilter = string.Empty;
            Audio.aVolume = string.Empty;
            Audio.aHardLimiter = string.Empty;

            if (AudioFilters.aFiltersList != null &&
                AudioFilters.aFiltersList.Count > 0)
            {
                AudioFilters.aFiltersList.Clear();
                AudioFilters.aFiltersList.TrimExcess();
            }

            // Batch
            FFprobe.batchFFprobeAuto = string.Empty;
            Video.batchVideoAuto = string.Empty;
            Audio.batchAudioAuto = string.Empty;

            // Streams
            //Streams.map = string.Empty;
            Streams.vMap = string.Empty;
            Streams.cMap = string.Empty;
            Streams.sMap = string.Empty;
            Streams.aMap = string.Empty;
            Streams.mMap = string.Empty;

            // General
            //outputNewFileName = string.Empty;

            // Do not Empty:
            //
            //inputDir
            //inputFileName
            //inputExt
            //input
            //outputDir
            //outputFileName
            //FFmpeg.ffmpegArgs
            //FFmpeg.ffmpegArgsSort
            //CropWindow.divisibleCropWidth
            //CropWindow.divisibleCropHeight
            //CropWindow.cropWidth
            //CropWindow.cropHeight
            //CropWindow.cropX
            //CropWindow.cropY
        }


        /// <summary>
        ///     Remove Linebreaks (Method)
        /// </summary>
        /// <remarks>
        ///     Used for Selected Controls FFmpeg Arguments
        /// </remarks>
        public static String RemoveLineBreaks(string lines)
        {
            lines = lines
                .Replace(Environment.NewLine, "")
                .Replace("\r\n\r\n", "")
                .Replace("\r\n", "")
                .Replace("\n\n", "")
                .Replace("\n", "")
                .Replace("\u2028", "")
                .Replace("\u000A", "")
                .Replace("\u000B", "")
                .Replace("\u000C", "")
                .Replace("\u000D", "")
                .Replace("\u0085", "")
                .Replace("\u2028", "")
                .Replace("\u2029", "");

            return lines;
        }


        /// <summary>
        ///     Replace Linebreaks with Space (Method)
        /// </summary>
        /// <remarks>
        ///     Used for Script View Custom Edited Script
        /// </remarks>
        public static String ReplaceLineBreaksWithSpaces(string lines)
        {
            // Replace Linebreaks with Spaces to avoid arguments touching

            lines = lines
                .Replace(Environment.NewLine, " ")
                .Replace("\r\n", " ")
                .Replace("\n", " ")
                .Replace("\u2028", " ")
                .Replace("\u000A", " ")
                .Replace("\u000B", " ")
                .Replace("\u000C", " ")
                .Replace("\u000D", " ")
                .Replace("\u0085", " ")
                .Replace("\u2028", " ")
                .Replace("\u2029", " ");

            return lines;
        }


        /// <summary>
        ///     Deny Special Keys
        /// </summary>
        public void DenySpecialKeys(KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) && e.Key == Key.D8 || // *
                Keyboard.IsKeyDown(Key.RightShift) && e.Key == Key.D8 ||  // *

                Keyboard.IsKeyDown(Key.LeftShift) && e.Key == Key.OemPeriod ||  // >
                Keyboard.IsKeyDown(Key.RightShift) && e.Key == Key.OemPeriod ||  // >

                Keyboard.IsKeyDown(Key.LeftShift) && e.Key == Key.OemComma ||  // <
                Keyboard.IsKeyDown(Key.RightShift) && e.Key == Key.OemComma ||  // <

                e.Key == Key.Tab ||  // tab
                e.Key == Key.Oem2 ||  // forward slash
                e.Key == Key.OemBackslash ||  // backslash
                e.Key == Key.OemQuestion ||  // ?
                e.Key == Key.OemQuotes ||  // "
                e.Key == Key.OemSemicolon ||  // ;
                e.Key == Key.OemPipe // |
                )
            {
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Allow Only Numbers
        /// </summary>
        public void AllowOnlyNumbers(KeyEventArgs e)
        {
            // Only allow Numbers
            // Deny Symbols (Shift + Number)
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) ||
                e.Key == Key.Back ||
                e.Key == Key.Delete ||
                Keyboard.IsKeyDown(Key.LeftShift) && (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                Keyboard.IsKeyDown(Key.RightShift) && (e.Key >= Key.D0 && e.Key <= Key.D9)
                )
            {
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Allow Only Numbers and Backspace
        /// </summary>
        public void AllowOnlyNumbersAndBackspace(KeyEventArgs e)
        {
            // Only allow Numbers and Backspace
            // Deny Symbols (Shift + Number)
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) &&
                e.Key != Key.Back &&
                e.Key != Key.Delete ||
                Keyboard.IsKeyDown(Key.LeftShift) && (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                Keyboard.IsKeyDown(Key.RightShift) && (e.Key >= Key.D0 && e.Key <= Key.D9)
                )
            {
                e.Handled = true;
            }
        }


        /// <summary>
        ///     Start Log Console (Method)
        /// </summary>
        public void StartLogConsole()
        {
            // Open LogConsole Window
            logconsole = new LogConsole(/*this*/);
            logconsole.Hide();

            // Position with Show();

            logconsole.rtbLog.Cursor = Cursors.Arrow;
        }


        /// <summary>
        ///     Selected Item
        /// </summary>
        public static string Video_EncodeSpeed_PreviousItem;
        public static string Video_Quality_PreviousItem;
        public static string Video_Pass_PreviousItem;
        public static string VideoOptimize_PreviousItem;
        public static string Audio_Quality_PreviousItem;
        public static string Audio_SampleRate_PreviousItem;
        public static string Audio_BitDepth_PreviousItem;
        public static string SelectedItem(List<string> itemsName,
                                          string selectedItem
                                          )
        {
            // -------------------------
            // Save Previous Selected Item
            // -------------------------
            //if (!string.IsNullOrEmpty(selectedItem))
            //{
            // Select Previous Item
            if (itemsName?.Contains(selectedItem) == true)
            {
                //MessageBox.Show(selectedItem); //debug
                return selectedItem;
            }
            else
            {
                //MessageBox.Show("null"); //debug
                return itemsName.FirstOrDefault();
            }
            //}
            // Default to First Item
            //else
            //{
            //    return itemsName.FirstOrDefault();
            //}

            //return selectedItem;
        }


        // --------------------------------------------------------------------------------------------------------
        // Configure
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------
        // Custom Presets Path - Textbox
        // --------------------------------------------------
        private void tbxCustomPresetsPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Configure.CustomPresetsFolderBrowser(vm);
        }

        // Drag and Drop
        private void tbxCustomPresetsPath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxCustomPresetsPath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            vm.CustomPresetsPath_Text = buffer.First();
        }


        // --------------------------------------------------
        // CustomPresets Auto Path - Button
        // --------------------------------------------------
        private void btnCustomPresetsAuto_Click(object sender, RoutedEventArgs e)
        {
            // Set the ffmpegPath string
            //Configure.ffmpegPath = "<auto>";

            // Display Folder Path in Textbox
            vm.CustomPresetsPath_Text = Profiles.presetsDir;

            // CustomPresets Path path for next launch
            //Settings.Default.CustomPresetsPath = Profiles.presetsDir;
            //Settings.Default.Save();
            //Settings.Default.Reload();
        }


        // --------------------------------------------------
        // FFmpeg Path - Textbox
        // --------------------------------------------------
        private void tbxFFmpegPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Configure.FFmpegFolderBrowser(vm);
        }

        // Drag and Drop
        private void tbxFFmpegPath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxFFmpegPath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            vm.FFmpegPath_Text = buffer.First();
        }


        // --------------------------------------------------
        // FFmpeg Auto Path - Button
        // --------------------------------------------------
        private void btnFFmpegAuto_Click(object sender, RoutedEventArgs e)
        {
            // Set the ffmpegPath string
            //Configure.ffmpegPath = "<auto>";

            // Display Folder Path in Textbox
            vm.FFmpegPath_Text = "<auto>";

            // FFmpeg Path path for next launch
            //Settings.Default.FFmpegPath = "<auto>";
            //Settings.Default.Save();
            //Settings.Default.Reload();
        }


        // --------------------------------------------------
        // FFprobe Path - Textbox
        // --------------------------------------------------
        private void tbxFFprobePath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Configure.FFprobeFolderBrowser(vm);
        }

        // Drag and Drop
        private void tbxFFprobePath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxFFprobePath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            vm.FFprobePath_Text = buffer.First();
        }

        // --------------------------------------------------
        // FFprobe Auto Path - Button
        // --------------------------------------------------
        private void btnFFprobeAuto_Click(object sender, RoutedEventArgs e)
        {
            // Set the ffprobePath string
            //Configure.ffprobePath = "<auto>"; //<auto>

            // Display Folder Path in Textbox
            vm.FFprobePath_Text = "<auto>";

            // Save 7-zip Path path for next launch
            //Settings.Default.FFprobePath = "<auto>";
            //Settings.Default.Save();
            //Settings.Default.Reload();
        }


        // --------------------------------------------------
        // FFplay Path - Textbox
        // --------------------------------------------------
        private void tbxFFplayPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Configure.FFplayFolderBrowser(vm);
        }

        // Drag and Drop
        private void tbxFFplayPath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxFFplayPath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            vm.FFplayPath_Text = buffer.First();
        }

        // --------------------------------------------------
        // FFplay Auto Path - Button
        // --------------------------------------------------
        private void btnFFplayAuto_Click(object sender, RoutedEventArgs e)
        {
            // Set the ffplayPath string
            //Configure.ffplayPath = "<auto>"; //<auto>

            // Display Folder Path in Textbox
            vm.FFplayPath_Text = "<auto>";

            // Save 7-zip Path path for next launch
            //Settings.Default.FFplayPath = "<auto>";
            //Settings.Default.Save();
            //Settings.Default.Reload();
        }


        // --------------------------------------------------
        // youtubedl Path - Textbox
        // --------------------------------------------------
        private void tbxyoutubedlPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Configure.youtubedlFolderBrowser(vm);
        }

        // Drag and Drop
        private void tbxyoutubedlPath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxyoutubedlPath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            vm.youtubedlPath_Text = buffer.First();
        }

        // --------------------------------------------------
        // youtubedl Auto Path - Button
        // --------------------------------------------------
        private void btnyoutubedlAuto_Click(object sender, RoutedEventArgs e)
        {
            // Set the ffmpegPath string
            //Configure.ffmpegPath = "<auto>";

            // Display Folder Path in Textbox
            vm.youtubedlPath_Text = "<auto>";

            // youtubedl Path path for next launch
            //Settings.Default.youtubedlPath = "<auto>";
            //Settings.Default.Save();
            //Settings.Default.Reload();
        }


        // --------------------------------------------------
        // Log Checkbox - Checked
        // --------------------------------------------------
        private void cbxLog_Checked(object sender, RoutedEventArgs e)
        {
            // Enable the Log

            //// Prevent Loading Corrupt App.Config
            //try
            //{
            //    Settings.Default.Log_IsChecked = vm.LogCheckBox_IsChecked;
            //    Settings.Default.Save();
            //}
            //catch
            //{

            //}
        }


        // --------------------------------------------------
        // Log Checkbox - Unchecked
        // --------------------------------------------------
        private void cbxLog_Unchecked(object sender, RoutedEventArgs e)
        {
            // Disable the Log

            //// Prevent Loading Corrupt App.Config
            //try
            //{
            //    Settings.Default.Log_IsChecked = vm.LogCheckBox_IsChecked;
            //    Settings.Default.Save();
            //}
            //catch
            //{

            //}
        }


        // --------------------------------------------------
        // Log Path - Textbox
        // --------------------------------------------------
        private void tbxLog_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Configure.LogFolderBrowser(vm);
        }

        // --------------------------------------------------
        // Log Auto Path - Button
        // --------------------------------------------------
        private void btnLogAuto_Click(object sender, RoutedEventArgs e)
        {
            // Uncheck Log Checkbox
            vm.LogCheckBox_IsChecked = false;

            // Clear Path in Textbox
            vm.LogPath_Text = string.Empty;

            // Set the logPath string
            //Configure.logPath = string.Empty;

            // Save Log Path path for next launch
            //Settings.Default.LogPath = string.Empty;
            //Settings.Default.Save();
            //Settings.Default.Reload();
        }


        // --------------------------------------------------
        // Threads - ComboBox
        // --------------------------------------------------
        private void threadSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Custom ComboBox Editable
            //if (vm.Threads_SelectedItem == "Custom" || cboThreads.SelectedValue == null)
            //{
            //    cboThreads.IsEditable = true;
            //}

            // Other Items Disable Editable
            //if (vm.Threads_SelectedItem != "Custom" && cboThreads.SelectedValue != null)
            //{
            //    cboThreads.IsEditable = false;
            //}

            // Maintain Editable Combobox while typing
            //if (cboThreads.IsEditable == true)
            //{
            //    cboThreads.IsEditable = true;

            //    // Clear Custom Text
            //    cboThreads.SelectedIndex = -1;
            //}

            // Set the threads to pass to MainWindow
            Configure.threads = vm.Threads_SelectedItem;

            // Save Thread Number for next launch
            //Settings.Default.Threads = vm.Threads_SelectedItem;
            //Settings.Default.Save();
            //Settings.Default.Reload();
        }
        // --------------------------------------------------
        // Thread Select ComboBox - Allow Only Numbers
        // --------------------------------------------------
        private void threadSelect_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers and Backspace
            AllowOnlyNumbersAndBackspace(e);
        }

        // --------------------------------------------------
        // Theme Select - ComboBox
        // --------------------------------------------------
        private void themeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Configure.theme = vm.Theme_SelectedItem;

            // Change Theme Resource
            App.Current.Resources.MergedDictionaries.Clear();
            App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("Themes/" + "Theme" + Configure.theme + ".xaml", UriKind.RelativeOrAbsolute)
            });

            // Save Theme for next launch
            //Settings.Default.Theme = vm.Theme_SelectedItem;
            //Settings.Default.Save();
            //Settings.Default.Reload();
        }


        /// <summary>
        ///    Updates Auto Check - Checked
        /// </summary>
        private void tglUpdateAutoCheck_Checked(object sender, RoutedEventArgs e)
        {
            // Update Toggle Text
            tblkUpdatesAutoCheck.Text = "On";

            //// Prevent Loading Corrupt App.Config
            //try
            //{
            //    Settings.Default.UpdateAutoCheck = vm.UpdateAutoCheck_IsChecked;
            //    Settings.Default.Save();
            //}
            //catch
            //{

            //}
        }
        /// <summary>
        ///    Updates Auto Check - Unchecked
        /// </summary>
        private void tglUpdateAutoCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            // Update Toggle Text
            tblkUpdatesAutoCheck.Text = "Off";

            //// Prevent Loading Corrupt App.Config
            //try
            //{
            //    Settings.Default.UpdateAutoCheck = vm.UpdateAutoCheck_IsChecked;
            //    Settings.Default.Save();
            //}
            //catch
            //{

            //}
        }


        // --------------------------------------------------
        // Reset Saved Settings - Button
        // --------------------------------------------------
        private void btnClearAllSavedSettings_Click(object sender, RoutedEventArgs e)
        {
            // Revert FFmpeg
            vm.FFmpegPath_Text = "<auto>";

            // Revert FFprobe
            vm.FFprobePath_Text = "<auto>";

            // Revert FFplay
            vm.FFplayPath_Text = "<auto>";

            // Revert youtube-dl
            vm.youtubedlPath_Text = "<auto>";

            // Revert Log
            vm.LogCheckBox_IsChecked = false;
            vm.LogPath_Text = string.Empty;

            // Revert Threads
            vm.Threads_SelectedItem = "optimal";


            // Yes/No Dialog Confirmation
            //
            MessageBoxResult result = MessageBox.Show(
                                                "Reset Saved Settings?",
                                                "Settings",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Exclamation
                                                );
            switch (result)
            {
                case MessageBoxResult.Yes:

                    // Reset AppData Settings
                    Settings.Default.Reset();
                    Settings.Default.Reload();

                    // Restart Program
                    Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();

                    break;

                case MessageBoxResult.No:

                    break;
            }
        }


        // --------------------------------------------------
        // Delete Saved Settings - Button
        // --------------------------------------------------
        private void btnDeleteSettings_Click(object sender, RoutedEventArgs e)
        {
            string userProfile = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%").TrimEnd('\\') + @"\";
            string appDataPath = @"AppData\Local\Axiom";

            // Check if Directory Exists
            if (Directory.Exists(userProfile + appDataPath))
            {
                // Show Yes No Window
                System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(
                    "Delete " + userProfile + appDataPath, "Delete Directory Confirm", System.Windows.Forms.MessageBoxButtons.YesNo);
                // Yes
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    // Delete
                    using (Process delete = new Process())
                    {
                        delete.StartInfo.UseShellExecute = false;
                        delete.StartInfo.CreateNoWindow = false;
                        delete.StartInfo.RedirectStandardOutput = true;
                        delete.StartInfo.FileName = "cmd.exe";
                        delete.StartInfo.Arguments = "/c RD /Q /S " + "\"" + userProfile + appDataPath;
                        delete.Start();
                        delete.WaitForExit();
                        //delete.Close();

                        delete.Dispose();
                    }

                    // Reset AppData Settings
                    Settings.Default.Reset();
                    Settings.Default.Reload();

                    // Restart Program
                    Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
                // No
                else if (dialogResult == System.Windows.Forms.DialogResult.No)
                {
                    //do nothing
                }
            }
            // If Axiom Folder Not Found
            else
            {
                MessageBox.Show("No Previous Settings Found.",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }


        /// <summary>
        ///    Is Valid Windows File Path
        /// </summary>
        /// <remarks>
        ///     Check for Invalid Characters
        /// </remarks>
        public static bool IsValidFilePath(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                // Not Valid
                string invalidChars = new string(Path.GetInvalidPathChars());
                Regex regex = new Regex("[" + Regex.Escape(invalidChars) + "]");

                if (regex.IsMatch(path)) { return false; };
            }

            // Empty
            else
            {
                return false;
            }

            // Is Valid
            return true;
        }


        /// <summary>
        ///    Is Valid Windows Filename
        /// </summary>
        /// <remarks>
        ///     Check for Invalid Characters
        /// </remarks>
        public static bool IsValidFilename(string filename)
        {
            // Not Valid
            string invalidChars = new string(Path.GetInvalidFileNameChars());
            Regex regex = new Regex("[" + Regex.Escape(invalidChars) + "]");

            if (regex.IsMatch(filename)) { return false; };

            // Is Valid
            return true;
        }



        /// <summary>
        ///    FFcheck (Method)
        /// </summary>
        /// <remarks>
        ///     Check if FFmpeg and FFprobe is on Computer 
        /// </remarks>
        public static bool FFcheck(ViewModel vm)
        {
            bool ready = true;

            try
            {
                // Environment Variables
                var envar = Environment.GetEnvironmentVariable("PATH");

                //MessageBox.Show(envar); //debug

                // -------------------------
                // FFmpeg
                // -------------------------
                // If Auto Mode
                if (vm.FFmpegPath_Text == "<auto>")
                {
                    // Check default current directory
                    if (File.Exists(appDir + "ffmpeg\\bin\\ffmpeg.exe"))
                    {
                        // let pass
                        return true;
                    }
                    else
                    {
                        int found = 0;

                        // Check Environment Variables
                        foreach (var envarPath in envar.Split(';'))
                        {
                            var exePath = Path.Combine(envarPath, "ffmpeg.exe");
                            if (File.Exists(exePath)) { found = 1; }
                        }

                        if (found == 1)
                        {
                            // let pass
                            return true;
                        }
                        else
                        {
                            // lock
                            MessageBox.Show("Cannot locate FFmpeg Path in Environment Variables or Current Folder.",
                                            "Error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Warning);

                            return false;
                        }

                    }
                }
                // If User Defined Path
                else if (vm.FFmpegPath_Text != "<auto>" && 
                        !string.IsNullOrEmpty(vm.FFprobePath_Text))
                {
                    var dirPath = Path.GetDirectoryName(vm.FFmpegPath_Text).TrimEnd('\\') + @"\";
                    var fullPath = Path.Combine(dirPath, "ffmpeg.exe");

                    // Make Sure ffmpeg.exe Exists
                    if (File.Exists(fullPath))
                    {
                        // let pass
                        return true;
                    }
                    else
                    {
                        // lock
                        MessageBox.Show("Cannot locate FFmpeg Path in User Defined Path.",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);

                        return false;
                    }

                    // If Configure Path is ffmpeg.exe and not another Program
                    //if (string.Equals(vm.FFmpegPath_Text, fullPath, StringComparison.CurrentCultureIgnoreCase))
                    //{
                    //    // let pass
                    //    //ffCheckCleared = true;
                    //    ready = true;
                    //}
                    //else
                    //{
                    //    /* lock */
                    //    //ready = false;
                    //    //ffCheckCleared = false;
                    //    MessageBox.Show("FFmpeg Path must link to ffmpeg.exe.",
                    //                    "Error",
                    //                    MessageBoxButton.OK,
                    //                    MessageBoxImage.Warning);

                    //    ready = false;
                    //}
                }

                // -------------------------
                // FFprobe
                // -------------------------
                // If Auto Mode
                if (vm.FFprobePath_Text == "<auto>")
                {
                    // Check default current directory
                    if (File.Exists(appDir + "ffmpeg\\bin\\ffprobe.exe"))
                    {
                        // let pass
                        return true;
                    }
                    else
                    {
                        int found = 0;

                        // Check Environment Variables
                        foreach (var envarPath in envar.Split(';'))
                        {
                            var exePath = Path.Combine(envarPath, "ffprobe.exe");
                            if (File.Exists(exePath)) { found = 1; }
                        }

                        if (found == 1)
                        {
                            // let pass
                            return true;
                        }
                        else
                        {
                            // lock
                            MessageBox.Show("Cannot locate FFprobe Path in Environment Variables or Current Folder.",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);

                            return false;
                        }

                    }
                }
                // If User Defined Path
                else if (vm.FFprobePath_Text != "<auto>" && !string.IsNullOrEmpty(vm.FFprobePath_Text))
                {
                    var dirPath = Path.GetDirectoryName(vm.FFprobePath_Text).TrimEnd('\\') + @"\";
                    var fullPath = Path.Combine(dirPath, "ffprobe.exe");

                    // Make Sure ffprobe.exe Exists
                    if (File.Exists(fullPath))
                    {
                        // let pass
                        return true;
                    }
                    else
                    {
                        // lock
                        MessageBox.Show("Cannot locate FFprobe Path in User Defined Path.",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);

                        return true;
                    }

                    //// If Configure Path is FFmpeg.exe and not another Program
                    //if (string.Equals(vm.FFprobePath_Text, fullPath, StringComparison.CurrentCultureIgnoreCase))
                    //{
                    //    // let pass
                    //    //ffCheckCleared = true;
                    //    //ready = true;
                    //    return true;
                    //}
                    //else
                    //{
                    //    /* lock */
                    //    //ready = false;
                    //    //ffCheckCleared = false;
                    //    MessageBox.Show("Error: FFprobe Path must link to ffprobe.exe.",
                    //                    "Error",
                    //                    MessageBoxButton.OK,
                    //                    MessageBoxImage.Warning);

                    //    ready = false;
                    //    return false;
                    //}
                }
            }
            catch
            {
                MessageBox.Show("Unknown Error trying to locate FFmpeg or FFprobe.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }

            //MessageBox.Show(ready.ToString()); //debug

            return ready;
        }


        /// <summary>
        ///    FFmpeg Path (Method)
        /// </summary>
        public static String FFmpegPath(ViewModel vm)
        {
            // -------------------------
            // FFmpeg.exe and FFprobe.exe Paths
            // -------------------------
            // If Configure FFmpeg Path is <auto>
            if (vm.FFmpegPath_Text == "<auto>")
            {
                if (File.Exists(appDir + "ffmpeg\\bin\\ffmpeg.exe"))
                {
                    // use included binary
                    FFmpeg.ffmpeg = "\"" + appDir + "ffmpeg\\bin\\ffmpeg.exe" + "\"";
                }
                else if (!File.Exists(appDir + "ffmpeg\\bin\\ffmpeg.exe"))
                {
                    // use system installed binaries
                    FFmpeg.ffmpeg = "ffmpeg";
                }
            }
            // Use User Custom Path
            else
            {
                FFmpeg.ffmpeg = "\"" + vm.FFmpegPath_Text + "\"";
            }

            // Return Value
            return FFmpeg.ffmpeg;
        }


        /// <summary>
        ///     FFprobe Path
        /// </summary>
        public static void FFprobePath(ViewModel vm)
        {
            // If Configure FFprobe Path is <auto>
            if (vm.FFprobePath_Text == "<auto>")
            {
                if (File.Exists(appDir + "ffmpeg\\bin\\ffprobe.exe"))
                {
                    // use included binary
                    FFprobe.ffprobe = "\"" + appDir + "ffmpeg\\bin\\ffprobe.exe" + "\"";
                }
                else if (!File.Exists(appDir + "ffmpeg\\bin\\ffprobe.exe"))
                {
                    // use system installed binaries
                    FFprobe.ffprobe = "ffprobe";
                }
            }
            // Use User Custom Path
            else
            {
                FFprobe.ffprobe = "\"" + vm.FFprobePath_Text + "\"";
            }

            // Return Value
            //return FFprobe.ffprobe;
        }


        /// <summary>
        ///     FFplay Path
        /// </summary>
        public static void FFplayPath(ViewModel vm)
        {
            // If Configure FFprobe Path is <auto>
            if (vm.FFplayPath_Text == "<auto>")
            {
                if (File.Exists(appDir + @"ffmpeg\bin\ffplay.exe"))
                {
                    // use included binary
                    FFplay.ffplay = "\"" + appDir + @"ffmpeg\bin\ffplay.exe" + "\"";
                }
                else if (!File.Exists(appDir + @"ffmpeg\bin\ffplay.exe"))
                {
                    // use system installed binaries
                    FFplay.ffplay = "ffplay";
                }
            }
            // Use User Custom Path
            else
            {
                FFplay.ffplay = "\"" + vm.FFplayPath_Text + "\"";
            }

            // Return Value
            //return FFplay.ffplay;
        }


        /// <summary>
        ///     youtube-dl Path
        /// </summary>
        public static void youtubedlPath(ViewModel vm)
        {
            // If Configure youtubedl Path is <auto>
            if (vm.youtubedlPath_Text == "<auto>")
            {
                // youtube-dl.exe Exists
                if (File.Exists(appDir + @"youtube-dl\youtube-dl.exe"))
                {
                    // use included binary path
                    youtubedl = appDir + @"youtube-dl\youtube-dl.exe";
                }
                else if (File.Exists(appDir + @"youtube-dl.exe"))
                {
                    // moved from folder
                    youtubedl = appDir + @"youtube-dl.exe";
                }

                // youtube-dl.exe Does Not Exist
                else if (!File.Exists(appDir + @"youtube-dl\youtube-dl.exe"))
                {
                    // Installed
                    // Environment Variable auto path
                    youtubedl = @"youtube-dl";
                }
            }

            // Use User Custom Path
            else
            {
                youtubedl = vm.youtubedlPath_Text;
            }
        }



        /// <summary>
        ///    Thread Detect
        /// </summary>
        public static String ThreadDetect(ViewModel vm)
        {
            // -------------------------
            // Default
            // -------------------------
            if (vm.Threads_SelectedItem == "default")
            {
                Configure.threads = string.Empty;
            }

            // -------------------------
            // Optimal
            // -------------------------
            else if (vm.Threads_SelectedItem == "optimal" ||
                     string.IsNullOrEmpty(Configure.threads))
            {
                Configure.threads = "-threads 0";
            }

            // -------------------------
            // All
            // -------------------------
            else if (vm.Threads_SelectedItem == "all" ||
                     string.IsNullOrEmpty(Configure.threads))
            {
                Configure.threads = "-threads " + Configure.maxthreads;
            }

            // -------------------------
            // Custom
            // -------------------------
            else
            {
                Configure.threads = "-threads " + vm.Threads_SelectedItem;
            }

            // Return Value
            return Configure.threads;
        }


        /// <summary>
        ///    Is Valid URL
        /// </summary>
        //public static bool IsValidURL(string source)
        //{
        //    Uri uriResult;
        //    return Uri.TryCreate(source, UriKind.Absolute, out uriResult) && (uriResult.Scheme == "http" || uriResult.Scheme == "https");
        //}


        /// <summary>
        ///    Is Website URL
        /// </summary>
        public static bool IsWebURL(string input_Text)
        {
            if (!string.IsNullOrEmpty(input_Text))
            {
                // URL
                if ((input_Text.StartsWith("http://") ||
                    input_Text.StartsWith("https://") ||
                    input_Text.StartsWith("www.")) //||
                    //input_Text.EndsWith(".com")) //&&
                    //IsValidURL(input_Text) == true
                   )
                {
                    return true;
                }

                // Local File
                else
                {
                    return false;
                }
            }
            
            // Empty
            else
            {
                return false;
            }
        }


        /// <summary>
        ///    Is Website URL
        /// </summary>
        public static bool IsYouTubeURL(string input_Text)
        {
            if (!string.IsNullOrEmpty(input_Text))
            {
                // YouTube
                if (// youtube (any domain extension)
                   input_Text.StartsWith("https://www.youtube.") ||
                   input_Text.StartsWith("http://www.youtube.") ||
                   input_Text.StartsWith("www.youtube.") ||
                   input_Text.StartsWith("youtube.") ||

                   // youtu.be
                   input_Text.StartsWith("https://youtu.be") ||
                   input_Text.StartsWith("http://youtu.be") ||
                   input_Text.StartsWith("www.youtu.be") ||
                   input_Text.StartsWith("youtu.be") ||

                   // YouTube Music
                   input_Text.StartsWith("https://music.youtube.") ||
                   input_Text.StartsWith("http://music.youtube.") ||
                   input_Text.StartsWith("music.youtube.")
                   )
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            // Empty
            else
            {
                return false;
            }
        }


        /// <summary>
        ///    YouTube Download-Only Mode Check (Method)
        /// </summary>
        /// <remarks>
        ///     If Axiom is in full Codec Copy mode Download the file without converting
        /// </remarks>
        public static bool IsWebDownloadOnly(string videoCodec_SelectedItem,
                                             string subtitleCodec_SelectedItem,
                                             string audioCodec_SelectedItem
                                             )
        {
            if (//IsYouTubeURL(vm.Input_Text) == true &&
                
                // Video
                (videoCodec_SelectedItem == "Copy" &&
                 subtitleCodec_SelectedItem == "Copy" &&
                 audioCodec_SelectedItem == "Copy") 
                 
                 ||

                (videoCodec_SelectedItem == "Copy" &&
                 subtitleCodec_SelectedItem == "Copy") 
                 
                 ||

                (videoCodec_SelectedItem == "Copy" &&
                 subtitleCodec_SelectedItem == "None") 
                 
                 ||

                 //(videoCodec_SelectedItem == "Copy" &&
                 //subtitleCodec_SelectedItem == "Copy" &&
                 //audioCodec_SelectedItem == "None") ||

                //(videoCodec_SelectedItem == "Copy" &&
                // subtitleCodec_SelectedItem == "None" &&
                // audioCodec_SelectedItem == "None") ||

                (videoCodec_SelectedItem == "Copy" &&
                 subtitleCodec_SelectedItem == "None" &&
                 audioCodec_SelectedItem == "Copy") 
                 
                 ||

                // Music
                (videoCodec_SelectedItem == "None" &&
                 subtitleCodec_SelectedItem == "None" &&
                 audioCodec_SelectedItem == "Copy")
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        ///     YouTube Download - FFmpeg Path
        /// </summary>
        public static String YouTubeDL_FFmpegPath(ViewModel vm)
        {
            // youtube-dl
            // FFmpeg must be detected by youtube-dl to merge video+audio into a single file
            // If using Environment Variables, path will be only 'ffmpeg'
            // If defining ffmpeg location, do not use "--ffmpeg-location ffmpeg", it will fail
            // You must use a full path --ffmpeg-location "C:\Path\To\ffmpeg.exe"

            string path = FFmpegPath(vm);

            // Environment Variables
            if (path == "ffmpeg")
            {
                // Do not specify a path if using Environment Variables
                // It will be detected by youtube-dl automatically
                return string.Empty;
            }

            // Missing
            else if (string.IsNullOrEmpty(path))
            {
                // Let youtube-dl throw error
                return string.Empty;
            }

            // Specify ffmpeg.exe path
            else
            {
                return " --ffmpeg-location " + path;
            }
        }



        /// <summary>
        ///    Convert Button Text Change (Method)
        /// </summary>
        public static void ConvertButtonText(ViewModel vm)
        {
            //MessageBox.Show(vm.Input_Text); //debug

            // Change to "Download" if YouTube Download-Only Mode
            if ((IsWebURL(vm.Input_Text) == true || IsYouTubeURL(vm.Input_Text) == true) &&
                IsWebDownloadOnly(vm.Video_Codec_SelectedItem, 
                                  vm.Subtitle_Codec_SelectedItem, 
                                  vm.Audio_Codec_SelectedItem) == true
                )
            {
                vm.Convert_Text = "Download";
            }

            // Change to Convert if User Defined Custom Settings
            else
            {
                vm.Convert_Text = "Convert";
            }
        }


        /// <summary>
        ///    YouTube Download Check (Method)
        /// </summary>
        /// <remarks>
        ///     Check if youtube-dl.exe is on Computer 
        /// </remarks>
        public static bool YouTubeDownloadCheck(ViewModel vm)
        {
            if (File.Exists(youtubedl))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        ///    YouTube Download - URL (Method)
        /// </summary>
        public static String YouTubeDownloadURL(string url)
        {
            // Strip URL Parameters
            int index = url.IndexOf("&");
            if (index > 0)
                url = url.Substring(0, index);

            return url;
        }


        /// <summary>
        ///     YouTube Download - Format (Method)
        /// </summary>
        /// <remarks>
        ///     For YouTube downloads - use mp4, as most users can play this format on their PC
        ///     For Other Websites - use mkv for merging format, in case the video+audio codecs can't be merged to mp4
        /// </remarks>
        public static String YouTubeDownloadFormat(string youtubedl_SelectedItem,
                                                   string videoCodec_SelectedItem,
                                                   string subtitleCodec_SelectedItem,
                                                   string audioCodec_SelectedItem
                                                   )
        {
            // Video + Audio
            if (youtubedl_SelectedItem == "Video + Audio")
            {
                // Use mp4 for Download-Only Mode
                if (IsWebDownloadOnly(videoCodec_SelectedItem, 
                                      subtitleCodec_SelectedItem, 
                                      audioCodec_SelectedItem) == true
                                      ) 
                {
                    return "mp4";
                }

                // Use mkv for converting
                else
                {
                    return "mkv";
                }
            }

            // Video Only
            else if (youtubedl_SelectedItem == "Video Only")
            {
                // Use mp4 for Download-Only Mode
                if (IsWebDownloadOnly(videoCodec_SelectedItem,
                                      subtitleCodec_SelectedItem,
                                      audioCodec_SelectedItem) == true
                                      ) 
                {
                    return "mp4";
                }

                // Use mkv for converting
                else
                {
                    return "mkv";
                }
            }

            // Audio Only
            else if (youtubedl_SelectedItem == "Audio Only")
            {
                // Can only use m4a, not mp3

                return "m4a";
            }

            return string.Empty;
        }


        /// <summary>
        ///    YouTube Download - Quality (Method)
        /// </summary>
        public static String YouTubeDownloadQuality(string input_Text,
                                                    string youtubedl_SelectedItem, 
                                                    string youtubedl_Quality_SelectedItem
                                                    )
        {
            // -------------------------
            // Video + Audio
            // -------------------------
            if (youtubedl_SelectedItem == "Video + Audio")
            {
                // Best
                if (youtubedl_Quality_SelectedItem == "best")
                {
                    return "bestvideo[ext=mp4]+bestaudio[ext=m4a]/bestvideo+bestaudio/best";
                }
                // Best 4K
                else if (youtubedl_Quality_SelectedItem == "best 4K")
                {
                    return "bestvideo[height=2160][ext=mp4]+bestaudio[ext=m4a]/bestvideo[height=2160]+bestaudio/bestvideo[ext=mp4]+bestaudio[ext=m4a]/bestvideo+bestaudio/best";
                }
                // Best 1080p
                else if (youtubedl_Quality_SelectedItem == "best 1080p")
                {
                    return "bestvideo[height=1080][ext=mp4]+bestaudio[ext=m4a]/bestvideo[height=1080]+bestaudio/bestvideo[ext=mp4]+bestaudio[ext=m4a]/bestvideo+bestaudio/best";
                }
                // Best 720p
                else if (youtubedl_Quality_SelectedItem == "best 720p")
                {
                    return "bestvideo[height=720][ext=mp4]+bestaudio[ext=m4a]/bestvideo[height=720]+bestaudio/bestvideo[ext=mp4]+bestaudio[ext=m4a]/bestvideo+bestaudio/best";
                }
                // Best 480p
                else if (youtubedl_Quality_SelectedItem == "best 480p")
                {
                    return "bestvideo[height=480][ext=mp4]+bestaudio[ext=m4a]/bestvideo[height=480]+bestaudio/bestvideo[ext=mp4]+bestaudio[ext=m4a]/bestvideo+bestaudio/best";
                }
                // Worst
                else if (youtubedl_Quality_SelectedItem == "worst")
                {
                    return "worstvideo[ext=mp4]+worstaudio[ext=m4a]/worstvideo+worstaudio/worst";
                }
            }

            // -------------------------
            // Video Only
            // -------------------------
            else if (youtubedl_SelectedItem == "Video Only")
            {
                // Best
                if (youtubedl_Quality_SelectedItem == "best")
                {
                    return "bestvideo[ext=mp4]/bestvideo";
                }
                // Best 4K
                else if (youtubedl_Quality_SelectedItem == "best 4K")
                {
                    return "bestvideo[height=2160][ext=mp4]/bestvideo[ext=mp4]/bestvideo";
                }
                // Best 1080p
                else if (youtubedl_Quality_SelectedItem == "best 1080p")
                {
                    return "bestvideo[height=1080][ext=mp4]/bestvideo[ext=mp4]/bestvideo";
                }
                // Best 720p
                else if (youtubedl_Quality_SelectedItem == "best 720p")
                {
                    return "bestvideo[height=720p][ext=mp4]/bestvideo[ext=mp4]/bestvideo";
                }
                // Best 480p
                else if (youtubedl_Quality_SelectedItem == "best 480p")
                {
                    return "bestvideo[height=480p][ext=mp4]/bestvideo[ext=mp4]/bestvideo";
                }
                // Worst
                else if (youtubedl_Quality_SelectedItem == "worst")
                {
                    return "worstvideo[ext=mp4]/worstvideo";
                }
            }

            // -------------------------
            // Audio Only
            // -------------------------
            else if (youtubedl_SelectedItem == "Audio Only")
            {
                // Best
                if (youtubedl_Quality_SelectedItem == "best")
                {
                    return "bestaudio[ext=m4a]/bestaudio";
                }
                // Worst
                else if (youtubedl_Quality_SelectedItem == "worst")
                {
                    return "worstaudio[ext=m4a]/worstaudio";
                }
            }


            return string.Empty;
        }


        /// <summary>
        ///    YouTube Method - Selection Changed
        /// </summary>
        private void cboYouTube_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Video + Audio
            // Video Only
            if (vm.Format_YouTube_SelectedItem == "Video + Audio" ||
                vm.Format_YouTube_SelectedItem == "Video Only")
            {
                // Change Items Source
                vm.Format_YouTube_Quality_Items = new List<string>()
                {
                    "best",
                    "best 4K",
                    "best 1080p",
                    "best 720p",
                    "best 480p",
                    "worst"
                };

                // Select Default
                vm.Format_YouTube_Quality_SelectedItem = "best";
            }

            // Audio Only
            else if (vm.Format_YouTube_SelectedItem == "Audio Only")
            {
                // Change Items Source
                vm.Format_YouTube_Quality_Items = new List<string>()
                {
                    "best",
                    "worst"
                };

                // Select Default
                vm.Format_YouTube_Quality_SelectedItem = "best";
            }
        }



        /// <summary>
        ///     YouTube Download - Merge Output Format
        /// </summary>
        //public static String YouTubeDownload_MergeOutputFormat(ViewModel vm)
        //{

        //    if (IsWebDownloadOnly() == true)
        //    {
        //        return string.Empty;
        //    }
        //    else
        //    {
        //        return "--merge-output-format " + YouTubeDownloadFormat(vm.Format_YouTube_SelectedItem,
        //                                                                vm.Video_Codec_SelectedItem,
        //                                                                vm.Subtitle_Codec_SelectedItem,
        //                                                                vm.Audio_Codec_SelectedItem
        //                                                                );
        //    }
        //}



        /// <summary>
        ///    Check if Script has been Edited (Method)
        /// </summary>
        public static bool CheckScriptEdited(ViewModel vm)
        {
            bool edited = false;

            // -------------------------
            // Check if Script has been modified
            // -------------------------
            if (!string.IsNullOrEmpty(vm.ScriptView_Text) && 
                !string.IsNullOrEmpty(FFmpeg.ffmpegArgs))
            {
                //MessageBox.Show(RemoveLineBreaks(ScriptView.GetScriptRichTextBoxContents(mainwindow))); //debug
                //MessageBox.Show(FFmpeg.ffmpegArgs); //debug

                // Compare RichTextBox Script Against FFmpeg Generated Args
                if (RemoveLineBreaks(vm.ScriptView_Text) != FFmpeg.ffmpegArgs)
                {
                    // Yes/No Dialog Confirmation
                    MessageBoxResult result = MessageBox.Show("The Convert button will override and replace your custom script with the selected controls."
                                                              + "\r\n\r\nPress the Run button instead to execute your script."
                                                              + "\r\n\r\nContinue Convert?",
                                                              "Edited Script Detected",
                                                              MessageBoxButton.YesNo,
                                                              MessageBoxImage.Information);

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            // Continue
                            break;
                        case MessageBoxResult.No:
                            // Halt
                            edited = true;
                            break;
                    }
                }
            }

            return edited;
        }


        /// <summary>
        ///    Ready Halts (Method)
        /// </summary>
        public static bool ReadyHalts(ViewModel vm)
        {
            // -------------------------
            // Check if FFmpeg & FFprobe Exists
            // -------------------------
            if (FFcheck(vm) == false)
            {
                // Halt
                return false;
            }

            //MessageBox.Show(FFcheck(vm).ToString()); //debug
            //MessageBox.Show(ready.ToString()); //debug

            // -------------------------
            // Input File does not exist
            // -------------------------
            //MessageBox.Show(input); //debug
            if (IsWebURL(vm.Input_Text) == false) // Ignore Web URL's
            {
                if (!string.IsNullOrEmpty(vm.Input_Text) &&
                    vm.Batch_IsChecked == false)
                {
                    if (!File.Exists(vm.Input_Text))
                    {
                        MessageBox.Show("Input file does not exist.",
                                        "Notice",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Exclamation);

                        // Halt
                        return false;
                    }
                }
            }

            // -------------------------
            // YouTube Download - URL Missing
            // -------------------------
            // Needs Preset Video/Music detection
            //if (IsYouTubeDownloadOnly(vm) == true)
            //{

            //}

            // -------------------------
            // Do Not allow Auto without FFprobe being installed or linked
            // -------------------------
            if (string.IsNullOrEmpty(FFprobe.ffprobe))
            {
                if (vm.Video_Quality_SelectedItem == "Auto" ||
                    vm.Audio_Quality_SelectedItem == "Auto")
                {
                    // Log Console Message /////////
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Auto Quality Mode Requires FFprobe in order to Detect File Info.")) { Foreground = Log.ConsoleWarning });

                    MessageBox.Show("Auto Quality Mode Requires FFprobe in order to Detect File Info.",
                                    "Notice",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);

                    // Halt
                    return false;
                }
            }


            // -------------------------
            // Crop Codec Copy
            // -------------------------
            if (!string.IsNullOrEmpty(CropWindow.crop) &&
                vm.Video_Codec_SelectedItem == "Copy") //null check
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Warning: Crop cannot use Codec Copy. Please select a Video Codec.")) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);

                // Warning
                MessageBox.Show("Crop cannot use Codec Copy. Please select a Video Codec.",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);

                return false;
            }


            // -------------------------
            // Video BitRate is missing K or M at end of value
            // -------------------------
            if (vm.Video_Quality_SelectedItem == "Custom" &&
                vm.Video_BitRate_IsEnabled == true &&
                vm.Video_BitRate_Text != "0" && // Constant Bit Rate 0 does not need K or M
                vm.Video_VBR_IsChecked != true)
            {
                // Error List
                List<string> errors = new List<string>();

                // Bit Rate
                if (!string.IsNullOrEmpty(vm.Video_BitRate_Text))
                {
                    if (vm.Video_BitRate_Text.ToUpper()?.Contains("K") != true &&
                        vm.Video_BitRate_Text.ToUpper()?.Contains("M") != true)
                    {
                        errors.Add("Bit Rate");
                    }
                }

                // Min Rate
                if (!string.IsNullOrEmpty(vm.Video_MinRate_Text))
                {
                    if (vm.Video_MinRate_Text.ToUpper()?.Contains("K") != true &&
                        vm.Video_MinRate_Text.ToUpper()?.Contains("M") != true)
                    {
                        errors.Add("Min Rate");
                    }
                }

                // Max Rate
                if (!string.IsNullOrEmpty(vm.Video_MaxRate_Text))
                {
                    if (vm.Video_MaxRate_Text.ToUpper()?.Contains("K") != true &&
                        vm.Video_MaxRate_Text.ToUpper()?.Contains("M") != true)
                    {
                        errors.Add("Max Rate");
                    }
                }

                // Buf Size
                if (!string.IsNullOrEmpty(vm.Video_BufSize_Text))
                {
                    if (vm.Video_BufSize_Text.ToUpper()?.Contains("K") != true &&
                        vm.Video_BufSize_Text.ToUpper()?.Contains("M") != true)
                    {
                        errors.Add("Buf Size");
                    }
                }


                // Halt and Display Errors
                if (errors != null &&
                    errors.Count > 0)
                {
                    // Log Console Message /////////
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Video Bit Rate is missing K or M at end of value.")) { Foreground = Log.ConsoleWarning });

                    // Warning
                    MessageBox.Show("Video " + string.Join(", ", errors) + " missing K or M at end of value.",
                                    "Notice",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);

                    // Halt
                    return false;
                }
            }


            // -------------------------
            // Single File Input with no Extension
            // -------------------------
            if (vm.Batch_IsChecked == false && 
                vm.Input_Text.EndsWith("\\"))
            {
                // Log Console Message /////////
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Please choose an input file.")) { Foreground = Log.ConsoleWarning });

                // Warning
                MessageBox.Show("Please choose an input file.",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);

                // Halt
                return false;
            }


            // -------------------------
            // Do Not allow Batch Copy to same folder if file extensions are the same (to avoid file overwrite)
            // -------------------------
            if (vm.Batch_IsChecked == true)
            {
                if (string.Equals(inputDir, outputDir, StringComparison.CurrentCultureIgnoreCase) &&
                    string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
                {
                    //MessageBox.Show("inputDir = " + inputDir); //debug
                    //MessageBox.Show("outputDir = " + outputDir); //debug
                    //MessageBox.Show("inputExt = " + inputExt); //debug
                    //MessageBox.Show("outputExt = " + outputExt); //debug

                    // Log Console Message /////////
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Please choose an output folder different than the input folder to avoid file overwrite.")) { Foreground = Log.ConsoleWarning });

                    // Warning
                    MessageBox.Show("Please choose an output folder different than the input folder to avoid file overwrite.",
                                    "Notice",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);

                    // Halt
                    return false;
                }
            }


            // -------------------------
            // VP8/VP9 & CRF does not have BitRate -b:v
            // -------------------------
            if (vm.Video_Codec_SelectedItem == "VP8" ||
                vm.Video_Codec_SelectedItem == "VP9")
            {
                if (!string.IsNullOrEmpty(vm.Video_CRF_Text) &&
                    string.IsNullOrEmpty(vm.Video_BitRate_Text))
                {
                    // Log Console Message /////////
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: VP8/VP9 CRF must also have Bit Rate. \n(e.g. 0 for Constant, 1234k for Constrained)")) { Foreground = Log.ConsoleWarning });

                    // Notice
                    MessageBox.Show("VP8/VP9 CRF must also have Bit Rate. \n(e.g. 0 for Constant, 1234K for Constrained)",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);

                    // Halt
                    return false;
                }
            }


            // -------------------------
            // Ready
            // -------------------------
            return true;
        }


        /// <summary>
        ///     System Info
        /// </summary>
        public void SystemInfoDisplay()
        {
            //int count = 0;
            //await Task.Factory.StartNew(() =>
            //{
            // -----------------------------------------------------------------
            /// <summary>
            ///     System Info
            /// </summary>
            /// <remarks>
            ///     Detect and Display System Hardware
            /// </remarks>
            // -----------------------------------------------------------------
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("System Info:")) { Foreground = Log.ConsoleAction });
            Log.logParagraph.Inlines.Add(new LineBreak());

            // -------------------------
            // OS
            // -------------------------
            try
            {
                ManagementClass os = new ManagementClass("Win32_OperatingSystem");

                foreach (ManagementObject obj in os.GetInstances())
                {
                    // Log Console Message /////////
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(obj["Caption"])) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());

                }
                os.Dispose();
            }
            catch
            {

            }

            // -------------------------
            // CPU
            // -------------------------
            try
            {
                ManagementObjectSearcher cpu = new ManagementObjectSearcher("root\\CIMV2", "SELECT Name FROM Win32_Processor");

                foreach (ManagementObject obj in cpu.Get())
                {
                    // Log Console Message /////////
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(obj["Name"])) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                }
                cpu.Dispose();

                // Max Threads
                foreach (var item in new System.Management.ManagementObjectSearcher("Select NumberOfLogicalProcessors FROM Win32_ComputerSystem").Get())
                {
                    Configure.maxthreads = String.Format("{0}", item["NumberOfLogicalProcessors"]);
                }
            }
            catch
            {

            }

            // -------------------------
            // GPU
            // -------------------------
            try
            {
                ManagementObjectSearcher gpu = new ManagementObjectSearcher("root\\CIMV2", "SELECT Name, AdapterRAM FROM Win32_VideoController");

                foreach (ManagementObject obj in gpu.Get())
                {
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(obj["Name"]) + " " + Convert.ToString(Math.Round(Convert.ToDouble(obj["AdapterRAM"]) * 0.000000001, 3) + "GB")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                }
            }
            catch
            {

            }

            // -------------------------
            // RAM
            // -------------------------
            try
            {
                Log.logParagraph.Inlines.Add(new Run("RAM ") { Foreground = Log.ConsoleDefault });

                double capacity = 0;
                //int memtype = 0;
                string type;
                int speed = 0;

                ManagementObjectSearcher ram = new ManagementObjectSearcher("root\\CIMV2", "SELECT Capacity, MemoryType, Speed FROM Win32_PhysicalMemory");

                foreach (ManagementObject obj in ram.Get())
                {
                    capacity += Convert.ToDouble(obj["Capacity"]);
                    //memtype = Int32.Parse(obj.GetPropertyValue("MemoryType").ToString());
                    speed = Int32.Parse(obj.GetPropertyValue("Speed").ToString());
                }

                capacity *= 0.000000001; // Convert Byte to GB
                capacity = Math.Round(capacity, 3); // Round to 3 decimal places

                // Select RAM Type
                type = RamInfo.MemType;
                //switch (memtype)
                //{
                //    case 20:
                //        type = "DDR";
                //        break;
                //    case 21:
                //        type = "DDR2";
                //        break;
                //    case 17:
                //        type = "SDRAM";
                //        break;
                //    default:
                //        if (memtype == 0 || memtype > 22)
                //            type = "DDR3";
                //        else
                //            type = "Unknown";
                //        break;
                //}

                // Log Console Message /////////
                Log.logParagraph.Inlines.Add(new Run(Convert.ToString(capacity) + "GB " + type + " " + Convert.ToString(speed) + "MHz") { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new LineBreak());

                ram.Dispose();
            }
            catch
            {

            }
            
            // End System Info

            //});

            //return count;
        }


        /// <summary>
        ///    RAM Type
        /// <summary>
        public class RamInfo
        {
            public static string MemType
            {
                get
                {
                    int type = 0;

                    ConnectionOptions connection = new ConnectionOptions();
                    connection.Impersonation = ImpersonationLevel.Impersonate;
                    ManagementScope scope = new ManagementScope("\\\\.\\root\\CIMV2", connection);
                    scope.Connect();
                    ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        type = Convert.ToInt32(queryObj["MemoryType"]);
                    }

                    return TypeString(type);
                }
            }

            private static string TypeString(int type)
            {
                string outValue = string.Empty;

                switch (type)
                {
                    case 0x0: outValue = "Unknown"; break;
                    case 0x1: outValue = "Other"; break;
                    case 0x2: outValue = "DRAM"; break;
                    case 0x3: outValue = "Synchronous DRAM"; break;
                    case 0x4: outValue = "Cache DRAM"; break;
                    case 0x5: outValue = "EDO"; break;
                    case 0x6: outValue = "EDRAM"; break;
                    case 0x7: outValue = "VRAM"; break;
                    case 0x8: outValue = "SRAM"; break;
                    case 0x9: outValue = "RAM"; break;
                    case 0xa: outValue = "ROM"; break;
                    case 0xb: outValue = "Flash"; break;
                    case 0xc: outValue = "EEPROM"; break;
                    case 0xd: outValue = "FEPROM"; break;
                    case 0xe: outValue = "EPROM"; break;
                    case 0xf: outValue = "CDRAM"; break;
                    case 0x10: outValue = "3DRAM"; break;
                    case 0x11: outValue = "SDRAM"; break;
                    case 0x12: outValue = "SGRAM"; break;
                    case 0x13: outValue = "RDRAM"; break;
                    case 0x14: outValue = "DDR"; break;
                    case 0x15: outValue = "DDR2"; break;
                    case 0x16: outValue = "DDR2 FB-DIMM"; break;
                    case 0x17: outValue = "Undefined 23"; break;
                    case 0x18: outValue = "DDR3"; break;
                    case 0x19: outValue = "FBD2"; break;
                    case 0x1a: outValue = "DDR4"; break;
                    default: outValue = "Undefined"; break;
                }

                return outValue;
            }
        }



        /// <summary>
        ///     Normalize Value (Method)
        /// <summary>
        public static double NormalizeValue(double val, double valmin, double valmax, double min, double max, double midpoint)
        {
            double mid = (valmin + valmax) / 2.0;
            if (val < mid)
            {
                return (val - valmin) / (mid - valmin) * (midpoint - min) + min;
            }
            else
            {
                return (val - mid) / (valmax - mid) * (max - midpoint) + midpoint;
            }
        }
        //public static double NormalizeValue(double val, double valmin, double valmax, double min, double max, double ffdefault)
        //{
        //    // (((sliderValue - sliderValueMin) / (sliderValueMax - sliderValueMin)) * (NormalizeMax - NormalizeMin)) + NormalizeMin

        //    return (((val - valmin) / (valmax - valmin)) * (max - min)) + min;
        //}



        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     CONTROLS
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        ///     Info Button
        /// </summary>
        private Boolean IsInfoWindowOpened = false;
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            // Prevent Monitor Resolution Window Crash
            //
            try
            {
                // Check if Window is already open
                if (IsInfoWindowOpened) return;

                // Start Window
                infowindow = new InfoWindow();

                // Only allow 1 Window instance
                infowindow.ContentRendered += delegate { IsInfoWindowOpened = true; };
                infowindow.Closed += delegate { IsInfoWindowOpened = false; };

                // Keep Window on Top
                infowindow.Owner = Window.GetWindow(this);

                // Detect which screen we're on
                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                var thisScreen = allScreens.SingleOrDefault(s => Left >= s.WorkingArea.Left && Left < s.WorkingArea.Right);
                if (thisScreen == null) thisScreen = allScreens.First();

                // Position Relative to MainWindow
                infowindow.Left = Math.Max((Left + (Width - infowindow.Width) / 2), thisScreen.WorkingArea.Left);
                infowindow.Top = Math.Max((Top + (Height - infowindow.Height) / 2), thisScreen.WorkingArea.Top);

                // Open Window
                infowindow.Show();
            }
            // Simplified
            catch
            {
                // Check if Window is already open
                if (IsInfoWindowOpened) return;

                // Start Window
                infowindow = new InfoWindow();

                // Only allow 1 Window instance
                infowindow.ContentRendered += delegate { IsInfoWindowOpened = true; };
                infowindow.Closed += delegate { IsInfoWindowOpened = false; };

                // Keep Window on Top
                infowindow.Owner = Window.GetWindow(this);

                // Position Relative to MainWindow
                infowindow.Left = Math.Max((Left + (Width - infowindow.Width) / 2), Left);
                infowindow.Top = Math.Max((Top + (Height - infowindow.Height) / 2), Top);

                // Open Window
                infowindow.Show();
            }
        }

        /// <summary>
        ///    Website Button
        /// </summary>
        private void btbWebsite_Click(object sender, RoutedEventArgs e)
        {
            // Open Axiom Website URL in Default Browser
            Process.Start("https://axiomui.github.io");

        }

        /// <summary>
        ///    Update Button
        /// </summary>
        private Boolean IsUpdateWindowOpened = false;
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Proceed if Internet Connection
            // -------------------------
            if (UpdateWindow.CheckForInternetConnection() == true)
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                WebClient wc = new WebClient();
                wc.Headers.Add(HttpRequestHeader.UserAgent, "Axiom (https://github.com/MattMcManis/Axiom)" + " v" + currentVersion + "-" + currentBuildPhase + " Update Check");
                wc.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
                wc.Headers.Add("accept-language", "en-US,en;q=0.9");
                wc.Headers.Add("dnt", "1");
                wc.Headers.Add("upgrade-insecure-requests", "1");
                //wc.Headers.Add("accept-encoding", "gzip, deflate, br"); //error

                // -------------------------
                // Parse GitHub .version file
                // -------------------------
                string parseLatestVersion = string.Empty;

                try
                {
                    parseLatestVersion = wc.DownloadString("https://raw.githubusercontent.com/MattMcManis/Axiom/master/.version");
                }
                catch
                {
                    MessageBox.Show("GitHub version file not found.",
                                    "Notice",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);

                    return;
                }

                // -------------------------
                // Split Version & Build Phase by dash
                // -------------------------
                if (!string.IsNullOrEmpty(parseLatestVersion)) //null check
                {
                    try
                    {
                        // Split Version and Build Phase
                        splitVersionBuildPhase = Convert.ToString(parseLatestVersion).Split('-');

                        // Set Version Number
                        latestVersion = new Version(splitVersionBuildPhase[0]); //number
                        latestBuildPhase = splitVersionBuildPhase[1]; //alpha
                    }
                    catch
                    {
                        MessageBox.Show("Error reading version.",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);

                        return;
                    }

                    // Debug
                    //MessageBox.Show(Convert.ToString(latestVersion));
                    //MessageBox.Show(latestBuildPhase);

                    // -------------------------
                    // Check if Axiom is the Latest Version
                    // -------------------------
                    // Update Available
                    if (latestVersion > currentVersion)
                    {
                        // Yes/No Dialog Confirmation
                        //
                        MessageBoxResult result = MessageBox.Show("v" + Convert.ToString(latestVersion) + "-" + latestBuildPhase + "\n\nDownload Update?",
                                                                  "Update Available",
                                                                  MessageBoxButton.YesNo
                                                                  );
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                // Check if Window is already open
                                if (IsUpdateWindowOpened) return;

                                // Start Window
                                updatewindow = new UpdateWindow();

                                // Keep in Front
                                updatewindow.Owner = Window.GetWindow(this);

                                // Only allow 1 Window instance
                                updatewindow.ContentRendered += delegate { IsUpdateWindowOpened = true; };
                                updatewindow.Closed += delegate { IsUpdateWindowOpened = false; };

                                // Position Relative to MainWindow
                                // Keep from going off screen
                                updatewindow.Left = Math.Max((Left + (Width - updatewindow.Width) / 2), Left);
                                updatewindow.Top = Math.Max((Top + (Height - updatewindow.Height) / 2), Top);

                                // Open Window
                                updatewindow.Show();
                                break;
                            case MessageBoxResult.No:
                                break;
                        }
                    }

                    // Update Not Available
                    //
                    else if (latestVersion <= currentVersion)
                    {
                        MessageBox.Show("This version is up to date.",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                        return;
                    }

                    // Unknown
                    //
                    else // null
                    {
                        MessageBox.Show("Could not find download. Try updating manually.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                        return;
                    }
                }

                // Version is Null
                //
                else
                {
                    MessageBox.Show("GitHub version file returned empty.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                    return;
                }
            }
            else
            {
                MessageBox.Show("Could not detect Internet Connection.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                return;
            }
        }

        /// <summary>
        ///    Update Available Check
        /// </summary>
        public static async Task<int> UpdateAvailableCheck(ViewModel vm)
        {
            int count = 0;
            if (vm.UpdateAutoCheck_IsChecked == true)
            {
                await Task.Factory.StartNew(() =>
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    //HttpClient client = new HttpClient();
                    //client.BaseAddress = new Uri("https://github.com/");
                    //client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Add("User-Agent", "Axiom (https://github.com/MattMcManis/Axiom)" + " v" + currentVersion + "-" + currentBuildPhase + " Update Check");
                    //client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
                    //client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
                    //client.DefaultRequestHeaders.Add("dnt", "1");
                    //client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");

                    WebClient wc = new WebClient();
                    wc.Headers.Add(HttpRequestHeader.UserAgent, "Axiom (https://github.com/MattMcManis/Axiom)" + " v" + currentVersion + "-" + currentBuildPhase + " Update Check");
                    wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
                    wc.Headers.Add("Accept-Language", "en-US,en;q=0.9");
                    wc.Headers.Add("dnt", "1");
                    wc.Headers.Add("Upgrade-Insecure-Requests", "1");
                    //wc.Headers.Add("accept-encoding", "gzip, deflate, br"); //error

                    // -------------------------
                    // Parse GitHub .version file
                    // -------------------------
                    string parseLatestVersion = string.Empty;

                    try
                    {
                        //parseLatestVersion = client.GetStringAsync("https://raw.githubusercontent.com/MattMcManis/Axiom/master/.version").Result;
                        parseLatestVersion = wc.DownloadString("https://raw.githubusercontent.com/MattMcManis/Axiom/master/.version");
                    }
                    catch
                    {
                        return;
                    }

                    // -------------------------
                    // Split Version & Build Phase by dash
                    // -------------------------
                    if (!string.IsNullOrEmpty(parseLatestVersion)) //null check
                    {
                        try
                        {
                            // Split Version and Build Phase
                            splitVersionBuildPhase = Convert.ToString(parseLatestVersion).Split('-');

                            // Set Version Number
                            latestVersion = new Version(splitVersionBuildPhase[0]); //number
                            latestBuildPhase = splitVersionBuildPhase[1]; //alpha
                        }
                        catch
                        {
                            return;
                        }

                        // Check if Axiom is the Latest Version
                        // Update Available
                        if (latestVersion > currentVersion)
                        {
                            //updateAvailable = " ~ Update Available: " + "(" + Convert.ToString(latestVersion) + "-" + latestBuildPhase + ")";

                            //Dispatcher.Invoke(new Action(delegate
                            //{
                            //vm.TitleVersion = "Axiom ~ FFmpeg UI (" + Convert.ToString(currentVersion) + "-" + currentBuildPhase + ")"
                            //                  + " ~ Update Available: " + "(" + Convert.ToString(latestVersion) + "-" + latestBuildPhase + ")";
                            //}));

                            vm.TitleVersion = vm.TitleVersion + " ~ Update Available: " + "(" + Convert.ToString(latestVersion) + "-" + latestBuildPhase + ")";
                        }
                        // Update Not Available
                        else if (latestVersion <= currentVersion)
                        {
                            return;
                        }
                    }
                });
            }

            return count;
        }


        /// <summary>
        ///    Keep Window - Toggle - Checked
        /// </summary>
        private void tglCMDWindowKeep_Checked(object sender, RoutedEventArgs e)
        {
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Keep FFmpeg Window Toggle: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run("On") { Foreground = Log.ConsoleDefault });

            //// Prevent Loading Corrupt App.Config
            //try
            //{
            //    Settings.Default.CMDWindowKeep = vm.CMDWindowKeep_IsChecked;
            //    Settings.Default.Save();
            //}
            //catch
            //{

            //}
        }
        /// <summary>
        ///    Keep Window - Toggle - Unchecked
        /// </summary>
        private void tglCMDWindowKeep_Unchecked(object sender, RoutedEventArgs e)
        {
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Keep FFmpeg Window Toggle: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run("Off") { Foreground = Log.ConsoleDefault });

            //// Prevent Loading Corrupt App.Config
            //try
            //{
            //    Settings.Default.CMDWindowKeep = vm.CMDWindowKeep_IsChecked;
            //    Settings.Default.Save();
            //}
            //catch
            //{

            //}
        }

        /// <summary>
        ///    Auto Sort Script - Toggle - Checked
        /// </summary>
        private void tglAutoSortScript_Checked(object sender, RoutedEventArgs e)
        {
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Auto Sort Script Toggle: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run("On") { Foreground = Log.ConsoleDefault });

            //// Prevent Loading Corrupt App.Config
            //try
            //{
            //    Settings.Default.AutoSortScript = vm.AutoSortScript_IsChecked;
            //    Settings.Default.Save();
            //}
            //catch
            //{

            //}
        }
        /// <summary>
        ///    Auto Sort Script - Toggle - Unchecked
        /// </summary>
        private void tglAutoSortScript_Unchecked(object sender, RoutedEventArgs e)
        {
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Auto Sort Script Toggle: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run("Off") { Foreground = Log.ConsoleDefault });

            //// Prevent Loading Corrupt App.Config
            //try
            //{
            //    Settings.Default.AutoSortScript = vm.AutoSortScript_IsChecked;
            //    Settings.Default.Save();
            //}
            //catch
            //{

            //}
        }

        /// <summary>
        ///     Debug Console Window Button
        /// </summary>
        private Boolean IsDebugConsoleOpened = false;
        private void btnDebugConsole_Click(object sender, RoutedEventArgs e)
        {
            // Prevent Monitor Resolution Window Crash
            //
            try
            {
                // Check if Window is already open
                if (IsDebugConsoleOpened) return;

                // Start Window
                debugconsole = new DebugConsole(this, vm);

                // Only allow 1 Window instance
                debugconsole.ContentRendered += delegate { IsDebugConsoleOpened = true; };
                debugconsole.Closed += delegate { IsDebugConsoleOpened = false; };

                // Detect which screen we're on
                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                var thisScreen = allScreens.SingleOrDefault(s => Left >= s.WorkingArea.Left && Left < s.WorkingArea.Right);
                if (thisScreen == null) thisScreen = allScreens.First();


                // Position Relative to MainWindow
                // Keep from going off screen
                debugconsole.Left = Math.Max(Left - debugconsole.Width - 12, thisScreen.WorkingArea.Left);
                debugconsole.Top = Math.Max(Top - 0, thisScreen.WorkingArea.Top);

                // Write Variables to Debug Window (Method)
                //DebugConsole.DebugWrite(debugconsole, vm);

                // Open Window
                debugconsole.Show();
            }
            // Simplified
            catch
            {
                // Check if Window is already open
                if (IsDebugConsoleOpened) return;

                // Start Window
                debugconsole = new DebugConsole(this, vm);

                // Only allow 1 Window instance
                debugconsole.ContentRendered += delegate { IsDebugConsoleOpened = true; };
                debugconsole.Closed += delegate { IsDebugConsoleOpened = false; };

                // Position Relative to MainWindow
                // Keep from going off screen
                debugconsole.Left = Left - debugconsole.Width - 12;
                debugconsole.Top = Top;

                // Write Variables to Debug Window (Method)
                //DebugConsole.DebugWrite(debugconsole, vm);

                // Open Window
                debugconsole.Show();
            }
        }

        /// <summary>
        ///     Log Console Window Button
        /// </summary>
        private void btnLogConsole_Click(object sender, RoutedEventArgs e)
        {
            // Prevent Monitor Resolution Window Crash
            //
            try
            {
                // Detect which screen we're on
                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                var thisScreen = allScreens.SingleOrDefault(s => Left >= s.WorkingArea.Left && Left < s.WorkingArea.Right);
                if (thisScreen == null) thisScreen = allScreens.First();

                // Position Relative to MainWindow
                // Keep from going off screen
                logconsole.Left = Math.Min(Left + ActualWidth + 12, thisScreen.WorkingArea.Right - logconsole.Width);
                logconsole.Top = Math.Min(Top + 0, thisScreen.WorkingArea.Bottom - logconsole.Height);

                // Open Winndow
                logconsole.Show();
            }
            // Simplified
            catch
            {
                // Position Relative to MainWindow
                // Keep from going off screen
                logconsole.Left = Left + ActualWidth + 12;
                logconsole.Top = Top;

                // Open Winndow
                logconsole.Show();
            }
        }

        /// <summary>
        ///    Log Button
        /// </summary>
        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            // Call Method to get Log Path
            Log.DefineLogPath(vm);

            //MessageBox.Show(Configure.logPath.ToString()); //debug

            // Open Log
            if (File.Exists(vm.LogPath_Text + "output.log"))
            {
                Process.Start("notepad.exe", "\"" + vm.LogPath_Text + "output.log" + "\"");
            }
            else
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Output Log has not been created yet.")) { Foreground = Log.ConsoleWarning });

                MessageBox.Show("Output Log has not been created yet.",
                                        "Notice",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
            }
        }

        /// <summary>
        ///    CMD Button
        /// </summary>
        private void btnCmd_Click(object sender, RoutedEventArgs e)
        {
            // launch command prompt
            Process.Start("CMD.exe", "/k cd %userprofile%");

        }

        /// <summary>
        ///     File Properties Button
        /// </summary>
        private Boolean IsFilePropertiesOpened = false;
        private void btnProperties_Click(object sender, RoutedEventArgs e)
        {
            // Prevent Monitor Resolution Window Crash
            //
            try
            {
                // Check if Window is already open
                if (IsFilePropertiesOpened) return;

                // Start window
                //MainWindow mainwindow = this;
                filepropwindow = new FilePropertiesWindow(this, vm);

                // Only allow 1 Window instance
                filepropwindow.ContentRendered += delegate { IsFilePropertiesOpened = true; };
                filepropwindow.Closed += delegate { IsFilePropertiesOpened = false; };

                // Detect which screen we're on
                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                var thisScreen = allScreens.SingleOrDefault(s => Left >= s.WorkingArea.Left && Left < s.WorkingArea.Right);
                if (thisScreen == null) thisScreen = allScreens.First();

                // Position Relative to MainWindow
                // Keep from going off screen
                filepropwindow.Left = Math.Max((Left + (Width - filepropwindow.Width) / 2), thisScreen.WorkingArea.Left);
                filepropwindow.Top = Math.Max((Top + (Height - filepropwindow.Height) / 2), thisScreen.WorkingArea.Top);

                // Write Properties to Textbox in FilePropertiesWindow Initialize

                // Open Window
                filepropwindow.Show();
            }
            // Simplified
            catch
            {
                // Check if Window is already open
                if (IsFilePropertiesOpened) return;

                // Start window
                filepropwindow = new FilePropertiesWindow(this, vm);

                // Only allow 1 Window instance
                filepropwindow.ContentRendered += delegate { IsFilePropertiesOpened = true; };
                filepropwindow.Closed += delegate { IsFilePropertiesOpened = false; };

                // Position Relative to MainWindow
                // Keep from going off screen
                filepropwindow.Left = Math.Max((Left + (Width - filepropwindow.Width) / 2), Left);
                filepropwindow.Top = Math.Max((Top + (Height - filepropwindow.Height) / 2), Top);

                // Write Properties to Textbox in FilePropertiesWindow Initialize

                // Open Window
                filepropwindow.Show();
            }
        }

        /// <summary>
        ///    Play File Button
        /// </summary>
        private void btnPlayFile_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(@output))
            {
                Process.Start("\"" + output + "\"");
            }
            else
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: File does not yet exist.")) { Foreground = Log.ConsoleWarning });

                MessageBox.Show("File does not yet exist.",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }



        /// <summary>
        ///    Input Button
        /// </summary>
        private void btnInput_Click(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Single File
            // -------------------------
            if (vm.Batch_IsChecked == false)
            {
                // Open Select File Window
                Microsoft.Win32.OpenFileDialog selectFile = new Microsoft.Win32.OpenFileDialog();

                // Remember Last Dir
                //
                try
                {
                    string previousPath = Settings.Default.InputDir.ToString();

                    // Use Previous Path if Not Null
                    if (!string.IsNullOrEmpty(previousPath))
                    {
                        selectFile.InitialDirectory = previousPath;
                    }
                }
                catch
                {

                }

                // Show Dialog Box
                Nullable<bool> result = selectFile.ShowDialog();

                // Process Dialog Box
                if (result == true)
                {
                    // Display path and file in Output Textbox
                    vm.Input_Text = selectFile.FileName;

                    // Set Input Dir, Name, Ext
                    inputDir = Path.GetDirectoryName(vm.Input_Text).TrimEnd('\\') + @"\";

                    inputFileName = Path.GetFileNameWithoutExtension(vm.Input_Text);

                    inputExt = Path.GetExtension(vm.Input_Text);

                    // Save Previous Path
                    Settings.Default.InputDir = inputDir;
                    Settings.Default.Save();

                }

                // --------------------------------------------------
                // Default Auto if Input Extension matches Output Extsion
                // This will trigger Auto Codec Copy
                // --------------------------------------------------
                ExtensionMatchCheckAuto(vm);

                // -------------------------
                // Prevent Losing Codec Copy after cancel closing Browse Folder Dialog Box 
                // Set Video & Audio Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
                // -------------------------
                VideoControls.AutoCopyVideoCodec(vm);
                SubtitleControls.AutoCopySubtitleCodec(vm);
                AudioControls.AutoCopyAudioCodec(vm);
            }

            // -------------------------
            // Batch
            // -------------------------
            else if (vm.Batch_IsChecked == true)
            {
                // Open Batch Folder
                System.Windows.Forms.FolderBrowserDialog inputFolder = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = inputFolder.ShowDialog();


                // Show Input Dialog Box
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    // Display Folder Path in Textbox
                    vm.Input_Text = inputFolder.SelectedPath.TrimEnd('\\') + @"\";

                    // Input Directory
                    //inputDir = Path.GetDirectoryName(vm.Input_Text.TrimEnd('\\') + @"\");
                    inputDir = vm.Input_Text.TrimEnd('\\') + @"\"; // Note: Do not use Path.GetDirectoryName() with Batch Path only
                                                                   //       It will remove the last dir as a file extension
                }

                // -------------------------
                // Prevent Losing Codec Copy after cancel closing Browse Folder Dialog Box 
                // Set Video & Audio Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
                // -------------------------
                VideoControls.AutoCopyVideoCodec(vm);
                SubtitleControls.AutoCopySubtitleCodec(vm);
                AudioControls.AutoCopyAudioCodec(vm);
            }
        }



        /// <summary>
        ///    Input Textbox
        /// </summary>
        private void tbxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            input = vm.Input_Text;

            // -------------------------
            // Local File
            // -------------------------
            if (IsWebURL(vm.Input_Text) == false)
            {
                // -------------------------
                // Single File
                // -------------------------
                if (vm.Batch_IsChecked == false)
                {
                    // Has Text
                    if (!string.IsNullOrEmpty(vm.Input_Text))
                    {
                        // Remove stray slash if closed out early
                        if (input == @"\")
                        {
                            input = string.Empty;
                        }

                        // Do not set inputDir

                        // Input Extension
                        inputExt = Path.GetExtension(vm.Input_Text);
                    }

                    // No Text
                    else
                    {
                        input = string.Empty;
                        inputDir = string.Empty;
                    }
                }

                // -------------------------
                // Batch
                // -------------------------
                else
                {
                    // Has Text
                    if (!string.IsNullOrEmpty(vm.Input_Text))
                    {
                        // Remove stray slash if closed out early
                        if (input == @"\")
                        {
                            input = string.Empty;
                        }

                        inputDir = vm.Input_Text.TrimEnd('\\') + @"\"; // Note: Do not use Path.GetDirectoryName() with Batch Path only
                                                                       //       It will remove the last dir as a file extension
                        inputExt = vm.BatchExtension_Text;
                    }

                    // No Text
                    else
                    {
                        input = string.Empty;
                        inputDir = string.Empty;
                    }
                }


                // -------------------------
                // Enable / Disable "Open Input Location" Button
                // -------------------------
                if (!string.IsNullOrEmpty(vm.Input_Text) &&
                    IsValidFilePath(vm.Input_Text) == true && // Detect Invalid Characters

                    Path.IsPathRooted(vm.Input_Text) == true  // TrimEnd('\\') + @"\" is adding a backslash to 
                                                              // Iput text 'http' until it is detected as Web URL
                    )
                {
                    bool exists = Directory.Exists(Path.GetDirectoryName(vm.Input_Text));

                    // Path exists
                    if (exists)
                    {
                        vm.Input_Location_IsEnabled = true;
                    }
                    // Path does not exist
                    else
                    {
                        vm.Input_Location_IsEnabled = false;
                    }
                }

                // Disable Button for Web URL
                else
                {
                    vm.Input_Location_IsEnabled = false;
                }


                // -------------------------
                // Set Video & Audio Codec Combobox to "Copy" 
                // if Input Extension is Same as Output Extension and Video Quality is Auto
                // -------------------------
                if (IsWebURL(vm.Input_Text) == false) // Check if Input is a Windows Path, Not a URL
                {
                    if (Path.HasExtension(vm.Input_Text) == true && // Check if Input has file extension after it has passed URL check
                                                                    // to prevent path forward slash error in Path.HasExtension()

                        !vm.Input_Text.Contains("youtube"))         // Input text does not contain "youtube", 
                                                                    // Path.HasExtension() detects .c, .co, .com as extension

                    {
                        VideoControls.AutoCopyVideoCodec(vm);
                        SubtitleControls.AutoCopySubtitleCodec(vm);
                        AudioControls.AutoCopyAudioCodec(vm);
                    }
                }
            }

            // -------------------------
            // Web URL
            // -------------------------
            else
            {
                inputDir = string.Empty;
                inputExt = string.Empty;
                vm.Input_Location_IsEnabled = false;
            }


            // -------------------------
            // Convert Button Text Change
            // -------------------------
            // YouTube Download
            ConvertButtonText(vm);
        }

        /// <summary>
        ///    Input Textbox - Drag and Drop
        /// </summary>
        private void tbxInput_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }

        private void tbxInput_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            vm.Input_Text = buffer.First();

            // Set Input Dir, Name, Ext
            inputDir = Path.GetDirectoryName(vm.Input_Text).TrimEnd('\\') + @"\";
            inputFileName = Path.GetFileNameWithoutExtension(vm.Input_Text);
            inputExt = Path.GetExtension(vm.Input_Text);

            // Set Video & Audio Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
            VideoControls.AutoCopyVideoCodec(vm);
            SubtitleControls.AutoCopySubtitleCodec(vm);
            AudioControls.AutoCopyAudioCodec(vm);
        }

        /// <summary>
        ///    Open Input Folder Button
        /// </summary>
        private void openLocationInput_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(inputDir))
            {
                Process.Start("explorer.exe", @inputDir);
            }
        }



        /// <summary>
        ///    Output Button
        /// </summary>
        private void btnOutput_Click(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Single File
            // -------------------------
            if (vm.Batch_IsChecked == false)
            {
                // -------------------------
                // Get Output Ext
                // -------------------------
                FormatControls.OutputFormatExt(vm);

                // -------------------------
                // Open 'Save File'
                // -------------------------
                Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();

                // -------------------------
                // 'Save File' Default Path same as Input Directory
                // -------------------------
                try
                {
                    string previousPath = Settings.Default.OutputDir.ToString();
                    // Use Input Path if Previous Path is Null
                    if (string.IsNullOrEmpty(previousPath))
                    {
                        saveFile.InitialDirectory = inputDir;
                    }
                }
                catch
                {

                }

                // Remember Last Dir
                //saveFile.RestoreDirectory = true;
                // Default Extension
                saveFile.DefaultExt = outputExt;

                // Default file name if empty
                if (string.IsNullOrEmpty(inputFileName))
                {
                    saveFile.FileName = "File";
                }
                // If file name exists
                else
                {
                    // Output Path
                    outputDir = inputDir;

                    // File Renamer
                    // Get new output file name (1) if already exists
                    outputFileName = FileRenamer(inputFileName);

                    // Same as input file name
                    saveFile.FileName = outputFileName;
                }


                // -------------------------
                // Show Dialog Box
                // -------------------------
                Nullable<bool> result = saveFile.ShowDialog();

                // Process Dialog Box
                if (result == true)
                {
                    // Display path and file in Output Textbox
                    vm.Output_Text = saveFile.FileName;

                    // Output Path
                    outputDir = Path.GetDirectoryName(vm.Output_Text).TrimEnd('\\') + @"\";

                    // Output Filename (without extension)
                    outputFileName = Path.GetFileNameWithoutExtension(vm.Output_Text);

                    // Add slash to inputDir path if missing
                    if (!string.IsNullOrEmpty(outputDir))
                    {
                        if (!outputDir.EndsWith("\\"))
                        {
                            outputDir = outputDir.TrimEnd('\\') + @"\";
                        }
                    }

                    // Save Previous Path
                    Settings.Default.OutputDir = outputDir;
                    Settings.Default.Save();
                }
            }

            // -------------------------
            // Batch
            // -------------------------
            else if (vm.Batch_IsChecked == true)
            {
                // Open 'Select Folder'
                System.Windows.Forms.FolderBrowserDialog outputFolder = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = outputFolder.ShowDialog();


                // Process Dialog Box
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    // Display path and file in Output Textbox
                    vm.Output_Text = outputFolder.SelectedPath.TrimEnd('\\') + @"\";

                    // Remove Double Slash in Root Dir, such as C:\
                    vm.Output_Text = vm.Output_Text.Replace(@"\\", @"\");

                    // Output Path
                    outputDir = Path.GetDirectoryName(vm.Output_Text.TrimEnd('\\') + @"\");

                    // Add slash to inputDir path if missing
                    if (!string.IsNullOrEmpty(outputDir))
                    {
                        if (!outputDir.EndsWith("\\"))
                        {
                            outputDir = outputDir.TrimEnd('\\') + @"\";
                        }
                    }
                }
            }

        }


        /// <summary>
        ///    Output Textbox
        /// </summary>
        private void tbxOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Remove stray slash if closed out early
            if (vm.Output_Text == "\\")
            {
                vm.Output_Text = string.Empty;
            }

            // Enable / Disable "Open Output Location" Buttion
            if (!string.IsNullOrEmpty(vm.Output_Text))
            {
                bool exists = Directory.Exists(Path.GetDirectoryName(vm.Output_Text));

                if (exists)
                {
                    vm.Output_Location_IsEnabled = true;
                }
                else
                {
                    vm.Output_Location_IsEnabled = false;
                }
            }
        }


        /// <summary>
        ///    Output Textbox - Drag and Drop
        /// </summary>
        private void tbxOutput_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }

        private void tbxOutput_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            vm.Output_Text = buffer.First();
        }


        /// <summary>
        ///    Open Output Folder Button
        /// </summary>
        private void openLocationOutput_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(outputDir))
            {
                Process.Start("explorer.exe", @outputDir);
            }
        }


        /// <summary>
        ///    Batch Extension Period Check (Method)
        /// </summary>
        public static void BatchExtCheck(ViewModel vm)
        {
            if (vm.Batch_IsChecked == true)
            {
                // Add period to Batch Extension if User did not enter one
                if (!string.IsNullOrEmpty(vm.BatchExtension_Text))
                {
                    if (vm.BatchExtension_Text != "extension" &&
                        vm.BatchExtension_Text != "." &&
                        !vm.BatchExtension_Text.StartsWith(".")
                        )
                    {
                        inputExt = "." + vm.BatchExtension_Text;
                    }
                }
                else
                {
                    inputExt = string.Empty;
                }
            }
        }


        /// <summary>
        ///    Batch Toggle
        /// </summary>
        // Checked
        private void tglBatch_Checked(object sender, RoutedEventArgs e)
        {
            // Enable / Disable batch extension textbox
            if (vm.Batch_IsChecked == true)
            {
                vm.BatchExtension_IsEnabled = true;
                vm.BatchExtension_Text = string.Empty;
            }

            // Clear Browse Textbox, Input Filename, Dir, Ext
            if (!string.IsNullOrEmpty(vm.Input_Text))
            {
                vm.Input_Text = string.Empty;
                inputFileName = string.Empty;
                inputDir = string.Empty;
                inputExt = string.Empty;
            }

            // Clear Output Textbox, Output Filename, Dir, Ext
            if (!string.IsNullOrEmpty(vm.Output_Text))
            {
                vm.Output_Text = string.Empty;
                outputFileName = string.Empty;
                outputDir = string.Empty;
                outputExt = string.Empty;
            }

        }
        // Unchecked
        private void tglBatch_Unchecked(object sender, RoutedEventArgs e)
        {
            // Enable / Disable batch extension textbox
            if (vm.Batch_IsChecked == false)
            {
                vm.BatchExtension_IsEnabled = false;
                vm.BatchExtension_Text = "extension";
            }

            // Clear Browse Textbox, Batch Filename, Dir, Ext
            if (!string.IsNullOrEmpty(vm.Input_Text))
            {
                vm.Input_Text = string.Empty;
                inputFileName = string.Empty;
                inputDir = string.Empty;
                inputExt = string.Empty;
            }

            // Clear Output Textbox, Output Filename, Dir, Ext
            if (!string.IsNullOrEmpty(vm.Output_Text))
            {
                vm.Output_Text = string.Empty;
                outputFileName = string.Empty;
                outputDir = string.Empty;
                outputExt = string.Empty;
            }

            // Set Video and AudioCodec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
            VideoControls.AutoCopyVideoCodec(vm);
            SubtitleControls.AutoCopySubtitleCodec(vm);
            AudioControls.AutoCopyAudioCodec(vm);
        }


        /// <summary>
        ///    Batch Extension Textbox
        /// </summary>
        private void batchExtension_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Remove Default Value
            if (string.IsNullOrEmpty(vm.BatchExtension_Text) ||
                vm.BatchExtension_Text == "extension"
                )
            {
                inputExt = string.Empty;
            }
            // TextBox Value
            else
            {
                inputExt = vm.BatchExtension_Text;
            }

            // Add period to batchExt if user did not enter (This helps enable Copy)
            if (!string.IsNullOrEmpty(vm.BatchExtension_Text) &&
                !inputExt.StartsWith(".") &&
                vm.BatchExtension_Text != "extension")
            {
                inputExt = "." + inputExt;
            }

            // --------------------------------------------------
            // Default Auto if Input Extension matches Output Extsion
            // This will trigger Auto Codec Copy
            // --------------------------------------------------
            ExtensionMatchCheckAuto(vm);

            // Set Video and AudioCodec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
            VideoControls.AutoCopyVideoCodec(vm);
            SubtitleControls.AutoCopySubtitleCodec(vm);
            AudioControls.AutoCopyAudioCodec(vm);
        }


        /// <summary>
        ///    File Renamer (Method)
        /// </summary>
        public static String FileRenamer(string filename)
        {
            string output = outputDir + filename + outputExt;
            string outputNewFileName = string.Empty;

            int count = 1;

            if (File.Exists(output))
            {
                while (File.Exists(output))
                {
                    outputNewFileName = string.Format("{0}({1})", filename + " ", count++);
                    output = Path.Combine(outputDir, outputNewFileName + outputExt);
                }
            }
            else
            {
                // stay default
                outputNewFileName = filename;
            }

            return outputNewFileName;
        }



        /// <summary>
        ///    Input Path
        /// </summary>
        public static String InputPath(ViewModel vm, string pass)
        {
            if (!string.IsNullOrEmpty(vm.Input_Text))
            {
                // -------------------------
                // Local File
                // -------------------------
                if (IsWebURL(vm.Input_Text) == false) // Ignore Web URL's
                {
                    // -------------------------
                    // Single File
                    // -------------------------
                    if (vm.Batch_IsChecked == false &&
                        pass != "pass 2") // Ignore Pass 2, use existing input path
                    {
                        // Input Directory
                        if (!string.IsNullOrEmpty(vm.Input_Text))
                        {
                            inputDir = Path.GetDirectoryName(vm.Input_Text).TrimEnd('\\') + @"\"; // eg. C:\Input\Path\
                            inputFileName = Path.GetFileNameWithoutExtension(vm.Input_Text);
                            inputExt = Path.GetExtension(vm.Input_Text);
                        }

                        // Combine Input
                        input = vm.Input_Text; // eg. C:\Path\To\file.avi
                    }

                    // -------------------------
                    // Batch
                    // -------------------------
                    else if (vm.Batch_IsChecked == true)
                    {
                        inputDir = vm.Input_Text.TrimEnd('\\') + @"\";  // Note: Do not use Path.GetDirectoryName() with Batch Path only
                                                                        //       It will remove the last dir as a file extension

                        // Note: %f is filename, %~f is full path
                        inputFileName = "%~f";

                        // Combine Input
                        input = inputDir + inputFileName; // eg. C:\Input\Path\
                    }
                }

                // -------------------------
                // YouTube Download
                // -------------------------
                else if (IsWebURL(vm.Input_Text) == true &&
                         pass != "pass 2") // Ignore Pass 2, use existing input path
                {
                    inputDir = downloadDir;
                    inputFileName = "%f";
                    inputExt = "." + YouTubeDownloadFormat(vm.Format_YouTube_SelectedItem,
                                                           vm.Video_Codec_SelectedItem,
                                                           vm.Subtitle_Codec_SelectedItem,
                                                           vm.Audio_Codec_SelectedItem
                                                           );

                    input = inputDir + inputFileName + inputExt; // eg. C:\Users\Example\Downloads\%f.mp4
                }
            }

            // -------------------------
            // Empty
            // -------------------------
            else
            {
                inputDir = string.Empty;
                inputFileName = string.Empty;
                input = string.Empty;
            }
           

            // Return Value
            return input;
        }



        /// <summary>
        ///    Batch Input Directory
        /// </summary>
        // Directory Only, Needed for Batch
        public static String BatchInputDirectory(ViewModel vm)
        {
            // -------------------------
            // Batch
            // -------------------------
            if (vm.Batch_IsChecked == true)
            {
                inputDir = vm.Input_Text; // eg. C:\Input\Path\
            }

            // -------------------------
            // Empty
            // -------------------------
            // Input Textbox & Output Textbox Both Empty
            if (string.IsNullOrEmpty(vm.Input_Text))
            {
                inputDir = string.Empty;
            }


            // Return Value
            return inputDir;
        }


        /// <summary>
        ///    Output Path
        /// </summary>
        public static String OutputPath(ViewModel vm)
        {
            // Get Output Extension (Method)
            FormatControls.OutputFormatExt(vm);

            if (!string.IsNullOrEmpty(vm.Input_Text)) // Check Input
            {
                // -------------------------
                // Local File
                // -------------------------
                if (IsWebURL(vm.Input_Text) == false) // Ignore Web URL's
                {
                    // -------------------------
                    // Single File
                    // -------------------------
                    if (vm.Batch_IsChecked == false)
                    {
                        // Input Not Empty
                        // Output Empty
                        if (!string.IsNullOrEmpty(vm.Input_Text) &&
                            string.IsNullOrEmpty(vm.Output_Text))
                        {
                            // Default Output Dir to be same as Input Directory
                            outputDir = inputDir;
                            outputFileName = inputFileName;
                        }

                        // Input Not Empty
                        // Output Not Empty
                        else
                        {
                            outputDir = Path.GetDirectoryName(vm.Output_Text).TrimEnd('\\') + @"\"; // eg. C:\Output\Path\
                            outputFileName = Path.GetFileNameWithoutExtension(vm.Output_Text);
                        }

                        // -------------------------
                        // File Renamer
                        // -------------------------
                        // Pressing Script or Convert while Output TextBox is empty
                        if (inputDir == outputDir &&
                            inputFileName == outputFileName &&
                            string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
                        {
                            outputFileName = FileRenamer(inputFileName);
                        }

                        // -------------------------
                        // Image Sequence Renamer
                        // -------------------------
                        if (vm.Format_MediaType_SelectedItem == "Sequence")
                        {
                            outputFileName = "image-%03d"; //must be this name
                        }

                        // -------------------------
                        // Combine Output
                        // -------------------------
                        output = outputDir + outputFileName + outputExt; // eg. C:\Output Folder\ + file + .mp4

                        // -------------------------
                        // Update TextBox
                        // -------------------------
                        // Used if FileRenamer() changes name: filename (1)
                        // Only used for Single File, ignore Batch and Web URLs
                        vm.Output_Text = output;
                    }

                    // -------------------------
                    // Batch
                    // -------------------------
                    else if (vm.Batch_IsChecked == true)
                    {
                        // Note: %f is filename, %~f is full path

                        // Add slash to Batch Output Text folder path if missing
                        vm.Output_Text = vm.Output_Text.TrimEnd('\\') + @"\";

                        // Input Not Empty, Output Empty
                        // Default Output to be same as Input Directory
                        if (!string.IsNullOrEmpty(vm.Input_Text) &&
                            string.IsNullOrEmpty(vm.Output_Text))
                        {
                            vm.Output_Text = vm.Input_Text;
                        }

                        outputDir = vm.Output_Text.TrimEnd('\\') + @"\";

                        // Combine Output  
                        output = outputDir + "%~nf" + outputExt; // eg. C:\Output Folder\%~nf.mp4
                    }
                }

                // -------------------------
                // YouTube Download
                // -------------------------
                else if (IsWebURL(vm.Input_Text) == true) 
                {
                    // Note: %f is filename, %~f is full path

                    // Auto Output Path
                    if (string.IsNullOrEmpty(vm.Output_Text))
                    {
                        outputDir = downloadDir;
                        //outputFileName = "%f";

                        // Check if output filename already exists
                        // Check if YouTube Download Format is the same as Output Extension
                        // The youtub-dl merged format for converting should be mkv
                        if ("." + YouTubeDownloadFormat(vm.Format_YouTube_SelectedItem,
                                                  vm.Video_Codec_SelectedItem,
                                                  vm.Subtitle_Codec_SelectedItem,
                                                  vm.Audio_Codec_SelectedItem
                                                  )

                                                  ==

                                                  outputExt
                                                  )
                        {
                            // Add (1)
                            outputFileName = "%f" + " (1)";
                        }
                        else
                        {
                            outputFileName = "%f";
                        }

                        // Combine Output
                        output = outputDir + outputFileName + outputExt; // eg. C:\Users\Example\Downloads\%f.webm
                    }

                    // User Defined Output Path
                    else
                    {
                        outputDir = Path.GetDirectoryName(vm.Output_Text).TrimEnd('\\') + @"\"; // eg. C:\Output\Path\
                        outputFileName = Path.GetFileNameWithoutExtension(vm.Output_Text);

                        // Combine Output
                        output = outputDir + outputFileName + outputExt;
                    }
                }
            }

            // -------------------------
            // Input Empty
            // -------------------------
            // Output must have an Input
            else
            {
                outputDir = string.Empty;
                outputFileName = string.Empty;
                output = string.Empty;
            }

            
            // Return Value
            return output;
        }


        /// <summary>
        ///    Extension Match Check Auto
        /// </summary>
        /// <remarks>
        ///    Change the Controls to Auto if Input Extension matches Output Extsion
        ///    This will trigger Auto Codec Copy
        /// </remarks>
        public void ExtensionMatchCheckAuto(ViewModel vm)
        {
            //MessageBox.Show(inputExt + " " + outputExt); //debug

            // -------------------------
            // Video
            // -------------------------
            if (vm.Video_Quality_SelectedItem == "Auto" &&
                string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
            {
                // Set Controls:

                // Main
                vm.Video_Quality_SelectedItem = "Auto";
                vm.Video_PixelFormat_SelectedItem = "auto";
                vm.Video_FPS_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "None";
                vm.Video_Scale_SelectedItem = "Source";
                vm.Video_ScalingAlgorithm_SelectedItem = "auto";

                // Filters
                vm.FiltersSetDefault();
            }

            // -------------------------
            // Audio
            // -------------------------
            if (vm.Audio_Quality_SelectedItem == "Auto" &&
                string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
            {
                // Set Controls:

                // Main
                vm.Audio_Quality_SelectedItem = "Auto";
                vm.Audio_Channel_SelectedItem = "Source";
                vm.Audio_SampleRate_SelectedItem = "auto";
                vm.Audio_BitDepth_SelectedItem = "auto";

                // Filters
                vm.Audio_Volume_Text = "100";
                vm.Audio_HardLimiter_Value = 1;
            }
        }


        /// <summary>
        ///    Container - ComboBox
        /// </summary>
        private void cboFormat_Container_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Set Controls
            // -------------------------
            FormatControls.SetControls(vm, vm.Format_Container_SelectedItem);

            // -------------------------
            // Get Output Extension
            // -------------------------
            FormatControls.OutputFormatExt(vm);

            // -------------------------
            // Video Encoding Pass
            // -------------------------
            VideoControls.EncodingPassControls(vm);

            // -------------------------
            // Optimize Controls
            // -------------------------
            VideoControls.OptimizeControls(vm);


            // -------------------------
            // File Renamer
            // -------------------------
            // Add (1) if File Names are the same
            if (!string.IsNullOrEmpty(inputDir) && 
                string.Equals(inputFileName, outputFileName, StringComparison.CurrentCultureIgnoreCase))
            {
                outputFileName = FileRenamer(inputFileName);
            }

            // --------------------------------------------------
            // Default Auto if Input Extension matches Output Extsion
            // This will trigger Auto Codec Copy
            // --------------------------------------------------
            ExtensionMatchCheckAuto(vm);
            //MessageBox.Show(inputExt + " " + outputExt); //debug

            // -------------------------
            // Update Ouput Textbox with current Format extension
            // -------------------------
            if (vm.Batch_IsChecked == false && // Single File
                !string.IsNullOrEmpty(vm.Output_Text))
            {
                //MessageBox.Show(outputExt); //debug
                vm.Output_Text = outputDir + outputFileName + outputExt;
            }
            
            // -------------------------
            // Force MediaTypeControls ComboBox to fire SelectionChanged Event
            // to update Format changes such as Audio_Stream_SelectedItem
            // -------------------------
            cboFormat_MediaType_SelectionChanged(cboFormat_MediaType, null);
        }



        /// <summary>
        ///    Media Type - Combobox
        /// </summary>
        private void cboFormat_MediaType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FormatControls.MediaTypeControls(vm);
        }


        /// <summary>
        ///    Cut Combobox
        /// </summary>
        private void cboFormat_Cut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FormatControls.CutControls(vm);
        }

        /// <summary>
        ///    Cut Start - Textbox
        /// </summary>
        // -------------------------
        // Cut Start - Hours - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutStartHours_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutStartHours.Focus() == true &&
                vm.Format_CutStart_Hours_Text == "00")
            {
                vm.Format_CutStart_Hours_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutStartHours_LostFocus(object sender, RoutedEventArgs e)
        {
            vm.Format_CutStart_Hours_Text = tbxCutStartHours.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrEmpty(vm.Format_CutStart_Hours_Text))
            {
                vm.Format_CutStart_Hours_Text = "00";
            }
        }
        // Key Down
        private void tbxCutStartHours_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbersAndBackspace(e);
        }

        // -------------------------
        // Cut Start - Minutes - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutStartMinutes_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutStartMinutes.Focus() == true &&
                vm.Format_CutStart_Minutes_Text == "00")
            {
                vm.Format_CutStart_Minutes_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutStartMinutes_LostFocus(object sender, RoutedEventArgs e)
        {
            vm.Format_CutStart_Minutes_Text = tbxCutStartMinutes.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrEmpty(vm.Format_CutStart_Minutes_Text))
            {
                vm.Format_CutStart_Minutes_Text = "00";
            }
        }
        // Key Down
        private void tbxCutStartMinutes_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbersAndBackspace(e);
        }

        // -------------------------
        // Cut Start - Seconds - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutStartSeconds_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutStartSeconds.Focus() == true &&
                vm.Format_CutStart_Seconds_Text == "00")
            {
                vm.Format_CutStart_Seconds_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutStartSeconds_LostFocus(object sender, RoutedEventArgs e)
        {
            vm.Format_CutStart_Seconds_Text = tbxCutStartSeconds.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrEmpty(vm.Format_CutStart_Seconds_Text))
            {
                vm.Format_CutStart_Seconds_Text = "00";
            }
        }
        // Key Down
        private void tbxCutStartSeconds_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbersAndBackspace(e);
        }

        // -------------------------
        // Cut Start - Milliseconds - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutStartMilliseconds_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutStartMilliseconds.Focus() == true &&
                vm.Format_CutStart_Milliseconds_Text == "000")
            {
                vm.Format_CutStart_Milliseconds_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutStartMilliseconds_LostFocus(object sender, RoutedEventArgs e)
        {
            vm.Format_CutStart_Milliseconds_Text = tbxCutStartMilliseconds.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrEmpty(vm.Format_CutStart_Milliseconds_Text))
            {
                vm.Format_CutStart_Milliseconds_Text = "000";
            }
        }
        // Key Down
        private void tbxCutStartMilliseconds_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbersAndBackspace(e);
        }

        /// <summary>
        ///    Cut End - Textbox
        /// </summary>
        // -------------------------
        // Cut End - Hours - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutEndHours_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutEndHours.Focus() == true &&
                vm.Format_CutEnd_Hours_Text == "00")
            {
                vm.Format_CutEnd_Hours_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutEndHours_LostFocus(object sender, RoutedEventArgs e)
        {
            vm.Format_CutEnd_Hours_Text = tbxCutEndHours.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrEmpty(vm.Format_CutEnd_Hours_Text))
            {
                vm.Format_CutEnd_Hours_Text = "00";
            }
        }
        // Key Down
        private void tbxCutEndHours_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbersAndBackspace(e);
        }

        // -------------------------
        // Cut End - Minutes - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutEndMinutes_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutEndMinutes.Focus() == true &&
                vm.Format_CutEnd_Minutes_Text == "00")
            {
                vm.Format_CutEnd_Minutes_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutEndMinutes_LostFocus(object sender, RoutedEventArgs e)
        {
            vm.Format_CutEnd_Minutes_Text = tbxCutEndMinutes.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrEmpty(vm.Format_CutEnd_Minutes_Text))
            {
                vm.Format_CutEnd_Minutes_Text = "00";
            }
        }
        // Key Down
        private void tbxCutEndMinutes_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbersAndBackspace(e);
        }

        // -------------------------
        // Cut End - Seconds - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutEndSeconds_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutEndSeconds.Focus() == true &&
                vm.Format_CutEnd_Seconds_Text == "00")
            {
                vm.Format_CutEnd_Seconds_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutEndSeconds_LostFocus(object sender, RoutedEventArgs e)
        {
            vm.Format_CutEnd_Seconds_Text = tbxCutEndSeconds.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrEmpty(vm.Format_CutEnd_Seconds_Text))
            {
                vm.Format_CutEnd_Seconds_Text = "00";
            }
        }
        // Key Down
        private void tbxCutEndSeconds_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbersAndBackspace(e);
        }

        // -------------------------
        // Cut End - Milliseconds - Textbox Change
        // -------------------------
        // Got Focus
        private void tbxCutEndMilliseconds_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxCutEndMilliseconds.Focus() == true &&
                vm.Format_CutEnd_Milliseconds_Text == "000")
            {
                vm.Format_CutEnd_Milliseconds_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxCutEndMilliseconds_LostFocus(object sender, RoutedEventArgs e)
        {
            vm.Format_CutEnd_Milliseconds_Text = tbxCutEndMilliseconds.Text;

            // Change textbox back to "00" if left empty
            if (string.IsNullOrEmpty(vm.Format_CutEnd_Milliseconds_Text))
            {
                vm.Format_CutEnd_Milliseconds_Text = "000";
            }
        }
        // Key Down
        private void tbxCutEndMilliseconds_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbersAndBackspace(e);
        }


        // -------------------------
        // Frame Start Textbox Change
        // -------------------------
        // Got Focus
        private void tbxFrameStart_GotFocus(object sender, RoutedEventArgs e)
        {
        }
        // Lost Focus
        private void tbxFrameStart_LostFocus(object sender, RoutedEventArgs e)
        {
        }
        // Key Down
        private void tbxFrameStart_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbersAndBackspace(e);
        }

        // -------------------------
        // Frame End Textbox Change
        // -------------------------
        // Got Focus
        private void tbxFrameEnd_GotFocus(object sender, RoutedEventArgs e)
        {
        }
        // Lost Focus
        private void tbxFrameEnd_LostFocus(object sender, RoutedEventArgs e)
        {
        }
        // Key Down
        private void tbxFrameEnd_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbersAndBackspace(e);
        }



        /// <summary>
        ///    Video Codec - ComboBox
        /// </summary>
        private void cboVideo_Codec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // PROBLEM - This Control is being triggered twice?
            //Change 1 - Is Clicked
            //Selection is Medium
            //Change 2 - New Items Loaded from Codec
            //Selection is Null

            //MessageBox.Show(vm.Video_Quality_SelectedItem); //debug
            //MessageBox.Show("cboVideo_Codec_SelectionChanged"); //debug

            // -------------------------
            // Set Controls
            // -------------------------
            VideoControls.SetControls(vm, vm.Video_Codec_SelectedItem);

            // -------------------------
            // Audio Stream Controls
            // -------------------------
            FormatControls.AudioStreamControls(vm); 

            // -------------------------
            // Video Encoding Pass
            // -------------------------
            VideoControls.EncodingPassControls(vm);

            // -------------------------
            // Pixel Format
            // -------------------------
            //VideoControls.PixelFormatControls(vm);

            // -------------------------
            // Optimize Controls
            // -------------------------
            VideoControls.OptimizeControls(vm);

            // -------------------------
            // Convert Button Text Change
            // -------------------------
            ConvertButtonText(vm);
        }


        /// <summary>
        ///    Pass - ComboBox
        /// </summary>
        private void cboVideo_Pass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Set Controls
            // -------------------------
            //VideoControls.SetControls(vm, vm.Video_Quality_SelectedItem);

            // -------------------------
            // Pass Controls
            // -------------------------
            VideoControls.EncodingPassControls(vm);

            // -------------------------
            // Display Bit Rate in TextBox
            // -------------------------
            VideoControls.VideoBitRateDisplay(vm,
                                              vm.Video_Quality_Items,
                                              vm.Video_Quality_SelectedItem,
                                              vm.Video_Pass_SelectedItem);
        }
        private void cboVideo_Pass_DropDownClosed(object sender, EventArgs e)
        {
            // User willingly selected a Pass
            VideoControls.passUserSelected = true;
        }


        /// <summary>
        ///    Video Quality - ComboBox
        /// </summary>
        private void cboVideo_Quality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Quality Controls
            // -------------------------
            VideoControls.QualityControls(vm);

            // -------------------------
            // Display Bit Rate in TextBox
            // -------------------------
            VideoControls.VideoBitRateDisplay(vm,
                                              vm.Video_Quality_Items,
                                              vm.Video_Quality_SelectedItem,
                                              vm.Video_Pass_SelectedItem);

            // -------------------------
            // Video Encoding Pass
            // -------------------------
            VideoControls.EncodingPassControls(vm);

            // Custom
            if (vm.Video_Quality_SelectedItem == "Custom")
            {
                // Default to CRF
                if (vm.Video_Pass_Items?.Contains("CRF") == true)
                {
                    vm.Video_Pass_SelectedItem = "CRF";
                }
                // Select first available (1 Pass, 2 Pass, auto)
                else
                {
                    vm.Video_Pass_SelectedItem = vm.Video_Pass_Items.FirstOrDefault();
                }
            }

            // -------------------------
            // Pixel Format
            // -------------------------
            VideoControls.PixelFormatControls(vm,
                                              vm.Format_MediaType_SelectedItem,
                                              vm.Video_Codec_SelectedItem,
                                              vm.Video_Quality_SelectedItem);
        }


        /// <summary>
        ///     Video CRF Custom Number Textbox
        /// </summary>
        // TextBox TextChanged
        private void tbxVideo_CRF_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Update Slider with entered value
            if (!string.IsNullOrEmpty(vm.Video_CRF_Text))
            {
                vm.Video_CRF_Value = Convert.ToDouble(vm.Video_CRF_Text);
            }

            // TextBox Empty
            //else if (string.IsNullOrEmpty(vm.Video_CRF_Text))
            //{
                //vm.Video_CRF_Value = 0;
                //vm.Video_CRF_Text = string.Empty;
            //}
            
        }
        // TextBox Key Down
        private void tbxVideo_CRF_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers and Backspace
            AllowOnlyNumbersAndBackspace(e);
        }
        // Slider Value Change
        private void slVideo_CRF_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Update TextBox with value
            vm.Video_CRF_Text = vm.Video_CRF_Value.ToString();
        }
        // Slider Double Click
        private void slVideo_CRF_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.Video_CRF_Value = 23;

            //VideoControls.AutoCopyVideoCodec(vm);
        }

        /// <summary>
        ///     Video VBR Toggle - Checked
        /// </summary>
        private void tglVideo_VBR_Checked(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Quality Controls
            // -------------------------
            VideoControls.QualityControls(vm);

            // -------------------------
            // MPEG-4 VBR can only use 1 Pass
            // -------------------------
            if (vm.Video_Codec_SelectedItem == "MPEG-2" || 
                vm.Video_Codec_SelectedItem == "MPEG-4")
            {
                // Change ItemsSource
                vm.Video_Pass_Items = new List<string>()
                {
                    "1 Pass",
                };

                // Populate ComboBox from ItemsSource
                //vm.Video_Pass_Items = VideoControls.Video_Pass_ItemsSource;

                // Select Item
                vm.Video_Pass_SelectedItem = "1 Pass";
            }


            // -------------------------
            // Display Bit Rate in TextBox
            // -------------------------
            VideoControls.VideoBitRateDisplay(vm,
                                              vm.Video_Quality_Items,
                                              vm.Video_Quality_SelectedItem,
                                              vm.Video_Pass_SelectedItem);
        }

        /// <summary>
        ///     Video VBR Toggle - Unchecked
        /// </summary>
        private void tglVideo_VBR_Unchecked(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Quality Controls
            // -------------------------
            VideoControls.QualityControls(vm);

            // -------------------------
            // MPEG-2 / MPEG-4 CBR Reset
            // -------------------------
            if (vm.Video_Codec_SelectedItem == "MPEG-2" || 
                vm.Video_Codec_SelectedItem == "MPEG-4")
            {
                // Change ItemsSource
                vm.Video_Pass_Items = new List<string>()
                {
                    "2 Pass",
                    "1 Pass",
                };

                // Populate ComboBox from ItemsSource
                //cboVideo_Pass.ItemsSource = VideoControls.Video_Pass_ItemsSource;

                // Select Item
                vm.Video_Pass_SelectedItem = "2 Pass";
            }

            // -------------------------
            // Display Bit Rate in TextBox
            // -------------------------
            VideoControls.VideoBitRateDisplay(vm,
                                              vm.Video_Quality_Items,
                                              vm.Video_Quality_SelectedItem,
                                              vm.Video_Pass_SelectedItem);
        }


        /// <summary>
        ///     Pixel Format
        /// </summary>
        private void cboVideo_PixelFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //VideoControls.AutoCopyVideoCodec(vm);
        }


        /// <summary>
        ///     FPS ComboBox
        /// </summary>
        private void cboVideo_FPS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Custom ComboBox Editable
            // -------------------------
            if (vm.Video_FPS_SelectedItem == "Custom" ||
                string.IsNullOrEmpty(vm.Video_FPS_SelectedItem))
            {
                vm.Video_FPS_IsEditable = true;
            }

            // -------------------------
            // Other Items Disable Editable
            // -------------------------
            if (vm.Video_FPS_SelectedItem != "Custom" &&
                !string.IsNullOrEmpty(vm.Video_FPS_SelectedItem))
            {
                vm.Video_FPS_IsEditable = false;
            }

            // -------------------------
            // Maintain Editable Combobox while typing
            // -------------------------
            if (vm.Video_FPS_IsEditable == true)
            {
                vm.Video_FPS_IsEditable = true;

                // Clear Custom Text
                vm.Video_FPS_SelectedIndex = -1;
            }

            // -------------------------
            // Disable Copy on change
            // -------------------------
            //VideoControls.AutoCopyVideoCodec(vm);
            //SubtitleControls.AutoCopySubtitleCodec(vm);
        }

        // Speed Custom KeyDown
        private void cboVideo_FrameRate_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers and Backspace
            // Deny Symbols (Shift + Number)
            // Allow Forward Slash
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back ||
                Keyboard.IsKeyDown(Key.LeftShift) && (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                Keyboard.IsKeyDown(Key.RightShift) && (e.Key >= Key.D0 && e.Key <= Key.D9)
                )
            {
                e.Handled = true;
            }

            //if (e.Key != Key.OemQuestion)
            //{
            //    e.Handled = true;
            //}
        }


        /// <summary>
        ///     Speed ComboBox
        /// </summary>
        private void cboVideo_Speed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Custom ComboBox Editable
            // -------------------------
            if (vm.Video_Speed_SelectedItem == "Custom" ||
                string.IsNullOrEmpty(vm.Video_Speed_SelectedItem))
            {
                vm.Video_Speed_IsEditable = true;
            }

            // -------------------------
            // Other Items Disable Editable
            // -------------------------
            if (vm.Video_Speed_SelectedItem != "Custom" &&
                !string.IsNullOrEmpty(vm.Video_Speed_SelectedItem))
            {
                vm.Video_Speed_IsEditable = false;
            }

            // -------------------------
            // Maintain Editable Combobox while typing
            // -------------------------
            if (vm.Video_Speed_IsEditable == true)
            {
                vm.Video_Speed_IsEditable = true;

                // Clear Custom Text
                vm.Video_Speed_SelectedIndex = -1;
            }

            // -------------------------
            // Disable Copy on change
            // -------------------------
            //VideoControls.AutoCopyVideoCodec(vm);
            //SubtitleControls.AutoCopySubtitleCodec(vm);
        }

        // Speed Custom KeyDown
        private void cboVideo_Speed_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers and Backspace
            AllowOnlyNumbersAndBackspace(e);
        }


        /// <summary>
        ///    Presets
        /// </summary>
        private void cboPreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Presets.SetPreset(vm);
        }

        /// <summary>
        ///    Import Preset
        /// </summary>
        private void btnImportPreset_Click(object sender, RoutedEventArgs e)
        {
            // Check if presets directory exists
            // If not, create it
            //Directory.CreateDirectory(presetsDir);

            // Open 'Select File'
            Microsoft.Win32.OpenFileDialog selectFile = new Microsoft.Win32.OpenFileDialog();

            // Defaults
            selectFile.InitialDirectory = vm.CustomPresetsPath_Text;
            selectFile.RestoreDirectory = true;
            selectFile.Filter = "ini file (*.ini)|*.ini";

            // Show select file dialog box
            Nullable<bool> result = selectFile.ShowDialog();

            // Process dialog box
            if (result == true)
            {
                // Set Preset Dir, Name, Ext
                string presetsDir = Path.GetDirectoryName(selectFile.FileName).TrimEnd('\\') + @"\";
                //string presetsFileName = Path.GetFileName(selectFile.FileName);
                string presetFileName = Path.GetFileNameWithoutExtension(selectFile.FileName);
                string presetExt = Path.GetExtension(selectFile.FileName);
                string preset = presetsDir + presetFileName + presetExt;
                //string presets = Path.Combine(presetsDir, presetsFileName);

                // Import ini file
                Profiles.ImportPreset(vm, preset);
            }
        }


        /// <summary>
        ///    Export Preset
        /// </summary>
        private void btnSavePreset_Click(object sender, RoutedEventArgs e)
        {
            // Check if Profiles Directory exists
            //bool exists = Directory.Exists(vm.ProfilesPath_Text);
            // If not, create it
            if (!Directory.Exists(vm.CustomPresetsPath_Text))
            {
                // Yes/No Dialog Confirmation
                //
                MessageBoxResult resultExport = MessageBox.Show("Profiles Folder does not exist. Automatically create it?",
                                                                "Directory Not Found",
                                                                MessageBoxButton.YesNo,
                                                                MessageBoxImage.Information);
                switch (resultExport)
                {
                    // Create
                    case MessageBoxResult.Yes:
                        try
                        {
                            Directory.CreateDirectory(vm.CustomPresetsPath_Text);
                        }
                        catch
                        {
                            MessageBox.Show("Could not create Profiles folder. May require Administrator privileges.",
                                            "Error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);
                        }
                        break;
                    // Use Default
                    case MessageBoxResult.No:
                        break;
                }
            }

            // Open 'Save File'
            Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();

            // Defaults
            saveFile.InitialDirectory = vm.CustomPresetsPath_Text;
            saveFile.RestoreDirectory = true;
            saveFile.Filter = "Initialization Files (*.ini)|*.ini";
            saveFile.DefaultExt = ".ini";
            saveFile.FileName = "Custom Preset.ini";

            // Show save file dialog box
            Nullable<bool> result = saveFile.ShowDialog();

            // Process dialog box
            if (result == true)
            {
                // Set Preset Dir, Name, Ext
                string presetsDir = Path.GetDirectoryName(saveFile.FileName).TrimEnd('\\') + @"\";
                //string presetsFileName = Path.GetFileName(saveFile.FileName);
                string presetFileName = Path.GetFileNameWithoutExtension(saveFile.FileName);
                string presetExt = Path.GetExtension(saveFile.FileName);
                string preset = presetsDir + presetFileName + presetExt;
                //string presets = Path.Combine(presetsDir, presetsFileName);

                // -------------------------
                // Overwriting doesn't work properly with INI Writer
                // Delete File instead before saving new
                // -------------------------
                if (File.Exists(preset))
                {
                    try
                    {
                        File.Delete(preset);
                    }
                    catch
                    {
                        
                    }
                }

                // -------------------------
                // Save Custom Preset ini file
                // -------------------------
                Profiles.ExportPreset(vm, preset);

                // -------------------------
                // Load Custom Presets
                // Refresh Presets ComboBox
                // -------------------------
                Profiles.LoadCustomPresets(vm); // not working
            }
        }


        /// <summary>
        ///    Video Optimize Combobox
        /// </summary>
        private void cboVideo_Optimize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Optimize Controls
            // -------------------------
            VideoControls.OptimizeControls(vm);
        }

        /// <summary>
        ///    Video Optimize Expander
        /// </summary>
        //// Expanded
        //private void expVideo_Optimize_Expander_Expanded(object sender, RoutedEventArgs e)
        //{

        //}
        //// Collapsed
        //private void expVideo_Optimize_Expander_Collapsed(object sender, RoutedEventArgs e)
        //{

        //}
        //// Mouse Down
        //private void expVideo_Optimize_Expander_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{

        //}
        //// Mouse Up
        //private void expVideo_Optimize_Expander_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        //{

        //}


        /// <summary>
        ///    Video Size Combobox
        /// </summary>
        private void cboVideo_Scale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Set Video Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
            //VideoControls.AutoCopyVideoCodec(vm);
            //SubtitleControls.AutoCopySubtitleCodec(vm);

            // --------------------------------------------------
            // Enable/Disable Size
            // --------------------------------------------------
            // -------------------------
            // Custom
            // -------------------------
            if (vm.Video_Scale_SelectedItem == "Custom")
            {
                vm.Video_Width_IsEnabled = true;
                vm.Video_Height_IsEnabled = true;

                vm.Video_ScalingAlgorithm_IsEnabled = true;
            }

            // -------------------------
            // Source
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "Source")
            {
                vm.Video_Width_IsEnabled = false;
                vm.Video_Height_IsEnabled = false;

                vm.Video_ScalingAlgorithm_IsEnabled = false;
            }

            // -------------------------
            // All Other Sizes
            // -------------------------
            else
            {
                vm.Video_Width_IsEnabled = false;
                vm.Video_Height_IsEnabled = false;

                vm.Video_ScalingAlgorithm_IsEnabled = true;
            }


            // -------------------------
            // Update Width/Height TextBox Display
            // -------------------------
            VideoScaleDisplay(vm);
        }



        /// <summary>
        ///     Video Scale Display
        /// </summary>
        /// <remarks>
        ///     If Input Video is Widescreen (16:9, 16:10, etc) or auto, scale by Width -vf "scale=1920:-2" 
        ///     If Input Video is Full Screen (4:3, 5:4, etc), scale by Height -vf "scale=-2:1080" 
        /// </remarks>
        public static void VideoScaleDisplay(ViewModel vm)
        {
            // --------------------------------------------------
            // Change TextBox Resolution Numbers
            // --------------------------------------------------
            // -------------------------
            // Source
            // -------------------------
            if (vm.Video_Scale_SelectedItem == "Source")
            {
                vm.Video_Width_Text = "auto";
                vm.Video_Height_Text = "auto";

                vm.Video_ScalingAlgorithm_SelectedItem = "auto";
            }

            // -------------------------
            // 8K
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "8K")
            {
                // Widescreen
                ////if (IsAspectRatioWidescreen(vm.Video_AspectRatio_SelectedItem) == true)
                if (vm.Video_ScreenFormat_SelectedItem == "auto" ||
                    vm.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                    vm.Video_ScreenFormat_SelectedItem == "Ultrawide"
                    )
                {
                    vm.Video_Width_Text = "8192";
                    vm.Video_Height_Text = "auto";
                }

                // Full Screen
                else if (vm.Video_ScreenFormat_SelectedItem == "Full Screen")
                {
                    vm.Video_Width_Text = "auto";
                    vm.Video_Height_Text = "4320";
                }
            }

            // -------------------------
            // 8K UHD
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "8K UHD")
            {
                // Widescreen
                ////if (IsAspectRatioWidescreen(vm.Video_AspectRatio_SelectedItem) == true)
                if (vm.Video_ScreenFormat_SelectedItem == "auto" ||
                    vm.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                    vm.Video_ScreenFormat_SelectedItem == "Ultrawide"
                    )
                {
                    vm.Video_Width_Text = "7680";
                    vm.Video_Height_Text = "auto";
                }

                // Full Screen
                else if (vm.Video_ScreenFormat_SelectedItem == "Full Screen")
                {
                    vm.Video_Width_Text = "auto";
                    vm.Video_Height_Text = "4320";
                }
            }

            // -------------------------
            // 4K
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "4K")
            {
                // Widescreen
                ////if (IsAspectRatioWidescreen(vm.Video_AspectRatio_SelectedItem) == true)
                if (vm.Video_ScreenFormat_SelectedItem == "auto" ||
                    vm.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                    vm.Video_ScreenFormat_SelectedItem == "Ultrawide"
                    )
                {
                    vm.Video_Width_Text = "4096";
                    vm.Video_Height_Text = "auto";
                }

                // Full Screen
                else if (vm.Video_ScreenFormat_SelectedItem == "Full Screen")
                {
                    vm.Video_Width_Text = "auto";
                    vm.Video_Height_Text = "2160";
                }
            }

            // -------------------------
            // 4K UHD
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "4K UHD")
            {
                // Widescreen
                ////if (IsAspectRatioWidescreen(vm.Video_AspectRatio_SelectedItem) == true)
                if (vm.Video_ScreenFormat_SelectedItem == "auto" ||
                    vm.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                    vm.Video_ScreenFormat_SelectedItem == "Ultrawide"
                    )
                {
                    vm.Video_Width_Text = "3840";
                    vm.Video_Height_Text = "auto";
                }

                // Full Screen
                else if (vm.Video_ScreenFormat_SelectedItem == "Full Screen")
                {
                    vm.Video_Width_Text = "auto";
                    vm.Video_Height_Text = "2160";
                }
            }

            // -------------------------
            // 2K
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "2K")
            {
                // Widescreen
                //if (IsAspectRatioWidescreen(vm.Video_AspectRatio_SelectedItem) == true)
                if (vm.Video_ScreenFormat_SelectedItem == "auto" ||
                    vm.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                    vm.Video_ScreenFormat_SelectedItem == "Ultrawide"
                    )
                {
                    vm.Video_Width_Text = "2048";
                    vm.Video_Height_Text = "auto";
                }

                // Full Screen
                else if (vm.Video_ScreenFormat_SelectedItem == "Full Screen")
                {
                    vm.Video_Width_Text = "auto";
                    vm.Video_Height_Text = "1556";
                }
            }

            // -------------------------
            // 1440p
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "1440p")
            {
                // Widescreen
                //if (IsAspectRatioWidescreen(vm.Video_AspectRatio_SelectedItem) == true)
                if (vm.Video_ScreenFormat_SelectedItem == "auto" ||
                    vm.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                    vm.Video_ScreenFormat_SelectedItem == "Ultrawide"
                    )
                {

                    vm.Video_Width_Text = "2560";
                    vm.Video_Height_Text = "auto";
                }

                // Full Screen
                else if (vm.Video_ScreenFormat_SelectedItem == "Full Screen")
                {
                    vm.Video_Width_Text = "auto";
                    vm.Video_Height_Text = "1440";
                }
            }

            // -------------------------
            // 1200p
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "1200p")
            {
                // Widescreen
                //if (IsAspectRatioWidescreen(vm.Video_AspectRatio_SelectedItem) == true)
                if (vm.Video_ScreenFormat_SelectedItem == "auto" ||
                    vm.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                    vm.Video_ScreenFormat_SelectedItem == "Ultrawide"
                    )
                {
                    vm.Video_Width_Text = "1920";
                    vm.Video_Height_Text = "auto";
                }

                // Full Screen
                else if (vm.Video_ScreenFormat_SelectedItem == "Full Screen")
                {
                    vm.Video_Width_Text = "auto";
                    vm.Video_Height_Text = "1200";
                }
            }

            // -------------------------
            // 1080p
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "1080p")
            {
                // Widescreen
                //if (IsAspectRatioWidescreen(vm.Video_AspectRatio_SelectedItem) == true)
                if (vm.Video_ScreenFormat_SelectedItem == "auto" ||
                    vm.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                    vm.Video_ScreenFormat_SelectedItem == "Ultrawide"
                    )
                {
                    vm.Video_Width_Text = "1920";
                    vm.Video_Height_Text = "auto";
                }

                // Full Screen
                else if (vm.Video_ScreenFormat_SelectedItem == "Full Screen")
                {
                    vm.Video_Width_Text = "auto";
                    vm.Video_Height_Text = "1080";
                }
            }

            // -------------------------
            // 900p
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "900p")
            {
                // Widescreen
                //if (IsAspectRatioWidescreen(vm.Video_AspectRatio_SelectedItem) == true)
                if (vm.Video_ScreenFormat_SelectedItem == "auto" ||
                    vm.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                    vm.Video_ScreenFormat_SelectedItem == "Ultrawide"
                    )
                {
                    vm.Video_Width_Text = "1600";
                    vm.Video_Height_Text = "auto";
                }

                // Full Screen
                else if (vm.Video_ScreenFormat_SelectedItem == "Full Screen")
                {
                    vm.Video_Width_Text = "auto";
                    vm.Video_Height_Text = "1080";
                }
            }

            // -------------------------
            // 720p
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "720p")
            {
                // Widescreen
                //if (IsAspectRatioWidescreen(vm.Video_AspectRatio_SelectedItem) == true)
                if (vm.Video_ScreenFormat_SelectedItem == "auto" ||
                    vm.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                    vm.Video_ScreenFormat_SelectedItem == "Ultrawide"
                    )
                {
                    vm.Video_Width_Text = "1280";
                    vm.Video_Height_Text = "auto";
                }

                // Full Screen
                else if (vm.Video_ScreenFormat_SelectedItem == "Full Screen")
                {
                    vm.Video_Width_Text = "auto";
                    vm.Video_Height_Text = "720";
                }
            }

            // -------------------------
            // 576p
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "576p")
            {
                // Widescreen
                //if (IsAspectRatioWidescreen(vm.Video_AspectRatio_SelectedItem) == true)
                if (vm.Video_ScreenFormat_SelectedItem == "auto" ||
                    vm.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                    vm.Video_ScreenFormat_SelectedItem == "Ultrawide"
                    )
                {
                    vm.Video_Width_Text = "1024";
                    vm.Video_Height_Text = "auto";
                }

                // Full Screen
                else if (vm.Video_ScreenFormat_SelectedItem == "Full Screen")
                {
                    vm.Video_Width_Text = "auto";
                    vm.Video_Height_Text = "576";
                }
            }

            // -------------------------
            // 480p
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "480p")
            {
                // Widescreen
                //if (IsAspectRatioWidescreen(vm.Video_AspectRatio_SelectedItem) == true)
                if (vm.Video_ScreenFormat_SelectedItem == "auto" ||
                    vm.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                    vm.Video_ScreenFormat_SelectedItem == "Ultrawide"
                    )
                {
                    vm.Video_Width_Text = "720";
                    vm.Video_Height_Text = "auto";
                }

                // Full Screen
                else if (vm.Video_ScreenFormat_SelectedItem == "Full Screen")
                {
                    vm.Video_Width_Text = "auto";
                    vm.Video_Height_Text = "480";
                }
            }

            // -------------------------
            // 320p
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "320p")
            {
                // Widescreen
                //if (IsAspectRatioWidescreen(vm.Video_AspectRatio_SelectedItem) == true)
                if (vm.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                    vm.Video_ScreenFormat_SelectedItem == "Ultrawide"
                    )
                {
                    vm.Video_Width_Text = "480";
                    vm.Video_Height_Text = "auto";
                }

                // Full Screen
                else if (vm.Video_ScreenFormat_SelectedItem == "Full Screen")
                {
                    vm.Video_Width_Text = "auto";
                    vm.Video_Height_Text = "320";
                }
            }

            // -------------------------
            // 240p
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "240p")
            {
                // Widescreen
                //if (IsAspectRatioWidescreen(vm.Video_AspectRatio_SelectedItem) == true)
                if (vm.Video_ScreenFormat_SelectedItem == "auto" ||
                    vm.Video_ScreenFormat_SelectedItem == "Widescreen" ||
                    vm.Video_ScreenFormat_SelectedItem == "Ultrawide"
                    )
                {
                    vm.Video_Width_Text = "320";
                    vm.Video_Height_Text = "auto";
                }

                // Full Screen
                else if (vm.Video_ScreenFormat_SelectedItem == "Full Screen")
                {
                    vm.Video_Width_Text = "auto";
                    vm.Video_Height_Text = "240";
                }
            }

            // -------------------------
            // Custom
            // -------------------------
            else if (vm.Video_Scale_SelectedItem == "Custom")
            {
                vm.Video_Width_Text = "auto";
                vm.Video_Height_Text = "auto";
            }
        }


        /// <summary>
        ///     Is Aspect Ratio Widescreen
        /// </summary>
        //public static bool IsAspectRatioWidescreen(string aspectRatio_SelectedItem)
        //{
        //    // Widescreen (16:9, 16:10, etc) or auto, scale by Width 
        //    if (aspectRatio_SelectedItem == "auto" ||
        //        aspectRatio_SelectedItem == "14:10" ||
        //        aspectRatio_SelectedItem == "16:9" ||
        //        aspectRatio_SelectedItem == "16:10" ||
        //        aspectRatio_SelectedItem == "19:10" ||
        //        aspectRatio_SelectedItem == "21:9" ||
        //        aspectRatio_SelectedItem == "32:9" ||
        //        aspectRatio_SelectedItem == "240:100"
        //        )
        //    {
        //        return true;
        //    }

        //    // Full Screen (4:3, 5:4, etc), scale by Height
        //    else
        //    {
        //        return false;
        //    }
        //}


        /// <summary>
        ///     Width Textbox Change
        /// </summary>
        // Got Focus
        private void tbxVideo_Width_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxVideo_Width.Focus() == true && 
                vm.Video_Width_Text == "auto")
            {
                vm.Video_Width_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxVideo_Width_LostFocus(object sender, RoutedEventArgs e)
        {
            vm.Video_Width_Text = tbxVideo_Width.Text;

            // Change textbox back to "auto" if left empty
            if (string.IsNullOrEmpty(vm.Video_Width_Text))
            {
                vm.Video_Width_Text = "auto";
            }
        }

        /// <summary>
        ///     Height Textbox Change
        /// </summary>
        // Got Focus
        private void tbxVideo_Height_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (tbxVideo_Height.Focus() == true && 
                vm.Video_Height_Text == "auto")
            {
                vm.Video_Height_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxVideo_Height_LostFocus(object sender, RoutedEventArgs e)
        {
            vm.Video_Height_Text = tbxVideo_Height.Text;

            // Change textbox back to "height" if left empty
            if (string.IsNullOrEmpty(vm.Video_Height_Text))
            {
                vm.Video_Height_Text = "auto";
            }
        }


        /// <summary>
        ///     Video Screen Format
        /// </summary>
        private void cboVideo_ScreenFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //VideoControls.AutoCopyVideoCodec(vm);

            if (vm.Video_Scale_SelectedItem != "Custom")
            {
                VideoScaleDisplay(vm);
            }
        }


        /// <summary>
        ///     Video Aspect Ratio
        /// </summary>
        private void cboVideo_AspectRatio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //VideoControls.AutoCopyVideoCodec(vm);

            //VideoScaleDisplay(vm);
        }


        /// <summary>
        ///     Video Scaling Algorithm
        /// </summary>
        private void cboVideo_ScalingAlgorithm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //VideoControls.AutoCopyVideoCodec(vm);
        }


        /// <summary>
        ///    Crop Window - Button
        /// </summary>
        private void btnVideo_Crop_Click(object sender, RoutedEventArgs e)
        {
            // Start Window
            cropwindow = new CropWindow(this, vm);

            // Detect which screen we're on
            var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
            var thisScreen = allScreens.SingleOrDefault(s => Left >= s.WorkingArea.Left && Left < s.WorkingArea.Right);

            // Position Relative to MainWindow
            // Keep from going off screen
            cropwindow.Left = Math.Max((Left + (Width - cropwindow.Width) / 2), thisScreen.WorkingArea.Left);
            cropwindow.Top = Math.Max(Top - cropwindow.Height - 12, thisScreen.WorkingArea.Top);

            // Keep Window on Top
            cropwindow.Owner = Window.GetWindow(this);

            // Open Window
            cropwindow.ShowDialog();
        }


        /// <summary>
        ///    Crop Clear Button
        /// </summary>
        private void btnVideo_CropClear_Click(object sender, RoutedEventArgs e)
        {
            // Clear Crop Values
            CropWindow.CropClear(vm);
        }



        /// <summary>
        ///    Subtitle Codec - ComboBox
        /// </summary>
        private void cboSubtitle_Codec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Set Controls
            // -------------------------
            SubtitleControls.SetControls(vm, vm.Subtitle_Codec_SelectedItem);

            //// -------------------------
            //// None Codec
            //// -------------------------
            //if (vm.Subtitle_Codec_SelectedItem == "None")
            //{
            //    vm.Subtitle_Stream_SelectedItem = "none";
            //    vm.Subtitle_Stream_IsEnabled = false;
            //}

            //// -------------------------
            //// Burn Codec
            //// -------------------------
            //else if (vm.Subtitle_Codec_SelectedItem == "Burn")
            //{
            //    // Force Select External
            //    // Can't burn All subtitle streams
            //    vm.Subtitle_Stream_SelectedItem = "external";
            //    vm.Subtitle_Stream_IsEnabled = true;
            //}

            //// -------------------------
            //// Copy Codec
            //// -------------------------
            //else if (vm.Subtitle_Codec_SelectedItem == "Copy")
            //{
            //    vm.Subtitle_Stream_IsEnabled = true;
            //}

            //// -------------------------
            //// All Other Codecs
            //// -------------------------
            //else
            //{
            //    vm.Subtitle_Stream_IsEnabled = true;
            //}


            // -------------------------
            // Convert Button Text Change
            // -------------------------
            ConvertButtonText(vm);
        }


        /// <summary>
        ///    Subtitle Stream - ComboBox
        /// </summary>
        private void cboSubtitle_Stream_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // External
            // -------------------------
            if (vm.Subtitle_Stream_SelectedItem == "external")
            {
                // Enable External ListView and Buttons
                vm.Subtitle_ListView_IsEnabled = true;

                lstv_Subtitles.Opacity = 1;
            }
            else
            {
                // Disable External ListView and Buttons
                vm.Subtitle_ListView_IsEnabled = false;

                lstv_Subtitles.Opacity = 0.1;
            }
        }

        /// <summary>
        ///     Subtitle ListView
        /// </summary>
        private void lstv_Subtitles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Clear before adding new selected items
            if (vm.Subtitle_ListView_SelectedItems != null &&
                vm.Subtitle_ListView_SelectedItems.Count > 0)
            {
                vm.Subtitle_ListView_SelectedItems.Clear();
                vm.Subtitle_ListView_SelectedItems.TrimExcess();
            }
           
            // Create Selected Items List for ViewModel
            vm.Subtitle_ListView_SelectedItems = lstv_Subtitles.SelectedItems
                                                .Cast<string>()
                                                .ToList();
        }


        /// <summary>
        ///     Subtitle Add
        /// </summary>
        private void btnSubtitle_Add_Click(object sender, RoutedEventArgs e)
        {
            // Open Select File Window
            Microsoft.Win32.OpenFileDialog selectFiles = new Microsoft.Win32.OpenFileDialog();

            // Defaults
            selectFiles.Multiselect = true;
            selectFiles.Filter = "All files (*.*)|*.*|SRT (*.srt)|*.srt|SUB (*.sub)|*.sub|SBV (*.sbv)|*.sbv|ASS (*.ass)|*.ass|SSA (*.ssa)|*.ssa|MPSUB (*.mpsub)|*.mpsub|LRC (*.lrc)|*.lrc|CAP (*.cap)|*.cap";

            // Process Dialog Box
            Nullable<bool> result = selectFiles.ShowDialog();
            if (result == true)
            {
                // Reset
                //SubtitlesClear();

                // Add Selected Files to List
                for (var i = 0; i < selectFiles.FileNames.Length; i++)
                {
                    // Wrap in quotes for ffmpeg -i
                    Subtitle.subtitleFilePathsList.Add("\"" + selectFiles.FileNames[i] + "\"");
                    //MessageBox.Show(Video.subtitleFiles[i]); //debug

                    Subtitle.subtitleFileNamesList.Add(Path.GetFileName(selectFiles.FileNames[i]));

                    // ListView Display File Names + Ext
                    vm.Subtitle_ListView_Items.Add(Path.GetFileName(selectFiles.FileNames[i]));
                }
            }
        }

        /// <summary>
        /// Subtitle Remove
        /// </summary>
        private void btnSubtitle_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Subtitle_ListView_SelectedItems.Count > 0)
            {
                var selectedIndex = vm.Subtitle_ListView_SelectedIndex;

                // ListView Items
                var itemlsvFileNames = vm.Subtitle_ListView_Items[selectedIndex];
                vm.Subtitle_ListView_Items.RemoveAt(selectedIndex);

                // List File Paths
                string itemFilePaths = Subtitle.subtitleFilePathsList[selectedIndex];
                Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);

                // List File Names
                string itemFileNames = Subtitle.subtitleFileNamesList[selectedIndex];
                Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
            }
        }

        /// <summary>
        /// Subtitle Clear All
        /// </summary>
        private void btnSubtitle_Clear_Click(object sender, RoutedEventArgs e)
        {
            SubtitlesClear();
        }

        /// <summary>
        /// Subtitle Clear - Method
        /// </summary>
        public void SubtitlesClear()
        {
            // Clear List View
            //lstv_Subtitles.Items.Clear();
            if (vm.Subtitle_ListView_Items != null &&
                vm.Subtitle_ListView_Items.Count > 0)
            {
                vm.Subtitle_ListView_Items.Clear();
            }

            // Clear Paths List
            if (Subtitle.subtitleFilePathsList != null &&
                Subtitle.subtitleFilePathsList.Count > 0)
            {
                Subtitle.subtitleFilePathsList.Clear();
                Subtitle.subtitleFilePathsList.TrimExcess();
            }

            // Clear Names List
            if (Subtitle.subtitleFileNamesList != null &&
                Subtitle.subtitleFileNamesList.Count > 0)
            {
                Subtitle.subtitleFileNamesList.Clear();
                Subtitle.subtitleFileNamesList.TrimExcess();
            }
        }

        /// <summary>
        /// Subtitle Sort Up
        /// </summary>
        private void btnSubtitle_SortUp_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Subtitle_ListView_SelectedItems.Count > 0)
            {
                var selectedIndex = vm.Subtitle_ListView_SelectedIndex;

                if (selectedIndex > 0)
                {
                    // ListView Items
                    var itemlsvFileNames = vm.Subtitle_ListView_Items[selectedIndex];
                    vm.Subtitle_ListView_Items.RemoveAt(selectedIndex);
                    vm.Subtitle_ListView_Items.Insert(selectedIndex - 1, itemlsvFileNames);

                    // List File Paths
                    string itemFilePaths = Subtitle.subtitleFilePathsList[selectedIndex];
                    Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);
                    Subtitle.subtitleFilePathsList.Insert(selectedIndex - 1, itemFilePaths);

                    // List File Names
                    string itemFileNames = Subtitle.subtitleFileNamesList[selectedIndex];
                    Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
                    Subtitle.subtitleFileNamesList.Insert(selectedIndex - 1, itemFileNames);

                    // Highlight Selected Index
                    vm.Subtitle_ListView_SelectedIndex = selectedIndex - 1;
                }
            }
        }

        /// <summary>
        /// Subtitle Sort Down
        /// </summary>
        private void btnSubtitle_SortDown_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Subtitle_ListView_SelectedItems.Count > 0)
            {
                var selectedIndex = vm.Subtitle_ListView_SelectedIndex;

                if (selectedIndex + 1 < vm.Subtitle_ListView_Items.Count)
                {
                    // ListView Items
                    var itemlsvFileNames = vm.Subtitle_ListView_Items[selectedIndex];
                    vm.Subtitle_ListView_Items.RemoveAt(selectedIndex);
                    vm.Subtitle_ListView_Items.Insert(selectedIndex + 1, itemlsvFileNames);

                    // List FilePaths
                    string itemFilePaths = Subtitle.subtitleFilePathsList[selectedIndex];
                    Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);
                    Subtitle.subtitleFilePathsList.Insert(selectedIndex + 1, itemFilePaths);

                    // List File Names
                    string itemFileNames = Subtitle.subtitleFileNamesList[selectedIndex];
                    Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
                    Subtitle.subtitleFileNamesList.Insert(selectedIndex + 1, itemFileNames);

                    // Highlight Selected Index
                    vm.Subtitle_ListView_SelectedIndex = selectedIndex + 1;
                }
            }
        }



        /// <summary>
        ///    Audio Codec - ComboBox
        /// </summary>
        private void cboAudio_Codec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Set Controls
            // -------------------------
            AudioControls.SetControls(vm, vm.Audio_Codec_SelectedItem);

            // -------------------------
            // Audio Stream Controls
            // -------------------------
            FormatControls.AudioStreamControls(vm);

            // -------------------------
            // Convert Button Text Change
            // -------------------------
            ConvertButtonText(vm);
        }


        /// <summary>
        ///    Audio Channel - ComboBox
        /// </summary>
        private void cboAudio_Channel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //AudioControls.AutoCopyAudioCodec(vm);
        }


        /// <summary>
        ///    Audio Quality - ComboBox
        /// </summary>
        private void cboAudio_Quality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Set Controls
            // -------------------------
            //AudioControls.SetControls(vm, vm.Audio_Codec_SelectedItem);

            // -------------------------
            // Quality Controls
            // -------------------------
            AudioControls.QualityControls(vm);

            // -------------------------
            // Display Bit Rate in TextBox
            // -------------------------
            AudioControls.AudioBitRateDisplay(vm,
                                              vm.Audio_Quality_Items,
                                              vm.Audio_Quality_SelectedItem
                                              );

        }


        /// <summary>
        ///    Audio VBR - Toggle
        /// </summary>
        // Checked
        private void tglAudio_VBR_Checked(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Quality Controls
            // -------------------------
            AudioControls.QualityControls(vm);

            // -------------------------
            // Display Bit Rate in TextBox
            // -------------------------
            AudioControls.AudioBitRateDisplay(vm,
                                              vm.Audio_Quality_Items,
                                              vm.Audio_Quality_SelectedItem
                                              );
        }
        // Unchecked
        private void tglAudio_VBR_Unchecked(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Quality Controls
            // -------------------------
            AudioControls.QualityControls(vm);

            // -------------------------
            // Display Bit Rate in TextBox
            // -------------------------
            AudioControls.AudioBitRateDisplay(vm,
                                              vm.Audio_Quality_Items,
                                              vm.Audio_Quality_SelectedItem
                                              );
        }


        /// <summary>
        ///     Audio Custom BitRate kbps - Textbox
        /// </summary>
        private void tbxAudio_BitRate_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers and Backspace
            AllowOnlyNumbersAndBackspace(e);
        }
        // Got Focus
        private void tbxAudio_BitRate_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear Textbox on first use
            if (vm.Audio_BitRate_Text == string.Empty)
            {
                TextBox tbac = (TextBox)sender;
                tbac.Text = string.Empty;
                tbac.GotFocus += tbxAudio_BitRate_GotFocus; //used to be -=
            }
        }
        // Lost Focus
        private void tbxAudio_BitRate_LostFocus(object sender, RoutedEventArgs e)
        {
            // Change Textbox back to kbps
            TextBox tbac = sender as TextBox;
            if (tbac.Text.Trim().Equals(string.Empty))
            {
                tbac.Text = string.Empty;
                tbac.GotFocus -= tbxAudio_BitRate_GotFocus; //used to be +=
            }
        }


        /// <summary>
        ///     Samplerate ComboBox
        /// </summary>
        private void cboAudio_SampleRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(vm.Audio_SampleRate_SelectedItem))
            //{
            //    Audio_SampleRate_PreviousItem = vm.Audio_SampleRate_SelectedItem;
            //}

            //MessageBox.Show("Previous: " + Audio_SampleRate_PreviousItem); //debug
            //MessageBox.Show("Current: " + vm.Audio_SampleRate_SelectedItem); //debug

            //if (Audio_SampleRate_PreviousItem != vm.Audio_SampleRate_SelectedItem)
            //{
            //    // Switch to Copy if inputExt & outputExt match
            //    AudioControls.AutoCopyAudioCodec(vm);
            //}

            //MessageBox.Show("Current Changed: " + vm.Audio_SampleRate_SelectedItem); //debug
        }


        /// <summary>
        ///     Bit Depth ComboBox
        /// </summary>
        private void cboAudio_BitDepth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm.Audio_Codec_SelectedItem == "PCM")
            {
                PCM.Codec_Set(vm);
            }
        }


        /// <summary>
        ///    Volume TextBox Changed
        /// </summary>
        private void tbxAudio_Volume_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Disable Volume instead of running AutoCopyAudioCodec each time 
            // This needs to be re-thought, calling method on every timer tick
            //AudioControls.AutoCopyAudioCodec(vm);
        }
        /// <summary>
        ///    Volume TextBox KeyDown
        /// </summary>
        private void tbxAudio_Volume_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers and Backspace
            AllowOnlyNumbersAndBackspace(e);
        }

        /// <summary>
        ///    Volume Buttons
        /// </summary>
        // -------------------------
        // Up
        // -------------------------
        // Volume Up Button Click
        private void btnAudio_VolumeUp_Click(object sender, RoutedEventArgs e)
        {
            int value;
            int.TryParse(vm.Audio_Volume_Text, out value);

            value += 1;
            vm.Audio_Volume_Text = value.ToString();
        }
        // Up Button Each Timer Tick
        private void dispatcherTimerUp_Tick(object sender, EventArgs e)
        {
            int value;
            int.TryParse(vm.Audio_Volume_Text, out value);

            value += 1;
            vm.Audio_Volume_Text = value.ToString();
        }
        // Hold Up Button
        private void btnAudio_VolumeUp_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Timer      
            dispatcherTimerUp.Interval = new TimeSpan(0, 0, 0, 0, 100); //100ms
            dispatcherTimerUp.Start();
        }
        // Up Button Released
        private void btnAudio_VolumeUp_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Disable Timer
            dispatcherTimerUp.Stop();
        }
        // -------------------------
        // Down
        // -------------------------
        // Volume Down Button Click
        private void btnAudio_VolumeDown_Click(object sender, RoutedEventArgs e)
        {
            int value;
            int.TryParse(vm.Audio_Volume_Text, out value);

            value -= 1;
            vm.Audio_Volume_Text = value.ToString();
        }
        // Down Button Each Timer Tick
        private void dispatcherTimerDown_Tick(object sender, EventArgs e)
        {
            int value;
            int.TryParse(vm.Audio_Volume_Text, out value);

            value -= 1;
            vm.Audio_Volume_Text = value.ToString();
        }
        // Hold Down Button
        private void btnAudio_VolumeDown_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Timer      
            dispatcherTimerDown.Interval = new TimeSpan(0, 0, 0, 0, 100); //100ms
            dispatcherTimerDown.Start();
        }
        // Down Button Released
        private void btnAudio_VolumeDown_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Disable Timer
            dispatcherTimerDown.Stop();
        }


        /// <summary>
        ///     Audio Hard Limiter - Slider
        /// </summary>
        private void slAudio_HardLimiter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.Audio_HardLimiter_Value = 1;

            AudioControls.AutoCopyAudioCodec(vm);
        }

        private void slAudio_HardLimiter_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }

        private void tbxAudio_HardLimiter_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }



        // --------------------------------------------------------------------------------------------------------
        // Filters
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        ///     Filter - Selective SelectiveColorPreview - ComboBox
        /// </summary>
        public static List<VideoFilters.FilterVideoSelectiveColor> cboSelectiveColor_Items = new List<VideoFilters.FilterVideoSelectiveColor>()
        {
            new VideoFilters.FilterVideoSelectiveColor("Reds", Colors.Red),
            new VideoFilters.FilterVideoSelectiveColor("Yellows", Colors.Yellow),
            new VideoFilters.FilterVideoSelectiveColor("Greens", Colors.Green),
            new VideoFilters.FilterVideoSelectiveColor("Cyans", Colors.Cyan),
            new VideoFilters.FilterVideoSelectiveColor("Blues", Colors.Blue),
            new VideoFilters.FilterVideoSelectiveColor("Magentas", Colors.Magenta),
            new VideoFilters.FilterVideoSelectiveColor("Whites", Colors.White),
            new VideoFilters.FilterVideoSelectiveColor("Neutrals", Colors.Gray),
            new VideoFilters.FilterVideoSelectiveColor("Blacks", Colors.Black),
        };
        //public static List<VideoFilters.FilterVideoSelectiveColor> _cboSelectiveColor_Previews
        //{
        //    get { return _cboSelectiveColor_Previews; }
        //    set { _cboSelectiveColor_Previews = value; }
        //}

        private void cboFilterVideo_SelectiveColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Switch Tab SelectiveColorPreview
            tabControl_SelectiveColor.SelectedIndex = 0;

            var selectedItem = (VideoFilters.FilterVideoSelectiveColor)cboFilterVideo_SelectiveColor.SelectedItem;
            string color = selectedItem.SelectiveColorName;

            if (color == "Reds")
            {
                tabControl_SelectiveColor.SelectedItem = selectedItem;
                tabItem_SelectiveColor_Reds.IsSelected = true;
            }
            else if (color == "Yellows")
            {
                tabControl_SelectiveColor.SelectedItem = selectedItem;
                tabItem_SelectiveColor_Yellows.IsSelected = true;
            }
            else if (color == "Greens")
            {
                tabControl_SelectiveColor.SelectedItem = selectedItem;
                tabItem_SelectiveColor_Greens.IsSelected = true;
            }
            else if (color == "Cyans")
            {
                tabControl_SelectiveColor.SelectedItem = selectedItem;
                tabItem_SelectiveColor_Cyans.IsSelected = true;
            }
            else if (color == "Blues")
            {
                tabControl_SelectiveColor.SelectedItem = selectedItem;
                tabItem_SelectiveColor_Blues.IsSelected = true;
            }
            else if (color == "Magentas")
            {
                tabControl_SelectiveColor.SelectedItem = selectedItem;
                tabItem_SelectiveColor_Magentas.IsSelected = true;
            }
            else if (color == "Whites")
            {
                tabControl_SelectiveColor.SelectedItem = selectedItem;
                tabItem_SelectiveColor_Whites.IsSelected = true;
            }
            else if (color == "Neutrals")
            {
                tabControl_SelectiveColor.SelectedItem = selectedItem;
                tabItem_SelectiveColor_Neutrals.IsSelected = true;
            }
            else if (color == "Blacks")
            {
                tabControl_SelectiveColor.SelectedItem = selectedItem;
                tabItem_SelectiveColor_Blacks.IsSelected = true;
            }
        }

        /// <summary>
        ///     Filter Video - Selective Color Sliders
        /// </summary>
        // Reds Cyan
        private void slFilterVideo_SelectiveColor_Reds_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Reds_Cyan_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Reds_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Reds_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Reds Magenta
        private void slFilterVideo_SelectiveColor_Reds_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Reds_Magenta_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Reds_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Reds Yellow
        private void slFilterVideo_SelectiveColor_Reds_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Reds_Yellow_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Reds_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        // Yellows Cyan
        private void slFilterVideo_SelectiveColor_Yellows_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Yellows_Cyan_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Yellows_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        private void tbxFilterVideo_SelectiveColor_Yellows_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Yellows Magenta
        private void slFilterVideo_SelectiveColor_Yellows_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Yellows_Magenta_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Yellows_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Yellows_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        // Yellows Yellow
        private void slFilterVideo_SelectiveColor_Yellows_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Yellows_Yellow_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Yellows_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Yellows_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        // Greens Cyan
        private void slFilterVideo_SelectiveColor_Greens_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Greens_Cyan_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Greens_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Greens_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Greens Magenta
        private void slFilterVideo_SelectiveColor_Greens_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Greens_Magenta_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Greens_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Greens_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Greens Yellow
        private void slFilterVideo_SelectiveColor_Greens_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Greens_Yellow_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Greens_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Greens_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        // Cyans Cyan
        private void slFilterVideo_SelectiveColor_Cyans_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Cyans_Cyan_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Cyans_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Cyans_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        // Cyans Magenta
        private void slFilterVideo_SelectiveColor_Cyans_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Cyans_Magenta_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Cyans_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Cyans_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Cyans Yellow
        private void slFilterVideo_SelectiveColor_Cyans_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Cyans_Yellow_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Cyans_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Cyans_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        // Blues Cyan
        private void slFilterVideo_SelectiveColor_Blues_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Blues_Cyan_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Blues_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Blues_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Blues Magneta
        private void slFilterVideo_SelectiveColor_Blues_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Blues_Magenta_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Blues_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Blues_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Blues Yellow
        private void slFilterVideo_SelectiveColor_Blues_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Blues_Yellow_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Blues_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Blues_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        // Magentas Cyan
        private void slFilterVideo_SelectiveColor_Magentas_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Magentas_Cyan_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Magentas_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Magentas_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Magentas Magenta
        private void slFilterVideo_SelectiveColor_Magentas_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Magentas_Magenta_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Magentas_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        private void tbxFilterVideo_SelectiveColor_Magentas_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Magentas Yellow
        private void slFilterVideo_SelectiveColor_Magentas_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Magentas_Yellow_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Magentas_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Magentas_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        // Whites Cyan
        private void slFilterVideo_SelectiveColor_Whites_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Whites_Cyan_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Whites_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Whites_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Whites Magenta
        private void slFilterVideo_SelectiveColor_Whites_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Whites_Magenta_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Whites_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Whites_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Whites Yellow
        private void slFilterVideo_SelectiveColor_Whites_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Whites_Yellow_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Whites_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Whites_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        // Neutrals Cyan
        private void slFilterVideo_SelectiveColor_Neutrals_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Neutrals_Cyan_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Neutrals_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Neutrals_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Neutrals Magenta
        private void slFilterVideo_SelectiveColor_Neutrals_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Neutrals_Magenta_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Neutrals_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Neutrals_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Neutrals Yellow
        private void slFilterVideo_SelectiveColor_Neutrals_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Neutrals_Yellow_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Neutrals_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Neutrals_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        // Blacks Cyan
        private void slFilterVideo_SelectiveColor_Blacks_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Blacks_Cyan_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Blacks_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Blacks_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Blacks Magenta
        private void slFilterVideo_SelectiveColor_Blacks_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Blacks_Magenta_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Blacks_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Blacks_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        // Blacks Yellow
        private void slFilterVideo_SelectiveColor_Blacks_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_SelectiveColor_Blacks_Yellow_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_SelectiveColor_Blacks_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_SelectiveColor_Blacks_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }


        /// <summary>
        ///     Filter Video - Selective Color Reset
        /// </summary>
        private void btnFilterVideo_SelectiveColorReset_Click(object sender, RoutedEventArgs e)
        {
            // Reset to default

            // Reds Cyan
            vm.FilterVideo_SelectiveColor_Reds_Cyan_Value = 0;
            // Reds Magenta
            vm.FilterVideo_SelectiveColor_Reds_Magenta_Value = 0;
            // Regs Yellow
            vm.FilterVideo_SelectiveColor_Reds_Yellow_Value = 0;

            // Yellows Cyan
            vm.FilterVideo_SelectiveColor_Yellows_Cyan_Value = 0;
            // Yellows Magenta
            vm.FilterVideo_SelectiveColor_Yellows_Magenta_Value = 0;
            // Yellows Yellow
            vm.FilterVideo_SelectiveColor_Yellows_Yellow_Value = 0;

            // Greens Cyan
            vm.FilterVideo_SelectiveColor_Greens_Cyan_Value = 0;
            // Greens Magenta
            vm.FilterVideo_SelectiveColor_Greens_Magenta_Value = 0;
            // Greens Yellow
            vm.FilterVideo_SelectiveColor_Greens_Yellow_Value = 0;

            // Cyans Cyan
            vm.FilterVideo_SelectiveColor_Cyans_Cyan_Value = 0;
            // Cyans Magenta
            vm.FilterVideo_SelectiveColor_Cyans_Magenta_Value = 0;
            // Cyans Yellow
            vm.FilterVideo_SelectiveColor_Cyans_Yellow_Value = 0;

            // Blues Cyan
            vm.FilterVideo_SelectiveColor_Blues_Cyan_Value = 0;
            // Blues Magneta
            vm.FilterVideo_SelectiveColor_Blues_Magenta_Value = 0;
            // Blues Yellow
            vm.FilterVideo_SelectiveColor_Blues_Yellow_Value = 0;

            // Magentas Cyan
            vm.FilterVideo_SelectiveColor_Magentas_Cyan_Value = 0;
            // Magentas Magenta
            vm.FilterVideo_SelectiveColor_Magentas_Magenta_Value = 0;
            // Magentas Yellow
            vm.FilterVideo_SelectiveColor_Magentas_Yellow_Value = 0;

            // Whites Cyan
            vm.FilterVideo_SelectiveColor_Whites_Cyan_Value = 0;
            // Whites Magenta
            vm.FilterVideo_SelectiveColor_Whites_Magenta_Value = 0;
            // Whites Yellow
            vm.FilterVideo_SelectiveColor_Whites_Yellow_Value = 0;

            // Neutrals Cyan
            vm.FilterVideo_SelectiveColor_Neutrals_Cyan_Value = 0;
            // Neutrals Magenta
            vm.FilterVideo_SelectiveColor_Neutrals_Magenta_Value = 0;
            // Neutrals Yellow
            vm.FilterVideo_SelectiveColor_Neutrals_Yellow_Value = 0;

            // Blacks Cyan
            vm.FilterVideo_SelectiveColor_Blacks_Cyan_Value = 0;
            // Blacks Magenta
            vm.FilterVideo_SelectiveColor_Blacks_Magenta_Value = 0;
            // Blacks Yellow
            vm.FilterVideo_SelectiveColor_Blacks_Yellow_Value = 0;


            VideoControls.AutoCopyVideoCodec(vm);
        }


        /// <summary>
        ///     Filter Video - EQ Sliders
        /// </summary>
        // Brightness
        private void slFilterVideo_EQ_Brightness_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_EQ_Brightness_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_EQ_Brightness_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_EQ_Brightness_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            // Reset Empty to 0
            if (string.IsNullOrWhiteSpace(tbxFilterVideo_EQ_Brightness.Text))
            {
                vm.FilterVideo_EQ_Brightness_Value = 0;
            }

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_EQ_Brightness_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbers(e);
        }

        // Contrast
        private void slFilterVideo_EQ_Contrast_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_EQ_Contrast_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_EQ_Contrast_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_EQ_Contrast_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_EQ_Contrast_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbers(e);
        }

        // Saturation
        private void slFilterVideo_EQ_Saturation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_EQ_Saturation_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_EQ_Saturation_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_EQ_Saturation_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_EQ_Saturation_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbers(e);
        }

        // Gamma
        private void slFilterVideo_EQ_Gamma_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterVideo_EQ_Gamma_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void slFilterVideo_EQ_Gamma_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_EQ_Gamma_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }
        private void tbxFilterVideo_EQ_Gamma_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbers(e);
        }

        // Reset
        private void btnFilterVideo_EQ_Reset_Click(object sender, RoutedEventArgs e)
        {
            // Reset to default

            // Brightness
            vm.FilterVideo_EQ_Brightness_Value = 0;
            // Contrast
            vm.FilterVideo_EQ_Contrast_Value = 0;
            // Saturation
            vm.FilterVideo_EQ_Saturation_Value = 0;
            // Gamma
            vm.FilterVideo_EQ_Gamma_Value = 0;

            VideoControls.AutoCopyVideoCodec(vm);
        }



        /// <summary>
        ///     Filter Video - Deband
        /// </summary>
        private void cboFilterVideo_Deband_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        /// <summary>
        ///     Filter Video - Deshake
        /// </summary>
        private void cboFilterVideo_Deshake_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        /// <summary>
        ///     Filter Video - Deflicker
        /// </summary>
        private void cboFilterVideo_Deflicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        /// <summary>
        ///     Filter Video - Dejudder
        /// </summary>
        private void cboFilterVideo_Dejudder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        /// <summary>
        ///     Filter Video - Denoise
        /// </summary>
        private void cboFilterVideo_Denoise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }

        /// <summary>
        ///     Filter Video - Deinterlace
        /// </summary>
        private void cboFilterVideo_Deinterlace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(vm);
        }



        /// <summary>
        ///     Audio Limiter
        /// </summary>
        private void slAudioLimiter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.Audio_HardLimiter_Value = 1;

            AudioControls.AutoCopyAudioCodec(vm);
        }

        private void slAudioLimiter_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }

        private void tbxAudioLimiter_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }

        /// <summary>
        ///     Filter Audio - Remove Click
        /// </summary>
        //private void slFilterAudio_RemoveClick_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    // Reset to default
        //    slFilterAudio_RemoveClick_Value = 0;

        //    AudioControls.AutoCopyAudioCodec(vm);
        //}

        //private void slFilterAudio_RemoveClick_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    AudioControls.AutoCopyAudioCodec(vm);
        //}

        //private void tbxFilterAudio_RemoveClick_PreviewKeyUp(object sender, KeyEventArgs e)
        //{
        //    AudioControls.AutoCopyAudioCodec(vm);
        //}


        /// <summary>
        ///     Filter Audio - Contrast
        /// </summary>
        // Double Click
        private void slFilterAudio_Contrast_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterAudio_Contrast_Value = 0;

            AudioControls.AutoCopyAudioCodec(vm);
        }
        // Key Down
        private void tbxFilterAudio_Contrast_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbers(e);
        }
        // Key Up
        private void tbxFilterAudio_Contrast_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }
        // Mouse Up
        private void slFilterAudio_Contrast_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }

        /// <summary>
        ///     Filter Audio - Extra Stereo
        /// </summary>
        // Double Click
        private void slFilterAudio_ExtraStereo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterAudio_ExtraStereo_Value = 0;

            AudioControls.AutoCopyAudioCodec(vm);
        }
        // Key Down
        private void tbxFilterAudio_ExtraStereo_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbers(e);
        }
        // Key Up
        private void tbxFilterAudio_ExtraStereo_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }
        // Mouse Up
        private void slFilterAudio_ExtraStereo_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }

        /// <summary>
        ///     Filter Audio - Tempo
        /// </summary>
        // Double Click
        private void slFilterAudio_Tempo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterAudio_Tempo_Value = 100;

            AudioControls.AutoCopyAudioCodec(vm);
        }
        // Key Down
        private void tbxFilterAudio_Tempo_KeyDown(object sender, KeyEventArgs e)
        {
            AllowOnlyNumbers(e);
        }
        // Key Up
        private void tbxFilterAudio_Tempo_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }
        // Mouse Up
        private void slFilterAudio_Tempo_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }



        /// <summary>
        ///     Sort (Method)
        /// </summary>
        public void Sort()
        {
            // Only if Script not empty
            if (!string.IsNullOrEmpty(vm.ScriptView_Text))
            {
                // -------------------------
                // Has Not Been Edited
                // -------------------------
                if (ScriptView.sort == false &&
                    RemoveLineBreaks(vm.ScriptView_Text) == FFmpeg.ffmpegArgs)
                {
                    vm.ScriptView_Text = FFmpeg.ffmpegArgsSort;

                    // Sort is Off
                    ScriptView.sort = true;
                    // Change Button Back to Inline
                    txblScriptSort.Text = "Inline";
                }

                // -------------------------
                // Has Been Edited
                // -------------------------
                else if (ScriptView.sort == false &&
                         RemoveLineBreaks(vm.ScriptView_Text)

                                        != FFmpeg.ffmpegArgs)
                {
                    MessageBox.Show("Cannot sort edited text.",
                                    "Notice",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);

                    return;
                }


                // -------------------------
                // Inline
                // -------------------------
                else if (ScriptView.sort == true)
                {
                    // CMD Arguments are from Script TextBox
                    FFmpeg.ffmpegArgs = RemoveLineBreaks(vm.ScriptView_Text);

                    vm.ScriptView_Text = FFmpeg.ffmpegArgs;

                    // Sort is On
                    ScriptView.sort = false;
                    // Change Button Back to Sort
                    txblScriptSort.Text = "Sort";
                }
            }
        }



        /// <summary>
        ///    Start Process
        /// </summary>
        //public static async void StartProcess(ViewModel vm)
        public static async Task<int> StartProcess(ViewModel vm)
        {
            int count = 0;
            await Task.Factory.StartNew(() =>
            {
                // -------------------------
                // Local File
                // -------------------------
                if (IsWebURL(vm.Input_Text) == false)
                {
                    // -------------------------
                    // Single
                    // -------------------------
                    if (vm.Batch_IsChecked == false)
                    {
                        // -------------------------
                        // FFprobe Detect Metadata
                        // -------------------------
                        FFprobe.Metadata(vm);

                        // -------------------------
                        // FFmpeg Generate Arguments (Single)
                        // -------------------------
                        // disabled if batch
                        FFmpeg.FFmpegSingleGenerateArgs(vm);
                    }

                    // -------------------------
                    // Batch
                    // -------------------------
                    else if (vm.Batch_IsChecked == true)
                    {
                        // -------------------------
                        // FFprobe Video Entry Type Containers
                        // -------------------------
                        FFprobe.VideoEntryType(vm);

                        // -------------------------
                        // FFprobe Video Entry Type Containers
                        // -------------------------
                        FFprobe.AudioEntryType(vm);

                        // -------------------------
                        // FFmpeg Generate Arguments (Batch)
                        // -------------------------
                        //disabled if single file
                        FFmpeg.FFmpegBatchGenerateArgs(vm);
                    }
                }

                // -------------------------
                // YouTube Download
                // -------------------------
                else if (IsWebURL(vm.Input_Text) == true)
                {
                    // -------------------------
                    // Generate Arguments
                    // -------------------------
                    // Do not use FFprobe Metadata Parsing
                    // Video/Audio Auto Quality will add BitRate
                    FFmpeg.YouTubeDownloadGenerateArgs(vm);
                }
            });

            return count;
        }



        /// <summary>
        ///    Script View Copy/Paste
        /// </summary>
        private void OnScriptPaste(object sender, DataObjectPastingEventArgs e)
        {
        }

        private void OnScriptCopy(object sender, DataObjectCopyingEventArgs e)
        {
        }

        /// <summary>
        ///    Script - Button
        /// </summary>
        private void btnScript_Click(object sender, RoutedEventArgs e)
        {
            ScriptButtonAsync();
        }

        public async void ScriptButtonAsync()
        {
            // -------------------------
            // Clear Variables before Run
            // -------------------------
            ClearGlobalVariables(vm);

            // -------------------------
            // Batch Extention Period Check
            // -------------------------
            BatchExtCheck(vm);

            // -------------------------
            // Set FFprobe Path
            // -------------------------
            FFprobePath(vm);

            // -------------------------
            // Set youtube-dl Path
            // -------------------------
            youtubedlPath(vm);

            // -------------------------
            // Reset Sort
            // -------------------------
            ScriptView.sort = false;
            txblScriptSort.Text = "Sort";


            // -------------------------
            // Start Script
            // -------------------------
            if (ReadyHalts(vm) == true)
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("...............................................")) { Foreground = Log.ConsoleAction });
                };
                Log.LogActions.Add(Log.WriteAction);

                // Log Console Message /////////
                DateTime localDate = DateTime.Now;

                // Log Console Message /////////
                Log.WriteAction = () =>
                {

                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run(Convert.ToString(localDate))) { Foreground = Log.ConsoleAction });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Generating Script...")) { Foreground = Log.ConsoleTitle });

                };
                Log.LogActions.Add(Log.WriteAction);

                // -------------------------
                // Start All Processes
                // -------------------------
                //StartProcess(vm);
                Task<int> task = StartProcess(vm);
                int count = await task;

                // -------------------------
                // Generate Script
                // -------------------------
                FFmpeg.FFmpegScript(vm);

                // -------------------------
                // Auto Sort Toggle
                // -------------------------
                if (vm.AutoSortScript_IsChecked == true)
                {
                    Sort();
                }

                // -------------------------
                // Write All Log Actions to Console
                // -------------------------
                Log.LogWriteAll(this, vm);

                // -------------------------
                // Clear Variables for next Run
                // -------------------------
                ClearGlobalVariables(vm);
                GC.Collect();
            }
        }



        /// <summary>
        ///     Save Script
        /// </summary>
        private void btnScriptSave_Click(object sender, RoutedEventArgs e)
        {
            // Open 'Save File'
            Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();

            //saveFile.InitialDirectory = inputDir;
            saveFile.RestoreDirectory = true;
            saveFile.Filter = "Text file (*.txt)|*.txt";
            saveFile.DefaultExt = ".txt";
            saveFile.FileName = "Script";

            // Show save file dialog box
            Nullable<bool> result = saveFile.ShowDialog();

            // Process dialog box
            if (result == true)
            {
                // Save document
                //File.WriteAllText(saveFile.FileName, ScriptView.GetScriptRichTextBoxContents(this), Encoding.Unicode);
                File.WriteAllText(saveFile.FileName, vm.ScriptView_Text, Encoding.Unicode);
            }
        }


        /// <summary>
        ///     Copy All Button
        /// </summary>
        private void btnScriptCopy_Click(object sender, RoutedEventArgs e)
        {
            //Clipboard.SetText(ScriptView.GetScriptRichTextBoxContents(this), TextDataFormat.UnicodeText);
            Clipboard.SetText(vm.ScriptView_Text, TextDataFormat.UnicodeText);
        }


        /// <summary>
        ///     Clear Button
        /// </summary>
        private void btnScriptClear_Click(object sender, RoutedEventArgs e)
        {
            ScriptView.ClearScriptView(vm);
        }


        /// <summary>
        ///     Sort Button
        /// </summary>
        private void btnScriptSort_Click(object sender, RoutedEventArgs e)
        {
            Sort();
        }


        /// <summary>
        /// Run Button
        /// </summary>
        private void btnScriptRun_Click(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Use Arguments from Script TextBox
            // -------------------------
            FFmpeg.ffmpegArgs = ReplaceLineBreaksWithSpaces(
                                    vm.ScriptView_Text
                                );

            // -------------------------
            // Start FFmpeg
            // -------------------------
            FFmpeg.FFmpegStart(vm);

            // -------------------------
            // Create output.log
            // -------------------------
            Log.CreateOutputLog(this, vm);
        }


        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///    Preview Button
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            FFplay.Preview(this, vm);
        }


        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///    Convert Button
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            ConvertButtonAsync();
        }

        public async void ConvertButtonAsync()
        {
            // -------------------------
            // Check if Script has been Edited
            // -------------------------
            if (CheckScriptEdited(vm) == true)
            {
                // Halt
                return;
            }

            // -------------------------
            // Clear Global Variables before each Run
            // -------------------------
            ClearGlobalVariables(vm);

            // -------------------------
            // Batch Extention Period Check
            // -------------------------
            BatchExtCheck(vm);

            // -------------------------
            // Set FFprobe Path
            // -------------------------
            FFprobePath(vm);

            // -------------------------
            // Set youtube-dl Path
            // -------------------------
            youtubedlPath(vm);

            // --------------------------------------------------
            // Start Convert
            // --------------------------------------------------
            if (ReadyHalts(vm) == true)
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("...............................................")) { Foreground = Log.ConsoleAction });

                    // Log Console Message /////////
                    DateTime localDate = DateTime.Now;

                    // Log Console Message /////////

                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run(Convert.ToString(localDate))) { Foreground = Log.ConsoleAction });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Starting Conversion...")) { Foreground = Log.ConsoleTitle });
                };
                Log.LogActions.Add(Log.WriteAction);

                // -------------------------
                // Start All Processes
                // -------------------------
                //StartProcess(vm);
                Task<int> task = StartProcess(vm);
                int count = await task;

                // -------------------------
                // FFmpeg Convert
                // -------------------------
                FFmpeg.FFmpegConvert(vm);

                // -------------------------
                // Sort Script
                // -------------------------
                // Only if Auto Sort is enabled
                if (vm.AutoSortScript_IsChecked == true)
                {
                    ScriptView.sort = false;
                    Sort();
                }

                // -------------------------
                // Write All Log Actions to Log Console
                // -------------------------
                Log.LogWriteAll(this, vm);

                // -------------------------
                // Create output.log
                // -------------------------
                Log.CreateOutputLog(this, vm);

                // -------------------------
                // Clear Global Variables before each Run
                // -------------------------
                //ClearGlobalVariables(vm);
                GC.Collect();
            }
            //else
            //{
            //    //debug
            //    //MessageBox.Show("Not Ready");

            //    // Log Console Message /////////
            //    Log.WriteAction = () =>
            //    {
            //        Log.logParagraph.Inlines.Add(new LineBreak());
            //        Log.logParagraph.Inlines.Add(new LineBreak());
            //        Log.logParagraph.Inlines.Add(new Run("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~") { Foreground = Log.ConsoleAction });
            //    };
            //    Log.LogActions.Add(Log.WriteAction);


            //    /// <summary>
            //    ///    Write All Log Actions to Console
            //    /// </summary> 
            //    Log.LogWriteAll(this, vm);


            //    // -------------------------
            //    // Clear Variables for next Run
            //    // -------------------------
            //    ClearVariables(vm);
            //    GC.Collect();

            //}
        }


    }
}
