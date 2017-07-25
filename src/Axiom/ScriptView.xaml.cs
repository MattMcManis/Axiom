using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;
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
    /// Interaction logic for ScriptView.xaml
    /// </summary>
    public partial class ScriptView : Window
    {
        //private MainWindow mainwindow;

        public static int sort = 0;

        public static Paragraph scriptParagraph = new Paragraph(); //RichTextBox


        public ScriptView()
        {
            //do not remove
        }

        public ScriptView(MainWindow mainwindow) // pass data constuctor
        {
            InitializeComponent();

            // Set Window Size to center TextBox & Button
            this.Width = 575;
            this.Height = 250;
            this.MinWidth = 575;
            this.MinHeight = 250;

            // Write New Text
            rtbScript.Document = new FlowDocument(scriptParagraph); // start

            // Clear Old Text
            rtbScript.Document = new FlowDocument(scriptParagraph); // start
            rtbScript.BeginChange();
            rtbScript.SelectAll();
            rtbScript.Selection.Text = "";
            rtbScript.EndChange();

            // Write New Text
            rtbScript.Document = new FlowDocument(scriptParagraph); // start

            // begin change
            rtbScript.BeginChange();
            scriptParagraph.Inlines.Add(new Run(FFmpeg.ffmpegArgs));
            rtbScript.EndChange();
        }

        /// <summary>
        /// Close
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Close();
            System.Windows.Forms.Application.ExitThread();
        }


        /// <summary>
        /// Save Script
        /// </summary>
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            // Open 'Save File'
            Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();

            // 'Save File' Default Path same as Input Directory
            //saveFile.InitialDirectory = inputDir;
            saveFile.RestoreDirectory = true;
            saveFile.Filter = "Text file (*.txt)|*.txt";
            saveFile.DefaultExt = ".txt";
            saveFile.FileName = "Script";

            // Show save file dialog box
            Nullable<bool> result = saveFile.ShowDialog();

            // Process dialog box
            if (result == true)
            {
                // Save document
                File.WriteAllText(saveFile.FileName, ScriptRichTextBoxCurrent(), Encoding.Unicode);
            }
        }


        /// <summary>
        /// Copy All Button
        /// </summary>
        private void buttonCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ScriptRichTextBoxCurrent(), TextDataFormat.UnicodeText);
        }

        /// <summary>
        /// Script RichTextBox Edited
        /// </summary>
        // current richtextbox text
        public String ScriptRichTextBoxCurrent()
        {
            FlowDocument scriptFlowDoc = new FlowDocument(scriptParagraph);

            rtbScript.Document = scriptFlowDoc;

            TextRange textRange = new TextRange(
                rtbScript.Document.ContentStart,
                rtbScript.Document.ContentEnd
            );

            // Return Text, Remove LineBreaks
            return textRange.Text;
        }


        /// <summary>
        /// Sort Button
        /// </summary>
        private void buttonSort_Click(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Sort
            // -------------------------
            // Has Not Been Edited
            if (ScriptView.sort == 0 && ScriptRichTextBoxCurrent().Replace(Environment.NewLine, "").Replace("\r\n", "") == FFmpeg.ffmpegArgs)
            {
                // Clear Old Text
                rtbScript.Document = new FlowDocument(scriptParagraph); // start
                rtbScript.BeginChange();
                rtbScript.SelectAll();
                rtbScript.Selection.Text = "";
                rtbScript.EndChange();

                // Write New Text
                rtbScript.Document = new FlowDocument(scriptParagraph); // start

                // Write FFmpeg Args Sort
                rtbScript.BeginChange();
                scriptParagraph.Inlines.Add(new Run(FFmpeg.ffmpegArgsSort));
                rtbScript.EndChange();

                // Sort is Off
                ScriptView.sort = 1;
                // Change Button Back to Inline
                buttonSortTextBlock.Text = "Inline";
            }

            // Has Been Edited
            else if (ScriptView.sort == 0 && ScriptRichTextBoxCurrent().Replace(Environment.NewLine, "").Replace("\r\n", "") != FFmpeg.ffmpegArgs)
            {
                MessageBox.Show("Cannot sort edited text.");
            }


            // -------------------------
            // Inline
            // -------------------------
            else if (ScriptView.sort == 1)
            {
                // CMD Arguments are from Script TextBox
                FFmpeg.ffmpegArgs = ScriptRichTextBoxCurrent().Replace(Environment.NewLine, "").Replace("\r\n", "");

                // Clear Old Text
                rtbScript.Document = new FlowDocument(scriptParagraph); // start
                rtbScript.BeginChange();
                rtbScript.SelectAll();
                rtbScript.Selection.Text = "";
                rtbScript.EndChange();

                // Write New Text
                rtbScript.Document = new FlowDocument(scriptParagraph); // start

                // Write FFmpeg Args
                rtbScript.BeginChange();
                scriptParagraph.Inlines.Add(new Run(FFmpeg.ffmpegArgs));
                rtbScript.EndChange();

                // Sort is On
                ScriptView.sort = 0;
                // Change Button Back to Sort
                buttonSortTextBlock.Text = "Sort";
            }

        }


        /// <summary>
        /// Run Button
        /// </summary>
        private void buttonRun_Click(object sender, RoutedEventArgs e)
        {
            string currentDir = Directory.GetCurrentDirectory();

            // CMD Arguments are from Script TextBox
            FFmpeg.ffmpegArgs = ScriptRichTextBoxCurrent().Replace(Environment.NewLine, "").Replace("\r\n", "");

            // Run FFmpeg Arguments
            System.Diagnostics.Process.Start("CMD.exe", "/k cd " + "\"" + currentDir + "\"" + " & " + /* start ffmpeg commands -->*/ FFmpeg.ffmpegArgs);

            // Clear FFmpeg Arguments for next run
            //FFmpeg.ffmpegArgs = string.Empty;
            //FFmpeg.ffmpegArgsSort = string.Empty;
        }


        /// <summary>
        /// Expand Button
        /// </summary>
        private void buttonExpand_Click(object sender, RoutedEventArgs e)
        {
            if (this.Height <= 250)
            {
                this.Width = 800;
                this.Height = 600;

                double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
                double windowWidth = this.Width;
                double windowHeight = this.Height;
                this.Left = (screenWidth / 2) - (windowWidth / 2);
                this.Top = (screenHeight / 2) - (windowHeight / 2);
            }
        }

    }
}
