using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alerian_Controller : MonoBehaviour
{
    public float patience;
    public int health;
    public GameObject activator, alerianHood, alerianCloak, alerianArm;
    public Transform pointA;
    public Transform pointB;
    public bool isRight = true;
    public float speed = 0.3f;
    private Vector3 pointAPosition;
    private Vector3 pointBPosition;
    public bool stop = false;
    private float patienceTime;
    private Vector3 forwardScale;
    private Vector3 backwardScale;
    private bool color;
    private float dmgTimer;
    public float colorTime;
    // Use this for initialization
    void Start()
    {
        pointAPosition = new Vector3(pointA.position.x, 0, 0);
        pointBPosition = new Vector3(pointB.position.x, 0, 0);
        forwardScale = transform.localScale;
        forwardScale = new Vector3(Mathf.Abs(forwardScale.x), forwardScale.y, forwardScale.z);
        backwardScale = Vector3.Scale(forwardScale, new Vector3(-1, 1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        if(color)
        {
            alerianHood.GetComponent<SpriteRenderer>().color = Color.red;
            alerianCloak.GetComponent<SpriteRenderer>().color = Color.red;
            alerianArm.GetComponent<SpriteRenderer>().color = Color.red;
            if((Time.time - dmgTimer) > colorTime)
            {
                color = false;
                alerianHood.GetComponent<SpriteRenderer>().color = Color.white;
                alerianCloak.GetComponent<SpriteRenderer>().color = Color.white;
                alerianArm.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if(health <= 0)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
        Vector3 thisPosition = new Vector3(transform.position.x, 0, 0);
        if(activator.GetComponent<Alerian_Activator>().activate)
        {
            if(activator.GetComponent<Alerian_Activator>().right)
            {
                this.transform.localScale = forwardScale;
            }
            else
            {             
                this.transform.localScale = backwardScale;
            }
            
            patienceTime = Time.time;
            stop = true;
        }
        if(!stop)
        {
            if (isRight)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
                this.transform.localScale = forwardScale;
                if (thisPosition.Equals(pointBPosition))
                {
                    //Debug.Log ("Position b");
                    isRight = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
                this.transform.localScale = backwardScale;
                if (thisPosition.Equals(pointAPosition))
                {
                    //Debug.Log ("Position a");
                    isRight = true;
                }
            }
        }
        else
        {
            if((Time.time - patienceTime) >= patience)
            {
                stop = false;
            }
        }
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Lance")
        {
            health--;
            color = true;
            dmgTimer = Time.time;
        }
    }
}
