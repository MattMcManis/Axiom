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

 * Generate FFmpeg Args
---------------------------------- */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using ViewModel;
using Axiom;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Generate
{
    public partial class FFmpeg
    {
        public static string nameVariable { get; set; }

        public class YouTubeDL
        {
            /// <summary>
            /// YouTube Download - Generate Args
            /// </summary>
            public static void Generate_FFmpegArgs()
            {
                // Log Console Message /////////
                Log.WriteAction = () =>
                {
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("YouTube Download: ")) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new Run(Convert.ToString(VM.MainView.Batch_IsChecked)) { Foreground = Log.ConsoleDefault });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Bold(new Run("Generating Script...")) { Foreground = Log.ConsoleTitle });
                    Log.logParagraph.Inlines.Add(new LineBreak());
                    Log.logParagraph.Inlines.Add(new Run(FFmpeg.ffmpegArgs) { Foreground = Log.ConsoleDefault });
                };
                Log.LogActions.Add(Log.WriteAction);

                // -------------------------
                // Generate the youtube-dl Output Path and break it into sections for args below
                // -------------------------
                MainWindow.OutputPath();
                //string outputPath = MainWindow.OutputPath();
                //string outputDir = Path.GetDirectoryName(VM.MainView.Output_Text).TrimEnd('\\') + @"\"; // eg. C:\Output\Path\
                //string outputFileName = Path.GetFileNameWithoutExtension(outputPath);
                //string output = Path.Combine(MainWindow.outputDir, MainWindow.outputFileName + MainWindow.outputExt);

                // -------------------------
                // Generate Download Script
                // -------------------------
                List<string> youtubedlArgs = new List<string>();

                // -------------------------
                // Set Name Variable
                // -------------------------
                //string nameVariable = string.Empty;

                switch (VM.ConfigureView.Shell_SelectedItem)
                {
                    // -------------------------
                    // CMD
                    // -------------------------
                    case "CMD":
                        nameVariable = "%f";

                        // -------------------------
                        // YouTube Download Arguments Full
                        // -------------------------
                        youtubedlArgs = new List<string>()
                        {
                            "cd /d",
                            "\"" + MainWindow.outputDir + "\"",

                            "\r\n\r\n" + 
                            "&&",

                            "\r\n\r\n" + 
                            "for /f \"delims=\" %f in ('",

                            // Get Title
                            "\r\n\r\n" + 
                            "@" + "\"" + MainWindow.youtubedl + "\"",

                            "\r\n" + 
                            "--get-filename -o \"%(title)s\" " + "\"" + MainWindow.YouTubeDownloadURL(VM.MainView.Input_Text) + "\"",

                            "\r\n" + 
                            "')",

                            // Download Video
                            "\r\n\r\n" + 
                            "do (",

                            "\r\n\r\n" + 
                            "@" + "\"" + MainWindow.youtubedl + "\"",

                            "\r\n\r\n" + 
                            "-f " + MainWindow.YouTubeDownloadQuality(VM.MainView.Input_Text,
                                                                      VM.FormatView.Format_YouTube_SelectedItem,
                                                                      VM.FormatView.Format_YouTube_Quality_SelectedItem
                                                                     ),

                            "\r\n\r\n" + 
                            "\"" + VM.MainView.Input_Text + "\"",

                            "\r\n" + 
                            "-o " + "\"" + MainWindow.outputDir + MainWindow.outputFileName + "." + MainWindow.YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
                                                                                                               VM.VideoView.Video_Codec_SelectedItem,
                                                                                                               VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                                                                               VM.AudioView.Audio_Codec_SelectedItem
                                                                                                              ) + 
                                    "\"",

                            // FFmpeg Location
                            "\r\n\r\n" +
                            MainWindow.YouTubeDL_FFmpegPath(),

                            // Merge Output Format
                            "\r\n\r\n" + "--merge-output-format " + MainWindow.YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
                                                                                                     VM.VideoView.Video_Codec_SelectedItem,
                                                                                                     VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                                                                     VM.AudioView.Audio_Codec_SelectedItem
                                                                                                    )
                        };
                        break;

                    // -------------------------
                    // PowerShell
                    // -------------------------
                    case "PowerShell":
                        nameVariable = "$name";

                        // Format youtube-dl Path
                        string youtubedl_formatted = string.Empty;
                        // Check if youtube-dl is not default, if so it is a user-defined path, wrap in quotes
                        // If default, it will not be wrapped in quotes
                        if (MainWindow.youtubedl != "youtube-dl")
                        {
                            youtubedl_formatted = MainWindow.WrapWithQuotes(MainWindow.youtubedl);
                        }
                        else
                        {
                            youtubedl_formatted = MainWindow.youtubedl;
                        }

                        // youtube-dl Args List
                        youtubedlArgs = new List<string>()
                        {
                            // Get Title
                            "$name =" + " & " + youtubedl_formatted + " " + "--get-filename -o \"%(title)s\" " + "\"" + MainWindow.YouTubeDownloadURL(VM.MainView.Input_Text) + "\"" + ";",

                            //"\r\n\r\n" ,

                            // Download Video
                            "\r\n\r\n" + 
                            "& " + youtubedl_formatted,

                            "\r\n\r\n" + 
                            "-f " + MainWindow.YouTubeDownloadQuality(VM.MainView.Input_Text,
                                                                      VM.FormatView.Format_YouTube_SelectedItem,
                                                                      VM.FormatView.Format_YouTube_Quality_SelectedItem
                                                                     ),
                            "\r\n\r\n" + 
                            "\"" + VM.MainView.Input_Text + "\"",

                            "\r\n" + 
                            "-o " + "\"" + Path.Combine(MainWindow.outputDir, MainWindow.outputFileName + "." + MainWindow.YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
                                                                                                                           VM.VideoView.Video_Codec_SelectedItem,
                                                                                                                           VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                                                                                           VM.AudioView.Audio_Codec_SelectedItem
                                                                                                                          )
                                                        ) + 
                                    "\"",

                            // FFmpeg Location
                            "\r\n\r\n" +
                            MainWindow.YouTubeDL_FFmpegPath(),

                            // Merge Output Format
                            "\r\n\r\n" + 
                            "--merge-output-format " + MainWindow.YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
                                                                                        VM.VideoView.Video_Codec_SelectedItem,
                                                                                        VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                                                        VM.AudioView.Audio_Codec_SelectedItem
                                                                                       )
                        };
                        break;
                }

                // FFmpeg Args
                //
                List<string> ffmpegArgsList = new List<string>()
                {
                    "\r\n\r\n" +
                    Sys.Shell.Shell_LogicalOperator_And(),
                };

                // Add Arguments to ffmpegArgsList
                switch (VM.VideoView.Video_Pass_SelectedItem)
                {
                    // -------------------------
                    // CRF
                    // -------------------------
                    case "CRF":
                        ffmpegArgsList.Add("\r\n\r\n" + FFmpeg.CRF.Arguments());
                        break;

                    // -------------------------
                    // 1 Pass
                    // -------------------------
                    case "1 Pass":
                        ffmpegArgsList.Add("\r\n\r\n" + FFmpeg._1_Pass.Arguments());
                        break;

                    // -------------------------
                    // 2 Pass
                    // -------------------------
                    case "2 Pass":
                        ffmpegArgsList.Add("\r\n\r\n" + FFmpeg._2_Pass.Arguments());
                        break;

                    // -------------------------
                    // Empty, none, auto / Audio
                    // -------------------------
                    default:
                        ffmpegArgsList.Add("\r\n\r\n" + FFmpeg._1_Pass.Arguments());
                        break;
                }

                // Add Logical Operator &&
                ffmpegArgsList.Add("\r\n\r\n" + Sys.Shell.Shell_LogicalOperator_And());

                // Add Delete Downloaded File
                ffmpegArgsList.Add("\r\n\r\n" + "del " + "\"" + Path.Combine(MainWindow.downloadDir, nameVariable + "." + MainWindow.YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
                                                                                                                                                           VM.VideoView.Video_Codec_SelectedItem,
                                                                                                                                                           VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                                                                                                                           VM.AudioView.Audio_Codec_SelectedItem
                                                                            )
                                                                ) + 
                                                                                                    
                                                        "\"");


                // -------------------------
                // Download-Only
                // -------------------------
                if (MainWindow.IsWebDownloadOnly(VM.VideoView.Video_Codec_SelectedItem,
                                                 VM.SubtitleView.Subtitle_Codec_SelectedItem,
                                                 VM.AudioView.Audio_Codec_SelectedItem) == true
                                                 )
                {
                    // Add "do" Closing Tag
                    // CMD
                    if (VM.ConfigureView.Shell_SelectedItem == "CMD")
                    {
                        youtubedlArgs.Add("\r\n)");
                    }

                    // Join List with Spaces
                    // Remove: Empty, Null, Standalone LineBreak
                    FFmpeg.ffmpegArgsSort = string.Join(" ", youtubedlArgs
                                                  .Where(s => !string.IsNullOrWhiteSpace(s))
                                                  .Where(s => !s.Equals(Environment.NewLine))
                                                  .Where(s => !s.Equals("\r\n\r\n"))
                                                  .Where(s => !s.Equals("\r\n"))
                                                  .Where(s => !s.Equals("\n"))
                                                  .Where(s => !s.Equals("\u2028"))
                                                  .Where(s => !s.Equals("\u000A"))
                                                  .Where(s => !s.Equals("\u000B"))
                                                  .Where(s => !s.Equals("\u000C"))
                                                  .Where(s => !s.Equals("\u000D"))
                                                  .Where(s => !s.Equals("\u0085"))
                                                  .Where(s => !s.Equals("\u2028"))
                                                  .Where(s => !s.Equals("\u2029"))
                                        );

                    // Inline 
                    FFmpeg.ffmpegArgs = MainWindow.RemoveLineBreaks(FFmpeg.ffmpegArgsSort);
                }

                // -------------------------
                // Download & Convert
                // -------------------------
                else
                {
                    // Join YouTube Args & FFmpeg Args
                    youtubedlArgs.AddRange(ffmpegArgsList);
                    // Add "do" Closing Tag
                    // CMD
                    if (VM.ConfigureView.Shell_SelectedItem == "CMD")
                    {
                        youtubedlArgs.Add("\r\n)");
                    }

                    // Join List with Spaces
                    // Remove: Empty, Null, Standalone LineBreak
                    FFmpeg.ffmpegArgsSort = string.Join(" ", youtubedlArgs
                                                  .Where(s => !string.IsNullOrWhiteSpace(s))
                                                  .Where(s => !s.Equals(Environment.NewLine))
                                                  .Where(s => !s.Equals("\r\n\r\n"))
                                                  .Where(s => !s.Equals("\r\n"))
                                                  .Where(s => !s.Equals("\n"))
                                                  .Where(s => !s.Equals("\u2028"))
                                                  .Where(s => !s.Equals("\u000A"))
                                                  .Where(s => !s.Equals("\u000B"))
                                                  .Where(s => !s.Equals("\u000C"))
                                                  .Where(s => !s.Equals("\u000D"))
                                                  .Where(s => !s.Equals("\u0085"))
                                                  .Where(s => !s.Equals("\u2028"))
                                                  .Where(s => !s.Equals("\u2029"))
                                     );

                    // Inline 
                    FFmpeg.ffmpegArgs = MainWindow.RemoveLineBreaks(FFmpeg.ffmpegArgsSort);
                }
            }
        }
    }
}
