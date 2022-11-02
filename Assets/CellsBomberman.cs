using UnityEngine;

namespace Bomberman.Grid
{
    internal enum TypeCell
    {
        ///<summary> без блока </summary>
        Empty,
        ///<summary> рушимый блок </summary>
        Destructible,
        ///<summary> нерушимый блок </summary>
        Unbreakable
    }

    internal class CellsBomberman
    {
        float _sizeCell;
        internal float sizeCell {get; set;}
        GameObject _objectCell;
        internal GameObject objectCell
        {
            get {return _objectCell; }
            set { _objectCell = value; }
        }
        Vector3 _worldPosition;
        internal Vector3 worldPosition 
        {
            get{return _worldPosition;}
            set{_worldPosition = value; CorrectWorldPosition();}
        }
        Vector2Int _positionInGrid;
        internal Vector2Int positionInGrid 
        {
            get {return _positionInGrid;}
            set {_positionInGrid = value;}
        }
        TypeCell _typeCell;
        internal TypeCell typeCell 
        {
            get {return _typeCell; }
            set {_typeCell = value; }
        }
        internal CellsBomberman(float sizeCell, GameObject objectCell, Vector3 worldPosition, Vector2Int positionOnGrid, TypeCell typeCell)
        {
            _sizeCell = sizeCell;
            _worldPosition = worldPosition; 
            _positionInGrid = positionOnGrid; 
            _typeCell = typeCell;
            if (objectCell != null) _objectCell =  GameObject.Instantiate(objectCell, _worldPosition, Quaternion.identity); 
            else _objectCell = new GameObject();
        }

        internal void SetActive(bool active)
        {
            _objectCell.SetActive(active);
            typeCell = (active) ? TypeCell.Destructible : TypeCell.Empty;
        }

        internal void AssignColliders() => _objectCell.AddComponent(typeof(BoxCollider2D));
        internal void CorrectWorldPosition() => _objectCell.transform.position = _worldPosition;

        internal bool IsEmpty() => (typeCell == TypeCell.Empty);
        internal bool IsNotEmpty() => !IsEmpty();
        internal bool CheckType(TypeCell typeCell) => (_typeCell == typeCell); 

        internal void RemoveCell() 
        {
            // пока пусто :(
        }
    }
}