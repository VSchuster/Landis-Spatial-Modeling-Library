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

namespace Wisc.Flel.Test.GeospatialModeling.Landscapes
{
    public static class Data
    {
        private static NUnitInfo myNUnitInfo = new NUnitInfo();

        //---------------------------------------------------------------------

        public static readonly string Directory = myNUnitInfo.GetDataDir();

        //---------------------------------------------------------------------

        public static List<Location> ReadLocations(string path)
        {
            List<Location> sites = new List<Location>();
            FileLineReader reader = new FileLineReader(path);
            string line;
            while ((line = reader.ReadLine()) != null) {
                string[] rowAndCol = line.Split(null);
                Assert.AreEqual(2, rowAndCol.Length);
                int row = int.Parse(rowAndCol[0]);
                int col = int.Parse(rowAndCol[1]);
                Location loc = new Location(row, col);
                sites.Add(loc);
            }
            reader.Close();
            return sites;
        }
    }
}
