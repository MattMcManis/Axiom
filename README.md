![https://github.com/MattMcManis/Axiom](https://raw.githubusercontent.com/MattMcManis/Axiom/master/images/axiom-logo-128.png)

# Axiom
An FFmpeg GUI for Windows

* [Overview](#overview)
* [Features](#features)
* [Downloads](#downloads)
* [Installation](#installation)
* [Examples](https://github.com/MattMcManis/Axiom/wiki/Examples)
* [Build](#build)

&nbsp;

## Overview
Axiom generates command scripts to be processed by multimedia encoder, [FFmpeg](https://www.ffmpeg.org), and streams analyzer, [FFprobe](https://ffmpeg.org/ffprobe.html).

Convert to `webm`, `mp4`, `mkv`, `avi`, `ogv`, `mp3`, `m4a`, `ogg`, `flac`, `wav`, `png`, `jpg`, `webp` and more.

![Axiom](https://raw.githubusercontent.com/MattMcManis/Axiom/master/images/axiom.png)  

&nbsp;

### Features
* The Power of FFmpeg in a Minimal Interface
* Convert any media file to multiple formats
* Cut Video and Audio timeline
* Resize Video to standard aspect ratios
* Lossless, Constant and Variable Quality
* Auto-Quality Copy Modes
* Match-Quality Batch Processing
* Video to Image Sequence
* Custom User Defined Settings
* Command Script Generator

&nbsp;

## Downloads
#### Releases
https://github.com/MattMcManis/Axiom/releases

* Axiom User Interface Standalone
* Axiom + FFmpeg 64-Bit Windows
* Install Instructions & User Guide.

#### Requires
[Microsoft .NET Framework 4.5](https://www.microsoft.com/en-us/download/details.aspx?id=30653)

#### FFmpeg Builds
https://ffmpeg.zeranoe.com/builds/

&nbsp;

## Installation
Axiom is portable and can be run from any location on the computer.

1. Extract `Axiom.FFmpeg.7z` to a location of your choice.
2. Run the program `Axiom.exe` or create a shortcut on the Desktop.
3. It will automatically detect `ffmpeg.exe` and `ffprobe.exe` in the included `ffmpeg` folder.
4. If you move the `ffmpeg` folder, set Windows Environment Variables or choose path in the Axiom Settings Tab.

#### Add FFmpeg to Environment Variables (optional):

1. Move FFmpeg folder to a location of your choice, such as `C:\Program Files\`.
2. Control Panel → System and Security → System → Advanced system settings
3. Advanced Tab → Environment Variables → System variables → Path
4. Add `C:\Program Files\FFmpeg\bin\`
5. Separate multiple paths with semicolon `;`
6. Typing `ffmpeg` in Command Prompt will now execute without needing to specify a direct path.

<a href="https://raw.githubusercontent.com/MattMcManis/Axiom/master/images/guide/Environment-Variables.png" target="_blank"><img src="https://raw.githubusercontent.com/MattMcManis/Axiom/master/images/guide/Environment-Variables.png" width="500"/></a> 

#### YouTube Download
`youtube-dl.exe` is included in `Axiom.FFmpeg.7z`. 

`Axiom` will auto-detect it in its included folder, or you can specify a custom path in the Axiom Settings Tab.  
Note: `youtube-dl` may not work correctly if located in `C:\Program Files\` due to Administrator Privileges.

1. Paste a YouTube URL into the Input TextBox
2. To Download the file without Converting, select Preset: YouTube-DL → Video or Music, Press Convert
3. To Download and Convert, select any Presets or Settings you need and Press Convert 
4. To Generate a Batch Script without Downloading, select any Settings you need and Press Script

&nbsp;

## Build
Visual Studio 2015
<br />
WPF, C#, XAML
<br />
Visual C++ 19.0 Compiler

&nbsp;

[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=VTUE7KQ8RS3DN) 

Thank you for your support.