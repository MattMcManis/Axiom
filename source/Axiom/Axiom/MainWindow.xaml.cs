/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017, 2018 Matt McManis
http://github.com/MattMcManis/Axiom
http://axiomui.github.io
axiom.interface@gmail.com

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
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
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
        // Axiom Current Version
        public static Version currentVersion;
        // Axiom GitHub Latest Version
        public static Version latestVersion;
        // Alpha, Beta, Stable
        public static string currentBuildPhase = "alpha";
        public static string latestBuildPhase;
        public static string[] splitVersionBuildPhase;

        public string TitleVersion {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Other Windows
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        ///     Log Console
        /// </summary>
        public LogConsole logconsole = new LogConsole(((MainWindow)Application.Current.MainWindow), configurewindow);

        /// <summary>
        ///     Debug Console
        /// </summary>
        public static DebugConsole debugconsole; //pass data

        /// <summary>
        ///     File Properties Console
        /// </summary>
        public FilePropertiesWindow filepropwindow; //pass data

        /// <summary>
        ///     Script View
        /// </summary>
        public static ScriptView scriptview; //pass data

        /// <summary>
        ///     Configure Window
        /// </summary>
        public static ConfigureWindow configurewindow; //pass data

        /// <summary>
        ///     File Queue
        /// </summary>
        //public FileQueue filequeue = new FileQueue();

        /// <summary>
        ///     Crop Window
        /// </summary>
        public static CropWindow cropwindow; //pass data

        /// <summary>
        ///     Optimize Advanced Window
        /// </summary>
        public static OptimizeAdvancedWindow optadvwindow; //pass data

        /// <summary>
        ///     Optimize Advanced Window
        /// </summary>
        public static InfoWindow infowindow; //pass data

        /// <summary>
        ///     Optimize Advanced Window
        /// </summary>
        public static UpdateWindow updatewindow; //pass data


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
        public static string threads; // CPU Threads
        public static string maxthreads; // All CPU Threads

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
        public static string batchExt; // Batch user entered extension (eg. mp4 or .mp4)
        public static string batchInputAuto;


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Volume Up Down
        /// </summary>
        /// <remarks>
        ///     Used for Volume Up Down buttons. Integer += 1 for each tick of the timer.
        ///     Timer Tick in MainWindow Initialize
        /// </remarks>
        // --------------------------------------------------------------------------------------------------------
        public DispatcherTimer dispatcherTimerUp = new DispatcherTimer(DispatcherPriority.Render);
        public DispatcherTimer dispatcherTimerDown = new DispatcherTimer(DispatcherPriority.Render);


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
            this.MinWidth = 615;
            this.MinHeight = 305;
            this.MaxWidth = 615;
            this.MaxHeight = 305;

            // -----------------------------------------------------------------
            /// <summary>
            /// Start the File Queue (Hidden)
            /// </summary>
            // disabled
            //StartFileQueue(); 
            // -----------------------------------------------------------------

            // -----------------------------------------------------------------
            /// <summary>
            /// Start the Log Console (Hidden)
            /// </summary>
            StartLogConsole();


            // Log Console Message /////////
            logconsole.rtbLog.Document = new FlowDocument(Log.logParagraph); //start
            logconsole.rtbLog.BeginChange(); //begin change

            Log.logParagraph.Inlines.Add(new Bold(new Run(TitleVersion)) { Foreground = Log.ConsoleTitle });

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
            try
            {
                // First time use
                if (Convert.ToDouble(Settings.Default["Left"]) == 0 || Convert.ToDouble(Settings.Default["Top"]) == 0)
                {
                    this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
            }
            catch
            {

            }         

            // -------------------------
            // Load Theme
            // -------------------------
            ConfigureWindow.LoadTheme(configurewindow);

            // Log Console Message /////////
            // Don't put in Configure Method, creates duplicate message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Theme: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(ConfigureWindow.theme) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load FFmpeg.exe Path
            // -------------------------
            ConfigureWindow.LoadFFmpegPath(configurewindow);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("FFmpeg: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(ConfigureWindow.ffmpegPath) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load FFprobe.exe Path
            // -------------------------
            ConfigureWindow.LoadFFprobePath(configurewindow);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("FFprobe: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(ConfigureWindow.ffprobePath) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load Log Enabled
            // -------------------------
            ConfigureWindow.LoadLogCheckbox(configurewindow);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Log Enabled: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(Convert.ToString(ConfigureWindow.logEnable)) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load Log Path
            // -------------------------
            ConfigureWindow.LoadLogPath(configurewindow);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Log Path: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(ConfigureWindow.logPath) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load Threads
            // -------------------------
            ConfigureWindow.LoadThreads(configurewindow);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Using CPU Threads: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(ConfigureWindow.threads) { Foreground = Log.ConsoleDefault });


            //end change !important
            logconsole.rtbLog.EndChange(); 


            // -------------------------
            // Load Keep Window Toggle
            // -------------------------
            // Log Checkbox     
            // Safeguard Against Corrupt Saved Settings
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                if (string.IsNullOrEmpty(Convert.ToString(Settings.Default.KeepWindow)))
                {
                    tglWindowKeep.IsChecked = true;
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Convert.ToString(Settings.Default.KeepWindow)))
                {
                    tglWindowKeep.IsChecked = Convert.ToBoolean(Settings.Default.KeepWindow);
                }
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

        } // End MainWindow




        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     MAIN METHODS
        /// </summary>
        /// <remarks>
        ///     Methods that belong to the MainWindow Class
        /// </remarks>
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        ///    Window Loaded
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Set Current Version to Assembly Version
            // -------------------------
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string assemblyVersion = fvi.FileVersion;
            currentVersion = new Version(assemblyVersion);

            // -------------------------
            // Title + Version
            // -------------------------
            TitleVersion = "Axiom ~ FFmpeg UI (" + Convert.ToString(currentVersion) + "-" + currentBuildPhase + ")";
            DataContext = this;

            // -------------------------
            // Control Defaults
            // -------------------------
            // ComboBox Item Sources
            cboFormat.ItemsSource = FormatControls.FormatItemSource;
            cboMediaType.ItemsSource = FormatControls.MediaTypeItemSource;

            cboFormat.SelectedIndex = 0;
            cboFPS.SelectedIndex = 0;
            cboCut.SelectedIndex = 0;
            cboSize.SelectedIndex = 0;
            cboPreset.SelectedIndex = 0;

            tglWindowKeep.IsChecked = true;

            //AudioControls.Audio_SelectedItem = AudioControls.AudioItemSource[0];
            //AudioControls.Audio_SelectedItem = AudioControls.AudioItemSource[0];

            // Batch Extension Box Disabled
            batchExtensionTextBox.IsEnabled = false;

            // Open Input/Output Location Disabled
            openLocationInput.IsEnabled = false;
            openLocationOutput.IsEnabled = false;

            // -------------------------
            // Startup Preset
            // -------------------------
            // Default Format is WebM
            if ((string)cboFormat.SelectedItem == "webm")
            {
                cboSubtitle.SelectedItem = "none";
                cboAudioStream.SelectedItem = "1";
            }
        }

        /// <summary>
        ///     Close / Exit (Method)
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            // Force Exit All Executables
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        // Save Window Position
        void Window_Closing(object sender, CancelEventArgs e)
        {
            Settings.Default.Save();
        }


        /// <summary>
        ///     Clear Variables (Method)
        /// </summary>
        public static void ClearVariables(MainWindow mainwindow)
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
            Video.vCodec = string.Empty;
            Video.vQuality = string.Empty;
            //Video.vBitMode = string.Empty;
            Video.vBitrate = string.Empty;
            Video.vMaxrate = string.Empty;
            Video.vOptions = string.Empty;
            Video.crf = string.Empty;
            Video.fps = string.Empty;
            Video.optTune = string.Empty;
            Video.optProfile = string.Empty;
            Video.optLevel = string.Empty;
            Video.aspect = string.Empty;
            Video.width = string.Empty;
            Video.height = string.Empty;

            // Clear Crop if ClearCrop Button Identifier is Empty
            if (mainwindow.buttonCropClearTextBox.Text == "") 
            {
                CropWindow.crop = string.Empty;
                CropWindow.divisibleCropWidth = null; //int
                CropWindow.divisibleCropHeight = null; //int
            }

            Format.trim = string.Empty;
            Format.trimStart = string.Empty;
            Format.trimEnd = string.Empty;
      
            Video.vFilter = string.Empty;
            Video.geq = string.Empty;

            if (Video.VideoFilters != null)
            {
                Video.VideoFilters.Clear();
                Video.VideoFilters.TrimExcess();
            }

            Video.v2PassArgs = string.Empty;
            Video.pass1Args = string.Empty; // Batch 2-Pass
            Video.pass2Args = string.Empty; // Batch 2-Pass
            Video.pass1 = string.Empty;
            Video.pass2 = string.Empty;
            Video.image = string.Empty;
            Video.optimize = string.Empty;
            Video.speed = string.Empty;

            // Audio
            Audio.aCodec = string.Empty;
            Audio.aQuality = string.Empty;
            Audio.aBitMode = string.Empty;
            Audio.aBitrate = string.Empty;
            Audio.aChannel = string.Empty;
            Audio.aSamplerate = string.Empty;
            Audio.aBitDepth = string.Empty;
            Audio.aBitrateLimiter = string.Empty;
            Audio.aFilter = string.Empty;
            Audio.aVolume = string.Empty;
            Audio.aLimiter = string.Empty;

            if (Audio.AudioFilters != null)
            {
                Audio.AudioFilters.Clear();
                Audio.AudioFilters.TrimExcess();
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
        ///     Start Log Console (Method)
        /// </summary>
        public void StartLogConsole()
        {
            // Open LogConsole Window
            logconsole = new LogConsole(this, configurewindow);
            logconsole.Hide();

            // Position with Show();

            logconsole.rtbLog.Cursor = Cursors.Arrow;
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
            ManagementClass os = new ManagementClass("Win32_OperatingSystem");
            foreach (ManagementObject obj in os.GetInstances())
            {
                // Log Console Message /////////
                try
                {
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(obj["Caption"])) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                }
                catch
                {

                }
            }
            os.Dispose();

            /// <summary>
            /// CPU
            /// </summary>
            ManagementObjectSearcher cpu = new ManagementObjectSearcher("root\\CIMV2", "SELECT Name FROM Win32_Processor");
            foreach (ManagementObject obj in cpu.Get())
            {
                // Log Console Message /////////
                try
                {
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(obj["Name"])) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                }
                catch
                {

                }
            }
            cpu.Dispose();
            // Max Threads
            foreach (var item in new System.Management.ManagementObjectSearcher("Select NumberOfLogicalProcessors FROM Win32_ComputerSystem").Get())
            {
                maxthreads = String.Format("{0}", item["NumberOfLogicalProcessors"]);
            }

            /// <summary>
            /// GPU
            /// </summary>
            ManagementObjectSearcher gpu = new ManagementObjectSearcher("root\\CIMV2", "SELECT Name, AdapterRAM FROM Win32_VideoController");
            foreach (ManagementObject obj in gpu.Get())
            {
                try
                {
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(obj["Name"]) + " " + Convert.ToString(Math.Round(Convert.ToDouble(obj["AdapterRAM"]) * 0.000000001, 3) + "GB")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                }
                catch
                {

                }
            }

            /// <summary>
            /// RAM
            /// </summary>
            Log.logParagraph.Inlines.Add(new Run("RAM ") { Foreground = Log.ConsoleDefault });

            double capacity = 0;
            int memtype = 0;
            string type;
            int speed = 0;

            ManagementObjectSearcher ram = new ManagementObjectSearcher("root\\CIMV2", "SELECT Capacity, MemoryType, Speed FROM Win32_PhysicalMemory");
            foreach (ManagementObject obj in ram.Get())
            {
                try
                {
                    capacity += Convert.ToDouble(obj["Capacity"]);
                    memtype = Int32.Parse(obj.GetPropertyValue("MemoryType").ToString());
                    speed = Int32.Parse(obj.GetPropertyValue("Speed").ToString());
                }
                catch
                {

                }
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
            try
            {
                Log.logParagraph.Inlines.Add(new Run(Convert.ToString(capacity) + "GB " + type + " " + Convert.ToString(speed) + "MHz") { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new LineBreak());
            }
            catch
            {

            }
            ram.Dispose();
            // End System Info
        }

        /// <summary>
        ///     Start File Queue (Method)
        /// </summary>
        //public void StartFileQueue()
        //{
        //    MainWindow mainwindow = this;

        //    // Open File Queue Window
        //    filequeue = new FileQueue(mainwindow);
        //    filequeue.Hide();

        //    // Position with Show();
        //}


        /// <summary>
        ///    FFcheck (Method)
        /// </summary>
        /// <remarks>
        ///     Check if FFmpeg and FFprobe is on Computer 
        /// </remarks>
        public void FFcheck()
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
                if (ConfigureWindow.ffmpegPath == "<auto>")
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
                            MessageBox.Show("Cannot locate FFmpeg Path in Environment Variables or Current Folder.");
                        }

                    }
                }
                // If User Defined Path
                else if (ConfigureWindow.ffmpegPath != "<auto>" && ConfigureWindow.ffmpegPath != null && ConfigureWindow.ffmpegPath != string.Empty)
                {
                    var dirPath = Path.GetDirectoryName(ConfigureWindow.ffmpegPath).TrimEnd('\\') + @"\";
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
                        MessageBox.Show("Cannot locate FFmpeg Path in User Defined Path.");
                    }

                    // If Configure Path is ffmpeg.exe and not another Program
                    if (string.Equals(ConfigureWindow.ffmpegPath, fullPath, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // let pass
                        ffCheckCleared = true;
                    }
                    else
                    {
                        /* lock */
                        ready = false;
                        ffCheckCleared = false;
                        MessageBox.Show("Error: FFmpeg Path must link to ffmpeg.exe.");
                    }
                }

                // -------------------------
                // FFprobe
                // -------------------------
                // If Auto Mode
                if (ConfigureWindow.ffprobePath == "<auto>")
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
                            MessageBox.Show("Cannot locate FFprobe Path in Environment Variables or Current Folder.");
                        }

                    }
                }
                // If User Defined Path
                else if (ConfigureWindow.ffprobePath != "<auto>" && ConfigureWindow.ffprobePath != null && ConfigureWindow.ffprobePath != string.Empty)
                {
                    var dirPath = Path.GetDirectoryName(ConfigureWindow.ffprobePath).TrimEnd('\\') + @"\";
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
                        MessageBox.Show("Cannot locate FFprobe Path in User Defined Path.");
                    }

                    // If Configure Path is FFmpeg.exe and not another Program
                    if (string.Equals(ConfigureWindow.ffprobePath, fullPath, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // let pass
                        ffCheckCleared = true;
                    }
                    else
                    {
                        /* lock */
                        ready = false;
                        ffCheckCleared = false;
                        MessageBox.Show("Error: FFprobe Path must link to ffprobe.exe.");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Unknown Error trying to locate FFmpeg or FFprobe.");
            }
        }


        /// <summary>
        ///    FFmpeg Path (Method)
        /// </summary>
        public static String FFmpegPath(MainWindow mainwindow)
        {
            // -------------------------
            // FFmpeg.exe and FFprobe.exe Paths
            // -------------------------
            // If Configure FFmpeg Path is <auto>
            if (ConfigureWindow.ffmpegPath == "<auto>")
            {
                if (File.Exists(appDir + "ffmpeg\\bin\\ffmpeg.exe"))
                {
                    //use included binary
                    FFmpeg.ffmpeg = "\"" + appDir + "ffmpeg\\bin\\ffmpeg.exe" + "\"";
                }
                else if (!File.Exists(appDir + "ffmpeg\\bin\\ffmpeg.exe"))
                {
                    //use system installed binaries
                    FFmpeg.ffmpeg = "ffmpeg";
                }
            }
            // Use User Custom Path
            else
            {
                FFmpeg.ffmpeg = "\"" + ConfigureWindow.ffmpegPath + "\"";
            }

            // Return Value
            return FFmpeg.ffmpeg;
        }


        /// <remarks>
        ///     FFprobe Path
        /// </remarks>
        public static void FFprobePath(MainWindow mainwindow)
        {
            // If Configure FFprobe Path is <auto>
            if (ConfigureWindow.ffprobePath == "<auto>")
            {
                if (File.Exists(appDir + "ffmpeg\\bin\\ffprobe.exe"))
                {
                    //use included binary
                    FFprobe.ffprobe = "\"" + appDir + "ffmpeg\\bin\\ffprobe.exe" + "\"";
                }
                else if (!File.Exists(appDir + "ffmpeg\\bin\\ffprobe.exe"))
                {
                    //use system installed binaries
                    FFprobe.ffprobe = "ffprobe";
                }
            }
            // Use User Custom Path
            else
            {
                FFprobe.ffprobe = "\"" + ConfigureWindow.ffprobePath + "\"";
            }

            // Return Value
            //return FFprobe.ffprobe;
        }


        /// <summary>
        ///    Thread Detect (Method)
        /// </summary>
        public static String ThreadDetect(MainWindow mainwindow)
        {
            // check threads from configure
            threads = ConfigureWindow.threads;

            // set threads
            if (ConfigureWindow.threads == "off")
            {
                threads = string.Empty;
            }
            else if (ConfigureWindow.threads == "all" || string.IsNullOrEmpty(ConfigureWindow.threads))
            {
                threads = "-threads " + maxthreads;
            }
            else
            {
                threads = "-threads " + threads;
            }

            // Return Value
            return threads;
        }



        /// <summary>
        ///    Input Directory (Method)
        /// </summary>
        // Directory Only, Needed for Batch
        public static String BatchInputDirectory(MainWindow mainwindow)
        {
            // -------------------------
            // Batch
            // -------------------------
            if (mainwindow.tglBatch.IsChecked == true)
            {
                inputDir = mainwindow.tbxInput.Text; // (eg. C:\Input Folder\)
            }

            // -------------------------
            // Empty
            // -------------------------
            // Input Textbox & Output Textbox Both Empty
            if (string.IsNullOrWhiteSpace(mainwindow.tbxInput.Text))
            {
                inputDir = string.Empty;
            }


            // Return Value
            return inputDir;
        }



        /// <summary>
        ///    Input Path (Method)
        /// </summary>
        public static String InputPath(MainWindow mainwindow)
        {
            // -------------------------
            // Single File
            // -------------------------
            if (mainwindow.tglBatch.IsChecked == false)
            {
                // Input Directory
                // If not Empty
                if (!string.IsNullOrWhiteSpace(mainwindow.tbxInput.Text))
                {
                    inputDir = Path.GetDirectoryName(mainwindow.tbxInput.Text).TrimEnd('\\') + @"\"; // (eg. C:\Input Folder\)
                }

                // Input
                input = mainwindow.tbxInput.Text; // (eg. C:\Input Folder\file.wmv)
            }

            // -------------------------
            // Batch
            // -------------------------
            else if (mainwindow.tglBatch.IsChecked == true)
            {
                // Add slash to Batch Browse Text folder path if missing
                mainwindow.tbxInput.Text = mainwindow.tbxInput.Text.TrimEnd('\\') + @"\";

                inputDir = mainwindow.tbxInput.Text; // (eg. C:\Input Folder\)

                inputFileName = "%~f";

                // Input
                input = inputDir + inputFileName; // (eg. C:\Input Folder\)
            }

            // -------------------------
            // Empty
            // -------------------------
            // Input Textbox & Output Textbox Both Empty
            if (string.IsNullOrWhiteSpace(mainwindow.tbxInput.Text))
            {
                inputDir = string.Empty;
                inputFileName = string.Empty;
                input = string.Empty;
            }


            // Return Value
            return input;
        }



        /// <summary>
        ///    Output Path (Method)
        /// </summary>
        public static String OutputPath(MainWindow mainwindow)
        {
            // Get Output Extension (Method)
            FormatControls.OutputFormatExt(mainwindow);

            // -------------------------
            // Single File
            // -------------------------
            if (mainwindow.tglBatch.IsChecked == false)
            {
                // Input Not Empty, Output Empty
                // Default Output to be same as Input Directory
                if (!string.IsNullOrWhiteSpace(mainwindow.tbxInput.Text) && string.IsNullOrWhiteSpace(mainwindow.tbxOutput.Text))
                {
                    mainwindow.tbxOutput.Text = inputDir + inputFileName + outputExt;
                }

                // Input Empty, Output Not Empty
                // Output is Output
                if (!string.IsNullOrWhiteSpace(mainwindow.tbxOutput.Text))
                {
                    outputDir = Path.GetDirectoryName(mainwindow.tbxOutput.Text).TrimEnd('\\') + @"\";

                    outputFileName = Path.GetFileNameWithoutExtension(mainwindow.tbxOutput.Text);
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
                    outputFileName = mainwindow.FileRenamer(inputFileName);
                }

                // -------------------------
                // Image Sequence Renamer
                // -------------------------
                if ((string)mainwindow.cboMediaType.SelectedItem == "Sequence")
                {
                    outputFileName = "image-%03d"; //must be this name
                }

                // -------------------------
                // Output
                // -------------------------
                output = outputDir + outputFileName + outputExt; // (eg. C:\Output Folder\ + file + .mp4)    
            }

            // -------------------------
            // Batch
            // -------------------------
            else if (mainwindow.tglBatch.IsChecked == true)
            {
                // Add slash to Batch Output Text folder path if missing
                mainwindow.tbxOutput.Text = mainwindow.tbxOutput.Text.TrimEnd('\\') + @"\";

                // Input Not Empty, Output Empty
                // Default Output to be same as Input Directory
                if (!string.IsNullOrWhiteSpace(mainwindow.tbxInput.Text) && string.IsNullOrWhiteSpace(mainwindow.tbxOutput.Text))
                {
                    mainwindow.tbxOutput.Text = mainwindow.tbxInput.Text;
                }

                outputDir = mainwindow.tbxOutput.Text;

                // Output             
                output = outputDir + "%~nf" + outputExt; // (eg. C:\Output Folder\%~nf.mp4)
            }

            // -------------------------
            // Empty
            // -------------------------
            // Input Textbox & Output Textbox Both Empty
            if (string.IsNullOrWhiteSpace(mainwindow.tbxOutput.Text))
            {
                outputDir = string.Empty;
                outputFileName = string.Empty;
                output = string.Empty;
            }


            // Return Value
            return output;
        }



        /// <summary>
        ///    Batch Extension Period Check (Method)
        /// </summary>
        public static void BatchExtCheck(MainWindow mainwindow)
        {
            // Add period if Batch Extension if User did not enter
            if (!mainwindow.batchExtensionTextBox.Text.Contains(".") 
                && mainwindow.batchExtensionTextBox.Text != "extension")
            {
                mainwindow.batchExtensionTextBox.Text = "." + mainwindow.batchExtensionTextBox.Text;
            }

            // Clear Batch Extension Text if Only period
            if (mainwindow.batchExtensionTextBox.Text == ".")
            {
                mainwindow.batchExtensionTextBox.Text = "";
                batchExt = "";
            }
        }


        /// <summary>
        ///    Delete 2 Pass Logs Lock Check (Method)
        /// </summary>
        /// <remarks>
        ///     Check if File is in use by another Process (FFmpeg writing 2 Pass log)
        /// </remarks>
        //protected virtual bool IsFileLocked(FileInfo file)
        //{
        //    FileStream stream = null;

        //    try
        //    {
        //        stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        //    }
        //    catch (IOException)
        //    {
        //        //the file is unavailable because it is:
        //        //still being written to
        //        //or being processed by another thread
        //        //or does not exist (has already been processed)
        //        return true;
        //    }
        //    finally
        //    {
        //        if (stream != null)
        //            stream.Close();
        //    }

        //    //file is not locked
        //    return false;
        //}



        /// <summary>
        ///    File Renamer (Method)
        /// </summary>
        public String FileRenamer(string filename)
        {
            string outputNewFileName = string.Empty;
            string output = outputDir + filename + outputExt;

            int count = 1;

            if (File.Exists(outputDir + filename + outputExt))
            {
                while (File.Exists(output))
                {
                    outputNewFileName = string.Format("{0}({1})", filename, count++);
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
        ///    Ready Halts (Method)
        /// </summary>
        public static void ReadyHalts(MainWindow mainwindow)
        {
            // Check if FFmpeg & FFprobe Exists
            //
            if (ffCheckCleared == false)
            {
                mainwindow.FFcheck();
            }

            // Do not allow Auto without FFprobe being installed or linked
            //
            if (string.IsNullOrEmpty(FFprobe.ffprobe))
            {
                if ((string)mainwindow.cboVideo.SelectedItem == "Auto" || (string)mainwindow.cboAudio.SelectedItem == "Auto")
                {
                    // Log Console Message /////////
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Auto Quality Mode Requires FFprobe in order to Detect File Info.")) { Foreground = Log.ConsoleWarning });

                    /* lock */
                    ready = false;
                    MessageBox.Show("Auto Quality Mode Requires FFprobe in order to Detect File Info.");
                }
            }

            // Do not allow Script to generate if Browse Empty & Auto, since there is no file to detect bitrates/codecs
            //
            if (mainwindow.tglBatch.IsChecked == false) // Ignore if Batch
            {
                if (string.IsNullOrWhiteSpace(mainwindow.tbxInput.Text)) // empty check
                {
                    if ((string)mainwindow.cboVideo.SelectedItem == "Auto" || (string)mainwindow.cboAudio.SelectedItem == "Auto")
                    {
                        if ((string)mainwindow.cboVideoCodec.SelectedItem != "Copy" || (string)mainwindow.cboAudioCodec.SelectedItem != "Copy")
                        {
                            // Log Console Message /////////
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Auto Mode needs an input file in order to detect settings.")) { Foreground = Log.ConsoleWarning });

                            /* lock */
                            ready = false;
                            script = false;
                            // Warning
                            MessageBox.Show("Auto Mode needs an input file in order to detect settings.");
                        }
                    }
                }
            }

            // STOP if Single File Input with no Extension
            //
            if (mainwindow.tglBatch.IsChecked == false && mainwindow.tbxInput.Text.EndsWith("\\"))
            {
                // Log Console Message /////////
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Please choose an input file.")) { Foreground = Log.ConsoleWarning });

                /* lock */
                ready = false;
                // Warning
                MessageBox.Show("Please choose an input file.");
            }

            // STOP Do not allow Batch Copy to same folder if file extensions are the same (to avoid file overwrite)
            //
            if (mainwindow.tglBatch.IsChecked == true 
                && string.Equals(inputDir, outputDir, StringComparison.CurrentCultureIgnoreCase) 
                && string.Equals(batchExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
            {
                // Log Console Message /////////
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Please choose an output folder different than the input folder to avoid file overwrite.")) { Foreground = Log.ConsoleWarning });

                /* lock */
                ready = false;
                // Warning
                MessageBox.Show("Please choose an output folder different than the input folder to avoid file overwrite.");
            }

            // STOP Throw Error if VP8/VP9 & CRF does not have Bitrate -b:v
            //
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" || (string)mainwindow.cboVideoCodec.SelectedItem == "VP9")
            {
                if (!string.IsNullOrWhiteSpace(mainwindow.crfCustom.Text) 
                    && string.IsNullOrWhiteSpace(mainwindow.vBitrateCustom.Text))
                {
                    // Log Console Message /////////
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: VP8/VP9 CRF must also have Bitrate. \n(e.g. 0 for Constant, 1234k for Constrained)")) { Foreground = Log.ConsoleWarning });

                    /* lock */
                    ready = false;
                    // Notice
                    MessageBox.Show("Notice: VP8/VP9 CRF must also have Bitrate. \n(e.g. 0 for Constant, 1234k for Constrained)");
                }
            }
        }



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
                // Detect which screen we're on
                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                var thisScreen = allScreens.SingleOrDefault(s => this.Left >= s.WorkingArea.Left && this.Left < s.WorkingArea.Right);
                if (thisScreen == null) thisScreen = allScreens.First();

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
                infowindow.Left = Math.Max((this.Left + (this.Width - infowindow.Width) / 2), thisScreen.WorkingArea.Left);
                infowindow.Top = Math.Max((this.Top + (this.Height - infowindow.Height) / 2), thisScreen.WorkingArea.Top);

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
                infowindow.Left = Math.Max((this.Left + (this.Width - infowindow.Width) / 2), this.Left);
                infowindow.Top = Math.Max((this.Top + (this.Height - infowindow.Height) / 2), this.Top);

                // Open Window
                infowindow.Show();
            }
        }


        /// <summary>
        ///     Configure Settings Window Button
        /// </summary>
        private void buttonConfigure_Click(object sender, RoutedEventArgs e)
        {
            // Prevent Monitor Resolution Window Crash
            //
            try
            {
                // Detect which screen we're on
                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                var thisScreen = allScreens.SingleOrDefault(s => this.Left >= s.WorkingArea.Left && this.Left < s.WorkingArea.Right);
                if (thisScreen == null) thisScreen = allScreens.First();

                // Open Configure Window
                configurewindow = new ConfigureWindow(this);

                // Keep Window on Top
                configurewindow.Owner = Window.GetWindow(this);

                // Position Relative to MainWindow
                // Keep from going off screen
                configurewindow.Left = Math.Max((this.Left + (this.Width - configurewindow.Width) / 2), thisScreen.WorkingArea.Left);
                configurewindow.Top = Math.Max(this.Top - configurewindow.Height - 12, thisScreen.WorkingArea.Top);

                // Open Winndow
                configurewindow.ShowDialog();
            }
            // Simplified
            catch
            {
                // Open Configure Window
                configurewindow = new ConfigureWindow(this);

                // Keep Window on Top
                configurewindow.Owner = Window.GetWindow(this);

                // Position Relative to MainWindow
                configurewindow.Left = Math.Max((this.Left + (this.Width - configurewindow.Width) / 2), this.Left);
                configurewindow.Top = Math.Max((this.Top + (this.Height - configurewindow.Height) / 2), this.Top);

                // Open Winndow
                configurewindow.ShowDialog();
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
                var thisScreen = allScreens.SingleOrDefault(s => this.Left >= s.WorkingArea.Left && this.Left < s.WorkingArea.Right);
                if (thisScreen == null) thisScreen = allScreens.First();

                // Position Relative to MainWindow
                // Keep from going off screen
                logconsole.Left = Math.Min(this.Left + this.ActualWidth + 12, thisScreen.WorkingArea.Right - logconsole.Width);
                logconsole.Top = Math.Min(this.Top + 0, thisScreen.WorkingArea.Bottom - logconsole.Height);

                // Open Winndow
                logconsole.Show();
            }
            // Simplified
            catch
            {
                // Position Relative to MainWindow
                // Keep from going off screen
                logconsole.Left = this.Left + this.ActualWidth + 12;
                logconsole.Top = this.Top;

                // Open Winndow
                logconsole.Show();
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
                // Detect which screen we're on
                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                var thisScreen = allScreens.SingleOrDefault(s => this.Left >= s.WorkingArea.Left && this.Left < s.WorkingArea.Right);
                if (thisScreen == null) thisScreen = allScreens.First();

                // Check if Window is already open
                if (IsDebugConsoleOpened) return;

                // Start Window
                debugconsole = new DebugConsole(this, configurewindow);

                // Only allow 1 Window instance
                debugconsole.ContentRendered += delegate { IsDebugConsoleOpened = true; };
                debugconsole.Closed += delegate { IsDebugConsoleOpened = false; };

                // Position Relative to MainWindow
                // Keep from going off screen
                debugconsole.Left = Math.Max(this.Left - debugconsole.Width - 12, thisScreen.WorkingArea.Left);
                debugconsole.Top = Math.Max(this.Top - 0, thisScreen.WorkingArea.Top);

                // Write Variables to Debug Window (Method)
                DebugConsole.DebugWrite(debugconsole, this);

                // Open Window
                debugconsole.Show();
            }
            // Simplified
            catch
            {
                // Check if Window is already open
                if (IsDebugConsoleOpened) return;

                // Start Window
                debugconsole = new DebugConsole(this, configurewindow);

                // Only allow 1 Window instance
                debugconsole.ContentRendered += delegate { IsDebugConsoleOpened = true; };
                debugconsole.Closed += delegate { IsDebugConsoleOpened = false; };

                // Position Relative to MainWindow
                // Keep from going off screen
                debugconsole.Left = this.Left - debugconsole.Width - 12;
                debugconsole.Top = this.Top;

                // Write Variables to Debug Window (Method)
                DebugConsole.DebugWrite(debugconsole, this);

                // Open Window
                debugconsole.Show();
            }
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
                // Detect which screen we're on
                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                var thisScreen = allScreens.SingleOrDefault(s => this.Left >= s.WorkingArea.Left && this.Left < s.WorkingArea.Right);
                if (thisScreen == null) thisScreen = allScreens.First();

                // Check if Window is already open
                if (IsFilePropertiesOpened) return;

                // Start window
                //MainWindow mainwindow = this;
                filepropwindow = new FilePropertiesWindow(this);

                // Only allow 1 Window instance
                filepropwindow.ContentRendered += delegate { IsFilePropertiesOpened = true; };
                filepropwindow.Closed += delegate { IsFilePropertiesOpened = false; };

                // Position Relative to MainWindow
                // Keep from going off screen
                filepropwindow.Left = Math.Max((this.Left + (this.Width - filepropwindow.Width) / 2), thisScreen.WorkingArea.Left);
                filepropwindow.Top = Math.Max((this.Top + (this.Height - filepropwindow.Height) / 2), thisScreen.WorkingArea.Top);

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
                filepropwindow.Left = Math.Max((this.Left + (this.Width - filepropwindow.Width) / 2), this.Left);
                filepropwindow.Top = Math.Max((this.Top + (this.Height - filepropwindow.Height) / 2), this.Top);

                // Write Properties to Textbox in FilePropertiesWindow Initialize

                // Open Window
                filepropwindow.Show();
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
            // Proceed if Internet Connection
            //
            if (UpdateWindow.CheckForInternetConnection() == true)
            {
                // Parse GitHub .version file
                //
                string parseLatestVersion = string.Empty;

                try
                {
                    parseLatestVersion = UpdateWindow.wc.DownloadString("https://raw.githubusercontent.com/MattMcManis/Axiom/master/.version");
                }
                catch
                {
                    MessageBox.Show("GitHub version file not found.");
                }


                //Split Version & Build Phase by dash
                //
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
                        MessageBox.Show("Error reading version.");
                    }

                    // Debug
                    //MessageBox.Show(Convert.ToString(latestVersion));
                    //MessageBox.Show(latestBuildPhase);


                    // Check if Axiom is the Latest Version
                    // Update Available
                    if (latestVersion > currentVersion)
                    {
                        // Yes/No Dialog Confirmation
                        //
                        MessageBoxResult result = MessageBox.Show("v" + latestVersion + "-" + latestBuildPhase + "\n\nDownload Update?", "Update Available ", MessageBoxButton.YesNo);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                // Prevent Monitor Resolution Window Crash
                                //
                                try
                                {
                                    // Detect which screen we're on
                                    var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                                    var thisScreen = allScreens.SingleOrDefault(s => this.Left >= s.WorkingArea.Left && this.Left < s.WorkingArea.Right);
                                    if (thisScreen == null) thisScreen = allScreens.First();

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
                                    updatewindow.Left = Math.Max((this.Left + (this.Width - updatewindow.Width) / 2), thisScreen.WorkingArea.Left);
                                    updatewindow.Top = Math.Max((this.Top + (this.Height - updatewindow.Height) / 2), thisScreen.WorkingArea.Top);

                                    // Open Window
                                    updatewindow.Show();
                                }
                                // Simplified
                                catch
                                {
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
                                    updatewindow.Left = Math.Max((this.Left + (this.Width - updatewindow.Width) / 2), this.Left);
                                    updatewindow.Top = Math.Max((this.Top + (this.Height - updatewindow.Height) / 2), this.Top);

                                    // Open Window
                                    updatewindow.Show();
                                }
                                break;
                            case MessageBoxResult.No:
                                break;
                        }
                    }
                    // Update Not Available
                    else if (latestVersion <= currentVersion)
                    {
                        MessageBox.Show("This version is up to date.");
                    }
                    // Unknown
                    else // null
                    {
                        MessageBox.Show("Could not find download. Try updating manually.");
                    }
                }
                // Version is Null
                else
                {
                    MessageBox.Show("GitHub version file returned empty.");
                }
            }
            else
            {
                MessageBox.Show("Could not detect Internet Connection.");
            }
        }


        /// <summary>
        ///    Log Button
        /// </summary>
        private void buttonLog_Click(object sender, RoutedEventArgs e)
        {
            // Call Method to get Log Path
            Log.DefineLogPath(this, configurewindow);

            //MessageBox.Show(ConfigureWindow.logPath.ToString()); //debug

            // Open Log
            if (File.Exists(ConfigureWindow.logPath + "output.log"))
            {
                Process.Start("notepad.exe", "\"" + ConfigureWindow.logPath + "output.log" + "\"");
            }
            else
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Output Log has not been created yet.")) { Foreground = Log.ConsoleWarning });

                MessageBox.Show("Output Log has not been created yet.");
            }
        }


        /// <summary>
        ///    Script Button
        /// </summary>
        private void btnScript_Click(object sender, RoutedEventArgs e)
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
            // Enable Script
            // -------------------------
            script = true;

            // -------------------------
            // Reset Sort
            // -------------------------
            ScriptView.sort = false;

            // -------------------------
            // Keep FFmpeg Window Toggle
            // -------------------------
            //KeepWindow(this);

            // -------------------------
            // Batch Extention Period Check
            // -------------------------
            BatchExtCheck(this);

            // -------------------------
            // Set FFprobe Path
            // -------------------------
            FFprobePath(this);

            // -------------------------
            // Ready Halts
            // -------------------------
            ReadyHalts(this);


            // -------------------------
            // Background Thread Worker
            // -------------------------
            BackgroundWorker fileprocess = new BackgroundWorker();

            fileprocess.WorkerSupportsCancellation = true;
            fileprocess.WorkerReportsProgress = true;

            fileprocess.DoWork += new DoWorkEventHandler(delegate (object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;

                // Cross-Thread Communication
                this.Dispatcher.Invoke(() =>
                {
                    // -------------------------
                    // Single
                    // -------------------------
                    if (tglBatch.IsChecked == false)
                    {
                        // -------------------------
                        // FFprobe Detect Metadata
                        // -------------------------
                        FFprobe.Metadata(this);

                        // -------------------------
                        // FFmpeg Generate Arguments (Single)
                        // -------------------------
                        //disabled if batch
                        FFmpeg.FFmpegSingleGenerateArgs(this);
                    }

                    // -------------------------
                    // Batch
                    // -------------------------
                    else if (tglBatch.IsChecked == true)
                    {
                        // -------------------------
                        // FFprobe Video Entry Type Containers
                        // -------------------------
                        //FFprobe.VideoEntryTypeBatch(this);
                        FFprobe.VideoEntryType(this);

                        // -------------------------
                        // FFprobe Video Entry Type Containers
                        // -------------------------
                        //FFprobe.AudioEntryTypeBatch(this);
                        FFprobe.AudioEntryType(this);

                        // -------------------------
                        // FFmpeg Generate Arguments (Batch)
                        // -------------------------
                        //disabled if single file
                        FFmpeg.FFmpegBatchGenerateArgs(this);
                    }

                    // -------------------------
                    // Write All Log Actions to Console
                    // -------------------------
                    Log.LogWriteAll(this, configurewindow);


                }); //end dispatcher

            }); //end thread


            // When background worker completes task
            fileprocess.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate(object o, RunWorkerCompletedEventArgs args)
            {
                // -------------------------
                // Generate Script
                // -------------------------
                FFmpeg.FFmpegScript(this, scriptview);

                // -------------------------
                // Close the Background Worker
                // -------------------------
                fileprocess.CancelAsync();
                fileprocess.Dispose();

                // -------------------------
                // Clear Variables for next Run
                // -------------------------
                ClearVariables(this);
                GC.Collect();

            }); //end worker completed task


            // -------------------------
            // Background Worker Run Async
            // -------------------------
            fileprocess.RunWorkerAsync();
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
        ///    Keep Window Toggle Checked
        /// </summary>
        private void tglWindowKeep_Checked(object sender, RoutedEventArgs e)
        {
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Keep FFmpeg Window Toggle: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run("On") { Foreground = Log.ConsoleDefault });

            //Prevent Loading Corrupt App.Config
            try
            {
                // Save Toggle Settings
                // must be done this way or you get "convert object to bool error"
                if (tglWindowKeep.IsChecked == true)
                {
                    Settings.Default.KeepWindow = true;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
                else if (tglWindowKeep.IsChecked == false)
                {
                    Settings.Default.KeepWindow = false;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
            }
            catch (ConfigurationErrorsException ex)
            {
                // Delete Old App.Config
                string filename = ex.Filename;

                if (File.Exists(filename) == true)
                {
                    File.Delete(filename);
                    Settings.Default.Upgrade();
                    // Properties.Settings.Default.Reload();
                }
                else
                {

                }
            }
        }
        /// <summary>
        ///    Keep Window Toggle Unchecked
        /// </summary>
        private void tglWindowKeep_Unchecked(object sender, RoutedEventArgs e)
        {
            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Keep FFmpeg Window Toggle: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run("Off") { Foreground = Log.ConsoleDefault });

            // Prevent Loading Corrupt App.Config
            try
            {
                // Save Toggle Settings
                // must be done this way or you get "convert object to bool error"
                if (tglWindowKeep.IsChecked == true)
                {
                    Settings.Default.KeepWindow = true;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
                else if (tglWindowKeep.IsChecked == false)
                {
                    Settings.Default.KeepWindow = false;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
            }
            catch (ConfigurationErrorsException ex)
            {
                // Delete Old App.Config
                string filename = ex.Filename;

                if (File.Exists(filename) == true)
                {
                    File.Delete(filename);
                    Settings.Default.Upgrade();
                    // Properties.Settings.Default.Reload();
                }
                else
                {

                }
            }
        }


        /// <summary>
        ///    Pass ComboBox
        /// </summary>
        private void cboPass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Pass Controls Method
            VideoControls.EncodingPass(this);
        }
        private void cboPass_DropDownClosed(object sender, EventArgs e)
        {
            // User willingly selected a Pass
            VideoControls.passUserSelected = true;
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

                MessageBox.Show("File does not yet exist.");
            }
        }


        /// <summary>
        ///    Browse Button
        /// </summary>
        private void buttonBrowse_Click(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Single File
            // -------------------------
            if (tglBatch.IsChecked == false)
            {
                // Open 'Select File'
                Microsoft.Win32.OpenFileDialog selectFile = new Microsoft.Win32.OpenFileDialog();

                selectFile.RestoreDirectory = true;

                // Show save file dialog box
                Nullable<bool> result = selectFile.ShowDialog();

                // Process dialog box
                if (result == true)
                {
                    // Display path and file in Output Textbox
                    tbxInput.Text = selectFile.FileName;

                    // Set Input Dir, Name, Ext
                    inputDir = Path.GetDirectoryName(tbxInput.Text).TrimEnd('\\') + @"\";

                    inputFileName = Path.GetFileNameWithoutExtension(tbxInput.Text);

                    inputExt = Path.GetExtension(tbxInput.Text);


                    // Add slash to inputDir path if missing
                    if (!inputDir.EndsWith("\\") && !string.IsNullOrEmpty(inputDir))
                    {
                        // inputDir += "\\";
                        inputDir = inputDir.TrimEnd('\\') + @"\";
                    }

                }

                // Set Video & Audio Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
                VideoControls.AutoCopyVideoCodec(this);
                AudioControls.AutoCopyAudioCodec(this);
            }
            // -------------------------
            // Batch
            // -------------------------
            else if (tglBatch.IsChecked == true)
            {
                // Open 'Batch Folder'
                System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

                // Popup Folder Browse Window
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    // Display Folder Path in Textbox
                    tbxInput.Text = folderBrowserDialog.SelectedPath.TrimEnd('\\') + @"\";

                    // Remove Double Slash in Root Dir, such as C:\
                    tbxInput.Text = tbxInput.Text.Replace(@"\\", @"\");

                    // Add slash to Batch Browse Text folder path if missing
                    if (!string.IsNullOrWhiteSpace(tbxInput.Text) && !tbxInput.Text.EndsWith("\\"))
                    {
                        tbxInput.Text = tbxInput.Text.TrimEnd('\\') + @"\";
                    }

                    // Input Directory Path
                    inputDir = Path.GetDirectoryName(tbxInput.Text).TrimEnd('\\') + @"\";

                    // Add slash to inputDir path if missing
                    if (!inputDir.EndsWith("\\") && !string.IsNullOrEmpty(inputDir))
                    {
                        // inputDir += "\\";
                        inputDir = inputDir.TrimEnd('\\') + @"\";
                    }
                }
                else
                {
                    // Prevent Losing Codec Copy after cancel closing Browse Folder Dialog Box 
                    //
                    // Set Video & Audio Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
                    VideoControls.AutoCopyVideoCodec(this);
                    AudioControls.AutoCopyAudioCodec(this);
                }

                // Prevent Losing Codec Copy after cancel closing Browse Folder Dialog Box 
                //
                // Set Video & Audio Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
                VideoControls.AutoCopyVideoCodec(this);
                AudioControls.AutoCopyAudioCodec(this);
            }
        }


        /// <summary>
        ///    Browse Textbox
        /// </summary>
        private void tbxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Remove stray slash if closed out early (duplicate code?)
            if (tbxInput.Text == "\\")
            {
                tbxInput.Text = string.Empty;
            }

            // Get input file extension
            inputExt = Path.GetExtension(tbxInput.Text);


            // Enable / Disable "Open Input Location" Buttion
            if (!string.IsNullOrWhiteSpace(tbxInput.Text))
            {
                bool exists = Directory.Exists(Path.GetDirectoryName(tbxInput.Text));

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
            VideoControls.AutoCopyVideoCodec(this);
            AudioControls.AutoCopyAudioCodec(this);
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
        private void buttonOutput_Click(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Single File
            // -------------------------
            if (tglBatch.IsChecked == false)
            {
                // Get Output Ext
                FormatControls.OutputFormatExt(this);


                // Open 'Save File'
                Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();

                // 'Save File' Default Path same as Input Directory
                saveFile.InitialDirectory = inputDir;
                saveFile.RestoreDirectory = true;
                saveFile.DefaultExt = outputExt;

                // Default file name if empty
                if (string.IsNullOrEmpty(inputFileName))
                {
                    // Default
                    saveFile.FileName = "File";
                }
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


                // Show save file dialog box
                Nullable<bool> result = saveFile.ShowDialog();

                // Process dialog box
                if (result == true)
                {
                    // Display path and file in Output Textbox
                    tbxOutput.Text = saveFile.FileName;

                    // Output Path
                    outputDir = Path.GetDirectoryName(tbxOutput.Text).TrimEnd('\\') + @"\";

                    // Output Filename (without extension)
                    outputFileName = Path.GetFileNameWithoutExtension(tbxOutput.Text);

                    // Add slash to inputDir path if missing
                    if (!outputDir.EndsWith("\\") && !string.IsNullOrEmpty(outputDir))
                    {
                        // inputDir += "\\";
                        outputDir = outputDir.TrimEnd('\\') + @"\";
                    }
                }
            }
            // -------------------------
            // Batch
            // -------------------------
            else if (tglBatch.IsChecked == true)
            {
                // Open 'Select Folder'
                System.Windows.Forms.FolderBrowserDialog outputFolder = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = outputFolder.ShowDialog();

                // Process dialog box
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    // Display path and file in Output Textbox
                    tbxOutput.Text = outputFolder.SelectedPath.TrimEnd('\\') + @"\";

                    // Remove Double Slash in Root Dir, such as C:\
                    tbxOutput.Text = tbxOutput.Text.Replace(@"\\", @"\");


                    // Output Path
                    outputDir = Path.GetDirectoryName(tbxOutput.Text).TrimEnd('\\') + @"\";

                    // Add slash to inputDir path if missing
                    if (!outputDir.EndsWith("\\") && !string.IsNullOrEmpty(outputDir))
                    {
                        // inputDir += "\\";
                        outputDir = outputDir.TrimEnd('\\') + @"\";
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
            if (tbxOutput.Text == "\\")
            {
                tbxOutput.Text = string.Empty;
            }

            // Enable / Disable "Open Output Location" Buttion
            if (!string.IsNullOrWhiteSpace(tbxOutput.Text))
            {
                bool exists = Directory.Exists(Path.GetDirectoryName(tbxOutput.Text));

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
        ///     Video Bitrate Custom Number Textbox
        /// </summary>
        // Got Focus
        private void vBitrateCustom_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear Textbox on first use
            if (vBitrateCustom.Text == "Bitrate")
            {
                TextBox tbvb = (TextBox)sender;
                tbvb.Text = string.Empty;
                tbvb.GotFocus += vBitrateCustom_GotFocus; //used to be -=
            }
        }
        // Lost Focus
        private void vBitrateCustom_LostFocus(object sender, RoutedEventArgs e)
        {
            // Change Textbox back to Bitrate
            TextBox tbvb = sender as TextBox;
            if (tbvb.Text.Trim().Equals(string.Empty))
            {
                tbvb.Text = "Bitrate";
                tbvb.GotFocus -= vBitrateCustom_GotFocus; //used to be +=

                //vBitrateCustom.Foreground = TextBoxDarkBlue;
            }
        }


        /// <summary>
        ///     Video CRF Custom Number Textbox
        /// </summary>
        private void crfCustom_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers or Backspace
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }
        // Got Focus
        private void crfCustom_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear Textbox on first use
            if (crfCustom.Text == "CRF")
            {
                TextBox tbcrf = (TextBox)sender;
                tbcrf.Text = string.Empty;
                tbcrf.GotFocus += crfCustom_GotFocus; //used to be -=
            }
        }
        // Lost Focus
        private void crfCustom_LostFocus(object sender, RoutedEventArgs e)
        {
            // Change Textbox back to CRF
            TextBox tbcrf = sender as TextBox;
            if (tbcrf.Text.Trim().Equals(string.Empty))
            {
                tbcrf.Text = "CRF";
                tbcrf.GotFocus -= crfCustom_GotFocus; //used to be +=
            }
        }


        /// <summary>
        ///     FPS ComboBox
        /// </summary>
        private void cboFPS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Custom ComboBox Editable
            if ((string)cboFPS.SelectedItem == "Custom" || string.IsNullOrEmpty((string)cboFPS.SelectedItem))
            {
                cboFPS.IsEditable = true;
            }

            // Other Items Disable Editable
            if ((string)cboFPS.SelectedItem != "Custom" && !string.IsNullOrEmpty((string)cboFPS.SelectedItem))
            {
                cboFPS.IsEditable = false;
            }

            // Maintain Editable Combobox while typing
            if (cboFPS.IsEditable == true)
            {
                cboFPS.IsEditable = true;

                // Clear Custom Text
                cboFPS.SelectedIndex = -1;
            }

            // Disable Copy on change
            VideoControls.AutoCopyVideoCodec(this);

        }


        /// <summary>
        ///     Audio Custom Bitrate kbps Textbox
        /// </summary>
        private void audioCustom_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers or Backspace
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }
        // Got Focus
        private void audioCustom_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear Textbox on first use
            if (audioCustom.Text == "kbps")
            {
                TextBox tbac = (TextBox)sender;
                tbac.Text = string.Empty;
                tbac.GotFocus += audioCustom_GotFocus; //used to be -=
            }
        }
        // Lost Focus
        private void audioCustom_LostFocus(object sender, RoutedEventArgs e)
        {
            // Change Textbox back to kbps
            TextBox tbac = sender as TextBox;
            if (tbac.Text.Trim().Equals(string.Empty))
            {
                tbac.Text = "kbps";
                tbac.GotFocus -= audioCustom_GotFocus; //used to be +=
            }
        }


        /// <summary>
        ///     Samplerate ComboBox
        /// </summary>
        private void cboSamplerate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Switch to Copy if inputExt & outputExt match
            AudioControls.AutoCopyAudioCodec(this);
        }


        /// <summary>
        ///     Bit Depth ComboBox
        /// </summary>
        private void cboBitDepth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Switch to Copy if inputExt & outputExt match
            AudioControls.AutoCopyAudioCodec(this);
        }


        /// <summary>
        ///    Volume TextBox Changed
        /// </summary>
        private void volumeUpDown_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Disable Volume instead of running AutoCopyAudioCodec each time 
            // This needs to be re-thought, calling method on every timer tick
            AudioControls.AutoCopyAudioCodec(this);
        }
        /// <summary>
        ///    Volume TextBox KeyDown
        /// </summary>
        private void volumeUpDown_KeyDown(object sender, KeyEventArgs e)
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
        private void volumeUpButton_Click(object sender, RoutedEventArgs e)
        {
            int value;
            int.TryParse(volumeUpDown.Text, out value);

            value += 1;
            volumeUpDown.Text = value.ToString();
        }
        // Up Button Each Timer Tick
        private void dispatcherTimerUp_Tick(object sender, EventArgs e)
        {
            int value;
            int.TryParse(volumeUpDown.Text, out value);

            value += 1;
            volumeUpDown.Text = value.ToString();
        }
        // Hold Up Button
        private void volumeUpButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Timer      
            dispatcherTimerUp.Interval = new TimeSpan(0, 0, 0, 0, 100); //100ms
            dispatcherTimerUp.Start();
        }
        // Up Button Released
        private void volumeUpButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Disable Timer
            dispatcherTimerUp.Stop();
        }
        // -------------------------
        // Down
        // -------------------------
        // Volume Down Button Click
        private void volumeDownButton_Click(object sender, RoutedEventArgs e)
        {
            int value;
            int.TryParse(volumeUpDown.Text, out value);

            value -= 1;
            volumeUpDown.Text = value.ToString();
        }
        // Down Button Each Timer Tick
        private void dispatcherTimerDown_Tick(object sender, EventArgs e)
        {
            int value;
            int.TryParse(volumeUpDown.Text, out value);

            value -= 1;
            volumeUpDown.Text = value.ToString();
        }
        // Hold Down Button
        private void volumeDownButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Timer      
            dispatcherTimerDown.Interval = new TimeSpan(0, 0, 0, 0, 100); //100ms
            dispatcherTimerDown.Start();
        }
        // Down Button Released
        private void volumeDownButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Disable Timer
            dispatcherTimerDown.Stop();
        }


        /// <summary>
        ///    Video Codec Combobox
        /// </summary>
        private void cboVideoCodec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoControls.VideoCodecControls(this);

            // Video Encoding Pass Controls Method
            VideoControls.EncodingPass(this);

            // Display Video Bit-rate in TextBox
            // Must be after EncodingPass
            VideoDisplayBitrate();
        }


        /// <summary>
        ///    Audio Codec Combobox
        /// </summary>
        private void cboAudioCodec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AudioControls.AudioCodecControls(this);
        }


        /// <summary>
        ///    Format Combobox
        /// </summary>
        private void cboFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Output Control Selections
            FormatControls.OuputFormatDefaults(this);

            // Get Output Extension
            FormatControls.OutputFormatExt(this);

            // Output ComboBox Options
            FormatControls.OutputFormat(this);

            // Change All MainWindow Items
            VideoControls.VideoCodecControls(this);
            AudioControls.AudioCodecControls(this);

            // File Renamer
            if (!string.IsNullOrEmpty(inputDir))
            {
                outputFileName = FileRenamer(inputFileName);
            }
            

            // Always Default Video to Auto if Input Ext matches Format Output Ext
            if ((string)cboVideo.SelectedItem != "Auto" 
                && string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
            {
                cboVideo.SelectedItem = "Auto";
            }
            // Always Default Video to Auto if Input Ext matches Format Output Ext
            if ((string)cboAudio.SelectedItem != "Auto" 
                && string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
            {
                cboAudio.SelectedItem = "Auto";
            }

            // Single File - Update Ouput Textbox with current Format extension
            if (tglBatch.IsChecked == false && !string.IsNullOrWhiteSpace(tbxOutput.Text))
            {
                tbxOutput.Text = outputDir + outputFileName + outputExt;
            }
            
        }


        /// <summary>
        ///    Media Type Combobox
        /// </summary>
        private void cboMediaType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FormatControls.MediaType(this); 
        }


        /// <summary>
        ///    Video Quality Combobox
        /// </summary>
        private void cboVideo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoControls.AutoCopyVideoCodec(this);

            //enable Video Custom
            if ((string)cboVideo.SelectedItem == "Custom")
            {
                crfCustom.IsEnabled = true;
                vBitrateCustom.IsEnabled = true;

                // Disable CRF for Theora
                if ((string)cboVideoCodec.SelectedItem == "Theora")
                {
                    crfCustom.IsEnabled = false;
                }
            }
            else
            {
                crfCustom.IsEnabled = false;
                //crfCustom.Text = "CRF";
                vBitrateCustom.IsEnabled = false;
                //vBitrateCustom.Text = "Bitrate";
            }

            // -------------------------
            // Set Encoding Speed
            // -------------------------
            if ((string)cboVideo.SelectedItem == "Auto") { cboSpeed.SelectedItem = "Medium"; }
            else if ((string)cboVideo.SelectedItem == "Lossless") { cboSpeed.SelectedItem = "Very Slow"; }
            else if ((string)cboVideo.SelectedItem == "Ultra") { cboSpeed.SelectedItem = "Slow"; }
            else if ((string)cboVideo.SelectedItem == "High") { cboSpeed.SelectedItem = "Medium"; }
            else if ((string)cboVideo.SelectedItem == "Medium") { cboSpeed.SelectedItem = "Medium"; }
            else if ((string)cboVideo.SelectedItem == "Low") { cboSpeed.SelectedItem = "Fast"; }
            else if ((string)cboVideo.SelectedItem == "Sub") { cboSpeed.SelectedItem = "Very Fast"; }
            else if ((string)cboVideo.SelectedItem == "Custom") { cboSpeed.SelectedItem = "Medium"; }

            // -------------------------
            // Pass Controls Method
            // -------------------------
            VideoControls.EncodingPass(this);

            // -------------------------
            // Pass - Default to CRF
            // -------------------------
            // Keep in Video SelectionChanged
            // If Video Not Auto and User Willingly Selected Pass is false
            if ((string)cboVideo.SelectedItem != "Auto" 
                && VideoControls.passUserSelected == false)
            {
                cboPass.SelectedItem = "CRF";
            }


            // -------------------------
            // Display Bit-rate in TextBox
            // -------------------------
            VideoDisplayBitrate();
            //if ((string)cboVideo.SelectedItem != "Auto"
            //    && (string)cboVideo.SelectedItem != "Lossless"
            //    && (string)cboVideo.SelectedItem != "Custom"
            //    && (string)cboVideo.SelectedItem != "None"
            //    && !string.IsNullOrEmpty((string)cboVideo.SelectedItem))
            //{
            //    //Video.vBitrate = string.Empty;
            //    //Video.crf = string.Empty;
            //    // TextBox Displayed placed at the end of VideoQuality Method
            //    Video.VideoQuality(this);

            //    // Display Bit-rate in TextBox
            //    if (!string.IsNullOrEmpty(Video.vBitrate))
            //    {
            //        vBitrateCustom.Text = Video.vBitrate;
            //    }
            //    if (!string.IsNullOrEmpty(Video.crf))
            //    {
            //        crfCustom.Text = Video.crf.Replace("-crf ", "");
            //    }
            //}
            //else
            //{
            //    vBitrateCustom.Text = "Bitrate";
            //    crfCustom.Text = "CRF";
            //}

        } // End Video Combobox


        /// <summary>
        ///    Video Display Bit-rate
        /// </summary>
        public void VideoDisplayBitrate()
        {
            if ((string)cboVideo.SelectedItem != "Auto"
                && (string)cboVideo.SelectedItem != "Lossless"
                && (string)cboVideo.SelectedItem != "Custom"
                && (string)cboVideo.SelectedItem != "None"
                && !string.IsNullOrEmpty((string)cboVideo.SelectedItem))
            {
                //Video.vBitrate = string.Empty;
                //Video.crf = string.Empty;
                // TextBox Displayed placed at the end of VideoQuality Method
                Video.VideoQuality(this);

                // Display Bit-rate in TextBox
                if (!string.IsNullOrEmpty(Video.vBitrate))
                {
                    vBitrateCustom.Text = Video.vBitrate;
                }
                if (!string.IsNullOrEmpty(Video.crf))
                {
                    crfCustom.Text = Video.crf.Replace("-crf ", "");
                }
            }
            else
            {
                vBitrateCustom.Text = "Bitrate";
                crfCustom.Text = "CRF";
            }
        }

        /// <summary>
        ///    Audio Quality Combobox
        /// </summary>
        private void cboAudio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Custom
            // -------------------------
            //enable Audio Custom
            if ((string)cboAudio.SelectedItem == "Custom")
            {
                audioCustom.IsEnabled = true;
            }
            else
            {
                audioCustom.IsEnabled = false;
                //audioCustom.Text = "kbps";
            }

            // -------------------------
            // Audio
            // -------------------------
            // Always Enable Audio for AAC codec
            if ((string)cboAudioCodec.SelectedItem == "AAC")
            {
                cboAudio.IsEnabled = true;
            }

            // -------------------------
            // Audio Codec
            // -------------------------
            // Always switch to ALAC if M4A is Lossless
            if ((string)cboFormat.SelectedItem == "m4a" && (string)cboAudio.SelectedItem == "Lossless")
            {
                cboAudioCodec.SelectedItem = "ALAC";
            }

            // -------------------------
            // VBR
            // -------------------------
            // Disable and Uncheck VBR if, Lossless or Mute
            if ((string)cboAudio.SelectedItem == "Lossless" 
                || (string)cboAudio.SelectedItem == "Mute")
            {
                tglVBR.IsEnabled = false;
                tglVBR.IsChecked = false;
            }

            // Disable VBR if AC3, ALAC, FLAC, PCM, Copy
            if ((string)cboAudioCodec.SelectedItem == "AC3"
                || (string)cboAudioCodec.SelectedItem == "ALAC"
                || (string)cboAudioCodec.SelectedItem == "FLAC"
                || (string)cboAudioCodec.SelectedItem == "PCM"
                || (string)cboAudioCodec.SelectedItem == "Copy")
            {
                tglVBR.IsEnabled = false;
            }
            // Enable VBR for Vorbis, Opus, LAME, AAC
            if ((string)cboAudioCodec.SelectedItem == "Vorbis" 
                || (string)cboAudioCodec.SelectedItem == "Opus" 
                || (string)cboAudioCodec.SelectedItem == "LAME" 
                || (string)cboAudioCodec.SelectedItem == "AAC")
            {
                tglVBR.IsEnabled = true;
            }

            // If AUTO, Check or Uncheck VBR
            if ((string)cboAudio.SelectedItem == "Auto")
            {
                if ((string)cboAudioCodec.SelectedItem == "Vorbis")
                {
                    tglVBR.IsChecked = true;
                }
                if ((string)cboAudioCodec.SelectedItem == "Opus" 
                    || (string)cboAudioCodec.SelectedItem == "AAC" 
                    || (string)cboAudioCodec.SelectedItem == "AC3" 
                    || (string)cboAudioCodec.SelectedItem == "LAME" 
                    || (string)cboAudioCodec.SelectedItem == "ALAC" 
                    || (string)cboAudioCodec.SelectedItem == "FLAC" 
                    || (string)cboAudioCodec.SelectedItem == "PCM" 
                    || (string)cboAudioCodec.SelectedItem == "Copy")
                {
                    tglVBR.IsChecked = false;
                }
            }

            // Quality VBR Override
            // Disable / Enable VBR
            if ((string)cboAudio.SelectedItem == "Lossless" || (string)cboAudio.SelectedItem == "Mute")
            {
                tglVBR.IsEnabled = false;
            }

            // Call Method (Needs to be at this location)
            // Set Audio Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Audio Quality is Auto
            AudioControls.AutoCopyAudioCodec(this);


            // -------------------------
            // Display Bit-rate in TextBox
            // -------------------------
            if ((string)cboAudio.SelectedItem != "Auto"
                && (string)cboAudio.SelectedItem != "Lossless"
                && (string)cboAudio.SelectedItem != "Custom"
                && (string)cboAudio.SelectedItem != "Mute"
                && (string)cboAudio.SelectedItem != "None"
                && !string.IsNullOrEmpty((string)cboAudio.SelectedItem))
            {
                audioCustom.Text = cboAudio.SelectedItem.ToString() + "k";
            }
            else
            {
                audioCustom.Text = "kbps";
            }

        } // End audio_SelectionChanged



        /// <summary>
        ///    Size Combobox
        /// </summary>
        private void cboSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Set Video Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
            VideoControls.AutoCopyVideoCodec(this);

            // Enable Aspect Custom
            if ((string)cboSize.SelectedItem == "Custom")
            {
                widthCustom.IsEnabled = true;
                heightCustom.IsEnabled = true;

                widthCustom.Text = "width";
                heightCustom.Text = "height";
            }
            else
            {
                widthCustom.IsEnabled = false;
                heightCustom.IsEnabled = false;
                widthCustom.Text = "width";
                heightCustom.Text = "height";
            }

            // Change TextBox Resolution numbers
            if ((string)cboSize.SelectedItem == "No")
            {
                widthCustom.Text = "width";
                heightCustom.Text = "height";
            }
            else if ((string)cboSize.SelectedItem == "8K")
            {
                widthCustom.Text = "7680";
                heightCustom.Text = "auto";
            }
            else if ((string)cboSize.SelectedItem == "4K")
            {
                widthCustom.Text = "4096";
                heightCustom.Text = "auto";
            }
            else if ((string)cboSize.SelectedItem == "4K UHD")
            {
                widthCustom.Text = "3840";
                heightCustom.Text = "auto";
            }
            else if ((string)cboSize.SelectedItem == "2K")
            {
                widthCustom.Text = "2048";
                heightCustom.Text = "auto";
            }
            else if ((string)cboSize.SelectedItem == "1440p")
            {
                widthCustom.Text = "auto";
                heightCustom.Text = "1440";
            }
            else if ((string)cboSize.SelectedItem == "1200p")
            {
                widthCustom.Text = "auto";
                heightCustom.Text = "1200";
            }
            else if ((string)cboSize.SelectedItem == "1080p")
            {
                widthCustom.Text = "auto";
                heightCustom.Text = "1080";
            }
            else if ((string)cboSize.SelectedItem == "720p")
            {
                widthCustom.Text = "auto";
                heightCustom.Text = "720";
            }
            else if ((string)cboSize.SelectedItem == "480p")
            {
                widthCustom.Text = "auto";
                heightCustom.Text = "480";
            }
            else if ((string)cboSize.SelectedItem == "320p")
            {
                widthCustom.Text = "auto";
                heightCustom.Text = "320";
            }
            else if ((string)cboSize.SelectedItem == "240p")
            {
                widthCustom.Text = "auto";
                heightCustom.Text = "240";
            }
        }
        // -------------------------
        // Width Textbox Change
        // -------------------------
        // Got Focus
        private void widthCustom_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "width"
            if (widthCustom.Focus() == true && widthCustom.Text == "width")
            {
                widthCustom.Text = string.Empty;
            }
        }
        // Lost Focus
        private void widthCustom_LostFocus(object sender, RoutedEventArgs e)
        {
            // Change textbox back to "width" if left empty
            if (string.IsNullOrWhiteSpace(widthCustom.Text))
            {
                widthCustom.Text = "width";
            }
        }

        // -------------------------
        // Height Textbox Change
        // -------------------------
        // Got Focus
        private void heightCustom_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "width"
            if (heightCustom.Focus() == true && heightCustom.Text == "height")
            {
                heightCustom.Text = string.Empty;
            }
        }
        // Lost Focus
        private void heightCustom_LostFocus(object sender, RoutedEventArgs e)
        {
            // Change textbox back to "width" if left empty
            if (string.IsNullOrWhiteSpace(heightCustom.Text))
            {
                heightCustom.Text = "height";
            }
        }


        /// <summary>
        ///    Cut Combobox
        /// </summary>
        private void cboCut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FormatControls.CutControls(this); 
        }

        // -------------------------
        // Frame Start Textbox Change
        // -------------------------
        // Got Focus
        private void frameStart_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "width"
            if (frameStart.Focus() == true && frameStart.Text == "Frame")
            {
                frameStart.Text = string.Empty;
            }
        }
        // Lost Focus
        private void frameStart_LostFocus(object sender, RoutedEventArgs e)
        {
            // Change textbox back to "width" if left empty
            if (string.IsNullOrWhiteSpace(frameStart.Text))
            {
                frameStart.Text = "Frame";
            }
        }

        // -------------------------
        // Frame End Textbox Change
        // -------------------------
        // Got Focus
        private void frameEnd_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear textbox on focus if default text "width"
            if (frameEnd.Focus() == true && frameEnd.Text == "Range")
            {
                frameEnd.Text = string.Empty;
            }
        }
        // Lost Focus
        private void frameEnd_LostFocus(object sender, RoutedEventArgs e)
        {
            // Change textbox back to "width" if left empty
            if (string.IsNullOrWhiteSpace(frameEnd.Text))
            {
                frameEnd.Text = "Range";
            }
        }


        /// <summary>
        ///    Crop Window Button
        /// </summary>
        private void buttonCrop_Click(object sender, RoutedEventArgs e)
        {
            // Detect which screen we're on
            var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
            var thisScreen = allScreens.SingleOrDefault(s => this.Left >= s.WorkingArea.Left && this.Left < s.WorkingArea.Right);

            // Start Window
            cropwindow = new CropWindow(this);

            // Position Relative to MainWindow
            // Keep from going off screen
            cropwindow.Left = Math.Max((this.Left + (this.Width - cropwindow.Width) / 2), thisScreen.WorkingArea.Left);
            cropwindow.Top = Math.Max(this.Top - cropwindow.Height - 12, thisScreen.WorkingArea.Top);

            // Keep Window on Top
            cropwindow.Owner = Window.GetWindow(this);

            // Open Window
            cropwindow.ShowDialog();
        }


        /// <summary>
        ///    Crop Clear Button
        /// </summary>
        private void buttonCropClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cropwindow.textBoxCropWidth.Text = string.Empty;
                cropwindow.textBoxCropHeight.Text = string.Empty;
                cropwindow.textBoxCropX.Text = string.Empty;
                cropwindow.textBoxCropY.Text = string.Empty;

                CropWindow.crop = string.Empty;

                Video.vFilter = string.Empty;

                if (Video.VideoFilters != null)
                {
                    Video.VideoFilters.Clear();
                    Video.VideoFilters.TrimExcess();
                }

                // Trigger the CropWindow Clear Button (only way it will clear the string)
                cropwindow.buttonClear_Click(sender, e);
            }
            catch
            {

            }
        }


        /// <summary>
        ///    Presets
        /// </summary>
        private void cboPreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Presets.Preset(this); // Method
        }


        /// <summary>
        ///    Optimize Combobox
        /// </summary>
        private void cboOptimize_DropDownClosed(object sender, EventArgs e)
        {
            // Open Advanced Window
            if ((string)cboOptimize.SelectedItem == "Advanced")
            {
                // Detect which screen we're on
                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                var thisScreen = allScreens.SingleOrDefault(s => this.Left >= s.WorkingArea.Left && this.Left < s.WorkingArea.Right);

                // Start Window
                optadvwindow = new OptimizeAdvancedWindow(this);

                // Position Relative to MainWindow
                // Keep from going off screen
                optadvwindow.Left = Math.Max((this.Left + (this.Width - optadvwindow.Width) / 2), thisScreen.WorkingArea.Left);
                optadvwindow.Top = Math.Max((this.Top + (this.Height - optadvwindow.Height) / 2), thisScreen.WorkingArea.Top);

                // Keep Window on Top
                optadvwindow.Owner = Window.GetWindow(this);

                // Open Window
                optadvwindow.ShowDialog();
            }
        }
        private void cboOptimize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Disable Copy on change
            //VideoControls.AutoCopyVideoCodec(this); // this caused a loop error
        }


        /// <summary>
        ///    Audio Limiter Toggle
        /// </summary>
        private void tglAudioLimiter_Checked(object sender, RoutedEventArgs e)
        {
            // Enable Limit TextBox
            if (tglAudioLimiter.IsChecked == true)
            {
                audioLimiter.IsEnabled = true;
            }

            // Disable Audio Codec Copy
            AudioControls.AutoCopyAudioCodec(this);
        }
        private void tglAudioLimiter_Unchecked(object sender, RoutedEventArgs e)
        {
            // Disable Limit TextBox
            if (tglAudioLimiter.IsChecked == false)
            {
                audioLimiter.IsEnabled = false;
            }

            // Enable Audio Codec Copy if InputExt / outputExt match
            AudioControls.AutoCopyAudioCodec(this);
        }


        /// <summary>
        ///    Batch Extension Textbox
        /// </summary>
        private void batchExtension_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Remove Default Value
            if (batchExtensionTextBox.Text == "extension" 
                || string.IsNullOrWhiteSpace(batchExtensionTextBox.Text))
            {
                batchExt = string.Empty;
            }
            // Batch Extension Variable
            else
            {
                batchExt = batchExtensionTextBox.Text;
            }

            // Add period to batchExt if user did not enter (This helps enable Copy)
            if (!batchExt.StartsWith(".") && !string.IsNullOrWhiteSpace(batchExtensionTextBox.Text) 
                && batchExtensionTextBox.Text != "extension")
            {
                batchExt = "." + batchExt;
            }

            // Set Video and AudioCodec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
            VideoControls.AutoCopyVideoCodec(this);
            AudioControls.AutoCopyAudioCodec(this);
        }


        /// <summary>
        ///    Batch Toggle
        /// </summary>
        // Checked
        private void tglBatch_Checked(object sender, RoutedEventArgs e)
        {
            // Enable / Disable batch extension textbox
            if (tglBatch.IsChecked == true)
            {
                batchExtensionTextBox.IsEnabled = true;
                batchExtensionTextBox.Text = string.Empty;
            }

            // Clear Browse Textbox, Input Filename, Dir, Ext
            if (!string.IsNullOrWhiteSpace(tbxInput.Text))
            {
                tbxInput.Text = string.Empty;
                inputFileName = string.Empty;
                inputDir = string.Empty;
                inputExt = string.Empty;
            }

            // Clear Output Textbox, Output Filename, Dir, Ext
            if (!string.IsNullOrWhiteSpace(tbxOutput.Text))
            {
                tbxOutput.Text = string.Empty;
                outputFileName = string.Empty;
                outputDir = string.Empty;
                outputExt = string.Empty;
            }

        }
        // Unchecked
        private void tglBatch_Unchecked(object sender, RoutedEventArgs e)
        {
            // Enable / Disable batch extension textbox
            if (tglBatch.IsChecked == false)
            {
                batchExtensionTextBox.IsEnabled = false;
                batchExtensionTextBox.Text = "extension";
            }

            // Clear Browse Textbox, Input Filename, Dir, Ext
            if (!string.IsNullOrWhiteSpace(tbxInput.Text))
            {
                tbxInput.Text = string.Empty;
                inputFileName = string.Empty;
                inputDir = string.Empty;
                inputExt = string.Empty;
            }

            // Clear Output Textbox, Output Filename, Dir, Ext
            if (!string.IsNullOrWhiteSpace(tbxOutput.Text))
            {
                tbxOutput.Text = string.Empty;
                outputFileName = string.Empty;
                outputDir = string.Empty;
                outputExt = string.Empty;
            }

            // Set Video and AudioCodec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
            VideoControls.AutoCopyVideoCodec(this);
            AudioControls.AutoCopyAudioCodec(this);
        }





        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///    Convert Button
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Keep FFmpeg Window Toggle
            // -------------------------
            //KeepWindow(this);

            // -------------------------
            // Batch Extention Period Check
            // -------------------------
            BatchExtCheck(this);

            // -------------------------
            // Set FFprobe Path
            // -------------------------
            FFprobePath(this);

            // -------------------------
            // Ready Halts
            // -------------------------
            ReadyHalts(this); 


            // Log Console Message /////////
            if (/*script == false && */ready == true)
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
                // Background Thread Worker
                // -------------------------
                BackgroundWorker fileprocess = new BackgroundWorker();

                fileprocess.WorkerSupportsCancellation = true;
                fileprocess.WorkerReportsProgress = true;

                fileprocess.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;

                    // Cross-Thread Communication
                    this.Dispatcher.Invoke(() =>
                    {
                        // -------------------------
                        // Single
                        // -------------------------
                        if (tglBatch.IsChecked == false)
                        {
                            // -------------------------
                            // FFprobe Detect Metadata
                            // -------------------------
                            FFprobe.Metadata(this);

                            // -------------------------
                            // FFmpeg Generate Arguments (Single)
                            // -------------------------
                            //disabled if batch
                            FFmpeg.FFmpegSingleGenerateArgs(this);
                        }

                        // -------------------------
                        // Batch
                        // -------------------------
                        else if (tglBatch.IsChecked == true)
                        {
                            // -------------------------
                            // FFprobe Video Entry Type Containers
                            // -------------------------
                            //FFprobe.VideoEntryTypeBatch(this);
                            FFprobe.VideoEntryType(this);

                            // -------------------------
                            // FFprobe Video Entry Type Containers
                            // -------------------------
                            //FFprobe.AudioEntryTypeBatch(this);
                            FFprobe.AudioEntryType(this);

                            // -------------------------
                            // FFmpeg Generate Arguments (Batch)
                            // -------------------------
                            //disabled if single file
                            FFmpeg.FFmpegBatchGenerateArgs(this);
                        }

                        // -------------------------
                        // FFmpeg Convert
                        // -------------------------
                        FFmpeg.FFmpegConvert(this);


                    }); //end dispatcher

                }); //end thread


                // When background worker completes task
                //
                fileprocess.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate(object o, RunWorkerCompletedEventArgs args)
                {
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
                    Log.LogWriteAll(this, configurewindow);

                    // -------------------------
                    // Close the Background Worker
                    // -------------------------
                    fileprocess.CancelAsync();
                    fileprocess.Dispose();

                    // -------------------------
                    // Clear Strings for next Run
                    // -------------------------
                    ClearVariables(this);
                    GC.Collect();

                }); //end worker completed task


                // -------------------------
                // Background Worker Run Async
                // -------------------------
                fileprocess.RunWorkerAsync(); 
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
                Log.LogWriteAll(this, configurewindow);

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
                ClearVariables(this);
                GC.Collect();

            }

        } //end convert button


    }

}