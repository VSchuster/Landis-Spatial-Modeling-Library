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

using System;
using System.Collections.Generic;
using System.IO;

namespace Wisc.Flel.GeospatialModeling.RasterIO
{
    /// <summary>
    /// Information about a raster driver.
    /// </summary>
    public class DriverInfo
        : Edu.Wisc.Forest.Flel.Util.PlugIns.Info
    {
        IDictionary<string, FileAccess> formats;

        //---------------------------------------------------------------------

        public DriverInfo(string                          name,
                          string                          implementationName,
                          IDictionary<string, FileAccess> formats)
            : base(name, typeof(IDriver), implementationName)
        {
            if (formats == null)
                throw new ArgumentNullException("formats argument is null");

            this.formats = formats;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets the file access that the driver supports for a particular
        /// format.
        /// </summary>
        public FileAccess this[string format]
        {
            get {
                FileAccess fileAccess;
                formats.TryGetValue(format, out fileAccess);
                return fileAccess;
            }
        }
    }
}
