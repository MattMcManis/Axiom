/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017, 2018 Matt McManis
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

using System.Windows;
using System.Windows.Controls;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
    /// <summary>
    /// Interaction logic for CropWindow.xaml
    /// </summary>
    public partial class OptimizeAdvancedWindow : Window
    {
        // Temporary saved settings holder
        public static string optAdvTune = string.Empty; 
        public static string optAdvProfile = string.Empty;
        public static string optAdvLevel = string.Empty;

        //public OptimizeAdvancedWindow()
        //{
        //    // Don't Remove
        //}

        public OptimizeAdvancedWindow(MainWindow mainwindow)
        {
            InitializeComponent();

            //this.mainwindow = mainwindow;

            // Set Min/Max Width/Height to prevent Tablets maximizing
            this.MinWidth = 371;
            this.MinHeight = 200;
            this.MaxWidth = 371;
            this.MaxHeight = 200;


            // --------------------------------------------------
            // Load From Saved Settings
            // --------------------------------------------------
            // -------------------------
            // Tune
            // -------------------------         
            // First time use
            if (string.IsNullOrEmpty(OptimizeAdvancedWindow.optAdvTune))
            {
                cboOptimizeTune.SelectedItem = "none";
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(OptimizeAdvancedWindow.optAdvTune))
            {
                cboOptimizeTune.SelectedItem = OptimizeAdvancedWindow.optAdvTune;
            }


            // -------------------------
            // Profile
            // -------------------------
            // First time use
            if (string.IsNullOrEmpty(OptimizeAdvancedWindow.optAdvProfile))
            {
                cboOptimizeProfile.SelectedItem = "baseline";
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(OptimizeAdvancedWindow.optAdvProfile))
            {
                cboOptimizeProfile.SelectedItem = OptimizeAdvancedWindow.optAdvProfile;
            }


            // -------------------------
            // Level
            // -------------------------
            // First time use
            if (string.IsNullOrEmpty(OptimizeAdvancedWindow.optAdvLevel))
            {
                cboOptimizeLevel.SelectedItem = "4.0";
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(OptimizeAdvancedWindow.optAdvLevel))
            {
                cboOptimizeLevel.SelectedItem = OptimizeAdvancedWindow.optAdvLevel;
            }
        }


        /// <summary>
        /// Close All
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Close();
        }


        /// <summary>
        // Profile ComboBox
        /// <summary>
        private void cboOptimizeProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If Profile = none, Level = none
            if ((string)cboOptimizeProfile.SelectedItem == "none")
            {
                cboOptimizeLevel.SelectedItem = "none";
            }

            // If Profile is Not none, and Level is none, default to Level 4.0
            if ((string)cboOptimizeProfile.SelectedItem != "none" && (string)cboOptimizeLevel.SelectedItem == "none")
            {
                cboOptimizeLevel.SelectedItem = "4.0";
            }
        }


        /// <summary>
        /// Level ComboBox
        /// </summary>
        private void cboOptimizeLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If Level = none, Profile = none
            if ((string)cboOptimizeLevel.SelectedItem == "none")
            {
                cboOptimizeProfile.SelectedItem = "none";
            }

            // If Level is Not none, and Profile is none, default to Profile Baseline
            if ((string)cboOptimizeLevel.SelectedItem != "none" && (string)cboOptimizeProfile.SelectedItem == "none")
            {
                cboOptimizeProfile.SelectedItem = "baseline";
            }
        }


        /// <summary>
        /// Set Button
        /// </summary>
        private void buttonOptimizeSet_Click(object sender, RoutedEventArgs e)
        {
            // Save to Temp Item Holder
            OptimizeAdvancedWindow.optAdvTune = cboOptimizeTune.SelectedItem.ToString();
            OptimizeAdvancedWindow.optAdvProfile = cboOptimizeProfile.SelectedItem.ToString();
            OptimizeAdvancedWindow.optAdvLevel = cboOptimizeLevel.SelectedItem.ToString();

            this.Close();
        }


        /// <summary>
        /// Clear Button
        /// </summary>
        private void buttonOptimizeClear_Click(object sender, RoutedEventArgs e)
        {
            // Set ComboBoxes to none
            cboOptimizeTune.SelectedItem = "none";
            cboOptimizeProfile.SelectedItem = "none";
            cboOptimizeLevel.SelectedItem = "none";

            // Clear Tmp Strings
            OptimizeAdvancedWindow.optAdvTune = string.Empty;
            OptimizeAdvancedWindow.optAdvProfile = string.Empty;
            OptimizeAdvancedWindow.optAdvLevel = string.Empty;

            // Clear any previous set strings
            Video.optTune = string.Empty;
            Video.optProfile = string.Empty;
            Video.optLevel = string.Empty;
            Video.optimize = string.Empty;

            this.Close();
        }
    }
}