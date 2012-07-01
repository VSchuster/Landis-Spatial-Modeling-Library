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
    public static class RasterBandWriters
    {
        public static RasterBandWriter<byte> NewByteWriter(OSGeo.GDAL.Band gdalBand)
        {
            return new RasterBandWriter<byte>(gdalBand, GdalBandIO.WriteByteBlock);
        }

        public static RasterBandWriter<short> NewShortWriter(OSGeo.GDAL.Band gdalBand)
        {
            return new RasterBandWriter<short>(gdalBand, GdalBandIO.WriteShortBlock);
        }

        public static RasterBandWriter<int> NewIntWriter(OSGeo.GDAL.Band gdalBand)
        {
            return new RasterBandWriter<int>(gdalBand, GdalBandIO.WriteIntBlock);
        }

        public static RasterBandWriter<float> NewFloatWriter(OSGeo.GDAL.Band gdalBand)
        {
            return new RasterBandWriter<float>(gdalBand, GdalBandIO.WriteFloatBlock);
        }

        public static RasterBandWriter<double> NewDoubleWriter(OSGeo.GDAL.Band gdalBand)
        {
            return new RasterBandWriter<double>(gdalBand, GdalBandIO.WriteDoubleBlock);
        }
    }
}
