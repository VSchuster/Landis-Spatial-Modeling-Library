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

using Edu.Wisc.Forest.Flel.Util;
using Gov.Fgdc.Csdgm;
using Wisc.Flel.GeospatialModeling.RasterIO;
using Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74;
using Erdas74Metadata = Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74.Metadata;
using NUnit.Framework;
using System;
using System.IO;

namespace Wisc.Flel.Test.GeospatialModeling.RasterDrivers.Erdas74
{
	[TestFixture]
	public class InputRasterTests
	{
		private InputRaster<TPixel> OpenRaster<TPixel>(string filename)
			where TPixel : IPixel, new()
		{
			Data.Output.WriteLine();
			Data.Output.WriteLine("Reading the file \"{0}\\{1}\" ...",
			                      Data.DirPlaceholder, filename);
			return new InputRaster<TPixel>(Data.MakeInputPath(filename));
		}

		//---------------------------------------------------------------------

		private void PrintError(Exception exc)
		{
			Data.Output.WriteLine("Error reading the raster file:");
			Data.Output.WriteLine("  {0}", exc.Message);
		}

		//---------------------------------------------------------------------

		private void TryOpen<TPixel>(string filename)
			where TPixel : IPixel, new()
		{
			try {
				IInputRaster<TPixel> raster = OpenRaster<TPixel>(filename);
				raster.Close();
			}
			catch (Exception exc) {
				PrintError(exc);
				throw;
			}
		}

		//---------------------------------------------------------------------

		[Test]
		[ExpectedException(typeof(EndOfStreamException))]
		public void Header_127Bytes()
		{
			TryOpen<SingleBandPixel<byte>>("Header-127Bytes.gis");
		}

		//---------------------------------------------------------------------

		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void Header_BadHDWORD()
		{
			TryOpen<SingleBandPixel<byte>>("Header-BadHDWORD.gis");
		}

		//---------------------------------------------------------------------

		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void Header_ICOLS_F1F2F3F4()
		{
			TryOpen<SingleBandPixel<byte>>("Header-ICOLS=F1F2F3F4.gis");
		}

		//---------------------------------------------------------------------

		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void Header_IROWS_F1F2F3F4()
		{
			TryOpen<SingleBandPixel<byte>>("Header-IROWS=F1F2F3F4.gis");
		}

		//---------------------------------------------------------------------

		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void Header_IPACK_4Bit()
		{
			TryOpen<SingleBandPixel<byte>>("Header-IPACK=4Bit.gis");
		}

		//---------------------------------------------------------------------

		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void Header_IPACK_0A0B()
		{
			TryOpen<SingleBandPixel<byte>>("Header-IPACK=0A0B.gis");
		}

		//---------------------------------------------------------------------

		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void Header_NBANDS_F1F2()
		{
			TryOpen<SingleBandPixel<byte>>("Header-NBANDS=F1F2.gis");
		}

		//---------------------------------------------------------------------

		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void BandCountMismatch()
		{
			TryOpen<SingleBandPixel<byte>>("7Band-8Bit.lan");
		}

		//---------------------------------------------------------------------

		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void BandTypeMismatch_16Bit_SByte()
		{
			TryOpen<SingleBandPixel<sbyte>>("1Band-16Bit.gis");
		}

		//---------------------------------------------------------------------

		[Test]
		[ExpectedException(typeof(ApplicationException))]
		public void BandTypeMismatch_16Bit_Byte()
		{
			TryOpen<SingleBandPixel<byte>>("1Band-16Bit.gis");
		}

		//---------------------------------------------------------------------

		[Test]
		public void Raster_1Band_8Bit()
		{
			InputRaster<SingleBandPixel<byte>> raster;
			using (raster = OpenRaster<SingleBandPixel<byte>>("1Band-8Bit.gis")) {
				Read8BitIntoByte(raster);
			}
		}

		//---------------------------------------------------------------------

		[Test]
		public void Raster_1Band_8Bit_Signed()
		{
			InputRaster<SingleBandPixel<sbyte>> raster;
			using (raster = OpenRaster<SingleBandPixel<sbyte>>("1Band-8Bit-00toFF.gis")) {
				byte pixelOffset = 0;
				for (int row = 1; row <= raster.Dimensions.Rows; ++row) {
					for (int column = 1; column <= raster.Dimensions.Columns; ++column) {
						SingleBandPixel<sbyte> pixel = raster.ReadPixel();
						Assert.AreEqual((sbyte) pixelOffset, pixel.Band0);
						Assert.AreEqual(pixelOffset + 1, raster.PixelsRead);
						pixelOffset++;
					}
				}
			}
		}

		//---------------------------------------------------------------------

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ReadTooMany()
		{
			try {
				InputRaster<SingleBandPixel<byte>> raster;
				using (raster = OpenRaster<SingleBandPixel<byte>>("1Band-8Bit.gis")) {
					Read8BitIntoByte(raster);
					raster.ReadPixel();
				}
			}
			catch (Exception exc) {
				PrintError(exc);
				throw;
			}
		}

		//---------------------------------------------------------------------

		[Test]
		[ExpectedException(typeof(EndOfStreamException))]
		public void MissingPixelData()
		{
			try {
				InputRaster<SingleBandPixel<byte>> raster;
				using (raster = OpenRaster<SingleBandPixel<byte>>("1Band-8Bit-PartialRow11.gis")) {
					Read8BitIntoByte(raster);
				}
			}
			catch (Exception exc) {
				PrintError(exc);
				throw;
			}
		}

		//---------------------------------------------------------------------

		private void Read8BitIntoByte(InputRaster<SingleBandPixel<byte>> raster)
		{
			byte pixelNumber = 0;
			for (int row = 1; row <= raster.Dimensions.Rows; ++row) {
				for (int column = 1; column <= raster.Dimensions.Columns; ++column) {
					SingleBandPixel<byte> pixel = raster.ReadPixel();
					pixelNumber++;
					Assert.AreEqual(pixelNumber, pixel.Band0);
					Assert.AreEqual(pixelNumber, raster.PixelsRead);
				}
			}
		}

		//---------------------------------------------------------------------

		public delegate T FromUInt<T>(uint value);

		public short FromUIntToShort(uint value)
		{
			return (short) value;
		}

		public ushort FromUIntToUShort(uint value)
		{
			return (ushort) value;
		}

		//---------------------------------------------------------------------

		[Test]
		public void Raster_1Band_16Bit_Signed()
		{
			Read_1Band_16Bit<short>(FromUIntToShort);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Raster_1Band_16Bit_Unsigned()
		{
			Read_1Band_16Bit<ushort>(FromUIntToUShort);
		}

		//---------------------------------------------------------------------

		private void Read_1Band_16Bit<T>(FromUInt<T> converter)
			where T : struct
		{
			InputRaster<SingleBandPixel<T>> raster;
			using (raster = OpenRaster<SingleBandPixel<T>>("1Band-16Bit-0000to7FFF.gis")) {
				uint pixelOffset = 0;
				for (int row = 1; row <= raster.Dimensions.Rows; ++row) {
					for (int column = 1; column <= raster.Dimensions.Columns; ++column) {
						SingleBandPixel<T> pixel = raster.ReadPixel();
						Assert.AreEqual(converter(pixelOffset), pixel.Band0);
						Assert.AreEqual(pixelOffset + 1, raster.PixelsRead);
						pixelOffset++;
					}
				}
			}
		}

		//---------------------------------------------------------------------

		[Test]
		public void Raster_7Band_8Bit()
		{
			IInputRaster<MultiBandPixel> raster;
			using (raster = OpenRaster<MultiBandPixel>("7Band-8Bit.lan")) {
				byte pixelNumber = 0;
				for (int row = 1; row <= raster.Dimensions.Rows; ++row) {
					for (int column = 1; column <= raster.Dimensions.Columns; ++column) {
						MultiBandPixel pixel = raster.ReadPixel();
						pixelNumber++;
						Assert.AreEqual((byte) (0x10 + pixelNumber), pixel.Band1);
						Assert.AreEqual((byte) (0x20 + pixelNumber), pixel.Band2);
						Assert.AreEqual((byte) (0x30 + pixelNumber), pixel.Band3);
						Assert.AreEqual((byte) (0x40 + pixelNumber), pixel.Band4);
						Assert.AreEqual((byte) (0x50 + pixelNumber), pixel.Band5);
						Assert.AreEqual((byte) (0x60 + pixelNumber), pixel.Band6);
						Assert.AreEqual((byte) (0x70 + pixelNumber), pixel.Band7);
					}
				}
			}
		}

		//---------------------------------------------------------------------

		[Test]
		public void Metadata()
		{
			InputRaster<SingleBandPixel<byte>> raster;
			using (raster = OpenRaster<SingleBandPixel<byte>>("1Band-8Bit.gis")) {
				IMetadata metadata = raster.Metadata;
				AssertMetadata(metadata, Erdas74Metadata.HDWORD, "HEAD74");
				AssertMetadata(metadata, Erdas74Metadata.XSTART, unchecked((int) 0xA4A3A2A1));
				AssertMetadata(metadata, Erdas74Metadata.YSTART, unchecked((int) 0xADACABAA));
				AssertMetadata(metadata, Erdas74Metadata.MAPTYP, (short) 0x11);
				AssertMetadata(metadata, MapProjectionName.Name,
				                         MapProjectionName.Equirectangular);

				AssertMetadata(metadata, Erdas74Metadata.NCLASS, (short) 0xFF);
				AssertMetadata(metadata, Erdas74Metadata.IAUTYP, (short) 0x03);
				AssertMetadata(metadata, Erdas74Metadata.ACRE,   MakeFloat(0xD4D3D2D1));

				float xMap = MakeFloat(0xB4B3B2B1);
				AssertMetadata(metadata, Erdas74Metadata.XMAP,        xMap);
				AssertMetadata(metadata, WestBoundingCoordinate.Name, xMap);

				float yMap = MakeFloat(0xBDBCBBBA);
				AssertMetadata(metadata, Erdas74Metadata.YMAP,         yMap);
				AssertMetadata(metadata, NorthBoundingCoordinate.Name, yMap);

				float xCell = MakeFloat(0x64636261);
				AssertMetadata(metadata, Erdas74Metadata.XCELL,   xCell);
				AssertMetadata(metadata, AbscissaResolution.Name, xCell);

				float yCell = MakeFloat(0x74737271);
				AssertMetadata(metadata, Erdas74Metadata.YCELL,   yCell);
				AssertMetadata(metadata, OrdinateResolution.Name, yCell);

				AssertMetadata(metadata, PlanarDistanceUnits.Name,
				                         PlanarDistanceUnits.Meters);
			}
		}

		//---------------------------------------------------------------------

		private void AssertMetadata<T>(IMetadata metadata,
		                               string    name,
		                               T         expectedValue)
		{
			T value = default(T);
			Assert.IsTrue(metadata.TryGetValue(name, ref value));
			Assert.AreEqual(expectedValue, value);
		}

		//---------------------------------------------------------------------

		private float MakeFloat(long bytes)
		{
			byte[] bytesInLong = BitConverter.GetBytes(bytes);
			//	Only interested low-order 4 bytes
			return BitConverter.ToSingle(bytesInLong, 0);
		}
	}
}
