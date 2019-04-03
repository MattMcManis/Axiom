/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2019 Matt McManis
https://github.com/MattMcManis/Axiom
https://axiomui.github.io
mattmcmanis@outlook.com

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.If not, see <http://www.gnu.org/licenses/>. 
---------------------------------------------------------------------- */

using Axiom.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
// Disable XML Comment warnings
#pragma warning disable 1591

namespace Axiom
{
    public class Configure
    {
        // --------------------------------------------------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------------------------------------------------
        public static string theme; // Set Theme
        public static string threads; // Set FFmpeg -threads
        public static string maxthreads; // All CPU Threads
        public static string configDir = MainWindow.appDir; // Axiom Config File Path axiom.conf path (Can't change location)
        public static string configFile = MainWindow.appDir + "axiom.conf"; // Axiom Config File (Can't change location)


        /// <summary>
        ///    INI Reader
        /// </summary>
        /*
        * Source: GitHub Sn0wCrack
        * https://gist.github.com/Sn0wCrack/5891612
        */
        public partial class INIFile
        {
            public string path { get; private set; }

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

            public INIFile(string INIPath)
            {
                path = INIPath;
            }
            public void Write(string Section, string Key, string Value)
            {
                WritePrivateProfileString(Section, Key, Value, this.path);
            }

            public string Read(string Section, string Key)
            {
                StringBuilder temp = new StringBuilder(255);
                int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
                return temp.ToString();
            }
        }


        /// <summary>
        ///    Import axiom.conf file
        /// </summary>
        public static void ImportConfig(MainWindow mainwindow, ViewModel vm)
        {
            try
            {
                List<string> listFailedImports = new List<string>();

                // Start INI File Read
                Configure.INIFile conf = null;

                // -------------------------
                // Check if axiom.conf file exists
                // -------------------------
                if (File.Exists(configFile))
                {
                    conf = new Configure.INIFile(configFile);

                    // -------------------------
                    // Main Window
                    // -------------------------
                    //Window Position Top
                    double top;
                    double.TryParse(conf.Read("Main Window", "Window_Position_Top"), out top);
                    mainwindow.Top = top;

                    ////double? top = Double.Parse(conf.Read("Main Window", "Window_Position_Top"));
                    //if (top != null)
                    //{
                    //    //vm.Window_Top = Convert.ToDouble(inif.Read("Main Window", "Window_Position_Top"));
                    //    //mainwindow.Top = Convert.ToDouble(conf.Read("Main Window", "Window_Position_Top"));
                    //    mainwindow.Top = top;
                    //}

                    // Window Position Left
                    double left;
                    double.TryParse(conf.Read("Main Window", "Window_Position_Left"), out left);
                    mainwindow.Left = left;

                    //double? left = Double.Parse(conf.Read("Main Window", "Window_Position_Left"));
                    //if (left != null)
                    //{
                    //    //vm.Window_Left = Convert.ToDouble(inif.Read("Main Window", "Window_Position_Left"));
                    //    //mainwindow.Left = Convert.ToDouble(conf.Read("Main Window", "Window_Position_Left"));
                    //    mainwindow.Left = left;
                    //}

                    // Window Maximized
                    bool mainwindow_WindowState_Maximized;
                    bool.TryParse(conf.Read("Main Window", "WindowState_Maximized").ToLower(), out mainwindow_WindowState_Maximized);

                    //if (Convert.ToBoolean(conf.Read("Main Window", "WindowState_Maximized").ToLower()) == true)
                    if (mainwindow_WindowState_Maximized == true)
                    {
                        //vm.Window_State = WindowState.Maximized;
                        mainwindow.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        //vm.Window_State = WindowState.Normal;
                        mainwindow.WindowState = WindowState.Normal;
                    }

                    // Window Width
                    double width;
                    double.TryParse(conf.Read("Main Window", "Window_Width"), out width);
                    mainwindow.Width = width;

                    //double? width = Double.Parse(conf.Read("Main Window", "Window_Width"));
                    //if (width != null)
                    //{
                    //    //vm.Window_Width = Convert.ToDouble(inif.Read("MainWindow", "Window_Width"));
                    //    mainwindow.Width = Convert.ToDouble(conf.Read("Main Window", "Window_Width"));
                    //}

                    // Window Height
                    double height;
                    double.TryParse(conf.Read("Main Window", "Window_Height"), out height);
                    mainwindow.Height = height;
                    //double? height = Double.Parse(conf.Read("Main Window", "Window_Height"));
                    //if (height != null)
                    //{
                    //    //vm.Window_Height = Convert.ToDouble(inif.Read("Main Window", "Window_Height"));
                    //    mainwindow.Height = Convert.ToDouble(conf.Read("Main Window", "Window_Height"));
                    //}

                    // CMD Window Keep
                    bool mainwindow_CMDWindowKeep_IsChecked;
                    bool.TryParse(conf.Read("Main Window", "CMDWindowKeep_IsChecked").ToLower(), out mainwindow_CMDWindowKeep_IsChecked);
                    vm.CMDWindowKeep_IsChecked = mainwindow_CMDWindowKeep_IsChecked;

                    // Auto Sort Script
                    bool mainwindow_AutoSortScript_IsChecked;
                    bool.TryParse(conf.Read("Main Window", "AutoSortScript_IsChecked").ToLower(), out mainwindow_AutoSortScript_IsChecked);
                    vm.AutoSortScript_IsChecked = mainwindow_AutoSortScript_IsChecked;


                    // --------------------------------------------------
                    // Settings
                    // --------------------------------------------------
                    // FFmpeg
                    vm.FFmpegPath_Text = conf.Read("Settings", "FFmpegPath_Text");
                    vm.FFprobePath_Text = conf.Read("Settings", "FFprobePath_Text");
                    vm.FFplayPath_Text = conf.Read("Settings", "FFplayPath_Text");

                    // Presets
                    vm.CustomPresetsPath_Text = conf.Read("Settings", "CustomPresetsPath_Text");

                    // Log
                    //vm.LogCheckBox_IsChecked = Convert.ToBoolean(conf.Read("Settings", "LogCheckBox_IsChecked").ToLower());
                    bool settings_LogCheckBox_IsChecked;
                    bool.TryParse(conf.Read("Settings", "LogCheckBox_IsChecked").ToLower(), out settings_LogCheckBox_IsChecked);
                    vm.LogCheckBox_IsChecked = settings_LogCheckBox_IsChecked;

                    vm.LogPath_Text = conf.Read("Settings", "LogPath_Text");

                    // Threads
                    vm.Threads_SelectedItem = conf.Read("Settings", "Threads_SelectedItem");

                    // Theme
                    vm.Theme_SelectedItem = conf.Read("Settings", "Theme_SelectedItem");

                    // Updates
                    //vm.UpdateAutoCheck_IsChecked = Convert.ToBoolean(conf.Read("Settings", "UpdateAutoCheck_IsChecked").ToLower());
                    bool settings_UpdateAutoCheck_IsChecked;
                    bool.TryParse(conf.Read("Settings", "UpdateAutoCheck_IsChecked").ToLower(), out settings_UpdateAutoCheck_IsChecked);
                    vm.UpdateAutoCheck_IsChecked = settings_UpdateAutoCheck_IsChecked;
                }

                // -------------------------
                // Preset ini file does not exist
                // -------------------------
                else if (!File.Exists(configFile))
                {
                    MessageBox.Show("Preset does not exist.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }


                // --------------------------------------------------
                // Failed Imports
                // --------------------------------------------------
                //if (listFailedImports.Count > 0 && listFailedImports != null)
                //{
                //    failedImportMessage = string.Join(Environment.NewLine, listFailedImports);

                //    // Detect which screen we're on
                //    var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                //    var thisScreen = allScreens.SingleOrDefault(s => mainwindow.Left >= s.WorkingArea.Left && mainwindow.Left < s.WorkingArea.Right);

                //    // Start Window
                //    FailedImportWindow failedimportwindow = new FailedImportWindow();

                //    // Position Relative to MainWindow
                //    failedimportwindow.Left = Math.Max((mainwindow.Left + (mainwindow.Width - failedimportwindow.Width) / 2), thisScreen.WorkingArea.Left);
                //    failedimportwindow.Top = Math.Max((mainwindow.Top + (mainwindow.Height - failedimportwindow.Height) / 2), thisScreen.WorkingArea.Top);

                //    // Open Window
                //    failedimportwindow.Show();
                //}
            }

            // Error Loading axiom.conf
            //
            catch
            {
                // Delete axiom.conf and Restart
                // Check if axiom.conf Exists
                if (File.Exists(configFile))
                {
                    // Yes/No Dialog Confirmation
                    //
                    MessageBoxResult result = MessageBox.Show(
                        "Could not load axiom.conf. \n\nDelete config and reload defaults?",
                        "Error",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error);
                    switch (result)
                    {
                        // Create
                        case MessageBoxResult.Yes:
                            File.Delete(configFile);

                            // Reload Control Defaults
                            vm.LoadDefaults();

                            // Restart Program
                            Process.Start(Application.ResourceAssembly.Location);
                            Application.Current.Shutdown();
                            break;

                        // Use Default
                        case MessageBoxResult.No:
                            // Force Shutdown
                            System.Windows.Forms.Application.ExitThread();
                            Environment.Exit(0);
                            return;
                    }
                }
                // If axiom.conf Not Found
                else
                {
                    MessageBox.Show("No Previous Config File Found.",
                                    "Notice",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);

                    return;
                }
            }
        }


        /// <summary>
        ///    Export axiom.conf
        /// </summary>
        public static void ExportConfig(MainWindow mainwindow, ViewModel vm)
        {
            //// Check if Directory Exists
            //if (!Directory.Exists(configDir))
            //{
            //    // Yes/No Dialog Confirmation
            //    //
            //    MessageBoxResult resultExport = MessageBox.Show("Config Folder does not exist. Automatically create it?",
            //                                                    "Directory Not Found",
            //                                                    MessageBoxButton.YesNo,
            //                                                    MessageBoxImage.Information);
            //    switch (resultExport)
            //    {
            //        // Create
            //        case MessageBoxResult.Yes:
            //            try
            //            {
            //                Directory.CreateDirectory(configDir);
            //            }
            //            catch
            //            {
            //                MessageBox.Show("Could not create Config folder. May require Administrator privileges.",
            //                                "Error",
            //                                MessageBoxButton.OK,
            //                                MessageBoxImage.Error);
            //            }
            //            break;
            //        // Use Default
            //        case MessageBoxResult.No:
            //            break;
            //    }
            //}

            // Save axiom.conf file if directory exists
            /*else */if (Directory.Exists(configDir))
            {
                // Start conf File Write
                Configure.INIFile conf = new Configure.INIFile(configFile);

                // -------------------------
                // Main Window
                // -------------------------
                // Window Position Top
                conf.Write("Main Window", "Window_Position_Top", mainwindow.Top.ToString());

                // Window Position Left
                conf.Write("Main Window", "Window_Position_Left", mainwindow.Left.ToString());

                // Window Width
                conf.Write("Main Window", "Window_Width", mainwindow.Width.ToString());

                // Window Height
                conf.Write("Main Window", "Window_Height", mainwindow.Height.ToString());

                // Window Maximized
                if (mainwindow.WindowState == WindowState.Maximized)
                {
                    conf.Write("Main Window", "WindowState_Maximized", "true");
                }
                else
                {
                    conf.Write("Main Window", "WindowState_Maximized", "false");
                }

                // CMD Keep Window Open Toggle
                conf.Write("Main Window", "CMDWindowKeep_IsChecked", vm.CMDWindowKeep_IsChecked.ToString().ToLower());

                // Auto Sort Script Toggle
                conf.Write("Main Window", "AutoSortScript_IsChecked", vm.AutoSortScript_IsChecked.ToString().ToLower());


                // --------------------------------------------------
                // Settings
                // --------------------------------------------------
                // FFmpeg
                conf.Write("Settings", "FFmpegPath_Text", vm.FFmpegPath_Text);
                conf.Write("Settings", "FFprobePath_Text", vm.FFprobePath_Text);
                conf.Write("Settings", "FFplayPath_Text", vm.FFplayPath_Text);

                // Presets
                conf.Write("Settings", "CustomPresetsPath_Text", vm.CustomPresetsPath_Text);

                // Log
                conf.Write("Settings", "LogCheckBox_IsChecked", vm.LogCheckBox_IsChecked.ToString().ToLower());
                conf.Write("Settings", "LogPath_Text", vm.LogPath_Text);

                // Threads
                conf.Write("Settings", "Threads_SelectedItem", vm.Threads_SelectedItem);

                // Theme
                conf.Write("Settings", "Theme_SelectedItem", vm.Theme_SelectedItem);

                // Updates
                conf.Write("Settings", "UpdateAutoCheck_IsChecked", vm.UpdateAutoCheck_IsChecked.ToString().ToLower());
            }
        }



        /// <summary>
        ///     Load CustomPresets Path
        /// </summary>
        //public static void LoadCustomPresetsPath(ViewModel vm)
        //{
        //    // --------------------------------------------------
        //    // Safeguard Against Corrupt Saved Settings
        //    // --------------------------------------------------
        //    try
        //    {
        //        // --------------------------
        //        // First time use
        //        // --------------------------
        //        if (string.IsNullOrEmpty(Settings.Default.CustomPresetsPath))
        //        {
        //            // Set ComboBox if Configure Window is Open
        //            vm.CustomPresetsPath_Text = "<auto>";

        //            // Save for next launch
        //            Settings.Default.CustomPresetsPath = vm.CustomPresetsPath_Text;
        //            Settings.Default.Save();
        //        }
        //        // --------------------------
        //        // Load Saved Settings Override
        //        // --------------------------
        //        else if (!string.IsNullOrEmpty(Settings.Default.CustomPresetsPath)) // null check
        //        {
        //            // Set ComboBox if Configure Window is Open
        //            vm.CustomPresetsPath_Text = Settings.Default.CustomPresetsPath;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}


        /// <summary>
        ///     Load FFmpeg Path
        /// </summary>
        //public static void LoadFFmpegPath(ViewModel vm)
        //{
        //    // --------------------------------------------------
        //    // Safeguard Against Corrupt Saved Settings
        //    // --------------------------------------------------
        //    try
        //    {
        //        // --------------------------
        //        // First time use
        //        // --------------------------
        //        if (string.IsNullOrEmpty(Settings.Default.FFmpegPath))
        //        {
        //            // Set ComboBox if Configure Window is Open
        //            vm.FFmpegPath_Text = "<auto>";

        //            // Save for next launch
        //            Settings.Default.FFmpegPath = vm.FFmpegPath_Text;
        //            Settings.Default.Save();
        //        }
        //        // --------------------------
        //        // Load Saved Settings Override
        //        // --------------------------
        //        else if (!string.IsNullOrEmpty(Settings.Default.FFmpegPath)) // null check
        //        {
        //            // Set ComboBox if Configure Window is Open
        //            vm.FFmpegPath_Text = Settings.Default.FFmpegPath;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}


        /// <summary>
        ///     Load FFprobe Path
        /// </summary>
        //public static void LoadFFprobePath(ViewModel vm)
        //{
        //    // --------------------------------------------------
        //    // Safeguard Against Corrupt Saved Settings
        //    // --------------------------------------------------
        //    try
        //    {
        //        // --------------------------
        //        // First time use
        //        // --------------------------
        //        if (string.IsNullOrEmpty(Settings.Default.FFprobePath))
        //        {
        //            // Set ComboBox if Configure Window is Open
        //            vm.FFprobePath_Text = "<auto>";

        //            // Save for next launch
        //            Settings.Default.FFprobePath = vm.FFprobePath_Text;
        //            Settings.Default.Save();
        //        }
        //        // --------------------------
        //        // Load Saved Settings Override
        //        // --------------------------
        //        else if (!string.IsNullOrEmpty(Settings.Default.FFprobePath)) // null check
        //        {
        //            // Set ComboBox if Configure Window is Open
        //            vm.FFprobePath_Text = Settings.Default.FFprobePath;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}


        /// <summary>
        ///     Load FFplay Path
        /// </summary>
        //public static void LoadFFplayPath(ViewModel vm)
        //{
        //    // --------------------------------------------------
        //    // Safeguard Against Corrupt Saved Settings
        //    // --------------------------------------------------
        //    try
        //    {
        //        // --------------------------
        //        // First time use
        //        // --------------------------
        //        if (string.IsNullOrEmpty(Settings.Default.FFplayPath))
        //        {
        //            // Set ComboBox if Configure Window is Open
        //            vm.FFplayPath_Text = "<auto>";

        //            // Save for next launch
        //            Settings.Default.FFplayPath = vm.FFplayPath_Text;
        //            Settings.Default.Save();
        //        }
        //        // --------------------------
        //        // Load Saved Settings Override
        //        // --------------------------
        //        else if (!string.IsNullOrEmpty(Settings.Default.FFplayPath)) // null check
        //        {
        //            // Set ComboBox if Configure Window is Open
        //            vm.FFplayPath_Text = Settings.Default.FFplayPath;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}


        /// <summary>
        ///     Load youtubedl Path
        /// </summary>
        //public static void LoadyoutubedlPath(ViewModel vm)
        //{
        //    // --------------------------------------------------
        //    // Safeguard Against Corrupt Saved Settings
        //    // --------------------------------------------------
        //    try
        //    {
        //        // --------------------------
        //        // First time use
        //        // --------------------------
        //        if (string.IsNullOrEmpty(Settings.Default.youtubedlPath))
        //        {
        //            // Set ComboBox if Configure Window is Open
        //            vm.youtubedlPath_Text = "<auto>";

        //            // Save for next launch
        //            Settings.Default.youtubedlPath = vm.youtubedlPath_Text;
        //            Settings.Default.Save();
        //        }
        //        // --------------------------
        //        // Load Saved Settings Override
        //        // --------------------------
        //        else if (!string.IsNullOrEmpty(Settings.Default.youtubedlPath)) // null check
        //        {
        //            // Set ComboBox if Configure Window is Open
        //            vm.youtubedlPath_Text = Settings.Default.youtubedlPath;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}


        /// <summary>
        ///     Load Log Checkbox
        /// </summary>
        //public static void LoadLogCheckbox(ViewModel vm)
        //{
        //    // --------------------------------------------------
        //    // Safeguard Against Corrupt Saved Settings
        //    // --------------------------------------------------
        //    try
        //    {
        //        // --------------------------
        //        // First time use
        //        // --------------------------
        //        if (string.IsNullOrEmpty(Convert.ToString(Settings.Default.Log_IsChecked)))
        //        {
        //            // Set ComboBox if Configure Window is Open
        //            vm.LogCheckBox_IsChecked = false;

        //            // Save for next launch
        //            Settings.Default.Log_IsChecked = false;
        //            Settings.Default.Save();
        //        }
        //        // --------------------------
        //        // Load Saved Settings Override
        //        // --------------------------
        //        else if (!string.IsNullOrEmpty(Convert.ToString(Settings.Default.Log_IsChecked)))
        //        {
        //            // Set ComboBox if Configure Window is Open
        //            vm.LogCheckBox_IsChecked = Convert.ToBoolean(Settings.Default.Log_IsChecked);
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}


        /// <summary>
        ///     Load Log Path
        /// </summary>
        //public static void LoadLogPath(ViewModel vm)
        //{
        //    // --------------------------------------------------
        //    // Safeguard Against Corrupt Saved Settings
        //    // --------------------------------------------------
        //    try
        //    {
        //        // --------------------------
        //        // First time use
        //        // --------------------------
        //        if (string.IsNullOrEmpty(Settings.Default.LogPath))
        //        {
        //            //logPath = string.Empty;

        //            // Set ComboBox if Configure Window is Open
        //            vm.LogPath_Text = string.Empty;

        //            // Save for next launch
        //            Settings.Default.LogPath = vm.LogPath_Text;
        //            Settings.Default.Save();
        //        }
        //        // --------------------------
        //        // Load Saved Settings Override
        //        // --------------------------
        //        if (!string.IsNullOrEmpty(Settings.Default.LogPath)) // null check
        //        {
        //            // Set ComboBox if Configure Window is Open
        //            vm.LogPath_Text = Settings.Default.LogPath;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}


        /// <summary>
        ///     Load Threads
        /// </summary>
        //public static void LoadThreads(ViewModel vm)
        //{
        //    // --------------------------------------------------
        //    // Safeguard Against Corrupt Saved Settings
        //    // --------------------------------------------------
        //    try
        //    {
        //        // --------------------------
        //        // First time use
        //        // --------------------------
        //        if (string.IsNullOrEmpty(Settings.Default.Threads))
        //        {
        //            threads = "optimal";

        //            // Set ComboBox if Configure Window is Open
        //            vm.Threads_SelectedItem = threads;

        //            // Save for next launch
        //            Settings.Default.Threads = threads;
        //            Settings.Default.Save();
        //        }
        //        // --------------------------
        //        // Load Saved Settings Override
        //        // --------------------------
        //        else if (!string.IsNullOrEmpty(Settings.Default.Threads)) // null check
        //        {
        //            threads = Settings.Default.Threads;

        //            // Set ComboBox if Configure Window is Open
        //            vm.Threads_SelectedItem = Settings.Default.Threads;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}



        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Control Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        // --------------------------------------------------
        // Custom Presets Folder Browser Dialog
        // --------------------------------------------------
        public static void CustomPresetsFolderBrowser(ViewModel vm)
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                vm.CustomPresetsPath_Text = folderBrowserDialog.SelectedPath;

                // Add backslash if missing
                vm.CustomPresetsPath_Text = vm.CustomPresetsPath_Text.TrimEnd('\\') + @"\";

                // Save FFmpeg Path for next launch
                //Settings.Default.CustomPresetsPath = vm.CustomPresetsPath_Text;
                //Settings.Default.Save();
                //Settings.Default.Reload();
            }
        }

        // --------------------------------------------------
        // FFmpeg Folder Browser Dialog
        // --------------------------------------------------
        public static void FFmpegFolderBrowser(ViewModel vm) 
        {
            var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = OpenFileDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                vm.FFmpegPath_Text = OpenFileDialog.FileName;

                // Save 7-zip Path for next launch
                //Settings.Default.FFmpegPath = vm.FFmpegPath_Text;
                //Settings.Default.Save();
                //Settings.Default.Reload();
            }
        }


        // --------------------------------------------------
        // FFprobe Folder Browser Dialog
        // --------------------------------------------------
        public static void FFprobeFolderBrowser(ViewModel vm)
        {
            var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = OpenFileDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                vm.FFprobePath_Text = OpenFileDialog.FileName;

                // Save WinRAR Path for next launch
                //Settings.Default.FFprobePath = vm.FFprobePath_Text;
                //Settings.Default.Save();
                //Settings.Default.Reload();
            }
        }


        // --------------------------------------------------
        // FFplay Folder Browser Dialog
        // --------------------------------------------------
        public static void FFplayFolderBrowser(ViewModel vm) // Method
        {
            var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = OpenFileDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                vm.FFplayPath_Text = OpenFileDialog.FileName;

                // Save WinRAR Path for next launch
                //Settings.Default.FFplayPath = vm.FFplayPath_Text;
                //Settings.Default.Save();
                //Settings.Default.Reload();
            }
        }


        // --------------------------------------------------
        // youtube-dl Folder Browser Dialog
        // --------------------------------------------------
        public static void youtubedlFolderBrowser(ViewModel vm)
        {
            var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = OpenFileDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                vm.youtubedlPath_Text = OpenFileDialog.FileName;

                //// Save 7-zip Path for next launch
                //Settings.Default.youtubedlPath = vm.youtubedlPath_Text;
                //Settings.Default.Save();
                //Settings.Default.Reload();
            }
        }


        // --------------------------------------------------
        // Log Folder Browser Dialog 
        // --------------------------------------------------
        public static void LogFolderBrowser(ViewModel vm) // Method
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                vm.LogPath_Text = folderBrowserDialog.SelectedPath;

                // Add backslash if missing
                vm.LogPath_Text = vm.LogPath_Text.TrimEnd('\\') + @"\";

                //// Save FFmpeg Path for next launch
                //Settings.Default.LogPath = vm.LogPath_Text;
                //Settings.Default.Save();
                //Settings.Default.Reload();
            }
        }


    }
}
