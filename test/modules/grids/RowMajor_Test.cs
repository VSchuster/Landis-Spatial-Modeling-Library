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

using System;
using NUnit.Framework;

using Wisc.Flel.GeospatialModeling.Grids;

namespace Wisc.Flel.Test.GeospatialModeling.Grids
{
    [TestFixture]
    public class RowMajor_Test
    {
        //---------------------------------------------------------------------

        private Location Loc(int row,
                             int column)
        {
            return new Location(row, column);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Test01_NextWith1Col()
        {
            Assert.AreEqual(Loc(2,1), RowMajor.Next(Loc(1,1), 1));
            Assert.AreEqual(Loc(3,1), RowMajor.Next(Loc(2,1), 1));

            Assert.AreEqual(Loc(790,1), RowMajor.Next(Loc(789,1), 1));
        }

        //---------------------------------------------------------------------

        [Test]
        public void Test02_NextWith89Cols()
        {
            Assert.AreEqual(Loc(1,2), RowMajor.Next(Loc(1,1), 89));
            Assert.AreEqual(Loc(1,3), RowMajor.Next(Loc(1,2), 89));
            Assert.AreEqual(Loc(1,89), RowMajor.Next(Loc(1,88), 89));
            Assert.AreEqual(Loc(2,1), RowMajor.Next(Loc(1,89), 89));

            Assert.AreEqual(Loc(45,89), RowMajor.Next(Loc(45,88), 89));
            Assert.AreEqual(Loc(46,1), RowMajor.Next(Loc(45,89), 89));
        }
    }
}
