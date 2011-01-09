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

using Wisc.Flel.GeospatialModeling.Grids;
using NUnit.Framework;

//  CS0649 warning is generated by the field "loc" not being assigned.
//  CS1718 warning is generated by the tests of == operator using the same
//  variable as both operands
#pragma warning disable 649, 1718

namespace Wisc.Flel.Test.GeospatialModeling.Grids
{
    [TestFixture]
    public class Location_Test
    {
        Location loc;
        Location loc_1234_987;

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
            loc_1234_987 = new Location(1234, 987);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DefaultCtorCheckRow()
        {
            Assert.AreEqual(0, loc.Row);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DefaultCtorCheckColumn()
        {
            Assert.AreEqual(0, loc.Column);
        }

        //---------------------------------------------------------------------

        [Test]
        public void CheckRow()
        {
            Assert.AreEqual(1234, loc_1234_987.Row);
        }

        //---------------------------------------------------------------------

        [Test]
        public void CheckColumn()
        {
            Assert.AreEqual(987, loc_1234_987.Column);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Ctor_ZeroZero()
        {
            Location loc_0_0 = new Location(0, 0);
            Assert.AreEqual(0, loc_0_0.Row);
            Assert.AreEqual(0, loc_0_0.Column);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Ctor_RowIs0()
        {
            const int column = 1234567;
            Location location = new Location(0, column);
            Assert.AreEqual(0, location.Row);
            Assert.AreEqual(column, location.Column);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Ctor_ColumnIs0()
        {
            const int row = 987654321;
            Location location = new Location(row, 0);
            Assert.AreEqual(row, location.Row);
            Assert.AreEqual(0, location.Column);
        }

        //---------------------------------------------------------------------

        [Test]
        public void EqualityWithSameLoc()
        {
            Assert.IsTrue(loc == loc);
            Assert.IsTrue(loc_1234_987 == loc_1234_987);
        }

        //---------------------------------------------------------------------

        [Test]
        public void EqualityWithDiffLocs()
        {
            Assert.IsFalse(loc == loc_1234_987);
        }

        //---------------------------------------------------------------------

        [Test]
        public void InequalityWithSameLoc()
        {
            Assert.IsFalse(loc != loc);
            Assert.IsFalse(loc_1234_987 != loc_1234_987);
        }

        //---------------------------------------------------------------------

        [Test]
        public void InequalityWithDiffLocs()
        {
            Assert.IsTrue(loc != loc_1234_987);
        }

        //---------------------------------------------------------------------

        [Test]
        public void EqualsWithNull()
        {
            Assert.IsFalse(loc.Equals(null));
        }

        //---------------------------------------------------------------------

        [Test]
        public void EqualsWithSameObj()
        {
            Assert.IsTrue(loc.Equals(loc));
            Assert.IsTrue(loc_1234_987.Equals(loc_1234_987));
        }

        //---------------------------------------------------------------------

        [Test]
        public void EqualsWithDiffObj()
        {
            Assert.IsFalse(loc.Equals(loc_1234_987));
        }

        //---------------------------------------------------------------------

        [Test]
        public void EqualsWithDiffObjSameValue()
        {
            Location loc_0_0 = new Location();
            Assert.IsTrue(loc.Equals(loc_0_0));
            Assert.IsTrue(loc_1234_987.Equals(new Location(1234, 987)));
        }

        //---------------------------------------------------------------------

        [Test]
        public void ConversionToBool()
        {
            Assert.IsFalse(loc);
            Assert.IsTrue(loc_1234_987);
            Assert.IsFalse(new Location(1, 0));
            Assert.IsFalse(new Location(0, 1));
        }

        //---------------------------------------------------------------------

        [Test]
        public void HashCode()
        {
            Assert.AreEqual((int)(0 ^ 0), loc.GetHashCode());
            Assert.AreEqual((int)(1234 ^ 987),
                            loc_1234_987.GetHashCode());
        }

        //---------------------------------------------------------------------

        [Test]
        public void ToStringMethod()
        {
            Assert.AreEqual("(0, 0)", loc.ToString());
            Assert.AreEqual("(1234, 987)", loc_1234_987.ToString());
        }
    }
}

#pragma warning restore 649, 1718
