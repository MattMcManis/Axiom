![https://github.com/MattMcManis/Axiom](https://raw.githubusercontent.com/MattMcManis/Axiom/screenshots/axiom-logo-128.png)

# Axiom
An FFmpeg GUI for Windows

* [Overview](#overview)
* [Features](#features)
* [Downloads](#downloads)
* [Installation](#installation)
* [Build](#build)

## Overview
Axiom generates command scripts to be processed by multimedia encoder, FFmpeg, and streams analyzer, FFprobe.

Convert to webm, mp4, mkv, ogv, mp3, m4a, ogg, flac, wav, png, jpg image & sequence.

![Axiom](https://github.com/MattMcManis/Axiom/blob/screenshots/axiom.png?raw=true)

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
#### Binary Releases
Axiom User Interface Standalone
<br />
Axiom + FFmpeg/FFprobe/FFplay 64-Bit Windows
<br />
Install Instructions & User Guide.
<br />
https://github.com/MattMcManis/Axiom/releases

FFmpeg Windows builds
<br />
https://ffmpeg.zeranoe.com/builds/

## Installation
Axiom is portable and can be run from any location on the computer.

1. Extract Axiom.FFmpeg.zip to a location of your choice.
2. Run the program Axiom.exe or create a shortcut on the Desktop.
2. It will automatically detect `ffmpeg.exe` and `ffprobe.exe` in the included `ffmpeg` folder.
3. If you move the `ffmpeg` folder, set Windows Environment Variables or choose path in the Axiom Configure Window.

#### Add FFmpeg to Environment Variables (optional):

* Move FFmpeg folder to a location of your choice, such as `C:\Program Files\`.
* Control Panel → System and Security → System → Advanced system settings
* Advanced Tab → Environment Variables → System variables → Path
* Add `C:\Program Files\FFmpeg\bin\`
* Separate multiple paths with semicolon `;`
* Typing `ffmpeg` in Command Prompt will now execute without needing to specify a direct path.

## Build
Visual Studio 2015
<br />
WPF, C#, XAML
<br />
Visual C++ 19.0 Compiler