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
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Linq;
using ViewModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Sort (Method)
        /// </summary>
        public void Sort()
        {
            // Only if Script not empty
            if (!string.IsNullOrWhiteSpace(VM.MainView.ScriptView_Text))
            {
                // -------------------------
                // Has Not Been Edited
                // -------------------------
                if (Controls.ScriptView.sort == false &&
                    RemoveLineBreaks(VM.MainView.ScriptView_Text) == Generate.FFmpeg.ffmpegArgs)
                {
                    VM.MainView.ScriptView_Text = Generate.FFmpeg.ffmpegArgsSort;

                    // Sort is Off
                    Controls.ScriptView.sort = true;
                    // Change Button Back to Inline
                    txblScriptSort.Text = "Inline";
                }

                // -------------------------
                // Has Been Edited
                // -------------------------
                else if (Controls.ScriptView.sort == false &&
                         RemoveLineBreaks(VM.MainView.ScriptView_Text) != Generate.FFmpeg.ffmpegArgs)
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
                else if (Controls.ScriptView.sort == true)
                {
                    // CMD Arguments are from Script TextBox
                    Generate.FFmpeg.ffmpegArgs = RemoveLineBreaks(VM.MainView.ScriptView_Text);

                    VM.MainView.ScriptView_Text = Generate.FFmpeg.ffmpegArgs;

                    // Sort is On
                    Controls.ScriptView.sort = false;
                    // Change Button Back to Sort
                    txblScriptSort.Text = "Sort";
                }
            }
        }


        /// <summary>
        /// Check if Script has been Edited (Method)
        /// </summary>
        public static bool CheckScriptEdited()
        {
            bool edited = false;

            // -------------------------
            // Check if Script has been modified
            // -------------------------
            if (!string.IsNullOrWhiteSpace(VM.MainView.ScriptView_Text) &&
                !string.IsNullOrWhiteSpace(Generate.FFmpeg.ffmpegArgs))
            {
                //MessageBox.Show(RemoveLineBreaks(ScriptView.GetScriptRichTextBoxContents(mainwindow))); //debug
                //MessageBox.Show(Generate.FFmpeg.ffmpegArgs); //debug

                // Compare RichTextBox Script Against FFmpeg Generated Args
                if (RemoveLineBreaks(VM.MainView.ScriptView_Text) != RemoveLineBreaks(Generate.FFmpeg.ffmpegArgs))
                {
                    //MessageBox.Show(RemoveLineBreaks(VM.MainView.ScriptView_Text)); //debug
                    //MessageBox.Show(RemoveLineBreaks(Generate.FFmpeg.ffmpegArgs)); //debug

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
        /// Script View Copy/Paste
        /// </summary>
        //private void OnScriptPaste(object sender, DataObjectPastingEventArgs e)
        //{
        //}

        //private void OnScriptCopy(object sender, DataObjectCopyingEventArgs e)
        //{
        //}


        /// <summary>
        /// Script - Button
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

            // -------------------------
            // Reset Sort
            // -------------------------
            Controls.ScriptView.sort = false;
            txblScriptSort.Text = "Sort";


            // -------------------------
            // Start Script
            // -------------------------
            if (ReadyHalts() == true)
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
                //Stopwatch sw = new Stopwatch(); // Performance Test
                //sw.Start();
                Task<int> task = StartProcess();
                int count = await task;
                //sw.Stop();
                //MessageBox.Show(sw.Elapsed.ToString());


                // -------------------------
                // Generate Script
                // -------------------------
                Generate.FFmpeg.FFmpegGenerateScript();

                // -------------------------
                // Auto Sort Toggle
                // -------------------------
                if (VM.MainView.AutoSortScript_IsChecked == true)
                {
                    Sort();
                }

                // -------------------------
                // Write All Log Actions to Console
                // -------------------------
                Log.LogWriteAll(this);

                // -------------------------
                // Clear Variables for next Run
                // -------------------------
                ClearGlobalVariables();
                GC.Collect();
            }
        }



        /// <summary>
        /// Save Script
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
                File.WriteAllText(saveFile.FileName, VM.MainView.ScriptView_Text, Encoding.Unicode);
            }
        }


        /// <summary>
        /// Copy All Button
        /// </summary>
        private void btnScriptCopy_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(VM.MainView.ScriptView_Text))
            {
                //Clipboard.SetText(ScriptView.GetScriptRichTextBoxContents(this), TextDataFormat.UnicodeText);
                Clipboard.SetText(VM.MainView.ScriptView_Text, TextDataFormat.UnicodeText);
            }
        }


        /// <summary>
        /// Clear Button
        /// </summary>
        private void btnScriptClear_Click(object sender, RoutedEventArgs e)
        {
            Controls.ScriptView.ClearScriptView();
        }


        /// <summary>
        /// Sort Button
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
            if (!string.IsNullOrWhiteSpace(VM.MainView.ScriptView_Text))
            {
                // -------------------------
                // Use Arguments from Script TextBox
                // -------------------------
                Generate.FFmpeg.ffmpegArgs = ReplaceLineBreaksWithSpaces(
                                        VM.MainView.ScriptView_Text
                                    );

                // -------------------------
                // Start FFmpeg
                // -------------------------
                Generate.FFmpeg.FFmpegStart(Generate.FFmpeg.ffmpegArgs);

                // -------------------------
                // Create output.log
                // -------------------------
                Log.CreateOutputLog(this);
            }
        }

    }
}
