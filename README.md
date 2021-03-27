# LUI4Rage
LUI4Rage adds the RAGE plugin hook support for LemonUI.

# LemonUI<br>[![AppVeyor][appveyor-img]][appveyor-url] [![NuGet][nuget-img-2]][nuget-url-2] [![NuGet][nuget-img-3]][nuget-url-3] [![NuGet][nuget-img-f]][nuget-url-f] [![Discord][discord-img]][discord-url]

LemonUI is a framework for creating UI systems in Grand Theft Auto V that is compatible with SHVDN2, SHVDN3 and FiveM. It allows you to create UI Elements with a NativeUI-like style, or you can also create your own UI System from scratch via the resolution-independant classes for Text, Rectangles and Textures.

It was created as a replacement for NativeUI due to being too convoluted to develop and maintain. LemonUI retains most (if not all) of the UI Elements available in NativeUI.

Special thanks to:

* Guad for the original work in NativeUI
* alloc8or for the help with the GTA Online Loading Screen Scaleform
* ikt for helping me to use SET_SCRIPT_GFX_ALIGN and SET_SCRIPT_GFX_ALIGN_PARAMS
* Dot. for the snippet of code used for the item scrolling
* deterministic_bubble for answering some questions about some missing C# classes in FiveM

## Download

* [GitHub](https://github.com/justalemon/LemonUI/releases)
* [5mods](https://www.gta5-mods.com/scripts/lemonui)
* [AppVeyor](https://ci.appveyor.com/project/justalemon/lemonui) (experimental)

## Installation

### RAGE Plugin Hook

Copy all of the files from the **RAGE** folder inside the compressed file to your game folder.

### SHVDN 2 and SHVDN 3

Copy all of the files from the **SHVDN2** and/or **SHVDN3** folders inside of the compressed file to your **scripts**.

### FiveM

No need to install. The resources using LemonUI will contain the required files.

## Usage

Once installed, the mods that require LemonUI will start working.

If you are a developer, check the [wiki](https://github.com/justalemon/LemonUI/wiki) for information to implement LemonUI in your mod.

[appveyor-img]: https://img.shields.io/appveyor/build/justalemon/lemonui?label=appveyor
[appveyor-url]: https://ci.appveyor.com/project/justalemon/lemonui
[nuget-img-2]: https://img.shields.io/nuget/v/LemonUI.SHVDN2?label=nuget%20%28shvdn%202%29
[nuget-url-2]: https://www.nuget.org/packages/LemonUI.SHVDN2/
[nuget-img-3]: https://img.shields.io/nuget/v/LemonUI.SHVDN3?label=nuget%20%28shvdn%203%29
[nuget-url-3]: https://www.nuget.org/packages/LemonUI.SHVDN3/
[nuget-img-f]: https://img.shields.io/nuget/v/LemonUI.FiveM?label=nuget%20%28fivem%29
[nuget-url-f]: https://www.nuget.org/packages/LemonUI.FiveM/
[discord-img]: https://img.shields.io/badge/discord-join-7289DA.svg
[discord-url]: https://discord.gg/Cf6sspj
