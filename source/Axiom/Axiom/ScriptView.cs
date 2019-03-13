/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2019 Matt McManis
http://github.com/MattMcManis/Axiom
http://axiomui.github.io
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
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class ScriptView
    {
        public static bool sort = false;

        //public static Paragraph scriptParagraph = new Paragraph(); //RichTextBox


        /// <summary>
        ///     Script RichTextBox Edited
        /// </summary>
        // Current RichTextBox Text
        //public static String GetScriptRichTextBoxContents(MainWindow mainwindow)
        //{
        //    // Select All Text
        //    TextRange textRange = new TextRange(
        //        mainwindow.rtbScriptView.Document.ContentStart,
        //        mainwindow.rtbScriptView.Document.ContentEnd
        //    );

        //    // Remove Formatting
        //    textRange.ClearAllProperties();

        //    // Return Text
        //    return textRange.Text;
        //}


        /// <summary>
        ///     Clear RichTextBox
        /// </summary>
        public static void ClearScriptView(ViewModel vm)
        {
            //MainWindow mainwindow = (MainWindow)System.Windows.Application.Current.MainWindow;

            // Clear Old Text
            //mainwindow.rtbScriptView.Document = new FlowDocument(scriptParagraph);
            //mainwindow.rtbScriptView.BeginChange();
            //mainwindow.rtbScriptView.SelectAll();
            //mainwindow.rtbScriptView.Selection.Text = string.Empty;
            //mainwindow.rtbScriptView.EndChange();

            //vm.ScriptView_FlowDocument = new FlowDocument(vm.ScriptView_Paragraph);
            //mainwindow.rtbScriptView.BeginChange();
            //mainwindow.rtbScriptView.SelectAll();
            //mainwindow.rtbScriptView.Selection.Text = "";
            //mainwindow.rtbScriptView.EndChange();

            vm.ScriptView_Text = string.Empty;
        }

    }
}
