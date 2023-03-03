using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Level
{
    public abstract class CellContentBase : MonoBehaviour, IGridCellContent
    {
        protected List<IGridCellContent> _cellsContents;

        public abstract void Initialize();

        public virtual void Remove()
        {
            for (int i = 0; i < _cellsContents.Count; i++)
            {
                _cellsContents[i].Remove();
            }

            _cellsContents.Clear();
        }
    }
}