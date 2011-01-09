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
	/// A band in an output raster.  A band has a buffer to hold a single row
	/// of pixel-band values.  Pixel-band values are put into the band's buffer
	/// one at a time.
	/// </summary>
	public interface IOutputBand
	{
		/// <summary>
		/// Appends a pixel value to the end of the buffer's contents.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">
		/// There is no more room in the buffer.  The caller is responsible for
		/// ensuring that the WriteData method is called when the buffer is
		/// full.
		/// </exception>
		void AppendPixel(IPixelBand pixelBand);
	}
}
