# Avocet Project

Includes code to scrub for faulty GPS points collected by drone, code to spidering through folders to generate NDVI, NDWI images, and timelapse generation algorithms. Satellite imagery pulled from Planet.com (PScope and RapidEye).

## ExtentsCropper 
#### (RScript in RStudio)

Mass-cropping TIFs to relevant extents, and exporting to PNGs.

## LatLongScrubber
#### (RScript in RStudio)

Currently deprecated, but used to clean UAV output to work in Pix4D.

## SpectralIndices
#### (RScript in RStudio)

Mass-indexing TIFs for NDVI, NDWI, etc., and exporting as PDF, PNG, etc.

## TimeLapse
#### (C#.NET Console Application in Visual Studio)

Generating GIFs from PNGs, with support for adding time-stamps for Planet imagery, as well as customized video parameters.

### Parameters (TimeLapse.exe)
Speed: integer (10 is most commonly used)
Folder: string (e.g. "C:\folder_of_tifs")
Timestamps: boolean (optional for Planet TIFs)
Frames per Day: string
    "range": start to end dates
    "month": month start to end
    "daytime": remove night/dark images
    "one-a-day": select one (noon) image per day, reducing stutter
    "three-a-day": 9am, 12pm, 3pm images per day, reducing stutter