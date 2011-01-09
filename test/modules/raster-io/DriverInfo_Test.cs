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
using System;
using System.Collections.Generic;
using System.IO;
using Wisc.Flel.GeospatialModeling.RasterIO;

namespace Wisc.Flel.Test.GeospatialModeling.RasterIO
{
    [TestFixture]
    public class DriverInfo_Test
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FormatsNull()
        {
            DriverInfo info = new DriverInfo("foo", "Foo.Driver,Foo.dll", null);
        }

        //---------------------------------------------------------------------

        [Test]
        public void Indexer()
        {
            Dictionary<string, FileAccess> formats = new Dictionary<string, FileAccess>();
            formats[".gis"] = FileAccess.ReadWrite;
            formats[".lan"] = FileAccess.ReadWrite;
            DriverInfo info = new DriverInfo("foo", "Foo.Driver,Foo.dll", formats);

            Assert.AreEqual(FileAccess.ReadWrite, info[".gis"]);
            Assert.AreEqual(FileAccess.ReadWrite, info[".lan"]);
            Assert.AreEqual((FileAccess) 0, info[".bar"]);
        }
    }
}
