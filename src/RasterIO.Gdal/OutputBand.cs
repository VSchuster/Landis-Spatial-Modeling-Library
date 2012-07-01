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

namespace Landis.SpatialModeling.RasterIO.Gdal
{
    public class OutputBand<T> : IOutputBand
        where T : struct
    {
        private RasterBandWriter<T> rasterBandWriter;
        private IPixelBandGetter<T> pixelBandGetter;
        private BlockDimensions blockDimensions;
        private BandBuffer<T> bandBuffer;
        private bool hasData;

        public OutputBand(RasterBandWriter<T> rasterBandWriter,
                          IPixelBandGetter<T> pixelBandGetter)
        {
            this.rasterBandWriter = rasterBandWriter;
            this.pixelBandGetter = pixelBandGetter;
            blockDimensions = rasterBandWriter.BlockSize;
            bandBuffer = new BandBuffer<T>(blockDimensions, new Dimensions(rasterBandWriter.Rows, rasterBandWriter.Columns));
            hasData = false;
        }

        /// <summary>
        /// Write the value of the corresponding band in the buffer pixel to
        /// the raster band.
        /// </summary>
        public void WriteValueFromBufferPixel()
        {
            // Get the band value from the pixel band
            T bandValue = pixelBandGetter.GetValue();

            bandBuffer.WriteValue(bandValue);
            hasData = true;

            if (bandBuffer.AtEnd)
                WriteBuffer();
        }

        private void WriteBuffer()
        {
            foreach (BandBlock<T> block in bandBuffer.Blocks)
                rasterBandWriter.WriteBlock(block);
            bandBuffer.Reset();
            hasData = false;
        }

        public void Flush()
        {
            if (hasData)
                WriteBuffer();
        }
    }
}
