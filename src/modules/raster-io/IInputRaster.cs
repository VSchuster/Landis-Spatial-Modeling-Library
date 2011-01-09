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

namespace Wisc.Flel.GeospatialModeling.RasterIO
{
    /// <summary>
    /// An input raster file from which pixel data are read.  Pixels are read
    /// in row-major order, from the upper-left corner to the lower-right
    /// corner.
    /// </summary>
    public interface IInputRaster<T>
        : IRaster
        where T : IPixel, new()
    {
        /// <summary>
        /// Reads the next pixel from the raster.
        /// </summary>
        /// <exception cref="System.IO.EndOfStreamException">
        /// There are no more pixels left to read.
        /// </exception>
        /// <exception cref="System.IO.IOException">
        /// An error occurred reading the pixel data from the raster.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// This method was called too many times (more than the number of
        /// pixels in the raster).
        /// </exception>
        T ReadPixel();
    }
}
