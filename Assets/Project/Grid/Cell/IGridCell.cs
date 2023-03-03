using UnityEngine;

namespace Project.Level
{
    public interface IGridCell
    {
        bool AddContent(IGridCellContent content);
        bool RemoveContent(IGridCellContent target);
        void ClearContent();
        IGridCellContent[] GetContents();
    }
}