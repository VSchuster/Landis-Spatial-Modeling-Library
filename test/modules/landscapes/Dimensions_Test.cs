// Copyright 2004-2006 University of Wisconsin
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

using NUnit.Framework;
using Wisc.Flel.GeospatialModeling.Landscapes;

using GridDimensions = Wisc.Flel.GeospatialModeling.Grids.Dimensions;

//  CS0649 warning is generated by the field "dims" not being assigned.
//  CS1718 warning is generated by the tests of == operator using the same
//  variable as both operands
#pragma warning disable 649, 1718

namespace Wisc.Flel.Test.GeospatialModeling.Landscapes
{
    [TestFixture]
    public class Dimensions_Test
    {
        Dimensions dims;
        Dimensions dims_4321_789;

        //---------------------------------------------------------------------

        [TestFixtureSetUp]
        public void Init()
        {
            dims_4321_789 = new Dimensions(4321, 789);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DefaultCtorCheckRows()
        {
            Assert.AreEqual(0, dims.Rows);
        }

        //---------------------------------------------------------------------

        [Test]
        public void DefaultCtorCheckColumns()
        {
            Assert.AreEqual(0, dims.Columns);
        }

        //---------------------------------------------------------------------

        [Test]
        public void CheckRows()
        {
            Assert.AreEqual(4321, dims_4321_789.Rows);
        }

        //---------------------------------------------------------------------

        [Test]
        public void CheckColumns()
        {
            Assert.AreEqual(789, dims_4321_789.Columns);
        }

        //---------------------------------------------------------------------

        [Test]
        public void EqualityWithSameLoc()
        {
            Assert.IsTrue(dims == dims);
            Assert.IsTrue(dims_4321_789 == dims_4321_789);
        }

        //---------------------------------------------------------------------

        [Test]
        public void EqualityWithDiffLocs()
        {
            Assert.IsFalse(dims == dims_4321_789);
        }

        //---------------------------------------------------------------------

        [Test]
        public void InequalityWithSameLoc()
        {
            Assert.IsFalse(dims != dims);
            Assert.IsFalse(dims_4321_789 != dims_4321_789);
        }

        //---------------------------------------------------------------------

        [Test]
        public void InequalityWithDiffLocs()
        {
            Assert.IsTrue(dims != dims_4321_789);
        }

        //---------------------------------------------------------------------

        [Test]
        public void EqualsWithNull()
        {
            Assert.IsFalse(dims.Equals(null));
        }

        //---------------------------------------------------------------------

        [Test]
        public void EqualsWithSameObj()
        {
            Assert.IsTrue(dims.Equals(dims));
            Assert.IsTrue(dims_4321_789.Equals(dims_4321_789));
        }

        //---------------------------------------------------------------------

        [Test]
        public void EqualsWithDiffObj()
        {
            Assert.IsFalse(dims.Equals(dims_4321_789));
        }

        //---------------------------------------------------------------------

        [Test]
        public void EqualsWithDiffObjSameValue()
        {
            Dimensions dims_0_0 = new Dimensions();
            Assert.IsTrue(dims.Equals(dims_0_0));
            Assert.IsTrue(dims_4321_789.Equals(new Dimensions(4321, 789)));
        }

        //---------------------------------------------------------------------

        [Test]
        public void ConversionToGridDims()
        {
            //  Implicit conversion during assignment
            GridDimensions gridDimensions = dims_4321_789;
            CheckGridDimensions(gridDimensions, dims_4321_789.Rows, dims_4321_789.Columns);

            //  Implicit conversion during method call
            CheckGridDimensions(dims_4321_789, dims_4321_789.Rows, dims_4321_789.Columns);
        }


        //---------------------------------------------------------------------

        private void CheckGridDimensions(GridDimensions gridDimensions,
                                         int            expectedRows,
                                         int            expectedColumns)
        {
            Assert.AreEqual(expectedRows,    gridDimensions.Rows);
            Assert.AreEqual(expectedColumns, gridDimensions.Columns);
        }

        //---------------------------------------------------------------------

        [Test]
        public void ConversionFromGridDims()
        {
            //  Implicit conversion during assignment
            GridDimensions gridDimensions = new GridDimensions(987654321, 707);
            Dimensions dimensions = gridDimensions;
            CheckDimensions(dimensions, gridDimensions.Rows, gridDimensions.Columns);

            //  Implicit conversion during method call
            CheckDimensions(gridDimensions, gridDimensions.Rows, gridDimensions.Columns);
        }

        //---------------------------------------------------------------------

        private void CheckDimensions(Dimensions dimensions,
                                     int        expectedRows,
                                     int        expectedColumns)
        {
            Assert.AreEqual(expectedRows,    dimensions.Rows);
            Assert.AreEqual(expectedColumns, dimensions.Columns);
        }

        //---------------------------------------------------------------------

        [Test]
        public void HashCode()
        {
            Assert.AreEqual((int)(0 ^ 0), dims.GetHashCode());
            Assert.AreEqual((int)((uint)4321 ^ (uint)789),
                            dims_4321_789.GetHashCode());
        }

        //---------------------------------------------------------------------

        [Test]
        public void ToStringMethod()
        {
            Assert.AreEqual("0 rows by 0 columns", dims.ToString());
            Assert.AreEqual("4,321 rows by 789 columns",
                            dims_4321_789.ToString());
            Assert.AreEqual("1 row by 1 column",
                            new Dimensions(1, 1).ToString());
            Assert.AreEqual("5 rows by 1 column",
                            new Dimensions(5, 1).ToString());
            Assert.AreEqual("1 row by 66 columns",
                            new Dimensions(1, 66).ToString());
        }
    }
}

#pragma warning restore 649, 1718
