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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
            switch (VM.MainView.Batch_IsChecked)
            {
                // -------------------------
                // Single File
                // -------------------------
                case false:
                    // -------------------------
                    // Get Output Ext
                    // -------------------------
                    Controls.Format.Controls.OutputFormatExt();

                    // -------------------------
                    // Open 'Save File'
                    // -------------------------
                    Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();

                    // File Filter
                    saveFile.Filter = Output_SaveFileDialog_Filter();

                    // -------------------------
                    // 'Save File' Default Path same as Input Directory
                    // -------------------------
                    try
                    {
                        //string previousPath = Settings.Default.OutputDir.ToString();
                        // Use Input Path if Previous Path is Null
                        //if (string.IsNullOrWhiteSpace(previousPath))
                        //{
                        //    saveFile.InitialDirectory = inputDir;
                        //}

                        if (File.Exists(Controls.Configure.configFile))
                        {
                            Controls.Configure.INIFile conf = new Controls.Configure.INIFile(Controls.Configure.configFile);
                            outputPreviousPath = conf.Read("User", "OutputPreviousPath");

                            // Use Input Path is Output Path is Empty
                            if (string.IsNullOrWhiteSpace(outputPreviousPath))
                            {
                                saveFile.InitialDirectory = inputPreviousPath;
                            }
                            // Use Output Path if it exists
                            else
                            {
                                saveFile.InitialDirectory = outputPreviousPath;
                            }
                        }
                    }
                    catch
                    {

                    }

                    // Remember Last Dir
                    //saveFile.RestoreDirectory = true;
                    // Default Extension
                    saveFile.DefaultExt = outputExt;

                    // Default file name if empty
                    if (string.IsNullOrWhiteSpace(inputFileName))
                    {
                        saveFile.FileName = "File";
                    }
                    // If file name exists
                    else
                    {
                        // Output Path
                        outputDir = inputDir;

                        // File Renamer
                        // Get new output file name (1) if already exists
                        // Add Settings to File Name
                        // e.g. MyFile x265 CRF25 1080p AAC 320k.mp4
                        //outputFileName = FileRenamer(FileNameAddSettings(inputFileName));
                        outputFileName = FileRenamer(inputFileName);
                        outputFileName_Original = outputFileName;
                        outputFileName_Tokens = FileRenamer(FileNameAddSettings(inputFileName));

                        // Same as input file name
                        saveFile.FileName = outputFileName;
                    }


                    // -------------------------
                    // Show Dialog Box
                    // -------------------------
                    Nullable<bool> result = saveFile.ShowDialog();

                    // Process Dialog Box
                    if (result == true)
                    {
                        if (IsValidPath(saveFile.FileName))
                        {
                            // Output Path
                            outputDir = Path.GetDirectoryName(saveFile.FileName).TrimEnd('\\') + @"\";
                            // Output Extension
                            outputExt = Path.GetExtension(saveFile.FileName);

                            outputFileName_Original = Path.GetFileNameWithoutExtension(saveFile.FileName);

                            // Default
                            if (!VM.ConfigureView.OutputNaming_ListView_SelectedItems.Any())
                            {
                                outputFileName = Path.GetFileNameWithoutExtension(outputFileName_Original);

                                // Display Path+File+Ext in Output Textbox
                                VM.MainView.Output_Text = Path.Combine(outputDir, outputFileName + outputExt);
                            }
                            // File Name Settings
                            else
                            {
                                outputFileName_Tokens = Path.GetFileNameWithoutExtension(FileNameAddSettings(outputFileName_Original));

                                // Display Path+File+Ext in Output Textbox
                                VM.MainView.Output_Text = Path.Combine(outputDir, outputFileName_Tokens + outputExt);
                            }
                        }

                        // Save Previous Path
                        if (File.Exists(Controls.Configure.configFile))
                        {
                            try
                            {
                                Controls.Configure.INIFile conf = new Controls.Configure.INIFile(Controls.Configure.configFile);
                                conf.Write("User", "OutputPreviousPath", outputDir);
                            }
                            catch
                            {

                            }
                        }
                    }
                    break;

                // -------------------------
                // Batch
                // -------------------------
                case true:
                    // Open 'Select Folder'
                    System.Windows.Forms.FolderBrowserDialog outputFolder = new System.Windows.Forms.FolderBrowserDialog();
                    System.Windows.Forms.DialogResult resultBatch = outputFolder.ShowDialog();

                    // Process Dialog Box
                    if (resultBatch == System.Windows.Forms.DialogResult.OK)
                    {
                        if (IsValidPath(outputFolder.SelectedPath.TrimEnd('\\') + @"\"))
                        {
                            // Display path and file in Output Textbox
                            VM.MainView.Output_Text = outputFolder.SelectedPath.TrimEnd('\\') + @"\";

                            // Remove Double Slash in Root Dir, such as C:\
                            VM.MainView.Output_Text = VM.MainView.Output_Text.Replace(@"\\", @"\");

                            // Output Path
                            outputDir = Path.GetDirectoryName(VM.MainView.Output_Text);

                            // Add slash to inputDir path if missing
                            outputDir = outputDir.TrimEnd('\\') + @"\";
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
            // Get Output Extension (Method)
            Controls.Format.Controls.OutputFormatExt();

            if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text)) // Check Input
            {
                switch (IsWebURL(VM.MainView.Input_Text))
                {
                    // -------------------------
                    // Local File
                    // -------------------------
                    case false:
                        switch (VM.MainView.Batch_IsChecked)
                        {
                            // -------------------------
                            // Single File
                            // -------------------------
                            case false:
                                // Input Not Empty
                                // Output Empty
                                // Default Output to be same as Input Directory
                                if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text) &&
                                    string.IsNullOrWhiteSpace(VM.MainView.Output_Text)
                                    )
                                {
                                    // Default Output Dir to be same as Input Directory
                                    outputDir = inputDir;
                                    outputFileName = inputFileName;
                                    outputFileName_Original = outputFileName;

                                    // Add Settings to File Name
                                    // e.g. MyFile x265 CRF25 1080p AAC 320k.mp4
                                    //outputFileName = FileNameAddSettings(inputFileName);
                                    outputFileName_Tokens = FileNameAddSettings(inputFileName);
                                }

                                // Input Not Empty
                                // Output Not Empty
                                else
                                {
                                    outputDir = Path.GetDirectoryName(VM.MainView.Output_Text).TrimEnd('\\') + @"\"; // eg. C:\Output\Path\
        
                                    outputFileName = Path.GetFileNameWithoutExtension(outputFileName_Original);

                                    outputFileName_Tokens = FileNameAddSettings(Path.GetFileNameWithoutExtension(outputFileName_Original));
                                }

                                // -------------------------
                                // File Renamer
                                // -------------------------
                                // Pressing Script or Convert while Output TextBox is empty
                                if (string.Equals(inputDir, outputDir, StringComparison.OrdinalIgnoreCase) &&
                                    string.Equals(inputFileName, outputFileName, StringComparison.OrdinalIgnoreCase) &&
                                    string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
                                {
                                    // Renamer 
                                    // Get new output file name (1) if already exists
                                    // Add Settings to File Name
                                    // e.g. MyFile x265 CRF25 1080p AAC 320k.mp4
                                    //outputFileName = FileRenamer(FileNameAddSettings(inputFileName));
                                    outputFileName = FileRenamer(inputFileName);
                                    outputFileName_Original = outputFileName;
                                    outputFileName_Tokens = FileRenamer(FileNameAddSettings(inputFileName)); // problem?
                                }

                                // -------------------------
                                // Image Sequence Renamer
                                // -------------------------
                                if (VM.FormatView.Format_MediaType_SelectedItem == "Sequence")
                                {
                                    outputFileName = "image-%03d"; //must be this name
                                    //outputFileName_Original = "image-%03d";
                                    outputFileName_Tokens = "image-%03d";
                                }

                                // -------------------------
                                // Combine Output
                                // -------------------------
                                // Default
                                if (!VM.ConfigureView.OutputNaming_ListView_SelectedItems.Any())
                                {
                                    output = Path.Combine(outputDir, outputFileName + outputExt);
                                }
                                // File Name Settings
                                else
                                {
                                    output = Path.Combine(outputDir, outputFileName_Tokens + outputExt);
                                }

                                // -------------------------
                                // Update TextBox
                                // -------------------------
                                // Used if FileRenamer() changes name: filename (1)
                                // Only used for Single File, ignore Batch and Web URLs
                                VM.MainView.Output_Text = output;
                                break; // end single file

                            // -------------------------
                            // Batch
                            // -------------------------
                            case true:
                                // Input Not Empty
                                // Output Empty
                                // Default Output to be same as Input Directory
                                if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text) &&
                                    string.IsNullOrWhiteSpace(VM.MainView.Output_Text)
                                    )
                                {
                                    VM.MainView.Output_Text = VM.MainView.Input_Text;
                                }

                                // Add slash to Batch Output Text folder path if missing
                                // If Output is not Empty
                                if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
                                {
                                    VM.MainView.Output_Text = VM.MainView.Output_Text.TrimEnd('\\') + @"\";
                                }

                                outputDir = VM.MainView.Output_Text.TrimEnd('\\') + @"\";

                                // -------------------------
                                // Combine Output  
                                // -------------------------
                                switch (VM.ConfigureView.Shell_SelectedItem)
                                {
                                    // CMD
                                    case "CMD":
                                        // Note: %f is filename, %~f is full path 
                                        // eg. C:\Output Folder\%~nf.mp4
                                        output = Path.Combine(outputDir, "%~nf" + outputExt); // eg. C:\Output Folder\%~nf.mp4
                                        break;

                                    // PowerShell
                                    case "PowerShell":
                                        output = Path.Combine(outputDir, "$outputName" + outputExt); // eg. C:\Output Folder\$name.mp4
                                        break;
                                }
                                break; // end batch
                        }
                        break; // end local file

                    // -------------------------
                    // YouTube Download
                    // -------------------------
                    case true:
                        // -------------------------
                        // Auto Output Path
                        // -------------------------
                        if (string.IsNullOrWhiteSpace(VM.MainView.Output_Text))
                        {
                            outputDir = downloadDir; // Default

                            switch (VM.ConfigureView.Shell_SelectedItem)
                            {
                                // CMD
                                case "CMD":
                                    // Note: %f is filename, %~f is full path
                                    outputFileName = "%f"; // eg. C:\Output Folder\%f.mp4
                                    break;

                                // PowerShell
                                case "PowerShell":
                                    outputFileName = "$name"; // eg. C:\Output Folder\$name.mp4
                                    break;
                            }

                            // Check if output filename already exists
                            // Check if YouTube Download Format is the same as Output Extension
                            // The youtub-dl merged format for converting should be mkv for converting, mp4 for download-only
                            //if ("." + YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
                            //                                VM.VideoView.Video_Codec_SelectedItem,
                            //                                VM.SubtitleView.Subtitle_Codec_SelectedItem,
                            //                                VM.AudioView.Audio_Codec_SelectedItem
                            //                                )
                            //                                ==
                            //                                outputExt
                            //                                )
                            //{
                            //    // Add (1)
                            //    outputFileName = "%f" + " (1)";
                            //}
                            //else
                            //{
                            //    outputFileName = "%f";
                            //}

                            // Combine Output
                            output = Path.Combine(outputDir, outputFileName + outputExt); // eg. C:\Users\Example\Downloads\%f.webm

                            // -------------------------
                            // Update TextBox
                            // -------------------------
                            // Display Folder + file (%f) + extension
                            VM.MainView.Output_Text = Path.Combine(outputDir, outputFileName + outputExt);
                        }

                        // -------------------------
                        // User Defined Output Path
                        // -------------------------
                        else
                        {
                            outputDir = Path.GetDirectoryName(VM.MainView.Output_Text).TrimEnd('\\') + @"\"; // eg. C:\Output\Path\
                            outputFileName = Path.GetFileNameWithoutExtension(VM.MainView.Output_Text);

                            // Combine Output
                            output = Path.Combine(outputDir, outputFileName + outputExt);
                        }
                        break; // end youtube-dl
                }
            }

            // -------------------------
            // Input Empty
            // -------------------------
            // Output must have an Input
            else
            {
                outputDir = string.Empty;
                outputFileName = string.Empty;
                output = string.Empty;
            }


            // Return Value
            return output;
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

            // Enable / Disable "Open Output Location" Buttion
            if (//!string.IsNullOrWhiteSpace(VM.MainView.Output_Text)
                IsValidPath(VM.MainView.Output_Text) && // Detect Invalid Characters
                Path.IsPathRooted(VM.MainView.Output_Text) == true
                )
            {
                bool exists = Directory.Exists(Path.GetDirectoryName(VM.MainView.Output_Text));

                if (exists)
                {
                    VM.MainView.Output_Location_IsEnabled = true;
                }
                else
                {
                    VM.MainView.Output_Location_IsEnabled = false;
                }
            }
        }

        /// <summary>
        /// Output Textbox - KeyUp
        /// </summary>
        private void tbxOutput_KeyUp(object sender, KeyEventArgs e)
        {
            outputFileName_Original = Path.GetFileNameWithoutExtension(VM.MainView.Output_Text);
        }

        /// <summary>
        /// Output Textbox - Paste
        /// </summary>
        private void OnOutputTextBoxPaste(object sender, DataObjectPastingEventArgs e)
        {
            var isText = e.SourceDataObject.GetDataPresent(DataFormats.UnicodeText, true);
            if (!isText) return;

            var text = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;

            // Save Output File Name
            outputFileName_Original = Path.GetFileNameWithoutExtension(VM.MainView.Output_Text);
        }

        /// <summary>
        /// Output Textbox - Drag and Drop
        /// </summary>
        private void tbxOutput_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }

        private void tbxOutput_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            VM.MainView.Output_Text = buffer.First();

            // Save Output File Name
            outputFileName_Original = Path.GetFileNameWithoutExtension(VM.MainView.Output_Text);
        }


        /// <summary>
        /// Open Output Folder Button
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
        public static String FileRenamer(string filename)
        {
            string output = Path.Combine(outputDir, filename + outputExt);
            string outputNewFileName = string.Empty;

            int count = 1;

            if (File.Exists(output))
            {
                while (File.Exists(output))
                {
                    outputNewFileName = string.Format("{0}({1})", filename + " ", count++);
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
        /// File Name Add Settings (Method)
        /// </summary>
        public static String FileNameAddSettings(string filename)
        {
            //MessageBox.Show(string.Join("\n",VM.ConfigureView.OutputNaming_ListView_SelectedItems)); //debug

            // -------------------------
            // Halt if No Output Naming is Selected
            // -------------------------
            if (!VM.ConfigureView.OutputNaming_ListView_SelectedItems.Any())
            {
                // return original
                return outputFileName;
            }

            // -------------------------
            // Halt if Batch
            // -------------------------
            if (VM.MainView.Batch_IsChecked == true)
            {
                // return original
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
            // HW Accel
            string video_hwAccel_Transcode = string.Empty;
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("HW Accel"))
            {
                if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_HWAccel_Transcode_SelectedItem))
                {
                    video_hwAccel_Transcode = SettingsCheck(VM.VideoView.Video_HWAccel_Transcode_SelectedItem.Replace(" ", "-"));
                }
            }

            // Video Codec
            string video_Codec = string.Empty;
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
            string video_Codec_Copy = string.Empty;
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
            string video_Pass = string.Empty;
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Pass"))
            {
                // null check prevents unknown crash
                if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_Pass_SelectedItem))
                {
                    video_Pass = SettingsCheck(VM.VideoView.Video_Pass_SelectedItem.Replace(" ", "-")); // do not lowercase
                }
            }

            // Video Bit Rate
            string video_BitRate = string.Empty;
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Video Bit Rate"))
            {
                if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_BitRate_Text))
                {
                    video_BitRate = SettingsCheck(VM.VideoView.Video_BitRate_Text.ToUpper()); //K / M
                }
            }

            // CRF
            string video_CRF = string.Empty;
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Video CRF"))
            {
                if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_CRF_Text))
                {
                    video_CRF = "CRF" + SettingsCheck(VM.VideoView.Video_CRF_Text);
                }
            }

            // Preset
            string video_Preset = string.Empty;
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
            string video_PixelFormat = string.Empty;
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Contains("Pixel Format"))
            {
                if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_PixelFormat_SelectedItem))
                {
                    video_PixelFormat = SettingsCheck(VM.VideoView.Video_PixelFormat_SelectedItem);
                }
            }

            // Scale/Size
            string video_Size = string.Empty;
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
            string video_Size_Source = string.Empty;
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
            string video_ScalingAlgorithm = string.Empty;
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
            string video_FPS = string.Empty;
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
                        audio_BitRate = SettingsCheck(VM.AudioView.Audio_Quality_SelectedItem) + "k";
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
            IList<string> newFileName = new List<string>();
            // Add Original File Name
            newFileName.Add(filename);

            // Create new File Name by Options Order Arranged
            for (var i = 0; i < VM.ConfigureView.OutputNaming_ListView_SelectedItems.Count; i++)
            {
                switch (VM.ConfigureView.OutputNaming_ListView_SelectedItems[i])
                {
                    // Add

                    // Format
                    case "Input Ext":
                        newFileName.Add(format_inputExt);
                        break;

                    // Video
                    case "HW Accel":
                        newFileName.Add(video_hwAccel_Transcode);
                        break;
                    case "Video Codec":
                        newFileName.Add(video_Codec);
                        break;
                    case "Video Codec Copy":
                        newFileName.Add(video_Codec_Copy);
                        break;
                    case "Pass":
                        newFileName.Add(video_Pass);
                        break;
                    case "Video Bit Rate":
                        newFileName.Add(video_BitRate);
                        break;
                    case "Video CRF":
                        newFileName.Add(video_CRF);
                        break;
                    case "Preset":
                        newFileName.Add(video_Preset);
                        break;
                    case "Pixel Format":
                        newFileName.Add(video_PixelFormat);
                        break;
                    case "Frame Rate":
                        newFileName.Add(video_FPS);
                        break;
                    case "Size":
                        newFileName.Add(video_Size);
                        break;
                    case "Size Source":
                        newFileName.Add(video_Size_Source);
                        break;
                    case "Scaling":
                        newFileName.Add(video_ScalingAlgorithm);
                        break;

                    // Audio
                    case "Audio Codec":
                        newFileName.Add(audio_Codec);
                        break;
                    case "Audio Codec Copy":
                        newFileName.Add(audio_Codec_Copy);
                        break;
                    case "Channel":
                        newFileName.Add(audio_Channel);
                        break;
                    case "Audio Bit Rate":
                        newFileName.Add(audio_BitRate + audio_VBR);
                        break;
                    case "Sample Rate":
                        newFileName.Add(audio_SampleRate);
                        break;
                    case "Bit Depth":
                        newFileName.Add(audio_BitDepth);
                        break;
                }
            }

            // New File Name
            return string.Join(" ", newFileName
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
        }

        /// <summary>
        /// Settings Check
        /// </summary>
        public static String SettingsCheck(string s)
        {
            // Fail
            if (string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }
            // Fail
            else if (s.Equals("off", StringComparison.OrdinalIgnoreCase) ||
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
        /// Output Path Update Display (Method)
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
        public static void OutputPath_UpdateDisplay()
        {
            // -------------------------
            // Halts
            // -------------------------         
            if (// Halt if Input is Empty
                string.IsNullOrWhiteSpace(VM.MainView.Input_Text) ||
                // Halt if Input Extension is Empty
                // Path Combine with null file extension causes error
                string.IsNullOrWhiteSpace(inputExt) ||
                // Halt if Output is Empty
                string.IsNullOrWhiteSpace(VM.MainView.Output_Text) ||
                // Halt if Output Directory is Empty
                // Prevents a crash when changing containers if input and output paths are not empty
                string.IsNullOrWhiteSpace(outputDir) ||
                // Halt if Output Filename is Empty
                string.IsNullOrWhiteSpace(outputFileName) ||
                // Halt if Batch
                VM.MainView.Batch_IsChecked == true
                )
            {
                return;
            }

            // Halt if Output Naming is Empty
            // Don't use
            //if (VM.ConfigureView.OutputNaming_ListView_SelectedItems == null ||
            //    VM.ConfigureView.OutputNaming_ListView_SelectedItems.Count == 0)
            //{
            //    return;
            //}

            // -------------------------
            // Update Ouput Textbox with current Format extension
            // -------------------------
            // -------------------------
            // Default
            // -------------------------
            if (!VM.ConfigureView.OutputNaming_ListView_SelectedItems.Any() ||
                // Is Web URL
                IsWebURL(VM.MainView.Input_Text) == true)
            {
                // File Renamer
                // Add (1) if File Names are the same
                if (!string.IsNullOrWhiteSpace(inputDir) &&
                    string.Equals(inputFileName, outputFileName, StringComparison.OrdinalIgnoreCase))
                {
                    //outputFileName = FileRenamer(inputFileName);

                    // Input Not Empty
                    // Output Empty
                    // Default Output to be same as Input Directory
                    if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text) &&
                        string.IsNullOrWhiteSpace(VM.MainView.Output_Text)
                        )
                    {
                        outputFileName = FileRenamer(inputFileName);
                    }

                    // Input Not Empty
                    // Output Not Empty
                    else
                    {
                        // Web URL
                        if (IsWebURL(VM.MainView.Input_Text) == true)
                        {
                            outputFileName = FileRenamer(outputFileName);
                        }
                        // Local File
                        else
                        {
                            outputFileName = FileRenamer(outputFileName_Original);
                        }
                    }
                }

                // Display
                VM.MainView.Output_Text = Path.Combine(outputDir, outputFileName + outputExt);

                //MessageBox.Show("Default"); //debug
            }

            // -------------------------
            // File Name Settings
            // -------------------------
            else
            {
                // File Renamer
                // Add (1) if File Names are the same
                if (!string.IsNullOrWhiteSpace(inputDir) &&
                    string.Equals(inputFileName, outputFileName_Tokens, StringComparison.OrdinalIgnoreCase))
                {
                    //outputFileName_Tokens = FileRenamer(FileNameAddSettings(inputFileName));
                    //outputFileName_Tokens = FileRenamer(FileNameAddSettings(outputFileName_Original));

                    // Input Not Empty
                    // Output Empty
                    // Default Output to be same as Input Directory
                    if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text) &&
                        string.IsNullOrWhiteSpace(VM.MainView.Output_Text)
                        )
                    {
                        outputFileName_Tokens = FileRenamer(FileNameAddSettings(inputFileName));
                    }

                    // Input Not Empty
                    // Output Not Empty
                    else
                    {
                        outputFileName_Tokens = FileRenamer(FileNameAddSettings(outputFileName_Original));
                    }
                }
                else
                {
                    // Regenerate
                    //outputFileName_Tokens = FileNameAddSettings(outputFileName); //probem
                    outputFileName_Tokens = FileNameAddSettings(outputFileName_Original); //working

                    //MessageBox.Show("Regenerate"); //debug
                }

                // Display
                VM.MainView.Output_Text = Path.Combine(outputDir, outputFileName_Tokens + outputExt);
            }
        }

    }
}
