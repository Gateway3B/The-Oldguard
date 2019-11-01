using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerStay : MonoBehaviour {

    private GameObject target = null;
    private Vector3 offset;
    private bool death;
    void Start()
    {
        target = null;
        death = false;
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            target = col.gameObject;
            offset = target.transform.position - transform.position;
        }
        if(col.CompareTag("PlayerHealth"))
        {
            death = col.GetComponent<Player_Health>().death;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        target = null;
    }
    void LateUpdate()
    {
        if (target != null)
        {
            if(!death)
            {
                target.transform.position = transform.position + offset;
            }
        }
    }
}
