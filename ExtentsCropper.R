######################################################
#                                                    #
# Author: Brian Hungerman                            #
# Date  : 10/16/2018                                 #
# Affil.: VICE Lab, University of California, Merced #
#                                                    #
######################################################

## Necessary Libraries
# If libraries aren't installed on your device, uncomment the following 4 lines
# install.packages("ggplot2")
# install.packages("raster")
# install.packages("tiff")
# install.packages("RStoolbox")
library(ggplot2)
library(raster)
library(tiff)
library(RStoolbox)

##Edit these parameters
folder = "D:\\IMAGES\\AVO_Images\\AVO_PScope4\\WY2016_AVO_PScope4\\images\\use"
shape_file = "D:\\IMAGES...\\.shp"
file_type = ".tif"

## Grab shapefile
aoi_boundary = shapefile(shape_file)

## Enters defined Folder
setwd(folder);

## Iterate over every File in Folder with desired File Type
files <- list.files(path=getwd(), pattern=paste("*",file_type, sep=""), full.names=TRUE, recursive=FALSE)
lapply(files, function(file) {
  ## Trim File to just File Name
  # e.g. "D:\IMAGES\AVO_Images\AVO_PScope4\WY2018_AVO_PScope4\images\use\20171002_180525_0f28_3B_AnalyticMS.tif"
  # goes to "20171002_180525_0f28_3B_AnalyticMS"
  file = substr(file, (nchar(getwd())+2), 1000)
  file = substr(file, 0, (nchar(file) - 4))
  
  ## RasterBrick-ize File
  file_brick=brick(paste(file, file_type, sep=""))
  
  #aoi is a shape file, chm is a raster*
  CHM_HARV_Cropped <- crop(x = CHM_HARV, y = as(aoi_boundary, "Spatial"))

  #CHM_HARV_Cropped_df <- as.data.frame(CHM_HARV_Cropped, xy = TRUE)
  
  ## Save Output
  # outfile <- writeRaster(output, filename=paste(file,"-",plot_type,file_type, sep=""), format="GTiff", overwrite=TRUE,options=c("INTERLEAVE=BAND","COMPRESS=LZW"))
  
  ## Plot to PDF
  #pdf(paste(file,"-",plot_type,".pdf", sep=""))
  #plot(output)
  #dev.off()
})


