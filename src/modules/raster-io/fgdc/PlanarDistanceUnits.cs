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
    /// Units of measure used for distances.
    /// </summary>
    /// <remarks>
    /// Section 4.1.2.4.4
    /// Type: text
    /// Domain: "meters" "international feet" "survey feet" free text
    /// Short Name: plandu
    /// </remarks>
    public static class PlanarDistanceUnits
    {
        public const string Name = "Planar Distance Units";

        public const string Meters = "meters";
        public const string InternationalFeet = "international feet";
        public const string SurveyFeet = "survey feet";
    }
}
