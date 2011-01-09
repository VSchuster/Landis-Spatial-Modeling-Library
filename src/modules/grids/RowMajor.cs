// Copyright 2004-2006 University of Wisconsin
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

namespace Wisc.Flel.GeospatialModeling.Grids
{
    /// <summary>
    /// Methods for traversing locations in a grid in row-major order.
    /// </summary>
    public static class RowMajor
    {
        /// <summary>
        /// Gets the next location in row-major order.
        /// </summary>
        /// <param name="columns">
        /// The number of columns in the grid being traversed.
        /// </param>
        public static Location Next(Location location,
                                    int      columns)
        {
            if (location.Column < columns) {
                return new Location(location.Row, location.Column + 1);
            }
            else {
                return new Location(location.Row + 1, 1);
            }
        }
    }
}
