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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using ViewModel;
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
            this.MinWidth = 400;
            this.MinHeight = 500;

            // -------------------------
            // Text Theme SelectiveColorPreview
            // -------------------------
            //Controls.Configure.LoadTheme(mainwindow);

            // -------------------------
            // Debug Text Theme SelectiveColorPreview
            // -------------------------
            if (Controls.Configure.theme == "Axiom")
            {
                DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2"));
                DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8"));
                DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            }
            else if (Controls.Configure.theme == "FFmpeg")
            {
                DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#878787"));
                DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cb85c"));
                DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            }
            else if (Controls.Configure.theme == "Cyberpunk")
            {
                DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9a989c"));
                DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9f3ed2"));
                DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            }
            else if (Controls.Configure.theme == "Onyx")
            {
                DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EEEEEE"));
                DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            }
            else if (Controls.Configure.theme == "Circuit")
            {
                DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ad8a4a"));
                DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2ebf93"));
                DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            }
            else if (Controls.Configure.theme == "Prelude")
            {
                DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ad8a4a"));
                DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2ebf93"));
                DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
            }
            else if (Controls.Configure.theme == "System")
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
            Log.logParagraph.Inlines.Add(new Run(Controls.Configure.theme) { Foreground = Log.ConsoleDefault });

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
        /// Methods Button
        /// </summary>
        private void btnDebugMethods_Click(object sender, RoutedEventArgs e)
        {
            try { MainWindow.BatchExtCheck(); } catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try { MainWindow.FFmpegPath(); } catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try { MainWindow.FFprobePath(); } catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try { MainWindow.FFplayPath(); } catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try { MainWindow.youtubedlPath(); } catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Video.Encoding.HWAccelerationDecode(VM.FormatView.Format_MediaType_SelectedItem,
                                             VM.VideoView.Video_Codec_SelectedItem,
                                             VM.VideoView.Video_HWAccel_Decode_SelectedItem
                                             );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                MainWindow.InputPath("pass 1");
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                MainWindow.OutputPath();
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Subtitle.Subtitle.SubtitlesMux(VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                        VM.SubtitleView.Subtitle_Stream_SelectedItem
                                                       );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Video.Encoding.HWAccelerationTranscode(VM.FormatView.Format_MediaType_SelectedItem,
                                                  VM.VideoView.Video_Codec_SelectedItem,
                                                  VM.VideoView.Video_HWAccel_Transcode_SelectedItem
                                                  );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Format.CutStart(VM.MainView.Input_Text,
                                    VM.MainView.Batch_IsChecked,
                                    VM.FormatView.Format_Cut_SelectedItem,
                                    VM.FormatView.Format_CutStart_Hours_Text,
                                    VM.FormatView.Format_CutStart_Minutes_Text,
                                    VM.FormatView.Format_CutStart_Seconds_Text,
                                    VM.FormatView.Format_CutStart_Milliseconds_Text,
                                    VM.FormatView.Format_FrameStart_Text
                                    );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Format.CutEnd(VM.MainView.Input_Text,
                                  VM.MainView.Batch_IsChecked,
                                  VM.FormatView.Format_MediaType_SelectedItem,
                                  VM.FormatView.Format_Cut_SelectedItem,
                                  VM.FormatView.Format_CutEnd_Hours_Text,
                                  VM.FormatView.Format_CutEnd_Minutes_Text,
                                  VM.FormatView.Format_CutEnd_Seconds_Text,
                                  VM.FormatView.Format_CutEnd_Milliseconds_Text,
                                  VM.FormatView.Format_FrameEnd_Text
                                  );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Video.Codec.VideoCodec(VM.VideoView.Video_HWAccel_Transcode_SelectedItem,
                                         VM.VideoView.Video_Codec_SelectedItem,
                                         VM.VideoView.Video_Codec
                                         );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Video.Params.QualityParams(VM.VideoView.Video_Quality_SelectedItem,
                                                    VM.VideoView.Video_Codec_SelectedItem,
                                                    VM.FormatView.Format_MediaType_SelectedItem
                                                   );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Video.Encoding.VideoEncodeSpeed(VM.VideoView.Video_EncodeSpeed_Items,
                                               VM.VideoView.Video_EncodeSpeed_SelectedItem,
                                               VM.VideoView.Video_Codec_SelectedItem,
                                               VM.VideoView.Video_Pass_SelectedItem
                                               );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Video.Quality.VideoQuality(VM.MainView.Batch_IsChecked,
                                           (bool)VM.VideoView.Video_VBR_IsChecked,
                                           VM.FormatView.Format_Container_SelectedItem,
                                           VM.FormatView.Format_MediaType_SelectedItem,
                                           VM.VideoView.Video_Codec_SelectedItem,
                                           VM.VideoView.Video_Quality_Items,
                                           VM.VideoView.Video_Quality_SelectedItem,
                                           VM.VideoView.Video_Pass_SelectedItem,
                                           VM.VideoView.Video_CRF_Text,
                                           VM.VideoView.Video_BitRate_Text,
                                           VM.VideoView.Video_MinRate_Text,
                                           VM.VideoView.Video_MaxRate_Text,
                                           VM.VideoView.Video_BufSize_Text,
                                           VM.MainView.Input_Text
                                           );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Video.Quality.PixFmt(VM.VideoView.Video_Codec_SelectedItem,
                                     VM.VideoView.Video_PixelFormat_SelectedItem
                                     );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Video.Color.Color_Primaries(VM.VideoView.Video_Color_Primaries_SelectedItem);
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Video.Color.Color_TransferCharacteristics(VM.VideoView.Video_Color_TransferCharacteristics_SelectedItem);
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Video.Color.Color_Space(VM.VideoView.Video_Color_Space_SelectedItem);
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Video.Color.Color_Range(VM.VideoView.Video_Color_Range_SelectedItem);
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Video.Video.FPS(VM.VideoView.Video_Codec_SelectedItem,
                                          VM.VideoView.Video_FPS_SelectedItem,
                                          VM.VideoView.Video_FPS_Text
                                          );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Filters.Video.VideoFilter();
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Video.Size.AspectRatio(VM.VideoView.Video_AspectRatio_SelectedItem);
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Video.Video.Images(VM.FormatView.Format_MediaType_SelectedItem,
                                             VM.VideoView.Video_Codec_SelectedItem
                                             );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Video.Quality.Optimize(VM.VideoView.Video_Codec_SelectedItem,
                                       VM.VideoView.Video_Optimize_Items,
                                       VM.VideoView.Video_Optimize_SelectedItem,
                                       VM.VideoView.Video_Optimize_Tune_SelectedItem,
                                       VM.VideoView.Video_Optimize_Profile_SelectedItem,
                                       VM.VideoView.Video_Optimize_Level_SelectedItem
                                       );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Streams.VideoStreamMaps();
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Subtitle.Subtitle.SubtitleCodec(VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                               VM.SubtitleView.Subtitle_Codec
                                               );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Streams.SubtitleMaps();
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Audio.Codec.AudioCodec(VM.AudioView.Audio_Codec_SelectedItem,
                                       VM.AudioView.Audio_Codec
                                       );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Audio.Quality.AudioQuality(VM.MainView.Input_Text,
                                           VM.MainView.Batch_IsChecked,
                                           VM.FormatView.Format_MediaType_SelectedItem,
                                           VM.AudioView.Audio_Stream_SelectedItem,
                                           VM.AudioView.Audio_Codec_SelectedItem,
                                           VM.AudioView.Audio_Quality_Items,
                                           VM.AudioView.Audio_Quality_SelectedItem,
                                           VM.AudioView.Audio_BitRate_Text,
                                           (bool)VM.AudioView.Audio_VBR_IsChecked
                                           );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Audio.Quality.CompressionLevel(VM.AudioView.Audio_Codec_SelectedItem,
                                               VM.AudioView.Audio_CompressionLevel_SelectedItem
                                               );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Audio.Quality.SampleRate(VM.AudioView.Audio_Codec_SelectedItem,
                                         VM.AudioView.Audio_SampleRate_Items,
                                         VM.AudioView.Audio_SampleRate_SelectedItem
                                         );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Audio.Quality.BitDepth(VM.AudioView.Audio_Codec_SelectedItem,
                                       VM.AudioView.Audio_BitDepth_Items,
                                       VM.AudioView.Audio_BitDepth_SelectedItem
                                       );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Audio.Channels.Channel(VM.AudioView.Audio_Codec_SelectedItem,
                                       VM.AudioView.Audio_Channel_SelectedItem
                                       );
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Filters.Audio.AudioFilter();
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Streams.AudioStreamMaps();
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Streams.FormatMaps();
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                Generate.Format.ForceFormat(VM.FormatView.Format_Container_SelectedItem);
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try
            {
                MainWindow.ThreadDetect();
            }
            catch (Exception exception) { MessageBox.Show(exception.ToString()); }

            try { MainWindow.ClearGlobalVariables(); } catch (Exception exception) { MessageBox.Show(exception.ToString()); }
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
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(),
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
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
                VM.VideoView.Video_Codec_SelectedItem = SelectRandom(VM.VideoView.Video_Codec_Items.ToList());
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
                VM.VideoView.Video_Pass_SelectedItem = SelectRandom(VM.VideoView.Video_Pass_Items.ToList());
            }

            // Pixel Format
            if (VM.VideoView.Video_PixelFormat_IsEnabled == true)
            {
                VM.VideoView.Video_PixelFormat_SelectedItem = SelectRandom(VM.VideoView.Video_PixelFormat_Items.ToList());
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
                VM.SubtitleView.Subtitle_Codec_SelectedItem = SelectRandom(VM.SubtitleView.Subtitle_Codec_Items.ToList());
            }


            // -------------------------
            // Audio
            // -------------------------
            // Codec
            if (VM.AudioView.Audio_Codec_IsEnabled == true)
            {
                VM.AudioView.Audio_Codec_SelectedItem = SelectRandom(VM.AudioView.Audio_Codec_Items.ToList());
            }

            // Channel
            if (VM.AudioView.Audio_Channel_IsEnabled == true)
            {
                VM.AudioView.Audio_Channel_SelectedItem = SelectRandom(VM.AudioView.Audio_Channel_Items.ToList());
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
            debugParagraph.Inlines.Add(new Run(Controls.Configure.threads) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("maxthreads ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Controls.Configure.maxthreads) { Foreground = Value });
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
            debugParagraph.Inlines.Add(new Run(Controls.Configure.theme) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("ffmpeg ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.FFmpeg.ffmpeg) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("ffprobe ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.ffprobe) { Foreground = Value });
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
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.batchFFprobeAuto) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("batchVideoAuto ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Quality.batchVideoAuto) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("batchAudioAuto ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Audio.Quality.batchAudioAuto) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());
            debugParagraph.Inlines.Add(new LineBreak());


            // Video
            debugParagraph.Inlines.Add(new Bold(new Run("Video ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("hwAccelDecode ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Encoding.hwAccelDecode) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("hwAccelTranscode ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Encoding.hwAccelTranscode) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vEncodeSpeed ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Encoding.vEncodeSpeed) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vCodec ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Codec.vCodec) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vQuality ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Quality.vQuality) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vLossless ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Quality.vLossless) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            //debugParagraph.Inlines.Add(new Bold(new Run("vBitMode ")) { Foreground = Variable });
            //debugParagraph.Inlines.Add(new Run(Generate.Video.vBitMode) { Foreground = Value });
            //debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vBitRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Quality.vBitRate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vMaxRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Quality.vMaxRate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vBufSize ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Quality.vBufSize) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vOptions ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Quality.vOptions) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("crf ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Quality.vCRF) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            //debugParagraph.Inlines.Add(new Bold(new Run("x265params ")) { Foreground = Variable });
            //debugParagraph.Inlines.Add(new Run(Generate.Video.x265params) { Foreground = Value });
            //debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vParams ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Params.vParams) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("fps ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Video.fps) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("image ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Video.image) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vEncodeSpeed ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Encoding.vEncodeSpeed) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("passUserSelected ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Controls.Video.Controls.passUserSelected.ToString()) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("pass1 ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Quality.pass1) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("pass2 ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Quality.pass2) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("v2passArgs ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Quality.v2PassArgs) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("optTune ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Quality.optTune) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("optProfile ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Quality.optProfile) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("optLevel ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Quality.optLevel) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("optimize ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Quality.optimize) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());


            debugParagraph.Inlines.Add(new Bold(new Run("width ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Size.width) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("height ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Video.Size.height) { Foreground = Value });
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
            debugParagraph.Inlines.Add(new Run(Generate.Video.Size.vAspectRatio) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("geq ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Filters.Video.geq) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vFilter ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Filters.Video.vFilter) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());
            debugParagraph.Inlines.Add(new LineBreak());


            // Audio
            debugParagraph.Inlines.Add(new Bold(new Run("Audio ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aCodec ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Audio.Codec.aCodec) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aQuality ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Audio.Quality.aQuality) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aBitMode ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Audio.Quality.aBitMode) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aBitRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Audio.Quality.aBitRate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aChannel ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Audio.Channels.aChannel) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aSamplerate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Audio.Quality.aSamplerate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aBitDepth ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Audio.Quality.aBitDepth) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("volume ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Filters.Audio.aVolume) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aHardLimiter ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Filters.Audio.aHardLimiter) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aFilter ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Filters.Audio.aFilter) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            //debugParagraph.Inlines.Add(new Bold(new Run("aBitRateLimiter ")) { Foreground = Variable });
            //debugParagraph.Inlines.Add(new Run(Generate.Audio.aBitRateLimiter) { Foreground = Value });
            //debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());
            debugParagraph.Inlines.Add(new LineBreak());


            // Streams
            debugParagraph.Inlines.Add(new Bold(new Run("Streams ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vMap ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Streams.vMap) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aMap ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Streams.aMap) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("cMap ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Streams.cMap) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("sMap ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Streams.sMap) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("mMap ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Streams.mMap) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            //debugParagraph.Inlines.Add(new Bold(new Run("map ")) { Foreground = Variable });
            //debugParagraph.Inlines.Add(new Run(Generate.Streams.map) { Foreground = Value });
            //debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());
            debugParagraph.Inlines.Add(new LineBreak());


            // Cut
            debugParagraph.Inlines.Add(new Bold(new Run("Cut ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("trimStart ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Format.trimStart) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("trimEnd ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Generate.Format.trimEnd) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            //debugParagraph.Inlines.Add(new Bold(new Run("trim ")) { Foreground = Variable });
            //debugParagraph.Inlines.Add(new Run(Generate.Format.trim) { Foreground = Value });
            //debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());
            debugParagraph.Inlines.Add(new LineBreak());


            // FFprobe
            debugParagraph.Inlines.Add(new Bold(new Run("FFprobe ")) { Foreground = Heading });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsProperties ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.argsProperties) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsVideoCodec ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.argsVideoCodec) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsAudioCodec ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.argsAudioCodec) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsVideoBitRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.argsVideoBitRate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsAudioBitRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.argsAudioBitRate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsSize ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.argsSize) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsDuration ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.argsDuration) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("argsFrameRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.argsFrameRate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());


            debugParagraph.Inlines.Add(new Bold(new Run("inputFileProperties ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.inputFileProperties) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputVideoCodec ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.inputVideoCodec) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputVideoBitRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.inputVideoBitRate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputAudioCodec ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.inputAudioCodec) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputAudioBitRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.inputAudioBitRate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputSize ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.inputSize) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputDuration ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.inputDuration) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("inputFrameRate ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.inputFrameRate) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("vEntryType ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.vEntryType) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            debugParagraph.Inlines.Add(new Bold(new Run("aEntryType ")) { Foreground = Variable });
            debugParagraph.Inlines.Add(new Run(Analyze.FFprobe.aEntryType) { Foreground = Value });
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
            debugParagraph.Inlines.Add(new Run(Generate.FFmpeg.ffmpegArgs) { Foreground = Value });
            debugParagraph.Inlines.Add(new LineBreak());

            //////////////////////////////////////////////////
            debugconsole.rtbDebug.EndChange(); // end change
        }

    }
}
