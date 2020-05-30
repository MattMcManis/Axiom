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
                            // Display path and file in Output Textbox
                            VM.MainView.Output_Text = saveFile.FileName;

                            // Output Path
                            outputDir = Path.GetDirectoryName(VM.MainView.Output_Text).TrimEnd('\\') + @"\";

                            // Output Filename (without extension)
                            outputFileName = Path.GetFileNameWithoutExtension(VM.MainView.Output_Text);

                            // Add slash to inputDir path if missing
                            outputDir = outputDir.TrimEnd('\\') + @"\";

                            // Debug
                            //MessageBox.Show(VM.MainView.Output_Text);
                            //MessageBox.Show(outputDir);
                        }

                        //// Add slash to inputDir path if missing
                        //if (IsValidPath(outputDir))
                        //{
                        //    if (!outputDir.EndsWith("\\"))
                        //    {
                        //        outputDir = outputDir.TrimEnd('\\') + @"\";
                        //    }
                        //}

                        // Save Previous Path
                        //Settings.Default.OutputDir = outputDir;
                        //Settings.Default.Save();
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

                        // Add slash to inputDir path if missing
                        //if (IsValidPath(outputDir))
                        //{
                        //    if (!outputDir.EndsWith("\\"))
                        //    {
                        //        outputDir = outputDir.TrimEnd('\\') + @"\";
                        //    }
                        //}
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

                                    // Add Settings to File Name
                                    // e.g. MyFile x265 CRF25 1080p AAC 320k.mp4
                                    //outputFileName = FileNameAddSettings(inputFileName);
                                }

                                // Input Not Empty
                                // Output Not Empty
                                else
                                {
                                    outputDir = Path.GetDirectoryName(VM.MainView.Output_Text).TrimEnd('\\') + @"\"; // eg. C:\Output\Path\
                                    outputFileName = Path.GetFileNameWithoutExtension(VM.MainView.Output_Text);
                                }

                                // -------------------------
                                // File Renamer
                                // -------------------------
                                // Pressing Script or Convert while Output TextBox is empty
                                if (inputDir == outputDir &&
                                    inputFileName == outputFileName &&
                                    string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
                                {
                                    // Renamer 
                                    // Get new output file name (1) if already exists
                                    // Add Settings to File Name
                                    // e.g. MyFile x265 CRF25 1080p AAC 320k.mp4
                                    //outputFileName = FileRenamer(FileNameAddSettings(inputFileName));

                                    outputFileName = FileRenamer(inputFileName);
                                }

                                // -------------------------
                                // Image Sequence Renamer
                                // -------------------------
                                if (VM.FormatView.Format_MediaType_SelectedItem == "Sequence")
                                {
                                    outputFileName = "image-%03d"; //must be this name
                                }

                                // -------------------------
                                // Combine Output
                                // -------------------------
                                output = Path.Combine(outputDir, outputFileName + outputExt);

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
                                        //output = Path.Combine(outputDir, "$name" + outputExt); // eg. C:\Output Folder\$name.mp4
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
                            //output = outputDir + outputFileName + outputExt;
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
        /// Output Textbox
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
            //string output = outputDir + filename + outputExt;
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
            // -------------------------
            // Format
            // -------------------------
            string format_inputExt = SettingsCheck(Path.GetExtension(VM.MainView.Input_Text)
                                                   .Replace(".", "")
                                                   .ToLower()
                                                   );

            // -------------------------
            // Video
            // -------------------------
            string video_hwAccel_Transcode = SettingsCheck(VM.VideoView.Video_HWAccel_Transcode_SelectedItem);
            string video_Codec = SettingsCheck(VM.VideoView.Video_Codec_SelectedItem);
            string video_Pass = SettingsCheck(VM.VideoView.Video_Pass_SelectedItem);
            string video_BitRate = SettingsCheck(VM.VideoView.Video_BitRate_Text);

            // Preset
            string video_Preset = VM.VideoView.Video_EncodeSpeed_Items.FirstOrDefault(item => item.Name == VM.VideoView.Video_EncodeSpeed_SelectedItem)?.Name;
            if (!string.IsNullOrWhiteSpace(video_Preset))
            {
                video_Preset = "p-" + video_Preset.ToLower();
            }

            // Pixel Format
            string video_PixelFormat = SettingsCheck(VM.VideoView.Video_PixelFormat_SelectedItem);

            // Scale/Size
            string video_Scale = SettingsCheck(VM.VideoView.Video_Scale_SelectedItem);

            // Scaling
            string video_ScalingAlgorithm = SettingsCheck(VM.VideoView.Video_ScalingAlgorithm_SelectedItem);

            // FPS
            string video_FPS = SettingsCheck(VM.VideoView.Video_FPS_SelectedItem);
            if (!string.IsNullOrWhiteSpace(video_FPS))
            {
                video_FPS = video_FPS + "fps";
            }

            // -------------------------
            // Audio
            // -------------------------
            // Codec
            string audio_Codec = SettingsCheck(VM.AudioView.Audio_Codec_SelectedItem);

            // Channel
            string audio_Channel = SettingsCheck(VM.AudioView.Audio_Channel_SelectedItem);
            if (!string.IsNullOrWhiteSpace(audio_Channel))
            {
                audio_Channel = "CH" + audio_Channel;
            }

            // VBR
            string audio_VBR = string.Empty;
            if (VM.AudioView.Audio_VBR_IsChecked == true)
            {
                audio_VBR = "VBR";
            }

            // Bit Rate
            string audio_BitRate = SettingsCheck(VM.AudioView.Audio_BitRate_Text);

            // Compression Level
            string audio_CompressionLevel = SettingsCheck(VM.AudioView.Audio_CompressionLevel_SelectedItem);

            // Sample Rate
            string audio_SampleRate = SettingsCheck(VM.AudioView.Audio_SampleRate_SelectedItem) + "kHz";

            // Bit Depth
            string audio_BitDepth = SettingsCheck(VM.AudioView.Audio_BitDepth_SelectedItem) + "-bit";

            // Merge
            List<string> newFileName = new List<string>()
            {
                // Original File Name
                filename,

                // Format
                format_inputExt,

                // Video
                video_hwAccel_Transcode,
                video_Codec,
                video_Pass,
                video_BitRate,
                video_Preset,
                video_PixelFormat,
                video_Scale,
                video_ScalingAlgorithm,
                video_FPS,

                // Audio
                audio_Codec,
                audio_Channel,
                audio_VBR,
                audio_BitRate,
                audio_CompressionLevel,
                audio_SampleRate,
                audio_BitDepth
            };

            // New File Name
            return string.Join(" ", newFileName
                                    .Where(s => !string.IsNullOrWhiteSpace(s))
                                    .Where(s => !s.Equals("CRF"))
                                    .Where(s => !s.Equals("fps"))
                                    .Where(s => !s.Equals("p-"))
                                    .Where(s => !s.Equals("-bit"))
                                    .Where(s => !s.Equals("kHz"))
                                    .Where(s => !s.Equals("CH"))
                              );
        }

        public static String SettingsCheck(string s)
        {
            if (string.IsNullOrWhiteSpace(s) ||
                s.Equals("off", StringComparison.OrdinalIgnoreCase) ||
                s.Equals("none", StringComparison.OrdinalIgnoreCase) ||
                s.Equals("auto", StringComparison.OrdinalIgnoreCase) ||
                s.Equals("Source", StringComparison.OrdinalIgnoreCase) ||
                s.Equals("Copy", StringComparison.OrdinalIgnoreCase) ||
                s.Equals("CRF", StringComparison.OrdinalIgnoreCase) ||
                s.Equals("CH", StringComparison.OrdinalIgnoreCase)
                )
            {
                return string.Empty;
            }
            else
            {
                return s;
            }
        }



    }
}
