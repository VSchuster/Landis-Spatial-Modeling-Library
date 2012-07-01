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

namespace Landis.SpatialModeling.RasterIO
{
    /// <summary>
    /// Output raster band
    /// </summary>
    public interface IOutputBand
    {
        /// <summary>
        /// Write the value from the corresponding band in the buffer pixel
        /// to the raster band.
        /// </summary>
        void WriteValueFromBufferPixel();

        /// <summary>
        /// Flush any data that has not yet been written to the filesystem.
        /// </summary>
        void Flush();
    }
}
