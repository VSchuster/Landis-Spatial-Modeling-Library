// Copyright 2005-2006 University of Wisconsin
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

namespace Landis.SpatialModeling.CoreServices.Landscapes
{
    /// <summary>
    /// A site variable where the inactive sites have distinct value.
    /// </summary>
    public class SiteVarDistinct<T>
        : SiteVar<T>, ISiteVar<T>
    {
        private T[,] values;

        //---------------------------------------------------------------------

        public InactiveSiteMode Mode
        {
            get {
                return InactiveSiteMode.DistinctValues;
            }
        }

        //---------------------------------------------------------------------

        public T this[Site site]
        {
            get {
                Validate(site);
                return values[site.Location.Row-1, site.Location.Column-1];
            }

            set {
                Validate(site);
                values[site.Location.Row-1, site.Location.Column-1] = value;
            }
        }

        //---------------------------------------------------------------------

        public T ActiveSiteValues
        {
            set {
                foreach (ActiveSite site in Landscape.ActiveSites)
                    this[site] = value;
            }
        }

        //---------------------------------------------------------------------

        public T InactiveSiteValues
        {
            set {
                foreach (Site site in Landscape.AllSites)
                    if (! site.IsActive)
                        this[site] = value;
            }
        }

        //---------------------------------------------------------------------

        public T SiteValues
        {
            set {
                foreach (Site site in Landscape.AllSites)
                    this[site] = value;
            }
        }

        //---------------------------------------------------------------------

        public SiteVarDistinct(ILandscape landscape)
            : base(landscape)
        {
            values = new T[landscape.Rows, landscape.Columns];
        }
    }
}
