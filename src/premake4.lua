newoption {
  trigger     = "gdal-csharp",
  value       = "DIR",
  description = "Directory with GDAL C# bindings (REQUIRED)",
}

--  If an action other than clean is specified, verify that the
--  --gdal-csharp option was used to provide the location of the
--  GDAL C# bindings.

function gdal_csharp_error(message)
  print("Error with --gdal-csharp: " .. message)
  error("command line error")
end

if _ACTION and _ACTION ~= "clean" then
  if not _OPTIONS["gdal-csharp"] then
    gdal_csharp_error("it is missing (use --help for details)")
  end
  gdal_csharp_dir = _OPTIONS["gdal-csharp"]
  if string.match(gdal_csharp_dir, "^%s*$") then
    gdal_csharp_error("no directory specified")
  end
  if os.isfile(gdal_csharp_dir) then
    gdal_csharp_error('"'..gdal_csharp_dir..'" is not a directory')
  end
  if not os.isdir(gdal_csharp_dir) then
    gdal_csharp_error('"'..gdal_csharp_dir..'" does not exist')
  end
else
  -- set the directory variable so Premake don't report an error
  -- about attempting to concatentate to a nil value in the "links"
  -- function below
  gdal_csharp_dir=""
end

build_dir="../build"

solution "landis-spatial"

  language "C#"
  -- by default, premake uses "Any CPU" for platform

  configurations { "Debug", "Release" }
 
  configuration "Debug"
    defines { "DEBUG" }
    flags { "Symbols" }
    targetdir ( build_dir .. "/Debug" )
 
  configuration "Release"
    flags { "OptimizeSize" }
    targetdir ( build_dir .. "/Release" )
 
  -- The library's API
  project "Landis_SpatialModeling"
    location "api"
    kind "SharedLib"
    targetname "Landis.SpatialModeling"
    files {
      "api/*.cs",
      "SharedAssemblyInfo.cs"
    }
    links { "System" }

  -- Implementation of the Landscape module
  project "Landis_Landscapes"
    location "Landscapes"
    kind "SharedLib"
    targetname "Landis.Landscapes"
    files {
      "Landscapes/**.cs",
      "SharedAssemblyInfo.cs"
    }
    links {
      "System",
      "Landis_SpatialModeling"
    }

  -- Data types shared by Raster I/O implementations
  project "Landis_RasterIO"
    location "RasterIO"
    kind "SharedLib"
    targetname "Landis.RasterIO"
    files {
      "RasterIO/**.cs",
      "SharedAssemblyInfo.cs"
    }
    links {
      "System",
      "Landis_SpatialModeling",
    }

  -- Implementation of the Raster I/O module using GDAL
  project "Landis_RasterIO_Gdal"
    location "RasterIO.Gdal"
    kind "SharedLib"
    targetname "Landis.RasterIO.Gdal"
    files {
      "RasterIO.Gdal/**.cs",
      "SharedAssemblyInfo.cs"
    }
    links {
      "System",
      "Landis_SpatialModeling",
      "Landis_RasterIO",
      gdal_csharp_dir.."/gdal_csharp.dll"
    }
