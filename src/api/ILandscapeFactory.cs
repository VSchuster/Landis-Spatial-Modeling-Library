// Copyright 2012 Green Code LLC
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
//   James Domingo, Green Code LLC

namespace Landis.SpatialModeling
{
    /// <summary>
    /// A factory that produces data structures for landscapes.
    /// </summary>
    public interface ILandscapeFactory
    {
        /// <summary>
        /// Creates a new landscape using an input grid of active sites.
        /// </summary>
        /// <param name="activeSites">
        /// A grid that indicates which sites are active.
        /// </param>
        ILandscape CreateLandscape(IInputGrid<bool> activeSites);

        //---------------------------------------------------------------------

        /// <summary>
        /// Creates a new landscape using an indexable grid of active sites.
        /// </summary>
        /// <param name="activeSites">
        /// A grid that indicates which sites are active.
        /// </param>
        ILandscape CreateLandscape(IIndexableGrid<bool> activeSites);
    }
}
