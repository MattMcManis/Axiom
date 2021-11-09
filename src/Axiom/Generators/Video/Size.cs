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

/* ----------------------------------
 METHODS

 * Size Width Auto
 * Size
 * Scaling Algorithm
 * Crop
---------------------------------- */


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using ViewModel;
using Axiom;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Generate.Video
{
    public class Size
    {
        public static string width { get; set; }
        public static string height { get; set; }
        public static string scale { get; set; } // contains size, width, height
        public static string vAspectRatio { get; set; }
        public static string vScalingAlgorithm { get; set; }

        /// <summary>
        /// Size Width Auto
        /// <summary>
        public static String SizeWidthAuto(string codec_SelectedItem)
        {
            if (codec_SelectedItem == "VP8" ||
                codec_SelectedItem == "VP9" ||
                codec_SelectedItem == "AV1" ||
                codec_SelectedItem == "FFV1" ||
                codec_SelectedItem == "HuffYUV" ||
                codec_SelectedItem == "Theora" ||
                codec_SelectedItem == "JPEG" ||
                codec_SelectedItem == "PNG" ||
                codec_SelectedItem == "WebP")
            {
                return "-1";
            }
            else if (codec_SelectedItem == "x264" ||
                     codec_SelectedItem == "x265" ||
                     codec_SelectedItem == "H264 AMF" ||
                     codec_SelectedItem == "HEVC AMF" ||
                     codec_SelectedItem == "H264 NVENC" ||
                     codec_SelectedItem == "HEVC NVENC" ||
                     codec_SelectedItem == "H264 QSV" ||
                     codec_SelectedItem == "HEVC QSV" ||
                     codec_SelectedItem == "MPEG-2" ||
                     codec_SelectedItem == "MPEG-4")
            {
                return "-2";
            }

            return "-1";
        }


        /// <summary>
        /// Size Height Auto
        /// <summary>
        public static String SizeHeightAuto(string codec_SelectedItem)
        {
            if (codec_SelectedItem == "VP8" ||
                codec_SelectedItem == "VP9" ||
                codec_SelectedItem == "AV1" ||
                codec_SelectedItem == "FFV1" ||
                codec_SelectedItem == "HuffYUV" ||
                codec_SelectedItem == "Theora" ||
                codec_SelectedItem == "JPEG" ||
                codec_SelectedItem == "PNG" ||
                codec_SelectedItem == "WebP")
            {
                return "-1";
            }
            else if (codec_SelectedItem == "x264" ||
                     codec_SelectedItem == "x265" ||
                     codec_SelectedItem == "H264 AMF" ||
                     codec_SelectedItem == "HEVC AMF" ||
                     codec_SelectedItem == "H264 NVENC" ||
                     codec_SelectedItem == "HEVC NVENC" ||
                     codec_SelectedItem == "H264 QSV" ||
                     codec_SelectedItem == "HEVC QSV" ||
                     codec_SelectedItem == "MPEG-2" ||
                     codec_SelectedItem == "MPEG-4")
            {
                return "-2";
            }

            // Fallback
            return "-1";
        }


        /// <summary>
        /// Size
        /// <summary>
        // Size is a Filter
        public static void Scale(string codec_SelectedItem,
                                    string size_SelectedItem,
                                    string width_Text,
                                    string height_Text,
                                    string screenFormat_SelectedItem,
                                    //string aspectRatio_SelectedItem,
                                    string scalingAlgorithm_SelectedItem,
                                    string cropClear_Text
                                    )
        {
            // -------------------------
            // No
            // -------------------------
            if (size_SelectedItem == "Source")
            {
                // MP4/MKV Width/Height Fix
                if (codec_SelectedItem == "x264" ||
                    codec_SelectedItem == "x265" ||
                    codec_SelectedItem == "H264 AMF" ||
                    codec_SelectedItem == "HEVC AMF" ||
                    codec_SelectedItem == "H264 NVENC" ||
                    codec_SelectedItem == "HEVC NVENC" ||
                    codec_SelectedItem == "H264 QSV" ||
                    codec_SelectedItem == "HEVC QSV" ||
                    codec_SelectedItem == "MPEG-2" ||
                    codec_SelectedItem == "MPEG-4")
                {
                    width = "trunc(iw/2)*2";
                    height = "trunc(ih/2)*2";

                    // -------------------------
                    // Combine & Add Scaling Algorithm
                    // -------------------------
                    scale = "scale=" + width + ":" + height + ScalingAlgorithm(scalingAlgorithm_SelectedItem);

                    // Video Filter Add
                    Filters.Video.vFiltersList.Add(scale);
                }
            }

            // -------------------------
            // Yes
            // -------------------------
            else
            {
                // -------------------------
                // Preset Scale
                // -------------------------
                if (size_SelectedItem != "Custom")
                {
                    // Widescreen
                    if (screenFormat_SelectedItem == "auto" ||
                        screenFormat_SelectedItem == "Widescreen" ||
                        screenFormat_SelectedItem == "Ultrawide"
                    )
                    {
                        width = width_Text;
                        height = SizeHeightAuto(codec_SelectedItem);
                    }

                    // Full Screen
                    else if (screenFormat_SelectedItem == "Full Screen")
                    {
                        width = SizeWidthAuto(codec_SelectedItem);
                        height = height_Text;
                    }

                    // -------------------------
                    // Combine & Add Scaling Algorithm
                    // -------------------------
                    scale = "scale=" + width + ":" + height + ScalingAlgorithm(scalingAlgorithm_SelectedItem);

                    // Video Filter Add
                    Filters.Video.vFiltersList.Add(scale);
                }

                // -------------------------
                // Custom Size
                // -------------------------
                else if (size_SelectedItem == "Custom")
                {
                    // Get width height from custom textbox
                    width = width_Text;
                    height = height_Text;

                    // Change the left over Default empty text to "auto"
                    if (string.Equals(width_Text, "", StringComparison.OrdinalIgnoreCase))
                    {
                        VM.VideoView.Video_Width_Text = "auto";
                    }

                    if (string.Equals(height_Text, "", StringComparison.OrdinalIgnoreCase))
                    {
                        VM.VideoView.Video_Height_Text = "auto";
                    }

                    // -------------------------
                    // VP8, VP9, Theora
                    // -------------------------
                    if (codec_SelectedItem == "VP8" ||
                        codec_SelectedItem == "VP9" ||
                        codec_SelectedItem == "Theora" ||
                        codec_SelectedItem == "JPEG" ||
                        codec_SelectedItem == "PNG" ||
                        codec_SelectedItem == "WebP")
                    {
                        // If User enters "auto" or textbox is empty
                        if (string.Equals(width_Text, "auto", StringComparison.OrdinalIgnoreCase))
                        {
                            width = "-1";
                        }
                        if (string.Equals(height_Text, "auto", StringComparison.OrdinalIgnoreCase))
                        {
                            height = "-1";
                        }

                        // -------------------------
                        // Combine & Add Scaling Algorithm
                        // -------------------------
                        scale = "scale=" + width + ":" + height + ScalingAlgorithm(scalingAlgorithm_SelectedItem);

                        // Video Filter Add
                        Filters.Video.vFiltersList.Add(scale);
                    }


                    // -------------------------
                    // x264 & x265
                    // -------------------------
                    // Fix FFmpeg MP4 but (User entered value)
                    // Apply Fix to all scale effects above
                    //
                    else if (codec_SelectedItem == "x264" ||
                             codec_SelectedItem == "x265" ||
                             codec_SelectedItem == "H264 AMF" ||
                             codec_SelectedItem == "HEVC AMF" ||
                             codec_SelectedItem == "H264 NVENC" ||
                             codec_SelectedItem == "HEVC NVENC" ||
                             codec_SelectedItem == "H264 QSV" ||
                             codec_SelectedItem == "HEVC QSV" ||
                             codec_SelectedItem == "MPEG-2" ||
                             codec_SelectedItem == "MPEG-4")
                    {
                        // -------------------------
                        // Width = Custom value
                        // Height = Custom value
                        // -------------------------
                        if (!string.Equals(width_Text, "auto", StringComparison.OrdinalIgnoreCase) &&
                            !string.Equals(height_Text, "auto", StringComparison.OrdinalIgnoreCase))
                        {
                            // -------------------------
                            // Aspect Must be Cropped to be divisible by 2
                            // e.g. -vf "scale=777:777, crop=776:776:0:0"
                            // -------------------------
                            try
                            {
                                // Convert width and height from string to int
                                int width_int;
                                int.TryParse(width, out width_int);
                                int height_int;
                                int.TryParse(height, out height_int);

                                // Set divible crop placeholders
                                int divisibleWidthCrop = width_int;
                                int divisibleHeightCrop = height_int;

                                // Set Crop Position to default top left
                                string cropX = "0";
                                string cropY = "0";

                                // Width
                                if (width_int % 2 != 0)
                                {
                                    divisibleWidthCrop = width_int - 1;
                                }
                                // Height
                                if (height_int % 2 != 0)
                                {
                                    divisibleHeightCrop = height_int - 1;
                                }


                                // -------------------------
                                // Only if Crop is already Empty
                                // -------------------------
                                // User Defined Crop should always override Divisible Crop
                                // CropClear Button symbol * is used as an Identifier, Divisible Crop does not leave "Clear*"
                                //
                                if (cropClear_Text == "Clear") // Crop Set Check
                                {
                                    // -------------------------
                                    // Combine & Add Scaling Algorithm
                                    // -------------------------
                                    scale = "scale=" + width + ":" + height + ScalingAlgorithm(scalingAlgorithm_SelectedItem);
                                    Filters.Video.vFiltersList.Add(scale);

                                    // Divisible Crop - crop off the extra pixels
                                    if (width_int % 2 != 0 || height_int % 2 != 0)
                                    {
                                        CropWindow.crop = Convert.ToString("crop=" + divisibleWidthCrop + ":" + divisibleHeightCrop + ":" + cropX + ":" + cropY);
                                        Filters.Video.vFiltersList.Add(CropWindow.crop);
                                    }
                                }

                                // -------------------------
                                // If Crop has manually been set
                                // -------------------------
                                else if (cropClear_Text == "Clear*")
                                {
                                    // -------------------------
                                    // Combine & Add Scaling Algorithm
                                    // -------------------------
                                    // Manual Crop - Crop what you want out of the video
                                    Filters.Video.vFiltersList.Add(CropWindow.crop);

                                    // Scale - Resize video
                                    scale = "scale=" + width + ":" + height + ScalingAlgorithm(scalingAlgorithm_SelectedItem);
                                    Filters.Video.vFiltersList.Add(scale);

                                    // Divisible Crop - crop off the extra pixels
                                    if (width_int % 2 != 0 || height_int % 2 != 0)
                                    {
                                        string divisibleCrop = Convert.ToString("crop=" + divisibleWidthCrop + ":" + divisibleHeightCrop + ":" + cropX + ":" + cropY);
                                        Filters.Video.vFiltersList.Add(divisibleCrop);
                                    }
                                }
                            }
                            catch
                            {
                                // Log Console Message /////////
                                Log.WriteAction = () =>
                                {
                                    Log.logParagraph.Inlines.Add(new LineBreak());
                                    Log.logParagraph.Inlines.Add(new LineBreak());
                                    Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Must enter numbers only.")) { Foreground = Log.ConsoleWarning });
                                };
                                Log.LogActions.Add(Log.WriteAction);

                                /* lock */
                                // Warning
                                MessageBox.Show("Must enter numbers only.",
                                                "Notice",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Exclamation);
                            }

                        }

                        // -------------------------
                        // Width = auto
                        // Height = Custom value
                        // -------------------------
                        else if (string.Equals(width_Text, "auto", StringComparison.OrdinalIgnoreCase) &&
                                !string.Equals(height_Text, "auto", StringComparison.OrdinalIgnoreCase))
                        {
                            //Width
                            width = "-2";

                            // Height
                            // Make user entered height divisible by 2
                            try
                            {
                                // Convert Height TextBox Value to Int
                                int divisibleHeight = Convert.ToInt32(height);

                                // int convert check
                                if (int.TryParse(height, out divisibleHeight))
                                {
                                    // If not divisible by 2, subtract 1 from total
                                    if (divisibleHeight % 2 != 0)
                                    {
                                        divisibleHeight -= 1;
                                        height = Convert.ToString(divisibleHeight);
                                    }
                                }
                            }
                            catch
                            {
                                // Log Console Message /////////
                                Log.WriteAction = () =>
                                {
                                    Log.logParagraph.Inlines.Add(new LineBreak());
                                    Log.logParagraph.Inlines.Add(new LineBreak());
                                    Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Must enter numbers only.")) { Foreground = Log.ConsoleWarning });
                                };
                                Log.LogActions.Add(Log.WriteAction);

                                /* lock */
                                //MainWindow.ready = false;
                                // Warning
                                MessageBox.Show("Must enter numbers only.",
                                                "Notice",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Exclamation);
                            }
                        }

                        // -------------------------
                        // Width = Custom value
                        // Height = auto
                        // -------------------------
                        else if (!string.Equals(width_Text, "auto", StringComparison.OrdinalIgnoreCase) &&
                                    string.Equals(height_Text, "auto", StringComparison.OrdinalIgnoreCase))
                        {
                            // Height
                            height = "-2";

                            // Width
                            // Make user entered Width divisible by 2
                            try
                            {
                                // Convert Height TextBox Value to Int
                                int divisibleWidth = Convert.ToInt32(width);

                                // int convert check
                                if (int.TryParse(width, out divisibleWidth))
                                {
                                    // If not divisible by 2, subtract 1 from total
                                    if (divisibleWidth % 2 != 0)
                                    {
                                        divisibleWidth -= 1;
                                        width = Convert.ToString(divisibleWidth);
                                    }
                                }
                            }
                            catch
                            {
                                // Log Console Message /////////
                                Log.WriteAction = () =>
                                {
                                    Log.logParagraph.Inlines.Add(new LineBreak());
                                    Log.logParagraph.Inlines.Add(new LineBreak());
                                    Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Must enter numbers only.")) { Foreground = Log.ConsoleWarning });
                                };
                                Log.LogActions.Add(Log.WriteAction);

                                /* lock */
                                // Warning
                                MessageBox.Show("Must enter numbers only.",
                                                "Notice",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Exclamation);
                            }
                        }

                        // -------------------------
                        // Width = auto
                        // Height = auto
                        // -------------------------
                        else if (string.Equals(width_Text, "auto", StringComparison.OrdinalIgnoreCase) &&
                                    string.Equals(height_Text, "auto", StringComparison.OrdinalIgnoreCase))
                        {
                            // If User enters "auto" or textbox is empty
                            if (string.Equals(width_Text, "auto", StringComparison.OrdinalIgnoreCase))
                            {
                                width = "trunc(iw/2)*2";

                            }
                            if (string.Equals(height_Text, "auto", StringComparison.OrdinalIgnoreCase))
                            {
                                height = "trunc(ih/2)*2";
                            }
                        }

                    } //end codec check

                } //end custom

            } //end Yes


            // -------------------------
            // Filter Clear
            // -------------------------
            // Copy
            if (codec_SelectedItem == "Copy")
            {
                scale = string.Empty;

                // Video Filter Add
                if (Filters.Video.vFiltersList != null &&
                    Filters.Video.vFiltersList.Count > 0)
                {
                    Filters.Video.vFiltersList.Clear();
                    Filters.Video.vFiltersList.TrimExcess();
                }
            }


            // Log Console Message /////////
            Log.WriteAction = () =>
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Resize: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(size_SelectedItem.ToString()) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Width: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(width) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Height: ")) { Foreground = Log.ConsoleDefault });
                Log.logParagraph.Inlines.Add(new Run(height) { Foreground = Log.ConsoleDefault });
            };
            Log.LogActions.Add(Log.WriteAction);

        } //End Size



        /// <summary>
        /// Aspect Ratio
        /// <summary>
        public static String AspectRatio(string aspectRatio_SelectedItem)
        {
            // Note: Can be used with Video Codec Copy

            vAspectRatio = "-aspect ";

            // None & Default
            switch (aspectRatio_SelectedItem)
            {
                // auto
                case "auto":
                    vAspectRatio = string.Empty;
                    break;

                // 2.4:1
                case "2.4:1":
                    vAspectRatio += "240:100";
                    break;

                // 4:3, 16:9, etc.
                default:
                    vAspectRatio += aspectRatio_SelectedItem;
                    break;
            }

            return vAspectRatio;
        }



        /// <summary>
        /// Scaling Algorithm
        /// <summary>
        public static String ScalingAlgorithm(string scalingAlgorithm_SelectedItem)
        {
            // -------------------------
            // None & Default
            // -------------------------
            if (scalingAlgorithm_SelectedItem == "auto")
            {
                vScalingAlgorithm = string.Empty;
            }

            // -------------------------
            // Scaler
            // -------------------------
            else
            {
                vScalingAlgorithm = ":flags=" + scalingAlgorithm_SelectedItem;
            }

            return vScalingAlgorithm;
        }



        /// <summary>
        /// Crop (Method)
        /// <summary>
        public static void Crop(CropWindow cropwindow)
        {
            // -------------------------
            // Clear
            // -------------------------
            // Clear leftover Divisible Crop if not x264/x265
            // CropClearButton is used as an Identifier, Divisible Crop does not leave "Clear*"
            if (VM.VideoView.Video_Codec_SelectedItem != "x264" &&
                VM.VideoView.Video_Codec_SelectedItem != "x265" &&
                VM.VideoView.Video_Codec_SelectedItem != "H264 AMF" &&
                VM.VideoView.Video_Codec_SelectedItem != "HEVC AMF" &&
                VM.VideoView.Video_Codec_SelectedItem != "H264 NVENC" &&
                VM.VideoView.Video_Codec_SelectedItem != "HEVC NVENC" &&
                VM.VideoView.Video_Codec_SelectedItem != "H264 QSV" &&
                VM.VideoView.Video_Codec_SelectedItem != "HEVC QSV" &&
                VM.VideoView.Video_Codec_SelectedItem != "MPEG-2" &&
                VM.VideoView.Video_Codec_SelectedItem != "MPEG-4" &&
                VM.VideoView.Video_CropClear_Text == "Clear"
                )
            {
                CropWindow.crop = string.Empty;
            }

            // Clear Crop if MediaTypeControls is Audio
            if (VM.FormatView.Format_MediaType_SelectedItem == "Audio")
            {
                CropWindow.crop = string.Empty;
            }

            // -------------------------
            // Add Crop to Video Filters if Not Null
            // -------------------------
            // If Crop is set by User in the CropWindow
            if (!string.IsNullOrWhiteSpace(CropWindow.crop))
            {
                // Video Filters Add
                Filters.Video.vFiltersList.Add(CropWindow.crop);
            }
        }
    }
}
