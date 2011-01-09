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
    //  A local class derived from Grid class so we can access its
    //  constructors.
    internal class Grid
        : Wisc.Flel.GeospatialModeling.Grids.Grid
    {
        public Grid(Dimensions dimensions)
            : base(dimensions)
        {
        }

        //---------------------------------------------------------------------

        public Grid(int rows,
                    int columns)
            : base(rows, columns)
        {
        }
    }

    //-------------------------------------------------------------------------

    [TestFixture]
    public class Grid_Test
    {
        private Dimensions dims_4321_789;
        private Grid grid_4321_789;
        private Grid grid_22_55555;
        private IGrid iGrid_22_55555;

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
            dims_4321_789 = new Dimensions(4321, 789);
            grid_4321_789 = new Grid(dims_4321_789);
            grid_22_55555 = new Grid(22, 55555);
            iGrid_22_55555 = grid_22_55555;
        }

        //---------------------------------------------------------------------

        [Test]
        public void Test01_DimCtorDims()
        {
            Assert.AreEqual(dims_4321_789, grid_4321_789.Dimensions);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Test02_DimCtorRows()
        {
            Assert.AreEqual(dims_4321_789.Rows, grid_4321_789.Rows);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Test03_DimCtorColumns()
        {
            Assert.AreEqual(dims_4321_789.Columns, grid_4321_789.Columns);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Test04_DimCtorCount()
        {
            Assert.AreEqual(dims_4321_789.Rows * dims_4321_789.Columns,
                            grid_4321_789.Count);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Test05_RowsColsCtorDims()
        {
            Assert.AreEqual(new Dimensions(22, 55555),
                            grid_22_55555.Dimensions);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Test06_RowsColsCtorRows()
        {
            Assert.AreEqual(22, grid_22_55555.Rows);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Test07_RowsColsCtorColumns()
        {
            Assert.AreEqual(55555, grid_22_55555.Columns);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Test08_RowsColsCtorCount()
        {
            Assert.AreEqual(22 * 55555, grid_22_55555.Count);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Test09_IGridDims()
        {
            Assert.AreEqual(grid_22_55555.Dimensions,
                            iGrid_22_55555.Dimensions);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Test10_IGridRows()
        {
            Assert.AreEqual(grid_22_55555.Rows, iGrid_22_55555.Rows);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Test11_IGridColumns()
        {
            Assert.AreEqual(grid_22_55555.Columns, iGrid_22_55555.Columns);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Test12_IGridCount()
        {
            Assert.AreEqual(grid_22_55555.Count, iGrid_22_55555.Count);
        }
    }
}
