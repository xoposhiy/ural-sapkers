@echo off
set archive=ural-sapkers.zip
if exist %archive% erase /f /q "%archive%"
winrar a -r -t -afzip %archive% contest