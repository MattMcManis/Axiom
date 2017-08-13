using Axiom.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
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

/* ----------------------------------------------------------------------
    Axiom UI
    Copyright (C) 2017 Matt McManis
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

namespace Axiom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string TitleVersion {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Dispatcher
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
        public DebugConsole debugconsole; //pass data

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

        // Axiom Current Version
        public static Version currentVersion;
        // Axiom GitHub Latest Version
        public static Version latestVersion;
        // Alpha, Beta, Stable
        public static string currentBuildPhase;
        public static string latestBuildPhase;
        public static string[] splitVersionBuildPhase;

        // Locks
        public static int ready = 1; // If 1 allow conversion, else stop
        public static int script = 0; // If 0 run ffmpeg, if 1 run generate script
        public static int ffCheckCleared = 0; // If 1, FFcheck no longer has to run for each convert

        // System
        public static string threads; // CPU Threads
        public static string maxthreads; // All CPU Threads
        public static string currentDir = Directory.GetCurrentDirectory().TrimEnd('\\') + @"\";

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
        ///     Main Window Initialize
        /// </summary>
        // --------------------------------------------------------------------------------------------------------
        public MainWindow()
        {
            InitializeComponent();

            currentVersion = new Version("1.0.0.0");
            currentBuildPhase = "alpha";
            TitleVersion = "Axiom ~ FFmpeg UI (" + Convert.ToString(currentVersion) + "-" + currentBuildPhase + ")";
            DataContext = this;

            /// <summary>
            /// Start the Log Console (Hidden)
            /// </summary>
            // Start
            StartLogConsole();

            /// <summary>
            /// Start the File Queue (Hidden)
            /// </summary>
            // disabled
            //StartFileQueue(); 


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
            ///     Window & Components
            /// </summary>
            // -----------------------------------------------------------------
            // Set Min/Max Width/Height to prevent Tablets maximizing
            this.MinWidth = 615;
            this.MinHeight = 305;
            this.MaxWidth = 615;
            this.MaxHeight = 305;


            /// <summary>
            ///     ComboBox Defaults
            /// </summary>
            // Item Sources
            cboFormat.ItemsSource = Format.FormatItemSource;
            cboMediaType.ItemsSource = Format.MediaTypeItemSource;

            cboFormat.SelectedIndex = 0;
            cboFPS.SelectedIndex = 0;
            cboCut.SelectedIndex = 0;
            cboSize.SelectedIndex = 0;
            cboPreset.SelectedIndex = 0;


            /// <summary>
            ///     Startup Preset
            /// </summary>
            // Default Format is WebM
            if ((string)cboFormat.SelectedItem == "webm")
            {
                cboSubtitle.SelectedItem = "none";
                cboAudioStream.SelectedItem = "1";
                //cboOptimize.SelectedItem = "Web";
            }

            // Batch Extension Box Disabled
            batchExtensionTextBox.IsEnabled = false;


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
            // Load Theme
            // -------------------------
            ConfigureWindow.ConfigTheme(configurewindow);

            // Log Console Message /////////
            // Don't put in Configure Method, creates duplicate message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Theme: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(ConfigureWindow.theme) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load FFmpeg.exe Path
            // -------------------------
            ConfigureWindow.ConfigFFmpegPath(configurewindow);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("FFmpeg: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(ConfigureWindow.ffmpegPath) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load FFprobe.exe Path
            // -------------------------
            ConfigureWindow.ConfigFFprobePath(configurewindow);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("FFprobe: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(ConfigureWindow.ffprobePath) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load Log Enabled
            // -------------------------
            ConfigureWindow.ConfigLogCheckbox(configurewindow);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Log Enabled: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(Convert.ToString(ConfigureWindow.logEnable)) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load Log Path
            // -------------------------
            ConfigureWindow.ConfigLogPath(configurewindow);

            // Log Console Message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Log Path: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(ConfigureWindow.logPath) { Foreground = Log.ConsoleDefault });

            // -------------------------
            // Load Threads
            // -------------------------
            ConfigureWindow.ConfigThreads(configurewindow);

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
                    tglWindowKeep.IsChecked = false;
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
            // Dispatcher Tick
            // Volume Up/Down Button Timer Tick
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
        ///     Close / Exit (Method)
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            // Force Exit All Executables
            base.OnClosed(e);
            Application.Current.Shutdown();
        }


        /// <summary>
        ///     Clear Variables (Method)
        /// </summary>
        public static void ClearVariables(MainWindow mainwindow)
        {
            // FFmpeg
            FFmpeg.cmdWindow = string.Empty;

            // FFprobe
            FFprobe.argsVideoCodec = string.Empty;
            FFprobe.argsAudioCodec = string.Empty;
            FFprobe.argsVideoBitrate = string.Empty;
            FFprobe.argsAudioBitrate = string.Empty;
            FFprobe.argsSize = string.Empty;
            FFprobe.argsDuration = string.Empty;
            FFprobe.argsFramerate = string.Empty;

            FFprobe.inputVideoCodec = string.Empty;
            FFprobe.inputVideoBitrate = string.Empty;
            FFprobe.inputAudioCodec = string.Empty;
            FFprobe.inputAudioBitrate = string.Empty;
            FFprobe.inputSize = string.Empty;
            FFprobe.inputDuration = string.Empty;
            FFprobe.inputFramerate = string.Empty;

            FFprobe.vEntryType = string.Empty;
            FFprobe.aEntryType = string.Empty;

            // Video
            //Video.v2PassSwitch = 0; // Set Two-Pass Switch back to Off to avoid doubling up   
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

            //Video.vFilterSwitch = 0; // Set vFilter Switch back to Off to avoid doubling up        
            Video.vFilter = string.Empty;
            Video.geq = string.Empty;
            Video.VideoFilters.Clear();
            Video.VideoFilters.TrimExcess();

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
            //Audio.aFilterSwitch = 0; //Set aFilter Switch back to Off to avoid doubling up
            Audio.aFilter = string.Empty;
            Audio.volume = string.Empty;
            Audio.aLimiter = string.Empty;
            Audio.AudioFilters.Clear();
            Audio.AudioFilters.TrimExcess();

            // Batch
            FFprobe.batchFFprobeAuto = string.Empty;
            Video.batchVideoAuto = string.Empty;
            Audio.batchAudioAuto = string.Empty;
            Audio.aBitrateLimiter = string.Empty;

            // Streams
            Streams.map = string.Empty;
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
                    if (File.Exists(currentDir + "ffmpeg\\bin\\ffmpeg.exe"))
                    {
                        // let pass
                        ffCheckCleared = 1;
                    }
                    else
                    {
                        int found = 0;

                        // Check Environment Variables
                        foreach (var envarPath in envar.Split(';'))
                        {
                            var exePath = System.IO.Path.Combine(envarPath, "ffmpeg.exe");
                            if (File.Exists(exePath)) { found = 1; }
                        }

                        if (found == 1)
                        {
                            // let pass
                            ffCheckCleared = 1;
                        }
                        else
                        {
                            /* lock */
                            ready = 0;
                            ffCheckCleared = 0;
                            MessageBox.Show("Cannot locate FFmpeg Path in Environment Variables or Current Folder.");
                        }

                    }
                }
                // If User Defined Path
                else if (ConfigureWindow.ffmpegPath != "<auto>" && ConfigureWindow.ffmpegPath != null && ConfigureWindow.ffmpegPath != string.Empty)
                {
                    var dirPath = System.IO.Path.GetDirectoryName(ConfigureWindow.ffmpegPath).TrimEnd('\\') + @"\";
                    var fullPath = System.IO.Path.Combine(dirPath, "ffmpeg.exe");

                    // Make Sure ffmpeg.exe Exists
                    if (File.Exists(fullPath))
                    {
                        // let pass
                        ffCheckCleared = 1;
                    }
                    else
                    {
                        /* lock */
                        ready = 0;
                        ffCheckCleared = 0;
                        MessageBox.Show("Cannot locate FFmpeg Path in User Defined Path.");
                    }

                    // If Configure Path is ffmpeg.exe and not another Program
                    if (string.Equals(ConfigureWindow.ffmpegPath, fullPath, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // let pass
                        ffCheckCleared = 1;
                    }
                    else
                    {
                        /* lock */
                        ready = 0;
                        ffCheckCleared = 0;
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
                    if (File.Exists(currentDir + "ffmpeg\\bin\\ffprobe.exe"))
                    {
                        // let pass
                        ffCheckCleared = 1;
                    }
                    else
                    {
                        int found = 0;

                        // Check Environment Variables
                        foreach (var envarPath in envar.Split(';'))
                        {
                            var exePath = System.IO.Path.Combine(envarPath, "ffprobe.exe");
                            if (File.Exists(exePath)) { found = 1; }
                        }

                        if (found == 1)
                        {
                            // let pass
                            ffCheckCleared = 1;
                        }
                        else
                        {
                            /* lock */
                            ready = 0;
                            ffCheckCleared = 0;
                            MessageBox.Show("Cannot locate FFprobe Path in Environment Variables or Current Folder.");
                        }

                    }
                }
                // If User Defined Path
                else if (ConfigureWindow.ffprobePath != "<auto>" && ConfigureWindow.ffprobePath != null && ConfigureWindow.ffprobePath != string.Empty)
                {
                    var dirPath = System.IO.Path.GetDirectoryName(ConfigureWindow.ffprobePath).TrimEnd('\\') + @"\";
                    var fullPath = System.IO.Path.Combine(dirPath, "ffprobe.exe");

                    // Make Sure ffprobe.exe Exists
                    if (File.Exists(fullPath))
                    {
                        // let pass
                        ffCheckCleared = 1;
                    }
                    else
                    {
                        /* lock */
                        ready = 0;
                        ffCheckCleared = 0;
                        MessageBox.Show("Cannot locate FFprobe Path in User Defined Path.");
                    }

                    // If Configure Path is FFmpeg.exe and not another Program
                    if (string.Equals(ConfigureWindow.ffprobePath, fullPath, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // let pass
                        ffCheckCleared = 1;
                    }
                    else
                    {
                        /* lock */
                        ready = 0;
                        ffCheckCleared = 0;
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
                if (File.Exists(currentDir + "ffmpeg\\bin\\ffmpeg.exe"))
                {
                    //use included binary
                    FFmpeg.ffmpeg = "\"" + currentDir + "ffmpeg\\bin\\ffmpeg.exe" + "\"";
                }
                else if (!File.Exists(currentDir + "ffmpeg\\bin\\ffmpeg.exe"))
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
        public static String FFprobePath(MainWindow mainwindow)
        {
            // If Configure FFprobe Path is <auto>
            if (ConfigureWindow.ffprobePath == "<auto>")
            {
                if (File.Exists(currentDir + "ffmpeg\\bin\\ffprobe.exe"))
                {
                    //use included binary
                    FFprobe.ffprobe = "\"" + currentDir + "ffmpeg\\bin\\ffprobe.exe" + "\"";
                }
                else if (!File.Exists(currentDir + "ffmpeg\\bin\\ffprobe.exe"))
                {
                    //use system installed binaries
                    //ffprobe = "\"" + "ffprobe" + "\"";

                    FFprobe.ffprobe = "ffprobe";
                }
            }
            // Use User Custom Path
            else
            {
                FFprobe.ffprobe = "\"" + ConfigureWindow.ffprobePath + "\"";
            }

            // Return Value
            return FFprobe.ffprobe;
        }


        /// <summary>
        ///    Keep FFmpegWindow Switch (Method)
        /// </summary>
        /// <remarks>
        ///     CMD.exe command, /k = keep, /c = close
        ///     Do not .Close(); if using /c, it will throw a Dispose exception
        /// </remarks>
        public static void KeepWindow(MainWindow mainwindow)
        {
            if (mainwindow.tglWindowKeep.IsChecked == true)
            {
                FFmpeg.cmdWindow = "/k ";
            }
            else
            {
                FFmpeg.cmdWindow = "/c ";
            }
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
            else if (ConfigureWindow.threads == "all" || string.IsNullOrEmpty(ConfigureWindow.threads) /* || ConfigureWindow.threads == null */)
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
                inputDir = mainwindow.textBoxBrowse.Text; // (eg. C:\Input Folder\)
            }

            // -------------------------
            // Empty
            // -------------------------
            // Input Textbox & Output Textbox Both Empty
            if (string.IsNullOrWhiteSpace(mainwindow.textBoxBrowse.Text))
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
                if (!string.IsNullOrWhiteSpace(mainwindow.textBoxBrowse.Text))
                {
                    inputDir = System.IO.Path.GetDirectoryName(mainwindow.textBoxBrowse.Text).TrimEnd('\\') + @"\"; // (eg. C:\Input Folder\)
                }

                // Input
                input = mainwindow.textBoxBrowse.Text; // (eg. C:\Input Folder\file.wmv)
            }

            // -------------------------
            // Batch
            // -------------------------
            else if (mainwindow.tglBatch.IsChecked == true)
            {
                // Add slash to Batch Browse Text folder path if missing
                mainwindow.textBoxBrowse.Text = mainwindow.textBoxBrowse.Text.TrimEnd('\\') + @"\";

                inputDir = mainwindow.textBoxBrowse.Text; // (eg. C:\Input Folder\)

                inputFileName = "%~f";

                // Input
                input = inputDir + inputFileName; // (eg. C:\Input Folder\)
            }

            // -------------------------
            // Empty
            // -------------------------
            // Input Textbox & Output Textbox Both Empty
            if (string.IsNullOrWhiteSpace(mainwindow.textBoxBrowse.Text))
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
            Format.OutputFormatExt(mainwindow);

            // -------------------------
            // Single File
            // -------------------------
            if (mainwindow.tglBatch.IsChecked == false)
            {
                // Input Not Empty, Output Empty
                // Default Output to be same as Input Directory
                if (!string.IsNullOrWhiteSpace(mainwindow.textBoxBrowse.Text) && string.IsNullOrWhiteSpace(mainwindow.textBoxOutput.Text))
                {
                    mainwindow.textBoxOutput.Text = inputDir + inputFileName + outputExt;
                }

                // Input Empty, Output Not Empty
                // Output is Output
                if (!string.IsNullOrWhiteSpace(mainwindow.textBoxOutput.Text))
                {
                    outputDir = System.IO.Path.GetDirectoryName(mainwindow.textBoxOutput.Text).TrimEnd('\\') + @"\";

                    outputFileName = System.IO.Path.GetFileNameWithoutExtension(mainwindow.textBoxOutput.Text);
                }

                // Image Sequence
                if ((string)mainwindow.cboMediaType.SelectedItem == "Sequence")
                {
                    outputFileName = "image-%03d"; //must be this name
                }

                // Output
                output = outputDir + outputFileName + outputExt; // (eg. C:\Output Folder\ + file + .mp4)                                                     
            }

            // -------------------------
            // Batch
            // -------------------------
            else if (mainwindow.tglBatch.IsChecked == true)
            {
                // Add slash to Batch Output Text folder path if missing
                mainwindow.textBoxOutput.Text = mainwindow.textBoxOutput.Text.TrimEnd('\\') + @"\";

                // Input Not Empty, Output Empty
                // Default Output to be same as Input Directory
                if (!string.IsNullOrWhiteSpace(mainwindow.textBoxBrowse.Text) && string.IsNullOrWhiteSpace(mainwindow.textBoxOutput.Text))
                {
                    mainwindow.textBoxOutput.Text = mainwindow.textBoxBrowse.Text;
                }

                outputDir = mainwindow.textBoxOutput.Text;

                // Output             
                output = outputDir + "%~nf" + outputExt; // (eg. C:\Output Folder\%~nf.mp4)
            }

            // -------------------------
            // Empty
            // -------------------------
            // Input Textbox & Output Textbox Both Empty
            if (string.IsNullOrWhiteSpace(mainwindow.textBoxOutput.Text))
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
        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }



        /// <summary>
        ///    File Renamer (Method)
        /// </summary>
        public void FileRenamer()
        {
            // Prevent Filename Overwrite, Add number to filename if it already exists
            // If Output File Already Exists
            if (File.Exists(outputDir + outputFileName + outputExt))
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("File Exists: ")) { Foreground = Log.ConsoleWarning });
                    Log.logParagraph.Inlines.Add(new Run(outputDir + outputFileName + outputExt) { Foreground = Log.ConsoleWarning });
                };
                Log.LogActions.Add(Log.WriteAction);

                List<string> FileNames = new List<string>();
                FileNames = Directory.GetFiles(@outputDir, "*" + outputExt)
                    .Select(System.IO.Path.GetFileName)
                    .ToList();

                int i = 0;
                int rn = 0;

                // For Each *.mp4 In OutputDir
                foreach (var file in FileNames)
                {
                    i += 1; //Add 1 to i per all files in the loop


                    // Add 1 to rn per matching file found
                    if (FileNames.Contains(string.Format("{0}({1})", inputFileName, i) + outputExt)) //example, +(1), .mp4
                    {
                        rn += 1;

                        outputNewFileName = string.Format("{0}({1})", inputFileName, rn);

                        // Log Console Message /////////
                        Log.WriteAction = () =>
                        {
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new LineBreak());
                            Log.logParagraph.Inlines.Add(new Bold(new Run("File Exists: ")) { Foreground = Log.ConsoleWarning });
                            Log.logParagraph.Inlines.Add(new Run(outputDir + string.Format("{0}({1})", inputFileName, rn) + outputExt) { Foreground = Log.ConsoleWarning });
                        };
                        Log.LogActions.Add(Log.WriteAction);
                    }
                    // If File Does Not Exist
                    else
                    {
                        rn += 1;

                        outputNewFileName = string.Format("{0}({1})", inputFileName, rn);

                        break;
                    }
                }

                // Output File name
                outputFileName = outputNewFileName;

                //rn += 1;                   
                output = outputDir + outputFileName + outputExt;

                // Clear
                FileNames.Clear(); 
                FileNames.TrimExcess();


                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Renaming File: ")) { Foreground = Log.ConsoleAction });
                    Log.logParagraph.Inlines.Add(new Run(output) { Foreground = Log.ConsoleAction });
                };
                Log.LogActions.Add(Log.WriteAction);
            }
        }


        /// <summary>
        ///    Error Halts (Method)
        /// </summary>
        public static void ErrorHalts(MainWindow mainwindow)
        {
            // Check if FFmpeg & FFprobe Exists
            if (ffCheckCleared == 0)
            {
                mainwindow.FFcheck();
            }

            // Do not allow Auto without FFprobe being installed or linked
            if ((string)mainwindow.cboVideo.SelectedItem == "Auto" 
                | (string)mainwindow.cboAudio.SelectedItem == "Auto" 
                && string.IsNullOrEmpty(MainWindow.FFprobePath(mainwindow)))
            {
                // Log Console Message /////////
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Auto Quality Mode Requires FFprobe in order to Detect File Info.")) { Foreground = Log.ConsoleWarning });

                MessageBox.Show("Auto Quality Mode Requires FFprobe in order to Detect File Info.");
                /* lock */
                ready = 0;
            }

            // Do not allow Script to generate if Browse Empty & Auto, since there is no file to detect bitrates/codecs
            // Ignore if Batch
            if (mainwindow.tglBatch.IsChecked == false)
            {
                if (string.IsNullOrWhiteSpace(mainwindow.textBoxBrowse.Text)
                    && (string)mainwindow.cboVideo.SelectedItem == "Auto"
                    | (string)mainwindow.cboAudio.SelectedItem == "Auto"
                    && (string)mainwindow.cboVideoCodec.SelectedItem != "Copy"
                    | (string)mainwindow.cboAudioCodec.SelectedItem != "Copy")
                {
                    // Log Console Message /////////
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Auto Mode needs an input file in order to detect settings.")) { Foreground = Log.ConsoleWarning });

                    MessageBox.Show("Auto Mode needs an input file in order to detect settings.");
                    /* lock */
                    ready = 0;
                    script = 0;
                }
            }


            // STOP if Single File Input with no Extension
            if (mainwindow.tglBatch.IsChecked == false && mainwindow.textBoxBrowse.Text.EndsWith("\\"))
            {
                // Log Console Message /////////
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Please choose an input file.")) { Foreground = Log.ConsoleWarning });

                MessageBox.Show("Please choose an input file.");
                /* lock */
                ready = 0;
            }

            // STOP Do not allow Batch Copy to same folder if file extensions are the same (to avoid file overwrite)
            if (mainwindow.tglBatch.IsChecked == true 
                && string.Equals(inputDir, outputDir, StringComparison.CurrentCultureIgnoreCase) 
                && string.Equals(batchExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
            {
                // Log Console Message /////////
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Please choose an output folder different than the input folder to avoid file overwrite.")) { Foreground = Log.ConsoleWarning });

                MessageBox.Show("Please choose an output folder different than the input folder to avoid file overwrite.");
                /* lock */
                ready = 0;
            }

            // STOP Throw Error if VP8/VP9 & CRF does not have Bitrate -b:v
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "VP8" 
                | (string)mainwindow.cboVideoCodec.SelectedItem == "VP9" 
                && !string.IsNullOrWhiteSpace(mainwindow.crfCustom.Text) 
                && string.IsNullOrWhiteSpace(mainwindow.vBitrateCustom.Text))
            {
                // Log Console Message /////////
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: VP8/VP9 CRF must also have Bitrate. \n(e.g. 0 for Constant, 1234k for Constrained)")) { Foreground = Log.ConsoleWarning });

                MessageBox.Show("Notice: VP8/VP9 CRF must also have Bitrate. \n(e.g. 0 for Constant, 1234k for Constrained)");
                /* lock */
                ready = 0;
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
            // Detect which screen we're on
            var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
            var thisScreen = allScreens.SingleOrDefault(s => this.Left >= s.WorkingArea.Left && this.Left < s.WorkingArea.Right);

            // Start Window
            infowindow = new InfoWindow();

            // Keep Window on Top
            infowindow.Owner = Window.GetWindow(this);

            // Only allow 1 Window instance
            if (IsInfoWindowOpened) return;
            infowindow.ContentRendered += delegate { IsInfoWindowOpened = true; };
            infowindow.Closed += delegate { IsInfoWindowOpened = false; };

            // Position Relative to MainWindow
            infowindow.Left = Math.Max((this.Left + (this.Width - infowindow.Width) / 2), thisScreen.WorkingArea.Left);
            infowindow.Top = Math.Max((this.Top + (this.Height - infowindow.Height) / 2), thisScreen.WorkingArea.Top);

            // Open Window
            infowindow.Show();
        }


        /// <summary>
        ///     Configure Settings Window Button
        /// </summary>
        private void buttonConfigure_Click(object sender, RoutedEventArgs e)
        {
            // Detect which screen we're on
            var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
            var thisScreen = allScreens.SingleOrDefault(s => this.Left >= s.WorkingArea.Left && this.Left < s.WorkingArea.Right);

            // Open Configure Window
            configurewindow = new ConfigureWindow(this);

            // Position Relative to MainWindow
            // Keep from going off screen
            configurewindow.Left = Math.Max((this.Left + (this.Width - configurewindow.Width) / 2), thisScreen.WorkingArea.Left);
            configurewindow.Top = Math.Max(this.Top - configurewindow.Height - 12, thisScreen.WorkingArea.Top);

            // Keep Window on Top
            configurewindow.Owner = Window.GetWindow(this);

            // Open Winndow
            configurewindow.ShowDialog();
        }


        /// <summary>
        ///     Log Console Window Button
        /// </summary>
        private void buttonLogConsole_Click(object sender, RoutedEventArgs e)
        {
            // Detect which screen we're on
            var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
            var thisScreen = allScreens.SingleOrDefault(s => this.Left >= s.WorkingArea.Left && this.Left < s.WorkingArea.Right);

            // Position Relative to MainWindow
            // Keep from going off screen
            logconsole.Left = Math.Min(this.Left + this.ActualWidth + 12, thisScreen.WorkingArea.Right - logconsole.Width);
            logconsole.Top = Math.Min(this.Top + 0, thisScreen.WorkingArea.Bottom - logconsole.Height);

            // Open Winndow
            logconsole.Show();
        }

        /// <summary>
        ///     Debug Console Window Button
        /// </summary>
        private Boolean IsDebugConsoleOpened = false;
        private void buttonDebugConsole_Click(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Open Debug Console Window
            // -------------------------

            // Detect which screen we're on
            var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
            var thisScreen = allScreens.SingleOrDefault(s => this.Left >= s.WorkingArea.Left && this.Left < s.WorkingArea.Right);

            // Start Window
            debugconsole = new DebugConsole(this, configurewindow);

            // Only allow 1 Window instance
            if (IsDebugConsoleOpened) return;
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

        /// <summary>
        ///     File Properties Button
        /// </summary>
        private Boolean IsFilePropertiesOpened = false;
        private void buttonProperties_Click(object sender, RoutedEventArgs e)
        {
            /// <summary>
            ///    FFprobe Video Entry Type Containers
            /// </summary> 
            FFprobe.FFprobeInputFileProperties(this);


            // Detect which screen we're on
            var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
            var thisScreen = allScreens.SingleOrDefault(s => this.Left >= s.WorkingArea.Left && this.Left < s.WorkingArea.Right);

            // Start window
            //MainWindow mainwindow = this;
            filepropwindow = new FilePropertiesWindow(this);

            // Only allow 1 Window instance
            if (IsFilePropertiesOpened) return;
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
                string parseLatestVersion = UpdateWindow.wc.DownloadString("https://raw.githubusercontent.com/MattMcManis/Axiom/master/.version");

                //Split Version & Build Phase by dash
                if (!string.IsNullOrEmpty(parseLatestVersion)) //null check
                {
                    // Split Version and Build Phase
                    splitVersionBuildPhase = Convert.ToString(parseLatestVersion).Split('-');

                    // Set Version Number
                    latestVersion = new Version(splitVersionBuildPhase[0]); //number
                    latestBuildPhase = splitVersionBuildPhase[1]; //alpha


                    // Debug
                    //MessageBox.Show(Convert.ToString(latestVersion));
                    //MessageBox.Show(latestBuildPhase);


                    // Check if Axiom is the Latest Version
                    // Update Available
                    if (latestVersion > currentVersion)
                    {
                        // Yes/No Dialog Confirmation
                        //
                        MessageBoxResult result = MessageBox.Show("Download Update?", "Update Available", MessageBoxButton.YesNo);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                // Detect which screen we're on
                                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                                var thisScreen = allScreens.SingleOrDefault(s => this.Left >= s.WorkingArea.Left && this.Left < s.WorkingArea.Right);

                                // Start Window
                                updatewindow = new UpdateWindow();

                                // Keep in Front
                                updatewindow.Owner = Window.GetWindow(this);

                                // Only allow 1 Window instance
                                if (IsUpdateWindowOpened) return;
                                updatewindow.ContentRendered += delegate { IsUpdateWindowOpened = true; };
                                updatewindow.Closed += delegate { IsUpdateWindowOpened = false; };

                                // Position Relative to MainWindow
                                // Keep from going off screen
                                updatewindow.Left = Math.Max((this.Left + (this.Width - updatewindow.Width) / 2), thisScreen.WorkingArea.Left);
                                updatewindow.Top = Math.Max((this.Top + (this.Height - updatewindow.Height) / 2), thisScreen.WorkingArea.Top);

                                // Open Window
                                updatewindow.Show();
                                break;
                            case MessageBoxResult.No:
                                break;
                        }
                    }
                    // Update Not Available
                    else if (latestVersion <= currentVersion)
                    {
                        //MainWindow.ready = 0;
                        MessageBox.Show("This version is up to date.");
                    }
                    // Unknown
                    else // null
                    {
                        //MainWindow.ready = 0;
                        MessageBox.Show("Could not find download. Try updating manually.");
                    }
                }
                // Version is Null
                else
                {
                    MessageBox.Show("Could not detect Axiom GitHub Version.");
                }
            }
            else
            {
                //MainWindow.ready = 0;
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
        ///    Save Profile Button
        /// </summary>
        //private void buttonSave_Click(object sender, RoutedEventArgs e)
        //{
        //    // Create an Array of SelectItem.ToString for each Control
        //    // Save the Array to a New Setting
        //    // Load each Control from Saved Setting Array

        //    string customPreset = cboPreset.Text;

        //    // Prefix the User Custom Preset Text to the Saved Setting
        //    // Create a New Setting
        //    //var propertyFormat = new SettingsProperty(customPreset + "Format");

        //    SettingsProperty propertyFormat = new SettingsProperty(customPreset + "Format");

        //    propertyFormat.Name = customPreset + "Format";
        //    //propertyFormat.Provider = Settings.Default.Providers["LocalFileSettingsProvider"];
        //    propertyFormat.PropertyType = typeof(string);
        //    propertyFormat.IsReadOnly = false;
        //    propertyFormat.Attributes.Add(typeof(UserScopedSettingAttribute), new UserScopedSettingAttribute());

        //    Settings.Default.Properties.Add(propertyFormat);

        //    Settings.Default.Save();
        //    Settings.Default.Reload();

        //    // Add ComboBox SelectedItem String to the Setting
        //    Settings.Default[customPreset + "Format"] = (string)cboFormat.SelectedItem;
        //}


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


            // Enable Script
            script = 1;

            // Reset Sort
            ScriptView.sort = 0;


            /// <summary>
            ///    Keep FFmpeg Window Toggle
            /// </summary>
            MainWindow.KeepWindow(this);


            /// <summary>
            ///    Batch Extention Period Check
            /// </summary>
            MainWindow.BatchExtCheck(this);


            /// <summary>
            ///    Error Halts
            /// </summary> 
            MainWindow.ErrorHalts(this);


            // -------------------------
            // Background Thread Worker
            // -------------------------
            BackgroundWorker fileprocess = new BackgroundWorker();

            fileprocess.WorkerSupportsCancellation = true;

            // This allows the worker to report progress during work
            fileprocess.WorkerReportsProgress = true;

            // What to do in the background thread
            fileprocess.DoWork += new DoWorkEventHandler(delegate (object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;

                // Dispatcher Allows Cross-Thread Communication
                this.Dispatcher.Invoke(() =>
                {
                    /// <summary>
                    ///    FFprobe Detect Metadata
                    /// </summary> 
                    FFprobe.Metadata(this);

                    // ------------------------------------------------------------------------

                    /// <summary>
                    ///    Write All Log Actions to Console
                    /// </summary> 
                    Log.LogWriteAll(this, configurewindow);

                    // ------------------------------------------------------------------------

                    /// <summary>
                    ///    FFmpeg Single File Generate Arguments
                    /// </summary> 
                    FFmpeg.FFmpegSingleGenerateArgs(this);

                    // ------------------------------------------------------------------------

                    /// <summary>
                    ///    FFmpeg Batch Generate Arguments
                    /// </summary> 
                    FFmpeg.FFmpegBatchGenerateArgs(this);


                }); //end dispatcher
            }); //end thread


            // When background worker completes task
            fileprocess.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate(object o, RunWorkerCompletedEventArgs args)
            {
                /// <summary>
                ///    Generate Script
                /// </summary> 
                FFmpeg.FFmpegScript(this, scriptview);

                //sw.Stop(); //stop stopwatch

                // Close the Background Worker
                fileprocess.CancelAsync();
                fileprocess.Dispose();

                // Clear Variables for next Run
                ClearVariables(this);
                GC.Collect();

            }); //end worker completed task


            // Background Worker Run Async
            fileprocess.RunWorkerAsync(); //important!
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
            Video.EncodingPass(this);
        }
        private void cboPass_DropDownClosed(object sender, EventArgs e)
        {
            // User willingly selected a Pass
            Video.passUserSelected = true;
        }


        /// <summary>
        ///    Play File Button
        /// </summary>
        private void buttonPlayFile_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(@output))
            {
                //MessageBox.Show("File exists.");
                System.Diagnostics.Process.Start("\"" + output + "\"");
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
                // Open 'Save File'
                Microsoft.Win32.OpenFileDialog selectFile = new Microsoft.Win32.OpenFileDialog();

                // Show save file dialog box
                Nullable<bool> result = selectFile.ShowDialog();

                // Process dialog box
                if (result == true)
                {
                    // Display path and file in Output Textbox
                    textBoxBrowse.Text = selectFile.FileName;

                    // Input Directory Path
                    inputDir = System.IO.Path.GetDirectoryName(textBoxBrowse.Text).TrimEnd('\\') + @"\";

                    // Set input file name
                    inputFileName = System.IO.Path.GetFileNameWithoutExtension(textBoxBrowse.Text);

                    // Get input file extension
                    inputExt = System.IO.Path.GetExtension(textBoxBrowse.Text);


                    // Add slash to inputDir path if missing
                    if (!inputDir.EndsWith("\\") && !string.IsNullOrEmpty(inputDir))
                    {
                        // inputDir += "\\";
                        inputDir = inputDir.TrimEnd('\\') + @"\";
                    }

                }

                // Set Video & Audio Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
                Video.AutoCopyVideoCodec(this);
                Audio.AutoCopyAudioCodec(this);
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
                    textBoxBrowse.Text = folderBrowserDialog.SelectedPath.TrimEnd('\\') + @"\";

                    // Remove Double Slash in Root Dir, such as C:\
                    textBoxBrowse.Text = textBoxBrowse.Text.Replace(@"\\", @"\");

                    // Add slash to Batch Browse Text folder path if missing
                    if (!string.IsNullOrWhiteSpace(textBoxBrowse.Text) && !textBoxBrowse.Text.EndsWith("\\"))
                    {
                        textBoxBrowse.Text = textBoxBrowse.Text.TrimEnd('\\') + @"\";
                    }

                    // Input Directory Path
                    inputDir = System.IO.Path.GetDirectoryName(textBoxBrowse.Text).TrimEnd('\\') + @"\";

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
                    Video.AutoCopyVideoCodec(this);
                    Audio.AutoCopyAudioCodec(this);
                }

                // Prevent Losing Codec Copy after cancel closing Browse Folder Dialog Box 
                //
                // Set Video & Audio Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
                Video.AutoCopyVideoCodec(this);
                Audio.AutoCopyAudioCodec(this);
            }
        }


        /// <summary>
        ///    Browse Textbox
        /// </summary>
        private void textBoxBrowse_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Remove stray slash if closed out early (duplicate code?)
            if (textBoxBrowse.Text == "\\")
            {
                textBoxBrowse.Text = string.Empty;
            }

            // Get input file extension
            inputExt = System.IO.Path.GetExtension(textBoxBrowse.Text);

            // Set Video & Audio Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
            Video.AutoCopyVideoCodec(this);
            Audio.AutoCopyAudioCodec(this);
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
                Format.OutputFormatExt(this);


                // Open 'Save File'
                Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();

                // 'Save File' Default Path same as Input Directory
                saveFile.InitialDirectory = inputDir;
                saveFile.RestoreDirectory = true;

                // Default file name if empty
                if (string.IsNullOrEmpty(inputFileName))
                {
                    // Default
                    saveFile.FileName = "File";
                }
                else
                {
                    // Temp Output Path
                    // Make same as Input Path so File Renamer can detect
                    outputDir = inputDir;

                    // Temp Output Filename (without extension)
                    // Make same as Input Filename so File Renamer can detect
                    outputFileName = inputFileName;

                    // Call File Renamer Method
                    // Get new output file name (1) if already exists
                    FileRenamer();

                    // Same as input file name
                    saveFile.FileName = outputFileName;
                }

                // Default file extension is selected format
                saveFile.DefaultExt = outputExt; 

                // Show save file dialog box
                Nullable<bool> result = saveFile.ShowDialog();

                // Process dialog box
                if (result == true)
                {
                    // Display path and file in Output Textbox
                    textBoxOutput.Text = saveFile.FileName;

                    // Output Path
                    outputDir = System.IO.Path.GetDirectoryName(textBoxOutput.Text).TrimEnd('\\') + @"\";

                    // Output Filename (without extension)
                    outputFileName = System.IO.Path.GetFileNameWithoutExtension(textBoxOutput.Text);

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
                    textBoxOutput.Text = outputFolder.SelectedPath.TrimEnd('\\') + @"\";

                    // Remove Double Slash in Root Dir, such as C:\
                    textBoxOutput.Text = textBoxOutput.Text.Replace(@"\\", @"\");


                    // Output Path
                    outputDir = System.IO.Path.GetDirectoryName(textBoxOutput.Text).TrimEnd('\\') + @"\";

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
        private void textBoxOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Remove stray slash if closed out early
            if (textBoxOutput.Text == "\\")
            {
                textBoxOutput.Text = string.Empty;
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
            Video.AutoCopyVideoCodec(this);

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
            Audio.AutoCopyAudioCodec(this);
        }


        /// <summary>
        ///     Bit Depth ComboBox
        /// </summary>
        private void cboBitDepth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Switch to Copy if inputExt & outputExt match
            Audio.AutoCopyAudioCodec(this);
        }


        /// <summary>
        ///    Volume TextBox Changed
        /// </summary>
        private void volumeUpDown_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Disable Volume instead of running AutoCopyAudioCodec each time 
            // This needs to be re-thought, calling method on every timer tick
            Audio.AutoCopyAudioCodec(this);
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
            Video.VideoCodecControls(this);

            // Video Encoding Pass Controls Method
            Video.EncodingPass(this); 
        }


        /// <summary>
        ///    Audio Codec Combobox
        /// </summary>
        private void cboAudioCodec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Audio.AudioCodecControls(this);
        }


        /// <summary>
        ///    Format Combobox
        /// </summary>
        private void cboFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Output Control Selections
            Format.OuputFormatDefaults(this);

            // Get Output Extension
            Format.OutputFormatExt(this);

            // Output ComboBox Options
            Format.OutputFormat(this);

            // Change All MainWindow Items
            Video.VideoCodecControls(this);
            Audio.AudioCodecControls(this);

            // File Renamer
            if (string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
            {
                FileRenamer();
            }
            

            // Always Default Video to Auto if Input Ext matches Format Output Ext
            if ((string)cboVideo.SelectedItem != "Auto" && string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
            {
                cboVideo.SelectedItem = "Auto";
            }
            // Always Default Video to Auto if Input Ext matches Format Output Ext
            if ((string)cboAudio.SelectedItem != "Auto" && string.Equals(inputExt, outputExt, StringComparison.CurrentCultureIgnoreCase))
            {
                cboAudio.SelectedItem = "Auto";
            }

            // Single File - Update Ouput Textbox with current Format extension
            if (tglBatch.IsChecked == false && !string.IsNullOrWhiteSpace(textBoxOutput.Text))
            {
                textBoxOutput.Text = outputDir + outputFileName + outputExt;
            }
            
        }


        /// <summary>
        ///    Media Type Combobox
        /// </summary>
        private void cboMediaType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Format.MediaType(this); 
        }


        /// <summary>
        ///    Video Quality Combobox
        /// </summary>
        private void cboVideo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Video.AutoCopyVideoCodec(this);

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
                crfCustom.Text = "CRF";
                vBitrateCustom.IsEnabled = false;
                vBitrateCustom.Text = "Bitrate";
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
            Video.EncodingPass(this);

            // -------------------------
            // Pass - Default to CRF
            // -------------------------
            // Keep in Video SelectionChanged
            // If Video Not Auto and User Willingly Selected Pass is false
            if ((string)cboVideo.SelectedItem != "Auto" && Video.passUserSelected == false)
            {
                cboPass.SelectedItem = "CRF";
            }

        } // End Video Combobox


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
                audioCustom.Text = "kbps";
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
            Audio.AutoCopyAudioCodec(this);

        } // End audio_SelectionChanged


        /// <summary>
        ///    Size Combobox
        /// </summary>
        private void cboSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Set Video Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
            Video.AutoCopyVideoCodec(this);

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
            Format.CutControls(this); 
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
                Video.VideoFilters.Clear();
                Video.VideoFilters.TrimExcess();

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
            //Video.AutoCopyVideoCodec(this); // this caused a loop error
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
            Audio.AutoCopyAudioCodec(this);
        }
        private void tglAudioLimiter_Unchecked(object sender, RoutedEventArgs e)
        {
            // Disable Limit TextBox
            if (tglAudioLimiter.IsChecked == false)
            {
                audioLimiter.IsEnabled = false;
            }

            // Enable Audio Codec Copy if InputExt / outputExt match
            Audio.AutoCopyAudioCodec(this);
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
            Video.AutoCopyVideoCodec(this);
            Audio.AutoCopyAudioCodec(this);
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
            if (!string.IsNullOrWhiteSpace(textBoxBrowse.Text))
            {
                textBoxBrowse.Text = string.Empty;
                inputFileName = string.Empty;
                inputDir = string.Empty;
                inputExt = string.Empty;
            }

            // Clear Output Textbox, Output Filename, Dir, Ext
            if (!string.IsNullOrWhiteSpace(textBoxOutput.Text))
            {
                textBoxOutput.Text = string.Empty;
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
            if (!string.IsNullOrWhiteSpace(textBoxBrowse.Text))
            {
                textBoxBrowse.Text = string.Empty;
                inputFileName = string.Empty;
                inputDir = string.Empty;
                inputExt = string.Empty;
            }

            // Clear Output Textbox, Output Filename, Dir, Ext
            if (!string.IsNullOrWhiteSpace(textBoxOutput.Text))
            {
                textBoxOutput.Text = string.Empty;
                outputFileName = string.Empty;
                outputDir = string.Empty;
                outputExt = string.Empty;
            }

            // Set Video and AudioCodec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
            Video.AutoCopyVideoCodec(this);
            Audio.AutoCopyAudioCodec(this);
        }





        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        ///    Convert Button
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            //sw.Start(); //start stopwatch

            /// <summary>
            ///    Keep FFmpeg Window Toggle
            /// </summary>
            MainWindow.KeepWindow(this);


            /// <summary>
            ///    Batch Extention Period Check
            /// </summary>
            MainWindow.BatchExtCheck(this);


            /// <summary>
            ///    Error Halts
            /// </summary> 
            MainWindow.ErrorHalts(this); 


            // Log Console Message /////////
            if (script == 0 && ready == 1)
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
            if (ready == 1)
            {
                // -------------------------
                // Background Thread Worker
                // -------------------------
                BackgroundWorker fileprocess = new BackgroundWorker();

                fileprocess.WorkerSupportsCancellation = true;

                // This allows the worker to report progress during work
                fileprocess.WorkerReportsProgress = true;

                // What to do in the background thread
                //
                fileprocess.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;

                    //Dispatcher Allows Cross-Thread Communication
                    this.Dispatcher.Invoke(() =>
                    {
                        /// <summary>
                        ///    FFprobe Detect Metadata
                        /// </summary> 
                        FFprobe.Metadata(this);

                        // ------------------------------------------------------------------------

                        /// <summary>
                        ///    FFmpeg Single File Generate Arguments
                        /// </summary> 
                        FFmpeg.FFmpegSingleGenerateArgs(this); //disabled if batch=

                        /// <summary>
                        ///    FFmpeg Single File Convert
                        /// </summary> 
                        FFmpeg.FFmpegSingleConvert(this); //disabled if batch

                        // ------------------------------------------------------------------------

                        /// <summary>
                        ///    FFmpeg Batch Generate Arguments
                        /// </summary> 
                        FFmpeg.FFmpegBatchGenerateArgs(this); //disabled if single file

                        /// <summary>
                        ///    FFmpeg Single File Convert
                        /// </summary> 
                        FFmpeg.FFmpegBatchConvert(this); //disabled if single file


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


                    /// <summary>
                    ///    Write All Log Actions to Console
                    /// </summary> 
                    Log.LogWriteAll(this, configurewindow);


                    //sw.Stop(); //stop stopwatch

                    // Write Variables to Debug Window (Method)
                    //DebugConsole.DebugWrite(debugconsole, this);

                    // Close the Background Worker
                    fileprocess.CancelAsync();
                    fileprocess.Dispose();

                    // Clear Strings for next Run
                    ClearVariables(this);
                    GC.Collect();

                }); //end worker completed task


                // Background Worker Run Async
                fileprocess.RunWorkerAsync(); //important!
            }
            else
            {
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
                ready = 1;


                // Write Variables to Debug Window (Method)
                DebugConsole.DebugWrite(debugconsole, this);

                // Clear Variables for next Run
                ClearVariables(this);
                GC.Collect();

            }
        } //end convert button


    }

}