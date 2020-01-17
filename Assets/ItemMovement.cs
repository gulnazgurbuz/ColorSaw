using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{

    private Vector3 _screenPoint;
    private Vector3 _offset;
    Vector3 curScreenPoint, curPosition;

    private Vector3Int _cellPosition, _itemPosition;
    private GameController _gameContObj;
    private GameObject _item;

    private void Start()
    {
        _gameContObj = GameObject.Find("GameController").GetComponent<GameController>();
        _item = GameObject.Find("Cube");

    }


    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            _itemPosition = _gameContObj._grid.LocalToCell(_item.transform.position);

            Debug.Log(_itemPosition);
            _screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            _offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
        }
        if (Input.GetMouseButton(0))
        {

            curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
            curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
            transform.position = new Vector3(curPosition.x, transform.position.y, curPosition.z);
            _cellPosition = _gameContObj._grid.LocalToCell(transform.position);

        }
    }
    private void FixedUpdate()
    {
        //Debug.Log(_cellPosition);
        if (_itemPosition!= _cellPosition)
        {
             _item.transform.position = _gameContObj._grid.CellToLocal(_cellPosition);
            _itemPosition = _gameContObj._grid.LocalToCell(_item.transform.position);
            Debug.Log(_itemPosition);
        }
    }



}
