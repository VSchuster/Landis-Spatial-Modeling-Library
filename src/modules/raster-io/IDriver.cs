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

namespace Wisc.Flel.GeospatialModeling.RasterIO
{
    /// <summary>
    /// A driver for opening raster files for reading and creating new files
    /// for writing.
    /// </summary>
    public interface IDriver
        : IRasterFactory
    {
        /// <summary>
        /// The list of raster formats that this driver recognizes.  A format
        /// is denoted by a filename extension, e.g., ".xyx".
        /// </summary>
        string[] Formats
        {
            get;
        }
    }
}
