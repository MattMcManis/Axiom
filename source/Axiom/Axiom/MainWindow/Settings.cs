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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;
using Axiom;
using System.Collections.ObjectModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class MainWindow : Window
    {
        public static void ConfigDirectoryOpen(string path)
        {
            if (Directory.Exists(path))
            {
                Process.Start("explorer.exe", path);
            }
            else
            {
                // Yes/No Dialog Confirmation
                //
                MessageBoxResult resultExport = MessageBox.Show("The Axiom UI directory does not exist in this location yet. Automatically create it?",
                                                                "Directory Not Found",
                                                                MessageBoxButton.YesNo,
                                                                MessageBoxImage.Information);
                switch (resultExport)
                {
                    // Create
                    case MessageBoxResult.Yes:
                        try
                        {
                            Directory.CreateDirectory(path);
                            Process.Start("explorer.exe", path);
                        }
                        catch
                        {
                            MessageBox.Show("Could not create directory. May require Administrator privileges.",
                                            "Error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);
                        }
                        break;
                    // Use Default
                    case MessageBoxResult.No:
                        break;
                }
            }
        }


        /// <summary>
        /// Config Open Directory - Label Button
        /// </summary>
        private void lblConfigPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Open Directory
            switch (VM.ConfigureView.ConfigPath_SelectedItem)
            {
                // AppData Local
                case "AppData Local":
                    ConfigDirectoryOpen(appDataLocalDir + @"Axiom UI\");
                    break;

                // AppData Roaming
                case "AppData Roaming":
                    ConfigDirectoryOpen(appDataRoamingDir + @"Axiom UI\");
                    break;

                // Documents
                case "Documents":
                    ConfigDirectoryOpen(documentsDir + @"Axiom UI\");
                    break;

                // App Root
                case "App Root":
                    Process.Start("explorer.exe", appRootDir);
                    break;
            }
        }

        /// <summary>
        /// Config Open Directory - Button
        /// </summary>
        private void btnConfigPath_Click(object sender, RoutedEventArgs e)
        {
            // Open Directory
            switch (VM.ConfigureView.ConfigPath_SelectedItem)
            {
                // AppData Local
                case "AppData Local":
                    ConfigDirectoryOpen(appDataLocalDir + @"Axiom UI\");
                    break;

                // AppData Roaming
                case "AppData Roaming":
                    ConfigDirectoryOpen(appDataRoamingDir + @"Axiom UI\");
                    break;

                // Documents
                case "Documents":
                    ConfigDirectoryOpen(documentsDir + @"Axiom UI\");
                    break;

                // App Root
                case "App Root":
                    Process.Start("explorer.exe", appRootDir);
                    break;
            }
        }


        /// <summary>
        /// Config Directory - ComboBox
        /// </summary>
        public void confMove(string source,
                             string target)
        {
            // Delete any pre-existing file before Move instead of Overwrite
            if (File.Exists(target))
            {
                File.Delete(target);
            }

            File.Move(source, target);
        }

        public void logMove(string source,
                            string target)
        {
            // Delete any pre-existing file before Move instead of Overwrite
            if (File.Exists(target))
            {
                File.Delete(target);
            }

            File.Move(source, target);
        }

        private void cboConfigPath_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (VM.ConfigureView.ConfigPath_SelectedItem)
            {
                // -------------------------
                // AppData Local Directory
                // -------------------------
                case "AppData Local":
                    // Change the conf output folder path
                    Controls.Configure.axiomConfFile = Controls.Configure.confAppDataLocalFilePath;

                    // Change the log output folder path
                    VM.ConfigureView.LogPath_Text = Log.logAppDataLocalDir;

                    try
                    {
                        // -------------------------
                        // Create Axiom UI folder if missing
                        // -------------------------
                        if (!Directory.Exists(Controls.Configure.confAppDataLocalDir))
                        {
                            Directory.CreateDirectory(Controls.Configure.confAppDataLocalDir);
                        }

                        // -------------------------
                        // Move axiom.conf to AppData Local
                        // -------------------------
                        // AppRoot to AppData Local
                        if (File.Exists(Controls.Configure.confAppRootFilePath))
                        {
                            confMove(Controls.Configure.confAppRootFilePath,     // from
                                     Controls.Configure.confAppDataLocalFilePath // to
                                    );
                        }
                        // AppData Roaming to AppData Local
                        else if (File.Exists(Controls.Configure.confAppDataRoamingFilePath))
                        {
                            confMove(Controls.Configure.confAppDataRoamingFilePath, // from
                                     Controls.Configure.confAppDataLocalFilePath    // to
                                    );
                        }

                        // -------------------------
                        // Move axiom.log to AppData Local
                        // -------------------------
                        // AppRoot to AppData Local
                        if (File.Exists(Controls.Configure.confAppRootFilePath))
                        {
                            logMove(Log.logAppRootFilePath,     // from
                                    Log.logAppDataLocalFilePath // to
                                   );
                        }
                        // AppData Roaming to AppData Local
                        else if (File.Exists(Controls.Configure.confAppDataRoamingFilePath))
                        {
                            logMove(Log.logAppDataRoamingFilePath, // from
                                    Log.logAppDataLocalFilePath    // to
                                   );
                        }
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString(),
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                    break;

                // -------------------------
                // AppData Roaming Directory
                // -------------------------
                case "AppData Roaming":
                    // Change the conf output folder path
                    Controls.Configure.axiomConfFile = Controls.Configure.confAppDataRoamingDir;

                    // Change the log output folder path
                    VM.ConfigureView.LogPath_Text = Log.logAppDataRoamingDir;

                    try
                    {
                        // -------------------------
                        // Create Axiom UI folder if missing
                        // -------------------------
                        if (!Directory.Exists(Controls.Configure.confAppDataRoamingDir))
                        {
                            Directory.CreateDirectory(Controls.Configure.confAppDataRoamingDir);
                        }

                        // -------------------------
                        // Move axiom.conf to AppData Roaming
                        // -------------------------
                        // Move from App Root to AppData Roaming
                        if (File.Exists(Controls.Configure.confAppRootFilePath))
                        {
                            confMove(Controls.Configure.confAppRootFilePath,       // from
                                     Controls.Configure.confAppDataRoamingFilePath // to
                                    );
                        }
                        // Move from AppData Local to AppData Roaming
                        else if (File.Exists(Controls.Configure.confAppDataLocalFilePath))
                        {
                            confMove(Controls.Configure.confAppDataLocalFilePath,  // from
                                     Controls.Configure.confAppDataRoamingFilePath // to
                                    );
                        }

                        // -------------------------
                        // Move axiom.log to AppData Roaming
                        // -------------------------
                        // App Root to AppData Roaming
                        if (File.Exists(Controls.Configure.confAppRootFilePath))
                        {
                            logMove(Log.logAppRootFilePath,       // from
                                    Log.logAppDataRoamingFilePath // to
                                   );
                        }
                        // AppData Local to AppData Roaming
                        else if (File.Exists(Controls.Configure.confAppDataLocalFilePath))
                        {
                            logMove(Log.logAppDataLocalFilePath,  // from
                                    Log.logAppDataRoamingFilePath // to
                                   );
                        }
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString(),
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                    break;

                // -------------------------
                // App Directory
                // -------------------------
                case "App Root":
                    // Change the axiom.conf output folder path
                    Controls.Configure.axiomConfFile = Controls.Configure.confAppRootDir;

                    // Change the axiom.log output folder path
                    VM.ConfigureView.LogPath_Text = Log.logAppRootDir;

                    // -------------------------
                    // Ignore Program Files
                    // -------------------------
                    if (!Controls.Configure.confAppRootDir.Contains(programFilesDir) &&
                        !Controls.Configure.confAppRootDir.Contains(programFilesX86Dir) &&
                        !Controls.Configure.confAppRootDir.Contains(programFilesX64Dir)
                        )
                    {
                        try
                        {
                            // -------------------------
                            // Create Axiom UI folder if missing
                            // -------------------------
                            // Always exists
                            //if (!Directory.Exists(Controls.Configure.confAppRootDir))
                            //{
                            //    Directory.CreateDirectory(Controls.Configure.confAppRootDir);
                            //}

                            // -------------------------
                            // Move the axiom.conf to App Root
                            // -------------------------
                            // AppData Local to App Root
                            if (File.Exists(Controls.Configure.confAppDataLocalFilePath))
                            {
                                confMove(Controls.Configure.confAppDataLocalFilePath, // from
                                         Controls.Configure.confAppRootFilePath       // to
                                        );
                            }
                            // AppData Roaming to App Root
                            else if (File.Exists(Controls.Configure.confAppDataRoamingFilePath))
                            {
                                confMove(Controls.Configure.confAppDataRoamingFilePath, // from
                                         Controls.Configure.confAppRootFilePath         // to
                                        );
                            }

                            // -------------------------
                            // Move axiom.log to App Root
                            // -------------------------
                            // AppData Local to App Root
                            if (File.Exists(Controls.Configure.confAppDataLocalFilePath))
                            {
                                logMove(Log.logAppDataLocalFilePath, // from
                                        Log.logAppRootDir            // to
                                       );
                            }
                            // AppData Roaming to App Root
                            else if (File.Exists(Controls.Configure.confAppDataRoamingFilePath))
                            {
                                logMove(Log.logAppDataRoamingFilePath, // from
                                        Log.logAppRootDir              // to
                                       );
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

                    // -------------------------
                    // Program Files Write Warning
                    // -------------------------
                    else
                    {
                        if (File.Exists(Controls.Configure.confAppRootFilePath) ||
                            File.Exists(logAppRootPath))
                        {
                            MessageBox.Show("Cannot save axiom.conf or axiom.log to Program Files, May require Administrator Privileges. \n\nPlease select AppData Local or Roaming instead.",
                                            "Notice",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Warning);
                        }
                    }
                    break;
            }
        }



        /// <summary>
        /// Presets Open Directory - Button
        /// </summary>
        private void lblCustomPresetsPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!Directory.Exists(VM.ConfigureView.CustomPresetsPath_Text))
            {
                // Yes/No Dialog Confirmation
                //
                MessageBoxResult resultExport = MessageBox.Show("Presets folder does not yet exist. Automatically create it?",
                                                                "Directory Not Found",
                                                                MessageBoxButton.YesNo,
                                                                MessageBoxImage.Information);
                switch (resultExport)
                {
                    // Create
                    case MessageBoxResult.Yes:
                        try
                        {
                            Directory.CreateDirectory(VM.ConfigureView.CustomPresetsPath_Text);
                        }
                        catch
                        {
                            MessageBox.Show("Could not create Presets folder. May require Administrator privileges.",
                                            "Error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);
                        }
                        break;
                    // Use Default
                    case MessageBoxResult.No:
                        break;
                }
            }

            // Open Directory
            if (IsValidPath(VM.ConfigureView.CustomPresetsPath_Text))
            {
                if (Directory.Exists(VM.ConfigureView.CustomPresetsPath_Text))
                {
                    Process.Start("explorer.exe", VM.ConfigureView.CustomPresetsPath_Text);
                }
            }
        }

        /// <summary>
        /// Custom Presets Path - Textbox
        /// </summary>
        private void tbxCustomPresetsPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Controls.Configure.CustomPresetsFolderBrowser();
        }

        // Drag and Drop
        private void tbxCustomPresetsPath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxCustomPresetsPath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];

            // If Path has file, extract Directory only
            if (Path.HasExtension(buffer.First()))
            {
                VM.ConfigureView.CustomPresetsPath_Text = Path.GetDirectoryName(buffer.First()).TrimEnd('\\') + @"\";
            }

            // Use Folder Path
            else
            {
                VM.ConfigureView.CustomPresetsPath_Text = buffer.First();
            }

            // -------------------------
            // Load Custom Presets
            // Refresh Presets ComboBox
            // -------------------------
            Profiles.Profiles.LoadCustomPresets();
        }

        /// <summary>
        /// CustomPresets Auto Path - Label Button
        /// </summary>
        private void btnCustomPresetsAuto_Click(object sender, RoutedEventArgs e)
        {
            CustomPresetsAuto();
        }
        public static void CustomPresetsAuto()
        {
            // Original Custom Presets Directory
            string oldPresetsDir = VM.ConfigureView.CustomPresetsPath_Text;

            // -------------------------
            // Display Folder Path in Textbox
            // -------------------------
            switch (VM.ConfigureView.ConfigPath_SelectedItem)
            {
                // AppData Local
                case "AppData Local":
                    VM.ConfigureView.CustomPresetsPath_Text = appDataLocalDir + @"Axiom UI\presets\";
                    break;

                // AppData Roaming
                case "AppData Roaming":
                    VM.ConfigureView.CustomPresetsPath_Text = appDataRoamingDir + @"Axiom UI\presets\";
                    break;

                // Documents
                case "Documents":
                    VM.ConfigureView.CustomPresetsPath_Text = documentsDir + @"Axiom UI\presets\";
                    break;

                // App Root
                case "App Root":
                    if (appRootDir.Contains(programFilesDir) &&
                        appRootDir.Contains(programFilesX86Dir) &&
                        appRootDir.Contains(programFilesX64Dir)
                        )
                    {
                        // Change Program Files to AppData Local
                        VM.ConfigureView.CustomPresetsPath_Text = appDataLocalDir + @"Axiom UI\presets\";
                    }
                    else
                    {
                        VM.ConfigureView.CustomPresetsPath_Text = appRootDir + @"presets\";
                    }
                    break;
            }

            // -------------------------
            // Move Custom Presets to new location
            // -------------------------
            Controls.Configure.MoveCustomPresets(oldPresetsDir);

            // -------------------------
            // Load Custom Presets
            // Refresh Presets ComboBox
            // -------------------------
            Profiles.Profiles.LoadCustomPresets();
        }


        /// <summary>
        /// FFmpeg Open Directory - Label Button
        /// </summary>
        private void lblFFmpegPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            string ffmpegPath = string.Empty;

            // If Configure FFmpeg Path is <auto>
            if (VM.ConfigureView.FFmpegPath_Text == "<auto>")
            {
                // Included Binary
                if (File.Exists(appRootDir + @"ffmpeg\bin\ffmpeg.exe"))
                {
                    ffmpegPath = appRootDir + @"ffmpeg\bin\";
                }
                // Using Enviornment Variable
                else
                {
                    var envar = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);

                    IEnumerable<string> files = new List<string>();
                    string exePath = string.Empty;

                    // Get Environment Variable Paths
                    foreach (var path in envar.Split(';'))
                    {
                        if (IsValidPath(path))
                        {
                            if (Directory.Exists(path))
                            {
                                // Get all files in Path
                                files = Directory.GetFiles(path, "ffmpeg.exe")
                                                 .Select(Path.GetFullPath)
                                                 .Where(s => !string.IsNullOrWhiteSpace(s))
                                                 .ToList();

                                // Find ffmpeg.exe in files list
                                if (files != null && files.Count() > 0)
                                {
                                    foreach (string file in files)
                                    {
                                        if (file.Contains("ffmpeg.exe"))
                                        {
                                            exePath = file;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    ffmpegPath = Path.GetDirectoryName(exePath).TrimEnd('\\') + @"\";
                    //MessageBox.Show(exePath); //debug
                }
            }

            // Use User Custom Path
            else
            {
                ffmpegPath = Path.GetDirectoryName(VM.ConfigureView.FFmpegPath_Text).TrimEnd('\\') + @"\";
            }


            // Open Directory
            if (IsValidPath(ffmpegPath))
            {
                if (Directory.Exists(ffmpegPath))
                {
                    Process.Start("explorer.exe", ffmpegPath);
                }
            }
        }

        /// <summary>
        /// FFmpeg Path - Textbox
        /// </summary>
        private void tbxFFmpegPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Controls.Configure.FFmpegFolderBrowser();
        }

        // Drag and Drop
        private void tbxFFmpegPath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxFFmpegPath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            VM.ConfigureView.FFmpegPath_Text = buffer.First();
        }

        /// <summary>
        /// FFmpeg Auto Path - Button
        /// </summary>
        private void btnFFmpegAuto_Click(object sender, RoutedEventArgs e)
        {
            // Display Folder Path in Textbox
            VM.ConfigureView.FFmpegPath_Text = "<auto>";
        }


        /// <summary>
        /// FFprobe Open Directory - Label Button
        /// </summary>
        private void lblFFprobePath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            string ffprobePath = string.Empty;

            // If Configure FFprobe Path is <auto>
            if (VM.ConfigureView.FFprobePath_Text == "<auto>")
            {
                // Included Binary
                if (File.Exists(appRootDir + @"ffmpeg\bin\ffprobe.exe"))
                {
                    ffprobePath = appRootDir + @"ffmpeg\bin\";
                }
                // Using Enviornment Variable
                else
                {
                    var envar = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);

                    IEnumerable<string> files = new List<string>();
                    string exePath = string.Empty;

                    // Get Environment Variable Paths
                    foreach (var path in envar.Split(';'))
                    {
                        if (IsValidPath(path))
                        {
                            if (Directory.Exists(path))
                            {
                                // Get all files in Path
                                files = Directory.GetFiles(path, "ffprobe.exe")
                                                 .Select(Path.GetFullPath)
                                                 .Where(s => !string.IsNullOrWhiteSpace(s))
                                                 .ToList();

                                // Find ffprobe.exe in files list
                                if (files != null && files.Count() > 0)
                                {
                                    foreach (string file in files)
                                    {
                                        if (file.Contains("ffprobe.exe"))
                                        {
                                            exePath = file;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    ffprobePath = Path.GetDirectoryName(exePath).TrimEnd('\\') + @"\";
                }
            }

            // Use User Custom Path
            else
            {
                ffprobePath = Path.GetDirectoryName(VM.ConfigureView.FFprobePath_Text).TrimEnd('\\') + @"\";
            }


            // Open Directory
            if (IsValidPath(ffprobePath))
            {
                if (Directory.Exists(ffprobePath))
                {
                    Process.Start("explorer.exe", ffprobePath);
                }
            }
        }

        /// <summary>
        /// FFprobe Path - Textbox
        /// </summary>
        private void tbxFFprobePath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Controls.Configure.FFprobeFolderBrowser();
        }

        // Drag and Drop
        private void tbxFFprobePath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxFFprobePath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            VM.ConfigureView.FFprobePath_Text = buffer.First();
        }

        /// <summary>
        /// FFprobe Auto Path - Button
        /// </summary>
        private void btnFFprobeAuto_Click(object sender, RoutedEventArgs e)
        {
            // Display Folder Path in Textbox
            VM.ConfigureView.FFprobePath_Text = "<auto>";
        }


        /// <summary>
        /// FFplay Open Directory - Label Button
        /// </summary>
        private void lblFFplayPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            string ffplayPath = string.Empty;

            // If Configure FFplay Path is <auto>
            if (VM.ConfigureView.FFplayPath_Text == "<auto>")
            {
                // Included Binary
                if (File.Exists(appRootDir + @"ffmpeg\bin\ffplay.exe"))
                {
                    ffplayPath = appRootDir + @"ffmpeg\bin\";
                }
                // Using Enviornment Variable
                else
                {
                    var envar = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);

                    IEnumerable<string> files = new List<string>();
                    string exePath = string.Empty;

                    // Get Environment Variable Paths
                    foreach (var path in envar.Split(';'))
                    {
                        if (IsValidPath(path))
                        {
                            if (Directory.Exists(path))
                            {
                                // Get all files in Path
                                files = Directory.GetFiles(path, "ffplay.exe")
                                                 .Select(Path.GetFullPath)
                                                 .Where(s => !string.IsNullOrWhiteSpace(s))
                                                 .ToList();

                                // Find ffplay.exe in files list
                                if (files != null && files.Count() > 0)
                                {
                                    foreach (string file in files)
                                    {
                                        if (file.Contains("ffplay.exe"))
                                        {
                                            exePath = file;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    ffplayPath = Path.GetDirectoryName(exePath).TrimEnd('\\') + @"\";
                }
            }

            // Use User Custom Path
            else
            {
                ffplayPath = Path.GetDirectoryName(VM.ConfigureView.FFplayPath_Text).TrimEnd('\\') + @"\";
            }


            // Open Directory
            if (IsValidPath(ffplayPath))
            {
                if (Directory.Exists(ffplayPath))
                {
                    Process.Start("explorer.exe", ffplayPath);
                }
            }
        }

        /// <summary>
        /// FFplay Path - Textbox
        /// </summary>
        private void tbxFFplayPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Controls.Configure.FFplayFolderBrowser();
        }

        // Drag and Drop
        private void tbxFFplayPath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxFFplayPath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            VM.ConfigureView.FFplayPath_Text = buffer.First();
        }

        /// <summary>
        /// FFplay Auto Path - Button
        /// </summary>
        private void btnFFplayAuto_Click(object sender, RoutedEventArgs e)
        {
            // Display Folder Path in Textbox
            VM.ConfigureView.FFplayPath_Text = "<auto>";
        }


        /// <summary>
        /// youtube-dl Open Directory - Label Button
        /// </summary>
        private void lblyoutubedlPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            string youtubedlPath = string.Empty;

            // If Configure youtube-dl Path is <auto>
            if (VM.ConfigureView.youtubedlPath_Text == "<auto>")
            {
                // Included Binary
                if (File.Exists(appRootDir + @"youtube-dl\youtube-dl.exe"))
                {
                    youtubedlPath = appRootDir + @"youtube-dl\";
                }
                // Using Enviornment Variable
                else
                {
                    var envar = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);

                    IEnumerable<string> files = new List<string>();
                    string exePath = string.Empty;

                    // Get Environment Variable Paths
                    foreach (var path in envar.Split(';'))
                    {
                        if (IsValidPath(path))
                        {
                            if (Directory.Exists(path))
                            {
                                // Get all files in Path
                                files = Directory.GetFiles(path, "youtube-dl.exe")
                                                 .Select(Path.GetFullPath)
                                                 .Where(s => !string.IsNullOrWhiteSpace(s))
                                                 .ToList();

                                // Find youtube-dl.exe in files list
                                if (files != null && files.Count() > 0)
                                {
                                    foreach (string file in files)
                                    {
                                        if (file.Contains("youtube-dl.exe"))
                                        {
                                            exePath = file;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    youtubedlPath = Path.GetDirectoryName(exePath).TrimEnd('\\') + @"\";
                }
            }

            // Use User Custom Path
            else
            {
                youtubedlPath = Path.GetDirectoryName(VM.ConfigureView.youtubedlPath_Text).TrimEnd('\\') + @"\";
            }


            // Open Directory
            if (IsValidPath(youtubedlPath))
            {
                if (Directory.Exists(youtubedlPath))
                {
                    Process.Start("explorer.exe", youtubedlPath);
                }
            }
        }

        /// <summary>
        /// youtubedl Path - Textbox
        /// </summary>
        private void tbxyoutubedlPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Controls.Configure.youtubedlFolderBrowser();
        }

        // Drag and Drop
        private void tbxyoutubedlPath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxyoutubedlPath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            VM.ConfigureView.youtubedlPath_Text = buffer.First();
        }

        /// <summary>
        /// youtubedl Auto Path - Button
        /// </summary>
        private void btnyoutubedlAuto_Click(object sender, RoutedEventArgs e)
        {
            // Display Folder Path in Textbox
            VM.ConfigureView.youtubedlPath_Text = "<auto>";
        }


        /// <summary>
        /// Log Open Directory - Label Button
        /// </summary>
        private void lblLogPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsValidPath(VM.ConfigureView.LogPath_Text))
            {
                if (Directory.Exists(VM.ConfigureView.LogPath_Text))
                {
                    Process.Start("explorer.exe", VM.ConfigureView.LogPath_Text);
                }
            }
        }

        // Drag and Drop
        private void tbxLogPath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.Copy;
        }
        private void tbxLogPath_PreviewDrop(object sender, DragEventArgs e)
        {
            var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];

            // If Path has file, extract Directory only
            if (Path.HasExtension(buffer.First()))
            {
                VM.ConfigureView.LogPath_Text = Path.GetDirectoryName(buffer.First()).TrimEnd('\\') + @"\";
            }

            // Use Folder Path
            else
            {
                VM.ConfigureView.LogPath_Text = buffer.First();
            }
        }

        /// <summary>
        /// Log Checkbox - Checked
        /// </summary>
        private void cbxLog_Checked(object sender, RoutedEventArgs e)
        {
            VM.ConfigureView.LogPath_IsEnabled = true;
        }

        /// <summary>
        /// Log Checkbox - Unchecked
        /// </summary>
        private void cbxLog_Unchecked(object sender, RoutedEventArgs e)
        {
            VM.ConfigureView.LogPath_IsEnabled = false;
        }

        /// <summary>
        /// Log Path - Textbox
        /// </summary>
        private void tbxLogPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Controls.Configure.LogFolderBrowser();
        }

        /// <summary>
        /// Log Auto Path - Button
        /// </summary>
        private void btnLogPathAuto_Click(object sender, RoutedEventArgs e)
        {
            // Uncheck Log Checkbox
            VM.ConfigureView.LogCheckBox_IsChecked = false;

            // Clear Path in Textbox
            VM.ConfigureView.LogPath_Text = Log.axiomLogDir;
        }

        /// <summary>
        /// Input Filename Tokens - ComboBox
        /// </summary>
        private void cboInputFileNameTokens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (VM.ConfigureView.InputFileNameTokens_SelectedItem)
            {
                case "Keep":
                    VM.ConfigureView.InputFileNameTokensCustom_IsEnabled = false;
                    break;

                case "Remove":
                    VM.ConfigureView.InputFileNameTokensCustom_IsEnabled = true;
                    break;
            }
        }

        /// <summary>
        /// Shell - ComboBox
        /// </summary>
        private void cboShell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Re-Set YouTube-DL File Name in the Output TextBox
            // -------------------------
            // Without it, on cboShell_SelectionChanged,
            // in Generate.FFmpeg.YouTubeDL.Generate_FFmpegArgs(),
            // the outputFileName gets stuck with the old value
            // because it reads it from VM.MainView.Output_Text
            if (!string.IsNullOrWhiteSpace(VM.MainView.Output_Text))
            {
                switch (VM.ConfigureView.Shell_SelectedItem)
                {
                    // CMD
                    case "CMD":
                        VM.MainView.Output_Text = VM.MainView.Output_Text.Replace("$name", "%f"); // eg. C:\Output Folder\$f.mp4
                        break;

                    // PowerShell
                    case "PowerShell":
                        VM.MainView.Output_Text = VM.MainView.Output_Text.Replace("%f", "$name"); // eg. C:\Output Folder\$name.mp4
                        break;
                }
            }
        }

        /// <summary>
        /// Shell
        /// </summary>
        private void btnShellDefault_Click(object sender, RoutedEventArgs e)
        {
            VM.ConfigureView.Shell_SelectedItem = "CMD";
        }

        /// <summary>
        /// Shell Title - ComboBox
        /// </summary>
        private void cboShellTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Shell Title Default
        /// </summary>
        private void btnShellTitleDefault_Click(object sender, RoutedEventArgs e)
        {
            VM.ConfigureView.ShellTitle_SelectedItem = "Disabled";
        }

        /// <summary>
        /// Process Priority Default
        /// </summary>
        private void btnProcessPriorityDefault_Click(object sender, RoutedEventArgs e)
        {
            VM.ConfigureView.ProcessPriority_SelectedItem = "Default";
        }

        /// <summary>
        /// Threads - ComboBox
        /// </summary>
        private void threadSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Set the threads to pass to MainWindow
            Controls.Configure.threads = VM.ConfigureView.Threads_SelectedItem;
        }

        // Key Down
        private void threadSelect_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers and Backspace
            Allow_Only_Number_Keys(e);
        }

        /// <summary>
        /// Threads Default
        /// </summary>
        private void btnThreadsDefault_Click(object sender, RoutedEventArgs e)
        {
            VM.ConfigureView.Threads_SelectedItem = "Optimal";
        }

        /// <summary>
        /// Output Naming ListView
        /// </summary>
        private void lstvOutputNaming_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If ListView Selected Items Contains Any Items
            // Clear before adding new Selected Items
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Any())
            {
                VM.ConfigureView.OutputNaming_ListView_SelectedItems.Clear();
                VM.ConfigureView.OutputNaming_ListView_SelectedItems.TrimExcess();
            }

            // Create Selected Items List for ViewModel
            //VM.ConfigureView.OutputNaming_ListView_SelectedItems = lstvOutputNaming.SelectedItems
            //                                                                       .Cast<string>()
            //                                                                       .ToList();

            // Remove ListView Items Duplicates
            VM.ConfigureView.OutputNaming_ListView_Items = new ObservableCollection<string>(VM.ConfigureView.OutputNaming_ListView_Items
                                                                                                            .Distinct()
                                                                                                            .ToList()
                                                                                                            .AsEnumerable()
                                                                                                            );
            //VM.ConfigureView.OutputNaming_ListView_Items = VM.ConfigureView.OutputNaming_ListView_Items.Distinct().ToList();

            // Build the list by Order Arranged
            for (var i = 0; i < VM.ConfigureView.OutputNaming_ListView_Items.Count; i++)
            {
                if (lstvOutputNaming.SelectedItems
                                    .Cast<string>()
                                    .ToList()
                                    .Contains(VM.ConfigureView.OutputNaming_ListView_Items[i]))
                {
                    VM.ConfigureView.OutputNaming_ListView_SelectedItems.Add(VM.ConfigureView.OutputNaming_ListView_Items[i]);
                }
            }

            // Remove ListView Selected Items Duplicates
            VM.ConfigureView.OutputNaming_ListView_SelectedItems = VM.ConfigureView.OutputNaming_ListView_SelectedItems
                                                                                   .Distinct()
                                                                                   .ToList();
            //MessageBox.Show(string.Join("\n", lstvOutputNaming.SelectedItems.Cast<string>().ToList())); //debug

            // -------------------------
            // Update Ouput Textbox with Name Settings
            // -------------------------
            //OutputPath_UpdateDisplay();
        }

        /// <summary>
        /// Output Naming Sort Up
        /// </summary>
        private void btnOutputNaming_SortUp_Click(object sender, RoutedEventArgs e)
        {
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Count > 0)
            {
                var selectedIndex = VM.ConfigureView.OutputNaming_ListView_SelectedIndex;

                if (selectedIndex > 0)
                {
                    // ListView Items
                    var itemlsvItems = VM.ConfigureView.OutputNaming_ListView_Items[selectedIndex];
                    VM.ConfigureView.OutputNaming_ListView_Items.RemoveAt(selectedIndex);
                    VM.ConfigureView.OutputNaming_ListView_Items.Insert(selectedIndex - 1, itemlsvItems);

                    // Highlight Selected Index
                    VM.ConfigureView.OutputNaming_ListView_SelectedIndex = selectedIndex - 1;
                }
            }
        }

        /// <summary>
        /// Output Naming Sort Down
        /// </summary>
        private void btnOutputNaming_SortDown_Click(object sender, RoutedEventArgs e)
        {
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Count > 0)
            {
                var selectedIndex = VM.ConfigureView.OutputNaming_ListView_SelectedIndex;

                if (selectedIndex + 1 < VM.ConfigureView.OutputNaming_ListView_Items.Count)
                {
                    // ListView Items
                    var itemlsvItems = VM.ConfigureView.OutputNaming_ListView_Items[selectedIndex];
                    VM.ConfigureView.OutputNaming_ListView_Items.RemoveAt(selectedIndex);
                    VM.ConfigureView.OutputNaming_ListView_Items.Insert(selectedIndex + 1, itemlsvItems);

                    // Highlight Selected Index
                    VM.ConfigureView.OutputNaming_ListView_SelectedIndex = selectedIndex + 1;
                }
            }
        }

        /// <summary>
        /// Output Naming Select All
        /// </summary>
        private void btnOutputNaming_SelectAll_Click(object sender, RoutedEventArgs e)
        {
            lstvOutputNaming.SelectAll();
        }

        /// <summary>
        /// Output Naming Deselect All
        /// </summary>
        private void btnOutputNaming_DeselectAll_Click(object sender, RoutedEventArgs e)
        {
            lstvOutputNaming.SelectedIndex = -1;
        }

        /// <summary>
        /// Output Naming Load Defaults
        /// </summary>
        private void btnOutputNamingDefaults_Click(object sender, RoutedEventArgs e)
        {
            // Deselect All
            lstvOutputNaming.SelectedIndex = -1;

            OutputNamignDefaults();
        }
        public static void OutputNamignDefaults()
        {
            // Clear Selected Items
            if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Any())
            {
                VM.ConfigureView.OutputNaming_ListView_SelectedItems.Clear();
                VM.ConfigureView.OutputNaming_ListView_SelectedItems.TrimExcess();
            }

            // Load Defaults
            VM.ConfigureView.OutputNaming_ListView_Items = ViewModel.Configure.OutputNaming_LoadDefaults();
        }


        /// <summary>
        /// Thread Detect
        /// </summary>
        public static String ThreadDetect()
        {
            // Default / Off
            if (VM.ConfigureView.Threads_SelectedItem == "Default")
            {
                // Optimal
                Controls.Configure.threads = "";

                return Controls.Configure.threads;
            }

            // Fallback if maxthreads was not detected in SystemInfo()
            if (string.IsNullOrWhiteSpace(Controls.Configure.threads))
            {
                // Optimal
                Controls.Configure.threads = "-threads 0";

                return Controls.Configure.threads;
            }

            // Options
            switch (VM.ConfigureView.Threads_SelectedItem)
            {
                // Empty
                case "":
                    Controls.Configure.threads = string.Empty;
                    break;

                // Default / Off
                //case "Default":
                //    Configure.threads = string.Empty;
                //    break;

                // Optimal
                case "Optimal":
                    Controls.Configure.threads = "-threads 0";
                    break;

                // All
                case "All":
                    // e.g. -threads 8
                    Controls.Configure.threads = "-threads " + Controls.Configure.maxthreads;
                    break;

                // Selected Number
                default:
                    // e.g. -threads 5
                    Controls.Configure.threads = "-threads " + VM.ConfigureView.Threads_SelectedItem;
                    break;
            }

            return Controls.Configure.threads;
        }


        /// <summary>
        /// Input FileName Tokens Default
        /// </summary>
        private void btnInputFileNameTokensDefault_Click(object sender, RoutedEventArgs e)
        {
            VM.ConfigureView.InputFileNameTokens_SelectedItem = "Keep";
        }


        /// <summary>
        /// Output Overwrite Default
        /// </summary>
        private void btnOutputOverwriteDefault_Click(object sender, RoutedEventArgs e)
        {
            VM.ConfigureView.OutputOverwrite_SelectedItem = "Always";
        }

        /// <summary>
        /// Output FileName Spacing
        /// </summary>
        private void btnOutputFileNameSpacingDefault_Click(object sender, RoutedEventArgs e)
        {
            VM.ConfigureView.OutputFileNameSpacing_SelectedItem = "Original";
        }


        /// <summary>
        /// Theme Select - ComboBox
        /// </summary>
        private void themeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Controls.Configure.theme = VM.ConfigureView.Theme_SelectedItem;

            // Change Theme Resource
            App.Current.Resources.MergedDictionaries.Clear();
            App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("Themes/" + "Theme" + Controls.Configure.theme + ".xaml", UriKind.RelativeOrAbsolute)
            });
        }


        /// <summary>
        /// Updates Auto Check - Checked
        /// </summary>
        private void tglUpdateAutoCheck_Checked(object sender, RoutedEventArgs e)
        {
            // Update Toggle Text
            VM.ConfigureView.UpdateAutoCheck_Text = "On";
        }
        /// <summary>
        /// Updates Auto Check - Unchecked
        /// </summary>
        private void tglUpdateAutoCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            // Update Toggle Text
            VM.ConfigureView.UpdateAutoCheck_Text = "Off";
        }


        /// <summary>
        /// Settings Default
        /// </summary>
        /// <remarks>
        /// Set all settings to their defaults.
        /// </remarks>
        private void btnSettingsDefault_Click(object sender, RoutedEventArgs e)
        {
            // Config
            CustomPresetsAuto();
            VM.ConfigureView.FFmpegPath_Text = "<auto>";
            VM.ConfigureView.FFprobePath_Text = "<auto>";
            VM.ConfigureView.FFplayPath_Text = "<auto>";
            VM.ConfigureView.youtubedlPath_Text = "<auto>";
            VM.ConfigureView.LogCheckBox_IsChecked = false;
            VM.ConfigureView.LogPath_Text = Log.axiomLogDir;

            // Process
            VM.ConfigureView.Shell_SelectedItem = "CMD";
            VM.ConfigureView.ShellTitle_SelectedItem = "Disabled";
            VM.ConfigureView.ProcessPriority_SelectedItem = "Default";
            VM.ConfigureView.Threads_SelectedItem = "Optimal";

            // Input
            VM.ConfigureView.InputFileNameTokens_SelectedItem = "Keep";

            // Output
            VM.ConfigureView.OutputOverwrite_SelectedItem = "Always";
            VM.ConfigureView.OutputFileNameSpacing_SelectedItem = "Original";
            lstvOutputNaming.SelectedIndex = -1;
            OutputNamignDefaults();
        }

        /// <summary>
        /// Reset Settings Button
        /// </summary>
        private void btnResetConfig_Click(object sender, RoutedEventArgs e)
        {
            // Check if Directory Exists
            if (File.Exists(Controls.Configure.axiomConfFile))
            {
                // Show Yes No Window
                System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(
                                                                "Delete " + Controls.Configure.axiomConfFile,
                                                                "Delete Directory Confirm",
                                                                System.Windows.Forms.MessageBoxButtons.YesNo);

                // Yes
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        if (File.Exists(Controls.Configure.axiomConfFile))
                        {
                            File.Delete(Controls.Configure.axiomConfFile);
                        }
                    }
                    catch
                    {

                    }

                    // Load Defaults
                    VM.ConfigureView.LoadConfigDefaults();
                    VM.MainView.LoadControlsDefaults();
                    VM.FormatView.LoadControlsDefaults();
                    VM.VideoView.LoadControlsDefaults();
                    VM.SubtitleView.LoadControlsDefaults();
                    VM.AudioView.LoadControlsDefaults();

                    // Restart Program
                    Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
                // No
                else if (dialogResult == System.Windows.Forms.DialogResult.No)
                {
                    //do nothing
                }
            }

            // If Axiom Folder Not Found
            else
            {
                MessageBox.Show("Config file " + Controls.Configure.axiomConfFile + " not Found.",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }
    }
}
