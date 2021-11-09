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
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using ViewModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Update Button
        /// </summary>
        private Boolean IsUpdateWindowOpened = false;
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Proceed if Internet Connection
            // -------------------------
            if (UpdateWindow.CheckForInternetConnection() == true)
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                WebClient wc = new WebClient();
                wc.Headers.Add(HttpRequestHeader.UserAgent, "Axiom (https://github.com/MattMcManis/Axiom)" + " v" + currentVersion + "-" + currentBuildPhase + " Update Check");
                wc.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
                wc.Headers.Add("accept-language", "en-US,en;q=0.9");
                wc.Headers.Add("dnt", "1");
                wc.Headers.Add("upgrade-insecure-requests", "1");
                //wc.Headers.Add("accept-encoding", "gzip, deflate, br"); //error

                // -------------------------
                // Parse GitHub .version file
                // -------------------------
                string parseLatestVersion = string.Empty;

                try
                {
                    parseLatestVersion = wc.DownloadString("https://raw.githubusercontent.com/MattMcManis/Axiom/master/.version");
                }
                catch
                {
                    MessageBox.Show("GitHub version file not found.",
                                    "Notice",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);

                    return;
                }

                // -------------------------
                // Split Version & Build Phase by dash
                // -------------------------
                if (!string.IsNullOrWhiteSpace(parseLatestVersion)) //null check
                {
                    try
                    {
                        // Split Version and Build Phase
                        splitVersionBuildPhase = Convert.ToString(parseLatestVersion).Split('-');

                        // Set Version Number
                        latestVersion = new Version(splitVersionBuildPhase[0]); //number
                        latestBuildPhase = splitVersionBuildPhase[1]; //alpha
                    }
                    catch
                    {
                        MessageBox.Show("Error reading version.",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);

                        return;
                    }

                    // Debug
                    //MessageBox.Show(Convert.ToString(latestVersion));
                    //MessageBox.Show(latestBuildPhase);

                    // -------------------------
                    // Check if Axiom is the Latest Version
                    // -------------------------
                    // Update Available
                    if (latestVersion > currentVersion)
                    {
                        // Yes/No Dialog Confirmation
                        //
                        MessageBoxResult result = MessageBox.Show("v" + Convert.ToString(latestVersion) + "-" + latestBuildPhase + "\n\nDownload Update?",
                                                                  "Update Available",
                                                                  MessageBoxButton.YesNo
                                                                  );
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                // Check if Window is already open
                                if (IsUpdateWindowOpened) return;

                                // Start Window
                                updatewindow = new UpdateWindow();

                                // Keep in Front
                                updatewindow.Owner = Window.GetWindow(this);

                                // Only allow 1 Window instance
                                updatewindow.ContentRendered += delegate { IsUpdateWindowOpened = true; };
                                updatewindow.Closed += delegate { IsUpdateWindowOpened = false; };

                                // Position Relative to MainWindow
                                // Keep from going off screen
                                updatewindow.Left = Math.Max((Left + (Width - updatewindow.Width) / 2), Left);
                                updatewindow.Top = Math.Max((Top + (Height - updatewindow.Height) / 2), Top);

                                // Open Window
                                updatewindow.Show();
                                break;
                            case MessageBoxResult.No:
                                break;
                        }
                    }

                    // Update Not Available
                    //
                    else if (latestVersion <= currentVersion)
                    {
                        MessageBox.Show("This version is up to date.",
                                        "Notice",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);

                        return;
                    }

                    // Unknown
                    //
                    else // null
                    {
                        MessageBox.Show("Could not find download. Try updating manually.",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);

                        return;
                    }
                }

                // Version is Null
                //
                else
                {
                    MessageBox.Show("GitHub version file returned empty.",
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                    return;
                }
            }
            else
            {
                MessageBox.Show("Could not detect Internet Connection.",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                return;
            }
        }

        /// <summary>
        /// Update Available Check
        /// </summary>
        public static void UpdateAvailableCheck()
        {
            if (VM.ConfigureView.UpdateAutoCheck_IsChecked == true)
            {
                if (UpdateWindow.CheckForInternetConnection() == true)
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    WebClient wc = new WebClient();
                    wc.Headers.Add(HttpRequestHeader.UserAgent, "Axiom (https://github.com/MattMcManis/Axiom)" + " v" + currentVersion + "-" + currentBuildPhase + " Update Check");
                    wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
                    wc.Headers.Add("Accept-Language", "en-US,en;q=0.9");
                    wc.Headers.Add("dnt", "1");
                    wc.Headers.Add("Upgrade-Insecure-Requests", "1");
                    //wc.Headers.Add("accept-encoding", "gzip, deflate, br"); //error

                    // -------------------------
                    // Parse GitHub .version file
                    // -------------------------
                    string parseLatestVersion = string.Empty;

                    try
                    {
                        parseLatestVersion = wc.DownloadString("https://raw.githubusercontent.com/MattMcManis/Axiom/master/.version");
                    }
                    catch
                    {
                        return;
                    }

                    // -------------------------
                    // Split Version & Build Phase by dash
                    // -------------------------
                    if (!string.IsNullOrWhiteSpace(parseLatestVersion)) //null check
                    {
                        try
                        {
                            // Split Version and Build Phase
                            splitVersionBuildPhase = Convert.ToString(parseLatestVersion).Split('-');

                            // Set Version Number
                            latestVersion = new Version(splitVersionBuildPhase[0]); //number
                            latestBuildPhase = splitVersionBuildPhase[1]; //alpha
                        }
                        catch
                        {
                            return;
                        }

                        // Check if Axiom is the Latest Version
                        // Update Available
                        if (latestVersion > currentVersion)
                        {
                            VM.MainView.TitleVersion = VM.MainView.TitleVersion + " ~ Update Available: " + "(" + Convert.ToString(latestVersion) + "-" + latestBuildPhase + ")";
                        }
                        // Update Not Available
                        else if (latestVersion <= currentVersion)
                        {
                            return;
                        }
                    }
                }
            }
        }

    }
}
