using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private int cnt = 1;
    private int BoxCounter = 5 ;
    private int FaceCounter = 1;

    public GameObject Rightknife;
    public GameObject Leftknife;
    public GameObject Box;
    public GameObject Clone;

    public float jumpPower = 5.0f;
    public float movemenSpeed = 2.0f;
    public float Rate = 2.0f;
    private float BoxDropRate = 0;
    private float KnifeThrowRate = 0;
    

    Vector2 MovementDir;

    Rigidbody2D rb;
    Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Storing Horizontal Movement Values 
        MovementDir.x = Input.GetAxisRaw("Horizontal") * movemenSpeed;     
    }


    void Update()
    {   
        //moving in the directions stored
        rb.velocity = MovementDir;

        // Walking left 
        if(Input.GetAxisRaw("Horizontal") < 0 && cnt == 1)
        {
            FaceCounter = 0;
            anim.SetInteger("IsFacing", 2);
            anim.SetInteger("IsWalking", 1);
            anim.SetBool("IsJumping", false);
        }

        // Walking Right
        else if(Input.GetAxisRaw("Horizontal") > 0 && cnt == 1)
        {
            FaceCounter = 1;
            anim.SetInteger("IsFacing", 1);
            anim.SetInteger("IsWalking", 1);
            anim.SetBool("IsJumping", false);
        }

        else if(cnt == 0)
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
        if (Input.GetKeyDown(KeyCode.Space) && cnt == 1)
        {
            Jump();
            anim.SetBool("IsJumping", true);
            anim.SetInteger("IsWalking", 0);
        }

        // Dissapearing 
        if (Input.GetKeyDown(KeyCode.Q) && transform.tag =="Player" && cnt == 1)
        {
            StartCoroutine("Dissapear");
        }

        // Placing Boxes
        if (Input.GetKeyDown(KeyCode.W) && transform.tag == "Player" && Time.time > BoxDropRate && BoxCounter > 0 )
        {
            // delay between placing every box 
            BoxDropRate = Time.time + Rate;
            BoxCounter--;

            if (FaceCounter == 1)
            {
                anim.SetInteger("IsFacing", 1);
                anim.SetBool("IsThrowing", true);
            }

            else
            {
                anim.SetInteger("IsFacing", 2);
                anim.SetBool("IsThrowing", true);
            }

            PlaceBox();
        }

        // throwing knifes
        if (Input.GetKeyDown(KeyCode.E) && transform.tag == "Player" && Time.time > KnifeThrowRate)
        {
            KnifeThrowRate = Time.time + Rate;
            

            if(FaceCounter == 1)
            {
                anim.SetInteger("IsFacing", 1);
                anim.SetBool("IsThrowing", true);
            }

            else
            {
                anim.SetInteger("IsFacing", 2);
                anim.SetBool("IsThrowing", true);
            }

            ThrowKnife();

        }

       
        //Summon Clone
        if (Input.GetKeyDown(KeyCode.R) && transform.tag == "Player")
        {
            StartCoroutine("SummonClone");
            ActivateIdleAnim();
        }
    }

    // Ienumerator function lets me to do some actions then wait for a certain amount of time then do another action
    IEnumerator Dissapear()
    {
        Color color1 = new Color(255, 255, 255 , .25f);
        Color color2 = new Color(255, 255, 255, 1f);

        GetComponent<SpriteRenderer>().color = color1  ;

        yield return new WaitForSeconds(3);

        GetComponent<SpriteRenderer>().color = color2;
    }


    IEnumerator SummonClone()
    {
        Instantiation(Clone, Clone, 4.5f , .5f);
        GetComponent<Movement>().enabled = false;

        yield return new WaitForSeconds(10);

        GetComponent<Movement>().enabled = true;        
    }


    void Jump()
    {
        rb.velocity = new Vector2(0 , 1 * jumpPower);
    }

    void PlaceBox()
    {
        Instantiation(Box, Box, 4.5f , .5f);
    }

    void ThrowKnife()
    {
        Instantiation(Rightknife , Leftknife , 3.5f , -.5f);
    }

    // a function to instantiate objects 
    void Instantiation(GameObject RightObjectToInstantiate , GameObject LeftObjectToInstantiate,float Xpos , float Ypos)
    {
        Vector2 InstantiatePos = transform.position;

        if (FaceCounter == 1)
        {
            InstantiatePos += new Vector2(Xpos, Ypos);
            Instantiate(RightObjectToInstantiate, InstantiatePos, Quaternion.identity);
        }

        else if (FaceCounter == 0)
        {
            InstantiatePos += new Vector2(-Xpos , Ypos);
            Instantiate(LeftObjectToInstantiate, InstantiatePos, Quaternion.identity);
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
        cnt = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        cnt = 1;
    }

}
