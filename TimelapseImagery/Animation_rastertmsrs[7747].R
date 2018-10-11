# Raster and hydrograph animation code
# Alison Whipple, 10.10.2018

# Based on the specific code used for my research on the Cosumnes, I've tried to simplify things below
# This is for a side by side comparison of two raster time series (e.g., pre- and post-restoration) with an inset animated hydrograph

# LOAD PACKAGES -------------------------------------
  # NOTE: These are just the animation related packages, other packages like raster, tidyr, etc. would be needed 
  # NOTE: I'm not 100% sure the script below needs all of these packages. I was messing around with a number of different ways to animate
  install.packages(animation)
  install.packages(tmap)
  install.packages(purrr)
  install.packages(cowplot)
  install.packages(viridis)
  install.packages(magick)
  library(animation)
  library(tmap)
  library(purrr)
  library(cowplot)
  library(viridis)
  library(magick)

# LOAD DATA ------------------------------------------
  # Set any variables
  # Bring in daily (or whatever timestep) rasters (as rasterstack), flow dataset, etc. Each raster layer should correspond with a row in the flow dataset
    fdays.in # a flow datatable with a date field (dt) and the corresponding flow (flws)
    rs.pre # stack of rasters representing pre-restoration
    rs.post # stack of rasters representing post-restoration
  # Bring in any spatial data to map on top (e.g., project boundary, river line)
    fp.shp # project boundary

# DAILY ANIMATION -------------------------------------

  # Establish base hydrograph plot
    p <- ggplot(data=fdays.in, aes(x=dt, y=flws)) + #geom_line()
      geom_area(fill="blue") + 
      geom_line(color="darkblue") +
      xlab("") + ylab(expression("Q ("*m^3*"/s)")) 
      
  img <- image_graph(res = 96)
  for (i in (1:length(fdays.in))) {
    rsdf.pre <- as.data.frame(rs.pre[[i]], xy=T, na.rm=F) # set the raster to dataframe for use in ggplot
    colnames(rsdf.pre) <- c("x","y","values")
    rsdf.post <- as.data.frame(rs.post[[i]], xy=T, na.rm=F) # set the raster to dataframe for use in ggplot
    colnames(rsdf.post) <- c("x","y","values")
    p1 <- ggplot() +
      geom_raster(data=rsdf.pre, aes(x,y, fill=values)) +
      scale_fill_gradientn(name="Var name",colours=c("lightgray","yellow","green", "blue", "purple"), 
                           na.value="white", limits=c(0,1)) + # or scale_fill_brewer, or any other color ramp
      geom_polygon(data=fp.shp, aes(x=long, y=lat, group=group), 
                   fill=NA,color="grey50", size=1)+
      coord_equal() +
      theme_map() +
      theme(legend.position="none")
    p2 <- ggplot() +
      geom_raster(data=rsdf.post, aes(x,y, fill=values)) +
      scale_fill_gradientn(name="Var name",colours=c("lightgray","yellow","green", "blue", "purple"), 
                           na.value="white", limits=c(0,1)) + # or scale_fill_brewer, or any other color ramp
      geom_polygon(data=fp.shp, aes(x=long, y=lat, group=group), 
                   fill=NA,color="grey50", size=1)+
      coord_equal() +
      theme_map() 
    p3 <- plot_grid(p1, p2, labels = c("Pre-restoration", "Post-restoration"))
    p4 <- p + geom_point(data=fdays.in[fdays.in[i],], aes(x=dt, y=flws), color="darkred",size=3) +
      ggtitle(fdays.in$dt[fdays.in[i]])
    p5 <- ggdraw() +
      draw_plot(p3, 0,0,1,1) + # lots of messing around with the values to get the layout to look right
      draw_plot(p4, .23,0,width=0.3, height=0.4) # lots of messing around with the values to get the layout to look right
    print(p5)
  }
  dev.off()
  animation1 <- image_animate(img, fps = 2)
  image_write(animation1, "./Animation.gif")

