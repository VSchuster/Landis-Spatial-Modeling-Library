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
using Wisc.Flel.GeospatialModeling.RasterIO;
using Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74;
using NUnit.Framework;
using System;
using System.IO;

namespace Wisc.Flel.Test.GeospatialModeling.RasterDrivers.Erdas74
{
    [TestFixture]
    public class ImageHeaderTests
    {
    	private byte[] readBuffer;
    	private const short expectedIPack = ImageHeader.IPack_16bit;
    	private const short expectedNBands = 3;
    	private const int   expectedICols = 123;
    	private const int   expectedIRows = 4000;
    	private const int   expectedXStart = -1;
    	private const int   expectedYStart = 7654321;
    	private const short expectedMapTyp = 11;
    	private const short expectedNClass = 222;
    	private const short expectedIAUTyp = ImageHeader.IAUTyp_HECTARE;
    	private const float expectedAcre = 900.0f;
        private const float expectedXMap = -4.0f;
        private const float expectedYMap = 1e18f;
        private const float expectedXCell = 30.0f;
        private const float expectedYCell = 0.25f;

    	[TestFixtureSetUp]
    	public void Init()
    	{
    		readBuffer = new byte[ImageHeader.Size];
    		readBuffer[0] = (byte) 'H';
    		readBuffer[1] = (byte) 'E';
    		readBuffer[2] = (byte) 'A';
    		readBuffer[3] = (byte) 'D';
    		readBuffer[4] = (byte) '7';
    		readBuffer[5] = (byte) '4';

    		PutNumber(expectedIPack,  readBuffer, 6);
    		PutNumber(expectedNBands, readBuffer, 8);
    		PutNumber(expectedICols,  readBuffer, 16);
    		PutNumber(expectedIRows,  readBuffer, 20);
    		PutNumber(expectedXStart, readBuffer, 24);
    		PutNumber(expectedYStart, readBuffer, 28);
    		PutNumber(expectedMapTyp, readBuffer, 88);
    		PutNumber(expectedNClass, readBuffer, 90);
    		PutNumber(expectedIAUTyp, readBuffer, 106);
    		PutNumber(expectedAcre,   readBuffer, 108);
        	PutNumber(expectedXMap,   readBuffer, 112);
        	PutNumber(expectedYMap,   readBuffer, 116);
        	PutNumber(expectedXCell,  readBuffer, 120);
        	PutNumber(expectedYCell,  readBuffer, 124);
    	}

    	private void PutNumber(short  number,
    	                       byte[] buffer,
    	                       int    index)
    	{
    		CopyBytes(BitConverter.GetBytes(number), buffer, index);
    	}

    	private void PutNumber(int    number,
    	                       byte[] buffer,
    	                       int    index)
    	{
    		CopyBytes(BitConverter.GetBytes(number), buffer, index);
    	}

    	private void PutNumber(float  number,
    	                       byte[] buffer,
    	                       int    index)
    	{
    		CopyBytes(BitConverter.GetBytes(number), buffer, index);
    	}

    	private void CopyBytes(byte[] dataToCopy,
    	                       byte[] buffer,
    	                       int    index)
    	{
    		if ((index < 0) || (index + dataToCopy.Length > buffer.Length))
    			throw new IndexOutOfRangeException();
    		for (int i = 0; i < dataToCopy.Length; ++i)
    			buffer[index+i] = dataToCopy[i];
    	}

    	[Test]
        public void NoArgConstruct()
        {
            ImageHeader h = new ImageHeader();

            Assert.AreEqual(0, h.IPack);
            Assert.AreEqual(0, h.NBands);
            Assert.AreEqual(0, h.ICols);
            Assert.AreEqual(0, h.IRows);
            Assert.AreEqual(0, h.XStart);
            Assert.AreEqual(0, h.YStart);
            Assert.AreEqual(0, h.MapTyp);
            Assert.AreEqual(0, h.NClass);
            Assert.AreEqual(0, h.IAUTyp);
            Assert.AreEqual(0, h.Acre);
            Assert.AreEqual(0, h.XMap);
            Assert.AreEqual(0, h.YMap);
            Assert.AreEqual(0, h.XCell);
            Assert.AreEqual(0, h.YCell);
        }

        private BinaryWriter MakeWriterWithBuffer(out byte[] buffer)
        {
        	buffer = new byte[ImageHeader.Size];
        	for (int i = 0; i < buffer.Length; ++i)
        		buffer[i] = (byte) 255;
        	MemoryStream stream = new MemoryStream(buffer);
        	return new BinaryWriter(stream);
        }

        [Test]
        public void NoArgConstruct_Write()
        {
        	byte[] buffer;
        	BinaryWriter writer = MakeWriterWithBuffer(out buffer);

        	ImageHeader h = new ImageHeader();
        	h.Write(writer);
        	writer.Close();

        	Assert.AreEqual((byte) 'H', buffer[0]);
        	Assert.AreEqual((byte) 'E', buffer[1]);
        	Assert.AreEqual((byte) 'A', buffer[2]);
        	Assert.AreEqual((byte) 'D', buffer[3]);
        	Assert.AreEqual((byte) '7', buffer[4]);
        	Assert.AreEqual((byte) '4', buffer[5]);
        	for (int i = 6; i < buffer.Length; ++i)
        		Assert.AreEqual(0, buffer[i]);
        }

        [Test]
        public void SetPropertiesAndWrite()
        {
        	ImageHeader h = new ImageHeader();
        	h.IPack  = expectedIPack;
            h.NBands = expectedNBands;
            h.ICols  = expectedICols;
            h.IRows  = expectedIRows;
            h.XStart = expectedXStart;
            h.YStart = expectedYStart;
            h.MapTyp = expectedMapTyp;
            h.NClass = expectedNClass;
            h.IAUTyp = expectedIAUTyp;
            h.Acre   = expectedAcre;
            h.XMap   = expectedXMap;
            h.YMap   = expectedYMap;  
            h.XCell  = expectedXCell;
            h.YCell  = expectedYCell;

        	byte[] buffer;
        	BinaryWriter writer = MakeWriterWithBuffer(out buffer);
        	h.Write(writer);
        	writer.Close();

        	for (int i = 0; i < buffer.Length; ++i)
        		Assert.AreEqual(readBuffer[i], buffer[i]);
        }

        [Test]
        public void ReaderConstruct()
        {
        	MemoryStream stream = new MemoryStream(readBuffer);
        	BinaryReader reader = new BinaryReader(stream);
            ImageHeader h = new ImageHeader(reader);
            reader.Close();
            
            Assert.AreEqual(expectedIPack,  h.IPack);
            Assert.AreEqual(expectedNBands, h.NBands);
            Assert.AreEqual(expectedICols,  h.ICols);
            Assert.AreEqual(expectedIRows,  h.IRows);
            Assert.AreEqual(expectedXStart, h.XStart);
            Assert.AreEqual(expectedYStart, h.YStart);
            Assert.AreEqual(expectedMapTyp, h.MapTyp);
            Assert.AreEqual(expectedNClass, h.NClass);
            Assert.AreEqual(expectedIAUTyp, h.IAUTyp);
            Assert.AreEqual(expectedAcre,   h.Acre);
            Assert.AreEqual(expectedXMap,   h.XMap);
            Assert.AreEqual(expectedYMap,   h.YMap);
            Assert.AreEqual(expectedXCell,  h.XCell);
            Assert.AreEqual(expectedYCell,  h.YCell);
        }

        private BinaryReader OpenRaster(string filename)
        {
        	FileStream stream = new FileStream(Data.MakeInputPath(filename),
        	                                   FileMode.Open);
        	try {
        		return new BinaryReader(stream);
        	}
        	catch {
        		stream.Close();
        		throw;
        	}
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void WrongHeaderWord()
        {
        	using (BinaryReader reader = OpenRaster("Header-BadHDWORD.gis")) {
        		ImageHeader h = new ImageHeader(reader);
        	}
        }
        
        [Test]
        [ExpectedException(typeof(EndOfStreamException))]
        public void NotEnoughBytes()
        {
        	using (BinaryReader reader = OpenRaster("Header-127Bytes.gis")) {
        		ImageHeader h = new ImageHeader(reader);
        	}
        }
    }
}
