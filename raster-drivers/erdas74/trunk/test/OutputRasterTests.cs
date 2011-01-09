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

using Wisc.Flel.GeospatialModeling.Grids;
using Edu.Wisc.Forest.Flel.Util;
using Wisc.Flel.GeospatialModeling.RasterIO;
using Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74;
using NUnit.Framework;
using System;
using System.IO;

namespace Wisc.Flel.Test.GeospatialModeling.RasterDrivers.Erdas74
{
	[TestFixture]
	public class OutputRasterTests
	{
		private OutputRaster<TPixel> CreateRaster<TPixel>(string     filename,
		                                                  Dimensions dimensions,
		                                                  IMetadata  metadata)
			where TPixel : IPixel, new()
		{
			Data.Output.WriteLine();
			Data.Output.WriteLine("Writing the file \"{0}\\{1}\" ...",
			                      Data.DirPlaceholder, filename);
			return new OutputRaster<TPixel>(Data.MakeOutputPath(filename),
			                                dimensions,
			                                metadata);
		}

		//---------------------------------------------------------------------

		private void PrintError(Exception exc)
		{
			Data.Output.WriteLine("Error writing the raster file:");
			Data.Output.WriteLine("  {0}", exc.Message);
		}

		//---------------------------------------------------------------------

		private BinaryReader MakeBinaryReader(string path)
		{
			FileStream stream = new FileStream(path, FileMode.Open);
			return new BinaryReader(stream);
		}

		//---------------------------------------------------------------------

		private IMetadata ReadMetadata(string path)
		{
			using (BinaryReader reader = MakeBinaryReader(path)) {
				ImageHeader header = new ImageHeader(reader);
				return Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74.Metadata.Create(header);
			}
		}

		//---------------------------------------------------------------------

		private void AssertFilesEqual(string path1,
		                              string path2)
		{
			using (BinaryReader reader1 = MakeBinaryReader(path1)) {
				using (BinaryReader reader2 = MakeBinaryReader(path2)) {
					while (reader1.PeekChar() != -1 && reader2.PeekChar() != -1)
						Assert.AreEqual(reader1.ReadByte(), reader2.ReadByte());
					Assert.AreEqual(-1, reader1.PeekChar());
					Assert.AreEqual(-1, reader2.PeekChar());
				}
			}
		}

		//---------------------------------------------------------------------

		[Test]
		public void Raster_1Band_8Bit()
		{
			Dimensions dimensions = new Dimensions(32, 8);
			string rasterFile = Data.MakeInputPath("1Band-8Bit-50m.gis");
			IMetadata metadata = ReadMetadata(rasterFile);

			OutputRaster<SingleBandPixel<byte>> raster;
			string outputRasterPath;
			using (raster = CreateRaster<SingleBandPixel<byte>>("1Band-8Bit.gis",
			                                                    dimensions,
			                                                    metadata)) {
				outputRasterPath = raster.Path;
				SingleBandPixel<byte> pixel = new SingleBandPixel<byte>();
				for (int i = 0x00; i <= 0xFF; i++) {
					pixel.Band0 = (byte) i;
					raster.WritePixel(pixel);
				}
			}

			AssertFilesEqual(rasterFile, outputRasterPath);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Raster_1Band_8Bit_Signed()
		{
			Dimensions dimensions = new Dimensions(32, 8);
			string rasterFile = Data.MakeInputPath("1Band-8Bit-50m.gis");
			IMetadata metadata = ReadMetadata(rasterFile);

			OutputRaster<SingleBandPixel<sbyte>> raster;
			string outputRasterPath;
			using (raster = CreateRaster<SingleBandPixel<sbyte>>("1Band-8Bit-Signed.gis",
			                                                     dimensions,
			                                                     metadata)) {
				outputRasterPath = raster.Path;
				SingleBandPixel<sbyte> pixel = new SingleBandPixel<sbyte>();
				for (int i = 0x00; i <= 0xFF; i++) {
					pixel.Band0 = (sbyte) i;
					raster.WritePixel(pixel);
				}
			}

			AssertFilesEqual(rasterFile, outputRasterPath);
		}

		//---------------------------------------------------------------------

		[Test]
		public void Raster_1Band_16Bit()
		{
			Dimensions dimensions = new Dimensions(4096, 8);
			string rasterFile = Data.MakeInputPath("1Band-16Bit-0000to7FFF.gis");
			IMetadata metadata = ReadMetadata(rasterFile);

			OutputRaster<SingleBandPixel<short>> raster;
			string outputRasterPath;
			using (raster = CreateRaster<SingleBandPixel<short>>("1Band-16Bit.gis",
			                                                     dimensions,
			                                                     metadata)) {
				outputRasterPath = raster.Path;
				SingleBandPixel<short> pixel = new SingleBandPixel<short>();
				for (int i = 0x0000; i <= 0x7FFF; i++) {
					pixel.Band0 = (short) i;
					raster.WritePixel(pixel);
				}
			}

			AssertFilesEqual(rasterFile, outputRasterPath);
		}
	}
}
