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
	/// Methods for working with band types.
	/// </summary>
	public static class BandType
	{
		public static string GetDescription(TypeCode bandType)
		{
			switch (bandType) {
				case TypeCode.Byte:
					return "8-bit unsigned integers";

				case TypeCode.SByte:
					return "8-bit signed integers";

				case TypeCode.Int16:
					return "16-bit signed integers";

				case TypeCode.UInt16:
					return "16-bit unsigned integers";

				case TypeCode.Int32:
					return "32-bit signed integers";

				case TypeCode.UInt32:
					return "32-bit unsigned integers";

				case TypeCode.Single:
					return "32-bit floating-point numbers";

				case TypeCode.Double:
					return "64-bit floating-point numbers";

				default:
					throw new ArgumentException();
			}
		}

		//---------------------------------------------------------------------

		public static string GetDescription(Type bandType)
		{
			return GetDescription(Type.GetTypeCode(bandType));
		}
	}
}
