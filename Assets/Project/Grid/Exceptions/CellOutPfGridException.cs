using System;
using UnityEngine;

namespace Project.Level
{
    public class CellOutPfGridException : Exception
    {
        public CellOutPfGridException(Vector2Int pos) : base($"Cell position {pos} is out of grid")
        {
        }
    }
}