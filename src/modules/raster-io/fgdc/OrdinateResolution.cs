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
    /// the (nominal) minimum distance between the "y" or row values of two
    /// adjacent points, expressed in Planar Distance Units of measure.
    /// </summary>
    /// <remarks>
    /// Section 4.1.2.4.2.2
    /// Type: real
    /// Domain: Ordinate Resolution > 0.0
    /// Short Name: ordres
    /// </remarks>
    public static class OrdinateResolution
    {
        public const string Name = "Ordinate Resolution";
    }
}
