# NASA Mars Rover Photo Import Utility

This project is a .Net Core Console Application that allows for command line access to download Mars rover images to a local folder store.

## Getting Started

Before getting started please refer to the [NASA API Getting Started Guide](https://api.nasa.gov) for instructions on how to request an NASA API Key for production use.

The Mars rover photo import utility is a .Net Core CLI application that allows for quickly importing Mars rover photos to a local directory. The utility takes in a text file path, that should contain a list of dates, and a local directory path to store the photos. For testing purposes you may use the NASA demo API key `DEMO_KEY` for the required NASA API key.

## Table of Contents

- [Expected Date File Format](#ExpectedDateFileFormat)
- [Examples](#Examples)
- [License](#License)

## Expected Date File Format

A unique set of dates should be provided, one per line, and without text delimiters.

Supported formats:

```txt
02/27/17
June 2, 2018
Jul-13-2016
April 31, 2018
```

## Examples

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

### 

## License

This project is licensed under the GNU License - see the [LICENSE.md](../LICENSE.md) file for details
