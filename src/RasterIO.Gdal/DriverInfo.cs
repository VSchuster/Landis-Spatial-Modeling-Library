// Copyright 2010 Green Code LLC
// All rights reserved.
//
// The copyright holders license this file under the New (3-clause) BSD
// License (the "License").  You may not use this file except in
// compliance with the License.  A copy of the License is available at
//
//   http://www.opensource.org/licenses/BSD-3-Clause
//
// and is included in the NOTICE.txt file distributed with this work.
//
// Contributors:
//   James Domingo, Green Code LLC

using System;
using OSGeo.GDAL;

namespace Landis.SpatialModeling.CoreServices
{
    public class DriverInfo
    {
        private string shortName;
        private string longName;
        private bool hasCreate;
        private bool hasCreateCopy;

        public string ShortName { get { return shortName; } }

        public string LongName { get { return longName; } }

        public bool HasCreate { get { return hasCreate; } }

        public bool HasCreateCopy { get { return hasCreateCopy; } }

        public DriverInfo(Driver gdalDriver)
        {
            shortName = gdalDriver.ShortName;
            longName = gdalDriver.LongName;
            hasCreate = GetMetadataItem(gdalDriver, "DCAP_CREATE") == "YES";
            hasCreateCopy = GetMetadataItem(gdalDriver, "DCAP_CREATECOPY") == "YES";
        }

        private string GetMetadataItem(Driver gdalDriver,
                                       string itemName)
        {
            string itemValue = gdalDriver.GetMetadataItem(itemName, "");
            return itemValue;
        }

        public static int CompareShortName(DriverInfo x,
                                           DriverInfo y)
        {
            return x.ShortName.CompareTo(y.ShortName);
        }
    }
}
