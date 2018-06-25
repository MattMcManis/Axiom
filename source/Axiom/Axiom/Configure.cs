/* ----------------------------------------------------------------------
Axiom UI
Copyright(C) 2017, 2018 Matt McManis
http://github.com/MattMcManis/Axiom
http://axiomui.github.io
axiom.interface @gmail.com

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
using System.Windows;
using System.Windows.Media;
// Disable XML Comment warnings
#pragma warning disable 1591

namespace Axiom
{
    public partial class Configure
    {
        //private MainWindow mainwindow;

        // --------------------------------------------------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------------------------------------------------
        public static string theme; // Set Theme (public - pass data)
        public static string ffmpegPath; // Config Settings Path (public - pass data)
        public static string ffprobePath; // Config Settings Path (public - pass data)
        public static string logPath; // output.log Config Settings Path (public - pass data)
        public static bool logEnable; //checkBoxLogConfig, Enable or Disable Log, true or false - (public - pass data)
        public static string threads; // Set FFmpeg -threads (public - pass data)
        public static string maxthreads; // All CPU Threads


        /// <summary>
        /// Load Theme
        /// </summary>
        //public static void LoadTheme(MainWindow mainwindow)
        //{
        //
        //}


        /// <summary>
        /// Load FFmpeg Path
        /// </summary>
        public static void LoadFFmpegPath(MainWindow mainwindow)
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
                    mainwindow.textBoxFFmpegPathConfig.Text = Configure.ffmpegPath;

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
                    mainwindow.textBoxFFmpegPathConfig.Text = Settings.Default["ffmpegPath"].ToString();
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// Load FFprobe Path
        /// </summary>
        public static void LoadFFprobePath(MainWindow mainwindow)
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
                    mainwindow.textBoxFFprobePathConfig.Text = Configure.ffprobePath;

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
                    mainwindow.textBoxFFprobePathConfig.Text = Settings.Default["ffprobePath"].ToString();
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// Load Log Checkbox
        /// </summary>
        public static void LoadLogCheckbox(MainWindow mainwindow)
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
                    mainwindow.checkBoxLogConfig.IsChecked = false;

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
                    mainwindow.checkBoxLogConfig.IsChecked = Convert.ToBoolean(Settings.Default.checkBoxLog);
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// Load Log Path
        /// </summary>
        public static void LoadLogPath(MainWindow mainwindow)
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
                    mainwindow.textBoxLogConfig.Text = Configure.logPath;

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
                    mainwindow.textBoxLogConfig.Text = Settings.Default["logPath"].ToString();
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// Load Threads
        /// </summary>
        public static void LoadThreads(MainWindow mainwindow)
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
                    Configure.threads = "optimal";

                    // Set ComboBox if Configure Window is Open
                    mainwindow.cboThreads.SelectedItem = Configure.threads;

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
                    mainwindow.cboThreads.SelectedItem = Settings.Default["threads"].ToString();
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
        // FFmpeg Folder Browser Dialog
        // --------------------------------------------------
        public static void FFmpegFolderBrowser(MainWindow mainwindow) // Method
        {
            var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = OpenFileDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                mainwindow.textBoxFFmpegPathConfig.Text = OpenFileDialog.FileName;

                // Set the ffmpegPath string
                ffmpegPath = mainwindow.textBoxFFmpegPathConfig.Text;

                // Save 7-zip Path for next launch
                Settings.Default["ffmpegPath"] = mainwindow.textBoxFFmpegPathConfig.Text;
                Settings.Default.Save();
                Settings.Default.Reload();
            }
        }


        // --------------------------------------------------
        // FFprobe Folder Browser Dialog
        // --------------------------------------------------
        public static void FFprobeFolderBrowser(MainWindow mainwindow) // Method
        {
            var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = OpenFileDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                mainwindow.textBoxFFprobePathConfig.Text = OpenFileDialog.FileName;

                // Set the ffprobePath string
                ffprobePath = mainwindow.textBoxFFprobePathConfig.Text;

                // Save WinRAR Path for next launch
                Settings.Default["ffprobePath"] = mainwindow.textBoxFFprobePathConfig.Text;
                Settings.Default.Save();
                Settings.Default.Reload();
            }
        }


        // --------------------------------------------------
        // Log Folder Browser Dialog 
        // --------------------------------------------------
        public static void logFolderBrowser(MainWindow mainwindow) // Method
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                mainwindow.textBoxLogConfig.Text = folderBrowserDialog.SelectedPath;

                // Add backslash if missing
                mainwindow.textBoxLogConfig.Text = mainwindow.textBoxLogConfig.Text.TrimEnd('\\') + @"\";

                // Set the ffprobePath string
                logPath = mainwindow.textBoxLogConfig.Text;


                // Save FFmpeg Path for next launch
                Settings.Default["logPath"] = mainwindow.textBoxLogConfig.Text;
                Settings.Default.Save();
                Settings.Default.Reload();
            }
        }
    }
}
