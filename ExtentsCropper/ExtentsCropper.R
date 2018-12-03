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
# install.packages("jpeg")
# install.packages("tiff")
library(ggplot2)
library(raster)
library(tiff)
library(RStoolbox)

##Edit these parameters
folder = "C:\\Users\\bhungerman\\Desktop\\more" #"G:\\IMAGES\\AVO_Images\\AVO_RapidEye\\AVOCET_RapidEye\\images"
file_type = ".tif"
plot_type = "CROP"

# new_extent <- extent(730353.61, 734353.22, 4141247.64, 4146424.20)              #AVO_BOUNDS
new_extent <- extent(732522.62507370487,732970.81861195061, 4143542.0276828129, 4143994.6915609557) #AVO_POND
# new_extent <- extent(731670.09419491375, 733097.07568875398,4142928.9240985578, 4144526.1959072393) #AVO_MODEL_BOUNDARY
# new_extent <- extent(731932.57051903661,732454.46218781825, 4140883.8773284461, 4141306.550048789) #3WILLOWS_POND

class(new_extent)
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
  file_brick <- crop(x = file_brick, y = new_extent)
  #output <- spectralIndices(file_brick, blue = paste("X",file, ".1", sep=""), green = paste("X",file, ".2", sep=""),red = paste("X",file, ".3", sep=""), nir = paste("X",file, ".4", sep=""), indices = plot_type)
  output <- file_brick
  ## Save Output
  png(paste(file,"-",plot_type,".png", sep=""))
  plotRGB(output)
  dev.off()
  
  # outfile <- writeRaster(output, filename=paste(file,"-",plot_type,file_type, sep=""), format="GTiff", overwrite=TRUE,options=c("INTERLEAVE=BAND","COMPRESS=LZW"))
  
})


