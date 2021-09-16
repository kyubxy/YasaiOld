![Untitled](https://user-images.githubusercontent.com/28855597/133410381-8996ebf2-7a67-42fa-915f-e711a330dbb0.png)

the funny vegetable framework, this serves as my general graphics playground. Powered by SDL.

# What
[infinite monkey theorem](https://en.wikipedia.org/wiki/Infinite_monkey_theorem) but it's being applied to writing a C# graphics framework. 

This framework features an awful mismatch of imperative and declerative programming styles to iterate interfaces in a concise yet comprehensive fashion. What you're looking at here is the culmination of years of wasted time and effort to produce what is subpar at best!

## Why
I like writing code that does nothing impressive.

# Contribution and Use
This is literaly a worse version of [osu framework](https://github.com/ppy/osu-framework), if you're looking for something similar just use that.

The roadmap section below mentions the project is still in its *part 1*. Although development will be fast paced and code will be frantically put together, I will still take the time to review any PRs that are sent. 

**At this point in time, although the code is open source, the general development process will be largely kept to myself**. This will change after part 1.

## Getting started
At the moment, the framework doesn't actually work. You can still test it by cloning the repo and following the steps below
### Windows
Download [SDL](https://www.libsdl.org/download-2.0.php), [SDL_image](https://www.libsdl.org/projects/SDL_image/) and [SDL_ttf](https://www.libsdl.org/projects/SDL_ttf/). Place the .dlls in the root of the Yasai.Tests folder. Ensure you set the build settings to *Build if newer* so the dlls are present in the application folder.
### Linux
(wip)

Currently the tests involve running the different `Scenarios`, the scenario that is run can be changed in the `TestGame`. The testing will improve in time. 

# Development Roadmap
Development is taking place in 2 parts, the goals of each are listed as follows

## Part 1, initial implementation (us right now)
features are still being rushed into the framework to get something feasible running as quickly as possible. By the end of part 1, the following will be implemented
- loading of single resources into a cache
- draw system with basic grouping
- primitives
- hierachy based input
- Screens 
- basic debug tools
- basic testing interface
- complete in code XML documentation
- linux support through .net 5

## Part 2, more features 
more care will be taken in maintaining code quality and readability. it will probably be easier to contribute by this point. By the end of part 2, the following will be implemented
- more game based functionality
  - tiled map support
  - cameras
  - basic physics
- relative positioning
- UI library
- completed load pipeline
- audio
- improved testing
- improved debug tools
- a wiki (probably)   
