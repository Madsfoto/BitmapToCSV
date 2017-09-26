**Bitmap To CSV**
Stupid console application that takes _one required_ argument as an image and ouputs the image data as grayscale ((r+b+g)/3) data as a CSV file.  
  
Todo:  
Better grayscale (REC 709 etc)  
Have the data in an array or something more suited than a string.
  
*Why?*  
I have been unable to get any histogram data from bitmaps, so this is an approach to get the data, internally as a string before being written to disk, in a form that I can work with later.
