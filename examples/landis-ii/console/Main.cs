using Landis.RasterIO.Gdal;

namespace LandisII.Examples
{
    class ConsoleUI
    {
        public static void Main (string[] args)
        {
            System.Console.WriteLine("Initializing LANDIS-II core...");
            RasterFactory rasterFactory = new RasterFactory();
            Core modelCore = new Core(rasterFactory);

            // Run a scenario using the model's core.
            modelCore.RunScenario("path/to/scenario.txt");
        }
    }
}
