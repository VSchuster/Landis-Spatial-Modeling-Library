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
	delegate byte[] ToBytesMethod<T>(T value);

    [TestFixture]
    public class InputBandTests
    {
    	private ushort[] values16Bit;
    	private MemoryStream stream16Bit;

        //---------------------------------------------------------------------

        private MemoryStream NewMemoryStream<T>(T[]              values,
                                                int              valueSize,
                                                ToBytesMethod<T> getBytes)
        	where T : struct
        {
        	byte[] bytes = new byte[valueSize * values.Length];
        	for (int i = 0; i < values.Length; ++i) {
        		byte[] valueBytes = getBytes(values[i]);
        		for (int j = 0; j < valueBytes.Length; ++j)
        			bytes[valueSize * i + j] = valueBytes[j];
        	}
        	return new MemoryStream(bytes);
        }

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
        	values16Bit = new ushort[] {
        		0, 1, 10, 100, 255, 256, 1000, 10000, 20000, 65535
        	};
        	stream16Bit = NewMemoryStream<ushort>(values16Bit, sizeof(ushort),
        	                                      BitConverter.GetBytes);
        }

        //---------------------------------------------------------------------

        [Test]
        public void UShort_Float()
        {
        	stream16Bit.Seek(0, SeekOrigin.Begin);
        	BinaryReader reader = new BinaryReader(stream16Bit);
        	SingleBandPixel<float> pixel = new SingleBandPixel<float>();
            IInputBand band = new InputBand<ushort, float>(values16Bit.Length,
        	                                               reader.ReadUInt16,
        	                                               pixel[0],
        	                                               Convert.ToSingle);
        	band.ReadData();
        	for (int i = 0; i < values16Bit.Length; i++) {
        		band.AssignNextPixel();
        		Assert.AreEqual(values16Bit[i], pixel.Band0);
        	}
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void AssignTooMany()
        {
        	stream16Bit.Seek(0, SeekOrigin.Begin);
        	BinaryReader reader = new BinaryReader(stream16Bit);
        	SingleBandPixel<float> pixel = new SingleBandPixel<float>();
            IInputBand band = new InputBand<ushort, float>(values16Bit.Length,
        	                                               reader.ReadUInt16,
        	                                               pixel[0],
        	                                               Convert.ToSingle);
        	band.ReadData();
        	for (int i = 0; i < values16Bit.Length; i++) {
        		band.AssignNextPixel();
        		Assert.AreEqual(values16Bit[i], pixel.Band0);
        	}
        	try {
	        	band.AssignNextPixel();
        	}
        	catch (System.InvalidOperationException exc) {
        		Data.Output.WriteLine(exc.Message);
        		throw;
        	}
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.IO.EndOfStreamException))]
        public void BufferTooBig()
        {
        	stream16Bit.Seek(0, SeekOrigin.Begin);
        	BinaryReader reader = new BinaryReader(stream16Bit);
        	SingleBandPixel<float> pixel = new SingleBandPixel<float>();
            IInputBand band = new InputBand<ushort, float>(values16Bit.Length + 1,
        	                                               reader.ReadUInt16,
        	                                               pixel[0],
        	                                               Convert.ToSingle);
        	band.ReadData();
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(System.ArgumentException))]
        public void BandNull()
        {
        	try {
	            IInputBand band = new InputBand<ushort, float>(1,
	        	                                               null,  // read method
	        	                                               null,  // pixel band
	        	                                               Convert.ToSingle);
        	}
        	catch (System.ArgumentException exc) {
        		Data.Output.WriteLine(exc.Message);
        		throw;
        	}
        }
    }
}
