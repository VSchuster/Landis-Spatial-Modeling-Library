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

using System.Collections;
using System.Collections.Generic;

namespace Wisc.Flel.GeospatialModeling.Grids
{
    /// <summary>
    /// A grid with data values of a particular type.
    /// </summary>
    public class DataGrid<TData>
        : Grid, IIndexableGrid<TData>, IEnumerableGrid<TData>
    {
        private TData[,] data;

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes an instance with specific dimensions.  All cells have
        /// the default value for <i>TData</i> (null if <i>TData</i> is a class
        /// or interface).
        /// </summary>
        public DataGrid(Dimensions dimensions)
            : base(dimensions)
        {
            if (Count > 0) {
                this.data = new TData[dimensions.Rows, dimensions.Columns];
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes an instance with specific dimensions.  All cells have
        /// the default value for <i>TData</i> (null if <i>TData</i> is a class
        /// or interface).
        /// </summary>
        public DataGrid(int rows,
                        int columns)
            : base(rows, columns)
        {
            if (Count > 0) {
                this.data = new TData[rows, columns];
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance from a 2-dimensional array.
        /// </summary>
        public DataGrid(TData[,] data)
            : base(data.GetLength(0), data.GetLength(1))
        {
            if (Count > 0) {
                this.data = (TData[,]) data.Clone();
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// The type of data in the grid.
        /// </summary>
        public System.Type DataType
        {
            get {
                return typeof(TData);
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the data value at a particular location.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown if row is not between 1 and the number of rows in the grid,
        /// or if column is not between 1 and the number of columns in the
        /// grid.
        /// </exception>
        public TData this [int row,
                           int column]
        {
            get {
                return this[new Location(row, column)];
            }
            set {
                this[new Location(row, column)] = value;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the data value at a particular location.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown if the location is not valid for the grid.  The location's
        /// row must be between 1 and the number of rows in the grid, and its
        /// column must be between 1 and the number of columns in the grid.
        /// </exception>
        public TData this [Location location]
        {
            get {
                MustBeValid(location);
                return data[location.Row-1, location.Column-1];
            }
            set {
                MustBeValid(location);
                data[location.Row-1, location.Column-1] = value;
            }
        }

        //---------------------------------------------------------------------

        private void MustBeValid(Location location)
        {
            if (location.Row < 1)
                throw new System.IndexOutOfRangeException("Location's row is 0");
            if (location.Row > Rows)
                throw new System.IndexOutOfRangeException("Location's row is > # of rows in grid");
            if (location.Column < 1)
                throw new System.IndexOutOfRangeException("Location's column is 0");
            if (location.Column > Columns)
                throw new System.IndexOutOfRangeException("Location's column is > # of columns in grid");
        }

        //---------------------------------------------------------------------

        public IEnumerator<TData> GetEnumerator()
        {
            for (int row = 0; row < Rows; ++row) {
                for (int column = 0; column < Columns; ++column) {
                    yield return data[row, column];
                }
            }
        }

        //---------------------------------------------------------------------

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
