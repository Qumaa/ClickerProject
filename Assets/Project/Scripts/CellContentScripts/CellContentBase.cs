using System.Collections.Generic;
using UnityEngine;

public abstract class CellContentBase : MonoBehaviour, ICellContent
{
    protected List<ICellContent> _cellsContents;

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