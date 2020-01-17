using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    private Vector3 _screenPoint, _offset,_curScreenPoint, _curPosition, _firstScreenPoint;
    private Vector3Int _cellPosition, _itemPosition;

    private GameController _gameContObj;
    private GameObject _item;

    private MovementEnum _movementEnum;
    private bool _mouseDownOnce = true;
    private bool _moveHorizontal, _moveVertical = false;


    private void Start()
    {
        _movementEnum = MovementEnum.EMPTY;
        _gameContObj = GameObject.Find("GameController").GetComponent<GameController>();
        _item = GameObject.Find("Cube");

    }


    private void Update()
    {
        switch (_movementEnum)
        {
            case MovementEnum.CLICK:
                _mouseDownOnce = true;

                _itemPosition = _gameContObj._grid.LocalToCell(_item.transform.position);
                _firstScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
                _screenPoint = Camera.main.WorldToScreenPoint(transform.position);
                _offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
                _movementEnum = MovementEnum.MOVE;
                break;


            case MovementEnum.MOVE:

                _curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
                _curPosition = Camera.main.ScreenToWorldPoint(_curScreenPoint) + _offset;

                if (_moveHorizontal)
                    transform.position = new Vector3(_curPosition.x, transform.position.y, transform.position.z);

                if (_moveVertical)
                    transform.position = new Vector3(transform.position.x, transform.position.y, _curPosition.z);

                _cellPosition = _gameContObj._grid.LocalToCell(transform.position);

                break;
            default:
                break;
        }

        if (Input.GetMouseButton(0))
        {

            if (Mathf.Abs(_firstScreenPoint.x - Input.mousePosition.x) > 20 && _mouseDownOnce)
            {
                _mouseDownOnce = false;
                _moveHorizontal = true;
                _moveVertical = false;

                if (Mathf.Abs(_firstScreenPoint.y - Input.mousePosition.y) > 20)
                {
                    _moveVertical = true;
                    _moveHorizontal = false;
                    _movementEnum = MovementEnum.CLICK;
                }
                _movementEnum = MovementEnum.CLICK;
            }

            if (Mathf.Abs(_firstScreenPoint.y - Input.mousePosition.y) > 20 && _mouseDownOnce)
            {
                _mouseDownOnce = false;
                _moveVertical = true;
                _moveHorizontal = false;


                if (Mathf.Abs(_firstScreenPoint.x - Input.mousePosition.x) > 20)
                {
                    _moveHorizontal = true;
                    _moveVertical = false;
                    _movementEnum = MovementEnum.CLICK;
                }
                _movementEnum = MovementEnum.CLICK;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _movementEnum = MovementEnum.EMPTY;
        }
    }
    private void FixedUpdate()
    {
        if (_itemPosition != _cellPosition)
        {
            _item.transform.position = _gameContObj._grid.CellToLocal(_cellPosition);
            _itemPosition = _gameContObj._grid.LocalToCell(_item.transform.position);
            Debug.Log(_itemPosition);
        }
    }



}

public enum MovementEnum
{
    CLICK, MOVE, EMPTY

}