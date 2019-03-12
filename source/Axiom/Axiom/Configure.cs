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
        public static string ffmpegPath; // Config Settings Path
        public static string ffprobePath; // Config Settings Path
        public static string ffplayPath; // Config Settings Path
        public static string logPath; // output.log Config Settings Path
        public static bool logEnable; //checkBoxLogConfig, Enable or Disable Log, true or false
        public static string threads; // Set FFmpeg -threads
        public static string maxthreads; // All CPU Threads


        /// <summary>
        ///     Load FFmpeg Path
        /// </summary>
        public static void LoadFFmpegPath(ViewModel vm)
        {
            // --------------------------------------------------
            // Safeguard Against Corrupt Saved Settings
            // --------------------------------------------------
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                if (string.IsNullOrEmpty(Settings.Default.FFmpegPath.ToString()))
                {
                    ffmpegPath = "<auto>";

                    // Set ComboBox if Configure Window is Open
                    vm.FFmpegPath_Text = ffmpegPath;

                    // Save for next launch
                    Settings.Default.FFmpegPath = ffmpegPath;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Settings.Default.FFmpegPath.ToString())) // null check
                {
                    ffmpegPath = Settings.Default.FFmpegPath.ToString();

                    // Set ComboBox if Configure Window is Open
                    vm.FFmpegPath_Text = Settings.Default.FFmpegPath.ToString();
                }
            }
            catch
            {

            }
        }


        /// <summary>
        ///     Load FFprobe Path
        /// </summary>
        public static void LoadFFprobePath(ViewModel vm)
        {
            // --------------------------------------------------
            // Safeguard Against Corrupt Saved Settings
            // --------------------------------------------------
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                if (string.IsNullOrEmpty(Settings.Default.FFprobePath.ToString()))
                {
                    ffprobePath = "<auto>";

                    // Set ComboBox if Configure Window is Open
                    vm.FFprobePath_Text = ffprobePath;

                    // Save for next launch
                    Settings.Default.FFprobePath = ffprobePath;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Settings.Default.FFprobePath.ToString())) // null check
                {
                    ffprobePath = Settings.Default.FFprobePath.ToString();

                    // Set ComboBox if Configure Window is Open
                    vm.FFprobePath_Text = Settings.Default.FFprobePath.ToString();
                }
            }
            catch
            {

            }
        }


        /// <summary>
        ///     Load FFplay Path
        /// </summary>
        public static void LoadFFplayPath(ViewModel vm)
        {
            // --------------------------------------------------
            // Safeguard Against Corrupt Saved Settings
            // --------------------------------------------------
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                if (string.IsNullOrEmpty(Settings.Default.FFplayPath.ToString()))
                {
                    ffplayPath = "<auto>";

                    // Set ComboBox if Configure Window is Open
                    vm.FFplayPath_Text = ffplayPath;

                    // Save for next launch
                    Settings.Default.FFplayPath = ffplayPath;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Settings.Default.FFplayPath.ToString())) // null check
                {
                    ffplayPath = Settings.Default.FFplayPath.ToString();

                    // Set ComboBox if Configure Window is Open
                    vm.FFplayPath_Text = Settings.Default.FFplayPath.ToString();
                }
            }
            catch
            {

            }
        }

        /// <summary>
        ///     Load Log Checkbox
        /// </summary>
        public static void LoadLogCheckbox(ViewModel vm)
        {
            // --------------------------------------------------
            // Safeguard Against Corrupt Saved Settings
            // --------------------------------------------------
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                if (string.IsNullOrEmpty(Convert.ToString(Settings.Default.Log_IsChecked)))
                {
                    logEnable = false;

                    // Set ComboBox if Configure Window is Open
                    vm.LogCheckBox_IsChecked = false;

                    // Save for next launch
                    Settings.Default.Log_IsChecked = logEnable;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Convert.ToString(Settings.Default.Log_IsChecked)))
                {
                    logEnable = Convert.ToBoolean(Settings.Default.Log_IsChecked);

                    // Set ComboBox if Configure Window is Open
                    vm.LogCheckBox_IsChecked = Convert.ToBoolean(Settings.Default.Log_IsChecked);
                }
            }
            catch
            {

            }
        }


        /// <summary>
        ///     Load Log Path
        /// </summary>
        public static void LoadLogPath(ViewModel vm)
        {
            // --------------------------------------------------
            // Safeguard Against Corrupt Saved Settings
            // --------------------------------------------------
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                if (string.IsNullOrEmpty(Settings.Default.LogPath.ToString()))
                {
                    logPath = string.Empty;

                    // Set ComboBox if Configure Window is Open
                    vm.LogPath_Text = logPath;

                    // Save for next launch
                    Settings.Default.LogPath = logPath;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                if (!string.IsNullOrEmpty(Settings.Default.LogPath.ToString())) // null check
                {
                    logPath = Settings.Default.LogPath.ToString();

                    // Set ComboBox if Configure Window is Open
                    vm.LogPath_Text = Settings.Default.LogPath.ToString();
                }
            }
            catch
            {

            }
        }


        /// <summary>
        ///     Load Threads
        /// </summary>
        public static void LoadThreads(ViewModel vm)
        {
            // --------------------------------------------------
            // Safeguard Against Corrupt Saved Settings
            // --------------------------------------------------
            try
            {
                // --------------------------
                // First time use
                // --------------------------
                if (string.IsNullOrEmpty(Settings.Default.Threads.ToString()))
                {
                    threads = "optimal";

                    // Set ComboBox if Configure Window is Open
                    vm.Threads_SelectedItem = threads;

                    // Save for next launch
                    Settings.Default.Threads = threads;
                    Settings.Default.Save();
                }
                // --------------------------
                // Load Saved Settings Override
                // --------------------------
                else if (!string.IsNullOrEmpty(Settings.Default.Threads.ToString())) // null check
                {
                    threads = Settings.Default.Threads.ToString();

                    // Set ComboBox if Configure Window is Open
                    vm.Threads_SelectedItem = Settings.Default.Threads.ToString();
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
        public static void FFmpegFolderBrowser(ViewModel vm) 
        {
            var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = OpenFileDialog.ShowDialog();

            // Popup Folder Browse Window
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Display Folder Path in Textbox
                vm.FFmpegPath_Text = OpenFileDialog.FileName;

                // Set the ffmpegPath string
                ffmpegPath = vm.FFmpegPath_Text;

                // Save 7-zip Path for next launch
                Settings.Default.FFmpegPath = vm.FFmpegPath_Text;
                Settings.Default.Save();
                Settings.Default.Reload();
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

                // Set the ffprobePath string
                ffprobePath = vm.FFprobePath_Text;

                // Save WinRAR Path for next launch
                Settings.Default.FFprobePath = vm.FFprobePath_Text;
                Settings.Default.Save();
                Settings.Default.Reload();
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

                // Set the ffplayPath string
                ffplayPath = vm.FFplayPath_Text;

                // Save WinRAR Path for next launch
                Settings.Default.FFplayPath = vm.FFplayPath_Text;
                Settings.Default.Save();
                Settings.Default.Reload();
            }
        }


        // --------------------------------------------------
        // Log Folder Browser Dialog 
        // --------------------------------------------------
        public static void logFolderBrowser(ViewModel vm) // Method
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

                // Set the ffprobePath string
                logPath = vm.LogPath_Text;


                // Save FFmpeg Path for next launch
                Settings.Default.LogPath = vm.LogPath_Text;
                Settings.Default.Save();
                Settings.Default.Reload();
            }
        }
    }
}
