using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private int cnt = 1;
    private int BoxCounter = 5 ;

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
    SpriteRenderer Sr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        MovementDir.x = Input.GetAxisRaw("Horizontal") * movemenSpeed;     
    }


    void Update()
    {
        rb.velocity = MovementDir;

        if(Input.GetAxisRaw("Horizontal") < 0 )
        {
            Sr.flipX = true;
        }

        else if(Input.GetAxisRaw("Horizontal") > 0 )
        {
            Sr.flipX = false;
        }



        if (Input.GetKeyDown(KeyCode.Space) && cnt == 1)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Q) && transform.tag =="Player")
        {
            StartCoroutine("Dissapear");
        }

        if (Input.GetKeyDown(KeyCode.E) && transform.tag == "Player" && Time.time > KnifeThrowRate)
        {
            KnifeThrowRate = Time.time + Rate;
            ThrowKnife();
        }

        if (Input.GetKeyDown(KeyCode.W) && transform.tag == "Player" && Time.time > BoxDropRate && BoxCounter > 0)
        {
            
            BoxDropRate = Time.time + Rate;
            PlaceBox();
            BoxCounter--;
        }

        if (Input.GetKeyDown(KeyCode.R) && transform.tag == "Player")
        {
            StartCoroutine("SummonClone");
        }
    }

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
        Instantiation(Clone, Clone, 4.5f);
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
        Instantiation(Box, Box, 4.5f);
    }

    void ThrowKnife()
    {
        Instantiation(Rightknife , Leftknife , 3.5f);
    }

    void Instantiation(GameObject RightObjectToInstantiate , GameObject LeftObjectToInstantiate,float Xpos)
    {
        Vector2 InstantiatePos = transform.position;
        if (Sr.flipX == false)
        {
            InstantiatePos += new Vector2(Xpos, .5f);
            var obj = Instantiate(RightObjectToInstantiate, InstantiatePos, Quaternion.identity);
            Destroy(obj, 10);
        }
        else if (Sr.flipX == true)
        {
            InstantiatePos += new Vector2(-Xpos , .5f);
            var obj = Instantiate(LeftObjectToInstantiate, InstantiatePos, Quaternion.identity);
            Destroy(obj, 10);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        cnt = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        cnt = 1;
    }

}
