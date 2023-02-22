# UsenetMediaProcessor
Utility to process Usenet downloads, auto extract using rar with password, and copy to personal library.

Extract button
==============
Scans the folder where Usenet files are downloaded to by a usenet reader, in this case Newsleecher. 
It will determine the secret password for the usenet file and extract it to the downloads folder. 
If it is a tv file, it will try and match with the NFO file to give it the correct name.

Add to Library button
=====================
Storage paths are defined in TVFolders and MovieFolders.  
Based on the filename, it will determine if it is a TV show or a movie file.
If it is a TV file, it will search all the TVFolders for the matching Name then match the corresponding Season # to the correct season folder.
If it is a movie file, it will search all the MovieFolders and create the matching Movie title in the corresponding Year folder.
If the TV episode or Movie already exists, it will not copy the file.
Once the files are copied, they are moved to the BackupFolder.


