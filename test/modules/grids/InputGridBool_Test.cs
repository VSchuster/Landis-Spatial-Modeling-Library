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

using NUnit.Framework;

using Wisc.Flel.GeospatialModeling.Grids;

namespace Wisc.Flel.Test.GeospatialModeling.Grids
{
    [TestFixture]
    public class InputGridBool_Test
    {
        private Dimensions      dimensions;
        private bool[,]         data;
        private DataGrid<bool>  dataGrid;
        private InputGrid<bool> grid;

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
            data = new bool[7,5] { {false, false, false, false, false},
                                   {false, false, true,  false, false},
                                   {false, true,  true,  true,  false},
                                   {true,  true,  true,  true,  true },
                                   {false, true,  true,  true,  false},
                                   {false, false, true,  false, false},
                                   {false, false, false, false, false} };
            dimensions = new Dimensions(data.GetLength(0),
                                        data.GetLength(1));
            dataGrid = new DataGrid<bool>(data);
            grid = new InputGrid<bool>(dataGrid);
        }

        //---------------------------------------------------------------------

        [Test]
        public void GridCtor_Dims()
        {
            Assert.AreEqual(dimensions, grid.Dimensions);
        }

        //---------------------------------------------------------------------

        [Test]
        public void GridCtor_Rows()
        {
            Assert.AreEqual(dimensions.Rows, grid.Rows);
        }

        //---------------------------------------------------------------------

        [Test]
        public void GridCtor_Columns()
        {
            Assert.AreEqual(dimensions.Columns, grid.Columns);
        }

        //---------------------------------------------------------------------

        [Test]
        public void GridCtor_Count()
        {
            Assert.AreEqual(dimensions.Rows * dimensions.Columns,
                            grid.Count);
        }

        //---------------------------------------------------------------------

        [Test]
        public void GridCtor_DataType()
        {
            Assert.AreEqual(typeof(bool), grid.DataType);
        }

        //---------------------------------------------------------------------

        [Test]
        public void GridCtor_ReadValue()
        {
            for (int row = 1; row <= dimensions.Rows; ++row)
                for (int col = 1; col <= dimensions.Columns; ++col)
                    Assert.AreEqual(data[row-1, col-1], grid.ReadValue());
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.IO.EndOfStreamException))]
        public void GridCtor_ReadPastEnd()
        {
            InputGrid<bool> myGrid = new InputGrid<bool>(dataGrid);

            for (int row = 1; row <= dimensions.Rows; ++row)
                for (int col = 1; col <= dimensions.Columns; ++col)
                    Assert.AreEqual(data[row-1, col-1], myGrid.ReadValue());

            myGrid.ReadValue();
        }
    }
}
