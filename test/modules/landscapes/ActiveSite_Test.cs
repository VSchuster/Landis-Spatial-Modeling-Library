// Copyright 2006 University of Wisconsin
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
    public class ActiveSite_Test
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

        [Test]
        public void Properties()
        {
            ActiveSite site = landscape[3, 7];

            Assert.IsTrue(site);
            Assert.AreEqual(new Location(3, 7), site.Location);
            Assert.AreEqual(landscape, site.Landscape);
            Assert.IsTrue(site.DataIndex != InactiveSite.DataIndex);
        }

        //---------------------------------------------------------------------

        [Test]
        public void OperatorEquals()
        {
            ActiveSite site1 = landscape[3, 7];
            ActiveSite site2 = landscape[5, 5];
            ActiveSite site3 = landscape[3, 7];

//  CS1718 warning: operands of comparison are the same variable
#pragma warning disable 1718
            Assert.IsTrue(site1 == site1);
#pragma warning restore 1718

            Assert.IsTrue(site1 == site3);

            Assert.IsFalse(site1 == site2);
            Assert.IsFalse(site2 == site3);
        }

        //---------------------------------------------------------------------

        [Test]
        public void OperatorNotEqual()
        {
            ActiveSite site1 = landscape[3, 7];
            ActiveSite site2 = landscape[5, 5];
            ActiveSite site3 = landscape[3, 7];

            Assert.IsTrue(site1 != site2);
            Assert.IsTrue(site2 != site3);

//  CS1718 warning: operands of comparison are the same variable
#pragma warning disable 1718
            Assert.IsFalse(site1 != site1);
#pragma warning restore 1718

            Assert.IsFalse(site1 != site3);
        }

        //---------------------------------------------------------------------

        [Test]
        public void ConvertToBool()
        {
            Assert.IsTrue(landscape[3, 7]);
            Assert.IsTrue(landscape[5, 5]);

            Assert.IsFalse(new ActiveSite());
            Assert.IsFalse(landscape[1, 1]);
        }

        //---------------------------------------------------------------------

        [Test]
        public void ConvertToSite()
        {
            //  Implicit conversion with assignment
            ActiveSite activeSite = landscape[3, 7];
            Site site = activeSite;

            Assert.IsTrue(site);
            Assert.IsTrue(site.IsActive);
            Assert.AreEqual(activeSite.Landscape, site.Landscape);
            Assert.AreEqual(activeSite.Location,  site.Location);
            Assert.AreEqual(activeSite.DataIndex, site.DataIndex);

            site = new ActiveSite();

            Assert.IsFalse(site);
            Assert.IsFalse(site.IsActive);
            Assert.IsNull(site.Landscape);
            Assert.AreEqual(new Location(0, 0),  site.Location);
            Assert.AreEqual(InactiveSite.DataIndex, site.DataIndex);

            //  Implicit conversion with method call
            CheckSite(activeSite, activeSite.Location, activeSite.DataIndex);
        }

        //---------------------------------------------------------------------

        private void CheckSite(Site     site,
                               Location expectedLocation,
                               uint     expectedDataIndex)
        {
            Assert.AreEqual(landscape, site.Landscape);
            Assert.AreEqual(expectedLocation, site.Location);
            Assert.AreEqual(expectedDataIndex, site.DataIndex);
        }

        //---------------------------------------------------------------------

        [Test]
        public void ConvertFromSite()
        {
            Site site = landscape.GetSite(3, 7);
            ActiveSite activeSite = (ActiveSite) site;

            Assert.IsTrue(activeSite);
            Assert.AreEqual(site.Landscape, activeSite.Landscape);
            Assert.AreEqual(site.Location,  activeSite.Location);
            Assert.AreEqual(site.DataIndex, activeSite.DataIndex);
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.InvalidCastException))]
        public void ConvertFromSite_Inactive()
        {
            Site site = landscape.GetSite(1, 1);
            Assert.IsFalse(site.IsActive);

            ActiveSite activeSite = (ActiveSite) site;
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.InvalidCastException))]
        public void ConvertFromSite_DefaultCtor()
        {
            Site site = new Site();
            Assert.IsFalse(site.IsActive);

            ActiveSite activeSite = (ActiveSite) site;
        }
    }
}
