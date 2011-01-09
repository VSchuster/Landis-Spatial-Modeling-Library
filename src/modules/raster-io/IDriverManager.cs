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

namespace Wisc.Flel.GeospatialModeling.RasterIO
{
    /// <summary>
    /// Manages a collection of raster drivers.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A manager maintains a list of raster formats supported by its drivers.
    /// Although many drivers may support a particular format, each format has
    /// just one or two drivers associated with it.  If a format has one driver
    /// associated with it, that driver reads and writes rasters in that
    /// format.  If a format has two drivers associated with it, one driver is
    /// designated for reading rasters, and the other for writing rasters.
    /// </para>
    /// <para>
    /// When a manager opens or creates a raster, it examines the raster format
    /// specified in the path argument.  If no driver supports the format, then
    /// a <see cref="System.FormatException">FormatException</see> is thrown.
    /// </para>
    /// </remarks>
    public interface IDriverManager
        : IRasterFactory
    {
    }
}
