using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public float CDTimer;
    public float dist;
    public float speed;
    public float CDTime;
    public GameObject lance;
    private GameObject player;
    private Vector3 activePosition;
    private Vector3 readyPosition;
    private bool CD = false;
    private bool forward = true;
    private float CDStartTime;
    public bool running = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        activePosition.x = dist;
        readyPosition = transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.flipX);
        if(player.GetComponent<SpriteRenderer>().flipX)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.localPosition = new Vector3(-Mathf.Abs(transform.localPosition.x), readyPosition.y, 0);
            readyPosition = new Vector3(-Mathf.Abs(readyPosition.x), readyPosition.y, 0);
            activePosition = new Vector3(-Mathf.Abs(activePosition.x), readyPosition.y, 0);
            GetComponent<SpriteRenderer>().sortingOrder = 2;
        }else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.localPosition = new Vector3(Mathf.Abs(transform.localPosition.x), readyPosition.y, 0);
            readyPosition = new Vector3(Mathf.Abs(readyPosition.x), readyPosition.y, 0);
            activePosition = new Vector3(Mathf.Abs(activePosition.x), readyPosition.y, 0);
            GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        if(!CD && Input.GetButtonDown("Fire1") && !lance.GetComponent<LanceController>().running)
        {
            CD = true;
            running = true;
            CDStartTime = Time.time;
        }
        if(running)
        {
            if(forward)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, activePosition, speed * Time.deltaTime);
                if (transform.localPosition.Equals(activePosition))
                {
                    forward = false;
                }
            }
            else
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, readyPosition, speed * Time.deltaTime);
                if (transform.localPosition.Equals(readyPosition))
                {
                    forward = true;
                    running = false;
                }
            }
        }
        if(CD)
        {
            CDTimer = Time.time - CDStartTime;
            if(CDTimer >= CDTime)
            {
                CD = false;
            }
        }
    }
}
