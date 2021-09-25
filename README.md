![Untitled](https://user-images.githubusercontent.com/28855597/133410381-8996ebf2-7a67-42fa-915f-e711a330dbb0.png)

the funny vegetable framework, this serves as my general graphics playground. Powered by SDL.

# What
[infinite monkey theorem](https://en.wikipedia.org/wiki/Infinite_monkey_theorem) but it's being applied to writing a C# graphics framework. 

This framework features an awful mismatch of imperative and declerative programming styles to iterate interfaces in a concise yet comprehensive fashion. What you're looking at here is the culmination of years of wasted time and effort to produce what is subpar at best!

## Why
I like writing code that does nothing impressive.

# Contribution and Use
This is literaly a worse version of [osu!framework](https://github.com/ppy/osu-framework), if you're looking for something similar just use that.

This project is still in *part 1* of its development. Although development will be fast paced and code will be frantically put together, I will still take the time to review any issues or PRs that are sent. 

**At this point in time, although the code is open source, the general development process will be largely kept to myself**. This will change after part 1.

Currently the tests involve running the different `Scenarios`, the scenario that is run can be changed in the `TestGame`. The testing will improve very soon. 

# Getting started
At the moment, the framework doesn't actually work. You can still test it by cloning the repo and following the steps below
## Windows
1. Download [SDL](https://www.libsdl.org/download-2.0.php), [SDL_image](https://www.libsdl.org/projects/SDL_image/) and [SDL_ttf](https://www.libsdl.org/projects/SDL_ttf/). 
2. Place the .dlls in the root of the Yasai.Tests folder. Ensure you set the build settings to *Build if newer* so the dlls are present in the application folder.
## Linux
Probably an easier installation process than windows (unless it doesn't work)
1. Using your distro's package manager, install the SDL2 library and required extensions. If your distro is not listed here, a quick google search for *[distro name] SDL2 install* should give you all the information you need
### Arch linux
Everything you need to get started with SDL and yasai is already available on the AUR 
```
pacman -S sdl2 sdl2_ttf sdl2_image
```
### Ubuntu/Debian
see [this page](https://lazyfoo.net/tutorials/SDL/01_hello_SDL/linux/index.php)

(wip vv, everything from this point on doesnt actually apply)

2. Ensure the packages are in `/usr/lib/`, you can either verify this directly or through your package manager. With *yay* this can be done using
```
yay -Ql sdl2_ttf
```
If the packages are in the correct spot, yasai should run issue-free

3. If necessary, the path to the .so files can be changed either globally or project-wide. see the wiki for more information.

