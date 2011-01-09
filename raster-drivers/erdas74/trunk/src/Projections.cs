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

namespace Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74
{
	/// <summary>
	/// Types of projections.
	/// </summary>
	/// <remarks>
	/// Reference: Document "Using ERDAS Ver. 7.X Files Within IMAGINE",
	/// section "PRO Files - Projection Parameters".
	/// </remarks>
	public enum Projections
	{
		UTM = 1,
		StatePlane = 2,
		AlbersConicalEqualArea = 3,
		LambertConformalConic = 4,
		Mercator = 5,
		PolarStereographic = 6,
		Polyconic = 7,
		EquidistantConic = 8,
		TransverseMercator = 9,
		Stereographic = 10,
		LambertAzimuthalEqualArea = 11,
		AzimuthalEquidistant = 12,
		Gnomonic = 13,
		Orthographic = 14,
		GeneralVerticalNearSidePerspective = 15,
		Sinusoidal = 16,
		Equirectangular = 17,
		MillerCylindrical = 18,
		VanDerGrinten = 19,
		ObliqueMercator = 20
	}
}
