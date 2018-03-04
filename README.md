![https://github.com/MattMcManis/Axiom](https://raw.githubusercontent.com/MattMcManis/Axiom/master/images/axiom-logo-128.png)

# Axiom
An FFmpeg GUI for Windows

* [Overview](#overview)
* [Features](#features)
* [Downloads](#downloads)
* [Installation](#installation)
* [Examples](https://github.com/MattMcManis/Axiom/wiki/Examples)
* [Build](#build)

## Overview
Axiom generates command scripts to be processed by multimedia encoder, FFmpeg, and streams analyzer, FFprobe.

Convert to webm, mp4, mkv, ogv, mp3, m4a, ogg, flac, wav, png, jpg image & sequence.

![Axiom](https://raw.githubusercontent.com/MattMcManis/Axiom/master/images/axiom.png)  
![Axiom Script](https://raw.githubusercontent.com/MattMcManis/Axiom/master/images/script.png)

### Features
* The Power of FFmpeg through a Minimal Interface
* Convert any media file to multiple formats
* Cut Video and Audio timeline
* Resize Video to standard aspect ratios
* Lossless, Constant and Variable Quality
* Auto-Quality Copy Modes
* Match-Quality Batch Processing
* Video to Image Sequence
* Custom User Defined Settings
* Command Script Generator

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

## Installation
Axiom is portable and can be run from any location on the computer.

1. Extract `Axiom.FFmpeg.zip` to a location of your choice.
2. Run the program Axiom.exe or create a shortcut on the Desktop.
3. It will automatically detect `ffmpeg.exe` and `ffprobe.exe` in the included `ffmpeg` folder.
4. If you move the `ffmpeg` folder, set Windows Environment Variables or choose path in the Axiom Configure Window.

#### Add FFmpeg to Environment Variables (optional):

1. Move FFmpeg folder to a location of your choice, such as `C:\Program Files\`.
2. Control Panel → System and Security → System → Advanced system settings
3. Advanced Tab → Environment Variables → System variables → Path
4. Add `C:\Program Files\FFmpeg\bin\`
5. Separate multiple paths with semicolon `;`
6. Typing `ffmpeg` in Command Prompt will now execute without needing to specify a direct path.

## Build
Visual Studio 2015
<br />
WPF, C#, XAML
<br />
Visual C++ 19.0 Compiler
