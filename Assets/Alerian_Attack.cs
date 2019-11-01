using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alerian_Attack : MonoBehaviour
{

    public GameObject stationary1, stationary2, attack1, attack2, controller;
    public float moveSpeed, stateChangeSpeed;
    private float randVal, lastMove;
    private bool nextState, oneRand, attack, moving;
    public bool forward;

    void Start()
    {
        transform.localPosition = stationary1.transform.localPosition;
        lastMove = Time.time;
        nextState = true;
        oneRand = true;
        forward = true;
        attack = false;
        moving = false;
    }

    void Update()
    {
        attack = controller.GetComponent<Alerian_Controller>().stop;
        if(attack || moving || !forward)
        {
            if(Time.time - lastMove > stateChangeSpeed)
            {
                if(oneRand)
                {
                    randVal = Random.value;
                    oneRand = false;
                }
                if(randVal > .65f)
                {
                    if(nextState)
                    {
                        if(moveTo(stationary2))
                        {
                            lastMove = Time.time;
                            oneRand = true;
                            nextState = false;
                        }
                    }
                    else
                    {
                        if(moveTo(stationary1))
                        {
                            lastMove = Time.time;
                            oneRand = true;
                            nextState = true;
                        }
                    }
                }
                else
                {
                    if(nextState)
                    {
                        if(forward)
                        {
                            if(moveTo(attack1))
                            {
                                forward = false;
                            }
                        }
                        else
                        {
                            if(moveTo(stationary1))
                            {
                                lastMove = Time.time;
                                oneRand = true;
                                forward = true;
                            }
                        }                    
                    }
                    else
                    {
                        if(forward)
                        {
                            if(moveTo(attack2))
                            {
                                forward = false;
                            }
                        }
                        else
                        {
                            if(moveTo(stationary2))
                            {
                                lastMove = Time.time;
                                oneRand = true;
                                forward = true;
                            }
                        } 
                    }
                }
            }
        }
    }

    bool moveTo(GameObject position)
    {
        moving = true;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, position.transform.localPosition, moveSpeed * Time.deltaTime);
        if (transform.localPosition.Equals(position.transform.localPosition))
        {
            return true;
        }
        moving = false;
        return false;
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Shield")
        {
            forward = false;
        }
    }
}