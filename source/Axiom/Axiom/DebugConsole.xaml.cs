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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
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

        public static Brush Heading { get; set; }
        public static Brush Variable { get; set; }
        public static Brush Value { get; set; }


        public DebugConsole(MainWindow mainwindow)
        {
            InitializeComponent();

            //this.mainwindow = mainwindow;
            //vm = mainwindow.DataContext as ViewModel;
            //DataContext = vm;

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
        /// Debug Test Button
        /// </summary>
        private void btnDebugTest_Click(object sender, RoutedEventArgs e)
        {
            DebugTest();
        }


        public async void DebugTest() 
        {
            //MainWindow mainwindow = (MainWindow)System.Windows.Application.Current.MainWindow;
            //MainView vm = mainwindow.DataContext as MainView;

            // -------------------------
            // Clear Variables before Run
            // -------------------------
            MainWindow.ClearGlobalVariables();

            // -------------------------
            // Batch Extention Period Check
            // -------------------------
            MainWindow.BatchExtCheck();

            // -------------------------
            // Set FFprobe Path
            // -------------------------
            MainWindow.FFprobePath();

            // -------------------------
            // Set youtube-dl Path
            // -------------------------
            MainWindow.youtubedlPath();

            // -------------------------
            // Ready Halts
            // -------------------------
            if (MainWindow.ReadyHalts() == true)
            {
                Task<int> task = MainWindow.StartProcess();
                int count = await task;
                
                // -------------------------
                // Write Variables to Debug Window
                // -------------------------
                DebugWrite(this);

                // -------------------------
                // Clear Variables for next Run
                // -------------------------
                MainWindow.ClearGlobalVariables();
                GC.Collect();
            }
        }


        /// <summary>
        /// Random Button
        /// </summary>
        private void btnDebugRandom_Click(object sender, RoutedEventArgs e)
        {
            // Run 500 Random Tests

            //for (var i = 0; i < 500; i++)
            //{
                try
                {
                    DebugControlRandomizer();
                    DebugTest();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            //}
        }


        /// <summary>
        /// Control Randomizer
        /// </summary>
        public void DebugControlRandomizer()
        {
            // -------------------------
            // Format
            // -------------------------
            // Container
            List<string> container = VM.FormatView.Format_Container_Items.Select(item => item.Name).ToList();
            for (int i = container.Count - 1; i >= 0; --i) // Remove Category Headers
            {
                if (container[i] == "Video" ||
                    container[i] == "Audio" ||
                    container[i] == "Image")
                {
                    container.RemoveAt(i);
                }
            }
            container.TrimExcess();

            VM.FormatView.Format_Container_SelectedItem = SelectRandom(container);

            // -------------------------
            // Video
            // -------------------------
            // Codec
            if (VM.VideoView.Video_Codec_IsEnabled == true)
            {
                VM.VideoView.Video_Codec_SelectedItem = SelectRandom(VM.VideoView.Video_Codec_Items);
            }

            // Encode Speed
            if (VM.VideoView.Video_EncodeSpeed_IsEnabled == true)
            {
                List<string> encodeSpeed = VM.VideoView.Video_EncodeSpeed_Items.Select(item => item.Name).ToList();
                VM.VideoView.Video_EncodeSpeed_SelectedItem = SelectRandom(encodeSpeed);
            }

            // Quality
            if (VM.VideoView.Video_Quality_IsEnabled == true)
            {
                List<string> quality = VM.VideoView.Video_Quality_Items.Select(item => item.Name).ToList();
                for (int i = quality.Count - 1; i >= 0; --i) // Remove Auto to prevent FFprobe execute
                {
                    if (quality[i] == "Auto") 
                    {
                        quality.RemoveAt(i);
                    }
                }
                quality.TrimExcess();

                VM.VideoView.Video_Quality_SelectedItem = SelectRandom(quality);
            }

            // Pass
            if (VM.VideoView.Video_Pass_IsEnabled == true)
            {
                VM.VideoView.Video_Pass_SelectedItem = SelectRandom(VM.VideoView.Video_Pass_Items);
            }

            // Pixel Format
            if (VM.VideoView.Video_PixelFormat_IsEnabled == true)
            {
                VM.VideoView.Video_PixelFormat_SelectedItem = SelectRandom(VM.VideoView.Video_PixelFormat_Items);
            }

            // Optimize
            if (VM.VideoView.Video_Optimize_IsEnabled == true)
            {
                List<string> optimize = VM.VideoView.Video_Quality_Items.Select(item => item.Name).ToList();
                VM.VideoView.Video_Optimize_SelectedItem = SelectRandom(optimize);
            }


            // -------------------------
            // Subtitle
            // -------------------------
            // Codec
            if (VM.SubtitleView.Subtitle_Codec_IsEnabled == true)
            {
                VM.SubtitleView.Subtitle_Codec_SelectedItem = SelectRandom(VM.SubtitleView.Subtitle_Codec_Items);
            }


            // -------------------------
            // Audio
            // -------------------------
            // Codec
            if (VM.AudioView.Audio_Codec_IsEnabled == true)
            {
                VM.AudioView.Audio_Codec_SelectedItem = SelectRandom(VM.AudioView.Audio_Codec_Items);
            }

            // Channel
            if (VM.AudioView.Audio_Channel_IsEnabled == true)
            {
                VM.AudioView.Audio_Channel_SelectedItem = SelectRandom(VM.AudioView.Audio_Channel_Items);
            }

            // Quality
            if (VM.AudioView.Audio_Quality_IsEnabled == true)
            {
                List<string> quality = VM.AudioView.Audio_Quality_Items.Select(item => item.Name).ToList();
                for (int i = quality.Count - 1; i >= 0; --i) // Remove Auto to prevent FFprobe execute
                {
                    if (quality[i] == "Auto")
                    {
                        quality.RemoveAt(i);
                    }
                }
                quality.TrimExcess();

                VM.AudioView.Audio_Quality_SelectedItem = SelectRandom(quality);
            }

            // Sample Rate
            if (VM.AudioView.Audio_SampleRate_IsEnabled == true)
            {
                List<string> sampleRate = VM.AudioView.Audio_SampleRate_Items.Select(item => item.Name).ToList();
                VM.AudioView.Audio_SampleRate_SelectedItem = SelectRandom(sampleRate);
            }

            // Bit Depth
            if (VM.AudioView.Audio_BitDepth_IsEnabled == true)
            {
                List<string> bitDpeth = VM.AudioView.Audio_BitDepth_Items.Select(item => item.Name).ToList();
                VM.AudioView.Audio_BitDepth_SelectedItem = SelectRandom(bitDpeth);
            }
        }

        /// <summary>
        /// Select Random
        /// </summary>
        public string SelectRandom(List<string> items)
        {
            // Return random item for SelectedItem
            Random rnd = new Random();
            int r = rnd.Next(items.Count);
            return items[r];
        }


        /// <summary>
        /// Debug Write
        /// </summary>
        public void DebugWrite(DebugConsole debugconsole)
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

            //debugParagraph.Inlines.Add(new Bold(new Run("ready ")) { Foreground = Variable });
            //debugParagraph.Inlines.Add(new Run(MainWindow.ready.ToString()) { Foreground = Value });
            //debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("ready ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.ReadyHalts().ToString()) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            //debugParagraph.Inlines.Add(new Bold(new Run("script ")) { Foreground = Variable });
            //debugParagraph.Inlines.Add(new Run(MainWindow.script.ToString()) { Foreground = Value });
            //debugParagraph.Inlines.Add(new LineBreak());

            //debugParagraph.Inlines.Add(new Bold(new Run("ffCheckCleared ")) { Foreground = Variable });
            //debugParagraph.Inlines.Add(new Run(MainWindow.ffCheckCleared.ToString()) { Foreground = Value });
            //debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("ffCheckCleared ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.FFcheck().ToString()) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("threads ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Configure.threads) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("maxthreads ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Configure.maxthreads) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("appDir ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(MainWindow.appRootDir) { Foreground = Value });
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
            debugParagraph.Inlines.Add(new Run(VM.ConfigureView.FFmpegPath_Text) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("ffprobePath ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(VM.ConfigureView.FFprobePath_Text) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("logPath ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(VM.ConfigureView.LogPath_Text) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("logEnable ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(VM.ConfigureView.LogCheckBox_IsChecked.ToString()) { Foreground = Value });
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
            debugParagraph.Inlines.Add(new Run(Video.hwacceleration) { Foreground = Value });
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

            debugParagraph.Inlines.Add(new Bold(new Run("vBitRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vBitRate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vMaxRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vMaxRate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vBufSize ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vBufSize) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vOptions ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vOptions) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("crf ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vCRF) { Foreground = Value });
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

            debugParagraph.Inlines.Add(new Bold(new Run("vAspectRatio ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Video.vAspectRatio) { Foreground = Value });
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

            debugParagraph.Inlines.Add(new Bold(new Run("aBitRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Audio.aBitRate) { Foreground = Value });
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

            //debugParagraph.Inlines.Add(new Bold(new Run("aBitRateLimiter ")) { Foreground = Variable });
            //debugParagraph.Inlines.Add(new Run(Audio.aBitRateLimiter) { Foreground = Value });
            //debugParagraph.Inlines.Add(new LineBreak());

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

            //debugParagraph.Inlines.Add(new Bold(new Run("trim ")) { Foreground = Variable });
            //debugParagraph.Inlines.Add(new Run(Format.trim) { Foreground = Value });
            //debugParagraph.Inlines.Add(new LineBreak());

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

            debugParagraph.Inlines.Add(new Bold(new Run("argsVideoBitRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.argsVideoBitRate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsAudioBitRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.argsAudioBitRate) { Foreground = Value });
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

            debugParagraph.Inlines.Add(new Bold(new Run("inputVideoBitRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.inputVideoBitRate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputAudioCodec ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.inputAudioCodec) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputAudioBitRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(FFprobe.inputAudioBitRate) { Foreground = Value });
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
