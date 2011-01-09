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

using Edu.Wisc.Forest.Flel.Util;
using NUnit.Framework;
using Wisc.Flel.GeospatialModeling.Landscapes;

using Grids = Wisc.Flel.GeospatialModeling.Grids;

namespace Wisc.Flel.Test.GeospatialModeling.Landscapes
{
    [TestFixture]
    public class Site_Test
    {
        private bool[,]    activeSites;
        private ILandscape landscape;

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
                                        // 12345678
            string[] rows = new string[]{ "...XX...",   // 1
                                          "..XXX..X",   // 2
                                          ".XXXX.XX",   // 3
                                          "...XXXX.",   // 4
                                          "....XX..",   // 5
                                          "........" }; // 6
            activeSites = Bool.Make2DimArray(rows, "X");
            Grids.DataGrid<bool> grid = new Grids.DataGrid<bool>(activeSites);
            landscape = new Landscape(grid);
        }

        //---------------------------------------------------------------------

        private void CheckNeighbor(Site     neighbor,
                                   Location expectedLocation)
        {
            Assert.IsNotNull(neighbor);
            Assert.AreEqual(expectedLocation, neighbor.Location);
            Assert.AreEqual(activeSites[expectedLocation.Row - 1,
                                        expectedLocation.Column - 1],
                            neighbor.IsActive);
        }

        //---------------------------------------------------------------------

        [Test]
        public void ActiveSite_NeighborEast()
        {
            Site site = landscape[3, 7];
            Assert.IsTrue(site.IsActive);
            Site neighbor = site.GetNeighbor(new RelativeLocation(0, 1));
            CheckNeighbor(neighbor, new Location(3, 8));
        }

        //---------------------------------------------------------------------

        [Test]
        public void ActiveSite_NeighborWest()
        {
            Site site = landscape[3, 7];
            Assert.IsTrue(site.IsActive);
            Site neighbor = site.GetNeighbor(new RelativeLocation(0, -1));
            CheckNeighbor(neighbor, new Location(3, 6));
        }

        //---------------------------------------------------------------------

        [Test]
        public void ActiveSite_NeighborNorth()
        {
            Site site = landscape[3, 7];
            Assert.IsTrue(site.IsActive);
            Site neighbor = site.GetNeighbor(new RelativeLocation(-1, 0));
            CheckNeighbor(neighbor, new Location(2, 7));
        }

        //---------------------------------------------------------------------

        [Test]
        public void ActiveSite_NeighborSouth()
        {
            Site site = landscape[3, 7];
            Assert.IsTrue(site.IsActive);
            Site neighbor = site.GetNeighbor(new RelativeLocation(1, 0));
            CheckNeighbor(neighbor, new Location(4, 7));
        }

        //---------------------------------------------------------------------

        [Test]
        public void UpperLeft_8Neighbors()
        {
            Site site = landscape.GetSite(1, 1);
            Assert.IsFalse(site.GetNeighbor(new RelativeLocation(-1, -1)));
            Assert.IsFalse(site.GetNeighbor(new RelativeLocation(-1, 0)));
            Assert.IsFalse(site.GetNeighbor(new RelativeLocation(-1, 1)));
            Assert.IsFalse(site.GetNeighbor(new RelativeLocation(0, -1)));
            Assert.IsFalse(site.GetNeighbor(new RelativeLocation(1, -1)));

            Site neighbor;
            neighbor = site.GetNeighbor(new RelativeLocation(0, 1));
            CheckNeighbor(neighbor, new Location(1, 2));
            neighbor = site.GetNeighbor(new RelativeLocation(1, 0));
            CheckNeighbor(neighbor, new Location(2, 1));
            neighbor = site.GetNeighbor(new RelativeLocation(1, 1));
            CheckNeighbor(neighbor, new Location(2, 2));
        }
    }
}
