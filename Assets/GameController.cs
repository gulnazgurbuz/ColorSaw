using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    float x, z;
    [SerializeField] private GameObject _baseDotForGrid;
    [SerializeField] private GameObject _basePivot;
    [HideInInspector] public Grid _grid;
    private void Start()
    {
        _grid = GetComponent<Grid>();
        CreateGrid();
        x = transform.position.x;
        z = transform.position.z;
    }

    private void Update()
    {

    }



    private void CreateGrid()
    {
        for (int i = 0; i < 30; i++)
        {
            for (int j = 0; j < 50; j++)
            {

                Vector3Int cellPosition = _grid.LocalToCell(transform.position);
                Instantiate(_baseDotForGrid,new Vector3( cellPosition.x+i, 0, cellPosition.z+j), Quaternion.Euler(-90, 0, 0));
            }
        }
    }
}
