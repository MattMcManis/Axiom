using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Documents;
using System.Windows.Media;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

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
    public partial class Log
    {
        //private MainWindow mainwindow;
        //private Configure configure;
        //private ScriptView scriptview;
        //private LogConsole console;

        // --------------------------------------------------------------------------------------------------------
        // Variables
        // --------------------------------------------------------------------------------------------------------
        // List of Actions
        public static List<Action> LogActions = new List<Action>();
        // Action
        public static Action WriteAction;
        // Rich Textbox Paragraph
        public static Paragraph logParagraph = new Paragraph(); //RichTextBox


        // --------------------------------------------------------------------------------------------------------
        // Template
        // --------------------------------------------------------------------------------------------------------
        // Console Colors
        public static Brush ConsoleDefault = Brushes.White; // Default
        public static Brush ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2")); // Titles
        public static Brush ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
        public static Brush ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
        public static Brush ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8")); // Actions


        /// <summary>
        /// Define Log Path (Method)
        /// </summary>
        public static void DefineLogPath(MainWindow mainwindow, Configure configure)
        {
            // Only if Log is Enabled through Configure Checkbox
            if (Configure.logEnable == true)
            {
                // If checkbox is enabled but textbox is empty, put log in exe's current directory
                if (string.IsNullOrEmpty(Configure.logPath))
                {
                    //configure.logPath = currentDir + "\\";
                    //System.Windows.MessageBox.Show(log); //debug

                    Configure.logPath = MainWindow.currentDir.TrimEnd('\\') + @"\";
                }
                // If textbox is not empty, use User custom path
                else if (!string.IsNullOrEmpty(Configure.logPath))
                {
                    // do nothing
                }
            }
            // If checkbox disabled
            else
            {
                Configure.logPath = string.Empty;
            }
        }


        /// <summary>
        /// Create Output Log (Method)
        /// </summary>
        public static void CreateOutputLog(MainWindow mainwindow, Configure configure)
        {
            // Log Path
            if (Configure.logEnable == true) // Only if Log is Enabled through Configure Checkbox
            {
                // Start Log /////////
                if (MainWindow.script == 0) // do not log if Script Button clicked
                {
                    // Start write output log file

                    //Catch Directory Access Errors
                    try
                    {
                        //Write Log Console to File
                        TextRange t = new TextRange(mainwindow.console.rtbLog.Document.ContentStart, mainwindow.console.rtbLog.Document.ContentEnd);
                        FileStream file = new FileStream(@Configure.logPath + "output.log", FileMode.Create);
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
                            Log.logParagraph.Inlines.Add(new Bold(new Run("Warning: Saving Output Log to " + "\"" + Configure.logPath + "\"" + " is Denied. May require Administrator Privileges.")) { Foreground = ConsoleWarning });
                        };
                        Log.LogActions.Add(Log.WriteAction);

                        // Popup Message Dialog Box
                        System.Windows.MessageBox.Show("Error Saving Output Log to " + "\"" + Configure.logPath + "\"" + ". May require Administrator Privileges.");
                        // do not halt program
                    }
                }

            }
            // If checkbox disabled
            else
            {
                Configure.logPath = string.Empty;
            }
        }


        /// <summary>
        /// Log Write All 
        /// </summary>
        public static void LogWriteAll(MainWindow mainwindow, Configure configure)
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
                    mainwindow.console.rtbLog.Document = new FlowDocument(logParagraph); // start
                    mainwindow.console.rtbLog.BeginChange(); // begin change

                    foreach (Action Write in LogActions)
                    {
                        Write();
                    }

                    mainwindow.console.rtbLog.EndChange(); // end change


                    // Clear
                    LogActions.Clear();
                    LogActions.TrimExcess();

                }); //end dispatcher
            }); //end thread


            // -------------------------
            // When Background Worker Completes Task
            // -------------------------
            bwlog.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate (object o, RunWorkerCompletedEventArgs args)
            {
                // Scroll Console to End
                mainwindow.console.rtbLog.ScrollToEnd();

                // -------------------------
                // Create Output Log File
                // -------------------------
                DefineLogPath(mainwindow, configure);
                CreateOutputLog(mainwindow, configure); //write output log to text file


                // -------------------------
                // Reset & Clear
                // -------------------------
                // Clear the Process Line before the next convert to be safe
                // Do not clear if Running from Script window, it will prevent ffmpegArgs from appearing in log
                if (MainWindow.script == 0)
                {
                    FFmpeg.ffmpegArgs = string.Empty;
                }

                // set script back to 0 for next convert
                MainWindow.script = 0;

                // Clear Strings for next Run
                //MainWindow.ClearVariables();

                // Close the Background Worker
                bwlog.CancelAsync();
                bwlog.Dispose();

            }); //end worker completed task

            bwlog.RunWorkerAsync();
        }

    }
}