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
    /// <summary>
    /// Represents a variable with values for the sites on a landscape.
    /// </summary>
    public interface ISiteVariable
    {
        /// <summary>
        /// The data type of the values.
        /// </summary>
        System.Type DataType
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Indicates whether the inactive sites share a common value or have
        /// distinct values.
        /// </summary>
        InactiveSiteMode Mode
        {
            get;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The landscape that the site variable was created for.
        /// </summary>
        ILandscape Landscape
        {
            get;
        }
    }
}
