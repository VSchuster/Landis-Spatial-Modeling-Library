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

namespace Landis.SpatialModeling.RasterIO.Gdal
{
    /// <summary>
    /// A writer for a particular GDAL raster band.
    /// </summary>
    public class RasterBandWriter<T>
        where T : struct
    {
        private OSGeo.GDAL.Band gdalBand;
        private BlockDimensions blockDimensions;
        private GdalBandIO.WriteBlock<T> writeBlock;

        public RasterBandWriter(OSGeo.GDAL.Band          gdalBand,
                                GdalBandIO.WriteBlock<T> writeBlock)
        {
            this.gdalBand = gdalBand;
            this.writeBlock = writeBlock;

            gdalBand.GetBlockSize(out blockDimensions.XSize, out blockDimensions.YSize);
        }

        public BlockDimensions BlockSize
        {
            get {
                return blockDimensions;
            }
        }

        public int Rows
        {
            get {
                return gdalBand.YSize;
            }
        }

        public int Columns
        {
            get {
                return gdalBand.XSize;
            }
        }

        public void WriteBlock(BandBlock<T> block)
        {
            writeBlock(gdalBand, block);
        }
    }
}
