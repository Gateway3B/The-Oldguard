using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
    // Attach this to your checkpoints. Checkpoints should have a collider 2D set to trigger.
    // If you want to make a sprite animate on activating the checkpoint, let me know! It shouldn't be too hard to program.
    private GameObject respawn;
    private bool activated = false;
    private SpriteRenderer sprite;
	
	void Start () {
        respawn = GameObject.FindGameObjectWithTag("Respawn");
        sprite = gameObject.GetComponentInChildren(typeof(SpriteRenderer)) as SpriteRenderer;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!activated)
        {
            if (collision.CompareTag("Player"))
            {
                respawn.transform.position = transform.position;
                sprite.color = Color.blue;
            }
        }
        
    }

    /* private void OnTriggerExit2D(Collider2D collision)
    {
        sprite.color = Color.white;
    } */

}
