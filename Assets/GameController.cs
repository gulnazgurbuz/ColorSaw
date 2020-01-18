using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameStatus GameStatusEnum;

    [HideInInspector] public Grid _grid;

    [SerializeField] private Text _buttonText;
    [SerializeField] private GameObject _baseDotForGrid, _basePivot;

    private Transform _gridParent, _primaryItemParent, _header;
    private bool rising = true;
    private bool confetti = false;
    
    private Vector3 newpos;

    private void Start()
    {
        GameStatusEnum = GameStatus.START;
        _grid = GetComponent<Grid>();
        _gridParent = GameObject.Find("GridParent").transform;
        _primaryItemParent = GameObject.Find("PrimaryItem").transform;
        _header = GameObject.Find("Header").transform;
        CreateGrid();
    }


    private void FixedUpdate()
    {
        switch (GameStatusEnum)
        {
            case GameStatus.START:
                GameStatusEnum = GameStatus.STAY;
                break;

            case GameStatus.STAY:
                if (GameObject.Find("ItemWillBeShattered").transform.childCount == 0)
                    GameStatusEnum = GameStatus.SUCCESS;
                break;

            case GameStatus.SUCCESS:
                if (confetti)
                {
                    _buttonText.text = "Next Level";
                    _buttonText.transform.parent.gameObject.SetActive(true);
                    Camera.main.transform.GetChild(0).gameObject.SetActive(true);
                }
                ObjectMove();
                break;

            case GameStatus.FAIL:
                _buttonText.text = "Try Again";
                _buttonText.transform.parent.gameObject.SetActive(true);
                break;
        }
    }


    private void CreateGrid()
    {
        for (int i = 0; i < 30; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                Vector3Int cellPosition = _grid.LocalToCell(transform.position);
                GameObject obj = Instantiate(_baseDotForGrid, new Vector3(cellPosition.x + i, 0, cellPosition.z + j), Quaternion.Euler(-90, 0, 0));
                obj.transform.SetParent(_gridParent);
            }
        }
    }

    public void ButtonClicked()
    {
        if (GameStatusEnum == GameStatus.SUCCESS)
        {
            if (SceneManager.sceneCount == SceneManager.GetActiveScene().buildIndex)
                SceneManager.LoadScene(0);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void ObjectMove()
    {
        newpos = new Vector3(_primaryItemParent.transform.position.x, 2, _primaryItemParent.transform.position.z);
        if (rising)
            _primaryItemParent.transform.position = Vector3.MoveTowards(_primaryItemParent.transform.position, newpos, 0.1f);

        if (Vector3.Distance(_primaryItemParent.transform.position, newpos) < 0.1f)
        {
            rising = false;
            newpos = new Vector3(_header.transform.position.x, 2, _header.transform.position.z - 3f);
            _primaryItemParent.transform.position = Vector3.MoveTowards(_primaryItemParent.transform.position, newpos, 0.3f);
            confetti = true;
        }
    }
}

public enum GameStatus
{
    START, STAY, SUCCESS, FAIL
}