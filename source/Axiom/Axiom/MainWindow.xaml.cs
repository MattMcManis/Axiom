/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2019 Matt McManis
http://github.com/MattMcManis/Axiom
http://axiomui.github.io
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

        public string TitleVersion
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Variables
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // Locks
        public static bool ready = true; // If 1 allow conversion, else stop
        public static bool script = false; // If 0 run ffmpeg, if 1 run generate script
        public static bool ffCheckCleared = false; // If 1, FFcheck no longer has to run for each convert

        // System
        public static string appDir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + @"\"; // Axiom.exe directory

        // Input
        public static string inputDir; // Input File Directory
        public static string inputFileName; // (eg. myvideo.mp4 = myvideo)
        public static string inputExt; // (eg. .mp4)
        public static string input; // Single: Input Path + Filename No Ext + Input Ext (Browse Text Box) /// Batch: Input Path (Browse Text Box)
        public static string youtubedl; // YouTube Download

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
            MinWidth = 768;
            MinHeight = 432;

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
            TitleVersion = "Axiom ~ FFmpeg UI (" + Convert.ToString(currentVersion) + "-" + currentBuildPhase + ")";

            // -------------------------
            // Load Theme
            // -------------------------
            // --------------------------
            // First time use
            // --------------------------
            try
            {
                if (string.IsNullOrEmpty(Settings.Default.Theme.ToString()))
                {
                    Configure.theme = "Axiom";

                    // Set ComboBox if Configure Window is Open
                    vm.Theme_SelectedItem = "Axiom";

                    // Save Theme for next launch
                    Settings.Default.Theme = Configure.theme;
                    Settings.Default.Save();

                    // Change Theme Resource
                    App.Current.Resources.MergedDictionaries.Clear();
                    App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                    {
                        Source = new Uri("Themes/" + "Theme" + Configure.theme + ".xaml", UriKind.RelativeOrAbsolute)
                    });
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Settings.Default.Theme.ToString())) // null check
                {
                    Configure.theme = Settings.Default.Theme.ToString();

                    // Set ComboBox
                    vm.Theme_SelectedItem = Configure.theme;

                    // Change Theme Resource
                    App.Current.Resources.MergedDictionaries.Clear();
                    App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                    {
                        Source = new Uri("Themes/" + "Theme" + Configure.theme + ".xaml", UriKind.RelativeOrAbsolute)
                    });
                }


                // -------------------------
                // Log Text Theme SelectiveColorPreview
                // -------------------------
                if (Configure.theme == "Axiom")
                {
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8")); // Actions
                }
                else if (Configure.theme == "FFmpeg")
                {
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cb85c")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cb85c")); // Actions
                }
                else if (Configure.theme == "Cyberpunk")
                {
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9f3ed2")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9380fd")); // Actions
                }
                else if (Configure.theme == "Onyx")
                {
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#777777")); // Actions
                }
                else if (Configure.theme == "Circuit")
                {
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ad8a4a")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2ebf93")); // Actions
                }
                else if (Configure.theme == "Prelude")
                {
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#777777")); // Actions
                }
                else if (Configure.theme == "System")
                {
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8")); // Actions
                }
            }
            catch
            {
                MessageBox.Show("Problem loading Theme.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }


            // Log Console Message /////////
            logconsole.rtbLog.Document = new FlowDocument(Log.logParagraph); //start
            logconsole.rtbLog.BeginChange(); //begin change

            Log.logParagraph.Inlines.Add(new Bold(new Run(TitleVersion)) { Foreground = Log.ConsoleTitle });

            //Log.LogConsoleMessageAdd(TitleVersion,      // Message
            //                         "bold",            // Emphasis
            //                         Log.ConsoleAction, // Color
            //                         0);                // Linebreaks

            /// <summary>
            ///     System Info
            /// </summary>
            // Shows OS and Hardware information in Log Console
            SystemInfoDisplay();


            // -----------------------------------------------------------------
            /// <summary>
            ///     Load Saved Settings
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
            // Window Position
            // -------------------------
            if (Convert.ToDouble(Settings.Default.Left) == 0
                && Convert.ToDouble(Settings.Default.Top) == 0)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            // Load Saved
            else
            {
                Top = Settings.Default.Top;
                Left = Settings.Default.Left;
                Height = Settings.Default.Height;
                Width = Settings.Default.Width;

                if (Settings.Default.Maximized)
                {
                    WindowState = WindowState.Maximized;
                }
            }

            // -------------------------
            // Load FFmpeg.exe Path
            // -------------------------
            Configure.LoadFFmpegPath(vm);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("FFmpeg: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(Configure.ffmpegPath) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load FFprobe.exe Path
            // -------------------------
            Configure.LoadFFprobePath(vm);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("FFprobe: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(Configure.ffprobePath) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load FFplay.exe Path
            // -------------------------
            Configure.LoadFFplayPath(vm);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("FFplay: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(Configure.ffplayPath) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load Log Enabled
            // -------------------------
            Configure.LoadLogCheckbox(vm);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Log Enabled: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(Convert.ToString(vm.LogCheckBox_IsChecked.ToString())) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load Log Path
            // -------------------------
            Configure.LoadLogPath(vm);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Log Path: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(Configure.logPath) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load Threads
            // -------------------------
            Configure.LoadThreads(vm);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Using CPU Threads: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(Configure.threads) { Foreground = Log.ConsoleDefault });


            //end change !important
            logconsole.rtbLog.EndChange();


            // -------------------------
            // Load CMD Keep Window Toggle
            // -------------------------
            // Log Checkbox     
            // Safeguard Against Corrupt Saved Settings
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                //tglCMDWindowKeep.IsChecked = Convert.ToBoolean(Settings.Default.CMDWindowKeep);
                vm.CMDWindowKeep_IsChecked = Convert.ToBoolean(Settings.Default.CMDWindowKeep);
            }
            catch
            {

            }

            // -------------------------
            // Load Auto Sort Script Toggle
            // -------------------------
            // Log Checkbox     
            // Safeguard Against Corrupt Saved Settings
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                //tglAutoSortScript.IsChecked = Convert.ToBoolean(Settings.Default.AutoSortScript);
                vm.AutoSortScript_IsChecked = Convert.ToBoolean(Settings.Default.AutoSortScript);
            }
            catch
            {

            }


            // -------------------------
            // Load Updates Auto Check
            // -------------------------
            // Log Checkbox     
            // Safeguard Against Corrupt Saved Settings
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                //tglUpdateAutoCheck.IsChecked = Convert.ToBoolean(Settings.Default.UpdateAutoCheck);
                vm.UpdateAutoCheck_IsChecked = Convert.ToBoolean(Settings.Default.UpdateAutoCheck);
            }
            catch
            {

            }

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
            DataObject.AddCopyingHandler(rtbScriptView, new DataObjectCopyingEventHandler(OnScriptCopy));
            DataObject.AddPastingHandler(rtbScriptView, new DataObjectPastingEventHandler(OnScriptPaste));
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
            listViewSubtitles.SelectionMode = SelectionMode.Single;

            //// Format
            //vm.Format_SelectedIndex = 0;
            //cboCut.SelectedIndex = 0;
            //cboSpeed_SelectedItem = "Medium";
            //cboHWAccel.SelectedIndex = 0;

            //// Video
            //cboVideoQuality.SelectedIndex = 0;
            //cboFPS.SelectedIndex = 0;
            //cboSize.SelectedIndex = 0;
            //cboOptimize.SelectedIndex = 0;

            //// Audio
            //cboAudioQuality.SelectedIndex = 0;
            //cboChannel.SelectedIndex = 0;
            //cboSamplerate.SelectedIndex = 0;
            //cboBitDepth.SelectedIndex = 0;
            //cboBitDepth.IsEnabled = false;

            // Video Filters
            cboFilterVideo_Deband.SelectedIndex = 0;
            cboFilterVideo_Deshake.SelectedIndex = 0;
            cboFilterVideo_Deflicker.SelectedIndex = 0;
            cboFilterVideo_Dejudder.SelectedIndex = 0;
            cboFilterVideo_Dejudder.SelectedIndex = 0;
            cboFilterVideo_Denoise.SelectedIndex = 0;
            cboFilterVideo_SelectiveColor.SelectedIndex = 0;
            cboFilterVideo_SelectiveColor_Correction_Method.SelectedIndex = 0;

            // Audio Filters
            cboFilterAudio_Lowpass.SelectedIndex = 0;
            cboFilterAudio_Highpass.SelectedIndex = 0;
            cboFilterAudio_Headphones.SelectedIndex = 0;

            // Preset
            //cboPreset.SelectedIndex = 0;

            // Batch Extension Box Disabled
            batchExtensionTextBox.IsEnabled = false;

            // Open Input/Output Location Disabled
            openLocationInput.IsEnabled = false;
            openLocationOutput.IsEnabled = false;


            // -------------------------
            // Load ComboBox Items
            // -------------------------
            // Filter Selective SelectiveColorPreview
            cboFilterVideo_SelectiveColor.ItemsSource = cboSelectiveColor_Items;

            // -------------------------
            // Startup Preset
            // -------------------------
            // Default Format is WebM
            //if ((string)cboFormat_SelectedItem == "webm")
            //{
            //    cboSubtitlesStream_SelectedItem = "none";
            //    cboAudioStream_SelectedItem = "1";
            //}

            // -------------------------
            // Check for Available Updates
            // -------------------------
            Task.Factory.StartNew(() =>
            {
                UpdateAvailableCheck();
            });
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
            if (WindowState == WindowState.Maximized)
            {
                // Use the RestoreBounds as the current values will be 0, 0 and the size of the screen
                Settings.Default.Top = RestoreBounds.Top;
                Settings.Default.Left = RestoreBounds.Left;
                Settings.Default.Height = RestoreBounds.Height;
                Settings.Default.Width = RestoreBounds.Width;
                Settings.Default.Maximized = true;
            }
            else
            {
                Settings.Default.Top = Top;
                Settings.Default.Left = Left;
                Settings.Default.Height = Height;
                Settings.Default.Width = Width;
                Settings.Default.Maximized = false;
            }

            Settings.Default.Save();

            // Exit
            e.Cancel = true;
            System.Windows.Forms.Application.ExitThread();
            Environment.Exit(0);
        }


        /// <summary>
        ///     Clear Variables (Method)
        /// </summary>
        public static void ClearVariables(ViewModel vm)
        {
            // FFmpeg
            //FFmpeg.cmdWindow = string.Empty;

            // FFprobe
            FFprobe.argsVideoCodec = string.Empty;
            FFprobe.argsAudioCodec = string.Empty;
            FFprobe.argsVideoBitrate = string.Empty;
            FFprobe.argsAudioBitrate = string.Empty;
            FFprobe.argsSize = string.Empty;
            FFprobe.argsDuration = string.Empty;
            FFprobe.argsFrameRate = string.Empty;

            FFprobe.inputVideoCodec = string.Empty;
            FFprobe.inputVideoBitrate = string.Empty;
            FFprobe.inputAudioCodec = string.Empty;
            FFprobe.inputAudioBitrate = string.Empty;
            FFprobe.inputSize = string.Empty;
            FFprobe.inputDuration = string.Empty;
            FFprobe.inputFrameRate = string.Empty;

            FFprobe.vEntryType = string.Empty;
            FFprobe.aEntryType = string.Empty;

            // Video
            Video.passSingle = string.Empty;
            Video.vEncodeSpeed = string.Empty;
            Video.vCodec = string.Empty;
            Video.vBitMode = string.Empty;
            Video.vQuality = string.Empty;
            Video.vBitrateNA = string.Empty;
            Video.vLossless = string.Empty;
            Video.vBitrate = string.Empty;
            Video.vMinrate = string.Empty;
            Video.vMaxrate = string.Empty;
            Video.vBufsize = string.Empty;
            Video.vOptions = string.Empty;
            Video.vCRF = string.Empty;
            Video.pix_fmt = string.Empty;
            Video.vScaling = string.Empty;
            Video.fps = string.Empty;
            Video.optTune = string.Empty;
            Video.optProfile = string.Empty;
            Video.optLevel = string.Empty;
            Video.optFlags = string.Empty;
            Video.width = string.Empty;
            Video.height = string.Empty;
            //Video.size = string.Empty;

            if (Video.x265paramsList != null &&
                Video.x265paramsList.Count > 0)
            {
                Video.x265paramsList.Clear();
                Video.x265paramsList.TrimExcess();
            }

            Video.x265params = string.Empty;

            // Clear Crop if ClearCrop Button Identifier is Empty
            if (vm.CropClear_Text == "Clear")
            {
                CropWindow.crop = string.Empty;
                CropWindow.divisibleCropWidth = null; //int
                CropWindow.divisibleCropHeight = null; //int
            }

            Format.trim = string.Empty;
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
            Video.vEncodeSpeed = string.Empty;
            Video.hwacceleration = string.Empty;

            // Subtitle
            Subtitle.sCodec = string.Empty;
            Subtitle.subtitles = string.Empty;

            // Audio
            Audio.aCodec = string.Empty;
            Audio.aBitMode = string.Empty;
            Audio.aBitrate = string.Empty;
            Audio.aBitrateNA = string.Empty;
            Audio.aQuality = string.Empty;
            Audio.aChannel = string.Empty;
            Audio.aSamplerate = string.Empty;
            Audio.aBitDepth = string.Empty;
            Audio.aBitrateLimiter = string.Empty;
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
            Audio.aBitrateLimiter = string.Empty;

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
        public static string VideoEncodeSpeed_PreviousItem;
        public static string VideoQuality_PreviousItem;
        public static string Pass_PreviousItem;
        public static string VideoOptimize_PreviousItem;
        public static string AudioQuality_PreviousItem;
        public static string AudioSampleRate_PreviousItem;
        public static string AudioBitDepth_PreviousItem;
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
        // FFmpeg Path - Textbox
        // --------------------------------------------------
        private void tbxFFmpegPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Configure.FFmpegFolderBrowser(vm);
        }


        // --------------------------------------------------
        // FFmpeg Auto Path - Button
        // --------------------------------------------------
        private void btnFFmpegAuto_Click(object sender, RoutedEventArgs e)
        {
            // Set the ffmpegPath string
            Configure.ffmpegPath = "<auto>";

            // Display Folder Path in Textbox
            vm.FFmpegPath_Text = "<auto>";

            // FFmpeg Path path for next launch
            Settings.Default.FFmpegPath = "<auto>";
            Settings.Default.Save();
            Settings.Default.Reload();
        }


        // --------------------------------------------------
        // FFprobe Path - Textbox
        // --------------------------------------------------
        private void tbxFFprobePath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Configure.FFprobeFolderBrowser(vm);
        }


        // --------------------------------------------------
        // FFprobe Auto Path - Button
        // --------------------------------------------------
        private void btnFFprobeAuto_Click(object sender, RoutedEventArgs e)
        {
            // Set the ffprobePath string
            Configure.ffprobePath = "<auto>"; //<auto>

            // Display Folder Path in Textbox
            vm.FFprobePath_Text = "<auto>";

            // Save 7-zip Path path for next launch
            Settings.Default.FFprobePath = "<auto>";
            Settings.Default.Save();
            Settings.Default.Reload();
        }


        // --------------------------------------------------
        // FFplay Path - Textbox
        // --------------------------------------------------
        private void tbxFFplayPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Configure.FFplayFolderBrowser(vm);
        }


        // --------------------------------------------------
        // FFplay Auto Path - Button
        // --------------------------------------------------
        private void btnFFplayAuto_Click(object sender, RoutedEventArgs e)
        {
            // Set the ffplayPath string
            Configure.ffplayPath = "<auto>"; //<auto>

            // Display Folder Path in Textbox
            vm.FFplayPath_Text = "<auto>";

            // Save 7-zip Path path for next launch
            Settings.Default.FFplayPath = "<auto>";
            Settings.Default.Save();
            Settings.Default.Reload();
        }

        // --------------------------------------------------
        // Log Checkbox - Checked
        // --------------------------------------------------
        private void cbxLog_Checked(object sender, RoutedEventArgs e)
        {
            // Enable the Log
            //Configure.logEnable = true;
            //vm.LogCheckBox_IsChecked = true;

            // Prevent Loading Corrupt App.Config
            try
            {
                Settings.Default.Log_IsChecked = vm.LogCheckBox_IsChecked;
                Settings.Default.Save();
            }
            catch
            {

            }

            //try
            //{
            //    // must be done this way or you get "convert object to bool error"
            //    if (vm.LogCheckBox_IsChecked == true)
            //    {
            //        // Save Checkbox Settings
            //        Settings.Default.Log_IsChecked = true;
            //        Settings.Default.Save();
            //        Settings.Default.Reload();

            //        // Save Log Enable Settings
            //        Settings.Default.Log_IsEnabled = true;
            //        Settings.Default.Save();
            //        Settings.Default.Reload();
            //    }
            //    if (vm.LogCheckBox_IsChecked == false)
            //    {
            //        // Save Checkbox Settings
            //        Settings.Default.Log_IsChecked = false;
            //        Settings.Default.Save();
            //        Settings.Default.Reload();

            //        // Save Log Enable Settings
            //        Settings.Default.Log_IsEnabled = false;
            //        Settings.Default.Save();
            //        Settings.Default.Reload();
            //    }
            //}
            //catch (ConfigurationErrorsException ex)
            //{
            //    // Delete Old App.Config
            //    string filename = ex.Filename;

            //    if (File.Exists(filename) == true)
            //    {
            //        File.Delete(filename);
            //        Properties.Settings.Default.Upgrade();
            //        // Properties.Settings.Default.Reload();
            //    }
            //    else
            //    {

            //    }
            //}

        }


        // --------------------------------------------------
        // Log Checkbox - Unchecked
        // --------------------------------------------------
        private void cbxLog_Unchecked(object sender, RoutedEventArgs e)
        {
            // Prevent Loading Corrupt App.Config
            try
            {
                Settings.Default.Log_IsChecked = vm.LogCheckBox_IsChecked;
                Settings.Default.Save();
            }
            catch
            {

            }

            //// Disable the Log
            //Configure.logEnable = false;

            //// -------------------------
            //// Prevent Loading Corrupt App.Config
            //// -------------------------
            //try
            //{
            //    // must be done this way or you get "convert object to bool error"
            //    if (vm.LogCheckBox_IsChecked == true)
            //    {
            //        // Save Checkbox Settings
            //        Settings.Default.Log_IsChecked = true;
            //        Settings.Default.Save();
            //        Settings.Default.Reload();

            //        // Save Log Enable Settings
            //        Settings.Default.Log_IsEnabled = true;
            //        Settings.Default.Save();
            //        Settings.Default.Reload();
            //    }
            //    if (vm.LogCheckBox_IsChecked == false)
            //    {
            //        // Save Checkbox Settings
            //        Settings.Default.Log_IsChecked = false;
            //        Settings.Default.Save();
            //        Settings.Default.Reload();

            //        // Save Log Enable Settings
            //        Settings.Default.Log_IsEnabled = false;
            //        Settings.Default.Save();
            //        Settings.Default.Reload();
            //    }
            //}
            //catch (ConfigurationErrorsException ex)
            //{
            //    // Delete Old App.Config
            //    string filename = ex.Filename;

            //    if (File.Exists(filename) == true)
            //    {
            //        File.Delete(filename);
            //        Settings.Default.Upgrade();
            //        // Properties.Settings.Default.Reload();
            //    }
            //    else
            //    {

            //    }
            //}
        }


        // --------------------------------------------------
        // Log Path - Textbox
        // --------------------------------------------------
        private void tbxLog_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Configure.logFolderBrowser(vm);
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
            Configure.logPath = string.Empty;

            // Save Log Path path for next launch
            Settings.Default.LogPath = string.Empty;
            Settings.Default.Save();
            Settings.Default.Reload();
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
            Settings.Default.Threads = vm.Threads_SelectedItem;
            Settings.Default.Save();
            Settings.Default.Reload();
        }
        // --------------------------------------------------
        // Thread Select ComboBox - Allow Only Numbers
        // --------------------------------------------------
        private void threadSelect_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers or Backspace
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
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
            Settings.Default.Theme = vm.Theme_SelectedItem;
            Settings.Default.Save();
            Settings.Default.Reload();
        }


        /// <summary>
        ///    Updates Auto Check - Checked
        /// </summary>
        private void tglUpdateAutoCheck_Checked(object sender, RoutedEventArgs e)
        {
            // Update Toggle Text
            tblkUpdatesAutoCheck.Text = "On";

            // Prevent Loading Corrupt App.Config
            try
            {
                Settings.Default.UpdateAutoCheck = vm.UpdateAutoCheck_IsChecked;
                Settings.Default.Save();
            }
            catch
            {

            }
        }
        /// <summary>
        ///    Updates Auto Check - Unchecked
        /// </summary>
        private void tglUpdateAutoCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            // Update Toggle Text
            tblkUpdatesAutoCheck.Text = "Off";

            // Prevent Loading Corrupt App.Config
            try
            {
                Settings.Default.UpdateAutoCheck = vm.UpdateAutoCheck_IsChecked;
                Settings.Default.Save();
            }
            catch
            {

            }
        }


        // --------------------------------------------------
        // Reset Saved Settings - Button
        // --------------------------------------------------
        private void btnClearAllSavedSettings_Click(object sender, RoutedEventArgs e)
        {
            // Revert FFmpeg
            vm.FFmpegPath_Text = "<auto>";
            Configure.ffmpegPath = vm.FFmpegPath_Text;

            // Revert FFprobe
            vm.FFprobePath_Text = "<auto>";
            Configure.ffprobePath = vm.FFprobePath_Text;

            // Revert FFplay
            vm.FFplayPath_Text = "<auto>";
            Configure.ffprobePath = vm.FFplayPath_Text;

            // Revert Log
            vm.LogCheckBox_IsChecked = false;
            vm.LogPath_Text = string.Empty;
            Configure.logPath = string.Empty;

            // Revert Threads
            vm.Threads_SelectedItem = "optimal";
            Configure.threads = string.Empty;


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
            string userProfile = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%");
            string appDataPath = "\\AppData\\Local\\Axiom";

            // Check if Directory Exists
            if (Directory.Exists(userProfile + appDataPath))
            {
                // Show Yes No Window
                System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(
                    "Delete " + userProfile + appDataPath, "Delete Directory Confirm", System.Windows.Forms.MessageBoxButtons.YesNo);
                // Yes
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    // Delete leftover 2 Pass Logs in Program's folder and Input Files folder
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
                    }
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
        ///    FFcheck (Method)
        /// </summary>
        /// <remarks>
        ///     Check if FFmpeg and FFprobe is on Computer 
        /// </remarks>
        public static void FFcheck()
        {
            try
            {
                // Environment Variables
                var envar = Environment.GetEnvironmentVariable("PATH");

                //MessageBox.Show(envar); //debug

                // -------------------------
                // FFmpeg
                // -------------------------
                // If Auto Mode
                if (Configure.ffmpegPath == "<auto>")
                {
                    // Check default current directory
                    if (File.Exists(appDir + "ffmpeg\\bin\\ffmpeg.exe"))
                    {
                        // let pass
                        ffCheckCleared = true;
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
                            ffCheckCleared = true;
                        }
                        else
                        {
                            /* lock */
                            ready = false;
                            ffCheckCleared = false;
                            MessageBox.Show("Cannot locate FFmpeg Path in Environment Variables or Current Folder.",
                                            "Error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Warning);
                        }

                    }
                }
                // If User Defined Path
                else if (Configure.ffmpegPath != "<auto>" && !string.IsNullOrEmpty(Configure.ffprobePath))
                {
                    var dirPath = Path.GetDirectoryName(Configure.ffmpegPath).TrimEnd('\\') + @"\";
                    var fullPath = Path.Combine(dirPath, "ffmpeg.exe");

                    // Make Sure ffmpeg.exe Exists
                    if (File.Exists(fullPath))
                    {
                        // let pass
                        ffCheckCleared = true;
                    }
                    else
                    {
                        /* lock */
                        ready = false;
                        ffCheckCleared = false;
                        MessageBox.Show("Cannot locate FFmpeg Path in User Defined Path.",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                    }

                    // If Configure Path is ffmpeg.exe and not another Program
                    if (string.Equals(Configure.ffmpegPath, fullPath, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // let pass
                        ffCheckCleared = true;
                    }
                    else
                    {
                        /* lock */
                        ready = false;
                        ffCheckCleared = false;
                        MessageBox.Show("FFmpeg Path must link to ffmpeg.exe.",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                    }
                }

                // -------------------------
                // FFprobe
                // -------------------------
                // If Auto Mode
                if (Configure.ffprobePath == "<auto>")
                {
                    // Check default current directory
                    if (File.Exists(appDir + "ffmpeg\\bin\\ffprobe.exe"))
                    {
                        // let pass
                        ffCheckCleared = true;
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
                            ffCheckCleared = true;
                        }
                        else
                        {
                            /* lock */
                            ready = false;
                            ffCheckCleared = false;
                            MessageBox.Show("Cannot locate FFprobe Path in Environment Variables or Current Folder.",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                        }

                    }
                }
                // If User Defined Path
                else if (Configure.ffprobePath != "<auto>" && !string.IsNullOrEmpty(Configure.ffprobePath))
                {
                    var dirPath = Path.GetDirectoryName(Configure.ffprobePath).TrimEnd('\\') + @"\";
                    var fullPath = Path.Combine(dirPath, "ffprobe.exe");

                    // Make Sure ffprobe.exe Exists
                    if (File.Exists(fullPath))
                    {
                        // let pass
                        ffCheckCleared = true;
                    }
                    else
                    {
                        /* lock */
                        ready = false;
                        ffCheckCleared = false;
                        MessageBox.Show("Cannot locate FFprobe Path in User Defined Path.",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                    }

                    // If Configure Path is FFmpeg.exe and not another Program
                    if (string.Equals(Configure.ffprobePath, fullPath, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // let pass
                        ffCheckCleared = true;
                    }
                    else
                    {
                        /* lock */
                        ready = false;
                        ffCheckCleared = false;
                        MessageBox.Show("Error: FFprobe Path must link to ffprobe.exe.",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Unknown Error trying to locate FFmpeg or FFprobe.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }


        /// <summary>
        ///    FFmpeg Path (Method)
        /// </summary>
        public static String FFmpegPath()
        {
            // -------------------------
            // FFmpeg.exe and FFprobe.exe Paths
            // -------------------------
            // If Configure FFmpeg Path is <auto>
            if (Configure.ffmpegPath == "<auto>")
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
                FFmpeg.ffmpeg = "\"" + Configure.ffmpegPath + "\"";
            }

            // Return Value
            return FFmpeg.ffmpeg;
        }


        /// <remarks>
        ///     FFprobe Path
        /// </remarks>
        public static void FFprobePath()
        {
            // If Configure FFprobe Path is <auto>
            if (Configure.ffprobePath == "<auto>")
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
                FFprobe.ffprobe = "\"" + Configure.ffprobePath + "\"";
            }

            // Return Value
            //return FFprobe.ffprobe;
        }


        /// <remarks>
        ///     FFplay Path
        /// </remarks>
        public static void FFplayPath()
        {
            // If Configure FFprobe Path is <auto>
            if (Configure.ffplayPath == "<auto>")
            {
                if (File.Exists(appDir + "ffmpeg\\bin\\ffplay.exe"))
                {
                    // use included binary
                    FFplay.ffplay = "\"" + appDir + "ffmpeg\\bin\\ffplay.exe" + "\"";
                }
                else if (!File.Exists(appDir + "ffmpeg\\bin\\ffplay.exe"))
                {
                    // use system installed binaries
                    FFplay.ffplay = "ffplay";
                }
            }
            // Use User Custom Path
            else
            {
                FFplay.ffplay = "\"" + Configure.ffplayPath + "\"";
            }

            // Return Value
            //return FFplay.ffplay;
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
            else if (vm.Threads_SelectedItem == "optimal"
                || string.IsNullOrEmpty(Configure.threads))
            {
                Configure.threads = "-threads 0";
            }

            // -------------------------
            // All
            // -------------------------
            else if (vm.Threads_SelectedItem == "all"
                || string.IsNullOrEmpty(Configure.threads))
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
                                                              MessageBoxImage.Warning);

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
        public static void ReadyHalts(ViewModel vm)
        {
            // -------------------------
            // Check if FFmpeg & FFprobe Exists
            // -------------------------
            if (ffCheckCleared == false)
            {
                MainWindow.FFcheck();
            }

            // -------------------------
            // Do not allow Auto without FFprobe being installed or linked
            // -------------------------
            if (string.IsNullOrEmpty(FFprobe.ffprobe))
            {
                if (vm.VideoQuality_SelectedItem == "Auto" ||
                    vm.AudioQuality_SelectedItem == "Auto")
                {
                    // Log Console Message /////////
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Auto Quality Mode Requires FFprobe in order to Detect File Info.")) { Foreground = Log.ConsoleWarning });

                    /* lock */
                    ready = false;
                    MessageBox.Show("Auto Quality Mode Requires FFprobe in order to Detect File Info.",
                                    "Notice",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);
                }
            }

            // -------------------------
            // Do not allow Script to generate if Browse Empty & Auto, since there is no file to detect bitrates/codecs
            // -------------------------
            if (vm.Batch_IsChecked == false) // Ignore if Batch
            {
                if (string.IsNullOrEmpty(vm.Input_Text)) // empty check
                {
                    // -------------------------
                    // Both Video & Audio are Auto Quality
                    // Combined Single Warning
                    // -------------------------
                    if (vm.VideoQuality_SelectedItem == "Auto" && 
                        vm.AudioQuality_SelectedItem == "Auto" && 
                        vm.VideoCodec_SelectedItem != "Copy" && 
                        vm.AudioCodec_SelectedItem != "Copy"
                        )
                    {
                        // Log Console Message /////////
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new LineBreak());
                        Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Video & Audio Quality require an input file in order to detect bitrate settings.")) { Foreground = Log.ConsoleWarning });

                        /* lock */
                        ready = false;
                        script = false;
                        // Warning
                        MessageBox.Show("Video & Audio Auto Quality require an input file in order to detect bitrate settings.",
                                        "Notice",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                    }

                    // -------------------------
                    // Either Video & Audio are Auto Quality
                    // Warning for each
                    // -------------------------
                    else
                    {
                        // -------------------------
                        // Video Auto Quality
                        // -------------------------
                        if (vm.VideoQuality_SelectedItem == "Auto")
                        {
                            if (vm.VideoCodec_SelectedItem != "Copy")
                            {
                                // Log Console Message /////////
                                Log.logParagraph.Inlines.Add(new LineBreak());
                                Log.logParagraph.Inlines.Add(new LineBreak());
                                Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Video Auto Quality requires an input file in order to detect bitrate settings.")) { Foreground = Log.ConsoleWarning });

                                /* lock */
                                ready = false;
                                script = false;
                                // Warning
                                MessageBox.Show("Video Auto Quality requires an input file in order to detect bitrate settings.",
                                                "Notice",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Information);
                            }
                        }

                        // -------------------------
                        // Audio Auto Quality
                        // -------------------------
                        if (vm.AudioQuality_SelectedItem == "Auto")
                        {
                            if (vm.AudioCodec_SelectedItem != "Copy")
                            {
                                // Log Console Message /////////
                                Log.logParagraph.Inlines.Add(new LineBreak());
                                Log.logParagraph.Inlines.Add(new LineBreak());
                                Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Audio Auto Quality requires an input file in order to detect bitrate settings.")) { Foreground = Log.ConsoleWarning });

                                /* lock */
                                ready = false;
                                script = false;
                                // Warning
                                MessageBox.Show("Audio Auto Quality requires an input file in order to detect bitrate settings.",
                                                "Notice",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Information);
                            }
                        }
                    }
                }
            }

            // -------------------------
            // Halt if Single File Input with no Extension
            // -------------------------
            if (vm.Batch_IsChecked == false && 
                vm.Input_Text.EndsWith("\\"))
            {
                // Log Console Message /////////
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Please choose an input file.")) { Foreground = Log.ConsoleWarning });

                /* lock */
                ready = false;
                // Warning
                MessageBox.Show("Please choose an input file.",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
            }

            // -------------------------
            // Do not allow Batch Copy to same folder if file extensions are the same (to avoid file overwrite)
            // -------------------------
            if (vm.Batch_IsChecked == true)
            {
                if (string.Equals(inputDir, outputDir, StringComparison.CurrentCultureIgnoreCase) &&
                    string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
                {
                    //MessageBox.Show(inputDir); //debug
                    //MessageBox.Show(outputDir); //debug

                    // Log Console Message /////////
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Please choose an output folder different than the input folder to avoid file overwrite.")) { Foreground = Log.ConsoleWarning });

                    /* lock */
                    ready = false;
                    // Warning
                    MessageBox.Show("Please choose an output folder different than the input folder to avoid file overwrite.",
                                    "Notice",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);
                }
            }

            // -------------------------
            // Throw Error if VP8/VP9 & CRF does not have Bitrate -b:v
            // -------------------------
            if (vm.VideoCodec_SelectedItem == "VP8"
                || vm.VideoCodec_SelectedItem == "VP9")
            {
                if (!string.IsNullOrEmpty(vm.CRF_Text)
                    && string.IsNullOrEmpty(vm.CRF_Text))
                {
                    // Log Console Message /////////
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: VP8/VP9 CRF must also have Bitrate. \n(e.g. 0 for Constant, 1234k for Constrained)")) { Foreground = Log.ConsoleWarning });

                    /* lock */
                    ready = false;
                    // Notice
                    MessageBox.Show("VP8/VP9 CRF must also have Bitrate. \n(e.g. 0 for Constant, 1234k for Constrained)",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
                }
            }
        }


        /// <summary>
        ///     System Info
        /// </summary>
        public void SystemInfoDisplay()
        {
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

            /// <summary>
            /// OS
            /// </summary>
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


            /// <summary>
            /// CPU
            /// </summary>
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


            /// <summary>
            /// GPU
            /// </summary>
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


            /// <summary>
            /// RAM
            /// </summary>
            try
            {
                Log.logParagraph.Inlines.Add(new Run("RAM ") { Foreground = Log.ConsoleDefault });

                double capacity = 0;
                int memtype = 0;
                string type;
                int speed = 0;

                ManagementObjectSearcher ram = new ManagementObjectSearcher("root\\CIMV2", "SELECT Capacity, MemoryType, Speed FROM Win32_PhysicalMemory");

                foreach (ManagementObject obj in ram.Get())
                {
                    capacity += Convert.ToDouble(obj["Capacity"]);
                    memtype = Int32.Parse(obj.GetPropertyValue("MemoryType").ToString());
                    speed = Int32.Parse(obj.GetPropertyValue("Speed").ToString());
                }

                capacity *= 0.000000001; // Convert Byte to GB
                capacity = Math.Round(capacity, 3); // Round to 3 decimal places

                // Select RAM Type
                switch (memtype)
                {
                    case 20:
                        type = "DDR";
                        break;
                    case 21:
                        type = "DDR2";
                        break;
                    case 17:
                        type = "SDRAM";
                        break;
                    default:
                        if (memtype == 0 || memtype > 22)
                            type = "DDR3";
                        else
                            type = "Unknown";
                        break;
                }

                // Log Console Message /////////
                Log.logParagraph.Inlines.Add(new Run(Convert.ToString(capacity) + "GB " + type + " " + Convert.ToString(speed) + "MHz") { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new LineBreak());

                ram.Dispose();
            }
            catch
            {

            }

            // End System Info
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
        private void buttonInfo_Click(object sender, RoutedEventArgs e)
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
        private void buttonWebsite_Click(object sender, RoutedEventArgs e)
        {
            // Open Axiom Website URL in Default Browser
            Process.Start("https://axiomui.github.io");

        }

        /// <summary>
        ///    Update Button
        /// </summary>
        private Boolean IsUpdateWindowOpened = false;
        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
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
                                                             MessageBoxButton.YesNo);
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
        public void UpdateAvailableCheck()
        {
            //if (tglUpdatesAutoCheck.IsChecked == true)
            if (tglUpdateAutoCheck.Dispatcher.Invoke((() => { return tglUpdateAutoCheck.IsChecked; })) == true)
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

                        Dispatcher.Invoke(new Action(delegate
                        {
                            TitleVersion = "Axiom ~ FFmpeg UI (" + Convert.ToString(currentVersion) + "-" + currentBuildPhase + ")"
                                            + " ~ Update Available: " + "(" + Convert.ToString(latestVersion) + "-" + latestBuildPhase + ")";
                        }));
                    }
                    // Update Not Available
                    else if (latestVersion <= currentVersion)
                    {
                        return;
                    }
                }
            }
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

            // Prevent Loading Corrupt App.Config
            try
            {
                Settings.Default.CMDWindowKeep = vm.CMDWindowKeep_IsChecked;
                Settings.Default.Save();
            }
            catch
            {

            }
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

            // Prevent Loading Corrupt App.Config
            try
            {
                Settings.Default.CMDWindowKeep = vm.CMDWindowKeep_IsChecked;
                Settings.Default.Save();
            }
            catch
            {

            }
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

            // Prevent Loading Corrupt App.Config
            try
            {
                Settings.Default.AutoSortScript = vm.AutoSortScript_IsChecked;
                Settings.Default.Save();
            }
            catch
            {

            }
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

            // Prevent Loading Corrupt App.Config
            try
            {
                Settings.Default.AutoSortScript = vm.AutoSortScript_IsChecked;
                Settings.Default.Save();
            }
            catch
            {

            }
        }

        /// <summary>
        ///     Debug Console Window Button
        /// </summary>
        private Boolean IsDebugConsoleOpened = false;
        private void buttonDebugConsole_Click(object sender, RoutedEventArgs e)
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
                DebugConsole.DebugWrite(debugconsole, vm/*, this*/);

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
                DebugConsole.DebugWrite(debugconsole, vm/*, this*/);

                // Open Window
                debugconsole.Show();
            }
        }

        /// <summary>
        ///     Log Console Window Button
        /// </summary>
        private void buttonLogConsole_Click(object sender, RoutedEventArgs e)
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
        private void buttonLog_Click(object sender, RoutedEventArgs e)
        {
            // Call Method to get Log Path
            Log.DefineLogPath(vm);

            //MessageBox.Show(Configure.logPath.ToString()); //debug

            // Open Log
            if (File.Exists(Configure.logPath + "output.log"))
            {
                Process.Start("notepad.exe", "\"" + Configure.logPath + "output.log" + "\"");
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
        private void buttonCmd_Click(object sender, RoutedEventArgs e)
        {
            // launch command prompt
            Process.Start("CMD.exe", "/k cd %userprofile%");

        }

        /// <summary>
        ///     File Properties Button
        /// </summary>
        private Boolean IsFilePropertiesOpened = false;
        private void buttonProperties_Click(object sender, RoutedEventArgs e)
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
        private void buttonPlayFile_Click(object sender, RoutedEventArgs e)
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
                    inputDir = Path.GetDirectoryName(vm.Input_Text.TrimEnd('\\') + @"\");
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
            //if (!vm.Input_Text.Contains("www.youtube.com")
            //    && !vm.Input_Text.Contains("youtube.com"))
            //{
            if (!string.IsNullOrEmpty(vm.Input_Text))
            {
                // Remove stray slash if closed out early (duplicate code?)
                if (vm.Input_Text == "\\")
                {
                    vm.Input_Text = string.Empty;
                }

                // Get input file extension
                inputExt = Path.GetExtension(vm.Input_Text);


                // Enable / Disable "Open Input Location" Buttion
                if (!string.IsNullOrEmpty(vm.Input_Text))
                {
                    bool exists = Directory.Exists(Path.GetDirectoryName(vm.Input_Text));

                    if (exists)
                    {
                        openLocationInput.IsEnabled = true;
                    }
                    else
                    {
                        openLocationInput.IsEnabled = false;
                    }
                }

                // Set Video & Audio Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
                VideoControls.AutoCopyVideoCodec(vm);
                SubtitleControls.AutoCopySubtitleCodec(vm);
                AudioControls.AutoCopyAudioCodec(vm);
            }
            //}
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
                // Get Output Ext
                FormatControls.OutputFormatExt(vm);


                // Open 'Save File'
                Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();


                // 'Save File' Default Path same as Input Directory
                //
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


                // Show Dialog Box
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
                    openLocationOutput.IsEnabled = true;
                }
                else
                {
                    openLocationOutput.IsEnabled = false;
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
            // Add period to Batch Extension if User did not enter one
            if (!string.IsNullOrEmpty(vm.BatchExtension_Text))
            {
                if (vm.BatchExtension_Text != "extension" &&
                    vm.BatchExtension_Text != "." &&
                    !vm.BatchExtension_Text.StartsWith(".")
                    )
                {
                    //batchExt = "." + vm.BatchExtension_Text;
                    inputExt = "." + vm.BatchExtension_Text;
                }
            }
            else
            {
                //batchExt = string.Empty;
                inputExt = string.Empty;
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
                //batchExt = string.Empty;
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
                //batchExt = string.Empty;
                inputExt = string.Empty;
            }
            // TextBox Value
            else
            {
                //batchExt = vm.BatchExtension_Text;
                inputExt = vm.BatchExtension_Text;
            }

            // Add period to batchExt if user did not enter (This helps enable Copy)
            if (!string.IsNullOrEmpty(vm.BatchExtension_Text) &&
                //!batchExt.StartsWith(".") &&
                !inputExt.StartsWith(".") &&
                vm.BatchExtension_Text != "extension")
            {
                //batchExt = "." + batchExt;
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
        public static String InputPath(ViewModel vm)
        {
            // -------------------------
            // Single File
            // -------------------------
            if (vm.Batch_IsChecked == false)
            {
                // Input Directory
                if (!string.IsNullOrEmpty(vm.Input_Text))
                {
                    inputDir = Path.GetDirectoryName(vm.Input_Text).TrimEnd('\\') + @"\"; // (eg. C:\Input Folder\)
                    inputFileName = Path.GetFileNameWithoutExtension(vm.Input_Text);
                    inputExt = Path.GetExtension(vm.Input_Text);
                }

                // Input
                input = vm.Input_Text; // (eg. C:\Input Folder\file.wmv)
            }

            // -------------------------
            // Batch
            // -------------------------
            else if (vm.Batch_IsChecked == true)
            {
                // Add slash to Batch Browse Text folder path if missing
                vm.Input_Text = vm.Input_Text.TrimEnd('\\') + @"\";

                inputDir = vm.Input_Text; // (eg. C:\Input Folder\)

                inputFileName = "%~f";

                // Input
                input = inputDir + inputFileName; // (eg. C:\Input Folder\)
            }

            // -------------------------
            // Empty
            // -------------------------
            // Input Textbox & Output Textbox Both Empty
            if (string.IsNullOrEmpty(vm.Input_Text))
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
                inputDir = vm.Input_Text; // (eg. C:\Input Folder\)
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

            // -------------------------
            // Single File
            // -------------------------
            if (vm.Batch_IsChecked == false)
            {
                // Input Not Empty, Output Empty
                // Default Output to be same as Input Directory
                if (!string.IsNullOrEmpty(vm.Input_Text) && 
                    string.IsNullOrEmpty(vm.Output_Text))
                {
                    vm.Output_Text = inputDir + inputFileName + outputExt;
                }

                // Input Empty, Output Not Empty
                if (!string.IsNullOrEmpty(vm.Output_Text))
                {
                    outputDir = Path.GetDirectoryName(vm.Output_Text).TrimEnd('\\') + @"\";

                    outputFileName = Path.GetFileNameWithoutExtension(vm.Output_Text);
                }

                // -------------------------
                // File Renamer
                // -------------------------
                // Auto Renamer
                // Pressing Script or Convert while Output is empty
                if (inputDir == outputDir
                    && inputFileName == outputFileName
                    && string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
                {
                    outputFileName = FileRenamer(inputFileName);
                }

                // -------------------------
                // Image Sequence Renamer
                // -------------------------
                if (vm.MediaType_SelectedItem == "Sequence")
                {
                    outputFileName = "image-%03d"; //must be this name
                }

                // -------------------------
                // Output
                // -------------------------
                output = outputDir + outputFileName + outputExt; // (eg. C:\Output Folder\ + file + .mp4)    

                // Update TextBox
                if (!string.IsNullOrEmpty(vm.Output_Text))
                {
                    vm.Output_Text = output;
                }
            }

            // -------------------------
            // Batch
            // -------------------------
            else if (vm.Batch_IsChecked == true)
            {
                // Add slash to Batch Output Text folder path if missing
                vm.Output_Text = vm.Output_Text.TrimEnd('\\') + @"\";

                // Input Not Empty, Output Empty
                // Default Output to be same as Input Directory
                if (!string.IsNullOrEmpty(vm.Input_Text) && string.IsNullOrEmpty(vm.Output_Text))
                {
                    vm.Output_Text = vm.Input_Text;
                }

                outputDir = vm.Output_Text;

                // Output             
                output = outputDir + "%~nf" + outputExt; // (eg. C:\Output Folder\%~nf.mp4)
            }

            // -------------------------
            // Empty
            // -------------------------
            // Input Textbox & Output Textbox Both Empty
            if (string.IsNullOrEmpty(vm.Output_Text))
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
            if (vm.VideoQuality_SelectedItem == "Auto" &&
                string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
            {
                // Set Controls:

                // Main
                vm.VideoQuality_SelectedItem = "Auto";
                vm.PixelFormat_SelectedItem = "auto";
                vm.FPS_SelectedItem = "auto";
                vm.Video_Optimize_SelectedItem = "None";
                vm.Size_SelectedItem = "Source";
                vm.Scaling_SelectedItem = "default";

                // Filters
                // Fix
                vm.FilterVideo_Deband_SelectedItem = "disabled";
                vm.FilterVideo_Deshake_SelectedItem = "disabled";
                vm.FilterVideo_Deflicker_SelectedItem = "disabled";
                vm.FilterVideo_Dejudder_SelectedItem = "disabled";
                vm.FilterVideo_Denoise_SelectedItem = "disabled";
                // Selective Color
                // Reds
                vm.FilterVideo_SelectiveColor_Reds_Cyan_Value = 0;
                vm.FilterVideo_SelectiveColor_Reds_Magenta_Value = 0;
                vm.FilterVideo_SelectiveColor_Reds_Yellow_Value = 0;
                // Yellows
                vm.FilterVideo_SelectiveColor_Yellows_Cyan_Value = 0;
                vm.FilterVideo_SelectiveColor_Yellows_Magenta_Value = 0;
                vm.FilterVideo_SelectiveColor_Yellows_Yellow_Value = 0;
                // Greens
                vm.FilterVideo_SelectiveColor_Greens_Cyan_Value = 0;
                vm.FilterVideo_SelectiveColor_Greens_Magenta_Value = 0;
                vm.FilterVideo_SelectiveColor_Greens_Yellow_Value = 0;
                // Cyans
                vm.FilterVideo_SelectiveColor_Cyans_Cyan_Value = 0;
                vm.FilterVideo_SelectiveColor_Cyans_Magenta_Value = 0;
                vm.FilterVideo_SelectiveColor_Cyans_Yellow_Value = 0;
                // Blues
                vm.FilterVideo_SelectiveColor_Blues_Cyan_Value = 0;
                vm.FilterVideo_SelectiveColor_Blues_Magenta_Value = 0;
                vm.FilterVideo_SelectiveColor_Blues_Yellow_Value = 0;
                // Magentas
                vm.FilterVideo_SelectiveColor_Magentas_Cyan_Value = 0;
                vm.FilterVideo_SelectiveColor_Magentas_Magenta_Value = 0;
                vm.FilterVideo_SelectiveColor_Magentas_Yellow_Value = 0;
                // Whites
                vm.FilterVideo_SelectiveColor_Whites_Cyan_Value = 0;
                vm.FilterVideo_SelectiveColor_Whites_Magenta_Value = 0;
                vm.FilterVideo_SelectiveColor_Whites_Yellow_Value = 0;
                // Neutrals
                vm.FilterVideo_SelectiveColor_Neutrals_Cyan_Value = 0;
                vm.FilterVideo_SelectiveColor_Neutrals_Magenta_Value = 0;
                vm.FilterVideo_SelectiveColor_Neutrals_Yellow_Value = 0;
                // Blacks
                vm.FilterVideo_SelectiveColor_Blacks_Cyan_Value = 0;
                vm.FilterVideo_SelectiveColor_Blacks_Magenta_Value = 0;
                vm.FilterVideo_SelectiveColor_Blacks_Yellow_Value = 0;

                // EQ
                vm.FilterVideo_EQ_Brightness_Value = 0;
                vm.FilterVideo_EQ_Contrast_Value = 0;
                vm.FilterVideo_EQ_Saturation_Value = 0;
                vm.FilterVideo_EQ_Gamma_Value = 0;
            }

            // -------------------------
            // Audio
            // -------------------------
            if (vm.AudioQuality_SelectedItem == "Auto" &&
                string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
            {
                // Set Controls:

                // Main
                vm.AudioQuality_SelectedItem = "Auto";
                vm.AudioChannel_SelectedItem = "Source";
                vm.AudioSampleRate_SelectedItem = "auto";
                vm.AudioBitDepth_SelectedItem = "auto";

                // Filters
                vm.Volume_Text = "100";
                vm.AudioHardLimiter_Value = 1;
            }
        }


        /// <summary>
        ///    Container - ComboBox
        /// </summary>
        private void cboContainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Set Controls
            // -------------------------
            FormatControls.SetControls(vm, vm.Container_SelectedItem);

            // -------------------------
            // Output Control Selections
            // -------------------------
            ///FormatControls.OuputFormatDefaults(this);

            // -------------------------
            // Get Output Extension
            // -------------------------
            FormatControls.OutputFormatExt(vm);

            // -------------------------
            // Output ComboBox Options
            // -------------------------
            //FormatControls.OutputFormat(this);

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
            // Video
            // -------------------------
            //if (vm.VideoQuality_SelectedItem != "Auto" &&
            //    string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
            //{
            //    vm.VideoQuality_SelectedItem = "Auto";
            //    vm.PixelFormat_SelectedItem = "auto";
            //    vm.FPS_SelectedItem = "auto";
            //    vm.Video_Optimize_SelectedItem = "None";
            //    vm.Size_SelectedItem = "Source";
            //    vm.Scaling_SelectedItem = "default";

            //    // Filters
            //    // Fix
            //    cboFilterVideo_Deband_SelectedItem = "disabled";
            //    cboFilterVideo_Deshake_SelectedItem = "disabled";
            //    cboFilterVideo_Deflicker_SelectedItem = "disabled";
            //    cboFilterVideo_Dejudder_SelectedItem = "disabled";
            //    cboFilterVideo_Denoise_SelectedItem = "disabled";
            //    // Selective Color
            //    // Reds
            //    vm.FilterVideo_SelectiveColor_Reds_Cyan_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Reds_Magenta_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Reds_Yellow_Value = 0;
            //    // Yellows
            //    vm.FilterVideo_SelectiveColor_Yellows_Cyan_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Yellows_Magenta_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Yellows_Yellow_Value = 0;
            //    // Greens
            //    vm.FilterVideo_SelectiveColor_Greens_Cyan_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Greens_Magenta_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Greens_Yellow_Value = 0;
            //    // Cyans
            //    vm.FilterVideo_SelectiveColor_Cyans_Cyan_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Cyans_Magenta_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Cyans_Yellow_Value = 0;
            //    // Blues
            //    vm.FilterVideo_SelectiveColor_Blues_Cyan_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Blues_Magenta_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Blues_Yellow_Value = 0;
            //    // Magentas
            //    vm.FilterVideo_SelectiveColor_Magentas_Cyan_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Magentas_Magenta_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Magentas_Yellow_Value = 0;
            //    // Whites
            //    vm.FilterVideo_SelectiveColor_Whites_Cyan_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Whites_Magenta_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Whites_Yellow_Value = 0;
            //    // Neutrals
            //    vm.FilterVideo_SelectiveColor_Neutrals_Cyan_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Neutrals_Magenta_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Neutrals_Yellow_Value = 0;
            //    // Blacks
            //    vm.FilterVideo_SelectiveColor_Blacks_Cyan_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Blacks_Magenta_Value = 0;
            //    vm.FilterVideo_SelectiveColor_Blacks_Yellow_Value = 0;

            //    // EQ
            //    slFilterVideo_EQ_Brightness_Value = 0;
            //    slFilterVideo_EQ_Contrast_Value = 0;
            //    slFilterVideo_EQ_Saturation_Value = 0;
            //    slFilterVideo_EQ_Gamma_Value = 0;
            //}

            //// -------------------------
            //// Audio
            //// -------------------------
            //if (vm.AudioQuality_SelectedItem != "Auto" &&
            //    string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
            //{
            //    vm.AudioQuality_SelectedItem = "Auto";
            //    vm.AudioChannel_SelectedItem = "Source";
            //    vm.AudioSampleRate_SelectedItem = "auto";
            //    vm.AudioBitDepth_SelectedItem = "auto";
            //    vm.Volume_Text = "100";
            //    vm.AudioHardLimiter_Value = 1;
            //}

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
            // Force MediaType ComboBox to fire SelectionChanged Event
            // to update Format changes such as AudioStream_SelectedItem
            // -------------------------
            cboMediaType_SelectionChanged(cboMediaType, null);
        }



        /// <summary>
        ///    Media Type - Combobox
        /// </summary>
        private void cboMediaType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FormatControls.MediaType(vm);
        }


        /// <summary>
        ///    Cut Combobox
        /// </summary>
        private void cboCut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FormatControls.CutControls(vm);
        }

        // -------------------------
        // Frame Start Textbox Change
        // -------------------------
        // Got Focus
        private void frameStart_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "width"
            if (frameStart.Focus() == true && vm.FrameStart_Text == "Frame")
            {
                vm.FrameStart_Text = string.Empty;
            }
        }
        // Lost Focus
        private void frameStart_LostFocus(object sender, RoutedEventArgs e)
        {
            // Change textbox back to "auto" if left empty
            if (string.IsNullOrEmpty(vm.FrameStart_Text))
            {
                vm.FrameStart_Text = "Frame";
            }
        }

        // -------------------------
        // Frame End Textbox Change
        // -------------------------
        // Got Focus
        private void frameEnd_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "auto"
            if (frameEnd.Focus() == true && vm.FrameEnd_Text == "Range")
            {
                vm.FrameEnd_Text = string.Empty;
            }
        }
        // Lost Focus
        private void frameEnd_LostFocus(object sender, RoutedEventArgs e)
        {
            // Change textbox back to "auto" if left empty
            if (string.IsNullOrEmpty(vm.FrameEnd_Text))
            {
                vm.FrameEnd_Text = "Range";
            }
        }


        /// <summary>
        ///    Video Codec - ComboBox
        /// </summary>
        private void cboVideoCodec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // PROBLEM - This Control is being triggered twice?
            //Change 1 - Is Clicked
            //Selection is Medium
            //Change 2 - New Items Loaded from Codec
            //Selection is Null

            //MessageBox.Show(vm.VideoQuality_SelectedItem); //debug
            //MessageBox.Show("cboVideoCodec_SelectionChanged"); //debug

            // -------------------------
            // Set Controls
            // -------------------------
            VideoControls.SetControls(vm, vm.VideoCodec_SelectedItem);

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
        }


        /// <summary>
        ///    Pass - ComboBox
        /// </summary>
        private void cboPass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Set Controls
            // -------------------------
            //VideoControls.SetControls(vm, vm.VideoQuality_SelectedItem);

            // -------------------------
            // Pass Controls
            // -------------------------
            VideoControls.EncodingPassControls(vm);

            // -------------------------
            // Display Bit-rate in TextBox
            // -------------------------
            VideoControls.VideoBitrateDisplay(vm,
                                              vm.VideoQuality_Items,
                                              vm.VideoQuality_SelectedItem,
                                              vm.Pass_SelectedItem);
        }
        private void cboPass_DropDownClosed(object sender, EventArgs e)
        {
            // User willingly selected a Pass
            VideoControls.passUserSelected = true;
        }


        /// <summary>
        ///    Video Quality - ComboBox
        /// </summary>
        private void cboVideoQuality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Quality Controls
            // -------------------------
            VideoControls.QualityControls(vm);

            // -------------------------
            // Display Bit-rate in TextBox
            // -------------------------
            VideoControls.VideoBitrateDisplay(vm,
                                              vm.VideoQuality_Items,
                                              vm.VideoQuality_SelectedItem,
                                              vm.Pass_SelectedItem);

            // -------------------------
            // Video Encoding Pass
            // -------------------------
            VideoControls.EncodingPassControls(vm);

            // -------------------------
            // Pixel Format
            // -------------------------
            VideoControls.PixelFormatControls(vm,
                                              vm.MediaType_SelectedItem,
                                              vm.VideoCodec_SelectedItem,
                                              vm.VideoQuality_SelectedItem);
        }


        /// <summary>
        ///     Video CRF Custom Number Textbox
        /// </summary>
        private void tbxCRF_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers or Backspace
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Video VBR Toggle - Checked
        /// </summary>
        private void tglVideoVBR_Checked(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Quality Controls
            // -------------------------
            VideoControls.QualityControls(vm);

            // -------------------------
            // MPEG-4 VBR can only use 1 Pass
            // -------------------------
            if (vm.VideoCodec_SelectedItem == "MPEG-2" || 
                vm.VideoCodec_SelectedItem == "MPEG-4")
            {
                // Change ItemSource
                vm.Pass_Items = new List<string>()
                {
                    "1 Pass",
                };

                // Populate ComboBox from ItemSource
                //vm.Pass_Items = VideoControls.Pass_ItemSource;

                // Select Item
                vm.Pass_SelectedItem = "1 Pass";
            }


            // -------------------------
            // Display Bit-rate in TextBox
            // -------------------------
            VideoControls.VideoBitrateDisplay(vm,
                                              vm.VideoQuality_Items,
                                              vm.VideoQuality_SelectedItem,
                                              vm.Pass_SelectedItem);
        }

        /// <summary>
        ///     Video VBR Toggle - Unchecked
        /// </summary>
        private void tglVideoVBR_Unchecked(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Quality Controls
            // -------------------------
            VideoControls.QualityControls(vm);

            // -------------------------
            // MPEG-2 / MPEG-4 CBR Reset
            // -------------------------
            if (vm.VideoCodec_SelectedItem == "MPEG-2" || 
                vm.VideoCodec_SelectedItem == "MPEG-4")
            {
                // Change ItemSource
                vm.Pass_Items = new List<string>()
                {
                    "2 Pass",
                    "1 Pass",
                };

                // Populate ComboBox from ItemSource
                //cboPass.ItemsSource = VideoControls.Pass_ItemSource;

                // Select Item
                vm.Pass_SelectedItem = "2 Pass";
            }

            // -------------------------
            // Display Bit-rate in TextBox
            // -------------------------
            VideoControls.VideoBitrateDisplay(vm,
                                              vm.VideoQuality_Items,
                                              vm.VideoQuality_SelectedItem,
                                              vm.Pass_SelectedItem);
        }


        /// <summary>
        ///     Pixel Format
        /// </summary>
        private void cboPixelFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //VideoControls.AutoCopyVideoCodec(vm);
        }


        /// <summary>
        ///     FPS ComboBox
        /// </summary>
        private void cboFPS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Custom ComboBox Editable
            // -------------------------
            if (vm.FPS_SelectedItem == "Custom" || 
                string.IsNullOrEmpty(vm.FPS_SelectedItem))
            {
                cboFPS.IsEditable = true;
            }

            // -------------------------
            // Other Items Disable Editable
            // -------------------------
            if (vm.FPS_SelectedItem != "Custom" && 
                !string.IsNullOrEmpty(vm.FPS_SelectedItem))
            {
                cboFPS.IsEditable = false;
            }

            // -------------------------
            // Maintain Editable Combobox while typing
            // -------------------------
            if (cboFPS.IsEditable == true)
            {
                cboFPS.IsEditable = true;

                // Clear Custom Text
                cboFPS.SelectedIndex = -1;
            }

            // -------------------------
            // Disable Copy on change
            // -------------------------
            //VideoControls.AutoCopyVideoCodec(vm);
            //SubtitleControls.AutoCopySubtitleCodec(vm);
        }


        /// <summary>
        ///    Presets
        /// </summary>
        private void cboPreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Presets.Preset(vm);
        }


        /// <summary>
        ///    Optimize Combobox
        /// </summary>
        private void cboOptimize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Optimize Controls
            // -------------------------
            VideoControls.OptimizeControls(vm);
        }


        /// <summary>
        ///    Size Combobox
        /// </summary>
        private void cboSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Set Video Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
            //VideoControls.AutoCopyVideoCodec(vm);
            //SubtitleControls.AutoCopySubtitleCodec(vm);

            // Enable Aspect Custom
            if (vm.Size_SelectedItem == "Custom")
            {
                vm.Width_IsEnabled = true;
                vm.Height_IsEnabled = true;

                vm.Width_Text = "auto";
                vm.Height_Text = "auto";
            }
            else
            {
                vm.Width_IsEnabled = false;
                vm.Height_IsEnabled = false;
                vm.Width_Text = "auto";
                vm.Height_Text = "auto";
            }

            // Change TextBox Resolution numbers
            if (vm.Size_SelectedItem == "Source")
            {
                vm.Width_Text = "auto";
                vm.Height_Text = "auto";
            }
            else if (vm.Size_SelectedItem == "8K")
            {
                vm.Width_Text = "7680";
                vm.Height_Text = "auto";
            }
            else if (vm.Size_SelectedItem == "4K")
            {
                vm.Width_Text = "4096";
                vm.Height_Text = "auto";
            }
            else if (vm.Size_SelectedItem == "4K UHD")
            {
                vm.Width_Text = "3840";
                vm.Height_Text = "auto";
            }
            else if (vm.Size_SelectedItem == "2K")
            {
                vm.Width_Text = "2048";
                vm.Height_Text = "auto";
            }
            else if (vm.Size_SelectedItem == "1440p")
            {
                vm.Width_Text = "auto";
                vm.Height_Text = "1440";
            }
            else if (vm.Size_SelectedItem == "1200p")
            {
                vm.Width_Text = "auto";
                vm.Height_Text = "1200";
            }
            else if (vm.Size_SelectedItem == "1080p")
            {
                vm.Width_Text = "auto";
                vm.Height_Text = "1080";
            }
            else if (vm.Size_SelectedItem == "720p")
            {
                vm.Width_Text = "auto";
                vm.Height_Text = "720";
            }
            else if (vm.Size_SelectedItem == "480p")
            {
                vm.Width_Text = "auto";
                vm.Height_Text = "480";
            }
            else if (vm.Size_SelectedItem == "320p")
            {
                vm.Width_Text = "auto";
                vm.Height_Text = "320";
            }
            else if (vm.Size_SelectedItem == "240p")
            {
                vm.Width_Text = "auto";
                vm.Height_Text = "240";
            }
        }
        // -------------------------
        // Width Textbox Change
        // -------------------------
        // Got Focus
        private void tbxWidth_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "width"
            if (tbxWidth.Focus() == true && vm.Width_Text == "auto")
            {
                vm.Width_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxWidth_LostFocus(object sender, RoutedEventArgs e)
        {
            // Change textbox back to "width" if left empty
            if (string.IsNullOrEmpty(vm.Width_Text))
            {
                vm.Width_Text = "auto";
            }
        }

        // -------------------------
        // Height Textbox Change
        // -------------------------
        // Got Focus
        private void tbxHeight_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "width"
            if (tbxHeight.Focus() == true && vm.Height_Text == "auto")
            {
                vm.Height_Text = string.Empty;
            }
        }
        // Lost Focus
        private void tbxHeight_LostFocus(object sender, RoutedEventArgs e)
        {
            // Change textbox back to "width" if left empty
            if (string.IsNullOrEmpty(vm.Height_Text))
            {
                vm.Height_Text = "auto";
            }
        }



        /// <summary>
        ///     Scaling Video
        /// </summary>
        private void cboScaling_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //VideoControls.AutoCopyVideoCodec(vm);
        }


        /// <summary>
        ///    Crop Window - Button
        /// </summary>
        private void btnCrop_Click(object sender, RoutedEventArgs e)
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
        private void btnCropClear_Click(object sender, RoutedEventArgs e)
        {
            // Clear Crop Values
            CropWindow.CropClear(vm);
        }



        /// <summary>
        ///    Subtitle Codec - ComboBox
        /// </summary>
        private void cboSubtitleCodec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Set Controls
            // -------------------------
            SubtitleControls.SetControls(vm, vm.SubtitleCodec_SelectedItem);

            // -------------------------
            // None Codec
            // -------------------------
            if (vm.SubtitleCodec_SelectedItem == "None")
            {
                vm.SubtitleStream_SelectedItem = "none";
                cboSubtitlesStream.IsEnabled = false;
            }

            // -------------------------
            // Burn Codec
            // -------------------------
            else if (vm.SubtitleCodec_SelectedItem == "Burn")
            {
                // Force Select External
                // Can't burn All subtitle streams
                vm.SubtitleStream_SelectedItem = "external";
                cboSubtitlesStream.IsEnabled = true;
            }

            // -------------------------
            // Copy Codec
            // -------------------------
            else if (vm.SubtitleCodec_SelectedItem == "Copy")
            {
                cboSubtitlesStream.IsEnabled = true;
            }

            // -------------------------
            // All Other Codecs
            // -------------------------
            else
            {
                cboSubtitlesStream.IsEnabled = true;
            }
        }


        /// <summary>
        ///    Subtitle Stream - ComboBox
        /// </summary>
        private void cboSubtitleStream_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // External
            // -------------------------
            if (vm.SubtitleStream_SelectedItem == "external")
            {
                // Enable External ListView and Buttons
                vm.SubtitleListView_IsEnabled = true;

                //listViewSubtitles.IsEnabled = true;

                //btnAddSubtitles.IsEnabled = true;
                //btnRemoveSubtitle.IsEnabled = true;
                //btnSortSubtitleUp.IsEnabled = true;
                //btnSortSubtitleDown.IsEnabled = true;
                //btnClearSubtitles.IsEnabled = true;

                listViewSubtitles.Opacity = 1;
            }
            else
            {
                // Disable External ListView and Buttons
                vm.SubtitleListView_IsEnabled = false;

                //listViewSubtitles.IsEnabled = false;

                //btnAddSubtitles.IsEnabled = false;
                //btnRemoveSubtitle.IsEnabled = false;
                //btnSortSubtitleUp.IsEnabled = false;
                //btnSortSubtitleDown.IsEnabled = false;
                //btnClearSubtitles.IsEnabled = false;

                listViewSubtitles.Opacity = 0.1;
            }
        }

        /// <summary>
        ///     Subtitle ListView
        /// </summary>
        private void listViewSubtitles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Clear before adding new selected items
            if (vm.SubtitleListView_SelectedItems != null &&
                vm.SubtitleListView_SelectedItems.Count > 0)
            {
                vm.SubtitleListView_SelectedItems.Clear();
                vm.SubtitleListView_SelectedItems.TrimExcess();
            }
           
            // Create Selected Items List for ViewModel
            vm.SubtitleListView_SelectedItems = listViewSubtitles.SelectedItems
                                                .Cast<string>()
                                                .ToList();
        }


        /// <summary>
        ///     Subtitle Add
        /// </summary>
        private void btnAddSubtitles_Click(object sender, RoutedEventArgs e)
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
                    //listViewSubtitles.Items.Add(Path.GetFileName(selectFiles.FileNames[i]));
                    vm.SubtitleListView_Items.Add(Path.GetFileName(selectFiles.FileNames[i]));
                }
            }
        }

        /// <summary>
        /// Subtitle Remove
        /// </summary>
        private void btnRemoveSubtitle_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SubtitleListView_SelectedItems.Count > 0)
            {
                var selectedIndex = vm.SubtitleListView_SelectedIndex;

                // ListView Items
                var itemlsvFileNames = vm.SubtitleListView_Items[selectedIndex];
                vm.SubtitleListView_Items.RemoveAt(selectedIndex);

                // List File Paths
                string itemFilePaths = Subtitle.subtitleFilePathsList[selectedIndex];
                Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);

                // List File Names
                string itemFileNames = Subtitle.subtitleFileNamesList[selectedIndex];
                Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
            }
            //if (listViewSubtitles.SelectedItems.Count > 0)
            //{
            //    var selectedIndex = listViewSubtitles.SelectedIndex;

            //    // ListView Items
            //    var itemlsvFileNames = listViewSubtitles.Items[selectedIndex];
            //    listViewSubtitles.Items.RemoveAt(selectedIndex);

            //    // List File Paths
            //    string itemFilePaths = Subtitle.subtitleFilePathsList[selectedIndex];
            //    Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);

            //    // List File Names
            //    string itemFileNames = Subtitle.subtitleFileNamesList[selectedIndex];
            //    Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
            //}
        }

        /// <summary>
        /// Subtitle Clear All
        /// </summary>
        private void btnClearSubtitles_Click(object sender, RoutedEventArgs e)
        {
            SubtitlesClear();
        }

        /// <summary>
        /// Subtitle Clear - Method
        /// </summary>
        public void SubtitlesClear()
        {
            // Clear List View
            //listViewSubtitles.Items.Clear();
            if (vm.SubtitleListView_Items != null &&
                vm.SubtitleListView_Items.Count > 0)
            {
                vm.SubtitleListView_Items.Clear();
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
        private void btnSortSubtitleUp_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SubtitleListView_SelectedItems.Count > 0)
            {
                var selectedIndex = vm.SubtitleListView_SelectedIndex;

                if (selectedIndex > 0)
                {
                    // ListView Items
                    var itemlsvFileNames = vm.SubtitleListView_Items[selectedIndex];
                    vm.SubtitleListView_Items.RemoveAt(selectedIndex);
                    vm.SubtitleListView_Items.Insert(selectedIndex - 1, itemlsvFileNames);

                    // List File Paths
                    string itemFilePaths = Subtitle.subtitleFilePathsList[selectedIndex];
                    Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);
                    Subtitle.subtitleFilePathsList.Insert(selectedIndex - 1, itemFilePaths);

                    // List File Names
                    string itemFileNames = Subtitle.subtitleFileNamesList[selectedIndex];
                    Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
                    Subtitle.subtitleFileNamesList.Insert(selectedIndex - 1, itemFileNames);

                    // Highlight Selected Index
                    vm.SubtitleListView_SelectedIndex = selectedIndex - 1;
                }
            }
            //if (listViewSubtitles.SelectedItems.Count > 0)
            //{
            //    var selectedIndex = listViewSubtitles.SelectedIndex;

            //    if (selectedIndex > 0)
            //    {
            //        // ListView Items
            //        var itemlsvFileNames = listViewSubtitles.Items[selectedIndex];
            //        listViewSubtitles.Items.RemoveAt(selectedIndex);
            //        listViewSubtitles.Items.Insert(selectedIndex - 1, itemlsvFileNames);

            //        // List File Paths
            //        string itemFilePaths = Subtitle.subtitleFilePathsList[selectedIndex];
            //        Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);
            //        Subtitle.subtitleFilePathsList.Insert(selectedIndex - 1, itemFilePaths);

            //        // List File Names
            //        string itemFileNames = Subtitle.subtitleFileNamesList[selectedIndex];
            //        Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
            //        Subtitle.subtitleFileNamesList.Insert(selectedIndex - 1, itemFileNames);

            //        // Highlight Selected Index
            //        listViewSubtitles.SelectedIndex = selectedIndex - 1;
            //    }
            //}
        }

        /// <summary>
        /// Subtitle Sort Down
        /// </summary>
        private void btnSortSubtitleDown_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SubtitleListView_SelectedItems.Count > 0)
            {
                var selectedIndex = vm.SubtitleListView_SelectedIndex;

                if (selectedIndex + 1 < vm.SubtitleListView_Items.Count)
                {
                    // ListView Items
                    var itemlsvFileNames = vm.SubtitleListView_Items[selectedIndex];
                    vm.SubtitleListView_Items.RemoveAt(selectedIndex);
                    vm.SubtitleListView_Items.Insert(selectedIndex + 1, itemlsvFileNames);

                    // List FilePaths
                    string itemFilePaths = Subtitle.subtitleFilePathsList[selectedIndex];
                    Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);
                    Subtitle.subtitleFilePathsList.Insert(selectedIndex + 1, itemFilePaths);

                    // List File Names
                    string itemFileNames = Subtitle.subtitleFileNamesList[selectedIndex];
                    Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
                    Subtitle.subtitleFileNamesList.Insert(selectedIndex + 1, itemFileNames);

                    // Highlight Selected Index
                    vm.SubtitleListView_SelectedIndex = selectedIndex + 1;
                }
            }
            //if (listViewSubtitles.SelectedItems.Count > 0)
            //{
            //    var selectedIndex = listViewSubtitles.SelectedIndex;

            //    if (selectedIndex + 1 < listViewSubtitles.Items.Count)
            //    {
            //        // ListView Items
            //        var itemlsvFileNames = listViewSubtitles.Items[selectedIndex];
            //        listViewSubtitles.Items.RemoveAt(selectedIndex);
            //        listViewSubtitles.Items.Insert(selectedIndex + 1, itemlsvFileNames);

            //        // List FilePaths
            //        string itemFilePaths = Subtitle.subtitleFilePathsList[selectedIndex];
            //        Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);
            //        Subtitle.subtitleFilePathsList.Insert(selectedIndex + 1, itemFilePaths);

            //        // List File Names
            //        string itemFileNames = Subtitle.subtitleFileNamesList[selectedIndex];
            //        Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
            //        Subtitle.subtitleFileNamesList.Insert(selectedIndex + 1, itemFileNames);

            //        // Highlight Selected Index
            //        listViewSubtitles.SelectedIndex = selectedIndex + 1;
            //    }
            //}
        }



        /// <summary>
        ///    Audio Codec - ComboBox
        /// </summary>
        private void cboAudioCodec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Set Controls
            // -------------------------
            AudioControls.SetControls(vm, vm.AudioCodec_SelectedItem);

            //// -------------------------
            //// Quality Controls
            //// -------------------------
            //AudioControls.QualityControls(vm);

            //// -------------------------
            //// Display Bit-rate in TextBox
            //// -------------------------
            //AudioControls.AudioBitrateDisplay(vm,
            //                                  vm.AudioQuality_Items,
            //                                  vm.AudioQuality_SelectedItem
            //                                  );

        }


        /// <summary>
        ///    Audio Channel - ComboBox
        /// </summary>
        private void cboChannel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //AudioControls.AutoCopyAudioCodec(vm);
        }


        /// <summary>
        ///    Audio Quality - ComboBox
        /// </summary>
        private void cboAudioQuality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Set Controls
            // -------------------------
            //AudioControls.SetControls(vm, vm.AudioCodec_SelectedItem);

            // -------------------------
            // Quality Controls
            // -------------------------
            AudioControls.QualityControls(vm);

            // -------------------------
            // Display Bit-rate in TextBox
            // -------------------------
            AudioControls.AudioBitrateDisplay(vm,
                                              vm.AudioQuality_Items,
                                              vm.AudioQuality_SelectedItem
                                              );

        }


        /// <summary>
        ///    Audio VBR - Toggle
        /// </summary>
        // Checked
        private void tglAudioVBR_Checked(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Quality Controls
            // -------------------------
            AudioControls.QualityControls(vm);

            // -------------------------
            // Display Bit-rate in TextBox
            // -------------------------
            AudioControls.AudioBitrateDisplay(vm,
                                              vm.AudioQuality_Items,
                                              vm.AudioQuality_SelectedItem
                                              );
        }
        // Unchecked
        private void tglAudioVBR_Unchecked(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Quality Controls
            // -------------------------
            AudioControls.QualityControls(vm);

            // -------------------------
            // Display Bit-rate in TextBox
            // -------------------------
            AudioControls.AudioBitrateDisplay(vm,
                                              vm.AudioQuality_Items,
                                              vm.AudioQuality_SelectedItem
                                              );
        }


        /// <summary>
        ///     Audio Custom Bitrate kbps - Textbox
        /// </summary>
        private void tbxAudioBitrate_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers or Backspace
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }
        // Got Focus
        private void tbxAudioBitrate_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear Textbox on first use
            if (vm.AudioBitrate_Text == string.Empty)
            {
                TextBox tbac = (TextBox)sender;
                tbac.Text = string.Empty;
                tbac.GotFocus += tbxAudioBitrate_GotFocus; //used to be -=
            }
        }
        // Lost Focus
        private void tbxAudioBitrate_LostFocus(object sender, RoutedEventArgs e)
        {
            // Change Textbox back to kbps
            TextBox tbac = sender as TextBox;
            if (tbac.Text.Trim().Equals(string.Empty))
            {
                tbac.Text = string.Empty;
                tbac.GotFocus -= tbxAudioBitrate_GotFocus; //used to be +=
            }
        }


        /// <summary>
        ///     Samplerate ComboBox
        /// </summary>
        private void cboSampleRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(vm.AudioSampleRate_SelectedItem))
            //{
            //    AudioSampleRate_PreviousItem = vm.AudioSampleRate_SelectedItem;
            //}

            //MessageBox.Show("Previous: " + AudioSampleRate_PreviousItem); //debug
            //MessageBox.Show("Current: " + vm.AudioSampleRate_SelectedItem); //debug

            //if (AudioSampleRate_PreviousItem != vm.AudioSampleRate_SelectedItem)
            //{
            //    // Switch to Copy if inputExt & outputExt match
            //    AudioControls.AutoCopyAudioCodec(vm);
            //}

            //MessageBox.Show("Current Changed: " + vm.AudioSampleRate_SelectedItem); //debug
        }


        /// <summary>
        ///     Bit Depth ComboBox
        /// </summary>
        private void cboBitDepth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(vm.AudioSampleRate_SelectedItem))
            //{
            //    AudioSampleRate_PreviousItem = vm.AudioSampleRate_SelectedItem;
            //}

            //MessageBox.Show("Previous: " + AudioSampleRate_PreviousItem); //debug
            //MessageBox.Show("Current: " + vm.AudioSampleRate_SelectedItem); //debug

            //if (AudioSampleRate_PreviousItem != vm.AudioSampleRate_SelectedItem)
            //{
            //    // Switch to Copy if inputExt & outputExt match
            //    AudioControls.AutoCopyAudioCodec(vm);
            //}

        }


        /// <summary>
        ///    Volume TextBox Changed
        /// </summary>
        private void tbxVolume_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Disable Volume instead of running AutoCopyAudioCodec each time 
            // This needs to be re-thought, calling method on every timer tick
            //AudioControls.AutoCopyAudioCodec(vm);
        }
        /// <summary>
        ///    Volume TextBox KeyDown
        /// </summary>
        private void tbxVolume_KeyDown(object sender, KeyEventArgs e)
        {
            try //error if other letters or symbols get in
            {
                // Only allow Numbers or Backspace
                if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back)
                {
                    e.Handled = true;
                }
                // Allow Percent %
                if ((e.Key == Key.D5) && e.Key == Key.RightShift | e.Key == Key.LeftShift)
                {
                    e.Handled = true;
                }
            }
            catch
            {

            }
        }

        /// <summary>
        ///    Volume Buttons
        /// </summary>
        // -------------------------
        // Up
        // -------------------------
        // Volume Up Button Click
        private void btnVolumeUp_Click(object sender, RoutedEventArgs e)
        {
            int value;
            int.TryParse(vm.Volume_Text, out value);

            value += 1;
            vm.Volume_Text = value.ToString();
        }
        // Up Button Each Timer Tick
        private void dispatcherTimerUp_Tick(object sender, EventArgs e)
        {
            int value;
            int.TryParse(vm.Volume_Text, out value);

            value += 1;
            vm.Volume_Text = value.ToString();
        }
        // Hold Up Button
        private void btnVolumeUp_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Timer      
            dispatcherTimerUp.Interval = new TimeSpan(0, 0, 0, 0, 100); //100ms
            dispatcherTimerUp.Start();
        }
        // Up Button Released
        private void btnVolumeUp_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Disable Timer
            dispatcherTimerUp.Stop();
        }
        // -------------------------
        // Down
        // -------------------------
        // Volume Down Button Click
        private void btnVolumeDown_Click(object sender, RoutedEventArgs e)
        {
            int value;
            int.TryParse(vm.Volume_Text, out value);

            value -= 1;
            vm.Volume_Text = value.ToString();
        }
        // Down Button Each Timer Tick
        private void dispatcherTimerDown_Tick(object sender, EventArgs e)
        {
            int value;
            int.TryParse(vm.Volume_Text, out value);

            value -= 1;
            vm.Volume_Text = value.ToString();
        }
        // Hold Down Button
        private void btnVolumeDown_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Timer      
            dispatcherTimerDown.Interval = new TimeSpan(0, 0, 0, 0, 100); //100ms
            dispatcherTimerDown.Start();
        }
        // Down Button Released
        private void btnVolumeDown_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Disable Timer
            dispatcherTimerDown.Stop();
        }


        /// <summary>
        ///     Audio Hard Limiter - Slider
        /// </summary>
        private void slAudioHardLimiter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.AudioHardLimiter_Value = 1;

            AudioControls.AutoCopyAudioCodec(vm);
        }

        private void slAudioHardLimiter_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }

        private void tbxAudioHardLimiter_PreviewKeyUp(object sender, KeyEventArgs e)
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
        ///     Audio Limiter
        /// </summary>
        private void slAudioLimiter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.AudioHardLimiter_Value = 1;

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
        private void slFilterAudio_Contrast_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterAudio_Contrast_Value = 0;

            AudioControls.AutoCopyAudioCodec(vm);
        }

        private void slFilterAudio_Contrast_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }
        private void tbxFilterAudio_Contrast_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }

        /// <summary>
        ///     Filter Audio - Extra Stereo
        /// </summary>
        private void slFilterAudio_ExtraStereo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterAudio_ExtraStereo_Value = 0;

            AudioControls.AutoCopyAudioCodec(vm);
        }

        private void slFilterAudio_ExtraStereo_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }
        private void tbxFilterAudio_ExtraStereo_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }

        /// <summary>
        ///     Filter Audio - Tempo
        /// </summary>
        private void slFilterAudio_Tempo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            vm.FilterAudio_Tempo_Value = 100;

            AudioControls.AutoCopyAudioCodec(vm);
        }

        private void slFilterAudio_Tempo_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            AudioControls.AutoCopyAudioCodec(vm);
        }

        private void tbxFilterAudio_Tempo_PreviewKeyUp(object sender, KeyEventArgs e)
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
                    // Clear Old Text
                    //ScriptView.scriptParagraph.Inlines.Clear();
                    //ScriptView.ClearScriptView(this, vm);

                    // Write FFmpeg Args Sort
                    //rtbScriptView.Document = new FlowDocument(ScriptView.scriptParagraph);
                    //rtbScriptView.BeginChange();
                    //ScriptView.scriptParagraph.Inlines.Add(new Run(FFmpeg.ffmpegArgsSort));
                    //rtbScriptView.EndChange();
                    //vm.ScriptView_Text = string.Join<char>(" ", FFmpeg.ffmpegArgsSort);
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

                    // Clear Old Text
                    //ScriptView.ClearScriptView(this, vm);
                    //ScriptView.scriptParagraph.Inlines.Clear();

                    // Write FFmpeg Args
                    //rtbScriptView.Document = new FlowDocument(ScriptView.scriptParagraph);
                    //rtbScriptView.BeginChange();
                    //ScriptView.scriptParagraph.Inlines.Add(new Run(FFmpeg.ffmpegArgs));
                    //rtbScriptView.EndChange();
                    //vm.ScriptView_Text = string.Join<char>(" ", FFmpeg.ffmpegArgs);
                    vm.ScriptView_Text = FFmpeg.ffmpegArgs;

                    // Sort is On
                    ScriptView.sort = false;
                    // Change Button Back to Sort
                    txblScriptSort.Text = "Sort";
                }
            }
        }


        /// <summary>
        ///    Script View Copy/Paste
        /// </summary>
        private void OnScriptPaste(object sender, DataObjectPastingEventArgs e)
        {
            // Copy Pasted Script
            //string script = ScriptView.GetScriptRichTextBoxContents(this);

            //// Select All Text
            //TextRange textRange = new TextRange(
            //    rtbScriptView.Document.ContentStart,
            //    rtbScriptView.Document.ContentEnd
            //);

            //// Remove Formatting
            //textRange.ClearAllProperties();

            // Clear Text
            //ScriptView.ClearScriptView(this);
            //ScriptView.scriptParagraph.Inlines.Clear();

            // Remove Double Paragraph Spaces
            //rtbScriptView.Document = new FlowDocument(ScriptView.scriptParagraph);

            //rtbScriptView.BeginChange();
            //ScriptView.scriptParagraph.Inlines.Add(new Run(script.Replace("\n","")));
            //rtbScriptView.EndChange();
        }

        private void OnScriptCopy(object sender, DataObjectCopyingEventArgs e)
        {

        }

        /// <summary>
        ///    Script - Button
        /// </summary>
        private void btnScript_Click(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Clear Variables before Run
            // -------------------------
            ClearVariables(vm);


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
            // Enable Script
            // -------------------------
            script = true;

            // -------------------------
            // Reset Sort
            // -------------------------
            ScriptView.sort = false;
            txblScriptSort.Text = "Sort";

            // -------------------------
            // Batch Extention Period Check
            // -------------------------
            BatchExtCheck(vm);

            // -------------------------
            // Set FFprobe Path
            // -------------------------
            FFprobePath();

            // -------------------------
            // Ready Halts
            // -------------------------
            ReadyHalts(vm);

            // -------------------------
            // Single
            // -------------------------
            if (tglBatch.IsChecked == false)
            {
                // -------------------------
                // FFprobe Detect Metadata
                // -------------------------
                FFprobe.Metadata(vm);

                // -------------------------
                // FFmpeg Generate Arguments (Single)
                // -------------------------
                //disabled if batch
                FFmpeg.FFmpegSingleGenerateArgs(vm);
            }

            // -------------------------
            // Batch
            // -------------------------
            else if (tglBatch.IsChecked == true)
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

            // -------------------------
            // Write All Log Actions to Console
            // -------------------------
            Log.LogWriteAll(this, vm);

            // -------------------------
            // Generate Script
            // -------------------------
            FFmpeg.FFmpegScript(vm);

            // -------------------------
            // Auto Sort Toggle
            // -------------------------
            if (tglAutoSortScript.IsChecked == true)
            {
                Sort();
            }

            // -------------------------
            // Clear Variables for next Run
            // -------------------------
            ClearVariables(vm);
            GC.Collect();
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
            // -------------------------
            // Check if Script has been Edited
            // -------------------------
            if (CheckScriptEdited(vm) == true)
            {
                // Halt
                return;
            }

            // -------------------------
            // Clear Variables before Run
            // -------------------------
            ClearVariables(vm);

            // -------------------------
            // Enable Script
            // -------------------------
            script = true;

            // -------------------------
            // Batch Extention Period Check
            // -------------------------
            BatchExtCheck(vm);

            // -------------------------
            // Set FFprobe Path
            // -------------------------
            FFprobePath();

            // -------------------------
            // Ready Halts
            // -------------------------
            ReadyHalts(vm);


            // Log Console Message /////////
            if (ready == true)
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
            }


            // --------------------------------------------------------------------
            // Ready Check
            // If Ready, start conversion process
            // --------------------------------------------------------------------
            if (ready == true)
            {
                // -------------------------
                // Single
                // -------------------------
                if (tglBatch.IsChecked == false)
                {
                    // -------------------------
                    // FFprobe Detect Metadata
                    // -------------------------
                    FFprobe.Metadata(vm);

                    // -------------------------
                    // FFmpeg Generate Arguments (Single)
                    // -------------------------
                    //disabled if batch
                    FFmpeg.FFmpegSingleGenerateArgs(vm);
                }

                // -------------------------
                // Batch
                // -------------------------
                else if (tglBatch.IsChecked == true)
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

                // -------------------------
                // FFmpeg Convert
                // -------------------------
                FFmpeg.FFmpegConvert(vm);

                // -------------------------
                // Sort Script
                // -------------------------
                // Only if Auto Sort is enabled
                if (tglAutoSortScript.IsChecked == true)
                {
                    ScriptView.sort = false;
                    Sort();
                }

                // -------------------------
                // Reset Sort
                // -------------------------
                // Auto Sort enabled
                //if (tglAutoSortScript.IsChecked == true)
                //{
                //    ScriptView.sort = false;
                //    txblScriptSort.Text = "Inline";
                //}
                //// Auto Sort disabled
                //else if (tglAutoSortScript.IsChecked == false)
                //{
                //    ScriptView.sort = true;
                //    txblScriptSort.Text = "Sort";
                //}

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Run("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~") { Foreground = Log.ConsoleAction });
                };
                Log.LogActions.Add(Log.WriteAction);


                // -------------------------
                // Write All Log Actions to Console
                // -------------------------
                Log.LogWriteAll(this, vm);

                // -------------------------
                // Generate Script
                // -------------------------
                //FFmpeg.FFmpegScript(this); // moved to FFmpegConvert()

                // -------------------------
                // Clear Strings for next Run
                // -------------------------
                ClearVariables(vm);
                GC.Collect();
            }
            else
            {
                //debug
                //MessageBox.Show("Not Ready");

                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Run("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~") { Foreground = Log.ConsoleAction });
                };
                Log.LogActions.Add(Log.WriteAction);


                /// <summary>
                ///    Write All Log Actions to Console
                /// </summary> 
                Log.LogWriteAll(this, vm);

                /// <summary>
                ///    Restart
                /// </summary> 
                /* unlock */
                ready = true;

                // -------------------------
                // Write Variables to Debug Window (Method)
                // -------------------------
                //DebugConsole.DebugWrite(debugconsole, this);

                // -------------------------
                // Clear Variables for next Run
                // -------------------------
                ClearVariables(vm);
                GC.Collect();

            }

        } //end convert button


    }
}
