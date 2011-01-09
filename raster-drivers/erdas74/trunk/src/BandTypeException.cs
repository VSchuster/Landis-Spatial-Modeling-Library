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

using System;

namespace Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74
{
	/// <summary>
	/// Represents an error with a data type of a pixel band.
	/// </summary>
	internal class BandTypeException
		: ApplicationException
	{
		internal BandTypeException(string          message,
		                           params object[] mesgArgs)
			: base(string.Format(message, mesgArgs))
		{
		}
	}
}
