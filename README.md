# Avocet Project

Includes code to scrub for faulty GPS points collected by drone, code to spidering through folders to generate NDVI, NDWI images, and timelapse generation algorithms. Satellite imagery pulled from Planet.com (PScope and RapidEye).

## ExtentsCropper

Mass-cropping TIFs to relevant extents, and exporting to PNGs.

## LatLongScrubber

Currently deprecated, but used to clean UAV output to work in Pix4D.

## SpectralIndices

Mass-indexing TIFs for NDVI, NDWI, etc, and exporting as PDF, PNG, etc.

## TimeLapseC#

Generating GIFs from PNGs, with support for adding time-stamps for Planet imagery, as well as customized video parameters.
