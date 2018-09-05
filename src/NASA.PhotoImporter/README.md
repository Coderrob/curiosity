# NASA Mars Rover Photo Import Utility

This project is a .Net Core Console Application that allows for command line access to download Mars rover images to a local folder store.

## Getting Started

Before getting started please refer to the [NASA API Getting Started Guide](https://api.nasa.gov) for instructions on how to request an NASA API Key for production use.

The Mars rover photo import utility is a .Net Core CLI application that allows for quickly importing Mars rover photos to a local directory. The utility takes in a text file path, that should contain a list of dates, and a local directory path to store the photos. For testing purposes you may use the NASA demo API key `DEMO_KEY` for the required NASA API key.

## Table of Contents

- [Expected Date File Format](#ExpectedDateFileFormat)
- [Examples](#Examples)
- [Built With](#BuiltWith)
- [License](#License)

## <a href="ExpectedDateFileFormat"></a> Expected Date File Format

A unique set of dates should be provided, one per line, and without text delimiters.

Supported formats:

```txt
02/27/17
June 2, 2018
Jul-13-2016
April 31, 2018
```

## <a href="Examples"></a> Examples

### Get List of Options

Command

```bash
>_ dotnet NASA.PhotoImporter.dll --help
```

Output

```bash
Options:
    -i=VALUE                Input date file
    -o=VALUE                Output directory
    -k, --key=VALUE         NASA API key
    -h, --help              show this message and exit
```

### Import Mars Rover Photos

Command

```bash
>_ dotnet NASA.PhotoImporter.dll -i c:\Mars\dates.txt -o c:\Mars\Photos -k DEMO_KEY
```

Output

```bash
Import complete:
    Input File: c:\Mars\dates.txt
    Output Folder: c:\Mars\Photos
```

## <a href="BuiltWith"></a> Built With

- [.Net Core](https://docs.microsoft.com/en-us/dotnet/core/) - The .Net framework used
- [NASA.Api](../NASA.Api/README.md) - The NASA.Api project
- [Flurl](https://github.com/tmenier/Flurl) - The web client used
- [NDesk.Options](http://www.ndesk.org/Options) - Call-back program option parser for C#
- [NLog](https://nlog-project.org) - The logging library used
- [NASA Rover Api](https://api.nasa.gov/api.html#MarsPhotos) - NASA rover photos api

## <a href="License"></a> License

This project is licensed under the GNU License - see the [LICENSE.md](../LICENSE.md) file for details
