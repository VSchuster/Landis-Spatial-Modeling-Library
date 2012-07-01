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
    /// Accessor for getting or seting a particular pixel band.
    /// </summary>
    public abstract class PixelBandAccessor<T>
        where T : struct
    {
        protected Band<T> pixelBand;

        protected PixelBandAccessor(PixelBand pixelBand)
        {
            this.pixelBand = pixelBand as Band<T>;
            if (this.pixelBand == null)
                throw new System.ArgumentException("pixelBand is not a Band<T>");
        }
    }
}
