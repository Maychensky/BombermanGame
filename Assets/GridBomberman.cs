using UnityEngine;
using System;

namespace Bomberman.Grid
{
    internal class GridBomberman
    {
        int _numberCellsWidth;
        internal int numberCellsWidth 
        {
            get { return _numberCellsWidth; }
            set { _numberCellsWidth = CorrectInputNumberCells(value); }
        }
        int _numberCellsHeight;
        internal int numberCellsHeight 
        { 
            get { return _numberCellsHeight; }
            set { _numberCellsHeight = CorrectInputNumberCells(value); }
        }
        Vector3 _position;
        internal Vector3 position
        {
            get {return _position;}
            set {_position = value; CorrectPositionGrid();}
        }
        float _sizeCell;
        internal float sizeCell 
        {
            get {return _sizeCell;}
            private set {_sizeCell = value;}
        }
        float _positionZForBlocks;
        GameObject _spriteForDestructibleBlock;
        GameObject _spriteForUnbreakableBlock;
        GameObject _spriteBackGround;
        GameObject[] _collidersForSides;
        CellsBomberman[,] _cellsGrid;
        CellsBomberman[] _cellsCurb;
        GridBomberman() { } // bat on an empty constructor
        internal GridBomberman(int numberCellsWidth, int numberCellsHeight, float sizeCell,
        GameObject spriteForDestructibleBlock, GameObject spriteForUnbreakableBlock, GameObject spriteBackGround)
        {
            _collidersForSides = new GameObject[4];
            this.numberCellsWidth = numberCellsWidth; 
            this.numberCellsHeight = numberCellsHeight;
            _sizeCell = sizeCell;
            _spriteForDestructibleBlock = spriteForDestructibleBlock;
            _spriteForUnbreakableBlock = spriteForUnbreakableBlock;  
            _spriteBackGround = spriteBackGround;
            CreateGrid();
        }

        int CorrectInputNumberCells(int numberCells) 
        {
            if (numberCells < 3) numberCells = 3;
            else if (numberCells.IsEven()) numberCells++;
            return numberCells;
        }

        void CreateGrid()
        {
            RemoveOldGrid();
            var numberCellsForCurb = _numberCellsWidth * 2 + _numberCellsHeight * 2 + 4;
            _cellsGrid = new CellsBomberman[_numberCellsWidth, _numberCellsHeight];
            _cellsCurb = new CellsBomberman[numberCellsForCurb];
            CreateCellsGrid();
            CreateCellsCurb();
            CreateColliders();
        }

        void CreateCellsGrid()
        {
            var worldPosition = new Vector3();
            var positionInGrid = new Vector2Int();
            for (int x = 0; x < _numberCellsWidth ; x ++)
                for (int y = 0; y < _numberCellsHeight; y ++)
                    if ( x.IsNotEven() && y.IsNotEven())
                    {
                        worldPosition.Set(x, y, _positionZForBlocks);
                        positionInGrid.Set(x, y);
                        _cellsGrid[x,y] = new CellsBomberman(_sizeCell, _spriteForUnbreakableBlock, worldPosition, positionInGrid, TypeCell.Unbreakable);
                    }
                    else
                    {   
                        worldPosition.Set(x,y,_positionZForBlocks);
                        positionInGrid.Set(x,y);
                        _cellsGrid[x,y] = new CellsBomberman(_sizeCell, _spriteForDestructibleBlock, worldPosition, positionInGrid, TypeCell.Destructible);
                        _cellsGrid[x,y].AssignColliders();
                        _cellsGrid[x,y].SetActive(false);
                    }
        }

        void CreateCellsCurb()
        {
            var worldPosition = new Vector3();
            var positionInGrid = new Vector2Int();
            var index = 0;
            for (int x = -1; x <= _numberCellsWidth; x ++)
                for (int y = -1; y <= _numberCellsHeight; y ++)
                    if ( !IsLocationInGrid(x,y))
                    {
                        worldPosition.Set(x, y, _positionZForBlocks);
                        positionInGrid.Set(x, y);
                         _cellsCurb[index ++] = new CellsBomberman(_sizeCell, _spriteForUnbreakableBlock, worldPosition, positionInGrid, TypeCell.Unbreakable);
                    }
        }
        
        void CreateColliders()
        {
            var positionAngleLeftDown = _cellsCurb[0].worldPosition;
            var positionAngleLeftUp = _cellsCurb[_numberCellsHeight + 1].worldPosition;
            var positionAngleRigthDown = _cellsCurb[_cellsCurb.Length - _numberCellsHeight - 2].worldPosition;
            var positionAngleRigthUp = _cellsCurb[_cellsCurb.Length - 1].worldPosition;
            var index = 0;
            CreateCollidersForSide(positionAngleLeftDown, positionAngleLeftUp, ref _collidersForSides[index ++]);
            CreateCollidersForSide(positionAngleLeftUp, positionAngleRigthUp, ref _collidersForSides[index ++]);
            CreateCollidersForSide(positionAngleRigthUp, positionAngleRigthDown, ref _collidersForSides[index ++]);
            CreateCollidersForSide(positionAngleRigthDown, positionAngleLeftDown, ref _collidersForSides[index ++]);
            foreach (var cell in _cellsGrid)
                if (cell.IsNotEmpty()) cell.AssignColliders();
        }

        void CreateCollidersForSide(Vector3 positionFirstCell, Vector3 positionLastCell, ref GameObject refForColliders)
        {
            refForColliders = new GameObject("collider side",typeof(BoxCollider2D));
            refForColliders.transform.position = (positionFirstCell + positionLastCell) / 2;
            refForColliders.GetComponent<BoxCollider2D>().size = new Vector2(
                Math.Abs(positionFirstCell.x - positionLastCell.x + _sizeCell),
                Math.Abs(positionFirstCell.y - positionLastCell.y + _sizeCell)
            );
        }

        void CorrectPositionGrid()
        {
            var vectorInNewCentralPosition =  _position - GetCentralPosition();
            foreach (var cellCurb in _cellsCurb) cellCurb.worldPosition += vectorInNewCentralPosition;
            foreach (var cellGrid in _cellsGrid) cellGrid.worldPosition += vectorInNewCentralPosition;
            foreach (var colliders in _collidersForSides) colliders.transform.position += vectorInNewCentralPosition;
            
        }

        internal Vector3 GetCentralPosition() => (_cellsGrid[1,1].worldPosition + _cellsGrid[_numberCellsWidth -2, _numberCellsHeight - 2].worldPosition) / 2;
        internal bool IsLocationInGrid(Vector2Int cellPositionInGrid) => IsLocationInGrid(cellPositionInGrid.x, cellPositionInGrid.y);
        internal bool IsLocationInGrid(int cellPositionInGridX, int cellPositionInGridY)
         => (cellPositionInGridX >= 0 && cellPositionInGridX < _numberCellsWidth && cellPositionInGridY >= 0 &&  cellPositionInGridY < _numberCellsHeight);
        
        internal CellsBomberman GetCell(Vector2Int cellPositionInGrid) => GetCell(cellPositionInGrid.x, cellPositionInGrid.y);
        internal CellsBomberman GetCell(int cellPositionInGridX, int cellPositionInGridY) => _cellsGrid[cellPositionInGridX, cellPositionInGridY];

        internal void GenericDestructibleBlocks(double probability) 
        {
            DeleteDestructibleBlocks();
            if(probability < 0 || probability > 1 )
                throw new Exception("вероятноть превышает допустимное значение");
            var random = new System.Random();
            for (int x = 0; x < _cellsGrid.GetLength(0); x ++)
                for (int y = 0; y < _cellsGrid.GetLength(1); y ++)
                    if (_cellsGrid[x,y].IsEmpty() && random.NextDouble() < probability)
                        _cellsGrid[x,y].SetActive(true);
            _cellsGrid[0,_numberCellsHeight - 1].SetActive(false);
            _cellsGrid[1,_numberCellsHeight - 1].SetActive(false);
            _cellsGrid[0,_numberCellsHeight - 2].SetActive(false);
        }

        internal void DeleteDestructibleBlocks()
        {
            foreach (var cell in _cellsGrid)
                if (cell.CheckType(TypeCell.Destructible))
                    cell.SetActive(false);
        }

        void RemoveOldGrid() 
        { 
            if (_cellsGrid != null)
                foreach (var cell in _cellsGrid)
                    cell.RemoveCell();
        }

    }
}