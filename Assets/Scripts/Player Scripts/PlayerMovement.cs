using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb; 
    public BoxCollider2D groundCheck;
    public LayerMask groundLayer;
    

    public float accelaration;
    [Range(0f, 1f)]
    public float groundDecay;
    public float groundSpeed;
    public float jumpSpeed;

    public float drag;
    
    public bool grounded;
    float xInput;
    float yInput;


    // Update is called once per frame
    void Update()
    {
        inputCheck();
        jumpHandler();
    }

    void FixedUpdate()
    {
        checkGround();
        handleXMovement();
        applyFriction();
        if(grounded)
        {
            rb.velocity *= drag;
        }
    }

    void inputCheck()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Jump");
    }

    void handleXMovement()
    {
        if(Mathf.Abs(xInput) > 0)
        {
            float increment = xInput * accelaration;
            float newSpeed = Mathf.Clamp(rb.velocity.x + increment, -groundSpeed, groundSpeed);
            rb.velocity = new Vector2(newSpeed, rb.velocity.y);

            faceInput();
        }
    }

    void faceInput()
    {
        float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(direction, 1, 1);
    }

    void jumpHandler()
    {
        if(Mathf.Abs(yInput) > 0 && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, yInput * jumpSpeed);
        }
    }

    void checkGround()
    {
        grounded =  Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundLayer).Length > 0;
    }

    void applyFriction()
    {
        if(grounded && xInput == 0 && yInput == 0)
        {
            rb.velocity *= groundDecay;
        }
    }
}