using UnityEngine;

namespace Project.Level
{
    public interface IGridBuilder
    {
        IGridCell[,] CreateGrid(Vector2Int size);
    }
}