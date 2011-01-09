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

namespace Wisc.Flel.Test.GeospatialModeling.RasterDrivers.Erdas74
{
	public class MultiBandPixel
		: Pixel, IPixel
	{
		private PixelBandByte   band1;
		private PixelBandShort  band2;
		private PixelBandUShort band3;
		private PixelBandInt    band4;
		private PixelBandUInt   band5;
		private PixelBandFloat  band6;
		private PixelBandDouble band7;

		//---------------------------------------------------------------------

		public byte Band1
		{
			get {
				return band1.Value;
			}
			set {
				band1.Value = value;
			}
		}

		//---------------------------------------------------------------------

		public short Band2
		{
			get {
				return band2.Value;
			}
			set {
				band2.Value = value;
			}
		}

		//---------------------------------------------------------------------

		public ushort Band3
		{
			get {
				return band3.Value;
			}
			set {
				band3.Value = value;
			}
		}

		//---------------------------------------------------------------------

		public int Band4
		{
			get {
				return band4.Value;
			}
			set {
				band4.Value = value;
			}
		}

		//---------------------------------------------------------------------

		public uint Band5
		{
			get {
				return band5.Value;
			}
			set {
				band5.Value = value;
			}
		}

		//---------------------------------------------------------------------

		public float Band6
		{
			get {
				return band6.Value;
			}
			set {
				band6.Value = value;
			}
		}

		//---------------------------------------------------------------------

		public double Band7
		{
			get {
				return band7.Value;
			}
			set {
				band7.Value = value;
			}
		}

		//---------------------------------------------------------------------

		private void InitializeBands()
		{
			band1 = new PixelBandByte();
			band2 = new PixelBandShort();
			band3 = new PixelBandUShort();
			band4 = new PixelBandInt();
			band5 = new PixelBandUInt();
			band6 = new PixelBandFloat();
			band7 = new PixelBandDouble();
			SetBands(band1, band2, band3, band4, band5, band6, band7);
		}

		//---------------------------------------------------------------------

		public MultiBandPixel()
		{
			InitializeBands();
		}
	}
}
