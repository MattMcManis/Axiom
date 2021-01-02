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
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using ViewModel;
using Axiom;
using System.Collections.ObjectModel;
// Disable XML Comment warnings
#pragma warning disable 1591

namespace Controls
{
    public class Configure
    {
        private MainWindow mainwindow = (MainWindow)Application.Current.MainWindow;

        // --------------------------------------------------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------------------------------------------------
        public static string theme; // Theme
        public static string threads; // FFmpeg -threads
        public static string maxthreads; // All available CPU threads

        // axiom.conf Directories
        public readonly static string confAppRootDir = MainWindow.appRootDir;
        public readonly static string confAppDataLocalDir = MainWindow.appDataLocalDir + @"Axiom UI\";
        public readonly static string confAppDataRoamingDir = MainWindow.appDataRoamingDir + @"Axiom UI\";

        // axoim.conf Full File Paths
        public readonly static string confAppRootFilePath = Path.Combine(confAppRootDir, "axiom.conf");
        public readonly static string confAppDataLocalFilePath = Path.Combine(confAppDataLocalDir, "axiom.conf");
        public readonly static string confAppDataRoamingFilePath = Path.Combine(confAppDataRoamingDir, "axiom.conf");
        public static string axiomConfFile { get; set; } // Global directory+filename

        /// <summary>
        /// Config File Reader
        /// </summary>
        /// License MIT
        // https://code.msdn.microsoft.com/windowsdesktop/Reading-and-Writing-Values-85084b6a
        public partial class ConfigFile
        {
            public static ConfigFile cfg;
            public static ConfigFile conf;

            public string path { get; private set; }


            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            private static extern int GetPrivateProfileString(string section, string key,
            string defaultValue, StringBuilder value, int size, string filePath);

            [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
            static extern int GetPrivateProfileString(string section, string key, string defaultValue,
                [In, Out] char[] value, int size, string filePath);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            private static extern int GetPrivateProfileSection(string section, IntPtr keyValue,
            int size, string filePath);

            [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool WritePrivateProfileString(string section, string key,
                string value, string filePath);

            public static int capacity = 512;

            public ConfigFile(string configPath)
            {
                path = configPath;
            }

            public bool Write(string section, string key, string value)
            {
                bool result = WritePrivateProfileString(section, key, value, path);
                return result;
            }

            public string Read(string section, string key)
            {
                var value = new StringBuilder(capacity);
                GetPrivateProfileString(section, key, string.Empty, value, value.Capacity, path);
                return value.ToString();
            }
        }


        /// <summary>
        /// Read axiom.conf file
        /// </summary>
        public static void ReadAxiomConf(string directory,
                                         string filename,
                                         List<Action> actionsToRead)
        {
            // Failed Imports
            List<string> listFailedImports = new List<string>();

            string axiomConfFile = Path.Combine(directory, filename);

            try
            {
                // Check if axiom.conf file exists in %AppData%\Utara UI\
                if (File.Exists(axiomConfFile))
                {
                    ConfigFile.conf = new ConfigFile(axiomConfFile);

                    //MessageBox.Show(Configure.axiomConfFile); //debug

                    // Write each action in the list
                    foreach (Action Read in actionsToRead)
                    {
                        Read();
                    }
                }
            }

            // Error Loading axiom.conf
            //
            catch (Exception ex)
            {
                // Check if axiom.conf has a valid path
                if (MainWindow.IsValidPath(axiomConfFile) == false)
                {
                    return;
                }

                // Delete axiom.conf and Restart
                // Check if axiom.conf Exists
                if (File.Exists(axiomConfFile))
                {
                    // Yes/No Dialog Confirmation
                    //
                    MessageBoxResult result = MessageBox.Show(
                        "Could not load axiom.conf. \n\nDelete config and reload defaults?\r\n\r\n" + ex.ToString(),
                        "Error",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error);
                    switch (result)
                    {
                        // Create
                        case MessageBoxResult.Yes:
                            File.Delete(axiomConfFile);

                            // Reload Control Defaults
                            VM.ConfigureView.LoadConfigDefaults();
                            VM.MainView.LoadControlsDefaults();
                            VM.FormatView.LoadControlsDefaults();
                            VM.VideoView.LoadControlsDefaults();
                            VM.SubtitleView.LoadControlsDefaults();
                            VM.AudioView.LoadControlsDefaults();

                            // Restart Program
                            Process.Start(Application.ResourceAssembly.Location);
                            Application.Current.Shutdown();
                            break;

                        // Use Default
                        case MessageBoxResult.No:
                            // Force Shutdown
                            System.Windows.Forms.Application.ExitThread();
                            Application.Current.Shutdown();
                            return;
                    }
                }
                // If axiom.conf Not Found
                else
                {
                    MessageBox.Show("No previous axiom.conf file found.",
                                    "Notice",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);

                    return;
                }
            }
        }


        /// <summary>
        /// Write axiom.conf (Method)
        /// </summary>
        public static void WriteAxiomConf(string directory,
                                          string filename,
                                          List<Action> actionsToWrite)
        {
            // -------------------------
            // Check if Directory Exists
            // -------------------------
            if (!Directory.Exists(directory))
            {
                try
                {
                    // Create Config Directory
                    Directory.CreateDirectory(directory);
                }
                catch
                {
                    MessageBox.Show("Could not create config folder " + directory + ".\n\nMay require Administrator privileges.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }

            // -------------------------
            // Save axiom.conf file if directory exists
            // -------------------------
            if (Directory.Exists(directory))
            {
                // Access
                if (MainWindow.hasWriteAccessToFolder(directory) == true)
                {
                    //ConfigFile conf = null;

                    try
                    {
                        // Start axiom.conf File Write
                        //MessageBox.Show(Path.Combine(directory, filename)); //debug
                        ConfigFile.conf = new ConfigFile(Path.Combine(directory, filename));

                        // Write each action in the list
                        foreach (Action Write in actionsToWrite)
                        {
                            Write();
                        }
                    }
                    // Error
                    catch
                    {
                        MessageBox.Show("Could not save " + filename + " to " + directory,
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                }
                // Denied
                else
                {
                    MessageBox.Show("User does not have write access to " + directory,
                                    "Notice",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                }

            }
        }


        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Control Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Custom Presets Folder Browser Dialog
        /// </summary>
        public static void CustomPresetsFolderBrowser()
        {
            // Original Custom Presets Directory
            string oldPresetsDir = VM.ConfigureView.CustomPresetsPath_Text;

            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                VM.ConfigureView.CustomPresetsPath_Text = folderBrowserDialog.SelectedPath;

                // Add backslash if missing
                VM.ConfigureView.CustomPresetsPath_Text = VM.ConfigureView.CustomPresetsPath_Text.TrimEnd('\\') + @"\";

                // -------------------------
                // Move Custom Presets to new location
                // -------------------------
                MoveCustomPresets(oldPresetsDir);

                // -------------------------
                // Load Custom Presets
                // Refresh Presets ComboBox
                // -------------------------
                Profiles.Profiles.LoadCustomPresets();
            }
        }

        /// <summary>
        /// Move Custom Presets
        /// </summary>
        public static void MoveCustomPresets(string oldPresetsDir)
        {
            if (!string.Equals(oldPresetsDir, // If source and target directory are not the same
                               VM.ConfigureView.CustomPresetsPath_Text, 
                               StringComparison.OrdinalIgnoreCase)) 
            {
                // Yes/No Dialog Confirmation
                //
                MessageBoxResult msb = MessageBox.Show(
                    "Would you like to move you existing presets to the new location now?" +
                    "\r\n\r\n" +
                    oldPresetsDir +
                    "\r\n\r\nto\r\n\r\n" +
                    VM.ConfigureView.CustomPresetsPath_Text,
                    "Move Presets",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);
                switch (msb)
                {
                    // Move presets directory
                    case MessageBoxResult.Yes:
                        try
                        {
                            //MessageBox.Show(oldPresetsDir); //debug
                            if (Directory.Exists(oldPresetsDir))
                            {
                                //MessageBox.Show("pass"); //debug
                                Directory.CreateDirectory(VM.ConfigureView.CustomPresetsPath_Text);

                                //MainWindow.MoveDirectory(oldPresetsDir, VM.ConfigureView.CustomPresetsPath_Text);

                                var presets = Directory.EnumerateFiles(oldPresetsDir, "*.ini");
                                foreach (var file in presets)
                                {
                                    // MessageBox.Show(file.ToString() + "\r\n" + VM.ConfigureView.CustomPresetsPath_Text.TrimEnd('\\') + @"\" + Path.GetFileName(file)); //debug
                                    File.Move(file, VM.ConfigureView.CustomPresetsPath_Text.TrimEnd('\\') + @"\" + Path.GetFileName(file));
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
                        break;

                    // Do not move
                    case MessageBoxResult.No:
                        return;
                }
            }
        }

        /// <summary>
        /// FFmpeg Folder Browser Dialog
        /// </summary>
        public static void FFmpegFolderBrowser()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                //InitialDirectory = @"C:\",
                Title = "Browse for ffmpeg.exe",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "exe",
                Filter = "Executable (*.exe)|*.exe",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    VM.ConfigureView.FFmpegPath_Text = openFileDialog.FileName;
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString(),
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }

            //var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            //System.Windows.Forms.DialogResult result = OpenFileDialog.ShowDialog();

            //// Popup Folder Browse Window
            //if (result == System.Windows.Forms.DialogResult.OK)
            //{
            //    // Display Folder Path in Textbox
            //    VM.ConfigureView.FFmpegPath_Text = OpenFileDialog.FileName;
            //}
        }

        /// <summary>
        /// FFprobe Folder Browser Dialog
        /// </summary>
        public static void FFprobeFolderBrowser()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                //InitialDirectory = @"C:\",
                Title = "Browse for ffprobe.exe",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "exe",
                Filter = "Executable (*.exe)|*.exe",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    VM.ConfigureView.FFprobePath_Text = openFileDialog.FileName;
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString(),
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }

            //var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            //System.Windows.Forms.DialogResult result = OpenFileDialog.ShowDialog();

            //// Popup Folder Browse Window
            //if (result == System.Windows.Forms.DialogResult.OK)
            //{
            //    // Display Folder Path in Textbox
            //    VM.ConfigureView.FFprobePath_Text = OpenFileDialog.FileName;
            //}
        }

        /// <summary>
        /// FFplay Folder Browser Dialog
        /// </summary>
        public static void FFplayFolderBrowser()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                //InitialDirectory = @"C:\",
                Title = "Browse for ffplay.exe",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "exe",
                Filter = "Executable (*.exe)|*.exe",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    VM.ConfigureView.FFplayPath_Text = openFileDialog.FileName;
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString(),
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }

            //var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            //System.Windows.Forms.DialogResult result = OpenFileDialog.ShowDialog();

            //// Popup Folder Browse Window
            //if (result == System.Windows.Forms.DialogResult.OK)
            //{
            //    // Display Folder Path in Textbox
            //    VM.ConfigureView.FFplayPath_Text = OpenFileDialog.FileName;
            //}
        }

        /// <summary>
        /// youtube-dl Folder Browser Dialog
        /// </summary>
        public static void youtubedlFolderBrowser()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                //InitialDirectory = @"C:\",
                Title = "Browse for youtube-dl.exe",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "exe",
                Filter = "Executable (*.exe)|*.exe",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    VM.ConfigureView.youtubedlPath_Text = openFileDialog.FileName;
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString(),
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }

            //var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            //System.Windows.Forms.DialogResult result = OpenFileDialog.ShowDialog();

            //// Popup Folder Browse Window
            //if (result == System.Windows.Forms.DialogResult.OK)
            //{
            //    // Display Folder Path in Textbox
            //    VM.ConfigureView.youtubedlPath_Text = OpenFileDialog.FileName;
            //}
        }

        /// <summary>
        /// Log Folder Browser Dialog 
        /// </summary>
        public static void LogFolderBrowser()
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                VM.ConfigureView.LogPath_Text = folderBrowserDialog.SelectedPath;

                // Add backslash if missing
                VM.ConfigureView.LogPath_Text = VM.ConfigureView.LogPath_Text.TrimEnd('\\') + @"\";
            }
        }


    }
}
