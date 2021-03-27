# LUI4Rage<br>![PartOfNal.png](https://i.loli.net/2021/03/27/iOtMSbcAz1D2fh9.png)

LUI4Rage is a fork from the original [LemonUI](https://github.com/justalemon/LemonUI) project that adds RAGE Plugin Hook support. The project will continue to follow the original upstream commits.

## License

This project is licensed under [Apache 2.0 License](LICENSE). The license for the original LemonUI project can be found at [Thidparty.license](ThirdParty.license).

## Installation

### RAGE Plugin Hook

Copy all of the files from the **RAGE** folder inside the compressed file to your game folder.

*Do **not** attempt to place it into plugins folder. RPH plugins don't work that way.*

### SHVDN 2 and SHVDN 3

Copy all of the files from the **SHVDN2** and/or **SHVDN3** folders inside of the compressed file to your **scripts**.

### FiveM

No need to install. The resources using LemonUI will contain the required files.

## Usage

Once installed, the mods that require LemonUI will start working.

If you are a developer, check the [wiki](https://github.com/justalemon/LemonUI/wiki) for information to implement LemonUI in your mod.

## Original Summary

LemonUI is a framework for creating UI systems in Grand Theft Auto V that is compatible with SHVDN2, SHVDN3 and FiveM. It allows you to create UI Elements with a NativeUI-like style, or you can also create your own UI System from scratch via the resolution-independant classes for Text, Rectangles and Textures.

It was created as a replacement for NativeUI due to being too convoluted to develop and maintain. LemonUI retains most (if not all) of the UI Elements available in NativeUI.

Special thanks to:

* Guad for the original work in NativeUI
* alloc8or for the help with the GTA Online Loading Screen Scaleform
* ikt for helping me to use SET_SCRIPT_GFX_ALIGN and SET_SCRIPT_GFX_ALIGN_PARAMS
* Dot. for the snippet of code used for the item scrolling
* deterministic_bubble for answering some questions about some missing C# classes in FiveM

## Original Download

* [GitHub](https://github.com/justalemon/LemonUI/releases)
* [5mods](https://www.gta5-mods.com/scripts/lemonui)
* [AppVeyor](https://ci.appveyor.com/project/justalemon/lemonui) (experimental)
