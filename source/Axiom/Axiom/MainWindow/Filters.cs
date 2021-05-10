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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        /// Color Reset - Button
        /// </summary>
        private void btnColor_Reset_Click(object sender, RoutedEventArgs e)
        {
            VM.VideoView.Video_Color_Range_SelectedItem = "auto";
            VM.VideoView.Video_Color_Space_SelectedItem = "auto";
            VM.VideoView.Video_Color_Primaries_SelectedItem = "auto";
            VM.VideoView.Video_Color_TransferCharacteristics_SelectedItem = "auto";
            VM.VideoView.Video_Color_Matrix_SelectedItem = "auto";
        }

        /// <summary>
        /// Filter Video - Drop Frames
        /// </summary>
        private void cboFilterVideo_DropFrames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Filter Video - Selective SelectiveColorPreview - ComboBox
        /// </summary>
        //public static List<Filters.Video.FilterVideoSelectiveColor> cboSelectiveColor_Items = new List<Filters.Video.FilterVideoSelectiveColor>()
        //{
        //    new Filters.Video.FilterVideoSelectiveColor("Reds", Colors.Red),
        //    new Filters.Video.FilterVideoSelectiveColor("Yellows", Colors.Yellow),
        //    new Filters.Video.FilterVideoSelectiveColor("Greens", Colors.Green),
        //    new Filters.Video.FilterVideoSelectiveColor("Cyans", Colors.Cyan),
        //    new Filters.Video.FilterVideoSelectiveColor("Blues", Colors.Blue),
        //    new Filters.Video.FilterVideoSelectiveColor("Magentas", Colors.Magenta),
        //    new Filters.Video.FilterVideoSelectiveColor("Whites", Colors.White),
        //    new Filters.Video.FilterVideoSelectiveColor("Neutrals", Colors.Gray),
        //    new Filters.Video.FilterVideoSelectiveColor("Blacks", Colors.Black),
        //};
        //public static List<Filters.Video.FilterVideoSelectiveColor> _cboSelectiveColor_Previews
        //{
        //    get { return _cboSelectiveColor_Previews; }
        //    set { _cboSelectiveColor_Previews = value; }
        //}

        private void cboFilterVideo_SelectiveColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Switch Tab SelectiveColorPreview
            //tabControl_SelectiveColor.SelectedIndex = 0;

            //var selectedItem = (Filters.Video.FilterVideoSelectiveColor)cboFilterVideo_SelectiveColor.SelectedItem;
            //string color = selectedItem.SelectiveColorName;

            string selectedItem = VM.FilterVideoView.FilterVideo_SelectiveColor_SelectedItem;

            switch (selectedItem)
            {
                case "Reds":
                    tabControl_SelectiveColor.SelectedItem = selectedItem;
                    tabItem_SelectiveColor_Reds.IsSelected = true;
                    break;

                case "Yellows":
                    tabControl_SelectiveColor.SelectedItem = selectedItem;
                    tabItem_SelectiveColor_Yellows.IsSelected = true;
                    break;

                case "Greens":
                    tabControl_SelectiveColor.SelectedItem = selectedItem;
                    tabItem_SelectiveColor_Greens.IsSelected = true;
                    break;

                case "Cyans":
                    tabControl_SelectiveColor.SelectedItem = selectedItem;
                    tabItem_SelectiveColor_Cyans.IsSelected = true;
                    break;

                case "Blues":
                    tabControl_SelectiveColor.SelectedItem = selectedItem;
                    tabItem_SelectiveColor_Blues.IsSelected = true;
                    break;

                case "Magentas":
                    tabControl_SelectiveColor.SelectedItem = selectedItem;
                    tabItem_SelectiveColor_Magentas.IsSelected = true;
                    break;

                case "Whites":
                    tabControl_SelectiveColor.SelectedItem = selectedItem;
                    tabItem_SelectiveColor_Whites.IsSelected = true;
                    break;

                case "Neutrals":
                    tabControl_SelectiveColor.SelectedItem = selectedItem;
                    tabItem_SelectiveColor_Neutrals.IsSelected = true;
                    break;

                case "Blacks":
                    tabControl_SelectiveColor.SelectedItem = selectedItem;
                    tabItem_SelectiveColor_Blacks.IsSelected = true;
                    break;
            }
        }

        /// <summary>
        /// Filter Video - Selective Color Sliders
        /// </summary>
        // Reds Cyan
        private void slFilterVideo_SelectiveColor_Reds_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Cyan_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Reds Magenta
        private void slFilterVideo_SelectiveColor_Reds_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Magenta_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Reds Yellow
        private void slFilterVideo_SelectiveColor_Reds_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Yellow_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }

        // Yellows Cyan
        private void slFilterVideo_SelectiveColor_Yellows_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Cyan_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Yellows Magenta
        private void slFilterVideo_SelectiveColor_Yellows_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Magenta_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }

        // Yellows Yellow
        private void slFilterVideo_SelectiveColor_Yellows_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Yellow_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }

        // Greens Cyan
        private void slFilterVideo_SelectiveColor_Greens_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Cyan_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Greens Magenta
        private void slFilterVideo_SelectiveColor_Greens_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Magenta_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Greens Yellow
        private void slFilterVideo_SelectiveColor_Greens_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Yellow_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }

        // Cyans Cyan
        private void slFilterVideo_SelectiveColor_Cyans_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Cyan_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }

        // Cyans Magenta
        private void slFilterVideo_SelectiveColor_Cyans_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Magenta_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Cyans Yellow
        private void slFilterVideo_SelectiveColor_Cyans_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Yellow_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }

        // Blues Cyan
        private void slFilterVideo_SelectiveColor_Blues_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Cyan_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Blues Magneta
        private void slFilterVideo_SelectiveColor_Blues_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Magenta_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Blues Yellow
        private void slFilterVideo_SelectiveColor_Blues_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Yellow_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }

        // Magentas Cyan
        private void slFilterVideo_SelectiveColor_Magentas_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Cyan_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Magentas Magenta
        private void slFilterVideo_SelectiveColor_Magentas_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Magenta_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Magentas Yellow
        private void slFilterVideo_SelectiveColor_Magentas_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Yellow_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }

        // Whites Cyan
        private void slFilterVideo_SelectiveColor_Whites_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Cyan_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Whites Magenta
        private void slFilterVideo_SelectiveColor_Whites_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Magenta_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Whites Yellow
        private void slFilterVideo_SelectiveColor_Whites_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Yellow_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }

        // Neutrals Cyan
        private void slFilterVideo_SelectiveColor_Neutrals_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Cyan_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Neutrals Magenta
        private void slFilterVideo_SelectiveColor_Neutrals_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Magenta_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Neutrals Yellow
        private void slFilterVideo_SelectiveColor_Neutrals_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Yellow_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }

        // Blacks Cyan
        private void slFilterVideo_SelectiveColor_Blacks_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Cyan_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Blacks Magenta
        private void slFilterVideo_SelectiveColor_Blacks_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Magenta_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        // Blacks Yellow
        private void slFilterVideo_SelectiveColor_Blacks_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Yellow_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }


        /// <summary>
        /// Filter Video - Selective Color Reset
        /// </summary>
        private void btnFilterVideo_SelectiveColorReset_Click(object sender, RoutedEventArgs e)
        {
            // Reset to default
            Filters.Video.FilterVideo_SelectiveColor_ResetAll();

            //VideoControls.AutoCopyVideoCodec("control");
        }


        /// <summary>
        /// Filter Video - EQ Sliders
        /// </summary>
        // Brightness
        private void slFilterVideo_EQ_Brightness_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_EQ_Brightness_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }
        private void tbxFilterVideo_EQ_Brightness_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            // Reset Empty to 0
            if (string.IsNullOrWhiteSpace(tbxFilterVideo_EQ_Brightness.Text))
            {
                VM.FilterVideoView.FilterVideo_EQ_Brightness_Value = 0;
            }

            //VideoControls.AutoCopyVideoCodec("control");
        }

        // Contrast
        private void slFilterVideo_EQ_Contrast_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_EQ_Contrast_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }

        // Saturation
        private void slFilterVideo_EQ_Saturation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_EQ_Saturation_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }

        // Gamma
        private void slFilterVideo_EQ_Gamma_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterVideoView.FilterVideo_EQ_Gamma_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }

        // Reset
        private void btnFilterVideo_EQ_Reset_Click(object sender, RoutedEventArgs e)
        {
            // Reset to default
            Filters.Video.FilterVideo_EQ_ResetAll();

            //// Brightness
            //VM.FilterVideoView.FilterVideo_EQ_Brightness_Value = 0;
            //// Contrast
            //VM.FilterVideoView.FilterVideo_EQ_Contrast_Value = 0;
            //// Saturation
            //VM.FilterVideoView.FilterVideo_EQ_Saturation_Value = 0;
            //// Gamma
            //VM.FilterVideoView.FilterVideo_EQ_Gamma_Value = 0;

            //VideoControls.AutoCopyVideoCodec("control");
        }

        /// <summary>
        /// Audio Limiter
        /// </summary>
        private void slAudioLimiter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.AudioView.Audio_HardLimiter_Value = 0.0;

            //AudioControls.AutoCopyAudioCodec("control");
        }

        /// <summary>
        /// Filter Audio - Contrast
        /// </summary>
        // Double Click
        private void slFilterAudio_Contrast_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterAudioView.FilterAudio_Contrast_Value = 0;

            //AudioControls.AutoCopyAudioCodec("control");
        }

        /// <summary>
        /// Filter Audio - Extra Stereo
        /// </summary>
        // Double Click
        private void slFilterAudio_ExtraStereo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterAudioView.FilterAudio_ExtraStereo_Value = 0;

            //AudioControls.AutoCopyAudioCodec("control");
        }

        /// <summary>
        /// Filter Audio - Tempo
        /// </summary>
        // Double Click
        private void slFilterAudio_Tempo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Reset to default
            VM.FilterAudioView.FilterAudio_Tempo_Value = 100;

            //AudioControls.AutoCopyAudioCodec("control");
        }
    }
}
