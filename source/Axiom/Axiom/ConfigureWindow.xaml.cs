/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017 Matt McManis
http://github.com/MattMcManis/Axiom
http://axiomui.github.io
axiom.interface@gmail.com

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

using Axiom.Properties;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
// Disable XML Comment warnings
#pragma warning disable 1591

namespace Axiom
{
    /// <summary>
    /// Interaction logic for ConfigureWindow.xaml
    /// </summary>
    public partial class ConfigureWindow : Window
    {
        private MainWindow mainwindow;

        // --------------------------------------------------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------------------------------------------------
        public static string theme; // Set Theme (public - pass data)
        public static string ffmpegPath; // Config Settings Path (public - pass data)
        public static string ffprobePath; // Config Settings Path (public - pass data)
        public static string logPath; // output.log Config Settings Path (public - pass data)
        public static bool logEnable; //checkBoxLogConfig, Enable or Disable Log, true or false - (public - pass data)
        public static string threads; // Set FFmpeg -threads (public - pass data)

        // --------------------------------------------------------------------------------------------------------
        // Window
        // --------------------------------------------------------------------------------------------------------
        public ConfigureWindow(MainWindow mainwindow) // Pass Constructor from MainWindow
        {
            InitializeComponent();

            this.mainwindow = mainwindow;

            // Set Min/Max Width/Height to prevent Tablets maximizing
            this.MinWidth = 450;
            this.MinHeight = 200;
            this.MaxWidth = 450;
            this.MaxHeight = 200;

            // --------------------------------------------------
            // Load From Saved Settings
            // --------------------------------------------------
            // Theme CombBox
            ConfigureWindow.LoadTheme(this);

            // FFmpeg Path 
            ConfigureWindow.LoadFFmpegPath(this);

            // PProbe Path
            ConfigureWindow.LoadFFprobePath(this);

            // Log CheckBox
            ConfigureWindow.LoadLogCheckbox(this);

            // Log Path
            ConfigureWindow.LoadLogPath(this);

            // Threads CombBox
            ConfigureWindow.LoadThreads(this);
        }


        /// <summary>
        /// Load Theme
        /// </summary>
        public static void LoadTheme(ConfigureWindow configurewindow)
        {
            // --------------------------------------------------
            // Safeguard Against Corrupt Saved Settings
            // --------------------------------------------------
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                if (string.IsNullOrEmpty(Settings.Default["Theme"].ToString()))
                {
                    ConfigureWindow.theme = "Axiom";

                    // Set ComboBox if Configure Window is Open
                    if (configurewindow != null)
                    {
                        configurewindow.cboTheme.SelectedItem = "Axiom";
                    }

                    // Save Theme for next launch
                    Settings.Default["Theme"] = ConfigureWindow.theme;
                    Settings.Default.Save();

                    // Change Theme Resource
                    App.Current.Resources.MergedDictionaries.Clear();
                    App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                    {
                        Source = new Uri("Theme" + ConfigureWindow.theme + ".xaml", UriKind.RelativeOrAbsolute)
                    });
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Settings.Default["Theme"].ToString())) // null check
                {
                    ConfigureWindow.theme = Settings.Default["Theme"].ToString();

                    // Set ComboBox if Configure Window is Open
                    if (configurewindow != null)
                    {
                        configurewindow.cboTheme.SelectedItem = Settings.Default["Theme"].ToString();
                    }

                    // Change Theme Resource
                    App.Current.Resources.MergedDictionaries.Clear();
                    App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                    {
                        Source = new Uri("Theme" + ConfigureWindow.theme + ".xaml", UriKind.RelativeOrAbsolute)
                    });
                }

                // -------------------------
                // Log Text Theme Color
                // -------------------------
                if (ConfigureWindow.theme == "Axiom")
                {
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8")); // Actions
                }
                else if (ConfigureWindow.theme == "FFmpeg")
                {
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cb85c")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cb85c")); // Actions
                }
                else if (ConfigureWindow.theme == "Cyberpunk")
                {
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9f3ed2")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9380fd")); // Actions
                }
                else if (ConfigureWindow.theme == "Onyx")
                {
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#777777")); // Actions
                }
                else if (ConfigureWindow.theme == "Circuit")
                {
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ad8a4a")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2ebf93")); // Actions
                }
                else if (ConfigureWindow.theme == "Prelude")
                {
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#777777")); // Actions
                }
                else if (ConfigureWindow.theme == "System")
                {
                    Log.ConsoleDefault = Brushes.White; // Default
                    Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2")); // Titles
                    Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
                    Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
                    Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8")); // Actions
                }

                // -------------------------
                // Debug Text Theme Color
                // -------------------------
                if (ConfigureWindow.theme == "Axiom")
                {
                    DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2"));
                    DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8"));
                    DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                }
                else if (ConfigureWindow.theme == "FFmpeg")
                {
                    DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#878787"));
                    DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cb85c"));
                    DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                }
                else if (ConfigureWindow.theme == "Cyberpunk")
                {
                    DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9a989c"));
                    DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9f3ed2"));
                    DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                }
                else if (ConfigureWindow.theme == "Onyx")
                {
                    DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EEEEEE"));
                    DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                    DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                }
                else if (ConfigureWindow.theme == "Circuit")
                {
                    DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ad8a4a"));
                    DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2ebf93"));
                    DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                }
                else if (ConfigureWindow.theme == "Prelude")
                {
                    DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ad8a4a"));
                    DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2ebf93"));
                    DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                }
                else if (ConfigureWindow.theme == "System")
                {
                    DebugConsole.Heading = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2"));
                    DebugConsole.Variable = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8"));
                    DebugConsole.Value = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
                }
            }
            catch
            {
            
            }
        }


        /// <summary>
        /// Load FFmpeg Path
        /// </summary>
        public static void LoadFFmpegPath(ConfigureWindow configurewindow)
        {
            // --------------------------------------------------
            // Safeguard Against Corrupt Saved Settings
            // --------------------------------------------------
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                if (string.IsNullOrEmpty(Settings.Default["ffmpegPath"].ToString()))
                {
                    ConfigureWindow.ffmpegPath = "<auto>";

                    // Set ComboBox if Configure Window is Open
                    if (configurewindow != null)
                    {
                        configurewindow.textBoxFFmpegPathConfig.Text = ConfigureWindow.ffmpegPath;
                    }

                    // Save for next launch
                    Settings.Default["ffmpegPath"] = ConfigureWindow.ffmpegPath;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Settings.Default["ffmpegPath"].ToString())) // null check
                {
                    ConfigureWindow.ffmpegPath = Settings.Default["ffmpegPath"].ToString();

                    // Set ComboBox if Configure Window is Open
                    if (configurewindow != null)
                    {
                        configurewindow.textBoxFFmpegPathConfig.Text = Settings.Default["ffmpegPath"].ToString();
                    }
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// Load FFprobe Path
        /// </summary>
        public static void LoadFFprobePath(ConfigureWindow configurewindow)
        {
            // --------------------------------------------------
            // Safeguard Against Corrupt Saved Settings
            // --------------------------------------------------
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                if (string.IsNullOrEmpty(Settings.Default["ffprobePath"].ToString()))
                {
                    ConfigureWindow.ffprobePath = "<auto>";
                    
                    // Set ComboBox if Configure Window is Open
                    if (configurewindow != null)
                    {
                        configurewindow.textBoxFFprobePathConfig.Text = ConfigureWindow.ffprobePath;
                    }

                    // Save for next launch
                    Settings.Default["ffprobePath"] = ConfigureWindow.ffprobePath;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Settings.Default["ffprobePath"].ToString())) // null check
                {
                    ConfigureWindow.ffprobePath = Settings.Default["ffprobePath"].ToString();

                    // Set ComboBox if Configure Window is Open
                    if (configurewindow != null)
                    {
                        configurewindow.textBoxFFprobePathConfig.Text = Settings.Default["ffprobePath"].ToString();
                    }
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// Load Log Checkbox
        /// </summary>
        public static void LoadLogCheckbox(ConfigureWindow configurewindow)
        {
            // --------------------------------------------------
            // Safeguard Against Corrupt Saved Settings
            // --------------------------------------------------
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                if (string.IsNullOrEmpty(Convert.ToString(Settings.Default.checkBoxLog)))
                {
                    ConfigureWindow.logEnable = false;

                    // Set ComboBox if Configure Window is Open
                    if (configurewindow != null)
                    {
                        configurewindow.checkBoxLogConfig.IsChecked = false;
                    }

                    // Save for next launch
                    Settings.Default["checkBoxLog"] = ConfigureWindow.logEnable;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Convert.ToString(Settings.Default.checkBoxLog)))
                {
                    ConfigureWindow.logEnable = Convert.ToBoolean(Settings.Default.logEnable);

                    // Set ComboBox if Configure Window is Open
                    if (configurewindow != null)
                    {
                        configurewindow.checkBoxLogConfig.IsChecked = Convert.ToBoolean(Settings.Default.checkBoxLog);
                    }
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// Load Log Path
        /// </summary>
        public static void LoadLogPath(ConfigureWindow configurewindow)
        {
            // --------------------------------------------------
            // Safeguard Against Corrupt Saved Settings
            // --------------------------------------------------
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                if (string.IsNullOrEmpty(Settings.Default["logPath"].ToString()))
                {
                    ConfigureWindow.logPath = string.Empty; 
                    
                    // Set ComboBox if Configure Window is Open
                    if (configurewindow != null)
                    {
                        configurewindow.textBoxLogConfig.Text = ConfigureWindow.logPath;
                    }

                    // Save for next launch
                    Settings.Default["logPath"] = ConfigureWindow.logPath;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                if (!string.IsNullOrEmpty(Settings.Default["logPath"].ToString())) // null check
                {
                    ConfigureWindow.logPath = Settings.Default["logPath"].ToString();

                    // Set ComboBox if Configure Window is Open
                    if (configurewindow != null)
                    {
                        configurewindow.textBoxLogConfig.Text = Settings.Default["logPath"].ToString();
                    }
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// Load Threads
        /// </summary>
        public static void LoadThreads(ConfigureWindow configurewindow)
        {
            // --------------------------------------------------
            // Safeguard Against Corrupt Saved Settings
            // --------------------------------------------------
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                if (string.IsNullOrEmpty(Settings.Default["threads"].ToString()))
                {
                    ConfigureWindow.threads = "all";

                    // Set ComboBox if Configure Window is Open
                    if (configurewindow != null)
                    {
                        configurewindow.cboThreads.SelectedItem = ConfigureWindow.threads;
                    }

                    // Save for next launch
                    Settings.Default["threads"] = ConfigureWindow.threads;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Settings.Default["threads"].ToString())) // null check
                {
                    ConfigureWindow.threads = Settings.Default["threads"].ToString();

                    // Set ComboBox if Configure Window is Open
                    if (configurewindow != null)
                    {
                        configurewindow.cboThreads.SelectedItem = Settings.Default["threads"].ToString();
                    }
                }
            }
            catch
            {

            }
        }




        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Control Methods
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        /// <summary>
        ///    Window Loaded
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        ///    Window Close
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //e.Cancel = true;
            //this.Close();
            //Settings.Default.Reload();
        }

        // --------------------------------------------------
        // FFmpeg Folder Browser Dialog
        // --------------------------------------------------
        public void FFmpegFolderBrowser() // Method
        {
            var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = OpenFileDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                textBoxFFmpegPathConfig.Text = OpenFileDialog.FileName;

                // Set the ffmpegPath string
                ffmpegPath = textBoxFFmpegPathConfig.Text;

                // Save 7-zip Path for next launch
                Settings.Default["ffmpegPath"] = textBoxFFmpegPathConfig.Text;
                Settings.Default.Save();
                Settings.Default.Reload();
            }
        }


        // --------------------------------------------------
        // FFprobe Folder Browser Dialog
        // --------------------------------------------------
        public void FFprobeFolderBrowser() // Method
        {
            var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = OpenFileDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                textBoxFFprobePathConfig.Text = OpenFileDialog.FileName;

                // Set the ffprobePath string
                ffprobePath = textBoxFFprobePathConfig.Text;

                // Save WinRAR Path for next launch
                Settings.Default["ffprobePath"] = textBoxFFprobePathConfig.Text;
                Settings.Default.Save();
                Settings.Default.Reload();
            }
        }


        // --------------------------------------------------
        // Log Folder Browser Dialog 
        // --------------------------------------------------
        public void logFolderBrowser() // Method
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                textBoxLogConfig.Text = folderBrowserDialog.SelectedPath;

                // Add backslash if missing
                textBoxLogConfig.Text = textBoxLogConfig.Text.TrimEnd('\\') + @"\";

                // Set the ffprobePath string
                logPath = textBoxLogConfig.Text;


                // Save FFmpeg Path for next launch
                Settings.Default["logPath"] = textBoxLogConfig.Text;
                Settings.Default.Save();
                Settings.Default.Reload();
            }
        }



        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------
        // Controls
        // --------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------

        // --------------------------------------------------
        // FFmpeg Textbox Click
        // --------------------------------------------------
        private void textBoxFFmpegPathConfig_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            FFmpegFolderBrowser();
        }


        // --------------------------------------------------
        // FFmpeg Textbox (Text Changed)
        // --------------------------------------------------
        private void textBoxFFmpegPathConfig_TextChanged(object sender, TextChangedEventArgs e)
        {
            // dont use
        }


        // --------------------------------------------------
        // FFmpeg Auto Path Button (On Click)
        // --------------------------------------------------
        private void buttonFFmpegAuto_Click(object sender, RoutedEventArgs e)
        {
            // Set the ffmpegPath string
            ffmpegPath = "<auto>";

            // Display Folder Path in Textbox
            textBoxFFmpegPathConfig.Text = "<auto>";

            // FFmpeg Path path for next launch
            Settings.Default["ffmpegPath"] = "<auto>";
            Settings.Default.Save();
            Settings.Default.Reload();
        }


        // --------------------------------------------------
        // FFprobe Textbox Click
        // --------------------------------------------------
        private void textBoxFFprobePathConfig_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            FFprobeFolderBrowser();
        }


        // --------------------------------------------------
        // FFprobe Textbox (Text Changed)
        // --------------------------------------------------
        private void textBoxFFprobePathConfig_TextChanged(object sender, TextChangedEventArgs e)
        {
            // dont use
        }


        // --------------------------------------------------
        // FFprobe Auto Path Button (On Click)
        // --------------------------------------------------
        private void buttonFFprobeAuto_Click(object sender, RoutedEventArgs e)
        {
            // Set the ffprobePath string
            ffprobePath = "<auto>"; //<auto>

            // Display Folder Path in Textbox
            textBoxFFprobePathConfig.Text = "<auto>";

            // Save 7-zip Path path for next launch
            Settings.Default["ffprobePath"] = "<auto>";
            Settings.Default.Save();
            Settings.Default.Reload();
        }


        // --------------------------------------------------
        // Log Checkbox (Checked)
        // --------------------------------------------------
        private void checkBoxLogConfig_Checked(object sender, RoutedEventArgs e)
        {
            // Enable the Log
            logEnable = true;

            // -------------------------
            // Prevent Loading Corrupt App.Config
            // -------------------------
            try
            {
                // must be done this way or you get "convert object to bool error"
                if (checkBoxLogConfig.IsChecked == true)
                {
                    // Save Checkbox Settings
                    Settings.Default.checkBoxLog = true;
                    Settings.Default.Save();
                    Settings.Default.Reload();

                    // Save Log Enable Settings
                    Settings.Default.logEnable = true;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
                if (checkBoxLogConfig.IsChecked == false)
                {
                    // Save Checkbox Settings
                    Settings.Default.checkBoxLog = false;
                    Settings.Default.Save();
                    Settings.Default.Reload();

                    // Save Log Enable Settings
                    Settings.Default.logEnable = false;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
            }
            catch (ConfigurationErrorsException ex)
            {
                // Delete Old App.Config
                string filename = ex.Filename;

                if (File.Exists(filename) == true)
                {
                    File.Delete(filename);
                    Properties.Settings.Default.Upgrade();
                    // Properties.Settings.Default.Reload();
                }
                else
                {

                }
            }

        }


        // --------------------------------------------------
        // Log Checkbox (Unchecked)
        // --------------------------------------------------
        private void checkBoxLogConfig_Unchecked(object sender, RoutedEventArgs e)
        {
            // Disable the Log
            logEnable = false;

            // -------------------------
            // Prevent Loading Corrupt App.Config
            // -------------------------
            try
            {
                // must be done this way or you get "convert object to bool error"
                if (checkBoxLogConfig.IsChecked == true)
                {
                    // Save Checkbox Settings
                    Settings.Default.checkBoxLog = true;
                    Settings.Default.Save();
                    Settings.Default.Reload();

                    // Save Log Enable Settings
                    Settings.Default.logEnable = true;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
                if (checkBoxLogConfig.IsChecked == false)
                {
                    // Save Checkbox Settings
                    Settings.Default.checkBoxLog = false;
                    Settings.Default.Save();
                    Settings.Default.Reload();

                    // Save Log Enable Settings
                    Settings.Default.logEnable = false;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
            }
            catch (ConfigurationErrorsException ex)
            {
                // Delete Old App.Config
                string filename = ex.Filename;

                if (File.Exists(filename) == true)
                {
                    File.Delete(filename);
                    Properties.Settings.Default.Upgrade();
                    // Properties.Settings.Default.Reload();
                }
                else
                {

                }
            }
        }


        // --------------------------------------------------
        // Log Textbox (On Click)
        // --------------------------------------------------
        private void textBoxLogConfig_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            logFolderBrowser();
        }

        // --------------------------------------------------
        // Log Auto Path Button (On Click)
        // --------------------------------------------------
        private void buttonLogAuto_Click(object sender, RoutedEventArgs e)
        {
            // Uncheck Log Checkbox
            checkBoxLogConfig.IsChecked = false;

            // Clear Path in Textbox
            textBoxLogConfig.Text = string.Empty;

            // Set the logPath string
            logPath = string.Empty;

            // Save Log Path path for next launch
            Settings.Default["logPath"] = string.Empty;
            Settings.Default.Save();
            Settings.Default.Reload();
        }


        // --------------------------------------------------
        // Thread Select ComboBox
        // --------------------------------------------------
        private void threadSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Custom ComboBox Editable
            if ((string)cboThreads.SelectedItem == "Custom" || cboThreads.SelectedValue == null)
            {
                cboThreads.IsEditable = true;
            }

            // Other Items Disable Editable
            if ((string)cboThreads.SelectedItem != "Custom" && cboThreads.SelectedValue != null)
            {
                cboThreads.IsEditable = false;
            }

            // Maintain Editable Combobox while typing
            if (cboThreads.IsEditable == true)
            {
                cboThreads.IsEditable = true;

                // Clear Custom Text
                cboThreads.SelectedIndex = -1;
            }

            // Set the threads to pass to MainWindow
            threads = cboThreads.SelectedItem.ToString();

            // Save Thread Number for next launch
            //Settings.Default["cboThreads"] = cboThreads.SelectedItem.ToString();
            Settings.Default["threads"] = cboThreads.SelectedItem.ToString();
            Settings.Default.Save();
            Settings.Default.Reload();
        }
        // --------------------------------------------------
        // Thread Select ComboBox - Allow Only Numbers
        // --------------------------------------------------
        private void threadSelect_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers or Backspace
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }

        // --------------------------------------------------
        // Theme Select ComboBox
        // --------------------------------------------------
        private void themeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConfigureWindow.theme = cboTheme.SelectedItem.ToString();

            // Change Theme Resource
            App.Current.Resources.MergedDictionaries.Clear();
            App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("Theme" + theme + ".xaml", UriKind.RelativeOrAbsolute)
            });

            // Save Theme for next launch
            Settings.Default["Theme"] = cboTheme.SelectedItem.ToString();
            Settings.Default.Save();
            Settings.Default.Reload();
        }


        // --------------------------------------------------
        // Reset Saved Settings Button
        // --------------------------------------------------
        private void buttonClearAllSavedSettings_Click(object sender, RoutedEventArgs e)
        {
            // Revert FFmpeg
            textBoxFFmpegPathConfig.Text = "<auto>";
            ffmpegPath = textBoxFFmpegPathConfig.Text;

            // Revert FFprobe
            textBoxFFprobePathConfig.Text = "<auto>";
            ffprobePath = textBoxFFprobePathConfig.Text;

            // Revert Log
            checkBoxLogConfig.IsChecked = false;
            textBoxLogConfig.Text = string.Empty;
            logPath = string.Empty;

            // Revert Threads
            cboThreads.SelectedItem = "all";
            threads = string.Empty;

            // Save Current Window Location
            // Prevents MainWindow from moving to Top 0 Left 0 while running
            double left = mainwindow.Left;
            double top = mainwindow.Top;

            // Reset AppData Settings
            Settings.Default.Reset();
            Settings.Default.Reload();

            // Set Window Location
            mainwindow.Left = left;
            mainwindow.Top = top;
        }


        // --------------------------------------------------
        // Delete Saved Settings Button
        // --------------------------------------------------
        private void buttonDeleteSettings_Click(object sender, RoutedEventArgs e)
        {
            string userProfile = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%");
            string appDataPath = "\\AppData\\Local\\Axiom";

            // Check if Directory Exists
            if (Directory.Exists(userProfile + appDataPath))
            {
                // Show Yes No Window
                System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(
                    "Delete " + userProfile + appDataPath, "Delete Directory Confirm", System.Windows.Forms.MessageBoxButtons.YesNo);
                // Yes
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    // Delete leftover 2 Pass Logs in Program's folder and Input Files folder
                    using (Process delete = new Process())
                    {
                        delete.StartInfo.UseShellExecute = false;
                        delete.StartInfo.CreateNoWindow = false;
                        delete.StartInfo.RedirectStandardOutput = true;
                        delete.StartInfo.FileName = "cmd.exe";
                        delete.StartInfo.Arguments = "/c RD /Q /S " + "\"" + userProfile + appDataPath;
                        delete.Start();
                        delete.WaitForExit();
                        //delete.Close();
                    }
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
                MessageBox.Show("No Previous Settings Found.");
            }
        }


    }

}