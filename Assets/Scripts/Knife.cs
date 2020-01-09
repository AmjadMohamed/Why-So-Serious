using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    Rigidbody2D rb;

    public float ThrowPosX;
    public float ThrowPosY = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        rb.velocity = new Vector2(ThrowPosX , ThrowPosY);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
