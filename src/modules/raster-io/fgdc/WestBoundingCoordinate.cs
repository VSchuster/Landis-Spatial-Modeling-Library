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

namespace Gov.Fgdc.Csdgm
{
    /// <summary>
    /// Western-most coordinate of the limit of coverage expressed in longitude.
    /// </summary>
    /// <remarks>
    /// Section 1.5.1.1
    /// Type: real
    /// Domain: -180.0 <= West Bounding Coordinate < 180.0
    /// Short Name: westbc
    /// </remarks>
    public static class WestBoundingCoordinate
    {
        public const string Name = "West Bounding Coordinate";
    }
}

