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

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    /// <summary>
    /// Interaction logic for DebugConsole.xaml
    /// </summary>
    public partial class DebugConsole : Window
    {
        //private MainWindow mainwindow;

        //private ViewModel vm;

        public static Paragraph debugParagraph = new Paragraph(); //RichTextBox

        public static Brush Heading;
        public static Brush Variable;
        public static Brush Value;


        public DebugConsole(MainWindow mainwindow, ViewModel vm)
        {
            InitializeComponent();

            //this.mainwindow = mainwindow;
            //vm = mainwindow.DataContext as ViewModel;
            DataContext = vm;

            //this.Width = 400;
            //this.Height = 500;
            this.MinWidth = 200;
            this.MinHeight = 200;

            // -------------------------
            // Text Theme SelectiveColorPreview
            // -------------------------
            //Configure.LoadTheme(mainwindow);

            // -------------------------
            // Debug Text Theme SelectiveColorPreview
            // -------------------------
            if (Configure.theme == "Axiom")
            {
                DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2"));
                DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8"));
                DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            }
            else if (Configure.theme == "FFmpeg")
            {
                DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#878787"));
                DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cb85c"));
                DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            }
            else if (Configure.theme == "Cyberpunk")
            {
                DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9a989c"));
                DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9f3ed2"));
                DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            }
            else if (Configure.theme == "Onyx")
            {
                DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EEEEEE"));
                DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            }
            else if (Configure.theme == "Circuit")
            {
                DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ad8a4a"));
                DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2ebf93"));
                DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            }
            else if (Configure.theme == "Prelude")
            {
                DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ad8a4a"));
                DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2ebf93"));
                DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            }
            else if (Configure.theme == "System")
            {
                DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2"));
                DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8"));
                DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            }


            // Log Console Message /////////
            // Don't put in Configure Method, creates duplicate message /////////
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new LineBreak());
            Log.logParagraph.Inlines.Add(new Bold(new Run("Theme: ")) { Foreground = Log.ConsoleDefault });
            Log.logParagraph.Inlines.Add(new Run(Configure.theme) { Foreground = Log.ConsoleDefault });

        }

        /// <summary>
        /// Close
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Close();
        }

        /// <summary>
        /// Expand Button
        /// </summary>
        private void btnExpand_Click(object sender, RoutedEventArgs e)
        {
            // If less than 600px Height
            if (this.Width <= 1048)
            {
                this.Width = 1048;
                this.Height = 720;

                double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
                double windowWidth = this.Width;
                double windowHeight = this.Height;
                this.Left = (screenWidth / 2) - (windowWidth / 2);
                this.Top = (screenHeight / 2) - (windowHeight / 2);
            }
        }


        /// <summary>
        ///     Debug Test Button
        /// </summary>
        private void btnDebugTest_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = (MainWindow)System.Windows.Application.Current.MainWindow;
            ViewModel vm = mainwindow.DataContext as ViewModel;

            // -------------------------
            // Keep FFmpeg Window Toggle
            // -------------------------
            //MainWindow.KeepWindow(mainwindow);

            // -------------------------
            // Clear Variables before Run
            // -------------------------
            MainWindow.ClearVariables(vm);

            // -------------------------
            // Batch Extention Period Check
            // -------------------------
            MainWindow.BatchExtCheck(vm);

            // -------------------------
            // Set FFprobe Path
            // -------------------------
            MainWindow.FFprobePath();

            // -------------------------
            // Ready Halts
            // -------------------------
            MainWindow.ReadyHalts(vm);


            // -------------------------
            // Background Thread Worker
            // -------------------------
            //BackgroundWorker fileprocess = new BackgroundWorker();

            //fileprocess.WorkerSupportsCancellation = true;
            //fileprocess.WorkerReportsProgress = true;

            //fileprocess.DoWork += new DoWorkEventHandler(delegate (object o, DoWorkEventArgs args)
            //{
            //    BackgroundWorker b = o as BackgroundWorker;

            //    // Cross-Thread Communication
            //    this.Dispatcher.Invoke(() =>
            //    {
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
                        //disabled if batch
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
                        //FFprobe.VideoEntryTypeBatch(this);
                        FFprobe.VideoEntryType(vm);

                        // -------------------------
                        // FFprobe Video Entry Type Containers
                        // -------------------------
                        //FFprobe.AudioEntryTypeBatch(this);
                        FFprobe.AudioEntryType(vm);

                        // -------------------------
                        // FFmpeg Generate Arguments (Batch)
                        // -------------------------
                        //disabled if single file
                        FFmpeg.FFmpegBatchGenerateArgs(vm);
                    }

            //    }); //end dispatcher

            //}); //end thread


            // When background worker completes task
            //fileprocess.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate (object o, RunWorkerCompletedEventArgs args)
            //{
                // -------------------------
                // Write Variables to Debug Window
                // -------------------------
                DebugWrite(this, vm);

                // -------------------------
                // Close the Background Worker
                // -------------------------
                //fileprocess.CancelAsync();
                //fileprocess.Dispose();

                // -------------------------
                // Clear Variables for next Run
                // -------------------------
                MainWindow.ClearVariables(vm);
                GC.Collect();

        //    }); //end worker completed task


        //    // -------------------------
        //    // Background Worker Run Async
        //    // -------------------------
        //    fileprocess.RunWorkerAsync();
        }


        /// <summary>
        ///     Debug Write
        /// </summary>
        public static void DebugWrite(DebugConsole debugconsole, ViewModel vm)
        {
            // -------------------------
            // Write Variables to Console
            // -------------------------

            // Clear Old Text
            debugParagraph.Inlines.Clear();

            debugconsole.rtbDebug.Document = new FlowDocument(debugParagraph); // start

            //debugconsole.rtbDebug.BeginChange();
            //debugconsole.rtbDebug.SelectAll();
            //debugconsole.rtbDebug.Selection.Text = "";
            //debugconsole.rtbDebug.EndChange();


            // Write New Text
            //debugconsole.rtbDebug.Document = new FlowDocument(debugParagraph); // start

            // begin change
            debugconsole.rtbDebug.BeginChange();
            //////////////////////////////////////////////////

            debugParagraph.Inlines.Add(new Bold(new Run("Program Variables")) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());


            // System
            debugParagraph.Inlines.Add(new Bold(new Run("System ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("ready ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.ready.ToString()) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("script ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.script.ToString()) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("ffCheckCleared ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.ffCheckCleared.ToString()) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("threads ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Configure.threads) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("maxthreads ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Configure.maxthreads) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("appDir ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.appDir) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());
            debugParagraph.Inlines.Add(new LineBreak());


            // Configure
            debugParagraph.Inlines.Add(new Bold(new Run("Configure ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("theme ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Configure.theme) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("ffmpeg ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFmpeg.ffmpeg) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("ffprobe ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.ffprobe) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("ffmpegPath ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Configure.ffmpegPath) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("ffprobePath ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Configure.ffprobePath) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("logPath ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Configure.logPath) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("logEnable ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(vm.LogCheckBox_IsChecked.ToString()) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());
            debugParagraph.Inlines.Add(new LineBreak());


            // Input
            debugParagraph.Inlines.Add(new Bold(new Run("Input ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputDir ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.inputDir) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputFileName ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.inputFileName) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputExt ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.inputExt) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("input ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.input) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());
            debugParagraph.Inlines.Add(new LineBreak());


            // Output
            debugParagraph.Inlines.Add(new Bold(new Run("Output ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("outputDir ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.outputDir) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("outputFileName ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.outputFileName) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("outputNewFileName ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.outputNewFileName) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("outputExt ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.outputExt) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("output ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.output) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());
            debugParagraph.Inlines.Add(new LineBreak());


            // Batch
            debugParagraph.Inlines.Add(new Bold(new Run("Batch ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("batchInputAuto ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.batchInputAuto) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            //debugParagraph.Inlines.Add(new Bold(new Run("batchExt ")) { Foreground = Variable });
            //debugParagraph.Inlines.Add(new Run(MainWindow.batchExt) { Foreground = Value });
            //debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("batchFFprobeAuto ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.batchFFprobeAuto) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("batchVideoAuto ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.batchVideoAuto) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("batchAudioAuto ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Audio.batchAudioAuto) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());
            debugParagraph.Inlines.Add(new LineBreak());


            // Video
            debugParagraph.Inlines.Add(new Bold(new Run("Video ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("hwaccel ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.hwaccel) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vEncodeSpeed ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vEncodeSpeed) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vCodec ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vCodec) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vQuality ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vQuality) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vLossless ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vLossless) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            //debugParagraph.Inlines.Add(new Bold(new Run("vBitMode ")) { Foreground = Variable });
            //debugParagraph.Inlines.Add(new Run(Video.vBitMode) { Foreground = Value });
            //debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vBitrate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vBitrate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vMaxrate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vMaxrate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vBufsize ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vBufsize) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vOptions ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vOptions) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("crf ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.crf) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("x265params ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.x265params) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("fps ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.fps) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("image ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.image) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vEncodeSpeed ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vEncodeSpeed) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("passUserSelected ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(VideoControls.passUserSelected.ToString()) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("pass1 ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.pass1) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("pass2 ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.pass2) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("v2passArgs ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.v2PassArgs) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("optTune ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.optTune) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("optProfile ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.optProfile) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("optLevel ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.optLevel) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("optimize ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.optimize) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());


            debugParagraph.Inlines.Add(new Bold(new Run("width ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.width) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("height ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.height) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("cropWidth ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(CropWindow.cropWidth) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("cropHeight ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(CropWindow.cropHeight) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("cropX ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(CropWindow.cropX) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("cropY ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(CropWindow.cropY) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("crop ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(CropWindow.crop) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("geq ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(VideoFilters.geq) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vFilter ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(VideoFilters.vFilter) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());
            debugParagraph.Inlines.Add(new LineBreak());


            // Audio
            debugParagraph.Inlines.Add(new Bold(new Run("Audio ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aCodec ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Audio.aCodec) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aQuality ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Audio.aQuality) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aBitMode ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Audio.aBitMode) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aBitrate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Audio.aBitrate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aChannel ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Audio.aChannel) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aSamplerate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Audio.aSamplerate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aBitDepth ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Audio.aBitDepth) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("volume ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Audio.aVolume) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aHardLimiter ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Audio.aHardLimiter) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aFilter ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(AudioFilters.aFilter) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aBitrateLimiter ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Audio.aBitrateLimiter) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());
            debugParagraph.Inlines.Add(new LineBreak());


            // Streams
            debugParagraph.Inlines.Add(new Bold(new Run("Streams ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vMap ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Streams.vMap) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aMap ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Streams.aMap) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("cMap ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Streams.cMap) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("sMap ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Streams.sMap) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("mMap ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Streams.mMap) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            //debugParagraph.Inlines.Add(new Bold(new Run("map ")) { Foreground = Variable });
            //debugParagraph.Inlines.Add(new Run(Streams.map) { Foreground = Value });
            //debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());
            debugParagraph.Inlines.Add(new LineBreak());


            // Cut
            debugParagraph.Inlines.Add(new Bold(new Run("Cut ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("trimStart ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Format.trimStart) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("trimEnd ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Format.trimEnd) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("trim ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Format.trim) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());
            debugParagraph.Inlines.Add(new LineBreak());


            // FFprobe
            debugParagraph.Inlines.Add(new Bold(new Run("FFprobe ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsProperties ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.argsProperties) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsVideoCodec ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.argsVideoCodec) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsAudioCodec ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.argsAudioCodec) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsVideoBitrate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.argsVideoBitrate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsAudioBitrate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.argsAudioBitrate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsSize ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.argsSize) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsDuration ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.argsDuration) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsFrameRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.argsFrameRate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());


            debugParagraph.Inlines.Add(new Bold(new Run("inputFileProperties ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.inputFileProperties) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputVideoCodec ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.inputVideoCodec) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputVideoBitrate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.inputVideoBitrate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputAudioCodec ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.inputAudioCodec) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputAudioBitrate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.inputAudioBitrate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputSize ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.inputSize) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputDuration ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.inputDuration) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputFrameRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.inputFrameRate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vEntryType ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.vEntryType) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aEntryType ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.aEntryType) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());
            debugParagraph.Inlines.Add(new LineBreak());


            // FFmpeg
            debugParagraph.Inlines.Add(new Bold(new Run("FFmpeg ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            //debugParagraph.Inlines.Add(new Bold(new Run("cmdWindow ")) { Foreground = Variable });
            //debugParagraph.Inlines.Add(new Run(FFmpeg.cmdWindow) { Foreground = Value });
            //debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("ffmpegArgs ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFmpeg.ffmpegArgs) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            //////////////////////////////////////////////////
            debugconsole.rtbDebug.EndChange(); // end change
        }

    }
}