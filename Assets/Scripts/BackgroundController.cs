using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float speed;



    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Bottom")
        {
            Vector3 newPos=this.transform.position;
            newPos.y = 10.35f;
            this.transform.position = newPos;
        }
    }
}
