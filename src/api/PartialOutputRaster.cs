// Copyright 2005-2006 University of Wisconsin
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
    public static class PartialOutputRaster
    {
        /// <summary>
        /// Signature for methods called with a partial output raster is
        /// closed.
        /// </summary>
        public delegate void CloseEventHandler(string     path,
                                               Dimensions dimensions,
                                               int        pixelsWritten);

        //---------------------------------------------------------------------

        /// <summary>
        /// The event when a partial output raster is called.
        /// </summary>
        public static event CloseEventHandler CloseEvent;

        //---------------------------------------------------------------------

        /// <summary>
        /// Called when a partial output raster is closed.
        /// </summary>
        public static void Closed(string     path,
                                  Dimensions dimensions,
                                  int        pixelsWritten)
        {
            if (CloseEvent != null)
                CloseEvent(path, dimensions, pixelsWritten);
        }
    }
}
