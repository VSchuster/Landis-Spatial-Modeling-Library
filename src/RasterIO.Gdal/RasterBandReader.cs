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

namespace Landis.SpatialModeling.CoreServices.RasterIO
{
    /// <summary>
    /// A reader for a particular GDAL raster band.
    /// </summary>
    public class RasterBandReader<T>
        where T : struct
    {
        private OSGeo.GDAL.Band gdalBand;
        private BlockDimensions blockDimensions;
        private GdalBandIO.ReadBlock<T> readBlock;

        public RasterBandReader(OSGeo.GDAL.Band         gdalBand,
                                GdalBandIO.ReadBlock<T> readBlock)
        {
            this.gdalBand = gdalBand;
            this.readBlock = readBlock;

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

        public void ReadBlock(BandBlock<T> block)
        {
            readBlock(gdalBand, block);
        }
    }
}
