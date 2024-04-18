# olmod-stable
Small C# program to make sure you're using the latest "stable" version of olmod without any extra bells and whistles.


### Usage
-----
To use: download the latest version from the releases page or compile it yourself. Then load olmod-stable.exe and enjoy having the latest olmod version!


If you want to keep the window open or pass command line arguments to olmod, edit the config.txt file. If you deleted the config.txt file, you can generate a new generic one by executing: olmod-stable.exe --config
This will create a new config.txt file with the following entries

```
#  Config file for olmod-stable
#  Lines that begin with # are ignored...
#
#  Uncomment the following line to keep the console window open
#auto-close-window=false
#
#  Each argument should be on a separate line
#-gamedir "C:\Program Files (x86)\Steam\steamapps\common\Overload"
#-frametime

```
