// Copyright 2006 University of Wisconsin
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

using System.Collections;
using System.Collections.Generic;

namespace Landis.SpatialModeling.CoreServices.Landscapes.DataIndexes
{
    /// <summary>
    /// An enumerator for a collection of data indexes and locations for a
    /// landscape.
    /// </summary>
    /// <remarks>
    /// Traverses the landscape from the upper-left corner to the lower-right
    /// corner in row-major order.
    /// </remarks>
    public abstract class Enumerator
        : EnumeratorBase
    {
        private int rows;
        private int columns;
        private Location currentLocation;
        private uint currentDataIndex;

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="landscapeDimensions">
        /// The dimensions of the landscape.
        /// </param>
        protected Enumerator(Dimensions landscapeDimensions)
            : base()
        {
            ResetLocationAndIndex();
            rows = landscapeDimensions.Rows;
            columns = landscapeDimensions.Columns;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The number of rows in the landscape.
        /// </summary>
        protected int Rows
        {
            get {
                return rows;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The number of coluns in the landscape.
        /// </summary>
        protected int Columns
        {
            get {
                return columns;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The current landscape location where the enumerator is at.
        /// </summary>
        public Location CurrentLocation
        {
            get {
                return currentLocation;
            }

            protected set {
                currentLocation = value;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The current data index associated with the current location where
        /// the enumerator is at.
        /// </summary>
        public uint CurrentDataIndex
        {
            get {
                return currentDataIndex;
            }

            protected set {
                currentDataIndex = value;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Resets the current location to (0,0) and the current data index to
        /// InactiveSite.DataIndex.
        /// </summary>
        protected void ResetLocationAndIndex()
        {
            currentLocation = new Location();
            currentDataIndex = InactiveSite.DataIndex;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Moves the enumerator to next location on the landscape.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully moved to the next location;
        /// false if the enumerator moved past the last location.
        /// </returns>
        /// <remarks>
        /// If the enumerator is traversing only active locations, then this
        /// method moves the enumerator to the next active location in row-
        /// major order.
        /// </remarks>
        public abstract bool MoveNext();

        //---------------------------------------------------------------------

        /// <summary>
        /// Resets the enumerator to its initial position.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            ResetLocationAndIndex();
        }
    }
}
