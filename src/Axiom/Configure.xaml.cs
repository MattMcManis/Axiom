using Axiom.Properties;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
// Disable XML Comment warnings
#pragma warning disable 1591

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

namespace Axiom
{
    /// <summary>
    /// Interaction logic for Configure.xaml
    /// </summary>
    public partial class Configure : Window
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

        public Configure()
        {
            // Configure, dont remove
        }

        // --------------------------------------------------------------------------------------------------------
        // Window
        // --------------------------------------------------------------------------------------------------------
        public Configure(MainWindow mainwindow) // Pass Constructor from MainWindow
        {
            InitializeComponent();

            // Set Min/Max Width/Height to prevent Tablets maximizing
            this.MinWidth = 450;
            this.MinHeight = 200;
            this.MaxWidth = 450;
            this.MaxHeight = 200;

            // Pass Constructor from MainWindow
            this.mainwindow = mainwindow;

            // --------------------------------------------------
            // Load From Saved Settings
            // --------------------------------------------------
            // Theme CombBox
            Configure.ConfigTheme(this);

            // FFmpeg Path 
            Configure.ConfigFFmpegPath(this);

            // PProbe Path
            Configure.ConfigFFprobePath(this);

            // Log CheckBox
            Configure.ConfigLogCheckbox(this);

            // Log Path
            Configure.ConfigLogPath(this);

            // Threads CombBox
            Configure.ConfigThreads(this);
        }


        /// <summary>
        /// Check if Window Is Open
        /// </summary>
        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }


        /// <summary>
        /// Theme CombBox
        /// </summary>
        public static void ConfigTheme(Configure configure)
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
                    Configure.theme = "Axiom";

                    // Set ComboBox if Configure Window is Open
                    if (configure != null)
                    {
                        configure.cboTheme.SelectedItem = "Axiom";
                    }

                    // Save Theme for next launch
                    Settings.Default["Theme"] = Configure.theme;
                    Settings.Default.Save();

                    // Change Theme Resource
                    App.Current.Resources.MergedDictionaries.Clear();
                    App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                    {
                        Source = new Uri("Theme" + Configure.theme + ".xaml", UriKind.RelativeOrAbsolute)
                    });
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Settings.Default["Theme"].ToString())) // null check
                {
                    Configure.theme = Settings.Default["Theme"].ToString();

                    // Set ComboBox if Configure Window is Open
                    if (configure != null)
                    {
                        configure.cboTheme.SelectedItem = Settings.Default["Theme"].ToString();
                    }

                    // Change Theme Resource
                    App.Current.Resources.MergedDictionaries.Clear();
                    App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                    {
                        Source = new Uri("Theme" + Configure.theme + ".xaml", UriKind.RelativeOrAbsolute)
                    });
                }
            }
            catch
            {
            
            }
        }


        /// <summary>
        /// FFmpeg Path
        /// </summary>
        public static void ConfigFFmpegPath(Configure configure)
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
                    Configure.ffmpegPath = "<auto>";

                    // Set ComboBox if Configure Window is Open
                    if (configure != null)
                    {
                        configure.textBoxFFmpegPathConfig.Text = Configure.ffmpegPath;
                    }

                    // Save for next launch
                    Settings.Default["ffmpegPath"] = Configure.ffmpegPath;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Settings.Default["ffmpegPath"].ToString())) // null check
                {
                    Configure.ffmpegPath = Settings.Default["ffmpegPath"].ToString();

                    // Set ComboBox if Configure Window is Open
                    if (configure != null)
                    {
                        configure.textBoxFFmpegPathConfig.Text = Settings.Default["ffmpegPath"].ToString();
                    }
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// FFprobe Path
        /// </summary>
        public static void ConfigFFprobePath(Configure configure)
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
                    Configure.ffprobePath = "<auto>";
                    
                    // Set ComboBox if Configure Window is Open
                    if (configure != null)
                    {
                        configure.textBoxFFprobePathConfig.Text = Configure.ffprobePath;
                    }

                    // Save for next launch
                    Settings.Default["ffprobePath"] = Configure.ffprobePath;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Settings.Default["ffprobePath"].ToString())) // null check
                {
                    Configure.ffprobePath = Settings.Default["ffprobePath"].ToString();

                    // Set ComboBox if Configure Window is Open
                    if (configure != null)
                    {
                        configure.textBoxFFprobePathConfig.Text = Settings.Default["ffprobePath"].ToString();
                    }
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// Log Checkbox
        /// </summary>
        public static void ConfigLogCheckbox(Configure configure)
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
                    Configure.logEnable = false;

                    // Set ComboBox if Configure Window is Open
                    if (configure != null)
                    {
                        configure.checkBoxLogConfig.IsChecked = false;
                    }

                    // Save for next launch
                    Settings.Default["checkBoxLog"] = Configure.logEnable;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Convert.ToString(Settings.Default.checkBoxLog)))
                {
                    Configure.logEnable = Convert.ToBoolean(Settings.Default.logEnable);

                    // Set ComboBox if Configure Window is Open
                    if (configure != null)
                    {
                        configure.checkBoxLogConfig.IsChecked = Convert.ToBoolean(Settings.Default.checkBoxLog);
                    }
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// Log Path
        /// </summary>
        public static void ConfigLogPath(Configure configure)
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
                    Configure.logPath = string.Empty; 
                    
                    // Set ComboBox if Configure Window is Open
                    if (configure != null)
                    {
                        configure.textBoxLogConfig.Text = Configure.logPath;
                    }

                    // Save for next launch
                    Settings.Default["logPath"] = Configure.logPath;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                if (!string.IsNullOrEmpty(Settings.Default["logPath"].ToString())) // null check
                {
                    Configure.logPath = Settings.Default["logPath"].ToString();

                    // Set ComboBox if Configure Window is Open
                    if (configure != null)
                    {
                        configure.textBoxLogConfig.Text = Settings.Default["logPath"].ToString();
                    }
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// Threads
        /// </summary>
        public static void ConfigThreads(Configure configure)
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
                    Configure.threads = "all";

                    // Set ComboBox if Configure Window is Open
                    if (configure != null)
                    {
                        configure.cboThreads.SelectedItem = Configure.threads;
                    }

                    // Save for next launch
                    Settings.Default["threads"] = Configure.threads;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Settings.Default["threads"].ToString())) // null check
                {
                    Configure.threads = Settings.Default["threads"].ToString();

                    // Set ComboBox if Configure Window is Open
                    if (configure != null)
                    {
                        configure.cboThreads.SelectedItem = Settings.Default["threads"].ToString();
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

        // --------------------------------------------------
        // Close All
        // --------------------------------------------------
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Close();
            Settings.Default.Reload();
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
            // Display Folder Path in Textbox
            textBoxFFmpegPathConfig.Text = "<auto>";

            // Set the ffmpegPath string
            ffmpegPath = "<auto>";

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
            // Display Folder Path in Textbox
            textBoxFFprobePathConfig.Text = "<auto>";

            // Set the ffprobePath string
            ffprobePath = "<auto>"; //<auto>

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
                    // you could optionally restart the app instead
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
                    // you could optionally restart the app instead
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
            string theme = cboTheme.SelectedItem.ToString();

            App.Current.Resources.MergedDictionaries.Clear();
            App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("Theme" + theme + ".xaml", UriKind.RelativeOrAbsolute) });

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

            Settings.Default.Reset();
            Settings.Default.Reload();
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
                System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Delete " + userProfile + appDataPath, "Delete Directory Confirm", System.Windows.Forms.MessageBoxButtons.YesNo);
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
                System.Windows.MessageBox.Show("No Previous Settings Found.");
            }
        }


    }

}