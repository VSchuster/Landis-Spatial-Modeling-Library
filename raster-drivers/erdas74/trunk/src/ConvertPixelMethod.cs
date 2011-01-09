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

namespace Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74
{
	/// <summary>
	/// A method to convert a pixel-band value from type TFrom to type TTo.
	/// </summary>
	public delegate TTo ConvertPixelMethod<TFrom, TTo>(TFrom pixel);
}
