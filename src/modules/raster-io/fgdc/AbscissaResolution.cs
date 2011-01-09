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
    /// The (nominal) minimum distance between the "x" or column values of two
    /// adjacent points, expressed in Planar Distance Units of measure.
    /// </summary>
    /// <remarks>
    /// Section 4.1.2.4.2.1
    /// Type: real
    /// Domain: Abscissa Resolution > 0.0
    /// Short Name: absres
    /// </remarks>
    public static class AbscissaResolution
    {
        public const string Name = "Abscissa Resolution";
    }
}
