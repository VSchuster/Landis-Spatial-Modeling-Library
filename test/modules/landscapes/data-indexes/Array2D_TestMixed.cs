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
using DataIndexEnumerator = Wisc.Flel.GeospatialModeling.Landscapes.DataIndexes.Enumerator;

namespace Wisc.Flel.Test.GeospatialModeling.Landscapes.DataIndexes
{
    [TestFixture]
    public class Array2D_TestMixed
    {
        private Grids.IInputGrid<bool> grid;
        private Array2D dataIndexes;
        private List<Location> expectedLocations;

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
            string path = Path.Combine(Data.Directory, "mixed.txt");
            bool[,] array = Bool.Read2DimArray(path);
            grid = new Grids.InputGrid<bool>(new Grids.DataGrid<bool>(array));
            dataIndexes = new Array2D(grid);

            path = Path.Combine(Data.Directory,
                                "true-locs-in-mixed.txt");
            expectedLocations = Data.ReadLocations(path);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Enumerator()
        {
            Assert.AreEqual(expectedLocations.Count, dataIndexes.ActiveLocationCount);

            int index = 0;
            DataIndexEnumerator locationEtor = dataIndexes.GetActiveLocationEnumerator();
            while (locationEtor.MoveNext()) {
                index++;
                Assert.IsTrue(index <= dataIndexes.ActiveLocationCount);
                Assert.AreEqual(expectedLocations[index-1], locationEtor.CurrentLocation);
                Assert.AreEqual((uint) index, locationEtor.CurrentDataIndex);
            }
            Assert.AreEqual(dataIndexes.ActiveLocationCount, index);
        }

        //---------------------------------------------------------------------

        [Test]
        public void EnumeratorReset()
        {
            Assert.AreEqual(expectedLocations.Count, dataIndexes.ActiveLocationCount);

            DataIndexEnumerator locationEtor = dataIndexes.GetActiveLocationEnumerator();

            for (int passes = 1; passes <= 3; passes++) {
                if (passes > 1)
                    locationEtor.Reset();
                int index = 0;
                while (locationEtor.MoveNext()) {
                    index++;
                    Assert.IsTrue(index <= dataIndexes.ActiveLocationCount);
                    Assert.AreEqual(expectedLocations[index-1], locationEtor.CurrentLocation);
                    Assert.AreEqual((uint) index, locationEtor.CurrentDataIndex);
                }
                Assert.AreEqual(dataIndexes.ActiveLocationCount, index);
            }
        }

        //---------------------------------------------------------------------

        [Test]
        public void EnumeratorForAll()
        {
            DataIndexEnumerator etor = dataIndexes.GetEnumerator();

            for (int passes = 1; passes <= 3; passes++) {
                if (passes > 1)
                    etor.Reset();

                long count = 0;
                Location expectedLocation = new Location(1, 1);

                int indexOfNextActiveLocation = 0;
                Location nextActiveLocation = new Location();
                if (expectedLocations.Count > 0)
                    nextActiveLocation = expectedLocations[0];

                while (etor.MoveNext()) {
                    count++;
                    Assert.IsTrue(count <= grid.Count);

                    Assert.AreEqual(expectedLocation, etor.CurrentLocation);

                    uint expectedDataIndex;
                    if (nextActiveLocation && etor.CurrentLocation == nextActiveLocation) {
                        expectedDataIndex = (uint) (indexOfNextActiveLocation + 1);
                        indexOfNextActiveLocation++;
                        if (indexOfNextActiveLocation < expectedLocations.Count)
                            nextActiveLocation = expectedLocations[indexOfNextActiveLocation];
                        else
                            nextActiveLocation = new Location();
                    }
                    else
                        expectedDataIndex = InactiveSite.DataIndex;
                    Assert.AreEqual(expectedDataIndex, etor.CurrentDataIndex);

                    expectedLocation = Grids.RowMajor.Next(expectedLocation,
                                                           grid.Columns);
                }
                Assert.AreEqual(grid.Count, count);
            }
        }

        //---------------------------------------------------------------------

        [Test]
        public void IndexerLocation()
        {
            int index = 0;
            Location nextActiveLocation = new Location();
            if (expectedLocations.Count > 0)
                nextActiveLocation = expectedLocations[0];
            for (int row = 1; row <= grid.Rows; ++row)
                for (int col = 1; col <= grid.Columns; ++col) {
                    Location location = new Location(row, col);
                    if (nextActiveLocation && nextActiveLocation == location) {
                        //  Check data indexes
                        Assert.AreEqual(index+1, dataIndexes[location]);
                        index++;
                        if (index < expectedLocations.Count)
                            nextActiveLocation = expectedLocations[index];
                        else
                            nextActiveLocation = new Location();
                    }
                    else
                        Assert.AreEqual(InactiveSite.DataIndex,
                                        dataIndexes[location]);
                }
        }
    }
}
