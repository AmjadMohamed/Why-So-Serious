﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Script : MonoBehaviour
{
   // public int ButtonCounter = 1;
    public int IsGrounded = 0;
   // private int BoxCounter = 5;
    private int FaceCounter = 1;

    //public GameObject Rightknife;
    //public GameObject Leftknife;
    //public GameObject Box;
    //public GameObject Clone;

    public float jumpPower = 5.0f;
    public float movemenSpeed = 2.0f;
    //public float Rate = 2.0f;
   // private float BoxDropRate = 0;
    //private float KnifeThrowRate = 0;
    

    Vector2 MovementDir;

    Rigidbody2D rb;
    Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        // Debug.Log(IsGrounded);
        // Walking left 
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            WalkToTheLeft();
        }

        // Walking Right
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            WalkToTheRight();
        }

        else if (IsGrounded == 0)
        {
            anim.SetBool("IsJumping", true);
            anim.SetInteger("IsWalking", 0);
        }


        // Idling
        else
        {
            ActivateIdleAnim();
        }


        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }


   
    public void Jump()
    {
        //float x = Input.GetAxisRaw("Horizontal");
        if (IsGrounded == 1)
        {
            rb.velocity = new Vector2(0, 1 * jumpPower);
            anim.SetBool("IsJumping", true);
            anim.SetInteger("IsWalking", 0);
        }
    }

  
    void ActivateIdleAnim()
    {
        anim.SetInteger("IsFacing", 0);
        anim.SetInteger("IsWalking", 0);
        anim.SetBool("IsJumping", false);
        anim.SetBool("IsThrowing", false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGrounded = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IsGrounded = 1;
    }

    public void WalkToTheLeft()
    {
            transform.Translate(Vector2.left * movemenSpeed * Time.deltaTime);
            if (IsGrounded == 1)
            {
                FaceCounter = 0;
                anim.SetInteger("IsFacing", 2);
                anim.SetInteger("IsWalking", 1);
                anim.SetBool("IsJumping", false);
            }
        
    }

    public void WalkToTheRight()
    {       
            transform.Translate(Vector2.right * movemenSpeed * Time.deltaTime);
            if (IsGrounded == 1)
            {
                FaceCounter = 1;
                anim.SetInteger("IsFacing", 1);
                anim.SetInteger("IsWalking", 1);
                anim.SetBool("IsJumping", false);
            }
        
    }

}
