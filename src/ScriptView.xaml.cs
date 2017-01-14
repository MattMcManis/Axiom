using System;
using System.IO;
using System.Windows;
// Disable XML Comment warnings
#pragma warning disable 1591

namespace Axiom
{
    /// <summary>
    /// Interaction logic for ScriptView.xaml
    /// </summary>
    public partial class ScriptView : Window
    {
        string ffmpegArgs; // pass data to string
        //private MainWindow mainwindow;
        //private Configure configure;
        //private LogConsole console;

        public ScriptView(string ffmpegArgs) // pass data constuctor
        {
            InitializeComponent();

            // Set Window Size to center TextBox & Button
            this.Width = 575;
            this.Height = 250;
            this.MinWidth = 300;
            this.MinHeight = 250;

            this.ffmpegArgs = ffmpegArgs;

            // Display Script in Textbox
            textBoxScript.Text = ffmpegArgs;
        }

        /// <summary>
        /// Close
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            textBoxScript.Text = null;
            this.Close();
            System.Windows.Forms.Application.ExitThread();
        }


        /// <summary>
        /// Copy All Button
        /// </summary>
        private void buttonCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(textBoxScript.Text);
        }

        /// <summary>
        /// Run Button
        /// </summary>
        private void buttonRun_Click(object sender, RoutedEventArgs e)
        {
            string currentDir = Directory.GetCurrentDirectory();

            // CMD Arguments are from Script TextBox
            ffmpegArgs = textBoxScript.Text;

            System.Diagnostics.Process.Start("CMD.exe", "/k cd " + "\"" + currentDir + "\"" + " & " + /* start ffmpeg commands -->*/ ffmpegArgs);

            // Error Writing Log from ScriptView
            // Call DefineLogPath Method
            //var getMethod1 = mainwindow.Owner as MainWindow;
            //Log.DefineLogPath(mainwindow, this, console, configure);
            // Call CreateLog Method
            //var getMethod2 = mainwindow.Owner as MainWindow;
            //Log.CreateOutputLog(mainwindow, this, console, configure);

            // Clear FFmpeg Arguments for next run
            ffmpegArgs = string.Empty;

            // Call Garbage Collector
            //GC.Collect();
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
