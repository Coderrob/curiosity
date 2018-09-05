# NASA Mars Rover Photo API Client

The Mars Rover Photo API client is a .Net Core assembly that wraps the [NASA Rover open API](https://api.nasa.gov/api.html#MarsPhotos).

## Getting Started

Before getting started please refer to the [NASA API Getting Started Guide](https://api.nasa.gov) for instructions on how to request an NASA API Key for production use. For testing purposes you may use the NASA demo API key `DEMO_KEY`.

## Table of Contents

- [Examples](#Examples)
- [Built With](#BuiltWith)
- [License](#License)

## <a name="Examples"></a> Examples

### Get All Mars Rovers

```cs
// Create new Rover Client instance
var client = new RoverClient('DEMO_KEY');

// Returns a collection of Mars rovers
var rovers = client.GetRovers();
```

### Get a Mars Rover By Name

```cs
// Create new Rover Client instance
var client = new RoverClient('DEMO_KEY');

// Get a specific Rover by name
var rover = client.GetRover('Spirit');
```

### Get Mars Rover Photos By Date

```cs
// Create new Rover Client instance
var client = new RoverClient('DEMO_KEY');

// Get a specific Rover by name
var rover = client.GetRover('Spirit');

// Day to request photos from
var date = new DateTime(2015, 6, 3);

// Get photos from every camera for a given date
var photos = await rover.GetPhotosAsync(date);
```

### Get Mars Rover Cameras

```cs
// Create new Rover Client instance
var client = new RoverClient('DEMO_KEY');

// Get a specific Rover by name
var rover = client.GetRover('Spirit');

// Get all cameras from the rover
var cameras = rover.GetCameras();
```

### Get Mars Rover Camera By Abbreviation

```cs
// Create new Rover Client instance
var client = new RoverClient('DEMO_KEY');

// Get a specific Rover by name
var rover = client.GetRover('Spirit');

// Get the cameras of the rover
var camera = rover.GetCamera('FHAZ');
```

### Get Mars Rover Photos From a Specific Camera

```cs
// Create new Rover Client instance
var client = new RoverClient('DEMO_KEY');

// Get a specific Rover by name
var rover = client.GetRover('Spirit');

// Get the cameras of the rover
var camera = rover.GetCamera('FHAZ');

// Day to request photos from
var date = new DateTime(2015, 6, 3);

// Get the photos from a specific rover camera by a given date
var photos = await camera.GetPhotosAsync(date);
```

Want to see more examples? Please feel free to contribute!

## Running the Tests

// TODO

## <a href="BuiltWith"></a> Built With

- [.Net Core](https://docs.microsoft.com/en-us/dotnet/core/) - The .Net framework used
- [Flurl](https://github.com/tmenier/Flurl) - The web client used
- [NLog](https://nlog-project.org) - The logging library used
- [NASA Rover Api](https://api.nasa.gov/api.html#MarsPhotos) - NASA rover photos api

## <a href="License"></a> License

This project is licensed under the GNU License - see the [LICENSE.md](LICENSE.md) file for details
