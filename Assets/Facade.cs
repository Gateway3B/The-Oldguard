using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facade : MonoBehaviour {

    [SerializeField] private GameObject displayed;
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        displayed.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            displayed.SetActive(true);
        }
    }
}
