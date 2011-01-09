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
    /// An input raster file from which pixel data are read.  Pixels are read
    /// in row-major order, from the upper-left corner to the lower-right
    /// corner.
    /// </summary>
    public class InputRaster<TPixel>
        : InputRaster, IInputRaster<TPixel>
        where TPixel : IPixel, new()
    {
        private BinaryReader reader;
        private TPixel pixel;
        private delegate IInputBand MakeInputBandMethod(IPixelBand band);
        private IInputBand[] inputBands;
        private int pixelsInBuffers;
        private bool disposed;

        //---------------------------------------------------------------------

        public InputRaster(string filename)
            : base(filename)
        {
            pixel = new TPixel();
            disposed = false;

            FileStream stream = new FileStream(filename, FileMode.Open);
            try {
                reader = new BinaryReader(stream);
                ImageHeader header = new ImageHeader(reader);

                if (header.IRows < 0)
                    throw new ApplicationException(string.Format("# of rows in header is {0:#,0} (0x{0:X8})",
                                                                 header.IRows));
                if (header.ICols < 0)
                    throw new ApplicationException(string.Format("# of columns in header is {0:#,0} (0x{0:X8})",
                                                                 header.ICols));
                Dimensions = new Dimensions(header.IRows, header.ICols);

                MakeInputBandMethod makeInputBand;
                switch (header.IPack) {
                    case ImageHeader.IPack_8bit:
                        makeInputBand = Make8BitInputBand;
                        break;

                    case ImageHeader.IPack_16bit:
                        makeInputBand = Make16BitInputBand;
                        break;

                    case ImageHeader.IPack_4bit:
                        throw new ApplicationException("Raster has 4-bit pixel data which is not supported.");

                    default:
                        throw new ApplicationException(string.Format("Raster has unknown packing format: code = {0:#,0} (0x{0:X4}))",
                                                                     header.IPack));
                }

                if (header.NBands < 1)
                    throw new ApplicationException(string.Format("# of bands in header is {0:#,0} (0x{0:X4})",
                                                                 header.NBands));
                if (header.NBands != pixel.BandCount)
                    throw new ApplicationException(string.Format("Expected {0} band{1} but raster data has {2} band{3}",
                                                                 pixel.BandCount, (pixel.BandCount == 1 ? "" : "s"),
                                                                 header.NBands, (header.NBands == 1 ? "" : "s")));

                inputBands = new IInputBand[header.NBands];
                int i = 0;
                try {
                    for (i = 0; i < inputBands.Length; ++i) {
                        inputBands[i] = makeInputBand(pixel[i]);
                    }
                }
                catch (BandTypeException exc) {
                    //    Put band # into exception message (#s start at 1 for user)
                    throw new ApplicationException(string.Format(exc.Message, i+1));
                }
                pixelsInBuffers = 0;

                Metadata = Erdas74.Metadata.Create(header);
            }
            catch {
                if (reader == null) {
                    //    Then exception must have been thrown by the
                    //    BinaryReader ctor, so close file stream.
                    stream.Close();
                }
                Close();
                throw;
            }
        }

        //---------------------------------------------------------------------

        //    Erdas 7.4 documentation isn't clear whether data is signed or
        //    unsigned.  So for flexibility, we'll treat according the
        //  pixel's band type.  If it's unsigned, then treat the raster
        //    data as unsigned.  If it's signed, then treat the data as
        //    signed.

        private IInputBand Make8BitInputBand(IPixelBand pixelBand)
        {
            if (pixelBand is IPixelBandValue<byte>)
                return NewU8BitInputBand<byte>(pixelBand, Convert.ToByte);

            else if (pixelBand is IPixelBandValue<sbyte>)
                return New8BitInputBand<sbyte>(pixelBand, Convert.ToSByte);

            else if (pixelBand is IPixelBandValue<short>)
                return New8BitInputBand<short>(pixelBand, Convert.ToInt16);

            else if (pixelBand is IPixelBandValue<ushort>)
                return NewU8BitInputBand<ushort>(pixelBand, Convert.ToUInt16);

            else if (pixelBand is IPixelBandValue<int>)
                return New8BitInputBand<int>(pixelBand, Convert.ToInt32);

            else if (pixelBand is IPixelBandValue<uint>)
                return NewU8BitInputBand<uint>(pixelBand, Convert.ToUInt32);

            else if (pixelBand is IPixelBandValue<float>)
                return New8BitInputBand<float>(pixelBand, Convert.ToSingle);

            else if (pixelBand is IPixelBandValue<double>)
                return New8BitInputBand<double>(pixelBand, Convert.ToDouble);

            throw new ArgumentException("Unknown type for pixel band");
        }

        //---------------------------------------------------------------------

        private IInputBand New8BitInputBand<TPixelBand>(IPixelBand                            pixelBand,
                                                        ConvertPixelMethod<sbyte, TPixelBand> convertMethod)
            where TPixelBand : struct
        {
            return new InputBand<sbyte, TPixelBand>((int) Dimensions.Columns,
                                                    reader.ReadSByte,
                                                    pixelBand,
                                                    convertMethod);
        }

        //---------------------------------------------------------------------

        private IInputBand NewU8BitInputBand<TPixelBand>(IPixelBand                           pixelBand,
                                                         ConvertPixelMethod<byte, TPixelBand> convertMethod)
            where TPixelBand : struct
        {
            return new InputBand<byte, TPixelBand>((int) Dimensions.Columns,
                                                   reader.ReadByte,
                                                   pixelBand,
                                                   convertMethod);
        }

        //---------------------------------------------------------------------

        private BandTypeException BandTypeMismatch(TypeCode rasterBandType,
                                                   TypeCode pixelBandType)
        {
            string supportedTypesDesc = BandType.GetDescription(pixelBandType);
            if (pixelBandType != TypeCode.SByte && pixelBandType != TypeCode.Byte)
                supportedTypesDesc = supportedTypesDesc + " and smaller data types";
            return new BandTypeException("The application supports {0} for band {{0}} but the data in the raster file are {1}.",
                                         supportedTypesDesc, BandType.GetDescription(rasterBandType));
        }

        //---------------------------------------------------------------------

        private IInputBand Make16BitInputBand(IPixelBand pixelBand)
        {
            if (pixelBand is IPixelBandValue<byte>)
                throw BandTypeMismatch(TypeCode.Int16, TypeCode.Byte);

            else if (pixelBand is IPixelBandValue<sbyte>)
                throw BandTypeMismatch(TypeCode.Int16, TypeCode.SByte);

            else if (pixelBand is IPixelBandValue<short>)
                return New16BitInputBand<short>(pixelBand, Convert.ToInt16);

            else if (pixelBand is IPixelBandValue<ushort>)
                return NewU16BitInputBand<ushort>(pixelBand, Convert.ToUInt16);

            else if (pixelBand is IPixelBandValue<int>)
                return New16BitInputBand<int>(pixelBand, Convert.ToInt32);

            else if (pixelBand is IPixelBandValue<uint>)
                return NewU16BitInputBand<uint>(pixelBand, Convert.ToUInt32);

            else if (pixelBand is IPixelBandValue<float>)
                return New16BitInputBand<float>(pixelBand, Convert.ToSingle);

            else if (pixelBand is IPixelBandValue<double>)
                return New16BitInputBand<double>(pixelBand, Convert.ToDouble);

            throw new ArgumentException("Unknown type for pixel band");
        }

        //---------------------------------------------------------------------

        private IInputBand New16BitInputBand<TPixelBand>(IPixelBand                            pixelBand,
                                                         ConvertPixelMethod<short, TPixelBand> convertMethod)
            where TPixelBand : struct
        {
            return new InputBand<short, TPixelBand>((int) Dimensions.Columns,
                                                    reader.ReadInt16,
                                                    pixelBand,
                                                    convertMethod);
        }

        //---------------------------------------------------------------------

        private IInputBand NewU16BitInputBand<TPixelBand>(IPixelBand                             pixelBand,
                                                          ConvertPixelMethod<ushort, TPixelBand> convertMethod)
            where TPixelBand : struct
        {
            return new InputBand<ushort, TPixelBand>((int) Dimensions.Columns,
                                                     reader.ReadUInt16,
                                                     pixelBand,
                                                     convertMethod);
        }

        //---------------------------------------------------------------------

        ///    <summary>
        /// Reads the next pixel from the raster.
        /// </summary>
        /// <remarks>
        /// The method returns the same mutable pixel object for each call.
        /// The pixel's band data are updated with each call.
        /// </remarks>
        public TPixel ReadPixel()
        {
            if (disposed)
                throw new ObjectDisposedException(null);
            IncrementPixelsRead();

            int i = 0;
            if (pixelsInBuffers == 0) {
                try {
                    for (i = 0; i < inputBands.Length; ++i)
                        inputBands[i].ReadData();
                }
                catch (EndOfStreamException) {
                    throw new EndOfStreamException(string.Format("Raster is missing pixel data for band {0} in row {1}",
                                                                 i + 1, (PixelsRead / Dimensions.Columns) + 1));
                }
                pixelsInBuffers = (int) Dimensions.Columns;
            }
            for (i = 0; i < inputBands.Length; ++i)
                inputBands[i].AssignNextPixel();
            pixelsInBuffers--;

            return pixel;
        }

        //---------------------------------------------------------------------

        protected override void Dispose(bool disposeManaged)
        {
            if (! disposed) {
                if (disposeManaged) {
                    if (reader != null)
                        reader.Close();
                }
                disposed = true;
                base.Dispose(disposeManaged);
            }
        }
    }
}
