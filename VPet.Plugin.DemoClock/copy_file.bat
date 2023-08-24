@echo off
setlocal

set "sourceDir=%~dp0bin\Debug"
set "targetPluginDir=%~dp01100_DemoClock\plugin"
set "targetModDir=..\VPet-Simulator.Windows\mod\1100_DemoClock"

REM 复制 DLL 文件和 XML 文件到 plugin 目录
xcopy "%sourceDir%\*.dll" "%targetPluginDir%" /Y /I /D

REM 复制整个 1100_DemoClock 文件夹到 mod 目录
xcopy "%~dp01100_DemoClock" "%targetModDir%" /E /Y /I /D

endlocal
