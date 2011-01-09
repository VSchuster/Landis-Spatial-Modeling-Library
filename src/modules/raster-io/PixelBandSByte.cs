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
    /// A PixelBand with a signed 8-bit integer value.
    /// </summary>
    public class PixelBandSByte
        : PixelBand<sbyte, Edu.Wisc.Forest.Flel.Util.ByteMethods.SByte>
    {
        /// <summary>
        /// Initializes a new instance with a default value of 0.
        /// </summary>
        public PixelBandSByte()
            : base()
        {
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance with a specific value.
        /// </summary>
        public PixelBandSByte(sbyte initialValue)
            : base(initialValue)
        {
        }
    }
}
