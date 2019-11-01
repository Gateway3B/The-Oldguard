/* Player Health Script
This script should be put on the player in a 2D game.
--------------------------------------------
7/19/18: 	Wilson Gansler - Fixed so it loads specific level after falling, enemy, hazard deaths
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{

    public GameObject characterSprite, character;
    public float waitTime;
    private GameObject respawn;
    public bool death;
    private bool once;
    private float counter;
    private GameObject lance, shield;

    void Start()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn");
        death = false;
        once = true;
        counter = 0f;
        lance = GameObject.FindGameObjectWithTag("Lance");
        shield = GameObject.FindGameObjectWithTag("Shield");
    }

    void Update()
    {
        if(death)
        {
            if(once)
            {
                characterSprite.GetComponent<Animator>().SetBool("Death", true);
                counter = Time.time;
                once = false;
                lance.SetActive(false);
                shield.SetActive(false);
            }
            else
            {
                if((Time.time - counter) > waitTime)
                {
                    characterSprite.GetComponent<Animator>().SetBool("Death", false);
                    character.transform.position = respawn.transform.position;
                    if((Time.time-counter) > (3*waitTime))
                    {
                        once = true;
                        death = false;
                        character.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                        lance.SetActive(true);
                        shield.SetActive(true);
                    }
                }
                else
                {
                    character.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Death")
        {
            death = true;
        }
    }
}
     

