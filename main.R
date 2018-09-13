library(ggplot2)
library(raster)
#D:\\IMAGES\\AVO_Images\\AVO_PScope4\\WY2018_AVO_PScope4\\images\\use\\20171002_180525_0f28_3B_AnalyticMS.tif
data(lsat)
lsat

## Calculate NDVI
ndvi <- spectralIndices(lsat, red = "B3_dn", nir = "B4_dn", indices = "NDVI")
ndvi
ggR(ndvi, geom_raster = TRUE) +
  scale_fill_gradientn(colours = c("black", "white")) 
mtlFile  <- system.file("external/landsat/LT52240631988227CUB02_MTL.txt", package="RStoolbox")
lsat_ref <- radCor(lsat, mtlFile, method = "apref")

SI <- spectralIndices(lsat_ref, red = "B3_tre", nir = "B4_tre")
plot(SI)





