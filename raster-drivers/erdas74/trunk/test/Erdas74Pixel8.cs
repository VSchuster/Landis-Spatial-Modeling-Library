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

using Wisc.Flel.GeospatialModeling.RasterIO;

namespace Wisc.Flel.Test.GeospatialModeling.RasterDrivers.Erdas74
{
    class Erdas74Pixel8 : IPixel
    {
        PixelBandByte[] bands;
        
        public Erdas74Pixel8()
        {
            bands = new PixelBandByte[1];
            bands[0] = new PixelBandByte();
        }
        
        public Erdas74Pixel8(int bandCount)
        {
            // if (bandCount < 1) throw new ArgumentException();
            bands = new PixelBandByte[bandCount];
            for (int i = 0; i < bands.Length; i++)
                bands[i] = new PixelBandByte();
        }
        
        public int BandCount
        {
            get {
                if (bands == null)
                    return 0;
                return bands.Length;
            }
        }

        public IPixelBand this[int index]
        {
            get {
                if (bands == null)
                    return null;
                return bands[index];
            }
        }
    }
}
