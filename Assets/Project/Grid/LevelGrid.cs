using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Meta;

namespace Project.Level
{
    public class LevelGrid : MonoBehaviour, ILevelGrid, ILevelGridInternal
    {
        #region Values

        // Editor values

        // Internal values
        private Vector2Int _gridSizeInternal = new(1, 1);
        private IGridCell[,] _gridCells;

        // References
        IGridBuilder _gridBuilder;

        // Properties
        public Vector2Int Size
        {
            get => _gridSizeInternal;
            private set => SetGridSize(value);
        }

        private void SetGridSize(Vector2Int newSize)
        {
            var gridMaxSize = Vector2Int.one;

            _gridSizeInternal = Vector2Int.Max(newSize, gridMaxSize);
        }

        #endregion

        #region Mono and inherited methods

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            // get references
            _gridBuilder = new GridBuilder();

            // set up values
            _gridCells = _gridBuilder.CreateGrid(_gridSizeInternal);
        }

        #endregion

        #region LevelGrid logic

        public bool ContainsPosition(Vector2Int cellPos)
        {
            Vector2Int min = Vector2Int.zero;

            return RangeCheck(cellPos.x, min.x, Size.x) && RangeCheck(cellPos.y, min.y, Size.y);

            static bool RangeCheck(int num, int min, int max) =>
                (num >= min) && (num < max);
        }
        public IGridCell GetCell(Vector2Int cellPos) =>
            ContainsPosition(cellPos) ? _gridCells[cellPos.x, cellPos.y] : throw new CellOutPfGridException(cellPos);
        public Vector3 CellToWorld(Vector2Int cellPos) =>
            ((ILevelGridInternal)this).GetGridBottomCorner() + cellPos.XYtoXoY();
        public Vector2Int WorldToCell(Vector3 worldPos)
        {
            var localPos = transform.InverseTransformPoint(worldPos) + ((Vector2)Size / 2).XYtoXoY();

            return localPos.XYZtoXZint();
        }
        public void AddContent(Vector2Int cellPos, IGridCellContent content) =>
            GetCell(cellPos).AddContent(content);
        public void RemoveContent(Vector2Int cellPos, IGridCellContent content) =>
            GetCell(cellPos).RemoveContent(content);

        Vector3 ILevelGridInternal.GetGridBottomCorner() =>
            transform.position - ((Vector2)Size / 2).XYtoXoY();
        void ILevelGridInternal.Expand(Vector2 direction, int cells)
        {
            //if (cells == 0) return;
            //if (Mathf.Abs(cells) > 100) return;

            //var newSize = Size + (direction.ToIntVec2() * cells);

            //SetGridSize(newSize);
            //transform.position += direction.XYtoXoY() * (cells / 2f);

            Debug.Log($"Expanded {cells} cells in {direction} direction");
        }

        #endregion
    }

    public interface ILevelGridInternal : ILevelGrid
    {
        Vector3 GetGridBottomCorner();

        void Expand(Vector2 direction, int cells);
    }

    public interface ILevelGrid
    {
        Vector2Int Size { get; }
        bool ContainsPosition(Vector2Int pos);
        IGridCell GetCell(Vector2Int cellPos);
        Vector3 CellToWorld(Vector2Int cellPos);
        Vector2Int WorldToCell(Vector3 worldPos);
        void AddContent(Vector2Int cellPos, IGridCellContent content);
        void RemoveContent(Vector2Int cellPos, IGridCellContent content);
    }
}