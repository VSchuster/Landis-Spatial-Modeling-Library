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

namespace Landis.SpatialModeling.CoreServices.RasterIO
{
    /// <summary>
    /// A block of data for a raster band.
    /// </summary>
    public class BandBlock<T>
        where T : struct
    {
        public T[] Buffer           { get; private set; }
        public BlockDimensions Size { get; private set; }
        public int XOffset          { get; set; }
        public int YOffset          { get; set; }
        public int UsedPortionXSize { get; set; }
        public int UsedPortionYSize { get; set; }
        public int PixelSpace       { get; private set; }
        public int LineSpace        { get; private set; }

        public BandBlock(BlockDimensions blockDimensions)
        {
            int bufferLength = blockDimensions.XSize * blockDimensions.YSize;
            Buffer = new T[bufferLength];

            Size = blockDimensions;

            PixelSpace = Band<T>.ComputeSize();
            LineSpace = PixelSpace * blockDimensions.XSize;
        }
    }
}
