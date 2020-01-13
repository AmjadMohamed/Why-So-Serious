using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;

    public float LeftClamp = 0;
    public float RightClamp = 0;
    public float TopClamp = 0;
    public float DownClamp = 0;

    public int counter = 0;


    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Y) && counter == 0)
        {
            GetComponent<Camera>().orthographicSize = 35;
            transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, LeftClamp, RightClamp) + offset.x, Mathf.Clamp(player.transform.position.y, DownClamp, TopClamp) + offset.y, offset.z);
            counter = 1;
        }

        else if (Input.GetKeyDown(KeyCode.Y) && counter == 1)
        {
            GetComponent<Camera>().orthographicSize = 60;
            transform.position = new Vector3(0, 0, -10);
            counter = 0;
        }

    }

}
