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

using System.Windows;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591

namespace Axiom
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class FilePropertiesWindow : Window
    {
        private MainWindow mainwindow;

        public FilePropertiesWindow(MainWindow mainwindow)
        {
            InitializeComponent();

            //DataContext = vm;

            // Set Width/Height to prevent Tablets maximizing
            //this.Width = 420;
            //this.Height = 400;
            this.MinWidth = 200;
            this.MinHeight = 200;

            this.mainwindow = mainwindow;
        }

        /// <summary>
        /// Window Loaded
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MainView vm = mainwindow.DataContext as MainView;

            // -------------------------
            // Display FFprobe File Properties
            // -------------------------

            // Get FFprobe Path
            MainWindow.FFprobePath();

            // -------------------------
            // Write Properties to Window
            // -------------------------
            try
            {
                Paragraph propertiesParagraph = new Paragraph(); //RichTextBox

                // Clear Rich Text Box on Start
                propertiesParagraph.Inlines.Clear();

                // Start
                rtbFileProperties.Document = new FlowDocument(propertiesParagraph); 

                FFprobe.argsFileProperties = " -i" + " " + "\"" + VM.MainView.Input_Text + "\"" + " -v quiet -print_format ini -show_format -show_streams";

                FFprobe.inputFileProperties = FFprobe.InputFileInfo(VM.MainView.Input_Text, 
                                                                    VM.MainView.Batch_IsChecked, 
                                                                    FFprobe.argsFileProperties
                                                                    );

                // Write All File Properties to Rich Text Box
                if (!string.IsNullOrEmpty(FFprobe.inputFileProperties))
                {
                    rtbFileProperties.BeginChange(); // begin change

                    propertiesParagraph.Inlines.Add(new Run(FFprobe.inputFileProperties) { Foreground = Log.ConsoleDefault });

                    rtbFileProperties.EndChange(); // end change
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// Close
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Close();
        }


        /// <summary>
        /// Expand Button
        /// </summary>
        private void buttonExpand_Click(object sender, RoutedEventArgs e)
        {
            // If less than 600px Height
            if (this.Height <= 650)
            {
                this.Width = 650;
                this.Height = 600;

                double screenWidth = SystemParameters.PrimaryScreenWidth;
                double screenHeight = SystemParameters.PrimaryScreenHeight;
                double windowWidth = this.Width;
                double windowHeight = this.Height;
                this.Left = (screenWidth / 2) - (windowWidth / 2);
                this.Top = (screenHeight / 2) - (windowHeight / 2);
            }
        }
    }
}
