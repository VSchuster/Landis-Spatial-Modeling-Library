// Copyright 2004-2006 University of Wisconsin
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
//   James Domingo, UW-Madison, Forest Landscape Ecology Lab

using Landis.SpatialModeling;
using System.Diagnostics;

namespace Landis.Landscapes
{
    public abstract class SiteVar<T>
    {
        private ILandscape landscape;

        //---------------------------------------------------------------------

        public System.Type DataType
        {
            get {
                return typeof(T);
            }
        }

        //---------------------------------------------------------------------

        public ILandscape Landscape
        {
            get {
                return landscape;
            }
        }

        //---------------------------------------------------------------------

        protected SiteVar(ILandscape landscape)
        {
            this.landscape = landscape;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Validates that a site refers to the same landscape as the site
        /// variable was created for.
        /// </summary>
        protected void Validate(Site site)
        {
            Trace.Assert(site.Landscape == landscape);
        }
    }
}
