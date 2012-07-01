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
  project "LandisII.Examples.Console"
    location "console"
    kind "ConsoleApp"
    targetname "LandisII.Examples.Console"
    files { "console/*.cs" }
    links {
      "Landis.SpatialModeling",
      "Landis.SpatialModeling.CoreServices",
      "System",
      "LandisII.Examples.SimpleCore",      -- API project
      "LandisII.Examples.SimpleCore.Impl"  -- implementation project
    }

  -- LANDIS-II model core (API; referenced by LANDIS-II extensions)
  project "LandisII.Examples.SimpleCore"
    location "core-api"
    kind "SharedLib"
    targetname "LandisII.Examples.SimpleCore"
    files { "core-api/**.cs" }
    links {
      "Landis.SpatialModeling",
      "System"
    }

  -- LANDIS-II model core (implementation)
  project "LandisII.Examples.SimpleCore.Impl"
    location "core"
    kind "SharedLib"
    targetname "LandisII.Examples.SimpleCore.Impl"
    files { "core/**.cs" }
    links {
      "Landis.SpatialModeling",
      "Landis.SpatialModeling.CoreServices",
      "System",
      "LandisII.Examples.SimpleCore",      -- API project
      "LandisII.Examples.SimpleExtension"  -- extension project
    }

  -- LANDIS-II extension
  project "LandisII.Examples.SimpleExtension"
    location "ext"
    kind "SharedLib"
    targetname "LandisII.Examples.SimpleExtension"
    files { "ext/**.cs" }
    links {
      "Landis.SpatialModeling",
      "System",
      "LandisII.Examples.SimpleCore"       -- API project
    }
