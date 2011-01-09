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

using Edu.Wisc.Forest.Flel.Util;
using Wisc.Flel.GeospatialModeling.RasterIO;
using Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74;

namespace Wisc.Flel.Test.GeospatialModeling.RasterDrivers.Erdas74
{
    class FredLanPixel : IPixel
    {
        IPixelBand[] bands;

        public FredLanPixel()
        {
            bands = new IPixelBand[2];
            bands[0] = new PixelBandUShort();
            bands[1] = new PixelBandUShort();
        }
        
        public int BandCount {
            get { return 2; }
        }
        
        public IPixelBand this[int index]
        {
            get { return bands[index]; }
        }
    }
}
