using UnityEngine;
using Bomberman.Grid;
public class scr : MonoBehaviour
{
    public GameObject objectCamera;
    GridBomberman _gridBomberman;
    
    void Start()
    {
        _gridBomberman = objectCamera.GetComponent<GenericGrid>().GetRefGrid();
    }

}
