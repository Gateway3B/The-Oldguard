using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Update : MonoBehaviour {
    public int playerSpeed = 10;
    public int playerJumpPower = 1250;
    public GameObject characterSprite;
    private float moveX;
    private float moveY;

    [Tooltip("Only change this if your character is having problems jumping when they shouldn't or not jumping at all.")]
    public float distToGround = 1.0f;
    private bool inControl = true;

    [Tooltip("Everything you jump on should be put in a ground layer. Without this, your player probably* is able to jump infinitely")]
    public LayerMask GroundLayer;





    // Update is called once per frame
    void Update()
    {
        if (inControl)
        {
            PlayerMove();
        }
        
    }

    void PlayerMove()
    {
        //CONTROLS
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

        //ANIMATIONS
        if (moveX != 0)
        {
            characterSprite.GetComponent<Animator>().SetBool("Walking", true);
        }
        else
        {
            characterSprite.GetComponent<Animator>().SetBool("Walking", false);
        }
        if (!IsGrounded())
        {
            characterSprite.GetComponent<Animator>().SetBool("Jump", true);
        }else{
            characterSprite.GetComponent<Animator>().SetBool("Jump", false);
        }

        //PLAYER DIRECTION
        if (moveX < 0.0f && IsGrounded())
        {
            GetComponent<SpriteRenderer>().flipX = true;
            GameObject[] sprites = GameObject.FindGameObjectsWithTag("CharacterSprite");
            foreach (GameObject sprite in sprites)
            {
                sprite.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else if (moveX > 0.0f && IsGrounded())
        {
            GetComponent<SpriteRenderer>().flipX = false;
            GameObject[] sprites = GameObject.FindGameObjectsWithTag("CharacterSprite");
            foreach (GameObject sprite in sprites)
            {
                sprite.GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        //PHYSICS
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        //JUMPING CODE
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);

    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distToGround, GroundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;

    }

    public void SetControl(bool b)
    {
        inControl = b;
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.CompareTag("Platform"))
        {
            GameObject.FindWithTag("MainCamera").GetComponent<Camera_Update>().platform = true;
        }
     }
 
    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Platform"))
        {
            GameObject.FindWithTag("MainCamera").GetComponent<Camera_Update>().platform = false;
        }
     }    
}
