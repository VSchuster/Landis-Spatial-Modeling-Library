// Copyright 2005 University of Wisconsin
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
//   Barry DeZonia, UW-Madison, Forest Landscape Ecology Lab
//   Jimm Domingo, UW-Madison, Forest Landscape Ecology Lab

using Wisc.Flel.GeospatialModeling.Grids;
using Wisc.Flel.GeospatialModeling.RasterIO;

namespace Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74
{
    public class Driver : IDriver
    {
        static string[] extensions = { "gis","lan" };
        
        //---------------------------------------------------------------------

        /// <summary>
        ///         
        /// </summary>
        public string[] Formats
        {
            get { return extensions; }
        }

        //---------------------------------------------------------------------

        /// <summary>
        ///         
        /// </summary>
        public IInputRaster<T> OpenRaster<T>(string path)
            where T : IPixel, new()
        {
            return new InputRaster<T>(path);
        }

        //---------------------------------------------------------------------

        /// <summary>
        ///         
        /// </summary>
        public IOutputRaster<T> CreateRaster<T>(string     path,
                                                Dimensions dimensions,
                                                IMetadata  metadata)
            where T : IPixel, new()
        {
            return new OutputRaster<T>(path, dimensions, metadata);
        }
    }
}
