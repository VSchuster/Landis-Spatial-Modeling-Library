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
using Wisc.Flel.GeospatialModeling.RasterIO;
using System;
using System.IO;

namespace Wisc.Flel.GeospatialModeling.RasterDrivers.Erdas74
{
	/// <summary>
	/// An output raster file to which pixel data are written.  Pixels are
	/// written in row-major order, from the upper-left corner to the
	/// lower-right corner.
	/// </summary>
	public class OutputRaster<TPixel>
		: OutputRaster, IOutputRaster<TPixel>
		where TPixel : IPixel, new()
	{
		private byte[][] bandBuffers;
		private BinaryWriter[] bufferWriters;
		private IOutputBand[] bands;
		private int pixelsInBuffers;
		private BinaryWriter fileWriter;
		private bool disposed;

		private delegate IOutputBand MakeBandMethod(IPixelBand   pixelBand,
		                                            BinaryWriter bufferWriter);

		//---------------------------------------------------------------------

		/// <exception cref="System.ArgumentException">
		/// TPixel has one band but the path's extension is not ".gis"; or
		/// TPixel has two or more bands but the path's extensions is not
		/// ".lan".
		/// </exception>
		/// <exception cref="BandTypeException">
		/// TPixel has one or more bands whose types that are larger than
		/// 16 bits.
		/// </exception>
		public OutputRaster(string     path,
		                    Dimensions dimensions,
		                    IMetadata  metadata)
			: base(path, dimensions, metadata)
		{
			disposed = false;
			TPixel pixel = new TPixel();

			if (pixel.BandCount == 1) {
				if (System.IO.Path.GetExtension(path) != ".gis")
					throw new ArgumentException(string.Format("Band count = 1, but the extension of the path \"{0}\" is not \".gis\".",
					                                          path));
			}
			else {
				if (System.IO.Path.GetExtension(path) != ".lan")
					throw new ArgumentException(string.Format("Band count = {1}, but the extension of the path \"{0}\" is not \".lan\".",
					                                          path, pixel.BandCount));
			}

			int bytesPerPixelBandValue = 1;  // 8 bits by default
			MakeBandMethod makeOutputBand = Make8BitOutputBand;
			for (int i = 0; i < pixel.BandCount; i++) {
				IPixelBand band = pixel[i];
				switch (band.TypeCode) {
					case TypeCode.Byte:
					case TypeCode.SByte:
						//	Default of 8 bits is sufficient.
						break;

					case TypeCode.Int16:
					case TypeCode.UInt16:
						bytesPerPixelBandValue = 2;
						makeOutputBand = Make16BitOutputBand;
						break;

					case TypeCode.Int32:
					case TypeCode.UInt32:
					case TypeCode.Single:
					case TypeCode.Double:
						throw new BandTypeException("Band {0} has {1}, but ERDAS GIS/LAN can only support up to 16-bit pixel data",
						                            i, BandType.GetDescription(band.TypeCode));
					default:
						throw new BandTypeException("Band {0} has an unknown type: {1}",
						                            i, band.TypeCode);
				}
			}

			int bufferSize = ((int) Dimensions.Columns) * bytesPerPixelBandValue;
			bandBuffers = new byte[pixel.BandCount][];
			for (int i = 0; i < pixel.BandCount; i++) {
				bandBuffers[i] = new byte[bufferSize];
			}

			bufferWriters = new BinaryWriter[pixel.BandCount];
			for (int i = 0; i < pixel.BandCount; i++) {
				bufferWriters[i] = new BinaryWriter(new MemoryStream(bandBuffers[i]));
			}

			bands = new IOutputBand[pixel.BandCount];
			for (int i = 0; i < pixel.BandCount; i++) {
				bands[i] = makeOutputBand(pixel[i], bufferWriters[i]);
			}
			pixelsInBuffers = 0;

			FileStream stream = new FileStream(path, FileMode.Create);
			try {
				fileWriter = new BinaryWriter(stream);

				ImageHeader header = new ImageHeader();
				if (bytesPerPixelBandValue == 1)
					header.IPack = ImageHeader.IPack_8bit;
				else  // 2 bytes
					header.IPack = ImageHeader.IPack_16bit;
				header.NBands = (short) pixel.BandCount;
				header.IRows = (int) Dimensions.Rows;
				header.ICols = (int) Dimensions.Columns;

				Erdas74.Metadata.SetFields(header, metadata);
				header.Write(fileWriter);
			}
			catch {
				if (fileWriter == null) {
					//	Then exception must have been thrown by the
					//	BinaryWriter ctor, so close file stream.
					stream.Close();
				}
				Close();
				throw;
			}
		}

		//---------------------------------------------------------------------

		//	Erdas 7.4 documentation isn't clear whether data is signed or
		//	unsigned.  So for flexibility, we'll treat according the
		//  pixel's band type.  If it's unsigned, then then raster data are
		//	written to the file as unsigned.  If it's signed, then data are as
		//	written as signed.

		private IOutputBand Make8BitOutputBand(IPixelBand   pixelBand,
		                                       BinaryWriter bufferWriter)
		{
			if (pixelBand is IPixelBandValue<byte>)
				return new OutputBand<byte, byte>(Convert.ToByte,
				                                  bufferWriter.Write);

			else if (pixelBand is IPixelBandValue<sbyte>)
				return new OutputBand<sbyte, sbyte>(Convert.ToSByte,
				                                    bufferWriter.Write);

			throw new ArgumentException("Pixel band does not implement IPixelBandValue<T> where T is byte or sbyte");
		}

		//---------------------------------------------------------------------

		private IOutputBand Make16BitOutputBand(IPixelBand   pixelBand,
		                                        BinaryWriter bufferWriter)
		{
			if (pixelBand is IPixelBandValue<byte>)
				return new OutputBand<byte, ushort>(Convert.ToUInt16,
				                                    bufferWriter.Write);

			else if (pixelBand is IPixelBandValue<sbyte>)
				return new OutputBand<sbyte, short>(Convert.ToInt16,
				                                    bufferWriter.Write);

			else if (pixelBand is IPixelBandValue<short>)
				return new OutputBand<short, short>(Convert.ToInt16,
				                                    bufferWriter.Write);

			else if (pixelBand is IPixelBandValue<ushort>)
				return new OutputBand<ushort, ushort>(Convert.ToUInt16,
				                                      bufferWriter.Write);

			throw new ArgumentException("Pixel band does not implement IPixelBandValue<T> where T is byte, sbyte, short or ushort");
		}

		//---------------------------------------------------------------------

		/// <summary>
		/// Writes the next pixel to the raster.
		/// </summary>
        /// <exception cref="System.ArgumentNullException">
		/// Pixel is null.
		/// </exception>
        /// <exception cref="System.InvalidOperationException">
		/// The number of pixels written is equal to the number of pixels
		/// allowed by the raster's dimensions.
		/// </exception>
		public void WritePixel(TPixel pixel)
		{
			if (pixel == null)
				throw new ArgumentNullException("'pixel' argument");
			if (disposed)
				throw new ObjectDisposedException(this.Path);

			IncrementPixelsWritten();
			for (int i = 0; i < bands.Length; i++)
				bands[i].AppendPixel(pixel[i]);

			pixelsInBuffers++;
			if (pixelsInBuffers == Dimensions.Columns) {
				foreach (BinaryWriter bufferWriter in bufferWriters)
					bufferWriter.Flush();
				foreach (byte[] buffer in bandBuffers)
					fileWriter.Write(buffer);
				foreach (BinaryWriter bufferWriter in bufferWriters)
					bufferWriter.Seek(0, SeekOrigin.Begin);
				pixelsInBuffers = 0;
			}
		}

		//---------------------------------------------------------------------

		protected override void Dispose(bool disposeManaged)
		{
			if (! disposed) {
				if (disposeManaged) {
					if (bufferWriters != null)
						foreach (BinaryWriter bufferWriter in bufferWriters) {
							if (bufferWriter != null)
								bufferWriter.Close();
						}
					if (fileWriter != null)
						fileWriter.Close();
				}
				disposed = true;
				base.Dispose(disposeManaged);
			}
		}
	}
}
