![https://github.com/MattMcManis/Axiom](https://raw.githubusercontent.com/MattMcManis/Axiom/screenshots/axiom-logo-128.png)

# Axiom
An FFmpeg GUI for Windows

## Overview
Axiom generates command scripts to be processed by multimedia encoder, FFmpeg, and streams analyzer, FFprobe.

It is portable and can be run from any location on the computer.

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
Axiom User Interface Standalone
<br />
Axiom + FFmpeg, FFprobe, FFplay 3.2.2 64-Bit Windows
<br />
Install Instructions & User Guide.
<br />
https://github.com/MattMcManis/Axiom/releases

FFmpeg Windows builds
<br />
https://ffmpeg.zeranoe.com/builds/

## Installation
Axiom first checks Window's Environment Variables to see if FFmpeg is installed and uses that location.
<br />
If not found, it will look for `ffmpeg.exe` and `ffprobe.exe` in `\ffmpeg\bin\`, within `Axiom.exe current folder`.

A custom path can also be defined in the Configuration Window.

#### Add FFmpeg to Environment Variables (optional):

* Move FFmpeg folder to a location of your choice, such as `C:\Program Files\`.
* Control Panel → System and Security → System → Advanced system settings
* Advanced Tab → Environment Variables → System variables → Path
* Add `C:\Program Files\FFmpeg\bin\`
* Separate multiple paths with semicolon `;`
* Typing `ffmpeg` in Command Prompt will now execute without needing to specify a direct path.
