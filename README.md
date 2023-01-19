## LaunchControlV

A simple GTAV and Modest Menu launcher and configurator.

Currently it's usable through the CLI test program, but once the backend is done, a WPF GUI will be built around it.

## Features

- [x] Epic, Steam, Rockstar, cracked support
- [x] GTAV, Modest Menu: start, suspend, stop
- [x] GTAV: Live private lobby configurator ([Idea](https://www.unknowncheats.me/forum/grand-theft-auto-v/488229-gta-public-solo-session.html))
- [x] GTAV: Live commandline configurator
- [ ] Modest Menu: themes.json editor
- [ ] Modest Menu: teleports.json editor
- [ ] Modest Menu: vehicles.json editor
- [ ] Modest Menu: config.json editor
- [ ] Modest Menu: Lua helper
- [ ] GTAV: garage tracker
- [ ] GTAV: Snapmatic exporter

## How To Use

- Build and run (see below)
- On the first run it'll create a config file in the data directory next to the built executable (`src\LCV_CLI\bin\Debug\net6.0\data\config.json`)
- Close the program, open the config file, and manually set `GTA.Path` and `ModestMenu.Path` with an escaped path string:
```js
{
  "GTA": {
    ...
    "Path": "C:\\Program Files\\Epic Games\\GTAV",
    ...
  },
  "ModestMenu": {
    ...
    "Path": "D:\\Programs\\modest-menu_v0.9.7\\",
    ...
},
```
- Now that the paths are set, the program will work properly, even after building or running it again

## How To Build

- Download [.NET6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- Clone/download this repository
- Open the directory containing the solution (.sln) in CMD/PowerShell
- Run the following commands:
```
dotnet build
dotnet run --project LCV_CLI
```
- The CLI test program should open in CMD/PowerShell

*If there are dependency issues, install [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json).*
