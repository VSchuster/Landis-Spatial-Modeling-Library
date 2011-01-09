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
    /// The metadata for a raster.
    /// </summary>
    public interface IMetadata
    {
        /// <summary>
        /// Gets a specific metadata value by name.
        /// </summary>
        /// <returns>
        /// null if there is no metadata associated with the given name.
        /// </returns>
        object this[string name]
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets a metadata value by name as a specific data type.
        /// </summary>
        /// <exception cref="System.InvalidCastException">
        /// Thrown if the metadata value cannot be converted to the specific
        /// type.
        /// </exception>
        /// <returns>
        /// true if there is a metadata value associated with the name, and it
        /// was converted to the specified data type and assigned to the
        /// dataValue parameter.  false if there is no metadata associated
        /// with the name.
        /// </returns>
        bool TryGetValue<T>(string name,
                            ref T  dataValue);
    }
}
