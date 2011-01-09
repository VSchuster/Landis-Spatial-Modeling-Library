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

using NUnit.Framework;

using Wisc.Flel.GeospatialModeling.Grids;

namespace Wisc.Flel.Test.GeospatialModeling.Grids
{
    [TestFixture]
    public class DataGridBool_Test
    {
        private Dimensions     dimensions;
        private DataGrid<bool> grid;
        private DataGrid<bool> grid_rowColCtor;
        private bool[,]        data;
        private Dimensions     dataDims;
        private DataGrid<bool> grid_dataCtor;

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
            dimensions = new Dimensions(22, 333);
            grid = new DataGrid<bool>(dimensions);
            grid_rowColCtor = new DataGrid<bool>(dimensions.Rows,
                                                 dimensions.Columns);
            data = new bool[7,5] { {false, false, false, false, false},
                                   {false, false, true,  false, false},
                                   {false, true,  true,  true,  false},
                                   {true,  true,  true,  true,  true },
                                   {false, true,  true,  true,  false},
                                   {false, false, true,  false, false},
                                   {false, false, false, false, false} };
            dataDims = new Dimensions(data.GetLength(0),
                                      data.GetLength(1));
            grid_dataCtor = new DataGrid<bool>(data);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DimCtorDims()
        {
            Assert.AreEqual(dimensions, grid.Dimensions);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DimCtorRows()
        {
            Assert.AreEqual(dimensions.Rows, grid.Rows);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DimCtorColumns()
        {
            Assert.AreEqual(dimensions.Columns, grid.Columns);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DimCtorCount()
        {
            Assert.AreEqual(dimensions.Rows * dimensions.Columns,
                            grid.Count);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DimCtorDataType()
        {
            Assert.AreEqual(typeof(bool), grid.DataType);
        }

        //---------------------------------------------------------------------

        [Test]
        public void RowColumnCtorDims()
        {
            Assert.AreEqual(dimensions, grid_rowColCtor.Dimensions);
        }

        //---------------------------------------------------------------------

        [Test]
        public void RowColumnCtorRows()
        {
            Assert.AreEqual(dimensions.Rows, grid_rowColCtor.Rows);
        }

        //---------------------------------------------------------------------

        [Test]
        public void RowColumnCtorColumns()
        {
            Assert.AreEqual(dimensions.Columns, grid_rowColCtor.Columns);
        }

        //---------------------------------------------------------------------

        [Test]
        public void RowColumnCtorCount()
        {
            Assert.AreEqual(dimensions.Rows * dimensions.Columns,
                            grid_rowColCtor.Count);
        }

        //---------------------------------------------------------------------

        [Test]
        public void RowColumnCtorDataType()
        {
            Assert.AreEqual(typeof(bool), grid_rowColCtor.DataType);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DefaultData()
        {
            for (int row = 1; row <= grid_rowColCtor.Rows; ++row)
                for (int col = 1; col <= grid_rowColCtor.Columns; ++col)
                    Assert.AreEqual(false, grid_rowColCtor[row, col]);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DataCtorDims()
        {
            Assert.AreEqual(dataDims, grid_dataCtor.Dimensions);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DataCtorRows()
        {
            Assert.AreEqual(dataDims.Rows, grid_dataCtor.Rows);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DataCtorColumns()
        {
            Assert.AreEqual(dataDims.Columns, grid_dataCtor.Columns);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DataCtorCount()
        {
            Assert.AreEqual(dataDims.Rows * dataDims.Columns,
                            grid_dataCtor.Count);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DataCtorDataType()
        {
            Assert.AreEqual(typeof(bool), grid_dataCtor.DataType);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DataCtorData()
        {
            for (int row = 1; row <= dataDims.Rows; ++row)
                for (int col = 1; col <= dataDims.Columns; ++col)
                    Assert.AreEqual(data[row-1, col-1],
                                    grid_dataCtor[row, col]);
        }

        //---------------------------------------------------------------------

        [Test]
        public void LocationIndexer()
        {
            bool cellValue = false;
            for (int row = 1; row <= grid.Rows; ++row)
                for (int col = 1; col <= grid.Columns; ++col) {
                    cellValue = !cellValue;
                    Location loc = new Location(row, col);
                    grid[loc] = cellValue;
                    Assert.AreEqual(cellValue, grid[loc]);
                }
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.IndexOutOfRangeException))]
        public void LocationIndexerRow0()
        {
            Location badLoc = new Location(0, 1);
            grid[badLoc] = true;
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.IndexOutOfRangeException))]
        public void LocationIndexerColumn0()
        {
            Location badLoc = new Location(1, 0);
            grid[badLoc] = true;
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.IndexOutOfRangeException))]
        public void LocationIndexerRowTooBig()
        {
            Location badLoc = new Location(dimensions.Rows + 1, 1);
            grid[badLoc] = true;
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.IndexOutOfRangeException))]
        public void LocationIndexerColumnTooBig()
        {
            Location badLoc = new Location(1, dimensions.Columns + 1);
            grid[badLoc] = true;
        }

        //---------------------------------------------------------------------

        [Test]
        public void RowColumnIndexer()
        {
            bool cellValue = false;
            for (int row = 1; row <= grid.Rows; ++row)
                for (int col = 1; col <= grid.Columns; ++col) {
                    cellValue = !cellValue;
                    grid[row, col] = cellValue;
                    Assert.AreEqual(cellValue, grid[row, col]);
                }
        }

        //---------------------------------------------------------------------

        [Test]
        public void Enumerator()
        {
            bool cellValue = false;
            for (int row = 1; row <= grid.Rows; ++row)
                for (int col = 1; col <= grid.Columns; ++col) {
                    cellValue = !cellValue;
                    Location loc = new Location(row, col);
                    grid[loc] = cellValue;
                }

            cellValue = false;
            foreach (bool b in grid) {
                cellValue = !cellValue;
                Assert.AreEqual(cellValue, b);
            }
        }
    }
}
