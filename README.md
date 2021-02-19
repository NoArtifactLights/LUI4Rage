# LUI4Rage

LUI4Rage is a project that ports LemonUI to the RAGE Plugin Hook framework, while keeping the original features. It also introduces several classes and types missing in the RAGE Plugin Hook, in order to implement these features, such as `Scaleform`, `Alignment`, such.

## Supported Platforms

* Citizen Framework (GTA V) - *originally supported*
* Community Script Hook V .NET, API v2 & v3 - *originally supported*
* RAGE Plugin Hook - *implemented by us* - built on 1.84/1.85

## License

This project is licensed under [Apache 2.0](LICENSE) license.

## Original Summary

LemonUI is a framework for creating UI systems in Grand Theft Auto V that is compatible with SHVDN2, SHVDN3 and FiveM. It allows you to create UI Elements with a NativeUI-like style, or you can also create your own UI System from scratch via the resolution-independant classes for Text, Rectangles and Textures.

It was created as a replacement for NativeUI due to being too convoluted to develop and maintain. LemonUI retains most (if not all) of the UI Elements available in NativeUI.

Special thanks to:

* Guad for the original work in NativeUI
* alloc8or for the help with the GTA Online Loading Screen Scaleform
* ikt for helping me to use SET_SCRIPT_GFX_ALIGN and SET_SCRIPT_GFX_ALIGN_PARAMS
* Dot. for the snippet of code used for the item scrolling
* deterministic_bubble for answering some questions about some missing C# classes in FiveM

## Installation

### SHVDN 2 and SHVDN 3

Copy all of the files from the **SHVDN2** and/or **SHVDN3** folders inside of the compressed file to your **scripts**.

*Do not keep it in the game folder. It will conflict with the RAGE Plugin Hook version. Placing it into `scripts` folder allows RPH Plugins to find their version of the library.*

### FiveM

No need to install. The resources using LemonUI will contain the required files.

### RAGE Plugin Hook

Copy all of the files from the **RAGE** folder inside the compressed file to your game folder. RPH tends to load plugin dependencies right under game folder.

*Remember: LUI4Rage is not a plugin, and interactions between RPH plugins is way more difficulty than SHVDN (e.g. PlayerCompanion API would be inaccessible under RPH environment as they are in different AppDomain)*

## Usage

Once installed, the mods that require LemonUI will start working. Refer to the original repository for more information on API.

