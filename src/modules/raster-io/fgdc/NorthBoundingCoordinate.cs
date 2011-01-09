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
    /// Northern-most coordinate of the limit of coverage expressed in
    /// latitude.
    /// </summary>
    /// <remarks>
    /// Section 1.5.1.3
    /// Type: real
    /// Domain: -90.0 <= North Bounding Coordinate <= 90.0;
    /// North Bounding Coordinate >= South Bounding Coordinate
    /// Short Name: northbc
    /// </remarks>
    public static class NorthBoundingCoordinate
    {
        public const string Name = "North Bounding Coordinate";
    }
}
