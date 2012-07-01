// Copyright 2011 Green Code LLC
// All rights reserved.
//
// The copyright holders license this file under the New (3-clause) BSD
// License (the "License").  You may not use this file except in
// compliance with the License.  A copy of the License is available at
//
//   http://www.opensource.org/licenses/BSD-3-Clause
//
// and is included in the NOTICE.txt file distributed with this work.
//
// Contributors:
//   James Domingo, Green Code LLC

namespace Landis.SpatialModeling.CoreServices
{
    /// <summary>
    /// A raster factory whose configuration can be changed.
    /// </summary>
    public interface IConfigurableRasterFactory : IRasterFactory
    {
        /// <summary>
        /// Bind a file extension with a raster format.
        /// </summary>
        /// <param name="fileExtension">
        /// Must start with a period.
        /// </param>
        /// <param name="formatCode">
        /// A GDAL format code.  Format codes are listed in the "Code"
        /// column in the table at http://gdal.org/formats_list.html.  If
        /// null is specified, then the file extension becomes unbound.
        /// </param>
        void BindExtensionToFormat(string fileExtension,
                                   string formatCode);
    }
}
