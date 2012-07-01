// Copyright 2010 Green Code LLC
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

using OSGeo.GDAL;

namespace Landis.SpatialModeling.CoreServices.RasterIO
{
    public static class GdalSystem
    {
        static GdalSystem()
        {
            Gdal.AllRegister();
        }

        public static void Initialize()
        {
            // Do nothing.  The initialization occurs on the first call.
        }
    }
}
