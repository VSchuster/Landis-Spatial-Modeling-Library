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
    /// An individual band value in a raster pixel.
    /// </summary>
    public interface IPixelBand
    {
        /// <summary>
        /// The type code for the band's data type.
        /// </summary>
        System.TypeCode TypeCode
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets the byte sequence representing the band's value.
        /// </summary>
        byte[] GetBytes();

        //---------------------------------------------------------------------


        /// <summary>
        /// Sets the byte sequence represeting the band's value.
        /// </summary>
        /// <param name="startIndex">
        /// The index in the byte array where the bytes for the band's value
        /// are located.
        /// </param>
        void SetBytes(byte[] bytes,
                      int    startIndex);
    }
}
