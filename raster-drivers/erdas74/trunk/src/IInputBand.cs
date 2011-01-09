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

namespace Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74
{
	/// <summary>
	/// A band in an input raster.  A band has a buffer to hold a single row
	/// of pixel-band values.  These pixel-band values are assigned one at a
	/// time to an IPixelBand object associated with the input raster band.
	/// </summary>
    public interface IInputBand
    {
    	/// <summary>
    	/// Reads pixel data from the associated raster data file into the
    	/// band's buffer.
    	/// </summary>
    	/// <exception cref="System.IO.EndOfStreamException">
    	/// The raster file does not contain enough data to fill the buffer.
    	/// </exception>
    	/// <exception cref="System.IO.IOException">
    	/// An error occurred reading data from the raster file.
    	/// </exception>
    	void ReadData();

    	//---------------------------------------------------------------------

        /// <summary>
        /// Assigns the next pixel value from the buffer to the associated
        /// IPixelBand object.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// There are no more values in the buffer to assign.  The caller is
        /// responsible for ensuring this method is called the appropriate
        /// number of times before calling the ReadData method to refill the
        /// buffer.
        /// </exception>
        void AssignNextPixel();
    }
}
