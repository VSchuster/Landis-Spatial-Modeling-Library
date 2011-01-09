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

namespace Wisc.Flel.GeospatialModeling.Landscapes
{
    /// <summary>
    /// The location of a site relative to another site (known as the origin
    /// site).
    /// </summary>
    public struct RelativeLocation
    {
        private int row;
        private int column;

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new relative location.
        /// </summary>
        public RelativeLocation(int row,
                                int column)
        {
            this.row    = row;
            this.column = column;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The row where the site is located, relative to the row of the
        /// origin site.
        /// </summary>
        public int Row
        {
            get {
                return row;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The column where the site is located, relative to the column of
        /// the origin site.
        /// </summary>
        public int Column
        {
            get {
                return column;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Compares two relative locations for equality.
        /// </summary>
        public static bool operator ==(RelativeLocation location1,
                                       RelativeLocation location2)
        {
            return (location1.row == location2.row) && (location1.column == location2.column);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Compares two locations for inequality.
        /// </summary>
        public static bool operator !=(RelativeLocation location1,
                                       RelativeLocation location2)
        {
            return !(location1 == location2);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Computes the absolute location that corresponds to a relative
        /// location.
        /// </summary>
        /// <returns>
        /// null if the relative location refers to an invalid location (row or
        /// column is negative or greater than 2,147,483,647).
        /// </returns>
        public static Location? operator +(Location         origin,
                                           RelativeLocation relativeLocation)
        {
            long row = origin.Row + relativeLocation.Row;
            long column = origin.Column + relativeLocation.Column;
            if (row < 0 || row > int.MaxValue ||
                column < 0 || column > int.MaxValue)
                return null;
            else
                return new Location((int) row, (int) column);
        }

        //---------------------------------------------------------------------

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;
            RelativeLocation loc = (RelativeLocation)obj;
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
            return "(" + row + ", " + column + ")";
        }
    }
}
