
library(ggplot2)
library(raster)
library (tiff)
library("RStoolbox")

setwd("D:\\IMAGES\\AVO_Images\\AVO_PScope4\\WY2018_AVO_PScope4\\images\\use");
val<-'20171002_180525_0f28_3B_AnalyticMS.tif'
read_file=raster(val)
read_file
read_brick = brick(read_file)
read_brick
## Calculate NDVI
ndvi <- spectralIndices(read_brick, red = "X20171002_180525_0f28_3B_AnalyticMS",nir = "X20171002_180525_0f28_3B_AnalyticMS", indices = "NDVI")
ndvi
plot(ndvi)




