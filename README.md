# TomsLogger

[![GitHub release (latest by date including pre-releases)](https://img.shields.io/github/v/release/TomiEckert/TomsLogger?include_prereleases&style=flat)](https://github.com/TomiEckert/TomsLogger/releases/latest)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/TomsLogger)](https://www.nuget.org/packages/TomsLogger)
[![GitHub](https://img.shields.io/github/license/TomiEckert/TomsLogger)](https://github.com/TomiEckert/TomsLogger/blob/main/LICENSE)
[![GitHub Workflow Status](https://img.shields.io/github/workflow/status/TomiEckert/TomsLogger/.NET)](https://github.com/TomiEckert/TomsLogger/actions/workflows/dotnet.yml)
[![CodeFactor](https://www.codefactor.io/repository/github/tomieckert/tomslogger/badge)](https://www.codefactor.io/repository/github/tomieckert/tomslogger)
[![BCH compliance](https://bettercodehub.com/edge/badge/TomiEckert/TomsLogger?branch=main)](https://bettercodehub.com/)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/047e56a65a6f4c8cb8fc860535156d74)](https://www.codacy.com/gh/TomiEckert/TomsLogger/dashboard)

Super simple thread safe logger

## Usage

```cs
// The defaults automatically load when
// you first fire a log.

Logger.Debug("This is a debug message");
```

with the default configuration it will producethe following message:

```js
[13:11:48] [Debug] [Main] This is a debug message
```

## Configuration

The configuration uses the `LoggerConfigBuilder` class:

```cs
var config = LoggerConfigBuilder.Default
                                .SetDisplayLevel(LogLevel.Debug)
                                .DoNotSaveToFile()
                                .Build();

Logger.Initialize(config);
```

The default configuration contains the following settings:

  - `DisplayLevel`: info
  - `WriteToFile`: true
  - `Filename`: log.txt
  - `Callback`: `Console.WriteLine`