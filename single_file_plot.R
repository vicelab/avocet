######################################################
#                                                    #
# Author: Brian Hungerman                            #
# Date  : 9/24/2018                                  #
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
folder = "D:\\IMAGES\\AVO_Images\\AVO_PScope4\\WY2018_AVO_PScope4\\images\\use"
file = "20180328_181215_100c_3B_AnalyticMS"
file_type = ".tif"
plot_type = "NDWI"

## Enters defined Folder
setwd(folder);

## RasterBrick-ize File
file_brick=brick(paste(file, file_type, sep=""))
  
## Calculate Indices
# Uses Bands 3 (RED) and 4 (NIR)
output <- spectralIndices(file_brick, blue = paste("X",file, ".1", sep=""), green = paste("X",file, ".2", sep=""),red = paste("X",file, ".3", sep=""), nir = paste("X",file, ".4", sep=""), indices = plot_type)
  
## Plot to PDF
outfile <- writeRaster(output, filename=paste(file,"-",plot_type,file_type, sep=""), format="GTiff", overwrite=TRUE,options=c("INTERLEAVE=BAND","COMPRESS=LZW"))


