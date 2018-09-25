
library(ggplot2)
library(raster)
library (tiff)
library(RStoolbox)

folder = "D:\IMAGES\AVO_Images\AVO_PScope4\WY2018_AVO_PScope4\images\use"
file_type = ".tif"
plot_type = "NDVI"

## Enter A Folder
setwd(folder);

# Iterate over every File in Folder with desired File Type
files <- list.files(path=getwd(), pattern=paste("*",file_type, sep=""), full.names=TRUE, recursive=FALSE)
lapply(files, function(file) {
  # Trim the file to just the File Name
  # e.g. "D:\IMAGES\AVO_Images\AVO_PScope4\WY2018_AVO_PScope4\images\use\20171002_180525_0f28_3B_AnalyticMS.tif"
  # goes to "20171002_180525_0f28_3B_AnalyticMS"
  file = substr(file, (nchar(getwd())+2), 1000)
  file = substr(file, 0, (nchar(file) - 4))
  
  # RasterBrick-ize file
  file_brick=brick(paste(file, file_type, sep=""))
  
  ## Calculate NDVI
  # Uses Bands 3 (RED) and 4 (NIR)
  ndvi <- spectralIndices(file_brick, red = paste("X",file, ".3", sep=""), nir = paste("X",file, ".4", sep=""), indices = plot_type)
  
  ## Plot NDVI to PDF
  pdf(paste(file,plot_type,".pdf", sep=""))
  plot(ndvi)
  dev.off()
})





