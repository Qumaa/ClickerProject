using System.Collections.Generic;
using UnityEngine;

namespace Project.Level
{
    public class GridCell : IGridCell
    {
        private readonly List<IGridCellContent> _contents = new();

        public bool AddContent(IGridCellContent content)
        {
            if (ContainsContent(content)) return false;

            _contents.Add(content);
            return true;
        }

        public void ClearContent()
        {
            _contents.Clear();
        }

        public IGridCellContent[] GetContents() => _contents.ToArray();

        public bool RemoveContent(IGridCellContent target)
        {
            if (!ContainsContent(target)) return false;

            _contents.Remove(target);
            return true;
        }

        protected bool ContainsContent(IGridCellContent content) => _contents.Contains(content);
    }
}