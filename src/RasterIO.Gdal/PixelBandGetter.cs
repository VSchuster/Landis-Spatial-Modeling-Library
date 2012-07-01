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
    /// Accessor for getting a particular pixel band as another data type.
    /// </summary>
    public class PixelBandGetter<TPixelBand, TRasterBand> : PixelBandAccessor<TPixelBand>, IPixelBandGetter<TRasterBand>
        where TPixelBand : struct
        where TRasterBand : struct
    {
        private Converter<TPixelBand, TRasterBand> converter;

        public PixelBandGetter(PixelBand                          pixelBand,
                               Converter<TPixelBand, TRasterBand> converter)
            : base(pixelBand)
        {
            this.converter = converter;
        }

        public TRasterBand GetValue()
        {
            return converter(pixelBand.Value);
        }
    }
}
