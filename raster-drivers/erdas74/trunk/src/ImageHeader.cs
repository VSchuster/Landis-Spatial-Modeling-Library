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

using Wisc.Flel.GeospatialModeling.RasterIO;
using System;
using System.IO;

namespace Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74
{
    /// Erdas file header structure
    ///
    /// Bytes 1-6     : HDWORD - a 6 byte array containing 'HEAD74'
    /// Bytes 7-8     : IPACK - an integer val which indicates the bits per
    ///                   subpixel band:  0 = 8 bit   1 = 4 bit   2 = 16 bit
    /// Bytes 9-10    : NBANDS - integer that indicates the number of bands per pixel
    ///                   Always 1 for GIS files, >= 1 for LAN files
    /// Bytes 11-16   : unused
    /// Bytes 17-20   : ICOLS - an integer specifying the width of file in pixels
    /// Bytes 21-24   : ICOLS - an integer specifying the length of file in pixels
    /// Bytes 25-28   : XSTART - integer specifying the database x-coord of upper
    ///                   left pixel. Raster coord space.
    /// Bytes 29-32   : YSTART - integer specifying the database y-coord of upper
    ///                   left pixel. Raster coord space.
    /// Bytes 33-88   : unused
    /// Bytes 89-90   : MAPTYP - the projection number - see Projections.cs for defs
    /// Bytes 91-92   : NCLASS - the number of classes present in map. We just make 0.
    /// Bytes 93-106  : unused
    /// Bytes 107-108 : IAUTYP - integer indicating area unit associated with each
    ///                   pixel. 0 = None, 1 = Acre, 2 = Hectare, 3 = Other
    /// Bytes 109-112 : ACRE - A float specifying number of area units of each pixel
    /// Bytes 113-116 : XMAP - A float giving x map coord for the upper left pixel.
    ///                   World coord space.
    /// Bytes 117-120 : YMAP - A float giving y map coord for the upper left pixel.
    ///                   World coord space.
    /// Bytes 121-124 : XCELL - A float specifying the x size of each cell. Units
    ///                   depend on MAPTYP: 2 (State Plane) = feet, 0 (Lat/Lon) =
    ///                   degrees, all others = meters
    /// Bytes 125-128 : YCELL - A float specifying the y size of each cell. Units
    ///                   depend on MAPTYP: 2 (State Plane) = feet, 0 (Lat/Lon) =
    ///                   degrees, all others = meters
    
    public class ImageHeader
    {
        // ERDAS GIS/LAN file header size (in bytes)
        public const int Size = 128;

        // Instance variables
        private Int16 iPack;
        private Int16 nBands;
    	private Int32 iCols;
    	private Int32 iRows;
    	private Int32 xStart;
    	private Int32 yStart;
    	private Int16 mapTyp;
    	private Int16 nClass;
    	private Int16 iAUTyp;
    	private Single acre;
    	private Single xMap;
    	private Single yMap;
    	private Single xCell;
    	private Single yCell;

    	/// <summary>
	    /// IPACK (bytes 7:8) indicates the pack type of the data.
    	/// </summary>
    	public Int16 IPack
    	{
    		get {
    			return iPack;
    		}
    		set {
    			if ((value == IPack_8bit) || (value == IPack_4bit)
    			                          || (value == IPack_16bit))
    				iPack = value;
    			else
    				throw new ArgumentException("IPack must be IPack_8bit, IPack_4bit, or IPack16bit");
    		}
    	}
        public const Int16 IPack_8bit =  0;
        public const Int16 IPack_4bit =  1;
        public const Int16 IPack_16bit = 2;

		/// <summary>
    	/// NBANDS (bytes 9:10) indicates the number of bands/channels per line.
    	/// </summary>
    	/// <remarks>
	    /// Always 1 for GIS files, = or > 1 for LAN files.
    	/// </remarks>
    	public Int16 NBands
    	{
    		get {
    			return nBands;
    		}
    		set {
    			if (value > 0)
    				nBands = value;
    			else
    				throw new ArgumentException("NBands must be > 0");
    		}
    	}

    	/// <summary>
	    /// ICOLS (bytes 17:20) specifies the width of the file in pixels.
    	/// </summary>
    	public Int32 ICols
    	{
    		get {
    			return iCols;
    		}
    		set {
    			if (value >= 0)
    				iCols = value;
    			else
    				throw new ArgumentException("ICols must be = or > 0");
    		}
    	}

    	/// <summary>
	    /// IROWS (bytes 21:24) specifies the length of the file in lines of
		///	pixels.
    	/// </summary>
    	public Int32 IRows
    	{
    		get {
    			return iRows;
    		}
    		set {
    			if (value >= 0)
    				iRows = value;
    			else
    				throw new ArgumentException("IRows must be = or > 0");
    		}
    	}

    	/// <summary>
	    /// XSTART (bytes 25:28) specifies the database x-coordinate of the
	    /// first pixel (upper left) in the file.
    	/// </summary>
    	public Int32 XStart
    	{
    		get {
    			return xStart;
    		}
    		set {
   				xStart = value;
    		}
    	}

    	/// <summary>
	    /// YSTART (bytes 29:32) specifies the database y-coordinate of the
	    /// first pixel (upper left) in the file.
    	/// </summary>
    	public Int32 YStart
    	{
    		get {
    			return yStart;
    		}
    		set {
   				yStart = value;
    		}
    	}

    	/// <summary>
	    /// MAPTYP (bytes 89:90) indicates the type of map projection
	    /// associated with the file.  See Projections class.
    	/// </summary>
    	public Int16 MapTyp
    	{
    		get {
    			return mapTyp;
    		}
    		set {
   				mapTyp = value;
    		}
    	}

    	/// <summary>
	    /// NCLASS (bytes 91:92) indicates the number of classes in the data
	    /// set.
    	/// </summary>
    	public Int16 NClass
    	{
    		get {
    			return nClass;
    		}
    		set {
    			if (value >= 0)
    				nClass = value;
    			else
    				throw new ArgumentException("NClass must be = or > 0");
    		}
    	}

    	/// <summary>
    	/// IAUTYP (bytes 107:108) indicates the unit of area associated with
		///	each pixel:
		/// <pre>
		/// 0 = NONE
		/// 1 = ACRE
		/// 2 = HECTARE
		/// 3 = OTHER
		/// </pre>
    	/// </summary>
    	public Int16 IAUTyp
    	{
    		get {
    			return iAUTyp;
    		}
    		set {
    			if ((value == IAUTyp_NONE) || (value == IAUTyp_ACRE)
						    			   || (value == IAUTyp_HECTARE)
						    			   || (value == IAUTyp_OTHER))
    				iAUTyp = value;
    			else
    				throw new ArgumentException("IAUTyp must be IAUTyp_NONE, IAUTyp_ACRE, IAUType_HECTARE or IAUTyp_OTHER");
    		}
    	}
    	public const Int16 IAUTyp_NONE = 0;
    	public const Int16 IAUTyp_ACRE = 1;
    	public const Int16 IAUTyp_HECTARE = 2;
    	public const Int16 IAUTyp_OTHER = 3;

    	/// <summary>
    	/// ACRE (bytes 109:112) specifies the number of area units
    	/// represented by each pixel, in the units given in IAUTYP.
    	/// </summary>
    	public Single Acre
    	{
    		get {
    			return acre;
    		}
    		set {
    			if (value >= 0.0)
    				acre = value;
    			else
    				throw new ArgumentException("Acre must be = > 0.0");
    		}
    	}

    	/// <summary>
    	/// XMAP (bytes 113:116) gives the map x-coordinate for the upper
    	/// left corner pixel in the file.
    	/// </summary>
    	public Single XMap
    	{
    		get {
    			return xMap;
    		}
    		set {
    			xMap = value;
    		}
    	}

    	/// <summary>
    	/// YMAP (bytes 117:120) gives the map y-coordinate for the upper
    	/// left corner pixel in the file.
    	/// </summary>
    	public Single YMap
    	{
    		get {
    			return yMap;
    		}
    		set {
    			yMap = value;
    		}
    	}

    	/// <summary>
    	/// XCELL (bytes 121:124) gives the x size of each pixel.
    	/// </summary>
    	/// <remarks>
    	/// Units depend upon the map type specified in MAPTYP:
    	/// <pre>
    	/// State Plane = feet
    	/// Lat/Lon = degrees
    	/// all others = meters
    	/// </pre>
    	/// XCELL is 0 if MAPTYP is “none”.
    	/// </remarks>
    	public Single XCell
    	{
    		get {
    			return xCell;
    		}
    		set {
    			if (value >= 0.0)
    				xCell = value;
    			else
    				throw new ArgumentException("XCell must be = or > 0.0");
    		}
    	}

    	/// <summary>
    	/// YCELL (bytes 125:128) gives the y size of each pixel, in the same
    	/// units as XCELL.
    	/// </summary>
    	public Single YCell
    	{
    		get {
    			return yCell;
    		}
    		set {
    			if (value >= 0.0)
    				yCell = value;
    			else
    				throw new ArgumentException("YCell must be = or > 0.0");
    		}
    	}

        /// <summary>
        /// Initializes a new instance with all fields set to 0.
        /// </summary>
        public ImageHeader()
        {
        }

        /// <summary>
        /// Initializes a new instance by reading a header from a binary
        /// stream.
        /// </summary>
        public ImageHeader(BinaryReader reader)
        {
        	byte[] buffer = reader.ReadBytes(Size);
        	if (buffer.Length != Size)
        		throw new EndOfStreamException("Could not read header record");

            if ((buffer[0] != (byte)'H') ||
        	    (buffer[1] != (byte)'E') ||
        	    (buffer[2] != (byte)'A') ||
        	    (buffer[3] != (byte)'D') ||
        	    (buffer[4] != (byte)'7') ||
        	    (buffer[5] != (byte)'4'))
            	throw new ApplicationException("Header does not start with \"HEAD74\"");

       		//	IPACK - bytes 7:8
			iPack = BitConverter.ToInt16(buffer, 6);

       		//	NBANDS - bytes 9:10
			nBands = BitConverter.ToInt16(buffer, 8);

			//	ICOLS - bytes 17:20
			iCols = BitConverter.ToInt32(buffer, 16);

			//	IROWS - bytes 21:24
			iRows = BitConverter.ToInt32(buffer, 20);

			//	XSTART - bytes 25:28
			xStart = BitConverter.ToInt32(buffer, 24);

			//	YSTART - bytes 29:32
			yStart = BitConverter.ToInt32(buffer, 28);

			//	MAPTYP - bytes 89:90
			mapTyp = BitConverter.ToInt16(buffer, 88);

			//	NCLASS - bytes 91:92
			nClass = BitConverter.ToInt16(buffer, 90);

			//	IAUTYP - bytes 107:108
			iAUTyp = BitConverter.ToInt16(buffer, 106);

			//	ACRE - bytes 109:112
			acre = BitConverter.ToSingle(buffer, 108);

			//	XMAP - bytes 113:116
			xMap = BitConverter.ToSingle(buffer, 112);

			//	YMAP - bytes 117:120
			yMap = BitConverter.ToSingle(buffer, 116);

			//	XCELL - bytes 121:124
			xCell = BitConverter.ToSingle(buffer, 120);

			//	YCELL - bytes 125:128
			yCell = BitConverter.ToSingle(buffer, 124);
        }

        /// <summary>
        ///	Used for writing headers
        /// </summary>
        private static byte[] bufferWithZeros;

        static ImageHeader()
        {
        	bufferWithZeros = new byte[Size];
        	bufferWithZeros[0] = (byte) 'H';
        	bufferWithZeros[1] = (byte) 'E';
        	bufferWithZeros[2] = (byte) 'A';
        	bufferWithZeros[3] = (byte) 'D';
        	bufferWithZeros[4] = (byte) '7';
        	bufferWithZeros[5] = (byte) '4';
        }

        /// <summary>
        /// Writes the header to a binary stream.
        /// </summary>
        public void Write(BinaryWriter writer)
        {
        	//	"HEAD74"
            writer.Write(bufferWithZeros, 0, 6);

       		//	IPACK - bytes 7:8
			writer.Write(iPack);

       		//	NBANDS - bytes 9:10
			writer.Write(nBands);

			//	Unused - bytes 11:16
            writer.Write(bufferWithZeros, 10, 16-11+1);

			//	ICOLS - bytes 17:20
			writer.Write(iCols);

			//	IROWS - bytes 21:24
			writer.Write(iRows);

			//	XSTART - bytes 25:28
			writer.Write(xStart);

			//	YSTART - bytes 29:32
			writer.Write(yStart);

			//	Unused - bytes 33:88
            writer.Write(bufferWithZeros, 32, 88-33+1);

			//	MAPTYP - bytes 89:90
			writer.Write(mapTyp);

			//	NCLASS - bytes 91:92
			writer.Write(nClass);

			//	Unused - bytes 93:106
            writer.Write(bufferWithZeros, 92, 106-93+1);

			//	IAUTYP - bytes 107:108
			writer.Write(iAUTyp);

			//	ACRE - bytes 109:112
			writer.Write(acre);

			//	XMAP - bytes 113:116
			writer.Write(xMap);

			//	YMAP - bytes 117:120
			writer.Write(yMap);

			//	XCELL - bytes 121:124
			writer.Write(xCell);

			//	YCELL - bytes 125:128
			writer.Write(yCell);
        }
    }
}
