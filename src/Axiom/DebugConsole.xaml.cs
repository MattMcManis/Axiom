using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
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
    /// Interaction logic for DebugConsole.xaml
    /// </summary>
    public partial class DebugConsole : Window
    {
        private MainWindow mainwindow;

        private ScriptView scriptview;

        public static Paragraph debugParagraph = new Paragraph(); //RichTextBox
        public static System.Windows.Media.Brush Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2"));
        public static System.Windows.Media.Brush Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8"));
        public static System.Windows.Media.Brush Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));

        public DebugConsole()
        {
            //do not remove
        }

        public DebugConsole(MainWindow mainwindow)
        {
            InitializeComponent();

            this.mainwindow = mainwindow;

            // Set Width/Height to prevent Tablets maximizing
            this.Width = 400;
            this.Height = 500;
            this.MinWidth = 200;
            this.MinHeight = 200;
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
            /// <summary>
            ///    Keep FFmpeg Window Toggle
            /// </summary>
            MainWindow.KeepWindow(mainwindow);


            /// <summary>
            ///    Batch Extention Period Check
            /// </summary>
            MainWindow.BatchExtCheck(mainwindow);


            /// <summary>
            ///    Error Halts
            /// </summary> 
            MainWindow.ErrorHalts(mainwindow);


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

                //Dispatcher Allows Cross-Thread Communication
                this.Dispatcher.Invoke(() =>
                {
                    /// <summary>
                    ///    FFprobe Detect Metadata
                    /// </summary> 
                    FFprobe.Metadata(mainwindow);

                    // ------------------------------------------------------------------------

                    /// <summary>
                    ///    FFmpeg Single File Generate Arguments
                    /// </summary> 
                    FFmpeg.FFmpegSingleGenerateArgs(mainwindow);

                    // ------------------------------------------------------------------------

                    /// <summary>
                    ///    FFmpeg Batch Generate Arguments
                    /// </summary> 
                    FFmpeg.FFmpegBatchGenerateArgs(mainwindow);

                }); //end dispatcher
            }); //end thread


            // When background worker completes task
            fileprocess.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate (object o, RunWorkerCompletedEventArgs args)
            {
                /// <summary>
                ///    Generate Script
                /// </summary> 
                FFmpeg.FFmpegScript(mainwindow, scriptview);

                //sw.Stop(); //stop stopwatch

                // Write Variables to Debug Window (Method)
                DebugWrite(this, mainwindow);

                // Close the Background Worker
                fileprocess.CancelAsync();
                fileprocess.Dispose();

                // Clear Variables for next Run
                MainWindow.ClearVariables();
                GC.Collect();

            }); //end worker completed task


            // Background Worker Run Async
            fileprocess.RunWorkerAsync(); //important!
        }


        /// <summary>
        ///     Debug Write
        /// </summary>
        public static void DebugWrite(DebugConsole debugconsole, MainWindow mainwindow)
        {
            // -------------------------
            // Write Variables to Console
            // -------------------------

            // Clear Old Text
            debugconsole.rtbDebug.Document = new FlowDocument(debugParagraph); // start

            debugconsole.rtbDebug.BeginChange();
            debugconsole.rtbDebug.SelectAll();
            debugconsole.rtbDebug.Selection.Text = "";
            debugconsole.rtbDebug.EndChange();


            // Write New Text
            debugconsole.rtbDebug.Document = new FlowDocument(debugParagraph); // start

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
            debugParagraph.Inlines.Add(new Run(MainWindow.threads) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());
        
            debugParagraph.Inlines.Add(new Bold(new Run("maxthreads ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.maxthreads) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("currentDir ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.currentDir) { Foreground = Value });
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
            debugParagraph.Inlines.Add(new Run(Configure.logEnable.ToString()) { Foreground = Value });
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

            debugParagraph.Inlines.Add(new Bold(new Run("batchExt ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.batchExt) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

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

            debugParagraph.Inlines.Add(new Bold(new Run("vCodec ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vCodec) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vQuality ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vCodec) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vBitMode ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vBitMode) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vBitrate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vBitMode) { Foreground = Value });
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

            debugParagraph.Inlines.Add(new Bold(new Run("fps ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.fps) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("tune ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.optTune) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("options ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.options) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("speed ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.speed) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("passUserSelected ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.passUserSelected.ToString()) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("pass1 ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.pass1) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("pass2 ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.pass2) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("v2passArgs ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.v2passArgs) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("optAdvTune ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.optAdvTune) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("optAdvProfile ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.optAdvProfile) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("optAdvLevel ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.optAdvLevel) { Foreground = Value });
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

            debugParagraph.Inlines.Add(new Bold(new Run("aspect ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.aspect) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

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

            debugParagraph.Inlines.Add(new Bold(new Run("cropDivisible ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.cropDivisible) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("crop ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(CropWindow.crop) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vFilterSwitch ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vFilterSwitch.ToString()) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("geq ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.geq) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vFilter ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vFilter) { Foreground = Value });
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
            debugParagraph.Inlines.Add(new Run(Audio.volume) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aLimiter ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Audio.aLimiter) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aFilterSwitch ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Audio.aFilterSwitch.ToString()) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aFilter ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Audio.aFilter) { Foreground = Value });
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

            debugParagraph.Inlines.Add(new Bold(new Run("map ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Streams.map) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

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

            debugParagraph.Inlines.Add(new Bold(new Run("argsFramerate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.argsFramerate) { Foreground = Value });
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

            debugParagraph.Inlines.Add(new Bold(new Run("inputFramerate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.inputFramerate) { Foreground = Value });
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

            debugParagraph.Inlines.Add(new Bold(new Run("ffmpeg ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFmpeg.ffmpeg) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("ffmpegArgs ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFmpeg.ffmpegArgs) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("cmdWindow ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFmpeg.cmdWindow) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            //////////////////////////////////////////////////
            debugconsole.rtbDebug.EndChange(); // end change
        }

    }
}
