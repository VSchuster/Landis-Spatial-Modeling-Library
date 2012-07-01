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

using Landis.SpatialModeling;

namespace Landis.Landscapes
{
    /// <summary>
    /// A site variable where the inactive sites share a common value.
    /// </summary>
    public class SiteVarShare<T>
        : SiteVar<T>, ISiteVar<T>
    {
        private T[] values;

        //---------------------------------------------------------------------

        public InactiveSiteMode Mode
        {
            get {
                return InactiveSiteMode.Share1Value;
            }
        }

        //---------------------------------------------------------------------

        public T this[Site site]
        {
            get {
                Validate(site);
                return values[site.DataIndex];
            }

            set {
                Validate(site);
                values[site.DataIndex] = value;
            }
        }

        //---------------------------------------------------------------------

        public T ActiveSiteValues
        {
            set {
                for (int i = 1; i <= Landscape.ActiveSiteCount; i++)
                    values[i] = value;
            }
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Sets all the values for the inactive sites to the same value.
        /// </summary>
        public T InactiveSiteValues
        {
            set {
                if (Landscape.InactiveSiteCount > 0)
                    values[Landscape.InactiveSiteDataIndex] = value;
            }
        }

        //---------------------------------------------------------------------

        public T SiteValues
        {
            set {
                for (int i = 0; i < values.Length; i++)
                    values[i] = value;
            }
        }

        //---------------------------------------------------------------------

        public SiteVarShare(ILandscape landscape)
            : base(landscape)
        {
            values = new T[landscape.ActiveSiteCount + 1];
        }
    }
}
