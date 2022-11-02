using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Bomberman.Grid
{
    public class GenericGrid : MonoBehaviour
    {
        [Tooltip("спрайт для разрушаемых блоков")]
        public GameObject destructibleBlock;

        [Tooltip("спрат для нерушимых блоков")]
        public GameObject unbreakableBlock;
        GridBomberman _grid;
        
        // неверная логика будет присвоен только первому
        internal float _leftExtremePositionGrid { get; private set; }
        internal float _rigthExtremePositionGrid { get; private set; }

        void Awake()
        {
            _grid = new GridBomberman(40,11,1,destructibleBlock,unbreakableBlock,unbreakableBlock);
            _grid.position = new Vector3(0,-1);
            _grid.GenericDestructibleBlocks(0.2);
            Test();
            InitExttPositionGrid();
        }

        void Start()
        {

        }

        internal ref GridBomberman GetRefGrid()
        {

            Debug.Log("добрались");
            return ref _grid;
        }

        internal void Test()
        {

        }

        /* тут что то не верно нужно поправить, потому что изначально все было хорошо 
            может стоит все исправить или попробовать начать с левой стороны */

        internal float GetDistanceToNearestCurbHorisontal(float positionX)
         => (positionX - _leftExtremePositionGrid < _rigthExtremePositionGrid - positionX) ? positionX - _leftExtremePositionGrid : _rigthExtremePositionGrid - positionX;

        internal Vector3 GetPositionCellForStartPleayr() => _grid.GetCell(0, _grid.numberCellsHeight - 1).worldPosition;

        void InitExttPositionGrid()
        {
            _leftExtremePositionGrid = _grid.GetCell(0,0).worldPosition.x;
            _rigthExtremePositionGrid = _grid.GetCell(_grid.numberCellsWidth - 1,0).worldPosition.x;
            var shiftPositionX = _grid.sizeCell * 1.5f;
            _leftExtremePositionGrid += _leftExtremePositionGrid.IsNegative() ? - shiftPositionX : shiftPositionX;
            _rigthExtremePositionGrid += _rigthExtremePositionGrid.IsNegative() ? - shiftPositionX : shiftPositionX;
        }

    }
}
