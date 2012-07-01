build_dir="../../build"

solution "landis-ii_example"

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
 
  -- LANDIS-II console
  project "LandisII_Examples_Console"
    location "console"
    kind "ConsoleApp"
    targetname "LandisII_Examples_Console"
    files { "console/*.cs" }
    links {
      "Landis.SpatialModeling",
      "Landis.RasterIO",
      "Landis.RasterIO.Gdal",
      "System",
      "LandisII_Examples_SimpleCore",      -- API project
      "LandisII_Examples_SimpleCore_Impl"  -- implementation project
    }

  -- LANDIS-II model core (API; referenced by LANDIS-II extensions)
  project "LandisII_Examples_SimpleCore"
    location "core-api"
    kind "SharedLib"
    targetname "LandisII.Examples.SimpleCore"
    files { "core-api/**.cs" }
    links {
      "Landis.SpatialModeling",
      "System"
    }

  -- LANDIS-II model core (implementation)
  project "LandisII_Examples_SimpleCore_Impl"
    location "core"
    kind "SharedLib"
    targetname "LandisII.Examples.SimpleCore.Impl"
    files { "core/**.cs" }
    links {
      "Landis.Landscapes",
      "Landis.SpatialModeling",
      "System",
      "LandisII_Examples_SimpleCore",      -- API project
      "LandisII_Examples_SimpleExtension"  -- extension project
    }

  -- LANDIS-II extension
  project "LandisII_Examples_SimpleExtension"
    location "ext"
    kind "SharedLib"
    targetname "LandisII.Examples.SimpleExtension"
    files { "ext/**.cs" }
    links {
      "Landis.SpatialModeling",
      "System",
      "LandisII_Examples_SimpleCore"       -- API project
    }
