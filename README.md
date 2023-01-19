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
