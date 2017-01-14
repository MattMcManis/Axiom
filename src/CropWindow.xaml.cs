using System;
using System.Windows;
using System.Windows.Input;
// Disable XML Comment warnings
#pragma warning disable 1591

namespace Axiom
{
    /// <summary>
    /// Interaction logic for CropWindow.xaml
    /// </summary>
    public partial class CropWindow : Window
    {
        public static string crop;

        private MainWindow mainwindow;

        //public static object cropwindow { get; internal set; }

        // Console Window
        //LogConsole console = new LogConsole(); //pass data

        public CropWindow()
        {
            // Don't Remove
        }

        public CropWindow(MainWindow mainwindow)
        {
            InitializeComponent();

            this.mainwindow = mainwindow;

            // Set Min/Max Width/Height to prevent Tablets maximizing
            this.MinWidth = 480;
            this.MinHeight = 270;
            this.MaxWidth = 480;
            this.MaxHeight = 270;

            // --------------------------------------------------
            // Load From Saved Settings
            // --------------------------------------------------
            // -------------------------
            // Crop Width
            // ------------------------- 
            // First time use
            if (string.IsNullOrEmpty(mainwindow.cropWidth))
            {
                cropWidth.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(mainwindow.cropWidth))
            {
                cropWidth.Text = mainwindow.cropWidth;
            }

            // -------------------------
            // Crop Height
            // ------------------------- 
            // First time use
            if (string.IsNullOrEmpty(mainwindow.cropHeight))
            {
                cropHeight.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(mainwindow.cropHeight))
            {
                cropHeight.Text = mainwindow.cropHeight;
            }

            // -------------------------
            // Crop X
            // ------------------------- 
            // First time use
            if (string.IsNullOrEmpty(mainwindow.cropX))
            {
                cropX.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(mainwindow.cropX))
            {
                cropX.Text = mainwindow.cropX;
            }

            // -------------------------
            // Crop Y
            // ------------------------- 
            // First time use
            if (string.IsNullOrEmpty(mainwindow.cropY))
            {
                cropY.Text = string.Empty;
            }
            // Load Temp Saved String
            else if (!string.IsNullOrEmpty(mainwindow.cropY))
            {
                cropY.Text = mainwindow.cropY;
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
        /// Crop Width
        /// </summary>
        private void cropWidth_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers or Backspace
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Crop Height
        /// </summary>
        private void cropHeight_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers or Backspace
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Crop X
        /// </summary>
        private void cropX_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers or Backspace
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Crop Y
        /// </summary>
        private void cropY_KeyDown(object sender, KeyEventArgs e)
        {
            // Only allow Numbers or Backspace
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }


        /// <summary>
        /// SET Button
        /// </summary>
        public void buttonSet_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.crop = string.Empty;
            //mainwindow.crop = null;

            // set crop string to be saved after each run
            //MainWindow.cropClear = false;

            // Make x264 & x265 Width/Height Divisible by 2
            if ((string)mainwindow.cboVideoCodec.SelectedItem == "x264" || (string)mainwindow.cboVideoCodec.SelectedItem == "x265")
            {
                try // will error if wrong characters input
                {
                    int width = Convert.ToInt32(cropWidth.Text);
                    int height = Convert.ToInt32(cropHeight.Text);

                    // If not divisible by 2, add 1 to total
                    if (width % 2 != 0)
                    {
                        width = width - 1;
                    }
                    if (height % 2 != 0)
                    {
                        height = height - 1;
                    }

                    // Save Temp TextBox String Holder
                    mainwindow.cropWidth = cropWidth.Text;
                    mainwindow.cropHeight = cropWidth.Text;
                    mainwindow.cropX = cropX.Text;
                    mainwindow.cropY = cropY.Text;

                    crop = Convert.ToString("crop=" + width + ":" + height + ":" + cropX.Text + ":" + cropY.Text);

                }
                catch
                {
                    MessageBox.Show("Error: Must enter numbers only.");
                }

            }
            // Use Normal Width/Height
            else
            {
                // Save Temp TextBox String Holder
                mainwindow.cropWidth = cropWidth.Text;
                mainwindow.cropHeight = cropHeight.Text;
                mainwindow.cropX = cropX.Text;
                mainwindow.cropY = cropY.Text;

                crop = "crop=" + cropWidth.Text + ":" + cropHeight.Text + ":" + cropX.Text + ":" + cropY.Text;
            }


            // Set Empty Crop X Textbox to 0
            if (string.IsNullOrWhiteSpace(cropX.Text))
            {
                cropX.Text = "0";
                mainwindow.cropX = cropX.Text;

                crop = "crop=" + cropWidth.Text + ":" + cropHeight.Text + ":" + cropX.Text + ":" + cropY.Text;
            }
            // Set Empty Crop Y Textbox to 0
            if (string.IsNullOrWhiteSpace(cropY.Text))
            {
                cropY.Text = "0";
                mainwindow.cropY = cropY.Text;

                crop = "crop=" + cropWidth.Text + ":" + cropHeight.Text + ":" + cropX.Text + ":" + cropY.Text;
            }

            // Set Button Text to show Crop is Active
            mainwindow.buttonCropClearTextBox.Text = "~";

            // Log Console Message /////////
            //console.rtbLog.Inlines.Add("Crop Set: " + "Width = " + cropWidth.Text + " Height = " + cropHeight.Text + " X = " + cropX.Text + " Y = " + cropY.Text);

            this.Close();
        }


        /// <summary>
        /// Clear Button
        /// </summary>
        public void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            // set crop string to be cleared after each run
            //MainWindow.cropClear = true;

            cropWidth.Text = string.Empty;
            cropHeight.Text = string.Empty;
            cropX.Text = string.Empty;
            cropY.Text = string.Empty;

            mainwindow.cropWidth = string.Empty;
            mainwindow.cropHeight = string.Empty;
            mainwindow.cropX = string.Empty;
            mainwindow.cropY = string.Empty;

            crop = string.Empty;
            MainWindow.crop = string.Empty;

            // Set Button Text to show Crop is Active
            mainwindow.buttonCropClearTextBox.Text = string.Empty;
        }
    }
}
