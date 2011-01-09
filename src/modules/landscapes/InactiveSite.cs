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

namespace Wisc.Flel.GeospatialModeling.Landscapes
{
    public static class InactiveSite
    {
        /// <summary>
        /// The data index assigned to inactive sites.
        /// </summary>
        public const uint DataIndex = 0;

        //---------------------------------------------------------------------

        /// <summary>
        /// Creates a new inactive site on a landscape.
        /// </summary>
        /// <param name="landscape">
        ///  The landscape where the site is located.
        /// </param>
        /// <param name="location">
        ///  The location of the site.
        /// </param>
        internal static Site Create(ILandscape landscape,
                                    Location   location)
        {
            return new Site(landscape, location, DataIndex);
        }

    }
}
