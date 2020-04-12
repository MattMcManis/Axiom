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
    public class VideoViewModel : INotifyPropertyChanged
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

        // Video View Model
        //public static VideoView vm = new VideoView();


        /// <summary>
        /// Video View Model
        /// </summary>
        public VideoViewModel()
        {
            LoadControlsDefaults();
        }


        /// <summary>
        /// Load Controls Defaults
        /// </summary>
        public void LoadControlsDefaults()
        {
            Video_Codec_SelectedItem = "VP8";
            Video_EncodeSpeed_SelectedItem = "Medium";
            Video_HWAccel_SelectedItem = "off";
            Video_Quality_SelectedItem = "Auto";
            Video_VBR_IsChecked = false;
            Video_Pass_SelectedItem = "2 Pass";
            Video_PixelFormat_SelectedItem = "auto";
            Video_FPS_SelectedItem = "auto";
            Video_Speed_SelectedItem = "auto";
            Video_Optimize_SelectedItem = "Web";
            Video_Video_Optimize_Tune_SelectedItem = "none";
            Video_Video_Optimize_Profile_SelectedItem = "none";
            Video_Optimize_Level_SelectedItem = "none";
            Video_Scale_SelectedItem = "Source";
            Video_ScreenFormat_SelectedItem = "auto";
            Video_AspectRatio_SelectedItem = "auto";
            Video_ScalingAlgorithm_SelectedItem = "auto";
            Video_CropClear_Text = "Clear";
        }


        // --------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Video
        /// </summary>
        // --------------------------------------------------------------------------------------------------------

        // --------------------------------------------------
        // Video Codec
        // --------------------------------------------------
        // Items Source
        private List<string> _Video_Codec_Items = new List<string>();
        public List<string> Video_Codec_Items
        {
            get { return _Video_Codec_Items; }
            set
            {
                _Video_Codec_Items = value;
                OnPropertyChanged("Video_Codec_Items");
            }
        }

        // Codec and Parameters
        public class VideoCodec
        {
            public string Codec { get; set; }
            public string Parameters { get; set; }
            public string Parameters_Auto { get; set; }
            public string Parameters_CRF { get; set; }
            public string Parameters_1Pass { get; set; }
            public string Parameters_2Pass { get; set; }
            public string Parameters_None { get; set; }
        }

        // Codec Command
        public string Video_Codec;

        // Selected Index
        private int _Video_Codec_SelectedIndex { get; set; }
        public int Video_Codec_SelectedIndex
        {
            get { return _Video_Codec_SelectedIndex; }
            set
            {
                if (_Video_Codec_SelectedIndex == value)
                {
                    return;
                }

                _Video_Codec_SelectedIndex = value;
                OnPropertyChanged("Video_Codec_SelectedIndex");
            }
        }

        // Selected Item
        private string _Video_Codec_SelectedItem { get; set; }
        public string Video_Codec_SelectedItem
        {
            get { return _Video_Codec_SelectedItem; }
            set
            {
                if (_Video_Codec_SelectedItem == value)
                {
                    return;
                }

                _Video_Codec_SelectedItem = value;
                OnPropertyChanged("Video_Codec_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Video_Codec_IsEnabled = true;
        public bool Video_Codec_IsEnabled
        {
            get { return _Video_Codec_IsEnabled; }
            set
            {
                if (_Video_Codec_IsEnabled == value)
                {
                    return;
                }

                _Video_Codec_IsEnabled = value;
                OnPropertyChanged("Video_Codec_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Encode Speed
        // --------------------------------------------------
        // Items Source
        public class VideoEncodeSpeed
        {
            public string Name { get; set; }
            public string Command { get; set; }
            public string Command_2Pass { get; set; }
        }
        public List<VideoEncodeSpeed> _Video_EncodeSpeed_Items = new List<VideoEncodeSpeed>();
        public List<VideoEncodeSpeed> Video_EncodeSpeed_Items
        {
            get { return _Video_EncodeSpeed_Items; }
            set
            {
                _Video_EncodeSpeed_Items = value;
                OnPropertyChanged("Video_EncodeSpeed_Items");
            }
        }

        // Selected Index
        private int _Video_EncodeSpeed_SelectedIndex { get; set; }
        public int Video_EncodeSpeed_SelectedIndex
        {
            get { return _Video_EncodeSpeed_SelectedIndex; }
            set
            {
                if (_Video_EncodeSpeed_SelectedIndex == value)
                {
                    return;
                }

                _Video_EncodeSpeed_SelectedIndex = value;
                OnPropertyChanged("Video_EncodeSpeed_SelectedIndex");
            }
        }

        // Selected Item
        private string _Video_EncodeSpeed_SelectedItem { get; set; }
        public string Video_EncodeSpeed_SelectedItem
        {
            get { return _Video_EncodeSpeed_SelectedItem; }
            set
            {
                var previousItem = _Video_EncodeSpeed_SelectedItem;

                if (!string.IsNullOrEmpty(Video_EncodeSpeed_SelectedItem) &&
                    Video_EncodeSpeed_SelectedItem != "none")
                {
                    MainWindow.Video_EncodeSpeed_PreviousItem = previousItem;
                }


                if (_Video_EncodeSpeed_SelectedItem == value)
                {
                    return;
                }

                _Video_EncodeSpeed_SelectedItem = value;
                OnPropertyChanged("Video_EncodeSpeed_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Video_EncodeSpeed_IsEnabled = true;
        public bool Video_EncodeSpeed_IsEnabled
        {
            get { return _Video_EncodeSpeed_IsEnabled; }
            set
            {
                if (_Video_EncodeSpeed_IsEnabled == value)
                {
                    return;
                }

                _Video_EncodeSpeed_IsEnabled = value;
                OnPropertyChanged("Video_EncodeSpeed_IsEnabled");
            }
        }


        // --------------------------------------------------
        // HW Accel
        // --------------------------------------------------
        // Items Source
        private List<string> _Video_HWAccel_Items = new List<string>()
        {
            "off",
            "dxva2",
            "cuvid",
            "nvenc",
            "cuvid+nvenc"
        };
        public List<string> Video_HWAccel_Items
        {
            get { return _Video_HWAccel_Items; }
            set
            {
                _Video_HWAccel_Items = value;
                OnPropertyChanged("Video_HWAccel_Items");
            }
        }

        // Selected Index
        private int _Video_HWAccel_SelectedIndex { get; set; }
        public int Video_HWAccel_SelectedIndex
        {
            get { return _Video_HWAccel_SelectedIndex; }
            set
            {
                if (_Video_HWAccel_SelectedIndex == value)
                {
                    return;
                }

                _Video_HWAccel_SelectedIndex = value;
                OnPropertyChanged("Video_HWAccel_SelectedIndex");
            }
        }

        // Selected Item
        private string _Video_HWAccel_SelectedItem { get; set; }
        public string Video_HWAccel_SelectedItem
        {
            get { return _Video_HWAccel_SelectedItem; }
            set
            {
                if (_Video_HWAccel_SelectedItem == value)
                {
                    return;
                }

                _Video_HWAccel_SelectedItem = value;
                OnPropertyChanged("Video_HWAccel_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Video_HWAccel_IsEnabled = true;
        public bool Video_HWAccel_IsEnabled
        {
            get { return _Video_HWAccel_IsEnabled; }
            set
            {
                if (_Video_HWAccel_IsEnabled == value)
                {
                    return;
                }

                _Video_HWAccel_IsEnabled = value;
                OnPropertyChanged("Video_HWAccel_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Pass
        // --------------------------------------------------
        // Items Source
        private List<string> _Video_Pass_Items = new List<string>()
        {
            "CRF",
            "1 Pass",
            "2 Pass"
        };
        public List<string> Video_Pass_Items
        {
            get { return _Video_Pass_Items; }
            set
            {
                _Video_Pass_Items = value;
                OnPropertyChanged("Video_Pass_Items");
            }
        }

        // Selected Index
        private int _Video_Pass_SelectedIndex { get; set; }
        public int Video_Pass_SelectedIndex
        {
            get { return _Video_Pass_SelectedIndex; }
            set
            {
                if (_Video_Pass_SelectedIndex == value)
                {
                    return;
                }

                _Video_Pass_SelectedIndex = value;
                OnPropertyChanged("Video_Pass_SelectedIndex");
            }
        }

        // Selected Item
        private string _Video_Pass_SelectedItem { get; set; }
        public string Video_Pass_SelectedItem
        {
            get { return _Video_Pass_SelectedItem; }
            set
            {
                if (_Video_Pass_SelectedItem == value)
                {
                    return;
                }

                _Video_Pass_SelectedItem = value;
                OnPropertyChanged("Video_Pass_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Video_Pass_IsEnabled = true;
        public bool Video_Pass_IsEnabled
        {
            get { return _Video_Pass_IsEnabled; }
            set
            {
                if (_Video_Pass_IsEnabled == value)
                {
                    return;
                }

                _Video_Pass_IsEnabled = value;
                OnPropertyChanged("Video_Pass_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Video Quality
        // --------------------------------------------------
        public class VideoQuality
        {
            public string Name { get; set; }
            public string NA { get; set; }
            public string Lossless { get; set; }
            public string CRF { get; set; }
            public string Video_CRF_BitRate { get; set; }
            public string CBR_BitMode { get; set; }
            public string CBR { get; set; }
            public string VBR_BitMode { get; set; }
            public string VBR { get; set; }
            public string MinRate { get; set; }
            public string MaxRate { get; set; }
            public string BufSize { get; set; }
            //public string Custom { get; set; }
        }
        private List<VideoQuality> _Video_Quality_Items = new List<VideoQuality>();
        public List<VideoQuality> Video_Quality_Items
        {
            get { return _Video_Quality_Items; }
            set
            {
                _Video_Quality_Items = value;
                OnPropertyChanged("Video_Quality_Items");
            }
        }

        // Command
        //public string VideoBitRateMode_Command;

        // Selected Index
        private int _Video_Quality_SelectedIndex { get; set; }
        public int Video_Quality_SelectedIndex
        {
            get { return _Video_Quality_SelectedIndex; }
            set
            {
                if (_Video_Quality_SelectedIndex == value)
                {
                    return;
                }

                _Video_Quality_SelectedIndex = value;
                OnPropertyChanged("Video_Quality_SelectedIndex");
            }
        }

        // Selected Item
        private string _Video_Quality_SelectedItem { get; set; }
        public string Video_Quality_SelectedItem
        {
            get { return _Video_Quality_SelectedItem; }
            set
            {
                if (_Video_Quality_SelectedItem == value)
                {
                    return;
                }

                _Video_Quality_SelectedItem = value;
                OnPropertyChanged("Video_Quality_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Video_Quality_IsEnabled = true;
        public bool Video_Quality_IsEnabled
        {
            get { return _Video_Quality_IsEnabled; }
            set
            {
                if (_Video_Quality_IsEnabled == value)
                {
                    return;
                }

                _Video_Quality_IsEnabled = value;
                OnPropertyChanged("Video_Quality_IsEnabled");
            }
        }


        // -------------------------
        // Video VBR - Toggle
        // -------------------------
        // Checked
        private bool _Video_VBR_IsChecked;
        public bool Video_VBR_IsChecked
        {
            get { return _Video_VBR_IsChecked; }
            set
            {
                if (_Video_VBR_IsChecked != value)
                {
                    _Video_VBR_IsChecked = value;
                    OnPropertyChanged("Video_VBR_IsChecked");
                }
            }
        }
        // Enabled
        private bool _Video_VBR_IsEnabled = true;
        public bool Video_VBR_IsEnabled
        {
            get { return _Video_VBR_IsEnabled; }
            set
            {
                if (_Video_VBR_IsEnabled == value)
                {
                    return;
                }

                _Video_VBR_IsEnabled = value;
                OnPropertyChanged("Video_VBR_IsEnabled");
            }
        }


        // -------------------------
        // CRF
        // -------------------------
        // Value
        private double? _Video_CRF_Value = 0;
        public double? Video_CRF_Value
        {
            get { return _Video_CRF_Value; }
            set
            {
                if (_Video_CRF_Value == value)
                {
                    return;
                }

                _Video_CRF_Value = value;
                OnPropertyChanged("Video_CRF_Value");
            }
        }
        // Text
        private string _Video_CRF_Text;
        public string Video_CRF_Text
        {
            get { return _Video_CRF_Text; }
            set
            {
                if (_Video_CRF_Text == value)
                {
                    return;
                }

                _Video_CRF_Text = value;
                OnPropertyChanged("Video_CRF_Text");
            }
        }
        // Enabled
        private bool _Video_CRF_IsEnabled = true;
        public bool Video_CRF_IsEnabled
        {
            get { return _Video_CRF_IsEnabled; }
            set
            {
                if (_Video_CRF_IsEnabled == value)
                {
                    return;
                }

                _Video_CRF_IsEnabled = value;
                OnPropertyChanged("Video_CRF_IsEnabled");
            }
        }


        // -------------------------
        // Video BitRate
        // -------------------------
        // Text
        private string _Video_BitRate_Text;
        public string Video_BitRate_Text
        {
            get { return _Video_BitRate_Text; }
            set
            {
                if (_Video_BitRate_Text == value)
                {
                    return;
                }

                _Video_BitRate_Text = value;
                OnPropertyChanged("Video_BitRate_Text");
            }
        }
        // Enabled
        private bool _Video_BitRate_IsEnabled = true;
        public bool Video_BitRate_IsEnabled
        {
            get { return _Video_BitRate_IsEnabled; }
            set
            {
                if (_Video_BitRate_IsEnabled == value)
                {
                    return;
                }

                _Video_BitRate_IsEnabled = value;
                OnPropertyChanged("Video_BitRate_IsEnabled");
            }
        }


        // -------------------------
        // Video MinRate
        // -------------------------
        // Text
        private string _Video_MinRate_Text;
        public string Video_MinRate_Text
        {
            get { return _Video_MinRate_Text; }
            set
            {
                if (_Video_MinRate_Text == value)
                {
                    return;
                }

                _Video_MinRate_Text = value;
                OnPropertyChanged("Video_MinRate_Text");
            }
        }
        // Enabled
        private bool _Video_MinRate_IsEnabled = true;
        public bool Video_MinRate_IsEnabled
        {
            get { return _Video_MinRate_IsEnabled; }
            set
            {
                if (_Video_MinRate_IsEnabled == value)
                {
                    return;
                }

                _Video_MinRate_IsEnabled = value;
                OnPropertyChanged("Video_MinRate_IsEnabled");
            }
        }

        // -------------------------
        // Video MaxRate
        // -------------------------
        // Text
        private string _Video_MaxRate_Text;
        public string Video_MaxRate_Text
        {
            get { return _Video_MaxRate_Text; }
            set
            {
                if (_Video_MaxRate_Text == value)
                {
                    return;
                }

                _Video_MaxRate_Text = value;
                OnPropertyChanged("Video_MaxRate_Text");
            }
        }
        // Enabled
        private bool _Video_MaxRate_IsEnabled = true;
        public bool Video_MaxRate_IsEnabled
        {
            get { return _Video_MaxRate_IsEnabled; }
            set
            {
                if (_Video_MaxRate_IsEnabled == value)
                {
                    return;
                }

                _Video_MaxRate_IsEnabled = value;
                OnPropertyChanged("Video_MaxRate_IsEnabled");
            }
        }


        // -------------------------
        // Video BufSize
        // -------------------------
        // Text
        private string _Video_BufSize_Text;
        public string Video_BufSize_Text
        {
            get { return _Video_BufSize_Text; }
            set
            {
                if (_Video_BufSize_Text == value)
                {
                    return;
                }

                _Video_BufSize_Text = value;
                OnPropertyChanged("Video_BufSize_Text");
            }
        }
        // Enabled
        private bool _Video_BufSize_IsEnabled = true;
        public bool Video_BufSize_IsEnabled
        {
            get { return _Video_BufSize_IsEnabled; }
            set
            {
                if (_Video_BufSize_IsEnabled == value)
                {
                    return;
                }

                _Video_BufSize_IsEnabled = value;
                OnPropertyChanged("Video_BufSize_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Pixel Format
        // --------------------------------------------------
        // Items Source
        private List<string> _Video_PixelFormat_Items = new List<string>();
        public List<string> Video_PixelFormat_Items
        {
            get { return _Video_PixelFormat_Items; }
            set
            {
                _Video_PixelFormat_Items = value;
                OnPropertyChanged("Video_PixelFormat_Items");
            }
        }

        // Selected Index
        private int _Video_PixelFormat_SelectedIndex { get; set; }
        public int Video_PixelFormat_SelectedIndex
        {
            get { return _Video_PixelFormat_SelectedIndex; }
            set
            {
                if (_Video_PixelFormat_SelectedIndex == value)
                {
                    return;
                }

                _Video_PixelFormat_SelectedIndex = value;
                OnPropertyChanged("Video_PixelFormat_SelectedIndex");
            }
        }

        // Selected Item
        private string _Video_PixelFormat_SelectedItem { get; set; }
        public string Video_PixelFormat_SelectedItem
        {
            get { return _Video_PixelFormat_SelectedItem; }
            set
            {
                if (_Video_PixelFormat_SelectedItem == value)
                {
                    return;
                }

                _Video_PixelFormat_SelectedItem = value;
                OnPropertyChanged("Video_PixelFormat_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Video_PixelFormat_IsEnabled = true;
        public bool Video_PixelFormat_IsEnabled
        {
            get { return _Video_PixelFormat_IsEnabled; }
            set
            {
                if (_Video_PixelFormat_IsEnabled == value)
                {
                    return;
                }

                _Video_PixelFormat_IsEnabled = value;
                OnPropertyChanged("Video_PixelFormat_IsEnabled");
            }
        }


        // --------------------------------------------------
        // FPS
        // --------------------------------------------------
        // Items Source
        private List<string> _Video_FPS_Items = new List<string>()
        {
            "auto",
            "ntsc",
            "pal",
            "film",
            "23.976",
            "24",
            "25",
            "29.97",
            "30",
            "48",
            "50",
            "59.94",
            "60",
            "Custom"
        };
        public List<string> Video_FPS_Items
        {
            get { return _Video_FPS_Items; }
            set
            {
                _Video_FPS_Items = value;
                OnPropertyChanged("Video_FPS_Items");
            }
        }

        // Selected Index
        private int _Video_FPS_SelectedIndex { get; set; }
        public int Video_FPS_SelectedIndex
        {
            get { return _Video_FPS_SelectedIndex; }
            set
            {
                if (_Video_FPS_SelectedIndex == value)
                {
                    return;
                }

                _Video_FPS_SelectedIndex = value;
                OnPropertyChanged("Video_FPS_SelectedIndex");
            }
        }

        // Selected Item
        private string _Video_FPS_SelectedItem { get; set; }
        public string Video_FPS_SelectedItem
        {
            get { return _Video_FPS_SelectedItem; }
            set
            {
                if (_Video_FPS_SelectedItem == value)
                {
                    return;
                }

                _Video_FPS_SelectedItem = value;
                OnPropertyChanged("Video_FPS_SelectedItem");
            }
        }

        // Text
        private string _Video_FPS_Text;
        public string Video_FPS_Text
        {
            get { return _Video_FPS_Text; }
            set
            {
                if (_Video_FPS_Text == value)
                {
                    return;
                }

                _Video_FPS_Text = value;
                OnPropertyChanged("Video_FPS_Text");
            }
        }

        // Controls Is Editable
        private bool _Video_FPS_IsEditable = false;
        public bool Video_FPS_IsEditable
        {
            get { return _Video_FPS_IsEditable; }
            set
            {
                if (_Video_FPS_IsEditable == value)
                {
                    return;
                }

                _Video_FPS_IsEditable = value;
                OnPropertyChanged("Video_FPS_IsEditable");
            }
        }

        // Controls Enable
        private bool _Video_FPS_IsEnabled = true;
        public bool Video_FPS_IsEnabled
        {
            get { return _Video_FPS_IsEnabled; }
            set
            {
                if (_Video_FPS_IsEnabled == value)
                {
                    return;
                }

                _Video_FPS_IsEnabled = value;
                OnPropertyChanged("Video_FPS_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Speed
        // --------------------------------------------------
        // Items Source
        private List<string> _Video_Speed_Items = new List<string>()
        {
            "auto",
            "10%",
            "25%",
            "50%",
            "75%",
            //"100%", default
            "150%",
            "200%",
            "250%",
            "300%",
            "500%",
            "Custom"
        };
        public List<string> Video_Speed_Items
        {
            get { return _Video_Speed_Items; }
            set
            {
                _Video_Speed_Items = value;
                OnPropertyChanged("Video_Speed_Items");
            }
        }

        // Selected Index
        private int _Video_Speed_SelectedIndex { get; set; }
        public int Video_Speed_SelectedIndex
        {
            get { return _Video_Speed_SelectedIndex; }
            set
            {
                if (_Video_Speed_SelectedIndex == value)
                {
                    return;
                }

                _Video_Speed_SelectedIndex = value;
                OnPropertyChanged("Video_Speed_SelectedIndex");
            }
        }

        // Selected Item
        private string _Video_Speed_SelectedItem { get; set; }
        public string Video_Speed_SelectedItem
        {
            get { return _Video_Speed_SelectedItem; }
            set
            {
                if (_Video_Speed_SelectedItem == value)
                {
                    return;
                }

                _Video_Speed_SelectedItem = value;
                OnPropertyChanged("Video_Speed_SelectedItem");
            }
        }

        // Text
        private string _Video_Speed_Text;
        public string Video_Speed_Text
        {
            get { return _Video_Speed_Text; }
            set
            {
                if (_Video_Speed_Text == value)
                {
                    return;
                }

                _Video_Speed_Text = value;
                OnPropertyChanged("Video_Speed_Text");
            }
        }

        // Controls Is Editable
        private bool _Video_Speed_IsEditable = false;
        public bool Video_Speed_IsEditable
        {
            get { return _Video_Speed_IsEditable; }
            set
            {
                if (_Video_Speed_IsEditable == value)
                {
                    return;
                }

                _Video_Speed_IsEditable = value;
                OnPropertyChanged("Video_Speed_IsEditable");
            }
        }

        // Controls Enable
        private bool _Video_Speed_IsEnabled = true;
        public bool Video_Speed_IsEnabled
        {
            get { return _Video_Speed_IsEnabled; }
            set
            {
                if (_Video_Speed_IsEnabled == value)
                {
                    return;
                }

                _Video_Speed_IsEnabled = value;
                OnPropertyChanged("Video_Speed_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Optimize
        // --------------------------------------------------
        public class VideoOptimize
        {
            public string Name { get; set; }
            public string Command { get; set; }
            public string Tune { get; set; }
            public string Profile { get; set; }
            public string Level { get; set; }
        }
        private List<VideoOptimize> _Video_Optimize_Items = new List<VideoOptimize>();
        // Items Source
        //private List<string> _Optimize_Items = new List<string>();
        public List<VideoOptimize> Video_Optimize_Items
        {
            get { return _Video_Optimize_Items; }
            set
            {
                _Video_Optimize_Items = value;
                OnPropertyChanged("Video_Optimize_Items");
            }
        }

        // Selected Index
        private int _Video_Optimize_SelectedIndex { get; set; }
        public int Video_Optimize_SelectedIndex
        {
            get { return _Video_Optimize_SelectedIndex; }
            set
            {
                if (_Video_Optimize_SelectedIndex == value)
                {
                    return;
                }

                _Video_Optimize_SelectedIndex = value;
                OnPropertyChanged("Video_Optimize_SelectedIndex");
            }
        }

        // Selected Item
        private string _Video_Optimize_SelectedItem { get; set; }
        public string Video_Optimize_SelectedItem
        {
            get { return _Video_Optimize_SelectedItem; }
            set
            {
                if (_Video_Optimize_SelectedItem == value)
                {
                    return;
                }

                _Video_Optimize_SelectedItem = value;
                OnPropertyChanged("Video_Optimize_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Video_Optimize_IsEnabled = true;
        public bool Video_Optimize_IsEnabled
        {
            get { return _Video_Optimize_IsEnabled; }
            set
            {
                if (_Video_Optimize_IsEnabled == value)
                {
                    return;
                }

                _Video_Optimize_IsEnabled = value;
                OnPropertyChanged("Video_Optimize_IsEnabled");
            }
        }

        // Controls Expanded
        private bool _Video_Optimize_IsExpanded = true;
        public bool Video_Optimize_IsExpanded
        {
            get { return _Video_Optimize_IsExpanded; }
            set
            {
                if (_Video_Optimize_IsExpanded == value)
                {
                    return;
                }

                _Video_Optimize_IsExpanded = value;
                OnPropertyChanged("Video_Optimize_IsExpanded");
            }
        }


        // --------------------------------------------------
        // Optimize Tune
        // --------------------------------------------------
        // Items Source
        private List<string> _Video_Optimize_Tune_Items = new List<string>();
        public List<string> Video_Optimize_Tune_Items
        {
            get { return _Video_Optimize_Tune_Items; }
            set
            {
                _Video_Optimize_Tune_Items = value;
                OnPropertyChanged("Video_Optimize_Tune_Items");
            }
        }

        // Selected Index
        private int _Video_Optimize_Tune_SelectedIndex { get; set; }
        public int Video_Optimize_Tune_SelectedIndex
        {
            get { return _Video_Optimize_Tune_SelectedIndex; }
            set
            {
                if (_Video_Optimize_Tune_SelectedIndex == value)
                {
                    return;
                }

                _Video_Optimize_Tune_SelectedIndex = value;
                OnPropertyChanged("Video_Optimize_Tune_SelectedIndex");
            }
        }

        // Selected Item
        public string _Video_Video_Optimize_Tune_SelectedItem { get; set; }
        public string Video_Video_Optimize_Tune_SelectedItem
        {
            get { return _Video_Video_Optimize_Tune_SelectedItem; }
            set
            {
                if (_Video_Video_Optimize_Tune_SelectedItem == value)
                {
                    return;
                }

                _Video_Video_Optimize_Tune_SelectedItem = value;
                OnPropertyChanged("Video_Video_Optimize_Tune_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Video_Optimize_Tune_IsEnabled = true;
        public bool Video_Optimize_Tune_IsEnabled
        {
            get { return _Video_Optimize_Tune_IsEnabled; }
            set
            {
                if (_Video_Optimize_Tune_IsEnabled == value)
                {
                    return;
                }

                _Video_Optimize_Tune_IsEnabled = value;
                OnPropertyChanged("Video_Optimize_Tune_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Optimize Profile
        // --------------------------------------------------
        // Items Source
        private List<string> _Video_Optimize_Profile_Items = new List<string>();
        public List<string> Video_Optimize_Profile_Items
        {
            get { return _Video_Optimize_Profile_Items; }
            set
            {
                _Video_Optimize_Profile_Items = value;
                OnPropertyChanged("Video_Optimize_Profile_Items");
            }
        }

        // Selected Index
        private int _Video_Optimize_Profile_SelectedIndex { get; set; }
        public int Video_Optimize_Profile_SelectedIndex
        {
            get { return _Video_Optimize_Profile_SelectedIndex; }
            set
            {
                if (_Video_Optimize_Profile_SelectedIndex == value)
                {
                    return;
                }

                _Video_Optimize_Profile_SelectedIndex = value;
                OnPropertyChanged("Video_Optimize_Profile_SelectedIndex");
            }
        }

        // Selected Item
        public string _Video_Video_Optimize_Profile_SelectedItem { get; set; }
        public string Video_Video_Optimize_Profile_SelectedItem
        {
            get { return _Video_Video_Optimize_Profile_SelectedItem; }
            set
            {
                if (_Video_Video_Optimize_Profile_SelectedItem == value)
                {
                    return;
                }

                _Video_Video_Optimize_Profile_SelectedItem = value;
                OnPropertyChanged("Video_Video_Optimize_Profile_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Video_Optimize_Profile_IsEnabled = true;
        public bool Video_Optimize_Profile_IsEnabled
        {
            get { return _Video_Optimize_Profile_IsEnabled; }
            set
            {
                if (_Video_Optimize_Profile_IsEnabled == value)
                {
                    return;
                }

                _Video_Optimize_Profile_IsEnabled = value;
                OnPropertyChanged("Video_Optimize_Profile_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Optimize Level
        // --------------------------------------------------
        // Items Source
        private List<string> _Video_Optimize_Level_Items = new List<string>();
        public List<string> Video_Optimize_Level_Items
        {
            get { return _Video_Optimize_Level_Items; }
            set
            {
                _Video_Optimize_Level_Items = value;
                OnPropertyChanged("Video_Optimize_Level_Items");
            }
        }

        // Selected Index
        private int _Video_Optimize_Level_SelectedIndex { get; set; }
        public int Video_Optimize_Level_SelectedIndex
        {
            get { return _Video_Optimize_Level_SelectedIndex; }
            set
            {
                if (_Video_Optimize_Level_SelectedIndex == value)
                {
                    return;
                }

                _Video_Optimize_Level_SelectedIndex = value;
                OnPropertyChanged("Video_Optimize_Level_SelectedIndex");
            }
        }

        // Selected Item
        public string _Video_Optimize_Level_SelectedItem { get; set; }
        public string Video_Optimize_Level_SelectedItem
        {
            get { return _Video_Optimize_Level_SelectedItem; }
            set
            {
                if (_Video_Optimize_Level_SelectedItem == value)
                {
                    return;
                }

                _Video_Optimize_Level_SelectedItem = value;
                OnPropertyChanged("Video_Optimize_Level_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Video_Optimize_Level_IsEnabled = true;
        public bool Video_Optimize_Level_IsEnabled
        {
            get { return _Video_Optimize_Level_IsEnabled; }
            set
            {
                if (_Video_Optimize_Level_IsEnabled == value)
                {
                    return;
                }

                _Video_Optimize_Level_IsEnabled = value;
                OnPropertyChanged("Video_Optimize_Level_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Size
        // --------------------------------------------------
        // Items Source
        private List<string> _Video_Scale_Items = new List<string>()
        {
            "Source",
            "8K",
            "8K UHD",
            "4K",
            "4K UHD",
            "2K",
            "1600p",
            "1440p",
            "1200p",
            "1080p",
            "900p",
            "720p",
            "576p",
            "480p",
            "320p",
            "240p",
            "Custom"
        };
        public List<string> Video_Scale_Items
        {
            get { return _Video_Scale_Items; }
            set
            {
                _Video_Scale_Items = value;
                OnPropertyChanged("Video_Scale_Items");
            }
        }

        // Selected Index
        private int _Video_Scale_SelectedIndex { get; set; }
        public int Video_Scale_SelectedIndex
        {
            get { return _Video_Scale_SelectedIndex; }
            set
            {
                if (_Video_Scale_SelectedIndex == value)
                {
                    return;
                }

                _Video_Scale_SelectedIndex = value;
                OnPropertyChanged("Video_Scale_SelectedIndex");
            }
        }

        // Selected Item
        private string _Video_Scale_SelectedItem { get; set; }
        public string Video_Scale_SelectedItem
        {
            get { return _Video_Scale_SelectedItem; }
            set
            {
                if (_Video_Scale_SelectedItem == value)
                {
                    return;
                }

                _Video_Scale_SelectedItem = value;
                OnPropertyChanged("Video_Scale_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Video_Scale_IsEnabled = true;
        public bool Video_Scale_IsEnabled
        {
            get { return _Video_Scale_IsEnabled; }
            set
            {
                if (_Video_Scale_IsEnabled == value)
                {
                    return;
                }

                _Video_Scale_IsEnabled = value;
                OnPropertyChanged("Video_Scale_IsEnabled");
            }
        }

        // --------------------------------------------------
        // Width
        // --------------------------------------------------
        // Text
        private string _Video_Width_Text;
        public string Video_Width_Text
        {
            get { return _Video_Width_Text; }
            set
            {
                if (_Video_Width_Text == value)
                {
                    return;
                }

                _Video_Width_Text = value;
                OnPropertyChanged("Video_Width_Text");
            }
        }
        // Enabled
        private bool _Video_Width_IsEnabled = true;
        public bool Video_Width_IsEnabled
        {
            get { return _Video_Width_IsEnabled; }
            set
            {
                if (_Video_Width_IsEnabled == value)
                {
                    return;
                }

                _Video_Width_IsEnabled = value;
                OnPropertyChanged("Video_Width_IsEnabled");
            }
        }
        // --------------------------------------------------
        // Height
        // --------------------------------------------------
        // Text
        private string _Video_Height_Text;
        public string Video_Height_Text
        {
            get { return _Video_Height_Text; }
            set
            {
                if (_Video_Height_Text == value)
                {
                    return;
                }

                _Video_Height_Text = value;
                OnPropertyChanged("Video_Height_Text");
            }
        }
        // Enabled
        private bool _Video_Height_IsEnabled = true;
        public bool Video_Height_IsEnabled
        {
            get { return _Video_Height_IsEnabled; }
            set
            {
                if (_Video_Height_IsEnabled == value)
                {
                    return;
                }

                _Video_Height_IsEnabled = value;
                OnPropertyChanged("Video_Height_IsEnabled");
            }
        }



        // --------------------------------------------------
        // Screen Format
        // --------------------------------------------------
        // Items Source
        private List<string> _Video_ScreenFormat_Items = new List<string>()
        {
            //"auto",
            "Full Screen",
            "Widescreen",
            //"Ultrawide"
        };
        public List<string> Video_ScreenFormat_Items
        {
            get { return _Video_ScreenFormat_Items; }
            set
            {
                _Video_ScreenFormat_Items = value;
                OnPropertyChanged("Video_ScreenFormat_Items");
            }
        }

        // Selected Index
        private int _Video_ScreenFormat_SelectedIndex { get; set; }
        public int Video_ScreenFormat_SelectedIndex
        {
            get { return _Video_ScreenFormat_SelectedIndex; }
            set
            {
                if (_Video_ScreenFormat_SelectedIndex == value)
                {
                    return;
                }

                _Video_ScreenFormat_SelectedIndex = value;
                OnPropertyChanged("Video_ScreenFormat_SelectedIndex");
            }
        }

        // Selected Item
        public string _Video_ScreenFormat_SelectedItem { get; set; }
        public string Video_ScreenFormat_SelectedItem
        {
            get { return _Video_ScreenFormat_SelectedItem; }
            set
            {
                if (_Video_ScreenFormat_SelectedItem == value)
                {
                    return;
                }

                _Video_ScreenFormat_SelectedItem = value;
                OnPropertyChanged("Video_ScreenFormat_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Video_ScreenFormat_IsEnabled = true;
        public bool Video_ScreenFormat_IsEnabled
        {
            get { return _Video_ScreenFormat_IsEnabled; }
            set
            {
                if (_Video_ScreenFormat_IsEnabled == value)
                {
                    return;
                }

                _Video_ScreenFormat_IsEnabled = value;
                OnPropertyChanged("Video_ScreenFormat_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Aspect Ratio
        // --------------------------------------------------
        // Items Source
        private List<string> _Video_AspectRatio_Items = new List<string>()
        {
            "auto",
            "1:1",
            "2.4:1",
            "3:2",
            "4:3",
            "5:4",
            "8:7",
            "14:10",
            "16:9",
            "16:10",
            "19:10",
            "21:9",
            "32:9",
        };
        public List<string> Video_AspectRatio_Items
        {
            get { return _Video_AspectRatio_Items; }
            set
            {
                _Video_AspectRatio_Items = value;
                OnPropertyChanged("Video_AspectRatio_Items");
            }
        }

        // Selected Index
        private int _Video_AspectRatio_SelectedIndex { get; set; }
        public int Video_AspectRatio_SelectedIndex
        {
            get { return _Video_AspectRatio_SelectedIndex; }
            set
            {
                if (_Video_AspectRatio_SelectedIndex == value)
                {
                    return;
                }

                _Video_AspectRatio_SelectedIndex = value;
                OnPropertyChanged("Video_AspectRatio_SelectedIndex");
            }
        }

        // Selected Item
        public string _Video_AspectRatio_SelectedItem { get; set; }
        public string Video_AspectRatio_SelectedItem
        {
            get { return _Video_AspectRatio_SelectedItem; }
            set
            {
                if (_Video_AspectRatio_SelectedItem == value)
                {
                    return;
                }

                _Video_AspectRatio_SelectedItem = value;
                OnPropertyChanged("Video_AspectRatio_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Video_AspectRatio_IsEnabled = true;
        public bool Video_AspectRatio_IsEnabled
        {
            get { return _Video_AspectRatio_IsEnabled; }
            set
            {
                if (_Video_AspectRatio_IsEnabled == value)
                {
                    return;
                }

                _Video_AspectRatio_IsEnabled = value;
                OnPropertyChanged("Video_AspectRatio_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Scaling
        // --------------------------------------------------
        // Items Source
        private List<string> _Video_ScalingAlgorithm_Items = new List<string>()
        {
            "auto",
            "neighbor",
            "area",
            "fast_bilinear",
            "bilinear",
            "bicubic",
            "experimental",
            "bicublin",
            "gauss",
            "sinc",
            "lanczos",
            "spline"
        };
        public List<string> Video_ScalingAlgorithm_Items
        {
            get { return _Video_ScalingAlgorithm_Items; }
            set
            {
                _Video_ScalingAlgorithm_Items = value;
                OnPropertyChanged("Video_ScalingAlgorithm_Items");
            }
        }

        // Selected Index
        private int _Video_ScalingAlgorithm_SelectedIndex { get; set; }
        public int Video_ScalingAlgorithm_SelectedIndex
        {
            get { return _Video_ScalingAlgorithm_SelectedIndex; }
            set
            {
                if (_Video_ScalingAlgorithm_SelectedIndex == value)
                {
                    return;
                }

                _Video_ScalingAlgorithm_SelectedIndex = value;
                OnPropertyChanged("Video_ScalingAlgorithm_SelectedIndex");
            }
        }

        // Selected Item
        private string _Video_ScalingAlgorithm_SelectedItem { get; set; }
        public string Video_ScalingAlgorithm_SelectedItem
        {
            get { return _Video_ScalingAlgorithm_SelectedItem; }
            set
            {
                if (_Video_ScalingAlgorithm_SelectedItem == value)
                {
                    return;
                }

                _Video_ScalingAlgorithm_SelectedItem = value;
                OnPropertyChanged("Video_ScalingAlgorithm_SelectedItem");
            }
        }

        // Controls Enable
        private bool _Video_ScalingAlgorithm_IsEnabled = true;
        public bool Video_ScalingAlgorithm_IsEnabled
        {
            get { return _Video_ScalingAlgorithm_IsEnabled; }
            set
            {
                if (_Video_ScalingAlgorithm_IsEnabled == value)
                {
                    return;
                }

                _Video_ScalingAlgorithm_IsEnabled = value;
                OnPropertyChanged("Video_ScalingAlgorithm_IsEnabled");
            }
        }


        // --------------------------------------------------
        // Crop
        // --------------------------------------------------
        // Controls Enable
        private bool _Video_Crop_IsEnabled = true;
        public bool Video_Crop_IsEnabled
        {
            get { return _Video_Crop_IsEnabled; }
            set
            {
                if (_Video_Crop_IsEnabled == value)
                {
                    return;
                }

                _Video_Crop_IsEnabled = value;
                OnPropertyChanged("Video_Crop_IsEnabled");
            }
        }

        // -------------------------
        // Crop Width
        // -------------------------
        // Text
        private string _Video_Crop_Width_Text;
        public string Video_Crop_Width_Text
        {
            get { return _Video_Crop_Width_Text; }
            set
            {
                if (_Video_Crop_Width_Text == value)
                {
                    return;
                }

                _Video_Crop_Width_Text = value;
                OnPropertyChanged("Video_Crop_Width_Text");
            }
        }

        // -------------------------
        // Crop Height
        // -------------------------
        // Text
        private string _Video_Crop_Height_Text;
        public string Video_Crop_Height_Text
        {
            get { return _Video_Crop_Height_Text; }
            set
            {
                if (_Video_Crop_Height_Text == value)
                {
                    return;
                }

                _Video_Crop_Height_Text = value;
                OnPropertyChanged("Video_Crop_Height_Text");
            }
        }

        // -------------------------
        // Crop X
        // -------------------------
        // Text
        private string _Video_Crop_X_Text;
        public string Video_Crop_X_Text
        {
            get { return _Video_Crop_X_Text; }
            set
            {
                if (_Video_Crop_X_Text == value)
                {
                    return;
                }

                _Video_Crop_X_Text = value;
                OnPropertyChanged("Video_Crop_X_Text");
            }
        }

        // -------------------------
        // Crop X
        // -------------------------
        private string _Video_Crop_Y_Text;
        public string Video_Crop_Y_Text
        {
            get { return _Video_Crop_Y_Text; }
            set
            {
                if (_Video_Crop_Y_Text == value)
                {
                    return;
                }

                _Video_Crop_Y_Text = value;
                OnPropertyChanged("Video_Crop_Y_Text");
            }
        }



        // --------------------------------------------------
        // Crop Clear Button
        // --------------------------------------------------
        // Text
        private string _Video_CropClear_Text;
        public string Video_CropClear_Text
        {
            get { return _Video_CropClear_Text; }
            set
            {
                if (_Video_CropClear_Text == value)
                {
                    return;
                }

                _Video_CropClear_Text = value;
                OnPropertyChanged("Video_CropClear_Text");
            }
        }


    }
}
