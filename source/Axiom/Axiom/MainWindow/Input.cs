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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        /// Input Button
        /// </summary>
        private void btnInput_Click(object sender, RoutedEventArgs e)
        {
            switch (VM.MainView.Batch_IsChecked)
            {
                // -------------------------
                // Single File
                // -------------------------
                case false:
                    // Open Select File Window
                    //Microsoft.Win32.OpenFileDialog selectFile = new Microsoft.Win32.OpenFileDialog();
                    Microsoft.Win32.OpenFileDialog selectFile = new Microsoft.Win32.OpenFileDialog
                    {
                        CheckFileExists = true,
                        CheckPathExists = true,
                        //RestoreDirectory = true,
                        //ReadOnlyChecked = true,
                        //ShowReadOnly = true
                    };

                    //// Remember Last Dir
                    ////
                    //try
                    //{
                    //    //string previousPath = Settings.Default.InputDir.ToString();
                    //    inputPreviousPath = string.Empty;

                    //    if (File.Exists(Controls.Configure.configFile))
                    //    {
                    //        Controls.Configure.ConigFile conf = new Controls.Configure.ConigFile(Controls.Configure.configFile);
                    //        inputPreviousPath = conf.Read("User", "InputPreviousPath");

                    //        // Use Previous Path if Not Empty
                    //        if (!string.IsNullOrWhiteSpace(inputPreviousPath))
                    //        {
                    //            selectFile.InitialDirectory = inputPreviousPath;
                    //        }
                    //    }
                    //}
                    //catch
                    //{

                    //}

                    // Show Dialog Box
                    //Nullable<bool> result = selectFile.ShowDialog();

                    // Process Dialog Box
                    //if (result == true)
                    if (selectFile.ShowDialog() == true)
                    {
                        // Display path and file in Output Textbox
                        VM.MainView.Input_Text = selectFile.FileName;

                        // Set Input Dir, Name, Ext
                        inputDir = Path.GetDirectoryName(selectFile.FileName).TrimEnd('\\') + @"\";

                        inputFileName = Path.GetFileNameWithoutExtension(selectFile.FileName);

                        inputExt = Path.GetExtension(selectFile.FileName);

                        // Clear Output TextBox
                        VM.MainView.Output_Text = string.Empty;

                        //// Save Previous Path
                        //if (File.Exists(Controls.Configure.configFile))
                        //{
                        //    try
                        //    {
                        //        Controls.Configure.ConigFile conf = new Controls.Configure.ConigFile(Controls.Configure.configFile);
                        //        conf.Write("User", "InputPreviousPath", inputDir);
                        //    }
                        //    catch
                        //    {

                        //    }
                        //}
                    }

                    // --------------------------------------------------
                    // Default Auto if Input Extension matches Output Extsion
                    // This will trigger Auto Codec Copy
                    // --------------------------------------------------
                    //ExtensionMatchLoadAutoValues();

                    // -------------------------
                    // Prevent Losing Codec Copy after cancel closing Browse Folder Dialog Box 
                    // Set Video & Audio Codec Combobox to "Copy" if Input Extension is Same as Output Extension and Video Quality is Auto
                    // -------------------------
                    //VideoControls.AutoCopyVideoCodec("input");
                    //SubtitleControls.AutoCopySubtitleCodec("input");
                    //AudioControls.AutoCopyAudioCodec("input");
                    break;

                // -------------------------
                // Batch
                // -------------------------
                case true:
                    // Open Batch Folder
                    System.Windows.Forms.FolderBrowserDialog inputFolder = new System.Windows.Forms.FolderBrowserDialog();
                    System.Windows.Forms.DialogResult resultBatch = inputFolder.ShowDialog();

                    // Show Input Dialog Box
                    if (resultBatch == System.Windows.Forms.DialogResult.OK)
                    {
                        // Display Folder Path in Textbox
                        VM.MainView.Input_Text = inputFolder.SelectedPath.TrimEnd('\\') + @"\";

                        // Input Directory
                        //inputDir = Path.GetDirectoryName(VM.MainView.Input_Text.TrimEnd('\\') + @"\");
                        inputDir = VM.MainView.Input_Text.TrimEnd('\\') + @"\"; // Note: Do not use Path.GetDirectoryName() with Batch Path only
                                                                                //       It will remove the last dir as a file extension
                    }

                    // -------------------------
                    // Prevent Losing Codec Copy after cancel closing Browse Folder Dialog Box 
                    // Set Video and AudioCodec Combobox to "Copy" if 
                    // Input File Extension is Same as Output File Extension 
                    // and Quality is Auto
                    // -------------------------
                    //VideoControls.AutoCopyVideoCodec("input");
                    //SubtitleControls.AutoCopySubtitleCodec("input");
                    //AudioControls.AutoCopyAudioCodec("input");
                    break;
            }
        }



        /// <summary>
        /// Input Textbox
        /// </summary>
        private void tbxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            input = VM.MainView.Input_Text;

            // -------------------------
            // Local File
            // -------------------------
            if (IsWebURL(VM.MainView.Input_Text) == false)
            {
                // -------------------------
                // Single File
                // -------------------------
                if (VM.MainView.Batch_IsChecked == false)
                {
                    // Has Text
                    if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
                    {
                        // Remove stray slash if closed out early
                        if (input == @"\")
                        {
                            input = string.Empty;
                        }

                        // Do not set inputDir

                        // Input Extension
                        inputExt = Path.GetExtension(VM.MainView.Input_Text);
                    }

                    // No Text
                    else
                    {
                        input = string.Empty;
                        inputDir = string.Empty;
                    }
                }

                // -------------------------
                // Batch
                // -------------------------
                else
                {
                    // Has Text
                    if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
                    {
                        // Remove stray slash if closed out early
                        if (input == @"\")
                        {
                            input = string.Empty;
                        }

                        inputDir = VM.MainView.Input_Text.TrimEnd('\\') + @"\"; // Note: Do not use Path.GetDirectoryName() with Batch Path only
                                                                                //       It will remove the last dir as a file extension
                        inputExt = VM.MainView.BatchExtension_Text;
                    }

                    // No Text
                    else
                    {
                        input = string.Empty;
                        inputDir = string.Empty;
                        inputExt = string.Empty;
                    }
                }

                // -------------------------
                // Enable / Disable "Open Input Location" Button
                // -------------------------
                if (IsValidPath(VM.MainView.Input_Text) == true && // Detect Invalid Characters
                    Path.IsPathRooted(VM.MainView.Input_Text) == true // TrimEnd('\\') + @"\" is adding a backslash to 
                                                                      // Iput text 'http' until it is detected as Web URL
                    )
                {
                    bool exists = Directory.Exists(Path.GetDirectoryName(VM.MainView.Input_Text));

                    // Path exists
                    if (exists)
                    {
                        //VM.MainView.Input_Clear_IsEnabled = true;
                        VM.MainView.Input_Location_IsEnabled = true;
                    }
                    // Path does not exist
                    else
                    {
                        //VM.MainView.Input_Clear_IsEnabled = false;
                        VM.MainView.Input_Location_IsEnabled = false;
                    }
                }
                // Disable for Web URL
                else
                {
                    //VM.MainView.Input_Clear_IsEnabled = false;
                    VM.MainView.Input_Location_IsEnabled = false;
                }

                //// -------------------------
                //// Set Video & Audio Codec Combobox to "Copy" 
                //// if Input Extension is Same as Output Extension and Video Quality is Auto
                //// -------------------------
                //if (IsWebURL(VM.MainView.Input_Text) == false) // Check if Input is a Windows Path, Not a URL
                //{
                //    if (Path.HasExtension(VM.MainView.Input_Text) == true && // Check if Input has file extension after it has passed URL check
                //                                                             // to prevent path forward slash error in Path.HasExtension()

                //        !VM.MainView.Input_Text.Contains("youtube"))         // Input text does not contain "youtube", 
                //                                                             // Path.HasExtension() detects .c, .co, .com as extension

                //    {
                //        // -------------------------
                //        // Set Video and AudioCodec Combobox to "Copy" if 
                //        // Input File Extension is Same as Output File Extension 
                //        // and Quality is Auto
                //        // -------------------------
                //        //VideoControls.AutoCopyVideoCodec("input");
                //        //SubtitleControls.AutoCopySubtitleCodec("input");
                //        //AudioControls.AutoCopyAudioCodec("input");
                //    }
                //}
            }

            // -------------------------
            // Web URL
            // -------------------------
            else
            {
                inputDir = string.Empty;
                inputExt = string.Empty;
                VM.MainView.Input_Location_IsEnabled = false;
            }

            // -------------------------
            // Enable / Disable "Input Clear" Button
            // -------------------------
            // Disable
            if (string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
            {
                VM.MainView.Input_Clear_IsEnabled = false;
            }
            // Enable
            else
            {
                VM.MainView.Input_Clear_IsEnabled = true;
            }

            // -------------------------
            // Convert Button Text Change
            // -------------------------
            // YouTube Download
            ConvertButtonText();
        }

        /// <summary>
        /// Input Textbox - Drag and Drop
        /// </summary>
        private void tbxInput_PreviewDragOver(object sender, DragEventArgs e)
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

        private void tbxInput_PreviewDrop(object sender, DragEventArgs e)
        {
            try
            {
                var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
                VM.MainView.Input_Text = buffer.First();

                // Set Input Dir, Name, Ext
                inputDir = Path.GetDirectoryName(VM.MainView.Input_Text).TrimEnd('\\') + @"\";
                inputFileName = Path.GetFileNameWithoutExtension(VM.MainView.Input_Text);
                inputExt = Path.GetExtension(VM.MainView.Input_Text);

                // Clear Output TextBox
                VM.MainView.Output_Text = string.Empty;

                // -------------------------
                // Set Video and AudioCodec Combobox to "Copy" if 
                // Input File Extension is Same as Output File Extension 
                // and Quality is Auto
                // -------------------------
                //VideoControls.AutoCopyVideoCodec("input");
                //SubtitleControls.AutoCopySubtitleCodec("input");
                //AudioControls.AutoCopyAudioCodec("input");
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(),
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Input TextBox Clear - Button
        /// </summary>
        private void btnInputClear_Click(object sender, RoutedEventArgs e)
        {
            VM.MainView.Input_Text = string.Empty;

            inputDir = string.Empty;
            inputFileName = string.Empty;
            inputExt = string.Empty;
            input = string.Empty;
        }

        /// <summary>
        /// Open Input Folder - Button
        /// </summary>
        private void openLocationInput_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidPath(inputDir))
            {
                if (Directory.Exists(@inputDir))
                {
                    Process.Start("explorer.exe", @inputDir);
                }
            }
        }


        /// <summary>
        /// Input Path
        /// </summary>
        public static String InputPath(string pass)
        {
            if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
            {
                // -------------------------
                // Local File
                // -------------------------
                if (IsWebURL(VM.MainView.Input_Text) == false) // Ignore Web URL's
                {
                    // -------------------------
                    // Single File
                    // -------------------------
                    if (VM.MainView.Batch_IsChecked == false &&
                        pass != "pass 2") // Ignore Pass 2, use existing input path
                    {
                        // Input Directory
                        //if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
                        //{
                        inputDir = Path.GetDirectoryName(VM.MainView.Input_Text).TrimEnd('\\') + @"\"; // eg. C:\Input\Path\
                        inputFileName = Path.GetFileNameWithoutExtension(VM.MainView.Input_Text);
                        inputExt = Path.GetExtension(VM.MainView.Input_Text);
                        //}

                        // Combine Input
                        input = VM.MainView.Input_Text; // eg. C:\Path\To\file.avi
                    }

                    // -------------------------
                    // Batch
                    // -------------------------
                    else if (VM.MainView.Batch_IsChecked == true)
                    {
                        inputDir = VM.MainView.Input_Text.TrimEnd('\\') + @"\";  // Note: Do not use Path.GetDirectoryName() with Batch Path only
                                                                                 //       It will remove the last dir as a file extension
                        inputExt = Path.GetExtension(VM.MainView.BatchExtension_Text);
                        // -------------------------
                        // CMD
                        // -------------------------
                        if (VM.ConfigureView.Shell_SelectedItem == "CMD")
                        {
                            // Note: %f is filename, %~f is full path
                            //inputFileName = "%~f";
                            inputFileName = "%f";
                        }
                        // -------------------------
                        // PowerShell
                        // -------------------------
                        else if (VM.ConfigureView.Shell_SelectedItem == "PowerShell")
                        {
                            //inputFileName = "$name"/*+ inputExt*/;
                            inputFileName = "$inputName"/*+ inputExt*/;
                        }

                        // Combine Input
                        input = inputDir + inputFileName; // eg. C:\Input\Path\%~f
                        //Path.Combine(inputDir, inputFileName); // Path.Combine does not work with %~f
                    }
                }

                // -------------------------
                // YouTube Download
                // -------------------------
                else if (IsWebURL(VM.MainView.Input_Text) == true &&
                         pass != "pass 2") // Ignore Pass 2, use existing input path
                {
                    inputDir = downloadDir;

                    //inputFileName = "%f";

                    //// -------------------------
                    //// CMD
                    //// -------------------------
                    //if (VM.ConfigureView.Shell_SelectedItem == "CMD")
                    //{
                    //    // Note: %f is filename, %~f is full path
                    //    inputFileName = "%f";
                    //}
                    //// -------------------------
                    //// PowerShell
                    //// -------------------------
                    //else if (VM.ConfigureView.Shell_SelectedItem == "PowerShell")
                    //{
                    //    inputFileName = "$name";
                    //}

                    // Shell Check
                    switch (VM.ConfigureView.Shell_SelectedItem)
                    {
                        // CMD
                        case "CMD":
                            inputFileName = "%f";
                            break;

                        // PowerShell
                        case "PowerShell":
                            inputFileName = "$name";
                            break;
                    }

                    inputExt = "." + YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
                                                           VM.VideoView.Video_Codec_SelectedItem,
                                                           VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                           VM.AudioView.Audio_Codec_SelectedItem
                                                           );

                    //input = inputDir + inputFileName + inputExt; // eg. C:\Users\Example\Downloads\%f.mp4
                    input = Path.Combine(inputDir, inputFileName + inputExt); // eg. C:\Users\Example\Downloads\%f.mp4
                }
            }

            // -------------------------
            // Empty
            // -------------------------
            else
            {
                inputDir = string.Empty;
                inputFileName = string.Empty;
                input = string.Empty;
            }


            // Return Value
            return input;
        }



        /// <summary>
        /// Batch Toggle
        /// </summary>
        // Checked
        private void tglBatch_Checked(object sender, RoutedEventArgs e)
        {
            // Enable / Disable batch extension textbox
            if (VM.MainView.Batch_IsChecked == true)
            {
                VM.MainView.BatchExtension_IsEnabled = true;
                VM.MainView.BatchExtension_Text = string.Empty;
            }

            // Clear Browse Textbox, Input Filename, Dir, Ext
            if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
            {
                VM.MainView.Input_Text = string.Empty;
                inputFileName = string.Empty;
                inputDir = string.Empty;
                inputExt = string.Empty;
            }

            // Clear Output Textbox, Output Filename, Dir, Ext
            if (!string.IsNullOrWhiteSpace(VM.MainView.Output_Text))
            {
                VM.MainView.Output_Text = string.Empty;
                outputFileName = string.Empty;
                outputDir = string.Empty;
                outputExt = string.Empty;
            }

        }
        // Unchecked
        private void tglBatch_Unchecked(object sender, RoutedEventArgs e)
        {
            // Enable / Disable batch extension textbox
            if (VM.MainView.Batch_IsChecked == false)
            {
                VM.MainView.BatchExtension_IsEnabled = false;
                VM.MainView.BatchExtension_Text = "extension";
            }

            // Clear Browse Textbox, Batch Filename, Dir, Ext
            if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
            {
                VM.MainView.Input_Text = string.Empty;
                inputFileName = string.Empty;
                inputDir = string.Empty;
                inputExt = string.Empty;
            }

            // Clear Output Textbox, Output Filename, Dir, Ext
            if (!string.IsNullOrWhiteSpace(VM.MainView.Output_Text))
            {
                VM.MainView.Output_Text = string.Empty;
                outputFileName = string.Empty;
                outputDir = string.Empty;
                outputExt = string.Empty;
            }

            // -------------------------
            // Set Video and AudioCodec Combobox to "Copy" if 
            // Input File Extension is Same as Output File Extension 
            // and Quality is Auto
            // -------------------------
            //VideoControls.AutoCopyVideoCodec("input");
            //SubtitleControls.AutoCopySubtitleCodec("input");
            //AudioControls.AutoCopyAudioCodec("input");
        }


        /// <summary>
        /// Batch Input Directory
        /// </summary>
        // Directory Only, Needed for Batch
        public static String BatchInputDirectory()
        {
            // -------------------------
            // Batch
            // -------------------------
            if (VM.MainView.Batch_IsChecked == true)
            {
                inputDir = VM.MainView.Input_Text; // eg. C:\Input\Path\
            }

            // -------------------------
            // Empty
            // -------------------------
            // Input Textbox & Output Textbox Both Empty
            if (string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
            {
                inputDir = string.Empty;
            }


            // Return Value
            return inputDir;
        }


        /// <summary>
        /// Batch Extension Textbox
        /// </summary>
        private void batchExtension_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Remove Default Value
            if (string.IsNullOrWhiteSpace(VM.MainView.BatchExtension_Text) ||
                VM.MainView.BatchExtension_Text == "extension"
                )
            {
                inputExt = string.Empty;
            }
            // TextBox Value
            else
            {
                inputExt = VM.MainView.BatchExtension_Text;
            }

            // Add period to batchExt if user did not enter (This helps enable Copy)
            if (!string.IsNullOrWhiteSpace(VM.MainView.BatchExtension_Text) &&
                !inputExt.StartsWith(".") &&
                VM.MainView.BatchExtension_Text != "extension")
            {
                inputExt = "." + inputExt;
            }

            // --------------------------------------------------
            // Default Auto if Input Extension matches Output Extsion
            // This will trigger Auto Codec Copy
            // --------------------------------------------------
            //ExtensionMatchLoadAutoValues();

            // -------------------------
            // Set Video and AudioCodec Combobox to "Copy" if 
            // Input File Extension is Same as Output File Extension 
            // and Quality is Auto
            // -------------------------
            //VideoControls.AutoCopyVideoCodec("input");
            //SubtitleControls.AutoCopySubtitleCodec("input");
            //AudioControls.AutoCopyAudioCodec("input");
        }


        /// <summary>
        /// Batch Extension Period Check (Method)
        /// </summary>
        public static void BatchExtCheck()
        {
            if (VM.MainView.Batch_IsChecked == true)
            {
                // Add period to Batch Extension if User did not enter one
                if (!string.IsNullOrWhiteSpace(VM.MainView.BatchExtension_Text))
                {
                    if (VM.MainView.BatchExtension_Text != "extension" &&
                        VM.MainView.BatchExtension_Text != "." &&
                        !VM.MainView.BatchExtension_Text.StartsWith(".")
                        )
                    {
                        inputExt = "." + VM.MainView.BatchExtension_Text;
                    }
                }
                else
                {
                    inputExt = string.Empty;
                }
            }
        }

    }
}
