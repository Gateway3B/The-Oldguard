using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creeper : MonoBehaviour
{
    public float dist;
    public GameObject player;
    private GameObject respawn;
    void Start()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn");
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
        if(player.GetComponent<Player_Health>().death)
        {
            StartCoroutine(move());
        }
    }

    IEnumerator move()
    {
        yield return new WaitUntil(() => player.GetComponent<Player_Health>().death == false);
        transform.position = new Vector3( 84, respawn.transform.position.y - dist, 0);
    }
}
