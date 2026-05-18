using System.Collections.Generic;

namespace WindowsFormsApp1.Core
{
    
    public class CellImageResolver
    {
        private readonly Dictionary<string, System.Drawing.Image> _cellImages;

        public CellImageResolver(Dictionary<string, System.Drawing.Image> cellImages)
        {
            _cellImages = cellImages;
        }

        public System.Drawing.Image Resolve(Cell cell)
        {
            if (cell.IsFlagged && !cell.IsRevealed)
                return _cellImages["flag"];

            if (!cell.IsRevealed)
                return _cellImages["field_0"];

            if (cell.IsMine)
                return _cellImages["mine"];

            if (cell.NeighborMineCount > 0)
                return _cellImages[$"field_{cell.NeighborMineCount}"];

            return _cellImages["ground"];
        }
    }
}