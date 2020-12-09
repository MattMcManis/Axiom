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

using Axiom.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ViewModel;
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
        // Axiom Current Version
        public static Version currentVersion { get; set; }
        // Axiom GitHub Latest Version
        public static Version latestVersion { get; set; }
        // Alpha, Beta, Stable
        public static string currentBuildPhase = "alpha";
        public static string latestBuildPhase { get; set; }
        public static string[] splitVersionBuildPhase { get; set; }

        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Global Variables
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // MainWindow
        //public static double minWidth = 824;
        //public static double minHeight = 464;

        // System
        public readonly static string appRootDir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + @"\"; // Axiom.exe directory

        public readonly static string commonProgramFilesDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles).TrimEnd('\\') + @"\";
        public readonly static string commonProgramFilesX86Dir = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86).TrimEnd('\\') + @"\";
        public readonly static string programFilesDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).TrimEnd('\\') + @"\";
        public readonly static string programFilesX86Dir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86).TrimEnd('\\') + @"\";
        public readonly static string programFilesX64Dir = @"C:\Program Files\";

        public readonly static string programDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).TrimEnd('\\') + @"\";
        public readonly static string appDataLocalDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).TrimEnd('\\') + @"\";
        public readonly static string appDataRoamingDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).TrimEnd('\\') + @"\";
        public readonly static string tempDir = Path.GetTempPath(); // Windows AppData Temp Directory

        public readonly static string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).TrimEnd('\\') + @"\";
        public readonly static string documentsDir = userProfile + @"Documents\"; // C:\Users\Example\Documents\
        public readonly static string videosDir = userProfile + @"Videos\"; // C:\Users\Example\Videos\
        public readonly static string downloadDir = userProfile + @"Downloads\"; // C:\Users\Example\Downloads\

        public readonly static string confAppRootPath = appRootDir + "axiom.conf";
        public readonly static string confAppDataLocalPath = appDataLocalDir + @"Axiom UI\axiom.conf";
        public readonly static string confAppDataRoamingPath = appDataRoamingDir + @"Axiom UI\axiom.conf";

        public readonly static string logAppRootPath = appRootDir + "axiom.log";
        public readonly static string logAppDataLocalPath = appDataLocalDir + @"Axiom UI\axiom.log";
        public readonly static string logAppDataRoamingPath = appDataRoamingDir + @"Axiom UI\axiom.log";

        // Programs
        public static string youtubedl { get; set; } // youtube-dl.exe

        // Input
        public static string inputPreviousPath { get; set; }
        public static string inputDir { get; set; } // Input File Directory
        public static string inputFileName { get; set; } // (eg. myvideo.mp4 = myvideo)
        public static string inputExt { get; set; } // (eg. .mp4)
        public static string input { get; set; } // Single: Input Path + Filename No Ext + Input Ext (Browse Text Box) /// Batch: Input Path (Browse Text Box)

        // Output
        public static string outputPreviousPath { get; set; }
        public static string outputDir { get; set; } // Output Path
        public static string outputFileName { get; set; } // Output Directory + Filename (No Extension)
        public static string outputFileName_Original { get; set; } // Output Directory + Filename (No Extension) // Backup Original
        public static string outputFileName_Tokens { get; set; } // Output Directory + Filename (No Extension) + Settings
        public static string outputExt { get; set; } // (eg. .webm)
        public static string output { get; set; } // Single: outputDir + outputFileName + outputExt /// Batch: outputDir + %~nf
        public static string outputNewFileName { get; set; } // File Rename if File already exists

        // Batch
        public static string batchInputAuto { get; set; }

        // Volume Up Down
        // Used for Volume Up Down buttons. Integer += 1 for each tick of the timer.
        // Timer Tick in MainWindow Initialize
        public DispatcherTimer dispatcherTimerUp = new DispatcherTimer(DispatcherPriority.Render);
        public DispatcherTimer dispatcherTimerDown = new DispatcherTimer(DispatcherPriority.Render);



        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Other Windows
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Log Console
        /// </summary>
        public LogConsole logconsole = new LogConsole();

        /// <summary>
        /// Debug Console
        /// </summary>
        public static DebugConsole debugconsole;

        /// <summary>
        /// File Properties Console
        /// </summary>
        public FilePropertiesWindow filepropwindow;

        /// <summary>
        /// Crop Window
        /// </summary>
        public static CropWindow cropwindow;

        /// <summary>
        /// Optimize Advanced Window
        /// </summary>
        public static InfoWindow infowindow;

        /// <summary>
        /// Update Window
        /// </summary>
        public static UpdateWindow updatewindow;



        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Main Window Initialize
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        public MainWindow()
        {
            InitializeComponent();

            // -----------------------------------------------------------------
            /// <summary>
            /// Window & Components
            /// </summary>
            // -----------------------------------------------------------------
            //base.Closing += this.Window_Closing;

            // Set Min/Max Width/Height to prevent Tablets maximizing
            //MinWidth = MainWindow.minWidth;
            //MinHeight = MainWindow.minHeight;
            MinWidth = VM.MainView.Window_Width;
            MinHeight = VM.MainView.Window_Height;

            // -------------------------
            // Set Current Version to Assembly Version
            // -------------------------
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string assemblyVersion = fvi.FileVersion;
            currentVersion = new Version(assemblyVersion);

            // -------------------------
            // Start the Log Console (Hidden)
            // -------------------------
            StartLogConsole();

            // -------------------------
            // Title + Version
            // -------------------------
            VM.MainView.TitleVersion = "Axiom ~ FFmpeg UI (" + Convert.ToString(currentVersion) + "-" + currentBuildPhase + ")";

            // -------------------------
            // Tool Tips
            // -------------------------
            // Longer Display Time
            ToolTipService.ShowDurationProperty.OverrideMetadata(typeof(DependencyObject), 
                                                                 new FrameworkPropertyMetadata(Int32.MaxValue));

            // -------------------------
            // Log Text Theme SelectiveColorPreview
            // -------------------------
            switch (VM.ConfigureView.Theme_SelectedItem)
            {
                case "Axiom":
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8")); // Actions
                    break;

                case "FFmpeg":
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cb85c")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cb85c")); // Actions
                    break;

                case "Cyberpunk":
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9f3ed2")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9380fd")); // Actions
                    break;

                case "Onyx":
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#777777")); // Actions
                    break;

                case "Circuit":
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ad8a4a")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2ebf93")); // Actions
                    break;

                case "Prelude":
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#777777")); // Actions
                    break;

                case "System":
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8")); // Actions
                    break;
            }

            // -----------------------------------------------------------------
            // Log Console Message ///////// 
            //-----------------------------------------------------------------
            logconsole.rtbLog.Document = new FlowDocument(Log.logParagraph); //start
            logconsole.rtbLog.BeginChange(); //begin change


            Log.logParagraph.Inlines.Add(new Bold(new Run(VM.MainView.TitleVersion)) { Foreground = Log.ConsoleTitle });


            /// <summary>
            /// System Info
            /// </summary>
            // Shows OS and Hardware information in Log Console
            SystemInfo();
            //Task<int> task = SystemInfoDisplay();


            // -----------------------------------------------------------------
            /// <summary>
            /// Load Saved Settings
            /// </summary>
            // -----------------------------------------------------------------  
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Loading Saved Settings...")) { Foreground = Log.ConsoleAction });

            // -------------------------
            // Prevent Loading Corrupt App.Config
            // -------------------------
            try
            {
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            }
            catch (ConfigurationErrorsException ex)
            {
                string filename = ex.Filename;

                if (File.Exists(filename) == true)
                {
                    File.Delete(filename);
                    Settings.Default.Upgrade();
                }
                else
                {

                }
            }

            // -------------------------
            // Volume Up/Down Button Timer Tick
            // Dispatcher Tick
            // In Intializer to prevent Tick from doubling up every MouseDown
            // -------------------------
            dispatcherTimerUp.Tick += new EventHandler(dispatcherTimerUp_Tick);
            dispatcherTimerDown.Tick += new EventHandler(dispatcherTimerDown_Tick);

            // --------------------------
            // Input/Output Copy/Paste
            // --------------------------
            DataObject.AddPastingHandler(tbxOutput, OnOutputTextBoxPaste);

            // --------------------------
            // ScriptView Copy/Paste
            // --------------------------
            //DataObject.AddCopyingHandler(tbxScriptView, new DataObjectCopyingEventHandler(OnScriptCopy));
            //DataObject.AddPastingHandler(tbxScriptView, new DataObjectPastingEventHandler(OnScriptPaste));


            // --------------------------------------------------
            // Import Axiom Config axiom.conf
            // --------------------------------------------------
            // -------------------------
            // AppData Local Directory
            // -------------------------
            if (File.Exists(appDataLocalDir + @"Axiom UI\axiom.conf"))
            {
                // Make changes for Program Exit
                // If Axiom finds axiom.conf in the App Directory
                // Change the Configure Directory variable to it 
                // so that it saves changes to that path on program exit
                Controls.Configure.configDir = appDataLocalDir + @"Axiom UI\";
                Controls.Configure.configFile = Path.Combine(appDataLocalDir + @"Axiom UI\", "axiom.conf");

                // Import Config
                Controls.Configure.ImportConfig(this, Controls.Configure.configFile);
                VM.ConfigureView.ConfigPath_SelectedItem = "AppData Local";

                // Change Log Directory to App Root Directory
                Log.logDir = appDataLocalDir + @"Axiom UI\";
                VM.ConfigureView.LogPath_Text = Log.logDir;

                // These changes will be seen in Axiom's Settings Tab

                // Log Console Message /////////
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Config Location: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(Controls.Configure.configFile) { Foreground = Log.ConsoleDefault });
            }

            // -------------------------
            // AppData Roaming Directory
            // -------------------------
            else if (File.Exists(appDataRoamingDir + @"Axiom UI\axiom.conf"))
            {
                // Make changes for Program Exit
                // If Axiom finds axiom.conf in the App Directory
                // Change the Configure Directory variable to it 
                // so that it saves changes to that path on program exit
                Controls.Configure.configDir = appDataRoamingDir + @"Axiom UI\";
                Controls.Configure.configFile = Path.Combine(appDataRoamingDir + @"Axiom UI\", "axiom.conf");

                // Import Config
                Controls.Configure.ImportConfig(this, Controls.Configure.configFile);
                VM.ConfigureView.ConfigPath_SelectedItem = "AppData Roaming";

                // Change Log Directory to App Root Directory
                Log.logDir = appDataRoamingDir + @"Axiom UI\";
                VM.ConfigureView.LogPath_Text = Log.logDir;

                // These changes will be seen in Axiom's Settings Tab

                // Log Console Message /////////
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Config Location: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(Controls.Configure.configFile) { Foreground = Log.ConsoleDefault });
            }

            // -------------------------
            // App Root Directory
            // -------------------------
            else if(File.Exists(Path.Combine(appRootDir, "axiom.conf")))
            {
                // Make changes for Program Exit
                // If Axiom finds axiom.conf in the App Directory
                // Change the Configure Directory variable to it 
                // so that it saves changes to that path on program exit
                Controls.Configure.configDir = appRootDir;
                Controls.Configure.configFile = Path.Combine(appRootDir, "axiom.conf");

                // Import Config
                Controls.Configure.ImportConfig(this, Controls.Configure.configFile);
                VM.ConfigureView.ConfigPath_SelectedItem = "App Root";

                // Change Log Directory to App Root Directory
                Log.logDir = appRootDir;
                VM.ConfigureView.LogPath_Text = Log.logDir;

                // These changes will be seen in Axiom's Settings Tab

                // Log Console Message /////////
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Config Location: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(Controls.Configure.configFile) { Foreground = Log.ConsoleDefault });
            }

            // -------------------------
            // Missing, Load Defaults
            // -------------------------
            else
            {
                VM.ConfigureView.LoadConfigDefaults();
                VM.MainView.LoadControlsDefaults();
                VM.FormatView.LoadControlsDefaults();
                VM.VideoView.LoadControlsDefaults();
                VM.SubtitleView.LoadControlsDefaults();
                VM.AudioView.LoadControlsDefaults();

                // Log Console Message /////////
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Run("Cannot Locate Config file axiom.conf. Loading defaults.") { Foreground = Log.ConsoleDefault });
            }

            // -------------------------
            // Window Position
            // -------------------------
            // Center on first run, before first axiom.conf has been created
            if (this.Top == 0 && this.Left == 0)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            // --------------------------------------------------
            // Log Console Messages
            // --------------------------------------------------
            // -------------------------
            // Load FFmpeg.exe Path
            // -------------------------
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("FFmpeg: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(VM.ConfigureView.FFmpegPath_Text) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load FFprobe.exe Path
            // -------------------------
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("FFprobe: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(VM.ConfigureView.FFprobePath_Text) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load FFplay.exe Path
            // -------------------------
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("FFplay: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(VM.ConfigureView.FFplayPath_Text) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load youtube-dl.exe Path
            // -------------------------
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("youtube-dl: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(VM.ConfigureView.youtubedlPath_Text) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Shell
            // -------------------------
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Shell: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(VM.ConfigureView.Shell_SelectedItem) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load Log Enabled
            // -------------------------
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Log Enabled: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(Convert.ToString(VM.ConfigureView.LogCheckBox_IsChecked.ToString())) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load Log Path
            // -------------------------
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Log Path: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(VM.ConfigureView.LogPath_Text) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load Threads
            // -------------------------
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Using CPU Threads: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(VM.ConfigureView.Threads_SelectedItem) { Foreground = Log.ConsoleDefault });

            // -----------------------------------------------------------------
            // end change !important
            // -----------------------------------------------------------------
            logconsole.rtbLog.EndChange();
        }



        /// <summary>
        /// Window Loaded
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = this;

            // --------------------------------------------------
            // Event Handlers
            // --------------------------------------------------
            // Attach SelectionChanged Handlers
            // Prevent Bound ComboBox from firing SelectionChanged Event at application startup
            // Format
            //cboFormat_Container.SelectionChanged += cboFormat_Container_SelectionChanged;

            // -------------------------
            // Format Controls
            // -------------------------
            //Controls.Format.Controls.FormatControls(VM.FormatView.Format_Container_SelectedItem);

            // -------------------------
            // Control Defaults
            // -------------------------
            lstvSubtitles.SelectionMode = SelectionMode.Single;

            // -------------------------
            // Check for Available Updates
            // -------------------------
            Task<int> task = UpdateAvailableCheck();

            // -------------------------
            // Load Custom Presets
            // -------------------------
            Profiles.Profiles.LoadCustomPresets();
        }


        /// <summary>
        /// On Closed
        /// </summary>
        //protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        //{
        //    // Force Exit All Executables
        //    base.OnClosed(e);
        //    System.Windows.Forms.Application.ExitThread();
        //    Application.Current.Shutdown();
        //}

        /// <summary>
        /// Window Closing
        /// </summary>
        public void Window_Closing(object sender, CancelEventArgs e)
        {
            // -------------------------
            // Export axiom.conf
            // -------------------------
            switch (VM.ConfigureView.ConfigPath_SelectedItem)
            {
                // -------------------------
                // AppData Local Directory
                // -------------------------
                case "AppData Local":
                    // Change the conf output folder path
                    Controls.Configure.configDir = appDataLocalDir + @"Axiom UI\";

                    try
                    {
                        // -------------------------
                        // Create Axiom UI folder if missing
                        // -------------------------
                        if (!Directory.Exists(Controls.Configure.configDir))
                        {
                            Directory.CreateDirectory(Controls.Configure.configDir);
                        }

                        // -------------------------
                        // Move the conf to AppData Local
                        // -------------------------
                        // Move from App Directory to AppData Local
                        if (File.Exists(confAppRootPath))
                        {
                            // Delete before Move instead of Overwrite
                            if (File.Exists(confAppDataLocalPath))
                            {
                                File.Delete(confAppDataLocalPath);
                            }

                            File.Move(confAppRootPath, confAppDataLocalPath);
                        }
                        // Move from AppData Roaming to AppData Local
                        else if (File.Exists(confAppDataRoamingPath))
                        {
                            // Delete before Move instead of Overwrite
                            if (File.Exists(confAppDataLocalPath))
                            {
                                File.Delete(confAppDataLocalPath);
                            }

                            File.Move(confAppDataRoamingPath, confAppDataLocalPath);
                        }

                        // -------------------------
                        // Move the log to AppData Local
                        // -------------------------
                        // Move from App Directory to AppData Local
                        if (File.Exists(logAppRootPath))
                        {
                            // Delete before Move instead of Overwrite
                            if (File.Exists(logAppDataLocalPath))
                            {
                                File.Delete(logAppDataLocalPath);
                            }

                            File.Move(logAppRootPath, logAppDataLocalPath);
                        }
                        // Move from AppData Roaming to AppData Local
                        else if (File.Exists(logAppDataRoamingPath))
                        {
                            // Delete before Move instead of Overwrite
                            if (File.Exists(logAppDataLocalPath))
                            {
                                File.Delete(logAppDataLocalPath);
                            }

                            File.Move(logAppDataRoamingPath, logAppDataLocalPath);
                        }

                        // -------------------------
                        // Save Config
                        // -------------------------
                        ExportWriteConfig(Controls.Configure.configDir);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString(),
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                    break;

                // -------------------------
                // AppData Roaming Directory
                // -------------------------
                case "AppData Roaming":
                    // Change the conf output folder path
                    Controls.Configure.configDir = appDataRoamingDir + @"Axiom UI\";

                    try
                    {
                        // -------------------------
                        // Create Axiom UI folder if missing
                        // -------------------------
                        if (!Directory.Exists(Controls.Configure.configDir))
                        {
                            Directory.CreateDirectory(Controls.Configure.configDir);
                        }

                        // -------------------------
                        // Move the conf to AppData Roaming
                        // -------------------------
                        // Move from App Root Directory to AppData Roaming
                        if (File.Exists(confAppRootPath))
                        {
                            // Delete before Move instead of Overwrite
                            if (File.Exists(confAppDataRoamingPath))
                            {
                                File.Delete(confAppDataRoamingPath);
                            }

                            File.Move(confAppRootPath, confAppDataRoamingPath);
                        }
                        // Move from AppData Local to AppData Roaming
                        else if (File.Exists(confAppDataLocalPath))
                        {
                            // Delete before Move instead of Overwrite
                            if (File.Exists(confAppDataRoamingPath))
                            {
                                File.Delete(confAppDataRoamingPath);
                            }

                            File.Move(confAppDataLocalPath, confAppDataRoamingPath);
                        }

                        // -------------------------
                        // Move the log to AppData Roaming
                        // -------------------------
                        // Move from App Root Directory to AppData Roaming
                        if (File.Exists(logAppRootPath))
                        {
                            // Delete before Move instead of Overwrite
                            if (File.Exists(logAppDataRoamingPath))
                            {
                                File.Delete(logAppDataRoamingPath);
                            }

                            File.Move(logAppRootPath, logAppDataRoamingPath);
                        }
                        // Move from AppData Local to AppData Roaming
                        else if (File.Exists(logAppDataLocalPath))
                        {
                            // Delete before Move instead of Overwrite
                            if (File.Exists(logAppDataRoamingPath))
                            {
                                File.Delete(logAppDataRoamingPath);
                            }

                            File.Move(logAppDataLocalPath, logAppDataRoamingPath);
                        }

                        // -------------------------
                        // Save Config
                        // -------------------------
                        ExportWriteConfig(Controls.Configure.configDir);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString(),
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                    break;

                // -------------------------
                // App Directory
                // -------------------------
                case "App Root":
                    // Change the conf output folder path
                    Controls.Configure.configDir = appRootDir;

                    // -------------------------
                    // Ignore Program Files
                    // -------------------------
                    if (!Controls.Configure.configDir.Contains(programFilesDir) &&
                        !Controls.Configure.configDir.Contains(programFilesX86Dir) &&
                        !Controls.Configure.configDir.Contains(programFilesX64Dir)
                        )
                    {
                        try
                        {
                            // -------------------------
                            // Create Axiom UI folder if missing
                            // -------------------------
                            if (!Directory.Exists(Controls.Configure.configDir))
                            {
                                Directory.CreateDirectory(Controls.Configure.configDir);
                            }

                            // -------------------------
                            // Move the conf to App Root
                            // -------------------------
                            // Move from AppData Local to App Root Directory
                            if (File.Exists(confAppDataLocalPath))
                            {
                                // Delete before Move instead of Overwrite
                                if (File.Exists(confAppRootPath))
                                {
                                    File.Delete(confAppRootPath);
                                }

                                File.Move(confAppDataLocalPath, confAppRootPath);
                            }
                            // Move from AppData Roaming to App Root Directory
                            else if (File.Exists(confAppDataRoamingPath))
                            {
                                // Delete before Move instead of Overwrite
                                if (File.Exists(confAppRootPath))
                                {
                                    File.Delete(confAppRootPath);
                                }

                                File.Move(confAppDataRoamingPath, confAppRootPath);
                            }

                            // -------------------------
                            // Move the log to App Root
                            // -------------------------
                            // Move from AppData Local to App Root Directory
                            if (File.Exists(logAppDataLocalPath))
                            {
                                // Delete before Move instead of Overwrite
                                if (File.Exists(logAppRootPath))
                                {
                                    File.Delete(logAppRootPath);
                                }

                                File.Move(logAppDataLocalPath, logAppRootPath);
                            }
                            // Move from AppData Roaming to App Root Directory
                            else if (File.Exists(logAppDataRoamingPath))
                            {
                                // Delete before Move instead of Overwrite
                                if (File.Exists(logAppRootPath))
                                {
                                    File.Delete(logAppRootPath);
                                }

                                File.Move(logAppDataRoamingPath, logAppRootPath);
                            }

                            // -------------------------
                            // Save Config
                            // -------------------------
                            ExportWriteConfig(Controls.Configure.configDir);
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show(ex.ToString(),
                                            "Error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);
                        }
                    }

                    // -------------------------
                    // Program Files Write Warning
                    // -------------------------
                    else
                    {
                        if (File.Exists(confAppRootPath) ||
                            File.Exists(logAppRootPath))
                        {
                            MessageBox.Show("Cannot save config/log to Program Files, Axiom does not have Administrator Privileges at this time. \n\nPlease select AppData Local or Roaming instead.",
                                            "Notice",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Warning);
                        }
                    }
                    break;
            }
            
            // Exit
            //e.Cancel = true;
            //System.Windows.Forms.Application.ExitThread();
            //Environment.Exit(0);

            //base.OnClosed(e);
            //System.Windows.Forms.Application.ExitThread();
            Application.Current.Shutdown();
        }


        /// <summary>
        /// Export Write Config (Method)
        /// </summary>
        public void ExportWriteConfig(string path)
        {
            Controls.Configure.INIFile conf = new Controls.Configure.INIFile(path.TrimEnd('\\') + @"\" + "axiom.conf");

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

            //string outputNaming_ItemOrder = string.Join(",", VM.ConfigureView.OutputNaming_ListView_Items.Where(s => !string.IsNullOrWhiteSpace(s)))

            // -------------------------
            // Save only if changes have been made
            // -------------------------
            if (// Main Window
                this.Top != top ||
                this.Left != left ||
                this.Width != width ||
                this.Height != height ||
                ////this.WindowState != windowState ||
                VM.MainView.CMDWindowKeep_IsChecked != settings_CMDWindowKeep_IsChecked ||
                VM.MainView.AutoSortScript_IsChecked != settings_AutoSortScript_IsChecked ||

                // Config
                VM.ConfigureView.FFmpegPath_Text != conf.Read("Settings", "FFmpegPath_Text") ||
                VM.ConfigureView.FFprobePath_Text != conf.Read("Settings", "FFprobePath_Text") ||
                VM.ConfigureView.FFplayPath_Text != conf.Read("Settings", "FFplayPath_Text") ||
                VM.ConfigureView.youtubedlPath_Text != conf.Read("Settings", "youtubedlPath_Text") ||
                VM.ConfigureView.CustomPresetsPath_Text != conf.Read("Settings", "CustomPresetsPath_Text") ||
                VM.ConfigureView.LogPath_Text != conf.Read("Settings", "LogPath_Text") ||
                VM.ConfigureView.LogCheckBox_IsChecked != settings_LogCheckBox_IsChecked ||

                // Process
                VM.ConfigureView.Shell_SelectedItem != conf.Read("Settings", "Shell_SelectedItem") ||
                VM.ConfigureView.ShellTitle_SelectedItem != conf.Read("Settings", "ShellTitle_SelectedItem") ||
                VM.ConfigureView.ProcessPriority_SelectedItem != conf.Read("Settings", "ProcessPriority_SelectedItem") ||
                VM.ConfigureView.Threads_SelectedItem != conf.Read("Settings", "Threads_SelectedItem") ||

                // Input
                VM.ConfigureView.InputFileNameTokens_SelectedItem != conf.Read("Settings", "InputFileNameTokens_SelectedItem") ||
                VM.ConfigureView.InputFileNameTokensCustom_Text != conf.Read("Settings", "InputFileNameTokensCustom_Text") ||

                // Output
                string.Join(",", VM.ConfigureView.OutputNaming_ListView_Items
                      .Where(s => !string.IsNullOrWhiteSpace(s))) != conf.Read("Settings", "OutputNaming_ItemOrder") ||

                string.Join(",", VM.ConfigureView.OutputNaming_ListView_SelectedItems
                      .Where(s => !string.IsNullOrEmpty(s))) != conf.Read("Settings", "OutputNaming_SelectedItems") ||

                VM.ConfigureView.OutputFileNameSpacing_SelectedItem != conf.Read("Settings", "OutputFileNameSpacing_SelectedItem") ||
                VM.ConfigureView.OutputOverwrite_SelectedItem != conf.Read("Settings", "OutputOverwrite_SelectedItem") ||

                // App
                VM.ConfigureView.Theme_SelectedItem != conf.Read("Settings", "Theme_SelectedItem") ||
                VM.ConfigureView.UpdateAutoCheck_IsChecked != settings_UpdateAutoCheck_IsChecked
                )
            {
                // Save Config
                Controls.Configure.ExportConfig(this, path);

                //debug
                //MessageBox.Show("changes made " +
                //                path + " " +
                //                VM.ConfigureView.ShellTitle_SelectedItem + " " +
                //                top.ToString() + " " +
                //                left.ToString() + " " +
                //                width.ToString() + " " +
                //                height.ToString() + " ");
            }
        }



        /// <summary>
        /// Folder Write Access Check (Method)
        /// </summary>
        private bool hasWriteAccessToFolder(string path)
        {
            try
            {
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(path);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        /// <summary>
        /// Path Wrap in Quotes
        /// </summary>
        //public static String WrapWithQuotes(string s)
        //{
        //    // "my string"
        //    return "\"" + s + "\"";
        //}
        /// <summary>
        /// Path Wrap in Quotes
        /// </summary>
        public static String WrapWithQuotes(string s)
        {
            // Shell Check
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // CMD
                case "CMD":
                    // "my string"
                    return "\"" + s + "\"";

                // PowerShell
                case "PowerShell":
                    // Process Priority
                    switch (VM.ConfigureView.ProcessPriority_SelectedItem)
                    {
                        // Default
                        case "Default":
                            // "my string"
                            return "\"" + s + "\"";

                        // All Other Options (escape)
                        default:
                            // `"my string`"
                            return "`\"" + s + "`\"";
                    }

                // Unknown
                default:
                    return s;
            }
        }

        /// <summary>
        /// Path Wrap in Escaped Quotes
        /// </summary>
        //public static String WrapWithEscapedQuotes(string s)
        //{
        //    // \"my string\"
        //    return "\\\"" + s + "\\\"";
        //}


        /// <summary>
        /// PowerShell Escape Quotes
        /// </summary>
        public static String PowerShellEscapeQuotes(string s)
        {
            // `"my string`"
            return Regex.Replace(s, "\"", "`\"");
        }

        /// <summary>
        /// Clear Global Variables (Method)
        /// </summary>
        public static void ClearGlobalVariables()
        {
            // Output
            regexTags = string.Empty;

            // FFprobe
            Analyze.FFprobe.argsVideoCodec = string.Empty;
            Analyze.FFprobe.argsAudioCodec = string.Empty;
            Analyze.FFprobe.argsVideoBitRate = string.Empty;
            Analyze.FFprobe.argsAudioBitRate = string.Empty;
            Analyze.FFprobe.argsSize = string.Empty;
            Analyze.FFprobe.argsDuration = string.Empty;
            Analyze.FFprobe.argsFrameRate = string.Empty;

            Analyze.FFprobe.inputVideoCodec = string.Empty;
            Analyze.FFprobe.inputVideoBitRate = string.Empty;
            Analyze.FFprobe.inputAudioCodec = string.Empty;
            Analyze.FFprobe.inputAudioBitRate = string.Empty;
            Analyze.FFprobe.inputSize = string.Empty;
            Analyze.FFprobe.inputDuration = string.Empty;
            Analyze.FFprobe.inputFrameRate = string.Empty;

            Analyze.FFprobe.vEntryType = string.Empty;
            Analyze.FFprobe.aEntryType = string.Empty;

            // Video
            Generate.Video.Quality.passSingle = string.Empty;
            Generate.Video.Encoding.vEncodeSpeed = string.Empty;
            Generate.Video.Encoding.hwAccelDecode = string.Empty;
            Generate.Video.Encoding.hwAccelTranscode = string.Empty;
            Generate.Video.Codec.vCodec = string.Empty;
            Generate.Video.Quality.vBitMode = string.Empty;
            Generate.Video.Quality.vQuality = string.Empty;
            Generate.Video.Quality.vBitRateNA = string.Empty;
            Generate.Video.Quality.vLossless = string.Empty;
            Generate.Video.Quality.vBitRate = string.Empty;
            Generate.Video.Quality.vMinRate = string.Empty;
            Generate.Video.Quality.vMaxRate = string.Empty;
            Generate.Video.Quality.vBufSize = string.Empty;
            Generate.Video.Quality.vOptions = string.Empty;
            Generate.Video.Quality.vCRF = string.Empty;
            Generate.Video.Quality.pix_fmt = string.Empty;
            Generate.Video.Color.colorPrimaries = string.Empty;
            Generate.Video.Color.colorTransferCharacteristics = string.Empty;
            Generate.Video.Color.colorSpace = string.Empty;
            Generate.Video.Color.colorRange = string.Empty;
            Generate.Video.Color.colorMatrix = string.Empty;
            Generate.Video.Size.vAspectRatio = string.Empty;
            Generate.Video.Size.vScalingAlgorithm = string.Empty;
            Generate.Video.Video.fps = string.Empty;
            Generate.Video.Video.vsync = string.Empty;
            Generate.Video.Quality.optTune = string.Empty;
            Generate.Video.Quality.optProfile = string.Empty;
            Generate.Video.Quality.optLevel = string.Empty;
            Generate.Video.Quality.optFlags = string.Empty;
            Generate.Video.Size.width = string.Empty;
            Generate.Video.Size.height = string.Empty;

            if (Generate.Video.Params.vParamsList != null &&
                Generate.Video.Params.vParamsList.Count > 0)
            {
                Generate.Video.Params.vParamsList.Clear();
                Generate.Video.Params.vParamsList.TrimExcess();
            }

            Generate.Video.Params.vParams = string.Empty;

            // Clear Crop if ClearCrop Button Identifier is Empty
            if (VM.VideoView.Video_CropClear_Text == "Clear")
            {
                CropWindow.crop = string.Empty;
                CropWindow.divisibleCropWidth = null; //int
                CropWindow.divisibleCropHeight = null; //int
            }

            //Format.trim = string.Empty;
            Generate.Format.trimStart = string.Empty;
            Generate.Format.trimEnd = string.Empty;

            Filters.Video.vFilter = string.Empty;
            Filters.Video.geq = string.Empty;

            if (Filters.Video.vFiltersList != null && 
                Filters.Video.vFiltersList.Count > 0)
            {
                Filters.Video.vFiltersList.Clear();
                Filters.Video.vFiltersList.TrimExcess();
            }

            Generate.Video.Quality.v2PassArgs = string.Empty;
            Generate.Video.Quality.pass1Args = string.Empty;
            Generate.Video.Quality.pass2Args = string.Empty;
            Generate.Video.Quality.pass1 = string.Empty;
            Generate.Video.Quality.pass2 = string.Empty;
            Generate.Video.Video.image = string.Empty;
            Generate.Video.Quality.optimize = string.Empty;

            // Subtitle
            Generate.Subtitle.sCodec = string.Empty;
            Generate.Subtitle.subtitles = string.Empty;

            // Audio
            Generate.Audio.Codec.aCodec = string.Empty;
            Generate.Audio.Channels.aChannel = string.Empty;
            Generate.Audio.Quality.aBitMode = string.Empty;
            Generate.Audio.Quality.aBitRate = string.Empty;
            Generate.Audio.Quality.aBitRateNA = string.Empty;
            Generate.Audio.Quality.aQuality = string.Empty;
            Generate.Audio.Quality.aCompressionLevel = string.Empty;
            Generate.Audio.Quality.aSamplerate = string.Empty;
            Generate.Audio.Quality.aBitDepth = string.Empty;
            Filters.Audio.aFilter = string.Empty;
            Filters.Audio.aVolume = string.Empty;
            Filters.Audio.aHardLimiter = string.Empty;

            if (Filters.Audio.aFiltersList != null &&
                Filters.Audio.aFiltersList.Count > 0)
            {
                Filters.Audio.aFiltersList.Clear();
                Filters.Audio.aFiltersList.TrimExcess();
            }

            // Batch
            Analyze.FFprobe.batchFFprobeAuto = string.Empty;
            Generate.Video.Quality.batchVideoAuto = string.Empty;
            Generate.Audio.Quality.batchAudioAuto = string.Empty;

            // Streams
            Generate.Streams.vMap = string.Empty;
            Generate.Streams.cMap = string.Empty;
            Generate.Streams.sMap = string.Empty;
            Generate.Streams.aMap = string.Empty;
            Generate.Streams.mMap = string.Empty;

            // Do not Empty:
            //
            //inputDir
            //inputFileName
            //inputExt
            //input
            //outputDir
            //outputFileName
            //outputFileName_Original
            //outputFileName_Tokens
            //outputNewFileName
            //FFmpeg.ffmpegArgs
            //FFmpeg.ffmpegArgsSort
            //Video.scale
            //CropWindow.divisibleCropWidth
            //CropWindow.divisibleCropHeight
            //CropWindow.cropWidth
            //CropWindow.cropHeight
            //CropWindow.cropX
            //CropWindow.cropY
            //Streams.map
            //FFmpeg.cmdWindow
        }

        /// <summary>
        /// Remove Linebreaks (Method)
        /// </summary>
        /// <remarks>
        /// Used for Selected Controls FFmpeg Arguments
        /// </remarks>
        public static String RemoveLineBreaks(string lines)
        {
            return lines.Replace(Environment.NewLine, "")
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
        }


        /// <summary>
        /// Replace Linebreaks with Space (Method)
        /// </summary>
        /// <remarks>
        /// Used for Script View Custom Edited Script
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
        /// Deny Special Keys
        /// </summary>
        public void Deny_Special_Keys(KeyEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.LeftShift) && e.Key == Key.D8) || // *
                (Keyboard.IsKeyDown(Key.RightShift) && e.Key == Key.D8) ||  // *

                (Keyboard.IsKeyDown(Key.LeftShift) && e.Key == Key.OemPeriod) ||  // >
                (Keyboard.IsKeyDown(Key.RightShift) && e.Key == Key.OemPeriod) ||  // >

                (Keyboard.IsKeyDown(Key.LeftShift) && e.Key == Key.OemComma) ||  // <
                (Keyboard.IsKeyDown(Key.RightShift) && e.Key == Key.OemComma) ||  // <

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
        /// Deny Backspace Key
        /// </summary>
        public void Deny_Backspace_Key(KeyEventArgs e)
        {
            if (e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Allow Only Numbers & Backspace
        /// </summary>
        public void Allow_Only_Number_Keys(KeyEventArgs e)
        {
            // Only allow Numbers
            // Deny Symbols (Shift + Number)
            if (!(e.Key >= System.Windows.Input.Key.D0 && e.Key <= System.Windows.Input.Key.D9) ||
                (e.Key >= System.Windows.Input.Key.NumPad0 && e.Key <= System.Windows.Input.Key.NumPad9) ||
                e.Key == System.Windows.Input.Key.Back ||
                e.Key == System.Windows.Input.Key.Delete ||
                (Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift) && (e.Key >= System.Windows.Input.Key.D9)) ||
                (Keyboard.IsKeyDown(System.Windows.Input.Key.RightShift) && (e.Key >= System.Windows.Input.Key.D9))
                )
            {
                e.Handled = true;
            }
        }


        /// <summary>
        /// Start Log Console (Method)
        /// </summary>
        public void StartLogConsole()
        {
            // Open LogConsole Window
            logconsole = new LogConsole();
            logconsole.Hide();

            // Position with Show();

            logconsole.rtbLog.Cursor = Cursors.Arrow;
        }


        /// <summary>
        /// Selected Item
        /// </summary>
        // Video
        public static string Video_EncodeSpeed_PreviousItem { get; set; }
        public static string Video_Quality_PreviousItem { get; set; }
        public static string Video_Pass_PreviousItem { get; set; }
        public static string VideoOptimize_PreviousItem { get; set; }
        // Audio
        public static string Audio_Quality_PreviousItem { get; set; }
        public static string Audio_SampleRate_PreviousItem { get; set; }
        public static string Audio_BitDepth_PreviousItem { get; set; }
        // Selected Item
        public static String SelectedItem(List<string> controlItems,
                                          string previousItem
                                          )
        {
            // -------------------------
            // Select the Prevoius Codec's Item if available
            // -------------------------
            if (!string.IsNullOrWhiteSpace(previousItem) &&
                controlItems?.Contains(previousItem) == true)
            {
                //MessageBox.Show(previousItem); //debug
                return previousItem;
            }
            // -------------------------
            // If missing Select Default to First Item
            // -------------------------
            else
            {
                //MessageBox.Show("missing"); //debug
                return controlItems.FirstOrDefault();
            }
        }


        /// <summary>
        /// Is Valid Windows Path
        /// </summary>
        /// <remarks>
        /// Check for Invalid Characters
        /// </remarks>
        public static bool IsValidPath(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
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
        /// Is Valid Windows Filename
        /// </summary>
        /// <remarks>
        /// Check for Invalid Characters
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
        /// FFcheck (Method)
        /// </summary>
        /// <remarks>
        /// Check if FFmpeg and FFprobe is on Computer 
        /// </remarks>
        public static bool FFcheck()
        {
            bool ready = true;

            try
            {
                // Environment Variables
                var envar = Environment.GetEnvironmentVariable("Path"); // Checks both User and System
                //var envar = Environment.GetEnvironmentVariable("PATH");, 
                //var envarUser = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);
                //var envarSystem = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);
                //MessageBox.Show(envar); //debug

                // -------------------------
                // FFmpeg
                // -------------------------
                // If Auto Mode
                if (VM.ConfigureView.FFmpegPath_Text == "<auto>")
                {
                    // Check default current directory
                    if (File.Exists(appRootDir + @"ffmpeg\bin\ffmpeg.exe"))
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
                else if (VM.ConfigureView.FFmpegPath_Text != "<auto>" &&
                         IsValidPath(VM.ConfigureView.FFprobePath_Text))
                {
                    var dirPath = Path.GetDirectoryName(VM.ConfigureView.FFmpegPath_Text).TrimEnd('\\') + @"\";
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
                    //if (string.Equals(VM.ConfigureView.FFmpegPath_Text, fullPath, StringComparison.OrdinalIgnoreCase))
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
                if (VM.ConfigureView.FFprobePath_Text == "<auto>")
                {
                    // Check default current directory
                    if (File.Exists(appRootDir + @"ffmpeg\bin\ffprobe.exe"))
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
                else if (VM.ConfigureView.FFprobePath_Text != "<auto>" &&
                         IsValidPath(VM.ConfigureView.FFprobePath_Text))
                {
                    var dirPath = Path.GetDirectoryName(VM.ConfigureView.FFprobePath_Text).TrimEnd('\\') + @"\";
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
                    //if (string.Equals(VM.ConfigureView.FFprobePath_Text, fullPath, StringComparison.OrdinalIgnoreCase))
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
        /// FFmpeg Path (Method)
        /// </summary>
        public static String FFmpegPath()
        {
            // -------------------------
            // FFmpeg.exe and FFprobe.exe Paths
            // -------------------------
            // If Configure FFmpeg Path is <auto>
            if (VM.ConfigureView.FFmpegPath_Text == "<auto>")
            {
                if (File.Exists(appRootDir + @"ffmpeg\bin\ffmpeg.exe"))
                {
                    // Use included binary
                    // Do not use WrapWithQuotes() Method
                    Generate.FFmpeg.ffmpeg = Sys.Shell.PowerShell_CallOperator_FFmpeg() + "\"" + appRootDir + @"ffmpeg\bin\ffmpeg.exe" + "\"";
                }
                else if (!File.Exists(appRootDir + @"ffmpeg\bin\ffmpeg.exe"))
                {
                    // Use system installed binaries
                    Generate.FFmpeg.ffmpeg = "ffmpeg";
                }
            }
            // Use User Custom Path
            else
            {
                // Do not use WrapWithQuotes() Method
                Generate.FFmpeg.ffmpeg = Sys.Shell.PowerShell_CallOperator_FFmpeg() + "\"" + VM.ConfigureView.FFmpegPath_Text + "\"";
            }

            // Return Value
            return Generate.FFmpeg.ffmpeg;
        }


        /// <summary>
        /// FFprobe Path
        /// </summary>
        public static void FFprobePath()
        {
            // If Configure FFprobe Path is <auto>
            if (VM.ConfigureView.FFprobePath_Text == "<auto>")
            {
                if (File.Exists(appRootDir + @"ffmpeg\bin\ffprobe.exe"))
                {
                    // use included binary
                    Analyze.FFprobe.ffprobe = "\"" + appRootDir + @"ffmpeg\bin\ffprobe.exe" + "\"";
                }
                else if (!File.Exists(appRootDir + @"ffmpeg\bin\ffprobe.exe"))
                {
                    // use system installed binaries
                    Analyze.FFprobe.ffprobe = "ffprobe";
                }
            }
            // Use User Custom Path
            else
            {
                Analyze.FFprobe.ffprobe = "\"" + VM.ConfigureView.FFprobePath_Text + "\"";
            }

            // Return Value
            //return FFprobe.ffprobe;
        }


        /// <summary>
        /// FFplay Path
        /// </summary>
        public static void FFplayPath()
        {
            // If Configure FFprobe Path is <auto>
            if (VM.ConfigureView.FFplayPath_Text == "<auto>")
            {
                if (File.Exists(appRootDir + @"ffmpeg\bin\ffplay.exe"))
                {
                    // use included binary
                    Preview.FFplay.ffplay = "\"" + appRootDir + @"ffmpeg\bin\ffplay.exe" + "\"";
                }
                else if (!File.Exists(appRootDir + @"ffmpeg\bin\ffplay.exe"))
                {
                    // use system installed binaries
                    Preview.FFplay.ffplay = "ffplay";
                }
            }
            // Use User Custom Path
            else
            {
                Preview.FFplay.ffplay = "\"" + VM.ConfigureView.FFplayPath_Text + "\"";
            }

            // Return Value
            //return FFplay.ffplay;
        }


        /// <summary>
        /// youtube-dl Path
        /// </summary>
        public static void youtubedlPath()
        {
            // If Configure youtubedl Path is <auto>
            if (VM.ConfigureView.youtubedlPath_Text == "<auto>")
            {
                // youtube-dl.exe Exists
                if (File.Exists(appRootDir + @"youtube-dl\youtube-dl.exe"))
                {
                    // use included binary path
                    youtubedl = appRootDir + @"youtube-dl\youtube-dl.exe";
                }
                else if (File.Exists(appRootDir + @"youtube-dl.exe"))
                {
                    // moved from folder
                    youtubedl = appRootDir + @"youtube-dl.exe";
                }

                // youtube-dl.exe Does Not Exist
                else if (!File.Exists(appRootDir + @"youtube-dl\youtube-dl.exe"))
                {
                    // Installed
                    // Environment Variable auto path
                    youtubedl = @"youtube-dl";
                }
            }

            // Use User Custom Path
            else
            {
                youtubedl = VM.ConfigureView.youtubedlPath_Text;
            }
        }


        /// <summary>
        /// Is Valid URL
        /// </summary>
        //public static bool IsValidURL(string source)
        //{
        //    Uri uriResult;
        //    return Uri.TryCreate(source, UriKind.Absolute, out uriResult) && (uriResult.Scheme == "http" || uriResult.Scheme == "https");
        //}


        /// <summary>
        /// Is Website URL
        /// </summary>
        public static bool IsWebURL(string input_Text)
        {
            input_Text = input_Text.Trim();

            // Empty
            if (string.IsNullOrWhiteSpace(input_Text))
            {
                return false;
            }

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


        /// <summary>
        /// Is YouTube URL
        /// </summary>
        public static bool IsYouTubeURL(string input_Text)
        {
            // Empty
            if (string.IsNullOrWhiteSpace(input_Text))
            {
                return false;
            }

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

            // Local File
            else
            { 
                return false;
            }
        }


        /// <summary>
        /// YouTube Download-Only Mode Check (Method)
        /// </summary>
        /// <remarks>
        /// If Axiom is in full Codec Copy mode Download the file without converting
        /// </remarks>
        public static bool IsWebDownloadOnly(string videoCodec_SelectedItem,
                                             string subtitleCodec_SelectedItem,
                                             string audioCodec_SelectedItem
                                             )
        {
            if (//IsYouTubeURL(VM.MainView.Input_Text) == true &&
                
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
        /// YouTube Download - FFmpeg Path
        /// </summary>
        public static String YouTubeDL_FFmpegPath()
        {
            // youtube-dl
            // FFmpeg must be detected by youtube-dl to merge video+audio into a single file
            // If using Environment Variables, path will be only 'ffmpeg'
            // If defining ffmpeg location, do not use "--ffmpeg-location ffmpeg", it will fail
            // You must use a full path --ffmpeg-location "C:\Path\To\ffmpeg.exe"

            string path = FFmpegPath();

            // Environment Variables
            if (path == "ffmpeg")
            {
                // Do not specify a path if using Environment Variables
                // It will be detected by youtube-dl automatically
                return string.Empty;
            }

            // Missing
            else if (string.IsNullOrWhiteSpace(path))
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
        /// Ready Halts (Method)
        /// </summary>
        public static bool ReadyHalts()
        {
            // -------------------------
            // Check if FFmpeg & FFprobe Exists
            // -------------------------
            if (FFcheck() == false)
            {
                // Halt
                return false;
            }

            //MessageBox.Show(FFcheck().ToString()); //debug
            //MessageBox.Show(ready.ToString()); //debug

            // -------------------------
            // Input File does not exist
            // -------------------------
            //MessageBox.Show(input); //debug
            if (IsWebURL(VM.MainView.Input_Text) == false) // Ignore Web URL's
            {
                if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text) &&
                    VM.MainView.Batch_IsChecked == false)
                {
                    if (!File.Exists(VM.MainView.Input_Text))
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
            //if (IsYouTubeDownloadOnly() == true)
            //{

            //}

            // -------------------------
            // Do Not allow Auto without FFprobe being installed or linked
            // -------------------------
            if (string.IsNullOrWhiteSpace(Analyze.FFprobe.ffprobe))
            {
                if (VM.VideoView.Video_Quality_SelectedItem == "Auto" ||
                    VM.AudioView.Audio_Quality_SelectedItem == "Auto")
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
            if (!string.IsNullOrWhiteSpace(CropWindow.crop) &&
                VM.VideoView.Video_Codec_SelectedItem == "Copy") //null check
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
            if (VM.VideoView.Video_Quality_SelectedItem == "Custom" &&
                VM.VideoView.Video_BitRate_IsEnabled == true &&
                VM.VideoView.Video_BitRate_Text != "0" && // Constant Bit Rate 0 does not need K or M
                VM.VideoView.Video_VBR_IsChecked == false)
            {
                // Error List
                ICollection<string> errors = new List<string>();

                // Bit Rate
                if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_BitRate_Text))
                {
                    if (VM.VideoView.Video_BitRate_Text.ToUpper()?.Contains("K") == false &&
                        VM.VideoView.Video_BitRate_Text.ToUpper()?.Contains("M") == false)
                    {
                        errors.Add("Bit Rate");
                    }
                }

                // Min Rate
                if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_MinRate_Text))
                {
                    if (VM.VideoView.Video_MinRate_Text.ToUpper()?.Contains("K") == false &&
                        VM.VideoView.Video_MinRate_Text.ToUpper()?.Contains("M") == false)
                    {
                        errors.Add("Min Rate");
                    }
                }

                // Max Rate
                if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_MaxRate_Text))
                {
                    if (VM.VideoView.Video_MaxRate_Text.ToUpper()?.Contains("K") == false &&
                        VM.VideoView.Video_MaxRate_Text.ToUpper()?.Contains("M") == false)
                    {
                        errors.Add("Max Rate");
                    }
                }

                // Buf Size
                if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_BufSize_Text))
                {
                    if (VM.VideoView.Video_BufSize_Text.ToUpper()?.Contains("K") == false &&
                        VM.VideoView.Video_BufSize_Text.ToUpper()?.Contains("M") == false)
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
            if (VM.MainView.Batch_IsChecked == false && 
                VM.MainView.Input_Text.EndsWith("\\"))
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
            if (VM.MainView.Batch_IsChecked == true &&
                !string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
            {
                if (string.Equals(inputDir, outputDir, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
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
            if (VM.VideoView.Video_Codec_SelectedItem == "VP8" ||
                VM.VideoView.Video_Codec_SelectedItem == "VP9")
            {
                if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_CRF_Text) &&
                    string.IsNullOrWhiteSpace(VM.VideoView.Video_BitRate_Text))
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
        /// System Info
        /// </summary>
        public void SystemInfo()
        {
            //int count = 0;
            //await Task.Factory.StartNew(() =>
            //{
            // -----------------------------------------------------------------
            /// <summary>
            /// System Info
            /// </summary>
            /// <remarks>
            /// Detect and Display System Hardware
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
                    Controls.Configure.maxthreads = String.Format("{0}", item["NumberOfLogicalProcessors"]);
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
        /// RAM Type
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
        /// Normalize Value (Method)
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


        /// <summary>
        /// Get Average Value (Method)
        /// <summary>
        //public static double GetAverageValue()
        //{

        //}


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// CONTROLS
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Info Button
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
        /// Website Button
        /// </summary>
        private void btbWebsite_Click(object sender, RoutedEventArgs e)
        {
            // Open Axiom Website URL in Default Browser
            Process.Start("https://axiomui.github.io");

        }

        /// <summary>
        /// Keep Window - Toggle - Checked
        /// </summary>
        private void tglCMDWindowKeep_Checked(object sender, RoutedEventArgs e)
        {
            //// Log Console Message /////////
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new Bold(new Run("Keep FFmpeg Window Toggle: ")) { Foreground = Log.ConsoleDefault });
            //Log.logParagraph.Inlines.Add(new Run("On") { Foreground = Log.ConsoleDefault });
        }
        /// <summary>
        /// Keep Window - Toggle - Unchecked
        /// </summary>
        private void tglCMDWindowKeep_Unchecked(object sender, RoutedEventArgs e)
        {
            //// Log Console Message /////////
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new Bold(new Run("Keep FFmpeg Window Toggle: ")) { Foreground = Log.ConsoleDefault });
            //Log.logParagraph.Inlines.Add(new Run("Off") { Foreground = Log.ConsoleDefault });
        }

        /// <summary>
        /// Auto Sort Script - Toggle - Checked
        /// </summary>
        private void tglAutoSortScript_Checked(object sender, RoutedEventArgs e)
        {
            //// Log Console Message /////////
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new Bold(new Run("Auto Sort Script Toggle: ")) { Foreground = Log.ConsoleDefault });
            //Log.logParagraph.Inlines.Add(new Run("On") { Foreground = Log.ConsoleDefault });
        }
        /// <summary>
        /// Auto Sort Script - Toggle - Unchecked
        /// </summary>
        private void tglAutoSortScript_Unchecked(object sender, RoutedEventArgs e)
        {
            //// Log Console Message /////////
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new Bold(new Run("Auto Sort Script Toggle: ")) { Foreground = Log.ConsoleDefault });
            //Log.logParagraph.Inlines.Add(new Run("Off") { Foreground = Log.ConsoleDefault });
        }

        /// <summary>
        /// Debug Console Window Button
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
                debugconsole = new DebugConsole(this);

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
                //DebugConsole.DebugWrite(debugconsole, MainView.vm);

                // Open Window
                debugconsole.Show();
            }
            // Simplified
            catch
            {
                // Check if Window is already open
                if (IsDebugConsoleOpened) return;

                // Start Window
                debugconsole = new DebugConsole(this);

                // Only allow 1 Window instance
                debugconsole.ContentRendered += delegate { IsDebugConsoleOpened = true; };
                debugconsole.Closed += delegate { IsDebugConsoleOpened = false; };

                // Position Relative to MainWindow
                // Keep from going off screen
                debugconsole.Left = Left - debugconsole.Width - 12;
                debugconsole.Top = Top;

                // Write Variables to Debug Window (Method)
                //DebugConsole.DebugWrite(debugconsole, MainView.vm);

                // Open Window
                debugconsole.Show();
            }
        }

        /// <summary>
        /// Log Console Window Button
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
        /// Log Button
        /// </summary>
        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            // Call Method to get Log Path
            Log.DefineLogPath();

            //MessageBox.Show(Configure.logPath.ToString()); //debug

            // Open Log
            if (File.Exists(VM.ConfigureView.LogPath_Text + "output.log"))
            {
                Process.Start("notepad.exe", "\"" + VM.ConfigureView.LogPath_Text + "output.log" + "\"");
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
        /// CMD Button
        /// </summary>
        private void btnCmd_Click(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Launch Shell
            // -------------------------
            // Default to User Profile Diretory
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // CMD
                case "CMD":
                    Process.Start("CMD.exe", "/k cd %userprofile%");
                    break;

                // PowerShell
                case "PowerShell":
                    Process.Start("PowerShell.exe", "-NoExit cd $home");
                    break;
            }
        }

        /// <summary>
        /// File Properties Button
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
                filepropwindow = new FilePropertiesWindow(this);

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
                filepropwindow = new FilePropertiesWindow(this);

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
        /// Play File Button
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
        /// Extension Match Load Auto
        /// </summary>
        /// <remarks>
        /// Change the Controls to Auto if Input Extension matches Output Extsion
        /// This will trigger Auto Codec Copy
        /// </remarks>
        public void ExtensionMatchLoadAutoValues()
        {
            //MessageBox.Show(inputExt + " " + outputExt); //debug
            //MessageBox.Show(VM.VideoView.Video_Quality_SelectedItem); //debug

            // -------------------------
            // Get Input/Output Extensions
            // -------------------------
            string inputExt = Path.GetExtension(VM.MainView.Input_Text);
            string outputExt = "." + VM.FormatView.Format_Container_SelectedItem;
            //MessageBox.Show(inputExt + "\n" + outputExt); //debug

            // Extensions Match Check
            if (string.IsNullOrWhiteSpace(inputExt) ||
                string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
            {
                // -------------------------
                // Video
                // -------------------------
                if (VM.VideoView.Video_Quality_SelectedItem == "Auto")
                {
                    // Set Controls:

                    // Main
                    // Pixel Format Auto uses PixelFormatControls()
                    VM.VideoView.Video_FPS_SelectedItem = "auto";
                    VM.VideoView.Video_Optimize_SelectedItem = "None";
                    VM.VideoView.Video_Scale_SelectedItem = "Source";
                    VM.VideoView.Video_ScalingAlgorithm_SelectedItem = "auto";

                    // Color
                    VM.VideoView.Video_Color_Range_SelectedItem = "auto";
                    VM.VideoView.Video_Color_Space_SelectedItem = "auto";
                    VM.VideoView.Video_Color_Primaries_SelectedItem = "auto";
                    VM.VideoView.Video_Color_TransferCharacteristics_SelectedItem = "auto";
                    VM.VideoView.Video_Color_Matrix_SelectedItem = "auto";

                    // Filters
                    VM.FilterVideoView.LoadFilterVideoDefaults();
                    VM.FilterAudioView.LoadFilterAudioDefaults();
                }

                // -------------------------
                // Audio
                // -------------------------
                if (VM.AudioView.Audio_Quality_SelectedItem == "Auto")
                {
                    // Set Controls:

                    // Main
                    //VM.AudioView.Audio_Quality_SelectedItem = "Auto";
                    VM.AudioView.Audio_Channel_SelectedItem = "Source";
                    VM.AudioView.Audio_SampleRate_SelectedItem = "auto";
                    VM.AudioView.Audio_BitDepth_SelectedItem = "auto";

                    // Filters
                    VM.AudioView.Audio_Volume_Text = "100";
                    VM.AudioView.Audio_HardLimiter_Value = 0.0;
                    VM.FilterAudioView.LoadFilterAudioDefaults();
                }
            }
        }


        /// <summary>
        /// Script View Drag and Drop
        /// </summary>
        private void tbxScriptView_PreviewDragOver(object sender, DragEventArgs e)
        {
            try
            {
                e.Handled = true;
                e.Effects = DragDropEffects.Copy;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(),
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void tbxScriptView_PreviewDrop(object sender, DragEventArgs e)
        {
            try
            {
                var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];

                string file = buffer.First();
                string ext = Path.GetExtension(file);

                // Only accept txt files
                if (ext == ".txt")
                {
                    VM.MainView.ScriptView_Text = File.ReadAllText(file/*buffer.First()*/);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(),
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

    }
}
