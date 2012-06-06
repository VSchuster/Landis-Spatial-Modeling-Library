This simple example demonstrates how the LSML is used by the LANDIS-II
model (http://www.landis-ii.org).  As with LSML, use Premake to generate
the C# solution and project files for the example.  The solution will
put the example's assemblies (executable and DLLs) into the same build
directories as the LSML solution:

  ../../build/Debug
  ../../build/Release

There is a Windows command script called "run-example.cmd" in the same
folder as this README file.  Use that script to run the example; it
requires one parameter which is the configuration to run:

  .\run-example debug
  .\run-example release

The example creates an ERDAS Imagine file (*.img), and then reads its
contents.
