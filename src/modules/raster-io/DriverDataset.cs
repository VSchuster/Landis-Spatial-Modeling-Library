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

using System.Collections.Generic;
using System.IO;

namespace Wisc.Flel.GeospatialModeling.RasterIO
{
    /// <summary>
    /// A collection of information about installed raster drivers.
    /// </summary>
    public class DriverDataset
    {
        private string path;
        private List<DriverInfo> drivers;
        private Dictionary<string, List<DriverInfo>> formats;

        //---------------------------------------------------------------------

        public DriverDataset(string path)
        {
            this.path = path;
            PersistentDriverDataset dataset = PersistentDriverDataset.Load(path);

            drivers = new List<DriverInfo>();
            foreach (PersistentDriverDataset.DriverInfo info in dataset.Drivers) {
                //  Create a dictionary from list of formats
                Dictionary<string, FileAccess> formats;
                formats = new Dictionary<string, FileAccess>();
                foreach (PersistentDriverDataset.FormatAccess formatAccess in info.Formats)
                    formats[formatAccess.Format] = formatAccess.Access;

                drivers.Add(new DriverInfo(info.Name,
                                           info.ImplementationName,
                                           formats));
            }

            this.formats = new Dictionary<string, List<DriverInfo>>();
            foreach (PersistentDriverDataset.FormatDrivers formatDrivers in dataset.Formats) {
                List<DriverInfo> driverList = new List<DriverInfo>();
                foreach (string driverName in formatDrivers.Drivers) {
                    DriverInfo driverInfo = null;
                    foreach (DriverInfo info in this.drivers)
                        if (info.Name == driverName) {
                            driverInfo = info;
                            break;
                        }
                    if (driverInfo == null)
                        throw new System.ApplicationException(string.Format("Unknown raster driver: \"{0}\"", driverName));
                    driverList.Add(driverInfo);
                }
                formats[formatDrivers.Format] = driverList;
            }
        }

        //---------------------------------------------------------------------

        public IList<DriverInfo> GetDrivers(string format)
        {
            List<DriverInfo> drivers;
            formats.TryGetValue(format, out drivers);
            return drivers;
        }
    }
}
