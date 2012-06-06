@echo off

setlocal
if "%1" == "debug" goto checkEnvVar
if "%1" == "release" goto checkEnvVar
if "%1" == "" (
  echo Error: No configuration specified
) else (
  echo Error: Unknown configuration: %1
)
echo Usage: %0 debug^|release
goto :eof

rem -----------------------------------------------------------------

:checkEnvVar
if "%GDAL_BIN_DIR%" == "" (
  echo Error: GDAL_BIN_DIR environment variable is not set
  goto :eof
)
if not exist "%GDAL_BIN_DIR%" (
  echo Error: GDAL_BIN_DIR environment variable refers to non-existant directory
  goto :eof
)
if not exist "%GDAL_BIN_DIR%"\ (
  echo Error: GDAL_BIN_DIR environment variable refers to a file
  goto :eof
)

echo Adding GDAL_BIN_DIR to PATH...
echo.
set PATH_SAVE=%PATH%
PATH %GDAL_BIN_DIR%;%PATH%

set SCRIPT_DIR=%~dp0
set CONFIG_DIR=%SCRIPT_DIR%..\..\build\%~1
set EXE_PATH=%CONFIG_DIR%\LandisII.Examples.Console.exe
"%EXE_PATH%"

echo.
echo Restoring PATH...
PATH %PATH_SAVE%
