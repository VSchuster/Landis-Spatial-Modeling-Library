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
    /// <summary>
    /// An input grid of data values that are read from the upper-left corner
    /// to the lower-right corner in row-major order.
    /// </summary>
    public interface IInputGrid<TValue>
        : IGrid, System.IDisposable
    {
        /// <summary>
        /// Reads the next data value from the grid.
        /// </summary>
        /// <exception cref="System.IO.EndOfStreamException">
        /// Thrown when there are no more data values left to read.
        /// </exception>
        TValue ReadValue();

        //---------------------------------------------------------------------

        /// <summary>
        /// Closes the input grid.
        /// </summary>
        void Close();
    }
}
