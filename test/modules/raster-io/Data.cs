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
using System.IO;

namespace Wisc.Flel.Test.GeospatialModeling.RasterIO
{
    public static class Data
    {
        private static NUnitInfo myNUnitInfo = new NUnitInfo();

        //---------------------------------------------------------------------

        public static readonly string Directory = myNUnitInfo.GetDataDir();
        public const string DirPlaceholder = "{data folder}";

        public static string MakeInputPath(string filename)
        {
            return Path.Combine(Directory, filename);
        }

        //---------------------------------------------------------------------

        public static readonly string OutputDir = myNUnitInfo.GetOutDir(true);

        public static string MakeOutputPath(string filename)
        {
            return Path.Combine(OutputDir, filename);
        }

        //---------------------------------------------------------------------

        static Data()
        {
            Output.WriteLine("{0} = \"{1}\"", DirPlaceholder, Directory);
        }

        //---------------------------------------------------------------------

        private static TextWriter writer = myNUnitInfo.GetTextWriter();

        public static TextWriter Output
        {
            get {
                return writer;
            }
        }
    }
}
