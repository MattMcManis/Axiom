/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017, 2018 Matt McManis
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    /// <summary>
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        // Windows AppData Temp Directory
        public static string tempDir = Path.GetTempPath();
        // Axiom Exe Current Directory
        //public static string currentDir = Directory.GetCurrentDirectory().TrimEnd('\\') + @"\";

        // Web Downloads
        public static WebClient wc = new WebClient();
        public static ManualResetEvent waiter = new ManualResetEvent(false); // Download one at a time

        // Progress Label Info
        public static string progressInfo;

        // Unzip CMD Arguments
        public static string extractArgs;


        public UpdateWindow()
        {
            InitializeComponent();

            // Start Download as soon as Update Window opens
            StartDownload();
        }

        /// <summary>
        /// Close
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Close();
        }

        // -----------------------------------------------
        // Download Handlers
        // -----------------------------------------------
        // -------------------------
        // Progress Changed
        // -------------------------
        public void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Progress Info
            this.Dispatcher.BeginInvoke((Action)(() =>
            {
                this.labelProgressInfo.Content = progressInfo;
            }));

            // Progress Bar
            this.Dispatcher.BeginInvoke((Action)(() =>
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                this.progressBar.Value = int.Parse(Math.Truncate(percentage).ToString());
            }));
        }

        // -------------------------
        // Download Complete
        // -------------------------
        public void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // Set the waiter Release
            // Must be here
            this.Dispatcher.BeginInvoke((Action)(() =>
            {
                waiter.Set();
            }));
        }

        // -------------------------
        // Check For Internet Connection
        // -------------------------
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static bool CheckForInternetConnection()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }



        // -------------------------
        // Axiom Self-Update Download
        // -------------------------
        public void StartDownload()
        {
            // Start New Thread
            Thread worker = new Thread(() =>
            {
                // -------------------------
                // Download
                // -------------------------
                waiter = new ManualResetEvent(false); //start a new waiter for next pass (clicking update again)

                Uri downloadUrl = new Uri("https://github.com/MattMcManis/Axiom/releases/download/" + "v" + Convert.ToString(MainWindow.latestVersion) + "-" + MainWindow.latestBuildPhase + "/Axiom.zip"); // v1.0.0.0-alpha/Axiom.zip

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                // Use SecurityProtocolType.Ssl3 if needed for compatibility reasons

                //Async
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
                wc.DownloadFileAsync(downloadUrl, tempDir + "Axiom.zip");

                // Progress Info
                progressInfo = "Downloading Axiom...";

                // Wait for Download to finish
                waiter.WaitOne();


                // -------------------------
                // Extract
                // -------------------------
                // Progress Info
                progressInfo = "Extracting Axiom...";

                List<string> extractArgs = new List<string>() {
                    // Powershell Launch Parameters
                    "-nologo -noprofile -command",
                    "$Host.UI.RawUI.WindowSize = New-Object System.Management.Automation.Host.Size (80, 30); ",
                    "$Host.UI.RawUI.BufferSize = New-Object System.Management.Automation.Host.Size (80, 30); ",
                    // Message
                    "Write-Host \"Updating Axiom to version " + Convert.ToString(MainWindow.latestVersion) + ".\";",
                    "Write-Host \"`nPlease wait for program to close.\";",
                    // Wait
                    "timeout 3;",
                    // Extract
                    "$shell = new-object -com shell.application;",
                    "$zip = $shell.NameSpace('" + tempDir + "Axiom.zip');",
                    "foreach ($item in $zip.items()) {$shell.Namespace('" + MainWindow.appDir + "').CopyHere($item, 0x14)};",
                    // Delete Temp
                    "Write-Host \"`nDeleting Temp File\";",
                    "del " + "\"" + tempDir + "Axiom.zip" + "\";",
                    // Complete
                    "Write-Host \"`nUpdate Complete. Relaunching Axiom.\";",
                    "timeout 3;",
                    // Relaunch Axiom
                    "& '" + MainWindow.appDir + "Axiom.exe'",
                };

                // Join List with Spaces
                string arguments = string.Join(" ", extractArgs.Where(s => !string.IsNullOrEmpty(s)));

                // Start
                Process.Start("powershell.exe", arguments);

                // Close Axiom before updating exe
                Environment.Exit(0);
            });


            // Start Download Thread
            //
            worker.Start();
        }
    }
}
