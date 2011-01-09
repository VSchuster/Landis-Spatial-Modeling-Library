// Copyright 2005-2006 University of Wisconsin
// All rights reserved. 
//
// The copyright holders license this file under the New (3-clause) BSD
// License (the "License").  You may not use this file except in
// compliance with the License.  A copy of the License is available at
//
//   http://www.opensource.org/licenses/bsd-license.php
//
// and is included in the NOTICE.txt file distributed with this work.
//
// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

namespace Wisc.Flel.GeospatialModeling.RasterIO
{
    /// <summary>
    /// A PixelBand with a signed 16-bit integer value.
    /// </summary>
    public class PixelBandShort
        : PixelBand<short, Edu.Wisc.Forest.Flel.Util.ByteMethods.Short>
    {
        /// <summary>
        /// Initializes a new instance with a default value of 0.
        /// </summary>
        public PixelBandShort()
            : base()
        {
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance with a specific value.
        /// </summary>
        public PixelBandShort(short initialValue)
            : base(initialValue)
        {
        }
    }
}
