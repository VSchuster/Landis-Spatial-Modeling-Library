using Landis.SpatialModeling;

namespace LandisII.Examples
{
    public class EcoregionGrid : Grid, IIndexableGrid<bool>
    {
        private int[,] codes;

        public EcoregionGrid(int[,] ecoregionCodes)
            : base(ecoregionCodes.GetLength(0), ecoregionCodes.GetLength(1))
        {
            codes = ecoregionCodes;
        }

#region IIndexableGrid<bool> members

        bool IIndexableGrid<bool>.this[int row, int column]
        {
            get {
                return codes[row-1, column-1] > 0;
            }
            set {
                throw new System.InvalidOperationException();
            }
        }

        bool IIndexableGrid<bool>.this[Location location]
        {
            get {
                return (this as IIndexableGrid<bool>)[location.Row, location.Column];
            }
            set {
                throw new System.InvalidOperationException();
            }
        }

#endregion
    }
}
