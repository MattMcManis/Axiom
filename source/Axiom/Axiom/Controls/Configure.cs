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
        public static string maxthreads; // All CPU Threads

        public readonly static string confAppRootDir = MainWindow.appRootDir;
        public readonly static string confAppDataLocalDir = MainWindow.appDataLocalDir + @"Axiom UI\";
        public readonly static string confAppDataRoamingDir = MainWindow.appDataRoamingDir + @"Axiom UI\";

        public readonly static string confAppRootPath = Path.Combine(confAppRootDir, "axiom.conf");
        public readonly static string confAppDataLocalPath = Path.Combine(confAppDataLocalDir, "axiom.conf");
        public readonly static string confAppDataRoamingPath = Path.Combine(confAppDataRoamingDir, "axiom.conf");

        //public static string axiomConfDir = MainWindow.appDataLocalDir + @"Axiom UI\"; // Axiom Config File Directory (Updated using Settings Tab cboConfigPath ComboBox)
        //public static string axiomConfFile = Path.Combine(axiomConfDir, "axiom.conf"); // Axiom Config File (Updated using Settings Tab cboConfigPath ComboBox)
        public static string axiomConfFile { get; set; } // Global directory+filename
        //public static String axiomConfFile()
        //{

        //}

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
        /// Import axiom.conf file
        /// </summary>
        //public static void ImportConfig(MainWindow mainwindow, string location)
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
            catch
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
                        "Could not load axiom.conf. \n\nDelete config and reload defaults?",
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
                            //Environment.Exit(0);
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

            //try
            //{
            //    List<string> listFailedImports = new List<string>();

            //    // Start Cofig File Read
            //    ConfigFile conf = null;

            //    // -------------------------
            //    // Check if axiom.conf file exists in in AppData Local Dir
            //    // -------------------------
            //    if (File.Exists(location))
            //    {
            //        conf = new ConfigFile(configFile);

            //        // Read
            //        ReadConfig(mainwindow, conf);
            //    }

            //    // --------------------------------------------------
            //    // Failed Imports
            //    // --------------------------------------------------
            //    if (listFailedImports.Count > 0 && listFailedImports != null)
            //    {
            //        Profiles.Profiles.failedImportMessage = string.Join(Environment.NewLine, listFailedImports);

            //        // Detect which screen we're on
            //        var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
            //        var thisScreen = allScreens.SingleOrDefault(s => mainwindow.Left >= s.WorkingArea.Left && mainwindow.Left < s.WorkingArea.Right);

            //        // Start Window
            //        FailedImportWindow failedimportwindow = new FailedImportWindow();

            //        // Position Relative to MainWindow
            //        failedimportwindow.Left = Math.Max((mainwindow.Left + (mainwindow.Width - failedimportwindow.Width) / 2), thisScreen.WorkingArea.Left);
            //        failedimportwindow.Top = Math.Max((mainwindow.Top + (mainwindow.Height - failedimportwindow.Height) / 2), thisScreen.WorkingArea.Top);

            //        // Open Window
            //        failedimportwindow.Show();
            //    }
            //}

            //// Error Loading axiom.conf
            ////
            //catch
            //{
            //    // Delete axiom.conf and Restart
            //    // Check if axiom.conf Exists
            //    if (File.Exists(configFile))
            //    {
            //        // Yes/No Dialog Confirmation
            //        //
            //        MessageBoxResult result = MessageBox.Show(
            //            "Could not load axiom.conf. \n\nDelete config and reload defaults?",
            //            "Error",
            //            MessageBoxButton.YesNo,
            //            MessageBoxImage.Error);
            //        switch (result)
            //        {
            //            // Create
            //            case MessageBoxResult.Yes:
            //                File.Delete(configFile);

            //                // Reload Control Defaults
            //                VM.ConfigureView.LoadConfigDefaults();
            //                VM.MainView.LoadControlsDefaults();
            //                VM.FormatView.LoadControlsDefaults();
            //                VM.VideoView.LoadControlsDefaults();
            //                VM.SubtitleView.LoadControlsDefaults();
            //                VM.AudioView.LoadControlsDefaults();

            //                // Restart Program
            //                Process.Start(Application.ResourceAssembly.Location);
            //                Application.Current.Shutdown();
            //                break;

            //            // Use Default
            //            case MessageBoxResult.No:
            //                // Force Shutdown
            //                System.Windows.Forms.Application.ExitThread();
            //                Environment.Exit(0);
            //                return;
            //        }
            //    }
            //    // If axiom.conf Not Found
            //    else
            //    {
            //        MessageBox.Show("No Previous Config File Found.",
            //                        "Notice",
            //                        MessageBoxButton.OK,
            //                        MessageBoxImage.Information);

            //        return;
            //    }
            //}
        }


        /// <summary>
        /// Import Read Config
        /// </summary>
        //public static void ReadConfig(MainWindow mainwindow, ConfigFile conf)
        //{
        //    // -------------------------
        //    // Main Window
        //    // -------------------------
        //    // Window Position Top
        //    double top = 0;
        //    double.TryParse(conf.Read("Main Window", "Window_Position_Top"), out top);
        //    mainwindow.Top = top;

        //    // Window Position Left
        //    double left = 0;
        //    double.TryParse(conf.Read("Main Window", "Window_Position_Left"), out left);
        //    mainwindow.Left = left;

        //    // Center
        //    if (top == 0 && left == 0)
        //    {
        //        mainwindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        //    }

        //    // Window Maximized
        //    bool mainwindow_WindowState_Maximized;
        //    bool.TryParse(conf.Read("Main Window", "WindowState_Maximized").ToLower(), out mainwindow_WindowState_Maximized);

        //    if (mainwindow_WindowState_Maximized == true)
        //    {
        //        //VM.MainView.Window_State = WindowState.Maximized;
        //        mainwindow.WindowState = WindowState.Maximized;
        //    }
        //    else
        //    {
        //        //VM.MainView.Window_State = WindowState.Normal;
        //        mainwindow.WindowState = WindowState.Normal;
        //    }

        //    // Window Width
        //    //double width = MainWindow.minWidth;
        //    double width = VM.MainView.Window_Width;
        //    double.TryParse(conf.Read("Main Window", "Window_Width"), out width);
        //    mainwindow.Width = width;

        //    // Window Height
        //    //double height = MainWindow.minHeight;
        //    double height = VM.MainView.Window_Height;
        //    double.TryParse(conf.Read("Main Window", "Window_Height"), out height);
        //    mainwindow.Height = height;

        //    // CMD Window Keep
        //    bool mainwindow_CMDWindowKeep_IsChecked;
        //    bool.TryParse(conf.Read("Main Window", "CMDWindowKeep_IsChecked").ToLower(), out mainwindow_CMDWindowKeep_IsChecked);
        //    VM.MainView.CMDWindowKeep_IsChecked = mainwindow_CMDWindowKeep_IsChecked;

        //    // Auto Sort Script
        //    bool mainwindow_AutoSortScript_IsChecked;
        //    bool.TryParse(conf.Read("Main Window", "AutoSortScript_IsChecked").ToLower(), out mainwindow_AutoSortScript_IsChecked);
        //    VM.MainView.AutoSortScript_IsChecked = mainwindow_AutoSortScript_IsChecked;


        //    // --------------------------------------------------
        //    // Settings
        //    // --------------------------------------------------
        //    // Config Path
        //    string configPath_SelectedItem = conf.Read("Settings", "ConfigPath_SelectedItem");
        //    if (!string.IsNullOrWhiteSpace(configPath_SelectedItem))
        //    {
        //        VM.ConfigureView.ConfigPath_SelectedItem = configPath_SelectedItem;
        //    }

        //    // Presets
        //    string customPresetsPath_Text = conf.Read("Settings", "CustomPresetsPath_Text");
        //    if (!string.IsNullOrWhiteSpace(customPresetsPath_Text))
        //    {
        //        VM.ConfigureView.CustomPresetsPath_Text = customPresetsPath_Text;
        //    }

        //    // FFmpeg
        //    string ffmpegPath_Text = conf.Read("Settings", "FFmpegPath_Text");
        //    if (!string.IsNullOrWhiteSpace(ffmpegPath_Text))
        //    {
        //        VM.ConfigureView.FFmpegPath_Text = ffmpegPath_Text;
        //    }
        //    // FFprobe
        //    string ffprobePath_Text = conf.Read("Settings", "FFprobePath_Text");
        //    if (!string.IsNullOrWhiteSpace(ffprobePath_Text))
        //    {
        //        VM.ConfigureView.FFprobePath_Text = ffprobePath_Text;
        //    }
        //    // FFplay
        //    string ffplayPath_Text = conf.Read("Settings", "FFplayPath_Text");
        //    if (!string.IsNullOrWhiteSpace(ffplayPath_Text))
        //    {
        //        VM.ConfigureView.FFplayPath_Text = ffplayPath_Text;
        //    }
        //    // youtube-dl
        //    string youtubedlPath_Text = conf.Read("Settings", "youtubedlPath_Text");
        //    if (!string.IsNullOrWhiteSpace(youtubedlPath_Text))
        //    {
        //        VM.ConfigureView.youtubedlPath_Text = youtubedlPath_Text;
        //    }

        //    // Log CheckBox
        //    bool logCheckBox_IsChecked;
        //    bool.TryParse(conf.Read("Settings", "LogCheckBox_IsChecked").ToLower(), out logCheckBox_IsChecked);
        //    VM.ConfigureView.LogCheckBox_IsChecked = logCheckBox_IsChecked;
        //    // Log Path
        //    string logPath_Text = conf.Read("Settings", "LogPath_Text");
        //    if (!string.IsNullOrWhiteSpace(logPath_Text))
        //    {
        //        VM.ConfigureView.LogPath_Text = logPath_Text;
        //    }

        //    // Shell
        //    string shell_SelectedItem = conf.Read("Settings", "Shell_SelectedItem");
        //    if (!string.IsNullOrWhiteSpace(shell_SelectedItem))
        //    {
        //        VM.ConfigureView.Shell_SelectedItem = shell_SelectedItem;
        //    }

        //    // Shell Title
        //    string shellTitle_SelectedItem = conf.Read("Settings", "ShellTitle_SelectedItem");
        //    if (!string.IsNullOrWhiteSpace(shellTitle_SelectedItem))
        //    {
        //        VM.ConfigureView.ShellTitle_SelectedItem = shellTitle_SelectedItem;
        //    }

        //    // Process Priority
        //    string processPriority_SelectedItem = conf.Read("Settings", "ProcessPriority_SelectedItem");
        //    if (!string.IsNullOrWhiteSpace(processPriority_SelectedItem))
        //    {
        //        VM.ConfigureView.ProcessPriority_SelectedItem = processPriority_SelectedItem;
        //    }

        //    // Threads
        //    string threads_SelectedItem = conf.Read("Settings", "Threads_SelectedItem");
        //    if (!string.IsNullOrWhiteSpace(threads_SelectedItem))
        //    {
        //        // Legacy Support: Capitalize First Letter of imported value. Old values are lowercase.
        //        VM.ConfigureView.Threads_SelectedItem = char.ToUpper(threads_SelectedItem[0]) + threads_SelectedItem.Substring(1);
        //    }

        //    // Ouput Naming
        //    // import full list new order
        //    string outputNaming_ItemOrder = conf.Read("Settings", "OutputNaming_ItemOrder");
        //    // null check
        //    if (!string.IsNullOrWhiteSpace(outputNaming_ItemOrder))
        //    {
        //        // Split the list by commas
        //        string[] arrOutputNaming_ItemOrder = outputNaming_ItemOrder.Split(',');

        //        // Create the new list
        //        // Remove Duplicates
        //        VM.ConfigureView.OutputNaming_ListView_Items = new ObservableCollection<string>(arrOutputNaming_ItemOrder
        //                                                                                        .Distinct()
        //                                                                                        .ToList()
        //                                                                                        .AsEnumerable()
        //                                                                                       );
        //        //VM.ConfigureView.OutputNaming_ListView_Items = arrOutputNaming_ItemOrder.Distinct().ToList();

        //        // Check the Master Default List for Missing Items
        //        IEnumerable<string> missingItems = MainWindow.outputNaming_Defaults
        //                                                     .Except(VM.ConfigureView.OutputNaming_ListView_Items/*arrOutputNaming_ItemOrder*/)
        //                                                     .ToList()
        //                                                     .AsEnumerable();
        //        //List<string> missingItems = MainWindow.outputNaming_Defaults.Except(arrOutputNaming_ItemOrder).ToList();

        //        foreach (string item in missingItems)
        //        {
        //            VM.ConfigureView.OutputNaming_ListView_Items.Add(item/*missingItems[i]*/);
        //        }

        //        // Selected Items String (items separated by commas)
        //        string outputNaming_SelectedItems = conf.Read("Settings", "OutputNaming_SelectedItems");
        //        // Empty List Check
        //        if (!string.IsNullOrEmpty(outputNaming_SelectedItems))
        //        {
        //            // Split
        //            string[] arrOuputNaming_SelectedItems = outputNaming_SelectedItems.Split(',');

        //            // Remove Duplicates
        //            List<string> lstOuputNaming_SelectedItems = arrOuputNaming_SelectedItems.Distinct().ToList();

        //            // Import Selected Items
        //            for (var i = 0; i < lstOuputNaming_SelectedItems.Count; i++)
        //            {
        //                // If Items List Contains the Imported Item
        //                if (VM.ConfigureView.OutputNaming_ListView_Items.Contains(arrOuputNaming_SelectedItems[i]))
        //                {
        //                    // Added Item to Selected Items List
        //                    //VM.ConfigureView.OutputNaming_ListView_SelectedItems.Add(arrOuputNaming_SelectedItems[i]);

        //                    // Select the Item
        //                    try
        //                    {
        //                        mainwindow.lstvOutputNaming.SelectedItems.Add(arrOuputNaming_SelectedItems[i]);
        //                    }
        //                    catch
        //                    {

        //                    }
        //                }
        //            }
        //        }
        //    }

        //    // Input Filename Tokens
        //    string inputFileNameTokens_SelectedItem = conf.Read("Settings", "InputFileNameTokens_SelectedItem");
        //    if (!string.IsNullOrWhiteSpace(inputFileNameTokens_SelectedItem))
        //    {
        //        // Legacy Values Fix
        //        switch (inputFileNameTokens_SelectedItem)
        //        {
        //            case "Job":
        //                VM.ConfigureView.InputFileNameTokens_SelectedItem = "Filename";
        //                break;
        //            case "Job+Tokens":
        //                VM.ConfigureView.InputFileNameTokens_SelectedItem = "Filename+Tokens";
        //                break;
        //            default:
        //                VM.ConfigureView.InputFileNameTokens_SelectedItem = inputFileNameTokens_SelectedItem;
        //                break;
        //        }
        //    }

        //    // Input Filename Tokens Custom
        //    string inputFileNameTokensCustom_Text = conf.Read("Settings", "InputFileNameTokensCustom_Text");
        //    if (!string.IsNullOrWhiteSpace(inputFileNameTokensCustom_Text))
        //    {
        //        VM.ConfigureView.InputFileNameTokensCustom_Text = inputFileNameTokensCustom_Text;
        //        //.Trim() // remove spaces
        //        //.Replace(",", ", "); // add spaces after every comma
        //    }

        //    // Spacing
        //    string outputFileNameSpacing_SelectedItem = conf.Read("Settings", "OutputFileNameSpacing_SelectedItem");
        //    if (!string.IsNullOrWhiteSpace(outputFileNameSpacing_SelectedItem))
        //    {
        //        VM.ConfigureView.OutputFileNameSpacing_SelectedItem = outputFileNameSpacing_SelectedItem;
        //    }

        //    // Output File Overwrite
        //    string outputOverwrite_SelectedItem = conf.Read("Settings", "OutputOverwrite_SelectedItem");
        //    if (!string.IsNullOrWhiteSpace(outputOverwrite_SelectedItem))
        //    {
        //        VM.ConfigureView.OutputOverwrite_SelectedItem = outputOverwrite_SelectedItem;
        //    }

        //    // Theme
        //    string theme_SelectedItem = conf.Read("Settings", "Theme_SelectedItem");
        //    if (!string.IsNullOrWhiteSpace(theme_SelectedItem))
        //    {
        //        VM.ConfigureView.Theme_SelectedItem = theme_SelectedItem;
        //    }

        //    // Updates
        //    bool updateAutoCheck_IsChecked;
        //    bool.TryParse(conf.Read("Settings", "UpdateAutoCheck_IsChecked").ToLower(), out updateAutoCheck_IsChecked);
        //    VM.ConfigureView.UpdateAutoCheck_IsChecked = updateAutoCheck_IsChecked;
        //}


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
                        Configure.ConfigFile.conf = new ConfigFile(Path.Combine(directory, filename));

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


                //Configure.ConfigFile conf = null;

                //try
                //{
                //    // Start conf File Write
                //    conf = new Configure.ConfigFile(directory + "axiom.conf");

                //    // -------------------------
                //    // Main Window
                //    // -------------------------
                //    // Window Position Top
                //    conf.Write("Main Window", "Window_Position_Top", mainwindow.Top.ToString());

                //    // Window Position Left
                //    conf.Write("Main Window", "Window_Position_Left", mainwindow.Left.ToString());

                //    // Window Width
                //    conf.Write("Main Window", "Window_Width", mainwindow.Width.ToString());

                //    // Window Height
                //    conf.Write("Main Window", "Window_Height", mainwindow.Height.ToString());

                //    // Window Maximized
                //    if (mainwindow.WindowState == WindowState.Maximized)
                //    {
                //        conf.Write("Main Window", "WindowState_Maximized", "true");
                //    }
                //    else
                //    {
                //        conf.Write("Main Window", "WindowState_Maximized", "false");
                //    }

                //    // CMD Keep Window Open Toggle
                //    conf.Write("Main Window", "CMDWindowKeep_IsChecked", VM.MainView.CMDWindowKeep_IsChecked.ToString().ToLower());

                //    // Auto Sort Script Toggle
                //    conf.Write("Main Window", "AutoSortScript_IsChecked", VM.MainView.AutoSortScript_IsChecked.ToString().ToLower());


                //    // --------------------------------------------------
                //    // Settings
                //    // --------------------------------------------------
                //    // -------------------------
                //    // Config
                //    // -------------------------
                //    // Config Path
                //    conf.Write("Settings", "ConfigPath_SelectedItem", VM.ConfigureView.ConfigPath_SelectedItem);

                //    // Presets
                //    conf.Write("Settings", "CustomPresetsPath_Text", VM.ConfigureView.CustomPresetsPath_Text);

                //    // FFmpeg
                //    conf.Write("Settings", "FFmpegPath_Text", VM.ConfigureView.FFmpegPath_Text);
                //    conf.Write("Settings", "FFprobePath_Text", VM.ConfigureView.FFprobePath_Text);
                //    conf.Write("Settings", "FFplayPath_Text", VM.ConfigureView.FFplayPath_Text);
                //    conf.Write("Settings", "youtubedlPath_Text", VM.ConfigureView.youtubedlPath_Text);

                //    // Log
                //    conf.Write("Settings", "LogCheckBox_IsChecked", VM.ConfigureView.LogCheckBox_IsChecked.ToString().ToLower());
                //    conf.Write("Settings", "LogPath_Text", VM.ConfigureView.LogPath_Text);

                //    // -------------------------
                //    // Process
                //    // -------------------------
                //    // Shell
                //    conf.Write("Settings", "Shell_SelectedItem", VM.ConfigureView.Shell_SelectedItem);

                //    // Shell Title
                //    conf.Write("Settings", "ShellTitle_SelectedItem", VM.ConfigureView.ShellTitle_SelectedItem);

                //    // Process Priority
                //    conf.Write("Settings", "ProcessPriority_SelectedItem", VM.ConfigureView.ProcessPriority_SelectedItem);

                //    // Threads
                //    conf.Write("Settings", "Threads_SelectedItem", VM.ConfigureView.Threads_SelectedItem);

                //    // -------------------------
                //    // Input
                //    // -------------------------

                //    // Input Filename Tokens
                //    conf.Write("Settings", "InputFileNameTokens_SelectedItem", VM.ConfigureView.InputFileNameTokens_SelectedItem);

                //    // Input Filename Tokens Custom
                //    conf.Write("Settings", "InputFileNameTokensCustom_Text",
                //                            MainWindow.RemoveLineBreaks(
                //                                VM.ConfigureView.InputFileNameTokensCustom_Text
                //                            //.Replace(" ", "")
                //                            )

                //            );

                //    // -------------------------
                //    // Output
                //    // -------------------------
                //    // Order
                //    string outputNaming_ItemOrder = string.Join(",", VM.ConfigureView.OutputNaming_ListView_Items
                //                                                     .Where(s => !string.IsNullOrWhiteSpace(s)));
                //    conf.Write("Settings", "OutputNaming_ItemOrder", outputNaming_ItemOrder);

                //    // Selected
                //    string outputNaming_SelectedItems = string.Join(",", VM.ConfigureView.OutputNaming_ListView_SelectedItems
                //                                                         .Where(s => !string.IsNullOrEmpty(s)));
                //    conf.Write("Settings", "OutputNaming_SelectedItems", outputNaming_SelectedItems);

                //    // Spacing
                //    conf.Write("Settings", "OutputFileNameSpacing_SelectedItem", VM.ConfigureView.OutputFileNameSpacing_SelectedItem);

                //    // Output File Overwrite
                //    conf.Write("Settings", "OutputOverwrite_SelectedItem", VM.ConfigureView.OutputOverwrite_SelectedItem);

                //    // -------------------------
                //    // App
                //    // -------------------------
                //    // Theme
                //    conf.Write("Settings", "Theme_SelectedItem", VM.ConfigureView.Theme_SelectedItem);

                //    // Updates
                //    conf.Write("Settings", "UpdateAutoCheck_IsChecked", VM.ConfigureView.UpdateAutoCheck_IsChecked.ToString().ToLower());


                //    // --------------------------------------------------
                //    // User
                //    // --------------------------------------------------
                //    // Input Previous Path
                //    if (!string.IsNullOrWhiteSpace(MainWindow.inputPreviousPath))
                //    {
                //        if (Directory.Exists(MainWindow.inputPreviousPath))
                //        {
                //            conf.Write("User", "InputPreviousPath", MainWindow.inputPreviousPath);
                //        }
                //    }

                //    // Output Previous Path
                //    if (!string.IsNullOrWhiteSpace(MainWindow.outputPreviousPath))
                //    {
                //        if (Directory.Exists(MainWindow.outputPreviousPath))
                //        {
                //            conf.Write("User", "OutputPreviousPath", MainWindow.outputPreviousPath);
                //        }
                //    }
                //}
                //catch
                //{
                //    MessageBox.Show("Could not save config file. May require Administrator privileges.",
                //                    "Error",
                //                    MessageBoxButton.OK,
                //                    MessageBoxImage.Error);
                //}

            }
        }


        /// <summary>
        /// Export Write Config
        /// </summary>
        //public static void WriteConfig(MainWindow mainwindow, MainViewModel vm, ConfigFile conf)
        //{

        //}


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
                // Load Custom Presets
                // Refresh Presets ComboBox
                // -------------------------
                Profiles.Profiles.LoadCustomPresets();
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
