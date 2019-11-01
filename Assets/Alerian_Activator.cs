using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alerian_Activator : MonoBehaviour
{
    public bool activate = false;
    public bool right;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        activate = true;
        if(collision.transform.position.x - this.transform.position.x > 0)
        {
            right = true;
        }
        else
        {
            right = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        activate = false;
    }
}
