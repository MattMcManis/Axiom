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

using Axiom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel;

namespace Generate.Audio
{
    public class Metadata
    {
        public static List<string> titleList = new List<string>();

        public static List<string> languageList = new List<string>();

        public static List<string> delayList = new List<string>();

        /// <summary>
        /// Audio Metadata
        /// </summary>
        public static String Audio()
        {
            string metadata = string.Empty;

            if (VM.AudioView.Audio_Stream_SelectedItem == "mux")
            {
                try
                {
                    for (var i = 0; i < VM.AudioView.Audio_ListView_Items.Count; i++)
                    {
                        // Title
                        if (!string.IsNullOrWhiteSpace(titleList[i]))
                        {
                            metadata += "-metadata:s:a:" + i + " title=" + MainWindow.WrapWithQuotes(titleList[i]) + "\r\n";
                        }

                        // Language
                        if (!string.IsNullOrWhiteSpace(languageList[i]) &&
                            languageList[i] != "none")
                        {
                            metadata += "-metadata:s:a:" + i + " language=" + MainWindow.WrapWithQuotes(
                                                                                VM.AudioView.Audio_Metadata_Language_Items
                                                                                    .FirstOrDefault(item => item.Name == languageList[i])?.Param
                                                                            ) + "\r\n";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(),
                                    "Mux Audio Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                }

                // Remove last linebreak
                metadata = metadata.TrimEnd('\r', '\n');
            }

            return metadata;
        }

    }
}
