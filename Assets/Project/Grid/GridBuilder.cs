using UnityEngine;


namespace Project.Level
{
    public class GridBuilder : IGridBuilder
    {
        public IGridCell[,] CreateGrid(Vector2Int size)
        {
            var cells = new IGridCell[size.x, size.y];

            for (int x = 0; x < size.x; x++)
                for (int y = 0; y < size.y; y++)
                    cells[x, y] = new GridCell();

            return cells;
        }
    }
}