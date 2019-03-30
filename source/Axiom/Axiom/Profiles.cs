using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Axiom
{
    class Profiles
    {
        ///// <summary>
        /////    INI Reader
        ///// </summary>
        ///*
        //* Source: GitHub Sn0wCrack
        //* https://gist.github.com/Sn0wCrack/5891612
        //*/
        //public partial class INIFile
        //{
        //    public string path { get; private set; }

        //    [DllImport("kernel32", CharSet = CharSet.Unicode)]
        //    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        //    [DllImport("kernel32", CharSet = CharSet.Unicode)]
        //    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        //    public INIFile(string INIPath)
        //    {
        //        path = INIPath;
        //    }
        //    public void Write(string Section, string Key, string Value)
        //    {
        //        WritePrivateProfileString(Section, Key, Value, this.path);
        //    }

        //    public string Read(string Section, string Key)
        //    {
        //        StringBuilder temp = new StringBuilder(255);
        //        int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
        //        return temp.ToString();
        //    }
        //}


        ///// <summary>
        /////    Scan PC Custom Profiles
        ///// </summary>
        //public static void GetCustomProfiles(ViewModel vm)
        //{
        //    // User Custom Profiles Full Path
        //    if (Directory.Exists(vm.ProfilesPath_Text))
        //    {
        //        listCustomProfilesPaths = Directory.GetFiles(vm.ProfilesPath_Text, "*.ini")
        //            .Select(Path.GetFullPath)
        //            .ToList();
        //    }

        //    // Profiles path does not exist
        //    else
        //    {
        //        // Load Default
        //        vm.ProfilesPath_Text = Paths.profilesDir;
        //    }

        //    // Get Names from Full Paths
        //    List<string> listCustomProfilesNames = new List<string>();
        //    foreach (string path in listCustomProfilesPaths)
        //    {
        //        // Get Name from Path
        //        string profileName = Path.GetFileNameWithoutExtension(path);

        //        // Add Name to List
        //        // Prevent adding duplicate
        //        // Ignore Desktop.ini
        //        if (!listCustomProfilesNames.Contains(profileName) && 
        //            !string.Equals(profileName, "desktop", StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            listCustomProfilesNames.Add(profileName);
        //        }

        //    }

        //    // Join Presets and Profiles Lists
        //    vm.Presets_Items.AddRange(listCustomProfilesNames);
        //    // Populate ComboBox
        //    vm.Profiles_Items = vm.Presets_Items.Distinct().ToList();

        //}


        ///// <summary>
        /////    Export Profile
        ///// </summary>
        //public static void ExportProfile(ViewModel vm, string profile)
        //{
        //    // Check if Profile Directory exists
        //    if (Directory.Exists(vm.ProfilesPath_Text))
        //    {
        //        // Start INI File Write
        //        INIFile inif = new INIFile(profile);

        //        // --------------------------------------------------
        //        // Main Window
        //        // --------------------------------------------------
        //        inif.Write("MainWindow", "Input_Text", (vm.Input_Text));
        //        inif.Write("MainWindow", "Output_Text", (vm.Output_Text));
        //        inif.Write("MainWindow", "BatchExtension_Text", (vm.BatchExtension_Text));
        //        inif.Write("MainWindow", "Batch_IsChecked", (vm.Batch_IsChecked.ToString()));
        //        inif.Write("MainWindow", "CMDWindowKeep_IsChecked", (vm.CMDWindowKeep_IsChecked.ToString()));
        //        inif.Write("MainWindow", "AutoSortScript_IsChecked", (vm.AutoSortScript_IsChecked.ToString()));


        //        // --------------------------------------------------
        //        // Format
        //        // --------------------------------------------------
        //        // Container
        //        inif.Write("Format", "Container_SelectedItem", (vm.Format_Container_SelectedItem));

        //        // MediaType
        //        inif.Write("Format", "MediaType_SelectedItem", (vm.Format_MediaType_SelectedItem));

        //        // Hardware Acceleration
        //        inif.Write("Format", "HWAccel_SelectedItem", (vm.Format_HWAccel_SelectedItem));

        //        // Cut Time
        //        inif.Write("Format", "Cut_SelectedItem", (vm.Format_Cut_SelectedItem));
        //        // Start
        //        inif.Write("Format", "CutStart_Hours_Text", (vm.Format_CutStart_Hours_Text));
        //        inif.Write("Format", "CutStart_Minutes_Text", (vm.Format_CutStart_Minutes_Text));
        //        inif.Write("Format", "CutStart_Seconds_Text", (vm.Format_CutStart_Seconds_Text));
        //        inif.Write("Format", "CutStart_Milliseconds_Text", (vm.Format_CutStart_Milliseconds_Text));
        //        // End
        //        inif.Write("Format", "CutEnd_Hours_Text", (vm.Format_CutEnd_Hours_Text));
        //        inif.Write("Format", "CutEnd_Minutes_Text", (vm.Format_CutEnd_Minutes_Text));
        //        inif.Write("Format", "CutEnd_Seconds_Text", (vm.Format_CutEnd_Seconds_Text));
        //        inif.Write("Format", "CutEnd_Milliseconds_Text", (vm.Format_CutEnd_Milliseconds_Text));

        //        // Cut Frames
        //        inif.Write("Format", "FrameStart_Text", (vm.Format_FrameStart_Text));
        //        inif.Write("Format", "FrameEnd_Text", (vm.Format_FrameEnd_Text));


        //        // --------------------------------------------------
        //        // Video
        //        // --------------------------------------------------
        //        inif.Write("Video", "Codec_SelectedItem", (vm.Video_Codec_SelectedItem));

        //        // Quality
        //        inif.Write("Video", "EncodeSpeed_SelectedItem", (vm.Video_EncodeSpeed_SelectedItem));
        //        inif.Write("Video", "Quality_SelectedItem", (vm.Video_Quality_SelectedItem));
        //        inif.Write("Video", "Pass_SelectedItem", (vm.Video_Pass_SelectedItem));
        //        inif.Write("Video", "CRF_Text", (vm.Video_CRF_Text));
        //        inif.Write("Video", "BitRate_Text", (vm.Video_BitRate_Text));
        //        inif.Write("Video", "MinRate_Text", (vm.Video_MinRate_Text));
        //        inif.Write("Video", "MaxRate_Text", (vm.Video_MaxRate_Text));
        //        inif.Write("Video", "BufSize_Text", (vm.Video_BufSize_Text));
        //        inif.Write("Video", "PixelFormat_SelectedItem", (vm.Video_PixelFormat_SelectedItem));
        //        inif.Write("Video", "FPS_SelectedItem", (vm.Video_FPS_SelectedItem));
        //        inif.Write("Video", "Speed_SelectedItem", (vm.Video_Speed_SelectedItem));

        //        // Optimize
        //        inif.Write("Video", "Optimize_SelectedItem", (vm.Video_Optimize_SelectedItem));
        //        inif.Write("Video", "Optimize_Tune_SelectedItem", (vm.Video_Optimize_Tune_SelectedItem));
        //        inif.Write("Video", "Optimize_Profile_SelectedItem", (vm.Video_Optimize_Profile_SelectedItem));
        //        inif.Write("Video", "Optimize_Level_SelectedItem", (vm.Video_Optimize_Level_SelectedItem));

        //        // Size
        //        inif.Write("Video", "Scale_SelectedItem", (vm.Video_Scale_SelectedItem));
        //        inif.Write("Video", "Width_Text", (vm.Video_Width_Text));
        //        inif.Write("Video", "Height_Text", (vm.Video_Height_Text));
        //        inif.Write("Video", "AspectRatio_SelectedItem", (vm.Video_AspectRatio_SelectedItem));
        //        inif.Write("Video", "ScalingAlgorithm_SelectedItem", (vm.Video_ScalingAlgorithm_SelectedItem));

        //        // Crop
        //        inif.Write("Video", "Crop_X_Text", (vm.Video_Crop_X_Text));
        //        inif.Write("Video", "Crop_Y_Text", (vm.Video_Crop_Y_Text));
        //        inif.Write("Video", "Crop_Width_Text", (vm.Video_Crop_Width_Text));
        //        inif.Write("Video", "Crop_Height_Text", (vm.Video_Crop_Height_Text));


        //        // --------------------------------------------------
        //        // Audio
        //        // --------------------------------------------------
        //        inif.Write("Audio", "Codec_SelectedItem", (vm.Audio_Codec_SelectedItem));
        //        inif.Write("Audio", "Stream_SelectedItem", (vm.Audio_Stream_SelectedItem));
        //        inif.Write("Audio", "Channel_SelectedItem", (vm.Audio_Channel_SelectedItem));

        //        // Quality
        //        inif.Write("Audio", "Quality_SelectedItem", (vm.Audio_Quality_SelectedItem));
        //        inif.Write("Audio", "VBR_IsChecked", (vm.Audio_VBR_IsChecked.ToString()));
        //        inif.Write("Audio", "BitRate_Text", (vm.Audio_BitRate_Text));
        //        inif.Write("Audio", "SampleRate_SelectedItem", (vm.Audio_SampleRate_SelectedItem));
        //        inif.Write("Audio", "BitDepth_SelectedItem", (vm.Audio_BitDepth_SelectedItem));

        //        // Filter
        //        inif.Write("Audio", "Volume_Text", (vm.Audio_Volume_Text));
        //        inif.Write("Audio", "HardLimiter_Value", (vm.Audio_HardLimiter_Value.ToString()));


        //        // --------------------------------------------------
        //        // Subtitle
        //        // --------------------------------------------------
        //        inif.Write("Subtitle", "", (vm.Subtitle_Codec_SelectedItem));
        //        inif.Write("Subtitle", "", (vm.Subtitle_Stream_SelectedItem));
        //        //inif.Write("Subtitle", "", (vm.));
        //        //inif.Write("Subtitle", "", (vm.));
        //        //inif.Write("Subtitle", "", (vm.));
        //        //inif.Write("Subtitle", "", (vm.));

        //        // --------------------------------------------------
        //        // Filters
        //        // --------------------------------------------------
        //        inif.Write("Filters", "", (vm.));
        //        inif.Write("Filters", "", (vm.));
        //        inif.Write("Filters", "", (vm.));
        //        inif.Write("Filters", "", (vm.));
        //        inif.Write("Filters", "", (vm.));
        //        inif.Write("Filters", "", (vm.));
        //        inif.Write("Filters", "", (vm.));
        //        inif.Write("Filters", "", (vm.));
        //        inif.Write("Filters", "", (vm.));
        //        inif.Write("Filters", "", (vm.));
        //        inif.Write("Filters", "", (vm.));

        //        // --------------------------------------------------
        //        // Settings
        //        // --------------------------------------------------
        //        // FFmpeg
        //        inif.Write("Settings", "", (vm.FFmpegPath_Text));
        //        inif.Write("Settings", "", (vm.FFprobePath_Text));
        //        inif.Write("Settings", "", (vm.FFplayPath_Text));

        //        // Log
        //        inif.Write("Settings", "", (vm.LogCheckBox_IsChecked.ToString()));
        //        inif.Write("Settings", "", (vm.LogConsole_Text));

        //        // Threads
        //        inif.Write("Settings", "", (vm.Threads_SelectedItem));

        //        // Theme
        //        inif.Write("Settings", "", (vm.Theme_SelectedItem));

        //        // Updates
        //        inif.Write("Settings", "", (vm.UpdateAutoCheck_IsChecked.ToString()));
        //    }

        //    // Create Profiles Directory if does not exist
        //    else
        //    {
        //        // Yes/No Dialog Confirmation
        //        //
        //        MessageBoxResult resultExport = MessageBox.Show("Profiles Folder does not exist. Automatically create it?",
        //                                                        "Directory Not Found",
        //                                                        MessageBoxButton.YesNo,
        //                                                        MessageBoxImage.Information);
        //        switch (resultExport)
        //        {
        //            // Create
        //            case MessageBoxResult.Yes:
        //                try
        //                {
        //                    Directory.CreateDirectory(vm.ProfilesPath_Text);
        //                }
        //                catch
        //                {
        //                    MessageBox.Show("Could not create Profiles folder. May require Administrator privileges.",
        //                                    "Error",
        //                                    MessageBoxButton.OK,
        //                                    MessageBoxImage.Error);
        //                }
        //                break;
        //            // Use Default
        //            case MessageBoxResult.No:
        //                break;
        //        }
        //    }

        //}

    }
}
