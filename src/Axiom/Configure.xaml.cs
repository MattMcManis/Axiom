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

/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017 Matt McManis
http://github.com/MattMcManis/Axiom
http://www.x.co/axiomui
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
        // --------------------------------------------------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------------------------------------------------
        public static string ffmpegPath; // Config Settings Path (public - pass data)
        public static string ffprobePath; // Config Settings Path (public - pass data)
        public static string logPath; // output.log Config Settings Path (public - pass data)
        public static bool logEnable; //checkBoxLogConfig, Enable or Disable Log, true or false - (public - pass data)
        public static string threads; // Set FFmpeg -threads (public - pass data)

        private MainWindow mainwindow;

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
            // Combobox Item Background Colors
            // --------------------------------------------------
            Brush DarkBlue = (SolidColorBrush)(new BrushConverter().ConvertFrom("#1049bb")); //hex color to brush
            cboThreads.Resources.Add(SystemColors.WindowBrushKey, DarkBlue);


            // --------------------------------------------------
            // Load From Saved Settings
            // --------------------------------------------------
            // -------------------------
            // FFmpeg Path
            // -------------------------        
            // Safeguard Against Corrupt Saved Settings
            try
            {
                // First time use
                if (string.IsNullOrEmpty(Settings.Default["ffmpegPath"].ToString()))
                {
                    ffmpegPath = "<auto>"; 
                    textBoxFFmpegPathConfig.Text = ffmpegPath;
                }
                // Load FFmpeg Path from saved settings
                else if (Settings.Default["ffmpegPath"].ToString() != "<auto>" && !string.IsNullOrEmpty(Settings.Default["ffmpegPath"].ToString())) // auto/null check
                {
                    textBoxFFmpegPathConfig.Text = Settings.Default["ffmpegPath"].ToString();
                    ffmpegPath = Settings.Default["ffmpegPath"].ToString();
                }
            }
            catch
            {

            }


            // -------------------------
            // PProbe Path
            // -------------------------
            // Safeguard Against Corrupt Saved Settings
            try
            {
                // First time use
                if (string.IsNullOrEmpty(Settings.Default["ffprobePath"].ToString()))
                {
                    ffprobePath = "<auto>";
                    textBoxFFprobePathConfig.Text = ffprobePath;
                }
                // Load PProbe Path from saved settings
                else if (Settings.Default["ffprobePath"].ToString() != "<auto>" && !string.IsNullOrEmpty(Settings.Default["ffprobePath"].ToString())) // auto/null check
                {
                    textBoxFFprobePathConfig.Text = Settings.Default["ffprobePath"].ToString();
                    ffprobePath = Settings.Default["ffprobePath"].ToString();
                }
            }
            catch
            {

            }

            // -------------------------
            // Log CheckBox
            // -------------------------
            // Safeguard Against Corrupt Saved Settings
            try
            {
                // First time use
                if (string.IsNullOrEmpty(Convert.ToString(Settings.Default.checkBoxLog)))
                {
                    checkBoxLogConfig.IsChecked = false;
                }
                // Load Log Checkbox Toggle from saved settings
                else if (!string.IsNullOrEmpty(Convert.ToString(Settings.Default.checkBoxLog)))
                {
                    checkBoxLogConfig.IsChecked = Convert.ToBoolean(Settings.Default.checkBoxLog);
                }
            }
            catch
            {

            }

            // -------------------------
            // Log Path
            // -------------------------
            // Safeguard Against Corrupt Saved Settings
            try
            {
                // First time use
                if (string.IsNullOrEmpty(Settings.Default["logPath"].ToString()))
                {
                    logPath = ""; // First time use
                    textBoxLogConfig.Text = logPath; // First time use
                }
                // Load Log Path from saved settings
                if (!string.IsNullOrEmpty(Settings.Default["logPath"].ToString())) // null check
                {
                    textBoxLogConfig.Text = Settings.Default["logPath"].ToString();
                    logPath = Settings.Default["logPath"].ToString();
                }
            }
            catch
            {

            }

            // -------------------------
            // Threads CombBox
            // -------------------------
            // Load Threads from saved settings
            // Safeguard Against Corrupt Saved Settings
            try
            {
                // First time use
                if (string.IsNullOrEmpty(Settings.Default["threads"].ToString()))
                {
                    cboThreads.SelectedItem = "all";
                    threads = cboThreads.SelectedItem.ToString(); 
                }
                // Load Threads ComboBox from Saved Settings
                else if (!string.IsNullOrEmpty(Settings.Default["threads"].ToString())) // null check
                {
                    cboThreads.SelectedItem = Settings.Default["threads"].ToString();
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
            ffmpegPath = textBoxFFmpegPathConfig.Text; //<auto>

            // FFmpeg Path path for next launch
            Settings.Default["ffmpegPath"] = textBoxFFmpegPathConfig.Text;
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
            ffprobePath = textBoxFFprobePathConfig.Text; //<auto>

            // Save 7-zip Path path for next launch
            Settings.Default["ffprobePath"] = textBoxFFprobePathConfig.Text;
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