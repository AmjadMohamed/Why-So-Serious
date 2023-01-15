using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaco_Script : MonoBehaviour
{
    // public 
    public int BoxCounter = 5;
    public int CloneCounter = 1;
    [HideInInspector] public int IsRealShaco = 1;
    [SerializeField] int IsGrounded = 0;
    [SerializeField] int CloneLifeTime = 0;
    [SerializeField] int DissapearLifeTime = 0;

    [SerializeField] GameObject Rightknife;
    [SerializeField] GameObject Leftknife;
    [SerializeField] GameObject Box;
    [SerializeField] GameObject Clone;
    [SerializeField] GameObject Camera;

    [SerializeField] float jumpPower = 5.0f;
    [SerializeField] float movemenSpeed = 2.0f;
    [SerializeField] float Rate = 2.0f;

    // private
    private bool IsWalkingRight = false;
    private bool IsWalkingLeft = false;
    private int FaceCounter = 1;
    private float BoxDropRate = 0;
    private float KnifeThrowRate = 0;
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
        //Debug.Log(CloneCounter);
        // Walking left 
        if (Input.GetAxisRaw("Horizontal") < 0 || IsWalkingLeft)
        {
            WalkToTheLeft();
        }

        // Walking Right
        else if (Input.GetAxisRaw("Horizontal") > 0 || IsWalkingRight)
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

        // Dissapearing 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShacoDissapear();
        }

        // Placing Boxes
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlaceBox();
        }

        // throwing knifes
        if (Input.GetKeyDown(KeyCode.E))
        {
            ThrowKnife();
        }


        //Summon Clone
        if (Input.GetKeyDown(KeyCode.R))
        {
            ShacoSumonClone();
        }
    }

    // Ienumerator function lets me to do some actions then wait for a certain amount of time then do another action
    IEnumerator Dissapear()
    {
        Color color1 = new Color(255, 255, 255, .25f);
        Color color2 = new Color(255, 255, 255, 1f);
        Physics2D.IgnoreLayerCollision(6, 7, true);
        GetComponent<SpriteRenderer>().color = color1;

        yield return new WaitForSeconds(DissapearLifeTime);

        Physics2D.IgnoreLayerCollision(6, 7, false);
        GetComponent<SpriteRenderer>().color = color2;
    }





    public void Jump()
    {
        //float x = Input.GetAxisRaw("Horizontal");
        if (IsGrounded == 1 && IsRealShaco == 1)
        {
            rb.velocity = new Vector2(0, 1 * jumpPower);
            anim.SetBool("IsJumping", true);
            anim.SetInteger("IsWalking", 0);
            print("Jumping");
        }
    }

    public void ShacoDissapear()
    {
        if (transform.tag == "Player" && IsGrounded == 1 && IsRealShaco == 1)
        {
            StartCoroutine("Dissapear");
        }
    }

    public void ShacoSumonClone()
    {
        if (transform.tag == "Player" && IsRealShaco == 1 && CloneCounter > 0)
        {
            StartCoroutine("SummonClone");
            CloneCounter--;
        }
    }

    public void PlaceBox()
    {
        if (transform.tag == "Player" && IsRealShaco == 1)
        {
            if (Time.time > BoxDropRate && BoxCounter > 0)
            {
                // delay between placing every box 
                BoxDropRate = Time.time + Rate;
                BoxCounter--;

                Instantiation(Box, Box, 8.5f, .5f);

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
            }

        }
    }

    public void ThrowKnife()
    {
        if (transform.tag == "Player" && IsRealShaco == 1)
        {
            // delay between throwing every knife 
            if (Time.time > KnifeThrowRate)
            {
                KnifeThrowRate = Time.time + Rate;
                Instantiation(Rightknife, Leftknife, 5.5f, -.5f);

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

            }

        }
    }

    IEnumerator SummonClone()
    {
        Instantiation(Clone, Clone, 10f, .5f);
        GetComponent<Shaco_Script>().enabled = false;
        IsRealShaco = 0;
        ActivateIdleAnim();

        yield return new WaitForSeconds(CloneLifeTime);

        GetComponent<Shaco_Script>().enabled = true;
        IsRealShaco = 1;
    }

    // a function to instantiate objects 
    void Instantiation(GameObject RightObjectToInstantiate, GameObject LeftObjectToInstantiate, float Xpos, float Ypos)
    {
        Vector2 InstantiatePos = transform.position;

        if (FaceCounter == 1)
        {
            InstantiatePos += new Vector2(Xpos, Ypos);
            Instantiate(RightObjectToInstantiate, InstantiatePos, Quaternion.identity);
        }

        else if (FaceCounter == 0)
        {
            InstantiatePos += new Vector2(-Xpos, Ypos);
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

    // moving the character to the left.
    public void WalkToTheLeft()
    {
        print("walking to the left");
        if (IsRealShaco == 1)
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
    }

    // moving the character to the right.
    public void WalkToTheRight()
    {
        print("walking to the right");
        if (IsRealShaco == 1)
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

    // Moving The camera to the next arena.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "LevelTrigger")
        {
            Vector3 NewCamPos = new Vector3(Camera.transform.position.x + 220, Camera.transform.position.y, Camera.transform.position.z);
            Camera.transform.position = NewCamPos;
            Vector3 NewTriggerPos = new Vector3(other.transform.position.x + 220, other.transform.position.y, other.transform.position.z);
            other.transform.position = NewTriggerPos;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IsGrounded = 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGrounded = 0;
    }

    public void SetWalkingToTheRight()
    {
        IsWalkingRight = true;
    }

    public void SetWalkingToTheLeft()
    {
        IsWalkingLeft = true;
    }

    public void SetIdle()
    {
        IsWalkingLeft = false;
        IsWalkingRight = false;
    }



}
