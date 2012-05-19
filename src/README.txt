The Premake build configuration tool is needed to generate the C# solution
and project files.  Premake is available from:

  http://industriousone.com/premake

Premake 4.3 has been used successfully to generate VS2008 project files.
In order to generate VS2010 project files, Premake 4.4 (currently, beta 4)
is needed because Premake 4.3 doesn't support VS2010 C# projects.

This solution contains two projects that represent the library's API and
its core services.  The core-services project uses the C# bindings to
GDAL.  Therefore, these bindings must be installed on the computer in
order to build the core-services assembly.  The directory where the
bindings are located must be specified with the --gdal-csharp option to
Premake; for example:

  premake4 --gdal-csharp=C:\GDAL\swig\csharp vs2008
