@echo off
set archive=ural-sapkers.zip
if exist %archive% erase /f /q "%archive%"
"C:\Program Files\WinRAR\winrar.exe" a -r -t -afzip -x*\.svn\* %archive% contest 