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
    /// Input raster band
    /// </summary>
    public interface IInputBand
    {
        /// <summary>
        /// Read the value from the raster band into the corresponding band of
        /// the buffer pixel.
        /// </summary>
        void ReadValueIntoBufferPixel();
    }
}
