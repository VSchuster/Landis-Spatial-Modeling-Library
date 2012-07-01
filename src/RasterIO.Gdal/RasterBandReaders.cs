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

namespace Landis.RasterIO.Gdal
{
    public static class RasterBandReaders
    {
        public static RasterBandReader<byte> NewByteReader(OSGeo.GDAL.Band gdalBand)
        {
            return new RasterBandReader<byte>(gdalBand, GdalBandIO.ReadByteBlock);
        }

        public static RasterBandReader<short> NewShortReader(OSGeo.GDAL.Band gdalBand)
        {
            return new RasterBandReader<short>(gdalBand, GdalBandIO.ReadShortBlock);
        }

        public static RasterBandReader<int> NewIntReader(OSGeo.GDAL.Band gdalBand)
        {
            return new RasterBandReader<int>(gdalBand, GdalBandIO.ReadIntBlock);
        }

        public static RasterBandReader<float> NewFloatReader(OSGeo.GDAL.Band gdalBand)
        {
            return new RasterBandReader<float>(gdalBand, GdalBandIO.ReadFloatBlock);
        }

        public static RasterBandReader<double> NewDoubleReader(OSGeo.GDAL.Band gdalBand)
        {
            return new RasterBandReader<double>(gdalBand, GdalBandIO.ReadDoubleBlock);
        }
    }
}
