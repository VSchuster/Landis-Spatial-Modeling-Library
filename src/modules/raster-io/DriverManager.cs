// Copyright 2005-2006 University of Wisconsin
// All rights reserved. 
//
// The copyright holders license this file under the New (3-clause) BSD
// License (the "License").  You may not use this file except in
// compliance with the License.  A copy of the License is available at
//
//   http://www.opensource.org/licenses/bsd-license.php
//
// and is included in the NOTICE.txt file distributed with this work.
//
// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

using Edu.Wisc.Forest.Flel.Util.PlugIns;
using System;
using System.Collections.Generic;
using System.IO;
using Wisc.Flel.GeospatialModeling.Grids;

namespace Wisc.Flel.GeospatialModeling.RasterIO
{
    /// <summary>
    /// Manages a collection of raster drivers.
    /// </summary>
    public class DriverManager
        : IDriverManager
    {
        private DriverDataset dataset;
        private Dictionary<string, IDriver> loadedDrivers;

        //---------------------------------------------------------------------

        public DriverManager(string datasetPath)
        {
            dataset = new DriverDataset(datasetPath);
            loadedDrivers = new Dictionary<string, IDriver>();
        }

        //---------------------------------------------------------------------

        public IInputRaster<TPixel> OpenRaster<TPixel>(string path)
            where TPixel : IPixel, new()
        {
            IDriver driver = GetDriver(path, FileAccess.Read);
            return driver.OpenRaster<TPixel>(path);
        }

        //---------------------------------------------------------------------

        public IOutputRaster<TPixel> CreateRaster<TPixel>(string     path,
                                                          Dimensions dimensions,
                                                          IMetadata  metadata)
             where TPixel : IPixel, new()
        {
            IDriver driver = GetDriver(path, FileAccess.Write);
            return driver.CreateRaster<TPixel>(path, dimensions, metadata);
        }

        //---------------------------------------------------------------------

        private IDriver GetDriver(string     path,
                                  FileAccess fileAccess)
        {
            string format = Path.GetExtension(path);
            if (string.IsNullOrEmpty(format))
                throw NewAppException("No file extension specified for raster map");

            IList<DriverInfo> drivers = dataset.GetDrivers(format);
            if (drivers == null)
                throw NewAppException("Unknown raster format: {0}", format);

            //  Find first driver that supports the requested access
            DriverInfo firstDriver = null;
            foreach (DriverInfo driverInfo in drivers) {
                if ((driverInfo[format] & fileAccess) == fileAccess) {
                    firstDriver = driverInfo;
                    break;
                }
            }
            if (firstDriver == null) {
                string action;
                if (fileAccess == FileAccess.Read)
                    action = "read";
                else
                    action = "write";
                throw NewAppException("No drivers {0} the raster format \"{1}\"",
                                      action, format);
            }

            IDriver driver;
            if (! loadedDrivers.TryGetValue(firstDriver.Name, out driver)) {
                driver = Loader.Load<IDriver>(firstDriver);
                loadedDrivers[firstDriver.Name] = driver;
            }
            return driver;
        }

        //---------------------------------------------------------------------

        private ApplicationException NewAppException(string          message,
                                                     params object[] mesgArgs)
        {
            return new ApplicationException(string.Format("Error: " + message,
                                                          mesgArgs));
        }
    }
}
