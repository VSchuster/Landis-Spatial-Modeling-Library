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
//   Jimm Domingo, UW-Madison, Forest Landscape Ecology Lab

using Wisc.Flel.GeospatialModeling.RasterIO;
using System;

namespace Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74
{
    /// <summary>
    /// A method to read a pixel value of type T for a band from a raster file.
    /// </summary>
    public delegate T ReadPixelMethod<T>();

       //-------------------------------------------------------------------------

    /// <summary>
    /// A band in an input raster.  A band has a buffer to hold a single row
    /// of pixel-band values whose type is TRasterBand.  These pixel-band
    /// values are assigned one at a time to an IPixelBand object associated
    /// with the input raster band.  This object accepts values of type
    /// TPixelBand.
    /// </summary>
    public class InputBand<TRasterBand, TPixelBand>
        : IInputBand
        where TRasterBand : struct
        where TPixelBand : struct
    {
        private TRasterBand[] buffer;
        private int offset;
        private ReadPixelMethod<TRasterBand> readPixel;
        private IPixelBandValue<TPixelBand> pixelBand;
        private ConvertPixelMethod<TRasterBand, TPixelBand> convertPixel;

        //---------------------------------------------------------------------

        public InputBand(int                                         bufferSize,
                         ReadPixelMethod<TRasterBand>                readMethod,
                         IPixelBand                                  band,
                         ConvertPixelMethod<TRasterBand, TPixelBand> convertMethod)
        {
            this.buffer = new TRasterBand[bufferSize];
            this.offset = bufferSize;
            this.readPixel = readMethod;
            this.pixelBand = band as IPixelBandValue<TPixelBand>;
            if (this.pixelBand == null)
                throw new ArgumentException(string.Format("band parameter is not a IPixelBandValue<{0}> object",
                                                          typeof(TPixelBand).Name));
            this.convertPixel = convertMethod;
        }

        //---------------------------------------------------------------------

        public void ReadData()
        {
            for (int i = 0; i < buffer.Length; ++i) {
                buffer[i] = readPixel();
            }
            offset = 0;
        }

        //---------------------------------------------------------------------

        public void AssignNextPixel()
        {
            if (offset < buffer.Length) {
                pixelBand.Value = convertPixel(buffer[offset]);
                offset++;
            }
            else
                throw new InvalidOperationException("Trying to read past end of buffer");
        }
    }
}
