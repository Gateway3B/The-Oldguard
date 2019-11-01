using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datacron : MonoBehaviour
{
    public float time;
    public AudioSource sound1, sound2;
    public GameObject activate, text, deactivate;
    private GameObject character, lance, shield;

    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        lance = GameObject.FindGameObjectWithTag("Lance");
        shield = GameObject.FindGameObjectWithTag("Shield");
        StartCoroutine(deactivation());
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            lance.SetActive(false);
            shield.SetActive(false);
            character.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            if(deactivate != null)
            {
                deactivate.SetActive(false);
            }
            StartCoroutine(play());
        }
    }
    IEnumerator play()
    {
        sound1.Play(0);
        text.SetActive(true);
        yield return new WaitWhile (()=> sound1.isPlaying);
        sound2.Play(0);
        yield return new WaitWhile (()=> sound2.isPlaying);
        yield return new WaitForSeconds(time);
        text.SetActive(false);
        character.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        lance.SetActive(true);
        shield.SetActive(true);
        if(activate != null)
        {
            activate.SetActive(true);
        }
        gameObject.SetActive(false);
    }
    IEnumerator deactivation()
    {
        yield return new WaitForSeconds(.01f);
        lance.SetActive(false);
        shield.SetActive(false);
        if(activate != null)
        {
        activate.SetActive(false);
        }
    }
}
