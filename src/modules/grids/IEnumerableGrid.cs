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
    /// A grid that whose cells (elements) can be enumerated.
    /// </summary>
    public interface IEnumerableGrid<TCell>
        : IGrid, System.Collections.Generic.IEnumerable<TCell>
    {
    }
}
