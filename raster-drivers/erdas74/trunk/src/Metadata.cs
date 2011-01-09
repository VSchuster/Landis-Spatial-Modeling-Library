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

using Gov.Fgdc.Csdgm;
using System.Collections.Generic;
using Wisc.Flel.GeospatialModeling.RasterIO;

namespace Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74
{
	/// <summary>
	/// Methods for creating and using raster metadata.
	/// </summary>
	public static class Metadata
	{
		public const string HDWORD = "ERDAS HDWORD";
		public const string XSTART = "ERDAS XSTART";
		public const string YSTART = "ERDAS YSTART";
		public const string MAPTYP = "ERDAS MAPTYP";
		public const string NCLASS = "ERDAS NCLASS";
		public const string IAUTYP = "ERDAS IAUTYP";
		public const string ACRE   = "ERDAS ACRE";
		public const string XMAP   = "ERDAS XMAP";
		public const string YMAP   = "ERDAS YMAP";
		public const string XCELL  = "ERDAS XCELL";
		public const string YCELL  = "ERDAS YCELL";

		//---------------------------------------------------------------------

		private struct ProjectionInfo
		{
			public string      StandardName;
			public Projections Erdas74MapTyp;
		}

		private static List<ProjectionInfo> projections;

		//---------------------------------------------------------------------

		static Metadata()
		{
			projections = new List<ProjectionInfo>(20);
			//	Note: UTM and StatePlane are names of the grid coordinate
			//	systems (Grid Coordinate System Name, section 4.1.2.2.1)
			AddProjection(MapProjectionName.AlbersConicalEqualArea,
  			                    Projections.AlbersConicalEqualArea);
			AddProjection(MapProjectionName.LambertConformalConic,
			                    Projections.LambertConformalConic);
			AddProjection(MapProjectionName.Mercator,
			                    Projections.Mercator);
			AddProjection(MapProjectionName.PolarStereographic,
			                    Projections.PolarStereographic);
			AddProjection(MapProjectionName.Polyconic,
			                    Projections.Polyconic);
			AddProjection(MapProjectionName.EquidistantConic,
			                    Projections.EquidistantConic);
			AddProjection(MapProjectionName.TransverseMercator,
			                    Projections.TransverseMercator);
			AddProjection(MapProjectionName.Stereographic,
			                    Projections.Stereographic);
			AddProjection(MapProjectionName.LambertAzimuthalEqualArea,
			                    Projections.LambertAzimuthalEqualArea);
			AddProjection(MapProjectionName.AzimuthalEquidistant,
			                    Projections.AzimuthalEquidistant);
			AddProjection(MapProjectionName.Gnomonic,
			                    Projections.Gnomonic);
			AddProjection(MapProjectionName.Orthographic,
			                    Projections.Orthographic);
			AddProjection(MapProjectionName.GeneralVerticalNearSidedPerspective,
			                    Projections.GeneralVerticalNearSidePerspective);
			AddProjection(MapProjectionName.Sinusoidal,
			                    Projections.Sinusoidal);
			AddProjection(MapProjectionName.Equirectangular,
			                    Projections.Equirectangular);
			AddProjection(MapProjectionName.MillerCylindrical,
			                    Projections.MillerCylindrical);
			AddProjection(MapProjectionName.VanDerGrinten,
			                    Projections.VanDerGrinten);
			AddProjection(MapProjectionName.ObliqueMercator,
			                    Projections.ObliqueMercator);
		}

		//---------------------------------------------------------------------

		private static void AddProjection(string      standardName,
		                                  Projections erdas74MapTyp)
		{
			ProjectionInfo projInfo = new ProjectionInfo();
			projInfo.StandardName = standardName;
			projInfo.Erdas74MapTyp = erdas74MapTyp;
			projections.Add(projInfo);
		}

		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a metadata collection from an image header.
		/// </summary>
		public static IMetadata Create(ImageHeader header)
		{
			RasterIO.Metadata metadata = new RasterIO.Metadata();
			metadata[HDWORD] = "HEAD74";
			metadata[XSTART] = header.XStart;
			metadata[YSTART] = header.YStart;
			metadata[MAPTYP] = header.MapTyp;
			metadata[NCLASS] = header.NClass;
			metadata[IAUTYP] = header.IAUTyp;
			metadata[ACRE]   = header.Acre;
			metadata[XMAP]   = header.XMap;
			metadata[YMAP]   = header.YMap;
			metadata[XCELL]  = header.XCell;
			metadata[YCELL]  = header.YCell;

			//  Add values into collection using standard metadata names.
			foreach (ProjectionInfo projInfo in projections) {
				if ((short) projInfo.Erdas74MapTyp == header.MapTyp) {
					metadata[MapProjectionName.Name] = projInfo.StandardName;
					break;
				}
			}

			metadata[WestBoundingCoordinate.Name] = header.XMap;
			metadata[NorthBoundingCoordinate.Name] = header.YMap;

			metadata[AbscissaResolution.Name] = header.XCell;
			metadata[OrdinateResolution.Name] = header.YCell;
			if (header.MapTyp == (short) Projections.StatePlane) {
				metadata[PlanarDistanceUnits.Name] = PlanarDistanceUnits.SurveyFeet;
				//	Or should this be InternationalFeet?
			}
			else if (header.MapTyp >= 1 && header.MapTyp <= 20) {
				//	Documentation says that valid projection numbers are 1..20
				metadata[PlanarDistanceUnits.Name] = PlanarDistanceUnits.Meters;
			}
			//	According to ERDAS Ver. 7.X File Format documentation, the
			//	units of XCELL and YCELL are degrees when MapTyp is Lat/Lon.
			//	Not sure which MapTyp codes are Lat/Lon based.

			return metadata;
		}

		//---------------------------------------------------------------------

		/// <summary>
		/// Sets certain fields in an image header with information from a
		/// metadata collection.
		/// </summary>
		public static void SetFields(ImageHeader header,
		                             IMetadata   metadata)
		{
			if (metadata == null)
				return;

			string hdWord = null;
			bool hasErdasMetadata = metadata.TryGetValue(HDWORD, ref hdWord) &&
									(hdWord == "HEAD74");

			int xStart = 0;
			if (hasErdasMetadata && metadata.TryGetValue(XSTART, ref xStart))
				header.XStart = xStart;

			int yStart = 0;
			if (hasErdasMetadata && metadata.TryGetValue(YSTART, ref yStart))
				header.YStart = yStart;

			short mapTyp = 0;
			if (hasErdasMetadata && metadata.TryGetValue(MAPTYP, ref mapTyp))
				header.MapTyp = mapTyp;

			short nClass = 0;
			if (hasErdasMetadata && metadata.TryGetValue(NCLASS, ref nClass))
				header.NClass = nClass;

			short iAUTyp = 0;
			if (hasErdasMetadata && metadata.TryGetValue(IAUTYP, ref iAUTyp))
				header.IAUTyp = iAUTyp;

			float acre = 0;
			if (hasErdasMetadata && metadata.TryGetValue(ACRE, ref acre))
				header.Acre = acre;

			float xMap = 0;
			if (hasErdasMetadata && metadata.TryGetValue(XMAP, ref xMap))
				header.XMap = xMap;

			float yMap = 0;
			if (hasErdasMetadata && metadata.TryGetValue(YMAP, ref yMap))
				header.YMap = yMap;

			float xCell = 0;
			if (hasErdasMetadata && metadata.TryGetValue(XCELL, ref xCell))
				header.XCell = xCell;

			float yCell = 0;
			if (hasErdasMetadata && metadata.TryGetValue(YCELL, ref yCell))
				header.YCell = yCell;
		}
	}
}
