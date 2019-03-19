# Changelog

Axiom FFmpeg UI

## [alpha]

## [1.5.6.0] - Mar 19, 2019
### Changed
- Redesigned Encoding Pass Controls
- Changed Video/Image Frame Range Controls
- Changed FFmpeg, FFprobe, FFplay, Log Paths to `MVVM`

### Fixed
- Output Log


## [1.5.5.0] - Mar 18, 2019
### Added
- Container ComboBox Headers
- Video `HuffYUV` Codec
- Video Codec Parameters
- CBR to `Theora`

### Fixed
- Audio Bitrate VBR FFprobe `NA`


## [1.5.4.1] - Mar 17, 2019
### Added
- Preset `HEVC`

### Fixed
- WebM Video VBR


## [1.5.4.0] - Mar 16, 2019
### Added
- Aspect Ratio ComboBox

### Changed
- Each Video Codec given its own Pixel Format List
- `AVI` Default Audio Changed from `AC3` to `LAME`
- `MPG` Default Audio Changed from `AC3` to `MP2`

### Fixed
- `MP2` Audio Codec
- Size Custom


## [1.5.3.3] - Mar 15, 2019
### Changed
- Optimized Audio Class
- Reordered Pixel Format ComboBox

### Fixed
- Audio VBR Calculator
- Opus VBR Auto
- Custom Minrate, Maxrate, Bufsize Preset Clear


## [1.5.3.2] - Mar 14, 2019
### Changed
- Refactored Codec Classes and Controls
- Improved MediaType ComboBox Control
- Disabled CRF VBR Button for `VP8`, `x264`
- CropWindow ViewModel Binding

### Fixed
- CRF Custom


## [1.5.3.1] - Mar 13, 2019
### Added
- `VP8`, `VP9`, `x264`, `x265` Video VBR
- VBR Calculator to Audio Quality Custom

### Fixed
- Video Quality Custom
- `MPEG-2`, `MPEG-4` Lossless


## [1.5.3.0] - Mar 13, 2019
### Added
- `FFV1` Video Codec


## [1.5.2.1] - Mar 13, 2019
### Fixed
- Video Pass ComboBox Display
- Optimize Custom `x264`, `x265`


## [1.5.2.0] - Mar 12, 2019
### Added
- Audio Auto Codec Copy Filter Checks

### Changed
- Filters to `MVVM`

### Fixed
- ScriptView Modify and Clear


## [1.5.1.0] - Mar 11, 2019
### Fixed
- LogPath Settings Save
- Media Type Image Audio Quality Controls
- VP8 Subtitle Enable Controls
- Preset ComboBox Crash on Auto Codec Copy

### Changed
- Improved Auto Codec Copy Controls
- Modified Video Pixel Format Controls
- Organized Theme files
- ScriptView RichTextBox to `MVVM`


## [1.5.0.0] - Mar 10, 2019
### Changed
- Complete program redesign
- Migrated to `MVVM`


## [1.2.9.2] - Feb 13, 2019
### Changed
- Priority of Video Filters `Size` and `Crop`

### Fixed
- Codec Copy Controls


## [1.2.9.1] - Jul 17, 2018
### Removed
- force `m4a` format

### Fixed
- Batch extension duplicate


## [1.2.9.0] - Jul 17, 2018
### Added
- Added `MP2` Audio Codec
- Added Subtitle Codec to Presets

### Changed
- Improved MVVM
- Improved Video Bitrate Display
- Improved Batch Extension Check
- Crop Clear Notification from `~` to `Clear*`

### Fixed
- Codec Copy Item Source check
- Batch Codec Copy


## [1.2.8.0] - Jul 14, 2018
### Added
- Update User Agent
- `JPEG` & `WebP` Custom Quality
- Optimized Video Quality Function
- Optimized Audio Quality Function
- Optimized Video Bitrate Calculator
- Adjusted Quality Presets

### Removed
- Additional VBR Rules from Format Controls
- `Theora` Lossless
- `Theora` 2 Pass

### Changed
- Image `auto Pass` to `1 Pass`

### Fixed
- `FLAC`, `ALAC`, `PCM` Bitrate if not Lossless
- `SSA` Subtitles Codec


## [1.2.7.0] - Jul 12, 2018
### Added
- `webp` Format
- Script Edited Check
- Pixel Format `bgra`
- Pixel Format `rgb24`
- Replace Line Breaks with Space
- Convert Button Auto Sort Script

### Changed
- Improved Remove Linebreaks
- Improved ScriptView ClearImproved Sort & Inline
- Improved Sort & Inline
- Optimized Remove Line Breaks
- Auto Quality Notice
- `JPEG` codec from auto to `-mjpeg`
- `PNG` codec from `auto` to `-png`
- `JPEG` pixel format to `yuvj444p`
- Updated User Guide


## [1.2.6.1] - Jul 11, 2018
### Added
- New Line Add Spaces between FFmpeg arguments

### Changed
- Update Check to New Thread
- Improved Remove Linebreaks

### Fixed
- Preview Button when no Environment Variables


## [1.2.6.0] - Jul 11, 2018
### Added
- Audio Filter Fix - Remove Click
- Audio Filters EQ - Lowpass, Highpass
- Audio Filters Dynamics - Contrast, Extra Stereo, Headphones
- Audio Filter Timing - Tempo

### Changed
- Moved Update Check to Window_Loaded
- Format - Speed to Encode Speed
- `mpeg2` to `MPEG-2`
- `mpeg4` to `MPEG-4`
- Audio Hard Limiter TextBox to Slider

### Removed
- `x264` DVD Optimize


## [1.2.5.0] - Jul 7, 2018
### Added
- MVVM ViewModel
- Filters Video & Audio Tabs
- `m2v` container
- `mpg` container
- `mpeg2` Video Codec
- Minrate, Maxrate, Bufsize Quality Preset Display
- `jpg` and `png` Pixel Format

### Changed
- Optimize Presets PC HD, PC SD, Animation
- DVD Preset to `.mpg` / `mpeg2` / `ac3`
- Subtitles ComboBox to SubtitlesStream
- Optimized Pixel Format

### Fixed
- Maxrate TextBox on Focus
- `jpg` and `png` Force Format


## [1.2.4.1] - Jul 4, 2018
### Added
- `VP8` Speed Modifier for realtime
- Pixel Format Codec Copy check
- Scaling Codec Copy check

### Changed
- Scaling ComboBox `None` to `default`
- `Vorbis`, `Opus`, `AAC`, `AC3`, `LAME` codec Bit Depth to `auto`

### Fixed
- `-x265-params` 2-Pass Auto, Lossless


## [1.2.4.0] - Jul 4, 2018
### Added
- Load Input/Ouput Previous Path Error Catch
- Subtitles Tab
- Filters Tab
- Video Filters Deband, Deshake, Deflicker, Dejudder Denoise
- Video Filters Selective Color
- Video Filters EQ - Brightness, Contrast, Saturation, Gamma
- `x265` Params List
- 1-Pass/CRF Force Format option
- FPS options `ntsc`, `pal`, `film`
- Filter Sliders Middle Point Scaler
- Filters Codec Copy Reset

### Changed
- Optimized FPS
- Optimized `x265` CRF Params
- Optimized `x265` Profiles
- `-x264opts` to `-x264-params`
- Video & Audio Auto Quality Warning

### Fixed
- Batch JPG to PNG transparancy filter


## [1.2.3.0] - Jun 27, 2018
### Added
- Script Clear Button
- Burn Subtitles

### Changed
- Video and Audio Controls ItemSource Method

### Fixed
- Audio Codec Copy


## [1.2.2.1] - Jun 26, 2018
### Fixed
- `JPEG` and `PNG` Codec Copy


## [1.2.2.0] - Jun 26, 2018
### Added
- New Controls ItemSource Method

### Removed
- Codec Copy Controls Insert
- `JPEG` and `PNG` Codec Copy

### Fixed
- Selected Threads Custom Number Duplicate
- Custom Filename Renamed on Format Container Change


## [1.2.0.0] - Jun 25, 2018
### Added
- Threads default and optimal
- Optimize Controls Method
- `x265` Optimize Tune
- maxrate, minrate, bufsize

### Changed
- Disabled Video FPS, Scaling with Copy Codec

### Fixed
- Audio Custom Bitrate textbox


## [1.1.9.0] - Jun 15, 2018
### Added
- `AV1` video codec
- Optimize `Tune`, `Profile`, `Level` comboboxes to Video tab

### Removed
- Optimized Advanced Window

### Changed
- Rearranged Format, Video, Audio comboboxes


## [1.1.8.1] - Apr 5, 2018

### Fixed
- Output Path on Empty

## [1.1.8.0] - May 16, 2018
### Added
- Speed None

### Removed
- MVVM (Temporary)

### Fixed
- Codecs ComboBox Change
- Codec Copy


## [1.1.7.0] - May 2, 2018
### Added
- MVVM
- Channel ComboBox enable/disable
- Subtitle Copy

### Changed
- Optimized Codec Copy
- Codec Copy

### Fixed
- `mpeg4` Encoding Pass ComboBox
- Crop Clear Identifier


## [1.1.5.4] - Apr 17, 2018
### Added
- Reset Settings Confirm

### Removed
- `mpeg4` Speed Presets

### Changed
- 2-Pass Log to Output Dir

### Fixed
- `mpeg4` VBR Quality Presets 


## [1.1.5.3] - Apr 17, 2018
## Fixed
- Subtitle Map  


## [1.1.5.2] - Apr 17, 2018
### Added
- Default Subtitle CheckBox
- Force Format on Two-Pass, Pass 2


## [1.1.5.0] - Apr 16, 2018
### Added
- Subtitle Codec
- External Subtitles
- MessageBox Icons

### Removed
- Quality Preset Speed settings, default Medium

### Fixed
- Sort on Empty Script Error
- `avi` Size filter
- Load Theme Error


## [1.1.4.1] - Apr 10, 2018
### Added
- Drag and Drop Output Filename


## [1.1.4.0] - Apr 9, 2018
### Added
- Drag and Drop File to TextBox

### Changed
- Disabled Administrator Rights Requirement to work with Drag and Drop

### Fixed
- Keep Window Toggle Save Settings
- Output Filename on manually entering Input Filepath


## [1.1.2.0] - Apr 6, 2018
### Added
- Mute disables Audio Controls

### Changed
- ComboBox Size: No to Source
- Width/Height Textboxes to display `auto`

### Fixed
- Threads selection


## [1.1.1.0] - Apr 5, 2018
### Added
- Themes Update


## [1.1.0.0] - Apr 5, 2018
### Added
- New Improved Interface

### Changed
- Improved Crop Clear



## [1.0.6.0] - Apr 3, 2018
### Added
- `avi` format
- `mpeg4` codec
- space to File Renamer number count
- Improved Input/Output RestoreDirectory

### Fixed
- Batch input directory on base drive `C:\`
- Batch output directory on base drive `C:\`
- ScriptView window size on low resolution


## [1.0.5.0] - Mar 28, 2018
### Removed
- `x265` Optimize Advanced

### Changed
- Adjusted `x265` Video Quality Presets

### Fixed
- `x265` Custom CRF


## [1.0.4.9] - Feb 7, 2018
### Removed
- Removed `x264` `-maxrate`

### Changed
- Adjusted Presets


## [1.0.4.8] - Mar 12, 2018
### Removed
- Background Worker

### Changed
- Optimized MainWindow XAML
- Optimized RichTextBox Clear

### Fixed
- File Properties Window
- Log Console Version Dispaly


## [1.0.4.7] - Mar 6, 2018
### Added
- Video Quality Preset Bit-rate & CRF display
- Audio Quality Preset Bit-rate display

### Changed
- KeepWindow Toggle default to true

### Fixed
- `wav` `PCM` codec and bitdepth
- Window Load position error prevention


## [1.0.4.6] - Mar 4, 2018
### Fixed
- New Window Crash on Ultra-Widescreen resolution
- Debug Write Crash on Auto Mode without Input File


## [1.0.4.5] - Mar 1, 2018
### Fixed
- Update Button Security Protocol Type, preventing download.


## [1.0.4.4] - Feb 27, 2018
### Fixed
- Crop Height


## [1.0.4.3] - Feb 25, 2018
### Added
- `-pix_fmt yuv420p` to `x264`/`x265` bitrate
- Audio 5.1 Channel `-ac 6` for downmix 6.1 to 5.1
- Audio `-rematrix_maxval 1.0` to prevent downmix clipping

### Changed
- Optimized Video Quality Presets 


## [1.0.4.2] - Feb 19, 2018
### Fixed
- Duplicate video bitrate argument in preset


## [1.0.4.1] - Feb 7, 2018
### Added
- `-pix_fmt yuv420p` to Video Custom Quality

### Changed
- `-scodec` to `-c:s`

### Fixed
- Audio Mute Check
- Mute removes all other audio arguments
- Custom Video Bitrate in CRF


## [1.0.3.2] - Nov 24, 2017
### Added
- `cd /d` to batch commands
- Video and Audio Format List

### Removed
- Unneeded maps on audio formats

### Changed
- Optimized FFprobe Format Entry Type
- Optimized maps

### Fixed
- Batch Audio VBR


## [1.0.2.0] - Sep 8, 2017
### Added
- MainWindow & ScriptView Window_Loaded

### Changed
- Rounded Video & Audio Auto Bitrate
- Simplified FFprobe Class


## [1.0.1.2] - Aug 24, 2017
### Changed
- Further isolated Single and Batch Convert


## [1.0.1.1] - Aug 23, 2017
### Changed
- File Renamer

### Fixed
- Output Path
- Sequence Output Name


## [1.0.1.0] - Aug 18, 2017
### Added
- Video Condition Checks

### Changed
- Improved Audio Auto VBR
- Improved File Renamer


## [1.0.0.3] - Aug 14, 2017
### Added
- Save Window Position on Exit

### Changed
- Refactored Script View Sort Expand
- Active/Inactive "Open File Location" Button
- Browse Button to Input
- Refactored File Renamer
- Refactored Script View Sort Expand


## [1.0.0.2] - Aug 13, 2017
### Changed
- Refactored Ready, Script, FFcheck, & Sort Switches
- Refactored FFprobe Path Set
- Refactored Ready Halts
- Unused Code Cleanup


## [1.0.0.1] - Aug 13, 2017
### Added
- List Clear Null Checks

### Changed
- Refactored Audio
- Refactored Audio VBR Calculator


## [1.0.0.0] - Aug 13, 2017
### Fixed
- Batch Auto Codec Copy


## [0.9.9.2] - Aug 8, 2017
### Fixed
- Video Bitrate Custom missing space


## [0.9.9.1] - Aug 1, 2017
### Fixed
- Controls Change on Output Button


## [0.9.9.0] - Jul 31, 2017
### Changed
- Organized Image Resources
- Refactored Window Names
- Refactored ConfigureWindow
- Refactored Streams
- Adjusted Themes
- Updated User Guide PDF

### Fixed
- Subtitle Codec on Audio
- `jpg` and `png` vframes
- `ogv` no audio when 2-Pass
- Controls change on Output Button


## [0.9.8.1] - Jul 29, 2017
### Changed
- Moved Optimize Advanced Temp Strings

### Fixed
- PS3 Preset
- `x264`/`x265` Custom Resize/Crop

## [0.9.8.0] - Jul 28, 2017
### Added
- Chapters Map

### Changed
- Optimized 2-Pass
- Optimized Subtitles
- Optimized Stream Maps


## [0.9.7.1] - Jul 27, 2017
### Fixed
- 2-Pass `-pass 1` variables clear


## [0.9.7.0] - Jul 26, 2017
### Added
- Script Save Function
- Script Sort Function

### Changed
- Improved Load Saved Settings
- Adjusted System Theme
- Adjusted Log/Debug Console Text Theme

### Fixed
- `x264`/`x265` Scale Down Bug


## [0.9.6.0] - Jul 18, 2017
### Changed
- Updated User Guide PDF


## [0.9.5.0] - Jul 14, 2017
### Added
- System Theme

### Changed
- Improved Batch Script
- Theme Color Adjustments
- Refactored Methods
- Optimized CMD Batch Script

### Fixed
- `x264`/`x265` Width Scale Bug
- 2-Pass Auto Quality
- CRF Var/TextBox Value OnChange to 2-Pass
- FFmpeg/FFprobe Path Auto Reset


## [0.9.3.0] - Jun 22, 2017
### Added
- Debug Button

### Changed
- Moved Configure Window

### Fixed
- Output Format Controls


## [0.9.2.0] - Jun 7, 2017
### Fixed
- Format/Codec Selection


## [0.9.1.0] - May 17, 2017
### Changed
- Input/Output Path
- 2-Pass
- FFmpeg Arguments


## [0.9.0.0] - May 8, 2017
### Added
- Output shows full path
- `FLAC` Codec available for `mkv` Format

### Changed
- Moved Video/Audio Auto Bitrates
- File rename from dialog box

### Fixed
- File Renamer
- Batch Audio Args
- Batch Switch, Output Ext
- InputOutputFile method


## [0.8.4.0] - Feb 5, 2017

## [0.7.1.0] - Jan 1, 2017

## [0.6.4.0] - Dec 24, 2016

## [0.6.1.0] - Dec 21, 2016

## [0.5.2.0] - Dec 12, 2016

## [0.5.0.0] - Sep 14, 2016

## [0.4.1.0] - Sep 3, 2016

## [0.3.0.0] - Mar 30, 2016

## [0.2.6.0] - Mar 23, 2016

## [0.2.3.0] - Mar 19, 2016

## [0.2.1.0] - Mar 14, 2016

## [0.1.0.0] - Mar 2, 2016

## [0.0.0.0] - Mar 1, 2016
