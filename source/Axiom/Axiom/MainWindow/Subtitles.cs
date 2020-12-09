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
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        /// Subtitle Codec - ComboBox
        /// </summary>
        private void cboSubtitle_Codec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string subtitle_Codec_SelectedItem = (sender as ComboBox).SelectedItem as string;

            // -------------------------
            // Halt if Selected Codec is Null
            // -------------------------
            if (string.IsNullOrWhiteSpace(subtitle_Codec_SelectedItem))
            {
                return;
            }

            // -------------------------
            // Codec Controls
            // -------------------------
            Controls.Subtitles.Controls.CodecControls(subtitle_Codec_SelectedItem);

            // -------------------------
            // Media Type Controls
            // Overrides Codec Controls
            // -------------------------
            // Must be after Codec Controls
            Controls.Format.Controls.MediaTypeControls();

            // -------------------------
            // Convert Button Text Change
            // -------------------------
            ConvertButtonText();
        }


        /// <summary>
        /// Subtitle Stream - ComboBox
        /// </summary>
        private void cboSubtitle_Stream_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // External
            // -------------------------
            if (VM.SubtitleView.Subtitle_Stream_SelectedItem == "external")
            {
                // Enable External ListView and Buttons
                VM.SubtitleView.Subtitle_ListView_IsEnabled = true;

                //lstvSubtitles.Opacity = 1;
                VM.SubtitleView.Subtitle_ListView_Opacity = 1;
            }
            else
            {
                // Disable External ListView and Buttons
                VM.SubtitleView.Subtitle_ListView_IsEnabled = false;

                //lstvSubtitles.Opacity = 0.1;
                VM.SubtitleView.Subtitle_ListView_Opacity = 0.1;
            }
        }

        /// <summary>
        /// Subtitle ListView
        /// </summary>
        private void lstvSubtitles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Clear before adding new selected items
            if (VM.SubtitleView.Subtitle_ListView_SelectedItems != null &&
                VM.SubtitleView.Subtitle_ListView_SelectedItems.Count > 0)
            {
                VM.SubtitleView.Subtitle_ListView_SelectedItems.Clear();
                VM.SubtitleView.Subtitle_ListView_SelectedItems.TrimExcess();
            }

            // Create Selected Items List for ViewModel
            VM.SubtitleView.Subtitle_ListView_SelectedItems = lstvSubtitles.SelectedItems
                                                                           .Cast<string>()
                                                                           .ToList();
        }


        /// <summary>
        /// Subtitle Add
        /// </summary>
        private void btnSubtitle_Add_Click(object sender, RoutedEventArgs e)
        {
            // Open Select File Window
            //Microsoft.Win32.OpenFileDialog selectFiles = new Microsoft.Win32.OpenFileDialog();
            Microsoft.Win32.OpenFileDialog selectFiles = new Microsoft.Win32.OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                //RestoreDirectory = true,
                //ReadOnlyChecked = true,
                //ShowReadOnly = true
            };

            // Defaults
            selectFiles.Multiselect = true;
            selectFiles.Filter = "All files (*.*)|*.*|SRT (*.srt)|*.srt|SUB (*.sub)|*.sub|SBV (*.sbv)|*.sbv|ASS (*.ass)|*.ass|SSA (*.ssa)|*.ssa|MPSUB (*.mpsub)|*.mpsub|LRC (*.lrc)|*.lrc|CAP (*.cap)|*.cap";

            // Process Dialog Box
            //Nullable<bool> result = selectFiles.ShowDialog();
            //if (result == true)
            if (selectFiles.ShowDialog() == true)
            {
                // Reset
                //SubtitlesClear();

                // Add Selected Files to List
                for (var i = 0; i < selectFiles.FileNames.Length; i++)
                {
                    // Wrap in quotes for ffmpeg -i
                    //Generate.Subtitle.subtitleFilePathsList.Add("\"" + selectFiles.FileNames[i] + "\"");
                    Generate.Subtitle.subtitleFilePathsList.Add(WrapWithQuotes(selectFiles.FileNames[i]));
                    //MessageBox.Show(Video.subtitleFiles[i]); //debug

                    Generate.Subtitle.subtitleFileNamesList.Add(Path.GetFileName(selectFiles.FileNames[i]));

                    // ListView Display File Names + Ext
                    VM.SubtitleView.Subtitle_ListView_Items.Add(Path.GetFileName(selectFiles.FileNames[i]));
                }
            }
        }

        /// <summary>
        /// Subtitle Remove
        /// </summary>
        private void btnSubtitle_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (VM.SubtitleView.Subtitle_ListView_SelectedItems.Count > 0)
            {
                var selectedIndex = VM.SubtitleView.Subtitle_ListView_SelectedIndex;

                // ListView Items
                var itemlsvFileNames = VM.SubtitleView.Subtitle_ListView_Items[selectedIndex];
                VM.SubtitleView.Subtitle_ListView_Items.RemoveAt(selectedIndex);

                // List File Paths
                string itemFilePaths = Generate.Subtitle.subtitleFilePathsList[selectedIndex];
                Generate.Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);

                // List File Names
                string itemFileNames = Generate.Subtitle.subtitleFileNamesList[selectedIndex];
                Generate.Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
            }
        }

        /// <summary>
        /// Subtitle Clear All
        /// </summary>
        private void btnSubtitle_Clear_Click(object sender, RoutedEventArgs e)
        {
            SubtitlesClear();
        }

        /// <summary>
        /// Subtitle Clear - Method
        /// </summary>
        public void SubtitlesClear()
        {
            // Clear List View
            //lstvSubtitles.Items.Clear();
            if (VM.SubtitleView.Subtitle_ListView_Items != null &&
                VM.SubtitleView.Subtitle_ListView_Items.Count > 0)
            {
                VM.SubtitleView.Subtitle_ListView_Items.Clear();
            }

            // Clear Paths List
            if (Generate.Subtitle.subtitleFilePathsList != null &&
                Generate.Subtitle.subtitleFilePathsList.Count > 0)
            {
                Generate.Subtitle.subtitleFilePathsList.Clear();
                Generate.Subtitle.subtitleFilePathsList.TrimExcess();
            }

            // Clear Names List
            if (Generate.Subtitle.subtitleFileNamesList != null &&
                Generate.Subtitle.subtitleFileNamesList.Count > 0)
            {
                Generate.Subtitle.subtitleFileNamesList.Clear();
                Generate.Subtitle.subtitleFileNamesList.TrimExcess();
            }
        }

        /// <summary>
        /// Subtitle Sort Up
        /// </summary>
        private void btnSubtitle_SortUp_Click(object sender, RoutedEventArgs e)
        {
            if (VM.SubtitleView.Subtitle_ListView_SelectedItems.Count > 0)
            {
                var selectedIndex = VM.SubtitleView.Subtitle_ListView_SelectedIndex;

                if (selectedIndex > 0)
                {
                    // ListView Items
                    var itemlsvFileNames = VM.SubtitleView.Subtitle_ListView_Items[selectedIndex];
                    VM.SubtitleView.Subtitle_ListView_Items.RemoveAt(selectedIndex);
                    VM.SubtitleView.Subtitle_ListView_Items.Insert(selectedIndex - 1, itemlsvFileNames);

                    // List File Paths
                    string itemFilePaths = Generate.Subtitle.subtitleFilePathsList[selectedIndex];
                    Generate.Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);
                    Generate.Subtitle.subtitleFilePathsList.Insert(selectedIndex - 1, itemFilePaths);

                    // List File Names
                    string itemFileNames = Generate.Subtitle.subtitleFileNamesList[selectedIndex];
                    Generate.Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
                    Generate.Subtitle.subtitleFileNamesList.Insert(selectedIndex - 1, itemFileNames);

                    // Highlight Selected Index
                    VM.SubtitleView.Subtitle_ListView_SelectedIndex = selectedIndex - 1;
                }
            }
        }

        /// <summary>
        /// Subtitle Sort Down
        /// </summary>
        private void btnSubtitle_SortDown_Click(object sender, RoutedEventArgs e)
        {
            if (VM.SubtitleView.Subtitle_ListView_SelectedItems.Count > 0)
            {
                var selectedIndex = VM.SubtitleView.Subtitle_ListView_SelectedIndex;

                if (selectedIndex + 1 < VM.SubtitleView.Subtitle_ListView_Items.Count)
                {
                    // ListView Items
                    var itemlsvFileNames = VM.SubtitleView.Subtitle_ListView_Items[selectedIndex];
                    VM.SubtitleView.Subtitle_ListView_Items.RemoveAt(selectedIndex);
                    VM.SubtitleView.Subtitle_ListView_Items.Insert(selectedIndex + 1, itemlsvFileNames);

                    // List FilePaths
                    string itemFilePaths = Generate.Subtitle.subtitleFilePathsList[selectedIndex];
                    Generate.Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);
                    Generate.Subtitle.subtitleFilePathsList.Insert(selectedIndex + 1, itemFilePaths);

                    // List File Names
                    string itemFileNames = Generate.Subtitle.subtitleFileNamesList[selectedIndex];
                    Generate.Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
                    Generate.Subtitle.subtitleFileNamesList.Insert(selectedIndex + 1, itemFileNames);

                    // Highlight Selected Index
                    VM.SubtitleView.Subtitle_ListView_SelectedIndex = selectedIndex + 1;
                }
            }
        }
    }
}
