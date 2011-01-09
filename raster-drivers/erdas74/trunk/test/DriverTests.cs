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
//Library
//        public IInputRaster<T> Open<T>(string path)
//        public IOutputRaster<T> Create<T>(string path,Dimensions dimensions,IMetadata metadata)

	//  Convert from one base type to another base type
    public delegate TTo Converter<TFrom,TTo>(TFrom value)
    	where TFrom : struct
    	where TTo : struct;

    [TestFixture]
    public class DriverTests
    {
        private Driver driver;
        private string pathSingleBand8Bit;
        private byte[,] map8BitData;
        private Dimensions dims8Bit;

        [TestFixtureSetUp]
        public void Init()
        {
            driver = new Driver();

            pathSingleBand8Bit = Data.MakeOutputPath("singleBand_8bit.gis");
            map8BitData = new byte[,] {
            	{   0,  10,  20,  30,  40 },
            	{  50,  60,  70,  80,  90 },
            	{ 100, 110, 120, 130, 140 },
            	{ 150, 160, 170, 180, 190 },
            	{ 200, 210, 220, 230, 240 },
            	{ 250, 252, 255,   0, 255 }
            };
            dims8Bit = new Dimensions(map8BitData.GetLength(0),
                                      map8BitData.GetLength(1));
            FileStream file = new FileStream(pathSingleBand8Bit, FileMode.Create, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(file);
            writer.Write(MakeHeader(dims8Bit));
            for (int row = 0; row < dims8Bit.Rows; ++row) {
            	for (int column = 0; column < dims8Bit.Columns; ++column) {
            		writer.Write(map8BitData[row, column]);
            	}
            }
            writer.Close();
        }

        private byte[] MakeHeader(Dimensions dims)
        {
        	byte[] header = new byte[128];
        	header[0] = (byte) 'H';
        	header[1] = (byte) 'E';
        	header[2] = (byte) 'A';
        	header[3] = (byte) 'D';
        	header[4] = (byte) '7';
        	header[5] = (byte) '4';
        	header[8] = 1;   // 1 band
        	PutValue((int) dims.Columns, header, 16);
        	PutValue((int) dims.Rows,    header, 20);
        	return header;
        }

        private void PutValue(int    value,
                              byte[] buffer,
                              int    offset)
        {
        	byte[] valueBytes = System.BitConverter.GetBytes(value);
        	if (valueBytes.Length > buffer.Length - (offset + 1))
        		throw new System.ArgumentException("Insufficient room in buffer");
        	for (int i = 0; i < valueBytes.Length; ++i) {
        		buffer[offset+i] = valueBytes[i];
        	}
        }

        private void Open8Bit<T>(Converter<byte,T> converter)
        	where T: struct
        {
            IInputRaster<SingleBandPixel<T>> raster;
            using (raster = driver.OpenRaster<SingleBandPixel<T>>(pathSingleBand8Bit)) {
	            Assert.AreEqual(pathSingleBand8Bit, raster.Path);
	            Assert.AreEqual(dims8Bit, raster.Dimensions);
	            for (int row = 0; row < dims8Bit.Rows; ++row) {
	            	for (int column = 0; column < dims8Bit.Columns; ++column) {
	            		Assert.AreEqual(converter(map8BitData[row,column]),
	            		                raster.ReadPixel().Band0);
	            	}
	            }
            }
        }

        private void OpenSigned8Bit<T>(Converter<sbyte,T> converter)
        	where T: struct
        {
            IInputRaster<SingleBandPixel<T>> raster;
            using (raster = driver.OpenRaster<SingleBandPixel<T>>(pathSingleBand8Bit)) {
	            Assert.AreEqual(pathSingleBand8Bit, raster.Path);
	            Assert.AreEqual(dims8Bit, raster.Dimensions);
	            for (int row = 0; row < dims8Bit.Rows; ++row) {
	            	for (int column = 0; column < dims8Bit.Columns; ++column) {
	            		Assert.AreEqual(converter((sbyte) map8BitData[row,column]),
	            		                raster.ReadPixel().Band0);
	            	}
	            }
            }
        }

        [Test]
        public void Open8BitAsByte()
        {
        	Open8Bit<byte>(System.Convert.ToByte);
        }

        [Test]
        public void Open8BitAsUShort()
        {
        	Open8Bit<ushort>(Convert.ToUInt16);
        }

        [Test]
        public void Open8BitAsShort()
        {
        	OpenSigned8Bit<short>(Convert.ToInt16);
        }

        [Test]
        public void Open8BitAsUInt()
        {
        	Open8Bit<uint>(Convert.ToUInt32);
        }

        [Test]
        public void Open8BitAsInt()
        {
        	OpenSigned8Bit<int>(Convert.ToInt32);
        }

        [Test]
        public void Open8BitAsFloat()
        {
        	OpenSigned8Bit<float>(Convert.ToSingle);
        }

        [Test]
        public void Open8BitAsDouble()
        {
        	OpenSigned8Bit<double>(Convert.ToDouble);
        }

        [Test]
        public void Open1()
        {
            IInputRaster<Erdas74Pixel8> raster;
            using (raster = driver.OpenRaster<Erdas74Pixel8>(Data.MakeInputPath("fred.gis"))) {
	            Assert.AreEqual(raster.Path, Data.MakeInputPath("fred.gis"));
            }
        }

        [Test]
        //	No longer an exception because 16-bit pixel supports 8-bit data now.
        //	[ExpectedException(typeof(ApplicationException))]
        public void Open2()
        {
            // not made up of 16bit pixels
            IInputRaster<Erdas74Pixel16> raster;
            try {
                raster = driver.OpenRaster<Erdas74Pixel16>(Data.MakeInputPath("fred.gis"));
                raster.Close();
            }
            catch (Exception exc) {
                Data.Output.WriteLine(exc.Message);
                throw;
            }
        }
        
        [Test]
        public void Create()
        {
            IOutputRaster<Erdas74Pixel8> raster =
                driver.CreateRaster<Erdas74Pixel8>(Data.MakeOutputPath("temp.gis"),
                                                   new Dimensions(20,10),
                                                   null);
            
            Assert.AreEqual(raster.Path,Data.MakeOutputPath("temp.gis"));
            Assert.AreEqual(raster.Dimensions.Rows,20);
            Assert.AreEqual(raster.Dimensions.Columns,10);
            
            raster.Close();
        }
        
    }
}
