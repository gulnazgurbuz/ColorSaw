using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private Vector3 _firstScreenPoint;
    private Vector3 _screenPoint;
    private Vector3 _offset;
    private Vector3Int _cellPosition;

    private GameController _gameContObj;
    bool test = false;
    private void Start()
    {
        _gameContObj = GameObject.Find("GameController").GetComponent<GameController>();
    }



    private void Update()
    {
        FindPPosition();

        if (Input.GetMouseButtonDown(0))
        {
            _firstScreenPoint = Input.mousePosition;
            _screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            _offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
        }
        if (Input.GetMouseButton(0))
        {
            
            if (test)
            {
                Vector3 curScreenPoint = Input.mousePosition;
                if (Mathf.Abs(_firstScreenPoint.x - curScreenPoint.x) > 50)
                {
                    if (_firstScreenPoint.x > curScreenPoint.x)
                    {
                        Debug.Log("LEFT");
                        //left
                        _cellPosition.Set(_cellPosition.x - 1, _cellPosition.y, _cellPosition.z);
                        transform.position = Vector3.MoveTowards(transform.position, _gameContObj._grid.CellToLocal(_cellPosition), 1);
                    }
                    else
                    {
                        Debug.Log("RIGHT");
                        _cellPosition.Set(_cellPosition.x + 1, _cellPosition.y, _cellPosition.z);
                        transform.position = Vector3.MoveTowards(transform.position, _gameContObj._grid.CellToLocal(_cellPosition), 1);
                    }
                }
                if (Mathf.Abs(_firstScreenPoint.y - curScreenPoint.y) > 50)
                {

                    if (_firstScreenPoint.y > curScreenPoint.y)
                    {
                        Debug.Log("DOWN");
                        //down
                        _cellPosition.Set(_cellPosition.x, _cellPosition.y, _cellPosition.z - 1);
                        transform.position = Vector3.MoveTowards(transform.position, _gameContObj._grid.CellToLocal(_cellPosition),1);
                    }
                    else
                    {
                        Debug.Log("UP");
                        _cellPosition.Set(_cellPosition.x, _cellPosition.y, _cellPosition.z + 1);
                        transform.position = Vector3.MoveTowards(transform.position, _gameContObj._grid.CellToLocal(_cellPosition),1);
                    }
                }
                test = false;
            }
            else
            {
                StartCoroutine(Movement());
            }


        }
    }
    private void FindPPosition()
    {
        _cellPosition = _gameContObj._grid.LocalToCell(transform.position);
        //  Debug.Log(cellPosition);
    }
    //private void FixedUpdate()
    //{
    //    Movement();
    //}

    private IEnumerator Movement()
    {
        yield return new WaitForSeconds(0.01f);
        test = true;
    }



}
