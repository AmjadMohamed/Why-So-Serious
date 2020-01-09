using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;

    void Update()
    {
        GetComponent<Camera>().orthographicSize = 25;
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
                  
        if (Input.GetKey(KeyCode.Y))
        {
            transform.position = new Vector3(0, 0, -10);
            GetComponent<Camera>().orthographicSize = 50;            
        }

    }

}
