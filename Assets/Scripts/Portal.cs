using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform PortalPos;
    private bool IsOrange;


    private void Start()
    {
        if(this.gameObject.transform.tag == "BluePortal")
        {
            IsOrange = false;
            PortalPos = GameObject.FindGameObjectWithTag("OrangePortal").GetComponent<Transform>();
        }
        else if(this.gameObject.transform.tag == "OrangePortal")
        {
            IsOrange = true;
            PortalPos = GameObject.FindGameObjectWithTag("BluePortal").GetComponent<Transform>();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Mathf.Abs(collision.transform.position.x - this.transform.position.x) > 1)
        {            
             collision.transform.position = new Vector2(PortalPos.position.x, PortalPos.position.y);
        }
    }
}
