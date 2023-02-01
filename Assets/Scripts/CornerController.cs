using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerController : MonoBehaviour
{
    /*    private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log(collision);
            if(collision.gameObject.tag== "playerBullet"|| collision.gameObject.tag == "enemyBullet")
            {
                Destroy(collision.gameObject);
                Debug.Log("hit Corner");
            }
        }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision);
    }
}
