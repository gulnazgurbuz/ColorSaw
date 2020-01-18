using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (transform.tag == "WillBeShattered")
        {
            if (other.tag == "Obstacle")
                Destroy(this.gameObject);
        }
        
        if (transform.tag == "Primary")
        {
            if (other.tag == "Obstacle")
            {
                transform.gameObject.SetActive(false);
                GameController.GameStatusEnum = GameStatus.FAIL;
            }
        }

    }
}
