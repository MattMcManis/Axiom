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
    public partial class OptimizeAdvanced : Window
    {
        private MainWindow mainwindow;

        // Optimize Window Window
        //OptimizeAdvanced optadv = new OptimizeAdvanced(); //must be "new" for pass data, do not change or remove

        public OptimizeAdvanced()
        {
            // Don't Remove
        }

        public OptimizeAdvanced(MainWindow mainwindow)
        {
            InitializeComponent();

            this.mainwindow = mainwindow;

            // Set Min/Max Width/Height to prevent Tablets maximizing
            this.MinWidth = 371;
            this.MinHeight = 200;
            this.MaxWidth = 371;
            this.MaxHeight = 200;

            // Set ComboBox Dropdown Color
            cboOptimizeTune.Resources.Add(SystemColors.WindowBrushKey, MainWindow.CustomBlue);
            cboOptimizeProfile.Resources.Add(SystemColors.WindowBrushKey, MainWindow.CustomBlue);
            cboOptimizeLevel.Resources.Add(SystemColors.WindowBrushKey, MainWindow.CustomBlue);


            // --------------------------------------------------
            // Load From Saved Settings
            // --------------------------------------------------
            // -------------------------
            // Tune
            // -------------------------         
            // First time use
            if (string.IsNullOrEmpty(MainWindow.OptAdvTune))
            {
                cboOptimizeTune.SelectedItem = "none";
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(MainWindow.OptAdvTune))
            {
                cboOptimizeTune.SelectedItem = MainWindow.OptAdvTune;
            }


            // -------------------------
            // Profile
            // -------------------------
            // First time use
            if (string.IsNullOrEmpty(MainWindow.OptAdvProfile))
            {
                cboOptimizeProfile.SelectedItem = "baseline";
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(MainWindow.OptAdvProfile))
            {
                cboOptimizeProfile.SelectedItem = MainWindow.OptAdvProfile;
            }


            // -------------------------
            // Level
            // -------------------------
            // First time use
            if (string.IsNullOrEmpty(MainWindow.OptAdvLevel))
            {
                cboOptimizeLevel.SelectedItem = "4.0";
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(MainWindow.OptAdvLevel))
            {
                cboOptimizeLevel.SelectedItem = MainWindow.OptAdvLevel;
            }

            //cboOptimizeLevel.SelectedItem = "4.0"; // First time use
            //// Safeguard Against Corrupt Saved Settings
            //try
            //{
            //    if (!string.IsNullOrEmpty(Settings.Default["OptAdvLevel"].ToString())) // auto/null check
            //    {
            //        cboOptimizeLevel.SelectedItem = Settings.Default["OptAdvLevel"];
            //    }
            //}
            //catch
            //{

            //}

            //MessageBox.Show(Settings.Default["OptAdvLevel"].ToString()); //debug

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
            MainWindow.OptAdvTune = cboOptimizeTune.SelectedItem.ToString();
            MainWindow.OptAdvProfile = cboOptimizeProfile.SelectedItem.ToString();
            MainWindow.OptAdvLevel = cboOptimizeLevel.SelectedItem.ToString();

            //try
            //{
            //    // Save Textboxes for next launch
            //    Settings.Default["OptAdvTune"] = cboOptimizeTune.SelectedItem.ToString();
            //    Settings.Default["OptAdvProfile"] = cboOptimizeProfile.SelectedItem.ToString();
            //    Settings.Default["OptAdvLevel"] = cboOptimizeLevel.SelectedItem.ToString();
            //    Settings.Default.Save();
            //    Settings.Default.Reload();
            //}
            //catch
            //{

            //}

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
            MainWindow.OptAdvTune = string.Empty;
            MainWindow.OptAdvProfile = string.Empty;
            MainWindow.OptAdvLevel = string.Empty;

            // Clear any previous set strings
            Video.tune = string.Empty;
            Video.optimizeProfile = string.Empty;
            Video.optimizeLevel = string.Empty;
            Video.optimize = string.Empty;

            this.Close();
        }
    }
}