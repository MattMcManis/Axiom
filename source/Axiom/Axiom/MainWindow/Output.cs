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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using ViewModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class MainWindow : Window
    {
        public static string regexTags { get; set; }

        /// <summary>
        /// Output File SaveFileDialog Filter
        /// </summary>
        public static String Output_SaveFileDialog_Filter()
        {
            switch (VM.FormatView.Format_Container_SelectedItem)
            {
                // Video
                case "webm":
                    return "WebM (*.webm)|*.webm";
                case "mp4":
                    return "MP4 (*.mp4)|*.mp4";
                case "mkv":
                    return "MKV (*.mkv)|*.mkv";
                case "mpg":
                    return "MPG (*.mpg)|*.mpg";
                case "avi":
                    return "AVI (*.avi)|*.avi";
                case "ogv":
                    return "OGV (*.ogv)|*.ogv";

                // Audio
                case "mp3":
                    return "MP3 (*.mp3)|*.mp3";
                case "m4a":
                    return "M4A (*.m4a)|*.m4a";
                case "ogg":
                    return "OGG (*.ogg)|*.ogg";
                case "flac":
                    return "FLAC (*.flac)|*.flac";
                case "wav":
                    return "WAV (*.wav)|*.wav";

                // Image
                case "jpg":
                    return "JPG (*.jpg)|*.jpg";
                case "png":
                    return "PNG (*.png)|*.png";
                case "webp":
                    return "WebP (*.webp)|*.webp";
                default:
                    return string.Empty;
            }
        }


        /// <summary>
        /// Output Button
        /// </summary>
        private void btnOutput_Click(object sender, RoutedEventArgs e)
        {
            // Get Output File Extension
            Controls.Format.Controls.OutputFormatExt();



            switch (VM.MainView.Batch_IsChecked)
            {
                // -------------------------
                // Single File
                // -------------------------
                case false:
                    // Initialize 'Save File' Dialog Window
                    Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();
                    // File Filter
                    saveFile.Filter = Output_SaveFileDialog_Filter();
                    // Set Output Extension
                    saveFile.DefaultExt = outputExt;

                    // -------------------------
                    // Output TextBox is Empty
                    // -------------------------
                    if (string.IsNullOrWhiteSpace(VM.MainView.Output_Text))
                    {
                        // Input File Name is Empty
                        if (string.IsNullOrWhiteSpace(inputFileName))
                        {
                            // Load InitialDirectory from axiom.conf
                            try
                            {
                                if (File.Exists(Controls.Configure.axiomConfFile))
                                {
                                    Controls.Configure.ConfigFile conf = new Controls.Configure.ConfigFile(Controls.Configure.axiomConfFile);
                                    outputPreviousPath = conf.Read("User", "OutputPreviousPath");

                                    if (!string.IsNullOrWhiteSpace(outputPreviousPath))
                                    {
                                        saveFile.InitialDirectory = outputPreviousPath;
                                    }
                                }
                            }
                            catch
                            {

                            }

                            saveFile.FileName = "File";
                        }
                        // Input has File Name
                        else
                        {
                            // Set Output to same as Input
                            saveFile.InitialDirectory = inputDir;
                            saveFile.FileName = TokenRemover(
                                                        FileRenamer(inputDir, // comparision
                                                            inputDir,      // comparision
                                                            inputFileName, // comparision
                                                            inputFileName  // comparison / name to change
                                                           )
                                                    );
                        }
                    }

                    // -------------------------
                    // Output TextBox has Text
                    // -------------------------
                    else
                    {
                        // Set Output to it's original text

                        // Set Initial Directory
                        saveFile.InitialDirectory = Path.GetDirectoryName(VM.MainView.Output_Text);

                        saveFile.FileName = Path.GetFileNameWithoutExtension(VM.MainView.Output_Text);
                    }

                    // -------------------------
                    // Show 'Save File' Dialog Window
                    // -------------------------
                    //Nullable<bool> result = saveFile.ShowDialog();

                    // Process Dialog Window
                    //if (result == true)
                    if (saveFile.ShowDialog() == true)
                    {
                        if (IsValidPath(saveFile.FileName))
                        {
                            // Set Output to Dialog Window entered
                            outputDir = Path.GetDirectoryName(Path.Combine(saveFile.InitialDirectory, saveFile.FileName + outputExt));
                            outputFileName_Original = TokenRemover(Path.GetFileNameWithoutExtension(saveFile.FileName));

                            // -------------------------
                            // Update Output TextBox
                            // -------------------------
                            outputFileName = outputFileName_Original;

                            // Update Output TextBox Display
                            if (!string.IsNullOrEmpty(outputDir))
                            {
                                VM.MainView.Output_Text = Path.Combine(outputDir, outputFileName + outputExt);
                            }

                            // Save Previous Path
                            if (File.Exists(Controls.Configure.axiomConfFile))
                            {
                                try
                                {
                                    Controls.Configure.ConfigFile conf = new Controls.Configure.ConfigFile(Controls.Configure.axiomConfFile);
                                    conf.Write("User", "OutputPreviousPath", outputDir);
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                    break;

                // -------------------------
                // Batch
                // -------------------------
                case true:
                    // Initialize 'Select Folder' Dialog Window
                    System.Windows.Forms.FolderBrowserDialog outputFolder = new System.Windows.Forms.FolderBrowserDialog();

                    // -------------------------
                    // Show 'Save File' Dialog Window
                    // -------------------------
                    System.Windows.Forms.DialogResult resultBatch = outputFolder.ShowDialog();

                    // Process Dialog Window
                    if (resultBatch == System.Windows.Forms.DialogResult.OK)
                    {
                        try
                        {
                            if (!string.IsNullOrWhiteSpace(outputFolder.SelectedPath)) // empty check
                            {
                                if (IsValidPath(outputFolder.SelectedPath.TrimEnd('\\') + @"\"))
                                {
                                    // Set Output Path
                                    outputDir = outputFolder.SelectedPath.TrimEnd('\\') + @"\";

                                    // Update Output TextBox Display
                                    VM.MainView.Output_Text = outputFolder.SelectedPath.TrimEnd('\\') + @"\";
                                }
                            }
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show(ex.ToString(),
                                            "Error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);
                        }
                    }
                    break;
            }
        }


        /// <summary>
        /// Output Path
        /// </summary>
        public static String OutputPath()
        {
            // Get Output File Extension
            Controls.Format.Controls.OutputFormatExt();

            switch (VM.MainView.Batch_IsChecked)
            {
                // -------------------------
                // Single File
                // -------------------------
                case false:
                    OutputPath_SingleFile();
                    break;

                // -------------------------
                // Batch
                // -------------------------
                case true:
                    OutputPath_Batch();
                    break;
            }

            // Update Output TextBox
            //VM.MainView.Output_Text = output;

            return output;
        }


        /// <summary>
        /// Output Path - Single File
        /// </summary>
        public static void OutputPath_SingleFile()
        {
            // Input TextBox is Empty
            // Return Output TextBox's original text (either File Path or Empty)
            if (string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
            {
                // Clear Inputs
                inputDir = string.Empty;
                inputFileName = string.Empty;
                inputExt = string.Empty;

                // Set Output
                output = VM.MainView.Output_Text;
                return;
            }

            // Input TextBox has Text
            switch (IsWebURL(VM.MainView.Input_Text))
            {
                // -------------------------
                // Local File
                // -------------------------
                case false:
                    // Output TextBox is Empty
                    if (string.IsNullOrWhiteSpace(VM.MainView.Output_Text))
                    {
                        // Set Output to be same as Input
                        outputDir = inputDir;

                        // Original File Name
                        //outputFileName_Original = TokenRemover(inputFileName);
                        outputFileName_Original = FileRenamer(inputDir,      // comparision
                                                              outputDir,     // comparision
                                                              inputFileName, // comparision
                                                              TokenRemover(inputFileName) // comparison / name to change
                                                              );

                        // Add Settings Tokens to File Name e.g. MyFile x265 CRF25 1080p AAC 320k
                        // Use Token Remover to remove tokens on Input files that already have Tokens
                        // To prevent Tokens from doubling up
                        if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Any())
                        {
                            outputFileName_Tokens = FileRenamer(inputDir,      // comparision
                                                                outputDir,     // comparision
                                                                inputFileName, // comparision
                                                                TokenAppender(outputFileName_Original) // comparison / name to change
                                                               );
                        }

                        // e.g. MyFile
                        outputFileName = outputFileName_Original;

                        //MessageBox.Show("orig: " + outputFileName_Original + " " + "tokens: " + outputFileName_Tokens); //debug
                    }

                    // Output TextBox has Text
                    else
                    {
                        // e.g. C:\Output\Path\
                        outputDir = Path.GetDirectoryName(VM.MainView.Output_Text).TrimEnd('\\') + @"\";

                        // Original File Name - Do not set outputFileName_Original

                        // Add Settings Tokens to File Name e.g. MyFile x265 CRF25 1080p AAC 320k
                        // Use Token Remover to remove tokens on Input files that already have Tokens
                        // To prevent Tokens from doubling up
                        if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Any())
                        {
                            outputFileName_Tokens = FileRenamer(inputDir,      // comparision
                                                                outputDir,     // comparision
                                                                inputFileName, // comparision
                                                                TokenAppender(TokenRemover(outputFileName_Original)) // comparison / name to change
                                                               );
                        }

                        // e.g. MyFile
                        outputFileName = FileRenamer(inputDir,      // comparision
                                                     outputDir,     // comparision
                                                     inputFileName, // comparision
                                                     TokenRemover(outputFileName_Original) // comparison / name to change
                                                    );
                    }

                    // Image Sequence Renamer
                    if (VM.FormatView.Format_MediaType_SelectedItem == "Sequence")
                    {
                        // Must be this name
                        outputFileName_Original = "image-%03d";
                        outputFileName_Tokens = "image-%03d";
                        outputFileName = "image-%03d";
                    }

                    // -------------------------
                    // Combine Output
                    // -------------------------
                    // eg. C:\Users\Example\Videos\MyFile.webm

                    // Default
                    if (!VM.ConfigureView.OutputNaming_ListView_SelectedItems.Any())
                    {
                        output = Path.Combine(outputDir, OutputFileNameSpacing(outputFileName) + outputExt);
                    }
                    // Output Name Tokens
                    else
                    {
                        output = Path.Combine(outputDir, OutputFileNameSpacing(outputFileName_Tokens) + outputExt);
                    }
                    break;

                // -------------------------
                // Web URL
                // -------------------------
                case true:
                    // Output TextBox is Empty
                    if (string.IsNullOrWhiteSpace(VM.MainView.Output_Text))
                    {
                        // Default
                        outputDir = downloadDir;

                        switch (VM.ConfigureView.Shell_SelectedItem)
                        {
                            // CMD
                            case "CMD":
                                // eg. C:\Output\Path\%f.mp4
                                outputFileName_Original = "%f";
                                outputFileName_Tokens = "%f";
                                outputFileName = "%f";
                                break;

                            // PowerShell
                            case "PowerShell":
                                // eg. C:\Output\Path\$name.mp4
                                outputFileName_Original = "$name";
                                outputFileName_Tokens = "$name";
                                outputFileName = "$name";
                                break;
                        }
                    }

                    // Output TextBox has Text
                    else
                    {
                        // e.g. C:\Output\Path\
                        outputDir = Path.GetDirectoryName(VM.MainView.Output_Text).TrimEnd('\\') + @"\";
                        // e.g. C:\Output\Path\MyFile.mp4
                        outputFileName = TokenRemover(Path.GetFileNameWithoutExtension(VM.MainView.Output_Text));
                        // Set Original Name
                        outputFileName_Original = outputFileName;
                        // Disable Tokens
                        outputFileName_Tokens = outputFileName;
                    }

                    // -------------------------
                    // Combine Output
                    // -------------------------
                    // eg. C:\Users\Example\Downloads\%f.webm
                    //output = outputFileName_Original;
                   
                    output = Path.Combine(outputDir, outputFileName + outputExt);
                    break;
            }
        }


        /// <summary>
        /// Output Path - Batch
        /// </summary>
        public static void OutputPath_Batch()
        {
            // Input TextBox is Empty
            // Return Output TextBox's original text (either File Path or Empty)
            if (string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
            {
                // Clear Inputs
                inputDir = string.Empty;
                inputFileName = string.Empty;
                inputExt = string.Empty;

                // Set Output
                output = VM.MainView.Output_Text;
                return;
            }

            // Input TextBox has Text
            switch (IsWebURL(VM.MainView.Input_Text))
            {
                // -------------------------
                // Local File
                // -------------------------
                case false:
                    // Output TextBox is Empty
                    if (string.IsNullOrWhiteSpace(VM.MainView.Output_Text))
                    {
                        // Set Output Directory to be same as Input Directory
                        outputDir = inputDir.TrimEnd('\\') + @"\";

                        switch (VM.ConfigureView.Shell_SelectedItem)
                        {
                            case "CMD":
                                outputFileName = "%~nf";
                                break;

                            case "PowerShell":
                                outputFileName = "$outputName";
                                break;

                            default:
                                outputFileName = "%~nf";
                                break;
                        }
                    }

                    // Output TextBox has Text
                    else
                    {
                        // e.g. C:\Output\Path\
                        //outputDir = Path.GetDirectoryName(VM.MainView.Output_Text.TrimEnd('\\') + @"\");
                        outputDir = Path.GetDirectoryName(VM.MainView.Output_Text.TrimEnd('\\') + @"\");

                        //MessageBox.Show(outputDir); //debug

                        switch (VM.ConfigureView.Shell_SelectedItem)
                        {
                            case "CMD":
                                outputFileName = "%~nf";
                                break;

                            case "PowerShell":
                                outputFileName = "$outputName";
                                break;

                            default:
                                outputFileName = "%~nf";
                                break;
                        }
                    }
                    break;

                // -------------------------
                // Web URL
                // -------------------------
                case true:
                    outputDir = string.Empty;

                    // Notice
                    MessageBox.Show("Cannot Batch Process Web URL's.",
                                    "Notice",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    return;
            }

            // Output
            // eg. C:\Users\Example\Videos\
            //output = outputDir;
            output = Path.Combine(outputDir, outputFileName + outputExt);
        }


        /// <summary>
        /// Output Textbox - TextChanged
        /// </summary>
        private void tbxOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Remove stray slash if closed out early
            if (VM.MainView.Output_Text == "\\")
            {
                VM.MainView.Output_Text = string.Empty;
            }

            // -------------------------
            // Enable / Disable "Open Output Location" Button
            // -------------------------
            if (!string.IsNullOrWhiteSpace(VM.MainView.Output_Text) && // null check
                IsValidPath(VM.MainView.Output_Text) == true && // Detect Invalid Characters
                Path.IsPathRooted(VM.MainView.Output_Text) == true // TrimEnd('\\') + @"\" is adding a backslash to 
                                                                   // Iput text 'http' until it is detected as Web URL
                )
            {
                bool exists = Directory.Exists(Path.GetDirectoryName(VM.MainView.Output_Text));

                // Path exists
                if (exists)
                {
                    VM.MainView.Output_Location_IsEnabled = true;
                }
                // Path does not exist
                else
                {
                    VM.MainView.Output_Location_IsEnabled = false;
                }
            }
            // Disable Button for Web URL
            else
            {
                //VM.MainView.Output_Clear_IsEnabled = false;
                VM.MainView.Output_Location_IsEnabled = false;
            }

            // -------------------------
            // Enable / Disable "Output Clear" Button
            // -------------------------
            // Disable
            if (string.IsNullOrWhiteSpace(VM.MainView.Output_Text))
            {
                VM.MainView.Output_Clear_IsEnabled = false;
            }
            // Enable
            else
            {
                VM.MainView.Output_Clear_IsEnabled = true;
            }

            // -------------------------
            // Set Output
            // -------------------------
            if (VM.MainView.BatchExtension_IsEnabled == true)
            {
                // e.g. C:\Output\Path\
                if (IsValidPath(VM.MainView.Output_Text) == true)
                {
                    outputDir = Path.GetDirectoryName(VM.MainView.Output_Text);
                    if (!string.IsNullOrWhiteSpace(outputDir))
                    {
                        outputDir = outputDir.TrimEnd('\\') + @"\";
                    }
                }
                else
                {
                    outputDir = string.Empty;
                }

                // e.g. C:\Output\Path\MyFile.mp4
                //outputFileName = TokenRemover(Path.GetFileNameWithoutExtension(VM.MainView.Output_Text));
                // Set Original Name
                //outputFileName_Original = outputFileName;
                // Disable Tokens
                //outputFileName_Tokens = outputFileName;
                // Output
                //output = VM.MainView.Output_Text;
            }
        }

        /// <summary>
        /// Output Textbox - KeyUp
        /// </summary>
        private void tbxOutput_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            outputFileName_Original = TokenRemover(Path.GetFileNameWithoutExtension(VM.MainView.Output_Text));
        }

        /// <summary>
        /// Output Textbox - Paste
        /// </summary>
        private void OnOutputTextBoxPaste(object sender, DataObjectPastingEventArgs e)
        {
            try
            {
                var isText = e.SourceDataObject.GetDataPresent(DataFormats.UnicodeText, true);
                if (!isText) return;

                var text = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;

                // Save Output File Name
                outputFileName_Original = TokenRemover(Path.GetFileNameWithoutExtension(VM.MainView.Output_Text));
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
        /// Output Textbox - Drag and Drop
        /// </summary>
        private void tbxOutput_PreviewDragOver(object sender, DragEventArgs e)
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

        private void tbxOutput_PreviewDrop(object sender, DragEventArgs e)
        {
            try
            {
                var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
                VM.MainView.Output_Text = buffer.First();

                // Save Output File Name
                outputFileName_Original = TokenRemover(Path.GetFileNameWithoutExtension(VM.MainView.Output_Text));
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
        /// Update Output TextBox Text
        /// </summary>
        public void UpdateOutputTextBoxText()
        {
            if (!string.IsNullOrWhiteSpace(outputDir))
            {
                switch (VM.MainView.Batch_IsChecked)
                {
                    // Single file
                    case false:
                        VM.MainView.Output_Text = Path.Combine(outputDir, outputFileName_Original + outputExt);
                        break;

                    // Batch
                    case true:
                        VM.MainView.Output_Text = outputDir.TrimEnd('\\') + @"\";
                        break;
                }
            }
        }

        /// <summary>
        /// Output TetBox Clear - Button
        /// </summary>
        private void btnOutputClear_Click(object sender, RoutedEventArgs e)
        {
            VM.MainView.Output_Text = string.Empty;

            outputDir = string.Empty;
            outputFileName_Original = string.Empty;
            outputFileName_Tokens = string.Empty;
            outputFileName = string.Empty;
            outputExt = string.Empty;
            output = string.Empty;
        }

        /// <summary>
        /// Open Output Folder - Button
        /// </summary>
        private void openLocationOutput_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidPath(outputDir))
            {
                if (Directory.Exists(@outputDir))
                {
                    Process.Start("explorer.exe", @outputDir);
                }
            }
        }


        /// <summary>
        /// File Renamer (Method)
        /// </summary>
        public static String FileRenamer(string inputDir,      // comparison
                                         string outputDir,     // comparison
                                         string inputFileName, // comparison
                                         string outputFileName // comparison / name to change
            )
        {
            string inputFullPath = Path.Combine(inputDir, inputFileName + inputExt);
            string outputFullPath = Path.Combine(outputDir, outputFileName + outputExt);

            //MessageBox.Show("inPath: " + inputFullPath + " " + "outPath: " + outputFullPath); //debug

            if (// Input is not Empty
                !string.IsNullOrWhiteSpace(VM.MainView.Input_Text) &&
                // Input & Output Match
                string.Equals(inputFullPath, outputFullPath, StringComparison.OrdinalIgnoreCase)
                )
            {
                //MessageBox.Show("inPath: " + inputFullPath + " " + "outPath: " + outputFullPath); //debug

                string output = Path.Combine(outputDir, outputFileName + outputExt);
                string outputNewFileName = string.Empty;

                int count = 1;

                // Add (1) if File Names are the same
                if (File.Exists(output))
                {
                    while (File.Exists(output))
                    {
                        outputNewFileName = string.Format("{0}({1})", outputFileName + " ", count++);
                        output = Path.Combine(outputDir, outputNewFileName + outputExt);
                    }
                }
                // stay original
                else
                {
                    outputNewFileName = outputFileName;
                }

                return outputNewFileName;
            }
            // stay original
            else
            {
                return outputFileName;
            }
        }


        /// <summary>
        /// Token Appender (Method)
        /// </summary>
        public static String TokenAppender(string filename)
        {
            //MessageBox.Show(string.Join("\n",VM.ConfigureView.OutputNaming_ListView_SelectedItems)); //debug

            // -------------------------
            // Halt 
            // -------------------------
            if (// If No Output Naming is Selected
                !VM.ConfigureView.OutputNaming_ListView_SelectedItems.Any() ||
                // If Batch
                VM.MainView.Batch_IsChecked == true)
            {
                // return original
                //return filename;
                return outputFileName;
            }

            // -------------------------
            // Format
            // -------------------------
            // Input Extension
            string format_inputExt = string.Empty;
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Input Ext"))
            {
                if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
                {
                    format_inputExt = SettingsCheck(Path.GetExtension(VM.MainView.Input_Text)
                                                                        .Replace(".", "")
                                                                        .ToLower()
                                                   );
                }
            }

            // -------------------------
            // Video
            // -------------------------
            string video_hwAccel_Transcode = string.Empty;
            string video_Codec = string.Empty;
            string video_Codec_Copy = string.Empty;
            string video_Pass = string.Empty;
            string video_BitRate = string.Empty;
            string video_CRF = string.Empty;
            string video_Preset = string.Empty;
            string video_PixelFormat = string.Empty;
            string video_Size = string.Empty;
            string video_Size_Source = string.Empty;
            string video_ScalingAlgorithm = string.Empty;
            string video_FPS = string.Empty;
            string video_Vsync = string.Empty;

            if (VM.FormatView.Format_MediaType_SelectedItem == "Video" ||
                VM.FormatView.Format_MediaType_SelectedItem == "Image" ||
                VM.FormatView.Format_MediaType_SelectedItem == "Sequence")
            {
                // HW Accel
                video_hwAccel_Transcode = string.Empty;
                if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("HW Accel"))
                {
                    if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_HWAccel_Transcode_SelectedItem))
                    {
                        video_hwAccel_Transcode = SettingsCheck(VM.VideoView.Video_HWAccel_Transcode_SelectedItem.Replace(" ", "-"));
                    }
                }

                // Video Codec
                if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Video Codec"))
                {
                    // Ignore Copy
                    if (VM.VideoView.Video_Codec_SelectedItem != "Copy")
                    {
                        if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_Codec_SelectedItem))
                        {
                            video_Codec = SettingsCheck(VM.VideoView.Video_Codec_SelectedItem);
                        }
                    }
                }

                // Video Codec Copy
                if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Video Codec Copy"))
                {
                    if (VM.VideoView.Video_Codec_SelectedItem == "Copy")
                    {
                        if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_Codec_SelectedItem))
                        {
                            video_Codec_Copy = "cv-" + SettingsCheck(VM.VideoView.Video_Codec_SelectedItem.ToLower());
                        }
                    }
                }

                // Pass
                if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Pass"))
                {
                    // null check prevents unknown crash
                    if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_Pass_SelectedItem))
                    {
                        video_Pass = SettingsCheck(VM.VideoView.Video_Pass_SelectedItem.Replace(" ", "-")); // do not lowercase
                    }
                }

                // Video Bit Rate
                if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Video Bit Rate"))
                {
                    if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_BitRate_Text))
                    {
                        video_BitRate = SettingsCheck(VM.VideoView.Video_BitRate_Text.ToUpper()); //K / M
                    }
                }

                // CRF
                if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Video CRF"))
                {
                    if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_CRF_Text))
                    {
                        video_CRF = "CRF" + SettingsCheck(VM.VideoView.Video_CRF_Text);
                    }
                }

                // Preset
                if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Preset"))
                {
                    video_Preset = SettingsCheck(VM.VideoView.Video_EncodeSpeed_Items
                                                   .FirstOrDefault(item => item.Name == VM.VideoView.Video_EncodeSpeed_SelectedItem)?.Name);

                    if (!string.IsNullOrWhiteSpace(video_Preset))
                    {
                        video_Preset = "p-" + video_Preset
                                              .Replace(" ", "-")
                                              .ToLower();
                    }
                }

                // Pixel Format
                if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Pixel Format"))
                {
                    if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_PixelFormat_SelectedItem))
                    {
                        video_PixelFormat = SettingsCheck(VM.VideoView.Video_PixelFormat_SelectedItem);
                    }
                }

                // Scale/Size
                if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Size"))
                {
                    if (VM.VideoView.Video_Scale_SelectedItem != "Source")
                    {
                        if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_Scale_SelectedItem))
                        {
                            video_Size = SettingsCheck(VM.VideoView.Video_Scale_SelectedItem.Replace(" ", "-"));
                        }
                    }
                }

                // Scale/Size Source
                if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Size Source"))
                {
                    if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_Scale_SelectedItem))
                    {
                        if (VM.VideoView.Video_Scale_SelectedItem == "Source")
                        {
                            video_Size_Source = "sz-" + SettingsCheck(VM.VideoView.Video_Scale_SelectedItem.ToLower());
                        }
                    }
                }

                // Scaling
                if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Scaling"))
                {
                    if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_ScalingAlgorithm_SelectedItem))
                    {
                        video_ScalingAlgorithm = SettingsCheck(VM.VideoView.Video_ScalingAlgorithm_SelectedItem);
                    }

                    if (!string.IsNullOrWhiteSpace(video_ScalingAlgorithm))
                    {
                        video_ScalingAlgorithm = "sa-" + video_ScalingAlgorithm
                                                         .Replace(" ", "-")
                                                         .ToLower();
                    }
                }

                // FPS
                if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Frame Rate"))
                {
                    if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_FPS_SelectedItem))
                    {
                        video_FPS = SettingsCheck(VM.VideoView.Video_FPS_SelectedItem);
                    }

                    if (!string.IsNullOrWhiteSpace(video_FPS))
                    {
                        video_FPS = video_FPS + "fps";
                    }
                }

                // Vsync
                if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Vsync"))
                {
                    if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_Vsync_SelectedItem))
                    {
                        video_Vsync = SettingsCheck(VM.VideoView.Video_Vsync_SelectedItem);
                    }

                    if (!string.IsNullOrWhiteSpace(video_Vsync))
                    {
                        video_Vsync = "vsync-" + video_Vsync;
                    }
                }
            }

            // -------------------------
            // Audio
            // -------------------------
            // Audio Codec
            string audio_Codec = string.Empty;
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Audio Codec"))
            {
                // Ignore Copy
                if (VM.AudioView.Audio_Codec_SelectedItem != "Copy")
                {
                    if (!string.IsNullOrWhiteSpace(VM.AudioView.Audio_Codec_SelectedItem))
                    {
                        audio_Codec = SettingsCheck(VM.AudioView.Audio_Codec_SelectedItem);
                    }
                }
            }

            // Audio Codec Copy
            string audio_Codec_Copy = string.Empty;
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Audio Codec Copy"))
            {
                if (VM.AudioView.Audio_Codec_SelectedItem == "Copy")
                {
                    if (!string.IsNullOrWhiteSpace(VM.AudioView.Audio_Codec_SelectedItem))
                    {
                        audio_Codec_Copy = "ca-" + SettingsCheck(VM.AudioView.Audio_Codec_SelectedItem.ToLower());
                    }
                }
            }

            // Channel
            string audio_Channel = string.Empty;
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Channel"))
            {
                if (VM.AudioView.Audio_Channel_SelectedItem != "Source")
                {
                    if (!string.IsNullOrWhiteSpace(VM.AudioView.Audio_Channel_SelectedItem))
                    {
                        audio_Channel = SettingsCheck(VM.AudioView.Audio_Channel_SelectedItem);
                    }

                    if (!string.IsNullOrWhiteSpace(audio_Channel))
                    {
                        string channel = string.Empty;
                        switch (VM.AudioView.Audio_Channel_SelectedItem)
                        {
                            case "Mono":
                                channel = "1";
                                break;

                            case "Stereo":
                                channel = "2";
                                break;

                            case "Joint Stereo":
                                channel = "2";
                                break;

                            case "1.0":
                                channel = "1";
                                break;

                            case "2.0":
                                channel = "2";
                                break;

                            case "2.1":
                                channel = "2.1";
                                break;

                            case "5.1":
                                channel = "5.1";
                                break;

                            case "6.1":
                                channel = "6.1";
                                break;

                            case "7.1":
                                channel = "7.1";
                                break;
                        }

                        audio_Channel = channel + "CH";
                    }
                }
            }

            // Audio Bit Rate
            string audio_BitRate = string.Empty;
            // VBR
            string audio_VBR = string.Empty;
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Audio Bit Rate"))
            {
                if (VM.AudioView.Audio_Quality_SelectedItem == "Custom")
                {
                    // TextBox
                    if (!string.IsNullOrWhiteSpace(VM.AudioView.Audio_BitRate_Text))
                    {
                        audio_BitRate = SettingsCheck(VM.AudioView.Audio_BitRate_Text) + "k";
                    }
                }
                else
                {
                    // Quality ComboBox
                    if (!string.IsNullOrWhiteSpace(VM.AudioView.Audio_Quality_SelectedItem))
                    {
                        audio_BitRate = SettingsCheck(VM.AudioView.Audio_Quality_SelectedItem);

                        if (VM.AudioView.Audio_Quality_SelectedItem != "Lossless")
                        {
                            audio_BitRate += "k";
                        }
                    }
                }

                if (VM.AudioView.Audio_VBR_IsChecked == true &&
                    !string.IsNullOrWhiteSpace(VM.AudioView.Audio_BitRate_Text))
                {
                    audio_VBR = "VBR";
                }
            }

            // Sample Rate
            string audio_SampleRate = string.Empty;
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Sample Rate"))
            {
                if (!string.IsNullOrWhiteSpace(VM.AudioView.Audio_SampleRate_SelectedItem))
                {
                    audio_SampleRate = SettingsCheck(VM.AudioView.Audio_SampleRate_SelectedItem.Replace("k", "")) + "kHz";
                }
            }

            // Bit Depth
            string audio_BitDepth = string.Empty;
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Bit Depth"))
            {
                if (!string.IsNullOrWhiteSpace(VM.AudioView.Audio_BitDepth_SelectedItem))
                {
                    audio_BitDepth = SettingsCheck(VM.AudioView.Audio_BitDepth_SelectedItem) + "-bit";
                }
            }

            // Merge
            List<string> newFileNameList = new List<string>();
            // Add Original File Name
            newFileNameList.Add(filename);

            // Create new File Name by Options Order Arranged
            for (var i = 0; i < VM.ConfigureView.OutputNaming_ListView_SelectedItems.Count; i++)
            {
                switch (VM.ConfigureView.OutputNaming_ListView_SelectedItems[i])
                {
                    // Add

                    // Format
                    case "Input Ext":
                        newFileNameList.Add(format_inputExt);
                        break;

                    // Video
                    case "HW Accel":
                        newFileNameList.Add(video_hwAccel_Transcode);
                        break;
                    case "Video Codec":
                        newFileNameList.Add(video_Codec);
                        break;
                    case "Video Codec Copy":
                        newFileNameList.Add(video_Codec_Copy);
                        break;
                    case "Pass":
                        newFileNameList.Add(video_Pass);
                        break;
                    case "Video Bit Rate":
                        newFileNameList.Add(video_BitRate);
                        break;
                    case "Video CRF":
                        newFileNameList.Add(video_CRF);
                        break;
                    case "Preset":
                        newFileNameList.Add(video_Preset);
                        break;
                    case "Pixel Format":
                        newFileNameList.Add(video_PixelFormat);
                        break;
                    case "Frame Rate":
                        newFileNameList.Add(video_FPS);
                        break;
                    case "Vsync":
                        newFileNameList.Add(video_Vsync);
                        break;
                    case "Size":
                        newFileNameList.Add(video_Size);
                        break;
                    case "Size Source":
                        newFileNameList.Add(video_Size_Source);
                        break;
                    case "Scaling":
                        newFileNameList.Add(video_ScalingAlgorithm);
                        break;

                    // Audio
                    case "Audio Codec":
                        newFileNameList.Add(audio_Codec);
                        break;
                    case "Audio Codec Copy":
                        newFileNameList.Add(audio_Codec_Copy);
                        break;
                    case "Channel":
                        newFileNameList.Add(audio_Channel);
                        break;
                    case "Audio Bit Rate":
                        newFileNameList.Add(audio_BitRate + audio_VBR);
                        break;
                    case "Sample Rate":
                        newFileNameList.Add(audio_SampleRate);
                        break;
                    case "Bit Depth":
                        newFileNameList.Add(audio_BitDepth);
                        break;
                }
            }

            // New File Name
            string newFileName = string.Join(" ", newFileNameList
                                                    // Remove empty values
                                                    .Where(s => !string.IsNullOrWhiteSpace(s))
                                                    .Where(s => !s.Equals("cv-"))
                                                    .Where(s => !s.Equals("CRF"))
                                                    .Where(s => !s.Equals("fps"))
                                                    .Where(s => !s.Equals("p-"))
                                                    .Where(s => !s.Equals("sz-"))
                                                    .Where(s => !s.Equals("sa-"))
                                                    .Where(s => !s.Equals("ca-"))
                                                    .Where(s => !s.Equals("CH-"))
                                                    .Where(s => !s.Equals("k"))
                                                    .Where(s => !s.Equals("-bit"))
                                                    .Where(s => !s.Equals("kHz"))
                                            );

            return newFileName;
        }

        /// <summary>
        /// Settings Check
        /// </summary>
        public static String SettingsCheck(string s)
        {
            // Fail
            if (string.IsNullOrWhiteSpace(s) ||
                s.Equals("off", StringComparison.OrdinalIgnoreCase) ||
                s.Equals("none", StringComparison.OrdinalIgnoreCase) ||
                s.Equals("auto", StringComparison.OrdinalIgnoreCase) ||
                s.Equals("CRF", StringComparison.OrdinalIgnoreCase) || // Use CRF TextBox instead of Pass
                s.Equals("Custom", StringComparison.OrdinalIgnoreCase)
                )
            {
                return string.Empty;
            }
            // Pass
            else
            {
                return s;
            }
        }

        /// <summary>
        /// Output Naming Defaults
        /// </summary>
        public static List<string> outputNaming_Defaults = new List<string>()
        {
            // Format
            "Input Ext",

            // Video
            "HW Accel",
            "Video Codec",
            "Video Codec Copy",
            "Pass",
            "Video Bit Rate",
            "Video CRF",
            "Preset",
            "Pixel Format",
            "Frame Rate",
            "Vsync",
            "Size",
            "Size Source",
            "Scaling",

            // Audio
            "Audio Codec",
            "Audio Codec Copy",
            "Channel",
            "Audio Bit Rate",
            "Sample Rate",
            "Bit Depth"
        };


        /// <summary>
        /// Token Remover (Method)
        /// </summary>
        public static String TokenRemover(string filename)
        {
            // Remove
            if (VM.ConfigureView.InputFileNameTokens_SelectedItem == "Remove")
            {
                // HW Accel Transcode
                string hwAccelTranscode = "(" + string.Join("|", VM.VideoView.Video_HWAccel_Transcode_Items
                                                                  .Where(s => !string.IsNullOrWhiteSpace(s))
                                                                  .Where(s => !s.Equals("none"))
                                                                  .Where(s => !s.Equals("off"))
                                                                  .Where(s => !s.Equals("auto"))
                                                                  .Distinct()
                                                                  .OrderByDescending(x => x)
                                                                  .ToList()
                                                            ) +
                                          ")";

                // Presets
                IEnumerable<string> presetsList = new List<string>()
                {
                    "placebo",
                    "very-slow",
                    "slower",
                    "slow",
                    "medium",
                    "fast",
                    "faster",
                    "very-fast",
                    "super-fast",
                    "ultra-fast",

                    "quality",
                    "balanced",
                    "speed",

                    "default",
                    "lossless",
                    "lossless-hp",
                    "hp",
                    "hq",
                    "bd",
                    "low-Latency",
                    "low-Latency-hp",
                    "low-Latency-hq",
                };
                string presets = @"(p\-(" + string.Join("|", presetsList) + @"))";

                // Pass
                string pass = @"((1|2)\-?\s?Pass)";

                // Sample Rate
                string sampleRate = @"(\d+\.?\d?kHz)";

                // Bit Rate
                IEnumerable<string> bitRateList = new List<string>()
                {
                    @"CRF\d+",
                    @"\d+(\.?\d+)?(kVBR|kbps|k|m)",
                };
                string bitRate = "(" + string.Join("|", bitRateList) + ")";

                Controls.Video.Codec.AV1 av1 = new Controls.Video.Codec.AV1();
                Controls.Video.Codec.FFV1 ffv1 = new Controls.Video.Codec.FFV1();
                Controls.Video.Codec.HuffYUV huffYUV = new Controls.Video.Codec.HuffYUV();
                Controls.Video.Codec.MagicYUV magicYUV = new Controls.Video.Codec.MagicYUV();
                Controls.Video.Codec.MPEG_2 mpeg2 = new Controls.Video.Codec.MPEG_2();
                Controls.Video.Codec.MPEG_4 mpeg4 = new Controls.Video.Codec.MPEG_4();
                Controls.Video.Codec.Theora theora = new Controls.Video.Codec.Theora();
                Controls.Video.Codec.VP8 vp8 = new Controls.Video.Codec.VP8();
                Controls.Video.Codec.VP9 vp9 = new Controls.Video.Codec.VP9();
                Controls.Video.Codec.x264 x264 = new Controls.Video.Codec.x264();
                Controls.Video.Codec.H264_AMF h264_amf = new Controls.Video.Codec.H264_AMF();
                Controls.Video.Codec.H264_NVENC h264_nvenc = new Controls.Video.Codec.H264_NVENC();
                Controls.Video.Codec.H264_QSV h264_qsv = new Controls.Video.Codec.H264_QSV();
                Controls.Video.Codec.x265 x265 = new Controls.Video.Codec.x265();
                Controls.Video.Codec.HEVC_AMF hevc_amf = new Controls.Video.Codec.HEVC_AMF();
                Controls.Video.Codec.HEVC_NVENC hevc_nvenc = new Controls.Video.Codec.HEVC_NVENC();
                Controls.Video.Codec.HEVC_QSV hevc_qsv = new Controls.Video.Codec.HEVC_QSV();
                Controls.Video.Image.Codec.JPEG jpeg = new Controls.Video.Image.Codec.JPEG();
                Controls.Video.Image.Codec.PNG png = new Controls.Video.Image.Codec.PNG();
                Controls.Video.Image.Codec.WebP webp = new Controls.Video.Image.Codec.WebP();

                // Pixel Format
                string pixelFormat = "(" + string.Join("|", av1.pixelFormat
                                                 .Concat(ffv1.pixelFormat)
                                                 .Concat(huffYUV.pixelFormat)
                                                 .Concat(magicYUV.pixelFormat)
                                                 .Concat(mpeg2.pixelFormat)
                                                 .Concat(mpeg4.pixelFormat)
                                                 .Concat(theora.pixelFormat)
                                                 .Concat(vp8.pixelFormat)
                                                 .Concat(vp9.pixelFormat)
                                                 .Concat(x264.pixelFormat)
                                                 .Concat(h264_amf.pixelFormat)
                                                 .Concat(h264_nvenc.pixelFormat)
                                                 .Concat(h264_qsv.pixelFormat)
                                                 .Concat(x265.pixelFormat)
                                                 .Concat(hevc_amf.pixelFormat)
                                                 .Concat(hevc_nvenc.pixelFormat)
                                                 .Concat(hevc_qsv.pixelFormat)
                                                 .Concat(jpeg.pixelFormat)
                                                 .Concat(png.pixelFormat)
                                                 .Concat(webp.pixelFormat)
                                                 .Where(s => !string.IsNullOrWhiteSpace(s))
                                                 .Where(s => !s.Equals("none"))
                                                 .Where(s => !s.Equals("auto"))
                                                 .Distinct()
                                                 .OrderByDescending(x => x)
                                                 .ToList()
                                    ) +
                    ")";

                // Profile
                // (e.g. Hi444PP, Hi10P)
                string profile = @"(Hi(\d+)(P|PP))";

                // Size
                IEnumerable<string> sizeList = new List<string>()
                {
                    @"\d+p", //1080p
                    @"(8|4|2)\s?K(\s?UHD)?", //4K UHD
                    @"\d\d\d\d?x\d\d\d\d?", //1920x1080, 720x480
                };
                string size = "(" + string.Join("|", sizeList) + ")";

                // Scaling Algorithm
                string scaling = "(" + string.Join("|", VM.VideoView.Video_ScalingAlgorithm_Items
                                                        .Where(s => !string.IsNullOrWhiteSpace(s.ToString()))
                                                        .Where(s => !s.Equals("none"))
                                                        .Where(s => !s.Equals("auto"))
                                                        .OrderByDescending(x => x)
                                                        .ToList()
                                                    ) +
                                ")";

                // FPS
                // (e.g. 23.976fps, 60fps)
                string fps = @"(\d+.?\d+?\s?fps)";

                // Subtitles
                string langs = @"(Eng(lish)?|E|Ara(bic)?|Ben(gali)?|Chi(nese)?|Chn|Dut(ch)?|Fin(nish)?|Fre(nch)?|Ger(man)?|De|Hin(di)?|Ita(lian)?|Jap(anese)?|Kor(ean)?|Man(darin)?|Por(tuguese)?|Rus(sian)?|Spa(nish)?|Swe(dish)?|Tel(ugu)?|Vie(tnamese)?)";
                string subs = @"(Subtitle(s|d)?|Sub(s)?)";
                IEnumerable<string> subtitlesList = new List<string>()
                {
                    langs + @"[\-\s]?" + subs,
                    subs + @"[\-\s]?" + langs,
                    langs.Replace("|E", "") // Fix words like Ben, Chi, Man
                         .Replace("Ben(gali)?", "Bengali")
                         .Replace("Chi(nese)?", "Chinese")
                         .Replace("Man(darin)?", "Mandarin"),
                    @"(Multi[\-\s]?Sub(s)?|Subtitle(s|d)?|Sub(s|bed)?)"
                };
                string subtitles = string.Join("|", subtitlesList);

                // Channel
                IEnumerable<string> channelList = new List<string>()
                {
                    @"(\d+(?:\.\d+)?[.\-_\s]?)?(CH)\s?(?(1)|([.\s]?\d+(?:\.\d+)?)?)",
                    @"(\d+(?:\.\d+)?[.-_\s]?)?((Dolby[.\-_\s]?(Digital)?)[.\-_\s]?(?:Pro[.\-_\s]?(Logic)?[.\-_\s]?(II)?|Surround|Atmos|TrueHD|Vision)?)[.\-_\s]?(?(1)|([.\s]?\d+(?:\.\d+)?)?)",
                    @"(\d+(?:\.\d+)?[.\-_\s]?)?(AC3|AAC|DTS|(DD(?:P|\+?)))(?(1)|([.\-_\s]?\d+(?:\.\d+)?)?)",
                    @"([235679][.][0-2]([.][2])?)" //standalone 2.0, 2.1, 3.1, 5.1, 5.2, 6.1, 6.2, 7.1, 7.1.2, 9.1, 9.1.2
                };
                string channel = string.Join("|", channelList);

                // Bit Depth
                string bitDepth = @"(\d+[\-\s]?bit)";

                // Tags
                string tags = @"(\[.*?\])"; // [tag] // do not wrap with \b

                // Formats
                IEnumerable<string> formatsList = new List<string>()
                {
                    @"(DVD|BRD|BD|Br|HD|SD|Web)[\-\s]?(Rip|DL)?",
                    @"(HD|SD)(TV|R|C)?",
                    @"Blu[\-\s]?Ray",
                    @"Rip",
                    @"(\d+)?CD",
                    @"Playlist",
                };
                string formats = "(" + string.Join("|", formatsList) + ")";

                // Containers
                IEnumerable<string> containersList = Generate.Format.VideoFormats
                                                     .Concat(Generate.Format.AudioFormats)
                                                     .Concat(Generate.Format.ImageFormats)
                                                     .Distinct()
                                                     .OrderByDescending(x => x)
                                                     .ToList();

                // Remove Formats that are also Codecs
                // These are usually for raw files
                // This will prevent regex from running into duplicates in other categories
                containersList = containersList.Except(Types.Codecs.CodecTypes, StringComparer.OrdinalIgnoreCase).ToList();
                string containers = "(" + string.Join("|", containersList) + ")";

                // Codecs
                List<string> codecsList = new List<string>()
                {
                    "RAW",
                    "Lossless",
                    "HEVC",
                    @"(H[.\-]?)?(x)?26(4|5)[.\-\s]?(QOQ)?", // H.264, x264, x264-QOQ
                    "QOQ",
                    "NF",
                    "FP",
                };
                codecsList.AddRange(Types.Codecs.CodecTypes);
                string codecs = "(" + string.Join("|", codecsList) + ")";

                // Video
                IEnumerable<string> videoList = new List<string>()
                {
                    "((UH|H|S)D)", // UHD, HD, SD
                    @"\d+[\-\s]?bit",
                };
                string video = "(" + string.Join("|", videoList) + ")";

                // Audio
                IEnumerable<string> audioList = new List<string>()
                {
                    @"(Dual|Multi|Original|Orig|Org)[\-\s]?(Audio|Aud)",
                    langs + @"[\-\s]?Dub",
                    @"(Non[\-\s])?English[\-\s]?Translated",
                    @"Dub(bed)?",
                };
                string audio = "(" + string.Join("|", audioList) + ")";

                // File
                string file = @"(\d+([.]?\d+?)?[.\-_\s]?((M|G|T)B))"; //100.5MB, 100GB, 100TB

                // Labels
                IEnumerable<string> labelsList = new List<string>()
                {
                    "Amazon",
                    "AMZN",
                    "iTunes",
                    "Spotify",
                    "Repack",
                    "Complete",
                };
                string labels = "(" + string.Join("|", labelsList) + ")";

                // Custom
                string custom = string.Empty;
                if (!string.IsNullOrWhiteSpace(VM.ConfigureView.InputFileNameTokensCustom_Text))
                {
                    string[] arrInputFileNameTokensCustom_Items = VM.ConfigureView.InputFileNameTokensCustom_Text
                                                                  .Split(',');

                    List<string> listInputFileNameTokensCustom_Items = new List<string>(); // Regex Escaped
                    for (var i = 0; i < arrInputFileNameTokensCustom_Items.Length; i++)
                    {
                        // Add item to list
                        // Replace invalid filename characters
                        // Escape for Regex rules
                        listInputFileNameTokensCustom_Items.Add(Regex.Escape(Regex.Replace(arrInputFileNameTokensCustom_Items[i].Trim(), "[\\/:*?\"<>|]", "")));
                    }

                    listInputFileNameTokensCustom_Items = listInputFileNameTokensCustom_Items // Clean up and remove empty strings from list
                                                          .Where(s => !string.IsNullOrWhiteSpace(s))
                                                          .Distinct()
                                                          .ToList();

                    custom = "(" + string.Join("|", listInputFileNameTokensCustom_Items) + ")";
                }

                //MessageBox.Show(custom); //debug

                // Symbols
                // Stray Parentheses
                string symbols = @"\(\)|\(\s+\)"; // do not wrap with \b


                // Build regex rules
                // Order is important
                IEnumerable<string> regexTagsList = new List<string>()
                {
                    tags,
                    @"((?<=_)|\b)(", // opening
                    hwAccelTranscode,
                    sampleRate,
                    bitRate,
                    channel,
                    pixelFormat,
                    formats,
                    containers,
                    codecs,
                    @"(cv-copy)",
                    @"(ca-copy)",
                    video,
                    pass,
                    presets,
                    profile,
                    size,
                    @"(sz-source)",
                    scaling,
                    fps,
                    subtitles,
                    audio,
                    bitDepth,
                    file,
                    labels,
                    custom,
                    @")((?=_)|\b)", //closing
                    symbols,
                };

                string regexTags = string.Join("|", regexTagsList
                                             // don't re-order
                                             .Distinct()
                                       )
                                       // Remove invalid rules caused by adding "((?<=_)|\b)(" to the list
                                       .Replace("(|", "(")
                                       .Replace("|)", ")");

                // Remove Tags and Tokens
                filename = Regex.Replace(
                    filename,
                    regexTags,
                    "",
                    RegexOptions.IgnoreCase
                );

                // Fix Multiple Spaces, Periods, Dashes, Underscores
                filename = Regex.Replace(filename, @"[ ]{1,}", " ");
                filename = Regex.Replace(filename, @"[.]{1,}", ".");
                filename = Regex.Replace(filename, @"[\-]{1,}", "-");
                filename = Regex.Replace(filename, @"[_]{1,}", "_");

                //MessageBox.Show(filename); //debug

                //// Log Console Message /////////
                //Log.WriteAction = () =>
                //{
                //    Log.logParagraph.Inlines.Add(new LineBreak());
                //    Log.logParagraph.Inlines.Add(new Bold(new Run("Tag Remover Regex:\r\n")) { Foreground = Log.ConsoleDefault });
                //    Log.logParagraph.Inlines.Add(new Run(regexTags) { Foreground = Log.ConsoleDefault });
                //};
                //Log.LogActions.Add(Log.WriteAction);

                return filename
                       .Trim()
                       .Trim('.')
                       .Trim('-')
                       .Trim('_');
            }

            // stay the same
            else
            {
                return filename;
            }
        }


        /// <summary>
        /// Output Filename Spacing (Method)
        /// </summary>
        public static String OutputFileNameSpacing(string filename)
        {
            switch (VM.ConfigureView.OutputFileNameSpacing_SelectedItem)
            {
                case "Original":
                    return filename;

                case "Spaces":
                    // periods
                    filename = Regex.Replace(filename, @"(?<!CRF\d*)(\d[.]\d)|[.]", m => m.Groups[1].Success ? m.Groups[1].Value : " ");
                    // dashes
                    filename = Regex.Replace(filename, @"(-+)|-", "$1");
                    // underscores
                    filename = Regex.Replace(filename, "_", " ");
                    return filename;

                case "Periods":
                    // spaces
                    filename = Regex.Replace(filename, " ", ".");
                    // dashes
                    filename = Regex.Replace(filename, "-", ".");
                    // underscores
                    filename = Regex.Replace(filename, "_", ".");
                    return filename;

                case "Dashes":
                    // spaces
                    filename = Regex.Replace(filename, " ", "-");
                    // periods
                    filename = Regex.Replace(filename, @"(?<!CRF\d*)(\d[.]\d)|[.]", m => m.Groups[1].Success ? m.Groups[1].Value : "-");
                    // underscores
                    filename = Regex.Replace(filename, "_", "-");
                    return filename;

                case "Underscores":
                    // spaces
                    filename = Regex.Replace(filename, " ", "_");
                    // periods
                    filename = Regex.Replace(filename, @"(?<!CRF\d*)(\d[.]\d)|[.]", m => m.Groups[1].Success ? m.Groups[1].Value : "_");
                    // dashes
                    filename = Regex.Replace(filename, "-", "_");
                    return filename;

                default:
                    return filename;
            }
        }

    }
}
