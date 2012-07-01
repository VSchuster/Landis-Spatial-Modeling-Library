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

using System.Collections;
using System.Collections.Generic;

namespace Landis.SpatialModeling.CoreServices.Landscapes
{
    /// <summary>
    /// Enumerator for all the sites in a landscape.
    /// </summary>
    public class SiteEnumerator
        : EnumeratorBase, IEnumerator<Site>
    {
        private ILandscape landscape;
        private Site currentSite;
        private ActiveSiteEnumerator activeSiteEtor;
        private ActiveSite nextActiveSite;

        //---------------------------------------------------------------------

        internal SiteEnumerator(ILandscape landscape)
            : base()
        {
            this.landscape = landscape;
            activeSiteEtor = landscape.ActiveSites.GetEnumerator() as ActiveSiteEnumerator;
        }

        //---------------------------------------------------------------------

        public Site Current
        {
            get {
                return currentSite;
            }
        }

        //---------------------------------------------------------------------

        object IEnumerator.Current
        {
            get {
                return currentSite;
            }
        }

        //---------------------------------------------------------------------

        public bool MoveNext()
        {
            if (CurrentState == State.AfterLast)
                return false;

            if (CurrentState == State.BeforeFirst) {
                if (landscape.SiteCount == 0)
                    return MoveNextReachedEnd();

                currentSite = landscape.GetSite(1, 1);
                nextActiveSite = GetNext(activeSiteEtor);

                //  If the first active site is (1, 1), then get the next
                //  active site.
                if (nextActiveSite == currentSite)
                    nextActiveSite = GetNext(activeSiteEtor);

                return MoveNextSucceeded();
            }

            //  CurrentState == State.InCollection
            Location nextLocation = RowMajor.Next(currentSite.Location, landscape.Columns);
            if (nextLocation.Row > landscape.Rows) {
                currentSite = new Site();
                return MoveNextReachedEnd();
            }

            //  We have a valid site location; is the site active or not?
            if (nextLocation == nextActiveSite.Location) {
                currentSite = nextActiveSite;
                nextActiveSite = GetNext(activeSiteEtor);
            }
            else {
                currentSite = InactiveSite.Create(landscape, nextLocation);
            }
            return MoveNextSucceeded();
        }

        //---------------------------------------------------------------------

        private ActiveSite GetNext(ActiveSiteEnumerator activeSiteEnumerator)
        {
            if (activeSiteEnumerator.MoveNext())
                return activeSiteEnumerator.Current;
            else
                return new ActiveSite();
        }

        //---------------------------------------------------------------------

        public override void Reset()
        {
            base.Reset();
            if (activeSiteEtor != null)
                activeSiteEtor.Reset();
        }
    }
}
