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

using Edu.Wisc.Forest.Flel.Util;
using Wisc.Flel.GeospatialModeling.RasterIO;
using Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74;
using NUnit.Framework;
using System;
using System.IO;

namespace Wisc.Flel.Test.GeospatialModeling.RasterDrivers.Erdas74
{
	[TestFixture]
	public class OutputBandTests
	{
		[Test]
		public void Byte_UShort()
		{
			byte[] byteValues = new byte[] { 1, 10, 1, 100, 255, 254, 200, 0 };

			byte[] buffer = new byte[byteValues.Length * sizeof(ushort)];
	   		BinaryWriter bufferWriter = new BinaryWriter(new MemoryStream(buffer));
			IOutputBand band = new OutputBand<byte, ushort>(Convert.ToUInt16,
															bufferWriter.Write);

	   		SingleBandPixel<byte> pixel = new SingleBandPixel<byte>();
			for (int i = 0; i < byteValues.Length; i++) {
				pixel.Band0 = byteValues[i];
				band.AppendPixel(pixel[0]);
			}

	   		for (int i = 0; i < byteValues.Length; i++) {
				int bufferOffset = i * 2;
				Assert.AreEqual((ushort) byteValues[i],
								BitConverter.ToUInt16(buffer, bufferOffset));
			}
		}

		//---------------------------------------------------------------------

		[Test]
		public void SByte_Short()
		{
			sbyte[] sbyteValues = new sbyte[] { 1, 10, 127, 100, -128, 0, -111 };

			byte[] buffer = new byte[sbyteValues.Length * sizeof(short)];
	   		BinaryWriter bufferWriter = new BinaryWriter(new MemoryStream(buffer));
			IOutputBand band = new OutputBand<sbyte, short>(Convert.ToInt16,
															bufferWriter.Write);

	   		SingleBandPixel<sbyte> pixel = new SingleBandPixel<sbyte>();
			for (int i = 0; i < sbyteValues.Length; i++) {
				pixel.Band0 = sbyteValues[i];
				band.AppendPixel(pixel[0]);
			}

	   		for (int i = 0; i < sbyteValues.Length; i++) {
				int bufferOffset = i * 2;
				Assert.AreEqual((short) sbyteValues[i],
								BitConverter.ToInt16(buffer, bufferOffset));
			}
		}
	}
}
