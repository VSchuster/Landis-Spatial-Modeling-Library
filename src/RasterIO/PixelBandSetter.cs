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

namespace Landis.SpatialModeling.RasterIO
{
    /// <summary>
    /// Accessor for setting a particular pixel band with another data type.
    /// </summary>
    public class PixelBandSetter<TPixelBand, TRasterBand> : PixelBandAccessor<TPixelBand>, IPixelBandSetter<TRasterBand>
        where TPixelBand : struct
        where TRasterBand : struct
    {
        private Converter<TRasterBand, TPixelBand> converter;  // TRasterBand -> TPixelBand

        public PixelBandSetter(PixelBand                          pixelBand,
                               Converter<TRasterBand, TPixelBand> converter)
            : base(pixelBand)
        {
            this.converter = converter;
        }

        public void SetValue(TRasterBand newValue)
        {
            pixelBand.Value = converter(newValue);
        }
    }
}
