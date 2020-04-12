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
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class Log
    {
        // --------------------------------------------------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------------------------------------------------
        // List of Actions
        public static List<Action> LogActions = new List<Action>();
        // Action
        public static Action WriteAction { get; set; }
        // Rich Textbox Paragraph
        public static Paragraph logParagraph = new Paragraph(); //RichTextBox

        public static string logDir = MainWindow.appDataLocalDir + @"Axiom UI\";

        // --------------------------------------------------------------------------------------------------------
        // Theme
        // --------------------------------------------------------------------------------------------------------
        // Console Colors
        public static Brush ConsoleDefault { get; set; } // Default
        public static Brush ConsoleTitle { get; set; } // Titles
        public static Brush ConsoleWarning { get; set; } // Warning
        public static Brush ConsoleError { get; set; } // Error
        public static Brush ConsoleAction { get; set; } // Actions


        /// <summary>
        /// Lod Console Message Add (Method)
        /// </summary>
        //public static void LogConsoleMessageAdd(string message, string emphasis, Brush color, int linebreaks)
        //{
        //    WriteAction = () =>
        //    {
        //        // Linebreaks
        //        for (var i = 0; i < linebreaks; i++)
        //        {
        //            logParagraph.Inlines.Add(new LineBreak());
        //        }

        //        // Message
        //        if (emphasis == "normal")
        //        {
        //            logParagraph.Inlines.Add(new Run(message) { Foreground = color });
        //        }
        //        else if (emphasis == "bold")
        //        {
        //            logParagraph.Inlines.Add(new Bold(new Run(message)) { Foreground = color });
        //        }

        //    };
        //    LogActions.Add(WriteAction);
        //}


        /// <summary>
        /// Define Log Path (Method)
        /// </summary>
        public static void DefineLogPath()
        {
            // --------------------------
            // Checked
            // --------------------------
            if (VM.ConfigureView.LogCheckBox_IsChecked == true)
            {
                // Empty, Set Log Path to %ProgramData% Directory
                if (string.IsNullOrEmpty(VM.ConfigureView.LogPath_Text))
                {
                    //VM.ConfigureView.LogPath_Text = MainWindow.appDir;
                    VM.ConfigureView.LogPath_Text = logDir;
                }
            }
        }


        /// <summary>
        /// Create Output Log (Method)
        /// </summary>
        public static void CreateOutputLog(MainWindow mainwindow)
        {
            // -------------------------
            // Background Thread Worker
            // -------------------------
            BackgroundWorker bwlog = new BackgroundWorker();

            bwlog.WorkerSupportsCancellation = true;

            // This allows the worker to report progress during work
            bwlog.WorkerReportsProgress = true;

            // What to do in the background thread
            bwlog.DoWork += new DoWorkEventHandler(delegate (object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;

                // Dispatcher Allows Cross-Thread Communication
                mainwindow.Dispatcher.Invoke(() =>
                {
                    // Log Path
                    if (VM.ConfigureView.LogCheckBox_IsChecked == true) // Only if Log is Enabled through Configure Checkbox
                    {
                        // Start write output log file
                        //Catch Directory Access Errors
                        try
                        {
                            TextRange t = new TextRange(mainwindow.logconsole.rtbLog.Document.ContentStart,
                                                        mainwindow.logconsole.rtbLog.Document.ContentEnd);
                            FileStream file = new FileStream(VM.ConfigureView.LogPath_Text + "axiom.log", FileMode.Create);
                            t.Save(file, System.Windows.DataFormats.Text);
                            file.Close();
                        }
                        catch
                        {
                            // Log Console Message /////////
                            Log.WriteAction = () =>
                            {
                                Log.logParagraph.Inlines.Add(new LineBreak());
                                Log.logParagraph.Inlines.Add(new LineBreak());
                                Log.logParagraph.Inlines.Add(new Bold(new Run("Warning: Saving Output Log to " + "\"" + VM.ConfigureView.LogPath_Text + "\"" + " is Denied. May require Administrator Privileges.")) { Foreground = ConsoleWarning });
                            };
                            Log.LogActions.Add(Log.WriteAction);

                            // Popup Message Dialog Box
                            MessageBox.Show("Error Saving Output Log to " + "\"" + VM.ConfigureView.LogPath_Text + "\"" + ". May require Administrator Privileges.",
                                            "Error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Warning);
                            // do not halt program
                        }
                    }

                }); //end dispatcher

            }); //end thread


            // -------------------------
            // When Background Worker Completes Task
            // -------------------------
            bwlog.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate (object o, RunWorkerCompletedEventArgs args)
            {
                // Close the Background Worker
                bwlog.CancelAsync();
                bwlog.Dispose();

            }); //end worker completed task

            bwlog.RunWorkerAsync();
        }


        /// <summary>
        /// Log Write All 
        /// </summary>
        public static void LogWriteAll(MainWindow mainwindow)
        {
            // -------------------------
            // Background Thread Worker
            // -------------------------
            BackgroundWorker bwlog = new BackgroundWorker();

            bwlog.WorkerSupportsCancellation = true;

            // This allows the worker to report progress during work
            bwlog.WorkerReportsProgress = true;

            // What to do in the background thread
            bwlog.DoWork += new DoWorkEventHandler(delegate (object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;

                // Dispatcher Allows Cross-Thread Communication
                mainwindow.Dispatcher.Invoke(() =>
                {
                    // -------------------------
                    // Write All Log Actions to Console
                    // -------------------------  
                    mainwindow.logconsole.rtbLog.Document = new FlowDocument(logParagraph); // start
                    mainwindow.logconsole.rtbLog.BeginChange(); // begin change

                    foreach (Action Write in LogActions)
                    {
                        Write();
                    }

                    mainwindow.logconsole.rtbLog.EndChange(); // end change


                    // Clear
                    if (LogActions != null)
                    {
                        LogActions.Clear();
                        LogActions.TrimExcess();
                    }

                }); //end dispatcher
            }); //end thread


            // -------------------------
            // When Background Worker Completes Task
            // -------------------------
            bwlog.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate (object o, RunWorkerCompletedEventArgs args)
            {
                // Scroll Console to End
                mainwindow.logconsole.rtbLog.ScrollToEnd();

                // Close the Background Worker
                bwlog.CancelAsync();
                bwlog.Dispose();

            }); //end worker completed task

            bwlog.RunWorkerAsync();
        }

    }
}
