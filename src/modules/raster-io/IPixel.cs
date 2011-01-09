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
    /// A raster pixel with one or more bands.
    /// </summary>
    public interface IPixel
    {
        /// <summary>
        /// The number of bands.
        /// </summary>
        int BandCount
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Accesses a particular band by its index.
        /// </summary>
        /// <param name="index">
        /// The index of the desired band.
        /// </param>
        /// <exception cref="System.IndexOutOfRangeException">
        /// index is not in the range of 0 to BandCount-1.
        /// </exception>
        IPixelBand this[int index]
        {
            get;
        }
    }
}
