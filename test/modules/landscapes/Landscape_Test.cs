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
using Wisc.Flel.GeospatialModeling.Grids;
using Wisc.Flel.GeospatialModeling.Landscapes;

using Location = Wisc.Flel.GeospatialModeling.Landscapes.Location;

namespace Wisc.Flel.Test.GeospatialModeling.Landscapes
{
    [TestFixture]
    public class Landscape_Test
    {
        private DataGrid<bool> grid;
        private Landscape landscape;
        private List<Location> activeSites;

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
            string path = Path.Combine(Data.Directory, "mixed.txt");
            bool[,] array = Bool.Read2DimArray(path);
            grid = new DataGrid<bool>(array);
            landscape = new Landscape(grid);

            path = Path.Combine(Data.Directory,
                                "true-locs-in-mixed.txt");
            activeSites = Data.ReadLocations(path);
        }

        //---------------------------------------------------------------------

        [Test]
        public void ActiveSiteEnumerator()
        {
            Assert.AreEqual(activeSites.Count, landscape.ActiveSiteCount);

            int index = 0;
            foreach (ActiveSite site in landscape) {
                index++;
                Assert.IsTrue(index <= landscape.ActiveSiteCount);
                Assert.AreEqual(index, site.DataIndex);
                Assert.AreEqual(activeSites[index-1], site.Location);
                Assert.AreEqual(landscape, site.Landscape);
            }
            Assert.AreEqual(landscape.ActiveSiteCount, index);
        }

        //---------------------------------------------------------------------

        [Test]
        public void ActiveSiteEnumerator_Reset()
        {
            Assert.AreEqual(activeSites.Count, landscape.ActiveSiteCount);

            ActiveSiteEnumerator activeSiteEtor = landscape.GetActiveSiteEnumerator();

            for (int passes = 1; passes <= 3; passes++) {
                if (passes > 1)
                    activeSiteEtor.Reset();
                int index = 0;
                while (activeSiteEtor.MoveNext()) {
                    ActiveSite site = activeSiteEtor.Current;
                    index++;
                    Assert.IsTrue(index <= landscape.ActiveSiteCount);
                    Assert.AreEqual(index, site.DataIndex);
                    Assert.AreEqual(activeSites[index-1], site.Location);
                    Assert.AreEqual(landscape, site.Landscape);
                }
                Assert.AreEqual(landscape.ActiveSiteCount, index);
            }
        }

        //---------------------------------------------------------------------

        [Test]
        public void ActiveSiteIndexer_Location()
        {
            int index = 0;
            Location? nextActiveSite;
            if (index < activeSites.Count)
                nextActiveSite = activeSites[index];
            else
                nextActiveSite = null;
            for (int row = 1; row <= grid.Rows; ++row)
                for (int col = 1; col <= grid.Columns; ++col) {
                    Location location = new Location(row, col);
                    ActiveSite site = landscape[location];
                    if (nextActiveSite != null && nextActiveSite == location) {
                        Assert.AreEqual(location, site.Location);
                        Assert.AreEqual(index+1, site.DataIndex);
                        Assert.AreEqual(landscape, site.Landscape);
                        index++;
                        if (index < activeSites.Count)
                            nextActiveSite = activeSites[index];
                        else
                            nextActiveSite = null;
                    }
                    else
                        Assert.IsFalse(site);
                }
        }

        //---------------------------------------------------------------------

        [Test]
        public void ActiveSiteIndexer_RowColumn()
        {
            int index = 0;
            Location? nextActiveSite;
            if (index < activeSites.Count)
                nextActiveSite = activeSites[index];
            else
                nextActiveSite = null;
            for (int row = 1; row <= grid.Rows; ++row)
                for (int col = 1; col <= grid.Columns; ++col) {
                    Location location = new Location(row, col);
                    ActiveSite site = landscape[row, col];
                    if (nextActiveSite != null && nextActiveSite == location) {
                        Assert.AreEqual(location, site.Location);
                        Assert.AreEqual(index+1, site.DataIndex);
                        Assert.AreEqual(landscape, site.Landscape);
                        index++;
                        if (index < activeSites.Count)
                            nextActiveSite = activeSites[index];
                        else
                            nextActiveSite = null;
                    }
                    else
                        Assert.IsFalse(site);
                }
        }

        //---------------------------------------------------------------------

        [Test]
        public void GetSite_Location()
        {
            int index = 0;
            Location? nextActiveSite;
            if (index < activeSites.Count)
                nextActiveSite = activeSites[index];
            else
                nextActiveSite = null;
            for (int row = 1; row <= grid.Rows; ++row)
                for (int col = 1; col <= grid.Columns; ++col) {
                    Location location = new Location(row, col);
                    Site site = landscape.GetSite(location);
                    if (nextActiveSite != null && nextActiveSite == location) {
                        Assert.AreEqual(location, site.Location);
                        Assert.AreEqual(index+1, site.DataIndex);
                        Assert.AreEqual(landscape, site.Landscape);
                        Assert.AreEqual(true, site.IsActive);
                        index++;
                        if (index < activeSites.Count)
                            nextActiveSite = activeSites[index];
                        else
                            nextActiveSite = null;
                    }
                    else {
                        Assert.AreEqual(location, site.Location);
                        Assert.AreEqual(landscape.InactiveSiteDataIndex, site.DataIndex);
                        Assert.AreEqual(landscape, site.Landscape);
                        Assert.AreEqual(false, site.IsActive);
                    }
                }
        }

        //---------------------------------------------------------------------

        [Test]
        public void SiteEnumerator()
        {
            int index = 0;
            Location? nextActiveSite;
            if (index < activeSites.Count)
                nextActiveSite = activeSites[index];
            else
                nextActiveSite = null;

            Location expectedLocation = new Location(1, 0);
                // so next in row-major order is (1,1)

            int siteCount = 0;
            foreach (Site site in landscape.AllSites) {
                siteCount++;

                Assert.AreEqual(landscape, site.Landscape);

                expectedLocation = RowMajor.Next(expectedLocation, grid.Columns);
                Assert.AreEqual(expectedLocation, site.Location);

                if (nextActiveSite != null && nextActiveSite == expectedLocation) {
                    Assert.AreEqual(index+1, site.DataIndex);
                    Assert.AreEqual(true, site.IsActive);
                    index++;
                    if (index < activeSites.Count)
                        nextActiveSite = activeSites[index];
                    else
                        nextActiveSite = null;
                }
                else {
                    //  Inactive site
                    Assert.AreEqual(landscape.InactiveSiteDataIndex, site.DataIndex);
                    Assert.AreEqual(false, site.IsActive);
                }
            }

            Assert.AreEqual(grid.Count, siteCount);
        }

        //---------------------------------------------------------------------

        [Test]
        public void SiteEnumerator_Reset()
        {
            SiteEnumerator allSites = landscape.GetSiteEnumerator();

            for (int passes = 1; passes <= 3; passes++) {
                if (passes > 1)
                    allSites.Reset();
                int index = 0;
                Location? nextActiveSite;
                if (index < activeSites.Count)
                    nextActiveSite = activeSites[index];
                else
                    nextActiveSite = null;

                Location expectedLocation = new Location(1, 0);
                    // so next in row-major order is (1,1)

                int siteCount = 0;
                while (allSites.MoveNext()) {
                    Site site = allSites.Current;
                    siteCount++;

                    Assert.AreEqual(landscape, site.Landscape);

                    expectedLocation = RowMajor.Next(expectedLocation, grid.Columns);
                    Assert.AreEqual(expectedLocation, site.Location);

                    if (nextActiveSite != null && nextActiveSite == expectedLocation) {
                        Assert.AreEqual(index+1, site.DataIndex);
                        Assert.AreEqual(true, site.IsActive);
                        index++;
                        if (index < activeSites.Count)
                            nextActiveSite = activeSites[index];
                        else
                            nextActiveSite = null;
                    }
                    else {
                        //  Inactive site
                        Assert.AreEqual(landscape.InactiveSiteDataIndex, site.DataIndex);
                        Assert.AreEqual(false, site.IsActive);
                    }
                }

                Assert.AreEqual(grid.Count, siteCount);
            }
        }

        //---------------------------------------------------------------------

        private void CheckGetSite_RowColumn(ILandscape landscape)
        {
            int index = 0;
            Location? nextActiveSite;
            if (index < activeSites.Count)
                nextActiveSite = activeSites[index];
            else
                nextActiveSite = null;
            for (int row = 1; row <= grid.Rows; ++row)
                for (int col = 1; col <= grid.Columns; ++col) {
                    Location location = new Location(row, col);
                    Site site = landscape.GetSite(row, col);
                    if (nextActiveSite != null && nextActiveSite == location) {
                        Assert.AreEqual(location, site.Location);
                        Assert.AreEqual(index+1, site.DataIndex);
                        Assert.AreEqual(landscape, site.Landscape);
                        Assert.AreEqual(true, site.IsActive);
                        index++;
                        if (index < activeSites.Count)
                            nextActiveSite = activeSites[index];
                        else
                            nextActiveSite = null;
                    }
                    else {
                        Assert.AreEqual(location, site.Location);
                        Assert.AreEqual(landscape.InactiveSiteDataIndex, site.DataIndex);
                        Assert.AreEqual(landscape, site.Landscape);
                        Assert.AreEqual(false, site.IsActive);
                    }
                }
        }

        //---------------------------------------------------------------------

        [Test]
        public void GetSite_RowColumn()
        {
            CheckGetSite_RowColumn(landscape);
        }

        //---------------------------------------------------------------------

        [Test]
        public void InputGridCtor_GetSite_RowColumn()
        {
            InputGrid<bool> inputGrid = new InputGrid<bool>(grid);
            ILandscape myLandscape = new Landscape(inputGrid);
            CheckGetSite_RowColumn(myLandscape);
        }

        //---------------------------------------------------------------------

        [Test]
        public void GetSite_0_0()
        {
            Assert.IsFalse(landscape.GetSite(0, 0));
            Assert.IsFalse(landscape.GetSite(new Location(0, 0)));
        }

        //---------------------------------------------------------------------

        private ILandscape MakeHomogeneousLandscape(string activeSiteChars)
        {
            string[] rows = new string[]{ "XXXXXXXX",
                                          "XXXXXXXX",
                                          "XXXXXXXX",
                                          "XXXXXXXX",
                                          "XXXXXXXX" };
            bool[,] array = Bool.Make2DimArray(rows, activeSiteChars);
            DataGrid<bool> grid = new DataGrid<bool>(array);
            return new Landscape(grid);
        }

        //---------------------------------------------------------------------

        [Test]
        public void AllActiveSites()
        {
            ILandscape landscape = MakeHomogeneousLandscape("X");
            Assert.AreEqual(landscape.SiteCount, landscape.ActiveSiteCount);
            Assert.AreEqual(0, landscape.InactiveSiteCount);

            foreach (Site site in landscape.AllSites) {
                Assert.IsTrue(site.IsActive);
            }

            int index = 0;
            foreach (ActiveSite site in landscape.ActiveSites) {
                index++;
                Assert.AreEqual(index, site.DataIndex);
            }
        }

        //---------------------------------------------------------------------

        [Test]
        public void AllInactiveSites()
        {
            ILandscape landscape = MakeHomogeneousLandscape("");
            Assert.AreEqual(landscape.SiteCount, landscape.InactiveSiteCount);
            Assert.AreEqual(0, landscape.ActiveSiteCount);

            foreach (Site site in landscape.AllSites) {
                Assert.IsFalse(site.IsActive);
            }

            foreach (ActiveSite site in landscape.ActiveSites) {
                Assert.Fail("Expected no active sites");
            }
        }

        //---------------------------------------------------------------------

        [Test]
        public void ZeroByZero()
        {
            bool[,] array = new bool[0,0];
            DataGrid<bool> grid = new DataGrid<bool>(array);
            ILandscape landscape = new Landscape(grid);

            Assert.AreEqual(0, landscape.SiteCount);
            Assert.AreEqual(0, landscape.InactiveSiteCount);
            Assert.AreEqual(0, landscape.ActiveSiteCount);

            foreach (Site site in landscape.AllSites) {
                Assert.Fail("Expected no sites");
            }

            foreach (ActiveSite site in landscape.ActiveSites) {
                Assert.Fail("Expected no active sites");
            }
        }
    }
}
