// Copyright 2005-2006 University of Wisconsin
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
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

using Edu.Wisc.Forest.Flel.Util;
using NUnit.Framework;
using System;
using Wisc.Flel.GeospatialModeling.RasterIO;

namespace Wisc.Flel.Test.GeospatialModeling.RasterIO
{
    [TestFixture]
    public class SingleBandPixel_Test
    {
        public class FloatPixel
            : SingleBandPixel<float>
        {
            public FloatPixel()
                : base()
            {
            }

            public FloatPixel(float band0)
                : base(band0)
            {
            }
        }

        //---------------------------------------------------------------------

        [Test]
        public void FloatPixel_DefaultCtor()
        {
            FloatPixel pixel = new FloatPixel();
            CheckFloatPixel(pixel, 0.0f);
        }

        //---------------------------------------------------------------------

        [Test]
        public void FloatPixel_CtorWithArg()
        {
            float initialBandValue = -2.5e3f;
            FloatPixel pixel = new FloatPixel(initialBandValue);
            CheckFloatPixel(pixel, initialBandValue);
        }

        //---------------------------------------------------------------------

        private void CheckFloatPixel(FloatPixel pixel,
                                     float      band0)
        {
            Assert.AreEqual(1, pixel.BandCount);
            Assert.AreEqual(System.TypeCode.Single, pixel[0].TypeCode);
            Assert.AreEqual(band0, pixel.Band0);
        }

        //---------------------------------------------------------------------

        public struct MyStruct
        {
            public int foo;
            public double bar;
        }

        //---------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void BadBandType()
        {
            SingleBandPixel<MyStruct> pixel = new SingleBandPixel<MyStruct>();
        }
    }
}
