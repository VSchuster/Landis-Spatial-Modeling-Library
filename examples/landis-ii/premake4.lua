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
  project "landis-ii_console"
    location "console"
    kind "ConsoleApp"
    targetname "LandisII.Examples.Console"
    files { "console/*.cs" }
    links {
      "Landis.SpatialModeling",
      "Landis.SpatialModeling.CoreServices",
      "System",
      "landis-ii_core",
      "landis-ii_core-api"
    }
    -- configuration "Debug"
    --   links {
    --     build_dir .. "/Debug/Landis.SpatialModeling.dll",
    --     build_dir .. "/Debug/Landis.SpatialModeling.CoreServices.dll",
    --   }
    -- configuration "Release"
    --   links {
    --     build_dir .. "/Release/Landis.SpatialModeling.dll",
    --     build_dir .. "/Release/Landis.SpatialModeling.CoreServices.dll",
    --   }

  -- LANDIS-II model core (API; referenced by LANDIS-II extensions)
  project "landis-ii_core-api"
    location "core-api"
    kind "SharedLib"
    targetname "LandisII.Examples.SimpleCore"
    files { "core-api/**.cs" }
    links {
      "Landis.SpatialModeling",
      "System"
    }
    -- configuration "Debug"
    --   libdirs { build_dir .. "/Debug" }
    -- configuration "Release"
    --   libdirs { build_dir .. "/Release" }

  -- LANDIS-II model core (implementation)
  project "landis-ii_core"
    location "core"
    kind "SharedLib"
    targetname "LandisII.Examples.SimpleCore.Impl"
    files { "core/**.cs" }
    links {
      "Landis.SpatialModeling",
      "Landis.SpatialModeling.CoreServices",
      "System",
      "landis-ii_core-api",
      "landis-ii_extension"
    }
    -- configuration "Debug"
    --   links {
    --     build_dir .. "/Debug/Landis.SpatialModeling.dll",
    --     build_dir .. "/Debug/Landis.SpatialModeling.CoreServices.dll",
    --   }
    -- configuration "Release"
    --   links {
    --     build_dir .. "/Release/Landis.SpatialModeling.dll",
    --     build_dir .. "/Release/Landis.SpatialModeling.CoreServices.dll",
    --   }

  -- LANDIS-II extension
  project "landis-ii_extension"
    location "ext"
    kind "SharedLib"
    targetname "LandisII.Examples.SimpleExtension"
    files { "ext/**.cs" }
    links {
      "Landis.SpatialModeling",
      "System",
      "landis-ii_core-api"
    }
    -- configuration "Debug"
    --   links { build_dir .. "/Debug/Landis.SpatialModeling.dll" }
    -- configuration "Release"
    --   links { build_dir .. "/Release/Landis.SpatialModeling.dll" }
