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

using System.Collections.Generic;
using System.ComponentModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    public class VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void OnPropertyChanged(string prop)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(prop));
            }
        }

        /// <summary>
        /// ViewModel Base
        /// </summary>
        public VM()
        {

        }

        // Main
        public static MainViewModel MainView { get; set; } = new MainViewModel();
        // Format
        public static FormatViewModel FormatView { get; set; } = new FormatViewModel();
        // Video
        public static VideoViewModel VideoView { get; set; } = new VideoViewModel();
        // Subtitle
        public static SubtitleViewModel SubtitleView { get; set; } = new SubtitleViewModel();
        // Audio
        public static AudioViewModel AudioView { get; set; } = new AudioViewModel();
        // Filter Video
        public static FilterVideoViewModel FilterVideoView { get; set; } = new FilterVideoViewModel();
        // Filter Audio
        public static FilterAudioViewModel FilterAudioView { get; set; } = new FilterAudioViewModel();
        // Configure
        public static ConfigureViewModel ConfigureView { get; set; } = new ConfigureViewModel();

    }
}
