// Copyright 2004-2006 University of Wisconsin
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

using Wisc.Flel.GeospatialModeling.Grids;

namespace Wisc.Flel.GeospatialModeling.RasterIO
{
    /// <summary>
    /// A file with raster data.
    /// </summary>
    public interface IRaster
        : System.IDisposable
    {
        /// <summary>
        /// The path used to open/create the raster.
        /// </summary>
        string Path
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The dimensions of the raster.
        /// </summary>
        Dimensions Dimensions
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Metadata for the raster.
        /// </summary>
        IMetadata Metadata
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Closes the raster, releasing any unmanaged resources.
        /// </summary>
        void Close();
    }
}
