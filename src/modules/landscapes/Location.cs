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

using CellLocation = Wisc.Flel.GeospatialModeling.Grids.Location;

namespace Wisc.Flel.GeospatialModeling.Landscapes
{
    /// <summary>
    /// A location of a site on a landscape.
    /// </summary>
    /// <remarks>
    /// This value type is semantically equivalent to its counterpart in the
    /// Grids module of this library.  This type is defined in this module
    /// as a convenience to developers using this module, so they don't have
    /// to reference the Grids module if they are working with site locations.
    /// </remarks>
    public struct Location
    {
        private int row;
        private int column;

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="row">
        /// The row where the site is located.
        /// </param>
        /// <param name="column">
        /// The column where the site is located.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// The row or column is negative.
        /// </exception>
        public Location(int row,
                        int column)
        {
            if (row < 0)
                throw new System.ArgumentException("row parameter is negative");
            if (column < 0)
                throw new System.ArgumentException("column parameter is negative");
            this.row    = row;
            this.column = column;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The row where the site is located.
        /// </summary>
        public int Row
        {
            get {
                return row;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The column where the site is located.
        /// </summary>
        public int Column
        {
            get {
                return column;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Compares two locations for equality.
        /// </summary>
        public static bool operator ==(Location location1,
                                       Location location2)
        {
            return (location1.row == location2.row) && (location1.column == location2.column);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Compares two locations for inequality.
        /// </summary>
        public static bool operator !=(Location location1,
                                       Location location2)
        {
            return !(location1 == location2);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Converts a location to a bool.
        /// </summary>
        /// <returns>
        /// true if the location's row and column are both positive (not zero);
        /// false otherwise.
        /// </returns>
        public static implicit operator bool(Location location)
        {
            return (location.row > 0) && (location.column > 0);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Converts a site location in a landscape to a cell location in a
        /// grid.
        /// </summary>
        public static implicit operator CellLocation(Location location)
        {
            return new CellLocation(location.Row, location.Column);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Converts a cell location in a grid to a site location in a
        /// landscape.
        /// </summary>
        public static implicit operator Location(CellLocation cellLocation)
        {
            return new Location(cellLocation.Row, cellLocation.Column);
        }

        //---------------------------------------------------------------------

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;
            Location loc = (Location)obj;
            return this == loc;
        }

        //---------------------------------------------------------------------

        public override int GetHashCode()
        {
            return (int)(row ^ column);
        }

        //---------------------------------------------------------------------

        public override string ToString()
        {
            return string.Format("({0}, {1})", row, column);
        }
    }
}
