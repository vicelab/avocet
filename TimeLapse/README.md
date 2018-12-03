# TimeLapseC#

This program is designed to produce custom GIFs relating to Avocet. It animates and annotates satellite imagery from Planet.com (RapidEye and PScope), as well as game camera footage of the Avocet and related stock ponds.

## Media: 

See for outputted content produced from the TimeLapse script

## convertGIF2MP4:

A convenient shell script to convert outputted GIFs to MP4s. 

## TimelapseBuilder:

The C#.NET console application. Visual Studio 2017 recommended when opening .sln file.

### Program.cs:

The Main script, see for changing hardcoded parameters (not required for typical use cases). 

### TimelapseBuilder.exe:

The executable to run the console application. 

### Parameters (TimeLapse.exe)
> + Speed: integer (10 is most commonly used)
> + Folder: string (e.g. "C:\folder_of_tifs")
> + Timestamps: boolean (optional for Planet TIFs)
> + Frames per Day: string
>   + "range": start to end dates
>   + "month": month start to end
>   + "daytime": remove night/dark images
>   + "one-a-day": select one (noon) image per day, reducing stutter
>   + "three-a-day": 9am, 12pm, 3pm images per day, reducing stutter