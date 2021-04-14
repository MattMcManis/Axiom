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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using ViewModel;
using System.IO;
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
            // Mux
            // -------------------------
            // ListView Opacity
            if (VM.SubtitleView.Subtitle_Stream_SelectedItem == "mux" ||
                VM.SubtitleView.Subtitle_Stream_SelectedItem == "external")
            {
                // Show
                VM.SubtitleView.Subtitle_ListView_Opacity = 1;
            }
            else
            {
                // Hide
                VM.SubtitleView.Subtitle_ListView_Opacity = 0.15;
            }

            // Enable Metadata
            // Mux
            if (VM.SubtitleView.Subtitle_Stream_SelectedItem == "mux")
            {
                // Enable ListView
                VM.SubtitleView.Subtitle_ListView_IsEnabled = true;
                // Enable Metadata
                VM.SubtitleView.Subtitle_Metadata_Title_IsEnabled = true;
                VM.SubtitleView.Subtitle_Metadata_Language_IsEnabled = true;
            }
            // External (Burn)
            else if (VM.SubtitleView.Subtitle_Stream_SelectedItem == "external")
            {
                // Enable ListView
                VM.SubtitleView.Subtitle_ListView_IsEnabled = true;
                // Disable Metadata
                VM.SubtitleView.Subtitle_Metadata_Title_IsEnabled = false;
                VM.SubtitleView.Subtitle_Metadata_Language_IsEnabled = false;
            }
            // Other Streams
            else
            {
                // Disable Subtitle Mux ListView and Buttons
                VM.SubtitleView.Subtitle_ListView_IsEnabled = false;
                VM.SubtitleView.Subtitle_Metadata_Title_IsEnabled = false;
                VM.SubtitleView.Subtitle_Metadata_Language_IsEnabled = false;
            }
        }

        /// <summary>
        /// Subtitle ListView
        /// </summary>
        private void lstvSubtitles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // ListView
            // -------------------------
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

            // -------------------------
            // Set Metadata
            // -------------------------
            int selectedIndex = VM.SubtitleView.Subtitle_ListView_SelectedIndex;

            // Title
            if (Generate.Subtitle.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
            {
                tbxSubtitle_Metadata_Title.Text = Generate.Subtitle.Metadata.titleList[selectedIndex];
            }
            else
            {
                tbxSubtitle_Metadata_Title.Text = string.Empty;
            }

            // Language
            if (Generate.Subtitle.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
            {
                VM.SubtitleView.Subtitle_Metadata_Language_SelectedItem = Generate.Subtitle.Metadata.languageList[selectedIndex];

                // default
                if (string.IsNullOrWhiteSpace(VM.SubtitleView.Subtitle_Metadata_Language_SelectedItem))
                {
                    VM.SubtitleView.Subtitle_Metadata_Language_SelectedItem = "none";
                }
            }
            else
            {
                VM.SubtitleView.Subtitle_Metadata_Language_SelectedItem = "none";
            }

            // Delay
            if (Generate.Subtitle.Metadata.delayList.ElementAtOrDefault(selectedIndex) != null)
            {
                VM.SubtitleView.Subtitle_Delay_Text = Generate.Subtitle.Metadata.delayList[selectedIndex];
            }
            else
            {
                VM.SubtitleView.Subtitle_Delay_Text = string.Empty;
            }
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
                    Generate.Subtitle.Subtitle.subtitleFilePathsList.Add(WrapWithQuotes(selectFiles.FileNames[i]));
                    //MessageBox.Show(Video.subtitleFiles[i]); //debug

                    Generate.Subtitle.Subtitle.subtitleFileNamesList.Add(Path.GetFileName(selectFiles.FileNames[i]));

                    // ListView Display File Names + Ext
                    VM.SubtitleView.Subtitle_ListView_Items.Add(Path.GetFileName(selectFiles.FileNames[i]));

                    // Metadata Placeholders
                    // Title
                    Generate.Subtitle.Metadata.titleList.Add(string.Empty);

                    // Language
                    Generate.Subtitle.Metadata.languageList.Add(string.Empty);

                    // Delay
                    Generate.Subtitle.Metadata.delayList.Add(string.Empty);
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

                // -------------------------
                // List View
                // -------------------------
                // ListView Items
                var itemlsvFileNames = VM.SubtitleView.Subtitle_ListView_Items[selectedIndex];
                VM.SubtitleView.Subtitle_ListView_Items.RemoveAt(selectedIndex);

                // List File Paths
                string itemFilePaths = Generate.Subtitle.Subtitle.subtitleFilePathsList[selectedIndex];
                Generate.Subtitle.Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);

                // List File Names
                string itemFileNames = Generate.Subtitle.Subtitle.subtitleFileNamesList[selectedIndex];
                Generate.Subtitle.Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);

                // -------------------------
                // Metadata
                // -------------------------
                // Title
                if (Generate.Subtitle.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
                {
                    Generate.Subtitle.Metadata.titleList.RemoveAt(selectedIndex);
                }

                // Language
                if (Generate.Subtitle.Metadata.languageList.ElementAtOrDefault(selectedIndex) != null)
                {
                    Generate.Subtitle.Metadata.languageList.RemoveAt(selectedIndex);
                }

                // Delay
                if (Generate.Subtitle.Metadata.delayList.ElementAtOrDefault(selectedIndex) != null)
                {
                    Generate.Subtitle.Metadata.delayList.RemoveAt(selectedIndex);
                }
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
            // -------------------------
            // List View
            // -------------------------
            // Clear List View
            if (VM.SubtitleView.Subtitle_ListView_Items != null &&
                VM.SubtitleView.Subtitle_ListView_Items.Count > 0)
            {
                VM.SubtitleView.Subtitle_ListView_Items.Clear();
            }

            // Clear Paths List
            if (Generate.Subtitle.Subtitle.subtitleFilePathsList != null &&
                Generate.Subtitle.Subtitle.subtitleFilePathsList.Count > 0)
            {
                Generate.Subtitle.Subtitle.subtitleFilePathsList.Clear();
                Generate.Subtitle.Subtitle.subtitleFilePathsList.TrimExcess();
            }

            // Clear Names List
            if (Generate.Subtitle.Subtitle.subtitleFileNamesList != null &&
                Generate.Subtitle.Subtitle.subtitleFileNamesList.Count > 0)
            {
                Generate.Subtitle.Subtitle.subtitleFileNamesList.Clear();
                Generate.Subtitle.Subtitle.subtitleFileNamesList.TrimExcess();
            }

            // -------------------------
            // Metadata
            // -------------------------
            // Title
            if (Generate.Subtitle.Metadata.titleList != null &&
                Generate.Subtitle.Metadata.titleList.Count > 0)
            {
                Generate.Subtitle.Metadata.titleList.Clear();
                Generate.Subtitle.Metadata.titleList.TrimExcess();
            }

            // Language
            if (Generate.Subtitle.Metadata.languageList != null &&
                Generate.Subtitle.Metadata.languageList.Count > 0)
            {
                Generate.Subtitle.Metadata.languageList.Clear();
                Generate.Subtitle.Metadata.languageList.TrimExcess();
            }

            // Delay
            if (Generate.Subtitle.Metadata.delayList != null &&
                Generate.Subtitle.Metadata.delayList.Count > 0)
            {
                Generate.Subtitle.Metadata.delayList.Clear();
                Generate.Subtitle.Metadata.delayList.TrimExcess();
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
                    // -------------------------
                    // List View
                    // -------------------------
                    // ListView Items
                    var itemlsvFileNames = VM.SubtitleView.Subtitle_ListView_Items[selectedIndex];
                    VM.SubtitleView.Subtitle_ListView_Items.RemoveAt(selectedIndex);
                    VM.SubtitleView.Subtitle_ListView_Items.Insert(selectedIndex - 1, itemlsvFileNames);

                    // List File Paths
                    string itemFilePaths = Generate.Subtitle.Subtitle.subtitleFilePathsList[selectedIndex];
                    Generate.Subtitle.Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);
                    Generate.Subtitle.Subtitle.subtitleFilePathsList.Insert(selectedIndex - 1, itemFilePaths);

                    // List File Names
                    string itemFileNames = Generate.Subtitle.Subtitle.subtitleFileNamesList[selectedIndex];
                    Generate.Subtitle.Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
                    Generate.Subtitle.Subtitle.subtitleFileNamesList.Insert(selectedIndex - 1, itemFileNames);

                    // -------------------------
                    // Metadata
                    // -------------------------
                    // Title
                    if (Generate.Subtitle.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
                    {
                        var titleItem = Generate.Subtitle.Metadata.titleList[selectedIndex];
                        Generate.Subtitle.Metadata.titleList.RemoveAt(selectedIndex);
                        Generate.Subtitle.Metadata.titleList.Insert(selectedIndex - 1, titleItem);
                    }

                    // Language
                    if (Generate.Subtitle.Metadata.languageList.ElementAtOrDefault(selectedIndex) != null)
                    {
                        var titleItem = Generate.Subtitle.Metadata.languageList[selectedIndex];
                        Generate.Subtitle.Metadata.languageList.RemoveAt(selectedIndex);
                        Generate.Subtitle.Metadata.languageList.Insert(selectedIndex - 1, titleItem);
                    }

                    // Delay
                    if (Generate.Subtitle.Metadata.delayList.ElementAtOrDefault(selectedIndex) != null)
                    {
                        var titleItem = Generate.Subtitle.Metadata.delayList[selectedIndex];
                        Generate.Subtitle.Metadata.delayList.RemoveAt(selectedIndex);
                        Generate.Subtitle.Metadata.delayList.Insert(selectedIndex - 1, titleItem);
                    }

                    // -------------------------
                    // Highlight Selected Index
                    // -------------------------
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
                    // -------------------------
                    // List View
                    // -------------------------
                    // ListView Items
                    var itemlsvFileNames = VM.SubtitleView.Subtitle_ListView_Items[selectedIndex];
                    VM.SubtitleView.Subtitle_ListView_Items.RemoveAt(selectedIndex);
                    VM.SubtitleView.Subtitle_ListView_Items.Insert(selectedIndex + 1, itemlsvFileNames);

                    // List FilePaths
                    string itemFilePaths = Generate.Subtitle.Subtitle.subtitleFilePathsList[selectedIndex];
                    Generate.Subtitle.Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);
                    Generate.Subtitle.Subtitle.subtitleFilePathsList.Insert(selectedIndex + 1, itemFilePaths);

                    // List File Names
                    string itemFileNames = Generate.Subtitle.Subtitle.subtitleFileNamesList[selectedIndex];
                    Generate.Subtitle.Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
                    Generate.Subtitle.Subtitle.subtitleFileNamesList.Insert(selectedIndex + 1, itemFileNames);

                    // -------------------------
                    // Metadata
                    // -------------------------
                    // Title
                    if (Generate.Subtitle.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
                    {
                        var titleItem = Generate.Subtitle.Metadata.titleList[selectedIndex];
                        Generate.Subtitle.Metadata.titleList.RemoveAt(selectedIndex);
                        Generate.Subtitle.Metadata.titleList.Insert(selectedIndex + 1, titleItem);
                    }

                    // Language
                    if (Generate.Subtitle.Metadata.languageList.ElementAtOrDefault(selectedIndex) != null)
                    {
                        var titleItem = Generate.Subtitle.Metadata.languageList[selectedIndex];
                        Generate.Subtitle.Metadata.languageList.RemoveAt(selectedIndex);
                        Generate.Subtitle.Metadata.languageList.Insert(selectedIndex + 1, titleItem);
                    }

                    // Delay
                    if (Generate.Subtitle.Metadata.delayList.ElementAtOrDefault(selectedIndex) != null)
                    {
                        var titleItem = Generate.Subtitle.Metadata.delayList[selectedIndex];
                        Generate.Subtitle.Metadata.delayList.RemoveAt(selectedIndex);
                        Generate.Subtitle.Metadata.delayList.Insert(selectedIndex + 1, titleItem);
                    }

                    // -------------------------
                    // Highlight Selected Index
                    // -------------------------
                    VM.SubtitleView.Subtitle_ListView_SelectedIndex = selectedIndex + 1;
                }
            }
        }


        /// <summary>
        /// Title Metadata - TextBox
        /// </summary>
        private void tbxSubtitle_Metadata_Title_KeyUp(object sender, KeyEventArgs e)
        {
            SaveMetadata_Subtitle_Title();
        }
        private void tbxSubtitle_Metadata_Title_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveMetadata_Subtitle_Title();
        }
        public void SaveMetadata_Subtitle_Title()
        {
            // -------------------------
            // Halts
            // -------------------------
            if (VM.SubtitleView.Subtitle_Stream_SelectedItem != "mux")
            {
                return;
            }

            // -------------------------
            // Title
            // -------------------------
            if (Generate.Subtitle.Metadata.titleList != null &&
                Generate.Subtitle.Metadata.titleList.Count > 0)
            {
                // Set selected index
                int selectedIndex = VM.SubtitleView.Subtitle_ListView_SelectedIndex;

                // Remove previous from the list at selected track index
                if (Generate.Subtitle.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
                {
                    try
                    {
                        Generate.Subtitle.Metadata.titleList.RemoveAt(selectedIndex);
                    }
                    catch
                    {

                    }
                }

                // Add to list
                try
                {
                    Generate.Subtitle.Metadata.titleList.Insert(selectedIndex, tbxSubtitle_Metadata_Title.Text);
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// Subtitle Language Metadata - ComboBox
        /// </summary>
        private void cboSubtitle_Metadata_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // -------------------------
            // Halts
            // -------------------------
            if (VM.SubtitleView.Subtitle_Stream_SelectedItem != "mux")
            {
                return;
            }

            // -------------------------
            // Language
            // -------------------------
            if (Generate.Subtitle.Metadata.languageList != null &&
                Generate.Subtitle.Metadata.languageList.Count > 0)
            {
                // Set selected index
                int selectedIndex = VM.SubtitleView.Subtitle_ListView_SelectedIndex;

                // Remove previous from the list at selected track index
                if (Generate.Subtitle.Metadata.languageList.ElementAtOrDefault(selectedIndex) != null)
                {
                    try
                    {
                        Generate.Subtitle.Metadata.languageList.RemoveAt(selectedIndex);
                    }
                    catch
                    {

                    }
                }

                // Add to list
                try
                {
                    Generate.Subtitle.Metadata.languageList.Insert(selectedIndex, VM.SubtitleView.Subtitle_Metadata_Language_SelectedItem);
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// Subtitle Delay - TextBox
        /// </summary>
        private void tbxSubtitle_Delay_KeyUp(object sender, KeyEventArgs e)
        {
            SaveMetadata_Subtitle_Delay();
        }
        private void tbxSubtitle_Delay_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveMetadata_Subtitle_Delay();
        }
        public void SaveMetadata_Subtitle_Delay()
        {
            // -------------------------
            // Halts
            // -------------------------
            if (VM.SubtitleView.Subtitle_Stream_SelectedItem != "mux")
            {
                return;
            }

            // -------------------------
            // Delay
            // -------------------------
            if (Generate.Subtitle.Metadata.delayList != null &&
                Generate.Subtitle.Metadata.titleList.Count > 0)
            {
                // Set selected index
                int selectedIndex = VM.SubtitleView.Subtitle_ListView_SelectedIndex;

                // Remove previous from the list at selected track index
                if (Generate.Subtitle.Metadata.delayList.ElementAtOrDefault(selectedIndex) != null)
                {
                    try
                    {
                        Generate.Subtitle.Metadata.delayList.RemoveAt(selectedIndex);
                    }
                    catch
                    {

                    }
                }

                // Add to list
                //MessageBox.Show(string.Join("\r\n", Generate.Subtitle.Metadata.titleList));
                try
                {
                    Generate.Subtitle.Metadata.delayList.Insert(selectedIndex, VM.SubtitleView.Subtitle_Delay_Text);
                }
                catch
                {

                }
            }
        }

    }
}
