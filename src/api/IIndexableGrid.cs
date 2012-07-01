// Copyright 2004-2006 University of Wisconsin
// All rights reserved. 
//
// The copyright holders license this file under the New (3-clause) BSD
// License (the "License").  You may not use this file except in
// compliance with the License.  A copy of the License is available at
//
//   http://www.opensource.org/licenses/BSD-3-Clause
//
// and is included in the NOTICE.txt file distributed with this work.
//
// Contributors:
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

namespace Landis.SpatialModeling.CoreServices
{
    /// <summary>
    /// A grid whose cells can be accessed by their locations.
    /// </summary>
    public interface IIndexableGrid<TCell>
        : IGrid
    {
        TCell this [int row,
                    int column]
        {
            get;
            set;
        }

        //---------------------------------------------------------------------

        TCell this [Location location]
        {
            get;
            set;
        }
    }
}
