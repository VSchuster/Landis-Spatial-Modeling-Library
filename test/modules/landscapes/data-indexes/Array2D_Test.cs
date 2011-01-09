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

using Edu.Wisc.Forest.Flel.Util;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using Wisc.Flel.GeospatialModeling.Landscapes;
using Wisc.Flel.GeospatialModeling.Landscapes.DataIndexes;

using Grids = Wisc.Flel.GeospatialModeling.Grids;

namespace Wisc.Flel.Test.GeospatialModeling.Landscapes.DataIndexes
{
    [TestFixture]
    public class Array2D_Test
    {
        private Grids.IInputGrid<bool> MakeGrid(bool[,] array)
        {
            return new Grids.InputGrid<bool>(new Grids.DataGrid<bool>(array));
        }

        //---------------------------------------------------------------------

        [Test]
        public void Grid0x0()
        {
            bool[,] array = new bool[0,0];
            Grids.IInputGrid<bool> grid = MakeGrid(array);
            Array2D dataIndexes = new Array2D(grid);

            Assert.AreEqual(0, dataIndexes.ActiveLocationCount);
            Assert.AreEqual(0, dataIndexes.InactiveLocationCount);

            Enumerator activeEtor = dataIndexes.GetActiveLocationEnumerator();
            Assert.IsFalse(activeEtor.MoveNext());

            Enumerator etor = dataIndexes.GetEnumerator();
            Assert.IsFalse(etor.MoveNext());
        }

        //---------------------------------------------------------------------

        [Test]
        public void Grid1x0()
        {
            bool[,] array = new bool[1,0];
            Grids.IInputGrid<bool> grid = MakeGrid(array);
            Array2D dataIndexes = new Array2D(grid);

            Assert.AreEqual(0, dataIndexes.ActiveLocationCount);
            Assert.AreEqual(0, dataIndexes.InactiveLocationCount);

            Enumerator activeEtor = dataIndexes.GetActiveLocationEnumerator();
            Assert.IsFalse(activeEtor.MoveNext());

            Enumerator etor = dataIndexes.GetEnumerator();
            Assert.IsFalse(etor.MoveNext());
        }

        //---------------------------------------------------------------------

        [Test]
        public void Grid4x0()
        {
            bool[,] array = new bool[4,0];
            Grids.IInputGrid<bool> grid = MakeGrid(array);
            Array2D dataIndexes = new Array2D(grid);

            Assert.AreEqual(0, dataIndexes.ActiveLocationCount);
            Assert.AreEqual(0, dataIndexes.InactiveLocationCount);

            Enumerator activeEtor = dataIndexes.GetActiveLocationEnumerator();
            Assert.IsFalse(activeEtor.MoveNext());

            Enumerator etor = dataIndexes.GetEnumerator();
            Assert.IsFalse(etor.MoveNext());
        }

        //---------------------------------------------------------------------

        private void CheckEtorForAllActive(Enumerator etor,
                                           uint       activeLocationCount,
                                           int        columns)
        {
            uint index = 0;
            Location expectedLocation = new Location(1,1);
            while (etor.MoveNext()) {
                index++;
                Assert.IsTrue(index <= activeLocationCount);
                Assert.AreEqual(expectedLocation, etor.CurrentLocation);
                Assert.AreEqual((uint) index, etor.CurrentDataIndex);
                expectedLocation = Grids.RowMajor.Next(expectedLocation, columns);
            }
            Assert.AreEqual(activeLocationCount, index);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Grid1x1True()
        {
            bool[,] array = new bool[,] { {true} };
            Grids.IInputGrid<bool> grid = MakeGrid(array);
            Array2D dataIndexes = new Array2D(grid);

            Assert.AreEqual(grid.Count, dataIndexes.ActiveLocationCount);
            Assert.AreEqual(0, dataIndexes.InactiveLocationCount);

            CheckEtorForAllActive(dataIndexes.GetActiveLocationEnumerator(),
                                  dataIndexes.ActiveLocationCount,
                                  grid.Columns);

            CheckEtorForAllActive(dataIndexes.GetEnumerator(),
                                  dataIndexes.ActiveLocationCount,
                                  grid.Columns);
        }

        //---------------------------------------------------------------------

        private void CheckEtorForAllInactive(Enumerator etor,
                                             long       expectedCount,
                                             int        columns)
        {
            long count = 0;
            Location expectedLocation = new Location(1,1);
            while (etor.MoveNext()) {
                count++;
                Assert.IsTrue(count <= expectedCount);
                Assert.AreEqual(expectedLocation, etor.CurrentLocation);
                Assert.AreEqual(InactiveSite.DataIndex, etor.CurrentDataIndex);
                expectedLocation = Grids.RowMajor.Next(expectedLocation, columns);
            }
            Assert.AreEqual(expectedCount, count);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Grid1x1False()
        {
            bool[,] array = new bool[,] { {false} };
            Grids.IInputGrid<bool> grid = MakeGrid(array);
            Array2D dataIndexes = new Array2D(grid);

            Assert.AreEqual(0, dataIndexes.ActiveLocationCount);
            Assert.AreEqual(grid.Count, dataIndexes.InactiveLocationCount);

            CheckEtorForAllInactive(dataIndexes.GetActiveLocationEnumerator(),
                                    dataIndexes.ActiveLocationCount,
                                    grid.Columns);

            CheckEtorForAllInactive(dataIndexes.GetEnumerator(),
                                    dataIndexes.InactiveLocationCount,
                                    grid.Columns);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Grid5x3True()
        {
            bool[,] array = new bool[,] { {true, true, true},
                                          {true, true, true},
                                          {true, true, true},
                                          {true, true, true},
                                          {true, true, true} };
            Grids.IInputGrid<bool> grid = MakeGrid(array);
            Array2D dataIndexes = new Array2D(grid);

            Assert.AreEqual(grid.Count, dataIndexes.ActiveLocationCount);
            Assert.AreEqual(0, dataIndexes.InactiveLocationCount);

            CheckEtorForAllActive(dataIndexes.GetActiveLocationEnumerator(),
                                  dataIndexes.ActiveLocationCount,
                                  grid.Columns);

            CheckEtorForAllActive(dataIndexes.GetEnumerator(),
                                  dataIndexes.ActiveLocationCount,
                                  grid.Columns);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Grid5x3False()
        {
            bool[,] array = new bool[,] { {false, false, false},
                                          {false, false, false},
                                          {false, false, false},
                                          {false, false, false},
                                          {false, false, false} };
            Grids.IInputGrid<bool> grid = MakeGrid(array);
            Array2D dataIndexes = new Array2D(grid);

            Assert.AreEqual(0, dataIndexes.ActiveLocationCount);
            Assert.AreEqual(grid.Count, dataIndexes.InactiveLocationCount);

            CheckEtorForAllInactive(dataIndexes.GetActiveLocationEnumerator(),
                                    dataIndexes.ActiveLocationCount,
                                    grid.Columns);

            CheckEtorForAllInactive(dataIndexes.GetEnumerator(),
                                    dataIndexes.InactiveLocationCount,
                                    grid.Columns);
        }
    }
}
