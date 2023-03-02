using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.INDEV
{
    public class LevelGrid : MonoBehaviour
    {
        #region Values
        // public: Name
        // private, protected: _name
        // small scope: name
        // events: OnName

        // Editor values
        [SerializeField] private Vector2Int _gridSize;

        // Internal values
        private Vector2Int _gridSizeInternal;

        // References


        // Properties


        #endregion

        #region Mono and inherited methods

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            // get references


            // set up values
            _gridSizeInternal = _gridSize;
        }

        private void Update()
        {

        }

        private void OnValidate()
        {
            ValidateGridSize();
        }

        private void ValidateGridSize()
        {
            _gridSize = Vector2Int.Max(_gridSize, Vector2Int.zero);

            CheckGridSizeUpdate(_gridSize);
        }

        private void CheckGridSizeUpdate(Vector2Int newGridSize)
        {
            if (newGridSize != _gridSizeInternal)
                GridSizeChanged(newGridSize);
        }

        private void GridSizeChanged(Vector2Int newSize)
        {
            _gridSizeInternal = newSize;
        }

        #endregion

        #region LevelGrid logic



        #endregion

        #region Math, shortcuts and utility



        #endregion

        // interfaces implementations
    }
}