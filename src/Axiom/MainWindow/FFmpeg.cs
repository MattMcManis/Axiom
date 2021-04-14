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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Linq;
using ViewModel;
using System.IO;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Start Process
        /// </summary>
        //public static async void StartProcess()
        public static async Task<int> StartProcess()
        {
            int count = 0;
            await Task.Run(() =>
            {
                switch (VM.MainView.Batch_IsChecked)
                {
                    // -------------------------
                    // Single
                    // -------------------------
                    case false:
                        switch (IsWebURL(VM.MainView.Input_Text))
                        {
                            // -------------------------
                            // Local File
                            // -------------------------
                            case false:
                                // FFprobe Detect Metadata
                                Analyze.FFprobe.Metadata();

                                // Generate Single File FFmpeg Arguments
                                Generate.FFmpeg.Generate_SingleArgs();
                                break;

                            // -------------------------
                            // Web URL
                            // -------------------------
                            case true:
                                // Generate YouTube-DL Arguments
                                // Do not use FFprobe Metadata Parsing
                                // Video/Audio Auto Quality will add BitRate
                                Generate.FFmpeg.YouTubeDL.Generate_FFmpegArgs();
                                break;
                        }
                        break;

                    // -------------------------
                    // Batch
                    // -------------------------
                    case true:
                        switch (IsWebURL(VM.MainView.Input_Text))
                        {
                            // -------------------------
                            // Local File
                            // -------------------------
                            case false:
                                // FFprobe Video Entry Type Containers
                                Analyze.FFprobe.VideoEntryType();

                                // FFprobe Video Entry Type Containers
                                Analyze.FFprobe.AudioEntryType();

                                // Generate Batch FFmpeg Arguments
                                Generate.FFmpeg.Batch.Generate_FFmpegArgs();
                                break;

                            // -------------------------
                            // Web URL
                            // -------------------------
                            case true:
                                // Does not apply
                                break;
                        }
                        break;
                }
            });

            return count;
        }


        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Preview Button
        /// </summary>
        /// --------------------------------------------------------------------------------------------------------
        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            Preview.FFplay.Preview();
        }


        /// --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Convert Button
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
            if (CheckScriptEdited() == true)
            {
                // Halt
                return;
            }

            // -------------------------
            // Clear Global Variables before each Run
            // -------------------------
            ClearGlobalVariables();

            // -------------------------
            // Batch Extention Period Check
            // -------------------------
            BatchExtCheck();

            // -------------------------
            // Set FFprobe Path
            // -------------------------
            FFprobePath();

            // -------------------------
            // Set youtube-dl Path
            // -------------------------
            youtubedlPath();

            // --------------------------------------------------
            // Start Convert
            // --------------------------------------------------
            if (ReadyHalts() == true)
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("..............................................."))
                    { Foreground = Log.ConsoleAction });

                    DateTime localDate = DateTime.Now;

                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run(Convert.ToString(localDate))) { Foreground = Log.ConsoleAction });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Initializing...")) { Foreground = Log.ConsoleTitle });
                };
                Log.LogActions.Add(Log.WriteAction);

                VM.MainView.ScriptView_Text = "Generating...";
                // Delay to make Generating... text visible
                await Task.Delay(100);

                // -------------------------
                // Start All Processes
                // -------------------------
                //StartProcess();
                Task<int> startProcess = StartProcess();
                int count1 = await startProcess;

                // -------------------------
                // FFmpeg Convert
                // -------------------------
                //Encode.FFmpeg.FFmpegConvert();
                //Task<int> convert = Encode.FFmpeg.FFmpegConvertAsync();
                //int count2 = await convert;
                //Encode.FFmpeg.FFmpegConvert();

                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Converting...")) { Foreground = Log.ConsoleAction });
                };
                Log.LogActions.Add(Log.WriteAction);

                // -------------------------
                // Generate Controls Script
                // -------------------------
                // Inline
                // FFmpegArgs → ScriptView
                Task<int> script = Generate.FFmpeg.FFmpegGenerateScriptAsync();
                int count2 = await script;

                // -------------------------
                // Start FFmpeg
                // -------------------------
                Task<int> start = Encode.FFmpeg.FFmpegStartAsync(
                                        ReplaceLineBreaksWithSpaces(VM.MainView.ScriptView_Text)
                                    );
                int count3 = await start;

                //// -------------------------
                //// Sort Script
                //// -------------------------
                //// Only if Auto Sort is enabled
                //if (VM.MainView.AutoSortScript_IsChecked == true)
                //{
                //    Controls.ScriptView.sort = false;
                //    Sort();
                //}

                // -------------------------
                // Write All Log Actions to Log Console
                // -------------------------
                Log.LogWriteAll(this);

                // -------------------------
                // Create output.log
                // -------------------------
                Log.CreateOutputLog(this);

                // -------------------------
                // Clear Global Variables before each Run
                // -------------------------
                //ClearGlobalVariables();
                GC.Collect();
            }

            // -------------------------
            // Update Output TextBox Text
            // -------------------------
            UpdateOutputTextBoxText();
        }


        /// <summary>
        /// Convert Button Text Change (Method)
        /// </summary>
        public static void ConvertButtonText()
        {
            //MessageBox.Show(VM.MainView.Input_Text); //debug

            // Change to "Download" if YouTube Download-Only Mode
            if ((IsWebURL(VM.MainView.Input_Text) == true || IsYouTubeURL(VM.MainView.Input_Text) == true) &&
                IsWebDownloadOnly(VM.VideoView.Video_Codec_SelectedItem,
                                  VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                  VM.AudioView.Audio_Codec_SelectedItem) == true
                )
            {
                VM.MainView.Convert_Text = "Download";
            }

            // Change to Convert if User Defined Custom Settings
            else
            {
                VM.MainView.Convert_Text = "Convert";
            }
        }

    }
}
