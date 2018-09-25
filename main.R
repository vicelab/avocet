
library(ggplot2)
library(raster)
library (tiff)
library(RStoolbox)

## Grab A File
setwd("D:\\IMAGES\\AVO_Images\\AVO_PScope4\\WY2018_AVO_PScope4\\images\\use");

files <- list.files(path=getwd(), pattern="*.tif", full.names=TRUE, recursive=FALSE)
lapply(files, function(x) {
  #'20171002_180525_0f28_3B_AnalyticMS'
  x = substr(x, (nchar(getwd())+2), 1000)
  x = substr(x, 0, (nchar(x) - 4))
 # sapply(strsplit(x, "."), "[", 1)
  x
  val<- x
  read_brick=brick(paste(val, "tif", sep="."))
  
  ## Calculate NDVI
  ndvi <- spectralIndices(read_brick, red = paste("X",val, ".3", sep=""), nir = paste("X",val, ".4", sep=""), indices = "NDVI")
  
  ## Plot NDVI
  pdf(paste(x,".pdf", sep=""))
  plot(ndvi)
   dev.off()
})





